// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.DetailSimpleView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>Represents the video in detail mode.</summary>
  [ControlTemplateInfo("LibrariesResources", "VideosDetailViewFriendlyName", "VideosTitle")]
  public class DetailSimpleView : BaseDetailSimpleView<Video>
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.DetailSimpleView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.DetailSimpleView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DetailSimpleView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the option for embedding will be shown.
    /// </summary>
    private bool? ShowEmbeddingOption { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the related videos will be shown.
    /// </summary>
    /// <value>The show related videos.</value>
    private bool? ShowRelatedVideos { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the full size of the video player is allowed.
    /// </summary>
    private bool? AllowFullSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be playe in full screen mode.
    /// </summary>
    private bool? FullScreen { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    private bool? AutoPlay { get; set; }

    /// <summary>Gets or sets the start volume of the video player.</summary>
    private int? StartVolume { get; set; }

    /// <summary>Gets or sets the start time of the video player.</summary>
    private int? StartTime { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    /// <param name="definition">The definition.</param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      base.InitializeControls(container, definition);
      IVideosViewDetailDefinition detailDefinition = definition as IVideosViewDetailDefinition;
      this.ShowEmbeddingOption = detailDefinition.ShowEmbeddingOption;
      this.ShowRelatedVideos = detailDefinition.ShowRelatedVideos;
      this.AllowFullSize = detailDefinition.AllowFullSize;
      this.FullScreen = detailDefinition.FullScreen;
      this.AutoPlay = detailDefinition.AutoPlay;
      this.StartVolume = detailDefinition.StartVolume;
      this.StartTime = detailDefinition.StartTime;
    }

    /// <summary>Configures the detail control.</summary>
    /// <param name="listViewItem">The list view item.</param>
    protected override void ConfigureDetailControl(RadListViewItem listViewItem)
    {
      MediaPlayerControl control = (MediaPlayerControl) listViewItem.FindControl("videoControl");
      if (control == null || !(listViewItem is RadListViewDataItem listViewDataItem) || !(listViewDataItem.DataItem is Video dataItem))
        return;
      control.MediaUrl = dataItem.MediaUrl;
      control.MediaTitle = (string) dataItem.Title;
      control.MediaDescription = (string) dataItem.Description;
      control.AutoPlay = this.AutoPlay.Value;
      control.SetSilverlightContainerVisibility = false;
      control.StartTime = this.StartTime.Value;
      control.FullScreen = this.FullScreen.Value;
      int? startVolume = this.StartVolume;
      if (startVolume.Value < 0)
      {
        control.StartVolume = 0;
      }
      else
      {
        startVolume = this.StartVolume;
        if (startVolume.Value > 100)
        {
          control.StartVolume = 100;
        }
        else
        {
          MediaPlayerControl mediaPlayerControl = control;
          startVolume = this.StartVolume;
          int num = startVolume.Value;
          mediaPlayerControl.StartVolume = num;
        }
      }
    }
  }
}
