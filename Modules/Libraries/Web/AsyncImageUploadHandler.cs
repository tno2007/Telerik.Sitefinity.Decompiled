// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.AsyncImageUploadHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  /// <summary>
  /// </summary>
  public class AsyncImageUploadHandler : IHttpHandler
  {
    /// <summary>Gets or sets the provider.</summary>
    /// <value>The provider.</value>
    public LibrariesManager Manager { get; set; }

    /// <summary>Gets or sets the content.</summary>
    /// <value>The content.</value>
    public MediaContent Content { get; set; }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => false;

    /// <summary>The file uploaded with the request</summary>
    public UploadedFile UploadedFile { get; set; }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      AsyncImageUploadHandler.UploadResponse clrObject = new AsyncImageUploadHandler.UploadResponse()
      {
        ContentId = (string) null,
        ErrorMessage = "No file uploaded",
        UploadResult = false
      };
      if (context.Request.Files.Count == 1)
      {
        this.UploadedFile = UploadedFile.FromHttpPostedFile(context.ApplicationInstance.Context.Request.Files[0]);
        this.SetUpMediaAndContent(context);
        clrObject = this.SaveData(context);
      }
      context.Response.Write(WcfHelper.SerializeToJson((object) clrObject, typeof (AsyncImageUploadHandler.UploadResponse)));
      context.Response.End();
    }

    /// <summary>Sets up the media content.</summary>
    /// <param name="context">The context.</param>
    internal virtual void SetUpMediaAndContent(HttpContextBase context)
    {
      Type itemType = TypeResolutionService.ResolveType(context.Request.Form["ContentType"]);
      string empty = string.Empty;
      if (context.Request.Form["ProviderName"] != null && context.Request.Form["ProviderName"] != "null")
        empty = context.Request.Form["ProviderName"];
      this.Manager = string.IsNullOrEmpty(empty) ? ManagerBase.GetMappedManager(itemType) as LibrariesManager : ManagerBase.GetMappedManager(itemType, empty) as LibrariesManager;
      if (this.Manager == null)
        throw new ApplicationException("Unable to get manager for type :" + itemType.FullName);
      string g = context.Request.Form["ContentId"];
      Guid id = Guid.Empty;
      if (!string.IsNullOrEmpty(g))
        id = new Guid(g);
      if (id != Guid.Empty)
        this.Content = this.Manager.GetItem(itemType, id) as MediaContent;
      else if (itemType == typeof (Image))
      {
        Image image = this.Manager.CreateImage();
        Album album1 = (Album) null;
        Folder folder = (Folder) null;
        string albumId = context.Request.Form["LibraryId"];
        if (!string.IsNullOrEmpty(albumId))
        {
          album1 = this.Manager.GetAlbums().FirstOrDefault<Album>((Expression<Func<Album, bool>>) (a => a.Id == new Guid(albumId)));
          if (album1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              album1 = this.Manager.GetAlbums().FirstOrDefault<Album>();
          }
        }
        if (album1 == null && folder == null)
          throw new ApplicationException("You have to create album first");
        if (folder != null)
        {
          image.FolderId = new Guid?(folder.Id);
          Album album2 = this.Manager.GetAlbums().Where<Album>((Expression<Func<Album, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<Album>();
          if (album2 != null)
            ((IHasParent) image).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) album2;
        }
        else
          ((IHasParent) image).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) album1;
        image.Extension = this.UploadedFile.GetExtension();
        image.Title = (Lstring) this.UploadedFile.GetNameWithoutExtension();
        image.AlternativeText = (Lstring) context.Request.Form["AlternativeText"];
        image.UrlName = (Lstring) CommonMethods.TitleToUrl((string) image.Title);
        image.Description = (Lstring) string.Empty;
        image.Author = (Lstring) string.Empty;
        image.Status = ContentLifecycleStatus.Master;
        this.Content = (MediaContent) image;
      }
      else if (itemType == typeof (Video))
      {
        Video video = this.Manager.CreateVideo();
        VideoLibrary videoLibrary1 = (VideoLibrary) null;
        Folder folder = (Folder) null;
        string albumId = context.Request.Form["LibraryId"];
        if (!string.IsNullOrEmpty(albumId))
        {
          videoLibrary1 = this.Manager.GetVideoLibraries().FirstOrDefault<VideoLibrary>((Expression<Func<VideoLibrary, bool>>) (a => a.Id == new Guid(albumId)));
          if (videoLibrary1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              videoLibrary1 = this.Manager.GetVideoLibraries().FirstOrDefault<VideoLibrary>();
          }
        }
        if (videoLibrary1 == null && folder == null)
          throw new ApplicationException("You have to create library first");
        if (folder != null)
        {
          video.FolderId = new Guid?(folder.Id);
          VideoLibrary videoLibrary2 = this.Manager.GetVideoLibraries().Where<VideoLibrary>((Expression<Func<VideoLibrary, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<VideoLibrary>();
          if (videoLibrary2 != null)
            ((IHasParent) video).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) videoLibrary2;
        }
        else
          ((IHasParent) video).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) videoLibrary1;
        video.Title = (Lstring) this.UploadedFile.GetNameWithoutExtension();
        video.Extension = this.UploadedFile.GetExtension();
        video.UrlName = (Lstring) CommonMethods.TitleToUrl((string) video.Title);
        video.Description = (Lstring) string.Empty;
        video.Author = (Lstring) string.Empty;
        video.Status = ContentLifecycleStatus.Master;
        this.Content = (MediaContent) video;
      }
      else if (itemType == typeof (Document))
      {
        Document document = this.Manager.CreateDocument();
        DocumentLibrary documentLibrary1 = (DocumentLibrary) null;
        Folder folder = (Folder) null;
        string albumId = context.Request.Form["LibraryId"];
        if (!string.IsNullOrEmpty(albumId))
        {
          documentLibrary1 = this.Manager.GetDocumentLibraries().FirstOrDefault<DocumentLibrary>((Expression<Func<DocumentLibrary, bool>>) (a => a.Id == new Guid(albumId)));
          if (documentLibrary1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              documentLibrary1 = this.Manager.GetDocumentLibraries().FirstOrDefault<DocumentLibrary>();
          }
        }
        if (documentLibrary1 == null && folder == null)
          throw new ApplicationException("You have to create library first");
        if (folder != null)
        {
          document.FolderId = new Guid?(folder.Id);
          DocumentLibrary documentLibrary2 = this.Manager.GetDocumentLibraries().Where<DocumentLibrary>((Expression<Func<DocumentLibrary, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<DocumentLibrary>();
          if (documentLibrary2 != null)
            ((IHasParent) document).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) documentLibrary2;
        }
        else
          ((IHasParent) document).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) documentLibrary1;
        if (context.Request.ParamsGet("UseTitleAsMediaItemTitle") == "true")
        {
          string str = context.Request.ParamsGet("Title");
          document.Title = (Lstring) (string.IsNullOrWhiteSpace(str) ? this.UploadedFile.GetNameWithoutExtension() : str);
        }
        else
          document.Title = (Lstring) this.UploadedFile.GetNameWithoutExtension();
        document.Extension = this.UploadedFile.GetExtension();
        document.UrlName = (Lstring) CommonMethods.TitleToUrl((string) document.Title);
        document.Description = (Lstring) string.Empty;
        document.Author = (Lstring) string.Empty;
        document.Status = ContentLifecycleStatus.Master;
        document.FilePath = this.UploadedFile.GetName();
        this.Content = (MediaContent) document;
      }
      else
      {
        this.Content = this.Manager.CreateItem(itemType) as MediaContent;
        this.Content.Status = ContentLifecycleStatus.Master;
      }
      if (!"true".Equals(context.Request.Form["RecompileItemUrls"], StringComparison.InvariantCultureIgnoreCase))
        return;
      CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) this.Content, (IManager) this.Manager, new List<string>(), false);
    }

    /// <summary>Saves the data.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    internal virtual AsyncImageUploadHandler.UploadResponse SaveData(
      HttpContextBase context)
    {
      bool uploadAndReplace = context.Request.QueryString["uploadAndReplace"] != null && bool.Parse(context.Request.QueryString["uploadAndReplace"]);
      AsyncImageUploadHandler.UploadResponse uploadResponse = new AsyncImageUploadHandler.UploadResponse();
      try
      {
        MediaContent content = this.Content;
        this.Manager.RecompileItemUrls<MediaContent>(content);
        this.Manager.Upload(content, this.UploadedFile.InputStream, this.UploadedFile.GetExtension(), uploadAndReplace);
        uploadResponse.UploadResult = true;
        uploadResponse.ErrorMessage = (string) null;
        if (content is Video)
          this.Manager.GenerateVideoThumbnails(this.Manager.GetVideo(this.Content.Id));
        this.Manager.SaveChanges();
        Type type1 = this.Content.GetType();
        content.ApprovalWorkflowState = (Lstring) "Published";
        IDataItem contentItem;
        if (type1 == typeof (Image))
          contentItem = (IDataItem) this.Manager.Publish((Image) content);
        else if (type1 == typeof (Document))
          contentItem = (IDataItem) this.Manager.Publish((Document) content);
        else if (type1 == typeof (Video))
        {
          contentItem = (IDataItem) this.Manager.Publish((Video) content);
        }
        else
        {
          Type type2 = typeof (IContentLifecycleManager<>).MakeGenericType(type1);
          if (!((IEnumerable<Type>) this.Manager.GetType().GetInterfaces()).Contains<Type>(type2))
            throw new NotSupportedException();
          contentItem = (IDataItem) this.Manager.GetType().GetMethod("Publish", new Type[1]
          {
            type1
          }).Invoke((object) this.Manager, new object[1]
          {
            (object) content
          });
        }
        LibrariesDataProvider provider = this.Manager.Provider;
        content.CreateApprovalTrackingRecord(provider.ApplicationName, "Published", provider.GetNewGuid());
        this.Manager.SaveChanges();
        uploadResponse.ContentId = contentItem.Id.ToString();
        if (type1 == typeof (Image))
          uploadResponse.ContentItem = (MediaContentViewModel) new AlbumItemViewModel(contentItem as Image, (ContentDataProviderBase) this.Manager.Provider);
        else if (type1 == typeof (Document))
          uploadResponse.ContentItem = (MediaContentViewModel) new DocumentLibraryItemViewModel(contentItem as Document, (ContentDataProviderBase) this.Manager.Provider);
        else if (type1 == typeof (Video))
          uploadResponse.ContentItem = (MediaContentViewModel) new VideoLibraryItemViewModel(contentItem as Video, (ContentDataProviderBase) this.Manager.Provider);
        uploadResponse.ContentUrl = content.MediaUrl;
      }
      catch (Exception ex)
      {
        string str = string.Format("Cannot save the file: [{0}]", (object) ex.Message);
        uploadResponse.ContentId = (string) null;
        uploadResponse.UploadResult = false;
        uploadResponse.ErrorMessage = str;
      }
      return uploadResponse;
    }

    [DataContract]
    public class UploadResponse
    {
      [DataMember]
      public string ContentId { get; set; }

      [DataMember]
      public bool UploadResult { get; set; }

      [DataMember]
      public string ErrorMessage { get; set; }

      [DataMember]
      public string ContentUrl { get; set; }

      [DataMember]
      public MediaContentViewModel ContentItem { get; set; }
    }
  }
}
