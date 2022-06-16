// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.OpenAccessDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// This class is a decorator for the DataProviderBase and is used when the concrete implementation of the data provider
  /// is based on OpenAccess.
  /// </summary>
  /// <remarks>
  /// Data providers implementing IOpenAccessDataProvider interface are considered to be using OpenAccess as the persistence layer.
  /// </remarks>
  public class OpenAccessDecorator : 
    IDataProviderDecorator,
    ICloneable,
    IDataProviderEventsCall,
    IExecutionStateDecorator
  {
    private ObjectContainer objectConainer;
    private string connectionName;
    private static OpenAccessDecorator.IGuidGenerator currentGuidGenerator;
    internal const string IsolationLevelKey = "IsolationLevel";

    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    public DataProviderBase DataProvider { get; set; }

    private SitefinityOAContext Context => (SitefinityOAContext) this.DataProvider.GetTransaction();

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    public bool Suspended => ((IOpenAccessDataProvider) this.DataProvider).Context == null;

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      string appName = this.DataProvider.ApplicationName;
      return this.Context.GetAll<Permission>().Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ApplicationName == appName && p.SetName == permissionSet && p.ObjectId == objectId && p.PrincipalId == principalId)).FirstOrDefault<Permission>();
    }

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionId">The permission pageId.</param>
    /// <returns></returns>
    public Permission GetPermission(Guid permissionId)
    {
      string appName = this.DataProvider.ApplicationName;
      return this.Context.GetAll<Permission>().Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ApplicationName == appName && p.Id == permissionId)).FirstOrDefault<Permission>();
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <returns></returns>
    public virtual IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Permission
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<TPermission>(this.DataProvider).Where<TPermission>((Expression<Func<TPermission, bool>>) (p => p.ApplicationName == appName));
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <param name="actualType">The actual type.</param>
    /// <returns></returns>
    public virtual IQueryable<Permission> GetPermissions(Type actualType)
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<Permission>(actualType, this.DataProvider).Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ApplicationName == appName));
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public IQueryable<Permission> GetPermissions()
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<Permission>(this.DataProvider).Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ApplicationName == appName));
    }

    /// <summary>Creates the permission.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      if (string.IsNullOrEmpty(permissionSet))
        throw new ArgumentNullException(nameof (permissionSet));
      if (objectId == Guid.Empty)
        throw new ArgumentNullException(nameof (objectId));
      if (principalId == Guid.Empty)
        throw new ArgumentNullException(nameof (principalId));
      Permission entity = new Permission();
      entity.Id = this.GetNewGuid();
      entity.ApplicationName = this.DataProvider.ApplicationName;
      entity.SetName = permissionSet;
      entity.ObjectId = objectId;
      entity.PrincipalId = principalId;
      this.Context.Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Deletes the permissions relevant to a secured object.
    /// If the object inherits permissions, only the ones which are directly attached to the object, not inherited ones.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    public void DeletePermissions(object securedObject)
    {
      ISecuredObject isecuredObject = securedObject as ISecuredObject;
      if (isecuredObject == null)
        return;
      if (isecuredObject is SecuredProxy)
        isecuredObject = this.GetSecuredObjectFromProxy((SecuredProxy) isecuredObject);
      Permission[] array = isecuredObject.Permissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId == isecuredObject.Id)).ToArray<Permission>();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        isecuredObject.Permissions.Remove(array[index]);
        this.DeletePermission(array[index]);
      }
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public void DeletePermission(Permission permission) => this.Context.Delete((object) permission);

    public virtual void CreatePermissionInheritanceAssociation(
      ISecuredObject parent,
      ISecuredObject child)
    {
      if (parent == null)
        return;
      if (parent is SecuredProxy)
        parent = this.GetSecuredObjectFromProxy((SecuredProxy) parent);
      this.CreatePermissionInheritanceAssociation(parent, parent.Permissions, child);
    }

    /// <summary>
    /// Executes permission inheritance between a parent and a child secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="parentPermissions">List of permissions to be used as parent permissions.</param>
    /// <param name="child">The child.</param>
    /// <remarks>
    /// This method is only added for the <see cref="T:Telerik.Sitefinity.Data.Decorators.OpenAccessDecorator" /> and not on the <see cref="T:Telerik.Sitefinity.Data.Decorators.IDataProviderDecorator" /> level because we want to avoid introducing breaking changes.
    /// This is not ideal because it leads to greater coupling between the <see cref="T:Telerik.Sitefinity.Data.Decorators.OpenAccessDecorator" /> and all the classes that use this method. If this becomes a problem,
    /// then the method should be moved on the <see cref="T:Telerik.Sitefinity.Data.Decorators.IDataProviderDecorator" /> level and a breaking change should be included in the release notes.
    /// </remarks>
    internal virtual void CreatePermissionInheritanceAssociation(
      ISecuredObject parent,
      IList<Permission> parentPermissions,
      ISecuredObject child)
    {
      if (parentPermissions == null)
        throw new ArgumentException(nameof (parentPermissions));
      if (parent == null || child == null)
        return;
      this.EnsureInheritanceMapIsCreated(parent, child);
      List<Permission> permissionsToCopy = parent.InheritsPermissions ? new List<Permission>(parentPermissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId != parent.Id))) : new List<Permission>(parentPermissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId == parent.Id)));
      this.ApplyPermissions(child, permissionsToCopy);
      this.ApplyPermissionsToInheritors(child, permissionsToCopy);
    }

    private void ApplyPermissions(ISecuredObject secObj, List<Permission> permissionsToCopy)
    {
      this.RemovePermissionsWithDifferentObjectId(secObj, secObj.Id);
      this.CopyPermissions(secObj, permissionsToCopy);
    }

    private void ApplyPermissionsToInheritors(
      ISecuredObject parent,
      List<Permission> permissionsToCopy)
    {
      foreach (ISecuredObject permissionsInheritor in this.DataProvider.GetPermissionsInheritors(parent, true, (Type) null))
      {
        this.RemovePermissionsWithDifferentObjectId(permissionsInheritor, parent.Id);
        this.CopyPermissions(permissionsInheritor, permissionsToCopy);
      }
    }

    private void EnsureInheritanceMapIsCreated(ISecuredObject parent, ISecuredObject child)
    {
      if (this.GetInheritanceMaps().Any<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (p => p.ObjectId == parent.Id && p.ChildObjectId == child.Id)))
        return;
      this.CreatePermissionsInheritanceMap(parent.Id, child.Id, this.ConvertClrTypeVoaClass(child.GetType()));
    }

    internal void CopyPermissions(ISecuredObject secObj, List<Permission> permissionsToCopy)
    {
      foreach (Permission p in permissionsToCopy)
        this.EnsurePermissionForObject(secObj, p);
    }

    private void EnsurePermissionForObject(ISecuredObject secObj, Permission p)
    {
      if (secObj.Permissions.Contains<Permission>(p, PermissionComparer.SetNamePrincipalIdObjectIdEqualityComparer) || !((IEnumerable<string>) secObj.SupportedPermissionSets).Contains<string>(p.SetName))
        return;
      secObj.Permissions.Add(p);
    }

    private void RemovePermissionsWithDifferentObjectId(ISecuredObject securedObject, Guid objectId)
    {
      for (int index = securedObject.Permissions.Count - 1; index >= 0; --index)
      {
        Permission permission = securedObject.Permissions[index];
        if (permission.ObjectId != objectId)
          securedObject.Permissions.Remove(permission);
      }
    }

    /// <summary>
    /// Deletes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="child">The child.</param>
    public void DeletePermissionsInheritanceAssociation(ISecuredObject parent, ISecuredObject child)
    {
      if (parent == null || child == null)
        return;
      this.DeletePermissionsInheritanceAssociation(parent.Id, child.Id);
    }

    /// <summary>
    /// Deletes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parentId">The Id of the parent.</param>
    /// <param name="childId">The Id of the child.</param>
    /// <remarks>
    /// This method is only added for the <see cref="T:Telerik.Sitefinity.Data.Decorators.OpenAccessDecorator" /> and not on the <see cref="T:Telerik.Sitefinity.Data.Decorators.IDataProviderDecorator" /> level because we want to avoid introducing breaking changes.
    /// This is not ideal because it leads to greater coupling between the <see cref="T:Telerik.Sitefinity.Data.Decorators.OpenAccessDecorator" /> and all the classes that use this method. If this becomes a problem,
    /// then the method should be moved on the <see cref="T:Telerik.Sitefinity.Data.Decorators.IDataProviderDecorator" /> level and a breaking change should be included in the release notes.
    /// </remarks>
    internal void DeletePermissionsInheritanceAssociation(Guid parentId, Guid childId)
    {
      List<PermissionsInheritanceMap> list = this.GetPermissionChildren(parentId).Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (c => c.ChildObjectId == childId)).ToList<PermissionsInheritanceMap>();
      list.AddRange(this.Context.GetDirtyObjects<PermissionsInheritanceMap>().Where<PermissionsInheritanceMap>((Func<PermissionsInheritanceMap, bool>) (m => m.ObjectId == parentId && m.ChildObjectId == childId && this.GetDirtyItemStatus((object) m) == SecurityConstants.TransactionActionType.New)));
      for (int index = 0; index < list.Count<PermissionsInheritanceMap>(); ++index)
        this.DeletePermissionsInheritanceMap(list[index]);
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object to add permission to.</param>
    /// <param name="permission">The permission to add.</param>
    /// <param name="tranName">This parameter is obsolete - Name of the transaction to use (to apply the permission to the rest of the object types down the inheritance tree)</param>
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string tranName)
    {
      if (!this.DataProvider.SuppressSecurityChecks && !securedObject.ArePermissionChangesAllowedByLicense())
        throw new NotSupportedException(Telerik.Sitefinity.Localization.Res.Get<SecurityResources>().PermissionSettingForThisObjectIsDisabledOnYourLicense);
      this.AddPermissionToObject(securedObject, permission);
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object to add permission to.</param>
    /// <param name="managerInstance">This parameter is obsolete - Instance of the secured object's related manager.</param>
    /// <param name="permission">The permission to add.</param>
    /// <param name="tranName">This parameter is obsolete - Name of the transaction to use (to apply the permission to the rest of the object types down the inheritance tree)</param>
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string tranName)
    {
      if (!this.DataProvider.SuppressSecurityChecks && !securedObject.ArePermissionChangesAllowedByLicense())
        throw new NotSupportedException(Telerik.Sitefinity.Localization.Res.Get<SecurityResources>().PermissionSettingForThisObjectIsDisabledOnYourLicense);
      this.AddPermissionToObject(securedObject, permission);
    }

    /// <summary>
    /// Adds a permission to a secured object and handles inheritance throughout the tree (adds this permission to its inheritors too).
    /// </summary>
    /// <param name="securedObject">The secured object to add permission to.</param>
    /// <param name="permission">The permission to add.</param>
    private void AddPermissionToObject(ISecuredObject securedObject, Permission permission)
    {
      if (!this.DataProvider.SuppressSecurityChecks && !securedObject.ArePermissionChangesAllowedByLicense())
        throw new NotSupportedException(Telerik.Sitefinity.Localization.Res.Get<SecurityResources>().PermissionSettingForThisObjectIsDisabledOnYourLicense);
      if (securedObject is SecuredProxy)
        securedObject = this.GetSecuredObjectFromProxy((SecuredProxy) securedObject);
      if (!securedObject.Permissions.Contains(permission))
        securedObject.Permissions.Add(permission);
      foreach (ISecuredObject permissionsInheritor in this.DataProvider.GetPermissionsInheritors(securedObject, false, (Type) null))
        this.EnsurePermissionForObject((ISecuredObject) this.DataProvider.GetItem(permissionsInheritor.GetType(), permissionsInheritor.Id), permission);
    }

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public SecurityRoot GetSecurityRoot()
    {
      ISecuredObject dataProvider = (ISecuredObject) this.DataProvider;
      return dataProvider != null && dataProvider.Id != Guid.Empty ? this.Context.GetItemById<SecurityRoot>(dataProvider.Id.ToString()) : (SecurityRoot) null;
    }

    private ISecuredObject GetSecuredObjectFromProxy(SecuredProxy proxy) => (ISecuredObject) this.Context.GetItemById(proxy.SecuredObjectType, proxy.Id.ToString());

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public SecurityRoot GetSecurityRoot(bool create) => this.GetSecurityRoot(create, (IDictionary<string, string>) new Dictionary<string, string>(), new string[0]);

    public SecurityRoot GetSecurityRoot(
      bool create,
      IDictionary<string, string> permissionsetObjectTitleResKeys,
      params string[] permissionSets)
    {
      if (this.DataProvider == null)
        return (SecurityRoot) null;
      string appName = this.DataProvider.ApplicationName;
      string key = this.DataProvider.RootKey + this.DataProvider.Name;
      SecurityRoot securityRoot = this.Context.GetAll<SecurityRoot>().Where<SecurityRoot>((Expression<Func<SecurityRoot, bool>>) (r => r.ApplicationName == appName && r.Key == key)).FirstOrDefault<SecurityRoot>();
      if (securityRoot == null & create)
      {
        if (((IEnumerable<string>) permissionSets).Count<string>() == 0)
          securityRoot = new SecurityRoot(appName, this.GetNewGuid())
          {
            Key = key
          };
        else
          securityRoot = new SecurityRoot(appName, this.GetNewGuid(), permissionSets, permissionsetObjectTitleResKeys)
          {
            Key = key
          };
        this.Context.Add((object) securityRoot);
        this.DataProvider.SupportedPermissionSets = permissionSets;
        bool suppressSecurityChecks = this.DataProvider.SuppressSecurityChecks;
        this.DataProvider.SuppressSecurityChecks = true;
        this.DataProvider.SetRootPermissions(securityRoot);
        this.DataProvider.SuppressSecurityChecks = suppressSecurityChecks;
        try
        {
          this.CommitTransaction();
        }
        catch (DuplicateKeyException ex)
        {
          securityRoot = this.Context.GetAll<SecurityRoot>().Where<SecurityRoot>((Expression<Func<SecurityRoot, bool>>) (r => r.ApplicationName == appName && r.Key == key)).FirstOrDefault<SecurityRoot>();
        }
      }
      return securityRoot;
    }

    /// <inheritdoc />
    public IQueryable<PermissionsInheritanceMap> GetPermissionChildren(
      Guid parentId)
    {
      return this.Context.GetAll<PermissionsInheritanceMap>().Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (p => p.ObjectId == parentId));
    }

    /// <summary>
    /// Converts a VOA (shorter OpenAccess string representation of a type) to a CLR type
    /// </summary>
    /// <param name="voaClassName">Name of the voa class.</param>
    /// <returns>The resolved CLR type</returns>
    public Type ConvertVoaClassToClrType(string voaClassName)
    {
      IOpenAccessDataProvider dataProvider = (IOpenAccessDataProvider) this.DataProvider;
      int voaClassId = -1;
      if (int.TryParse(voaClassName, out voaClassId) && voaClassId > -1)
      {
        MetaPersistentType metaPersistentType = dataProvider.GetContext().Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt =>
        {
          int? classId = pt.ClassId;
          int num = voaClassId;
          return classId.GetValueOrDefault() == num & classId.HasValue;
        }));
        if (metaPersistentType != null)
          return TypeResolutionService.ResolveType(metaPersistentType.FullName, false, false);
      }
      return (Type) null;
    }

    /// <summary>
    /// Converts  a CLR type to a VOA string (shorter OpenAccess string representation of a type)
    /// </summary>
    /// <param name="ClrType">The CLR type.</param>
    /// <returns>Name of the voa class.</returns>
    public string ConvertClrTypeVoaClass(Type ClrType) => this.Context.PersistentMetaData.GetPersistentTypeDescriptor(ClrType).ClassId.ToString();

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => this.Context.GetAll<PermissionsInheritanceMap>();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      if (this.Context.Scope.GetObjectId((object) permissionsInheritanceMap) == null)
        return;
      this.Context.Remove((object) permissionsInheritanceMap);
    }

    /// <inheritdoc />
    public PermissionsInheritanceMap CreatePermissionsInheritanceMap(
      Guid objectId,
      Guid childObjectId,
      string childObjectTypeName)
    {
      PermissionsInheritanceMap entity = new PermissionsInheritanceMap(objectId, childObjectId, childObjectTypeName);
      this.Context.Add((object) entity);
      return entity;
    }

    internal void UpdateInternalDirtyItemsCache(SitefinityOAContext context)
    {
      if (this.DataProvider.SuppressNotifications || !Bootstrapper.IsSystemInitialized)
        return;
      IList dirtyItems = context.GetDirtyItems();
      if (dirtyItems.Count <= 0)
        return;
      if (context.OpenAccessConnection.Replication == OpenAccessConnection.ReplicationMode.Master)
        ReplicationMessageSynchronizer.RegisterDatabaseUpdate(context.OpenAccessConnection.Name);
      List<CacheDependencyKey> collection = new List<CacheDependencyKey>();
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        if (obj is ICacheDependable cacheDependable)
        {
          IList<CacheDependencyKey> dependentObjects = cacheDependable.GetKeysOfDependentObjects();
          collection.AddRange((IEnumerable<CacheDependencyKey>) dependentObjects);
        }
        else
        {
          string key;
          if (CacheDependencyHandler.TryGetWellKnownItemKey(obj, out key))
            collection.Add(new CacheDependencyKey()
            {
              Key = key,
              Type = obj.GetType()
            });
        }
      }
      foreach (Guid guid in dirtyItems.OfType<PageNode>().Select<PageNode, Guid>((Func<PageNode, Guid>) (p => p.RootNodeId)).Distinct<Guid>())
        collection.Add(new CacheDependencyKey()
        {
          Type = typeof (ISiteMapRootCacheDependency),
          Key = guid.ToString()
        });
      this.InvalidatedItems.AddRange((IEnumerable<CacheDependencyKey>) collection);
    }

    internal List<CacheDependencyKey> InvalidatedItems
    {
      get
      {
        if (SystemManager.CurrentHttpContext == null)
          return new List<CacheDependencyKey>();
        string key = this.Context.GetHashCode().ToString() + "_InvalidatedObjects";
        if (!(SystemManager.CurrentHttpContext.Items[(object) key] is List<CacheDependencyKey> invalidatedItems))
        {
          invalidatedItems = new List<CacheDependencyKey>();
          SystemManager.CurrentHttpContext.Items.Add((object) key, (object) invalidatedItems);
        }
        return invalidatedItems;
      }
    }

    internal void UpdateInvalidatedModules(SitefinityOAContext context)
    {
      ResetModelReason resetModelReason1;
      if (!(this.DataProvider is OpenAccessMetaDataProvider dataProvider) || !dataProvider.EnsureSchemaChanges(context, out resetModelReason1))
        return;
      ResetModelReason resetModelReason2 = this.CurrentResetModelReason;
      if (resetModelReason2 == null)
        this.CurrentResetModelReason = resetModelReason1;
      else
        resetModelReason2.Merge(resetModelReason1);
    }

    internal void EnsureModelReset()
    {
      ResetModelReason currentReason = this.CurrentResetModelReason;
      if (currentReason == null)
        return;
      if (string.IsNullOrEmpty(this.DataProvider.TransactionName) || !TransactionManager.TryAddPostCommitAction(this.DataProvider.TransactionName, (System.Action) (() => OpenAccessConnection.ResetModel((OperationReason) currentReason))))
        OpenAccessConnection.ResetModel((OperationReason) currentReason);
      this.CurrentResetModelReason = (ResetModelReason) null;
    }

    internal ResetModelReason CurrentResetModelReason
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return (ResetModelReason) null;
        string key = this.Context.GetHashCode().ToString() + "_CurrentResetModelReason";
        return currentHttpContext.Items[(object) key] as ResetModelReason;
      }
      set
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        string key = this.Context.GetHashCode().ToString() + "_CurrentResetModelReason";
        currentHttpContext.Items[(object) key] = (object) value;
      }
    }

    private string GetConnectionName() => !(this.DataProvider is IOpenAccessDataProvider dataProvider) ? (string) null : dataProvider.Context.ConnectionId;

    /// <summary>Commits the provided transaction.</summary>
    public virtual void CommitTransaction()
    {
      string connectionName = this.GetConnectionName();
      SitefinityOAContext context = this.Context;
      if (!context.IsActive)
        return;
      this.UpdateInternalDirtyItemsCache(context);
      context.Commit();
      if (!this.DataProvider.SuppressNotifications && this.InvalidatedItems.Count > 0)
      {
        CacheDependency.Notify((IList<CacheDependencyKey>) this.InvalidatedItems, connectionName);
        this.InvalidatedItems.Clear();
      }
      this.EnsureModelReset();
    }

    /// <summary>
    /// Flush all dirty and new instances to the database and evicts all instances from the local cache.
    /// </summary>
    public virtual void FlushTransaction()
    {
      SitefinityOAContext context = this.Context;
      if (!context.IsActive)
        return;
      this.UpdateInternalDirtyItemsCache(context);
      this.UpdateInvalidatedModules(context);
      context.Flush();
    }

    /// <summary>Aborts the transaction for the current scope.</summary>
    public void RollbackTransaction() => this.Context.Rollback();

    /// <summary>
    /// Sets a pessimistic lock for read on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public void LockTransactionForRead(object target)
    {
      SitefinityOAContext context = this.Context;
      if (!context.IsActive)
        return;
      context.Lock(target, LockMode.READ);
    }

    /// <summary>
    /// Sets a pessimistic lock for write on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public void LockTransactionForWrite(object target)
    {
      SitefinityOAContext context = this.Context;
      if (!context.IsActive)
        return;
      context.Lock(target, LockMode.WRITE);
    }

    /// <summary>
    /// When overridden this method returns new transaction object.
    /// </summary>
    /// <param name="transactionName">Name of a global named transaction.
    /// Can be null, which indicates that the transaction is not global.</param>
    /// <returns>The transaction object.</returns>
    public object CreateNewTransaction(string transactionName)
    {
      IOpenAccessDataProvider dataProvider = (IOpenAccessDataProvider) this.DataProvider;
      if (transactionName.IsNullOrEmpty())
        return (object) this.GetNewContext(dataProvider, (string) null);
      string scopeId = this.GetScopeId(dataProvider);
      object transaction = TransactionManager.GetTransaction<object>(transactionName, scopeId);
      if (transaction == null)
      {
        transaction = (object) this.GetNewContext(dataProvider, transactionName);
        TransactionManager.AddTransaction(transactionName, scopeId, (IDataProviderDecorator) this, transaction);
      }
      return transaction;
    }

    /// <inheritdoc />
    public IList GetDirtyItems() => this.Context.GetDirtyItems();

    /// <inheritdoc />
    public bool IsFieldDirty(object entity, string propertyName) => this.Context.IsFieldDirty(entity, propertyName);

    private string GetScopeId(IOpenAccessDataProvider prov) => prov.Context.ProviderKey;

    private SitefinityOAContext GetNewContext(
      IOpenAccessDataProvider prov,
      string transactionName)
    {
      SitefinityOAContext newContext = prov.Context != null ? OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) prov, this.connectionName) : throw new Exception(string.Format("Provider {0} is not initialized", (object) prov.Name));
      newContext.ProviderName = prov.ModuleName + ": " + prov.Name;
      newContext.TransactionName = transactionName;
      IsolationLevel result;
      if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Items != null && SystemManager.CurrentHttpContext.Items[(object) "IsolationLevel"] != null && Enum.TryParse<IsolationLevel>(SystemManager.CurrentHttpContext.Items[(object) "IsolationLevel"].ToString(), out result))
      {
        IsolationLevel? isolationLevel1 = newContext.ContextOptions.IsolationLevel;
        IsolationLevel isolationLevel2 = result;
        if (!(isolationLevel1.GetValueOrDefault() == isolationLevel2 & isolationLevel1.HasValue))
          newContext.ContextOptions.IsolationLevel = new IsolationLevel?(result);
      }
      return newContext;
    }

    /// <summary>
    /// Gets the status of a dirty item in a transaction: new/updated/removed.
    /// </summary>
    /// <param name="itemInTransaction">The item in transaction to check.</param>
    /// <returns>The status of the item</returns>
    public SecurityConstants.TransactionActionType GetDirtyItemStatus(
      object itemInTransaction)
    {
      int num = this.Context.IsNew(itemInTransaction) ? 1 : 0;
      bool flag = this.Context.IsRemoved(itemInTransaction);
      if (num != 0)
        return flag ? SecurityConstants.TransactionActionType.None : SecurityConstants.TransactionActionType.New;
      if (flag)
        return SecurityConstants.TransactionActionType.Deleted;
      return this.Context.IsDirty(itemInTransaction) ? SecurityConstants.TransactionActionType.Updated : SecurityConstants.TransactionActionType.None;
    }

    /// <inheritdoc />
    public T GetOriginalValue<T>(object entity, string propertyName) => this.Context.GetOriginalValue<T>(entity, propertyName);

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
    /// <param name="managerType">The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.</param>
    public void Initialize(string providerName, NameValueCollection config, Type managerType)
    {
      this.connectionName = OpenAccessDecorator.InitializeDatabase(this.DataProvider, config);
      SystemManager.Shutdown += new EventHandler<EventArgs>(this.System_Shutdown);
    }

    private void System_Shutdown(object sender, EventArgs e)
    {
      this.objectConainer = (ObjectContainer) null;
      SystemManager.Shutdown -= new EventHandler<EventArgs>(this.System_Shutdown);
    }

    internal static bool TestConnection(
      IConnectionStringSettings connStrSettings,
      out Exception err,
      int retryCount = 0)
    {
      string connectionString = connStrSettings.ConnectionString;
      try
      {
        using (Database database = Database.Get(connectionString, OpenAccessConnection.CreateBackendConfiguration(connStrSettings.DatabaseType), new EmptyMetadataSource().GetModel()))
        {
          database.GetSchemaHandler();
          MetadataContainer metaData = database.MetaData;
        }
      }
      catch (DatabaseNotFoundException ex)
      {
        string str = "Could not resolve the connect identifier specified";
        err = !ex.Message.ToLowerInvariant().Contains(str.ToLowerInvariant()) ? (Exception) new DatabaseNotFoundException(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().DatabaseNotFound, (Exception) null, (IBackendError) null) : (Exception) new DataStoreException(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().UnableToParseOracleDataSource, (Exception) null);
        return false;
      }
      catch (AuthorizationException ex)
      {
        if (retryCount < 1)
          return OpenAccessDecorator.TestConnection(connStrSettings, out err, 1);
        string msg = connStrSettings.DatabaseType != DatabaseType.MsSql || !connectionString.ToLowerInvariant().Contains("Integrated Security=True".ToLowerInvariant()) && !connectionString.ToLowerInvariant().Contains("Integrated Security=SSPI".ToLowerInvariant()) ? Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().AuthorizationFailed : Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().AuthorizationFailedSqlWinAuth;
        err = (Exception) new AuthorizationException(msg, (Exception) null, OpenAccessException.Failure.Runtime);
        return false;
      }
      catch (DataStoreException ex)
      {
        bool flag = false;
        foreach (string str in new List<string>()
        {
          "Unable to reach database server on host",
          "Unable to connect to",
          "Could not open a connection to"
        })
        {
          if (ex.Message.ToLowerInvariant().Contains(str.ToLowerInvariant()))
          {
            flag = true;
            break;
          }
          if (ex.InnerException != null && ex.InnerException.Message.ToLowerInvariant().Contains(str.ToLowerInvariant()))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          err = (Exception) new DataStoreException(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().DatabaseServerNotFound, (Exception) null);
          return false;
        }
        Log.Write((object) ex, ConfigurationPolicy.ErrorLog);
        err = new Exception(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().GenericStartupDatabaseConnectingErrorMessage);
        return false;
      }
      catch (ConfigurationErrorsException ex)
      {
        Log.Write((object) ex, ConfigurationPolicy.ErrorLog);
        err = (Exception) new ConfigurationErrorsException(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().StartupDatabaseConfigurationErrorMessage);
        return false;
      }
      catch (Exception ex)
      {
        List<string> source = new List<string>();
        source.Add("Database provider Oracle.DataAccess");
        source.Add("Not installed properly");
        Log.Write((object) ex, ConfigurationPolicy.ErrorLog);
        err = !source.All<string>((Func<string, bool>) (p => ex.Message.ToLowerInvariant().Contains(p.ToLowerInvariant()))) ? new Exception(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().GenericStartupDatabaseConnectingErrorMessage) : (Exception) new DataStoreException(Telerik.Sitefinity.Localization.Res.Get<ErrorMessages>().OdacNotInstalledProperly, (Exception) null);
        return false;
      }
      err = (Exception) null;
      return true;
    }

    /// <summary>Initializes the database.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="config">The configuration value collection.</param>
    /// <returns>The name of the connections string to be used.</returns>
    internal static string InitializeDatabase(
      DataProviderBase dataProvider,
      NameValueCollection config)
    {
      string connectionName = config["connectionString"];
      if (string.IsNullOrEmpty(connectionName))
        connectionName = "Sitefinity";
      if (!(dataProvider is IOpenAccessDataProvider provider))
        return connectionName;
      OpenAccessConnection accessConnection = OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) provider, connectionName);
      provider.Context = new OpenAccessProviderContext()
      {
        ProviderKey = Guid.NewGuid().ToString(),
        ConnectionId = accessConnection.Name
      };
      return accessConnection.Name;
    }

    /// <summary>Gets the class id for the given persistent type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The classId of the given type</returns>
    public int GetClassId(Type type) => this.Context.PersistentMetaData.GetPersistentTypeDescriptor(type).ClassId;

    /// <summary>Gets the object container.</summary>
    /// <value>The object container.</value>
    protected virtual ObjectContainer ObjectContainer
    {
      get
      {
        if (this.objectConainer == null)
          this.objectConainer = new ObjectContainer();
        return this.objectConainer;
      }
    }

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <returns>The proxy object</returns>
    public virtual T CreateProxy<T>(T dataItem) where T : IDataItem => this.CreateProxy<T>(dataItem, (string) null, (string) null);

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <param name="listName">Specifies a named list for keeping reference to this proxy. This parameter is not required.</param>
    /// <param name="fetchGroup">The fetch group.</param>
    /// <returns>The proxy object</returns>
    public virtual T CreateProxy<T>(T dataItem, string listName, string fetchGroup) where T : IDataItem
    {
      if ((object) dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (string.IsNullOrEmpty(listName))
        listName = "Default";
      if (string.IsNullOrEmpty(fetchGroup))
        fetchGroup = "Deep";
      IObjectId objectId = this.Context.GetObjectId((object) dataItem);
      this.ObjectContainer.CopyFrom(this.Context.Scope, listName, (object) dataItem, (IObjectCollector) new FetchGroupCollector(fetchGroup));
      CacheDependency.Subscribe((object) dataItem, new ChangedCallback(this.CacheDependencyCallback));
      return (T) this.ObjectContainer.GetObjectById(objectId);
    }

    private void CacheDependencyCallback(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      if (string.IsNullOrEmpty(trackedItemKey))
        return;
      this.ObjectContainer.Evict((object) trackedItemKey);
      CacheDependency.Unsubscribe((object) trackedItemKey, new ChangedCallback(this.CacheDependencyCallback));
    }

    /// <summary>
    /// Gets a proxy object for the specified data item identity if available.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="pageId">The identity of the data item.</param>
    /// <returns>The proxy object if available otherwise null.</returns>
    public virtual T GetProxy<T>(Guid id) where T : IDataItem => (T) this.ObjectContainer.GetObjectById(Database.OID.ParseObjectId(typeof (T), id.ToString()));

    /// <summary>
    /// Gets the specified <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="objectId">The identity of the <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public virtual ManagerInfo GetManagerInfo(Guid id) => !(id == Guid.Empty) ? this.Context.GetItemById<ManagerInfo>(id.ToString()) : throw new ArgumentException("id cannot be empty GUID.");

    /// <summary>
    /// Gets or creates new manager info if one does not exist with the provided parameters.
    /// </summary>
    /// <param name="managerType">The full type name, without assembly information, of the manager.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <returns>ManagerInfo</returns>
    public virtual ManagerInfo GetManagerInfo(string managerType, string providerName)
    {
      if (string.IsNullOrEmpty(managerType))
        throw new ArgumentNullException(nameof (managerType));
      if (string.IsNullOrEmpty(providerName))
        throw new ArgumentNullException(nameof (providerName));
      ManagerInfo managerInfo1 = this.GetManagerInfos().Where<ManagerInfo>((Expression<Func<ManagerInfo, bool>>) (i => i.ManagerType == managerType && i.ProviderName == providerName)).SingleOrDefault<ManagerInfo>();
      if (managerInfo1 == null)
      {
        Type type = TypeResolutionService.ResolveType(managerType, true);
        managerType = !(type is IManager) ? type.FullName : throw new ArgumentException("Invalid manager type specified.");
        ManagerInfo managerInfo2 = new ManagerInfo();
        managerInfo2.ApplicationName = this.DataProvider.ApplicationName;
        managerInfo2.Id = this.GetNewGuid();
        managerInfo2.ManagerType = managerType;
        managerInfo2.ProviderName = providerName;
        using (IDisposable newTransaction = (IDisposable) this.CreateNewTransaction((string) null))
        {
          if (newTransaction is IObjectScope)
          {
            ((IObjectContext) newTransaction).Add((object) managerInfo2);
            ((IObjectContext) newTransaction).Transaction.Commit();
          }
          else
          {
            ((OpenAccessContextBase) newTransaction).Add((object) managerInfo2);
            ((OpenAccessContextBase) newTransaction).SaveChanges();
          }
        }
        managerInfo1 = this.GetManagerInfo(managerInfo2.Id);
      }
      return managerInfo1;
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> objects.
    /// </summary>
    /// <returns>The query object.</returns>
    public virtual IQueryable<ManagerInfo> GetManagerInfos()
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<ManagerInfo>(this.DataProvider).Where<ManagerInfo>((Expression<Func<ManagerInfo, bool>>) (e => e.ApplicationName == appName));
    }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public virtual ManagerInfo CreateManagerInfo() => this.CreateManagerInfo(this.GetNewGuid());

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public virtual ManagerInfo CreateManagerInfo(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      ManagerInfo entity = new ManagerInfo();
      entity.ApplicationName = this.DataProvider.ApplicationName;
      entity.Id = id;
      ((IDataItem) entity).Provider = (object) this.DataProvider;
      this.Context.Add((object) entity);
      return entity;
    }

    /// <summary>Deletes the manager info.</summary>
    /// <param name="info">The manager info to be deleted.</param>
    public virtual void DeleteManagerInfo(ManagerInfo info) => this.Context.Delete((object) info);

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public object Clone() => this.MemberwiseClone();

    /// <summary>
    /// Raises the <see cref="E:Executing" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public virtual void OnExecuting(ExecutingEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      this.DataProvider.Decorator_Executing((object) this, args);
      if (this.Executing == null)
        return;
      this.Executing((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:Executed" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public void OnExecuted(ExecutedEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      this.DataProvider.Decorator_Executed((object) this, args);
      if (this.Executed == null)
        return;
      this.Executed((object) this, args);
    }

    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    public event EventHandler<ExecutingEventArgs> Executing;

    /// <summary>Fired after executing data method.</summary>
    public event EventHandler<ExecutedEventArgs> Executed;

    /// <summary>Gets the execution state data.</summary>
    /// <param name="stateBagKey">The state bag key.</param>
    /// <returns></returns>
    public object GetExecutionStateData(string stateBagKey)
    {
      if (!this.Context.HasExecutionState)
        return (object) null;
      return !this.Context.ExecutionStateBag.ContainsKey(stateBagKey) ? (object) null : this.Context.ExecutionStateBag[stateBagKey];
    }

    /// <summary>Sets the execution state data.</summary>
    /// <param name="stateBagKey">The state bag key.</param>
    /// <param name="data">The data.</param>
    public void SetExecutionStateData(string stateBagKey, object data)
    {
      if (data == null)
      {
        if (!this.Context.HasExecutionState || !this.Context.ExecutionStateBag.ContainsKey(stateBagKey))
          return;
        this.Context.ExecutionStateBag.Remove(stateBagKey);
      }
      else
        this.Context.ExecutionStateBag[stateBagKey] = data;
    }

    /// <summary>Clears the execution state bag of the current context</summary>
    public void ClearExecutionStateBag() => this.Context.ClearExecutionStateBag();

    public static IDatabaseMappingContext CreateFluentMappingContext(
      DatabaseType databaseTypes)
    {
      return OpenAccessConnection.CreateFluentMappingContext(databaseTypes);
    }

    /// <inheritdoc />
    public virtual Guid GetNewGuid() => OpenAccessDecorator.CurrentGuidGenerator.GetNewGuid(this);

    private static OpenAccessDecorator.IGuidGenerator CurrentGuidGenerator
    {
      get
      {
        if (OpenAccessDecorator.currentGuidGenerator == null)
          OpenAccessDecorator.currentGuidGenerator = Config.Get<DataConfig>().GuidGenerationStrategy != GuidGenerationStrategies.Random ? (OpenAccessDecorator.IGuidGenerator) new OpenAccessDecorator.IntegratedGuidGenerator() : (OpenAccessDecorator.IGuidGenerator) new OpenAccessDecorator.DotNetGuidGenerator();
        return OpenAccessDecorator.currentGuidGenerator;
      }
    }

    private interface IGuidGenerator
    {
      Guid GetNewGuid(OpenAccessDecorator oaDecorator);
    }

    private class IntegratedGuidGenerator : OpenAccessDecorator.IGuidGenerator
    {
      public Guid GetNewGuid(OpenAccessDecorator oaDecorator) => oaDecorator.Context.KeyGenerators.GetIncrementalGuid(50);
    }

    private class DotNetGuidGenerator : OpenAccessDecorator.IGuidGenerator
    {
      public Guid GetNewGuid(OpenAccessDecorator oaDecorator) => Guid.NewGuid();
    }
  }
}
