// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Configuration.CommentsModuleDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Comments.Fields;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Backend;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Configuration
{
  internal static class CommentsModuleDefinitions
  {
    internal const string BackendListViewByThread = "CommentsByThreadGridView";
    internal const string BackendDefinitionName = "CommentsModuleBackend";
    private const string BackendListViewName = "CommentsModuleBackendListView";
    private const string BackendListViewByThreadName = "CommentsModuleBackendListViewByThread";
    private const string BackendDetailViewName = "CommentsModuleBackendDetailView";
    private const string GroupHideCommandName = "groupHide";
    private const string GroupMarkSpamCommandName = "groupMarkSpam";

    /// <summary>Defines the comments backend view.</summary>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    internal static ViewContainerElement DefineCommentsBackendView(
      ConfigElement parent)
    {
      ViewContainerElement containerElement = new ViewContainerElement(parent);
      containerElement.ModuleName = "Comments";
      containerElement.ControlDefinitionName = "CommentsModuleBackend";
      ViewContainerElement backendView = containerElement;
      backendView.ViewsConfig.AddLazy((object) "CommentsModuleBackendListView", (Func<ConfigElement>) (() => CommentsModuleDefinitions.DefineBackendListView(backendView)));
      backendView.ViewsConfig.AddLazy((object) "CommentsModuleBackendListViewByThread", (Func<ConfigElement>) (() => CommentsModuleDefinitions.DefineBackendListViewByThread(backendView)));
      backendView.ViewsConfig.AddLazy((object) "CommentsModuleBackendDetailView", (Func<ConfigElement>) (() => CommentsModuleDefinitions.DefineBackendDetailView(backendView)));
      return backendView;
    }

    private static ConfigElement DefineBackendDetailView(
      ViewContainerElement backendView)
    {
      DetailViewControlElement viewControlElement1 = new DetailViewControlElement((ConfigElement) backendView);
      viewControlElement1.ViewName = "CommentsModuleBackendDetailView";
      viewControlElement1.ViewType = typeof (CommentsDetailView);
      DetailViewControlElement viewControlElement2 = viewControlElement1;
      ContentViewSectionElement element1 = new ContentViewSectionElement((ConfigElement) viewControlElement2.Sections)
      {
        Name = "MainSection",
        DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write),
        ViewName = "CommentsModuleBackendDetailView"
      };
      HtmlFieldElement htmlFieldElement1 = new HtmlFieldElement((ConfigElement) element1.Fields);
      htmlFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      htmlFieldElement1.DataFieldName = "Message";
      htmlFieldElement1.FieldName = "Message";
      HtmlFieldElement htmlFieldElement2 = htmlFieldElement1;
      ValidatorDefinitionElement definitionElement1 = new ValidatorDefinitionElement((ConfigElement) htmlFieldElement2)
      {
        Required = new bool?(true),
        MessageCssClass = "sfError",
        RequiredViolationMessage = "MessageCannotBeEmpty"
      };
      htmlFieldElement2.ValidatorConfig = definitionElement1;
      element1.Fields.Add((FieldDefinitionElement) htmlFieldElement2);
      ChoiceFieldElement choiceFieldElement = new ChoiceFieldElement((ConfigElement) element1.Fields);
      choiceFieldElement.ID = "ratingField";
      choiceFieldElement.DataFieldName = "Rating";
      choiceFieldElement.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      choiceFieldElement.Title = "Rating";
      choiceFieldElement.ResourceClassId = typeof (CommentsResources).Name;
      choiceFieldElement.FieldType = typeof (RatingChoiceField);
      choiceFieldElement.MutuallyExclusive = true;
      ChoiceFieldElement element2 = choiceFieldElement;
      for (int index = 1; index < 6; ++index)
        element2.ChoicesConfig.Add(new ChoiceElement((ConfigElement) element2.ChoicesConfig)
        {
          Text = index.ToString(),
          Value = index.ToString()
        });
      element1.Fields.Add((FieldDefinitionElement) element2);
      TextFieldDefinitionElement definitionElement2 = new TextFieldDefinitionElement((ConfigElement) element1.Fields);
      definitionElement2.Title = "Name";
      definitionElement2.FieldName = "Name";
      definitionElement2.DataFieldName = "Name";
      definitionElement2.ResourceClassId = typeof (CommentsResources).Name;
      definitionElement2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      definitionElement2.CssClass = "sfFormSeparator sfShortField250";
      TextFieldDefinitionElement element3 = definitionElement2;
      element1.Fields.Add((FieldDefinitionElement) element3);
      TextFieldDefinitionElement definitionElement3 = new TextFieldDefinitionElement((ConfigElement) element1.Fields);
      definitionElement3.Title = "Email";
      definitionElement3.FieldName = "Email";
      definitionElement3.ResourceClassId = typeof (CommentsResources).Name;
      definitionElement3.DataFieldName = "Email";
      definitionElement3.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      definitionElement3.CssClass = "sfFormSeparator sfShortField250";
      TextFieldDefinitionElement element4 = definitionElement3;
      element1.Fields.Add((FieldDefinitionElement) element4);
      ConfigElementDictionary<string, FieldDefinitionElement> fields1 = element1.Fields;
      DateFieldElement element5 = new DateFieldElement((ConfigElement) element1.Fields);
      element5.Title = "PostedOn";
      element5.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      element5.ResourceClassId = typeof (CommentsResources).Name;
      element5.FieldName = "DateCreated";
      element5.DataFieldName = "DateCreated";
      element5.CssClass = "sfFormSeparator";
      fields1.Add((FieldDefinitionElement) element5);
      ConfigElementDictionary<string, FieldDefinitionElement> fields2 = element1.Fields;
      TextFieldDefinitionElement element6 = new TextFieldDefinitionElement((ConfigElement) element1.Fields);
      element6.Title = "IP";
      element6.ResourceClassId = typeof (CommentsResources).Name;
      element6.FieldName = "AuthorIpAddress";
      element6.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      element6.DataFieldName = "AuthorIpAddress";
      element6.CssClass = "sfFormSeparator";
      fields2.Add((FieldDefinitionElement) element6);
      viewControlElement2.Sections.Add(element1);
      WidgetBarSectionElement element7 = new WidgetBarSectionElement((ConfigElement) viewControlElement2.ToolbarConfig.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div
      };
      ConfigElementList<WidgetElement> items1 = element7.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element7.Items);
      element8.Name = "SaveChangesWidgetElement";
      element8.ButtonType = CommandButtonType.Standard;
      element8.CommandName = "save";
      element8.Text = "SaveChanges";
      element8.WrapperTagKey = HtmlTextWriterTag.Span;
      element8.WidgetType = typeof (CommandWidget);
      element8.ResourceClassId = typeof (Labels).Name;
      element8.CssClass = "sfSave";
      items1.Add((WidgetElement) element8);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element7.Items);
      menuWidgetElement.Name = "moreActions";
      menuWidgetElement.Text = "MoreActionsLink";
      menuWidgetElement.ResourceClassId = typeof (Labels).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
      ActionMenuWidgetElement element9 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element9.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element9.MenuItems);
      element10.Name = "Publish";
      element10.Text = "Publish";
      element10.CommandName = "publish";
      element10.ResourceClassId = typeof (Labels).Name;
      element10.WidgetType = typeof (CommandWidget);
      menuItems1.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems2 = element9.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element9.MenuItems);
      element11.Name = "Hide";
      element11.Text = "Hide";
      element11.CommandName = "hide";
      element11.ResourceClassId = typeof (CommentsResources).Name;
      element11.WidgetType = typeof (CommandWidget);
      menuItems2.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems3 = element9.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element9.MenuItems);
      element12.Name = "MarkSpam";
      element12.Text = "MarkSpam";
      element12.CommandName = "markSpam";
      element12.ResourceClassId = typeof (CommentsResources).Name;
      element12.WidgetType = typeof (CommandWidget);
      menuItems3.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> menuItems4 = element9.MenuItems;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element9.MenuItems);
      element13.Name = "Delete";
      element13.Text = "DeleteLabel";
      element13.CommandName = "delete";
      element13.WidgetType = typeof (CommandWidget);
      element13.ResourceClassId = typeof (Labels).Name;
      element13.CssClass = "sfDeleteItm";
      menuItems4.Add((WidgetElement) element13);
      element7.Items.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> items2 = element7.Items;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element7.Items);
      element14.Name = "CancelWidgetElement";
      element14.ButtonType = CommandButtonType.Cancel;
      element14.CommandName = "cancel";
      element14.Text = "BackToComments";
      element14.WrapperTagKey = HtmlTextWriterTag.Span;
      element14.ResourceClassId = typeof (CommentsResources).Name;
      element14.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element14);
      viewControlElement2.ToolbarConfig.Sections.Add(element7);
      return (ConfigElement) viewControlElement2;
    }

    /// <summary>Defines the backend list view.</summary>
    /// <param name="backendView">The backend view.</param>
    /// <returns></returns>
    private static ConfigElement DefineBackendListView(ViewContainerElement backendView)
    {
      CommentsMasterViewElement masterViewElement = new CommentsMasterViewElement((ConfigElement) backendView);
      masterViewElement.ViewName = "CommentsGridView";
      masterViewElement.ViewType = typeof (CommentsGridView);
      CommentsMasterViewElement element1 = masterViewElement;
      WidgetBarSectionElement element2 = new WidgetBarSectionElement((ConfigElement) element1.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement1.Name = "DeleteCommentsWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Standard;
      commandWidgetElement1.CommandName = "groupDelete";
      commandWidgetElement1.Text = "Delete";
      commandWidgetElement1.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.CssClass = "sfGroupBtn";
      CommandWidgetElement element3 = commandWidgetElement1;
      element2.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement2.Name = "PublishCommentsWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupPublish";
      commandWidgetElement2.Text = "Publish";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement2;
      element2.Items.Add((WidgetElement) element4);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element2.Items);
      commandWidgetElement3.Name = "HideCommentsWidget";
      commandWidgetElement3.ButtonType = CommandButtonType.Standard;
      commandWidgetElement3.CommandName = "groupHide";
      commandWidgetElement3.Text = "Hide";
      commandWidgetElement3.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.CssClass = "sfGroupBtn";
      CommandWidgetElement element5 = commandWidgetElement3;
      element2.Items.Add((WidgetElement) element5);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element2.Items);
      menuWidgetElement.Name = "MoreActionsWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (Labels).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element6 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems = element6.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element7.Name = "MarkSpamCommentsWidget";
      element7.Text = "MarkSpam";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "groupMarkSpam";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (CommentsResources).Name;
      element7.CssClass = "sfPublishItm";
      menuItems.Add((WidgetElement) element7);
      element2.Items.Add((WidgetElement) element6);
      element1.ToolbarConfig.Sections.Add(element2);
      LocalizationWidgetBarSectionElement barSectionElement = new LocalizationWidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections);
      barSectionElement.Name = "Languages";
      barSectionElement.Title = "Languages";
      barSectionElement.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element8 = barSectionElement;
      ConfigElementList<WidgetElement> items1 = element8.Items;
      LanguagesDropDownListWidgetElement element9 = new LanguagesDropDownListWidgetElement((ConfigElement) element8.Items);
      element9.Name = "Languages";
      element9.Text = "Languages";
      element9.ResourceClassId = typeof (LocalizationResources).Name;
      element9.CssClass = "";
      element9.WidgetType = typeof (LanguagesDropDownListWidget);
      element9.IsSeparator = false;
      element9.LanguageSource = LanguageSource.Frontend;
      element9.AddAllLanguagesOption = false;
      element9.CommandName = "changeLanguage";
      items1.Add((WidgetElement) element9);
      WidgetBarSectionElement element10 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "FilterComments",
        Title = "FilterComments",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items2 = element10.Items;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element10.Items);
      element11.Name = "AllComments";
      element11.CommandName = "showAllComments";
      element11.ButtonType = CommandButtonType.SimpleLinkButton;
      element11.Text = "AllComments";
      element11.ResourceClassId = typeof (CommentsResources).Name;
      element11.CssClass = "sfComments";
      element11.WidgetType = typeof (CommandWidget);
      element11.IsSeparator = false;
      element11.ButtonCssClass = "sfSel";
      items2.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> items3 = element10.Items;
      LiteralWidgetElement element12 = new LiteralWidgetElement((ConfigElement) element10.Items);
      element12.Name = "Separator";
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.WidgetType = typeof (LiteralWidget);
      element12.CssClass = "sfSeparator";
      element12.Text = "&nbsp;";
      element12.IsSeparator = true;
      items3.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> items4 = element10.Items;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element10.Items);
      element13.Name = "WaitingApproval";
      element13.CommandName = "showWaitingApprovalItems";
      element13.ButtonType = CommandButtonType.SimpleLinkButton;
      element13.Text = "WaitingApproval";
      element13.ResourceClassId = typeof (CommentsResources).Name;
      element13.CssClass = "sfComments";
      element13.WidgetType = typeof (CommandWidget);
      element13.IsSeparator = false;
      items4.Add((WidgetElement) element13);
      ConfigElementList<WidgetElement> items5 = element10.Items;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element10.Items);
      element14.Name = "Published";
      element14.CommandName = "showPublishedItems";
      element14.ButtonType = CommandButtonType.SimpleLinkButton;
      element14.Text = "Published";
      element14.ResourceClassId = typeof (CommentsResources).Name;
      element14.CssClass = "sfComments";
      element14.WidgetType = typeof (CommandWidget);
      element14.IsSeparator = false;
      items5.Add((WidgetElement) element14);
      ConfigElementList<WidgetElement> items6 = element10.Items;
      CommandWidgetElement element15 = new CommandWidgetElement((ConfigElement) element10.Items);
      element15.Name = "Hidden";
      element15.CommandName = "showHiddenItems";
      element15.ButtonType = CommandButtonType.SimpleLinkButton;
      element15.Text = "Hidden";
      element15.ResourceClassId = typeof (CommentsResources).Name;
      element15.CssClass = "sfComments";
      element15.WidgetType = typeof (CommandWidget);
      element15.IsSeparator = false;
      items6.Add((WidgetElement) element15);
      ConfigElementList<WidgetElement> items7 = element10.Items;
      CommandWidgetElement element16 = new CommandWidgetElement((ConfigElement) element10.Items);
      element16.Name = "Spam";
      element16.CommandName = "showMarkedAsSpamItems";
      element16.ButtonType = CommandButtonType.SimpleLinkButton;
      element16.Text = "Spam";
      element16.ResourceClassId = typeof (CommentsResources).Name;
      element16.CssClass = "sfComments";
      element16.WidgetType = typeof (CommandWidget);
      element16.IsSeparator = false;
      items7.Add((WidgetElement) element16);
      if (CommentsModuleDefinitions.ReviewsAreAllowed((ConfigElement) element1))
      {
        ConfigElementList<WidgetElement> items8 = element10.Items;
        CommandWidgetElement element17 = new CommandWidgetElement((ConfigElement) element10.Items);
        element17.Name = "WithRating";
        element17.CommandName = "showReviews";
        element17.ButtonType = CommandButtonType.SimpleLinkButton;
        element17.Text = "WithRating";
        element17.ResourceClassId = typeof (CommentsResources).Name;
        element17.CssClass = "sfComments";
        element17.WidgetType = typeof (CommandWidget);
        element17.IsSeparator = false;
        element17.ButtonCssClass = "sfComments";
        items8.Add((WidgetElement) element17);
        ConfigElementList<WidgetElement> items9 = element10.Items;
        CommandWidgetElement element18 = new CommandWidgetElement((ConfigElement) element10.Items);
        element18.Name = "WithoutRating";
        element18.CommandName = "showOnlyComments";
        element18.ButtonType = CommandButtonType.SimpleLinkButton;
        element18.Text = "WithoutRating";
        element18.ResourceClassId = typeof (CommentsResources).Name;
        element18.CssClass = "sfComments";
        element18.WidgetType = typeof (CommandWidget);
        element18.IsSeparator = false;
        element18.ButtonCssClass = "sfComments";
        items9.Add((WidgetElement) element18);
      }
      ConfigElementList<WidgetElement> items10 = element10.Items;
      LiteralWidgetElement element19 = new LiteralWidgetElement((ConfigElement) element10.Items);
      element19.Name = "Separator";
      element19.WrapperTagKey = HtmlTextWriterTag.Li;
      element19.WidgetType = typeof (LiteralWidget);
      element19.CssClass = "sfSeparator";
      element19.Text = "&nbsp;";
      element19.IsSeparator = true;
      items10.Add((WidgetElement) element19);
      WidgetBarSectionElement element20 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "OtherFilterOptions",
        Title = "OtherFilterOptions",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "otherFilterOptions"
      };
      WidgetBarSectionElement element21 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "CommentsContentFilter",
        Title = "ByContentTypeHeader",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "contentFilterSection",
        Visible = new bool?(false)
      };
      element1.SidebarConfig.Sections.Add(element21);
      WidgetBarSectionElement element22 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "CommentsDateFilter",
        Title = "DisplayCommentsIn",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      element1.SidebarConfig.Sections.Add(element22);
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element21.Items);
      commandWidgetElement4.Name = "CloseContentFilter";
      commandWidgetElement4.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement4.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element21.WrapperTagId, element22.WrapperTagId);
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "CloseContentFilter";
      commandWidgetElement4.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement4.CssClass = "sfCloseFilter";
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      commandWidgetElement4.IsSeparator = false;
      CommandWidgetElement element23 = commandWidgetElement4;
      element21.Items.Add((WidgetElement) element23);
      CommentsFilterByTypeElement filterByTypeElement = new CommentsFilterByTypeElement((ConfigElement) element21.Items);
      filterByTypeElement.Name = "ContentFilter";
      filterByTypeElement.WidgetType = typeof (ContentFilteringWidget);
      filterByTypeElement.IsSeparator = false;
      filterByTypeElement.PropertyNameToFilter = "ContentType";
      CommentsFilterByTypeElement element24 = filterByTypeElement;
      element21.Items.Add((WidgetElement) element24);
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element20.Items);
      commandWidgetElement5.Name = "FilterByContent";
      commandWidgetElement5.CommandName = "hideSectionsExcept";
      commandWidgetElement5.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element21.WrapperTagId);
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "ByContentType";
      commandWidgetElement5.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      commandWidgetElement5.CssClass = "sfComments";
      CommandWidgetElement element25 = commandWidgetElement5;
      element20.Items.Add((WidgetElement) element25);
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element22.Items);
      commandWidgetElement6.Name = "CloseDateFilter";
      commandWidgetElement6.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement6.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element21.WrapperTagId, element22.WrapperTagId);
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "CloseDateFilter";
      commandWidgetElement6.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement6.CssClass = "sfCloseFilter";
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      commandWidgetElement6.IsSeparator = false;
      CommandWidgetElement element26 = commandWidgetElement6;
      element22.Items.Add((WidgetElement) element26);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element22.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element27 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element27.PredefinedFilteringRanges);
      element22.Items.Add((WidgetElement) element27);
      CommandWidgetElement commandWidgetElement7 = new CommandWidgetElement((ConfigElement) element20.Items);
      commandWidgetElement7.Name = "FilterByDate";
      commandWidgetElement7.CommandName = "hideSectionsExcept";
      commandWidgetElement7.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element22.WrapperTagId);
      commandWidgetElement7.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement7.Text = "ByDateModified";
      commandWidgetElement7.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement7.WidgetType = typeof (CommandWidget);
      commandWidgetElement7.IsSeparator = false;
      commandWidgetElement7.CssClass = "sfComments";
      CommandWidgetElement element28 = commandWidgetElement7;
      element20.Items.Add((WidgetElement) element28);
      WidgetBarSectionElement element29 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "Settings",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      CommandWidgetElement commandWidgetElement8 = new CommandWidgetElement((ConfigElement) element29.Items);
      commandWidgetElement8.Name = "CommentsSettings";
      commandWidgetElement8.CommandName = "settings";
      commandWidgetElement8.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement8.Text = "Settings";
      commandWidgetElement8.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement8.WidgetType = typeof (CommandWidget);
      commandWidgetElement8.IsSeparator = false;
      CommandWidgetElement element30 = commandWidgetElement8;
      element29.Items.Add((WidgetElement) element30);
      element1.SidebarConfig.ResourceClassId = typeof (CommentsResources).Name;
      element1.SidebarConfig.Title = "ManageComments";
      element1.SidebarConfig.Sections.Add((WidgetBarSectionElement) element8);
      element1.SidebarConfig.Sections.Add(element10);
      element1.SidebarConfig.Sections.Add(element20);
      element1.SidebarConfig.Sections.Add(element29);
      DecisionScreenElement element31 = new DecisionScreenElement((ConfigElement) element1.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoComments",
        ResourceClassId = typeof (CommentsResources).Name
      };
      ConfigElementList<CommandWidgetElement> actions = element31.Actions;
      CommandWidgetElement element32 = new CommandWidgetElement((ConfigElement) element31.Actions);
      element32.Name = "Settings";
      element32.ButtonType = CommandButtonType.Standard;
      element32.CommandName = "settings";
      element32.Text = "SettingForComments";
      element32.ResourceClassId = typeof (CommentsResources).Name;
      element32.CssClass = "sfCreateItem sfCreateComments";
      actions.Add(element32);
      element1.DecisionScreensConfig.Add(element31);
      CommentsModuleDefinitions.DefineDetailsDialog(element1.DialogsConfig);
      return (ConfigElement) element1;
    }

    /// <summary>Defines the backend list view by comments thread.</summary>
    /// <param name="backendView">The backend view.</param>
    /// <returns></returns>
    private static ConfigElement DefineBackendListViewByThread(
      ViewContainerElement backendView)
    {
      CommentsMasterViewElement masterViewElement1 = new CommentsMasterViewElement((ConfigElement) backendView);
      masterViewElement1.ViewName = "CommentsByThreadGridView";
      masterViewElement1.ViewType = typeof (CommentsByThreadGridView);
      CommentsMasterViewElement masterViewElement2 = masterViewElement1;
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterViewElement2.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "DeleteCommentsWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Standard;
      commandWidgetElement1.CommandName = "groupDelete";
      commandWidgetElement1.Text = "Delete";
      commandWidgetElement1.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.CssClass = "sfGroupBtn";
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "PublishCommentsWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupPublish";
      commandWidgetElement2.Text = "Publish";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement3.Name = "HideCommentsWidget";
      commandWidgetElement3.ButtonType = CommandButtonType.Standard;
      commandWidgetElement3.CommandName = "groupHide";
      commandWidgetElement3.Text = "Hide";
      commandWidgetElement3.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.CssClass = "sfGroupBtn";
      CommandWidgetElement element4 = commandWidgetElement3;
      element1.Items.Add((WidgetElement) element4);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "MoreActionsWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (Labels).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element5 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "MarkSpamCommentsWidget";
      element6.Text = "MarkSpam";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "groupMarkSpam";
      element6.WidgetType = typeof (CommandWidget);
      element6.ResourceClassId = typeof (CommentsResources).Name;
      element6.CssClass = "sfPublishItm";
      menuItems.Add((WidgetElement) element6);
      element1.Items.Add((WidgetElement) element5);
      masterViewElement2.ToolbarConfig.Sections.Add(element1);
      WidgetBarSectionElement element7 = new WidgetBarSectionElement((ConfigElement) masterViewElement2.SidebarConfig.Sections)
      {
        Name = "FilterComments",
        Title = "FilterComments",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterSection"
      };
      ConfigElementList<WidgetElement> items1 = element7.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element7.Items);
      element8.Name = "AllComments";
      element8.CommandName = "showAllComments";
      element8.ButtonType = CommandButtonType.SimpleLinkButton;
      element8.Text = "AllComments";
      element8.ResourceClassId = typeof (CommentsResources).Name;
      element8.CssClass = "sfComments";
      element8.WidgetType = typeof (CommandWidget);
      element8.IsSeparator = false;
      element8.ButtonCssClass = "sfSel";
      items1.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> items2 = element7.Items;
      LiteralWidgetElement element9 = new LiteralWidgetElement((ConfigElement) element7.Items);
      element9.Name = "Separator";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.WidgetType = typeof (LiteralWidget);
      element9.CssClass = "sfSeparator";
      element9.Text = "&nbsp;";
      element9.IsSeparator = true;
      items2.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> items3 = element7.Items;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element7.Items);
      element10.Name = "WaitingApproval";
      element10.CommandName = "showWaitingApprovalItems";
      element10.ButtonType = CommandButtonType.SimpleLinkButton;
      element10.Text = "WaitingApproval";
      element10.ResourceClassId = typeof (CommentsResources).Name;
      element10.CssClass = "sfComments";
      element10.WidgetType = typeof (CommandWidget);
      element10.IsSeparator = false;
      items3.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> items4 = element7.Items;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element7.Items);
      element11.Name = "Published";
      element11.CommandName = "showPublishedItems";
      element11.ButtonType = CommandButtonType.SimpleLinkButton;
      element11.Text = "Published";
      element11.ResourceClassId = typeof (CommentsResources).Name;
      element11.CssClass = "sfComments";
      element11.WidgetType = typeof (CommandWidget);
      element11.IsSeparator = false;
      items4.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> items5 = element7.Items;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element7.Items);
      element12.Name = "Hidden";
      element12.CommandName = "showHiddenItems";
      element12.ButtonType = CommandButtonType.SimpleLinkButton;
      element12.Text = "Hidden";
      element12.ResourceClassId = typeof (CommentsResources).Name;
      element12.CssClass = "sfComments";
      element12.WidgetType = typeof (CommandWidget);
      element12.IsSeparator = false;
      items5.Add((WidgetElement) element12);
      ConfigElementList<WidgetElement> items6 = element7.Items;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element7.Items);
      element13.Name = "Spam";
      element13.CommandName = "showMarkedAsSpamItems";
      element13.ButtonType = CommandButtonType.SimpleLinkButton;
      element13.Text = "Spam";
      element13.ResourceClassId = typeof (CommentsResources).Name;
      element13.CssClass = "sfComments";
      element13.WidgetType = typeof (CommandWidget);
      element13.IsSeparator = false;
      items6.Add((WidgetElement) element13);
      ConfigElementList<WidgetElement> items7 = element7.Items;
      LiteralWidgetElement element14 = new LiteralWidgetElement((ConfigElement) element7.Items);
      element14.Name = "Separator";
      element14.WrapperTagKey = HtmlTextWriterTag.Li;
      element14.WidgetType = typeof (LiteralWidget);
      element14.CssClass = "sfSeparator";
      element14.Text = "&nbsp;";
      element14.IsSeparator = true;
      items7.Add((WidgetElement) element14);
      WidgetBarSectionElement element15 = new WidgetBarSectionElement((ConfigElement) masterViewElement2.SidebarConfig.Sections)
      {
        Name = "OtherFilterOptions",
        Title = "OtherFilterOptions",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "otherFilterOptions"
      };
      WidgetBarSectionElement element16 = new WidgetBarSectionElement((ConfigElement) masterViewElement2.SidebarConfig.Sections)
      {
        Name = "CommentsDateFilter",
        Title = "DisplayCommentsIn",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      masterViewElement2.SidebarConfig.Sections.Add(element16);
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element16.Items);
      commandWidgetElement4.Name = "CloseDateFilter";
      commandWidgetElement4.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement4.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element16.WrapperTagId);
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "CloseDateFilter";
      commandWidgetElement4.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement4.CssClass = "sfCloseFilter";
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      commandWidgetElement4.IsSeparator = false;
      CommandWidgetElement element17 = commandWidgetElement4;
      element16.Items.Add((WidgetElement) element17);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element16.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element18 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element18.PredefinedFilteringRanges);
      element16.Items.Add((WidgetElement) element18);
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element15.Items);
      commandWidgetElement5.Name = "FilterByDate";
      commandWidgetElement5.CommandName = "hideSectionsExcept";
      commandWidgetElement5.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element16.WrapperTagId);
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "ByDateModified";
      commandWidgetElement5.ResourceClassId = typeof (CommentsResources).Name;
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      commandWidgetElement5.CssClass = "sfComments";
      CommandWidgetElement element19 = commandWidgetElement5;
      element15.Items.Add((WidgetElement) element19);
      WidgetBarSectionElement element20 = new WidgetBarSectionElement((ConfigElement) masterViewElement2.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "Settings",
        ResourceClassId = typeof (CommentsResources).Name,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element20.Items);
      commandWidgetElement6.Name = "CommentsSettings";
      commandWidgetElement6.CommandName = "settings";
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "Settings";
      commandWidgetElement6.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      commandWidgetElement6.IsSeparator = false;
      CommandWidgetElement element21 = commandWidgetElement6;
      element20.Items.Add((WidgetElement) element21);
      masterViewElement2.SidebarConfig.ResourceClassId = typeof (CommentsResources).Name;
      masterViewElement2.SidebarConfig.Title = "ManageComments";
      masterViewElement2.SidebarConfig.Sections.Add(element7);
      masterViewElement2.SidebarConfig.Sections.Add(element15);
      masterViewElement2.SidebarConfig.Sections.Add(element20);
      CommentsModuleDefinitions.DefineDetailsDialog(masterViewElement2.DialogsConfig);
      return (ConfigElement) masterViewElement2;
    }

    private static void DefineDetailsDialog(ConfigElementList<DialogElement> dialogs) => dialogs.Add(new DialogElement((ConfigElement) dialogs)
    {
      OpenOnCommandName = "edit",
      Name = typeof (ViewContainerDialog).Name,
      Parameters = "?ControlDefinitionName={0}&ViewName={1}&ConfigType={2}".Arrange((object) "CommentsModuleBackend", (object) "CommentsModuleBackendDetailView", (object) typeof (CommentsModuleConfig).FullName),
      CssClass = "sfMaximizedWindow",
      InitialBehaviors = WindowBehaviors.Maximize,
      Width = Unit.Percentage(100.0),
      Height = Unit.Percentage(100.0)
    });

    private static bool ReviewsAreAllowed(ConfigElement element)
    {
      CommentsModuleConfig config = DefinitionsHelper.GetConfig<CommentsModuleConfig>(element);
      if (config.DefaultSettings.EnableRatings)
        return true;
      foreach (CommentsSettingsElement commentableType in (ConfigElementCollection) config.CommentableTypes)
      {
        if (commentableType.EnableRatings)
          return true;
      }
      return false;
    }
  }
}
