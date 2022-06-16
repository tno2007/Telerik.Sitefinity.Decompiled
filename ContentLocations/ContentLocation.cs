// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides content location information in Sitefinity and the ability to check if a particular item matches it.
  /// </summary>
  internal class ContentLocation : IContentLocation, IContentLocationBase
  {
    private List<ContentLocation> redirectLocations;
    protected int priority;
    protected Guid siteId;
    protected Guid pageId;
    protected Guid controlId;
    protected Guid redirectPageId;
    private readonly int operatorLength = " and ".Length;
    private const string FilterOperator = " and ";

    public ContentLocation()
    {
    }

    public ContentLocation(
      ContentLocationDataItem contentLocation,
      ContentTypeLocationsContainer parent)
    {
      this.Id = contentLocation.Id;
      this.siteId = contentLocation.SiteId;
      this.pageId = contentLocation.PageId;
      this.controlId = contentLocation.ControlId;
      this.priority = contentLocation.Priority;
      this.redirectPageId = !(contentLocation.RedirectPageId != Guid.Empty) || !(contentLocation.RedirectPageId != contentLocation.PageId) ? Guid.Empty : contentLocation.RedirectPageId;
      this.Parent = parent;
      this.Culture = AppSettings.CurrentSettings.GetCultureByName(contentLocation.Language);
      this.ItemDescendantTypeName = contentLocation.ItemDescendantType;
      if (string.IsNullOrEmpty(this.ItemDescendantTypeName))
        return;
      this.ItemDescendantType = TypeResolutionService.ResolveType(this.ItemDescendantTypeName, false);
    }

    public virtual bool IsMatch(Guid itemId) => this.IsMatch(itemId, false);

    public string Language => this.Culture.Name;

    public CultureInfo Culture { get; set; }

    public Guid Id { get; set; }

    public Guid PageId
    {
      get => this.pageId;
      set => this.pageId = value;
    }

    public Guid SiteId
    {
      get => this.siteId;
      set => this.siteId = value;
    }

    public Guid ControlId
    {
      get => this.controlId;
      set => this.controlId = value;
    }

    public virtual bool IsSingleItem => this.GetFiltersFromCache().Where<IContentLocationFilter>((Func<IContentLocationFilter, bool>) (f => f is IContentLocationSingleItemFilter)).Any<IContentLocationFilter>();

    public Type ItemType => this.Parent.ItemType;

    public string ItemTypeName => this.Parent.ItemTypeName;

    public Type ItemDescendantType { get; set; }

    public string ItemDescendantTypeName { get; set; }

    public string ItemProvider => this.Parent.ItemProvider;

    public Guid RedirectPageId
    {
      get => this.redirectPageId;
      set => this.redirectPageId = value;
    }

    public virtual int Priority
    {
      get => this.priority;
      set => this.priority = value;
    }

    internal ContentTypeLocationsContainer Parent { get; set; }

    private bool IsMatch(Guid itemId, bool isRedirectMatch)
    {
      bool flag1 = true;
      bool flag2;
      if (!isRedirectMatch && this.RedirectPageId != Guid.Empty)
      {
        flag2 = false;
      }
      else
      {
        IList<IContentLocationFilter> filtersFromCache = this.GetFiltersFromCache();
        foreach (IContentLocationMatchingFilter locationMatchingFilter in filtersFromCache.Where<IContentLocationFilter>((Func<IContentLocationFilter, bool>) (f => f is IContentLocationMatchingFilter)))
        {
          flag1 &= locationMatchingFilter.IsMatch((IContentLocationService) this.Parent.LocationService, (IContentLocation) this, itemId);
          if (!flag1 || !locationMatchingFilter.ShouldApplyAdditionalFilters)
            return flag1;
        }
        flag2 = flag1 & this.MatchGroupFilters(filtersFromCache, itemId, (Func<IContentLocationFilter, bool>) (f => f is IContentLocationGroupFilter), (Func<IContentLocationFilter, string>) (f => ((IContentLocationGroupFilter) f).GetExpression())) & this.MatchGroupFilters(filtersFromCache, itemId, (Func<IContentLocationFilter, bool>) (f =>
        {
          switch (f)
          {
            case null:
            case IContentLocationGroupFilter _:
              return false;
            default:
              return !(f is IContentLocationMatchingFilter);
          }
        }), (Func<IContentLocationFilter, string>) (f => f.Value));
      }
      if (!isRedirectMatch)
        flag2 |= this.IsLinkLocationsMatch(itemId);
      return flag2;
    }

    /// <summary>
    /// If there are no filters returns true otherwise applies the filter and returns its result.
    /// </summary>
    /// <param name="locationService"></param>
    /// <param name="t"></param>
    /// <param name="itemProvider"></param>
    /// <param name="itemId"></param>
    /// <param name="fitlersPredicate"></param>
    /// <param name="getExpression"></param>
    /// <returns></returns>
    [SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable")]
    private bool MatchGroupFilters(
      IList<IContentLocationFilter> filters,
      Guid itemId,
      Func<IContentLocationFilter, bool> fitlersPredicate,
      Func<IContentLocationFilter, string> getExpression)
    {
      List<IContentLocationFilter> list = filters.Where<IContentLocationFilter>(fitlersPredicate).ToList<IContentLocationFilter>();
      if (!list.Any<IContentLocationFilter>())
        return true;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (IContentLocationFilter contentLocationFilter in list)
        stringBuilder.AppendFormat("{0}{1}", (object) " and ", (object) getExpression(contentLocationFilter));
      string empty = string.Empty;
      if (stringBuilder.Length > this.operatorLength)
        empty = stringBuilder.ToString(this.operatorLength, stringBuilder.Length - this.operatorLength);
      return this.Parent.LocationService.MatchFilter(this.Parent.ItemType, this.Parent.ItemProvider, itemId, empty);
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    private IList<IContentLocationFilter> GetFiltersFromCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.ContentLocations);
      string key = this.Id.ToString();
      if (!(cacheManager[key] is IList<IContentLocationFilter> filtersFromCache))
      {
        filtersFromCache = (IList<IContentLocationFilter>) new List<IContentLocationFilter>();
        foreach (ContentLocationFilterDataItem locationFilterDataItem in ContentLocationsManager.GetManager().GetContentFilters(this.Id).ToList<ContentLocationFilterDataItem>())
        {
          if (!string.IsNullOrWhiteSpace(locationFilterDataItem.Type))
          {
            Type type = TypeResolutionService.ResolveType(locationFilterDataItem.Type, false);
            if (type != (Type) null && Activator.CreateInstance(type) is IContentLocationFilter instance)
            {
              instance.Name = locationFilterDataItem.Name;
              instance.Value = locationFilterDataItem.Value;
              filtersFromCache.Add(instance);
            }
          }
        }
        cacheManager.Add(key, (object) filtersFromCache, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (ContentLocationDataItem), key), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
      }
      return filtersFromCache;
    }

    private bool IsLinkLocationsMatch(Guid itemId)
    {
      bool flag = false;
      if (this.redirectLocations != null)
      {
        foreach (ContentLocation redirectLocation in this.redirectLocations)
          flag |= redirectLocation.IsMatch(itemId, true);
      }
      return flag;
    }

    internal void AddRedirectLocation(ContentLocation location)
    {
      if (this.redirectLocations == null)
        this.redirectLocations = new List<ContentLocation>();
      this.redirectLocations.Add(location);
    }
  }
}
