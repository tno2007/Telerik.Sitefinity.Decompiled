// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.SubFolderUrlLocalizationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Class that provides the functionality to store/load the language version of the page depends on a subfolder appended after the domain
  /// <example>
  /// default language: domain.com/page.aspx
  /// second language: domain.com/fr/page.aspx
  /// </example>
  /// </summary>
  public class SubFolderUrlLocalizationStrategy : IUrlLocalizationStrategy
  {
    private IDictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper> languageCultures = (IDictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper>) new Dictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper>();
    private IDictionary<string, string> uiCultureSettings;
    private ILanguageCultures defaultCultures;
    private bool includeSubfoderPrefixForDefaultLanguage;
    private string specificCultureSettingSeparator;

    /// <summary>
    /// Initializes the strategy
    /// For each culture there should be a subfolder(en/es/fr/etc) specified in strategy culture settings
    /// </summary>
    public virtual void Initialize(IUrlLocalizationContext context)
    {
      this.defaultCultures = context.GetDefaultLanguageCultures();
      this.uiCultureSettings = (IDictionary<string, string>) new Dictionary<string, string>();
      IDictionary<string, ICultureSetting> dictionary = (IDictionary<string, ICultureSetting>) null;
      NameValueCollection nameValueCollection = (NameValueCollection) null;
      IUrlLocalizationStrategySettings strategySettings = context.StrategySettings;
      if (strategySettings != null)
      {
        dictionary = (IDictionary<string, ICultureSetting>) strategySettings.GetCultureSettings().ToDictionary<ICultureSetting, string, ICultureSetting>((Func<ICultureSetting, string>) (i => i.CultureKey), (Func<ICultureSetting, ICultureSetting>) (i => i));
        nameValueCollection = strategySettings.Parameters;
      }
      if (nameValueCollection != null)
      {
        string str1 = nameValueCollection["includeSubfoderPrefixForDefaultLanguage"];
        bool result;
        if (!string.IsNullOrEmpty(str1) && bool.TryParse(str1, out result))
          this.includeSubfoderPrefixForDefaultLanguage = result;
        string str2 = nameValueCollection["specificCultureSettingSeparator"];
        if (!string.IsNullOrEmpty(str2))
          this.specificCultureSettingSeparator = str2;
      }
      IEnumerable<LanguageCultures> languageCultures = context.GetAllLanguageCultures();
      this.languageCultures = (IDictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper>) new Dictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper>();
      foreach (LanguageCultures language1 in languageCultures)
      {
        string key = dictionary == null || !dictionary.ContainsKey(language1.Key) ? this.BuildLanguageSetting((ILanguageCultures) language1) : dictionary[language1.Key].Setting.ToLowerInvariant();
        this.uiCultureSettings.Add(language1.UICulture.Name, key);
        string[] strArray = key.Split('/');
        if (strArray.Length > 1)
          key = strArray[0];
        SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper language2;
        if (!this.languageCultures.TryGetValue(key, out language2))
        {
          language2 = new SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper();
          this.languageCultures.Add(key, language2);
        }
        SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper languageCulturesWrapper = language2;
        if (strArray.Length > 1)
        {
          for (int index = 1; index < strArray.Length; ++index)
          {
            if (!languageCulturesWrapper.TryGetChild(strArray[index], out language2))
            {
              language2 = new SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper();
              languageCulturesWrapper.Children.Add(strArray[index], language2);
            }
            languageCulturesWrapper = language2;
          }
        }
        languageCulturesWrapper.Language = (ILanguageCultures) language1;
      }
    }

    /// <summary>Builds the language setting.</summary>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    protected virtual string BuildLanguageSetting(ILanguageCultures language)
    {
      CultureInfo uiCulture = language.UICulture;
      string str = uiCulture.Name.ToLowerInvariant();
      if (!uiCulture.IsNeutralCulture && !string.IsNullOrEmpty(this.specificCultureSettingSeparator))
        str = str.Replace("-", this.specificCultureSettingSeparator);
      return str;
    }

    /// <summary>
    /// Processes the specified URL and modifies if needed. Detects the culture/UI culture from it.
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
      IList<string> pathSegmentStrings1 = RouteHelper.SplitUrlToPathSegmentStrings(url, true);
      if (pathSegmentStrings1.Count > 0)
      {
        int index = 0;
        if (url.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
        {
          IList<string> pathSegmentStrings2 = RouteHelper.SplitUrlToPathSegmentStrings(HttpRuntime.AppDomainAppVirtualPath, true);
          index = pathSegmentStrings2.Count >= pathSegmentStrings1.Count ? pathSegmentStrings2.Count - 1 : pathSegmentStrings2.Count;
        }
        else if (url.StartsWith("~/"))
          ++index;
        SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper language = (SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper) null;
        while (index < pathSegmentStrings1.Count)
        {
          string lowerInvariant = pathSegmentStrings1[index].ToLowerInvariant();
          if (language != null)
          {
            if (!language.TryGetChild(lowerInvariant, out language))
              break;
          }
          else if (!this.languageCultures.TryGetValue(lowerInvariant, out language))
            break;
          if (!language.IsTransient)
          {
            culture = language.Language.Culture;
            uiCulture = language.Language.UICulture;
          }
          pathSegmentStrings1.RemoveAt(index);
        }
        string str = string.Join("/", (IEnumerable<string>) pathSegmentStrings1);
        if (url.StartsWith("/"))
          str = "/" + str;
        else
          url.StartsWith("~/");
        if (url.EndsWith("/"))
          str += "/";
        url = str;
      }
      return url;
    }

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Uses the current UI culture as a target</remarks>
    public string ResolveUrl(string url) => this.ResolveUrl(url, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <param name="targetCulture">Culture for the resolved URL</param>
    /// <returns>Resolved URL for the specified culture</returns>
    public virtual string ResolveUrl(string url, CultureInfo targetCulture)
    {
      if (!this.includeSubfoderPrefixForDefaultLanguage && targetCulture.Equals((object) this.defaultCultures.UICulture) || RouteHelper.IsAbsoluteUrl(url) || string.IsNullOrEmpty(url))
        return url;
      int startIndex = url.IndexOf("/");
      string str;
      if (startIndex >= 0 && this.uiCultureSettings.TryGetValue(targetCulture.Name, out str))
        url = url.Insert(startIndex, "/" + str);
      return url;
    }

    /// <summary>
    /// Gets the include subfoder prefix for default language.
    /// </summary>
    /// <value>The include subfoder prefix for default language.</value>
    protected bool IncludeSubfoderPrefixForDefaultLanguage => this.includeSubfoderPrefixForDefaultLanguage;

    private class LanguageCulturesWrapper
    {
      private string key;
      private ILanguageCultures innerLanguageCultures;
      private Dictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper> children;

      public LanguageCulturesWrapper()
        : this((LanguageCultures) null)
      {
      }

      public LanguageCulturesWrapper(LanguageCultures innerLanguageCultures) => this.innerLanguageCultures = (ILanguageCultures) innerLanguageCultures;

      public ILanguageCultures Language
      {
        get => this.innerLanguageCultures;
        internal set => this.innerLanguageCultures = value;
      }

      public bool IsTransient => this.innerLanguageCultures == null;

      public bool TryGetChild(
        string key,
        out SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper language)
      {
        if (this.children != null)
          return this.children.TryGetValue(key, out language);
        language = (SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper) null;
        return false;
      }

      public Dictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper> Children
      {
        get
        {
          if (this.children == null)
            this.children = new Dictionary<string, SubFolderUrlLocalizationStrategy.LanguageCulturesWrapper>();
          return this.children;
        }
      }
    }
  }
}
