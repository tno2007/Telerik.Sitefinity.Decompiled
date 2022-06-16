// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncStatisticsLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync
{
  internal class SiteSyncStatisticsLoader : ISiteSyncStatisticsLoader
  {
    public IEnumerable<WrapperObject> LoadStatistics(
      ISiteSyncExportContext context,
      object item,
      ICommonItemLoader commonItemLoader,
      ISiteSyncSerializer serializer,
      string itemType,
      string providerName,
      string language)
    {
      List<WrapperObject> wrapperObjectList = new List<WrapperObject>();
      if (!(item is IDataItem))
        return (IEnumerable<WrapperObject>) wrapperObjectList;
      TaxonomyPropertyDescriptor[] propertiesForType = OrganizerBase.GetPropertiesForType(TypeResolutionService.ResolveType(itemType));
      IEnumerable<string> strings = ((IEnumerable<TaxonomyPropertyDescriptor>) propertiesForType).Select<TaxonomyPropertyDescriptor, string>((Func<TaxonomyPropertyDescriptor, string>) (p => p.TaxonomyProvider)).Distinct<string>();
      List<Guid> taxonIds = new List<Guid>();
      foreach (PropertyDescriptor propertyDescriptor in propertiesForType)
      {
        if (propertyDescriptor.GetValue(item) is IEnumerable<Guid> collection)
          taxonIds.AddRange(collection);
      }
      foreach (string providerName1 in strings)
      {
        IQueryable<TaxonomyStatistic> statistics = TaxonomyManager.GetManager(providerName1).GetStatistics();
        Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (s => s.DataItemType == itemType && taxonIds.Contains(s.TaxonId));
        foreach (TaxonomyStatistic taxonomyStatistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
        {
          WrapperObject so = new WrapperObject((object) taxonomyStatistic);
          commonItemLoader.SetCommonProperties(so, taxonomyStatistic.GetType().FullName, providerName1, DataEventAction.Updated, language);
          so.MappingSettings = serializer.GetTypeMappings(taxonomyStatistic.GetType());
          if (context.TargetMicrosite != null)
            SiteSyncStatisticsLoader.PrepareItemForMigration(context, itemType, taxonomyStatistic, so);
          wrapperObjectList.Add(so);
        }
      }
      return (IEnumerable<WrapperObject>) wrapperObjectList;
    }

    private static void PrepareItemForMigration(
      ISiteSyncExportContext context,
      string itemType,
      TaxonomyStatistic statistic,
      WrapperObject so)
    {
      object mapping = context.GetMapping(itemType, (object) statistic.ItemProviderName, "Provider");
      if (mapping == null)
        return;
      so.SetProperty("ItemProviderName", mapping);
    }
  }
}
