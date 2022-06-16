// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.HierarchicalTaxonDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.UI;
using Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Config;
using Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Taxonomies
{
  internal static class HierarchicalTaxonDefinitions
  {
    public static readonly string ResourceClassId = typeof (TaxonomyResources).Name;
    public static readonly string LabelsResClassId = typeof (Labels).Name;
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for classifications on the backend.
    /// </summary>
    public const string BackendDefinitionName = "HierarchicalTaxonBackend";
    /// <summary>
    /// Name of the view used to display classifications as a list
    /// </summary>
    public const string BackendListViewName = "HierarchicalTaxonBackendList";
    /// <summary>
    /// Name of the view used to edit a classification on the backend.
    /// </summary>
    public const string BackendEditViewName = "HierarchicalTaxonBackendEdit";
    /// <summary>
    /// Name of the view used to create a classification on the backend.
    /// </summary>
    public const string BackendInsertViewName = "HierarchicalTaxonBackendInsert";

    /// <summary>
    /// Defines the ContentView control for Hierarchical Taxons on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineHierarchicalTaxonsBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "HierarchicalTaxonBackend", typeof (HierarchicalTaxon)).DoNotUseWorkflow().SetManagerType(typeof (TaxonomyManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) "HierarchicalTaxonBackendList", (Func<ConfigElement>) (() => HierarchicalTaxonDefinitions.DefineHierarchicalTaxonsBackendListView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "HierarchicalTaxonBackendEdit", (Func<ConfigElement>) (() => HierarchicalTaxonDefinitions.DefineHierarchicalTaxonBackendEditView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "HierarchicalTaxonBackendInsert", (Func<ConfigElement>) (() => HierarchicalTaxonDefinitions.DefineHierarchicalTaxonBackendInsertView(fluentContentView)));
      return viewControlElement;
    }

    private static ConfigElement DefineHierarchicalTaxonsBackendListView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.HierarchicalTaxonTreeModeExtensions.js, Telerik.Sitefinity", "HierarchicalTaxonTreeModeExtensions_ViewLoaded");
      MasterViewDefinitionFacade fluentFacade = fluentContentView.AddMasterView("HierarchicalTaxonBackendList").SetViewType(typeof (TaxaMasterGridView)).SetSortExpression("Ordinal").SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc").SetExternalClientScripts(extenalClientScripts).SetCssClass("sfCategoriesTreeview");
      MasterGridViewElement masterGridViewElement = fluentFacade.Get();
      ConfigElementList<WidgetElement> titleWidgetsConfig = masterGridViewElement.TitleWidgetsConfig;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) masterGridViewElement.TitleWidgetsConfig);
      element.Name = "BackToTaxonomies";
      element.CommandName = "backTo";
      element.ButtonType = CommandButtonType.SimpleLinkButton;
      element.WidgetType = typeof (CommandWidget);
      element.Text = "AllClassifications";
      element.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
      element.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element);
      masterGridViewElement.AddResourceString("Labels", "AreYouSureYouWantToDeleteSelectedItems").AddResourceString("Labels", "Items");
      HierarchicalTaxonDefinitions.DefineToolbar(masterGridViewElement);
      HierarchicalTaxonDefinitions.DefineSidebar(masterGridViewElement);
      HierarchicalTaxonDefinitions.DefineTreeViewMode(masterGridViewElement);
      HierarchicalTaxonDefinitions.DefineDecisionScreens(masterGridViewElement);
      HierarchicalTaxonDefinitions.DefineDialogs(fluentFacade);
      HierarchicalTaxonDefinitions.DefineLinks(masterGridViewElement);
      return (ConfigElement) masterGridViewElement;
    }

    private static void DefineDecisionScreens(MasterGridViewElement gridView)
    {
      DecisionScreenElement element1 = new DecisionScreenElement((ConfigElement) gridView.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow"
      };
      CommandWidgetElement commandWidgetElement = new CommandWidgetElement((ConfigElement) element1.Actions);
      commandWidgetElement.Name = "Create";
      commandWidgetElement.ButtonType = CommandButtonType.Create;
      commandWidgetElement.CommandName = "create";
      commandWidgetElement.CssClass = "sfCreateItem";
      commandWidgetElement.PermissionSet = "Taxonomies";
      commandWidgetElement.ActionName = "ModifyTaxonomyAndSubTaxons";
      CommandWidgetElement element2 = commandWidgetElement;
      element1.Actions.Add(element2);
      gridView.DecisionScreensConfig.Add(element1);
    }

    private static ConfigElement DefineHierarchicalTaxonBackendEditView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("HierarchicalTaxonBackendEdit").SetTitle("EditATaxonName").SetAlternativeTitle("EditATaxonName").SetViewType(typeof (TaxonDetailFormView)).HideTopToolbar().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      HierarchicalTaxonDefinitions.CreateBackendSections(detailView, fluentDetailView);
      HierarchicalTaxonDefinitions.CreateBackendFormToolbar(detailView, false);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineHierarchicalTaxonBackendInsertView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("HierarchicalTaxonBackendInsert").SetTitle("CreateATaxonName").SetAlternativeTitle("CreateATaxonName").SetViewType(typeof (TaxonDetailFormView)).HideTopToolbar().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      HierarchicalTaxonDefinitions.CreateBackendSections(detailView, fluentDetailView);
      HierarchicalTaxonDefinitions.CreateBackendFormToolbar(detailView, true);
      return (ConfigElement) detailView;
    }

    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      DetailViewDefinitionFacade fluentDetailView)
    {
      if (detailView.ViewName == "HierarchicalTaxonBackendEdit")
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade = fluentDetailView.AddFirstSection("MainSection").LocalizeUsing<TaxonomyResources>();
      ContentViewSectionElement viewSectionElement = definitionFacade.Get();
      definitionFacade.AddTextField("Title").SetId("Title").SetExample("FlatTaxonTitleExample").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done().Done();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      HierarchicalTaxonParentSelectorFieldElement element = new HierarchicalTaxonParentSelectorFieldElement((ConfigElement) viewSectionElement.Fields);
      element.ID = "parentSelectorField";
      element.DataFieldName = "ParentTaxonId";
      element.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
      element.WebServiceBaseUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/";
      element.WrapperTag = HtmlTextWriterTag.Li;
      element.CssClass = "sfPageHierarchy sfFormSeparator";
      element.TaxonomyId = Guid.Empty;
      element.ExpandableDefinitionConfig.Expanded = new bool?(true);
      fields.Add((FieldDefinitionElement) element);
      RegexStrategy regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
      fluentDetailView.AddSection("DescSection").SetCssClass("sfForm").LocalizeUsing<Labels>().AddTextField("Description").SetId("descriptionFieldControl").SetTitle("Description").SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").Done().Done().AddMirrorTextField("UrlName").SetId("UrlName").Trim().ToLower().SetRegularExpressionFilter(regexStrategy.DefaultExpressionFilter).SetReplaceWithValue("-").SetCssClass("sfFormSeparator").SetMirroredControlId("Title").SetTitle("UrlName").AddExpandableBehavior().Expand().Done().AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(regexStrategy.DefaultValidationExpression).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").LocalizeUsing<TaxonomyResources>();
      fluentDetailView.AddSection("AdvancedSection").SetTitle("AdvancedSynonymsNameInCode").LocalizeUsing<Labels>().SetCssClass("sfForm sfExpandableForm").AddExpandableBehavior().Collapse().Done().AddTextField("Synonyms").SetId("taxonSynonyms").SetTitle("Synonyms").LocalizeUsing<TaxonomyResources>().AddExpandableBehavior().Expand().SetExpandText("ClickToAddSynonyms").Done().Done().AddMirrorTextField("Name").SetId("Name").Trim().SetMirroredControlId("Title").SetRegularExpressionFilter(regexStrategy.DefaultExpressionFilter).SetTitle("ForDevelopersNameUsedInCode").LocalizeUsing<TaxonomyResources>().AddExpandableBehavior().Expand().SetExpandText("ClickToAddNameUsedInCode").Done().AddValidation().MakeRequired().SetRequiredViolationMessage("NameUsedInCodeCannotBeEmpty").SetRegularExpression(regexStrategy.DefaultValidationExpression).SetRegularExpressionViolationMessage("DevNameInvalidSymbols");
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
      element2.Name = "SaveButton";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = isCreateMode ? "CreateThisTaxonName" : "SaveThisTaxonName";
      element2.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items2 = element1.Items;
        CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
        element3.Name = "SaveAndContinueButton";
        element3.ButtonType = CommandButtonType.SaveAndContinue;
        element3.CommandName = "saveAndContinue";
        element3.Text = "CreateAndAddAnotherTaxonName";
        element3.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
        element3.WrapperTagKey = HtmlTextWriterTag.Span;
        element3.WidgetType = typeof (CommandWidget);
        items2.Add((WidgetElement) element3);
      }
      ConfigElementList<WidgetElement> items3 = element1.Items;
      LiteralWidgetElement element4 = new LiteralWidgetElement((ConfigElement) element1.Items);
      element4.Name = "orLiteral";
      element4.WidgetType = typeof (LiteralWidget);
      element4.Text = "or";
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.ResourceClassId = HierarchicalTaxonDefinitions.LabelsResClassId;
      element4.IsSeparator = false;
      items3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items4 = element1.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.Items);
      element5.Name = "CancelButton";
      element5.ButtonType = CommandButtonType.Cancel;
      element5.CommandName = "cancel";
      element5.Text = "Cancel";
      element5.ResourceClassId = HierarchicalTaxonDefinitions.LabelsResClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Span;
      element5.WidgetType = typeof (CommandWidget);
      items4.Add((WidgetElement) element5);
      detailView.Toolbar.Sections.Add(element1);
    }

    private static void DefineToolbar(MasterGridViewElement gridView)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "CreateHierarchicalTaxon";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "create";
      element2.Text = "CreateATaxonName";
      element2.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
      element2.CssClass = "sfMainAction";
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "Taxonomies";
      element2.ActionName = "ModifyTaxonomyAndSubTaxons";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "DeleteHierarchicalTaxon";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "groupDelete";
      element3.Text = "Delete";
      element3.ResourceClassId = "Labels";
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfGroupBtn";
      element3.PermissionSet = "Taxonomies";
      element3.ActionName = "ModifyTaxonomyAndSubTaxons";
      items2.Add((WidgetElement) element3);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "Move";
      menuWidgetElement.Text = "Move";
      menuWidgetElement.ResourceClassId = "Labels";
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element4 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element4.MenuItems;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element5.Name = "BatchMoveUp";
      element5.Text = "Up";
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.CommandName = "batchMoveUp";
      element5.WidgetType = typeof (CommandWidget);
      element5.ResourceClassId = "Labels";
      element5.CssClass = "sfMoveUp";
      element5.PermissionSet = "Taxonomies";
      element5.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems1.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems2 = element4.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element6.Name = "BatchMoveDown";
      element6.Text = "Down";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "batchMoveDown";
      element6.WidgetType = typeof (CommandWidget);
      element6.ResourceClassId = "Labels";
      element6.CssClass = "sfMoveDown";
      element6.PermissionSet = "Taxonomies";
      element6.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems2.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems3 = element4.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element7.Name = "BatchChangeParent";
      element7.Text = "ChangeParent";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "batchChangeParent";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = "TaxonomyResources";
      element7.CssClass = "sfCreateChild";
      element7.PermissionSet = "Taxonomies";
      element7.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems3.Add((WidgetElement) element7);
      element1.Items.Add((WidgetElement) element4);
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (HierarchicalTaxon), false, gridView.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (HierarchicalTaxon), true, gridView.Section);
      DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement.Name = "EditCustomSorting";
      commandWidgetElement.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement.HeaderText = "Sort";
      commandWidgetElement.PageSize = 10;
      commandWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement.WidgetType = typeof (SortWidget);
      commandWidgetElement.ResourceClassId = HierarchicalTaxonDefinitions.ResourceClassId;
      commandWidgetElement.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement.ContentType = typeof (HierarchicalTaxon);
      DynamicCommandWidgetElement element8 = commandWidgetElement;
      element1.Items.Add((WidgetElement) element8);
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element8.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element8.Items.Add(dynamicItemElement);
        element8.DesignTimeItems.Add(dynamicItemElement.GetKey());
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element8.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element8.CustomItems.Add(dynamicItemElement);
      }
      gridView.ToolbarConfig.Sections.Add(element1);
    }

    private static void DefineSidebar(MasterGridViewElement gridView)
    {
      gridView.SidebarConfig.ResourceClassId = "Labels";
      gridView.SidebarConfig.Title = "Properties";
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
      gridView.SidebarConfig.Sections.Add((WidgetBarSectionElement) element1);
      WidgetBarSectionElement element3 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "StatusFilters",
        Title = "Filter",
        ResourceClassId = FlatTaxonDefinitions.LabelsResClassId,
        WrapperTagId = "StatusFiltersSection",
        CssClass = "sfWidgetsList"
      };
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element3.Items);
      gridView.SidebarConfig.Sections.Add(element3);
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "Properties",
        Title = " ",
        ResourceClassId = "",
        CssClass = "sfFirst sfWidgetsList sfModules"
      };
      ConfigElementList<WidgetElement> items2 = element4.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.Items);
      element5.Name = "Permissions";
      element5.CommandName = "permissions";
      element5.ButtonType = CommandButtonType.SimpleLinkButton;
      element5.Text = "Permissions";
      element5.ResourceClassId = HierarchicalTaxonDefinitions.LabelsResClassId;
      element5.WidgetType = typeof (CommandWidget);
      element5.IsSeparator = false;
      element5.CssClass = "sfSettings";
      items2.Add((WidgetElement) element5);
      gridView.SidebarConfig.Sections.Add(element4);
    }

    private static void DefineTreeViewMode(MasterGridViewElement gridView)
    {
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) gridView.ViewModesConfig);
      gridViewModeElement.Name = "TreeTable";
      gridViewModeElement.EnableDragAndDrop = new bool?(false);
      GridViewModeElement element1 = gridViewModeElement;
      gridView.GridCssClass = "sfMultilingualTreeview sfCategoriesTreeview";
      gridView.ViewModesConfig.Add((ViewModeElement) element1);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element1.ColumnsConfig;
      DataColumnElement element2 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element2.Name = "Title";
      element2.HeaderText = "Title";
      element2.ResourceClassId = "Labels";
      element2.HeaderCssClass = "sfTitleCol";
      element2.ItemCssClass = "sfTitleCol";
      element2.ClientTemplate = "<div>\r\n                    <a href=\"javascript:void(0)\" sys:class=\"{{'sf_binderCommand_edit sfItemTitle' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus' : '')}}\"><strong>{{Title.htmlEncode()}}</strong><span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span></a>\r\n                </div>";
      columnsConfig1.Add((ColumnElement) element2);
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
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) element1.ColumnsConfig);
      menuColumnElement.Name = "Actions";
      menuColumnElement.HeaderText = "Actions";
      menuColumnElement.ResourceClassId = "Labels";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.Width = 80;
      ActionMenuColumnElement element3 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element3.MenuItems;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element4.Name = "Delete";
      element4.WrapperTagKey = HtmlTextWriterTag.Li;
      element4.CommandName = "delete";
      element4.Text = "Delete";
      element4.ResourceClassId = "Labels";
      element4.CssClass = "sfDeleteItm";
      element4.WidgetType = typeof (CommandWidget);
      element4.PermissionSet = "Taxonomies";
      element4.ActionName = "DeleteTaxonomy";
      menuItems1.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> menuItems2 = element3.MenuItems;
      LiteralWidgetElement element5 = new LiteralWidgetElement((ConfigElement) element3.MenuItems);
      element5.Name = "Separator";
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.WidgetType = typeof (LiteralWidget);
      element5.IsSeparator = true;
      menuItems2.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems3 = element3.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element6.Name = "CreateChild";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "createChild";
      element6.Text = "ChildCategory";
      element6.ResourceClassId = "TaxonomyResources";
      element6.CssClass = "sfCreateChild";
      element6.WidgetType = typeof (CommandWidget);
      element6.PermissionSet = "Taxonomies";
      element6.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems3.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems4 = element3.MenuItems;
      LiteralWidgetElement element7 = new LiteralWidgetElement((ConfigElement) element3.MenuItems);
      element7.Name = "Move";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.Text = "MoveEllipsis";
      element7.ResourceClassId = "Labels";
      element7.WidgetType = typeof (LiteralWidget);
      element7.IsSeparator = true;
      menuItems4.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems5 = element3.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element8.Name = "MoveUp";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "moveUp";
      element8.Text = "Up";
      element8.ResourceClassId = "Labels";
      element8.CssClass = "sfMoveUp";
      element8.WidgetType = typeof (CommandWidget);
      element8.PermissionSet = "Taxonomies";
      element8.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems5.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems6 = element3.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element9.Name = "MoveDown";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "moveDown";
      element9.Text = "Down";
      element9.ResourceClassId = "Labels";
      element9.CssClass = "sfMoveDown";
      element9.WidgetType = typeof (CommandWidget);
      element9.PermissionSet = "Taxonomies";
      element9.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems6.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems7 = element3.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element10.Name = "ChangeParent";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "changeParent";
      element10.Text = "ChangeParent";
      element10.ResourceClassId = "TaxonomyResources";
      element10.CssClass = "sfCreateChild";
      element10.WidgetType = typeof (CommandWidget);
      element10.PermissionSet = "Taxonomies";
      element10.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems7.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems8 = element3.MenuItems;
      LiteralWidgetElement element11 = new LiteralWidgetElement((ConfigElement) element3.MenuItems);
      element11.Name = "EditSeparator";
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.Text = "EditEllipsis";
      element11.ResourceClassId = "Labels";
      element11.WidgetType = typeof (LiteralWidget);
      element11.IsSeparator = true;
      menuItems8.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems9 = element3.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element12.Name = "EditProperties";
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.CommandName = "edit";
      element12.Text = "Properties";
      element12.ResourceClassId = "Labels";
      element12.CssClass = "";
      element12.WidgetType = typeof (CommandWidget);
      element12.PermissionSet = "Taxonomies";
      element12.ActionName = "ModifyTaxonomyAndSubTaxons";
      menuItems9.Add((WidgetElement) element12);
      element1.ColumnsConfig.Add((ColumnElement) element3);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element1.ColumnsConfig;
      DataColumnElement element13 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element13.Name = "UrlName";
      element13.HeaderText = "Url";
      element13.ResourceClassId = "Labels";
      element13.DisableSorting = new bool?(true);
      element13.Width = 150;
      element13.HeaderCssClass = "sfUrl";
      element13.ItemCssClass = "sfUrl";
      element13.ClientTemplate = "<div>{{UrlName}}&nbsp;</div>";
      columnsConfig2.Add((ColumnElement) element13);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element1.ColumnsConfig;
      DataColumnElement element14 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element14.Name = "AppliedTo";
      element14.HeaderText = "AppliedTo";
      element14.ResourceClassId = "Labels";
      element14.DisableSorting = new bool?(true);
      element14.Width = 100;
      element14.HeaderCssClass = "sfNumeric";
      element14.ItemCssClass = "sfNumeric";
      element14.ClientTemplate = "<div>\r\n                    <a class=\"sf_binderCommand_viewMarkedItems\" href=\"javascript:void(0)\">{{ItemsCount}} {$Labels, Items$}</a>\r\n                </div>";
      columnsConfig3.Add((ColumnElement) element14);
    }

    private static void DefineDialogs(MasterViewDefinitionFacade fluentFacade) => fluentFacade.AddInsertDialog("HierarchicalTaxonBackendInsert").AddParameters("&TaxonomyId={{TaxonomyId}}&Ordinal={{Ordinal}}" + "&TaxonType=" + typeof (HierarchicalTaxon).FullName).Done().AddEditDialog("HierarchicalTaxonBackendEdit").AddParameters("&TaxonId={{Id}}&TaxonomyId={{TaxonomyId}}&Ordinal={{Ordinal}}" + "&TaxonType=" + typeof (HierarchicalTaxon).FullName).Done().AddDialog<ChangeParentDialog>("changeParent").MakeModal().SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth((Unit) 425).SetHeight((Unit) 250).Done().AddInsertDialog("HierarchicalTaxonBackendInsert", "", "", "", (Type) null, "createChild").AddParameters("&TaxonomyId={{TaxonomyId}}&Ordinal={{Ordinal}}&TaxonId={{Id}}" + "&TaxonType=" + typeof (HierarchicalTaxon).FullName).Done().AddPermissionsDialog();

    private static void DefineLinks(MasterGridViewElement gridView)
    {
      gridView.LinksConfig.Add(new LinkElement((ConfigElement) gridView.LinksConfig)
      {
        Name = "viewSettings",
        CommandName = "viewMarkedItems",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.MarkedItemsPageId) + "/{{UrlPath}}"
      });
      gridView.LinksConfig.Add(new LinkElement((ConfigElement) gridView.LinksConfig)
      {
        Name = "BackTo",
        CommandName = "backTo",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.TaxonomiesNodeId)
      });
    }
  }
}
