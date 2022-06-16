// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  public static class NavigationUtilities
  {
    private static bool? generateAbsoluteUrls;

    public static int Depth(this SiteMapNode node)
    {
      if (node == null)
        return -1;
      return node.ParentNode == null ? 0 : 1 + node.ParentNode.Depth();
    }

    public static SiteMapNode FindNode(SiteMapNode curNode, string nodeUrl)
    {
      if (curNode.Url == nodeUrl)
        return curNode;
      foreach (SiteMapNode childNode in curNode.ChildNodes)
      {
        SiteMapNode node = NavigationUtilities.FindNode(childNode, nodeUrl);
        if (node != null)
          return node;
      }
      return (SiteMapNode) null;
    }

    public static int RootDepth(this SiteMapDataSource sitemap) => !string.IsNullOrEmpty(sitemap.StartingNodeUrl) ? sitemap.Provider.FindSiteMapNode(sitemap.StartingNodeUrl).Depth() : 0;

    public static void SetNavigationItemTarget(NavigationItem item) => item.Target = NavigationUtilities.GetLinkTargetInternal((object) item);

    public static string GetLinkTarget(object item) => NavigationUtilities.GetLinkTargetInternal(item);

    /// <summary>
    /// Gets the related item link of the page node represented by the specified <see cref="T:System.Web.SiteMapNode" /> and specified field.
    /// </summary>
    /// <param name="item">The SiteMapNode item.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static string GetRelatedDataItemLink(object item, string fieldName)
    {
      IDataItem relatedItem = NavigationUtilities.GetRelatedItem<IDataItem>(item, fieldName);
      if (relatedItem != null)
      {
        IContentItemLocation itemDefaultLocation = SystemManager.GetContentLocationService().GetItemDefaultLocation(relatedItem);
        if (itemDefaultLocation != null)
          return itemDefaultLocation.ItemAbsoluteUrl;
      }
      return string.Empty;
    }

    /// <summary>
    /// Gets the related media item (document, video or image) link of the page node represented by the specified <see cref="T:System.Web.SiteMapNode" /> and specified field.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="thumbnail">The thumbnail.</param>
    /// <param name="resolveAsAbsoluteUrl">The resolve as absolute URL.</param>
    /// <returns></returns>
    public static string GetRelatedMediaLink(
      object item,
      string fieldName,
      string thumbnail = null,
      bool resolveAsAbsoluteUrl = false)
    {
      MediaContent relatedItem = NavigationUtilities.GetRelatedItem<MediaContent>(item, fieldName);
      if (relatedItem == null)
        return string.Empty;
      return thumbnail != null ? relatedItem.ResolveThumbnailUrl(thumbnail, resolveAsAbsoluteUrl) : relatedItem.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
    }

    /// <summary>Gets the related item.</summary>
    /// <typeparam name="TItem">The type of the T item.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    internal static TItem GetRelatedItem<TItem>(object item, string fieldName, int index = 0)
    {
      if (!(item is PageSiteNode node))
        return default (TItem);
      int? totalCount = new int?();
      return Queryable.FirstOrDefault<TItem>(node.GetRelatedItems<TItem>(fieldName, (string) null, (string) null, new int?(index), new int?(1), ref totalCount));
    }

    public static void ExpandPredecessorsOfSelectedNode(
      this IExpandableSiteMapControl control,
      RadTreeNode treeNode)
    {
      if (!(((SiteMapNode) treeNode.DataItem).Url == control.CurrentPageURL) || !treeNode.Selected || treeNode.Expanded)
        return;
      for (RadTreeNode radTreeNode = treeNode; radTreeNode != null; radTreeNode = radTreeNode.ParentNode)
        radTreeNode.Expanded = true;
    }

    public static void SetSelectedItem(
      this IExpandableSiteMapControl control,
      SiteMapNode node,
      Action performSelection)
    {
      if (!(node.Url == control.CurrentPageURL))
        return;
      if (node is PageSiteNode)
      {
        if ((node as PageSiteNode).NodeType == NodeType.InnerRedirect)
          return;
        performSelection();
      }
      else
        performSelection();
    }

    /// <summary>
    /// Suitable for binding to page site node item that will resolve seo friendly url.
    /// </summary>
    /// <param name="item">SitemapNode or PageSiteNode.</param>
    /// <returns></returns>
    public static string ResolveUrl(object item) => NavigationUtilities.ResolveUrl(item as SiteMapNode, new bool?(), (string[]) null);

    /// <summary>
    /// Suitable for binding to page site node item that will resolve seo friendly url.
    /// </summary>
    /// <param name="item">SitemapNode or PageSiteNode.</param>
    /// <param name="resolveAsAbsoluteUrl">Whether to resolve the Url as absolute or relative path.</param>
    /// <returns></returns>
    public static string ResolveUrl(object item, bool? resolveAsAbsoluteUrl) => NavigationUtilities.ResolveUrl(item as SiteMapNode, resolveAsAbsoluteUrl, (string[]) null);

    /// <summary>Resolves SEO-friendly Url for client side usage.</summary>
    /// <param name="siteMapNode">The site map node.</param>
    /// <param name="resolveAsAbsoluteUrl">Whether to resolve the Url as absolute or relative path.</param>
    /// <param name="segments">The url segments.</param>
    /// <returns></returns>
    internal static string ResolveUrl(
      SiteMapNode siteMapNode,
      bool? resolveAsAbsoluteUrl = null,
      string[] segments = null)
    {
      string str = string.Empty;
      bool flag = false;
      if (siteMapNode != null)
      {
        string url;
        if (siteMapNode is PageSiteNode node)
        {
          flag = node.IsBackend;
          url = !node.IsHomePage() || segments != null && segments.Length != 0 ? node.Url : SystemManager.CurrentContext.ResolveUrl("~/");
        }
        else
          url = siteMapNode.Url;
        if (!resolveAsAbsoluteUrl.HasValue)
          resolveAsAbsoluteUrl = new bool?(!flag && NavigationUtilities.GenerateAbsoluteUrls);
        str = UrlPath.ResolveUrl(url, resolveAsAbsoluteUrl.Value, true);
      }
      return str;
    }

    private static string GetLinkTargetInternal(object item)
    {
      string empty = string.Empty;
      if (item is ISitefinitySiteMapNode sitefinitySiteMapNode)
      {
        string[] values = sitefinitySiteMapNode.Attributes.GetValues("target");
        if (values != null && values.Length == 1)
          empty = values[0];
      }
      return empty;
    }

    private static bool GenerateAbsoluteUrls
    {
      get
      {
        if (!NavigationUtilities.generateAbsoluteUrls.HasValue)
          NavigationUtilities.generateAbsoluteUrls = new bool?(Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls);
        return NavigationUtilities.generateAbsoluteUrls.Value;
      }
    }

    internal static void InvalidateCachedSettings() => NavigationUtilities.generateAbsoluteUrls = new bool?();
  }
}
