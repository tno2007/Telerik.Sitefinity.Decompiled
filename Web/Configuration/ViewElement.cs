// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>Defines settings for views.</summary>
  public class ViewElement : ConfigElement
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ViewElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name of the view. Name is required for User Controls.
    /// </summary>
    [ConfigurationProperty("name", DefaultValue = "", IsRequired = false)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets a title for the veiw.</summary>
    [ConfigurationProperty("title", DefaultValue = "")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets descripton of the view.</summary>
    [ConfigurationProperty("description", DefaultValue = "")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>Gets or set virtual path to the view.</summary>
    [ConfigurationProperty("virtualPath", DefaultValue = "", IsRequired = false)]
    public string VirtualPath
    {
      get => (string) this["virtualPath"];
      set => this["virtualPath"] = (object) value;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("viewType", IsRequired = false)]
    public Type ViewType
    {
      get => (Type) this["viewType"];
      set => this["viewType"] = (object) value;
    }

    /// <summary>A type providing reference to embeded resource files.</summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("localizationAssemblyInfo", IsRequired = false)]
    public Type LocalizationAssemblyInfo
    {
      get => (Type) this["localizationAssemblyInfo"];
      set => this["localizationAssemblyInfo"] = (object) value;
    }
  }
}
