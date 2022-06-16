// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.AssetsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Field control for associating various assets, such as documents, images and videos
  /// to a data item.
  /// </summary>
  public class AssetsField : FieldControl
  {
    private ContentLink[] contentLinks;
    private Guid targetLibraryId = Guid.Empty;
    private Guid sourceLibraryId = Guid.Empty;
    private bool useOnlyUploadMode;
    private bool useOnlySelectMode;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.AssetsField.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.AssetsField.js";
    internal const string jqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string iLocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = AssetsField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    [Obsolete]
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the mode in which <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AssetsField" /> field control should work.
    /// </summary>
    public AssetsWorkMode WorkMode { get; set; }

    /// <summary>Gets or sets the select view provider name.</summary>
    /// <value>The provider name.</value>
    public string SelectViewProviderName { get; set; }

    /// <summary>Gets or sets the upload view provider name.</summary>
    /// <value>The provider name.</value>
    public string UploadViewProviderName { get; set; }

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used. By default
    /// it returns current type.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (AssetsField).FullName;

    /// <summary>
    /// The content links that will be displayed by the Assets field
    /// </summary>
    public ContentLink[] ContentLinks
    {
      get => this.contentLinks;
      set
      {
        this.contentLinks = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets the current view mode of the Assets field for loading the conditional template
    /// </summary>
    public string CurrentViewMode => this.WorkMode.ToString() + this.DisplayMode.ToString();

    /// <summary>
    /// Gets or sets the maximum size of the file to be uploaded through the assets field.
    /// </summary>
    public int MaxFileSize { get; set; }

    /// <summary>
    /// Gets or sets the extensions of files that are allowed to be uploaded.
    /// </summary>
    public string AllowedExtensions { get; set; }

    /// <summary>
    /// Gets or sets the id of the library where images will be uploaded.
    /// </summary>
    public Guid TargetLibraryId
    {
      get => this.targetLibraryId;
      set => this.targetLibraryId = value;
    }

    /// <summary>
    /// Gets or sets the id of the library which items will be shown in the selector's select screen.
    /// </summary>
    /// <value>The source library id.</value>
    public Guid SourceLibraryId
    {
      get => this.sourceLibraryId;
      set => this.sourceLibraryId = value;
    }

    /// <summary>
    /// Gets or sets the text for select button. If not set, the default text will be used.
    /// </summary>
    public string SelectButtonText { get; set; }

    /// <summary>Gets or sets the CSS class for select button.</summary>
    public string SelectButtonCssClass { get; set; }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only upload option.
    /// </summary>
    /// <value>The flag, indicating if only upload mode will be used.</value>
    public bool UseOnlyUploadMode
    {
      get => this.useOnlyUploadMode;
      set => this.useOnlyUploadMode = value;
    }

    /// <summary>
    /// Gets or sets the option to hide upload option and use only select mode.
    /// </summary>
    /// <value>The flag, indicating if only select mode will be used.</value>
    public bool UseOnlySelectMode
    {
      get => this.useOnlySelectMode;
      set => this.useOnlySelectMode = value;
    }

    /// <summary>
    /// Gets or sets the CSS class that will be aplied to the media selector.
    /// </summary>
    /// <value>The selector CSS class.</value>
    public string SelectorCssClass { get; set; }

    /// <summary>
    /// Gets or sets whether to pre select already selected item in the selector.
    /// </summary>
    /// <value>The pre select item in selector.</value>
    public bool PreSelectItemInSelector { get; set; }

    /// <summary>
    /// Determines what screen will be shown when the selector is opened - upload or select.
    /// </summary>
    /// <value>The selector open mode.</value>
    public MediaSelectorOpenMode SelectorOpenMode { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual Label TitleLabel => this.GetControlForCurrentMode<Label>("title", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected virtual SitefinityLabel DescriptionLabel => this.GetControlForCurrentMode<SitefinityLabel>("description", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected virtual SitefinityLabel ExampleLabel => this.GetControlForCurrentMode<SitefinityLabel>("example", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the button that opens the dialog for choosing assets.
    /// </summary>
    protected virtual LinkButton SelectAssetsButton => this.GetControlForCurrentMode<LinkButton>("selectAssetsButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control which displays text of the button that opens the dialog for choosing assets.
    /// </summary>
    protected virtual Literal SelectAssetsButtonText => this.GetControlForCurrentMode<Literal>("selectAssetsButtonText", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the selector container control.</summary>
    protected virtual SingleMediaContentItemDialog Selector => this.GetControlForCurrentMode<SingleMediaContentItemDialog>("selector", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the control which displays the list of selected assets
    /// </summary>
    protected virtual HtmlGenericControl SelectedAssetsList => this.GetControlForCurrentMode<HtmlGenericControl>("selectedAssets", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the control that displays the single image in the read mode.
    /// </summary>
    protected virtual ImageControl ImageControlReadMode => this.Container.GetControl<ImageControl>("imageControl", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.SingleImage);

    /// <summary>
    /// Gets the control that displays multiple images in the read mode.
    /// </summary>
    protected virtual ImagesView ImagesViewControl => this.Container.GetControl<ImagesView>("imagesViewControl", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.MultipleImages);

    /// <summary>
    /// Gets the control that displays the single document in the read mode.
    /// </summary>
    protected virtual DocumentLink SingleDocumentLink => this.Container.GetControl<DocumentLink>("documentLink", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.SingleDocument);

    /// <summary>
    /// Gets the control that displays multiple documents in the read mode.
    /// </summary>
    protected virtual DownloadListView MultipleDocumentsControl => this.Container.GetControl<DownloadListView>("multipleDocumentsControl", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.MultipleDocuments);

    /// <summary>
    /// Gets the control that displays the single video in the read mode.
    /// </summary>
    protected virtual MediaPlayerControl SingleVideoPlayer => this.Container.GetControl<MediaPlayerControl>("singleVideoPlayer", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.SingleVideo);

    /// <summary>
    /// Gets the control that displays multiple videos in the read mode.
    /// </summary>
    protected virtual VideosView MultipleVideosControl => this.Container.GetControl<VideosView>("multipleVideosControl", this.DisplayMode == FieldDisplayMode.Read && this.WorkMode == AssetsWorkMode.MultipleVideos);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IAssetsFieldDefinition assetsFieldDefinition))
        return;
      if (!assetsFieldDefinition.WorkMode.HasValue)
        throw new InvalidOperationException();
      this.WorkMode = assetsFieldDefinition.WorkMode.Value;
      this.MaxFileSize = assetsFieldDefinition.MaxFileSize;
      this.AllowedExtensions = assetsFieldDefinition.AllowedExtensions;
      if (assetsFieldDefinition.TargetLibraryId.HasValue)
        this.TargetLibraryId = assetsFieldDefinition.TargetLibraryId.Value;
      if (assetsFieldDefinition.SourceLibraryId.HasValue)
        this.SourceLibraryId = assetsFieldDefinition.SourceLibraryId.Value;
      this.SelectButtonText = assetsFieldDefinition.SelectButtonText;
      this.SelectButtonCssClass = assetsFieldDefinition.SelectButtonCssClass;
      if (assetsFieldDefinition.UseOnlyUploadMode.HasValue)
        this.UseOnlyUploadMode = assetsFieldDefinition.UseOnlyUploadMode.Value;
      if (assetsFieldDefinition.UseOnlySelectMode.HasValue)
        this.UseOnlySelectMode = assetsFieldDefinition.UseOnlySelectMode.Value;
      this.SelectorCssClass = assetsFieldDefinition.SelectorCssClass;
      this.PreSelectItemInSelector = assetsFieldDefinition.PreSelectItemInSelector;
      if (!assetsFieldDefinition.SelectorOpenMode.HasValue)
        return;
      this.SelectorOpenMode = assetsFieldDefinition.SelectorOpenMode.Value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetBasicUserInterface();
      if (this.DisplayMode != FieldDisplayMode.Read)
        return;
      switch (this.WorkMode)
      {
        case AssetsWorkMode.SingleImage:
          this.DisplaySingleImageReadMode();
          break;
        case AssetsWorkMode.MultipleImages:
          this.DisplaysMultipleImagesReadMode();
          break;
        case AssetsWorkMode.SingleDocument:
          this.DisplaySingleDocumentReadMode();
          break;
        case AssetsWorkMode.MultipleDocuments:
          this.DisplaysMultipleDocumentsReadMode();
          break;
        case AssetsWorkMode.SingleVideo:
          this.DisplaySingleVideoReadMode();
          break;
        case AssetsWorkMode.MultipleVideos:
          this.DisplaysMultipleVideosReadMode();
          break;
        default:
          throw new NotSupportedException();
      }
      if (this.IsBackend() || this.SelectedAssetsList == null)
        return;
      this.SelectedAssetsList.Visible = false;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && !this.IsBackend())
        return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_assetsWorkMode", (object) this.WorkMode.ToString());
      controlDescriptor.AddProperty("_maxFileSize", (object) this.MaxFileSize);
      controlDescriptor.AddProperty("_allowedExtensions", (object) this.AllowedExtensions);
      controlDescriptor.AddProperty("_imageTypeFullName", (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName);
      controlDescriptor.AddProperty("_documentTypeFullName", (object) typeof (Document).FullName);
      controlDescriptor.AddProperty("_videoTypeFullName", (object) typeof (Video).FullName);
      controlDescriptor.AddProperty("_imageServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/"));
      controlDescriptor.AddProperty("_documentServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/"));
      controlDescriptor.AddProperty("_videoServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/VideoService.svc/"));
      controlDescriptor.AddProperty("_isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      controlDescriptor.AddProperty("_notTranslatedLabel", (object) Res.Get<LocalizationResources>().NotTranslated);
      controlDescriptor.AddProperty("_actionsLabel", (object) Res.Get<Labels>().Actions);
      controlDescriptor.AddProperty("_removeLabel", (object) Res.Get<Labels>().Remove);
      controlDescriptor.AddProperty("_viewOriginalSizeLabel", (object) Res.Get<Labels>().ViewOriginalSize);
      controlDescriptor.AddProperty("_editPropertiesLabel", (object) Res.Get<Labels>().EditProperties);
      controlDescriptor.AddProperty("_playVideoLabel", (object) Res.Get<Labels>().PlayVideo);
      controlDescriptor.AddProperty("_setAsPrimaryImageLabel", (object) Res.Get<Labels>().SetAsPrimaryImage);
      controlDescriptor.AddProperty("_setAsPrimaryVideoLabel", (object) Res.Get<Labels>().SetAsPrimaryVideo);
      controlDescriptor.AddProperty("_selectLabel", (object) Res.Get<Labels>().Select);
      controlDescriptor.AddProperty("_changeLabel", (object) Res.Get<Labels>().Change);
      if (this.SelectAssetsButton != null)
        controlDescriptor.AddElementProperty("selectAssetsButton", this.SelectAssetsButton.ClientID);
      if (this.SelectedAssetsList != null)
        controlDescriptor.AddElementProperty("selectedAssetsList", this.SelectedAssetsList.ClientID);
      if (this.Selector != null)
        controlDescriptor.AddComponentProperty("selector", this.Selector.ClientID);
      controlDescriptor.AddProperty("_isBackendReadMode", (object) (bool) (this.DisplayMode != FieldDisplayMode.Read ? 0 : (this.IsBackend() ? 1 : 0)));
      controlDescriptor.AddProperty("_enabled", (object) this.Enabled);
      controlDescriptor.AddProperty("_preSelectItemInSelector", (object) this.PreSelectItemInSelector);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && !this.IsBackend())
        return (IEnumerable<ScriptReference>) new ScriptReference[0];
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", typeof (AssetsField).Assembly.FullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.AssetsField.js", typeof (AssetsField).Assembly.FullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", name)
      };
    }

    /// <summary>Sets the basic user interface of the field.</summary>
    private void SetBasicUserInterface()
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ExampleLabel.Text = this.Example;
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      this.Selector.UploadViewProviderName = this.UploadViewProviderName;
      this.Selector.SelectViewProviderName = this.SelectViewProviderName;
      this.SelectAssetsButtonText.Text = this.ResolveSelectAssetButtonText();
      this.Selector.TargetLibraryId = this.TargetLibraryId;
      this.Selector.SourceLibraryId = this.SourceLibraryId;
      this.Selector.UseOnlyUploadMode = this.UseOnlyUploadMode;
      this.Selector.UseOnlySelectMode = this.UseOnlySelectMode;
      this.Selector.BodyCssClass = this.SelectorCssClass;
      this.Selector.MediaDialogOpenMode = this.SelectorOpenMode;
      if (string.IsNullOrEmpty(this.SelectButtonCssClass))
        return;
      ((WebControl) this.SelectAssetsButtonText.Parent).CssClass = this.SelectButtonCssClass;
    }

    private string ResolveSelectAssetButtonText()
    {
      if (!string.IsNullOrEmpty(this.SelectButtonText))
        return this.SelectButtonText;
      string str1 = Res.Get<Labels>().Select + " ";
      string str2;
      switch (this.WorkMode)
      {
        case AssetsWorkMode.SingleImage:
          str2 = Res.Get<Labels>().ImageSingular;
          break;
        case AssetsWorkMode.MultipleImages:
          str2 = Res.Get<Labels>().ImagePlural;
          break;
        case AssetsWorkMode.SingleDocument:
          str2 = Res.Get<Labels>().DocumentSingular;
          break;
        case AssetsWorkMode.MultipleDocuments:
          str2 = Res.Get<Labels>().DocumentPlural;
          break;
        case AssetsWorkMode.SingleVideo:
          str2 = Res.Get<Labels>().VideoSingular;
          break;
        case AssetsWorkMode.MultipleVideos:
          str2 = Res.Get<Labels>().VideoPlural;
          break;
        default:
          throw new NotSupportedException();
      }
      return str1 + str2.ToLower();
    }

    /// <summary>
    /// Gets the control based on the current display and work mode.
    /// </summary>
    /// <typeparam name="TControl">Type of the control to find.</typeparam>
    /// <param name="baseId">
    /// Base id of the control which is same for all the modes.
    /// </param>
    /// <param name="isRequired">
    /// Determines weather the control is required (exception will be thrown if control is required and not found).
    /// </param>
    /// <returns>The instance of the control.</returns>
    private TControl GetControlForCurrentMode<TControl>(string baseId, bool isRequired) where TControl : Control => this.Container.GetControl<TControl>(AssetsField.GetActualId(baseId, this.WorkMode, this.DisplayMode), isRequired);

    /// <summary>
    /// Gets the actual id of the control, based on it's base id, specified work mode and display mode.
    /// </summary>
    /// <param name="baseId">The base id of the control.</param>
    /// <param name="workMode">
    /// The work mode for which the control should be found.
    /// </param>
    /// <param name="displayMode">
    /// The display mode for which the control should be found.
    /// </param>
    /// <returns>The actual id of the control.</returns>
    private static string GetActualId(
      string baseId,
      AssetsWorkMode workMode,
      FieldDisplayMode displayMode)
    {
      string str1;
      switch (workMode)
      {
        case AssetsWorkMode.SingleImage:
          str1 = "_image";
          break;
        case AssetsWorkMode.MultipleImages:
          str1 = "_images";
          break;
        case AssetsWorkMode.SingleDocument:
          str1 = "_document";
          break;
        case AssetsWorkMode.MultipleDocuments:
          str1 = "_documents";
          break;
        case AssetsWorkMode.SingleVideo:
          str1 = "_video";
          break;
        case AssetsWorkMode.MultipleVideos:
          str1 = "_videos";
          break;
        default:
          throw new NotSupportedException();
      }
      string str2 = displayMode != FieldDisplayMode.Read ? "_write" : "_read";
      return baseId + str1 + str2;
    }

    internal virtual void DisplaySingleImageReadMode()
    {
      if (this.ContentLinks == null)
      {
        this.ImageControlReadMode.Visible = false;
      }
      else
      {
        ContentLink contentLink = ((IEnumerable<ContentLink>) this.ContentLinks).FirstOrDefault<ContentLink>();
        if (contentLink == null)
          return;
        this.ImageControlReadMode.ImageId = contentLink.ChildItemId;
        this.ImageControlReadMode.ProviderName = contentLink.ChildItemProviderName;
        this.ImageControlReadMode.DataFieldName = this.DataFieldName;
      }
    }

    internal virtual void DisplaysMultipleImagesReadMode()
    {
      this.ImagesViewControl.DefaultViewFallBack = true;
      if (this.ContentLinks == null)
      {
        this.ImagesViewControl.DataSource = (object[]) Enumerable.Empty<Telerik.Sitefinity.Libraries.Model.Image>().ToArray<Telerik.Sitefinity.Libraries.Model.Image>();
      }
      else
      {
        ContentLink[] contentLinks = this.ContentLinks;
        if (contentLinks == null)
          return;
        List<object> objectList = new List<object>();
        foreach (ContentLink contentLink in contentLinks)
        {
          ContentLink imageLink = contentLink;
          Telerik.Sitefinity.Libraries.Model.Image image = LibrariesManager.GetManager(imageLink.ChildItemProviderName).GetImages().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == imageLink.ChildItemId));
          if (image != null)
            objectList.Add((object) image);
        }
        this.ImagesViewControl.GetDetailItemFromDataSource = true;
        this.ImagesViewControl.DataSource = objectList.ToArray();
      }
    }

    private void DisplaySingleDocumentReadMode()
    {
      if (this.ContentLinks == null || this.IsBackend())
        return;
      ContentLink contentLink = ((IEnumerable<ContentLink>) this.ContentLinks).FirstOrDefault<ContentLink>();
      if (contentLink == null)
        return;
      this.SingleDocumentLink.DocumentId = contentLink.ChildItemId;
      this.SingleDocumentLink.ProviderName = contentLink.ChildItemProviderName;
    }

    private void DisplaysMultipleDocumentsReadMode()
    {
      this.MultipleDocumentsControl.DefaultViewFallBack = true;
      if (this.ContentLinks == null)
      {
        this.MultipleDocumentsControl.DataSource = (object[]) Enumerable.Empty<Document>().ToArray<Document>();
      }
      else
      {
        ContentLink[] contentLinks = this.ContentLinks;
        if (contentLinks == null)
          return;
        List<object> objectList = new List<object>();
        foreach (ContentLink contentLink in contentLinks)
        {
          ContentLink documentLink = contentLink;
          Document document = LibrariesManager.GetManager(documentLink.ChildItemProviderName).GetDocuments().FirstOrDefault<Document>((Expression<Func<Document, bool>>) (d => d.Id == documentLink.ChildItemId));
          if (document != null)
            objectList.Add((object) document);
        }
        this.MultipleDocumentsControl.GetDetailItemFromDataSource = true;
        this.MultipleDocumentsControl.DataSource = objectList.ToArray();
      }
    }

    private void DisplaySingleVideoReadMode()
    {
      if (this.ContentLinks == null)
        return;
      ContentLink contentLink = ((IEnumerable<ContentLink>) this.ContentLinks).FirstOrDefault<ContentLink>();
      if (contentLink == null)
        return;
      this.SingleVideoPlayer.MediaContentId = contentLink.ChildItemId;
      this.SingleVideoPlayer.ProviderName = contentLink.ChildItemProviderName;
    }

    private void DisplaysMultipleVideosReadMode()
    {
      this.MultipleVideosControl.DefaultViewFallBack = true;
      if (this.ContentLinks == null)
      {
        this.MultipleVideosControl.DataSource = (object[]) Enumerable.Empty<Video>().ToArray<Video>();
      }
      else
      {
        ContentLink[] contentLinks = this.ContentLinks;
        if (contentLinks == null)
          return;
        List<object> objectList = new List<object>();
        foreach (ContentLink contentLink in contentLinks)
        {
          ContentLink videoLink = contentLink;
          Video video = LibrariesManager.GetManager(videoLink.ChildItemProviderName).GetVideos().FirstOrDefault<Video>((Expression<Func<Video, bool>>) (v => v.Id == videoLink.ChildItemId));
          if (video != null)
            objectList.Add((object) video);
        }
        this.MultipleVideosControl.GetDetailItemFromDataSource = true;
        this.MultipleVideosControl.DataSource = objectList.ToArray();
      }
    }
  }
}
