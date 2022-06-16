// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.NavigationTransformationElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration
{
  /// <summary>
  /// Represents a configuration element that describes a transformation of a navigation control.
  /// </summary>
  public class NavigationTransformationElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public NavigationTransformationElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.NavigationTransformationElement" /> class.
    /// </summary>
    internal NavigationTransformationElement()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets the name of the navigation transformation element.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "NavigationTransformationNameConfig")]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the css for the control transformation.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "TransformationCssConfig")]
    [ConfigurationProperty("transformationCss")]
    public string TransformationCss
    {
      get => (string) this["transformationCss"];
      set => this["transformationCss"] = (object) value;
    }

    /// <summary>Gets or sets the title.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "NavigationTransformationTitleConfig")]
    [ConfigurationProperty("title", IsRequired = true)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the resource class that is to be used for localization of the Title property.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "NavigationTransformationResourceClassIdConfig")]
    [ConfigurationProperty("resourceClassId")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets or sets whether the transformation is active.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "NavigationTransformationIsActiveConfig")]
    [ConfigurationProperty("isActive", DefaultValue = true)]
    public bool IsActive
    {
      get => (bool) this["isActive"];
      set => this["isActive"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Name = "name";
      public const string TransformationCss = "transformationCss";
      public const string Title = "title";
      public const string ResourceClassId = "resourceClassId";
      public const string IsActive = "isActive";
    }
  }
}
