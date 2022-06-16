// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PresentationElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Represents a configuration element for presentation data.
  /// </summary>
  public class PresentationElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.PresentationElement" /> class.
    /// </summary>
    public PresentationElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.PresentationElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public PresentationElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the programmatic name of the configuration element.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [ConfigurationProperty("theme", DefaultValue = "")]
    public string Theme
    {
      get => (string) this["theme"];
      set => this["theme"] = (object) value;
    }

    [ConfigurationProperty("dataType", DefaultValue = "")]
    public string DataType
    {
      get => (string) this["dataType"];
      set => this["dataType"] = (object) value;
    }

    [ConfigurationProperty("data", DefaultValue = "")]
    public string Data
    {
      get => (string) this["data"];
      set => this["data"] = (object) value;
    }
  }
}
