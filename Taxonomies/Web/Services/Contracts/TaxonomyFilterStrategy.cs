// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Contracts.TaxonomyFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Contracts
{
  internal class TaxonomyFilterStrategy : IDynamicFilterStrategy, IFilterStrategy
  {
    private const string NotUsedFilterName = "not-used";
    private const string FilterFlatTaxaFirstLetter = "FilterFlatTaxaFirstLetter";
    private const string FirstLetterCacheKey = "sf_taxa_first_letter_";
    private const string IgnoreSiteContextParam = "sf_ignore_site_context";
    private static readonly object FirstLetterCacheSync = new object();

    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (typeof (Taxonomy).IsAssignableFrom(itemType))
      {
        filters.Add(new FilterItem()
        {
          Name = TaxonomyType.Flat.ToString().ToLower(),
          Title = Res.Get<TaxonomyResources>().SimpleLists
        });
        filters.Add(new FilterItem()
        {
          Name = TaxonomyType.Hierarchical.ToString().ToLower(),
          Title = Res.Get<TaxonomyResources>().HierarchicalLists
        });
        if (SystemManager.CurrentContext.IsMultisiteMode)
          filters.Add(new FilterItem()
          {
            Name = "not-used",
            Title = Res.Get<TaxonomyResources>().NotUsedTaxonomies
          });
      }
      if (typeof (FlatTaxon).IsAssignableFrom(itemType))
        filters.Add(new FilterItem()
        {
          Name = "FilterFlatTaxaFirstLetter",
          Category = "FilterFlatTaxaFirstLetter",
          IsDynamicFilter = true
        });
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
      return Enumerable.Empty<Result>();
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
      resultQuery = (IQueryable) null;
      if (!string.IsNullOrWhiteSpace(filter))
      {
        using (new CultureRegion(culture != null ? culture : SystemManager.CurrentContext.Culture))
        {
          if (filter.Equals("not-used", StringComparison.OrdinalIgnoreCase))
          {
            TaxonomyManager manager = TaxonomyManager.GetManager(providerName);
            resultQuery = (IQueryable) manager.FilterByNotUsedTaxonomies();
          }
          else if (typeof (FlatTaxon).IsAssignableFrom(itemType) && filter.Equals("FilterFlatTaxaFirstLetter") && parameters.Count<string>() > 0)
          {
            string filterValue = parameters.First<string>();
            resultQuery = (IQueryable) this.GetStartsWithQueryForCurrentCulture(query, filterValue);
          }
          else
          {
            TaxonomyType result;
            if (Enum.TryParse<TaxonomyType>(filter, true, out result))
            {
              switch (result)
              {
                case TaxonomyType.Flat:
                  resultQuery = (IQueryable) Queryable.OfType<FlatTaxonomy>(query);
                  break;
                case TaxonomyType.Hierarchical:
                  resultQuery = (IQueryable) Queryable.OfType<HierarchicalTaxonomy>(query);
                  break;
                default:
                  resultQuery = (IQueryable) null;
                  break;
              }
            }
          }
        }
      }
      return resultQuery != null;
    }

    public IEnumerable<FilterItem> GetDynamicFilters(
      Type itemType,
      string providerName,
      CultureInfo culture,
      Guid? parentId)
    {
      dynamicFilters1 = new List<FilterItem>();
      if (typeof (FlatTaxon).IsAssignableFrom(itemType) && parentId.HasValue)
      {
        if (SystemManager.CurrentContext.IsMultisiteMode)
        {
          NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
          if (!queryString.Keys.Contains("sf_ignore_site_context", StringComparison.OrdinalIgnoreCase) || !queryString["sf_ignore_site_context"].Equals("true", StringComparison.OrdinalIgnoreCase))
          {
            TaxonomyManager manager = TaxonomyManager.GetManager(providerName);
            ITaxonomy taxonomy = manager.GetTaxonomy(parentId.Value);
            Guid taxonomyId = taxonomy.RootTaxonomyId.HasValue ? taxonomy.RootTaxonomyId.Value : taxonomy.Id;
            parentId = new Guid?(MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomyId(taxonomyId));
          }
        }
        CultureInfo currentCulture = culture != null ? culture : SystemManager.CurrentContext.Culture;
        string key = "sf_taxa_first_letter_" + parentId.Value.ToString() + providerName + currentCulture.Name;
        ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
        if (!(cacheManager[key] is List<FilterItem> dynamicFilters1))
        {
          lock (TaxonomyFilterStrategy.FirstLetterCacheSync)
          {
            if (!(cacheManager["sf_taxa_first_letter_"] is List<FilterItem> dynamicFilters1))
            {
              dynamicFilters1 = this.GetFirstLetterFilterItems(itemType, providerName, parentId.Value, currentCulture);
              cacheManager.Add(key, (object) dynamicFilters1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Taxon), parentId.Value.ToString()), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
            }
          }
        }
      }
      return (IEnumerable<FilterItem>) dynamicFilters1;
    }

    private List<FilterItem> GetFirstLetterFilterItems(
      Type itemType,
      string providerName,
      Guid parentId,
      CultureInfo currentCulture)
    {
      List<FilterItem> letterFilterItems = new List<FilterItem>();
      using (new CultureRegion(currentCulture))
      {
        if (typeof (FlatTaxon).IsAssignableFrom(itemType))
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          IQueryable<\u003C\u003Ef__AnonymousType12<string, int>> source = this.GetFirstLettersForCurrentCulture(providerName, parentId).GroupBy<string, string>(Expression.Lambda<Func<string, string>>((Expression) Expression.Call(t, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToUpper)), Array.Empty<Expression>()), parameterExpression)).Select(g => new
          {
            Key = g.Key,
            Count = g.Count<string>()
          });
          Expression<Func<\u003C\u003Ef__AnonymousType12<string, int>, string>> keySelector = g => g.Key;
          foreach (var data in source.OrderBy(keySelector))
            letterFilterItems.Add(new FilterItem()
            {
              Name = "FilterFlatTaxaFirstLetter," + data.Key,
              Title = data.Key.ToString(),
              Count = data.Count,
              Category = "FilterFlatTaxaFirstLetter"
            });
        }
      }
      return letterFilterItems;
    }

    private IQueryable<FlatTaxon> GetStartsWithQueryForCurrentCulture(
      IQueryable query,
      string filterValue)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.filterValue = filterValue;
      ParameterExpression parameterExpression1;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      IQueryable<FlatTaxon> source1 = Queryable.Cast<FlatTaxon>(query).Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.OrElse((Expression) Expression.Call((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.StartsWith)), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0)), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0.filterValue))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToUpper)), Array.Empty<Expression>())), (Expression) Expression.Call((Expression) Expression.Call((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Taxon.get_Title))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.StartsWith)), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0)), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0.filterValue))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()))), parameterExpression1));
      IQueryable<FlatTaxon> queryable = (IQueryable<FlatTaxon>) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && SystemManager.CurrentContext.Culture.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name)
      {
        using (new CultureRegion(CultureInfo.InvariantCulture))
        {
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          queryable = Queryable.Cast<FlatTaxon>(query).Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.OrElse((Expression) Expression.Call((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.StartsWith)), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0)), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0.filterValue))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToUpper)), Array.Empty<Expression>())), (Expression) Expression.Call((Expression) Expression.Call((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Taxon.get_Title))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.StartsWith)), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0)), FieldInfo.GetFieldFromHandle(__fieldref (TaxonomyFilterStrategy.\u003C\u003Ec__DisplayClass6_0.filterValue))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()))), parameterExpression2));
        }
        foreach (CultureInfo frontendLanguage in SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages)
        {
          using (new CultureRegion(frontendLanguage))
          {
            ParameterExpression parameterExpression3;
            // ISSUE: method reference
            queryable = queryable.Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.Equal((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Constant((object) null, typeof (string))), parameterExpression3));
          }
        }
      }
      return queryable == null ? source1 : source1.Union<FlatTaxon>((IEnumerable<FlatTaxon>) queryable);
    }

    private IQueryable<string> GetFirstLettersForCurrentCulture(
      string providerName,
      Guid taxonomyId)
    {
      IQueryable<FlatTaxon> source1 = TaxonomyManager.GetManager(providerName).GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.TaxonomyId == taxonomyId));
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      IQueryable<string> source1_1 = Queryable.Cast<FlatTaxon>(source1).Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.NotEqual((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Constant((object) null, typeof (string))), parameterExpression1)).Select<FlatTaxon, string>(Expression.Lambda<Func<FlatTaxon, string>>((Expression) Expression.Call((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Substring)), new Expression[2]
      {
        (Expression) Expression.Constant((object) 0, typeof (int)),
        (Expression) Expression.Constant((object) 1, typeof (int))
      }), parameterExpression2));
      IQueryable<string> source2 = (IQueryable<string>) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && SystemManager.CurrentContext.Culture.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name)
      {
        IQueryable<FlatTaxon> source3 = (IQueryable<FlatTaxon>) null;
        using (new CultureRegion(CultureInfo.InvariantCulture))
        {
          ParameterExpression parameterExpression3;
          // ISSUE: method reference
          source3 = Queryable.Cast<FlatTaxon>(source1).Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.NotEqual((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Constant((object) null, typeof (string))), parameterExpression3));
        }
        foreach (CultureInfo frontendLanguage in SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages)
        {
          using (new CultureRegion(frontendLanguage))
          {
            ParameterExpression parameterExpression4;
            // ISSUE: method reference
            source3 = source3.Where<FlatTaxon>(Expression.Lambda<Func<FlatTaxon, bool>>((Expression) Expression.Equal((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Constant((object) null, typeof (string))), parameterExpression4));
          }
        }
        if (source3 != null)
        {
          using (new CultureRegion(CultureInfo.InvariantCulture))
          {
            ParameterExpression parameterExpression5;
            // ISSUE: method reference
            // ISSUE: method reference
            source2 = source3.Select<FlatTaxon, string>(Expression.Lambda<Func<FlatTaxon, string>>((Expression) Expression.Call((Expression) Expression.Call(t.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Substring)), new Expression[2]
            {
              (Expression) Expression.Constant((object) 0, typeof (int)),
              (Expression) Expression.Constant((object) 1, typeof (int))
            }), parameterExpression5));
          }
        }
      }
      return (source2 != null ? source1_1.Concat<string>((IEnumerable<string>) source2) : source1_1).Where<string>((Expression<Func<string, bool>>) (t => t != default (string)));
    }
  }
}
