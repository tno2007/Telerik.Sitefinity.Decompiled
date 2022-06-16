// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.TemplatesInSitesFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>Represents a strategy for filtering items</summary>
  internal class TemplatesInSitesFilterStrategy : IFilterStrategy
  {
    /// <inheritdoc />
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

    /// <inheritdoc />
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (itemType.FullName == typeof (PageTemplate).FullName)
        filters = this.GetFiltersInternal(itemType);
      return (IEnumerable<FilterItem>) filters;
    }

    /// <summary>
    /// Gets all filters registered for specific item type from this strategy
    /// </summary>
    /// <param name="itemType">The item type</param>
    /// <returns>Available filters</returns>
    protected virtual List<FilterItem> GetFiltersInternal(Type itemType)
    {
      string shortPluralTitle = SystemManager.TypeRegistry.GetType(itemType.FullName).ShortPluralTitle;
      return new List<FilterItem>()
      {
        new FilterItem()
        {
          Name = "AllTemplates",
          Title = Res.Get<PageResources>().AllTemplatesPascalCase,
          Ordinal = -1
        },
        new FilterItem()
        {
          Name = "ThisSite",
          Title = Res.Get<MultisiteResources>().ThisSiteCapital
        },
        new FilterItem()
        {
          Name = "NoSite",
          Title = Res.Get<MultisiteResources>().NotSharedWithAnySite
        }
      };
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
      IQueryable queryable = query;
      PageTemplatesFacade pageTemplatesFacade = App.WorkWith().PageTemplates();
      if (filter == "ThisSite")
      {
        Guid id = SystemManager.CurrentContext.CurrentSite.Id;
        queryable = (IQueryable) pageTemplatesFacade.GetInSite(id).GetNotInCategory(SiteInitializer.BackendTemplatesCategoryId);
      }
      else if (filter == "NoSite")
        queryable = (IQueryable) pageTemplatesFacade.GetNotShared().GetNotInCategory(SiteInitializer.BackendTemplatesCategoryId);
      else if (filter == "AllTemplates")
        queryable = (IQueryable) PageTemplateHelper.FilterFrameworkSpecificTemplates(Queryable.Cast<PageTemplate>(pageTemplatesFacade.GetNotInCategory(SiteInitializer.BackendTemplatesCategoryId)));
      resultQuery = queryable;
      return true;
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
  }
}
