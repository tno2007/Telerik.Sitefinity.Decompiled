// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignDefinitions
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
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// This class provides the default backend UI definitions for the Responsive Design module.
  /// </summary>
  public static class ResponsiveDesignDefinitions
  {
    public const string RulesGroupDefinitionName = "RulesGroupBackend";
    public const string RulesGroupListViewName = "RulesGroupList";
    public const string RulesGroupInsertViewName = "RulesGroupInsert";
    public const string RulesGroupEditViewName = "RulesGroupEdit";

    /// <summary>
    /// Defines the ContentView control for Media Queries on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineMediaQueriesBackend(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("ResponsiveDesign").DefineContainer(parent, "RulesGroupBackend", typeof (MediaQuery));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) "RulesGroupList", (Func<ConfigElement>) (() => ResponsiveDesignDefinitions.DefineMediaQueriesListView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "RulesGroupInsert", (Func<ConfigElement>) (() => ResponsiveDesignDefinitions.DefineMediaQueriesBackendInsertView(fluentContentView)));
      viewControlElement.ViewsConfig.AddLazy((object) "RulesGroupEdit", (Func<ConfigElement>) (() => ResponsiveDesignDefinitions.DefineMediaQueriesBackendEditView(fluentContentView)));
      return viewControlElement;
    }

    private static ConfigElement DefineMediaQueriesListView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView("RulesGroupList").LocalizeUsing<ResponsiveDesignResources>().SetTitle("ResponsiveAndMobileDesign").SetCssClass("sfListViewGrid").DoNotUseWorkflow().SetServiceBaseUrl("~/Sitefinity/Services/ResponsiveDesign/MediaQuery.svc/");
      MasterGridViewElement cfg = definitionFacade.Get();
      cfg.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.ResponsiveDesignGridExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      };
      cfg.AddResourceString("ResponsiveDesignResources", "ResponsiveAndMobileDesignCompatibilityWarning");
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) cfg.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement.Name = "CreateAGroupOfRules";
      commandWidgetElement.ButtonType = CommandButtonType.Create;
      commandWidgetElement.CommandName = "create";
      commandWidgetElement.Text = "CreateAGroupOfRules";
      commandWidgetElement.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      commandWidgetElement.CssClass = "sfMainAction";
      commandWidgetElement.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element2 = commandWidgetElement;
      element1.Items.Add((WidgetElement) element2);
      cfg.ToolbarConfig.Sections.Add(element1);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) cfg.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element3 = gridViewModeElement;
      cfg.ViewModesConfig.Add((ViewModeElement) element3);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element3.ColumnsConfig);
      dataColumnElement1.Name = "Name";
      dataColumnElement1.HeaderText = "MediaQueryNameGrid";
      dataColumnElement1.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      dataColumnElement1.HeaderCssClass = "sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfTitleCol";
      dataColumnElement1.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_edit sfItemTitle sf' + Status.toLowerCase()}}\">\r\n                    <span>{{Name.htmlEncode()}}</span>\r\n                    <span class='sfStatusLocation'>{{Status}}</span></a>";
      DataColumnElement element4 = dataColumnElement1;
      element3.ColumnsConfig.Add((ColumnElement) element4);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element3.ColumnsConfig);
      dataColumnElement2.Name = "BehaviorDescription";
      dataColumnElement2.HeaderText = "Behavior";
      dataColumnElement2.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      dataColumnElement2.ClientTemplate = "<span>{{BehaviorDescription}}</span>";
      dataColumnElement2.HeaderCssClass = "sfLarge";
      dataColumnElement2.ItemCssClass = "sfLarge";
      DataColumnElement element5 = dataColumnElement2;
      element3.ColumnsConfig.Add((ColumnElement) element5);
      ConfigElementDictionary<string, ColumnElement> columnsConfig = element3.ColumnsConfig;
      DataColumnElement element6 = new DataColumnElement((ConfigElement) element3.ColumnsConfig);
      element6.Name = "AppliedTo";
      element6.HeaderText = "ContentBlocksAppliedToColumnHeaderText";
      element6.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      element6.ClientTemplate = "<span class=\"sf_binderCommand_PagesAndTemplatesDialog\">{{AppliedToString}}</span>";
      element6.HeaderCssClass = "sfUsedOnPage";
      element6.ItemCssClass = "sfUsedOnPage";
      columnsConfig.Add((ColumnElement) element6);
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) element3.ColumnsConfig);
      menuColumnElement.Name = "Actions";
      menuColumnElement.HeaderText = "Actions";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.ResourceClassId = typeof (Labels).Name;
      ActionMenuColumnElement element7 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element7.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
      element8.Name = "DeleteMediaQuery";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "delete";
      element8.Text = "Delete";
      element8.ResourceClassId = typeof (Labels).Name;
      element8.WidgetType = typeof (CommandWidget);
      menuItems1.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems2 = element7.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
      element9.Name = "EditMediaQuery";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "edit";
      element9.Text = "Edit";
      element9.ResourceClassId = typeof (Labels).Name;
      element9.WidgetType = typeof (CommandWidget);
      menuItems2.Add((WidgetElement) element9);
      element3.ColumnsConfig.Add((ColumnElement) element7);
      definitionFacade.AddInsertDialog("RulesGroupInsert", "RulesGroupBackend");
      definitionFacade.AddEditDialog("RulesGroupEdit", "RulesGroupBackend");
      definitionFacade.AddDialog<MediaQueryPagesAndTemplatesDialog>("PagesAndTemplatesDialog").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).DisplayTitleBar().MakeModal().ReloadOnShow();
      PromptDialogElement element10 = new PromptDialogElement((ConfigElement) cfg.PromptDialogsConfig)
      {
        DialogName = "singleRuleInUseDialog",
        ResourceClassId = typeof (ResponsiveDesignResources).Name,
        Message = "PromptMessageSingleRuleInUse",
        Width = 300,
        Height = 300,
        Title = "YouCannotDeleteARuleInUseTitle",
        ShowOnLoad = false
      };
      element10.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element10.CommandsConfig));
      cfg.PromptDialogsConfig.Add(element10);
      return (ConfigElement) cfg;
    }

    private static ConfigElement DefineMediaQueriesBackendInsertView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MediaQueryFormExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentFacade = fluentContentView.AddDetailView("RulesGroupInsert").SetTitle("CreateAGroupOfRules").LocalizeUsing<ResponsiveDesignResources>().SetServiceBaseUrl("~/Sitefinity/Services/ResponsiveDesign/MediaQuery.svc/").SetExternalClientScripts(extenalClientScripts).DoNotUseWorkflow().DoNotRenderTranslationView();
      DetailFormViewElement detailView = fluentFacade.Get();
      ResponsiveDesignDefinitions.CreateBackendSections(detailView, fluentFacade, FieldDisplayMode.Write);
      DefinitionsHelper.CreateBackendFormToolbar(detailView, typeof (ResponsiveDesignResources).Name, true, "RulesGroup", false, false, "Cancel", false);
      return (ConfigElement) detailView;
    }

    private static ConfigElement DefineMediaQueriesBackendEditView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MediaQueryFormExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentFacade = fluentContentView.AddDetailView("RulesGroupEdit").SetTitle("ModifyAGroupOfRules").LocalizeUsing<ResponsiveDesignResources>().SetServiceBaseUrl("~/Sitefinity/Services/ResponsiveDesign/MediaQuery.svc/").SetExternalClientScripts(extenalClientScripts).DoNotUseWorkflow().DoNotRenderTranslationView();
      DetailFormViewElement detailView = fluentFacade.Get();
      ResponsiveDesignDefinitions.CreateBackendSections(detailView, fluentFacade, FieldDisplayMode.Write);
      DefinitionsHelper.CreateBackendFormToolbar(detailView, typeof (ResponsiveDesignResources).Name, true, "RulesGroup", false, false, "Cancel", false);
      return (ConfigElement) detailView;
    }

    /// <summary>Creates the backend sections.</summary>
    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      DetailViewDefinitionFacade fluentFacade,
      FieldDisplayMode displayMode)
    {
      fluentFacade.AddFirstSection("MainSection").AddTextField("Name").SetId("nameFieldControl").SetTitle("MediaQueryNameFieldTitle").SetExample("MediaQueryNameFieldExample").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("MediaQueryNameCannotBeEmpty").Done();
      ContentViewSectionElement viewSectionElement1 = fluentFacade.AddSection("Rules").Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields1 = viewSectionElement1.Fields;
      MediaQueryRuleFieldDefinitionElement element1 = new MediaQueryRuleFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      element1.DataFieldName = "Rules";
      element1.FieldName = "Rules";
      element1.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      element1.Title = "ForDevicesWithTheseCharacteristics";
      element1.CssClass = "sfImportantForm";
      fields1.Add((FieldDefinitionElement) element1);
      ContentViewSectionElement viewSectionElement2 = fluentFacade.AddSection("Behavior").Get();
      ChoiceFieldElement choiceFieldElement1 = new ChoiceFieldElement((ConfigElement) viewSectionElement2.Fields);
      choiceFieldElement1.FieldName = "BehaviorField";
      choiceFieldElement1.ID = "BehaviorFieldId";
      choiceFieldElement1.DataFieldName = "Behavior";
      choiceFieldElement1.Title = "ApplyBehaviorTo";
      choiceFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
      choiceFieldElement1.RenderChoiceAs = RenderChoicesAs.RadioButtons;
      choiceFieldElement1.WrapperTag = HtmlTextWriterTag.Div;
      choiceFieldElement1.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      choiceFieldElement1.CssClass = "sfImportantForm";
      ChoiceFieldElement element2 = choiceFieldElement1;
      element2.ChoicesConfig.Add(new ChoiceElement((ConfigElement) element2.ChoicesConfig)
      {
        Text = "TransformTheLayout",
        Value = "TransformLayout",
        ResourceClassId = typeof (ResponsiveDesignResources).Name
      });
      element2.ChoicesConfig.Add(new ChoiceElement((ConfigElement) element2.ChoicesConfig)
      {
        Text = "OpenSpeciallyPreparedSite",
        Value = "MiniSite",
        ResourceClassId = typeof (ResponsiveDesignResources).Name
      });
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      ContentViewSectionElement viewSectionElement3 = fluentFacade.AddSection("CssSelector").Get();
      CssSelectorFieldDefinitionElement element3 = new CssSelectorFieldDefinitionElement((ConfigElement) viewSectionElement3.Fields);
      element3.FieldName = "AdditionalCss";
      element3.ID = "CssSelectorFieldId";
      element3.DataFieldName = "AdditionalCss";
      element3.DisplayMode = new FieldDisplayMode?(displayMode);
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.Title = "CssSelectorTitle";
      element3.Description = "CssSelectorDescription";
      element3.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      viewSectionElement3.Fields.Add((FieldDefinitionElement) element3);
      ContentViewSectionElement element4 = new ContentViewSectionElement((ConfigElement) detailView.Sections)
      {
        Name = "LayoutTransformationSection",
        Title = "LayoutTransformationSectionTitle",
        ResourceClassId = typeof (ResponsiveDesignResources).Name,
        CssClass = "sfExpandableForm",
        ExpandableDefinitionConfig = {
          Expanded = new bool?(true),
          ExpandText = "LayoutTransformationSectionTitle",
          ResourceClassId = typeof (ResponsiveDesignResources).Name
        }
      };
      ConfigElementDictionary<string, FieldDefinitionElement> fields2 = element4.Fields;
      LayoutTransformFieldDefinitionElement element5 = new LayoutTransformFieldDefinitionElement((ConfigElement) element4.Fields);
      element5.FieldName = "LayoutTransformations";
      element5.ID = "LayoutTransformFieldId";
      element5.DataFieldName = "LayoutTransformations";
      element5.DisplayMode = new FieldDisplayMode?(displayMode);
      fields2.Add((FieldDefinitionElement) element5);
      detailView.Sections.Add(element4);
      ContentViewSectionElement element6 = new ContentViewSectionElement((ConfigElement) detailView.Sections)
      {
        Name = "NavigationTransformationsSection",
        Title = "NavigationTransformationsSectionTitle",
        ResourceClassId = typeof (ResponsiveDesignResources).Name,
        CssClass = "sfExpandableForm",
        ExpandableDefinitionConfig = {
          Expanded = new bool?(false),
          ExpandText = "NavigationTransformationsSectionTitle",
          ResourceClassId = typeof (ResponsiveDesignResources).Name
        }
      };
      ConfigElementDictionary<string, FieldDefinitionElement> fields3 = element6.Fields;
      NavigationTransformationsFieldDefinitionElement element7 = new NavigationTransformationsFieldDefinitionElement((ConfigElement) element6.Fields);
      element7.FieldName = "NavigationTransformations";
      element7.ID = "NavigationTransformationsId";
      element7.DataFieldName = "NavigationTransformations";
      element7.DisplayMode = new FieldDisplayMode?(displayMode);
      fields3.Add((FieldDefinitionElement) element7);
      detailView.Sections.Add(element6);
      ContentViewSectionElement viewSectionElement4 = fluentFacade.AddSection("SiteSelection").Get();
      PageFieldElement element8 = new PageFieldElement((ConfigElement) viewSectionElement4.Fields);
      element8.Title = "SiteSelectorFieldTitle";
      element8.Description = "SiteSelectorFieldDescription";
      element8.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      element8.ID = "siteSelectorFieldId";
      element8.FieldName = "MiniSitePageId";
      element8.DataFieldName = "MiniSitePageId";
      element8.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element8.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      element8.CssClass = "sfFormSeparator";
      viewSectionElement4.Fields.Add((FieldDefinitionElement) element8);
      ContentViewSectionElement viewSectionElement5 = fluentFacade.AddSection("IsActive").Get();
      ChoiceFieldElement choiceFieldElement2 = new ChoiceFieldElement((ConfigElement) viewSectionElement5.Fields);
      choiceFieldElement2.FieldName = "IsActive";
      choiceFieldElement2.ID = "IsActiveField";
      choiceFieldElement2.DataFieldName = "IsActive";
      choiceFieldElement2.DisplayMode = new FieldDisplayMode?(displayMode);
      choiceFieldElement2.RenderChoiceAs = RenderChoicesAs.SingleCheckBox;
      choiceFieldElement2.CssClass = "sfCheckBox";
      choiceFieldElement2.WrapperTag = HtmlTextWriterTag.Li;
      choiceFieldElement2.ResourceClassId = typeof (ResponsiveDesignResources).Name;
      ChoiceFieldElement element9 = choiceFieldElement2;
      element9.ChoicesConfig.Add(new ChoiceElement((ConfigElement) element9.ChoicesConfig)
      {
        Text = Res.Get(typeof (ResponsiveDesignResources).Name, "ThisGroupOfRulesIsActive")
      });
      viewSectionElement5.Fields.Add((FieldDefinitionElement) element9);
    }
  }
}
