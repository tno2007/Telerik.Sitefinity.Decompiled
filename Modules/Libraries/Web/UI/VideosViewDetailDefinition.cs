// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective detail view control.
  /// </summary>
  public class VideosViewDetailDefinition : 
    MediaContentDetailDefinition,
    IVideosViewDetailDefinition,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    private bool? showEmbeddingOption;
    private bool? showRelatedVideos;
    private bool? allowFullSize;
    private bool? fullScreen;
    private bool? autoPlay;
    private int? startVolume;
    private int? startTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewDetailDefinition" /> class.
    /// </summary>
    public VideosViewDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public VideosViewDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public VideosViewDetailDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets a value indicating whether the option for embedding will be shown.
    /// </summary>
    /// <value></value>
    public bool? ShowEmbeddingOption
    {
      get => this.ResolveProperty<bool?>(nameof (ShowEmbeddingOption), this.showEmbeddingOption);
      set => this.showEmbeddingOption = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the related videos will be shown.
    /// </summary>
    /// <value>The show related videos.</value>
    public bool? ShowRelatedVideos
    {
      get => this.ResolveProperty<bool?>(nameof (ShowRelatedVideos), this.showRelatedVideos);
      set => this.showRelatedVideos = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the full size of the video player is allowed.
    /// </summary>
    /// <value></value>
    public bool? AllowFullSize
    {
      get => this.ResolveProperty<bool?>(nameof (AllowFullSize), this.allowFullSize);
      set => this.allowFullSize = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be playe in full screen mode.
    /// </summary>
    /// <value></value>
    public bool? FullScreen
    {
      get => this.ResolveProperty<bool?>(nameof (FullScreen), this.fullScreen);
      set => this.fullScreen = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    public bool? AutoPlay
    {
      get => this.ResolveProperty<bool?>(nameof (AutoPlay), this.autoPlay);
      set => this.autoPlay = value;
    }

    /// <summary>Gets or sets the start volume of the video player.</summary>
    public int? StartVolume
    {
      get => this.ResolveProperty<int?>(nameof (StartVolume), this.startVolume);
      set => this.startVolume = value;
    }

    /// <summary>Gets or sets the start time of the video player.</summary>
    public int? StartTime
    {
      get => this.ResolveProperty<int?>(nameof (StartTime), this.startTime);
      set => this.startTime = value;
    }
  }
}
