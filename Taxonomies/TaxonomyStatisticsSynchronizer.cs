// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyStatisticsSynchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  internal class TaxonomyStatisticsSynchronizer
  {
    public void Syncronize(Type itemType, string[] providers)
    {
      TaxonomyPropertyDescriptor[] propertyDescriptorArray = typeof (IDynamicFieldsContainer).IsAssignableFrom(itemType) ? OrganizerBase.GetPropertiesForType(itemType) : throw new System.InvalidOperationException("The type '{0}' is not assignable from '{1}'".Arrange((object) itemType.FullName, (object) typeof (IDynamicFieldsContainer).FullName));
      if (propertyDescriptorArray == null || propertyDescriptorArray.Length == 0)
        return;
      TaxonomyStatisticsSynchronizer.IMarkedItemsResolver markedItemsResolver = !typeof (Content).IsAssignableFrom(itemType) ? (!typeof (DynamicContent).IsAssignableFrom(itemType) ? (TaxonomyStatisticsSynchronizer.IMarkedItemsResolver) new TaxonomyStatisticsSynchronizer.DefaultMarkedItemsResolver(itemType) : (TaxonomyStatisticsSynchronizer.IMarkedItemsResolver) new TaxonomyStatisticsSynchronizer.DynamicContentMarkedItemsResolver(itemType)) : (TaxonomyStatisticsSynchronizer.IMarkedItemsResolver) new TaxonomyStatisticsSynchronizer.ContentMarkedItemsResolver(itemType);
      foreach (TaxonomyPropertyDescriptor property in propertyDescriptorArray)
      {
        TaxonomyManager manager = TaxonomyManager.GetManager(property.TaxonomyProvider);
        ITaxonomy taxonomy = manager.GetTaxonomy(property.TaxonomyId);
        List<Guid> list1 = manager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => t.Taxonomy == taxonomy)).Select<Taxon, Guid>((Expression<Func<Taxon, Guid>>) (t => t.Id)).ToList<Guid>();
        List<string> list2 = ManagerBase.GetMappedManager(itemType).GetProviderNames(ProviderBindingOptions.NoFilter).ToList<string>();
        if (providers != null && providers.Length != 0)
          list2 = list2.Intersect<string>((IEnumerable<string>) providers).ToList<string>();
        foreach (string providerName in list2)
        {
          IManager mappedManager = ManagerBase.GetMappedManager(itemType, providerName);
          foreach (Guid guid in list1)
          {
            Guid taxonId = guid;
            int num = 3;
            while (num > 0)
            {
              try
              {
                bool flag = false;
                foreach (TaxonomyStatisticsSynchronizer.StatInfo statInfo in markedItemsResolver.GetMarkedItemsCount(property, mappedManager, taxonId))
                {
                  string dataItemType = itemType.FullName;
                  string itemProviderName = mappedManager.Provider.Name;
                  ContentLifecycleStatus statisticType = statInfo.Status;
                  TaxonomyStatistic statistic = manager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.DataItemType == dataItemType && st.TaxonId == taxonId && (int) st.StatisticType == (int) statisticType && st.ItemProviderName == itemProviderName)).FirstOrDefault<TaxonomyStatistic>();
                  if (statistic != null)
                  {
                    if (statisticType == ContentLifecycleStatus.Temp)
                    {
                      manager.Provider.DeleteStatistic(statistic);
                      flag = true;
                    }
                    if ((long) statistic.MarkedItemsCount == (long) statInfo.Count)
                    {
                      if (statistic.MarkedItemsCount == 0U)
                      {
                        manager.Provider.DeleteStatistic(statistic);
                        flag = true;
                        continue;
                      }
                      continue;
                    }
                  }
                  else if (statisticType != ContentLifecycleStatus.Temp && statInfo.Count != 0)
                  {
                    statistic = manager.Provider.CreateStatistic(itemType, taxonomy.Id, taxonId);
                    statistic.StatisticType = statisticType;
                    statistic.ItemProviderName = itemProviderName;
                  }
                  else
                    continue;
                  statistic.MarkedItemsCount = (uint) statInfo.Count;
                  flag = true;
                }
                if (flag)
                  manager.SaveChanges();
                num = 0;
              }
              catch (OptimisticVerificationException ex)
              {
                manager.Provider.RollbackTransaction();
                --num;
              }
              catch
              {
                manager.Provider.RollbackTransaction();
                break;
              }
            }
          }
        }
      }
    }

    private interface IMarkedItemsResolver
    {
      IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo> GetMarkedItemsCount(
        TaxonomyPropertyDescriptor property,
        IManager manager,
        Guid taxonId);
    }

    private class MarkedItemsResolver<T> : TaxonomyStatisticsSynchronizer.IMarkedItemsResolver
      where T : IDynamicFieldsContainer
    {
      private Type itemType;

      public MarkedItemsResolver(Type itemType) => this.itemType = itemType;

      public virtual IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo> GetMarkedItemsCount(
        TaxonomyPropertyDescriptor property,
        IManager manager,
        Guid taxonId)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TaxonomyStatisticsSynchronizer.MarkedItemsResolver<T>.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new TaxonomyStatisticsSynchronizer.MarkedItemsResolver<T>.\u003C\u003Ec__DisplayClass1_0();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10.taxonId = taxonId;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10.propertyName = property.Name;
        IQueryable<T> items = manager.GetItems(this.itemType, string.Empty, string.Empty, 0, 0) as IQueryable<T>;
        IQueryable<T> query;
        if (property.MetaField.IsSingleTaxon)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          query = items.Where<T>((Expression<Func<T, bool>>) (i => i.GetValue<Guid>(cDisplayClass10.propertyName) == cDisplayClass10.taxonId));
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          query = items.Where<T>((Expression<Func<T, bool>>) (i => i.GetValue<IList<Guid>>(cDisplayClass10.propertyName).Any<Guid>((Func<Guid, bool>) (t => t == cDisplayClass10.taxonId))));
        }
        return this.GeItemsCount(query);
      }

      protected virtual IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo> GeItemsCount(
        IQueryable<T> query)
      {
        return (IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo>) new TaxonomyStatisticsSynchronizer.StatInfo[1]
        {
          new TaxonomyStatisticsSynchronizer.StatInfo()
          {
            Status = ContentLifecycleStatus.Master,
            Count = query.Count<T>()
          }
        };
      }
    }

    private class DefaultMarkedItemsResolver : 
      TaxonomyStatisticsSynchronizer.MarkedItemsResolver<IDynamicFieldsContainer>
    {
      public DefaultMarkedItemsResolver(Type itemType)
        : base(itemType)
      {
      }
    }

    private class ContentMarkedItemsResolver : 
      TaxonomyStatisticsSynchronizer.MarkedItemsResolver<Content>
    {
      public ContentMarkedItemsResolver(Type itemType)
        : base(itemType)
      {
      }

      protected override IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo> GeItemsCount(
        IQueryable<Content> query)
      {
        return (IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo>) new TaxonomyStatisticsSynchronizer.StatInfo[3]
        {
          this.GetContentStat(query, ContentLifecycleStatus.Live),
          this.GetContentStat(query, ContentLifecycleStatus.Master),
          this.GetContentStat(query, ContentLifecycleStatus.Temp)
        };
      }

      private TaxonomyStatisticsSynchronizer.StatInfo GetContentStat(
        IQueryable<Content> query,
        ContentLifecycleStatus status)
      {
        TaxonomyStatisticsSynchronizer.StatInfo contentStat = new TaxonomyStatisticsSynchronizer.StatInfo();
        contentStat.Status = status;
        contentStat.Count = query.Where<Content>((Expression<Func<Content, bool>>) (c => (int) c.Status == (int) status)).Count<Content>();
        return contentStat;
      }
    }

    private class DynamicContentMarkedItemsResolver : 
      TaxonomyStatisticsSynchronizer.MarkedItemsResolver<DynamicContent>
    {
      public DynamicContentMarkedItemsResolver(Type itemType)
        : base(itemType)
      {
      }

      protected override IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo> GeItemsCount(
        IQueryable<DynamicContent> query)
      {
        return (IEnumerable<TaxonomyStatisticsSynchronizer.StatInfo>) new TaxonomyStatisticsSynchronizer.StatInfo[3]
        {
          this.GetContentStat(query, ContentLifecycleStatus.Live),
          this.GetContentStat(query, ContentLifecycleStatus.Master),
          this.GetContentStat(query, ContentLifecycleStatus.Temp)
        };
      }

      private TaxonomyStatisticsSynchronizer.StatInfo GetContentStat(
        IQueryable<DynamicContent> query,
        ContentLifecycleStatus status)
      {
        TaxonomyStatisticsSynchronizer.StatInfo contentStat = new TaxonomyStatisticsSynchronizer.StatInfo();
        contentStat.Status = status;
        contentStat.Count = query.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (c => (int) c.Status == (int) status)).Count<DynamicContent>();
        return contentStat;
      }
    }

    private class StatInfo
    {
      public ContentLifecycleStatus Status { get; set; }

      public int Count { get; set; }
    }
  }
}
