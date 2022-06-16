// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingError
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// Contains the specific error reason for the image processing error.
  /// </summary>
  public enum ImageProcessingError
  {
    /// <summary>The generation method could not be found.</summary>
    MethodNotFound,
    /// <summary>
    /// Internal image processing error. The method for generation is executed and throws an error.
    /// </summary>
    MethodGenerationError,
  }
}
