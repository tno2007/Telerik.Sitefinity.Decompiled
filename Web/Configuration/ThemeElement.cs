// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ThemeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration
{
  public class ThemeElement : ConfigElement
  {
    public ThemeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name of the view. Name is required for User Controls.
    /// </summary>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [ConfigurationProperty("path", DefaultValue = "", IsRequired = false)]
    public string Path
    {
      get => (string) this["path"];
      set => this["path"] = (object) value;
    }

    [ConfigurationProperty("namespace", DefaultValue = "", IsRequired = false)]
    public string Namespace
    {
      get => (string) this["namespace"];
      set => this["namespace"] = (object) value;
    }

    [ConfigurationProperty("assemblyInfo", DefaultValue = "", IsRequired = false)]
    public string AssemblyInfo
    {
      get => (string) this["assemblyInfo"];
      set => this["assemblyInfo"] = (object) value;
    }
  }
}
