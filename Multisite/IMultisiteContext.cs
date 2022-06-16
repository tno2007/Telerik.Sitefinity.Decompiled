// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.IMultisiteContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Multisite
{
  public interface IMultisiteContext
  {
    IEnumerable<ISite> GetSites();

    ISite CurrentSite { get; }

    ISiteContext CurrentSiteContext { get; set; }

    void ChangeCurrentSite(ISite site, SiteContextResolutionTypes siteResolutionType = SiteContextResolutionTypes.ExplicitlySet);

    ISite GetSiteById(Guid siteId);

    ISite GetSiteByName(string name);

    ISite GetSiteBySiteMapRoot(Guid siteMapRootId);

    ISite GetSiteByDomain(string domain);

    /// <summary>
    /// Gets the list of IDs to which a specific user is allowed access.
    /// </summary>
    /// <param name="user">The Id of the user.</param>
    /// <returns>The list of IDs to which a specific user is allowed access.</returns>
    IEnumerable<Guid> GetAllowedSites(Guid userId, string provider);

    /// <summary>Resolves the URL for the current site</summary>
    /// <param name="url"></param>
    /// <returns></returns>
    string ResolveUrl(string url);

    /// <summary>Unresolve the Url to the actual Url</summary>
    /// <param name="url"></param>
    /// <returns>The actual Url</returns>
    string UnresolveUrl(string url);

    /// <summary>Unresolves the URL and apply detected site.</summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    string UnresolveUrlAndApplyDetectedSite(string url);

    bool DataSourceExists(string name, string provider);
  }
}
