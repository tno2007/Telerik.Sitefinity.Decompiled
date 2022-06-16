// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IGeoLocatableEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  internal interface IGeoLocatableEvent : IDataEvent, IEvent
  {
    /// <summary>
    /// Gets or sets the geo locations belonging to this instance.
    /// </summary>
    Dictionary<string, Address> GeoLocations { get; set; }
  }
}
