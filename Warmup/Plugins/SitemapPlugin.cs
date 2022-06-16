// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.Plugins.SitemapPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Warmup.Plugins
{
  /// <summary>Sitemap warmup plugin</summary>
  internal class SitemapPlugin : IWarmupPlugin
  {
    public const string MaxPagesOnStartupPerSiteKey = "maxPagesOnStartupPerSite";
    public const string MaxPagesAfterInitializationPerSiteKey = "maxPagesAfterInitializationPerSite";
    public const int DefaultMaxPagesOnStartupPerSite = 10;
    public const int DefaultMaxPagesAfterInitializationPerSite = 50;

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.Warmup.IWarmupPlugin" />.
    /// </summary>
    /// <value>
    /// The name of the <see cref="T:Telerik.Sitefinity.Warmup.IWarmupPlugin" />.
    /// </value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the maximum number of pages to warm up on startup per site.
    /// </summary>
    /// <value>
    /// The maximum number of pages to warm up on startup per site.
    /// </value>
    protected int MaxPagesOnStartupPerSite { get; private set; }

    /// <summary>
    /// Gets the maximum number of pages to warm up after initialization per site.
    /// </summary>
    /// <value>
    /// The maximum number of pages to warm up after initialization per site.
    /// </value>
    protected int MaxPagesAfterInitializationPerSite { get; private set; }

    /// <summary>Initializes the new instance of the warmup plug-in</summary>
    /// <param name="name">The name of the plug-in instance</param>
    /// <param name="parameters">Collection of parameters to initialize the instance with.</param>
    public void Initialize(string name, NameValueCollection parameters)
    {
      this.Name = name;
      this.MaxPagesOnStartupPerSite = this.GetParamIntValue(parameters, "maxPagesOnStartupPerSite");
      this.MaxPagesAfterInitializationPerSite = this.GetParamIntValue(parameters, "maxPagesAfterInitializationPerSite");
    }

    /// <summary>Gets the URLs. Will run for every site.</summary>
    /// <returns>The URLs collection</returns>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public IEnumerable<WarmupUrl> GetUrls()
    {
      int count = this.MaxPagesOnStartupPerSite + this.MaxPagesAfterInitializationPerSite;
      IOrderedEnumerable<ISite> source = SystemManager.CurrentContext.GetSites().OrderByDescending<ISite, bool>((Func<ISite, bool>) (s => s.IsDefault));
      List<WarmupUrl> urls = new List<WarmupUrl>(count * source.Count<ISite>());
      IPrincipal principal = (IPrincipal) null;
      try
      {
        HttpContext current = HttpContext.Current;
        if (current != null)
        {
          principal = current.User;
          current.User = (IPrincipal) ClaimsManager.GetAnonymous();
        }
        foreach (ISite site in (IEnumerable<ISite>) source)
        {
          if (!site.IsOffline)
          {
            using (new SiteRegion(site, SiteContextResolutionTypes.ByFolder))
            {
              using (new CultureRegion(site.DefaultCulture))
              {
                Uri uri1 = site.GetUri();
                if (site.IsDefault && current != null && uri1.Authority.Equals("localhost"))
                  uri1 = new UriBuilder(current.Request.Url.AbsoluteUri)
                  {
                    Path = uri1.PathAndQuery
                  }.Uri;
                UriBuilder uriBuilder1 = new UriBuilder(uri1);
                uriBuilder1.Path = (string) null;
                string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
                if (!applicationVirtualPath.IsNullOrEmpty() && !(applicationVirtualPath == "/"))
                {
                  uriBuilder1.Path = applicationVirtualPath;
                  UriBuilder uriBuilder2 = new UriBuilder(uri1);
                  if (!uriBuilder2.Path.IsNullOrEmpty())
                    uriBuilder2.Path = applicationVirtualPath + uriBuilder2.Path;
                  uri1 = uriBuilder2.Uri;
                }
                Uri uri2 = uriBuilder1.Uri;
                SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
                if (currentProvider != null)
                {
                  if (currentProvider.RootNode != null)
                  {
                    IList<WarmupUrl> urlsInternal = this.GetUrlsInternal(uri1, uri2, currentProvider.RootNode, count);
                    for (int index = 0; index < this.MaxPagesOnStartupPerSite && index < urlsInternal.Count; ++index)
                      urlsInternal[index].Priority = WarmupPriority.High;
                    urls.AddRange((IEnumerable<WarmupUrl>) urlsInternal);
                  }
                }
              }
            }
          }
        }
      }
      finally
      {
        if (HttpContext.Current != null)
          HttpContext.Current.User = principal;
      }
      return (IEnumerable<WarmupUrl>) urls;
    }

    private IList<WarmupUrl> GetUrlsInternal(
      Uri siteUri,
      Uri baseUri,
      SiteMapNode parentNode,
      int count)
    {
      List<WarmupUrl> urlsInternal = new List<WarmupUrl>();
      Queue<SiteMapNode> source = new Queue<SiteMapNode>();
      source.Enqueue(parentNode);
      while (count > urlsInternal.Count && source.Any<SiteMapNode>())
      {
        foreach (PageSiteNode sortedPage in (IEnumerable<PageSiteNode>) this.GetSortedPageList(source.Dequeue().ChildNodes, count))
        {
          if (sortedPage.NodeType == NodeType.Standard)
          {
            WarmupUrl warmupUrl = this.GetWarmupUrl(sortedPage, siteUri, baseUri);
            urlsInternal.Add(warmupUrl);
          }
          source.Enqueue((SiteMapNode) sortedPage);
        }
      }
      return (IList<WarmupUrl>) urlsInternal;
    }

    private IList<PageSiteNode> GetSortedPageList(
      SiteMapNodeCollection childNodes,
      int count)
    {
      return (IList<PageSiteNode>) childNodes.OfType<PageSiteNode>().Where<PageSiteNode>((Func<PageSiteNode, bool>) (p => (p.NodeType == NodeType.Group || p.NodeType == NodeType.Standard && p.ShowInNavigation) && p.IsPublished())).OrderByDescending<PageSiteNode, float>((Func<PageSiteNode, float>) (n => n.Priority)).Take<PageSiteNode>(count).ToList<PageSiteNode>();
    }

    private WarmupUrl GetWarmupUrl(PageSiteNode node, Uri siteUri, Uri baseUri)
    {
      WarmupUrl warmupUrl = new WarmupUrl(node.IsHomePage() ? siteUri.AbsoluteUri : UrlPath.ResolveAbsoluteUrl(baseUri, node.Url));
      OutputCacheProfileElement outputCacheProfile = node.GetOutputCacheProfile();
      if (outputCacheProfile != null)
        warmupUrl.VaryByUserAgent = outputCacheProfile.VaryByUserAgent;
      return warmupUrl;
    }

    private int GetParamIntValue(NameValueCollection parameters, string key)
    {
      int result;
      int.TryParse(parameters[key], out result);
      return result;
    }
  }
}
