// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Provides the ability to easily get the page node's url, culture, translations, status.
  /// </summary>
  internal static class PageExtensions
  {
    /// <summary>Gets the live URL of the specified page.</summary>
    /// <param name="page">The page.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="resolve">Specify whether the returned url should be resolved based on the current context.</param>
    /// <returns>The live URL.</returns>
    internal static string GetLiveUrl(this PageNode page, CultureInfo culture = null, bool resolve = true) => PageExtensions.GetUrl(page, false, (string) null, culture, resolve);

    /// <summary>Gets the live URL of the specified page.</summary>
    /// <param name="page">The page.</param>
    /// <param name="urlEnd">The url string that will be appended to the generated page url.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="resolve">Specify whether the returned url should be resolved based on the current context.</param>
    /// <returns>The live URL</returns>
    internal static string GetLiveUrl(
      this PageNode page,
      string urlEnd,
      CultureInfo culture = null,
      bool resolve = true)
    {
      return PageExtensions.GetUrl(page, false, urlEnd, culture, resolve);
    }

    /// <summary>Gets the backend URL.</summary>
    /// <param name="page">The page.</param>
    /// <param name="actionName">Name of the action.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The backend URL.</returns>
    internal static string GetBackendUrl(
      this PageNode page,
      string actionName,
      CultureInfo culture = null)
    {
      return PageExtensions.GetUrl(page, true, actionName, culture, false);
    }

    /// <summary>Gets the site map node.</summary>
    /// <param name="page">The page.</param>
    /// <param name="sitemapProvider">The sitemap provider.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The site map node.</returns>
    internal static PageSiteNode GetSiteMapNode(
      PageNode page,
      out SiteMapBase sitemapProvider,
      CultureInfo culture = null)
    {
      if (page.Id != Guid.Empty)
      {
        sitemapProvider = (SiteMapBase) SiteMapBase.GetSiteMapProviderForPageNode(page);
        if (sitemapProvider == null)
          return (PageSiteNode) null;
        if (sitemapProvider.FindSiteMapNodeFromKey(page.Id.ToString(), false) is PageSiteNode siteMapNodeFromKey)
          return sitemapProvider.FindSiteMapNodeForSpecificLanguage((SiteMapNode) siteMapNodeFromKey, culture) as PageSiteNode;
      }
      else
        sitemapProvider = (SiteMapBase) null;
      return (PageSiteNode) null;
    }

    internal static PageSiteNode GetSiteMapNode(this PageNode page)
    {
      using (SiteRegion.FromSiteMapRoot(page.RootNodeId == Guid.Empty ? page.Id : page.RootNodeId))
        return PageExtensions.GetSiteMapNode(page, out SiteMapBase _);
    }

    /// <summary>
    /// Determines the default node url traversing the node structure.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="skipExtension">The skip extension.</param>
    /// <returns>The default node url.</returns>
    internal static string BuildUrl(
      this PageNode pageNode,
      CultureInfo culture,
      bool skipExtension = false)
    {
      PageNode pageNode1 = pageNode;
      List<string> stringList = new List<string>();
      bool flag1 = false;
      bool flag2 = false;
      while (pageNode1 != null)
      {
        if (pageNode1.RenderAsLink || !flag2 && pageNode1.Parent != null)
        {
          string lstringValue = PageExtensions.GetLstringValue(pageNode1.UrlName, culture, false);
          if (!string.IsNullOrEmpty(lstringValue))
          {
            stringList.Add(lstringValue.Trim('/'));
            if (lstringValue.StartsWith("~"))
            {
              flag1 = true;
              break;
            }
          }
        }
        pageNode1 = pageNode1.Parent;
        flag2 = true;
      }
      if (stringList.Count == 0)
        return "~/";
      if (!flag1)
        stringList.Add("~");
      stringList.Reverse();
      string str = string.Join("/", stringList.ToArray());
      if (!skipExtension)
        str += PageExtensions.GetLstringValue(pageNode.Extension, culture, false);
      return str;
    }

    /// <summary>Gets the node structure using all ancestor titles</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="includeRootNode">Include root node.</param>
    /// <returns>The node structure using all ancestor titles.</returns>
    internal static string BuildFullTitlesPath(
      this PageNode pageNode,
      string culture,
      string separator = ">",
      bool includeRootNode = false)
    {
      CultureInfo culture1 = (CultureInfo) null;
      if (culture != null)
        culture1 = CultureInfo.GetCultureInfo(culture);
      return pageNode.BuildFullTitlesPath(culture1, separator, includeRootNode);
    }

    /// <summary>Gets the node structure using all ancestor titles</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="includeRootNode">The include root node.</param>
    /// <returns>The node structure using all ancestor titles.</returns>
    internal static string BuildFullTitlesPath(
      this PageNode pageNode,
      CultureInfo culture,
      string separator = ">",
      bool includeRootNode = false)
    {
      if (pageNode == null || !includeRootNode && pageNode.Parent == null)
        return string.Empty;
      Stack<string> values = new Stack<string>();
      for (PageNode pageNode1 = pageNode; pageNode1 != null && (includeRootNode || pageNode1.Parent != null); pageNode1 = pageNode1.Parent)
      {
        string lstringValue = PageExtensions.GetLstringValue(pageNode1.Title, culture, true);
        if (!string.IsNullOrEmpty(lstringValue))
          values.Push(lstringValue);
      }
      return string.Join(separator, (IEnumerable<string>) values);
    }

    /// <summary>
    /// Determines whether the specified page node has custom URL.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A value indicating whether the specified page node has custom URL.</returns>
    internal static bool HasCustomUrl(this PageNode node, CultureInfo culture = null)
    {
      string str = culture == null ? (string) node.UrlName : node.UrlName.GetString(culture, true);
      return str != null && str.StartsWith("~/");
    }

    /// <summary>
    /// Determines whether the specified page node is published on the given culture.
    /// </summary>
    /// <param name="page">The node.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A value indicating whether the specified page node is published on the given culture.</returns>
    internal static bool IsPublished(this PageNode page, CultureInfo culture = null)
    {
      if (!(page.RootNodeId != Guid.Empty))
        return false;
      using (SiteRegion.FromSiteMapRoot(page.RootNodeId))
      {
        culture = culture.GetSitefinityCulture();
        bool flag = page.IsBackend ? ((IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 : SystemManager.CurrentContext.AppSettings.Multilingual;
        PageData pageData = page.GetPageData(culture);
        if (pageData != null)
        {
          if (!page.IsBackend & flag && culture != null)
          {
            if (pageData.PublishedTranslations.Count > 0)
              return pageData.PublishedTranslations.Contains(culture.GetLanguageKey());
            CultureInfo cultureInfo = page.IsBackend ? SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
            if (!culture.Equals((object) cultureInfo))
              return false;
          }
          return pageData.Visible && pageData.Version > 0;
        }
        return !flag || ((IEnumerable<string>) page.AvailableLanguages).Contains<string>(culture.Name);
      }
    }

    /// <summary>
    /// Determines whether the specified page node is deleted on the given culture.
    /// </summary>
    /// <param name="page">The node.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A value indicating whether the specified page node is deleted on the given culture.</returns>
    internal static bool IsDeleted(this PageNode page, CultureInfo culture = null)
    {
      if (page == null || page.IsDeleted)
        return true;
      culture = culture.GetSitefinityCulture();
      return SystemManager.CurrentContext.AppSettings.Multilingual && !((IEnumerable<string>) page.AvailableLanguages).Contains<string>(culture.Name);
    }

    /// <summary>Gets the related items.</summary>
    /// <typeparam name="TItem">The type of the T item.</typeparam>
    /// <param name="node">The node.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>The related items.</returns>
    public static IQueryable<TItem> GetRelatedItems<TItem>(
      this PageSiteNode node,
      string fieldName,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      return PageExtensions.GetRelatedItems<TItem>(node.Id, ((IDataItem) node).GetProviderName(), fieldName, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Determines whether this instance [can have translation siblings] the specified page node.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns>A value indicating whether the page can have translation siblings.</returns>
    internal static bool CanHaveTranslationSiblings(this PageNode pageNode)
    {
      PageNode parent = pageNode.Parent;
      return LocalizationHelper.SplitableNodeTypes.Contains(parent.NodeType);
    }

    /// <summary>
    /// Returns the extension from a given virtual path with the leading dot
    /// <example>~/segment1.ext1.ext2 returns .ext1.ext2</example>
    /// <example>~/segment1.ext1/segment2.ext2 returns .ext2</example>
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns>The extension if such is available, or string.Empty if not</returns>
    internal static string GetVirtualPathExtension(string virtualPath)
    {
      if (string.IsNullOrEmpty(virtualPath))
        return string.Empty;
      int num = virtualPath[0] == '~' ? virtualPath.LastIndexOf(".") : throw new ArgumentException("The specified path is not virtual.", nameof (virtualPath));
      if (num < 1)
        return string.Empty;
      int startIndex1 = virtualPath.LastIndexOf("/");
      if (num <= startIndex1)
        return string.Empty;
      int startIndex2 = virtualPath.IndexOf(".", startIndex1);
      return startIndex2 == virtualPath.Length - 1 ? string.Empty : virtualPath.Substring(startIndex2);
    }

    /// <summary>Gets the available cultures.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="site">The site.</param>
    /// <returns>The available cultures.</returns>
    internal static IEnumerable<CultureInfo> GetAvailableCultures(
      this PageData pageData,
      ISite site = null)
    {
      if (site == null)
      {
        PageNode navigationNode = pageData.NavigationNode;
        if (navigationNode != null)
          site = PageManager.GetSite(navigationNode);
        if (site == null)
          site = SystemManager.CurrentContext.CurrentSite;
      }
      List<CultureInfo> availableCultures = new List<CultureInfo>();
      if (pageData.LocalizationStrategy == LocalizationStrategy.Split)
      {
        if (!string.IsNullOrEmpty(pageData.Culture))
          availableCultures.Add(CultureInfo.GetCultureInfo(pageData.Culture));
        else
          availableCultures.Add(site.DefaultCulture);
      }
      else
      {
        IEnumerable<LanguageData> source = pageData.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (l => !string.IsNullOrEmpty(l.Language)));
        if (source.Any<LanguageData>())
        {
          foreach (CultureInfo cultureInfo in source.Select<LanguageData, CultureInfo>((Func<LanguageData, CultureInfo>) (l => CultureInfo.GetCultureInfo(l.Language))))
          {
            if (!availableCultures.Contains(cultureInfo))
              availableCultures.Add(cultureInfo);
          }
        }
        else
          availableCultures.Add(site.DefaultCulture);
      }
      return (IEnumerable<CultureInfo>) availableCultures;
    }

    internal static void RemoveAssociatedPageDataObjects(this PageNode node, PageManager manager)
    {
      foreach (PageData pageData in new List<PageData>((IEnumerable<PageData>) node.PageDataList))
      {
        pageData.NavigationNode = (PageNode) null;
        manager.Delete(pageData);
      }
      if (node.Page == null)
        return;
      node.Page = (PageData) null;
    }

    private static string GetUrl(
      PageNode page,
      bool backend,
      string paramString,
      CultureInfo culture,
      bool resolve)
    {
      if (!(page.RootNodeId != Guid.Empty))
        return "~/";
      using (SiteRegion.FromSiteMapRoot(page.RootNodeId))
      {
        using (new CultureRegion(culture))
        {
          PageSiteNode siteMapNode = PageExtensions.GetSiteMapNode(page, out SiteMapBase _, culture);
          if (siteMapNode != null)
            return backend ? siteMapNode.GetBackendUrl(paramString) : siteMapNode.GetLiveUrl(paramString, resolve);
          string url;
          if (!string.IsNullOrEmpty(paramString))
          {
            if (!paramString.StartsWith("/"))
              paramString = "/" + paramString;
            if (backend)
              paramString = "/" + "Action" + paramString;
            url = page.BuildUrl(culture, true) + paramString;
          }
          else
            url = page.BuildUrl(culture);
          return url;
        }
      }
    }

    private static string GetLstringValue(
      Lstring lstring,
      CultureInfo culture,
      bool fallbackToAnyLanguage)
    {
      if (lstring != (Lstring) null)
      {
        string lstringValue = culture == null ? lstring.Value : lstring.GetString(culture, fallbackToAnyLanguage);
        if (!string.IsNullOrEmpty(lstringValue))
          return lstringValue;
        if (fallbackToAnyLanguage)
          return lstring.GetStringAnyLanguage(out CultureInfo _);
      }
      return string.Empty;
    }

    internal static IQueryable<TItem> GetRelatedItems<TItem>(
      Guid pageId,
      string pageProvider,
      string fieldName,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      return Queryable.Cast<TItem>(RelatedDataHelper.GetRelatedItems(typeof (PageNode).FullName, pageProvider, pageId, fieldName, new ContentLifecycleStatus?(ContentLifecycleStatus.Live), filterExpression, orderExpression, skip, take, ref totalCount));
    }
  }
}
