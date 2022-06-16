// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Defines Manager base methods.</summary>
  public interface IManager : IDisposable, IProviderResolver
  {
    /// <summary>Gets the current provider</summary>
    DataProviderBase Provider { get; }

    /// <summary>Gets the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    string TransactionName { get; }

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    [Obsolete("No longer returns the providers, created when creating a site in Multisite managerment, as those providers are not instantiated on startup. To get all providers, use GetAllProviderInfos() extension method of IManager, which returns a collection of IDataProviderInfo objects (Name, Title, etc.), that describes the provider without instantiating it. Having the information about the provider, it can be instantiated using GetManager() method and providing the name of the provider.To get static providers instantiated from the configuration, StaticProviders property can be used.")]
    IEnumerable<DataProviderBase> Providers { get; }

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    IEnumerable<DataProviderBase> StaticProviders { get; }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    IEnumerable<Type> GetKnownTypes();

    /// <summary>Gets or sets the object container.</summary>
    /// <value>The object container.</value>
    object ObjectContainer { get; set; }

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>New data item.</returns>
    object CreateItem(Type itemType);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    object CreateItem(Type itemType, Guid id);

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    object GetItem(Type itemType, Guid id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    object GetItemOrDefault(Type itemType, Guid id);

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns></returns>
    IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take);

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount);

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    void DeleteItem(object item);

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="language">The language version to delete.</param>
    void DeleteItem(object item, CultureInfo language);

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    Permission GetPermission(string permissionSet, Guid objectId, Guid principalId);

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    IQueryable<Permission> GetPermissions();

    /// <summary>Creates new permission of generic type TPermission</summary>
    /// <typeparam name="TPermission">Must inherit from Permission</typeparam>
    /// <param name="permissionSet">The name of the permission set</param>
    /// <param name="objectId">Id of the new permission</param>
    /// <param name="principalId">ID of the principal related to the permission</param>
    /// <returns>The newly created permission</returns>
    Permission CreatePermission(string permissionSet, Guid objectId, Guid principalId);

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName);

    /// <summary>Adds the permission to object.</summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="managerInstance">This parameter is obsolete - The manager instance.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string transactionName);

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    void DeletePermission(Permission permission);

    /// <summary>
    /// Makes a deep copy of the permission from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source collection of permissions.</param>
    /// <param name="target">The target list.</param>
    /// <param name="sourceObjectId">The source object pageId.</param>
    /// <param name="targetObjectId">The target object pageId.</param>
    void CopyPermissions(
      IEnumerable<Permission> source,
      IList target,
      Guid sourceObjectId,
      Guid targetObjectId);

    /// <summary>
    /// Gets the all the secured objects which inherit permissions, through permissions hierarchy, from a secured object.
    /// </summary>
    /// <param name="root">The root object for which the permission inherirots are scanned. Must implement the ISecuredObject interface</param>
    /// <param name="inheritors">A empty list of ISecuredObject object. This argument is required for the recursion of the method.</param>
    /// <param name="permissionInheritorsOnly">if set to <c>true</c>, only the child objects for which the InheritsPermissions property is set to true are returned. Otherwise, all child objects are returned.</param>
    /// <param name="objectType">If set to null, all child objects are returned. If set to a Type, only child objects of the type are returned.</param>
    /// <returns>A List of ISecured objects which inherit permissions, through permissions hierarchy, from the root object.</returns>
    List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType);

    /// <summary>
    /// This recursive method handles the correct behaviour when the inheritance is broken or restored on a secured object:
    /// 1. The direct permissions inheritors (children in the tree) of the object (parent), are removed all their inherited permissions.
    ///   - When the inheritance on the root is broken, each child is  assigned the permissions from the parent object.
    ///   - When the inheritance on the root is restored, each child is  assigned the permissions from the parent's inherited permissions.
    ///   (the actions are the same, but the permissions set in the permissionsToAssignToChildren variable make the difference).
    /// 2. For each of the children, if it inherits permissions, the assignment (#1) continues recursively down to the grandchildren, and so on.
    /// 3. If down the tree, an offspring (not a direct child of the root) does not inherit permissions, the function will not continue to its children.
    /// </summary>
    /// <param name="brokenOrRestoredInheritanceRootObjectId">The broken or restored inheritance root object id.</param>
    /// <param name="permissionsToAssignToChildren">The permissions to assign to children.</param>
    /// <param name="currentlyScannedNode">The currently scanned node in the recursion.</param>
    /// <param name="transactionName">The name of the transaction for the child object changes.</param>
    void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenInheritanceRootObjectId,
      List<Permission> brokenInheritanceRootUninheritedPermissions,
      ISecuredObject currentlyScannedNode,
      string transactionName);

    /// <summary>
    /// Executes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="child">The permissions child.</param>
    void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child);

    /// <summary>Breaks the permiossions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    void BreakPermiossionsInheritance(ISecuredObject securedObject);

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    void RestorePermissionsInheritance(ISecuredObject securedObject);

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    IQueryable<PermissionsInheritanceMap> GetInheritanceMaps();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap);

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    void SaveChanges();

    /// <summary>
    /// Cancels any changes made to objects retrieved with this manager.
    /// </summary>
    void CancelChanges();

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    ISecuredObject GetSecurityRoot();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    ISecuredObject GetSecurityRoot(bool create);
  }
}
