// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IVideosViewMasterLightBoxDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Declares the contract for the base VideosViewMasterLightBox. Implemented by each master view and the configuration element for the view
  /// </summary>
  public interface IVideosViewMasterLightBoxDefinition : 
    IVideosViewMasterDefinition,
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    bool? AutoPlay { get; set; }
  }
}
