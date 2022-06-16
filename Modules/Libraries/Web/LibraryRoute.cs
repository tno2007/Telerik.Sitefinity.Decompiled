// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.LibraryRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  /// <summary>Handles Library items requests</summary>
  public class LibraryRoute : RouteBase
  {
    public const string GetLiveMedia = "getLiveMedia";
    public const string ContentUrlKey = "ContentUrl";
    public const string ProviderNameKey = "ProviderName";
    public const string ShowThumbnailKey = "ShowThumbnail";
    public const string ContentTypeKey = "ContentType";
    public const string LCStatus = "MediaStatus";
    internal const string ThumbnailNameKey = "ThumbnailName";
    internal const string VersionKey = "Version";
    internal const string CultureKey = "Culture";

    /// <summary>Gets the route data.</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns></returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string str = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2);
      if (LibraryRoute.AdditionalURLsToFiles)
      {
        string redirectUrl = MediaFileAdditionalUrlsManager.GetRedirectUrl(str);
        if (!string.IsNullOrEmpty(redirectUrl))
          return new RouteData((RouteBase) this, (IRouteHandler) new RedirectRoutingHandler(redirectUrl));
      }
      IDictionary<string, object> result = (IDictionary<string, object>) null;
      if (!LibraryRoute.TryExtractValues(str, httpContext.Request.QueryString, out result, httpContext))
        return (RouteData) null;
      RouteData routeData = new RouteData((RouteBase) this, (IRouteHandler) ObjectFactory.Resolve<LibraryRouteHandler>());
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) result)
        routeData.DataTokens.Add(keyValuePair.Key, keyValuePair.Value);
      return routeData;
    }

    /// <summary>Gets the virtual path.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public override VirtualPathData GetVirtualPath(
      RequestContext requestContext,
      RouteValueDictionary values)
    {
      throw new NotSupportedException();
    }

    internal static bool TryExtractValues(
      string appRelativePath,
      NameValueCollection queryStringValues,
      out IDictionary<string, object> result,
      HttpContextBase httpContext = null)
    {
      result = (IDictionary<string, object>) null;
      int length1 = appRelativePath.IndexOf('/');
      int num1 = length1;
      if (length1 == -1)
        return false;
      string str1 = appRelativePath.Substring(0, length1);
      Type type;
      if (str1.Equals(LibraryRoute.ImagesRoot, StringComparison.OrdinalIgnoreCase))
        type = typeof (Image);
      else if (str1.Equals(LibraryRoute.DocumentsRoot, StringComparison.OrdinalIgnoreCase))
        type = typeof (Document);
      else if (str1.Equals(LibraryRoute.VideosRoot, StringComparison.OrdinalIgnoreCase))
      {
        type = typeof (Video);
      }
      else
      {
        if (!str1.Equals(LibraryRoute.DefaultRoot, StringComparison.OrdinalIgnoreCase))
          return false;
        type = typeof (MediaContent);
      }
      if (httpContext == null)
        httpContext = SystemManager.CurrentHttpContext;
      bool flag1 = false;
      ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Live;
      int result1 = 0;
      string str2 = (string) null;
      if (queryStringValues != null)
      {
        ContentLifecycleStatus result2;
        if (Enum.TryParse<ContentLifecycleStatus>(queryStringValues["Status"], out result2))
        {
          contentLifecycleStatus = result2;
          flag1 = true;
        }
        else
        {
          Uri urlReferrer = httpContext.Request.UrlReferrer;
          if (urlReferrer != (Uri) null)
          {
            bool? nullable = SystemManager.IsBackendUri(httpContext, urlReferrer, out CultureInfo _);
            if (nullable.HasValue)
              flag1 = nullable.Value;
          }
        }
        if (queryStringValues["Version"] != null)
          int.TryParse(queryStringValues["Version"], out result1);
        if (queryStringValues["Culture"] != null)
          str2 = queryStringValues["Culture"];
      }
      if (!flag1 || !SystemManager.TrySetBackendRequest())
        RouteHelper.ApplyCurrentSiteFromUrlReferrer(httpContext.Request);
      bool flag2 = false;
      string str3 = (string) null;
      int length2 = appRelativePath.LastIndexOf('.');
      if (length2 > -1)
      {
        string str4 = appRelativePath.Substring(length2 + 1);
        appRelativePath = appRelativePath.Substring(0, length2);
        if (str4.Equals("tmb", StringComparison.OrdinalIgnoreCase))
        {
          flag2 = true;
          int length3 = appRelativePath.LastIndexOf('.');
          if (length3 > -1)
            appRelativePath = appRelativePath.Substring(0, length3);
        }
        else
        {
          int length4 = appRelativePath.LastIndexOf('.');
          if (length4 > -1)
          {
            string str5 = appRelativePath.Substring(length4 + 1);
            if (str5.StartsWith(LibraryRoute.ThumbnailExtensionPrefix, StringComparison.OrdinalIgnoreCase))
            {
              str3 = str5.Substring(LibraryRoute.ThumbnailExtensionPrefix.Length);
              flag2 = true;
              appRelativePath = appRelativePath.Substring(0, length4);
            }
          }
        }
      }
      int num2 = appRelativePath.IndexOf('/', num1 + 1);
      string str6;
      if (num2 != -1)
        str6 = LibrariesManager.GetProviderNameFromUrl(appRelativePath.Substring(num1 + 1, num2 - num1 - 1).Trim('/'));
      else
        str6 = ManagerBase<LibrariesDataProvider>.GetDefaultProviderName();
      if (!appRelativePath.StartsWith("/"))
        appRelativePath = "/" + appRelativePath;
      result = (IDictionary<string, object>) new Dictionary<string, object>();
      result.Add("ContentUrl", (object) appRelativePath);
      result.Add("ProviderName", (object) str6);
      result.Add("ShowThumbnail", (object) flag2);
      result.Add("ContentType", (object) type);
      result.Add("MediaStatus", (object) contentLifecycleStatus);
      result.Add("ThumbnailName", (object) str3);
      result.Add("Version", (object) result1);
      result.Add("Culture", (object) str2);
      return true;
    }

    public static string ImagesRoot => ConfigSettings.Current.ImagesRoot;

    public static string DocumentsRoot => ConfigSettings.Current.DocumentsRoot;

    public static string VideosRoot => ConfigSettings.Current.VideosRoot;

    public static string DefaultRoot => ConfigSettings.Current.DefaultRoot;

    internal static string ThumbnailExtensionPrefix => ConfigSettings.Current.ThumbnailExtensionPrefix;

    internal static bool AdditionalURLsToFiles => ConfigSettings.Current.AdditionalURLsToFiles;
  }
}
