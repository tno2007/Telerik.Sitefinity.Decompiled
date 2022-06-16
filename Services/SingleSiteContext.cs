// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SingleSiteContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Sitefinity context returned by the SystemManager.CurrentContext when the system is in single site mode (MultisiteModule is disabled)
  /// Left to save major refactoring of multisite module, used only during the system startup.
  /// </summary>
  internal class SingleSiteContext : SitefinityContextBase
  {
    private ISite currentSite;

    /// <inheritdoc />
    public override ISite CurrentSite
    {
      get
      {
        if (this.currentSite == null)
          this.currentSite = (ISite) new SingleSiteContext.SingleSiteProxy();
        return this.currentSite;
      }
    }

    internal override ISite DefaultSite => this.CurrentSite;

    /// <inheritdoc />
    public override void InvalidateCache() => this.currentSite = (ISite) null;

    /// <inheritdoc />
    internal override void ReloadCurrentContext() => this.currentSite = (ISite) new SingleSiteContext.SingleSiteProxy();

    internal class GlobalSystemCultureInfo
    {
      public GlobalSystemCultureInfo()
      {
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        string name = resourcesConfig.initialDefaultCulture.Name;
        this.DefaultCulture = name;
        this.DefaultCultureKey = name;
        this.SetPublicContentCultures((IList<string>) resourcesConfig.Cultures.Keys.ToList<string>(), resourcesConfig);
      }

      public string DefaultCulture { get; set; }

      public string DefaultCultureKey { get; set; }

      public CultureInfo[] PublicContentCultures { get; set; }

      public Dictionary<string, string> PublicCultures { get; set; }

      private void SetPublicContentCultures(
        IList<string> cultureKeys,
        ResourcesConfig resourcesConfig)
      {
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (string cultureKey in (IEnumerable<string>) cultureKeys)
        {
          CultureElement cultureElement;
          if (resourcesConfig.Cultures.TryGetValue(cultureKey, out cultureElement))
          {
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(cultureElement.UICulture);
            cultureInfoList.Add(cultureInfo);
            dictionary.Add(cultureElement.Key, cultureElement.UICulture);
          }
        }
        if (cultureInfoList.Count == 0)
        {
          cultureInfoList.Add(CultureInfo.GetCultureInfo(resourcesConfig.DefaultCulture.Culture));
          dictionary.Add(resourcesConfig.DefaultCulture.Key, resourcesConfig.DefaultCulture.Culture);
        }
        this.PublicContentCultures = cultureInfoList.ToArray();
        this.PublicCultures = dictionary;
      }
    }

    /// <inheritdoc />
    internal class SingleSiteProxy : ISite
    {
      private string siteMapName;
      private string siteMapKey;
      private Uri uri;
      private string siteUrl;
      private readonly ConcurrentProperty<SingleSiteContext.GlobalSystemCultureInfo> cultureInfo = new ConcurrentProperty<SingleSiteContext.GlobalSystemCultureInfo>((Func<SingleSiteContext.GlobalSystemCultureInfo>) (() => new SingleSiteContext.GlobalSystemCultureInfo()));

      public SingleSiteProxy() => CacheDependency.Subscribe(typeof (ResourcesConfig), new ChangedCallback(this.ConfigChangedCallback));

      /// <summary>
      /// Finalizes an instance of the <see cref="T:Telerik.Sitefinity.Services.SingleSiteContext.SingleSiteProxy" /> class.
      /// </summary>
      ~SingleSiteProxy() => CacheDependency.Unsubscribe(typeof (ResourcesConfig), new ChangedCallback(this.ConfigChangedCallback));

      private void ConfigChangedCallback(
        ICacheDependencyHandler caller,
        Type trackedItemType,
        string trackedItemKey)
      {
        if (!trackedItemType.Equals(typeof (ResourcesConfig)))
          return;
        this.cultureInfo.Reset();
      }

      /// <inheritdoc />
      public Guid Id => Config.Get<ProjectConfig>().DefaultSite.Id;

      /// <inheritdoc />
      public string Name => Config.Get<ProjectConfig>().DefaultSite.Name;

      /// <inheritdoc />
      public Guid HomePageId => Config.Get<ProjectConfig>().DefaultSite.HomePageId;

      /// <inheritdoc />
      public Guid FrontEndLoginPageId => Config.Get<ProjectConfig>().DefaultSite.FrontEndLoginPageId;

      /// <inheritdoc />
      public string FrontEndLoginPageUrl => Config.Get<ProjectConfig>().DefaultSite.FrontEndLoginPageUrl;

      /// <inheritdoc />
      public Guid SiteMapRootNodeId => Config.Get<ProjectConfig>().DefaultSite.SiteMapRootNodeId;

      /// <inheritdoc />
      public string StagingUrl
      {
        get
        {
          if (this.siteUrl == null)
            this.siteUrl = UrlPath.GetDomainUrl();
          return this.siteUrl;
        }
      }

      /// <inheritdoc />
      public string LiveUrl => this.StagingUrl;

      /// <inheritdoc />
      public IList<string> DomainAliases => (IList<string>) null;

      /// <inheritdoc />
      public IList<MultisiteContext.SiteDataSourceLinkProxy> SiteDataSourceLinks => (IList<MultisiteContext.SiteDataSourceLinkProxy>) null;

      /// <inheritdoc />
      public bool RequiresSsl { get; private set; }

      /// <inheritdoc />
      public string SiteMapName
      {
        get
        {
          if (this.siteMapName == null)
            this.siteMapName = PageManager.GetManager().GetPageNode(this.SiteMapRootNodeId).Name;
          return this.siteMapName;
        }
      }

      /// <inheritdoc />
      public string SiteMapKey
      {
        get
        {
          if (this.siteMapKey == null)
            this.siteMapKey = SiteMapBase.GetNodeKey(this.SiteMapName) + this.SiteMapRootNodeId.ToString().ToUpperInvariant();
          return this.siteMapKey;
        }
      }

      /// <inheritdoc />
      public bool IsLocatedInMainMenu => false;

      /// <inheritdoc />
      public Uri GetUri()
      {
        if (this.uri == (Uri) null)
        {
          try
          {
            SiteUrlSettings siteUrlSettings = Config.Get<SystemConfig>().SiteUrlSettings;
            string host = siteUrlSettings.Host;
            HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
            if (siteUrlSettings.EnableNonDefaultSiteUrlSettings && !string.IsNullOrWhiteSpace(host))
              this.uri = UrlPath.GetNonDefaultSiteUrl(host, (string) null);
            else if (!string.IsNullOrEmpty(host))
            {
              this.uri = new Uri(UrlPath.GetScheme() + "://" + host);
            }
            else
            {
              if (currentHttpContext == null)
                return new Uri(SystemManager.RootUrl);
              if (!currentHttpContext.Request.IsLocal)
              {
                if (!LicenseState.Current.LicenseInfo.SkipDomainValidation)
                {
                  if (!LicenseState.CheckDomainIsLicensed(currentHttpContext.Request.Url.Host, currentHttpContext.Request.Url.HostNameType))
                    goto label_12;
                }
                else
                  goto label_12;
              }
              this.uri = new Uri(currentHttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + currentHttpContext.Request.ApplicationPath);
            }
          }
          catch
          {
            this.uri = SystemManager.CurrentHttpContext == null ? new Uri(SystemManager.RootUrl) : SystemManager.CurrentHttpContext.Request.Url;
          }
        }
label_12:
        return this.uri;
      }

      /// <inheritdoc />
      public MultisiteContext.SiteDataSourceLinkProxy GetDefaultProvider(
        string dataSourceName)
      {
        return (MultisiteContext.SiteDataSourceLinkProxy) null;
      }

      /// <inheritdoc />
      public IEnumerable<MultisiteContext.SiteDataSourceLinkProxy> GetProviders(
        string dataSourceName)
      {
        return (IEnumerable<MultisiteContext.SiteDataSourceLinkProxy>) null;
      }

      /// <inheritdoc />
      public bool IsOffline => false;

      /// <inheritdoc />
      public ISiteOfflineInfo OfflineInfo => (ISiteOfflineInfo) null;

      /// <inheritdoc />
      [Obsolete("Use cultures.")]
      public CultureInfo[] PublicContentCultures => this.cultureInfo.Value.PublicContentCultures;

      /// <inheritdoc />
      public CultureInfo[] Cultures => this.cultureInfo.Value.PublicContentCultures;

      /// <inheritdoc />
      [Obsolete("Use cultures.")]
      public Dictionary<string, string> PublicCultures => this.cultureInfo.Value.PublicCultures;

      /// <inheritdoc />
      public CultureInfo DefaultCulture => CultureInfo.GetCultureInfo(this.cultureInfo.Value.DefaultCulture);

      [Obsolete("For internal use only. Use DefaultCulture.")]
      public string DefaultCultureKey => this.cultureInfo.Value.DefaultCultureKey;

      /// <inheritdoc />
      public bool IsDefault => true;

      /// <inheritdoc />
      public Guid DefaultFrontendTemplateId => Config.Get<PagesConfig>().DefaultFrontendTemplateId;

      /// <inheritdoc />
      public bool IsModuleAccessible(IModule module) => true;

      /// <inheritdoc />
      public void SetHomePage(Guid pageId, PageManager pageManager = null)
      {
        List<CacheDependencyKey> items = new List<CacheDependencyKey>();
        int num = ConfigProvider.DisableSecurityChecks ? 1 : 0;
        ConfigProvider.DisableSecurityChecks = true;
        ConfigManager manager = Config.GetManager();
        ProjectConfig section = manager.GetSection<ProjectConfig>();
        if (section.DefaultSite.HomePageId != pageId)
        {
          List<CacheDependencyKey> cacheDependencyKeys = items;
          List<Guid> pageIds = new List<Guid>();
          pageIds.Add(pageId);
          pageIds.Add(section.DefaultSite.HomePageId);
          PageManager pageManager1 = pageManager;
          MultisiteContext.SiteProxy.GetPagesCacheDependencies(cacheDependencyKeys, (IEnumerable<Guid>) pageIds, pageManager1);
          section.DefaultSite.HomePageId = pageId;
          manager.SaveSection((ConfigSection) section);
          SystemManager.CurrentContext.InvalidateCache();
          CacheDependency.Notify((IList<CacheDependencyKey>) items);
        }
        ConfigProvider.DisableSecurityChecks = num != 0;
      }
    }
  }
}
