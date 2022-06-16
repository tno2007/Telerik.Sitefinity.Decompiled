// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewMasterLightBoxDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective master view control.
  /// </summary>
  public class VideosViewMasterLightBoxDefinition : 
    VideosViewMasterDefinition,
    IVideosViewMasterLightBoxDefinition,
    IVideosViewMasterDefinition,
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    private bool? autoPlay;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewMasterLightBoxDefinition" /> class.
    /// </summary>
    public VideosViewMasterLightBoxDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.VideosViewMasterLightBoxDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public VideosViewMasterLightBoxDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns>The definition.</returns>
    public VideosViewMasterLightBoxDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    public bool? AutoPlay
    {
      get => this.ResolveProperty<bool?>(nameof (AutoPlay), this.autoPlay);
      set => this.autoPlay = value;
    }
  }
}
