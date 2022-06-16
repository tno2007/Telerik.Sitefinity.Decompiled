// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Data.OpenAccessLibrariesProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Events;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Upgrades.To5100;
using Telerik.Sitefinity.Upgrades.To6000;

namespace Telerik.Sitefinity.Modules.Libraries.Data
{
  /// <summary>Implements the libraries data layer with OpenAccess</summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessLibrariesProvider : 
    LibrariesDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider,
    IDataEventProvider,
    IFolderOAProvider,
    IHasParentProvider
  {
    private ICounterDecorator counterDecorator;

    /// <inheritdoc />
    protected internal override Library GetLibrary(Guid id)
    {
      Library library = !(id == Guid.Empty) ? this.GetContext().GetItemById<Library>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid", nameof (id));
      library.Provider = (object) this;
      return library;
    }

    /// <summary>Get a query for all media items</summary>
    /// <returns>Queryable object for all MediaItems</returns>
    protected internal override IQueryable<MediaContent> GetMediaItems()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MediaContent>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MediaContent>((Expression<Func<MediaContent, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Ges the libraries.</summary>
    /// <returns></returns>
    protected internal override IQueryable<Library> GetLibraries()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Library>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Library>((Expression<Func<Library, bool>>) (b => b.ApplicationName == appName));
    }

    /// <inheritdoc />
    public override MediaContent GetMediaItem(Guid id)
    {
      MediaContent mediaItem = !(id == Guid.Empty) ? this.GetContext().GetItemById<MediaContent>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid", nameof (id));
      mediaItem.Provider = (object) this;
      return mediaItem;
    }

    /// <summary>Mark an library for removal</summary>
    /// <param name="libraryToDelete">The library to delete.</param>
    public override void Delete(Library libraryToDelete)
    {
      SitefinityOAContext context = this.GetContext();
      ISecuredObject securityRoot = this.GetSecurityRoot();
      if (securityRoot != null)
        this.DeletePermissionsInheritanceAssociation(securityRoot, (ISecuredObject) libraryToDelete);
      this.providerDecorator.DeletePermissions((object) libraryToDelete);
      IQueryable<MediaContent> source = libraryToDelete.Items();
      Expression<Func<MediaContent, bool>> predicate1 = (Expression<Func<MediaContent, bool>>) (item => (int) item.Status == 0 || (int) item.Status == 4);
      foreach (object obj in (IEnumerable<MediaContent>) source.Where<MediaContent>(predicate1))
        this.DeleteItem(obj);
      Guid libraryId = libraryToDelete.Id;
      IQueryable<Folder> folders = this.GetFolders();
      Expression<Func<Folder, bool>> predicate2 = (Expression<Func<Folder, bool>>) (f => f.RootId == libraryId);
      foreach (Folder folder in (IEnumerable<Folder>) folders.Where<Folder>(predicate2))
        this.Delete(folder);
      context?.Remove((object) libraryToDelete);
      this.CounterDecorator.DeleteCounter(libraryId.ToString());
    }

    /// <summary>
    /// Create a new <c>Album</c> and choose a random identity
    /// </summary>
    /// <returns>Created <c>Album</c> instance</returns>
    public override Telerik.Sitefinity.Libraries.Model.Album CreateAlbum() => this.CreateAlbum(this.GetNewGuid());

    /// <summary>
    /// Create a new <c>Album</c> with a explicitly specified identity
    /// </summary>
    /// <param name="pageId"><c>Album</c> identity</param>
    /// <returns>Created <c>Album</c> instance</returns>
    public override Telerik.Sitefinity.Libraries.Model.Album CreateAlbum(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Album album = new Telerik.Sitefinity.Libraries.Model.Album(this.ApplicationName, id);
      album.Owner = SecurityManager.GetCurrentUserId();
      album.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) album);
      this.OnLibraryCreate((Library) album);
      if (id != Guid.Empty)
        this.GetContext().Add((object) album);
      return album;
    }

