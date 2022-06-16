// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// RadEditor Manager dialog for inserting image, document and video.
  /// </summary>
  public class SingleMediaContentItemDialog : MediaDialogBase
  {
    private string bodyCssClass;
    private bool useOnlyUploadMode;
    private bool useOnlySelectMode;
    private Guid targetLibraryId = Guid.Empty;
    private Guid sourceLibraryId = Guid.Empty;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.SingleMediaContentItemDialog.ascx");
    private const string script = "Telerik.Sitefinity.Web.Scripts.SingleMediaContentItemDialog.js";
    private const string jqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private string uiCulture;
    private bool showOnlySystemLibraries;
    private bool useTitleFieldValueAsMediaItemTitle;
    private MediaSelectorOpenMode mediaDialogOpenMode;
    private string permissionSet = string.Empty;
    private string action = string.Empty;
    private string permissionSetLibrary = string.Empty;
    private string actionLibrary = string.Empty;
    private ISecuredObject relatedSecuredObject;
    private LibrariesManager librariesManager;
    private const string albumServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/";
    private const string videoLibraryServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/";
    private const string documentLibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
    private bool selectorViewShowLibFilterWrp = true;
    private bool selectorViewShowBreadcrumb = true;
    private bool selectorViewShowSearchBox = true;
    private bool bindOnLoad = true;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets an instance of <see cref="P:Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.LibrariesManager" /> used for initialization of the dialog.
    /// </summary>
    public override LibrariesManager LibrariesManager
    {
      get
      {
        if (this.librariesManager == null)
          this.librariesManager = LibrariesManager.GetManager(this.UploadViewProviderName);
        return this.librariesManager;
      }
    }

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => this.GetType().FullName;

    /// <summary>
    /// Determines what screen will be shown when the selector is opened - upload or select.
    /// The default value is Upload.
    /// </summary>
    /// <value>The media dialog open mode.</value>
    public MediaSelectorOpenMode MediaDialogOpenMode
    {
      get => this.mediaDialogOpenMode;
      set => this.mediaDialogOpenMode = value;
    }

    /// <summary>
    /// Gets or sets the whether to show only system libraries or not
    /// </summary>
    public bool ShowOnlySystemLibraries
    {
      get => this.showOnlySystemLibraries;
      set => this.showOnlySystemLibraries = value;
    }

    /// <summary>
    /// Gets or sets a boolean value to allow the creation of a new library.
    /// </summary>
    public bool AllowCreateLibrary { get; set; }

    /// <summary>
    /// Represents css class to be added to the container of the dialog
    /// </summary>
    public string BodyCssClass
    {
      get => this.bodyCssClass;
      set => this.bodyCssClass = value;
    }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null && SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
          if (this.uiCulture == null)
            this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["propertyValueCulture"];
          if (this.uiCulture == null)
            this.uiCulture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.ToString();
        }
        return this.uiCulture;
      }
      set => this.uiCulture = value;
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
    /// Gets or sets the id of the uploaded content library which will be displayed in selector view.
    /// </summary>
    /// <value>The id of the uploaded content library.</value>
    public Guid SourceLibraryId
    {
      get => this.sourceLibraryId;
      set => this.sourceLibraryId = value;
    }

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
    /// Gets or sets the option to hide options switcher and use only select option.
    /// </summary>
    /// <value>The flag, indicating if only select mode will be used.</value>
    public bool UseOnlySelectMode
    {
      get => this.useOnlySelectMode;
      set => this.useOnlySelectMode = value;
    }

    /// <summary>Gets or sets the upload view provider name.</summary>
    /// <value>The provider name.</value>
    public string UploadViewProviderName { get; set; }

    /// <summary>Gets or sets the select view provider name.</summary>
    /// <value>The provider name.</value>
    public string SelectViewProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether providers selector control will be hidden.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if providers selector control is hidden otherwise, <c>false</c>.
    /// </value>
    public bool HideProvidersSelector { get; set; }

    /// <summary>
    /// Gets or sets whether library filter wrapper of SelectorView should be visible
    /// </summary>
    internal bool SelectorViewShowLibFilterWrp
    {
      get => this.selectorViewShowLibFilterWrp;
      set => this.selectorViewShowLibFilterWrp = value;
    }

    /// <summary>
    /// Gets or sets whether breadcrumb  of SelectorView should be visible
    /// </summary>
    internal bool SelectorViewShowBreadcrumb
    {
      get => this.selectorViewShowBreadcrumb;
      set => this.selectorViewShowBreadcrumb = value;
    }

    /// <summary>
    /// Gets or sets whether search box of SelectorView should be visible
    /// </summary>
    internal bool SelectorViewShowSearchBox
    {
      get => this.selectorViewShowSearchBox;
      set => this.selectorViewShowSearchBox = value;
    }

    /// <summary>Gets or sets the title of the selector view</summary>
    internal string SelectorViewTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the control and it's child controls
    /// when the page loads.
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the value of the title field as a title for the media item when uploading files.
    /// </summary>
    public bool UseTitleFieldValueAsMediaItemTitle
    {
      get => this.useTitleFieldValueAsMediaItemTitle;
      set => this.useTitleFieldValueAsMediaItemTitle = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to add the culture of the property editor to the filter.
    /// </summary>
    /// <value><c>true</c> if the culture will be added to the filter; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// If not set a value, the property will return the value of the <see cref="M:Telerik.Sitefinity.Model.IAppSettings.Multilingual" />.;
    /// </remarks>
    public bool AddCultureToFilter { get; set; }

    /// <summary>
    /// Gets a value indicating whether One Click Upload function is enabled.
    /// </summary>
    public bool EnableOneClickUpload => Config.Get<LibrariesConfig>().EnableOneClickUpload;

    /// <summary>Gets the reference to the uploaderView control.</summary>
    protected virtual SingleMediaContentItemUploaderView UploaderView => this.Container.GetControl<SingleMediaContentItemUploaderView>("uploaderView", true);

    /// <summary>Gets the reference to the selectorView control.</summary>
    protected virtual SingleMediaContentItemSelectorView SelectorView => this.Container.GetControl<SingleMediaContentItemSelectorView>("selectorView", true);

    /// <summary>
    /// Gets the reference to the filter panel (filtering by libraries, taxonomies, etc).
    /// </summary>
    /// <value>The filter panel.</value>
    protected virtual MediaSelectorFilterPanel FilterPanel => this.Container.GetControl<MediaSelectorFilterPanel>("filterPanel", true);

    /// <summary>Gets the reference to the title label.</summary>
    /// <value>The title label.</value>
    protected virtual ITextControl TitleLabel => this.Container.GetControl<ITextControl>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the title label of the save link.
    /// </summary>
    protected virtual ITextControl SaveLinkTitle => this.Container.GetControl<ITextControl>("saveLinkTitle", true);

    /// <summary>
    /// Gets the reference to the title label of the cancel link.
    /// </summary>
    protected virtual ITextControl CancelLinkTitle => this.Container.GetControl<ITextControl>("cancelLinkTitle", true);

    /// <summary>
    /// Gets the reference to the title label of the back link.
    /// </summary>
    protected virtual ITextControl BackLinkTitle => this.Container.GetControl<ITextControl>("backLinkTitle", true);

    /// <summary>Gets the reference to the button area.</summary>
    /// <value>The button area.</value>
    protected virtual Control ButtonArea => this.Container.GetControl<Control>("buttonArea", true);

    /// <summary>Gets the reference to the save link button.</summary>
    protected virtual HyperLink SaveLink => this.Container.GetControl<HyperLink>("saveLink", true);

    /// <summary>Gets the reference to the cancel link button.</summary>
    /// <value>The cancel link button.</value>
    protected virtual HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>Gets the reference to the back link button.</summary>
    /// <value>The back link button.</value>
    protected virtual HyperLink BackLink => this.Container.GetControl<HyperLink>("backLink", true);

    /// <summary>Gets the reference to the uploaderSection control.</summary>
    protected virtual HtmlGenericControl UploaderSection => this.Container.GetControl<HtmlGenericControl>("uploaderSection", true);

    /// <summary>Gets the reference to the selectorSection control.</summary>
    protected virtual HtmlGenericControl SelectorSection => this.Container.GetControl<HtmlGenericControl>("selectorSection", true);

    /// <summary>Gets the filter section.</summary>
    /// <value>The filter section.</value>
    protected virtual HtmlGenericControl FilterSection => this.Container.GetControl<HtmlGenericControl>("filterSection", true);

    /// <summary>Represents the</summary>
    protected virtual Control CustomCssClasses => this.Container.GetControl<Control>("customCssClasses", true);

    /// <summary>
    /// The control that provides localization on the client side
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", false);

    /// <summary>Gets the instance of the provider selector control.</summary>
    protected ProvidersSelector ProvidersSelector => this.Container.GetControl<ProvidersSelector>("providersSelector", false);

    /// <summary>Gets the no libraries ok button.</summary>
    protected virtual LinkButton NoLibrariesOkButton => this.Container.GetControl<LinkButton>("noLibrariesOkButton", false);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!SystemManager.IsModuleAccessible("Libraries"))
      {
        this.UseOnlySelectMode = true;
        this.UseOnlyUploadMode = false;
      }
      this.InitializeItemType();
      this.UploaderView.UICulture = this.UICulture;
      this.UploaderView.TargetLibraryId = this.TargetLibraryId;
      this.SelectorView.UICulture = this.UICulture;
      this.SelectorView.SourceLibraryId = this.SourceLibraryId;
      this.SelectorView.ShowBreadcrumb = this.SelectorViewShowBreadcrumb;
      this.SelectorView.ShowSearchBox = this.SelectorViewShowSearchBox;
      this.InitializeProvidersSelector();
      this.InitializeNoLibrariesWarning();
      this.SelectorView.BindOnLoad = this.BindOnLoad;
      this.FilterPanel.BindOnLoad = this.BindOnLoad;
      this.MediaDialogOpenMode = this.EnableOneClickUpload ? MediaSelectorOpenMode.Select : MediaSelectorOpenMode.Upload;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("uploaderView", this.UploaderView.ClientID);
      controlDescriptor.AddElementProperty("uploaderSection", this.UploaderSection.ClientID);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      controlDescriptor.AddComponentProperty("selectorView", this.SelectorView.ClientID);
      controlDescriptor.AddElementProperty("saveLink", this.SaveLink.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("titleLabel", ((Control) this.TitleLabel).ClientID);
      controlDescriptor.AddElementProperty("cancelLinkTitle", ((Control) this.CancelLinkTitle).ClientID);
      controlDescriptor.AddElementProperty("backLinkTitle", ((Control) this.BackLinkTitle).ClientID);
      controlDescriptor.AddElementProperty("buttonArea", this.ButtonArea.ClientID);
      controlDescriptor.AddElementProperty("selectorSection", this.SelectorSection.ClientID);
      controlDescriptor.AddElementProperty("filterSection", this.FilterSection.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddProperty("_bodyCssClass", (object) this.BodyCssClass);
      controlDescriptor.AddProperty("dialogMode", (object) this.DialogMode);
      controlDescriptor.AddProperty("mediaDialogOpenMode", (object) this.MediaDialogOpenMode);
      controlDescriptor.AddProperty("_useOnlyUploadMode", (object) this.UseOnlyUploadMode);
      controlDescriptor.AddProperty("_useOnlySelectMode", (object) this.UseOnlySelectMode);
      controlDescriptor.AddProperty("_sourceLibraryId", (object) this.SourceLibraryId);
      controlDescriptor.AddProperty("_enableOneClickUpload", (object) this.EnableOneClickUpload);
      IEnumerable<DataProviderBase> contextProviders = this.LibrariesManager.GetContextProviders();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      SingleMediaContentItemDialog.Permission permission1 = new SingleMediaContentItemDialog.Permission()
      {
        Upload = (this.LibrariesManager.Provider.IsGranted(this.permissionSet, this.action) ? 1 : 0) != 0,
        CreateLibrary = this.AllowCreateLibrary
      };
      dictionary[this.LibrariesManager.Provider.Name] = permission1.ToJson<SingleMediaContentItemDialog.Permission>();
      foreach (DataProviderBase dataProviderBase in contextProviders)
      {
        SingleMediaContentItemDialog.Permission permission2 = new SingleMediaContentItemDialog.Permission()
        {
          Upload = (dataProviderBase.SecurityRoot.IsGranted(this.permissionSet, this.action) ? 1 : 0) != 0,
          CreateLibrary = (dataProviderBase.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary) ? 1 : 0) != 0
        };
        dictionary[dataProviderBase.Name] = permission2.ToJson<SingleMediaContentItemDialog.Permission>();
      }
      controlDescriptor.AddProperty("isGrantedDictionary", (object) dictionary);
      controlDescriptor.AddProperty("_provider", (object) this.LibrariesManager.Provider.Name);
      if (!string.IsNullOrEmpty(this.SelectorViewTitle))
        controlDescriptor.AddProperty("_selectorViewTitle", (object) this.SelectorViewTitle);
      if (this.ProvidersSelector != null && !this.HideProvidersSelector && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      controlDescriptor.AddProperty("_libraryNotSelectedErrorMessage", (object) Res.Get<LibrariesResources>().LibraryNotSelected);
      if (this.NoLibrariesWarning != null && this.NoLibrariesWarning.Visible && this.NoLibrariesOkButton != null)
        controlDescriptor.AddElementProperty("noLibrariesOkButton", this.NoLibrariesOkButton.ClientID);
      if (this.FilterPanel != null)
        controlDescriptor.AddComponentProperty("filterPanel", this.FilterPanel.ClientID);
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.SingleMediaContentItemDialog.js", typeof (SingleMediaContentItemDialog).Assembly.FullName));
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.JQueryUI;

    private void InitializeItemType()
    {
      this.InitializeDialogMode();
      this.InitializeViews();
    }

    private void InitializeDialogMode()
    {
      if (this.DialogMode == EditorExternalDialogModes.NotSet)
      {
        string str = SystemManager.CurrentHttpContext.Request.QueryString["mode"];
        this.DialogMode = !string.IsNullOrEmpty(str) ? (EditorExternalDialogModes) Enum.Parse(typeof (EditorExternalDialogModes), str, true) : throw new ArgumentException("No dialog mode parameter specified. Use one of the Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.EditorExternalDialogModes as 'mode' query string parameter.");
        if (this.DialogMode == EditorExternalDialogModes.NotSet)
          throw new ArgumentOutOfRangeException("Invalid mode dialog parameter specified - " + (object) this.DialogMode);
      }
      MediaSelectorOpenMode result;
      if (!Enum.TryParse<MediaSelectorOpenMode>(SystemManager.CurrentHttpContext.Request.QueryString["dialogOpenMode"], out result))
        return;
      this.MediaDialogOpenMode = result;
    }

    private void InitializeViews()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string empty5 = string.Empty;
      string empty6 = string.Empty;
      string empty7 = string.Empty;
      string empty8 = string.Empty;
      string empty9 = string.Empty;
      string fullName1;
      string fullName2;
      string str1;
      string str2;
      string str3;
      string str4;
      string str5;
      string str6;
      switch (this.DialogMode)
      {
        case EditorExternalDialogModes.NotSet:
          throw new ArgumentOutOfRangeException("Invalid mode dialog parameter specified - " + (object) this.DialogMode);
        case EditorExternalDialogModes.Image:
          fullName1 = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName;
          fullName2 = typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName;
          this.SelectorView.LibraryServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
          this.UploaderView.FolderFieldServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
          this.UploaderView.LibraryTypeName = fullName2;
          this.UploaderView.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/";
          str1 = "~/Sitefinity/Services/Content/ImageService.svc/";
          str2 = Res.Get<ImagesResources>().Images;
          str3 = Res.Get<ImagesResources>().Image;
          str4 = Res.Get<LibrariesResources>().ImageItemName;
          str5 = Res.Get<ImagesResources>().ImageWithArticle;
          string album = Res.Get<ImagesResources>().Album;
          str6 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageItemDescriptionTemplate.htm";
          this.permissionSet = "Image";
          this.permissionSetLibrary = "Album";
          this.action = "ManageImage";
          this.actionLibrary = "CreateAlbum";
          this.relatedSecuredObject = this.LibrariesManager.SecurityRoot;
          this.AllowCreateLibrary = this.LibrariesManager.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary);
          break;
        case EditorExternalDialogModes.Document:
          fullName1 = typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName;
          fullName2 = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName;
          this.SelectorView.LibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
          this.UploaderView.FolderFieldServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
          this.UploaderView.LibraryTypeName = fullName2;
          this.UploaderView.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
          str1 = "~/Sitefinity/Services/Content/DocumentService.svc/";
          str2 = Res.Get<DocumentsResources>().Documents;
          str3 = Res.Get<DocumentsResources>().Document;
          str4 = Res.Get<LibrariesResources>().DocumentItemName;
          str5 = Res.Get<DocumentsResources>().DocumentWithArticle;
          string library1 = Res.Get<LibrariesResources>().Library;
          str6 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DocumentItemDescriptionTemplate.htm";
          this.permissionSet = "Document";
          this.permissionSetLibrary = "DocumentLibrary";
          this.action = "ManageDocument";
          this.actionLibrary = "CreateDocumentLibrary";
          this.relatedSecuredObject = this.LibrariesManager.SecurityRoot;
          this.AllowCreateLibrary = this.LibrariesManager.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary);
          break;
        case EditorExternalDialogModes.Media:
          fullName1 = typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName;
          fullName2 = typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName;
          this.SelectorView.LibraryServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
          this.UploaderView.FolderFieldServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
          this.UploaderView.LibraryTypeName = fullName2;
          this.UploaderView.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/";
          str1 = "~/Sitefinity/Services/Content/VideoService.svc/";
          str2 = Res.Get<VideosResources>().Videos;
          str3 = Res.Get<VideosResources>().Video;
          str4 = Res.Get<LibrariesResources>().VideoItemName;
          str5 = Res.Get<VideosResources>().VideoWithArticle;
          string library2 = Res.Get<LibrariesResources>().Library;
          str6 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Videos.VideoItemDescriptionTemplate.htm";
          this.permissionSet = "Video";
          this.permissionSetLibrary = "VideoLibrary";
          this.action = "ManageVideo";
          this.actionLibrary = "CreateVideoLibrary";
          this.relatedSecuredObject = this.LibrariesManager.SecurityRoot;
          this.AllowCreateLibrary = this.LibrariesManager.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary);
          break;
        default:
          throw new NotSupportedException();
      }
      this.AddCultureToFilter = false;
      this.UploaderView.ItemName = str3;
      this.UploaderView.LibrarySelectorItemName = str4;
      this.UploaderView.ProviderName = this.LibrariesManager.Provider.Name;
      this.UploaderView.ContentType = fullName1;
      this.UploaderView.ParentType = fullName2;
      this.UploaderView.ItemsName = str2;
      this.UploaderView.ItemNameWithArticle = str5;
      this.SelectorView.ProviderName = this.SelectViewProviderName;
      this.SelectorView.ContentType = fullName1;
      this.SelectorView.ParentType = fullName2;
      this.SelectorView.MediaContentBinderServiceUrl = str1;
      this.SelectorView.ItemsName = str2;
      this.SelectorView.ItemName = str3;
      this.SelectorView.ItemNameWithArticle = str5;
      this.SelectorView.MediaContentItemsListDescriptionTemplate = str6;
      this.SelectorView.AddCultureToFilter = this.AddCultureToFilter;
      this.TitleLabel.Text = str3;
      this.SaveLinkTitle.Text = string.Format(Res.Get<LibrariesResources>().InsertItem, (object) str3.ToLower());
      this.FilterPanel.LibraryServiceUrl = this.SelectorView.LibraryServiceUrl;
      this.FilterPanel.ItemsName = str2;
      this.FilterPanel.ItemName = str3;
      this.FilterPanel.ProviderName = this.SelectViewProviderName;
      this.FilterPanel.UseOnlySelectMode = this.UseOnlySelectMode;
      this.FilterPanel.TargetLibraryId = this.TargetLibraryId;
      if (this.UseOnlySelectMode)
      {
        this.UploaderView.Visible = false;
      }
      else
      {
        if (!this.UseOnlyUploadMode)
          return;
        this.FilterSection.Visible = false;
        this.SelectorView.Visible = false;
      }
    }

    private void InitializeProvidersSelector()
    {
      string str = SystemManager.CurrentHttpContext.Request.QueryString["hideProvidersSelector"];
      if (!string.IsNullOrEmpty(str))
      {
        bool result;
        if (!bool.TryParse(str, out result))
          throw new ArgumentException("You should provide true/false value for the parameter 'hideProvidersSelector'");
        this.HideProvidersSelector = result;
      }
      if (this.ProvidersSelector == null)
        return;
      if (this.HideProvidersSelector)
      {
        this.ProvidersSelector.Visible = false;
      }
      else
      {
        LibrariesManager manager = LibrariesManager.GetManager();
        this.ProvidersSelector.Manager = (IManager) manager;
        this.ProvidersSelector.SelectedProviderName = manager.Provider.Name;
      }
    }

    [DataContract]
    private class Permission
    {
      [DataMember]
      public bool Upload { get; set; }

      [DataMember]
      public bool CreateLibrary { get; set; }
    }
  }
}
