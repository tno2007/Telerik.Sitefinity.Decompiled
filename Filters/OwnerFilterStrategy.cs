// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.OwnerFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>Represents a strategy for filtering items</summary>
  internal class OwnerFilterStrategy : IFilterStrategy
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
      if (!this.GetFilters(itemType, providerName, culture).Any<FilterItem>((Func<FilterItem, bool>) (f => f.Name == filter)))
      {
        filteredItemsIDs = (IEnumerable<Guid>) new List<Guid>();
        return false;
      }
      IQueryable<IOwnership> source1 = Queryable.Cast<IOwnership>(ManagerBase.GetMappedManager(itemType, providerName).GetItems(itemType, (string) null, (string) null, 0, 0).AsQueryable());
      SitefinityIdentity currentUserIdentity = ClaimsManager.GetCurrentIdentity();
      IQueryable<IOwnership> source2 = source1.Where<IOwnership>((Expression<Func<IOwnership, bool>>) (i => i.Owner == currentUserIdentity.UserId));
      filteredItemsIDs = (IEnumerable<Guid>) Queryable.Cast<IDataItem>(source2).Select<IDataItem, Guid>((Expression<Func<IDataItem, Guid>>) (i => i.Id)).ToList<Guid>();
      return true;
    }

    /// <inheritdoc />
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (!typeof (ITaxonomy).IsAssignableFrom(itemType) && typeof (IOwnership).IsAssignableFrom(itemType) && typeof (IDataItem).IsAssignableFrom(itemType))
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
          Name = "Owner",
          Title = Res.Get<Labels>().MyItems.Arrange((object) shortPluralTitle.ToLowerInvariant())
        },
        new FilterItem()
        {
          Name = "User",
          Title = string.Format(Res.Get<Labels>().FilterTitle, (object) Res.Get<ContentResources>().Author),
          Parameters = new FilterParameters((IDictionary<string, object>) new Dictionary<string, object>()
          {
            {
              "Type",
              (object) "call"
            },
            {
              "ContentSingularName",
              (object) Res.Get<ContentResources>().Author
            },
            {
              "ContentPluralName",
              (object) Res.Get<ContentResources>().Authors
            }
          })
        }
      };
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
      Guid[] userIds = parameters.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToArray<Guid>();
      IQueryable<IOwnership> source = Queryable.Cast<IOwnership>(query);
      if (filter == "Owner")
      {
        Guid userId = ClaimsManager.GetCurrentUserId();
        source = source.Where<IOwnership>((Expression<Func<IOwnership, bool>>) (x => x.Owner == userId));
      }
      else if (filter == "User")
        source = source.Where<IOwnership>((Expression<Func<IOwnership, bool>>) (x => userIds.Contains<Guid>(x.Owner)));
      resultQuery = (IQueryable) source;
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
      IQueryable<IOwnership> source1 = Queryable.Cast<IOwnership>(ManagerBase.GetMappedManager(itemType, providerName).GetItems(itemType, (string) null, (string) null, 0, 0).AsQueryable());
      if (parameters.Count > 0)
      {
        List<Guid> guidList = parameters.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>();
        source1 = source1.Where<IOwnership>((Expression<Func<IOwnership, bool>>) (x => guidList.Contains(x.Owner)));
      }
      IQueryable<Guid> source2 = source1.Select<IOwnership, Guid>((Expression<Func<IOwnership, Guid>>) (item => item.Owner)).Distinct<Guid>();
      if (totalCount.HasValue)
        totalCount = new int?(source2.Count<Guid>());
      int num = 0;
      List<Result> source3 = new List<Result>();
      foreach (Guid userId in (IEnumerable<Guid>) source2)
      {
        UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
        if (cachedUserProfile != null)
        {
          string str = string.Format("{0} {1}", (object) cachedUserProfile.FirstName, (object) cachedUserProfile.LastName);
          if (string.IsNullOrEmpty(search) || this.MatchFullName(str, search) || cachedUserProfile.Email.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0)
          {
            ++num;
            source3.Add(new Result(cachedUserProfile.UserId.ToString(), str)
            {
              ImageUrl = cachedUserProfile.AvatarUrl
            });
            if (num == take)
              break;
          }
        }
      }
      return !string.IsNullOrEmpty(search) ? (IEnumerable<Result>) source3.OrderBy<Result, string>((Func<Result, string>) (x => x.Title)) : (IEnumerable<Result>) source3;
    }

    private bool MatchFullName(string fullName, string search)
    {
      if (string.IsNullOrEmpty(search))
        return false;
      return fullName.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) == 0 || fullName.IndexOf(string.Format(" {0}", (object) search), StringComparison.InvariantCultureIgnoreCase) >= 0;
    }
  }
}
