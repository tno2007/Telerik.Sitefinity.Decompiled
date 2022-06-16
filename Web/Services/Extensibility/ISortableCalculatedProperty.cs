// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ISortableCalculatedProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>Interface for sortable calculated properties</summary>
  public interface ISortableCalculatedProperty
  {
    /// <summary>Gets the ordered query.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="query">The query.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
    /// <returns>Ordered query</returns>
    IQueryable GetOrderedQuery<TItem>(
      IQueryable query,
      IManager manager,
      bool isAscending)
      where TItem : IDataItem;
  }
}
