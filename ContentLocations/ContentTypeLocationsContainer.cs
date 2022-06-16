// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentTypeLocationsContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Stores the information for a group of ContentLocations (that will be cached).
  /// The group has a common item type and provider.
  /// </summary>
  internal class ContentTypeLocationsContainer
  {
    private readonly List<ContentLocation> locations = new List<ContentLocation>();
    private IDictionary<string, IList<SingleItemContentLocation>> singleItemLocations;
    private IDictionary<string, object> localCache = (IDictionary<string, object>) new ConcurrentDictionary<string, object>();

    internal ContentTypeLocationsContainer(
      ContentLocationService locationService,
      string itemTypeName,
      string itemProvider)
    {
      this.LocationService = locationService;
      this.ItemTypeName = itemTypeName;
      this.ItemProvider = itemProvider;
      this.ItemType = TypeResolutionService.ResolveType(itemTypeName, false);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentTypeLocationsContainer" /> class.
    /// Gets all ContentLocations for a given itemType and provider.
    /// </summary>
    /// <param name="locationService">The location service.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="contentLocations">The content locations.</param>
    public ContentTypeLocationsContainer(
      ContentLocationService locationService,
      Type itemType,
      string itemProvider,
      IList<ContentLocationDataItem> contentLocations = null)
    {
      if (contentLocations != null && contentLocations.Count > 0)
      {
        foreach (ContentLocationDataItem contentLocation in (IEnumerable<ContentLocationDataItem>) contentLocations)
          this.locations.Add(new ContentLocation(contentLocation, this));
        IEnumerable<ContentLocation> contentLocations1 = this.locations.Where<ContentLocation>((Func<ContentLocation, bool>) (l => l.RedirectPageId != Guid.Empty));
        if (contentLocations1.Count<ContentLocation>() > 0)
        {
          foreach (ContentLocation contentLocation in new List<ContentLocation>(contentLocations1))
          {
            ContentLocation rl = contentLocation;
            this.locations.Where<ContentLocation>((Func<ContentLocation, bool>) (l => l.PageId == rl.RedirectPageId && l.Culture.Equals((object) rl.Culture))).FirstOrDefault<ContentLocation>()?.AddRedirectLocation(rl);
            if (!this.locations.Any<ContentLocation>((Func<ContentLocation, bool>) (l => l.RedirectPageId == rl.PageId)))
              this.locations.Remove(rl);
          }
        }
      }
      this.LocationService = locationService;
      this.ItemType = itemType;
      this.ItemProvider = itemProvider;
      this.ItemTypeName = itemType.FullName;
    }

    /// <summary>
    /// Gets the default location for the item with the specified ID.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="descendantType">Type of the descendant.</param>
    /// <returns></returns>
    public IContentLocation GetItemLocation(
      Guid itemId,
      CultureInfo culture,
      Type descendantType = null,
      Guid[] siteFilter = null)
    {
      IContentLocation itemLocation = (IContentLocation) null;
      foreach (IContentLocation location in this.GetLocations(culture, descendantType))
      {
        if ((siteFilter == null || siteFilter.Length == 0 || ((IEnumerable<Guid>) siteFilter).Contains<Guid>(location.SiteId)) && location.IsMatch(itemId) && location.IsAccessible(true))
        {
          itemLocation = location;
          break;
        }
      }
      if (itemLocation == null)
        itemLocation = this.GetSingleItemLocations(itemId, culture, descendantType).FirstOrDefault<IContentLocation>();
      return itemLocation;
    }

    /// <summary>
    /// Gets the locations (for the current type and provider).
    /// </summary>
    /// <value>The locations.</value>
    public IEnumerable<IContentLocation> GetLocations(
      CultureInfo culture = null,
      Type descendantType = null)
    {
      return this.GetActualLocations((IEnumerable<ContentLocation>) this.locations, culture, descendantType);
    }

    /// <summary>Gets the location for specific item</summary>
    /// <param name="itemId"></param>
    /// <param name="culture"></param>
    /// <param name="descendantType"></param>
    /// <returns>The locations</returns>
    public IEnumerable<IContentLocation> GetSingleItemLocations(
      Guid itemId,
      CultureInfo culture = null,
      Type descendantType = null)
    {
      IList<SingleItemContentLocation> rawLocations;
      return this.SingleItemLocations.TryGetValue(itemId.ToString(), out rawLocations) ? this.GetActualLocations((IEnumerable<ContentLocation>) rawLocations, culture, descendantType) : (IEnumerable<IContentLocation>) new List<ContentLocation>();
    }

    internal IContentLocation GetCachedItemLocation(
      Guid itemId,
      string itemHash,
      CultureInfo culture,
      Type descendantType = null)
    {
      object cachedItemLocation = (object) null;
      string locationCacheKey = this.GetItemLocationCacheKey(itemId, itemHash, culture, descendantType);
      if (!this.localCache.TryGetValue(locationCacheKey, out cachedItemLocation))
      {
        cachedItemLocation = (object) this.GetItemLocation(itemId, culture, descendantType);
        this.localCache[locationCacheKey] = cachedItemLocation ?? new object();
      }
      return cachedItemLocation as IContentLocation;
    }

    private IEnumerable<IContentLocation> GetActualLocations(
      IEnumerable<ContentLocation> rawLocations,
      CultureInfo culture,
      Type descendantType)
    {
      IEnumerable<IContentLocation> source;
      if (rawLocations != null && rawLocations.Any<ContentLocation>())
      {
        if (culture == null)
          culture = SystemManager.CurrentContext.Culture;
        Guid[] sites = SystemManager.CurrentContext.MultisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => s.DefaultCulture.Name == culture.Name)).Select<ISite, Guid>((Func<ISite, Guid>) (s => s.Id)).ToArray<Guid>();
        source = rawLocations.Where<ContentLocation>((Func<ContentLocation, bool>) (l =>
        {
          if (l.Language == culture.Name)
            return true;
          return ((IEnumerable<Guid>) sites).Contains<Guid>(l.SiteId) && string.IsNullOrEmpty(l.Language);
        })).GroupBy(l => new
        {
          PageId = l.PageId,
          ControlId = l.ControlId
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType35<Guid, Guid>, ContentLocation>, IContentLocation>(gr => ContentTypeLocationsContainer.ResolveLocation((IEnumerable<IContentLocation>) gr));
      }
      else
        source = (IEnumerable<IContentLocation>) (rawLocations ?? (IEnumerable<ContentLocation>) new List<ContentLocation>());
      if (descendantType != (Type) null)
        source = source.Where<IContentLocation>((Func<IContentLocation, bool>) (l => l.ItemDescendantType == (Type) null || l.ItemDescendantType == descendantType));
      return (IEnumerable<IContentLocation>) source.OrderBy<IContentLocation, int>((Func<IContentLocation, int>) (l => l.Priority));
    }

    internal static IContentLocation ResolveLocation(
      IEnumerable<IContentLocation> gr)
    {
      return gr.OrderByDescending<IContentLocation, string>((Func<IContentLocation, string>) (l => l.Culture.Name)).First<IContentLocation>();
    }

    public ContentLocationService LocationService { get; private set; }

    public Type ItemType { get; private set; }

    public string ItemProvider { get; private set; }

    public string ItemTypeName { get; private set; }

    public IDictionary<string, IList<SingleItemContentLocation>> SingleItemLocations
    {
      get
      {
        if (this.singleItemLocations == null)
        {
          lock (this)
          {
            if (this.singleItemLocations == null)
            {
              IQueryable<ContentLocationFilterDataItem> source = ContentLocationsManager.GetManager().GetContentFilters().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.ItemType == this.ItemTypeName && f.ContentLocation.ItemProvider == this.ItemProvider && f.ContentLocation.Priority == -1 && f.Type == typeof (ContentLocationSingleItemFilter).FullName));
              Dictionary<string, IList<SingleItemContentLocation>> dictionary = new Dictionary<string, IList<SingleItemContentLocation>>();
              Expression<Func<ContentLocationFilterDataItem, Tuple<string, ContentLocationDataItem>>> selector = (Expression<Func<ContentLocationFilterDataItem, Tuple<string, ContentLocationDataItem>>>) (f => new Tuple<string, ContentLocationDataItem>(f.Value, f.ContentLocation));
              foreach (Tuple<string, ContentLocationDataItem> tuple in (IEnumerable<Tuple<string, ContentLocationDataItem>>) source.Select<ContentLocationFilterDataItem, Tuple<string, ContentLocationDataItem>>(selector))
              {
                ContentLocationSingleItemFilter filter = new ContentLocationSingleItemFilter(tuple.Item1);
                SingleItemContentLocation itemContentLocation = new SingleItemContentLocation(tuple.Item2, this, filter);
                foreach (string itemId in filter.ItemIds)
                {
                  IList<SingleItemContentLocation> itemContentLocationList;
                  if (!dictionary.TryGetValue(itemId, out itemContentLocationList))
                  {
                    itemContentLocationList = (IList<SingleItemContentLocation>) new List<SingleItemContentLocation>();
                    dictionary.Add(itemId, itemContentLocationList);
                  }
                  itemContentLocationList.Add(itemContentLocation);
                }
              }
              this.singleItemLocations = (IDictionary<string, IList<SingleItemContentLocation>>) dictionary;
            }
          }
        }
        return this.singleItemLocations;
      }
    }

    private string GetItemLocationCacheKey(
      Guid id,
      string hash,
      CultureInfo culture,
      Type descendantType)
    {
      return id.ToString() + hash + (culture != null ? (object) culture.Name : (object) string.Empty) + (descendantType != (Type) null ? (object) descendantType.Name : (object) string.Empty);
    }
  }
}
