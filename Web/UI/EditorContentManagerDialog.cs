// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.EditorContentManagerDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// RadEditor Manager dialog for inserting image, document and video.
  /// </summary>
  public class EditorContentManagerDialog : MediaDialogBase
  {
    private string bodyCssClass = "sfSelectorDialog";
    private bool useOnlyUploadMode;
    private bool useOnlySelectMode;
    public Guid targetLibraryId = Guid.Empty;
    public Guid sourceLibraryId = Guid.Empty;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.EditorContentManagerDialog.ascx");
    private const string script = "Telerik.Sitefinity.Web.Scripts.EditorContentManagerDialog.js";
    private string uiCulture;
    private bool showOnlySystemLibraries;
    private bool useTitleFieldValueAsMediaItemTitle;
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
    private bool showProviderSelector = true;
    private bool bindOnLoad = true;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets an instance of <see cref="P:Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.LibrariesManager" /> used for initialization of the dialog.
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EditorContentManagerDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
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
    /// Represents css class to be added to the body tag of the dialog
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
        if (this.uiCulture == null)
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
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
    /// Gets or sets whether ProviderSelector should be visible
    /// </summary>
    internal bool ShowProviderSelector
    {
      get => this.showProviderSelector;
      set => this.showProviderSelector = value;
    }

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

    /// <summary>Gets the reference to the uploaderView control.</summary>
    protected virtual MediaContentUploaderView UploaderView => this.Container.GetControl<MediaContentUploaderView>("uploaderView", true);

    /// <summary>Gets the reference to the selectorView control.</summary>
    protected virtual MediaContentSelectorView SelectorView => this.Container.GetControl<MediaContentSelectorView>("selectorView", true);

    /// <summary>
    /// Gets the reference to the dialogModesSwitcher control.
    /// </summary>
    protected virtual ChoiceField DialogModesSwitcher => this.Container.GetControl<ChoiceField>("dialogModesSwitcher", true);

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

    /// <summary>Gets the reference to the save link button.</summary>
    protected virtual HyperLink SaveLink => this.Container.GetControl<HyperLink>("saveLink", true);

    /// <summary>Gets the reference to the cancel link button.</summary>
    /// <value>The cancel link button.</value>
    protected virtual HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>Gets the reference to the uploaderSection control.</summary>
    protected virtual HtmlGenericControl UploaderSection => this.Container.GetControl<HtmlGenericControl>("uploaderSection", true);

    /// <summary>Gets the reference to the selectorSection control.</summary>
    protected virtual HtmlGenericControl SelectorSection => this.Container.GetControl<HtmlGenericControl>("selectorSection", true);

    /// <summary>Represents the</summary>
    protected virtual Control CustomCssClasses => this.Container.GetControl<Control>("customCssClasses", true);

    /// <summary>
    /// Gets a reference to the InsertEditImageView that edits the
    /// </summary>
    protected virtual InsertEditImageView EditImageView => this.Container.GetControl<InsertEditImageView>("editImageView", true);

    /// <summary>
    /// Gets a reference to asp:Panel that contains the EditImageView
    /// </summary>
    protected virtual Panel EditImageSection => this.Container.GetControl<Panel>("editImageSection", true);

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
      this.InitializeItemType();
      this.UploaderView.TargetLibraryId = this.TargetLibraryId;
      this.UploaderView.UseTitleFieldValueAsMediaItemTitle = this.UseTitleFieldValueAsMediaItemTitle;
      this.SelectorView.SourceLibraryId = this.SourceLibraryId;
      this.SelectorView.ShowLibFilterWrp = this.SelectorViewShowLibFilterWrp;
      this.SelectorView.ShowBreadcrumb = this.SelectorViewShowBreadcrumb;
      this.SelectorView.ShowSearchBox = this.SelectorViewShowSearchBox;
      if (this.ShowProviderSelector)
        this.InitializeProvidersSelector();
      this.InitializeNoLibrariesWarning();
      this.SelectorView.BindOnLoad = this.BindOnLoad;
      this.UploaderView.BindOnLoad = this.BindOnLoad;
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
      ScriptControlDescriptor descriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      descriptor.Type = this.GetType().FullName;
      descriptor.AddComponentProperty("uploaderView", this.UploaderView.ClientID);
      descriptor.AddElementProperty("uploaderSection", this.UploaderSection.ClientID);
      descriptor.AddComponentProperty("dialogModesSwitcher", this.DialogModesSwitcher.ClientID);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        descriptor.AddProperty("uiCulture", (object) this.UICulture);
      descriptor.AddComponentProperty("selectorView", this.SelectorView.ClientID);
      descriptor.AddElementProperty("saveLink", this.SaveLink.ClientID);
      descriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      descriptor.AddElementProperty("titleLabel", ((Control) this.TitleLabel).ClientID);
      descriptor.AddElementProperty("cancelLinkTitle", ((Control) this.CancelLinkTitle).ClientID);
      descriptor.AddElementProperty("selectorSection", this.SelectorSection.ClientID);
      if (!string.IsNullOrEmpty(this.BodyCssClass))
        descriptor.AddProperty("_bodyCssClass", (object) this.BodyCssClass);
      descriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      descriptor.AddProperty("dialogMode", (object) this.DialogMode);
      descriptor.AddProperty("_useOnlyUploadMode", (object) this.UseOnlyUploadMode);
      descriptor.AddProperty("_useOnlySelectMode", (object) this.UseOnlySelectMode);
      IEnumerable<DataProviderBase> contextProviders = this.LibrariesManager.GetContextProviders();
      Dictionary<string, bool> dictionary1 = new Dictionary<string, bool>();
      Dictionary<string, bool> dictionary2 = dictionary1;
      string name1 = this.LibrariesManager.Provider.Name;
      int num1;
      if (!this.AllowCreateLibrary)
        num1 = this.LibrariesManager.Provider.IsGranted(this.permissionSet, this.action) ? 1 : 0;
      else
        num1 = 1;
      dictionary2[name1] = num1 != 0;
      foreach (DataProviderBase dataProviderBase in contextProviders)
      {
        Dictionary<string, bool> dictionary3 = dictionary1;
        string name2 = dataProviderBase.Name;
        int num2;
        if (!dataProviderBase.SecurityRoot.IsGranted(this.permissionSet, this.action))
          num2 = dataProviderBase.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary) ? 1 : 0;
        else
          num2 = 1;
        dictionary3[name2] = num2 != 0;
      }
      descriptor.AddProperty("isGrantedDictionary", (object) dictionary1);
      descriptor.AddProperty("_provider", (object) this.LibrariesManager.Provider.Name);
      if (!string.IsNullOrEmpty(this.SelectorViewTitle))
        descriptor.AddProperty("_selectorViewTitle", (object) this.SelectorViewTitle);
      this.AddImageDescriptors(descriptor);
      if (this.ProvidersSelector != null && this.ShowProviderSelector && this.ProvidersSelector.Visible)
        descriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      descriptor.AddProperty("_libraryNotSelectedErrorMessage", (object) Res.Get<LibrariesResources>().LibraryNotSelected);
      if (this.NoLibrariesWarning != null && this.NoLibrariesWarning.Visible && this.NoLibrariesOkButton != null)
        descriptor.AddElementProperty("noLibrariesOkButton", this.NoLibrariesOkButton.ClientID);
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      descriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        descriptor
      };
    }

    /// <summary>
    /// Adds script properties that are specific to the image manager.
    /// </summary>
    /// <param name="descriptor">The Script Descriptor object.</param>
    private void AddImageDescriptors(ScriptControlDescriptor descriptor)
    {
      if (!this.EditImageView.Visible)
        return;
      descriptor.AddElementProperty("editImageSection", this.EditImageSection.ClientID);
      descriptor.AddComponentProperty("editImageView", this.EditImageView.ClientID);
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
      scriptReferences.AddRange((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.EditorContentManagerDialog.js", typeof (EditorContentManagerDialog).Assembly.FullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void InitializeItemType()
    {
      this.InitializeDialogMode();
      this.InitializeViews();
    }

    private void InitializeDialogMode()
    {
      if (this.DialogMode != EditorExternalDialogModes.NotSet)
        return;
      string str = SystemManager.CurrentHttpContext.Request.QueryString["mode"];
      this.DialogMode = !string.IsNullOrEmpty(str) ? (EditorExternalDialogModes) Enum.Parse(typeof (EditorExternalDialogModes), str, true) : throw new ArgumentException("No dialog mode parameter specified. Use one of the Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.EditorExternalDialogModes as 'mode' query string parameter.");
      if (this.DialogMode == EditorExternalDialogModes.NotSet)
        throw new ArgumentOutOfRangeException("Invalid mode dialog parameter specified - " + (object) this.DialogMode);
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
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = true;
      string fullName1;
      string fullName2;
      string str1;
      string str2;
      string str3;
      string str4;
      string str5;
      string str6;
      string str7;
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
          str6 = Res.Get<ImagesResources>().Album;
          str7 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageItemDescriptionTemplate.htm";
          this.permissionSet = "Image";
          this.permissionSetLibrary = "Album";
          this.action = "ManageImage";
          this.actionLibrary = "CreateAlbum";
          this.relatedSecuredObject = this.LibrariesManager.SecurityRoot;
          this.AllowCreateLibrary = this.LibrariesManager.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary);
          this.InitializeEditImageView();
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
          str6 = Res.Get<LibrariesResources>().Library;
          str7 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DocumentItemDescriptionTemplate.htm";
          flag2 = true;
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
          str6 = Res.Get<LibrariesResources>().Library;
          str7 = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Videos.VideoItemDescriptionTemplate.htm";
          flag4 = false;
          this.permissionSet = "Video";
          this.permissionSetLibrary = "VideoLibrary";
          this.action = "ManageVideo";
          this.actionLibrary = "CreateVideoLibrary";
          this.relatedSecuredObject = this.LibrariesManager.SecurityRoot;
          this.AllowCreateLibrary = this.LibrariesManager.SecurityRoot.IsGranted(this.permissionSetLibrary, this.actionLibrary);
          flag3 = true;
          break;
        default:
          throw new NotSupportedException();
      }
      this.UploaderView.ItemName = str3;
      this.UploaderView.LibrarySelectorItemName = str4;
      this.UploaderView.LibraryName = str6;
      if (this.UseOnlyUploadMode || this.UseOnlySelectMode)
        this.DialogModesSwitcher.Visible = false;
      this.UploaderView.ProviderName = this.LibrariesManager.Provider.Name;
      this.UploaderView.ContentType = fullName1;
      this.UploaderView.ParentType = fullName2;
      this.UploaderView.ItemsName = str2;
      this.UploaderView.ItemNameWithArticle = str5;
      this.UploaderView.DisplayAltTextField = flag1;
      this.UploaderView.DisplayTitleTextField = flag2;
      this.UploaderView.DisplayResizingOptionsControl = flag3;
      this.UploaderView.ShowOpenOriginalSizeCheckBox = flag4;
      ChoiceItem choiceItem = this.DialogModesSwitcher.Choices.Where<ChoiceItem>((Func<ChoiceItem, bool>) (c => c.Value == "2")).SingleOrDefault<ChoiceItem>();
      if (choiceItem != null)
        choiceItem.Text = string.Format(Res.Get<LibrariesResources>().FromAlreadyUploadedItems, (object) str2.ToLower());
      this.SelectorView.ProviderName = this.SelectViewProviderName;
      this.SelectorView.ContentType = fullName1;
      this.SelectorView.ParentType = fullName2;
      this.SelectorView.MediaContentBinderServiceUrl = str1;
      this.SelectorView.ItemsName = str2;
      this.SelectorView.ItemName = str3;
      this.SelectorView.ItemNameWithArticle = str5;
      this.SelectorView.MediaContentItemsListDescriptionTemplate = str7;
      this.SelectorView.DisplayResizingOptionsControl = flag3;
      this.SelectorView.ShowOpenOriginalSizeCheckBox = flag4;
      this.TitleLabel.Text = string.Format(Res.Get<LibrariesResources>().InsertAItem, (object) str5);
      this.SaveLinkTitle.Text = string.Format(Res.Get<LibrariesResources>().InsertItem, (object) str3.ToLower());
    }

    private void InitializeEditImageView()
    {
      this.EditImageView.Visible = true;
      this.EditImageView.HostedInRadWindow = this.HostedInRadWindow;
      this.EditImageView.ViewType = this.GetViewType();
      this.EditImageSection.Visible = true;
    }

    private string GetViewType()
    {
      if (this.Page != null)
      {
        Uri urlReferrer = this.Page.Request.UrlReferrer;
        if (urlReferrer != (Uri) null && ((IEnumerable<string>) urlReferrer.Segments).Last<string>() == "PropertyEditor")
        {
          string str = ((IEnumerable<string>) this.Page.Request.UrlReferrer.Query.Split("?&".ToCharArray(), StringSplitOptions.None)).Where<string>((Func<string, bool>) (e => e.StartsWith("Id="))).FirstOrDefault<string>();
          if (!string.IsNullOrEmpty(str))
          {
            string g = str.Substring("Id=".Length);
            return PageManager.GetManager().GetControl<ControlData>(new Guid(g)).ObjectType;
          }
        }
      }
      return (string) null;
    }

    private void InitializeProvidersSelector()
    {
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
  }
}
