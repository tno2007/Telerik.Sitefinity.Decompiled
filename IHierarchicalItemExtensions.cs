// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.IHierarchicalItemExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Net;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity
{
  internal static class IHierarchicalItemExtensions
  {
    /// <summary>
    /// Constructs the path of all predecessors of the specified <see cref="T:Telerik.Sitefinity.IHierarchicalItem" />.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="previouslyVisitedItems">The previously visited items.</param>
    /// <returns></returns>
    public static List<IHierarchicalItem> ConstructPath(
      this IHierarchicalItem item,
      HashSet<Guid> previouslyVisitedItems)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      List<IHierarchicalItem> itemPath = new List<IHierarchicalItem>();
      itemPath.Add(item);
      Guid parentId = item.ParentId;
      if (item.ParentId != Guid.Empty && (previouslyVisitedItems == null || !previouslyVisitedItems.Contains(item.Id)))
        item.VisitItemTaxon(itemPath, new HashSet<Guid>()
        {
          item.Id
        }, previouslyVisitedItems);
      return itemPath;
    }

    /// <summary>
    /// Visits the item taxon and adds it to the specified item path. The method keeps track on the visited items in order to prevent endless recursion.
    /// </summary>
    /// <param name="taxon">The taxon.</param>
    /// <param name="pagePath">The page path.</param>
    /// <param name="visitedItemIds">The visited item ids.</param>
    /// <param name="previouslyVisitedItems">The previously visited items.</param>
    public static void VisitItemTaxon(
      this IHierarchicalItem taxon,
      List<IHierarchicalItem> itemPath,
      HashSet<Guid> visitedItemIds,
      HashSet<Guid> previouslyVisitedItems)
    {
      Guid parentId = taxon.ParentId;
      if (!(taxon.ParentId != Guid.Empty))
        return;
      if (visitedItemIds.Contains(taxon.ParentId))
        IHierarchicalItemExtensions.TreeMalformed();
      if (previouslyVisitedItems != null && previouslyVisitedItems.Contains(taxon.ParentId))
        return;
      visitedItemIds.Add(taxon.ParentId);
      itemPath.Insert(0, taxon.Parent);
      taxon.Parent.VisitItemTaxon(itemPath, visitedItemIds, previouslyVisitedItems);
    }

    /// <summary>Trees the malformed.</summary>
    private static void TreeMalformed() => throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<TaxonomyResources>().TaxonomyTreeMalformed, (Exception) null);
  }
}
