// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataProviderCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// The default implementation of the IDataProviderCreatedEvent event.
  /// </summary>
  internal class DataProviderCreatedEvent : IDataProviderCreatedEvent, IEvent
  {
    /// <summary>
    /// Gets or sets value specifying the origin of the event.
    /// </summary>
    /// <value></value>
    public string Origin { get; set; }

    /// <summary>
    /// Gets or sets the type of the manager where the provider was created.
    /// </summary>
    /// <value>The type of the manager.</value>
    public Type ManagerType { get; set; }

    /// <summary>
    /// Gets or sets the name of the newly created data provider.
    /// </summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the name of the dynamic module.</summary>
    /// <value>The name of the module.</value>
    public string ModuleName { get; set; }
  }
}
