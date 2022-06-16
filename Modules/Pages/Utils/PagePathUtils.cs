// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Utils.PagePathUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Utils
{
  /// <summary>
  /// Contains methods for sitefinity pages <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> url resolution.
  /// </summary>
  internal static class PagePathUtils
  {
    /// <summary>
    /// Resolves the url for a given PageNode from the backend or returns null if such url is not available.
    /// </summary>
    /// <param name="pageId">The id of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /></param>
    /// <returns>The absolute url to the backend page.</returns>
    public static string ResolveBackendPageNodeUrl(Guid pageId)
    {
      if (pageId == Guid.Empty)
        return (string) null;
      SiteMapNode siteMapNodeFromKey = BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageId.ToString());
      string str = (string) null;
      if (siteMapNodeFromKey != null)
        str = RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, UrlResolveOptions.Absolute);
      return str;
    }

    /// <summary>
    /// Returns the current backend home page url or ~/Sitefinity if such is not found.
    /// </summary>
    /// <returns>Url as string.</returns>
    public static string ResolveBackendHomePageUrl()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(Config.Get<PagesConfig>().BackendHomePageId, false);
      return siteMapNode == null ? RouteHelper.ResolveUrl("~/Sitefinity", UrlResolveOptions.Absolute) : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Absolute);
    }
  }
}
