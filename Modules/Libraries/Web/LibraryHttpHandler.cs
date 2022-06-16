// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.LibraryHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web.Events;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  public class LibraryHttpHandler : IHttpHandler
  {
    private static readonly object syncLock = new object();
    private const string DefaultThumbnailImage = "Telerik.Sitefinity.Resources.Images.video-thumb.png";
    private const string DefaultThumbnailCacheKey = "defaultThumbnail";
    private Regex firstRangeRegex = new Regex("^.*[\\D]+0[\\D]+.*$", RegexOptions.Compiled);

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public virtual void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    /// <exception cref="T:System.Web.HttpException">403</exception>
    public virtual void ProcessRequest(HttpContextBase context)
    {
      try
      {
        RouteHelper.ApplyThreadCulturesForCurrentUser();
        RouteData routeData = context.Request.RequestContext.RouteData;
        int result = 0;
        int.TryParse((routeData.DataTokens["Version"] ?? (object) string.Empty).ToString(), out result);
        if (result > 0)
        {
          this.ProcessVersionRequest(context, result);
        }
        else
        {
          HttpRequestBase request = context.Request;
          HttpResponseBase response = context.Response;
          string key = LibraryHttpHandler.GetCacheKey(request.Url, context);
          ICacheManager cache = LibraryHttpHandler.Cache;
          object obj = cache[key];
          MediaContent content = (MediaContent) null;
          if (obj == null)
          {
            lock (LibraryHttpHandler.syncLock)
            {
              obj = cache[key];
              if (obj == null)
              {
                obj = (object) new Lazy<object>((Func<object>) (() => (object) this.GetOutputItem(request, response, cache, key, out content)), true);
                cache.Add(key, obj, CacheItemPriority.Low, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new AbsoluteTime(TimeSpan.FromSeconds(30.0)));
              }
            }
          }
          if (obj is Lazy<object> lazy)
            obj = lazy.Value;
          LibraryHttpHandler.OutputItem outputItem = obj as LibraryHttpHandler.OutputItem;
          string dataToken = (string) routeData.DataTokens["ContentUrl"];
          LibraryHttpHandler.LibrariesEventsFactory librariesEventsFactory = new LibraryHttpHandler.LibrariesEventsFactory();
          if (outputItem != null)
          {
            this.RaiseEvent((IMediaContentDownloadEvent) librariesEventsFactory.CreateMediaContentDownloadingEvent((LibraryHttpHandler.IOutputItem) outputItem, request, dataToken), true, context.Request);
            this.SendCachedResponse(request, response, outputItem);
            this.RaiseEvent((IMediaContentDownloadEvent) librariesEventsFactory.CreateMediaContentDownloadedEvent((LibraryHttpHandler.IOutputItem) outputItem, request, dataToken), false, context.Request);
          }
          else if (obj is LibraryHttpHandler.FileSystemItem)
          {
            this.RaiseEvent((IMediaContentDownloadEvent) new MediaContentDownloadingEvent()
            {
              Headers = request.Headers,
              Url = dataToken,
              UserId = ClaimsManager.GetCurrentUserId()
            }, true, context.Request);
            this.ProcessFileSystemPath(request, response);
            this.RaiseEvent((IMediaContentDownloadEvent) new MediaContentDownloadedEvent()
            {
              Headers = request.Headers,
              Url = dataToken,
              UserId = ClaimsManager.GetCurrentUserId()
            }, false, context.Request);
          }
          else if (obj is LibraryHttpHandler.RedirectOutputItem redirectOutputItem)
          {
            string redirectUrl = ((LibraryHttpHandler.RedirectOutputItem) obj).RedirectUrl;
            if (!ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(redirectUrl))
            {
              string cdnUrl = redirectOutputItem.CdnUrl;
              if (string.IsNullOrEmpty(cdnUrl) && !redirectUrl.Contains(cdnUrl))
                throw new HttpException(404, "File not found.");
            }
            response.RedirectPermanent(redirectUrl, true);
          }
          else
          {
            LibrariesManager manager;
            if (content == null)
              content = this.GetContentItem(request, out string _, out manager);
            else
              manager = this.GetManager((IDictionary<string, object>) routeData.DataTokens);
            if (!this.IsViewAllowed(content))
            {
              ProtectedRoute.HandleItemViewNotAllowed(context, Res.Get<LibrariesResources>().YouAreNotAllowedToViewThisLibraryItem);
            }
            else
            {
              this.RaiseEvent((IMediaContentDownloadEvent) librariesEventsFactory.CreateMediaContentDownloadingEvent(content, request, dataToken), true, context.Request);
              this.SendResponse(request, response, content, manager);
              this.RaiseEvent((IMediaContentDownloadEvent) librariesEventsFactory.CreateMediaContentDownloadedEvent(content, request, dataToken), false, context.Request);
            }
          }
        }
      }
      catch (UnauthorizedAccessException ex)
      {
        throw new HttpException(403, ex.Message);
      }
    }

    private void ProcessVersionRequest(HttpContextBase context, int version)
    {
      try
      {
        ServiceUtility.RequestBackendUserAuthentication();
      }
      catch (WebProtocolException ex)
      {
        throw new UnauthorizedAccessException(ex.Message, (Exception) ex);
      }
      HttpRequestBase request = context.Request;
      HttpResponseBase response = context.Response;
      RouteData routeData = request.RequestContext.RouteData;
      LibrariesManager manager1;
      MediaContent contentItem = this.GetContentItem(request, out string _, out manager1);
      if (contentItem == null)
        throw new HttpException(404, "File not found.");
      if (!this.IsViewAllowed(contentItem))
      {
        ProtectedRoute.HandleItemViewNotAllowed(context, Res.Get<LibrariesResources>().YouAreNotAllowedToViewThisLibraryItem);
      }
      else
      {
        if (routeData.DataTokens["Culture"] != null)
          LibraryHttpHandler.SetThreadCulture(AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.GetCultureInfo((string) routeData.DataTokens["Culture"])));
        if (contentItem != null)
        {
          VersionManager manager2 = VersionManager.GetManager();
          Guid itemId = !contentItem.OriginalContentId.IsEmpty() ? contentItem.OriginalContentId : contentItem.Id;
          Guid.NewGuid();
          try
          {
            manager2.GetSpecificVersion((object) contentItem, itemId, version);
          }
          catch (TargetInvocationException ex)
          {
            throw new HttpException(404, "Version does not exist.");
          }
        }
        int bufferSize = contentItem.ChunkSize;
        if (bufferSize == 0)
          bufferSize = Config.Get<LibrariesConfig>().SizeOfChunk;
        using (Stream stream = manager1.Download(contentItem))
        {
          this.SetHeaders(request, response, string.Empty, contentItem.ResolveMimeType(), (int) contentItem.TotalSize);
          this.WriteToOutput(response, stream, bufferSize);
        }
      }
    }

    private void RaiseEvent(
      IMediaContentDownloadEvent eventToRaise,
      bool throwExcecption,
      HttpRequestBase request)
    {
      string input = this.RetrieveHeader(request, "Range", string.Empty);
      if (!string.IsNullOrEmpty(input) && !this.firstRangeRegex.IsMatch(input))
        return;
      EventHub.Raise((IEvent) eventToRaise, throwExcecption);
    }

    internal static string GetCacheKey(Uri itemUri, HttpContextBase context = null)
    {
      string cacheKey = itemUri.AbsoluteUri.ToUpperInvariant();
      int num = cacheKey.IndexOf("?");
      if (num != -1 && context != null)
      {
        QueryStringBuilder queryStringBuilder = new QueryStringBuilder(cacheKey.Substring(num));
        queryStringBuilder.Remove(MediaContentExtensions.UrlVersionQueryParam);
        if (!context.User.Identity.IsAuthenticated || !SecurityManager.IsBackendUser())
        {
          queryStringBuilder.Remove("Status");
          context.Request.RequestContext.RouteData.DataTokens["MediaStatus"] = (object) ContentLifecycleStatus.Live;
        }
        cacheKey = cacheKey.Substring(0, num) + queryStringBuilder.ToString();
      }
      return cacheKey;
    }

    internal static ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.ContentOutput);

    private MediaContent GetContentItem(
      HttpRequestBase request,
      out string redirectUrl,
      out LibrariesManager manager)
    {
      int resolvedCultureId = 0;
      RouteValueDictionary dataTokens = request.RequestContext.RouteData.DataTokens;
      ContentLifecycleStatus contentLifecycleStatus = (ContentLifecycleStatus) dataTokens["MediaStatus"];
      bool published = contentLifecycleStatus == ContentLifecycleStatus.Live;
      manager = this.GetManager((IDictionary<string, object>) dataTokens);
      MediaFileLink mediaFileLink = (MediaFileLink) null;
      string url = (string) dataTokens["ContentUrl"];
      MediaFileLink fileFromUrl;
      if (dataTokens["Culture"] != null)
      {
        resolvedCultureId = AppSettings.CurrentSettings.GetCultureLcid(new CultureInfo((string) dataTokens["Culture"]));
        fileFromUrl = manager.GetFileFromUrl(url, published, resolvedCultureId, out redirectUrl);
      }
      else
        fileFromUrl = manager.GetFileFromUrl(url, published, out redirectUrl, out resolvedCultureId);
      if (fileFromUrl == null)
        return (MediaContent) null;
      MediaContent mediaContent = fileFromUrl.MediaContent;
      if (resolvedCultureId != 0)
        LibraryHttpHandler.SetThreadCulture(resolvedCultureId);
      if (contentLifecycleStatus == ContentLifecycleStatus.Temp)
      {
        ILifecycleDecorator lifecycle = manager.Lifecycle;
        if (lifecycle != null)
        {
          ILifecycleDataItem temp = lifecycle.GetTemp((ILifecycleDataItem) mediaContent);
          if (temp != null)
          {
            if (temp is MediaContent contentItem)
              mediaFileLink = contentItem.GetFileLink(cultureId: new int?(resolvedCultureId));
            return contentItem;
          }
        }
      }
      return mediaContent;
    }

    private static void SetThreadCulture(int cultureId) => SystemManager.CurrentContext.Culture = AppSettings.CurrentSettings.GetCultureByLcid(cultureId);

    private LibrariesManager GetManager(IDictionary<string, object> dataTokens) => LibrariesManager.GetManager((string) dataTokens["ProviderName"]);

    /// <summary>
    /// Checks that the object view permission is granted and throws and exception if not. Otherwise caches the object and sends a response.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="response">The HTTP response.</param>
    /// <param name="cache">Instance of ICacheManager.</param>
    /// <param name="key">The key for caching.</param>
    protected virtual LibraryHttpHandler.IOutputItem GetOutputItem(
      System.Web.HttpRequest request,
      System.Web.HttpResponse response,
      ICacheManager cache,
      string key,
      out MediaContent content)
    {
      return this.GetOutputItem((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response), cache, key, out content);
    }

    /// <summary>
    /// Checks that the object view permission is granted and throws and exception if not. Otherwise caches the object and sends a response.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="response">The HTTP response.</param>
    /// <param name="cache">Instance of ICacheManager.</param>
    /// <param name="key">The key for caching.</param>
    protected virtual LibraryHttpHandler.IOutputItem GetOutputItem(
      HttpRequestBase request,
      HttpResponseBase response,
      ICacheManager cache,
      string key,
      out MediaContent content)
    {
      string redirectUrl;
      LibrariesManager manager;
      content = this.GetContentItem(request, out redirectUrl, out manager);
      if (!Config.Get<LibrariesConfig>().Images.AllowUnsignedDynamicResizing && this.GetSizeParameter(request) > 0)
        return (LibraryHttpHandler.IOutputItem) null;
      if (content == null)
      {
        if (Config.Get<LibrariesConfig>().EnableFileSystemFallback)
          return (LibraryHttpHandler.IOutputItem) new LibraryHttpHandler.FileSystemItem();
        cache.Remove(key);
        throw new HttpException(404, "File not found.");
      }
      if (!string.IsNullOrEmpty(redirectUrl))
      {
        string cdn = string.Empty;
        manager.Provider.GetBlobStorageManager(content).Provider.TryGetCdnUrl(out cdn);
        LibraryHttpHandler.RedirectOutputItem outputItem = new LibraryHttpHandler.RedirectOutputItem(redirectUrl, cdn);
        cache.Remove(key);
        cache.Add(key, (object) outputItem, CacheItemPriority.Low, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency((IDataItem) content), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(20.0)));
        return (LibraryHttpHandler.IOutputItem) outputItem;
      }
      bool dataToken = (bool) request.RequestContext.RouteData.DataTokens["ShowThumbnail"];
      OutputCacheElement cacheSettings = Config.Get<SystemConfig>().CacheSettings;
      OutputCacheProfileElement cacheProfile = content.GetCacheProfile(cacheSettings);
      if (cacheSettings.EnableOutputCache && cacheProfile.Enabled && (dataToken || content.TotalSize <= (long) (cacheProfile.MaxSize * 1024)) && string.IsNullOrEmpty(content.Parent.DownloadSecurityProviderName) && !this.isRangeRequest(request))
      {
        LibraryHttpHandler.OutputItem outputItem = new LibraryHttpHandler.OutputItem();
        this.PopulateOutputItem(request, content, manager, outputItem);
        outputItem.ClientCacheControl = cacheProfile.ToClientCacheControl(!cacheSettings.EnableClientCache);
        this.SetPermissionSpecificClientCache(outputItem);
        ICacheItemExpiration cacheItemExpiration = !cacheProfile.SlidingExpiration ? (ICacheItemExpiration) new AbsoluteTime(DateTime.Now.AddSeconds((double) cacheProfile.Duration)) : (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) cacheProfile.Duration));
        cache.Remove(key);
        cache.Add(key, (object) outputItem, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency((IDataItem) content), cacheItemExpiration);
        return (LibraryHttpHandler.IOutputItem) outputItem;
      }
      cache.Remove(key);
      cache.Add(key, new object(), CacheItemPriority.Low, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency((IDataItem) content));
      return (LibraryHttpHandler.IOutputItem) null;
    }

    private bool isRangeRequest(HttpRequestBase request) => !this.RetrieveHeader(request, "Range", string.Empty).IsNullOrEmpty();

    protected virtual void SetClientCaching(
      System.Web.HttpResponse response,
      DateTime dateModified,
      ClientCacheControl cacheControl)
    {
      this.SetClientCaching((HttpResponseBase) new HttpResponseWrapper(response), dateModified, cacheControl);
    }

    protected virtual void SetClientCaching(
      HttpResponseBase response,
      DateTime dateModified,
      ClientCacheControl cacheControl)
    {
      HttpCachePolicyBase cache = response.Cache;
      cache.SetLastModified(dateModified);
      if (cacheControl.IsDefault)
        return;
      cache.SetCacheability(cacheControl.Cacheablity);
      if (cacheControl.Cacheablity == HttpCacheability.NoCache)
        return;
      cache.SetMaxAge(cacheControl.MaxAge);
      if (cacheControl.ProxyMaxAge != new TimeSpan())
        cache.SetProxyMaxAge(cacheControl.ProxyMaxAge);
      cache.SetExpires(DateTime.Now + cacheControl.MaxAge);
    }

    private void SetPermissionSpecificClientCache(LibraryHttpHandler.OutputItem item)
    {
      string[] supportedPermissionSets = item.Secured.SupportedPermissionSets;
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      string key;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(item.Type))
        key = "ViewDocument";
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(item.Type))
      {
        key = "ViewImage";
      }
      else
      {
        if (!typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(item.Type))
          throw new ArgumentException("Unsupported item type: " + item.Type.FullName + ".");
        key = "ViewVideo";
      }
      Guid[] principals = new Guid[1]
      {
        SecurityManager.EveryoneRole.Id
      };
      foreach (string str in supportedPermissionSets)
      {
        int actions = securityConfig.Permissions[str].Actions[key].Value;
        if (!item.Secured.IsGranted(str, principals, actions))
        {
          item.ClientCacheControl = new ClientCacheControl(HttpCacheability.NoCache);
          break;
        }
      }
    }

    /// <summary>
    /// Populates the output item for caching with data from the content item.
    /// This code was extracted and united from SendResponse and SendResponseAndSetCaching in order to separate population of the cached object from the process of the response.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="content">The content item to retrieve data from.</param>
    /// <param name="manager">Instance of LibrariesManager.</param>
    /// <param name="outputItem">The output item to populate.</param>
    protected virtual void PopulateOutputItem(
      System.Web.HttpRequest request,
      MediaContent content,
      LibrariesManager manager,
      LibraryHttpHandler.OutputItem outputItem)
    {
      this.PopulateOutputItem((HttpRequestBase) new HttpRequestWrapper(request), content, manager, outputItem);
    }

    /// <summary>
    /// Populates the output item for caching with data from the content item.
    /// This code was extracted and united from SendResponse and SendResponseAndSetCaching in order to separate population of the cached object from the process of the response.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="content">The content item to retrieve data from.</param>
    /// <param name="manager">Instance of LibrariesManager.</param>
    /// <param name="outputItem">The output item to populate.</param>
    protected virtual void PopulateOutputItem(
      HttpRequestBase request,
      MediaContent content,
      LibrariesManager manager,
      LibraryHttpHandler.OutputItem outputItem)
    {
      this.SetResponseOrCacheData(request, content, manager, (HttpResponseBase) null, outputItem);
      ISecuredObject original = (ISecuredObject) content;
      if (outputItem == null || original == null)
        return;
      outputItem.Secured = new SecuredProxy(original);
    }

    /// <summary>
    /// Populates the output item for caching with data from the content item.
    /// This code was extracted and united from SendResponse and SendResponseAndSetCaching in order to separate population of the cached object from the process of the response.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="content">The content item to retrieve data from.</param>
    /// <param name="manager">Instance of LibrariesManager.</param>
    /// <param name="outputItem">The output item to populate.</param>
    protected virtual void PopulateOutputItem(
      HttpRequestBase request,
      MediaContent content,
      CultureInfo culture,
      LibrariesManager manager,
      LibraryHttpHandler.OutputItem outputItem)
    {
      this.SetResponseOrCacheData(request, content, manager, (HttpResponseBase) null, outputItem);
      ISecuredObject original = (ISecuredObject) content;
      if (outputItem == null || original == null)
        return;
      outputItem.Secured = new SecuredProxy(original);
    }

    protected virtual void SendResponse(
      System.Web.HttpRequest request,
      System.Web.HttpResponse response,
      MediaContent content,
      LibrariesManager manager)
    {
      this.SendResponse((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response), content, manager);
    }

    protected virtual void SendResponse(
      HttpRequestBase request,
      HttpResponseBase response,
      MediaContent content,
      LibrariesManager manager)
    {
      if (!this.IsModifiedSince(request, response, content.LastModified))
        return;
      ClientCacheControl clientCacheControl = content.GetClientCacheControl();
      this.SetClientCaching(response, content.LastModified, clientCacheControl);
      this.SetResponseOrCacheData(request, content, manager, response, (LibraryHttpHandler.OutputItem) null);
    }

    protected virtual void SetResponseOrCacheData(
      System.Web.HttpRequest request,
      MediaContent content,
      LibrariesManager manager,
      System.Web.HttpResponse response,
      LibraryHttpHandler.OutputItem outputItem)
    {
      this.SetResponseOrCacheData((HttpRequestBase) new HttpRequestWrapper(request), content, manager, (HttpResponseBase) new HttpResponseWrapper(response), outputItem);
    }

    protected virtual void SetResponseOrCacheData(
      HttpRequestBase request,
      MediaContent content,
      LibrariesManager manager,
      HttpResponseBase response,
      LibraryHttpHandler.OutputItem outputItem,
      int? cultureId = null)
    {
      if (!cultureId.HasValue)
        cultureId = new int?(AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture));
      LibraryHttpHandler.SetThreadCulture(cultureId.Value);
      string imageMethod = request.QueryString["method"];
      MediaFileLink fileLink = content.GetFileLink(cultureId: new int?(cultureId.Value));
      MediaContent mediaContent = fileLink.MediaContent;
      string fileName = (string) (((IEnumerable<CultureInfo>) mediaContent.MediaFileUrlName.GetAvailableLanguages()).Any<CultureInfo>() ? mediaContent.MediaFileUrlName : mediaContent.UrlName) + fileLink.Extension;
      RouteValueDictionary dataTokens = request.RequestContext.RouteData.DataTokens;
      string providerName = DataEventFactory.GetProviderName(content.Provider);
      if (!mediaContent.IsVectorGraphics() && (bool) dataTokens["ShowThumbnail"])
      {
        string querySize = request.QueryString["size"];
        Thumbnail content1 = (Thumbnail) null;
        string thumbnailName = (string) dataTokens["ThumbnailName"];
        if (!string.IsNullOrEmpty(thumbnailName))
          content1 = content.GetThumbnails().Where<Thumbnail>((Func<Thumbnail, bool>) (tmb => tmb.Name == thumbnailName)).FirstOrDefault<Thumbnail>();
        else if (!string.IsNullOrEmpty(querySize))
          content1 = content.GetThumbnails().Where<Thumbnail>((Func<Thumbnail, bool>) (tmb => tmb.Name == querySize)).FirstOrDefault<Thumbnail>();
        if (content1 == null)
          content1 = LazyThumbnailGenerator.Instance.CreateThumbnail(content, thumbnailName);
        if (content1 == null)
          content1 = content.GetThumbnails().FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Type == ThumbnailTypes.System));
        if (content1 == null)
          content1 = content.GetThumbnails().FirstOrDefault<Thumbnail>();
        if (content1 == null)
          content1 = this.GetDefaultVideoThumbnail(content);
        if (content1 == null)
          content1 = this.GetDefaultImageThumbnail(content);
        if (content1 == null)
          return;
        if (content1.FileId != Guid.Empty)
        {
          long streamSize;
          int bufferSize;
          using (Stream mediaStream = this.GetMediaStream(manager, (IBlobContent) content1, 0, out streamSize, out bufferSize))
          {
            if (outputItem != null)
            {
              outputItem.Id = content.Id;
              outputItem.Provider = providerName;
              outputItem.BlobStorageProvider = content.BlobStorageProvider;
              outputItem.LibraryId = content.Parent.Id;
              outputItem.MimeType = content1.ResolveMimeType();
              outputItem.Type = content.GetType();
              outputItem.FileId = content1.FileId;
              outputItem.Title = (string) content.Title;
              outputItem.FileName = fileName;
              outputItem.LastModified = content.LastModified;
              outputItem.Data = this.GetContentData(mediaStream, streamSize, bufferSize);
            }
            else
            {
              this.SetHeaders(request, response, fileName, content1.ResolveMimeType(), (int) streamSize);
              this.WriteToOutput(response, mediaStream, bufferSize);
            }
          }
        }
        else if (outputItem != null)
        {
          outputItem.Id = content.Id;
          outputItem.Provider = providerName;
          outputItem.BlobStorageProvider = content.BlobStorageProvider;
          outputItem.LibraryId = content.Parent.Id;
          outputItem.MimeType = content1.ResolveMimeType();
          outputItem.Type = content.GetType();
          outputItem.Title = (string) content.Title;
          outputItem.FileId = content1.FileId;
          outputItem.FileName = fileName;
          outputItem.LastModified = content.LastModified;
          outputItem.Data = content1.Data;
        }
        else
        {
          this.SetHeaders(request, response, fileName, content1.ResolveMimeType(), content1.Data.Length);
          response.BinaryWrite(content1.Data);
        }
      }
      else if (!imageMethod.IsNullOrEmpty() && !mediaContent.IsVectorGraphics())
      {
        Thumbnail thumbnail = LazyThumbnailGenerator.Instance.CreateThumbnail(content, (string) null, imageMethod, request.QueryString);
        if (thumbnail == null)
          return;
        if (thumbnail.FileId != Guid.Empty)
        {
          long streamSize;
          int bufferSize;
          using (Stream mediaStream = this.GetMediaStream(manager, (IBlobContent) thumbnail, 0, out streamSize, out bufferSize))
          {
            if (outputItem != null)
            {
              outputItem.Id = content.Id;
              outputItem.Provider = providerName;
              outputItem.BlobStorageProvider = content.BlobStorageProvider;
              outputItem.LibraryId = content.Parent.Id;
              outputItem.MimeType = thumbnail.ResolveMimeType();
              outputItem.Type = content.GetType();
              outputItem.Title = (string) content.Title;
              outputItem.FileId = thumbnail.FileId;
              outputItem.FileName = fileName;
              outputItem.LastModified = content.LastModified;
              outputItem.Data = this.GetContentData(mediaStream, streamSize, bufferSize);
            }
            else
            {
              this.SetHeaders(request, response, fileName, thumbnail.ResolveMimeType(), (int) streamSize);
              this.WriteToOutput(response, mediaStream, bufferSize);
            }
          }
        }
        else if (outputItem != null)
        {
          outputItem.Id = content.Id;
          outputItem.Provider = providerName;
          outputItem.BlobStorageProvider = content.BlobStorageProvider;
          outputItem.LibraryId = content.Parent.Id;
          outputItem.MimeType = thumbnail.ResolveMimeType();
          outputItem.Type = content.GetType();
          outputItem.Title = (string) content.Title;
          outputItem.FileName = fileName;
          outputItem.FileId = thumbnail.FileId;
          outputItem.LastModified = content.LastModified;
          outputItem.Data = thumbnail.Data;
        }
        else
        {
          this.SetHeaders(request, response, fileName, thumbnail.ResolveMimeType(), thumbnail.Data.Length);
          response.BinaryWrite(thumbnail.Data);
        }
      }
      else
      {
        int sizeParameter = this.GetSizeParameter(request);
        long streamSize;
        int bufferSize;
        using (Stream mediaStream = this.GetMediaStream(manager, content, sizeParameter, out streamSize, out bufferSize))
        {
          if (outputItem != null)
          {
            outputItem.Id = content.Id;
            outputItem.Provider = providerName;
            outputItem.BlobStorageProvider = content.BlobStorageProvider;
            outputItem.LibraryId = content.Parent.Id;
            outputItem.MimeType = content.ResolveMimeType();
            outputItem.Type = content.GetType();
            outputItem.Title = (string) content.Title;
            outputItem.FileName = fileName;
            outputItem.FileId = content.FileId;
            outputItem.LastModified = content.LastModified;
            outputItem.Data = this.GetContentData(mediaStream, streamSize, bufferSize);
          }
          else
          {
            this.SetHeaders(request, response, fileName, content.ResolveMimeType(), (int) streamSize);
            this.WriteToOutput(response, mediaStream, bufferSize);
          }
        }
      }
    }

    protected virtual Thumbnail GetDefaultVideoThumbnail(MediaContent item)
    {
      if (!(item is Telerik.Sitefinity.Libraries.Model.Video))
        return (Thumbnail) null;
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.ContentOutput);
      object obj = cacheManager["defaultThumbnail"];
      Thumbnail defaultVideoThumbnail;
      if (obj == null)
      {
        defaultVideoThumbnail = new Thumbnail();
        using (Stream manifestResourceStream = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetManifestResourceStream("Telerik.Sitefinity.Resources.Images.video-thumb.png"))
        {
          int length = (int) manifestResourceStream.Length;
          byte[] buffer = new byte[length];
          manifestResourceStream.Read(buffer, 0, length);
          defaultVideoThumbnail.Data = buffer;
        }
        cacheManager.Add("defaultThumbnail", (object) defaultVideoThumbnail);
      }
      else
        defaultVideoThumbnail = (Thumbnail) obj;
      return defaultVideoThumbnail;
    }

    /// <summary>
    /// In case you are retreiving the thumbnail for ML item in MonoLingual site, we want to still retreive a thumbnail
    /// </summary>
    /// <param name="item">The media content item</param>
    /// <returns>The default thumbnail, if any</returns>
    protected virtual Thumbnail GetDefaultImageThumbnail(MediaContent item) => !(item is Telerik.Sitefinity.Libraries.Model.Image) ? (Thumbnail) null : item.Thumbnails.FirstOrDefault<Thumbnail>();

    protected virtual byte[] GetContentData(Stream stream, long streamSize, int bufferSize)
    {
      byte[] destinationArray = new byte[streamSize];
      byte[] numArray = new byte[bufferSize];
      int destinationIndex = 0;
      int length;
      do
      {
        length = stream.Read(numArray, 0, numArray.Length);
        if (length > 0)
          Array.Copy((Array) numArray, 0, (Array) destinationArray, destinationIndex, length);
        destinationIndex += length;
      }
      while (length > 0);
      return destinationArray;
    }

    protected virtual byte[] GetContentData(Stream stream, int bufferSize) => this.GetContentData(stream, stream.Length, bufferSize);

    /// <summary>
    /// Catches and ignores all 'The remote host closed the connection' errors; rethrows the rest.
    /// </summary>
    protected static void IgnoringClosedConnection(Action action)
    {
      try
      {
        action();
      }
      catch (HttpException ex)
      {
        switch (ex.ErrorCode)
        {
          case -2147024832:
            break;
          case -2147024775:
            break;
          case -2147023901:
            break;
          case -2147023667:
            break;
          default:
            Log.Write((object) ex, ConfigurationPolicy.Trace);
            break;
        }
      }
    }

    protected virtual void WriteToOutput(System.Web.HttpResponse response, Stream stream, int bufferSize) => this.WriteToOutput((HttpResponseBase) new HttpResponseWrapper(response), stream, bufferSize);

    protected virtual void WriteToOutput(HttpResponseBase response, Stream stream, int bufferSize)
    {
      if (stream.CanSeek && Config.Get<LibrariesConfig>().ByteRangeSettings.Enabled)
      {
        response.AddHeader("Accept-Ranges", "bytes");
        long[][] ranges;
        if (this.TryGetHeaderRanges(HttpContext.Current.Request, stream, out ranges))
        {
          long startRange = 0;
          long endRange = stream.Length - 1L;
          if (ranges.Length == 1)
          {
            startRange = ranges[0][0];
            endRange = ranges[0][1];
          }
          this.ValidateRange(startRange, endRange, stream.Length);
          response.StatusCode = 206;
          response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", (object) startRange.ToString(), (object) endRange.ToString(), (object) stream.Length.ToString()));
          response.Headers["Content-Length"] = (endRange - startRange + 1L).ToString();
          this.WritePartialResponse(response, stream, bufferSize, startRange, endRange);
          return;
        }
      }
      this.WriteEntireResponse(response, stream, bufferSize);
    }

    protected virtual void WritePartialResponse(
      System.Web.HttpResponse response,
      Stream stream,
      int bufferSize,
      long startRange,
      long endRange)
    {
      this.WritePartialResponse((HttpResponseBase) new HttpResponseWrapper(response), stream, bufferSize, startRange, endRange);
    }

    protected virtual void WritePartialResponse(
      HttpResponseBase response,
      Stream stream,
      int bufferSize,
      long startRange,
      long endRange)
    {
      byte[] buffer = new byte[bufferSize];
      int bytesToReadRemaining = Convert.ToInt32(endRange - startRange) + 1;
      stream.Seek(startRange, SeekOrigin.Begin);
      LibraryHttpHandler.IgnoringClosedConnection((Action) (() =>
      {
        while (bytesToReadRemaining > 0)
        {
          int count = stream.Read(buffer, 0, bufferSize < bytesToReadRemaining ? bufferSize : bytesToReadRemaining);
          if (count == 0)
          {
            response.End();
            break;
          }
          response.OutputStream.Write(buffer, 0, count);
          bytesToReadRemaining -= count;
          response.Flush();
        }
      }));
    }

    protected virtual void WriteEntireResponse(
      System.Web.HttpResponse response,
      Stream stream,
      int bufferSize)
    {
      this.WriteEntireResponse((HttpResponseBase) new HttpResponseWrapper(response), stream, bufferSize);
    }

    protected virtual void WriteEntireResponse(
      HttpResponseBase response,
      Stream stream,
      int bufferSize)
    {
      byte[] buffer = new byte[bufferSize];
      int bytesRead;
      LibraryHttpHandler.IgnoringClosedConnection((Action) (() =>
      {
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
          response.OutputStream.Write(buffer, 0, bytesRead);
      }));
    }

    protected virtual bool TryGetHeaderRanges(
      System.Web.HttpRequest request,
      Stream stream,
      out long[][] ranges)
    {
      return this.TryGetHeaderRanges((HttpRequestBase) new HttpRequestWrapper(request), stream, out ranges);
    }

    protected virtual bool TryGetHeaderRanges(
      HttpRequestBase request,
      Stream stream,
      out long[][] ranges)
    {
      bool headerRanges = false;
      ranges = (long[][]) null;
      string str = this.RetrieveHeader(request, "Range", string.Empty);
      if (!str.IsNullOrEmpty())
      {
        string[] strArray1 = str.Replace("bytes=", string.Empty).Split(",".ToCharArray());
        headerRanges = true;
        ranges = new long[strArray1.Length][];
        int index1 = 0;
        int index2 = 1;
        for (int index3 = 0; index3 < strArray1.Length; ++index3)
        {
          ranges[index3] = new long[2];
          string[] strArray2 = strArray1[index3].Split("-".ToCharArray());
          ranges[index3][1] = !string.IsNullOrEmpty(strArray2[index2]) ? long.Parse(strArray2[index2]) : stream.Length - 1L;
          if (string.IsNullOrEmpty(strArray2[index1]))
          {
            ranges[index3][0] = stream.Length - 1L - ranges[index3][1];
            ranges[index3][1] = stream.Length - 1L;
          }
          else
            ranges[index3][0] = long.Parse(strArray2[index1]);
        }
      }
      return headerRanges;
    }

    protected virtual void SendCachedResponse(
      System.Web.HttpRequest request,
      System.Web.HttpResponse response,
      LibraryHttpHandler.OutputItem item)
    {
      this.SendCachedResponse((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response), item);
    }

    protected virtual void SendCachedResponse(
      HttpRequestBase request,
      HttpResponseBase response,
      LibraryHttpHandler.OutputItem item)
    {
      if (!this.IsViewAllowed((ISecuredObject) item.Secured, item.Type))
      {
        ProtectedRoute.HandleItemViewNotAllowed(request.RequestContext.HttpContext, Res.Get<LibrariesResources>().YouAreNotAllowedToViewThisLibraryItem);
      }
      else
      {
        response.Clear();
        if (!this.IsModifiedSince(request, response, item.LastModified) || item.Data == null)
          return;
        this.SetHeaders(request, response, item.FileName, item.MimeType, item.Data.Length);
        this.SetClientCaching(response, item.LastModified, item.ClientCacheControl);
        LibraryHttpHandler.IgnoringClosedConnection((Action) (() => response.OutputStream.Write(item.Data, 0, item.Data.Length)));
      }
    }

    protected virtual void SetHeaders(
      System.Web.HttpRequest request,
      System.Web.HttpResponse response,
      string fileName,
      string mimeType,
      int contentLength)
    {
      this.SetHeaders((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response), fileName, mimeType, contentLength);
    }

    protected virtual void SetHeaders(
      HttpRequestBase request,
      HttpResponseBase response,
      string fileName,
      string mimeType,
      int contentLength)
    {
      response.StatusCode = 200;
      response.StatusDescription = "OK";
      if ("true".Equals(request.QueryString["download"], StringComparison.OrdinalIgnoreCase))
        response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
      else
        response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
      response.ContentType = mimeType;
      response.AddHeader("Content-Length", contentLength.ToString());
      response.Buffer = false;
    }

    private void ValidateRange(long startRange, long endRange, long itemLength)
    {
      if ((((startRange < 0L ? 1 : (startRange >= itemLength ? 1 : 0)) | (endRange < 0L ? (true ? 1 : 0) : (endRange >= itemLength ? 1 : 0))) != 0 ? 1 : (startRange > endRange ? 1 : 0)) != 0)
        throw new HttpException(416, "Range Not Satisfiable.");
    }

    private string RetrieveHeader(System.Web.HttpRequest request, string headerName, string defaultValue) => this.RetrieveHeader((HttpRequestBase) new HttpRequestWrapper(request), headerName, defaultValue);

    private string RetrieveHeader(HttpRequestBase request, string headerName, string defaultValue) => !string.IsNullOrEmpty(request.Headers[headerName]) ? request.Headers[headerName].Replace("\"", string.Empty) : defaultValue;

    protected virtual bool IsModifiedSince(
      System.Web.HttpRequest request,
      System.Web.HttpResponse response,
      DateTime lastModified)
    {
      return this.IsModifiedSince((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response), lastModified);
    }

    protected virtual bool IsModifiedSince(
      HttpRequestBase request,
      HttpResponseBase response,
      DateTime lastModified)
    {
      string s = request.Headers["If-Modified-Since"];
      DateTime result;
      if (!string.IsNullOrEmpty(s))
      {
        int length = s.IndexOf(";");
        if (length > 0)
          s = s.Substring(0, length);
        if (DateTime.TryParse(s, out result))
          result = result.ToUniversalTime();
      }
      else
        result = DateTime.MaxValue;
      if (!(lastModified != DateTime.MinValue) || !(result != DateTime.MaxValue) || !(lastModified.AddSeconds(-1.0) <= result))
        return true;
      response.StatusCode = 304;
      response.StatusDescription = "Not Modified";
      return false;
    }

    protected virtual Stream GetMediaStream(
      LibrariesManager manager,
      MediaContent content,
      int size,
      out long streamSize,
      out int bufferSize)
    {
      return this.GetMediaStream(manager, (IBlobContent) content, size, out streamSize, out bufferSize);
    }

    protected virtual Stream GetMediaStream(
      LibrariesManager manager,
      IBlobContent content,
      int size,
      out long streamSize,
      out int bufferSize)
    {
      if (content is Telerik.Sitefinity.Libraries.Model.Image && size > 0 && Config.Get<LibrariesConfig>().Images.AllowUnsignedDynamicResizing)
      {
        Telerik.Sitefinity.Libraries.Model.Image content1 = content as Telerik.Sitefinity.Libraries.Model.Image;
        System.Drawing.Image image;
        using (Stream stream = manager.Download((MediaContent) content1))
        {
          byte[] buffer = new byte[content1.TotalSize];
          stream.Read(buffer, 0, (int) content1.TotalSize);
          using (MemoryStream memoryStream = new MemoryStream(buffer))
            image = System.Drawing.Image.FromStream((Stream) memoryStream);
        }
        using (image)
        {
          System.Drawing.Image thumbnail;
          if (ImagesHelper.TryResizeImage(image, size, false, false, out thumbnail))
          {
            MemoryStream mediaStream = new MemoryStream();
            using (thumbnail)
              ImagesHelper.SaveImageToStream(thumbnail, (Stream) mediaStream, content.ResolveMimeType());
            mediaStream.Position = 0L;
            bufferSize = 1024;
            streamSize = mediaStream.Length;
            return (Stream) mediaStream;
          }
        }
      }
      bufferSize = 0;
      if (content is IChunksBlobContent)
        bufferSize = ((IChunksBlobContent) content).ChunkSize;
      if (bufferSize == 0)
        bufferSize = Config.Get<LibrariesConfig>().SizeOfChunk;
      streamSize = content.TotalSize;
      return manager.Download(content);
    }

    protected virtual bool ProcessFileSystemPath(System.Web.HttpRequest request, System.Web.HttpResponse response) => this.ProcessFileSystemPath((HttpRequestBase) new HttpRequestWrapper(request), (HttpResponseBase) new HttpResponseWrapper(response));

    protected virtual bool ProcessFileSystemPath(HttpRequestBase request, HttpResponseBase response)
    {
      string physicalPath = request.PhysicalPath;
      FileInfo fileInfo = new FileInfo(physicalPath);
      if (!fileInfo.Exists)
        throw new HttpException(404, "File not found.");
      DateTime dateTime1;
      ref DateTime local = ref dateTime1;
      int year = fileInfo.LastWriteTime.Year;
      DateTime dateTime2 = fileInfo.LastWriteTime;
      int month = dateTime2.Month;
      dateTime2 = fileInfo.LastWriteTime;
      int day = dateTime2.Day;
      dateTime2 = fileInfo.LastWriteTime;
      int hour = dateTime2.Hour;
      dateTime2 = fileInfo.LastWriteTime;
      int minute = dateTime2.Minute;
      dateTime2 = fileInfo.LastWriteTime;
      int second = dateTime2.Second;
      local = new DateTime(year, month, day, hour, minute, second, 0);
      DateTime now = DateTime.Now;
      if (dateTime1 > now)
        dateTime1 = new DateTime(now.Ticks - now.Ticks % 10000000L);
      string etag = LibraryHttpHandler.GenerateETag(dateTime1, now);
      try
      {
        response.TransmitFile(physicalPath, 0L, fileInfo.Length);
        response.ContentType = MimeMapping.GetMimeMapping(this.GetPathExtension(physicalPath));
        response.AppendHeader("Accept-Ranges", "bytes");
        response.AddFileDependency(physicalPath);
        HttpCachePolicyBase cache = response.Cache;
        dateTime2 = DateTime.Now;
        DateTime date = dateTime2.AddDays(1.0);
        cache.SetExpires(date);
        response.Cache.SetLastModified(dateTime1);
        response.Cache.SetETag(etag);
        response.Cache.SetCacheability(HttpCacheability.Public);
        return true;
      }
      catch
      {
      }
      return true;
    }

    /// <summary>
    /// Determines whether view permissions are granted for the specified media content.
    /// </summary>
    /// <param name="content">The media content item.</param>
    /// <returns>
    ///   <c>true</c> if view permissions are granted for the specified media content; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsViewAllowed(MediaContent content)
    {
      bool flag = this.IsViewAllowed((ISecuredObject) content, content.GetType());
      if (!ClaimsManager.IsUnrestricted() && !string.IsNullOrEmpty(content.Parent.DownloadSecurityProviderName))
      {
        IDownloadSecurityProvider securityProvider = ObjectFactory.Resolve<IDownloadSecurityProvider>(content.Parent.DownloadSecurityProviderName);
        if (securityProvider != null)
          flag = securityProvider.IsAllowed(content);
      }
      return flag;
    }

    /// <summary>
    /// Determines whether view permissions are granted for the specified secured item and item type.
    /// </summary>
    /// <param name="item">The secured item.</param>
    /// <param name="itemType">The item type.</param>
    /// <returns>
    ///   <c>true</c> if view permissions are granted for the specified secured item and item type; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsViewAllowed(ISecuredObject item, Type itemType)
    {
      bool flag;
      if (ClaimsManager.IsUnrestricted())
        flag = true;
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(itemType) && item.IsPermissionSetSupported("Image"))
        flag = item.IsGranted("Image", "ViewImage");
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(itemType) && item.IsPermissionSetSupported("Document"))
        flag = item.IsGranted("Document", "ViewDocument");
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(itemType) && item.IsPermissionSetSupported("Video"))
        flag = item.IsGranted("Video", "ViewVideo");
      else
        flag = ((IEnumerable<string>) item.SupportedPermissionSets).Where<string>((Func<string, bool>) (s => s != "Comments")).All<string>((Func<string, bool>) (setName => item.IsGranted(setName, 1)));
      return flag;
    }

    [Obsolete("Method is deprecated. Use alternative overload.")]
    protected bool IsViewAllowed(LibraryHttpHandler.OutputItem outputItem)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      bool flag = false;
      if (currentIdentity.IsUnrestricted)
        flag = true;
      else if ((outputItem.DeniedPrincipals.Contains(currentIdentity.UserId) ? 1 : (currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (role => outputItem.DeniedPrincipals.Contains(role.Id))) ? 1 : 0)) == 0)
      {
        if ((outputItem.AllowedPrincipals.Contains(currentIdentity.UserId) ? 1 : (currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (role => outputItem.AllowedPrincipals.Contains(role.Id))) ? 1 : 0)) != 0)
          flag = true;
      }
      else
        flag = false;
      return flag;
    }

    private static string GenerateETag(DateTime lastModified, DateTime now)
    {
      long fileTime1 = lastModified.ToFileTime();
      long fileTime2 = now.ToFileTime();
      string str = fileTime1.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture);
      long num = fileTime1;
      return fileTime2 - num <= 30000000L ? "W/\"" + str + "\"" : "\"" + str + "\"";
    }

    private string GetPathExtension(string path)
    {
      int startIndex = path.LastIndexOf('.');
      return 0 < startIndex && startIndex > path.LastIndexOf('\\') ? path.Substring(startIndex) : ".*";
    }

    private int GetSizeParameter(HttpRequestBase request)
    {
      int result = 0;
      string s = request.QueryString["size"];
      if (!string.IsNullOrEmpty(s) && !int.TryParse(s, out result))
        result = 0;
      return result;
    }

    protected internal interface IOutputItem
    {
      Guid Id { get; set; }

      Guid LibraryId { get; set; }

      string Provider { get; set; }

      string BlobStorageProvider { get; set; }

      string FileName { get; set; }

      string Title { get; set; }

      string MimeType { get; set; }

      Type Type { get; set; }
    }

    protected class FileSystemItem : LibraryHttpHandler.IOutputItem
    {
      public Guid Id { get; set; }

      public Guid LibraryId { get; set; }

      public string Provider { get; set; }

      public string BlobStorageProvider { get; set; }

      public string FileName { get; set; }

      public string Title { get; set; }

      public string MimeType { get; set; }

      public Type Type { get; set; }
    }

    protected class OutputItem : LibraryHttpHandler.IOutputItem
    {
      public Guid Id { get; set; }

      public Guid LibraryId { get; set; }

      public string Provider { get; set; }

      public string BlobStorageProvider { get; set; }

      public string FileName { get; set; }

      public string Title { get; set; }

      public string MimeType { get; set; }

      public Type Type { get; set; }

      public Guid FileId { get; set; }

      public DateTime LastModified { get; set; }

      public byte[] Data { get; set; }

      public ClientCacheControl ClientCacheControl { get; set; }

      [Obsolete("Use property Secured instead.")]
      public List<Guid> AllowedPrincipals { get; set; }

      [Obsolete("Use property Secured instead.")]
      public List<Guid> DeniedPrincipals { get; set; }

      public SecuredProxy Secured { get; set; }
    }

    protected class RedirectOutputItem : LibraryHttpHandler.IOutputItem
    {
      public RedirectOutputItem(string redirectUrl, string cdnUrl)
      {
        this.RedirectUrl = redirectUrl;
        this.CdnUrl = cdnUrl;
      }

      public Guid Id { get; set; }

      public Guid LibraryId { get; set; }

      public string Provider { get; set; }

      public string BlobStorageProvider { get; set; }

      public string FileName { get; set; }

      public string Title { get; set; }

      public string MimeType { get; set; }

      public Type Type { get; set; }

      public string RedirectUrl { get; set; }

      public string CdnUrl { get; set; }
    }

    protected class LibrariesEventsFactory
    {
      public MediaContentDownloadingEvent CreateMediaContentDownloadingEvent(
        LibraryHttpHandler.IOutputItem item,
        System.Web.HttpRequest request,
        string url)
      {
        return this.CreateMediaContentDownloadingEvent(item, (HttpRequestBase) new HttpRequestWrapper(request), url);
      }

      public MediaContentDownloadingEvent CreateMediaContentDownloadingEvent(
        LibraryHttpHandler.IOutputItem item,
        HttpRequestBase request,
        string url)
      {
        return new MediaContentDownloadingEvent()
        {
          FileId = item.Id,
          LibraryId = item.LibraryId,
          Headers = request.Headers,
          ProviderName = item.Provider,
          BlobStorageProviderName = item.BlobStorageProvider,
          Title = item.Title,
          MimeType = item.MimeType,
          Type = item.Type,
          Url = url,
          UserId = ClaimsManager.GetCurrentUserId()
        };
      }

      public MediaContentDownloadedEvent CreateMediaContentDownloadedEvent(
        LibraryHttpHandler.IOutputItem item,
        System.Web.HttpRequest request,
        string url)
      {
        return this.CreateMediaContentDownloadedEvent(item, (HttpRequestBase) new HttpRequestWrapper(request), url);
      }

      public MediaContentDownloadedEvent CreateMediaContentDownloadedEvent(
        LibraryHttpHandler.IOutputItem item,
        HttpRequestBase request,
        string url)
      {
        return new MediaContentDownloadedEvent()
        {
          FileId = item.Id,
          LibraryId = item.LibraryId,
          Headers = request.Headers,
          ProviderName = item.Provider,
          BlobStorageProviderName = item.BlobStorageProvider,
          Title = item.Title,
          MimeType = item.MimeType,
          Type = item.Type,
          Url = url,
          UserId = ClaimsManager.GetCurrentUserId()
        };
      }

      public MediaContentDownloadingEvent CreateMediaContentDownloadingEvent(
        MediaContent item,
        System.Web.HttpRequest request,
        string url)
      {
        return this.CreateMediaContentDownloadingEvent(item, (HttpRequestBase) new HttpRequestWrapper(request), url);
      }

      public MediaContentDownloadingEvent CreateMediaContentDownloadingEvent(
        MediaContent item,
        HttpRequestBase request,
        string url)
      {
        return new MediaContentDownloadingEvent()
        {
          FileId = item.Id,
          LibraryId = item.Parent.Id,
          Headers = request.Headers,
          ProviderName = DataEventFactory.GetProviderName(item.Provider),
          BlobStorageProviderName = item.BlobStorageProvider,
          Title = (string) item.Title,
          MimeType = item.ResolveMimeType(),
          Type = item.GetType(),
          Url = url,
          UserId = ClaimsManager.GetCurrentUserId()
        };
      }

      public MediaContentDownloadedEvent CreateMediaContentDownloadedEvent(
        MediaContent item,
        System.Web.HttpRequest request,
        string url)
      {
        return this.CreateMediaContentDownloadedEvent(item, (HttpRequestBase) new HttpRequestWrapper(request), url);
      }

      public MediaContentDownloadedEvent CreateMediaContentDownloadedEvent(
        MediaContent item,
        HttpRequestBase request,
        string url)
      {
        return new MediaContentDownloadedEvent()
        {
          FileId = item.Id,
          LibraryId = item.Parent.Id,
          Headers = request.Headers,
          ProviderName = DataEventFactory.GetProviderName(item.Provider),
          BlobStorageProviderName = item.BlobStorageProvider,
          Title = (string) item.Title,
          MimeType = item.ResolveMimeType(),
          Type = item.GetType(),
          Url = url,
          UserId = ClaimsManager.GetCurrentUserId()
        };
      }
    }
  }
}
