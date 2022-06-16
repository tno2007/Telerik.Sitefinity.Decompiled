// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PagesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config;
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
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Enums;
using Telerik.Sitefinity.Workflow;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// This static class provides methods that define the ContentView controls and views used by the pages module.
  /// </summary>
  public static class PagesDefinitions
  {
    /// <summary>
    /// Name of the definition for configuring list of frontend pages
    /// </summary>
    public const string FrontendPagesDefinitionName = "FrontendPages";
    /// <summary>
    /// Name of the definition for configuring list of backend pages
    /// </summary>
    public const string BackendPagesDefinitionName = "BackendPages";
    /// <summary>
    /// Name of the view used at listview mode with frontend pages
    /// </summary>
    public const string FrontendPagesListViewName = "FrontendPagesListView";
    /// <summary>
    /// Name of the view used as listview mode with backend pages
    /// </summary>
    public const string BackendPagesListViewName = "BackendPagesListView";
    /// <summary>
    /// Name of the view used to edit page properties for frontend pages
    /// </summary>
    public const string FrontendPagesEditViewName = "FrontendPagesEdit";
    /// <summary>
    /// Name of the view used to edit page properties for backend pages
    /// </summary>
    public const string BackendPagesEditViewName = "BackendPagesEdit";
    /// <summary>
    /// Name of the view used to create a page in the frontend.
    /// </summary>
    public const string FrontendPagesCreateViewName = "FrontendPagesCreate";
    /// <summary>
    /// Name of the view used to create a page in the backend.
    /// </summary>
    public const string BackendPagesCreateViewName = "BackendPagesCreate";
    /// <summary>
    /// Name of the view used to duplicate a page in the backend.
    /// </summary>
    public const string BackendPagesDuplicateViewName = "BackendPagesDuplicate";
    /// <summary>
    /// Name of the view used to duplicate a page in the frontend.
    /// </summary>
    public const string FrontendPagesDuplicateViewName = "FrontendPagesDuplicate";
    /// <summary>The resource class id id for pages.</summary>
    public static readonly string ResourceClassId = typeof (PageResources).Name;
    /// <summary>The workflow resources class id.</summary>
    public static readonly string WorklfowResourcesClassId = typeof (WorkflowResources).Name;
    /// <summary>The name of the module;</summary>
    public const string ModuleName = "Pages";

    /// <summary>
    /// Defines the ContentView control for the view listing frontend pages
    /// </summary>
    /// <param name="config">The configuration.</param>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFrontendPagesContentView(
      ContentViewConfig config,
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "FrontendPages", typeof (PageNode));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy("FrontendPagesListView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineMasterGridView(fluentContentView, config, "FrontendPagesListView", false)));
      viewControlElement.ViewsConfig.AddLazy("FrontendPagesCreate", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, true, "FrontendPagesCreate", false, false)));
      viewControlElement.ViewsConfig.AddLazy("FrontendPagesEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, false, "FrontendPagesEdit", false, false)));
      viewControlElement.ViewsConfig.AddLazy("FrontendPagesDuplicate", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, true, "FrontendPagesDuplicate", false, true)));
      PagesDefinitions.CreateDialogs(fluentContentView);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control for the view listing backend pages.
    /// </summary>
    /// <param name="config">The config.</param>
    /// <param name="parent">The parent.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineBackendPagesContentView(
      ContentViewConfig config,
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "BackendPages", typeof (PageNode));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy("BackendPagesListView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineMasterGridView(fluentContentView, config, "BackendPagesListView", true)));
      viewControlElement.ViewsConfig.AddLazy("BackendPagesCreate", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, true, "BackendPagesCreate", true, false, SiteInitializer.SitefinityNodeId)));
      viewControlElement.ViewsConfig.AddLazy("BackendPagesEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, false, "BackendPagesEdit", true, false, SiteInitializer.SitefinityNodeId)));
      viewControlElement.ViewsConfig.AddLazy("BackendPagesDuplicate", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) PagesDefinitions.DefineDetailsView(fluentContentView, true, "BackendPagesDuplicate", true, true, SiteInitializer.SitefinityNodeId)));
      return viewControlElement;
    }

    private static void CreateDialogs(ContentViewControlDefinitionFacade fluentFacade) => fluentFacade.AddDialog<PropertyEditor>("editControl").SetParameters("?PageId=" + (object) Guid.Empty).MakeFullScreen().ReloadOnShow();

    internal static void DefineToolbar(
      MasterGridViewElement pagesBackendMaster,
      ContentViewConfig config)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "CreatePageWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreatePage";
      commandWidgetElement1.ResourceClassId = typeof (PageResources).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.PermissionSet = "Pages";
      commandWidgetElement1.ActionName = "Create";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "CreateChildWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Create;
      commandWidgetElement2.CommandName = "createChild";
      commandWidgetElement2.Text = "CreateChildOfTheSelectedPage";
      commandWidgetElement2.ResourceClassId = typeof (PageResources).Name;
      commandWidgetElement2.CssClass = "sfMainAction";
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.PermissionSet = "Pages";
      commandWidgetElement2.ActionName = "Create";
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement3.Name = "DeletePageWidget";
      commandWidgetElement3.ButtonType = CommandButtonType.Standard;
      commandWidgetElement3.CommandName = "groupDelete";
      commandWidgetElement3.Text = "Delete";
      commandWidgetElement3.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement3;
      element1.Items.Add((WidgetElement) element4);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "MoreActionsPagesWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (PageResources).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element5 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "Publish";
      element6.Text = "Publish";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "batchPublishPage";
      element6.WidgetType = typeof (CommandWidget);
      element6.ResourceClassId = typeof (Labels).Name;
      element6.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems2 = element5.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element7.Name = "Unpublish";
      element7.Text = "Unpublish";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "batchUnpublishPage";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (Labels).Name;
      element7.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems3 = element5.MenuItems;
      LiteralWidgetElement element8 = new LiteralWidgetElement((ConfigElement) element5.MenuItems);
      element8.Name = "ChangeSeparator";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CssClass = "sfSeparator sfFirst";
      element8.Text = "Change";
      element8.ResourceClassId = typeof (Labels).Name;
      element8.WidgetType = typeof (LiteralWidget);
      element8.IsSeparator = true;
      menuItems3.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems4 = element5.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element9.Name = "Template";
      element9.Text = "Template";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "changeTemplate";
      element9.WidgetType = typeof (CommandWidget);
      element9.ResourceClassId = typeof (PageResources).Name;
      menuItems4.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems5 = element5.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element10.Name = "BulkTemplate";
      element10.Text = "Template";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "batchChangeTemplate";
      element10.WidgetType = typeof (CommandWidget);
      element10.ResourceClassId = typeof (PageResources).Name;
      menuItems5.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems6 = element5.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element11.Name = "Owner";
      element11.Text = "Owner";
      element11.RequiredPermissionSet = "Backend";
      element11.RequiredActions = "ManageUsers";
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.CommandName = "batchChangeOwner";
      element11.WidgetType = typeof (CommandWidget);
      element11.ResourceClassId = typeof (Labels).Name;
      menuItems6.Add((WidgetElement) element11);
      element1.Items.Add((WidgetElement) element5);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (PageNode), "searchGrid", "closeSearchGrid"));
      IEnumerable<SortingExpressionElement> expressionElements1 = config.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == typeof (PageData).FullName && !s.IsCustom));
      IEnumerable<SortingExpressionElement> expressionElements2 = config.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == typeof (PageData).FullName && s.IsCustom));
      DynamicCommandWidgetElement commandWidgetElement4 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement4.Name = "PageSortingWidget";
      commandWidgetElement4.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement4.HeaderText = "SortPages";
      commandWidgetElement4.PageSize = 10;
      commandWidgetElement4.MoreLinkText = "More";
      commandWidgetElement4.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement4.WidgetType = typeof (SortWidget);
      commandWidgetElement4.ResourceClassId = PagesDefinitions.ResourceClassId;
      commandWidgetElement4.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement4.ContentType = typeof (PageData);
      DynamicCommandWidgetElement element12 = commandWidgetElement4;
      element1.Items.Add((WidgetElement) element12);
      foreach (SortingExpressionElement expressionElement in expressionElements1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element12.Items, Res.ResolveLocalizedValue(expressionElement.ResourceClassId, expressionElement.SortingExpressionTitle), expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element12.Items.Add(dynamicItemElement);
      }
      foreach (SortingExpressionElement expressionElement in expressionElements2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element12.CustomItems, Res.ResolveLocalizedValue(expressionElement.ResourceClassId, expressionElement.SortingExpressionTitle), expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element12.CustomItems.Add(dynamicItemElement);
      }
      pagesBackendMaster.ToolbarConfig.Sections.Add(element1);
    }

    internal static void DefineSidebar(MasterGridViewElement pagesBackendMaster, bool isBackend)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "Filter",
        WrapperTagId = "FilterPagesSection",
        Title = "FilterPages",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "AllPages";
      element2.CommandName = "showAllPages";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = "AllPages";
      element2.ResourceClassId = typeof (PageResources).Name;
      element2.CssClass = string.Empty;
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      element2.ButtonCssClass = "sfSel";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "MyPages";
      element3.CommandName = "showMyPages";
      element3.ButtonType = CommandButtonType.SimpleLinkButton;
      element3.Text = "MyPages";
      element3.ResourceClassId = typeof (PageResources).Name;
      element3.CssClass = string.Empty;
      element3.WidgetType = typeof (CommandWidget);
      element3.IsSeparator = false;
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "AwaitingMyActionPages";
      element4.CommandName = "showAwaitingMyActionPages";
      element4.ButtonType = CommandButtonType.SimpleLinkButton;
      element4.Text = "AwaitingMyAction";
      element4.ResourceClassId = typeof (PageResources).Name;
      element4.CssClass = string.Empty;
      element4.WidgetType = typeof (CommandWidget);
      element4.IsSeparator = false;
      items3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items4 = element1.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.Items);
      element5.Name = "Published";
      element5.CommandName = "showPublishedPages";
      element5.ButtonType = CommandButtonType.SimpleLinkButton;
      element5.Text = "Published";
      element5.ResourceClassId = typeof (Labels).Name;
      element5.CssClass = string.Empty;
      element5.WidgetType = typeof (CommandWidget);
      element5.IsSeparator = false;
      items4.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> items5 = element1.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element1.Items);
      element6.Name = "PendingApprovalPages";
      element6.CommandName = "showPendingApprovalPages";
      element6.ButtonType = CommandButtonType.SimpleLinkButton;
      element6.Text = "WaitingForApproval";
      element6.ResourceClassId = typeof (PageResources).Name;
      element6.CssClass = string.Empty;
      element6.WidgetType = typeof (CommandWidget);
      element6.IsSeparator = false;
      items5.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> items6 = element1.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element1.Items);
      element7.Name = "PendingReviewPages";
      element7.CommandName = "showPendingReviewPages";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.Text = "AwaitingReview";
      element7.ResourceClassId = typeof (PageResources).Name;
      element7.CssClass = string.Empty;
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      items6.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> items7 = element1.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element1.Items);
      element8.Name = "PendingPublishingPages";
      element8.CommandName = "showPendingPublishingPages";
      element8.ButtonType = CommandButtonType.SimpleLinkButton;
      element8.Text = "AwaitingPublishing";
      element8.ResourceClassId = typeof (PageResources).Name;
      element8.CssClass = string.Empty;
      element8.WidgetType = typeof (CommandWidget);
      element8.IsSeparator = false;
      items7.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> items8 = element1.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element1.Items);
      element9.Name = "RejectedPages";
      element9.CommandName = "showRejectedPages";
      element9.ButtonType = CommandButtonType.SimpleLinkButton;
      element9.Text = "Rejected";
      element9.ResourceClassId = typeof (Labels).Name;
      element9.CssClass = string.Empty;
      element9.WidgetType = typeof (CommandWidget);
      element9.IsSeparator = false;
      items8.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> items9 = element1.Items;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element1.Items);
      element10.Name = "Drafts";
      element10.CommandName = "showDraftPages";
      element10.ButtonType = CommandButtonType.SimpleLinkButton;
      element10.Text = "Drafts";
      element10.ResourceClassId = typeof (Labels).Name;
      element10.CssClass = string.Empty;
      element10.WidgetType = typeof (CommandWidget);
      element10.IsSeparator = false;
      items9.Add((WidgetElement) element10);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element1.Items);
      ConfigElementList<WidgetElement> items10 = element1.Items;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element1.Items);
      element11.Name = "WithNoDescriptions";
      element11.CommandName = "showPagesWithNoDescriptions";
      element11.ButtonType = CommandButtonType.SimpleLinkButton;
      element11.Text = "WithNoDescriptions";
      element11.ResourceClassId = typeof (PageResources).Name;
      element11.CssClass = string.Empty;
      element11.WidgetType = typeof (CommandWidget);
      element11.IsSeparator = false;
      items10.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> items11 = element1.Items;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element1.Items);
      element12.Name = "WithNoKeywords";
      element12.CommandName = "showPagesWithNoKeywords";
      element12.ButtonType = CommandButtonType.SimpleLinkButton;
      element12.Text = "WithNoKeywords";
      element12.ResourceClassId = typeof (PageResources).Name;
      element12.CssClass = string.Empty;
      element12.WidgetType = typeof (CommandWidget);
      element12.IsSeparator = false;
      items11.Add((WidgetElement) element12);
      WidgetBarSectionElement element13 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "ByDate",
        Title = "PagesByDate",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      pagesBackendMaster.SidebarConfig.Sections.Add(element13);
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element13.Items);
      commandWidgetElement1.Name = "CloseDateFilter";
      commandWidgetElement1.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement1.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element13.WrapperTagId);
      commandWidgetElement1.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement1.Text = "CloseDateFilter";
      commandWidgetElement1.ResourceClassId = PagesDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfCloseFilter";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.IsSeparator = false;
      CommandWidgetElement element14 = commandWidgetElement1;
      element13.Items.Add((WidgetElement) element14);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element13.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.CommandName = "customDateFilterCommand";
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element15 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element15.PredefinedFilteringRanges);
      element13.Items.Add((WidgetElement) element15);
      ConfigElementList<WidgetElement> items12 = element1.Items;
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "FilterByDate";
      commandWidgetElement2.CommandName = "hideSectionsExcept";
      commandWidgetElement2.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element13.WrapperTagId);
      commandWidgetElement2.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement2.Text = "ByDate";
      commandWidgetElement2.ResourceClassId = PagesDefinitions.ResourceClassId;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.IsSeparator = false;
      CommandWidgetElement element16 = commandWidgetElement2;
      items12.Add((WidgetElement) element16);
      DefinitionsHelper.AddLanguageSection(pagesBackendMaster, isBackend);
      pagesBackendMaster.SidebarConfig.Sections.Add(element1);
      pagesBackendMaster.SidebarConfig.ResourceClassId = typeof (PageResources).Name;
      pagesBackendMaster.SidebarConfig.Title = "ManagePages";
      WidgetBarSectionElement element17 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        WrapperTagId = "ManageAlsoSection",
        Title = "ManageAlso",
        ResourceClassId = typeof (Labels).Name,
        CssClass = "sfWidgetsList sfSeparator"
      };
      ConfigElementList<WidgetElement> items13 = element17.Items;
      CommandWidgetElement element18 = new CommandWidgetElement((ConfigElement) element17.Items);
      element18.Name = "ManageTemplates";
      element18.CommandName = "manageTemplates";
      element18.ButtonType = CommandButtonType.SimpleLinkButton;
      element18.Text = "Templates";
      element18.ResourceClassId = typeof (PageResources).Name;
      element18.CssClass = string.Empty;
      element18.WidgetType = typeof (CommandWidget);
      element18.IsSeparator = false;
      element18.PermissionSet = "Pages";
      element18.ActionName = "View";
      element18.RelatedSecuredObjectId = SiteInitializer.PageTemplatesNodeId.ToString();
      element18.RelatedSecuredObjectTypeName = typeof (PageNode).FullName;
      items13.Add((WidgetElement) element18);
      pagesBackendMaster.SidebarConfig.Sections.Add(element17);
      WidgetBarSectionElement element19 = new WidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections)
      {
        Name = "Settings",
        WrapperTagId = "SettingsForPages",
        Title = "SettingsForPages",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfWidgetsList sfSettings sfSeparator"
      };
      ConfigElementList<WidgetElement> items14 = element19.Items;
      CommandWidgetElement element20 = new CommandWidgetElement((ConfigElement) element19.Items);
      element20.Name = "PermissionsForAllPages";
      element20.CommandName = "permissions";
      element20.ButtonType = CommandButtonType.SimpleLinkButton;
      element20.Text = "PermissionsForAllPages";
      element20.ResourceClassId = typeof (PageResources).Name;
      element20.CssClass = string.Empty;
      element20.WidgetType = typeof (CommandWidget);
      element20.IsSeparator = false;
      items14.Add((WidgetElement) element20);
      ConfigElementList<WidgetElement> items15 = element19.Items;
      CommandWidgetElement element21 = new CommandWidgetElement((ConfigElement) element19.Items);
      element21.Name = "CustomFields";
      element21.CommandName = "moduleEditor";
      element21.ButtonType = CommandButtonType.SimpleLinkButton;
      element21.Text = "CustomFields";
      element21.ResourceClassId = typeof (Labels).Name;
      element21.CssClass = string.Empty;
      element21.WidgetType = typeof (CommandWidget);
      element21.IsSeparator = false;
      items15.Add((WidgetElement) element21);
      pagesBackendMaster.SidebarConfig.Sections.Add(element19);
      DefinitionsHelper.CreateRecycleBinLink(pagesBackendMaster.SidebarConfig.Sections, typeof (PageNode).FullName);
    }

    private static MasterGridViewElement DefineMasterGridView(
      ContentViewControlDefinitionFacade fluentContentView,
      ContentViewConfig config,
      string viewName,
      bool isForBackendPages)
    {
      Type type = isForBackendPages ? typeof (BackendPagesMasterGridView) : typeof (FrontendPagesMasterGridView);
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).SetViewType(type).LocalizeUsing<PageResources>().DisableSorting().SetTitle("Pages").SetCssClass("sfPagesTreeview").SetServiceBaseUrl("~/Sitefinity/Services/Pages/PagesService.svc/").SetTemplateEvaluationMode(TemplateEvalutionMode.Client);
      MasterGridViewElement pagesBackendMaster = definitionFacade.Get();
      PagesDefinitions.DefineToolbar(pagesBackendMaster, config);
      PagesDefinitions.DefineSidebar(pagesBackendMaster, isForBackendPages);
      LocalizationWidgetBarSectionElement barSectionElement = new LocalizationWidgetBarSectionElement((ConfigElement) pagesBackendMaster.ContextBarConfig.Sections);
      barSectionElement.Name = "contextBar";
      barSectionElement.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement.CssClass = "sfContextWidgetWrp";
      barSectionElement.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element1 = barSectionElement;
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "ShowMoreTranslations";
      element2.CommandName = "showMoreTranslations";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = "ShowAllTranslations";
      element2.ResourceClassId = typeof (LocalizationResources).Name;
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      element2.CssClass = "sfShowHideLangVersions";
      element2.WrapperTagKey = HtmlTextWriterTag.Div;
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "HideMoreTranslations";
      element3.CommandName = "hideMoreTranslations";
      element3.ButtonType = CommandButtonType.SimpleLinkButton;
      element3.Text = "ShowBasicTranslationsOnly";
      element3.ResourceClassId = typeof (LocalizationResources).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.IsSeparator = false;
      element3.CssClass = "sfDisplayNone sfShowHideLangVersions";
      element3.WrapperTagKey = HtmlTextWriterTag.Div;
      items2.Add((WidgetElement) element3);
      pagesBackendMaster.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element1);
      pagesBackendMaster.ViewModesConfig.AddLazy("TreeTable", (Func<ViewModeElement>) (() => (ViewModeElement) PagesDefinitions.DefinePagesTreeTable(pagesBackendMaster, isForBackendPages)));
      pagesBackendMaster.ViewModesConfig.AddLazy("Grid", (Func<ViewModeElement>) (() => (ViewModeElement) PagesDefinitions.DefinePagesGridView(pagesBackendMaster, isForBackendPages)));
      DecisionScreenElement element4 = new DecisionScreenElement((ConfigElement) pagesBackendMaster.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "NoPagesHaveBeenCreatedYet",
        MessageText = "NoPagesHaveBeenCreatedYet",
        ResourceClassId = typeof (PageResources).Name
      };
      ConfigElementList<CommandWidgetElement> actions = element4.Actions;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.Actions);
      element5.Name = "Create";
      element5.ButtonType = CommandButtonType.Create;
      element5.CommandName = "create";
      element5.Text = "CreatePage";
      element5.ResourceClassId = typeof (PageResources).Name;
      element5.CssClass = "sfCreateItem";
      actions.Add(element5);
      pagesBackendMaster.DecisionScreensConfig.Add(element4);
      string viewName1 = isForBackendPages ? "BackendPagesCreate" : "FrontendPagesCreate";
      string viewName2 = isForBackendPages ? "BackendPagesEdit" : "FrontendPagesEdit";
      string viewName3 = isForBackendPages ? "BackendPagesDuplicate" : "FrontendPagesDuplicate";
      Guid guid = isForBackendPages ? SiteInitializer.BackendRootNodeId : SiteInitializer.FrontendRootNodeId;
      string str = isForBackendPages ? RootTaxonType.Backend.ToString() : RootTaxonType.Frontend.ToString();
      string parameters = "?ShowUnlock=true&LockedByUsername={{LockedByUsername}}&ItemName={{Title.Value}}&Title=Pages&ViewUrl={{PageViewUrl}}/Action/Preview&UnlockServiceUrl=" + RouteHelper.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/Page/UnlockPage/{{PageDataId}}", UrlResolveOptions.Rooted);
      definitionFacade.AddInsertDialog(viewName1).AddParameters("&SuppressBackToButtonLabelModify=true").Done().AddEditDialog(viewName2).AddParameters("&SuppressBackToButtonLabelModify=true").Done().AddInsertDialog(viewName1, string.Empty, string.Empty, "{{Id}}", (Type) null, "createChild").Done().AddDialog<ChangePageOwnerDialog>("changePageOwner").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().Done().AddDialog<ChangePageOwnerDialog>("batchChangeOwner").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().Done().AddPermissionsDialog(Res.Get<PageResources>().BackToAllPages, Res.Get<PageResources>().PermissionsForAllPages).AddParameters("&managerClassName=" + typeof (PageManager).FullName + "&securedObjectId=" + (object) guid).Done().AddDialog<TemplateSelectorDialog>("changeTemplate").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(760)).SetHeight(Unit.Pixel(670)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters("?rootTaxonType={0}&showEmptyTemplate={1}".Arrange((object) str, (object) "true")).Done().AddDialog<PageVersionHistoryDialog>("historygrid").SetParameters("?typeName=" + typeof (PageData).FullName + "&DisableComparisonForVersions=" + (object) true).MakeFullScreen().Done().AddDialog<PageViewVersionDialog>("versionPreview").SetParameters("?moduleName=" + "Pages" + "&typeName=" + typeof (PageData).FullName + "&title=" + Res.Get<PageResources>().Templates + "&IsFromEditor=" + "true" + "&IsTemplate=" + "false").MakeFullScreen().Done().AddDialog<ShareLinkDialog>("shareLink").SetParameters("?pageId={{Id}}").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().Done().AddDialog<TemplateSelectorDialog>("batchChangeTemplate").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(760)).SetHeight(Unit.Pixel(670)).DisplayTitleBar().MakeModal().SetParameters("?rootTaxonType={0}&showEmptyTemplate={1}".Arrange((object) str, (object) "true")).Done().AddDialog<LockingDialog>("unlockPage").MakeFullScreen().SetParameters(parameters).ReloadOnShow().Done().AddInsertDialog(viewName3, string.Empty, string.Empty, string.Empty, (Type) null, "duplicate").AddParameters("&SuppressBackToButtonLabelModify=true").Done().AddCustomFieldsDialog(Res.Get<PageResources>().BackToAllPages, Res.Get<PageResources>().PagesDataFields, Res.Get<PageResources>().Pages, typeof (PageNode)).Done();
      PromptDialogElement element6 = new PromptDialogElement((ConfigElement) pagesBackendMaster.PromptDialogsConfig)
      {
        DialogName = "changeTemplateWarningDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitleWarning",
        Message = "PromptMessagePageChangeTemplate",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand1 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element6.CommandsConfig, "Continue", "ok", CommandType.NormalButton, "LI", (string) null);
      element6.CommandsConfig.Add(promptDialogCommand1);
      CommandToolboxItemElement promptDialogCommand2 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element6.CommandsConfig, "Cancel", "cancel", CommandType.CancelButton, "LI", (string) null);
      element6.CommandsConfig.Add(promptDialogCommand2);
      pagesBackendMaster.PromptDialogsConfig.Add(element6);
      PromptDialogElement element7 = new PromptDialogElement((ConfigElement) pagesBackendMaster.PromptDialogsConfig)
      {
        DialogName = "switchToNewUiPageDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PageCannotBeEdited",
        Message = "PromptMessageSwitchToNewUI",
        Width = 400,
        Height = 200,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand3 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element7.CommandsConfig, "Close", "cancel", CommandType.NormalButton, "LI", (string) null);
      element7.CommandsConfig.Add(promptDialogCommand3);
      pagesBackendMaster.PromptDialogsConfig.Add(element7);
      PromptDialogElement element8 = new PromptDialogElement((ConfigElement) pagesBackendMaster.PromptDialogsConfig)
      {
        DialogName = "changeTemplatesWarningDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitleWarning",
        Message = "PromptMessagePageChangeTemplates",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand4 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element8.CommandsConfig, "Continue", "ok", CommandType.NormalButton, "LI", (string) null);
      element8.CommandsConfig.Add(promptDialogCommand4);
      CommandToolboxItemElement promptDialogCommand5 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element8.CommandsConfig, "Cancel", "cancel", CommandType.CancelButton, "LI", (string) null);
      element8.CommandsConfig.Add(promptDialogCommand5);
      pagesBackendMaster.PromptDialogsConfig.Add(element8);
      PromptDialogElement parentPageDialog = PagesDefinitions.GetCannotDeleteParentPageDialog(pagesBackendMaster.PromptDialogsConfig);
      pagesBackendMaster.PromptDialogsConfig.Add(parentPageDialog);
      PromptDialogElement element9 = new PromptDialogElement((ConfigElement) pagesBackendMaster.PromptDialogsConfig)
      {
        DialogName = "cannotCreateChildPageDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitlePageCannotCreateChildPage",
        Message = "PromptMessagePageCannotCreateChildPage",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand6 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element9.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null);
      element9.CommandsConfig.Add(promptDialogCommand6);
      pagesBackendMaster.PromptDialogsConfig.Add(element9);
      PromptDialogElement deleteHomePageDialog = PagesDefinitions.GetCannotDeleteHomePageDialog(pagesBackendMaster.PromptDialogsConfig);
      pagesBackendMaster.PromptDialogsConfig.Add(deleteHomePageDialog);
      PromptDialogElement element10 = new PromptDialogElement((ConfigElement) pagesBackendMaster.PromptDialogsConfig)
      {
        DialogName = "somePagesWereNotDeletedDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitleSomePagesWereNotDeleted",
        Message = "PromptMessageSomePagesWereNotDeleted",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      CommandToolboxItemElement promptDialogCommand7 = DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) element10.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null);
      element10.CommandsConfig.Add(promptDialogCommand7);
      pagesBackendMaster.PromptDialogsConfig.Add(element10);
      LinkElement element11 = new LinkElement((ConfigElement) pagesBackendMaster.LinksConfig)
      {
        Name = "templates",
        CommandName = "manageTemplates",
        NavigateUrl = isForBackendPages ? "~/Sitefinity/Administration/BackendTemplates" : "~/Sitefinity/Design/PageTemplates"
      };
      pagesBackendMaster.LinksConfig.Add(element11);
      return pagesBackendMaster;
    }

    private static GridViewModeElement DefinePagesTreeTable(
      MasterGridViewElement pagesBackendMaster,
      bool isForBackendPages)
    {
      GridViewModeElement gridViewModeElement1 = new GridViewModeElement((ConfigElement) pagesBackendMaster.ViewModesConfig);
      gridViewModeElement1.Name = "TreeTable";
      gridViewModeElement1.EnableDragAndDrop = new bool?(true);
      gridViewModeElement1.EnableInitialExpanding = new bool?(true);
      gridViewModeElement1.ExpandedNodesCookieName = isForBackendPages ? "ExpBackendPages" : "ExpFrontEndPages";
      GridViewModeElement gridViewModeElement2 = gridViewModeElement1;
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dataColumnElement.Name = "PageLocationColumn";
      dataColumnElement.HeaderText = "StatusPage";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.HeaderCssClass = "sfTitleCol";
      dataColumnElement.ItemCssClass = "sfTitleCol";
      dataColumnElement.DisableSorting = new bool?(true);
      dataColumnElement.ClientTemplate = "<a href='javascript:void(0);' sys:class=\"{{ (IsContentEditable ? 'sf_binderCommand_editPage' : 'sf_binderCommand_void sfDisabled') + ' rtIn sfItemTitle sf' + Status.toLowerCase() + (IsHomePage ? ' sfhome' : '') +  (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() : '')}}\"><strong>{{Title.Value.htmlEncode()}}</strong><span class='sfNewRenderer sfMLeft5'>{{Renderer != null ? 'New editor' : ''}}</span><span  sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span> <span class='sfStatusLocation'><span  sys:if='AdditionalStatus'>| </span> <span>{{StatusText}}</span></span> </a>";
      DataColumnElement element = dataColumnElement;
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) element);
      DefinitionsHelper.AddVariationsColumn(gridViewModeElement2, isForBackendPages);
      DefinitionsHelper.AddTranslationsColumn(gridViewModeElement2, isForBackendPages);
      PagesDefinitions.AddViewColumn(gridViewModeElement2);
      PagesDefinitions.AddActionsMenuColumn(gridViewModeElement2);
      if (!isForBackendPages)
        DefinitionsHelper.AddPageStatsColumn(gridViewModeElement2);
      PagesDefinitions.AddDateOwnerColumn(gridViewModeElement2);
      return gridViewModeElement2;
    }

    private static GridViewModeElement DefinePagesGridView(
      MasterGridViewElement pagesBackendMaster,
      bool isForBackendPages)
    {
      GridViewModeElement gridViewModeElement1 = new GridViewModeElement((ConfigElement) pagesBackendMaster.ViewModesConfig);
      gridViewModeElement1.Name = "Grid";
      GridViewModeElement gridViewModeElement2 = gridViewModeElement1;
      pagesBackendMaster.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PagesMasterGridViewExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      };
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) gridViewModeElement2.ColumnsConfig);
      dataColumnElement.Name = "PageLocationColumn";
      dataColumnElement.HeaderText = "StatusPageLocation";
      dataColumnElement.ResourceClassId = typeof (PageResources).Name;
      dataColumnElement.HeaderCssClass = "sfTitleCol";
      dataColumnElement.ItemCssClass = "sfTitleCol";
      dataColumnElement.ClientTemplate = "<a href='javascript:void(0);' sys:class=\"{{ (IsContentEditable ? 'sf_binderCommand_editPage sfItemTitle sf' : 'sf_binderCommand_void sfDisabled  sfItemTitle sf') + Status.toLowerCase() + (IsHomePage ? ' sfhome' : '') + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() : '')}}\">\r\n                <strong>{{Title.Value.htmlEncode()}}</strong>\r\n                <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span><span class='sfNewRenderer sfMLeft5'>{{Renderer != null ? 'New editor' : ''}}</span> \r\n                <span class='sfStatusLocation'>\r\n                    <span sys:if='AdditionalStatus' class='sfSep'>| </span>\r\n                    {{StatusText}} {{Location}}\r\n                </span>\r\n                </a>";
      DataColumnElement element = dataColumnElement;
      gridViewModeElement2.ColumnsConfig.Add((ColumnElement) element);
      DefinitionsHelper.AddVariationsColumn(gridViewModeElement2, isForBackendPages);
      DefinitionsHelper.AddTranslationsColumn(gridViewModeElement2, isForBackendPages);
      PagesDefinitions.AddViewColumn(gridViewModeElement2);
      PagesDefinitions.AddActionsMenuColumn(gridViewModeElement2);
      if (!isForBackendPages)
        PagesDefinitions.AddStatsColumn(gridViewModeElement2);
      PagesDefinitions.AddDateOwnerColumn(gridViewModeElement2);
      return gridViewModeElement2;
    }

    private static void AddStatsColumn(GridViewModeElement gridMode)
    {
      DynamicColumnElement dynamicColumnElement = new DynamicColumnElement((ConfigElement) gridMode.ColumnsConfig);
      dynamicColumnElement.Name = "GoogleStats";
      dynamicColumnElement.HeaderText = "Analytics";
      dynamicColumnElement.ResourceClassId = typeof (Labels).Name;
      dynamicColumnElement.DisableSorting = new bool?(true);
      dynamicColumnElement.HeaderCssClass = "sfStatsColumn";
      dynamicColumnElement.ItemCssClass = "sfStatsColumn";
      dynamicColumnElement.ModuleName = "Analytics";
      dynamicColumnElement.DynamicMarkupGenerator = typeof (GoogleStatsColumnGenerator);
      DynamicColumnElement element = dynamicColumnElement;
      gridMode.ColumnsConfig.Add((ColumnElement) element);
    }

    private static DetailFormViewElement DefineDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode,
      string viewName,
      bool isForBackendPages,
      bool isDuplicateMode)
    {
      return PagesDefinitions.DefineDetailsView(fluentContentView, isCreateMode, viewName, isForBackendPages, isDuplicateMode, Guid.Empty);
    }

    private static DetailFormViewElement DefineDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode,
      string viewName,
      bool isForBackendPages,
      bool isDuplicateMode,
      Guid rootId)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PagesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isDuplicateMode ? "DuplicatePage" : (isCreateMode ? "CreatePage" : "EditPage")).HideTopToolbar().LocalizeUsing<PageResources>().SetServiceBaseUrl("~/Sitefinity/Services/Pages/PagesService.svc/").SetExternalClientScripts(extenalClientScripts).DoNotUnlockDetailItemOnExit().DoNotUseWorkflow();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("PagesNameFieldControl").SetTitle("Name").SetCssClass("sfTitleField sfShortField250").SetExample("NameExample").AddValidation().MakeRequired().SetRequiredViolationMessage("NameCannotBeEmpty").SetMaxLength(500).SetMaxLengthViolationMessage("TitleMaxLength").Done();
      if (isDuplicateMode)
      {
        SiteSelectorFieldElement selectorFieldElement = new SiteSelectorFieldElement((ConfigElement) viewSectionElement1.Fields);
        selectorFieldElement.ID = "siteSelectorField";
        selectorFieldElement.DataFieldName = "TargetSiteId";
        selectorFieldElement.FieldType = typeof (SiteSelectorField);
        selectorFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
        selectorFieldElement.ModuleName = "MultisiteInternal";
        selectorFieldElement.ResourceClassId = PagesDefinitions.ResourceClassId;
        selectorFieldElement.Title = "SiteAndLanguageSelectorFieldTitle";
        selectorFieldElement.Description = "DuplicatePageSiteFieldDescription";
        selectorFieldElement.CssClass = "sfFormSeparator";
        selectorFieldElement.ShowSiteLanguageSelector = true;
        SiteSelectorFieldElement element = selectorFieldElement;
        viewSectionElement1.Fields.Add((FieldDefinitionElement) element);
        definitionFacade2.AddGroupSingleCheckobxField("DuplicateChildren", "DuplicateChildPages", false).SetId("duplicateChildrenChoiceField").SetDisplayMode(FieldDisplayMode.Write).SetCssClass("sfCheckBox").Done();
      }
      LanguageChoiceFieldElement choiceFieldElement1 = new LanguageChoiceFieldElement((ConfigElement) viewSectionElement1.Fields);
      choiceFieldElement1.ID = "languageChoiceField";
      choiceFieldElement1.FieldType = typeof (LanguageChoiceField);
      choiceFieldElement1.ResourceClassId = typeof (PageResources).Name;
      choiceFieldElement1.Title = "Language";
      choiceFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement1.FieldName = "languageField";
      choiceFieldElement1.DataFieldName = "AvailableLanguages";
      choiceFieldElement1.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement1.LanguageSource = new LanguageSource?(isForBackendPages ? LanguageSource.Backend : LanguageSource.Frontend);
      choiceFieldElement1.MutuallyExclusive = true;
      LanguageChoiceFieldElement element1 = choiceFieldElement1;
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element1);
      GenericHierarchicalFieldDefinitionElement definitionElement1 = new GenericHierarchicalFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      definitionElement1.ID = "parentSelectorField";
      definitionElement1.DataFieldName = "Parent";
      definitionElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement1.ResourceClassId = PagesDefinitions.ResourceClassId;
      definitionElement1.WebServiceBaseUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      definitionElement1.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement1.CssClass = "sfPageHierarchy sfFormSeparator";
      definitionElement1.FieldType = typeof (PageSelectorField);
      definitionElement1.RootId = rootId;
      definitionElement1.SelectedNodeDataFieldName = "TitlesPath";
      definitionElement1.ExpandableDefinitionConfig.Expanded = new bool?(isCreateMode);
      GenericHierarchicalFieldDefinitionElement element2 = definitionElement1;
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element2);
      UrlMirrorTextFieldElement textFieldElement1;
      if (isForBackendPages)
      {
        UrlMirrorTextFieldElement textFieldElement2 = new UrlMirrorTextFieldElement((ConfigElement) viewSectionElement1.Fields);
        textFieldElement2.ID = "UrlNameFieldControl";
        textFieldElement2.DataFieldName = "UrlName";
        textFieldElement2.Title = "UrlName";
        textFieldElement2.ResourceClassId = typeof (PageResources).Name;
        textFieldElement2.MirroredControlId = definitionFacade3.Get().ID;
        textFieldElement2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
        textFieldElement2.RegularExpressionFilter = DefinitionsHelper.UrlPagesRegularExpressionFilterForPages;
        textFieldElement2.WrapperTag = HtmlTextWriterTag.Li;
        textFieldElement2.ReplaceWith = "-";
        textFieldElement2.UrlControlId = element2.ID;
        textFieldElement2.CssClass = "sfPageUrlField sfFormSeparator";
        textFieldElement1 = textFieldElement2;
      }
      else
      {
        PageUrlMirrorTextFieldElement textFieldElement3 = new PageUrlMirrorTextFieldElement((ConfigElement) viewSectionElement1.Fields);
        textFieldElement3.ID = "UrlNameFieldControl";
        textFieldElement3.DataFieldName = "UrlName";
        textFieldElement3.Title = "UrlName";
        textFieldElement3.ResourceClassId = typeof (PageResources).Name;
        textFieldElement3.MirroredControlId = definitionFacade3.Get().ID;
        textFieldElement3.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
        textFieldElement3.WrapperTag = HtmlTextWriterTag.Li;
        textFieldElement3.UrlControlId = element2.ID;
        textFieldElement3.CssClass = "sfPageUrlField sfFormSeparator";
        textFieldElement3.CustomUrlValidationMessage = Res.Get<PageResources>().CustomUrlValidationMessage;
        textFieldElement1 = (UrlMirrorTextFieldElement) textFieldElement3;
      }
      textFieldElement1.ValidatorConfig = new ValidatorDefinitionElement((ConfigElement) textFieldElement1)
      {
        Required = new bool?(true),
        MessageCssClass = "sfError",
        RequiredViolationMessage = "UrlNameCannotBeEmpty",
        RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForContentValidator,
        RegularExpressionViolationMessage = "UrlNameInvalidSymbols",
        MaxLength = (int) byte.MaxValue,
        MaxLengthViolationMessage = "UrlMaxLength",
        ResourceClassId = typeof (PageResources).Name
      };
      viewSectionElement1.Fields.Add((FieldDefinitionElement) textFieldElement1);
      definitionFacade2.AddExpandableField("AllowMultipleUrls").SetId("multipleUrlsExpandableField").SetDisplayMode(FieldDisplayMode.Write).SetCssClass("sfCheckBox").DefineToggleControl("AllowMultipleUrls", "AllowMultipleUrlsForThisPage", false).SetId("allowMultipleUrlsFieldElement").SetCssClass("sfMTop10 sfMBottom10").Done().AddTextField("MultipleNavigationNodes").SetId("urlsTextArea").SetFieldType<MultilineTextField>().SetTitle("AdditionalUrls").SetExample("AdditionalUrlsExample").SetRows(5).SetFieldType<TextField>().SetCssClass("sfDependentGroup sfFirstInDependentGroup sfInGroup sfTxtAreaExampleOnRight sfMBottom5").AddExpandableBehaviorAndContinue().AddValidation().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalPagesUrlsValidator).SetRegularExpressionViolationMessage("AdditionalUrlNameInvalidSymbols").SetMessageCssClass("sfError").SetRegularExpressionSeparator("\\r\\n|\\r|\\n").DoNotValidateIfInvisible().Done().Done().AddSingleCheckboxField("AdditionalUrlsRedirectToDefaultOne", "AllAdditionalUrlsRedirectToTheDefaultOne").SetId("allAdditionalUrlsRedirectToTheDefaultPage").SetCssClass("sfDependentGroup sfCheckBox sfMBottom10").Done().Done();
      definitionFacade2.AddGroupSingleCheckobxField("ShowInNavigation", "ShowInNavigation", true).SetId("choiceField").SetDisplayMode(FieldDisplayMode.Write).SetCssClass("sfCheckBox sfInGroup sfFormSeparator").Done();
      definitionFacade2.AddGroupSingleCheckobxField("IsGroup", "UseForGroupPage", true).SetId("groupPageCheckbox").SetDisplayMode(FieldDisplayMode.Write).SetDescription("GroupPageDescription").Done();
      ExternalPageFieldElement pageFieldElement1 = new ExternalPageFieldElement((ConfigElement) viewSectionElement1.Fields);
      pageFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      pageFieldElement1.DataFieldName = "IsExternalGroup";
      pageFieldElement1.FieldName = "externalPageFieldElement";
      pageFieldElement1.ResourceClassId = PagesDefinitions.ResourceClassId;
      pageFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      pageFieldElement1.FieldType = typeof (ExternalPageField);
      pageFieldElement1.CssClass = "sfCheckBox sfInGroup";
      pageFieldElement1.InternalPageId = Guid.Empty;
      ExternalPageFieldElement pageFieldElement2 = pageFieldElement1;
      ChoiceFieldElement choiceFieldElement2 = new ChoiceFieldElement((ConfigElement) pageFieldElement2);
      choiceFieldElement2.ID = "isExternalPageFieldDefinition";
      choiceFieldElement2.DataFieldName = "IsExternal";
      choiceFieldElement2.RenderChoiceAs = RenderChoicesAs.SingleCheckBox;
      choiceFieldElement2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement2.MutuallyExclusive = true;
      choiceFieldElement2.WrapperTag = HtmlTextWriterTag.Div;
      choiceFieldElement2.Description = Res.Get<PageResources>().ExternalPageFieldDescription;
      ChoiceFieldElement choiceFieldElement3 = choiceFieldElement2;
      choiceFieldElement3.ChoicesConfig.Add(new ChoiceElement((ConfigElement) choiceFieldElement3.ChoicesConfig)
      {
        Text = "ExternalPageFieldLabel",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Value = "false"
      });
      pageFieldElement2.ValidatorConfig = new ValidatorDefinitionElement((ConfigElement) pageFieldElement2)
      {
        Required = new bool?(true),
        MessageCssClass = "sfError",
        RequiredViolationMessage = Res.Get<PageResources>().ExternalPageFieldValidatorMessage
      };
      TextFieldDefinitionElement definitionElement2 = new TextFieldDefinitionElement((ConfigElement) pageFieldElement2);
      definitionElement2.ID = "externalPageUrlFieldDefinition";
      definitionElement2.DataFieldName = "RedirectUrl";
      definitionElement2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      TextFieldDefinitionElement definitionElement3 = definitionElement2;
      ChoiceFieldElement choiceFieldElement4 = new ChoiceFieldElement((ConfigElement) pageFieldElement2);
      choiceFieldElement4.ID = "openInNewWindowFieldDefinition";
      choiceFieldElement4.DataFieldName = "OpenNewWindow";
      choiceFieldElement4.RenderChoiceAs = RenderChoicesAs.SingleCheckBox;
      choiceFieldElement4.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement4.MutuallyExclusive = true;
      choiceFieldElement4.WrapperTag = HtmlTextWriterTag.Div;
      ChoiceFieldElement choiceFieldElement5 = choiceFieldElement4;
      pageFieldElement2.IsExternalPageChoiceFieldDefinition = choiceFieldElement3;
      pageFieldElement2.ExternalPageUrlFieldDefinition = definitionElement3;
      pageFieldElement2.OpenInNewWindowChoiceFieldDefinition = choiceFieldElement5;
      viewSectionElement1.Fields.Add((FieldDefinitionElement) pageFieldElement2);
      definitionFacade1.AddSection("SeoSection").SetTitle("SEOSectionTitle").AddExpandableBehavior().Expand().Done().AddMirrorTextField("SeoTitle", "SeoTitle.PersistedValue").SetId("SeoTitleFieldControl").SetMirroredControlId(definitionFacade3.Get().ID).SetTitle("TitleForSearchEngines").SetRegularExpressionFilter(DefinitionsHelper.SeoRegularExpressionFilter).SetCssClass("sfTitleField").SetExample("SeoTitleExample").DisableChangeButton().ShowCharacterCounter().RecommendedCharactersCount(70).SetCharacterCounterDescription("CharacterCounterDescription").ToUpper().AddValidation().MakeRequired().SetRequiredViolationMessage("SeoTitleCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionSeoFilterForPagesValidator).SetRegularExpressionViolationMessage("SeoTitleInvalidSymbols").DoNotValidateIfInvisible().Done().Done();
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddSection("TemplateSection").Get();
      PageTemplateFieldDefinitionElement element3 = new PageTemplateFieldDefinitionElement((ConfigElement) viewSectionElement2.Fields);
      element3.ID = "PageTemplateField";
      element3.DataFieldName = "Template";
      element3.Title = "PageTemplateYouCanChangeItLater";
      element3.ResourceClassId = typeof (PageResources).Name;
      element3.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.IsBackendTemplate = isForBackendPages;
      element3.ShowEmptyTemplate = true;
      element3.ShowAllBasicTemplates = false;
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element3);
      definitionFacade1.AddExpandableSection("DescriptionKeywordsSection").SetTitle("DescriptionKeywordsSection").AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetRows(5).ShowCharacterCounter().RecommendedCharactersCount(150).SetCharacterCounterDescription("CharacterCounterDescription").Done().AddTextField("Keywords", "Keywords.PersistedValue").SetId("KeywordsFieldControl").SetTitle("Keywords").SetRows(5).SetDescription("KeywordsDescription").SetExample("KeywordsExample").Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("AdvancedOptionsSection").SetTitle("AdvancedOptionsSection");
      ContentViewSectionElement viewSectionElement3 = definitionFacade4.Get();
      definitionFacade4.AddGroupSingleCheckobxField("IncludeInSearchIndex", "AllowSiteSearchToIndexThisPage", true).SetId("allowInternalIndexingChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done();
      definitionFacade4.AddGroupSingleCheckobxField("Crawlable", "AllowSearchEnginesToIndexThisPage", true).SetId("allowIndexingChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done().AddTextField("Priority").SetCssClass("sfMLeft15 sfShortField40").SetTitle("Priority").SetToolTipVisible().SetToolTipContent("PriorityToolTipMessage").AddValidation().SetExpectedFormat(ValidationFormat.Numeric).SetMinValue((object) 0).SetMinValueViolationMessage("PriorityRangeViolationMessage").SetMaxValueViolationMessage("PriorityRangeViolationMessage").SetNumericViolationMessage("PriorityRangeViolationMessage").SetMessageCssClass("sfError").SetMaxValue((object) 1).DoNotValidateIfInvisible();
      definitionFacade4.AddGroupSingleCheckobxField("RequireSsl", "RequireSsl", false).SetId("requireSslChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done();
      definitionFacade4.AddGroupSingleCheckobxField("EnableViewState", "EnableViewState", false).SetId("enableViewStateChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done();
      definitionFacade4.AddGroupSingleCheckobxField("IncludeScriptManager", "IncludeRadScriptManager", false).SetId("enableAjaxChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done();
      CacheProfileFieldElement profileFieldElement1 = new CacheProfileFieldElement((ConfigElement) viewSectionElement3.Fields);
      profileFieldElement1.FieldName = "cacheProfileElement";
      profileFieldElement1.DisplayMode = FieldDisplayMode.Write;
      profileFieldElement1.ResourceClassId = PagesDefinitions.ResourceClassId;
      profileFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      profileFieldElement1.FieldType = typeof (PageCacheProfileField);
      profileFieldElement1.Title = "CachingOptions";
      profileFieldElement1.CssClass = "sfCheckBox sfInGroup";
      CacheProfileFieldElement profileFieldElement2 = profileFieldElement1;
      ChoiceFieldElement choiceFieldElement6 = new ChoiceFieldElement((ConfigElement) profileFieldElement2);
      choiceFieldElement6.ID = "profileChoiceFieldDefinition";
      choiceFieldElement6.DataFieldName = "OutputCacheProfile";
      choiceFieldElement6.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement6.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement6.WrapperTag = HtmlTextWriterTag.Div;
      choiceFieldElement6.CssClass = "sfCheckBox sfInlineBlock";
      ChoiceFieldElement choiceFieldElement7 = choiceFieldElement6;
      choiceFieldElement7.ChoicesConfig.Add(new ChoiceElement((ConfigElement) choiceFieldElement7.ChoicesConfig)
      {
        Text = "AsForWholeSite",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Value = string.Empty
      });
      definitionFacade4.AddGroupSingleCheckobxField("AllowParameterValidation", "AllowParameterValidation", true).SetId("allowParameterValidationChoiceField").SetDisplayMode(FieldDisplayMode.Write).Done();
      profileFieldElement2.CssClass = "sfSubgroupInGroup";
      profileFieldElement2.ProfileChoiceFieldDefinition = choiceFieldElement7;
      viewSectionElement3.Fields.Add((FieldDefinitionElement) profileFieldElement2);
      CanonicalUrlSettingsFieldElement settingsFieldElement1 = new CanonicalUrlSettingsFieldElement((ConfigElement) viewSectionElement3.Fields);
      settingsFieldElement1.FieldName = "canonicalUrlSettingsFieldElement";
      settingsFieldElement1.DisplayMode = FieldDisplayMode.Write;
      settingsFieldElement1.ResourceClassId = PagesDefinitions.ResourceClassId;
      settingsFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      settingsFieldElement1.FieldType = typeof (CanonicalUrlSettingsField);
      settingsFieldElement1.Title = "CanonicalUrl";
      settingsFieldElement1.CssClass = "sfCheckBox sfInGroup";
      CanonicalUrlSettingsFieldElement settingsFieldElement2 = settingsFieldElement1;
      ChoiceFieldElement choiceFieldElement8 = new ChoiceFieldElement((ConfigElement) settingsFieldElement2);
      choiceFieldElement8.ID = "canonicalUrlSettingsChoiceFieldDefinition";
      choiceFieldElement8.DataFieldName = "EnableDefaultCanonicalUrl";
      choiceFieldElement8.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement8.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement8.WrapperTag = HtmlTextWriterTag.Div;
      choiceFieldElement8.CssClass = "sfCheckBox sfInlineBlock";
      ChoiceFieldElement choiceFieldElement9 = choiceFieldElement8;
      settingsFieldElement2.CssClass = "sfSubgroupInGroup";
      settingsFieldElement2.CanonicalUrlSettingsChoiceFieldDefinition = choiceFieldElement9;
      viewSectionElement3.Fields.Add((FieldDefinitionElement) settingsFieldElement2);
      definitionFacade4.AddTextField("HeadTagContent").SetId("htmlIncludedInTheHeadTagElement").SetTitle("HtmlIncludedInTheHeadTag").SetRows(5).SetExample("HtmlIncludedInTheHeadTagExample").Done().AddTextField("CodeBehindType").SetDataFieldName("CodeBehindType").SetId("codeBehindTypeElement").SetTitle("CodeBehindType").SetDescription("UseTheFullyQualifiedNameOfTheType").SetExample("CodeBehindTypeExample").Done();
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element4.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.Items);
      element5.Name = "SaveChangesWidgetElement";
      element5.ButtonType = CommandButtonType.Save;
      element5.CommandName = isCreateMode ? "create" : "save";
      element5.Text = isCreateMode ? "CreateAndAddContent" : "SaveChanges";
      element5.ResourceClassId = PagesDefinitions.ResourceClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Span;
      element5.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element5);
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items2 = element4.Items;
        CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.Items);
        element6.Name = "CreateAndReturnWidgetElement";
        element6.ButtonType = CommandButtonType.Standard;
        element6.CommandName = "save";
        element6.Text = "CreateAndReturnToPages";
        element6.ResourceClassId = PagesDefinitions.ResourceClassId;
        element6.WrapperTagKey = HtmlTextWriterTag.Span;
        element6.WidgetType = typeof (CommandWidget);
        items2.Add((WidgetElement) element6);
      }
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element4.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (PageResources).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element7 = menuWidgetElement;
        ConfigElementList<WidgetElement> menuItems1 = element7.MenuItems;
        CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
        element8.Name = "DeleteCommandName";
        element8.ButtonType = CommandButtonType.SimpleLinkButton;
        element8.Text = "DeleteThisItem";
        element8.CommandName = "delete";
        element8.ResourceClassId = typeof (PageResources).Name;
        element8.WidgetType = typeof (CommandWidget);
        menuItems1.Add((WidgetElement) element8);
        ConfigElementList<WidgetElement> menuItems2 = element7.MenuItems;
        CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
        element9.Name = "PermissionsCommandName";
        element9.ButtonType = CommandButtonType.SimpleLinkButton;
        element9.Text = "SetPermissions";
        element9.CommandName = "permissions";
        element9.ResourceClassId = typeof (PageResources).Name;
        element9.WidgetType = typeof (CommandWidget);
        menuItems2.Add((WidgetElement) element9);
        element4.Items.Add((WidgetElement) element7);
      }
      ConfigElementList<WidgetElement> items3 = element4.Items;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element4.Items);
      element10.Name = "CancelWidgetElement";
      element10.ButtonType = CommandButtonType.Cancel;
      element10.CommandName = "cancel";
      element10.Text = "BackToItems";
      element10.ResourceClassId = typeof (PageResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Span;
      element10.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element10);
      detailFormViewElement.Toolbar.Sections.Add(element4);
      PromptDialogElement parentPageDialog = PagesDefinitions.GetCannotDeleteParentPageDialog(detailFormViewElement.PromptDialogsConfig);
      detailFormViewElement.PromptDialogsConfig.Add(parentPageDialog);
      PromptDialogElement deleteHomePageDialog = PagesDefinitions.GetCannotDeleteHomePageDialog(detailFormViewElement.PromptDialogsConfig);
      detailFormViewElement.PromptDialogsConfig.Add(deleteHomePageDialog);
      definitionFacade1.AddReadOnlySection("SidebarSection").AddRelatingDataField();
      return detailFormViewElement;
    }

    private static void AddViewColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "ViewColumn";
      dataColumnElement.DisableSorting = new bool?(true);
      dataColumnElement.HeaderCssClass = "sfView";
      dataColumnElement.ItemCssClass = "sfView";
      dataColumnElement.ClientTemplate = "<a sys:href=\"{{ (PageLiveUrl) }}\" target=\"_blank\">" + Res.Get<Labels>().View + "</a>";
      dataColumnElement.HeaderText = "&nbsp;";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static void AddActionsMenuColumn(GridViewModeElement parent)
    {
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) parent.ColumnsConfig);
      menuColumnElement.Name = "ActionsLinkText";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.HeaderText = "ActionsLinkText";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.TitleText = "ActionsLinkText";
      menuColumnElement.DisableSorting = new bool?(true);
      menuColumnElement.ResourceClassId = typeof (PageResources).Name;
      ActionMenuColumnElement element1 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element1.MenuItems;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element2.Name = "Delete";
      element2.WrapperTagKey = HtmlTextWriterTag.Li;
      element2.CommandName = "delete";
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
      element4.Name = "Unpublish";
      element4.WrapperTagKey = HtmlTextWriterTag.Li;
      element4.CommandName = "unpublishPage";
      element4.Text = "Unpublish";
      element4.ResourceClassId = typeof (Labels).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = "sfUnpublishItm";
      menuItems3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> menuItems4 = element1.MenuItems;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element5.Name = "Duplicate";
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.CommandName = "duplicate";
      element5.Text = "Duplicate";
      element5.ResourceClassId = typeof (Labels).Name;
      element5.WidgetType = typeof (CommandWidget);
      element5.CssClass = "sfDuplicateItm";
      menuItems4.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems5 = element1.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element6.Name = "SetAsHomepage";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "setAsHomepage";
      element6.Text = "SetAsHomepage";
      element6.ResourceClassId = typeof (PageResources).Name;
      element6.WidgetType = typeof (CommandWidget);
      element6.CssClass = "sfSetHome";
      menuItems5.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems6 = element1.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element7.Name = "ShareLinkButton";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "shareLink";
      element7.Text = "ShareLink";
      element7.ResourceClassId = typeof (PageResources).Name;
      element7.WidgetType = typeof (CommandWidget);
      element7.CssClass = "sfShareLinkItm";
      menuItems6.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems7 = element1.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element8.Name = "Unlock";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "unlockPage";
      element8.Text = "Unlock";
      element8.ResourceClassId = typeof (Labels).Name;
      element8.WidgetType = typeof (CommandWidget);
      menuItems7.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems8 = element1.MenuItems;
      LiteralWidgetElement element9 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element9.Name = "Create";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.IsSeparator = true;
      element9.CssClass = "sfSepNoTitle";
      menuItems8.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems9 = element1.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element10.Name = "CreateChildPage";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "createChild";
      element10.Text = "CreateChildPage";
      element10.ResourceClassId = typeof (PageResources).Name;
      element10.WidgetType = typeof (CommandWidget);
      element10.CssClass = "sfCreateChild";
      menuItems9.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems10 = element1.MenuItems;
      LiteralWidgetElement element11 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element11.Name = "Edit";
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.Text = "EditEllipsis";
      element11.ResourceClassId = typeof (Labels).Name;
      element11.IsSeparator = true;
      menuItems10.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems11 = element1.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element12.Name = "Content";
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.CommandName = "editPageContent";
      element12.Text = "Content";
      element12.ResourceClassId = typeof (Labels).Name;
      element12.WidgetType = typeof (CommandWidget);
      menuItems11.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> menuItems12 = element1.MenuItems;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element13.Name = "Properties";
      element13.WrapperTagKey = HtmlTextWriterTag.Li;
      element13.CommandName = "edit";
      element13.Text = "TitleAndProperties";
      element13.ResourceClassId = typeof (PageResources).Name;
      element13.WidgetType = typeof (CommandWidget);
      menuItems12.Add((WidgetElement) element13);
      ConfigElementList<WidgetElement> menuItems13 = element1.MenuItems;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element14.Name = "Permissions";
      element14.WrapperTagKey = HtmlTextWriterTag.Li;
      element14.CommandName = "permissions";
      element14.Text = "Permissions";
      element14.ResourceClassId = typeof (Labels).Name;
      element14.WidgetType = typeof (CommandWidget);
      menuItems13.Add((WidgetElement) element14);
      ConfigElementList<WidgetElement> menuItems14 = element1.MenuItems;
      CommandWidgetElement element15 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element15.Name = "RevisionHistory";
      element15.WrapperTagKey = HtmlTextWriterTag.Li;
      element15.CommandName = "historygrid";
      element15.Text = "RevisionHistory";
      element15.ResourceClassId = typeof (Labels).Name;
      element15.WidgetType = typeof (CommandWidget);
      menuItems14.Add((WidgetElement) element15);
      ConfigElementList<WidgetElement> menuItems15 = element1.MenuItems;
      LiteralWidgetElement element16 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element16.Name = "Change";
      element16.WrapperTagKey = HtmlTextWriterTag.Li;
      element16.Text = "Change";
      element16.ResourceClassId = typeof (Labels).Name;
      element16.IsSeparator = true;
      menuItems15.Add((WidgetElement) element16);
      ConfigElementList<WidgetElement> menuItems16 = element1.MenuItems;
      CommandWidgetElement element17 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element17.Name = "ChangeTemplate";
      element17.WrapperTagKey = HtmlTextWriterTag.Li;
      element17.CommandName = "changeTemplate";
      element17.Text = "Template";
      element17.ResourceClassId = typeof (PageResources).Name;
      element17.WidgetType = typeof (CommandWidget);
      menuItems16.Add((WidgetElement) element17);
      ConfigElementList<WidgetElement> menuItems17 = element1.MenuItems;
      CommandWidgetElement element18 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element18.Name = "ChangeOwner";
      element18.WrapperTagKey = HtmlTextWriterTag.Li;
      element18.CommandName = "changePageOwner";
      element18.Text = "Owner";
      element18.RequiredPermissionSet = "Backend";
      element18.RequiredActions = "ManageUsers";
      element18.ResourceClassId = typeof (Labels).Name;
      element18.WidgetType = typeof (CommandWidget);
      menuItems17.Add((WidgetElement) element18);
      ConfigElementList<WidgetElement> menuItems18 = element1.MenuItems;
      LiteralWidgetElement element19 = new LiteralWidgetElement((ConfigElement) element1.MenuItems);
      element19.Name = "Move";
      element19.WrapperTagKey = HtmlTextWriterTag.Li;
      element19.Text = "MoveEllipsis";
      element19.ResourceClassId = typeof (Labels).Name;
      element19.IsSeparator = true;
      menuItems18.Add((WidgetElement) element19);
      ConfigElementList<WidgetElement> menuItems19 = element1.MenuItems;
      CommandWidgetElement element20 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element20.Name = "Up";
      element20.WrapperTagKey = HtmlTextWriterTag.Li;
      element20.CommandName = "moveUp";
      element20.Text = "Up";
      element20.ResourceClassId = typeof (Labels).Name;
      element20.WidgetType = typeof (CommandWidget);
      element20.CssClass = "sfMoveUp";
      menuItems19.Add((WidgetElement) element20);
      ConfigElementList<WidgetElement> menuItems20 = element1.MenuItems;
      CommandWidgetElement element21 = new CommandWidgetElement((ConfigElement) element1.MenuItems);
      element21.Name = "Down";
      element21.WrapperTagKey = HtmlTextWriterTag.Li;
      element21.CommandName = "moveDown";
      element21.Text = "Down";
      element21.ResourceClassId = typeof (Labels).Name;
      element21.WidgetType = typeof (CommandWidget);
      element21.CssClass = "sfMoveDown";
      menuItems20.Add((WidgetElement) element21);
      parent.ColumnsConfig.Add((ColumnElement) element1);
    }

    private static void AddDateOwnerColumn(GridViewModeElement parent)
    {
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) parent.ColumnsConfig);
      dataColumnElement.Name = "DateOwner";
      dataColumnElement.HeaderText = "DateOwner";
      dataColumnElement.ResourceClassId = typeof (Labels).Name;
      dataColumnElement.DisableSorting = new bool?(true);
      dataColumnElement.ClientTemplate = "<span>{{ (DateCreated) ? DateCreated.sitefinityLocaleFormat('dd MMM, yyyy hh:mm') : '-' }}</span>\r\n                                   <span class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement.HeaderCssClass = "sfDateAuthor";
      dataColumnElement.ItemCssClass = "sfDateAuthor";
      DataColumnElement element = dataColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    private static PromptDialogElement GetCannotDeleteParentPageDialog(
      ConfigElementList<PromptDialogElement> parent)
    {
      PromptDialogElement parentPageDialog = new PromptDialogElement((ConfigElement) parent)
      {
        DialogName = "cannotDeleteParentPageDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitlePageCannotDeleteChildren",
        Message = "PromptMessagePageCannotDeleteChildren",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      parentPageDialog.CommandsConfig.Add(DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) parentPageDialog.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null));
      return parentPageDialog;
    }

    private static PromptDialogElement GetCannotDeleteHomePageDialog(
      ConfigElementList<PromptDialogElement> parent)
    {
      PromptDialogElement deleteHomePageDialog = new PromptDialogElement((ConfigElement) parent)
      {
        DialogName = "cannotDeleteHomepageDialog",
        ResourceClassId = PagesDefinitions.ResourceClassId,
        Title = "PromptTitlePageCannotDeleteHomepage",
        Message = "PromptMessagePageCannotDeleteHomepage",
        Width = 300,
        Height = 300,
        ShowOnLoad = false
      };
      deleteHomePageDialog.CommandsConfig.Add(DefinitionsHelper.CreatePromptDialogCommand((ConfigElement) deleteHomePageDialog.CommandsConfig, "Ok", "ok", CommandType.NormalButton, "LI", (string) null));
      return deleteHomePageDialog;
    }
  }
}
