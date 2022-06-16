// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleFilters
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>Provides common filters for the lifecycle items.</summary>
  public static class LifecycleFilters
  {
    /// <summary>Gets only the published master items.</summary>
    /// <typeparam name="TItem">
    /// Type of the item on which the filter should be applied.
    /// </typeparam>
    /// <param name="source">
    /// IQueryable on which the filter should be applied.
    /// </param>
    /// <returns>
    /// IQueryable object with appended filter for only published master items.
    /// </returns>
    public static IQueryable<TItem> OnlyPublishedMasterItems<TItem>(
      IQueryable<TItem> source)
      where TItem : ILifecycleDataItem
    {
      return source.Where<TItem>((Expression<Func<TItem, bool>>) (p => (int) p.Status == 0));
    }

    /// <summary>Gets only the live items.</summary>
    /// <typeparam name="TItem">
    /// Type of the item on which the filter should be applied.
    /// </typeparam>
    /// <param name="source">
    /// IQueryable on which the filter should be applied.
    /// </param>
    /// <returns>
    /// IQueryable object with appended filter for only live items.
    /// </returns>
    public static IQueryable<TItem> OnlyLiveItems<TItem>(IQueryable<TItem> source) where TItem : ILifecycleDataItem => source.Where<TItem>((Expression<Func<TItem, bool>>) (p => (int) p.Status == 2));
  }
}
