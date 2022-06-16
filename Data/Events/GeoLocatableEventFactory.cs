// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.GeoLocatableEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  internal class GeoLocatableEventFactory
  {
    /// <summary>Creates the event.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public static IEvent CreateEvent(
      IGeoLocatable dataItem,
      SecurityConstants.TransactionActionType action)
    {
      string dataProviderName = string.Empty;
      if (dataItem.Provider is IDataProviderBase provider)
        dataProviderName = provider.Name;
      Dictionary<string, Address> addressFields = dataItem.GetAddressFields();
      return addressFields.Count > 0 ? (IEvent) GeoLocatableEventFactory.CreateEvent(dataItem, dataItem.GetType(), action, dataProviderName, addressFields) : (IEvent) null;
    }

    /// <summary>Creates the event.</summary>
    /// <param name="item">The item.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="addresses">The addresses.</param>
    /// <returns></returns>
    private static GeoLocatableEvent CreateEvent(
      IGeoLocatable item,
      Type itemType,
      SecurityConstants.TransactionActionType action,
      string dataProviderName,
      Dictionary<string, Address> addresses)
    {
      ClaimsManager.GetCurrentUserId();
      string str = action.ToString();
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          GeoLocatableCreatedEvent locatableCreatedEvent = new GeoLocatableCreatedEvent();
          locatableCreatedEvent.Action = str;
          locatableCreatedEvent.ItemType = itemType;
          locatableCreatedEvent.ItemId = item.Id;
          locatableCreatedEvent.ProviderName = dataProviderName;
          locatableCreatedEvent.GeoLocations = addresses;
          return (GeoLocatableEvent) locatableCreatedEvent;
        case SecurityConstants.TransactionActionType.Updated:
          GeoLocatableUpdatedEvent locatableUpdatedEvent = new GeoLocatableUpdatedEvent();
          locatableUpdatedEvent.Action = str;
          locatableUpdatedEvent.ItemType = itemType;
          locatableUpdatedEvent.ItemId = item.Id;
          locatableUpdatedEvent.ProviderName = dataProviderName;
          locatableUpdatedEvent.GeoLocations = addresses;
          return (GeoLocatableEvent) locatableUpdatedEvent;
        case SecurityConstants.TransactionActionType.Deleted:
          GeoLocatableDeletedEvent locatableDeletedEvent = new GeoLocatableDeletedEvent();
          locatableDeletedEvent.Action = str;
          locatableDeletedEvent.ItemType = itemType;
          locatableDeletedEvent.ItemId = item.Id;
          locatableDeletedEvent.ProviderName = dataProviderName;
          locatableDeletedEvent.GeoLocations = addresses;
          return (GeoLocatableEvent) locatableDeletedEvent;
        default:
          return new GeoLocatableEvent()
          {
            Action = str,
            ItemType = itemType,
            ItemId = item.Id,
            ProviderName = dataProviderName,
            GeoLocations = addresses
          };
      }
    }
  }
}
