// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicensingSystemStatusRetriever
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Licensing
{
  internal class LicensingSystemStatusRetriever : ISystemStatusRetriever
  {
    private ICollection<string> loggedUrls;
    private const string ProtocolSeparator = "://";
    private const string Localhost = "localhost";
    private const string HostPattern = "(?<=://)(.*?)(?=/|$|:)";
    private const string NoHostPattern = "^(.*?)(?=/|$|:)";

    public IEnumerable<DashboardSystemStatus> GetSystemStatus()
    {
      IEnumerable<ISite> sites = SystemManager.CurrentContext.GetSites();
      Dictionary<string, ICollection<string>> invalidDomains = new Dictionary<string, ICollection<string>>();
      LicensingMessages res = Res.Get<LicensingMessages>();
      foreach (ISite site in sites)
        this.CheckSiteUrls(res, (IDictionary<string, ICollection<string>>) invalidDomains, site);
      List<DashboardSystemStatus> systemStatus = new List<DashboardSystemStatus>();
      if (invalidDomains.Count != 0)
      {
        DashboardSystemStatus status = this.CreateStatus(res, (IDictionary<string, ICollection<string>>) invalidDomains);
        systemStatus.Add(status);
      }
      return (IEnumerable<DashboardSystemStatus>) systemStatus;
    }

    private DashboardSystemStatus CreateStatus(
      LicensingMessages res,
      IDictionary<string, ICollection<string>> invalidDomains)
    {
      string description = this.GetDescription(res, invalidDomains);
      DashboardSystemStatus status = new DashboardSystemStatus(res.SystemErrorTitle, description);
      status.CanFindSolution = false;
      status.Links.Add(new DashboardSystemStatusLink()
      {
        Title = res.SystemErrorLinkTitle,
        Url = res.SystemErrorLinkUrl
      });
      return status;
    }

    private string GetDescription(
      LicensingMessages res,
      IDictionary<string, ICollection<string>> invalidDomains)
    {
      return res.SystemErrorDescription + this.GetSitesInfo(res, invalidDomains);
    }

    private string GetSitesInfo(
      LicensingMessages res,
      IDictionary<string, ICollection<string>> invalidDomains)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, ICollection<string>> invalidDomain in (IEnumerable<KeyValuePair<string, ICollection<string>>>) invalidDomains)
        stringBuilder.Append(this.GetSiteInfo(res, invalidDomain.Key, invalidDomain.Value));
      return stringBuilder.ToString();
    }

    private string GetSiteInfo(LicensingMessages res, string key, ICollection<string> value) => string.Format(res.SiteInfoTemplate, (object) key, (object) string.Join(", ", (IEnumerable<string>) value));

    private void CheckSiteUrls(
      LicensingMessages res,
      IDictionary<string, ICollection<string>> invalidDomains,
      ISite site)
    {
      this.AddIfInvalid(res, invalidDomains, site.Name, site.LiveUrl);
      this.AddIfInvalid(res, invalidDomains, site.Name, site.StagingUrl);
      if (site.DomainAliases == null)
        return;
      foreach (string domainAlias in (IEnumerable<string>) site.DomainAliases)
        this.AddIfInvalid(res, invalidDomains, site.Name, domainAlias);
    }

    private void AddIfInvalid(
      LicensingMessages res,
      IDictionary<string, ICollection<string>> dictionary,
      string siteName,
      string url)
    {
      if (string.IsNullOrEmpty(url) || this.IsUrlValid(url))
        return;
      this.TryAdd(dictionary, siteName, url);
      if (this.LoggedUrls.Contains(url))
        return;
      Log.Write((object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, res.SystemErrorLogMessage, (object) siteName, (object) url, (object) res.SystemErrorLinkUrl), ConfigurationPolicy.Trace);
      this.LoggedUrls.Add(url);
    }

    private bool IsUrlValid(string url)
    {
      url = !string.IsNullOrEmpty(url) ? this.ExtractHost(url) : throw new ArgumentException("url is null or empty");
      return this.IsDomainValid(url);
    }

    private bool IsDomainValid(string domain)
    {
      UriHostNameType hostNameType = !string.IsNullOrEmpty(domain) ? Uri.CheckHostName(domain) : throw new ArgumentNullException("domain is null or empty");
      return LicenseState.CheckCurrentDomain((IEnumerable<string>) LicenseState.Current.LicenseInfo.AllLicensedDomains.Select<string, string>((Func<string, string>) (d => this.ExtractHost(d))).ToList<string>(), LicenseState.Current.LicenseInfo.AllowSubDomains, domain, hostNameType);
    }

    private string ExtractHost(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path is null or empty");
      return path.Equals("localhost", StringComparison.InvariantCultureIgnoreCase) ? path : new Regex(!path.Contains("://") ? "^(.*?)(?=/|$|:)" : "(?<=://)(.*?)(?=/|$|:)").Match(path).Groups[1].Value;
    }

    private bool TryAdd(
      IDictionary<string, ICollection<string>> dictionary,
      string key,
      string value)
    {
      bool flag = false;
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, (ICollection<string>) new HashSet<string>());
      if (!dictionary[key].Contains(value))
      {
        dictionary[key].Add(value);
        flag = true;
      }
      return flag;
    }

    private ICollection<string> LoggedUrls
    {
      get
      {
        if (this.loggedUrls == null)
          this.loggedUrls = (ICollection<string>) new HashSet<string>();
        return this.loggedUrls;
      }
    }
  }
}
