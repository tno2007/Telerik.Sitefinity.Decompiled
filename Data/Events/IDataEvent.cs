// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IDataEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// A contract for event notification containing minimal information about modified items.
  /// </summary>
  public interface IDataEvent : IEvent
  {
    /// <summary>Gets the action.</summary>
    string Action { get; }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    Type ItemType { get; }

    /// <summary>Gets the item id.</summary>
    Guid ItemId { get; }

    /// <summary>Gets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; }
  }
}
