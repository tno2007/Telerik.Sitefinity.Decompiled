// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Fluent;

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// Provides fluent API for lifecycle operations of the live items.
  /// </summary>
  public class LiveFacade : BaseLifecycleFacade, ILiveLifecycleFacade
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.
    /// </param>
    public LiveFacade(ILifecycleDataItem lifecycleItem)
      : base(lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="lifecycleItem">The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.</param>
    public LiveFacade(AppSettings appSettings, ILifecycleDataItem lifecycleItem)
      : base(appSettings, lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public LiveFacade(string itemTypeFullName, Guid itemId)
      : base(itemTypeFullName, itemId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> class.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemTypeFullName">Full name of the item type.</param>
    /// <param name="itemId">The item id.</param>
    public LiveFacade(AppSettings appSettings, string itemTypeFullName, Guid itemId)
      : base(appSettings, itemTypeFullName, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public LiveFacade(Type itemType, Guid itemId)
      : base(itemType, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemType">Type of the item to be managed through lifecycle.</param>
    /// <param name="itemId">Id of the item to be managed through lifecycle.</param>
    public LiveFacade(AppSettings appSettings, Type itemType, Guid itemId)
      : base(appSettings, itemType, itemId)
    {
    }

    /// <summary>
    /// Gets the state of the faced cast to the specified type and returns the
    /// facade.
    /// </summary>
    /// <typeparam name="T">
    /// The type to which the state should be cast.
    /// </typeparam>
    /// <param name="currentItem">
    /// Out parameter in which the state will be populated.
    /// </param>
    /// <returns>The instance of the facade.</returns>
    public LiveFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return this;
    }

    /// <summary>
    /// Makes the live item inaccessbile (only master will exist after).
    /// </summary>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Unpublish() => this.Unpublish((CultureInfo) null);

    /// <summary>
    /// Makes the live item inaccessbile for the given culture (only master will exist).
    /// </summary>
    /// <param name="culture">The culture that ought to be unpublished.</param>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Unpublish(CultureInfo culture) => new MasterFacade(this.AppSettings, this.Lifecycle.Unpublish(this.LifecycleItem, culture) ?? throw new InvalidOperationException());

    /// <summary>
    /// Copies the public state to the draft state of the content item. Warning: discards any changes in the draft
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public MasterFacade CopyToMaster() => new MasterFacade(this.AppSettings, this.Lifecycle.Edit(this.LifecycleItem));

    /// <summary>
    /// Exists the temp facade and transfers the item to the master facade.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Done() => new MasterFacade(this.AppSettings, this.Lifecycle.GetMaster(this.LifecycleItem));
  }
}
