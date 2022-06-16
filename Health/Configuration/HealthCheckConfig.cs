// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.Configuration.HealthCheckConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Health.Configuration
{
  /// <summary>Health Check Configuration settings</summary>
  public class HealthCheckConfig : ConfigElement
  {
    internal const string HealthChecksChecksPropName = "healthChecks";
    internal const string HealthChecksCaptionTitleName = "HealthChecksCaption";
    private const string EnabledPropName = "enabled";
    private const string LoggingPropName = "logging";
    private const string ReturnHttpErrorStatusCodePropName = "returnHttpErrorStatusCode";
    private const string AuthenticationKeyPropName = "authenticationKey";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.Configuration.HealthCheckConfig" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public HealthCheckConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.Configuration.HealthCheckConfig" /> class.
    /// </summary>
    internal HealthCheckConfig()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the test is enabled or disabled.
    /// </summary>
    /// <value>The Health test is enabled or disabled.</value>
    [ConfigurationProperty("enabled", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HealthChecksEnabledCaption")]
    public virtual bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the HealthCheck tasks are logging to file.
    /// </summary>
    /// <value>The Health tasks are logging or not.</value>
    [ConfigurationProperty("logging", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckLoggingDescription", Title = "HealthCheckLoggingCaption")]
    public virtual bool Logging
    {
      get => (bool) this["logging"];
      set => this["logging"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the HealthCheck status code is 500 when any critical check is failed.
    /// </summary>
    /// <value>The Health tasks are logging or not.</value>
    [ConfigurationProperty("returnHttpErrorStatusCode", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckReturnHttpErrorStatusCodeDescription", Title = "HealthCheckReturnHttpErrorStatusCodeCaption")]
    [Obsolete("Use app setting key sf:HealthCheckUnhealthyStatusCode to specify the status code that should be returned when the system is not healthy, e.g. <add key=\"sf: HealthCheckUnhealthyStatusCode\" value=\"500\" />")]
    public virtual bool ReturnHttpErrorStatusCode
    {
      get => (bool) this["returnHttpErrorStatusCode"];
      set => this["returnHttpErrorStatusCode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the authentication key that will be used by the HealthCheck requests
    /// </summary>
    /// <value>The authentication key.</value>
    [ConfigurationProperty("authenticationKey", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckAuthenticationKeyDescription", Title = "HealthCheckAuthenticationKeyCaption")]
    public string AuthenticationKey
    {
      get => (string) this["authenticationKey"];
      set => this["authenticationKey"] = (object) value;
    }

    /// <summary>Gets or sets the list of Health checks.</summary>
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Used for unit tests.")]
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Used for unit tests.")]
    [ConfigurationProperty("healthChecks")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HealthChecksCaption")]
    public virtual ConfigElementDictionary<string, CheckConfigElement> HealthChecks
    {
      get => (ConfigElementDictionary<string, CheckConfigElement>) this["healthChecks"] ?? new ConfigElementDictionary<string, CheckConfigElement>();
      set => this["healthChecks"] = (object) value;
    }
  }
}
