// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingMethodAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// Specifies the image generation method and it's metadata.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public class ImageProcessingMethodAttribute : Attribute
  {
    public string Title { get; set; }

    public string ResourceClassId { get; set; }

    /// <summary>Gets or sets the label format.</summary>
    /// <value>The label format.</value>
    public string LabelFormat { get; set; }

    public string DescriptionText { get; set; }

    public string DescriptionImageResourceName { get; set; }

    public string ValidateArgumentsMethodName { get; set; }
  }
}
