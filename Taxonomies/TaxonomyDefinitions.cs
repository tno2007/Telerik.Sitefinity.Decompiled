// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Taxonomies
{
  internal static class TaxonomyDefinitions
  {
    public static readonly string ResourceClassId = typeof (TaxonomyResources).Name;
    public static readonly string LabelsResClassId = typeof (Labels).Name;
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for classifications on the backend.
    /// </summary>
    public const string BackendDefinitionName = "TaxonomyBackend";
    /// <summary>
    /// Name of the view used to display classifications as a list
    /// </summary>
    public const string BackendListViewName = "TaxonomyBackendList";
    /// <summary>
    /// Name of the view used to edit a classification on the backend.
    /// </summary>
    public const string BackendEditViewName = "TaxonomyBackendEdit";
    /// <summary>
    /// Name of the view used to create a classification on the backend.
    /// </summary>
    public const string BackendInsertViewName = "TaxonomyBackendInsert";

    /// <summary>
    /// Defines the ContentView control for Taxonomies on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineTaxonomiesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "TaxonomyBackend", typeof (Taxonomy)).DoNotUseWorkflow().SetManagerType(typeof (TaxonomyManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) "TaxonomyBackendList", (Func<ConfigElement>) (() => TaxonomyDefinitions.DefineTaxonomiesBackendListView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "TaxonomyBackendEdit", (Func<ConfigElement>) (() => TaxonomyDefinitions.DefineTaxonomyBackendEditView(fluentContentView, false)));
      viewControlElement.ViewsConfig.AddLazy((object) "TaxonomyBackendInsert", (Func<ConfigElement>) (() => TaxonomyDefinitions.DefineTaxonomyBackendInsertView(fluentContentView, true)));
      return viewControlElement;
    }

    private static ConfigElement DefineTaxonomiesBackendListView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.BackendTaxonomyListExtensions.js, Telerik.Sitefinity", "BackendTaxonomyListExtensions_ViewLoaded");
      MasterViewDefinitionFacade fluentFacade = fluentContentView.AddMasterView("TaxonomyBackendList").LocalizeUsing<TaxonomyResources>().SetTitle("Classifications").SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc").SetSortExpression("Title asc").SetExternalClientScripts(extenalClientScripts).SetCssClass("sfListViewGrid");
      MasterGridViewElement masterGridViewElement = fluentFacade.Get();
      masterGridViewElement.AddResourceString("Labels", "NoItemsEmpty").AddResourceString("Labels", "MoreItem").AddResourceString("Labels", "MoreItems").AddResourceString("Labels", "AreYouSureYouWantToDeleteSelectedItems").AddResourceString("TaxonomyResources", "SearchClassifications").AddResourceString("TaxonomyResources", "CloseSearchClassifications").AddResourceString("TaxonomyResources", "BackToClassificationsName").AddResourceString("TaxonomyResources", "ThisSiteOnly").AddResourceString("TaxonomyResources", "NotUsed");
      TaxonomyDefinitions.DefineToolbar(masterGridViewElement);
      TaxonomyDefinitions.DefineSidebar(masterGridViewElement);
      TaxonomyDefinitions.DefineGridViewMode(masterGridViewElement);
      TaxonomyDefinitions.DefineDialogs(fluentFacade);
      return (ConfigElement) masterGridViewElement;
    }

    private static ConfigElement DefineTaxonomyBackendEditView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("TaxonomyBackendEdit").SetTitle("EditAClassificationName").SetAlternativeTitle("EditAClassificationName").HideTopToolbar().LocalizeUsing<TaxonomyResources>().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      TaxonomyDefinitions.CreateBackendSections(detailView, isCreateMode, fluentDetailView);
      TaxonomyDefinitions.CreateBackendFormToolbar(detailView, isCreateMode);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineTaxonomyBackendInsertView(
      ContentViewControlDefinitionFacade fluentContentView,
      bool isCreateMode)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("TaxonomyBackendInsert").SetTitle("CreateAClassificationName").SetAlternativeTitle("EditAClassificationName").HideTopToolbar().LocalizeUsing<TaxonomyResources>().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      TaxonomyDefinitions.CreateBackendSections(detailView, isCreateMode, fluentDetailView);
      TaxonomyDefinitions.CreateBackendFormToolbar(detailView, isCreateMode);
      return (ConfigElement) detailView;
    }

    private static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      bool isCreateMode)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "toolbarSection",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "SaveTaxonomyButton";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = isCreateMode ? "CreateThisClassificationName" : "SaveChanges";
      element2.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      LiteralWidgetElement element3 = new LiteralWidgetElement((ConfigElement) element1.Items);
      element3.Name = "orLiteral";
      element3.WidgetType = typeof (LiteralWidget);
      element3.Text = "or";
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      element3.IsSeparator = false;
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "CancelButton";
      element4.ButtonType = CommandButtonType.Cancel;
      element4.CommandName = "cancel";
      element4.Text = "Cancel";
      element4.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element4);
      detailView.Toolbar.Sections.Add(element1);
    }

    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      bool isCreateMode,
      DetailViewDefinitionFacade fluentDetailView)
    {
      if (detailView.ViewName == "TaxonomyBackendEdit")
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField().SetId("languageListField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade = fluentDetailView.AddFirstSection("MainSection");
      definitionFacade.AddTextField("Title").SetId("classificationTitle").SetTitle("Classification").SetExample("ClassificationTitleExample").AddValidation().MakeRequired().SetRequiredViolationMessage("ClassificationCannotBeEmpty").Done().Done().AddTextField("SingleItemName").SetId("singleItemName").SetTitle("SingleItemName").SetExample("ClassificationSingleItemExample").SetCssClass("sfFormSeparator").SetToolTipText("TaxonomySingleItemNameTooltipText").SetToolTipTitle("TaxonomySingleItemNameTooltipTitle").SetToolTipContent("TaxonomySingleItemNameTooltipText").AddValidation().MakeRequired().SetRequiredViolationMessage("ItemNameCannotBeEmpty").Done().Done().AddLanguageChoiceField("AvailableLanguages").SetCssClass("sfFormSeparator").Done();
      if (isCreateMode)
        definitionFacade.AddChoiceField("Type", RenderChoicesAs.RadioButtons).SetId("classificationTypeSelector").SetTitle("Type").SetDisplayMode(isCreateMode ? FieldDisplayMode.Write : FieldDisplayMode.Read).MakeMutuallyExclusive().SetWrapperTag(HtmlTextWriterTag.Li).SetCssClass("sfFormSeparator sfTypeOptions").AddChoiceAndContinue("FlatTaxonomyUserFriendlyWithExample", "FlatTaxonomy", "ClassificationSimpleListExample", true).AddChoiceAndContinue("HierarchicalTaxonomyUserFriendlyWithExample", "HierarchicalTaxonomy").Done();
      else
        definitionFacade.AddTextField("Type").SetId("classificationTypeField").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Type").SetDescription("TypeCannotBeChanged").LocalizeUsing<TaxonomyResources>().SetCssClass("sfFormSeparator").Done();
      fluentDetailView.AddSection("MoreOptionsSection").AddTextField("Description").SetId("classificationDescription").SetTitle("ClassificationDescription").SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<Labels>().Done().Done().AddMirrorTextField("Name").SetId("classificationName").SetTitle("NameUsedInCode").Trim().SetRegularExpressionFilter("[^\\w\\-\\_]+").SetCssClass("sfFormSeparator").SetMirroredControlId("classificationTitle").AddExpandableBehavior().Collapse().SetExpandText("ClickToAddNameUsedInCode").Done().AddValidation().MakeRequired().SetRegularExpressionViolationMessage("DevNameInvalidSymbols").SetRequiredViolationMessage("NameUsedInCodeCannotBeEmpty").SetRegularExpression("^[\\w\\-\\_]+$");
    }

    private static void DefineToolbar(MasterGridViewElement gridView)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "CreateClassification";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "create";
      element2.Text = "CreateAClassificationName";
      element2.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element2.CssClass = "sfMainAction";
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "Taxonomies";
      element2.ActionName = "CreateTaxonomy";
      items.Add((WidgetElement) element2);
      gridView.ToolbarConfig.Sections.Add(element1);
    }

    private static void DefineSidebar(MasterGridViewElement gridView)
    {
      LocalizationWidgetBarSectionElement barSectionElement = new LocalizationWidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections);
      barSectionElement.Name = "Languages";
      barSectionElement.Title = "Languages";
      barSectionElement.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element1 = barSectionElement;
      ConfigElementList<WidgetElement> items1 = element1.Items;
      LanguagesDropDownListWidgetElement element2 = new LanguagesDropDownListWidgetElement((ConfigElement) element1.Items);
      element2.Name = "Languages";
      element2.Text = "Languages";
      element2.ResourceClassId = typeof (LocalizationResources).Name;
      element2.WidgetType = typeof (LanguagesDropDownListWidget);
      element2.IsSeparator = false;
      element2.LanguageSource = LanguageSource.Frontend;
      element2.AddAllLanguagesOption = false;
      element2.CommandName = "changeLanguage";
      items1.Add((WidgetElement) element2);
      WidgetBarSectionElement element3 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "Filter",
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items2 = element3.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element3.Items);
      element4.Name = "allTaxonomiesFilter";
      element4.CommandName = "showAllItems";
      element4.ButtonType = CommandButtonType.SimpleLinkButton;
      element4.Text = "AllClassifications";
      element4.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element4.WidgetType = typeof (CommandWidget);
      element4.IsSeparator = false;
      element4.ButtonCssClass = "sfSel";
      items2.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items3 = element3.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element3.Items);
      element5.Name = "flatTaxonomiesFilter";
      element5.CommandName = "showFlatTaxonomies";
      element5.ButtonType = CommandButtonType.SimpleLinkButton;
      element5.Text = "SimpleLists";
      element5.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element5.WidgetType = typeof (CommandWidget);
      element5.IsSeparator = false;
      items3.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> items4 = element3.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element3.Items);
      element6.Name = "hierarchicalTaxonomiesFilter";
      element6.CommandName = "showHierarchicalTaxonomies";
      element6.ButtonType = CommandButtonType.SimpleLinkButton;
      element6.Text = "Hierarchicals";
      element6.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element6.WidgetType = typeof (CommandWidget);
      element6.IsSeparator = false;
      items4.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> items5 = element3.Items;
      MultisiteCommandWidgetElement element7 = new MultisiteCommandWidgetElement((ConfigElement) element3.Items);
      element7.Name = "unusedTaxonomiesFilter";
      element7.CommandName = "showNotUsedTaxonomies";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.Text = "NotUsedTaxonomies";
      element7.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      element7.ModuleName = "MultisiteInternal";
      items5.Add((WidgetElement) element7);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element3.Items);
      WidgetBarSectionElement element8 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "EditAlso",
        Title = "EditAlso",
        ResourceClassId = TaxonomyDefinitions.LabelsResClassId,
        CssClass = "sfWidgetsList",
        WrapperTagId = "editAlsoSection"
      };
      CommandWidgetElement commandWidgetElement = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement.Name = "ClassificationsPermissions";
      commandWidgetElement.CommandName = "permissions";
      commandWidgetElement.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement.Text = "PermissionsForAllClassifications";
      commandWidgetElement.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      commandWidgetElement.WidgetType = typeof (CommandWidget);
      commandWidgetElement.IsSeparator = false;
      commandWidgetElement.CssClass = "sfSettings";
      CommandWidgetElement element9 = commandWidgetElement;
      element8.Items.Add((WidgetElement) element9);
      gridView.SidebarConfig.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      gridView.SidebarConfig.Title = "FilterClassifications";
      gridView.SidebarConfig.Sections.Add((WidgetBarSectionElement) element1);
      gridView.SidebarConfig.Sections.Add(element3);
      gridView.SidebarConfig.Sections.Add(element8);
    }

    private static void DefineGridViewMode(MasterGridViewElement gridView)
    {
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) gridView.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element1 = gridViewModeElement;
      gridView.ViewModesConfig.Add((ViewModeElement) element1);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element1.ColumnsConfig;
      DataColumnElement element2 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element2.Name = "Title";
      element2.HeaderText = "ClassificationSlashType";
      element2.ResourceClassId = TaxonomyDefinitions.ResourceClassId;
      element2.HeaderCssClass = "sfTitleCol";
      element2.ItemCssClass = "sfTitleCol";
      element2.ClientTemplate = "\r\n                <a sys:href='{{EditUrl}}' sys:class=\"{{ 'sfItemTitle ' + CssClass + (AdditionalStatus ? ' sfWrapperLink sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus' : '')}}\">\r\n                    <span class='sfItemTitleIn'>{{Title}}</span>\r\n                    <span class='sfComplement'>{{UserFriendlyType}}</span><span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>\r\n                </a>";
      columnsConfig1.Add((ColumnElement) element2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element1.ColumnsConfig;
      DataColumnElement element3 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element3.Name = "SharedWith";
      element3.HeaderText = "SharedWith";
      element3.ResourceClassId = typeof (MultisiteResources).Name;
      element3.HeaderCssClass = "sfMedium";
      element3.ItemCssClass = "sfMedium";
      element3.ModuleName = "MultisiteInternal";
      element3.ClientTemplate = string.Format("<div id='showSites'><a href='javascript:void(0);' sys:class='sf_binderCommand_showSharedSites'>{0}</a></div>", (object) "{{(SharedSitesCount)}} {$MultisiteResources,SitesLower$}");
      columnsConfig2.Add((ColumnElement) element3);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element1.ColumnsConfig);
      dynamicColumnElement1.Name = "Translations";
      dynamicColumnElement1.HeaderText = "Translations";
      dynamicColumnElement1.ResourceClassId = typeof (LocalizationResources).Name;
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
      dynamicColumnElement1.ItemCssClass = "sfLanguagesCol";
      dynamicColumnElement1.HeaderCssClass = "sfLanguagesCol";
      DynamicColumnElement dynamicColumnElement2 = dynamicColumnElement1;
      dynamicColumnElement2.GeneratorSettingsElement = (DynamicMarkupGeneratorElement) new LanguagesColumnMarkupGeneratorElement((ConfigElement) dynamicColumnElement2)
      {
        LanguageSource = LanguageSource.Frontend,
        ItemsInGroupCount = 6,
        ContainerTag = "div",
        GroupTag = "div",
        ItemTag = "div",
        ContainerClass = string.Empty,
        GroupClass = string.Empty,
        ItemClass = string.Empty
      };
      element1.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element1.ColumnsConfig;
      DataColumnElement element4 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element4.Name = "Contains";
      element4.HeaderText = "Contains";
      element4.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      element4.HeaderCssClass = "sfMedium";
      element4.ItemCssClass = "sfMedium";
      element4.ClientTemplate = "<strong>{$Labels,Contains$}</strong><ul id='containsList' class='sfItemsInCategory'></ul>";
      columnsConfig3.Add((ColumnElement) element4);
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element1.ColumnsConfig;
      DataColumnElement element5 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element5.Name = "Description";
      element5.HeaderText = "Description";
      element5.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      element5.HeaderCssClass = "sfMedium";
      element5.ItemCssClass = "sfMedium";
      element5.ClientTemplate = "<p>{{Description}}&nbsp;</p>";
      columnsConfig4.Add((ColumnElement) element5);
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) element1.ColumnsConfig);
      menuColumnElement.Name = "Actions";
      menuColumnElement.HeaderText = "Actions";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      ActionMenuColumnElement element6 = menuColumnElement;
      element6.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element6.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", TaxonomyDefinitions.LabelsResClassId, "sfDeleteItm"));
      ConfigElementList<WidgetElement> menuItems = element6.MenuItems;
      MultisiteCommandWidgetElement element7 = new MultisiteCommandWidgetElement((ConfigElement) element6.MenuItems);
      element7.Name = "SetTaxonomyForThisSite";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "setTaxonomy";
      element7.Text = "SetTaxonomyForThisSite";
      element7.ResourceClassId = TaxonomyDefinitions.LabelsResClassId;
      element7.WidgetType = typeof (CommandWidget);
      element7.ModuleName = "MultisiteInternal";
      menuItems.Add((WidgetElement) element7);
      CommandWidgetElement actionMenuCommand = DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element6.MenuItems, "UseClassificationIn", HtmlTextWriterTag.Li, "useClassificationIn", "UseClassificationIn", TaxonomyDefinitions.LabelsResClassId);
      actionMenuCommand.ModuleName = "MultisiteInternal";
      element6.MenuItems.Add((WidgetElement) actionMenuCommand);
      element6.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) element6.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", TaxonomyDefinitions.LabelsResClassId));
      element6.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element6.MenuItems, "Properties", HtmlTextWriterTag.Li, "edit", "Properties", TaxonomyDefinitions.LabelsResClassId));
      element6.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element6.MenuItems, "Permissions", HtmlTextWriterTag.Li, "permissions", "Permissions", TaxonomyDefinitions.LabelsResClassId));
      element1.ColumnsConfig.Add((ColumnElement) element6);
    }

    private static void DefineDialogs(MasterViewDefinitionFacade fluentFacade) => fluentFacade.AddInsertDialog("TaxonomyBackendInsert").Done().AddEditDialog("TaxonomyBackendEdit").Done().AddDialog<SetTaxonomyDialog>("setTaxonomy").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters("?itemId={{Id}}&provider={{ProviderName}}&currentSiteTaxonomyId={{CurrentSiteTaxonomyId}}").BlackList().Done().AddDialog<TaxonomySitesUsageDialog>("showSharedSites").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters(string.Format("?itemType={0}&itemId={1}&title={2}&description={3}&resourceClassId={4}&titleSingular={5}&managerType={6}", (object) typeof (FlatTaxonomy).FullName, (object) "{{Id}}", (object) "TaxonomyUsedIn", (object) "", (object) typeof (MultisiteResources).Name, (object) "", (object) typeof (TaxonomyManager).FullName)).SetModuleName("MultisiteInternal").BlackList().Done().AddDialog<MultisiteTaxonomiesSiteSelectorDialog>("useClassificationIn").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().MakeModal().ReloadOnShow().SetParameters("?taxonomyId={{Id}}").SetModuleName("MultisiteInternal").BlackList().Done().AddPermissionsDialog(Res.Get<TaxonomyResources>().BackToClassificationsName, Res.Get<TaxonomyResources>().ModuleTitle, "", "", typeof (SecurityRoot)).AddParameters("&managerClassName=" + typeof (TaxonomyManager).FullName).AddParameters("&currentSiteTaxonomyId={{CurrentSiteTaxonomyId}}");

    public static string webServiceUrl { get; set; }
  }
}
