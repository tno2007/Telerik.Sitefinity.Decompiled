// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("LibrariesResources", ResourceClassId = "LibrariesResources")]
  public class LibrariesResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesResources" /> class.
    /// </summary>
    public LibrariesResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public LibrariesResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Library</summary>
    [ResourceEntry("LibrariesResourcesTitle", Description = "The title of this class.", LastModified = "2010/03/16", Value = "Library")]
    public string LibrariesResourcesTitle => this[nameof (LibrariesResourcesTitle)];

    /// <summary>Libraries</summary>
    [ResourceEntry("LibrariesResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2010/03/16", Value = "Libraries")]
    public string LibrariesResourcesTitlePlural => this[nameof (LibrariesResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Libraries user interface.
    /// </summary>
    [ResourceEntry("LibrariesResourcesDescription", Description = "The description of this class.", LastModified = "2010/03/16", Value = "Contains localizable resources for Libraries user interface.")]
    public string LibrariesResourcesDescription => this[nameof (LibrariesResourcesDescription)];

    /// <summary>word: Database</summary>
    [ResourceEntry("Database", Description = "word: Database", LastModified = "2011/06/29", Value = "Database")]
    public string Database => this[nameof (Database)];

    /// <summary>phrase: File System</summary>
    [ResourceEntry("FileSystem", Description = "phrase: File System", LastModified = "2011/06/29", Value = "File System")]
    public string FileSystem => this[nameof (FileSystem)];

    /// <summary>phrase: Provider name</summary>
    [ResourceEntry("ProviderName", Description = "phrase: Provider name", LastModified = "2011/06/29", Value = "Provider name")]
    public string ProviderName => this[nameof (ProviderName)];

    /// <summary>phrase: Provider name cannot be empty.</summary>
    [ResourceEntry("ProviderNameCannotBeEmpty", Description = "phrase: Provider name cannot be empty.", LastModified = "2011/06/30", Value = "Provider name cannot be empty.")]
    public string ProviderNameCannotBeEmpty => this[nameof (ProviderNameCannotBeEmpty)];

    /// <summary>phrase: Provider name '{0}' already used.</summary>
    [ResourceEntry("ProviderNameAlreadyUsed", Description = "phrase: Provider name '{0}' already used.", LastModified = "2011/07/18", Value = "Provider name '{0}' already used.")]
    public string ProviderNameAlreadyUsed => this[nameof (ProviderNameAlreadyUsed)];

    /// <summary>phrase: Provider type</summary>
    [ResourceEntry("ProviderType", Description = "phrase: Provider type", LastModified = "2011/06/29", Value = "Provider type")]
    public string ProviderType => this[nameof (ProviderType)];

    /// <summary>
    /// Translated message, similar to 'You are not authorized to view this media item'
    /// </summary>
    /// <value>Error message displayed when returning 403 if an image cannot be displayed</value>
    [ResourceEntry("NotAuthorizedToViewMediaItem", Description = "Error message displayed when returning 403 if an image cannot be displayed", LastModified = "2011/02/16", Value = "You are not authorized to view this media item")]
    public string NotAuthorizedToViewMediaItem => this[nameof (NotAuthorizedToViewMediaItem)];

    /// <summary>Publish</summary>
    [ResourceEntry("Publish", Description = "Label of the publish action.", LastModified = "2010/07/29", Value = "Publish")]
    public string Publish => this[nameof (Publish)];

    /// <summary>word: Unpublish</summary>
    [ResourceEntry("Unpublish", Description = "word: Unpublish", LastModified = "2010/08/03", Value = "Unpublish")]
    public string Unpublish => this[nameof (Unpublish)];

    /// <summary>Libraries</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Libraries module.", LastModified = "2010/03/16", Value = "Libraries")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>phrase: Which {0} to upload?</summary>
    [ResourceEntry("WhichItemsToUpload", Description = "phrase: Which {0} to upload?", LastModified = "2010/03/16", Value = "Which {0} to upload?")]
    public string WhichItemsToUpload => this[nameof (WhichItemsToUpload)];

    /// <summary>phrase: Select {0}</summary>
    [ResourceEntry("SelectItems", Description = "phrase: Select {0}", LastModified = "2010/03/16", Value = "Select {0}")]
    public string SelectItems => this[nameof (SelectItems)];

    /// <summary>phrase: Multiple {0} can be selected at once.</summary>
    [ResourceEntry("MultipleItemsCanBeSelectedAtOnce", Description = "phrase: Multiple {0} can be selected at once.", LastModified = "2010/03/16", Value = "Multiple {0} can be selected at once.")]
    public string MultipleItemsCanBeSelectedAtOnce => this[nameof (MultipleItemsCanBeSelectedAtOnce)];

    /// <summary>
    /// Provides user interface for managing images, documents and videos.
    /// </summary>
    [ResourceEntry("ModuleDescription", Description = "The description of the Libraries module", LastModified = "2010/03/17", Value = "Provides user interface for managing images, documents and videos.")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>Messsage: Images</summary>
    /// <value>Title of the images page group.</value>
    [ResourceEntry("ImagesPageGroupNodeTitle", Description = "Title of the images page group.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesPageGroupNodeTitle => this[nameof (ImagesPageGroupNodeTitle)];

    /// <summary>
    /// Messsage: This is the page group that contains all pages for the Images module.
    /// </summary>
    /// <value>Images Page Group Description</value>
    [ResourceEntry("ImagesPageGroupNodeDescription", Description = "Images Page Group Description", LastModified = "2010/03/16", Value = "This is the page group that contains all pages for the Images module.")]
    public string ImagesPageGroupNodeDescription => this[nameof (ImagesPageGroupNodeDescription)];

    /// <summary>Messsage: Documents</summary>
    /// <value>Title of the documents page group.</value>
    [ResourceEntry("DocumentsPageGroupNodeTitle", Description = "Title of the documents page group.", LastModified = "2010/03/16", Value = "Documents & Files")]
    public string DocumentsPageGroupNodeTitle => this[nameof (DocumentsPageGroupNodeTitle)];

    /// <summary>Messsage: Documents</summary>
    /// <value>Url of the documents page group.</value>
    [ResourceEntry("DocumentsPageGroupNodeUrl", Description = "Url of the documents page group.", LastModified = "2010/03/16", Value = "Documents")]
    public string DocumentsPageGroupNodeUrl => this[nameof (DocumentsPageGroupNodeUrl)];

    /// <summary>
    /// Messsage: This is the page group that contains all pages for the Documents module.
    /// </summary>
    /// <value>Documents Page Group Description</value>
    [ResourceEntry("DocumentsPageGroupNodeDescription", Description = "Documents & Files Page Group Description", LastModified = "2010/03/16", Value = "This is the page group that contains all pages for the Documents & Files module.")]
    public string DocumentsPageGroupNodeDescription => this[nameof (DocumentsPageGroupNodeDescription)];

    /// <summary>Messsage: Videos</summary>
    /// <value>Title of the videos page group.</value>
    [ResourceEntry("VideosPageGroupNodeTitle", Description = "Title of the videos page group.", LastModified = "2010/03/16", Value = "Videos")]
    public string VideosPageGroupNodeTitle => this[nameof (VideosPageGroupNodeTitle)];

    /// <summary>
    /// Messsage: This is the page group that contains all pages for the Videos module.
    /// </summary>
    /// <value>Videos Page Group Description</value>
    [ResourceEntry("VideosPageGroupNodeDescription", Description = "Videos Page Group Description", LastModified = "2010/03/16", Value = "This is the page group that contains all pages for the Videos module.")]
    public string VideosPageGroupNodeDescription => this[nameof (VideosPageGroupNodeDescription)];

    /// <summary>Messsage: ImagesTitle</summary>
    [ResourceEntry("ImagesTitle", Description = "Title of the Images page.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesTitle => this[nameof (ImagesTitle)];

    /// <summary>Messsage: ImagesHtmlTitle</summary>
    [ResourceEntry("ImagesHtmlTitle", Description = "Html Title of the Images page.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesHtmlTitle => this[nameof (ImagesHtmlTitle)];

    /// <summary>Messsage: Images</summary>
    [ResourceEntry("ImagesUrlName", Description = "Images page URL.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesUrlName => this[nameof (ImagesUrlName)];

    /// <summary>Messsage: Image Files</summary>
    [ResourceEntry("ImageFiles", Description = "ImageFiles.", LastModified = "2010/03/16", Value = "Image Files")]
    public string ImageFiles => this[nameof (ImageFiles)];

    /// <summary>Messsage: Video Files</summary>
    [ResourceEntry("VideoFiles", Description = "VideoFiles.", LastModified = "2010/03/16", Value = "Video Files")]
    public string VideoFiles => this[nameof (VideoFiles)];

    /// <summary>Messsage: Images Description</summary>
    [ResourceEntry("ImagesDescription", Description = "Description of the Images page", LastModified = "2010/03/16", Value = "Images Description")]
    public string ImagesDescription => this[nameof (ImagesDescription)];

    /// <summary>Word: Images</summary>
    [ResourceEntry("ImagesViewTitle", Description = "The title of the ImagesView control, that appears on the controls toolbox.", LastModified = "2010/04/08", Value = "Image gallery")]
    public string ImagesViewTitle => this[nameof (ImagesViewTitle)];

    /// <summary>
    /// Displays images in variety of ways, such as thumbnail list, thumbnail strip and detailed views.
    /// </summary>
    [ResourceEntry("ImagesViewDescription", Description = "The description of the ImagesView control, that appears on the controls toolbox.", LastModified = "2010/04/08", Value = "Images from a library, displayed in various ways")]
    public string ImagesViewDescription => this[nameof (ImagesViewDescription)];

    /// <summary>Phrase: Video gallery</summary>
    [ResourceEntry("VideosViewTitle", Description = "The title of the VideosView control, that appears on the controls toolbox.", LastModified = "2010/06/08", Value = "Video gallery")]
    public string VideosViewTitle => this[nameof (VideosViewTitle)];

    /// <summary>
    /// Displays videos in variety of ways, such as thumbnails list and detailed views.
    /// </summary>
    [ResourceEntry("VideosViewDescription", Description = "The description of the ImagesView control, that appears on the controls toolbox.", LastModified = "2010/06/08", Value = "Videos from a library")]
    public string VideosViewDescription => this[nameof (VideosViewDescription)];

    /// <summary>Messsage: DocumentsTitle</summary>
    [ResourceEntry("DocumentsTitle", Description = "Title of the Documents & Files page.", LastModified = "2010/03/16", Value = "Documents & Files")]
    public string DocumentsTitle => this[nameof (DocumentsTitle)];

    /// <summary>Messsage: DocumentsHtmlTitle</summary>
    [ResourceEntry("DocumentsHtmlTitle", Description = "Html Title of the Documents & Files page.", LastModified = "2010/03/16", Value = "Documents")]
    public string DocumentsHtmlTitle => this[nameof (DocumentsHtmlTitle)];

    /// <summary>Messsage: Documents</summary>
    [ResourceEntry("DocumentsUrlName", Description = "Documents page URL.", LastModified = "2010/03/16", Value = "Documents")]
    public string DocumentsUrlName => this[nameof (DocumentsUrlName)];

    /// <summary>Messsage: Manage Documents</summary>
    [ResourceEntry("DocumentsDescription", Description = "Description of the Documents page", LastModified = "2010/03/16", Value = "Manage Documents")]
    public string DocumentsDescription => this[nameof (DocumentsDescription)];

    /// <summary>Messsage: VideosTitle</summary>
    [ResourceEntry("VideosTitle", Description = "Title of the Videos page.", LastModified = "2010/03/16", Value = "Videos")]
    public string VideosTitle => this[nameof (VideosTitle)];

    /// <summary>Messsage: VideosHtmlTitle</summary>
    [ResourceEntry("VideosHtmlTitle", Description = "Html Title of the Videos page.", LastModified = "2010/03/16", Value = "Videos")]
    public string VideosHtmlTitle => this[nameof (VideosHtmlTitle)];

    /// <summary>Messsage: Videos Description</summary>
    [ResourceEntry("VideosDescription", Description = "Description of the Videos page", LastModified = "2010/05/27", Value = "Videos Description")]
    public string VideosDescription => this[nameof (VideosDescription)];

    /// <summary>Messsage: Videos</summary>
    [ResourceEntry("VideosUrlName", Description = "Videos page URL.", LastModified = "2010/03/16", Value = "Videos")]
    public string VideosUrlName => this[nameof (VideosUrlName)];

    /// <summary>Messsage: LibraryVideosTitle</summary>
    [ResourceEntry("LibraryVideosTitle", Description = "Title of the Libraries at Videos page.", LastModified = "2010/03/16", Value = "Library Videos")]
    public string LibraryVideosTitle => this[nameof (LibraryVideosTitle)];

    /// <summary>Messsage: LibraryVideosHtmlTitle</summary>
    [ResourceEntry("LibraryVideosHtmlTitle", Description = "Html Title of the LibraryVideos page.", LastModified = "2010/03/16", Value = "Library Videos")]
    public string LibraryVideosHtmlTitle => this[nameof (LibraryVideosHtmlTitle)];

    /// <summary>Messsage: Videos page for selected library.</summary>
    [ResourceEntry("LibraryVideosUrlName", Description = "Videos page for selected library.", LastModified = "2010/05/27", Value = "LibraryVideos")]
    public string LibraryVideosUrlName => this[nameof (LibraryVideosUrlName)];

    /// <summary>Messsage: LibraryVideos Description</summary>
    [ResourceEntry("LibraryVideosDescription", Description = "Description of the LibraryVideos page", LastModified = "2010/03/16", Value = "Library Videos Description")]
    public string LibraryVideosDescription => this[nameof (LibraryVideosDescription)];

    /// <summary>Messsage: Libraries</summary>
    [ResourceEntry("LibrariesTitle", Description = "Title of the Libraries page.", LastModified = "2010/05/27", Value = "Libraries")]
    public string LibrariesTitle => this[nameof (LibrariesTitle)];

    /// <summary>Messsage: Libraries</summary>
    [ResourceEntry("LibrariesHtmlTitle", Description = "Html Title of the Libraries page.", LastModified = "2010/05/27", Value = "Libraries")]
    public string LibrariesHtmlTitle => this[nameof (LibrariesHtmlTitle)];

    /// <summary>Messsage: Libraries</summary>
    [ResourceEntry("LibrariesUrlName", Description = "Libraries page URL.", LastModified = "2010/05/27", Value = "Libraries")]
    public string LibrariesUrlName => this[nameof (LibrariesUrlName)];

    /// <summary>Messsage: Libraries description</summary>
    [ResourceEntry("LibrariesDescription", Description = "Description of the Libraries page", LastModified = "2010/05/27", Value = "Libraries description")]
    public string LibrariesDescription => this[nameof (LibrariesDescription)];

    /// <summary>Messsage: LibraryDocumentsTitle</summary>
    [ResourceEntry("LibraryDocumentsTitle", Description = "Title of the Libraries at Documents page.", LastModified = "2010/03/16", Value = "Library Documents")]
    public string LibraryDocumentsTitle => this[nameof (LibraryDocumentsTitle)];

    /// <summary>Messsage: LibraryDocumentsHtmlTitle</summary>
    [ResourceEntry("LibraryDocumentsHtmlTitle", Description = "Html Title of the LibraryDocuments page.", LastModified = "2010/03/16", Value = "Library Documents")]
    public string LibraryDocumentsHtmlTitle => this[nameof (LibraryDocumentsHtmlTitle)];

    /// <summary>Messsage: Documents page for selected library.</summary>
    [ResourceEntry("LibraryDocumentsUrlName", Description = "Documents page for selected library.", LastModified = "2010/05/27", Value = "LibraryDocuments")]
    public string LibraryDocumentsUrlName => this[nameof (LibraryDocumentsUrlName)];

    /// <summary>Messsage: LibraryDocuments Description</summary>
    [ResourceEntry("LibraryDocumentsDescription", Description = "Description of the LibraryDocuments page", LastModified = "2010/03/16", Value = "Library Documents Description")]
    public string LibraryDocumentsDescription => this[nameof (LibraryDocumentsDescription)];

    /// <summary>Messsage: Library Images</summary>
    [ResourceEntry("LibraryImagesTitle", Description = "Title of the LibraryImages page.", LastModified = "2010/03/23", Value = "Library Images")]
    public string LibraryImagesTitle => this[nameof (LibraryImagesTitle)];

    /// <summary>Messsage: Library Images</summary>
    [ResourceEntry("LibraryImagesHtmlTitle", Description = "Html Title of the LibraryImages page.", LastModified = "2010/03/23", Value = "Library Images")]
    public string LibraryImagesHtmlTitle => this[nameof (LibraryImagesHtmlTitle)];

    /// <summary>Messsage: LibraryImages</summary>
    [ResourceEntry("LibraryImagesUrlName", Description = "LibraryImages page URL.", LastModified = "2010/03/23", Value = "LibraryImages")]
    public string LibraryImagesUrlName => this[nameof (LibraryImagesUrlName)];

    /// <summary>Messsage: Manage Images</summary>
    [ResourceEntry("LibraryImagesDescription", Description = "Description of the LibraryImages page", LastModified = "2010/03/23", Value = "Manage Images")]
    public string LibraryImagesDescription => this[nameof (LibraryImagesDescription)];

    /// <summary>Messsage: Libraries</summary>
    [ResourceEntry("AlbumsTitle", Description = "Title of the Image Libraries page.", LastModified = "2010/07/13", Value = "Libraries")]
    public string AlbumsTitle => this[nameof (AlbumsTitle)];

    /// <summary>Messsage: Libraries</summary>
    [ResourceEntry("AlbumsHtmlTitle", Description = "Html Title of the Image Libraries page.", LastModified = "2010/07/13", Value = "Libraries")]
    public string AlbumsHtmlTitle => this[nameof (AlbumsHtmlTitle)];

    /// <summary>Messsage: Albums</summary>
    [ResourceEntry("AlbumsUrlName", Description = "Albums page URL.", LastModified = "2010/04/09", Value = "Albums")]
    public string AlbumsUrlName => this[nameof (AlbumsUrlName)];

    /// <summary>Messsage: Manage Image Libraries</summary>
    [ResourceEntry("AlbumsDescription", Description = "Description of the Image Libraries page", LastModified = "2010/07/13", Value = "Manage Image Libraries")]
    public string AlbumsDescription => this[nameof (AlbumsDescription)];

    /// <summary>phrase: Where to store uploaded {0}?</summary>
    [ResourceEntry("WhereToStoreUploadedItems", Description = "phrase: Where to store the uploaded {0}?", LastModified = "2010/03/25", Value = "Where to store the uploaded {0}?")]
    public string WhereToStoreUploadedItems => this[nameof (WhereToStoreUploadedItems)];

    /// <summary>
    /// phrase: You can't upload {0} files. Only {1} can be uploaded
    /// </summary>
    [ResourceEntry("CantUploadFiles", Description = "phrase: You can't upload {0} files. Only {1} can be uploaded", LastModified = "2010/03/25", Value = "You can't upload {0} files. Only {1} can be uploaded")]
    public string CantUploadFiles => this[nameof (CantUploadFiles)];

    /// <summary>
    /// Message displayed on attempt to insert a file with size larger than allowed.
    /// </summary>
    [ResourceEntry("InvalidFileSizeAlertMessage", Description = " Message displayed on attempt to insert a file with size larger than allowed.", LastModified = "2014/02/04", Value = "The selected file is larger than maximum allowed size ({0} kB)")]
    public string InvalidFileSizeAlertMessage => this[nameof (InvalidFileSizeAlertMessage)];

    /// <summary>
    /// phrase: You must select the library in which the files ought to be uploaded.
    /// </summary>
    [ResourceEntry("LibraryNotSelected", Description = "phrase: You must select the library in which the files ought to be uploaded.", LastModified = "2012/09/27", Value = "You must select the library in which the files ought to be uploaded.")]
    public string LibraryNotSelected => this[nameof (LibraryNotSelected)];

    /// <summary>phrase: Images - list</summary>
    [ResourceEntry("ImagesMasterThumbnailViewFriendlyName", Description = "phrase: Images - list", LastModified = "2010/11/12", Value = "Images - list")]
    public string ImagesMasterThumbnailViewFriendlyName => this[nameof (ImagesMasterThumbnailViewFriendlyName)];

    /// <summary>phrase: Images - strip</summary>
    [ResourceEntry("ImagesMasterThumbnailStripViewFriendlyName", Description = "phrase: Images - strip", LastModified = "2010/11/12", Value = "Images - strip")]
    public string ImagesMasterThumbnailStripViewFriendlyName => this[nameof (ImagesMasterThumbnailStripViewFriendlyName)];

    /// <summary>phrase: Images - simple list</summary>
    [ResourceEntry("ImagesMasterThumbnailSimpleViewFriendlyName", Description = "phrase: Images - simple list", LastModified = "2010/11/12", Value = "Images - simple list")]
    public string ImagesMasterThumbnailSimpleViewFriendlyName => this[nameof (ImagesMasterThumbnailSimpleViewFriendlyName)];

    /// <summary>phrase: Images - lightbox</summary>
    [ResourceEntry("ImagesMasterThumbnailLightboxViewFriendlyName", Description = "phrase: Images - lightbox", LastModified = "2010/11/12", Value = "Images - lightbox")]
    public string ImagesMasterThumbnailLightboxViewFriendlyName => this[nameof (ImagesMasterThumbnailLightboxViewFriendlyName)];

    /// <summary>phrase: Images - single</summary>
    [ResourceEntry("ImagesDetailSimpleViewFriendlyName", Description = "phrase: Images - single", LastModified = "2010/11/12", Value = "Images - single")]
    public string ImagesDetailSimpleViewFriendlyName => this[nameof (ImagesDetailSimpleViewFriendlyName)];

    /// <summary>phrase: Videos - list</summary>
    [ResourceEntry("VideosMasterThumbnailViewFriendlyName", Description = "phrase: Videos - list", LastModified = "2010/11/12", Value = "Videos - list")]
    public string VideosMasterThumbnailViewFriendlyName => this[nameof (VideosMasterThumbnailViewFriendlyName)];

    /// <summary>phrase: Videos - lightbox</summary>
    [ResourceEntry("VideosMasterThumbnailLightboxViewFriendlyName", Description = "phrase: Videos - lightbox", LastModified = "2010/11/12", Value = "Videos - lightbox")]
    public string VideosMasterThumbnailLightboxViewFriendlyName => this[nameof (VideosMasterThumbnailLightboxViewFriendlyName)];

    /// <summary>phrase: Videos - single</summary>
    [ResourceEntry("VideosDetailViewFriendlyName", Description = "phrase: Videos - single", LastModified = "2010/11/12", Value = "Videos - single")]
    public string VideosDetailViewFriendlyName => this[nameof (VideosDetailViewFriendlyName)];

    /// <summary>phrase: Documents - list</summary>
    [ResourceEntry("DocumentsMasterListViewFriendlyName", Description = "phrase: Documents - list", LastModified = "2010/11/12", Value = "Documents - list")]
    public string DocumentsMasterListViewFriendlyName => this[nameof (DocumentsMasterListViewFriendlyName)];

    /// <summary>phrase: Documents - table</summary>
    [ResourceEntry("DocumentsMasterTableViewFriendlyName", Description = "phrase: Documents - table", LastModified = "2010/11/12", Value = "Documents - table")]
    public string DocumentsMasterTableViewFriendlyName => this[nameof (DocumentsMasterTableViewFriendlyName)];

    /// <summary>phrase: Documents - list details</summary>
    [ResourceEntry("DocumentsListDetailViewFriendlyName", Description = "phrase: Documents - list details", LastModified = "2010/11/12", Value = "Documents - list details")]
    public string DocumentsListDetailViewFriendlyName => this[nameof (DocumentsListDetailViewFriendlyName)];

    /// <summary>phrase: Documents - table details</summary>
    [ResourceEntry("DocumentsTableDetailViewFriendlyName", Description = "phrase: Documents - table details", LastModified = "2010/11/12", Value = "Documents - table details")]
    public string DocumentsTableDetailViewFriendlyName => this[nameof (DocumentsTableDetailViewFriendlyName)];

    /// <summary>phrase: Documents - single</summary>
    [ResourceEntry("DocumentsDetailViewFriendlyName", Description = "phrase: Documents - single", LastModified = "2010/11/12", Value = "Documents - single")]
    public string DocumentsDetailViewFriendlyName => this[nameof (DocumentsDetailViewFriendlyName)];

    /// <summary>phrase: Parent library</summary>
    [ResourceEntry("ParentLibrary", Description = "phrase: Parent library", LastModified = "2013/02/21", Value = "Parent library")]
    public string ParentLibrary => this[nameof (ParentLibrary)];

    /// <summary>phrase: No parent (this library is on top level)</summary>
    [ResourceEntry("NoParentLib", Description = "phrase: No parent (this library is on top level)", LastModified = "2013/02/21", Value = "No parent (this library is on top level)")]
    public string NoParentLib => this[nameof (NoParentLib)];

    /// <summary>phrase: Selected parent library...</summary>
    [ResourceEntry("SelectedParentLib", Description = "phrase: Selected parent library...", LastModified = "2013/02/21", Value = "Selected parent library...")]
    public string SelectedParentLib => this[nameof (SelectedParentLib)];

    /// <summary>Phrase: Aspect ratio</summary>
    /// <value>Aspect ratio</value>
    [ResourceEntry("AspectRatio", Description = "Phrase: Aspect ratio", LastModified = "2014/02/13", Value = "Aspect ratio")]
    public string AspectRatio => this[nameof (AspectRatio)];

    /// <summary>Phrase: Media Player Control</summary>
    /// <value>Media Player Control</value>
    [ResourceEntry("Html5MediaPlayerControlFriendlyName", Description = "Phrase: Media Player Control", LastModified = "2014/03/27", Value = "Media Player Control")]
    public string Html5MediaPlayerControlFriendlyName => this[nameof (Html5MediaPlayerControlFriendlyName)];

    /// <summary>Word: Auto</summary>
    /// <value>Auto</value>
    [ResourceEntry("AutoLabel", Description = "Word: Auto", LastModified = "2014/03/28", Value = "Auto")]
    public string AutoLabel => this[nameof (AutoLabel)];

    /// <summary>Word: Custom</summary>
    /// <value>Custom</value>
    [ResourceEntry("CustomLabel", Description = "Word: Custom", LastModified = "2014/03/28", Value = "Custom")]
    public string CustomLabel => this[nameof (CustomLabel)];

    /// <summary>Upload</summary>
    [ResourceEntry("Upload", Description = "The text of the upload button in grid toolbar.", LastModified = "2010/03/17", Value = "Upload")]
    public string Upload => this[nameof (Upload)];

    /// <summary>Upload video</summary>
    /// <value>Upload video</value>
    [ResourceEntry("Uploadvideo", Description = "UploadVideo", LastModified = "2014/04/07", Value = "Upload video")]
    public string UploadVideo => this["Uploadvideo"];

    /// <summary>Upload  image</summary>
    [ResourceEntry("UploadImage", Description = "UploadImage", LastModified = "2014/01/24", Value = "Upload image")]
    public string UploadImage => this[nameof (UploadImage)];

    /// <summary>Upload  document</summary>
    [ResourceEntry("UploadDocument", Description = "UploadDocument", LastModified = "2014/02/10", Value = "Upload document")]
    public string UploadDocument => this[nameof (UploadDocument)];

    /// <summary>word: Delete</summary>
    [ResourceEntry("Delete", Description = "The text of the delete button.", LastModified = "2010/03/17", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>phrase: Delete this library</summary>
    [ResourceEntry("DeleteThisLibrary", Description = "The text of the delete button.", LastModified = "2013/03/27", Value = "Delete this library")]
    public string DeleteThisLibrary => this[nameof (DeleteThisLibrary)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "The text of the cancel button.", LastModified = "2010/03/30", Value = "(Cancel)")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>More actions</summary>
    [ResourceEntry("MoreActions", Description = "The text of the more action menu in grid toolbar.", LastModified = "2010/03/17", Value = "More actions")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>Bulk edit titles, categories, tags</summary>
    [ResourceEntry("BulkEdit", Description = "The text of the bulk edit button in the action menu.", LastModified = "2010/03/17", Value = "<strong>Bulk edit</strong> titles, categories, tags")]
    public string BulkEdit => this[nameof (BulkEdit)];

    /// <summary>Other filter options</summary>
    [ResourceEntry("OtherFilterOptions", Description = "The 'Other filter options' label in the sidebar.", LastModified = "2010/03/17", Value = "Other filter options")]
    public string OtherFilterOptions => this[nameof (OtherFilterOptions)];

    /// <summary>By Date</summary>
    [ResourceEntry("ByDate", Description = "The link for filtering by date in the sidebar.", LastModified = "2010/03/17", Value = "by Date...")]
    public string ByDate => this[nameof (ByDate)];

    /// <summary>By Author</summary>
    [ResourceEntry("ByAuthor", Description = "The link for filtering by author in the sidebar.", LastModified = "2010/03/17", Value = "by Author...")]
    public string ByAuthor => this[nameof (ByAuthor)];

    /// <summary>Permissions</summary>
    [ResourceEntry("Permissions", Description = "The link that navigates to permissions dialog in the sidebar.", LastModified = "2010/03/17", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>Custom Fields</summary>
    [ResourceEntry("CustomFields", Description = "The link that navigates to the dialog for managing custom fields in the sidebar.", LastModified = "2010/03/17", Value = "Custom Fields")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>Actions</summary>
    [ResourceEntry("Actions", Description = "The link representing the actions menu in the grid.", LastModified = "2010/03/18", Value = "Actions")]
    public string Actions => this[nameof (Actions)];

    /// <summary>File / Dim. / Size</summary>
    [ResourceEntry("FileDimSize", Description = "The header of 'File / Dim. / Size' column in the grid.", LastModified = "2010/03/17", Value = "File / Dim. / Size")]
    public string FileDimSize => this[nameof (FileDimSize)];

    /// <summary>File / Size</summary>
    [ResourceEntry("FileSize", Description = "The header of 'File / Size' column in the grid.", LastModified = "2010/03/17", Value = "File / Size")]
    public string FileSize => this[nameof (FileSize)];

    /// <summary>Uploaded / Owner</summary>
    [ResourceEntry("UploadedOwner", Description = "The header of 'Uploaded / Owner' column in the grid.", LastModified = "2010/03/17", Value = "Uploaded / Owner")]
    public string UploadedOwner => this[nameof (UploadedOwner)];

    /// <summary>Categories</summary>
    [ResourceEntry("Categories", Description = "The 'Categories' label in the grid column.", LastModified = "2010/03/18", Value = "<em>Categories:</em>")]
    public string Categories => this[nameof (Categories)];

    /// <summary>Tags</summary>
    [ResourceEntry("Tags", Description = "The 'Tags' label in the grid column.", LastModified = "2010/03/18", Value = "<em>Tags:</em>")]
    public string Tags => this[nameof (Tags)];

    /// <summary>Uploaded on</summary>
    [ResourceEntry("UploadedOn", Description = "The 'Uploaded on' label in the grid column.", LastModified = "2010/03/18", Value = "<em>Uploaded on</em>")]
    public string UploadedOn => this[nameof (UploadedOn)];

    /// <summary>word: by</summary>
    [ResourceEntry("By", Description = "The 'by' label in the grid column.", LastModified = "2010/03/18", Value = "by")]
    public string By => this[nameof (By)];

    /// <summary>phrase: View original</summary>
    [ResourceEntry("ViewOriginal", Description = "The text of the 'View original' link in the action menu.", LastModified = "2010/03/22", Value = "View original")]
    public string ViewOriginal => this[nameof (ViewOriginal)];

    /// <summary>phrase: View original size</summary>
    [ResourceEntry("ViewOriginalSize", Description = "The text of the 'View original size' link in the action menu.", LastModified = "2011/06/10", Value = "View original size")]
    public string ViewOriginalSize => this[nameof (ViewOriginalSize)];

    /// <summary>phrase: Set as primary image</summary>
    [ResourceEntry("SetAsPrimaryImage", Description = "phrase: Set as primary image", LastModified = "2011/06/02", Value = "Set as primary image")]
    public string SetAsPrimaryImage => this[nameof (SetAsPrimaryImage)];

    /// <summary>word: Download</summary>
    [ResourceEntry("Download", Description = "The text of the 'Download' button.", LastModified = "2010/03/22", Value = "Download")]
    public string Download => this[nameof (Download)];

    /// <summary>phrase: Edit Properties</summary>
    [ResourceEntry("EditProperties", Description = "The text of the 'Edit Properties' link in the action menu.", LastModified = "2010/03/22", Value = "Edit Properties")]
    public string EditProperties => this[nameof (EditProperties)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "The text of the 'Set permissions' link in the action menu.", LastModified = "2010/03/22", Value = "Set Permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>
    /// word: <strong>Edit...</strong>
    /// </summary>
    [ResourceEntry("Edit", Description = "word", LastModified = "2010/03/22", Value = "<strong>Edit...</strong>")]
    public string Edit => this[nameof (Edit)];

    /// <summary>Edit image</summary>
    [ResourceEntry("EditImage", Description = "Edit image", LastModified = "2011/06/02", Value = "Edit image")]
    public string EditImage => this[nameof (EditImage)];

    /// <summary>Edit video</summary>
    [ResourceEntry("EditAllProperties", Description = "Edit all properties", LastModified = "2014/02/10", Value = "Edit all properties")]
    public string EditAllProperties => this[nameof (EditAllProperties)];

    /// <summary>Label: Change image</summary>
    [ResourceEntry("ChangeImage", Description = "Label: Change image", LastModified = "2014/02/10", Value = "Change image")]
    public string ChangeImage => this[nameof (ChangeImage)];

    /// <summary>Label: Change document</summary>
    [ResourceEntry("ChangeDocument", Description = "Label: Change document", LastModified = "2014/02/10", Value = "Change document")]
    public string ChangeDocument => this[nameof (ChangeDocument)];

    /// <summary>Label: Change video</summary>
    [ResourceEntry("ChangeVideo", Description = "Label: Change video", LastModified = "2014/02/10", Value = "Change video")]
    public string ChangeVideo => this[nameof (ChangeVideo)];

    /// <summary>
    /// Phrase: Advanced (Cache, Restrictions, URL, Storage provider)
    /// </summary>
    [ResourceEntry("AdvancedSection", Description = "The title of the 'Advanced' section in the Backend Detail View form.", LastModified = "2011/06/28", Value = "Advanced <em class='sfNote'>(Cache, Restrictions, URL, Storage provider)</em>")]
    public string AdvancedSection => this[nameof (AdvancedSection)];

    /// <summary>Phrase: Advanced (URL, Comments, Storage provider)</summary>
    [ResourceEntry("ImageAdvancedSection", Description = "The title of the 'Advanced' section in the Backend Detail View form.", LastModified = "2011/06/28", Value = "Advanced <em class='sfNote'>(URL, Comments, Storage provider)</em>")]
    public string ImageAdvancedSection => this[nameof (ImageAdvancedSection)];

    /// <summary>Phrase: Categories and Tags</summary>
    [ResourceEntry("CategoriesAndTags", Description = "The title of the 'Categories and Tags' section.", LastModified = "2010/04/07", Value = "Categories and tags")]
    public string CategoriesAndTags => this[nameof (CategoriesAndTags)];

    /// <summary>Phrase: Details (Author, Description)</summary>
    [ResourceEntry("Details", Description = "The title of the 'Details' section.", LastModified = "2011/05/20", Value = "Details <em class='sfNote'>(Author, Description)</em>")]
    public string Details => this[nameof (Details)];

    /// <summary>word: Title</summary>
    [ResourceEntry("Title", Description = "The 'Title' label.", LastModified = "2010/04/07", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>word: Title</summary>
    [ResourceEntry("LibraryName", Description = "The 'Library name' label.", LastModified = "2020/02/07", Value = "Library name")]
    public string LibraryName => this[nameof (LibraryName)];

    /// <summary>Phrase: Title cannot be empty</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "phrase: Title cannot be empty", LastModified = "2010/04/07", Value = "Title cannot be empty")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Saves the edited item changes", LastModified = "2010/04/07", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>phrase: Delete this image</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>phrase: ReviewHistory</summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Revision History")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>Word: Status</summary>
    [ResourceEntry("Status", Description = "The 'Status' label.", LastModified = "2010/04/07", Value = "Status")]
    public string Status => this[nameof (Status)];

    /// <summary>Phrase: Publish on:</summary>
    [ResourceEntry("PublishOn", Description = "Phrase: Publish on:", LastModified = "2010/04/07", Value = "Publish on:")]
    public string PublishOn => this[nameof (PublishOn)];

    /// <summary>Phrase: Expires on:</summary>
    [ResourceEntry("ExpiresOn", Description = "Phrase: Expires on:", LastModified = "2010/04/07", Value = "Expires on:")]
    public string ExpiresOn => this[nameof (ExpiresOn)];

    /// <summary>Phrase: Publication Date cannot be empty</summary>
    [ResourceEntry("PublicationDateCannotBeEmpty", Description = "Publication Date cannot be empty.", LastModified = "2010/04/07", Value = "Publication Date cannot be empty")]
    public string PublicationDateCannotBeEmpty => this[nameof (PublicationDateCannotBeEmpty)];

    /// <summary>word: Author</summary>
    [ResourceEntry("Author", Description = "The 'Author' label.", LastModified = "2010/04/08", Value = "Author")]
    public string Author => this[nameof (Author)];

    /// <summary>Phrase: Size</summary>
    [ResourceEntry("Size", Description = "The 'Size' label.", LastModified = "2019/01/30", Value = "Size")]
    public string Size => this[nameof (Size)];

    /// <summary>Phrase: Dimensions:</summary>
    [ResourceEntry("Dimensions", Description = "The 'Dimensions:' label.", LastModified = "2010/04/09", Value = "Dimensions:")]
    public string Dimensions => this[nameof (Dimensions)];

    /// <summary>Phrase: bytes</summary>
    [ResourceEntry("bytes", Description = "The 'bytes' unit.", LastModified = "2011/01/20", Value = "bytes")]
    public string bytes => this[nameof (bytes)];

    /// <summary>Phrase: Original file:</summary>
    [ResourceEntry("OriginalFile", Description = "The 'Original file:' label.", LastModified = "2010/04/09", Value = "Original file:")]
    public string OriginalFile => this[nameof (OriginalFile)];

    /// <summary>Phrase: As they are ordered in image library</summary>
    [ResourceEntry("AsOrderedInAlbum", Description = "Phrase: As they are ordered in image library", LastModified = "2011/09/07", Value = "As they are ordered in image library")]
    public string AsOrderedInAlbum => this[nameof (AsOrderedInAlbum)];

    /// <summary>Phrase: As they are ordered in library</summary>
    [ResourceEntry("AsOrderedInLibrary", Description = "Phrase: As they are ordered in library", LastModified = "2011/09/07", Value = "As they are ordered in library")]
    public string AsOrderedInLibrary => this[nameof (AsOrderedInLibrary)];

    /// <summary>Phrase: Random (different each time)</summary>
    [ResourceEntry("RandomEachTime", Description = "Phrase: Random (different each time)", LastModified = "2010/09/14", Value = "Random (different each time)")]
    public string RandomEachTime => this[nameof (RandomEachTime)];

    /// <summary>Phrase: Divide the thumbnail list on pages up to</summary>
    [ResourceEntry("GalleryDesignerPagingDesc", Description = "Phrase: Divide the thumbnail list on pages up to", LastModified = "2010/06/08", Value = "Divide the thumbnail list on pages up to")]
    public string GalleryDesignerPagingDesc => this[nameof (GalleryDesignerPagingDesc)];

    /// <summary>Phrase: Use Paging</summary>
    [ResourceEntry("UsePaging", Description = "Phrase: Use Paging", LastModified = "2010/08/16", Value = "Use paging")]
    public string UsePaging => this[nameof (UsePaging)];

    /// <summary>Phrase: Use limit</summary>
    [ResourceEntry("UseLimit", Description = "Phrase: Use limit", LastModified = "2010/04/12", Value = "Use limit")]
    public string UseLimit => this[nameof (UseLimit)];

    /// <summary>Phrase: Show only limited number of items:</summary>
    [ResourceEntry("GalleryDesignerLimitDesc", Description = "Phrase: Show only limited number of items:", LastModified = "2010/04/12", Value = "Show only limited number of items:")]
    public string GalleryDesignerLimitDesc => this[nameof (GalleryDesignerLimitDesc)];

    /// <summary>Phrase: Show all published items at once</summary>
    [ResourceEntry("GalleryDesignerNoLimitAndPagingDesc", Description = "Phrase: Show all published items at once", LastModified = "2010/04/12", Value = "Show all published items at once")]
    public string GalleryDesignerNoLimitAndPagingDesc => this[nameof (GalleryDesignerNoLimitAndPagingDesc)];

    /// <summary>Phrase: No limit and paging</summary>
    [ResourceEntry("NoLimitAndPaging", Description = "Phrase: No limit and paging", LastModified = "2010/04/12", Value = "No limit and paging")]
    public string NoLimitAndPaging => this[nameof (NoLimitAndPaging)];

    /// <summary>Phrase: Size of thumbnails (optional)</summary>
    [ResourceEntry("ThumbnailSize", Description = "Phrase: Size of thumbnails (optional)", LastModified = "2013/12/11", Value = "Size of thumbnails <i class='sfNote'>(optional)</i>")]
    public string ThumbnailSize => this[nameof (ThumbnailSize)];

    /// <summary>Phrase: Size of images</summary>
    [ResourceEntry("ImagesSize", Description = "Phrase: Size of images", LastModified = "2010/06/21", Value = "Size of images")]
    public string ImagesSize => this[nameof (ImagesSize)];

    /// <summary>Phrase: Size of the big image</summary>
    [ResourceEntry("BigImageSize", Description = "Phrase: Size of the image", LastModified = "2013/07/03", Value = "Size of the image")]
    public string BigImageSize => this[nameof (BigImageSize)];

    /// <summary>Phrase: Size of the video</summary>
    [ResourceEntry("VideoSize", Description = "Phrase: Size of the video", LastModified = "2010/06/08", Value = "Size of the video")]
    public string VideoSize => this[nameof (VideoSize)];

    /// <summary>Phrase: Medium: 500 px width</summary>
    [ResourceEntry("Medium500px", Description = "Phrase: Medium: 500 px width", LastModified = "2010/06/07", Value = "Medium: 500 px width")]
    public string Medium500px => this[nameof (Medium500px)];

    /// <summary>Phrase: Big: 800 px width</summary>
    [ResourceEntry("Big800px", Description = "Phrase: Big: 800 px width", LastModified = "2010/06/07", Value = "Big: 800 px width")]
    public string Big800px => this[nameof (Big800px)];

    /// <summary>Phrase: Thumbnails + Detail page</summary>
    [ResourceEntry("ThumbnailsPlusDetailPage", Description = "Phrase: Thumbnails + Detail page", LastModified = "2010/04/12", Value = "Thumbnails + Detail page")]
    public string ThumbnailsPlusDetailPage => this[nameof (ThumbnailsPlusDetailPage)];

    /// <summary>Phrase: Thumbnails + Overlay (lightbox)</summary>
    [ResourceEntry("ThumbnailsPlusOverlay", Description = "Phrase: Thumbnails + Overlay (lightbox)", LastModified = "2010/04/12", Value = "Thumbnails + Overlay (lightbox)")]
    public string ThumbnailsPlusOverlay => this[nameof (ThumbnailsPlusOverlay)];

    /// <summary>Phrase: An image per page</summary>
    [ResourceEntry("AnImagePerPage", Description = "Phrase: An image per page", LastModified = "2010/04/12", Value = "An image per page")]
    public string AnImagePerPage => this[nameof (AnImagePerPage)];

    /// <summary>Phrase: Thumbnail strip + Image on the same page</summary>
    [ResourceEntry("ThumbnailStripPlusImage", Description = "Phrase: Thumbnail strip + Image on the same page", LastModified = "2010/04/12", Value = "Thumbnail strip + Image on the same page")]
    public string ThumbnailStripPlusImage => this[nameof (ThumbnailStripPlusImage)];

    /// <summary>Phrase: Simple list</summary>
    [ResourceEntry("SimpleList", Description = "Phrase: Simple list", LastModified = "2010/04/12", Value = "Simple list")]
    public string SimpleList => this[nameof (SimpleList)];

    /// <summary>Phrase: - Select image size -</summary>
    [ResourceEntry("SelectImageSize", Description = "Phrase: - Select image size -", LastModified = "2013/07/03", Value = "- Select image size -")]
    public string SelectImageSize => this[nameof (SelectImageSize)];

    /// <summary>Phrase: Original size</summary>
    [ResourceEntry("OriginalSize", Description = "Phrase: Original size", LastModified = "2010/06/04", Value = "Original size")]
    public string OriginalSize => this[nameof (OriginalSize)];

    /// <summary>Phrase: 120 px width</summary>
    [ResourceEntry("Thumbnail120px", Description = "Phrase: 120 px width", LastModified = "2010/06/04", Value = "120 px width")]
    public string Thumbnail100px => this[nameof (Thumbnail100px)];

    /// <summary>Phrase: 240 px width</summary>
    [ResourceEntry("Small240px", Description = "Phrase: 240 px width", LastModified = "2010/06/04", Value = "240 px width")]
    public string Small240px => this[nameof (Small240px)];

    /// <summary>Phrase: Show links to the previous and the next image</summary>
    [ResourceEntry("ShowPrevAndNextLinks", Description = "Phrase: Show links to the previous and the next image", LastModified = "2010/06/04", Value = "Show links to the previous and the next image")]
    public string ShowPrevAndNextLinks => this[nameof (ShowPrevAndNextLinks)];

    /// <summary>
    /// Phrase: Show links to the previous and the next image as...
    /// </summary>
    [ResourceEntry("ShowPrevAndNextLinksAs", Description = "Phrase: Show links to the previous and the next image as...", LastModified = "2010/06/04", Value = "Show links to the previous and the next image as...")]
    public string ShowPrevAndNextLinksAs => this[nameof (ShowPrevAndNextLinksAs)];

    /// <summary>Phrase: Text Links</summary>
    [ResourceEntry("TextLinks", Description = "Phrase: Text Links", LastModified = "2010/06/04", Value = "Text Links")]
    public string TextLinks => this[nameof (TextLinks)];

    /// <summary>Phrase: Thumbnails of previous and next image</summary>
    [ResourceEntry("ThumbnailsOfPrevAndNextImage", Description = "Phrase: Thumbnails of previous and next image", LastModified = "2010/06/04", Value = "Thumbnails of previous and next image")]
    public string ThumbnailsOfPrevAndNextImage => this[nameof (ThumbnailsOfPrevAndNextImage)];

    /// <summary>Phrase: Edit Thumbnail list template</summary>
    [ResourceEntry("EditThumbnailListTemplate", Description = "Phrase: Edit Thumbnail list template", LastModified = "2010/06/04", Value = "Edit Thumbnail list template")]
    public string EditThumbnailListTemplate => this[nameof (EditThumbnailListTemplate)];

    /// <summary>Phrase: Edit Detail page template</summary>
    [ResourceEntry("EditDetailPageTemplate", Description = "Phrase: Edit Detail page template", LastModified = "2010/06/04", Value = "Edit Detail page template")]
    public string EditDetailPageTemplate => this[nameof (EditDetailPageTemplate)];

    /// <summary>phrase: Fine tune the selected type</summary>
    [ResourceEntry("FineTuneTheSelectedType", Description = "Phrase: Fine tune the selected type", LastModified = "2010/05/27", Value = "Fine tune the selected type")]
    public string FineTuneTheSelectedType => this[nameof (FineTuneTheSelectedType)];

    /// <summary>Word: Choose</summary>
    [ResourceEntry("Choose", Description = "Word: Choose", LastModified = "2010/04/15", Value = "Choose")]
    public string Choose => this[nameof (Choose)];

    /// <summary>Phrase: Image Gallery</summary>
    [ResourceEntry("ImageGalleryPropertyEditorTitle", Description = "Word: Image Gallery", LastModified = "2010/04/15", Value = "Image Gallery")]
    public string ImageGalleryPropertyEditorTitle => this[nameof (ImageGalleryPropertyEditorTitle)];

    /// <summary>Phrase: Video Gallery</summary>
    [ResourceEntry("VideoGalleryPropertyEditorTitle", Description = "Word: Video Gallery", LastModified = "2010/04/15", Value = "Video Gallery")]
    public string VideoGalleryPropertyEditorTitle => this[nameof (VideoGalleryPropertyEditorTitle)];

    /// <summary>Phrase: No image library selected</summary>
    [ResourceEntry("NoAlbumSelected", Description = "Word: No image library selected", LastModified = "2010/07/13", Value = "No image library selected")]
    public string NoAlbumSelected => this[nameof (NoAlbumSelected)];

    /// <summary>Phrase: No library selected</summary>
    [ResourceEntry("NoLibrarySelected", Description = "Word: No library selected", LastModified = "2010/06/08", Value = "No library selected")]
    public string NoLibrarySelected => this[nameof (NoLibrarySelected)];

    /// <summary>Phrase: Select an image</summary>
    [ResourceEntry("ImageControlPropertyEditorTitle", Description = "Phrase: Select an image", LastModified = "2013/02/25", Value = "Select an image")]
    public string ImageControlPropertyEditorTitle => this[nameof (ImageControlPropertyEditorTitle)];

    /// <summary>
    /// The text of the command for generating embed image html.
    /// </summary>
    [ResourceEntry("EmbedImage", Description = "The text of the command for generating embed image html.", LastModified = "2010/04/15", Value = "Embed")]
    public string EmbedImage => this[nameof (EmbedImage)];

    /// <summary>word: Move</summary>
    [ResourceEntry("Move", Description = "The text of the Move button.", LastModified = "2010/04/26", Value = "Move")]
    public string Move => this[nameof (Move)];

    /// <summary>phrase: Start typing {0} name...</summary>
    [ResourceEntry("LibrarySelectorSearchBoxTitle", Description = "The title of the search box in library selector.", LastModified = "2010/04/26", Value = "Start typing {0} name...")]
    public string LibrarySelectorSearchBoxTitle => this[nameof (LibrarySelectorSearchBoxTitle)];

    /// <summary>phrase: Select an {0} where to move {1} {2}</summary>
    [ResourceEntry("LibrarySelectorTitle", Description = "The title of the library selector.", LastModified = "2010/04/26", Value = "Select {0} where to move {1} {2}")]
    public string LibrarySelectorTitle => this[nameof (LibrarySelectorTitle)];

    /// <summary>
    /// phrase: Moving items may take a while. You will not be able to continue with your work until the process finishes
    /// </summary>
    [ResourceEntry("ThisOperationCanTakeConsiderableAmountOfTime", Description = "warning message: Moving items may take a while. You will not be able to continue with your work until the process finishes", LastModified = "2011/07/22", Value = "Moving items may take a while. You will not be able to continue with your work until the process finishes")]
    public string ThisOperationCanTakeConsiderableAmountOfTime => this[nameof (ThisOperationCanTakeConsiderableAmountOfTime)];

    /// <summary>phrase: Create a library</summary>
    [ResourceEntry("CreateLibrary", Description = "The text of 'Create a library' button.", LastModified = "2010/05/11", Value = "Create a library")]
    public string CreateLibrary => this[nameof (CreateLibrary)];

    /// <summary>phrase: Create a new {0}</summary>
    [ResourceEntry("CreateNewLibrary", Description = "label", LastModified = "2010/08/19", Value = "Create a new {0}")]
    public string CreateNewLibrary => this[nameof (CreateNewLibrary)];

    /// <summary>phrase: Create a new {0}</summary>
    [ResourceEntry("CreateANewLibrary", Description = "label", LastModified = "2011/09/27", Value = "Create a new library")]
    public string CreateANewLibrary => this[nameof (CreateANewLibrary)];

    /// <summary>phrase: Create this {0}</summary>
    [ResourceEntry("CreateThisLibrary", Description = "The text of 'Create this library', specific to each library type", LastModified = "2011/01/21", Value = "Create this {0}")]
    public string CreateThisLibrary => this[nameof (CreateThisLibrary)];

    /// <summary>phrase: Edit a library</summary>
    [ResourceEntry("EditLibrary", Description = "The text of 'Edit a library' button.", LastModified = "2010/05/11", Value = "Edit a library")]
    public string EditLibrary => this[nameof (EditLibrary)];

    /// <summary>phrase: Move to another library</summary>
    [ResourceEntry("MoveToAnotherLibrary", Description = "The text of 'Move to another library' menu item.", LastModified = "2011/01/04", Value = "Move to another library")]
    public string MoveToAnotherLibrary => this[nameof (MoveToAnotherLibrary)];

    /// <summary>phrase: Assign tags and categories</summary>
    [ResourceEntry("AssignTaxons", Description = "The text of 'Assign tags and categories' menu item.", LastModified = "2020/06/22", Value = "Assign tags and categories")]
    public string AssignTaxons => this[nameof (AssignTaxons)];

    /// <summary>phrase: Assign tags and categories to {0}</summary>
    [ResourceEntry("AssignTaxonsDetailedTitle", Description = "The detailed title of 'Assign tags and categories' operation.", LastModified = "2020/06/22", Value = "Assign tags and categories to {0}")]
    public string AssignTaxonsDetailedTitle => this[nameof (AssignTaxonsDetailedTitle)];

    /// <summary>word: Library</summary>
    [ResourceEntry("Library", Description = "The 'Library' string.", LastModified = "2010/05/11", Value = "Library")]
    public string Library => this[nameof (Library)];

    /// <summary>phrase: Show 10 more libraries</summary>
    [ResourceEntry("ShowMoreLibraries", Description = "The 'Show 10 more libraries' link.", LastModified = "2010/05/11", Value = "Show 10 more libraries")]
    public string ShowMoreLibraries => this[nameof (ShowMoreLibraries)];

    /// <summary>phrase: Manage libraries</summary>
    [ResourceEntry("ManageLibraries", Description = "The 'Manage libraries' link in the grid sidebar.", LastModified = "2010/05/11", Value = "Manage libraries")]
    public string ManageLibraries => this[nameof (ManageLibraries)];

    /// <summary>Phrase: Click to add a description</summary>
    [ResourceEntry("ClickToAddDescription", Description = "The expand text for the description field.", LastModified = "2010/05/12", Value = "Click to add a description")]
    public string ClickToAddDescription => this[nameof (ClickToAddDescription)];

    /// <summary>Phrase: URL</summary>
    [ResourceEntry("UrlName", Description = "The title of the url field.", LastModified = "2010/03/30", Value = "URL")]
    public string UrlName => this[nameof (UrlName)];

    /// <summary>Phrase: URL</summary>
    [ResourceEntry("MediaFileUrlName", Description = "The title of the media file url field.", LastModified = "2016/02/23", Value = "URL to file")]
    public string MediaFileUrlName => this[nameof (MediaFileUrlName)];

    /// <summary>Phrase: URL cannot be empty</summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "The message shown when the url is empty.", LastModified = "2010/03/30", Value = "URL cannot be empty")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>Phrase: Status of uploaded {0}</summary>
    [ResourceEntry("StatusControlTitle", Description = "The title of the status control in upload dialog.", LastModified = "2010/05/13", Value = "Status of uploaded {0}")]
    public string StatusControlTitle => this[nameof (StatusControlTitle)];

    /// <summary>word: Library:</summary>
    [ResourceEntry("LibraryLabel", Description = "The 'Library:' string.", LastModified = "2010/05/13", Value = "<em>Library:</em>")]
    public string LibraryLabel => this[nameof (LibraryLabel)];

    /// <summary>phrase: Select a library</summary>
    [ResourceEntry("SelectLibrary", Description = "phrase: -- Select a library --", LastModified = "2010/07/26", Value = "-- Select a library --")]
    public string SelectLibrary => this[nameof (SelectLibrary)];

    /// <summary>phrase: Select a library</summary>
    [ResourceEntry("SelectALibrary", Description = "phrase: Select a library", LastModified = "2013/03/14", Value = "Select a library")]
    public string SelectALibrary => this[nameof (SelectALibrary)];

    /// <summary>phrase: Select a library</summary>
    [ResourceEntry("SelectImageLibrary", Description = "phrase: Select image library", LastModified = "2013/02/18", Value = "Select image library")]
    public string SelectImageLibrary => this[nameof (SelectImageLibrary)];

    /// <summary>phrase: Select image</summary>
    [ResourceEntry("SelectImage", Description = "phrase: Select image", LastModified = "2014/01/24", Value = "Select image")]
    public string SelectImage => this[nameof (SelectImage)];

    /// <summary>phrase: Select document</summary>
    [ResourceEntry("SelectDocument", Description = "phrase: Select document", LastModified = "2014/02/10", Value = "Select document")]
    public string SelectDocument => this[nameof (SelectDocument)];

    /// <summary>phrase: Select video</summary>
    [ResourceEntry("SelectVideo", Description = "phrase: Select video", LastModified = "2014/02/10", Value = "Select video")]
    public string SelectVideo => this[nameof (SelectVideo)];

    /// <summary>phrase: View all {0}</summary>
    [ResourceEntry("ViewAll", Description = "phrase: View all {0}", LastModified = "2010/05/13", Value = "View all {0}")]
    public string ViewAll => this[nameof (ViewAll)];

    /// <summary>phrase: View all items</summary>
    [ResourceEntry("ViewAllItems", Description = "phrase: View all items", LastModified = "2010/05/13", Value = "View all items")]
    public string ViewAllItems => this[nameof (ViewAllItems)];

    /// <summary>phrase: Upload this {0}</summary>
    [ResourceEntry("UploadThisItem", Description = "phrase: Upload this {0}", LastModified = "2010/08/18", Value = "Upload this {0}")]
    public string UploadThisItem => this[nameof (UploadThisItem)];

    /// <summary>phrase: Add details to the uploaded {0}</summary>
    [ResourceEntry("AddDetails", Description = "phrase: Add details to the uploaded {0}", LastModified = "2010/05/13", Value = "Add details to the uploaded {0}")]
    public string AddDetails => this[nameof (AddDetails)];

    /// <summary>phrase: Upload more {0}</summary>
    [ResourceEntry("UploadOther", Description = "phrase: Upload more {0}", LastModified = "2010/08/16", Value = "Upload more {0}")]
    public string UploadOther => this[nameof (UploadOther)];

    /// <summary>
    /// phrase: Reorder {0} in <em>{{0}}</em> {1}
    /// </summary>
    [ResourceEntry("ReorderItemsInLibrary", Description = "phrase: Reorder {0} in <em>{{0}}</em> {1}", LastModified = "2010/05/25", Value = "Reorder {0} in <em>{{0}}</em> {1}")]
    public string ReorderItemsInLibrary => this[nameof (ReorderItemsInLibrary)];

    /// <summary>Settings</summary>
    [ResourceEntry("Settings", Description = "Word: Settings", LastModified = "2010/05/27", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    [ResourceEntry("DocumentsAndFiles", Description = "Phrase: Documents & Files", LastModified = "2010/05/27", Value = "Documents & Files")]
    public string DocumentsAndFiles => this[nameof (DocumentsAndFiles)];

    /// <summary>phrase: Library actions</summary>
    [ResourceEntry("LibraryActions", Description = "Phrase: Library actions", LastModified = "2013/03/11", Value = "Library actions")]
    public string LibraryActions => this[nameof (LibraryActions)];

    /// <summary>phrase: Edit Properties of this {0}</summary>
    [ResourceEntry("EditLibraryProperties", Description = "Phrase: Edit Properties of this {0}", LastModified = "2010/05/28", Value = "<em>Edit properties</em> of this {0}")]
    public string EditLibraryProperties => this[nameof (EditLibraryProperties)];

    /// <summary>phrase: Set permissions of this {0}</summary>
    [ResourceEntry("SetLibraryPermissions", Description = "Phrase: Set permissions of this {0}", LastModified = "2010/05/28", Value = "<em>Set permissions</em> of this {0}")]
    public string SetLibraryPermissions => this[nameof (SetLibraryPermissions)];

    /// <summary>phrase: Delete this {0}</summary>
    [ResourceEntry("DeleteLibrary", Description = "Phrase: Delete this {0}", LastModified = "2010/05/28", Value = "<em>Delete</em> this {0}")]
    public string DeleteLibrary => this[nameof (DeleteLibrary)];

    /// <summary>phrase: Reorder {0}</summary>
    [ResourceEntry("ReorderItems", Description = "phrase: Reorder {0}", LastModified = "2010/09/24", Value = "<em>Reorder</em> ")]
    public string ReorderItems => this[nameof (ReorderItems)];

    /// <summary>phrase: Narrow by typing title</summary>
    [ResourceEntry("NarrowByTypingTitle", Description = "Phrase: Narrow by typing title", LastModified = "2010/06/08", Value = "Narrow by typing title")]
    public string NarrowByTypingTitle => this[nameof (NarrowByTypingTitle)];

    /// <summary>phrase: Bulk edit</summary>
    [ResourceEntry("BulkEditDialogTitle", Description = "Phrase: Bulk edit", LastModified = "2010/06/15", Value = "Bulk edit")]
    public string BulkEditDialogTitle => this[nameof (BulkEditDialogTitle)];

    /// <summary>word: History</summary>
    [ResourceEntry("History", Description = "word: History", LastModified = "2010/06/21", Value = "History")]
    public string History => this[nameof (History)];

    /// <summary>phrase: Selected {0} was deleted</summary>
    [ResourceEntry("SelectedLibraryWasDeleted", Description = "phrase: Selected {0} was deleted", LastModified = "2010/07/19", Value = "Selected {0} was deleted")]
    public string SelectedLibraryWasDeleted => this[nameof (SelectedLibraryWasDeleted)];

    /// <summary>phrase: No {0} were created</summary>
    [ResourceEntry("NoLibrariesWereCreated", Description = "phrase: No {0} were created", LastModified = "2010/07/19", Value = "No {0} were created")]
    public string NoLibrariesWereCreated => this[nameof (NoLibrariesWereCreated)];

    /// <summary>
    /// phrase: The selected {0} was deleted. Please select another {1}.
    /// </summary>
    [ResourceEntry("SelectAnotherLibrary", Description = "phrase: The selected {0} was moved or deleted. Please select another {1}.", LastModified = "2013/07/26", Value = "The selected {0} was moved or deleted. Please select another {1}.")]
    public string SelectAnotherLibrary => this[nameof (SelectAnotherLibrary)];

    /// <summary>phrase: Drag {0} to change its order</summary>
    [ResourceEntry("DragToChangeOrder", Description = "phrase: Drag {0} to change its order", LastModified = "2010/08/04", Value = "Drag {0} to change its order")]
    public string DragToChangeOrder => this[nameof (DragToChangeOrder)];

    /// <summary>phrase: From your computer</summary>
    [ResourceEntry("FromYourComputer", Description = "phrase: From your computer", LastModified = "2010/08/16", Value = "From your computer")]
    public string FromYourComputer => this[nameof (FromYourComputer)];

    /// <summary>All {0}</summary>
    [ResourceEntry("AllItems", Description = "All {0}", LastModified = "2010/08/16", Value = "All {0}")]
    public string AllItems => this[nameof (AllItems)];

    /// <summary>All items</summary>
    [ResourceEntry("AllItems1", Description = "All items", LastModified = "2010/08/16", Value = "All items")]
    public string AllItems1 => this[nameof (AllItems1)];

    /// <summary>All videos</summary>
    [ResourceEntry("AllVideos", Description = "All videos", LastModified = "2010/08/16", Value = "All videos")]
    public string AllVideos => this[nameof (AllVideos)];

    /// <summary>All videos</summary>
    [ResourceEntry("AllDocuments", Description = "All documents", LastModified = "2010/08/16", Value = "All documents")]
    public string AllDocuments => this[nameof (AllDocuments)];

    /// <summary>Insert the {0}</summary>
    [ResourceEntry("InsertItem", Description = "Insert the {0}", LastModified = "2010/08/17", Value = "Insert the {0}")]
    public string InsertItem => this[nameof (InsertItem)];

    /// <summary>phrase: Insert {0}</summary>
    [ResourceEntry("InsertAItem", Description = "phrase: Insert {0}", LastModified = "2010/08/19", Value = "Insert {0}")]
    public string InsertAItem => this[nameof (InsertAItem)];

    /// <summary>phrase: From already uploaded {0}</summary>
    [ResourceEntry("FromAlreadyUploadedItems", Description = "phrase: From already uploaded {0}", LastModified = "2010/08/17", Value = "From already uploaded")]
    public string FromAlreadyUploadedItems => this[nameof (FromAlreadyUploadedItems)];

    /// <summary>Label: Which {0} to upload?</summary>
    [ResourceEntry("WhichItemToUpload", Description = "label", LastModified = "2010/08/18", Value = "Which {0} to upload?")]
    public string WhichItemToUpload => this[nameof (WhichItemToUpload)];

    /// <summary>Label: Where to store the uploaded {0}?</summary>
    [ResourceEntry("WhereToStoreTheUploadedItem", Description = "label", LastModified = "2010/08/18", Value = "Where to store the uploaded {0}?")]
    public string WhereToStoreTheUploadedItem => this[nameof (WhereToStoreTheUploadedItem)];

    /// <summary>
    /// phrase: Clicking the resized {0) opens the {1} in its original size
    /// </summary>
    [ResourceEntry("ClickingTheResizedItemOpensTheOriginal", Description = "phrase:  Clicking the resized {0} opens the {1} in its original size", LastModified = "2010/08/19", Value = " Clicking the resized {0} opens the {1} in its original size")]
    public string ClickingTheResizedItemOpensTheOriginal => this[nameof (ClickingTheResizedItemOpensTheOriginal)];

    /// <summary>phrase: {0} {1} has been successfully uploaded</summary>
    [ResourceEntry("OneItemHasBeenSuccessfullyUploaded", Description = "phrase:  {0} {1} has been successfully uploaded", LastModified = "2010/10/14", Value = "{0} {1} has been successfully uploaded")]
    public string OneItemHasBeenSuccessfullyUploaded => this[nameof (OneItemHasBeenSuccessfullyUploaded)];

    /// <summary>phrase: {0} {1}s have been successfully uploaded</summary>
    [ResourceEntry("MultipleItemsHaveBeenSuccessfullyUploaded", Description = "phrase:  {0} {1}s have been successfully uploaded", LastModified = "2010/10/14", Value = "{0} {1}s have been successfully uploaded")]
    public string MultipleItemsHaveBeenSuccessfullyUploaded => this[nameof (MultipleItemsHaveBeenSuccessfullyUploaded)];

    /// <summary>phrase: Multiple {0} can be uploaded at a time</summary>
    [ResourceEntry("MultipleItemsCanBeUploadedAtATime", Description = "phrase:  Multiple {0} can be uploaded at a time", LastModified = "2010/10/14", Value = "Multiple {0} can be uploaded at a time")]
    public string MultipleItemsCanBeUploadedAtATime => this[nameof (MultipleItemsCanBeUploadedAtATime)];

    /// <summary>phrase: Only one {0} can be uploaded at a time</summary>
    [ResourceEntry("OnlyOneItemCanBeUploadedAtATime", Description = "phrase:  Multiple {0} can be uploaded at a time", LastModified = "2010/10/14", Value = "Only one {0} can be uploaded at a time")]
    public string OnlyOneItemCanBeUploadedAtATime => this[nameof (OnlyOneItemCanBeUploadedAtATime)];

    /// <summary>phrase: Select {0}</summary>
    [ResourceEntry("SelectItem", Description = "phrase:  Select {0}", LastModified = "2010/10/14", Value = "Select {0}")]
    public string SelectItem => this[nameof (SelectItem)];

    /// <summary>
    /// phrase: You must select the {0} in which the {1} ought to be uploaded.
    /// </summary>
    [ResourceEntry("YouMustSelectLibraryNameInWhichToUploadItemName", Description = "phrase:  You must select the {0} in which the {1} ought to be uploaded.", LastModified = "2010/10/14", Value = "You must select the {0} in which the {1} ought to be uploaded.")]
    public string YouMustSelectLibraryNameInWhichToUploadItemName => this[nameof (YouMustSelectLibraryNameInWhichToUploadItemName)];

    /// <summary>phrase: Which {0} to upload?</summary>
    [ResourceEntry("WhichItemNameToUpload", Description = "phrase:  Which {0} to upload?", LastModified = "2010/10/14", Value = "Which {0} to upload?")]
    public string WhichItemNameToUpload => this[nameof (WhichItemNameToUpload)];

    /// <summary>phrase: Uploading</summary>
    [ResourceEntry("Uploading", Description = "phrase:  Uploading", LastModified = "2010/10/14", Value = "Uploading")]
    public string Uploading => this[nameof (Uploading)];

    /// <summary>phrase: Upload done</summary>
    [ResourceEntry("UploadDone", Description = "phrase:  Upload done", LastModified = "2010/10/14", Value = "Upload done")]
    public string UploadDone => this[nameof (UploadDone)];

    /// <summary>
    /// phrase: You have unsaved changes! Do you want to leave this page?
    /// </summary>
    [ResourceEntry("YouHaveUnsavedChangesWantToLeavePage", Description = "phrase: You have unsaved changes! Do you want to leave this page?", LastModified = "2010/10/14", Value = "Are you sure you want to navigate away from this page?\n\nYou have unsaved changes.\n\nPress OK to continue, or Cancel to stay on the current page.")]
    public string YouHaveUnsavedChangesWantToLeavePage => this[nameof (YouHaveUnsavedChangesWantToLeavePage)];

    /// <summary>
    /// phrase: You have unsaved changes! Do you want to leave this page?
    /// </summary>
    [ResourceEntry("YouHaveUnuploadedItemsWantToInterruptUploading", Description = "phrase: By leaving this page you will abandon the upload and some items may appear corrupted.", LastModified = "2011/09/14", Value = "By leaving this page you will abandon the upload and some items may appear corrupted.")]
    public string YouHaveUnuploadedItemsWantToInterruptUploading => this[nameof (YouHaveUnuploadedItemsWantToInterruptUploading)];

    /// <summary>phrase: Add more</summary>
    [ResourceEntry("AddMore", Description = "phrase: Add more", LastModified = "2010/10/18", Value = "Add more")]
    public string AddMore => this[nameof (AddMore)];

    /// <summary>phrase: Select type</summary>
    [ResourceEntry("SelectType", Description = "phrase: Select type", LastModified = "2010/10/18", Value = "Select type")]
    public string SelectType => this[nameof (SelectType)];

    /// <summary>Message: Back to libraries</summary>
    /// <value>The back to all libraries</value>
    [ResourceEntry("BackToItems", Description = "The back to all libraries button", LastModified = "2010/10/16", Value = "Back to Libraries")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Word: Language</summary>
    [ResourceEntry("Language", Description = "word: Language", LastModified = "2010/10/05", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("WaitingForApproval", Description = "The text of the 'Awaiting approval' button in the sidebar.", LastModified = "2010/11/08", Value = "Awaiting approval")]
    public string WaitingForApproval => this[nameof (WaitingForApproval)];

    /// <summary>phrase: Awaiting Review</summary>
    [ResourceEntry("WaitingForReview", Description = "The text of the 'Awaiting Review' button in the sidebar.", LastModified = "2018/11/08", Value = "Awaiting review")]
    public string WaitingForReview => this[nameof (WaitingForReview)];

    /// <summary>phrase: Awaiting Publishing</summary>
    [ResourceEntry("WaitingForPublishing", Description = "The text of the 'Awaiting Publishing' button in the sidebar.", LastModified = "2018/11/08", Value = "Awaiting publishing")]
    public string WaitingForPublishing => this[nameof (WaitingForPublishing)];

    /// <summary>phrase: {0} name</summary>
    [ResourceEntry("LibraryNameText", Description = "Label for the field control where a name for the new library needs to be given.", LastModified = "2011/01/21", Value = "{0} name")]
    public string LibraryNameText => this[nameof (LibraryNameText)];

    /// <summary>phrase: Example: Summer vacation</summary>
    [ResourceEntry("LibraryNameExample", Description = "An example for the field control where a name for the new library has to be given.", LastModified = "2011/01/21", Value = "Example: Summer vacation")]
    public string LibraryNameExample => this[nameof (LibraryNameExample)];

    /// <summary>label: MB</summary>
    [ResourceEntry("Mb", Description = "label: MB", LastModified = "2011/05/04", Value = "MB")]
    public string Mb => this[nameof (Mb)];

    /// <summary>label: KB</summary>
    [ResourceEntry("Kb", Description = "label: KB", LastModified = "2011/05/04", Value = "KB")]
    public string Kb => this[nameof (Kb)];

    /// <summary>Phrase: You are not allowed to view this library item</summary>
    [ResourceEntry("YouAreNotAllowedToViewThisLibraryItem", Description = "Phrase: You are not allowed to view this library item", LastModified = "2011/06/09", Value = "You are not allowed to view this library item")]
    public string YouAreNotAllowedToViewThisLibraryItem => this[nameof (YouAreNotAllowedToViewThisLibraryItem)];

    /// <summary>phrase: Storage providers</summary>
    [ResourceEntry("StorageProviders", Description = "phrase: Storage providers", LastModified = "2011/06/28", Value = "Storage providers")]
    public string StorageProviders => this[nameof (StorageProviders)];

    /// <summary>phrase: Storage providers</summary>
    [ResourceEntry("StorageProvidersForLibraries", Description = "phrase: Storage providers", LastModified = "2011/12/12", Value = "Storage providers")]
    public string StorageProvidersForLibraries => this[nameof (StorageProvidersForLibraries)];

    /// <summary>phrase: Add storage provider</summary>
    [ResourceEntry("AddStorageProvider", Description = "phrase: Add storage provider", LastModified = "2011/06/30", Value = "Add storage provider")]
    public string AddStorageProvider => this[nameof (AddStorageProvider)];

    /// <summary>phrase: Add a provider</summary>
    [ResourceEntry("AddAProvider", Description = "phrase: Add a provider", LastModified = "2011/06/30", Value = "Add a provider")]
    public string AddAProvider => this[nameof (AddAProvider)];

    /// <summary>phrase: Storage provider</summary>
    [ResourceEntry("StorageProvider", Description = "phrase: Storage provider", LastModified = "2011/06/28", Value = "Storage provider")]
    public string StorageProvider => this[nameof (StorageProvider)];

    /// <summary>phrase: {0} (default)</summary>
    [ResourceEntry("DefaultStorageProvider", Description = "phrase: {0} (default)", LastModified = "2011/06/28", Value = "{0} (default)")]
    public string DefaultStorageProvider => this[nameof (DefaultStorageProvider)];

    /// <summary>
    /// phrase: Stores the blob data of library items in a database, using OpenAccess ORM.
    /// </summary>
    [ResourceEntry("BlobStorageDatabaseTypeDescription", Description = "phrase: Stores the blob data of library items in a database, using OpenAccess ORM.", LastModified = "2011/06/29", Value = "Stores the blob data of library items in a database, using OpenAccess ORM.")]
    public string BlobStorageDatabaseTypeDescription => this[nameof (BlobStorageDatabaseTypeDescription)];

    /// <summary>
    /// phrase: Stores the blob data of library items in the file system.
    /// </summary>
    [ResourceEntry("BlobStorageFileSystemTypeDescription", Description = "phrase: Stores the blob data of library items in the file system.", LastModified = "2011/06/29", Value = "Stores the blob data of library items in the file system.")]
    public string BlobStorageFileSystemTypeDescription => this[nameof (BlobStorageFileSystemTypeDescription)];

    /// <summary>phrase: Windows Azure</summary>
    [ResourceEntry("WindowsAzure", Description = "phrase: Windows Azure", LastModified = "2011/11/23", Value = "Windows Azure")]
    public string WindowsAzure => this[nameof (WindowsAzure)];

    /// <summary>
    /// phrase: Stores the blob data of library items in Windows Azure Blob storage.
    /// </summary>
    [ResourceEntry("BlobStorageAzureTypeDescription", Description = "phrase: Stores the blob data of library items in Windows Azure Blob storage.", LastModified = "2011/11/10", Value = "Stores the blob data of library items in Windows Azure Blob storage.")]
    public string BlobStorageAzureTypeDescription => this[nameof (BlobStorageAzureTypeDescription)];

    /// <summary>phrase: Database</summary>
    [ResourceEntry("BlobStorageDatabaseDefaultProviderTitle", Description = "phrase: Database", LastModified = "2011/06/29", Value = "Database")]
    public string BlobStorageDatabaseDefaultProviderTitle => this[nameof (BlobStorageDatabaseDefaultProviderTitle)];

    /// <summary>
    /// phrase: Stores the blob data of library items in the default database.
    /// </summary>
    [ResourceEntry("BlobStorageDatabaseDefaultProviderDescription", Description = "phrase: Stores the blob data of library items in the default database.", LastModified = "2011/06/29", Value = "Stores the blob data of library items in the default database.")]
    public string BlobStorageDatabaseDefaultProviderDescription => this[nameof (BlobStorageDatabaseDefaultProviderDescription)];

    /// <summary>phrase: File System</summary>
    [ResourceEntry("BlobStorageFileSystemDefaultProviderTitle", Description = "phrase: File System", LastModified = "2011/06/29", Value = "File System")]
    public string BlobStorageFileSystemDefaultProviderTitle => this[nameof (BlobStorageFileSystemDefaultProviderTitle)];

    /// <summary>
    /// phrase: Stores the blob data of library items in the App_Data folder.
    /// </summary>
    [ResourceEntry("BlobStorageFileSystemDefaultProviderDescription", Description = "phrase: Stores the blob data of library items in the App_Data folder.", LastModified = "2011/06/29", Value = "Stores the blob data of library items in the App_Data folder.")]
    public string BlobStorageFileSystemDefaultProviderDescription => this[nameof (BlobStorageFileSystemDefaultProviderDescription)];

    /// <summary>
    /// phrase: Stores the blob data of library items in the App_Data folder.
    /// </summary>
    [ResourceEntry("BlobStorageProviderIsUsedMessage", Description = "Warning for users that  blob storage provider is used so it cannot be deleted/disabled", LastModified = "2011/07/18", Value = "The provider {0} is already used in a media library and cannot be deleted.")]
    public string BlobStorageProviderIsUsedMessage => this[nameof (BlobStorageProviderIsUsedMessage)];

    /// <summary>
    /// phrase: External storage providers can be configured only for content libraries: Images, Videos and Documents &amp; Files
    /// </summary>
    [ResourceEntry("BlobStorageProviderLimitationMessage", Description = "phrase: External storage providers can be configured only for content libraries: Images, Videos and Documents &amp; Files", LastModified = "2011/08/15", Value = "External storage providers can be configured only for content libraries: Images, Videos and Documents &amp; Files")]
    public string BlobStorageProviderLimitationMessage => this[nameof (BlobStorageProviderLimitationMessage)];

    /// <summary>
    /// phrase: Label preceding the blob storage location of a library
    /// </summary>
    [ResourceEntry("StoredIn", Description = "Label preceding the blob storage location of a library", LastModified = "2011/07/18", Value = "Stored in")]
    public string StoredIn => this[nameof (StoredIn)];

    /// <summary>Phrase: Max document size</summary>
    [ResourceEntry("MaxDocumentSize", Description = "The title of the field for setting max document size.", LastModified = "2020/06/17", Value = "Max document size")]
    public string MaxDocumentSize => this[nameof (MaxDocumentSize)];

    /// <summary>Phrase: Max image size</summary>
    [ResourceEntry("MaxImageSize", Description = "The title of the field for setting max image size.", LastModified = "2011/07/13", Value = "Max image size")]
    public string MaxImageSize => this[nameof (MaxImageSize)];

    /// <summary>Phrase: Custom size...</summary>
    [ResourceEntry("CustomSize", Description = "The custom size option in the dropdown for selecting size.", LastModified = "2011/07/13", Value = "Custom size...")]
    public string CustomSize => this[nameof (CustomSize)];

    /// <summary>Phrase: Max album size</summary>
    [ResourceEntry("MaxAlbumSize", Description = "The title of the field for setting max album size.", LastModified = "2010/03/30", Value = "Max album size")]
    public string MaxAlbumSize => this[nameof (MaxAlbumSize)];

    /// <summary>Phrase: Max library size</summary>
    [ResourceEntry("MaxLibrarySize", Description = "The title of the field for setting max library size.", LastModified = "2011/07/14", Value = "Max library size")]
    public string MaxLibrarySize => this[nameof (MaxLibrarySize)];

    /// <summary>Phrase: Max video size</summary>
    [ResourceEntry("MaxVideoSize", Description = "The title of the field for setting max video size.", LastModified = "2011/07/14", Value = "Max video size")]
    public string MaxVideoSize => this[nameof (MaxVideoSize)];

    /// <summary>phrase: Folder</summary>
    [ResourceEntry("Folder", Description = "phrase: Folder", LastModified = "2011/07/12", Value = "Folder")]
    public string Folder => this[nameof (Folder)];

    /// <summary>phrase: Folder is a required field</summary>
    [ResourceEntry("FolderIsRequired", Description = "phrase: Folder is a required field", LastModified = "2011/07/12", Value = "Folder is a required field")]
    public string FolderIsRequired => this[nameof (FolderIsRequired)];

    /// <summary>
    /// phrase: Type the destination folder path, something as:<ul><li>\\fileserver\share</li><li>D:\filedirectory</li><li>~/App_Data/MyStorage</li></ul>
    /// </summary>
    [ResourceEntry("TypeDestinationFolderPath", Description = "phrase: Type the destination folder path, something as:<ul><li>\\\\fileserver\\share</li><li>D:\\filedirectory</li><li>~/App_Data/MyStorage</li></ul>", LastModified = "2011/07/25", Value = "Type the destination folder path, something as:<ul><li>\\\\fileserver\\share</li><li>D:\\filedirectory</li><li>~/App_Data/MyStorage</li></ul>")]
    public string TypeDestinationFolderPath => this[nameof (TypeDestinationFolderPath)];

    /// <summary>phrase: Select a database you want to store items to</summary>
    [ResourceEntry("SelectDatabaseStoreItemsTo", Description = "phrase: Select a database you want to store items to", LastModified = "2011/07/12", Value = "Select a database you want to store items to")]
    public string SelectDatabaseStoreItemsTo => this[nameof (SelectDatabaseStoreItemsTo)];

    /// <summary>phrase: Settings for {0}</summary>
    [ResourceEntry("SettingsFor", Description = "phrase: Settings for {0}", LastModified = "2011/07/13", Value = "Settings for {0}")]
    public string SettingsFor => this[nameof (SettingsFor)];

    /// <summary>phrase: Test settings</summary>
    [ResourceEntry("TestSettings", Description = "phrase: Test settings", LastModified = "2011/07/25", Value = "Test settings")]
    public string TestSettings => this[nameof (TestSettings)];

    /// <summary>phrase: Settings are valid.</summary>
    [ResourceEntry("SettingsAreValid", Description = "phrase: Settings are valid.", LastModified = "2011/07/25", Value = "Settings are valid.")]
    public string SettingsAreValid => this[nameof (SettingsAreValid)];

    /// <summary>phrase: There is a problem with the settings: {0}</summary>
    [ResourceEntry("ThereIsProblemWithTheSettings", Description = "phrase: There is a problem with the settings: {0}", LastModified = "2011/07/25", Value = "There is a problem with the settings: {0}")]
    public string ThereIsProblemWithTheSettings => this[nameof (ThereIsProblemWithTheSettings)];

    /// <summary>phrase: Embedding will work only for published items</summary>
    [ResourceEntry("EmbeddedLinkOnlyForPublieshedContent", Description = "phrase: Embedding will work only for published items", LastModified = "2011/08/10", Value = "Embedding will work only for published items")]
    public string EmbeddedLinkOnlyForPublieshedContent => this[nameof (EmbeddedLinkOnlyForPublieshedContent)];

    /// <summary>
    /// phrase: Account name. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("AccountName", Description = "phrase: Account name. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/22", Value = "Account name")]
    public string AccountName => this[nameof (AccountName)];

    /// <summary>
    /// phrase: Account name is required. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("AccountNameIsRequired", Description = "phrase: Account name is required. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/22", Value = "Account name is required")]
    public string AccountNameIsRequired => this[nameof (AccountNameIsRequired)];

    /// <summary>
    /// phrase: 'Account key or shared access signature (SAS)'; Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("AccountKeyOrSAS", Description = "phrase: 'Account key or shared access signature (SAS)'; Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/29", Value = "Account key or shared access signature (SAS)")]
    public string AccountKeyOrSAS => this[nameof (AccountKeyOrSAS)];

    [ResourceEntry("AccountKeyOrSASExample", Description = "phrase: For shared access signature (SAS) use a token. <strong>Example:</strong> <em>?sr=c&si=myPolicyName&sig=[signature]</em>", LastModified = "2011/12/13", Value = "For shared access signature (SAS) use a token. <strong>Example:</strong> <em>?sr=c&si=myPolicyName&sig=[signature]</em>")]
    public string AccountKeyOrSASExample => this[nameof (AccountKeyOrSASExample)];

    /// <summary>
    /// phrase: 'Either an account key or a shared access signature (SAS) is required.'; Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("AccountKeyOrSASIsRequired", Description = "phrase: 'Either an account key or a shared access signature (SAS) is required.'; Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/29", Value = "Either an account key or a shared access signature (SAS) is required.")]
    public string AccountKeyOrSASIsRequired => this[nameof (AccountKeyOrSASIsRequired)];

    /// <summary>
    /// phrase: Name of your Azure container. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("ContainerName", Description = "phrase: Name of your Azure container. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/12/13", Value = "Name of your Azure container")]
    public string ContainerName => this[nameof (ContainerName)];

    /// <summary>
    /// phrase: If you do not have created a container in Azure it will be automatically created with the name you entered here. <strong>Example:</strong> <em>Images-container</em>. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("ContainerNameExample", Description = "phrase: If you do not have created a container in Azure it will be automatically created with the name you entered here. <strong>Example:</strong> <em>Images-container</em>. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/12/13", Value = "If you do not have created a container in Azure it will be automatically created with the name you entered here. <strong>Example:</strong> <em>Images-container</em>")]
    public string ContainerNameExample => this[nameof (ContainerNameExample)];

    /// <summary>
    /// phrase: Valid container name is required. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("ContainerNameIsRequired", Description = "phrase: Valid container name is required. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/12/12", Value = "Valid container name is required")]
    public string ContainerNameIsRequired => this[nameof (ContainerNameIsRequired)];

    /// <summary>
    /// Title of the 'Public host' field in the Windows Azure blob storage provider basic settings.
    /// </summary>
    [ResourceEntry("PublicHost", Description = "Title of the 'Public host' field in the Windows Azure blob storage provider basic settings.", LastModified = "2011/11/23", Value = "Public host")]
    public string PublicHost => this[nameof (PublicHost)];

    /// <summary>
    /// Example for the 'Public host' field in the Windows Azure blob storage provider basic settings.
    /// </summary>
    [ResourceEntry("PublicHostExample", Description = "Example for the 'Public host' field in the Windows Azure blob storage provider basic settings.", LastModified = "2011/12/13", Value = "Your CDN host or your custom domain host. <strong>Example:</strong> <em>www.myhost.com (custom DNS name), az12345.vo.msecnd.net (CDN URL), https://mystorageaccount.blob.core.windows.net (use HTTPS)</em>")]
    public string PublicHostExample => this[nameof (PublicHostExample)];

    /// <summary>
    /// Validation error message for the 'Public host' field in the Windows Azure blob storage provider basic settings.
    /// </summary>
    [ResourceEntry("PublicHostIsInvalid", Description = "Validation error message for the 'Public host' field in the Windows Azure blob storage provider basic settings.", LastModified = "2011/11/23", Value = "Invalid public host.")]
    public string PublicHostIsInvalid => this[nameof (PublicHostIsInvalid)];

    /// <summary>
    /// Exaplainatino message for the 'Container name' field in the Windows Azure blob storage provider basic settings.
    /// </summary>
    [ResourceEntry("ContainerNameExplaination", Description = "Exaplainatino message for the 'Container name' field in the Windows Azure blob storage provider basic settings.", LastModified = "2011/12/12", Value = "A container name must be a valid DNS name, conforming to the following naming rules: <ol><li>Container names must start with a letter or number, and can contain only letters, numbers, and the dash (-) character</li><li>Every dash (-) character must be immediately preceded and followed by a letter or number; consecutive dashes are not permitted in container names</li><li>All letters in a container name must be lowercase</li><li>Container names must be from 3 through 63 characters long.</li></ol>")]
    public string ContainerNameExplaination => this[nameof (ContainerNameExplaination)];

    /// <summary>
    /// Validation error message for the 'Account name' field in the Windows Azure blob storage provider basic settings.
    /// </summary>
    [ResourceEntry("AccountNameIsInvalid", Description = "Validation error message for the 'Account name' field in the Windows Azure blob storage provider basic settings.", LastModified = "2011/11/28", Value = "Invalid account name. The storage name must be between 3 and 24 characters in length  and use lower-case letters and numbers only.")]
    public string AccountNameIsInvalid => this[nameof (AccountNameIsInvalid)];

    /// <summary>
    /// phrase: Use SSL for storage management. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("UseHttps", Description = "phrase: Use SSL for storage management. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/24", Value = "Use SSL for storage management")]
    public string UseHttps => this[nameof (UseHttps)];

    /// <summary>
    /// phrase: Use local development storage. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("UseLocalDevelopmentStorage", Description = "phrase: Use local development storage. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/22", Value = "Use local development storage")]
    public string UseLocalDevelopmentStorage => this[nameof (UseLocalDevelopmentStorage)];

    /// <summary>
    /// phrase: Sample blob url:. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("SampleBlobUrl", Description = "phrase: Sample blob url:. Used in Blob storage provider basic settings Azure storage", LastModified = "2011/11/22", Value = "Sample blob url:")]
    public string SampleBlobUrl => this[nameof (SampleBlobUrl)];

    /// <summary>
    /// phrase: Sample blob url:. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("RelocateLibrary", Description = "Change library Url context menu item in backend library grid action menu", LastModified = "2011/12/01", Value = "Change library URL")]
    public string RelocateLibrary => this[nameof (RelocateLibrary)];

    /// <summary>word: Move to</summary>
    /// <value>MoveTo</value>
    [ResourceEntry("MoveTo", Description = "Redirects to the 'Move To Library' dialog", LastModified = "2013/07/22", Value = "Move to...")]
    public string MoveTo => this[nameof (MoveTo)];

    /// <summary>
    /// phrase: Sample blob url:. Used in Blob storage provider basic settings Azure storage
    /// </summary>
    [ResourceEntry("TransferLibrary", Description = "Transfer library context menu item in backend library grid action menu", LastModified = "2011/12/01", Value = "Move to another storage")]
    public string TransferLibrary => this[nameof (TransferLibrary)];

    /// <summary>
    /// Relocation warning label in move to another storage and change url screen
    /// </summary>
    [ResourceEntry("RelocationWarning", Description = "Relocation warning label in move to another storage and change url screen", LastModified = "2011/12/05", Value = "Please note that this operation may take some time (up to few hours).<br />Transferring of the items in the library will not affect the public pages which are using them, but during this process you will not be able to edit or delete these items")]
    public string MoveStorageWarning => this["RelocationWarning"];

    /// <summary>
    /// Relocation warning label in move to another storage and change url screen
    /// </summary>
    [ResourceEntry("EditUrlNameWarning", Description = "Editing URL may take some time (up to few hours). During the process of generating new URLs for the items in the library you will not be able to edit or delete these items.", LastModified = "2020/01/07", Value = "Editing URL may take some time (up to few hours). During the process of generating new URLs for the items in the library you will not be able to edit or delete these items.")]
    public string EditUrlNameWarning => this[nameof (EditUrlNameWarning)];

    /// <summary>
    /// Relocation warning label in move to another storage and change url screen
    /// </summary>
    [ResourceEntry("ChangeLibraryUrlWarning", Description = "Relocation warning label in move to another storage and change url screen", LastModified = "2011/12/05", Value = "Changing the URL will break all links that point to this library and its items. This action might take significant time and extra cost for cloud storages.")]
    public string ChangeLibraryUrlWarning => this[nameof (ChangeLibraryUrlWarning)];

    /// <summary>
    /// Message displayed on the client on attempt to edit media content, while the library is in process of moving to another storage.
    /// </summary>
    [ResourceEntry("UnableToEditItemBecauseOfLibraryStorageChangeTask", Description = "Message displayed on the client on attempt to edit media content, while the library is in process of moving to another storage.", LastModified = "2011/12/12", Value = "You are not allowed to edit this item, because the library it belongs to is in process of moving to another storage. Try again in a while.")]
    public string UnableToEditItemBecauseOfLibraryStorageChangeTask => this[nameof (UnableToEditItemBecauseOfLibraryStorageChangeTask)];

    /// <summary>
    /// Message displayed on the client on attempt to edit media content, while the library is in process of changing URL.
    /// </summary>
    [ResourceEntry("UnableToEditItemBecauseOfLibraryUrlChangeTask", Description = "Message displayed on the client on attempt to edit media content, while the library is in process of changing URL.", LastModified = "2011/12/12", Value = "You are not allowed to edit this item, because the library it belongs to is in process of changing URL. Try again in a while.")]
    public string UnableToEditItemBecauseOfLibraryUrlChangeTask => this[nameof (UnableToEditItemBecauseOfLibraryUrlChangeTask)];

    /// <summary>
    /// Message displayed on the client on attempt to insert an image, while there are no library providers configured for the current site.
    /// </summary>
    [ResourceEntry("NoImageLibrariesWarningMessage", Description = "Message displayed on the client on attempt to insert an image, while there are no library providers configured for the current site.", LastModified = "2012/10/04", Value = "Image cannot be inserted because there are no libraries. Please contact your administrator for more details.")]
    public string NoImageLibrariesWarningMessage => this[nameof (NoImageLibrariesWarningMessage)];

    /// <summary>
    /// Message displayed on the client on attempt to insert a video, while there are no library providers configured for the current site.
    /// </summary>
    [ResourceEntry("NoVideoLibrariesWarningMessage", Description = "Message displayed on the client on attempt to insert a video, while there are no library providers configured for the current site.", LastModified = "2012/10/04", Value = "Video cannot be inserted because there are no libraries. Please contact your administrator for more details.")]
    public string NoVideoLibrariesWarningMessage => this[nameof (NoVideoLibrariesWarningMessage)];

    /// <summary>
    /// Message displayed on the client on attempt to insert a document, while there are no library providers configured for the current site.
    /// </summary>
    [ResourceEntry("NoDocumentLibrariesWarningMessage", Description = "Message displayed on the client on attempt to insert a document, while there are no library providers configured for the current site.", LastModified = "2012/10/04", Value = "Document cannot be inserted because there are no libraries. Please contact your administrator for more details.")]
    public string NoDocumentLibrariesWarningMessage => this[nameof (NoDocumentLibrariesWarningMessage)];

    /// <summary>
    /// Message displayed on the client on attempt to insert media content, while there are no library providers configured for the current site.
    /// </summary>
    [ResourceEntry("NoLibrariesWarningMessage", Description = "Message displayed on the client on attempt to insert media content, while there are no library providers configured for the current site.", LastModified = "2012/10/04", Value = "Item cannot be inserted because there are no libraries. Please contact your administrator for more details.")]
    public string NoLibrariesWarningMessage => this[nameof (NoLibrariesWarningMessage)];

    /// <summary>
    /// A phrase representing a count of images. Should be formatted with one argument.
    /// </summary>
    [ResourceEntry("ImagesCountFormat", Description = "A phrase representing a count of images. Should be formatted with one argument.", LastModified = "2013/02/20", Value = "<b>{0}</b> images")]
    public string ImagesCountFormat => this[nameof (ImagesCountFormat)];

    /// <summary>
    /// A phrase representing a count of videos. Should be formatted with one argument.
    /// </summary>
    [ResourceEntry("VideosCountFormat", Description = "A phrase representing a count of videos. Should be formatted with one argument.", LastModified = "2013/02/20", Value = "<b>{0}</b> videos")]
    public string VideosCountFormat => this[nameof (VideosCountFormat)];

    /// <summary>
    /// A phrase representing a count of documents. Should be formatted with one argument.
    /// </summary>
    [ResourceEntry("DocumentsCountFormat", Description = "A phrase representing a count of documents. Should be formatted with one argument.", LastModified = "2013/02/20", Value = "<b>{0}</b> documents")]
    public string DocumentsCountFormat => this[nameof (DocumentsCountFormat)];

    /// <summary>
    /// A phrase representing a count of libraries. Should be formatted with one argument.
    /// </summary>
    [ResourceEntry("LibrariesCountFormat", Description = "A phrase representing a count of libraries. Should be formatted with one argument.", LastModified = "2013/02/20", Value = "{0} libraries")]
    public string LibrariesCountFormat => this[nameof (LibrariesCountFormat)];

    /// <summary>by Storage...</summary>
    [ResourceEntry("FilterByStorage", Description = "The 'by Storage...' label in the sidebar.", LastModified = "2010/07/19", Value = "by Storage...")]
    public string FilterByStorage => this[nameof (FilterByStorage)];

    /// <summary>by Date...</summary>
    [ResourceEntry("FilterByDate", Description = "The 'by Date...' label in the sidebar.", LastModified = "2010/07/19", Value = "by Date...")]
    public string FilterByDate => this[nameof (FilterByDate)];

    /// <summary>Phrase: Close storages</summary>
    [ResourceEntry("CloseStorageFilter", Description = "The link for closing the storage filter widget in the sidebar.", LastModified = "2010/07/19", Value = "Close storages")]
    public string CloseStorageFilter => this[nameof (CloseStorageFilter)];

    /// <summary>Phrase: Libraries by storage</summary>
    [ResourceEntry("LibrariesByStorage", Description = "phrase: Libraries by storage", LastModified = "2010/07/19", Value = "Libraries by storage")]
    public string LibrariesByStorage => this[nameof (LibrariesByStorage)];

    /// <summary>Phrase: Filter libraries</summary>
    [ResourceEntry("FilterLibraries", Description = "phrase: Filter libraries", LastModified = "2010/07/19", Value = "Filter libraries")]
    public string FilterLibraries => this[nameof (FilterLibraries)];

    /// <summary>Phrase: All libraries</summary>
    [ResourceEntry("AllLibraries", Description = "phrase: All libraries", LastModified = "2010/07/19", Value = "All libraries")]
    public string AllLibraries => this[nameof (AllLibraries)];

    /// <summary>Phrase: My libraries</summary>
    [ResourceEntry("MyLibraries", Description = "phrase: My libraries", LastModified = "2010/07/19", Value = "My libraries")]
    public string MyLibraries => this[nameof (MyLibraries)];

    /// <summary>Phrase: More Libraries</summary>
    [ResourceEntry("MoreLibraries", Description = "phrase: More Libraries", LastModified = "2010/07/19", Value = "More Libraries")]
    public string MoreLibraries => this[nameof (MoreLibraries)];

    /// <summary>Phrase: Less Libraries</summary>
    [ResourceEntry("LessLibraries", Description = "phrase: Less Libraries", LastModified = "2010/07/15", Value = "Less Libraries")]
    public string LessLibraries => this[nameof (LessLibraries)];

    /// <summary>Phrase: Display libraries modified in...</summary>
    [ResourceEntry("LibrariesByDate", Description = "phrase: Display libraries modified in...", LastModified = "2011/07/19", Value = "Display libraries modified in...")]
    public string LibrariesByDate => this[nameof (LibrariesByDate)];

    /// <summary>
    /// Phrase: Libraries Move to another storage dialog storage selector title
    /// </summary>
    [ResourceEntry("MoveDialogStorageSelectorTitle", Description = "Libraries Move to another storage dialog storage selector title", LastModified = "2011/12/13", Value = "Select a storage where this library is to be moved")]
    public string MoveDialogStorageSelectorTitle => this[nameof (MoveDialogStorageSelectorTitle)];

    /// <summary>Phrase: (default)</summary>
    [ResourceEntry("SitefinityDefaultConnection", Description = "phrase: '(default)' in edit/add blob storage provider database connection's dropdown", LastModified = "2011/07/20", Value = "(default)")]
    public string SitefinityDefaultConnection => this[nameof (SitefinityDefaultConnection)];

    /// <summary>
    /// phrase: The title of the done button in the move library dialog
    /// </summary>
    [ResourceEntry("MoveLibrary", Description = "The title of the done button in the move library dialog", LastModified = "2011/12/13", Value = "Move library")]
    public string MoveLibrary => this[nameof (MoveLibrary)];

    /// <summary>
    /// phrase: The title of the done button in the move library dialog
    /// </summary>
    [ResourceEntry("MovingLibrary", Description = "The description of the move library task.", LastModified = "2015/09/28", Value = "Moving library <em>{0}</em> to <em>{1}</em>")]
    public string MovingLibrary => this[nameof (MovingLibrary)];

    /// <summary>
    /// phrase: The title of the move to another storage dialog
    /// </summary>
    [ResourceEntry("MoveLibraryDialogTitle", Description = "The title of the move to another storage dialog", LastModified = "2011/12/13", Value = "Move a library to another storage")]
    public string MoveLibraryDialogTitle => this[nameof (MoveLibraryDialogTitle)];

    /// <summary>phrase: Select {0} library</summary>
    [ResourceEntry("SelectLibraryCommon", Description = "phrase: Select {0} library", LastModified = "2013/02/25", Value = "Select {0} library")]
    public string SelectLibraryCommon => this[nameof (SelectLibraryCommon)];

    /// <summary>Phrase: The media item has been unpublished</summary>
    /// <value>The {0} has been unpublished</value>
    [ResourceEntry("UnpublishedMediaItemMessage", Description = "Phrase: The media item has been unpublished", LastModified = "2014/04/17", Value = "The {0} has been unpublished")]
    public string UnpublishedMediaItemMessage => this[nameof (UnpublishedMediaItemMessage)];

    /// <summary>phrase: Streaming provider</summary>
    /// <value>Streaming provider</value>
    [ResourceEntry("BlobStorageStreamingProviderName", Description = "phrase: Streaming provider", LastModified = "2013/09/09", Value = "Streaming provider")]
    public string BlobStorageStreamingProviderName => this[nameof (BlobStorageStreamingProviderName)];

    /// <summary>phrase: Streaming provider</summary>
    /// <value>Streaming provider</value>
    [ResourceEntry("BlobStorageStreamingProviderTitle", Description = "phrase: Streaming provider", LastModified = "2013/09/09", Value = "Streaming provider")]
    public string BlobStorageStreamingProviderTitle => this[nameof (BlobStorageStreamingProviderTitle)];

    /// <summary>
    /// phrase: Uses the Open Access Streaming API to upload blobs to the database.
    /// </summary>
    /// <value>Uses the Open Access Streaming API to upload blobs to the database.</value>
    [ResourceEntry("BlobStorageStreamingProviderDescription", Description = "phrase: Uses the Open Access Streaming API to upload blobs to the database.", LastModified = "2013/09/09", Value = "Uses the Open Access Streaming API to upload blobs to the database.")]
    public string BlobStorageStreamingProviderDescription => this[nameof (BlobStorageStreamingProviderDescription)];

    /// <summary>Phrase: Bulk edit documents</summary>
    /// <value>Bulk edit documents</value>
    [ResourceEntry("BulkEditDocuments", Description = "Phrase: Bulk edit documents", LastModified = "2014/04/09", Value = "Bulk edit documents")]
    public string BulkEditDocuments => this[nameof (BulkEditDocuments)];

    /// <summary>Phrase: Bulk edit user files</summary>
    /// <value>Bulk edit user files</value>
    [ResourceEntry("BulkEditUserFiles", Description = "Phrase: Bulk edit user files", LastModified = "2014/04/09", Value = "Bulk edit user files")]
    public string BulkEditUserFiles => this[nameof (BulkEditUserFiles)];

    /// <summary>Phrase: Set as library cover</summary>
    /// <value>Set as cover</value>
    [ResourceEntry("SetAsCoverMenuItemTitle", Description = "Set as library cover menu item title", LastModified = "2015/01/08", Value = "Set as library cover")]
    public string SetAsCoverMenuItemTitle => this[nameof (SetAsCoverMenuItemTitle)];

    /// <summary>Phrase: URL contains invalid symbols</summary>
    [ResourceEntry("UrlNameInvalidSymbols", Description = "The message shown when the url contains invalid symbols.", LastModified = "2010/07/13", Value = "The URL contains invalid symbols.")]
    public string UrlNameInvalidSymbols => this[nameof (UrlNameInvalidSymbols)];

    /// <summary>Phrase: My content</summary>
    [ResourceEntry("ShowMy", Description = "phrase: My {0}", LastModified = "2013/02/25", Value = "My {0}")]
    public string ShowMy => this[nameof (ShowMy)];

    /// <summary>Phrase: All content</summary>
    [ResourceEntry("ShowAll", Description = "phrase: All {0}", LastModified = "2013/03/07", Value = "All {0}")]
    public string ShowAll => this[nameof (ShowAll)];

    /// <summary>Phrase: Recent {0}</summary>
    [ResourceEntry("ShowRecent", Description = "phrase: Recent {0}", LastModified = "2013/02/25", Value = "Recent {0}")]
    public string ShowRecent => this[nameof (ShowRecent)];

    /// <summary>Phrase: Select {0} From Already Uploaded</summary>
    [ResourceEntry("SelectFromAlreadyUploaded", Description = "phrase: Select {0} from already uploaded", LastModified = "2014/01/10", Value = "Select {0} from already uploaded")]
    public string SelectFromAlreadyUploaded => this[nameof (SelectFromAlreadyUploaded)];

    /// <summary>Phrase: Libraries</summary>
    [ResourceEntry("Libraries", Description = "phrase: Libraries", LastModified = "2013/02/25", Value = "Libraries")]
    public string Libraries => this[nameof (Libraries)];

    /// <summary>
    /// The title of the Root library settings section in the library details dialog.
    /// </summary>
    /// <value>Root library settings</value>
    [ResourceEntry("RootLibrarySettings", Description = "The title of the Root library settings section in the library details dialog.", LastModified = "2013/02/27", Value = "Root library settings")]
    public string RootLibrarySettings => this[nameof (RootLibrarySettings)];

    /// <summary>The title of the image item name.</summary>
    [ResourceEntry("ImageItemName", Description = "The title of the image item name.", LastModified = "2013/02/28", Value = "Image")]
    public string ImageItemName => this[nameof (ImageItemName)];

    /// <summary>The title of the document item name.</summary>
    [ResourceEntry("DocumentItemName", Description = "The title of the document item name.", LastModified = "2013/02/28", Value = "Document")]
    public string DocumentItemName => this[nameof (DocumentItemName)];

    /// <summary>The title of the video item name.</summary>
    [ResourceEntry("VideoItemName", Description = "The title of the video item name.", LastModified = "2013/02/28", Value = "Video")]
    public string VideoItemName => this[nameof (VideoItemName)];

    /// <summary>Change parent library</summary>
    [ResourceEntry("ChangeParentLibrary", Description = "Change parent library", LastModified = "2013/03/05", Value = "Change parent library")]
    public string ChangeParentLibrary => this[nameof (ChangeParentLibrary)];

    /// <summary>Text shown in the Html5 Upload prompt</summary>
    /// <value>Drag {0} here to upload</value>
    [ResourceEntry("DragAndDropWhileDragging", Description = "Text shown in the Html5 Upload prompt", LastModified = "2013/04/12", Value = "Drag {0} here to upload")]
    public string DragAndDropWhileDragging => this[nameof (DragAndDropWhileDragging)];

    /// <summary>
    /// Text shown in the Html5 Upload prompt before dragging files
    /// </summary>
    /// <value><span class="sfNoFilesLbl"><b>Select {0} from your computer</b> <br />or simply drag &amp; drop it here</span><span class="sfFilesSelectedLbl">Add more {0}s</span></value>
    [ResourceEntry("DragAndDropBeforeDragging", Description = "Text shown in the Html5 Upload prompt before dragging files", LastModified = "2013/04/12", Value = "<span class=\"sfNoFilesLbl\"><b>Select {0} from your computer</b> <br/>or simply drag &amp; drop it here</span><span class=\"sfFilesSelectedLbl\">Add more {0}s</span>")]
    public string DragAndDropBeforeDragging => this[nameof (DragAndDropBeforeDragging)];

    /// <summary>
    /// Text shown in the Html5 Upload prompt before dragging files
    /// </summary>
    /// <value>&lt;span class=\"sfNoFilesLbl\"&gt;&lt;b&gt;Select {0} from your computer&lt;/b&gt; &lt;br/&gt;or simply drag &amp; drop it here&lt;/span&gt;&lt;span class=\"sfFilesSelectedLbl\"&gt;Add more {0}s&lt;/span&gt;</value>
    [ResourceEntry("DragAndDropBeforeDraggingEscaped", Description = "Text shown in the Html5 Upload prompt before dragging files", LastModified = "2017/05/19", Value = "&lt;span class=&#92;&#34;sfNoFilesLbl&#92;&#34;&gt;&lt;b&gt;Select {0} from your computer&lt;/b&gt; &lt;br/&gt;or simply drag &amp; drop it here&lt;/span&gt;&lt;span class=&#92;&#34;sfFilesSelectedLbl&#92;&#34;&gt;Add more {0}s&lt;/span&gt;")]
    public string DragAndDropBeforeDraggingEscaped => this[nameof (DragAndDropBeforeDraggingEscaped)];

    /// <summary>
    /// Text shown when the user is trying to upload more files than the maximum limit
    /// </summary>
    /// <value>Please select {0} files or less</value>
    [ResourceEntry("UploadFileLimitReachedPlural", Description = "Text shown when the user is trying to upload more files than the maximum limit", LastModified = "2013/04/12", Value = "Please select {0} files or less")]
    public string UploadFileLimitReachedPlural => this[nameof (UploadFileLimitReachedPlural)];

    /// <summary>
    /// Text shown when the user is trying to upload more than one file (when replacing)
    /// </summary>
    /// <value>Please select only 1 file.</value>
    [ResourceEntry("UploadFileLimitReachedSingular", Description = "Text shown when the user is trying to upload more than one file (when replacing)", LastModified = "2013/04/12", Value = "Please select only 1 file.")]
    public string UploadFileLimitReachedSingular => this[nameof (UploadFileLimitReachedSingular)];

    /// <summary>
    /// Error message shown in the HTML5 Upload dialog when the user selects a file with a not allowed extension
    /// </summary>
    /// <value>Please select only files with the following extensions: {0}</value>
    [ResourceEntry("AllowedExtensionsErrorMessage", Description = "Error message shown in the HTML5 Upload dialog when the user selects a file with a not allowed extension", LastModified = "2013/04/19", Value = "Please select only files with the following extensions: {0}")]
    public string AllowedExtensionsErrorMessage => this[nameof (AllowedExtensionsErrorMessage)];

    /// <summary>phrase: Applied to</summary>
    /// <value>Applied to</value>
    [ResourceEntry("AppliedTo", Description = "phrase: Applied to", LastModified = "2013/05/28", Value = "Applied to")]
    public string AppliedTo => this[nameof (AppliedTo)];

    /// <summary>phrase: Thumbnails</summary>
    /// <value>Thumbnails</value>
    [ResourceEntry("ThumbnailProfiles", Description = "phrase: Thumbnails", LastModified = "2013/06/07", Value = "Thumbnails")]
    public string ThumbnailProfiles => this[nameof (ThumbnailProfiles)];

    /// <summary>phrase: Image Thumbnails</summary>
    /// <value>Image thumbnails</value>
    [ResourceEntry("ImageThumbnails", Description = "phrase: Image Thumbnails", LastModified = "2013/05/29", Value = "Image thumbnails")]
    public string ImageThumbnails => this[nameof (ImageThumbnails)];

    /// <summary>Information for what the thumbnails are.</summary>
    /// <value>Thumbnails are copies of the original image generated according to predefined settings. They can be applied to Image libraries.</value>
    [ResourceEntry("ThumbnailsInfo", Description = "Information for what the thumbnails are.", LastModified = "2013/05/29", Value = "Thumbnails are copies of the original image generated according to predefined settings. They can be applied to Image libraries.")]
    public string ThumbnailsInfo => this[nameof (ThumbnailsInfo)];

    /// <summary>phrase: Add an image thumbnail size.</summary>
    /// <value>Add an image thumbnail size</value>
    [ResourceEntry("AddImageThumbnailProfile", Description = "phrase: Add an image thumbnail size.", LastModified = "2013/05/29", Value = "Add an image thumbnail size")]
    public string AddImageThumbnailProfile => this[nameof (AddImageThumbnailProfile)];

    /// <summary>phrase: Thumbnail 120x120</summary>
    /// <value><strong>Example: </strong>Thumbnail 120x120</value>
    [ResourceEntry("ThumbnailProfileTitleExample", Description = "phrase: Thumbnail 120x120", LastModified = "2013/07/02", Value = "<strong>Example: </strong>Thumbnail 120x120")]
    public string ThumbnailProfileTitleExample => this[nameof (ThumbnailProfileTitleExample)];

    /// <summary>phrase: For developers: name used in code</summary>
    /// <value>For developers: name used in code</value>
    [ResourceEntry("ThumbnailProfileNameTitle", Description = "phrase: For developers: name used in code", LastModified = "2013/05/29", Value = "For developers: name used in code")]
    public string ThumbnailProfileNameTitle => this[nameof (ThumbnailProfileNameTitle)];

    /// <summary>phrase: Example</summary>
    /// <value>Example:</value>
    [ResourceEntry("Example", Description = "phrase: Example", LastModified = "2013/05/29", Value = "Example:")]
    public string Example => this[nameof (Example)];

    /// <summary>phrase: A profile with the same name already exists.</summary>
    /// <value>A thumbnail size with the same name already exists.</value>
    [ResourceEntry("ThumbnailProfileExists", Description = "phrase: A profile with the same name already exists.", LastModified = "2013/07/04", Value = "A thumbnail size with the same name already exists.")]
    public string ThumbnailProfileExists => this[nameof (ThumbnailProfileExists)];

    /// <summary>phrase: Thumbnail generator has no methods.</summary>
    /// <value>Thumbnail generator has no methods.</value>
    [ResourceEntry("GeneratorHasNoMethods", Description = "phrase: Thumbnail generator has no methods.", LastModified = "2013/05/31", Value = "Thumbnail generator has no methods.")]
    public string GeneratorHasNoMethods => this[nameof (GeneratorHasNoMethods)];

    /// <summary>
    /// phrase: The selected method for the {0} profile does not exist.
    /// </summary>
    /// <value>The selected method for the {0} profile does not exist.</value>
    [ResourceEntry("InvalidMethodName", Description = "phrase: The selected method {0} does not exist.", LastModified = "2013/06/06", Value = "The selected method for the {0} profile does not exist.")]
    public string InvalidMethodName => this[nameof (InvalidMethodName)];

    /// <summary>
    /// phrase: When creating an image library select this size by default.
    /// </summary>
    /// <value>When creating an image library select this size by default.</value>
    [ResourceEntry("SetProfileAsDefault", Description = "phrase: When creating an image library select this size by default.", LastModified = "2013/06/06", Value = "When creating an image library select this size by default.")]
    public string SetProfileAsDefault => this[nameof (SetProfileAsDefault)];

    /// <summary>prase: Field must have an integer value.</summary>
    /// <value>Value must be an integer between 1 and 9999 inclusive.</value>
    [ResourceEntry("ValueMustBeInteger", Description = "prase: Field must have an integer value.", LastModified = "2013/07/11", Value = "Value must be an integer between 1 and 9999 inclusive.")]
    public string ValueMustBeInteger => this[nameof (ValueMustBeInteger)];

    /// <summary>phrase: Selected by default.</summary>
    /// <value>Selected by default</value>
    [ResourceEntry("SelectedByDefault", Description = "phrase: Selected by default.", LastModified = "2013/06/07", Value = "Selected by default")]
    public string SelectedByDefault => this[nameof (SelectedByDefault)];

    /// <summary>phrase: Invalid format for developer name.</summary>
    /// <value>Invalid format for developer name.</value>
    [ResourceEntry("InvalidProfileName", Description = "phrase: Invalid format for developer name.", LastModified = "2013/07/01", Value = "Invalid format for developer name.")]
    public string InvalidProfileName => this[nameof (InvalidProfileName)];

    /// <summary>phrase: Regenerate thumbnails.</summary>
    /// <value>Regenerate thumbnails</value>
    [ResourceEntry("RegenerateThumbnails", Description = "phrase: Regenerate thumbnails.", LastModified = "2013/06/07", Value = "Regenerate thumbnails")]
    public string RegenerateThumbnails => this[nameof (RegenerateThumbnails)];

    /// <summary>word: Regenerate.</summary>
    /// <value>Regenerate</value>
    [ResourceEntry("Regenerate", Description = "word: Regenerate.", LastModified = "2013/06/12", Value = "Regenerate")]
    public string Regenerate => this[nameof (Regenerate)];

    /// <summary>
    /// The text to be shown when a user action is to be confirmed.
    /// </summary>
    /// <value>Are you sure you want to proceed?</value>
    [ResourceEntry("RegenerateThumbnailsInLibraryTitle", Description = "Regenerate thumbnails", LastModified = "2013/07/02", Value = "Regenerate thumbnails")]
    public string RegenerateThumbnailsInLibraryTitle => this[nameof (RegenerateThumbnailsInLibraryTitle)];

    /// <summary>
    /// phrase: The thumbnails in the library will be regenerated.
    /// </summary>
    /// <value>The thumbnails in the library will be regenerated.</value>
    [ResourceEntry("RegenerateThumbnailsInLibraryMessage", Description = "phrase: The thumbnails in the library will be regenerated.", LastModified = "2013/06/12", Value = "The thumbnails in the library will be regenerated.")]
    public string RegenerateThumbnailsInLibraryMessage => this[nameof (RegenerateThumbnailsInLibraryMessage)];

    /// <summary>
    /// phrase: The thumbnails for this library need to be regenerated.
    /// </summary>
    /// <value>The thumbnails for this library need to be regenerated.</value>
    [ResourceEntry("ThumbnailsNeedRegeneration", Description = "phrase: The thumbnails for this library need to be regenerated.", LastModified = "2013/06/12", Value = "The thumbnails for this library need to be regenerated.")]
    public string ThumbnailsNeedRegeneration => this[nameof (ThumbnailsNeedRegeneration)];

    /// <summary>
    /// phrase: Thumbnail images in {0} libraries will be regenerated to reflect the changes.
    /// </summary>
    /// <value>Thumbnail {0} in {1} libraries will be regenerated to reflect the changes.</value>
    [ResourceEntry("ThumbnailsForMultipleLibrariesNeedRegen", Description = "phrase: Thumbnail images in {0} libraries will be regenerated to reflect the changes.", LastModified = "2013/06/18", Value = "Thumbnail {0} in {1} libraries will be regenerated to reflect the changes.")]
    public string ThumbnailsForMultipleLibrariesNeedRegen => this[nameof (ThumbnailsForMultipleLibrariesNeedRegen)];

    /// <summary>phrase: Regenerating thumbnails</summary>
    /// <value>Regenerating thumbnails</value>
    [ResourceEntry("RegeneratingThumbnails", Description = "phrase: Regenerating thumbnails", LastModified = "2013/06/12", Value = "Regenerating thumbnails")]
    public string RegeneratingThumbnails => this[nameof (RegeneratingThumbnails)];

    /// <summary>phrase: Regenerating thumbnails failed</summary>
    /// <value>Regenerating thumbnails failed</value>
    [ResourceEntry("RegeneratingThumbnailsFailed", Description = "phrase: Regenerating thumbnails failed", LastModified = "2013/06/12", Value = "Regenerating thumbnails failed")]
    public string RegeneratingThumbnailsFailed => this[nameof (RegeneratingThumbnailsFailed)];

    /// <summary>phrase: Thumbnail settings</summary>
    /// <value>Thumbnail settings</value>
    [ResourceEntry("ThumbnailSettings", Description = "phrase: Thumbnail settings", LastModified = "2013/06/18", Value = "Thumbnails settings")]
    public string ThumbnailSettings => this[nameof (ThumbnailSettings)];

    /// <summary>phrase: Changing thumbnail sizes</summary>
    /// <value>Changing thumbnail sizes</value>
    [ResourceEntry("ThumbnailSettingsTooltipTitle", Description = "phrase: Changing thumbnail sizes", LastModified = "2020/03/06", Value = "Changing thumbnail sizes")]
    public string ThumbnailSettingsTooltipTitle => this[nameof (ThumbnailSettingsTooltipTitle)];

    /// <summary>
    /// phrase: This operation may take from few seconds up to few hours depending on the number of images you have in the current library. If you are removing existing sizes deleting of the thumbnails may affect the public pages which are using them.
    /// </summary>
    /// <value>This operation may take from few seconds up to few hours depending on the number of images you have in the current library. If you are removing existing sizes deleting of the thumbnails may affect the public pages which are using them.</value>
    [ResourceEntry("ThumbnailSettingsTooltip", Description = "phrase: This operation may take from few seconds up to few hours depending on the number of images you have in the current library. If you are removing existing sizes deleting of the thumbnails may affect the public pages which are using them.", LastModified = "2020/03/06", Value = "This operation may take from few seconds up to few hours depending on the number of images you have in the current library. If you are removing existing sizes deleting of the thumbnails may affect the public pages which are using them.")]
    public string ThumbnailSettingsTooltip => this[nameof (ThumbnailSettingsTooltip)];

    /// <summary>
    /// phrase: This operation may take from few seconds up to few hours depending on the size of the current library. Transferring of the items in the library will not affect the public pages, but during this process you will not be able to edit or delete these items.
    /// </summary>
    /// <value>This operation may take from few seconds up to few hours depending on the size of the current library. Transferring of the items in the library will not affect the public pages, but during this process you will not be able to edit or delete these items.</value>
    [ResourceEntry("StorageProviderTooltip", Description = "phrase: This operation may take from few seconds up to few hours depending on the size of the current library. Transferring of the items in the library will not affect the public pages, but during this process you will not be able to edit or delete these items.", LastModified = "2020/03/12", Value = "This operation may take from few seconds up to few hours depending on the size of the current library. Transferring of the items in the library will not affect the public pages, but during this process you will not be able to edit or delete these items.")]
    public string StorageProviderTooltip => this[nameof (StorageProviderTooltip)];

    /// <summary>phrase: Changing storage provider</summary>
    /// <value>Changing storage provider</value>
    [ResourceEntry("StorageProviderTooltipTitle", Description = "phrase: Changing storage provider", LastModified = "2020/03/12", Value = "Changing storage provider")]
    public string StorageProviderTooltipTitle => this[nameof (StorageProviderTooltipTitle)];

    /// <summary>phrase: View all sizes</summary>
    /// <value>View all sizes</value>
    [ResourceEntry("ViewAllThumbnailSizes", Description = "phrase: View all sizes", LastModified = "2013/06/18", Value = "View all sizes")]
    public string ViewAllThumbnailSizes => this[nameof (ViewAllThumbnailSizes)];

    /// <summary>phrase: You cannot delete these settings.</summary>
    /// <value>You cannot delete these settings.</value>
    [ResourceEntry("CannotDeleteSettings", Description = "phrase: You cannot delete these settings.", LastModified = "2013/06/21", Value = "You cannot delete these settings.")]
    public string CannotDeleteSettings => this[nameof (CannotDeleteSettings)];

    /// <summary>
    /// phrase: You cannot delete this set because it is applied to {0} libraries. You need to remove it from these libraries first.
    /// </summary>
    /// <value>You cannot delete this set because it is applied to {0} libraries. You need to remove it from these libraries first.</value>
    [ResourceEntry("CannotDeleteSettingsInfo", Description = "phrase: You cannot delete this set because it is applied to {0} libraries. You need to remove it from these libraries first.", LastModified = "2013/06/21", Value = "You cannot delete this set because it is applied to {0} libraries. You need to remove it from these libraries first.")]
    public string CannotDeleteSettingsInfo => this[nameof (CannotDeleteSettingsInfo)];

    /// <summary>phrase: Max Height</summary>
    /// <value>Max height</value>
    [ResourceEntry("MaxHeight", Description = "phrase: Max Height", LastModified = "2013/06/21", Value = "Max height")]
    public string MaxHeight => this[nameof (MaxHeight)];

    /// <summary>phrase: Max Width</summary>
    /// <value>Max width</value>
    [ResourceEntry("MaxWidth", Description = "phrase: Max Width", LastModified = "2013/06/21", Value = "Max width")]
    public string MaxWidth => this[nameof (MaxWidth)];

    /// <summary>phrase: Thumbnails of original images</summary>
    /// <value>Thumbnails of original images</value>
    [ResourceEntry("ThumbnailsOfImages", Description = "phrase: Thumbnails of original images", LastModified = "2013/07/01", Value = "Thumbnails of original images")]
    public string ThumbnailsOfImages => this[nameof (ThumbnailsOfImages)];

    /// <summary>phrase: Generate thumbnails with selected sizes...</summary>
    /// <value>Generate thumbnails with selected sizes...</value>
    [ResourceEntry("GenerateThumbnails", Description = "phrase: Generate thumbnails with selected sizes...", LastModified = "2013/06/21", Value = "Generate thumbnails with selected sizes...")]
    public string GenerateThumbnails => this[nameof (GenerateThumbnails)];

    /// <summary>phrase: Do not generate thumbnails</summary>
    /// <value>Do not generate thumbnails</value>
    [ResourceEntry("DoNotGenerateThumbnails", Description = "phrase: Do not generate thumbnails", LastModified = "2013/06/21", Value = "Do not generate thumbnails")]
    public string DoNotGenerateThumbnails => this[nameof (DoNotGenerateThumbnails)];

    /// <summary>phrase: Thumbnail sizes</summary>
    /// <value>Thumbnail sizes</value>
    [ResourceEntry("ThumbnailSizes", Description = "phrase: Thumbnail sizes", LastModified = "2013/07/01", Value = "Thumbnail sizes")]
    public string ThumbnailSizes => this[nameof (ThumbnailSizes)];

    /// <summary>word: px</summary>
    /// <value>px</value>
    [ResourceEntry("Pixels", Description = "word: px", LastModified = "2013/06/21", Value = "px")]
    public string Pixels => this[nameof (Pixels)];

    /// <summary>phrase: Resize image</summary>
    /// <value>Resize image</value>
    [ResourceEntry("ResizeImageCaption", Description = "phrase: Resize image", LastModified = "2013/06/24", Value = "Resize image")]
    public string ResizeImageCaption => this[nameof (ResizeImageCaption)];

    /// <summary>
    /// phrase: Resize images on upload to width no larger than...
    /// </summary>
    /// <value>Resize images on upload to width no larger than...</value>
    [ResourceEntry("ResizeImageOnUpload", Description = "phrase: Resize images on upload to width no larger than...", LastModified = "2020/03/05", Value = "Resize images on upload to width no larger than...")]
    public string ResizeImageOnUpload => this[nameof (ResizeImageOnUpload)];

    /// <summary>phrase: Do not resize</summary>
    /// <value>Do not resize</value>
    [ResourceEntry("DoNotResize", Description = "phrase: Do not resize", LastModified = "2020/03/05", Value = "Do not resize")]
    public string DoNotResize => this[nameof (DoNotResize)];

    /// <summary>phrase: {0} px width</summary>
    /// <value>{0} px width</value>
    [ResourceEntry("PXWidthPlaceholder", Description = "phrase: {0} px width", LastModified = "2020/03/05", Value = "{0} px width")]
    public string PXWidthPlaceholder => this[nameof (PXWidthPlaceholder)];

    /// <summary>word: Thumbnail</summary>
    /// <value>Thumbnail</value>
    [ResourceEntry("Thumbnail", Description = "word: Thumbnail", LastModified = "2013/07/01", Value = "Thumbnail")]
    public string Thumbnail => this[nameof (Thumbnail)];

    /// <summary>word: Size</summary>
    /// <value>Size</value>
    [ResourceEntry("SizeLabel", Description = "word: Size", LastModified = "2013/07/01", Value = "Size")]
    public string SizeLabel => this[nameof (SizeLabel)];

    /// <summary>Crop to {Width}x{Height}</summary>
    /// <value>Crop to {Width}x{Height}</value>
    [ResourceEntry("CropToAreaSizeFormat", Description = "Crop to {Width}x{Height}", LastModified = "2013/07/01", Value = "Crop to {Width}x{Height}")]
    public string CropToAreaSizeFormat => this[nameof (CropToAreaSizeFormat)];

    /// <summary>Fit to side {Size}x{Size}</summary>
    /// <value>Resize to {Size}</value>
    [ResourceEntry("ResizeWithFitToSideSizeFormat", Description = "Fit to side {Size}x{Size}", LastModified = "2013/07/01", Value = "Resize to {Size}")]
    public string ResizeWithFitToSideSizeFormat => this[nameof (ResizeWithFitToSideSizeFormat)];

    /// <summary>Fit to area {MaxWidth}x{MaxHeight}</summary>
    /// <value>Fit to area {MaxWidth}x{MaxHeight}</value>
    [ResourceEntry("ResizeWithFitToAreaSizeFormat", Description = "Fit to area {MaxWidth}x{MaxHeight}", LastModified = "2013/07/01", Value = "Fit to area {MaxWidth}x{MaxHeight}")]
    public string ResizeWithFitToAreaSizeFormat => this[nameof (ResizeWithFitToAreaSizeFormat)];

    /// <summary>
    /// phrase: Please note that this operation may take some time (up to few hours). Deleting of the thumbnails may affect the public pages which are using them.
    /// </summary>
    /// <value>Please note that this operation may take some time (up to few hours). Deleting of the thumbnails may affect the public pages which are using them.</value>
    [ResourceEntry("ThumbnailProfilesWarning", Description = "phrase: Please note that this operation may take some time (up to few hours). Deleting of the thumbnails may affect the public pages which are using them.", LastModified = "2013/07/01", Value = "Please note that this operation may take some time (up to few hours). Deleting of the thumbnails may affect the public pages which are using them.")]
    public string ThumbnailProfilesWarning => this[nameof (ThumbnailProfilesWarning)];

    /// <summary>
    /// Phrase: Please note that this operation may take some time (up to few hours). Regenerating of the thumbnails in the library will not affect the public pages which are using them, but during this process you will not be able to edit or delete these thumbnails.
    /// </summary>
    /// <value>Please note that this operation may take some time (up to few hours). Regenerating of the thumbnails in the library will not affect the public pages which are using them, but during this process you will not be able to edit or delete these thumbnails.</value>
    [ResourceEntry("ThumbnailRegenerationWarning", Description = "Phrase: Please note that this operation may take some time (up to few hours). Regenerating of the thumbnails in the library will not affect the public pages which are using them, but during this process you will not be able to edit or delete these thumbnails.", LastModified = "2013/07/01", Value = "Please note that this operation may take some time (up to few hours). Regenerating of the thumbnails in the library will not affect the public pages which are using them, but during this process you will not be able to edit or delete these thumbnails.")]
    public string ThumbnailRegenerationWarning => this[nameof (ThumbnailRegenerationWarning)];

    /// <summary>phrase: Yes, regenerate all thumbnails</summary>
    /// <value>Yes, regenerate all thumbnails</value>
    [ResourceEntry("RegenereateThumbnailsConfirm", Description = "phrase: Yes, regenerate all thumbnails", LastModified = "2013/07/01", Value = "Yes, regenerate all thumbnails")]
    public string RegenereateThumbnailsConfirm => this[nameof (RegenereateThumbnailsConfirm)];

    /// <summary>
    /// phrase: {0} is added and the existing thumbnails will be regenerated to reflect the cnanges.
    /// </summary>
    /// <value>{0} is added and the existing thumbnails will be regenerated to reflect the changes.</value>
    [ResourceEntry("SingleThumbnailAddedWarning", Description = "phrase: {0} is added and the existing thumbnails will be regenerated to reflect the cnanges.", LastModified = "2013/07/04", Value = "{0} is added and the existing thumbnails will be regenerated to reflect the changes.")]
    public string SingleThumbnailAddedWarning => this[nameof (SingleThumbnailAddedWarning)];

    /// <summary>
    /// phrase: {0} and {1} are added and the existing thumbnails will be regenerated to reflect the cnanges.
    /// </summary>
    /// <value>{0} and {1} are added and the existing thumbnails will be regenerated to reflect the changes.</value>
    [ResourceEntry("MultpleThumbnailsAddedWarning", Description = "phrase: {0} and {1} are added and the existing thumbnails will be regenerated to reflect the cnanges.", LastModified = "2013/07/04", Value = "{0} and {1} are added and the existing thumbnails will be regenerated to reflect the changes.")]
    public string MultpleThumbnailsAddedWarning => this[nameof (MultpleThumbnailsAddedWarning)];

    /// <summary>
    /// phrase: {0} is no longer selected and these thumbnails will be deleted from this library.
    /// </summary>
    /// <value>{0} is no longer selected and these thumbnails will be deleted from this library.</value>
    [ResourceEntry("SingleThumbnailDeletedWarning", Description = "phrase: {0} is no longer selected and these thumbnails will be deleted from this library.", LastModified = "2013/07/03", Value = "{0} is no longer selected and these thumbnails will be deleted from this library.")]
    public string SingleThumbnailDeletedWarning => this[nameof (SingleThumbnailDeletedWarning)];

    /// <summary>
    /// phrase: {0} and {1} are no longer selected and these thumbnails will be deleted from this library.
    /// </summary>
    /// <value>{0} and {1} are no longer selected and these thumbnails will be deleted from this library.</value>
    [ResourceEntry("MultipleThumbnailsDeletedWarning", Description = "phrase: {0} and {1} are no longer selected and these thumbnails will be deleted from this library.", LastModified = "2013/07/03", Value = "{0} and {1} are no longer selected and these thumbnails will be deleted from this library.")]
    public string MultipleThumbnailsDeletedWarning => this[nameof (MultipleThumbnailsDeletedWarning)];

    /// <summary>phrase: Yes, delete these thumbnails</summary>
    /// <value>Yes, delete these thumbnails</value>
    [ResourceEntry("YesDeleteThumbnails", Description = "phrase: Yes, delete these thumbnails", LastModified = "2013/07/03", Value = "Yes, delete these thumbnails")]
    public string YesDeleteThumbnails => this[nameof (YesDeleteThumbnails)];

    [ResourceEntry("DeleteAndRegenerate", Description = "phrase: Delete & Regenerate", LastModified = "2013/07/03", Value = "Delete & Regenerate")]
    public string DeleteAndRegenerate => this[nameof (DeleteAndRegenerate)];

    /// <summary>phrase: Yes, Regenerate</summary>
    /// <value>Yes, Regenerate</value>
    [ResourceEntry("YesRegenerate", Description = "phrase: Yes, Regenerate", LastModified = "2013/07/03", Value = "Yes, Regenerate")]
    public string YesRegenerate => this[nameof (YesRegenerate)];

    /// <summary>phrase: Edit an image thumbnail size ({0})</summary>
    /// <value>Edit an image thumbnail size ({0})</value>
    [ResourceEntry("EditImageThumbnailProfile", Description = "phrase: Edit an image thumbnail size ({0})", LastModified = "2013/07/04", Value = "Edit an image thumbnail size ({0})")]
    public string EditImageThumbnailProfile => this[nameof (EditImageThumbnailProfile)];

    /// <summary>phrase: Edit a video thumbnail size ({0})</summary>
    /// <value>Edit a video thumbnail size ({0})</value>
    [ResourceEntry("EditVideoThumbnailProfile", Description = "phrase: Edit a video thumbnail size ({0})", LastModified = "2013/07/04", Value = "Edit a video thumbnail size ({0})")]
    public string EditVideoThumbnailProfile => this[nameof (EditVideoThumbnailProfile)];

    /// <summary>
    /// phrase: You should define at least one image side (width or height)
    /// </summary>
    /// <value>You should define at least one image side (width or height)</value>
    [ResourceEntry("FitToAreaErrorMessage", Description = "phrase: You should define at least one image side (width or height)", LastModified = "2013/07/15", Value = "You should define at least one image side (width or height)")]
    public string FitToAreaErrorMessage => this[nameof (FitToAreaErrorMessage)];

    /// <summary>
    /// phrase: {0} must be a value between 1 and 9999 inclusive.
    /// </summary>
    /// <value>{0} must be a value between 1 and 9999 inclusive.</value>
    [ResourceEntry("MethodParameterErrorMessage", Description = "phrase: {0} must be a value between 1 and 9999 inclusive.", LastModified = "2013/07/11", Value = "{0} must be a value between 1 and 9999 inclusive.")]
    public string MethodParameterErrorMessage => this[nameof (MethodParameterErrorMessage)];

    /// <summary>word: Width</summary>
    /// <value>Width</value>
    [ResourceEntry("Width", Description = "word: Width", LastModified = "2013/07/11", Value = "Width")]
    public string Width => this[nameof (Width)];

    /// <summary>word: Height</summary>
    /// <value>Height</value>
    [ResourceEntry("Height", Description = "word: Height", LastModified = "2013/07/11", Value = "Height")]
    public string Height => this[nameof (Height)];

    /// <summary>
    /// phrase: Thumbnail regeneration is supported only for Images and Videos.
    /// </summary>
    /// <value>Thumbnail regeneration is supported only for Images and Videos.</value>
    [ResourceEntry("ThumbnailMediaContentSupport", Description = "phrase: Thumbnail regeneration is supported only for Images and Videos.", LastModified = "2013/07/15", Value = "Thumbnail regeneration is supported only for Images and Videos.")]
    public string ThumbnailMediaContentSupport => this[nameof (ThumbnailMediaContentSupport)];

    /// <summary>Phrase: Get the currently displayed frame</summary>
    /// <value>Get the currently displayed frame</value>
    [ResourceEntry("GetCurrentFrame", Description = "Phrase: Get the currently displayed frame", LastModified = "2014/03/10", Value = "Get the currently displayed frame")]
    public string GetCurrentFrame => this[nameof (GetCurrentFrame)];

    /// <summary>phrase: {0} is a reqiured field.</summary>
    /// <value>{0} is a required field.</value>
    [ResourceEntry("IsRequired", Description = "phrase: {0} is a reqiured field.", LastModified = "2013/05/29", Value = "{0} is a required field.")]
    public string IsRequired => this[nameof (IsRequired)];

    /// <summary>phrase: Thumbnail</summary>
    /// <value>Thumbnail</value>
    [ResourceEntry("ThumbnailProfileNameExample", Description = "phrase: Thumbnail", LastModified = "2013/05/29", Value = "<strong>Example:</strong> Thumbnail")]
    public string ThumbnailProfileNameExample => this[nameof (ThumbnailProfileNameExample)];

    /// <summary>
    /// The title of the ImageProcessor.Resize method with FitToSideArguments
    /// </summary>
    [ResourceEntry("ResizeWithFitToSideImageProcessorMethod", Description = "The title of the ImageProcessor.Resize method with FitToSideArguments", LastModified = "2013/07/05", Value = "Resize to side")]
    public string ResizeWithFitToSideImageProcessorMethod => this[nameof (ResizeWithFitToSideImageProcessorMethod)];

    /// <summary>
    /// The title of the ImageProcessor.Resize method with FitToAreaArguments
    /// </summary>
    [ResourceEntry("ResizeWithFitToAreaImageProcessorMethod", Description = "The title of the ImageProcessor.Resize method with FitToAreaArguments", LastModified = "2013/07/05", Value = "Resize to area")]
    public string ResizeWithFitToAreaImageProcessorMethod => this[nameof (ResizeWithFitToAreaImageProcessorMethod)];

    /// <summary>
    /// The title of the ImageProcessor.Resize method with FitToSideArguments
    /// </summary>
    /// <value>Thumbnail</value>
    [ResourceEntry("CropToAreaImageProcessorMethod", Description = "The title of the ImageProcessor.Crop method with FitToAreaArguments", LastModified = "2013/06/20", Value = "Crop to area")]
    public string CropToAreaImageProcessorMethod => this[nameof (CropToAreaImageProcessorMethod)];

    /// <summary>
    /// The title of the ScaleUp property of the ImageProcessor.Resize method
    /// </summary>
    /// <value>Thumbnail</value>
    [ResourceEntry("ScaleUp", Description = "The title of the ScaleUp property of the ImageProcessor.Resize method", LastModified = "2013/06/20", Value = "Resize smaller images to bigger dimensions")]
    public string ScaleUp => this[nameof (ScaleUp)];

    /// <summary>
    /// The title of the ResizeBiggerSide property of the ImageProcessor.Resize method with FitToSideArguments
    /// </summary>
    [ResourceEntry("ResizeBiggerSide", Description = "The title of the ResizeBiggerSide property of the ImageProcessor.Resize method with FitToSideArguments", LastModified = "2013/07/05", Value = "Resize bigger dimension to side")]
    public string ResizeBiggerSide => this[nameof (ResizeBiggerSide)];

    /// <summary>All sizes of {0}</summary>
    [ResourceEntry("AllSizesOf", Description = "All sizes of {0}", LastModified = "2013/06/24", Value = "All sizes of {0}")]
    public string AllSizesOf => this[nameof (AllSizesOf)];

    /// <summary>Link or embed</summary>
    [ResourceEntry("LinkOrEmbed", Description = "Link or embed", LastModified = "2013/06/24", Value = "Link or embed")]
    public string LinkOrEmbed => this[nameof (LinkOrEmbed)];

    /// <summary>Replace thumbnail</summary>
    [ResourceEntry("ReplaceThumbnail", Description = "Replace thumbnail", LastModified = "2013/06/24", Value = "Replace thumbnail")]
    public string ReplaceThumbnail => this[nameof (ReplaceThumbnail)];

    /// <summary>Regenerate thumbnail warning message.</summary>
    [ResourceEntry("ThmbnailUploadedWarning", Description = "Regenerate thumbnail warning message.", LastModified = "2013/06/24", Value = "This thumbnail was uploaded by user and not generated automatically.")]
    public string ThmbnailUploadedWarning => this[nameof (ThmbnailUploadedWarning)];

    /// <summary>Regenerate this thumbnail</summary>
    [ResourceEntry("RegenerateThumbnail", Description = "Regenerate this thumbnail", LastModified = "2013/06/24", Value = "Regenerate this thumbnail")]
    public string RegenerateThumbnail => this[nameof (RegenerateThumbnail)];

    /// <summary>
    /// phrase: To change thumbnail sizes go to Library &gt;Actions &gt;Thumbnails settings
    /// </summary>
    /// <value>To change thumbnail sizes go to Library &gt;Actions &gt;Thumbnails settings</value>
    [ResourceEntry("ChangeThumbnailSizesHint", Description = "phrase: To change thumbnail sizes go to Library >Actions >Thumbnails settings", LastModified = "2013/07/11", Value = "To change thumbnail sizes go to Library >Actions >Thumbnails settings")]
    public string ChangeThumbnailSizesHint => this[nameof (ChangeThumbnailSizesHint)];

    /// <summary>word: Preview</summary>
    /// <value>Preview</value>
    [ResourceEntry("Preview", Description = "word: Preview", LastModified = "2013/07/11", Value = "Preview")]
    public string Preview => this[nameof (Preview)];

    /// <summary>
    /// phrase: There is another task running for this library.
    /// </summary>
    /// <value>There is another task running for this library.</value>
    [ResourceEntry("AnotherTaskRunning", Description = "phrase: There is another task running for this library.", LastModified = "2013/07/15", Value = "There is another task running for this library.")]
    public string AnotherTaskRunning => this[nameof (AnotherTaskRunning)];

    /// <summary>
    /// phrase: You are about to move this library to library {LibTitle}.This may take a lot of time and all URLs will be changed.Are you sure you want to proceed?
    /// </summary>
    /// <value>You are about to move this library to library {LibTitle}.This may take a lot of time and all URLs will be changed.Are you sure you want to proceed?</value>
    [ResourceEntry("MoveLibraryWarning", Description = "phrase: <p><strong>You are about to move this library to library {0}.</strong><br/><br/>This may take a lot of time and all URLs will be changed.<br/><br/>Are you sure you want to proceed?</p>", LastModified = "2013/07/24", Value = "<p><strong>You are about to move this library to library <i>{0}</i>.</strong><br/><br/>This may take a lot of time and all URLs will be changed.<br/><br/>Are you sure you want to proceed?</p>")]
    public string MoveLibraryWarning => this[nameof (MoveLibraryWarning)];

    /// <summary>
    /// phrase: You are about to move this library on top level.This may take a lot of time and all URLs will be changed.Are you sure you want to proceed?
    /// </summary>
    /// <value>You are about to move this library on top level.This may take a lot of time and all URLs will be changed.Are you sure you want to proceed?</value>
    [ResourceEntry("MoveToRootLibraryWarning", Description = "phrase: <p><strong>You are about to move this library on top level.</strong><br/><br/>This may take a lot of time and all URLs will be changed.<br/><br/>Are you sure you want to proceed?</p>", LastModified = "2013/07/24", Value = "<p><strong>You are about to move this library on top level.</strong><br/><br/>This may take a lot of time and all URLs will be changed.<br/><br/>Are you sure you want to proceed?</p>")]
    public string MoveToRootLibraryWarning => this[nameof (MoveToRootLibraryWarning)];

    /// <summary>
    /// Label that indicates that a selected library will also include its child library items.
    /// </summary>
    [ResourceEntry("IncludesItemsFromChildLibraries", Description = "Label that indicates that a selected library will also include its child library items.", LastModified = "2013/11/27", Value = "(includes items from child libraries)")]
    public string IncludesItemsFromChildLibraries => this[nameof (IncludesItemsFromChildLibraries)];

    /// <summary>phrase: What is this?</summary>
    /// <value>What is this?</value>
    [ResourceEntry("WhatIsThis", Description = "phrase: What is this?", LastModified = "2014/01/17", Value = "What is this?")]
    public string WhatIsThis => this[nameof (WhatIsThis)];

    /// <summary>phrase: Invalid method parameters string.</summary>
    /// <value>Invalid method parameters string.</value>
    [ResourceEntry("InvalidMethodParametersString", Description = "phrase: Invalid method parameters string.", LastModified = "2014/01/28", Value = "Invalid method parameters string.")]
    public string InvalidMethodParametersString => this[nameof (InvalidMethodParametersString)];

    /// <summary>libraries</summary>
    /// <value>libraries</value>
    [ResourceEntry("LibrariesLowerCase", Description = "libraries", LastModified = "2014/03/24", Value = "libraries")]
    public string LibrariesLowerCase => this[nameof (LibrariesLowerCase)];

    /// <summary>Label: Amazon S3</summary>
    /// <value>Amazon S3</value>
    [ResourceEntry("AmazonS3", Description = "Label: Amazon S3", LastModified = "2015/10/05", Value = "Amazon S3")]
    public string AmazonS3 => this[nameof (AmazonS3)];

    /// <summary>Label: View original image</summary>
    /// <value>View original image</value>
    [ResourceEntry("ViewOriginalImage", Description = "Label: View original image", LastModified = "2015/11/09", Value = "View original image")]
    public string ViewOriginalImage => this[nameof (ViewOriginalImage)];

    /// <summary>Label: Crop/Resize/Rotate...</summary>
    /// <value>Crop/Resize/Rotate...</value>
    [ResourceEntry("CropResizeRotate", Description = "Label: Crop/Resize/Rotate...", LastModified = "2015/11/09", Value = "Crop/Resize/Rotate...")]
    public string CropResizeRotate => this[nameof (CropResizeRotate)];

    /// <summary>Label: Replace image</summary>
    /// <value>Replae image</value>
    [ResourceEntry("ReplaceImage", Description = "Label: Replace image", LastModified = "2015/11/09", Value = "Replace image")]
    public string ReplaceImage => this[nameof (ReplaceImage)];

    /// <summary>Label: Use another file for this translation</summary>
    /// <value>Use another file for this translation</value>
    [ResourceEntry("UseAnotherFile", Description = "Label: Use another file for this translation", LastModified = "2016/02/25", Value = "Use another file for this translation")]
    public string UseAnotherFile => this[nameof (UseAnotherFile)];

    /// <summary>Label: Replace the file</summary>
    /// <value>Replace the file</value>
    [ResourceEntry("ReplaceFile", Description = "Label: Replace the file", LastModified = "2015/11/13", Value = "Replace the file")]
    public string ReplaceFile => this[nameof (ReplaceFile)];

    /// <summary>
    /// Label: more translations are using this file and any changes will be applied to them too
    /// </summary>
    /// <value>more translations are using this file and any changes will be applied to them too</value>
    [ResourceEntry("MoreTranslations", Description = "Label: more translations are using this file and any changes will be applied to them too", LastModified = "2015/11/13", Value = "more translations are using this file and any changes will be applied to them too")]
    public string MoreTranslations => this[nameof (MoreTranslations)];

    /// <summary>Label: or upload another file for this translation</summary>
    /// <value>or upload another file for this translation</value>
    [ResourceEntry("UploadAnother", Description = "Label: or upload another file for this translation", LastModified = "2016/02/25", Value = "or upload another file for this translation")]
    public string UploadAnother => this[nameof (UploadAnother)];

    /// <summary>
    /// Label: Use file from already uploaded for other translations...
    /// </summary>
    /// <value>Use file from already uploaded for other translations...</value>
    [ResourceEntry("ChooseFromAlreadyUploaded", Description = "Label: Use file from already uploaded for other translations...", LastModified = "2016/02/25", Value = "Use file from already uploaded for other translations...")]
    public string ChooseFromAlreadyUploaded => this[nameof (ChooseFromAlreadyUploaded)];

    /// <summary>Label: Open the file</summary>
    /// <value>Open the file</value>
    [ResourceEntry("OpenTheFile", Description = "Label: Open the file", LastModified = "2016/02/5", Value = "Open the file")]
    public string OpenTheFile => this[nameof (OpenTheFile)];

    /// <summary>Label: Image size is scalable.</summary>
    [ResourceEntry("Scalable", Description = "Image size is scalable.", LastModified = "2017/02/15", Value = "Scalable")]
    public string Scalable => this[nameof (Scalable)];

    /// <summary>Label: Select a file.</summary>
    [ResourceEntry("SelectAFile", Description = "Select a file", LastModified = "2017/05/25", Value = "Select a file")]
    public string SelectAFile => this[nameof (SelectAFile)];

    /// <summary>word: extensions</summary>
    /// <value>extensions</value>
    [ResourceEntry("Extensions", Description = "word: Extensions", LastModified = "2018/04/12", Value = "Extensions")]
    public string Extensions => this[nameof (Extensions)];

    /// <summary>word: extension</summary>
    /// <value>extension</value>
    [ResourceEntry("Extension", Description = "word: Extension", LastModified = "2020/01/16", Value = "Extension")]
    public string Extension => this[nameof (Extension)];

    /// <summary>label: not set</summary>
    /// <value>not set</value>
    [ResourceEntry("NotSet", Description = "label: not set", LastModified = "2019/01/31", Value = "not set")]
    public string NotSet => this[nameof (NotSet)];

    /// <summary>Library with the same name already exists.</summary>
    /// <value>Library with the same name already exists.</value>
    [ResourceEntry("DuplicateNameErrorMessage", Description = "Library with the same name already exists.", LastModified = "2019/12/07", Value = "Library with the same name already exists.")]
    public string DuplicateNameErrorMessage => this[nameof (DuplicateNameErrorMessage)];

    /// <summary>Items displaying this image/video</summary>
    [ResourceEntry("ItemsDisplayingThisItem", Description = "Items displaying this image/video", LastModified = "2020/06/17", Value = "Items displaying this {0}")]
    public string ItemsDisplayingThisItem => this[nameof (ItemsDisplayingThisItem)];

    /// <summary>Pages displaying this image/video</summary>
    [ResourceEntry("PagesDisplayingThisItem", Description = "Pages displaying this image/video", LastModified = "2020/06/17", Value = "Pages displaying this {0}")]
    public string PagesDisplayingThisItem => this[nameof (PagesDisplayingThisItem)];
  }
}
