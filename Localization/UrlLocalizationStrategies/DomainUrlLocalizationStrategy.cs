// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.DomainUrlLocalizationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Represents a strategy that sores/loads urls based on the domain/subdomain
  /// <example>
  /// default language: domain.com/page.aspx
  /// second language: fr.domain.com/page.aspx or domain.fr/page.aspx
  /// </example>
  /// </summary>
  public class DomainUrlLocalizationStrategy : IUrlLocalizationStrategy
  {
    private static readonly Regex domainRegex = new Regex("((?<ProtocolType>\\w+):\\/\\/)?(?<DomainName>[\\w.\\-:]+\\/?)(?<RestOfUrl>.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Initializes the strategy
    /// Expects that the keys in the urlstrategysettings are actually the names of the defined languages (CultureElement key).
    /// This represents a map between the culture and the domain that will correspond to it.
    /// </summary>
    public void Initialize(IUrlLocalizationContext context)
    {
      this.defaultCultures = context.GetDefaultLanguageCultures();
      this.uiCultureSettings = (IDictionary<int, string>) new Dictionary<int, string>();
      IDictionary<string, ICultureSetting> dictionary = (IDictionary<string, ICultureSetting>) null;
      IUrlLocalizationStrategySettings strategySettings = context.StrategySettings;
      if (strategySettings != null)
      {
        dictionary = (IDictionary<string, ICultureSetting>) strategySettings.GetCultureSettings().ToDictionary<ICultureSetting, string, ICultureSetting>((Func<ICultureSetting, string>) (i => i.CultureKey), (Func<ICultureSetting, ICultureSetting>) (i => i));
        NameValueCollection parameters = strategySettings.Parameters;
      }
      IEnumerable<LanguageCultures> languageCultures = context.GetAllLanguageCultures();
      this.languageCultures = (IDictionary<string, ILanguageCultures>) new Dictionary<string, ILanguageCultures>();
      foreach (LanguageCultures language in languageCultures)
      {
        string str1 = dictionary == null || !dictionary.ContainsKey(language.Key) ? this.BuildLanguageSetting((ILanguageCultures) language) : dictionary[language.Key].Setting;
        if (!string.IsNullOrEmpty(str1))
        {
          MultisiteUrlLocalizationContext localizationContext = context as MultisiteUrlLocalizationContext;
          int cultureLcid = AppSettings.CurrentSettings.GetCultureLcid(language.UICulture);
          string str2 = str1;
          char[] separator = new char[1]{ ',' };
          foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            string ascii = new IdnMapping().GetAscii(this.GetLanguageCultureKey(str3.Trim()));
            if (!this.languageCultures.ContainsKey(ascii))
            {
              this.languageCultures.Add(ascii, (ILanguageCultures) language);
              bool flag = false;
              if (!this.uiCultureSettings.ContainsKey(cultureLcid))
                this.uiCultureSettings.Add(cultureLcid, ascii);
              else if (!flag && localizationContext != null && (localizationContext.Site.LiveUrl == ascii || localizationContext.Site.DomainAliases.Contains(ascii) || localizationContext.Site.StagingUrl == ascii))
                this.uiCultureSettings[cultureLcid] = ascii;
            }
          }
        }
      }
    }

    /// <summary>Builds the language setting.</summary>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    protected virtual string BuildLanguageSetting(ILanguageCultures language) => (string) null;

    /// <summary>
    /// Processes the specified URL and modifies if needed. Detects the culture/UIculture from it.
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="culture">Culture</param>
    /// <param name="uiCulture">UI culture</param>
    /// <returns>
    /// Returns the modified URL and cultures that can be taken from it
    /// </returns>
    public virtual string UnResolveUrl(
      string url,
      out CultureInfo culture,
      out CultureInfo uiCulture)
    {
      culture = this.defaultCultures.Culture;
      uiCulture = this.defaultCultures.UICulture;
      string domain;
      ILanguageCultures languageCultures;
      if (this.TryGetCurrentDomain(out domain) && this.languageCultures.TryGetValue(domain, out languageCultures))
      {
        culture = languageCultures.Culture;
        uiCulture = languageCultures.UICulture;
      }
      return url;
    }

    /// <summary>
    /// Resolves the url to such that has language information in it
    /// </summary>
    /// <param name="url">Url to be processed</param>
    /// <returns>The url with the language info in it</returns>
    /// <remarks>Useses the current UI culture as a target</remarks>
    public string ResolveUrl(string url) => this.ResolveUrl(url, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Resolves the url to such that has language information in it
    /// </summary>
    /// <param name="url">Url to be processed</param>
    /// <param name="targetCulture">Culture for the resolved url</param>
    /// <returns>Resolved url for the specified culture</returns>
    public string ResolveUrl(string url, CultureInfo targetCulture) => this.ResolveUrl(url, targetCulture, false);

    internal string ResolveUrl(string url, CultureInfo targetCulture, bool skipCurrentDomainCheck)
    {
      string host;
      string domain;
      if (!url.StartsWith("/") && !url.StartsWith("~/") || !this.uiCultureSettings.TryGetValue(AppSettings.CurrentSettings.GetCultureLcid(targetCulture), out host) || !skipCurrentDomainCheck && (!this.TryGetCurrentDomain(out domain) || domain.Equals(host)))
        return url;
      url = url.Replace("~/", "/");
      return UrlPath.ResolveAbsoluteUrlWithHost(url, host);
    }

    internal string GetLanguageCultureKey(string host)
    {
      Match match = DomainUrlLocalizationStrategy.domainRegex.Match(host);
      if (match.Success)
      {
        if (match.Groups["DomainName"].Success)
          host = match.Groups["DomainName"].Value;
        return VirtualPathUtility.RemoveTrailingSlash(host.ToLowerInvariant());
      }
      throw new ApplicationException("Wrong domain set: {0}".Arrange((object) host));
    }

    private bool TryGetCurrentDomain(out string domain)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null && currentHttpContext.Request != null)
      {
        Uri url = currentHttpContext.Request.Url;
        if (url != (Uri) null)
        {
          domain = url.Authority.ToLowerInvariant();
          IdnMapping idnMapping = new IdnMapping();
          domain = idnMapping.GetAscii(domain);
          return true;
        }
      }
      domain = (string) null;
      return false;
    }

    private IDictionary<string, ILanguageCultures> languageCultures { get; set; }

    private IDictionary<int, string> uiCultureSettings { get; set; }

    private ILanguageCultures defaultCultures { get; set; }
  }
}
