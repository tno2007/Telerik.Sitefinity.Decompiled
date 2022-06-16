// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Filters.PagingFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;

namespace Telerik.Sitefinity.RecycleBin.Filters
{
  /// <summary>Applies paging to a query of items.</summary>
  internal class PagingFilter : IPagingFilter
  {
    /// <summary>Applies paging to the query.</summary>
    /// <typeparam name="T">The type on which paging is applied.</typeparam>
    /// <param name="query">The input query on which paging is applied.</param>
    /// <param name="skip">The number of elements in the query to bypass.</param>
    /// <param name="take">The number of contiguous elements from the start.</param>
    /// <returns>IQueryable of <typeparamref name="T" />.</returns>
    public IQueryable<T> ApplyPaging<T>(IQueryable<T> query, int? skip, int? take)
    {
      if (skip.HasValue)
        query = Queryable.Skip<T>(query, skip.Value);
      if (take.HasValue)
        query = Queryable.Take<T>(query, take.Value);
      return query;
    }
  }
}
