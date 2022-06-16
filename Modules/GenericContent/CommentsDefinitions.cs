// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.CommentsDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for the comments.
  /// </summary>
  [Obsolete("The backend UI for comments is configurable via CommentsModuleDefinitions.")]
  public class CommentsDefinitions
  {
    /// <summary>
    /// Name of the view used to display comments in a list.
    /// on the backend.
    /// </summary>
    public static string BackendCommentsListViewName = "CommentsBackendList";
    /// <summary>Name of the view used to edit comment on the backend.</summary>
    public static string BackendCommentsEditDetailsViewName = "CommentsBackendEdit";
    /// <summary>
    /// Name of the view used to preview comment on the backend.
    /// </summary>
    public const string BackendPreviewViewName = "CommentsBackendPreview";
    /// <summary>
    /// Default name for the comments master view on the frontend.
    /// </summary>
    public static readonly string FrontendCommentsMasterViewName = "CommentsMasterView";
    /// <summary>
    /// Default name for the comments details view on the frontend.
    /// </summary>
    public static readonly string FrontendCommentsDetailsViewName = "CommentsDetailsView";

    /// <summary>
    /// Defines the ContentView control for Generic Content comments on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <param name="controlDefinitionName">Name of the control definition.</param>
    /// <param name="defaultProviderName">Default name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns>
    /// A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.
    /// </returns>
    public static ContentViewControlElement DefineCommentsBackendContentView(
      ConfigElement parent,
      string controlDefinitionName,
      string defaultProviderName,
      Type managerType,
      string specificResourceClassId)
    {
      ContentViewControlDefinitionFacade definitionFacade1 = App.WorkWith().Module().DefineContainer(parent, controlDefinitionName, typeof (Comment)).SetManagerType(managerType).DoNotUseWorkflow();
      ContentViewControlElement viewControlElement = definitionFacade1.Get();
      MasterViewDefinitionFacade definitionFacade2 = definitionFacade1.AddMasterView(CommentsDefinitions.BackendCommentsListViewName).LocalizeUsing<ContentResources>().SetSortExpression("DateCreated DESC").SetTitle("Comments").SetServiceBaseUrl("~/Sitefinity/Services/Content/CommentsGenericService.svc/").SetExternalClientScripts(new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.CommentsMasterExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      });
      MasterGridViewElement element1 = definitionFacade2.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      ConfigElementList<WidgetElement> titleWidgetsConfig = element1.TitleWidgetsConfig;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.TitleWidgetsConfig);
      element2.Name = "ViewAllItems";
      element2.CommandName = "view";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.WidgetType = typeof (CommandWidget);
      element2.Text = "BackToItems";
      element2.ResourceClassId = specificResourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element2);
      definitionFacade2.AddEditDialog(CommentsDefinitions.BackendCommentsEditDetailsViewName, "", Res.Get<Labels>().BackToComments, "", (Type) null, "editComment").Done().AddInsertDialog(CommentsDefinitions.BackendCommentsEditDetailsViewName, "", "", "", (Type) null, "createComment");
      WidgetBarSectionElement element3 = new WidgetBarSectionElement((ConfigElement) element1.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element3.Items);
      commandWidgetElement1.Name = "DeleteCommentWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Standard;
      commandWidgetElement1.CommandName = "groupDelete";
      commandWidgetElement1.Text = "Delete";
      commandWidgetElement1.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element4 = commandWidgetElement1;
      element3.Items.Add((WidgetElement) element4);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element3.Items);
      commandWidgetElement2.Name = "HideCommentWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "GroupHideComments";
      commandWidgetElement2.Text = "Hide";
      commandWidgetElement2.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement2.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element5 = commandWidgetElement2;
      element3.Items.Add((WidgetElement) element5);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element3.Items);
      commandWidgetElement3.Name = "HideAndMarkAsSpamCommentWidget";
      commandWidgetElement3.ButtonType = CommandButtonType.Standard;
      commandWidgetElement3.CommandName = "GroupSpamComments";
      commandWidgetElement3.Text = "HideAndMarkAsSpam";
      commandWidgetElement3.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement3.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element6 = commandWidgetElement3;
      element3.Items.Add((WidgetElement) element6);
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element3.Items);
      commandWidgetElement4.Name = "PublishCommentWidget";
      commandWidgetElement4.ButtonType = CommandButtonType.Standard;
      commandWidgetElement4.CommandName = "GroupPublishComments";
      commandWidgetElement4.Text = "Publish";
      commandWidgetElement4.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement4.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element7 = commandWidgetElement4;
      element3.Items.Add((WidgetElement) element7);
      element1.ToolbarConfig.Sections.Add(element3);
      WidgetBarSectionElement element8 = new WidgetBarSectionElement((ConfigElement) element1.SidebarConfig.Sections)
      {
        Name = "Comments",
        Title = "Comments",
        ResourceClassId = typeof (ContentResources).Name,
        CssClass = "sfWidgetsList sfFirst sfSeparator sfModules"
      };
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement5.Name = "AllComments";
      commandWidgetElement5.CommandName = "ShowAllComments";
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "AllComments";
      commandWidgetElement5.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement5.CssClass = "sfComments";
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      CommandWidgetElement element9 = commandWidgetElement5;
      element8.Items.Add((WidgetElement) element9);
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement6.Name = "TodayComments";
      commandWidgetElement6.CommandName = "ShowTodayComments";
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "TodayComments";
      commandWidgetElement6.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement6.CssClass = "sfComments";
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      commandWidgetElement6.IsSeparator = false;
      CommandWidgetElement element10 = commandWidgetElement6;
      element8.Items.Add((WidgetElement) element10);
      CommandWidgetElement commandWidgetElement7 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement7.Name = "HiddenComments";
      commandWidgetElement7.CommandName = "ShowHiddenComments";
      commandWidgetElement7.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement7.Text = "Hidden";
      commandWidgetElement7.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement7.CssClass = "sfComments";
      commandWidgetElement7.WidgetType = typeof (CommandWidget);
      commandWidgetElement7.IsSeparator = false;
      CommandWidgetElement element11 = commandWidgetElement7;
      element8.Items.Add((WidgetElement) element11);
      CommandWidgetElement commandWidgetElement8 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement8.Name = "PublishedComments";
      commandWidgetElement8.CommandName = "ShowPublishedComments";
      commandWidgetElement8.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement8.Text = "Published";
      commandWidgetElement8.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement8.CssClass = "sfComments";
      commandWidgetElement8.WidgetType = typeof (CommandWidget);
      commandWidgetElement8.IsSeparator = false;
      CommandWidgetElement element12 = commandWidgetElement8;
      element8.Items.Add((WidgetElement) element12);
      CommandWidgetElement commandWidgetElement9 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement9.Name = "SpamComments";
      commandWidgetElement9.CommandName = "ShowSpamComments";
      commandWidgetElement9.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement9.Text = "Spam";
      commandWidgetElement9.ResourceClassId = typeof (ContentResources).Name;
      commandWidgetElement9.CssClass = "sfComments";
      commandWidgetElement9.WidgetType = typeof (CommandWidget);
      commandWidgetElement9.IsSeparator = false;
      CommandWidgetElement element13 = commandWidgetElement9;
      element8.Items.Add((WidgetElement) element13);
      element1.SidebarConfig.Title = "FilterAndEdit";
      element1.SidebarConfig.ResourceClassId = typeof (ContentResources).Name;
      element1.SidebarConfig.Sections.Add(element8);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element14 = gridViewModeElement;
      element1.ViewModesConfig.Add((ViewModeElement) element14);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element14.ColumnsConfig);
      dataColumnElement1.Name = "AuthorInfo";
      dataColumnElement1.HeaderText = "CommentsAuthorInfoColumnHeaderText";
      dataColumnElement1.ResourceClassId = typeof (ContentResources).Name;
      dataColumnElement1.HeaderCssClass = "sfMedium sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfCommentAuthor sfMedium sfTitleCol";
      dataColumnElement1.ClientTemplate = "<strong class='sfItemTitle'>{{AuthorName}}</strong><span class='sfCommentEmail'>{{Email}}</span><span><a href='{{Website}}'>{{Website}}</a></span><span class='sfCommentIP'>{{IpAddress}}</span>";
      DataColumnElement element15 = dataColumnElement1;
      element14.ColumnsConfig.Add((ColumnElement) element15);
      string sitefinityTextResource = ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.CommentsContentColumn.htm");
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element14.ColumnsConfig);
      dataColumnElement2.Name = "CommentContent";
      dataColumnElement2.HeaderText = "CommenstContentColumnHeaderText";
      dataColumnElement2.ResourceClassId = typeof (ContentResources).Name;
      dataColumnElement2.ClientTemplate = sitefinityTextResource;
      DataColumnElement element16 = dataColumnElement2;
      element14.ColumnsConfig.Add((ColumnElement) element16);
      DataColumnElement dataColumnElement3 = new DataColumnElement((ConfigElement) element14.ColumnsConfig);
      dataColumnElement3.Name = "CommentedItemLink";
      dataColumnElement3.HeaderText = "CommentedItemLinkHeaderText";
      dataColumnElement3.ResourceClassId = typeof (ContentResources).Name;
      dataColumnElement3.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_editItem' }}\">" + Res.Get<ContentResources>().CommentedItemLinkClientTemplate + "</a>";
      dataColumnElement3.HeaderCssClass = "sfRegular";
      dataColumnElement3.ItemCssClass = "sfRegular";
      DataColumnElement element17 = dataColumnElement3;
      element14.ColumnsConfig.Add((ColumnElement) element17);
      DetailViewDefinitionFacade definitionFacade3 = definitionFacade1.AddDetailView(CommentsDefinitions.BackendCommentsEditDetailsViewName).SetTitle("EditComment").HideTopToolbar().LocalizeUsing<ContentResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/CommentsGenericService.svc/");
      DetailFormViewElement detailFormViewElement = definitionFacade3.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) detailFormViewElement);
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade3.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement = definitionFacade4.Get();
      definitionFacade4.AddHtmlField("Content").SetDataFieldName("Content.PersistedValue").AddValidation().MakeRequired().SetRequiredViolationMessage("CommentCannotBeEmpty").Done().Done();
      definitionFacade4.AddLocalizedTextField("AuthorName").SetTitle("Name").SetCssClass("sfFormSeparator sfShortField250").AddValidation().MakeRequired().SetRequiredViolationMessage("NameCannotBeEmpty").Done();
      ConfigElementDictionary<string, FieldDefinitionElement> fields1 = viewSectionElement.Fields;
      CompositeFieldElement element18 = new CompositeFieldElement((ConfigElement) viewSectionElement.Fields);
      element18.FieldName = "Email";
      element18.Title = Res.Get<ContentResources>().Email;
      element18.CssClass = "sfFormSeparator sfShortField250";
      element18.DisplayMode = FieldDisplayMode.Write;
      element18.FieldType = typeof (BlockableEmailField);
      element18.ResourceClassId = typeof (ContentResources).Name;
      fields1.Add((FieldDefinitionElement) element18);
      definitionFacade4.AddTextField("Website").SetDataFieldName("Website").SetTitle("Website").SetCssClass("sfFormSeparator sfShortField250").Done();
      ConfigElementDictionary<string, FieldDefinitionElement> fields2 = viewSectionElement.Fields;
      DateFieldElement element19 = new DateFieldElement((ConfigElement) viewSectionElement.Fields);
      element19.DataFieldName = "DateCreated";
      element19.Title = "PostedOn";
      element19.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
      element19.CssClass = "sfFormSeparator";
      element19.ResourceClassId = typeof (ContentResources).Name;
      fields2.Add((FieldDefinitionElement) element19);
      ConfigElementDictionary<string, FieldDefinitionElement> fields3 = viewSectionElement.Fields;
      CompositeFieldElement element20 = new CompositeFieldElement((ConfigElement) viewSectionElement.Fields);
      element20.FieldName = "IpAddress";
      element20.Title = "IpAddress";
      element20.CssClass = "sfFormSeparator";
      element20.DisplayMode = FieldDisplayMode.Write;
      element20.FieldType = typeof (BlockableIpField);
      element20.ResourceClassId = typeof (ContentResources).Name;
      fields3.Add((FieldDefinitionElement) element20);
      definitionFacade3.AddSection("StatusSection").AddChoiceField("CommentStatus", RenderChoicesAs.RadioButtons).SetTitle("Status").SetDisplayMode(FieldDisplayMode.Write).AddChoicesFromEnumAsInts<CommentStatus>().Done();
      DefinitionsHelper.CreateCommentBackendFormToolbar(detailFormViewElement, typeof (ContentResources).Name);
      return viewControlElement;
    }

    /// <summary>Defines the comments frontend view.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="controlDefinitionName">Name of the control definition.</param>
    /// <param name="defaultProviderName">Default name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns>Default config</returns>
    public static ContentViewControlElement DefineCommentsFrontendView(
      ConfigElement parent,
      string controlDefinitionName,
      string defaultProviderName,
      Type managerType)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, controlDefinitionName, typeof (Comment)).SetProviderName(defaultProviderName).SetManagerType(managerType).Get();
      string name = typeof (ContentResources).Name;
      ContentViewMasterElement element1 = new ContentViewMasterElement((ConfigElement) viewControlElement.ViewsConfig);
      element1.Title = "MasterView";
      element1.ViewName = CommentsDefinitions.FrontendCommentsMasterViewName;
      element1.ViewType = typeof (CommentsMasterView);
      element1.DisplayMode = FieldDisplayMode.Read;
      element1.ResourceClassId = name;
      element1.AllowPaging = new bool?(false);
      element1.FilterExpression = "CommentStatus == Published";
      element1.SortExpression = "PublicationDate DESC";
      element1.WebServiceBaseUrl = "~/Sitefinity/Services/Content/CommentsGenericService.svc/";
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      ContentViewDetailElement element2 = new ContentViewDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element2.Title = "DetailsView";
      element2.ViewName = CommentsDefinitions.FrontendCommentsDetailsViewName;
      element2.ViewType = typeof (CommentsDetailsView);
      element2.ShowSections = new bool?(false);
      element2.DisplayMode = FieldDisplayMode.Read;
      element2.ResourceClassId = name;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      return viewControlElement;
    }
  }
}
