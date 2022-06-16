// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.LifecycleFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Fluent;

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// Facade which provides functionality for working with lifecycle items.
  /// </summary>
  public class LifecycleFacade
  {
    private MasterFacade masterFacade;
    private TempFacade tempFacade;
    private LiveFacade liveFacade;
    private AppSettings appSettings;

    public LifecycleFacade() => this.appSettings = new AppSettings();

    public LifecycleFacade(AppSettings appSettings) => this.appSettings = appSettings;

    /// <summary>
    /// Provides the functionality for working with master items.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> to be used by the facade.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Master(ILifecycleDataItem lifecycleItem)
    {
      if (this.masterFacade == null)
        this.masterFacade = new MasterFacade(this.appSettings, lifecycleItem);
      return this.masterFacade;
    }

    /// <summary>
    /// Provides the functionality for working with master items.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the item type fo be used by the facade.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to load into master lifecycle.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Master(string itemTypeFullName, Guid itemId)
    {
      if (this.masterFacade == null)
        this.masterFacade = new MasterFacade(this.appSettings, itemTypeFullName, itemId);
      return this.masterFacade;
    }

    /// <summary>
    /// Provides the functionality for working with master items.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to load into master lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to load into master lifecycle.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Master(Type itemType, Guid itemId)
    {
      if (this.masterFacade == null)
        this.masterFacade = new MasterFacade(this.appSettings, itemType, itemId);
      return this.masterFacade;
    }

    /// <summary>
    /// Provides the functionality for working with temp items.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> to be used by the facade.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" />.
    /// </returns>
    public TempFacade Temp(ILifecycleDataItem lifecycleItem)
    {
      if (this.tempFacade == null)
        this.tempFacade = new TempFacade(this.appSettings, lifecycleItem);
      return this.tempFacade;
    }

    /// <summary>
    /// Provides the functionality for working with temp items.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the item type fo be used by the facade.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to load into master lifecycle.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" />.
    /// </returns>
    public TempFacade Temp(string itemTypeFullName, Guid itemId)
    {
      if (this.tempFacade == null)
        this.tempFacade = new TempFacade(this.appSettings, itemTypeFullName, itemId);
      return this.tempFacade;
    }

    /// <summary>
    /// Provides the functionality for working with temp items.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to load into temp lifecycle.
    /// </param>
    /// <param name="itemId">Id of the item to load into temp lifecycle.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" />.
    /// </returns>
    public TempFacade Temp(Type itemType, Guid itemId)
    {
      if (this.tempFacade == null)
        this.tempFacade = new TempFacade(this.appSettings, itemType, itemId);
      return this.tempFacade;
    }

    /// <summary>
    /// Provides the functionality for working with live items.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> to be used by the facade.
    /// </param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" />.
    /// </returns>
    public LiveFacade Live(ILifecycleDataItem lifecycleItem)
    {
      if (this.liveFacade == null)
        this.liveFacade = new LiveFacade(this.appSettings, lifecycleItem);
      return this.liveFacade;
    }

    /// <summary>
    /// Provides the functionality for working with live items.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the item type fo be used by the facade.
    /// </param>
    /// <param name="itemId">Id of the item to load into live lifecycle.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" />.
    /// </returns>
    public LiveFacade Live(string itemTypeFullName, Guid itemId)
    {
      if (this.liveFacade == null)
        this.liveFacade = new LiveFacade(this.appSettings, itemTypeFullName, itemId);
      return this.liveFacade;
    }

    /// <summary>
    /// Provides the functionality for working with live items.
    /// </summary>
    /// <param name="itemType">
    /// Type of the item to load into live lifecycle.
    /// </param>
    /// <param name="itemId">Id of the item to load into live lifecycle.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.LiveFacade" />.
    /// </returns>
    public LiveFacade Live(Type itemType, Guid itemId)
    {
      if (this.liveFacade == null)
        this.liveFacade = new LiveFacade(this.appSettings, itemType, itemId);
      return this.liveFacade;
    }
  }
}
