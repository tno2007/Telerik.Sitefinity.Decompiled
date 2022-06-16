// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.ContentLocations
{
  internal static class ContentLocationExtensions
  {
    /// <summary>Gets the URL for a given content item.</summary>
    /// <param name="contentLocation">The content location.</param>
    /// <param name="item">The content item.</param>
    /// <returns></returns>
    public static string GetUrl(this IContentLocation contentLocation, object item = null) => contentLocation.GetUrl(item, false);

    /// <summary>Gets the URL for a given content item.</summary>
    /// <param name="contentLocation">The content location.</param>
    /// <param name="item">The content item.</param>
    /// <param name="getRelativeUri">The content item.</param>
    /// <returns></returns>
    public static string GetUrl(
      this IContentLocation contentLocation,
      object item,
      bool getRelativeUri)
    {
      SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
      using (SiteRegion.FromSiteId(contentLocation.SiteId, SiteContextResolutionTypes.ByParam))
      {
        using (new CultureRegion(contentLocation.Culture, (CultureInfo) null))
        {
          Guid pageId = contentLocation.PageId;
          SiteMapNode node;
          if (currentProvider is SiteMapBase)
          {
            SiteMapBase siteMapBase = (SiteMapBase) currentProvider;
            node = siteMapBase.FindSiteMapNodeFromKey(pageId.ToString(), false);
            if (node != null)
              node = siteMapBase.FindSiteMapNodeForSpecificLanguage(node, contentLocation.Culture, false);
          }
          else
            node = currentProvider.FindSiteMapNodeFromKey(pageId.ToString());
          if (node != null)
          {
            string url = !(node is PageSiteNode pageSiteNode) || item == null || contentLocation.IsSingleItem ? node.Url : VirtualPathUtility.RemoveTrailingSlash(pageSiteNode.UrlWithoutExtension) + ContentLocationExtensions.GetItemUrl(item);
            return getRelativeUri ? url : UrlPath.ResolveUrlForCurrentSite(url, false);
          }
        }
      }
      return string.Empty;
    }

    private static string GetItemUrl(object dataItem)
    {
      string itemUrl;
      if (dataItem is ISimpleLocatable simpleLocatable)
      {
        itemUrl = simpleLocatable.ItemUrl;
      }
      else
      {
        IManager manager;
        if (ManagerBase.TryGetMappedManager(dataItem.GetType(), string.Empty, out manager) && manager is IContentLocatableManager)
          return ((IContentLocatableManager) manager).GetItemUrl(dataItem);
        if (!(dataItem is ILocatable locatable))
          return string.Empty;
        UrlDataProviderBase provider;
        if (dataItem is IDataItem dataItem1 && dataItem1.Provider != null)
        {
          provider = (UrlDataProviderBase) dataItem1.Provider;
        }
        else
        {
          if (manager == null)
            manager = ManagerBase.GetMappedManager(locatable.GetType());
          provider = (UrlDataProviderBase) manager.Provider;
        }
        itemUrl = provider.GetItemUrl(locatable);
      }
      return itemUrl;
    }

    internal static string GetTitle(this IContentLocation contentLocation)
    {
      if (contentLocation != null)
      {
        using (SiteRegion.FromSiteId(contentLocation.SiteId))
        {
          SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(contentLocation.PageId.ToString());
          if (siteMapNodeFromKey != null)
            return siteMapNodeFromKey.Title;
        }
      }
      return string.Empty;
    }

    /// <summary>
    /// Gets the IsAccessible content location for a given content item.
    /// </summary>
    /// <param name="contentLocation">The content location.</param>
    /// <returns></returns>
    public static bool IsAccessible(
      this IContentLocationBase contentLocation,
      bool shouldBeAccessibleForEveryone = false)
    {
      if (contentLocation != null)
      {
        using (SiteRegion.FromSiteId(contentLocation.SiteId))
        {
          if (SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(contentLocation.PageId.ToString()) is PageSiteNode siteMapNodeFromKey)
          {
            if (siteMapNodeFromKey.Visible)
              return !shouldBeAccessibleForEveryone || siteMapNodeFromKey.IsAccessibleForEveryone;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Gets the IsAccessible content location for a given content item for the current user.
    /// </summary>
    /// <param name="contentLocation">The content location.</param>
    /// <returns></returns>
    public static bool IsAccessibleForCurrentUser(this IContentLocationBase contentLocation)
    {
      if (contentLocation != null)
      {
        using (SiteRegion.FromSiteId(contentLocation.SiteId))
        {
          if (SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(contentLocation.PageId.ToString()) is PageSiteNode siteMapNodeFromKey)
          {
            if (siteMapNodeFromKey.Visible)
              return siteMapNodeFromKey.IsGranted("View");
          }
        }
      }
      return false;
    }
  }
}
