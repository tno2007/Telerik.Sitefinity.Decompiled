// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PageControlElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// 
  /// </summary>
  public class PageControlElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PageControlElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the type.</summary>
    /// <value>The type.</value>
    [ConfigurationProperty("type")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type Type
    {
      get => (Type) this["type"];
      set => this["type"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the programmatic name of the toolbox item.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("placeHolder", DefaultValue = "Body")]
    public string PlaceHolder
    {
      get => (string) this["placeHolder"];
      set => this["placeHolder"] = (object) value;
    }

    /// <summary>Gets the collection of properties of the control.</summary>
    /// <value>The properties.</value>
    [ConfigurationProperty("properties")]
    public virtual ConfigElementDictionary<string, ControlPropertyElement> Properties => (ConfigElementDictionary<string, ControlPropertyElement>) this["properties"];
  }
}
