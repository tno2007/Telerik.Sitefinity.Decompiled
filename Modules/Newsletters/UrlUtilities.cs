// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.UrlUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>Provides utility methods for working with urls.</summary>
  public static class UrlUtilities
  {
    /// <summary>
    /// Returns a site relative HTTP path from a partial path starting out with a ~.
    /// Same syntax that ASP.Net internally supports but this method can be used
    /// outside of the Page framework.
    /// 
    /// Works like Control.ResolveUrl including support for ~ syntax
    /// but returns an absolute URL.
    /// </summary>
    /// <param name="originalUrl">Any Url including those starting with ~</param>
    /// <returns>relative url</returns>
    public static string ResolveUrl(string originalUrl)
    {
      if (originalUrl == null)
        return (string) null;
      if (originalUrl.IndexOf("://") != -1 || !originalUrl.StartsWith("~"))
        return originalUrl;
      if (SystemManager.CurrentHttpContext == null)
        throw new ArgumentException("Invalid URL: Relative URL not allowed.");
      string applicationPath = SystemManager.CurrentHttpContext.Request.ApplicationPath;
      if (!applicationPath.EndsWith("/"))
        applicationPath += "/";
      return applicationPath + originalUrl.Substring(2);
    }

    /// <summary>
    /// This method returns a fully qualified absolute server Url which includes
    /// the protocol, server, port in addition to the server relative Url.
    /// 
    /// Works like Control.ResolveUrl including support for ~ syntax
    /// but returns an absolute URL.
    /// </summary>
    /// <param name="ServerUrl">Any Url, either App relative or fully qualified</param>
    /// <param name="forceHttps">if true forces the url to use https</param>
    /// <returns></returns>
    public static string ResolveServerUrl(string serverUrl, bool forceHttps)
    {
      if (serverUrl.IndexOf("://") > -1)
        return serverUrl;
      string str = UrlUtilities.ResolveUrl(serverUrl);
      Uri url = SystemManager.CurrentHttpContext.Request.Url;
      return (forceHttps ? "https" : url.Scheme) + "://" + url.Authority + str;
    }

    /// <summary>
    /// This method returns a fully qualified absolute server Url which includes
    /// the protocol, server, port in addition to the server relative Url.
    /// 
    /// It work like Page.ResolveUrl, but adds these to the beginning.
    /// This method is useful for generating Urls for AJAX methods
    /// </summary>
    /// <param name="ServerUrl">Any Url, either App relative or fully qualified</param>
    /// <returns></returns>
    public static string ResolveServerUrl(string serverUrl) => UrlUtilities.ResolveServerUrl(serverUrl, false);
  }
}
