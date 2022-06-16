// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.DataProcessing;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Base class for data providers.</summary>
  public abstract class DataProviderBase : 
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    ISecuredObject,
    IDataItemProvider,
    IDataProviderInfo
  {
    private bool? filterQueriesByViewPermissions;
    private string notificationKey;
    private LocalDataStoreSlot notificationSlot;
    private string eventsKey;
    private LocalDataStoreSlot eventsSlot;
    private string securityKey;
    private string relatedManagersKey;
    private LocalDataStoreSlot securitySlot;
    internal const string ValidateOnCommitKey = "sf-data-validate-commit";
    private LocalDataStoreSlot dataStoreSlot;
    private string suppressValidationOnCommitKey;
    private IDataProcessingEngine integrityChecker;
    private LocalDataStoreSlot relatedManagersSlot;
    private const string relatedMangersTransactionName = "sf_tran_relatedManagers";
    private string moduleName;
    private string description;
    private bool initialized;
    private bool secRootSet;
    private readonly object secRootLockObject = new object();
    private string name;
    private Type managerType;
    private SecuredProxy secRoot;
    private string applicationName;
    private string providerGroup;
    protected IDataProviderDecorator providerDecorator;
    private ProviderAbilities abilities = new ProviderAbilities();
    private bool disposed;
    private string[] supportedPermissionSets = new string[1]
    {
      "General"
    };
    private IDictionary<string, string> permissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>();
    private IDictionary<TypeAction, List<TransactionPermissionAttribute>> transactionPermissionAttributeCache = SystemManager.CreateStaticCache<TypeAction, List<TransactionPermissionAttribute>>();
    private const string ItemsStateKey = "ItemsStateKey";
    internal const string IsRollbackedExecutionStateKey = "IsRollbacked";
    private string title;
    private Dictionary<string, bool> typeCacheEnabled = new Dictionary<string, bool>();
    internal const string RecompiledDependentItemsTypeKey = "RecompiledAdditionalItemsTypeKey";
    private const int MaxCollectionSize = 500;

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    public virtual string Name => this.name;

    /// <summary>
    /// Gets or sets the name of the application to store and retrieve role information for.
    /// </summary>
    /// <returns>
    /// The name of the application to store and retrieve role information for.
    /// </returns>
    public virtual string ApplicationName
    {
      get => this.applicationName;
      private set => this.applicationName = value;
    }

    /// <summary>
    /// Gets or sets an optional group provider of this provider (e.g System providers)
    /// </summary>
    public virtual string ProviderGroup
    {
      get => this.providerGroup;
      private set => this.providerGroup = value;
    }

    /// <summary>Gets the title.</summary>
    /// <value>The title.</value>
    public virtual string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.title))
          return this.name;
        return !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, this.title) : this.title;
      }
    }

    /// <summary>
    /// Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).
    /// </summary>
    /// <returns>
    /// A brief, friendly description suitable for display in administrative tools or other UIs.
    /// </returns>
    public virtual string Description
    {
      get
      {
        if (string.IsNullOrEmpty(this.description))
          return this.name;
        return !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, this.description) : this.description;
      }
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    public string ResourceClassId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to suppress cache dependency notifications.
    /// Notifications can be suppressed for better performance when performing bulk operations.
    /// After completing the bulk operations, the property should be set to false again and CacheDependency.NotifyAll()
    /// should be called to invalidate all cached data.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if notifications are suppressed; otherwise, <c>false</c>.
    /// </value>
    public bool SuppressNotifications
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(this.NotificationSlot) : SystemManager.CurrentHttpContext.Items[(object) this.NotificationKey];
        return obj != null && (bool) obj;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) this.NotificationKey] = (object) value;
        else
          Thread.SetData(this.NotificationSlot, (object) value);
      }
    }

    internal bool FilterQueriesByViewPermissions
    {
      get => this.filterQueriesByViewPermissions.HasValue ? this.filterQueriesByViewPermissions.Value : Config.Get<SecurityConfig>().FilterQueriesByViewPermissions;
      set => this.filterQueriesByViewPermissions = new bool?(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the model must be updated after commiting the translction.
    /// </summary>
    /// <value>The update model.</value>
    [Obsolete]
    public bool ResetModel { get; set; }

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    public virtual bool Suspended => this.providerDecorator.Suspended;

    private string NotificationKey
    {
      get
      {
        if (this.notificationKey == null)
          this.notificationKey = "sfNK" + (object) this.GetHashCode();
        return this.notificationKey;
      }
    }

    private LocalDataStoreSlot NotificationSlot
    {
      get
      {
        if (this.notificationSlot == null)
          this.notificationSlot = Thread.AllocateDataSlot();
        return this.notificationSlot;
      }
    }

    internal virtual bool SuppressEvents
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(this.EventsSlot) : SystemManager.CurrentHttpContext.Items[(object) this.EventsKey];
        return obj != null && (bool) obj;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) this.EventsKey] = (object) value;
        else
          Thread.SetData(this.EventsSlot, (object) value);
      }
    }

    internal Type TheManagerType => this.managerType;

    private string EventsKey
    {
      get
      {
        if (this.eventsKey == null)
          this.eventsKey = "sfEK" + (object) this.GetHashCode();
        return this.eventsKey;
      }
    }

    private LocalDataStoreSlot EventsSlot
    {
      get
      {
        if (this.eventsSlot == null)
          this.eventsSlot = Thread.AllocateDataSlot();
        return this.eventsSlot;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to suppress all security checks for this provider.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if security checks are suppressed; otherwise, <c>false</c>.
    /// </value>
    public virtual bool SuppressSecurityChecks
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(this.SecuritySlot) : SystemManager.CurrentHttpContext.Items[(object) this.SecurityKey];
        return obj != null && (bool) obj;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) this.SecurityKey] = (object) value;
        else
          Thread.SetData(this.SecuritySlot, (object) value);
      }
    }

    /// <summary>
    /// Suppresses the security checks of this provider, executes <paramref name="action" /> and restores the settings.
    /// </summary>
    /// <param name="action">A delegate to be executed with suppressed security checks.</param>
    public void WithSuppressedSecurityChecks(Action action)
    {
      bool suppressSecurityChecks = this.SuppressSecurityChecks;
      try
      {
        this.SuppressSecurityChecks = true;
        action();
      }
      finally
      {
        this.SuppressSecurityChecks = suppressSecurityChecks;
      }
    }

    /// <summary>Gets the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    public string TransactionName { get; internal set; }

    private string SecurityKey
    {
      get
      {
        if (this.securityKey == null)
          this.securityKey = "sfSK" + (object) this.GetHashCode();
        return this.securityKey;
      }
    }

    private LocalDataStoreSlot SecuritySlot
    {
      get
      {
        if (this.securitySlot == null)
          this.securitySlot = Thread.AllocateDataSlot();
        return this.securitySlot;
      }
    }

    /// <summary>
    /// Gets the provider abilities for the current principal. E.g. which operations are supported and allowed.
    /// </summary>
    /// <value>The provider abilities.</value>
    public virtual ProviderAbilities Abilities => this.abilities;

    public string CurrentTaxonomyProperty { get; set; }

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    [Obsolete("In case your provider is IOpenAccessDataProvider implement IOpenAccessUpgradableProvider, otherwise add the upgrade logic in the module")]
    public virtual bool CheckForUpdates => false;

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public virtual string ModuleName
    {
      get
      {
        if (this.moduleName == null)
        {
          Type type = this.GetType();
          if (type.Assembly.IsDynamic)
            type = type.BaseType;
          this.moduleName = !(type != (Type) null) ? this.Name : type.FullName;
        }
        return this.moduleName;
      }
    }

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <returns>New data item.</returns>
    [ApplyNoPolicies]
    public virtual T CreateItem<T>() where T : class, IDataItem => (T) this.CreateItem(typeof (T));

    /// <summary>Creates new data item.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual T CreateItem<T>(Guid id) where T : class, IDataItem => (T) this.CreateItem(typeof (T), id);

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>New data item.</returns>
    [ApplyNoPolicies]
    public virtual object CreateItem(Type itemType) => this.CreateItem(itemType, this.GetNewGuid());

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>Gets the data item with the specified ID.</summary>
    /// <typeparam name="T">The type of the data item.</typeparam>
    /// <param name="id">The ID of the data item.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual object GetItem<T>(Guid id) where T : class, IDataItem => this.GetItem(typeof (T), id);

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual object GetItem(Type itemType, Guid id)
    {
      if (itemType == typeof (Telerik.Sitefinity.Security.Model.SecurityRoot) || itemType == typeof (SecuredProxy))
      {
        ISecuredObject securityRoot = this.GetSecurityRoot(true);
        if (securityRoot != null && securityRoot.Id == id)
          return (object) securityRoot;
      }
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    [ApplyNoPolicies]
    public virtual object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == typeof (Telerik.Sitefinity.Security.Model.SecurityRoot) || itemType == typeof (SecuredProxy))
      {
        ISecuredObject securityRoot = this.GetSecurityRoot(true);
        if (securityRoot != null && securityRoot.Id == id)
          return (object) securityRoot;
      }
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Retrieves a collection of items of the specified type form the data store.
    /// </summary>
    /// <param name="itemType">The type of the items to be retrieved.</param>
    /// <param name="filterExpression">Defines filter expression to be applied.</param>
    /// <param name="orderExpression">Specifies the order of the items in the collection.</param>
    /// <param name="skip">The number of items to skip from the beginning of the result.</param>
    /// <param name="take">The number or items take in the collection from the result.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      int? totalCount = new int?();
      return this.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotSupportedException();
    }

    /// <summary>Changes the order position of the item</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The item id.</param>
    /// <param name="newPosition">New position.</param>
    [ApplyNoPolicies]
    public virtual void ReorderItem<T>(Guid id, float newPosition) => this.ReorderItem(typeof (T), id, newPosition);

    /// <summary>Changes the order position of the item</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The item id.</param>
    /// <param name="newPosition">New position.</param>
    public virtual void ReorderItem(Type itemType, Guid id, float newPosition)
    {
      if (!typeof (IOrderedItem).IsAssignableFrom(itemType))
        throw new ArgumentException(string.Format("{0} must implement IOrderedITem.", (object) itemType.FullName));
      if (!(this.GetItem(itemType, id) is IOrderedItem orderedItem))
        throw new ArgumentException(string.Format("There is no item of type {0} with the specified Id {1}", (object) itemType.FullName, (object) id));
      orderedItem.Ordinal = newPosition;
    }

    /// <summary>Changes the order position of the item</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The item id.</param>
    /// <param name="oldPosition">Old position.</param>
    /// <param name="newPosition">New position.</param>
    /// <param name="insertBefore">if set to <c>true</c> the item will be placed before the item at the newPosition.
    /// if set to <c>false</c> it will be appended after the item at the newPosition.
    /// </param>
    [ApplyNoPolicies]
    public virtual void ReorderItem<T>(
      Guid id,
      float oldPosition,
      float newPosition,
      bool insertBefore)
      where T : class, IDataItem, IOrderedItem
    {
      this.ReorderItem(typeof (T), id, oldPosition, newPosition, insertBefore);
    }

    /// <summary>Changes the order position of the item</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The item id.</param>
    /// <param name="oldPosition">Old position.</param>
    /// <param name="newPosition">New position.</param>
    /// <remarks>The item will be appended after the item at the newPosition.</remarks>
    [ApplyNoPolicies]
    public virtual void ReorderItem<T>(Guid id, float oldPosition, float newPosition) where T : class, IDataItem, IOrderedItem => this.ReorderItem(typeof (T), id, oldPosition, newPosition, false);

    /// <summary>Changes the order position of the item</summary>
    /// <param name="itemType">Type of the item. Must implement IOrderedItem</param>
    /// <param name="id">The item id.</param>
    /// <param name="oldPosition">Old position.</param>
    /// <param name="newPosition">New position.</param>
    [ApplyNoPolicies]
    public virtual void ReorderItem(Type itemType, Guid id, float oldPosition, float newPosition) => this.ReorderItem(itemType, id, oldPosition, newPosition, false);

    /// <summary>Changes the order position of the item</summary>
    /// <param name="itemType">Type of the item. Must implement IOrderedItem</param>
    /// <param name="id">The item id.</param>
    /// <param name="oldPosition">Old position.</param>
    /// <param name="newPosition">New position.</param>
    public virtual void ReorderItem(
      Type itemType,
      Guid id,
      float oldPosition,
      float newPosition,
      bool insertBefore)
    {
      if ((double) oldPosition == (double) newPosition)
        return;
      if (!typeof (IOrderedItem).IsAssignableFrom(itemType))
        throw new ArgumentException(string.Format("{0} must implement IOrderedITem.", (object) itemType.FullName));
      if (!(this.GetItem(itemType, id) is IOrderedItem orderedItem))
        throw new ArgumentException(string.Format("There is no item of type {0} with the specified Id {1}", (object) itemType.FullName, (object) id));
      string filterExpression;
      string orderExpression;
      if (insertBefore)
      {
        filterExpression = string.Format("Ordinal <= {0}", (object) newPosition.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        orderExpression = "Ordinal DESC";
      }
      else
      {
        filterExpression = string.Format("Ordinal >= {0}", (object) newPosition.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        orderExpression = "Ordinal ASC";
      }
      IEnumerable<IOrderedItem> source = this.GetItems(itemType, filterExpression, orderExpression, 0, 2).Cast<IOrderedItem>();
      IEnumerator<IOrderedItem> enumerator = source.Count<IOrderedItem>() >= 1 ? source.GetEnumerator() : throw new ArgumentException(string.Format("There is no item of type {0} with the specified Ordinal value of: {1}", (object) itemType.FullName, (object) newPosition));
      enumerator.MoveNext();
      IOrderedItem current = enumerator.Current;
      IOrderedItem nextAdjacentItem = (IOrderedItem) null;
      if (enumerator.MoveNext())
        nextAdjacentItem = enumerator.Current;
      orderedItem.SetOrdinalBetweenItems(current, nextAdjacentItem, insertBefore);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    [ApplyNoPolicies]
    public virtual void DeleteItem(object item) => throw new NotSupportedException();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    [ApplyNoPolicies]
    public abstract Type[] GetKnownTypes();

    private IEnumerable<string> GetKnownSecuredObjectsTypes() => ((IEnumerable<Type>) this.GetKnownTypes()).Where<Type>((Func<Type, bool>) (t => typeof (ISecuredObject).IsAssignableFrom(t))).Select<Type, string>((Func<Type, string>) (t => this.ConvertClrTypeVoaClass(t)));

    private bool IsInTransaction => !string.IsNullOrEmpty(this.TransactionName);

    /// <summary>Commits the provided transaction.</summary>
    public virtual void CommitTransaction()
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "CommitTransaction()");
      IList dirtyItems = this.GetDirtyItems();
      if (!this.IsInTransaction && dirtyItems.Count > 0 && this.ShouldValidateOnCommit())
        this.ValidateOnCommitting(dirtyItems);
      ICollection<IEvent> afterCommitEvents = (ICollection<IEvent>) null;
      IDataEventProvider dataEventProvider;
      if (this.ShouldRaiseDataEvents(out dataEventProvider))
      {
        this.RaiseBeforeCommitEvents(dataEventProvider);
        afterCommitEvents = this.GetDirtyItemsStateData();
        if (afterCommitEvents == null)
          afterCommitEvents = this.GetDataEventItems(new Func<IDataItem, bool>(dataEventProvider.ApplyDataEventItemFilter));
      }
      this.providerDecorator.CommitTransaction();
      this.DoWithRelatedManagers((Action<IManager>) (m => m.Provider.CommitTransaction()));
      if (afterCommitEvents == null || afterCommitEvents.Count<IEvent>() <= 0 || this.IsInTransaction && TransactionManager.TryAddPostCommitAction(this.TransactionName, (Action) (() => this.RaiseAfterEvents(afterCommitEvents))))
        return;
      this.RaiseAfterEvents(afterCommitEvents);
    }

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    public virtual void FlushTransaction()
    {
      IList dirtyItems = this.GetDirtyItems();
      if (!this.IsInTransaction && dirtyItems.Count > 0 && this.ShouldValidateOnCommit())
        this.ValidateOnCommitting(dirtyItems);
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "CommitTransaction()");
      this.AccumulateDirtyItemsStateData();
      this.providerDecorator.FlushTransaction();
      this.DoWithRelatedManagers((Action<IManager>) (m => m.Provider.FlushTransaction()));
    }

    [ApplyNoPolicies]
    protected virtual void AccumulateDirtyItemsStateData()
    {
      IDataEventProvider dataEventProvider;
      if (!this.ShouldRaiseDataEvents(out dataEventProvider))
        return;
      this.RaiseBeforeCommitEvents(dataEventProvider);
      ICollection<IEvent> events = this.GetDataEventItems(new Func<IDataItem, bool>(dataEventProvider.ApplyDataEventItemFilter));
      if (events == null || !events.Any<IEvent>())
        return;
      ICollection<IEvent> dirtyItemsStateData = this.GetDirtyItemsStateData();
      if (dirtyItemsStateData != null && dirtyItemsStateData.Any<IEvent>())
      {
        foreach (IEvent @event in (IEnumerable<IEvent>) events)
          dirtyItemsStateData.Add(@event);
        events = dirtyItemsStateData;
      }
      this.SetExecutionStateData("ItemsStateKey", (object) events);
    }

    private void RaiseBeforeCommitEvents(IDataEventProvider dataEventProvider)
    {
      IEnumerable<IEvent> beforeCommitEvents = this.GetBeforeCommitEvents(new Func<IDataItem, bool>(dataEventProvider.ApplyDataEventItemFilter));
      if (beforeCommitEvents == null || beforeCommitEvents.Count<IEvent>() <= 0)
        return;
      this.RaiseBeforeEvents(beforeCommitEvents);
    }

    [ApplyNoPolicies]
    protected virtual ICollection<IEvent> GetDirtyItemsStateData() => this.GetExecutionStateData("ItemsStateKey") as ICollection<IEvent>;

    [ApplyNoPolicies]
    protected virtual void RaiseBeforeEvent(IEvent preEvent)
    {
      preEvent.Origin = this.GetEventOrigin();
      EventHub.Raise(preEvent, true);
    }

    [ApplyNoPolicies]
    protected virtual void RaiseBeforeEvents(IEnumerable<IEvent> events)
    {
      if (events == null)
        throw new ArgumentNullException(nameof (events));
      foreach (IEvent @event in events)
      {
        @event.Origin = this.GetEventOrigin();
        EventHub.Raise(@event, true);
      }
    }

    [ApplyNoPolicies]
    protected virtual void RaiseAfterEvents(ICollection<IEvent> events)
    {
      if (this.ShouldRaiseDataEvents(out IDataEventProvider _) && events != null && events.Any<IEvent>())
      {
        foreach (IEvent @event in (IEnumerable<IEvent>) events)
        {
          @event.Origin = this.GetEventOrigin();
          EventHub.Raise(@event, false);
        }
      }
      SystemManager.CurrentHttpContext.Items.Remove((object) "RecompiledAdditionalItemsTypeKey");
    }

    [ApplyNoPolicies]
    protected string GetEventOrigin()
    {
      object eventOrigin;
      this.TryGetExecutionStateData("EventOriginKey", out eventOrigin);
      return (string) eventOrigin;
    }

    [ApplyNoPolicies]
    protected virtual IEnumerable<IEvent> GetBeforeCommitEvents(
      Func<IDataItem, bool> filterPredicate)
    {
      return Enumerable.Empty<IEvent>();
    }

    /// <summary>Gets the data event items that will be raised.</summary>
    /// <param name="filterPredicate">The filter predicate.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    protected virtual ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      try
      {
        IList dirtyItems = this.GetDirtyItems();
        List<IEvent> dataEventItems = new List<IEvent>(dirtyItems.Count);
        foreach (object itemInTransaction in (IEnumerable) dirtyItems)
        {
          IEvent @event = this.TryHandleCustomEvent(itemInTransaction);
          if (@event != null)
            dataEventItems.Add(@event);
          else if (itemInTransaction is IDataItem dataItem && filterPredicate(dataItem))
          {
            if (dataItem.Transaction == null && !string.IsNullOrEmpty(this.TransactionName))
              dataItem.Transaction = (object) this.TransactionName;
            SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus(itemInTransaction);
            if (dataItem.Provider == null)
              dataItem.Provider = (object) this;
            this.AddEvents((ICollection<IEvent>) dataEventItems, dataItem, dirtyItemStatus);
          }
          this.ClearTrackingContext(itemInTransaction);
        }
        return (ICollection<IEvent>) dataEventItems;
      }
      catch (Exception ex)
      {
        this.RollbackTransaction();
        throw ex;
      }
    }

    /// <summary>
    /// Clears the tracking context of an item if it implements the <see cref="T:Telerik.Sitefinity.Model.IHasTrackingContext" />.
    /// </summary>
    /// <param name="item">The item which tracking context should be cleared.</param>
    [ApplyNoPolicies]
    protected internal virtual void ClearTrackingContext(object item)
    {
      if (!(item is IHasTrackingContext context))
        return;
      context.Clear();
    }

    protected virtual IEvent TryHandleCustomEvent(object item) => (IEvent) null;

    protected virtual void AddEvents(
      ICollection<IEvent> events,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType)
    {
      List<string> source = new List<string>();
      if (dataItem is IHasTrackingContext context)
      {
        source = context.GetLanguages();
        if (context.HasOperation(OperationStatus.Deleted) || context.HasOperation(OperationStatus.MovedToRecycleBin) || context.HasOperation(OperationStatus.MovedToRecycleBinWithParent))
          actionType = SecurityConstants.TransactionActionType.Deleted;
      }
      if (source.Any<string>())
      {
        foreach (string language in source)
        {
          IEvent eventItem = this.CreateEventItem(dataItem, actionType.ToString(), language);
          events.Add(eventItem);
        }
      }
      else
      {
        IEvent eventItem = this.CreateEventItem(dataItem, actionType.ToString());
        events.Add(eventItem);
      }
      if (!(dataItem is IGeoLocatable))
        return;
      IEvent @event = GeoLocatableEventFactory.CreateEvent((IGeoLocatable) dataItem, actionType);
      if (@event == null)
        return;
      events.Add(@event);
    }

    internal void ValidateOnCommittingInTransaction()
    {
      IList dirtyItems = this.GetDirtyItems();
      if (dirtyItems.Count <= 0 || !this.ShouldValidateOnCommit())
        return;
      this.ValidateOnCommitting(dirtyItems);
    }

    protected virtual void ValidateOnCommitting(IList dirtyItems)
    {
      if (!Bootstrapper.FinalEventsExecuted)
        return;
      this.DataProcessingEngine.Process(this, dirtyItems);
    }

    internal virtual IDataProcessingEngine DataProcessingEngine
    {
      get
      {
        if (this.integrityChecker == null)
          this.integrityChecker = (IDataProcessingEngine) new Telerik.Sitefinity.Data.DataProcessing.DataProcessingEngine();
        return this.integrityChecker;
      }
    }

    /// <summary>
    /// Suppresses the validation checks of this provider, executes <paramref name="action" /> and restores the settings.
    /// </summary>
    /// <param name="action">A delegate to be executed with suppressed validation checks.</param>
    internal void WithSuppressedValidationOnCommit(Action action)
    {
      bool validationOnCommit = this.SuppressValidationOnCommit;
      try
      {
        this.SuppressValidationOnCommit = true;
        action();
      }
      finally
      {
        this.SuppressValidationOnCommit = validationOnCommit;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to suppress all validations
    /// during flush/commit for this provider.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if validation checks are suppressed; otherwise, <c>false</c>.
    /// </value>
    internal virtual bool SuppressValidationOnCommit
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(this.DataStoreSlot) : SystemManager.CurrentHttpContext.Items[(object) this.SuppressValidationOnCommitKey];
        return obj != null && (bool) obj;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) this.SuppressValidationOnCommitKey] = (object) value;
        else
          Thread.SetData(this.DataStoreSlot, (object) value);
      }
    }

    internal virtual bool ShouldValidateOnCommit() => Bootstrapper.IsReady && !this.SuppressValidationOnCommit;

    internal virtual IEvent CreateEventItem(
      IDataItem dataItem,
      string action,
      string language = null)
    {
      return (IEvent) DataEventFactory.CreateEvent(dataItem, action, language);
    }

    private string SuppressValidationOnCommitKey
    {
      get
      {
        if (this.suppressValidationOnCommitKey == null)
          this.suppressValidationOnCommitKey = "sf-data-validate-commit" + (object) this.GetHashCode();
        return this.suppressValidationOnCommitKey;
      }
    }

    private LocalDataStoreSlot DataStoreSlot
    {
      get
      {
        if (this.dataStoreSlot == null)
          this.dataStoreSlot = Thread.AllocateDataSlot();
        return this.dataStoreSlot;
      }
    }

    [ApplyNoPolicies]
    internal bool ShouldRaiseDataEvents(out IDataEventProvider dataEventProvider)
    {
      dataEventProvider = this as IDataEventProvider;
      return !SystemManager.Initializing && dataEventProvider != null && dataEventProvider.DataEventsEnabled && !this.SuppressEvents;
    }

    /// <summary>Aborts the transaction for the current scope.</summary>
    public virtual void RollbackTransaction()
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "RollbackTransaction()");
      this.providerDecorator.RollbackTransaction();
      this.DoWithRelatedManagers((Action<IManager>) (m => m.Provider.RollbackTransaction()));
      this.SetExecutionStateData("IsRollbacked", (object) true);
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items.Remove((object) "RecompiledAdditionalItemsTypeKey");
    }

    /// <summary>
    /// Sets a pessimistic lock for read on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public virtual void LockTransactionForRead(object target)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "LockTransactionForRead(target)");
      this.providerDecorator.LockTransactionForRead(target);
    }

    /// <summary>
    /// Sets a pessimistic lock for write on the specified object.
    /// </summary>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public virtual void LockTransactionForWrite(object target)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "LockTransactionForWrite(target)");
      this.providerDecorator.LockTransactionForWrite(target);
    }

    /// <summary>Gets the dirty items from the current transaction.</summary>
    /// <returns>An array of dirty items.</returns>
    [ApplyNoPolicies]
    public virtual IList GetDirtyItems() => this.providerDecorator != null ? this.providerDecorator.GetDirtyItems() : throw new MissingDecoratorException(this, "GetDirtyItems()");

    /// <summary>
    /// Gets the status of a dirty item in a transaction: new/updated/removed.
    /// </summary>
    /// <param name="itemInTransaction">The item in transaction to check.</param>
    /// <returns>The status of the item</returns>
    public SecurityConstants.TransactionActionType GetDirtyItemStatus(
      object itemInTransaction)
    {
      return this.providerDecorator != null ? this.providerDecorator.GetDirtyItemStatus(itemInTransaction) : throw new MissingDecoratorException(this, "GetDirtyItemStatus(itemInTransaction)");
    }

    /// <summary>
    /// Determines whether the specifield field of the given persisted object is changed.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public bool IsFieldDirty(object entity, string fieldName)
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.IsFieldDirty(entity, fieldName);
      throw new MissingDecoratorException(this, "IsFieldDirty()");
    }

    /// <summary>Gets the original value.</summary>
    /// <param name="entity">The entity.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public T GetOriginalValue<T>(object entity, string propertyName)
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.GetOriginalValue<T>(entity, propertyName);
      throw new MissingDecoratorException(this, "GetOriginalValue()");
    }

    internal bool IsLstringFieldDirty(
      object entity,
      string fieldName,
      out string[] cultures,
      string[] culturesToCheck = null)
    {
      if (TypeDescriptor.GetProperties(entity)[fieldName] is LstringPropertyDescriptor property)
      {
        List<string> stringList = new List<string>();
        culturesToCheck = culturesToCheck ?? ((IEnumerable<CultureInfo>) property.GetAvailableLanguages(entity)).Select<CultureInfo, string>((Func<CultureInfo, string>) (s => s.Name)).ToArray<string>();
        foreach (KeyValuePair<string, PropertyDescriptor> localProperty in property.GetLocalProperties(entity, (IEnumerable<string>) culturesToCheck))
        {
          MemberDescriptor memberDescriptor = (MemberDescriptor) localProperty.Value;
          if (this.IsFieldDirty(entity, memberDescriptor.Name))
            stringList.Add(localProperty.Key);
        }
        if (stringList.Count > 0)
        {
          cultures = stringList.ToArray();
          return true;
        }
      }
      cultures = (string[]) null;
      return false;
    }

    internal string GetLstringOriginalValue(
      object entity,
      string propertyName,
      CultureInfo culture)
    {
      return TypeDescriptor.GetProperties(entity)[propertyName] is LstringPropertyDescriptor property ? property.GetString(entity, culture, false) : string.Empty;
    }

    /// <summary>
    /// When overridden this method returns new transaction object.
    /// </summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>The transaction object.</returns>
    [ApplyNoPolicies]
    protected internal virtual object CreateNewTransaction(string transactionName) => this.providerDecorator != null ? this.providerDecorator.CreateNewTransaction(transactionName) : throw new MissingDecoratorException(this, "CreateNewTransaction()");

    /// <summary>Gets the transaction for the current scope.</summary>
    [ApplyNoPolicies]
    protected internal virtual object GetTransaction()
    {
      if (this.disposed)
        throw new ObjectDisposedException(this.GetType().FullName);
      if (!string.IsNullOrEmpty(this.TransactionName))
        return this.CreateNewTransaction(this.TransactionName);
      IManager manager = ManagerBase.ResolveManager(this.managerType, this.Name);
      return manager.ObjectContainer ?? (manager.ObjectContainer = this.CreateNewTransaction((string) null));
    }

    /// <summary>Generates a new GUID.</summary>
    /// <returns></returns>
    [ApplyNoPolicies]
    protected internal virtual Guid GetNewGuid() => this.providerDecorator != null ? this.providerDecorator.GetNewGuid() : throw new MissingDecoratorException(this, "NewGuid()");

    /// <summary>Sets the expressions.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="totalCount">Total count of items in the query passing the filter expression</param>
    /// <returns></returns>
    public static IQueryable<T> SetExpressions<T>(
      IQueryable<T> query,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      if (!string.IsNullOrEmpty(filterExpression))
      {
        string filterName;
        query = !typeof (IDataItem).IsAssignableFrom(typeof (T)) || !NamedFiltersHandler.TryParseFilterName(filterExpression, out filterName) ? query.Where<T>(filterExpression) : Queryable.Cast<T>(NamedFiltersHandler.ApplyStatusProviderFilter<IDataItem>(query as IQueryable<IDataItem>, filterName));
      }
      if (totalCount.HasValue)
        totalCount = new int?(Queryable.Count<T>(query));
      if (totalCount.HasValue)
      {
        int? nullable = totalCount;
        int num = 0;
        if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
          return Enumerable.Empty<T>().AsQueryable<T>();
      }
      if (!string.IsNullOrEmpty(orderExpression))
        query = query.OrderBy<T>(orderExpression);
      if (skip.HasValue && skip.Value > 0)
        query = Queryable.Skip<T>(query, skip.Value);
      if (take.HasValue && take.Value > 0)
        query = Queryable.Take<T>(query, take.Value);
      return query;
    }

    /// <summary>
    /// Filters the items by the specified radius of the point with the specified coordinates. The items may also be filtered by provider, type and custom key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="itemsList">The items list.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="geoLocationsList">The geo locations list.</param>
    /// <param name="itemFilter">The item filter.</param>
    internal IQueryable<T> FilterByGeoLocation<T>(
      IQueryable<T> itemsList,
      double latitude,
      double longitude,
      double radius,
      out IEnumerable<IGeoLocation> geoLocationsList,
      ItemFilter itemFilter = null)
      where T : IDataItem
    {
      IGeoLocationService geoLocationService = SystemManager.GetGeoLocationService();
      geoLocationsList = geoLocationService.GetLocationsInCircle(latitude, longitude, radius, itemFilter: itemFilter);
      geoLocationsList = geoLocationsList.Count<IGeoLocation>() > 500 ? geoLocationsList.Take<IGeoLocation>(500) : geoLocationsList;
      Guid[] geoLocations = geoLocationsList.Select<IGeoLocation, Guid>((Func<IGeoLocation, Guid>) (i => i.ContentItemId)).ToArray<Guid>();
      return itemsList.Where<T>((Expression<Func<T, bool>>) (i => geoLocations.Contains<Guid>(i.Id)));
    }

    /// <summary>
    /// Sorts items by distance to the point with the specified coordinates
    /// </summary>
    /// <param name="itemsList">The items list.</param>
    /// <param name="geoLocationsList">The geo locations list.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="distanceSorting">The distance sorting.</param>
    internal IEnumerable<T> SortByDistance<T>(
      IEnumerable<T> itemsList,
      IEnumerable<IGeoLocation> geoLocationsList,
      double latitude,
      double longitude,
      DistanceSorting distanceSorting)
      where T : IGeoLocationDistance
    {
      GeoLocationsHelper.PopulateDistance<T>(itemsList, geoLocationsList, latitude, longitude);
      return distanceSorting == DistanceSorting.Desc ? (IEnumerable<T>) itemsList.OrderByDescending<T, double>((Func<T, double>) (i => i.Distance)) : (IEnumerable<T>) itemsList.OrderBy<T, double>((Func<T, double>) (i => i.Distance));
    }

    /// <summary>Sets the expressions.</summary>
    /// <param name="query">The query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="totalCount">Total count of items in the query passing the filter expression</param>
    /// <returns></returns>
    public static IQueryable SetExpressions(
      IQueryable query,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      if (!string.IsNullOrEmpty(filterExpression))
        query = query.Where(filterExpression);
      if (totalCount.HasValue)
        totalCount = new int?(query.Count());
      if (!string.IsNullOrEmpty(orderExpression))
        query = query.OrderBy(orderExpression);
      if (skip.HasValue && skip.Value > 0)
        query = query.Skip(skip.Value);
      if (take.HasValue && take.Value > 0)
        query = query.Take(take.Value);
      return query;
    }

    /// <summary>
    /// Throws an exception for invalid item type the type of the invalid item.
    /// </summary>
    /// <param name="itemType">Type of the invalid item.</param>
    /// <param name="acceptedTypes">The accepted types.</param>
    protected static ArgumentException GetInvalidItemTypeException(
      Type itemType,
      params Type[] acceptedTypes)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string fullName = itemType.FullName;
      for (int index = 0; index < acceptedTypes.Length; ++index)
      {
        stringBuilder.Append("\"").Append(acceptedTypes[index].FullName).Append("\"");
        if (index + 1 < acceptedTypes.Length)
          stringBuilder.Append(", ");
      }
      return new ArgumentException(Res.Get<ErrorMessages>().InvalidItemType.Arrange((object) fullName, (object) stringBuilder.ToString()));
    }

    /// <summary>Initializes the data provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
    /// <param name="managerType">The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.</param>
    [ApplyNoPolicies]
    protected internal virtual void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      this.Initialize(providerName, config, managerType, true);
    }

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
    /// <param name="managerType">The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.</param>
    /// <param name="initializeDecorator">if set to <c>true</c> [initialize decorator].</param>
    [ApplyNoPolicies]
    protected internal virtual void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType,
      bool initializeDecorator)
    {
      if (string.IsNullOrEmpty(providerName))
        throw new ArgumentNullException(nameof (providerName));
      if (config == null)
        throw new ArgumentNullException(nameof (config));
      if (managerType == (Type) null)
        throw new ArgumentNullException(nameof (managerType));
      lock (this)
        this.initialized = !this.initialized ? true : throw new System.InvalidOperationException("Provider Already Initialized");
      if (((IEnumerable<Type>) this.GetType().GetInterfaces()).Where<Type>((Func<Type, bool>) (i => ((IEnumerable<object>) i.GetCustomAttributes(typeof (DataProviderDecoratorAttribute), false)).Count<object>() > 0)).Select<Type, object>((Func<Type, object>) (i => i.GetCustomAttributes(typeof (DataProviderDecoratorAttribute), false)[0])).SingleOrDefault<object>() is DataProviderDecoratorAttribute decoratorAttribute)
      {
        this.providerDecorator = ObjectFactory.Resolve<IDataProviderDecorator>(decoratorAttribute.DecoratorType.FullName);
        this.providerDecorator.DataProvider = this;
      }
      if (config["title"] != null)
        this.title = config["title"];
      this.name = providerName;
      string str1 = config["applicationName"];
      this.ApplicationName = !string.IsNullOrEmpty(str1) || this is ConfigProvider ? str1 : Config.Get<ProjectConfig>().ProjectName + "/";
      config.Remove("applicationName");
      this.providerGroup = config["providerGroup"];
      config.Remove("providerGroup");
      this.description = config["description"];
      config.Remove("description");
      this.ResourceClassId = config["resourceClassId"];
      config.Remove("resourceClassId");
      string str2 = config["filterQueriesByViewPermissions"];
      if (!string.IsNullOrEmpty(str2))
        this.FilterQueriesByViewPermissions = bool.Parse(str2);
      config.Remove("filterQueriesByViewPermissions");
      this.managerType = managerType;
      if (!(this.providerDecorator != null & initializeDecorator))
        return;
      this.providerDecorator.Initialize(providerName, config, managerType);
    }

    /// <summary>Initializes default data for the provider.</summary>
    [ApplyNoPolicies]
    internal void OnInitialized()
    {
      this.SuppressSecurityChecks = true;
      string transactionName = this.TransactionName;
      this.TransactionName = string.Format("sf_{0}_installDefaultData", (object) this.ModuleName);
      try
      {
        if (!this.InitializeDefaultData())
          return;
        TransactionManager.CommitTransaction(this.TransactionName);
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(this.TransactionName);
        Exception exceptionToHandle = new Exception("An error occurred while executing InitializeDefaultData() method for provider '{0}' of type '{1}': {2}".Arrange((object) this.name, (object) this.GetType().FullName, (object) ex.Message), ex);
        if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
          throw exceptionToHandle;
      }
      finally
      {
        TransactionManager.DisposeTransaction(this.TransactionName);
        this.SuppressSecurityChecks = false;
        this.TransactionName = transactionName;
      }
    }

    /// <summary>Gets the provider abilities for a user.</summary>
    /// <param name="userID">The user ID.</param>
    /// <returns></returns>
    public virtual ProviderAbilities GetAbilitiesForUser(Guid userID) => throw new NotImplementedException("GetAbilitiesForUser not implemented");

    /// <summary>Upgrades the specified version to upgrade from.</summary>
    /// <param name="upgradeFrom">The version to upgrade from. If version is null, it is installed for first time.</param>
    [Obsolete("Use one of these approaches: 1.If your provider is IOpenAccessDataProvider, implement IOpenAccessUpgradableProvider to handle upgrades; 2.Handle upgrade in the Initialize method; 3.Handle upgrade in your module")]
    [ApplyNoPolicies]
    public virtual void Upgrade(Version upgradeFrom)
    {
    }

    /// <summary>Gets the value of the item's title.</summary>
    /// <param name="item">The item.</param>
    public virtual string GetItemTitleValue(IDataItem item) => string.Empty;

    /// <summary>Gets the culture specific value of the item's title.</summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    public virtual string GetItemTitleValue(IDataItem item, CultureInfo culture) => string.Empty;

    /// <summary>
    /// Initializes the provider with default data. Returns true if there are any changes for commiting
    /// </summary>
    protected virtual bool InitializeDefaultData() => false;

    internal IManager GetMappedRelatedManager<TItem>(string providerName) => this.GetRelatedManager(ManagerBase.GetMappedManagerType(typeof (TItem)), providerName);

    internal IManager GetMappedRelatedManager(Type itemType, string providerName) => this.GetRelatedManager(ManagerBase.GetMappedManagerType(itemType), providerName);

    internal T GetRelatedManager<T>(string providerName) where T : IManager => (T) this.GetRelatedManager(typeof (T), providerName);

    /// <summary>Gets manager mapped for the specified item type.</summary>
    /// <typeparam name="T">Type of the items</typeparam>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    internal IManager GetRelatedManager(Type managerType, string providerName)
    {
      IDictionary<string, IManager> dictionary = this.CurrentRelatedManagers;
      if (dictionary == null)
      {
        dictionary = (IDictionary<string, IManager>) new Dictionary<string, IManager>();
        this.CurrentRelatedManagers = dictionary;
      }
      string key = string.Format("{0}_{1}", (object) managerType.FullName, (object) providerName);
      IManager relatedManager;
      if (!dictionary.TryGetValue(key, out relatedManager))
      {
        relatedManager = string.IsNullOrEmpty(this.TransactionName) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetManagerInTransaction(managerType, providerName, this.TransactionName);
        if (relatedManager.Provider != this)
          dictionary.Add(key, relatedManager);
      }
      return relatedManager;
    }

    /// <summary>Do a specified operation with the related managers</summary>
    /// <param name="action"></param>
    internal void DoWithRelatedManagers(Action<IManager> action)
    {
      if (!string.IsNullOrEmpty(this.TransactionName))
        return;
      IDictionary<string, IManager> currentRelatedManagers = this.CurrentRelatedManagers;
      if (currentRelatedManagers == null)
        return;
      foreach (IManager manager in (IEnumerable<IManager>) currentRelatedManagers.Values)
        action(manager);
    }

    internal IDictionary<string, IManager> CurrentRelatedManagers
    {
      get => (!HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(this.RelatedManagersSlot) : SystemManager.CurrentHttpContext.Items[(object) this.RelatedManagersKey]) as IDictionary<string, IManager>;
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) this.RelatedManagersKey] = (object) value;
        else
          Thread.SetData(this.RelatedManagersSlot, (object) value);
      }
    }

    private string RelatedManagersKey
    {
      get
      {
        if (this.relatedManagersKey == null)
          this.relatedManagersKey = "sfRM" + (object) this.GetHashCode();
        return this.relatedManagersKey;
      }
    }

    private LocalDataStoreSlot RelatedManagersSlot
    {
      get
      {
        if (this.relatedManagersSlot == null)
          this.relatedManagersSlot = Thread.AllocateDataSlot();
        return this.relatedManagersSlot;
      }
    }

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object id.</param>
    /// <param name="principalId">The principal id.</param>
    /// <returns></returns>
    public virtual Telerik.Sitefinity.Security.Model.Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.GetPermission(permissionSet, objectId, principalId);
      throw new MissingDecoratorException(this, "GetPermission(string permissionSet, Guid objectId, Guid principalId)");
    }

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionId">The permission id.</param>
    /// <returns></returns>
    public virtual Telerik.Sitefinity.Security.Model.Permission GetPermission(
      Guid permissionId)
    {
      return this.providerDecorator != null ? this.providerDecorator.GetPermission(permissionId) : throw new MissingDecoratorException(this, "GetPermission(Guid PermissionId)");
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    [MethodPermission("General", new string[] {"View"})]
    public virtual IQueryable<Telerik.Sitefinity.Security.Model.Permission> GetPermissions() => this.providerDecorator != null ? this.providerDecorator.GetPermissions() : throw new MissingDecoratorException(this, "GetPermissions()");

    /// <summary>Gets a query for permissions.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <returns></returns>
    [MethodPermission("General", new string[] {"View"})]
    public virtual IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Telerik.Sitefinity.Security.Model.Permission => this.providerDecorator != null ? this.providerDecorator.GetPermissions<TPermission>() : throw new MissingDecoratorException(this, "GetPermissions<TPermission>()");

    /// <summary>Gets a query for permissions.</summary>
    /// <param name="actualType">The actual type.</param>
    /// <returns></returns>
    public virtual IQueryable<Telerik.Sitefinity.Security.Model.Permission> GetPermissions(
      Type actualType)
    {
      return this.providerDecorator != null ? this.providerDecorator.GetPermissions(actualType) : throw new MissingDecoratorException(this, "GetPermissions(Type actualType)");
    }

    /// <summary>Creates new permission.</summary>
    /// <typeparam name="TPermission">Generic type of the permission</typeparam>
    /// <param name="permissionSet">Name of the permission set</param>
    /// <param name="objectId">Id of the secured object</param>
    /// <param name="principalId">Id of the related principal</param>
    /// <returns>A permission of type TPermission</returns>
    public virtual Telerik.Sitefinity.Security.Model.Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.CreatePermission(permissionSet, objectId, principalId);
      throw new MissingDecoratorException(this, "CreatePermission<TPermission>(string permissionSet, Guid objectId, Guid principalId)");
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public virtual void DeletePermission(Telerik.Sitefinity.Security.Model.Permission permission)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "DeletePermission(Permission permission)");
      this.providerDecorator.DeletePermission(permission);
    }

    /// <summary>
    /// Makes a deep copy of the permission from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source collection of permissions.</param>
    /// <param name="target">The target list.</param>
    /// <param name="souceObjectId">The source object id.</param>
    /// <param name="targetObjectId">The target object id.</param>
    public virtual void CopyPermissions(
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> source,
      IList target,
      Guid souceObjectId,
      Guid targetObjectId,
      bool overwrite = false)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      foreach (Telerik.Sitefinity.Security.Model.Permission permission1 in source)
      {
        Telerik.Sitefinity.Security.Model.Permission src = permission1;
        Telerik.Sitefinity.Security.Model.Permission permission2 = !(src.ObjectId != souceObjectId) ? target.OfType<Telerik.Sitefinity.Security.Model.Permission>().FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.SetName == src.SetName && p.PrincipalId == src.PrincipalId && p.ObjectId == targetObjectId)) : target.OfType<Telerik.Sitefinity.Security.Model.Permission>().FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.SetName == src.SetName && p.PrincipalId == src.PrincipalId && p.ObjectId == src.ObjectId));
        if (permission2 == null)
        {
          if (src.ObjectId != souceObjectId)
          {
            target.Add((object) src);
          }
          else
          {
            Telerik.Sitefinity.Security.Model.Permission permission3 = this.CreatePermission(src.SetName, targetObjectId, src.PrincipalId);
            permission3.Grant = src.Grant;
            permission3.Deny = src.Deny;
            target.Add((object) permission3);
          }
        }
        else if (overwrite)
        {
          permission2.Grant = src.Grant;
          permission2.Deny = src.Deny;
        }
      }
    }

    /// <summary>Breaks the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void BreakPermiossionsInheritance(ISecuredObject securedObject)
    {
      if (!this.SuppressSecurityChecks)
      {
        if (securedObject is PageNode)
        {
          if (!LicenseState.Current.LicenseInfo.IsPagesGranularPermissionsEnabled)
            throw new NotSupportedException(Res.Get<SecurityResources>().GranularPermissionsForPagesAreDisabled);
        }
        else if (!LicenseState.Current.LicenseInfo.IsGranularPermissionsEnabled)
          throw new NotSupportedException(Res.Get<SecurityResources>().GranularPermissionsAreDisabled);
        if (!securedObject.ArePermissionChangesAllowedByLicense())
          throw new NotSupportedException(Res.Get<SecurityResources>().PermissionSettingForThisObjectIsDisabledOnYourLicense);
      }
      if (securedObject.InheritsPermissions)
      {
        securedObject.InheritsPermissions = false;
        IEnumerable<Telerik.Sitefinity.Security.Model.Permission> permissions = securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (permission => permission.ObjectId != securedObject.Id));
        List<Telerik.Sitefinity.Security.Model.Permission> permissionList = new List<Telerik.Sitefinity.Security.Model.Permission>();
        foreach (Telerik.Sitefinity.Security.Model.Permission permission1 in permissions)
        {
          Telerik.Sitefinity.Security.Model.Permission inheritedPermission = permission1;
          Telerik.Sitefinity.Security.Model.Permission[] array = securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.SetName.Equals(inheritedPermission.SetName, StringComparison.OrdinalIgnoreCase) && p.PrincipalId == inheritedPermission.PrincipalId && p.ObjectId == securedObject.Id)).ToArray<Telerik.Sitefinity.Security.Model.Permission>();
          if (array.Length != 0)
          {
            for (int index = 0; index < array.Length; ++index)
            {
              array[index].Grant = inheritedPermission.Grant;
              array[index].Deny = inheritedPermission.Deny;
            }
          }
          else
          {
            Telerik.Sitefinity.Security.Model.Permission permission2 = this.GetOrCreatePermission(inheritedPermission.SetName, securedObject.Id, inheritedPermission.PrincipalId);
            permission2.Grant = inheritedPermission.Grant;
            permission2.Deny = inheritedPermission.Deny;
            permissionList.Add(permission2);
          }
        }
        foreach (Telerik.Sitefinity.Security.Model.Permission permission in permissionList)
        {
          if (!securedObject.Permissions.Contains(permission))
            securedObject.Permissions.Add(permission);
        }
      }
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> ownPermissions = securedObject.GetOwnPermissions();
      bool showAction = true;
      foreach (string supportedPermissionSet in securedObject.SupportedPermissionSets)
      {
        string permissionSetName = supportedPermissionSet;
        Telerik.Sitefinity.Security.Configuration.Permission permission3 = Config.Get<SecurityConfig>().Permissions[permissionSetName];
        IEnumerable<Telerik.Sitefinity.Security.Model.Permission> permissions = ownPermissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (perm => perm.SetName == permissionSetName));
        if (permission3 != null)
        {
          foreach (SecurityAction action in (ConfigElementCollection) permission3.Actions)
          {
            action.GetTitle(securedObject, out showAction);
            if (!showAction)
            {
              foreach (Telerik.Sitefinity.Security.Model.Permission permission4 in permissions)
              {
                permission4.UngrantActions(action.Name);
                permission4.UndenyActions(action.Name);
              }
            }
          }
        }
      }
      this.ResetChildrenPermissionsOnInheritanceBreak(securedObject, this.TransactionName);
    }

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void RestorePermissionInheritance(ISecuredObject securedObject)
    {
      foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) securedObject.Permissions)
      {
        if (permission.ObjectId == securedObject.Id)
        {
          permission.Grant = 0;
          permission.Deny = 0;
        }
      }
      securedObject.InheritsPermissions = true;
      this.ResetChildrenPermissionsOnInheritanceRestore(securedObject, this.TransactionName);
    }

    /// <summary>
    /// Converts a VOA (shorter OpenAccess string representation of a type) to a CLR type
    /// </summary>
    /// <param name="voaClassName">Name of the voa class.</param>
    /// <returns>The resolved CLR type</returns>
    public virtual Type ConvertVoaClassToClrType(string voaClassName) => this.providerDecorator != null ? this.providerDecorator.ConvertVoaClassToClrType(voaClassName) : throw new MissingDecoratorException(this, "ConvertVoaClassToClrType(string voaClass)");

    /// <summary>
    /// Converts  a CLR type to a VOA string (shorter OpenAccess string representation of a type)
    /// </summary>
    /// <param name="clrType">The CLR type.</param>
    /// <returns>Name of the voa class.</returns>
    public virtual string ConvertClrTypeVoaClass(Type clrType) => this.providerDecorator != null ? this.providerDecorator.ConvertClrTypeVoaClass(clrType) : throw new MissingDecoratorException(this, "ConvertClrTypeVoaClass(Type ClrType)");

    /// <summary>
    /// Executes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="child">The permissions child.</param>
    public void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child)");
      this.providerDecorator.CreatePermissionInheritanceAssociation(parent, child);
    }

    /// <summary>Deletes the permissions inheritance association.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="child">The child.</param>
    public void DeletePermissionsInheritanceAssociation(ISecuredObject parent, ISecuredObject child)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "DeletePermissionsInheritanceAssociation(ISecuredObject parent, ISecuredObject child)");
      this.providerDecorator.DeletePermissionsInheritanceAssociation(parent, child);
    }

    /// <summary>
    /// Retrieves a list of TransactionPermissionAttribute objects, which contain information about permission actions (and sets) relevant for a specific CRUD action type, and secured object type.
    /// </summary>
    /// <param name="securedObjectType">Type of the secured object.</param>
    /// <param name="actionType">Type of the security action.</param>
    /// <returns>A list of relevant TransactionPermissionAttribute objects</returns>
    public List<TransactionPermissionAttribute> GetPermissionActionsForTransaction(
      Type securedObjectType,
      SecurityConstants.TransactionActionType actionType)
    {
      TypeAction key = new TypeAction(securedObjectType, actionType);
      List<TransactionPermissionAttribute> actionsForTransaction = new List<TransactionPermissionAttribute>();
      if (this.transactionPermissionAttributeCache.Keys.Contains(key))
      {
        actionsForTransaction = this.transactionPermissionAttributeCache[key];
      }
      else
      {
        MethodInfo method = this.GetType().GetMethod("CommitTransaction");
        if (method != (MethodInfo) null)
        {
          foreach (TransactionPermissionAttribute customAttribute in method.GetCustomAttributes(typeof (TransactionPermissionAttribute), true))
          {
            if (customAttribute.ItemType == securedObjectType && customAttribute.ActionType == actionType)
              actionsForTransaction.Add(customAttribute);
          }
          this.transactionPermissionAttributeCache.Add(key, actionsForTransaction);
        }
      }
      return actionsForTransaction;
    }

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => this.providerDecorator.GetInheritanceMaps();

    /// <inheritdoc />
    public PermissionsInheritanceMap CreatePermissionsInheritanceMap(
      Guid objectId,
      Guid childObjectId,
      string childObjectTypeName)
    {
      return this.providerDecorator.CreatePermissionsInheritanceMap(objectId, childObjectId, childObjectTypeName);
    }

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      this.providerDecorator.DeletePermissionsInheritanceMap(permissionsInheritanceMap);
    }

    protected internal virtual bool IsSecurityActionGranted(
      ISecuredObject securedObject,
      SecurityActionTypes action)
    {
      return securedObject.IsSecurityActionTypeGranted(action);
    }

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <returns>The proxy object</returns>
    public virtual T CreateProxy<T>(T dataItem) where T : IDataItem => this.providerDecorator != null ? this.providerDecorator.CreateProxy<T>(dataItem) : throw new MissingDecoratorException(this, "CreateProxy(IDataItem dataItem)");

    /// <summary>
    /// Creates a database disconnected copy of the provided data item.
    /// </summary>
    /// <typeparam name="T">The CLR type of the data item.</typeparam>
    /// <param name="dataItem">The data item to create proxy for.</param>
    /// <param name="listName">Specifies a named list for keeping reference to this proxy. This parameter is not required.</param>
    /// <param name="fetchGroup">The fetch group.</param>
    /// <returns>The proxy object</returns>
    public virtual T CreateProxy<T>(T dataItem, string listName, string fetchGroup) where T : IDataItem
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.CreateProxy<T>(dataItem, listName, fetchGroup);
      throw new MissingDecoratorException(this, "CreateProxy(IDataItem dataItem, string listName, string fetchGroup)");
    }

    /// <summary>
    /// Gets a proxy object for the specified data item identity if available.
    /// </summary>
    /// <typeparam name="T">The CLR type of the persistent data item.</typeparam>
    /// <param name="id">The identity of the data item.</param>
    /// <returns>The proxy object if available otherwise null.</returns>
    public virtual T GetProxy<T>(Guid id) where T : IDataItem => this.providerDecorator != null ? this.providerDecorator.GetProxy<T>(id) : throw new MissingDecoratorException(this, "GetProxy<T>(Guid id)");

    /// <summary>
    /// Gets or creates new manager info if one does not exist with the provided parameters.
    /// </summary>
    /// <param name="managerType">The full type name, without assembly information, of the manager.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <returns>ManagerInfo</returns>
    public virtual ManagerInfo GetManagerInfo(string managerType, string providerName)
    {
      if (this.providerDecorator != null)
        return this.providerDecorator.GetManagerInfo(managerType, providerName);
      throw new MissingDecoratorException(this, "GetManagerInfo(string managerType, string providerName)");
    }

    /// <summary>
    /// Gets the specified <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object to be retrieved.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    [MethodPermission("General", new string[] {"View"})]
    public virtual ManagerInfo GetManagerInfo(Guid id) => this.providerDecorator != null ? this.providerDecorator.GetManagerInfo(id) : throw new MissingDecoratorException(this, "GetManagerInfo(Guid id)");

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> objects.
    /// </summary>
    /// <returns>The query object.</returns>
    [MethodPermission("General", new string[] {"View"})]
    public virtual IQueryable<ManagerInfo> GetManagerInfos() => this.providerDecorator != null ? this.providerDecorator.GetManagerInfos() : throw new MissingDecoratorException(this, "GetManagerInfo()");

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    [MethodPermission("General", new string[] {"Create"})]
    public virtual ManagerInfo CreateManagerInfo() => this.providerDecorator != null ? this.providerDecorator.CreateManagerInfo() : throw new MissingDecoratorException(this, "CreateManagerInfo()");

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" /> object.
    /// </summary>
    /// <param name="managerInfoType">Type of the manager info.</param>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Model.ManagerInfo" />.</returns>
    [MethodPermission("General", new string[] {"Create"})]
    public virtual ManagerInfo CreateManagerInfo(Guid id) => this.providerDecorator != null ? this.providerDecorator.CreateManagerInfo(id) : throw new MissingDecoratorException(this, "CreateManagerInfo(Guid id)");

    /// <summary>Deletes the manager info object.</summary>
    /// <param name="permission">The permission.</param>
    [MethodPermission("General", new string[] {"Delete"})]
    public virtual void DeleteManagerInfo(ManagerInfo info)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "DeleteManagerInfo(ManagerInfo info)");
      this.providerDecorator.DeleteManagerInfo(info);
    }

    internal Telerik.Sitefinity.Security.Model.Permission GetOrCreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return (this.GetDirtyItems().OfType<Telerik.Sitefinity.Security.Model.Permission>().Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => this.GetDirtyItemStatus((object) p) != SecurityConstants.TransactionActionType.Deleted && p.SetName == permissionSet && p.ObjectId == objectId && p.PrincipalId == principalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() ?? this.GetPermission(permissionSet, objectId, principalId)) ?? this.CreatePermission(permissionSet, objectId, principalId);
    }

    internal void FetchAllLanguagesData()
    {
      if (!(this is IOpenAccessDataProvider provider))
        return;
      provider.GetContext().FetchAllLanguagesData();
    }

    internal void FetchLanguadeSpecificData(CultureInfo culture)
    {
      if (!(this is IOpenAccessDataProvider provider))
        return;
      provider.GetContext().FetchLanguadeSpecificData(culture);
    }

    /// <summary>Gets the result for a dynamic LINQ query as list.</summary>
    /// <typeparam name="TProvider">The type of the data provider.</typeparam>
    /// <typeparam name="TItem">The type of the data item.</typeparam>
    /// <param name="query">The LINQ query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of items to skip. If the value is zero, no items are skipped.</param>
    /// <param name="take">The number of items to take. If the value is zero, all items are taken.</param>
    /// <param name="totalCount">The total count of items regardless skip and take.</param>
    /// <param name="forEach">For each.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual IList<TItem> GetList<TProvider, TItem>(
      IQueryable<TItem> query,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount,
      ForEachItem<TProvider, TItem> forEach)
      where TProvider : DataProviderBase
      where TItem : IDataItem
    {
      if (!string.IsNullOrEmpty(filterExpression))
        query = query.Where<TItem>(filterExpression);
      IList<TItem> list;
      if (skip == 0 && take == 0)
      {
        if (!string.IsNullOrEmpty(sortExpression))
          query = query.OrderBy<TItem>(sortExpression);
        if (forEach != null)
        {
          list = (IList<TItem>) new List<TItem>(1000);
          foreach (TItem obj in (IEnumerable<TItem>) query)
          {
            obj.Provider = (object) this;
            forEach((TProvider) this, obj);
            list.Add(obj);
          }
        }
        else
          list = (IList<TItem>) query.ToList<TItem>();
        totalCount = list.Count;
      }
      else
      {
        totalCount = query.Count<TItem>();
        if (!string.IsNullOrEmpty(sortExpression))
          query = query.OrderBy<TItem>(sortExpression);
        if (skip != 0)
          query = query.Skip<TItem>(skip);
        if (take != 0)
          query = query.Take<TItem>(take);
        if (forEach != null)
        {
          list = (IList<TItem>) new List<TItem>(take);
          foreach (TItem obj in (IEnumerable<TItem>) query)
          {
            obj.Provider = (object) this;
            forEach((TProvider) this, obj);
            list.Add(obj);
          }
        }
        else
          list = (IList<TItem>) query.ToList<TItem>();
      }
      return list;
    }

    /// <summary>
    /// Gets the result for a dynamic LINQ query as enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The LINQ query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of items to skip. If the value is zero, no items are skipped.</param>
    /// <param name="take">The number of items to take. If the value is zero, all items are taken.</param>
    /// <param name="totalCount">The total count of items regardless skip and take.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual IEnumerable<T> GetEnumerable<T>(
      IQueryable<T> query,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      if (!string.IsNullOrEmpty(filterExpression))
        query = query.Where<T>(filterExpression);
      totalCount = Queryable.Count<T>(query);
      if (!string.IsNullOrEmpty(sortExpression))
        query = query.OrderBy<T>(sortExpression);
      if (skip != 0)
        query = Queryable.Skip<T>(query, skip);
      if (take != 0)
        query = Queryable.Take<T>(query, take);
      return (IEnumerable<T>) query;
    }

    /// <summary>Gets the total items count for the specified query.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual int GetCount<T>(IQueryable<T> query, string filterExpression)
    {
      if (!string.IsNullOrEmpty(filterExpression))
        query = query.Where<T>(filterExpression);
      return Queryable.Count<T>(query);
    }

    /// <summary>Gets the class id for the given persistent type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The classId of the given type</returns>
    public int GetClassId(Type type) => !(type == (Type) null) ? this.providerDecorator.GetClassId(type) : throw new ArgumentNullException(nameof (type));

    /// <summary>Determines whether is system provider.</summary>
    internal bool IsSystemProvider() => "System".Equals(this.ProviderGroup);

    /// <summary>
    /// Determines if the provider supports caching for a given type.
    /// </summary>
    /// <param name="t">The type that has been managed by the provider.</param>
    /// <returns></returns>
    protected internal bool IsCacheEnabledFor(Type t)
    {
      bool flag;
      if (!this.typeCacheEnabled.TryGetValue(t.FullName, out flag))
      {
        lock (this.typeCacheEnabled)
        {
          if (!this.typeCacheEnabled.TryGetValue(t.FullName, out flag))
          {
            flag = false;
            if (this is IOpenAccessDataProvider provider)
            {
              SitefinityOAContext context = provider.GetContext();
              if (context != null && context.OpenAccessConnection.Backend.SecondLevelCache.Enabled)
              {
                MetaPersistentType metaPersistentType = (context.Metadata.PersistentTypes as MetadataCollection<MetaPersistentType>).Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (mpt => mpt.FullName == t.FullName)).FirstOrDefault<MetaPersistentType>();
                if (metaPersistentType != null && metaPersistentType.CacheStrategy != CacheStrategy.No)
                  flag = true;
              }
            }
            this.typeCacheEnabled.Add(t.FullName, flag);
          }
        }
      }
      return flag;
    }

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    [ApplyNoPolicies]
    public virtual ISecuredObject GetSecurityRoot() => this.SecurityRoot;

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    [ApplyNoPolicies]
    public virtual ISecuredObject GetSecurityRoot(bool create)
    {
      if (this.providerDecorator != null)
        return (ISecuredObject) this.providerDecorator.GetSecurityRoot(create, this.PermissionsetObjectTitleResKeys, this.SupportedPermissionSets);
      throw new MissingDecoratorException(this, "GetSecurityRoot(bool create, IDictionary<string, string> permissionsetObjectTitleResKeys, params string[] permissionSets)");
    }

    /// <inheritdoc />
    [ApplyNoPolicies]
    public virtual IQueryable<PermissionsInheritanceMap> GetPermissionChildren(
      Guid parentId)
    {
      return this.providerDecorator != null ? this.providerDecorator.GetPermissionChildren(parentId) : throw new MissingDecoratorException(this, "GetPermissionChildren()");
    }

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    [ApplyNoPolicies]
    public virtual void SetRootPermissions(Telerik.Sitefinity.Security.Model.SecurityRoot root)
    {
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.CreatePermission("General", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(true, "View");
      root.Permissions.Add(permission1);
      Telerik.Sitefinity.Security.Model.Permission permission2 = this.CreatePermission("General", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(true, "Delete", "Modify");
      root.Permissions.Add(permission2);
    }

    /// <summary>Gets the security root for this provider.</summary>
    [ApplyNoPolicies]
    public virtual ISecuredObject SecurityRoot
    {
      get
      {
        if (!this.secRootSet)
        {
          lock (this.secRootLockObject)
          {
            if (!this.secRootSet)
            {
              int num = 3;
              while (num > 0)
              {
                ISecuredObject securityRoot = this.GetSecurityRoot(true);
                if (securityRoot != null)
                {
                  try
                  {
                    this.secRoot = securityRoot as SecuredProxy;
                    if (this.secRoot == null)
                    {
                      this.secRoot = this.GetSecuredProxy(securityRoot);
                      if (this.Expire == null)
                        this.Expire = new ChangedCallback(this.OnPermissonChanged);
                      CacheDependency.Subscribe((object) securityRoot, this.Expire);
                    }
                    num = 0;
                  }
                  catch (NoSuchObjectException ex)
                  {
                    if (--num == 0)
                      throw;
                  }
                }
              }
              this.secRootSet = true;
            }
          }
        }
        return (ISecuredObject) this.secRoot;
      }
    }

    public ChangedCallback Expire { get; private set; }

    protected virtual SecuredProxy GetSecuredProxy(ISecuredObject root) => new SecuredProxy(root);

    private void OnPermissonChanged(ICacheDependencyHandler handler, Type itemType, string itemKey)
    {
      if (string.IsNullOrEmpty(itemKey) || this.secRoot == null || !this.secRoot.Id.ToString().Equals(itemKey, StringComparison.InvariantCultureIgnoreCase))
        return;
      this.secRoot = (SecuredProxy) null;
      this.secRootSet = false;
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    [ApplyNoPolicies]
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      Telerik.Sitefinity.Security.Model.Permission permission,
      string transactionName)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "AddPermissionToObject(ISecuredObject securedObject, Permission permission, string transactionName)");
      this.providerDecorator.AddPermissionToObject(securedObject, permission, transactionName);
    }

    /// <summary>Adds the permission to object.</summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="managerInstace">This parameter is obsolete - Instance of the secured object's related manager.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    [ApplyNoPolicies]
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstance,
      Telerik.Sitefinity.Security.Model.Permission permission,
      string transactionName)
    {
      if (this.providerDecorator == null)
        throw new MissingDecoratorException(this, "AddPermissionToObject(ISecuredObject securedObject, IManager managerInstance, Permission permission, string transactionName)");
      this.providerDecorator.AddPermissionToObject(securedObject, managerInstance, permission, transactionName);
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    public abstract string RootKey { get; }

    /// <summary>Gets the identifier of the secured object.</summary>
    /// <value></value>
    public virtual Guid Id => this.SecurityRoot == null ? Guid.Empty : this.SecurityRoot.Id;

    /// <summary>
    /// Defines if the implemented type inherits permissions from a parent object.
    /// </summary>
    /// <value></value>
    public bool InheritsPermissions
    {
      get => false;
      set => throw new NotSupportedException();
    }

    /// <summary>Gets the permissions for this object.</summary>
    /// <value>A list of defined permissions.</value>
    public virtual IList<Telerik.Sitefinity.Security.Model.Permission> Permissions => this.SecurityRoot == null ? (IList<Telerik.Sitefinity.Security.Model.Permission>) new Telerik.Sitefinity.Security.Model.Permission[0] : this.SecurityRoot.Permissions;

    /// <summary>
    /// Gets or sets a value indicating whether this instance can inherit permissions.
    /// </summary>
    /// <value></value>
    public virtual bool CanInheritPermissions
    {
      get => this.SecurityRoot.CanInheritPermissions;
      set => this.SecurityRoot.CanInheritPermissions = value;
    }

    /// <summary>
    /// Gets the all the secured objects which inherit permissions, through permissions hierarchy, from a secured object.
    /// </summary>
    /// <param name="root">The root object for which the permission inheritors are scanned. Must implement the ISecuredObject interface</param>
    /// <param name="permissionInheritorsOnly">if set to <c>true</c>, only the child objects for which the InheritsPermissions property is set to true are returned. Otherwise, all child objects are returned.</param>
    /// <param name="objectType">If set to null, all child objects are returned. If set to a Type, only child objects of the type are returned.</param>
    /// <returns>A List of ISecured objects which inherit permissions, through permissions hierarchy, from the root object.</returns>
    public virtual List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType)
    {
      List<ISecuredObject> inheritors = new List<ISecuredObject>();
      return this.GetPermissionsInheritors(root, permissionInheritorsOnly, objectType, inheritors);
    }

    /// <summary>
    /// Gets the all the secured objects which inherit permissions, through permissions hierarchy, from a secured object.
    /// </summary>
    /// <param name="root">The root object for which the permission inheritors are scanned. Must implement the ISecuredObject interface</param>
    /// <param name="permissionInheritorsOnly">if set to <c>true</c>, only the child objects for which the InheritsPermissions property is set to true are returned. Otherwise, all child objects are returned.</param>
    /// <param name="objectType">If set to null, all child objects are returned. If set to a Type, only child objects of the type are returned.</param>
    /// <param name="inheritors">A empty list of ISecuredObject object. This argument is required for the recursion of the method.</param>
    /// <returns>A List of ISecured objects which inherit permissions, through permissions hierarchy, from the root object.</returns>
    [SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable")]
    internal virtual List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType,
      List<ISecuredObject> inheritors)
    {
      if (inheritors == null)
        inheritors = new List<ISecuredObject>();
      if (root != null)
      {
        IEnumerable<string> classes = this.GetKnownSecuredObjectsTypes();
        List<PermissionsInheritanceMap> list = this.GetPermissionChildren(root.Id).Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (pc => classes.Contains<string>(pc.ChildObjectTypeName))).ToList<PermissionsInheritanceMap>();
        if (list.Count == 0)
          return inheritors;
        foreach (PermissionsInheritanceMap permissionsInheritanceMap in list)
        {
          Type clrType = this.ConvertVoaClassToClrType(permissionsInheritanceMap.ChildObjectTypeName);
          if (clrType != (Type) null)
          {
            if (typeof (ISecuredObject).IsAssignableFrom(clrType))
            {
              try
              {
                object obj1 = (object) null;
                if (((IEnumerable<Type>) this.GetKnownTypes()).Contains<Type>(clrType))
                {
                  obj1 = this.GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
                }
                else
                {
                  IManager manager;
                  if (ManagerBase.TryGetMappedManager(clrType, (string) null, out manager))
                  {
                    if (manager.Provider.GetType().Equals(this.GetType()))
                    {
                      obj1 = this.GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
                    }
                    else
                    {
                      foreach (DataProviderBase allProvider in manager.GetAllProviders())
                      {
                        try
                        {
                          obj1 = allProvider.GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
                          if (obj1 != null)
                            break;
                        }
                        catch (ItemNotFoundException ex)
                        {
                        }
                      }
                    }
                  }
                  else
                    continue;
                }
                PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>) obj1.GetType().GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name == "ParentId"));
                if (propertyInfo1 != (PropertyInfo) null)
                {
                  PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>) obj1.GetType().GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name == "Parent"));
                  if (propertyInfo2 == (PropertyInfo) null || typeof (ISecuredObject).IsAssignableFrom(propertyInfo2.PropertyType))
                  {
                    object obj2 = propertyInfo1.GetValue(obj1, (object[]) null);
                    if (obj2 != null && (Guid) obj2 != root.Id)
                      obj1 = (object) null;
                  }
                }
                if (obj1 != null)
                {
                  ISecuredObject root1 = (ISecuredObject) obj1;
                  if (inheritors.Contains(root1))
                    return inheritors;
                  if ((permissionInheritorsOnly && root1.InheritsPermissions || !permissionInheritorsOnly) && (objectType == (Type) null || root.GetType() == objectType))
                    inheritors.Add(root1);
                  if (root1.InheritsPermissions)
                    this.GetPermissionsInheritors(root1, permissionInheritorsOnly, objectType, inheritors);
                }
              }
              catch (ItemNotFoundException ex)
              {
              }
            }
          }
        }
      }
      return inheritors;
    }

    /// <summary>
    /// This recursive method handles the correct behavior when the inheritance is broken or restored on a secured object:
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
    [Obsolete("This method is part of break/restore inheritance behavior and it is not intended to use separately.")]
    public void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenOrRestoredInheritanceRootObjectId,
      List<Telerik.Sitefinity.Security.Model.Permission> permissionsToAssignToChildren,
      ISecuredObject currentlyScannedNode,
      string transactionName)
    {
      IEnumerable<string> classes = this.GetKnownSecuredObjectsTypes();
      IQueryable<PermissionsInheritanceMap> permissionChildren = this.GetPermissionChildren(currentlyScannedNode.Id);
      Expression<Func<PermissionsInheritanceMap, bool>> predicate = (Expression<Func<PermissionsInheritanceMap, bool>>) (pc => classes.Contains<string>(pc.ChildObjectTypeName));
      foreach (PermissionsInheritanceMap permissionsInheritanceMap in (IEnumerable<PermissionsInheritanceMap>) permissionChildren.Where<PermissionsInheritanceMap>(predicate))
      {
        Type clrType = this.ConvertVoaClassToClrType(permissionsInheritanceMap.ChildObjectTypeName);
        if (clrType.GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
        {
          IManager managerInTransaction = ManagerBase.GetMappedManagerInTransaction(clrType, transactionName);
          ISecuredObject currentlyScannedNode1;
          try
          {
            currentlyScannedNode1 = (ISecuredObject) managerInTransaction.GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
          }
          catch (ItemNotFoundException ex)
          {
            continue;
          }
          string name = managerInTransaction.Provider.Name;
          string providerName = managerInTransaction.ResolveSecuredObjectProviderName(clrType, permissionsInheritanceMap.ChildObjectId);
          if (providerName != name)
            currentlyScannedNode1 = (ISecuredObject) ManagerBase.GetMappedManagerInTransaction(clrType, providerName, transactionName).GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
          for (int index = currentlyScannedNode1.Permissions.Count - 1; index >= 0; --index)
          {
            if (currentlyScannedNode1.Permissions[index].ObjectId != currentlyScannedNode1.Id)
              currentlyScannedNode1.Permissions.Remove(currentlyScannedNode1.Permissions[index]);
          }
          foreach (Telerik.Sitefinity.Security.Model.Permission permissionsToAssignToChild in permissionsToAssignToChildren)
            currentlyScannedNode1.Permissions.Add(permissionsToAssignToChild);
          if (currentlyScannedNode1.InheritsPermissions)
            this.ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(brokenOrRestoredInheritanceRootObjectId, permissionsToAssignToChildren, currentlyScannedNode1, transactionName);
        }
      }
    }

    /// <summary>
    /// Resets the children permissions when inheritance is changed for the parent.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="inheritanceAction">The inheritance action.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected virtual void ResetChildrenPermissionsOnInheritanceChange(
      ISecuredObject parent,
      InheritanceAction inheritanceAction,
      string transactionName)
    {
      if (parent == null)
        throw new ArgumentNullException(nameof (parent));
      IEnumerable<string> classes = this.GetKnownSecuredObjectsTypes();
      IQueryable<PermissionsInheritanceMap> queryable = this.GetPermissionChildren(parent.Id).Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (pc => classes.Contains<string>(pc.ChildObjectTypeName)));
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> assignToChildren = parent.GetPermissionsToAssignToChildren(inheritanceAction);
      foreach (PermissionsInheritanceMap permissionsInheritanceMap in (IEnumerable<PermissionsInheritanceMap>) queryable)
      {
        Type clrType = this.ConvertVoaClassToClrType(permissionsInheritanceMap.ChildObjectTypeName);
        if (clrType.GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
        {
          IManager managerInTransaction = ManagerBase.GetMappedManagerInTransaction(clrType, transactionName);
          ISecuredObject parent1;
          try
          {
            parent1 = (ISecuredObject) managerInTransaction.GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
          }
          catch (ItemNotFoundException ex)
          {
            continue;
          }
          string name = managerInTransaction.Provider.Name;
          string providerName = managerInTransaction.ResolveSecuredObjectProviderName(clrType, permissionsInheritanceMap.ChildObjectId);
          if (providerName != name)
            parent1 = (ISecuredObject) ManagerBase.GetMappedManagerInTransaction(clrType, providerName, transactionName).GetItem(clrType, permissionsInheritanceMap.ChildObjectId);
          for (int index = parent1.Permissions.Count - 1; index >= 0; --index)
          {
            if (parent1.Permissions[index].ObjectId != parent1.Id)
              parent1.Permissions.Remove(parent1.Permissions[index]);
          }
          foreach (Telerik.Sitefinity.Security.Model.Permission permission in assignToChildren)
            parent1.Permissions.Add(permission);
          if (parent1.InheritsPermissions)
            this.ResetChildrenPermissionsOnInheritanceChange(parent1, InheritanceAction.SyncWithParent, transactionName);
        }
      }
    }

    /// <summary>Resets the children permissions on inheritance break.</summary>
    /// <param name="parent">The parent secured object for which permission inheritance is broken.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected void ResetChildrenPermissionsOnInheritanceBreak(
      ISecuredObject parent,
      string transactionName)
    {
      this.ResetChildrenPermissionsOnInheritanceChange(parent, InheritanceAction.Break, transactionName);
    }

    /// <summary>
    /// Resets the children permissions on inheritance restore.
    /// </summary>
    /// <param name="parent">The parent secured object for which permission inheritance is restored.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected void ResetChildrenPermissionsOnInheritanceRestore(
      ISecuredObject parent,
      string transactionName)
    {
      this.ResetChildrenPermissionsOnInheritanceChange(parent, InheritanceAction.Restore, transactionName);
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public virtual string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permission set object titles.</value>
    public virtual IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.permissionsetObjectTitleResKeys;
      set => this.permissionsetObjectTitleResKeys = value;
    }

    /// <summary>
    /// Raises the <see cref="E:Disposing" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    protected virtual void OnDisposing(EventArgs e)
    {
      if (this.Disposing == null)
        return;
      this.Disposing((object) this, e);
    }

    /// <summary>
    /// Dispose of the data provider before garbage collection.
    /// </summary>
    ~DataProviderBase() => this.Dispose(false);

    /// <summary>
    /// Dispose of the data provider before garbage collection.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Dispose of the data provider before garbage collection.
    /// </summary>
    /// <param name="disposing">
    /// <see langword="true" /> if disposing; otherwise, <see langword="false" />.
    /// </param>
    [ApplyNoPolicies]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.OnDisposing(EventArgs.Empty);
      if (this.providerDecorator != null)
      {
        this.providerDecorator.Executing -= new EventHandler<ExecutingEventArgs>(this.Decorator_Executing);
        this.providerDecorator.Executed -= new EventHandler<ExecutedEventArgs>(this.Decorator_Executed);
      }
      this.Executed = (EventHandler<ExecutedEventArgs>) null;
      this.Executing = (EventHandler<ExecutingEventArgs>) null;
      this.disposed = true;
    }

    internal bool IsDisposed => this.disposed;

    internal bool IsVirtual { get; set; }

    bool IDataProviderInfo.IsVirtual => this.IsVirtual;

    bool IDataProviderInfo.IsSystem => this.IsSystemProvider();

    /// <summary>Occurs when the provider is about to be disposed.</summary>
    public event EventHandler<EventArgs> Disposing;

    /// <summary>
    /// Raises the <see cref="E:Executing" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public virtual void Decorator_Executing(object sender, ExecutingEventArgs args) => this.OnExecuting(args);

    /// <summary>
    /// Raises the <see cref="E:Executed" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public virtual void Decorator_Executed(object sender, ExecutedEventArgs args) => this.OnExecuted(args);

    /// <summary>
    /// Raises the <see cref="E:Executing" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public virtual void OnExecuting(ExecutingEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.Executing == null)
        return;
      this.Executing((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:Executed" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    [ApplyNoPolicies]
    public virtual void OnExecuted(ExecutedEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      bool flag = false;
      if (this.Executed != null)
      {
        flag = args.CommandName == "CommitTransaction" && !string.IsNullOrEmpty(this.TransactionName) && TransactionManager.TryAddPostCommitAction(this.TransactionName, (Action) (() => this.Executed((object) this, args)));
        if (!flag)
          this.Executed((object) this, args);
      }
      IExecutionStateDecorator stateDecorator = this.providerDecorator as IExecutionStateDecorator;
      if (stateDecorator == null || !(args.CommandName == "CommitTransaction") && !(args.CommandName == "RollbackTransaction") || flag && TransactionManager.TryAddPostCommitAction(this.TransactionName, (Action) (() => stateDecorator.ClearExecutionStateBag())))
        return;
      stateDecorator.ClearExecutionStateBag();
    }

    /// <summary>
    /// Gets temporary stored  data that was set before provider transaction was committed
    /// </summary>
    /// <param name="stateBagKey">The state bag key.</param>
    /// <returns></returns>
    public object GetExecutionStateData(string stateBagKey) => this.providerDecorator is IExecutionStateDecorator providerDecorator ? providerDecorator.GetExecutionStateData(stateBagKey) : throw new ApplicationException("Execution state persistence is not implemented by the current provider");

    internal bool TryGetExecutionStateData(string stateBagKey, out object value)
    {
      value = (object) null;
      if (!(this.providerDecorator is IExecutionStateDecorator providerDecorator))
        return false;
      value = providerDecorator.GetExecutionStateData(stateBagKey);
      return true;
    }

    /// <summary>
    /// Sets temporary storage of data that will be present after the provider transaction is committed
    /// </summary>
    /// <param name="stateBagKey">The state bag key.</param>
    /// <param name="data">The data.</param>
    public void SetExecutionStateData(string stateBagKey, object data)
    {
      if (!(this.providerDecorator is IExecutionStateDecorator providerDecorator))
        throw new ApplicationException("Execution state persistence is not implemented by the current provider");
      providerDecorator.SetExecutionStateData(stateBagKey, data);
    }

    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    public event EventHandler<ExecutingEventArgs> Executing;

    /// <summary>Fired after executing data method.</summary>
    public event EventHandler<ExecutedEventArgs> Executed;

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public virtual object Clone()
    {
      DataProviderBase provider = (DataProviderBase) this.MemberwiseClone();
      if (this.providerDecorator != null)
      {
        IDataProviderDecorator providerDecorator = (IDataProviderDecorator) this.providerDecorator.Clone();
        provider.providerDecorator = providerDecorator;
        provider.providerDecorator.DataProvider = provider;
      }
      this.ClearContextDependingFields(provider);
      return (object) provider;
    }

    private void ClearContextDependingFields(DataProviderBase provider)
    {
      provider.eventsKey = (string) null;
      provider.securityKey = (string) null;
      provider.notificationKey = (string) null;
      provider.relatedManagersKey = (string) null;
      provider.eventsSlot = (LocalDataStoreSlot) null;
      provider.securitySlot = (LocalDataStoreSlot) null;
      provider.notificationSlot = (LocalDataStoreSlot) null;
      provider.relatedManagersSlot = (LocalDataStoreSlot) null;
    }

    internal virtual DataProviderBase CloneInternal() => (DataProviderBase) this.MemberwiseClone();

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => this.Name;

    internal class ConfigKeys
    {
      internal const string ApplicationName = "applicationName";
    }
  }
}
