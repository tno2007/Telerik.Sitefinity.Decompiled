// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.CanonicalUrlPageExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Enums;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides methods for manipulating the canonical URL link on a given page.
  /// </summary>
  public static class CanonicalUrlPageExtensions
  {
    private const string HrefAttributeKey = "href=";
    private const string RelTagAttribute = "rel";
    private const string CanonicalUrlLinkAttributeValue = "canonical";
    private const string PaginationPreviousLinkAttributeValue = "prev";
    private const string PaginationNextLinkAttributeValue = "next";
    /// <summary>
    /// The key of the context item that holds the canonical url
    /// </summary>
    private const string CanonicalUrlLocationContextItemKey = "sf-canonical-url";
    internal const string PaginationUrlsContextItemKey = "sf-pagination-prev";
    /// <summary>
    /// The key of the context item that indicates if the canonical url has been set externally
    /// </summary>
    private const string HasExternalCanonicalUrlContextItemKey = "sf-canonical-url-external";

    /// <summary>
    /// Retrieves canonical url for given page if canonical urls are enabled.
    /// </summary>
    /// <param name="page">The page control.</param>
    /// <param name="node">The page site node.</param>
    /// <returns></returns>
    public static string GetCanonicalUrlForPage(this Page page, PageSiteNode node)
    {
      ContentLocationsSettingsElement locationsSettings = Config.Get<SystemConfig>().ContentLocationsSettings;
      string canonicalUrlForPage = (string) null;
      if (!locationsSettings.DisableCanonicalUrls && !node.IsBackend)
      {
        List<string> allowedParameters = CanonicalUrlPageExtensions.GetAllowedParameters(locationsSettings.PageDefaultCanonicalUrlSettings);
        bool? defaultCanonicalUrl = node.EnableDefaultCanonicalUrl;
        if (defaultCanonicalUrl.HasValue)
        {
          defaultCanonicalUrl = node.EnableDefaultCanonicalUrl;
          if (defaultCanonicalUrl.Value)
            goto label_4;
        }
        defaultCanonicalUrl = node.EnableDefaultCanonicalUrl;
        if (defaultCanonicalUrl.HasValue || !locationsSettings.PageDefaultCanonicalUrlSettings.EnableDefaultPageCanonicalUrls)
          goto label_5;
label_4:
        canonicalUrlForPage = CanonicalUrlPageExtensions.GetDefaultCanonicalUrl(page, node, SystemManager.CurrentHttpContext.Request.RawUrl, (IEnumerable<string>) allowedParameters);
      }
label_5:
      return canonicalUrlForPage;
    }

    /// <summary>
    /// Adds a canonical and pagination URL links in the page tag when required
    /// (link rel="canonical", pagination - link rel="prev" and link rel="next").
    /// <para>Checks if an internal canonical URL has been set - for dynamic pages.</para>
    /// <para>If not - check for external canonical URLs - for static pages.</para>
    /// <para>If not - set canonical URL using the page URL.</para>
    /// </summary>
    /// <param name="page"></param>
    /// <param name="node"></param>
    internal static void SetCanonicalLinks(this Page page, PageSiteNode node)
    {
      ContentLocationsSettingsElement locationsSettings = Config.Get<SystemConfig>().ContentLocationsSettings;
      if (locationsSettings.DisableCanonicalUrls || node.IsBackend)
        return;
      object obj = page.Items[(object) "DisableCanonicalUrlMetaTag"];
      if (obj != null && (bool) obj && SystemManager.IsDetailsView())
        return;
      string canonicalUrl = (string) null;
      List<string> allowedParameters = CanonicalUrlPageExtensions.GetAllowedParameters(locationsSettings.PageDefaultCanonicalUrlSettings);
      if (!CanonicalUrlPageExtensions.TryGetInternalCanonicalUrl(page, out canonicalUrl) && !CanonicalUrlPageExtensions.HasExternalCanonicalUrl(page) && (node.EnableDefaultCanonicalUrl.HasValue && node.EnableDefaultCanonicalUrl.Value || !node.EnableDefaultCanonicalUrl.HasValue && locationsSettings.PageDefaultCanonicalUrlSettings.EnableDefaultPageCanonicalUrls))
        canonicalUrl = CanonicalUrlPageExtensions.GetDefaultCanonicalUrl(page, node, SystemManager.CurrentHttpContext.Request.RawUrl, (IEnumerable<string>) allowedParameters);
      if (!string.IsNullOrWhiteSpace(canonicalUrl))
        CanonicalUrlPageExtensions.AddUrlLink(page, "canonical", canonicalUrl);
      CanonicalUrlPageExtensions.SetPaginationLinks(page, allowedParameters);
    }

    /// <summary>
    /// Sets a value indicating if the canonical URL has been resolved externally.
    /// </summary>
    /// <param name="setExternally">If true indicates that the canonical URL is set externally.</param>
    internal static void SetCanonicalUrlResolvedExternally(this Page page, bool setExternally) => page.Items.Add((object) "sf-canonical-url-external", (object) setExternally);

    /// <summary>
    /// Tries to store the canonical URL in order to be set later on the given page.
    /// If a canonical URL has been set already - returns false. Otherwise true.
    /// </summary>
    /// <param name="page">The page on which the canonical URL will be set.</param>
    /// <param name="url">The canonical url.</param>
    /// <returns>If a canonical URL has been set already - returns false. Otherwise true.</returns>
    internal static bool TryStoreCanonicalUrl(this Page page, string url)
    {
      bool flag = false;
      if (!CanonicalUrlPageExtensions.TryGetInternalCanonicalUrl(page, out string _))
      {
        page.Items.Add((object) "sf-canonical-url", (object) url);
        flag = true;
      }
      return flag;
    }

    internal static bool TryStorePaginationUrls(this Page page, PaginationUrls paginationUrls)
    {
      bool flag = false;
      if (!CanonicalUrlPageExtensions.TryGetInternalPaginationUrls(page, out PaginationUrls _) && page != null)
      {
        page.Items.Add((object) "sf-pagination-prev", (object) paginationUrls);
        flag = true;
      }
      return flag;
    }

    /// <summary>
    /// Tries to parse the given html content and to extract the canonical URL.
    /// </summary>
    /// <param name="htmlContent">The HTML or part of it where the canonical URL should be present.</param>
    /// <param name="canonicalUrl">The canonical URL.</param>
    /// <returns>True if the parsing was successful otherwise false.</returns>
    internal static bool TryParse(string htmlContent, out string canonicalUrl)
    {
      bool flag = false;
      canonicalUrl = (string) null;
      try
      {
        string str1 = "rel=\"canonical\"";
        string str2 = "rel='canonical'";
        int length = htmlContent.IndexOf(str1, StringComparison.InvariantCultureIgnoreCase);
        if (length < 0)
          length = htmlContent.IndexOf(str2, StringComparison.InvariantCultureIgnoreCase);
        if (length < 0)
          return flag;
        int startIndex1 = htmlContent.Substring(0, length).LastIndexOf('<');
        if (startIndex1 < 0)
          return flag;
        int num1 = htmlContent.IndexOf('>', startIndex1);
        if (num1 < 0)
          return flag;
        string str3 = htmlContent.Substring(startIndex1, num1 - startIndex1 + 1);
        int num2 = str3.IndexOf("href=");
        if (num2 < 0 || num2 + "href=".Length + 2 >= str3.Length)
          return flag;
        int startIndex2 = num2 + "href=".Length;
        string str4 = str3.Substring(startIndex2, 1);
        int num3 = str3.IndexOf(str4, startIndex2 + 1);
        if (num3 < 0)
          return flag;
        canonicalUrl = str3.Substring(startIndex2 + 1, num3 - startIndex2 - 1);
        if (canonicalUrl.Length > 0)
          flag = true;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
      }
      return flag;
    }

    /// <summary>
    /// Gets the default canonical URL for the given page using the current request URL.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    /// <example>
    /// <![CDATA[
    ///     given:
    ///     http://localhost:8087/parent1/p1/f1/t3/page/2?a=1&b=3
    ///     http://127.0.0.1:8087/parent1/p1/f1/t3/page/2?b=3 - b is registered as non-excludable parameter
    ///     http://localhost/scrum1/parent1/p1/f1/t3#k-Fr3Zrcg2uN0P8AAPhBjw
    /// 
    ///     result:
    ///     http://localhost:8087/parent1/p1/f1/t3/page/2?b=3
    /// ]]>
    /// </example>
    private static string GetDefaultCanonicalUrl(
      Page page,
      PageSiteNode node,
      string originalUrl,
      IEnumerable<string> allowedQueryStringParamKeys)
    {
      string[] urlParameters = ControlExtensions.GetUrlParameters(page);
      string str = (string) null;
      if (urlParameters != null)
        str = string.Join("/", urlParameters);
      string virtualPath = NavigationUtilities.ResolveUrl((SiteMapNode) node, segments: urlParameters);
      PaginationUrls paginationUrls = (PaginationUrls) null;
      if (!str.IsNullOrEmpty() && CanonicalUrlPageExtensions.TryGetInternalPaginationUrls(page, out paginationUrls))
        virtualPath = VirtualPathUtility.AppendTrailingSlash(virtualPath) + str;
      string url = virtualPath + CanonicalUrlPageExtensions.GetFilteredQueryString(originalUrl, allowedQueryStringParamKeys);
      bool flag = true;
      switch (Config.Get<SystemConfig>().ContentLocationsSettings.CanonicalUrlResolverMode)
      {
        case CanonicalUrlResolverMode.Auto:
          if (SystemManager.CurrentContext.CurrentSite.DomainAliases != null && SystemManager.CurrentContext.CurrentSite.DomainAliases.Any<string>() && !page.Response.Cache.VaryByHeaders["Host"])
          {
            flag = false;
            break;
          }
          break;
        case CanonicalUrlResolverMode.Static:
          flag = false;
          break;
      }
      return UrlPath.ResolveUrl(url, true, true, !flag);
    }

    private static void SetPaginationLinks(Page page, List<string> allowedQueryStringParameterKeys)
    {
      PaginationUrls paginationUrls = (PaginationUrls) null;
      if (!CanonicalUrlPageExtensions.TryGetInternalPaginationUrls(page, out paginationUrls))
        return;
      string previousUrl = paginationUrls.PreviousUrl;
      string nextUrl = paginationUrls.NextUrl;
      if (!string.IsNullOrEmpty(previousUrl))
      {
        string str = CanonicalUrlPageExtensions.ReplaceQueryString(previousUrl, (IEnumerable<string>) allowedQueryStringParameterKeys);
        CanonicalUrlPageExtensions.AddUrlLink(page, "prev", str);
      }
      if (string.IsNullOrEmpty(nextUrl))
        return;
      string str1 = CanonicalUrlPageExtensions.ReplaceQueryString(nextUrl, (IEnumerable<string>) allowedQueryStringParameterKeys);
      CanonicalUrlPageExtensions.AddUrlLink(page, "next", str1);
    }

    private static string GetFilteredQueryString(
      string originalUrl,
      IEnumerable<string> allowedQueryStringParamKeys)
    {
      QueryStringBuilder queryStringBuilder1 = new QueryStringBuilder(originalUrl);
      QueryStringBuilder queryStringBuilder2 = new QueryStringBuilder();
      foreach (string queryStringParamKey in allowedQueryStringParamKeys)
      {
        if (queryStringBuilder1[queryStringParamKey] != null)
          queryStringBuilder2.Set(queryStringParamKey, queryStringBuilder1[queryStringParamKey]);
      }
      return queryStringBuilder2.ToString();
    }

    private static string ReplaceQueryString(
      string originalUrl,
      IEnumerable<string> allowedQueryStringParamKeys)
    {
      string filteredQueryString = CanonicalUrlPageExtensions.GetFilteredQueryString(originalUrl, allowedQueryStringParamKeys);
      int length = originalUrl.IndexOf('?');
      return length > 0 ? originalUrl.Substring(0, length) + filteredQueryString : originalUrl;
    }

    /// <summary>
    /// Gets the white-list parameters from the configuration.
    /// </summary>
    /// <param name="pageCanonicalUrlSettings"></param>
    /// <returns></returns>
    private static List<string> GetAllowedParameters(
      PageDefaultCanonicalUrlSettingsElement pageCanonicalUrlSettings)
    {
      List<string> allowedParameters = new List<string>();
      foreach (CanonicalUrlQueryStringParameterElement parameterElement in (IEnumerable<CanonicalUrlQueryStringParameterElement>) pageCanonicalUrlSettings.AllowedCanonicalUrlQueryStringParameters.Values)
        allowedParameters.Add(parameterElement.ParameterName);
      return allowedParameters;
    }

    /// <summary>
    /// Determines whether a canonical URL has been set for the specified page.
    /// Checks if the page header as html, as object and the HTTP Header
    /// </summary>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    private static bool HasExternalCanonicalUrl(Page page)
    {
      flag = false;
      if (!(page.Items[(object) "sf-canonical-url-external"] is bool flag))
        ;
      if (page != null)
      {
        foreach (object control in page.Header.Controls)
        {
          if (control is HtmlLink link)
            flag |= CanonicalUrlPageExtensions.IsCanonicalLink(link);
          if (flag)
            break;
        }
      }
      if (HttpRuntime.UsingIntegratedPipeline)
      {
        NameValueCollection headers = page.Response.Headers;
        if (headers.HasKeys() && headers.Keys.Contains("Content-Location"))
          flag = true;
      }
      return flag;
    }

    private static bool TryGetInternalCanonicalUrl(Page page, out string canonicalUrl)
    {
      bool internalCanonicalUrl = false;
      canonicalUrl = page.Items[(object) "sf-canonical-url"] as string;
      if (!string.IsNullOrWhiteSpace(canonicalUrl))
        internalCanonicalUrl = true;
      return internalCanonicalUrl;
    }

    private static bool TryGetInternalPaginationUrls(Page page, out PaginationUrls paginationUrls)
    {
      bool internalPaginationUrls = false;
      if (page == null)
      {
        paginationUrls = (PaginationUrls) null;
        return internalPaginationUrls;
      }
      paginationUrls = page.Items[(object) "sf-pagination-prev"] as PaginationUrls;
      if (paginationUrls != null)
        internalPaginationUrls = true;
      return internalPaginationUrls;
    }

    private static void AddUrlLink(Page page, string key, string value)
    {
      HtmlLink link = CanonicalUrlPageExtensions.GetLink(key, value);
      page.Header.Controls.Add((Control) link);
    }

    private static HtmlLink GetLink(string value, string url)
    {
      HtmlLink link = new HtmlLink();
      link.Attributes.Add("rel", value);
      link.Href = url;
      return link;
    }

    /// <summary>
    /// Determines whether the given HtmlLink is the canonical one.
    /// </summary>
    /// <param name="link">The link.</param>
    /// <returns></returns>
    private static bool IsCanonicalLink(HtmlLink link)
    {
      bool flag = false;
      if (link != null)
      {
        string attribute = link.Attributes["rel"];
        if (!string.IsNullOrWhiteSpace(attribute) && attribute == "canonical")
          flag = true;
      }
      return flag;
    }
  }
}
