// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// Provides fluent API for lifecycle operations of the master items.
  /// </summary>
  public class MasterFacade : BaseLifecycleFacade, IMasterLifecycleFacade
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.
    /// </param>
    public MasterFacade(ILifecycleDataItem lifecycleItem)
      : base(lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="lifecycleItem">The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.</param>
    public MasterFacade(AppSettings appSettings, ILifecycleDataItem lifecycleItem)
      : base(appSettings, lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public MasterFacade(string itemTypeFullName, Guid itemId)
      : base(itemTypeFullName, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemTypeFullName">The full name of the type of the item to be managed through lifecycle.</param>
    /// <param name="itemId">Id of the item to be managed through lifecycle.</param>
    public MasterFacade(AppSettings appSettings, string itemTypeFullName, Guid itemId)
      : base(appSettings, itemTypeFullName, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public MasterFacade(Type itemType, Guid itemId)
      : base(itemType, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" /> with the
    /// specified type and id of the item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemType">Type of the item to be managed through lifecycle.</param>
    /// <param name="itemId">Id of the item to be managed through lifecycle.</param>
    public MasterFacade(AppSettings appSettings, Type itemType, Guid itemId)
      : base(appSettings, itemType, itemId)
    {
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public bool IsCheckedOut(CultureInfo culture = null) => this.Lifecycle.IsCheckedOut(this.LifecycleItem, culture);

    /// <summary>
    /// Gets the temp version of the master item and returns the instance of
    /// <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> that provides the functionality for working
    /// with the items in temp state.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" />.
    /// </returns>
    public TempFacade GetTemp() => new TempFacade(this.AppSettings, this.Lifecycle.GetTemp(this.LifecycleItem));

    /// <summary>
    /// Gets the live version of the master item and returns the instance
    /// of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> that provides the functionality for
    /// working with the live items.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" />.
    /// </returns>
    public LiveFacade GetLive() => new LiveFacade(this.AppSettings, this.Lifecycle.GetLive(this.LifecycleItem));

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
    public MasterFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return this;
    }

    /// <summary>
    /// Determines weather there is a live version of this master item;
    /// has the item been already published.
    /// </summary>
    /// <returns>True if the item has been published; otherwise false.</returns>
    public bool IsPublished()
    {
      ILifecycleDataItem live = this.Lifecycle.GetLive(this.LifecycleItem);
      return live != null && live.IsPublished();
    }

    /// <summary>
    /// Publishes the item in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <param name="excludeVersioning">if set to <c>true</c> exclude versioning of that item. The default value is false.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" /> with loaded live item.
    /// </returns>
    public LiveFacade Publish(CultureInfo culture = null, bool excludeVersioning = false)
    {
      ILifecycleDataItem lifecycleItem = this.Lifecycle.Publish(this.LifecycleItem);
      if (!excludeVersioning)
      {
        Guid ID = lifecycleItem is ILifecycleDataItemGeneric ? ((ILifecycleDataItemGeneric) lifecycleItem).OriginalContentId : lifecycleItem.Id;
        AllFacadesHelper.CreateVersion(this.AppSettings, (IDataItem) lifecycleItem, ID, ContentLifecycleStatus.Live);
      }
      this.AppSettings.TrackPublishedItem((IDataItem) lifecycleItem);
      return new LiveFacade(this.AppSettings, lifecycleItem);
    }

    public LiveFacade PublishWithSpecificDate(
      DateTime publicationDate,
      CultureInfo culture = null,
      bool excludeVersioning = false)
    {
      ILifecycleDataItem lifecycleItem = this.Lifecycle.PublishWithSpecificDate(this.LifecycleItem, publicationDate, culture);
      if (!excludeVersioning)
      {
        Guid ID = lifecycleItem is ILifecycleDataItemGeneric ? ((ILifecycleDataItemGeneric) lifecycleItem).OriginalContentId : lifecycleItem.Id;
        AllFacadesHelper.CreateVersion(this.AppSettings, (IDataItem) lifecycleItem, ID, ContentLifecycleStatus.Live);
      }
      this.AppSettings.TrackPublishedItem((IDataItem) lifecycleItem);
      return new LiveFacade(this.AppSettings, lifecycleItem);
    }

    /// <summary>Deletes the item.</summary>
    /// <param name="item">Item to be deleted.</param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Delete(CultureInfo culture)
    {
      this.LifecycleManager.DeleteItem((object) this.LifecycleItem, culture);
      return this;
    }

    bool IMasterLifecycleFacade.IsCheckedOut() => this.IsCheckedOut((CultureInfo) null);

    ILiveLifecycleFacade IMasterLifecycleFacade.Publish() => (ILiveLifecycleFacade) this.Publish((CultureInfo) null, false);
  }
}
