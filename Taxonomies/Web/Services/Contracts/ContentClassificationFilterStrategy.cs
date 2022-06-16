// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Contracts.ContentClassificationFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Contracts
{
  internal class ContentClassificationFilterStrategy : IFilterStrategy
  {
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      IEnumerable<TaxonomyPropertyDescriptor> source = TypeDescriptor.GetProperties(itemType).OfType<TaxonomyPropertyDescriptor>();
      IEnumerable<Guid> taxIds = source.Select<TaxonomyPropertyDescriptor, Guid>((Func<TaxonomyPropertyDescriptor, Guid>) (x => x.TaxonomyId));
      IQueryable<Taxonomy> taxonomies = TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>();
      Expression<Func<Taxonomy, bool>> predicate = (Expression<Func<Taxonomy, bool>>) (x => taxIds.Contains<Guid>(x.Id));
      foreach (Taxonomy taxonomy1 in (IEnumerable<Taxonomy>) taxonomies.Where<Taxonomy>(predicate))
      {
        Taxonomy taxonomy = taxonomy1;
        TaxonomyPropertyDescriptor propertyDescriptor = source.FirstOrDefault<TaxonomyPropertyDescriptor>((Func<TaxonomyPropertyDescriptor, bool>) (x => x.TaxonomyId == taxonomy.Id));
        string str = propertyDescriptor.MetaField == null || string.IsNullOrEmpty(propertyDescriptor.MetaField.Title) ? taxonomy.TaxonName.GetString(SystemManager.CurrentContext.Culture, true) : propertyDescriptor.MetaField.Title;
        filters.Add(new FilterItem()
        {
          Name = propertyDescriptor.Name,
          Title = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Res.Get<Labels>().FilterTitle, (object) str),
          Parameters = new FilterParameters((IDictionary<string, object>) new Dictionary<string, object>()
          {
            {
              "Type",
              (object) "call"
            },
            {
              "ContentSingularName",
              (object) taxonomy.TaxonName.ToString()
            },
            {
              "ContentPluralName",
              (object) taxonomy.Name
            }
          })
        });
      }
      return (IEnumerable<FilterItem>) filters;
    }

    public IEnumerable<Result> GetValues(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      int skip,
      int take,
      string search,
      ISet<string> parameters,
      ref int? totalCount)
    {
      TaxonomyPropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(itemType).Find(filter, false) as TaxonomyPropertyDescriptor;
      Type relatedType = this.GetTaxaType(propertyDescriptor.TaxonomyType);
      TaxonomyManager manager = TaxonomyManager.GetManager(propertyDescriptor.TaxonomyProvider);
      string orderExpression = (string) null;
      if (!string.IsNullOrEmpty(search))
        orderExpression = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} ASC", (object) "Title");
      string filterExpression = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "TaxonomyId == {0}", (object) MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomyId(propertyDescriptor.TaxonomyId));
      if (!string.IsNullOrEmpty(search))
        filterExpression += string.Format((IFormatProvider) CultureInfo.InvariantCulture, " AND ({0}.StartsWith(\"{1}\") OR {0}.Contains(\" {1}\"))", (object) "Title", (object) search);
      IQueryable<Taxon> queryable = manager.GetItems(relatedType, filterExpression, orderExpression, skip, take, ref totalCount) as IQueryable<Taxon>;
      List<Guid> itemIds = parameters.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>();
      if (itemIds.Count > 0)
        queryable = queryable.Where<Taxon>((Expression<Func<Taxon, bool>>) (x => itemIds.Contains(x.Id)));
      if (typeof (HierarchicalTaxon).IsAssignableFrom(relatedType))
        queryable = queryable.Include<Taxon>((Expression<Func<Taxon, object>>) (x => x.Parent));
      return queryable.ToList<Taxon>().Select<Taxon, Result>((Func<Taxon, Result>) (taxon =>
      {
        Result values = new Result(taxon.Id.ToString(), (string) taxon.Title);
        if (typeof (HierarchicalTaxon).IsAssignableFrom(relatedType))
        {
          HierarchicalTaxon hierarchicalTaxon = taxon as HierarchicalTaxon;
          values.Description = hierarchicalTaxon.Parent == null ? "On top level" : string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Under {0}", (object) hierarchicalTaxon.Parent.Title);
        }
        return values;
      }));
    }

    public bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      filteredItemsIDs = (IEnumerable<Guid>) null;
      return false;
    }

    public bool TryToFilterByQuery(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      IQueryable query,
      out IQueryable resultQuery)
    {
      List<Guid> list = parameters.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>();
      TaxonomyPropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(itemType).Find(filter, false) as TaxonomyPropertyDescriptor;
      bool flag = false;
      if (propertyDescriptor != null)
        flag = propertyDescriptor.MetaField.IsSingleTaxon;
      if (flag)
      {
        string predicate = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "({0}==(@0))", (object) filter);
        query = query.Where(predicate, (object) list.FirstOrDefault<Guid>());
      }
      else
      {
        string format = "{0}.Contains((@{1}))";
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("(");
        for (int index = 0; index < list.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(" Or ");
          stringBuilder.Append(string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) filter, (object) index));
        }
        stringBuilder.Append(")");
        query = query.Where(stringBuilder.ToString(), list.Cast<object>().ToArray<object>());
      }
      resultQuery = query;
      return true;
    }

    private Type GetTaxaType(TaxonomyType taxonomyType) => taxonomyType != TaxonomyType.Flat ? typeof (HierarchicalTaxon) : typeof (FlatTaxon);
  }
}
