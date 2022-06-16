// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.WorkflowFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>Represents a strategy for filtering items</summary>
  internal class WorkflowFilterStrategy : IFilterStrategy
  {
    private const string WithNoDescriptionsFilterName = "WithNoDescriptions";

    /// <inheritdoc />
    public bool TryToFilterBy(
      string filterName,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      if (typeof (PageNode).IsAssignableFrom(itemType))
      {
        IQueryable<PageNode> source = this.FilterPages(filterName);
        filteredItemsIDs = (IEnumerable<Guid>) source.Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (i => i.Id)).ToList<Guid>();
        return true;
      }
      int? totalCount = new int?();
      IQueryable<ILifecycleDataItemGeneric> queryable = Queryable.Cast<ILifecycleDataItemGeneric>(ManagerBase.GetMappedManager(itemType, providerName).GetItems(itemType, (string) null, (string) null, 0, 0).AsQueryable()).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (item => (int) item.Status == 0));
      string filterName1;
      IQueryable<ILifecycleDataItemGeneric> source1;
      if (this.TryGetNamedFilter(true, this.MapFilter(filterName), out filterName1))
      {
        source1 = !(filterName1 == "LifecyclePublishedDrafts") ? this.FilterLifecycleItems(queryable, filterName1) : this.FilterLifecyclePublishedItems(queryable, culture);
      }
      else
      {
        ContentHelper.AdaptMultilingualFilterExpressionRaw(filterName1, culture, false);
        source1 = DataProviderBase.SetExpressions<ILifecycleDataItemGeneric>(queryable, filterName1, string.Empty, new int?(), new int?(), ref totalCount);
      }
      filteredItemsIDs = (IEnumerable<Guid>) source1.Select<ILifecycleDataItemGeneric, Guid>((Expression<Func<ILifecycleDataItemGeneric, Guid>>) (i => i.Id)).ToList<Guid>();
      return true;
    }

    /// <inheritdoc />
    public bool TryToFilterByQuery(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      IQueryable query,
      out IQueryable resultQuery)
    {
      if (typeof (PageNode).IsAssignableFrom(itemType))
      {
        resultQuery = (IQueryable) this.FilterPages(filter, (IQueryable<PageNode>) query);
        return true;
      }
      string str = filter;
      if (!(str == "Published"))
      {
        if (str == "Rejected")
          resultQuery = (IQueryable) Queryable.Cast<IApprovalWorkflowItem>(query).Where<IApprovalWorkflowItem>((Expression<Func<IApprovalWorkflowItem, bool>>) (x => x.ApprovalWorkflowState == (Lstring) "Rejected" || x.ApprovalWorkflowState == (Lstring) "RejectedForReview" || x.ApprovalWorkflowState == (Lstring) "RejectedForPublishing"));
        else
          resultQuery = (IQueryable) Queryable.Cast<IApprovalWorkflowItem>(query).Where<IApprovalWorkflowItem>((Expression<Func<IApprovalWorkflowItem, bool>>) (x => x.ApprovalWorkflowState == (Lstring) filter));
      }
      else
        resultQuery = (IQueryable) this.FilterLifecyclePublishedItems(Queryable.Cast<ILifecycleDataItemGeneric>(query), culture);
      return true;
    }

    /// <inheritdoc />
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (this.IsCurrentTypeSupported(itemType))
        filters = this.GetFiltersInternal(itemType, providerName, culture);
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

    private IQueryable<ILifecycleDataItemGeneric> FilterLifecycleItems(
      IQueryable<ILifecycleDataItemGeneric> items,
      string filter)
    {
      items = Queryable.Cast<ILifecycleDataItemGeneric>(Queryable.Cast<IApprovalWorkflowItem>(items).Where<IApprovalWorkflowItem>((Expression<Func<IApprovalWorkflowItem, bool>>) (item => item.ApprovalWorkflowState == (Lstring) filter)));
      return items;
    }

    private IQueryable<PageNode> FilterPages(
      string filterName,
      IQueryable<PageNode> query = null)
    {
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      PagesFacade facade = App.WorkWith().Pages();
      if (query != null)
        facade.PageNodes = query;
      facade.FetchAllLanguages().LocatedIn(frontendRootNodeId);
      return new PagesService().FilterPages(facade, filterName, (string) null, frontendRootNodeId).Get();
    }

    private IQueryable<ILifecycleDataItemGeneric> FilterLifecyclePublishedItems(
      IQueryable<ILifecycleDataItemGeneric> items,
      CultureInfo culture)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      WorkflowFilterStrategy.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new WorkflowFilterStrategy.\u003C\u003Ec__DisplayClass6_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.cultureName = culture.Name;
      // ISSUE: reference to a compiler-generated field
      if (DataExtensions.AppSettings.ContextSettings.Multilingual && cDisplayClass60.cultureName == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw())
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        items = items.Where<ILifecycleDataItemGeneric>(Expression.Lambda<Func<ILifecycleDataItemGeneric, bool>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
        {
          item.LanguageData,
          (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.AndAlso((Expression) Expression.OrElse(ld.Language == cDisplayClass60.cultureName, (Expression) Expression.AndAlso(ld.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ILifecycleDataItem.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (LanguageData.get_ContentState))), typeof (int)), (Expression) Expression.Constant((object) 1, typeof (int)))), parameterExpression2)
        }), parameterExpression1));
      }
      else
      {
        bool shouldCheckLegacyMonolingualLanguageData = !DataExtensions.AppSettings.ContextSettings.Multilingual;
        // ISSUE: reference to a compiler-generated field
        items = items.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (item => item.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => (ld.Language == cDisplayClass60.cultureName || shouldCheckLegacyMonolingualLanguageData && ld.Language == default (string)) && (int) ld.ContentState == 1))));
      }
      return items;
    }

    private bool IsCurrentTypeSupported(Type itemType) => typeof (ILifecycleDataItemGeneric).IsAssignableFrom(itemType) && typeof (IApprovalWorkflowItem).IsAssignableFrom(itemType) || typeof (PageNode).IsAssignableFrom(itemType);

    private List<FilterItem> GetFiltersInternal(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      filters.Add(new FilterItem()
      {
        Name = "Draft",
        Title = Res.Get<WorkflowResources>().Draft
      });
      filters.Add(new FilterItem()
      {
        Name = "Published",
        Title = Res.Get<WorkflowResources>().Published
      });
      filters.Add(new FilterItem()
      {
        Name = "Unpublished",
        Title = Res.Get<WorkflowResources>().Unpublished
      });
      IWorkflowExecutionDefinition executionDefinition = WorkflowManager.GetWorkflowExecutionDefinition(itemType, providerName, Guid.Empty, culture);
      if (executionDefinition.WorkflowType == WorkflowType.StandardOneStep || executionDefinition.WorkflowType == WorkflowType.StandardTwoStep || executionDefinition.WorkflowType == WorkflowType.StandardThreeStep)
      {
        filters.Add(new FilterItem()
        {
          Name = "AwaitingApproval",
          Title = Res.Get<ApprovalWorkflowResources>().AwaitingApproval
        });
        if (executionDefinition.WorkflowType == WorkflowType.StandardTwoStep || executionDefinition.WorkflowType == WorkflowType.StandardThreeStep)
          filters.Add(new FilterItem()
          {
            Name = "AwaitingPublishing",
            Title = Res.Get<ApprovalWorkflowResources>().AwaitingPublishing
          });
        if (executionDefinition.WorkflowType == WorkflowType.StandardThreeStep)
          filters.Add(new FilterItem()
          {
            Name = "AwaitingReview",
            Title = Res.Get<ApprovalWorkflowResources>().AwaitingReview
          });
        filters.Add(new FilterItem()
        {
          Name = "Rejected",
          Title = Res.Get<ApprovalWorkflowResources>().Rejected
        });
        if (typeof (PageNode).IsAssignableFrom(itemType))
          filters.Add(new FilterItem()
          {
            Name = "AwaitingMyAction",
            Title = Res.Get<PageResources>().AwaitingMyAction,
            IsStatus = true
          });
      }
      this.TryAddWithNoDescriptionsFilter(filters, itemType);
      filters.ForEach((Action<FilterItem>) (filter => filter.IsStatus = true));
      return filters;
    }

    private void TryAddWithNoDescriptionsFilter(List<FilterItem> filters, Type itemType)
    {
      if (!typeof (PageNode).IsAssignableFrom(itemType))
        return;
      FilterItem filterItem = new FilterItem()
      {
        Name = "WithNoDescriptions",
        Title = Res.Get<PageResources>().WithNoDescriptionsAdminApp
      };
      filters.Add(filterItem);
    }

    /// <summary>Maps the filter names to named filter command</summary>
    /// <param name="filter">The filter</param>
    /// <returns>The named filter</returns>
    private string MapFilter(string filter)
    {
      switch (filter)
      {
        case "AwaitingApproval":
          return "[AwaitingApproval]";
        case "AwaitingMyAction":
          return "[AwaitingMyAction]";
        case "AwaitingPublishing":
          return "[AwaitingPublishing]";
        case "AwaitingReview":
          return "[AwaitingReview]";
        case "Draft":
          return "[Draft]";
        case "Published":
          return "[PublishedDrafts]";
        case "Scheduled":
          return "[Scheduled]";
        case "Unpublished":
          return "[Unpublished]";
        default:
          return filter;
      }
    }

    private bool TryGetNamedFilter(bool supportsLifecycle, string filter, out string filterName)
    {
      filterName = (string) null;
      if (!NamedFiltersHandler.TryParseFilterName(filter, out filterName))
        return false;
      if (supportsLifecycle && filterName == "PublishedDrafts")
        filterName = "LifecyclePublishedDrafts";
      return true;
    }
  }
}
