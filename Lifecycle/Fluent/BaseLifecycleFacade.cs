// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// This is the base class to be used by all lifecycle facades.
  /// </summary>
  public abstract class BaseLifecycleFacade
  {
    private ILifecycleManager lifecycleManager;
    private ILifecycleDecorator lifecycle;
    private ILifecycleDataItem lifecycleItem;
    private Type lifecycleItemType;
    private AppSettings appSettings;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> with the
    /// specified instance of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" />.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> which will be used
    /// by this facade.
    /// </param>
    public BaseLifecycleFacade(ILifecycleDataItem lifecycleItem) => this.SetIntialState(new AppSettings(), lifecycleItem);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> with the
    /// specified instance of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" />.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="lifecycleItem">The instance of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> which will be used
    /// by this facade.</param>
    public BaseLifecycleFacade(AppSettings appSettings, ILifecycleDataItem lifecycleItem) => this.SetIntialState(appSettings, lifecycleItem);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public BaseLifecycleFacade(string itemTypeFullName, Guid itemId) => this.SetIntialState(new AppSettings(), itemTypeFullName, itemId);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> class.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemTypeFullName">Full name of the item type.</param>
    /// <param name="itemId">The item id.</param>
    public BaseLifecycleFacade(AppSettings appSettings, string itemTypeFullName, Guid itemId)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException("settings");
      this.SetIntialState(appSettings, itemTypeFullName, itemId);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public BaseLifecycleFacade(Type itemType, Guid itemId) => this.SetInitialState(new AppSettings(), itemType, itemId);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.BaseLifecycleFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemType">Type of the item to be managed through lifecycle.</param>
    /// <param name="itemId">Id of the item to be managed through lifecycle.</param>
    public BaseLifecycleFacade(AppSettings appSettings, Type itemType, Guid itemId) => this.SetInitialState(appSettings, itemType, itemId);

    public AppSettings AppSettings => this.appSettings;

    /// <summary>
    /// Gets the lifecycle item that facades are working on; this is the
    /// state of the facade.
    /// </summary>
    protected ILifecycleDataItem LifecycleItem => this.lifecycleItem;

    /// <summary>
    /// Gets the type of the lifecycle item that facades are working on.
    /// </summary>
    protected Type LifecycleItemType => this.lifecycleItemType;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDecorator" /> that this
    /// fluent API wraps.
    /// </summary>
    protected ILifecycleDecorator Lifecycle
    {
      get
      {
        if (this.lifecycle == null)
          this.lifecycle = this.LifecycleManager.Lifecycle;
        return this.lifecycle;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleManager" />.
    /// </summary>
    protected ILifecycleManager LifecycleManager
    {
      get
      {
        if (this.lifecycleManager == null)
          this.lifecycleManager = this.GetManager();
        return this.lifecycleManager;
      }
    }

    /// <summary>
    /// Gets the state of the facade and casts it to the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type to which the state should be cast.
    /// </typeparam>
    /// <remarks>
    /// If the state cannot be cast to the specified type, null will be returned.
    /// </remarks>
    /// <returns>The state of the facade.</returns>
    public T GetAs<T>() where T : class => this.Get() as T;

    /// <summary>Gets the state of the facade.</summary>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" />.
    /// </returns>
    public ILifecycleDataItem Get() => this.LifecycleItem;

    /// <summary>
    /// Saves all the made changes by committing the transaction.
    /// </summary>
    public bool SaveChanges()
    {
      if (!this.AppSettings.IsGlobalTransaction)
        TransactionManager.CommitTransaction(this.LifecycleManager.TransactionName);
      return true;
    }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleManager" /> that manages the
    /// lifecycle item.
    /// </summary>
    /// <returns></returns>
    private ILifecycleManager GetManager()
    {
      if (this.lifecycleItemType == (Type) null)
        this.lifecycleItemType = this.LifecycleItem.GetType();
      return (ILifecycleManager) ManagerBase.GetMappedManagerInTransaction(this.LifecycleItemType, this.appSettings.ContentProviderName, this.appSettings.TransactionName);
    }

    private void SetIntialState(AppSettings appSettings, string itemTypeFullName, Guid itemId)
    {
      if (appSettings == null)
        throw new ArgumentNullException("settings");
      if (string.IsNullOrEmpty(itemTypeFullName))
        throw new ArgumentNullException(nameof (itemTypeFullName));
      if (itemId == Guid.Empty)
        throw new ArgumentNullException(nameof (itemId));
      this.lifecycleItemType = TypeResolutionService.ResolveType(itemTypeFullName);
      this.appSettings = appSettings;
      if (!(this.LifecycleManager.GetItem(this.LifecycleItemType, itemId) is ILifecycleDataItem lifecycleDataItem))
        throw new NotSupportedException(string.Format("The item of type '{0}' does not support lifecycle as it does not implement ILifecycleDataItem interface.", (object) this.LifecycleItemType.FullName));
      this.lifecycleItem = lifecycleDataItem;
    }

    private void SetInitialState(AppSettings appSettings, Type itemType, Guid itemId)
    {
      if (appSettings == null)
        throw new ArgumentNullException("settings");
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemId == Guid.Empty)
        throw new ArgumentNullException(nameof (itemId));
      this.lifecycleItemType = itemType;
      this.appSettings = appSettings;
      if (!(this.LifecycleManager.GetItem(itemType, itemId) is ILifecycleDataItem lifecycleDataItem))
        throw new NotSupportedException(string.Format("The item of type '{0}' does not support lifecycle as it does not implement ILifecycleDataItem interface.", (object) itemType.FullName));
      this.lifecycleItem = lifecycleDataItem;
    }

    private void SetIntialState(AppSettings appSettings, ILifecycleDataItem lifecycleItem)
    {
      if (lifecycleItem == null)
        throw new ArgumentNullException(nameof (lifecycleItem));
      if (appSettings == null)
        throw new ArgumentNullException("settings");
      this.lifecycleItem = lifecycleItem;
      this.appSettings = appSettings;
    }
  }
}
