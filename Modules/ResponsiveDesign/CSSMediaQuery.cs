// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.CSSMediaQuery
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>This class represents the structured css media query.</summary>
  public class CSSMediaQuery
  {
    /// <summary>Gets or sets the width property.</summary>
    public int? Width { get; set; }

    /// <summary>Gets or sets the minimum width property.</summary>
    public int? MinWidth { get; set; }

    /// <summary>Gets or sets the maximum width property.</summary>
    public int? MaxWidth { get; set; }

    /// <summary>Gets or sets the height property.</summary>
    public int? Height { get; set; }

    /// <summary>Gets or sets the minimum height property.</summary>
    public int? MinHeight { get; set; }

    /// <summary>Gets or sets the maximum height property.</summary>
    public int? MaxHeight { get; set; }

    /// <summary>Gets or sets the device width property.</summary>
    public int? DeviceWidth { get; set; }

    /// <summary>Gets or sets the device min width property.</summary>
    public int? MinDeviceWidth { get; set; }

    /// <summary>Gets or sets the device max width property.</summary>
    public int? MaxDeviceWidth { get; set; }

    /// <summary>Gets or sets the device height property.</summary>
    public int? DeviceHeight { get; set; }

    /// <summary>Gets or sets the min device height property.</summary>
    public int? MinDeviceHeight { get; set; }

    /// <summary>Gets or sets the max device height property.</summary>
    public int? MaxDeviceHeight { get; set; }

    /// <summary>Gets or sets the orientation of the device property.</summary>
    public string Orientation { get; set; }

    /// <summary>Gets or sets the aspect ratio of the device property.</summary>
    public string AspectRatio { get; set; }

    /// <summary>Gets or sets the minimum aspect ratio.</summary>
    public string MinAspectRatio { get; set; }

    /// <summary>Gets or sets the max aspect ratio.</summary>
    public string MaxAspectRatio { get; set; }

    /// <summary>Gets or sets the device aspect ratio.</summary>
    public string DeviceAspectRatio { get; set; }

    /// <summary>Gets or sets the minimum device aspect ratio.</summary>
    public string MinDeviceAspectRatio { get; set; }

    /// <summary>Gets or sets the maximum device aspect ratio.</summary>
    public string MaxDeviceAspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather device is monochrome.
    /// </summary>
    public bool? Monochrome { get; set; }

    /// <summary>Gets or sets the min monochrome property.</summary>
    public int? MinMonochrome { get; set; }

    /// <summary>Gets or sets the max monochrome property.</summary>
    public int? MaxMonochrome { get; set; }

    /// <summary>Gets or sets the resolution property.</summary>
    public int? Resolution { get; set; }

    /// <summary>Gets or sets the min resolution property.</summary>
    public int? MinResolution { get; set; }

    /// <summary>Gets or sets the max resolution property.</summary>
    public int? MaxResolution { get; set; }

    /// <summary>Gets or sets the grid property.</summary>
    public bool? IsGrid { get; set; }
  }
}
