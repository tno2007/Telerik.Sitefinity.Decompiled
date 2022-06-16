// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog
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
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Workflow.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>Dialog for uploading new files.</summary>
  public class UploadDialog : AjaxDialogBase
  {
    private string itemName;
    private string itemsName;
    private string libraryName;
    private Type libraryType;
    private Type contentType;
    private string librariesProviderName;
    private string parentServiceUrl;
    private string childServiceUrl;
    private bool? supportsMultiligual;
    private IAppSettings appSettings;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.UploadDialog.ascx");
    private const string uploadDialogScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadDialog.js";

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (UploadDialog).FullName;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UploadDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the localizable string that represents the name of the item in singular.
    /// </summary>
    protected virtual string ItemName
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemName))
          this.itemName = this.Page.Request.QueryString["itemName"];
        return this.itemName;
      }
    }

    /// <summary>
    /// Gets the localizable string that represents the name of the item in plural.
    /// </summary>
    protected virtual string ItemsName
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemsName))
          this.itemsName = this.Page.Request.QueryString["itemsName"];
        return this.itemsName;
      }
    }

    /// <summary>
    /// Gets the localizable string that represents the name of the library.
    /// </summary>
    protected virtual string LibraryName
    {
      get
      {
        if (string.IsNullOrEmpty(this.libraryName))
          this.libraryName = this.Page.Request.QueryString["libraryTypeName"];
        return this.libraryName;
      }
    }

    /// <summary>Gets the type of the library.</summary>
    protected virtual Type LibraryType
    {
      get
      {
        if (this.libraryType == (Type) null && !string.IsNullOrEmpty(this.Page.Request.QueryString["libraryType"]))
          this.libraryType = TypeResolutionService.ResolveType(this.Page.Request.QueryString["libraryType"]);
        return this.libraryType;
      }
    }

    /// <summary>
    /// Gets the base url of the web service to use from the query string of the current request
    /// </summary>
    protected virtual string ParentServiceUrl
    {
      get
      {
        if (this.parentServiceUrl.IsNullOrWhitespace())
        {
          this.parentServiceUrl = SystemManager.CurrentHttpContext.Request.QueryString["parentServiceUrl"];
          this.parentServiceUrl = HttpUtility.UrlDecode(this.parentServiceUrl);
        }
        return this.parentServiceUrl;
      }
    }

    /// <summary>
    /// Gets the base url of the web service to use from the query string of the current request
    /// </summary>
    protected virtual string ChildServiceUrl
    {
      get
      {
        if (this.childServiceUrl.IsNullOrWhitespace())
        {
          this.childServiceUrl = SystemManager.CurrentHttpContext.Request.QueryString["childServiceUrl"];
          this.childServiceUrl = HttpUtility.UrlDecode(this.childServiceUrl);
        }
        return this.childServiceUrl;
      }
    }

    /// <summary>Gets the type of the content.</summary>
    protected virtual Type ContentType
    {
      get
      {
        if (this.contentType == (Type) null && !string.IsNullOrEmpty(this.Page.Request.QueryString["contentType"]))
          this.contentType = TypeResolutionService.ResolveType(this.Page.Request.QueryString["contentType"]);
        return this.contentType;
      }
    }

    /// <summary>Gets the type of the content.</summary>
    protected virtual string LibrariesProviderName
    {
      get
      {
        if (this.librariesProviderName == null && (!string.IsNullOrEmpty(this.Page.Request.QueryString["providerName"]) || !string.IsNullOrEmpty(this.Page.Request.QueryString["provider"])))
        {
          this.librariesProviderName = this.Page.Request.QueryString["providerName"];
          if (this.librariesProviderName.IsNullOrEmpty())
            this.librariesProviderName = this.Page.Request.QueryString["provider"];
          this.librariesProviderName = HttpUtility.UrlDecode(this.librariesProviderName);
        }
        return this.librariesProviderName;
      }
    }

    /// <summary>Gets the localizable text for selecting a library.</summary>
    protected virtual string SelectALibraryText => this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image) ? Res.Get<ImagesResources>().SelectAnAlbum : Res.Get<LibrariesResources>().SelectLibrary;

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(this.AppSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    /// <summary>Gets the default application settings information.</summary>
    protected virtual IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// Gets the configured value of how many items should be displayed on the first load. This configuration enables the control to load items only when required.
    /// </summary>
    public int ItemsCount => Config.Get<LibrariesConfig>().ItemsCount;

    /// <summary>
    /// Gets the reference to thet control that enables javascript localization
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the reference to the control that represents the back button.
    /// </summary>
    /// <value>The back button.</value>
    protected virtual LinkButton BackButton => this.Container.GetControl<LinkButton>("backButton", true);

    /// <summary>
    /// Gets the reference to the control for uploading files.
    /// </summary>
    /// <value>The file upload.</value>
    protected virtual FileField FileUpload => this.Container.GetControl<FileField>("fileUpload", true);

    /// <summary>
    /// Gets the reference to the control that represents the library selector.
    /// </summary>
    /// <value>The library selector.</value>
    protected virtual HtmlGenericControl LibrarySelector => this.Container.GetControl<HtmlGenericControl>("librarySelector", true);

    /// <summary>
    /// Gets the reference to the control that represents the library selector title.
    /// </summary>
    /// <value>The library selector title.</value>
    protected virtual Label LibrarySelectorTitle => this.Container.GetControl<Label>("librarySelectorTitle", true);

    /// <summary>
    /// Gets the reference to the control that represents the taxonomies selector.
    /// </summary>
    /// <value>The taxonomies selector.</value>
    protected virtual SectionControl TaxonomiesSelector => this.Container.GetControl<SectionControl>("taxonomiesSelector", true);

    /// <summary>
    /// Gets the reference to the control that represents the dialog title.
    /// </summary>
    /// <value>The dialog title.</value>
    protected virtual HtmlGenericControl DialogTitle => this.Container.GetControl<HtmlGenericControl>("dialogTitle", true);

    /// <summary>
    /// Gets the reference to the control that represents the dialog instance pageId.
    /// </summary>
    /// <value>The dialog instance pageId.</value>
    protected virtual HiddenField DialogInstanceId => this.Container.GetControl<HiddenField>("dialogInstanceId", true);

    /// <summary>Gets a reference to the parent library selector.</summary>
    protected virtual FolderField ParentLibrarySelector => this.Container.GetControl<FolderField>("parentLibrarySelector", true);

    /// <summary>Gets the reference to the workflow menu.</summary>
    public virtual WorkflowMenu WorkflowMenu => this.Container.GetControl<WorkflowMenu>("workflowMenu", true);

    /// <summary>
    /// Gets the reference to the control that represents the success command bar.
    /// </summary>
    /// <value>The success command bar.</value>
    protected virtual CommandBar SuccessCommandBar => this.Container.GetControl<CommandBar>("successCommandBar", true);

    /// <summary>Gets the reference to the message control.</summary>
    /// <value>The message control.</value>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.DialogInstanceId.Value = this.ClientID;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ParentLibrarySelector.ProviderName = this.LibrariesProviderName;
      this.ParentLibrarySelector.WebServiceUrl = this.ParentServiceUrl + "folders";
      this.ParentLibrarySelector.ItemName = this.ItemName;
      this.ParentLibrarySelector.LibraryTypeName = this.LibraryType.FullName;
      this.ParentLibrarySelector.CreateLibraryServiceUrl = this.ParentServiceUrl;
      this.TaxonomiesSelector.Style.Add("display", "none");
      this.LibrarySelector.Style.Add("display", "none");
      this.LibrarySelectorTitle.Text = string.Format(Res.Get<LibrariesResources>().WhereToStoreUploadedItems, (object) this.ItemsName.ToLower());
      string str1 = string.Format(Res.Get<Labels>().BackToAllItemsParameter, (object) this.ItemsName);
      if (this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        str1 = Res.Get<Labels>().BackToAllItems;
      this.BackButton.Text = str1;
      string str2 = string.Empty;
      foreach (TaxonomyPropertyDescriptor propertyDescriptor in OrganizerBase.GetPropertiesForType(this.ContentType))
      {
        MetaField metaField = propertyDescriptor.MetaField;
        ITaxonomy taxonomy = TaxonomyManager.GetManager(metaField.TaxonomyProvider).GetTaxonomy(metaField.TaxonomyId);
        Type type = taxonomy.GetType();
        string str3 = (string) null;
        string resourceOrKey;
        if (taxonomy.Id == TaxonomyManager.CategoriesTaxonomyId)
          resourceOrKey = this.GetResourceOrKey(typeof (TaxonomyResources).Name, "ClickToAddCategories");
        else if (taxonomy.Id == TaxonomyManager.TagsTaxonomyId)
        {
          resourceOrKey = this.GetResourceOrKey(typeof (TaxonomyResources).Name, "ClickToAddTags");
          str3 = this.GetResourceOrKey(typeof (TaxonomyResources).Name, "TagsFieldInstructions");
        }
        else
        {
          resourceOrKey = this.GetResourceOrKey(typeof (TaxonomyResources).Name, "ClickToAdd" + propertyDescriptor.Name, "Click to add " + propertyDescriptor.DisplayName);
          if (type == typeof (FlatTaxonomy))
            str3 = this.GetResourceOrKey(typeof (TaxonomyResources).Name, propertyDescriptor.Name + "FieldInstructions", "Comma separated, type new or existing " + propertyDescriptor.DisplayName);
        }
        TaxonFieldDefinition taxonFieldDefinition;
        if (type == typeof (FlatTaxonomy))
        {
          taxonFieldDefinition = (TaxonFieldDefinition) new FlatTaxonFieldDefinition();
          taxonFieldDefinition.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
          taxonFieldDefinition.CssClass = "sfFormSeparator";
          ((FlatTaxonFieldDefinition) taxonFieldDefinition).ExpandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition()
          {
            Expanded = new bool?(true),
            ExpandText = resourceOrKey
          };
        }
        else
        {
          if (!(type == typeof (HierarchicalTaxonomy)))
            throw new NotSupportedException();
          taxonFieldDefinition = (TaxonFieldDefinition) new HierarchicalTaxonFieldDefinition();
          taxonFieldDefinition.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
          ((HierarchicalTaxonFieldDefinition) taxonFieldDefinition).ExpandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition()
          {
            Expanded = new bool?(false),
            ExpandText = resourceOrKey
          };
        }
        if (!string.IsNullOrEmpty(str3))
          taxonFieldDefinition.Description = str3;
        taxonFieldDefinition.Title = this.GetResourceOrKey(typeof (TaxonomyResources).Name, propertyDescriptor.DisplayName);
        taxonFieldDefinition.DataFieldName = propertyDescriptor.Name;
        taxonFieldDefinition.AllowMultipleSelection = !metaField.IsSingleTaxon;
        taxonFieldDefinition.TaxonomyId = metaField.TaxonomyId;
        taxonFieldDefinition.TaxonomyProvider = metaField.TaxonomyProvider;
        taxonFieldDefinition.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
        taxonFieldDefinition.AllowCreating = true;
        this.TaxonomiesSelector.Fields.Add((IFieldDefinition) taxonFieldDefinition);
        str2 = str2 + propertyDescriptor.Name + " and ";
        bool flag = false;
        IManager mappedManager = ManagerBase.GetMappedManager(this.contentType);
        if (this.contentType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
          flag = mappedManager.Provider.GetSecurityRoot().IsGranted("Album", "CreateAlbum");
        else if (this.contentType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
          flag = mappedManager.Provider.GetSecurityRoot().IsGranted("VideoLibrary", "CreateVideoLibrary");
        else if (this.contentType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
          flag = mappedManager.Provider.GetSecurityRoot().IsGranted("DocumentLibrary", "CreateDocumentLibrary");
        this.ParentLibrarySelector.ShowCreateNewLibraryButton = flag && this.LibrariesProviderName != "SystemLibrariesProvider";
      }
      this.TaxonomiesSelector.Title = Res.Get<TaxonomyResources>().CategoriesAndTags;
      this.TaxonomiesSelector.Rebuild();
      this.FileUpload.ItemName = this.ItemName;
      this.FileUpload.ItemNamePlural = this.ItemsName.ToLower();
      this.FileUpload.LibraryContentType = this.ContentType;
      this.FileUpload.IsMultiselect = true;
      this.FileUpload.MaxFileCount = 100;
      this.FileUpload.LibraryProviderName = this.LibrariesProviderName;
      foreach (ToolboxItemBase command in this.SuccessCommandBar.Commands)
      {
        if (command is ICommandButton)
        {
          CommandToolboxItem commandToolboxItem = (CommandToolboxItem) command;
          string commandName = commandToolboxItem.CommandName;
          if (!(commandName == "viewAll"))
          {
            if (!(commandName == "batchEdit"))
            {
              if (commandName == "uploadOther")
                commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().UploadOther, (object) this.ItemsName.ToLower());
            }
            else
              commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().AddDetails, (object) this.ItemsName.ToLower());
          }
          else
          {
            string str4 = string.Format(Res.Get<LibrariesResources>().ViewAll, (object) this.ItemsName.ToLower());
            if (this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
              str4 = Res.Get<LibrariesResources>().ViewAllItems;
            commandToolboxItem.Text = str4;
          }
        }
      }
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
      controlDescriptor.AddProperty("_uploadText", (object) Res.Get<Labels>().Upload);
      controlDescriptor.AddProperty("_webServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.ChildServiceUrl));
      controlDescriptor.AddProperty("_parentWebServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.ParentServiceUrl));
      controlDescriptor.AddProperty("_libraryType", (object) this.LibraryType.FullName);
      controlDescriptor.AddProperty("_itemType", (object) this.ContentType.FullName);
      controlDescriptor.AddProperty("_itemsCount", (object) this.ItemsCount);
      controlDescriptor.AddComponentProperty("successCommandBar", this.SuccessCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("fileUpload", this.FileUpload.ClientID);
      controlDescriptor.AddComponentProperty("parentLibrarySelector", this.ParentLibrarySelector.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("librarySelector", this.LibrarySelector.ClientID);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddElementProperty("backButton", this.BackButton.ClientID);
      controlDescriptor.AddElementProperty("taxonomySelectorId", this.TaxonomiesSelector.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddProperty("isMultilingual", (object) this.SupportsMultiligual);
      if (this.SupportsMultiligual)
        controlDescriptor.AddProperty("_defaultLanguage", (object) this.AppSettings.DefaultFrontendLanguage.Name);
      controlDescriptor.AddProperty("_blankDataItem", (object) this.CreateBlankDataItem(this.ContentType).ToJson(this.ContentType));
      controlDescriptor.AddProperty("_blankLibraryDataItem", (object) this.CreateBlankDataItem(this.LibraryType).ToJson(this.LibraryType));
      if (this.WorkflowMenu != null)
        controlDescriptor.AddComponentProperty("workflowMenu", this.WorkflowMenu.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadDialog.js", typeof (UploadDialog).Assembly.FullName)
    };

    private string GetResourceOrKey(string resClassId, string key) => this.GetResourceOrKey(resClassId, key, (string) null);

    private string GetResourceOrKey(string resClassId, string key, string defaultValue) => Res.Get(resClassId, key, SystemManager.CurrentContext.Culture, true, false) ?? defaultValue ?? key;

    private object CreateBlankDataItem(Type contentType)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(contentType);
      bool suppressSecurityChecks = mappedManager.Provider.SuppressSecurityChecks;
      mappedManager.Provider.SuppressSecurityChecks = true;
      object blankDataItem = mappedManager.CreateItem(contentType, Guid.Empty);
      mappedManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      return blankDataItem;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
