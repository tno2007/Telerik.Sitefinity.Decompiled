// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.AppliedToProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A calculated property for retrieving the applied to taxon number.
  /// </summary>
  public class AppliedToProperty : CalculatedProperty, ISortableCalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (uint);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      List<ITaxon> list = items.OfType<ITaxon>().ToList<ITaxon>();
      IEnumerable<Guid> itemsIds = list.Select<ITaxon, Guid>((Func<ITaxon, Guid>) (i => i.Id));
      IDictionary<Guid, uint> taxaItemsCount = TaxonomyManager.GetManager().GetTaxaItemsCount(itemsIds, ContentLifecycleStatus.Master);
      foreach (ITaxon key in list)
      {
        if (taxaItemsCount.ContainsKey(key.Id))
          values.Add((object) key, (object) taxaItemsCount[key.Id]);
        else
          values.Add((object) key, (object) 0U);
      }
      return (IDictionary<object, object>) values;
    }

    /// <inheritdoc />
    IQueryable ISortableCalculatedProperty.GetOrderedQuery<TItem>(
      IQueryable query,
      IManager manager,
      bool isAscending)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AppliedToProperty.\u003C\u003Ec__DisplayClass3_0<TItem> cDisplayClass30 = new AppliedToProperty.\u003C\u003Ec__DisplayClass3_0<TItem>();
      if (!(manager is TaxonomyManager taxonomyManager))
        return query;
      IQueryable<TaxonomyStatistic> queryable = taxonomyManager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => (int) st.StatisticType == 0));
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.taxaStatistics = queryable;
      IQueryable orderedQuery;
      if (isAscending)
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        orderedQuery = (IQueryable) Queryable.Cast<TItem>(query).OrderBy<TItem, long>((Expression<Func<TItem, long>>) (t => cDisplayClass30.taxaStatistics.Where<TaxonomyStatistic>(Expression.Lambda<Func<TaxonomyStatistic, bool>>((Expression) Expression.Equal(st.TaxonId, (Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression1, typeof (IDataItem)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IDataItem.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality))), parameterExpression2)).Sum<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, long>>) (st => (long) st.MarkedItemsCount)))).ThenBy<TItem, Guid>((Expression<Func<TItem, Guid>>) (t => t.Id));
      }
      else
      {
        ParameterExpression parameterExpression3;
        ParameterExpression parameterExpression4;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        orderedQuery = (IQueryable) Queryable.Cast<TItem>(query).OrderByDescending<TItem, long>((Expression<Func<TItem, long>>) (t => cDisplayClass30.taxaStatistics.Where<TaxonomyStatistic>(Expression.Lambda<Func<TaxonomyStatistic, bool>>((Expression) Expression.Equal(st.TaxonId, (Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression3, typeof (IDataItem)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IDataItem.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality))), parameterExpression4)).Sum<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, long>>) (st => (long) st.MarkedItemsCount)))).ThenByDescending<TItem, Guid>((Expression<Func<TItem, Guid>>) (t => t.Id));
      }
      return orderedQuery;
    }
  }
}
