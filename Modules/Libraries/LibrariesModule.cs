// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Routing;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Data;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Modules.Libraries.Web.Services;
using Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility;
using Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RecycleBin.ItemFactories;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Web.Services.Contracts;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Entry point for the Libraries Module.</summary>
  public class LibrariesModule : 
    ContentModuleBase,
    IPublishingEnabledModule,
    IDashboardEnabledModule,
    IExportableModule,
    ITypeSettingsProvider,
    ITrackingReporter
  {
    /// <summary>
    /// Name of the Libraries module. (e.g. used in LibrariesManager)
    /// </summary>
    public const string ModuleName = "Libraries";
    /// <summary>
    /// Identity for the page group used by all pages in the Images module
    /// </summary>
    public static readonly Guid ImagesPageGroupId = new Guid("B909836F-6C7E-4a2b-B32D-3EDD5E40DE2E");
    /// <summary>
    /// Identity for the page group used by all pages in the Documents module
    /// </summary>
    public static readonly Guid DocumentsPageGroupId = new Guid("F3083700-E625-4945-B1F8-65D9011912EA");
    /// <summary>
    /// Identity for the page group used by all pages in the Videos module
    /// </summary>
    public static readonly Guid VideosPageGroupId = new Guid("53F8FC22-1983-42b9-82D2-357BEC53BAF6");
    /// <summary>Identity of the home (landing) page for the images</summary>
    public static readonly Guid ImagesHomePageId = new Guid("FA2F74B8-7008-40fe-9054-53C2DA565531");
    /// <summary>
    /// Identity of the page for the images in a specific library
    /// </summary>
    public static readonly Guid LibraryImagesPageId = new Guid("DA65343D-EBC9-4464-AFE5-2A3BC7E425B9");
    /// <summary>Identity of the page for the albums</summary>
    [Obsolete("Not applicable anymore.")]
    public static readonly Guid AlbumsPageId = new Guid("9AB017C9-E6A3-4bf6-BE8B-09F8B109D190");
    /// <summary>Identity of the home (landing) page for the documents</summary>
    public static readonly Guid DocumentsHomePageId = new Guid("4059ABCF-42E7-4c9c-BF2F-CFB1BD55B349");
    /// <summary>
    /// Identity of the page for the documents in a specific library
    /// </summary>
    public static readonly Guid LibraryDocumentsPageId = new Guid("60300A16-EE7F-4295-8B4E-765635020A3A");
    /// <summary>Identity of the page for library list for the videos</summary>
    [Obsolete("Not applicable anymore.")]
    public static readonly Guid DocumentLibraryListPageId = new Guid("F371AF72-F66B-418e-AA64-582BF7C5CD6B");
    /// <summary>Identity of the home (landing) page for the videos</summary>
    public static readonly Guid VideosHomePageId = new Guid("F1ECD0CD-1C21-4bb6-86D8-E6D604D2826F");
    /// <summary>
    /// Identity of the page for the videos in a specific library
    /// </summary>
    public static readonly Guid LibraryVideosPageId = new Guid("71149983-D0A8-40dd-BDC3-09F595DB483C");
    /// <summary>Identity of the page for library list for the videos</summary>
    [Obsolete("Not applicable anymore.")]
    public static readonly Guid VideoLibraryListPageId = new Guid("18F93050-D9C3-4f5d-884C-9709851A0D1A");
    /// <summary>Localization resources' class Id for Libraries</summary>
    public static readonly string ResourceClassId = typeof (LibrariesResources).Name;
    /// <summary>Id of the default library for the images</summary>
    [Obsolete("Not applicable when using multiple provider")]
    public static readonly Guid DefaultImagesLibraryId = new Guid("4BA7AD46-F29B-4e65-BE17-9BF7CE5BA1FB");
    /// <summary>Id of the default library for the documents</summary>
    [Obsolete("Not applicable when using multiple provider")]
    public static readonly Guid DefaultDocumentsLibraryId = new Guid("2064E46B-240B-431C-B9FA-4D700C57FAE0");
    /// <summary>Id of the default library for the videos</summary>
    [Obsolete("Not applicable when using multiple provider")]
    public static readonly Guid DefaultVideosLibraryId = new Guid("8818C109-3515-4A74-BF3F-D5F1D5E201A3");
    /// <summary>Id of the default thumbnails for templates</summary>
    public static readonly Guid DefaultTemplateThumbnailsLibraryId = new Guid("86D90DA3-6982-47F4-B28E-9B401CA7DA0A");
    /// <summary>
    /// The control id of the ContentView inside the Images screen.
    /// </summary>
    public const string ImagesBackedContentViewControlId = "imgsCntView";
    /// <summary>
    /// The control id of the ContentView inside the image album screen.
    /// </summary>
    [Obsolete("Not applicable anymore")]
    public const string ImageAlbumBackedContentViewControlId = "imgAlbmCntView";
    /// <summary>
    /// The control id of the ContentView inside the image albums screen.
    /// </summary>
    public const string ImageAlbumsBackedContentViewControlId = "imgAlbmsCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Documents screen.
    /// </summary>
    public const string DocumentsContentViewControlId = "docsCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Document library screen.
    /// </summary>
    [Obsolete("Not applicable anymore")]
    public const string DocumentLibraryContentViewControlId = "docLibCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Document libraries screen.
    /// </summary>
    public const string DocumentLibrariesContentViewControlId = "docLibsCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Videos screen.
    /// </summary>
    public const string VideosContentViewControlId = "vidsCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Video library screen.
    /// </summary>
    [Obsolete("Not applicable anymore")]
    public const string VideoLibraryContentViewControlId = "vidLibCntView";
    /// <summary>
    /// TThe control id of the ContentView inside the Video libraries screen.
    /// </summary>
    public const string VideoLibrariesContentViewControlId = "vidLibsCntView";
    /// <summary>Provider name that will used by System Libraries</summary>
    public const string SystemLibrariesProviderName = "SystemLibrariesProvider";
    internal const string AzureBlobStorageProviderTypeName = "Telerik.Sitefinity.Azure.BlobStorage.AzureBlobStorageProvider, Telerik.Sitefinity.Azure";
    internal const string AzureBlobSettingsViewTypeName = "Telerik.Sitefinity.Azure.BlobStorage.AzureBlobSettingsView";
    internal const string AmazonBlobStorageProviderTypeName = "Telerik.Sitefinity.Amazon.BlobStorage.AmazonBlobStorageProvider, Telerik.Sitefinity.Amazon";
    public const string BlobStorageConfigServiceUrl = "Sitefinity/Services/Content/BlobStorage.svc";
    public const string LibraryRelocationServiceUrl = "Sitefinity/Services/Content/LibraryRelocationService.svc";
    public const string ImagesFrontEndServiceUrl = "Sitefinity/Frontend/Services/Content/ImageService.svc";
    public const string DocumentsFrontEndServiceUrl = "Sitefinity/Frontend/Services/Content/DocumentService.svc";
    public const string ThumbnailsServiceUrl = "Sitefinity/Services/ThumbnailService.svc";
    internal const string ThumbnailListDialogUrl = "~/Sitefinity/Dialog/ThumbnailListDialog";
    internal const string LibrariesMasterExtensionsScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.LibrariesMasterExtensions.js";
    internal const string DefaultVideoExtensionsHtml5Player = ".mp4, .webm, .ogv";
    private const string Less25mb = "Less than 25 MB";
    private const string Mb25mb100 = "Between 25 and 100 MB";
    private const string Mb100mb500 = "Between 100 and 500 MB";
    private const string Mb500gb1 = "Between 500 MB and 1 GB";
    private const string Greater1gb = "Greater than 1GB";
    private const long Mb25 = 26214400;
    private const long Mb100 = 104857600;
    private const long Mb500 = 524288000;
    private const long Gb1 = 1073741824;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[2]
    {
      typeof (LibrariesManager),
      typeof (BlobStorageManager)
    };

    /// <summary>
    /// Gets the identity of the home (landing) page for the Libraries module.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => Guid.Empty;

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartupAsync;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().Localization<LibrariesResources>().SitemapFilter<LibrariesNodeFilter>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailView, Telerik.Sitefinity.Libraries.Model.Image>().TemplatableControl<MasterThumbnailStripView, Telerik.Sitefinity.Libraries.Model.Image>().TemplatableControl<MasterThumbnailSimpleView, Telerik.Sitefinity.Libraries.Model.Image>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView, Telerik.Sitefinity.Libraries.Model.Image>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.DetailSimpleView, Telerik.Sitefinity.Libraries.Model.Image>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailView, Telerik.Sitefinity.Libraries.Model.Video>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView, Telerik.Sitefinity.Libraries.Model.Video>().TemplatableControl<MediaPlayerControl, Telerik.Sitefinity.Libraries.Model.Video>().TemplatableControl<Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.DetailSimpleView, Telerik.Sitefinity.Libraries.Model.Video>().TemplatableControl<MasterListView, Telerik.Sitefinity.Libraries.Model.Document>().TemplatableControl<MasterTableView, Telerik.Sitefinity.Libraries.Model.Document>().TemplatableControl<MasterListDetailView, Telerik.Sitefinity.Libraries.Model.Document>().TemplatableControl<MasterTableDetailView, Telerik.Sitefinity.Libraries.Model.Document>().TemplatableControl<DetailsListView, Telerik.Sitefinity.Libraries.Model.Document>().WebService<BlobStorageService>("Sitefinity/Services/Content/BlobStorage.svc").WebService<ImageService>("Sitefinity/Frontend/Services/Content/ImageService.svc").WebService<DocumentService>("Sitefinity/Frontend/Services/Content/DocumentService.svc").WebService<LibraryRelocationService>("Sitefinity/Services/Content/LibraryRelocationService.svc").WebService<ThumbnailService>("Sitefinity/Services/ThumbnailService.svc").BasicSettings<BlobStorageBasicSettingsView>("BlobStorage", "StorageProvidersForLibraries", "LibrariesResources").BasicSettings<ThumbnailsBasicSettingsView>("Thumbnails", "ThumbnailProfiles", "LibrariesResources").Route("LibraryItems", (RouteBase) ObjectFactory.Resolve<LibraryRoute>()).Dialog<ThumbnailProfileDialog>().Dialog<ThumbnailPromptDialog>().Dialog<ThumbnailListDialog>().Dialog<ThumbnailSettingsDialog>();
      ObjectFactory.Container.RegisterType<IRecycleBinItemFactory, ContentItemRecycleBinFactory>(typeof (LibrariesManager).FullName);
      ObjectFactory.Container.RegisterType<IStructureTransfer, LibrariesStructureTransfer>(new LibrariesStructureTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType<IContentTransfer, LibrariesContentTransfer>(new LibrariesContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IDownloadSecurityProvider), typeof (TemplateThumbnailsDownloadSecurityProvider), "TemplateThumbnailsDownloadSecurityProvider", (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      this.RegisterFilterStrategies();
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null,
        PluralTitle = Res.Get<ImagesResources>().AlbumsPluralTypeName,
        SingularTitle = Res.Get<ImagesResources>().AlbumsSingleTypeName,
        Icon = "folder-o"
      });
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName,
        PluralTitle = Res.Get<ImagesResources>().ImagesPluralTypeName,
        SingularTitle = Res.Get<ImagesResources>().ImagesSingleTypeName,
        Icon = "picture-o"
      });
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null,
        PluralTitle = Res.Get<VideosResources>().VideoLibraryPluralTypeName,
        SingularTitle = Res.Get<VideosResources>().VideoLibrarySingleTypeName,
        Icon = "folder-o"
      });
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName,
        PluralTitle = Res.Get<VideosResources>().VideosPluralTypeName,
        SingularTitle = Res.Get<VideosResources>().VideosSingleTypeName,
        Icon = "video-camera"
      });
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null,
        PluralTitle = Res.Get<DocumentsResources>().DocumentLibrariesPluralTypeName,
        SingularTitle = Res.Get<DocumentsResources>().DocumentLibrarySingleTypeName,
        Icon = "folder-o"
      });
      SystemManager.TypeRegistry.Register(typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName, new SitefinityType()
      {
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName,
        PluralTitle = Res.Get<DocumentsResources>().DocumentsPluralTypeName,
        SingularTitle = Res.Get<DocumentsResources>().DocumentsSingleTypeName,
        Icon = "file-o"
      });
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (FolderOperationProvider), typeof (FolderOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (DocumentOperationProvider), typeof (DocumentOperationProvider).Name);
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (MediaOperationProvider), typeof (MediaOperationProvider).Name);
      this.RegisterConfigRestrictions();
    }

    void IPublishingEnabledModule.ConfigurePublishing()
    {
      if (!PublishingSystemFactory.IsPipeRegistered(DocumentInboundPipe.PipeName))
        PublishingSystemFactory.RegisterPipe(DocumentInboundPipe.PipeName, typeof (DocumentInboundPipe));
      List<Mapping> defaultMappings1 = DocumentInboundPipe.GetDefaultMappings();
      PublishingSystemFactory.RegisterPipeMappings(DocumentInboundPipe.PipeName, true, (IList<Mapping>) defaultMappings1);
      SitefinityContentPipeSettings defaultPipeSettings1 = DocumentInboundPipe.GetDefaultPipeSettings();
      PublishingSystemFactory.RegisterPipeSettings(DocumentInboundPipe.PipeName, (PipeSettings) defaultPipeSettings1);
      PublishingSystemFactory.RegisterPipeForAllContentTemplates(PublishingSystemFactory.GetPipeSettings(DocumentInboundPipe.PipeName), (Predicate<PipeSettings>) (ps => ps.PipeName == DocumentInboundPipe.PipeName));
      IList<IDefinitionField> contentPipeDefinitions1 = PublishingSystemFactory.CreateDefaultAnyContentPipeDefinitions();
      PublishingSystemFactory.RegisterPipeDefinitions(DocumentInboundPipe.PipeName, contentPipeDefinitions1);
      if (!PublishingSystemFactory.IsPipeRegistered(MediaContentInboundPipe.PipeName))
        PublishingSystemFactory.RegisterPipe(MediaContentInboundPipe.PipeName, typeof (MediaContentInboundPipe));
      List<Mapping> defaultMappings2 = MediaContentInboundPipe.GetDefaultMappings();
      PublishingSystemFactory.RegisterPipeMappings(MediaContentInboundPipe.PipeName, true, (IList<Mapping>) defaultMappings2);
      SitefinityContentPipeSettings defaultPipeSettings2 = MediaContentInboundPipe.GetDefaultPipeSettings();
      PublishingSystemFactory.RegisterPipeSettings(MediaContentInboundPipe.PipeName, (PipeSettings) defaultPipeSettings2);
      PublishingSystemFactory.RegisterPipeForAllContentTemplates(PublishingSystemFactory.GetPipeSettings(MediaContentInboundPipe.PipeName), (Predicate<PipeSettings>) (ps => ps.PipeName == MediaContentInboundPipe.PipeName && ps is SitefinityContentPipeSettings && (ps as SitefinityContentPipeSettings).ContentTypeName == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName));
      PipeSettings pipeSettings;
      ((SitefinityContentPipeSettings) (pipeSettings = PublishingSystemFactory.GetPipeSettings(MediaContentInboundPipe.PipeName))).ContentTypeName = typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName;
      pipeSettings.UIName = Res.Get<LibrariesResources>().VideosTitle;
      PublishingSystemFactory.RegisterPipeForAllContentTemplates(pipeSettings, (Predicate<PipeSettings>) (ps => ps.PipeName == MediaContentInboundPipe.PipeName && ps is SitefinityContentPipeSettings && (ps as SitefinityContentPipeSettings).ContentTypeName == typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName));
      IList<IDefinitionField> contentPipeDefinitions2 = PublishingSystemFactory.CreateDefaultAnyContentPipeDefinitions();
      PublishingSystemFactory.RegisterPipeDefinitions(MediaContentInboundPipe.PipeName, contentPipeDefinitions2);
    }

    void IDashboardEnabledModule.Configure(IDashboardInitializer initializer)
    {
      initializer.RegisterType(typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName);
      initializer.RegisterType(typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName);
      initializer.RegisterType(typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName);
    }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      base.Install(initializer);
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailView).FullName, "List of thumbnails", new Guid("DB0D628C-5471-4197-A94F-000000000001"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailStripView.ascx", typeof (MasterThumbnailStripView).FullName, "Image and strip of thumbnails", new Guid("DB0D628C-5471-4197-A94F-000000000003"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailSimpleView.ascx", typeof (MasterThumbnailSimpleView).FullName, "List of images in full size", new Guid("DB0D628C-5471-4197-A94F-000000000004"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailLightBoxView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView).FullName, "List of thumbnails and overlay dialog (lightbox)", new Guid("DB0D628C-5471-4197-A94F-000000000002"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.DetailSimpleView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.DetailSimpleView).FullName, "Single image with details", new Guid("DB0D628C-5471-4197-A94F-000000000005"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.MasterThumbnailView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailView).FullName, "List of video thumbnails", new Guid("DB92F414-1C8F-4F43-ABFE-000000000001"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.MasterThumbnailLightBoxView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView).FullName, "List of video thumbnails and overlay dialog (lightbox)", new Guid("DB92F414-1C8F-4F43-ABFE-000000000002"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.DetailSimpleView.ascx", typeof (Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.DetailSimpleView).FullName, "Single video with details", new Guid("DB92F414-1C8F-4F43-ABFE-000000000003"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Videos.Html5MediaPlayerControl.ascx", typeof (MediaPlayerControl).FullName, "Media Player HTML5", new Guid("DB92F414-1C8F-4F43-ABFE-000000000005"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListView.ascx", typeof (MasterListView).FullName, "List of documents for direct download", new Guid("57D8E0F2-8B3D-4CBF-96A4-000000000001"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableView.ascx", typeof (MasterTableView).FullName, "Table of documents for direct download", new Guid("57D8E0F2-8B3D-4CBF-96A4-000000000002"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListDetailView.ascx", typeof (MasterListDetailView).FullName, "List of documents linking to document details", new Guid("57D8E0F2-8B3D-4CBF-96A4-000000000004"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableDetailView.ascx", typeof (MasterTableDetailView).FullName, "Table of documents for direct download and links to document details", new Guid("57D8E0F2-8B3D-4CBF-96A4-000000000005"));
      initializer.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.DetailsListView.ascx", typeof (DetailsListView).FullName, "Document details", new Guid("57D8E0F2-8B3D-4CBF-96A4-000000000003"));
      PageTemplateHelper.InstallTemplateThumbnailsLibrary(initializer);
    }

    /// <summary>Installs the pages.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallPages(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(LibrariesModule.ImagesPageGroupId, "Images").PlaceUnder(CommonNode.TypesOfContent).SetOrdinal(4).LocalizeUsing<LibrariesResources>().SetTitleLocalized("ImagesTitle").SetUrlNameLocalized("ImagesPageGroupNodeTitle").SetDescriptionLocalized("ImagesPageGroupNodeDescription").AddChildPage(LibrariesModule.ImagesHomePageId, "Images").LocalizeUsing<LibrariesResources>().SetTitleLocalized("ImagesTitle").SetUrlNameLocalized("ImagesUrlName").SetDescriptionLocalized("ImagesDescription").SetHtmlTitleLocalized("ImagesHtmlTitle").AddContentView("AlbumsBackend", "imgAlbmsCntView").Done().AddChildPage(LibrariesModule.LibraryImagesPageId, "LibraryImages").LocalizeUsing<LibrariesResources>().SetTitleLocalized("LibraryImagesTitle").SetUrlNameLocalized("LibraryImagesUrlName").SetDescriptionLocalized("LibraryImagesDescription").SetHtmlTitleLocalized("LibraryImagesHtmlTitle").AddContentView("ImagesBackend", "imgsCntView").Done().Done().CreateModuleGroupPage(LibrariesModule.DocumentsPageGroupId, "Documents").PlaceUnder(CommonNode.TypesOfContent).SetOrdinal(6).LocalizeUsing<LibrariesResources>().SetTitleLocalized("DocumentsTitle").SetUrlNameLocalized("DocumentsPageGroupNodeUrl").SetDescriptionLocalized("DocumentsPageGroupNodeDescription").AddChildPage(LibrariesModule.DocumentsHomePageId, "Documents").LocalizeUsing<LibrariesResources>().SetTitleLocalized("DocumentsTitle").SetUrlNameLocalized("DocumentsUrlName").SetDescriptionLocalized("DocumentsDescription").SetHtmlTitleLocalized("DocumentsHtmlTitle").AddContentView(DocumentsDefinitions.BackendLibraryDefinitionName, "docLibsCntView").Done().AddChildPage(LibrariesModule.LibraryDocumentsPageId, "LibraryDocuments").LocalizeUsing<LibrariesResources>().SetTitleLocalized("LibraryDocumentsTitle").SetUrlNameLocalized("LibraryDocumentsUrlName").SetDescriptionLocalized("LibraryDocumentsDescription").SetHtmlTitleLocalized("LibraryDocumentsHtmlTitle").AddContentView(DocumentsDefinitions.BackendDefinitionName, "docsCntView").Done().Done().CreateModuleGroupPage(LibrariesModule.VideosPageGroupId, "Videos").PlaceUnder(CommonNode.TypesOfContent).SetOrdinal(5).LocalizeUsing<LibrariesResources>().SetTitleLocalized("VideosTitle").SetUrlNameLocalized("VideosPageGroupNodeTitle").SetDescriptionLocalized<LibrariesResources>("VideosPageGroupNodeDescription").AddChildPage(LibrariesModule.VideosHomePageId, "Videos").LocalizeUsing<LibrariesResources>().SetTitleLocalized("VideosTitle").SetUrlNameLocalized("VideosUrlName").SetDescriptionLocalized("VideosDescription").SetHtmlTitleLocalized("VideosHtmlTitle").AddContentView(VideosDefinitions.BackendLibraryDefinitionName, "vidLibsCntView").Done().AddChildPage(LibrariesModule.LibraryVideosPageId, "LibraryVideos").LocalizeUsing<LibrariesResources>().SetTitleLocalized("LibraryVideosTitle").SetUrlNameLocalized("LibraryVideosUrlName").SetDescriptionLocalized("LibraryVideosDescription").SetHtmlTitleLocalized("LibraryVideosHtmlTitle").AddContentView(VideosDefinitions.BackendVideosDefinitionName, "vidsCntView").Done().Done().CreateModuleGroupPage(UserFilesConstants.UserFilesNodeId, "UserFilesNodeTitle").PlaceUnder(CommonNode.Settings).SetOrdinal(3).LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesModuleTitle").SetUrlNameLocalized("UserFileModuleUrlName").SetDescriptionLocalized("UserFilesModuleDescription").AddChildPage(UserFilesConstants.HomePageId, "UserFilesNodeTitle)").LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesModuleTitle").SetUrlNameLocalized("UserFileModuleUrlName").SetDescriptionLocalized("UserFilesModuleDescription").SetHtmlTitleLocalized("UserFilesModuleTitle").AddContentView(UserFilesDefinitions.BackendUserFilesDefinitionName, "userFilesCntView").Done().AddChildPage(UserFilesConstants.UserFilesDocumentsPageId, "UserFilesDocuments").LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesDocumentsTitle").SetUrlNameLocalized("UserFilesDocumentsUrlName").SetDescriptionLocalized("UserFilesDocumentsDescription").SetHtmlTitleLocalized("UserFilesDocumentsHtmlTitle").AddContentView(UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, "userFilesDocumentsCntView").Done();

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      base.Upgrade(initializer, upgradeFrom);
      if (upgradeFrom.Build <= 1210)
      {
        PageNode pageNode1 = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == LibrariesModule.DocumentsPageGroupId)).SingleOrDefault<PageNode>();
        if (pageNode1 != null)
        {
          pageNode1.Title = (Lstring) Res.Expression("DocumentsResources", "ModuleTitle");
          Res.SetLstring(pageNode1.Description, LibrariesModule.ResourceClassId, "DocumentsPageGroupNodeDescription");
        }
        PageNode pageNode2 = initializer.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == LibrariesModule.DocumentLibraryListPageId));
        if (pageNode2 != null)
        {
          Res.SetLstring(pageNode2.Title, LibrariesModule.ResourceClassId, "LibrariesTitle");
          Res.SetLstring(pageNode2.Description, LibrariesModule.ResourceClassId, "LibrariesDescription");
          Res.SetLstring(pageNode2.Page.HtmlTitle, LibrariesModule.ResourceClassId, "LibrariesHtmlTitle");
        }
      }
      if (upgradeFrom.Build < 1238)
        this.UpgradeFrom1238(initializer);
      if (upgradeFrom.Major == 4 && upgradeFrom.Minor < 2)
      {
        LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<Telerik.Sitefinity.Libraries.Model.Image, LibrariesManager>(initializer, (ContentModuleConfigBase) Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>());
        LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<Telerik.Sitefinity.Libraries.Model.Video, LibrariesManager>(initializer, (ContentModuleConfigBase) Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>());
        LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<Telerik.Sitefinity.Libraries.Model.Document, LibrariesManager>(initializer, (ContentModuleConfigBase) Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>());
      }
      if (upgradeFrom.Build < 1800)
        this.UpgradeLibrariesOrdinalCounters(initializer);
      if (upgradeFrom.Major < 5 && upgradeFrom.Minor == 4)
      {
        this.UpgradeContentDefaultUrls(initializer);
        this.AddUserFilesMenuItem(initializer);
        LibrariesManager managerInTransaction = initializer.GetManagerInTransaction<LibrariesManager>("SystemLibrariesProvider");
        using (new ElevatedModeRegion((IManager) managerInTransaction))
          this.RestoreDownloadableGoodsLibraryPermissions(managerInTransaction);
      }
      if (upgradeFrom.Build < 3860)
      {
        initializer.TryFixTaxonomyStatistics(typeof (Telerik.Sitefinity.Libraries.Model.Image));
        initializer.TryFixTaxonomyStatistics(typeof (Telerik.Sitefinity.Libraries.Model.Video));
        initializer.TryFixTaxonomyStatistics(typeof (Telerik.Sitefinity.Libraries.Model.Document));
      }
      if (upgradeFrom.Build <= 3900)
      {
        ConfigElementList<SortingExpressionElement> expressionSettings = initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings;
        foreach (SortingExpressionElement expressionElement in expressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (r => r.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName || r.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName || r.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName)).ToArray<SortingExpressionElement>())
          expressionSettings.Remove(expressionElement);
        this.InitializeSortingExpressionSettings(expressionSettings);
      }
      if (upgradeFrom.Build < 4100)
      {
        this.UpdateContentView(initializer, LibrariesModule.ImagesHomePageId, "imgAlbmsCntView", "AlbumsBackend");
        this.UpdateContentView(initializer, LibrariesModule.LibraryImagesPageId, "imgsCntView", "ImagesBackend");
        this.DeletePage(initializer, LibrariesModule.AlbumsPageId);
        this.UpdateContentView(initializer, LibrariesModule.DocumentsHomePageId, "docLibsCntView", DocumentsDefinitions.BackendLibraryDefinitionName);
        this.UpdateContentView(initializer, LibrariesModule.LibraryDocumentsPageId, "docsCntView", DocumentsDefinitions.BackendDefinitionName);
        this.DeletePage(initializer, LibrariesModule.DocumentLibraryListPageId);
        this.UpdateContentView(initializer, LibrariesModule.VideosHomePageId, "vidLibsCntView", VideosDefinitions.BackendLibraryDefinitionName);
        this.UpdateContentView(initializer, LibrariesModule.LibraryVideosPageId, "vidsCntView", VideosDefinitions.BackendVideosDefinitionName);
        this.DeletePage(initializer, LibrariesModule.VideoLibraryListPageId);
      }
      if (upgradeFrom.Build < SitefinityVersion.Sitefinity6_1.Build)
      {
        LibrariesConfig config = initializer.Context.GetConfig<LibrariesConfig>();
        this.EnsureOld120TmbProfile(config.Images.Thumbnails.Profiles);
        this.EnsureOld120TmbProfile(config.Videos.Thumbnails.Profiles);
        foreach (LibrariesDataProvider librariesDataProvider in (Collection<LibrariesDataProvider>) (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection ?? LibrariesManager.GetManager().StaticProviders))
        {
          if (!librariesDataProvider.IsSystemProvider())
          {
            LibrariesManager manager = LibrariesManager.GetManager(librariesDataProvider.Name, "sf_lib_upgrade_def_tmb_profs");
            using (new ElevatedModeRegion((IManager) manager))
            {
              using (new DataSyncModeRegion((IManager) manager))
              {
                IEnumerable<string> array1 = (IEnumerable<string>) config.Images.Thumbnails.Profiles.Values.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (p => p.IsDefault)).Select<ThumbnailProfileConfigElement, string>((Func<ThumbnailProfileConfigElement, string>) (p => p.Name)).ToArray<string>();
                foreach (Telerik.Sitefinity.Libraries.Model.Album album in (IEnumerable<Telerik.Sitefinity.Libraries.Model.Album>) manager.GetAlbums())
                {
                  foreach (string str in array1)
                    album.ThumbnailProfiles.Add(str);
                }
                IEnumerable<string> array2 = (IEnumerable<string>) config.Videos.Thumbnails.Profiles.Values.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (p => p.IsDefault)).Select<ThumbnailProfileConfigElement, string>((Func<ThumbnailProfileConfigElement, string>) (p => p.Name)).ToArray<string>();
                foreach (Telerik.Sitefinity.Libraries.Model.VideoLibrary videoLibrary in (IEnumerable<Telerik.Sitefinity.Libraries.Model.VideoLibrary>) manager.GetVideoLibraries())
                {
                  foreach (string str in array2)
                    videoLibrary.ThumbnailProfiles.Add(str);
                }
                TransactionManager.CommitTransaction("sf_lib_upgrade_def_tmb_profs");
              }
            }
          }
        }
      }
      if (upgradeFrom.Build < SitefinityVersion.Sitefinity7_0.Build)
      {
        LibrariesConfig config = initializer.Context.GetConfig<LibrariesConfig>();
        if (config != null)
        {
          config.ContentBlockVideoTag = ContentBlockVideoTag.Object;
          Log.Upgrade("Libraries: Html video tag for content blocks changed to {0}", (object) config.ContentBlockVideoTag);
        }
      }
      if (upgradeFrom.Build < SitefinityVersion.Sitefinity7_1.Build)
      {
        PageTemplateHelper.InstallTemplateThumbnailsLibrary(initializer);
        Log.Upgrade("Libraries: Installed system library for page templates' thumbnails, uploaded default thumbnails.");
      }
      if (upgradeFrom.Build < SitefinityVersion.Sitefinity7_2_HF2.Build)
      {
        string str = "Setting Thumbnail Generator Hash Algorithm";
        try
        {
          LibrariesConfig config = initializer.Context.GetConfig<LibrariesConfig>();
          if (config != null)
          {
            config.Images.ImageUrlSignatureHashAlgorithm = ImageUrlSignatureHashAlgorithm.MD5;
            Log.Upgrade(string.Format("PASSED : {0}. Thumbnail Generator Hash Algorithm has been set to {1}", (object) str, (object) Enum.GetName(typeof (ImageUrlSignatureHashAlgorithm), (object) ImageUrlSignatureHashAlgorithm.MD5)));
          }
        }
        catch (Exception ex)
        {
          Log.Upgrade(string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message));
        }
      }
      if (upgradeFrom < SitefinityVersion.Sitefinity8_0)
        initializer.AddSupportedPermissionSetsToSecurityRoot<LibrariesManager>("ImagesSitemapGeneration", "DocumentsSitemapGeneration", "VideosSitemapGeneration");
      if (upgradeFrom.Build < 7000)
      {
        LibrariesManager managerInTransaction = initializer.GetManagerInTransaction<LibrariesManager>("SystemLibrariesProvider");
        bool suppressSecurityChecks = managerInTransaction.Provider.SuppressSecurityChecks;
        try
        {
          managerInTransaction.Provider.SuppressSecurityChecks = true;
          Telerik.Sitefinity.Libraries.Model.Album album = managerInTransaction.GetAlbums().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (lib => lib.Id == LibrariesModule.DefaultTemplateThumbnailsLibraryId));
          album.DownloadSecurityProviderName = "TemplateThumbnailsDownloadSecurityProvider";
          PageTemplateHelper.UpdateDefaultTemplateImages(initializer.PageManager, managerInTransaction, album);
        }
        finally
        {
          managerInTransaction.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      if (upgradeFrom.Build >= SitefinityVersion.Sitefinity12_1.Build)
        return;
      initializer.UpgradeLegacyMultipleChoiceCustomFields(typeof (Telerik.Sitefinity.Libraries.Model.Image));
      initializer.UpgradeLegacyMultipleChoiceCustomFields(typeof (Telerik.Sitefinity.Libraries.Model.Video));
      initializer.UpgradeLegacyMultipleChoiceCustomFields(typeof (Telerik.Sitefinity.Libraries.Model.Document));
    }

    protected internal override IDictionary<Type, Guid> GetTypeLandingPagesMapping() => (IDictionary<Type, Guid>) new Dictionary<Type, Guid>()
    {
      {
        typeof (Telerik.Sitefinity.Libraries.Model.Image),
        LibrariesModule.ImagesHomePageId
      },
      {
        typeof (Telerik.Sitefinity.Libraries.Model.Video),
        LibrariesModule.VideosHomePageId
      },
      {
        typeof (Telerik.Sitefinity.Libraries.Model.Document),
        LibrariesModule.DocumentsHomePageId
      }
    };

    [UpgradeInfo(Description = "AzureBlobStorageProvider is now extracted in a separate Git project. The new assembly is Telerik.Sitefinity.Azure and the new namespace is Telerik.Sitefinity.Azure.BlobStorage.", FailMassage = "Failed to replace the AzureBlobStorageProvider namespace and assembly with the new ones. AzureBlobStorageProvider might not be working.", Id = "9360e40e-3692-4b67-86c9-60c64de3b919", UpgradeTo = 5900)]
    private void UpgradeAzureBlobStorageProviderTo82(SiteInitializer initializer)
    {
      string str = "Changing the AzureBlobStorageProvider namespace and assembly";
      foreach (DataProviderSettings providerSettings in initializer.Context.GetConfig<LibrariesConfig>().BlobStorage.Providers.Values.Where<DataProviderSettings>((Func<DataProviderSettings, bool>) (p => p.ProviderTypeName == "Telerik.Sitefinity.Modules.Libraries.BlobStorage.AzureBlobStorageProvider, Telerik.Sitefinity")))
        providerSettings.ProviderTypeName = "Telerik.Sitefinity.Azure.BlobStorage.AzureBlobStorageProvider, Telerik.Sitefinity.Azure";
      Log.Upgrade(string.Format("PASSED : {0}.", (object) str));
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CAF-81345CF3BE4D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    private ThumbnailProfileConfigElement EnsureOld120TmbProfile(
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> tmbProfiles)
    {
      foreach (ThumbnailProfileConfigElement profileConfigElement in (IEnumerable<ThumbnailProfileConfigElement>) tmbProfiles.Values)
        profileConfigElement.IsDefault = false;
      ThumbnailProfileConfigElement element;
      if (!tmbProfiles.TryGetValue("thumbnail", out element))
      {
        element = new ThumbnailProfileConfigElement((ConfigElement) tmbProfiles);
        element.Name = "thumbnail";
        tmbProfiles.Add(element);
      }
      element.Title = "Thumbnail: 120 px by smaller side";
      element.IsDefault = true;
      element.Method = "ResizeFitToSideArguments";
      element.Parameters = new NameValueCollection()
      {
        {
          "Size",
          "120"
        },
        {
          "Quality",
          "Medium"
        }
      };
      return element;
    }

    private void UpdateContentView(
      SiteInitializer initializer,
      Guid id,
      string newId,
      string controlDefinitionName)
    {
      PageNode pageNode = initializer.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == id));
      PageData pageData = pageNode.GetPageData();
      if (pageNode == null || pageData == null)
        return;
      PageControl pageControl = pageData.Controls.SingleOrDefault<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == typeof (BackendContentView).FullName && c.PlaceHolder == "Content"));
      if (pageControl == null)
        return;
      ControlProperty controlProperty1 = pageControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("ControlDefinitionName", StringComparison.OrdinalIgnoreCase)));
      if (controlProperty1 != null)
        controlProperty1.Value = controlDefinitionName;
      ControlProperty controlProperty2 = pageControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase)));
      if (controlProperty2 == null)
        return;
      controlProperty2.Value = newId;
    }

    private void DeletePage(SiteInitializer initializer, Guid id)
    {
      PageManager pageManager = initializer.PageManager;
      PageNode pageNode = pageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == id));
      if (pageNode == null)
        return;
      pageManager.Delete(pageNode);
    }

    private void RestoreDownloadableGoodsLibraryPermissions(LibrariesManager manager)
    {
      if (!manager.GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (l => l.Id == UserFilesConstants.DefaultDownloadableGoodsLibraryId)).Any<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>())
        return;
      new Telerik.Sitefinity.Security.Web.Services.Permissions().ChangeInheritance(UserFilesConstants.DefaultDownloadableGoodsLibraryId.ToString(), "Telerik.Sitefinity.Libraries.Model.DocumentLibrary", manager.GetType().FullName, "SystemLibrariesProvider", "false", "true", manager.TransactionName);
    }

    private void AddUserFilesMenuItem(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(UserFilesConstants.UserFilesNodeId, "UserFilesNodeTitle").PlaceUnder(CommonNode.System).SetOrdinal(6).LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesModuleTitle").SetUrlNameLocalized("UserFileModuleUrlName").SetDescriptionLocalized("UserFilesModuleDescription").AddChildPage(UserFilesConstants.HomePageId, "UserFilesNodeTitle)").LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesModuleTitle").SetUrlNameLocalized("UserFileModuleUrlName").SetDescriptionLocalized("UserFilesModuleDescription").SetHtmlTitleLocalized("UserFilesModuleTitle").AddContentView(UserFilesDefinitions.BackendUserFilesDefinitionName, "userFilesCntView").Done().AddChildPage(UserFilesConstants.UserFilesDocumentsPageId, "UserFilesDocuments").LocalizeUsing<UserFilesResources>().SetTitleLocalized("UserFilesDocumentsTitle").SetUrlNameLocalized("UserFilesDocumentsUrlName").SetDescriptionLocalized("UserFilesDocumentsDescription").SetHtmlTitleLocalized("UserFilesDocumentsHtmlTitle").AddContentView(UserFilesDefinitions.BackendUserFileLibraryDocumentsDefinitionName, "userFilesDocumentsCntView").Done();

    private void UpgradeFrom1238(SiteInitializer initializer)
    {
      LibrariesManager managerInTransaction = initializer.GetManagerInTransaction<LibrariesManager>();
      foreach (LibrariesDataProvider librariesDataProvider in (Collection<LibrariesDataProvider>) (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection ?? managerInTransaction.StaticProviders))
      {
        IQueryable<Telerik.Sitefinity.Libraries.Model.Document> documents = initializer.GetManagerInTransaction<LibrariesManager>(librariesDataProvider.Name).GetDocuments();
        string[] extensionsToBeChecked = new string[37]
        {
          ".dot",
          ".docx",
          ".dotx",
          ".docm",
          ".dotm",
          ".xls",
          ".xlt",
          ".xla",
          ".xlsx",
          ".xltx",
          ".xlsm",
          ".xltm",
          ".xlam",
          ".xlsb",
          ".pot",
          ".ppa",
          ".pptx",
          ".potx",
          ".ppsx",
          ".ppam",
          ".pptm",
          ".potm",
          ".ppsm",
          ".odb",
          ".odc",
          ".odf",
          ".odg",
          ".odi",
          ".odm",
          ".odp",
          ".ods",
          ".odt",
          ".otg",
          ".oth",
          ".otp",
          ".ots",
          ".ott"
        };
        List<Telerik.Sitefinity.Libraries.Model.Document> documentList = (List<Telerik.Sitefinity.Libraries.Model.Document>) null;
        try
        {
          documentList = documents.Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (d => extensionsToBeChecked.Contains<string>(d.Extension))).ToList<Telerik.Sitefinity.Libraries.Model.Document>();
        }
        catch
        {
        }
        if (documentList != null)
        {
          foreach (Telerik.Sitefinity.Libraries.Model.Document document in documentList)
            document.MimeType = MimeMapping.GetMimeMapping(document.Extension);
        }
        else
        {
          foreach (Telerik.Sitefinity.Libraries.Model.Document document in (IEnumerable<Telerik.Sitefinity.Libraries.Model.Document>) documents)
          {
            string extension = document.Extension;
            if (((IEnumerable<string>) extensionsToBeChecked).Contains<string>(extension))
              document.MimeType = MimeMapping.GetMimeMapping(extension);
          }
        }
      }
    }

    private void UpgradeLibrariesOrdinalCounters(SiteInitializer initializer)
    {
      LibrariesManager managerInTransaction = initializer.GetManagerInTransaction<LibrariesManager>();
      foreach (LibrariesDataProvider librariesDataProvider in (Collection<LibrariesDataProvider>) (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection ?? managerInTransaction.StaticProviders))
      {
        if (librariesDataProvider is OpenAccessLibrariesProvider librariesProvider)
        {
          foreach (Library library in (IEnumerable<Library>) librariesProvider.GetLibraries())
          {
            Guid libraryId = library.Id;
            string name = libraryId.ToString();
            List<float> list = library.Items().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.Parent.Id == libraryId && (int) i.Status == 0)).Select<MediaContent, float>((Expression<Func<MediaContent, float>>) (i => i.Ordinal)).ToList<float>();
            if (list.Count > 0)
            {
              long initialValue = (long) Math.Ceiling((double) list.Max<float>((Func<float, float>) (i => i)));
              librariesProvider.CounterDecorator.InitCounter(name, initialValue);
            }
          }
        }
      }
    }

    private void UpgradeContentDefaultUrls(SiteInitializer initializer)
    {
      foreach (DataProviderBase dataProviderBase in (Collection<LibrariesDataProvider>) (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection ?? LibrariesManager.GetManager().StaticProviders))
      {
        LibrariesManager manager = LibrariesManager.GetManager(dataProviderBase.Name, "sftran_upgrade_defaulturls");
        using (new ElevatedModeRegion((IManager) manager))
        {
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetAlbums().Where<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetVideoLibraries().Where<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetVideos().Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
          initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) manager.GetDocuments().Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (i => string.IsNullOrEmpty((string) i.ItemDefaultUrl))), (IManager) manager);
        }
      }
    }

    /// <summary>Installs the taxonomies.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallTaxonomies(SiteInitializer initializer)
    {
      this.InstallTaxonomy(initializer, typeof (Telerik.Sitefinity.Libraries.Model.Image));
      this.InstallTaxonomy(initializer, typeof (Telerik.Sitefinity.Libraries.Model.Document));
      this.InstallTaxonomy(initializer, typeof (Telerik.Sitefinity.Libraries.Model.Video));
    }

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>();

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallConfiguration(SiteInitializer initializer)
    {
      initializer.Installer.PageToolbox().ContentSection().LoadOrAddWidget<MediaPlayerControl>("MediaPlayerControl").SetTitle("MediaPlayerControlTitle").SetDescription("MediaPlayerControlDescription").LocalizeUsing<PageResources>().SetCssClass("sfVideoIcn").Done().LoadOrAddWidget<VideosView>("VideosView").SetTitle("VideosViewTitle").SetDescription("VideosViewDescription").LocalizeUsing<LibrariesResources>().SetCssClass("sfVideoListIcn").Done().LoadOrAddWidget<DocumentLink>("DocumentLinkControl").SetTitle("DocumentLinkControlTitle").SetDescription("DocumentLinkControlDescription").LocalizeUsing<PageResources>().SetCssClass("sfDownloadLinkIcn").Done().LoadOrAddWidget<DownloadListView>("DownloadListView").SetTitle("DownloadListViewTitle").SetDescription("DownloadListViewDescription").LocalizeUsing<PageResources>().SetCssClass("sfDownloadListIcn").Done();
      this.InitializeSortingExpressionSettings(initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings);
    }

    /// <inheritdoc />
    string IExportableModule.ModuleName => this.Name;

    /// <inheritdoc />
    IList<MetaType> IExportableModule.GetModuleMetaTypes()
    {
      MetadataManager manager = MetadataManager.GetManager();
      return (IList<MetaType>) new MetaType[3]
      {
        manager.GetMetaType(typeof (Telerik.Sitefinity.Libraries.Model.Image)),
        manager.GetMetaType(typeof (Telerik.Sitefinity.Libraries.Model.Video)),
        manager.GetMetaType(typeof (Telerik.Sitefinity.Libraries.Model.Document))
      };
    }

    IDictionary<string, ITypeSettings> ITypeSettingsProvider.GetTypeSettings()
    {
      string fullName = typeof (Library).FullName;
      IDictionary<string, ITypeSettings> contractsInternal = this.GetContractsInternal();
      if (contractsInternal.ContainsKey(fullName))
        contractsInternal.Remove(fullName);
      foreach (KeyValuePair<string, string> keyValuePair in new Dictionary<string, string>()
      {
        {
          typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName,
          typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName
        },
        {
          typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName,
          typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName
        },
        {
          typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName,
          typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName
        }
      })
      {
        if (contractsInternal.ContainsKey(keyValuePair.Key))
        {
          ITypeSettings typeSettings1 = contractsInternal[keyValuePair.Key];
          if (typeSettings1.Properties.FirstOrDefault<IPropertyMapping>((Func<IPropertyMapping, bool>) (x => x.Name == LinqHelper.MemberName<IHasParent>((Expression<Func<IHasParent, object>>) (y => y.Parent)))) is INavigationPropertyMapping navigationPropertyMapping)
            navigationPropertyMapping.Parameters["relatedType"] = keyValuePair.Value;
          if (keyValuePair.Key == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
          {
            CalculatedPropertyMappingProxy propertyMappingProxy = new CalculatedPropertyMappingProxy();
            propertyMappingProxy.Name = "Thumbnails";
            propertyMappingProxy.ResolverType = typeof (ThumbnailProperty).FullName;
            propertyMappingProxy.SelectedByDefault = false;
            CalculatedPropertyMappingProxy thumbnailsProperty = propertyMappingProxy;
            IPropertyMapping propertyMapping = typeSettings1.Properties.FirstOrDefault<IPropertyMapping>((Func<IPropertyMapping, bool>) (p => p.Name == thumbnailsProperty.Name));
            if (propertyMapping != null)
              typeSettings1.Properties.Remove(propertyMapping);
            typeSettings1.Properties.Add((IPropertyMapping) thumbnailsProperty);
          }
          IList<IPropertyMapping> properties1 = typeSettings1.Properties;
          PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
          propertyMappingProxy1.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => (object) x.FolderId));
          PersistentPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
          properties1.Add((IPropertyMapping) propertyMappingProxy2);
          IList<IPropertyMapping> properties2 = typeSettings1.Properties;
          PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
          propertyMappingProxy3.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => (object) x.ParentId));
          PersistentPropertyMappingProxy propertyMappingProxy4 = propertyMappingProxy3;
          properties2.Add((IPropertyMapping) propertyMappingProxy4);
          if (contractsInternal.ContainsKey(keyValuePair.Value))
          {
            ITypeSettings typeSettings2 = contractsInternal[keyValuePair.Value];
            PersistentPropertyMappingProxy propertyMappingProxy5 = new PersistentPropertyMappingProxy();
            propertyMappingProxy5.Name = LinqHelper.MemberName<Library>((Expression<Func<Library, object>>) (x => (object) x.ParentId));
            propertyMappingProxy5.SelectedByDefault = false;
            propertyMappingProxy5.AllowFilter = false;
            propertyMappingProxy5.AllowSort = false;
            PersistentPropertyMappingProxy propertyMappingProxy6 = propertyMappingProxy5;
            typeSettings2.Properties.Add((IPropertyMapping) propertyMappingProxy6);
          }
        }
      }
      return contractsInternal;
    }

    /// <summary>Initializes the sorting expression settings.</summary>
    /// <param name="sortingExpressions"></param>
    protected virtual void InitializeSortingExpressionSettings(
      ConfigElementList<SortingExpressionElement> sortingExpressions)
    {
      this.AddSortingExpression(sortingExpressions, typeof (Telerik.Sitefinity.Libraries.Model.Image));
      this.AddSortingExpression(sortingExpressions, typeof (Telerik.Sitefinity.Libraries.Model.Video));
      this.AddSortingExpression(sortingExpressions, typeof (Telerik.Sitefinity.Libraries.Model.Document));
      this.AddLibrarySortingExpression(sortingExpressions, typeof (Library));
    }

    private void AddSortingExpression(
      ConfigElementList<SortingExpressionElement> sortingExpressions,
      Type contentType)
    {
      ConfigElementList<SortingExpressionElement> configElementList1 = sortingExpressions;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "NewUploadedFirst";
      element1.SortingExpression = "DateCreated DESC";
      configElementList1.Add(element1);
      ConfigElementList<SortingExpressionElement> configElementList2 = sortingExpressions;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "NewModifiedFirst";
      element2.SortingExpression = "LastModified DESC";
      configElementList2.Add(element2);
      ConfigElementList<SortingExpressionElement> configElementList3 = sortingExpressions;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element3.ContentType = contentType.FullName;
      element3.SortingExpressionTitle = "AsManuallyOrdered";
      element3.SortingExpression = "Ordinal";
      configElementList3.Add(element3);
      ConfigElementList<SortingExpressionElement> configElementList4 = sortingExpressions;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element4.ContentType = contentType.FullName;
      element4.SortingExpressionTitle = "CustomSorting";
      element4.SortingExpression = "Custom";
      element4.IsCustom = true;
      configElementList4.Add(element4);
    }

    private void AddLibrarySortingExpression(
      ConfigElementList<SortingExpressionElement> sortingExpressions,
      Type contentType)
    {
      ConfigElementList<SortingExpressionElement> configElementList1 = sortingExpressions;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "LibraryCreatedFirst";
      element1.ResourceClassId = typeof (ImagesResources).Name;
      element1.SortingExpression = "DateCreated DESC";
      configElementList1.Add(element1);
      ConfigElementList<SortingExpressionElement> configElementList2 = sortingExpressions;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "LibraryModifiedFirst";
      element2.ResourceClassId = typeof (ImagesResources).Name;
      element2.SortingExpression = "LastModified DESC";
      configElementList2.Add(element2);
      ConfigElementList<SortingExpressionElement> configElementList3 = sortingExpressions;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element3.ContentType = contentType.FullName;
      element3.SortingExpressionTitle = "ByTitleAsc";
      element3.ResourceClassId = typeof (ImagesResources).Name;
      element3.SortingExpression = "Title ASC";
      configElementList3.Add(element3);
      ConfigElementList<SortingExpressionElement> configElementList4 = sortingExpressions;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element4.ContentType = contentType.FullName;
      element4.SortingExpressionTitle = "ByTitleDesc";
      element4.ResourceClassId = typeof (ImagesResources).Name;
      element4.SortingExpression = "Title DESC";
      configElementList4.Add(element4);
      ConfigElementList<SortingExpressionElement> configElementList5 = sortingExpressions;
      SortingExpressionElement element5 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element5.ContentType = contentType.FullName;
      element5.SortingExpressionTitle = "CustomSorting";
      element5.ResourceClassId = typeof (ImagesResources).Name;
      element5.SortingExpression = "Custom";
      element5.IsCustom = true;
      configElementList5.Add(element5);
    }

    private void RegisterConfigRestrictions() => CommandWidgetReadOnlyConfigRestrictionStrategy.Add("ThumbnailSettings", RestrictionLevel.ReadOnlyConfigFile);

    private void RegisterFilterStrategies() => ObjectFactory.Container.RegisterType(typeof (IFilterStrategy), typeof (MediaFilterStrategy), typeof (MediaFilterStrategy).Name, (LifetimeManager) new ContainerControlledLifetimeManager());

    public object GetReport()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      HashSet<string> values1 = new HashSet<string>();
      HashSet<string> values2 = new HashSet<string>();
      HashSet<string> values3 = new HashSet<string>();
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      Dictionary<string, int> imageThumbnailProfiles = new Dictionary<string, int>();
      bool flag = false;
      Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>().Images.Thumbnails.Profiles.Values.ToList<ThumbnailProfileConfigElement>().ForEach((Action<ThumbnailProfileConfigElement>) (profile => imageThumbnailProfiles.Add(profile.Name, 0)));
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      dictionary2.Add("Less than 25 MB", 0);
      dictionary2.Add("Between 25 and 100 MB", 0);
      dictionary2.Add("Between 100 and 500 MB", 0);
      dictionary2.Add("Between 500 MB and 1 GB", 0);
      dictionary2.Add("Greater than 1GB", 0);
      foreach (string providerName in LibrariesManager.GetManager().GetProviderNames(ProviderBindingOptions.SkipSystem))
      {
        LibrariesManager manager = LibrariesManager.GetManager(providerName);
        flag = flag || manager.Provider.FilterQueriesByViewPermissions;
        IQueryable<Folder> allFolders = manager.GetAllFolders();
        IQueryable<Telerik.Sitefinity.Libraries.Model.Album> albums = manager.GetAlbums();
        HashSet<Guid?> imageLibrariesGuids = albums.Select<Telerik.Sitefinity.Libraries.Model.Album, Guid?>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, Guid?>>) (x => (Guid?) x.Id)).ToHashSet<Guid?>();
        IQueryable<IFolder> source1 = (IQueryable<IFolder>) allFolders.Where<Folder>((Expression<Func<Folder, bool>>) (x => imageLibrariesGuids.Contains(x.ParentId) || imageLibrariesGuids.Contains((Guid?) x.RootId)));
        num1 += manager.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (img => (int) img.Status == 0)).Count<Telerik.Sitefinity.Libraries.Model.Image>();
        num2 += albums.Count<Telerik.Sitefinity.Libraries.Model.Album>();
        num3 += source1.Count<IFolder>();
        values1.UnionWith((IEnumerable<string>) albums.Select<Telerik.Sitefinity.Libraries.Model.Album, string>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, string>>) (album => album.BlobStorageProvider)).ToHashSet<string>());
        IQueryable<string> source2 = albums.SelectMany<Telerik.Sitefinity.Libraries.Model.Album, string>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, IEnumerable<string>>>) (album => album.ThumbnailProfiles));
        Expression<Func<string, string>> keySelector = (Expression<Func<string, string>>) (profile => profile);
        foreach (IGrouping<string, string> source3 in (IEnumerable<IGrouping<string, string>>) source2.GroupBy<string, string>(keySelector))
          imageThumbnailProfiles[source3.Key] += source3.Count<string>();
        IQueryable<Telerik.Sitefinity.Libraries.Model.VideoLibrary> videoLibraries = manager.GetVideoLibraries();
        HashSet<Guid?> videoLibrariesGuids = videoLibraries.Select<Telerik.Sitefinity.Libraries.Model.VideoLibrary, Guid?>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, Guid?>>) (x => (Guid?) x.Id)).ToHashSet<Guid?>();
        IQueryable<IFolder> source4 = (IQueryable<IFolder>) allFolders.Where<Folder>((Expression<Func<Folder, bool>>) (x => videoLibrariesGuids.Contains(x.ParentId) || videoLibrariesGuids.Contains((Guid?) x.RootId)));
        IQueryable<Telerik.Sitefinity.Libraries.Model.Video> source5 = manager.GetVideos().Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (video => (int) video.Status == 0));
        num4 += source5.Count<Telerik.Sitefinity.Libraries.Model.Video>();
        num5 += videoLibraries.Count<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
        num6 += source4.Count<IFolder>();
        values2.UnionWith((IEnumerable<string>) videoLibraries.Select<Telerik.Sitefinity.Libraries.Model.VideoLibrary, string>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, string>>) (album => album.BlobStorageProvider)).ToHashSet<string>());
        Func<long, string> getSizeGroup = (Func<long, string>) (size =>
        {
          if (size <= 26214400L)
            return "Less than 25 MB";
          if (size > 26214400L && size <= 104857600L)
            return "Between 25 and 100 MB";
          if (size > 104857600L && size <= 524288000L)
            return "Between 100 and 500 MB";
          return size > 524288000L && size <= 1073741824L ? "Between 500 MB and 1 GB" : "Greater than 1GB";
        });
        IQueryable<Telerik.Sitefinity.Libraries.Model.Video> source6 = source5;
        Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, IEnumerable<MediaFileLink>>> selector1 = (Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, IEnumerable<MediaFileLink>>>) (video => video.MediaFileLinks);
        foreach (IGrouping<string, long> source7 in source6.SelectMany<Telerik.Sitefinity.Libraries.Model.Video, MediaFileLink>(selector1).AsEnumerable<MediaFileLink>().GroupBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (mfl => mfl.FileId)).Select<IGrouping<Guid, MediaFileLink>, long>((Func<IGrouping<Guid, MediaFileLink>, long>) (g => g.First<MediaFileLink>().TotalSize)).GroupBy<long, string>((Func<long, string>) (size => getSizeGroup(size))))
          dictionary2[source7.Key] += source7.Count<long>();
        IQueryable<Telerik.Sitefinity.Libraries.Model.DocumentLibrary> documentLibraries = manager.GetDocumentLibraries();
        HashSet<Guid?> documentLibrariesGuids = documentLibraries.Select<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, Guid?>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, Guid?>>) (x => (Guid?) x.Id)).ToHashSet<Guid?>();
        IQueryable<IFolder> source8 = (IQueryable<IFolder>) allFolders.Where<Folder>((Expression<Func<Folder, bool>>) (x => documentLibrariesGuids.Contains(x.ParentId) || documentLibrariesGuids.Contains((Guid?) x.RootId)));
        IQueryable<Telerik.Sitefinity.Libraries.Model.Document> source9 = manager.GetDocuments().Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (doc => (int) doc.Status == 0));
        num7 += source9.Count<Telerik.Sitefinity.Libraries.Model.Document>();
        num8 += documentLibraries.Count<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
        num9 += source8.Count<IFolder>();
        values3.UnionWith((IEnumerable<string>) documentLibraries.Select<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, string>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, string>>) (album => album.BlobStorageProvider)).ToHashSet<string>());
        IQueryable<Telerik.Sitefinity.Libraries.Model.Document> source10 = source9;
        Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, IEnumerable<MediaFileLink>>> selector2 = (Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, IEnumerable<MediaFileLink>>>) (doc => doc.MediaFileLinks);
        foreach (IGrouping<string, string> source11 in source10.SelectMany<Telerik.Sitefinity.Libraries.Model.Document, MediaFileLink>(selector2).AsEnumerable<MediaFileLink>().GroupBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (mfl => mfl.FileId)).Select<IGrouping<Guid, MediaFileLink>, string>((Func<IGrouping<Guid, MediaFileLink>, string>) (g => g.First<MediaFileLink>().Extension)).GroupBy<string, string>((Func<string, string>) (ext => ext)))
        {
          if (dictionary1.ContainsKey(source11.Key))
            dictionary1[source11.Key] += source11.Count<string>();
          else
            dictionary1[source11.Key] = source11.Count<string>();
        }
      }
      return (object) new LibrariesModuleReport()
      {
        ModuleName = "Libraries",
        ImagesCount = num1,
        ImageLibrariesCount = num2,
        ImageFoldersCount = num3,
        VideosCount = num4,
        VideoLibrariesCount = num5,
        VideoFoldersCount = num6,
        DocumentsCount = num7,
        DocumentLibrariesCount = num8,
        DocumentFoldersCount = num9,
        ImageStorageProviders = string.Join(",", (IEnumerable<string>) values1),
        VideoStorageProviders = string.Join(",", (IEnumerable<string>) values2),
        DocumentStorageProviders = string.Join(",", (IEnumerable<string>) values3),
        ImagesThumbnailProfilesAppliedTo = (IDictionary<string, int>) imageThumbnailProfiles,
        VideoSizes = (IDictionary<string, int>) dictionary2,
        DocumentExtensions = (IDictionary<string, int>) dictionary1,
        FilterQueriesByViewPermissions = flag
      };
    }
  }
}
