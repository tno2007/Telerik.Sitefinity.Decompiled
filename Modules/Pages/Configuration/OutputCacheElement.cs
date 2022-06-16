// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.OutputCacheElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Web.OutputCache.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Represents configuration element for output cache settings.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheElementDescription", Title = "OutputCacheElementTitle")]
  public class OutputCacheElement : ConfigElement
  {
    internal const string NoCacheProfileName = "No Caching";
    internal const string StandardCachingProfileName = "Standard Caching";
    internal const string LongCachingProfileName = "Long Caching";
    internal const string DefaultAnyLocationProfileName = "Any Location";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.OutputCacheElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public OutputCacheElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page and control output cache are enabled.
    /// If disabled, no pages are cached regardless of the programmatic or declarative settings.
    /// Default value is true.
    /// </summary>
    [ConfigurationProperty("enableOutputCache", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableOutputCacheDescription", Title = "EnableOutputCacheTitle")]
    public bool EnableOutputCache
    {
      get => (bool) this["enableOutputCache"];
      set => this["enableOutputCache"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether client caching is controlled by a client cache profile
    /// <c>true</c> (the default) - client caching is controlled by a client cache profile;
    /// <c>false</c> - the client cache is globally disabled (<c>Cache-Control: no-cache</c>).
    /// </summary>
    [ConfigurationProperty("enableClientCache", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableClientCacheDescription", Title = "EnableClientCacheTitle")]
    public bool EnableClientCache
    {
      get => (bool) this["enableClientCache"];
      set => this["enableClientCache"] = (object) value;
    }

    /// <summary>Gets or sets the default cache provider name</summary>
    [ConfigurationProperty("defaultOutputCacheProvider", DefaultValue = CacheProvider.InMemory)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DefaultCacheProviderTitle")]
    public CacheProvider DefaultCacheProvider
    {
      get => (CacheProvider) this["defaultOutputCacheProvider"];
      set => this["defaultOutputCacheProvider"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default output cache profile.
    /// </summary>
    [ConfigurationProperty("defaultProfile", DefaultValue = "Standard Caching")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultOutputProfileDescription", Title = "DefaultOutputProfileTitle")]
    public virtual string DefaultProfile
    {
      get => (string) this["defaultProfile"];
      set => this["defaultProfile"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default client cache profile for Image libraries.
    /// </summary>
    [ConfigurationProperty("defaultClientProfile", DefaultValue = "Standard Caching")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DefaultImageProfileTitle")]
    public virtual string DefaultImageProfile
    {
      get => (string) this["defaultClientProfile"];
      set => this["defaultClientProfile"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default client cache profile for Document libraries.
    /// </summary>
    [ConfigurationProperty("defaultDocumentProfile", DefaultValue = "Standard Caching")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DefaultDocumentProfileTitle")]
    public virtual string DefaultDocumentProfile
    {
      get => (string) this["defaultDocumentProfile"];
      set => this["defaultDocumentProfile"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default client cache profile for Video libraries.
    /// </summary>
    [ConfigurationProperty("defaultVideoProfile", DefaultValue = "Standard Caching")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DefaultVideoProfileTitle")]
    public virtual string DefaultVideoProfile
    {
      get => (string) this["defaultVideoProfile"];
      set => this["defaultVideoProfile"] = (object) value;
    }

    /// <summary>Gets or sets the maximum age for static resources.</summary>
    [ConfigurationProperty("maxAgeForStaticResources", DefaultValue = 86400)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxAgeForStaticResourcesDescription", Title = "MaxAgeTitle")]
    public int? MaxAgeForStaticResources
    {
      get => (int?) this["maxAgeForStaticResources"];
      set => this["maxAgeForStaticResources"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether page should be served only once before its output cache is filled.
    /// If not - page can be served directly from database to many requests, causing compilation every time until it gets into the cache.
    /// </summary>
    [ConfigurationProperty("waitForPageOutputCacheToFill", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WaitForPageOutputCacheToFillDescription", Title = "WaitForPageOutputCacheToFillTitle")]
    public bool WaitForPageOutputCacheToFill
    {
      get => (bool) this["waitForPageOutputCacheToFill"];
      set => this["waitForPageOutputCacheToFill"] = (object) value;
    }

    /// <summary>Gets or sets the cache provider settings.</summary>
    /// <value>The cache provider settings.</value>
    [ConfigurationProperty("cacheProvidersSettings")]
    public virtual CacheProvidersSettingsElement CacheProviders
    {
      get => (CacheProvidersSettingsElement) this["cacheProvidersSettings"];
      set => this["cacheProvidersSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the settings for invalidation of the page output cache.
    /// </summary>
    /// <value>The cache provider settings.</value>
    [ConfigurationProperty("pageCacheInvalidation")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageCacheInvalidationDescription", Title = "PageCacheInvalidationTitle")]
    public virtual CacheInvalidationConfigElement PageCacheInvalidation
    {
      get => (CacheInvalidationConfigElement) this["pageCacheInvalidation"];
      set => this["pageCacheInvalidation"] = (object) value;
    }

    /// <summary>Gets the output cache profiles.</summary>
    /// <value>The output cache profiles.</value>
    [ConfigurationProperty("profiles")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheProfilesDescription", Title = "OutputCacheProfilesTitle")]
    public ConfigElementDictionary<string, OutputCacheProfileElement> Profiles => (ConfigElementDictionary<string, OutputCacheProfileElement>) this["profiles"];

    /// <summary>Gets the library cache profiles.</summary>
    /// <value>The output cache profiles.</value>
    [ConfigurationProperty("mediaProfiles")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientCacheProfilesDescription", Title = "ClientCacheProfilesTitle")]
    public ConfigElementDictionary<string, OutputCacheProfileElement> MediaCacheProfiles => (ConfigElementDictionary<string, OutputCacheProfileElement>) this["mediaProfiles"];

    /// <summary>Gets the settings for cache service.</summary>
    /// <value>The cache service settings.</value>
    [ConfigurationProperty("cacheService")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "CacheServiceSettingsCaption")]
    public CacheServiceElement CacheService => (CacheServiceElement) this["cacheService"];

    /// <summary>Gets the client cache profiles.</summary>
    /// <value>The output cache profiles.</value>
    [Obsolete("Use LibraryCacheProfiles instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ConfigurationProperty("clientProfiles")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientCacheProfilesDescription", Title = "ClientCacheProfilesTitle")]
    public ConfigElementDictionary<string, ClientCacheProfileElement> ClientProfiles => (ConfigElementDictionary<string, ClientCacheProfileElement>) this["clientProfiles"];

    /// <summary>
    /// Gets or sets the name of the default client cache profile for Image libraries.
    /// </summary>
    [Obsolete("Use DefaultImageProfile instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string DefaultClientProfile
    {
      get => this.DefaultImageProfile;
      set => this.DefaultImageProfile = value;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Generic large method")]
    protected override void OnPropertiesInitialized()
    {
      this.Profiles.Add(new OutputCacheProfileElement((ConfigElement) this.Profiles)
      {
        Name = "No Caching",
        Location = OutputCacheLocation.None,
        Enabled = false
      });
      this.Profiles.Add(new OutputCacheProfileElement((ConfigElement) this.Profiles)
      {
        Name = "Standard Caching",
        Duration = 120,
        SlidingExpiration = true
      });
      this.Profiles.Add(new OutputCacheProfileElement((ConfigElement) this.Profiles)
      {
        Name = "Long Caching",
        Duration = 1200,
        SlidingExpiration = false
      });
      this.Profiles.Add(new OutputCacheProfileElement((ConfigElement) this.Profiles)
      {
        Name = "Any Location",
        Location = OutputCacheLocation.Any,
        ClientMaxAge = new int?(60),
        Duration = 120,
        SlidingExpiration = true
      });
      this.MediaCacheProfiles.Add(new OutputCacheProfileElement((ConfigElement) this.MediaCacheProfiles)
      {
        Name = "No Caching",
        Location = OutputCacheLocation.None,
        Enabled = false,
        ClientMaxAge = new int?(0)
      });
      this.MediaCacheProfiles.Add(new OutputCacheProfileElement((ConfigElement) this.MediaCacheProfiles)
      {
        Name = "Standard Caching",
        Location = OutputCacheLocation.Any,
        ClientMaxAge = new int?(7776000)
      });
      this.MediaCacheProfiles.Add(new OutputCacheProfileElement((ConfigElement) this.MediaCacheProfiles)
      {
        Name = "Long Caching",
        Location = OutputCacheLocation.Any,
        ClientMaxAge = new int?(31536000)
      });
      this.ClientProfiles.Add(new ClientCacheProfileElement((ConfigElement) this.ClientProfiles)
      {
        Name = "No Caching",
        Enabled = new bool?(false),
        Duration = 0
      });
      this.ClientProfiles.Add(new ClientCacheProfileElement((ConfigElement) this.ClientProfiles)
      {
        Name = "Standard Caching",
        Enabled = new bool?(true),
        Duration = 7776000
      });
      this.ClientProfiles.Add(new ClientCacheProfileElement((ConfigElement) this.ClientProfiles)
      {
        Name = "Long Caching",
        Enabled = new bool?(true),
        Duration = 31536000
      });
    }

    /// <summary>
    /// Known parameters for OutputCacheProfileElement that applies for pages
    /// </summary>
    public class PageCacheKnownParams
    {
      internal const string VaryByHost = "varyByHost";
      internal const string VaryByUserAgent = "varyByUserAgent";
      internal const string VaryByCustom = "varyByCustom";
      internal const string SetNoStore = "setNoStore";
      internal const string SetRevalidation = "setRevalidation";
      internal const string SetOmitVaryStar = "setOmitVaryStar";
      internal const string VaryByParams = "varyByParams";
    }

    /// <summary>
    /// Known parameters for OutputCacheProfileElement that applies for media (images, documents and videos)
    /// </summary>
    public class MediaCacheKnownParams
    {
      internal const string MaxSize = "maxSize";
    }

    internal class PropNames
    {
      internal const string EnableOutputCache = "enableOutputCache";
      internal const string EnableClientCache = "enableClientCache";
      internal const string DefaultCacheProvider = "defaultOutputCacheProvider";
      internal const string CacheProvidersSettings = "cacheProvidersSettings";
      internal const string DefaultProfile = "defaultProfile";
      internal const string DefaultImageProfile = "defaultClientProfile";
      internal const string DefaultDocumentProfile = "defaultDocumentProfile";
      internal const string DefaultVideoProfile = "defaultVideoProfile";
      internal const string Profiles = "profiles";
      internal const string ClientProfiles = "clientProfiles";
      internal const string MediaProfiles = "mediaProfiles";
      internal const string WaitForPageOutputCacheToFill = "waitForPageOutputCacheToFill";
      internal const string MaxAgeForStaticResources = "maxAgeForStaticResources";
      internal const string PageCacheInvalidationSettings = "pageCacheInvalidation";
      internal const string CacheService = "cacheService";
    }
  }
}
