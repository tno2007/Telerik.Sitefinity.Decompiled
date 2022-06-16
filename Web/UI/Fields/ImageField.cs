// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ImageField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.ContentFluentApi;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Workflow;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Field control for displaying and uploading single image
  /// </summary>
  [FieldDefinitionElement(typeof (ImageFieldElement))]
  [RequiresDataItem]
  public class ImageField : FieldControl, IRequiresDataItem
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ImageField.ascx");
    private IDataItem dataItem;
    private ImageFieldUploadMode uploadMode;
    private bool? boundOnServer;
    private const string jqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string imageFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private const string imageFieldModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageFieldUploadMode.js";
    internal const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    private Type dataFieldType;
    private Guid editorSourceLibraryId = Guid.Empty;
    private string editorSelectViewProviderName;
    private bool editorShowOnlySystemLibraries;
    private bool editorUseOnlySelectMode;
    private bool editorShowLibFilterWrp = true;
    private bool editorShowBreadcrumb = true;
    private bool editorShowSearchBox = true;
    private string editorSelectorViewTitle;
    private bool hideProvidersSelector;
    public Guid targetLibraryId = Guid.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ImageField" /> class.
    /// </summary>
    public ImageField() => this.LayoutTemplatePath = ImageField.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the value of the property. If bound to a content link will resolve the image from it, otherwise expects the value to be the url to the image
    /// </summary>
    /// <value>The value.</value>
    public override object Value
    {
      get
      {
        if (typeof (ContentLink).IsAssignableFrom(this.DataFieldType))
        {
          if (this.DataItem == null)
            return (object) null;
          return TypeDescriptor.GetProperties((object) this.DataItem)[this.DataFieldName]?.GetValue((object) this.DataItem);
        }
        return this.DataFieldType == typeof (Guid) ? (object) this.ImageId : (object) this.ImageControl.Src;
      }
      set
      {
        if (value != null)
        {
          if (typeof (ContentLink).IsAssignableFrom(this.DataFieldType))
          {
            ContentLink contentLink = value as ContentLink;
            this.ImageId = contentLink.ChildItemId;
            this.ImageProviderName = contentLink.ChildItemProviderName;
          }
          else if (this.DataFieldType == typeof (Guid))
            this.ImageId = (Guid) value;
          else
            this.ImageControl.Src = value as string;
        }
        else
        {
          this.ImageControl.Src = (string) null;
          this.ImageId = Guid.Empty;
        }
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the mode in which the ImageField operates.
    /// </summary>
    public ImageFieldUploadMode UploadMode
    {
      get => this.uploadMode;
      set => this.uploadMode = value;
    }

    /// <summary>
    /// Gets or sets the name of the provider used to retrieve the default image
    /// </summary>
    public string ProviderNameForDefaultImage { get; set; }

    /// <summary>
    /// Ges or sets the Id of the image used as default image (when there is no ImageId specified).
    /// </summary>
    public Guid DefaultImageId { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider used to retrieve/store the image
    /// </summary>
    public string ImageProviderName { get; set; }

    /// <summary>Gets or sets the Id of the selected Image</summary>
    public Guid ImageId { get; set; }

    /// <summary>Gets or sets the maximum image width</summary>
    public int? MaxWidth { get; set; }

    /// <summary>Gets or sets maximum image height</summary>
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the text of the label for the Replace photo button.
    /// </summary>
    public string ReplacePhotoButtonLabel { get; set; }

    /// <summary>
    /// Gets or sets the text of the label for the Delete photo button.
    /// </summary>
    public string DeletePhotoButtonLabel { get; set; }

    /// <summary>
    /// Gets or sets the text of the label for the Don't upload photo button.
    /// </summary>
    public string CancelUploadButtonLabel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the Delete image button
    /// </summary>
    public bool ShowDeleteImageButton { get; set; }

    /// <summary>Id of the album where to upload the image</summary>
    public Guid AlbumId { get; set; }

    /// <summary>
    /// Represents the type of the value which the control should display - content link, image id - guid or string - url
    /// </summary>
    [TypeConverter(typeof (StringTypeConverter))]
    public Type DataFieldType
    {
      get => this.dataFieldType;
      set
      {
        if (value == (Type) null)
          value = typeof (string);
        this.dataFieldType = typeof (ContentLink).IsAssignableFrom(value) || !(value != typeof (Guid)) || !(value != typeof (string)) ? value : throw new NotSupportedException("Image field cannot be bound to a value of type {0}".Arrange((object) this.DataFieldType.FullName));
      }
    }

    /// <summary>Gets or sets the default Src for the image.</summary>
    /// <value>The default Src for the image.</value>
    public virtual string DefaultSrc { get; set; }

    /// <summary>Gets or sets the default value for the image ALT tag.</summary>
    public virtual string DefaultAlt { get; set; }

    /// <summary>
    /// Specifies if the image field will allow client binding when a
    /// </summary>
    public virtual bool BoundOnServer
    {
      get
      {
        if (!this.boundOnServer.HasValue)
        {
          switch (this.DisplayMode)
          {
            case FieldDisplayMode.Read:
              this.boundOnServer = this.IsDesignMode() || this.IsPreviewMode() || !this.IsBackend() ? new bool?(true) : new bool?(false);
              break;
            case FieldDisplayMode.Write:
              switch (this.UploadMode)
              {
                case ImageFieldUploadMode.Dialog:
                  this.boundOnServer = new bool?(false);
                  break;
                default:
                  this.boundOnServer = new bool?(true);
                  break;
              }
              break;
            default:
              this.boundOnServer = new bool?(false);
              break;
          }
        }
        return this.boundOnServer.Value;
      }
      set => this.boundOnServer = new bool?(value);
    }

    /// <summary>
    /// Gets or sets the size of the img tag in pixels. This is used as the size of the smaller of the two dimensions of the imade(width or height).
    /// </summary>
    /// <value>Gets or sets the size of the img tag in pixels.</value>
    public int? SizeInPx { get; set; }

    /// <summary>
    /// Gets or sets whether ProviderSelector should be visible
    /// </summary>
    internal bool HideProvidersSelector
    {
      get => this.hideProvidersSelector;
      set => this.hideProvidersSelector = value;
    }

    /// <summary>
    /// Gets or sets the id of the uploaded content library which will be displayed in selector view.
    /// </summary>
    internal Guid EditorSourceLibraryId
    {
      get => this.editorSourceLibraryId;
      set => this.editorSourceLibraryId = value;
    }

    /// <summary>Gets or sets the select view provider name.</summary>
    internal string EditorSelectViewProviderName
    {
      get => this.editorSelectViewProviderName;
      set => this.editorSelectViewProviderName = value;
    }

    /// <summary>
    /// Gets or sets the whether to show only system libraries or not
    /// </summary>
    internal bool EditorShowOnlySystemLibraries
    {
      get => this.editorShowOnlySystemLibraries;
      set => this.editorShowOnlySystemLibraries = value;
    }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only select option.
    /// </summary>
    internal bool EditorUseOnlySelectMode
    {
      get => this.editorUseOnlySelectMode;
      set => this.editorUseOnlySelectMode = value;
    }

    /// <summary>
    /// Gets or sets whether library filter wrapper of EditorContentManagerDialog should be visible
    /// </summary>
    internal bool EditorShowLibFilterWrp
    {
      get => this.editorShowLibFilterWrp;
      set => this.editorShowLibFilterWrp = value;
    }

    /// <summary>
    /// Gets or sets whether breadcrumb of EditorContentManagerDialog should be visible
    /// </summary>
    internal bool EditorShowBreadcrumb
    {
      get => this.editorShowBreadcrumb;
      set => this.editorShowBreadcrumb = value;
    }

    /// <summary>
    /// Gets or sets whether search box of EditorContentManagerDialog should be visible
    /// </summary>
    internal bool EditorShowSearchBox
    {
      get => this.editorShowSearchBox;
      set => this.editorShowSearchBox = value;
    }

    /// <summary>Gets or sets the title of EditorContentManagerDialog</summary>
    internal string EditorSelectorViewTitle
    {
      get => this.editorSelectorViewTitle;
      set => this.editorSelectorViewTitle = value;
    }

    /// <summary>
    /// Gets or sets the id where the uploaded content will be saved.
    /// </summary>
    /// <value>The id where to save the uploaded content.</value>
    public Guid TargetLibraryId
    {
      get => this.targetLibraryId;
      set => this.targetLibraryId = value;
    }

    /// <summary>
    /// Converts a control ID used in conditional templates accoding to this.DisplayMode
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string lower = this.DisplayMode.ToString().ToLower();
      return originalName + "_" + lower;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control works only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("descriptionLabel_write", true) : (Label) null;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control works only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("titleLabel_write", true) : (Label) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>Gets the image control which should be displayed</summary>
    protected internal virtual HtmlImage ImageControl => this.GetConditionalControl<HtmlImage>("img", true);

    /// <summary>
    /// Gets a reference to the div tag containing the PostbackUpload panel.
    /// </summary>
    protected internal virtual HtmlGenericControl PostbackUploadPanel => this.Container.GetControl<HtmlGenericControl>(nameof (PostbackUploadPanel), this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the div tag containing the AsyncUpload panel.
    /// </summary>
    protected internal virtual HtmlGenericControl AsyncUploadPanel => this.Container.GetControl<HtmlGenericControl>(nameof (AsyncUploadPanel), this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets a reference to the ViewOriginalSize button.</summary>
    protected internal virtual LinkButton ViewOriginalSizeButton => this.GetConditionalControl<LinkButton>("viewOriginalSize", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets a reference to the ReplaceImage button.</summary>
    protected internal virtual LinkButton ReplaceImageButton => this.Container.GetControl<LinkButton>("replaceImage", false);

    /// <summary>Gets a reference to the DeleteImage button.</summary>
    protected internal virtual LinkButton DeleteImageButton => this.Container.GetControl<LinkButton>("deleteImage", false);

    /// <summary>Gets a reference to the CancelUpload button.</summary>
    protected internal virtual LinkButton CancelUploadButton => this.Container.GetControl<LinkButton>("cancelUpload", false);

    /// <summary>
    /// Gets a reference to the literal inside the ReplaceImage button.
    /// </summary>
    protected internal virtual Literal ReplaceImageButtonLiteral => this.Container.GetControl<Literal>("lReplaceImage", this.UploadMode == ImageFieldUploadMode.Dialog || this.UploadMode == ImageFieldUploadMode.InputField);

    /// <summary>
    /// Gets a reference to the literal inside the DeleteImage button.
    /// </summary>
    protected internal virtual Literal DeleteImageButtonLiteral => this.Container.GetControl<Literal>("lDeleteImage", this.UploadMode == ImageFieldUploadMode.Dialog || this.UploadMode == ImageFieldUploadMode.InputField);

    /// <summary>
    /// Gets a reference to the literal inside the CancelUpload button.
    /// </summary>
    protected internal virtual Literal CancelUploadButtonLiteral => this.Container.GetControl<Literal>("lCancelUpload", this.UploadMode == ImageFieldUploadMode.InputField);

    /// <summary>
    /// Gets a reference to the view panel div tag when FieldMode is InputField.
    /// </summary>
    protected internal virtual HtmlGenericControl ViewPanel => this.Container.GetControl<HtmlGenericControl>("viewPanel", this.UploadMode == ImageFieldUploadMode.InputField);

    /// <summary>
    /// Gets a reference to the upload panel div tag when FieldMode is InputField.
    /// </summary>
    protected internal virtual HtmlGenericControl UploadPanel => this.Container.GetControl<HtmlGenericControl>("uploadPanel", this.UploadMode == ImageFieldUploadMode.InputField);

    /// <summary>
    /// Gets a reference to the photoUpload RadUpload instance.
    /// </summary>
    protected internal virtual RadUpload PhotoUpload => this.Container.GetControl<RadUpload>("photoUpload", false);

    protected internal virtual SingleMediaContentItemDialog AsyncImageSelector => this.Container.GetControl<SingleMediaContentItemDialog>("asyncImageSelector", this.UploadMode == ImageFieldUploadMode.Dialog);

    /// <summary>
    /// The DataItem field from the IRequiresDataItem interface.
    /// </summary>
    public IDataItem DataItem
    {
      get => this.dataItem;
      set => this.dataItem = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        this.AsyncImageSelector.SourceLibraryId = this.EditorSourceLibraryId;
        this.AsyncImageSelector.SelectViewProviderName = this.EditorSelectViewProviderName;
        this.AsyncImageSelector.ShowOnlySystemLibraries = this.EditorShowOnlySystemLibraries;
        this.AsyncImageSelector.UseOnlySelectMode = this.EditorUseOnlySelectMode;
        this.AsyncImageSelector.SelectorViewShowLibFilterWrp = this.EditorShowLibFilterWrp;
        this.AsyncImageSelector.SelectorViewShowBreadcrumb = this.EditorShowBreadcrumb;
        this.AsyncImageSelector.SelectorViewShowSearchBox = this.EditorShowSearchBox;
        this.AsyncImageSelector.SelectorViewTitle = this.EditorSelectorViewTitle;
        this.AsyncImageSelector.HideProvidersSelector = this.HideProvidersSelector;
        this.AsyncImageSelector.TargetLibraryId = this.TargetLibraryId;
        if (this.UploadMode == ImageFieldUploadMode.NotSet)
          this.UploadMode = !this.IsBackend() ? ImageFieldUploadMode.InputField : ImageFieldUploadMode.Dialog;
        if (!this.ShowDeleteImageButton)
          this.DeleteImageButton.Visible = false;
        if (!string.IsNullOrEmpty(this.ReplacePhotoButtonLabel))
          this.ReplaceImageButtonLiteral.Text = this.ReplacePhotoButtonLabel;
        if (!string.IsNullOrEmpty(this.DeletePhotoButtonLabel))
          this.DeleteImageButtonLiteral.Text = this.DeletePhotoButtonLabel;
        if (this.UploadMode == ImageFieldUploadMode.InputField)
        {
          this.PostbackUploadPanel.Visible = true;
          this.AsyncUploadPanel.Visible = false;
          if (!string.IsNullOrEmpty(this.CancelUploadButtonLabel))
            this.CancelUploadButtonLiteral.Text = this.CancelUploadButtonLabel;
          if (this.ShowDeleteImageButton)
            this.DeleteImageButton.Command += new CommandEventHandler(this.DeleteImage_Click);
        }
        else
        {
          this.AsyncUploadPanel.Visible = true;
          this.AsyncImageSelector.Style.Add("display", "none");
          this.PostbackUploadPanel.Visible = false;
        }
      }
      if (this.TitleLabel != null)
        this.TitleLabel.Text = HttpUtility.HtmlEncode(this.Title);
      if (this.DescriptionLabel == null)
        return;
      this.DescriptionLabel.Text = HttpUtility.HtmlEncode(this.Description);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!this.Page.IsPostBack)
        return;
      using (new CultureRegion(SystemManager.CurrentContext.CurrentSite.DefaultCulture))
        this.UploadSelectedImage();
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IImageFieldDefinition imageFieldDefinition))
        return;
      this.ImageProviderName = imageFieldDefinition.ImageProviderName;
      this.ProviderNameForDefaultImage = imageFieldDefinition.ProviderNameForDefaultImage;
      this.DefaultImageId = imageFieldDefinition.DefaultImageId;
      this.MaxWidth = imageFieldDefinition.MaxWidth;
      this.MaxHeight = imageFieldDefinition.MaxHeight;
      this.DataFieldType = imageFieldDefinition.DataFieldType;
      this.DefaultSrc = imageFieldDefinition.DefaultSrc;
      ImageFieldUploadMode? uploadMode = imageFieldDefinition.UploadMode;
      if (uploadMode.HasValue)
      {
        uploadMode = imageFieldDefinition.UploadMode;
        this.UploadMode = uploadMode.Value;
      }
      this.SizeInPx = imageFieldDefinition.SizeInPx;
    }

    /// <summary>Overrides the default PreRender handler.</summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.BindImage();
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
    protected override ScriptRef GetRequiredCoreScripts()
    {
      ScriptRef requiredCoreScripts = ScriptRef.JQuery;
      if (this.UploadMode == ImageFieldUploadMode.Dialog && this.DisplayMode == FieldDisplayMode.Write)
        requiredCoreScripts |= ScriptRef.JQueryUI;
      return requiredCoreScripts;
    }

    private void UploadSelectedImage()
    {
      if (this.PhotoUpload == null || this.PhotoUpload.UploadedFiles.Count != 1)
        return;
      UploadedFile uploadedFile = this.PhotoUpload.UploadedFiles[0];
      if (!this.ValidateFile(uploadedFile, out string _))
        return;
      string fileName = uploadedFile.GetNameWithoutExtension();
      Telerik.Sitefinity.Libraries.Model.Image image1 = (Telerik.Sitefinity.Libraries.Model.Image) null;
      AppSettings appSettings = App.Prepare().SetContentProvider(this.ImageProviderName);
      if (this.ImageId != Guid.Empty)
      {
        string urlName = Regex.Replace(fileName, "[^\\w_-]", "-").ToLower();
        Guid id = this.ImageId;
        Telerik.Sitefinity.Libraries.Model.Image image2 = appSettings.WorkWith().Images().Where((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (im => im.Id == id)).Get().SingleOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
        if (image2 != null)
        {
          Guid originalContentId1 = image2.OriginalContentId;
          ImageDraftFacade<BaseFacade> facade = appSettings.WorkWith().Image(originalContentId1);
          image1 = facade.CheckOut().Do((Action<Telerik.Sitefinity.Libraries.Model.Image>) (img =>
          {
            if (string.IsNullOrEmpty((string) img.Title))
              img.Title = (Lstring) fileName;
            if (string.IsNullOrEmpty((string) img.Description))
              img.Description = (Lstring) fileName;
            if (string.IsNullOrEmpty((string) img.AlternativeText))
              img.AlternativeText = (Lstring) fileName;
            if (string.IsNullOrEmpty((string) img.UrlName))
              img.UrlName = (Lstring) urlName;
            if (string.IsNullOrEmpty((string) img.MediaFileUrlName))
              img.MediaFileUrlName = (Lstring) urlName;
            ((ILocatable) img).ClearUrls(true);
            img.ClearMediaFileUrls((IManager) facade.GetManager(), true);
          })).UploadContent(uploadedFile.InputStream, uploadedFile.GetExtension()).SaveAndContinue().Get();
        }
      }
      if (image1 == null)
        image1 = this.UploadNewImage(uploadedFile, fileName);
      Dictionary<string, string> contextBag = new Dictionary<string, string>();
      Type itemType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
      contextBag.Add("ContentType", itemType.FullName);
      contextBag.Add("MasterId", image1.OriginalContentId.ToString());
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        contextBag.Add("Language", SystemManager.CurrentContext.Culture.Name);
      string operationName = image1.ApprovalWorkflowState == (Lstring) "Created" ? "Upload" : "Publish";
      WorkflowManager.MessageWorkflow(image1.Id, itemType, this.ImageProviderName, operationName, true, contextBag);
      Guid originalContentId = image1.OriginalContentId;
      Telerik.Sitefinity.Libraries.Model.Image childDataItem = appSettings.WorkWith().Images().Where((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (im => im.OriginalContentId == originalContentId && (int) im.Status == 2)).Get().SingleOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
      this.ImageId = childDataItem.Id;
      if (this.DataItem != null)
      {
        ContentLink contentLink = this.DataItem.CreateContentLink((IDataItem) childDataItem);
        TypeDescriptor.GetProperties((object) this.DataItem)[this.DataFieldName].SetValue((object) this.DataItem, (object) contentLink);
      }
      this.ImageControl.Src = childDataItem.MediaUrl;
      this.ImageControl.Alt = (string) childDataItem.AlternativeText;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("imageElement", this.ImageControl.ClientID);
      controlDescriptor.AddProperty("_dataFieldType", (object) this.DataFieldType.Name);
      controlDescriptor.AddProperty("_blankLink", (object) new ContentLink()
      {
        ChildItemType = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName,
        ComponentPropertyName = this.DataFieldName
      });
      controlDescriptor.AddProperty("_boundOnServer", (object) this.BoundOnServer);
      controlDescriptor.AddProperty("sizeInPx", (object) this.SizeInPx);
      controlDescriptor.AddProperty("_imageServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/"));
      if (!string.IsNullOrEmpty(this.DefaultSrc))
      {
        if (VirtualPathUtility.IsAppRelative(this.DefaultSrc))
          controlDescriptor.AddProperty("_defaultSrc", (object) UrlPath.ResolveAbsoluteUrl(this.DefaultSrc));
        else
          controlDescriptor.AddProperty("_defaultSrc", (object) this.DefaultSrc);
      }
      controlDescriptor.AddProperty("fieldMode", (object) this.UploadMode);
      controlDescriptor.AddProperty("_editorShowLibFilterWrp", (object) this.EditorShowLibFilterWrp);
      if ((this.UploadMode == ImageFieldUploadMode.Dialog || this.UploadMode == ImageFieldUploadMode.InputField) && this.DisplayMode == FieldDisplayMode.Write)
      {
        if (this.ReplaceImageButton != null)
          controlDescriptor.AddElementProperty("replaceImageButtonElement", this.ReplaceImageButton.ClientID);
        if (this.DeleteImageButton != null && this.ShowDeleteImageButton)
          controlDescriptor.AddElementProperty("deleteImageButtonElement", this.DeleteImageButton.ClientID);
      }
      if (this.UploadMode == ImageFieldUploadMode.InputField && this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddElementProperty("cancelUploadButtonElement", this.CancelUploadButton.ClientID);
        controlDescriptor.AddProperty("viewPanelID", (object) this.ViewPanel.ClientID);
        controlDescriptor.AddProperty("uploadPanelID", (object) this.UploadPanel.ClientID);
      }
      if (this.UploadMode == ImageFieldUploadMode.Dialog && this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddProperty("albumId", (object) this.AlbumId);
        controlDescriptor.AddProperty("firstItemText", (object) Res.Get<LibrariesResources>().AllItems1);
        controlDescriptor.AddComponentProperty("asyncImageSelector", this.AsyncImageSelector.ClientID);
      }
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
      string fullName = typeof (TextField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageFieldUploadMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      };
    }

    /// <summary>
    /// Overrides the .Src property of the inner ImageControl depending on the current mode of the field control.
    /// </summary>
    protected virtual void BindImage()
    {
      bool flag = true;
      string altTag;
      if (this.ImageId != Guid.Empty)
      {
        Telerik.Sitefinity.Libraries.Model.Image sourceImage;
        string imageUrl = this.GetImageUrl(this.ImageProviderName, this.ImageId, out altTag, out sourceImage);
        this.ImageControl.Src = imageUrl;
        this.ImageControl.Alt = altTag;
        if (sourceImage != null)
        {
          int? sizeInPx = this.SizeInPx;
          if (sizeInPx.HasValue)
          {
            if (sourceImage.Width < sourceImage.Height)
            {
              System.Web.UI.AttributeCollection attributes = this.ImageControl.Attributes;
              sizeInPx = this.SizeInPx;
              string str = sizeInPx.ToString();
              attributes["width"] = str;
            }
            else
            {
              System.Web.UI.AttributeCollection attributes = this.ImageControl.Attributes;
              sizeInPx = this.SizeInPx;
              string str = sizeInPx.ToString();
              attributes["height"] = str;
            }
          }
        }
        flag = string.IsNullOrEmpty(imageUrl);
      }
      if (flag)
      {
        if (this.DefaultImageId != Guid.Empty)
        {
          this.ImageControl.Src = this.GetImageUrl(this.ProviderNameForDefaultImage, this.DefaultImageId, out altTag);
          this.ImageControl.Alt = altTag;
          if (!string.IsNullOrEmpty(this.ImageControl.Src))
          {
            this.DefaultSrc = this.ImageControl.Src;
            this.DefaultAlt = this.ImageControl.Alt;
          }
        }
        else if (!string.IsNullOrEmpty(this.DefaultSrc))
        {
          this.ImageControl.Src = this.DefaultSrc;
          this.ImageControl.Alt = this.DefaultAlt;
        }
      }
      if (!string.IsNullOrEmpty(this.ImageControl.Alt))
        return;
      this.ImageControl.Attributes["alt"] = string.Empty;
    }

    /// <summary>
    /// Gets the image URL from the specifieed id and provider.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="imageId">The image id.</param>
    /// <returns></returns>
    protected virtual string GetImageUrl(string providerName, Guid imageId, out string altTag) => this.GetImageUrl(providerName, imageId, out altTag, out Telerik.Sitefinity.Libraries.Model.Image _);

    protected virtual string GetImageUrl(
      string providerName,
      Guid imageId,
      out string altTag,
      out Telerik.Sitefinity.Libraries.Model.Image sourceImage)
    {
      Telerik.Sitefinity.Libraries.Model.Image image = LibrariesManager.GetManager(providerName).GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == imageId)).SingleOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
      sourceImage = image;
      if (image != null)
      {
        altTag = (string) image.AlternativeText;
        return image.MediaUrl;
      }
      altTag = string.Empty;
      return (string) null;
    }

    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      IDataItemContainer dataItemContainer = this.GetDataItemContainer();
      object component = (object) null;
      if (dataItemContainer != null)
        component = dataItemContainer.DataItem;
      if (component == null)
        return;
      PropertyDescriptor property = TypeDescriptor.GetProperties(component)[this.DataFieldName];
      if (this.IsDesignMode())
      {
        this.Value = property.GetValue(component);
      }
      else
      {
        try
        {
          this.Value = property.GetValue(component);
        }
        catch
        {
        }
      }
      this.DataItem = component as IDataItem;
      this.BoundOnServer = true;
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    /// <summary>Validates the dimensions uploaded image</summary>
    /// <param name="imageFile">the uploaded file</param>
    /// <param name="errorMessage">error message</param>
    /// <returns></returns>
    protected virtual bool ValidateFile(UploadedFile imageFile, out string errorMessage)
    {
      bool flag1 = false;
      bool flag2 = false;
      try
      {
        using (System.Drawing.Image img = System.Drawing.Image.FromStream(imageFile.InputStream))
        {
          int? nullable = this.MaxHeight;
          int num1 = 0;
          if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
          {
            int height = img.Height;
            nullable = this.MaxHeight;
            int valueOrDefault = nullable.GetValueOrDefault();
            if (height > valueOrDefault & nullable.HasValue)
              flag1 = false;
          }
          nullable = this.MaxWidth;
          int num2 = 0;
          if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
          {
            int width = img.Width;
            nullable = this.MaxWidth;
            int valueOrDefault = nullable.GetValueOrDefault();
            if (width > valueOrDefault & nullable.HasValue)
              flag2 = false;
          }
          if (flag2 & flag1)
          {
            errorMessage = "Image dimensions are larger than the maximum allowed - height:{0}, widht:{1}.".Arrange((object) this.MaxHeight, (object) this.MaxWidth);
            return false;
          }
          if (flag1)
          {
            errorMessage = "Image height is larger than the maximum allowed {0}.".Arrange((object) this.MaxHeight);
            return false;
          }
          if (flag2)
          {
            errorMessage = "Image width is larger than the maximum allowed {0}.".Arrange((object) this.MaxWidth);
            return false;
          }
          ImageCodecInfo imageCodecInfo = ((IEnumerable<ImageCodecInfo>) ImageCodecInfo.GetImageDecoders()).Where<ImageCodecInfo>((Func<ImageCodecInfo, bool>) (c => c.FormatID == img.RawFormat.Guid)).FirstOrDefault<ImageCodecInfo>();
          if (imageCodecInfo == null)
          {
            errorMessage = "Unknown image format.";
            return false;
          }
          if (!((IEnumerable<string>) imageCodecInfo.FilenameExtension.Split(';')).Select<string, string>((Func<string, string>) (ext => ext.Trim('*').ToLower())).Contains<string>(imageFile.GetExtension().ToLower()))
          {
            errorMessage = "Wrong file extension.";
            return false;
          }
          if (imageCodecInfo.MimeType.ToLower() != imageFile.ContentType.ToLower())
          {
            errorMessage = "Wrong mime type.";
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        errorMessage = ex.ToString();
        return false;
      }
      errorMessage = string.Empty;
      return true;
    }

    /// <summary>Handles the click on the delete image button</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void DeleteImage_Click(object sender, EventArgs e)
    {
      if (!(this.ImageId != Guid.Empty))
        return;
      LibrariesManager manager = LibrariesManager.GetManager(this.ImageProviderName);
      Telerik.Sitefinity.Libraries.Model.Image cnt = manager.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == this.ImageId)).SingleOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
      if (cnt == null)
        return;
      Telerik.Sitefinity.Libraries.Model.Image master = manager.GetMaster(cnt);
      manager.DeleteImage(master);
      manager.SaveChanges();
    }

    protected Telerik.Sitefinity.Libraries.Model.Image UploadNewImage(
      UploadedFile imageFile,
      string fileName)
    {
      AppSettings appSettings = App.Prepare().SetContentProvider(this.ImageProviderName);
      if (this.AlbumId == Guid.Empty)
        this.AlbumId = appSettings.WorkWith().Albums().First().Get().Id;
      return appSettings.WorkWith().Album(this.AlbumId).CreateImage().Do((Action<Telerik.Sitefinity.Libraries.Model.Image>) (img =>
      {
        img.Title = (Lstring) fileName;
        img.Description = (Lstring) fileName;
        img.AlternativeText = (Lstring) fileName;
        img.UrlName = (Lstring) Regex.Replace(fileName, "[^\\w_-]", "-").ToLower();
      })).SaveAndContinue().CheckOut().UploadContent(imageFile.InputStream, imageFile.GetExtension()).SaveAndContinue().Get();
    }
  }
}
