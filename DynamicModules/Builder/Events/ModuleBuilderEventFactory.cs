// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.ModuleBuilderEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// Factory for constructing events for module builder <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> objects that.
  /// </summary>
  internal abstract class ModuleBuilderEventFactory : IModuleBuilderEventFactory
  {
    /// <summary>Constructs event from item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item.</returns>
    public IModuleBuilderEvent CreateEvent(
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      switch (dataItem)
      {
        case Permission permission:
          return this.CreatePermissionEvent(permission, actionType, dataProviderName);
        case DynamicContentProvider dynamicContentProvider:
          return this.CreateDynamicContentProviderEvent(dynamicContentProvider, actionType, dataProviderName);
        default:
          throw new NotSupportedException("Not supported data type " + dataItem.GetType().FullName.ToString());
      }
    }

    /// <summary>Constructs event from item for specific language.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <param name="language">The language for which the event is constructed.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item for specified language.</returns>
    public IModuleBuilderEvent CreateEvent(
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName,
      string language)
    {
      switch (dataItem)
      {
        case Permission _:
          return this.CreateEvent(dataItem, actionType, dataProviderName);
        case DynamicContentProvider dynamicContentProvider:
          return this.CreateEvent((IDataItem) dynamicContentProvider, actionType, dataProviderName);
        default:
          throw new NotSupportedException("Not supported data type " + dataItem.GetType().FullName.ToString());
      }
    }

    public IModuleBuilderEvent CreatePermissionEvent(
      Permission permission,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      PermissionDataEventBase permissionEventByAction = this.CreatePermissionEventByAction(permission, actionType, dataProviderName);
      ModuleBuilderEventFactory.SetEventProperties((IPropertyChangeDataEvent) permissionEventByAction, (IDataItem) permission, actionType);
      permissionEventByAction.Title = DataEventFactory.GetItemTitle((IDataItem) permission, actionType.ToString(), (CultureInfo) null);
      return (IModuleBuilderEvent) permissionEventByAction;
    }

    public IModuleBuilderEvent CreateDynamicContentProviderEvent(
      DynamicContentProvider dynamicContentProvider,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      DynamicContentProviderEventBase providerEventByAction = this.CreateDynamicContentProviderEventByAction(dynamicContentProvider, actionType, dataProviderName);
      ModuleBuilderEventFactory.SetEventProperties((IPropertyChangeDataEvent) providerEventByAction, (IDataItem) dynamicContentProvider, actionType);
      providerEventByAction.Title = DataEventFactory.GetItemTitle((IDataItem) dynamicContentProvider, actionType.ToString(), (CultureInfo) null);
      return (IModuleBuilderEvent) providerEventByAction;
    }

    public abstract PermissionDataEventBase CreatePermissionEventByAction(
      Permission dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName);

    public abstract DynamicContentProviderEventBase CreateDynamicContentProviderEventByAction(
      DynamicContentProvider dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName);

    internal static void SetEventProperties(
      IPropertyChangeDataEvent dynamicContentEvent,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType action)
    {
      if (action != SecurityConstants.TransactionActionType.Updated)
        return;
      DataEventFactory.SetChangedProperties(dynamicContentEvent, dataItem, (CultureInfo) null);
    }
  }
}
