// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.MediaContentExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Versioning;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Extension methods which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> with helper methods.
  /// </summary>
  public static class MediaContentExtensions
  {
    internal static string UrlVersionQueryParam = Config.Get<LibrariesConfig>().UrlVersionQueryParameter;
    private static readonly char[] urlSegmentDelimiters = new char[1]
    {
      '/'
    };
    private static readonly string[] forceDownloadMimeTypes = new string[2]
    {
      "text/html",
      "application/octet-stream"
    };
    private static readonly string urlForceDownloadQueryParameter = "download=true";

    [Obsolete("Use ResolveThumbnailUrl() extension method instead.")]
    public static string GetThumbnailUrl(this MediaContent mediaContent)
    {
      string url = "~" + (mediaContent.Provider as LibrariesDataProvider).GetItemUrl((ILocatable) mediaContent, CultureInfo.InvariantCulture) + mediaContent.Extension + ".tmb";
      if (mediaContent.Status != ContentLifecycleStatus.Live)
        url += "?Status=Master";
      return UrlPath.ResolveUrl(url, false);
    }

    /// <summary>
    /// Gets the thumbnail url of item of type <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" />.
    ///  </summary>
    /// <param name="mediaContent">The media content.</param>
    /// <returns>The generated thumbnail url.</returns>
    public static string ResolveThumbnailUrl(
      this MediaContent mediaContent,
      bool resolveAsAbsoluteUrl = false,
      int size = 0)
    {
      return mediaContent.ResolveThumbnailUrl(size == 0 ? string.Empty : size.ToString(), resolveAsAbsoluteUrl);
    }

    /// <summary>
    /// Gets the thumbnail url of item of type <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" />.
    /// </summary>
    /// <param name="mediaContent">The media content.</param>
    /// <param name="name">The name of the thumbnail.</param>
    /// <param name="resolveAsAbsoluteUrl">if set to <c>true</c> resolves the URL as absolute. The default is false.</param>
    /// <param name="culture">The culture of the media file.</param>
    /// <returns>The generated thumbnail url.</returns>
    public static string ResolveThumbnailUrl(
      this MediaContent mediaContent,
      string name,
      bool resolveAsAbsoluteUrl = false,
      CultureInfo culture = null)
    {
      if (mediaContent.IsVectorGraphics())
        return mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
      LibrariesDataProvider provider1 = mediaContent.Provider as LibrariesDataProvider;
      BlobStorageManager manager = BlobStorageManager.GetManager(mediaContent.GetStorageProviderName(provider1), true);
      IExternalBlobStorageProvider provider2 = manager.Provider as IExternalBlobStorageProvider;
      bool skipCdn = false;
      string url = (string) null;
      if (provider2 != null && !mediaContent.UseLagacyThumbnailsStorage && mediaContent.Thumbnail != null)
      {
        if (name.IsNullOrEmpty())
          name = "0";
        if (!(provider2 is CloudBlobStorageProvider) || ((CloudBlobStorageProvider) provider2).UrlMode == UrlMode.Original)
        {
          string itemUrl = provider2.GetItemUrl((IBlobContentLocation) mediaContent);
          url = mediaContent.ResolveThumbnailFilePath(name.ToLower(), itemUrl, culture?.Name);
        }
      }
      if (string.IsNullOrEmpty(url))
      {
        string str1 = name.IsNullOrEmpty() ? LibraryRoute.ThumbnailExtensionPrefix : LibraryRoute.ThumbnailExtensionPrefix + name.ToLower();
        MediaFileLink fileLink = MediaContentExtensions.GetFileLinkForCulture(mediaContent, culture);
        if (fileLink != null)
        {
          url = "~" + fileLink.DefaultUrl + "." + str1 + MediaContentExtensions.GetThumbnailExtension(mediaContent);
          if (mediaContent.Status != ContentLifecycleStatus.Live)
          {
            url += "?Status=Master";
            skipCdn = true;
          }
          int num = provider1.GetMediaFileUrls().Where<MediaFileUrl>((Expression<Func<MediaFileUrl, bool>>) (mfu => mfu.Url == fileLink.DefaultUrl && (int) mfu.MediaFileLink.MediaContent.Status == (int) mediaContent.Status)).Select<MediaFileUrl, int>((Expression<Func<MediaFileUrl, int>>) (x => x.MediaFileLink.Culture)).Distinct<int>().Count<int>();
          if (mediaContent.Thumbnails.Count > 0 && num > 1)
          {
            culture = AppSettings.CurrentSettings.GetCultureByLcid(fileLink.Culture);
            if (mediaContent.GetThumbnails(culture).Count<Thumbnail>() == 0)
              culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
            string str2 = url.IndexOf("?") > 0 ? "&" : "?";
            url += string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}={2}", (object) str2, (object) "Culture", (object) culture.Name);
          }
        }
        else
          url = string.Empty;
      }
      return MediaContentExtensions.ResolveUrl(mediaContent, resolveAsAbsoluteUrl, manager, url, name, skipCdn);
    }

    private static string ResolveUrl(
      MediaContent mediaContent,
      bool resolveAsAbsoluteUrl,
      BlobStorageManager blobStorage,
      string url,
      string thumbnailName = null,
      bool skipCdn = false,
      bool skipHostResolvingFromCurrentRequest = false,
      bool skipSchemeResolvingFromCurrentRequest = false)
    {
      bool isThumbnail = thumbnailName != null;
      string cdn;
      string str;
      if (!skipCdn && blobStorage.Provider.TryGetCdnUrl((IBlobContent) mediaContent, out cdn))
      {
        if (url.StartsWith("~/"))
          url = url.Remove(0, 2);
        if (url.StartsWith("http"))
          url = new Uri(url).PathAndQuery;
        str = blobStorage.Provider.GetCdnUrl(cdn, MediaContentExtensions.AppendVersionUrlParam(url, mediaContent, isThumbnail), MediaContentExtensions.urlSegmentDelimiters);
      }
      else
        str = UrlPath.ResolveUrl(MediaContentExtensions.AppendVersionUrlParam(url, mediaContent, isThumbnail), resolveAsAbsoluteUrl, false, skipHostResolvingFromCurrentRequest, skipSchemeResolvingFromCurrentRequest);
      return str;
    }

    /// <summary>
    /// Gets the thumbnail url of item of type <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" />.
    ///  </summary>
    /// <param name="mediaContent">The media content.</param>
    /// <returns>The generated thumbnail url.</returns>
    public static string ResolveMediaUrl(
      this MediaContent mediaContent,
      Dictionary<string, string> urlParameters,
      bool resolveAsAbsoluteUrl = false)
    {
      string str = mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(str);
      if (stringBuilder.ToString().Contains("?"))
        stringBuilder.Append("&");
      else
        stringBuilder.Append("?");
      if (urlParameters != null)
      {
        foreach (KeyValuePair<string, string> urlParameter in urlParameters)
          stringBuilder.Append(urlParameter.Key + "=" + HttpUtility.UrlEncode(urlParameter.Value) + "&");
      }
      if (Config.Get<LibrariesConfig>().Images.EnableImageUrlSignature)
        stringBuilder.Append("Signature=" + MediaContentExtensions.GenerateUrlParamSignature(urlParameters, mediaContent));
      if (stringBuilder[stringBuilder.Length - 1] == '&' || stringBuilder[stringBuilder.Length - 1] == '?')
        --stringBuilder.Length;
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates and returns an URL Signature including all passed urlParameters and the id of the media content.
    /// </summary>
    /// <param name="urlParameters"></param>
    /// <param name="mediaContent"></param>
    /// <returns></returns>
    public static string GenerateUrlParamSignature(
      Dictionary<string, string> urlParameters,
      MediaContent mediaContent)
    {
      return MediaContentExtensions.GenerateUrlParamSignature(urlParameters, mediaContent, new ImageUrlSignatureHashAlgorithm?());
    }

    internal static string GenerateUrlParamSignature(
      Dictionary<string, string> urlParameters,
      MediaContent mediaContent,
      ImageUrlSignatureHashAlgorithm? hashAlgorithm = null)
    {
      HashAlgorithm imageHashAlgorithm = MediaContentExtensions.GetImageHashAlgorithm(hashAlgorithm);
      Dictionary<string, string> dictionary = new Dictionary<string, string>((IDictionary<string, string>) urlParameters);
      if (!dictionary.ContainsKey("ImageId"))
        dictionary.Add("ImageId", mediaContent.Id.ToString());
      StringBuilder stringBuilder = new StringBuilder();
      List<string> list = dictionary.Keys.ToList<string>();
      list.Sort();
      foreach (string key in list)
      {
        if (!dictionary[key].IsNullOrWhitespace() && dictionary[key] != "0")
          stringBuilder.Append(key + "=" + dictionary[key] + ",");
      }
      --stringBuilder.Length;
      byte[] hash;
      using (imageHashAlgorithm)
        hash = imageHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString().ToLowerInvariant()));
      return Utility.BytesToHex(hash);
    }

    internal static string ResolveThumbnailFilePath(
      this MediaContent mediaContent,
      string tmbName,
      string fromPath = null,
      string culture = null)
    {
      if (!string.IsNullOrEmpty(culture) && !AppSettings.CurrentSettings.AllLanguages.Values.Contains(CultureInfo.GetCultureInfo(culture)))
        culture = (string) null;
      using (new CultureRegion(culture))
      {
        if (string.IsNullOrEmpty(fromPath))
          fromPath = mediaContent.FilePath;
        if (fromPath == null)
          return (string) null;
        if (!string.IsNullOrEmpty(mediaContent.Extension))
        {
          int length = fromPath.LastIndexOf(mediaContent.Extension);
          if (length > 0)
            fromPath = fromPath.Substring(0, length);
        }
        fromPath = fromPath + "." + LibraryRoute.ThumbnailExtensionPrefix + tmbName + MediaContentExtensions.GetThumbnailExtension(mediaContent);
      }
      return fromPath;
    }

    private static string GetThumbnailExtension(MediaContent mediaContent)
    {
      switch (mediaContent)
      {
        case Telerik.Sitefinity.Libraries.Model.Image image when ImagesHelper.supportedTmbExtensions.Contains(image.Extension):
          return image.Extension;
        case Video _:
          return ".jpg";
        default:
          return ".png";
      }
    }

    public static string ResolveMediaUrl(this IBlobContent mediaContent, bool resolveAsAbsoluteUrl = false)
    {
      switch (mediaContent)
      {
        case MediaContent _:
          return ((MediaContent) mediaContent).ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
        case Thumbnail _:
          return ((Thumbnail) mediaContent).Parent.ResolveThumbnailUrl(((Thumbnail) mediaContent).Name, resolveAsAbsoluteUrl);
        default:
          throw new NotSupportedException(string.Format("ResolveMediaUrl does not support type '{0}'", (object) mediaContent.GetType()));
      }
    }

    internal static string ResolveMimeType(this IBlobContentLocation content)
    {
      switch (content)
      {
        case MediaContent mediaContent:
          string extension = mediaContent.Extension;
          MimeMappingElement mimeMappingElement;
          return Config.Get<LibrariesConfig>().MimeMappings.TryGetValue(extension, out mimeMappingElement) ? mimeMappingElement.MimeType : mediaContent.MimeType;
        case Thumbnail thumbnail:
          return thumbnail.MimeType;
        default:
          return content.MimeType;
      }
    }

    /// <summary>
    /// Gets the original media url of item of type <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" />.
    /// </summary>
    /// <param name="mediaContent">The media content.</param>
    /// <param name="resolveAsAbsoluteUrl">if set to <c>true</c> resolves the URL as absolute. The default is false.</param>
    /// <param name="culture">The culture of the media file.</param>
    /// <returns>The generated thumbnail url.</returns>
    public static string ResolveMediaUrl(
      this MediaContent mediaContent,
      bool resolveAsAbsoluteUrl = false,
      CultureInfo culture = null)
    {
      return mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, culture, false, false);
    }

    internal static string ResolveMediaUrl(
      this MediaContent mediaContent,
      bool resolveAsAbsoluteUrl,
      CultureInfo culture,
      bool skipHostResolvingFromCurrentRequest = false,
      bool skipSchemeResolvingFromCurrentRequest = false)
    {
      LibrariesDataProvider provider1 = mediaContent.Provider as LibrariesDataProvider;
      BlobStorageManager manager = BlobStorageManager.GetManager(mediaContent.GetStorageProviderName(provider1), false);
      if (manager == null)
        return string.Empty;
      IExternalBlobStorageProvider provider2 = manager.Provider as IExternalBlobStorageProvider;
      bool skipCdn = false;
      string url1 = (string) null;
      if (provider2 != null && (!(provider2 is CloudBlobStorageProvider) || ((CloudBlobStorageProvider) provider2).UrlMode == UrlMode.Original))
        url1 = provider2.GetItemUrl((IBlobContentLocation) mediaContent);
      if (string.IsNullOrEmpty(url1))
      {
        MediaFileLink fileLinkForCulture = MediaContentExtensions.GetFileLinkForCulture(mediaContent, culture);
        url1 = fileLinkForCulture == null ? "~/" : "~" + fileLinkForCulture.DefaultUrl + fileLinkForCulture.Extension;
        if (mediaContent.Status != ContentLifecycleStatus.Live)
        {
          url1 = url1 + "?Status=" + mediaContent.Status.ToString();
          skipCdn = true;
        }
      }
      string url2 = MediaContentExtensions.ResolveUrl(mediaContent, resolveAsAbsoluteUrl, manager, url1, skipCdn: skipCdn, skipHostResolvingFromCurrentRequest: skipHostResolvingFromCurrentRequest, skipSchemeResolvingFromCurrentRequest: skipSchemeResolvingFromCurrentRequest);
      if (((IEnumerable<string>) MediaContentExtensions.forceDownloadMimeTypes).Contains<string>(mediaContent.MimeType))
        url2 = MediaContentExtensions.AppendDownloadUrlParam(url2);
      return url2;
    }

    private static MediaFileLink GetFileLinkForCulture(
      MediaContent mediaContent,
      CultureInfo culture)
    {
      if (culture == null)
        culture = AppSettings.CurrentSettings.CurrentCulture;
      return mediaContent.GetFileLink(cultureId: new int?(AppSettings.CurrentSettings.GetCultureLcid(culture)));
    }

    /// <summary>Gets media content item Id code</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <returns>Returns the id code as string.</returns>
    internal static string GetIdCode(this MediaContent mediaContent) => (mediaContent.OriginalContentId != Guid.Empty ? mediaContent.OriginalContentId : mediaContent.Id).GetHashCode().ToString("x");

    internal static IBlobProperties GetBlobProperties(this MediaContent content) => (IBlobProperties) new BlobProperties()
    {
      ContentType = content.MimeType,
      CacheControl = content.GetClientCacheControl().ToHttpCacheControlHeaderValue()
    };

    internal static IBlobProperties GetBlobProperties(
      this MediaContent media,
      IBlobContent content)
    {
      return (IBlobProperties) new BlobProperties()
      {
        ContentType = content.MimeType,
        CacheControl = media.GetClientCacheControl().ToHttpCacheControlHeaderValue()
      };
    }

    internal static ClientCacheControl GetClientCacheControl(
      this MediaContent content,
      OutputCacheElement cacheConfig = null)
    {
      cacheConfig = cacheConfig ?? Config.Get<SystemConfig>().CacheSettings;
      return content.GetCacheProfile(cacheConfig).ToClientCacheControl(!cacheConfig.EnableClientCache);
    }

    internal static OutputCacheProfileElement GetCacheProfile(
      this MediaContent content,
      OutputCacheElement cacheConfig = null)
    {
      cacheConfig = cacheConfig ?? Config.Get<SystemConfig>().CacheSettings;
      string key = content.Parent.ClientCacheProfile;
      if (!string.IsNullOrEmpty(key) && !cacheConfig.MediaCacheProfiles.ContainsKey(key))
      {
        ArgumentException exceptionToHandle = new ArgumentException("Invalid library cache profile specified: \"{0}\".".Arrange((object) key));
        if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
          throw exceptionToHandle;
        key = (string) null;
      }
      if (string.IsNullOrEmpty(key))
        key = !(content.Parent is DocumentLibrary) ? (!(content.Parent is VideoLibrary) ? cacheConfig.DefaultImageProfile : cacheConfig.DefaultVideoProfile) : cacheConfig.DefaultDocumentProfile;
      return cacheConfig.MediaCacheProfiles[key];
    }

    internal static void ApplyThumbnailProfileToControl(
      this MediaContent content,
      WebControl imageControl,
      string thumbnailName)
    {
      double width;
      double height;
      MediaContentExtensions.GetThumbnailProfileSizes(content, thumbnailName, out width, out height);
      MediaContentExtensions.SetSizePropertiesToControl(imageControl, width, height);
    }

    internal static void ApplySingleItemThumbnailProfileToControl(
      this MediaContent content,
      WebControl imageControl,
      string thumbnailName)
    {
      double width;
      double height;
      MediaContentExtensions.GetThumbnailProfileSizes(content, thumbnailName, out width, out height);
      MediaContentExtensions.SetSizeAttributesToControl(imageControl, width, height);
    }

    internal static IEnumerable<IDependentItem> GetDependencies(
      this MediaContent mediaContent)
    {
      foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) mediaContent.MediaFileLinks)
        yield return (IDependentItem) new MediaContentDependentItem(mediaFileLink);
    }

    internal static void UpdateMediaContent(
      this MediaContent mediaContent,
      IDependentItem dependentItem)
    {
      MediaFileLink fileLink = mediaContent.GetFileLink(true);
      Guid guid = new Guid(dependentItem.Key);
      if (!(fileLink.FileId != guid))
        return;
      IDictionary<string, object> data = BlobContentCleanerTask.GetData(dependentItem.GetData());
      string providerName = (string) data["blobStorageProvider"];
      bool flag1 = BlobStorageManager.GetManager(providerName).Provider is IExternalBlobStorageProvider;
      bool flag2 = fileLink.FileId != Guid.Empty;
      mediaContent.BlobStorageProvider = providerName;
      fileLink.FileId = guid;
      fileLink.FilePath = flag1 ? dependentItem.Key : (string) data["filePath"];
      fileLink.NumberOfChunks = (int) data["numberOfChunks"];
      fileLink.TotalSize = (long) (int) data["totalSize"];
      fileLink.Width = (int) data["width"];
      fileLink.Height = (int) data["height"];
      fileLink.Extension = (string) data["extension"];
      fileLink.MimeType = (string) data["mimeType"];
      fileLink.DefaultUrl = fileLink.DefaultUrl ?? (string) data["defaultUrl"];
      if (!flag2)
        return;
      ((LibrariesDataProvider) mediaContent.GetProvider()).RegenerateThumbnails(mediaContent);
    }

    internal static string ResolveVersionMediaUrl(
      this MediaContent mediaContent,
      int version,
      CultureInfo culture = null,
      bool absolute = true)
    {
      if (culture == null)
        culture = AppSettings.CurrentSettings.CurrentCulture;
      MediaFileLink fileLinkForCulture = MediaContentExtensions.GetFileLinkForCulture(mediaContent, culture);
      string url = UrlPath.ResolveUrl(MediaContentExtensions.AppendVersionUrlParam(fileLinkForCulture == null ? "~/" : "~" + fileLinkForCulture.DefaultUrl + fileLinkForCulture.Extension, mediaContent), absolute);
      if (((IEnumerable<string>) MediaContentExtensions.forceDownloadMimeTypes).Contains<string>(mediaContent.MimeType))
        url = MediaContentExtensions.AppendDownloadUrlParam(url);
      ContentLifecycleStatus contentLifecycleStatus = mediaContent.IsPublishedInCulture() ? mediaContent.Status : ContentLifecycleStatus.Master;
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}&{1}={2}&Status={3}&{4}={5}", (object) url, (object) "Version", (object) version, (object) contentLifecycleStatus, (object) "Culture", (object) culture);
    }

    private static void GetThumbnailProfileSizes(
      MediaContent content,
      string thumbnailName,
      out double width,
      out double height)
    {
      width = 0.0;
      height = 0.0;
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = Config.Get<LibrariesConfig>().GetThumbnailProfiles(content.Parent.GetType());
      if (!thumbnailProfiles.ContainsKey(thumbnailName))
        return;
      ThumbnailProfileConfigElement thumbnailProfile = thumbnailProfiles[thumbnailName];
      if (thumbnailProfile.Parameters.Keys.Contains("Width"))
        width = MediaContentExtensions.ParseThumbnailProfileSize(thumbnailProfile, "Width");
      else if (thumbnailProfile.Parameters.Keys.Contains("MaxWidth"))
        width = MediaContentExtensions.ParseThumbnailProfileSize(thumbnailProfile, "MaxWidth");
      if (thumbnailProfile.Parameters.Keys.Contains("Height"))
      {
        height = MediaContentExtensions.ParseThumbnailProfileSize(thumbnailProfile, "Height");
      }
      else
      {
        if (!thumbnailProfile.Parameters.Keys.Contains("MaxHeight"))
          return;
        height = MediaContentExtensions.ParseThumbnailProfileSize(thumbnailProfile, "MaxHeight");
      }
    }

    private static double ParseThumbnailProfileSize(
      ThumbnailProfileConfigElement thumbnailProfile,
      string parameterKey)
    {
      double result = 0.0;
      double.TryParse(thumbnailProfile.Parameters[parameterKey], out result);
      return result;
    }

    private static void SetSizePropertiesToControl(WebControl control, double width, double height)
    {
      if (height > 0.0)
        control.Height = new Unit(height);
      if (width <= 0.0)
        return;
      control.Width = new Unit(width);
    }

    private static void SetSizeAttributesToControl(WebControl control, double width, double height)
    {
      if (height > 0.0)
        control.Attributes.Add("data-height", height.ToString());
      if (width <= 0.0)
        return;
      control.Attributes.Add("data-width", width.ToString());
    }

    private static string GetUrl(string url) => RouteHelper.IsCompleteUrl(url) ? url : RouteHelper.ResolveUrl(url, UrlResolveOptions.Rooted);

    private static string AppendVersionUrlParam(
      string url,
      MediaContent mediaContent,
      bool isThumbnail = false)
    {
      if (string.IsNullOrEmpty(MediaContentExtensions.UrlVersionQueryParam))
        return url;
      string appenderSymbol = MediaContentExtensions.GetAppenderSymbol(url);
      Guid guid = mediaContent.OriginalContentId != Guid.Empty ? mediaContent.OriginalContentId : mediaContent.Id;
      return url + appenderSymbol + MediaContentExtensions.UrlVersionQueryParam + "=" + guid.GetHashCode().ToString("x") + "_" + (isThumbnail ? mediaContent.ThumbnailsVersion.ToString() : mediaContent.Version.ToString());
    }

    private static string AppendDownloadUrlParam(string url)
    {
      string appenderSymbol = MediaContentExtensions.GetAppenderSymbol(url);
      return url + appenderSymbol + MediaContentExtensions.urlForceDownloadQueryParameter;
    }

    private static string GetAppenderSymbol(string url) => url.IndexOf('?') <= 0 ? "?" : "&";

    private static HashAlgorithm GetImageHashAlgorithm(
      ImageUrlSignatureHashAlgorithm? hash = null)
    {
      if (!hash.HasValue)
        hash = new ImageUrlSignatureHashAlgorithm?(Config.Get<LibrariesConfig>().Images.ImageUrlSignatureHashAlgorithm);
      ImageUrlSignatureHashAlgorithm? nullable = hash;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case ImageUrlSignatureHashAlgorithm.MD5:
            return (HashAlgorithm) MD5.Create();
          case ImageUrlSignatureHashAlgorithm.SHA1:
            return (HashAlgorithm) SHA1.Create();
        }
      }
      throw new ArgumentException("Invalid HashAlgorithm.");
    }

    /// <summary>Gets the file URL.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <returns></returns>
    internal static string GetUrlRoot(this MediaContent mediaContent)
    {
      Type type = mediaContent.GetType();
      return !(type == typeof (Telerik.Sitefinity.Libraries.Model.Image)) ? (!(type == typeof (Video)) ? (!(type == typeof (Document)) ? "libraries" : Config.Get<LibrariesConfig>().Documents.UrlRoot) : Config.Get<LibrariesConfig>().Videos.UrlRoot) : Config.Get<LibrariesConfig>().Images.UrlRoot;
    }

    /// <summary>Gets the name of the storage provider.</summary>
    /// <param name="mediaContent">Content of the media.</param>
    /// <returns></returns>
    public static string GetStorageProviderName(
      this MediaContent mediaContent,
      LibrariesDataProvider provider = null)
    {
      string blobStorageProvider = mediaContent.BlobStorageProvider;
      if (string.IsNullOrEmpty(blobStorageProvider) && mediaContent.Parent != null)
        blobStorageProvider = mediaContent.Parent.BlobStorageProvider;
      if (provider == null)
        provider = mediaContent.Provider as LibrariesDataProvider;
      return provider != null ? provider.GetMappedBlobStorageProviderName(blobStorageProvider) : blobStorageProvider;
    }

    /// <summary>Clears the media urls for this media content item</summary>
    /// <param name="mediaContent">The media content item</param>
    /// <param name="manager">The manager to use</param>
    /// <param name="excludeDefault">A value, indicating if the default url should be left</param>
    /// <param name="excludeSharedTranslations">A value, indicating if the shared translations should be left</param>
    public static void ClearMediaFileUrls(
      this MediaContent mediaContent,
      IManager manager,
      bool excludeDefault = false,
      bool excludeSharedTranslations = false)
    {
      MediaFileLink fileLink = mediaContent.GetFileLink();
      if (fileLink == null)
        return;
      List<MediaFileLink> mediaFileLinkList;
      if (!excludeSharedTranslations)
      {
        mediaFileLinkList = mediaContent.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileLink.FileId)).ToList<MediaFileLink>();
      }
      else
      {
        mediaFileLinkList = new List<MediaFileLink>();
        mediaFileLinkList.Add(fileLink);
      }
      foreach (MediaFileLink mediaFileLink in mediaFileLinkList)
      {
        IEnumerable<MediaFileUrl> source = (IEnumerable<MediaFileUrl>) mediaFileLink.Urls;
        if (excludeDefault)
          source = source.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => !u.IsDefault));
        foreach (MediaFileUrl mediaFileUrl in source.ToList<MediaFileUrl>())
        {
          fileLink.Urls.Remove(mediaFileUrl);
          manager.DeleteItem((object) mediaFileUrl);
        }
      }
    }

    /// <summary>
    /// Extension method which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Libraries.Model.Library" /> with helper methods.
    /// </summary>
    public static IQueryable<MediaContent> Items(this Library library)
    {
      if (!(library.Provider is LibrariesDataProvider provider))
        return new List<MediaContent>().AsQueryable<MediaContent>();
      Guid parentId = library.Id;
      return provider.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.Parent.Id == parentId));
    }

    /// <summary>
    /// Extension method which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Libraries.Model.Album" /> with helper methods.
    /// </summary>
    public static IQueryable<Telerik.Sitefinity.Libraries.Model.Image> Images(
      this Album album)
    {
      if (!(album.Provider is LibrariesDataProvider provider))
        return new List<Telerik.Sitefinity.Libraries.Model.Image>().AsQueryable<Telerik.Sitefinity.Libraries.Model.Image>();
      Guid parentId = album.Id;
      return provider.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Parent.Id == parentId));
    }

    /// <summary>
    /// Extension method which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> with helper methods.
    /// </summary>
    public static IQueryable<Document> Documents(this DocumentLibrary library)
    {
      if (!(library.Provider is LibrariesDataProvider provider))
        return new List<Document>().AsQueryable<Document>();
      Guid parentId = library.Id;
      return provider.GetDocuments().Where<Document>((Expression<Func<Document, bool>>) (d => d.Parent.Id == parentId));
    }

    /// <summary>
    /// Extension method which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> with helper methods.
    /// </summary>
    public static IQueryable<Video> Videos(this VideoLibrary library)
    {
      if (!(library.Provider is LibrariesDataProvider provider))
        return new List<Video>().AsQueryable<Video>();
      Guid parentId = library.Id;
      return provider.GetVideos().Where<Video>((Expression<Func<Video, bool>>) (d => d.Parent.Id == parentId));
    }

    /// <summary>
    /// Gets a value indicating whether the media content is vector graphics or not.
    /// </summary>
    /// <param name="mediaContent">The media content.</param>
    /// <returns>A value indicating whether the media content is vector graphics or not.</returns>
    public static bool IsVectorGraphics(this MediaContent mediaContent) => mediaContent.Extension != null && mediaContent.Extension.Equals(".svg", StringComparison.InvariantCultureIgnoreCase);
  }
}
