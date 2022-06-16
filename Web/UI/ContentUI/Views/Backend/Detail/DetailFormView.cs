// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ClientBinders;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Sitefinity.Workflow.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail
{
  /// <summary>
  /// View for the ContentView control which allows inserting and editing of a particular data item that
  /// inherits from base <see cref="T:System.Web.UI.WebControls.Content" /> class.
  /// </summary>
  public class DetailFormView : ViewBase, IScriptControl
  {
    /// <summary>The widget bars</summary>
    public List<WidgetBar> widgetBars;
    /// <summary>The field controls</summary>
    protected List<Control> fieldControls;
    /// <summary>The translation field controls</summary>
    protected List<Control> translationFieldControls;
    /// <summary>The section controls</summary>
    protected List<SectionControl> sectionControls;
    /// <summary>The translation section controls</summary>
    protected List<SectionControl> translationSectionControls;
    private FieldControlsBinder fieldControlsBinder;
    private AsyncCommandMediator asyncCommandMediator;
    private string serviceUrl;
    private bool bindOnLoad;
    private Type contentType;
    private IDetailFormViewDefinition viewDefinition;
    private string deleteConfirmationMessage;
    private static string detailFormViewScript;
    private static string layoutTemplateName;
    private static string fieldDisplayModeScript;
    private string cancelChangesServiceUrl;
    private string contentLocationPreviewUrl;
    private IContentViewDefinition definition;
    private bool? supportsMultiligual;
    private List<string> widgetCommandNamesToHideIfItemIsNotEditable;
    private IDictionary<string, string> localization;
    private WorkflowMenu topWorkflowMenu;
    private WorkflowMenu bottomWorkflowMenu;
    private string itemTemplate;
    private Collection<PromptDialog> promptDialogControls;
    private MultilingualMode multilingualMode;
    private string additionalCreateCommands;
    private bool showCheckRelatingData;
    private bool recycleBinEnabled;
    private const string IRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";
    private const string IRequiresUiCultureScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.js";
    private const string ProviderNameQueryStringParameter = "provider";
    private static readonly string HeaderSectionTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.HeaderSectionControl.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView" /> class.
    /// </summary>
    public DetailFormView() => this.Initialize();

    /// <summary>
    /// Gets or sets a value indicating whether the client binder should be bound on load (client side).
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set
      {
        this.bindOnLoad = value;
        this.FieldControlsBinder.BindOnLoad = value;
      }
    }

    /// <summary>
    /// Gets or sets the url of the WCF service to get/set the data
    /// </summary>
    public string ServiceUrl
    {
      get => this.serviceUrl;
      set
      {
        this.serviceUrl = value;
        this.FieldControlsBinder.ServiceUrl = value;
        this.TranslationBinder.ServiceUrl = value;
      }
    }

    /// <summary>
    /// Gets or sets the url for deleting the temp version of the item (cancel changes/remove lock)
    /// </summary>
    public string CancelChangesServiceUrl
    {
      get => this.cancelChangesServiceUrl;
      set => this.cancelChangesServiceUrl = value;
    }

    /// <summary>Gets or sets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DetailFormView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the message to be shown when prompting the user if they are sure they want to delete an item
    /// </summary>
    public string DeleteConfirmationMessage
    {
      get
      {
        if (string.IsNullOrEmpty(this.deleteConfirmationMessage))
          this.deleteConfirmationMessage = Res.Get<Labels>().AreYouSureYouWantToDeleteItem;
        return this.deleteConfirmationMessage;
      }
      set => this.deleteConfirmationMessage = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multilingual.
    /// </summary>
    /// <value><c>true</c> if supports multilingual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(this.IsMultilingualSupported(this.multilingualMode, this.ContentType, SystemManager.CurrentContext.AppSettings.Multilingual));
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    /// <summary>
    /// Gets or sets the item template string for the detail view (to be used to get the right item type, if required).
    /// </summary>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for getting a new item.
    /// </remarks>
    public string ItemTemplate
    {
      get => this.itemTemplate;
      set => this.itemTemplate = value;
    }

    /// <summary>Gets the cannot modify dialog.</summary>
    /// <value>The cannot modify dialog.</value>
    public virtual PromptDialog CannotModifyDialog => this.Container.GetControl<PromptDialog>("cannotModifyDialog", true);

    /// <summary>
    /// Gets the choice field responsible for the selection of the language selector.
    /// </summary>
    /// <value>The translation selector.</value>
    public LanguageChoiceField TranlationSelector => this.Container.GetControl<LanguageChoiceField>("translationLanguagesSelector", false);

    /// <summary>Gets the translation label.</summary>
    /// <value>The translation label.</value>
    public Label TranlationLabel => this.Container.GetControl<Label>("lblTranslateFrom", false);

    /// <summary>
    /// Gets the control that is used to show the translation view.
    /// </summary>
    /// <value>The control that is used to show the translation view.</value>
    public Control ShowTranslation => this.Container.GetControl<Control>("showTranslation", true);

    /// <summary>
    /// Gets the control that is used to hide the translation view.
    /// </summary>
    /// <value>The control that is used to hide the translation view.</value>
    public Control HideTranslation => this.Container.GetControl<Control>("hideTranslation", true);

    /// <summary>Gets the collection of prompt dialog controls.</summary>
    public Collection<PromptDialog> PromptDialogControls
    {
      get
      {
        if (this.promptDialogControls == null)
          this.promptDialogControls = new Collection<PromptDialog>();
        return this.promptDialogControls;
      }
    }

    internal string ContentLocationPreviewUrl
    {
      get => this.contentLocationPreviewUrl;
      set => this.contentLocationPreviewUrl = value;
    }

    /// <summary>Gets the field controls client binder.</summary>
    protected internal virtual FieldControlsBinder FieldControlsBinder
    {
      get
      {
        if (this.fieldControlsBinder == null)
          this.fieldControlsBinder = this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);
        return this.fieldControlsBinder;
      }
    }

    /// <summary>Gets the field controls client binder.</summary>
    protected internal virtual AsyncCommandMediator AsyncCommandMediator
    {
      get
      {
        if (this.asyncCommandMediator == null)
          this.asyncCommandMediator = this.Container.GetControl<AsyncCommandMediator>("asyncBinder", true);
        return this.asyncCommandMediator;
      }
    }

    /// <summary>
    /// Gets the reference to the WarningField which is bound to the ItemContext warnings collection.
    /// </summary>
    protected internal virtual SectionControl HeaderSection => this.Container.GetControl<SectionControl>("headerSection", false);

    /// <summary>
    /// Gets the reference to the repeater which binds the sections of the form
    /// </summary>
    protected internal virtual Repeater Sections => this.Container.GetControl<Repeater>("sections", true);

    /// <summary>
    /// Gets the reference to the wrapper that holds top tool bar
    /// </summary>
    protected internal virtual Control TopToolbarWrapper => this.Container.GetControl<Control>("topToolbarWrapper", true);

    /// <summary>
    /// Gets the reference to the wrapper that holds bottom tool bar
    /// </summary>
    protected internal virtual Control BottomToolbarWrapper => this.Container.GetControl<Control>("bottomToolbarWrapper", true);

    /// <summary>
    /// Gets the wrapper for the section that will be shown as a tool bar
    /// </summary>
    /// <value>The section tool bar wrapper.</value>
    protected internal virtual Control SectionToolbarWrapper => this.Container.GetControl<Control>("sectionToolbarWrapper", false);

    /// <summary>
    /// Gets the reference to the text control which represents the title of the detail form.
    /// </summary>
    protected internal virtual ITextControl TitleControl => this.Container.GetControl<ITextControl>("ViewTitle", true);

    /// <summary>Gets the next button control.</summary>
    /// <value>The next button control.</value>
    protected internal virtual Control NextButtonControl => this.Container.GetControl<Control>("nextNavButton", true);

    /// <summary>Gets the previous button control.</summary>
    /// <value>The previous button control.</value>
    protected internal virtual Control PreviousButtonControl => this.Container.GetControl<Control>("previousNavButton", true);

    /// <summary>Gets the navigation panel.</summary>
    /// <value>The navigation panel.</value>
    protected internal virtual HtmlGenericControl NavigationPanel => this.Container.GetControl<HtmlGenericControl>("navigationPanel", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the field controls.</summary>
    /// <value>The field controls.</value>
    protected List<Control> FieldControls => this.fieldControls;

    /// <summary>
    /// Gets a value indicating whether to show "check relating data".
    /// </summary>
    /// <value>
    /// <c>true</c> if "check relating data" should be shown; otherwise, <c>false</c>.
    /// </value>
    protected bool ShowCheckRelatingData
    {
      get => this.showCheckRelatingData;
      private set => this.showCheckRelatingData = value;
    }

    /// <summary>
    /// Gets a value indicating whether the Recycle Bin module is enabled.
    /// </summary>
    /// <value>The recycle bin enabled.</value>
    protected bool RecycleBinEnabled
    {
      get => this.recycleBinEnabled;
      private set => this.recycleBinEnabled = value;
    }

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the button for "back to all items"</summary>
    protected virtual Control BackToAllItemsButton => this.Container.GetControl<Control>("backButton", true);

    /// <summary>
    /// Gets the literal that lies inside of the button "back to all items"
    /// </summary>
    protected virtual Literal BackToAllItemsLiteral => this.Container.GetControl<Literal>("backToAllItemsLiteral", true);

    /// <summary>Gets the reference to the message control</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to the window manager.</summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the wrapper for the translation related controls.
    /// </summary>
    /// <value>The translation wrapper.</value>
    protected virtual WebControl TranslationWrapper => (WebControl) this.Container.GetControl<Panel>("translationWrapper", false);

    /// <summary>
    /// Gets the binder that is responsible for binding the translation view.
    /// </summary>
    /// <value>The translation binder.</value>
    protected virtual FieldControlsBinder TranslationBinder => this.Container.GetControl<FieldControlsBinder>("translationBinder", false);

    /// <summary>
    /// Gets the the repeater which binds the sections of the translation view.
    /// </summary>
    protected virtual Repeater TranslationSections => this.Container.GetControl<Repeater>("translationSections", false);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the type of the content that will be shown.</summary>
    /// <value>The type of the content.</value>
    private Type ContentType
    {
      get
      {
        if (this.contentType == (Type) null)
        {
          string name = this.Page.Request.QueryString["itemType"];
          if (!string.IsNullOrEmpty(name))
            this.contentType = TypeResolutionService.ResolveType(name);
          else if (this.Host.ControlDefinition.ContentType != (Type) null)
            this.contentType = this.Host.ControlDefinition.ContentType;
        }
        return this.contentType;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the "Back to items" link text is to be kept unchanged.
    /// </summary>
    /// <value>
    /// <c>true</c> if the "Back to items" link text is to be kept unchanged; otherwise, <c>false</c>.
    /// </value>
    private bool SuppressBackToButtonLabelModify => this.Page.Request.QueryString[nameof (SuppressBackToButtonLabelModify)] != null && bool.Parse(this.Page.Request.QueryString[nameof (SuppressBackToButtonLabelModify)]);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      this.EnsureFieldControls();
      ScriptControlDescriptor baseDescriptor = this.GetBaseDescriptor(typeof (DetailFormView).FullName, this.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      if (this.viewDefinition.CreateBlankItem.GetValueOrDefault())
      {
        object blankDataItem = this.CreateBlankDataItem();
        using (MemoryStream memoryStream = new MemoryStream())
        {
          new DataContractJsonSerializer(this.ContentType, (IEnumerable<Type>) new Type[1]
          {
            blankDataItem.GetType()
          }).WriteObject((Stream) memoryStream, blankDataItem);
          baseDescriptor.AddProperty("_blankDataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
        }
      }
      baseDescriptor.AddComponentProperty("mediator", this.asyncCommandMediator.ClientID);
      baseDescriptor.AddComponentProperty("binder", this.FieldControlsBinder.ClientID);
      baseDescriptor.AddComponentProperty("translationBinder", this.TranslationBinder.ClientID);
      if (this.topWorkflowMenu != null)
        baseDescriptor.AddComponentProperty("topWorkflowMenu", this.topWorkflowMenu.ClientID);
      if (this.bottomWorkflowMenu != null)
        baseDescriptor.AddComponentProperty("bottomWorkflowMenu", this.bottomWorkflowMenu.ClientID);
      baseDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      List<string> list = this.sectionControls.Select<SectionControl, string>((Func<SectionControl, string>) (n => n.ClientID)).ToList<string>();
      if (list.Count > 0)
        baseDescriptor.AddProperty("sectionIds", (object) scriptSerializer.Serialize((object) list));
      if (this.SupportsMultiligual)
      {
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        baseDescriptor.AddProperty("_defaultLanguage", (object) appSettings.DefaultFrontendLanguage.Name);
        baseDescriptor.AddProperty("translationFieldControlIds", (object) scriptSerializer.Serialize((object) this.translationFieldControls.Select<Control, string>((Func<Control, string>) (c => c.ClientID))));
        Control control1 = this.fieldControls.Where<Control>((Func<Control, bool>) (c => c is LanguageChoiceField)).FirstOrDefault<Control>();
        if (control1 != null && control1.ID == "languageChoiceField")
          baseDescriptor.AddComponentProperty("languageSelector", control1.ClientID);
        Control control2 = this.fieldControls.Where<Control>((Func<Control, bool>) (c => c is LanguageListField)).FirstOrDefault<Control>();
        if (control2 != null)
          baseDescriptor.AddComponentProperty("languageList", control2.ClientID);
        baseDescriptor.AddComponentProperty("translationLanguageSelector", this.TranlationSelector.ClientID);
        baseDescriptor.AddElementProperty("showTranslationControl", this.ShowTranslation.ClientID);
        baseDescriptor.AddElementProperty("hideTranslationControl", this.HideTranslation.ClientID);
        baseDescriptor.AddElementProperty("translationWrapper", this.TranslationWrapper.ClientID);
      }
      baseDescriptor.AddProperty("requireDataItemControlIds", (object) this.GetRequireDataItemControlIds());
      baseDescriptor.AddProperty("bulkEditFieldControlIds", (object) this.GetBulkEditFieldControlsClientIds());
      baseDescriptor.AddProperty("compositeFieldControlIds", (object) this.GetCompositeFieldControlsClientIds());
      baseDescriptor.AddProperty("commandFieldControlIds", (object) this.GetCommandFieldControlids());
      baseDescriptor.AddProperty("displayMode", (object) this.Definition.DisplayMode);
      baseDescriptor.AddProperty("_suppressBackToButtonLabelModify", (object) this.SuppressBackToButtonLabelModify);
      IEnumerable<string> strings = this.fieldControls.Select<Control, string>((Func<Control, string>) (c => c.ClientID));
      baseDescriptor.AddProperty("fieldControlIds", (object) scriptSerializer.Serialize((object) strings));
      if (this.SectionToolbarWrapper != null)
        baseDescriptor.AddElementProperty("sectionToolbarWrapper", this.SectionToolbarWrapper.ClientID);
      baseDescriptor.AddProperty("isMultilingual", (object) this.SupportsMultiligual);
      baseDescriptor.AddProperty("serviceBaseUrl", this.ServiceUrl.StartsWith("~", StringComparison.Ordinal) ? (object) this.Page.ResolveUrl(this.ServiceUrl) : (object) this.ServiceUrl);
      baseDescriptor.AddProperty("baseBackendUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/"));
      baseDescriptor.AddProperty("cancelChangesServiceUrl", (object) this.CancelChangesServiceUrl);
      baseDescriptor.AddProperty("contentLocationPreviewUrl", (object) this.ContentLocationPreviewUrl);
      baseDescriptor.AddProperty("_createCommandName", (object) "create");
      baseDescriptor.AddProperty("_createAndUploadCommandName", (object) "createAndUpload");
      baseDescriptor.AddProperty("_editCommandName", (object) "edit");
      baseDescriptor.AddProperty("_additionalCreateCommands", (object) this.additionalCreateCommands);
      baseDescriptor.AddProperty("_saveCommandName", (object) "save");
      baseDescriptor.AddProperty("_saveAndContinueCommandName", (object) "saveAndContinue");
      baseDescriptor.AddProperty("_cancelCommandName", (object) "cancel");
      baseDescriptor.AddProperty("_publishCommandName", (object) "publish");
      baseDescriptor.AddProperty("_deleteCommandName", (object) "delete");
      baseDescriptor.AddProperty("_historyCommandName", (object) "history");
      baseDescriptor.AddProperty("_permissionsCommandName", (object) "permissions");
      baseDescriptor.AddProperty("_previewCommandName", (object) "preview");
      baseDescriptor.AddProperty("_saveTempAndOpenLinkCommandName", (object) "saveTempAndOpenLink");
      baseDescriptor.AddProperty("_deleteConfirmationMessage", (object) this.DeleteConfirmationMessage);
      baseDescriptor.AddProperty("_deleteVersionCommandName", (object) "deleteVersion");
      baseDescriptor.AddProperty("_restoreVersionAsNewCommandName", (object) "restoreVersionAsNew");
      baseDescriptor.AddProperty("_createChildCommandName", (object) "createChild");
      baseDescriptor.AddProperty("_duplicateCommandName", (object) "duplicate");
      baseDescriptor.AddProperty("_closeDialogCommandName", (object) "closeDialog");
      baseDescriptor.AddProperty("_widgetCommandNamesToHideIfItemIsNotEditableArr", (object) scriptSerializer.Serialize((object) this.widgetCommandNamesToHideIfItemIsNotEditable.ToArray()));
      string str1 = string.Empty;
      if (this.Host.ControlDefinition.ManagerType != (Type) null)
        str1 = this.Host.ControlDefinition.ManagerType.FullName;
      if (string.IsNullOrEmpty(str1) && this.ContentType != (Type) null && this.ContentType.IsAssignableFrom(typeof (IContent)))
        str1 = ManagerBase.GetMappedManagerType(this.contentType).AssemblyQualifiedName;
      baseDescriptor.AddProperty("_managerType", (object) str1);
      if (this.ContentType.FullName.StartsWith("Telerik.Sitefinity"))
        baseDescriptor.AddProperty("_contentType", this.ContentType == (Type) null ? (object) string.Empty : (object) this.ContentType.FullName);
      else
        baseDescriptor.AddProperty("_contentType", this.ContentType == (Type) null ? (object) string.Empty : (object) this.ContentType.AssemblyQualifiedName);
      baseDescriptor.AddProperty("_permissionsDialogUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted));
      if (this.widgetBars != null && this.widgetBars.Count > 0)
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (WidgetBar widgetBar in this.widgetBars)
        {
          stringList1.Add(widgetBar.ClientID);
          if (widgetBar is ButtonBar)
            stringList2.Add(widgetBar.ClientID);
        }
        string str2 = scriptSerializer.Serialize((object) stringList1);
        string str3 = scriptSerializer.Serialize((object) stringList1);
        baseDescriptor.AddProperty("widgetBarIds", (object) str2);
        baseDescriptor.AddProperty("buttonBarIds", (object) str3);
      }
      baseDescriptor.AddElementProperty("titleElement", ((Control) this.TitleControl).ClientID);
      baseDescriptor.AddElementProperty("previousButton", this.PreviousButtonControl.ClientID);
      baseDescriptor.AddElementProperty("nextButton", this.NextButtonControl.ClientID);
      baseDescriptor.AddElementProperty("backToAllItemsButton", this.BackToAllItemsButton.ClientID);
      baseDescriptor.AddProperty("_backToLabel", (object) Res.Get<Labels>().BackTo);
      baseDescriptor.AddProperty("_youHaveUnsavedChangesWantToLeavePage", (object) Res.Get<Labels>().YouHaveUnsavedChangesWantToLeavePage);
      baseDescriptor.AddProperty("_itemTemplate", (object) this.itemTemplate);
      if (!string.IsNullOrEmpty(this.viewDefinition.AlternativeTitle))
        baseDescriptor.AddProperty("_alternativeTitle", (object) this.GetLabel(this.viewDefinition.ResourceClassId, this.viewDefinition.AlternativeTitle));
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (PromptDialog promptDialogControl in this.PromptDialogControls)
      {
        dictionary1[promptDialogControl.DialogName] = promptDialogControl.ClientID;
        if (!string.IsNullOrEmpty(promptDialogControl.OpenOnCommand))
          dictionary2[promptDialogControl.OpenOnCommand] = promptDialogControl.DialogName;
      }
      string str4 = scriptSerializer.Serialize((object) dictionary1);
      string str5 = scriptSerializer.Serialize((object) dictionary2);
      baseDescriptor.AddProperty("_promptDialogNamesJson", (object) str4);
      baseDescriptor.AddProperty("_promptDialogCommandsJson", (object) str5);
      baseDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      baseDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      baseDescriptor.AddProperty("_unlockMode", (object) this.CheckUnlockMode());
      string str6 = scriptSerializer.Serialize((object) this.localization);
      baseDescriptor.AddProperty("_localization", (object) str6);
      baseDescriptor.AddProperty("_homePageId", (object) SystemManager.CurrentContext.CurrentSite.HomePageId);
      baseDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      if (this.Definition is IDetailFormViewDefinition definition)
        baseDescriptor.AddProperty("_doNotUseContentItemContext", (object) definition.DoNotUseContentItemContext);
      else
        baseDescriptor.AddProperty("_doNotUseContentItemContext", (object) false);
      baseDescriptor.AddProperty("_cannotModifyDialogId", (object) this.CannotModifyDialog.ClientID);
      baseDescriptor.AddProperty("_revertUrl", (object) HttpUtility.UrlDecode(this.Page.Request.QueryString["revertURL"]));
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>()
      {
        (ScriptDescriptor) baseDescriptor
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
      string fullName = typeof (DetailFormView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.js", fullName));
      scriptReferences.Add(new ScriptReference(DetailFormView.detailFormViewScript, fullName));
      scriptReferences.Add(new ScriptReference(DetailFormView.fieldDisplayModeScript, fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    internal void SetUpSections(
      IEnumerable<IContentViewSectionDefinition> sections)
    {
      if (this.HeaderSection != null)
      {
        IContentViewSectionDefinition sectionDefinition = sections.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (x => x.Name == "HeaderSection")).FirstOrDefault<IContentViewSectionDefinition>();
        if (sectionDefinition != null)
        {
          this.HeaderSection.LayoutTemplatePath = DetailFormView.HeaderSectionTemplateName;
          this.HeaderSection.SectionDefinition = sectionDefinition;
          string name = string.IsNullOrEmpty(this.definition.ControlId) ? sectionDefinition.Name : this.definition.ControlId;
          if (!string.IsNullOrEmpty(name))
            ControlUtilities.SetControlIdFromName(name, (Control) this.HeaderSection);
          this.sectionControls.Add(this.HeaderSection);
          this.HeaderSection.Rebuild();
        }
        else
          this.HeaderSection.Visible = false;
      }
      List<IContentViewSectionDefinition> list1 = sections.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (x => x.Name != "HeaderSection")).ToList<IContentViewSectionDefinition>();
      List<IContentViewSectionDefinition> list2 = list1.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (x => !x.Ordinal.HasValue)).ToList<IContentViewSectionDefinition>();
      list2.AddRange((IEnumerable<IContentViewSectionDefinition>) list1.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (x => x.Ordinal.HasValue)).OrderBy<IContentViewSectionDefinition, int?>((Func<IContentViewSectionDefinition, int?>) (x => x.Ordinal)));
      this.Sections.DataSource = (object) list2;
      this.Sections.ItemCreated += new RepeaterItemEventHandler(this.Sections_ItemCreated);
      this.Sections.DataBind();
    }

    /// <summary>Initializes the provider.</summary>
    protected internal virtual void InitializeProvider()
    {
      NameValueCollection queryString = this.Page.Request.QueryString;
      if (string.IsNullOrEmpty(queryString["provider"]) || !(queryString["provider"] != "undefined"))
        return;
      this.Host.ControlDefinition.ProviderName = queryString["provider"];
    }

    /// <summary>Initializes this instance.</summary>
    protected virtual void Initialize()
    {
      this.sectionControls = new List<SectionControl>();
      this.translationSectionControls = new List<SectionControl>();
      this.fieldControls = new List<Control>();
      this.translationFieldControls = new List<Control>();
      DetailFormView.detailFormViewScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.DetailFormView.js";
      DetailFormView.layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.DetailView.ascx");
      DetailFormView.fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
      this.widgetCommandNamesToHideIfItemIsNotEditable = new List<string>()
      {
        "restoreVersionAsNew",
        "deleteVersion",
        "delete"
      };
      this.itemTemplate = string.Empty;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <exception cref="T:System.ArgumentNullException">definition argument is required.</exception>
    /// <exception cref="T:System.Exception">DetailFormView requires IDetailFormViewDefinition.</exception>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      IDetailFormViewDefinition definition1 = definition != null ? definition as IDetailFormViewDefinition : throw new ArgumentNullException(nameof (definition));
      this.viewDefinition = definition1;
      if (definition1 == null)
        throw new Exception("DetailFormView requires IDetailFormViewDefinition.");
      if (!string.IsNullOrEmpty(definition1.Title))
        this.TitleControl.Text = this.GetLabel(definition1.ResourceClassId, definition1.Title);
      if (!string.IsNullOrEmpty(this.GetLabel(definition1.ResourceClassId, "BackToItems")))
        this.BackToAllItemsLiteral.Text = this.GetLabel(definition1.ResourceClassId, "BackToItems");
      string backLabelText = this.BackLabelText;
      if (!string.IsNullOrEmpty(backLabelText))
        this.BackToAllItemsLiteral.Text = backLabelText;
      this.InitializeProvider();
      this.DetermineServiceUrl(this.ContentType, definition1);
      this.definition = definition;
      this.ExternalClientScripts = (IDictionary<string, string>) definition1.ExternalClientScripts;
      this.localization = (IDictionary<string, string>) definition1.Localization;
      this.multilingualMode = definition1.MultilingualMode;
      this.additionalCreateCommands = definition1.AdditionalCreateCommands;
      if (!string.IsNullOrWhiteSpace(definition1.ItemTemplate))
        this.itemTemplate = definition1.ItemTemplate;
      if (definition1.Labels != null)
      {
        foreach (ILabelDefinition label in (IEnumerable<ILabelDefinition>) definition1.Labels)
          this.ClientLabelManager.AddClientLabel(label.ClassId, label.MessageKey);
      }
      else if (this.Page.Request.QueryString["itemTemplate"] != null && !string.IsNullOrWhiteSpace(this.Page.Request.QueryString["itemTemplate"].ToString()))
        this.itemTemplate = this.Page.Request.QueryString["itemTemplate"].ToString();
      this.ClientLabelManager.AddClientLabel("Labels", "ItemIsLockedFormat");
      this.ClientLabelManager.AddClientLabel("Labels", "ItemUnlockQuestion");
      this.ClientLabelManager.AddClientLabel("Labels", "ItemUnlockCloseAlert");
      foreach (IPromptDialogDefinition def in definition.PromptDialogs.ToList<IPromptDialogDefinition>())
      {
        PromptDialog child = PromptDialog.FromDefinition(def);
        container.Controls.Add((Control) child);
        this.PromptDialogControls.Add(child);
      }
      this.ShowCheckRelatingData = RelatedDataHelper.IsTypeSupportCheckRelatingData(this.Host.ControlDefinition.ContentType);
      this.RecycleBinEnabled = this.IsRecycleBinEnabled();
      PromptDialog awareDeleteDialog = ItemsListBase.GetLanguageAwareDeleteDialog(Res.Get<Labels>().WhatDoYouWantToDelete, this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) awareDeleteDialog);
      this.PromptDialogControls.Add(awareDeleteDialog);
      PromptDialog standartDeleteDialog = ItemsListBase.GetStandartDeleteDialog(Res.Get<Labels>().AreYouSureYouWantToDeleteItem, this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) standartDeleteDialog);
      this.PromptDialogControls.Add(standartDeleteDialog);
      this.FieldControlsBinder.DoNotUseContentItemContext = definition1.DoNotUseContentItemContext;
      this.TranslationBinder.DoNotUseContentItemContext = definition1.DoNotUseContentItemContext;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      IDetailFormViewDefinition definition = (IDetailFormViewDefinition) this.definition;
      this.SetUpNavigation(definition);
      IEnumerable<IContentViewSectionDefinition> detailSectionDefinitions;
      IEnumerable<IContentViewSectionDefinition> toolbarSectionDefinitions;
      this.DifferentiateSectionsPlacement(definition, out detailSectionDefinitions, out toolbarSectionDefinitions);
      this.SetUpSections(detailSectionDefinitions);
      this.SetUpToolbars(definition, toolbarSectionDefinitions);
      this.SetUpTranslation(detailSectionDefinitions);
      this.SetUpAsyncCommandMediator();
      this.SetUpTabIndexOrder();
    }

    /// <summary>
    /// Handles the ItemCreated event of the Sections control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void Sections_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("section") is SectionControl control) || !(e.Item.DataItem is IContentViewSectionDefinition dataItem))
        return;
      control.SectionDefinition = dataItem;
      string name = string.IsNullOrEmpty(dataItem.ControlId) ? dataItem.Name : dataItem.ControlId;
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, (Control) control);
      this.sectionControls.Add(control);
    }

    /// <summary>
    /// Handles the ItemCreated event of the Sections control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void TranslationSections_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("section") is SectionControl control))
        return;
      control.OverrideFieldControlsDisplayMode = true;
      control.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      foreach (Control fieldControl1 in control.FieldControls)
      {
        if (fieldControl1 is Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl fieldControl2)
          fieldControl2.DisplayMode = FieldDisplayMode.Read;
      }
      if (!(e.Item.DataItem is IContentViewSectionDefinition dataItem))
        return;
      control.SectionDefinition = dataItem;
      this.translationSectionControls.Add(control);
    }

    /// <summary>
    /// Determines the base service url that should be used for the service calls in this view
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="definition">The definition.</param>
    protected virtual void DetermineServiceUrl(
      Type contentType,
      IDetailFormViewDefinition definition)
    {
      string fullName = contentType.FullName;
      string managerTypeName = string.Empty;
      Type type = (Type) null;
      string providerName = this.Host.ControlDefinition.ProviderName;
      string str = string.IsNullOrEmpty(definition.WebServiceBaseUrl) ? "~/Sitefinity/Services/Content/ContentService.svc/" : definition.WebServiceBaseUrl;
      if (!string.IsNullOrEmpty(definition.WebServiceBaseUrl))
        this.CancelChangesServiceUrl = VirtualPathUtility.ToAbsolute(definition.WebServiceBaseUrl);
      this.ContentLocationPreviewUrl = VirtualPathUtility.ToAbsolute("~/" + ContentLocationRoute.path);
      IManager manager;
      if (this.Host.ControlDefinition.ManagerType != (Type) null)
      {
        managerTypeName = this.Host.ControlDefinition.ManagerType.FullName;
        manager = ManagerBase.GetManager(managerTypeName, providerName);
      }
      else
        ManagerBase.TryGetMappedManager(contentType, providerName, out manager);
      if (manager != null && manager.Provider is IHierarchyProvider provider)
        type = provider.GetParentType(contentType);
      if (type != (Type) null)
      {
        string empty = string.Empty;
        if (this.Page.Request.QueryString["parentId"] != null)
          empty = this.Page.Request.QueryString["parentId"];
        if (!empty.IsNullOrEmpty())
        {
          this.ServiceUrl = string.Format(str + "parent/{{{{parentId}}}}/?itemType={1}&parentItemType={2}&providerName={3}&managerType={4}", (object) empty, (object) fullName, (object) type, (object) providerName, (object) managerTypeName);
          return;
        }
      }
      this.ServiceUrl = string.Format(str + "?itemType={0}&providerName={1}&managerType={2}", (object) fullName, (object) providerName, (object) managerTypeName);
    }

    /// <summary>Ensures the field controls are added.</summary>
    protected virtual void EnsureFieldControls()
    {
      foreach (SectionControl sectionControl in this.sectionControls)
      {
        foreach (Control fieldControl in sectionControl.FieldControls)
          this.fieldControls.Add(fieldControl);
      }
      foreach (SectionControl translationSectionControl in this.translationSectionControls)
      {
        foreach (Control fieldControl in translationSectionControl.FieldControls)
          this.translationFieldControls.Add(fieldControl);
      }
      IDetailFormViewDefinition definition = this.Definition as IDetailFormViewDefinition;
      if (!this.SupportsMultiligual)
        return;
      bool? renderTranslationView = definition.IsToRenderTranslationView;
      if (!renderTranslationView.HasValue)
        return;
      renderTranslationView = definition.IsToRenderTranslationView;
      if (!renderTranslationView.Value)
        return;
      this.fieldControls.Add((Control) this.TranlationSelector);
    }

    /// <summary>
    /// Create a blank data item. When bound on the client, this item will be used to construct
    /// the JS object sent back to the server.
    /// </summary>
    /// <returns>Blank data item</returns>
    protected virtual object CreateBlankDataItem()
    {
      object blankDataItem1 = (object) null;
      BlankItemDelegate blankItemDelegate = ManagerBase.GetMappedBlankItemDelegate(this.ContentType);
      if (blankItemDelegate != null)
        blankDataItem1 = blankItemDelegate(this.ContentType, this.Host.ControlDefinition.ProviderName);
      if (blankDataItem1 == null)
      {
        SystemConfig systemConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();
        if (this.ContentType == typeof (PageData) || this.ContentType == typeof (PageNode))
        {
          PageManager manager = PageManager.GetManager(this.Host.ControlDefinition.ProviderName, "BlankItemTransactionName");
          bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
          manager.Provider.SuppressSecurityChecks = true;
          PageNode pageNode = manager.CreatePageNode(Guid.Empty);
          manager.CreatePageData(Guid.Empty).NavigationNode = pageNode;
          manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
          WcfPage blankDataItem2 = new WcfPage(pageNode, manager);
          blankDataItem2.AvailableLanguages = new string[0];
          if (this.DetailViewDefinition != null && this.DetailViewDefinition.ControlDefinitionName == "BackendPages")
            blankDataItem2.RootId = SiteInitializer.BackendRootNodeId;
          else
            blankDataItem2.RootId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
          blankDataItem2.Template = new WcfPageTemplate()
          {
            Id = Guid.Empty
          };
          PagesConfig pagesConfig = Telerik.Sitefinity.Configuration.Config.Get<PagesConfig>();
          blankDataItem2.EnableViewState = pagesConfig.ViewStateMode;
          blankDataItem2.OutputCacheProfile = string.Empty;
          return (object) blankDataItem2;
        }
        if (this.ContentType == typeof (FormDescription))
        {
          FormsManager manager = FormsManager.GetManager(this.Host.ControlDefinition.ProviderName, "BlankItemTransactionName");
          bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
          manager.Provider.SuppressSecurityChecks = true;
          FormDescription form = manager.CreateForm("Name", Guid.Empty);
          form.Framework = SystemManager.IsModuleEnabled("Feather") ? FormFramework.Mvc : FormFramework.WebForms;
          FormDescriptionViewModel blankDataItem3 = new FormDescriptionViewModel(form);
          blankDataItem3.AvailableFrameworks = Telerik.Sitefinity.Configuration.Config.Get<PagesConfig>().PageTemplatesFrameworks;
          manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
          blankDataItem3.Name = string.Empty;
          return (object) blankDataItem3;
        }
        IManager manager1 = !(this.Host.ControlDefinition.ManagerType != (Type) null) ? ManagerBase.GetMappedManagerInTransaction(this.ContentType, this.Host.ControlDefinition.ProviderName, "BlankItemTransactionName") : ManagerBase.GetManager(this.Host.ControlDefinition.ManagerType.FullName, this.Host.ControlDefinition.ProviderName);
        bool suppressSecurityChecks1 = manager1.Provider.SuppressSecurityChecks;
        manager1.Provider.SuppressSecurityChecks = true;
        blankDataItem1 = manager1.CreateItem(this.ContentType, Guid.Empty);
        if (this.ContentType == typeof (Album))
        {
          Album album = blankDataItem1 as Album;
          OutputCacheProfileElement profile = systemConfig.CacheSettings.Profiles[systemConfig.CacheSettings.DefaultProfile];
          album.OutputCacheDuration = profile.Duration;
          album.OutputSlidingExpiration = profile.SlidingExpiration;
          album.ClientCacheDuration = systemConfig.CacheSettings.MediaCacheProfiles[systemConfig.CacheSettings.DefaultImageProfile].Duration;
          album.UseDefaultSettingsForClientCaching = true;
          album.UseDefaultSettingsForOutputCaching = true;
        }
        if (blankDataItem1 is Telerik.Sitefinity.GenericContent.Model.Content && typeof (IHasParent).IsAssignableFrom(blankDataItem1.GetType()))
        {
          string input = this.Page.Request.QueryString["parentId"];
          string name = WcfHelper.DecodeWcfString(this.Page.Request.QueryString["parentType"]);
          Guid result = Guid.Empty;
          if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(name) && Guid.TryParse(input, out result))
          {
            Type itemType = TypeResolutionService.ResolveType(name);
            try
            {
              object obj = manager1.GetItem(itemType, result);
              ((IHasParent) blankDataItem1).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) obj;
            }
            catch (ItemNotFoundException ex)
            {
            }
          }
        }
        Guid result1;
        if (blankDataItem1 is IFolderItem && Guid.TryParse(this.Page.Request.QueryString["folderId"], out result1))
          ((IFolderItem) blankDataItem1).FolderId = new Guid?(result1);
        if (blankDataItem1 is PageTemplate)
        {
          PageTemplatesAvailability templatesFrameworks = Telerik.Sitefinity.Configuration.Config.Get<PagesConfig>().PageTemplatesFrameworks;
          return (object) new PageTemplateViewModel((PageTemplate) blankDataItem1)
          {
            AvailableFrameworks = templatesFrameworks,
            DateCreated = new DateTime?(DateTime.UtcNow)
          };
        }
        manager1.Provider.SuppressSecurityChecks = suppressSecurityChecks1;
      }
      return blankDataItem1;
    }

    /// <summary>Checks if the form should work in unlock mode.</summary>
    /// <returns>True if the form should work in unlock mode, false otherwise.</returns>
    protected virtual bool CheckUnlockMode() => this.viewDefinition.UnlockDetailItemOnExit.GetValueOrDefault();

    private void DifferentiateSectionsPlacement(
      IDetailFormViewDefinition definition,
      out IEnumerable<IContentViewSectionDefinition> detailSectionDefinitions,
      out IEnumerable<IContentViewSectionDefinition> toolbarSectionDefinitions)
    {
      detailSectionDefinitions = definition.Sections.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (s => s.Name != "toolbarSection")).Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (s => SystemManager.IsItemAccessble((object) s)));
      toolbarSectionDefinitions = definition.Sections.Where<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (s => s.Name == "toolbarSection"));
    }

    private bool IsRecycleBinEnabled() => ObjectFactory.Resolve<IRecycleBinStateResolver>().ShouldMoveToRecycleBin(this.Host.ControlDefinition.ContentType);

    private string BackLabelText => this.Page.Request.QueryString["backLabelText"] != null ? this.Page.Request.QueryString["backLabelText"].ToString() : string.Empty;

    private void SetUpTranslation(
      IEnumerable<IContentViewSectionDefinition> sections)
    {
      IDetailFormViewDefinition definition = this.Definition as IDetailFormViewDefinition;
      if (this.SupportsMultiligual)
      {
        bool? renderTranslationView = definition.IsToRenderTranslationView;
        if (renderTranslationView.HasValue)
        {
          renderTranslationView = definition.IsToRenderTranslationView;
          if (renderTranslationView.Value)
          {
            List<ContentViewSectionDefinition> sectionDefinitionList = new List<ContentViewSectionDefinition>();
            foreach (ContentViewSectionDefinition section in sections)
            {
              if (section.Name != "statusSection" && section.Name != "SidebarSection" && section.Name != "AdvancedSection" && section.Name != "MoreOptionsSection" && !section.IsHiddenInTranslationMode)
                sectionDefinitionList.Add(section);
            }
            this.TranslationSections.DataSource = (object) sectionDefinitionList;
            this.TranslationSections.ItemCreated += new RepeaterItemEventHandler(this.TranslationSections_ItemCreated);
            this.TranslationSections.DataBind();
            return;
          }
        }
      }
      this.TranslationSections.Visible = false;
      this.TranlationSelector.Visible = false;
      this.TranlationLabel.Visible = false;
      this.ShowTranslation.Visible = false;
      this.HideTranslation.Visible = false;
    }

    private void SetUpToolbars(
      IDetailFormViewDefinition def,
      IEnumerable<IContentViewSectionDefinition> toolbarSections)
    {
      this.SetUpWidgetToolbars(def);
      if (!this.SupportsMultiligual)
        return;
      this.SetUpSectionToolbars(toolbarSections);
    }

    private void SetUpWidgetToolbars(IDetailFormViewDefinition def)
    {
      this.widgetBars = new List<WidgetBar>();
      bool flag = false;
      if (def.UseWorkflow.HasValue)
      {
        flag = def.UseWorkflow.Value;
      }
      else
      {
        ContentViewControlElement contentViewControl = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[def.ControlDefinitionName];
        if (contentViewControl != null && contentViewControl.UseWorkflow.HasValue)
          flag = contentViewControl.UseWorkflow.Value;
      }
      if (flag)
      {
        if (def.ShowTopToolbar.HasValue && def.ShowTopToolbar.Value)
        {
          this.topWorkflowMenu = new WorkflowMenu();
          this.TopToolbarWrapper.Controls.Add((Control) this.topWorkflowMenu);
          this.widgetBars.Add((WidgetBar) this.topWorkflowMenu);
          this.topWorkflowMenu.ShowCheckRelatingData = this.ShowCheckRelatingData;
          this.topWorkflowMenu.RecycleBinEnabled = this.RecycleBinEnabled;
        }
        this.bottomWorkflowMenu = new WorkflowMenu();
        this.BottomToolbarWrapper.Controls.Add((Control) this.bottomWorkflowMenu);
        this.widgetBars.Add((WidgetBar) this.bottomWorkflowMenu);
        this.bottomWorkflowMenu.ShowCheckRelatingData = this.ShowCheckRelatingData;
        this.bottomWorkflowMenu.RecycleBinEnabled = this.RecycleBinEnabled;
      }
      else
      {
        ButtonBar child1 = new ButtonBar();
        this.widgetBars.Add((WidgetBar) child1);
        child1.WidgetBarDefiniton = def.Toolbar;
        this.BottomToolbarWrapper.Controls.Add((Control) child1);
        bool? showTopToolbar = def.ShowTopToolbar;
        if (showTopToolbar.HasValue)
        {
          showTopToolbar = def.ShowTopToolbar;
          if (showTopToolbar.Value)
          {
            ButtonBar child2 = new ButtonBar();
            child2.WidgetBarDefiniton = def.Toolbar;
            this.TopToolbarWrapper.Controls.Add((Control) child2);
            this.widgetBars.Add((WidgetBar) child2);
            child2.TabIndex = (short) 2000;
            goto label_12;
          }
        }
        this.TopToolbarWrapper.Visible = false;
label_12:
        child1.TabIndex = (short) 1000;
      }
    }

    private void SetUpSectionToolbars(
      IEnumerable<IContentViewSectionDefinition> toolbarSections)
    {
      foreach (IContentViewSectionDefinition toolbarSection in toolbarSections)
      {
        SectionControl child = new SectionControl()
        {
          SectionDefinition = toolbarSection
        };
        this.SectionToolbarWrapper.Controls.Add((Control) child);
        this.sectionControls.Add(child);
      }
    }

    private void SetUpTabIndexOrder()
    {
      short num1 = 1;
      foreach (SectionControl sectionControl in this.sectionControls)
      {
        foreach (Control fieldControl1 in sectionControl.FieldControls)
        {
          if (fieldControl1 is FieldControl fieldControl2)
          {
            int num2 = num1++;
            fieldControl2.TabIndex = (short) num2;
          }
        }
      }
    }

    private void SetUpAsyncCommandMediator()
    {
      this.asyncCommandMediator = AsyncCommandMediator.GetCurrent(this.Page) ?? this.AsyncCommandMediator;
      foreach (WidgetBar widgetBar in this.widgetBars)
        this.asyncCommandMediator.AsyncPairs.Add(new SenderReceiverPair()
        {
          CommandReceiverClientId = this.FieldControlsBinder.ClientID,
          CommandSenderClientId = widgetBar.ClientID
        });
      if (this.bottomWorkflowMenu == null || this.topWorkflowMenu == null)
        return;
      this.asyncCommandMediator.AsyncPairs.Add(new SenderReceiverPair()
      {
        CommandReceiverClientId = this.bottomWorkflowMenu.ClientID,
        CommandSenderClientId = this.topWorkflowMenu.ClientID,
        TwoWayCommunicationMode = true
      });
    }

    private List<string> GetRequireDataItemControlIds()
    {
      List<string> dataItemControlIds = new List<string>();
      foreach (SectionControl sectionControl in this.sectionControls)
        dataItemControlIds.AddRange(sectionControl.RequireDataItemControls.Select<Control, string>((Func<Control, string>) (f => f.ClientID)));
      return dataItemControlIds;
    }

    private List<string> GetBulkEditFieldControlsClientIds()
    {
      List<string> controlsClientIds = new List<string>();
      foreach (SectionControl sectionControl in this.sectionControls)
        controlsClientIds.AddRange(sectionControl.BulkEditFieldControls.Select<Control, string>((Func<Control, string>) (f => f.ClientID)));
      return controlsClientIds;
    }

    private List<string> GetCompositeFieldControlsClientIds()
    {
      List<string> controlsClientIds = new List<string>();
      foreach (SectionControl sectionControl in this.sectionControls)
        controlsClientIds.AddRange(sectionControl.CompositeFieldControls.Select<Control, string>((Func<Control, string>) (f => f.ClientID)));
      return controlsClientIds;
    }

    private List<string> GetCommandFieldControlids()
    {
      List<string> commandFieldControlids = new List<string>();
      foreach (SectionControl sectionControl in this.sectionControls)
        commandFieldControlids.AddRange(sectionControl.CommandFieldControls.Select<Control, string>((Func<Control, string>) (f => f.ClientID)));
      return commandFieldControlids;
    }

    private void SetUpNavigation(IDetailFormViewDefinition def)
    {
      if (def.ShowNavigation.GetValueOrDefault())
        return;
      this.NavigationPanel.Visible = false;
      this.PreviousButtonControl.Visible = false;
      this.NextButtonControl.Visible = false;
    }

    private bool IsMultilingualSupported(
      MultilingualMode viewMultilingualMode,
      Type contentType,
      bool isSiteInMultilingual)
    {
      bool flag = false;
      if (!isSiteInMultilingual)
        return flag;
      switch (viewMultilingualMode)
      {
        case MultilingualMode.Automatic:
          flag = typeof (ILocalizable).IsAssignableFrom(contentType);
          break;
        case MultilingualMode.On:
          flag = true;
          break;
      }
      return flag;
    }
  }
}
