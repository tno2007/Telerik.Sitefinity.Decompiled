// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.CultureSettingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  public class CultureSettingElement : ConfigElement, ICultureSetting
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:CultureElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CultureSettingElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal CultureSettingElement()
      : base(false)
    {
    }

    [ConfigurationProperty("cultureKey", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string CultureKey
    {
      get => (string) this["cultureKey"];
      set => this["cultureKey"] = (object) value;
    }

    [ConfigurationProperty("setting", DefaultValue = "")]
    public virtual string Setting
    {
      get => (string) this["setting"];
      set => this["setting"] = (object) value;
    }
  }
}
