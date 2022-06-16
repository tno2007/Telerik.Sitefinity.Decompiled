// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master
{
  /// <summary>
  /// A content view control which displays the a list of content items in the backend
  /// </summary>
  public class MasterGridView : ViewBase
  {
    /// <summary>
    /// Indicates if the view will show the provider selector toolbar
    /// </summary>
    protected bool? ShowProviderSelector = new bool?(true);
    internal const string ContentTypecontextItemKey = "sf_content_type_context_item";
    private const string itemsListID = "itemsList";
    private const string itemsGridID = "itemsGrid";
    private const string itemsTreeID = "itemsTreeTable";
    private const string masterGridViewScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.MasterGridView.js";
    public static readonly string defaultTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.MasterGridView.ascx");
    private const string decisionScreensID = "decisionScreens";
    private const string sidebarID = "sidebar";
    private const string toolbarID = "toolbar";
    private const string contextBarID = "contextBar";
    private const string titleWidgetsID = "titleWidgets";
    private const string cmdColumnTemplate = "<div><a href=\"#\" class=\"sf_binderCommand_{0}\">{0}</a></div>";
    private const string providerNameQueryStringParameter = "provider";
    private WidgetBar sidebar;
    private WidgetBar toolbar;
    private WidgetBar contextBar;
    private Dictionary<string, DecisionScreen> decisionScreens;
    private List<Control> commandableItems = new List<Control>();
    private IDictionary<string, string> clientMappedCommnadNames;
    private IDataItem parentItem;
    private Content parentContent;
    private IFolder folder;
    private IManager manager;
    private bool? supportsMultilingual;
    private bool? supportsApprovalWorkflow;
    public const string GridModeKey = "Grid";
    public const string ListModeKey = "List";
    public const string TreeModeKey = "Tree";
    private IList<ItemsListBase> itemLists;
    private ISecuredObject relatedSecuredObject;
    private string selectedFilterItemCssClass = "sfSel";
    private string parentTitleFormat;
    private IDictionary<string, string> localization;
    private IAppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView" /> class.
    /// </summary>
    public MasterGridView() => this.ShowHierarchicalExpression = SortWidget.ShowHierarchicalCommandName;

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overridden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected virtual string ScriptDescriptorTypeName => typeof (MasterGridView).FullName;

    protected virtual bool UseAction(CommandWidgetElement action) => true;

    /// <summary>Gets or sets the current view mode.</summary>
    /// <value>The view mode.</value>
    public string CurrentViewMode => this.CurrentViewModeDefinition != null ? this.CurrentViewModeDefinition.Name : string.Empty;

    /// <summary>Gets or sets the selected filter item CSS class.</summary>
    /// <value>The selected filter item CSS class.</value>
    public string SelectedFilterItemCssClass
    {
      get => this.selectedFilterItemCssClass;
      set => this.selectedFilterItemCssClass = value;
    }

    public IViewModeDefinition CurrentViewModeDefinition { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public virtual string CurrentLanguage { get; private set; }

    protected internal virtual ItemsGrid ItemsGrid => this.Container.GetControl<ItemsGrid>("itemsGrid", false);

    protected internal virtual ItemsList ItemsList => this.Container.GetControl<ItemsList>("itemsList", false);

    protected internal virtual ItemsTreeTable ItemsTreeTable => this.Container.GetControl<ItemsTreeTable>("itemsTreeTable", false);

    /// <summary>Gets the title control.</summary>
    /// <value>The title control.</value>
    protected internal virtual HtmlGenericControl TitleControl => this.Container.GetControl<HtmlGenericControl>("ViewTitle", true);

    /// <summary>Gets the search binder control.</summary>
    /// <value>The search binder control.</value>
    protected internal BinderSearch BinderSearch => this.Container.GetControl<BinderSearch>("binderSearch", true);

    /// <summary>Gets the prompt window.</summary>
    /// <value>The prompt window.</value>
    protected internal virtual PromptDialog PromptWindow => this.Container.GetControl<PromptDialog>("promptWindow", true);

    /// <summary>Gets the lock window.</summary>
    /// <value>The lock window.</value>
    protected internal virtual PromptDialog LockWindow => this.Container.GetControl<PromptDialog>("lockWindow", true);

    /// <summary>Gets the reference to the message control</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Gets or sets the cookie key with which some properties of the grid controls are stored
    /// </summary>
    /// <value>The cookie key.</value>
    public string CookieKey { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName
    {
      get
      {
        string layoutTemplateName = base.LayoutTemplateName;
        return string.IsNullOrEmpty(layoutTemplateName) ? (string) null : layoutTemplateName;
      }
    }

    /// <summary>Gets the master grid view definition.</summary>
    /// <value>The master grid view definition.</value>
    public IMasterViewDefinition MasterGridViewDefinition => (IMasterViewDefinition) this.Definition;

    /// <summary>Gets the reference to the parent item.</summary>
    public IDataItem ParentItem => this.parentItem == null ? (IDataItem) this.ParentContent : this.parentItem;

    /// <summary>Gets the reference to the parent item.</summary>
    [Obsolete("Use ParentItem property")]
    protected Content ParentContent => this.parentContent;

    /// <summary>Gets the reference to the parent folder.</summary>
    internal IFolder Folder => this.folder;

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected virtual IManager Manager
    {
      get
      {
        if (this.manager == null)
        {
          if (this.Host.ControlDefinition.ManagerType == (Type) null)
          {
            if (this.Host.ControlDefinition.ContentType.GetInterface(typeof (IContent).FullName) != (Type) null)
              this.manager = ManagerBase.GetMappedManager(this.Host.ControlDefinition.ContentType, this.Host.ControlDefinition.ProviderName);
          }
          else
            this.manager = ManagerBase.GetManager(this.Host.ControlDefinition.ManagerType, this.Host.ControlDefinition.ProviderName);
        }
        return this.manager;
      }
    }

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected string ManagerType => this.manager != null ? this.manager.GetType().ToString() : (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether this instance has a sidebar.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has a sidebar; otherwise, <c>false</c>.
    /// </value>
    public bool HasSidebar { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has a toolbar.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has a toolbar; otherwise, <c>false</c>.
    /// </value>
    public bool HasToolbar { get; set; }

    /// <summary>Gets or sets the toolbar.</summary>
    /// <value>The toolbar.</value>
    public WidgetBar Toolbar
    {
      get => this.toolbar == null ? this.Container.GetControl<WidgetBar>("toolbar", false) : this.toolbar;
      set => this.toolbar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has a context bar.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has a context bar; otherwise, <c>false</c>.
    /// </value>
    public bool HasContextBar { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the template ought to be evaluated on the
    /// client or on the server.
    /// </summary>
    public TemplateEvalutionMode TemplateEvaluationMode { get; set; }

    /// <summary>Gets the default application settings information.</summary>
    protected IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multilingual.
    /// </summary>
    /// <value><c>true</c> if supports multilingual; otherwise, <c>false</c>.</value>
    public virtual bool SupportsMultilingual
    {
      get
      {
        if (!this.supportsMultilingual.HasValue)
          this.supportsMultilingual = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.supportsMultilingual.Value;
      }
      set => this.supportsMultilingual = new bool?(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the content type for this grid supports an approval workflow.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the content type for this grid supports an approval workflow; otherwise, <c>false</c>.
    /// </value>
    public bool SupportsApprovalWorkflow
    {
      get
      {
        if (!this.supportsApprovalWorkflow.HasValue)
          this.supportsApprovalWorkflow = new bool?(typeof (IApprovalWorkflowItem).IsAssignableFrom(this.Host.ControlDefinition.ContentType));
        return this.supportsApprovalWorkflow.Value;
      }
      set => this.supportsApprovalWorkflow = new bool?(value);
    }

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    public ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    public string ShowHierarchicalExpression { get; set; }

    public string SortExpression { get; set; }

    /// <summary>
    /// Gets or sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    /// <value>A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).</value>
    public string ProvidersGroups { get; set; }

    /// <summary>
    /// If set to true, the MGV will not bind the current ItemsListBase on the client when the page is loaded. Default is false, i.e.
    /// it will bind. This is different from the current ItemsListBase.ClientBinder.BindOnLoad, which is always false and not affected by this property
    /// </summary>
    public bool DoNotBindOnClientWhenPageIsLoaded { get; set; }

    /// <summary>
    /// Value of LayoutTemplatePath if LayoutTemplateName and LayoutTemplatePath both would otherwize have null or empty values
    /// </summary>
    /// <value></value>
    protected override string DefaultLayoutTemplatePath => MasterGridView.defaultTemplatePath;

    /// <summary>
    /// Override this method to return the web service root key if needed.
    /// </summary>
    /// <returns></returns>
    protected virtual string GetWebServiceRootKey() => string.Empty;

    /// <summary>
    /// Gets the security root that is used to check permissions
    /// </summary>
    /// <returns></returns>
    protected virtual ISecuredObject GetSecurityRoot() => this.Manager != null ? this.Manager.GetSecurityRoot() : (ISecuredObject) null;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      RequestContext requestContext = this.Page.GetRequestContext();
      IMasterViewDefinition definition = this.Definition as IMasterViewDefinition;
      this.TemplateEvaluationMode = definition.TemplateEvaluationMode;
      string key = requestContext.HttpContext.Request.QueryString["mode"];
      Type contentType = this.Host.ControlDefinition.ContentType;
      if (contentType != (Type) null)
      {
        bool hasValue;
        KeyValuePair<string, string> propertyValueFromCookie = definition.GetPropertyValueFromCookie(contentType, "mode", out hasValue);
        if (hasValue)
          key = propertyValueFromCookie.Value;
      }
      IViewModeDefinition viewModeDefinition = (IViewModeDefinition) null;
      if (definition.ViewModes.Count <= 0)
        throw new ArgumentException("The MasterView's Definition should have at least one ViewMode defined!", "IMasterViewDefinition.ViewModes");
      if (string.IsNullOrEmpty(key) || !definition.ViewModes.TryGetValue(key, out viewModeDefinition))
        viewModeDefinition = definition.ViewModes.Values.First<IViewModeDefinition>();
      this.CurrentViewModeDefinition = viewModeDefinition;
      string str = requestContext.HttpContext.Request.QueryString["lang"];
      if (string.IsNullOrEmpty(str))
        return;
      this.CurrentLanguage = str;
    }

    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      Dictionary<string, Control> controls = container.GetControls<ConditionalTemplateContainer>();
      if (controls != null)
      {
        foreach (ConditionalTemplateContainer templateContainer in controls.Values)
        {
          templateContainer.EvaluationMode = this.TemplateEvaluationMode;
          templateContainer.Evaluate((object) this);
        }
      }
      return container;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <param name="definition"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      IMasterViewDefinition masterDefinition = definition as IMasterViewDefinition;
      this.manager = (IManager) null;
      this.InitializeProvider();
      this.relatedSecuredObject = this.GetSecurityRoot();
      if (masterDefinition != null)
      {
        Type contentType = this.Host.ControlDefinition.ContentType;
        if (contentType != (Type) null)
        {
          this.Context.Items[(object) "sf_content_type_context_item"] = (object) contentType;
          this.DetermineParent(contentType);
          this.ShowProviderSelector = new bool?(this.ShowProvidersList(contentType));
        }
        this.DetermineFolder();
        this.DetermineTitle((IContentViewMasterDefinition) masterDefinition);
        this.BindProvidersList();
        this.clientMappedCommnadNames = (IDictionary<string, string>) ((ContentViewDefinition) definition).ClientMappedCommnadNames;
        Control control = this.Container.GetControl<Control>("titleWidgets", false);
        if (control != null)
        {
          IWidgetDefinition[] array = masterDefinition.TitleWidgets.ToArray<IWidgetDefinition>();
          if (array.Length != 0)
          {
            foreach (IWidgetDefinition definition1 in array)
            {
              Control widgetControl = WidgetBar.CreateWidgetControl(definition1);
              this.commandableItems.Add(widgetControl);
              control.Controls.Add(WidgetBar.CreateWidgetControlWrapper((IWidget) widgetControl));
            }
            control.Visible = true;
          }
          else
            control.Visible = false;
        }
        if (this.ItemsGrid != null)
        {
          this.InitializeItemsListBase((ItemsListBase) this.ItemsGrid, masterDefinition);
          this.SortExpression = this.ItemsGrid.DefaultSortExpression;
          if (masterDefinition.ViewModes.ContainsKey("Grid"))
            this.SetGridViewMode((IGridViewModeDefinition) masterDefinition.ViewModes["Grid"], "Grid");
        }
        if (this.ItemsList != null)
        {
          this.InitializeItemsListBase((ItemsListBase) this.ItemsList, masterDefinition);
          this.SortExpression = this.ItemsList.DefaultSortExpression;
          if (masterDefinition.ViewModes.ContainsKey("List"))
            this.SetListViewMode((IListViewModeDefinition) masterDefinition.ViewModes["List"]);
        }
        if (this.ItemsTreeTable != null)
        {
          this.InitializeItemsListBase((ItemsListBase) this.ItemsTreeTable, masterDefinition);
          this.SortExpression = this.ItemsTreeTable.DefaultSortExpression;
          if (masterDefinition.ViewModes.ContainsKey("TreeTable"))
            this.SetGridViewMode((IGridViewModeDefinition) masterDefinition.ViewModes["TreeTable"], "TreeTable");
        }
        if (this.BinderSearch != null)
        {
          this.BinderSearch.SearchFields = masterDefinition.SearchFields;
          this.BinderSearch.ExtendedSearchFields = masterDefinition.ExtendedSearchFields;
          if (contentType != (Type) null && contentType.FullName == typeof (PresentationData).FullName)
            this.BinderSearch.Multilingual = new bool?(false);
        }
        this.SetDecisionScreens((IList<IDecisionScreenDefinition>) masterDefinition.DecisionScreens.ToList<IDecisionScreenDefinition>());
        this.HasSidebar = this.ConfigureWidgetBar(ref this.sidebar, "sidebar", masterDefinition.Sidebar);
        this.HasToolbar = this.ConfigureWidgetBar(ref this.toolbar, "toolbar", masterDefinition.Toolbar);
        this.HasContextBar = this.ConfigureWidgetBar(ref this.contextBar, "contextBar", masterDefinition.ContextBar);
        foreach (ItemsListBase availableList in (IEnumerable<ItemsListBase>) this.GetAvailableLists())
        {
          this.SetDialogs((IList<IDialogDefinition>) masterDefinition.Dialogs.ToList<IDialogDefinition>(), availableList);
          this.SetPromptDialogs((IList<IPromptDialogDefinition>) masterDefinition.PromptDialogs.ToList<IPromptDialogDefinition>(), availableList);
          this.SetLinks((IList<ILinkDefinition>) masterDefinition.Links.Select<ILinkDefinition, LinkDefinition>((Func<ILinkDefinition, LinkDefinition>) (l => new LinkDefinition()
          {
            NavigateUrl = l.NavigateUrl,
            CommandName = l.CommandName,
            Name = l.Name
          })).Cast<ILinkDefinition>().ToList<ILinkDefinition>(), availableList);
        }
        this.ExternalClientScripts = (IDictionary<string, string>) masterDefinition.ExternalClientScripts;
        this.localization = (IDictionary<string, string>) masterDefinition.Localization;
        if (contentType != (Type) null)
        {
          this.CookieKey = definition.GetCookieKey(contentType);
          bool hasValue = false;
          KeyValuePair<string, string> propertyValueFromCookie = definition.GetPropertyValueFromCookie(contentType, SortWidget.ShowHierarchicalCommandName, out hasValue);
          if (hasValue)
          {
            this.ShowHierarchicalExpression = propertyValueFromCookie.Value;
            this.SetSortWidgetSellection(masterDefinition, propertyValueFromCookie.Value);
          }
          if (masterDefinition.Labels != null)
          {
            foreach (ILabelDefinition label in (IEnumerable<ILabelDefinition>) masterDefinition.Labels)
              this.ClientLabelManager.AddClientLabel(label.ClassId, label.MessageKey);
          }
        }
        this.ClientLabelManager.AddClientLabel("Labels", "UndoLabel");
        this.ClientLabelManager.AddClientLabel("Labels", "UndoDeleteMessageSingle");
        this.ClientLabelManager.AddClientLabel("Labels", "UndoDeleteMessagePlural");
        this.DoNotBindOnClientWhenPageIsLoaded = masterDefinition.DoNotBindOnClientWhenPageIsLoaded;
        this.ProvidersGroups = masterDefinition.ProvidersGroups;
      }
      this.Style.Add("display", "none");
    }

    /// <summary>Initializes the provider.</summary>
    protected internal virtual void InitializeProvider()
    {
      NameValueCollection queryString = this.Page.Request.QueryString;
      if (string.IsNullOrEmpty(queryString["provider"]) || !(queryString["provider"] != "undefined"))
        return;
      this.Host.ControlDefinition.ProviderName = queryString["provider"];
      this.ProviderSelectorPanel.SelectedProvider = queryString["provider"];
    }

    protected virtual void InitializeItemsListBase(
      ItemsListBase itemsListBase,
      IMasterViewDefinition masterDefinition)
    {
      if (!string.IsNullOrWhiteSpace(masterDefinition.DeleteSingleConfirmationMessage))
      {
        string resourceClassId = masterDefinition.ResourceClassId;
        if (!string.IsNullOrEmpty(resourceClassId))
          itemsListBase.DeleteSingleConfirmationMessage = Res.Get(resourceClassId, masterDefinition.DeleteSingleConfirmationMessage);
      }
      if (!string.IsNullOrWhiteSpace(masterDefinition.DeleteMultipleConfirmationMessage))
        itemsListBase.DeleteMultipleConfirmationMessage = Res.Get(masterDefinition.ResourceClassId, masterDefinition.DeleteMultipleConfirmationMessage);
      itemsListBase.CheckRelatingDataMessageSingle = Res.Get<Labels>().CheckRelatingDataMessageSingle;
      itemsListBase.CheckRelatingDataMessageMultiple = Res.Get<Labels>().CheckRelatingDataMessageMultiple;
      itemsListBase.RecycleBinEnabled = this.IsRecycleBinEnabled();
      itemsListBase.SendToRecycleBinSingleConfirmationMessage = Res.Get<Labels>().SendToRecycleBinSingleConfirmationMessage;
      itemsListBase.SendToRecycleBinMultipleConfirmationMessage = Res.Get<Labels>().SendToRecycleBinMultipleConfirmationMessage;
      itemsListBase.BindOnLoad = this.TemplateEvaluationMode == TemplateEvalutionMode.Server;
      this.DetermineServiceUrl(itemsListBase, (IContentViewMasterDefinition) masterDefinition);
      itemsListBase.ProviderName = this.Host.ControlDefinition.ProviderName;
      if (!string.IsNullOrEmpty(masterDefinition.GridCssClass))
      {
        ItemsListBase itemsListBase1 = itemsListBase;
        itemsListBase1.WrapperTagCssClass = itemsListBase1.WrapperTagCssClass + " " + masterDefinition.GridCssClass;
      }
      if (!string.IsNullOrEmpty(masterDefinition.FilterExpression))
        itemsListBase.ConstantFilter = masterDefinition.FilterExpression;
      bool? nullable = masterDefinition.DisableSorting;
      if (nullable.HasValue)
      {
        ItemsListBase itemsListBase2 = itemsListBase;
        nullable = masterDefinition.DisableSorting;
        int num = !nullable.Value ? 1 : 0;
        itemsListBase2.AllowSorting = num != 0;
      }
      if (!string.IsNullOrEmpty(masterDefinition.SortExpression))
        itemsListBase.DefaultSortExpression = masterDefinition.SortExpression;
      nullable = masterDefinition.DisableSorting;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        itemsListBase.DefaultSortExpression = (string) null;
      IContentViewDefinition defintion = (IContentViewDefinition) masterDefinition;
      Type contentType = this.Host.ControlDefinition.ContentType;
      if (contentType != (Type) null)
      {
        bool hasValue;
        KeyValuePair<string, string> propertyValueFromCookie = defintion.GetPropertyValueFromCookie(contentType, SortWidget.SortCommandName, out hasValue);
        if (hasValue)
        {
          itemsListBase.DefaultSortExpression = propertyValueFromCookie.Value;
          this.SetSortWidgetSellection(masterDefinition, propertyValueFromCookie.Value);
        }
        itemsListBase.RecyclableContentItemType = contentType;
      }
      nullable = masterDefinition.AllowPaging;
      if (nullable.HasValue)
      {
        ItemsListBase itemsListBase3 = itemsListBase;
        nullable = masterDefinition.AllowPaging;
        int num = nullable.Value ? 1 : 0;
        itemsListBase3.AllowPaging = num != 0;
      }
      int? itemsPerPage = masterDefinition.ItemsPerPage;
      if (itemsPerPage.HasValue)
      {
        ItemsListBase itemsListBase4 = itemsListBase;
        itemsPerPage = masterDefinition.ItemsPerPage;
        int num = itemsPerPage.Value;
        itemsListBase4.PageSize = num;
      }
      if (this.CurrentLanguage != null)
        itemsListBase.UICulture = this.CurrentLanguage;
      itemsListBase.ShowCheckRelatingData = RelatedDataHelper.IsTypeSupportCheckRelatingData(this.Host.ControlDefinition.ContentType);
    }

    /// <summary>Binds the providers list.</summary>
    public virtual void BindProvidersList()
    {
      bool? providerSelector = this.ShowProviderSelector;
      bool flag = true;
      if (providerSelector.GetValueOrDefault() == flag & providerSelector.HasValue)
      {
        this.ProviderSelectorPanel.AllProvidersTitle = Res.Get<Labels>().AllProviders;
        this.ProviderSelectorPanel.HiddenProviderNames = new List<string>()
        {
          "AppRoles"
        };
        this.ProviderSelectorPanel.ShowAllProvidersLink = false;
        if (this.MasterGridViewDefinition != null && this.MasterGridViewDefinition.ProvidersGroups != null)
          this.ProviderSelectorPanel.ShowProviderGroups = this.MasterGridViewDefinition.ProvidersGroups;
        this.ProviderSelectorPanel.Manager = this.Manager;
      }
      else
        this.ProviderSelectorPanel.Visible = false;
    }

    /// <summary>
    /// Determines whether the providers list should be displayed for the current content type
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    protected virtual bool ShowProvidersList(Type contentType) => this.ParentItem == null && !(contentType.Name == "Comment");

    protected virtual void SetCurrentViewMode(IViewModeDefinition viewModeDefinition)
    {
      switch (viewModeDefinition)
      {
        case IGridViewModeDefinition _:
          this.SetGridViewMode((IGridViewModeDefinition) viewModeDefinition, viewModeDefinition.Name);
          break;
        case IListViewModeDefinition _:
          this.SetListViewMode((IListViewModeDefinition) viewModeDefinition);
          break;
      }
    }

    protected virtual void SetGridViewMode(IGridViewModeDefinition definition, string viewModeName)
    {
      if (viewModeName == "Grid")
      {
        this.SetDataItems((IList<IColumnDefinition>) definition.Columns, (ItemsListBase) this.ItemsGrid);
      }
      else
      {
        if (!(viewModeName == "TreeTable"))
          return;
        this.SetDataItems((IList<IColumnDefinition>) definition.Columns, (ItemsListBase) this.ItemsTreeTable);
        bool? nullable = definition.EnableDragAndDrop;
        if (nullable.HasValue)
        {
          ItemsTreeTable itemsTreeTable = this.ItemsTreeTable;
          nullable = definition.EnableDragAndDrop;
          int num = nullable.Value ? 1 : 0;
          itemsTreeTable.EnableDragAndDrop = num != 0;
        }
        nullable = definition.EnableInitialExpanding;
        if (nullable.HasValue)
        {
          ItemsTreeTable itemsTreeTable = this.ItemsTreeTable;
          nullable = definition.EnableInitialExpanding;
          int num = nullable.Value ? 1 : 0;
          itemsTreeTable.EnableInitialExpanding = num != 0;
        }
        if (!string.IsNullOrEmpty(this.ItemsTreeTable.ExpandedNodesCookieName))
          return;
        this.ItemsTreeTable.ExpandedNodesCookieName = definition.ExpandedNodesCookieName;
      }
    }

    protected virtual void SetListViewMode(IListViewModeDefinition definition)
    {
      string str = definition.ClientTemplate;
      if (definition is IDynamicListViewModeDefinition)
        str = this.GetTemplateHtml(definition as IDynamicListViewModeDefinition);
      this.ItemsList.Items.Add(new ItemDescription()
      {
        Name = "Item",
        Markup = str
      });
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Redirects to the root node of the current module.</summary>
    protected virtual void RedirectToModuleRoot()
    {
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      if (actualCurrentNode.ParentNode == null)
        return;
      QueryStringBuilder collection = new QueryStringBuilder(this.Context.Request.QueryString.ToString());
      this.Page.Response.Redirect(RouteHelper.ResolveUrl(actualCurrentNode.ParentNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + collection.ToQueryString());
    }

    /// <summary>Determines the parent.</summary>
    /// <param name="contentType">Type of the content.</param>
    protected virtual void DetermineParent(Type contentType)
    {
      if (this.Manager == null || !(this.Manager.Provider is IHierarchyProvider provider))
        return;
      Type parentType = provider.GetParentType(contentType);
      if (!(parentType != (Type) null) || !(this.Page.GetRequestContext().RouteData.Values["Params"] is string[] parameters))
        return;
      this.parentItem = this.GetParentItemFromParameters(parameters, (UrlDataProviderBase) provider, parentType);
      this.DetermineSecuredObjectFromParent(this.parentItem);
      if (this.parentItem != null)
        return;
      this.RedirectToModuleRoot();
    }

    /// <summary>
    /// Determines from the parent item, what is the secured object used to evaluate command widgets permissions.
    /// </summary>
    protected virtual void DetermineSecuredObjectFromParent(IDataItem parentItem)
    {
      if (!(parentItem is ISecuredObject))
        return;
      this.relatedSecuredObject = (ISecuredObject) parentItem;
    }

    /// <summary>Gets the parent item from parameters.</summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns>The parent data item.</returns>
    protected virtual IDataItem GetParentItemFromParameters(
      string[] parameters,
      UrlDataProviderBase provider,
      Type parentType)
    {
      return provider.GetItemFromUrl(parentType, "/" + parameters[0].ToString(), out string _);
    }

    /// <summary>
    /// Determines the base service url that should be used for the service calls in this view
    /// </summary>
    /// <param name="itemsListBase">The items list base.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected virtual void DetermineServiceUrl(
      ItemsListBase itemsListBase,
      IContentViewMasterDefinition masterDefinition)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string providerName = this.Host.ControlDefinition.ProviderName;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (this.Host.ControlDefinition.ContentType != (Type) null)
        str1 = this.Host.ControlDefinition.ContentType.FullName;
      if (this.Host.ControlDefinition.ManagerType != (Type) null)
        str2 = this.Host.ControlDefinition.ManagerType.FullName;
      string str3 = string.IsNullOrEmpty(masterDefinition.WebServiceBaseUrl) ? "~/Sitefinity/Services/Content/ContentService.svc/" : masterDefinition.WebServiceBaseUrl;
      string str4 = str3;
      if (!str4.EndsWith("/"))
        str4 += "/";
      if (this.ParentItem != null)
      {
        string str5 = this.Page.Request.QueryString["folderId"];
        string str6 = !str5.IsNullOrEmpty() ? str5 : this.ParentItem.Id.ToString();
        str4 = str4 + "parent/" + str6 + "/";
      }
      if (str1 == typeof (Comment).FullName && this.Page.GetRequestContext().RouteData.Values["Params"] is string[] strArray)
        str4 = str4 + "parent/" + strArray[0] + "/";
      string str7 = str4 + string.Format("?itemType={0}&providerName={1}&managerType={2}", (object) str1, (object) providerName, (object) str2);
      string webServiceRootKey = this.GetWebServiceRootKey();
      if (!string.IsNullOrEmpty(webServiceRootKey))
        str7 += string.Format("&root={0}", (object) webServiceRootKey);
      itemsListBase.ServiceBaseUrl = str7;
      itemsListBase.ManagerType = str2;
      itemsListBase.Binder.ManagerType = str2;
      if (itemsListBase is ItemsTreeTable itemsTreeTable)
      {
        itemsTreeTable.ServiceChildItemsBaseUrl = str3 + "children/";
        itemsTreeTable.ServicePredecessorBaseUrl = str3 + "predecessor/";
        itemsTreeTable.ServiceTreeUrl = str3 + "tree/";
        if (!string.IsNullOrEmpty(webServiceRootKey))
        {
          itemsTreeTable.ServiceTreeUrl += string.Format("?root={0}", (object) webServiceRootKey);
          itemsTreeTable.ServiceChildItemsBaseUrl += string.Format("?root={0}", (object) webServiceRootKey);
          itemsTreeTable.ServicePredecessorBaseUrl += string.Format("?root={0}", (object) webServiceRootKey);
          itemsTreeTable.ExpandedNodesCookieName = "sfExpPages_" + webServiceRootKey;
        }
        itemsTreeTable.DataKeyNames = "Id";
        itemsTreeTable.ParentDataKeyName = "ParentId";
      }
      itemsListBase.ContentLocationPreviewUrl = VirtualPathUtility.ToAbsolute("~/" + ContentLocationRoute.path);
    }

    /// <summary>
    /// Returns a collection of Grid, List and Tree mode, if available
    /// </summary>
    /// <returns>List of defined modes</returns>
    protected IList<ItemsListBase> GetAvailableLists()
    {
      if (this.itemLists == null)
      {
        this.itemLists = (IList<ItemsListBase>) new List<ItemsListBase>();
        if (this.ItemsGrid != null)
          this.itemLists.Add((ItemsListBase) this.ItemsGrid);
        if (this.ItemsList != null)
          this.itemLists.Add((ItemsListBase) this.ItemsList);
        if (this.ItemsTreeTable != null)
          this.itemLists.Add((ItemsListBase) this.ItemsTreeTable);
      }
      return this.itemLists;
    }

    /// <summary>Sets grid column properties.</summary>
    /// <param name="columns">The columns.</param>
    internal void SetDataItems(IList<IColumnDefinition> columns, ItemsListBase itemsListBase)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Host.ControlDefinition.ContentType);
      foreach (IColumnDefinition column in (IEnumerable<IColumnDefinition>) columns)
      {
        int num = !SystemManager.IsItemAccessble((object) column) ? 0 : (!this.IsColumnRestricted(column) ? 1 : 0);
        bool flag = false;
        PropertyDescriptor propertyDescriptor = properties?.Find(column.Name, false);
        if (propertyDescriptor is RelatedDataPropertyDescriptor || propertyDescriptor is TaxonomyPropertyDescriptor)
          flag = true;
        if (num != 0 && !flag)
        {
          ItemDescription itemDescription = new ItemDescription()
          {
            Name = Res.ResolveLocalizedValue(column.ResourceClassId, column.Name),
            HeaderText = Res.ResolveLocalizedValue(column.ResourceClassId, column.HeaderText),
            HeaderCssClass = column.HeaderCssClass,
            ItemCssClass = column.ItemCssClass
          };
          if (column.Width > 0)
            itemDescription.Width = column.Width;
          if (column.DisableSorting.HasValue)
            itemDescription.DisableSorting = column.DisableSorting.Value;
          if (!string.IsNullOrEmpty(column.SortExpression))
            itemDescription.SortExpression = column.SortExpression;
          switch (column)
          {
            case IImageColumnDefinition _:
              IImageColumnDefinition columnDefinition1 = (IImageColumnDefinition) column;
              itemDescription.Markup = columnDefinition1.ClientTemplate;
              break;
            case IDataColumnDefinition _:
              IDataColumnDefinition columnDefinition2 = (IDataColumnDefinition) column;
              itemDescription.Markup = columnDefinition2.ClientTemplate;
              break;
            case IDynamicColumnDefinition _:
              IDynamicColumnDefinition columnDefinition3 = (IDynamicColumnDefinition) column;
              if (Activator.CreateInstance(columnDefinition3.DynamicMarkupGenerator) is IDynamicMarkupGenerator instance)
              {
                instance.Configure(columnDefinition3.GeneratorSettings);
                if (instance.Visible)
                {
                  itemDescription.Markup = instance.GetMarkup();
                  break;
                }
                continue;
              }
              continue;
            case ICommandColumnDefinition _:
              ICommandColumnDefinition columnDefinition4 = (ICommandColumnDefinition) column;
              itemDescription.Markup = string.Format("<div><a href=\"#\" class=\"sf_binderCommand_{0}\">{0}</a></div>", (object) Res.ResolveLocalizedValue(columnDefinition4.ResourceClassId, columnDefinition4.Name));
              break;
            case IActionMenuColumnDefinition _:
              string controlMarkup = this.GetControlMarkup((Control) new ListItemActionMenuLiteralControl((IActionMenuDefinition) column));
              if (!controlMarkup.IsNullOrEmpty())
              {
                itemDescription.Markup = controlMarkup;
                break;
              }
              itemDescription = (ItemDescription) null;
              break;
            default:
              continue;
          }
          if (itemDescription != null)
            itemsListBase.Items.Add(itemDescription);
        }
      }
    }

    /// <summary>Sets grid dialogs properties.</summary>
    /// <param name="dialogs">The dialogs.</param>
    internal virtual void SetDialogs(IList<IDialogDefinition> dialogs, ItemsListBase itemsList)
    {
      foreach (IDialogDefinition dialog in (IEnumerable<IDialogDefinition>) dialogs)
      {
        if (SystemManager.IsItemAccessble((object) dialog))
        {
          if (this.ParentItem != null)
          {
            if (!string.IsNullOrEmpty(dialog.Parameters))
            {
              Dictionary<string, string> paramsDictionary = this.GetParamsDictionary(dialog.Parameters);
              bool flag = false;
              if (paramsDictionary.ContainsKey("parentId"))
              {
                paramsDictionary.Remove("parentId");
                flag = true;
              }
              if (paramsDictionary.ContainsKey("parentType"))
              {
                paramsDictionary.Remove("parentType");
                flag = true;
              }
              if (flag)
                dialog.Parameters = this.GetParamsString(paramsDictionary);
            }
            if (!string.IsNullOrEmpty(dialog.Parameters))
              dialog.Parameters += string.Format("&parentId={0}&parentType={1}", (object) this.ParentItem.Id, (object) this.ParentItem.GetType().FullName);
            else
              dialog.Parameters = string.Format("?parentId={0}&parentType={1}", (object) this.ParentItem.Id, (object) this.ParentItem.GetType().FullName);
          }
          itemsList.Dialogs.Add(dialog);
        }
      }
    }

    /// <summary>Sets grid prompt dialogs.</summary>
    /// <param name="dialogs">The dialogs.</param>
    internal void SetPromptDialogs(IList<IPromptDialogDefinition> dialogs, ItemsListBase itemsList)
    {
      foreach (IPromptDialogDefinition dialog in (IEnumerable<IPromptDialogDefinition>) dialogs)
        itemsList.PromptDialogs.Add(dialog);
    }

    /// <summary>Sets the links.</summary>
    /// <param name="links">The links.</param>
    internal virtual void SetLinks(IList<ILinkDefinition> links, ItemsListBase itemsList)
    {
      foreach (IDefinition link in (IEnumerable<ILinkDefinition>) links)
      {
        LinkDefinition definition = link.GetDefinition<LinkDefinition>();
        if (definition.CommandName == "parentComments" && this.ParentItem != null)
        {
          UrlDataProviderBase provider = (UrlDataProviderBase) this.Manager.Provider;
          string str = this.ParentItem is ILocatable parentItem ? provider.GetItemUrl(parentItem) : string.Empty;
          definition.NavigateUrl = string.Format(definition.NavigateUrl, (object) this.ParentItem.Id, (object) str);
        }
        definition.NavigateUrl = RouteHelper.ResolveUrl(definition.NavigateUrl, UrlResolveOptions.Rooted);
        itemsList.Links.Add((ILinkDefinition) definition);
      }
    }

    /// <summary>
    /// Sets the properties of the decision screens in the template.
    /// </summary>
    /// <param name="decisionScreens">The decision screens.</param>
    internal void SetDecisionScreens(IList<IDecisionScreenDefinition> dsDefinitions)
    {
      if (dsDefinitions.Count <= 0)
        return;
      Control control = this.Container.GetControl<Control>("decisionScreens", false);
      if (control == null)
        return;
      this.decisionScreens = new Dictionary<string, DecisionScreen>();
      foreach (IDecisionScreenDefinition dsDefinition in (IEnumerable<IDecisionScreenDefinition>) dsDefinitions)
      {
        DecisionScreen child = new DecisionScreen();
        child.DecisionType = dsDefinition.DecisionType;
        if (!string.IsNullOrEmpty(dsDefinition.Name))
          ControlUtilities.SetControlIdFromName(dsDefinition.Name, (Control) child);
        bool? displayed = dsDefinition.Displayed;
        if (displayed.HasValue)
        {
          DecisionScreen decisionScreen = child;
          displayed = dsDefinition.Displayed;
          int num = displayed.Value ? 1 : 0;
          decisionScreen.Displayed = num != 0;
        }
        foreach (ICommandWidgetDefinition action1 in dsDefinition.Actions)
        {
          bool flag = true;
          CommandWidgetElement action2 = action1 as CommandWidgetElement;
          if (this.relatedSecuredObject != null && action1 is CommandWidgetElement && !string.IsNullOrEmpty(((CommandWidgetElement) action1).PermissionSet) && !string.IsNullOrEmpty(((CommandWidgetElement) action1).ActionName) && (action2 == null || action2.RelatedSecuredObjectId.IsNullOrWhitespace() && action2.RelatedSecuredObjectTypeName.IsNullOrWhitespace()))
            flag = this.relatedSecuredObject.IsGranted(((CommandWidgetElement) action1).PermissionSet, ((CommandWidgetElement) action1).ActionName);
          else if (action1 is CommandWidgetElement && !string.IsNullOrEmpty(((CommandWidgetElement) action1).PermissionSet) && !string.IsNullOrEmpty(((CommandWidgetElement) action1).ActionName) && !ClaimsManager.GetCurrentIdentity().IsUnrestricted && action2 != null)
          {
            ISecuredObject securedObject1 = (ISecuredObject) null;
            if (!action2.RelatedSecuredObjectId.IsNullOrWhitespace() && !action2.RelatedSecuredObjectTypeName.IsNullOrWhitespace())
            {
              Type type = WcfHelper.ResolveEncodedTypeName(action2.RelatedSecuredObjectTypeName);
              Guid result;
              if (Guid.TryParse(action2.RelatedSecuredObjectId, out result))
              {
                IManager manager = action2.RelatedSecuredObjectManagerTypeName.IsNullOrWhitespace() ? ManagerBase.GetMappedManager(type, action2.RelatedSecuredObjectProviderName) : ManagerBase.GetManager(action2.RelatedSecuredObjectManagerTypeName, action2.RelatedSecuredObjectProviderName);
                if (manager != null)
                {
                  bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
                  manager.Provider.SuppressSecurityChecks = true;
                  object securedObject2 = this.GetSecuredObject(manager, type, result);
                  manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
                  if (securedObject2 != null)
                    securedObject1 = securedObject2 as ISecuredObject;
                }
              }
            }
            if (securedObject1 != null)
              flag = securedObject1.IsGranted(action2.PermissionSet, action2.ActionName);
            else
              flag = false;
          }
          if (flag && this.UseAction(action2))
            child.ActionItems.Add(new ActionItem()
            {
              CommandName = action1.CommandName,
              CssClass = action1.CssClass,
              Title = this.GetLabel(action1.ResourceClassId, action1.Text)
            });
        }
        child.MessageText = this.GetLabel(dsDefinition.ResourceClassId, dsDefinition.MessageText);
        child.MessageType = dsDefinition.MessageType;
        child.Title = this.GetLabel(dsDefinition.ResourceClassId, dsDefinition.Title);
        control.Controls.Add((Control) child);
        if (!this.decisionScreens.ContainsKey(dsDefinition.DecisionType.ToString()))
          this.decisionScreens.Add(dsDefinition.DecisionType.ToString(), child);
      }
    }

    /// <summary>
    /// Gets the secured object from the specified type with the specified id.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="securedObjectTypeName">Name of the secured object type.</param>
    /// <param name="securedObjectId">The secured object id.</param>
    protected internal virtual object GetSecuredObject(
      IManager manager,
      Type securedObjectTypeName,
      Guid securedObjectId)
    {
      return manager.GetItem(securedObjectTypeName, securedObjectId);
    }

    /// <summary>Sets and configures a widgetBar control.</summary>
    /// <param name="widgetBar">The widget bar.</param>
    /// <param name="widgetBarID">The ID of the widget bar.</param>
    /// <param name="widgetBarDefinition">The widget bar definition.</param>
    /// <returns></returns>
    internal bool ConfigureWidgetBar(
      ref WidgetBar widgetBar,
      string widgetBarID,
      IWidgetBarDefinition widgetBarDefinition)
    {
      if (widgetBar == null)
        widgetBar = this.Container.GetControl<WidgetBar>(widgetBarID, false);
      widgetBar.RelatedSecuredObject = this.relatedSecuredObject;
      bool hasSections = widgetBarDefinition.HasSections;
      if (hasSections)
      {
        if (widgetBar == null)
        {
          ControlCollection controls = this.Container.GetControl<Control>(widgetBarID + "Panel", true).Controls;
          WidgetBar child = new WidgetBar();
          child.ID = widgetBarID;
          controls.Add((Control) child);
        }
        widgetBarDefinition = (IWidgetBarDefinition) widgetBarDefinition.GetDefinition<WidgetBarDefinition>();
        foreach (IWidgetBarSectionDefinition section in widgetBarDefinition.Sections)
        {
          foreach (IWidgetDefinition widgetDefinition in section.Items)
          {
            widgetDefinition.Visible = new bool?(SystemManager.IsItemAccessble((object) widgetDefinition));
            if (widgetDefinition is IModeStateWidgetDefinition)
            {
              foreach (IStateCommandWidgetDefinition state in ((IStateWidgetDefinition) widgetDefinition).States)
                this.ConfigureViewModeSwitchingLink(state);
            }
          }
        }
        widgetBar.WidgetBarDefiniton = widgetBarDefinition;
        ITextControl control = this.Container.GetControl<ITextControl>(widgetBarID + "Title", false);
        if (control != null)
          control.Text = this.GetLabel(widgetBarDefinition.ResourceClassId, widgetBarDefinition.Title);
      }
      return hasSections;
    }

    internal Dictionary<string, string> GetParamsDictionary(string paramsString)
    {
      Dictionary<string, string> paramsDictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (!string.IsNullOrEmpty(paramsString))
      {
        int num = paramsString.IndexOf('?');
        if (num > -1)
          paramsString = paramsString.Substring(num + 1);
        string[] strArray1 = paramsString.Split('&');
        for (int index = 0; index < strArray1.Length; ++index)
        {
          string[] strArray2 = strArray1[index].Split('=');
          if (strArray2.Length != 2)
          {
            strArray2 = new string[2];
            string str = strArray1[index];
            int length = str.IndexOf('=');
            if (length != -1)
            {
              strArray2[0] = str.Substring(0, length);
              strArray2[1] = str.Length > length + 1 ? str.Substring(length + 1) : (string) null;
            }
            else
            {
              strArray2[0] = str;
              strArray2[1] = (string) null;
            }
          }
          string key = strArray2[0];
          string str1 = strArray2.Length == 2 ? strArray2[1] : key;
          paramsDictionary.Add(key, str1);
        }
      }
      return paramsDictionary;
    }

    internal string GetParamsString(Dictionary<string, string> paramsDictionary)
    {
      string paramsString = string.Empty;
      foreach (KeyValuePair<string, string> keyValuePair in paramsDictionary)
      {
        paramsString = paramsString.Length != 0 ? paramsString + "&" : paramsString + "?";
        paramsString = paramsString + keyValuePair.Key + "=" + keyValuePair.Value;
      }
      return paramsString;
    }

    /// <summary>Sets the custom client parameters.</summary>
    /// <param name="customClientParams">The custom client params.</param>
    protected virtual void SetCustomClientParameters(Dictionary<string, string> customClientParams)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor baseDescriptor = this.GetBaseDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      if (this.ItemsGrid != null)
        baseDescriptor.AddComponentProperty("itemsGrid", this.ItemsGrid.ClientID);
      if (this.ItemsList != null)
        baseDescriptor.AddComponentProperty("itemsList", this.ItemsList.ClientID);
      if (this.ItemsTreeTable != null)
        baseDescriptor.AddComponentProperty("itemsTreeTable", this.ItemsTreeTable.ClientID);
      if (this.BinderSearch != null)
        baseDescriptor.AddComponentProperty("binderSearch", this.BinderSearch.ClientID);
      baseDescriptor.AddProperty("titleID", (object) this.TitleControl.ClientID);
      baseDescriptor.AddProperty("titleText", (object) Regex.Replace(this.TitleControl.InnerHtml, "<(.|\\n)*?>", string.Empty));
      baseDescriptor.AddProperty("parentTitleFormat", (object) this.parentTitleFormat);
      baseDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      if (this.sidebar != null)
      {
        baseDescriptor.AddProperty("sidebarID", (object) this.sidebar.ClientID);
        baseDescriptor.AddComponentProperty("sidebar", this.sidebar.ClientID);
      }
      if (this.toolbar != null)
      {
        baseDescriptor.AddProperty("toolbarID", (object) this.toolbar.ClientID);
        baseDescriptor.AddComponentProperty("toolbar", this.toolbar.ClientID);
      }
      if (this.contextBar != null)
        baseDescriptor.AddComponentProperty("contextBar", this.contextBar.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      if (this.decisionScreens != null)
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (KeyValuePair<string, DecisionScreen> decisionScreen in this.decisionScreens)
          dictionary.Add(decisionScreen.Key, decisionScreen.Value.ClientID);
        string str = scriptSerializer.Serialize((object) dictionary);
        baseDescriptor.AddProperty("decisionScreens", (object) str);
      }
      if (this.commandableItems.Count > 0)
      {
        List<string> stringList = new List<string>();
        foreach (Control commandableItem in this.commandableItems)
          stringList.Add(commandableItem.ClientID);
        string str = scriptSerializer.Serialize((object) stringList);
        baseDescriptor.AddProperty("widgetIds", (object) str);
      }
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      baseDescriptor.AddProperty("_isAdmin", (object) (bool) (currentIdentity != null ? (currentIdentity.IsUnrestricted ? 1 : 0) : 0));
      baseDescriptor.AddProperty("_currentUserId", (object) SecurityManager.GetCurrentUserId());
      baseDescriptor.AddProperty("hasSidebar", (object) this.HasSidebar);
      baseDescriptor.AddProperty("hasToolbar", (object) this.HasToolbar);
      baseDescriptor.AddProperty("_showNotSharedCommandName", (object) "showNotShared");
      baseDescriptor.AddProperty("_showAllItemsCommandName", (object) "showAllItems");
      baseDescriptor.AddProperty("_showMyItemsCommandName", (object) "showMyItems");
      baseDescriptor.AddProperty("_showDraftItemsCommandName", (object) "showMasterItems");
      baseDescriptor.AddProperty("_showPublishedItemsCommandName", (object) "showPublishedItems");
      baseDescriptor.AddProperty("_showScheduledItemsCommandName", (object) "showScheduledItems");
      baseDescriptor.AddProperty("_pendingApprovalItemsCommandName", (object) "showPendingApprovalItems");
      baseDescriptor.AddProperty("_pendingReviewItemsCommandName", (object) "showPendingReviewItems");
      baseDescriptor.AddProperty("_pendingPublishingItemsCommandName", (object) "showPendingPublishingItems");
      baseDescriptor.AddProperty("_rejectedItemsCommandName", (object) "showRejectedItems");
      baseDescriptor.AddProperty("_pendingApprovalPagesCommandName", (object) "showPendingApprovalPages");
      baseDescriptor.AddProperty("_pendingReviewPagesCommandName", (object) "showPendingReviewPages");
      baseDescriptor.AddProperty("_pendingPublishingPagesCommandName", (object) "showPendingPublishingPages");
      baseDescriptor.AddProperty("_awaitingMyActionPagesCommandName", (object) "showAwaitingMyActionPages");
      baseDescriptor.AddProperty("_rejectedPagesCommandName", (object) "showRejectedPages");
      baseDescriptor.AddProperty("_filterCommandName", (object) "filter");
      baseDescriptor.AddProperty("_searchCommandName", (object) "search");
      baseDescriptor.AddProperty("_closeSearchCommandName", (object) "closeSearch");
      baseDescriptor.AddProperty("_tagFilterCommandName", (object) "tagFilter");
      baseDescriptor.AddProperty("_categoryFilterCommandName", (object) "categoryFilter");
      baseDescriptor.AddProperty("_gridViewStateCommandName", (object) "gridViewState");
      baseDescriptor.AddProperty("_listViewStateCommandName", (object) "listViewState");
      baseDescriptor.AddProperty("_treeViewStateCommandName", (object) "treeViewState");
      baseDescriptor.AddProperty("_notImplementedCommandName", (object) "notImplemented");
      baseDescriptor.AddProperty("_parentPropertiesCommandName", (object) "parentProperties");
      baseDescriptor.AddProperty("_showMoreTranslationsCommandName", (object) "showMoreTranslations");
      baseDescriptor.AddProperty("_hideMoreTranslationsCommandName", (object) "hideMoreTranslations");
      baseDescriptor.AddProperty("_clientMappedCommnadNames", (object) new JavaScriptSerializer().Serialize((object) this.clientMappedCommnadNames));
      baseDescriptor.AddProperty("_promptWindowId", (object) this.PromptWindow.ClientID);
      baseDescriptor.AddProperty("_lockWindowId", (object) this.LockWindow.ClientID);
      baseDescriptor.AddProperty("_managerType", (object) this.ManagerType);
      baseDescriptor.AddProperty("_providerSelectorId", (object) this.ProviderSelectorPanel.ClientID);
      baseDescriptor.AddProperty("_selectedItemFilterCssClass", (object) this.SelectedFilterItemCssClass);
      baseDescriptor.AddProperty("_noEditPermissionsConfirmationMessage", (object) Res.Get<SecurityResources>().NoEditPermissionsPreviewOnlyConfirmation);
      baseDescriptor.AddProperty("_noEditPermissionsPreviewOnlyConfirmationNoViewOption", (object) Res.Get<SecurityResources>().NoEditPermissionsPreviewOnlyConfirmationNoViewOption);
      baseDescriptor.AddProperty("_noEditPermissionsViewDialogTitle", (object) Res.Get<SecurityResources>().NoEditPermissionsViewDialogTitle);
      baseDescriptor.AddProperty("_hasBeenLockedForEditingBySince", (object) Res.Get<Labels>().HasBeenLockedForEditingBySince);
      baseDescriptor.AddProperty("_noPermissionsToSetAsHomepage", (object) Res.Get<SecurityResources>().NoPermissionsToSetAsHomepage);
      baseDescriptor.AddProperty("_doNotBindOnClientWhenPageIsLoaded", (object) this.DoNotBindOnClientWhenPageIsLoaded);
      baseDescriptor.AddProperty("customParameters", (object) this.GetClientParametersInJSON());
      baseDescriptor.AddProperty("sortCommandName", (object) SortWidget.SortCommandName);
      baseDescriptor.AddProperty("showHierarchicalCommandName", (object) SortWidget.ShowHierarchicalCommandName);
      baseDescriptor.AddProperty("showHierarchicalExpression", (object) this.ShowHierarchicalExpression);
      baseDescriptor.AddProperty("sortExpression", (object) this.SortExpression);
      string str1 = !this.MasterGridViewDefinition.LandingPageId.HasValue || !(this.MasterGridViewDefinition.LandingPageId.Value != Guid.Empty) ? RouteHelper.ResolveUrl("~/", UrlResolveOptions.Rooted | UrlResolveOptions.AppendTrailingSlash) : RouteHelper.ResolveUrl(DefinitionsHelper.GetBaseUrl(this.MasterGridViewDefinition.LandingPageId.Value), UrlResolveOptions.Rooted | UrlResolveOptions.AppendTrailingSlash);
      baseDescriptor.AddProperty("_baseViewPath", (object) str1);
      if (this.ParentItem != null)
        baseDescriptor.AddProperty("_parentId", (object) this.ParentItem.Id);
      baseDescriptor.AddProperty("_contentLifecycleStatusName", (object) "Status");
      baseDescriptor.AddProperty("_workflowStateName", (object) "ApprovalWorkflowState");
      baseDescriptor.AddProperty("_publishedDraftStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_publishedDraftWorkflowStateFilterExpression", (object) "Published");
      baseDescriptor.AddProperty("_notPublishedDraftStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_notPublishedDraftWorkflowStateFilterExpression", (object) "Draft");
      baseDescriptor.AddProperty("_scheduledItemsStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_scheduledItemsWorkflowStateFilterExpression", (object) "Scheduled");
      baseDescriptor.AddProperty("_pendingApprovalItemsStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_pendingApprovalItemsWorkflowStateFilterExpression", (object) "AwaitingApproval");
      baseDescriptor.AddProperty("_pendingReviewItemsStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_pendingReviewItemsWorkflowStateFilterExpression", (object) "AwaitingReview");
      baseDescriptor.AddProperty("_pendingPublishingItemsStatusFilterExpression", (object) "Master");
      baseDescriptor.AddProperty("_pendingPublishingItemsWorkflowStateFilterExpression", (object) "AwaitingPublishing");
      baseDescriptor.AddProperty("_rejectedItemsWorkflowStateFilterExpression", (object) "Rejected");
      baseDescriptor.AddProperty("_supportsMultilingual", (object) this.SupportsMultilingual);
      baseDescriptor.AddProperty("_supportsApprovalWorkflow", (object) this.SupportsApprovalWorkflow);
      baseDescriptor.AddProperty("_cookieKey", (object) this.CookieKey);
      this.AddCulturesSpecificValues(baseDescriptor);
      string str2 = this.MasterGridViewDefinition.DraftFilter;
      if (str2.IsNullOrWhitespace())
        str2 = DefinitionsHelper.NotPublishedDraftsFilterExpression;
      baseDescriptor.AddProperty("_draftFilter", (object) str2);
      string str3 = this.MasterGridViewDefinition.PublishedFilter;
      if (str3.IsNullOrWhitespace())
        str3 = DefinitionsHelper.PublishedDraftsFilterExpression;
      baseDescriptor.AddProperty("_publishedFilter", (object) str3);
      string str4 = this.MasterGridViewDefinition.ScheduledFilter;
      if (str4.IsNullOrWhitespace())
        str4 = DefinitionsHelper.ScheduledItemsFilterExpression;
      baseDescriptor.AddProperty("_scheduledFilter", (object) str4);
      string str5 = this.MasterGridViewDefinition.PendingApprovalFilter;
      if (str5.IsNullOrWhitespace())
        str5 = DefinitionsHelper.PendingApprovalItemsFilterExpression;
      baseDescriptor.AddProperty("_pendingApprovalFilter", (object) str5);
      baseDescriptor.AddProperty("_pendingReviewFilter", (object) DefinitionsHelper.PendingReviewItemsFilterExpression);
      baseDescriptor.AddProperty("_pendingPublishingFilter", (object) DefinitionsHelper.PendingPublishingItemsFilterExpression);
      baseDescriptor.AddProperty("_rejectedFilter", (object) DefinitionsHelper.RejectedItemsFilterExpression);
      string str6 = scriptSerializer.Serialize((object) this.localization);
      baseDescriptor.AddProperty("_localization", (object) str6);
      baseDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        ISiteContext currentSiteContext = multisiteContext.CurrentSiteContext;
        if (currentSiteContext.ResolutionType == SiteContextResolutionTypes.ByParam)
        {
          baseDescriptor.AddProperty("_currentSiteId", (object) currentSiteContext.Site.Id.ToString());
          baseDescriptor.AddProperty("_siteIdParamKey", (object) "sf_site");
        }
      }
      string applicationPath = this.Context.Request.ApplicationPath;
      if (!applicationPath.EndsWith("/"))
        applicationPath += "/";
      baseDescriptor.AddProperty("_appPath", (object) applicationPath);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        baseDescriptor
      };
    }

    /// <summary>
    /// Adds the cultures specific values to the script descriptor.
    /// </summary>
    /// <param name="scriptDescriptor">The script descriptor.</param>
    protected internal virtual void AddCulturesSpecificValues(
      ScriptControlDescriptor scriptDescriptor)
    {
      if (!this.SupportsMultilingual)
        return;
      string name = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      CultureInfo[] frontendLanguages = SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages;
      scriptDescriptor.AddProperty("_defaultLanguage", (object) name);
      scriptDescriptor.AddProperty("_definedLanguages", (object) ((IEnumerable<CultureInfo>) frontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (ci => ci.Name)));
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
      foreach (ScriptReference scriptReference in base.GetScriptReferences())
        scriptReferences.Add(scriptReference);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.MasterGridView.js", typeof (MasterGridView).Assembly.FullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Gets the flat provider selector panel.</summary>
    /// <value>The provider selector panel.</value>
    protected virtual FlatProviderSelector ProviderSelectorPanel => this.Container.GetControl<FlatProviderSelector>("providerSelectorPanel", true);

    /// <summary>Gets the child types selector.</summary>
    protected virtual SiblingTypeSelector ChildTypesSelector => this.Container.GetControl<SiblingTypeSelector>("stsChildTypes", false);

    private bool IsRecycleBinEnabled()
    {
      Type contentType = this.Host.ControlDefinition.ContentType;
      return !(contentType == (Type) null) && !typeof (MediaContent).IsAssignableFrom(contentType) && ObjectFactory.Resolve<IRecycleBinStateResolver>().ShouldMoveToRecycleBin(contentType);
    }

    private void SetSortWidgetSellection(
      IMasterViewDefinition masterDefinition,
      string sortExpression)
    {
      if (sortExpression == null || !(masterDefinition.Toolbar is WidgetBarElement toolbar))
        return;
      foreach (WidgetBarSectionElement widgetSection in toolbar.WidgetSections)
      {
        if (widgetSection != null)
        {
          foreach (WidgetElement widgetElement in widgetSection.Items.Where<WidgetElement>((Func<WidgetElement, bool>) (item => item is DynamicCommandWidgetElement && item.WidgetType == typeof (SortWidget))))
          {
            DynamicCommandWidgetElement commandWidgetElement = widgetElement as DynamicCommandWidgetElement;
            ConfigElementList<DynamicItemElement> items = commandWidgetElement.Items;
            Func<DynamicItemElement, bool> predicate = (Func<DynamicItemElement, bool>) (item => item.Value == sortExpression);
            commandWidgetElement.SelectedValue = items.FirstOrDefault<DynamicItemElement>(predicate) != null ? sortExpression : SortWidget.CustomOptionName;
          }
        }
      }
    }

    private void DetermineFolder()
    {
      string input = this.Page.Request.QueryString["folderId"];
      Guid result;
      if (string.IsNullOrEmpty(input) || this.Manager == null || !(this.Manager is IFolderManager manager) || !Guid.TryParse(input, out result))
        return;
      this.folder = (IFolder) manager.GetFolder(result);
    }

    private void DetermineTitle(IContentViewMasterDefinition masterDefinition)
    {
      if (!string.IsNullOrEmpty(masterDefinition.ParentTitleFormat))
        this.parentTitleFormat = this.GetLabel(masterDefinition.ResourceClassId, masterDefinition.ParentTitleFormat);
      string input = (string) null;
      if (this.Folder != null && this.parentTitleFormat != null)
        input = string.Format(this.parentTitleFormat, (object) HttpUtility.HtmlEncode((string) this.Folder.Title));
      else if (this.ParentItem != null && this.parentTitleFormat != null)
        input = string.Format(this.parentTitleFormat, (object) HttpUtility.HtmlEncode(!this.SupportsMultilingual || string.IsNullOrEmpty(this.Page.Request.QueryString["lang"]) ? this.Manager.Provider.GetItemTitleValue(this.ParentItem) : this.Manager.Provider.GetItemTitleValue(this.ParentItem, new CultureInfo(this.Page.Request.QueryString["lang"]))));
      else if (!string.IsNullOrEmpty(masterDefinition.Title))
        input = this.GetLabel(masterDefinition.ResourceClassId, masterDefinition.Title);
      if (input == null)
        return;
      this.TitleControl.InnerHtml = ControlUtilities.Sanitize(input);
    }

    private void ConfigureViewModeSwitchingLink(IStateCommandWidgetDefinition stateCommandWidget)
    {
      stateCommandWidget.IsSelected = stateCommandWidget.Name == this.CurrentViewMode;
      if (stateCommandWidget.IsSelected)
      {
        stateCommandWidget.NavigateUrl = string.Empty;
      }
      else
      {
        string rawUrl = SystemManager.CurrentHttpContext.Request.RawUrl;
        string empty = string.Empty;
        int length = rawUrl.IndexOf('?');
        if (length <= -1)
          return;
        rawUrl.Right(length - 1);
        rawUrl.Left(length);
      }
    }

    private string GetTemplateHtml(IDynamicListViewModeDefinition definition) => definition == null ? (string) null : this.GetTemplateHtml(definition.IsClientTemplateDynamic, definition.VirtualPath, definition.AssemblyName, definition.ResourceFileName, definition.AssemblyInfo, definition.ClientTemplate, (IActionMenuDefinition) definition);

    private string GetTemplateHtml(
      bool isDynamic,
      string virtualPath,
      string assemblyName,
      string resourceFileName,
      Type assemblyInfo,
      string templateDeclaration,
      IActionMenuDefinition actionMenu = null)
    {
      string templateDeclaration1 = (string) null;
      ITemplate template;
      if (isDynamic)
        template = ControlUtilities.GetTemplate((string) null, templateDeclaration.GetHashCode().ToString(), (Type) null, templateDeclaration);
      else if (!string.IsNullOrEmpty(assemblyName))
      {
        using (Stream manifestResourceStream = Assembly.Load(assemblyName).GetManifestResourceStream(resourceFileName))
          templateDeclaration1 = new StreamReader(manifestResourceStream).ReadToEnd();
        template = ControlUtilities.GetTemplate((string) null, resourceFileName, (Type) null, templateDeclaration1);
      }
      else
        template = ControlUtilities.GetTemplate(virtualPath, resourceFileName, assemblyInfo, templateDeclaration);
      GenericContainer container = new GenericContainer();
      template.InstantiateIn((Control) container);
      if (actionMenu != null)
      {
        ListItemActionMenuLiteralControl control = container.GetControl<ListItemActionMenuLiteralControl>(nameof (actionMenu), false);
        if (control != null)
          control.ActionMenu = actionMenu;
      }
      return this.GetControlMarkup((Control) container);
    }

    private string GetControlMarkup(Control control)
    {
      StringBuilder sb = new StringBuilder();
      using (StringWriter writer1 = new StringWriter(sb))
      {
        using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
          control.RenderControl(writer2);
      }
      return sb.ToString();
    }

    private string GetClientParametersInJSON()
    {
      Dictionary<string, string> customClientParams = new Dictionary<string, string>();
      this.SetCustomClientParameters(customClientParams);
      string parametersInJson = string.Empty;
      if (customClientParams != null)
        parametersInJson = new JavaScriptSerializer().Serialize((object) customClientParams);
      return parametersInJson;
    }

    private bool IsColumnRestricted(IColumnDefinition column)
    {
      foreach (IRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<IColumnRestrictionStrategy>())
      {
        if (restrictionStrategy.IsRestricted((object) column))
          return true;
      }
      return false;
    }
  }
}
