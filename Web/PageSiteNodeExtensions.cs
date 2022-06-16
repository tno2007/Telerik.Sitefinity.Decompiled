// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageSiteNodeExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Extension methods for the PageSiteNode</summary>
  public static class PageSiteNodeExtensions
  {
    /// <summary>
    /// Gets the live URL merging it with additional parameters string if specified.
    /// Use this method instead of Url property, because the page node could have a specific logic for building the URL with addtional parameters.
    /// For example, if the page have extension '.aspx', its URL is '~/page.aspx', but the URL with parameters is '~/page/param1/param2'.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="resolve">Specify whether the returned url should be resolved based on the current context.</param>
    /// <returns></returns>
    internal static string GetLiveUrl(this PageSiteNode node, bool resolve = true) => PageSiteNodeExtensions.GetUrl(node, (string) null, resolve);

    /// <summary>
    /// Gets the live URL merging it with additional parameters string if specified.
    /// Use this method instead of Url property, because the page node could have a specific logic for building the URL with addtional parameters.
    /// For example, if the page have extension '.aspx', its URL is '~/page.aspx', but the URL with parameters is '~/page/param1/param2'.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="urlEnd">The url string that will be appended to the generated page url.</param>
    /// <param name="resolve">Specify whether the returned url should be resolved based on the current context.</param>
    /// <returns></returns>
    internal static string GetLiveUrl(this PageSiteNode node, string urlEnd, bool resolve = true) => PageSiteNodeExtensions.GetUrl(node, urlEnd, resolve);

    /// <summary>
    /// Gets a value indicating whether the specified page node is a home page.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static bool IsHomePage(this PageSiteNode node) => node.Provider is SiteMapBase provider && node.Key.Equals(provider.RootNodeKey, StringComparison.OrdinalIgnoreCase);

    /// <summary>Gets the related items.</summary>
    /// <typeparam name="TItem">The type of the T item.</typeparam>
    /// <param name="node">The node.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public static IQueryable<TItem> GetRelatedItems<TItem>(
      this PageSiteNode node,
      string fieldName,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      return PageExtensions.GetRelatedItems<TItem>(node.Id, node.PageProviderName, fieldName, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>Gets the edit url for the page site node.</summary>
    /// <param name="node">The page node.</param>
    /// <param name="culture">The culture. If null, the current culture will be used.</param>
    /// <returns>The backend edit url for the page site node.</returns>
    public static string GetPageEditBackendUrl(this PageSiteNode node, CultureInfo culture = null)
    {
      using (new CultureRegion(culture))
      {
        string url = node.GetBackendUrl("Edit");
        if (culture != null)
          url = url + "/" + culture.Name;
        return UrlPath.ResolveUrl(url);
      }
    }

    /// <summary>
    /// Gets the backend URL for the specified actions. The supported actions can be found in RouteHelper.ActionKeys
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="actionName">Name of the action.</param>
    /// <returns></returns>
    internal static string GetBackendUrl(this PageSiteNode node, string actionName)
    {
      string paramString = "Action" + "/" + actionName;
      return PageSiteNodeExtensions.GetUrl(node, paramString, false);
    }

    internal static OutputCacheProfileElement GetOutputCacheProfile(
      this PageSiteNode node)
    {
      PageDataProxy currentPageDataItem = node.CurrentPageDataItem;
      if (currentPageDataItem == null)
        return (OutputCacheProfileElement) null;
      OutputCacheProfileElement outputCache = currentPageDataItem.OutputCache;
      if (outputCache == null)
      {
        SystemConfig systemConfig = Config.Get<SystemConfig>();
        string key = currentPageDataItem.OutputCacheProfile;
        if (string.IsNullOrEmpty(key))
          key = systemConfig.CacheSettings.DefaultProfile;
        if (systemConfig.CacheSettings.Profiles.TryGetValue(key, out outputCache))
        {
          currentPageDataItem.OutputCache = outputCache;
        }
        else
        {
          ArgumentException exceptionToHandle = new ArgumentException("Invalid output cache profile specified: \"{0}\".".Arrange((object) key));
          if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
            throw exceptionToHandle;
          systemConfig.CacheSettings.Profiles.TryGetValue(systemConfig.CacheSettings.DefaultProfile, out outputCache);
        }
      }
      return outputCache;
    }

    internal static string GetTitlesPath(
      this PageSiteNode node,
      string separator = " > ",
      CultureInfo culture = null)
    {
      IEnumerable<string> values = node.GetTitlesHierarchy(culture, false).Reverse<string>();
      return string.Join(separator, values);
    }

    private static string GetUrl(PageSiteNode node, string paramString, bool resolve)
    {
      string url;
      if (!paramString.IsNullOrEmpty())
      {
        if (paramString.StartsWith("?"))
        {
          url = node.GetUrl(false, resolve) + paramString;
        }
        else
        {
          if (!paramString.StartsWith("/"))
            paramString = "/" + paramString;
          url = node.GetUrl(true, resolve) + paramString;
        }
      }
      else
        url = node.GetUrl(false, resolve);
      return url;
    }

    internal static bool IsGranted(this PageSiteNode node, string permAction)
    {
      SitefinityIdentity user = ClaimsManager.GetCurrentIdentity();
      bool flag = false;
      if (user.IsAdmin)
        return true;
      List<Guid> allowedPrincipal = node.AllowedPrincipals[permAction];
      List<Guid> deniedPrincipal = node.DeniedPrincipals[permAction];
      if (allowedPrincipal.Count > 0)
      {
        bool isOwner = user.IsAuthenticated && user.UserId == node.Owner;
        Guid ownerRoleId = SecurityManager.OwnerRole.Id;
        flag = allowedPrincipal.Any<Guid>((Func<Guid, bool>) (p =>
        {
          if (user.UserId == p || user.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == p)))
            return true;
          return isOwner && p == ownerRoleId;
        }));
        if (flag && deniedPrincipal.Count > 0)
          flag = !deniedPrincipal.Any<Guid>((Func<Guid, bool>) (p =>
          {
            if (user.UserId == p || user.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == p)))
              return true;
            return isOwner && p == ownerRoleId;
          }));
      }
      return flag;
    }

    internal static bool? UseSiteContext(this PageSiteNode siteNode)
    {
      bool? nullable = new bool?();
      if (!siteNode.ModuleName.IsNullOrEmpty() && SystemManager.GetModule(siteNode.ModuleName) is IMultisiteContextModule module)
        nullable = module.IsPageUseSiteContext(siteNode);
      return nullable;
    }

    /// <summary>
    /// Gets the live URL of a page or preview URL if the page is published
    /// Use this method instead of GetLiveUrl as this one takes into consideration if the site is multilingual and the culture
    /// </summary>
    /// <returns>Live URL for a page or Preview URL if page is not published</returns>
    internal static string GetPageViewUrl(this PageSiteNode siteNode)
    {
      string empty = string.Empty;
      string pageUrlWithCulture = siteNode.GetAbsolutePageUrlWithCulture();
      CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
      string pageViewUrl;
      try
      {
        pageViewUrl = siteNode.NodeType == NodeType.Group || siteNode.NodeType == NodeType.InnerRedirect || siteNode.NodeType == NodeType.OuterRedirect || siteNode.NodeType == NodeType.Rewriting || siteNode.IsPublished(culture) ? pageUrlWithCulture : siteNode.GetPagePreviewUrl(pageUrlWithCulture);
      }
      catch
      {
        pageViewUrl = string.Empty;
      }
      return pageViewUrl;
    }

    internal static string GetPagePreviewUrl(this PageSiteNode siteNode)
    {
      string pageUrlWithCulture = siteNode.GetAbsolutePageUrlWithCulture();
      return siteNode.GetPagePreviewUrl(pageUrlWithCulture);
    }

    private static string GetPagePreviewUrl(this PageSiteNode siteNode, string pageUrl)
    {
      string pagePreviewUrl;
      if (!siteNode.IsExternallyRendered())
      {
        int num = siteNode.IsBackend ? (((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 ? 1 : 0) : (SystemManager.CurrentContext.AppSettings.Multilingual ? 1 : 0);
        pagePreviewUrl = RouteHelper.ResolveUrl(siteNode.GetBackendUrl("Preview"), UrlResolveOptions.Rooted);
        if (num != 0)
          pagePreviewUrl = pagePreviewUrl + "/" + SystemManager.CurrentContext.Culture.Name;
      }
      else
        pagePreviewUrl = pageUrl + string.Format("?{0}={1}&sfaction={2}", (object) "sf_site", (object) SystemManager.CurrentContext.CurrentSite.Id, (object) "Preview".ToLowerInvariant());
      return pagePreviewUrl;
    }

    internal static string GetAbsolutePageUrlWithCulture(this PageSiteNode siteNode)
    {
      string str = siteNode.GetLiveUrl(false);
      if ((siteNode.IsBackend ? (((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 ? 1 : 0) : (SystemManager.CurrentContext.AppSettings.Multilingual ? 1 : 0)) != 0)
        str = ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(str, SystemManager.CurrentContext.Culture);
      if (str.StartsWith("~"))
        str = RouteHelper.ResolveUrl(str, UrlResolveOptions.Absolute);
      if (str.IsNullOrWhitespace())
        return string.Empty;
      Uri uri = new Uri(str);
      return !uri.IsAbsoluteUri ? VirtualPathUtility.ToAbsolute(uri.ToString()) : uri.ToString();
    }

    internal static bool IsRedirectPage(this PageSiteNode siteNode) => siteNode.NodeType == NodeType.InnerRedirect || siteNode.NodeType == NodeType.OuterRedirect;

    internal static bool IsExternallyRendered(this PageSiteNode siteNode) => !string.IsNullOrEmpty(siteNode.CurrentPageDataItem.Renderer);

    internal static string GetVariationKey(this PageSiteNode siteNode) => siteNode.GetVariationKey(out string _);

    internal static string GetVariationKey(this PageSiteNode siteNode, out string variationType)
    {
      if (siteNode.IsPersonalized())
      {
        foreach (string variationType1 in siteNode.GetVariationTypes())
        {
          IPageVariationPlugin pageVariationPlugin = ObjectFactory.Container.Resolve<IPageVariationPlugin>(variationType1);
          if (pageVariationPlugin != null)
          {
            string pageVariationKey = pageVariationPlugin.GetPageVariationKey(siteNode);
            if (!string.IsNullOrEmpty(pageVariationKey))
            {
              variationType = variationType1;
              return pageVariationKey;
            }
          }
        }
      }
      variationType = (string) null;
      return (string) null;
    }

    internal static PageDataProxy FindPersonalizedPageDataProxyOrFallbackToDefault(
      this PageSiteNode siteNode)
    {
      string variationKey = siteNode.GetVariationKey();
      return string.IsNullOrEmpty(variationKey) ? siteNode.CurrentPageDataItem : (siteNode.Provider as SiteMapBase).FindPageProxyVariation(siteNode, Guid.Parse(variationKey));
    }
  }
}
