// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.VideosDefinitions
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
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
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
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Videos
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for Videos.
  /// </summary>
  public static class VideosDefinitions
  {
    /// <summary>The predefined sizes for the embed functionality</summary>
    internal static readonly List<ChoiceDefinition> PredefinedEmbedVideoSizes = new List<ChoiceDefinition>()
    {
      new ChoiceDefinition()
      {
        Text = "400x226",
        Value = "400x226"
      },
      new ChoiceDefinition()
      {
        Text = "640x360",
        Value = "640x360"
      },
      new ChoiceDefinition()
      {
        Text = "854x480",
        Value = "854x480"
      },
      new ChoiceDefinition()
      {
        Text = "1280x720",
        Value = "1280x720"
      },
      new ChoiceDefinition()
      {
        Text = "1920x1080",
        Value = "1920x1080"
      }
    };
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for videos on the backend.
    /// </summary>
    public static string BackendVideosDefinitionName = "VideosBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for folders on the backend.
    /// </summary>
    public const string BackendVideoFoldersDefinitionName = "VideoFoldersBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for videos' library on the backend.
    /// </summary>
    public static string BackendLibraryDefinitionName = "LibraryVideoBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control library list library on the backend(videomodule)).
    /// </summary>
    [Obsolete("Not applicable anymore.")]
    public static string BackendLibraryVideosDefinitionName = "BackendLibraryVideoList";
    /// <summary>
    /// Name of the view used to display videos in a list in the Videos module
    /// on the backend.
    /// </summary>
    public static string BackendListViewName = "VideosBackendList";
    /// <summary>
    /// Name of the view used to create libraries in the Videos module
    /// on the backend.
    /// </summary>
    public const string BackendLibraryInsertViewName = "LibrariesBackendInsert";
    /// <summary>
    /// Name of the view used to edit libraries in the Videos module
    /// on the backend.
    /// </summary>
    public const string BackendLibraryEditViewName = "LibrariesBackendEdit";
    /// <summary>
    /// Name of the view used to display videos in a list in the Videos module
    /// on the backend.
    /// </summary>
    public static string BackendLibrariesListViewName = "BackendLibrariesList";
    /// <summary>
    /// Name of the view used to edit video properties in the Videos module
    /// on the backend.
    /// </summary>
    public const string BackendEditViewName = "VideosBackendEdit";
    /// <summary>The view used to edit folders.</summary>
    private const string BackendFolderEditViewName = "BackendFolderEditViewName";
    /// <summary>Name of the bulk edit view in the Videos module.</summary>
    public const string BackendBulkEditViewName = "VideosBackendBulkEdit";
    /// <summary>
    /// Name of the video upload view in the media item selector.
    /// </summary>
    public const string SingleVideoUploadDetailsViewName = "SingleVideoUploadDetailsView";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the frontend videos ContentView control.
    /// </summary>
    public const string FrontendVideosDefinitionName = "VideosFrontend";
    /// <summary>
    /// Name of the view used to display videos in the frontend as a thumbnails list.
    /// </summary>
    public const string FrontendMasterThumbnailsListViewName = "VideosFrontendThumbnailsList";
    /// <summary>
    /// Id of the view used to display videos in the frontend as a thumbnails list.
    /// </summary>
    public const string FrontendMasterThumbnailsListViewId = "DB92F414-1C8F-4F43-ABFE-000000000001";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display videos in the frontend as a thumbnails list.
    /// </summary>
    public const string FrontendMasterThumbnailsListViewFriendlyName = "List of video thumbnails";
    /// <summary>
    /// Name of the view used to display videos in the frontend as a thumbnails list with light box.
    /// </summary>
    public const string FrontendMasterThumbnailsLightBoxViewName = "VideosFrontendThumbnailsLightBox";
    /// <summary>
    /// Id of the view used to display videos in the frontend as a thumbnails list with lightbox.
    /// </summary>
    public const string FrontendMasterThumbnailsLightBoxViewId = "DB92F414-1C8F-4F43-ABFE-000000000002";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display videos in the frontend as a thumbnails list with lightbox.
    /// </summary>
    public const string FrontendMasterThumbnailsLightBoxViewFriendlyName = "List of video thumbnails and overlay dialog (lightbox)";
    /// <summary>Name of the view used to display videos details.</summary>
    public const string FrontendDetailViewName = "VideosDetailView";
    /// <summary>Id of the view used to display videos details.</summary>
    public const string FrontendDetailViewId = "DB92F414-1C8F-4F43-ABFE-000000000003";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display video details.
    /// </summary>
    public const string FrontendDetailViewFriendlyName = "Single video with details";
    /// <summary>The default frontend detail view name.</summary>
    public const string DefaultFrontendDetailViewName = "VideosDetailView";
    /// <summary>The default frontend master view name;</summary>
    public const string DefaultFrontendMasterViewName = "VideosFrontendThumbnailsList";
    /// <summary>Version Comparison View Name</summary>
    public const string VersionComparisonView = "VideosBackendVersionComparisonView";
    private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";
    public const string BackendVersionPreviewViewName = "VideosBackendVersionPreview";
    public const string BackendPreviewViewName = "VideosVersionPreview";
    public static readonly string ResourceClassId = typeof (VideosResources).Name;
    private static Dictionary<string, string> clientMappedCommnadNames = new Dictionary<string, string>()
    {
      {
        "edit",
        "editMediaContentProperties"
      }
    };
    private const string NotForFolderClass = "sf_NotForFolder";
    private const string ForFolderClass = "sf_ForFolder";

    /// <summary>
    /// Defines the ContentView control for Videos on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineVideosBackendContentView(
      ConfigElement parent)
    {
      return VideosDefinitions.DefineBackendContentView(parent, VideosDefinitions.BackendVideosDefinitionName);
    }

    /// <summary>
    /// Defines the ContentView control for Folders on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFoldersBackendContentView(
      ConfigElement parent)
    {
      return VideosDefinitions.DefineBackendFolderContentView(parent, "VideoFoldersBackend");
    }

    /// <summary>
    /// Defines the ContentView control for libraries on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineLibrariesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, VideosDefinitions.BackendLibraryDefinitionName, typeof (VideoLibrary));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(VideosDefinitions.BackendLibrariesListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendLibraryListView((ConfigElement) backendContentView.ViewsConfig, VideosDefinitions.BackendLibrariesListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("LibrariesBackendInsert", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendLibraryDetailsView(fluentContentView, "LibrariesBackendInsert", true, false)));
      backendContentView.ViewsConfig.AddLazy("LibrariesBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendLibraryDetailsView(fluentContentView, "LibrariesBackendEdit", false, true)));
      return backendContentView;
    }

    internal static ContentViewControlElement DefineFrontendVideosContentView(
      ConfigElement parent)
    {
      return VideosDefinitions.DefineFrontendContentView(parent, "VideosFrontend");
    }

    /// <summary>
    /// Defines the ContentView control for Videos on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <param name="controlDefinitionName">The definition name.</param>
    /// <param name="viewVideosByLibrary">Videos by library flag.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Video));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy(VideosDefinitions.BackendListViewName, (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendListView((ConfigElement) backendContentView.ViewsConfig, controlDefinitionName, VideosDefinitions.BackendListViewName, fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("VideosBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendDetailsView(fluentContentView, "VideosBackendEdit")));
      backendContentView.ViewsConfig.AddLazy("VideosBackendBulkEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendBulkEditView(fluentContentView, "VideosBackendBulkEdit")));
      backendContentView.ViewsConfig.AddLazy("VideosBackendVersionPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendVersionPreviewView(fluentContentView, "VideosBackendVersionPreview")));
      backendContentView.ViewsConfig.AddLazy("VideosVersionPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendPreviewViewName(fluentContentView, "VideosVersionPreview")));
      backendContentView.ViewsConfig.AddLazy("VideosBackendVersionComparisonView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineVersionComparisonView((ConfigElement) backendContentView.ViewsConfig, "VideosBackendVersionComparisonView")));
      backendContentView.ViewsConfig.AddLazy("SingleVideoUploadDetailsView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineSingleVideoUploadDetailsView(fluentContentView, "SingleVideoUploadDetailsView", FieldDisplayMode.Write)));
      return backendContentView;
    }

    private static ContentViewControlElement DefineBackendFolderContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Folder)).SetManagerType(typeof (LibrariesManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy("BackendFolderEditViewName", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) VideosDefinitions.DefineBackendFolderDetailsView(fluentContentView, "BackendFolderEditViewName")));
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
      comparisonViewElement1.ResourceClassId = VideosDefinitions.ResourceClassId;
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
      element6.ResourceClassId = VideosDefinitions.ResourceClassId;
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

    internal static DetailFormViewElement DefineBackendVersionPreviewView(
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
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("VersionPreviewVideo").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<VideosResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/").ShowNavigation().SetLocalization(localization).DoNotUseWorkflow().SupportMultilingual();
      DetailFormViewElement detailView = fluentDetailView.Get();
      VideosDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (VideosResources).Name);
      return detailView;
    }

    internal static DetailFormViewElement DefineBackendPreviewViewName(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("ViewItem").SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<VideosResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/").ShowNavigation().SupportMultilingual().DoNotUseWorkflow();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      VideosDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Read);
      return detailFormViewElement;
    }

    internal static MasterGridViewElement DefineBackendListView(
      ConfigElement parent,
      string controlDefinitionName,
      string viewName,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      string fullName1 = typeof (VideosDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js, Telerik.Sitefinity", "OnMasterViewLoaded");
      extenalClientScripts.Add(key1, string.Empty);
      extenalClientScripts.Add(key2, string.Empty);
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).LocalizeUsing<VideosResources>().SetTitle("ModuleTitle").SetViewType(typeof (LibrariesMasterGridView)).SetParentTitleFormat("LibraryVideosTitleFormat").SetCssClass("sfListViewGrid sfVideoList sfLibListView").SetExternalClientScripts(extenalClientScripts).SetClientMappedCommnadNames(VideosDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      masterGridViewElement.AddResourceString("VideosResources", "AllVideos");
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadVideoWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadVideosButton";
      element2.ResourceClassId = typeof (VideosResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageVideo";
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
      element3.PermissionSet = "VideoLibrary";
      element3.ActionName = "CreateVideoLibrary";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteVideoWidget";
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
      element5.Text = "ReorderVideos";
      element5.ResourceClassId = VideosDefinitions.ResourceClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Li;
      element5.WidgetType = typeof (CommandWidget);
      element5.CssClass = "";
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
      element7.Name = "PublishVideoWidget";
      element7.Text = "PublishVideos";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "groupPublish";
      element7.WidgetType = typeof (CommandWidget);
      element7.ResourceClassId = typeof (VideosResources).Name;
      element7.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems2 = element6.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element8.Name = "UnpublishVideoWidget";
      element8.Text = "UnpublishVideos";
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "groupUnpublish";
      element8.WidgetType = typeof (CommandWidget);
      element8.ResourceClassId = typeof (VideosResources).Name;
      element8.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems3 = element6.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element9.Name = "BulkEditWidget";
      element9.Text = "BulkEditVideoProperties";
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "bulkEdit";
      element9.WidgetType = typeof (CommandWidget);
      element9.ResourceClassId = typeof (VideosResources).Name;
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
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (Video)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Video), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Video), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.HeaderText = "SortVideos";
      commandWidgetElement1.MoreLinkText = "More";
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = VideosDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort";
      commandWidgetElement1.ContentType = typeof (Video);
      DynamicCommandWidgetElement element11 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element11);
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
      ModeStateWidgetElement stateWidgetElement = new ModeStateWidgetElement((ConfigElement) element1.Items);
      stateWidgetElement.Name = "StateWidget";
      stateWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      stateWidgetElement.WidgetType = typeof (StateWidget);
      stateWidgetElement.CssClass = "sfMasterViews";
      ModeStateWidgetElement element12 = stateWidgetElement;
      ConfigElementList<StateCommandWidgetElement> states = element12.States;
      StateCommandWidgetElement element13 = new StateCommandWidgetElement((ConfigElement) states);
      element13.Name = "List";
      element13.WrapperTagKey = HtmlTextWriterTag.Li;
      element13.CommandName = "listViewState";
      element13.ButtonType = CommandButtonType.SimpleLinkButton;
      element13.CssClass = "sfMasterViewsBoxes";
      element13.Text = "Box";
      element13.ToolTip = "ThumbnailsView";
      element13.ResourceClassId = typeof (Labels).Name;
      states.Add(element13);
      StateCommandWidgetElement element14 = new StateCommandWidgetElement((ConfigElement) states);
      element14.Name = "Grid";
      element14.WrapperTagKey = HtmlTextWriterTag.Li;
      element14.CommandName = "gridViewState";
      element14.ButtonType = CommandButtonType.SimpleLinkButton;
      element14.CssClass = "sfMasterViewsGrid";
      element14.Text = "Grid";
      element14.ToolTip = "ListView";
      element14.ResourceClassId = typeof (Labels).Name;
      states.Add(element14);
      element1.Items.Add((WidgetElement) element12);
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      LocalizationWidgetBarSectionElement barSectionElement1 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections);
      barSectionElement1.Name = "LanguagesWidgetBar";
      barSectionElement1.Title = "Languages";
      barSectionElement1.ResourceClassId = typeof (LocalizationResources).Name;
      barSectionElement1.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement1.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element15 = barSectionElement1;
      ConfigElementList<WidgetElement> items5 = element15.Items;
      LanguagesDropDownListWidgetElement element16 = new LanguagesDropDownListWidgetElement((ConfigElement) element15.Items);
      element16.Name = "LanguagesDropDown";
      element16.Text = "Languages";
      element16.ResourceClassId = typeof (LocalizationResources).Name;
      element16.CssClass = "";
      element16.WidgetType = typeof (LanguagesDropDownListWidget);
      element16.IsSeparator = false;
      element16.LanguageSource = LanguageSource.Frontend;
      element16.AddAllLanguagesOption = false;
      element16.CommandName = "changeLanguage";
      items5.Add((WidgetElement) element16);
      masterGridViewElement.SidebarConfig.Sections.Add((WidgetBarSectionElement) element15);
      WidgetBarSectionElement element17 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "VideosMainSection",
        CssClass = "sfFilterBy sfLibFolders sfSeparator",
        WrapperTagId = "mainSection"
      };
      DynamicCommandWidgetElement commandWidgetElement2 = new DynamicCommandWidgetElement((ConfigElement) element17.Items);
      commandWidgetElement2.Name = "FolderFilter";
      commandWidgetElement2.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement2.IsSeparator = false;
      commandWidgetElement2.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/predecessors/";
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
      commandWidgetElement2.ClientItemTemplate = "<a sys:href='{{ Url }}'>{{ Title.htmlEncode() }}</a>";
      commandWidgetElement2.ParentDataKeyName = "ParentId";
      commandWidgetElement2.SortExpression = "Title";
      commandWidgetElement2.PageSize = 100;
      DynamicCommandWidgetElement element18 = commandWidgetElement2;
      element18.UrlParameters.Add("provider", "");
      element17.Items.Add((WidgetElement) element18);
      masterGridViewElement.SidebarConfig.Sections.Add(element17);
      WidgetBarSectionElement element19 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ShowAllMediaItems",
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "showAllMediaItemsSection"
      };
      ConfigElementList<WidgetElement> items6 = element19.Items;
      CommandWidgetElement element20 = new CommandWidgetElement((ConfigElement) element19.Items);
      element20.Name = "AllMediaItems";
      element20.CommandName = "showAllMediaItems";
      element20.ButtonType = CommandButtonType.SimpleLinkButton;
      element20.Text = "FilterVideosBy";
      element20.ResourceClassId = VideosDefinitions.ResourceClassId;
      element20.WidgetType = typeof (CommandWidget);
      element20.IsSeparator = false;
      items6.Add((WidgetElement) element20);
      masterGridViewElement.SidebarConfig.Sections.Add(element19);
      WidgetBarSectionElement element21 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ShowAllLibraries",
        CssClass = "sfWidgetsList sfSeparator sfLibFolder",
        WrapperTagId = "showAllLibrariesSection"
      };
      ConfigElementList<WidgetElement> items7 = element21.Items;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) element21.Items);
      element22.Name = "AllLibraries";
      element22.CommandName = "viewLibraries";
      element22.ButtonType = CommandButtonType.SimpleLinkButton;
      element22.Text = "VideosByLibrary";
      element22.ResourceClassId = VideosDefinitions.ResourceClassId;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      items7.Add((WidgetElement) element22);
      masterGridViewElement.SidebarConfig.Sections.Add(element21);
      WidgetBarSectionElement barSectionElement2 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "MoreOptions",
        Title = "FilterVideos",
        ResourceClassId = VideosDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "moreFiltersSection"
      };
      ConfigElementList<WidgetElement> items8 = barSectionElement2.Items;
      CommandWidgetElement element23 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element23.Name = "AllVideos";
      element23.CommandName = "showAllItems";
      element23.ButtonType = CommandButtonType.SimpleLinkButton;
      element23.ButtonCssClass = "sfSel";
      element23.Text = "AllVideos";
      element23.ResourceClassId = VideosDefinitions.ResourceClassId;
      element23.WidgetType = typeof (CommandWidget);
      element23.IsSeparator = false;
      items8.Add((WidgetElement) element23);
      ConfigElementList<WidgetElement> items9 = barSectionElement2.Items;
      CommandWidgetElement element24 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element24.Name = "MyVideos";
      element24.CommandName = "showMyItems";
      element24.ButtonType = CommandButtonType.SimpleLinkButton;
      element24.Text = "MyVideos";
      element24.ResourceClassId = VideosDefinitions.ResourceClassId;
      element24.WidgetType = typeof (CommandWidget);
      element24.IsSeparator = false;
      items9.Add((WidgetElement) element24);
      ConfigElementList<WidgetElement> items10 = barSectionElement2.Items;
      CommandWidgetElement element25 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element25.Name = "ShowDraftVideos";
      element25.CommandName = "showMasterItems";
      element25.ButtonType = CommandButtonType.SimpleLinkButton;
      element25.Text = "DraftVideos";
      element25.ResourceClassId = VideosDefinitions.ResourceClassId;
      element25.WidgetType = typeof (CommandWidget);
      element25.IsSeparator = false;
      items10.Add((WidgetElement) element25);
      ConfigElementList<WidgetElement> items11 = barSectionElement2.Items;
      CommandWidgetElement element26 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element26.Name = "ShowPublishedVideos";
      element26.CommandName = "showPublishedItems";
      element26.ButtonType = CommandButtonType.SimpleLinkButton;
      element26.Text = "PublishedVideos";
      element26.ResourceClassId = VideosDefinitions.ResourceClassId;
      element26.WidgetType = typeof (CommandWidget);
      element26.IsSeparator = false;
      items11.Add((WidgetElement) element26);
      ConfigElementList<WidgetElement> items12 = barSectionElement2.Items;
      CommandWidgetElement element27 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element27.Name = "PendingApprovalVideos";
      element27.CommandName = "showPendingApprovalItems";
      element27.ButtonType = CommandButtonType.SimpleLinkButton;
      element27.Text = "WaitingForApproval";
      element27.ResourceClassId = typeof (LibrariesResources).Name;
      element27.WidgetType = typeof (CommandWidget);
      element27.IsSeparator = false;
      items12.Add((WidgetElement) element27);
      ConfigElementList<WidgetElement> items13 = barSectionElement2.Items;
      CommandWidgetElement element28 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element28.Name = "PendingReviewVideos";
      element28.CommandName = "showPendingReviewItems";
      element28.ButtonType = CommandButtonType.SimpleLinkButton;
      element28.Text = "WaitingForReview";
      element28.ResourceClassId = typeof (LibrariesResources).Name;
      element28.WidgetType = typeof (CommandWidget);
      element28.IsSeparator = false;
      items13.Add((WidgetElement) element28);
      ConfigElementList<WidgetElement> items14 = barSectionElement2.Items;
      CommandWidgetElement element29 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element29.Name = "PendingPublishingVideos";
      element29.CommandName = "showPendingPublishingItems";
      element29.ButtonType = CommandButtonType.SimpleLinkButton;
      element29.Text = "WaitingForPublishing";
      element29.ResourceClassId = typeof (LibrariesResources).Name;
      element29.WidgetType = typeof (CommandWidget);
      element29.IsSeparator = false;
      items14.Add((WidgetElement) element29);
      ConfigElementList<WidgetElement> items15 = barSectionElement2.Items;
      CommandWidgetElement element30 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element30.Name = "RejectedVideos";
      element30.CommandName = "showRejectedItems";
      element30.ButtonType = CommandButtonType.SimpleLinkButton;
      element30.Text = "Rejected";
      element30.ResourceClassId = typeof (Labels).Name;
      element30.WidgetType = typeof (CommandWidget);
      element30.IsSeparator = false;
      items15.Add((WidgetElement) element30);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) barSectionElement2.Items);
      ConfigElementList<WidgetElement> items16 = barSectionElement2.Items;
      LiteralWidgetElement element31 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element31.Name = "Separator";
      element31.WrapperTagKey = HtmlTextWriterTag.Li;
      element31.WidgetType = typeof (LiteralWidget);
      element31.CssClass = "sfSeparator";
      element31.Text = "&nbsp;";
      element31.IsSeparator = true;
      items16.Add((WidgetElement) element31);
      WidgetBarSectionElement element32 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ByDate",
        Title = "VideosByDate",
        ResourceClassId = VideosDefinitions.ResourceClassId,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      string[] strArray = new string[3]
      {
        element32.WrapperTagId,
        element17.WrapperTagId,
        element19.WrapperTagId
      };
      string[] taxonomySection = DefinitionsHelper.CreateTaxonomySection<Video>(masterGridViewElement.SidebarConfig.Sections, barSectionElement2, "Video", VideosDefinitions.ResourceClassId, strArray);
      masterGridViewElement.SidebarConfig.Sections.Add(element32);
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element32.Items);
      commandWidgetElement3.Name = "CloseDateFilter";
      commandWidgetElement3.CommandName = "showSectionsExceptAndResetFilter";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(taxonomySection);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "CloseDateFilter";
      commandWidgetElement3.ResourceClassId = VideosDefinitions.ResourceClassId;
      commandWidgetElement3.CssClass = "sfCloseFilter";
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element33 = commandWidgetElement3;
      element32.Items.Add((WidgetElement) element33);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element32.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element34 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element34.PredefinedFilteringRanges);
      element32.Items.Add((WidgetElement) element34);
      ConfigElementList<WidgetElement> items17 = barSectionElement2.Items;
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      commandWidgetElement4.Name = "FilterByDate";
      commandWidgetElement4.CommandName = "hideSectionsExcept";
      commandWidgetElement4.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element32.WrapperTagId);
      commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement4.Text = "ByDate";
      commandWidgetElement4.ResourceClassId = VideosDefinitions.ResourceClassId;
      commandWidgetElement4.WidgetType = typeof (CommandWidget);
      commandWidgetElement4.IsSeparator = false;
      CommandWidgetElement element35 = commandWidgetElement4;
      items17.Add((WidgetElement) element35);
      ConfigElementList<WidgetElement> items18 = barSectionElement2.Items;
      LiteralWidgetElement element36 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element36.Name = "Separator";
      element36.WrapperTagKey = HtmlTextWriterTag.Li;
      element36.WidgetType = typeof (LiteralWidget);
      element36.CssClass = "sfSeparator";
      element36.Text = "&nbsp;";
      element36.IsSeparator = true;
      items18.Add((WidgetElement) element36);
      masterGridViewElement.SidebarConfig.Sections.Add(barSectionElement2);
      WidgetBarSectionElement element37 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = VideosDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element37.Items);
        commandWidgetElement5.Name = "VideosComments";
        commandWidgetElement5.CommandName = "comments";
        commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement5.Text = "CommentsForVideos";
        commandWidgetElement5.ResourceClassId = VideosDefinitions.ResourceClassId;
        commandWidgetElement5.CssClass = "sfComments";
        commandWidgetElement5.WidgetType = typeof (CommandWidget);
        commandWidgetElement5.IsSeparator = false;
        CommandWidgetElement element38 = commandWidgetElement5;
        element37.Items.Add((WidgetElement) element38);
      }
      WidgetBarSectionElement element39 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForVideos",
        ResourceClassId = VideosDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSettings sfSeparator",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items19 = element39.Items;
      CommandWidgetElement element40 = new CommandWidgetElement((ConfigElement) element39.Items);
      element40.Name = "Permissions";
      element40.CommandName = "permissions";
      element40.ButtonType = CommandButtonType.SimpleLinkButton;
      element40.Text = "Permissions";
      element40.ResourceClassId = typeof (LibrariesResources).Name;
      element40.WidgetType = typeof (CommandWidget);
      element40.IsSeparator = false;
      items19.Add((WidgetElement) element40);
      CommandWidgetElement commandWidgetElement6 = new CommandWidgetElement((ConfigElement) element39.Items);
      commandWidgetElement6.Name = "ManageContentLocations";
      commandWidgetElement6.CommandName = "manageContentLocations";
      commandWidgetElement6.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement6.Text = "PagesWhereVideosArePublished";
      commandWidgetElement6.ResourceClassId = typeof (VideosResources).Name;
      commandWidgetElement6.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element41 = commandWidgetElement6;
      element39.Items.Add((WidgetElement) element41);
      ConfigElementList<WidgetElement> items20 = element39.Items;
      CommandWidgetElement element42 = new CommandWidgetElement((ConfigElement) element39.Items);
      element42.Name = "CustomFields";
      element42.CommandName = "moduleEditor";
      element42.ButtonType = CommandButtonType.SimpleLinkButton;
      element42.Text = "CustomFields";
      element42.ResourceClassId = typeof (LibrariesResources).Name;
      element42.WidgetType = typeof (CommandWidget);
      element42.IsSeparator = false;
      items20.Add((WidgetElement) element42);
      if (element37.Items.Count == 0)
        element37.Visible = new bool?(false);
      masterGridViewElement.SidebarConfig.Sections.Add(element37);
      masterGridViewElement.SidebarConfig.Sections.Add(element39);
      DefinitionsHelper.CreateRecycleBinLink(masterGridViewElement.SidebarConfig.Sections, typeof (Video).FullName);
      masterGridViewElement.SidebarConfig.Title = "ManageVideos";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (VideosResources).Name;
      LocalizationWidgetBarSectionElement barSectionElement3 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.ContextBarConfig.Sections);
      barSectionElement3.Name = "translations";
      barSectionElement3.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement3.CssClass = "sfContextWidgetWrp";
      barSectionElement3.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element43 = barSectionElement3;
      ConfigElementList<WidgetElement> items21 = element43.Items;
      CommandWidgetElement element44 = new CommandWidgetElement((ConfigElement) element43.Items);
      element44.Name = "ShowMoreTranslations";
      element44.CommandName = "showMoreTranslations";
      element44.ButtonType = CommandButtonType.SimpleLinkButton;
      element44.Text = "ShowAllTranslations";
      element44.ResourceClassId = typeof (LocalizationResources).Name;
      element44.WidgetType = typeof (CommandWidget);
      element44.IsSeparator = false;
      element44.CssClass = "sfShowHideLangVersions";
      element44.WrapperTagKey = HtmlTextWriterTag.Div;
      items21.Add((WidgetElement) element44);
      ConfigElementList<WidgetElement> items22 = element43.Items;
      CommandWidgetElement element45 = new CommandWidgetElement((ConfigElement) element43.Items);
      element45.Name = "HideMoreTranslations";
      element45.CommandName = "hideMoreTranslations";
      element45.ButtonType = CommandButtonType.SimpleLinkButton;
      element45.Text = "ShowBasicTranslationsOnly";
      element45.ResourceClassId = typeof (LocalizationResources).Name;
      element45.WidgetType = typeof (CommandWidget);
      element45.IsSeparator = false;
      element45.CssClass = "sfDisplayNone sfShowHideLangVersions";
      element45.WrapperTagKey = HtmlTextWriterTag.Div;
      items22.Add((WidgetElement) element45);
      masterGridViewElement.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element43);
      VideosDefinitions.DefineMasterContextBar(masterGridViewElement);
      DynamicListViewModeElement listViewModeElement = new DynamicListViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      listViewModeElement.Name = "List";
      listViewModeElement.VirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.VideosListItemTemplate.ascx");
      DynamicListViewModeElement element46 = listViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element46);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element47 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element47);
      string empty = string.Empty;
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element47.ColumnsConfig;
      DataColumnElement element48 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      element48.Name = "Video";
      element48.HeaderText = "Video";
      element48.ResourceClassId = typeof (VideosResources).Name;
      element48.HeaderCssClass = "sfImgTmb sfVideoSelector";
      element48.ItemCssClass = "sfImgTmb sfVideoSelector";
      element48.ClientTemplate = "<a sys:if='!IsFolder' sys:href='javascript:void(0);' class='sf_binderCommand_editMediaContentProperties'><img sys:src='{{SnapshotUrl}}' sys:alt='{{Title.htmlEncode()}}' height='60' /></a><a sys:if='IsFolder' sys:href='javascript:void(0);' class='sf_binderCommand_openLibrary sfSmallLibTmb'><span sys:class=\"{{ ((VideosCount == 0) ? 'sfDisplayNone' : 'sfImgFromLib') }}\"><img sys:src='{{SnapshotUrl}}' sys:alt='{{Title.htmlEncode()}}' /></span></a>";
      columnsConfig1.Add((ColumnElement) element48);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element47.ColumnsConfig;
      DataColumnElement element49 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      element49.Name = "TitleDurationStatus";
      element49.HeaderText = "TitleDurationStatus";
      element49.ResourceClassId = typeof (VideosResources).Name;
      element49.HeaderCssClass = "sfTitleCol";
      element49.ItemCssClass = "sfTitleCol";
      element49.ClientTemplate = "<span sys:if='!IsFolder'><strong sys:class=\"{{'sfItemTitle' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus'  : '')}}\"><a sys:href='javascript:void(0);' class='sf_binderCommand_editMediaContentProperties '>{{Title.htmlEncode()}}</a></strong>\r\n                                    <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span>\r\n                                    <span sys:class=\"{{ 'sfPrimaryStatus sf' + UIStatus.toLowerCase()}}\">\r\n                                        <span sys:if='AdditionalStatus' class='sfSep'>| </span>\r\n                                        {{Status}}\r\n                                    </span></span><span sys:if='IsFolder'><a sys:href='javascript:void(0);' class='sf_binderCommand_openLibrary sfItemTitle sfMBottom5'>{{Title.htmlEncode()}}</a>\r\n                                    <i sys:if='VideosCount' class='sfItemsCount'>{{ VideosCount }}</i>\r\n                                    <i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i></span>";
      columnsConfig2.Add((ColumnElement) element49);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element47.ColumnsConfig);
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
      element47.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element47.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.HeaderText = "Actions";
      menuColumnElement1.HeaderCssClass = "sfRegular";
      menuColumnElement1.ItemCssClass = "sfRegular";
      menuColumnElement1.ResourceClassId = typeof (LibrariesResources).Name;
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      menuColumnElement2.MainAction = DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2, "EmbedVideo", HtmlTextWriterTag.Li, "embedMediaContent sf_NotForFolder", "EmbedThisVideo", VideosDefinitions.ResourceClassId);
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "PlayOriginal", HtmlTextWriterTag.Li, "playOriginalVideo sf_NotForFolder", "PlayOriginal", typeof (VideosResources).Name, "sfPlayItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", typeof (LibrariesResources).Name, "sfDeleteItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "MoveTo", HtmlTextWriterTag.Li, "moveToSingle", "MoveTo", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Publish", HtmlTextWriterTag.Li, "publish sf_NotForFolder", "Publish", typeof (LibrariesResources).Name, "sfPublishItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Unpublish", HtmlTextWriterTag.Li, "unpublish sf_NotForFolder", "Unpublish", typeof (LibrariesResources).Name, "sfUnpublishItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Download", HtmlTextWriterTag.Li, "download sf_NotForFolder", "Download", typeof (LibrariesResources).Name, "sfDownloadItm"));
      menuColumnElement2.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuColumnElement2.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator sfSepNoTitle", string.Empty, string.Empty));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditProperties", HtmlTextWriterTag.Li, "editMediaContentProperties sf_NotForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditFolderProperties", HtmlTextWriterTag.Li, "editFolder sf_ForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "RelocateLibrary", HtmlTextWriterTag.Li, "relocateLibrary sf_ForFolder", "RelocateLibrary", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "SetPermissions", HtmlTextWriterTag.Li, "permissions sf_NotForFolder", "SetPermissions", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "History", HtmlTextWriterTag.Li, "historygrid sf_NotForFolder", "HistoryMenuItemTitle", typeof (VersionResources).Name));
      element47.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element47.ColumnsConfig;
      DataColumnElement element50 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      element50.Name = "FileSize";
      element50.HeaderText = "FileSize";
      element50.ResourceClassId = typeof (LibrariesResources).Name;
      element50.ClientTemplate = "<span sys:if='!IsFolder'><p class='sfLine'>{{Extension.substring(1).toUpperCase()}}</p><p class='sfLine'>{{TotalSize}} KB</p></span>";
      element50.HeaderCssClass = "sfRegular";
      element50.ItemCssClass = "sfRegular";
      columnsConfig3.Add((ColumnElement) element50);
      string str = string.Format("<p class='sfLine'>{0} <a sys:href='{6}'>{1}</a></p><p class='sfLine'>{2} {3}</p><p class='sfLine'>{4} {5}</p>", (object) Res.Get<LibrariesResources>().LibraryLabel, (object) "{{FolderTitle ? FolderTitle.htmlEncode() : LibraryTitle.htmlEncode()}}", (object) Res.Get<LibrariesResources>().Categories, (object) "{{CategoryText}}", (object) Res.Get<LibrariesResources>().Tags, (object) "{{TagsText}}", (object) "{{LibraryFullUrl}}");
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element47.ColumnsConfig;
      DataColumnElement element51 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      element51.Name = "LibraryCategoriesTags";
      element51.HeaderText = "LibraryCategoriesTags";
      element51.ResourceClassId = typeof (VideosResources).Name;
      element51.ClientTemplate = "<span sys:if='!IsFolder'>" + str + "</span>";
      element51.HeaderCssClass = "sfMedium";
      element51.ItemCssClass = "sfMedium";
      columnsConfig4.Add((ColumnElement) element51);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      dataColumnElement1.Name = "Owner";
      dataColumnElement1.HeaderText = "Owner";
      dataColumnElement1.ResourceClassId = typeof (Labels).Name;
      dataColumnElement1.ClientTemplate = "<span sys:if='!IsFolder' class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement1.HeaderCssClass = "sfRegular";
      dataColumnElement1.ItemCssClass = "sfRegular";
      DataColumnElement element52 = dataColumnElement1;
      element47.ColumnsConfig.Add((ColumnElement) element52);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element47.ColumnsConfig);
      dataColumnElement2.Name = "Date";
      dataColumnElement2.HeaderText = "Date";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "<span sys:if='!IsFolder'>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss') : '-' }}</span>";
      dataColumnElement2.HeaderCssClass = "sfDateAndHour";
      dataColumnElement2.ItemCssClass = "sfDateAndHour";
      DataColumnElement element53 = dataColumnElement2;
      element47.ColumnsConfig.Add((ColumnElement) element53);
      DecisionScreenElement element54 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoItemsExist",
        ResourceClassId = VideosDefinitions.ResourceClassId
      };
      ConfigElementList<CommandWidgetElement> actions1 = element54.Actions;
      CommandWidgetElement element55 = new CommandWidgetElement((ConfigElement) element54.Actions);
      element55.Name = "UploadVideos";
      element55.ButtonType = CommandButtonType.Create;
      element55.CommandName = "uploadFile";
      element55.Text = "UploadVideosLabel";
      element55.ResourceClassId = typeof (VideosResources).Name;
      element55.CssClass = "sfCreateItem";
      element55.ActionName = "ManageVideo";
      actions1.Add(element55);
      ConfigElementList<CommandWidgetElement> actions2 = element54.Actions;
      CommandWidgetElement element56 = new CommandWidgetElement((ConfigElement) element54.Actions);
      element56.Name = "CreateLibrary";
      element56.ButtonType = CommandButtonType.Standard;
      element56.CommandName = "create";
      element56.Text = "CreateLibrary";
      element56.ResourceClassId = typeof (LibrariesResources).Name;
      element56.CssClass = "sfCreateAlbum";
      element56.PermissionSet = "VideoLibrary";
      element56.ActionName = "CreateVideoLibrary";
      actions2.Add(element56);
      ConfigElementList<CommandWidgetElement> actions3 = element54.Actions;
      CommandWidgetElement element57 = new CommandWidgetElement((ConfigElement) element54.Actions);
      element57.Name = "ManageLibraries";
      element57.ButtonType = CommandButtonType.Standard;
      element57.CommandName = "viewLibraries";
      element57.Text = "ManageLibraries";
      element57.ResourceClassId = typeof (LibrariesResources).Name;
      element57.CssClass = "sfCreateAlbum";
      element57.PermissionSet = "VideoLibrary";
      element57.ActionName = "CreateVideoLibrary";
      actions3.Add(element57);
      masterGridViewElement.DecisionScreensConfig.Add(element54);
      string parameters1 = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&showPrompt={5}", (object) Res.Get<VideosResources>().Video, (object) Res.Get<VideosResources>().Videos, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Video).FullName, (object) typeof (VideoLibrary).FullName, (object) true);
      string parameters2 = string.Format("?itemName={0}&itemNameWithArticle={1}&itemsName={2}&libraryTypeName={3}&itemType={4}&parentType={5}&folderId=[[folderId]]", (object) Res.Get<VideosResources>().Video, (object) Res.Get<VideosResources>().VideoWithArticle, (object) Res.Get<VideosResources>().Videos, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Video).FullName, (object) typeof (VideoLibrary).FullName);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<VideosResources>().Video, Res.Get<VideosResources>().Videos, Res.Get<LibrariesResources>().Library, typeof (VideoLibrary), "~/Sitefinity/Services/Content/VideoService.svc/", "~/Sitefinity/Services/Content/VideoLibraryService.svc/").AddParameters("&folderId={{FolderId}}").Done().AddEditDialog("VideosBackendEdit", "", "", "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddInsertDialog("LibrariesBackendInsert", VideosDefinitions.BackendLibraryDefinitionName).AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("LibrariesBackendEdit", VideosDefinitions.BackendLibraryDefinitionName, "", "", (Type) null, "editLibraryProperties").Done().AddEditDialog("VideosBackendBulkEdit", "", "", "", (Type) null, "bulkEdit").AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("BackendFolderEditViewName", "VideoFoldersBackend", "", "{{RootId}}", typeof (DocumentLibrary), "editFolder").Done().AddDialog<LibrarySelectorDialog>("selectLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToAll").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters1).Done().AddDialog<EmbedDialog>("embedMediaContent").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(425)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters("?mode=videos").Done().AddDialog<MediaPlayerDialog>("playOriginalVideo").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).DisplayTitleBar().SetWidth(Unit.Pixel(710)).SetHeight(Unit.Pixel(425)).MakeModal().Done().AddDialog<ReorderDialog>("reorder").MakeFullScreen().SetParameters(parameters2).Done().AddPermissionsDialog(Res.Get<VideosResources>().BackToItems, Res.Get<VideosResources>().PermissionsForVideos, string.Join(",", new string[3]
      {
        "Video",
        "VideoLibrary",
        "VideosSitemapGeneration"
      })).Done().AddPermissionsDialog(Res.Get<VideosResources>().BackToItems, Res.Get<VideosResources>().PermissionsForVideos, string.Join(",", new string[2]
      {
        "Video",
        "VideoLibrary"
      }), "libraryPermissionsDialog", typeof (VideoLibrary)).Done().AddDialog<CustomSortingDialog>("editCustomSortingExpression").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).DisplayTitleBar().SetWidth(Unit.Pixel(500)).SetHeight(Unit.Pixel(600)).MakeModal().SetParameters("?contentType=" + typeof (Video).FullName).Done().AddHistoryComparisonDialog("VideosBackendVersionComparisonView", "", Res.Get<VideosResources>().BackToItems, "", "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddHistoryGridDialog("VideosBackendVersionComparisonView", "", Res.Get<VideosResources>().BackToItems, "", "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddHistoryPreviewDialog("VideosBackendVersionPreview", "", Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddPreviewDialog("VideosVersionPreview", "", "", "{{ParentId}}", typeof (VideoLibrary)).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetParameters("?mode=RelocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddCustomFieldsDialog(Res.Get<VideosResources>().BackToItems, Res.Get<VideosResources>().VideosDataFields, Res.Get<VideosResources>().VideoPluralItemName);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Video).FullName
      });
      ConfigElementList<WidgetElement> titleWidgetsConfig = masterGridViewElement.TitleWidgetsConfig;
      CommandWidgetElement element58 = new CommandWidgetElement((ConfigElement) masterGridViewElement.TitleWidgetsConfig);
      element58.Name = "ViewAllLibrariesCommand";
      element58.CommandName = "viewLibraries";
      element58.ButtonType = CommandButtonType.SimpleLinkButton;
      element58.WidgetType = typeof (CommandWidget);
      element58.Text = "AllLibraries";
      element58.ResourceClassId = typeof (LibrariesResources).Name;
      element58.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element58);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllLibrariesLink",
        CommandName = "viewLibraries",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.VideosHomePageId)
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllVideos",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryVideosPageId) + "/?displayMode=allVideos"
      });
      string fullName2 = typeof (Video).FullName;
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullName2
      });
      return masterGridViewElement;
    }

    internal static DetailFormViewElement DefineBackendLibraryDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      bool isCreateMode,
      bool showDeleteButton)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isCreateMode ? "CreateLibrary" : "EditLibrary").HideTopToolbar().LocalizeUsing<LibrariesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoLibraryService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual().SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      DetailFormViewElement cfg = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("LibraryName").SetExample("LibraryNameExample").SetCssClass("sfTitleField").LocalizeUsing<VideosResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("LibraryNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("LibraryDescription").SetCssClass("sfFormSeparator").LocalizeUsing<VideosResources>().SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done().Done();
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
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "VideoItemName";
      fields.Add((FieldDefinitionElement) element1);
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("RootLibrarySection").SetTitle("RootLibrarySettings");
      ContentViewSectionElement viewSectionElement2 = definitionFacade4.Get();
      definitionFacade4.AddTextField("MaxSize").SetId("MaxLibrarySizeFieldControl").SetTitle("MaxLibrarySize").SetDescription("Mb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").AddValidation().SetRegularExpression("\\d").LocalizeUsingDefaultErrorMessages().SetMessageCssClass("sfError").Done().Done().AddTextField("MaxItemSize").SetId("MaxVideoSizeFieldControl").SetTitle("MaxVideoSize").SetDescription("Kb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").AddValidation().SetRegularExpression("\\d").LocalizeUsingDefaultErrorMessages().SetMessageCssClass("sfError").Done().Done();
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
        element4.Text = "CreateAndGoToUploadVideos";
        element4.ResourceClassId = VideosDefinitions.ResourceClassId;
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
      element5.ResourceClassId = VideosDefinitions.ResourceClassId;
      element5.WrapperTagKey = HtmlTextWriterTag.Span;
      element5.WidgetType = typeof (CommandWidget);
      element5.CssClass = isCreateMode ? "" : "sfSave";
      items1.Add((WidgetElement) element5);
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element3.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (VideosResources).Name;
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
          element7.ResourceClassId = typeof (VideosResources).Name;
          element7.WidgetType = typeof (CommandWidget);
          menuItems.Add((WidgetElement) element7);
        }
        ConfigElementList<WidgetElement> menuItems1 = element6.MenuItems;
        CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
        element8.Name = "PermissionsCommandName";
        element8.ButtonType = CommandButtonType.SimpleLinkButton;
        element8.Text = "SetPermissions";
        element8.CommandName = "permissions";
        element8.ResourceClassId = typeof (VideosResources).Name;
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

    private static MasterGridViewElement DefineBackendLibraryListView(
      ConfigElement parent,
      string viewName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      string fullName1 = typeof (VideosDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js, Telerik.Sitefinity", "OnMasterViewLoaded");
      extenalClientScripts.Add(key1, string.Empty);
      extenalClientScripts.Add(key2, string.Empty);
      MasterViewDefinitionFacade definitionFacade = fluentFacade.AddMasterView(viewName).LocalizeUsing<VideosResources>().SetTitle("ModuleTitle").SetCssClass("sfListViewGrid sfLibListView").SetExternalClientScripts(extenalClientScripts).SetClientMappedCommnadNames(VideosDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoLibraryService.svc/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadVideoWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadVideosButton";
      element2.ResourceClassId = typeof (VideosResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageVideo";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CreateLibraryWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "create";
      element3.Text = "CreateLibrary";
      element3.ResourceClassId = VideosDefinitions.ResourceClassId;
      element3.WidgetType = typeof (CommandWidget);
      element3.PermissionSet = "VideoLibrary";
      element3.ActionName = "CreateVideoLibrary";
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
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (VideoLibrary)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.HeaderText = "Sort";
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = VideosDefinitions.ResourceClassId;
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
      element7.HeaderText = "Library";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.HeaderCssClass = "sfImgTmb sfVideoSelector";
      element7.ItemCssClass = "sfImgTmb sfVideoSelector";
      element7.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_viewItemsByParent sfSmallLibTmb'><span sys:class=\"{{ ((MediaItemsCount == 0) ? 'sfDisplayNone' : 'sfImgFromLib') }}\"><img sys:src='{{ThumbnailUrl}}' sys:alt='{{Title.htmlEncode()}}' /></span></a>";
      columnsConfig1.Add((ColumnElement) element7);
      string sitefinityTextResource = ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibraryCommonActionsColumn.htm");
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
      element9.ClientTemplate = sitefinityTextResource.Replace("UploadMediaName", "{$VideosResources, UploadVideos$}");
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
      element11.ClientTemplate = "<p>{{getDateTemplate(LastUploadedDate, 'dd MMM, yyyy', '{$VideosResources, LastUploaded$}')}}</p>";
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
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/predecessors/";
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
      commandWidgetElement2.ClientItemTemplate = "<a sys:href='{{ Url }}'>{{ Title.htmlEncode() }}</a>";
      commandWidgetElement2.ParentDataKeyName = "ParentId";
      commandWidgetElement2.SortExpression = "Title";
      commandWidgetElement2.PageSize = 100;
      DynamicCommandWidgetElement element13 = commandWidgetElement2;
      element13.UrlParameters.Add("provider", "");
      element12.Items.Add((WidgetElement) element13);
      masterGridViewElement.SidebarConfig.Sections.Add(element12);
      masterGridViewElement.SidebarConfig.Title = "ManageVideos";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (VideosResources).Name;
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
      element15.Text = "FilterVideosBy";
      element15.ResourceClassId = VideosDefinitions.ResourceClassId;
      element15.WidgetType = typeof (CommandWidget);
      element15.IsSeparator = false;
      items4.Add((WidgetElement) element15);
      masterGridViewElement.SidebarConfig.Sections.Add(element14);
      WidgetBarSectionElement element16 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = VideosDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element16.Items);
        commandWidgetElement3.Name = "VideosComments";
        commandWidgetElement3.CommandName = "comments";
        commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement3.Text = "CommentsForVideos";
        commandWidgetElement3.ResourceClassId = VideosDefinitions.ResourceClassId;
        commandWidgetElement3.CssClass = "sfComments";
        commandWidgetElement3.WidgetType = typeof (CommandWidget);
        commandWidgetElement3.IsSeparator = false;
        CommandWidgetElement element17 = commandWidgetElement3;
        element16.Items.Add((WidgetElement) element17);
      }
      WidgetBarSectionElement element18 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForVideos",
        ResourceClassId = VideosDefinitions.ResourceClassId,
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
      commandWidgetElement4.Text = "PagesWhereVideosArePublished";
      commandWidgetElement4.ResourceClassId = typeof (VideosResources).Name;
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
      string parameters = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&showPrompt={5}", (object) Res.Get<VideosResources>().Video, (object) Res.Get<VideosResources>().Videos, (object) Res.Get<LibrariesResources>().Library, (object) typeof (Video).FullName, (object) typeof (VideoLibrary).FullName, (object) true);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<VideosResources>().Video, Res.Get<VideosResources>().Videos, Res.Get<VideosResources>().Library, typeof (VideoLibrary), "~/Sitefinity/Services/Content/VideoService.svc/", "~/Sitefinity/Services/Content/VideoLibraryService.svc/", "", typeof (Video)).AddParameters("&LibraryId={{Id}}").Done().AddInsertDialog("LibrariesBackendInsert", "", Res.Get<VideosResources>().BackToAllLibraries).AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("LibrariesBackendEdit", "", Res.Get<VideosResources>().BackToAllLibraries).Done().AddPermissionsDialog(Res.Get<VideosResources>().BackToAllLibraries, Res.Get<VideosResources>().PermissionsForVideos, string.Join(",", new string[3]
      {
        "Video",
        "VideoLibrary",
        "VideosSitemapGeneration"
      })).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibraryRelocateDialog>("transferLibrary").SetParameters("?mode=TransferLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters).Done().AddCustomFieldsDialog(Res.Get<VideosResources>().BackToItems, Res.Get<VideosResources>().VideosDataFields, Res.Get<VideosResources>().VideoPluralItemName, typeof (Video));
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewVideosByLibrary",
        CommandName = "viewItemsByParent",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryVideosPageId) + "{{Url}}/"
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Video).FullName
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllImages",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryVideosPageId) + "/?displayMode=allVideos"
      });
      string fullName2 = typeof (Video).FullName;
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
      DetailViewDefinitionFacade fluentDetailView = fluentContentView.AddDetailView(viewName).SetTitle("EditVideo").LocalizeUsing<VideosResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/").SetAlternativeTitle("CreateItem").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = fluentDetailView.Get();
      VideosDefinitions.CreateBackendSections(fluentDetailView, FieldDisplayMode.Write);
      return detailFormViewElement;
    }

    private static void CreateBackendSections(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      if (displayMode == FieldDisplayMode.Read)
        fluentDetailView.AddReadOnlySection("Sidebar").AddVersionNoteControl();
      if (fluentDetailView.Get().ViewName == "VideosBackendEdit")
        fluentDetailView.AddSection("toolbarSection").AddLanguageListField();
      fluentDetailView.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade1.Get();
      string title = displayMode == FieldDisplayMode.Write ? "Title" : string.Empty;
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade2 = definitionFacade1.AddLocalizedTextField("Title").SetId("VideoTitleFieldControl").SetTitle(title).SetCssClass("sfTitleField sfMBottom25").LocalizeUsing<LibrariesResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      if (displayMode == FieldDisplayMode.Write)
        definitionFacade1.AddLanguageChoiceField("AvailableLanguages").SetDisplayMode(displayMode).Done();
      if (displayMode == FieldDisplayMode.Read)
      {
        FileFieldDefinitionElement definitionElement = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
        definitionElement.ID = "MediaFieldControl";
        definitionElement.DataFieldName = "MediaUrl";
        definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
        definitionElement.CssClass = "";
        definitionElement.WrapperTag = HtmlTextWriterTag.Li;
        definitionElement.FieldType = typeof (MediaField);
        definitionElement.LibraryContentType = typeof (Video);
        definitionElement.ResourceClassId = VideosDefinitions.ResourceClassId;
        definitionElement.ItemName = "Video";
        definitionElement.ItemNamePlural = "Videos";
        definitionElement.IsMultiselect = false;
        definitionElement.MaxFileCount = 1;
        FileFieldDefinitionElement element1 = definitionElement;
        viewSectionElement1.Fields.Add((FieldDefinitionElement) element1);
        ImageFieldElement imageFieldElement = new ImageFieldElement((ConfigElement) viewSectionElement1.Fields);
        imageFieldElement.ID = "EditThumbnailFieldControl";
        imageFieldElement.Title = "Thumbnail";
        imageFieldElement.DataFieldName = "ThumbnailUrl";
        imageFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Read);
        imageFieldElement.CssClass = "sfFormSeparator sfVideoTmbWrp";
        imageFieldElement.WrapperTag = HtmlTextWriterTag.Li;
        imageFieldElement.FieldType = typeof (ThumbnailField);
        imageFieldElement.ResourceClassId = VideosDefinitions.ResourceClassId;
        ImageFieldElement element2 = imageFieldElement;
        viewSectionElement1.Fields.Add((FieldDefinitionElement) element2);
      }
      else if (displayMode == FieldDisplayMode.Write)
      {
        FileFieldDefinitionElement definitionElement = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
        definitionElement.ID = "MediaFieldControl";
        definitionElement.Title = "Thumbnail";
        definitionElement.DataFieldName = "MediaUrl";
        definitionElement.DisplayMode = new FieldDisplayMode?(displayMode);
        definitionElement.CssClass = "";
        definitionElement.WrapperTag = HtmlTextWriterTag.Li;
        definitionElement.FieldType = typeof (MediaField);
        definitionElement.LibraryContentType = typeof (Video);
        definitionElement.ResourceClassId = VideosDefinitions.ResourceClassId;
        definitionElement.ItemName = "Video";
        definitionElement.ItemNamePlural = "Videos";
        definitionElement.IsMultiselect = false;
        definitionElement.MaxFileCount = 1;
        FileFieldDefinitionElement element3 = definitionElement;
        viewSectionElement1.Fields.Add((FieldDefinitionElement) element3);
        ImageFieldElement imageFieldElement = new ImageFieldElement((ConfigElement) viewSectionElement1.Fields);
        imageFieldElement.ID = "EditThumbnailFieldControl";
        imageFieldElement.Title = "Thumbnail";
        imageFieldElement.DataFieldName = "ThumbnailUrl";
        imageFieldElement.DisplayMode = new FieldDisplayMode?(displayMode);
        imageFieldElement.CssClass = "sfFormSeparator sfVideoTmbWrp";
        imageFieldElement.WrapperTag = HtmlTextWriterTag.Li;
        imageFieldElement.FieldType = typeof (ThumbnailField);
        imageFieldElement.ResourceClassId = VideosDefinitions.ResourceClassId;
        imageFieldElement.UploadMode = new ImageFieldUploadMode?(ImageFieldUploadMode.Dialog);
        ImageFieldElement element4 = imageFieldElement;
        viewSectionElement1.Fields.Add((FieldDefinitionElement) element4);
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "LibraryFieldControl";
        folderFieldElement1.DataFieldName = "Library";
        folderFieldElement1.ItemName = "VideoItemName";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Library";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (LibrariesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeLibraryInSpan",
          ResourceClassId = typeof (VideosResources).Name
        };
        viewSectionElement1.Fields.Add((FieldDefinitionElement) folderFieldElement2);
      }
      ContentViewSectionElement viewSectionElement2 = fluentDetailView.AddExpandableSection("TaxonomiesSection").SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element5 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element5.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element5);
      FlatTaxonFieldDefinitionElement element6 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element6.DisplayMode = new FieldDisplayMode?(displayMode);
      element6.CssClass = "sfFormSeparator";
      element6.Description = "TagsFieldInstructions";
      element6.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element6);
      fluentDetailView.AddExpandableSection("DetailsSection").SetTitle("Details").LocalizeUsing<LibrariesResources>().AddLocalizedTextField("Author").SetId("AuthorFieldControl").SetTitle("Author").Done().AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").LocalizeUsing<VideosResources>().SetRows(5).Done();
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
        ContentWorkflowStatusInfoFieldElement element7 = new ContentWorkflowStatusInfoFieldElement((ConfigElement) viewSectionElement3.Fields);
        element7.DisplayMode = new FieldDisplayMode?(displayMode);
        element7.FieldName = "VideosWorkflowStatusInfoField";
        element7.ResourceClassId = typeof (VideosResources).Name;
        element7.WrapperTag = HtmlTextWriterTag.Li;
        element7.FieldType = typeof (ContentWorkflowStatusInfoField);
        fields.Add((FieldDefinitionElement) element7);
        definitionFacade4.AddContentLocationInfoField();
        definitionFacade4.AddRelatingDataField();
      }
      if (displayMode != FieldDisplayMode.Write)
        return;
      DefinitionsHelper.CreateBackendFormToolbar(fluentDetailView.Get(), typeof (VideosResources).Name, false, "ThisItem", true, false);
    }

    private static DetailFormViewElement DefineBackendBulkEditView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade = fluentContentView.AddDetailView(viewName).SetTitle("BulkEditDialogTitle").HideTopToolbar().LocalizeUsing<LibrariesResources>().UnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade.Get();
      ContentViewSectionElement viewSectionElement1 = definitionFacade.AddSection("CommonDataSection").SetCssClass("sfBulkEdit sfFirstForm").Get();
      PageFieldElement pageFieldElement1 = new PageFieldElement((ConfigElement) viewSectionElement1.Fields);
      pageFieldElement1.FieldType = typeof (EditMediaContentFolderField);
      pageFieldElement1.ID = "LibraryFieldControl";
      pageFieldElement1.DataFieldName = "Library";
      pageFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      pageFieldElement1.Title = "CommonLibrary";
      pageFieldElement1.CssClass = "sfChangeAlbum";
      pageFieldElement1.ResourceClassId = typeof (VideosResources).Name;
      pageFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      pageFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
      pageFieldElement1.BindOnLoad = new bool?(false);
      pageFieldElement1.SortExpression = "Title ASC";
      PageFieldElement pageFieldElement2 = pageFieldElement1;
      pageFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) pageFieldElement2)
      {
        Expanded = new bool?(false),
        ExpandText = "ChangeLibraryInSpan",
        ResourceClassId = typeof (VideosResources).Name
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
      element3.FieldName = "VideosBulkEditControl";
      element3.TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Videos.BulkEditFieldControl.ascx");
      element3.DisplayMode = FieldDisplayMode.Write;
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.WebServiceUrl = "~/Sitefinity/Services/Content/VideoService.svc/";
      element3.ContentType = typeof (Video);
      element3.ParentType = typeof (VideoLibrary);
      fields.Add((FieldDefinitionElement) element3);
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) detailFormViewElement.Toolbar.Sections)
      {
        Name = "BulkEdit",
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

    internal static ContentViewControlElement DefineFrontendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentFacade = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Video));
      ContentViewControlElement viewControlElement = fluentFacade.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) VideosDefinitions.DefineFrontendMasterThumbnailsListView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) VideosDefinitions.DefineFrontendMasterThumbnailsLightBoxView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) VideosDefinitions.DefineFrontendDetailView((ConfigElement) viewControlElement.ViewsConfig));
      VideosDefinitions.CreateFrontendVideosDialogs((ConfigElementCollection) viewControlElement.DialogsConfig, VideosDefinitions.BackendVideosDefinitionName, fluentFacade);
      return viewControlElement;
    }

    internal static DetailFormViewElement DefineSingleVideoUploadDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      FieldDisplayMode displayMode)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle("UploadVideo").LocalizeUsing<VideosResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/").SetAlternativeTitle("CreateItem").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      definitionFacade2.AddLocalizedTextField("Title").SetId("VideoTitleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").LocalizeUsing<LibrariesResources>().AddValidation().LocalizeUsing<ErrorMessages>().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").SetMaxLength((int) byte.MaxValue).SetMaxLengthViolationMessage("TitleMaxLength").Done().Done();
      if (displayMode == FieldDisplayMode.Write)
      {
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "LibraryFieldControl";
        folderFieldElement1.DataFieldName = "Library";
        folderFieldElement1.ItemName = "VideoItemName";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Library";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (LibrariesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        folderFieldElement1.ShowCreateNewLibraryButton = true;
        folderFieldElement1.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/";
        folderFieldElement1.LibraryTypeName = "Telerik.Sitefinity.Libraries.Model.VideoLibrary";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeLibraryInSpan",
          ResourceClassId = typeof (VideosResources).Name
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

    /// <summary>
    /// Definition of the Choice Field containing the predefined image embed sizes
    /// </summary>
    /// <returns></returns>
    internal static ChoiceFieldElement DefineEmbedVideoSizesChoiceField(
      ConfigElement parent)
    {
      ChoiceFieldElement choiceFieldElement1 = new ChoiceFieldElement(parent);
      choiceFieldElement1.RenderChoiceAs = RenderChoicesAs.RadioButtons;
      choiceFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement1.FieldName = "sizesChoiceField";
      choiceFieldElement1.MutuallyExclusive = true;
      choiceFieldElement1.Title = "ChooseSize1";
      choiceFieldElement1.WrapperTag = HtmlTextWriterTag.Div;
      choiceFieldElement1.ResourceClassId = typeof (ImagesResources).Name;
      ChoiceFieldElement choiceFieldElement2 = choiceFieldElement1;
      foreach (ChoiceDefinition predefinedEmbedVideoSiz in VideosDefinitions.PredefinedEmbedVideoSizes)
        choiceFieldElement2.Choices.Add((IChoiceDefinition) predefinedEmbedVideoSiz);
      return choiceFieldElement2;
    }

    /// <summary>Gets the HTML5 video embed string template.</summary>
    /// <returns></returns>
    internal static string GetHtml5VideoEmbedStringTemplate() => "<video width=\"{0}\" height=\"{1}\" src=\"{2}\" controls=\"true\"></video>";

    private static ContentViewMasterElement DefineFrontendMasterThumbnailsListView(
      ConfigElement parent)
    {
      VideosViewMasterElement viewMasterElement = new VideosViewMasterElement(parent);
      viewMasterElement.ViewName = "VideosFrontendThumbnailsList";
      viewMasterElement.ViewType = typeof (MasterThumbnailView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = VideosDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefineFrontendMasterThumbnailsLightBoxView(
      ConfigElement parent)
    {
      VideosViewMasterLightBoxElement masterLightBoxElement = new VideosViewMasterLightBoxElement(parent);
      masterLightBoxElement.ViewName = "VideosFrontendThumbnailsLightBox";
      masterLightBoxElement.ViewType = typeof (MasterThumbnailLightBoxView);
      masterLightBoxElement.AllowPaging = new bool?(true);
      masterLightBoxElement.DisplayMode = FieldDisplayMode.Read;
      masterLightBoxElement.ItemsPerPage = new int?(50);
      masterLightBoxElement.ResourceClassId = VideosDefinitions.ResourceClassId;
      masterLightBoxElement.FilterExpression = "Visible = true AND Status = Live";
      masterLightBoxElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) masterLightBoxElement;
    }

    private static ContentViewDetailElement DefineFrontendDetailView(
      ConfigElement parent)
    {
      VideosViewDetailElement viewDetailElement = new VideosViewDetailElement(parent);
      viewDetailElement.ViewName = "VideosDetailView";
      viewDetailElement.ViewType = typeof (DetailSimpleView);
      viewDetailElement.DisplayMode = FieldDisplayMode.Read;
      viewDetailElement.ResourceClassId = VideosDefinitions.ResourceClassId;
      return (ContentViewDetailElement) viewDetailElement;
    }

    private static void CreateFrontendVideosDialogs(
      ConfigElementCollection parent,
      string controlDefinitionName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      fluentFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider((ConfigElement) parent), Res.Get<VideosResources>().Video, Res.Get<VideosResources>().Videos, Res.Get<LibrariesResources>().Library, typeof (VideoLibrary), "~/Sitefinity/Services/Content/VideoService.svc/", "~/Sitefinity/Services/Content/VideoLibraryService.svc/", "create").Done().AddEditDialog("VideosBackendEdit", controlDefinitionName, "", "{{ParentId}}", typeof (VideoLibrary)).Done().AddPermissionsDialog(Res.Get<VideosResources>().BackToItems, Res.Get<VideosResources>().PermissionsForVideos, string.Join(",", new string[3]
      {
        "Video",
        "VideoLibrary",
        "VideosSitemapGeneration"
      })).Done().AddHistoryComparisonDialog("VideosBackendVersionComparisonView", controlDefinitionName, "", "", "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddHistoryGridDialog("VideosBackendVersionComparisonView", controlDefinitionName, Res.Get<VideosResources>().BackToItems, "", "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddHistoryPreviewDialog("VideosBackendVersionPreview", controlDefinitionName, Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (VideoLibrary), "editMediaContentProperties").Done().AddPreviewDialog("VideosVersionPreview", controlDefinitionName, "", "{{ParentId}}", typeof (VideoLibrary));
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
      element2.NavigationPageId = LibrariesModule.LibraryVideosPageId;
      element2.RootPageId = LibrariesModule.VideosHomePageId;
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
      ConfigElementList<WidgetElement> items3 = element1.Items;
      SelectionWidgetElement element4 = new SelectionWidgetElement((ConfigElement) element1.Items);
      element4.Name = "SelectionWidget";
      element4.WrapperTagKey = HtmlTextWriterTag.Div;
      element4.WidgetType = typeof (SelectionWidget);
      element4.CssClass = "sfSelectionLinksWrp";
      items3.Add((WidgetElement) element4);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
      menuWidgetElement.Name = "FolderActionsWidget";
      menuWidgetElement.Text = "LibraryActions";
      menuWidgetElement.ResourceClassId = typeof (LibrariesResources).Name;
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      menuWidgetElement.CssClass = "sfAlwaysOn sfLibActionsWrp";
      ActionMenuWidgetElement element5 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "DeleteLibraryActionWidget";
      element6.Text = "DeleteThisLibrary";
      element6.ResourceClassId = typeof (LibrariesResources).Name;
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "deleteLibrary";
      element6.CssClass = "sf_NotForFolder";
      element6.WidgetType = typeof (CommandWidget);
      menuItems1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems2 = element5.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element7.Name = "EditLibraryPropertiesActionWidget";
      element7.Text = "EditProperties";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "editLibraryProperties";
      element7.WidgetType = typeof (CommandWidget);
      element7.CssClass = "sf_NotForFolder";
      menuItems2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems3 = element5.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element8.Name = "LibraryPermissionsActionWidget";
      element8.Text = "SetPermissions";
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "libraryPermissions";
      element8.WidgetType = typeof (CommandWidget);
      element8.CssClass = "sf_NotForFolder";
      menuItems3.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems4 = element5.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element9.Name = "DeleteFolderActionWidget";
      element9.Text = "DeleteThisLibrary";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "deleteFolder";
      element9.CssClass = "sf_ForFolder";
      element9.WidgetType = typeof (CommandWidget);
      menuItems4.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems5 = element5.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element10.Name = "MoveToActionWidget";
      element10.Text = "MoveTo";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "moveToAll";
      element10.WidgetType = typeof (CommandWidget);
      menuItems5.Add((WidgetElement) element10);
      ConfigElementList<WidgetElement> menuItems6 = element5.MenuItems;
      CommandWidgetElement element11 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element11.Name = "EditFolderActionWidget";
      element11.Text = "EditProperties";
      element11.ResourceClassId = typeof (LibrariesResources).Name;
      element11.WrapperTagKey = HtmlTextWriterTag.Li;
      element11.CommandName = "editFolder";
      element11.CssClass = "sf_ForFolder";
      element11.WidgetType = typeof (CommandWidget);
      menuItems6.Add((WidgetElement) element11);
      ConfigElementList<WidgetElement> menuItems7 = element5.MenuItems;
      CommandWidgetElement element12 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element12.Name = "RelocateLibraryActionWidget";
      element12.Text = "RelocateLibrary";
      element12.ResourceClassId = typeof (LibrariesResources).Name;
      element12.WrapperTagKey = HtmlTextWriterTag.Li;
      element12.CommandName = "relocateLibrary";
      element12.WidgetType = typeof (CommandWidget);
      menuItems7.Add((WidgetElement) element12);
      element1.Items.Add((WidgetElement) element5);
      gridView.ContextBarConfig.Sections.Add(element1);
    }

    private static DetailFormViewElement DefineBackendFolderDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle("EditLibrary").HideTopToolbar().LocalizeUsing<LibrariesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/VideoService.svc/folder/").DoNotUseWorkflow().DoNotCreateBlankItem().SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      DetailFormViewElement cfg = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("LibraryName").SetExample("LibraryNameExample").SetCssClass("sfTitleField").LocalizeUsing<VideosResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("LibraryNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("LibraryDescription").SetCssClass("sfFormSeparator").LocalizeUsing<VideosResources>().SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done().Done();
      ContentViewSectionElement viewSectionElement = definitionFacade2.Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      ParentLibraryFieldDefinitionElement element1 = new ParentLibraryFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
      element1.FieldName = "ParentLibraryField";
      element1.DataFieldName = "ParentId";
      element1.FieldType = typeof (ParentLibraryField);
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "VideoItemName";
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
      element3.ResourceClassId = VideosDefinitions.ResourceClassId;
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
