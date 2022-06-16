// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.GeoLocatableEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  internal class GeoLocatableEvent : IGeoLocatableEvent, IDataEvent, IEvent
  {
    public string Origin { get; set; }

    /// <summary>Gets the action.</summary>
    public string Action { get; set; }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public Type ItemType { get; set; }

    /// <summary>Gets the item id.</summary>
    public Guid ItemId { get; set; }

    /// <summary>Gets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    public Dictionary<string, Address> GeoLocations { get; set; }
  }
}
