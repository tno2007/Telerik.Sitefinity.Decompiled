// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.PageNodeRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Restriction
{
  /// <summary>Defines a PageNode restriction strategy.</summary>
  public class PageNodeRestrictionStrategy : IPageNodeRestrictionStrategy, IRestrictionStrategy
  {
    private static readonly Dictionary<Guid, Func<bool>> RestrictedPageNodeIds = new Dictionary<Guid, Func<bool>>();

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="pageNodeId">The page node identifier.</param>
    /// <param name="restrictionLevel">The restriction level.</param>
    public static void Add(Guid pageNodeId, RestrictionLevel restrictionLevel)
    {
      if (pageNodeId == Guid.Empty)
        throw new ArgumentException(nameof (pageNodeId));
      PageNodeRestrictionStrategy.Add(pageNodeId, (Func<bool>) (() => !SystemManager.IsOperationEnabled(restrictionLevel)));
    }

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="pageNodeId">The page node identifier.</param>
    /// <param name="isRestrictedCheck">Function which returns whether the page is restricted.</param>
    public static void Add(Guid pageNodeId, Func<bool> isRestrictedCheck)
    {
      if (pageNodeId == Guid.Empty)
        throw new ArgumentException(nameof (pageNodeId));
      if (isRestrictedCheck == null)
        throw new ArgumentException(nameof (isRestrictedCheck));
      if (PageNodeRestrictionStrategy.RestrictedPageNodeIds.ContainsKey(pageNodeId))
        return;
      PageNodeRestrictionStrategy.RestrictedPageNodeIds.Add(pageNodeId, isRestrictedCheck);
    }

    /// <summary>
    /// Determines whether the specified page (sitemap) node is restricted.
    /// </summary>
    /// <param name="item">The page (sitemap) node.</param>
    /// <returns>Whether item is restricted.</returns>
    public bool IsRestricted(object item) => this.IsRestricted(item.GetId());

    /// <inheritdoc />
    public bool IsRestricted(Guid pageNodeId) => PageNodeRestrictionStrategy.RestrictedPageNodeIds.ContainsKey(pageNodeId) && PageNodeRestrictionStrategy.RestrictedPageNodeIds[pageNodeId]();

    /// <summary>Gets the restricted page node ids</summary>
    /// <returns>The restricted page node ids</returns>
    protected static Dictionary<Guid, Func<bool>> GetRestrictedPageNodeIds() => PageNodeRestrictionStrategy.RestrictedPageNodeIds;
  }
}
