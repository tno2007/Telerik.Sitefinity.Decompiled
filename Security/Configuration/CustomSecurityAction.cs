// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.CustomSecurityAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  public class CustomSecurityAction : ConfigElement
  {
    public CustomSecurityAction(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the action.</summary>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the name of the action.</summary>
    [ConfigurationProperty("showActionInList", IsKey = false, IsRequired = true)]
    public bool ShowActionInList
    {
      get => (bool) this["showActionInList"];
      set => this["showActionInList"] = (object) value;
    }

    [ConfigurationProperty("title", DefaultValue = "", IsKey = false, IsRequired = false)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    [ConfigurationProperty("resourceClassId", DefaultValue = "", IsKey = false, IsRequired = false)]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }
  }
}
