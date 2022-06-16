// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Events.ILinkEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Taxonomies.Events
{
  /// <summary>
  /// A base contract for events notifying for link/unlink operation between an item and a <see cref="!:Site" />.
  /// </summary>
  public interface ILinkEvent : IDataEvent, IEvent
  {
    /// <summary>Gets the item id.</summary>
    new Guid ItemId { get; }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    new Type ItemType { get; }

    /// <summary>Gets the name of the item provider.</summary>
    /// <value>The name of the item provider.</value>
    string ItemProviderName { get; }

    /// <summary>Gets the site id.</summary>
    Guid SiteId { get; }
  }
}
