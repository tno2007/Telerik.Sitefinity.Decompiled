// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SiteMapProviderResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Defines methods for resolving SiteMapProvider from URL, http context context or Sitefinity page node.
  /// Can be replaced in the ObjectFactory.Container in order to plug custom SiteMapProvider logic.
  /// </summary>
  public class SiteMapProviderResolver
  {
    private static readonly string backendServicesPath = BackendRoute.BackendRootPath + "Services/";
    private static readonly string publicServicesPath = BackendRoute.BackendRootPath + "Public/";
    private static readonly string frontEndServicesPath = BackendRoute.BackendRootPath + "Frontend/";

    /// <summary>Gets the site map provider for request.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public SiteMapProvider GetSiteMapProviderForRequest(HttpContextBase context = null)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      return this.GetSiteMapProviderForAppRelativeUrl(context.Request.AppRelativeCurrentExecutionFilePath);
    }

    /// <summary>Gets the site map provider for URL.</summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    public SiteMapProvider GetSiteMapProviderForUrl(string url)
    {
      url = VirtualPathUtility.ToAppRelative(url);
      return this.GetSiteMapProviderForAppRelativeUrl(url);
    }

    /// <summary>Gets the site map provider for page node.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    public virtual SiteMapProvider GetSiteMapProviderForPageNode(PageNode node) => node.IsBackend ? (SiteMapProvider) BackendSiteMap.GetCurrentProvider() : SitefinitySiteMap.GetCurrentProvider();

    /// <summary>Gets the site map provider for app relative URL.</summary>
    /// <param name="url">The URL. Should be application relative url, e.g. '~/home-page'</param>
    /// <returns></returns>
    protected virtual SiteMapProvider GetSiteMapProviderForAppRelativeUrl(string url) => url.StartsWith(BackendRoute.BackendRootPath, StringComparison.OrdinalIgnoreCase) && !url.StartsWith(SiteMapProviderResolver.backendServicesPath, StringComparison.OrdinalIgnoreCase) && !url.StartsWith(SiteMapProviderResolver.publicServicesPath, StringComparison.OrdinalIgnoreCase) && !url.StartsWith(SiteMapProviderResolver.frontEndServicesPath, StringComparison.OrdinalIgnoreCase) ? (SiteMapProvider) BackendSiteMap.GetCurrentProvider() : SitefinitySiteMap.GetCurrentProvider();

    internal static SiteMapProviderResolver Current => ObjectFactory.Resolve<SiteMapProviderResolver>();
  }
}
