// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyStatisticSupport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  internal class TaxonomyStatisticSupport : IContentStatisticSupport
  {
    public bool IsReusable => true;

    public StatisticResult GetStatistic(
      Type type,
      string statisticKind,
      string provider,
      string filter = null)
    {
      if (statisticKind.Equals("Count"))
      {
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        if (type.Equals(typeof (Taxon)))
        {
          IQueryable<Taxon> source = manager.GetTaxa<Taxon>();
          Guid taxonomyId = Guid.Empty;
          if (!filter.IsNullOrEmpty() && Guid.TryParse(filter, out taxonomyId))
            source = source.Where<Taxon>((Expression<Func<Taxon, bool>>) (t => t.Taxonomy.Id == taxonomyId));
          return new StatisticResult()
          {
            Kind = "Count",
            Value = (object) source.Count<Taxon>(),
            CacheDependency = (ICacheItemExpiration) new DataItemCacheDependency(typeof (Taxon), taxonomyId != Guid.Empty ? taxonomyId.ToString() : string.Empty)
          };
        }
      }
      return (StatisticResult) null;
    }

    public IEnumerable<IStatisticSupportTypeInfo> GetTypeInfos(
      string moduleName = null)
    {
      return (IEnumerable<IStatisticSupportTypeInfo>) new List<StatisticSupportTypeInfo>()
      {
        new StatisticSupportTypeInfo(typeof (Taxon), new string[1]
        {
          "Count"
        })
        {
          LandingPages = (IEnumerable<StatisticLandingPageInfo>) new StatisticLandingPageInfo[2]
          {
            new StatisticLandingPageInfo(SiteInitializer.FlatTaxonomyPageId, TaxonomyManager.TagsTaxonomyId.ToString()),
            new StatisticLandingPageInfo(SiteInitializer.HierarchicalTaxonomyPageId, TaxonomyManager.CategoriesTaxonomyId.ToString())
          }
        }
      };
    }

    public string GetDefaultProviderName(string moduleName = null) => TaxonomyManager.GetManager().Provider.Name;

    public IEnumerable<string> GetProviderNames(string moduleName = null) => TaxonomyManager.GetManager().GetSiteProviders().Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name));
  }
}
