// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.MediaQueryRuleElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration
{
  /// <summary>
  /// Configuration element that represents a single media query rule.
  /// </summary>
  [DataContract]
  public class MediaQueryRuleElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public MediaQueryRuleElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.MediaQueryRuleElement" /> class.
    /// </summary>
    internal MediaQueryRuleElement()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets the name of the device type for which the rule applies.
    /// </summary>
    [DataMember]
    public string DeviceTypeName
    {
      get => ((DeviceTypeElement) this.Parent.Parent).Name;
      set => ((DeviceTypeElement) this.Parent.Parent).Name = value;
    }

    /// <summary>Gets or sets the id of the media query rule element.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "RuleDescriptionConfig")]
    [ConfigurationProperty("description", IsKey = true, IsRequired = true)]
    [DataMember]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>Gets or sets the type of the width</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "WidthTypeConfig")]
    [ConfigurationProperty("widthType")]
    [DataMember]
    public SizeType WidthType
    {
      get
      {
        object obj = this["widthType"];
        return obj == null ? SizeType.Exact : (SizeType) obj;
      }
      set => this["widthType"] = (object) value;
    }

    /// <summary>Gets or sets the exact width.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ExactWidthConfig")]
    [ConfigurationProperty("exactWidth")]
    [DataMember]
    public int? ExactWidth
    {
      get => (int?) this["exactWidth"];
      set => this["exactWidth"] = (object) value;
    }

    /// <summary>Gets or sets the minimum width.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "MinWidthConfig")]
    [ConfigurationProperty("minWidth")]
    [DataMember]
    public int? MinWidth
    {
      get => (int?) this["minWidth"];
      set => this["minWidth"] = (object) value;
    }

    /// <summary>Gets or sets the maximum width.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "MaxWidthConfig")]
    [ConfigurationProperty("maxWidth")]
    [DataMember]
    public int? MaxWidth
    {
      get => (int?) this["maxWidth"];
      set => this["maxWidth"] = (object) value;
    }

    /// <summary>Gets or sets the type of the height rule.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "HeightTypeConfig")]
    [ConfigurationProperty("heightType")]
    [DataMember]
    public SizeType HeightType
    {
      get
      {
        object obj = this["heightType"];
        return obj == null ? SizeType.Exact : (SizeType) obj;
      }
      set => this["heightType"] = (object) value;
    }

    /// <summary>Gets or sets the exact height.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ExactHeightConfig")]
    [ConfigurationProperty("exactHeight")]
    [DataMember]
    public int? ExactHeight
    {
      get => (int?) this["exactHeight"];
      set => this["exactHeight"] = (object) value;
    }

    /// <summary>Gets or sets the minimum height.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "MinHeightConfig")]
    [ConfigurationProperty("minHeight")]
    [DataMember]
    public int? MinHeight
    {
      get => (int?) this["minHeight"];
      set => this["minHeight"] = (object) value;
    }

    /// <summary>Gets or sets the maximum height.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "MaxHeightConfig")]
    [ConfigurationProperty("maxHeight")]
    [DataMember]
    public int? MaxHeight
    {
      get => (int?) this["maxHeight"];
      set => this["maxHeight"] = (object) value;
    }

    /// <summary>Gets or sets the orientation of the device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "OrientationConfig")]
    [ConfigurationProperty("orientation")]
    [DataMember]
    public DeviceOrientation Orientation
    {
      get
      {
        object obj = this["orientation"];
        return obj == null ? DeviceOrientation.Both : (DeviceOrientation) obj;
      }
      set => this["orientation"] = (object) value;
    }

    /// <summary>Gets or sets the aspect ratio of the device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "AspectRatioConfig")]
    [ConfigurationProperty("aspectRatio")]
    [DataMember]
    public string AspectRatio
    {
      get => (string) this["aspectRatio"];
      set => this["aspectRatio"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value which indicates weather the display of the
    /// device is monochrome. If true device is monochrome; otherwise device
    /// is not monochrome.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "IsMonochromeConfig")]
    [ConfigurationProperty("isMonochrome", DefaultValue = false)]
    [DataMember]
    public bool IsMonochrome
    {
      get => (bool) this["isMonochrome"];
      set => this["isMonochrome"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value which indicates weather the display is in grid mode.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "IsGridConfig")]
    [ConfigurationProperty("isGrid", DefaultValue = false)]
    [DataMember]
    public bool IsGrid
    {
      get => (bool) this["isGrid"];
      set => this["isGrid"] = (object) value;
    }

    /// <summary>Gets or sets the resolution of the device.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ResolutionConfig")]
    [ConfigurationProperty("resolution")]
    [DataMember]
    public string Resolution
    {
      get => (string) this["resolution"];
      set => this["resolution"] = (object) value;
    }

    /// <summary>Gets or sets the resulting rule.</summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ResultingRuleConfig")]
    [ConfigurationProperty("mediaQueryRule")]
    [DataMember]
    public string MediaQueryRule
    {
      get => (string) this["mediaQueryRule"];
      set => this["mediaQueryRule"] = (object) value;
    }

    /// <summary>
    /// Defines all the property names of the propeties used in this configuration element.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Description = "description";
      public const string WidthType = "widthType";
      public const string ExactWidth = "exactWidth";
      public const string MinWidth = "minWidth";
      public const string MaxWidth = "maxWidth";
      public const string HeightType = "heightType";
      public const string ExactHeight = "exactHeight";
      public const string MinHeight = "minHeight";
      public const string MaxHeight = "maxHeight";
      public const string Orientation = "orientation";
      public const string AspectRatio = "aspectRatio";
      public const string IsMonochrome = "isMonochrome";
      public const string IsGrid = "isGrid";
      public const string Resolution = "resolution";
      public const string MediaQueryRule = "mediaQueryRule";
    }
  }
}
