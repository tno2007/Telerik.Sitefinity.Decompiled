// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.IImageProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>Defines the image processor interface.</summary>
  public interface IImageProcessor
  {
    /// <summary>
    /// Generates a new image from the given Image using the given method and arguments.
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <param name="methodName">The name of the method of the image processor that will be called to process the image.</param>
    /// <returns></returns>
    Image ProcessImage(Image sourceImage, string methodName, object methodArgument = null);

    /// <summary>
    /// Resizes the specified source image by changing one of the sides (width or height) to the desired size and the other side proportionally (keeping the ratio).
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <param name="args">The arguments for the resize operation.</param>
    /// <returns></returns>
    Image Resize(Image sourceImage, FitToSideArguments args);

    /// <summary>Gets the supported methods for image processing.</summary>
    /// <value>The methods.</value>
    IDictionary<string, ImageProcessingMethod> Methods { get; }
  }
}
