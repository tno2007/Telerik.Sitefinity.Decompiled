// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FileField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Images;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A field control for representing and managing files.</summary>
  public class FileField : FieldControl
  {
    private string uploadServiceUrl = "~/Telerik.Sitefinity.Html5UploadHandler.ashx";
    private bool updateItemsList = true;
    private const string itemTemplateName = "FileDescription";
    private const string resourceAssemblyName = "Telerik.Sitefinity.Resources";
    private const string fileDesriptionTemplate = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DocumentItemInfoTemplate.htm";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.FileField.ascx");
    private static readonly string serviceUrlBase = VirtualPathUtility.ToAbsolute(VirtualPathUtility.RemoveTrailingSlash("~/Sitefinity/Services/Content/DocumentService.svc/"));
    private readonly string getMediaFileLinksServiceUrl = FileField.serviceUrlBase + "/mediaFileLinks";
    private readonly string copyMediaFileLinkServiceUrl = FileField.serviceUrlBase + "/copyFileLink";
    private const string fileFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FileField.js";
    internal const string selfExecutableScript = "Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FileField" /> class.
    /// </summary>
    public FileField() => this.LayoutTemplatePath = FileField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the item.</summary>
    /// <value>The name of the item.</value>
    public string ItemName { get; set; }

    /// <summary>Gets or sets the item name in plural.</summary>
    /// <value>The item name in plural.</value>
    public string ItemNamePlural { get; set; }

    /// <summary>Gets or sets the upload service URL.</summary>
    /// <value>The upload service URL.</value>
    public string UploadServiceUrl
    {
      get => this.uploadServiceUrl;
      set => this.uploadServiceUrl = value;
    }

    /// <summary>Gets or sets the name of the library provider.</summary>
    /// <value>The name of the library provider.</value>
    public string LibraryProviderName { get; set; }

    /// <summary>Gets or sets the type of the library content.</summary>
    /// <value>The type of the library content.</value>
    public Type LibraryContentType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the uploader allows multiple files to be selected.
    /// </summary>
    public bool IsMultiselect { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed number of files selected in the uploader.
    /// </summary>
    public int MaxFileCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the uploader is used by the SingleMediaContentItemUploaderView (a little bit hack).
    /// This flag ensures that the control behavior is persisted when used outside the SingleMediaContentItemUploaderView
    /// </summary>
    public bool UsedByMediaContentUploader { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the items list should be initialized or not
    /// </summary>
    public bool UpdateItemsList
    {
      get => this.updateItemsList;
      set => this.updateItemsList = value;
    }

    /// <summary>
    /// Gets or sets value indicating whether multilingual controls should be hidden.
    /// </summary>
    public bool DisableFileModifications { get; set; }

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
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>Gets the title label.</summary>
    /// <value>The title label.</value>
    protected virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the example label.</summary>
    /// <value>The example label.</value>
    protected virtual Label ExampleLabel => this.GetConditionalControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the description label.</summary>
    /// <value>The description label.</value>
    protected virtual Label DescriptionLabel => this.GetConditionalControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the uploader container control.</summary>
    /// <value>The uploader container.</value>
    protected virtual HtmlGenericControl UploaderContainer => this.GetConditionalControl<HtmlGenericControl>("uploaderContainer", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the commands container control.</summary>
    /// <value>The comamnds container.</value>
    protected virtual HtmlGenericControl CommandsContainer => this.GetConditionalControl<HtmlGenericControl>("commandsContainer", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to mediaContentItemsList control.</summary>
    protected virtual ItemsList MediaContentItemsList => this.GetConditionalControl<ItemsList>("mediaContentItemsList", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the RadMenu containing the menu options that initiate the respective commands.
    /// </summary>
    protected internal virtual RadMenu OptionsMenu => this.Menu.Container.GetControl<RadMenu>("optionsMenu", this.DisplayMode == FieldDisplayMode.Write);

    public ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    protected internal virtual MenuList Menu => this.GetConditionalControl<MenuList>("menuList", this.DisplayMode == FieldDisplayMode.Write);

    protected internal virtual Control MediaContainer => this.GetConditionalControl<Control>("mediaContainer", this.DisplayMode == FieldDisplayMode.Write);

    protected internal virtual Control RadioButtonList => this.GetConditionalControl<Control>("radioButtonList", this.DisplayMode == FieldDisplayMode.Write);

    protected internal virtual Label MoreTranslationsLabel => this.GetConditionalControl<Label>("moreTranslationsLbl", this.DisplayMode == FieldDisplayMode.Write);

    protected virtual HtmlInputRadioButton UploadNewItemButton => this.GetConditionalControl<HtmlInputRadioButton>("uploadNewItemButton", this.DisplayMode == FieldDisplayMode.Write);

    protected virtual Control CancelUploadImageContainer => this.GetConditionalControl<Control>("cancelUploadContainer", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the <input type="file"></input> tag which is going to be used by the Kendo Upload widget
    /// </summary>
    public virtual HtmlInputFile UploadInput => this.Container.GetControl<HtmlInputFile>("uploadInput", false);

    /// <summary>Gets the file description panel.</summary>
    public HtmlControl FileDescription => this.Container.GetControl<HtmlControl>("fileDescription", false);

    /// <summary>Gets the file description panel.</summary>
    public HtmlControl FileDescriptionRead => this.Container.GetControl<HtmlControl>("fileDescriptionRead", false);

    protected internal virtual CommandBar CommandBar => this.GetConditionalControl<CommandBar>("commandBar", this.DisplayMode == FieldDisplayMode.Read);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      this.TitleLabel.Text = string.Format(Res.Get<LibrariesResources>().WhichItemsToUpload, (object) this.ItemNamePlural);
      this.PrepareMenuItems();
      this.InstantiateTemplate("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DocumentItemInfoTemplate.htm", "FileDescription");
      this.MediaContentItemsList.AllowMultipleSelection = false;
    }

    protected virtual void InstantiateTemplate(string templatePath, string itemTemplateName)
    {
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream(templatePath))
      {
        string end = new StreamReader(manifestResourceStream).ReadToEnd();
        ItemDescription itemDescription = this.MediaContentItemsList.Items.Where<ItemDescription>((Func<ItemDescription, bool>) (d => d.Name == itemTemplateName)).FirstOrDefault<ItemDescription>();
        if (itemDescription == null)
          return;
        itemDescription.Markup = end;
      }
    }

    protected virtual void PrepareMenuItems()
    {
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().OpenTheFile, "openFile"));
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().ReplaceFile, "replace"));
      if (!SystemManager.CurrentContext.AppSettings.Current.Multilingual)
        return;
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().UseAnotherFile, "useAnother"));
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IFileFieldDefinition fileFieldDefinition))
        return;
      this.ItemName = Res.Get(fileFieldDefinition.ResourceClassId, fileFieldDefinition.ItemName);
      this.ItemNamePlural = Res.Get(fileFieldDefinition.ResourceClassId, fileFieldDefinition.ItemNamePlural);
      this.LibraryContentType = fileFieldDefinition.LibraryContentType;
      this.IsMultiselect = fileFieldDefinition.IsMultiselect;
      this.MaxFileCount = fileFieldDefinition.MaxFileCount;
    }

    private static string GenerateAcceptAttributeForAllowedFileTypes(string allowedExtensions)
    {
      string empty = string.Empty;
      if (allowedExtensions.Length <= 0)
        return empty;
      char ch = ',';
      if (!allowedExtensions.Contains<char>(ch))
        return Telerik.Sitefinity.Modules.Libraries.Web.MimeMapping.GetMimeMapping(allowedExtensions);
      string[] strArray = allowedExtensions.Replace(" ", "").Split(',');
      List<string> stringList = new List<string>();
      foreach (string extension in strArray)
      {
        string mimeMapping = Telerik.Sitefinity.Modules.Libraries.Web.MimeMapping.GetMimeMapping(extension);
        if (!stringList.Contains(mimeMapping))
          stringList.Add(mimeMapping);
      }
      return string.Join(",", stringList.ToArray());
    }

    private static string GetDialogAllowedExtensions(
      string contentTypefilesName,
      string allowedExensionsSettings)
    {
      string allowedExtensions = string.Empty;
      if (allowedExensionsSettings.Length > 0)
      {
        char ch = ',';
        if (allowedExensionsSettings.Contains<char>(ch))
          allowedExtensions = FileField.BuildDialogFileExtension(allowedExensionsSettings.Split(','), contentTypefilesName);
        else
          allowedExtensions = FileField.BuildDialogFileExtension(new string[1]
          {
            allowedExensionsSettings
          }, contentTypefilesName);
      }
      return allowedExtensions;
    }

    private static string BuildDialogFileExtension(string[] items, string contentTypefilesName)
    {
      string str1 = string.Empty;
      if (items.Length != 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string str2 in items)
          stringBuilder.AppendFormat("*{0};", (object) str2.Trim());
        string str3 = stringBuilder.ToString().Remove(stringBuilder.Length - 1);
        str1 = string.Format("{2} ({0})|{1}", (object) str3, (object) str3, (object) contentTypefilesName);
      }
      return str1;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_uploadServiceUrl", (object) this.ResolveUrl(this.UploadServiceUrl));
      controlDescriptor.AddProperty("_mediaContentBinderServiceUrl", (object) this.getMediaFileLinksServiceUrl);
      controlDescriptor.AddProperty("_copyMediaFileLinkServiceUrl", (object) this.copyMediaFileLinkServiceUrl);
      controlDescriptor.AddProperty("_libraryProviderName", (object) this.LibraryProviderName);
      controlDescriptor.AddProperty("_updateList", (object) this.UpdateItemsList);
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddElementProperty("mediaContainer", this.MediaContainer.ClientID);
        controlDescriptor.AddElementProperty("moreTranslationsLbl", this.MoreTranslationsLabel.ClientID);
        controlDescriptor.AddElementProperty("radioButtonList", this.RadioButtonList.ClientID);
        controlDescriptor.AddElementProperty("uploadNewItemButton", this.UploadNewItemButton.ClientID);
        controlDescriptor.AddComponentProperty("mediaContentItemsList", this.MediaContentItemsList.ClientID);
        controlDescriptor.AddComponentProperty("mediaContentBinder", this.MediaContentItemsList.Binder.ClientID);
        controlDescriptor.AddComponentProperty("menuList", this.OptionsMenu.ClientID);
        controlDescriptor.AddElementProperty("uploaderContainer", this.UploaderContainer.ClientID);
        controlDescriptor.AddProperty("disableFileModifications", (object) this.DisableFileModifications);
        if (this.FileDescription != null)
          controlDescriptor.AddProperty("_fileDescriptionId", (object) this.FileDescription.ClientID);
      }
      else
      {
        controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
        if (this.FileDescriptionRead != null)
          controlDescriptor.AddProperty("_fileDescriptionId", (object) this.FileDescriptionRead.ClientID);
      }
      if (this.LibraryContentType != (Type) null)
      {
        controlDescriptor.AddProperty("_libraryContentType", (object) this.LibraryContentType.ToString());
        string str1 = string.Empty;
        string str2 = string.Empty;
        LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
        string lower = this.LibraryContentType.Name.ToLower();
        if (!(lower == "image"))
        {
          if (!(lower == "video"))
          {
            if (lower == "document")
            {
              bool? allowedExensions = librariesConfig.Documents.AllowedExensions;
              if (allowedExensions.HasValue)
              {
                allowedExensions = librariesConfig.Documents.AllowedExensions;
                if (allowedExensions.Value)
                {
                  str1 = FileField.GenerateAcceptAttributeForAllowedFileTypes(librariesConfig.Documents.AllowedExensionsSettings);
                  str2 = librariesConfig.Documents.AllowedExensionsSettings;
                }
              }
            }
          }
          else
          {
            str1 = FileField.GenerateAcceptAttributeForAllowedFileTypes(librariesConfig.Videos.AllowedExensionsSettings);
            str2 = librariesConfig.Videos.AllowedExensionsSettings;
          }
        }
        else
        {
          str1 = FileField.GenerateAcceptAttributeForAllowedFileTypes(librariesConfig.Images.AllowedExensionsSettings);
          str2 = librariesConfig.Images.AllowedExensionsSettings;
        }
        controlDescriptor.AddProperty("_filter", (object) str1);
        controlDescriptor.AddProperty("_allowedExtensions", (object) str2.Replace(" ", ""));
      }
      controlDescriptor.AddProperty("_itemNamePlural", (object) HttpUtility.HtmlEncode(this.ItemNamePlural.ToLower()));
      controlDescriptor.AddProperty("_itemName", (object) HttpUtility.HtmlEncode(this.ItemName.ToLower()));
      controlDescriptor.AddProperty("_isMultiselect", (object) this.IsMultiselect);
      controlDescriptor.AddProperty("_maxFileCount", (object) this.MaxFileCount);
      controlDescriptor.AddProperty("_usedByMediaContentUploader", (object) this.UsedByMediaContentUploader);
      controlDescriptor.AddElementProperty("commandsContainer", this.CommandsContainer.ClientID);
      controlDescriptor.AddProperty("_isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      if (!string.IsNullOrEmpty(this.ClientLabelManager.ClientID))
        controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      if (this.UploadInput != null)
        controlDescriptor.AddElementProperty("uploadInput", this.UploadInput.ClientID);
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
      string fullName = typeof (FileField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FileField.js", fullName)
      };
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
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.KendoAll;
  }
}
