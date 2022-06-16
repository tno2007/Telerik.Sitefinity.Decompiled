// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleItemExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// This class provides functionality for enhancing the lifecycle items for usage in the
  /// web user interface of Sitefinity.
  /// </summary>
  public static class LifecycleItemExtensions
  {
    /// <summary>Enahnces the item with the lifecycle features.</summary>
    /// <typeparam name="TItem">
    /// Actual type of the item to be enhanced
    /// </typeparam>
    /// <param name="item">The instance of the item to be enhanced.</param>
    public static void PopulateLifecycleInformation(this ILifecycleViewModel item) => item.PopulateLifecycleInformation(string.Empty);

    public static void PopulateLifecycleInformation(
      this ILifecycleViewModel item,
      string providerName)
    {
      if (string.IsNullOrEmpty(providerName))
      {
        IDataItem dataItem = (IDataItem) item;
        if (dataItem != null)
          providerName = dataItem.GetProviderName();
      }
      LifecycleFacade lifecycleFacade = new LifecycleFacade(new Telerik.Sitefinity.Fluent.AppSettings()
      {
        ContentProviderName = providerName
      });
      ILifecycleDataItem live = (ILifecycleDataItem) null;
      ILifecycleDataItem temp = (ILifecycleDataItem) null;
      if (lifecycleFacade.Master((ILifecycleDataItem) item).IsPublished())
        live = lifecycleFacade.Master((ILifecycleDataItem) item).GetLive().Get();
      if (lifecycleFacade.Master((ILifecycleDataItem) item).IsCheckedOut((CultureInfo) null))
        temp = lifecycleFacade.Master((ILifecycleDataItem) item).GetTemp().Get();
      LifecycleItemExtensions.SetStatus(live, (ILifecycleDataItem) item, temp);
    }

    /// <summary>
    /// Gets the query of dependant lifecycle items for the item in master state.
    /// If the item is not in master state returns null;
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of the item for which the dependant lifecycle items should be retrieved.
    /// </typeparam>
    /// <param name="item">
    /// The item for which dependant lifecycle items ought to be obtained.
    /// </param>
    /// <param name="items">The query of the items.</param>
    /// <remarks>
    /// This method is mainly used for cleaning up when a master item is deleted.
    /// </remarks>
    /// <returns>The query of lifecycle dependant items.</returns>
    public static IQueryable<TItem> GetMasterDependantLifecycleItems<TItem>(
      this ILifecycleDataItem item,
      IQueryable<TItem> items)
      where TItem : ILifecycleDataItemGeneric
    {
      if (item.Status != ContentLifecycleStatus.Master)
        return (IQueryable<TItem>) null;
      Guid id = item.Id;
      return items.Where<TItem>((Expression<Func<TItem, bool>>) (c => (int) c.Status != 0 && c.OriginalContentId == id));
    }

    public static DefaultLifecycleStatusResolver GetLifecycleStatusResolver() => ObjectFactory.Resolve<DefaultLifecycleStatusResolver>();

    internal static void SetStatus(
      ILifecycleDataItem live,
      ILifecycleDataItem item,
      ILifecycleDataItem temp)
    {
      if (!(item is ILifecycleViewModel lifecycleViewModel) || !(item is IApprovalWorkflowItem approvalWorkflowItem))
        return;
      Lstring approvalWorkflowState = approvalWorkflowItem.ApprovalWorkflowState;
      lifecycleViewModel.Lifecycle.WorkflowStatus = (string) approvalWorkflowState;
      LifecycleItemExtensions.GetLifecycleStatusResolver().SetStatusInfo(lifecycleViewModel, live, temp);
    }

    /// <summary>
    /// Gets the id of the master item. If master item is passed, then return its id.
    /// </summary>
    /// <param name="dataItem">Item whose master id to find.</param>
    /// <returns>The master id.</returns>
    internal static Guid GetMasterId(this ILifecycleDataItemGeneric dataItem) => !(dataItem.OriginalContentId != Guid.Empty) ? dataItem.Id : dataItem.OriginalContentId;
  }
}
