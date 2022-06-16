// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.Profile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>The profile configuration section</summary>
  public class Profile : ConfigElement
  {
    /// <summary>Constructor</summary>
    /// <param name="parent">Parent element in the configuration</param>
    public Profile(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Provider name section</summary>
    [ConfigurationProperty("providerName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Properties of the configuration element: a dictionary of ProfileProperty objects
    /// </summary>
    [ConfigurationProperty("properties")]
    [ConfigurationCollection(typeof (ConfigElementDictionary<string, ProfileProperty>), AddItemName = "add")]
    public ConfigElementDictionary<string, ProfileProperty> Properties => (ConfigElementDictionary<string, ProfileProperty>) this["properties"];
  }
}
