// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.LayoutElement
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
  /// Represents a layout element that can participate in the
  /// transformations of the responsive design module.
  /// </summary>
  public class LayoutElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LayoutElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.LayoutElement" /> class.
    /// </summary>
    internal LayoutElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the layout element.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "LayoutElementNameConfig")]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the group name of the layout element.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "LayoutElementGroupNameConfig")]
    [ConfigurationProperty("groupName", IsRequired = true)]
    public string GroupName
    {
      get => (string) this["groupName"];
      set => this["groupName"] = (object) value;
    }

    /// <summary>Gets or sets the title of the layout element.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "LayoutElementTitleConfig")]
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the class used to localize the labels of this class.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ResourceClassNameConfig")]
    [ConfigurationProperty("resourceClassName")]
    public string ResourceClassName
    {
      get => (string) this["resourceClassName"];
      set => this["resourceClassName"] = (object) value;
    }

    /// <summary>Gets or sets the css for the layout element.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "LayoutCssConfig")]
    [ConfigurationProperty("layoutCss")]
    public string LayoutCss
    {
      get => (string) this["layoutCss"];
      set => this["layoutCss"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Name = "name";
      public const string GroupName = "groupName";
      public const string Title = "title";
      public const string ResourceClassName = "resourceClassName";
      public const string LayoutCss = "layoutCss";
    }
  }
}
