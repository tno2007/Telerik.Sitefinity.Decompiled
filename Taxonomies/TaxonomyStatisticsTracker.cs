// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyStatisticsTracker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Implements logic for tracking changes for taxonomy statistics
  /// </summary>
  internal class TaxonomyStatisticsTracker : ITracker
  {
    private Dictionary<string, TaxonStatDelta> delta = new Dictionary<string, TaxonStatDelta>();
    public const string TaxonomyStatisticsChangesStateKey = "taxonomy_statistics_changes";

    public void Track(object item, DataProviderBase provider)
    {
      if (SystemManager.DataTrackingDisabled || !(item is IDynamicFieldsContainer dynamicFieldsContainer))
        return;
      ILifecycleDataItem lifecycleDataItem1 = item as ILifecycleDataItem;
      ContentLifecycleStatus staisticType = ContentLifecycleStatus.Live;
      if (lifecycleDataItem1 != null)
      {
        staisticType = lifecycleDataItem1.Status == ContentLifecycleStatus.Deleted ? ContentLifecycleStatus.Master : lifecycleDataItem1.Status;
        if (staisticType == ContentLifecycleStatus.Temp)
          return;
      }
      if (item is IUserProfile || item is IPageNode)
        staisticType = ContentLifecycleStatus.Master;
      if (!(provider.GetTransaction() is SitefinityOAContext transaction))
        return;
      bool flag1 = false;
      bool flag2 = false;
      if (item is IRecyclableDataItem recyclableDataItem)
      {
        flag1 = recyclableDataItem.IsDeleted;
        if (!flag1)
        {
          if (lifecycleDataItem1 != null)
          {
            if (lifecycleDataItem1.Status == ContentLifecycleStatus.Live)
            {
              if (item is IRecyclableLifecycleDataItem lifecycleDataItem2)
              {
                bool? originalValue = transaction.GetOriginalValue<bool?>(item, "WasPublished");
                flag2 = !lifecycleDataItem2.WasPublished.HasValue && originalValue.HasValue;
              }
              else
              {
                bool originalValue = transaction.GetOriginalValue<bool>(item, "Visible");
                flag2 = lifecycleDataItem1.Visible && !originalValue;
              }
            }
            else if (lifecycleDataItem1.Status == ContentLifecycleStatus.Master)
              flag2 = transaction.GetOriginalValue<ContentLifecycleStatus>(item, "Status") == ContentLifecycleStatus.Deleted;
          }
          else if (recyclableDataItem is IPageNode)
            flag2 = transaction.GetOriginalValue<bool>(item, "IsDeleted");
        }
      }
      SecurityConstants.TransactionActionType dirtyItemStatus = provider.GetDirtyItemStatus(item);
      Type type = item.GetType();
      TaxonomyPropertyDescriptor[] propertiesForType = OrganizerBase.GetPropertiesForType(type);
      if (propertiesForType == null || propertiesForType.Length == 0)
        return;
      foreach (TaxonomyPropertyDescriptor property in propertiesForType)
      {
        if (!flag1 && !flag2 && !transaction.IsFieldDirty((object) dynamicFieldsContainer, property.Name) && (dirtyItemStatus != SecurityConstants.TransactionActionType.Deleted || recyclableDataItem != null))
          break;
        if (property.MetaField.IsSingleTaxon)
        {
          Guid guid = dynamicFieldsContainer.GetValue<Guid>(property.Name);
          Guid originalValue = transaction.GetOriginalValue<Guid>((object) dynamicFieldsContainer, property.Name);
          if (originalValue != guid)
          {
            this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) new Guid[1]
            {
              originalValue
            }, -1);
            if (dirtyItemStatus != SecurityConstants.TransactionActionType.Deleted)
              this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) new Guid[1]
              {
                guid
              }, 1);
          }
          else if (flag1)
            this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) new Guid[1]
            {
              guid
            }, -1);
          else if (flag2)
            this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) new Guid[1]
            {
              guid
            }, 1);
        }
        else
        {
          TrackedList<Guid> trackedList1 = dynamicFieldsContainer.GetValue<TrackedList<Guid>>(property.Name);
          if (trackedList1 != null)
          {
            List<Guid> taxons1 = new List<Guid>();
            List<Guid> taxons2 = new List<Guid>();
            if (dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted | flag1)
            {
              taxons1.AddRange((IEnumerable<Guid>) trackedList1);
            }
            else
            {
              List<Guid> list = trackedList1.ToList<Guid>();
              TrackedList<Guid> trackedList2 = transaction.GetOriginalValue<TrackedList<Guid>>((object) dynamicFieldsContainer, property.Name);
              if (flag2)
                trackedList2 = (TrackedList<Guid>) null;
              if (trackedList2 != null)
              {
                foreach (Guid guid in trackedList2)
                {
                  if (!list.Contains(guid))
                    taxons1.Add(guid);
                }
                foreach (Guid guid in list)
                {
                  if (!trackedList2.Contains(guid))
                    taxons2.Add(guid);
                }
              }
              else
                taxons2.AddRange((IEnumerable<Guid>) list);
            }
            if (taxons2.Count > 0)
              this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) taxons2, 1);
            if (taxons1.Count > 0)
              this.Update(property, type, provider.Name, staisticType, (IEnumerable<Guid>) taxons1, -1);
          }
        }
      }
    }

    public void SaveChanges()
    {
      IEnumerable<TaxonStatDelta> changesQuery = this.ChangesQuery;
      if (changesQuery.Count<TaxonStatDelta>() <= 0)
        return;
      foreach (TaxonStatDelta taxonStatDelta in changesQuery)
      {
        int num = 20;
        while (num > 0)
        {
          TaxonomyManager manager = TaxonomyManager.GetManager(taxonStatDelta.TaxonomyProvider);
          try
          {
            manager.AdjustMarkedItemsCount(taxonStatDelta.DataItemType, taxonStatDelta.TaxonomyId, taxonStatDelta.TaxonId, taxonStatDelta.MarkedItemsCountDelta, taxonStatDelta.ItemProviderName, taxonStatDelta.StatisticType);
            manager.SaveChanges();
            num = 0;
          }
          catch (OptimisticVerificationException ex)
          {
            manager.Provider.RollbackTransaction();
            --num;
            if (num == 0)
            {
              Exception exceptionToHandle = new Exception("Failed to update taxonomy statistic with: {0} for DataItemType: {1} ({2}) and taxon: {3}".Arrange((object) taxonStatDelta.MarkedItemsCountDelta, (object) taxonStatDelta.DataItemType.FullName, (object) taxonStatDelta.ItemProviderName, (object) taxonStatDelta.TaxonId), (Exception) ex);
              if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
                throw exceptionToHandle;
            }
            else if (num > 2)
              Thread.Sleep(new Random().Next(0, 500));
          }
        }
      }
    }

    public bool HasChanges() => this.ChangesQuery.Any<TaxonStatDelta>();

    private IEnumerable<TaxonStatDelta> ChangesQuery => this.delta.Values.Where<TaxonStatDelta>((Func<TaxonStatDelta, bool>) (d => (uint) d.MarkedItemsCountDelta > 0U));

    internal void Update(
      TaxonomyPropertyDescriptor property,
      Type itemType,
      string itemProviderName,
      ContentLifecycleStatus staisticType,
      IEnumerable<Guid> taxons,
      int change)
    {
      string str = itemType.ToString() + itemProviderName + property.TaxonomyProvider + property.TaxonomyId.ToString() + ((int) staisticType).ToString();
      foreach (Guid taxon in taxons)
      {
        string key = str + taxon.ToString();
        TaxonStatDelta taxonStatDelta;
        if (!this.delta.TryGetValue(key, out taxonStatDelta))
        {
          Guid guid = ObjectFactory.Resolve<MultisiteTaxonomiesResolver>().ResolveSiteTaxonomyId(property.TaxonomyId);
          taxonStatDelta = new TaxonStatDelta()
          {
            DataItemType = itemType,
            ItemProviderName = itemProviderName,
            TaxonomyProvider = property.TaxonomyProvider,
            TaxonomyId = guid,
            TaxonId = taxon,
            StatisticType = staisticType
          };
          this.delta.Add(key, taxonStatDelta);
        }
        taxonStatDelta.MarkedItemsCountDelta += change;
      }
    }

    internal bool SkipAutoTracking { get; set; }
  }
}
