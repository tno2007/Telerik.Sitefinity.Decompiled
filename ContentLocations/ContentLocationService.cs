// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.ContentLocations
{
  internal class ContentLocationService : IContentLocationService
  {
    internal const int SingleItemLocationPriority = -1;
    private List<Type> baseItemTypes;
    private static readonly object settingsCacheSync = new object();
    private const string QueryFormat = "Id = {0} and ({1})";

    /// <inheritdoc />
    public IContentItemLocation GetItemDefaultLocation(
      Type itemType,
      string itemProvider,
      Guid itemId,
      CultureInfo culture = null)
    {
      return this.GetItemDefaultLocation(itemType, itemProvider, itemId, (Guid[]) null, culture);
    }

    /// <inheritdoc />
    public IContentItemLocation GetItemDefaultLocation(
      IDataItem item,
      CultureInfo culture = null)
    {
      return this.GetItemDefaultLocation(item, (Guid[]) null, culture);
    }

    /// <inheritdoc />
    public IEnumerable<IContentItemLocation> GetItemLocations(
      Type itemType,
      string itemProvider,
      Guid itemId,
      CultureInfo culture = null)
    {
      List<ContentItemLocation> contentItemLocationList = (List<ContentItemLocation>) null;
      IManager manager;
      if (this.TryResolveItemManager(itemType, itemProvider, out manager))
      {
        try
        {
          if (manager.GetItem(itemType, itemId) is IDataItem dataItem)
            contentItemLocationList = this.GetItemLocations(itemType, itemProvider, culture, dataItem);
        }
        catch (ItemNotFoundException ex)
        {
        }
      }
      return (IEnumerable<IContentItemLocation>) contentItemLocationList ?? (IEnumerable<IContentItemLocation>) new List<ContentItemLocation>();
    }

    /// <inheritdoc />
    public IEnumerable<IContentItemLocation> GetItemLocations(
      IDataItem item,
      CultureInfo culture = null)
    {
      List<ContentItemLocation> contentItemLocationList = (List<ContentItemLocation>) null;
      IManager manager;
      if (item != null && this.TryResolveItemManager(item, out manager))
        contentItemLocationList = this.GetItemLocations(item.GetType(), manager.Provider.Name, culture, item);
      return (IEnumerable<IContentItemLocation>) contentItemLocationList ?? (IEnumerable<IContentItemLocation>) new List<ContentItemLocation>();
    }

    /// <summary>
    /// Gets the default (canonical) location where the specified item could be opened. Can be filtered by site ID.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="siteFilter">The filter of sites ID.</param>
    /// <param name="culture">The culture.</param>
    /// <returns><typeparamref name="IContentItemLocation" /> item, or null if no location is found.</returns>
    public IContentItemLocation GetItemDefaultLocation(
      Type itemType,
      string itemProvider,
      Guid itemId,
      Guid[] siteFilter,
      CultureInfo culture = null)
    {
      IManager manager;
      if (this.TryResolveItemManager(itemType, itemProvider, out manager))
      {
        IDataItem dataItem;
        try
        {
          dataItem = manager.GetItem(itemType, itemId) as IDataItem;
        }
        catch (ItemNotFoundException ex)
        {
          return (IContentItemLocation) null;
        }
        if (dataItem != null)
          return this.GetItemDefaultLocation(dataItem, manager, culture, siteFilter);
      }
      return (IContentItemLocation) null;
    }

    /// <summary>
    /// Gets the default (canonical) location where the specified item could be opened.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="siteFilter">The filter of sites ID.</param>
    /// <param name="culture">The culture.</param>
    /// <returns><typeparamref name="IContentItemLocation" /> item, or null if no location is found.</returns>
    public IContentItemLocation GetItemDefaultLocation(
      IDataItem item,
      Guid[] siteFilter,
      CultureInfo culture = null)
    {
      IManager manager;
      return this.TryResolveItemManager(item, out manager) ? this.GetItemDefaultLocation(item, manager, culture, siteFilter) : (IContentItemLocation) null;
    }

    /// <summary>Finds the default location for the given item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public IContentLocation FindDefaultLocation(
      Type itemType,
      string itemProvider,
      Guid itemId,
      CultureInfo culture = null,
      Guid[] siteFilter = null)
    {
      Type parentType;
      if (!this.HasParentContentItemType(itemType, out parentType))
      {
        parentType = itemType;
        itemType = (Type) null;
      }
      return this.GetContentTypeLocations(parentType, itemProvider).GetItemLocation(itemId, culture, itemType, siteFilter);
    }

    /// <summary>Updates the location.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="controlId">The site id.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="info">The info.</param>
    public void UpdateLocation(
      Type itemType,
      string itemProvider,
      Guid pageId,
      Guid siteId,
      Guid controlId,
      CultureInfo culture,
      IContentLocationInfo info)
    {
      try
      {
        string language = ContentLocationService.GetPersistedLanguageString(culture);
        string itemTypeName;
        string descendantTypeName;
        this.GetContentTypeNames(itemType, out itemTypeName, out descendantTypeName);
        ContentLocationsManager manager = ContentLocationsManager.GetManager();
        List<ContentLocationDataItem> contentLocations;
        ContentLocationDataItem location;
        if (!this.TryGetContentLocationItems(manager, itemTypeName, itemProvider, pageId, controlId, language, out contentLocations, descendantTypeName))
        {
          location = manager.CreateLocation();
          location.ItemType = itemTypeName;
          location.ItemProvider = itemProvider;
          location.Language = language;
          location.PageId = pageId;
          location.SiteId = siteId;
          location.ControlId = controlId;
          location.ItemDescendantType = descendantTypeName;
        }
        else
        {
          location = contentLocations.Where<ContentLocationDataItem>((Func<ContentLocationDataItem, bool>) (l => l.Language == language)).FirstOrDefault<ContentLocationDataItem>();
          if (location.ControlId == Guid.Empty)
            location.ControlId = controlId;
          foreach (ContentLocationFilterDataItem contentFilter in (IEnumerable<ContentLocationFilterDataItem>) manager.GetContentFilters(location.Id))
            manager.Delete(contentFilter);
        }
        this.ApplyAdditionalInfo(info, manager, location);
        ContentLocationService.UpdatePriorities(location, info, manager, itemProvider, itemTypeName);
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions))
          return;
        throw;
      }
    }

    private static string GetPersistedLanguageString(CultureInfo culture)
    {
      AppSettings currentSettings = AppSettings.CurrentSettings;
      return culture == null ? currentSettings.GetCultureName(currentSettings.DefaultFrontendLanguage) : currentSettings.GetCultureName(culture);
    }

    private void ApplyAdditionalInfo(
      IContentLocationInfo info,
      ContentLocationsManager manager,
      ContentLocationDataItem location)
    {
      if (info == null)
        return;
      location.RedirectPageId = info.RedirectPageId;
      if (info.Filters == null || !info.Filters.Any<IContentLocationFilter>())
        return;
      foreach (IContentLocationFilter filter in info.Filters)
      {
        ContentLocationFilterDataItem contentFilter = manager.CreateContentFilter();
        contentFilter.ContentLocation = location;
        contentFilter.Name = filter.Name;
        contentFilter.Type = filter.GetType().FullName;
        contentFilter.Value = filter.Value;
      }
    }

    /// <summary>Deletes the location.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="conrolId">The control id.</param>
    /// <param name="culture">The culture.</param>
    public void DeleteLocation(
      Type itemType,
      string itemProvider,
      Guid pageId,
      Guid siteId,
      Guid controlId,
      CultureInfo culture)
    {
      string itemTypeName;
      string descendantTypeName;
      this.GetContentTypeNames(itemType, out itemTypeName, out descendantTypeName);
      this.DeleteLocation(itemTypeName, descendantTypeName, itemProvider, pageId, siteId, controlId, culture);
    }

    /// <summary>Deletes the location.</summary>
    /// <param name="location">The location.</param>
    internal void DeleteLocation(IContentLocation location) => this.DeleteLocation(location.ItemTypeName, location.ItemDescendantTypeName, location.ItemProvider, location.PageId, location.SiteId, location.ControlId, location.Culture);

    private void DeleteLocation(
      string itemTypeName,
      string descendantTypeName,
      string itemProvider,
      Guid pageId,
      Guid siteId,
      Guid controlId,
      CultureInfo culture)
    {
      string persistedLanguageString = ContentLocationService.GetPersistedLanguageString(culture);
      ContentLocationsManager manager = ContentLocationsManager.GetManager();
      List<ContentLocationDataItem> contentLocations;
      if (!this.TryGetContentLocationItems(manager, itemTypeName, itemProvider, pageId, controlId, persistedLanguageString, out contentLocations, descendantTypeName))
        return;
      foreach (ContentLocationDataItem location in contentLocations)
        manager.Delete(location);
      manager.SaveChanges();
    }

    /// <summary>Deletes all locations with the specified page id.</summary>
    /// <param name="pageId">The page id.</param>
    internal void DeletePageLocations(Guid pageId)
    {
      ContentLocationsManager manager = ContentLocationsManager.GetManager();
      IQueryable<ContentLocationDataItem> locations = manager.GetLocations();
      Expression<Func<ContentLocationDataItem, bool>> predicate = (Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == pageId);
      foreach (ContentLocationDataItem location in locations.Where<ContentLocationDataItem>(predicate).ToList<ContentLocationDataItem>())
        manager.Delete(location);
      manager.SaveChanges();
    }

    /// <summary>Get all locations with the specified page id.</summary>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    internal IEnumerable<IContentLocation> GetPageLocations(Guid pageId)
    {
      IEnumerable<IContentLocation> source = ContentLocationsManager.GetManager().GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == pageId)).ToList<ContentLocationDataItem>().Select<ContentLocationDataItem, IContentLocation>((Func<ContentLocationDataItem, IContentLocation>) (l => (IContentLocation) new ContentLocation(l, new ContentTypeLocationsContainer(this, l.ItemType, l.ItemProvider))));
      if (source.Any<IContentLocation>())
        source = source.GroupBy(l => new
        {
          ItemType = l.ItemType,
          ItemProvider = l.ItemProvider,
          Priority = l.Priority,
          ControlId = l.ControlId
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType36<Type, string, int, Guid>, IContentLocation>, IContentLocation>(gr => ContentTypeLocationsContainer.ResolveLocation((IEnumerable<IContentLocation>) gr.ToList<IContentLocation>()));
      return source;
    }

    /// <summary>
    /// Determines whether [is type supported] [the specified item type].
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public bool IsTypeSupported(Type itemType) => typeof (IDataItem).IsAssignableFrom(itemType);

    /// <summary>
    /// Gets the content locations for the given item type and item provider.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public IEnumerable<IContentLocation> GetContentLocations(
      Type itemType,
      string itemProvider,
      CultureInfo culture = null)
    {
      return this.GetContentLocations(itemType, itemProvider, out ContentTypeLocationsContainer _, culture);
    }

    /// <summary>
    /// Changes the the specified item priority in the context of the item's provider and type.
    /// </summary>
    /// <param name="id">The id of the content location that will have its priority updated.</param>
    /// <param name="direction">The desired change of the priority.</param>
    /// <returns>True if the content location's priority has changed.</returns>
    public bool ChangeContentLocationPriority(Guid id, MovingDirection direction)
    {
      bool flag = false;
      ContentLocationsManager manager = ContentLocationsManager.GetManager();
      ContentLocationDataItem location = manager.GetLocation(id);
      Type itemType = TypeResolutionService.ResolveType(location.ItemType);
      Type parentType;
      if (!this.HasParentContentItemType(itemType, out parentType))
        parentType = itemType;
      IContentLocation[] array = this.GetContentTypeLocations(parentType, location.ItemProvider).GetLocations().Distinct<IContentLocation>((IEqualityComparer<IContentLocation>) new ContentLocationEqualityComparer()).ToArray<IContentLocation>();
      int num = ((IEnumerable<IContentLocation>) array).Count<IContentLocation>();
      if (num > 1)
      {
        switch (direction)
        {
          case MovingDirection.Bottom:
            for (int index1 = 0; index1 < num - 1; ++index1)
            {
              if (array[index1].Id == id)
              {
                for (int index2 = index1 + 1; index2 < num; ++index2)
                  this.SwapLocationPriorities(manager, array[index1], array[index2]);
                manager.SaveChanges();
                flag = true;
                break;
              }
            }
            break;
          case MovingDirection.Down:
            for (int index = 0; index < num - 1; ++index)
            {
              if (array[index].Id == id)
              {
                this.SwapLocationPriorities(manager, array[index], array[index + 1]);
                manager.SaveChanges();
                flag = true;
                break;
              }
            }
            break;
          case MovingDirection.Up:
            for (int index = 1; index < num; ++index)
            {
              if (array[index].Id == id)
              {
                this.SwapLocationPriorities(manager, array[index], array[index - 1]);
                manager.SaveChanges();
                flag = true;
                break;
              }
            }
            break;
          case MovingDirection.Top:
            for (int index3 = num - 1; index3 > 0; --index3)
            {
              if (array[index3].Id == id)
              {
                for (int index4 = index3 - 1; index4 >= 0; --index4)
                  this.SwapLocationPriorities(manager, array[index3], array[index4]);
                manager.SaveChanges();
                flag = true;
                break;
              }
            }
            break;
        }
      }
      return flag;
    }

    internal IEnumerable<IContentLocation> GetContentLocations(
      Type itemType,
      string itemProvider,
      out ContentTypeLocationsContainer container,
      CultureInfo culture = null)
    {
      Type parentType;
      if (!this.HasParentContentItemType(itemType, out parentType))
      {
        parentType = itemType;
        itemType = (Type) null;
      }
      container = this.GetContentTypeLocations(parentType, itemProvider);
      return container.GetLocations(culture, itemType);
    }

    internal bool MatchFilter(Type itemType, string itemProvider, Guid itemId, string filter)
    {
      IManager manager;
      if (!this.TryResolveItemManager(itemType, itemProvider, out manager))
        return false;
      string filterExpression = "Id = {0} and ({1})".Arrange((object) itemId, (object) filter);
      return ((IQueryable<object>) manager.GetItems(itemType, filterExpression, string.Empty, 0, 0)).Any<object>();
    }

    public string[] GetContentFilter(Guid contentLocationId) => ContentLocationsManager.GetManager().GetContentFilters(contentLocationId).Select<ContentLocationFilterDataItem, string>((Expression<Func<ContentLocationFilterDataItem, string>>) (x => x.Value)).ToArray<string>();

    private bool TryResolveItemManager(IDataItem item, out IManager manager)
    {
      string providerName = item.Provider is IDataProviderBase provider ? provider.Name : string.Empty;
      return ManagerBase.TryGetMappedManager(item.GetType(), providerName, out manager);
    }

    private bool TryResolveItemManager(Type itemType, string itemProvider, out IManager manager) => ManagerBase.TryGetMappedManager(itemType, itemProvider, out manager);

    private List<ContentItemLocation> GetItemLocations(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      IDataItem item)
    {
      List<ContentItemLocation> source = new List<ContentItemLocation>();
      ContentTypeLocationsContainer container;
      foreach (IContentLocation contentLocation in this.GetContentLocations(itemType, itemProvider, out container, culture))
      {
        if (contentLocation.IsMatch(item.Id))
          source.Add(this.GetItemLocation(item, contentLocation));
      }
      ContentItemLocation contentItemLocation = source.FirstOrDefault<ContentItemLocation>((Func<ContentItemLocation, bool>) (l => l.IsAccessible(true)));
      if (contentItemLocation != null)
        contentItemLocation.IsDefault = true;
      source.AddRange(container.GetSingleItemLocations(item.Id, culture).Select<IContentLocation, ContentItemLocation>((Func<IContentLocation, ContentItemLocation>) (l => this.GetItemLocation(item, l))));
      return source;
    }

    private IContentItemLocation GetItemDefaultLocation(
      IDataItem item,
      IManager manager,
      CultureInfo culture,
      Guid[] siteFilter = null)
    {
      ContentItemLocation itemDefaultLocation = (ContentItemLocation) null;
      IContentLocation defaultLocation = this.FindDefaultLocation(item.GetType(), manager.Provider.Name, item.Id, culture, siteFilter);
      if (defaultLocation != null)
      {
        itemDefaultLocation = this.GetItemLocation(item, defaultLocation);
        itemDefaultLocation.IsDefault = true;
      }
      return (IContentItemLocation) itemDefaultLocation;
    }

    private ContentItemLocation GetItemLocation(
      IDataItem item,
      IContentLocation location)
    {
      return new ContentItemLocation()
      {
        PageId = location.PageId,
        SiteId = location.SiteId,
        ItemAbsoluteUrl = UrlPath.ResolveAbsoluteUrl(location.GetUrl((object) item))
      };
    }

    private void SwapLocationPriorities(
      ContentLocationsManager manager,
      IContentLocation first,
      IContentLocation second)
    {
      ContentLocationDataItem location1 = manager.GetLocation(first.Id);
      ContentLocationDataItem location2 = manager.GetLocation(second.Id);
      this.SwapLocationPriorities(location1, location2);
      this.UpdateContentLocationPriorities(manager, location1);
      this.UpdateContentLocationPriorities(manager, location2);
    }

    private void UpdateContentLocationPriorities(
      ContentLocationsManager manager,
      ContentLocationDataItem contentLocation)
    {
      IQueryable<ContentLocationDataItem> locations = manager.GetLocations();
      Expression<Func<ContentLocationDataItem, bool>> predicate = (Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == contentLocation.PageId && l.ItemType == contentLocation.ItemType && l.Priority > -1 && l.Language == contentLocation.Language);
      foreach (ContentLocationDataItem locationDataItem in (IEnumerable<ContentLocationDataItem>) locations.Where<ContentLocationDataItem>(predicate))
        locationDataItem.Priority = contentLocation.Priority;
    }

    private void SwapLocationPriorities(
      ContentLocationDataItem first,
      ContentLocationDataItem second)
    {
      int priority = first.Priority;
      first.Priority = second.Priority;
      second.Priority = priority;
    }

    private bool TryGetContentLocationItems(
      ContentLocationsManager manager,
      string itemTypeName,
      string itemProvider,
      Guid pageId,
      Guid controlId,
      string language,
      out List<ContentLocationDataItem> contentLocations,
      string descendantTypeName = null)
    {
      IQueryable<ContentLocationDataItem> source1 = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.ItemType == itemTypeName && l.ItemProvider == itemProvider && l.PageId == pageId && (l.ControlId == controlId || (Guid?) l.ControlId == new Guid?() || l.ControlId == Guid.Empty)));
      IQueryable<ContentLocationDataItem> source2;
      if (language.IsNullOrEmpty())
        source2 = source1.Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => string.IsNullOrEmpty(l.Language)));
      else
        source2 = source1.Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.Language == language));
      if (!string.IsNullOrEmpty(descendantTypeName))
        source2 = source2.Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.ItemDescendantType == descendantTypeName));
      contentLocations = source2.ToList<ContentLocationDataItem>();
      return contentLocations.Count > 0;
    }

    private ContentTypeLocationsContainer GetContentTypeLocations(
      Type itemType,
      string itemProvider)
    {
      string itemTypeName = itemType.FullName;
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.ContentLocations);
      string cacheDependencyKey = ContentLocationDataItem.GetCacheDependencyKey(itemTypeName, itemProvider);
      object contentTypeLocations = cacheManager[cacheDependencyKey];
      if (contentTypeLocations == null)
      {
        lock (ContentLocationService.settingsCacheSync)
        {
          contentTypeLocations = cacheManager[cacheDependencyKey];
          if (contentTypeLocations == null)
          {
            IQueryable<ContentLocationDataItem> source = ContentLocationsManager.GetManager().GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.ItemType == itemTypeName && l.ItemProvider == itemProvider && l.Priority != -1));
            contentTypeLocations = (object) new ContentTypeLocationsContainer(this, itemType, itemProvider, (IList<ContentLocationDataItem>) source.OrderBy<ContentLocationDataItem, int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority)).ToList<ContentLocationDataItem>());
            cacheManager.Add(cacheDependencyKey, contentTypeLocations, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (ContentLocationDataItem), cacheDependencyKey), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return (ContentTypeLocationsContainer) contentTypeLocations;
    }

    /// <summary>
    /// Update the new item priority and the old ones accordingly
    /// </summary>
    /// <param name="location"></param>
    /// <param name="info"></param>
    /// <param name="manager"></param>
    /// <param name="itemProvider"></param>
    /// <param name="itemTypeName"></param>
    private static void UpdatePriorities(
      ContentLocationDataItem location,
      IContentLocationInfo info,
      ContentLocationsManager manager,
      string itemProvider,
      string itemTypeName)
    {
      if (info != null && info.Filters.Count<IContentLocationFilter>() == 1 && info.Filters.First<IContentLocationFilter>().GetType() == typeof (ContentLocationSingleItemFilter))
      {
        location.Priority = -1;
      }
      else
      {
        IQueryable<ContentLocationDataItem> source1 = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.ItemType == itemTypeName && l.ItemProvider == itemProvider && l.Priority != -1));
        IQueryable<ContentLocationDataItem> source2 = source1.Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == location.PageId && l.Language == location.Language));
        if (source2.Any<ContentLocationDataItem>())
        {
          location.Priority = source2.First<ContentLocationDataItem>().Priority;
        }
        else
        {
          ContentLocationPriority locationPriority = ContentLocationPriority.Default;
          if (info != null)
            locationPriority = info.Priority;
          if (locationPriority != ContentLocationPriority.Lowest && locationPriority != ContentLocationPriority.Default && locationPriority == ContentLocationPriority.Highest)
          {
            int num1 = source1.Min<ContentLocationDataItem, int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority));
            int num2;
            location.Priority = num2 = num1 - 1;
            if (location.Priority != -1)
              return;
            --location.Priority;
          }
          else
          {
            int num3 = source1.Max<ContentLocationDataItem, int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority));
            int num4;
            location.Priority = num4 = num3 + 1;
            if (location.Priority != -1)
              return;
            ++location.Priority;
          }
        }
      }
    }

    /// <summary>
    /// Checks if the type has a parent that is registered as a base content type
    /// and returns the appropriate parent type name and the descendant type name.
    /// If there is no such parent the descendant type is null and the typeName is the one of the given type.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="itemTypeName"></param>
    /// <param name="descendantTypeName"></param>
    private void GetContentTypeNames(
      Type itemType,
      out string itemTypeName,
      out string descendantTypeName)
    {
      itemTypeName = itemType.FullName;
      descendantTypeName = (string) null;
      foreach (Type baseItemType in this.BaseItemTypes)
      {
        if (baseItemType != itemType && baseItemType.IsAssignableFrom(itemType))
        {
          descendantTypeName = itemTypeName;
          itemTypeName = baseItemType.FullName;
          break;
        }
      }
    }

    /// <summary>
    /// Checks if the given type has a parent type that has been registered as available location type.
    /// </summary>
    /// <param name="itemType"></param>
    private bool HasParentContentItemType(Type itemType, out Type parentType)
    {
      bool flag = false;
      parentType = (Type) null;
      foreach (Type baseItemType in this.BaseItemTypes)
      {
        if (baseItemType.IsAssignableFrom(itemType))
        {
          parentType = baseItemType;
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Gets the base types of the content location items that are able to show content of a descendant type.
    /// </summary>
    /// <value>The base item types.</value>
    private List<Type> BaseItemTypes
    {
      get
      {
        if (this.baseItemTypes == null)
        {
          this.baseItemTypes = new List<Type>();
          if (SystemManager.IsModuleEnabled("Ecommerce"))
            this.baseItemTypes.Add(TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product"));
        }
        return this.baseItemTypes;
      }
    }
  }
}
