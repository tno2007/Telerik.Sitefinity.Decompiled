// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryRuleViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>View model class for the media query rule type.</summary>
  [DataContract]
  public class MediaQueryRuleViewModel
  {
    /// <summary>Gets or sets the id of the parent media query.</summary>
    [DataMember]
    public Guid ParentMediaQueryId { get; set; }

    /// <summary>Gets or sets the name of the device type.</summary>
    [DataMember]
    public string DeviceTypeName { get; set; }

    /// <summary>Gets or sets the description of the media query rule.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the type of the width.</summary>
    [DataMember]
    public SizeType WidthType { get; set; }

    /// <summary>Gets or sets the exact width.</summary>
    [DataMember]
    public string ExactWidth { get; set; }

    /// <summary>Gets or sets the minimum width.</summary>
    [DataMember]
    public string MinWidth { get; set; }

    /// <summary>Gets or sets the maximum width.</summary>
    [DataMember]
    public string MaxWidth { get; set; }

    /// <summary>Gets or sets the type of the height.</summary>
    [DataMember]
    public SizeType HeightType { get; set; }

    /// <summary>Gets or sets the exact height.</summary>
    [DataMember]
    public string ExactHeight { get; set; }

    /// <summary>Gets or sets the minimum height.</summary>
    [DataMember]
    public string MinHeight { get; set; }

    /// <summary>Gets or sets the maximum height.</summary>
    [DataMember]
    public string MaxHeight { get; set; }

    /// <summary>Gets or sets the device orientation.</summary>
    [DataMember]
    public DeviceOrientation Orientation { get; set; }

    /// <summary>Gets or sets the aspect ratio.</summary>
    [DataMember]
    public string AspectRatio { get; set; }

    /// <summary>Gets or sets the resolution.</summary>
    [DataMember]
    public string Resolution { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the screen is grid.
    /// </summary>
    [DataMember]
    public bool IsGrid { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the screen is monochrome.
    /// </summary>
    [DataMember]
    public bool IsMonochrome { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the media query has been composed manually.
    /// </summary>
    [DataMember]
    public bool IsManualMediaQuery { get; set; }

    /// <summary>Gets or sets the value of the media query rule.</summary>
    [DataMember]
    public string MediaQueryRule { get; set; }
  }
}
