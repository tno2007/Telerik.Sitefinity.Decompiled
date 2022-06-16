// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DummyManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// A dummy manager class used to persist the transaction of instantly created providers.
  /// </summary>
  internal class DummyManager : IManager, IDisposable, IProviderResolver
  {
    internal static T CreateInstantProvider<T>(DataProviderSettings settings) where T : DataProviderBase
    {
      string name1 = settings.Name;
      Type type = typeof (DummyManager);
      T instantProvider = (T) ObjectFactory.Resolve(settings.ProviderType);
      NameValueCollection parameters = settings.Parameters;
      NameValueCollection config = new NameValueCollection(parameters.Count, (IEqualityComparer) StringComparer.Ordinal);
      config["description"] = settings.Description;
      config["resourceClassId"] = settings.ResourceClassId;
      foreach (string name2 in (NameObjectCollectionBase) parameters)
        config[name2] = parameters[name2];
      ObjectFactory.Container.RegisterType(type, type, name1.ToUpperInvariant(), (LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) name1
      }));
      instantProvider.Initialize(name1, config, type);
      return instantProvider;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DummyManager" /> class.
    /// </summary>
    /// <param name="dummyParam">The dummy param.</param>
    public DummyManager(string dummyParam) => string.Format("{0}", (object) dummyParam);

    /// <summary>Gets or sets the object container.</summary>
    /// <value>The object container.</value>
    public object ObjectContainer { get; set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (!(this.ObjectContainer is IDisposable objectContainer))
        return;
      objectContainer.Dispose();
    }

    /// <summary>Gets the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    public virtual string TransactionName { get; private set; }

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>New data item.</returns>
    public object CreateItem(Type itemType) => throw new NotSupportedException();

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public object GetItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns></returns>
    public IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotSupportedException();
    }

    /// <inheritdoc />
    public IEnumerable GetItems(Type itemType, out int? totalCount, int skip = 0, int take = 0) => throw new NotSupportedException();

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public void DeleteItem(object item) => throw new NotSupportedException();

    public void DeleteItem(object item, CultureInfo language) => throw new NotSupportedException();

    /// <summary>
    /// Gets a collection of all providers configured for this manager.
    /// </summary>
    /// <value></value>
    public IEnumerable<DataProviderBase> Providers => this.StaticProviders;

    /// <summary>
    /// Gets a collection of all providers configured for this manager.
    /// </summary>
    /// <value></value>
    public IEnumerable<DataProviderBase> StaticProviders => throw new NotSupportedException();

    /// <summary>Gets the current provider</summary>
    public DataProviderBase Provider => (DataProviderBase) null;

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
      throw new NotImplementedException();
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public IQueryable<Permission> GetPermissions() => throw new NotImplementedException();

    /// <summary>Creates new permission.</summary>
    /// <param name="permissionSet">The permission set name.</param>
    /// <param name="objectId">The secured object identifier.</param>
    /// <param name="principalId">The principal identifier.</param>
    /// <returns></returns>
    public Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      string.Format("{0}{1}{2}", (object) permissionSet, (object) objectId, (object) principalId);
      throw new NotImplementedException();
    }

    /// <summary>Creates the permission.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public TPermission CreatePermission<TPermission>(
      string permissionSet,
      Guid objectId,
      Guid principalId)
      where TPermission : Permission, new()
    {
      throw new NotImplementedException();
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public void DeletePermission(Permission permission) => throw new NotImplementedException();

    /// <summary>
    /// Makes a deep copy of the permission from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source collection of permissions.</param>
    /// <param name="target">The target list.</param>
    /// <param name="sourceObjectId">The source object pageId.</param>
    /// <param name="targetObjectId">The target object pageId.</param>
    public void CopyPermissions(
      IEnumerable<Permission> source,
      IList target,
      Guid sourceObjectId,
      Guid targetObjectId)
    {
      throw new NotImplementedException();
    }

    public void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child) => throw new NotSupportedException();

    /// <summary>Breaks the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void BreakPermiossionsInheritance(ISecuredObject securedObject) => throw new NotSupportedException();

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void RestorePermissionsInheritance(ISecuredObject securedObject) => throw new NotSupportedException();

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    public void SaveChanges() => throw new NotImplementedException();

    /// <summary>
    /// Cancels any changes made to objects retrieved with this manager.
    /// </summary>
    public void CancelChanges() => throw new NotImplementedException();

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public ISecuredObject GetSecurityRoot() => throw new NotImplementedException();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    public ISecuredObject GetSecurityRoot(bool create) => throw new NotImplementedException();

    public List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType)
    {
      throw new NotImplementedException();
    }

    public void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName)
    {
      throw new NotImplementedException();
    }

    public void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string transactionName)
    {
      throw new NotImplementedException();
    }

    public void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenInheritanceRootObjectId,
      List<Permission> brokenInheritanceRootUninheritedPermissions,
      ISecuredObject currentlyScannedNode,
      string transactionName)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => throw new NotImplementedException();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      throw new NotImplementedException();
    }

    public object GetItemOrDefault(Type itemType, Guid id) => throw new NotImplementedException();

    /// <inheritdoc />
    public DataProviderBase GetDefaultContextProvider() => throw new NotSupportedException();

    /// <inheritdoc />
    public IEnumerable<DataProviderBase> GetContextProviders() => throw new NotSupportedException();

    /// <inheritdoc />
    public IEnumerable<Type> GetKnownTypes() => throw new NotSupportedException();
  }
}
