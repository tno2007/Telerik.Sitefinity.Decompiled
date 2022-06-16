// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.ProfileProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Profile Property class</summary>
  public class ProfileProperty : ConfigElement
  {
    /// <summary>Constructor</summary>
    /// <param name="parent">The parent element</param>
    public ProfileProperty(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Profile property name attribute</summary>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Profile property type attribute</summary>
    [ConfigurationProperty("type", DefaultValue = "")]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }
  }
}
