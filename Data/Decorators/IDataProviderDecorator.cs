// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.IDataProviderDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// Represents common interface for data provider decorators.
  /// </summary>
  public interface IDataProviderDecorator : ICloneable, IDataProviderEventsCall
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    DataProviderBase DataProvider { get; set; }

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    ///     <c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    bool Suspended { get; }

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    Permission GetPermission(string permissionSet, Guid objectId, Guid principalId);

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionId">The permission pageId.</param>
    /// <returns></returns>
    Permission GetPermission(Guid permissionId);

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    IQueryable<Permission> GetPermissions();

    /// <summary>Gets a query for permissions.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <returns></returns>
    [Obsolete("Remove this. Use GetPermissions() instead!")]
    IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Permission;

    /// <summary>Gets a query for permissions.</summary>
    /// <param name="actualType">The actual type.</param>
    /// <returns></returns>
    [Obsolete("Remove this. Use GetPermissions() instead!")]
    IQueryable<Permission> GetPermissions(Type actualType);

    /// <summary>
    /// Converts a VOA (shorter OpenAccess string representation of a type) to a CLR type
    /// </summary>
    /// <param name="voaClassName">Name of the voa class.</param>
    /// <returns>The resolved CLR type</returns>
    Type ConvertVoaClassToClrType(string voaClassName);

    /// <summary>
    /// Converts  a CLR type to a VOA string (shorter OpenAccess string representation of a type)
    /// </summary>
    /// <param name="ClrType">The CLR type.</param>
    /// <returns>Name of the voa class.</returns>
    string ConvertClrTypeVoaClass(Type ClrType);

    /// <summary>Creates new permission.</summary>
    /// <param name="permissionSet">The permission set name.</param>
    /// <param name="objectId">The secured object identifier.</param>
    /// <param name="principalId">The principal identifier.</param>
    /// <returns></returns>
    Permission CreatePermission(string permissionSet, Guid objectId, Guid principalId);

    /// <summary>
    /// Deletes the permissions relevant to a secured object.
    /// If the object inherits permissions, only the ones which are directly attached to the object, not inherited ones.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    void DeletePermissions(object securedObject);

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    void DeletePermission(Permission permission);

    /// <summary>
    /// Executes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="child">The permissions child.</param>
    void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child);

    /// <summary>
    /// Deletes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="child">The child.</param>
    void DeletePermissionsInheritanceAssociation(ISecuredObject parent, ISecuredObject child);

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object to add permission to.</param>
    /// <param name="permission">The permission to add.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName);

    /// <summary>Adds the permission to object.</summary>
    /// <param name="securedObject">The secured object to add permission to.</param>
    /// <param name="managerInstance">This parameter is obsolete - Instance of the secured object's related manager.</param>
    /// <param name="permission">The permission to add.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string transactionName);

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    IQueryable<PermissionsInheritanceMap> GetInheritanceMaps();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap);

    /// <summary>Creates a permissions inheritance map object.</summary>
    /// <param name="objectId"></param>
    /// <param name="childObjectId"></param>
    /// <param name="childObjectTypeName"></param>
    /// <returns></returns>
    PermissionsInheritanceMap CreatePermissionsInheritanceMap(
      Guid objectId,
      Guid childObjectId,
      string childObjectTypeName);

    /// <summary>Commits all changes and clears current transaction.</summary>
    void CommitTransaction();

    /// <summary>
    /// Flush all dirty and new instances to the database and evicts all instances from the local cache.
    /// </summary>
    void FlushTransaction();

    /// <summary>Aborts the transaction and roles back all changes.</summary>
    void RollbackTransaction();

    /// <summary>
    /// Sets a pessimistic lock for read on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    void LockTransactionForRead(object target);

    /// <summary>
    /// Sets a pessimistic lock for write on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    void LockTransactionForWrite(object target);

    /// <summary>Gets the dirty items from the current transaction.</summary>
    /// <returns>An array of dirty items.</returns>
    new IList GetDirtyItems();

    /// <summary>
    /// Gets the status of a dirty item in a transaction: new/updated/removed.
    /// </summary>
    /// <param name="itemInTransaction">The item in transaction to check.</param>
    /// <returns></returns>
    SecurityConstants.TransactionActionType GetDirtyItemStatus(
      object itemInTransaction);

    /// <summary>
    /// When overridden this method returns new transaction object.
    /// </summary>
    /// <param name="transactionName">
    /// Name of a global named transaction.
    /// Can be null, which indicates that the transaction is not global.
    /// </param>
    /// <returns>The transaction object.</returns>
    object CreateNewTransaction(string transactionName);

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">
    /// A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.
    /// </param>
    /// <param name="managerType">
    /// The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.
    /// </param>
    void Initialize(string providerName, NameValueCollection config, Type managerType);

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    SecurityRoot GetSecurityRoot();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    SecurityRoot GetSecurityRoot(bool create);

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <param name="permissionsetObjectTitleResKeys">Dictionary: Key is a name of a permission set supported by this provider, Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders..</param>
    /// <param name="permissionSets">Optional permission sets supproted by this security root</param>
    /// <returns></returns>
    SecurityRoot GetSecurityRoot(
      bool create,
      IDictionary<string, string> permissionsetObjectTitleResKeys,
      params string[] permissionSets);

    /// <summary>Gets permission children for the given parent id.</summary>
    /// <param name="parentId">The id of the parent item which children will be returned.</param>
    IQueryable<PermissionsInheritanceMap> GetPermissionChildren(
      Guid parentId);

    /// <summary>
    /// Gets the specified <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="objectId">The identity of the <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    ManagerInfo GetManagerInfo(Guid id);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> objects.
    /// </summary>
    /// <returns>The query object.</returns>
    IQueryable<ManagerInfo> GetManagerInfos();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    ManagerInfo CreateManagerInfo();

    /// <summary>
    /// Gets or creates new manager info if one does not exist with the provided parameters.
    /// </summary>
    /// <param name="managerType">The full type name, without assembly information, of the manager.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <returns>ManagerInfo</returns>
    ManagerInfo GetManagerInfo(string managerType, string providerName);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    ManagerInfo CreateManagerInfo(Guid id);

    /// <summary>Deletes the manager info.</summary>
    /// <param name="info">The manager info to be deleted.</param>
    void DeleteManagerInfo(ManagerInfo info);

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <returns>The proxy object</returns>
    T CreateProxy<T>(T dataItem) where T : IDataItem;

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <param name="listName">Specifies a named list for keeping reference to this proxy. This parameter is not required.</param>
    /// <param name="fetchGroup">The fetch group.</param>
    /// <returns>The proxy object</returns>
    T CreateProxy<T>(T dataItem, string listName, string fetchGroup) where T : IDataItem;

    /// <summary>
    /// Gets a proxy object for the specified data item identity if available.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="pageId">The identity of the data item.</param>
    /// <returns>The proxy object if available otherwise null.</returns>
    T GetProxy<T>(Guid id) where T : IDataItem;

    /// <summary>Gets the class id for the given persistent type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The classId of the given type</returns>
    int GetClassId(Type type);

    /// <summary>Generates a new GUID.</summary>
    /// <returns></returns>
    Guid GetNewGuid();

    /// <summary>
    /// Gets the original value of the specified property for the persisted object.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    T GetOriginalValue<T>(object entity, string propertyName);

    /// <summary>
    /// Determines whether the specifield field of the given persisted object is changed.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="propertyName">Name of the field.</param>
    /// <returns></returns>
    bool IsFieldDirty(object entity, string fieldName);
  }
}
