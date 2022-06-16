// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecoratorWrapper`3
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
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Lifecycle
{
  public class LifecycleDecoratorWrapper<TItem, TManager, TProvider> : 
    IContentManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IContentLifecycleManager<TItem>,
    IContentLifecycleManager
    where TItem : Content, ILifecycleDataItem
    where TManager : ContentManagerBase<TProvider>, ILifecycleManager
    where TProvider : ContentDataProviderBase, ILanguageDataProvider
  {
    private TManager lifecycleManager;
    private string providerName;

    public LifecycleDecoratorWrapper(string providerName)
    {
      this.providerName = providerName;
      this.Manager = (TManager) ManagerBase.GetManager(typeof (TManager), providerName);
    }

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public TItem CheckIn(TItem item) => this.Manager.Lifecycle.CheckIn((ILifecycleDataItem) item) as TItem;

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public TItem CheckOut(TItem item) => this.Manager.Lifecycle.CheckOut((ILifecycleDataItem) item) as TItem;

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public TItem Edit(TItem item) => this.Manager.Lifecycle.Edit((ILifecycleDataItem) item) as TItem;

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    public TItem Publish(TItem item) => this.Manager.Lifecycle.Publish((ILifecycleDataItem) item) as TItem;

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public TItem Unpublish(TItem item) => this.Manager.Lifecycle.Unpublish((ILifecycleDataItem) item) as TItem;

    public void Copy(TItem source, TItem destination) => this.Manager.Lifecycle.CopyProperties((ILifecycleDataItem) source, (ILifecycleDataItem) destination);

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="NewsItem">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    public TItem Schedule(TItem item, DateTime publicationDate, DateTime? expirationDate) => throw new NotSupportedException();

    public Guid GetCheckedOutBy(TItem item) => this.Manager.Lifecycle.GetCheckedOutBy((ILifecycleDataItem) item);

    public bool IsCheckedOut(TItem item) => this.Manager.Lifecycle.IsCheckedOut((ILifecycleDataItem) item);

    public bool IsCheckedOutBy(TItem item, Guid userId) => this.Manager.Lifecycle.IsCheckedOutBy((ILifecycleDataItem) item, userId);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public TItem GetLive(TItem cnt) => this.Manager.Lifecycle.GetLive((ILifecycleDataItem) cnt) as TItem;

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public TItem GetTemp(TItem cnt) => this.Manager.Lifecycle.GetTemp((ILifecycleDataItem) cnt) as TItem;

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
    public TItem GetMaster(TItem cnt) => this.Manager.Lifecycle.GetMaster((ILifecycleDataItem) cnt) as TItem;

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Content CheckIn(Content item) => (Content) this.CheckIn((TItem) item);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Content CheckOut(Content item) => (Content) this.CheckOut((TItem) item);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Content Edit(Content item) => (Content) this.Edit((TItem) item);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    public Content Publish(Content item) => (Content) this.Publish((TItem) item);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public Content Unpublish(Content item) => (Content) this.Unpublish((TItem) item);

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
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(Content item) => this.GetCheckedOutBy((TItem) item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(Content item) => this.IsCheckedOut((TItem) item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s.</param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    public bool IsCheckedOutBy(Content item, Guid userId) => this.IsCheckedOutBy((TItem) item, userId);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLive(Content cnt) => (Content) this.GetLive((TItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetTemp(Content cnt) => (Content) this.GetTemp((TItem) cnt);

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
    public Content GetMaster(Content cnt) => (Content) this.GetMaster((TItem) cnt);

    public void Copy(Content source, Content destination) => this.Copy((TItem) source, (TItem) destination);

    public DataProviderBase Provider => (DataProviderBase) this.Manager.Provider;

    public string TransactionName => this.Manager.TransactionName;

    public IEnumerable<DataProviderBase> Providers => (IEnumerable<DataProviderBase>) this.Manager.StaticProviders;

    public IEnumerable<DataProviderBase> StaticProviders => (IEnumerable<DataProviderBase>) this.Manager.StaticProviders;

    public object ObjectContainer
    {
      get => this.Manager.ObjectContainer;
      set => this.Manager.ObjectContainer = value;
    }

    public object CreateItem(Type itemType) => this.Manager.CreateItem(itemType);

    public object CreateItem(Type itemType, Guid id) => this.Manager.CreateItem(itemType, id);

    public object GetItem(Type itemType, Guid id) => this.Manager.GetItem(itemType, id);

    public object GetItemOrDefault(Type itemType, Guid id) => this.Manager.GetItemOrDefault(itemType, id);

    public IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return this.Manager.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    public IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return this.Manager.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    public void DeleteItem(object item) => this.Manager.DeleteItem(item);

    public void DeleteItem(object item, CultureInfo language) => this.Manager.DeleteItem(item, language);

    public Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Manager.GetPermission(permissionSet, objectId, principalId);
    }

    public IQueryable<Permission> GetPermissions() => this.Manager.GetPermissions();

    public Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Manager.CreatePermission(permissionSet, objectId, principalId);
    }

    public void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName)
    {
      this.Manager.AddPermissionToObject(securedObject, permission, transactionName);
    }

    public void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Permission permission,
      string transactionName)
    {
      this.Manager.AddPermissionToObject(securedObject, managerInstance, permission, transactionName);
    }

    public void DeletePermission(Permission permission) => this.Manager.DeletePermission(permission);

    public void CopyPermissions(
      IEnumerable<Permission> source,
      IList target,
      Guid sourceObjectId,
      Guid targetObjectId)
    {
      this.Manager.CopyPermissions(source, target, sourceObjectId, targetObjectId);
    }

    public List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType)
    {
      return this.Manager.GetPermissionsInheritors(root, permissionInheritorsOnly, objectType);
    }

    public void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenInheritanceRootObjectId,
      List<Permission> brokenInheritanceRootUninheritedPermissions,
      ISecuredObject currentlyScannedNode,
      string transactionName)
    {
      this.Manager.ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(brokenInheritanceRootObjectId, brokenInheritanceRootUninheritedPermissions, currentlyScannedNode, transactionName);
    }

    public void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child) => this.Manager.CreatePermissionInheritanceAssociation(parent, child);

    /// <summary>Breaks the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void BreakPermiossionsInheritance(ISecuredObject securedObject) => this.Manager.BreakPermiossionsInheritance(securedObject);

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void RestorePermissionsInheritance(ISecuredObject securedObject) => this.Manager.RestorePermissionsInheritance(securedObject);

    public void SaveChanges() => this.Manager.SaveChanges();

    public void CancelChanges() => this.Manager.CancelChanges();

    public ISecuredObject GetSecurityRoot() => this.Manager.GetSecurityRoot();

    public ISecuredObject GetSecurityRoot(bool create) => this.Manager.GetSecurityRoot(create);

    public void Dispose() => this.Manager.Dispose();

    public TManager Manager
    {
      get => this.lifecycleManager;
      set => this.lifecycleManager = value;
    }

    public string GetItemUrl(ILocatable item) => this.Manager.GetItemUrl(item);

    public IDataItem GetItemFromUrl(Type itemType, string url, out string redirectUrl) => this.Manager.GetItemFromUrl(itemType, url, out redirectUrl);

    public IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.Manager.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    public IQueryable<TItem> GetItems<TItem>() where TItem : IContent => this.Manager.GetItems<TItem>();

    public void RecompileItemUrls<TItem>(TItem item) where TItem : ILocatable => this.Manager.RecompileItemUrls<TItem>(item);

    public void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable => this.Manager.AddItemUrl<T>(item, url, isDefault, redirectToDefault);

    public void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable => this.Manager.RemoveItemUrls<TItem>(item, predicate);

    public void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.Manager.ClearItemUrls<TItem>(item, excludeDefault);

    public void RecompileAndValidateUrls<TContent>(TContent content) where TContent : ILocatable => this.Manager.RecompileAndValidateUrls<TContent>(content);

    public void ValidateUrlConstraints<TContent>(TContent item) where TContent : ILocatable => this.Manager.ValidateUrlConstraints<TContent>(item);

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => this.Manager.GetInheritanceMaps();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      this.Manager.DeletePermissionsInheritanceMap(permissionsInheritanceMap);
    }

    /// <inheritdoc />
    public DataProviderBase GetDefaultContextProvider() => this.Manager.GetDefaultContextProvider();

    /// <inheritdoc />
    public IEnumerable<DataProviderBase> GetContextProviders() => this.Manager.GetContextProviders();

    /// <inheritdoc />
    public IEnumerable<Type> GetKnownTypes() => this.Manager.GetKnownTypes();
  }
}
