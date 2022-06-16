// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IMultiImageFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of multi image field element.
  /// </summary>
  public interface IMultiImageFieldDefinition
  {
    /// <summary>
    /// Gets or sets the maximal width in pixels of the image in the  multi image field.
    /// </summary>
    /// <value>The maximal width.</value>
    int? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the maximal height in pixels of the image in the multi image field.
    /// </summary>
    /// <value>The maximum height.</value>
    int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider used to retrieve the default image
    /// </summary>
    /// <value>The name of the provider for the default image</value>
    string ProviderNameForDefaultImage { get; set; }

    /// <summary>Gets or sets the id if the default image</summary>
    /// <value>The default image id.</value>
    Guid DefaultImageId { get; set; }

    /// <summary>
    /// Gets or sets the name of the image provider used for getting and uploading of a image
    /// </summary>
    /// <value>The name of the image data provider.</value>
    string ImageProviderName { get; set; }

    /// <summary>
    /// Represents the type of the field to which the field control is bound
    /// <remarks>
    /// Used to specify what type of value to control should return both on the server and client sides
    /// </remarks>
    /// </summary>
    Type DataFieldType { get; set; }

    /// <summary>Gets or sets the default src for the image.</summary>
    /// <value>The default src for the image.</value>
    string DefaultSrc { get; set; }

    /// <summary>
    /// Gets or sets whether to display the image placeholder. The default behavior is to display the placeholder.
    /// </summary>
    /// <value>Gets or sets whether to display the image placeholder.</value>
    bool DisplayEmptyImage { get; set; }
  }
}
