// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.Configuration.CheckConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Health.Configuration
{
  /// <summary>Health Check Configuration Element</summary>
  public class CheckConfigElement : ConfigElement, ICheckConfigElement
  {
    private const string NamePropName = "name";
    private const string TimeoutSecondsPropName = "timeoutSeconds";
    private const string CriticalPropName = "critical";
    private const string GroupsPropName = "groups";
    private const string TypePropName = "type";
    private const string EnabledPropName = "enabled";
    private const string ParametersPropName = "parameters";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.Configuration.CheckConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CheckConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.Configuration.CheckConfigElement" /> class.
    /// </summary>
    public CheckConfigElement()
      : base(false)
    {
    }

    /// <inheritdoc />
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HealthCheckEnabledCaption")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>Gets or sets the friendly name for the Health check.</summary>
    /// <value>The Health check name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HealthCheckNamePropNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("timeoutSeconds", DefaultValue = 30)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckTimeoutSecondsDescription", Title = "HealthCheckTimeoutSecondsCaption")]
    public int TimeoutSeconds
    {
      get => (int) this["timeoutSeconds"];
      set => this["timeoutSeconds"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("critical", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckCriticalDescription", Title = "HealthCheckCriticalCaption")]
    public bool Critical
    {
      get => (bool) this["critical"];
      set => this["critical"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("groups")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckGroupsDescription", Title = "HealthCheckGroupsCaption")]
    public string Groups
    {
      get => (string) this["groups"];
      set => this["groups"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("type")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HealthCheckTypeDescription", Title = "HealthCheckTypeCaption")]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Used for unit tests.")]
    [ConfigurationProperty("parameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HealthCheckParametersCaption")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"] ?? new NameValueCollection();
      set => this["parameters"] = (object) value;
    }
  }
}
