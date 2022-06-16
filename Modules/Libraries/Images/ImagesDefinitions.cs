// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Images.ImagesDefinitions
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
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Config;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
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

namespace Telerik.Sitefinity.Modules.Libraries.Images
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for Images.
  /// </summary>
  public static class ImagesDefinitions
  {
    private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";
    /// <summary>Version Comparison View Name</summary>
    public const string VersionComparisonView = "ImagesBackendVersionComparisonView";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for albums on the backend.
    /// </summary>
    public const string BackendAlbumsDefinitionName = "AlbumsBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for images on the backend.
    /// </summary>
    public const string BackendImagesDefinitionName = "ImagesBackend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for folders on the backend.
    /// </summary>
    public const string BackendImageFoldersDefinitionName = "ImageFoldersBackend";
    /// <summary>
    /// Name of the view used to display albums in a list in the Images module
    /// on the backend.
    /// </summary>
    public const string BackendAlbumsListViewName = "AlbumsBackendList";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the frontend images ContentView control.
    /// </summary>
    public const string FrontendImagesDefinitionName = "ImagesFrontend";
    /// <summary>
    /// Name of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" />
    /// definition for the ContentView control for images in a specific library on the backend.
    /// </summary>
    [Obsolete("Not applicable anymore.")]
    public const string BackendLibraryImagesDefinitionName = "LibraryImagesBackend";
    /// <summary>
    /// Name of the view used to display images in a list in the Images module
    /// on the backend.
    /// </summary>
    public const string BackendListViewName = "ImagesBackendList";
    /// <summary>
    /// Name of the view used to edit image properties in the Images module
    /// on the backend.
    /// </summary>
    public const string BackendEditViewName = "ImagesBackendEdit";
    /// <summary>
    /// Name of the view used keep definitions of  to edit image properties in upload image dialog.
    /// on the backend.
    /// </summary>
    public const string SingleImageUploadDetailsView = "SingleImageUploadDetailsView";
    /// <summary>Name of the bulk edit view in the Images module.</summary>
    public const string BackendBulkEditViewName = "ImagesBackendBulkEdit";
    /// <summary>
    /// Name of the view used to create albums in the Images module
    /// on the backend.
    /// </summary>
    public const string BackendAlbumInsertViewName = "AlbumsBackendInsert";
    /// <summary>
    /// Name of the view used to edit albums in the Images module
    /// on the backend.
    /// </summary>
    public const string BackendAlbumEditViewName = "AlbumsBackendEdit";
    /// <summary>
    /// Name of the view used to display images in the frontend as a thumbnail list.
    /// </summary>
    public const string FrontendMasterThumbnailViewName = "ImagesFrontendThumbnailsListBasic";
    /// <summary>
    /// Id of the view used to display images in the frontend as a thumbnail list.
    /// </summary>
    public const string FrontendMasterThumbnailViewId = "DB0D628C-5471-4197-A94F-000000000001";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display images in the frontend as a thumbnail list.
    /// </summary>
    public const string FrontendMasterThumbnailViewFriendlyName = "List of thumbnails";
    /// <summary>
    /// Name of the view used to display images in the frontend as a thumbnail list with light box.
    /// </summary>
    public const string FrontendMasterThumbnailLightBoxViewName = "ImagesFrontendThumbnailsListLightBox";
    /// <summary>
    /// Id of the view used to display images in the frontend as a thumbnail list with lightbox.
    /// </summary>
    public const string FrontendMasterThumbnailLightBoxViewId = "DB0D628C-5471-4197-A94F-000000000002";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display images in the frontend as a thumbnail list with lightbox.
    /// </summary>
    public const string FrontendMasterThumbnailLightBoxViewFriendlyName = "List of thumbnails and overlay dialog (lightbox)";
    /// <summary>
    /// Name of the view used to display images in the frontend as a tumbnail strip mode
    /// </summary>
    public const string FrontendMasterThumbnailStripViewName = "ImagesFrontendThumbnailsListStrip";
    /// <summary>
    /// If of the view used to display images in the frontend as a thumbnail strip mode.
    /// </summary>
    public const string FrontendMasterThumbnailStripViewId = "DB0D628C-5471-4197-A94F-000000000003";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display images in the frontend as a thumbnail strip mode.
    /// </summary>
    public const string FrontendMasterThumbnailStripViewFriendlyName = "Image and strip of thumbnails";
    /// <summary>
    /// Name of the view used to display images in the frontend as simple thumbnail list.
    /// </summary>
    public const string FrontendMasterThumbnailSimpleViewName = "ImagesFrontendThumbnailsListSimple";
    /// <summary>
    /// Id of the view used to display images in the frontend as a simple thumbnail list
    /// </summary>
    public const string FrontendMasterThumbnailSimpleViewId = "DB0D628C-5471-4197-A94F-000000000004";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display images in the frontend as a smple thumbnail list.
    /// </summary>
    public const string FrontendMasterThumbnailSimpleViewFriendlyName = "List of images in full size";
    /// <summary>
    /// Name of the view used to display images in the frontend in detail mode.
    /// </summary>
    public const string FrontendDetailViewName = "ImagesDetailView";
    /// <summary>
    /// Id of the view used to display images in the frontend in detail mode.
    /// </summary>
    public const string FrontendDetailViewId = "DB0D628C-5471-4197-A94F-000000000005";
    /// <summary>
    /// Friendly name (used in UI) of the view used to display images in the frontend in detail mode.
    /// </summary>
    public const string FrontendDetailViewFriendlyName = "Single image with details";
    /// <summary>The default frontend detail view name.</summary>
    public const string DefaultFrontendDetailViewName = "ImagesDetailView";
    /// <summary>The default frontend master view name;</summary>
    public const string DefaultFrontendMasterViewName = "ImagesFrontendThumbnailsListBasic";
    /// <summary>Definition name for the frontend commments list.</summary>
    public static readonly string FrontendCommentsDefinitionName = "ImagesCommentsFrontend";
    /// <summary>The resource id for images.</summary>
    public static readonly string ResourceClassId = typeof (ImagesResources).Name;
    /// <summary>The view used to do history preview of images</summary>
    public const string BackendVersionPreviewViewName = "ImagesBackendVersionPreview";
    /// <summary>The view used to do preview images.</summary>
    public const string BackendPreviewViewName = "ImagesBackendPreview";
    /// <summary>The view used to edit folders.</summary>
    private const string BackendFolderEditViewName = "BackendFolderEditViewName";
    internal const string DetailsSection = "DetailsSection";
    private static Dictionary<string, string> clientMappedCommnadNames = new Dictionary<string, string>()
    {
      {
        "edit",
        "editMediaContentProperties"
      }
    };
    public const string ImageLibrariesId = "713AEDE8-4201-4FCB-B0B1-F781D40A9663";
    private const string NotForFolderClass = "sf_NotForFolder";
    private const string ForFolderClass = "sf_ForFolder";
    internal const int DefaultMaxFolderFilterPageSizeValue = 100;
    /// <summary>
    /// String template used to build the image embed html code
    /// </summary>
    public const string ImageEmbedStringTemplate = "<img width=\"{0}\" height=\"{1}\" src=\"{2}\" alt=\"{3}\"/>";
    /// <summary>The predefined sizes for the embed functionality</summary>
    internal static readonly List<ChoiceDefinition> PredefinedEmbedImageSizes = new List<ChoiceDefinition>()
    {
      new ChoiceDefinition()
      {
        Text = "300x265",
        Value = "300x265"
      },
      new ChoiceDefinition()
      {
        Text = "425x344",
        Value = "425x344"
      },
      new ChoiceDefinition()
      {
        Text = "480x340",
        Value = "480x340"
      },
      new ChoiceDefinition()
      {
        Text = "640x505",
        Value = "640x505"
      }
    };

    /// <summary>
    /// Defines the ContentView control for Albums on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineAlbumsBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, "AlbumsBackend", typeof (Album));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy("AlbumsBackendList", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendAlbumsListView((ConfigElement) backendContentView.ViewsConfig, "AlbumsBackendList", fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("AlbumsBackendInsert", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendAlbumDetailsView(fluentContentView, "AlbumsBackendInsert", true, false)));
      backendContentView.ViewsConfig.AddLazy("AlbumsBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendAlbumDetailsView(fluentContentView, "AlbumsBackendEdit", false, true)));
      return backendContentView;
    }

    /// <summary>
    /// Defines the ContentView control for Images on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineImagesBackendContentView(
      ConfigElement parent)
    {
      return ImagesDefinitions.DefineBackendContentView(parent, "ImagesBackend");
    }

    /// <summary>
    /// Defines the ContentView control for Folders on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineFoldersBackendContentView(
      ConfigElement parent)
    {
      return ImagesDefinitions.DefineBackendFolderContentView(parent, "ImageFoldersBackend");
    }

    internal static ContentViewControlElement DefineFrontendImagesContentView(
      ConfigElement parent)
    {
      return ImagesDefinitions.DefineFrontendContentView(parent, "ImagesFrontend");
    }

    /// <summary>
    /// Definition of the Choice Field containing the predefined image embed sizes
    /// </summary>
    /// <returns></returns>
    internal static ChoiceFieldElement DefineEmbedImageSizesChoiceField(
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
      foreach (ChoiceDefinition predefinedEmbedImageSiz in ImagesDefinitions.PredefinedEmbedImageSizes)
        choiceFieldElement2.Choices.Add((IChoiceDefinition) predefinedEmbedImageSiz);
      return choiceFieldElement2;
    }

    private static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Telerik.Sitefinity.Libraries.Model.Image));
      ContentViewControlElement backendContentView = fluentContentView.Get();
      backendContentView.ViewsConfig.AddLazy("ImagesBackendList", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendListView((ConfigElement) backendContentView.ViewsConfig, "ImagesBackendList", fluentContentView)));
      backendContentView.ViewsConfig.AddLazy("ImagesBackendEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendDetailsView(fluentContentView, "ImagesBackendEdit")));
      backendContentView.ViewsConfig.AddLazy("ImagesBackendVersionPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendVersionPreviewView(fluentContentView, "ImagesBackendVersionPreview")));
      backendContentView.ViewsConfig.AddLazy("ImagesBackendPreview", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendPreviewView(fluentContentView, "ImagesBackendPreview")));
      backendContentView.ViewsConfig.AddLazy("ImagesBackendBulkEdit", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendBulkEditView(fluentContentView, "ImagesBackendBulkEdit")));
      backendContentView.ViewsConfig.AddLazy("ImagesBackendVersionComparisonView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineVersionComparisonView((ConfigElement) backendContentView.ViewsConfig, "ImagesBackendVersionComparisonView")));
      backendContentView.ViewsConfig.AddLazy("SingleImageUploadDetailsView", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineSingleImageUploadDetailsView(fluentContentView, "SingleImageUploadDetailsView", FieldDisplayMode.Write)));
      return backendContentView;
    }

    private static ContentViewControlElement DefineBackendFolderContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Folder)).SetManagerType(typeof (LibrariesManager));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy("BackendFolderEditViewName", (Func<ContentViewDefinitionElement>) (() => (ContentViewDefinitionElement) ImagesDefinitions.DefineBackendFolderDetailsView(fluentContentView, "BackendFolderEditViewName")));
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
      comparisonViewElement1.ResourceClassId = typeof (ImagesDefinitions).Name;
      comparisonViewElement1.UseWorkflow = new bool?(false);
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      ConfigElementDictionary<string, ComparisonFieldElement> fields1 = comparisonViewElement2.Fields;
      ComparisonFieldElement element1 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element1.FieldName = "MediaUrl";
      fields1.Add(element1);
      ConfigElementDictionary<string, ComparisonFieldElement> fields2 = comparisonViewElement2.Fields;
      ComparisonFieldElement element2 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element2.FieldName = "Title";
      element2.Title = "Title";
      element2.ResourceClassId = typeof (ImagesResources).Name;
      fields2.Add(element2);
      ConfigElementDictionary<string, ComparisonFieldElement> fields3 = comparisonViewElement2.Fields;
      ComparisonFieldElement element3 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element3.FieldName = "AlternativeText";
      element3.Title = "AlternativeText";
      element3.ResourceClassId = typeof (ImagesResources).Name;
      fields3.Add(element3);
      ConfigElementDictionary<string, ComparisonFieldElement> fields4 = comparisonViewElement2.Fields;
      ComparisonFieldElement element4 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element4.FieldName = "Category";
      element4.Title = "Categories";
      element4.ResourceClassId = typeof (TaxonomyResources).Name;
      fields4.Add(element4);
      ConfigElementDictionary<string, ComparisonFieldElement> fields5 = comparisonViewElement2.Fields;
      ComparisonFieldElement element5 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element5.FieldName = "Tags";
      element5.Title = "Tags";
      element5.ResourceClassId = typeof (TaxonomyResources).Name;
      fields5.Add(element5);
      ConfigElementDictionary<string, ComparisonFieldElement> fields6 = comparisonViewElement2.Fields;
      ComparisonFieldElement element6 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element6.FieldName = "Author";
      element6.Title = "Author";
      element6.ResourceClassId = typeof (ImagesResources).Name;
      fields6.Add(element6);
      ConfigElementDictionary<string, ComparisonFieldElement> fields7 = comparisonViewElement2.Fields;
      ComparisonFieldElement element7 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element7.FieldName = "Description";
      element7.Title = "Description";
      element7.ResourceClassId = typeof (ImagesResources).Name;
      fields7.Add(element7);
      ConfigElementDictionary<string, ComparisonFieldElement> fields8 = comparisonViewElement2.Fields;
      ComparisonFieldElement element8 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element8.FieldName = "UrlName";
      element8.Title = "UrlName";
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      fields8.Add(element8);
      ConfigElementDictionary<string, ComparisonFieldElement> fields9 = comparisonViewElement2.Fields;
      ComparisonFieldElement element9 = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
      element9.FieldName = "MediaFileUrlName";
      element9.Title = "MediaFileUrlName";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      fields9.Add(element9);
      return comparisonViewElement2;
    }

    private static MasterGridViewElement DefineBackendAlbumsListView(
      ConfigElement parent,
      string viewName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      Dictionary<string, string> scripts = new Dictionary<string, string>();
      string fullName1 = typeof (ImagesDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      string key3 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js", (object) fullName1);
      scripts.Add(key1, string.Empty);
      scripts.Add(key2, string.Empty);
      scripts.Add(key3, "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade = fluentFacade.AddMasterView(viewName).LocalizeUsing<ImagesResources>().SetTitle("Images").SetCssClass("sfListViewGrid sfLibListView").SetExternalClientScripts(scripts).SetClientMappedCommnadNames(ImagesDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/AlbumService.svc/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadImageWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadImagesButton";
      element2.ResourceClassId = typeof (ImagesResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageImage";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CreateAlbumWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "create";
      element3.Text = "CreateAlbum";
      element3.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element3.WidgetType = typeof (CommandWidget);
      element3.PermissionSet = "Album";
      element3.ActionName = "CreateAlbum";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteImageWidget";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "groupDelete";
      element4.Text = "Delete";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.WidgetType = typeof (CommandWidget);
      element4.CssClass = "sfGroupBtn";
      items3.Add((WidgetElement) element4);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (Album)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Library), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.HeaderText = "Sort";
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = ImagesDefinitions.ResourceClassId;
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
      element7.HeaderText = "Album";
      element7.ResourceClassId = typeof (ImagesResources).Name;
      element7.HeaderCssClass = "sfImgTmb";
      element7.ItemCssClass = "sfImgTmb";
      element7.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_viewItemsByParent sfSmallLibTmb'><span sys:class=\"{{ ((MediaItemsCount == 0) ? 'sfDisplayNone' : 'sfImgFromLib') }}\"><img sys:src='{{ThumbnailUrl}}' sys:alt='{{Title.htmlEncode()}}' /></span></a>";
      columnsConfig1.Add((ColumnElement) element7);
      string end;
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibraryCommonActionsColumn.htm"))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          end = streamReader.ReadToEnd();
      }
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
      element9.ClientTemplate = end.Replace("UploadMediaName", "{$ImagesResources, UploadImages$}");
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
      element11.ClientTemplate = "<i class='sfItemsCount'>{{getDateTemplate(LastUploadedDate, 'dd MMM, yyyy', '{$ImagesResources, LastUploaded$}')}}</i>";
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
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/predecessors/";
      commandWidgetElement2.ClientItemTemplate = "<a sys:href='{{ Url }}'>{{ Title.htmlEncode() }}</a>";
      commandWidgetElement2.ParentDataKeyName = "ParentId";
      commandWidgetElement2.SortExpression = "Title";
      commandWidgetElement2.PageSize = 100;
      DynamicCommandWidgetElement element13 = commandWidgetElement2;
      element13.UrlParameters.Add("provider", "");
      element12.Items.Add((WidgetElement) element13);
      masterGridViewElement.SidebarConfig.Sections.Add(element12);
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
      element15.Text = "FilterImagesBy";
      element15.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element15.WidgetType = typeof (CommandWidget);
      element15.IsSeparator = false;
      items4.Add((WidgetElement) element15);
      masterGridViewElement.SidebarConfig.Sections.Add(element14);
      WidgetBarSectionElement element16 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element16.Items);
        commandWidgetElement3.Name = "ImagesComments";
        commandWidgetElement3.CommandName = "comments";
        commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement3.Text = "CommentsForImages";
        commandWidgetElement3.ResourceClassId = ImagesDefinitions.ResourceClassId;
        commandWidgetElement3.CssClass = "sfComments";
        commandWidgetElement3.WidgetType = typeof (CommandWidget);
        commandWidgetElement3.IsSeparator = false;
        CommandWidgetElement element17 = commandWidgetElement3;
        element16.Items.Add((WidgetElement) element17);
      }
      WidgetBarSectionElement element18 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForImages",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
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
      commandWidgetElement4.Text = "PagesWhereImagesArePublished";
      commandWidgetElement4.ResourceClassId = typeof (ImagesResources).Name;
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
      ConfigElementList<WidgetElement> items7 = element18.Items;
      CommandWidgetElement element22 = new CommandWidgetElement((ConfigElement) element18.Items);
      element22.Name = "ThumbnailSettings";
      element22.CommandName = "thumbnailSettingsPage";
      element22.ButtonType = CommandButtonType.SimpleLinkButton;
      element22.Text = "ThumbnailSettings";
      element22.ResourceClassId = typeof (LibrariesResources).Name;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      items7.Add((WidgetElement) element22);
      if (element16.Items.Count == 0)
        element16.Visible = new bool?(false);
      masterGridViewElement.SidebarConfig.Sections.Add(element16);
      masterGridViewElement.SidebarConfig.Sections.Add(element18);
      masterGridViewElement.SidebarConfig.Title = "ManageImages";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (ImagesResources).Name;
      string parameters = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&commandName={5}&showPrompt={6}", (object) Res.Get<ImagesResources>().Image, (object) Res.Get<ImagesResources>().Images, (object) Res.Get<ImagesResources>().Album, (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, (object) typeof (Album).FullName, (object) "selectLibrary", (object) true);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<ImagesResources>().Image, Res.Get<ImagesResources>().Images, Res.Get<ImagesResources>().Album, typeof (Album), "~/Sitefinity/Services/Content/ImageService.svc/", "~/Sitefinity/Services/Content/AlbumService.svc/", "", typeof (Telerik.Sitefinity.Libraries.Model.Image)).AddParameters("&LibraryId={{Id}}").Done().AddInsertDialog("AlbumsBackendInsert", "", Res.Get<ImagesResources>().BackToAllAlbums).AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("AlbumsBackendEdit", "", Res.Get<ImagesResources>().BackToAllAlbums).Done().AddPermissionsDialog(Res.Get<ImagesResources>().BackToAllAlbums, Res.Get<ImagesResources>().PermissionsForImages, string.Join(",", new string[3]
      {
        "Image",
        "Album",
        "ImagesSitemapGeneration"
      })).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetParameters("?mode=RelocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibraryRelocateDialog>("transferLibrary").SetParameters("?mode=TransferLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<ThumbnailPromptDialog>("regenerateThumbnails").SetParameters("?closeCommand=submit&libraryType=Telerik.Sitefinity.Libraries.Model.Album&messageRes=LibrariesResources&messageKey=ThumbnailRegenerationWarning&messageArg=&buttonRes=LibrariesResources&buttonKey=RegenereateThumbnailsConfirm&buttonArg=").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.None).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().Done().AddDialog<ThumbnailSettingsDialog>("thumbnailSettings").SetParameters("?libraryType=Telerik.Sitefinity.Libraries.Model.Album").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.None).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters).Done().AddCustomFieldsDialog(Res.Get<ImagesResources>().BackToItems, Res.Get<ImagesResources>().ImagesDataFields, Res.Get<ImagesResources>().ImagePluralItemName, typeof (Telerik.Sitefinity.Libraries.Model.Image));
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewImagesByLibrary",
        CommandName = "viewItemsByParent",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryImagesPageId) + "{{Url}}/"
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllImages",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryImagesPageId) + "/?displayMode=allImages"
      });
      string fullName2 = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName;
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullName2
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "thumbnailSettings",
        CommandName = "thumbnailSettingsPage",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.BasicSettingsNodeId) + "/Thumbnails"
      });
      return masterGridViewElement;
    }

    private static MasterGridViewElement DefineBackendListView(
      ConfigElement parent,
      string viewName,
      ContentViewControlDefinitionFacade fluentContentView)
    {
      Dictionary<string, string> scripts = new Dictionary<string, string>();
      string fullName1 = typeof (ImagesDefinitions).Assembly.FullName;
      string key1 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js", (object) fullName1);
      string key2 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js", (object) fullName1);
      string key3 = string.Format("{0}, {1}", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js", (object) fullName1);
      scripts.Add(key1, string.Empty);
      scripts.Add(key2, string.Empty);
      scripts.Add(key3, "OnMasterViewLoaded");
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(viewName).LocalizeUsing<ImagesResources>().SetTitle("ModuleTitle").SetViewType(typeof (LibrariesMasterGridView)).SetParentTitleFormat("AlbumImagesTitleFormat").SetCssClass("sfListViewGrid sfLibListView").SetExternalClientScripts(scripts).SetClientMappedCommnadNames(ImagesDefinitions.clientMappedCommnadNames).SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/").SetSortExpression("Ordinal ASC, LastModified DESC");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      masterGridViewElement.AddResourceString("ImagesResources", "AllImages");
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "UploadImageWidget";
      element2.ButtonType = CommandButtonType.Create;
      element2.CommandName = "uploadFile";
      element2.Text = "UploadImagesButton";
      element2.ResourceClassId = typeof (ImagesResources).Name;
      element2.CssClass = "sfMainAction sfUpload";
      element2.WidgetType = typeof (CommandWidget);
      element2.ActionName = "ManageImage";
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CreateAlbumWidget";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "create";
      element3.Text = "CreateAlbum";
      element3.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element3.WidgetType = typeof (CommandWidget);
      element3.CssClass = "";
      element3.PermissionSet = "Album";
      element3.ActionName = "CreateAlbum";
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteImageWidget";
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
      element5.Text = "ReorderImages";
      element5.ResourceClassId = ImagesDefinitions.ResourceClassId;
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
      element7.Name = "PublishWidget";
      element7.Text = "PublishImages";
      element7.ResourceClassId = typeof (ImagesResources).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "groupPublish";
      element7.WidgetType = typeof (CommandWidget);
      element7.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems2 = element6.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element8.Name = "UnpublishWidget";
      element8.Text = "UnpublishImages";
      element8.ResourceClassId = typeof (ImagesResources).Name;
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "groupUnpublish";
      element8.WidgetType = typeof (CommandWidget);
      element8.CssClass = "sfUnpublishItm";
      menuItems2.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems3 = element6.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element9.Name = "BulkEditWidget";
      element9.Text = "BulkEditImageProperties";
      element9.ResourceClassId = typeof (ImagesResources).Name;
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "bulkEdit";
      element9.WidgetType = typeof (CommandWidget);
      menuItems3.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems4 = element6.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element6.MenuItems);
      element10.Name = "MoveWidget";
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "selectLibrary";
      element10.Text = "MoveToAnotherAlbum";
      element10.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element10.WidgetType = typeof (CommandWidget);
      menuItems4.Add((WidgetElement) element10);
      element1.Items.Add((WidgetElement) element6);
      element1.Items.Add(DefinitionsHelper.CreateSearchButtonWidget((ConfigElement) element1.Items, typeof (Telerik.Sitefinity.Libraries.Model.Image)));
      IEnumerable<SortingExpressionElement> expressionSettings1 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Telerik.Sitefinity.Libraries.Model.Image), false, masterGridViewElement.Section);
      IEnumerable<SortingExpressionElement> expressionSettings2 = DefinitionsHelper.GetSortingExpressionSettings(typeof (Telerik.Sitefinity.Libraries.Model.Image), true, masterGridViewElement.Section);
      DynamicCommandWidgetElement commandWidgetElement1 = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "EditCustomSorting";
      commandWidgetElement1.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement1.HeaderText = "SortImages";
      commandWidgetElement1.PageSize = 10;
      commandWidgetElement1.MoreLinkText = "More";
      commandWidgetElement1.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement1.WidgetType = typeof (SortWidget);
      commandWidgetElement1.ResourceClassId = ImagesDefinitions.ResourceClassId;
      commandWidgetElement1.CssClass = "sfQuickSort";
      commandWidgetElement1.ContentType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
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
        Name = "ImagesMainSection",
        CssClass = "sfFilterBy sfLibFolders sfSeparator",
        WrapperTagId = "mainSection"
      };
      DynamicCommandWidgetElement commandWidgetElement2 = new DynamicCommandWidgetElement((ConfigElement) element17.Items);
      commandWidgetElement2.Name = "FolderFilter";
      commandWidgetElement2.WidgetType = typeof (DynamicCommandWidget);
      commandWidgetElement2.IsSeparator = false;
      commandWidgetElement2.BindTo = BindCommandListTo.HierarchicalData;
      commandWidgetElement2.BaseServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
      commandWidgetElement2.PredecessorServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/predecessors/";
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
      element20.Text = "FilterImagesBy";
      element20.ResourceClassId = ImagesDefinitions.ResourceClassId;
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
      element22.Text = "ImagesByLibrary";
      element22.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element22.WidgetType = typeof (CommandWidget);
      element22.IsSeparator = false;
      items7.Add((WidgetElement) element22);
      masterGridViewElement.SidebarConfig.Sections.Add(element21);
      WidgetBarSectionElement element23 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ByDate",
        Title = "ImagesByDate",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        CssClass = "sfFilterBy sfFilterByDate sfSeparator",
        WrapperTagId = "dateFilterSection",
        Visible = new bool?(false)
      };
      masterGridViewElement.SidebarConfig.Sections.Add(element23);
      WidgetBarSectionElement barSectionElement2 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "FilterOptions",
        Title = "FilterImages",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator sfModules",
        WrapperTagId = "moreFiltersSection"
      };
      ConfigElementList<WidgetElement> items8 = barSectionElement2.Items;
      CommandWidgetElement element24 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element24.Name = "AllImages";
      element24.CommandName = "showAllItems";
      element24.ButtonType = CommandButtonType.SimpleLinkButton;
      element24.ButtonCssClass = "sfSel";
      element24.Text = "AllImages";
      element24.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element24.WidgetType = typeof (CommandWidget);
      element24.IsSeparator = false;
      items8.Add((WidgetElement) element24);
      ConfigElementList<WidgetElement> items9 = barSectionElement2.Items;
      CommandWidgetElement element25 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element25.Name = "MyImages";
      element25.CommandName = "showMyItems";
      element25.ButtonType = CommandButtonType.SimpleLinkButton;
      element25.Text = "MyImages";
      element25.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element25.WidgetType = typeof (CommandWidget);
      element25.IsSeparator = false;
      items9.Add((WidgetElement) element25);
      ConfigElementList<WidgetElement> items10 = barSectionElement2.Items;
      CommandWidgetElement element26 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element26.Name = "ShowDraftImages";
      element26.CommandName = "showMasterItems";
      element26.ButtonType = CommandButtonType.SimpleLinkButton;
      element26.Text = "MasterImages";
      element26.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element26.WidgetType = typeof (CommandWidget);
      element26.IsSeparator = false;
      items10.Add((WidgetElement) element26);
      ConfigElementList<WidgetElement> items11 = barSectionElement2.Items;
      CommandWidgetElement element27 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element27.Name = "ShowPublishedImages";
      element27.CommandName = "showPublishedItems";
      element27.ButtonType = CommandButtonType.SimpleLinkButton;
      element27.Text = "PublishedImages";
      element27.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element27.WidgetType = typeof (CommandWidget);
      element27.IsSeparator = false;
      items11.Add((WidgetElement) element27);
      ConfigElementList<WidgetElement> items12 = barSectionElement2.Items;
      CommandWidgetElement element28 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element28.Name = "PendingApprovalImages";
      element28.CommandName = "showPendingApprovalItems";
      element28.ButtonType = CommandButtonType.SimpleLinkButton;
      element28.Text = "WaitingForApproval";
      element28.ResourceClassId = typeof (LibrariesResources).Name;
      element28.WidgetType = typeof (CommandWidget);
      element28.IsSeparator = false;
      items12.Add((WidgetElement) element28);
      ConfigElementList<WidgetElement> items13 = barSectionElement2.Items;
      CommandWidgetElement element29 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element29.Name = "PendingPublishingImages";
      element29.CommandName = "showPendingPublishingItems";
      element29.ButtonType = CommandButtonType.SimpleLinkButton;
      element29.Text = "WaitingForPublishing";
      element29.ResourceClassId = typeof (LibrariesResources).Name;
      element29.WidgetType = typeof (CommandWidget);
      element29.IsSeparator = false;
      items13.Add((WidgetElement) element29);
      ConfigElementList<WidgetElement> items14 = barSectionElement2.Items;
      CommandWidgetElement element30 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element30.Name = "PendingReviewImages";
      element30.CommandName = "showPendingReviewItems";
      element30.ButtonType = CommandButtonType.SimpleLinkButton;
      element30.Text = "WaitingForReview";
      element30.ResourceClassId = typeof (LibrariesResources).Name;
      element30.WidgetType = typeof (CommandWidget);
      element30.IsSeparator = false;
      items14.Add((WidgetElement) element30);
      ConfigElementList<WidgetElement> items15 = barSectionElement2.Items;
      CommandWidgetElement element31 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      element31.Name = "RejectedImages";
      element31.CommandName = "showRejectedItems";
      element31.ButtonType = CommandButtonType.SimpleLinkButton;
      element31.Text = "Rejected";
      element31.ResourceClassId = typeof (Labels).Name;
      element31.WidgetType = typeof (CommandWidget);
      element31.IsSeparator = false;
      items15.Add((WidgetElement) element31);
      DefinitionsHelper.AppendStatusFilters((ConfigElementCollection) barSectionElement2.Items);
      ConfigElementList<WidgetElement> items16 = barSectionElement2.Items;
      LiteralWidgetElement element32 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element32.Name = "Separator";
      element32.WrapperTagKey = HtmlTextWriterTag.Li;
      element32.WidgetType = typeof (LiteralWidget);
      element32.CssClass = "sfSeparator";
      element32.Text = "&nbsp;";
      element32.IsSeparator = true;
      items16.Add((WidgetElement) element32);
      string[] strArray = new string[3]
      {
        element23.WrapperTagId,
        element17.WrapperTagId,
        element19.WrapperTagId
      };
      string[] taxonomySection = DefinitionsHelper.CreateTaxonomySection<Telerik.Sitefinity.Libraries.Model.Image>(masterGridViewElement.SidebarConfig.Sections, barSectionElement2, "Image", ImagesDefinitions.ResourceClassId, strArray);
      ConfigElementList<WidgetElement> items17 = element23.Items;
      CommandWidgetElement element33 = new CommandWidgetElement((ConfigElement) element23.Items);
      element33.Name = "CloseDateFilter";
      element33.CommandName = "showSectionsExceptAndResetFilter";
      element33.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(taxonomySection);
      element33.ButtonType = CommandButtonType.SimpleLinkButton;
      element33.Text = "CloseDateFilter";
      element33.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element33.CssClass = "sfCloseFilter";
      element33.WidgetType = typeof (CommandWidget);
      element33.IsSeparator = false;
      items17.Add((WidgetElement) element33);
      DateFilteringWidgetDefinitionElement definitionElement = new DateFilteringWidgetDefinitionElement((ConfigElement) element23.Items);
      definitionElement.Name = "DateFilter";
      definitionElement.WidgetType = typeof (DateFilteringWidget);
      definitionElement.IsSeparator = false;
      definitionElement.PropertyNameToFilter = "LastModified";
      DateFilteringWidgetDefinitionElement element34 = definitionElement;
      DefinitionsHelper.GetPredefinedDateFilteringRanges(element34.PredefinedFilteringRanges);
      element23.Items.Add((WidgetElement) element34);
      ConfigElementList<WidgetElement> items18 = barSectionElement2.Items;
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) barSectionElement2.Items);
      commandWidgetElement3.Name = "FilterByDate";
      commandWidgetElement3.CommandName = "hideSectionsExcept";
      commandWidgetElement3.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element23.WrapperTagId);
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "ByDate";
      commandWidgetElement3.ResourceClassId = typeof (LibrariesResources).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element35 = commandWidgetElement3;
      items18.Add((WidgetElement) element35);
      ConfigElementList<WidgetElement> items19 = barSectionElement2.Items;
      LiteralWidgetElement element36 = new LiteralWidgetElement((ConfigElement) barSectionElement2.Items);
      element36.Name = "Separator";
      element36.WrapperTagKey = HtmlTextWriterTag.Li;
      element36.WidgetType = typeof (LiteralWidget);
      element36.CssClass = "sfSeparator";
      element36.Text = "&nbsp;";
      element36.IsSeparator = true;
      items19.Add((WidgetElement) element36);
      masterGridViewElement.SidebarConfig.Sections.Add(barSectionElement2);
      WidgetBarSectionElement element37 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "ManageAlso",
        Title = "ManageAlso",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSeparator",
        WrapperTagId = "manageAlsoSection"
      };
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element37.Items);
        commandWidgetElement4.Name = "ImagesComments";
        commandWidgetElement4.CommandName = "comments";
        commandWidgetElement4.ButtonType = CommandButtonType.SimpleLinkButton;
        commandWidgetElement4.Text = "CommentsForImages";
        commandWidgetElement4.ResourceClassId = ImagesDefinitions.ResourceClassId;
        commandWidgetElement4.CssClass = "sfComments";
        commandWidgetElement4.WidgetType = typeof (CommandWidget);
        commandWidgetElement4.IsSeparator = false;
        CommandWidgetElement element38 = commandWidgetElement4;
        element37.Items.Add((WidgetElement) element38);
      }
      WidgetBarSectionElement element39 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "SettingsForImages",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        CssClass = "sfWidgetsList sfSettings sfSeparator",
        WrapperTagId = "settingsSection"
      };
      ConfigElementList<WidgetElement> items20 = element39.Items;
      CommandWidgetElement element40 = new CommandWidgetElement((ConfigElement) element39.Items);
      element40.Name = "Permissions";
      element40.CommandName = "permissions";
      element40.ButtonType = CommandButtonType.SimpleLinkButton;
      element40.Text = "Permissions";
      element40.ResourceClassId = typeof (LibrariesResources).Name;
      element40.WidgetType = typeof (CommandWidget);
      element40.IsSeparator = false;
      items20.Add((WidgetElement) element40);
      CommandWidgetElement commandWidgetElement5 = new CommandWidgetElement((ConfigElement) element39.Items);
      commandWidgetElement5.Name = "ManageContentLocations";
      commandWidgetElement5.CommandName = "manageContentLocations";
      commandWidgetElement5.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement5.Text = "PagesWhereImagesArePublished";
      commandWidgetElement5.ResourceClassId = typeof (ImagesResources).Name;
      commandWidgetElement5.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element41 = commandWidgetElement5;
      element39.Items.Add((WidgetElement) element41);
      ConfigElementList<WidgetElement> items21 = element39.Items;
      CommandWidgetElement element42 = new CommandWidgetElement((ConfigElement) element39.Items);
      element42.Name = "CustomFields";
      element42.CommandName = "moduleEditor";
      element42.ButtonType = CommandButtonType.SimpleLinkButton;
      element42.Text = "CustomFields";
      element42.ResourceClassId = typeof (LibrariesResources).Name;
      element42.WidgetType = typeof (CommandWidget);
      element42.IsSeparator = false;
      items21.Add((WidgetElement) element42);
      ConfigElementList<WidgetElement> items22 = element39.Items;
      CommandWidgetElement element43 = new CommandWidgetElement((ConfigElement) element39.Items);
      element43.Name = "ThumbnailSettings";
      element43.CommandName = "thumbnailSettingsPage";
      element43.ButtonType = CommandButtonType.SimpleLinkButton;
      element43.Text = "ThumbnailSettings";
      element43.ResourceClassId = typeof (LibrariesResources).Name;
      element43.WidgetType = typeof (CommandWidget);
      element43.IsSeparator = false;
      items22.Add((WidgetElement) element43);
      if (element37.Items.Count == 0)
        element37.Visible = new bool?(false);
      masterGridViewElement.SidebarConfig.Sections.Add(element37);
      masterGridViewElement.SidebarConfig.Sections.Add(element39);
      DefinitionsHelper.CreateRecycleBinLink(masterGridViewElement.SidebarConfig.Sections, typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName);
      masterGridViewElement.SidebarConfig.Title = "ManageImages";
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (ImagesResources).Name;
      LocalizationWidgetBarSectionElement barSectionElement3 = new LocalizationWidgetBarSectionElement((ConfigElement) masterGridViewElement.ContextBarConfig.Sections);
      barSectionElement3.Name = "translations";
      barSectionElement3.WrapperTagKey = HtmlTextWriterTag.Div;
      barSectionElement3.CssClass = "sfContextWidgetWrp";
      barSectionElement3.MinLanguagesCountTreshold = new int?(6);
      LocalizationWidgetBarSectionElement element44 = barSectionElement3;
      ConfigElementList<WidgetElement> items23 = element44.Items;
      CommandWidgetElement element45 = new CommandWidgetElement((ConfigElement) element44.Items);
      element45.Name = "ShowMoreTranslations";
      element45.CommandName = "showMoreTranslations";
      element45.ButtonType = CommandButtonType.SimpleLinkButton;
      element45.Text = "ShowAllTranslations";
      element45.ResourceClassId = typeof (LocalizationResources).Name;
      element45.WidgetType = typeof (CommandWidget);
      element45.IsSeparator = false;
      element45.CssClass = "sfShowHideLangVersions";
      element45.WrapperTagKey = HtmlTextWriterTag.Div;
      items23.Add((WidgetElement) element45);
      ConfigElementList<WidgetElement> items24 = element44.Items;
      CommandWidgetElement element46 = new CommandWidgetElement((ConfigElement) element44.Items);
      element46.Name = "HideMoreTranslations";
      element46.CommandName = "hideMoreTranslations";
      element46.ButtonType = CommandButtonType.SimpleLinkButton;
      element46.Text = "ShowBasicTranslationsOnly";
      element46.ResourceClassId = typeof (LocalizationResources).Name;
      element46.WidgetType = typeof (CommandWidget);
      element46.IsSeparator = false;
      element46.CssClass = "sfDisplayNone sfShowHideLangVersions";
      element46.WrapperTagKey = HtmlTextWriterTag.Div;
      items24.Add((WidgetElement) element46);
      masterGridViewElement.ContextBarConfig.Sections.Add((WidgetBarSectionElement) element44);
      ImagesDefinitions.DefineMasterContextBar(masterGridViewElement);
      DynamicListViewModeElement listViewModeElement = new DynamicListViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      listViewModeElement.Name = "List";
      listViewModeElement.VirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ImagesListItemTemplate.ascx");
      DynamicListViewModeElement element47 = listViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element47);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element48 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element48);
      ConfigElementDictionary<string, ColumnElement> columnsConfig1 = element48.ColumnsConfig;
      DataColumnElement element49 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      element49.Name = "Image";
      element49.HeaderText = "Image";
      element49.ResourceClassId = typeof (ImagesResources).Name;
      element49.HeaderCssClass = "sfImgTmb";
      element49.ItemCssClass = "sfImgTmb";
      element49.ClientTemplate = "<a sys:if='!IsFolder' sys:href='javascript:void(0);' class='sf_binderCommand_editMediaContentProperties sfSmallImgTmb'><img sys:src='{{ThumbnailUrl}}' sys:alt='{{Title.htmlEncode()}}' sys:class=\"{{IsVectorGraphics === true ? 'sfSvgImg' : ''}}\" /></a><a sys:if='IsFolder' sys:href='javascript:void(0);' class='sf_binderCommand_openLibrary sfSmallLibTmb'><span sys:class=\"{{ ((ImagesCount == 0) ? 'sfDisplayNone' : 'sfImgFromLib') }}\"><img sys:src='{{ThumbnailUrl}}' sys:alt='{{Title.htmlEncode()}}' /></span></a>";
      columnsConfig1.Add((ColumnElement) element49);
      ConfigElementDictionary<string, ColumnElement> columnsConfig2 = element48.ColumnsConfig;
      DataColumnElement element50 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      element50.Name = "TitleStatus";
      element50.HeaderText = "TitleStatus";
      element50.ResourceClassId = typeof (ImagesResources).Name;
      element50.HeaderCssClass = "sfTitleCol";
      element50.ItemCssClass = "sfTitleCol";
      element50.ClientTemplate = "<span sys:if='!IsFolder'><strong sys:class=\"{{'sfItemTitle' + (AdditionalStatus ? ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + ' sf' + AdditionalStatus.PrimaryProvider.toLowerCase() + 'SmStatus'  : '')}}\"><a sys:href='javascript:void(0);' class='sf_binderCommand_editMediaContentProperties'>{{Title.htmlEncode()}}</a></strong>\r\n                    <span sys:if='AdditionalStatus' class='sfStatusAdditional'>{{AdditionalStatus.Text}}</span> \r\n                    \r\n                    <span sys:class=\"{{ 'sfPrimaryStatus sf' + UIStatus.toLowerCase()}}\"><span sys:if='AdditionalStatus' class='sfSep'>| </span>{{Status}}</span></span><span sys:if='IsFolder'><strong class='sfItemTitle sfMBottom5'><a sys:href='javascript:void(0);' class='sf_binderCommand_openLibrary'>{{Title.htmlEncode()}}</a></strong>\r\n                    <i sys:if='ImagesCount' class='sfItemsCount'>{{ ImagesCount }}</i><i sys:if='LibrariesCount' class='sfItemsCount'>{{ LibrariesCount }}</i></span>";
      columnsConfig2.Add((ColumnElement) element50);
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) element48.ColumnsConfig);
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
      element48.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element48.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.HeaderText = "Actions";
      menuColumnElement1.HeaderCssClass = "sfRegular";
      menuColumnElement1.ItemCssClass = "sfRegular";
      menuColumnElement1.ResourceClassId = typeof (LibrariesResources).Name;
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      menuColumnElement2.MainAction = DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2, "EmbedImage", HtmlTextWriterTag.Li, "embedMediaContent sf_NotForFolder", "EmbedThisImage", ImagesDefinitions.ResourceClassId);
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "ViewOriginal", HtmlTextWriterTag.Li, "viewOriginalImage sf_NotForFolder", "ViewOriginal", typeof (LibrariesResources).Name, "sfViewImg"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "ViewAllThumbnailSizes", HtmlTextWriterTag.Li, "viewAllThumbnailSizes sf_NotForFolder", "ViewAllThumbnailSizes", typeof (LibrariesResources).Name, "sfViewAllSizes"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", typeof (LibrariesResources).Name, "sfDeleteItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "MoveTo", HtmlTextWriterTag.Li, "moveToSingle", "MoveTo", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Publish", HtmlTextWriterTag.Li, "publish sf_NotForFolder", "Publish", typeof (LibrariesResources).Name, "sfPublishItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Unpublish", HtmlTextWriterTag.Li, "unpublish sf_NotForFolder", "Unpublish", typeof (LibrariesResources).Name, "sfUnpublishItm"));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "Download", HtmlTextWriterTag.Li, "download sf_NotForFolder", "Download", typeof (LibrariesResources).Name, "sfDownloadItm"));
      menuColumnElement2.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuColumnElement2.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator sfSepNoTitle", string.Empty, string.Empty));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "SetAsCover", HtmlTextWriterTag.Li, "setAsCover sf_NotForFolder", "SetAsCoverMenuItemTitle", typeof (LibrariesResources).Name, "sfSetCoverItm"));
      menuColumnElement2.MenuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuColumnElement2.MenuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator sfSepNoTitle sf_NotForFolder", string.Empty, string.Empty));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditProperties", HtmlTextWriterTag.Li, "editMediaContentProperties sf_NotForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "EditFolderProperties", HtmlTextWriterTag.Li, "editFolder sf_ForFolder", "EditProperties", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "RelocateLibrary", HtmlTextWriterTag.Li, "relocateLibrary sf_ForFolder", "RelocateLibrary", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "SetPermissions", HtmlTextWriterTag.Li, "permissions sf_NotForFolder", "SetPermissions", typeof (LibrariesResources).Name));
      menuColumnElement2.MenuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement2.MenuItems, "History", HtmlTextWriterTag.Li, "historygrid sf_NotForFolder", "HistoryMenuItemTitle", typeof (VersionResources).Name));
      element48.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig3 = element48.ColumnsConfig;
      DataColumnElement element51 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      element51.Name = "FileDimSize";
      element51.HeaderText = "FileDimSize";
      element51.ResourceClassId = typeof (LibrariesResources).Name;
      element51.ClientTemplate = "<span sys:if='!IsFolder'><p class='sfLine'>{{Extension ? Extension.substring(1).toUpperCase() : ''}}</p><p sys:if='IsVectorGraphics !== true' class='sfLine'>{{Width}}x{{Height}}</p><p class='sfLine'>{{TotalSize}} KB</p></span>";
      element51.HeaderCssClass = "sfRegular";
      element51.ItemCssClass = "sfRegular";
      columnsConfig3.Add((ColumnElement) element51);
      string str = string.Format("<p class='sfLine'>{0} <a sys:href='{6}'>{1}</a></p><p class='sfLine'>{2} {3}</p><p class='sfLine'>{4} {5}</p>", (object) Res.Get<ImagesResources>().AlbumLabel, (object) "{{FolderTitle ? FolderTitle.htmlEncode() : LibraryTitle.htmlEncode()}}", (object) Res.Get<LibrariesResources>().Categories, (object) "{{CategoryText}}", (object) Res.Get<LibrariesResources>().Tags, (object) "{{TagsText}}", (object) "{{LibraryFullUrl}}");
      ConfigElementDictionary<string, ColumnElement> columnsConfig4 = element48.ColumnsConfig;
      DataColumnElement element52 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      element52.Name = "AlbumCategoriesTags";
      element52.HeaderText = "AlbumCategoriesTags";
      element52.ResourceClassId = typeof (ImagesResources).Name;
      element52.ClientTemplate = "<span sys:if='!IsFolder'>" + str + "</span>";
      element52.HeaderCssClass = "sfMedium";
      element52.ItemCssClass = "sfMedium";
      columnsConfig4.Add((ColumnElement) element52);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      dataColumnElement1.Name = "Owner";
      dataColumnElement1.HeaderText = "Owner";
      dataColumnElement1.ResourceClassId = typeof (Labels).Name;
      dataColumnElement1.ClientTemplate = "<span class='sfLine'>{{Owner ? Owner : ''}}</span>";
      dataColumnElement1.HeaderCssClass = "sfRegular";
      dataColumnElement1.ItemCssClass = "sfRegular";
      DataColumnElement element53 = dataColumnElement1;
      element48.ColumnsConfig.Add((ColumnElement) element53);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element48.ColumnsConfig);
      dataColumnElement2.Name = "Date";
      dataColumnElement2.HeaderText = "Date";
      dataColumnElement2.ResourceClassId = typeof (Labels).Name;
      dataColumnElement2.ClientTemplate = "<span sys:if='!IsFolder'>{{ (LastModified) ? LastModified.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss') : '-' }}</span>";
      dataColumnElement2.HeaderCssClass = "sfDateAndHour";
      dataColumnElement2.ItemCssClass = "sfDateAndHour";
      DataColumnElement element54 = dataColumnElement2;
      element48.ColumnsConfig.Add((ColumnElement) element54);
      DecisionScreenElement element55 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoItemsExist",
        ResourceClassId = ImagesDefinitions.ResourceClassId
      };
      ConfigElementList<CommandWidgetElement> actions1 = element55.Actions;
      CommandWidgetElement element56 = new CommandWidgetElement((ConfigElement) element55.Actions);
      element56.Name = "UploadImages";
      element56.ButtonType = CommandButtonType.Create;
      element56.CommandName = "uploadFile";
      element56.Text = "UploadImagesLabel";
      element56.ResourceClassId = typeof (ImagesResources).Name;
      element56.CssClass = "sfCreateItem";
      element56.ActionName = "ManageImage";
      actions1.Add(element56);
      ConfigElementList<CommandWidgetElement> actions2 = element55.Actions;
      CommandWidgetElement element57 = new CommandWidgetElement((ConfigElement) element55.Actions);
      element57.Name = "CreateAlbum";
      element57.ButtonType = CommandButtonType.Standard;
      element57.CommandName = "create";
      element57.Text = "CreateAlbum";
      element57.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element57.CssClass = "sfCreateAlbum";
      element57.PermissionSet = "Album";
      element57.ActionName = "CreateAlbum";
      actions2.Add(element57);
      ConfigElementList<CommandWidgetElement> actions3 = element55.Actions;
      CommandWidgetElement element58 = new CommandWidgetElement((ConfigElement) element55.Actions);
      element58.Name = "ManageAlbums";
      element58.ButtonType = CommandButtonType.Standard;
      element58.CommandName = "viewLibraries";
      element58.Text = "ManageAlbums";
      element58.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element58.CssClass = "sfCreateAlbum";
      element58.PermissionSet = "Album";
      element58.ActionName = "CreateAlbum";
      actions3.Add(element58);
      masterGridViewElement.DecisionScreensConfig.Add(element55);
      string parameters1 = string.Format("?itemName={0}&itemsName={1}&libraryTypeName={2}&itemType={3}&parentType={4}&commandName={5}&showPrompt={6}", (object) Res.Get<ImagesResources>().Image, (object) Res.Get<ImagesResources>().Images, (object) Res.Get<ImagesResources>().Album, (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, (object) typeof (Album).FullName, (object) "selectLibrary", (object) true);
      string parameters2 = string.Format("?itemName={0}&itemNameWithArticle={1}&itemsName={2}&libraryTypeName={3}&itemType={4}&parentType={5}&folderId=[[folderId]]", (object) Res.Get<ImagesResources>().Image, (object) Res.Get<ImagesResources>().ImageWithArticle, (object) Res.Get<ImagesResources>().Images, (object) Res.Get<ImagesResources>().Album, (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, (object) typeof (Album).FullName);
      definitionFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider(parent), Res.Get<ImagesResources>().Image, Res.Get<ImagesResources>().Images, Res.Get<ImagesResources>().Album, typeof (Album), "~/Sitefinity/Services/Content/ImageService.svc/", "~/Sitefinity/Services/Content/AlbumService.svc/").AddParameters("&folderId={{FolderId}}").Done().AddEditDialog("ImagesBackendEdit", "", "", "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddInsertDialog("AlbumsBackendInsert", "AlbumsBackend").AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("AlbumsBackendEdit", "AlbumsBackend", "", "", (Type) null, "editLibraryProperties").Done().AddEditDialog("ImagesBackendBulkEdit", "", "", "", (Type) null, "bulkEdit").AddParameters("&folderId=[[folderId]]").Done().AddEditDialog("BackendFolderEditViewName", "ImageFoldersBackend", "", "{{RootId}}", typeof (Album), "editFolder").Done().AddDialog<LibrarySelectorDialog>("selectLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().ReloadOnShow().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToSingle").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).ReloadOnShow().MakeModal().SetParameters(parameters1).Done().AddDialog<LibrarySelectorDialog>("moveToAll").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().SetParameters(parameters1).Done().AddDialog<EmbedDialog>("embedMediaContent").SetInitialBehaviors(WindowBehaviors.Close).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(425)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddDialog<ReorderDialog>("reorder").MakeFullScreen().SetParameters(parameters2).Done().AddPermissionsDialog(Res.Get<ImagesResources>().BackToItems, Res.Get<ImagesResources>().PermissionsForImages, string.Join(",", new string[3]
      {
        "Image",
        "Album",
        "ImagesSitemapGeneration"
      })).Done().AddPermissionsDialog(Res.Get<ImagesResources>().BackToImages, Res.Get<ImagesResources>().PermissionsForImages, string.Join(",", new string[2]
      {
        "Image",
        "Album"
      }), "libraryPermissionsDialog", typeof (Album)).Done().AddHistoryComparisonDialog("ImagesBackendVersionComparisonView", "", "", Res.Get<LibrariesResources>().ImagesTitle, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddHistoryGridDialog("ImagesBackendVersionComparisonView", "", Res.Get<ImagesResources>().BackToItems, Res.Get<LibrariesResources>().ImagesTitle, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddHistoryPreviewDialog("ImagesBackendVersionPreview", "", Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddPreviewDialog("ImagesBackendPreview", "", "", "{{ParentId}}", typeof (Album)).Done().AddDialog<LibraryRelocateDialog>("relocateLibrary").SetParameters("?mode=RelocateLibrary").SetInitialBehaviors(WindowBehaviors.None).SetBehaviors(WindowBehaviors.Close).SetAutoSizeBehaviors(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height).DisplayTitleBar().SetWidth(Unit.Pixel(395)).SetHeight(Unit.Pixel(250)).MakeModal().Done().AddCustomFieldsDialog(Res.Get<ImagesResources>().BackToItems, Res.Get<ImagesResources>().ImagesDataFields, Res.Get<ImagesResources>().ImagePluralItemName).Done().AddDialog<ThumbnailListDialog>("viewAllThumbnailSizes").SetParameters("?backLabelText=BackToItems&backLabelResourceClassId=ImagesResources").MakeFullScreen().Done();
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewComments",
        CommandName = "comments",
        NavigateUrl = RouteHelper.CreateNodeReference(CommentsModule.CommentsPageId) + "?threadType=" + typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName
      });
      ConfigElementList<WidgetElement> titleWidgetsConfig = masterGridViewElement.TitleWidgetsConfig;
      CommandWidgetElement element59 = new CommandWidgetElement((ConfigElement) masterGridViewElement.TitleWidgetsConfig);
      element59.Name = "ViewAllAlbumsCommand";
      element59.CommandName = "viewLibraries";
      element59.ButtonType = CommandButtonType.SimpleLinkButton;
      element59.WidgetType = typeof (CommandWidget);
      element59.Text = "AllLibraries";
      element59.ResourceClassId = typeof (LibrariesResources).Name;
      element59.WrapperTagKey = HtmlTextWriterTag.Span;
      titleWidgetsConfig.Add((WidgetElement) element59);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllAlbumsLink",
        CommandName = "viewLibraries",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.ImagesHomePageId)
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "ViewAllImages",
        CommandName = "showAllMediaItems",
        NavigateUrl = RouteHelper.CreateNodeReference(LibrariesModule.LibraryImagesPageId) + "/?displayMode=allImages"
      });
      string fullName2 = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName;
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "manageContentLocations",
        CommandName = "manageContentLocations",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullName2
      });
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "thumbnailSettings",
        CommandName = "thumbnailSettingsPage",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.BasicSettingsNodeId) + "/Thumbnails"
      });
      return masterGridViewElement;
    }

    private static DetailFormViewElement DefineBackendDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailFormViewElement detailView = ImagesDefinitions.DefineBackendDetailsViewCommon(fluentContentView, viewName, FieldDisplayMode.Write, "EditImage");
      DefinitionsHelper.CreateBackendFormToolbar(detailView, typeof (ImagesResources).Name, false, "ThisItem", true, false);
      return detailView;
    }

    private static DetailFormViewElement DefineBackendVersionPreviewView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
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
      DetailFormViewElement detailView = ImagesDefinitions.DefineBackendPreviewDetailsView(fluentContentView, viewName, FieldDisplayMode.Read, "ImageRevisionHistory");
      detailView.ExternalClientScripts = extenalClientScripts;
      detailView.Localization = dictionary;
      detailView.ShowNavigation = new bool?(true);
      detailView.UseWorkflow = new bool?(false);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (LibrariesResources).Name);
      return detailView;
    }

    private static DetailFormViewElement DefineBackendPreviewView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
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
      DetailFormViewElement detailFormViewElement = ImagesDefinitions.DefineBackendPreviewDetailsView(fluentContentView, viewName, FieldDisplayMode.Read, "PreviewImage");
      detailFormViewElement.ExternalClientScripts = extenalClientScripts;
      detailFormViewElement.Localization = dictionary;
      detailFormViewElement.ShowNavigation = new bool?(true);
      detailFormViewElement.DisplayMode = FieldDisplayMode.Read;
      detailFormViewElement.UseWorkflow = new bool?(false);
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineBackendDetailsViewCommon(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      FieldDisplayMode displayMode,
      string titleResourceKey)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibrariesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(titleResourceKey).LocalizeUsing<ImagesResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/").SetAlternativeTitle("CreateImage").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      if (detailFormViewElement.ViewName == "ImagesBackendEdit")
        definitionFacade1.AddSection("toolbarSection").AddLanguageListField();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("ImageTitleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").LocalizeUsing<LibrariesResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      definitionFacade2.AddLanguageChoiceField("AvailableLanguages").SetDisplayMode(displayMode).Done();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
      FileFieldDefinitionElement element1 = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      element1.ID = "ImageFieldControl";
      element1.DataFieldName = "MediaUrl";
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      element1.CssClass = "";
      element1.WrapperTag = HtmlTextWriterTag.Li;
      element1.FieldType = typeof (ImageUploadField);
      element1.LibraryContentType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
      element1.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element1.ItemName = "Image";
      element1.ItemNamePlural = "Images";
      element1.IsMultiselect = false;
      element1.MaxFileCount = 1;
      fields.Add((FieldDefinitionElement) element1);
      if (displayMode == FieldDisplayMode.Write)
      {
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "AlbumFieldControl";
        folderFieldElement1.DataFieldName = "Album";
        folderFieldElement1.ItemName = "Image";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Album";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (ImagesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeAlbumInSpan",
          ResourceClassId = typeof (ImagesResources).Name
        };
        viewSectionElement1.Fields.Add((FieldDefinitionElement) folderFieldElement2);
      }
      definitionFacade2.AddLocalizedTextField("AlternativeText").SetId("AlternativeTextFieldControl").SetTitle("AlternativeText").SetCssClass("sfFormSeparator").AddExpandableBehavior().Collapse().SetExpandText("ClickToAddAlternativeText").Done();
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddExpandableSection("TaxonomiesSection").SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element2 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      FlatTaxonFieldDefinitionElement element3 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element3.CssClass = "sfFormSeparator";
      element3.Description = "TagsFieldInstructions";
      element3.DisplayMode = new FieldDisplayMode?(displayMode);
      element3.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element3);
      definitionFacade1.AddExpandableSection("DetailsSection").SetTitle("Details").LocalizeUsing<LibrariesResources>().AddLocalizedTextField("Author").SetId("AuthorFieldControl").SetTitle("Author").Done().AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").LocalizeUsing<ImagesResources>().SetRows(5).Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("AdvancedSection").SetTitle("ImageAdvancedSection").LocalizeUsing<LibrariesResources>();
      definitionFacade4.AddMirrorTextField("UrlName").SetTitle("UrlName").SetId("urlName").SetFieldType(typeof (ContentUrlField)).SetMirroredControlId(definitionFacade3.Get().ID).SetRegularExpressionFilter(DefinitionsHelper.UrlRegularExpressionFilterForLibraries).AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForLibrariesContentValidator).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").Done().SetLocalizableDataFieldName("UrlName");
      string str1 = "$Context.AllowMultipleUrls";
      string dataFieldName1 = "$Context.AdditionalUrlNames";
      string fieldName1 = "$Context.AdditionalUrlsRedirectToDefault";
      string dataFieldName2 = "$Context.DefaultUrl";
      definitionFacade4.AddExpandableField("multipleUrlsExpandableField").SetId("multipleUrlsExpandableField").SetDataFieldName(str1).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(str1, "AllowMultipleURLsForThisItem", false).SetId("allowMultipleUrlsFieldElement").Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(dataFieldName1).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(fieldName1, "AllAditionalUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultUrlNameTextField").SetDataFieldName(dataFieldName2).SetId("defaultUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
      definitionFacade4.AddMirrorTextField("MediaFileUrlName").SetTitle("MediaFileUrlName").SetDataFieldName("MediaFileUrlName").SetMirroredControlId(definitionFacade3.Get().ID).SetId("mediaFilеUrlName").SetFieldType(typeof (ContentUrlField)).SetLocalizableDataFieldName("MediaFileUrlName");
      string str2 = "$SfAdditionalInfo.AllowMultipleFileUrls";
      string dataFieldName3 = "$SfAdditionalInfo.FileAdditionalUrlsKey";
      string fieldName2 = "$SfAdditionalInfo.RedirectToDefault";
      string dataFieldName4 = "$SfAdditionalInfo.DefaultFileUrl";
      definitionFacade4.AddExpandableField("multipleFileUrlsExpandableField").SetId("multipleFileUrlsExpandableField").SetDataFieldName(str2).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(str2, "AllowMultipleFileURLsForThisItem", false).SetId("allowMultipleFileUrlsFieldElement").Done().AddTextField("multipleFileUrlsField").SetId("multipleFileUrlsField").SetDataFieldName(dataFieldName3).SetFieldType<MultilineTextField>().SetTitle("AdditionalFileUrls").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(fieldName2, "AllAditionalFileUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultFileUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultFileUrlNameTextField").SetDataFieldName(dataFieldName4).SetId("defaultFileUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade5 = definitionFacade1.AddReadOnlySection("Sidebar");
      ContentViewSectionElement viewSectionElement3 = definitionFacade5.Get();
      if (displayMode == FieldDisplayMode.Read)
        definitionFacade5.AddVersionNoteControl();
      if (displayMode == FieldDisplayMode.Write)
      {
        definitionFacade5.AddWorkflowStatusInfoField();
        definitionFacade5.AddContentLocationInfoField();
      }
      definitionFacade5.AddTextField("Width").SetId("WidthFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Width").SetCssClass("sfConstantField").SetUnit("px").SetReadOnlyReplacement("{ \"Value\": \"0\", \"Text\": \"Scalable\" }").Done().AddTextField("Height").SetId("HeightFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Height").SetCssClass("sfConstantField").SetUnit("px").SetReadOnlyReplacement("{ \"Value\": \"0\", \"Text\": \"Scalable\" }").Done().AddTextField("TotalSize").SetId("SizeFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Size").SetDescription("bytes").SetCssClass("sfConstantField").LocalizeUsing<LibrariesResources>().Done();
      EmbedControlDefinitionElement definitionElement1 = new EmbedControlDefinitionElement((ConfigElement) viewSectionElement3.Fields);
      definitionElement1.DisplayMode = new FieldDisplayMode?(displayMode);
      definitionElement1.FieldName = "EmbedControl";
      definitionElement1.ResourceClassId = typeof (LibrariesResources).Name;
      definitionElement1.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement1.FieldType = typeof (EmbedControl);
      definitionElement1.EmbedStringTemplate = "<img width=\"{0}\" height=\"{1}\" src=\"{2}\" alt=\"{3}\"/>";
      definitionElement1.CustomizeButtonTitle = Res.Get<ImagesResources>().Custom;
      EmbedControlDefinitionElement definitionElement2 = definitionElement1;
      definitionElement2.SizesChoiceFieldDefinition = ImagesDefinitions.DefineEmbedImageSizesChoiceField((ConfigElement) definitionElement2);
      viewSectionElement3.Fields.Add((FieldDefinitionElement) definitionElement2);
      if (displayMode != FieldDisplayMode.Write)
        return detailFormViewElement;
      definitionFacade5.AddRelatingDataField();
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineBackendPreviewDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      FieldDisplayMode displayMode,
      string titleResourceKey)
    {
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibrariesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(titleResourceKey).LocalizeUsing<ImagesResources>().SetExternalClientScripts(extenalClientScripts).SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/").SetAlternativeTitle("CreateImage").SupportMultilingual().SetDisplayMode(displayMode);
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("ImageTitleFieldControl").SetTitle(string.Empty).SetCssClass("sfTitleField sfMBottom25").LocalizeUsing<LibrariesResources>().AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
      FileFieldDefinitionElement element1 = new FileFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      element1.ID = "ImageFieldControl";
      element1.DataFieldName = "MediaUrl";
      element1.CssClass = "";
      element1.WrapperTag = HtmlTextWriterTag.Li;
      element1.FieldType = typeof (ImageUploadField);
      element1.LibraryContentType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
      element1.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element1.ItemName = "Image";
      element1.ItemNamePlural = "Images";
      element1.IsMultiselect = false;
      element1.MaxFileCount = 1;
      fields.Add((FieldDefinitionElement) element1);
      definitionFacade2.AddLocalizedTextField("AlternativeText").SetId("AlternativeTextFieldControl").SetTitle("AlternativeText").SetCssClass("sfFormSeparator").AddExpandableBehavior().Collapse().SetExpandText("ClickToAddAlternativeText").Done();
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddExpandableSection("TaxonomiesSection").SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element2 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      FlatTaxonFieldDefinitionElement element3 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element3.CssClass = "sfFormSeparator";
      element3.Description = "TagsFieldInstructions";
      element3.DisplayMode = new FieldDisplayMode?(displayMode);
      element3.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element3);
      definitionFacade1.AddExpandableSection("DetailsSection").SetTitle("Details").LocalizeUsing<LibrariesResources>().AddLocalizedTextField("Author").SetId("AuthorFieldControl").SetTitle("Author").Done().AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").LocalizeUsing<ImagesResources>().SetRows(5).Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("AdvancedSection").SetTitle("ImageAdvancedSection").LocalizeUsing<LibrariesResources>();
      definitionFacade4.AddMirrorTextField("UrlName").SetTitle("UrlName").SetId("urlName").SetFieldType(typeof (ContentUrlField)).SetMirroredControlId(definitionFacade3.Get().ID).SetRegularExpressionFilter(DefinitionsHelper.UrlRegularExpressionFilterForLibraries).AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForLibrariesContentValidator).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").Done().SetLocalizableDataFieldName("UrlName");
      string dataFieldName1 = "$Context.AllowMultipleUrls";
      string dataFieldName2 = "$Context.AdditionalUrlNames";
      string dataFieldName3 = "$Context.AdditionalUrlsRedirectToDefault";
      definitionFacade4.AddSingleCheckboxField("multipleUrlsExpandableField", "AllowMultipleURLsForThisItem").SetId("multipleUrlsExpandableField").SetDataFieldName(dataFieldName1).SetTitle("AllowMultipleURLsForThisItem").LocalizeUsing<ContentResources>().Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(dataFieldName2).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetCssClass("sfTxtOnePerLine").LocalizeUsing<ContentResources>().SetRows(5).Done().AddSingleCheckboxField("additionalUrlsRedirectToDefaultFieldName", "AllAditionalUrlsRedirectoToTheDefaultOne").SetId("redirectToDefaultUrlChoiceFieldDefinition").SetDataFieldName(dataFieldName3).SetTitle("AllAditionalUrlsRedirectoToTheDefaultOne").LocalizeUsing<ContentResources>().Done();
      string dataFieldName4 = "$SfAdditionalInfo.AllowMultipleFileUrls";
      string dataFieldName5 = "$SfAdditionalInfo.FileAdditionalUrlsKey";
      string dataFieldName6 = "$SfAdditionalInfo.RedirectToDefault";
      definitionFacade4.AddSingleCheckboxField("multipleFileUrlsExpandableField", "AllowMultipleFileURLsForThisItem").SetId("multipleFileUrlsExpandableField").SetDataFieldName(dataFieldName4).SetTitle("AllowMultipleFileURLsForThisItem").LocalizeUsing<ContentResources>().Done().AddTextField("multipleFileUrlsField").SetId("multipleFileUrlsField").SetDataFieldName(dataFieldName5).SetFieldType<MultilineTextField>().SetTitle("AdditionalFileUrls").SetCssClass("sfTxtOnePerLine").LocalizeUsing<ContentResources>().SetRows(5).Done().AddSingleCheckboxField("additionalFileUrlsRedirectToDefaultFieldName", "AllAditionalFileUrlsRedirectoToTheDefaultOne").SetId("redirectToDefaultFileUrlChoiceFieldDefinition").SetDataFieldName(dataFieldName6).SetTitle("AllAditionalFileUrlsRedirectoToTheDefaultOne").LocalizeUsing<ContentResources>().Done();
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade5 = definitionFacade1.AddReadOnlySection("Sidebar");
      definitionFacade5.Get();
      definitionFacade5.AddVersionNoteControl();
      definitionFacade5.AddTextField("Width").SetId("WidthFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Width").SetCssClass("sfConstantField").SetUnit("px").SetReadOnlyReplacement("{ \"Value\": \"0\", \"Text\": \"Scalable\" }").Done().AddTextField("Height").SetId("HeightFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Height").SetCssClass("sfConstantField").SetUnit("px").SetReadOnlyReplacement("{ \"Value\": \"0\", \"Text\": \"Scalable\" }").Done().AddTextField("TotalSize").SetId("SizeFieldControl").SetDisplayMode(FieldDisplayMode.Read).SetTitle("Size").SetDescription("bytes").SetCssClass("sfConstantField").LocalizeUsing<LibrariesResources>().Done();
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineSingleImageUploadDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      FieldDisplayMode displayMode)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle("UploadImage").LocalizeUsing<ImagesResources>().SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/").SetAlternativeTitle("CreateImage").SupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      if (displayMode == FieldDisplayMode.Write)
      {
        FolderFieldElement folderFieldElement1 = new FolderFieldElement((ConfigElement) viewSectionElement1.Fields);
        folderFieldElement1.FieldType = typeof (EditMediaContentFolderField);
        folderFieldElement1.ID = "AlbumFieldControl";
        folderFieldElement1.DataFieldName = "Album";
        folderFieldElement1.ItemName = "Image";
        folderFieldElement1.DisplayMode = new FieldDisplayMode?(displayMode);
        folderFieldElement1.Title = "Album";
        folderFieldElement1.CssClass = "sfFormSeparator sfChangeAlbum";
        folderFieldElement1.ResourceClassId = typeof (ImagesResources).Name;
        folderFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
        folderFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
        folderFieldElement1.BindOnLoad = new bool?(false);
        folderFieldElement1.SortExpression = "Title ASC";
        folderFieldElement1.ShowCreateNewLibraryButton = true;
        folderFieldElement1.CreateLibraryServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/";
        folderFieldElement1.LibraryTypeName = "Telerik.Sitefinity.Libraries.Model.Album";
        FolderFieldElement folderFieldElement2 = folderFieldElement1;
        folderFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) folderFieldElement2)
        {
          Expanded = new bool?(false),
          ExpandText = "ChangeAlbumInSpan",
          ResourceClassId = typeof (ImagesResources).Name
        };
        viewSectionElement1.Fields.Add((FieldDefinitionElement) folderFieldElement2);
      }
      definitionFacade2.AddLocalizedTextField("Title").SetId("ImageTitleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").LocalizeUsing<LibrariesResources>().AddValidation().LocalizeUsing<ErrorMessages>().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").SetMaxLength((int) byte.MaxValue).SetMaxLengthViolationMessage("TitleMaxLength").Done().Done().AddLocalizedTextField("AlternativeText").SetId("AlternativeTextFieldControl").SetTitle("AlternativeText").SetCssClass("sfFormSeparator").AddValidation().LocalizeUsing<ErrorMessages>().SetMaxLength((int) byte.MaxValue).SetMaxLengthViolationMessage("AltTextMaxLength").Done().Done();
      ContentViewSectionElement viewSectionElement2 = definitionFacade1.AddExpandableSection("TaxonomiesSection").AddExpandableBehavior().SetExpandText("Categories and tags").Collapse().Done().SetTitle("CategoriesAndTags").LocalizeUsing<LibrariesResources>().Get();
      HierarchicalTaxonFieldDefinitionElement element1 = DefinitionTemplates.CategoriesFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element1.DisplayMode = new FieldDisplayMode?(displayMode);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element1);
      FlatTaxonFieldDefinitionElement element2 = DefinitionTemplates.TagsFieldWriteMode((ConfigElement) viewSectionElement2.Fields);
      element2.CssClass = "sfFormSeparator";
      element2.Description = "TagsFieldInstructions";
      element2.DisplayMode = new FieldDisplayMode?(displayMode);
      element2.ExpandableDefinition.Expanded = new bool?(true);
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      return detailFormViewElement;
    }

    private static DetailFormViewElement DefineBackendBulkEditView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName)
    {
      DetailViewDefinitionFacade definitionFacade = fluentContentView.AddDetailView(viewName).SetTitle("BulkEditDialogTitle").HideTopToolbar().LocalizeUsing<LibrariesResources>().UnlockDetailItemOnExit().DoNotUseWorkflow().SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/").DoNotSupportMultilingual();
      DetailFormViewElement detailFormViewElement = definitionFacade.Get();
      ContentViewSectionElement viewSectionElement1 = definitionFacade.AddSection("CommonDataSection").SetCssClass("sfBulkEdit sfFirstForm").Get();
      PageFieldElement pageFieldElement1 = new PageFieldElement((ConfigElement) viewSectionElement1.Fields);
      pageFieldElement1.FieldType = typeof (EditMediaContentFolderField);
      pageFieldElement1.ID = "AlbumFieldControl";
      pageFieldElement1.DataFieldName = "Album";
      pageFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      pageFieldElement1.Title = "CommonAlbum";
      pageFieldElement1.CssClass = "sfChangeAlbum";
      pageFieldElement1.ResourceClassId = typeof (ImagesResources).Name;
      pageFieldElement1.WrapperTag = HtmlTextWriterTag.Li;
      pageFieldElement1.WebServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
      pageFieldElement1.BindOnLoad = new bool?(false);
      pageFieldElement1.SortExpression = "Title ASC";
      PageFieldElement pageFieldElement2 = pageFieldElement1;
      pageFieldElement2.ExpandableDefinitionConfig = new ExpandableControlElement((ConfigElement) pageFieldElement2)
      {
        Expanded = new bool?(false),
        ExpandText = "ChangeAlbumInSpan",
        ResourceClassId = typeof (ImagesResources).Name
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
      element3.FieldName = "ImagesBulkEditControl";
      element3.TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Images.BulkEditFieldControl.ascx");
      element3.DisplayMode = FieldDisplayMode.Write;
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.WebServiceUrl = "~/Sitefinity/Services/Content/ImageService.svc/";
      element3.ContentType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
      element3.ParentType = typeof (Album);
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

    private static DetailFormViewElement DefineBackendAlbumDetailsView(
      ContentViewControlDefinitionFacade fluentContentView,
      string viewName,
      bool isCreateMode,
      bool showDeleteButton)
    {
      DetailViewDefinitionFacade definitionFacade1 = fluentContentView.AddDetailView(viewName).SetTitle(isCreateMode ? "CreateAlbum" : "EditAlbum").HideTopToolbar().LocalizeUsing<ImagesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/AlbumService.svc/").DoNotUseWorkflow().DoNotSupportMultilingual().SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      DetailFormViewElement cfg = definitionFacade1.Get();
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade3 = definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("AlbumName").SetExample("AlbumNameExample").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("AlbumNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("AlbumDescription").SetCssClass("sfFormSeparator").SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done();
      if (isCreateMode)
        definitionFacade2.LocalizeUsing<LibrariesResources>().AddLocalizedUrlNameField(definitionFacade3.Get().ID);
      else
        definitionFacade2.AddTextField("UrlName").LocalizeUsing<LibrariesResources>().SetTitle("UrlName").SetDisplayMode(FieldDisplayMode.Read).SetId("urlName").Done();
      ContentViewSectionElement viewSectionElement1 = definitionFacade2.Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement1.Fields;
      ParentLibraryFieldDefinitionElement element1 = new ParentLibraryFieldDefinitionElement((ConfigElement) viewSectionElement1.Fields);
      element1.FieldName = "ParentLibraryField";
      element1.DataFieldName = "ParentId";
      element1.FieldType = typeof (ParentLibraryField);
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "ImageItemName";
      fields.Add((FieldDefinitionElement) element1);
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade4 = definitionFacade1.AddExpandableSection("RootLibrarySection").SetTitle("RootLibrarySettings").LocalizeUsing<LibrariesResources>();
      definitionFacade4.AddChoiceField("ResizeOnUpload", RenderChoicesAs.RadioButtons).LocalizeUsing<ImagesResources>().SetId("ResizingOptionsFieldControl").SetDisplayMode(FieldDisplayMode.Write).SetTitle("ResizeImagesOnUpload").AddChoiceAndContinue("UploadOriginalImages", "false").AddChoiceAndContinue("ResizeImages", "true").Done();
      definitionFacade4.AddChoiceField("NewSize", RenderChoicesAs.DropDown).LocalizeUsing<ImagesResources>().SetId("SelectSizeFieldControl").SetDisplayMode(FieldDisplayMode.Write).AddChoiceAndContinue("SelectSize", "0").AddChoiceAndContinue("Thumbnail", "100").AddChoiceAndContinue("Small", "240").AddChoiceAndContinue("Medium", "500").AddChoiceAndContinue("Large", "800").AddChoiceAndContinue("ExtraLarge", "1024").Done();
      ContentViewSectionElement viewSectionElement2 = definitionFacade4.Get();
      viewSectionElement2.Fields.Add((FieldDefinitionElement) LibrariesDefinitions.DefineClientCacheProfileField((ConfigElement) viewSectionElement2.Fields));
      ThumbnailProfileFieldDefinitionElement definitionElement = new ThumbnailProfileFieldDefinitionElement((ConfigElement) viewSectionElement2.Fields);
      definitionElement.DataFieldName = "ThumbnailProfiles";
      definitionElement.ResourceClassId = typeof (LibrariesResources).Name;
      definitionElement.Title = "ThumbnailsOfImages";
      definitionElement.LibraryType = typeof (Album).FullName;
      definitionElement.ThumbnailSettingsServiceUrl = string.Format("~/{0}/thumbnail-profiles/", (object) "Sitefinity/Services/ThumbnailService.svc");
      definitionElement.DisplayMode = new FieldDisplayMode?(isCreateMode ? FieldDisplayMode.Write : FieldDisplayMode.Read);
      ThumbnailProfileFieldDefinitionElement element2 = definitionElement;
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element2);
      definitionFacade4.AddTextField("MaxSize").SetId("MaxLibrarySizeFieldControl").SetTitle("MaxAlbumSize").SetDescription("Mb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression("\\d").SetMessageCssClass("sfError").Done().Done().AddTextField("MaxItemSize").SetId("MaxImageSizeFieldControl").SetTitle("MaxImageSize").SetDescription("Kb").SetCssClass("sfShortField40 sfConstantField").SetHideIfValue("0").Done();
      BlobStorageChoiceFieldElement choiceFieldElement = new BlobStorageChoiceFieldElement((ConfigElement) viewSectionElement2.Fields);
      choiceFieldElement.Title = "StorageProvider";
      choiceFieldElement.ResourceClassId = typeof (LibrariesResources).Name;
      choiceFieldElement.DisplayMode = new FieldDisplayMode?(isCreateMode ? FieldDisplayMode.Write : FieldDisplayMode.Read);
      choiceFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      choiceFieldElement.ID = "blobStorageChoiceFieldElement";
      choiceFieldElement.FieldName = "blobStorageField";
      choiceFieldElement.DataFieldName = "BlobStorageProvider";
      BlobStorageChoiceFieldElement element3 = choiceFieldElement;
      viewSectionElement2.Fields.Add((FieldDefinitionElement) element3);
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) cfg.Toolbar.Sections)
      {
        Name = "toolbar",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      if (isCreateMode)
      {
        ConfigElementList<WidgetElement> items = element4.Items;
        CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.Items);
        element5.Name = "CreateAndUploadWidgetElement";
        element5.ButtonType = CommandButtonType.Save;
        element5.CommandName = "createAndUpload";
        element5.Text = "CreateAndGoToUploadImages";
        element5.ResourceClassId = ImagesDefinitions.ResourceClassId;
        element5.WrapperTagKey = HtmlTextWriterTag.Span;
        element5.WidgetType = typeof (CommandWidget);
        items.Add((WidgetElement) element5);
      }
      ConfigElementList<WidgetElement> items1 = element4.Items;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.Items);
      element6.Name = "SaveChangesWidgetElement";
      element6.ButtonType = CommandButtonType.Standard;
      element6.CommandName = "save";
      element6.Text = isCreateMode ? "CreateThisAlbum" : "SaveChanges";
      element6.ResourceClassId = ImagesDefinitions.ResourceClassId;
      element6.WrapperTagKey = HtmlTextWriterTag.Span;
      element6.WidgetType = typeof (CommandWidget);
      element6.CssClass = isCreateMode ? "" : "sfSave";
      items1.Add((WidgetElement) element6);
      int num = isCreateMode ? 1 : 0;
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element4.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActions";
        menuWidgetElement.ResourceClassId = typeof (LibrariesResources).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element7 = menuWidgetElement;
        if (showDeleteButton)
        {
          ConfigElementList<WidgetElement> menuItems = element7.MenuItems;
          CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
          element8.Name = "DeleteCommandName";
          element8.ButtonType = CommandButtonType.SimpleLinkButton;
          element8.Text = "DeleteThisItem";
          element8.CommandName = "delete";
          element8.ResourceClassId = typeof (LibrariesResources).Name;
          element8.WidgetType = typeof (CommandWidget);
          menuItems.Add((WidgetElement) element8);
        }
        ConfigElementList<WidgetElement> menuItems1 = element7.MenuItems;
        CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element7.MenuItems);
        element9.Name = "PermissionsCommandName";
        element9.ButtonType = CommandButtonType.SimpleLinkButton;
        element9.Text = "SetPermissions";
        element9.CommandName = "permissions";
        element9.ResourceClassId = typeof (LibrariesResources).Name;
        element9.WidgetType = typeof (CommandWidget);
        menuItems1.Add((WidgetElement) element9);
        element4.Items.Add((WidgetElement) element7);
      }
      ConfigElementList<WidgetElement> items2 = element4.Items;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element4.Items);
      element10.Name = "CancelWidgetElement";
      element10.ButtonType = CommandButtonType.Cancel;
      element10.CommandName = "cancel";
      element10.Text = "Cancel";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Span;
      element10.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element10);
      cfg.Toolbar.Sections.Add(element4);
      cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveToRootLibraryWarning");
      cfg.AddResourceString(typeof (LibrariesResources).Name, "MoveLibraryWarning");
      if (!isCreateMode)
      {
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

    private static ContentViewControlElement DefineFrontendContentView(
      ConfigElement parent,
      string controlDefinitionName)
    {
      ContentViewControlDefinitionFacade fluentFacade = App.WorkWith().Module("Libraries").DefineContainer(parent, controlDefinitionName, typeof (Telerik.Sitefinity.Libraries.Model.Image));
      ContentViewControlElement viewControlElement = fluentFacade.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) ImagesDefinitions.DefineFrontendMasterThumbnailView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) ImagesDefinitions.DefineFrontendMasterThumbnailLightBoxView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) ImagesDefinitions.DefineFrontendMasterThumbnailSimpleView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) ImagesDefinitions.DefineFrontendMasterThumbnailStripView((ConfigElement) viewControlElement.ViewsConfig));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) ImagesDefinitions.DefineFrontendDetailView((ConfigElement) viewControlElement.ViewsConfig));
      ImagesDefinitions.CreateFrontendImagesDialogs((ConfigElementCollection) viewControlElement.DialogsConfig, "ImagesBackend", fluentFacade);
      return viewControlElement;
    }

    private static ContentViewMasterElement DefineFrontendMasterThumbnailView(
      ConfigElement parent)
    {
      ImagesViewMasterElement viewMasterElement = new ImagesViewMasterElement(parent);
      viewMasterElement.ViewName = "ImagesFrontendThumbnailsListBasic";
      viewMasterElement.ViewType = typeof (MasterThumbnailView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = ImagesDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefineFrontendMasterThumbnailLightBoxView(
      ConfigElement parent)
    {
      ImagesViewMasterElement viewMasterElement = new ImagesViewMasterElement(parent);
      viewMasterElement.ViewName = "ImagesFrontendThumbnailsListLightBox";
      viewMasterElement.ViewType = typeof (MasterThumbnailLightBoxView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = ImagesDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefineFrontendMasterThumbnailSimpleView(
      ConfigElement parent)
    {
      ImagesViewMasterElement viewMasterElement = new ImagesViewMasterElement(parent);
      viewMasterElement.ViewName = "ImagesFrontendThumbnailsListSimple";
      viewMasterElement.ViewType = typeof (MasterThumbnailSimpleView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = ImagesDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewMasterElement DefineFrontendMasterThumbnailStripView(
      ConfigElement parent)
    {
      ImagesViewMasterElement viewMasterElement = new ImagesViewMasterElement(parent);
      viewMasterElement.ViewName = "ImagesFrontendThumbnailsListStrip";
      viewMasterElement.ViewType = typeof (MasterThumbnailStripView);
      viewMasterElement.AllowPaging = new bool?(true);
      viewMasterElement.DisplayMode = FieldDisplayMode.Read;
      viewMasterElement.ItemsPerPage = new int?(50);
      viewMasterElement.ResourceClassId = ImagesDefinitions.ResourceClassId;
      viewMasterElement.FilterExpression = "Visible = true AND Status = Live";
      viewMasterElement.SortExpression = "PublicationDate DESC";
      return (ContentViewMasterElement) viewMasterElement;
    }

    private static ContentViewDetailElement DefineFrontendDetailView(
      ConfigElement parent)
    {
      ImagesViewDetailElement viewDetailElement = new ImagesViewDetailElement(parent);
      viewDetailElement.ViewName = "ImagesDetailView";
      viewDetailElement.ViewType = typeof (DetailSimpleView);
      viewDetailElement.DisplayMode = FieldDisplayMode.Read;
      viewDetailElement.ResourceClassId = ImagesDefinitions.ResourceClassId;
      return (ContentViewDetailElement) viewDetailElement;
    }

    private static void CreateFrontendImagesDialogs(
      ConfigElementCollection parent,
      string controlDefinitionName,
      ContentViewControlDefinitionFacade fluentFacade)
    {
      fluentFacade.AddUploadDialog(DefinitionsHelper.GetDefaultProvider((ConfigElement) parent), Res.Get<ImagesResources>().Image, Res.Get<ImagesResources>().Images, Res.Get<ImagesResources>().Album, typeof (Album), "~/Sitefinity/Services/Content/ImageService.svc/", "~/Sitefinity/Services/Content/AlbumService.svc/", "create").Done().AddEditDialog("ImagesBackendEdit", controlDefinitionName, "", "{{ParentId}}", typeof (Album)).Done().AddHistoryComparisonDialog("ImagesBackendVersionComparisonView", controlDefinitionName, "", Res.Get<LibrariesResources>().ImagesTitle, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddHistoryGridDialog("ImagesBackendVersionComparisonView", controlDefinitionName, Res.Get<ImagesResources>().BackToItems, Res.Get<LibrariesResources>().ImagesTitle, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddHistoryPreviewDialog("ImagesBackendVersionPreview", controlDefinitionName, Res.Get<Labels>().BackToRevisionHistory, "{{ParentId}}", typeof (Album), "editMediaContentProperties").Done().AddPreviewDialog("ImagesBackendPreview", controlDefinitionName, "", "{{ParentId}}", typeof (Album));
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
      element2.NavigationPageId = LibrariesModule.LibraryImagesPageId;
      element2.RootPageId = LibrariesModule.ImagesHomePageId;
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
      element7.Name = "MoveToActionWidget";
      element7.Text = "MoveTo";
      element7.ResourceClassId = typeof (LibrariesResources).Name;
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "moveToAll";
      element7.WidgetType = typeof (CommandWidget);
      menuItems2.Add((WidgetElement) element7);
      ConfigElementList<WidgetElement> menuItems3 = element5.MenuItems;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element8.Name = "EditLibraryPropertiesActionWidget";
      element8.Text = "EditProperties";
      element8.ResourceClassId = typeof (LibrariesResources).Name;
      element8.WrapperTagKey = HtmlTextWriterTag.Li;
      element8.CommandName = "editLibraryProperties";
      element8.WidgetType = typeof (CommandWidget);
      element8.CssClass = "sf_NotForFolder";
      menuItems3.Add((WidgetElement) element8);
      ConfigElementList<WidgetElement> menuItems4 = element5.MenuItems;
      CommandWidgetElement element9 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element9.Name = "LibraryPermissionsActionWidget";
      element9.Text = "SetPermissions";
      element9.ResourceClassId = typeof (LibrariesResources).Name;
      element9.WrapperTagKey = HtmlTextWriterTag.Li;
      element9.CommandName = "libraryPermissions";
      element9.WidgetType = typeof (CommandWidget);
      element9.CssClass = "sf_NotForFolder";
      menuItems4.Add((WidgetElement) element9);
      ConfigElementList<WidgetElement> menuItems5 = element5.MenuItems;
      CommandWidgetElement element10 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element10.Name = "DeleteFolderActionWidget";
      element10.Text = "DeleteThisLibrary";
      element10.ResourceClassId = typeof (LibrariesResources).Name;
      element10.WrapperTagKey = HtmlTextWriterTag.Li;
      element10.CommandName = "deleteFolder";
      element10.CssClass = "sf_ForFolder";
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
      DetailViewDefinitionFacade blankItem = fluentContentView.AddDetailView(viewName).SetTitle("EditAlbum").HideTopToolbar().LocalizeUsing<ImagesResources>().DoNotUnlockDetailItemOnExit().SetServiceBaseUrl("~/Sitefinity/Services/Content/ImageService.svc/folder/").DoNotUseWorkflow().DoNotCreateBlankItem();
      DetailFormViewElement cfg = blankItem.Get();
      DetailViewDefinitionFacade definitionFacade1 = blankItem.SetExternalClientScripts(DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.LibraryDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded"));
      definitionFacade1.AddHeaderSection("HeaderSection").AddWarningField().SetId("warningsField");
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade2 = definitionFacade1.AddFirstSection("MainSection");
      definitionFacade2.AddLocalizedTextField("Title").SetId("LibraryNameFieldControl").SetTitle("AlbumName").SetExample("AlbumNameExample").SetCssClass("sfTitleField").AddValidation().MakeRequired().SetRequiredViolationMessage("AlbumNameCannotBeEmpty").Done();
      definitionFacade2.AddLocalizedTextField("Description").SetId("DescriptionFieldControl").SetTitle("Description").SetDescription("AlbumDescription").SetCssClass("sfFormSeparator").SetRows(5).AddExpandableBehavior().Collapse().SetExpandText("ClickToAddDescription").LocalizeUsing<LibrariesResources>().Done();
      ContentViewSectionElement viewSectionElement = definitionFacade2.Get();
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      ParentLibraryFieldDefinitionElement element1 = new ParentLibraryFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
      element1.FieldName = "ParentLibraryField";
      element1.DataFieldName = "ParentId";
      element1.FieldType = typeof (ParentLibraryField);
      element1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element1.WebServiceUrl = "~/Sitefinity/Services/Content/AlbumService.svc/folders/";
      element1.BindOnLoad = new bool?(false);
      element1.NoParentLibTitle = "NoParentLib";
      element1.SelectedParentLibTitle = "SelectedParentLib";
      element1.ResourceClassId = typeof (LibrariesResources).Name;
      element1.LibraryItemName = "ImageItemName";
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
      element3.ResourceClassId = ImagesDefinitions.ResourceClassId;
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
