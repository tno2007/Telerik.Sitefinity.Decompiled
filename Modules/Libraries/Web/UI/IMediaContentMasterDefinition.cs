// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IMediaContentMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Declares the contract for the media content master view.
  /// </summary>
  public interface IMediaContentMasterDefinition : 
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    /// <summary>Gets or sets the width of the thumbnails.</summary>
    [Obsolete("Use ThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    int? ThumbnailsWidth { get; set; }

    /// <summary>Gets or sets the name of the thumbnails.</summary>
    /// <value>The name of the thumbnails.</value>
    string ThumbnailsName { get; set; }
  }
}
