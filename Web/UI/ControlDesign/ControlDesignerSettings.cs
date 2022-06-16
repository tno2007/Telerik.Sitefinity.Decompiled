// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlDesignerSettingsDescription", Title = "ControlDesignerSettingsCaption")]
  public class ControlDesignerSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ControlDesignerSettings(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("controlType", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string ControlType
    {
      get => (string) this["controlType"];
      set => this["controlType"] = (object) value;
    }

    [ConfigurationProperty("controlDesigner", DefaultValue = "", IsRequired = true)]
    public virtual string ControlDesigner
    {
      get => (string) this["controlDesigner"];
      set => this["controlDesigner"] = (object) value;
    }
  }
}
