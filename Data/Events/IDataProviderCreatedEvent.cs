// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IDataProviderCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// A contract for event notification containing information about a newly created data provider.
  /// </summary>
  public interface IDataProviderCreatedEvent : IEvent
  {
    /// <summary>
    /// Gets the type of the manager where the provider was created.
    /// </summary>
    /// <value>The type of the manager.</value>
    Type ManagerType { get; }

    /// <summary>Gets the name of the newly created data provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; }

    /// <summary>Gets the name of the dynamic module.</summary>
    /// <value>The name of the module.</value>
    string ModuleName { get; }
  }
}
