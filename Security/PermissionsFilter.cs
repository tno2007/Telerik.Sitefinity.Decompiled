// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.PermissionsFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// The filter responsible for performing the item filtration based on permissions.
  /// </summary>
  public class PermissionsFilter
  {
    private static int typePermissionsCacheMaxTotalCount = Config.Get<SecurityConfig>().PermissionFilterCache.TypePermissionsCacheMaxTotalCount;
    private static int itemPermissionsCacheMaxTotalCount = Config.Get<SecurityConfig>().PermissionFilterCache.ItemPermissionsCacheMaxTotalCount;
    private static Dictionary<string, Dictionary<Guid, bool>> itemPermissionsCache = new Dictionary<string, Dictionary<Guid, bool>>();
    private static int itemPermissionsCacheTotalCount = 0;
    private static Dictionary<string, Dictionary<string, bool>> typePermissionsCache = new Dictionary<string, Dictionary<string, bool>>();
    private static Dictionary<string, HashSet<string>> restrictedTypesCache = new Dictionary<string, HashSet<string>>();
    private static Dictionary<string, DateTime> permissionsUserActivity = new Dictionary<string, DateTime>();
    private static int typePermissionsCacheTotalCount = 0;
    private static object permissionsFilterLock = new object();
    private const string DashboardUserCachePrefix = "dashboardCache_";

    /// <summary>
    /// Determines whether a specific dataItem can be modified/managed by the current user
    /// </summary>
    /// <param name="dataItem">The data item to be checked</param>
    /// <param name="action">The permission action</param>
    /// <returns>A value indicating whether a specific dataItem can be modified/managed by the current user</returns>
    public static bool IsCurrentUserAllowedAccessItem(
      IDataItem dataItem,
      PermissionsFilter.PermissionAction action)
    {
      string str = string.Format(ClaimsManager.GetCurrentUserId().ToString() + "_" + (object) action);
      lock (PermissionsFilter.permissionsFilterLock)
      {
        if (PermissionsFilter.permissionsUserActivity.ContainsKey(str))
          PermissionsFilter.permissionsUserActivity[str] = DateTime.Now;
        else if (!PermissionsFilter.permissionsUserActivity.ContainsKey(str))
          PermissionsFilter.permissionsUserActivity.Add(str, DateTime.Now);
        int slidingExpirationTime = Config.Get<SecurityConfig>().PermissionFilterCache.PermissionsCacheSlidingExpirationTime;
        PermissionsFilter.PermissionsFilterCache.Add("dashboardCache_" + str, new object(), CacheItemPriority.Normal, (ICacheItemRefreshAction) new PermissionsFilter.DisposeItemOnExpire(), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes((double) slidingExpirationTime)), (ICacheItemExpiration) new DataItemCacheDependency(typeof (Telerik.Sitefinity.Security.Model.Permission), (string) null));
        string fullName = dataItem.GetType().FullName;
        IManager mappedManager = ManagerBase.GetMappedManager(fullName);
        if (mappedManager != null)
        {
          if (PermissionsFilter.typePermissionsCache.ContainsKey(str))
          {
            Dictionary<string, bool> currentUserCache = PermissionsFilter.typePermissionsCache[str];
            bool canAccessType = PermissionsFilter.IsCurrentUserAllowed(mappedManager, dataItem, action);
            PermissionsFilter.AddToTypePermissionsCache(fullName, canAccessType, currentUserCache);
          }
          else
          {
            Dictionary<string, bool> currentUserCache = new Dictionary<string, bool>();
            bool canAccessType = PermissionsFilter.IsCurrentUserAllowed(mappedManager, dataItem, action);
            PermissionsFilter.AddToTypePermissionsCache(fullName, canAccessType, currentUserCache);
            PermissionsFilter.typePermissionsCache.Add(str, currentUserCache);
            if (!PermissionsFilter.restrictedTypesCache.ContainsKey(str))
              PermissionsFilter.restrictedTypesCache.Add(str, new HashSet<string>());
            PermissionsFilter.restrictedTypesCache[str].Add(fullName);
          }
        }
        return PermissionsFilter.AddItemToCache(dataItem, str, action);
      }
    }

    /// <summary>Verifies whether given item has modify permissions.</summary>
    /// <param name="item">The item</param>
    /// <returns>True or false</returns>
    public static bool IsModifyPermissionGranted(object item)
    {
      if (!(item is ISecuredObject secObj))
        return true;
      string permissionSet = "Pages";
      if (((IEnumerable<string>) secObj.SupportedPermissionSets).Contains<string>(permissionSet))
        return secObj.IsGranted(permissionSet, "EditContent");
      if (secObj.IsSecurityActionSupported(SecurityActionTypes.Manage))
        return secObj.IsSecurityActionTypeGranted(SecurityActionTypes.Manage);
      return !secObj.IsSecurityActionSupported(SecurityActionTypes.Modify) || secObj.IsSecurityActionTypeGranted(SecurityActionTypes.Modify);
    }

    /// <summary>
    /// Verifies whether a data item with view or modify permissions exists in the current permission cache.
    /// </summary>
    /// <param name="dataItem">The data item</param>
    /// <param name="userId">The user id</param>
    /// <param name="action">The PermissionAction value</param>
    /// <returns>True or false</returns>
    public static bool AddItemToCache(
      IDataItem dataItem,
      string userId,
      PermissionsFilter.PermissionAction action)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (PermissionsFilter.itemPermissionsCache.ContainsKey(userId))
      {
        Dictionary<Guid, bool> currentUserCache = PermissionsFilter.itemPermissionsCache[userId];
        Guid id = dataItem.Id;
        if (currentUserCache.ContainsKey(id))
          return currentUserCache[id];
        bool canAccessItem = PermissionsFilter.IsActionGranted(dataItem, action);
        PermissionsFilter.AddToItemPermissionsCache(id, canAccessItem, currentUserCache);
        return canAccessItem;
      }
      Dictionary<Guid, bool> currentUserCache1 = new Dictionary<Guid, bool>();
      bool canAccessItem1 = PermissionsFilter.IsActionGranted(dataItem, action);
      PermissionsFilter.AddToItemPermissionsCache(dataItem.Id, canAccessItem1, currentUserCache1);
      if (!PermissionsFilter.itemPermissionsCache.ContainsKey(userId))
        PermissionsFilter.itemPermissionsCache.Add(userId, currentUserCache1);
      return canAccessItem1;
    }

    /// <summary>
    /// Adds the dataItemId to the current permissions cache if it does not exist
    /// </summary>
    /// <param name="dataItemId">The data item id</param>
    /// <param name="canAccessItem">The value whether the user can modify or view the item</param>
    /// <param name="currentUserCache">The current user cache</param>
    public static void AddToItemPermissionsCache(
      Guid dataItemId,
      bool canAccessItem,
      Dictionary<Guid, bool> currentUserCache)
    {
      if (currentUserCache == null)
        throw new ArgumentNullException(nameof (currentUserCache));
      lock (PermissionsFilter.permissionsFilterLock)
      {
        if (PermissionsFilter.itemPermissionsCacheTotalCount > PermissionsFilter.itemPermissionsCacheMaxTotalCount)
        {
          string userToDrop = PermissionsFilter.GetUserToDrop();
          if (!PermissionsFilter.itemPermissionsCache.ContainsKey(userToDrop))
            return;
          PermissionsFilter.itemPermissionsCacheTotalCount -= PermissionsFilter.itemPermissionsCache[userToDrop].Keys.Count;
          PermissionsFilter.itemPermissionsCache.Remove(userToDrop);
        }
        else
        {
          if (currentUserCache.ContainsKey(dataItemId))
            return;
          currentUserCache.Add(dataItemId, canAccessItem);
          ++PermissionsFilter.itemPermissionsCacheTotalCount;
        }
      }
    }

    /// <summary>Adds the current type to the permissions cache</summary>
    /// <param name="type">The specific type</param>
    /// <param name="canAccessType">The value indicating whether the user can view / modify the item</param>
    /// <param name="currentUserCache">The current user cache dictionary</param>
    public static void AddToTypePermissionsCache(
      string type,
      bool canAccessType,
      Dictionary<string, bool> currentUserCache)
    {
      if (currentUserCache == null)
        throw new ArgumentNullException(nameof (currentUserCache));
      lock (PermissionsFilter.permissionsFilterLock)
      {
        if (PermissionsFilter.typePermissionsCacheTotalCount > PermissionsFilter.typePermissionsCacheMaxTotalCount)
        {
          string userToDrop = PermissionsFilter.GetUserToDrop();
          if (!PermissionsFilter.typePermissionsCache.ContainsKey(userToDrop))
            return;
          PermissionsFilter.typePermissionsCacheTotalCount -= PermissionsFilter.typePermissionsCache[userToDrop].Keys.Count;
          PermissionsFilter.typePermissionsCache.Remove(userToDrop);
        }
        else
        {
          if (currentUserCache.ContainsKey(type))
            return;
          currentUserCache.Add(type, canAccessType);
          ++PermissionsFilter.typePermissionsCacheTotalCount;
        }
      }
    }

    /// <summary>
    /// Retrieves from cache the types that current user can't modify
    /// </summary>
    /// <returns>The types that current user can't modify</returns>
    public static List<string> GetCurrentUserRestrictedTypes()
    {
      lock (PermissionsFilter.permissionsFilterLock)
      {
        string key = ClaimsManager.GetCurrentUserId().ToString();
        return PermissionsFilter.restrictedTypesCache.ContainsKey(key) ? PermissionsFilter.restrictedTypesCache[key].ToList<string>() : new List<string>();
      }
    }

    /// <summary>
    /// Clears the current permissions cache in the Dashboard module
    /// </summary>
    public static void ClearCache()
    {
      lock (PermissionsFilter.permissionsFilterLock)
      {
        PermissionsFilter.itemPermissionsCache.Clear();
        PermissionsFilter.itemPermissionsCacheTotalCount = 0;
        PermissionsFilter.typePermissionsCache.Clear();
        PermissionsFilter.typePermissionsCacheTotalCount = 0;
        PermissionsFilter.restrictedTypesCache.Clear();
      }
    }

    /// <summary>Checks if the current user can modify the given item.</summary>
    /// <param name="dataItem">The data item to check</param>
    /// <param name="action">The current users permission action</param>
    /// <returns>A value, indicating if the current user can modify the given item</returns>
    public static bool IsActionGranted(
      IDataItem dataItem,
      PermissionsFilter.PermissionAction action)
    {
      switch (action)
      {
        case PermissionsFilter.PermissionAction.View:
          bool flag = PermissionsFilter.VerifyItemParentIsAccessible(dataItem);
          return flag ? PermissionsFilter.IsViewPermissionGranted((object) dataItem) : flag;
        case PermissionsFilter.PermissionAction.Modify:
          return PermissionsFilter.IsModifyPermissionGranted((object) dataItem);
        default:
          return false;
      }
    }

    /// <summary>Verifies whether given item has view permissions.</summary>
    /// <param name="item">The item</param>
    /// <returns>True or false</returns>
    internal static bool IsViewPermissionGranted(object item)
    {
      if (!(item is ISecuredObject secObj))
        return true;
      string permissionSet = "Pages";
      if (((IEnumerable<string>) secObj.SupportedPermissionSets).Contains<string>(permissionSet))
      {
        if (secObj.IsGranted(permissionSet, "View"))
          return true;
      }
      return secObj.IsSecurityActionTypeGranted(SecurityActionTypes.View);
    }

    private static bool VerifyItemParentIsAccessible(IDataItem item)
    {
      hasIdataItemParent = item as IHasIDataItemParent;
      bool flag = true;
      while (hasIdataItemParent != null)
      {
        switch (hasIdataItemParent.ItemParent)
        {
          case null:
            return flag;
          case ISecuredObject secObj:
            return secObj.IsSecurityActionTypeGranted(SecurityActionTypes.View);
          case IHasIDataItemParent hasIdataItemParent:
            continue;
          default:
            return flag;
        }
      }
      return flag;
    }

    private static bool IsCurrentUserAllowed(
      IManager manager,
      IDataItem dataItem,
      PermissionsFilter.PermissionAction action)
    {
      if (!SystemManager.TypeRegistry.IsRegistered(dataItem.GetType().FullName))
        return false;
      object obj = (object) null;
      PageNode pageNode = dataItem as PageNode;
      if (manager is PageManager && pageNode != null)
        obj = (object) pageNode.RootNode;
      if (obj == null)
        obj = (object) manager.GetSecurityRoot();
      ISecuredObject securedObject = dataItem as ISecuredObject;
      switch (action)
      {
        case PermissionsFilter.PermissionAction.View:
          return securedObject != null ? PermissionsFilter.IsViewPermissionGranted((object) securedObject) : PermissionsFilter.IsViewPermissionGranted(obj);
        case PermissionsFilter.PermissionAction.Modify:
          return PermissionsFilter.IsCreatePermissionGranted(obj) || PermissionsFilter.IsModifyPermissionGranted(obj);
        default:
          return false;
      }
    }

    private static ICacheManager PermissionsFilterCache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    private static bool IsCreatePermissionGranted(object item)
    {
      if (!(item is ISecuredObject secObj))
        return true;
      string permissionSet = "Pages";
      if (((IEnumerable<string>) secObj.SupportedPermissionSets).Contains<string>(permissionSet))
      {
        if (secObj.IsGranted(permissionSet, "Create"))
          return true;
      }
      return secObj.IsSecurityActionTypeGranted(SecurityActionTypes.Create);
    }

    private static string GetUserToDrop()
    {
      if (PermissionsFilter.permissionsUserActivity.Keys.Count <= 0)
        return string.Empty;
      string key = PermissionsFilter.permissionsUserActivity.OrderBy<KeyValuePair<string, DateTime>, DateTime>((Func<KeyValuePair<string, DateTime>, DateTime>) (p => p.Value)).FirstOrDefault<KeyValuePair<string, DateTime>>().Key;
      PermissionsFilter.permissionsUserActivity.Remove(key);
      return key;
    }

    /// <summary>Defines permission names.</summary>
    public enum PermissionAction
    {
      /// <summary>View permissions</summary>
      View,
      /// <summary>Modify permissions</summary>
      Modify,
    }

    private class DisposeItemOnExpire : ICacheItemRefreshAction
    {
      public virtual void Refresh(
        string removedKey,
        object expiredValue,
        CacheItemRemovedReason removalReason)
      {
        lock (PermissionsFilter.permissionsFilterLock)
        {
          string key = removedKey.Replace("dashboardCache_", string.Empty);
          if (PermissionsFilter.typePermissionsCache.ContainsKey(key))
          {
            PermissionsFilter.typePermissionsCacheTotalCount -= PermissionsFilter.typePermissionsCache[key].Keys.Count;
            PermissionsFilter.typePermissionsCache.Remove(key);
          }
          if (PermissionsFilter.itemPermissionsCache.ContainsKey(key))
          {
            PermissionsFilter.itemPermissionsCacheTotalCount -= PermissionsFilter.itemPermissionsCache[key].Keys.Count;
            PermissionsFilter.itemPermissionsCache.Remove(key);
          }
          if (PermissionsFilter.restrictedTypesCache.ContainsKey(key))
            PermissionsFilter.restrictedTypesCache.Remove(key);
          if (!PermissionsFilter.permissionsUserActivity.ContainsKey(key))
            return;
          PermissionsFilter.permissionsUserActivity.Remove(key);
        }
      }
    }
  }
}
