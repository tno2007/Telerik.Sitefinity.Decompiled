// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecuredObjectExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Provides extension methods for <see cref="!:ISecuredObject" />-s
  /// </summary>
  public static class SecuredObjectExtensions
  {
    /// <summary>Returns active permissions only</summary>
    /// <param name="securedObject">Extended secured object</param>
    /// <returns>Unique set of active permissions</returns>
    /// <remarks>This could be a very slow operation if the secured object has many permissions.</remarks>
    public static IQueryable<Telerik.Sitefinity.Security.Model.Permission> GetActivePermissions(
      this ISecuredObject securedObject)
    {
      if (securedObject == null)
        throw new ArgumentNullException(nameof (securedObject));
      return !securedObject.InheritsPermissions ? securedObject.GetOwnPermissions().AsQueryable<Telerik.Sitefinity.Security.Model.Permission>() : securedObject.GetInheritedPermissions().Union<Telerik.Sitefinity.Security.Model.Permission>(securedObject.GetOwnPermissions(), PermissionComparer.SetNamePrincipalIdComparer).AsQueryable<Telerik.Sitefinity.Security.Model.Permission>();
    }

    /// <summary>
    /// Gets the active permission actions for currently logged in user, as an array of "PermissionSetName.ActionName" strings.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <returns></returns>
    public static string[] GetActivePermissionActionsForCurrentUser(
      this ISecuredObject securedObject)
    {
      List<string> stringList = new List<string>();
      SitefinityIdentity currentUser = ClaimsManager.GetCurrentIdentity();
      if (currentUser.IsUnrestricted)
      {
        foreach (string supportedPermissionSet in securedObject.SupportedPermissionSets)
        {
          foreach (SecurityAction action in (ConfigElementCollection) Config.Get<SecurityConfig>().Permissions[supportedPermissionSet].Actions)
            stringList.Add(supportedPermissionSet + "." + action.Name);
        }
      }
      else
      {
        IEnumerable<Guid> userRoles = currentUser.Roles.Select<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id));
        IQueryable<Telerik.Sitefinity.Security.Model.Permission> activePermissions = securedObject.GetActivePermissions();
        Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>> predicate = (Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == currentUser.UserId || userRoles.Contains<Guid>(p.PrincipalId));
        foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) activePermissions.Where<Telerik.Sitefinity.Security.Model.Permission>(predicate))
        {
          foreach (SecurityAction action in (ConfigElementCollection) Config.Get<SecurityConfig>().Permissions[permission.SetName].Actions)
          {
            if (permission.IsGranted(action.Name))
              stringList.Add(permission.SetName + "." + action.Name);
          }
        }
      }
      return stringList.ToArray();
    }

    /// <summary>
    /// Gets the inherited permissions for specific secured object.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <returns>Set of inherited permissions.</returns>
    public static IEnumerable<Telerik.Sitefinity.Security.Model.Permission> GetInheritedPermissions(
      this ISecuredObject securedObject)
    {
      return securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId != securedObject.Id));
    }

    /// <summary>Gets own permissions for specific secured object.</summary>
    /// <param name="securedObject">The secured object.</param>
    /// <returns>Set of own permissions.</returns>
    public static IEnumerable<Telerik.Sitefinity.Security.Model.Permission> GetOwnPermissions(
      this ISecuredObject securedObject)
    {
      return securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId == securedObject.Id));
    }

    /// <summary>
    /// Create a copy of the secured object that is not persisted or tracked
    /// </summary>
    /// <param name="source">Secured object instance to get a proxy for</param>
    /// <returns>In-memory copy of the original secured object</returns>
    public static ISecuredObject CreateProxy(this ISecuredObject source) => source != null ? (ISecuredObject) new SecuredProxy(source) : throw new ArgumentNullException(nameof (source));

    /// <summary>
    /// Throws a security exception if the secured object is not granted the demanded permission
    /// </summary>
    /// <param name="securedObject">Secured object to check against</param>
    /// <param name="permissionSetName">Name of the permission set</param>
    /// <param name="actions">List of security actions to demand</param>
    public static void Demand(
      this ISecuredObject securedObject,
      string permissionSetName,
      params string[] actions)
    {
      if (securedObject == null)
        throw new ArgumentNullException(nameof (securedObject));
      if (string.IsNullOrEmpty(permissionSetName))
        throw new ArgumentNullException(nameof (permissionSetName));
      int actions1 = actions != null && actions.Length != 0 ? PermissionAttribute.GetActionValue(permissionSetName, actions) : throw new ArgumentNullException(nameof (actions), "Value cannot be null or empty array.");
      securedObject.Demand(permissionSetName, actions1);
    }

    /// <summary>
    /// See if a generic security action is granted or not. Will work only on properly configured permission sets
    /// </summary>
    /// <param name="securedObject">Secured object to test.</param>
    /// <param name="actionType">Generic security action to test.</param>
    /// <param name="permissionsSetName">Generic security action to test.</param>
    /// <returns>True if the action is granted, false otherwise.</returns>
    public static bool IsGranted(
      this ISecuredObject securedObject,
      SecurityActionTypes actionType,
      string permissionsSetName = null)
    {
      return securedObject.IsGranted(actionType, permissionsSetName, (List<string>) null);
    }

    /// <summary>
    /// See if a generic security action is granted or not. Will work only on properly configured permission sets
    /// </summary>
    /// <param name="securedObject">Secured object to test.</param>
    /// <param name="actionType">Generic security action to test.</param>
    /// <param name="permissionsSetName">Generic security action to test.</param>
    /// <param name="excludedPermissionsSetNames">Exluded generic security actions.</param>
    /// <returns>True if the action is granted, false otherwise.</returns>
    public static bool IsGranted(
      this ISecuredObject securedObject,
      SecurityActionTypes actionType,
      string permissionsSetName,
      List<string> excludedPermissionsSetNames)
    {
      if (securedObject == null)
        throw new ArgumentNullException(nameof (securedObject));
      bool flag = true;
      if (actionType != SecurityActionTypes.None)
      {
        List<string> stringList1 = new List<string>();
        if (!string.IsNullOrWhiteSpace(permissionsSetName) && ((IEnumerable<string>) securedObject.SupportedPermissionSets).Contains<string>(permissionsSetName))
          stringList1.Add(permissionsSetName);
        else
          stringList1.AddRange((IEnumerable<string>) securedObject.SupportedPermissionSets);
        if (excludedPermissionsSetNames != null)
          stringList1.RemoveAll((Predicate<string>) (setName => excludedPermissionsSetNames.Contains(setName)));
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions = Config.Get<SecurityConfig>().Permissions;
        foreach (string key in stringList1)
        {
          ConfigElementDictionary<string, SecurityAction> actions = permissions[key].Actions;
          List<string> stringList2 = new List<string>();
          foreach (SecurityAction securityAction in (ConfigElementCollection) actions)
          {
            if ((securityAction.Type & actionType) > SecurityActionTypes.None)
              stringList2.Add(securityAction.Name);
          }
          dictionary.Add(key, stringList2);
        }
        foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
        {
          flag = flag && securedObject.IsGranted(keyValuePair.Key, keyValuePair.Value.ToArray());
          if (!flag)
            break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Demand a generic security action. Will work only on properly configured permission sets
    /// </summary>
    /// <param name="securedObject">Secured object to demand for</param>
    /// <param name="actionType">Generic type of the action to demand</param>
    public static void Demand(
      this ISecuredObject securedObject,
      SecurityActionTypes actionType,
      string permissionsSetName = null)
    {
      if (!securedObject.IsGranted(actionType, permissionsSetName))
        throw new SecurityDemandFailException(securedObject.GetType().ToString() + ": " + (object) actionType);
    }

    /// <summary>Checks whether a permission set is supported</summary>
    /// <param name="securedObject">Secured object that is tested whether it supports <paramref name="permssionSetName" /></param>
    /// <param name="permissionSetName">Name of the permission set that is checked for</param>
    /// <returns>True if supported, false otherwise.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// When either <paramref name="securedObject" /> or <paramref name="permissionSetName" /> is <c>null</c>.
    /// </exception>
    public static bool IsPermissionSetSupported(
      this ISecuredObject securedObject,
      string permissionSetName)
    {
      if (securedObject == null)
        throw new ArgumentNullException(nameof (securedObject));
      if (permissionSetName == null)
        throw new ArgumentNullException(nameof (permissionSetName));
      bool flag = false;
      for (int index = 0; index < securedObject.SupportedPermissionSets.Length; ++index)
      {
        if (string.Equals(securedObject.SupportedPermissionSets[index], permissionSetName, StringComparison.OrdinalIgnoreCase))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Determines whether the specified secured object is editable.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="provider">The provider related to the object.</param>
    /// <returns>
    /// 	<c>true</c> if the specified secured object is editable; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEditable(this ISecuredObject securedObject, DataProviderBase provider)
    {
      bool flag = true;
      foreach (TransactionPermissionAttribute permissionAttribute in provider.GetPermissionActionsForTransaction(securedObject.GetType(), SecurityConstants.TransactionActionType.Updated))
      {
        if (!securedObject.IsGranted(permissionAttribute.PermissionSetName, permissionAttribute.Value))
          flag = false;
      }
      return flag;
    }

    public static bool IsEditable(
      this ISecuredObject securedObject,
      string permissionSetName,
      string modifyActionName)
    {
      bool flag = true;
      if (!securedObject.IsGranted(permissionSetName, modifyActionName))
        flag = false;
      return flag;
    }

    /// <summary>
    /// Copy security settings from <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="destination">Secured object to </param>
    /// <param name="source">Secured object to be copied from</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// If either <paramref name="destination" /> or <paramref name="source" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// When the data provider cannot be inferred from <paramref name="destination" />.
    /// </exception>
    public static void CopySecurityFrom(this ISecuredObject destination, ISecuredObject source) => destination.CopySecurityFrom(source, (IDataProviderBase) null, (IDataProviderBase) null);

    /// <summary>
    /// Copy security settings from <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="destination">Secured object to </param>
    /// <param name="source">Secured object to be copied from</param>
    /// <param name="destinationProvider">Data provider to manage <paramref name="destination" />. Can be null.</param>
    /// <param name="sourceProvider">Data provider to manage <paramref name="source" />. Can be null.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// If either <paramref name="destination" /> or <paramref name="source" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// When <paramref name="destinationProvider" /> is <c>null</c> and the data provider cannot be inferred from <paramref name="destination" />.
    /// </exception>
    public static void CopySecurityFrom(
      this ISecuredObject destination,
      ISecuredObject source,
      IDataProviderBase destinationProvider,
      IDataProviderBase sourceProvider,
      bool copyInheritanceMap = true)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destinationProvider == null)
      {
        if (!(destination is IDataItem dataItem) || !(dataItem.Provider is DataProviderBase))
          throw new ArgumentException("Cannot infer destinationProvider.");
        destinationProvider = (IDataProviderBase) dataItem.Provider;
      }
      if (sourceProvider == null)
      {
        if (!(source is IDataItem dataItem) || !(dataItem.Provider is DataProviderBase))
          throw new ArgumentException("Cannot infer sourceProvider.");
        sourceProvider = (IDataProviderBase) dataItem.Provider;
      }
      destination.CanInheritPermissions = source.CanInheritPermissions;
      destination.InheritsPermissions = source.InheritsPermissions;
      if (destination.SupportedPermissionSets != source.SupportedPermissionSets)
      {
        destination.SupportedPermissionSets = new string[source.SupportedPermissionSets.Length];
        Array.Copy((Array) source.SupportedPermissionSets, (Array) destination.SupportedPermissionSets, source.SupportedPermissionSets.Length);
      }
      PermissionsInheritanceMap[] array = destinationProvider.GetPermissionChildren(destination.Id).ToArray<PermissionsInheritanceMap>();
      for (int index = array.Length - 1; index >= 0; --index)
        destinationProvider.DeletePermissionsInheritanceMap(array[index]);
      if (copyInheritanceMap)
      {
        foreach (PermissionsInheritanceMap permissionChild in (IEnumerable<PermissionsInheritanceMap>) sourceProvider.GetPermissionChildren(source.Id))
          destinationProvider.CreatePermissionsInheritanceMap(SecuredObjectExtensions.GetIdExcept(permissionChild.ObjectId, source.Id, destination.Id), SecuredObjectExtensions.GetIdExcept(permissionChild.ChildObjectId, source.Id, destination.Id), permissionChild.ChildObjectTypeName);
      }
      Telerik.Sitefinity.Security.Model.Permission[] permsToDeleteFromTheDestination = destination.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId == destination.Id)).ToArray<Telerik.Sitefinity.Security.Model.Permission>();
      DataProviderBase dataProviderBase = destinationProvider as DataProviderBase;
      for (int i = 0; i < permsToDeleteFromTheDestination.Length; i++)
      {
        if (source.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (sourcePermission => sourcePermission.ObjectId == source.Id && sourcePermission.SetName == permsToDeleteFromTheDestination[i].SetName && sourcePermission.PrincipalId == permsToDeleteFromTheDestination[i].PrincipalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() == null)
        {
          destination.Permissions.Remove(permsToDeleteFromTheDestination[i]);
          dataProviderBase?.DeletePermission(permsToDeleteFromTheDestination[i]);
        }
      }
      foreach (Telerik.Sitefinity.Security.Model.Permission permission1 in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) source.Permissions)
      {
        Telerik.Sitefinity.Security.Model.Permission srcPermissions = permission1;
        if (srcPermissions.ObjectId == source.Id)
        {
          Telerik.Sitefinity.Security.Model.Permission permission2 = destination.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (destPerm => destPerm.ObjectId == destination.Id && destPerm.SetName == srcPermissions.SetName && destPerm.PrincipalId == srcPermissions.PrincipalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>();
          if (permission2 == null && dataProviderBase != null)
          {
            Telerik.Sitefinity.Security.Model.Permission permission3 = dataProviderBase.CreatePermission(srcPermissions.SetName, destination.Id, srcPermissions.PrincipalId);
            permission3.Deny = srcPermissions.Deny;
            permission3.Grant = srcPermissions.Grant;
            destination.Permissions.Add(permission3);
          }
          else
          {
            permission2.Grant = srcPermissions.Grant;
            permission2.Deny = srcPermissions.Deny;
          }
        }
        else if (destination.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (destPerm => destPerm.ObjectId == srcPermissions.ObjectId && destPerm.SetName == srcPermissions.SetName && destPerm.PrincipalId == srcPermissions.PrincipalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() == null)
          destination.Permissions.Add(srcPermissions);
      }
    }

    /// <summary>
    /// Determines whether permission changes on a specific secured object are enabled in the currently used license.
    /// This is overridden if the secured object implements IDataItem, and SuppressSecurityChecks is set to true in its associated provider instance.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <returns>True if permission changes on a specific secured object are enabled in the currently used license; False otherwise.</returns>
    public static bool ArePermissionChangesAllowedByLicense(this ISecuredObject securedObject)
    {
      bool flag = true;
      if (securedObject.GetType().GetInterface(typeof (IDataItem).FullName) != (Type) null && ((IDataItem) securedObject).Provider != null && ((IDataItem) securedObject).Provider is DataProviderBase && ((DataProviderBase) ((IDataItem) securedObject).Provider).SuppressSecurityChecks)
        return true;
      if (securedObject is ControlData && !LicenseState.Current.LicenseInfo.IsPageControlsPermissionsEnabled)
        flag = false;
      return flag;
    }

    /// <summary>
    /// Converts the permissions which are attached to the secured object, with IDs different from the object Id, to permissions with current object id.
    /// This is needed when deserializing controls (which do not inherit permissions), and their deserialized permissions are with the old Id.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="managerInstance">Instance of IManager, for creating new permissions.</param>
    public static void ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(
      this ISecuredObject securedObject,
      IManager managerInstance)
    {
      List<Telerik.Sitefinity.Security.Model.Permission> permissionList1 = new List<Telerik.Sitefinity.Security.Model.Permission>();
      List<Telerik.Sitefinity.Security.Model.Permission> permissionList2 = new List<Telerik.Sitefinity.Security.Model.Permission>();
      foreach (Telerik.Sitefinity.Security.Model.Permission permission1 in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) securedObject.Permissions)
      {
        Telerik.Sitefinity.Security.Model.Permission perm = permission1;
        if (perm.ObjectId != securedObject.Id && securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId == securedObject.Id && p.PrincipalId == perm.PrincipalId && p.SetName == perm.SetName)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() == null)
        {
          Telerik.Sitefinity.Security.Model.Permission permission2 = managerInstance.CreatePermission(perm.SetName, securedObject.Id, perm.PrincipalId);
          permission2.Grant = perm.Grant;
          permission2.Deny = perm.Deny;
          permissionList2.Add(permission2);
          permissionList1.Add(perm);
        }
      }
      foreach (Telerik.Sitefinity.Security.Model.Permission permission in permissionList2)
        securedObject.Permissions.Add(permission);
      Telerik.Sitefinity.Security.Model.Permission[] array = permissionList1.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
        securedObject.Permissions.Remove(array[index]);
    }

    /// <summary>
    /// The method copies the security permissions from a source to destination provider of the same static or dynamic type.
    /// </summary>
    /// <param name="managerType">The type of the manager for the content whose permissions are being copied.</param>
    /// <param name="moduleName">Name of the module for a dynamic type.</param>
    /// <param name="destinationProviderName">Name of the destination provider.</param>
    /// <param name="sourceProviderName">Name of the source provider. If not set the default provider for the content type is used as a source.</param>
    [Obsolete]
    public static void CopyProviderPermissions(
      Type managerType,
      string moduleName,
      string destinationProviderName,
      string sourceProviderName = "OpenAccess")
    {
      if (managerType == (Type) null)
        throw new ArgumentNullException(nameof (managerType));
      if (typeof (DynamicModuleManager).IsAssignableFrom(managerType))
      {
        ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
        DynamicModuleDataProvider moduleDataProvider1 = ManagerBase<DynamicModuleDataProvider>.StaticProvidersCollection.FirstOrDefault<DynamicModuleDataProvider>((Func<DynamicModuleDataProvider, bool>) (x => x.Name.Contains(sourceProviderName)));
        DynamicModuleDataProvider moduleDataProvider2 = ManagerBase<DynamicModuleDataProvider>.StaticProvidersCollection.FirstOrDefault<DynamicModuleDataProvider>((Func<DynamicModuleDataProvider, bool>) (x => x.Name.Contains(destinationProviderName)));
        DynamicModule module = manager.Provider.GetDynamicModules().FirstOrDefault<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Name == moduleName));
        if (module == null)
          return;
        ISecuredObject securedObject1 = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) module, moduleDataProvider1.Name);
        DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) module, moduleDataProvider2.Name).CopySecurityFrom(securedObject1, (IDataProviderBase) manager.Provider, (IDataProviderBase) manager.Provider);
        IQueryable<DynamicModuleType> dynamicModuleTypes = manager.Provider.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == module.Id);
        foreach (DynamicModuleType mainSecuredObject in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
        {
          ISecuredObject securedObject2 = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) mainSecuredObject, moduleDataProvider1.Name);
          DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) mainSecuredObject, moduleDataProvider2.Name).CopySecurityFrom(securedObject2, (IDataProviderBase) manager.Provider, (IDataProviderBase) manager.Provider);
        }
        manager.SaveChanges();
      }
      else
      {
        IManager manager1 = ManagerBase.GetManager(managerType, destinationProviderName);
        ISecuredObject securityRoot1 = manager1.Provider.GetSecurityRoot(true);
        DataProviderBase dataProviderBase = manager1.StaticProviders.FirstOrDefault<DataProviderBase>((Func<DataProviderBase, bool>) (x => x.Name.Contains(destinationProviderName))) ?? manager1.StaticProviders.First<DataProviderBase>();
        IManager manager2 = ManagerBase.GetManager(managerType, dataProviderBase.Name);
        ISecuredObject securityRoot2 = manager2.Provider.SecurityRoot;
        DataProviderBase provider1 = manager1.Provider;
        DataProviderBase provider2 = manager2.Provider;
        securityRoot1.CopySecurityFrom(securityRoot2, (IDataProviderBase) provider1, (IDataProviderBase) provider2);
        manager1.SaveChanges();
      }
    }

    /// <summary>
    /// Gets the list of secured object permissions to assign to its children.
    /// </summary>
    /// <param name="securedObject">The parent secured object.</param>
    /// <param name="inheritanceAction">The inheritance action.</param>
    /// <returns></returns>
    internal static IEnumerable<Telerik.Sitefinity.Security.Model.Permission> GetPermissionsToAssignToChildren(
      this ISecuredObject securedObject,
      InheritanceAction inheritanceAction)
    {
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> assignToChildren;
      switch (inheritanceAction)
      {
        case InheritanceAction.Break:
          assignToChildren = securedObject.GetOwnPermissions();
          break;
        case InheritanceAction.Restore:
          assignToChildren = securedObject.GetInheritedPermissions();
          break;
        case InheritanceAction.SyncWithParent:
          assignToChildren = securedObject.GetInheritedPermissions();
          break;
        default:
          assignToChildren = (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) new List<Telerik.Sitefinity.Security.Model.Permission>();
          break;
      }
      return assignToChildren;
    }

    /// <summary>
    /// Get <paramref name="value" /> unless it is equal to <paramref name="exception" />, in which case get <paramref name="replacement" /> instead
    /// </summary>
    /// <param name="val">Test value</param>
    /// <param name="exception">Exceptional value</param>
    /// <param name="replacement">Replacement value</param>
    /// <returns><paramref name="value" /> unless it is equal to <paramref name="exception" />, in which case return <paramref name="replacement" /> instead</returns>
    private static Guid GetIdExcept(Guid val, Guid exception, Guid replacement)
    {
      val = val != exception ? val : replacement;
      return val;
    }
  }
}
