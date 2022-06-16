// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IVideosViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>Base interface for videos view detail</summary>
  public interface IVideosViewDetailDefinition : 
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the option for embedding will be shown.
    /// </summary>
    bool? ShowEmbeddingOption { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the related videos will be shown.
    /// </summary>
    /// <value>The show related videos.</value>
    bool? ShowRelatedVideos { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the full size of the video player is allowed.
    /// </summary>
    bool? AllowFullSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be playe in full screen mode.
    /// </summary>
    bool? FullScreen { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    bool? AutoPlay { get; set; }

    /// <summary>Gets or sets the start volume of the video player.</summary>
    int? StartVolume { get; set; }

    /// <summary>Gets or sets the start time of the video player.</summary>
    int? StartTime { get; set; }
  }
}
