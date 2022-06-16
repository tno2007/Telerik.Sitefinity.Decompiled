// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.FitToSideArguments
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// Defines the Resize (fit-to-side) method argument for specifying the desired resizing options.
  /// </summary>
  public class FitToSideArguments
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.FitToSideArguments" /> class and sets the quality to medium.
    /// </summary>
    public FitToSideArguments() => this.Quality = ImageQuality.Medium;

    /// <summary>Gets or sets the size of the resized side.</summary>
    /// <value>The size.</value>
    [ImageProcessingProperty(IsRequired = true, RegularExpression = "^[1-9][0-9]{0,3}$", RegularExpressionViolationMessage = "ValueMustBeInteger", ResourceClassId = "LibrariesResources")]
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets if the bigger or the smaller image side should be resized.
    /// </summary>
    /// <value>The resize bigger side.</value>
    [ImageProcessingProperty(ResourceClassId = "LibrariesResources", Title = "ResizeBiggerSide")]
    public bool ResizeBiggerSide { get; set; }

    /// <summary>
    /// If set to true and the desired size is greater than the initial it will generate a bigger image.
    /// Otherwise no bigger images will be delivered.
    /// </summary>
    /// <value>The scale up.</value>
    [ImageProcessingProperty(ResourceClassId = "LibrariesResources", Title = "ScaleUp")]
    public bool ScaleUp { get; set; }

    /// <summary>Gets or sets the quality.</summary>
    /// <value>The quality.</value>
    [ImageProcessingProperty]
    public ImageQuality Quality { get; set; }
  }
}
