// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.FluentSitefinity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent;
using Telerik.Sitefinity.Fluent.ContentFluentApi;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// This class represents the fluent API of the Sitefinity and provides different interfaces for working with various
  /// parts of Sitefinity.
  /// </summary>
  public class FluentSitefinity : IDisposable
  {
    private bool disposed;
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentSitefinity" /> class.
    /// </summary>
    /// <remarks>
    /// This will initialize the instance of <see cref="P:Telerik.Sitefinity.Fluent.FluentSitefinity.AppSettings" /> to its default values.
    /// </remarks>
    public FluentSitefinity() => this.appSettings = new AppSettings();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentSitefinity" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The instance of <see cref="P:Telerik.Sitefinity.Fluent.FluentSitefinity.AppSettings" /> that will configure the functionality of the fluent API.
    /// </param>
    public FluentSitefinity(AppSettings appSettings) => this.appSettings = appSettings;

    public IAnyDraftFacade AnyContentItem(Type itemType)
    {
      IAnyDraftFacade anyDraftFacade = ObjectFactory.Resolve<IAnyDraftFacade>();
      anyDraftFacade.SetInitialState(this.appSettings, itemType, (Content) null, new Guid?());
      return anyDraftFacade;
    }

    public IAnyDraftFacade AnyContentItem(Type itemType, Guid itemId)
    {
      IAnyDraftFacade anyDraftFacade = ObjectFactory.Resolve<IAnyDraftFacade>();
      anyDraftFacade.SetInitialState(this.appSettings, itemType, (Content) null, new Guid?());
      Content master = (Content) anyDraftFacade.Manager.GetItem(itemType, itemId);
      if (master.Status != ContentLifecycleStatus.Master)
        master = anyDraftFacade.Manager.GetMaster(master);
      anyDraftFacade.Set(master);
      return anyDraftFacade;
    }

    public IAnyDraftFacade AnyContentItem(string itemTypeName) => this.AnyContentItem(TypeResolutionService.ResolveType(itemTypeName));

    public IAnyDraftFacade AnyContentItem(string itemTypeName, Guid itemId) => this.AnyContentItem(TypeResolutionService.ResolveType(itemTypeName), itemId);

    public IAnyDraftFacade AnyContentItem<TContent>() where TContent : Content => this.AnyContentItem(typeof (TContent));

    public IAnyDraftFacade AnyContentItem<TContent>(Guid itemID) where TContent : Content => this.AnyContentItem(typeof (TContent), itemID);

    /// <summary>
    /// Provides the methods for working with a single page. Use this method when you want to work with a new page.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that provides fluent API for working with a single page.</returns>
    public PageFacade Page() => new PageFacade(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single page. Use this method when you want to work with an existing page.
    /// </summary>
    /// <param name="pageId">The pageId of the page you wish to work with.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that provides fluent API for working with a single page.</returns>
    public PageFacade Page(Guid pageId) => new PageFacade(this.appSettings, pageId);

    /// <summary>
    /// Provides the methods for working with a single page. Use this method when you want to work with an existing page.
    /// </summary>
    /// <param name="page">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> which represent the page on which you want to use fluent API functionality.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that provides fluent API for working with a single page.</returns>
    public PageFacade Page(PageNode page) => new PageFacade(this.appSettings, page);

    /// <summary>
    /// Provides the methods for working with a collection of pages.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PagesFacade" /> that provides fluent API for working with multiple pages.</returns>
    public PagesFacade Pages() => new PagesFacade(this.appSettings);

    /// <summary>
    /// Provides the facades with fluent API for working with dynamic data in Sitefinity.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.FluentDynamicData" />, aggregation class that exposes dynamic data
    /// related facades.
    /// </returns>
    public FluentDynamicData DynamicData() => new FluentDynamicData(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single page template. Use this method when you want to work with a new page template.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> that provides fluent API for working with a single page template.</returns>
    public PageTemplateFacade PageTemplate() => new PageTemplateFacade(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single page. Use this method when you want to work with an existing page template.
    /// </summary>
    /// <param name="templateId">The templateId of the page template you wish to work with.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> that provides fluent API for working with a single page template.</returns>
    public PageTemplateFacade PageTemplate(Guid templateId) => new PageTemplateFacade(this.appSettings, templateId);

    /// <summary>
    /// Provides the methods for working with a single page. Use this method when you want to work with an existing page template.
    /// </summary>
    /// <param name="template">
    /// An instance of the <see cref="M:Telerik.Sitefinity.Fluent.FluentSitefinity.PageTemplate" /> which represent the page template on which you want to use fluent API functionality.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplateFacade" /> that provides fluent API for working with a single page template.</returns>
    public PageTemplateFacade PageTemplate(Telerik.Sitefinity.Pages.Model.PageTemplate template) => new PageTemplateFacade(this.appSettings, template);

    /// <summary>
    /// Provides the methods for working with a list of page templates. Use this method when you want to work with a list of templates
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> that provides fluent API for working with a list page template.</returns>
    public PageTemplatesFacade PageTemplates() => new PageTemplatesFacade(this.appSettings);

    /// <summary>
    /// Provides the facades with the fluent API for working with forms module.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.FluentForms" />, aggregation class that exposes form module related facades.</returns>
    public FluentForms Forms() => new FluentForms(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of ContentItem.
    /// </summary>
    /// <returns>An instance of <see cref="!:ContentItemFacade" /> that provides fluent API for working with single instance of ContentItem.</returns>
    public GenericContentDraftFacade<BaseFacade> ContentItem() => new GenericContentDraftFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of ContentItem.
    /// </summary>
    /// <param name="itemId">The id of the ContentItem.</param>
    /// <returns>
    /// An instance of <see cref="!:ContentItemFacade" /> that provides fluent API for working with single instance of ContentItem.
    /// </returns>
    public GenericContentDraftFacade<BaseFacade> ContentItem(
      Guid itemId)
    {
      return new GenericContentDraftFacade<BaseFacade>(this.appSettings, new BaseFacade(), itemId);
    }

    /// <summary>
    /// Provides the methods for working with a collection of ContentItems.
    /// </summary>
    /// <returns>An instance of <see cref="!:ContentItemsFacade" /> that provides fluent API for working with multiple ContentItems.</returns>
    public GenericContentsPluralFacade<BaseFacade> ContentItems() => new GenericContentsPluralFacade<BaseFacade>(this.appSettings, new BaseFacade());

    /// <summary>
    /// Provides the methods for working with a single instance of Album.
    /// </summary>
    /// <returns>An instance of <see cref="!:AlbumFacade" /> that provides fluent API for working with single instance of Album.</returns>
    public AlbumSingularFacade<BaseFacade> Album() => new AlbumSingularFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Album.
    /// </summary>
    /// <param name="albumId">The id of the Album.</param>
    /// <returns>
    /// An instance of <see cref="!:AlbumFacade" /> that provides fluent API for working with single instance of Album.
    /// </returns>
    public AlbumSingularFacade<BaseFacade> Album(Guid albumId) => new AlbumSingularFacade<BaseFacade>(this.appSettings, new BaseFacade(), albumId);

    /// <summary>
    /// Provides the methods for working with a collection of Albums.
    /// </summary>
    /// <returns>An instance of <see cref="!:AlbumsFacade" /> that provides fluent API for working with multiple Albums.</returns>
    public AlbumPluralFacade<BaseFacade> Albums() => new AlbumPluralFacade<BaseFacade>(this.appSettings, new BaseFacade());

    /// <summary>
    /// Provides the methods for working with a single instance of Image.
    /// </summary>
    /// <returns>An instance of <see cref="!:ImageFacade" /> that provides fluent API for working with single instance of Image.</returns>
    public ImageDraftFacade<BaseFacade> Image() => new ImageDraftFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Image.
    /// </summary>
    /// <param name="imageId">The id of the Image.</param>
    /// <returns>
    /// An instance of <see cref="!:ImageFacade" /> that provides fluent API for working with single instance of Image.
    /// </returns>
    public ImageDraftFacade<BaseFacade> Image(Guid imageId) => new ImageDraftFacade<BaseFacade>(this.appSettings, new BaseFacade(), imageId);

    /// <summary>
    /// Provides the methods for working with a collection of Images.
    /// </summary>
    /// <returns>An instance of <see cref="!:AlbumsFacade" /> that provides fluent API for working with multiple Images.</returns>
    public ImagesPluralFacade<BaseFacade> Images() => new ImagesPluralFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of VideoLibrary.
    /// </summary>
    /// <returns>An instance of <see cref="!:VideoLibraryFacade" /> that provides fluent API for working with single instance of VideoLibrary.</returns>
    public VideoLibrarySingularFacade<BaseFacade> VideoLibrary() => new VideoLibrarySingularFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of VideoLibrary.
    /// </summary>
    /// <param name="libraryId">The library id.</param>
    /// <returns>An instance of <see cref="!:VideoLibraryFacade" /> that provides fluent API for working with single instance of VideoLibrary.</returns>
    public VideoLibrarySingularFacade<BaseFacade> VideoLibrary(
      Guid libraryId)
    {
      return new VideoLibrarySingularFacade<BaseFacade>(this.appSettings, new BaseFacade(), libraryId);
    }

    /// <summary>
    /// Provides the methods for working with a collection of VideoLibraries.
    /// </summary>
    /// <returns>An instance of <see cref="!:VideoLibrariesFacade" /> that provides fluent API for working with multiple VideoLibraries.</returns>
    public VideoLibraryPluralFacade<BaseFacade> VideoLibraries() => new VideoLibraryPluralFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Video.
    /// </summary>
    /// <returns>An instance of <see cref="!:VideoFacade" /> that provides fluent API for working with single instance of Video.</returns>
    public VideoDraftFacade<BaseFacade> Video() => new VideoDraftFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Video.
    /// </summary>
    /// <param name="videoId">The id of the Video.</param>
    /// <returns>
    /// An instance of <see cref="!:VideoFacade" /> that provides fluent API for working with single instance of Video.
    /// </returns>
    public VideoDraftFacade<BaseFacade> Video(Guid videoId) => new VideoDraftFacade<BaseFacade>(this.appSettings, new BaseFacade(), videoId);

    /// <summary>
    /// Provides the methods for working with a collection of Videos.
    /// </summary>
    /// <returns>An instance of <see cref="!:VideosFacade" /> that provides fluent API for working with multiple Videos.</returns>
    public VideoPluralFacade<BaseFacade> Videos() => new VideoPluralFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of DocumentLibrary.
    /// </summary>
    /// <returns>An instance of <see cref="!:DocumentLibraryFacade" /> that provides fluent API for working with single instance of DocumentLibrary.</returns>
    public DocumentLibrarySingularFacade<BaseFacade> DocumentLibrary() => new DocumentLibrarySingularFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of DocumentLibrary.
    /// </summary>
    /// <param name="libraryId">The id of the DocumentLibrary.</param>
    /// <returns>
    /// An instance of <see cref="!:DocumentLibraryFacade" /> that provides fluent API for working with single instance of DocumentLibrary.
    /// </returns>
    public DocumentLibrarySingularFacade<BaseFacade> DocumentLibrary(
      Guid libraryId)
    {
      return new DocumentLibrarySingularFacade<BaseFacade>(this.appSettings, new BaseFacade(), libraryId);
    }

    /// <summary>
    /// Provides the methods for working with a collection of DocumentLibraries.
    /// </summary>
    /// <returns>An instance of <see cref="!:DocumentLibrariesFacade" /> that provides fluent API for working with multiple DocumentLibraries.</returns>
    public DocumentLibraryPluralFacade<BaseFacade> DocumentLibraries() => new DocumentLibraryPluralFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Document.
    /// </summary>
    /// <returns>An instance of <see cref="!:DocumentFacade" /> that provides fluent API for working with single instance of Document.</returns>
    public DocumentDraftFacade<BaseFacade> Document() => new DocumentDraftFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single instance of Document.
    /// </summary>
    /// <param name="documentId">The id of the Document.</param>
    /// <returns>
    /// An instance of <see cref="!:DocumentFacade" /> that provides fluent API for working with single instance of Document.
    /// </returns>
    public DocumentDraftFacade<BaseFacade> Document(Guid documentId) => new DocumentDraftFacade<BaseFacade>(this.appSettings, new BaseFacade(), documentId);

    /// <summary>
    /// Provides the methods for working with a collection of Documents.
    /// </summary>
    /// <returns>An instance of <see cref="!:DocumentsFacade" /> that provides fluent API for working with multiple Documents.</returns>
    public DocumentsPluralFacade<BaseFacade> Documents() => new DocumentsPluralFacade<BaseFacade>(this.appSettings);

    /// <summary>
    /// Provides the facades with the fluent API used for user profiles.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.FluentUserProfiles" />, aggregation class that exposes user profiles related facades.</returns>
    public FluentUserProfiles UserProfiles() => new FluentUserProfiles(this.appSettings);

    /// <summary>
    /// Provides methods for installation, uninstallation, upgrade and initialization of modules.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleFacade" /> which provides functionality for installing, uninstalling, upgrading and initializing of modules.</returns>
    public ModuleFacade Module() => new ModuleFacade(this.appSettings);

    /// <summary>
    /// Provides methods for installation, uninstallation, upgrade and initialization of modules.
    /// </summary>
    /// <param name="moduleName">Name of the module for which the fluent API will be used.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleFacade" /> which provides functionality for installing, uninstalling, upgrading and initializing of modules.</returns>
    public ModuleFacade Module(string moduleName) => !string.IsNullOrEmpty(moduleName) ? new ModuleFacade(moduleName, this.appSettings) : throw new ArgumentNullException(nameof (moduleName), "moduleName argument cannot be empty or null.");

    /// <summary>
    /// Provides methods for installation, uninstallation, upgrad and initialization of modules.
    /// </summary>
    /// <param name="moduleName">Name of the module for which the fluent API will be used.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleFacade" /> which provides functionality for installing, uninstalling, upgrading and initializing a module.</returns>
    public ModuleFacade Module(string moduleName, PageManager pageManager)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName), "moduleName argument cannot be empty or null.");
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager), "pageManager argument cannot be null.");
      return new ModuleFacade(moduleName, pageManager, this.appSettings);
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LifecycleFacade" />.
    /// </summary>
    public LifecycleFacade Lifecycle() => new LifecycleFacade(this.appSettings);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations before the
    /// <see cref="T:Telerik.Sitefinity.Fluent.FluentSitefinity" /> is reclaimed by garbage collection.
    /// </summary>
    ~FluentSitefinity() => this.Dispose(false);

    private void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (!this.AppSettings.IsGlobalTransaction)
        TransactionManager.DisposeTransaction(this.appSettings.TransactionName);
      this.disposed = true;
    }

    public AppSettings AppSettings => this.appSettings;

    internal void SaveChanges()
    {
      if (this.AppSettings.IsGlobalTransaction)
        return;
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade(
      Type itemType)
    {
      Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade dataItemFacade = new Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade();
      dataItemFacade.SetInitialState(this.appSettings, itemType, (IDataItem) null, new Guid?());
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) dataItemFacade;
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade()
    {
      Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade dataItemFacade = new Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade();
      dataItemFacade.SetInitialState(this.appSettings, (Type) null, (IDataItem) null, new Guid?());
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) dataItemFacade;
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade(
      Type itemType,
      Guid itemId)
    {
      Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade dataItemFacade = new Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade();
      dataItemFacade.SetInitialState(this.appSettings, itemType, (IDataItem) null, new Guid?());
      dataItemFacade.Set((IDataItem) dataItemFacade.Manager.GetItem(itemType, itemId));
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) dataItemFacade;
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade(
      Type itemType,
      Guid itemId,
      string provider)
    {
      Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade dataItemFacade = new Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade();
      dataItemFacade.SetInitialState(this.appSettings, itemType, (IDataItem) null, new Guid?());
      dataItemFacade.SetProviderName(provider);
      dataItemFacade.Set((IDataItem) dataItemFacade.Manager.GetItem(itemType, itemId));
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) dataItemFacade;
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade(
      string itemTypeName)
    {
      return this.DataItemFacade(TypeResolutionService.ResolveType(itemTypeName));
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade(
      string itemTypeName,
      Guid itemId)
    {
      return this.DataItemFacade(TypeResolutionService.ResolveType(itemTypeName), itemId);
    }

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade<TContent>() where TContent : Content => this.DataItemFacade(typeof (TContent));

    internal Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DataItemFacade<TContent>(
      Guid itemID)
      where TContent : Content
    {
      return this.DataItemFacade(typeof (TContent), itemID);
    }
  }
}
