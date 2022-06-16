// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Html5UploadHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  internal class Html5UploadHandler : IHttpHandler
  {
    private static readonly object _uploadLock = new object();

    public bool IsReusable => true;

    /// <summary>Gets or sets the provider.</summary>
    /// <value>The provider.</value>
    public LibrariesManager Manager { get; set; }

    /// <summary>
    /// Gets or sets the content of the uploaded file (binary data)
    /// </summary>
    /// <value>The content.</value>
    public MediaContent Content { get; set; }

    public HttpPostedFile UploadedFile { get; set; }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      context.Items[(object) "IsBackendRequest"] = (object) true;
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      IList<AsyncImageUploadHandler.UploadResponse> clrObject = this.SaveFiles(context);
      context.Response.Write(WcfHelper.SerializeToJson((object) clrObject, typeof (IList<AsyncImageUploadHandler.UploadResponse>)));
    }

    internal IList<AsyncImageUploadHandler.UploadResponse> SaveFiles(
      HttpContextBase context)
    {
      List<AsyncImageUploadHandler.UploadResponse> uploadResponseList = new List<AsyncImageUploadHandler.UploadResponse>();
      bool result1;
      bool.TryParse(context.Request.Form["UploadAndReplace"], out result1);
      int num = SystemManager.CurrentContext.AppSettings.Multilingual ? 1 : 0;
      string str1 = context.Request.Form["Culture"];
      string str2 = num == 0 || string.IsNullOrEmpty(str1) ? SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name : str1;
      SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(str2);
      for (int index = 0; index < context.Request.Files.Count; ++index)
      {
        HttpPostedFile file = context.ApplicationInstance.Context.Request.Files[index];
        if (file.ContentLength > 0)
        {
          Type contentType = TypeResolutionService.ResolveType(context.Request.Form["ContentType"]);
          string libraryId = context.Request.Form["LibraryId"];
          string contentId = context.Request.Form["ContentId"];
          string providerName = context.Request.Form["ProviderName"];
          string workflowOperation = context.Request.Form["Workflow"];
          bool result2;
          bool.TryParse(context.Request.Form["SkipWorkflow"], out result2);
          this.SaveSingleContentItem(file, file.FileName, providerName, contentType, libraryId, contentId, str2);
          uploadResponseList.Add(this.SaveData(workflowOperation, providerName, str2, result2, file.FileName, result1));
        }
        else
        {
          AsyncImageUploadHandler.UploadResponse uploadResponse = new AsyncImageUploadHandler.UploadResponse()
          {
            ContentId = (string) null,
            ErrorMessage = "No file uploaded",
            UploadResult = false
          };
          uploadResponseList.Add(uploadResponse);
        }
      }
      return (IList<AsyncImageUploadHandler.UploadResponse>) uploadResponseList;
    }

    private void SaveSingleContentItem(
      HttpPostedFile binaryContent,
      string filename,
      string providerName,
      Type contentType,
      string libraryId,
      string contentId,
      string culture)
    {
      this.UploadedFile = binaryContent;
      this.Manager = string.IsNullOrEmpty(providerName) ? ManagerBase.GetMappedManager(contentType) as LibrariesManager : ManagerBase.GetMappedManager(contentType, providerName) as LibrariesManager;
      if (this.Manager == null)
        throw new ApplicationException("Unable to get manager for type :" + contentType.FullName);
      if (contentType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
      {
        string albumId = libraryId;
        Telerik.Sitefinity.Libraries.Model.Image image;
        if (!string.IsNullOrEmpty(contentId))
        {
          image = this.Manager.GetImage(new Guid(contentId));
          albumId = image.Album.Id.ToString();
        }
        else
          image = this.Manager.CreateImage();
        Telerik.Sitefinity.Libraries.Model.Album album1 = (Telerik.Sitefinity.Libraries.Model.Album) null;
        Folder folder = (Folder) null;
        if (!string.IsNullOrEmpty(albumId))
        {
          album1 = this.Manager.GetAlbums().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (a => a.Id == new Guid(albumId)));
          if (album1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              album1 = this.Manager.GetAlbums().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>();
          }
        }
        if (album1 == null && folder == null)
          throw new ApplicationException("You have to create album first");
        if (folder != null)
        {
          image.FolderId = new Guid?(folder.Id);
          Telerik.Sitefinity.Libraries.Model.Album album2 = this.Manager.GetAlbums().Where<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>();
          if (album2 != null)
            ((IHasParent) image).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) album2;
        }
        else
          ((IHasParent) image).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) album1;
        if (string.IsNullOrEmpty((string) image.Title))
          image.Title = (Lstring) Path.GetFileNameWithoutExtension(filename);
        if (string.IsNullOrEmpty((string) image.AlternativeText))
          image.AlternativeText = image.Title;
        this.Content = (MediaContent) image;
      }
      else if (contentType == typeof (Telerik.Sitefinity.Libraries.Model.Video))
      {
        Telerik.Sitefinity.Libraries.Model.VideoLibrary videoLibrary1 = (Telerik.Sitefinity.Libraries.Model.VideoLibrary) null;
        Folder folder = (Folder) null;
        string albumId = libraryId;
        Telerik.Sitefinity.Libraries.Model.Video video;
        if (!string.IsNullOrEmpty(contentId))
        {
          video = this.Manager.GetVideo(new Guid(contentId));
          albumId = video.Library.Id.ToString();
        }
        else
          video = this.Manager.CreateVideo();
        if (!string.IsNullOrEmpty(albumId))
        {
          videoLibrary1 = this.Manager.GetVideoLibraries().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (a => a.Id == new Guid(albumId)));
          if (videoLibrary1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              videoLibrary1 = this.Manager.GetVideoLibraries().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
          }
        }
        if (videoLibrary1 == null && folder == null)
          throw new ApplicationException("You have to create library first");
        if (folder != null)
        {
          video.FolderId = new Guid?(folder.Id);
          Telerik.Sitefinity.Libraries.Model.VideoLibrary videoLibrary2 = this.Manager.GetVideoLibraries().Where<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
          if (videoLibrary2 != null)
            ((IHasParent) video).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) videoLibrary2;
        }
        else
          ((IHasParent) video).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) videoLibrary1;
        if (string.IsNullOrEmpty((string) video.Title))
          video.Title = (Lstring) Path.GetFileNameWithoutExtension(filename);
        this.Content = (MediaContent) video;
      }
      else if (contentType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
      {
        Telerik.Sitefinity.Libraries.Model.DocumentLibrary documentLibrary1 = (Telerik.Sitefinity.Libraries.Model.DocumentLibrary) null;
        Folder folder = (Folder) null;
        string albumId = libraryId;
        Telerik.Sitefinity.Libraries.Model.Document document;
        if (!string.IsNullOrEmpty(contentId))
        {
          document = this.Manager.GetDocument(new Guid(contentId));
          albumId = document.Library.Id.ToString();
        }
        else
          document = this.Manager.CreateDocument();
        if (!string.IsNullOrEmpty(albumId))
        {
          documentLibrary1 = this.Manager.GetDocumentLibraries().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (a => a.Id == new Guid(albumId)));
          if (documentLibrary1 == null)
          {
            folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(albumId)));
            if (folder == null)
              documentLibrary1 = this.Manager.GetDocumentLibraries().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
          }
        }
        if (documentLibrary1 == null && folder == null)
          throw new ApplicationException("You have to create library first");
        if (folder != null)
        {
          document.FolderId = new Guid?(folder.Id);
          Telerik.Sitefinity.Libraries.Model.DocumentLibrary documentLibrary2 = this.Manager.GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (a => a.Id == folder.RootId)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
          if (documentLibrary2 != null)
            ((IHasParent) document).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) documentLibrary2;
        }
        else
          ((IHasParent) document).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) documentLibrary1;
        if (string.IsNullOrEmpty((string) document.Title))
          document.Title = (Lstring) Path.GetFileNameWithoutExtension(filename);
        this.Content = (MediaContent) document;
      }
      else
        this.Content = this.Manager.CreateItem(contentType) as MediaContent;
      this.Content.Extension = Path.GetExtension(this.UploadedFile.FileName);
      this.Content.FilePath = this.UploadedFile.FileName;
    }

    /// <summary>Saves the data.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    private AsyncImageUploadHandler.UploadResponse SaveData(
      string workflowOperation,
      string providerName,
      string culture,
      bool skipWorkflow,
      string fileName,
      bool uploadAndReplace)
    {
      AsyncImageUploadHandler.UploadResponse uploadResponse = new AsyncImageUploadHandler.UploadResponse();
      try
      {
        MediaContent content1 = this.Content;
        if (this.Content.UrlName.Value.IsNullOrEmpty())
          this.Content.UrlName = (Lstring) CommonMethods.TitleToUrl((string) this.Content.Title);
        content1.GetFileLink(true).DefaultUrl = (string) null;
        if (!uploadAndReplace)
          content1.MediaFileUrlName = (Lstring) CommonMethods.TitleToUrl(Path.GetFileNameWithoutExtension(fileName));
        content1.ClearMediaFileUrls((IManager) this.Manager, excludeSharedTranslations: (!uploadAndReplace));
        this.Manager.Provider.FlushTransaction();
        this.Manager.RecompileItemUrls<MediaContent>(content1);
        this.Manager.SaveChanges();
        this.Manager.Upload(content1, this.UploadedFile.InputStream, this.Content.Extension, uploadAndReplace);
        uploadResponse.UploadResult = true;
        uploadResponse.ErrorMessage = (string) null;
        this.Manager.SaveChanges();
        if (content1 is Telerik.Sitefinity.Libraries.Model.Video)
        {
          this.Manager.GenerateVideoThumbnails(this.Manager.GetVideo(this.Content.Id));
          this.Manager.SaveChanges();
        }
        Type type = this.Content.GetType();
        MediaContent mediaContent;
        if (skipWorkflow)
        {
          content1.SetWorkflowStatus(this.Manager.Provider.ApplicationName, LifecycleExtensions.StatusPublished);
          this.Content.LastModifiedBy = SecurityManager.CurrentUserId;
          mediaContent = (MediaContent) this.Manager.Lifecycle.Publish((ILifecycleDataItem) this.Content);
          if (this.Content != null)
          {
            VersionManager manager = VersionManager.GetManager((string) null, this.Manager.Provider.TransactionName);
            manager.CreateVersion((object) mediaContent, mediaContent.OriginalContentId, true);
            manager.SaveChanges();
          }
          this.Manager.SaveChanges();
        }
        else if (content1.SupportsContentLifecycle && !string.IsNullOrEmpty(workflowOperation))
        {
          Telerik.Sitefinity.GenericContent.Model.Content content2 = this.Manager.CheckOut((Telerik.Sitefinity.GenericContent.Model.Content) this.Content);
          this.Manager.SaveChanges();
          MediaContent content3 = this.Content;
          Dictionary<string, string> contextBag = new Dictionary<string, string>();
          contextBag.Add("ContentType", content2.GetType().FullName);
          contextBag.Add("MasterId", content3.Id.ToString());
          if (!string.IsNullOrEmpty(culture))
            contextBag.Add("Language", culture);
          lock (Html5UploadHandler._uploadLock)
            WorkflowManager.MessageWorkflow(content2.Id, content3.GetType(), providerName, workflowOperation, false, contextBag);
          mediaContent = this.Content;
        }
        else
          mediaContent = this.Content;
        uploadResponse.ContentId = mediaContent.Id.ToString();
        if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
          uploadResponse.ContentItem = (MediaContentViewModel) new AlbumItemViewModel(mediaContent as Telerik.Sitefinity.Libraries.Model.Image, (ContentDataProviderBase) this.Manager.Provider);
        else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
          uploadResponse.ContentItem = (MediaContentViewModel) new DocumentLibraryItemViewModel(mediaContent as Telerik.Sitefinity.Libraries.Model.Document, (ContentDataProviderBase) this.Manager.Provider);
        else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
          uploadResponse.ContentItem = (MediaContentViewModel) new VideoLibraryItemViewModel(mediaContent as Telerik.Sitefinity.Libraries.Model.Video, (ContentDataProviderBase) this.Manager.Provider);
        uploadResponse.ContentUrl = mediaContent.MediaUrl;
      }
      catch (Exception ex)
      {
        this.CleanOnError(this.Content);
        string str = string.Format("Cannot save the file: {0}", (object) ex.Message);
        uploadResponse.ContentId = (string) null;
        uploadResponse.UploadResult = false;
        uploadResponse.ErrorMessage = str;
      }
      return uploadResponse;
    }

    private void CleanOnError(MediaContent media)
    {
      if (media == null)
        return;
      this.Manager.DeleteItem((object) media);
      this.Manager.SaveChanges();
    }
  }
}
