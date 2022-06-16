// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IMediaContentDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// An interface that provides all common properties to construct the actual images or videos view.
  /// </summary>
  public interface IMediaContentDefinition
  {
    /// <summary>
    /// Gets or sets the width of the single item. The single item could be an image or video.
    /// </summary>
    [Obsolete("Use SingleItemThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    int? SingleItemWidth { get; set; }

    /// <summary>Gets or sets the name of the single item thumbnails.</summary>
    /// <value>The name of the single item thumbnails.</value>
    string SingleItemThumbnailsName { get; set; }
  }
}
