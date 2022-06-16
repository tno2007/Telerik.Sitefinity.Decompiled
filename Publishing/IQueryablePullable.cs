// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IQueryablePullable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// An optional interface allowing more options during a query of a "pullable"
  /// (e.g. <see cref="T:Telerik.Sitefinity.Publishing.IPullPipe" />, <see cref="T:Telerik.Sitefinity.Publishing.PassThroughPublishingPoint" />).
  /// </summary>
  internal interface IQueryablePullable
  {
    /// <summary>
    /// Returns an <see cref="T:System.Linq.IQueryable`1" /> which can be used to "pull" all the items in the pipe.
    /// </summary>
    /// <param name="filter">Dynamic LINQ filter expression.</param>
    /// <param name="order">Ordering expression.</param>
    /// <param name="skip">The number of items to be skipped from the beginning.</param>
    /// <param name="take">The number of items to take; when less or equal to zero, all items are returned.</param>
    IQueryable<WrapperObject> GetItems(
      string filter,
      string order,
      int skip,
      int take);
  }
}
