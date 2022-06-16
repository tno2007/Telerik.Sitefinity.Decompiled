// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyContentManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class AnyContentManager : 
    IAnyContentManager,
    IContentLifecycleManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IContentManager
  {
    private IManager manager;
    private IContentManager content;
    private IContentLifecycleManager lifecycleManager;
    private ILifecycleDecorator lifecycle;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyContentManager" /> class.
    /// </summary>
    public AnyContentManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyContentManager" /> class.
    /// </summary>
    /// <param name="managerToWrap">The manager to wrap.</param>
    /// <param name="itemType">Type of the item.</param>
    public AnyContentManager(IManager managerToWrap, Type itemType) => this.Initialize(managerToWrap, itemType);

    /// <summary>Initializes the specified manager to wrap.</summary>
    /// <param name="managerToWrap">The manager to wrap.</param>
    /// <param name="itemType">Type of the item.</param>
    public void Initialize(IManager managerToWrap, Type itemType)
    {
      this.manager = managerToWrap;
      this.content = managerToWrap as IContentManager;
      this.lifecycleManager = managerToWrap as IContentLifecycleManager;
      if (!typeof (ILifecycleManager).IsAssignableFrom(managerToWrap.GetType()) || !typeof (ILifecycleDataItem).IsAssignableFrom(itemType))
        return;
      this.lifecycle = (managerToWrap as ILifecycleManager).Lifecycle;
      this.UseLifecycleDecorator = true;
    }

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Content CheckIn(Content item) => this.CheckIn(item, (CultureInfo) null, true);

    /// <summary>
    /// Checks in the content in the temp state for a given culture. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <param name="culture">The culture to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Content CheckIn(Content item, CultureInfo culture, bool deleteTemp = true) => this.UseLifecycleDecorator ? this.lifecycle.CheckIn(item as ILifecycleDataItem, culture, deleteTemp) as Content : this.lifecycleManager.CheckIn(item);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Content CheckOut(Content item) => this.CheckOut(item, (CultureInfo) null);

    /// <summary>
    /// Checks out the content in master state for a given culture. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <param name="culture">The culture to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Content CheckOut(Content item, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.CheckOut(item as ILifecycleDataItem, culture) as Content : this.lifecycleManager.CheckOut(item);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Content Edit(Content item) => this.Edit(item, (CultureInfo) null);

    /// <summary>
    /// Edits the content in live state for a given culture. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <param name="culture">The culture to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Content Edit(Content item, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.Edit(item as ILifecycleDataItem, culture) as Content : this.lifecycleManager.Edit(item);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    public Content Publish(Content item) => this.Publish(item, new DateTime?(), (CultureInfo) null);

    /// <summary>
    /// Publishes the content in master state for a give culture. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <param name="culture">The culture to publish.</param>
    /// <returns>Published content item</returns>
    public Content Publish(Content item, CultureInfo culture) => this.Publish(item, new DateTime?(), culture);

    public Content Publish(Content item, DateTime publicationDate) => this.Publish(item, new DateTime?(publicationDate), (CultureInfo) null);

    public Content Publish(Content item, DateTime? publicationDate, CultureInfo culture)
    {
      if (!this.UseLifecycleDecorator)
        return this.lifecycleManager.Publish(item);
      return publicationDate.HasValue ? this.lifecycle.PublishWithSpecificDate(item as ILifecycleDataItem, publicationDate.Value, culture) as Content : this.lifecycle.Publish(item as ILifecycleDataItem, culture) as Content;
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public Content Unpublish(Content item) => this.Unpublish(item, (CultureInfo) null);

    /// <summary>
    /// Unpublish a content item in live state for a give culture.
    /// </summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <param name="culture">The culture to publish.</param>
    /// <returns>Master (draft) state.</returns>
    public Content Unpublish(Content item, CultureInfo culture)
    {
      IHasTrackingContext context = (IHasTrackingContext) item;
      if (context != null)
        context.RegisterOperation(OperationStatus.Unpublished);
      return this.UseLifecycleDecorator ? this.lifecycle.Unpublish(item as ILifecycleDataItem, culture) as Content : this.lifecycleManager.Unpublish(item);
    }

    /// <summary>Copies the specified source.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public void Copy(Content source, Content destination) => this.Copy(source, destination, (CultureInfo) null);

    /// <summary>
    /// Copis all proeprties from the source item to the destination using the specified culture
    /// </summary>
    /// <param name="source">source item</param>
    /// <param name="destination">destination item</param>
    /// <param name="culture">culture - if not specified the current thread UI culture will be used</param>
    public void Copy(Content source, Content destination, CultureInfo culture)
    {
      if (this.UseLifecycleDecorator)
        this.lifecycle.CopyProperties((ILifecycleDataItem) source, (ILifecycleDataItem) destination, culture);
      else
        this.lifecycleManager.Copy(source, destination);
    }

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    /// <returns>Scheduled content item</returns>
    public Content Schedule(
      Content item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      if (this.UseLifecycleDecorator)
        throw new NotSupportedException();
      return this.lifecycleManager.Schedule(item, publicationDate, expirationDate);
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(Content item) => this.GetCheckedOutBy(item, (CultureInfo) null);

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="culture"></param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(Content item, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.GetCheckedOutBy(item as ILifecycleDataItem, culture) : this.lifecycleManager.GetCheckedOutBy(item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(Content item) => this.IsCheckedOut(item, (CultureInfo) null);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(Content item, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.IsCheckedOut((ILifecycleDataItem) item, culture) : this.lifecycleManager.IsCheckedOut(item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    public bool IsCheckedOutBy(Content item, Guid userId) => this.IsCheckedOutBy(item, userId, (CultureInfo) null);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    /// 
    ///             ///
    public bool IsCheckedOutBy(Content item, Guid userId, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.IsCheckedOutBy(item as ILifecycleDataItem, userId, culture) : this.lifecycleManager.IsCheckedOutBy(item, userId);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLive(Content cnt) => this.GetLive(cnt, (CultureInfo) null);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists, for a given culture.
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <param name="culture">The culture for which to get the live version.</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLive(Content cnt, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.GetLive((ILifecycleDataItem) cnt, culture) as Content : this.lifecycleManager.GetLive(cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetTemp(Content cnt) => this.GetTemp(cnt, (CultureInfo) null);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists, for a given culture.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <param name="culture">The culture for which to get the temp version.</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetTemp(Content cnt, CultureInfo culture) => this.UseLifecycleDecorator ? this.lifecycle.GetTemp((ILifecycleDataItem) cnt, culture) as Content : this.lifecycleManager.GetTemp(cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetMaster(Content cnt) => this.UseLifecycleDecorator ? this.lifecycle.GetMaster((ILifecycleDataItem) cnt) as Content : this.lifecycleManager.GetMaster(cnt);

    /// <summary>Gets the current provider</summary>
    public DataProviderBase Provider => this.manager.Provider;

    /// <summary>Gets the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    public string TransactionName => this.manager.TransactionName;

    /// <summary>
    /// Gets a collection of all providers configured for this manager.
    /// </summary>
    public IEnumerable<DataProviderBase> Providers => this.manager.StaticProviders;

    /// <summary>
    /// Gets a collection of all providers configured for this manager.
    /// </summary>
    public IEnumerable<DataProviderBase> StaticProviders => this.manager.StaticProviders;

    /// <summary>Gets or sets the object container.</summary>
    /// <value>The object container.</value>
    public object ObjectContainer
    {
      get => this.manager.ObjectContainer;
      set => this.manager.ObjectContainer = value;
    }

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>New data item.</returns>
    public object CreateItem(Type itemType) => this.manager.CreateItem(itemType);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id"></param>
    /// <returns></returns>
    public object CreateItem(Type itemType, Guid id) => this.manager.CreateItem(itemType, id);

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id"></param>
    /// <returns></returns>
    public object GetItem(Type itemType, Guid id) => this.manager.GetItem(itemType, id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public object GetItemOrDefault(Type itemType, Guid id) => this.manager.GetItemOrDefault(itemType, id);

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
      return this.manager.GetItems(itemType, filterExpression, orderExpression, skip, take);
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
      return this.manager.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public void DeleteItem(object item) => this.manager.DeleteItem(item);

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="language">The language version to delete.</param>
    public void DeleteItem(object item, CultureInfo language) => this.manager.DeleteItem(item, language);

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
      return this.manager.GetPermission(permissionSet, objectId, principalId);
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public IQueryable<Permission> GetPermissions() => this.manager.GetPermissions();

    /// <summary>Creates new permission of generic type TPermission</summary>
    /// <param name="permissionSet">The name of the permission set</param>
    /// <param name="objectId">Id of the new permission</param>
    /// <param name="principalId">ID of the principal related to the permission</param>
    /// <returns>The newly created permission</returns>
    public Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.manager.CreatePermission(permissionSet, objectId, principalId);
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName"></param>
    public void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName)
    {
      this.manager.AddPermissionToObject(securedObject, permission, transactionName);
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
      this.manager.AddPermissionToObject(securedObject, managerInstance, permission, transactionName);
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public void DeletePermission(Permission permission) => this.manager.DeletePermission(permission);

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
      this.manager.CopyPermissions(source, target, sourceObjectId, targetObjectId);
    }

    /// <summary>
    /// Gets the all the secured objects which inherit permissions, through permissions hierarchy, from a secured object.
    /// </summary>
    /// <param name="root">The root object for which the permission inherirots are scanned. Must implement the ISecuredObject interface</param>
    /// <param name="permissionInheritorsOnly">if set to <c>true</c>, only the child objects for which the InheritsPermissions property is set to true are returned. Otherwise, all child objects are returned.</param>
    /// <param name="objectType">If set to null, all child objects are returned. If set to a Type, only child objects of the type are returned.</param>
    /// <returns>
    /// A List of ISecured objects which inherit permissions, through permissions hierarchy, from the root object.
    /// </returns>
    public List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType)
    {
      return this.manager.GetPermissionsInheritors(root, permissionInheritorsOnly, objectType);
    }

    /// <summary>
    /// This recursive method handles the correct behaviour when the inheritance is broken or restored on a secured object:
    /// 1. The direct permissions inheritors (children in the tree) of the object (parent), are removed all their inherited permissions.
    /// - When the inheritance on the root is broken, each child is  assigned the permissions from the parent object.
    /// - When the inheritance on the root is restored, each child is  assigned the permissions from the parent's inherited permissions.
    /// (the actions are the same, but the permissions set in the permissionsToAssignToChildren variable make the difference).
    /// 2. For each of the children, if it inherits permissions, the assignment (#1) continues recursively down to the grandchildren, and so on.
    /// 3. If down the tree, an offspring (not a direct child of the root) does not inherit permissions, the function will not continue to its children.
    /// </summary>
    /// <param name="brokenInheritanceRootObjectId"></param>
    /// <param name="brokenInheritanceRootUninheritedPermissions"></param>
    /// <param name="currentlyScannedNode">The currently scanned node in the recursion.</param>
    /// <param name="transactionName">The name of the transaction for the child object changes.</param>
    public void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenInheritanceRootObjectId,
      List<Permission> brokenInheritanceRootUninheritedPermissions,
      ISecuredObject currentlyScannedNode,
      string transactionName)
    {
      this.manager.ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(brokenInheritanceRootObjectId, brokenInheritanceRootUninheritedPermissions, currentlyScannedNode, transactionName);
    }

    /// <summary>
    /// Executes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="child">The permissions child.</param>
    public void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child) => this.manager.CreatePermissionInheritanceAssociation(parent, child);

    /// <summary>Breaks the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void BreakPermiossionsInheritance(ISecuredObject securedObject) => this.manager.BreakPermiossionsInheritance(securedObject);

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void RestorePermissionsInheritance(ISecuredObject securedObject) => this.manager.RestorePermissionsInheritance(securedObject);

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    public void SaveChanges() => this.manager.SaveChanges();

    /// <summary>
    /// Cancels any changes made to objects retrieved with this manager.
    /// </summary>
    public void CancelChanges() => this.manager.CancelChanges();

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public ISecuredObject GetSecurityRoot() => this.manager.GetSecurityRoot();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public ISecuredObject GetSecurityRoot(bool create) => this.manager.GetSecurityRoot(create);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.manager.Dispose();

    /// <summary>Recompiles the URLs of the item.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <remarks>
    /// Adds UrlData to the urls field of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> compiled from the item's Provider urlFormat.
    /// </remarks>
    public virtual void RecompileItemUrls<T>(T item) where T : ILocatable => this.content.RecompileItemUrls<T>(item);

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> item to the current URLs collection for this item.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <param name="url">The URL string value that should be added.</param>
    public virtual void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable => this.content.AddItemUrl<T>(item, url, isDefault, redirectToDefault);

    /// <summary>
    /// Removes all urls from the item satisfying the condition that is checked in the predicate function.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    public void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable => this.content.RemoveItemUrls<TItem>(item, predicate);

    /// <summary>Clears the Urls collection for this item.</summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
    public void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.content.ClearItemUrls<TItem>(item, excludeDefault);

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => this.manager.GetInheritanceMaps();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      this.manager.DeletePermissionsInheritanceMap(permissionsInheritanceMap);
    }

    /// <summary>Get url from a locatable item</summary>
    /// <param name="item">Locatable item</param>
    /// <returns>Url of the locatable item</returns>
    public string GetItemUrl(ILocatable item) => this.content.GetItemUrl(item);

    /// <summary>Retrieve a content item by its url, ignoring status</summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public IDataItem GetItemFromUrl(Type itemType, string url, out string redirectUrl) => this.content.GetItemFromUrl(itemType, url, out redirectUrl);

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.content.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <summary>Gets the items.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <returns></returns>
    public IQueryable<TItem> GetItems<TItem>() where TItem : IContent => this.content.GetItems<TItem>();

    /// <summary>Recompiles the and validate urls.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="content">The content.</param>
    public void RecompileAndValidateUrls<TContent>(TContent content) where TContent : ILocatable => this.content.RecompileAndValidateUrls<TContent>(content);

    /// <summary>Validates the URL constraints.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="urlRecompilationMethod">The URL recompilation method.</param>
    public void ValidateUrlConstraints<TContent>(TContent item) where TContent : ILocatable => this.content.ValidateUrlConstraints<TContent>(item);

    /// <inheritdoc />
    public DataProviderBase GetDefaultContextProvider() => this.StaticProviders.FirstOrDefault<DataProviderBase>() ?? this.Provider;

    /// <inheritdoc />
    public IEnumerable<DataProviderBase> GetContextProviders() => this.GetSiteProviders();

    /// <inheritdoc />
    public IEnumerable<Type> GetKnownTypes() => (IEnumerable<Type>) this.Provider.GetKnownTypes();

    private bool UseLifecycleDecorator { get; set; }
  }
}
