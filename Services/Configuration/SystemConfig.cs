// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SystemConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage.SystemMessaging;
using Telerik.Sitefinity.BackgroundTasks.Configuration;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.GeoLocations.Configuration;
using Telerik.Sitefinity.Health;
using Telerik.Sitefinity.Health.Configuration;
using Telerik.Sitefinity.Licensing.Configuration;
using Telerik.Sitefinity.Lifecycle.Configuration;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.LoadBalancing.Replication;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Files.Configuration;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Config;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls;
using Telerik.Sitefinity.Modules.UserProfiles.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.ProtectionShield;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Security.Web.UI.Configuration;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Config;
using Telerik.Sitefinity.TrackingConsent.Configuration;
using Telerik.Sitefinity.UsageTracking.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Warmup;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UrlShorteners;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines system configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "SystemConfig")]
  public class SystemConfig : ConfigSection
  {
    private static IEnumerable<SitefinityServiceAttribute> sitefinityServiceAttributes;
    private static IEnumerable<SitefinityModuleAttribute> sitefinityModuleAttributes;
    internal const string RecycleBinModuleName = "RecycleBin";
    private const int SiteMapCacheManagerStartScavengingAfterItemCount = 10000;
    private const string NewsModuleTypeName = "Telerik.Sitefinity.Modules.News.NewsModule, Telerik.Sitefinity.ContentModules";
    private const string BlogsModuleTypeName = "Telerik.Sitefinity.Modules.Blogs.BlogsModule, Telerik.Sitefinity.ContentModules";
    private const string EventsModuleTypeName = "Telerik.Sitefinity.Modules.Events.EventsModule, Telerik.Sitefinity.ContentModules";
    private const string ListsModuleTypeName = "Telerik.Sitefinity.Modules.Lists.ListsModule, Telerik.Sitefinity.ContentModules";
    private const string CommentsModuleTypeName = "Telerik.Sitefinity.Modules.Comments.CommentsModule, Telerik.Sitefinity";
    private const string RecycleBinModuleTypeName = "Telerik.Sitefinity.RecycleBin.RecycleBinModule, Telerik.Sitefinity.RecycleBin";
    private const string FeatherModuleTypeName = "Telerik.Sitefinity.Frontend.FrontendModule, Telerik.Sitefinity.Frontend";

    [ConfigurationProperty("disableBackendUI", DefaultValue = false)]
    public bool DisableBackendUI
    {
      get => (bool) this["disableBackendUI"];
      set => this["disableBackendUI"] = (object) value;
    }

    [Browsable(false)]
    [ConfigurationProperty("build", DefaultValue = 0)]
    internal int Build
    {
      get => (int) this["build"];
      set => this["build"] = (object) value;
    }

    [Browsable(false)]
    [ConfigurationProperty("previousBuild", DefaultValue = 0)]
    internal int PreviousBuild
    {
      get => (int) this["previousBuild"];
      set => this["previousBuild"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of cache dependency handlers settings.
    /// </summary>
    /// <value>The cache dependency handlers settings.</value>
    [ConfigurationProperty("cacheDependencyHandlers")]
    [DescriptionResource(typeof (ConfigDescriptions), "CacheDependencyHandlers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> CacheDependencyHandlers => (ConfigElementDictionary<string, DataProviderSettings>) this["cacheDependencyHandlers"];

    /// <summary>
    /// Gets a collection of Sitefinity system services settings.
    /// System services are process that run in the background and do not have specific user interface
    /// such as schedule timers, indexers and etc.
    /// </summary>
    [ConfigurationProperty("systemServices")]
    [DescriptionResource(typeof (ConfigDescriptions), "SystemServices")]
    public virtual ConfigElementDictionary<string, ModuleSettings> SystemServices => (ConfigElementDictionary<string, ModuleSettings>) this["systemServices"];

    /// <summary>
    /// Gets a collection of Sitefinity service modules settings.
    /// Service modules are logically self contained, pluggable applications that serve other modules.
    /// </summary>
    [ConfigurationProperty("servicesModules")]
    [DescriptionResource(typeof (ConfigDescriptions), "ServicesModules")]
    [Browsable(false)]
    [Obsolete("Use ApplicationModules or SystemServices to register a module or service")]
    public virtual ConfigElementDictionary<string, ServiceModuleSettings> ServicesModules => (ConfigElementDictionary<string, ServiceModuleSettings>) this["servicesModules"];

    /// <summary>
    /// Gets a collection of Sitefinity control designer settings.
    /// This maps specific controls to control designer classes.
    /// </summary>
    [ConfigurationProperty("controlDesigners")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlDesignersDescription", Title = "ControlDesignersTitle")]
    public virtual ConfigElementDictionary<string, ControlDesignerSettings> ControlDesigners => (ConfigElementDictionary<string, ControlDesignerSettings>) this["controlDesigners"];

    /// <summary>
    /// Gets a collection of Sitefinity service application modules settings.
    /// Application modules are logically self contained, pluggable applications.
    /// </summary>
    [ConfigurationProperty("applicationModules")]
    [DescriptionResource(typeof (ConfigDescriptions), "ApplicationModules")]
    public virtual ConfigElementDictionary<string, AppModuleSettings> ApplicationModules => (ConfigElementDictionary<string, AppModuleSettings>) this["applicationModules"];

    /// <summary>
    /// Gets or sets the SMTP client settings used by Sitefinity for sending emails (e.g. recovering passwords)
    /// </summary>
    [ConfigurationProperty("smtpSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpSettings")]
    [Obsolete("Use the NotificationsConfig to create a profile with the SMTP client settings")]
    [Browsable(false)]
    public virtual SmtpElement SmtpSettings
    {
      get => (SmtpElement) this["smtpSettings"];
      set => this["smtpSettings"] = (object) value;
    }

    [ConfigurationProperty("licensing")]
    [DescriptionResource(typeof (ConfigDescriptions), "Licensing")]
    [Browsable(false)]
    public virtual LicenseElement Licensing
    {
      get => (LicenseElement) this["licensing"];
      set => this["licensing"] = (object) value;
    }

    /// <summary>Gets or sets the UI time zone settings.</summary>
    /// <value>The UI time zone settings.</value>
    [ConfigurationProperty("uiTimeZoneSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "UITimeZoneConfigDescriptions")]
    public virtual TimeZoneUISettings UITimeZoneSettings
    {
      get => (TimeZoneUISettings) this["uiTimeZoneSettings"];
      set => this["uiTimeZoneSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the Elmah logging configuration settings.
    /// </summary>
    /// <value>The Elmah logging configuration settings.</value>
    [ConfigurationProperty("uiElmahSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "UIElmahConfigDescriptions")]
    public virtual ElmahSettings UIElmahSettings
    {
      get => (ElmahSettings) this["uiElmahSettings"];
      set => this["uiElmahSettings"] = (object) value;
    }

    [ConfigurationProperty("servicesPaths")]
    public virtual ServiciesPaths ServicesPaths
    {
      get => (ServiciesPaths) this["servicesPaths"];
      set => this["servicesPaths"] = (object) value;
    }

    /// <summary>Gets or sets the files module config.</summary>
    /// <value>The files module config.</value>
    [ConfigurationProperty("filesModuleConfig")]
    public virtual FilesModuleConfig FilesModuleConfig
    {
      get => (FilesModuleConfig) this["filesModuleConfig"];
      set => this["filesModuleConfig"] = (object) value;
    }

    /// <summary>Gets or sets the URL Shortener settings.</summary>
    /// <value>The URL Shortener settings.</value>
    [ConfigurationProperty("urlShortenerSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "UrlShortenerConfigDescriptions")]
    public virtual ConfigElementDictionary<string, UrlShortenerElement> UrlShortenerSettings
    {
      get => (ConfigElementDictionary<string, UrlShortenerElement>) this["urlShortenerSettings"];
      set => this["urlShortenerSettings"] = (object) value;
    }

    /// <summary>Gets or sets the load balancing settings.</summary>
    /// <value>The load balancing settings.</value>
    [ConfigurationProperty("loadBalancingConfig")]
    public virtual LoadBalancingConfig LoadBalancingConfig
    {
      get => (LoadBalancingConfig) this["loadBalancingConfig"];
      set => this["loadBalancingConfig"] = (object) value;
    }

    /// <summary>
    /// Maps a, possibly abstract, type or interface to types that inherit/implement it. This setting is used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.
    /// </summary>
    [ConfigurationProperty("typeImplementationsMapping")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TypeImplementationsMappingDescription", Title = "TypeImplementationsMappingTitle")]
    public virtual ConfigElementDictionary<Type, TypeImplementationsMappingElement> TypeImplementationsMapping => (ConfigElementDictionary<Type, TypeImplementationsMappingElement>) this["typeImplementationsMapping"];

    /// <summary>Gets or sets the site URL settings.</summary>
    /// <value>The site URL settings.</value>
    [ConfigurationProperty("siteUrlSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "SiteUrlSettingsConfigDescriptions")]
    public virtual SiteUrlSettings SiteUrlSettings
    {
      get => (SiteUrlSettings) this["siteUrlSettings"];
      set => this["siteUrlSettings"] = (object) value;
    }

    /// <summary>Gets or sets the output cache settings.</summary>
    [ConfigurationProperty("outputCacheSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheElementDescription", Title = "OutputCacheElementTitle")]
    public OutputCacheElement CacheSettings
    {
      get => (OutputCacheElement) this["outputCacheSettings"];
      set => this["outputCacheSettings"] = (object) value;
    }

    [ConfigurationProperty("cacheManagers")]
    public virtual ConfigElementDictionary<string, CacheManagerElement> CacheManagers => (ConfigElementDictionary<string, CacheManagerElement>) this["cacheManagers"];

    /// <summary>
    /// Gets or sets the background tasks settings used by Sitefinity for executing tasks asynchronously in parallel.
    /// </summary>
    [ConfigurationProperty("backgroundTasks")]
    [DescriptionResource(typeof (ConfigDescriptions), "BackgroundTasksConfigDescription")]
    public virtual BackgroundTasksElement BackgroundTasks
    {
      get => (BackgroundTasksElement) this["backgroundTasks"];
      set => this["backgroundTasks"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the configuration for the content links cache.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentLinksCacheConfigDescription", Title = "ContentLinksCacheConfigTitle")]
    [ConfigurationProperty("contentLinksCache")]
    public virtual CacheProfileElement ContentLinksCache
    {
      get => (CacheProfileElement) this["contentLinksCache"];
      set => this["contentLinksCache"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the configuration for the content links cache.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "GeoLocationSettingsElementDescription", Title = "GeoLocationSettingsElementTitle")]
    [ConfigurationProperty("geoLocationSettings")]
    public virtual GeoLocationSettingsElement GeoLocationSettings
    {
      get => (GeoLocationSettingsElement) this["geoLocationSettings"];
      set => this["geoLocationSettings"] = (object) value;
    }

    /// <summary>Gets or sets the content locations settings.</summary>
    /// <value>The content locations settings.</value>
    [ConfigurationProperty("contentLocationsSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentLocationsSettingsDescription", Title = "ContentLocationsSettingsTitle")]
    public virtual ContentLocationsSettingsElement ContentLocationsSettings
    {
      get => (ContentLocationsSettingsElement) this["contentLocationsSettings"];
      set => this["contentLocationsSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the SMTP client settings used by Sitefinity for sending emails (e.g. recovering passwords)
    /// </summary>
    [ConfigurationProperty("sslOffloadingSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "SslOffloadingSettings")]
    public virtual SslOffloadingElement SslOffloadingSettings
    {
      get => (SslOffloadingElement) this["sslOffloadingSettings"];
      set => this["sslOffloadingSettings"] = (object) value;
    }

    /// <summary>Gets or sets the health checks settings.</summary>
    /// <value>The health settings.</value>
    [ConfigurationProperty("healthCheckConfig")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckDescription", Title = "HealthCheckCaption")]
    public virtual HealthCheckConfig HealthCheckConfig
    {
      get => (HealthCheckConfig) this["healthCheckConfig"];
      set => this["healthCheckConfig"] = (object) value;
    }

    [ConfigurationProperty("trackingConsentConfig")]
    [DescriptionResource(typeof (ConfigDescriptions), "TrackingConsentConfigDescriptions")]
    public virtual TrackingConsentConfig TrackingConsentConfig
    {
      get => (TrackingConsentConfig) this["trackingConsentConfig"];
      set => this["trackingConsentConfig"] = (object) value;
    }

    /// <summary>Gets or sets the SEO and OpenGraph settings.</summary>
    /// <value>The SEO and OpenGraph settings.</value>
    [ConfigurationProperty("seoAndOpenGraphConfig")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "SeoAndOpenGraphPropertiesCaption")]
    public virtual SeoAndOpenGraphElement SeoAndOpenGraphConfig
    {
      get => (SeoAndOpenGraphElement) this["seoAndOpenGraphConfig"];
      set => this["seoAndOpenGraphConfig"] = (object) value;
    }

    [ConfigurationProperty("lifecycleConfig")]
    [DescriptionResource(typeof (ConfigDescriptions), "LifecycleConfig")]
    public virtual LifecycleElement LifecycleConfig
    {
      get => (LifecycleElement) this["lifecycleConfig"];
      set => this["lifecycleConfig"] = (object) value;
    }

    [ConfigurationProperty("usageTracking")]
    [DescriptionResource(typeof (ConfigDescriptions), "UsageTrackingConfig")]
    public virtual UsageTrackingElement UsageTracking
    {
      get => (UsageTrackingElement) this["usageTracking"];
      set => this["usageTracking"] = (object) value;
    }

    /// <summary>
    /// Adds implementation types for the <paramref name="type" /> type.
    /// </summary>
    /// <param name="type">Base type/interface.</param>
    /// <param name="implementationTypes">Type impelemntations.</param>
    internal void AddTypeImplementations(Type type, params Type[] implementationTypes)
    {
      TypeImplementationsMappingElement implementationsMappingElement;
      if (!this.TypeImplementationsMapping.TryGetValue(type, out implementationsMappingElement))
      {
        implementationsMappingElement = new TypeImplementationsMappingElement((ConfigElement) this.TypeImplementationsMapping, type);
        this.TypeImplementationsMapping.Add(type, implementationsMappingElement);
      }
      foreach (Type implementationType in implementationTypes)
      {
        Type implType = implementationType;
        if (!implementationsMappingElement.Any<Type>((Func<Type, bool>) (t => t == implType)))
        {
          if (!type.IsAssignableFrom(implType))
            throw new ArgumentException(string.Format("Type {0} is not assignable to type {1} and thus is invalid as its implementation.", (object) implType.FullName, (object) type.FullName));
          implementationsMappingElement.Add(implType);
        }
      }
    }

    internal void RemoveTypeImplementations(Type type)
    {
      if (!this.TypeImplementationsMapping.ContainsKey(type))
        return;
      this.TypeImplementationsMapping.Remove(type);
    }

    /// <summary>
    /// Gets all registerted type implementations for the given <paramref name="type" />.
    /// </summary>
    /// <param name="type">The type, which registered implementations are requested.</param>
    public IEnumerable<Type> GetTypeImplementations(Type type)
    {
      TypeImplementationsMappingElement implementationsMappingElement;
      return this.TypeImplementationsMapping.TryGetValue(type, out implementationsMappingElement) ? implementationsMappingElement.Implementations.Select<TypeImplementationsElement, Type>((Func<TypeImplementationsElement, Type>) (i => i.Type)) : (IEnumerable<Type>) null;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      this.InitializeCacheDependencyHandlers();
      this.InitializeServices();
      this.InitializeApplicationModules();
      this.InitializeLoadBalancingConfiguration();
      this.InitializeControlDesigners();
      this.InitializeTypeImplementationsMapping();
      this.InitializeCacheManagers();
      this.IntializeUrlShortServiceConfig();
    }

    private void IntializeUrlShortServiceConfig()
    {
      UrlShortenerElement element = new UrlShortenerElement(false)
      {
        ProviderType = typeof (BitLyUrlShortener)
      };
      element.ProviderTypeName = element.ProviderType.FullName;
      element.ShortenerServiceUrl = "http://api.bit.ly/v3/shorten?login={0}&apiKey={1}&longUrl={2}&format=xml";
      element.Parameters.Add("login", "Sitefinity");
      element.Parameters.Add("apiKey", "R_2ab6988bf5092a5848e4154455c1a5ab");
      element.Parameters.Add("providerName", "bit.ly");
      this.UrlShortenerSettings.Add(element);
    }

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion == (Version) null && this.ApplicationModules.ContainsKey("Classifications"))
        this.ApplicationModules.Remove("Classifications");
      AppModuleSettings appModuleSettings1;
      if (oldVersion < new Version(5, 3) && this.ApplicationModules.TryGetValue("Synchronization", out appModuleSettings1))
        appModuleSettings1.StartupType = StartupType.OnApplicationStart;
      if (oldVersion < new Version(5, 0))
        this.TransformSiteUrlToHostAndPortConfiguration();
      if (oldVersion.Build < 3040)
      {
        if (this.ApplicationModules.ContainsKey("DynamicModule"))
          this.ApplicationModules.Remove("DynamicModule");
        if (this.ApplicationModules.ContainsKey("SiteSync"))
        {
          AppModuleSettings appModuleSettings2;
          if (this.ApplicationModules.TryGetValue("Synchronization", out appModuleSettings2))
          {
            AppModuleSettings applicationModule = this.ApplicationModules["SiteSync"];
            appModuleSettings2.Version = applicationModule.Version;
          }
          this.ApplicationModules.Remove("SiteSync");
        }
        if (this.ServicesModules.ContainsKey("Search"))
        {
          ServiceModuleSettings servicesModule = this.ServicesModules["Search"];
          this.ServicesModules.Remove("Search");
          this.ApplicationModules["Search"].Version = servicesModule.Version;
        }
        AppModuleSettings appModuleSettings3;
        if (this.ApplicationModules.TryGetValue("Catalog", out appModuleSettings3) && appModuleSettings3.Type.IsNullOrEmpty())
        {
          this.ApplicationModules["Ecommerce"].Version = appModuleSettings3.Version;
          this.ApplicationModules.Remove("Catalog");
          if (this.ApplicationModules.ContainsKey("Orders"))
            this.ApplicationModules.Remove("Orders");
          if (this.ApplicationModules.ContainsKey("Shipping"))
            this.ApplicationModules.Remove("Shipping");
        }
        foreach (AppModuleSettings appModuleSettings4 in this.ApplicationModules.Values.Where<AppModuleSettings>((Func<AppModuleSettings, bool>) (m => !m.Type.IsNullOrEmpty())))
        {
          if (appModuleSettings4.Type.StartsWith("Telerik.Sitefinity.Modules.News.NewsModule, Telerik.Sitefinity"))
            appModuleSettings4.Type = "Telerik.Sitefinity.Modules.News.NewsModule, Telerik.Sitefinity.ContentModules";
          if (appModuleSettings4.Type.StartsWith("Telerik.Sitefinity.Modules.Blogs.BlogsModule, Telerik.Sitefinity"))
            appModuleSettings4.Type = "Telerik.Sitefinity.Modules.Blogs.BlogsModule, Telerik.Sitefinity.ContentModules";
          if (appModuleSettings4.Type.StartsWith("Telerik.Sitefinity.Modules.Events.EventsModule, Telerik.Sitefinity"))
            appModuleSettings4.Type = "Telerik.Sitefinity.Modules.Events.EventsModule, Telerik.Sitefinity.ContentModules";
          if (appModuleSettings4.Type.StartsWith("Telerik.Sitefinity.Modules.Libraries.ListsModule, Telerik.Sitefinity"))
            appModuleSettings4.Type = "Telerik.Sitefinity.Modules.Lists.ListsModule, Telerik.Sitefinity.ContentModules";
        }
      }
      if (oldVersion.Build < 4100)
        this.ContentLocationsSettings.DisableCanonicalUrls = true;
      else if (oldVersion.Build < SitefinityVersion.Sitefinity6_1.Build)
      {
        this.ContentLocationsSettings.DisableCanonicalUrls = this.ContentLocationsSettings.DisableCanonicalUrlsObsolete;
        this.ContentLocationsSettings.EnableSingleItemModeWidgetsBackwardCompatibilityMode = true;
      }
      if (oldVersion.Build < SitefinityVersion.Sitefinity9_0.Build)
      {
        AppModuleSettings appModuleSettings5 = this.ApplicationModules.Values.FirstOrDefault<AppModuleSettings>((Func<AppModuleSettings, bool>) (ams => ams.Type == "Telerik.Sitefinity.Frontend.FrontendModule, Telerik.Sitefinity.Frontend"));
        if (appModuleSettings5 != null)
          appModuleSettings5.StartupType = StartupType.OnApplicationStart;
      }
      if (oldVersion.Build < SitefinityVersion.Sitefinity10_0.Build)
      {
        OutputCacheElement cacheSettings = this.CacheSettings;
        foreach (OutputCacheProfileElement cacheProfileElement in (IEnumerable<OutputCacheProfileElement>) cacheSettings.Profiles.Values)
        {
          if (!(cacheProfileElement.Name != "Any Location"))
          {
            if (cacheProfileElement.Name == this.CacheSettings.DefaultProfile && cacheProfileElement.WaitForPageOutputCacheToFill)
              cacheSettings.WaitForPageOutputCacheToFill = true;
            if (cacheProfileElement.Parameters["varyByUserAgent"] == null)
              cacheProfileElement.Parameters["varyByUserAgent"] = true.ToString();
          }
        }
        foreach (ClientCacheProfileElement cacheProfileElement1 in (IEnumerable<ClientCacheProfileElement>) cacheSettings.ClientProfiles.Values)
        {
          OutputCacheProfileElement element;
          if (!cacheSettings.MediaCacheProfiles.TryGetValue(cacheProfileElement1.Name, out element))
          {
            element = new OutputCacheProfileElement((ConfigElement) cacheSettings.MediaCacheProfiles)
            {
              Name = cacheProfileElement1.Name,
              Location = OutputCacheLocation.Any
            };
            cacheSettings.MediaCacheProfiles.Add(element);
          }
          OutputCacheProfileElement cacheProfileElement2;
          if (cacheSettings.Profiles.TryGetValue(cacheProfileElement1.Name, out cacheProfileElement2))
          {
            element.Duration = cacheProfileElement2.Duration;
            element.MaxSize = cacheProfileElement2.MaxSize;
          }
          element.ClientMaxAge = new int?(cacheProfileElement1.Duration);
        }
        cacheSettings.DefaultVideoProfile = cacheSettings.DefaultImageProfile;
        cacheSettings.DefaultDocumentProfile = cacheSettings.DefaultImageProfile;
      }
      if (oldVersion < SitefinityVersion.Sitefinity10_2)
      {
        if (this.SeoAndOpenGraphConfig.EnabledSEO)
          this.SeoAndOpenGraphConfig.EnabledSEO = false;
        if (this.SeoAndOpenGraphConfig.EnabledOpenGraph)
          this.SeoAndOpenGraphConfig.EnabledOpenGraph = false;
      }
      if (oldVersion.Build < SitefinityVersion.Sitefinity11_0.Build)
        this.DisableSecurityModule();
      if (oldVersion.Build < SitefinityVersion.Sitefinity12_0.Build)
        this.ConfigureDefaultCacheVaryBy();
      if (oldVersion.Build < SitefinityVersion.Sitefinity13_0.Build)
        this.UpgradeModuleStartupType("Ecommerce", StartupType.OnApplicationStart, StartupType.Disabled);
      foreach (AppModuleSettings appModuleSettings6 in this.ApplicationModules.Values.Where<AppModuleSettings>((Func<AppModuleSettings, bool>) (m => m.Type.IsNullOrEmpty())).ToArray<AppModuleSettings>())
        this.ApplicationModules.Remove(appModuleSettings6);
    }

    internal override void InitUpgradeContext(
      Version oldVersion,
      ConfigUpgradeContext upgradeContext)
    {
      base.InitUpgradeContext(oldVersion, upgradeContext);
      if (oldVersion.Build >= SitefinityVersion.Sitefinity9_1.Build)
        return;
      this.PrepareUpgradeTo91(upgradeContext);
    }

    private void UpgradeModuleStartupType(
      string moduleName,
      StartupType oldValue,
      StartupType newValue)
    {
      AppModuleSettings appModuleSettings;
      ConfigProperty prop;
      PersistedValueWrapper valueWrapper;
      if (!this.ApplicationModules.TryGetValue(moduleName, out appModuleSettings) || !appModuleSettings.Properties.TryGetValue("startupType", out prop) || (StartupType) appModuleSettings.GetRawValue(prop, out valueWrapper) != newValue || valueWrapper != null && valueWrapper.Source != ConfigSource.Default)
        return;
      appModuleSettings.StartupType = oldValue;
    }

    private void PrepareUpgradeTo91(ConfigUpgradeContext upgradeContext) => upgradeContext.AddElementLoadHandler((Action<ConfigUpgradeContext, ConfigElement>) ((context, element) =>
    {
      if (!(element is TypeImplementationsElement))
        return;
      TypeImplementationsElement implementationsElement = element as TypeImplementationsElement;
      try
      {
        implementationsElement.GetKey();
      }
      catch (ArgumentException ex)
      {
        ConfigProperty prop = implementationsElement.Properties.Single<ConfigProperty>((Func<ConfigProperty, bool>) (p => p.Name == "type"));
        string rawValue = implementationsElement.GetRawValue(prop) as string;
        int length = rawValue.IndexOf(',');
        if (length == -1)
          return;
        Type type = TypeResolutionService.ResolveType(rawValue.Substring(0, length), true);
        implementationsElement.Type = type;
      }
    }));

    private void TransformSiteUrlToHostAndPortConfiguration()
    {
      try
      {
        if (!this.SiteUrlSettings.EnableNonDefaultSiteUrlSettings || string.IsNullOrEmpty(this.SiteUrlSettings.Host))
          return;
        Uri uri = new Uri(UrlPath.GetAbsoluteHost(this.SiteUrlSettings.Host));
        if (!string.IsNullOrEmpty(uri.Host))
          this.SiteUrlSettings.Host = uri.Host;
        int port;
        if (uri.Scheme == "http" && uri.Port != 80)
        {
          SiteUrlSettings siteUrlSettings = this.SiteUrlSettings;
          port = uri.Port;
          string str = port.ToString();
          siteUrlSettings.NonDefaultHttpPort = str;
        }
        if (!(uri.Scheme == "https") || uri.Port == 443)
          return;
        SiteUrlSettings siteUrlSettings1 = this.SiteUrlSettings;
        port = uri.Port;
        string str1 = port.ToString();
        siteUrlSettings1.NonDefaultHttpsPort = str1;
      }
      catch (Exception ex)
      {
        Log.Write((object) "Failed: transform old setting Site Url to Host and Port at System > Settings > Advanced> Site URL Settings configuration.", ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private void InitializeLoadBalancingConfiguration()
    {
      LoadBalancingConfig loadBalancingConfig = this.LoadBalancingConfig;
      loadBalancingConfig.AddHandler<InvalidateCacheHandler>();
      loadBalancingConfig.AddHandler<InvalidateLicenseHandler>();
      loadBalancingConfig.AddHandler<SchedulingHandler>();
      loadBalancingConfig.AddHandler<ResetApplicationHandler>();
      loadBalancingConfig.AddHandler<InvalidateSitemapCacheHandler>();
      loadBalancingConfig.AddHandler<InvalidateDynamicModuleCacheHandler>();
      loadBalancingConfig.AddHandler<InvalidateOpenAccessL2CacheHandler>();
      loadBalancingConfig.AddHandler<NlbCheckHandler>();
      loadBalancingConfig.AddHandler<RedisCheckHandler>();
      loadBalancingConfig.AddHandler<SystemMessageHandler>();
      loadBalancingConfig.AddHandler<RescheduleNextRunMessageHandler>();
      loadBalancingConfig.AddSender<WebServiceSystemMessageSender>();
      loadBalancingConfig.AddSender<AzureSystemMessageSender>();
      loadBalancingConfig.AddSender<MsmqSystemMessageSender>();
      loadBalancingConfig.AddSender<RedisSystemMessageSender>();
      loadBalancingConfig.ReplicationSyncSettings.AddTransporter<RedisMessageTransporter>(new NameValueCollection()
      {
        {
          "ConnectionString",
          string.Empty
        },
        {
          "Prefix",
          "sf-"
        }
      });
    }

    private void InitializeServices()
    {
      ConfigElementDictionary<string, ModuleSettings> systemServices = this.SystemServices;
      systemServices.Add(new ModuleSettings((ConfigElement) systemServices)
      {
        Name = "Notifications",
        Title = "Notifications",
        Type = "Telerik.Sitefinity.Services.Notifications.NotificationService, Telerik.Sitefinity.Services.Notifications.Impl",
        StartupType = StartupType.OnApplicationStart
      });
      systemServices.Add(new ModuleSettings((ConfigElement) systemServices)
      {
        Name = "Documents",
        Title = "Documents",
        Type = "Telerik.Sitefinity.Services.Documents.DocumentService, Telerik.Sitefinity.Services.Documents.Impl",
        StartupType = StartupType.OnApplicationStart
      });
      systemServices.Add(new ModuleSettings((ConfigElement) systemServices)
      {
        Name = "Statistics",
        Title = "Statistics",
        Type = "Telerik.Sitefinity.Services.Statistics.StatisticsService, Telerik.Sitefinity.Services.Statistics.Impl",
        StartupType = StartupType.OnApplicationStart
      });
      systemServices.Add(new ModuleSettings((ConfigElement) systemServices)
      {
        Name = "Comments",
        Title = "Comments",
        Type = "Telerik.Sitefinity.Services.Comments.Impl.CommentService, Telerik.Sitefinity.Services.Comments.Impl",
        StartupType = StartupType.OnApplicationStart
      });
      foreach (SitefinityServiceAttribute serviceAttribute in SystemConfig.GetSitefinityServiceAttributes())
        this.SystemServices.Add(new ModuleSettings((ConfigElement) this.SystemServices)
        {
          Name = serviceAttribute.Name,
          Type = serviceAttribute.Type.AssemblyQualifiedName,
          Title = serviceAttribute.Title,
          Description = serviceAttribute.Description,
          StartupType = serviceAttribute.StartupType
        });
    }

    private void InitializeApplicationModules()
    {
      ConfigElementDictionary<string, AppModuleSettings> applicationModules1 = this.ApplicationModules;
      AppModuleSettings element1 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element1.Name = "Multisite";
      element1.Title = "Multisite_Obsolete";
      element1.Type = typeof (MultisiteModule_Obsolete).FullName;
      element1.StartupType = StartupType.OnApplicationStart;
      element1.Hidden = true;
      applicationModules1.Add(element1);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules2 = this.ApplicationModules;
      AppModuleSettings element2 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element2.Name = "MultisiteInternal";
      element2.Title = "Multisite";
      element2.Description = "Manage multiple sites through one Sitefinity instance. Share content and templates  across the sites.";
      element2.Type = typeof (MultisiteModule).FullName;
      element2.StartupType = StartupType.OnApplicationStart;
      element2.Hidden = true;
      applicationModules2.Add(element2);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules3 = this.ApplicationModules;
      AppModuleSettings element3 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element3.Name = SchedulingModule.ModuleName;
      element3.Title = "Scheduling";
      element3.Description = "Provides functionality for scheduled tasks";
      element3.Type = typeof (SchedulingModule).FullName;
      element3.StartupType = StartupType.OnApplicationStart;
      applicationModules3.Add(element3);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules4 = this.ApplicationModules;
      AppModuleSettings element4 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element4.Name = "News";
      element4.Title = "News";
      element4.Description = "Create, modify and publish all kinds of news";
      element4.Type = "Telerik.Sitefinity.Modules.News.NewsModule, Telerik.Sitefinity.ContentModules";
      element4.StartupType = StartupType.OnApplicationStart;
      applicationModules4.Add(element4);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules5 = this.ApplicationModules;
      AppModuleSettings element5 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element5.Name = "Blogs";
      element5.Title = "Blogs";
      element5.Description = "Blogs module allows you to create blogs and publish blog posts";
      element5.Type = "Telerik.Sitefinity.Modules.Blogs.BlogsModule, Telerik.Sitefinity.ContentModules";
      element5.StartupType = StartupType.OnApplicationStart;
      applicationModules5.Add(element5);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules6 = this.ApplicationModules;
      AppModuleSettings element6 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element6.Name = "Events";
      element6.Title = "Events";
      element6.Description = "Create, manage and organize different types of events";
      element6.Type = "Telerik.Sitefinity.Modules.Events.EventsModule, Telerik.Sitefinity.ContentModules";
      element6.StartupType = StartupType.OnApplicationStart;
      applicationModules6.Add(element6);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules7 = this.ApplicationModules;
      AppModuleSettings element7 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element7.Name = "Libraries";
      element7.Title = "Libraries";
      element7.Description = "Use Libraries to store, manage and organize images, videos and other documents";
      element7.Type = typeof (LibrariesModule).FullName;
      element7.StartupType = StartupType.OnApplicationStart;
      applicationModules7.Add(element7);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules8 = this.ApplicationModules;
      AppModuleSettings element8 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element8.ModuleId = new Guid("A64410F7-2F1E-4068-81D0-E28D864DE323");
      element8.Name = "Forms";
      element8.Title = "Forms";
      element8.Description = "Design and manage different types of forms (feedback forms, application forms, queries etc.)";
      element8.Type = typeof (FormsModule).FullName;
      element8.StartupType = StartupType.OnApplicationStart;
      applicationModules8.Add(element8);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules9 = this.ApplicationModules;
      AppModuleSettings element9 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element9.Name = "Lists";
      element9.Title = "Lists";
      element9.Description = "Organize information using lists (FAQ, list of contacts etc.)";
      element9.Type = "Telerik.Sitefinity.Modules.Lists.ListsModule, Telerik.Sitefinity.ContentModules";
      element9.StartupType = StartupType.OnApplicationStart;
      applicationModules9.Add(element9);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules10 = this.ApplicationModules;
      AppModuleSettings element10 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element10.Name = "Authentication";
      element10.Title = "Authentication";
      element10.Description = "Authentication module is responsible for Sitefinity authentication and authorization";
      element10.Type = "Telerik.Sitefinity.Authentication.AuthenticationModule, Telerik.Sitefinity.Authentication";
      element10.StartupType = StartupType.OnApplicationStart;
      applicationModules10.Add(element10);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules11 = this.ApplicationModules;
      AppModuleSettings element11 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element11.ModuleId = new Guid("3D8A2051-6F6F-437C-865E-B3177689AC12");
      element11.Name = "Newsletters";
      element11.Title = "Email Campaigns";
      element11.Description = "Prepare and send marketing campaigns and newsletters, manage email subscribers, perform A/B tests";
      element11.Type = typeof (NewslettersModule).FullName;
      element11.StartupType = StartupType.OnApplicationStart;
      applicationModules11.Add(element11);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules12 = this.ApplicationModules;
      AppModuleSettings element12 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element12.Name = "GenericContent";
      element12.Title = "Content blocks";
      element12.Description = "Use content blocks to display various kinds of nonspecific information (different than the predefined content types)";
      element12.Type = typeof (ContentModule).FullName;
      element12.StartupType = StartupType.OnApplicationStart;
      applicationModules12.Add(element12);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules13 = this.ApplicationModules;
      AppModuleSettings element13 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element13.Name = "ControlTemplates";
      element13.Title = "Widget templates";
      element13.Description = "With widget templates you can change the look of the build-in Sitefinity widgets – Blog posts, News, Events, Generic content, Images, Videos, etc.";
      element13.Type = typeof (ControlTemplatesModule).AssemblyQualifiedName;
      element13.StartupType = StartupType.OnApplicationStart;
      applicationModules13.Add(element13);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules14 = this.ApplicationModules;
      AppModuleSettings element14 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element14.Name = "ModuleBuilder";
      element14.Title = "Module builder";
      element14.Description = "Module builder allows you to create and manage custom content types";
      element14.Type = typeof (ModuleBuilderModule).FullName;
      element14.StartupType = StartupType.OnApplicationStart;
      applicationModules14.Add(element14);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules15 = this.ApplicationModules;
      AppModuleSettings element15 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element15.ModuleId = new Guid("01F89003-7A52-4C08-BA60-45C8B8824B38");
      element15.Name = "ResponsiveDesign";
      element15.Title = "Responsive & Mobile design";
      element15.Description = "Responsive & Mobile design module allows you to create mobile friendly version of a website";
      element15.Type = typeof (ResponsiveDesignModule).FullName;
      element15.StartupType = StartupType.OnApplicationStart;
      applicationModules15.Add(element15);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules16 = this.ApplicationModules;
      AppModuleSettings element16 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element16.Name = "Publishing";
      element16.Title = "Feeds & Notifications";
      element16.Description = "Use Feeds & Notifications to publish frequently updated information. You can publish an RSS feed for a blog, news, or events on the website and the users can subscribe to this feed";
      element16.Type = typeof (PublishingModule).FullName;
      element16.StartupType = StartupType.OnApplicationStart;
      applicationModules16.Add(element16);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules17 = this.ApplicationModules;
      AppModuleSettings element17 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element17.Name = "Search";
      element17.Title = "Search and Indexes";
      element17.Description = "Define different sets of content to be searched, using the internal search engine of your website (requires Feeds & Notifications module to be active)";
      element17.Type = "Telerik.Sitefinity.Services.Search.SearchModule, Telerik.Sitefinity.Search.Impl";
      element17.StartupType = StartupType.OnApplicationStart;
      applicationModules17.Add(element17);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules18 = this.ApplicationModules;
      AppModuleSettings element18 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element18.ModuleId = new Guid("92ED576B-1CE3-4102-9F6A-F2F8A05CBFDB");
      element18.Name = "Personalization";
      element18.Title = "Personalization";
      element18.Description = "Personalize Sitefinity";
      element18.Type = "Telerik.Sitefinity.Personalization.Impl.PersonalizationModule, Telerik.Sitefinity.Personalization.Impl";
      element18.StartupType = StartupType.OnApplicationStart;
      applicationModules18.Add(element18);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules19 = this.ApplicationModules;
      AppModuleSettings element19 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element19.Name = "Feather";
      element19.Title = "Feather";
      element19.Description = "Modern, intuitive, convention based, mobile-first UI for Progress Sitefinity CMS";
      element19.Type = "Telerik.Sitefinity.Frontend.FrontendModule, Telerik.Sitefinity.Frontend";
      element19.StartupType = StartupType.OnApplicationStart;
      applicationModules19.Add(element19);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules20 = this.ApplicationModules;
      AppModuleSettings element20 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element20.ModuleId = new Guid("9DFDBB8F-CD5F-4125-81A0-06F31A97068E");
      element20.Name = "ProtectionShield";
      element20.Title = "Site shield";
      element20.Description = "Protect your website while it is under development and allow only trusted users to preview it for evaluation purposes";
      element20.Type = typeof (ProtectionShieldModule).FullName;
      element20.StartupType = StartupType.Disabled;
      applicationModules20.Add(element20);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules21 = this.ApplicationModules;
      AppModuleSettings element21 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element21.Name = "Comments";
      element21.Title = "Comments";
      element21.Description = "Create, modify and publish all kinds of comments";
      element21.Type = "Telerik.Sitefinity.Modules.Comments.CommentsModule, Telerik.Sitefinity";
      element21.StartupType = StartupType.OnApplicationStart;
      applicationModules21.Add(element21);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules22 = this.ApplicationModules;
      AppModuleSettings element22 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element22.Name = "WebServices";
      element22.Title = "Web services";
      element22.Description = "Web services module allows exposure of Sitefinity data through the OData protocol.";
      element22.Type = "Telerik.Sitefinity.Web.Api.ServiceApiModule";
      element22.StartupType = StartupType.OnApplicationStart;
      applicationModules22.Add(element22);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules23 = this.ApplicationModules;
      AppModuleSettings element23 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element23.Name = "Warmup";
      element23.Title = "Warmup";
      element23.Description = "Warmup module for plugging page warmup functionality";
      element23.Type = typeof (WarmupModule).FullName;
      element23.StartupType = StartupType.Disabled;
      applicationModules23.Add(element23);
      ConfigElementDictionary<string, AppModuleSettings> applicationModules24 = this.ApplicationModules;
      AppModuleSettings element24 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
      element24.Name = "AdminBridge";
      element24.Title = "AdminBridge";
      element24.Description = "Enables usage of the new Siteifinity backend.";
      element24.Type = "Telerik.Sitefinity.AdminBridge.AdminBridgeModule, Telerik.Sitefinity.AdminBridge";
      element24.StartupType = StartupType.OnApplicationStart;
      element24.Hidden = true;
      applicationModules24.Add(element24);
      foreach (SitefinityModuleAttribute sitefinityModuleAttribute in SystemConfig.GetSitefinityModuleAttributes())
      {
        ConfigElementDictionary<string, AppModuleSettings> applicationModules25 = this.ApplicationModules;
        AppModuleSettings element25 = new AppModuleSettings((ConfigElement) this.ApplicationModules);
        element25.Name = sitefinityModuleAttribute.Name;
        element25.Type = sitefinityModuleAttribute.Type.AssemblyQualifiedName;
        element25.Title = sitefinityModuleAttribute.Title;
        element25.Description = sitefinityModuleAttribute.Description;
        element25.StartupType = sitefinityModuleAttribute.StartupType;
        element25.ModuleId = Guid.Parse(sitefinityModuleAttribute.ModuleId);
        element25.ResourceClassId = sitefinityModuleAttribute.ResourceClassId;
        applicationModules25.Add(element25);
      }
    }

    private void InitializeCacheDependencyHandlers()
    {
      this.CacheDependencyHandlers.Add(new DataProviderSettings((ConfigElement) this.CacheDependencyHandlers)
      {
        Name = "ConfigCacheDependency",
        Description = "A cache dependency handler for configuration polices.",
        ProviderType = typeof (ConfigCacheDependencyHandler)
      });
      this.CacheDependencyHandlers.Add(new DataProviderSettings((ConfigElement) this.CacheDependencyHandlers)
      {
        Name = "GeneralCacheDependency",
        Description = "A generic cache dependency handler.",
        ProviderType = typeof (CacheDependencyHandler)
      });
    }

    private void InitializeCacheManagers()
    {
      ConfigElementDictionary<string, CacheManagerElement> cacheManagers = this.CacheManagers;
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.Internal.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.Global.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.Configuration.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.SiteMap.ToString(),
        StartScavengingAfterItemCount = 10000
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.SiteMapPageData.ToString(),
        StartScavengingAfterItemCount = 10000
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.SiteMapNodeUrl.ToString(),
        StartScavengingAfterItemCount = 10000
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.UserActivities.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.ContentOutput.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.PageFullPath.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.Users.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.ContentLinks.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.ContentLocations.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = "ApprovalTracking"
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.LocalizationResources.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.Statistics.ToString()
      });
      cacheManagers.Add(new CacheManagerElement((ConfigElement) cacheManagers)
      {
        Name = CacheManagerInstance.OutputCacheInfo.ToString()
      });
    }

    private void InitializeControlDesigners() => this.ControlDesigners.Add(new ControlDesignerSettings((ConfigElement) this.ControlDesigners)
    {
      ControlType = typeof (ContentBlock).FullName,
      ControlDesigner = typeof (ContentBlockDesigner).FullName
    });

    private void DisableSecurityModule()
    {
      AppModuleSettings appModuleSettings;
      if (!this.ApplicationModules.TryGetValue("WebSecurity", out appModuleSettings))
        return;
      appModuleSettings.StartupType = StartupType.Disabled;
    }

    private void ConfigureDefaultCacheVaryBy()
    {
      foreach (ConfigElement profile in (ConfigElementCollection) this.CacheSettings.Profiles)
        profile.SetParameter("varyByParams", "*");
    }

    private void InitializeTypeImplementationsMapping()
    {
      this.AddTypeImplementations(typeof (ViewModeElement), typeof (GridViewModeElement), typeof (ListViewModeElement), typeof (DynamicListViewModeElement));
      this.AddTypeImplementations(typeof (ColumnElement), typeof (CommandColumnElement), typeof (DataColumnElement), typeof (MultisiteDataColumnElement), typeof (ImageColumnElement), typeof (ActionMenuColumnElement), typeof (DynamicColumnElement));
      this.AddTypeImplementations(typeof (WidgetElement), typeof (ContentItemWidgetElement), typeof (LibraryWidgetElement), typeof (DynamicCommandWidgetElement), typeof (LiteralWidgetElement), typeof (CommandWidgetElement), typeof (MultisiteCommandWidgetElement), typeof (ActionMenuWidgetElement), typeof (MultiFlatSelectorElement), typeof (StateWidgetElement), typeof (SearchWidgetElement), typeof (LanguagesDropDownListWidgetElement), typeof (StateCommandWidgetElement), typeof (ProvidersListWidgetElement), typeof (DateFilteringWidgetDefinitionElement), typeof (ModeStateWidgetElement), typeof (FolderBreadcrumbWidgetElement), typeof (SelectionWidgetElement), typeof (ContentFilteringWidgetDefinitionElement), typeof (CommentsFilterByTypeElement), typeof (StatusFilterElement), typeof (ScheduledTaskProgressBarElement));
      this.AddTypeImplementations(typeof (ContentViewDefinitionElement), typeof (ContentViewDetailElement), typeof (DownloadListViewDetailElement), typeof (ContentViewMasterElement), typeof (MediaContentMasterElement), typeof (VideosViewMasterElement), typeof (VideosViewMasterLightBoxElement), typeof (MediaContentDetailElement), typeof (FormsDetailViewElement), typeof (MasterGridViewElement), typeof (ImagesViewDetailElement), typeof (ComparisonViewElement), typeof (ContentViewMasterDetailElement), typeof (DetailFormViewElement), typeof (DownloadListViewMasterElement), typeof (VideosViewDetailElement), typeof (ImagesViewMasterElement), typeof (UserProfileDetailElement), typeof (UserProfileViewMasterElement), typeof (MarkedItemsMasterGridViewElement), typeof (TaxaMasterGridViewElement));
      this.AddTypeImplementations(typeof (FieldDefinitionElement), typeof (CssSelectorFieldDefinitionElement), typeof (LayoutTransformFieldDefinitionElement), typeof (MediaQueryRuleFieldDefinitionElement), typeof (ChoiceFieldElement), typeof (BlobStorageChoiceFieldElement), typeof (UserProvidersFieldDefinitionElement), typeof (HierarchicalTaxonParentSelectorFieldElement), typeof (AssetsFieldDefinitionElement), typeof (RelatedMediaFieldDefinitionElement), typeof (RelatedDataFieldDefinitionElement), typeof (AddressFieldDefinitionElement), typeof (BindableChoiceFieldElement), typeof (DefaultFieldDefinitionElement), typeof (CompositeFieldElement), typeof (CacheProfileFieldElement), typeof (GenericFieldDefinitionElement), typeof (MultiImageFieldElement), typeof (ContentWorkflowStatusInfoFieldElement), typeof (ContentStatisticsFieldElement), typeof (CacheSettingsFieldElement), typeof (ExternalPageFieldElement), typeof (LinkFieldDefinitionElement), typeof (LanguageChoiceFieldElement), typeof (LanguageListFieldElement), typeof (MetaTypeStructureFieldDefinitionElement), typeof (ExpandableFieldElement), typeof (PageFieldElement), typeof (BulkEditFieldElement), typeof (EmbedControlDefinitionElement), typeof (LibrarySelectorFieldDefinitionElement), typeof (TextFieldDefinitionElement), typeof (MirrorTextFieldElement), typeof (UrlMirrorTextFieldElement), typeof (PageUrlMirrorTextFieldElement), typeof (GenericHierarchicalFieldDefinitionElement), typeof (ListPipeSettingsFieldDefinitionElement), typeof (PageTemplateFieldDefinitionElement), typeof (ResponsiveLayoutFieldDefinitionElement), typeof (VersionNoteControlDefinitionElement), typeof (ComparisonFieldElement), typeof (ImageFieldElement), typeof (FileFieldDefinitionElement), typeof (TaxonFieldDefinitionElement), typeof (HierarchicalTaxonFieldDefinitionElement), typeof (FlatTaxonFieldDefinitionElement), typeof (StatusFieldElement), typeof (HtmlFieldElement), typeof (DateFieldElement), typeof (ParentSelectorFieldElement), typeof (DynamicContentWorkflowStatusInfoFieldElement), typeof (ParentLibraryFieldDefinitionElement), typeof (ColorPickerFieldElement), typeof (RecurrencyFieldDefinitionElement), typeof (FolderFieldElement), typeof (ContentLocationInfoFieldElement), typeof (NavigationTransformationsFieldDefinitionElement), typeof (ThumbnailProfileFieldDefinitionElement), typeof (CanonicalUrlSettingsFieldElement), typeof (SiteSelectorFieldElement), typeof (RelatingDataFieldDefinitionElement), typeof (WarningFieldElement), typeof (EmailTextFieldDefinitionElement));
      this.AddTypeImplementations(typeof (FieldControlDefinitionElement), typeof (CssSelectorFieldDefinitionElement), typeof (LayoutTransformFieldDefinitionElement), typeof (MediaQueryRuleFieldDefinitionElement), typeof (ChoiceFieldElement), typeof (BlobStorageChoiceFieldElement), typeof (UserProvidersFieldDefinitionElement), typeof (HierarchicalTaxonParentSelectorFieldElement), typeof (AssetsFieldDefinitionElement), typeof (RelatedMediaFieldDefinitionElement), typeof (RelatedDataFieldDefinitionElement), typeof (AddressFieldDefinitionElement), typeof (BindableChoiceFieldElement), typeof (GenericFieldDefinitionElement), typeof (MultiImageFieldElement), typeof (ContentWorkflowStatusInfoFieldElement), typeof (ContentStatisticsFieldElement), typeof (ExternalPageFieldElement), typeof (LinkFieldDefinitionElement), typeof (LanguageChoiceFieldElement), typeof (LanguageListFieldElement), typeof (MetaTypeStructureFieldDefinitionElement), typeof (ExpandableFieldElement), typeof (PageFieldElement), typeof (EmbedControlDefinitionElement), typeof (LibrarySelectorFieldDefinitionElement), typeof (TextFieldDefinitionElement), typeof (MirrorTextFieldElement), typeof (UrlMirrorTextFieldElement), typeof (PageUrlMirrorTextFieldElement), typeof (GenericHierarchicalFieldDefinitionElement), typeof (ListPipeSettingsFieldDefinitionElement), typeof (PageTemplateFieldDefinitionElement), typeof (ResponsiveLayoutFieldDefinitionElement), typeof (VersionNoteControlDefinitionElement), typeof (ImageFieldElement), typeof (FileFieldDefinitionElement), typeof (TaxonFieldDefinitionElement), typeof (HierarchicalTaxonFieldDefinitionElement), typeof (FlatTaxonFieldDefinitionElement), typeof (HtmlFieldElement), typeof (DateFieldElement), typeof (ParentSelectorFieldElement), typeof (DynamicContentWorkflowStatusInfoFieldElement), typeof (ParentLibraryFieldDefinitionElement), typeof (ColorPickerFieldElement), typeof (FolderFieldElement), typeof (RecurrencyFieldDefinitionElement), typeof (ContentLocationInfoFieldElement), typeof (NavigationTransformationsFieldDefinitionElement), typeof (ThumbnailProfileFieldDefinitionElement), typeof (SiteSelectorFieldElement), typeof (RelatingDataFieldDefinitionElement), typeof (WarningFieldElement), typeof (EmailTextFieldDefinitionElement));
    }

    private static IEnumerable<SitefinityServiceAttribute> GetSitefinityServiceAttributes()
    {
      if (SystemConfig.sitefinityServiceAttributes == null)
        SystemConfig.sitefinityServiceAttributes = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (p => SystemConfig.ContainsAnyAttributes<SitefinityServiceAttribute>(p))).SelectMany<Assembly, SitefinityServiceAttribute>((Func<Assembly, IEnumerable<SitefinityServiceAttribute>>) (a => a.GetCustomAttributes(typeof (SitefinityServiceAttribute)).Cast<SitefinityServiceAttribute>()));
      return SystemConfig.sitefinityServiceAttributes;
    }

    private static IEnumerable<SitefinityModuleAttribute> GetSitefinityModuleAttributes()
    {
      if (SystemConfig.sitefinityModuleAttributes == null)
        SystemConfig.sitefinityModuleAttributes = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (p => SystemConfig.ContainsAnyAttributes<SitefinityModuleAttribute>(p))).SelectMany<Assembly, SitefinityModuleAttribute>((Func<Assembly, IEnumerable<SitefinityModuleAttribute>>) (a => a.GetCustomAttributes(typeof (SitefinityModuleAttribute)).Cast<SitefinityModuleAttribute>()));
      return SystemConfig.sitefinityModuleAttributes;
    }

    private static bool ContainsAnyAttributes<TAttribute>(Assembly assembly) where TAttribute : Attribute
    {
      try
      {
        return assembly.GetCustomAttributes(typeof (TAttribute)).Any<Attribute>();
      }
      catch
      {
        return false;
      }
    }
  }
}
