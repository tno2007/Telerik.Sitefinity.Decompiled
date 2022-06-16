// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.UserFiles.UserFilesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.UserFiles
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for Documents.
  /// </summary>
  internal static class UserFilesDefinitions
  {
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for user files on the backend.
    /// </summary>
    public static string BackendUserFilesDefinitionName = "UserFilesBackend";
    /// <summary>
    /// Name of the view used to display user files in a list on the backend.
    /// </summary>
    public static string BackendUserFilesListViewName = "UserFilesBackendList";
    /// <summary>
    /// Name of the view used to display user files in a insert screen on the backend.
    /// </summary>
    public static string BackendUserFilesInsertViewName = "UserFilesBackendInsertView";
    /// <summary>
    /// Name of the view used to display user files in a edit screen on the backend.
    /// </summary>
    public static string BackendUserFilesEditViewName = "UserFilesBackendEditView";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for user files documents on the backend.
    /// </summary>
    public static string BackendUserFileLibraryDocumentsDefinitionName = "BackendUserFileLibraryDocuments";
    public static string BackendUserFileLibraryDocumentsListViewName = "BackendUserFileDocumentsBackendList";
    public static string BackendUserFileLibraryDocumentsEditViewName = "BackendUserFileDocumentsBackendEditView";
    public static string BackendUserFileLibraryDocumentsInsertViewName = "BackendUserFileDocumentsBackendInsertView";
    /// <summary>Name of the bulk edit view in the Documents module.</summary>
    public const string BackendUserFileLibraryDocumentsBulkEditViewName = "BackendUserFileDocumentsBackendBulkEditView";
    public const string BackendUserFileLibraryDocumentsVersionPreviewViewName = "BackendUserFileDocumentsVersionPreviewViewView";
    public const string BackendUserFileLibraryDocumentsPreviewViewName = "BackendUserFileLibraryDocumentsPreviewViewName";
    public const string BackendUserFileLibraryDocumentsVersionComparisonView = "BackendUserFileLibraryDocumentsVersionComparisonView";
    /// <summary>The resource pageId for user files.</summary>
    public static readonly string ResourceClassId = typeof (UserFilesResources).Name;
    public static readonly string DocumentsResourceClassId = typeof (DocumentsResources).Name;
    public static readonly string LibrariesResourceClassId = typeof (LibrariesResources).Name;
    private static readonly Dictionary<string, string> clientMappedCommnadNames = new Dictionary<string, string>()
    {
      {
        "edit",
        "editMediaContentProperties"
      }
    };

    /// <summary>
    /// Defines the ContentView control for Documents on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineDocumentsBackendContentView(
      ConfigElement parent)
    {
      return UserFilesDefinitions.DefineUserFileDocumentsBackendContentView(parent, UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, false);
    }

    /// <summary>
    /// Defines the ContentView control for libraries on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineUserFileLibrariesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, UserFilesDefinitions.BackendUserFilesDefinitionName, typeof (DocumentLibrary));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(UserFilesDefinitions.BackendUserFilesListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineBackendLibraryListView((ConfigElement) backendContentView.ViewsConfig, UserFilesDefinitions.BackendUserFilesListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy(UserFilesDefinitions.BackendUserFilesInsertViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineBackendLibraryDetailsView(fluentContentView, UserFilesDefinitions.BackendUserFilesInsertViewName, true, false)));
      backendContentView.ViewsConfig.AddLazy(UserFilesDefinitions.BackendUserFilesEditViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineBackendLibraryDetailsView(fluentContentView, UserFilesDefinitions.BackendUserFilesEditViewName, false, true)));
      return backendContentView;
    }

    /// <summary>
    /// Defines the ContentView control for documents in a specific library on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineUserFileLibraryDocumentsBackendContentView(
      ConfigElement parent)
    {
      return UserFilesDefinitions.DefineUserFileDocumentsBackendContentView(parent, UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, true);
    }

    /// <summary>
    /// Defines the ContentView control for Documents on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    /// <param name="viewByLibrary">Flag if to display list with.</param>
    private static ContentViewControlElement DefineUserFileDocumentsBackendContentView(
      ConfigElement parent,
      string controlDefinitionName,
      bool viewByLibrary)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Document));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(UserFilesDefinitions.BackendUserFileLibraryDocumentsListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsBackendListView((ConfigElement) backendContentView.ViewsConfig, controlDefinitionName, UserFilesDefinitions.BackendUserFileLibraryDocumentsListViewName, viewByLibrary, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy(UserFilesDefinitions.BackendUserFileLibraryDocumentsEditViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsBackendDetailsView(fluentContentView, UserFilesDefinitions.BackendUserFileLibraryDocumentsEditViewName)));
      backendContentView.ViewsConfig.AddLazy("BackendUserFileDocumentsBackendBulkEditView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsBackendBulkEditView(fluentContentView, "BackendUserFileDocumentsBackendBulkEditView")));
      backendContentView.ViewsConfig.AddLazy("BackendUserFileDocumentsVersionPreviewViewView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsBackendVersionPreviewView(fluentContentView, "BackendUserFileDocumentsVersionPreviewViewView")));
      backendContentView.ViewsConfig.AddLazy("BackendUserFileLibraryDocumentsPreviewViewName", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsBackendPreviewView(fluentContentView, "BackendUserFileLibraryDocumentsPreviewViewName")));
      backendContentView.ViewsConfig.AddLazy("BackendUserFileLibraryDocumentsVersionComparisonView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) UserFilesDefinitions.DefineUserFileDocumentsVersionComparisonView((ConfigElement) backendContentView.ViewsConfig, "BackendUserFileLibraryDocumentsVersionComparisonView")));
      return backendContentView;
    }

    private static ComparisonViewElement DefineUserFileDocumentsVersionComparisonView(
      ConfigElement parent,
      string viewName)
    {
      ComparisonViewElement comparisonViewElement1 = new ComparisonViewElement(parent);
      comparisonViewElement1.Title = "VersionComparison";
      comparisonViewElement1.ViewName = viewName;
      comparisonViewElement1.ViewType = typeof (VersionComparisonView);
      comparisonViewElement1.DisplayMode = FieldDisplayMode.Read;
      comparisonViewElement1.ResourceClassId = UserFilesDefinitions.ResourceClassId;
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      ConfigElementDictionary<string, ComparisonFieldElement> fields1 = comparisonViewElement2.Fields;
      ComparisonFieldElement element1 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element1.FieldName = "Title";
      element1.Title = "Title";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      fields1.Add(element1);
      ConfigElementDictionary<string, ComparisonFieldElement> fields2 = comparisonViewElement2.Fields;
      ComparisonFieldElement element2 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element2.FieldName = "Description";
      element2.Title = "Description";
      element2.ResourceClassId = UserFilesDefinitions.ResourceClassId;
      fields2.Add(element2);
      ConfigElementDictionary<string, ComparisonFieldElement> fields3 = comparisonViewElement2.Fields;
      ComparisonFieldElement element3 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element3.FieldName = "Categories";
      element3.Title = "Categories";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      fields3.Add(element3);
      ConfigElementDictionary<string, ComparisonFieldElement> fields4 = comparisonViewElement2.Fields;
      ComparisonFieldElement element4 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element4.FieldName = "Tags";
      element4.Title = "Tags";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      fields4.Add(element4);
      ConfigElementDictionary<string, ComparisonFieldElement> fields5 = comparisonViewElement2.Fields;
      ComparisonFieldElement element5 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element5.FieldName = "Author";
      element5.Title = "Author";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      fields5.Add(element5);
      ConfigElementDictionary<string, ComparisonFieldElement> fields6 = comparisonViewElement2.Fields;
      ComparisonFieldElement element6 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element6.FieldName = "UrlName";
      element6.Title = "UrlName";
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      fields6.Add(element6);
      return comparisonViewElement2;
    }

    private static DetailFormViewElement DefineUserFileDocumentsBackendPreviewView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("PreviewUserFile").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<DocumentsResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").ShowNavigation().DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      UserFilesDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineUserFileDocumentsBackendVersionPreviewView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      Dictionary<string, string> localization = new Dictionary<string, string>()
      {
        {
          "ItemVersionOfClientTemplate",
          Res.Get<VersionResources>().ItemVersionOfClientTemplate
        },
        {
          "PreviouslyPublished",
          Res.Get<VersionResources>().PreviouslyPublishedBrackets
        },
        {
          "CannotDeleteLastPublishedVersion",
          Res.Get<VersionResources>().CannotDeleteLastPublishedVersion
        }
      };
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("UserFileVersionPreview").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<DocumentsResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").ShowNavigation().SetLocalization(localization).DoNotUseWorkflow().DoNotSupportMultilingual();
      DetailFormViewElement detailView = fluentDetailView.Get();
      UserFilesDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (DocumentsResources).Name);
      return detailView;
    }

    internal static MasterGridViewElement DefineUserFileDocumentsBackendListView(
      ConfigElement parent,
      string controlDefinitionName,
      string viewName,
      bool viewDocumentsByLibrary,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> extensionsScripts = UserFilesDefinitions.GetLibrariesMasterExtensionsScripts();
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).LocalizeUsing<UserFilesResources>().SetTitle("UserFilesModuleTitle").SetParentTitleFormat("UserFileDocumentsTitleFormat").SetCssClass("sfListViewGrid").SetExternalClientScripts(extensionsScripts).SetClientMappedCommnadNames(UserFilesDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").SetProvidersGroups("System");
      MasterGridViewElement gridView = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadDocumentWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "upload";
      element2.Text = "UploadFilesButton";
      element2.ResourceClassId = typeof (UserFilesResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "Document";
      element2.ActionName = "ManageDocument";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "DeleteDocumentWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "groupDelete";
      element3.Text = "Delete";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfGroupBtn";
      element3.PermissionSet = "Document";
      element3.ActionName = "ManageDocument";
      items2.Add((WidgetElement) element3);
      if (viewDocumentsByLibrary)
      {
        ConfigElementList<WidgetElement> items3 = element1.Items;
        CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
        element4.Name = "ReorderWidget";
        element4.ButtonType = CommandButtonType.Standard;
        element4.CommandName = "reorder";
        element4.Text = "ReorderDocuments";
        element4.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
        element4.WrapperTagKey = HtmlTextWriterTag.Li;
        element4.WidgetType = typeof (CommandWidget);
        items3.Add((WidgetElement) element4);
      }
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "MoreActionsWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (LibrariesResources).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element5 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "PublishDocumentsWidget";
      element6.Text = "Publish";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "groupPublish";
      element6.WidgetType = typeof (CommandWidget);
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      element6.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems2 = element5.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element7.Name = "UnpublishDocumentsWidget";
      element7.Text = "Unpublish";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "groupUnpublish";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems3 = element5.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element8.Name = "BulkEditWidget";
      element8.Text = "BulkEdit";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "bulkEdit";
      element8.WidgetType = typeof (CommandWidget);
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      menuItems3.Add((WidgetElement) element8);
      element1.Items.Add((WidgetElement) element5);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (Document)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Document), false, gridView.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Document), true, gridView.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.HeaderText = "SortDocuments";
      commandWidgetElement1.MoreLinkText = "More";
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement1.ContentType = typeof (Document);
      DynamicCommandWidgetElement element9 = commandWidgetElement1;
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element9.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element9.Items.Add(dynamicItemElement);
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element9.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element9.CustomItems.Add(dynamicItemElement);
      }
      element1.Items.Add((WidgetElement) element9);
      gridView.ToolbarConfig.Sections.Add(element1);
      LocalizationWidgetBarSectionElement barSectionElement1 = new LocalizationWidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections);
      barSectionElement1.Name = "LanguagesWidgetBar";
      barSectionElement1.Title = "Languages";
      barSectionElement1.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement1.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement1.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element10 = barSectionElement1;
      ConfigElementList<WidgetElement> items4 = element10.Items;
      LanguagesDropDownListWidgetElement element11 = new LanguagesDropDownListWidgetElement((ConfigElement) element10.Items);
      element11.Name = "LanguagesDropDown";
      element11.Text = "Languages";
      element11.ResourceClassId = typeof (LocalizationResources).Name;
      element11.CssClass = "";
      element11.WidgetType = typeof (LanguagesDropDownListWidget);
      element11.IsSeparator = false;
      element11.LanguageSource = LanguageSource.Frontend;
      element11.AddAllLanguagesOption = false;
      element11.CommandName = "changeLanguage";
      items4.Add((WidgetElement) element11);
      gridView.SidebarConfig.Sections.Add((WidgetBarSectionElement) element10);
      WidgetBarSectionElement element12 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "FilterDocuments",
        Title = viewDocumentsByLibrary ? "FilterDocumentsInThisFolder" : "FilterUserFiles",
        ResourceClassId = UserFilesDefinitions.ResourceClassId,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterDocSection"
      };
      ConfigElementList<WidgetElement> items5 = element12.Items;
      CommandWidgetElement element13 = new CommandWidgetElement((ConfigElement) element12.Items);
      element13.Name = "AllDocuments";
      element13.CommandName = "showAllItems";
      element13.ButtonType = CommandButtonType.SimpleLinkButton;
      element13.ButtonCssClass = "sfSel";
      element13.Text = "AllDocuments";
      element13.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      element13.CssClass = "";
      element13.WidgetType = typeof (CommandWidget);
      element13.IsSeparator = false;
      items5.Add((WidgetElement) element13);
      ConfigElementList<WidgetElement> items6 = element12.Items;
      CommandWidgetElement element14 = new CommandWidgetElement((ConfigElement) element12.Items);
      element14.Name = "MyDocuments";
      element14.CommandName = "showMyItems";
      element14.ButtonType = CommandButtonType.SimpleLinkButton;
      element14.Text = "MyDocuments";
      element14.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      element14.CssClass = "";
      element14.WidgetType = typeof (CommandWidget);
      element14.IsSeparator = false;
      items6.Add((WidgetElement) element14);
      gridView.SidebarConfig.Sections.Add(element12);
      if (!viewDocumentsByLibrary)
      {
        WidgetBarSectionElement element15 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
        {
          Name = "ByLibrary",
          Title = "DocumentsByLibrary",
          ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId,
          CssClass = "sfFilterBy sfSeparator",
          WrapperTagId = "docsByLibrarySection"
        };
        DynamicCommandWidgetElement commandWidgetElement2 = new DynamicCommandWidgetElement((ConfigElement) element15.Items);
        commandWidgetElement2.Name = "LibraryFilter";
        commandWidgetElement2.CommandName = "filterByLibrary";
        commandWidgetElement2.MoreLinkText = "ShowMoreLibraries";
        commandWidgetElement2.MoreLinkCssClass = "sfShowMore";
        commandWidgetElement2.LessLinkText = "ShowLessLibraries";
        commandWidgetElement2.PageSize = 10;
        commandWidgetElement2.Text = "DocumentsByLibrary";
        commandWidgetElement2.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
        commandWidgetElement2.CssClass = "sfFilterByAlbum sfDocFilter";
        commandWidgetElement2.WidgetType = typeof (DynamicCommandWidget);
        commandWidgetElement2.IsSeparator = false;
        commandWidgetElement2.BindTo = BindCommandListTo.Client;
        commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
        commandWidgetElement2.ClientItemTemplate = LibrariesDefinitions.GetByLibrarySectionClientTemplate(Res.Get<DocumentsResources>().DocumentSingularItemName, Res.Get<DocumentsResources>().DocumentPluralItemName);
        DynamicCommandWidgetElement element16 = commandWidgetElement2;
        element16.UrlParameters.Add("providerName", "");
        element16.UrlParameters.Add("itemType", "Telerik.Sitefinity.Libraries.Model.DocumentLibrary");
        element15.Items.Add((WidgetElement) element16);
        ConfigElementList<WidgetElement> items7 = element15.Items;
        CommandWidgetElement element17 = new CommandWidgetElement((ConfigElement) element15.Items);
        element17.Name = "ManageLibraries";
        element17.CommandName = "viewLibraries";
        element17.ButtonType = CommandButtonType.SimpleLinkButton;
        element17.Text = "ManageLibraries";
        element17.ResourceClassId = typeof (LibrariesResources).Name;
        element17.CssClass = "sfManageItems";
        element17.WidgetType = typeof (CommandWidget);
        element17.IsSeparator = false;
        items7.Add((WidgetElement) element17);
        gridView.SidebarConfig.Sections.Add(element15);
      }
      WidgetBarSectionElement element18 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "ByCategory",
        Title = "DocumentsByCategory",
        ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "categoryFilterSection",
        Visible = new bool?(false)
      };
      gridView.SidebarConfig.Sections.Add(element18);
      WidgetBarSectionElement element19 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "ByTag",
        Title = "DocumentsByTag",
        ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "tagFilterSection",
        Visible = new bool?(false)
      };
      gridView.SidebarConfig.Sections.Add(element19);
      WidgetBarSectionElement element20 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "Filter",
        Title = "DocumentsByDate",
        ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      gridView.SidebarConfig.Sections.Add(element20);
      ConfigElementList<WidgetElement> items8 = element18.Items;
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element18.Items);
      commandWidgetElement3.Name = "CloseCategories";
      commandWidgetElement3.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element18.WrapperTagId, element19.WrapperTagId, element20.WrapperTagId);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "CloseCategories";
      commandWidgetElement3.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement3.CssClass = "sfCloseFilter";
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element21 = commandWidgetElement3;
      items8.Add((WidgetElement) element21);
      DynamicCommandWidgetElement commandWidgetElement4 = new DynamicCommandWidgetElement((ConfigElement) element18.Items);
      commandWidgetElement4.Name = "CategoryFilter";
      commandWidgetElement4.CommandName = "filterByCategory";
      commandWidgetElement4.PageSize = 0;
      commandWidgetElement4.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement4.IsSeparator = false;
      commandWidgetElement4.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement4.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", (object) TaxonomyManager.CategoriesTaxonomyId);
      commandWidgetElement4.ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/";
      commandWidgetElement4.PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/";
      commandWidgetElement4.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement4.MoreLinkText = "ShowMoreCategories";
      commandWidgetElement4.MoreLinkCssClass = "sfShowMore";
      commandWidgetElement4.LessLinkText = "ShowLessCategories";
      commandWidgetElement4.LessLinkCssClass = "sfShowMore";
      commandWidgetElement4.ClientItemTemplate = "<a href='javascript:void(0);' class='sf_binderCommand_filterByCategory'>{{ Title }}</a> <span class='sfCount'>({{ItemsCount}})</span>";
      DynamicCommandWidgetElement element22 = commandWidgetElement4;
      element22.UrlParameters.Add("itemType", typeof (Document).FullName);
      element18.Items.Add((WidgetElement) element22);
      ConfigElementList<WidgetElement> items9 = element20.Items;
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element20.Items);
      commandWidgetElement5.Name = "CloseDateFilter";
      commandWidgetElement5.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement5.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element20.WrapperTagId, element18.WrapperTagId, element19.WrapperTagId);
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "CloseDateFilter";
      commandWidgetElement5.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      commandWidgetElement5.CssClass = "sfCloseFilter";
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      CommandWidgetElement element23 = commandWidgetElement5;
      items9.Add((WidgetElement) element23);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element20.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element24 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element24.PredefinedFilteringRanges);
      element20.Items.Add((WidgetElement) element24);
      ConfigElementList<WidgetElement> items10 = element19.Items;
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element19.Items);
      commandWidgetElement6.Name = "CloseTags";
      commandWidgetElement6.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement6.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element19.WrapperTagId, element18.WrapperTagId, element20.WrapperTagId);
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "CloseTags";
      commandWidgetElement6.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement6.CssClass = "sfCloseFilter";
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      commandWidgetElement6.IsSeparator = false;
      CommandWidgetElement element25 = commandWidgetElement6;
      items10.Add((WidgetElement) element25);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<a href=\"javascript:void(0);\" class=\"sf_binderCommand_filterByTag");
      stringBuilder.Append("\">{{Title}}</a> <span class='sfCount'>({{ItemsCount}})</span>");
      DynamicCommandWidgetElement commandWidgetElement7 = new DynamicCommandWidgetElement((ConfigElement) element19.Items);
      commandWidgetElement7.Name = "TagFilter";
      commandWidgetElement7.CommandName = "filterByTag";
      commandWidgetElement7.PageSize = 30;
      commandWidgetElement7.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement7.IsSeparator = false;
      commandWidgetElement7.BindTo = BindCommandListTo.Client;
      commandWidgetElement7.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", (object) TaxonomyManager.TagsTaxonomyId);
      commandWidgetElement7.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement7.MoreLinkText = "ShowMoreTags";
      commandWidgetElement7.MoreLinkCssClass = "sfShowMore";
      commandWidgetElement7.LessLinkText = "ShowLessTags";
      commandWidgetElement7.LessLinkCssClass = "sfShowMore";
      commandWidgetElement7.SelectedItemCssClass = "sfSel";
      commandWidgetElement7.ClientItemTemplate = stringBuilder.ToString();
      DynamicCommandWidgetElement element26 = commandWidgetElement7;
      element26.UrlParameters.Add("itemType", typeof (Document).FullName);
      element19.Items.Add((WidgetElement) element26);
      WidgetBarSectionElement barSectionElement2 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "FilterOptions",
        Title = "OtherFilterOptions",
        ResourceClassId = typeof (LibrariesResources).Name,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "moreFiltersSection"
      };
      ConfigElementList<WidgetElement> items11 = barSectionElement2.Items;
      CommandWidgetElement element27 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element27.Name = "ShowDraftDocuments";
      element27.CommandName = "showMasterItems";
      element27.ButtonType = CommandButtonType.SimpleLinkButton;
      element27.Text = "DraftDocuments";
      element27.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      element27.CssClass = "";
      element27.WidgetType = typeof (CommandWidget);
      element27.IsSeparator = false;
      items11.Add((WidgetElement) element27);
      ConfigElementList<WidgetElement> items12 = barSectionElement2.Items;
      CommandWidgetElement element28 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element28.Name = "ShowPublishedDocuments";
      element28.CommandName = "showPublishedItems";
      element28.ButtonType = CommandButtonType.SimpleLinkButton;
      element28.Text = "PublishedDocuments";
      element28.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      element28.CssClass = "";
      element28.WidgetType = typeof (CommandWidget);
      element28.IsSeparator = false;
      items12.Add((WidgetElement) element28);
      ConfigElementList<WidgetElement> items13 = barSectionElement2.Items;
      CommandWidgetElement element29 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element29.Name = "ShowScheduledDocuments";
      element29.CommandName = "showScheduledItems";
      element29.ButtonType = CommandButtonType.SimpleLinkButton;
      element29.Text = "ScheduledDocuments";
      element29.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      element29.CssClass = "";
      element29.WidgetType = typeof (CommandWidget);
      element29.IsSeparator = false;
      items13.Add((WidgetElement) element29);
      ConfigElementList<WidgetElement> items14 = barSectionElement2.Items;
      CommandWidgetElement element30 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element30.Name = "PendingApprovalDocuments";
      element30.CommandName = "showPendingApprovalItems";
      element30.ButtonType = CommandButtonType.SimpleLinkButton;
      element30.Text = "WaitingForApproval";
      element30.ResourceClassId = typeof (LibrariesResources).Name;
      element30.CssClass = "";
      element30.WidgetType = typeof (CommandWidget);
      element30.IsSeparator = false;
      items14.Add((WidgetElement) element30);
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.CategoriesTaxonomyId, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element18.WrapperTagId), barSectionElement2);
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.TagsTaxonomyId, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element19.WrapperTagId), barSectionElement2);
      ConfigElementList<WidgetElement> items15 = barSectionElement2.Items;
      CommandWidgetElement commandWidgetElement8 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      commandWidgetElement8.Name = "FilterByDate";
      commandWidgetElement8.CommandName = "hideSectionsExcept";
      commandWidgetElement8.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element20.WrapperTagId);
      commandWidgetElement8.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement8.Text = "ByDate";
      commandWidgetElement8.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement8.CssClass = "";
      commandWidgetElement8.WidgetType = typeof (CommandWidget);
      commandWidgetElement8.IsSeparator = false;
      CommandWidgetElement element31 = commandWidgetElement8;
      items15.Add((WidgetElement) element31);
      gridView.SidebarConfig.Title = "ManageUserFiles";
      gridView.SidebarConfig.ResourceClassId = typeof (UserFilesResources).Name;
      gridView.SidebarConfig.Sections.Add(barSectionElement2);
      WidgetBarSectionElement element32 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForUserFiles",
        ResourceClassId = UserFilesDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items16 = element32.Items;
      CommandWidgetElement element33 = new CommandWidgetElement((ConfigElement) element32.Items);
      element33.Name = "Permissions";
      element33.CommandName = "permissions";
      element33.ButtonType = CommandButtonType.SimpleLinkButton;
      element33.Text = "Permissions";
      element33.ResourceClassId = typeof (LibrariesResources).Name;
      element33.WidgetType = typeof (CommandWidget);
      element33.IsSeparator = false;
      items16.Add((WidgetElement) element33);
      gridView.SidebarConfig.Sections.Add(element32);
      WidgetBarSectionElement element34 = new WidgetBarSectionElement((ConfigElement) gridView.ContextBarConfig.Sections)
      {
        Name = "contextBar"
      };
      element34.WrapperTagKey = HtmlTextWriterTag.Div;
      element34.CssClass = "sfContextWidgetWrp";
      ConfigElementList<WidgetElement> items17 = element34.Items;
      LibraryWidgetElement element35 = new LibraryWidgetElement((ConfigElement) element34.Items);
      element35.Name = "LibraryHeader";
      element35.WrapperTagKey = HtmlTextWriterTag.Div;
      element35.CssClass = "sfSelectedLibrary sfSelectedDocLibrary";
      element35.WidgetType = typeof (LibraryWidget);
      element35.Text = "This is a test CI widget";
      element35.ContentItemType = typeof (DocumentLibrary);
      element35.ShowActionMenu = true;
      element35.ItemName = Res.Get<DocumentsResources>().Document;
      element35.ItemsName = Res.Get<DocumentsResources>().Documents;
      element35.LibraryName = Res.Get<DocumentsResources>().Library;
      element35.ServiceBaseUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
      element35.SupportsReordering = false;
      items17.Add((WidgetElement) element35);
      gridView.ContextBarConfig.Sections.Add(element34);
      LocalizationWidgetBarSectionElement barSectionElement3 = new LocalizationWidgetBarSectionElement((ConfigElement) gridView.ContextBarConfig.Sections);
      barSectionElement3.Name = "translations";
      barSectionElement3.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement3.CssClass = "sfContextWidgetWrp";
      barSectionElement3.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element36 = barSectionElement3;
      ConfigElementList<WidgetElement> items18 = element36.Items;
      CommandWidgetElement element37 = new CommandWidgetElement((ConfigElement) element36.Items);
      element37.Name = "ShowMoreTranslations";
      element37.CommandName = "showMoreTranslations";
      element37.ButtonType = CommandButtonType.SimpleLinkButton;
      element37.Text = "ShowAllTranslations";
      element37.ResourceClassId = typeof (LocalizationResources).Name;
      element37.WidgetType = typeof (CommandWidget);
      element37.IsSeparator = false;
      items18.Add((WidgetElement) element37);
      ConfigElementList<WidgetElement> items19 = element36.Items;
      CommandWidgetElement element38 = new CommandWidgetElement((ConfigElement) element36.Items);
      element38.Name = "HideMoreTranslations";
      element38.CommandName = "hideMoreTranslations";
      element38.ButtonType = CommandButtonType.SimpleLinkButton;
      element38.Text = "ShowBasicTranslationsOnly";
      element38.ResourceClassId = typeof (LocalizationResources).Name;
      element38.WidgetType = typeof (CommandWidget);
      element38.IsSeparator = false;
      element38.CssClass = "sfDisplayNone";
      items19.Add((WidgetElement) element38);
      gridView.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element36);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) gridView.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element39 = gridViewModeElement;
      gridView.ViewModesConfig.Add((ViewModeElement) element39);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element39.ColumnsConfig;
      DataColumnElement element40 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      element40.Name = "Icon";
      element40.HeaderCssClass = "sfFileIcon";
      element40.ItemCssClass = "sfFileIcon";
      element40.ClientTemplate = "<span sys:if='!IsFolder' sys:class=\"{{ 'sfext sf' + Extension.substring(1).toLowerCase()}}\" sys:title=\"{{Extension.substring(1).toLowerCase()}}\">{{Extension.substring(1).toLowerCase()}}</span>\r\n                                        <span sys:if='IsFolder' sys:class=\"{{(!(DocumentsCount == 0 && LibrariesCount == 0) ? 'sfDocIcnSmall' : 'sfDocIcnSmall sfEmptyDocIcn') }}\"></span>";
      columnsConfig1.Add((ColumnElement) element40);
      string empty = string.Empty;
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element39.ColumnsConfig;
      DataColumnElement element41 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      element41.Name = "Title";
      element41.HeaderText = "TitleStatus";
      element41.ResourceClassId = typeof (DocumentsResources).Name;
      element41.HeaderCssClass = "sfTitleCol";
      element41.ItemCssClass = "sfTitleCol";
      element41.ClientTemplate = "<span sys:if='!IsFolder'><strong class='sfItemTitle'>\r\n                                        <a sys:href='javascript:void(0);' class=\"sf_binderCommand_editMediaContentProperties\">{{Title.htmlEncode()}}</a>\r\n                                    </strong>\r\n                                    <span sys:class=\"{{ 'sf' + UIStatus.toLowerCase()}}\">{{Status}}</span></span><span sys:if='IsFolder'><strong class='sfItemTitle sfMBottom5'>\r\n                                        <a href='javascript:void(0);' class=\"sf_binderCommand_openLibrary\">{{Title.htmlEncode()}}</a>\r\n                                    </strong>\r\n                                    <i sys:if='DocumentsCount' class='sfItemsCount'>{{ DocumentsCount }}</i><i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i></span>";
      columnsConfig2.Add((ColumnElement) element41);
      string end = new StreamReader(Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.UserFilesDocumentsActionMenuColumn.htm")).ReadToEnd();
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element39.ColumnsConfig);
      dynamicColumnElement1.Name = "Translations";
      dynamicColumnElement1.HeaderText = "Translations";
      dynamicColumnElement1.ResourceClassId = typeof (LocalizationResources).Name;
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
      dynamicColumnElement1.HeaderCssClass = "sfLanguagesCol";
      dynamicColumnElement1.ItemCssClass = "sfLanguagesCol";
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
      element39.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      dataColumnElement1.Name = "Status";
      dataColumnElement1.HeaderText = "Actions";
      dataColumnElement1.ResourceClassId = typeof (LibrariesResources).Name;
      dataColumnElement1.HeaderCssClass = "sfRegular";
      dataColumnElement1.ItemCssClass = "sfRegular";
      dataColumnElement1.ClientTemplate = end;
      DataColumnElement element42 = dataColumnElement1;
      element39.ColumnsConfig.Add((ColumnElement) element42);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element39.ColumnsConfig;
      DataColumnElement element43 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      element43.Name = "FileDimSize";
      element43.HeaderText = "FileDimSize";
      element43.ResourceClassId = typeof (LibrariesResources).Name;
      element43.ClientTemplate = "<span sys:if='!IsFolder'><p class='sfLine'>{{Extension.substring(1).toUpperCase()}}</p><p class='sfLine'>{{TotalSize}} KB</p></span>";
      element43.HeaderCssClass = "sfRegular";
      element43.ItemCssClass = "sfRegular";
      columnsConfig3.Add((ColumnElement) element43);
      string str = string.Format("<p class='sfLine'>{0} <a sys:href='javascript:void(0);' class='sf_binderCommand_filter'>{1}</a></p><p class='sfLine'>{2} {3}</p><p class='sfLine'>{4} {5}</p>", (object) Res.Get<LibrariesResources>().LibraryLabel, (object) "{{LibraryTitle.htmlEncode()}}", (object) Res.Get<LibrariesResources>().Categories, (object) "{{CategoryText}}", (object) Res.Get<LibrariesResources>().Tags, (object) "{{TagsText}}");
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element39.ColumnsConfig;
      DataColumnElement element44 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      element44.Name = "LibraryCategoriesTags";
      element44.HeaderText = "LibraryCategoriesTags";
      element44.ResourceClassId = typeof (DocumentsResources).Name;
      element44.ClientTemplate = "<span sys:if='!IsFolder'>" + str + "</span>";
      element44.HeaderCssClass = "sfMedium";
      element44.ItemCssClass = "sfMedium";
      columnsConfig4.Add((ColumnElement) element44);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      dataColumnElement2.Name = "Owner";
      dataColumnElement2.HeaderText = "Owner";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "<span sys:if='!IsFolder' class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement2.HeaderCssClass = "sfRegular";
      dataColumnElement2.ItemCssClass = "sfRegular";
      DataColumnElement element45 = dataColumnElement2;
      element39.ColumnsConfig.Add((ColumnElement) element45);
      DataColumnElement dataColumnElement3 = new DataColumnElement((ConfigElement) element39.ColumnsConfig);
      dataColumnElement3.Name = "Date";
      dataColumnElement3.HeaderText = "LastModified";
      dataColumnElement3.ResourceClassId = typeof (Labels).Name;
      dataColumnElement3.ClientTemplate = "<span sys:if='!IsFolder'>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss') : '-' }}</span>";
      dataColumnElement3.HeaderCssClass = "sfDateAndHour";
      dataColumnElement3.ItemCssClass = "sfDateAndHour";
      DataColumnElement element46 = dataColumnElement3;
      element39.ColumnsConfig.Add((ColumnElement) element46);
      DecisionScreenElement element47 = new DecisionScreenElement((ConfigElement) gridView.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoItemsExist",
        ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId
      };
      ConfigElementList<CommandWidgetElement> actions = element47.Actions;
      CommandWidgetElement element48 = new CommandWidgetElement((ConfigElement) element47.Actions);
      element48.Name = "UploadDocuments";
      element48.ButtonType = CommandButtonType.Create;
      element48.CommandName = "upload";
      element48.Text = "UploadDocumentsOrOtherFiles";
      element48.ResourceClassId = typeof (DocumentsResources).Name;
      element48.CssClass = "sfCreateItem";
      element48.PermissionSet = "Document";
      element48.ActionName = "ManageDocument";
      actions.Add(element48);
      gridView.DecisionScreensConfig.Add(element47);
      string parameters1 = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&showOnlySystemLibraries={5}", (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().Documents, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Document).FullName, (object) typeof (DocumentLibrary).FullName, (object) true);
      string parameters2 = string.Format("?itemName={0}&itemNameWithArticle={1}&itemsName={2}&libraryTypeName={3}&itemType={4}&parentType={5}", (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().DocumentWithArticle, (object) Res.Get<DocumentsResources>().DocumentsOnly, (object) Res.Get<DocumentsResources>().Library, (object) typeof (Document).FullName, (object) typeof (DocumentLibrary).FullName);
      definitionFacade.AddUploadDialog("SystemLibrariesProvider", Res.Get<LibrariesResources>().DocumentItemName, Res.Get<DocumentsResources>().Documents, Res.Get<LibrariesResources>().Library, typeof (DocumentLibrary), "~/Sitefinity/Services/Content/DocumentService.svc/", "~/Sitefinity/Services/Content/DocumentLibraryService.svc/").Done().AddEditDialog(UserFilesDefinitions.BackendUserFileLibraryDocumentsEditViewName, "", Res.Get<DocumentsResources>().BackToItems, "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddInsertDialog(UserFilesDefinitions.BackendUserFileLibraryDocumentsInsertViewName, UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, Res.Get<DocumentsResources>().BackToItems).Done().AddEditDialog(UserFilesDefinitions.BackendUserFileLibraryDocumentsEditViewName, UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, Res.Get<DocumentsResources>().BackToItems, "", (Type) null, "editLibraryProperties").Done().AddEditDialog("BackendUserFileDocumentsBackendBulkEditView", "", "", "", (Type) null, "bulkEdit").Done().AddDialog<LibrarySelectorDialog>("selectLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters1).Done().AddDialog<EmbedDialog>("embedMediaContent").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(425)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters("?mode=documents").Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().PermissionsForDocuments, "DocumentLibrary, Document").Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().PermissionsForDocuments, "DocumentLibrary, Document", "libraryPermissionsDialog", typeof (DocumentLibrary)).Done().AddDialog<CustomSortingDialog>("editCustomSortingExpression").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).DisplayTitleBar().SetWidth(Unit.Pixel(500)).SetHeight(Unit.Pixel(600)).MakeModal().SetParameters("?contentType=" + typeof (Document).FullName).Done().AddHistoryComparisonDialog("BackendUserFileLibraryDocumentsVersionComparisonView", "", Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryGridDialog("BackendUserFileLibraryDocumentsVersionComparisonView", "", Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryPreviewDialog("BackendUserFileDocumentsVersionPreviewViewView", "", Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddPreviewDialog("BackendUserFileLibraryDocumentsPreviewViewName", "", "", "{{ParentId}}", typeof (DocumentLibrary)).Done().AddDialog<ReorderDialog>("reorder").MakeFullScreen().SetParameters(parameters2).Done().AddCustomFieldsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().DocumentsDataFields, Res.Get<DocumentsResources>().DocumentPluralItemName);
      ConfigElementList<WidgetElement> titleWidgetsConfig = gridView.TitleWidgetsConfig;
      CommandWidgetElement element49 = new CommandWidgetElement((ConfigElement) gridView.TitleWidgetsConfig);
      element49.Name = "ViewAllDocumentsCommand";
      element49.CommandName = "view";
      element49.ButtonType = CommandButtonType.SimpleLinkButton;
      element49.WidgetType = typeof (CommandWidget);
      element49.Text = "BackToItems";
      element49.ResourceClassId = UserFilesDefinitions.ResourceClassId;
      element49.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element49);
      gridView.LinksConfig.Add(new LinkElement((ConfigElement) gridView.LinksConfig)
      {
        Name = "ViewAllDocumentLink",
        CommandName = "view",
        NavigateUrl = RouteHelper.CreateNodeReference(UserFilesConstants.UserFilesNodeId)
      });
      DefinitionsHelper.CreateNotImplementedLink(gridView);
      return gridView;
    }

    internal static DetailFormViewElement DefineUserFileDocumentsBackendDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibrariesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("EditUserFile").LocalizeUsing<DocumentsResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").SetAlternativeTitle("CreateItem").DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      UserFilesDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Write);
      return detailFormViewElement;
    }

    private static void CreateBackendSections(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      if (fluentDetailView.Get().ViewName == UserFilesDefinitions.BackendUserFileLibraryDocumentsEditViewName)
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      fluentDetailView.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade1.Get();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade2 = definitionFacade1.AddLocalizedTextField("Title").SetId("DocumentTitleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").LocalizeUsing<LibrariesResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      definitionFacade1.AddLanguageChoiceFieldAndContinue("AvailableLanguages");
      if (displayMode == FieldDisplayMode.Read)
      {
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
        FileFieldDefinitionElement element = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
        element.ID = "FileFieldControl";
        element.DataFieldName = "MediaUrl";
        element.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
        element.CssClass = "";
        element.WrapperTag = HtmlTextWriterTag.Li;
        element.FieldType = typeof (FileField);
        element.LibraryContentType = typeof (Document);
        element.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
        element.ItemName = "Document";
        element.ItemNamePlural = "Documents";
        element.IsMultiselect = false;
        element.MaxFileCount = 1;
        fields.Add((FieldDefinitionElement) element);
      }
      if (displayMode == FieldDisplayMode.Write)
      {
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
        FileFieldDefinitionElement element = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
        element.ID = "FileFieldControl";
        element.DataFieldName = "MediaUrl";
        element.DisplayMode = new FieldDisplayMode?(displayMode);
        element.CssClass = "";
        element.WrapperTag = HtmlTextWriterTag.Li;
        element.FieldType = typeof (FileField);
        element.LibraryContentType = typeof (Document);
        element.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
        element.ItemName = "Document";
        element.ItemNamePlural = "Documents";
        element.IsMultiselect = false;
        element.MaxFileCount = 1;
        fields.Add((FieldDefinitionElement) element);
      }
      ContentViewSectionElement viewSectionElement2 = fluentDetailView.AddExpandableSection("TaxonomiesSection").SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element1 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element1);
      FlatTaxonFieldDefinitionElement element2 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      element2.CssClass = "sfFormSeparator";
      element2.Description = "TagsFieldInstructions";
      element2.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      fluentDetailView.AddExpandableSection("DetailsSection").SetTitle("Details").AddLocalizedTextField("Author").SetId("AuthorFieldControl").SetTitle("Author").LocalizeUsing<LibrariesResources>().Done().AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetRows(5).Done().AddLocalizedTextField("Parts").SetId("PartsFieldControl").SetTitle("Parts").SetExample("PartsExample").SetCssClass("sfFormSeparator").Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade3 = fluentDetailView.AddExpandableSection("AdvancedSection").SetTitle("ImageAdvancedSection").LocalizeUsing<LibrariesResources>();
      definitionFacade3.AddMirrorTextField("UrlName").SetTitle("UrlName").SetId("urlName").SetMirroredControlId(definitionFacade2.Get().ID).SetRegularExpressionFilter(DefinitionsHelper.UrlRegularExpressionFilterForLibraries).AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForLibrariesContentValidator).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").Done().SetLocalizableDataFieldName("UrlName");
      string str = "$Context.AllowMultipleUrls";
      string dataFieldName1 = "$Context.AdditionalUrlNames";
      string fieldName = "$Context.AdditionalUrlsRedirectToDefault";
      string dataFieldName2 = "$Context.DefaultUrl";
      definitionFacade3.AddExpandableField("multipleUrlsExpandableField").SetId("multipleUrlsExpandableField").SetDataFieldName(str).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(str, "AllowMultipleURLsForThisItem", false).SetId("allowMultipleUrlsFieldElement").Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(dataFieldName1).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(fieldName, "AllAditionalUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultUrlNameTextField").SetDataFieldName(dataFieldName2).SetId("defaultUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
      if (displayMode == FieldDisplayMode.Write)
      {
        ContentViewSectionElement viewSectionElement3 = fluentDetailView.AddReadOnlySection("SidebarSection").Get();
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement3.Fields;
        ContentWorkflowStatusInfoFieldElement element3 = new ContentWorkflowStatusInfoFieldElement((ConfigElement) viewSectionElement3.Fields);
        element3.DisplayMode = new FieldDisplayMode?(displayMode);
        element3.FieldName = "DocumentsWorkflowStatusInfoField";
        element3.ResourceClassId = typeof (DocumentsResources).Name;
        element3.WrapperTag = HtmlTextWriterTag.Li;
        element3.FieldType = typeof (ContentWorkflowStatusInfoField);
        fields.Add((FieldDefinitionElement) element3);
      }
      if (displayMode != FieldDisplayMode.Write)
        return;
      DefinitionsHelper.CreateBackendFormToolbar(fluentDetailView.Get(), typeof (DocumentsResources).Name, false, "ThisItem", true, false);
    }

    private static DetailFormViewElement DefineUserFileDocumentsBackendBulkEditView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade = fluentContentView.AddDetailView(viewName).SetTitle("BulkEditUserFiles").LocalizeUsing<LibrariesResources>().UnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade.Get();
      ContentViewSectionElement viewSectionElement1 = definitionFacade.AddSection("CommonDataSection").SetCssClass("sfBulkEdit sfFirstForm").Get();
      LibrarySelectorFieldDefinitionElement definitionElement1 = new LibrarySelectorFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      definitionElement1.ID = "LibraryFieldControl";
      definitionElement1.DataFieldName = "Library";
      definitionElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement1.Title = "CommonLibrary";
      definitionElement1.CssClass = "sfChangeAlbum";
      definitionElement1.ResourceClassId = typeof (DocumentsResources).Name;
      definitionElement1.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement1.ContentType = typeof (DocumentLibrary);
      definitionElement1.ShowOnlySystemLibraries = true;
      LibrarySelectorFieldDefinitionElement definitionElement2 = definitionElement1;
      definitionElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) definitionElement2)
      {
        Expanded = new bool?(false),
        ExpandText = "ChangeLibraryInSpan",
        ResourceClassId = typeof (DocumentsResources).Name
      };
      viewSectionElement1.Fields.Add((FieldDefinitionElement) definitionElement2);
      HierarchicalTaxonFieldDefinitionElement element1 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement1.Fields);
      element1.CssClass = "sfFormSeparator";
      element1.Title = "CommonCategories";
      element1.ExpandableDefinitionConfig.ExpandText = "ClickToAddCommonCategories";
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element1);
      FlatTaxonFieldDefinitionElement element2 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement1.Fields);
      element2.CssClass = "sfFormSeparator";
      element2.Description = "TagsFieldInstructions";
      element2.Title = "CommonTags";
      element2.ExpandableDefinitionConfig.Expanded = new bool?(true);
      viewSectionElement1.Fields.Add((FieldDefinitionElement) element2);
      ContentViewSectionElement viewSectionElement2 = definitionFacade.AddSection("SpecificDataSection").SetCssClass("sfBulkEdit").Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement2.Fields;
      BulkEditFieldElement element3 = new BulkEditFieldElement((ConfigElement) viewSectionElement2.Fields);
      element3.FieldName = "DocumentsBulkEditControl";
      element3.TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Documents.BulkEditFieldControl.ascx");
      element3.DisplayMode = FieldDisplayMode.Write;
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentService.svc/";
      element3.ContentType = typeof (Document);
      element3.ParentType = typeof (DocumentLibrary);
      fields.Add((FieldDefinitionElement) element3);
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "bulkEdit",
        WrapperTagKey = HtmlTextWriterTag.Div
      };
      ConfigElementList<WidgetElement> items1 = element4.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.Items);
      element5.Name = "SaveChangesWidgetElement";
      element5.ButtonType = CommandButtonType.Save;
      element5.CommandName = "save";
      element5.Text = "SaveChanges";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      element5.WrapperTagKey = HtmlTextWriterTag.Span;
      element5.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> items2 = element4.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.Items);
      element6.Name = "CancelWidgetElement";
      element6.ButtonType = CommandButtonType.Cancel;
      element6.CommandName = "cancel";
      element6.Text = "Cancel";
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      element6.WrapperTagKey = HtmlTextWriterTag.Span;
      element6.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element6);
      detailFormViewElement.Toolbar.Sections.Add(element4);
      return detailFormViewElement;
    }

    private static MasterGridViewElement DefineBackendLibraryListView(
      ConfigElement parent,
      string viewName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      Dictionary<string, string> extensionsScripts = UserFilesDefinitions.GetLibrariesMasterExtensionsScripts();
      MasterViewDefinitionFacade definitionFacade = fluentFacade.AddMasterView(viewName).LocalizeUsing<UserFilesResources>().SetTitle("UserFiles").SetCssClass("sfListViewGrid").SetExternalClientScripts(extensionsScripts).SetClientMappedCommnadNames(UserFilesDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentLibraryService.svc/").SetProvidersGroups("System");
      MasterGridViewElement gridView = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      gridView.ToolbarConfig.Sections.Add(element1);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) gridView.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element2 = gridViewModeElement;
      gridView.ViewModesConfig.Add((ViewModeElement) element2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element2.ColumnsConfig;
      DataColumnElement element3 = new DataColumnElement((ConfigElement) element2.ColumnsConfig);
      element3.Name = "Thumbnail";
      element3.HeaderText = "LibrariesResourcesTitle";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.HeaderCssClass = "sfFolderIcn";
      element3.ItemCssClass = "sfFolderIcn";
      element3.ClientTemplate = "<p><a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_viewItemsByParent' }}\">{{Title.htmlEncode()}}</a></p> ";
      columnsConfig1.Add((ColumnElement) element3);
      string end;
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.UserFilesLibrariesActionsColumn.htm"))
        end = new StreamReader(manifestResourceStream).ReadToEnd();
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element2.ColumnsConfig;
      DataColumnElement element4 = new DataColumnElement((ConfigElement) element2.ColumnsConfig);
      element4.Name = "Title";
      element4.HeaderCssClass = "sfAlbumInfo";
      element4.ItemCssClass = "sfAlbumInfo";
      element4.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_viewItemsByParent sfItemTitle'>{{Title.htmlEncode()}}</a><p><i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i><i sys:if='ItemsCount' class='sfItemsCount'>{{ ItemsCount }} {$UserFilesResources, Items$}</i></p><p>{{getDateTemplate(LastUploadedDate, 'dd MMM, yyyy', '{$DocumentsResources, LastUploaded$}')}}</p>";
      columnsConfig2.Add((ColumnElement) element4);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element2.ColumnsConfig;
      DataColumnElement element5 = new DataColumnElement((ConfigElement) element2.ColumnsConfig);
      element5.Name = "Actions";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      element5.HeaderText = "Actions";
      element5.HeaderCssClass = "sfActionsWithProgress";
      element5.ItemCssClass = "sfActionsWithProgress";
      element5.ClientTemplate = end.Replace("UploadMediaName", "{$UserFilesResources, UploadFiles$}");
      columnsConfig3.Add((ColumnElement) element5);
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element2.ColumnsConfig;
      DataColumnElement element6 = new DataColumnElement((ConfigElement) element2.ColumnsConfig);
      element6.Name = "Storage";
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      element6.HeaderText = "StoredIn";
      element6.HeaderCssClass = "sfRegular";
      element6.ItemCssClass = "sfRegular";
      element6.ClientTemplate = "<p><span>{{BlobStorageProvider}}</span></p>";
      columnsConfig4.Add((ColumnElement) element6);
      WidgetBarSectionElement element7 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "LibrariesByStorage",
        Title = "LibrariesByStorage",
        ResourceClassId = typeof (LibrariesResources).Name,
        CssClass = "sfFilterBy sfSeparator",
        WrapperTagId = "librariesByStorage",
        Visible = new bool?(false)
      };
      WidgetBarSectionElement element8 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "ByDate",
        Title = "LibrariesByDate",
        ResourceClassId = typeof (LibrariesResources).Name,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      WidgetBarSectionElement element9 = new WidgetBarSectionElement((ConfigElement) gridView.SidebarConfig.Sections)
      {
        Name = "OtherFilterOptions",
        Title = "OtherFilterOptions",
        ResourceClassId = typeof (LibrariesResources).Name,
        CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "filterImagesSection"
      };
      ConfigElementList<WidgetElement> items1 = element9.Items;
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element9.Items);
      commandWidgetElement1.Name = "FilterByStorage";
      commandWidgetElement1.CommandName = "hideSectionsExcept";
      commandWidgetElement1.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element7.WrapperTagId);
      commandWidgetElement1.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement1.Text = "FilterByStorage";
      commandWidgetElement1.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement1.CssClass = "";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.IsSeparator = false;
      CommandWidgetElement element10 = commandWidgetElement1;
      items1.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> items2 = element8.Items;
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element8.Items);
      commandWidgetElement2.Name = "CloseDateFilter";
      commandWidgetElement2.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement2.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element8.WrapperTagId, element7.WrapperTagId);
      commandWidgetElement2.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement2.Text = "CloseDateFilter";
      commandWidgetElement2.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
      commandWidgetElement2.CssClass = "sfCloseFilter";
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.IsSeparator = false;
      CommandWidgetElement element11 = commandWidgetElement2;
      items2.Add((WidgetElement) element11);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element8.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element12 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element12.PredefinedFilteringRanges);
      element8.Items.Add((WidgetElement) element12);
      gridView.SidebarConfig.Sections.Add(element8);
      ConfigElementList<WidgetElement> items3 = element9.Items;
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element9.Items);
      commandWidgetElement3.Name = "FilterByDate";
      commandWidgetElement3.CommandName = "hideSectionsExcept";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element8.WrapperTagId);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "FilterByDate";
      commandWidgetElement3.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement3.CssClass = "";
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element13 = commandWidgetElement3;
      items3.Add((WidgetElement) element13);
      gridView.SidebarConfig.Sections.Add(element9);
      DynamicCommandWidgetElement commandWidgetElement4 = new DynamicCommandWidgetElement((ConfigElement) element7.Items);
      commandWidgetElement4.Name = "StorageProviderFilter";
      commandWidgetElement4.CommandName = "filterByStorage";
      commandWidgetElement4.PageSize = 10;
      commandWidgetElement4.MoreLinkText = "MoreLibraries";
      commandWidgetElement4.LessLinkText = "LessLibraries";
      commandWidgetElement4.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement4.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement4.IsSeparator = false;
      commandWidgetElement4.BindTo = BindCommandListTo.Client;
      commandWidgetElement4.BaseServiceUrl = "~/Sitefinity/Services/BlobStorage/BlobStorage.svc/providerstats/";
      commandWidgetElement4.ClientItemTemplate = "<a href='javascript:void(0);' class='sf_binderCommand_filterByStorage'>{{ Title }}</a> <span class='sfCount'>({{TotalItemsCount}})</span>";
      DynamicCommandWidgetElement element14 = commandWidgetElement4;
      element14.UrlParameters.Add("provider", "SystemLibrariesProvider");
      element14.UrlParameters.Add("libraryType", typeof (DocumentLibrary).FullName);
      element7.Items.Add((WidgetElement) element14);
      ConfigElementList<WidgetElement> items4 = element7.Items;
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element7.Items);
      commandWidgetElement5.Name = "CloseStorageFilter";
      commandWidgetElement5.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement5.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element8.WrapperTagId, element7.WrapperTagId);
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "CloseStorageFilter";
      commandWidgetElement5.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement5.CssClass = "sfCloseFilter";
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      commandWidgetElement5.IsSeparator = false;
      CommandWidgetElement element15 = commandWidgetElement5;
      items4.Add((WidgetElement) element15);
      gridView.SidebarConfig.Sections.Add(element7);
      gridView.SidebarConfig.Title = "ManageUserFileLibraries";
      gridView.SidebarConfig.ResourceClassId = UserFilesDefinitions.ResourceClassId;
      definitionFacade.AddUploadDialog("SystemLibrariesProvider", Res.Get<LibrariesResources>().DocumentItemName, Res.Get<DocumentsResources>().Documents, Res.Get<DocumentsResources>().Library, typeof (DocumentLibrary), "~/Sitefinity/Services/Content/DocumentService.svc/", "~/Sitefinity/Services/Content/DocumentLibraryService.svc/", "", typeof (Document)).AddParameters("&LibraryId={{Id}}").Done().AddInsertDialog(UserFilesDefinitions.BackendUserFilesInsertViewName).Done().AddEditDialog(UserFilesDefinitions.BackendUserFilesEditViewName).Done().AddPermissionsDialog(Res.Get<UserFilesResources>().BackToAllLibraries, Res.Get<UserFilesResources>().PermissionsForUserFiles, "DocumentLibrary,Document").AddParameters("&RequireSystemProviders=true").Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibraryRelocateDialog>("transferLibrary").SetParameters("?mode=TransferLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal();
      gridView.LinksConfig.Add(new LinkElement((ConfigElement) gridView.LinksConfig)
      {
        Name = "ViewDocumentsByLibrary",
        CommandName = "viewItemsByParent",
        NavigateUrl = RouteHelper.CreateNodeReference(UserFilesConstants.UserFilesDocumentsPageId) + "{{Url}}/"
      });
      DefinitionsHelper.CreateNotImplementedLink(gridView);
      return gridView;
    }

    private static Dictionary<string, string> GetLibrariesMasterExtensionsScripts()
    {
      Dictionary<string, string> extensionsScripts = new Dictionary<string, string>();
      string fullName = typeof (UserFilesDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName);
      string key3 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js", (object) fullName);
      extensionsScripts.Add(key1, string.Empty);
      extensionsScripts.Add(key2, string.Empty);
      extensionsScripts.Add(key3, "OnMasterViewLoaded");
      return extensionsScripts;
    }

    private static DetailFormViewElement DefineBackendLibraryDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      bool isCreateMode,
      bool showDeleteButton)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isCreateMode ? "CreateLibrary" : "EditLibrary").HideTopToolbar().LocalizeUsing<LibrariesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentLibraryService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("LibraryName").SetExample("LibraryNameExample").SetCssClass("sfTitleField").LocalizeUsing<DocumentsResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("LibraryNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("LibraryDescription").SetCssClass("sfFormSeparator").LocalizeUsing<DocumentsResources>().SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("AdvancedSection").SetTitle("AdvancedSection");
      ContentViewSectionElement viewSectionElement = definitionFacade4.Get();
      definitionFacade4.AddTextField("MaxSize").SetId("MaxLibrarySizeFieldControl").SetTitle("MaxLibrarySize").SetDescription("Mb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").LocalizeUsing<DocumentsResources>().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression("\\d").SetMessageCssClass("sfError").Done().Done().AddTextField("MaxItemSize").SetId("MaxDocumentSizeFieldControl").SetTitle("MaxDocumentSize").SetDescription("Kb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression("\\d").SetMessageCssClass("sfError").Done().Done();
      if (isCreateMode)
        definitionFacade4.AddLocalizedUrlNameField(definitionFacade3.Get().ID);
      else
        definitionFacade4.AddTextField("UrlName").SetTitle("UrlName").SetDisplayMode(FieldDisplayMode.Read).SetId("urlName").Done();
      BlobStorageChoiceFieldElement choiceFieldElement = new BlobStorageChoiceFieldElement((ConfigElement) viewSectionElement.Fields);
      choiceFieldElement.Title = "StorageProvider";
      choiceFieldElement.ResourceClassId = typeof (LibrariesResources).Name;
      choiceFieldElement.DisplayMode = new FieldDisplayMode?(isCreateMode ? FieldDisplayMode.Write : FieldDisplayMode.Read);
      choiceFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      choiceFieldElement.ID = "blobStorageChoiceFieldElement";
      choiceFieldElement.FieldName = "blobStorageField";
      choiceFieldElement.DataFieldName = "BlobStorageProvider";
      BlobStorageChoiceFieldElement element1 = choiceFieldElement;
      viewSectionElement.Fields.Add((FieldDefinitionElement) element1);
      viewSectionElement.Fields.Add((FieldDefinitionElement) LibrariesDefinitions.DefineClientCacheProfileField((ConfigElement) viewSectionElement.Fields));
      WidgetBarSectionElement element2 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items = element2.Items;
        CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element2.Items);
        element3.Name = "CreateAndUploadWidgetElement";
        element3.ButtonType = CommandButtonType.Save;
        element3.CommandName = "createAndUpload";
        element3.Text = "CreateAndGoToUploadDocuments";
        element3.ResourceClassId = UserFilesDefinitions.DocumentsResourceClassId;
        element3.WrapperTagKey = HtmlTextWriterTag.Span;
        element3.WidgetType = typeof (CommandWidget);
        items.Add((WidgetElement) element3);
      }
      ConfigElementList<WidgetElement> items1 = element2.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element2.Items);
      element4.Name = "SaveChangesWidgetElement";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "save";
      element4.Text = isCreateMode ? "CreateThisLibrary" : "SaveChanges";
      element4.ResourceClassId = UserFilesDefinitions.ResourceClassId;
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = isCreateMode ? "" : "sfSave";
      items1.Add((WidgetElement) element4);
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element2.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (DocumentsResources).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element5 = menuWidgetElement;
        ConfigElementList<WidgetElement> menuItems = element5.MenuItems;
        CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
        element6.Name = "PermissionsCommandName";
        element6.ButtonType = CommandButtonType.SimpleLinkButton;
        element6.Text = "SetPermissions";
        element6.CommandName = "permissions";
        element6.ResourceClassId = typeof (DocumentsResources).Name;
        element6.WidgetType = typeof (CommandWidget);
        menuItems.Add((WidgetElement) element6);
        element2.Items.Add((WidgetElement) element5);
      }
      ConfigElementList<WidgetElement> items2 = element2.Items;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element2.Items);
      element7.Name = "CancelWidgetElement";
      element7.ButtonType = CommandButtonType.Cancel;
      element7.CommandName = "cancel";
      element7.Text = "Cancel";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Span;
      element7.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element7);
      detailFormViewElement.Toolbar.Sections.Add(element2);
      return detailFormViewElement;
    }
  }
}
