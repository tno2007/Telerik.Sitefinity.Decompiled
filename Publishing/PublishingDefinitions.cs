// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Defines publishing module's default UI</summary>
  public class PublishingDefinitions
  {
    public static string BackendDefinitionName = "PublishingBackend";
    /// <summary>
    /// Name of the view used to display list of events on the backend.
    /// </summary>
    public static string BackendListViewName = "PublishingBackendList";
    public static string BackendInsertDetailsViewName = "create";
    public static string BackendEditDetailsViewName = "edit";
    /// <summary>
    /// Common name used for a command that starts updating feed.
    /// </summary>
    public const string StartUpdatingFeedCommand = "startUpdating";
    /// <summary>
    /// Common name used for a command that stop updating feed.
    /// </summary>
    public const string StopUpdatingFeedCommand = "stopUpdating";
    public const string ActiveFeedsCommandName = "activeFeeds";
    public const string InactiveFeedsCommandName = "inactiveFeeds";
    public const string UpdatedTodayCommandName = "updatedToday";
    public const string UpdatedThisWeekCommandName = "updatedThisWeek";
    public const string UpdatedThisMonthCommandName = "updatedThisMonth";
    /// <summary>Localization resources' class Id for Labels</summary>
    public static readonly string ItemTemplate = "PublishingItemTemplate";

    /// <summary>
    /// Defines the ContentView control for Events on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade definitionFacade1 = App.WorkWith().Module("Publishing").DefineContainer(parent, PublishingDefinitions.BackendDefinitionName, typeof (PublishingPointDetailViewModel)).DoNotUseWorkflow();
      ContentViewControlElement viewControlElement = definitionFacade1.Get();
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.PublishingMasterExtensions.js, Telerik.Sitefinity", "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade2 = definitionFacade1.AddMasterView(PublishingDefinitions.BackendListViewName).LocalizeUsing<PublishingMessages>().SetSortExpression("DateCreated").SetTitle("ModuleTitle").SetServiceBaseUrl("~/Sitefinity/Services/Publishing/PublishingService.svc").SetExternalClientScripts(extenalClientScripts).SetSearchFields("Title");
      MasterGridViewElement element1 = definitionFacade2.Get();
      WidgetBarSectionElement element2 = new WidgetBarSectionElement((ConfigElement) element1.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement1.Name = "CreateFeedWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateNew";
      commandWidgetElement1.ResourceClassId = typeof (PublishingMessages).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element3 = commandWidgetElement1;
      element2.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement2.Name = "DeleteFeedWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupDelete";
      commandWidgetElement2.Text = "Delete";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement2;
      element2.Items.Add((WidgetElement) element4);
      element1.ToolbarConfig.Sections.Add(element2);
      WidgetBarSectionElement element5 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "Filter",
        Title = "FilterFeeds",
        ResourceClassId = typeof (PublishingMessages).Name,
        CssClass = "sfWidgetsList sfFirstNoHeading sfSeparator sfModules",
        WrapperTagId = "filterFeedsSection"
      };
      ConfigElementList<WidgetElement> items1 = element5.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.Items);
      element6.Name = "AllFeeds";
      element6.CommandName = "showAllItems";
      element6.ButtonType = CommandButtonType.SimpleLinkButton;
      element6.Text = "AllFeeds";
      element6.ResourceClassId = typeof (PublishingMessages).Name;
      element6.CssClass = "";
      element6.WidgetType = typeof (CommandWidget);
      element6.IsSeparator = false;
      element6.ButtonCssClass = "sfSel";
      items1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> items2 = element5.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.Items);
      element7.Name = "MyFeeds";
      element7.CommandName = "showMyItems";
      element7.ButtonType = CommandButtonType.SimpleLinkButton;
      element7.Text = "MyFeeds";
      element7.ResourceClassId = typeof (PublishingMessages).Name;
      element7.CssClass = "";
      element7.WidgetType = typeof (CommandWidget);
      element7.IsSeparator = false;
      items2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> items3 = element5.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element5.Items);
      element8.Name = "ActiveFeeds";
      element8.CommandName = "activeFeeds";
      element8.ButtonType = CommandButtonType.SimpleLinkButton;
      element8.Text = "ActiveFeeds";
      element8.ResourceClassId = typeof (PublishingMessages).Name;
      element8.CssClass = "";
      element8.WidgetType = typeof (CommandWidget);
      element8.IsSeparator = false;
      items3.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> items4 = element5.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element5.Items);
      element9.Name = "InactiveFeeds";
      element9.CommandName = "inactiveFeeds";
      element9.ButtonType = CommandButtonType.SimpleLinkButton;
      element9.Text = "InactiveFeeds";
      element9.ResourceClassId = typeof (PublishingMessages).Name;
      element9.CssClass = "";
      element9.WidgetType = typeof (CommandWidget);
      element9.IsSeparator = false;
      items4.Add((WidgetElement) element9);
      element1.SidebarConfig.Sections.Add(element5);
      WidgetBarSectionElement element10 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "LastUpdated",
        Title = "DisplayLastUpdatedFeeds",
        ResourceClassId = typeof (PublishingMessages).Name,
        CssClass = "sfFilterBy sfFilterByDate sfFirstNoHeading sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      element1.SidebarConfig.Sections.Add(element10);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element10.Items);
      commandWidgetElement3.Name = "CloseDateFilter";
      commandWidgetElement3.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element10.WrapperTagId);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "CloseDateFilter";
      commandWidgetElement3.ResourceClassId = typeof (PublishingMessages).Name;
      commandWidgetElement3.CssClass = "sfCloseFilter";
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element11 = commandWidgetElement3;
      element10.Items.Add((WidgetElement) element11);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element10.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element12 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element12.PredefinedFilteringRanges);
      element10.Items.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> items5 = element5.Items;
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element5.Items);
      commandWidgetElement4.Name = "FilterByDate";
      commandWidgetElement4.CommandName = "hideSectionsExcept";
      commandWidgetElement4.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element10.WrapperTagId);
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "ByLastPublication";
      commandWidgetElement4.ResourceClassId = typeof (PublishingMessages).Name;
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      commandWidgetElement4.IsSeparator = false;
      CommandWidgetElement element13 = commandWidgetElement4;
      items5.Add((WidgetElement) element13);
      WidgetBarSectionElement element14 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "ByContentType",
        Title = "FeedsByContentType",
        ResourceClassId = typeof (PublishingMessages).Name,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "contentTypeFilterSection"
      };
      element1.SidebarConfig.Sections.Add(element14);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element15 = gridViewModeElement;
      element1.ViewModesConfig.Add((ViewModeElement) element15);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element15.ColumnsConfig);
      dataColumnElement1.Name = "Title";
      dataColumnElement1.HeaderText = "Feed";
      dataColumnElement1.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement1.HeaderCssClass = "sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfTitleCol";
      dataColumnElement1.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_edit sfItemTitle ' + ((IsActive) ? 'sfpublished' : 'sfhidden') }}\">\r\n                    <strong>{{Title.htmlEncode()}}</strong>\r\n                    <span class='sfStatusLocation'>{{ (IsActive) ? '" + Res.Get<PublishingMessages>().Active + "' : '" + Res.Get<PublishingMessages>().Stopped + "' }}</span></a>";
      DataColumnElement element16 = dataColumnElement1;
      element15.ColumnsConfig.Add((ColumnElement) element16);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element15.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.TitleText = "Actions";
      menuColumnElement1.HeaderCssClass = "sfMoreActions";
      menuColumnElement1.ItemCssClass = "sfMoreActions";
      menuColumnElement1.ResourceClassId = typeof (Labels).Name;
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      PublishingDefinitions.FillGridActionMenuItems(menuColumnElement2.MenuItems, (ConfigElement) menuColumnElement2, typeof (PublishingMessages).Name);
      element15.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element15.ColumnsConfig);
      dataColumnElement2.Name = "PublishedAs";
      dataColumnElement2.HeaderText = "PublishedAsHeader";
      dataColumnElement2.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement2.ClientTemplate = "<div id=\"publishedAs\"></div>";
      dataColumnElement2.HeaderCssClass = "sfRegular";
      dataColumnElement2.ItemCssClass = "sfRegular";
      DataColumnElement element17 = dataColumnElement2;
      element15.ColumnsConfig.Add((ColumnElement) element17);
      DataColumnElement dataColumnElement3 = new DataColumnElement((ConfigElement) element15.ColumnsConfig);
      dataColumnElement3.Name = "PublicationDate";
      dataColumnElement3.HeaderText = "PublicationDateHeader";
      dataColumnElement3.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement3.ClientTemplate = "<span class='sfLine'>{{ (LastPublicationDate) ? LastPublicationDate.sitefinityLocaleFormat('dd MMM, yyyy') : '&nbsp;' }}</span>";
      dataColumnElement3.HeaderCssClass = "sfLongDate";
      dataColumnElement3.ItemCssClass = "sfLongDate";
      DataColumnElement element18 = dataColumnElement3;
      element15.ColumnsConfig.Add((ColumnElement) element18);
      DataColumnElement dataColumnElement4 = new DataColumnElement((ConfigElement) element15.ColumnsConfig);
      dataColumnElement4.Name = "Date";
      dataColumnElement4.HeaderText = "DateOwnerHeader";
      dataColumnElement4.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement4.ClientTemplate = "<span class='sfLine'>{{ (DateCreated) ? DateCreated.sitefinityLocaleFormat('dd MMM, yyyy') : '&nbsp;' }}</span><span class='sfLine'>{{Owner.htmlEncode()}}</span>";
      dataColumnElement4.HeaderCssClass = "sfDateAuthor";
      dataColumnElement4.ItemCssClass = "sfDateAuthor";
      DataColumnElement element19 = dataColumnElement4;
      element15.ColumnsConfig.Add((ColumnElement) element19);
      DecisionScreenElement element20 = new DecisionScreenElement((ConfigElement) element1.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoFeeds",
        ResourceClassId = typeof (PublishingMessages).Name
      };
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element20.Actions);
      commandWidgetElement5.Name = "Create";
      commandWidgetElement5.ButtonType = CommandButtonType.Create;
      commandWidgetElement5.CommandName = "create";
      commandWidgetElement5.Text = "CreateNew";
      commandWidgetElement5.ResourceClassId = typeof (PublishingMessages).Name;
      commandWidgetElement5.CssClass = "sfCreateItem";
      CommandWidgetElement element21 = commandWidgetElement5;
      element20.Actions.Add(element21);
      element1.DecisionScreensConfig.Add(element20);
      definitionFacade2.AddInsertDialog(PublishingDefinitions.BackendInsertDetailsViewName).Done().AddEditDialog(PublishingDefinitions.BackendEditDetailsViewName);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      DetailViewDefinitionFacade blankItem = definitionFacade1.AddDetailView(PublishingDefinitions.BackendEditDetailsViewName).SetTitle("EditItem").HideTopToolbar().LocalizeUsing<PublishingMessages>().SetServiceBaseUrl("~/Sitefinity/Services/Publishing/PublishingService.svc/").DoNotUnlockDetailItemOnExit().DoNotCreateBlankItem();
      DetailFormViewElement detailFormViewElement1 = blankItem.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) detailFormViewElement1);
      DetailViewDefinitionFacade fluentDetailView = definitionFacade1.AddDetailView(PublishingDefinitions.BackendInsertDetailsViewName).SetTitle("CreateNewItem").HideTopToolbar().LocalizeUsing<PublishingMessages>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Publishing/PublishingService.svc/").DoNotCreateBlankItem().SetItemTemplate(PublishingDefinitions.ItemTemplate);
      DetailFormViewElement detailFormViewElement2 = fluentDetailView.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) detailFormViewElement2);
      PublishingDefinitions.CreateBackendSections(detailFormViewElement1, false, blankItem);
      PublishingDefinitions.CreateBackendSections(detailFormViewElement2, true, fluentDetailView);
      return viewControlElement;
    }

    internal static void AddBackendFeedsContentTypeFilterSection(
      Type type,
      string name,
      string text)
    {
      ConfigElementList<WidgetElement> items = ((MasterGridViewElement) Telerik.Sitefinity.Configuration.Config.Get<PublishingConfig>().ContentViewControls[PublishingDefinitions.BackendDefinitionName].ViewsConfig[PublishingDefinitions.BackendListViewName]).SidebarConfig.Sections.Where<WidgetBarSectionElement>((Func<WidgetBarSectionElement, bool>) (s => s.Name == "ByContentType")).First<WidgetBarSectionElement>().Items;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) items);
      element.Name = name;
      element.CommandName = "filterContent";
      element.CommandArgument = DefinitionsHelper.ConstructValueCommandArgument((object) type.FullName);
      element.ButtonType = CommandButtonType.SimpleLinkButton;
      element.Text = text;
      element.ResourceClassId = typeof (PublishingMessages).Name;
      element.CssClass = "";
      element.WidgetType = typeof (CommandWidget);
      element.IsSeparator = false;
      items.Add((WidgetElement) element);
    }

    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      bool createNew,
      DetailViewDefinitionFacade fluentDetailView)
    {
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = fluentDetailView.AddExpandableSection("AdvancedSection").SetTitle("AdvancedOptionsSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      definitionFacade1.AddTextField("Title").SetId("titleField").SetTitle("TitleFieldTitle").SetCssClass("sfTitleField").SetExample("ExampleUnderTitle").AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").LocalizeUsing<ContentResources>().Done().Done().AddTextField("Description").SetId("descriptionField").SetTitle("Description").SetRows(3).Done().AddSingleCheckboxField("IsActive", "ThisFeedIsActive", true).SetId("activateField").MakeMutuallyExclusive().SetCssClass("sfCheckBox").SetDescription("UncheckToStopUpdatingTheFeed").Done();
      ContentViewSectionElement viewSectionElement2 = fluentDetailView.AddSection("PipeSettingsSectionInbound").Get();
      ListPipeSettingsFieldDefinitionElement element1 = new ListPipeSettingsFieldDefinitionElement((ConfigElement) viewSectionElement2.Fields);
      element1.ID = "pipeSettingsFieldControl";
      element1.DataFieldName = "InboundSettings";
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.ResourceClassId = typeof (PublishingMessages).Name;
      element1.WrapperTag = HtmlTextWriterTag.Li;
      element1.Title = "ContentToInclude";
      element1.DefaultPipeName = "ContentInboundPipe";
      element1.AddPipeText = "AddAnotherContentType";
      element1.DisableActivation = true;
      element1.CssClass = "sfContentToInclude sfAltContent";
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element1);
      ContentViewSectionElement viewSectionElement3 = fluentDetailView.AddSection("PipeSettingsSectionOutbound").Get();
      ListPipeSettingsFieldDefinitionElement element2 = new ListPipeSettingsFieldDefinitionElement((ConfigElement) viewSectionElement3.Fields);
      element2.ID = "pipeSettingsFieldControl";
      element2.DataFieldName = "OutboundSettings";
      element2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element2.ResourceClassId = typeof (PublishingMessages).Name;
      element2.WrapperTag = HtmlTextWriterTag.Li;
      element2.Title = "PublishAs";
      element2.CssClass = "sfContentPublishedAs";
      element2.DisableAdding = false;
      element2.DisableRemoving = false;
      element2.DisableActivation = true;
      element2.ChangePipeText = "SettingsPipeLabel";
      element2.AddPipeText = "AddAnotherExportPipe";
      element2.WorkWithOutboundPipes = true;
      element2.DefaultPipeName = "RSSOutboundPipe";
      element2.ShowDefaultPipes = createNew;
      viewSectionElement3.Fields.Add((FieldDefinitionElement) element2);
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade3 = fluentDetailView.AddReadOnlySection("Sidebar");
      ContentViewSectionElement viewSectionElement4 = definitionFacade3.Get();
      MetaTypeStructureFieldDefinitionElement definitionElement = new MetaTypeStructureFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      definitionElement.ID = "structureField";
      definitionElement.DataFieldName = "PublishingPointDefinition";
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement.Title = Res.Get<Labels>().Data;
      definitionElement.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement.EditButtonTextResource = "EditStructure";
      definitionElement.ResourceClassId = typeof (Labels).Name;
      MetaTypeStructureFieldDefinitionElement element3 = definitionElement;
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element3);
      definitionFacade2.AddChoiceField<PublishingPointTypeSelectorField>("PublishingPointBusinessObjectName", RenderChoicesAs.DropDown).SetId("publishingPointType").MakeMutuallyExclusive().SetDisplayMode(FieldDisplayMode.Write).SetTitle("FeedType").SetCssClass("sfFormSeparator").Done().Done();
      DateFieldElement element4 = new DateFieldElement((ConfigElement) viewSectionElement4.Fields);
      element4.Title = "DateCreatedColon";
      element4.DataFieldName = "DateCreated";
      element4.WrapperTag = HtmlTextWriterTag.Li;
      element4.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      element4.ResourceClassId = typeof (PublishingMessages).Name;
      viewSectionElement4.Fields.Add((FieldDefinitionElement) element4);
      definitionFacade3.AddTextField("Owner").SetTitle("CreatedByColon").SetDisplayMode(FieldDisplayMode.Read).SetCssClass("sfOneLineInfo").Done();
      DateFieldElement element5 = new DateFieldElement((ConfigElement) viewSectionElement4.Fields);
      element5.Title = "LastPublicationOn";
      element5.DataFieldName = "LastPublicationDate";
      element5.WrapperTag = HtmlTextWriterTag.Li;
      element5.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      element5.ResourceClassId = typeof (PublishingMessages).Name;
      viewSectionElement4.Fields.Add((FieldDefinitionElement) element5);
      WidgetBarSectionElement element6 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div
      };
      ConfigElementList<WidgetElement> items1 = element6.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element6.Items);
      element7.Name = "SaveChangesWidgetElement";
      element7.ButtonType = CommandButtonType.Save;
      element7.CommandName = "save";
      element7.Text = "SaveChanges";
      element7.ResourceClassId = typeof (PublishingMessages).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Span;
      element7.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> items2 = element6.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.Items);
      element8.Name = "CancelWidgetElement";
      element8.ButtonType = CommandButtonType.Cancel;
      element8.CommandName = "cancel";
      element8.Text = "Cancel";
      element8.ResourceClassId = typeof (Labels).Name;
      element8.WrapperTagKey = HtmlTextWriterTag.Span;
      element8.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element8);
      detailView.Toolbar.Sections.Add(element6);
    }

    public static void FillGridActionMenuItems(
      ConfigElementList<WidgetElement> menuItems,
      ConfigElement parent,
      string resourceClassId)
    {
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", resourceClassId, "sfDeleteItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "StopUpdating", HtmlTextWriterTag.Li, "stopUpdating", "StopUpdatingFeed", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "StartUpdating", HtmlTextWriterTag.Li, "startUpdating", "StartUpdatingFeed", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "RunPipes", HtmlTextWriterTag.Li, "runPipes", "RunPipes", resourceClassId));
    }
  }
}
