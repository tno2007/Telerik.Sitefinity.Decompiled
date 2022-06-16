// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.FlatTaxonDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.UI;
using Telerik.Sitefinity.Taxonomies.Web.UI.Flat;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.Taxonomies
{
  internal static class FlatTaxonDefinitions
  {
    public static readonly string ResourceClassId = typeof (TaxonomyResources).Name;
    public static readonly string LabelsResClassId = typeof (Labels).Name;
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for classifications on the backend.
    /// </summary>
    public const string BackendDefinitionName = "FlatTaxonBackend";
    /// <summary>
    /// Name of the view used to display classifications as a list
    /// </summary>
    public const string BackendListViewName = "FlatTaxonBackendList";
    /// <summary>
    /// Name of the view used to edit a classification on the backend.
    /// </summary>
    public const string BackendEditViewName = "FlatTaxonBackendEdit";
    /// <summary>
    /// Name of the view used to create a classification on the backend.
    /// </summary>
    public const string BackendInsertViewName = "FlatTaxonBackendInsert";

    /// <summary>
    /// Defines the ContentView control for Flat Taxons on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFlatTaxonsBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, "FlatTaxonBackend", typeof (FlatTaxon)).DoNotUseWorkflow().SetManagerType(typeof (TaxonomyManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) "FlatTaxonBackendList", (Func<ConfigElement>) (() => FlatTaxonDefinitions.DefineFlatTaxonsBackendListView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "FlatTaxonBackendEdit", (Func<ConfigElement>) (() => FlatTaxonDefinitions.DefineFlatTaxonBackendEditView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "FlatTaxonBackendInsert", (Func<ConfigElement>) (() => FlatTaxonDefinitions.DefineFlatTaxonBackendInsertView(fluentContentView)));
      return viewControlElement;
    }

    private static ConfigElement DefineFlatTaxonsBackendListView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.FlatTaxonListExtensions.js, Telerik.Sitefinity", "FlatTaxonListExtensions_ViewLoaded");
      MasterViewDefinitionFacade fluentFacade = fluentContentView.AddMasterView("FlatTaxonBackendList").SetViewType(typeof (TaxaMasterGridView)).SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc").SetSortExpression("Title ASC").SetExternalClientScripts(extenalClientScripts).SetCssClass("sfLetterMarked");
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
      masterGridViewElement.AddResourceString("Labels", "Items").AddResourceString("Labels", "Item");
      FlatTaxonDefinitions.DefineToolbar(masterGridViewElement);
      FlatTaxonDefinitions.DefineSidebar(masterGridViewElement);
      FlatTaxonDefinitions.DefineGridViewMode(masterGridViewElement);
      FlatTaxonDefinitions.DefineDesicionScreens(masterGridViewElement);
      FlatTaxonDefinitions.DefineDialogs(fluentFacade);
      FlatTaxonDefinitions.DefineLinks(masterGridViewElement);
      return (ConfigElement) masterGridViewElement;
    }

    private static ConfigElement DefineFlatTaxonBackendEditView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("FlatTaxonBackendEdit").SetTitle("EditATaxonName").SetAlternativeTitle("EditATaxonName").SetViewType(typeof (TaxonDetailFormView)).HideTopToolbar().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      FlatTaxonDefinitions.CreateBackendSections(detailView, fluentDetailView);
      FlatTaxonDefinitions.CreateBackendFormToolbar(detailView, false);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineFlatTaxonBackendInsertView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView("FlatTaxonBackendInsert").SetTitle("CreateATaxonName").SetAlternativeTitle("CreateATaxonName").SetViewType(typeof (TaxonDetailFormView)).HideTopToolbar().SetServiceBaseUrl("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/").DoNotUseContentItemContext();
      DetailFormViewElement detailView = fluentDetailView.Get();
      FlatTaxonDefinitions.CreateBackendSections(detailView, fluentDetailView);
      FlatTaxonDefinitions.CreateBackendFormToolbar(detailView, true);
      return (ConfigElement) detailView;
    }

    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      DetailViewDefinitionFacade fluentDetailView)
    {
      if (detailView.ViewName == "FlatTaxonBackendEdit")
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection").LocalizeUsing<TaxonomyResources>().AddTextField("Title").SetId("taxonTitle").SetExample("TaxonTitleExample").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("ClassificationCannotBeEmpty").Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = fluentDetailView.AddSection("secondTaxonSection").LocalizeUsing<TaxonomyResources>();
      definitionFacade2.AddTextField("Description").SetId("descriptionFieldControl").SetTitle("lDescription").SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").Done().Done();
      definitionFacade2.AddUrlNameField(definitionFacade1.Get().ID).SetCssClass("sfFormSeparator");
      RegexStrategy regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
      fluentDetailView.AddExpandableSection("advancedTaxonSection").SetTitle("AdvancedSynonymsNameInCode").LocalizeUsing<Labels>().AddTextField("Synonyms").SetId("synonymsFieldControl").SetTitle("Synonyms").SetCssClass("sfShortField250").LocalizeUsing<TaxonomyResources>().Done().AddMirrorTextField("DevelopersName", "Name").SetTitle("ForDevelopersNameUsedInCode").SetId("developersName").SetMirroredControlId(definitionFacade1.Get().ID).SetRegularExpressionFilter(regexStrategy.DefaultExpressionFilter).Trim().SetCssClass("sfFormSeparator").LocalizeUsing<TaxonomyResources>().AddValidation().MakeRequired().SetRegularExpression(regexStrategy.DefaultValidationExpression).SetRequiredViolationMessage("NameUsedInCodeCannotBeEmpty").SetRegularExpressionViolationMessage("DevNameInvalidSymbols");
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
      element2.ResourceClassId = FlatTaxonDefinitions.ResourceClassId;
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
        element3.ResourceClassId = FlatTaxonDefinitions.ResourceClassId;
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
      element4.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element4.IsSeparator = false;
      items3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items4 = element1.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.Items);
      element5.Name = "CancelButton";
      element5.ButtonType = CommandButtonType.Cancel;
      element5.CommandName = "cancel";
      element5.Text = "Cancel";
      element5.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
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
      element2.Name = "CreateFlatTaxon";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "create";
      element2.Text = "CreateATaxonName";
      element2.ResourceClassId = FlatTaxonDefinitions.ResourceClassId;
      element2.CssClass = "sfMainAction";
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "Taxonomies";
      element2.ActionName = "ModifyTaxonomyAndSubTaxons";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "DeleteFlatTaxon";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "groupDelete";
      element3.Text = "Delete";
      element3.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfGroupBtn";
      element3.PermissionSet = "Taxonomies";
      element3.ActionName = "ModifyTaxonomyAndSubTaxons";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "BulkEdit";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "bulkEdit";
      element4.Text = "TagsBulkEdit";
      element4.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element4.WidgetType = typeof (CommandWidget);
      element4.PermissionSet = "Taxonomies";
      element4.ActionName = "ModifyTaxonomyAndSubTaxons";
      items3.Add((WidgetElement) element4);
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (FlatTaxon), false, gridView.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (FlatTaxon), true, gridView.Section);
      DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement.Name = "EditCustomSorting";
      commandWidgetElement.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement.HeaderText = "Sort";
      commandWidgetElement.PageSize = 10;
      commandWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement.WidgetType = typeof (SortWidget);
      commandWidgetElement.ResourceClassId = FlatTaxonDefinitions.ResourceClassId;
      commandWidgetElement.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement.ContentType = typeof (FlatTaxon);
      DynamicCommandWidgetElement element5 = commandWidgetElement;
      element1.Items.Add((WidgetElement) element5);
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element5.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element5.Items.Add(dynamicItemElement);
        element5.DesignTimeItems.Add(dynamicItemElement.GetKey());
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element5.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element5.CustomItems.Add(dynamicItemElement);
      }
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
        Name = "Settings",
        CssClass = "sfFirst sfWidgetsList sfModules",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items2 = element3.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element3.Items);
      element4.Name = "Permissions";
      element4.CommandName = "permissions";
      element4.ButtonType = CommandButtonType.SimpleLinkButton;
      element4.Text = "Permissions";
      element4.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element4.WidgetType = typeof (CommandWidget);
      element4.IsSeparator = false;
      element4.CssClass = "sfSettings";
      items2.Add((WidgetElement) element4);
      WidgetBarSectionElement element5 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "StatusFilters",
        Title = "Filter",
        ResourceClassId = FlatTaxonDefinitions.LabelsResClassId,
        WrapperTagId = "StatusFiltersSection",
        CssClass = "sfWidgetsList"
      };
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) element5.Items);
      gridView.SidebarConfig.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      gridView.SidebarConfig.Title = "Properties";
      gridView.SidebarConfig.Sections.Add((WidgetBarSectionElement) element1);
      gridView.SidebarConfig.Sections.Add(element5);
      gridView.SidebarConfig.Sections.Add(element3);
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
      element2.HeaderText = "Title";
      element2.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element2.HeaderCssClass = "sfTitleCol";
      element2.ItemCssClass = "sfFlat sfTitleCol";
      element2.ClientTemplate = "\r\n                <a href='#' sys:class=\"{{'sf_binderCommand_edit sfItemTitle' + (AdditionalStatus ? ' sfDisplayBlock sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus' : '')}}\">\r\n                    {{Title}}\r\n                </a>\r\n                <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>  ";
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
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      ActionMenuColumnElement element3 = menuColumnElement;
      element3.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element3.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", FlatTaxonDefinitions.LabelsResClassId, "sfDeleteItm"));
      element3.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) element3.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", FlatTaxonDefinitions.LabelsResClassId));
      element3.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) element3.MenuItems, "Properties", HtmlTextWriterTag.Li, "edit", "Properties", FlatTaxonDefinitions.LabelsResClassId));
      element1.ColumnsConfig.Add((ColumnElement) element3);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element1.ColumnsConfig;
      DataColumnElement element4 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element4.Name = "UrlName";
      element4.HeaderText = "UrlName";
      element4.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element4.HeaderCssClass = "sfRegular";
      element4.ItemCssClass = "sfRegular";
      element4.ClientTemplate = "<span>{{UrlName}}</span>";
      columnsConfig2.Add((ColumnElement) element4);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element1.ColumnsConfig;
      DataColumnElement element5 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      element5.Name = "AppliedTo";
      element5.HeaderText = "AppliedTo";
      element5.ResourceClassId = FlatTaxonDefinitions.LabelsResClassId;
      element5.HeaderCssClass = "sfRegular sfNumeric";
      element5.ItemCssClass = "sfRegular sfNumeric";
      element5.ClientTemplate = "\r\n                <div>\r\n                    <a class='sf_binderCommand_viewMarkedItems' href='#'>{{ItemsCount}} {$Labels, Items$}</a>\r\n                </div>";
      columnsConfig3.Add((ColumnElement) element5);
    }

    private static void DefineDesicionScreens(MasterGridViewElement gridView)
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

    private static void DefineDialogs(MasterViewDefinitionFacade fluentFacade) => fluentFacade.AddInsertDialog("FlatTaxonBackendInsert").AddParameters("&TaxonomyId={{TaxonomyId}}").Done().AddEditDialog("FlatTaxonBackendEdit").AddParameters("&TaxonomyId={{TaxonomyId}}" + "&TaxonType=" + typeof (FlatTaxon).FullName).Done().AddDialog<FlatTaxaBulkEditForm>("bulkEdit").MakeFullScreen().Done().AddPermissionsDialog(Res.Get<TaxonomyResources>().BackToItems, "{{Title}}");

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
