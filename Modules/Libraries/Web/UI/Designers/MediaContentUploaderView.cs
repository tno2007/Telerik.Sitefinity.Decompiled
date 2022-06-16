// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A base upload view for all media content controls, letting you upload a new file.
  /// </summary>
  public class MediaContentUploaderView : SimpleScriptView
  {
    private bool showOpenOriginalSizeCheckBox = true;
    private Guid targetLibraryId = Guid.Empty;
    private bool allowCreateLibrary = true;
    private bool useTitleFieldValueAsMediaItemTitle;
    private bool bindOnLoad = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.MediaContentUploaderView.ascx");
    private const string uploadServiceUrl = "~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx";
    private const string MediaContentJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentUploaderView.js";
    private const string AjaxUploadJs = "Telerik.Sitefinity.Resources.Scripts.ajaxupload.js";
    private static Dictionary<Type, string> libraryServices = new Dictionary<Type, string>()
    {
      {
        typeof (Album),
        "~/Sitefinity/Services/Content/AlbumService.svc/"
      },
      {
        typeof (VideoLibrary),
        "~/Sitefinity/Services/Content/VideoLibraryService.svc/"
      },
      {
        typeof (DocumentLibrary),
        "~/Sitefinity/Services/Content/DocumentLibraryService.svc/"
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MediaContentUploaderView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; set; }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public string ParentType { get; set; }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The type of the content.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    public string ItemsName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    internal string LibrarySelectorItemName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular with an article in front of it (for example an image).
    /// </summary>
    public string ItemNameWithArticle { get; set; }

    /// <summary>
    /// Gets the localizable string that represents the name of the library.
    /// </summary>
    public string LibraryName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether ResizingOptionsControl control should be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if ResizingOptionsControl control should be displayed; otherwise, <c>false</c>.
    /// </value>
    public bool DisplayResizingOptionsControl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether AltTextField control should be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if AltTextField control should be displayed; otherwise, <c>false</c>.
    /// </value>
    public bool DisplayAltTextField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether TitleTextField control should be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if TitleTextField control should be displayed; otherwise, <c>false</c>.
    /// </value>
    public bool DisplayTitleTextField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the "open original size" check box.
    /// </summary>
    public bool ShowOpenOriginalSizeCheckBox
    {
      get => this.showOpenOriginalSizeCheckBox;
      set => this.showOpenOriginalSizeCheckBox = value;
    }

    /// <summary>
    /// Gets or sets the service URL used for the folder field.
    /// </summary>
    public string FolderFieldServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets a custom value indicating where to save the uploaded file.
    /// </summary>
    public Guid TargetLibraryId
    {
      get => this.targetLibraryId;
      set => this.targetLibraryId = value;
    }

    /// <summary>
    /// Gets or sets a boolean value to allow the creation of a new library.
    /// </summary>
    public bool AllowCreateLibrary
    {
      get => this.allowCreateLibrary;
      set => this.allowCreateLibrary = value;
    }

    /// <summary>Gets or sets the name of the library type.</summary>
    public string LibraryTypeName { get; set; }

    /// <summary>Gets or sets the create library service URL.</summary>
    public string CreateLibraryServiceUrl { get; set; }

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

    /// <summary>Text box that shows the selected file name.</summary>
    protected internal virtual TextBox FileNameTextBox => this.Container.GetControl<TextBox>("fileNameTextBox", true);

    /// <summary>
    /// The button that invokes the select file functionality.
    /// </summary>
    protected internal virtual LinkButton SelectFileButton => this.Container.GetControl<LinkButton>("selectFileButton", true);

    /// <summary>Gets the reference to the settings panel.</summary>
    protected virtual Panel SettingsPanel => this.Container.GetControl<Panel>("settingsPanel", true);

    /// <summary>
    /// Represents the text field containig the AlternativeText to be set to the image.
    /// </summary>
    protected virtual TextField AltTextField => this.Container.GetControl<TextField>("altTextField", true);

    /// <summary>
    /// Represents the text field containig the title to be set to the document.
    /// </summary>
    protected virtual TextField TitleTextField => this.Container.GetControl<TextField>("titleTextField", true);

    /// <summary>Gets the reference to resizingOptionsControl.</summary>
    protected virtual ResizingOptionsControl ResizingOptionsControl => this.Container.GetControl<ResizingOptionsControl>("resizingOptionsControl", true);

    /// <summary>Gets the reference to whichItemToUploadLabel control.</summary>
    protected virtual ITextControl WhichItemToUploadLabel => this.Container.GetControl<ITextControl>("whichItemToUploadLabel", true);

    /// <summary>Gets a reference to the parent library selector.</summary>
    protected virtual FolderField ParentLibrarySelector => this.Container.GetControl<FolderField>("parentLibrarySelector", true);

    /// <summary>Represents the label of the library selector.</summary>
    protected virtual SitefinityLabel LibrarySelectorTitle => this.Container.GetControl<SitefinityLabel>("librarySelectorTitle", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.AltTextField.Visible = this.DisplayAltTextField;
      this.TitleTextField.Visible = this.DisplayTitleTextField;
      if (this.DisplayResizingOptionsControl)
      {
        this.ResizingOptionsControl.ItemName = this.ItemName;
        this.ResizingOptionsControl.ItemsName = this.ItemsName;
        this.ResizingOptionsControl.ShowOpenOriginalSizeCheckBox = this.ShowOpenOriginalSizeCheckBox;
      }
      else
        this.ResizingOptionsControl.Visible = false;
      this.WhichItemToUploadLabel.Text = string.Format(Res.Get<LibrariesResources>().WhichItemToUpload, (object) this.ItemName.ToLower());
      this.LibrarySelectorTitle.Text = string.Format(Res.Get<LibrariesResources>().WhereToStoreTheUploadedItem, (object) this.ItemName.ToLower());
      this.ParentLibrarySelector.WebServiceUrl = this.FolderFieldServiceUrl;
      this.ParentLibrarySelector.LibraryTypeName = this.LibraryTypeName;
      this.ParentLibrarySelector.CreateLibraryServiceUrl = this.CreateLibraryServiceUrl;
      this.ParentLibrarySelector.ItemName = this.LibrarySelectorItemName;
      this.ParentLibrarySelector.ShowCreateNewLibraryButton = this.AllowCreateLibrary;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_uploadServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx"));
      controlDescriptor.AddProperty("_contentType", (object) this.ContentType);
      controlDescriptor.AddProperty("_parentType", (object) this.ParentType);
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("_selectFileButtonText", (object) Res.Get<Labels>().ChangeDotDotDot);
      controlDescriptor.AddProperty("_selectLabel", (object) Res.Get<Labels>().SelectDotDotDot);
      controlDescriptor.AddProperty("_targetLibraryId", (object) this.TargetLibraryId);
      controlDescriptor.AddProperty("_emptyGuid", (object) Guid.Empty);
      controlDescriptor.AddElementProperty("fileNameTextBox", this.FileNameTextBox.ClientID);
      controlDescriptor.AddElementProperty("selectFileButton", this.SelectFileButton.ClientID);
      controlDescriptor.AddElementProperty("settingsPanel", this.SettingsPanel.ClientID);
      if (this.DisplayAltTextField)
        controlDescriptor.AddComponentProperty("altTextField", this.AltTextField.ClientID);
      if (this.DisplayResizingOptionsControl)
        controlDescriptor.AddComponentProperty("resizingOptionsControl", this.ResizingOptionsControl.ClientID);
      if (this.DisplayTitleTextField)
        controlDescriptor.AddComponentProperty("titleTextField", this.TitleTextField.ClientID);
      controlDescriptor.AddProperty("_cantUploadFilesErrorMessage", (object) Res.Get<LibrariesResources>().CantUploadFiles);
      Type type = TypeResolutionService.ResolveType(this.ContentType);
      controlDescriptor.AddProperty("_fileAllowedExtensions", (object) this.GetFileExtensionFilter(type.Name));
      controlDescriptor.AddElementProperty("librarySelectorTitle", this.LibrarySelectorTitle.ClientID);
      controlDescriptor.AddComponentProperty("parentLibrarySelector", this.ParentLibrarySelector.ClientID);
      controlDescriptor.AddProperty("_bindOnLoad", (object) this.BindOnLoad);
      controlDescriptor.AddProperty("useTitleFieldValueAsMediaItemTitle", (object) this.UseTitleFieldValueAsMediaItemTitle);
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
      string assembly = typeof (MediaContentUploaderView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.ajaxupload.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentUploaderView.js", assembly)
      };
    }

    protected string GetFileExtensionFilter(string contentType)
    {
      string fileExtensionFilter = string.Empty;
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (contentType != null)
      {
        string lower = contentType.ToLower();
        if (!(lower == "image"))
        {
          if (!(lower == "video"))
          {
            if (lower == "document")
            {
              bool? allowedExensions = librariesConfig.Documents.AllowedExensions;
              bool flag = true;
              if (allowedExensions.GetValueOrDefault() == flag & allowedExensions.HasValue)
                fileExtensionFilter = librariesConfig.Documents.AllowedExensionsSettings;
            }
          }
          else
            fileExtensionFilter = librariesConfig.Videos.AllowedExensionsSettings;
        }
        else
          fileExtensionFilter = librariesConfig.Images.AllowedExensionsSettings;
      }
      return fileExtensionFilter;
    }
  }
}
