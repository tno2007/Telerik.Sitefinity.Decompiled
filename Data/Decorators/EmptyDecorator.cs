// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.EmptyDecorator
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
  public class EmptyDecorator : IDataProviderDecorator, ICloneable, IDataProviderEventsCall
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    public DataProviderBase DataProvider { get; set; }

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    public bool Suspended => false;

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
      throw new NotSupportedException();
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public IQueryable<Permission> GetPermissions() => throw new NotSupportedException();

    /// <summary>Creates new permission.</summary>
    /// <typeparam name="TPermission"></typeparam>
    /// <param name="permissionSet">The permission set name.</param>
    /// <param name="objectId">The secured object identifier.</param>
    /// <param name="principalId">The principal identifier.</param>
    /// <returns></returns>
    public Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Deletes the permissions relevant to a secured object.
    /// If the object inherits permissions, only the ones which are directly attached to the object, not inherited ones.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    public void DeletePermissions(object securedObject) => throw new NotImplementedException();

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public void DeletePermission(Permission permission) => throw new NotSupportedException();

    public virtual void CreatePermissionInheritanceAssociation(
      ISecuredObject parent,
      ISecuredObject child)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permission">The permission.</param>
    public void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName)
    {
      throw new NotSupportedException();
    }

    /// <summary>Adds the permission to object.</summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="managerInstance">The manager instance.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string transactionName)
    {
      throw new NotSupportedException();
    }

    /// <summary>Commits the provided transaction.</summary>
    public virtual void CommitTransaction() => throw new NotSupportedException();

    /// <summary>Aborts the transaction for the current scope.</summary>
    public void AbortTransaction() => throw new NotSupportedException();

    /// <summary>
    /// When overridden this method returns new transaction object.
    /// </summary>
    /// <returns>The transaction object.</returns>
    public object CreateNewTransaction() => throw new NotSupportedException();

    public IList GetDirtyItems() => throw new NotSupportedException();

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
    /// <param name="managerType">The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.</param>
    public void Initialize(string providerName, NameValueCollection config, Type managerType)
    {
    }

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public SecurityRoot GetSecurityRoot() => (SecurityRoot) null;

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public SecurityRoot GetSecurityRoot(bool create) => (SecurityRoot) null;

    /// <inheritdoc />
    public IQueryable<PermissionsInheritanceMap> GetPermissionChildren(
      Guid parentId)
    {
      return new List<PermissionsInheritanceMap>().AsQueryable<PermissionsInheritanceMap>();
    }

    /// <summary>
    /// Gets the specified <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public ManagerInfo GetManagerInfo(Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> objects.
    /// </summary>
    /// <returns>The query object.</returns>
    public IQueryable<ManagerInfo> GetManagerInfos() => ((IEnumerable<ManagerInfo>) new ManagerInfo[0]).AsQueryable<ManagerInfo>();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public ManagerInfo CreateManagerInfo() => this.CreateManagerInfo(Guid.Empty);

    /// <summary>
    /// Gets or creates new manager info if one does not exist with the provided parameters.
    /// </summary>
    /// <param name="managerType">The full type name, without assembly information, of the manager.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <returns>ManagerInfo</returns>
    public ManagerInfo GetManagerInfo(string managerType, string providerName) => throw new NotSupportedException();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    public ManagerInfo CreateManagerInfo(Guid id) => new ManagerInfo()
    {
      Id = id,
      ApplicationName = this.DataProvider.ApplicationName
    };

    /// <summary>Deletes the permission.</summary>
    /// <param name="info"></param>
    public void DeleteManagerInfo(ManagerInfo info) => throw new NotSupportedException();

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <returns>The proxy object</returns>
    public T CreateProxy<T>(T dataItem) where T : IDataItem => dataItem;

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <param name="listName">Specifies a named list for keeping reference to this proxy. This parameter is not required.</param>
    /// <param name="fetchGroup">The fetch group.</param>
    /// <returns>The proxy object</returns>
    public T CreateProxy<T>(T dataItem, string listName, string fetchGroup) where T : IDataItem => dataItem;

    /// <summary>
    /// Gets a proxy object for the specified data item identity if available.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="pageId">The identity of the data item.</param>
    /// <returns>The proxy object if available otherwise null.</returns>
    public T GetProxy<T>(Guid id) where T : IDataItem => throw new NotSupportedException();

    public void FlushTransaction()
    {
    }

    public void RollbackTransaction()
    {
    }

    public void LockTransactionForRead(object target)
    {
    }

    public void LockTransactionForWrite(object target)
    {
    }

    public virtual object CreateNewTransaction(string transactionName) => (object) null;

    public IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Permission => throw new NotImplementedException();

    public IQueryable<Permission> GetPermissions(Type actualType) => throw new NotImplementedException();

    public object Clone() => this.MemberwiseClone();

    /// <summary>
    /// Converts a VOA (shorter OpenAccess string representation of a type) to a CLR type
    /// </summary>
    /// <param name="voaClassName">Name of the voa class.</param>
    /// <returns>The resolved CLR type</returns>
    public Type ConvertVoaClassToClrType(string voaClassName) => throw new NotImplementedException();

    /// <summary>
    /// Converts  a CLR type to a VOA string (shorter OpenAccess string representation of a type)
    /// </summary>
    /// <param name="ClrType">The CLR type.</param>
    /// <returns>Name of the voa class.</returns>
    public string ConvertClrTypeVoaClass(Type ClrType) => throw new NotImplementedException();

    public Permission GetPermission(Guid permissionId) => throw new NotImplementedException();

    public SecurityRoot GetSecurityRoot(
      bool create,
      IDictionary<string, string> permissionsetObjectTitleResKeys,
      params string[] permissionSets)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the status of a dirty item in a transaction: new/updated/removed.
    /// </summary>
    /// <param name="itemInTransaction">The item in transaction to check.</param>
    /// <returns></returns>
    public SecurityConstants.TransactionActionType GetDirtyItemStatus(
      object itemInTransaction)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetOriginalValue<T>(object entity, string propertyName) => throw new NotImplementedException();

    /// <inheritdoc />
    public bool IsFieldDirty(object entity, string fieldName) => throw new NotImplementedException();

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

    /// <inheritdoc />
    public PermissionsInheritanceMap CreatePermissionsInheritanceMap(
      Guid objectId,
      Guid childObjectId,
      string childObjectTypeName)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void DeletePermissionsInheritanceAssociation(ISecuredObject parent, ISecuredObject child) => throw new NotImplementedException();

    /// <summary>Gets the class id for the given persistent type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The classId of the given type</returns>
    public int GetClassId(Type type) => type.GetHashCode();

    /// <inheritdoc />
    public Guid GetNewGuid() => Guid.NewGuid();

    public void OnExecuting(ExecutingEventArgs args) => throw new NotImplementedException();

    public void OnExecuted(ExecutedEventArgs args) => throw new NotImplementedException();

    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    public event EventHandler<ExecutingEventArgs> Executing;

    /// <summary>Fired after executing data method.</summary>
    public event EventHandler<ExecutedEventArgs> Executed;
  }
}
