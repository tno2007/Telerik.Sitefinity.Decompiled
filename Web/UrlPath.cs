// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlPath
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Helper class for working with virtual paths.</summary>
  public static class UrlPath
  {
    private const string IsSecureConnection = "IsSecureConnection";
    internal const string RendererProxyHeader = "X-SFRENDERER-PROXY";

    internal static string ResolveAbsoluteUrl(Uri baseUri, string relativePath)
    {
      if (RouteHelper.IsAbsoluteUrl(relativePath))
        return relativePath;
      if (relativePath.StartsWith("~/"))
        relativePath = relativePath.Substring(2);
      relativePath = baseUri.AbsolutePath.TrimEnd('/') + "/" + relativePath.TrimStart('/');
      return new Uri(baseUri, relativePath).AbsoluteUri;
    }

    /// <summary>Appends the query string.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="queryString">The query string.</param>
    /// <returns></returns>
    public static string AppendQueryString(string url, NameValueCollection queryString)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < queryString.Keys.Count; ++index1)
      {
        string key = queryString.Keys[index1];
        string[] values = queryString.GetValues(key);
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          stringBuilder.Append(key);
          stringBuilder.Append("=");
          stringBuilder.Append(values[index2]);
          if (index2 < values.Length - 1)
            stringBuilder.Append("&");
        }
        if (index1 < queryString.Keys.Count - 1)
          stringBuilder.Append("&");
      }
      return UrlPath.AppendQueryString(url, stringBuilder.ToString());
    }

    /// <summary>Appends the query string.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="queryString">The query string.</param>
    /// <returns></returns>
    public static string AppendQueryString(string url, string queryString)
    {
      if (string.IsNullOrEmpty(url))
        return string.Empty;
      url = url.IndexOf('?') != -1 ? url + "&" : url + "?";
      return url + queryString;
    }

    /// <summary>
    /// Converts a URL into one that is usable on the requesting client.
    /// </summary>
    /// <returns>The converted URL.</returns>
    /// <param name="url">The URL to convert.</param>
    public static string ResolveUrl(string url) => UrlPath.ResolveUrl(url, false);

    /// <summary>Resolves the URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="absolute">if set to <c>true</c> resolves the URL as absolute.</param>
    /// <returns></returns>
    public static string ResolveUrl(string url, bool absolute, bool removeTrailingSlash = false) => UrlPath.ResolveUrl(url, absolute, (string) null, (string) null, -1, removeTrailingSlash);

    /// <summary>
    /// Converts a relative path into fully qualified one that is usable on the requesting client
    /// including protocol, domain, port (if not standard) and file path.
    /// </summary>
    /// <param name="relativePath">The path to convert.</param>
    /// <returns>The converted path.</returns>
    public static string ResolveAbsoluteUrl(string relativePath, bool removeTrailingSlash = false) => UrlPath.ResolveAbsoluteUrl(relativePath, (string) null, -1, (string) null, removeTrailingSlash);

    /// <summary>
    /// Converts a relative path into fully qualified one that is usable on the requesting client
    /// including protocol, domain, port (if not standard) and file path.
    /// </summary>
    /// <param name="relativePath">The path to convert.</param>
    /// <param name="host">The domain name to use.</param>
    /// <returns>The converted path.</returns>
    internal static string ResolveAbsoluteUrlWithHost(string relativePath, string host) => UrlPath.ResolveAbsoluteUrl(relativePath, (string) null, -1, host);

    internal static string ResolveAbsoluteUrlWithoutNonDefaultUrlSettings(
      string relativePath,
      string host)
    {
      return UrlPath.ResolveUrl(relativePath, true, (string) null, host, -1, ignoreNonDefaultSiteUrl: true);
    }

    /// <summary>
    /// Converts a relative path into fully qualified one that is usable on the requesting client
    /// including protocol, domain, port (if not standard) and file path.
    /// </summary>
    /// <param name="relativePath">The path to convert.</param>
    /// <param name="protocol">
    /// The protocol (http, https, ftp and etc.) to use.
    /// </param>
    /// <returns>The converted path.</returns>
    public static string ResolveAbsoluteUrl(string relativePath, string protocol) => UrlPath.ResolveAbsoluteUrl(relativePath, protocol, -1, (string) null);

    /// <summary>
    /// Converts a relative path into fully qualified one that is usable on the requesting client
    /// including protocol, domain, port (if not standard) and file path.
    /// </summary>
    /// <param name="relativePath">The path to convert.</param>
    /// <param name="protocol">
    /// The protocol (http, https, ftp and etc.) to use.
    /// </param>
    /// <param name="port">Specifies the TCP port ot use.</param>
    /// <returns>The converted path.</returns>
    public static string ResolveAbsoluteUrl(string relativePath, string protocol, int port) => UrlPath.ResolveAbsoluteUrl(relativePath, protocol, port, (string) null);

    /// <summary>
    /// Converts a relative path into fully qualified one that is usable on the requesting client
    /// including protocol, domain, port (if not standard) and file path.
    /// </summary>
    /// <param name="relativePath">The path to convert.</param>
    /// <param name="protocol">
    /// The protocol (http, https, ftp and etc.) to use.
    /// </param>
    /// <param name="port">Specifies the TCP port ot use.</param>
    /// <param name="host">The domain name to use.</param>
    /// <returns>The converted path.</returns>
    public static string ResolveAbsoluteUrl(
      string relativePath,
      string protocol,
      int port,
      string host,
      bool removeTrailingSlash = false)
    {
      return UrlPath.ResolveUrl(relativePath, true, protocol, host, port, removeTrailingSlash);
    }

    internal static string ResolveUrl(
      string url,
      bool absolute,
      bool removeTrailingSlash,
      bool skipHostResolvingFromCurrentRequest = false,
      bool skipSchemeResolvingFromCurrentRequest = false)
    {
      return UrlPath.ResolveUrl(url, absolute, (string) null, (string) null, -1, removeTrailingSlash, skipHostResolvingFromCurrentRequest, skipSchemeResolvingFromCurrentRequest);
    }

    private static string ResolveUrl(
      string url,
      bool absolute,
      string scheme,
      string host,
      int port,
      bool removeTrailingSlash = false,
      bool skipHostResolvingFromCurrentRequest = false,
      bool ignoreNonDefaultSiteUrl = false)
    {
      if (string.IsNullOrEmpty(url) || url.StartsWith("../") || url.StartsWith("//"))
        return url;
      string str1 = string.Empty;
      int num = url.IndexOf("?");
      if (num != -1)
      {
        str1 = url.Substring(num);
        url = url.Substring(0, num);
      }
      if (string.IsNullOrEmpty(url) || url.IndexOf(':') != -1)
        return url + str1;
      string str2;
      if (url.StartsWith("~/"))
        str2 = VirtualPathUtility.AppendTrailingSlash(HostingEnvironment.ApplicationVirtualPath) + url.Substring(2) + str1;
      else if (VirtualPathUtility.IsAbsolute(url))
        str2 = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(HostingEnvironment.ApplicationVirtualPath), url) + str1;
      else if (SystemManager.CurrentHttpContext != null)
      {
        string basePath = SystemManager.CurrentHttpContext.Request.RawUrl;
        int length = basePath.IndexOf("?");
        if (length != -1)
          basePath = basePath.Substring(0, length);
        str2 = VirtualPathUtility.Combine(basePath, url) + str1;
      }
      else
        str2 = url + str1;
      if (absolute)
        str2 = UrlPath.GetDomainUrl(scheme, host, port, skipHostResolvingFromCurrentRequest, ignoreNonDefaultSiteUrl) + str2;
      if (removeTrailingSlash && string.IsNullOrWhiteSpace(str1) && str2.EndsWith("/") && str2.Length > 1)
        str2 = str2.TrimEnd('/');
      return str2;
    }

    /// <summary>Gets the domain of the current request.</summary>
    /// <returns></returns>
    public static string GetDomainUrl() => UrlPath.GetDomainUrl((string) null, (string) null, -1);

    private static string GetDomainUrl(
      string scheme,
      string host,
      int port,
      bool skipHostResolvingFromCurrentRequest = false,
      bool ignoreNonDefaultSiteUrl = false)
    {
      Uri uri = (Uri) null;
      if (!ignoreNonDefaultSiteUrl)
        uri = UrlPath.GetNonDefaultSiteUrl(SystemManager.Host, scheme);
      if (uri == (Uri) null)
        uri = SystemManager.GetSiteUri(skipHostResolvingFromCurrentRequest);
      if (string.IsNullOrEmpty(host))
      {
        if (uri == (Uri) null)
          return string.Empty;
        host = uri.Host;
      }
      if (string.IsNullOrEmpty(scheme))
        scheme = UrlPath.GetScheme();
      string str = string.Empty;
      if (port < 0 && uri != (Uri) null && !uri.IsDefaultPort)
        port = uri.Port;
      if (port > 0 && (scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) && port != 80 || scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) && port != 443))
        str = ":" + (object) port;
      return scheme + "://" + host + str;
    }

    internal static string GetScheme() => UrlPath.GetDefaultScheme(UrlPath.IsSecuredConnection(SystemManager.CurrentHttpContext));

    internal static bool IsSecuredConnection(HttpContextBase httpContext)
    {
      if (httpContext == null || httpContext.Request == null)
        return false;
      object obj = httpContext.Items[(object) "IsSecureConnection"];
      if (obj == null)
      {
        obj = (object) (bool) (UrlPath.IsSslOffloaded(httpContext) ? 1 : (UrlPath.IsSecuredConnection(httpContext.Request) ? 1 : 0));
        httpContext.Items[(object) "IsSecureConnection"] = obj;
      }
      return (bool) obj;
    }

    internal static bool IsSslOffloaded(HttpContextBase httpContext)
    {
      bool flag1 = false;
      SslOffloadingElement offloadingSettings = Config.Get<SystemConfig>().SslOffloadingSettings;
      if (offloadingSettings.EnableSslOffloading || UrlPath.IsKnownProxy(httpContext))
      {
        string httpHeaderFieldName = offloadingSettings.HttpHeaderFieldName;
        string headerFieldValue = offloadingSettings.HttpHeaderFieldValue;
        bool flag2 = string.Equals(httpContext.Request.Headers[httpHeaderFieldName], headerFieldValue, StringComparison.OrdinalIgnoreCase);
        if (httpContext.Items.Contains((object) httpHeaderFieldName))
          flag2 = flag2 || string.Equals(httpContext.Items[(object) httpHeaderFieldName].ToString(), headerFieldValue, StringComparison.OrdinalIgnoreCase);
        flag1 = flag2 || UrlPath.IsProtoHttpsForwarded(httpContext.Request);
      }
      return flag1;
    }

    internal static bool IsKnownProxy(HttpContextBase httpContext)
    {
      string header = httpContext.Request.Headers["X-SFRENDERER-PROXY"];
      return header != null && string.Equals(header, "true", StringComparison.OrdinalIgnoreCase);
    }

    internal static void AddKnownProxyHeaders(
      HttpContextBase httpContext,
      HttpRequestHeaders headers)
    {
      string header = httpContext.Request.Headers["X-SFRENDERER-PROXY"];
      if (header == null || headers.Contains(header))
        return;
      headers.Add("X-SFRENDERER-PROXY", header);
    }

    private static bool IsProtoHttpsForwarded(HttpRequestBase request)
    {
      string header = request.Headers["Forwarded"];
      if (string.IsNullOrWhiteSpace(header))
        return false;
      IEnumerable<string> source = ((IEnumerable<string>) header.Split(new char[1]
      {
        ';'
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (p => p.Trim()));
      if (source == null)
        return false;
      string pair = "proto=" + Uri.UriSchemeHttps;
      return source.Any<string>((Func<string, bool>) (value => string.Equals(value, pair, StringComparison.InvariantCultureIgnoreCase)));
    }

    internal static bool IsSecuredConnection(HttpRequestBase request)
    {
      if (request == null)
        return false;
      return request.IsSecureConnection || string.Equals(request.Url.Scheme, Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase);
    }

    internal static string ResolveUrlForCurrentSite(
      string url,
      bool absolute,
      bool removeTrailingSlash = false)
    {
      return UrlPath.ResolveUrl(url, absolute, (string) null, (string) null, -1, removeTrailingSlash, true);
    }

    private static string GetDefaultScheme(bool isSecureConnection) => isSecureConnection ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

    internal static Uri GetNonDefaultSiteUrl(string host, string scheme)
    {
      Uri nonDefaultSiteUrl = (Uri) null;
      SiteUrlSettings siteUrlSettings = Config.Get<SystemConfig>().SiteUrlSettings;
      if (siteUrlSettings.EnableNonDefaultSiteUrlSettings)
      {
        string str1 = string.Empty;
        string str2 = string.Empty;
        bool isSecureConnection = string.Equals(scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) || UrlPath.IsSecuredConnection(SystemManager.CurrentHttpContext);
        if (string.IsNullOrEmpty(scheme))
          scheme = UrlPath.GetDefaultScheme(isSecureConnection);
        if (!isSecureConnection)
          str2 = string.IsNullOrEmpty(siteUrlSettings.NonDefaultHttpPort) ? "80" : siteUrlSettings.NonDefaultHttpPort;
        if (isSecureConnection)
          str2 = string.IsNullOrEmpty(siteUrlSettings.NonDefaultHttpsPort) ? "443" : siteUrlSettings.NonDefaultHttpsPort;
        if (!string.IsNullOrEmpty(str2))
          str1 = host + ":" + str2;
        if (!string.IsNullOrEmpty(str1))
          nonDefaultSiteUrl = new Uri(scheme + "://" + str1);
      }
      return nonDefaultSiteUrl;
    }

    /// <summary>
    /// Returns the scheme and host part of an URI, dealing with <c>null</c>, empty string, invalid and relative URIs.
    /// </summary>
    /// <param name="host">A full URI or only the host name, possibly <c>null</c>, empty or invalid.</param>
    /// <param name="defaultScheme">The default URI scheme to be used, if <paramref name="host" /> does not specify one (is considered relative by the <see cref="T:System.Uri" /> class).</param>
    /// <returns>The scheme and host part of the URI (no trailing slash).</returns>
    public static string GetAbsoluteHost(string host, string defaultScheme = "http")
    {
      if (string.IsNullOrEmpty(host))
        return (string) null;
      Uri result;
      return !Uri.TryCreate(host, UriKind.RelativeOrAbsolute, out result) ? (string) null : UrlPath.GetAbsoluteHost(result);
    }

    /// <summary>
    /// Returns the scheme and host part of an URI, dealing with <c>null</c> and relative URIs.
    /// </summary>
    /// <param name="uri">An absolute or relative URI, possibly <c>null</c>.</param>
    /// <param name="defaultScheme">The default URI scheme to be used, if <paramref name="host" /> does not specify one (is relative).</param>
    /// <returns>The scheme and host part of the URI (no trailing slash).</returns>
    public static string GetAbsoluteHost(Uri uri, string defaultScheme = "http")
    {
      if (uri == (Uri) null)
        return (string) null;
      if (!uri.IsAbsoluteUri)
        uri = new Uri(defaultScheme.ToLowerInvariant() + "://" + uri.ToString());
      return uri.GetLeftPart(UriPartial.Authority);
    }

    /// <summary>Determines whether the given url is a relative one.</summary>
    /// <param name="url">The URL.</param>
    /// <returns>True if the url is relative otherwise false.</returns>
    internal static bool IsRelativeUrl(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return false;
      return url.StartsWith("/") || url.StartsWith("%2f", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Adds the application virtual path.</summary>
    /// <param name="path">The path.</param>
    /// <returns>Real URL with application virtual path.</returns>
    public static string AddAppVirtualPath(string path)
    {
      if (string.IsNullOrEmpty(HostingEnvironment.ApplicationVirtualPath))
        return path;
      return HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + path;
    }
  }
}
