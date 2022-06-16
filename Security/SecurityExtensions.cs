// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecurityExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Contains extension methods for security classes.</summary>
  public static class SecurityExtensions
  {
    /// <summary>
    /// Throws security exception if the requested actions are not permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions to check.</param>
    public static void Demand(this ISecuredObject item, string permissionSet, int actions) => item.Demand(permissionSet, (Guid[]) null, actions);

    /// <summary>
    /// Throws security exception if the requested actions are not permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    public static void Demand(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions)
    {
      item.Demand(permissionSet, principals, actions, (IOwnership) null);
    }

    /// <summary>
    /// Determines whether the item is accessible to everyone.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    internal static bool IsAccessibleToEveryone(this ISecuredObject item)
    {
      IQueryable<Telerik.Sitefinity.Security.Model.Permission> activePermissions = item.GetActivePermissions();
      if (!activePermissions.Any<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == SecurityManager.EveryoneRole.Id && p.Grant > 0)))
        return false;
      return !activePermissions.Any<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.Deny > 0));
    }

    /// <summary>
    /// Throws security exception if the requested actions are not permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    internal static void Demand(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions,
      IOwnership actualOwnerObject)
    {
      bool flag = false;
      if (item is IDataItem dataItem && dataItem.Provider is DataProviderBase provider)
        flag = provider.SuppressSecurityChecks;
      if (flag || item.IsGranted(permissionSet, principals, actions, actualOwnerObject))
        return;
      SecurityExtensions.ThrowSecurityException(item, permissionSet, principals, actions);
    }

    private static void ThrowSecurityException(
      ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions)
    {
      throw SecurityDemandFailException.Create(item, permissionSet, principals, actions);
    }

    /// <summary>
    /// Determines whether the specified actions are granted for the provided item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions.</param>
    /// <returns>
    /// 	<c>true</c> if the specified item is granted; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsGranted(
      this ISecuredObject item,
      string permissionSet,
      params string[] actions)
    {
      int actions1 = 0;
      Telerik.Sitefinity.Security.Configuration.Permission permission;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSet, out permission))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permissionSet));
      if (!((IEnumerable<string>) item.SupportedPermissionSets).Contains<string>(permissionSet))
        throw new ArgumentException(Res.Get<ErrorMessages>().ObjectDoesNotSupportPermissionSet.Arrange((object) item.GetType().FullName, (object) permissionSet));
      if (actions != null)
      {
        foreach (string action in actions)
        {
          SecurityAction securityAction;
          if (!permission.Actions.TryGetValue(action, out securityAction))
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permissionSet));
          actions1 |= securityAction.Value;
        }
      }
      return item.IsGranted(permissionSet, (Guid[]) null, actions1);
    }

    /// <summary>
    /// Determines whether the specified actions are granted for the provided item for the provided principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals.</param>
    /// <param name="actions">The actions.</param>
    /// <returns>
    ///     <c>true</c> if the specified actions are granted for the provided item for the provided principals; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsGranted(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      params string[] actions)
    {
      int actions1 = 0;
      Telerik.Sitefinity.Security.Configuration.Permission permission;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSet, out permission))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permissionSet));
      if (!((IEnumerable<string>) item.SupportedPermissionSets).Contains<string>(permissionSet))
        throw new ArgumentException(Res.Get<ErrorMessages>().ObjectDoesNotSupportPermissionSet.Arrange((object) item.GetType().FullName, (object) permissionSet));
      if (actions != null)
      {
        foreach (string action in actions)
        {
          SecurityAction securityAction;
          if (!permission.Actions.TryGetValue(action, out securityAction))
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permissionSet));
          actions1 |= securityAction.Value;
        }
      }
      return item.IsGranted(permissionSet, principals, actions1);
    }

    /// <summary>
    /// Checks if the requested actions are permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are permitted to the current user; otherwise, false.
    /// </returns>
    public static bool IsGranted(this ISecuredObject item, string permissionSet, int actions) => item.IsGranted(permissionSet, (Guid[]) null, actions);

    /// <summary>
    /// Checks if the requested actions are permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are permitted to the current user; otherwise, false.
    /// </returns>
    public static bool IsGranted(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions)
    {
      return item.IsGranted(permissionSet, principals, actions, (IOwnership) null);
    }

    /// <summary>Determines whether the specified item is granted.</summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions.</param>
    /// <param name="actualOwnership">The actual ownership.</param>
    /// <returns>
    /// 	<c>true</c> if the specified item is granted; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsGranted(
      this ISecuredObject item,
      string permissionSet,
      int actions,
      IOwnership actualOwnerObject)
    {
      return item.IsGranted(permissionSet, (Guid[]) null, actions, actualOwnerObject);
    }

    /// <summary>
    /// Checks if the requested actions are permitted to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    /// <param name="actualOwnerObject">The ownership actual object.</param>
    /// <returns>
    /// true if the requested actions are permitted to the current user; otherwise, false.
    /// </returns>
    internal static bool IsGranted(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions,
      IOwnership actualOwnerObject)
    {
      if (string.IsNullOrEmpty(permissionSet))
        throw new ArgumentNullException(nameof (permissionSet));
      if (!item.IsPermissionSetSupported(permissionSet))
        throw new NotSupportedException(Res.Get<SecurityResources>().SecuredObjectWithIdDoesNotSupportPermissionSet.Arrange((object) item.GetType(), (object) item.Id, (object) permissionSet, (object) string.Join(", ", item.SupportedPermissionSets)));
      if (actions == 0)
        return true;
      int num1 = 0;
      int num2 = 0;
      if (principals == null)
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity == null)
          throw new InvalidOperationException(Res.Get<SecurityResources>().MissingCurrentPrincipal);
        if (currentIdentity.IsUnrestricted)
          return true;
        if (!currentIdentity.IsGlobalUser)
        {
          if (!SecurityExtensions.IsActionAllowedPerSite(item, permissionSet, actions, currentIdentity))
            return false;
          if (currentIdentity.IsAdmin)
            return true;
        }
        bool flag;
        if (currentIdentity.IsAuthenticated)
        {
          IOwnership ownership = actualOwnerObject != null ? actualOwnerObject : item as IOwnership;
          flag = ownership != null && ownership.Owner == currentIdentity.UserId;
        }
        else
          flag = false;
        Guid id = SecurityManager.OwnerRole.Id;
        foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) item.GetActivePermissions())
        {
          Telerik.Sitefinity.Security.Model.Permission perm = activePermission;
          if (perm.SetName == permissionSet && (perm.PrincipalId == currentIdentity.UserId || currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == perm.PrincipalId)) || flag && id == perm.PrincipalId))
          {
            num1 |= perm.Grant;
            num2 |= perm.Deny;
          }
        }
      }
      else
      {
        IOwnership ownership = actualOwnerObject != null ? actualOwnerObject : item as IOwnership;
        bool flag = ownership == null || !(ownership.Owner != Guid.Empty) || ((IEnumerable<Guid>) principals).Contains<Guid>(ownership.Owner);
        Guid id = SecurityManager.OwnerRole.Id;
        foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) item.GetActivePermissions())
        {
          if (activePermission.SetName == permissionSet && ((IEnumerable<Guid>) principals).Contains<Guid>(activePermission.PrincipalId) || flag && id == activePermission.PrincipalId)
          {
            num1 |= activePermission.Grant;
            num2 |= activePermission.Deny;
          }
        }
      }
      if (num2 > 0 && (actions & num2) != 0)
        return false;
      if (permissionSet != "Backend")
      {
        if ((actions & 1) == 1 && num1 > 0)
          return true;
      }
      else if ((num1 & actions) == actions)
        return true;
      return (actions & ~num1) == 0;
    }

    private static bool IsActionAllowedPerSite(
      ISecuredObject item,
      string permissionSet,
      int actions,
      SitefinityIdentity user)
    {
      if (item is ISiteSpecificSecuredObject || !(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext))
        return true;
      if (permissionSet == "Site" && item is Site)
      {
        if (multisiteContext.GetAllowedSites(user.UserId, user.MembershipProvider).Contains<Guid>(item.Id))
        {
          Telerik.Sitefinity.Security.Configuration.Permission permission = (Telerik.Sitefinity.Security.Configuration.Permission) null;
          SecurityAction securityAction;
          if (Config.Get<SecurityConfig>().Permissions.TryGetValue("Site", out permission) && permission.Actions.TryGetValue("AccessSite", out securityAction) && (actions & securityAction.Value) == actions)
            return true;
        }
        return false;
      }
      Type type = (Type) null;
      dataProviderBase1 = (DataProviderBase) null;
      switch (item)
      {
        case IDataItem dataItem:
          type = !(dataItem.GetProvider() is DataProviderBase dataProviderBase1) ? ManagerBase.GetMappedManagerType(dataItem.GetType()) : dataProviderBase1.TheManagerType;
          break;
        case DataProviderBase dataProviderBase2:
          type = dataProviderBase2.TheManagerType;
          dataProviderBase1 = dataProviderBase2;
          break;
        case SecuredProxy securedProxy:
          type = securedProxy.ManagerType;
          break;
      }
      if (dataProviderBase1 != null && type == typeof (UserManager))
      {
        IEnumerable<Guid> allowedSites = multisiteContext.GetAllowedSites(user.UserId, user.MembershipProvider);
        if (!multisiteContext.GetDataSourcesByManager(typeof (UserManager).FullName).Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (p => p.Sites.Any<Guid>((Func<Guid, bool>) (s => allowedSites.Contains<Guid>(s))))).Select<ISiteDataSource, string>((Func<ISiteDataSource, string>) (p => p.Provider)).Contains<string>(dataProviderBase1.Name))
          return false;
      }
      if (type != (Type) null)
      {
        if (typeof (IMultisiteEnabledManager).IsAssignableFrom(type) && !typeof (IMultisiteEnabledManagerExtended).IsAssignableFrom(type))
        {
          if (dataProviderBase1 != null)
          {
            Type itemType = item.GetType();
            IMultisiteEnabledManager multisiteEnabledManager = !dataProviderBase1.TransactionName.IsNullOrEmpty() ? (IMultisiteEnabledManager) ManagerBase.GetManagerInTransaction(type, dataProviderBase1.Name, dataProviderBase1.TransactionName) : (IMultisiteEnabledManager) ManagerBase.GetManager(type, dataProviderBase1.Name);
            if (((IEnumerable<Type>) multisiteEnabledManager.GetShareableTypes()).Any<Type>((Func<Type, bool>) (t => t.IsAssignableFrom(itemType))) || ((IEnumerable<Type>) multisiteEnabledManager.GetSiteSpecificTypes()).Any<Type>((Func<Type, bool>) (t => t.IsAssignableFrom(itemType))))
              return ((IMultisiteEnabledProvider) multisiteEnabledManager.Provider).GetItemSiteLinks(dataItem).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).ToHashSet<Guid>().IsSubsetOf(multisiteContext.GetAllowedSites(user.UserId, user.MembershipProvider)) || actions == 1;
          }
          else if (typeof (PublishingManager).IsAssignableFrom(type))
            return true;
        }
        if (typeof (IDataSource).IsAssignableFrom(type))
          return true;
      }
      int num = 0;
      EffectivePermission effectivePermission;
      if (Config.Get<SecurityConfig>().UsersPerSiteSettings.SiteAdminPermissions.TryGetValue(permissionSet, out effectivePermission))
        num |= effectivePermission.Grant;
      return (num & actions) == actions;
    }

    /// <summary>
    /// Checks if the requested actions are denied to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are denied to the current user; otherwise, false.
    /// </returns>
    public static bool IsDenied(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions)
    {
      return item.IsDenied(permissionSet, (Guid[]) null, actions, (IOwnership) null);
    }

    /// <summary>
    /// Determines whether the specified actions are denied for the provided item for the provided principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals.</param>
    /// <param name="actions">The actions.</param>
    /// <returns>true if the requested actions are denied for the provided item for the provided principals; otherwise, false.</returns>
    public static bool IsDenied(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      params string[] actions)
    {
      int actions1 = 0;
      Telerik.Sitefinity.Security.Configuration.Permission permission;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSet, out permission))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permissionSet));
      if (!((IEnumerable<string>) item.SupportedPermissionSets).Contains<string>(permissionSet))
        throw new ArgumentException(Res.Get<ErrorMessages>().ObjectDoesNotSupportPermissionSet.Arrange((object) item.GetType().FullName, (object) permissionSet));
      if (actions != null)
      {
        foreach (string action in actions)
        {
          SecurityAction securityAction;
          if (!permission.Actions.TryGetValue(action, out securityAction))
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permissionSet));
          actions1 |= securityAction.Value;
        }
      }
      return item.IsDenied(permissionSet, principals, actions1);
    }

    /// <summary>
    /// Checks if the requested actions are denied to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="principals">The principals IDs.</param>
    /// <param name="actions">The actions to check.</param>
    /// <param name="actualOwnerObject">The actual owner object.</param>
    /// <returns>
    /// true if the requested actions are denied to the current user; otherwise, false.
    /// </returns>
    internal static bool IsDenied(
      this ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions,
      IOwnership actualOwnerObject)
    {
      if (string.IsNullOrEmpty(permissionSet))
        throw new ArgumentNullException(nameof (permissionSet));
      if (actions == 0)
        return true;
      int num1 = 0;
      int num2 = 0;
      if (principals == null)
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity.IsUnrestricted)
          return true;
        bool flag;
        if (currentIdentity.IsAuthenticated)
        {
          IOwnership ownership = actualOwnerObject != null ? actualOwnerObject : item as IOwnership;
          flag = ownership != null && ownership.Owner == currentIdentity.UserId;
        }
        else
          flag = false;
        Guid id = SecurityManager.OwnerRole.Id;
        foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) item.GetActivePermissions())
        {
          Telerik.Sitefinity.Security.Model.Permission perm = activePermission;
          if (perm.SetName == permissionSet && (perm.PrincipalId == currentIdentity.UserId || currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == perm.PrincipalId))) || flag && id == perm.PrincipalId)
          {
            num1 |= perm.Grant;
            num2 |= perm.Deny;
          }
        }
      }
      else
      {
        foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) item.GetActivePermissions())
        {
          if (activePermission.SetName == permissionSet && ((IEnumerable<Guid>) principals).Contains<Guid>(activePermission.PrincipalId))
          {
            num1 |= activePermission.Grant;
            num2 |= activePermission.Deny;
          }
        }
      }
      return (num1 <= 0 || (num2 & ~num1) != 0) && (actions & ~num2) == 0;
    }

    /// <summary>
    /// Throws security exception if the requested actions are not denied to any of the specified principals.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions to check.</param>
    public static bool IsDenied(this ISecuredObject item, string permissionSet, int actions) => item.IsDenied(permissionSet, (Guid[]) null, actions);

    /// <summary>
    /// Determines whether the specified actions are denied for the provided item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions.</param>
    /// <returns>
    /// 	<c>true</c> if the specified item is denied; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDenied(
      this ISecuredObject item,
      string permissionSet,
      params string[] actions)
    {
      int actions1 = 0;
      Telerik.Sitefinity.Security.Configuration.Permission permission;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSet, out permission))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permissionSet));
      if (actions != null)
      {
        foreach (string action in actions)
        {
          SecurityAction securityAction;
          if (!permission.Actions.TryGetValue(action, out securityAction))
            throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permissionSet));
          actions1 |= securityAction.Value;
        }
      }
      return item.IsDenied(permissionSet, (Guid[]) null, actions1);
    }

    /// <summary>
    /// Checks if the requested actions are denied to this permission.
    /// </summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are denied the specified actions; otherwise, false.
    /// </returns>
    public static bool IsDenied(this Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permission.SetName));
        int num = securityAction.Value;
        if ((permission.Deny & num) == num)
          return true;
      }
      return false;
    }

    /// <summary>Grants the specified actions to this permissions.</summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to grant.</param>
    /// <param name="append">
    /// if set to <c>true</c> ensures the specified actions are granted without removing any previously set actions.
    /// if set to <c>false</c> any previously set actions are cleared and the new ones are set.
    /// </param>
    public static void GrantActions(
      this Telerik.Sitefinity.Security.Model.Permission permission,
      bool append,
      params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
      if (!append)
        permission.Grant = 0;
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permission.SetName));
        int num = securityAction.Value;
        if ((permission.Grant & num) != num)
          permission.Grant |= num;
      }
    }

    /// <summary>
    /// Reset specific granted actions (make sure the principal is not granted those actions).
    /// This action always appends to the existing permission, and does not reset the whole grant value
    /// (otherwise the result would always be Grant=0)
    /// </summary>
    /// <param name="permission">The Permission</param>
    /// <param name="actions">The actions to ungrant.</param>
    public static void UngrantActions(this Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permission.SetName));
        int num = securityAction.Value;
        permission.Grant &= ~num;
      }
    }

    /// <summary>
    /// Reset specific denied actions (make sure the principal is not denied those actions)
    /// This action always appends to the existing permission, and does not reset the whole deny value
    /// (otherwise the result would always be Deny=0)
    /// </summary>
    /// <param name="permission">The Permission</param>
    /// <param name="actions">The actions to undeny.</param>
    public static void UndenyActions(this Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permission.SetName));
        int num = securityAction.Value;
        permission.Deny &= ~num;
      }
    }

    /// <summary>
    /// Checks if the requested actions are granted to this permission.
    /// </summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to check.</param>
    /// <returns>
    /// true if the requested actions are granted the specified actions; otherwise, false.
    /// </returns>
    public static bool IsGranted(this Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) action, (object) permission.SetName));
        int num = securityAction.Value;
        if ((permission.Deny & num) == num || (permission.Grant & num) != num)
          return false;
      }
      return true;
    }

    /// <summary>Denies the specified actions to this permissions.</summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to deny.</param>
    /// <param name="append">
    /// if set to <c>true</c> ensures the specified actions are denied without removing any previously set actions.
    /// if set to <c>false</c> any previously set actions are cleared and the new ones are set.
    /// </param>
    public static void DenyActions(
      this Telerik.Sitefinity.Security.Model.Permission permission,
      bool append,
      params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permission.SetName, out permission1))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) permission.SetName));
      if (!append)
        permission.Deny = 0;
      foreach (string action in actions)
      {
        SecurityAction securityAction;
        if (!permission1.Actions.TryGetValue(action, out securityAction))
          throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permission.SetName));
        int num = securityAction.Value;
        if ((permission.Deny & num) != num)
          permission.Deny |= num;
      }
    }

    /// <summary>
    /// Gets the title of a security action.
    /// If there are unique configuration settings based on the secured object type, they are also retrieved.
    /// </summary>
    /// <param name="action">The configuration security action.</param>
    /// <returns>The security action title.</returns>
    public static string GetTitle(this SecurityAction action) => action.GetTitle((ISecuredObject) null, out bool _);

    /// <summary>
    /// Gets the title of a security action, for a specific secured object.
    /// If there are unique configuration settings based on the secured object type, they are also retrieved.
    /// </summary>
    /// <param name="action">The configuration security action.</param>
    /// <param name="showAction">if set to <c>true</c> this action should appear on the permissions list.</param>
    /// <returns>The security action title.</returns>
    public static string GetTitle(this SecurityAction action, out bool showAction) => action.GetTitle((ISecuredObject) null, out showAction);

    /// <summary>
    /// Gets the title of a security action, for a specific secured object.
    /// If there are unique configuration settings based on the secured object type, they are also retrieved.
    /// </summary>
    /// <param name="action">The configuration security action.</param>
    /// <param name="securedObject">The secured object.</param>
    /// <returns>The security action title.</returns>
    public static string GetTitle(this SecurityAction action, ISecuredObject securedObject) => action.GetTitle(securedObject, out bool _);

    /// <summary>
    /// Gets the title of a security action, for a specific secured object.
    /// If there are unique configuration settings based on the secured object type, they are also retrieved.
    /// </summary>
    /// <param name="action">The configuration security action.</param>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="showAction">if set to <c>true</c> this action should appear on the permissions list.</param>
    /// <returns>The security action title.</returns>
    public static string GetTitle(
      this SecurityAction action,
      ISecuredObject securedObject,
      out bool showAction)
    {
      if (action is IModuleDependentItem moduleDependentItem && !SystemManager.ValidateModuleItem(moduleDependentItem))
      {
        showAction = false;
        return action.Title;
      }
      showAction = true;
      string str = string.Empty;
      string title = string.Format(action.Title, (object) str);
      Telerik.Sitefinity.Security.Configuration.Permission parent = (Telerik.Sitefinity.Security.Configuration.Permission) action.Parent.Parent;
      Type objectType = securedObject == null ? (Type) null : securedObject.GetType();
      if (securedObject is IDynamicSecuredObject)
      {
        if (securedObject is DynamicContent && action.Name == "Create")
          showAction = false;
        if (action.Name == "ChangeBackendLinkPermissions")
          showAction = false;
        return ((IDynamicSecuredObject) securedObject).GetSecurityActionTitle(action.Title);
      }
      if (securedObject != null && parent != null && !string.IsNullOrEmpty(parent.Name))
      {
        string name = parent.Name;
        if (securedObject.PermissionsetObjectTitleResKeys.Keys.Contains(name))
        {
          string objectTitleResKey = securedObject.PermissionsetObjectTitleResKeys[name];
          str = !Res.Get<SecurityResources>().Properties.Contains(objectTitleResKey) ? objectTitleResKey : Res.Get<SecurityResources>(objectTitleResKey);
          title = string.Format(action.Title, (object) str);
        }
        if (Config.Get<SecurityConfig>().CustomPermissionsDisplaySettings.ContainsKey(name))
        {
          CustomPermissionsDisplaySettingsConfig permissionsDisplaySetting = Config.Get<SecurityConfig>().CustomPermissionsDisplaySettings[name];
          if (permissionsDisplaySetting != null && objectType != (Type) null)
          {
            SecuredObjectCustomPermissionSet customPermissionSet = permissionsDisplaySetting.SecuredObjectCustomPermissionSets.Values.FirstOrDefault<SecuredObjectCustomPermissionSet>((Func<SecuredObjectCustomPermissionSet, bool>) (cps =>
            {
              if (!(cps.TypeName == objectType.FullName) || string.IsNullOrWhiteSpace(cps.SecuredObjectIds))
                return false;
              return ((IEnumerable<string>) cps.SecuredObjectIds.Split(',')).Contains<string>(securedObject.Id.ToString());
            }));
            if (customPermissionSet == null)
            {
              customPermissionSet = permissionsDisplaySetting.SecuredObjectCustomPermissionSets.Values.FirstOrDefault<SecuredObjectCustomPermissionSet>((Func<SecuredObjectCustomPermissionSet, bool>) (cps => cps.TypeName == objectType.FullName && string.IsNullOrWhiteSpace(cps.SecuredObjectIds)));
              if (customPermissionSet == null)
              {
                foreach (string key in (IEnumerable<string>) permissionsDisplaySetting.SecuredObjectCustomPermissionSets.Keys)
                {
                  Type type = TypeResolutionService.ResolveType(key, false);
                  if (type != (Type) null && type.IsAssignableFrom(objectType))
                  {
                    if (!string.IsNullOrWhiteSpace(permissionsDisplaySetting.SecuredObjectCustomPermissionSets[key].SecuredObjectIds))
                    {
                      if (!((IEnumerable<string>) permissionsDisplaySetting.SecuredObjectCustomPermissionSets[key].SecuredObjectIds.Split(',')).Contains<string>(securedObject.Id.ToString()))
                        continue;
                    }
                    customPermissionSet = permissionsDisplaySetting.SecuredObjectCustomPermissionSets[key];
                  }
                }
              }
            }
            if (customPermissionSet != null && customPermissionSet.CustomSecurityActions.ContainsKey(action.Name.ToString()))
            {
              CustomSecurityAction customSecurityAction = customPermissionSet.CustomSecurityActions[action.Name.ToString()];
              if (customSecurityAction != null)
              {
                showAction = customSecurityAction.ShowActionInList;
                string classId = customSecurityAction.ResourceClassId.IsNullOrEmpty() ? customSecurityAction.ResourceClassId : action.ResourceClassId;
                if (!string.IsNullOrEmpty(customSecurityAction.Title))
                  title = string.Format(!string.IsNullOrEmpty(classId) ? Res.Get(classId, customSecurityAction.Title) : customSecurityAction.Title, (object) str);
              }
            }
          }
        }
        if (string.IsNullOrEmpty(title))
          title = action.Name;
      }
      return title;
    }

    /// <summary>
    /// Determines whether a specific action type is allowed for the secured object.
    /// The permission set to check against is the first supported one, or the first which supports the action type.
    /// </summary>
    /// <param name="secObj">The secured object.</param>
    /// <param name="actionType">Type of the action to check.</param>
    /// <returns>
    /// 	<c>true</c> if security action type granted for the specified secured object; otherwise, (or if the action is not supported) <c>false</c>.
    /// </returns>
    public static bool IsSecurityActionTypeGranted(
      this ISecuredObject secObj,
      SecurityActionTypes actionType)
    {
      return secObj.IsSecurityActionTypeGranted(string.Empty, actionType);
    }

    /// <summary>
    /// Determines whether a specific action type is allowed for the secured object.
    /// </summary>
    /// <param name="secObj">The secured object.</param>
    /// <param name="PermissionSetName">Name of the permission set to check against.</param>
    /// <param name="actionType">Type of the action to check.</param>
    /// <returns>
    /// 	<c>true</c> if security action type granted for the specified secured object; otherwise, (or if the action is not supported)<c>false</c>.
    /// </returns>
    public static bool IsSecurityActionTypeGranted(
      this ISecuredObject secObj,
      string permissionSetName,
      SecurityActionTypes actionType)
    {
      if (!secObj.IsSecurityActionSupported(permissionSetName, actionType))
        return false;
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      List<string> list = (!string.IsNullOrWhiteSpace(permissionSetName) ? ((IEnumerable<string>) secObj.SupportedPermissionSets).Where<string>((Func<string, bool>) (s => s == permissionSetName)) : (IEnumerable<string>) secObj.SupportedPermissionSets).ToList<string>();
      string str = string.Empty;
      foreach (string key in list)
      {
        foreach (SecurityAction action in (ConfigElementCollection) securityConfig.Permissions[key].Actions)
        {
          if ((action.Type & actionType) > SecurityActionTypes.None)
          {
            str = action.Name;
            permissionSetName = key;
            break;
          }
        }
        if (!string.IsNullOrWhiteSpace(str))
          break;
      }
      if (string.IsNullOrWhiteSpace(str))
        return false;
      return secObj.IsGranted(permissionSetName, str);
    }

    /// <summary>
    /// Determines whether a specific action type is supported by a secured object..
    /// </summary>
    /// <param name="secObj">The secured object.</param>
    /// <param name="permissionSetName">Name of the permission set to check against.</param>
    /// <param name="actionType">Type of the action.</param>
    /// <returns>
    ///     <c>true</c> if security action type supported by the specified secured object; otherwise <c>false</c>.
    /// </returns>
    public static bool IsSecurityActionSupported(
      this ISecuredObject secObj,
      string permissionSetName,
      SecurityActionTypes actionType)
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      bool flag = false;
      foreach (string key in (!string.IsNullOrWhiteSpace(permissionSetName) ? ((IEnumerable<string>) secObj.SupportedPermissionSets).Where<string>((Func<string, bool>) (s => s == permissionSetName)) : (IEnumerable<string>) secObj.SupportedPermissionSets).ToList<string>())
      {
        foreach (SecurityAction action in (ConfigElementCollection) securityConfig.Permissions[key].Actions)
        {
          if ((action.Type & actionType) > SecurityActionTypes.None)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Determines whether a specific action type is supported by a secured object..
    /// </summary>
    /// <param name="secObj">The secured object.</param>
    /// <param name="actionType">Type of the action.</param>
    /// <returns>
    ///     <c>true</c> if security action type supported by the specified secured object; otherwise <c>false</c>.
    /// </returns>
    public static bool IsSecurityActionSupported(
      this ISecuredObject secObj,
      SecurityActionTypes actionType)
    {
      return secObj.IsSecurityActionSupported(string.Empty, actionType);
    }

    /// <summary>
    /// Gets the security action with the specified action name.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actionName">Name of the action.</param>
    internal static SecurityAction GetSecurityAction(
      this ISecuredObject securedObject,
      string permissionSetName,
      string actionName)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission;
      if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSetName, out permission))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidPermissionSet.Arrange((object) permissionSetName));
      if (!((IEnumerable<string>) securedObject.SupportedPermissionSets).Contains<string>(permissionSetName))
        throw new ArgumentException(Res.Get<ErrorMessages>().ObjectDoesNotSupportPermissionSet.Arrange((object) securedObject.GetType().FullName, (object) permissionSetName));
      SecurityAction securityAction;
      if (!permission.Actions.TryGetValue(actionName, out securityAction))
        throw new ArgumentException(Res.Get<ErrorMessages>().InvalidActionForPermissionSet.Arrange((object) actionName, (object) permissionSetName));
      return securityAction;
    }
  }
}
