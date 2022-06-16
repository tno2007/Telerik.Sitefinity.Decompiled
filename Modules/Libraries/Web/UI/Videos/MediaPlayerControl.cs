// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos
{
  /// <summary>Public control for displaying rich media content.</summary>
  [ControlDesigner(typeof (SingleMediaContentItemDesigner))]
  [PropertyEditorTitle(typeof (VideosResources), "MediaPlayerControlPropertyEditorTitle")]
  [ControlTemplateInfo("LibrariesResources", "Html5MediaPlayerControlFriendlyName", "VideosTitle")]
  public class MediaPlayerControl : 
    SimpleScriptView,
    IBrowseAndEditable,
    IWidgetData,
    IRelatedDataView,
    IHasCacheDependency,
    IContentLocatableView
  {
    private string mediaMimeType;
    private bool setSilverlightContainerVisibility = true;
    private bool shouldDisplayPlayer = true;
    private bool? isModuleEnabledForRelatedData;
    private int startVolume = 50;
    private BrowseAndEditToolbar browseAndEditToolbar;
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    internal const string Html5TemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.Html5MediaPlayerControl.ascx";
    internal const string Html5TemplateFriendlyName = "Media Player HTML5";
    internal const string Html5TemplateId = "DB92F414-1C8F-4F43-ABFE-000000000005";
    public static readonly string Html5TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.Html5MediaPlayerControl.ascx");
    internal const string Script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaPlayerControl.js";
    internal Type TypeName = typeof (MediaPlayerControl);
    private LibrariesManager manager;
    private RelatedDataDefinition relatedDataDefinition;

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the URL of the media content item.</summary>
    /// <value>The MediaUrl.</value>
    public string MediaUrl { get; set; }

    /// <summary>Gets or sets the title of the media content item.</summary>
    /// <value>The MediaTitle.</value>
    public string MediaTitle { get; set; }

    /// <summary>
    /// Gets or sets the Description of the media content item.
    /// </summary>
    /// <value>The MediaDescription.</value>
    public string MediaDescription { get; set; }

    /// <summary>
    /// Gets or sets whether the media content item to be autoplayed.
    /// </summary>
    /// <value>The Auto Play option for the player.</value>
    public bool AutoPlay { get; set; }

    /// <summary>Gets or sets the player start volume.</summary>
    /// <value>The start volume.</value>
    public int StartVolume
    {
      get => this.startVolume;
      set
      {
        if (value < 0)
          this.startVolume = 0;
        else if (value > 100)
          this.startVolume = 100;
        else
          this.startVolume = value;
      }
    }

    /// <summary>Gets or sets the player start time.</summary>
    /// <value>The start time.</value>
    public int StartTime { get; set; }

    /// <summary>
    /// Gets or sets whether the media content item will be played in full-screen mode.
    /// </summary>
    /// <value>The full screen.</value>
    public bool FullScreen { get; set; }

    /// <summary>Gets or sets the media content id.</summary>
    /// <value>The media content id.</value>
    public Guid MediaContentId { get; set; }

    /// <summary>
    /// Gets or sets the url of the YouTube video that should be played.
    /// </summary>
    /// <value>You tube video.</value>
    public string YouTubeVideoURL { get; set; }

    /// <summary>
    /// Gets or sets the ID of the YouTube playlist that should be played.
    /// </summary>
    /// <value>You tube playlist.</value>
    public string YouTubePlaylistID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to set silverlight container visibility.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if silverlight container visibility will be explicitly set; otherwise, <c>false</c>.
    /// </value>
    public bool SetSilverlightContainerVisibility
    {
      get => this.setSilverlightContainerVisibility;
      set => this.setSilverlightContainerVisibility = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MediaPlayerControl.Html5TemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the manager.</summary>
    public LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
        {
          try
          {
            this.manager = LibrariesManager.GetManager(this.ProviderName);
          }
          catch (MissingProviderConfigurationException ex)
          {
            this.manager = LibrariesManager.GetManager();
          }
        }
        return this.manager;
      }
    }

    /// <summary>
    /// Gets value indicating if current site has Libraries module enabled with current provider
    /// </summary>
    private bool IsModuleEnabledForRelatedData
    {
      get
      {
        if (!this.isModuleEnabledForRelatedData.HasValue)
          this.isModuleEnabledForRelatedData = !this.DisplayRelatedData() ? new bool?(true) : new bool?(this.IsModuleEnabledForCurrentSite());
        return this.isModuleEnabledForRelatedData.Value;
      }
    }

    /// <summary>
    /// Gets the reference to the HtmlGenericControl that contains the player container.
    /// </summary>
    protected internal virtual HtmlGenericControl PlayerContainer => this.Container.GetControl<HtmlGenericControl>("playerContainer", true);

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>("MediaPlayerBrowseAndEditToolbar", false);
        return this.browseAndEditToolbar;
      }
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    /// <summary>Gets the reference to the RadMediaPlayer control.</summary>
    protected virtual RadMediaPlayer MediaPlayer => this.Container.GetControl<RadMediaPlayer>("mediaPlayer", false);

    /// <summary>
    /// Gets the reference to the error HtmlGenericControl that is displayed whenever the MediaPlayer cannot open a file.
    /// </summary>
    protected virtual HtmlGenericControl MediaPlayerError => this.Container.GetControl<HtmlGenericControl>("unableToPlayVideo", false);

    /// <summary>
    /// Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" />. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      this.ControlStyle.Reset();
      base.AddAttributesToRender(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.SetSilverlightContainerVisibility)
      {
        if (this.IsDesignMode() && !this.IsPreviewMode())
        {
          this.Controls.Clear();
          if (this.IsModuleEnabledForRelatedData)
            this.Controls.Add((Control) new LiteralControl(Res.Get<VideosResources>().VideoNotAvailableInEditMode));
          else
            this.Controls.Add(this.GetModuleErrorMessageControl());
          this.shouldDisplayPlayer = false;
        }
        else if (this.UseYouTubePlaylist() || this.UseYouTubeVideo())
          this.shouldDisplayPlayer = true;
        else if (this.MediaContentId != Guid.Empty || this.IsModuleEnabledForRelatedData)
        {
          if (this.GetVideoById() == null)
          {
            if (this.IsDesignMode())
            {
              this.Controls.Clear();
              this.Controls.Add((Control) new LiteralControl(Res.Get<VideosResources>().VideoWasNotSelectedOrHasBeenDeleted));
            }
            this.shouldDisplayPlayer = false;
          }
          else if (this.TitleLabel != null)
            this.TitleLabel.Text = this.Title;
        }
        else
          this.shouldDisplayPlayer = false;
      }
      if (this.shouldDisplayPlayer)
        return;
      this.PlayerContainer.Visible = false;
    }

    private Video GetVideoById()
    {
      Video videoById = (Video) null;
      if (this.MediaContentId != Guid.Empty)
      {
        Guid id = this.MediaContentId;
        videoById = this.Manager.GetVideos().Where<Video>((Expression<Func<Video, bool>>) (v => v.Id == id)).Where<Video>(PredefinedFilters.PublishedItemsFilter<Video>()).SingleOrDefault<Video>();
      }
      else if (this.DisplayRelatedData())
        videoById = this.GetRelatedItem() as Video;
      return videoById;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.LoadMediaUrl();
      this.SetUpMediaPlayer();
      if (!SystemManager.IsInlineEditingMode)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Subscribes the cache dependency.</summary>
    protected virtual void SubscribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageDataCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageDataCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageDataCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>A collection of  instances of type <see cref="!:CacheDependencyNotifiedObject" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> dependencyObjects = new List<CacheDependencyKey>();
      if (this.MediaContentId != Guid.Empty)
        dependencyObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof (Video), this.MediaContentId));
      return (IList<CacheDependencyKey>) dependencyObjects;
    }

    public string GetProviderName() => !this.ProviderName.IsNullOrEmpty() ? this.ProviderName : this.Manager.Provider.Name;

    public bool DefinedProviderNotAvailable() => !this.ProviderName.IsNullOrEmpty() && this.ProviderName != this.Manager.Provider.Name;

    public string ContentType => typeof (Video).FullName;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      if (this.shouldDisplayPlayer)
      {
        ScriptControlDescriptor controlDescriptor1 = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
        controlDescriptor1.AddProperty("_domain", (object) UrlPath.GetDomainUrl());
        controlDescriptor1.AddProperty("url", (object) this.MediaUrl);
        controlDescriptor1.AddProperty("title", (object) this.MediaTitle);
        controlDescriptor1.AddProperty("description", (object) this.MediaDescription);
        controlDescriptor1.AddProperty("autoPlay", (object) this.AutoPlay);
        ScriptControlDescriptor controlDescriptor2 = controlDescriptor1;
        Unit unit = this.Width;
        string str1 = unit.ToString();
        controlDescriptor2.AddProperty("width", (object) str1);
        ScriptControlDescriptor controlDescriptor3 = controlDescriptor1;
        unit = this.Height;
        string str2 = unit.ToString();
        controlDescriptor3.AddProperty("height", (object) str2);
        controlDescriptor1.AddComponentProperty("mediaPlayer", this.MediaPlayer.ClientID);
        controlDescriptor1.AddElementProperty("mediaPlayerError", this.MediaPlayerError.ClientID);
        controlDescriptor1.AddProperty("_useYouTubePlayer", (object) this.UseYouTubeVideo());
        controlDescriptor1.AddProperty("_useYouTubePlaylist", (object) this.UseYouTubePlaylist());
        controlDescriptor1.AddProperty("startVolume", (object) this.StartVolume);
        controlDescriptor1.AddProperty("startTime", (object) this.StartTime);
        controlDescriptor1.AddProperty("fullScreen", (object) this.FullScreen);
        controlDescriptor1.AddProperty("isFrontend", (object) (bool) (!this.IsBackend() ? 1 : (!this.IsBackend() ? 0 : (this.IsPreviewMode() ? 1 : 0))));
        controlDescriptor1.AddElementProperty("playerContainer", this.PlayerContainer.ClientID);
        if (!string.IsNullOrEmpty(this.mediaMimeType))
          controlDescriptor1.AddProperty("mimeType", (object) this.mediaMimeType);
        scriptDescriptors.Add((ScriptDescriptor) controlDescriptor1);
      }
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      if (this.shouldDisplayPlayer)
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaPlayerControl.js", this.TypeName.Assembly.FullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    private bool UseYouTubePlaylist() => !string.IsNullOrEmpty(this.YouTubePlaylistID);

    private bool UseYouTubeVideo() => !this.UseYouTubePlaylist() && !string.IsNullOrEmpty(this.YouTubeVideoURL);

    private void LoadMediaUrl()
    {
      if (this.UseYouTubePlaylist())
        this.MediaUrl = (string) null;
      else if (this.UseYouTubeVideo())
      {
        this.MediaUrl = this.YouTubeVideoURL;
      }
      else
      {
        if (!(this.MediaContentId != Guid.Empty) && !this.DisplayRelatedData())
          return;
        Video videoById = this.GetVideoById();
        if (videoById == null)
          return;
        this.MediaUrl = videoById.MediaUrl;
        this.mediaMimeType = videoById.MimeType;
        this.SubscribeCacheDependency();
      }
    }

    private void SetUpMediaPlayer()
    {
      if (this.MediaPlayer == null)
        return;
      if (!string.IsNullOrEmpty(this.MediaUrl))
        this.MediaPlayer.Source = this.MediaUrl;
      if (!string.IsNullOrEmpty(this.mediaMimeType))
        this.MediaPlayer.MimeType = this.mediaMimeType;
      this.MediaPlayer.StartTime = (double) this.StartTime;
      this.MediaPlayer.StartVolume = this.StartVolume;
      if (!this.IsBackend() || this.IsPreviewMode())
      {
        this.MediaPlayer.AutoPlay = this.AutoPlay;
        this.MediaPlayer.FullScreen = this.FullScreen;
      }
      if (this.UseYouTubePlaylist())
        this.MediaPlayer.PlaylistSettings.YouTubePlaylist = this.YouTubePlaylistID;
      Unit unit = this.Width;
      if (!unit.IsEmpty)
        this.MediaPlayer.Width = this.Width;
      unit = this.Height;
      if (unit.IsEmpty)
        return;
      this.MediaPlayer.Height = this.Height;
    }

    /// <inheritdoc />
    public bool? DisplayRelatedData { get; set; }

    /// <inheritdoc />
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public RelatedDataDefinition RelatedDataDefinition
    {
      get
      {
        if (this.relatedDataDefinition == null)
          this.relatedDataDefinition = new RelatedDataDefinition();
        return this.relatedDataDefinition;
      }
      set => this.relatedDataDefinition = value;
    }

    /// <inheritdoc />
    public string GetContentType() => this.ContentType;

    /// <inheritdoc />
    public string UrlKeyPrefix { get; set; }

    /// <inheritdoc />
    public IEnumerable<IContentLocationInfo> GetLocations()
    {
      if (!(this.MediaContentId != Guid.Empty))
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo contentLocationInfo = new ContentLocationInfo();
      contentLocationInfo.ContentType = typeof (Video);
      contentLocationInfo.ProviderName = this.ProviderName;
      contentLocationInfo.Priority = ContentLocationPriority.Lowest;
      Video video = this.Manager.GetVideo(this.MediaContentId);
      List<string> itemIds = new List<string>();
      List<string> stringList1 = itemIds;
      Guid guid = video.Id;
      string str1 = guid.ToString();
      stringList1.Add(str1);
      if (video.OriginalContentId != Guid.Empty)
      {
        List<string> stringList2 = itemIds;
        guid = video.OriginalContentId;
        string str2 = guid.ToString();
        stringList2.Add(str2);
      }
      ContentLocationSingleItemFilter singleItemFilter = new ContentLocationSingleItemFilter((IEnumerable<string>) itemIds);
      contentLocationInfo.Filters.Add((IContentLocationFilter) singleItemFilter);
      return (IEnumerable<IContentLocationInfo>) new ContentLocationInfo[1]
      {
        contentLocationInfo
      };
    }

    /// <inheritdoc />
    public bool? DisableCanonicalUrlMetaTag { get; set; }
  }
}
