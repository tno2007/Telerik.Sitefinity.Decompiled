// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SitefinityTestingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  public class SitefinityTestingElement : ConfigurationElement
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Configuration.SitefinityTestingElement" /> is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enabled", DefaultValue = false)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value inidicationg whether load balancing synchronization
    /// messages/operations should be logged, when <see cref="P:Telerik.Sitefinity.Configuration.SitefinityTestingElement.Enabled" /> is <c>true</c>.
    /// </summary>
    [ConfigurationProperty("loadBalancingSyncLoggingEnabled", DefaultValue = false)]
    public bool LoadBalancingSyncLoggingEnabled
    {
      get => (bool) this["loadBalancingSyncLoggingEnabled"];
      set => this["loadBalancingSyncLoggingEnabled"] = (object) value;
    }
  }
}
