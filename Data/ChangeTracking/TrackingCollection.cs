// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ChangeTracking.TrackingCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.ChangeTracking;

namespace Telerik.Sitefinity.Data.ChangeTracking
{
  public class TrackingCollection : Dictionary<Guid, ITrackableItem>
  {
    /// <summary>Gets the items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IEnumerable<T> GetItems<T>() => this.Values.Where<ITrackableItem>((Func<ITrackableItem, bool>) (item => typeof (T).IsAssignableFrom(item.GetType()))).Cast<T>();

    /// <summary>Gets the deleted items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IList<T> GetDeletedItems<T>() => (IList<T>) this.GetItems<T>().Where<T>((Func<T, bool>) (item => ((ITrackableItem) (object) item).ItemState.IsDeleted)).ToList<T>();

    /// <summary>Gets the inserted items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IList<T> GetInsertedItems<T>() => (IList<T>) this.GetItems<T>().Where<T>((Func<T, bool>) (item => ((ITrackableItem) (object) item).ItemState.IsNew)).ToList<T>();

    /// <summary>Gets the updated items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IList<T> GetUpdatedItems<T>() => (IList<T>) this.GetItems<T>().Where<T>((Func<T, bool>) (item => ((ITrackableItem) (object) item).ItemState.IsChanged && !((ITrackableItem) (object) item).ItemState.IsNew)).ToList<T>();
  }
}