    /// <summary>Get a queryable object for all librarys</summary>
    /// <returns>Queryable object for all librarys</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.Album> GetAlbums()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.Album>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Search for an library by its identity</summary>
    /// <param name="pageId">Searched library identity</param>
    /// <returns>
    /// Found Album, or <c>null</c> if not found.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.Album GetAlbum(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Album album = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.Album>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      album.Provider = (object) this;
      return album;
    }

    /// <summary>Creates a new video library.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary() => this.CreateVideoLibrary(this.GetNewGuid());

    /// <summary>
    /// Creates a new video library with an explicitly specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the video library to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary(
      Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.VideoLibrary videoLibrary = new Telerik.Sitefinity.Libraries.Model.VideoLibrary(this.ApplicationName, id);
      videoLibrary.Owner = SecurityManager.GetCurrentUserId();
      videoLibrary.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) videoLibrary);
      this.OnLibraryCreate((Library) videoLibrary);
      if (id != Guid.Empty)
        this.GetContext().Add((object) videoLibrary);
      return videoLibrary;
    }

    /// <summary>Gets a queryable object for all video libraries.</summary>
    /// <returns>Queryable object for all video libraries.</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.VideoLibrary> GetVideoLibraries()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((DataProviderBase) this, "VideoLibrary").Where<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets a video library by its identity.</summary>
    /// <param name="pageId">The identity of the video library to get.</param>
    /// <returns>
    /// The <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.VideoLibrary GetVideoLibrary(
      Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.VideoLibrary videoLibrary = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.VideoLibrary>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      videoLibrary.Provider = (object) this;
      return videoLibrary;
    }

    /// <summary>Creates a new document library.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary() => this.CreateDocumentLibrary(this.GetNewGuid());

    /// <summary>
    /// Creates a new document library with an explicitly specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the document library to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary(
      Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary documentLibrary = new Telerik.Sitefinity.Libraries.Model.DocumentLibrary(this.ApplicationName, id);
      documentLibrary.LastModified = DateTime.UtcNow;
      documentLibrary.Owner = SecurityManager.GetCurrentUserId();
      documentLibrary.Provider = (object) this;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) documentLibrary);
      this.OnLibraryCreate((Library) documentLibrary);
      if (id != Guid.Empty)
        this.GetContext().Add((object) documentLibrary);
      return documentLibrary;
    }

    /// <summary>Gets a queryable object for all document libraries.</summary>
    /// <returns>Queryable object for all document libraries.</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.DocumentLibrary> GetDocumentLibraries()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((DataProviderBase) this, "DocumentLibrary").Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets a document library by its identity.</summary>
    /// <param name="pageId">The identity of the document library to get.</param>
    /// <returns>
    /// The <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetDocumentLibrary(
      Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary documentLibrary = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      documentLibrary.Provider = (object) this;
      return documentLibrary;
    }

    /// <summary>Create a new image and choose a random identity</summary>
    /// <returns>Created Image instance.</returns>
    public override Telerik.Sitefinity.Libraries.Model.Image CreateImage() => this.CreateImage(this.GetNewGuid());

    /// <summary>Create an image by explicitly set its identity</summary>
    /// <param name="pageId">Identity of the Image to create</param>
    /// <returns>Created Image instance.</returns>
    public override Telerik.Sitefinity.Libraries.Model.Image CreateImage(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Image entity = new Telerik.Sitefinity.Libraries.Model.Image(this.ApplicationName, id);
      entity.Owner = SecurityManager.GetCurrentUserId();
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Handles the event of changing the parent of the image</summary>
    /// <param name="sender">The sender (image).</param>
    /// <param name="parent">The parent (album).</param>
    private void Image_ParentChanged(MediaContent sender, Content parent)
    {
      if (parent == null || sender == null)
        return;
      if (!this.SuppressSecurityChecks)
        ((ISecuredObject) parent).Demand("Image", "ManageImage");
      if (sender.Parent != null)
        this.providerDecorator.DeletePermissionsInheritanceAssociation((ISecuredObject) sender.Parent, (ISecuredObject) sender);
      this.providerDecorator.CreatePermissionInheritanceAssociation((ISecuredObject) parent, (ISecuredObject) sender);
    }

    /// <summary>Get a query for all images</summary>
    /// <returns>Queryable object for all images</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.Image> GetImages()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.Image>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Search for an image by its identity</summary>
    /// <param name="pageId">Identity of the searched image</param>
    /// <returns>Found image.</returns>
    public override Telerik.Sitefinity.Libraries.Model.Image GetImage(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Image image = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.Image>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      image.Provider = (object) this;
      return image;
    }

    /// <summary>Mark an image for removal</summary>
    /// <param name="imageToDelete">The image to delete.</param>
    public override void Delete(Telerik.Sitefinity.Libraries.Model.Image imageToDelete)
    {
      SitefinityOAContext context = this.GetContext();
      if (imageToDelete.Parent != null)
        this.DeletePermissionsInheritanceAssociation((ISecuredObject) imageToDelete.Parent, (ISecuredObject) imageToDelete);
      this.providerDecorator.DeletePermissions((object) imageToDelete);
      this.ClearLifecycle<Telerik.Sitefinity.Libraries.Model.Image>(imageToDelete, this.GetImages());
      if (context == null)
        return;
      this.DeleteMediaContent((MediaContent) imageToDelete, context);
    }

    /// <summary>Creates a thumbnail for the specified Image and size</summary>
    /// <param name="contentId"></param>
    /// <param name="size"></param>
    /// <returns>Created Image instance.</returns>
    public override Thumbnail CreateThumbnail(int size)
    {
      Thumbnail entity = new Thumbnail(this.GetNewGuid(), size.ToString());
      entity.Culture = AppSettings.CurrentSettings.CurrentCulture.Name;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Creates a thumbnail for the specified thumbnail profile
    /// </summary>
    /// <param name="contentId"></param>
    /// <param name="size"></param>
    /// <returns>Created Image instance.</returns>
    public override Thumbnail CreateThumbnail(string profileName)
    {
      Thumbnail entity = new Thumbnail(this.GetNewGuid(), profileName);
      entity.Culture = AppSettings.CurrentSettings.CurrentCulture.Name;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Returns all thumbnails</summary>
    /// <returns>Query object for all thumbnails</returns>
    public override IQueryable<Thumbnail> GetThumbnails() => SitefinityQuery.Get<Thumbnail>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Select<Thumbnail, Thumbnail>((Expression<Func<Thumbnail, Thumbnail>>) (thumb => thumb));

    /// <inheritdoc />
    public override void DeleteMediaThumbnails(MediaContent content)
    {
      if (!content.GetThumbnails().Any<Thumbnail>())
        return;
      List<Thumbnail> thumbnailList = new List<Thumbnail>(content.GetThumbnails());
      CultureInfo currentCulture = DataExtensions.AppSettings.Current.CurrentCulture;
      bool flag = ((IEnumerable<CultureInfo>) content.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => c != CultureInfo.InvariantCulture && c != currentCulture)).Any<CultureInfo>((Func<CultureInfo, bool>) (c => !content.Thumbnails.Any<Thumbnail>((Func<Thumbnail, bool>) (t => t.Culture == c.Name))));
      foreach (Thumbnail thumbnail in thumbnailList)
      {
        if (!(thumbnail.Culture == null & flag))
        {
          this.Delete(thumbnail);
          content.Thumbnails.Remove(thumbnail);
        }
      }
    }

    /// <inheritdoc />
    public override void DeleteMediaFileLinks(MediaContent content)
    {
      if (!content.MediaFileLinks.Any<MediaFileLink>())
        return;
      foreach (MediaFileLink mediaFileLink in new List<MediaFileLink>((IEnumerable<MediaFileLink>) content.MediaFileLinks))
      {
        this.DeleteMediaFileUrls(mediaFileLink);
        this.Delete(mediaFileLink);
        content.MediaFileLinks.Remove(mediaFileLink);
      }
    }

    /// <inheritdoc />
    public override void DeleteMediaFileUrls(MediaFileLink mediaFileLink)
    {
      if (!mediaFileLink.Urls.Any<MediaFileUrl>())
        return;
      foreach (MediaFileUrl mediaFileUrlToDelete in new List<MediaFileUrl>((IEnumerable<MediaFileUrl>) mediaFileLink.Urls))
      {
        this.Delete(mediaFileUrlToDelete);
        mediaFileLink.Urls.Remove(mediaFileUrlToDelete);
      }
    }

    /// <summary>Creates a new video.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.Video CreateVideo() => this.CreateVideo(this.GetNewGuid());

    /// <summary>
    /// Creates a new video with an explicitly specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the video to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.Video CreateVideo(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Video entity = new Telerik.Sitefinity.Libraries.Model.Video(this.ApplicationName, id);
      entity.Owner = SecurityManager.GetCurrentUserId();
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Handles the event of changing the parent of the image</summary>
    /// <param name="sender">The sender (video).</param>
    /// <param name="parent">The parent (video library).</param>
    private void Video_ParentChanged(MediaContent sender, Content parent)
    {
      if (parent == null || sender == null)
        return;
      if (!this.SuppressSecurityChecks)
        ((ISecuredObject) parent).Demand("Video", "ManageVideo");
      if (sender.Parent != null)
        this.providerDecorator.DeletePermissionsInheritanceAssociation((ISecuredObject) sender.Parent, (ISecuredObject) sender);
      this.providerDecorator.CreatePermissionInheritanceAssociation((ISecuredObject) parent, (ISecuredObject) sender);
    }

    /// <summary>Gets a query for all videos.</summary>
    /// <returns>Queryable object for all videos.</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.Video> GetVideos()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.Video>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets a video by its identity.</summary>
    /// <param name="pageId">The identity of the video to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.</returns>
    public override Telerik.Sitefinity.Libraries.Model.Video GetVideo(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Video video = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.Video>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      video.Provider = (object) this;
      return video;
    }

    /// <summary>Marks a video for removal.</summary>
    /// <param name="videoToDelete">The video to delete.</param>
    public override void Delete(Telerik.Sitefinity.Libraries.Model.Video videoToDelete)
    {
      SitefinityOAContext context = this.GetContext();
      if (videoToDelete.Parent != null)
        this.DeletePermissionsInheritanceAssociation((ISecuredObject) videoToDelete.Parent, (ISecuredObject) videoToDelete);
      this.providerDecorator.DeletePermissions((object) videoToDelete);
      this.ClearLifecycle<Telerik.Sitefinity.Libraries.Model.Video>(videoToDelete, this.GetVideos());
      if (context == null)
        return;
      this.DeleteMediaContent((MediaContent) videoToDelete, context);
    }

    /// <summary>Creates a new document.</summary>
    /// <returns>
    /// The newly created <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" />
    /// </returns>
    public override Telerik.Sitefinity.Libraries.Model.Document CreateDocument() => this.CreateDocument(this.GetNewGuid());

    public override Telerik.Sitefinity.Libraries.Model.Document CreateDocument(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Document entity = new Telerik.Sitefinity.Libraries.Model.Document(this.ApplicationName, id);
      entity.Owner = SecurityManager.GetCurrentUserId();
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Handles the event of changing the parent of the document
    /// </summary>
    /// <param name="sender">The sender (document).</param>
    /// <param name="parent">The parent (document library).</param>
    private void Document_ParentChanged(MediaContent sender, Content parent)
    {
      if (parent == null || sender == null)
        return;
      if (!this.SuppressSecurityChecks)
        ((ISecuredObject) parent).Demand("Document", "ManageDocument");
      if (sender.Parent != null)
        this.providerDecorator.DeletePermissionsInheritanceAssociation((ISecuredObject) sender.Parent, (ISecuredObject) sender);
      this.providerDecorator.CreatePermissionInheritanceAssociation((ISecuredObject) parent, (ISecuredObject) sender);
    }

    /// <summary>Gets a query for all documents.</summary>
    /// <returns>A queryable object for all documents</returns>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.Document> GetDocuments()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Libraries.Model.Document>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets a document by ID.</summary>
    /// <param name="pageId">The ID.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /> instance</returns>
    public override Telerik.Sitefinity.Libraries.Model.Document GetDocument(Guid id)
    {
      Telerik.Sitefinity.Libraries.Model.Document document = !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Libraries.Model.Document>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      document.Provider = (object) this;
      return document;
    }

    /// <summary>Marks a document for removal.</summary>
    /// <param name="documentToDelete">The document to delete.</param>
    public override void Delete(Telerik.Sitefinity.Libraries.Model.Document documentToDelete)
    {
      SitefinityOAContext context = this.GetContext();
      if (documentToDelete.Parent != null)
        this.DeletePermissionsInheritanceAssociation((ISecuredObject) documentToDelete.Parent, (ISecuredObject) documentToDelete);
      this.providerDecorator.DeletePermissions((object) documentToDelete);
      this.ClearLifecycle<Telerik.Sitefinity.Libraries.Model.Document>(documentToDelete, this.GetDocuments());
      if (context == null)
        return;
      this.DeleteMediaContent((MediaContent) documentToDelete, context);
    }

    /// <summary>Create a new media file link</summary>
    /// <returns>Created media file link instance.</returns>
    public override MediaFileLink CreateMediaFileLink()
    {
      MediaFileLink entity = new MediaFileLink();
      entity.Id = this.GetNewGuid();
      entity.ApplicationName = this.ApplicationName;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Get a query for all media file links</summary>
    /// <returns>Queryable object for all media file links</returns>
    public override IQueryable<MediaFileLink> GetMediaFileLinks() => SitefinityQuery.Get<MediaFileLink>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (b => b.ApplicationName == this.ApplicationName));

    /// <summary>Mark an media file link for removal</summary>
    /// <param name="mediaFileLinkToDelete">The media file link to delete.</param>
    public override void Delete(MediaFileLink mediaFileLinkToDelete)
    {
      foreach (MediaFileUrl mediaFileUrlToDelete in mediaFileLinkToDelete.Urls.ToList<MediaFileUrl>())
        this.Delete(mediaFileUrlToDelete);
      this.GetContext().Remove((object) mediaFileLinkToDelete);
    }

    /// <summary>Create a new media file url</summary>
    /// <returns>Created media file url instance.</returns>
    public override MediaFileUrl CreateMediaFileUrl()
    {
      MediaFileUrl entity = new MediaFileUrl();
      entity.Id = this.GetNewGuid();
      entity.ApplicationName = this.ApplicationName;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Get a query for all media file urls</summary>
    /// <returns>Queryable object for all media file urls</returns>
    public override IQueryable<MediaFileUrl> GetMediaFileUrls()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MediaFileUrl>((DataProviderBase) this).Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Mark an media file url for removal</summary>
    /// <param name="mediaFileUrlToDelete">The media file url to delete.</param>
    public override void Delete(MediaFileUrl mediaFileUrlToDelete) => this.GetContext().Remove((object) mediaFileUrlToDelete);

    void IHasParentProvider.ParentChanged(IHasParent sender, Content parent)
    {
      if (!(sender is MediaContent sender1))
        throw new InvalidOperationException("Sender type not supported");
      switch (sender1)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          this.Image_ParentChanged(sender1, parent);
          break;
        case Telerik.Sitefinity.Libraries.Model.Video _:
          this.Video_ParentChanged(sender1, parent);
          break;
        case Telerik.Sitefinity.Libraries.Model.Document _:
          this.Document_ParentChanged(sender1, parent);
          break;
        default:
          throw new InvalidOperationException("Sender type not supported");
      }
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new LibrariesMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    internal override void RefreshItem(MediaContent item) => (this.GetTransaction() as SitefinityOAContext).Refresh(RefreshMode.OverwriteChangesFromStore, (object) item);

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public override LanguageData CreateLanguageData() => this.CreateLanguageData(this.GetNewGuid());

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override LanguageData CreateLanguageData(Guid id)
    {
      LanguageData entity = new LanguageData(this.ApplicationName, id);
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override LanguageData GetLanguageData(Guid id)
    {
      LanguageData languageData = !(id == Guid.Empty) ? this.GetContext().GetItemById<LanguageData>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      ((IDataItem) languageData).Provider = (object) this;
      return languageData;
    }

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public override IQueryable<LanguageData> GetLanguageData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<LanguageData>((DataProviderBase) this).Where<LanguageData>((Expression<Func<LanguageData, bool>>) (c => c.ApplicationName == appName));
    }

    protected override void AddEvents(
      ICollection<IEvent> events,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType itemStatus)
    {
      if (!(dataItem is Folder folder))
      {
        List<IEvent> eventList = new List<IEvent>();
        base.AddEvents((ICollection<IEvent>) eventList, dataItem, itemStatus);
        foreach (IEvent evt in eventList)
        {
          IEvent @event = evt;
          if (!(evt is IGeoLocatableEvent) && dataItem is MediaContent media)
          {
            MediaEvent mediaEvent = new MediaEvent(media);
            mediaEvent.CoppyFrom(evt as DataEvent);
            @event = (IEvent) mediaEvent;
          }
          events.Add(@event);
        }
      }
      else
      {
        string actualItemAction = DataEventFactory.GetActualItemAction(dataItem, itemStatus.ToString(), (CultureInfo) null);
        FolderEvent evt = new FolderEvent();
        evt.ItemId = folder.Id;
        evt.ItemType = folder.GetType();
        evt.Action = actualItemAction;
        evt.RootId = folder.RootId;
        evt.Title = ((IHasTitle) folder).GetTitle();
        IDataProviderBase provider1 = dataItem.Provider as IDataProviderBase;
        string provider2 = dataItem.Provider as string;
        if (provider1 != null)
          evt.ProviderName = provider1.Name;
        else if (provider2 != null)
          evt.ProviderName = provider2;
        if (actualItemAction == SecurityConstants.TransactionActionType.Updated.ToString())
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) evt, (IDataItem) folder, (CultureInfo) null);
        events.Add((IEvent) evt);
      }
    }

    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      foreach (object dirtyItem in (IEnumerable) this.GetDirtyItems())
      {
        if (dirtyItem is MediaFileLink mediaFileLink && mediaFileLink.MediaContent != null && this.GetDirtyItemStatus((object) mediaFileLink.MediaContent) != SecurityConstants.TransactionActionType.Deleted)
        {
          string name = AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture).Name;
          if (!SystemManager.CurrentContext.AppSettings.Multilingual || mediaFileLink.MediaContent.PublishedTranslations.Contains(name))
            mediaFileLink.MediaContent.RegisterOperation(OperationStatus.Modified, new string[1]
            {
              name
            });
        }
      }
      return base.GetDataEventItems(filterPredicate);
    }

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item) => item is Content || item is Folder;

    protected internal ICounterDecorator CounterDecorator
    {
      get
      {
        if (this.counterDecorator == null)
          this.counterDecorator = this.CreateCounterDecorator();
        return this.counterDecorator;
      }
    }

    protected virtual ICounterDecorator CreateCounterDecorator() => (ICounterDecorator) new OpenAccessCounterDecorator((IOpenAccessDataProvider) this);

    protected virtual void DeleteMediaContent(MediaContent content, SitefinityOAContext context)
    {
      List<IBlobContentLocation> blobContentLocationList = new List<IBlobContentLocation>();
      BlobStorageManager manager = BlobStorageManager.GetManager(content.GetStorageProviderName((LibrariesDataProvider) this), false);
      if (manager != null && content.Uploaded)
      {
        foreach (MediaFileLink mediaFileLink in content.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId != Guid.Empty)).ToArray<MediaFileLink>())
        {
          if (AppSettings.CurrentSettings.AllLanguages.Values.Contains(AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture)))
          {
            using (new CultureRegion(mediaFileLink.Culture))
            {
              if (!this.IsFileUsedByOtherMediaContent(mediaFileLink.FileId, content.BlobStorageProvider, content.Id))
                blobContentLocationList.Add(manager.ResolveBlobContentLocation((IBlobContent) content));
            }
          }
          mediaFileLink.FileId = Guid.Empty;
        }
      }
      foreach (Thumbnail thumbnail in new List<Thumbnail>((IEnumerable<Thumbnail>) content.Thumbnails))
      {
        this.Delete(thumbnail);
        content.Thumbnails.Remove(thumbnail);
      }
      this.DeleteMediaFileLinks(content);
      content.Parent = (Library) null;
      context.Remove((object) content);
      foreach (IBlobContentLocation blobContentLocation in blobContentLocationList)
      {
        this.DeleteBlob(blobContentLocation, manager, content.IsDeleted);
        this.DeleteBlobFromRecycleBin(blobContentLocation, manager);
      }
    }

    protected internal override Thumbnail FindAnotherThumbnailForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      string culture = null,
      string filePath = null)
    {
      return this.FindAnotherThumbnailForFile(fileId, storageProvider, contentId, false, culture, filePath);
    }

    internal override Thumbnail FindAnotherThumbnailForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      bool checkScope,
      string culture = null,
      string filePath = null)
    {
      SitefinityOAContext context = this.GetContext();
      IEnumerable<Thumbnail> source1 = this.GetDirtyItems().OfType<Thumbnail>();
      foreach (Thumbnail itemInTransaction in source1.Where<Thumbnail>((Func<Thumbnail, bool>) (c => c.FileId == fileId && c.Parent != null && c.Parent.BlobStorageProvider == storageProvider && c.Id != contentId)))
      {
        switch (this.GetDirtyItemStatus((object) itemInTransaction))
        {
          case SecurityConstants.TransactionActionType.Deleted:
          case SecurityConstants.TransactionActionType.None:
            continue;
          default:
            return itemInTransaction;
        }
      }
      IQueryable<Thumbnail> source2 = context.GetAll<Thumbnail>().Where<Thumbnail>((Expression<Func<Thumbnail, bool>>) (c => c.FileId == fileId && c.Id != contentId));
      if (!string.IsNullOrEmpty(culture))
        source2 = source2.Where<Thumbnail>((Expression<Func<Thumbnail, bool>>) (r => r.Culture == culture));
      foreach (Thumbnail thumbnail in (IEnumerable<Thumbnail>) source2)
      {
        Thumbnail item = thumbnail;
        SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus((object) item);
        if (dirtyItemStatus != SecurityConstants.TransactionActionType.Deleted && (dirtyItemStatus != SecurityConstants.TransactionActionType.Updated || !(item.FileId != fileId)) && (dirtyItemStatus != SecurityConstants.TransactionActionType.Updated || item.Parent == null || item.Parent.BlobStorageProvider.Equals(storageProvider)) && (!checkScope || !source1.Any<Thumbnail>((Func<Thumbnail, bool>) (i => i.Id == item.Id))))
          return item;
      }
      foreach (IBlobContentLocation itemInTransaction in source1.OfType<IBlobContentLocation>())
      {
        if (itemInTransaction.FilePath == filePath && this.GetDirtyItemStatus((object) itemInTransaction) != SecurityConstants.TransactionActionType.Deleted)
          return (Thumbnail) itemInTransaction;
      }
      return (Thumbnail) null;
    }

    protected internal override MediaContent FindAnotherMediaContentForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId)
    {
      return this.FindAnotherMediaContentForFile(fileId, storageProvider, contentId, false, SystemManager.CurrentContext.Culture);
    }

    internal override MediaContent FindAnotherMediaContentForFile(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      bool checkScope,
      CultureInfo cultureInfo)
    {
      MediaFileLink mediaFileLink1 = this.GetDirtyItems().OfType<MediaFileLink>().AsQueryable<MediaFileLink>().Where<MediaFileLink>(OpenAccessLibrariesProvider.IsDuplicateLinkPredicate(fileId, storageProvider, contentId, cultureInfo)).FirstOrDefault<MediaFileLink>();
      if (mediaFileLink1 != null)
        return mediaFileLink1.MediaContent;
      MediaFileLink mediaFileLink2 = this.GetMediaFileLinks().Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (l => l.FileId == fileId)).ToList<MediaFileLink>().AsQueryable<MediaFileLink>().FirstOrDefault<MediaFileLink>(OpenAccessLibrariesProvider.IsDuplicateLinkPredicate(fileId, storageProvider, contentId, cultureInfo));
      return mediaFileLink2 != null && mediaFileLink2.FileId == fileId && mediaFileLink2.MediaContent != null && mediaFileLink2.MediaContent.BlobStorageProvider.Equals(storageProvider) ? mediaFileLink2.MediaContent : (MediaContent) null;
    }

    /// <summary>Changes the order position of the item</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The item id.</param>
    /// <param name="newPosition">New position.</param>
    public override void ReorderItem(Type itemType, Guid id, float newPosition)
    {
      this.EnsureCounterIsValid(id, newPosition);
      base.ReorderItem(itemType, id, newPosition);
    }

    /// <summary>Changes the order position of the item</summary>
    /// <param name="itemType">Type of the item. Must implement IOrderedItem</param>
    /// <param name="id">The item id.</param>
    /// <param name="oldPosition">Old position.</param>
    /// <param name="newPosition">New position.</param>
    /// <param name="insertBefore">Insert before.</param>
    public override void ReorderItem(
      Type itemType,
      Guid id,
      float oldPosition,
      float newPosition,
      bool insertBefore)
    {
      this.EnsureCounterIsValid(id, newPosition);
      base.ReorderItem(itemType, id, oldPosition, newPosition, insertBefore);
    }

    protected override float GetNextOrdinalInternal(MediaContent item) => item.Parent != null ? (float) this.CounterDecorator.GetNextValue(item.Parent.Id.ToString()) : 0.0f;

    /// <summary>Delete a thumbnail</summary>
    /// <param name="thumbnail">Thumbnail to delete</param>
    public override void Delete(Thumbnail thumbnail)
    {
      SitefinityOAContext context = this.GetContext();
      IBlobContentLocation blobContentLocation = (IBlobContentLocation) null;
      BlobStorageManager manager = BlobStorageManager.GetManager(thumbnail.Parent.GetStorageProviderName((LibrariesDataProvider) this), false);
      if (manager != null && thumbnail.FileId != Guid.Empty && !this.IsFileUsedByOtherThumbnail(thumbnail))
        blobContentLocation = manager.ResolveBlobContentLocation((IBlobContent) thumbnail);
      bool isDeleted = thumbnail.Parent.IsDeleted;
      thumbnail.Parent = (MediaContent) null;
      Thumbnail entity = thumbnail;
      context.Remove((object) entity);
      if (blobContentLocation == null)
        return;
      if (!isDeleted)
        manager.Delete(blobContentLocation);
      this.DeleteBlobFromRecycleBin(blobContentLocation, manager);
    }

    private void EnsureCounterIsValid(Guid id, float newOrdinal)
    {
      MediaContent mediaItem = this.GetMediaItem(id);
      if (mediaItem.Parent == null)
        return;
      string name = mediaItem.Parent.Id.ToString();
      long currentValue = this.CounterDecorator.GetCurrentValue(name);
      int incrementStep = (int) ((double) newOrdinal - (double) currentValue);
      if (incrementStep <= 0)
        return;
      this.CounterDecorator.GetNextValue(name, incrementStep);
    }

    private static Expression<Func<MediaFileLink, bool>> IsDuplicateLinkPredicate(
      Guid fileId,
      string storageProvider,
      Guid contentId,
      CultureInfo culture)
    {
      if (culture != null)
      {
        int cultureId = AppSettings.CurrentSettings.GetCultureLcid(culture);
        return (Expression<Func<MediaFileLink, bool>>) (l => l.FileId == fileId && l.MediaContent != default (object) && l.MediaContent.BlobStorageProvider == storageProvider && (l.MediaContent.Id != contentId || l.Culture != cultureId));
      }
      return (Expression<Func<MediaFileLink, bool>>) (l => l.FileId == fileId && l.MediaContent != default (object) && l.MediaContent.BlobStorageProvider == storageProvider && l.MediaContent.Id != contentId);
    }

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public virtual void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber < 3040)
      {
        string upgradeScript = context.DatabaseContext.DatabaseType != DatabaseType.MySQL ? "ALTER TABLE \"sf_libraries\" DROP COLUMN \"voa_version\"" : "ALTER TABLE sf_libraries DROP COLUMN voa_version";
        OpenAccessConnection.Upgrade(context, "Removing the voa_version optimistic locking version column from Libraries.", upgradeScript);
      }
      if (upgradingFromSchemaVersionNumber >= SitefinityVersion.Sitefinity6_1.Build)
        return;
      SqlUpgradeScriptInfo[] upgradeScriptInfoArray;
      if (context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
        upgradeScriptInfoArray = new SqlUpgradeScriptInfo[7]
        {
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table \"sf_media_thumbnails\" add \"id\" varchar(40)"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table \"sf_media_thumbnails\" add \"nme\" varchar(10)"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = string.Format("update \"sf_media_thumbnails\" set \"id\"=sys_guid(), \"nme\" = N'{0}' where \"sze\" = 120", (object) "thumbnail"),
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "update \"sf_media_thumbnails\" set \"id\"=sys_guid(), \"nme\" = '0' where \"sze\" = 0"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table \"sf_media_thumbnails\" drop column \"sze\""
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table \"sf_media_thumbnails\" drop constraint \"pk_sf_media_thumbnails\""
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table \"sf_media_thumbnails\" drop column \"sf_media_thumbnails_id\""
          }
        };
      else if (context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
        upgradeScriptInfoArray = new SqlUpgradeScriptInfo[6]
        {
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails add id varchar(40)"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails add nme varchar(10)"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = string.Format("update sf_media_thumbnails set id=UUID(), nme = N'{0}' where sze = 120", (object) "thumbnail"),
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "update sf_media_thumbnails set id=UUID(), nme = '0' where sze = 0"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails drop column sze"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails drop column sf_media_thumbnails_id"
          }
        };
      else if (context.DatabaseContext.DatabaseType == DatabaseType.SqlAzure)
        upgradeScriptInfoArray = new SqlUpgradeScriptInfo[5]
        {
          new SqlUpgradeScriptInfo()
          {
            Text = "\r\nCREATE TABLE [sf_media_thumbnails_tmp](\r\n\t[id] [uniqueidentifier] NOT NULL,\r\n\t[content_id] [uniqueidentifier] NULL,\r\n\t[nme] [varchar](10) NULL,\r\n\t[mime_type] [varchar](255) NULL,\r\n\t[dta] [image] NULL,\r\n\t[voa_version] [smallint] NOT NULL,\r\n    CONSTRAINT [pk_sf_media_thumbnails_tmp] PRIMARY KEY CLUSTERED \r\n    (\r\n\t    [id] ASC\r\n    )\r\n) \r\n\r\n",
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = string.Format("insert into sf_media_thumbnails_tmp (id, content_id, nme, mime_type, dta, voa_version) select newid(), content_id, case sze when 120 then N'{0}' else '0' end, mime_type, dta, voa_version from sf_media_thumbnails", (object) "thumbnail"),
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "drop table sf_media_thumbnails",
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "sp_rename sf_media_thumbnails_tmp, sf_media_thumbnails",
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "sp_rename pk_sf_media_thumbnails_tmp, pk_sf_media_thumbnails",
            IsRequired = true
          }
        };
      else
        upgradeScriptInfoArray = new SqlUpgradeScriptInfo[7]
        {
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails add id uniqueidentifier"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails add nme varchar(10)"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = string.Format("update sf_media_thumbnails set id=newid(), nme = N'{0}' where sze = 120", (object) "thumbnail"),
            IsRequired = true
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "update sf_media_thumbnails set id=newid(), nme = '0' where sze = 0"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails drop column sze"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails drop constraint pk_sf_media_thumbnails"
          },
          new SqlUpgradeScriptInfo()
          {
            Text = "alter table sf_media_thumbnails drop column sf_media_thumbnails_id"
          }
        };
      if (upgradeScriptInfoArray == null)
        return;
      string str = string.Empty;
      string empty = string.Empty;
      for (int index = 0; index < upgradeScriptInfoArray.Length; ++index)
      {
        try
        {
          context.ExecuteSQL(upgradeScriptInfoArray[index].Text);
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          {
            throw;
          }
          else
          {
            if (upgradeScriptInfoArray[index].IsRequired)
            {
              str = ex.Message;
              break;
            }
            if (empty.Length > 0)
              empty += "; ";
            empty += ex.Message;
          }
        }
      }
      Log.Write(str.Length <= 0 ? (empty.Length <= 0 ? (object) string.Format("PASSED: {0} - Prepare 'sf_media_thumbnails' table for upgrade", (object) MethodBase.GetCurrentMethod().DeclaringType.Name) : (object) string.Format("PASSED WITH WARNINGS: {0}: Prepare 'sf_media_thumbnails' table for upgrade: {1}.{2}", (object) MethodBase.GetCurrentMethod().DeclaringType.Name, (object) empty, (object) "For more details see the error log")) : (object) string.Format("FAILED: {0} - Prepare 'sf_media_thumbnails' table for upgrade. Error: {1}", (object) MethodBase.GetCurrentMethod().DeclaringType.Name, (object) str), ConfigurationPolicy.UpgradeTrace);
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 1300)
        OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessLibrariesProvider: Upgrade to 1300", (System.Action<IDbCommand>) (cmd =>
        {
          try
          {
            cmd.ExecuteNonQuery("alter table sf_chunks add file_id uniqueidentifier");
          }
          catch
          {
          }
          try
          {
            cmd.ExecuteNonQuery("UPDATE sf_chunks SET file_id = [content_id2] WHERE [content_id2] IS NOT NULL");
            cmd.ExecuteNonQuery("Update sf_media_content \r\nSET\r\nfile_id = b.[content_id]\r\nFROM (\r\nSELECT  distinct sf_media_content.content_id\r\nFROM         sf_chunks INNER JOIN\r\n            sf_media_content ON sf_chunks.content_id = sf_media_content.content_id) as b\r\n\r\nWHERE sf_media_content.[content_id] = b.content_id AND b.content_id IS NOT NULL");
          }
          catch
          {
          }
        }));
      if (upgradedFromSchemaVersionNumber < 1600)
      {
        context.GetAll<Library>().Where<Library>((Expression<Func<Library, bool>>) (i => i.BlobStorageProvider == default (string))).UpdateAll<Library>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<Library>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<Library>>>) (u => u.Set<string>((Expression<Func<Library, string>>) (i => i.BlobStorageProvider), (Expression<Func<Library, string>>) (i => "Database"))));
        context.GetAll<MediaContent>().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.BlobStorageProvider == default (string))).UpdateAll<MediaContent>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<MediaContent>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<MediaContent>>>) (u => u.Set<string>((Expression<Func<MediaContent, string>>) (i => i.BlobStorageProvider), (Expression<Func<MediaContent, string>>) (i => "Database"))));
      }
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity6_1.Build)
      {
        string[] strArray;
        if (context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
          strArray = new string[5]
          {
            "update \"sf_media_thumbnails\" set \"total_size\"=lengthb(\"dta\") where \"dta\" is not null",
            "update \"sf_media_content\" set \"lgcy_tmb_strg\"=1 where \"blob_storage\" <> 'Database'",
            "update \"sf_libraries\" set \"tmb_regen\"=1 where \"voa_class\" = 447337637 and \"blob_storage\" <> 'Database'",
            "update \"sf_media_thumbnails\" set \"typ\" = 1 where \"nme\" = '0'",
            string.Format("update \"sf_media_thumbnails\" set \"typ\" = 2 where \"nme\" = '{0}'", (object) "thumbnail")
          };
        else if (context.DatabaseContext.DatabaseType == DatabaseType.MySQL)
          strArray = new string[5]
          {
            "update sf_media_thumbnails set total_size=LENGTH(dta) where dta is not null",
            "update sf_media_content set lgcy_tmb_strg=1 where blob_storage <> 'Database'",
            "update sf_libraries set tmb_regen=1 where voa_class = 447337637 and blob_storage <> 'Database'",
            "update sf_media_thumbnails set typ = 1 where nme = '0'",
            string.Format("update sf_media_thumbnails set typ = 2 where nme = '{0}'", (object) "thumbnail")
          };
        else
          strArray = new string[5]
          {
            "update sf_media_thumbnails set total_size=DATALENGTH(dta) where dta is not null",
            "update sf_media_content set lgcy_tmb_strg=1 where blob_storage <> 'Database'",
            "update sf_libraries set tmb_regen=1 where voa_class = 447337637 and blob_storage <> 'Database'",
            "update sf_media_thumbnails set typ = 1 where nme = '0'",
            string.Format("update sf_media_thumbnails set typ = 2 where nme = '{0}'", (object) "thumbnail")
          };
        string empty = string.Empty;
        for (int index = 0; index < strArray.Length; ++index)
        {
          try
          {
            using (OACommand command = ((OpenAccessContextBase) context).Connection.CreateCommand())
            {
              command.CommandTimeout = 180;
              command.CommandText = strArray[index];
              command.ExecuteNonQuery();
              context.SaveChanges();
            }
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            {
              throw;
            }
            else
            {
              if (empty.Length > 0)
                empty += "; ";
              empty += ex.Message;
            }
          }
        }
        Log.Write(empty.Length <= 0 ? (object) string.Format("PASSED: {0} - Prepare thumbnails legacy mode", (object) MethodBase.GetCurrentMethod().DeclaringType.Name) : (object) string.Format("PASSED WITH WARNINGS: {0}: Prepare thumbnails legacy mode: {1}.{2}", (object) MethodBase.GetCurrentMethod().DeclaringType.Name, (object) empty, (object) "For more details see the error log"), ConfigurationPolicy.UpgradeTrace);
      }
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity7_0.Build)
        ApprovalRecordsUpgrader.UpgradeDb((OpenAccessContext) context, "sf_media_content", this.GetType().Name);
      if (upgradedFromSchemaVersionNumber >= SitefinityVersion.Sitefinity9_0.Build)
        return;
      new MediaContentFileUrlsUpgrader(context, upgradedFromSchemaVersionNumber).Upgrade();
    }
  }
}
