// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplatesDefinitions
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
using Telerik.Sitefinity.Modules.ControlTemplates.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for the Control Templates module.
  /// </summary>
  public class ControlTemplatesDefinitions
  {
    public const string BackendVersionPreviewViewName = "NewsBackendVersionPreview";
    /// <summary>
    /// Name of the control definition used for defining <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> control
    /// to manage Control Templates on the backend.
    /// </summary>
    public static string BackendDefinitionName = "ControlTemplatesBackend";
    /// <summary>
    /// Name of the view used to display list of Control Templates on the backend.
    /// </summary>
    public static string BackendListViewName = "ControlTemplatesBackendList";
    /// <summary>
    /// Name of the view used to edit Control Templates on the backend.
    /// </summary>
    public static string BackendEditDetailsViewName = "ControlTemplatesBackendEdit";
    /// <summary>
    /// Name of the view used to inster Control Templates on the backend.
    /// </summary>
    public static string BackendInsertDetailsViewName = "ControlTemplatesBackendInsert";
    /// <summary>Version Comparison View Name</summary>
    public const string VersionComparisonView = "ControlTemplatesBackendVersionComparisonView";

    /// <summary>
    /// Defines the ContentView control for Control Templates on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineControlTemplatesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("ControlTemplates").DefineContainer(parent, ControlTemplatesDefinitions.BackendDefinitionName, typeof (PresentationData));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy((object) ControlTemplatesDefinitions.BackendListViewName, (Func<ConfigElement>) (() => ControlTemplatesDefinitions.DefineBackendListView(ControlTemplatesDefinitions.BackendListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy((object) "ControlTemplatesBackendVersionComparisonView", (Func<ConfigElement>) (() => ControlTemplatesDefinitions.DefineNewsBackendVersioningComparisonView(backendContentView)));
      return backendContentView;
    }

    private static ConfigElement DefineBackendListView(
      string viewName,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.ControlTemplates.Web.Scripts.ControlTemplatesMasterExtensions.js, Telerik.Sitefinity", "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).LocalizeUsing<ControlTemplatesResources>().SetTitle("ModuleTitle").SetServiceBaseUrl("~/Sitefinity/Services/ControlTemplates/ControlTemplateService.svc").SetExternalClientScripts(extenalClientScripts).SetExtendedSearchFields("Name").SetSortExpression("DateCreated DESC");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "CreateTemplateWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateATemplate";
      commandWidgetElement1.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.PermissionSet = "General";
      commandWidgetElement1.ActionName = "Create";
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "DeleteWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupDelete";
      commandWidgetElement2.Text = "Delete";
      commandWidgetElement2.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (PresentationData)));
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      string contentType = typeof (PresentationData).FullName;
      IEnumerable<SortingExpressionElement> expressionElements1 = contentViewConfig.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == contentType && !s.IsCustom));
      IEnumerable<SortingExpressionElement> expressionElements2 = contentViewConfig.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == contentType && s.IsCustom));
      DynamicCommandWidgetElement commandWidgetElement3 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement3.Name = "SortTemplatesWidget";
      commandWidgetElement3.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement3.HeaderText = "SortTemplates";
      commandWidgetElement3.PageSize = 10;
      commandWidgetElement3.MoreLinkText = "More";
      commandWidgetElement3.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement3.WidgetType = typeof (SortWidget);
      commandWidgetElement3.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      commandWidgetElement3.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement3.ContentType = typeof (PresentationData);
      DynamicCommandWidgetElement element4 = commandWidgetElement3;
      element1.Items.Add((WidgetElement) element4);
      foreach (SortingExpressionElement expressionElement in expressionElements1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element4.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element4.Items.Add(dynamicItemElement);
      }
      foreach (SortingExpressionElement expressionElement in expressionElements2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element4.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element4.CustomItems.Add(dynamicItemElement);
      }
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      WidgetBarSectionElement element5 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Filter",
        Title = "FilterWidgetTemplates",
        ResourceClassId = ControlTemplatesModule.ResourceClassId,
        CssClass = "sfWidgetsList sfFirstNoHeading sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items1 = element5.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.Items);
      element6.Name = "AllTemplates";
      element6.CommandName = "showAllTemplates";
      element6.ButtonType = CommandButtonType.SimpleLinkButton;
      element6.ButtonCssClass = "sfSel";
      element6.Text = "AllTemplates";
      element6.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      element6.WidgetType = typeof (CommandWidget);
      element6.IsSeparator = false;
      items1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> items2 = element5.Items;
      MultisiteCommandWidgetElement element7 = new MultisiteCommandWidgetElement((ConfigElement) element5.Items);
      element7.Name = "ThisSiteTemplates";
      element7.CommandName = "showThisSiteTemplates";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.Text = "ThisSiteCapital";
      element7.ResourceClassId = typeof (MultisiteResources).Name;
      element7.CssClass = "";
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      element7.IsFilterCommand = true;
      element7.ButtonCssClass = "sfSel";
      element7.ModuleName = "MultisiteInternal";
      items2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> items3 = element5.Items;
      MultisiteCommandWidgetElement element8 = new MultisiteCommandWidgetElement((ConfigElement) element5.Items);
      element8.Name = "NotSharedTemplates";
      element8.CommandName = "showNotSharedTemplates";
      element8.ButtonType = CommandButtonType.SimpleLinkButton;
      element8.Text = "NotSharedWithAnySite";
      element8.ResourceClassId = typeof (MultisiteResources).Name;
      element8.CssClass = "";
      element8.WidgetType = typeof (CommandWidget);
      element8.IsSeparator = false;
      element8.IsFilterCommand = true;
      element8.ModuleName = "MultisiteInternal";
      items3.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> items4 = element5.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element5.Items);
      element9.Name = "MyTemplates";
      element9.CommandName = "showMyTemplates";
      element9.ButtonType = CommandButtonType.SimpleLinkButton;
      element9.Text = "MyTemplates";
      element9.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      element9.WidgetType = typeof (CommandWidget);
      element9.IsSeparator = false;
      items4.Add((WidgetElement) element9);
      masterGridViewElement.SidebarConfig.Sections.Add(element5);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element10 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element10);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element10.ColumnsConfig;
      DataColumnElement element11 = new DataColumnElement((ConfigElement) element10.ColumnsConfig);
      element11.Name = "Area";
      element11.HeaderText = "Area";
      element11.ResourceClassId = typeof (PageResources).Name;
      element11.HeaderCssClass = "sfShort";
      element11.ItemCssClass = "sfShort";
      element11.ClientTemplate = "<strong>{{AreaName}}:</strong>";
      element11.SortExpression = "AreaName";
      columnsConfig1.Add((ColumnElement) element11);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element10.ColumnsConfig;
      DataColumnElement element12 = new DataColumnElement((ConfigElement) element10.ColumnsConfig);
      element12.Name = "Name";
      element12.HeaderText = "Name";
      element12.ResourceClassId = typeof (PageResources).Name;
      element12.HeaderCssClass = "sfTitleCol";
      element12.ItemCssClass = "sfTitleCol";
      element12.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class='sf_binderCommand_edit sfItemTitle sfavailable'><strong>{{Name}}</strong></a>";
      element12.DisableSorting = new bool?(false);
      columnsConfig2.Add((ColumnElement) element12);
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) element10.ColumnsConfig);
      menuColumnElement.Name = "ActionsLinkText";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.TitleText = "ActionsLinkText";
      menuColumnElement.ResourceClassId = typeof (PageResources).Name;
      ActionMenuColumnElement element13 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element13.MenuItems;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element13.MenuItems);
      element14.Name = "Delete";
      element14.WrapperTagKey = HtmlTextWriterTag.Li;
      element14.CommandName = "delete";
      element14.Text = "Delete";
      element14.ResourceClassId = typeof (Labels).Name;
      element14.WidgetType = typeof (CommandWidget);
      element14.CssClass = "sfDeleteItm";
      menuItems1.Add((WidgetElement) element14);
      ConfigElementList<WidgetElement> menuItems2 = element13.MenuItems;
      MultisiteCommandWidgetElement element15 = new MultisiteCommandWidgetElement((ConfigElement) element13.MenuItems);
      element15.Name = "ShareWith";
      element15.WrapperTagKey = HtmlTextWriterTag.Li;
      element15.CommandName = "shareWith";
      element15.Text = "ShareWith";
      element15.ResourceClassId = typeof (Labels).Name;
      element15.WidgetType = typeof (CommandWidget);
      element15.ModuleName = "MultisiteInternal";
      element15.CssClass = "sfShareLinkItm";
      menuItems2.Add((WidgetElement) element15);
      ConfigElementList<WidgetElement> menuItems3 = element13.MenuItems;
      CommandWidgetElement element16 = new CommandWidgetElement((ConfigElement) element13.MenuItems);
      element16.Name = "historygrid";
      element16.Text = "HistoryMenuItemTitle";
      element16.CommandName = "historygrid";
      element16.ResourceClassId = typeof (VersionResources).Name;
      element16.WidgetType = typeof (CommandWidget);
      menuItems3.Add((WidgetElement) element16);
      element10.ColumnsConfig.Add((ColumnElement) element13);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element10.ColumnsConfig;
      DataColumnElement element17 = new DataColumnElement((ConfigElement) element10.ColumnsConfig);
      element17.Name = "FriendlyUserName";
      element17.HeaderText = "AppliedTo";
      element17.ResourceClassId = typeof (ControlTemplatesResources).Name;
      element17.ClientTemplate = "<span>{{FriendlyUserName}}</span>";
      element17.DisableSorting = new bool?(false);
      element17.ItemCssClass = "sfMedium";
      columnsConfig3.Add((ColumnElement) element17);
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element10.ColumnsConfig;
      MultisiteDataColumnElement element18 = new MultisiteDataColumnElement((ConfigElement) element10.ColumnsConfig);
      element18.Name = "SiteUsage";
      element18.HeaderText = "";
      element18.ClientTemplate = string.Format("<a href='javascript:void(0);' sys:class='sf_binderCommand_widgetTemplateSites' title='{0}'>{1}</a>", (object) Res.Get<PageResources>().ClickToViewWhichSitesUseThisTemplate, (object) "{{(SiteLinksString)}}");
      element18.ModuleName = "MultisiteInternal";
      columnsConfig4.Add((ColumnElement) element18);
      ConfigElementDictionary<string, ColumnElement> columnsConfig5 = element10.ColumnsConfig;
      DataColumnElement element19 = new DataColumnElement((ConfigElement) element10.ColumnsConfig);
      element19.Name = "Date";
      element19.HeaderText = "Date";
      element19.ResourceClassId = typeof (ControlTemplatesResources).Name;
      element19.ClientTemplate = "<span>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy') : '-' }}</span>";
      element19.DisableSorting = new bool?(false);
      element19.ItemCssClass = "sfDate";
      columnsConfig5.Add((ColumnElement) element19);
      ConfigElementDictionary<string, ColumnElement> columnsConfig6 = element10.ColumnsConfig;
      DataColumnElement element20 = new DataColumnElement((ConfigElement) element10.ColumnsConfig);
      element20.Name = "Owner";
      element20.HeaderText = "Owner";
      element20.ResourceClassId = typeof (ControlTemplatesResources).Name;
      element20.ClientTemplate = "<span>{{Owner}}</span>";
      element20.ItemCssClass = "sfRegular";
      columnsConfig6.Add((ColumnElement) element20);
      DecisionScreenElement element21 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoControlTemplates",
        ResourceClassId = ControlTemplatesModule.ResourceClassId
      };
      ConfigElementList<CommandWidgetElement> actions = element21.Actions;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) element21.Actions);
      element22.Name = "CreateTemplate";
      element22.ButtonType = CommandButtonType.Create;
      element22.CommandName = "create";
      element22.Text = "CreateATemplate";
      element22.ResourceClassId = ControlTemplatesModule.ResourceClassId;
      element22.CssClass = "sfCreateItem";
      element22.PermissionSet = "General";
      element22.ActionName = "Create";
      actions.Add(element22);
      masterGridViewElement.DecisionScreensConfig.Add(element21);
      string parameters1 = string.Format("?ViewName={0}", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      definitionFacade.AddDialog<ControlTemplateEditor>("create").SetParameters(parameters1).MakeFullScreen();
      string parameters2 = string.Format("?ViewName={0}", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      definitionFacade.AddDialog<ControlTemplateEditor>("edit").SetParameters(parameters2).MakeFullScreen();
      definitionFacade.AddDialog<SitesUsageDialog>("widgetTemplateSites").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters(string.Format("?itemType={0}&itemId={1}&title={2}&description={3}&resourceClassId={4}&titleSingular={5}&managerType={6}", (object) typeof (ControlPresentation).FullName, (object) "{{Id}}", (object) "TemplateUsageTitle", (object) "TemplateUsageDescription", (object) typeof (MultisiteResources).Name, (object) "TemplateUsageTitleSingular", (object) typeof (PageManager).FullName)).SetModuleName("MultisiteInternal").BlackList().Done();
      string absolute = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/ControlTemplates/ControlTemplateService.svc/");
      definitionFacade.AddDialog<ShareItemDialog>("shareWith").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters(string.Format("?itemId={0}&title={1}&resourceClassId={2}&getSharedSitesUrl={3}&setSharedSitesUrl={4}", (object) "{{Id}}", (object) "ShareTemplateTitle", (object) typeof (MultisiteResources).Name, (object) HttpUtility.UrlEncode(absolute + "sitelinks/{0}/?sortExpression=Name"), (object) HttpUtility.UrlEncode(absolute + "savesitelinks/{0}/"))).SetModuleName("MultisiteInternal").BlackList().Done();
      string parameters3 = "?EditDialogCommand=" + "history" + "&ControlDefinitionName=" + ControlTemplatesDefinitions.BackendDefinitionName + "&moduleName=" + "ControlTemplates" + "&typeName=" + typeof (ControlPresentation).AssemblyQualifiedName + "&title=" + "dumi Title Revision History" + "&backLabelText=" + Res.Get<ControlTemplatesResources>().BackToItems + "&VersionComparisonView=ControlTemplatesBackendVersionComparisonView";
      definitionFacade.AddDialog<VersionHistoryDialog>("historygrid").SetParameters(parameters3).MakeFullScreen().Done();
      string parameters4 = string.Format("?ViewName={0}&versionHistory=true", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      definitionFacade.AddDialog<ControlTemplateVersionReview>("versionPreview").SetParameters(parameters4).MakeFullScreen();
      return (ConfigElement) masterGridViewElement;
    }

    private static ConfigElement DefineNewsBackendVersioningComparisonView(
      ContentViewControlElement backendContentView)
    {
      ComparisonViewElement comparisonViewElement1 = new ComparisonViewElement((ConfigElement) backendContentView.ViewsConfig);
      comparisonViewElement1.Title = "VersionComparison";
      comparisonViewElement1.ViewName = "ControlTemplatesBackendVersionComparisonView";
      comparisonViewElement1.ViewType = typeof (Telerik.Sitefinity.Versioning.Web.UI.Views.VersionComparisonView);
      comparisonViewElement1.DisplayMode = FieldDisplayMode.Read;
      comparisonViewElement1.ResourceClassId = typeof (ControlTemplatesResources).Name;
      comparisonViewElement1.UseWorkflow = new bool?(false);
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      ConfigElementDictionary<string, ComparisonFieldElement> fields1 = comparisonViewElement2.Fields;
      ComparisonFieldElement element1 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element1.IsHtmlEnchancedField = true;
      element1.IncludeInDetails = true;
      element1.FieldName = "Data";
      element1.Title = "Data";
      element1.ResourceClassId = typeof (ControlTemplatesResources).Name;
      fields1.Add(element1);
      ConfigElementDictionary<string, ComparisonFieldElement> fields2 = comparisonViewElement2.Fields;
      ComparisonFieldElement element2 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element2.FieldName = "Name";
      element2.Title = "Name";
      element2.ResourceClassId = typeof (ControlTemplatesResources).Name;
      fields2.Add(element2);
      ConfigElementDictionary<string, ComparisonFieldElement> fields3 = comparisonViewElement2.Fields;
      ComparisonFieldElement element3 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element3.FieldName = "AreaName";
      element3.Title = "AreaName";
      element3.ResourceClassId = typeof (ControlTemplatesResources).Name;
      fields3.Add(element3);
      ConfigElementDictionary<string, ComparisonFieldElement> fields4 = comparisonViewElement2.Fields;
      ComparisonFieldElement element4 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element4.FieldName = "NameForDevelopers";
      element4.Title = "NameForDevelopers";
      element4.ResourceClassId = typeof (ControlTemplatesResources).Name;
      fields4.Add(element4);
      return (ConfigElement) comparisonViewElement2;
    }
  }
}
