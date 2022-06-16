// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplatesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// This static class provides methods that define the ContentView controls and views used by the page templates.
  /// </summary>
  internal static class PageTemplatesDefinitions
  {
    /// <summary>
    /// Name of the definition for configuring list of frontend page templates
    /// </summary>
    public const string FrontendPageTemplatesDefinitionName = "FrontendPageTemplates";
    /// <summary>
    /// Name of the definition for configuring list of backend page templates
    /// </summary>
    public const string BackendPageTemplatesDefinitionName = "BackendPageTemplates";
    /// <summary>
    /// Name of the view used at listview mode with frontend page templates
    /// </summary>
    public const string FrontendPageTemplatesListViewName = "FrontendPageTemplatesListView";
    /// <summary>
    /// Name of the view used as listview mode with backend page templates
    /// </summary>
    public const string BackendPageTemplatesListViewName = "BackendPageTemplatesListView";
    /// <summary>
    /// Name of the view used to edit page properties for frontend page templates
    /// </summary>
    public const string FrontendPageTemplatesEditViewName = "FrontendPageTemplatesEdit";
    /// <summary>
    /// Name of the view used to duplicate page properties for backend page templates
    /// </summary>
    public const string BackendPageTemplatesDuplicateViewName = "BackendPageTemplatesDuplicate";
    /// <summary>
    /// Name of the view used to duplicate page properties for frontend page templates
    /// </summary>
    public const string FrontendPageTemplatesDuplicateViewName = "FrontendPageTemplatesDuplicate";
    /// <summary>
    /// Name of the view used to edit page properties for backend page templates
    /// </summary>
    public const string BackendPageTemplatesEditViewName = "BackendPageTemplatesEdit";
    /// <summary>
    /// Name of the view used to create a page in the frontend.
    /// </summary>
    public const string FrontendPageTemplatesCreateViewName = "FrontendPageTemplatesCreate";
    /// <summary>
    /// Name of the view used to create a page in the backend.
    /// </summary>
    public const string BackendPageTemplatesCreateViewName = "BackendPageTemplatesCreate";
    /// <summary>The resource class id id for pages.</summary>
    public static readonly string ResourceClassId = typeof (PageResources).Name;
    /// <summary>The name of the module;</summary>
    public const string ModuleName = "PageTemplates";

    /// <summary>
    /// Defines the ContentView control for the view listing frontend page template
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFrontendPageTemplatesContentView(
      ContentViewConfig config,
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "FrontendPageTemplates", typeof (PageTemplate)).DoNotUseWorkflow().SetManagerType(typeof (PageManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      MasterGridViewElement element1 = PageTemplatesDefinitions.DefineMasterGridView(fluentContentView, config, false);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      DetailFormViewElement element2 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, true, "FrontendPageTemplatesCreate");
      DetailFormViewElement element3 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, false, "FrontendPageTemplatesEdit");
      DetailFormViewElement element4 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, true, "FrontendPageTemplatesDuplicate", isDuplicate: true);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element3);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element4);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control for the view listing backend page templates.
    /// </summary>
    /// <param name="config">The config.</param>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    internal static ContentViewControlElement DefineBackendPageTemplatesContentView(
      ContentViewConfig config,
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "BackendPageTemplates", typeof (PageTemplate)).SetManagerType(typeof (PageManager)).DoNotUseWorkflow();
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      MasterGridViewElement element1 = PageTemplatesDefinitions.DefineMasterGridView(fluentContentView, config, true);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      DetailFormViewElement element2 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, true, "BackendPageTemplatesCreate", true);
      DetailFormViewElement element3 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, false, "BackendPageTemplatesEdit", true);
      DetailFormViewElement element4 = PageTemplatesDefinitions.DefineDetailsView(fluentContentView, true, "BackendPageTemplatesDuplicate", true, true);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element3);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element4);
      return viewControlElement;
    }

    internal static void DefineToolbar(
      MasterGridViewElement templatesBackendMaster,
      ContentViewConfig config)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) templatesBackendMaster.ToolbarConfig.Sections)
      {
        Name = "main"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "CreateTemplateWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateTemplate";
      commandWidgetElement1.ResourceClassId = typeof (PageResources).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "DeleteTemplateWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupDeleteTemplate";
      commandWidgetElement2.Text = "Delete";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      string contentType = typeof (PageTemplate).FullName;
      IEnumerable<SortingExpressionElement> expressionElements = config.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == contentType && !s.IsCustom));
      config.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == contentType && s.IsCustom));
      DynamicCommandWidgetElement commandWidgetElement3 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement3.Name = "SortTemplatesWidget";
      commandWidgetElement3.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement3.HeaderText = "SortPages";
      commandWidgetElement3.PageSize = 10;
      commandWidgetElement3.MoreLinkText = "More";
      commandWidgetElement3.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement3.WidgetType = typeof (SortWidget);
      commandWidgetElement3.ResourceClassId = PageTemplatesDefinitions.ResourceClassId;
      commandWidgetElement3.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement3.ContentType = typeof (PageTemplate);
      DynamicCommandWidgetElement element4 = commandWidgetElement3;
      element1.Items.Add((WidgetElement) element4);
      foreach (SortingExpressionElement expressionElement in expressionElements)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element4.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element4.Items.Add(dynamicItemElement);
      }
      templatesBackendMaster.ToolbarConfig.Sections.Add(element1);
    }

    internal static void DefineSidebar(MasterGridViewElement pagesBackendMaster, bool isBackend)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "sidebar",
        WrapperTagId = "TemplatesSection",
        Title = "FilterTemplates",
        ResourceClassId = typeof (PageTemplateResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "AllTemplates";
      element2.CommandName = "showAllTemplates";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = "AllTemplates";
      element2.ResourceClassId = typeof (PageResources).Name;
      element2.CssClass = "";
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      element2.ButtonCssClass = "sfSel";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      MultisiteCommandWidgetElement element3 = new MultisiteCommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "ThisSiteTemplates";
      element3.CommandName = "showThisSiteTemplates";
      element3.ButtonType = CommandButtonType.SimpleLinkButton;
      element3.Text = "ThisSiteCapital";
      element3.ResourceClassId = typeof (MultisiteResources).Name;
      element3.CssClass = "";
      element3.WidgetType = typeof (CommandWidget);
      element3.IsSeparator = false;
      element3.IsFilterCommand = true;
      element3.ButtonCssClass = "sfSel";
      element3.ModuleName = "MultisiteInternal";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      MultisiteCommandWidgetElement element4 = new MultisiteCommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "NotSharedTemplates";
      element4.CommandName = "showNotSharedTemplates";
      element4.ButtonType = CommandButtonType.SimpleLinkButton;
      element4.Text = "NotSharedWithAnySite";
      element4.ResourceClassId = typeof (MultisiteResources).Name;
      element4.CssClass = "";
      element4.WidgetType = typeof (CommandWidget);
      element4.IsSeparator = false;
      element4.IsFilterCommand = true;
      element4.ModuleName = "MultisiteInternal";
      items3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items4 = element1.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.Items);
      element5.Name = "MyTemplates";
      element5.CommandName = "showMyTemplates";
      element5.ButtonType = CommandButtonType.SimpleLinkButton;
      element5.Text = "MyTemplates";
      element5.ResourceClassId = typeof (PageTemplateResources).Name;
      element5.CssClass = "";
      element5.WidgetType = typeof (CommandWidget);
      element5.IsSeparator = false;
      items4.Add((WidgetElement) element5);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element1.Items);
      DefinitionsHelper.AddLanguageSection(pagesBackendMaster, isBackend);
      pagesBackendMaster.SidebarConfig.Sections.Add(element1);
      pagesBackendMaster.SidebarConfig.Title = "ManageTemplates";
      pagesBackendMaster.SidebarConfig.ResourceClassId = typeof (PageTemplateResources).Name;
      WidgetBarSectionElement element6 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        WrapperTagId = "ManageAlsoSection",
        Title = "ManageAlso",
        ResourceClassId = typeof (Labels).Name,
        CssClass = "sfWidgetsList"
      };
      ConfigElementList<WidgetElement> items5 = element6.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element6.Items);
      element7.Name = "Pages";
      element7.CommandName = "managePages";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.Text = "Pages";
      element7.ResourceClassId = typeof (PageResources).Name;
      element7.CssClass = "";
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      items5.Add((WidgetElement) element7);
      pagesBackendMaster.SidebarConfig.Sections.Add(element6);
      WidgetBarSectionElement element8 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "Settings",
        WrapperTagId = "SettingsForTemplates",
        Title = "SettingsForTemplates",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfWidgetsList sfSettings sfSeparator"
      };
      ConfigElementList<WidgetElement> items6 = element8.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element8.Items);
      element9.Name = "PermissionsForAllTemplates";
      element9.CommandName = "permissions";
      element9.ButtonType = CommandButtonType.SimpleLinkButton;
      element9.Text = "PermissionsForAllTemplates";
      element9.ResourceClassId = typeof (PageResources).Name;
      element9.CssClass = string.Empty;
      element9.WidgetType = typeof (CommandWidget);
      element9.IsSeparator = false;
      items6.Add((WidgetElement) element9);
      pagesBackendMaster.SidebarConfig.Sections.Add(element8);
    }

    private static MasterGridViewElement DefineMasterGridView(
      ContentViewControlDefinitionFacade fluentContentView,
      ContentViewConfig config,
      bool isForBackendPages)
    {
      Type type = isForBackendPages ? typeof (BackendPagesMasterGridView) : typeof (FrontendPagesMasterGridView);
      string viewName1 = isForBackendPages ? "BackendPageTemplatesListView" : "FrontendPageTemplatesListView";
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName1).SetViewType(type).LocalizeUsing<PageResources>().DisableSorting().SetTitle("Templates").SetCssClass("sfListViewGrid").SetServiceBaseUrl("~/Sitefinity/Services/Pages/PageTemplatesService.svc/").SetTemplateEvaluationMode(TemplateEvalutionMode.Server);
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      GridViewModeElement gridViewModeElement1 = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement1.Name = "Grid";
      GridViewModeElement gridViewModeElement2 = gridViewModeElement1;
      masterGridViewElement.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageTemplatesMasterGridViewExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      };
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) gridViewModeElement2);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dataColumnElement1.Name = "Thumbnail";
      dataColumnElement1.HeaderCssClass = "sfTmplTmb";
      dataColumnElement1.ItemCssClass = "sfTmplTmb";
      dataColumnElement1.ClientTemplate = "<span sys:class=\"{{ 'sfFramework' + Framework}}\"><img sys:src='{{TemplateIconUrl}}' sys:alt='{{Title}}' /></span>";
      DataColumnElement element1 = dataColumnElement1;
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) element1);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dataColumnElement2.Name = "Title";
      dataColumnElement2.HeaderText = "Title";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.HeaderCssClass = "sfTitleCol";
      dataColumnElement2.ItemCssClass = "sfTitleCol";
      dataColumnElement2.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{'sf_binderCommand_editTemplate sfItemTitle ' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() : '') + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus' : '')}}\"><span>{{Title}}</span></a>\r\n                    <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>                         \r\n                    <span sys:class=\"{{ 'sf' + Status.toLowerCase() + ' sfPrimaryStatus'}}\"><span sys:if='AdditionalStatus'>| </span> {{StatusText}}</span>";
      DataColumnElement element2 = dataColumnElement2;
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) element2);
      DefinitionsHelper.AddPersonalizationColumn(gridViewModeElement2, isForBackendPages);
      DefinitionsHelper.AddTranslationsColumn(gridViewModeElement2, isForBackendPages);
      PageTemplatesDefinitions.AddActionsMenuColumn(gridViewModeElement2);
      PageTemplatesDefinitions.AddBasedOnColumn(gridViewModeElement2);
      PageTemplatesDefinitions.AddPagesCountColumn(gridViewModeElement2);
      PageTemplatesDefinitions.AddDateOwnerColumn(gridViewModeElement2);
      PageTemplatesDefinitions.DefineToolbar(masterGridViewElement, config);
      PageTemplatesDefinitions.DefineSidebar(masterGridViewElement, isForBackendPages);
      string viewName2 = isForBackendPages ? "BackendPageTemplatesCreate" : "FrontendPageTemplatesCreate";
      string viewName3 = isForBackendPages ? "BackendPageTemplatesEdit" : "FrontendPageTemplatesEdit";
      string viewName4 = isForBackendPages ? "BackendPageTemplatesDuplicate" : "FrontendPageTemplatesDuplicate";
      string permissionSetName = "PageTemplates";
      string str = isForBackendPages ? RootTaxonType.Backend.ToString() : RootTaxonType.Frontend.ToString();
      string parameters = "?ShowUnlock=true&LockedByUsername={{LockedByUsername}}&ItemName={{Title}}&Title=Templates&ViewUrl={{TemplatePreviewUrl}}/Action/Preview&UnlockServiceUrl=" + RouteHelper.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/Template/UnlockTemplate/{{TemplateId}}", UrlResolveOptions.Rooted);
      string absolute = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Pages/PageTemplatesService.svc/");
      definitionFacade.AddInsertDialog(viewName2, "", Res.Get<PageResources>().BackToTemplates).Done().AddEditDialog(viewName3, "", Res.Get<PageResources>().BackToTemplates).AddParameters("&SuppressBackToButtonLabelModify=true").Done().AddInsertDialog(viewName4, "", Res.Get<PageResources>().BackToTemplates, "{{Id}}", (Type) null, "duplicate").AddParameters("&SuppressBackToButtonLabelModify=true").Done().AddInsertDialog(viewName2, "", Res.Get<PageResources>().BackToTemplates, "{{Id}}", (Type) null, "createChild").Done().AddDialog<ChangePageOwnerDialog>("changePageOwner").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetWidth(Unit.Pixel(450)).SetHeight(Unit.Pixel(570)).DisplayTitleBar().MakeModal().Done().AddDialog<ChangePageOwnerDialog>("batchChangeOwner").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetWidth(Unit.Pixel(450)).SetHeight(Unit.Pixel(570)).DisplayTitleBar().MakeModal().Done().AddPermissionsDialog(Res.Get<PageResources>().BackToTemplates, Res.Get<PageResources>().PermissionsForAllTemplates, permissionSetName).AddParameters("&managerClassName=" + typeof (PageManager).FullName).Done().AddDialog<TemplateSelectorDialog>("changeTemplate").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetWidth(Unit.Pixel(760)).SetHeight(Unit.Pixel(670)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters("?showEmptyTemplate={0}&notCreateTemplateForMasterPage={1}&rootTaxonType={2}".Arrange((object) "true", (object) true, (object) str)).Done().AddDialog<TemplatePagesDialog>("templatePages").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().BlackList().Done().AddDialog<LockingDialog>("unlockPage").MakeFullScreen().SetParameters(parameters).ReloadOnShow().Done().AddDialog<ShareItemDialog>("shareWith").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters("?itemId={0}&title={1}&resourceClassId={2}&getSharedSitesUrl={3}&setSharedSitesUrl={4}".Arrange((object) "{{Id}}", (object) "ShareTemplateTitle", (object) typeof (MultisiteResources).Name, (object) HttpUtility.UrlEncode(absolute + "sitelinks/{0}/?sortExpression=Name"), (object) HttpUtility.UrlEncode(absolute + "savesitelinks/{0}/"))).SetModuleName("MultisiteInternal").BlackList().Done().AddDialog<PageVersionHistoryDialog>("historygrid").SetParameters("?typeName=" + typeof (PageTemplate).FullName + "&DisableComparisonForVersions=" + (object) true).MakeFullScreen().Done().AddDialog<PageViewVersionDialog>("versionPreview").SetParameters("?moduleName=" + "PageTemplates" + "&typeName=" + typeof (PageTemplate).FullName + "&title=" + Res.Get<PageResources>().Templates + "&IsFromEditor=" + "true" + "&IsTemplate=" + "true").MakeFullScreen().Done();
      PromptDialogElement element3 = new PromptDialogElement((ConfigElement) masterGridViewElement.PromptDialogsConfig)
      {
        DialogName = "templateNotUsedWarningDialog",
        ResourceClassId = PageTemplatesDefinitions.ResourceClassId,
        Message = "PromptMessageTemplateNotUsed",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand1 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element3.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null);
      element3.CommandsConfig.Add(promptDialogCommand1);
      CommandToolboxItemElement promptDialogCommand2 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element3.CommandsConfig, "Cancel", "cancel", CommandType.CancelButton, "LI", (string) null);
      element3.CommandsConfig.Add(promptDialogCommand2);
      masterGridViewElement.PromptDialogsConfig.Add(element3);
      PromptDialogElement element4 = new PromptDialogElement((ConfigElement) masterGridViewElement.PromptDialogsConfig)
      {
        DialogName = "singleTemplateInUseDialog",
        ResourceClassId = PageTemplatesDefinitions.ResourceClassId,
        Message = "PromptMessageSingleTemplateInUse",
        Width = 300,
        Height = 300,
        Title = "YouCannotDeleteATemplateInUseTitle",
        ShowOnLoad = false
      };
      element4.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element4.CommandsConfig));
      masterGridViewElement.PromptDialogsConfig.Add(element4);
      PromptDialogElement element5 = new PromptDialogElement((ConfigElement) masterGridViewElement.PromptDialogsConfig)
      {
        DialogName = "batchDeleteNotAllowedDialog",
        ResourceClassId = PageTemplatesDefinitions.ResourceClassId,
        Message = "PromptMessageBatchDeleteNotAllowed",
        Width = 300,
        Height = 300,
        Title = "YouCannotDeleteATemplateInUseTitle",
        ShowOnLoad = false
      };
      element5.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element5.CommandsConfig));
      masterGridViewElement.PromptDialogsConfig.Add(element5);
      PromptDialogElement element6 = new PromptDialogElement((ConfigElement) masterGridViewElement.PromptDialogsConfig)
      {
        DialogName = "templateChangedDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitleInfo",
        Message = "PromptMessageTemplateChanged",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      element6.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element6.CommandsConfig));
      masterGridViewElement.PromptDialogsConfig.Add(element6);
      PromptDialogElement element7 = new PromptDialogElement((ConfigElement) masterGridViewElement.PromptDialogsConfig)
      {
        DialogName = "templateChangeFailedDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitleWarning",
        Message = "PromptMessageTemplateChangeFailed",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      element7.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element7.CommandsConfig));
      masterGridViewElement.PromptDialogsConfig.Add(element7);
      LinkElement element8 = new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "pages",
        CommandName = "managePages",
        NavigateUrl = isForBackendPages ? "~/Sitefinity/Administration/BackendPages" : "~/Sitefinity/Pages"
      };
      masterGridViewElement.LinksConfig.Add(element8);
      return masterGridViewElement;
    }

    private static DetailFormViewElement DefineDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode,
      string viewName,
      bool isForBackendPage = false,
      bool isDuplicate = false)
    {
      Labels labels = Res.Get<Labels>();
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageTemplatesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isCreateMode ? (isDuplicate ? "DuplicateTemplate" : "CreateTemplate") : "EditTemplate").HideTopToolbar().LocalizeUsing<PageResources>().SetServiceBaseUrl("~/Sitefinity/Services/Pages/PageTemplatesService.svc/").SetExternalClientScripts(extenalClientScripts).DoNotUnlockDetailItemOnExit();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      PromptDialogElement element1 = new PromptDialogElement((ConfigElement) detailFormViewElement.PromptDialogsConfig)
      {
        DialogName = "translationSyncedWarningDialog",
        ResourceClassId = PageTemplatesDefinitions.ResourceClassId,
        Title = "PromptTitleTranslationSyncedWarning",
        Message = "PromptMessageTranslationSyncedWarning",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand1 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element1.CommandsConfig, labels.ContinueCommand, "ok", CommandType.NormalButton, "LI", (string) null);
      element1.CommandsConfig.Add(promptDialogCommand1);
      CommandToolboxItemElement promptDialogCommand2 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element1.CommandsConfig, labels.BackToTemplates, "cancel", CommandType.CancelButton, "LI", (string) null);
      element1.CommandsConfig.Add(promptDialogCommand2);
      detailFormViewElement.PromptDialogsConfig.Add(element1);
      string fieldName = isDuplicate ? "DuplicateTitle" : "Title";
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      definitionFacade2.AddTextField(fieldName).SetId("TemplatesNameFieldControl").SetTitle("Name").SetCssClass("sfTitleField sfShortField250").AddValidation().MakeRequired().SetRequiredViolationMessage("NameCannotBeEmpty").Done();
      definitionFacade2.AddTextField("Name").SetId("TemplatesDevNameFieldControl").SetTitle("DeveloperNameLabel").SetCssClass("sfShortField250").Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade3 = definitionFacade1.AddSection("CopyLanguageSection").SetTitle("TranslationsSectionTitle").SetCssClass("sfExpandableForm").AddExpandableBehavior().Expand().Done();
      ContentViewSectionElement viewSectionElement1 = definitionFacade3.Get();
      definitionFacade3.AddTextField("copySettingsDecriptionField").SetId("copySettingsDecriptionField").SetFieldType<DescriptionField>().SetDescription("CopyWidgetSettingsDescription").SetDisplayMode(FieldDisplayMode.Read).Done();
      LanguageChoiceFieldElement choiceFieldElement = new LanguageChoiceFieldElement((ConfigElement) viewSectionElement1.Fields);
      choiceFieldElement.ID = "languageCopyChoiceField";
      choiceFieldElement.FieldType = typeof (LanguageChoiceField);
      choiceFieldElement.ResourceClassId = typeof (PageResources).Name;
      choiceFieldElement.Title = "CopyWidgetSettings";
      choiceFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement.FieldName = "languageCopyField";
      choiceFieldElement.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement.MutuallyExclusive = true;
      choiceFieldElement.LanguageSource = new LanguageSource?(isForBackendPage ? LanguageSource.Backend : LanguageSource.Frontend);
      choiceFieldElement.HideIfSingleLanguage = new bool?(false);
      choiceFieldElement.LanguagesToShow = new LanguagesSelection?(LanguagesSelection.Available);
      choiceFieldElement.DataFieldName = "AvailableLanguages";
      LanguageChoiceFieldElement element2 = choiceFieldElement;
      element2.ChoicesConfig.Add(new ChoiceElement((ConfigElement) element2.ChoicesConfig)
      {
        Text = "ShowInNavigation",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Value = ""
      });
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element2);
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddSection("ThumbnailSection").Get();
      RelatedMediaFieldDefinitionElement definitionElement = new RelatedMediaFieldDefinitionElement((ConfigElement) viewSectionElement2.Fields);
      definitionElement.ID = "templateThumbnailField";
      definitionElement.DataFieldName = PageTemplate.ThumbnailFieldName;
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement.Title = "TemplateThumbnailFieldTitle";
      definitionElement.Description = "TemplateThumbnailRecommendedImageSize";
      definitionElement.CssClass = "sfFormSeparator sfHideActions sfTemplateTmb";
      definitionElement.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.SingleImage);
      definitionElement.ResourceClassId = "PageTemplateResources";
      definitionElement.RelatedDataProvider = "SystemLibrariesProvider";
      definitionElement.TargetLibraryId = new Guid?(LibrariesModule.DefaultTemplateThumbnailsLibraryId);
      definitionElement.SourceLibraryId = new Guid?(LibrariesModule.DefaultTemplateThumbnailsLibraryId);
      definitionElement.SelectButtonText = PageTemplatesDefinitions.GetSelectButtonLabel();
      definitionElement.SelectorCssClass = "sfTemplateTmbSelector";
      definitionElement.SelectorOpenMode = new MediaSelectorOpenMode?(MediaSelectorOpenMode.Select);
      definitionElement.PreSelectItemInSelector = true;
      RelatedMediaFieldDefinitionElement element3 = definitionElement;
      element3.ValidatorConfig.MessageCssClass = "sfError";
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element3);
      if (isCreateMode)
      {
        ContentViewSectionElement viewSectionElement3 = definitionFacade1.AddSection("TemplateSection").Get();
        PageTemplateFieldDefinitionElement element4 = new PageTemplateFieldDefinitionElement((ConfigElement) viewSectionElement3.Fields);
        element4.ID = "PageTemplateField";
        element4.DataFieldName = "Template";
        element4.Title = "TemplateYouCanChangeItLater";
        element4.ResourceClassId = typeof (PageResources).Name;
        element4.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
        element4.WrapperTag = HtmlTextWriterTag.Li;
        element4.ShouldNotCreateTemplateForMasterPage = true;
        element4.IsBackendTemplate = isForBackendPage;
        element4.ShowEmptyTemplate = true;
        element4.ShowAllBasicTemplates = true;
        viewSectionElement3.Fields.Add((FieldDefinitionElement) element4);
      }
      WidgetBarSectionElement element5 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element5.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.Items);
      element6.Name = "SaveChangesWidgetElement";
      element6.ButtonType = CommandButtonType.Save;
      element6.CommandName = isCreateMode ? "create" : "save";
      element6.Text = isCreateMode ? "CreateAndAddContent" : "SaveChanges";
      element6.ResourceClassId = PageTemplatesDefinitions.ResourceClassId;
      element6.WrapperTagKey = HtmlTextWriterTag.Span;
      element6.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element6);
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items2 = element5.Items;
        CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.Items);
        element7.Name = "CreateAndReturnWidgetElement";
        element7.ButtonType = CommandButtonType.Standard;
        element7.CommandName = "save";
        element7.Text = "CreateAndReturnToTemplates";
        element7.ResourceClassId = PagesDefinitions.ResourceClassId;
        element7.WrapperTagKey = HtmlTextWriterTag.Span;
        element7.WidgetType = typeof (CommandWidget);
        items2.Add((WidgetElement) element7);
      }
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element5.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (PageResources).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element8 = menuWidgetElement;
        ConfigElementList<WidgetElement> menuItems = element8.MenuItems;
        CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element8.MenuItems);
        element9.Name = "PermissionsCommandName";
        element9.ButtonType = CommandButtonType.SimpleLinkButton;
        element9.Text = "SetPermissions";
        element9.CommandName = "permissions";
        element9.ResourceClassId = typeof (PageResources).Name;
        element9.WidgetType = typeof (CommandWidget);
        menuItems.Add((WidgetElement) element9);
        element5.Items.Add((WidgetElement) element8);
      }
      ConfigElementList<WidgetElement> items3 = element5.Items;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element5.Items);
      element10.Name = "CancelWidgetElement";
      element10.ButtonType = CommandButtonType.Cancel;
      element10.CommandName = "cancel";
      element10.Text = "BackToTemplates";
      element10.ResourceClassId = typeof (PageResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Span;
      element10.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element10);
      detailFormViewElement.Toolbar.Sections.Add(element5);
      return detailFormViewElement;
    }

    private static void AddActionsMenuColumn(GridViewModeElement parent)
    {
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) parent.ColumnsConfig);
      menuColumnElement.Name = "ActionsLinkText";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.TitleText = "ActionsLinkText";
      menuColumnElement.ResourceClassId = typeof (PageResources).Name;
      ActionMenuColumnElement element1 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element1.MenuItems;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element2.Name = "Delete";
      element2.WrapperTagKey = HtmlTextWriterTag.Li;
      element2.CommandName = "deleteTemplate";
      element2.Text = "Delete";
      element2.ResourceClassId = typeof (Labels).Name;
      element2.WidgetType = typeof (CommandWidget);
      element2.CssClass = "sfDeleteItm";
      menuItems1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> menuItems2 = element1.MenuItems;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element3.Name = "Publish";
      element3.WrapperTagKey = HtmlTextWriterTag.Li;
      element3.CommandName = "publishDraft";
      element3.Text = "Publish";
      element3.ResourceClassId = typeof (Labels).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfPublishItm";
      menuItems2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> menuItems3 = element1.MenuItems;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element4.Name = "Unlock";
      element4.WrapperTagKey = HtmlTextWriterTag.Li;
      element4.CommandName = "unlockPage";
      element4.Text = "Unlock";
      element4.ResourceClassId = typeof (Labels).Name;
      element4.WidgetType = typeof (CommandWidget);
      menuItems3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> menuItems4 = element1.MenuItems;
      MultisiteCommandWidgetElement element5 = new MultisiteCommandWidgetElement((ConfigElement) element1.MenuItems);
      element5.Name = "ShareWith";
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.CommandName = "shareWith";
      element5.Text = "ShareWith";
      element5.ResourceClassId = typeof (Labels).Name;
      element5.WidgetType = typeof (CommandWidget);
      element5.ModuleName = "MultisiteInternal";
      menuItems4.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems5 = element1.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element6.Name = "Duplicate";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "duplicate";
      element6.Text = "Duplicate";
      element6.ResourceClassId = typeof (Labels).Name;
      element6.WidgetType = typeof (CommandWidget);
      menuItems5.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems6 = element1.MenuItems;
      LiteralWidgetElement element7 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element7.Name = "Separator";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.Text = "Edit";
      element7.IsSeparator = true;
      element7.CssClass = "sfSeparator";
      menuItems6.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems7 = element1.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element8.Name = "Properties";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "edit";
      element8.Text = "ViewProperties";
      element8.ResourceClassId = typeof (PageResources).Name;
      element8.WidgetType = typeof (CommandWidget);
      menuItems7.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems8 = element1.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element9.Name = "Content";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "editPageContent";
      element9.Text = "Content";
      element9.ResourceClassId = typeof (Labels).Name;
      element9.WidgetType = typeof (CommandWidget);
      menuItems8.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems9 = element1.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element10.Name = "Permissions";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "permissions";
      element10.Text = "Permissions";
      element10.ResourceClassId = typeof (Labels).Name;
      element10.WidgetType = typeof (CommandWidget);
      menuItems9.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems10 = element1.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element11.Name = "BaseTemplate";
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.CommandName = "changeTemplate";
      element11.Text = "BaseTemplate";
      element11.ResourceClassId = typeof (PageResources).Name;
      element11.WidgetType = typeof (CommandWidget);
      menuItems10.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems11 = element1.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element12.Name = "RevisionHistory";
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.CommandName = "historygrid";
      element12.Text = "RevisionHistory";
      element12.ResourceClassId = typeof (Labels).Name;
      element12.WidgetType = typeof (CommandWidget);
      menuItems11.Add((WidgetElement) element12);
      parent.ColumnsConfig.Add((ColumnElement) element1);
    }

    private static void AddBasedOnColumn(GridViewModeElement parent)
    {
      string str = "<span class=\"sfBasedOn\">{0}</span><a href='javascript:void(0);' sys:class=\"sfParentLnk\"></a><div class=\"sfPackage\"><span class=\"sfPackageName\"></span><a href='javascript:void(0);' sys:class=\"sfFullPath\"></a><span class=\"sfFileName\"></span></div>".Arrange((object) Res.Get<PageResources>().NotBasedOnOtherTemplate);
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "BasedOn";
      dataColumnElement.HeaderText = "BasedOnGridColumnHeaderText";
      dataColumnElement.ResourceClassId = typeof (PageResources).Name;
      dataColumnElement.ClientTemplate = str;
      dataColumnElement.HeaderCssClass = "sfTemplate";
      dataColumnElement.ItemCssClass = "sfTemplate";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static void AddPagesCountColumn(GridViewModeElement parent)
    {
      string str = string.Format("<a href='javascript:void(0);' sys:class='sf_binderCommand_templatePages' title='{0}'>{1}</a>", (object) Res.Get<PageResources>().ClickToViewWhichPagesUseThisTemplate, (object) "{{(PagesCountString)}}");
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "PagesCount";
      dataColumnElement.HeaderText = string.Empty;
      dataColumnElement.ClientTemplate = str;
      dataColumnElement.HeaderCssClass = "sfPageCount";
      dataColumnElement.ItemCssClass = "sfPageCount";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static void AddDateOwnerColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "DateOwner";
      dataColumnElement.HeaderText = "DateOwner";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.ClientTemplate = "<span>{{ (DateCreated) ? DateCreated.sitefinityLocaleFormat('dd MMM, yyyy hh:mm') : '-' }}</span>\r\n                                   <span class='sfLine'>{{Owner ? Owner.htmlEncode() : ''}}</span>";
      dataColumnElement.HeaderCssClass = "sfDateAuthor";
      dataColumnElement.ItemCssClass = "sfDateAuthor";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static string GetSelectButtonLabel() => Res.Get<PageTemplateResources>().ChangeLabel;
  }
}
