// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Documents.DocumentsDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Documents
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for Documents.
  /// </summary>
  public static class DocumentsDefinitions
  {
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for documents on the backend.
    /// </summary>
    public static string BackendDefinitionName = "DocumentsBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for folders on the backend.
    /// </summary>
    public const string BackendDocumentFoldersDefinitionName = "DocumentFoldersBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for library on the backend.
    /// </summary>
    public static string BackendLibraryDefinitionName = "BackendLibraryDocumentsList";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for library on the backend.
    /// </summary>
    [Obsolete("Not applicable anymore.")]
    public static string BackendLibraryDocumentsDefinitionName = "BackendLibraryDocuments";
    /// <summary>
    /// Name of the view used to display documents in a list on the backend.
    /// </summary>
    public static string BackendLibrariesListViewName = "BackendLibrariesList";
    /// <summary>
    /// Name of the view used to display documents and files in a list in the Documents module
    /// on the backend.
    /// </summary>
    public static string BackendListViewName = "DocumentsBackendList";
    /// <summary>The resource pageId for documents.</summary>
    public static readonly string ResourceClassId = typeof (DocumentsResources).Name;
    /// <summary>
    /// Name of the view used to create albums in the Documents module
    /// on the backend.
    /// </summary>
    public const string BackendLibraryInsertViewName = "DocumentLibraryBackendInsert";
    /// <summary>
    /// Name of the view used to edit albums in the Documents module
    /// on the backend.
    /// </summary>
    public const string BackendLibraryEditViewName = "DocumentLibraryBackendEdit";
    /// <summary>
    /// Name of the view used to edit document properties in the Documents module
    /// on the backend.
    /// </summary>
    public const string BackendEditViewName = "DocumentsBackendEdit";
    /// <summary>Name of the bulk edit view in the Documents module.</summary>
    public const string BackendBulkEditViewName = "DocumentsBackendBulkEdit";
    /// <summary>
    /// Name of the single document upload view in the media content selector
    /// </summary>
    public const string SingleDocumentUploadDetailsViewName = "SingleDocumentUploadDetailsView";
    /// <summary>Name to the view used</summary>
    public const string FrontendDocumentsDefinitionName = "FrontendDocuments";
    public const string FrontendMasterListViewName = "MasterListView";
    public const string FrontendMasterListViewId = "57D8E0F2-8B3D-4CBF-96A4-000000000001";
    public const string FrontendMasterListViewFriendlyName = "List of documents for direct download";
    public const string FrontendMasterTableViewName = "MasterTableView";
    public const string FrontendMasterTableViewId = "57D8E0F2-8B3D-4CBF-96A4-000000000002";
    public const string FrontendMasterTableViewFriendlyName = "Table of documents for direct download";
    public const string FrontendDetailsListViewName = "DetailsListView";
    public const string FrontendDetailsListViewId = "57D8E0F2-8B3D-4CBF-96A4-000000000003";
    public const string FrontendDetailsListViewFriendlyName = "Document details";
    public const string FrontendMasterListDetailViewName = "MasterListDetailView";
    public const string FrontendMasterListDetailViewId = "57D8E0F2-8B3D-4CBF-96A4-000000000004";
    public const string FrontendMasterListDetailViewFriendlyName = "List of documents linking to document details";
    public const string FrontendMasterTableDetailViewName = "MasterTableDetailView";
    public const string FrontendMasterTableDetailViewId = "57D8E0F2-8B3D-4CBF-96A4-000000000005";
    public const string FrontendMasterTableDetailViewFriendlyName = "Table of documents for direct download and links to document details";
    /// <summary>The default frontend details view name;</summary>
    public const string FrontendDetaisTableViewName = "DetaisTableView";
    /// <summary>The default frontend master view name;</summary>
    public const string DefaultFrontendMasterViewName = "MasterListView";
    /// <summary>The default frontend detail view name.</summary>
    public const string DefaultFrontendDetailViewName = "DetailsListView";
    /// <summary>Version Comparison View Name</summary>
    public const string VersionComparisonView = "DocumentsBackendVersionComparisonView";
    public const string BackendVersionPreviewViewName = "DocumentsBackendVersionPreview";
    public const string BackendPreviewViewName = "DocumentsBackendPreview";
    private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";
    private const string ForFolderClass = "sf_ForFolder";
    private const string NotForFolderClass = "sf_NotForFolder";
    /// <summary>The view used to edit folders.</summary>
    private const string BackendFolderEditViewName = "BackendFolderEditViewName";
    private static Dictionary<string, string> clientMappedCommnadNames = new Dictionary<string, string>()
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
      return DocumentsDefinitions.DefineBackendContentView(parent, DocumentsDefinitions.BackendDefinitionName);
    }

    /// <summary>
    /// Defines the ContentView control for Folders on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFoldersBackendContentView(
      ConfigElement parent)
    {
      return DocumentsDefinitions.DefineBackendFolderContentView(parent, "DocumentFoldersBackend");
    }

    /// <summary>
    /// Defines the ContentView control for libraries on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineLibrariesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, DocumentsDefinitions.BackendLibraryDefinitionName, typeof (DocumentLibrary));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(DocumentsDefinitions.BackendLibrariesListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendLibraryListView((ConfigElement) backendContentView.ViewsConfig, DocumentsDefinitions.BackendLibrariesListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("DocumentLibraryBackendInsert", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendLibraryDetailsView(fluentContentView, "DocumentLibraryBackendInsert", true, false)));
      backendContentView.ViewsConfig.AddLazy("DocumentLibraryBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendLibraryDetailsView(fluentContentView, "DocumentLibraryBackendEdit", false, true)));
      return backendContentView;
    }

    internal static ContentViewControlElement DefineFrontendDocumentContentView(
      ConfigElement parent)
    {
      return DocumentsDefinitions.DefineFrontendContentView(parent, "FrontendDocuments");
    }

    private static ContentViewControlElement DefineFrontendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentFacade = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Document));
      ContentViewControlElement viewControlElement = fluentFacade.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) DocumentsDefinitions.DefinieFrontendMasterListView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) DocumentsDefinitions.DefinieFrontendMasterTableView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) DocumentsDefinitions.DefinieFrontendDetailListView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) DocumentsDefinitions.DefinieFrontendMasterListDetailView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) DocumentsDefinitions.DefinieFrontendMasterTableDetailView((ConfigElement) viewControlElement.ViewsConfig));
      DocumentsDefinitions.CreateFrontendDocumentsDialogs((ConfigElementCollection) viewControlElement.DialogsConfig, DocumentsDefinitions.BackendDefinitionName, fluentFacade);
      return viewControlElement;
    }

    private static ContentViewMasterElement DefinieFrontendMasterListView(
      ConfigElement parent)
    {
      DownloadListViewMasterElement viewMasterElement = new DownloadListViewMasterElement(parent);
      viewMasterElement.ViewName = "MasterListView";
      viewMasterElement.ViewType = typeof (MasterListView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.ThumbnailType = ThumbnailType.BigIcons;
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefinieFrontendMasterTableView(
      ConfigElement parent)
    {
      DownloadListViewMasterElement viewMasterElement = new DownloadListViewMasterElement(parent);
      viewMasterElement.ViewName = "MasterTableView";
      viewMasterElement.ViewType = typeof (MasterTableView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.ThumbnailType = ThumbnailType.SmallIcons;
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewDetailElement DefinieFrontendDetailListView(
      ConfigElement parent)
    {
      DownloadListViewDetailElement viewDetailElement = new DownloadListViewDetailElement(parent);
      viewDetailElement.ViewName = "DetailsListView";
      viewDetailElement.ViewType = typeof (DetailsListView);
      viewDetailElement.DisplayMode = FieldDisplayMode.Read;
      viewDetailElement.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      return (ContentViewDetailElement) viewDetailElement;
    }

    private static ContentViewMasterElement DefinieFrontendMasterListDetailView(
      ConfigElement parent)
    {
      DownloadListViewMasterElement viewMasterElement = new DownloadListViewMasterElement(parent);
      viewMasterElement.ViewName = "MasterListDetailView";
      viewMasterElement.ViewType = typeof (MasterListDetailView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      viewMasterElement.ShowDownloadLinkBelowDescription = new bool?(true);
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefinieFrontendMasterTableDetailView(
      ConfigElement parent)
    {
      DownloadListViewMasterElement viewMasterElement = new DownloadListViewMasterElement(parent);
      viewMasterElement.ViewName = "MasterTableDetailView";
      viewMasterElement.ViewType = typeof (MasterTableDetailView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      viewMasterElement.ShowDownloadLinkBelowDescription = new bool?(true);
      return (ContentViewMasterElement) viewMasterElement;
    }

    /// <summary>
    /// Defines the ContentView control for Documents on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    private static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Document));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(DocumentsDefinitions.BackendListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendListView((ConfigElement) backendContentView.ViewsConfig, controlDefinitionName, DocumentsDefinitions.BackendListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("DocumentsBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendDetailsView(fluentContentView, "DocumentsBackendEdit")));
      backendContentView.ViewsConfig.AddLazy("DocumentsBackendBulkEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendBulkEditView(fluentContentView, "DocumentsBackendBulkEdit")));
      backendContentView.ViewsConfig.AddLazy("DocumentsBackendVersionPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendVersionPreviewView(fluentContentView, "DocumentsBackendVersionPreview")));
      backendContentView.ViewsConfig.AddLazy("DocumentsBackendPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendPreviewView(fluentContentView, "DocumentsBackendPreview")));
      backendContentView.ViewsConfig.AddLazy("DocumentsBackendVersionComparisonView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineVersionComparisonView((ConfigElement) backendContentView.ViewsConfig, "DocumentsBackendVersionComparisonView")));
      backendContentView.ViewsConfig.AddLazy("SingleDocumentUploadDetailsView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineSingleDocumentUploadDetailsView(fluentContentView, "SingleDocumentUploadDetailsView", FieldDisplayMode.Write)));
      return backendContentView;
    }

    private static ContentViewControlElement DefineBackendFolderContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Folder)).SetManagerType(typeof (LibrariesManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy("BackendFolderEditViewName", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) DocumentsDefinitions.DefineBackendFolderDetailsView(fluentContentView, "BackendFolderEditViewName")));
      return viewControlElement;
    }

    private static ComparisonViewElement DefineVersionComparisonView(
      ConfigElement parent,
      string viewName)
    {
      ComparisonViewElement comparisonViewElement1 = new ComparisonViewElement(parent);
      comparisonViewElement1.Title = "VersionComparison";
      comparisonViewElement1.ViewName = viewName;
      comparisonViewElement1.ViewType = typeof (Telerik.Sitefinity.Versioning.Web.UI.Views.VersionComparisonView);
      comparisonViewElement1.DisplayMode = FieldDisplayMode.Read;
      comparisonViewElement1.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      ConfigElementDictionary<string, ComparisonFieldElement> fields1 = comparisonViewElement2.Fields;
      ComparisonFieldElement element1 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element1.FieldName = "MediaUrl";
      fields1.Add(element1);
      ConfigElementDictionary<string, ComparisonFieldElement> fields2 = comparisonViewElement2.Fields;
      ComparisonFieldElement element2 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element2.FieldName = "Title";
      element2.Title = "Title";
      element2.ResourceClassId = typeof (LibrariesResources).Name;
      fields2.Add(element2);
      ConfigElementDictionary<string, ComparisonFieldElement> fields3 = comparisonViewElement2.Fields;
      ComparisonFieldElement element3 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element3.FieldName = "Category";
      element3.Title = "Categories";
      element3.ResourceClassId = typeof (TaxonomyResources).Name;
      fields3.Add(element3);
      ConfigElementDictionary<string, ComparisonFieldElement> fields4 = comparisonViewElement2.Fields;
      ComparisonFieldElement element4 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element4.FieldName = "Tags";
      element4.Title = "Tags";
      element4.ResourceClassId = typeof (TaxonomyResources).Name;
      fields4.Add(element4);
      ConfigElementDictionary<string, ComparisonFieldElement> fields5 = comparisonViewElement2.Fields;
      ComparisonFieldElement element5 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element5.FieldName = "Author";
      element5.Title = "Author";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      fields5.Add(element5);
      ConfigElementDictionary<string, ComparisonFieldElement> fields6 = comparisonViewElement2.Fields;
      ComparisonFieldElement element6 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element6.FieldName = "Description";
      element6.Title = "Description";
      element6.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      fields6.Add(element6);
      ConfigElementDictionary<string, ComparisonFieldElement> fields7 = comparisonViewElement2.Fields;
      ComparisonFieldElement element7 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element7.FieldName = "UrlName";
      element7.Title = "UrlName";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      fields7.Add(element7);
      ConfigElementDictionary<string, ComparisonFieldElement> fields8 = comparisonViewElement2.Fields;
      ComparisonFieldElement element8 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element8.FieldName = "MediaFileUrlName";
      element8.Title = "MediaFileUrlName";
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      fields8.Add(element8);
      return comparisonViewElement2;
    }

    private static DetailFormViewElement DefineBackendPreviewView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("PreviewDocument").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<DocumentsResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").ShowNavigation().SupportMultilingual().DoNotUseWorkflow();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      DocumentsDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineBackendVersionPreviewView(
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
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("DocumentVersionHistory").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<DocumentsResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").ShowNavigation().SetLocalization(localization).DoNotUseWorkflow().SupportMultilingual();
      DetailFormViewElement detailView = fluentDetailView.Get();
      DocumentsDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (DocumentsResources).Name);
      return detailView;
    }

    internal static MasterGridViewElement DefineBackendListView(
      ConfigElement parent,
      string controlDefinitionName,
      string viewName,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> scripts = new Dictionary<string, string>();
      string fullName1 = typeof (DocumentsDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      string key3 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js", (object) fullName1);
      scripts.Add(key1, string.Empty);
      scripts.Add(key2, string.Empty);
      scripts.Add(key3, "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).LocalizeUsing<DocumentsResources>().SetTitle("ModuleTitle").SetViewType(typeof (LibrariesMasterGridView)).SetParentTitleFormat("LibraryDocumentsTitleFormat").SetCssClass("sfListViewGrid").SetExternalClientScripts(scripts).SetClientMappedCommnadNames(DocumentsDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      masterGridViewElement.AddResourceString("DocumentsResources", "AllDocumentsAndFiles");
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadDocumentWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadDocumentsButton";
      element2.ResourceClassId = typeof (DocumentsResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageDocument";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CreateLibraryWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "create";
      element3.Text = "CreateLibrary";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "";
      element3.PermissionSet = "DocumentLibrary";
      element3.ActionName = "CreateDocumentLibrary";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteDocumentWidget";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "groupDelete";
      element4.Text = "Delete";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = "sfGroupBtn";
      items3.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> items4 = element1.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element1.Items);
      element5.Name = "ReorderWidget";
      element5.ButtonType = CommandButtonType.Standard;
      element5.CommandName = "reorder";
      element5.Text = "ReorderDocuments";
      element5.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.WidgetType = typeof (CommandWidget);
      items4.Add((WidgetElement) element5);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "MoreActionsWidget";
      menuWidgetElement.Text = "MoreActions";
      menuWidgetElement.ResourceClassId = typeof (LibrariesResources).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element6 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element6.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element7.Name = "PublishDocumentsWidget";
      element7.Text = "PublishDocuments";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "groupPublish";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (DocumentsResources).Name;
      element7.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems2 = element6.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element8.Name = "UnpublishDocumentsWidget";
      element8.Text = "UnpublishDocuments";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "groupUnpublish";
      element8.WidgetType = typeof (CommandWidget);
      element8.ResourceClassId = typeof (DocumentsResources).Name;
      element8.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems3 = element6.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element9.Name = "BulkEditWidget";
      element9.Text = "BulkEditDocumentProperties";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "bulkEdit";
      element9.WidgetType = typeof (CommandWidget);
      element9.ResourceClassId = typeof (DocumentsResources).Name;
      menuItems3.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems4 = element6.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element10.Name = "MoveWidget";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "selectLibrary";
      element10.Text = "MoveToAnotherLibrary";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.WidgetType = typeof (CommandWidget);
      menuItems4.Add((WidgetElement) element10);
      element1.Items.Add((WidgetElement) element6);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (Document)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Document), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Document), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.HeaderText = "SortDocuments";
      commandWidgetElement1.MoreLinkText = "More";
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement1.ContentType = typeof (Document);
      DynamicCommandWidgetElement element11 = commandWidgetElement1;
      foreach (SortingExpressionElement expressionElement in expressionSettings1)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element11.Items, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element11.Items.Add(dynamicItemElement);
      }
      foreach (SortingExpressionElement expressionElement in expressionSettings2)
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element11.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element11.CustomItems.Add(dynamicItemElement);
      }
      element1.Items.Add((WidgetElement) element11);
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      LocalizationWidgetBarSectionElement barSectionElement1 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections);
      barSectionElement1.Name = "LanguagesWidgetBar";
      barSectionElement1.Title = "Languages";
      barSectionElement1.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement1.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement1.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element12 = barSectionElement1;
      ConfigElementList<WidgetElement> items5 = element12.Items;
      LanguagesDropDownListWidgetElement element13 = new LanguagesDropDownListWidgetElement((ConfigElement) element12.Items);
      element13.Name = "LanguagesDropDown";
      element13.Text = "Languages";
      element13.ResourceClassId = typeof (LocalizationResources).Name;
      element13.CssClass = "";
      element13.WidgetType = typeof (LanguagesDropDownListWidget);
      element13.IsSeparator = false;
      element13.LanguageSource = LanguageSource.Frontend;
      element13.AddAllLanguagesOption = false;
      element13.CommandName = "changeLanguage";
      items5.Add((WidgetElement) element13);
      masterGridViewElement.SidebarConfig.Sections.Add((WidgetBarSectionElement) element12);
      WidgetBarSectionElement element14 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "DocumentsMainSection",
        CssClass = "sfFilterBy sfLibFolders sfSeparator",
        WrapperTagId = "mainSection"
      };
      DynamicCommandWidgetElement commandWidgetElement2 = new DynamicCommandWidgetElement((ConfigElement) element14.Items);
      commandWidgetElement2.Name = "FolderFilter";
      commandWidgetElement2.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement2.IsSeparator = false;
      commandWidgetElement2.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/predecessors/";
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      commandWidgetElement2.ClientItemTemplate = "<a sys:href='{{ Url }}'>{{ Title.htmlEncode() }}</a>";
      commandWidgetElement2.ParentDataKeyName = "ParentId";
      commandWidgetElement2.SortExpression = "Title";
      commandWidgetElement2.PageSize = 100;
      DynamicCommandWidgetElement element15 = commandWidgetElement2;
      element15.UrlParameters.Add("provider", "");
      element14.Items.Add((WidgetElement) element15);
      masterGridViewElement.SidebarConfig.Sections.Add(element14);
      WidgetBarSectionElement element16 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ShowAllMediaItems",
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "showAllMediaItemsSection"
      };
      ConfigElementList<WidgetElement> items6 = element16.Items;
      CommandWidgetElement element17 = new CommandWidgetElement((ConfigElement) element16.Items);
      element17.Name = "AllMediaItems";
      element17.CommandName = "showAllMediaItems";
      element17.ButtonType = CommandButtonType.SimpleLinkButton;
      element17.Text = "FilterDocumentsBy";
      element17.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element17.WidgetType = typeof (CommandWidget);
      element17.IsSeparator = false;
      items6.Add((WidgetElement) element17);
      masterGridViewElement.SidebarConfig.Sections.Add(element16);
      WidgetBarSectionElement element18 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ShowAllLibraries",
        CssClass = "sfWidgetsList sfSeparator sfLibFolder",
        WrapperTagId = "showAllLibrariesSection"
      };
      ConfigElementList<WidgetElement> items7 = element18.Items;
      CommandWidgetElement element19 = new CommandWidgetElement((ConfigElement) element18.Items);
      element19.Name = "AllLibraries";
      element19.CommandName = "viewLibraries";
      element19.ButtonType = CommandButtonType.SimpleLinkButton;
      element19.Text = "DocumentsAndFilesByLibrary";
      element19.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element19.WidgetType = typeof (CommandWidget);
      element19.IsSeparator = false;
      items7.Add((WidgetElement) element19);
      masterGridViewElement.SidebarConfig.Sections.Add(element18);
      WidgetBarSectionElement element20 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Filter",
        Title = "DocumentsByDate",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      masterGridViewElement.SidebarConfig.Sections.Add(element20);
      string[] strArray = new string[3]
      {
        element20.WrapperTagId,
        element16.WrapperTagId,
        element14.WrapperTagId
      };
      WidgetBarSectionElement barSectionElement2 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "FilterOptions",
        Title = "FilterDocuments",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "moreFiltersSection"
      };
      ConfigElementList<WidgetElement> items8 = barSectionElement2.Items;
      CommandWidgetElement element21 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element21.Name = "AllDocuments";
      element21.CommandName = "showAllItems";
      element21.ButtonType = CommandButtonType.SimpleLinkButton;
      element21.ButtonCssClass = "sfSel";
      element21.Text = "AllDocuments";
      element21.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element21.WidgetType = typeof (CommandWidget);
      element21.IsSeparator = false;
      items8.Add((WidgetElement) element21);
      ConfigElementList<WidgetElement> items9 = barSectionElement2.Items;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element22.Name = "MyDocuments";
      element22.CommandName = "showMyItems";
      element22.ButtonType = CommandButtonType.SimpleLinkButton;
      element22.Text = "MyDocuments";
      element22.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      items9.Add((WidgetElement) element22);
      ConfigElementList<WidgetElement> items10 = barSectionElement2.Items;
      CommandWidgetElement element23 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element23.Name = "ShowDraftDocuments";
      element23.CommandName = "showMasterItems";
      element23.ButtonType = CommandButtonType.SimpleLinkButton;
      element23.Text = "DraftDocuments";
      element23.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element23.WidgetType = typeof (CommandWidget);
      element23.IsSeparator = false;
      items10.Add((WidgetElement) element23);
      ConfigElementList<WidgetElement> items11 = barSectionElement2.Items;
      CommandWidgetElement element24 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element24.Name = "ShowPublishedDocuments";
      element24.CommandName = "showPublishedItems";
      element24.ButtonType = CommandButtonType.SimpleLinkButton;
      element24.Text = "PublishedDocuments";
      element24.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element24.WidgetType = typeof (CommandWidget);
      element24.IsSeparator = false;
      items11.Add((WidgetElement) element24);
      ConfigElementList<WidgetElement> items12 = barSectionElement2.Items;
      CommandWidgetElement element25 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element25.Name = "PendingApprovalDocuments";
      element25.CommandName = "showPendingApprovalItems";
      element25.ButtonType = CommandButtonType.SimpleLinkButton;
      element25.Text = "WaitingForApproval";
      element25.ResourceClassId = typeof (LibrariesResources).Name;
      element25.WidgetType = typeof (CommandWidget);
      element25.IsSeparator = false;
      items12.Add((WidgetElement) element25);
      ConfigElementList<WidgetElement> items13 = barSectionElement2.Items;
      CommandWidgetElement element26 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element26.Name = "PendingPublishingDocuments";
      element26.CommandName = "showPendingPublishingItems";
      element26.ButtonType = CommandButtonType.SimpleLinkButton;
      element26.Text = "WaitingForPublishing";
      element26.ResourceClassId = typeof (LibrariesResources).Name;
      element26.WidgetType = typeof (CommandWidget);
      element26.IsSeparator = false;
      items13.Add((WidgetElement) element26);
      ConfigElementList<WidgetElement> items14 = barSectionElement2.Items;
      CommandWidgetElement element27 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element27.Name = "PendingReviewDocuments";
      element27.CommandName = "showPendingReviewItems";
      element27.ButtonType = CommandButtonType.SimpleLinkButton;
      element27.Text = "WaitingForReview";
      element27.ResourceClassId = typeof (LibrariesResources).Name;
      element27.WidgetType = typeof (CommandWidget);
      element27.IsSeparator = false;
      items14.Add((WidgetElement) element27);
      ConfigElementList<WidgetElement> items15 = barSectionElement2.Items;
      CommandWidgetElement element28 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element28.Name = "RejectedDocuments";
      element28.CommandName = "showRejectedItems";
      element28.ButtonType = CommandButtonType.SimpleLinkButton;
      element28.Text = "Rejected";
      element28.ResourceClassId = typeof (Labels).Name;
      element28.WidgetType = typeof (CommandWidget);
      element28.IsSeparator = false;
      items15.Add((WidgetElement) element28);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) barSectionElement2.Items);
      ConfigElementList<WidgetElement> items16 = barSectionElement2.Items;
      LiteralWidgetElement element29 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element29.Name = "Separator";
      element29.WrapperTagKey = HtmlTextWriterTag.Li;
      element29.WidgetType = typeof (LiteralWidget);
      element29.CssClass = "sfSeparator";
      element29.Text = "&nbsp;";
      element29.IsSeparator = true;
      items16.Add((WidgetElement) element29);
      string[] taxonomySection = DefinitionsHelper.CreateTaxonomySection<Document>(masterGridViewElement.SidebarConfig.Sections, barSectionElement2, "Documents & Files", DocumentsDefinitions.ResourceClassId, strArray);
      ConfigElementList<WidgetElement> items17 = element20.Items;
      CommandWidgetElement element30 = new CommandWidgetElement((ConfigElement) element20.Items);
      element30.Name = "CloseDateFilter";
      element30.CommandName = "showSectionsExceptAndResetFilter";
      element30.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(taxonomySection);
      element30.ButtonType = CommandButtonType.SimpleLinkButton;
      element30.Text = "CloseDateFilter";
      element30.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element30.CssClass = "sfCloseFilter";
      element30.WidgetType = typeof (CommandWidget);
      element30.IsSeparator = false;
      items17.Add((WidgetElement) element30);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element20.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element31 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element31.PredefinedFilteringRanges);
      element20.Items.Add((WidgetElement) element31);
      ConfigElementList<WidgetElement> items18 = barSectionElement2.Items;
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      commandWidgetElement3.Name = "FilterByDate";
      commandWidgetElement3.CommandName = "hideSectionsExcept";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element20.WrapperTagId);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "ByDate";
      commandWidgetElement3.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement3.CssClass = "";
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element32 = commandWidgetElement3;
      items18.Add((WidgetElement) element32);
      ConfigElementList<WidgetElement> items19 = barSectionElement2.Items;
      LiteralWidgetElement element33 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element33.Name = "Separator";
      element33.WrapperTagKey = HtmlTextWriterTag.Li;
      element33.WidgetType = typeof (LiteralWidget);
      element33.CssClass = "sfSeparator";
      element33.Text = "&nbsp;";
      element33.IsSeparator = true;
      items19.Add((WidgetElement) element33);
      masterGridViewElement.SidebarConfig.Sections.Add(barSectionElement2);
      WidgetBarSectionElement element34 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element34.Items);
        commandWidgetElement4.Name = "DocumentsComments";
        commandWidgetElement4.CommandName = "comments";
        commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement4.Text = "CommentsForDocuments";
        commandWidgetElement4.ResourceClassId = DocumentsDefinitions.ResourceClassId;
        commandWidgetElement4.CssClass = "sfComments";
        commandWidgetElement4.WidgetType = typeof (CommandWidget);
        commandWidgetElement4.IsSeparator = false;
        CommandWidgetElement element35 = commandWidgetElement4;
        element34.Items.Add((WidgetElement) element35);
      }
      WidgetBarSectionElement element36 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForDocuments",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSettings sfSeparator",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items20 = element36.Items;
      CommandWidgetElement element37 = new CommandWidgetElement((ConfigElement) element36.Items);
      element37.Name = "Permissions";
      element37.CommandName = "permissions";
      element37.ButtonType = CommandButtonType.SimpleLinkButton;
      element37.Text = "Permissions";
      element37.ResourceClassId = typeof (LibrariesResources).Name;
      element37.WidgetType = typeof (CommandWidget);
      element37.IsSeparator = false;
      items20.Add((WidgetElement) element37);
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element36.Items);
      commandWidgetElement5.Name = "ManageContentLocations";
      commandWidgetElement5.CommandName = "manageContentLocations";
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "PagesWhereDocumentsAndFilesArePublished";
      commandWidgetElement5.ResourceClassId = typeof (DocumentsResources).Name;
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element38 = commandWidgetElement5;
      element36.Items.Add((WidgetElement) element38);
      ConfigElementList<WidgetElement> items21 = element36.Items;
      CommandWidgetElement element39 = new CommandWidgetElement((ConfigElement) element36.Items);
      element39.Name = "CustomFields";
      element39.CommandName = "moduleEditor";
      element39.ButtonType = CommandButtonType.SimpleLinkButton;
      element39.Text = "CustomFields";
      element39.ResourceClassId = typeof (LibrariesResources).Name;
      element39.WidgetType = typeof (CommandWidget);
      element39.IsSeparator = false;
      items21.Add((WidgetElement) element39);
      if (element34.Items.Count == 0)
        element34.Visible = new bool?(false);
      masterGridViewElement.SidebarConfig.Sections.Add(element34);
      masterGridViewElement.SidebarConfig.Sections.Add(element36);
      DefinitionsHelper.CreateRecycleBinLink(masterGridViewElement.SidebarConfig.Sections, typeof (Document).FullName);
      masterGridViewElement.SidebarConfig.Title = "ManageDocuments";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (DocumentsResources).Name;
      LocalizationWidgetBarSectionElement barSectionElement3 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.ContextBarConfig.Sections);
      barSectionElement3.Name = "translations";
      barSectionElement3.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement3.CssClass = "sfContextWidgetWrp";
      barSectionElement3.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element40 = barSectionElement3;
      ConfigElementList<WidgetElement> items22 = element40.Items;
      CommandWidgetElement element41 = new CommandWidgetElement((ConfigElement) element40.Items);
      element41.Name = "ShowMoreTranslations";
      element41.CommandName = "showMoreTranslations";
      element41.ButtonType = CommandButtonType.SimpleLinkButton;
      element41.Text = "ShowAllTranslations";
      element41.ResourceClassId = typeof (LocalizationResources).Name;
      element41.WidgetType = typeof (CommandWidget);
      element41.CssClass = "sfShowHideLangVersions";
      element41.WrapperTagKey = HtmlTextWriterTag.Div;
      element41.IsSeparator = false;
      items22.Add((WidgetElement) element41);
      ConfigElementList<WidgetElement> items23 = element40.Items;
      CommandWidgetElement element42 = new CommandWidgetElement((ConfigElement) element40.Items);
      element42.Name = "HideMoreTranslations";
      element42.CommandName = "hideMoreTranslations";
      element42.ButtonType = CommandButtonType.SimpleLinkButton;
      element42.Text = "ShowBasicTranslationsOnly";
      element42.ResourceClassId = typeof (LocalizationResources).Name;
      element42.WidgetType = typeof (CommandWidget);
      element42.IsSeparator = false;
      element42.CssClass = "sfShowHideLangVersions sfDisplayNone";
      element42.WrapperTagKey = HtmlTextWriterTag.Div;
      items23.Add((WidgetElement) element42);
      masterGridViewElement.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element40);
      DocumentsDefinitions.DefineMasterContextBar(masterGridViewElement);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element43 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element43);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element43.ColumnsConfig;
      DataColumnElement element44 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      element44.Name = "Icon";
      element44.HeaderCssClass = "sfFileIcon";
      element44.ItemCssClass = "sfFileIcon";
      element44.ClientTemplate = "<span sys:if='!IsFolder' sys:class=\"{{ 'sfext sf' + Extension.substring(1).toLowerCase()}}\" sys:title=\"{{Extension.substring(1).toLowerCase()}}\">{{Extension.substring(1).toLowerCase()}}</span>\r\n                                    <span sys:if='IsFolder' sys:class=\"{{(!(DocumentsCount == 0 && LibrariesCount == 0) ? 'sfDocIcnSmall' : 'sfDocIcnSmall sfEmptyDocIcn') }}\"></span>";
      columnsConfig1.Add((ColumnElement) element44);
      string empty = string.Empty;
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element43.ColumnsConfig;
      DataColumnElement element45 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      element45.Name = "Title";
      element45.HeaderText = "TitleStatus";
      element45.ResourceClassId = typeof (DocumentsResources).Name;
      element45.HeaderCssClass = "sfTitleCol";
      element45.ItemCssClass = "sfTitleCol";
      element45.ClientTemplate = "<span sys:if='!IsFolder'><strong sys:class=\"{{'sfItemTitle' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus'  : '')}}\">\r\n                                        <a sys:href='javascript:void(0);' class=\"sf_binderCommand_editMediaContentProperties\">{{Title.htmlEncode()}}</a>\r\n                                    </strong>\r\n                                    <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>\r\n                                    \r\n                                    <span sys:class=\"{{ 'sfPrimaryStatus sf' + UIStatus.toLowerCase()}}\"><span sys:if='AdditionalStatus' class='sfSep'>| </span>{{Status}}</span></span><span sys:if='IsFolder'><strong class='sfItemTitle sfMBottom5'>\r\n                                        <a href='javascript:void(0);' class=\"sf_binderCommand_openLibrary\">{{Title.htmlEncode()}}</a>\r\n                                    </strong>\r\n                                    <i sys:if='DocumentsCount' class='sfItemsCount'>{{ DocumentsCount }}</i><i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i></span>";
      columnsConfig2.Add((ColumnElement) element45);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element43.ColumnsConfig);
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
      element43.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element43.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.HeaderText = "Actions";
      menuColumnElement1.HeaderCssClass = "sfRegular";
      menuColumnElement1.ItemCssClass = "sfRegular";
      menuColumnElement1.ResourceClassId = typeof (LibrariesResources).Name;
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      menuColumnElement2.MainAction = DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2, "EmbedDocumentLink", HtmlTextWriterTag.Li, "embedMediaContent sf_NotForFolder", "EmbedThisDocument", DocumentsDefinitions.ResourceClassId);
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "MoveTo", HtmlTextWriterTag.Li, "moveToSingle", "MoveTo", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Publish", HtmlTextWriterTag.Li, "publish sf_NotForFolder", "Publish", typeof (ContentResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Unpublish", HtmlTextWriterTag.Li, "unpublish sf_NotForFolder", "Unpublish", typeof (LibrariesResources).Name, "sfUnpublishItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Download", HtmlTextWriterTag.Li, "download sf_NotForFolder", "Download", typeof (LibrariesResources).Name, "sfDownloadItm"));
      menuColumnElement2.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuColumnElement2.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditProperties", HtmlTextWriterTag.Li, "editMediaContentProperties sf_NotForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditFolderProperties", HtmlTextWriterTag.Li, "editFolder sf_ForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "SetPermissions", HtmlTextWriterTag.Li, "permissions sf_NotForFolder", "SetPermissions", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "RelocateLibrary", HtmlTextWriterTag.Li, "relocateLibrary sf_ForFolder", "RelocateLibrary", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "History", HtmlTextWriterTag.Li, "historygrid sf_NotForFolder", "HistoryMenuItemTitle", typeof (VersionResources).Name));
      element43.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element43.ColumnsConfig;
      DataColumnElement element46 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      element46.Name = "FileDimSize";
      element46.HeaderText = "FileDimSize";
      element46.ResourceClassId = typeof (LibrariesResources).Name;
      element46.ClientTemplate = "<span sys:if='!IsFolder'><p class='sfLine'>{{Extension.substring(1).toUpperCase()}}</p><p class='sfLine'>{{TotalSize}} KB</p></span>";
      element46.HeaderCssClass = "sfRegular";
      element46.ItemCssClass = "sfRegular";
      columnsConfig3.Add((ColumnElement) element46);
      string str = string.Format("<p class='sfLine'>{0} <a sys:href='{6}'>{1}</a></p><p class='sfLine'>{2} {3}</p><p class='sfLine'>{4} {5}</p>", (object) Res.Get<LibrariesResources>().LibraryLabel, (object) "{{FolderTitle ? FolderTitle.htmlEncode() : LibraryTitle.htmlEncode()}}", (object) Res.Get<LibrariesResources>().Categories, (object) "{{CategoryText}}", (object) Res.Get<LibrariesResources>().Tags, (object) "{{TagsText}}", (object) "{{LibraryFullUrl}}");
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element43.ColumnsConfig;
      DataColumnElement element47 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      element47.Name = "LibraryCategoriesTags";
      element47.HeaderText = "LibraryCategoriesTags";
      element47.ResourceClassId = typeof (DocumentsResources).Name;
      element47.ClientTemplate = "<span sys:if='!IsFolder'>" + str + "</span>";
      element47.HeaderCssClass = "sfMedium";
      element47.ItemCssClass = "sfMedium";
      columnsConfig4.Add((ColumnElement) element47);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      dataColumnElement1.Name = "Owner";
      dataColumnElement1.HeaderText = "Owner";
      dataColumnElement1.ResourceClassId = typeof (Labels).Name;
      dataColumnElement1.ClientTemplate = "<span sys:if='!IsFolder' class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement1.HeaderCssClass = "sfRegular";
      dataColumnElement1.ItemCssClass = "sfRegular";
      DataColumnElement element48 = dataColumnElement1;
      element43.ColumnsConfig.Add((ColumnElement) element48);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element43.ColumnsConfig);
      dataColumnElement2.Name = "Date";
      dataColumnElement2.HeaderText = "Date";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "<span sys:if='!IsFolder'>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm') : '-' }}</span>";
      dataColumnElement2.HeaderCssClass = "sfDateAndHour";
      dataColumnElement2.ItemCssClass = "sfDateAndHour";
      DataColumnElement element49 = dataColumnElement2;
      element43.ColumnsConfig.Add((ColumnElement) element49);
      DecisionScreenElement element50 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoItemsExist",
        ResourceClassId = DocumentsDefinitions.ResourceClassId
      };
      ConfigElementList<CommandWidgetElement> actions1 = element50.Actions;
      CommandWidgetElement element51 = new CommandWidgetElement((ConfigElement) element50.Actions);
      element51.Name = "UploadDocuments";
      element51.ButtonType = CommandButtonType.Create;
      element51.CommandName = "uploadFile";
      element51.Text = "UploadDocumentsOrOtherFiles";
      element51.ResourceClassId = typeof (DocumentsResources).Name;
      element51.CssClass = "sfCreateItem";
      element51.RelatedSecuredObjectProviderName = "";
      element51.ActionName = "ManageDocument";
      actions1.Add(element51);
      ConfigElementList<CommandWidgetElement> actions2 = element50.Actions;
      CommandWidgetElement element52 = new CommandWidgetElement((ConfigElement) element50.Actions);
      element52.Name = "CreateLibrary";
      element52.ButtonType = CommandButtonType.Standard;
      element52.CommandName = "create";
      element52.Text = "CreateLibrary";
      element52.ResourceClassId = typeof (LibrariesResources).Name;
      element52.CssClass = "sfCreateAlbum";
      element52.PermissionSet = "DocumentLibrary";
      element52.ActionName = "CreateDocumentLibrary";
      actions2.Add(element52);
      ConfigElementList<CommandWidgetElement> actions3 = element50.Actions;
      CommandWidgetElement element53 = new CommandWidgetElement((ConfigElement) element50.Actions);
      element53.Name = "ManageLibraries";
      element53.ButtonType = CommandButtonType.Standard;
      element53.CommandName = "viewLibraries";
      element53.Text = "ManageLibraries";
      element53.ResourceClassId = typeof (LibrariesResources).Name;
      element53.CssClass = "sfCreateAlbum";
      element53.PermissionSet = "DocumentLibrary";
      element53.ActionName = "CreateDocumentLibrary";
      actions3.Add(element53);
      masterGridViewElement.DecisionScreensConfig.Add(element50);
      string parameters1 = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&showPrompt={5}", (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().Documents, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Document).FullName, (object) typeof (DocumentLibrary).FullName, (object) true);
      string parameters2 = string.Format("?itemName={0}&itemNameWithArticle={1}&itemsName={2}&libraryTypeName={3}&itemType={4}&parentType={5}&folderId=[[folderId]]", (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().DocumentWithArticle, (object) Res.Get<DocumentsResources>().DocumentsOnly, (object) Res.Get<DocumentsResources>().Library, (object) typeof (Document).FullName, (object) typeof (DocumentLibrary).FullName);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<LibrariesResources>().DocumentItemName, Res.Get<DocumentsResources>().Documents, Res.Get<LibrariesResources>().Library, typeof (DocumentLibrary), "~/Sitefinity/Services/Content/DocumentService.svc/", "~/Sitefinity/Services/Content/DocumentLibraryService.svc/").AddParameters("&folderId={{FolderId}}").Done().AddEditDialog("DocumentsBackendEdit", "", "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddInsertDialog("DocumentLibraryBackendInsert", DocumentsDefinitions.BackendLibraryDefinitionName).AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("DocumentLibraryBackendEdit", DocumentsDefinitions.BackendLibraryDefinitionName, "", "", (Type) null, "editLibraryProperties").Done().AddEditDialog("DocumentsBackendBulkEdit", "", "", "", (Type) null, "bulkEdit").AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("BackendFolderEditViewName", "DocumentFoldersBackend", "", "{{RootId}}", typeof (DocumentLibrary), "editFolder").Done().AddDialog<LibrarySelectorDialog>("selectLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToAll").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters1).Done().AddDialog<EmbedDialog>("embedMediaContent").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(425)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters("?mode=documents").Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().PermissionsForDocuments, string.Join(",", new string[3]
      {
        "Document",
        "DocumentLibrary",
        "DocumentsSitemapGeneration"
      })).Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().PermissionsForDocuments, string.Join(",", new string[2]
      {
        "Document",
        "DocumentLibrary"
      }), "libraryPermissionsDialog", typeof (DocumentLibrary)).Done().AddDialog<CustomSortingDialog>("editCustomSortingExpression").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).DisplayTitleBar().SetWidth(Unit.Pixel(500)).SetHeight(Unit.Pixel(600)).MakeModal().SetParameters("?contentType=" + typeof (Document).FullName).Done().AddHistoryComparisonDialog("DocumentsBackendVersionComparisonView", "", Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryGridDialog("DocumentsBackendVersionComparisonView", "", Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryPreviewDialog("DocumentsBackendVersionPreview", "", Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddPreviewDialog("DocumentsBackendPreview", "", "", "{{ParentId}}", typeof (DocumentLibrary)).Done().AddDialog<ReorderDialog>("reorder").MakeFullScreen().SetParameters(parameters2).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetParameters("?mode=RelocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddCustomFieldsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().DocumentsDataFields, Res.Get<DocumentsResources>().DocumentPluralItemName);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Document).FullName
      });
      ConfigElementList<WidgetElement> titleWidgetsConfig = masterGridViewElement.TitleWidgetsConfig;
      CommandWidgetElement element54 = new CommandWidgetElement((ConfigElement) masterGridViewElement.TitleWidgetsConfig);
      element54.Name = "ViewAllLibrariesCommand";
      element54.CommandName = "viewLibraries";
      element54.ButtonType = CommandButtonType.SimpleLinkButton;
      element54.WidgetType = typeof (CommandWidget);
      element54.Text = "AllLibraries";
      element54.ResourceClassId = typeof (LibrariesResources).Name;
      element54.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element54);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllLibrariesLink",
        CommandName = "viewLibraries",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.DocumentsHomePageId)
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllDocuments",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryDocumentsPageId) + "/?displayMode=allDocuments"
      });
      string fullName2 = typeof (Document).FullName;
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullName2
      });
      return masterGridViewElement;
    }

    internal static DetailFormViewElement DefineBackendDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibrariesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("EditDocumentProperties").LocalizeUsing<DocumentsResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").SetAlternativeTitle("CreateItem").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      DocumentsDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Write);
      return detailFormViewElement;
    }

    private static void CreateBackendSections(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      if (fluentDetailView.Get().ViewName == "DocumentsBackendEdit")
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      fluentDetailView.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade1.Get();
      string title = displayMode == FieldDisplayMode.Write ? "Title" : string.Empty;
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade2 = definitionFacade1.AddLocalizedTextField("Title").SetId("DocumentTitleFieldControl").SetTitle(title).SetCssClass("sfTitleField sfMBottom25").LocalizeUsing<LibrariesResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      if (displayMode == FieldDisplayMode.Write)
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
        element.ResourceClassId = DocumentsDefinitions.ResourceClassId;
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
        element.ResourceClassId = DocumentsDefinitions.ResourceClassId;
        element.ItemName = "Document";
        element.ItemNamePlural = "Documents";
        element.IsMultiselect = false;
        element.MaxFileCount = 1;
        fields.Add((FieldDefinitionElement) element);
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "LibraryFieldControl";
        folderFieldElement1.DataFieldName = "Library";
        folderFieldElement1.ItemName = "DocumentItemName";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Library";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (LibrariesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeLibraryInSpan",
          ResourceClassId = typeof (DocumentsResources).Name
        };
        viewSectionElement1.Fields.Add((FieldDefinitionElement) folderFieldElement2);
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
      definitionFacade3.AddMirrorTextField("UrlName").SetTitle("UrlName").SetId("urlName").SetFieldType(typeof (ContentUrlField)).SetMirroredControlId(definitionFacade2.Get().ID).SetRegularExpressionFilter(DefinitionsHelper.UrlRegularExpressionFilterForLibraries).AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForLibrariesContentValidator).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").Done().SetLocalizableDataFieldName("UrlName");
      string str1 = "$Context.AllowMultipleUrls";
      string dataFieldName1 = "$Context.AdditionalUrlNames";
      string str2 = "$Context.AdditionalUrlsRedirectToDefault";
      string dataFieldName2 = "$Context.DefaultUrl";
      string str3 = "$SfAdditionalInfo.AllowMultipleFileUrls";
      string dataFieldName3 = "$SfAdditionalInfo.FileAdditionalUrlsKey";
      string str4 = "$SfAdditionalInfo.RedirectToDefault";
      string dataFieldName4 = "$SfAdditionalInfo.DefaultFileUrl";
      if (displayMode == FieldDisplayMode.Write)
      {
        definitionFacade3.AddExpandableField("multipleUrlsExpandableField").SetId("multipleUrlsExpandableField").SetDataFieldName(str1).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(str1, "AllowMultipleURLsForThisItem", false).SetId("allowMultipleUrlsFieldElement").Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(dataFieldName1).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(str2, "AllAditionalUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultUrlNameTextField").SetDataFieldName(dataFieldName2).SetId("defaultUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
        definitionFacade3.AddMirrorTextField("MediaFileUrlName").SetTitle("MediaFileUrlName").SetDataFieldName("MediaFileUrlName").SetMirroredControlId(definitionFacade2.Get().ID).SetId("mediaFilеUrlName").SetFieldType(typeof (ContentUrlField)).SetLocalizableDataFieldName("MediaFileUrlName");
        definitionFacade3.AddExpandableField("multipleFileUrlsExpandableField").SetId("multipleFileUrlsExpandableField").SetDataFieldName(str3).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(str3, "AllowMultipleFileURLsForThisItem", false).SetId("allowMultipleFileUrlsFieldElement").Done().AddTextField("multipleFileUrlsField").SetId("multipleFileUrlsField").SetDataFieldName(dataFieldName3).SetFieldType<MultilineTextField>().SetTitle("AdditionalFileUrls").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(str4, "AllAditionalFileUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultFileUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultFileUrlNameTextField").SetDataFieldName(dataFieldName4).SetId("defaultFileUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
      }
      else
      {
        definitionFacade3.AddSingleCheckboxField("multipleUrlsExpandableField", "AllowMultipleURLsForThisItem").SetId("multipleUrlsExpandableField").SetDataFieldName(str1).SetTitle("AllowMultipleURLsForThisItem").LocalizeUsing<ContentResources>().Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(dataFieldName1).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").LocalizeUsing<ContentResources>().SetCssClass("sfTxtOnePerLine").SetRows(5).Done().AddSingleCheckboxField("additionalUrlsRedirectToDefaultFieldName", "AllAditionalUrlsRedirectoToTheDefaultOne").SetId("redirectToDefaultUrlChoiceFieldDefinition").SetDataFieldName(str2).SetTitle("AllAditionalUrlsRedirectoToTheDefaultOne").LocalizeUsing<ContentResources>().Done();
        definitionFacade3.AddSingleCheckboxField(str3, "AllowMultipleFileURLsForThisItem").SetId("multipleFileUrlsExpandableField").SetDataFieldName(str1).SetTitle("AllowMultipleFileURLsForThisItem").LocalizeUsing<ContentResources>().Done().AddTextField("multipleFileUrlsField").SetId("multipleFileUrlsField").SetDataFieldName(dataFieldName3).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetCssClass("sfTxtOnePerLine").LocalizeUsing<ContentResources>().SetRows(5).Done().AddSingleCheckboxField(str4, "AllAditionalFileUrlsRedirectoToTheDefaultOne").SetId("redirectToDefaultFileUrlChoiceFieldDefinition").SetDataFieldName(str4).SetTitle("AllAditionalFileUrlsRedirectoToTheDefaultOne").LocalizeUsing<ContentResources>().Done();
      }
      if (displayMode == FieldDisplayMode.Write)
      {
        SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = fluentDetailView.AddReadOnlySection("SidebarSection");
        ContentViewSectionElement viewSectionElement3 = definitionFacade4.Get();
        ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement3.Fields;
        ContentWorkflowStatusInfoFieldElement element3 = new ContentWorkflowStatusInfoFieldElement((ConfigElement) viewSectionElement3.Fields);
        element3.DisplayMode = new FieldDisplayMode?(displayMode);
        element3.FieldName = "DocumentsWorkflowStatusInfoField";
        element3.ResourceClassId = typeof (DocumentsResources).Name;
        element3.WrapperTag = HtmlTextWriterTag.Li;
        element3.FieldType = typeof (ContentWorkflowStatusInfoField);
        fields.Add((FieldDefinitionElement) element3);
        definitionFacade4.AddContentLocationInfoField();
        definitionFacade4.AddRelatingDataField();
      }
      if (displayMode != FieldDisplayMode.Write)
        return;
      DefinitionsHelper.CreateBackendFormToolbar(fluentDetailView.Get(), typeof (DocumentsResources).Name, false, "ThisItem", true, false);
    }

    private static DetailFormViewElement DefineBackendBulkEditView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade = fluentContentView.AddDetailView(viewName).SetTitle("BulkEditDocuments").LocalizeUsing<LibrariesResources>().UnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade.Get();
      ContentViewSectionElement viewSectionElement1 = definitionFacade.AddSection("CommonDataSection").SetCssClass("sfBulkEdit sfFirstForm").Get();
      PageFieldElement pageFieldElement1 = new PageFieldElement((ConfigElement) viewSectionElement1.Fields);
      pageFieldElement1.FieldType = typeof (EditMediaContentFolderField);
      pageFieldElement1.ID = "LibraryFieldControl";
      pageFieldElement1.DataFieldName = "Library";
      pageFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      pageFieldElement1.Title = "CommonLibrary";
      pageFieldElement1.CssClass = "sfChangeAlbum";
      pageFieldElement1.ResourceClassId = typeof (DocumentsResources).Name;
      pageFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      pageFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      pageFieldElement1.BindOnLoad = new bool?(false);
      pageFieldElement1.SortExpression = "Title ASC";
      PageFieldElement pageFieldElement2 = pageFieldElement1;
      pageFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) pageFieldElement2)
      {
        Expanded = new bool?(false),
        ExpandText = "ChangeLibraryInSpan",
        ResourceClassId = typeof (DocumentsResources).Name
      };
      viewSectionElement1.Fields.Add((FieldDefinitionElement) pageFieldElement2);
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
      Dictionary<string, string> scripts = new Dictionary<string, string>();
      string fullName1 = typeof (DocumentsDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      string key3 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js", (object) fullName1);
      scripts.Add(key1, string.Empty);
      scripts.Add(key2, string.Empty);
      scripts.Add(key3, "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade = fluentFacade.AddMasterView(viewName).LocalizeUsing<DocumentsResources>().SetTitle("ModuleTitle").SetCssClass("sfListViewGrid").SetExternalClientScripts(scripts).SetClientMappedCommnadNames(DocumentsDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentLibraryService.svc/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadDocumentWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadDocumentsButton";
      element2.ResourceClassId = typeof (DocumentsResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageDocument";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CreateLibraryWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "create";
      element3.Text = "CreateLibrary";
      element3.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element3.WidgetType = typeof (CommandWidget);
      element3.PermissionSet = "DocumentLibrary";
      element3.ActionName = "CreateDocumentLibrary";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteLibraryWidget";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "groupDelete";
      element4.Text = "Delete";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = "sfGroupBtn";
      items3.Add((WidgetElement) element4);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (DocumentLibrary)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.HeaderText = "Sort";
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement1.ContentType = typeof (Library);
      DynamicCommandWidgetElement element5 = commandWidgetElement1;
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
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element6 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element6);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element6.ColumnsConfig;
      DataColumnElement element7 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      element7.Name = "Thumbnail";
      element7.HeaderText = "LibrariesResourcesTitle";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.HeaderCssClass = "sfDocIcn";
      element7.ItemCssClass = "sfDocIcn";
      element7.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_viewItemsByParent' + (!(MediaItemsCount == 0 && LibrariesCount == 0) ? '' : ' sfEmptyDocIcn') }}\">{{Title.htmlEncode()}}</a>";
      columnsConfig1.Add((ColumnElement) element7);
      string end;
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibraryCommonActionsColumn.htm"))
        end = new StreamReader(manifestResourceStream).ReadToEnd();
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element6.ColumnsConfig;
      DataColumnElement element8 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      element8.Name = "Title";
      element8.HeaderCssClass = "sfAlbumInfo";
      element8.ItemCssClass = "sfAlbumInfo";
      element8.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_viewItemsByParent sfItemTitle sfMBottom5'>{{Title.htmlEncode()}}</a><i sys:if='MediaItemsCount' class='sfItemsCount'>{{ MediaItemsCount }}</i> <i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i>";
      columnsConfig2.Add((ColumnElement) element8);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element6.ColumnsConfig;
      DataColumnElement element9 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      element9.Name = "Actions";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      element9.HeaderText = "Actions";
      element9.HeaderCssClass = "sfActionsWithProgress";
      element9.ItemCssClass = "sfActionsWithProgress";
      element9.ClientTemplate = end.Replace("UploadMediaName", "{$DocumentsResources, UploadDocuments$}");
      columnsConfig3.Add((ColumnElement) element9);
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element6.ColumnsConfig;
      DataColumnElement element10 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      element10.Name = "Storage";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.HeaderText = "StoredIn";
      element10.HeaderCssClass = "sfRegular";
      element10.ItemCssClass = "sfRegular";
      element10.ClientTemplate = "<p><span>{{BlobStorageProvider}}</span></p>";
      columnsConfig4.Add((ColumnElement) element10);
      ConfigElementDictionary<string, ColumnElement> columnsConfig5 = element6.ColumnsConfig;
      DataColumnElement element11 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      element11.Name = "LastUploadedOn";
      element11.HeaderCssClass = "sfAlbumInfo";
      element11.ItemCssClass = "sfAlbumInfo";
      element11.ClientTemplate = "<p>{{getDateTemplate(LastUploadedDate, 'dd MMM, yyyy', '{$DocumentsResources, LastUploaded$}')}}</p>";
      columnsConfig5.Add((ColumnElement) element11);
      WidgetBarSectionElement element12 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "MainSection",
        CssClass = "sfFilterBy sfLibFolders sfSeparator",
        WrapperTagId = "mainSection"
      };
      DynamicCommandWidgetElement commandWidgetElement2 = new DynamicCommandWidgetElement((ConfigElement) element12.Items);
      commandWidgetElement2.Name = "FolderFilter";
      commandWidgetElement2.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement2.IsSeparator = false;
      commandWidgetElement2.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/predecessors/";
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      commandWidgetElement2.ClientItemTemplate = "<a sys:href='{{ Url }}'>{{ Title.htmlEncode() }}</a>";
      commandWidgetElement2.ParentDataKeyName = "ParentId";
      commandWidgetElement2.SortExpression = "Title";
      commandWidgetElement2.PageSize = 100;
      DynamicCommandWidgetElement element13 = commandWidgetElement2;
      element13.UrlParameters.Add("provider", "");
      element12.Items.Add((WidgetElement) element13);
      masterGridViewElement.SidebarConfig.Sections.Add(element12);
      masterGridViewElement.SidebarConfig.Title = "ManageDocuments";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (DocumentsResources).Name;
      WidgetBarSectionElement element14 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ShowAllMediaItems",
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "showAllMediaItemsSection"
      };
      ConfigElementList<WidgetElement> items4 = element14.Items;
      CommandWidgetElement element15 = new CommandWidgetElement((ConfigElement) element14.Items);
      element15.Name = "AllMediaItems";
      element15.CommandName = "showAllMediaItems";
      element15.ButtonType = CommandButtonType.SimpleLinkButton;
      element15.Text = "FilterDocumentsBy";
      element15.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element15.WidgetType = typeof (CommandWidget);
      element15.IsSeparator = false;
      items4.Add((WidgetElement) element15);
      masterGridViewElement.SidebarConfig.Sections.Add(element14);
      WidgetBarSectionElement element16 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element16.Items);
        commandWidgetElement3.Name = "DocumentsComments";
        commandWidgetElement3.CommandName = "comments";
        commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement3.Text = "CommentsForDocuments";
        commandWidgetElement3.ResourceClassId = DocumentsDefinitions.ResourceClassId;
        commandWidgetElement3.CssClass = "sfComments";
        commandWidgetElement3.WidgetType = typeof (CommandWidget);
        commandWidgetElement3.IsSeparator = false;
        CommandWidgetElement element17 = commandWidgetElement3;
        element16.Items.Add((WidgetElement) element17);
      }
      WidgetBarSectionElement element18 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForDocuments",
        ResourceClassId = DocumentsDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items5 = element18.Items;
      CommandWidgetElement element19 = new CommandWidgetElement((ConfigElement) element18.Items);
      element19.Name = "Permissions";
      element19.CommandName = "permissions";
      element19.ButtonType = CommandButtonType.SimpleLinkButton;
      element19.Text = "Permissions";
      element19.ResourceClassId = typeof (LibrariesResources).Name;
      element19.WidgetType = typeof (CommandWidget);
      element19.IsSeparator = false;
      items5.Add((WidgetElement) element19);
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element18.Items);
      commandWidgetElement4.Name = "ManageContentLocations";
      commandWidgetElement4.CommandName = "manageContentLocations";
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "PagesWhereDocumentsAndFilesArePublished";
      commandWidgetElement4.ResourceClassId = typeof (DocumentsResources).Name;
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element20 = commandWidgetElement4;
      element18.Items.Add((WidgetElement) element20);
      ConfigElementList<WidgetElement> items6 = element18.Items;
      CommandWidgetElement element21 = new CommandWidgetElement((ConfigElement) element18.Items);
      element21.Name = "CustomFields";
      element21.CommandName = "moduleEditor";
      element21.ButtonType = CommandButtonType.SimpleLinkButton;
      element21.Text = "CustomFields";
      element21.ResourceClassId = typeof (LibrariesResources).Name;
      element21.WidgetType = typeof (CommandWidget);
      element21.IsSeparator = false;
      items6.Add((WidgetElement) element21);
      if (element16.Items.Count == 0)
        element16.Visible = new bool?(false);
      masterGridViewElement.SidebarConfig.Sections.Add(element16);
      masterGridViewElement.SidebarConfig.Sections.Add(element18);
      string parameters = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&showPrompt={5}", (object) Res.Get<DocumentsResources>().Document, (object) Res.Get<DocumentsResources>().Documents, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Document).FullName, (object) typeof (DocumentLibrary).FullName, (object) true);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<LibrariesResources>().DocumentItemName, Res.Get<DocumentsResources>().Documents, Res.Get<DocumentsResources>().Library, typeof (DocumentLibrary), "~/Sitefinity/Services/Content/DocumentService.svc/", "~/Sitefinity/Services/Content/DocumentLibraryService.svc/", "", typeof (Document)).AddParameters("&LibraryId={{Id}}").Done().AddInsertDialog("DocumentLibraryBackendInsert", "", Res.Get<DocumentsResources>().BackToAllLibraries).AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("DocumentLibraryBackendEdit", "", Res.Get<DocumentsResources>().BackToAllLibraries).Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToAllLibraries, Res.Get<DocumentsResources>().PermissionsForDocuments, string.Join(",", new string[3]
      {
        "Document",
        "DocumentLibrary",
        "DocumentsSitemapGeneration"
      })).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibraryRelocateDialog>("transferLibrary").SetParameters("?mode=TransferLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters).Done().AddCustomFieldsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().DocumentsDataFields, Res.Get<DocumentsResources>().DocumentPluralItemName, typeof (Document));
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewDocumentsByLibrary",
        CommandName = "viewItemsByParent",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryDocumentsPageId) + "{{Url}}/"
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Document).FullName
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllDocuments",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryDocumentsPageId) + "/?displayMode=allDocuments"
      });
      string fullName2 = typeof (Document).FullName;
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullName2
      });
      return masterGridViewElement;
    }

    private static DetailFormViewElement DefineBackendLibraryDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      bool isCreateMode,
      bool showDeleteButton)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isCreateMode ? "CreateLibrary" : "EditLibrary").HideTopToolbar().LocalizeUsing<LibrariesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentLibraryService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual().SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      DetailFormViewElement cfg = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("LibraryName").SetExample("LibraryNameExample").SetCssClass("sfTitleField").LocalizeUsing<DocumentsResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("LibraryNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("LibraryDescription").SetCssClass("sfFormSeparator").LocalizeUsing<DocumentsResources>().SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done();
      if (isCreateMode)
        definitionFacade2.AddLocalizedUrlNameField(definitionFacade3.Get().ID);
      else
        definitionFacade2.AddTextField("UrlName").LocalizeUsing<LibrariesResources>().SetTitle("UrlName").SetDisplayMode(FieldDisplayMode.Read).SetId("urlName").Done();
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
      ParentLibraryFieldDefinitionElement element1 = new ParentLibraryFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      element1.FieldName = "ParentLibraryField";
      element1.DataFieldName = "ParentId";
      element1.FieldType = typeof (ParentLibraryField);
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "DocumentItemName";
      fields.Add((FieldDefinitionElement) element1);
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("RootLibrarySection").SetTitle("RootLibrarySettings");
      ContentViewSectionElement viewSectionElement2 = definitionFacade4.Get();
      definitionFacade4.AddTextField("MaxSize").SetId("MaxLibrarySizeFieldControl").SetTitle("MaxLibrarySize").SetDescription("Mb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").LocalizeUsing<DocumentsResources>().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression("\\d").SetMessageCssClass("sfError").Done().Done().AddTextField("MaxItemSize").SetId("MaxDocumentSizeFieldControl").SetTitle("MaxDocumentSize").SetDescription("Kb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression("\\d").SetMessageCssClass("sfError").Done().Done();
      BlobStorageChoiceFieldElement choiceFieldElement = new BlobStorageChoiceFieldElement((ConfigElement) viewSectionElement2.Fields);
      choiceFieldElement.Title = "StorageProvider";
      choiceFieldElement.ResourceClassId = typeof (LibrariesResources).Name;
      choiceFieldElement.DisplayMode = new FieldDisplayMode?(isCreateMode ? FieldDisplayMode.Write : FieldDisplayMode.Read);
      choiceFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      choiceFieldElement.ID = "blobStorageChoiceFieldElement";
      choiceFieldElement.FieldName = "blobStorageField";
      choiceFieldElement.DataFieldName = "BlobStorageProvider";
      BlobStorageChoiceFieldElement element2 = choiceFieldElement;
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) LibrariesDefinitions.DefineClientCacheProfileField((ConfigElement) viewSectionElement2.Fields));
      WidgetBarSectionElement element3 = new WidgetBarSectionElement((ConfigElement) cfg.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items = element3.Items;
        CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element3.Items);
        element4.Name = "CreateAndUploadWidgetElement";
        element4.ButtonType = CommandButtonType.Save;
        element4.CommandName = "createAndUpload";
        element4.Text = "CreateAndGoToUploadDocuments";
        element4.ResourceClassId = DocumentsDefinitions.ResourceClassId;
        element4.WrapperTagKey = HtmlTextWriterTag.Span;
        element4.WidgetType = typeof (CommandWidget);
        items.Add((WidgetElement) element4);
      }
      ConfigElementList<WidgetElement> items1 = element3.Items;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element3.Items);
      element5.Name = "SaveChangesWidgetElement";
      element5.ButtonType = CommandButtonType.Standard;
      element5.CommandName = "save";
      element5.Text = isCreateMode ? "CreateThisLibrary" : "SaveChanges";
      element5.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Span;
      element5.WidgetType = typeof (CommandWidget);
      element5.CssClass = isCreateMode ? "" : "sfSave";
      items1.Add((WidgetElement) element5);
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element3.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (DocumentsResources).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element6 = menuWidgetElement;
        if (showDeleteButton)
        {
          ConfigElementList<WidgetElement> menuItems = element6.MenuItems;
          CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
          element7.Name = "DeleteCommandName";
          element7.ButtonType = CommandButtonType.SimpleLinkButton;
          element7.Text = "DeleteThisItem";
          element7.CommandName = "delete";
          element7.ResourceClassId = typeof (DocumentsResources).Name;
          element7.WidgetType = typeof (CommandWidget);
          menuItems.Add((WidgetElement) element7);
        }
        ConfigElementList<WidgetElement> menuItems1 = element6.MenuItems;
        CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
        element8.Name = "PermissionsCommandName";
        element8.ButtonType = CommandButtonType.SimpleLinkButton;
        element8.Text = "SetPermissions";
        element8.CommandName = "permissions";
        element8.ResourceClassId = typeof (DocumentsResources).Name;
        element8.WidgetType = typeof (CommandWidget);
        menuItems1.Add((WidgetElement) element8);
        element3.Items.Add((WidgetElement) element6);
      }
      ConfigElementList<WidgetElement> items2 = element3.Items;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element3.Items);
      element9.Name = "CancelWidgetElement";
      element9.ButtonType = CommandButtonType.Cancel;
      element9.CommandName = "cancel";
      element9.Text = "Cancel";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      element9.WrapperTagKey = HtmlTextWriterTag.Span;
      element9.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element9);
      cfg.Toolbar.Sections.Add(element3);
      if (!isCreateMode)
      {
        cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveToRootLibraryWarning");
        cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveLibraryWarning");
        PromptDialogDefinition dialogDefinition = new PromptDialogDefinition()
        {
          Width = 350,
          Height = 300,
          Mode = PromptMode.Confirm,
          AllowCloseButton = false,
          InputRows = 5,
          ShowOnLoad = false,
          DialogName = "confirmMove",
          Message = "MoveLibraryWarning",
          ResourceClassId = typeof (LibrariesResources).Name
        };
        List<ICommandToolboxItemDefinition> commands1 = dialogDefinition.Commands;
        CommandToolboxItemDefinition toolboxItemDefinition1 = new CommandToolboxItemDefinition();
        toolboxItemDefinition1.CommandName = "submit";
        toolboxItemDefinition1.CommandType = CommandType.NormalButton;
        toolboxItemDefinition1.CausesValidation = false;
        toolboxItemDefinition1.ResourceClassId = typeof (Labels).Name;
        toolboxItemDefinition1.Text = "YesMove";
        toolboxItemDefinition1.WrapperTagName = "LI";
        toolboxItemDefinition1.CssClass = "sfSave";
        toolboxItemDefinition1.Visible = true;
        commands1.Add((ICommandToolboxItemDefinition) toolboxItemDefinition1);
        List<ICommandToolboxItemDefinition> commands2 = dialogDefinition.Commands;
        CommandToolboxItemDefinition toolboxItemDefinition2 = new CommandToolboxItemDefinition();
        toolboxItemDefinition2.CommandName = "cancel";
        toolboxItemDefinition2.CommandType = CommandType.CancelButton;
        toolboxItemDefinition2.CausesValidation = false;
        toolboxItemDefinition2.ResourceClassId = typeof (Labels).Name;
        toolboxItemDefinition2.Text = "Cancel";
        toolboxItemDefinition2.WrapperTagName = "LI";
        toolboxItemDefinition2.Visible = true;
        commands2.Add((ICommandToolboxItemDefinition) toolboxItemDefinition2);
        cfg.PromptDialogs.Add((IPromptDialogDefinition) dialogDefinition);
      }
      return cfg;
    }

    private static DetailFormViewElement DefineSingleDocumentUploadDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      FieldDisplayMode displayMode)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle("UploadDocument").LocalizeUsing<DocumentsResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/").SetAlternativeTitle("CreateItem").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      definitionFacade2.AddLocalizedTextField("Title").SetId("DocumentTitleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").LocalizeUsing<LibrariesResources>().AddValidation().LocalizeUsing<ErrorMessages>().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").SetMaxLength((int) byte.MaxValue).SetMaxLengthViolationMessage("TitleMaxLength").Done().Done();
      if (displayMode == FieldDisplayMode.Write)
      {
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "LibraryFieldControl";
        folderFieldElement1.DataFieldName = "Library";
        folderFieldElement1.ItemName = "DocumentItemName";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Library";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (LibrariesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        folderFieldElement1.ShowCreateNewLibraryButton = true;
        folderFieldElement1.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
        folderFieldElement1.LibraryTypeName = "Telerik.Sitefinity.Libraries.Model.DocumentLibrary";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeLibraryInSpan",
          ResourceClassId = typeof (DocumentsResources).Name
        };
        viewSectionElement1.Fields.Add((FieldDefinitionElement) folderFieldElement2);
      }
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddExpandableSection("TaxonomiesSection").SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element1 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element1);
      FlatTaxonFieldDefinitionElement element2 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      element2.CssClass = "sfFormSeparator";
      element2.Description = "TagsFieldInstructions";
      element2.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      return detailFormViewElement;
    }

    private static void CreateFrontendDocumentsDialogs(
      ConfigElementCollection parent,
      string controlDefinitionName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      fluentFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider((ConfigElement) parent), Res.Get<LibrariesResources>().DocumentItemName, Res.Get<DocumentsResources>().Documents, Res.Get<LibrariesResources>().Library, typeof (DocumentLibrary), "~/Sitefinity/Services/Content/DocumentService.svc/", "~/Sitefinity/Services/Content/DocumentLibraryService.svc/", "create").Done().AddEditDialog("DocumentsBackendEdit", controlDefinitionName, Res.Get<DocumentsResources>().BackToItems, "{{ParentId}}", typeof (DocumentLibrary)).Done().AddDialog<EmbedDialog>("embedMediaContent").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters("?mode=documents").Done().AddPermissionsDialog(Res.Get<DocumentsResources>().BackToItems, Res.Get<DocumentsResources>().PermissionsForDocuments, string.Join(",", new string[3]
      {
        "Document",
        "DocumentLibrary",
        "DocumentsSitemapGeneration"
      })).Done().AddHistoryComparisonDialog("DocumentsBackendVersionComparisonView", controlDefinitionName, Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryGridDialog("DocumentsBackendVersionComparisonView", controlDefinitionName, Res.Get<DocumentsResources>().BackToItems, "", "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddHistoryPreviewDialog("DocumentsBackendVersionPreview", controlDefinitionName, Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (DocumentLibrary), "editMediaContentProperties").Done().AddPreviewDialog("DocumentsBackendPreview", controlDefinitionName, "", "{{ParentId}}", typeof (DocumentLibrary));
    }

    private static void DefineMasterContextBar(MasterGridViewElement gridView)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) gridView.ContextBarConfig.Sections)
      {
        Name = "librariesProperties",
        WrapperTagKey = HtmlTextWriterTag.Div,
        WrapperTagId = "librariesPropertiesSection",
        CssClass = "sfContextWidgetWrp"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      FolderBreadcrumbWidgetElement element2 = new FolderBreadcrumbWidgetElement((ConfigElement) element1.Items);
      element2.Name = "FolderBreadcrumbWidget";
      element2.WrapperTagKey = HtmlTextWriterTag.Div;
      element2.WidgetType = typeof (FolderBreadcrumbWidget);
      element2.ManagerType = typeof (LibrariesManager);
      element2.NavigationPageId = LibrariesModule.LibraryDocumentsPageId;
      element2.RootPageId = LibrariesModule.DocumentsHomePageId;
      element2.RootTitle = "AllLibraries";
      element2.ResourceClassId = typeof (LibrariesResources).Name;
      element2.AppendRootUrl = true;
      element2.CssClass = "sfFolderBreadcrumbWrp";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      ScheduledTaskProgressBarElement element3 = new ScheduledTaskProgressBarElement((ConfigElement) element1.Items);
      element3.Name = "LibraryRunningTaskProgressBar";
      element3.WrapperTagKey = HtmlTextWriterTag.Div;
      element3.WidgetType = typeof (ScheduledTaskProgressBarWidget);
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.CssClass = "sfLibrariesProgress";
      items2.Add((WidgetElement) element3);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "FolderActionsWidget";
      menuWidgetElement.Text = "LibraryActions";
      menuWidgetElement.ResourceClassId = typeof (LibrariesResources).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      menuWidgetElement.CssClass = "sfAlwaysOn sfLibActionsWrp";
      ActionMenuWidgetElement element4 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element4.MenuItems;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element5.Name = "DeleteLibraryActionWidget";
      element5.Text = "DeleteThisLibrary";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.CommandName = "deleteLibrary";
      element5.CssClass = "sf_NotForFolder";
      element5.WidgetType = typeof (CommandWidget);
      menuItems1.Add((WidgetElement) element5);
      ConfigElementList<WidgetElement> menuItems2 = element4.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element6.Name = "MoveToActionWidget";
      element6.Text = "MoveTo";
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "moveToAll";
      element6.WidgetType = typeof (CommandWidget);
      menuItems2.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems3 = element4.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element7.Name = "EditLibraryPropertiesActionWidget";
      element7.Text = "EditProperties";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "editLibraryProperties";
      element7.WidgetType = typeof (CommandWidget);
      element7.CssClass = "sf_NotForFolder";
      menuItems3.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems4 = element4.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element8.Name = "LibraryPermissionsActionWidget";
      element8.Text = "SetPermissions";
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "libraryPermissions";
      element8.WidgetType = typeof (CommandWidget);
      element8.CssClass = "sf_NotForFolder";
      menuItems4.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems5 = element4.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element9.Name = "DeleteFolderActionWidget";
      element9.Text = "DeleteThisLibrary";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "deleteFolder";
      element9.CssClass = "sf_ForFolder";
      element9.WidgetType = typeof (CommandWidget);
      menuItems5.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems6 = element4.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element10.Name = "EditFolderActionWidget";
      element10.Text = "EditProperties";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "editFolder";
      element10.CssClass = "sf_ForFolder";
      element10.WidgetType = typeof (CommandWidget);
      menuItems6.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems7 = element4.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
      element11.Name = "RelocateLibraryActionWidget";
      element11.Text = "RelocateLibrary";
      element11.ResourceClassId = typeof (LibrariesResources).Name;
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.CommandName = "relocateLibrary";
      element11.WidgetType = typeof (CommandWidget);
      menuItems7.Add((WidgetElement) element11);
      element1.Items.Add((WidgetElement) element4);
      gridView.ContextBarConfig.Sections.Add(element1);
    }

    private static DetailFormViewElement DefineBackendFolderDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle("EditLibrary").HideTopToolbar().LocalizeUsing<LibrariesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/DocumentService.svc/folder/").DoNotUseWorkflow().DoNotCreateBlankItem().SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      DetailFormViewElement cfg = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("LibraryName").SetExample("LibraryNameExample").SetCssClass("sfTitleField").LocalizeUsing<DocumentsResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("LibraryNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("LibraryDescription").SetCssClass("sfFormSeparator").LocalizeUsing<DocumentsResources>().SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done();
      ContentViewSectionElement viewSectionElement = definitionFacade2.Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      ParentLibraryFieldDefinitionElement element1 = new ParentLibraryFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
      element1.FieldName = "ParentLibraryField";
      element1.DataFieldName = "ParentId";
      element1.FieldType = typeof (ParentLibraryField);
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "DocumentItemName";
      fields.Add((FieldDefinitionElement) element1);
      WidgetBarSectionElement element2 = new WidgetBarSectionElement((ConfigElement) cfg.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element2.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element2.Items);
      element3.Name = "SaveChangesWidgetElement";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "save";
      element3.Text = "SaveChanges";
      element3.ResourceClassId = DocumentsDefinitions.ResourceClassId;
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "sfSave";
      items1.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items2 = element2.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element2.Items);
      element4.Name = "CancelWidgetElement";
      element4.ButtonType = CommandButtonType.Cancel;
      element4.CommandName = "cancel";
      element4.Text = "Cancel";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element4);
      cfg.Toolbar.Sections.Add(element2);
      cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveToRootLibraryWarning");
      cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveLibraryWarning");
      PromptDialogDefinition dialogDefinition = new PromptDialogDefinition()
      {
        Width = 350,
        Height = 300,
        Mode = PromptMode.Confirm,
        AllowCloseButton = false,
        InputRows = 5,
        ShowOnLoad = false,
        DialogName = "confirmMove",
        Message = "MoveLibraryWarning",
        ResourceClassId = typeof (LibrariesResources).Name
      };
      List<ICommandToolboxItemDefinition> commands1 = dialogDefinition.Commands;
      CommandToolboxItemDefinition toolboxItemDefinition1 = new CommandToolboxItemDefinition();
      toolboxItemDefinition1.CommandName = "submit";
      toolboxItemDefinition1.CommandType = CommandType.NormalButton;
      toolboxItemDefinition1.CausesValidation = false;
      toolboxItemDefinition1.ResourceClassId = typeof (Labels).Name;
      toolboxItemDefinition1.Text = "YesMove";
      toolboxItemDefinition1.WrapperTagName = "LI";
      toolboxItemDefinition1.CssClass = "sfSave";
      toolboxItemDefinition1.Visible = true;
      commands1.Add((ICommandToolboxItemDefinition) toolboxItemDefinition1);
      List<ICommandToolboxItemDefinition> commands2 = dialogDefinition.Commands;
      CommandToolboxItemDefinition toolboxItemDefinition2 = new CommandToolboxItemDefinition();
      toolboxItemDefinition2.CommandName = "cancel";
      toolboxItemDefinition2.CommandType = CommandType.CancelButton;
      toolboxItemDefinition2.CausesValidation = false;
      toolboxItemDefinition2.ResourceClassId = typeof (Labels).Name;
      toolboxItemDefinition2.Text = "Cancel";
      toolboxItemDefinition2.WrapperTagName = "LI";
      toolboxItemDefinition2.Visible = true;
      commands2.Add((ICommandToolboxItemDefinition) toolboxItemDefinition2);
      cfg.PromptDialogs.Add((IPromptDialogDefinition) dialogDefinition);
      return cfg;
    }
  }
}
