// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataProcessing;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.FileProcessors;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Exceptions;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Versioning;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Defines the basic functionality that should be implemented by libraries module data providers
  /// </summary>
  public abstract class LibrariesDataProvider : 
    ContentDataProviderBase,
    ILanguageDataProvider,
    ILibrariesDataProvider,
    IMediaContentFilePathResolver
  {
    private ProcessorFactory<LibrariesConfig, IFileProcessor> fileProcessorFactory;
    private const string folderPathUrlKey = "FolderPath";
    private const string notValidImageException = "The file received is not a valid image";
    internal const string UrlNameParam = "urlName";
    private string defaultUrlFormat = "/[FolderPath][UrlName]";
    private string configurationUrlFormat;
    private string urlName;
    private IDictionary<string, string> blobStorageProviderMappings;
    private static Type[] knownTypes;
    private string[] supportedPermissionSets = new string[9]
    {
      "Image",
      "Album",
      "ImagesSitemapGeneration",
      "Document",
      "DocumentLibrary",
      "DocumentsSitemapGeneration",
      "Video",
      "VideoLibrary",
      "VideosSitemapGeneration"
    };
    internal const string SystemThumbnailName = "0";
    internal const string DefaultAlbumUrlName = "default-album";
    internal const string DefaultDocumentLibraryUrlName = "default-document-library";
    internal const string DefaultVideoLibraryUrlName = "default-video-library";
    internal const string FileProcessingErrorMessage = "The file cannot be processed. It may have an invalid format or may be corrupt. Check the file for compatiblity or contact your administrator to review the error log.";

    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The config.</param>
    /// <param name="managerType">Type of the manager.</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      this.configurationUrlFormat = config["urlFormat"];
      this.urlName = config["urlName"];
      if (this.urlName == null)
        this.urlName = LibrariesManager.GenerateUrlName(this.Name);
      string str1 = config["blobStorageProviderMappings"];
      if (string.IsNullOrEmpty(str1))
        return;
      string[] strArray1 = str1.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray1.Length == 0)
        return;
      foreach (string str2 in strArray1)
      {
        string[] strArray2 = str2.Trim().Split(new char[1]
        {
          '='
        }, 2, StringSplitOptions.RemoveEmptyEntries);
        if (strArray2.Length == 2)
        {
          if (this.blobStorageProviderMappings == null)
            this.blobStorageProviderMappings = (IDictionary<string, string>) new Dictionary<string, string>();
          this.blobStorageProviderMappings.Add(strArray2[0], strArray2[1]);
        }
      }
    }

    /// <summary>
    /// Initializes the provider with default data. Returns true if there are any changes for committing
    /// </summary>
    protected override bool InitializeDefaultData()
    {
      bool flag1 = base.InitializeDefaultData();
      if (this.IsSystemProvider())
        return flag1;
      bool flag2 = "OpenAccessDataProvider".Equals(this.Name);
      if (!this.GetLibraries().Any<Library>())
      {
        Telerik.Sitefinity.Libraries.Model.Album lib1 = flag2 ? this.CreateAlbum(LibrariesModule.DefaultImagesLibraryId) : this.CreateAlbum((this.ApplicationName + "default-album").GenerateGuid());
        lib1.Title = (Lstring) "Default Library";
        lib1.UrlName = (Lstring) "default-album";
        this.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.Album>(lib1);
        this.SetPermissionActionForEveryoneOnSpecificLibrary((Library) lib1, "Image", "ManageImage");
        bool flag3 = true;
        Telerik.Sitefinity.Libraries.Model.DocumentLibrary lib2 = flag2 ? this.CreateDocumentLibrary(LibrariesModule.DefaultDocumentsLibraryId) : this.CreateDocumentLibrary((this.ApplicationName + "default-document-library").GenerateGuid());
        lib2.Title = (Lstring) "Default Library";
        lib2.UrlName = (Lstring) "default-document-library";
        this.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>(lib2);
        this.SetPermissionActionForEveryoneOnSpecificLibrary((Library) lib2, "Document", "ManageDocument");
        flag3 = true;
        Telerik.Sitefinity.Libraries.Model.VideoLibrary lib3 = flag2 ? this.CreateVideoLibrary(LibrariesModule.DefaultVideosLibraryId) : this.CreateVideoLibrary((this.ApplicationName + "default-video-library").GenerateGuid());
        lib3.Title = (Lstring) "Default Library";
        lib3.UrlName = (Lstring) "default-video-library";
        this.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.VideoLibrary>(lib3);
        this.SetPermissionActionForEveryoneOnSpecificLibrary((Library) lib3, "Video", "ManageVideo");
        flag1 = true;
      }
      return flag1;
    }

    /// <summary>
    /// Breaks permissions inheritance on a specific library (album, document library or video library) and makes sure a specific action is granted for everyone.
    /// (Used in the creation of the default libraries during startup).
    /// </summary>
    /// <param name="lib">The library to set permissions to.</param>
    /// <param name="permissionSetName">Name of the related permission set.</param>
    /// <param name="manageSecurityActionName">Name of the security action to grant.</param>
    private void SetPermissionActionForEveryoneOnSpecificLibrary(
      Library lib,
      string permissionSetName,
      string manageSecurityActionName)
    {
      this.BreakPermiossionsInheritance((ISecuredObject) lib);
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> permissions = lib.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId == lib.Id && p.SetName == permissionSetName));
      Telerik.Sitefinity.Security.Model.Permission permission1 = (Telerik.Sitefinity.Security.Model.Permission) null;
      foreach (Telerik.Sitefinity.Security.Model.Permission permission2 in permissions)
      {
        if (permission2.PrincipalId != SecurityManager.EveryoneRole.Id)
        {
          permission2.UngrantActions(manageSecurityActionName);
        }
        else
        {
          permission1 = permission2;
          permission1.GrantActions(true, manageSecurityActionName);
        }
      }
      if (permission1 != null)
        return;
      Telerik.Sitefinity.Security.Model.Permission permission3 = this.CreatePermission(permissionSetName, lib.Id, SecurityManager.EveryoneRole.Id);
      lib.Permissions.Add(permission3);
      permission3.GrantActions(true, manageSecurityActionName);
    }

    /// <summary>Gets the library.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    protected internal abstract Library GetLibrary(Guid id);

    /// <summary>Get a query for all media items</summary>
    /// <returns>Queryable object for all MediaItems</returns>
    protected internal abstract IQueryable<MediaContent> GetMediaItems();

    /// <summary>Ges the libraries.</summary>
    /// <returns></returns>
    protected internal abstract IQueryable<Library> GetLibraries();

    /// <summary>Gets a media item by its ID.</summary>
    /// <param name="id">The ID of the requested media item.</param>
    /// <returns>An instance of the media item.</returns>
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", new string[] {"ViewImage"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", new string[] {"ViewVideo"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", new string[] {"ViewDocument"})]
    public abstract MediaContent GetMediaItem(Guid id);

    /// <summary>Mark a library for removal</summary>
    /// <param name="LibraryToDelete">The Library to delete.</param>
    [TypedParameterPermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "libraryToDelete", "Album", new string[] {"DeleteAlbum"})]
    [TypedParameterPermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "libraryToDelete", "VideoLibrary", new string[] {"DeleteVideoLibrary"})]
    [TypedParameterPermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "libraryToDelete", "DocumentLibrary", new string[] {"DeleteDocumentLibrary"})]
    public abstract void Delete(Library libraryToDelete);

    /// <summary>
    /// This method is called when creating a library(album,video,document) to initialize common default properties for a media library
    /// When you override this method call the base implementation
    /// </summary>
    /// <param name="library">The library to initialize.</param>
    protected virtual void OnLibraryCreate(Library library)
    {
      library.BlobStorageProvider = Config.Get<LibrariesConfig>().BlobStorage.DefaultProvider;
      if (this.IsSystemProvider())
        return;
      switch (library)
      {
        case Telerik.Sitefinity.Libraries.Model.Album _:
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          ICollection<ThumbnailProfileConfigElement> values = Config.Get<LibrariesConfig>().GetThumbnailProfiles(library.GetType()).Values;
          List<string> stringList = new List<string>();
          foreach (ThumbnailProfileConfigElement profileConfigElement in (IEnumerable<ThumbnailProfileConfigElement>) values)
          {
            if (profileConfigElement.IsDefault)
              stringList.Add(profileConfigElement.Name);
          }
          using (List<string>.Enumerator enumerator = stringList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              library.ThumbnailProfiles.Add(current);
            }
            break;
          }
      }
    }

    /// <summary>
    /// Create a new <c>Album</c> and choose a random identity
    /// </summary>
    /// <returns>Created <c>Album</c> instance</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Album CreateAlbum();

    /// <summary>
    /// Create a new <c>Album</c> with a explicitly specified identity
    /// </summary>
    /// <param name="id"><c>Album</c> identity</param>
    /// <returns>Created <c>Album</c> instance</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Album CreateAlbum(Guid id);

    /// <summary>Get a queryable object for all libraries</summary>
    /// <returns>Queryable object for all librarys</returns>
    [EnumeratorPermission("Album", new string[] {"ViewAlbum"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.Album> GetAlbums();

    /// <summary>Search for an library by its identity</summary>
    /// <param name="id">Searched library identity</param>
    /// <returns>Found Album, or <c>null</c> if not found.</returns>
    [ValuePermission("Album", new string[] {"ViewAlbum"})]
    public abstract Telerik.Sitefinity.Libraries.Model.Album GetAlbum(Guid id);

    /// <summary>Creates a new video library.</summary>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary();

    /// <summary>
    /// Creates a new video library with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the video library to create.</param>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary(
      Guid id);

    /// <summary>Gets a queryable object for all video libraries.</summary>
    /// <returns>Queryable object for all video libraries.</returns>
    [EnumeratorPermission("VideoLibrary", new string[] {"ViewVideoLibrary"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.VideoLibrary> GetVideoLibraries();

    /// <summary>Gets a video library by its identity.</summary>
    /// <param name="id">The identity of the video library to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.</returns>
    [ValuePermission("VideoLibrary", new string[] {"ViewVideoLibrary"})]
    public abstract Telerik.Sitefinity.Libraries.Model.VideoLibrary GetVideoLibrary(
      Guid id);

    /// <summary>Creates a new document library.</summary>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary();

    /// <summary>
    /// Creates a new document library with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the document library to create.</param>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary(
      Guid id);

    /// <summary>Gets a queryable object for all document libraries.</summary>
    /// <returns>Queryable object for all document libraries.</returns>
    [EnumeratorPermission("DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.DocumentLibrary> GetDocumentLibraries();

    /// <summary>Gets a document library by its identity.</summary>
    /// <param name="id">The identity of the document library to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.</returns>
    [ValuePermission("DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    public abstract Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetDocumentLibrary(
      Guid id);

    /// <summary>Create a new image and choose a random identity</summary>
    /// <returns>Created Image instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Image CreateImage();

    /// <summary>Create an image by explicitly set its identity</summary>
    /// <param name="id">Identity of the Image to create</param>
    /// <returns>Created Image instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Image CreateImage(Guid id);

    /// <summary>Get a query for all images</summary>
    /// <returns>Queryable object for all images</returns>
    [EnumeratorPermission("Image", new string[] {"ViewImage"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.Image> GetImages();

    /// <summary>Search for an image post by its identity</summary>
    /// <param name="id">Identity of the searched image</param>
    /// <returns>Found image.</returns>
    [ValuePermission("Image", new string[] {"ViewImage"})]
    public abstract Telerik.Sitefinity.Libraries.Model.Image GetImage(Guid id);

    /// <summary>Mark an image for removal</summary>
    /// <param name="imageToDelete">The image to delete.</param>
    [ParameterPermission("imageToDelete", "Image", new string[] {"ManageImage"})]
    public abstract void Delete(Telerik.Sitefinity.Libraries.Model.Image imageToDelete);

    /// <summary>Creates a thumbnail for the specified Image and size</summary>
    /// <returns>Created Image instance.</returns>
    [MethodPermission("General", new string[] {"Create"})]
    [Obsolete("Use CreateThumbnail(string profileName)")]
    public abstract Thumbnail CreateThumbnail(int size);

    /// <summary>Creates the thumbnail.</summary>
    /// <param name="profileName">Name of the profile.</param>
    /// <returns></returns>
    [MethodPermission("General", new string[] {"Create"})]
    public abstract Thumbnail CreateThumbnail(string profileName);

    /// <summary>Returns all thumbnails</summary>
    /// <returns>Query object for all thumbnails</returns>
    [EnumeratorPermission("General", new string[] {"View"})]
    public abstract IQueryable<Thumbnail> GetThumbnails();

    /// <summary>Deletes the media thumbnails.</summary>
    /// <param name="content">The content.</param>
    public abstract void DeleteMediaThumbnails(MediaContent content);

    /// <summary>Creates a new video.</summary>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Video CreateVideo();

    /// <summary>
    /// Creates a new video with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the video to create.</param>
    /// <returns>The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.</returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Video CreateVideo(Guid id);

    /// <summary>Gets a query for all videos.</summary>
    /// <returns>Queryable object for all videos.</returns>
    [EnumeratorPermission("Video", new string[] {"ViewVideo"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.Video> GetVideos();

    /// <summary>Gets a video by its identity.</summary>
    /// <param name="id">The identity of the video to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.</returns>
    [ValuePermission("Video", new string[] {"ViewVideo"})]
    public abstract Telerik.Sitefinity.Libraries.Model.Video GetVideo(Guid id);

    /// <summary>Marks a video for removal.</summary>
    /// <param name="videoToDelete">The video to delete.</param>
    [ParameterPermission("videoToDelete", "Video", new string[] {"ManageVideo"})]
    public abstract void Delete(Telerik.Sitefinity.Libraries.Model.Video videoToDelete);

    /// <summary>Creates a new document.</summary>
    /// <returns>The newly created <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /></returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Document CreateDocument();

    /// <summary>
    /// Creates a new document with the explicitly specified ID.
    /// </summary>
    /// <param name="id">The ID.</param>
    /// <returns>The newly created <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /></returns>
    public abstract Telerik.Sitefinity.Libraries.Model.Document CreateDocument(Guid id);

    /// <summary>Gets a query for all documents.</summary>
    /// <returns>A queryable object for all documents</returns>
    [EnumeratorPermission("Document", new string[] {"ViewDocument"})]
    public abstract IQueryable<Telerik.Sitefinity.Libraries.Model.Document> GetDocuments();

    /// <summary>Gets a document by ID.</summary>
    /// <param name="id">The ID.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /> instance</returns>
    [ValuePermission("Document", new string[] {"ViewDocument"})]
    public abstract Telerik.Sitefinity.Libraries.Model.Document GetDocument(Guid id);

    /// <summary>Marks a document for removal.</summary>
    /// <param name="documentToDelete">The document to delete.</param>
    [ParameterPermission("documentToDelete", "Document", new string[] {"ManageDocument"})]
    public abstract void Delete(Telerik.Sitefinity.Libraries.Model.Document documentToDelete);

    /// <summary>
    /// Adds the media file URL for the given media content item.
    /// </summary>
    /// <param name="mediaContentItem">The media content item.</param>
    /// <param name="fileUrl">The file URL.</param>
    /// <param name="isDefault">The is default.</param>
    /// <param name="redirectToDefault">The redirect to default.</param>
    /// <param name="cultureId">The culture id. If it is not specified it will be added for the current culture.</param>
    public void AddMediaFileUrl(
      MediaContent mediaContentItem,
      string fileUrl,
      bool isDefault = false,
      bool redirectToDefault = false,
      int? cultureId = null)
    {
      MediaFileLink fileLink = mediaContentItem.GetFileLink(true, cultureId);
      LibrariesDataProvider.ValidateMediaContentUrl(fileUrl, mediaContentItem.GetType());
      this.AddMediaFileUrl(fileLink, fileUrl, isDefault, redirectToDefault);
    }

    /// <summary>
    /// Reads the additional info and save the information for media file urls
    /// </summary>
    /// <param name="mediaContentItem">The media content item</param>
    /// <param name="additionalInfo">The additional info</param>
    internal void RecompileMediaFileUrls(
      MediaContent mediaContentItem,
      Dictionary<string, object> additionalInfo)
    {
      if (mediaContentItem == null)
        return;
      MediaFileLink fileLink = mediaContentItem.GetFileLink(fallBack: false);
      if (fileLink == null)
        return;
      if (mediaContentItem.MediaFileUrlName.Value.IsNullOrWhitespace())
        mediaContentItem.MediaFileUrlName = mediaContentItem.UrlName;
      List<string> urlsToRemove = new List<string>();
      if (additionalInfo != null)
      {
        if (additionalInfo.ContainsKey("FileAdditionalUrlsKey"))
          urlsToRemove = fileLink.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (url => !url.IsDefault)).Select<MediaFileUrl, string>((Func<MediaFileUrl, string>) (url => url.Url)).ToList<string>();
        IEnumerable<string> fileAdditionalUrls = this.GetMediaFileAdditionalUrls(additionalInfo, fileLink);
        if (fileAdditionalUrls.Any<string>())
        {
          bool redirectToDefault = this.GetValue<bool>(additionalInfo, "RedirectToDefault");
          foreach (string url in fileAdditionalUrls)
          {
            LibrariesDataProvider.ValidateMediaContentUrl(url, mediaContentItem.GetType());
            this.AddMediaFileUrl(fileLink, url, redirectToDefault: redirectToDefault);
            urlsToRemove.Remove(url);
          }
        }
      }
      string fileUrl = this.CompileMediaFileUrl(mediaContentItem);
      MediaFileUrl previousDefault = fileLink.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => u.IsDefault)).FirstOrDefault<MediaFileUrl>();
      if (previousDefault == null || fileUrl != previousDefault.Url)
      {
        this.AddMediaFileUrl(mediaContentItem, fileUrl, true);
        urlsToRemove.Remove(fileUrl);
      }
      this.UpdateAdditionalUrls(fileLink, urlsToRemove, additionalInfo, previousDefault);
      this.ValidateFileUrlConstraints(mediaContentItem);
    }

    internal string CompileMediaFileUrl(MediaContent item)
    {
      StringBuilder contentProviderUrl = this.GetMediaContentProviderUrl(item);
      contentProviderUrl.Append("/[Parent.UrlName]");
      contentProviderUrl.Append("/[FolderPath]");
      string str = this.CompileItemUrl<MediaContent>(item, SystemManager.CurrentContext.Culture, contentProviderUrl.ToString());
      if (item.MediaFileUrlName.Value.IndexOf(str) == 0)
        item.MediaFileUrlName.Value = item.MediaFileUrlName.Value.Substring(str.Length);
      if (!str.EndsWith("/"))
        str += "/";
      return str + (string) item.MediaFileUrlName;
    }

    /// <summary>
    /// Removes redundant urls. If the additional urls are disabled - all additional urls are removed
    /// </summary>
    /// <param name="link">The link with the urls</param>
    /// <param name="urlsToRemove">Urls to remove collection</param>
    /// <param name="additionalInfo">The additional info, containing the information about additional urls</param>
    private void UpdateAdditionalUrls(
      MediaFileLink link,
      List<string> urlsToRemove,
      Dictionary<string, object> additionalInfo,
      MediaFileUrl previousDefault)
    {
      if (additionalInfo == null)
        return;
      string key = "AllowMultipleFileUrls";
      List<MediaFileUrl> mediaFileUrlList = !additionalInfo.ContainsKey(key) || this.GetValue<bool>(additionalInfo, key) ? link.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => urlsToRemove.Contains(u.Url))).ToList<MediaFileUrl>() : link.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => !u.IsDefault)).ToList<MediaFileUrl>();
      if (previousDefault != null)
        mediaFileUrlList.Remove(previousDefault);
      foreach (MediaFileUrl mediaFileUrlToDelete in mediaFileUrlList)
      {
        link.Urls.Remove(mediaFileUrlToDelete);
        this.Delete(mediaFileUrlToDelete);
      }
    }

    private IEnumerable<string> GetMediaFileAdditionalUrls(
      Dictionary<string, object> additionalInfo,
      MediaFileLink link)
    {
      List<string> fileAdditionalUrls = new List<string>();
      if (additionalInfo.ContainsKey("FileAdditionalUrlsKey"))
        fileAdditionalUrls.AddRange((IEnumerable<string>) this.GetValue<object[]>(additionalInfo, "FileAdditionalUrlsKey").Cast<string>().Select<string, string>((Func<string, string>) (x => !x.StartsWith("/") ? "/" + x : x)).ToList<string>());
      return (IEnumerable<string>) fileAdditionalUrls;
    }

    private T GetValue<T>(Dictionary<string, object> dict, string key)
    {
      T obj1 = default (T);
      object obj2;
      if (dict != null && dict.TryGetValue(key, out obj2))
        obj1 = (T) Convert.ChangeType(obj2, typeof (T));
      return obj1;
    }

    private static void ValidateMediaContentUrl(string url, Type mediaContentType)
    {
      if (url.IsNullOrWhitespace())
        throw new ArgumentException("Empty Urls are not allowed");
      string viewControlSection = LibrariesDataProvider.GetContentViewControlSection(mediaContentType);
      string backendViewName = LibrariesDataProvider.GetBackendViewName(mediaContentType);
      Regex regex = new Regex(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator, RegexOptions.Compiled);
      if (!viewControlSection.IsNullOrEmpty() && !backendViewName.IsNullOrEmpty() && (Config.Get<LibrariesConfig>().ContentViewControls[viewControlSection].Views[backendViewName] as IContentViewDetailDefinition).Sections.SingleOrDefault<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (s => s.Name == "AdvancedSection")).Fields.SingleOrDefault<IFieldDefinition>((Func<IFieldDefinition, bool>) (f => f.FieldName == "multipleFileUrlsExpandableField")) is IExpandableFieldDefinition expandableFieldDefinition && expandableFieldDefinition.ExpandableFields.SingleOrDefault<IFieldDefinition>((Func<IFieldDefinition, bool>) (f => f.FieldName == "multipleFileUrlsField")) is ITextFieldDefinition textFieldDefinition)
        regex = new Regex(textFieldDefinition.Validation.RegularExpression, RegexOptions.Compiled);
      string input = Regex.Replace(url, "/+", "/");
      if (!regex.IsMatch(input))
        throw new ArgumentException(string.Format("The URL: '{0}' is not valid.", (object) input));
    }

    private static string GetContentViewControlSection(Type mediaContentType)
    {
      string fullName = mediaContentType.FullName;
      if (fullName != null)
      {
        string str = fullName;
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName)
          return DocumentsDefinitions.BackendDefinitionName;
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
          return "ImagesBackend";
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName)
          return VideosDefinitions.BackendVideosDefinitionName;
      }
      return string.Empty;
    }

    private static string GetBackendViewName(Type mediaContentType)
    {
      string fullName = mediaContentType.FullName;
      if (fullName != null)
      {
        string str = fullName;
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName)
          return "DocumentsBackendEdit";
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
          return "ImagesBackendEdit";
        if (str == typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName)
          return "VideosBackendEdit";
      }
      return string.Empty;
    }

    /// <summary>
    /// Аdds media file url. To validate the url prior that  use the <see cref="M:Telerik.Sitefinity.Modules.Libraries.LibrariesDataProvider.ValidateMediaContentUrl(System.String,System.Type)" /> method
    /// </summary>
    /// <param name="mfLink">The mf link.</param>
    /// <param name="url">The URL.</param>
    /// <param name="isDefault">If true this is the default url of the item.</param>
    private void AddMediaFileUrl(
      MediaFileLink mfLink,
      string url,
      bool isDefault = false,
      bool redirectToDefault = false)
    {
      MediaFileUrl mediaFileUrl1 = mfLink.Urls.FirstOrDefault<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => string.Compare(url, u.Url, StringComparison.OrdinalIgnoreCase) == 0));
      if (mediaFileUrl1 == null)
      {
        mediaFileUrl1 = this.CreateMediaFileUrl();
        mediaFileUrl1.MediaFileLink = mfLink;
        string str = url.StartsWith("/") ? url : "/" + url;
        mediaFileUrl1.Url = str.ToLowerInvariant();
      }
      if (isDefault && !string.IsNullOrWhiteSpace(url))
        mfLink.DefaultUrl = url.ToLowerInvariant();
      mediaFileUrl1.IsDefault = isDefault;
      mediaFileUrl1.RedirectToDefault = redirectToDefault;
      if (!isDefault)
        return;
      foreach (MediaFileUrl mediaFileUrl2 in mfLink.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => string.Compare(url, u.Url, StringComparison.OrdinalIgnoreCase) != 0 && u.IsDefault)))
      {
        mediaFileUrl2.IsDefault = false;
        mediaFileUrl2.RedirectToDefault = true;
      }
    }

    /// <summary>
    /// Validates the URL constraints.
    /// Throw exception if the item has a conflicting URL with an existing item.
    /// </summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="item">The item which URL should be validated.</param>
    /// <param name="revertChangesOnError">Specifies whether to revert changes before throwing the URL duplication exception.</param>
    /// <param name="recompileAutoGenerateUrlOnConflict">Specifies whether to throw exception for URL conflicts of auto generate URL items.</param>
    /// <exception cref="!:ThrowDuplicateUrlException">Throw exception if the item has a conflicting URL with an existing item.</exception>
    private void ValidateFileUrlConstraints(
      MediaContent item,
      bool revertChangesOnError = true,
      bool recompileAutoGenerateUrlOnConflict = true)
    {
      IList<MediaFileUrl> mediaFileUrls = item.MediaFileUrls;
      if (mediaFileUrls == null || !mediaFileUrls.Any<MediaFileUrl>())
        return;
      Guid currentFileId = item.GetFileLink().FileId;
      foreach (MediaFileUrl mediaFileUrl1 in (IEnumerable<MediaFileUrl>) mediaFileUrls)
      {
        MediaFileUrl mediaFileUrl = mediaFileUrl1;
        string url = mediaFileUrl.Url;
        IQueryable<MediaFileUrl> source = this.GetMediaFileUrls().Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => mfu.Url == url && mfu.MediaFileLink != default (object) && (int) mfu.MediaFileLink.MediaContent.Status == 0));
        if (item.Status != ContentLifecycleStatus.Master)
          source = source.Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => mfu.MediaFileLink.MediaContent.Id != item.OriginalContentId || mfu.MediaFileLink.Culture != mediaFileUrl.MediaFileLink.Culture));
        if (source.AsEnumerable<MediaFileUrl>().FirstOrDefault<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => u.MediaFileLink != null && u.MediaFileLink.FileId != currentFileId && u.MediaFileLink.FileId != Guid.Empty && !this.IsSameContent(mediaFileUrl.MediaFileLink, u.MediaFileLink))) != null)
        {
          if (recompileAutoGenerateUrlOnConflict && item.AutoGenerateUniqueUrl)
          {
            string str1 = item.Id.ToString("N");
            string str2 = url + str1;
            MediaContent mediaContent = mediaFileUrl.MediaFileLink.MediaContent;
            mediaContent.MediaFileUrlName = (Lstring) ((string) mediaContent.MediaFileUrlName + str1);
            mediaFileUrl.Url = str2;
            if (mediaFileUrl.IsDefault)
            {
              MediaFileLink fileLink = item.GetFileLink();
              if (fileLink != null)
                fileLink.DefaultUrl = str2;
            }
          }
          else
            CommonMethods.ThrowDuplicateUrlException(url);
        }
      }
    }

    private bool IsSameContent(MediaFileLink currentFileLink, MediaFileLink duplicateFileLink)
    {
      int culture1 = currentFileLink.Culture;
      MediaContent mediaContent1 = currentFileLink.MediaContent;
      if (mediaContent1.Status != ContentLifecycleStatus.Temp)
        return false;
      int culture2 = duplicateFileLink.Culture;
      MediaContent mediaContent2 = duplicateFileLink.MediaContent;
      return !(mediaContent1.OriginalContentId != mediaContent2.Id) && mediaContent1.GetFileLink(cultureId: new int?(culture1)).FileId == mediaContent1.GetFileLink(cultureId: new int?(culture2)).FileId & mediaContent2.GetFileLink(cultureId: new int?(culture1)).FileId == mediaContent2.GetFileLink(cultureId: new int?(culture2)).FileId;
    }

    internal void DeleteBlob(
      IBlobContentLocation blobContentLocation,
      BlobStorageManager blobStorageManager,
      bool isInRecycleBin = false)
    {
      Dependency dependency = this.GetRelatedManager<VersionManager>((string) null).Provider.GetDependency(blobContentLocation.FileId.ToString(), typeof (BlobContentCleanerTask));
      if (dependency == null || BlobContentCleanerTask.GetData(dependency.Data)["blobStorageProvider"].ToString() != blobStorageManager.Provider.Name)
      {
        blobStorageManager.Delete(blobContentLocation);
      }
      else
      {
        if (!(blobStorageManager.Provider is IExternalBlobStorageProvider provider) || !(blobContentLocation.FileId.ToString() != blobContentLocation.FilePath) || isInRecycleBin)
          return;
        this.ArchiveBlob(blobContentLocation, provider);
      }
    }

    /// <summary>
    /// Checks if file have to be archived.
    /// Do not archive files that do not have dependencies created for them - e.g. files uploaded to external provider in versions older then 12.0
    /// Do not archive files that are moved from one storage provider to external storage provider because the created dependency already keeps the file
    /// in the previous storage and if we archive the file nobody will then take care to delete it.
    /// </summary>
    /// <param name="blob">The blob that is sent to be archived.</param>
    /// <param name="blobStorageProviderName">The name of the blob storage provider which knows where the file is stored when dependency is created.</param>
    /// <returns>Wheather to skip archiving the blob.</returns>
    internal bool SkipArchive(IBlobContentLocation blob, string blobStorageProviderName)
    {
      Dependency dependency = VersionManager.GetManager().Provider.GetDependency(blob.FileId.ToString(), typeof (BlobContentCleanerTask));
      return dependency == null || BlobContentCleanerTask.GetData(dependency.Data)["blobStorageProvider"].ToString() != blobStorageProviderName;
    }

    internal void ArchiveBlob(
      IBlobContentLocation blob,
      IExternalBlobStorageProvider provider,
      bool preserveOriginalBlob = false)
    {
      try
      {
        if (provider is BlobStorageProvider blobStorageProvider && this.SkipArchive(blob, blobStorageProvider.Name))
        {
          if (preserveOriginalBlob)
            return;
          blobStorageProvider.Delete(blob);
        }
        else
        {
          IBlobContentLocation destination = (IBlobContentLocation) new BlobContentProxy()
          {
            FileId = blob.FileId,
            FilePath = blob.FileId.ToString(),
            MimeType = blob.MimeType,
            Extension = blob.Extension
          };
          if (preserveOriginalBlob)
            provider.Copy(blob, destination);
          else
            provider.Move(blob, destination);
        }
      }
      catch
      {
      }
    }

    internal void MoveBlobToRecycleBin(MediaFileLink mediaFileLink, BlobStorageProvider provider)
    {
      try
      {
        IBlobContentLocation blobContentLocation1 = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = mediaFileLink.FileId,
          FilePath = mediaFileLink.FilePath,
          MimeType = mediaFileLink.MimeType,
          Extension = mediaFileLink.Extension
        };
        IBlobContentLocation blobContentLocation2 = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = mediaFileLink.FileId,
          FilePath = ("RecycleBin_" + mediaFileLink.FileId.ToString()),
          MimeType = mediaFileLink.MimeType,
          Extension = mediaFileLink.Extension
        };
        if (!(provider is IExternalBlobStorageProvider blobStorageProvider) || !provider.BlobExists(blobContentLocation1) || provider.BlobExists(blobContentLocation2))
          return;
        blobStorageProvider.Move(blobContentLocation1, blobContentLocation2);
      }
      catch
      {
      }
    }

    internal void MoveBlobToRecycleBin(IBlobContentLocation blob, BlobStorageProvider provider)
    {
      try
      {
        IBlobContentLocation blobContentLocation = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = blob.FileId,
          FilePath = ("RecycleBin_" + blob.FileId.ToString()),
          MimeType = blob.MimeType,
          Extension = blob.Extension
        };
        if (!(provider is IExternalBlobStorageProvider blobStorageProvider) || !provider.BlobExists(blob) || provider.BlobExists(blobContentLocation))
          return;
        blobStorageProvider.Move(blob, blobContentLocation);
      }
      catch
      {
      }
    }

    internal void RestoreBlobFromRecycleBin(
      MediaFileLink mediaFileLink,
      BlobStorageProvider provider)
    {
      try
      {
        IBlobContentLocation blobContentLocation1 = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = mediaFileLink.FileId,
          FilePath = mediaFileLink.FilePath,
          MimeType = mediaFileLink.MimeType,
          Extension = mediaFileLink.Extension
        };
        IBlobContentLocation blobContentLocation2 = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = mediaFileLink.FileId,
          FilePath = ("RecycleBin_" + mediaFileLink.FileId.ToString()),
          MimeType = mediaFileLink.MimeType,
          Extension = mediaFileLink.Extension
        };
        if (!(provider is IExternalBlobStorageProvider blobStorageProvider) || provider.BlobExists(blobContentLocation1) || !provider.BlobExists(blobContentLocation2))
          return;
        blobStorageProvider.Move(blobContentLocation2, blobContentLocation1);
      }
      catch
      {
      }
    }

    internal void RestoreBlobFromRecycleBin(IBlobContentLocation blob, BlobStorageProvider provider)
    {
      try
      {
        IBlobContentLocation blobContentLocation = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = blob.FileId,
          FilePath = ("RecycleBin_" + blob.FileId.ToString()),
          MimeType = blob.MimeType,
          Extension = blob.Extension
        };
        if (!(provider is IExternalBlobStorageProvider blobStorageProvider) || provider.BlobExists(blob) || !provider.BlobExists(blobContentLocation))
          return;
        blobStorageProvider.Move(blobContentLocation, blob);
      }
      catch
      {
      }
    }

    internal void DeleteBlobFromRecycleBin(IBlobContentLocation blob, BlobStorageManager manager)
    {
      try
      {
        IBlobContentLocation location = (IBlobContentLocation) new BlobContentProxy()
        {
          FileId = blob.FileId,
          FilePath = ("RecycleBin_" + blob.FileId.ToString()),
          MimeType = blob.MimeType,
          Extension = blob.Extension
        };
        if (!(manager.Provider is IExternalBlobStorageProvider) || !manager.BlobExists(location))
          return;
        manager.Delete(location);
      }
      catch
      {
      }
    }

    /// <summary>Create a new media file link</summary>
    /// <returns>Created media file link instance.</returns>
    public abstract MediaFileLink CreateMediaFileLink();

    /// <summary>Get a query for all media file links</summary>
    /// <returns>Queryable object for all media file links</returns>
    public abstract IQueryable<MediaFileLink> GetMediaFileLinks();

    /// <summary>Mark an media file link for removal</summary>
    /// <param name="mediaFileLinkToDelete">The media file link to delete.</param>
    public abstract void Delete(MediaFileLink mediaFileLinkToDelete);

    /// <summary>
    /// Deletes the media file links for the given content item.
    /// </summary>
    /// <param name="content">The content.</param>
    public abstract void DeleteMediaFileLinks(MediaContent content);

    /// <summary>
    /// Deletes the media file urls for the given media file link.
    /// </summary>
    /// <param name="mediaFileLink">The media file link.</param>
    public abstract void DeleteMediaFileUrls(MediaFileLink mediaFileLink);

    /// <summary>Create a new media file url</summary>
    /// <returns>Created media file url instance.</returns>
    public abstract MediaFileUrl CreateMediaFileUrl();

    /// <summary>Get a query for all media file urls</summary>
    /// <returns>Queryable object for all media file urls</returns>
    public abstract IQueryable<MediaFileUrl> GetMediaFileUrls();

    /// <summary>Mark an media file url for removal</summary>
    /// <param name="mediaFileUrlToDelete">The media file url to delete.</param>
    public abstract void Delete(MediaFileUrl mediaFileUrlToDelete);

    public virtual void TransferItemStorage(MediaContent item, string storageProvider)
    {
      if (storageProvider == null)
        storageProvider = ManagerBase<BlobStorageProvider>.GetDefaultProviderName();
      if (storageProvider.Equals(item.BlobStorageProvider))
        return;
      BlobStorageManager blobStorageManager = this.GetBlobStorageManager(item);
      BlobStorageManager manager = BlobStorageManager.GetManager(this.GetMappedBlobStorageProviderName(storageProvider));
      List<IBlobContentLocation> blobContentLocationList = new List<IBlobContentLocation>();
      BlobContentProxy[] array = item.Thumbnails.Select<Thumbnail, BlobContentProxy>((Func<Thumbnail, BlobContentProxy>) (t => BlobContentProxy.CreateFrom((IBlobContent) t))).ToArray<BlobContentProxy>();
      IDictionary<int, Guid> liveFileIds = this.GetLiveFileIds(item);
      List<MediaFileLink> source1 = new List<MediaFileLink>();
      foreach (MediaFileLink mediaFileLink1 in (IEnumerable<MediaFileLink>) item.MediaFileLinks)
      {
        MediaFileLink mediaFileLink = mediaFileLink1;
        using (new CultureRegion(mediaFileLink.Culture))
        {
          BlobContentProxy from = BlobContentProxy.CreateFrom((IBlobContent) item);
          item.BlobStorageProvider = storageProvider;
          if (!liveFileIds.Any<KeyValuePair<int, Guid>>())
            liveFileIds = this.GetLiveFileIds(item);
          if (!liveFileIds.Any<KeyValuePair<int, Guid>>() || liveFileIds.ContainsKey(mediaFileLink.Culture) && item.FileId == liveFileIds[mediaFileLink.Culture])
          {
            item.FilePath = this.GenerateLivePath(item);
          }
          else
          {
            string str = mediaFileLink.DefaultUrl != null ? mediaFileLink.DefaultUrl : item.ItemDefaultUrl.ToLowerInvariant().ToString();
            item.FilePath = str.TrimStart('/') + "_" + item.FileId.ToString() + item.Extension;
          }
          if (!blobStorageManager.Provider.HasSameLocation(manager.Provider))
          {
            blobContentLocationList.Add((IBlobContentLocation) from);
            if (manager.BlobExists((IBlobContentLocation) item))
            {
              MediaFileLink mediaFileLink2 = source1.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.FileId == mediaFileLink.FileId));
              if (mediaFileLink2 == null)
              {
                MediaContent mediaContentForFile = this.FindAnotherMediaContentForFile(item.FileId, storageProvider, item.Id);
                if (mediaContentForFile != null)
                  mediaFileLink2 = mediaContentForFile.GetFileLink();
              }
              if (mediaFileLink2 != null)
              {
                item.NumberOfChunks = mediaFileLink2.NumberOfChunks;
                item.ChunkSize = mediaFileLink2.ChunkSize;
                item.TotalSize = mediaFileLink2.TotalSize;
              }
            }
            else
            {
              using (Stream downloadStream = blobStorageManager.GetDownloadStream((IBlobContent) from))
                this.UploadToStorage(new BlobItemWrapper((IBlobContent) item), downloadStream, manager.Provider);
              source1.Add(mediaFileLink);
            }
          }
        }
      }
      foreach (Thumbnail thumbnail in (IEnumerable<Thumbnail>) item.Thumbnails)
      {
        Thumbnail thmbItem = thumbnail;
        Stream source2 = (Stream) null;
        if (thmbItem.FileId != Guid.Empty)
        {
          BlobContentProxy content = ((IEnumerable<BlobContentProxy>) array).Single<BlobContentProxy>((Func<BlobContentProxy, bool>) (t => t.Id == thmbItem.Id));
          blobContentLocationList.Add((IBlobContentLocation) content);
          if (manager.BlobExists((IBlobContentLocation) thmbItem))
          {
            Thumbnail thumbnailForFile = this.FindAnotherThumbnailForFile(thmbItem.FileId, storageProvider, thmbItem.Id);
            if (thumbnailForFile != null)
            {
              ((IChunksBlobContent) thmbItem).NumberOfChunks = ((IChunksBlobContent) thumbnailForFile).NumberOfChunks;
              ((IChunksBlobContent) thmbItem).ChunkSize = ((IChunksBlobContent) thumbnailForFile).ChunkSize;
              ((IChunksBlobContent) thmbItem).Uploaded = ((IChunksBlobContent) thumbnailForFile).Uploaded;
              thmbItem.TotalSize = thumbnailForFile.TotalSize;
            }
            else
              continue;
          }
          else
            source2 = blobStorageManager.GetDownloadStream((IBlobContent) content);
        }
        else
        {
          source2 = (Stream) new MemoryStream(thmbItem.Data);
          thmbItem.FileId = Guid.NewGuid();
          thmbItem.Data = (byte[]) null;
        }
        if (source2 != null)
        {
          using (source2)
            this.UploadToStorage(new BlobItemWrapper((IBlobContent) thmbItem), source2, manager.Provider);
        }
      }
      List<IBlobContentLocation> blobsToDelete = new List<IBlobContentLocation>();
      LibraryTasksUtilities.TransferRevisionBlobs(item, this, manager.Provider, blobStorageManager, blobsToDelete, source1.Select<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (x => x.FileId)));
      foreach (IBlobContentLocation blobContentLocation in blobsToDelete)
        this.DeleteBlob(blobContentLocation, blobStorageManager);
      if (this.IsFileUsedByOtherMediaContent(item.FileId, blobStorageManager.Provider.Name, item.Id))
        return;
      foreach (IBlobContentLocation blobContentLocation in blobContentLocationList)
        this.DeleteBlob(blobContentLocation, blobStorageManager);
    }

    /// <summary>Downloads the specified content from the database.</summary>
    /// <param name="content">The content.</param>
    /// <returns>The content data as a stream</returns>
    public virtual Stream Download(MediaContent content) => this.Download((IBlobContent) content);

    public Stream Download(Thumbnail thumbnail) => this.Download((IBlobContent) thumbnail);

    public virtual Stream Download(IBlobContent item) => item is Thumbnail && ((Thumbnail) item).FileId == Guid.Empty ? (Stream) new MemoryStream(((Thumbnail) item).Data) : this.GetBlobStorageManager(item).GetDownloadStream(item);

    /// <summary>Uploads the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    public virtual void Upload(MediaContent content, Stream source, string extension) => this.Upload(content, source, extension, false);

    /// <summary>Uploads the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    /// <param name="uploadAndReplace">Whether to replace the existing file with the uploaded one.</param>
    public virtual void Upload(
      MediaContent content,
      Stream source,
      string extension,
      bool uploadAndReplace)
    {
      extension = extension.ToLower();
      string mimeMapping = MimeMapping.GetMimeMapping(extension);
      if (this.GetFileProcessorFactory().GetProcessors().Count<IFileProcessor>() > 0)
      {
        FileProcessorInput fileInput = new FileProcessorInput();
        fileInput.FileExtension = extension;
        fileInput.MimeType = mimeMapping;
        fileInput.FileStream = source;
        foreach (IFileProcessor processor in this.GetFileProcessorFactory().GetProcessors())
        {
          if (processor.CanProcessFile(fileInput))
          {
            try
            {
              source = processor.Process(fileInput);
            }
            catch (Exception ex)
            {
              Log.Error(string.Format("File processor '{0}' was unable to successfully process the file '{1}' and thrown the following error: {2}", (object) processor.Name, (object) content.FilePath, (object) ex.Message));
              this.ThrowDatabaseLibraryException("The file cannot be processed. It may have an invalid format or may be corrupt. Check the file for compatiblity or contact your administrator to review the error log.", content);
            }
            if (source == null || !source.CanRead)
            {
              Log.Error(string.Format("File processor '{0}' was unable to process the file '{1}'. Returned stream was empty or unreadable.", (object) processor.Name, (object) content.FilePath));
              this.ThrowDatabaseLibraryException("The file cannot be processed. It may have an invalid format or may be corrupt. Check the file for compatiblity or contact your administrator to review the error log.", content);
            }
          }
        }
      }
      if (!this.ValidateUpload(content, source, extension))
        return;
      content.ChunkSize = this.ChunkSize;
      content.MimeType = mimeMapping;
      content.Extension = extension;
      bool flag = true;
      Guid fileId = content.FileId;
      Telerik.Sitefinity.Libraries.Model.Image image = content as Telerik.Sitefinity.Libraries.Model.Image;
      try
      {
        if (image != null)
          this.UploadImage(image, source, mimeMapping, uploadAndReplace);
        else
          this.UploadDataOnly(content, source, uploadAndReplace);
      }
      catch (OpenAccessException ex)
      {
        string str = "{0} upload failed.";
        if (ex.Message.Contains("The timeout period elapsed prior to completion of the operation or the server is not responding."))
          this.ThrowDatabaseLibraryException(str + " Database connection timed out.", content);
        this.ThrowDatabaseLibraryException(str + " Database error.", content);
      }
      catch (Exception ex)
      {
        throw;
      }
      if (uploadAndReplace)
        this.ReplaceRelatedTranslations(content, fileId);
      if (!flag)
        return;
      this.RecompileMediaFileUrls(content, (Dictionary<string, object>) null);
    }

    private LibraryItemUploadException ThrowDatabaseLibraryException(
      string message,
      MediaContent content)
    {
      throw new LibraryItemUploadException(message)
      {
        LibraryErrorType = LibraryErrorType.DatabaseError,
        ContentType = content.GetType()
      };
    }

    private void ReplaceRelatedTranslations(MediaContent content, Guid fileId)
    {
      foreach (MediaFileLink mediaFileLink in content.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileId)))
      {
        MediaFileLink fileLink = content.GetFileLink();
        fileLink.CopyTo(mediaFileLink);
        this.CopyMediaFileUrls(fileLink, mediaFileLink);
        string str = content.MediaFileUrlName.Value;
        using (new CultureRegion(Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture)))
        {
          content.MediaFileUrlName = (Lstring) str;
          this.RegenerateThumbnails(content);
        }
      }
    }

    internal void CopyMediaFileUrls(MediaFileLink source, MediaFileLink destination)
    {
      List<MediaFileUrl> source1 = new List<MediaFileUrl>((IEnumerable<MediaFileUrl>) destination.Urls);
      foreach (MediaFileUrl url in (IEnumerable<MediaFileUrl>) source.Urls)
      {
        MediaFileUrl fileUrl = url;
        MediaFileUrl dest = source1.FirstOrDefault<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => u.Url.Equals(fileUrl.Url, StringComparison.OrdinalIgnoreCase)));
        if (dest == null)
        {
          dest = this.CreateMediaFileUrl();
          dest.MediaFileLink = destination;
        }
        else
          source1.Remove(dest);
        fileUrl.CopyTo(dest);
      }
      foreach (MediaFileUrl mediaFileUrlToDelete in source1)
      {
        destination.Urls.Remove(mediaFileUrlToDelete);
        this.Delete(mediaFileUrlToDelete);
      }
    }

    /// <summary>Generates the thumbnail of the media content item.</summary>
    /// <param name="mediaContent">The media content item.</param>
    public virtual void RegenerateThumbnails(
      MediaContent mediaContent,
      params string[] profilesFilter)
    {
      if (!(mediaContent is Telerik.Sitefinity.Libraries.Model.Image) || mediaContent.IsVectorGraphics())
        return;
      using (Stream stream = this.Download(mediaContent))
      {
        using (MemoryStream destination = new MemoryStream())
        {
          stream.CopyTo((Stream) destination);
          using (System.Drawing.Image img = System.Drawing.Image.FromStream((Stream) destination))
            this.RegenerateThumbnails(mediaContent, img, profilesFilter);
        }
      }
    }

    /// <summary>Generates the video thumbnails.</summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    public virtual void GenerateVideoThumbnails(Telerik.Sitefinity.Libraries.Model.Video video, FileInfo videoFile)
    {
      bool flag = false;
      if (!videoFile.Exists)
      {
        using (FileStream fileStream = videoFile.Create())
        {
          using (Stream stream = this.Download((MediaContent) video))
          {
            byte[] buffer = new byte[video.ChunkSize];
            int count = buffer.Length;
            if (count > 0)
            {
              while (count == buffer.Length)
              {
                count = stream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                  fileStream.Write(buffer, 0, count);
              }
            }
          }
        }
        flag = true;
      }
      string str = videoFile.FullName + ".jpg";
      try
      {
        using (System.Drawing.Image thumbnail = this.VideoThumbnailGenerator.CreateThumbnail(video, videoFile, str))
          this.RegenerateThumbnails((MediaContent) video, thumbnail);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex, "Video");
      }
      finally
      {
        if (flag)
          this.DeleteFile(videoFile.FullName);
        this.DeleteFile(str);
      }
    }

    private void DeleteFile(string path)
    {
      FileInfo fileInfo = new FileInfo(path);
      if (!fileInfo.Exists)
        return;
      for (int index = 0; index < 3; ++index)
      {
        try
        {
          fileInfo.Delete();
          break;
        }
        catch (Exception ex)
        {
          if (index == 2)
          {
            if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              break;
            throw;
          }
        }
      }
    }

    /// <inheritdoc />
    public virtual void OnLibraryUpdating(Library library)
    {
      if (library.PrevClientCacheProfile == null || !(library.ClientCacheProfile != library.PrevClientCacheProfile) || !(BlobStorageManager.GetManager(this.GetMappedBlobStorageProviderName(library.BlobStorageProvider)).Provider is IExternalBlobStorageProvider provider))
        return;
      foreach (MediaContent mediaContent in (IEnumerable<MediaContent>) library.Items())
      {
        if (mediaContent.Uploaded)
          provider.SetProperties((IBlobContentLocation) mediaContent, mediaContent.GetBlobProperties());
      }
    }

    internal virtual ProcessorFactory<LibrariesConfig, IFileProcessor> GetFileProcessorFactory() => ProcessorFactory<LibrariesConfig, IFileProcessor>.Instance;

    /// <summary>Delete a thumbnail</summary>
    /// <param name="thumbnail">Thumbnail to delete</param>
    public abstract void Delete(Thumbnail thumbnail);

    /// <summary>Copies one media content item over another</summary>
    /// <param name="source">Source media content</param>
    /// <param name="destination">Destination media content</param>
    [Obsolete("Use the same method in LibrariesManager instead")]
    public void CopyMediaContent(MediaContent source, MediaContent destination)
    {
      this.CopyContent((Content) source, (Content) destination);
      destination.Urls.ClearDestinationUrls<MediaUrlData>(source.Urls, new System.Action<MediaUrlData>(((UrlDataProviderBase) this).Delete));
      source.Urls.CopyTo<MediaUrlData>(destination.Urls, (IDataItem) destination);
      destination.Author.CopyFrom(source.Author);
      destination.Parent = source.Parent;
      destination.NumberOfChunks = source.NumberOfChunks;
      destination.Uploaded = source.Uploaded;
      destination.TotalSize = source.TotalSize;
      destination.ChunkSize = source.ChunkSize;
      destination.MimeType = source.MimeType;
      destination.Extension = source.Extension;
      destination.Ordinal = source.Ordinal;
      this.ClearNotReferencedMediaContentChunks(source, destination);
      destination.FileId = source.FileId;
      destination.FilePath = source.FilePath;
      destination.BlobStorageProvider = source.BlobStorageProvider;
      foreach (Thumbnail thumbnail in new List<Thumbnail>((IEnumerable<Thumbnail>) destination.Thumbnails))
        this.Delete(thumbnail);
      destination.Thumbnails.Clear();
      foreach (Thumbnail thumbnail1 in (IEnumerable<Thumbnail>) source.Thumbnails)
      {
        Thumbnail thumbnail2 = new Thumbnail();
        byte[] destinationArray = new byte[thumbnail1.Data.Length];
        Array.Copy((Array) thumbnail1.Data, (Array) destinationArray, thumbnail1.Data.Length);
        thumbnail2.Data = destinationArray;
        thumbnail2.MimeType = thumbnail1.MimeType;
        thumbnail2.Parent = destination;
        thumbnail2.Size = thumbnail1.Size;
        thumbnail2.Culture = thumbnail1.Culture;
        destination.Thumbnails.Add(thumbnail2);
      }
    }

    /// <summary>
    /// Clears the media content items chunks for specified file id.
    /// </summary>
    /// <param name="fileId">The file id.</param>
    /// <param name="currentContentId">The current content id.</param>
    [Obsolete("Use the same method in LibrariesManager instead")]
    public virtual void ClearNotReferencedMediaContentChunks(
      MediaContent source,
      MediaContent destination)
    {
      Guid fileId = destination.FileId;
      BlobStorageManager blobStorageManager = this.GetBlobStorageManager(destination);
      if ((!(source.FileId != destination.FileId) ? 0 : (destination.FileId != Guid.Empty ? 1 : 0)) == 0)
        return;
      int num = destination.Status != ContentLifecycleStatus.Master ? 0 : (source.Status == ContentLifecycleStatus.Temp ? 1 : 0);
      bool flag1 = destination.Status == ContentLifecycleStatus.Live && source.Status == ContentLifecycleStatus.Master;
      bool flag2 = source.Status == ContentLifecycleStatus.Live && destination.Status == ContentLifecycleStatus.Master;
      bool flag3 = false;
      if (num != 0)
        flag3 = SitefinityQuery.Get<MediaFileLink>((DataProviderBase) this).Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (ml => ml.MediaContent != default (object) && ml.FileId == fileId && (int) ml.MediaContent.Status == 2)).Any<MediaFileLink>();
      else if (flag1 | flag2)
        flag3 = true;
      if (!flag3)
        return;
      IBlobContentLocation location = blobStorageManager.ResolveBlobContentLocation((IBlobContent) destination);
      blobStorageManager.Delete(location);
    }

    /// <summary>Updates thumbnails data for a video.</summary>
    /// <param name="video">The video.</param>
    /// <param name="image">The image.</param>
    /// <param name="mimeType">MIME Type.</param>
    public void UpdateThumbnail(Telerik.Sitefinity.Libraries.Model.Video video, System.Drawing.Image image, string mimeType) => this.GenerateThumbnails((MediaContent) video, image, mimeType);

    internal virtual void RefreshItem(MediaContent item) => throw new NotImplementedException();

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (object) this.CreateImage(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (object) this.CreateDocument(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        return (object) this.CreateVideo(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        return (object) this.CreateAlbum(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        return (object) this.CreateVideoLibrary(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        return (object) this.CreateDocumentLibrary(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (object) this.GetImage(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (object) this.GetDocument(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        return (object) this.GetVideo(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        return (object) this.GetAlbum(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        return (object) this.GetVideoLibrary(id);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        return (object) this.GetDocumentLibrary(id);
      return itemType == typeof (Comment) ? (object) this.GetComment(id) : base.GetItem(itemType, id);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (object) this.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (object) this.GetDocuments().Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Document>();
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        return (object) this.GetVideos().Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Video>();
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        return (object) this.GetAlbums().Where<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>();
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        return (object) this.GetVideoLibraries().Where<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        return (object) this.GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (i => i.Id == id)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
      if (!(itemType == typeof (Comment)))
        return base.GetItemOrDefault(itemType, id);
      if (!this.EnableCommentsBackwardCompatibility)
        return (object) null;
      try
      {
        return (object) this.GetComment(id);
      }
      catch (Exception ex)
      {
        return (object) null;
      }
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Comment))
        return (IEnumerable) DataProviderBase.SetExpressions<Comment>(this.GetComments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.Image>(this.GetImages(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.Document>(this.GetDocuments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.Video>(this.GetVideos(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.Album>(this.GetAlbums(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.VideoLibrary>(this.GetVideoLibraries(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>(this.GetDocumentLibraries(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (Library).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<Library>(this.GetLibraries(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      Type type = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      int num = type == typeof (Comment) || type == typeof (Telerik.Sitefinity.Libraries.Model.Image) || type == typeof (Telerik.Sitefinity.Libraries.Model.Document) || type == typeof (Telerik.Sitefinity.Libraries.Model.Video) || type == typeof (Telerik.Sitefinity.Libraries.Model.Album) || type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary) || type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary) || type == typeof (MediaFileLink) ? 1 : (type == typeof (MediaFileUrl) ? 1 : 0);
      if (type == typeof (Comment))
        this.Delete((Comment) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        this.Delete((Telerik.Sitefinity.Libraries.Model.Image) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        this.Delete((Telerik.Sitefinity.Libraries.Model.Document) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        this.Delete((Telerik.Sitefinity.Libraries.Model.Video) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        this.Delete((Library) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        this.Delete((Library) item);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        this.Delete((Library) item);
      else if (type == typeof (MediaFileLink))
        this.Delete((MediaFileLink) item);
      else if (type == typeof (MediaFileUrl))
        this.Delete((MediaFileUrl) item);
      if (num == 0)
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      this.providerDecorator.DeletePermissions(item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Collection of known types for this manager</returns>
    public override Type[] GetKnownTypes()
    {
      if (LibrariesDataProvider.knownTypes == null)
        LibrariesDataProvider.knownTypes = new Type[7]
        {
          typeof (Telerik.Sitefinity.Libraries.Model.Image),
          typeof (Telerik.Sitefinity.Libraries.Model.Document),
          typeof (Telerik.Sitefinity.Libraries.Model.Video),
          typeof (Library),
          typeof (Telerik.Sitefinity.Libraries.Model.Album),
          typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary),
          typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary)
        };
      return LibrariesDataProvider.knownTypes;
    }

    internal StringBuilder GetMediaContentRootUrlUrl(MediaContent item)
    {
      Type type = item.GetType();
      StringBuilder contentRootUrlUrl = new StringBuilder("/");
      if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        contentRootUrlUrl.Append(Config.Get<LibrariesConfig>().Images.UrlRoot);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        contentRootUrlUrl.Append(Config.Get<LibrariesConfig>().Videos.UrlRoot);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        contentRootUrlUrl.Append(Config.Get<LibrariesConfig>().Documents.UrlRoot);
      else
        contentRootUrlUrl.Append("libraries");
      return contentRootUrlUrl;
    }

    internal StringBuilder GetMediaContentProviderUrl(MediaContent item)
    {
      StringBuilder contentRootUrlUrl = this.GetMediaContentRootUrlUrl(item);
      if (!string.IsNullOrEmpty(this.UrlName))
      {
        contentRootUrlUrl.Append("/");
        contentRootUrlUrl.Append(this.UrlName);
      }
      return contentRootUrlUrl;
    }

    internal bool IsStandardMediaContentUrl(string url, MediaContent item)
    {
      if (url.IsNullOrEmpty() || url.Contains<char>('.'))
        return false;
      url = url.ToLower();
      if (url.StartsWith("~"))
        url = url.Substring(1);
      string lower = (!(this.Name == ManagerBase<LibrariesDataProvider>.GetDefaultProviderName()) ? (object) this.GetMediaContentProviderUrl(item) : (object) this.GetMediaContentRootUrlUrl(item)).ToString().ToLower();
      return url.StartsWith(lower);
    }

    /// <summary>
    /// Gets the url format for the specified data item that implements <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> interface.
    /// </summary>
    /// <param name="item">The locatable item for which the url format should be returned.</param>
    /// <returns>Regular expression used to format the url.</returns>
    public override string GetUrlFormat(ILocatable item)
    {
      Type type = item.GetType();
      if (typeof (Library).IsAssignableFrom(type))
        return "/[UrlName]";
      if (!typeof (MediaContent).IsAssignableFrom(type))
        return base.GetUrlFormat(item);
      StringBuilder contentProviderUrl = this.GetMediaContentProviderUrl(item as MediaContent);
      contentProviderUrl.Append("/[Parent.UrlName]");
      string str = this.configurationUrlFormat;
      if (this.configurationUrlFormat.IsNullOrEmpty())
        str = this.defaultUrlFormat;
      contentProviderUrl.Append(str);
      return contentProviderUrl.ToString();
    }

    /// <inheritdoc />
    protected internal override string GetUrlPart<T>(string key, string format, T item)
    {
      if (!(key == "FolderPath"))
        return base.GetUrlPart<T>(key, format, item);
      return item is IFolderItem folderItem && folderItem.FolderId.HasValue ? this.GetFolderPath(folderItem) : "";
    }

    private string GetFolderPath(IFolderItem folderItem)
    {
      if (!(this is IFolderOAProvider provider) || !folderItem.FolderId.HasValue)
        return "";
      Guid id = folderItem.FolderId.Value;
      Folder folder = provider.GetFolder(id);
      StringBuilder stringBuilder = new StringBuilder();
      List<Folder> folderList = new List<Folder>();
      for (; folder != null; folder = folder.Parent)
        folderList.Add(folder);
      for (int index = folderList.Count - 1; index >= 0; --index)
      {
        if (!string.IsNullOrEmpty((string) folderList[index].UrlName))
          stringBuilder.Append((string) folderList[index].UrlName + "/");
      }
      return stringBuilder.ToString();
    }

    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon">Determines whether the examined taxon is a single taxon or a collection of taxons.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public override IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (IEnumerable) this.GetItemsByTaxon<Telerik.Sitefinity.Libraries.Model.Image>(taxonId, isSingleTaxon, propertyName, itemType, filterExpression, orderExpression, skip, take, ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        return (IEnumerable) this.GetItemsByTaxon<Telerik.Sitefinity.Libraries.Model.Video>(taxonId, isSingleTaxon, propertyName, itemType, filterExpression, orderExpression, skip, take, ref totalCount);
      if (itemType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (IEnumerable) this.GetItemsByTaxon<Telerik.Sitefinity.Libraries.Model.Document>(taxonId, isSingleTaxon, propertyName, itemType, filterExpression, orderExpression, skip, take, ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (Telerik.Sitefinity.Libraries.Model.Image), typeof (Telerik.Sitefinity.Libraries.Model.Video), typeof (Telerik.Sitefinity.Libraries.Model.Document));
    }

    private IQueryable<T> GetItemsByTaxon<T>(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
      where T : MediaContent
    {
      this.CurrentTaxonomyProperty = propertyName;
      int? totalCount1 = new int?();
      IQueryable<T> items = (IQueryable<T>) this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref totalCount1);
      IQueryable<T> source1;
      if (isSingleTaxon)
      {
        source1 = items.Where<T>((Expression<Func<T, bool>>) (i => i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId));
      }
      else
      {
        IQueryable<T> source2 = items.Where<T>((Expression<Func<T, bool>>) (i => i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty).Any<Guid>((Func<Guid, bool>) (t => t == taxonId))));
        totalCount = new int?(Queryable.Count<T>(source2));
        source1 = source2;
      }
      if (totalCount.HasValue)
        totalCount = new int?(Queryable.Count<T>(source1));
      if (skip > 0)
        source1 = Queryable.Skip<T>(source1, skip);
      if (take > 0)
        source1 = Queryable.Take<T>(source1, take);
      return source1;
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (LibrariesDataProvider);

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="contentType">The content type.</param>
    /// <returns></returns>
    public override Type GetUrlTypeFor(Type contentType)
    {
      if (typeof (Library).IsAssignableFrom(contentType))
        return typeof (LibraryUrlData);
      if (typeof (MediaContent).IsAssignableFrom(contentType))
        return typeof (MediaUrlData);
      throw new ArgumentException("Unknown type specified.");
    }

    /// <summary>
    /// Override this method in order to return the type of the Parent object of the specified content type.
    /// If the type has no parent type, return null.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    [Obsolete("Use GetParentType method instead.")]
    public override Type GetParentTypeFor(Type contentType) => typeof (MediaContent).IsAssignableFrom(contentType) ? typeof (Library) : (Type) null;

    /// <inheritdoc />
    public override Type GetParentType(Type childType) => typeof (MediaContent).IsAssignableFrom(childType) || typeof (Folder).IsAssignableFrom(childType) ? typeof (Library) : base.GetParentType(childType);

    /// <inheritdoc />
    public new virtual IDataItem GetParent(IDataItem child) => child.GetType() == typeof (MediaContent) ? (IDataItem) ((MediaContent) child).Parent : base.GetParent(child);

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.CreatePermission("Image", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(true, "ViewImage");
      root.Permissions.Add(permission1);
      Telerik.Sitefinity.Security.Model.Permission permission2 = this.CreatePermission("Image", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(true, "ManageImage");
      root.Permissions.Add(permission2);
      Telerik.Sitefinity.Security.Model.Permission permission3 = this.CreatePermission("Image", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "ManageImage", "ChangeImageOwner");
      root.Permissions.Add(permission3);
      Telerik.Sitefinity.Security.Model.Permission permission4 = this.CreatePermission("Image", root.Id, SecurityManager.DesignersRole.Id);
      permission4.GrantActions(false, "ManageImage");
      root.Permissions.Add(permission4);
      Telerik.Sitefinity.Security.Model.Permission permission5 = this.CreatePermission("Album", root.Id, SecurityManager.EveryoneRole.Id);
      permission5.GrantActions(true, "ViewAlbum");
      root.Permissions.Add(permission5);
      Telerik.Sitefinity.Security.Model.Permission permission6 = this.CreatePermission("Album", root.Id, SecurityManager.EditorsRole.Id);
      permission6.GrantActions(true, "ChangeAlbumOwner", "CreateAlbum", "DeleteAlbum");
      root.Permissions.Add(permission6);
      Telerik.Sitefinity.Security.Model.Permission permission7 = this.CreatePermission("Album", root.Id, SecurityManager.AuthorsRole.Id);
      permission7.GrantActions(true, "CreateAlbum");
      root.Permissions.Add(permission7);
      Telerik.Sitefinity.Security.Model.Permission permission8 = this.CreatePermission("Album", root.Id, SecurityManager.DesignersRole.Id);
      permission8.GrantActions(true, "CreateAlbum");
      root.Permissions.Add(permission8);
      Telerik.Sitefinity.Security.Model.Permission permission9 = this.CreatePermission("Album", root.Id, SecurityManager.OwnerRole.Id);
      permission9.GrantActions(true, "DeleteAlbum");
      root.Permissions.Add(permission9);
      Telerik.Sitefinity.Security.Model.Permission permission10 = this.CreatePermission("Video", root.Id, SecurityManager.EveryoneRole.Id);
      permission10.GrantActions(true, "ViewVideo");
      root.Permissions.Add(permission10);
      Telerik.Sitefinity.Security.Model.Permission permission11 = this.CreatePermission("Video", root.Id, SecurityManager.OwnerRole.Id);
      permission11.GrantActions(true, "ManageVideo");
      root.Permissions.Add(permission11);
      Telerik.Sitefinity.Security.Model.Permission permission12 = this.CreatePermission("Video", root.Id, SecurityManager.EditorsRole.Id);
      permission12.GrantActions(false, "ManageVideo", "ChangeVideoOwner");
      root.Permissions.Add(permission12);
      Telerik.Sitefinity.Security.Model.Permission permission13 = this.CreatePermission("Video", root.Id, SecurityManager.DesignersRole.Id);
      permission13.GrantActions(false, "ManageVideo");
      root.Permissions.Add(permission13);
      Telerik.Sitefinity.Security.Model.Permission permission14 = this.CreatePermission("VideoLibrary", root.Id, SecurityManager.EveryoneRole.Id);
      permission14.GrantActions(true, "ViewVideoLibrary");
      root.Permissions.Add(permission14);
      Telerik.Sitefinity.Security.Model.Permission permission15 = this.CreatePermission("VideoLibrary", root.Id, SecurityManager.EditorsRole.Id);
      permission15.GrantActions(true, "ChangeVideoLibraryOwner", "CreateVideoLibrary", "DeleteVideoLibrary");
      root.Permissions.Add(permission15);
      Telerik.Sitefinity.Security.Model.Permission permission16 = this.CreatePermission("VideoLibrary", root.Id, SecurityManager.AuthorsRole.Id);
      permission16.GrantActions(false, "CreateVideoLibrary");
      root.Permissions.Add(permission16);
      Telerik.Sitefinity.Security.Model.Permission permission17 = this.CreatePermission("VideoLibrary", root.Id, SecurityManager.DesignersRole.Id);
      permission17.GrantActions(false, "CreateVideoLibrary");
      root.Permissions.Add(permission17);
      Telerik.Sitefinity.Security.Model.Permission permission18 = this.CreatePermission("VideoLibrary", root.Id, SecurityManager.OwnerRole.Id);
      permission18.GrantActions(false, "CreateVideoLibrary");
      root.Permissions.Add(permission18);
      Telerik.Sitefinity.Security.Model.Permission permission19 = this.CreatePermission("Document", root.Id, SecurityManager.EveryoneRole.Id);
      permission19.GrantActions(true, "ViewDocument");
      root.Permissions.Add(permission19);
      Telerik.Sitefinity.Security.Model.Permission permission20 = this.CreatePermission("Document", root.Id, SecurityManager.OwnerRole.Id);
      permission20.GrantActions(true, "ManageDocument");
      root.Permissions.Add(permission20);
      Telerik.Sitefinity.Security.Model.Permission permission21 = this.CreatePermission("Document", root.Id, SecurityManager.EditorsRole.Id);
      permission21.GrantActions(false, "ManageDocument", "ChangeDocumentOwner");
      root.Permissions.Add(permission21);
      Telerik.Sitefinity.Security.Model.Permission permission22 = this.CreatePermission("DocumentLibrary", root.Id, SecurityManager.EveryoneRole.Id);
      permission22.GrantActions(true, "ViewDocumentLibrary");
      root.Permissions.Add(permission22);
      Telerik.Sitefinity.Security.Model.Permission permission23 = this.CreatePermission("DocumentLibrary", root.Id, SecurityManager.EditorsRole.Id);
      permission23.GrantActions(true, "ChangeDocumentLibraryOwner", "CreateDocumentLibrary", "DeleteDocumentLibrary");
      root.Permissions.Add(permission23);
      Telerik.Sitefinity.Security.Model.Permission permission24 = this.CreatePermission("Document", root.Id, SecurityManager.AuthorsRole.Id);
      permission24.GrantActions(false, "ManageDocument");
      root.Permissions.Add(permission24);
    }

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot(bool create)
    {
      if (this.providerDecorator != null)
        return (ISecuredObject) this.providerDecorator.GetSecurityRoot(create, (IDictionary<string, string>) new Dictionary<string, string>(), this.SupportedPermissionSets);
      throw new MissingDecoratorException((DataProviderBase) this, "GetSecurityRoot(bool create)");
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets => this.supportedPermissionSets;

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageImage"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageVideo"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageDocument"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Image", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageImage"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "Video", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageVideo"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "Document", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageDocument"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", SecurityConstants.TransactionActionType.New, new string[] {"CreateAlbum"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", SecurityConstants.TransactionActionType.New, new string[] {"CreateDocumentLibrary"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", SecurityConstants.TransactionActionType.New, new string[] {"CreateVideoLibrary"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageImage"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageVideo"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageDocument"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Image", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageImage"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "Video", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageVideo"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "Document", SecurityConstants.TransactionActionType.Updated, new string[] {"ManageDocument"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", SecurityConstants.TransactionActionType.New, new string[] {"CreateAlbum"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", SecurityConstants.TransactionActionType.New, new string[] {"CreateDocumentLibrary"})]
    [TransactionPermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", SecurityConstants.TransactionActionType.New, new string[] {"CreateVideoLibrary"})]
    public override void FlushTransaction() => base.FlushTransaction();

    public override void RecompileItemUrls<T>(T item) => this.RecompileItemUrls<T>(item, SystemManager.CurrentContext.Culture);

    public override void RecompileItemUrls<T>(T item, CultureInfo culture)
    {
      base.RecompileItemUrls<T>(item, culture);
      if ((object) item is IHasContentChildren)
      {
        Type type = item.GetType();
        if (type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
          this.RecompileChildrenUrls<Telerik.Sitefinity.Libraries.Model.Image>((IHasContentChildren) (object) item);
        else if (type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
          this.RecompileChildrenUrls<Telerik.Sitefinity.Libraries.Model.Document>((IHasContentChildren) (object) item);
        else if (type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
          this.RecompileChildrenUrls<Telerik.Sitefinity.Libraries.Model.Video>((IHasContentChildren) (object) item);
      }
      if (!(item is MediaContent mediaContentItem) || !mediaContentItem.Uploaded)
        return;
      this.RecompileMediaFileUrls(mediaContentItem, (Dictionary<string, object>) null);
    }

    protected internal override bool IsSecurityActionGranted(
      ISecuredObject securedObject,
      SecurityActionTypes action)
    {
      if (action == SecurityActionTypes.Modify)
      {
        switch (securedObject)
        {
          case Telerik.Sitefinity.Libraries.Model.Image _:
            return securedObject.IsGranted("Image", "ManageImage");
          case Telerik.Sitefinity.Libraries.Model.Document _:
            return securedObject.IsGranted("Document", "ManageDocument");
          case Telerik.Sitefinity.Libraries.Model.Video _:
            return securedObject.IsGranted("Video", "ManageVideo");
        }
      }
      return base.IsSecurityActionGranted(securedObject, action);
    }

    /// <summary>Validates the upload.</summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    /// <returns></returns>
    protected virtual bool ValidateUpload(MediaContent content, Stream source, string extension)
    {
      Library parent = content.Parent;
      int num = 0;
      Type type = content.GetType();
      if (typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(type))
        num = Config.Get<LibrariesConfig>().Images.AllowedMaxImageSize;
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(type))
        num = Config.Get<LibrariesConfig>().Videos.AllowedMaxVideoSize;
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(type))
        num = Config.Get<LibrariesConfig>().Documents.AllowedMaxDocumentSize;
      bool flag1 = parent.MaxItemSize > 0L;
      bool flag2 = num > 0;
      string format = "'{0}' could not be uploaded, because the maximum allowed file size of {1} KB is exceeded.";
      if (flag1 && source.Length > parent.MaxItemSize * 1024L)
        throw new LibraryItemUploadException(string.Format(format, (object) content.Title, (object) parent.MaxItemSize))
        {
          MaxItemSize = parent.MaxItemSize,
          ContentType = type,
          LibraryErrorType = LibraryErrorType.ImageSizeExceeded
        };
      if (!flag1 & flag2 && source.Length > (long) (num * 1024))
        throw new LibraryItemUploadException(string.Format(format, (object) content.Title, (object) num))
        {
          MaxItemSize = (long) num,
          ContentType = type,
          LibraryErrorType = LibraryErrorType.ImageSizeExceeded
        };
      if (parent.MaxSize > 0L)
      {
        long totalSize = parent.GetTotalSize(this, new Guid[1]
        {
          content.FileId
        });
        if (source.Length + totalSize > parent.MaxSize * 1048576L)
          throw new LibraryItemUploadException(string.Format("'{0}' could not be uploaded, because the maximum allowed library size of {1} MB will be exceeded.", (object) content.Title, (object) parent.MaxSize))
          {
            ContentType = type,
            LibraryErrorType = LibraryErrorType.LibrarySizeExceeded
          };
      }
      return true;
    }

    /// <summary>Uploads the image.</summary>
    /// <param name="image">The image.</param>
    /// <param name="source">The source.</param>
    /// <param name="mimeType">Type of the MIME.</param>
    protected virtual void UploadImage(Telerik.Sitefinity.Libraries.Model.Image image, Stream source, string mimeType) => this.UploadImage(image, source, mimeType, false);

    /// <summary>Uploads the image.</summary>
    /// <param name="image">The image.</param>
    /// <param name="source">The source.</param>
    /// <param name="mimeType">Type of the MIME.</param>
    /// <param name="uploadAndReplace">Whether to replace the existing file with the uploaded one.</param>
    protected virtual void UploadImage(
      Telerik.Sitefinity.Libraries.Model.Image image,
      Stream source,
      string mimeType,
      bool uploadAndReplace)
    {
      Telerik.Sitefinity.Libraries.Model.Album album = image.Album;
      Stream inputStream = (Stream) null;
      Stream stream = (Stream) null;
      System.Drawing.Image image1 = (System.Drawing.Image) null;
      if (image.IsVectorGraphics())
      {
        image.Width = 0;
        image.Height = 0;
      }
      else
        image1 = this.GetImageFromSource(source, out inputStream);
      try
      {
        bool flag1 = false;
        if (image1 != null)
        {
          if (ImagesHelper.ExifRotate(image1))
            flag1 = true;
          bool flag2 = album.NewSize < image1.Width || album.NewSize < image1.Height;
          if (album.ResizeOnUpload & flag2)
          {
            flag1 = true;
            System.Drawing.Image image2 = ImagesHelper.Resize(image1, album.NewSize, false, false);
            if (image2 != image1)
            {
              image1.Dispose();
              image1 = image2;
            }
          }
          image.Width = image1.Width;
          image.Height = image1.Height;
        }
        if (flag1)
        {
          stream = (Stream) new MemoryStream();
          ImagesHelper.SaveImageToStream(image1, stream, mimeType);
          this.UploadDataOnly((MediaContent) image, stream, uploadAndReplace);
        }
        else if (inputStream != null)
          this.UploadDataOnly((MediaContent) image, inputStream, uploadAndReplace);
        else
          this.UploadDataOnly((MediaContent) image, source, uploadAndReplace);
        if (image1 == null)
          return;
        this.RegenerateThumbnails((MediaContent) image, image1, uploadAndReplace);
        image1.Dispose();
      }
      finally
      {
        inputStream?.Dispose();
        stream?.Dispose();
      }
    }

    private System.Drawing.Image GetImageFromSource(Stream source, out Stream inputStream)
    {
      if (source.CanSeek)
      {
        inputStream = (Stream) null;
        try
        {
          return System.Drawing.Image.FromStream(source);
        }
        catch (ArgumentException ex)
        {
          throw new Exception("The file received is not a valid image", (Exception) ex);
        }
      }
      else
      {
        inputStream = (Stream) new MemoryStream();
        source.CopyTo(inputStream);
        inputStream.Position = 0L;
        try
        {
          return System.Drawing.Image.FromStream(inputStream);
        }
        catch (ArgumentException ex)
        {
          throw new InvalidImageException("The file received is not a valid image");
        }
      }
    }

    /// <summary>Generates the thumbnails.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <param name="img">The img.</param>
    [Obsolete("Use RegenerateThumbnails method")]
    protected virtual void GenerateThumbnails(
      MediaContent mediaContent,
      System.Drawing.Image img,
      string mimeType)
    {
      this.RegenerateThumbnails(mediaContent, img);
    }

    /// <summary>Generates the thumbnails.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <param name="img">The img.</param>
    protected internal virtual void RegenerateThumbnails(
      MediaContent mediaContent,
      System.Drawing.Image img,
      params string[] profilesFilter)
    {
      this.RegenerateThumbnails(mediaContent, img, false, profilesFilter);
    }

    /// <summary>Generates the thumbnails.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <param name="img">The img.</param>
    /// <param name="uploadAndReplace">Whether to replace the existing file with the uploaded one.</param>
    protected internal virtual void RegenerateThumbnails(
      MediaContent mediaContent,
      System.Drawing.Image img,
      bool uploadAndReplace,
      params string[] profilesFilter)
    {
      string mimeType = mediaContent is Telerik.Sitefinity.Libraries.Model.Video ? MimeMapping.GetMimeMapping(".jpg") : mediaContent.MimeType;
      bool flag = false;
      string[] array = mediaContent.Parent.ThumbnailProfiles.ToArray<string>();
      if (profilesFilter.Length == 0)
      {
        this.DeleteMediaThumbnails(mediaContent);
        flag = true;
      }
      else
        array = ((IEnumerable<string>) array).Intersect<string>((IEnumerable<string>) profilesFilter).ToArray<string>();
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = Config.Get<LibrariesConfig>().GetThumbnailProfiles(mediaContent.Parent.GetType());
      foreach (string key in array)
      {
        if (thumbnailProfiles.ContainsKey(key))
        {
          ThumbnailProfileConfigElement thumbnailProfile = thumbnailProfiles[key];
          if (thumbnailProfile != null)
            this.RegenerateThumbnail(mediaContent, img, thumbnailProfile, mimeType, uploadAndReplace);
        }
        else
          Log.Error("The library {0} has a thumbnail profile with name {1} but there is no such configured.", (object) mediaContent.Parent.Title, (object) key);
      }
      if (flag)
      {
        Thumbnail thumbnail = this.CreateThumbnail("0");
        thumbnail.Type = ThumbnailTypes.System;
        thumbnail.Parent = mediaContent;
        FitToSideArguments args = new FitToSideArguments()
        {
          Size = 160,
          ScaleUp = false
        };
        System.Drawing.Image image = this.ImageProcessor.Resize(img, args);
        thumbnail.Width = image.Width;
        thumbnail.Height = image.Height;
        thumbnail.MimeType = mimeType;
        this.UploadThumbnail(thumbnail, image, uploadAndReplace);
        if (image != img)
          image.Dispose();
      }
      ++mediaContent.ThumbnailsVersion;
    }

    private void RegenerateThumbnail(
      MediaContent mediaContent,
      System.Drawing.Image sourceImage,
      ThumbnailProfileConfigElement thumbnailProfile,
      string mimeType,
      bool uploadAndReplace)
    {
      this.RegenerateThumbnail(mediaContent, sourceImage, thumbnailProfile.Name, thumbnailProfile.Method, thumbnailProfile.MethodArgument, mimeType, uploadAndReplace);
    }

    internal void RegenerateThumbnail(
      MediaContent mediaContent,
      System.Drawing.Image sourceImage,
      string thumbnailProfileName,
      string thumbnailProfileMethod,
      object thumbnailProfileMethodArgument,
      string mimeType,
      bool uploadAndReplace = false)
    {
      System.Drawing.Image image = this.ImageProcessor.ProcessImage(sourceImage, thumbnailProfileMethod, thumbnailProfileMethodArgument);
      Thumbnail thumbnail = mediaContent.GetThumbnails().SingleOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Name == thumbnailProfileName));
      if (thumbnail == null || thumbnail.Culture == null && SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        thumbnail = this.CreateThumbnail(thumbnailProfileName);
        thumbnail.Parent = mediaContent;
        thumbnail.Type = ThumbnailTypes.Autogenerated;
      }
      else if (thumbnail.Type != ThumbnailTypes.Autogenerated)
        return;
      thumbnail.Width = image.Width;
      thumbnail.Height = image.Height;
      thumbnail.MimeType = mimeType;
      this.UploadThumbnail(thumbnail, image, uploadAndReplace);
      if (image == sourceImage)
        return;
      image.Dispose();
    }

    internal void UploadThumbnail(Thumbnail thumbnail, System.Drawing.Image image, bool uploadAndReplace)
    {
      using (MemoryStream source = new MemoryStream())
      {
        thumbnail.MimeType = ImagesHelper.SaveImageToStream(image, (Stream) source, thumbnail.MimeType, true);
        thumbnail.Image = (object) new Bitmap(image);
        this.UploadDataOnly(thumbnail, (Stream) source, uploadAndReplace);
      }
    }

    /// <summary>
    /// Uploads the binary data from the specified stream associated with specified media content item.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    protected virtual void UploadDataOnly(MediaContent content, Stream source) => this.UploadDataOnly(content, source, false);

    /// <summary>
    /// Uploads the binary data from the specified stream associated with specified media content item.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="uploadAndReplace">Whether to replace the existing file with the uploaded one.</param>
    protected virtual void UploadDataOnly(
      MediaContent content,
      Stream source,
      bool uploadAndReplace)
    {
      this.UploadDataOnly(new BlobItemWrapper((IBlobContent) content), source, uploadAndReplace);
    }

    internal void UploadDataOnly(Thumbnail thumbnail, Stream source, bool uploadAndReplace = false)
    {
      this.UploadDataOnly(new BlobItemWrapper((IBlobContent) thumbnail), source, uploadAndReplace);
      if (!thumbnail.Parent.UseLagacyThumbnailsStorage)
        return;
      thumbnail.Data = (byte[]) null;
      thumbnail.Parent.UseLagacyThumbnailsStorage = false;
    }

    private void UploadDataOnly(BlobItemWrapper item, Stream source, bool uploadAndReplace)
    {
      IBlobContent content1 = item.Content;
      BlobStorageManager blobStorageManager = this.GetBlobStorageManager(content1);
      IBlobContentLocation blobContentLocation = (IBlobContentLocation) null;
      if (!(blobStorageManager.Provider is IMediaContentFilePathResolver filePathResolver))
        filePathResolver = (IMediaContentFilePathResolver) this;
      BlobItemWrapper blobItemWrapper = new BlobItemWrapper(content1);
      if (content1.FileId == Guid.Empty)
      {
        blobItemWrapper.SetFilePath(this, filePathResolver, uploadAndReplace);
        if (blobStorageManager.BlobExists((IBlobContentLocation) content1))
        {
          BlobPathExtender blobPathExtender = ObjectFactory.Resolve<BlobPathExtender>(blobStorageManager.Provider.Name);
          content1.FilePath = blobPathExtender.ExtendCurrentPathToUnique(content1.FilePath, content1.Extension);
        }
      }
      else
      {
        if (!item.IsFileUsedByOtherContent(this))
          blobContentLocation = blobStorageManager.ResolveBlobContentLocation(content1);
        blobItemWrapper.SetFilePath(this, filePathResolver, uploadAndReplace);
        if (blobStorageManager.Provider is IExternalBlobStorageProvider provider)
        {
          if (blobContentLocation != null && content1.FilePath.Equals(blobContentLocation.FilePath))
            blobContentLocation = (IBlobContentLocation) null;
          MediaContent content2 = this.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (m => (int) m.Status == 0 && m.Id == ((MediaContent) item.Content).OriginalContentId)).FirstOrDefault<MediaContent>();
          this.ArchiveBlob(blobStorageManager.ResolveBlobContentLocation((IBlobContent) content2), provider, true);
        }
      }
      content1.FileId = Guid.NewGuid();
      if (blobItemWrapper.Content is MediaContent || blobItemWrapper.Content is Thumbnail content3 && content3.Image == null)
        this.UploadToStorage(blobItemWrapper, source, blobStorageManager.Provider);
      if (blobContentLocation == null)
        return;
      this.DeleteBlob(blobContentLocation, blobStorageManager);
    }

    internal IBlobContentLocation GetBlobToDelete(
      BlobStorageManager blobStorageManager,
      string storageProvider,
      IBlobContent content,
      bool includeExternalProviders = false)
    {
      if (blobStorageManager.Provider is IExternalBlobStorageProvider && !includeExternalProviders)
        return (IBlobContentLocation) null;
      if (blobStorageManager == null)
        blobStorageManager = BlobStorageManager.GetManager(storageProvider);
      IBlobContentLocation blobToDelete = (IBlobContentLocation) null;
      if (this.FindAnotherMediaContentForFile(content.FileId, storageProvider, content.Id, true) == null)
        blobToDelete = blobStorageManager.ResolveBlobContentLocation(content);
      return blobToDelete;
    }

    /// <summary>
    /// Uploads the stream of the content to the specified storage.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="blobStorage">The BLOB storage manager.</param>
    internal void UploadToStorage(
      BlobItemWrapper item,
      Stream source,
      BlobStorageProvider blobStorage,
      long bufferSize = 0)
    {
      IBlobContent content = item.Content;
      if (content is IChunksBlobContent chunksBlobContent)
      {
        chunksBlobContent.Uploaded = false;
        chunksBlobContent.NumberOfChunks = 0;
      }
      if (source.CanSeek)
        source.Position = 0L;
      bufferSize = Math.Max(bufferSize, blobStorage.GetStreamingBufferSize());
      if (bufferSize == 0L)
        bufferSize = (long) this.ChunkSize;
      if (chunksBlobContent != null && bufferSize > (long) chunksBlobContent.ChunkSize)
        chunksBlobContent.ChunkSize = (int) bufferSize;
      long num = blobStorage.Upload(content, source, (int) bufferSize);
      if (chunksBlobContent != null)
        chunksBlobContent.Uploaded = true;
      content.TotalSize = num;
      if (content is MediaContent)
        ((MediaContent) content).BlobStorageProvider = blobStorage.Name;
      if (!(blobStorage is IExternalBlobStorageProvider blobStorageProvider))
        return;
      blobStorageProvider.SetProperties((IBlobContentLocation) content, item.GetBlobProperties());
    }

    /// <summary>Uploads blob content to a storage</summary>
    /// <param name="item">The item to upload</param>
    /// <param name="source">The stream of the item to upload</param>
    /// <param name="blobStorage">The destination storage</param>
    /// <param name="bufferSize">The buffer size</param>
    internal void UploadBlobToStorage(
      IBlobContent item,
      Stream source,
      BlobStorageProvider blobStorage,
      long bufferSize = 0)
    {
      if (item is IChunksBlobContent chunksBlobContent)
      {
        chunksBlobContent.Uploaded = false;
        chunksBlobContent.NumberOfChunks = 0;
      }
      if (source.CanSeek)
        source.Position = 0L;
      bufferSize = Math.Max(bufferSize, blobStorage.GetStreamingBufferSize());
      if (bufferSize == 0L)
        bufferSize = (long) this.ChunkSize;
      if (chunksBlobContent != null && bufferSize > (long) chunksBlobContent.ChunkSize)
        chunksBlobContent.ChunkSize = (int) bufferSize;
      blobStorage.Upload(item, source, (int) bufferSize);
      if (chunksBlobContent == null)
        return;
      chunksBlobContent.Uploaded = true;
    }

    string IMediaContentFilePathResolver.GenerateLivePath(
      MediaContent mediaContent)
    {
      MediaFileLink fileLink = mediaContent.GetFileLink();
      return (fileLink == null || fileLink.DefaultUrl == null ? mediaContent.ItemDefaultUrl.ToLowerInvariant().ToString() : fileLink.DefaultUrl).TrimStart('/') + mediaContent.Extension;
    }

    string IMediaContentFilePathResolver.GenerateTempPath(
      MediaContent mediaContent)
    {
      MediaFileLink fileLink = mediaContent.GetFileLink();
      return (fileLink == null || fileLink.DefaultUrl == null ? mediaContent.ItemDefaultUrl.ToLowerInvariant().ToString() : fileLink.DefaultUrl).TrimStart('/') + "_" + mediaContent.FileId.ToString() + mediaContent.Extension;
    }

    internal string GenerateLivePath(MediaContent mediaContent) => ((IMediaContentFilePathResolver) this).GenerateLivePath(mediaContent);

    internal string GenerateTempPath(MediaContent mediaContent) => ((IMediaContentFilePathResolver) this).GenerateTempPath(mediaContent);

    internal string GetMappedBlobStorageProviderName(string blobStorageProvider)
    {
      if (this.blobStorageProviderMappings != null)
      {
        if (string.IsNullOrEmpty(blobStorageProvider))
          blobStorageProvider = ManagerBase<BlobStorageProvider>.GetDefaultProviderName();
        string storageProviderName;
        if (this.blobStorageProviderMappings.TryGetValue(blobStorageProvider, out storageProviderName))
          return storageProviderName;
      }
      return blobStorageProvider;
    }

    protected internal virtual BlobStorageManager GetBlobStorageManager(
      IBlobContent item)
    {
      return this.GetBlobStorageManager(!(item is Thumbnail) ? item as MediaContent : ((Thumbnail) item).Parent);
    }

    /// <summary>
    /// Gets the BLOB storage manager, which persists the binary data of the media content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    protected internal virtual BlobStorageManager GetBlobStorageManager(
      MediaContent content)
    {
      return BlobStorageManager.GetManager(content.GetStorageProviderName(this));
    }

    /// <summary>
    /// Determines whether the blob data of the media content item is used by other media content items.
    /// Called by the provider to validate if the BLOB data has to be deleted.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>
    /// 	<c>true</c> if [is file used by other media content] [the specified content]; otherwise, <c>false</c>.
    /// </returns>
    protected internal virtual bool IsFileUsedByOtherMediaContent(MediaContent content) => this.IsFileUsedByOtherMediaContent(content.FileId, content.BlobStorageProvider, content.Id);

    /// <summary>
    /// Determines whether the blob data of the media content item is used by other media content items.
    /// Called by the provider to validate if the BLOB data has to be deleted.
    /// </summary>
    /// <param name="fileId">The file id.</param>
    /// <param name="storageProvider">The storage provider.</param>
    /// <param name="contentId">The content id.</param>
    /// <returns>
    /// 	<c>true</c> if [is file used by other media content] [the specified content]; otherwise, <c>false</c>.
    /// </returns>
    protected internal bool IsFileUsedByOtherMediaContent(
      Guid fileId,
      string storageProvider,
      Guid contentId)
    {
      return this.FindAnotherMediaContentForFile(fileId, storageProvider, contentId) != null;
    }

    protected internal abstract MediaContent FindAnotherMediaContentForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId);

    internal MediaContent FindAnotherMediaContentForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      bool checkScope)
    {
      return this.FindAnotherMediaContentForFile(fileId, storageProvider, contentId, checkScope, SystemManager.CurrentContext.Culture);
    }

    internal void RecompileLibraryUrl(Library library) => base.RecompileItemUrls<Library>(library);

    internal abstract MediaContent FindAnotherMediaContentForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      bool checkScope,
      CultureInfo cultureInfo);

    protected internal virtual bool IsFileUsedByOtherThumbnail(Thumbnail content) => this.IsFileUsedByOtherThumbnail(content.FileId, content.Parent.BlobStorageProvider, content.Id, ((IBlobContentLocation) content).FilePath);

    protected internal bool IsFileUsedByOtherThumbnail(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      string filePath)
    {
      return this.FindAnotherThumbnailForFile(fileId, storageProvider, contentId, filePath: filePath) != null;
    }

    protected internal abstract Thumbnail FindAnotherThumbnailForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      string culture = null,
      string filePath = null);

    internal abstract Thumbnail FindAnotherThumbnailForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      bool checkScope,
      string culture = null,
      string filePath = null);

    protected virtual bool ShouldGenerateTempPath(MediaContent content)
    {
      if (content.Status == ContentLifecycleStatus.Live)
        return false;
      string storageProvider = content.BlobStorageProvider;
      Guid originalContentId = content.OriginalContentId != Guid.Empty ? content.OriginalContentId : content.Id;
      return this.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (c => c.OriginalContentId == originalContentId && (int) c.Status == 2 && c.BlobStorageProvider == storageProvider)).Any<MediaContent>();
    }

    protected virtual IDictionary<int, Guid> GetLiveFileIds(MediaContent content)
    {
      if (content.Status == ContentLifecycleStatus.Live)
        return (IDictionary<int, Guid>) new Dictionary<int, Guid>();
      string storageProvider = content.BlobStorageProvider;
      Guid originalContentId = content.OriginalContentId != Guid.Empty ? content.OriginalContentId : content.Id;
      return (IDictionary<int, Guid>) this.GetMediaFileLinks().Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (ml => ml.MediaContent != default (object) && ml.MediaContent.OriginalContentId == originalContentId && (int) ml.MediaContent.Status == 2 && ml.MediaContent.BlobStorageProvider == storageProvider)).ToDictionary<MediaFileLink, int, Guid>((Func<MediaFileLink, int>) (mfl => mfl.Culture), (Func<MediaFileLink, Guid>) (mfl => mfl.FileId));
    }

    internal string UrlName => this.urlName;

    private int ChunkSize => Config.Get<LibrariesConfig>().SizeOfChunk;

    /// <summary>
    /// Gets a value indicating whether this provider is default.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDefault => Config.Get<LibrariesConfig>().DefaultProvider.Equals(this.Name);

    private IImageProcessor ImageProcessor => ObjectFactory.Resolve<IImageProcessor>();

    private IVideoThumbnailGenerator VideoThumbnailGenerator => ObjectFactory.Resolve<IVideoThumbnailGenerator>();

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData();

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData GetLanguageData(Guid id);

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public abstract IQueryable<LanguageData> GetLanguageData();

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", new string[] {"ViewImage"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", new string[] {"ViewAlbum"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", new string[] {"ViewVideo"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", new string[] {"ViewVideoLibrary"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", new string[] {"ViewDocument"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, out redirectUrl);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", new string[] {"ViewImage"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", new string[] {"ViewAlbum"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", new string[] {"ViewVideo"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", new string[] {"ViewVideoLibrary"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", new string[] {"ViewDocument"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <inheritdoc />
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", new string[] {"ViewImage"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", new string[] {"ViewAlbum"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", new string[] {"ViewVideo"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", new string[] {"ViewVideoLibrary"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", new string[] {"ViewDocument"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    internal MediaFileLink GetFileFromUrl(
      string url,
      bool published,
      out string redirectUrl,
      out int resolvedCultureId)
    {
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      IQueryable<MediaFileUrl> source1 = this.GetMediaFileUrls().Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => mfu.Url == url));
      IQueryable<MediaFileUrl> source2;
      if (published)
        source2 = source1.Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => (int) mfu.MediaFileLink.MediaContent.Status == 2));
      else
        source2 = source1.Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => (int) mfu.MediaFileLink.MediaContent.Status == 0));
      MediaFileLink linkFromMediaUrl = this.GetMediaFileLinkFromMediaUrl((IQueryable<MediaFileUrl>) source2.OrderByDescending<MediaFileUrl, bool>((Expression<Func<MediaFileUrl, bool>>) (f => f.IsDefault)), published, out redirectUrl);
      resolvedCultureId = linkFromMediaUrl != null ? linkFromMediaUrl.Culture : 0;
      return linkFromMediaUrl;
    }

    /// <inheritdoc />
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Image), "Image", new string[] {"ViewImage"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Album), "Album", new string[] {"ViewAlbum"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Video), "Video", new string[] {"ViewVideo"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary), "VideoLibrary", new string[] {"ViewVideoLibrary"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.Document), "Document", new string[] {"ViewDocument"})]
    [TypedValuePermission(typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary), "DocumentLibrary", new string[] {"ViewDocumentLibrary"})]
    internal MediaFileLink GetFileFromUrl(
      string url,
      bool published,
      int cultureId,
      out string redirectUrl)
    {
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      return this.GetMediaFileLinkFromMediaUrl((IQueryable<MediaFileUrl>) this.GetMediaFileUrls().Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => mfu.Url == url && mfu.MediaFileLink.Culture == cultureId && published == ((int) mfu.MediaFileLink.MediaContent.Status == 2))).OrderByDescending<MediaFileUrl, bool>((Expression<Func<MediaFileUrl, bool>>) (f => f.IsDefault)), published, out redirectUrl);
    }

    private MediaFileLink GetMediaFileLinkFromMediaUrl(
      IQueryable<MediaFileUrl> mfUrls,
      bool published,
      out string redirectUrl)
    {
      foreach (MediaFileUrl mfUrl in (IEnumerable<MediaFileUrl>) mfUrls)
      {
        MediaFileLink mediaFileLink = mfUrl.MediaFileLink;
        MediaContent mediaContent = mediaFileLink.MediaContent;
        if (!mediaContent.IsDeleted && mediaContent.Status != ContentLifecycleStatus.PartialTemp && mediaContent.Status != ContentLifecycleStatus.Temp)
        {
          CultureInfo cultureByLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture);
          if (!published || published && LifecycleExtensions.IsItemPublished<MediaContent>(mediaContent, cultureByLcid) || this.IsSystemProvider())
          {
            redirectUrl = (string) null;
            if (mfUrl.RedirectToDefault)
              redirectUrl = mediaFileLink.MediaContent.ResolveMediaUrl(culture: cultureByLcid);
            mediaContent.Provider = (object) this;
            return mediaFileLink;
          }
        }
      }
      redirectUrl = (string) null;
      return (MediaFileLink) null;
    }

    IQueryable<MediaContent> ILibrariesDataProvider.GetItemsInLibrary(
      Library library)
    {
      return this.GetLibraryItemsInternal(library);
    }

    float ILibrariesDataProvider.GetNextOrdinal(MediaContent item) => this.GetNextOrdinalInternal(item);

    protected virtual IQueryable<MediaContent> GetLibraryItemsInternal(
      Library library)
    {
      return library.Items();
    }

    protected virtual float GetNextOrdinalInternal(MediaContent item)
    {
      if (item.Parent == null)
        return 0.0f;
      return this.GetLibraryItemsInternal(item.Parent).Max<MediaContent, float>((Expression<Func<MediaContent, float>>) (x => x.Ordinal)) + 1f;
    }
  }
}
