// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.CommentsManagerWrapper
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
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data
{
  /// <summary>Wraps any content manager that supports comments</summary>
  [Obsolete("CommentService should be used to manage comments.")]
  public class CommentsManagerWrapper : 
    IContentManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IContentLifecycleManager<Comment>,
    IContentLifecycleManager
  {
    private IContentManager manager;
    private ContentDataProviderBase provider;
    private IContentLifecycleManager<Comment> clcManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.CommentsManagerWrapper" /> class.
    /// </summary>
    /// <param name="manager">The manager to wrap.</param>
    public CommentsManagerWrapper(IContentManager manager)
    {
      this.manager = manager;
      this.provider = manager.Provider as ContentDataProviderBase;
      if (this.provider == null)
        throw new NotSupportedException();
      this.clcManager = manager as IContentLifecycleManager<Comment>;
    }

    /// <summary>Gets the current provider</summary>
    /// <value></value>
    public DataProviderBase Provider => (DataProviderBase) this.provider;

    /// <summary>Gets the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    public string TransactionName => this.manager.TransactionName;

    /// <summary>
    /// Gets a collection of static providers configured for this manager.
    /// </summary>
    /// <value></value>
    public IEnumerable<DataProviderBase> Providers => this.manager.StaticProviders;

    /// <summary>
    /// Gets a collection of static providers configured for this manager.
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.manager.Dispose();

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Comment CheckIn(Comment item) => this.clcManager != null ? this.clcManager.CheckIn(item) : throw new NotSupportedException();

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Comment CheckOut(Comment item) => this.clcManager != null ? this.clcManager.CheckOut(item) : throw new NotSupportedException();

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Comment Edit(Comment item) => this.clcManager != null ? this.clcManager.Edit(item) : throw new NotSupportedException();

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    public Comment Publish(Comment item) => this.clcManager != null ? this.clcManager.Publish(item) : throw new NotSupportedException();

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public virtual Comment Unpublish(Comment cnt) => this.clcManager != null ? this.clcManager.Publish(cnt) : throw new NotSupportedException();

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    /// <returns>Scheduled content item</returns>
    public Comment Schedule(
      Comment item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      if (this.clcManager != null)
        return this.clcManager.Schedule(item, publicationDate, expirationDate);
      throw new NotSupportedException();
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s</param>
    /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
    public Guid GetCheckedOutBy(Comment item) => this.clcManager != null ? this.clcManager.GetCheckedOutBy(item) : throw new NotSupportedException();

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(Comment item) => this.clcManager != null ? this.clcManager.IsCheckedOut(item) : throw new NotSupportedException();

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s.</param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
    public bool IsCheckedOutBy(Comment item, Guid userId)
    {
      if (this.clcManager != null)
        return this.clcManager.IsCheckedOutBy(item, userId);
      throw new NotSupportedException();
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Comment GetLive(Comment cnt) => this.clcManager != null ? this.clcManager.GetLive(cnt) : throw new NotSupportedException();

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Comment GetTemp(Comment cnt) => this.clcManager != null ? this.clcManager.GetTemp(cnt) : throw new NotSupportedException();

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
    public Comment GetMaster(Comment cnt) => this.clcManager != null ? this.clcManager.GetMaster(cnt) : throw new NotSupportedException();

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Content CheckIn(Content item) => item is Comment ? (Content) this.CheckIn((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Content CheckOut(Content item) => item is Comment ? (Content) this.CheckOut((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Content Edit(Content item) => item is Comment ? (Content) this.Edit((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    public Content Publish(Content item) => item is Comment ? (Content) this.Publish((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public Content Unpublish(Content item) => item is Comment ? (Content) this.Unpublish((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

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
      if (item is Comment)
        return (Content) this.Schedule((Comment) item, publicationDate, expirationDate);
      throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(Content item) => item is Comment ? this.GetCheckedOutBy((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(Content item) => item is Comment ? this.IsCheckedOut((Comment) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s.</param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    public bool IsCheckedOutBy(Content item, Guid userId) => item is Comment ? this.IsCheckedOutBy((Comment) item, userId) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLive(Content cnt) => cnt is Comment ? (Content) this.GetLive((Comment) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetTemp(Content cnt) => cnt is Comment ? (Content) this.GetTemp((Comment) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

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
    public Content GetMaster(Content cnt) => cnt is Comment ? (Content) this.GetMaster((Comment) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

    public void Copy(Comment source, Comment destination)
    {
    }

    public void Copy(Content source, Content destination)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      Type type;
      if ((type = source.GetType()) != destination.GetType())
        throw new ArgumentException("source and destination must be of the same type");
      if (type == typeof (Comment))
        this.Copy((Comment) source, (Comment) destination);
      else
        throw new NotSupportedException("Type {0} is not supported".Arrange((object) type));
    }

    /// <summary>Gets the item URL.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public string GetItemUrl(ILocatable item) => this.manager.GetItemUrl(item);

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    public IDataItem GetItemFromUrl(Type itemType, string url, out string redirectUrl) => this.manager.GetItemFromUrl(itemType, url, out redirectUrl);

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    public IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.manager.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <summary>Gets the items.</summary>
    /// <returns></returns>
    public IQueryable<TItem> GetItems<TItem>() where TItem : IContent => this.manager.GetItems<TItem>();

    /// <summary>Recompiles the item urls.</summary>
    /// <param name="item">The item.</param>
    public void RecompileItemUrls<TItem>(TItem item) where TItem : ILocatable => this.manager.RecompileItemUrls<TItem>(item);

    /// <summary>Adds the item URL.</summary>
    /// <param name="item">The item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="isDefault">The is default.</param>
    /// <param name="redirectToDefault">The redirect to default.</param>
    public void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable => this.manager.AddItemUrl<T>(item, url, isDefault, redirectToDefault);

    /// <summary>Removes the item urls.</summary>
    /// <param name="item">The item.</param>
    /// <param name="predicate">The predicate.</param>
    public void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable => this.manager.RemoveItemUrls<TItem>(item, predicate);

    /// <summary>Clears the item urls.</summary>
    /// <param name="item">The item.</param>
    /// <param name="excludeDefault">The exclude default.</param>
    public void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.manager.ClearItemUrls<TItem>(item, excludeDefault);

    /// <summary>Recompiles the and validate urls.</summary>
    /// <param name="content">The content.</param>
    public void RecompileAndValidateUrls<TContent>(TContent content) where TContent : ILocatable => this.manager.RecompileAndValidateUrls<TContent>(content);

    /// <summary>Validates the URL constraints.</summary>
    /// <param name="item">The item.</param>
    public void ValidateUrlConstraints<TContent>(TContent item) where TContent : ILocatable => this.manager.ValidateUrlConstraints<TContent>(item);

    /// <inheritdoc />
    public DataProviderBase GetDefaultContextProvider() => this.manager.GetDefaultContextProvider();

    /// <inheritdoc />
    public IEnumerable<DataProviderBase> GetContextProviders() => this.manager.GetContextProviders();

    /// <inheritdoc />
    public IEnumerable<Type> GetKnownTypes() => this.manager.GetKnownTypes();
  }
}
