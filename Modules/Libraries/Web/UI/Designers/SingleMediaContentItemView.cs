// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A view for selecting/uploading, configuring and editing a media item.
  /// </summary>
  public class SingleMediaContentItemView : SimpleScriptView
  {
    private string uiCulture;
    private bool isMediaItemPublished = true;
    private MediaContent mediaItemInfo;
    public const string controlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemView.js";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.SingleMediaContentItemView.ascx");
    private string mediaItemServiceUrl;
    private static readonly IDictionary<string, string> mediaItemsServices = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName,
        "~/Sitefinity/Services/Content/ImageService.svc/"
      },
      {
        typeof (Video).FullName,
        "~/Sitefinity/Services/Content/VideoService.svc/"
      },
      {
        typeof (Document).FullName,
        "~/Sitefinity/Services/Content/DocumentService.svc/"
      }
    };

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null)
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>The id of the item that will be displayed.</summary>
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the thumbnail extension prefix.</summary>
    /// <value>The thumbnail extension prefix.</value>
    public string ThumbnailExtensionPrefix { get; set; }

    /// <summary>
    /// Gets the media item that this view is currently displaying.
    /// </summary>
    /// <value>The media item info.</value>
    private MediaContent MediaItemInfo
    {
      get
      {
        if (this.mediaItemInfo == null && this.ItemId != Guid.Empty)
          this.mediaItemInfo = (MediaContent) LibrariesManager.GetManager(this.ProviderName).GetItem(TypeResolutionService.ResolveType(this.MediaItemTypeName), this.ItemId);
        return this.mediaItemInfo;
      }
    }

    /// <summary>Gets or sets whether to use small item preview.</summary>
    /// <value>The use small item preview.</value>
    public bool UseSmallItemPreview { get; set; }

    /// <summary>
    /// Gets or sets the full name of the media item type that this view will work with.
    /// </summary>
    /// <value>The name of the media item type.</value>
    public string MediaItemTypeName { get; set; }

    /// <summary>Gets or sets whether the media item is published.</summary>
    /// <value>Gets or sets whether the media item is published.</value>
    public bool IsMediaItemPublished
    {
      get => this.isMediaItemPublished;
      set => this.isMediaItemPublished = value;
    }

    /// <summary>
    /// Gets or sets whether to show the crop/resize/rotate button.
    /// </summary>
    /// <value>The show crop resize rotate button.</value>
    public bool ShowCropResizeRotateButton { get; set; }

    /// <summary>Gets or sets the media item service URL.</summary>
    /// <value>The media item service URL.</value>
    public string MediaItemServiceUrl
    {
      get
      {
        string str;
        return !string.IsNullOrEmpty(this.mediaItemServiceUrl) || !SingleMediaContentItemView.mediaItemsServices.TryGetValue(this.MediaItemTypeName, out str) ? this.mediaItemServiceUrl : str;
      }
      set => this.mediaItemServiceUrl = value;
    }

    /// <summary>Gets or sets the media content details URL.</summary>
    /// <value>The media content details URL.</value>
    private string MediaContentDetailsUrl { get; set; }

    /// <summary>
    /// Gets or sets the option that determines whether to show blank item screen if there is no selected item or to open the items selector.
    /// </summary>
    /// <value>The skip blank item view.</value>
    public bool SkipBlankItemView { get; set; }

    /// <summary>Gets the reference to the title label.</summary>
    /// <value>The title label.</value>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("lblTitle", true);

    /// <summary>Gets the reference to the alt text label.</summary>
    /// <value>The alt text label.</value>
    protected virtual Label AltTextLabel => this.Container.GetControl<Label>("lblAltText", true);

    /// <summary>Gets the reference to the extension label.</summary>
    /// <value>The extension label.</value>
    protected virtual Label ExtensionLabel => this.Container.GetControl<Label>("lblExtension", true);

    /// <summary>Gets the reference to the label for the size.</summary>
    /// <value>The size label.</value>
    protected virtual Label SizeLabel => this.Container.GetControl<Label>("lblSize", true);

    /// <summary>Gets the reference to the library label.</summary>
    /// <value>The library label.</value>
    protected virtual Label LibraryLabel => this.Container.GetControl<Label>("lblLibrary", true);

    /// <summary>Gets the referene to the SFImage control.</summary>
    /// <value>The image.</value>
    protected virtual SfImage Image => this.Container.GetControl<SfImage>("thumbnailImage", true);

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a reference to the Button that changes the media item.
    /// </summary>
    protected virtual HyperLink SelectButton => this.Container.GetControl<HyperLink>("selectImageButton", true);

    /// <summary>
    /// Gets a reference to the Button that uploads the media item.
    /// </summary>
    protected virtual HyperLink UploadButton => this.Container.GetControl<HyperLink>("uploadImageButton", true);

    /// <summary>
    /// Represents the window manager which opens the window with the Image selector dialog
    /// </summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets a reference to the Button that changes the image.
    /// </summary>
    protected virtual WebControl ChangeImageButton => this.Container.GetControl<WebControl>("changeImageButton", true);

    /// <summary>
    /// Gets a reference to the Button that initiates editing of the image.
    /// </summary>
    protected virtual WebControl EditImageButton => this.Container.GetControl<WebControl>("editImageButton", true);

    /// <summary>
    /// Gets a reference to the Button that initiates editing of the image.
    /// </summary>
    protected virtual WebControl CropResizeRotateImageButton => this.Container.GetControl<WebControl>("cropResizeRotateImageButton", true);

    /// <summary>Gets the image selector.</summary>
    /// <value>The image selector.</value>
    protected virtual SingleMediaContentItemDialog ImageSelector => this.Container.GetControl<SingleMediaContentItemDialog>("imageSelector", true);

    /// <summary>Gets the reference to the image editor RadWindow.</summary>
    /// <value>The image editor.</value>
    protected virtual Telerik.Web.UI.RadWindow ImageEditor => this.Container.GetControl<Telerik.Web.UI.RadWindow>("imageEditor", true);

    /// <summary>
    /// Gets a reference to the title of the Button that initiates editing of the media item.
    /// </summary>
    private Literal EditButtonTitle => this.Container.GetControl<Literal>("editButtonTitle", true);

    /// <summary>
    /// Gets a reference to the title of the Button that initiates change of the media item.
    /// </summary>
    private Literal ChangeButtonTitle => this.Container.GetControl<Literal>("changeButtonTitle", true);

    /// <summary>
    /// Gets a reference to the literal that is shown when the media item is unpublished.
    /// </summary>
    private Literal UnpublishedMediaItemMessage => this.Container.GetControl<Literal>("unpublishedMediaItemMessage", true);

    /// <summary>
    /// Gets a reference to the video field that displays selected video.
    /// </summary>
    protected virtual MediaField MediaField => this.Container.GetControl<MediaField>("mediaField", true);

    /// <summary>Gets the document hyper link.</summary>
    /// <value>The document hyper link.</value>
    protected virtual SitefinityHyperLink DocumentHyperLink => this.Container.GetControl<SitefinityHyperLink>("documentHyperLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      EditorExternalDialogModes viewMode = this.GetViewMode();
      this.CropResizeRotateImageButton.Visible = viewMode == EditorExternalDialogModes.Image && this.ShowCropResizeRotateButton;
      LibrariesManager manager = LibrariesManager.GetManager(this.ProviderName);
      if (this.MediaItemInfo != null)
      {
        this.TitleLabel.Text = HttpUtility.HtmlEncode((string) this.MediaItemInfo.Title);
        this.ExtensionLabel.Text = HttpUtility.HtmlEncode(this.mediaItemInfo.Extension);
        this.SizeLabel.Text = HttpUtility.HtmlEncode(this.GetSizeString(this.MediaItemInfo.TotalSize));
        switch (viewMode)
        {
          case EditorExternalDialogModes.NotSet:
          case EditorExternalDialogModes.Image:
            Telerik.Sitefinity.Libraries.Model.Image mediaItemInfo = (Telerik.Sitefinity.Libraries.Model.Image) this.MediaItemInfo;
            this.AltTextLabel.Text = HttpUtility.HtmlEncode((string) mediaItemInfo.AlternativeText);
            this.LibraryLabel.Text = HttpUtility.HtmlEncode((string) mediaItemInfo.Album.Title);
            this.Image.ImageUrl = "~" + manager.Provider.GetItemUrl((ILocatable) this.MediaItemInfo) + this.MediaItemInfo.Extension;
            break;
          case EditorExternalDialogModes.Document:
            this.UseSmallItemPreview = false;
            break;
          case EditorExternalDialogModes.Media:
            if (this.UseSmallItemPreview)
              this.Image.ImageUrl = this.MediaItemInfo.ThumbnailUrl;
            this.MediaField.LibraryContentType = typeof (Video);
            this.MediaField.ResourceClassId = VideosDefinitions.ResourceClassId;
            break;
          default:
            this.Image.ImageUrl = this.MediaItemInfo.ThumbnailUrl;
            break;
        }
      }
      this.SetControlsVisibility(viewMode);
      this.SetControlsText(viewMode);
      this.ImageSelector.DialogMode = viewMode;
      this.ThumbnailExtensionPrefix = Config.Get<LibrariesConfig>().ThumbnailExtensionPrefix;
      this.SetMediaContentDetailUrl();
    }

    private void SetControlsVisibility(EditorExternalDialogModes dialogMode)
    {
      this.MediaField.Visible = false;
      this.Image.Visible = false;
      switch (dialogMode)
      {
        case EditorExternalDialogModes.NotSet:
        case EditorExternalDialogModes.Image:
          this.Image.Visible = true;
          break;
        case EditorExternalDialogModes.Document:
          this.UseSmallItemPreview = false;
          break;
        case EditorExternalDialogModes.Media:
          this.Image.Visible = this.UseSmallItemPreview;
          this.MediaField.Visible = !this.UseSmallItemPreview;
          this.MediaField.LibraryContentType = typeof (Video);
          this.MediaField.ResourceClassId = VideosDefinitions.ResourceClassId;
          break;
      }
    }

    private void SetControlsText(EditorExternalDialogModes dialogMode)
    {
      this.EditButtonTitle.Text = Res.Get<LibrariesResources>().EditAllProperties;
      string mediaItemMessage = Res.Get<LibrariesResources>().UnpublishedMediaItemMessage;
      switch (dialogMode)
      {
        case EditorExternalDialogModes.Image:
          this.SelectButton.Text = Res.Get<LibrariesResources>().SelectImage;
          this.UploadButton.Text = Res.Get<LibrariesResources>().UploadImage;
          this.ChangeButtonTitle.Text = Res.Get<LibrariesResources>().ChangeImage;
          this.UnpublishedMediaItemMessage.Text = string.Format(mediaItemMessage, (object) "image");
          break;
        case EditorExternalDialogModes.Document:
          this.SelectButton.Text = Res.Get<LibrariesResources>().SelectDocument;
          this.UploadButton.Text = Res.Get<LibrariesResources>().UploadDocument;
          this.ChangeButtonTitle.Text = Res.Get<LibrariesResources>().ChangeDocument;
          this.UnpublishedMediaItemMessage.Text = string.Format(mediaItemMessage, (object) "document or file");
          break;
        case EditorExternalDialogModes.Media:
          this.SelectButton.Text = Res.Get<LibrariesResources>().SelectVideo;
          this.UploadButton.Text = Res.Get<LibrariesResources>().UploadVideo;
          this.ChangeButtonTitle.Text = Res.Get<LibrariesResources>().ChangeVideo;
          this.UnpublishedMediaItemMessage.Text = string.Format(mediaItemMessage, (object) "video");
          break;
      }
    }

    private string GetSizeString(long totalSize) => totalSize > 1048576L ? (totalSize / 1048576L).ToString() + " " + Res.Get<LibrariesResources>().Mb : (totalSize / 1024L).ToString() + " " + Res.Get<LibrariesResources>().Kb;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void SetMediaContentDetailUrl()
    {
      switch (this.GetViewMode())
      {
        case EditorExternalDialogModes.Document:
          this.MediaContentDetailsUrl = this.GetDetailsViewUrl(DocumentsDefinitions.BackendDefinitionName, "DocumentsBackendEdit");
          break;
        case EditorExternalDialogModes.Media:
          this.MediaContentDetailsUrl = this.GetDetailsViewUrl(VideosDefinitions.BackendVideosDefinitionName, "VideosBackendEdit");
          break;
        default:
          this.MediaContentDetailsUrl = this.GetDetailsViewUrl("ImagesBackend", "ImagesBackendEdit");
          break;
      }
    }

    private string GetDetailsViewUrl(string controlDefinitionName, string viewName) => VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ContentViewEditDialog?" + "ControlDefinitionName=" + controlDefinitionName + "&ViewName=" + viewName + "&" + "IsInlineEditingMode" + "=true");

    private EditorExternalDialogModes GetViewMode()
    {
      if (this.MediaItemTypeName == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
        return EditorExternalDialogModes.Image;
      if (this.MediaItemTypeName == typeof (Video).FullName)
        return EditorExternalDialogModes.Media;
      return this.MediaItemTypeName == typeof (Document).FullName ? EditorExternalDialogModes.Document : EditorExternalDialogModes.NotSet;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("titleLabel", this.TitleLabel.ClientID);
      controlDescriptor.AddElementProperty("altTextLabel", this.AltTextLabel.ClientID);
      controlDescriptor.AddElementProperty("extensionLabel", this.ExtensionLabel.ClientID);
      controlDescriptor.AddElementProperty("sizeLabel", this.SizeLabel.ClientID);
      controlDescriptor.AddElementProperty("libraryLabel", this.LibraryLabel.ClientID);
      controlDescriptor.AddElementProperty("imageElement", this.Image.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("selectImageButton", this.SelectButton.ClientID);
      controlDescriptor.AddElementProperty("uploadImageButton", this.UploadButton.ClientID);
      controlDescriptor.AddElementProperty("changeImageButton", this.ChangeImageButton.ClientID);
      controlDescriptor.AddElementProperty("editImageButton", this.EditImageButton.ClientID);
      controlDescriptor.AddElementProperty("cropResizeRotateButton", this.CropResizeRotateImageButton.ClientID);
      controlDescriptor.AddComponentProperty("imageSelector", this.ImageSelector.ClientID);
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      controlDescriptor.AddProperty("imageEditorDialogUrl", (object) this.MediaContentDetailsUrl);
      controlDescriptor.AddProperty("_serviceUrl", (object) VirtualPathUtility.ToAbsolute(this.MediaItemServiceUrl));
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("useSmallItemPreview", (object) this.UseSmallItemPreview);
      controlDescriptor.AddProperty("editorBackPhrase", (object) Res.Get<Labels>().BackToEditPage);
      controlDescriptor.AddProperty("imageEditorName", (object) this.ImageEditor.ID);
      controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      controlDescriptor.AddProperty("viewMode", (object) this.GetViewMode().ToString());
      controlDescriptor.AddProperty("skipBlankItemView", (object) this.SkipBlankItemView);
      controlDescriptor.AddProperty("isMediaItemPublished", (object) this.IsMediaItemPublished);
      if (!string.IsNullOrEmpty(MediaContentExtensions.UrlVersionQueryParam))
        controlDescriptor.AddProperty("_urlVersionQueryParam", (object) MediaContentExtensions.UrlVersionQueryParam);
      if (this.MediaField.Visible)
        controlDescriptor.AddComponentProperty("mediaField", this.MediaField.ClientID);
      if (this.GetViewMode() == EditorExternalDialogModes.Document)
        controlDescriptor.AddElementProperty("documentLink", this.DocumentHyperLink.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (SingleMediaContentItemView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        PageManager.GetScriptReferences(ScriptRef.DialogManager).SingleOrDefault<ScriptReference>(),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemView.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", assembly)
      };
    }
  }
}
