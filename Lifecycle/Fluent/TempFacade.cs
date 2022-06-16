// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.TempFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// Provides fluent API for lifecycle operations of the live items.
  /// </summary>
  public class TempFacade : BaseLifecycleFacade, ITempLifecycleFacade
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="lifecycleItem">
    /// The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.
    /// </param>
    public TempFacade(ILifecycleDataItem lifecycleItem)
      : base(lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> with the specified
    /// data item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="lifecycleItem">The instance of the <see cref="!:ILifecycleItem" /> to be used by the facade.</param>
    public TempFacade(AppSettings appSettings, ILifecycleDataItem lifecycleItem)
      : base(appSettings, lifecycleItem)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="itemTypeFullName">
    /// The full name of the type of the item to be managed through lifecycle.
    /// </param>
    /// <param name="itemId">
    /// Id of the item to be managed through lifecycle.
    /// </param>
    public TempFacade(string itemTypeFullName, Guid itemId)
      : base(itemTypeFullName, itemId)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> with the
    /// specified type full name and id of the item.
    /// </summary>
    /// <param name="settings">The appSettings.</param>
    /// <param name="itemTypeFullName">The full name of the type of the item to be managed through lifecycle.</param>
    /// <param name="itemId">Id of the item to be managed through lifecycle.</param>
    public TempFacade(AppSettings appSettings, string itemTypeFullName, Guid itemId)
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
    public TempFacade(Type itemType, Guid itemId)
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
    public TempFacade(AppSettings appSettings, Type itemType, Guid itemId)
      : base(appSettings, itemType, itemId)
    {
    }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="excludeVersioning">This argument is ignored and here is only for backwards compatibility purposes.</param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> for chaning purposes.
    /// </returns>
    public MasterFacade CheckIn(bool excludeVersioning)
    {
      ILifecycleDataItem lifecycleItem = this.Lifecycle.CheckIn(this.LifecycleItem, this.GetTempCulture());
      ++lifecycleItem.Version;
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.AppSettings, (IDataItem) this.Get(), lifecycleItem.Id, ContentLifecycleStatus.Master);
      this.AppSettings.TrackModifiedItem((IDataItem) lifecycleItem);
      return new MasterFacade(this.AppSettings, lifecycleItem);
    }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>
    /// In monolingual the culture is ignored. In multilingual mode if
    /// null - the current ui culture will be used.
    /// </remarks></param>
    /// <param name="deleteTemp">if set to <c>true</c> [delete temporary].</param>
    /// <param name="excludeVersioning">if set to <c>true</c> [exclude versioning].</param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.TempFacade" /> for chaning purposes.
    /// </returns>
    public MasterFacade CheckIn(
      CultureInfo culture = null,
      bool deleteTemp = true,
      bool excludeVersioning = false)
    {
      ILifecycleDataItem lifecycleItem = this.Lifecycle.CheckIn(this.LifecycleItem, culture, deleteTemp);
      ++lifecycleItem.Version;
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.AppSettings, (IDataItem) this.Get(), lifecycleItem.Id, ContentLifecycleStatus.Master);
      this.AppSettings.TrackModifiedItem((IDataItem) lifecycleItem);
      return new MasterFacade(this.AppSettings, lifecycleItem);
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
    public TempFacade GetAs<T>(out T currentItem) where T : class
    {
      currentItem = this.GetAs<T>();
      return this;
    }

    /// <summary>Copies to master.</summary>
    /// <param name="excludeVersioning">if set to <c>true</c> [exclude versioning].</param>
    /// <summary>Copies to master.</summary>
    /// <param name="excludeVersioning">if set to <c>true</c> [exclude versioning].</param>
    /// <returns></returns>
    public MasterFacade CopyToMaster(bool excludeVersioning = false)
    {
      ILifecycleDataItem lifecycleDataItem = this.Lifecycle.CheckIn(this.LifecycleItem, this.GetTempCulture(), false);
      if (!excludeVersioning)
        AllFacadesHelper.CreateVersion(this.AppSettings, (IDataItem) this.Get(), lifecycleDataItem.Id, ContentLifecycleStatus.Master);
      this.AppSettings.TrackModifiedItem((IDataItem) lifecycleDataItem);
      return this.Done();
    }

    /// <summary>
    /// Exists the temp facade and transfers the item to the master facade.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Fluent.MasterFacade" />.
    /// </returns>
    public MasterFacade Done() => new MasterFacade(this.AppSettings, this.Lifecycle.GetMaster(this.LifecycleItem));

    /// <summary>Gets the culture of the temp lifecycle item.</summary>
    /// <returns>
    /// The instance of <see cref="T:System.Globalization.CultureInfo" /> which represents
    /// the culture of the temp lifecycle item.
    /// </returns>
    private CultureInfo GetTempCulture()
    {
      IEnumerable<LanguageData> source = this.LifecycleItem.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (l => !l.Language.IsNullOrEmpty()));
      return source.Any<LanguageData>() ? new CultureInfo(source.First<LanguageData>().Language) : (CultureInfo) null;
    }

    IMasterLifecycleFacade ITempLifecycleFacade.CopyToMaster(
      bool excludeVersioning)
    {
      return (IMasterLifecycleFacade) this.CopyToMaster(excludeVersioning);
    }

    IMasterLifecycleFacade ITempLifecycleFacade.CheckIn(
      bool excludeVersioning)
    {
      return (IMasterLifecycleFacade) this.CheckIn(excludeVersioning);
    }
  }
}
