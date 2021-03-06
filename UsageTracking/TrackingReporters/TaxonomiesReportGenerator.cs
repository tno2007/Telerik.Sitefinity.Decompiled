// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.TrackingReporters.TaxonomiesReportGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.UsageTracking.Model;

namespace Telerik.Sitefinity.UsageTracking.TrackingReporters
{
  internal class TaxonomiesReportGenerator
  {
    internal const string TaxonomiesModuleName = "Taxonomies";

    public TaxonomiesReportModel GenerateReport()
    {
      TaxonomiesReportModel report = new TaxonomiesReportModel()
      {
        ModuleName = "Taxonomies"
      };
      TaxonomyManager manager = TaxonomyManager.GetManager();
      report.CustomHierarchicalTaxonomiesCount = manager.GetTaxonomies<HierarchicalTaxonomy>().Where<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Id != TaxonomyManager.CategoriesTaxonomyId && t.Id != TaxonomyManager.DepartmentsTaxonomyId && t.Id != SiteInitializer.PageTemplatesTaxonomyId && t.RootTaxonomyId == new Guid?())).Count<HierarchicalTaxonomy>();
      report.CustomFlatTaxonomiesCount = manager.GetTaxonomies<FlatTaxonomy>().Where<FlatTaxonomy>((Expression<Func<FlatTaxonomy, bool>>) (t => t.Id != TaxonomyManager.TagsTaxonomyId && t.RootTaxonomyId == new Guid?())).Count<FlatTaxonomy>();
      report.TagsCount = manager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.TaxonomyId == TaxonomyManager.TagsTaxonomyId || t.Taxonomy.RootTaxonomyId == (Guid?) TaxonomyManager.TagsTaxonomyId)).Count<FlatTaxon>();
      report.CategoriesCount = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.TaxonomyId == TaxonomyManager.CategoriesTaxonomyId || t.Taxonomy.RootTaxonomyId == (Guid?) TaxonomyManager.CategoriesTaxonomyId)).Count<HierarchicalTaxon>();
      report.DepartmentsCount = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.TaxonomyId == TaxonomyManager.DepartmentsTaxonomyId || t.Taxonomy.RootTaxonomyId == (Guid?) TaxonomyManager.DepartmentsTaxonomyId)).Count<HierarchicalTaxon>();
      report.FlatTaxaCount = manager.GetTaxa<FlatTaxon>().Count<FlatTaxon>();
      report.HierarchicalTaxaCount = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.TaxonomyId != SiteInitializer.PageTemplatesTaxonomyId)).Count<HierarchicalTaxon>();
      report.SplitTaxonomiesCount = manager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId != new Guid?())).Count<Taxonomy>();
      report.SynonymsTotalCount = manager.GetSynonyms().Count<Synonym>();
      report.AppliedToItemsTotalCount = manager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => (int) s.StatisticType == 0)).Sum<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, long>>) (st => (long) st.MarkedItemsCount));
      return report;
    }
  }
}
