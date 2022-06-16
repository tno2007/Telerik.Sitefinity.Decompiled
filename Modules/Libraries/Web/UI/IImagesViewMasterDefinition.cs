﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IImagesViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Declares the contract for the base ImagesViewMaster. Implemented by each master view and the configuration element for the view
  /// </summary>
  public interface IImagesViewMasterDefinition : 
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition,
    IImagesViewDefinition
  {
    /// <summary>Height of the thumbnails.</summary>
    int? ThumbnailsHeight { get; set; }

    /// <summary>The height of the canvas in thumbnail strip view</summary>
    /// <value>The height of the canvas.</value>
    int? CanvasHeight { get; set; }
  }
}
