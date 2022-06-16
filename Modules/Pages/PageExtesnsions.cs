// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageExtesnsions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Extension methods for pages</summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Ignored so that the file can be included in StyleCop")]
  public static class PageExtesnsions
  {
    /// <summary>
    /// Gets the URL of the page using the current frontend root and the current culture.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <returns>The URL of the page.</returns>
    public static string GetUrl(this PageNode page) => page.GetLiveUrl(resolve: false);

    /// <summary>
    /// Gets the URL of the page using the current frontend root and the specified culture.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The URL of the page.</returns>
    public static string GetUrl(this PageNode page, CultureInfo culture) => page.GetLiveUrl(culture, false);

    /// <summary>
    /// Gets the URL of the page using the specified site root and the current culture.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="rootName">Name of the root.</param>
    /// <returns>The URL of the page.</returns>
    public static string GetUrl(this PageNode page, string rootName) => page.GetUrl(rootName, (CultureInfo) null, false);

    /// <summary>
    /// Gets the URL of the page using the specified site root and the specified culture.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="rootName">Name of the root.</param>
    /// <param name="culture">The culture to retrieve the URL for.</param>
    /// <param name="fallbackToAnyLanguage">The fallback to any language.</param>
    /// <returns>The URL of the page.</returns>
    public static string GetUrl(
      this PageNode page,
      string rootName,
      CultureInfo culture,
      bool fallbackToAnyLanguage)
    {
      return page.GetLiveUrl(culture, false);
    }

    /// <summary>Gets the full URL of the page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>The full URL of the page.</returns>
    public static string GetFullUrl(this PageNode pageNode) => pageNode.GetFullUrl((CultureInfo) null, false);

    /// <summary>Gets the full URL of the page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">The fallback to any language.</param>
    /// <returns>The full URL of the page.</returns>
    public static string GetFullUrl(
      this PageNode pageNode,
      CultureInfo culture,
      bool fallbackToAnyLanguage)
    {
      bool localizeUrl = false;
      if (culture != null && !pageNode.IsBackend && SystemManager.CurrentContext.AppSettings.Multilingual)
        localizeUrl = true;
      return pageNode.GetFullUrl(culture, fallbackToAnyLanguage, localizeUrl);
    }

    /// <summary>Gets the full URL of the page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">The fallback to any language.</param>
    /// <param name="localizeUrl">Resolves the URL using the appropriate localization strategy.</param>
    /// <returns>The full URL of the page.</returns>
    public static string GetFullUrl(
      this PageNode pageNode,
      CultureInfo culture,
      bool fallbackToAnyLanguage,
      bool localizeUrl)
    {
      return pageNode.GetFullUrlIgnoreCache(culture, fallbackToAnyLanguage, localizeUrl);
    }

    /// <summary>Gets the full URL ignore cache.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">The fallback to any language.</param>
    /// <param name="localizeUrl">Resolves the URL using the appropriate localization strategy.</param>
    /// <returns>The full URL ignore cache.</returns>
    public static string GetFullUrlIgnoreCache(
      this PageNode pageNode,
      CultureInfo culture,
      bool fallbackToAnyLanguage,
      bool localizeUrl)
    {
      return pageNode.GetLiveUrl(culture, localizeUrl);
    }

    /// <summary>
    /// Gets the full path of the page replacing the / character with the given separator.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="separator">The separator that will be used when generating a path.</param>
    /// <param name="fallbackToAnyLanguage">The fallback to any language.</param>
    /// <returns>The full path of the page.</returns>
    public static string GetFullPath(
      this PageNode pageNode,
      string separator,
      bool fallbackToAnyLanguage = false)
    {
      if (SiteMapBase.GetCurrentProvider() == null)
        throw new InvalidOperationException("Cannot determine current site map provider.");
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      PageSiteNode siteMapNode = PageExtensions.GetSiteMapNode(pageNode, out SiteMapBase _, culture);
      if (siteMapNode == null)
        return string.Empty;
      List<string> list = ((IEnumerable<string>) siteMapNode.GetUrl(culture, fallbackToAnyLanguage, false).TrimStart('~').Split(new char[1]
      {
        '/'
      }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
      return string.Join(separator, list.ToArray());
    }

    /// <summary>Gets the full titles path for the given page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="separator">The separator.</param>
    /// <returns>The full titles path.</returns>
    public static string GetFullTitlesPath(this PageNode pageNode, string separator)
    {
      if (SiteMapBase.GetCurrentProvider() == null)
        throw new InvalidOperationException("Cannot determine current site map provider.");
      CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
      PageSiteNode siteMapNode = PageExtensions.GetSiteMapNode(pageNode, out SiteMapBase _, culture);
      return siteMapNode != null ? siteMapNode.GetTitlesPath(separator, culture) : string.Empty;
    }

    /// <summary>Gets the full titles for the given page.</summary>
    /// <param name="siteNode">The page site node.</param>
    /// <returns>The full titles path.</returns>
    public static IEnumerable<string> GetFullTitles(this PageSiteNode siteNode)
    {
      IEnumerable<string> fullTitles = (IEnumerable<string>) new string[0];
      if (siteNode != null)
      {
        CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
        fullTitles = siteNode.GetTitlesHierarchy(culture, false).Reverse<string>();
      }
      return fullTitles;
    }

    /// <summary>Gets the linked node of a page node.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>The linked node.</returns>
    public static PageNode GetLinkedNode(this PageNode pageNode)
    {
      if (!pageNode.HasLinkedNode())
        return (PageNode) null;
      if (pageNode.LinkedNodeId == Guid.Empty)
        throw new ArgumentException(Res.Get<ErrorMessages>().LinkedNodeIdNotSet);
      PageManager manager = PageManager.GetManager(pageNode.LinkedNodeProvider);
      Guid linkedNodeId = pageNode.LinkedNodeId;
      return manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == linkedNodeId)).SingleOrDefault<PageNode>();
    }

    /// <summary>Sets the linked node of a page node.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="linkedNode">The linked node.</param>
    public static void SetLinkedNode(this PageNode pageNode, PageNode linkedNode)
    {
      pageNode.LinkedNodeId = linkedNode.Id;
      pageNode.LinkedNodeProvider = ((DataProviderBase) ((IDataItem) linkedNode).Provider).Name;
    }

    /// <summary>Gets the terminal linked node of the page node.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>The terminal linked node.</returns>
    public static PageNode GetTerminalLinkedNode(this PageNode pageNode)
    {
      PageNode linkedNode = pageNode.GetLinkedNode();
      while (linkedNode != null && linkedNode.HasLinkedNode())
        linkedNode = linkedNode.GetLinkedNode();
      return linkedNode;
    }

    /// <summary>
    /// Determines whether the specified page node links another page node.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>A value indicating whether the specified page node links another page node.</returns>
    public static bool HasLinkedNode(this PageNode pageNode) => pageNode.NodeType == NodeType.Rewriting || pageNode.NodeType == NodeType.InnerRedirect;

    /// <summary>
    /// Gets the personalized page data for the given page node and segment.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="segmentId">The segment id.</param>
    /// <returns>The personalized page data.</returns>
    public static PageData GetPersonalizedPage(this PageNode node, Guid segmentId)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      if (segmentId == Guid.Empty)
        return node.GetPageData();
      PageManager manager = PageManager.GetManager();
      Guid pageDataId = node.GetPageData().Id;
      return manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageDataId && p.PersonalizationSegmentId == segmentId)).SingleOrDefault<PageData>() ?? node.GetPageData();
    }

    internal static string GetStandardPageStatusText(
      this PageNode node,
      PageData data,
      out string status,
      CultureInfo culture = null)
    {
      status = (string) node.ApprovalWorkflowState;
      string localizedStatus = node.GetLocalizedStatus();
      if (data != null)
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) data, culture, ref status, ref localizedStatus);
      return localizedStatus;
    }
  }
}
