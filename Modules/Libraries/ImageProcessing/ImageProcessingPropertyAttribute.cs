// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingPropertyAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// Specifies the image processing argument for the transformation method.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class ImageProcessingPropertyAttribute : Attribute
  {
    /// <summary>
    /// Gets or sets the key resource to the title or actault title of the property displayed in the UI.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the units of the property displayed in the UI.
    /// </summary>
    /// <value>The units.</value>
    public string Units { get; set; }

    /// <summary>
    /// Gets or sets the resource class id of the resource to get the property Title.
    /// </summary>
    /// <value>The resource class id.</value>
    public string ResourceClassId { get; set; }

    /// <summary>
    /// Gets or sets wether the property is required to be set in the UI.
    /// </summary>
    /// <value>The is required.</value>
    public bool IsRequired { get; set; }

    /// <summary>Gets or sets the regular expression.</summary>
    public string RegularExpression { get; set; }

    /// <summary>
    /// Gets or sets the regular expression violation message.
    /// </summary>
    public string RegularExpressionViolationMessage { get; set; }
  }
}
