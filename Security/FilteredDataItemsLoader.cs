// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.FilteredDataItemsLoader`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// The loader filters given query of items based on the current user permissions.
  /// </summary>
  /// <typeparam name="T">The item type that is inheriting IFilterableItem interface</typeparam>
  public class FilteredDataItemsLoader<T> where T : IFilterableItem
  {
    private int skip;
    private const int DataItemsChunkSize = 100;
    private IEnumerable<T> mainDataItemsQuery;
    private Func<int, int, IEnumerable<T>> queryLoadDelegate;
    private List<T> currentItemsQuery;
    private IEnumerator<T> currentItemsQueryEnumerator;
    private Dictionary<string, Dictionary<string, IEnumerable<IDataItem>>> queues = new Dictionary<string, Dictionary<string, IEnumerable<IDataItem>>>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FilteredDataItemsLoader`1" /> class.
    /// </summary>
    /// <param name="queryLoadDelegate">The delegate, used to load the query chunk</param>
    public FilteredDataItemsLoader(Func<int, int, IEnumerable<T>> queryLoadDelegate)
    {
      this.queryLoadDelegate = queryLoadDelegate;
      this.InitializeNextDataItemChunk();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FilteredDataItemsLoader`1" /> class.
    /// </summary>
    /// <param name="query">The query data source</param>
    public FilteredDataItemsLoader(IEnumerable<T> query)
    {
      this.mainDataItemsQuery = query;
      this.InitializeNextDataItemChunk();
    }

    /// <summary>
    /// Validates the items query, based on the permission action that is passed.
    /// </summary>
    /// <typeparam name="K">The type of the returned items</typeparam>
    /// <param name="action">The permission action</param>
    /// <param name="hasMoreItems">The parameter validating whether there are more items left</param>
    /// <param name="take">The take parameter</param>
    /// <param name="skipItems">The skip parameter</param>
    /// <param name="generateItemViewModel">Relates to DashboardLogEntryViewModel</param>
    /// <returns>List of items implementing the IFilterableItem interface</returns>
    public List<K> ValidateDataItems<K>(
      PermissionsFilter.PermissionAction action,
      out bool hasMoreItems,
      int take,
      int skipItems,
      Func<T, IDataItem, K> generateItemViewModel)
    {
      List<K> kList = new List<K>();
      T entry = default (T);
      hasMoreItems = false;
      do
      {
        IDataItem nextDataItem = this.GetNextDataItem(out entry, this.queryLoadDelegate);
        if (nextDataItem != null)
        {
          if ((object) entry != null)
          {
            if (PermissionsFilter.IsCurrentUserAllowedAccessItem(nextDataItem, action))
            {
              if (skipItems == 0)
              {
                if (kList.Count >= take)
                {
                  hasMoreItems = true;
                  break;
                }
                K k = generateItemViewModel(entry, nextDataItem);
                kList.Add(k);
              }
              else
                --skipItems;
            }
          }
          else
            break;
        }
      }
      while ((object) entry != null);
      return kList;
    }

    /// <summary>
    /// Gets next data item from the enumerator and initializes next chunk of items
    /// </summary>
    /// <param name="entry">The current entry</param>
    /// <param name="query">The query of items</param>
    /// <returns>The current entry as IDataItem object</returns>
    private IDataItem GetNextDataItem(out T entry, Func<int, int, IEnumerable<T>> query = null)
    {
      if (this.currentItemsQueryEnumerator.MoveNext())
      {
        entry = this.currentItemsQueryEnumerator.Current;
        return this.GetOrLoadDataItem(entry);
      }
      this.InitializeNextDataItemChunk(query);
      if (this.currentItemsQuery.Any<T>())
        return this.GetNextDataItem(out entry, query);
      entry = default (T);
      return (IDataItem) null;
    }

    private IDataItem GetOrLoadDataItem(T entry)
    {
      string itemType = entry.ItemType;
      string str = itemType == typeof (PageNode).FullName ? string.Empty : entry.ItemProvider;
      string itemId = entry.ItemId;
      if (!this.queues.ContainsKey(itemType))
        this.queues.Add(itemType, new Dictionary<string, IEnumerable<IDataItem>>());
      Dictionary<string, IEnumerable<IDataItem>> queue = this.queues[itemType];
      if (!queue.ContainsKey(str))
      {
        List<IDataItem> list = this.GetDataItems(itemType, str).ToList<IDataItem>();
        queue.Add(str, (IEnumerable<IDataItem>) list);
      }
      return queue[str].FirstOrDefault<IDataItem>((Func<IDataItem, bool>) (di => di.Id.ToString() == itemId));
    }

    private IEnumerable<IDataItem> GetDataItems(
      string itemType,
      string providerName)
    {
      if (SystemManager.TypeRegistry.IsRegistered(itemType))
      {
        Type itemType1 = TypeResolutionService.ResolveType(itemType, false);
        if (itemType1 != (Type) null)
        {
          try
          {
            IManager mappedManager = ManagerBase.GetMappedManager(itemType1, providerName);
            if (mappedManager != null)
            {
              StringBuilder builder = new StringBuilder();
              foreach (T obj in this.currentItemsQuery)
              {
                if (providerName.Equals(obj.ItemProvider, StringComparison.OrdinalIgnoreCase) && itemType1.FullName == obj.ItemType)
                  builder = this.FormatContent(builder, obj.ItemId);
              }
              return mappedManager.GetItems(itemType1, builder.ToString(), (string) null, 0, 0).OfType<IDataItem>();
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
      return Enumerable.Empty<IDataItem>();
    }

    private StringBuilder FormatContent(StringBuilder builder, string entry)
    {
      if (builder.Length > 0)
        builder.Append(" OR ");
      if (entry != null)
        builder.AppendFormat("Id = \"{0}\"", (object) entry);
      return builder;
    }

    private void InitializeNextDataItemChunk(Func<int, int, IEnumerable<T>> query = null)
    {
      List<string> supportedTypes = PermissionsFilter.GetCurrentUserRestrictedTypes();
      this.currentItemsQuery = query == null ? (this.queryLoadDelegate == null ? this.mainDataItemsQuery.Skip<T>(this.skip).Take<T>(100).Where<T>((Func<T, bool>) (p => !supportedTypes.Contains(p.ItemType))).ToList<T>() : this.queryLoadDelegate(this.skip, 100).Where<T>((Func<T, bool>) (p => !supportedTypes.Contains(p.ItemType))).ToList<T>()) : query(this.skip, 100).Where<T>((Func<T, bool>) (p => !supportedTypes.Contains(p.ItemType))).ToList<T>();
      this.queues.Clear();
      this.currentItemsQueryEnumerator = (IEnumerator<T>) this.currentItemsQuery.GetEnumerator();
      this.skip += 100;
    }
  }
}
