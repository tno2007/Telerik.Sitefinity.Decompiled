// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Configuration.CacheServiceElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.OutputCache.Configuration
{
  /// <summary>Configuration for Cache service.</summary>
  public class CacheServiceElement : ConfigElement
  {
    private const string EnabledPropName = "enabled";
    private const string AuthenticationKeyPropName = "authenticationKey";
    private const string RequireHttpsForAllRequestsPropName = "requireHttpsForAllRequests";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Configuration.CacheServiceElement" /> class.
    /// </summary>
    /// <param name="parent">Parent configuration element.</param>
    public CacheServiceElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Configuration.CacheServiceElement" /> class.
    /// </summary>
    internal CacheServiceElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Configuration.CacheServiceElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal CacheServiceElement(bool check)
      : base(check)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether Cache Service is enabled.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty("enabled")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheServiceEnabledDescription", Title = "CacheServiceEnabledCaption")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the authentication key that will be used by the Cache Service requests
    /// </summary>
    /// <value>The authentication key.</value>
    [ConfigurationProperty("authenticationKey", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheServiceAuthenticationKeyDescription", Title = "CacheServiceAuthenticationKeyCaption")]
    public string AuthenticationKey
    {
      get => (string) this["authenticationKey"];
      set => this["authenticationKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to require https for all Cache Service requests.
    /// </summary>
    [ConfigurationProperty("requireHttpsForAllRequests", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheServiceRequireHttpsDecription", Title = "CacheServiceRequireHttpsTitle")]
    public virtual bool RequireHttpsForAllRequests
    {
      get => (bool) this["requireHttpsForAllRequests"];
      set => this["requireHttpsForAllRequests"] = (object) value;
    }
  }
}
