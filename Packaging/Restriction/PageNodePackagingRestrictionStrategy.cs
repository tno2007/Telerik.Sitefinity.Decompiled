// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Restriction.PageNodePackagingRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Packaging.Configuration;
using Telerik.Sitefinity.Restriction;

namespace Telerik.Sitefinity.Packaging.Restriction
{
  /// <summary>
  /// Defines a PageNode restriction strategy for packaging related functionality.
  /// </summary>
  internal class PageNodePackagingRestrictionStrategy : 
    IPageNodeRestrictionStrategy,
    IRestrictionStrategy
  {
    private static readonly Dictionary<Guid, PackagingMode> RestrictedPageNodeIds = new Dictionary<Guid, PackagingMode>();

    /// <summary>
    /// Determines whether the specified page (sitemap) node is restricted.
    /// </summary>
    /// <param name="item">The page (sitemap) node.</param>
    /// <returns>Whether item is restricted.</returns>
    /// <exception cref="T:System.ArgumentException">item is not of types PageSiteNode, PageNode.</exception>
    public bool IsRestricted(object item) => this.IsRestricted(item.GetId());

    /// <inheritdoc />
    public bool IsRestricted(Guid pageNodeId) => PageNodePackagingRestrictionStrategy.RestrictedPageNodeIds.ContainsKey(pageNodeId) && !this.IsNodeVisible(PageNodePackagingRestrictionStrategy.RestrictedPageNodeIds[pageNodeId]);

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="pageNodeId">The page node identifier.</param>
    /// <param name="packagingMode">The packaging mode.</param>
    internal static void Add(Guid pageNodeId, PackagingMode packagingMode)
    {
      if (pageNodeId == Guid.Empty)
        throw new ArgumentException(nameof (pageNodeId));
      if (PageNodePackagingRestrictionStrategy.RestrictedPageNodeIds.ContainsKey(pageNodeId))
        return;
      PageNodePackagingRestrictionStrategy.RestrictedPageNodeIds.Add(pageNodeId, packagingMode);
    }

    private bool IsNodeVisible(PackagingMode packagingMode) => (Config.Get<PackagingConfig>().PackagingMode & packagingMode) == PackagingMode.Source;
  }
}
