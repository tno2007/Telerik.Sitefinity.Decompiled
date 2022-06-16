// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.PreviewDeviceElement
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
  /// Configuration element which defines a single preview device.
  /// </summary>
  public class PreviewDeviceElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PreviewDeviceElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.PreviewDeviceElement" /> class.
    /// </summary>
    internal PreviewDeviceElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the preview device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceNameConfig")]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the title of the preview device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceTitleConfig")]
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the css class associated with this preview device.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceCssClassConfig")]
    [ConfigurationProperty("cssClass")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>Gets or sets the device width of the preview device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceWidthConfig")]
    [ConfigurationProperty("deviceWidth")]
    public int DeviceWidth
    {
      get => (int) this["deviceWidth"];
      set => this["deviceWidth"] = (object) value;
    }

    /// <summary>Gets or sets the height of the preview device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceHeightConfig")]
    [ConfigurationProperty("deviceHeight")]
    public int DeviceHeight
    {
      get => (int) this["deviceHeight"];
      set => this["deviceHeight"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the width of the viewport of the preview device.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceViewportWidthConfig")]
    [ConfigurationProperty("viewportWidth")]
    public int ViewportWidth
    {
      get => (int) this["viewportWidth"];
      set => this["viewportWidth"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the height of the viewport of the preview device.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceViewportHeightConfig")]
    [ConfigurationProperty("viewportHeight")]
    public int ViewportHeight
    {
      get => (int) this["viewportHeight"];
      set => this["viewportHeight"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the X offset of the viewport in the portrait mode.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceOffsetXConfig")]
    [ConfigurationProperty("offsetX")]
    public int OffsetX
    {
      get => (int) this["offsetX"];
      set => this["offsetX"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the Y offset of the viewport in the portrait mode.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceOffsetYConfig")]
    [ConfigurationProperty("offsetY")]
    public int OffsetY
    {
      get => (int) this["offsetY"];
      set => this["offsetY"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the X offset of the viewport in the ladscape mode.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceOffsetXLandscapeConfig")]
    [ConfigurationProperty("offsetXLandscape")]
    public int OffsetXLandscape
    {
      get => (int) this["offsetXLandscape"];
      set => this["offsetXLandscape"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the Y offset of the viewport in the landscape mode.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceOffsetYLandscapeConfig")]
    [ConfigurationProperty("offsetYLandscape")]
    public int OffsetYLandscape
    {
      get => (int) this["offsetYLandscape"];
      set => this["offsetYLandscape"] = (object) value;
    }

    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDeviceCategoryConfig")]
    [ConfigurationProperty("deviceCategory", IsRequired = true)]
    public string DeviceCategory
    {
      get => (string) this["deviceCategory"];
      set => this["deviceCategory"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Name = "name";
      public const string Title = "title";
      public const string CssClass = "cssClass";
      public const string DeviceWidth = "deviceWidth";
      public const string DeviceHeight = "deviceHeight";
      public const string ViewportWidth = "viewportWidth";
      public const string ViewportHeight = "viewportHeight";
      public const string OffsetX = "offsetX";
      public const string OffsetY = "offsetY";
      public const string OffsetXLandscape = "offsetXLandscape";
      public const string OffsetYLandscape = "offsetYLandscape";
      public const string DeviceCategory = "deviceCategory";
    }
  }
}
