// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ContentExtensions.IRecyclableContentExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.Sitefinity.RecycleBin.ContentExtensions
{
  /// <summary>
  /// Contains extension methods for <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /> data items.
  /// These methods are used to extend static content like News, Events, etc.
  /// </summary>
  internal static class IRecyclableContentExtensions
  {
    /// <summary>
    /// Gets whether the specified item that is <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /> and <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> is
    /// marked as deleted.
    /// </summary>
    /// <remarks>This method will resolve the component responsible for determining the deleted state of the item.</remarks>
    /// <typeparam name="T">The type of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /><see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> data item.</typeparam>
    /// <param name="dataItem">The data item.</param>
    /// <returns>Whether the specified item that is <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /> and <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> is
    /// marked as deleted.</returns>
    internal static bool GetContentIsDeleted<T>(this T dataItem) where T : IRecyclableDataItem, ILifecycleDataItem => ObjectFactory.Resolve<IRecyclableLifecycleDataItemModifier>().GetIsMarkedAsDeleted<T>(dataItem);

    /// <summary>
    /// Sets whether the specified item that is <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /> and <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> is
    /// marked as deleted.
    /// </summary>
    /// <remarks>This method will resolve the component responsible for determining the deleted state of the item.</remarks>
    /// <typeparam name="T">The type of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /><see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> data item.</typeparam>
    /// <param name="dataItem">The data item.</param>
    /// <param name="value">The value to set.</param>
    internal static void SetContentIsDeleted<T>(this T dataItem, bool value) where T : IRecyclableDataItem, ILifecycleDataItem => ObjectFactory.Resolve<IRecyclableLifecycleDataItemModifier>().SetIsMarkedAsDeleted<T>(dataItem, value);
  }
}
