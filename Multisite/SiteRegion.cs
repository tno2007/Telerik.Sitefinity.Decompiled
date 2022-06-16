// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.SiteRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  public class SiteRegion : IDisposable
  {
    private readonly ISiteContext siteToRestore;

    protected SiteRegion()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.SiteRegion" /> class.
    /// </summary>
    /// <param name="site">The site that will be set as current.</param>
    public SiteRegion(ISite site)
      : this(site, SiteContextResolutionTypes.Auto)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.SiteRegion" /> class.
    /// </summary>
    /// <param name="site">The site that will be set as current.</param>
    /// <param name="siteResolutionType">Determines how the site context is resolved.</param>
    public SiteRegion(ISite site, SiteContextResolutionTypes siteResolutionType)
      : this(site, siteResolutionType, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.SiteRegion" /> class.
    /// </summary>
    /// <param name="site">The site that will be set as current.</param>
    /// <param name="siteResolutionType">Determines how the site context is resolved.</param>
    /// <param name="force">if set to <c>true</c> [the site is changed anyway].</param>
    /// <exception cref="T:System.ArgumentNullException">site</exception>
    private SiteRegion(ISite site, SiteContextResolutionTypes siteResolutionType, bool force)
    {
      if (site == null)
        throw new ArgumentNullException(nameof (site));
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext == null)
        return;
      ISiteContext currentSiteContext = multisiteContext.CurrentSiteContext;
      if (!(currentSiteContext.Site.Id != site.Id | force))
        return;
      this.siteToRestore = currentSiteContext;
      if (siteResolutionType == SiteContextResolutionTypes.Auto)
      {
        siteResolutionType = SiteContextResolutionTypes.ExplicitlySet;
        if (site is MultisiteContext.SiteProxy subSite && !subSite.RedirectFolder.IsNullOrEmpty() && currentSiteContext.Site is MultisiteContext.SiteProxy site1 && site1.Contains(subSite))
          siteResolutionType = SiteContextResolutionTypes.ByFolder;
      }
      multisiteContext.ChangeCurrentSite(site, siteResolutionType);
    }

    /// <summary>
    /// Determines whether this region has changed the current site.
    /// </summary>
    /// <value>The changed.</value>
    public bool Changed => this.siteToRestore != null;

    /// <summary>
    /// Switches the current site to be the one before the creation of the region.
    /// </summary>
    public void Dispose()
    {
      if (this.siteToRestore == null)
        return;
      SystemManager.CurrentContext.MultisiteContext.CurrentSiteContext = this.siteToRestore;
    }

    /// <summary>
    /// Sets the site with the given site map root id as current.
    /// </summary>
    /// <param name="rootId">The root id.</param>
    /// <param name="siteResolutionType">Determines how the site context is resolved.</param>
    /// <returns></returns>
    public static SiteRegion FromSiteMapRoot(
      Guid rootId,
      SiteContextResolutionTypes siteResolutionType = SiteContextResolutionTypes.Auto)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && rootId != Guid.Empty && rootId != SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId)
      {
        ISite siteBySiteMapRoot = multisiteContext.GetSiteBySiteMapRoot(rootId);
        if (siteBySiteMapRoot != null)
          return new SiteRegion(siteBySiteMapRoot, siteResolutionType);
      }
      return new SiteRegion();
    }

    /// <summary>Sets the site with the given id as current.</summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="siteResolutionType">Determines how the site context is resolved.</param>
    /// <returns></returns>
    public static SiteRegion FromSiteId(
      Guid siteId,
      SiteContextResolutionTypes siteResolutionType = SiteContextResolutionTypes.Auto)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && siteId != Guid.Empty && siteId != SystemManager.CurrentContext.CurrentSite.Id)
      {
        ISite siteById = multisiteContext.GetSiteById(siteId);
        if (siteById != null)
          return new SiteRegion(siteById, siteResolutionType);
      }
      return new SiteRegion();
    }

    /// <summary>Sets the site with the given name as current.</summary>
    /// <param name="siteName">Name of the site.</param>
    /// <param name="siteResolutionType">Determines how the site context is resolved.</param>
    /// <returns></returns>
    public static SiteRegion FromSiteName(
      string siteName,
      SiteContextResolutionTypes siteResolutionType = SiteContextResolutionTypes.Auto)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && !siteName.IsNullOrEmpty() && siteName != SystemManager.CurrentContext.CurrentSite.Name)
      {
        ISite siteByName = multisiteContext.GetSiteByName(siteName);
        if (siteByName != null)
          return new SiteRegion(siteByName, siteResolutionType);
      }
      return new SiteRegion();
    }

    /// <summary>Sets the system in single site mode.</summary>
    /// <returns></returns>
    internal static SiteRegion SingleSiteMode() => new SiteRegion((ISite) new SingleSiteContext.SingleSiteProxy(), SiteContextResolutionTypes.ExplicitlySet, true);
  }
}
