// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SitefinityContextBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.SiteSettings;
using Telerik.Sitefinity.SiteSettings.Basic;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Base class that represents data and logic specific for the current Sitefinity context
  /// </summary>
  public abstract class SitefinityContextBase
  {
    private Telerik.Sitefinity.SiteSettings.SiteSettings siteSettings;
    internal const string BackendCultureName = "sf_backend_lang";
    internal const string FrontendCultureName = "sf_frontend_lang";
    /// <summary>
    /// Query string parameter name for loading context on global level for the whole application
    /// (not for the current site).
    /// </summary>
    internal const string GlobalContextParamName = "sf_global";
    /// <summary>
    /// Query string parameter value that will be parsed
    /// as true for <see cref="F:Telerik.Sitefinity.Services.SitefinityContextBase.GlobalContextParamName" />.
    /// </summary>
    internal const string GlobalContextQueryStringParamValueTrue = "true";
    private const string GlobalTransactionKey = "sf_global_transaction";
    private const string ServiceRequestKey = "sf_is_service_request";
    private const string AllowConcurrentEditingKey = "sf_allow_concurrent_editing";

    public abstract ISite CurrentSite { get; }

    internal abstract ISite DefaultSite { get; }

    public abstract void InvalidateCache();

    [Obsolete("The system is always in Multisite mode.")]
    public virtual bool IsMultisiteMode => this is IMultisiteContext;

    internal virtual bool IsDataProviderRestricted(Type managerType, string providerName) => false;

    internal virtual bool IsOneSiteMode => this.GetSites().Count<ISite>() == 1;

    /// <summary>
    /// Gets information if the current context is for the whole application instance
    /// (global) or is per site.
    /// </summary>
    /// <value>The is global context.</value>
    internal virtual bool IsGlobalContext
    {
      get => true;
      set
      {
      }
    }

    /// <summary>
    /// Gets the multisite context if the application is running in multisite mode, otherwise returns null.
    /// </summary>
    /// <value>The multisite context.</value>
    public IMultisiteContext MultisiteContext => this as IMultisiteContext;

    /// <summary>Gets the app settings.</summary>
    /// <value>The app settings.</value>
    public virtual IAppSettings AppSettings => (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;

    /// <summary>Gets all sites is the system</summary>
    /// <param name="ifAccessible">Shows only the sites that the current user is allowed to view.</param>
    /// <returns>Collection with <see cref="T:Telerik.Sitefinity.Multisite.ISite" /> objects.</returns>
    public IEnumerable<ISite> GetSites(bool ifAccessible = false)
    {
      List<ISite> source = new List<ISite>();
      if (this.MultisiteContext != null)
      {
        source.AddRange(this.MultisiteContext.GetSites());
        if (ifAccessible)
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity != null)
          {
            IEnumerable<Guid> allowedSiteIds = SystemManager.MultisiteContext.GetAllowedSites(currentIdentity.UserId, currentIdentity.MembershipProvider);
            source = source.Where<ISite>((Func<ISite, bool>) (s => allowedSiteIds.Contains<Guid>(s.Id))).ToList<ISite>();
          }
        }
      }
      else if (!ifAccessible || SecurityManager.IsBackendUser())
        source.Add(this.CurrentSite);
      return (IEnumerable<ISite>) source;
    }

    internal TContract GetSetting<TContract>() where TContract : ISettingsDataContract => this.SiteSettings.GetSetting<TContract>("sitepolicy", this.CurrentSite.Id.ToString());

    /// <summary>Gets the setting for the current site.</summary>
    /// <typeparam name="TContract">The type of the contract.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns></returns>
    public TResult GetSetting<TContract, TResult>() where TContract : ISettingsDataContract, TResult => this.SiteSettings.GetSetting<TContract, TResult>("sitepolicy", this.CurrentSite.Id.ToString());

    internal object GetSetting(Type contractType) => this.SiteSettings.GetSetting(contractType, "sitepolicy", this.CurrentSite.Id.ToString());

    internal TimeZoneInfo GetCurrentTimeZone()
    {
      TimeZoneSettingsContract setting = this.GetSetting<TimeZoneSettingsContract>();
      return setting != null && setting.TimeZone != null ? setting.TimeZone : TimeZoneInfo.Local;
    }

    /// <summary>
    /// When overridden in a derived class, reloads the current site context.
    /// </summary>
    internal virtual void ReloadCurrentContext()
    {
    }

    internal CultureInfo BackendCulture
    {
      get
      {
        if (!Bootstrapper.IsReady)
          return (CultureInfo) null;
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return (CultureInfo) null;
        CultureInfo backendCulture = (CultureInfo) currentHttpContext.Items[(object) "sf_backend_lang"];
        if (backendCulture != null)
          return backendCulture;
        UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(SecurityManager.GetCurrentUserId());
        if (cachedUserProfile != null && !cachedUserProfile.PreferredLanguage.IsNullOrEmpty())
        {
          CultureInfo[] backendLanguages = this.AppSettings.DefinedBackendLanguages;
          try
          {
            CultureElement cultureElement;
            if (Config.Get<ResourcesConfig>().BackendCultures.TryGetValue(cachedUserProfile.PreferredLanguage, out cultureElement))
              backendCulture = CultureInfo.GetCultureInfo(cultureElement.Culture);
          }
          catch (CultureNotFoundException ex)
          {
          }
        }
        if (backendCulture == null)
          backendCulture = this.AppSettings.DefaultBackendLanguage;
        currentHttpContext.Items[(object) "sf_backend_lang"] = (object) backendCulture;
        return backendCulture;
      }
    }

    public string GlobalTransaction
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        return currentHttpContext != null ? (string) currentHttpContext.Items[(object) "sf_global_transaction"] : (string) null;
      }
      set
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        currentHttpContext.Items[(object) "sf_global_transaction"] = (object) value;
      }
    }

    /// <summary>
    /// Gets or sets the current request`s culture for content operations.
    /// </summary>
    public CultureInfo Culture
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (SystemManager.Initializing || currentHttpContext == null)
          return CultureInfo.CurrentUICulture;
        return currentHttpContext.Items.Contains((object) "sf_frontend_lang") && currentHttpContext.Items[(object) "sf_frontend_lang"] is CultureInfo cultureInfo ? cultureInfo : this.CurrentSite.DefaultCulture;
      }
      set
      {
        CultureInfo cultureInfo = value;
        if (cultureInfo.Equals((object) CultureInfo.InvariantCulture))
          cultureInfo = this.CurrentSite.DefaultCulture;
        if (!SystemManager.Initializing && !Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.AllLanguages.Values.Contains(cultureInfo))
          throw new InvalidOperationException("Culture " + cultureInfo.DisplayName + " not available in Sitefinity`s defined languages.");
        if (SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sf_frontend_lang"] = (object) cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
      }
    }

    /// <summary>Gets all configured languages for Sitefinity.</summary>
    public CultureInfo[] SystemCultures => Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages;

    /// <summary>Gets the default language for Sitefinity.</summary>
    public CultureInfo DefaultSystemCulture => Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefaultFrontendLanguage;

    [Obsolete("Use GetSetting method to get a setting for the current site")]
    internal ISiteSettings SiteSettings
    {
      get
      {
        if (this.siteSettings == null)
          this.siteSettings = new Telerik.Sitefinity.SiteSettings.SiteSettings();
        return (ISiteSettings) this.siteSettings;
      }
    }

    /// <summary>
    /// Resolves the given server relative URL based on the Sitefinity context. (E.g. Appends the language and site parts of the url)
    /// </summary>
    /// <param name="url">The server relative URL.</param>
    /// <returns></returns>
    internal virtual string ResolveUrl(string url)
    {
      url = url.StartsWith("~/") ? ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(url) : throw new ArgumentException("The url argument must start with (~/).", nameof (url));
      if (this is Telerik.Sitefinity.Multisite.MultisiteContext multisiteContext)
        url = multisiteContext.ResolveUrl(url);
      return url;
    }

    internal bool IsServiceRequest
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
        {
          bool? nullable = (bool?) currentHttpContext.Items[(object) "sf_is_service_request"];
          if (nullable.HasValue && nullable.Value)
            return true;
        }
        return false;
      }
      set
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        currentHttpContext.Items[(object) "sf_is_service_request"] = (object) value;
      }
    }

    internal bool AllowConcurrentEditing
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
        {
          bool? nullable = (bool?) currentHttpContext.Items[(object) "sf_allow_concurrent_editing"];
          if (nullable.HasValue && nullable.Value)
            return true;
        }
        return false;
      }
      set
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        currentHttpContext.Items[(object) "sf_allow_concurrent_editing"] = (object) value;
      }
    }
  }
}
