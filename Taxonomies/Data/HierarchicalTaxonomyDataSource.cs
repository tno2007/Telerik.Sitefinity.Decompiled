// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Data.HierarchicalTaxonomyDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Data
{
  public class HierarchicalTaxonomyDataSource
  {
    private List<HierarchicalTaxonomyDataItem> dataSource;

    public HierarchicalTaxonomyDataSource() => this.dataSource = new List<HierarchicalTaxonomyDataItem>();

    public string TaxonomyProvider { get; set; }

    public Guid RootTaxonomyId { get; set; }

    public List<HierarchicalTaxonomyDataItem> DataSource => this.dataSource;

    protected virtual void RecurseNodes<T>(Taxon parent) where T : Taxon
    {
      this.dataSource.Add(new HierarchicalTaxonomyDataItem((ITaxon) parent)
      {
        Id = parent.Id,
        ParentId = parent.Parent != null ? parent.Parent.Id : Guid.Empty,
        Title = parent.Title,
        Expanded = true
      });
      TaxonomyManager manager = TaxonomyManager.GetManager(this.TaxonomyProvider);
      if (typeof (T) == typeof (FlatTaxon))
      {
        IQueryable<FlatTaxon> taxa = manager.GetTaxa<FlatTaxon>();
        Expression<Func<FlatTaxon, bool>> predicate = (Expression<Func<FlatTaxon, bool>>) (t => t.Parent == parent);
        foreach (Taxon parent1 in (IEnumerable<FlatTaxon>) taxa.Where<FlatTaxon>(predicate))
          this.RecurseNodes<FlatTaxon>(parent1);
      }
      else
      {
        IQueryable<HierarchicalTaxon> taxa = manager.GetTaxa<HierarchicalTaxon>();
        Expression<Func<HierarchicalTaxon, bool>> predicate = (Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == parent);
        foreach (Taxon parent2 in (IEnumerable<HierarchicalTaxon>) taxa.Where<HierarchicalTaxon>(predicate))
          this.RecurseNodes<HierarchicalTaxon>(parent2);
      }
    }

    public virtual void BuildDataSource<T>() where T : Taxon
    {
      this.dataSource.Clear();
      Guid tId = this.RootTaxonomyId;
      TaxonomyManager manager = TaxonomyManager.GetManager(this.TaxonomyProvider);
      if (typeof (T) == typeof (FlatTaxon))
      {
        IQueryable<FlatTaxon> taxa = manager.GetTaxa<FlatTaxon>();
        Expression<Func<FlatTaxon, bool>> predicate = (Expression<Func<FlatTaxon, bool>>) (t => t.Parent == default (object) && t.Taxonomy.Id == tId);
        foreach (Taxon parent in (IEnumerable<FlatTaxon>) taxa.Where<FlatTaxon>(predicate))
          this.RecurseNodes<FlatTaxon>(parent);
      }
      else
      {
        IQueryable<HierarchicalTaxon> taxa = manager.GetTaxa<HierarchicalTaxon>();
        Expression<Func<HierarchicalTaxon, bool>> predicate = (Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == default (object) && t.Taxonomy.Id == tId);
        foreach (Taxon parent in (IEnumerable<HierarchicalTaxon>) taxa.Where<HierarchicalTaxon>(predicate))
          this.RecurseNodes<HierarchicalTaxon>(parent);
      }
    }
  }
}
