// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Configuration.ProtectionShieldConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.ProtectionShield.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.ProtectionShield.Configuration
{
  /// <summary>The configuration for Protection shield module.</summary>
  [ObjectInfo(typeof (ProtectionShieldResources), Description = "ProtectionShieldDescription", Title = "ProtectionShieldSettings")]
  internal class ProtectionShieldConfig : ModuleConfigBase
  {
    private const string EnabledForSitesPropName = "enabledForSites";
    private const string DefaultAccessTokenProviderPropName = "defaultAccessTokenProvider";
    private const string AccessTokenProvidersPropName = "accessTokenProviders";
    private const string AccessTokenIssuersPropName = "accessTokenIssuers";
    private const string EnabledForAllSitesPropName = "enabledForAllSites";
    private const string DefaultAccessTokenIssuerPropName = "defaultAccessTokenIssuer";

    /// <inheritdoc />
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      if (this.Providers.Count == 0)
        this.Providers.Add(new DataProviderSettings()
        {
          Name = "OpenAccessDataProvider",
          Description = "The data provider for protection shield module based on the Telerik OpenAccess ORM.",
          ProviderType = typeof (OpenAccessProtectionShieldProvider)
        });
      if (this.AccessTokenProviders.Count == 0)
        this.AccessTokenProviders.Add(new DataProviderSettings()
        {
          Name = "OpenAccessDataProvider",
          Description = "The data provider for access tokens based on the Telerik OpenAccess ORM.",
          ProviderType = typeof (OpenAccessAccessTokenProvider)
        });
      if (this.AccessTokenIssuers.Count != 0)
        return;
      this.AccessTokenIssuers.Add(new DataProviderSettings()
      {
        Name = "AccessTokenIssuer",
        Description = "Access token issuer based on transactional persistance.",
        ProviderType = typeof (AccessTokenIssuer)
      });
    }

    /// <summary>Gets the notifications settings.</summary>
    /// <value>The notifications.</value>
    [ConfigurationProperty("notifications")]
    public NotificationsSettings Notifications => (NotificationsSettings) this["notifications"];

    /// <summary>
    /// Gets or sets a collection of predefined list of enabled site.
    /// </summary>
    /// <value>The site config elements.</value>
    [ConfigurationProperty("enabledForSites")]
    [ObjectInfo(typeof (ProtectionShieldResources), Description = "ProtectionShieldDescription", Title = "ProtectionShieldResourcesTitlePlural")]
    public virtual ConfigElementDictionary<string, EnabledSitesConfigElement> EnabledForSites
    {
      get => (ConfigElementDictionary<string, EnabledSitesConfigElement>) this["enabledForSites"];
      set => this["enabledForSites"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default access token provider.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultAccessTokenProvider")]
    [ConfigurationProperty("defaultAccessTokenProvider", DefaultValue = "OpenAccessDataProvider")]
    public virtual string DefaultAccessTokenProvider
    {
      get => (string) this["defaultAccessTokenProvider"];
      set => this["defaultAccessTokenProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "AccessTokenProviders")]
    [ConfigurationProperty("accessTokenProviders")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> AccessTokenProviders => (ConfigElementDictionary<string, DataProviderSettings>) this["accessTokenProviders"];

    /// <summary>
    /// Gets or sets the name of the default access token issuer.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultAccessTokenIssuer")]
    [ConfigurationProperty("defaultAccessTokenIssuer", DefaultValue = "AccessTokenIssuer")]
    public virtual string DefaultAccessTokenIssuer
    {
      get => (string) this["defaultAccessTokenIssuer"];
      set => this["defaultAccessTokenIssuer"] = (object) value;
    }

    /// <summary>Gets a collection of access token issuer settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "AccessTokenIssuers")]
    [ConfigurationProperty("accessTokenIssuers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> AccessTokenIssuers => (ConfigElementDictionary<string, DataProviderSettings>) this["accessTokenIssuers"];

    /// <summary>
    /// Gets or sets a value indicating whether to enable the SiteShield for all sites.
    /// </summary>
    /// <value>
    /// <c>true</c> if [Enable Shield for all sites]; otherwise, <c>false</c>.
    /// </value>
    [DescriptionResource(typeof (ConfigDescriptions), "EnableForAllSites")]
    [ConfigurationProperty("enabledForAllSites", DefaultValue = false)]
    public virtual bool EnabledForAllSites
    {
      get => (bool) this["enabledForAllSites"];
      set => this["enabledForAllSites"] = (object) value;
    }
  }
}
