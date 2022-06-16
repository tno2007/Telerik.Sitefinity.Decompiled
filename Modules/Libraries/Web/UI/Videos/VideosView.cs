// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.VideosView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>
  /// This control acts as a container for the views that display videos.
  /// </summary>
  [RequireScriptManager]
  [ControlDesigner(typeof (VideoGalleryDesigner))]
  [PropertyEditorTitle(typeof (LibrariesResources), "VideoGalleryPropertyEditorTitle")]
  public class VideosView : MediaContentView, IBrowseAndEditable
  {
    protected BrowseAndEditToolbar browseAndEditToolBar;
    protected List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();

    /// <summary>
    /// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
    /// </summary>
    /// <value>The name of the module.</value>
    public override string ModuleName
    {
      get => string.IsNullOrEmpty(base.ModuleName) ? "Libraries" : base.ModuleName;
      set => base.ModuleName = value;
    }

    /// <summary>
    /// Gets or sets the name of the configuration definition for the whole control. From this definition
    /// control can find out all other configurations needed in order to construct views.
    /// </summary>
    /// <value>The name of the control definition.</value>
    public override string ControlDefinitionName
    {
      get => string.IsNullOrEmpty(base.ControlDefinitionName) ? "VideosFrontend" : base.ControlDefinitionName;
      set => base.ControlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the detail view to be loaded when
    /// control is in the ContentViewDisplayMode.Detail
    /// </summary>
    /// <value></value>
    public override string DetailViewName
    {
      get => string.IsNullOrEmpty(base.DetailViewName) ? "VideosDetailView" : base.DetailViewName;
      set => base.DetailViewName = value;
    }

    /// <summary>
    /// Gets or sets the name of the master view to be loaded when
    /// control is in the ContentViewDisplayMode.Master
    /// </summary>
    /// <value></value>
    public override string MasterViewName
    {
      get => string.IsNullOrEmpty(base.MasterViewName) ? "VideosFrontendThumbnailsList" : base.MasterViewName;
      set => base.MasterViewName = value;
    }

    /// <summary>
    /// Gets or sets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public override string EmptyLinkText => Res.Get<VideosResources>().EditVideoGallerySettings;

    /// <summary>Gets the browse and edit toolbar.</summary>
    /// <value>The browse and edit toolbar.</value>
    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.browseAndEditToolBar;

    /// <summary>
    /// Adds browse and edit commands to be executed by the toolbar
    /// </summary>
    /// <param name="commands">The commands.</param>
    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }

    /// <summary>Configures the current view control.</summary>
    /// <param name="control">The current view control.</param>
    protected override void ConfigureViewControl(Control control)
    {
      if (!SystemManager.IsBrowseAndEditMode || !(control is IBrowseAndEditable))
        return;
      ((IBrowseAndEditable) control).BrowseAndEditableInfo = this.BrowseAndEditableInfo;
    }

    /// <inheritdoc />
    public bool? AutoPlay
    {
      get => ((IVideosViewDetailDefinition) this.DetailViewDefinition).AutoPlay;
      set => ((IVideosViewDetailDefinition) this.DetailViewDefinition).AutoPlay = value;
    }

    /// <inheritdoc />
    public int? StartVolume
    {
      get => ((IVideosViewDetailDefinition) this.DetailViewDefinition).StartVolume;
      set => ((IVideosViewDetailDefinition) this.DetailViewDefinition).StartVolume = value;
    }

    /// <inheritdoc />
    public int? StartTime
    {
      get => ((IVideosViewDetailDefinition) this.DetailViewDefinition).StartTime;
      set => ((IVideosViewDetailDefinition) this.DetailViewDefinition).StartTime = value;
    }

    /// <inheritdoc />
    public bool? FullScreen
    {
      get => ((IVideosViewDetailDefinition) this.DetailViewDefinition).FullScreen;
      set => ((IVideosViewDetailDefinition) this.DetailViewDefinition).FullScreen = value;
    }
  }
}
