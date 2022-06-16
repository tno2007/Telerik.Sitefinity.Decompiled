// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Events.LinkingEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Taxonomies.Events
{
  /// <summary>
  /// A base implementation for events notifying for link/unlink operation between an item and a <see cref="!:Site" />
  /// </summary>
  public class LinkingEventBase : ILinkEvent, IDataEvent, IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Events.LinkingEventBase" /> class.
    /// </summary>
    public LinkingEventBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Events.LinkingEventBase" /> class.
    /// </summary>
    /// <param name="origin">A string indicating the origin of the event.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="action">The action.</param>
    internal LinkingEventBase(
      string origin,
      Guid itemId,
      Type itemType,
      string itemProviderName,
      Guid siteId,
      string action)
    {
      this.Origin = origin;
      this.ItemId = itemId;
      this.ItemType = itemType;
      this.ItemProviderName = itemProviderName;
      this.SiteId = siteId;
      this.Action = action;
    }

    /// <inheritdoc />
    public string Origin { get; set; }

    /// <inheritdoc />
    public Guid ItemId { get; set; }

    /// <inheritdoc />
    public Type ItemType { get; set; }

    /// <inheritdoc />
    public string ItemProviderName { get; set; }

    /// <inheritdoc />
    public Guid SiteId { get; set; }

    /// <inheritdoc />
    public string Action { get; set; }

    /// <inheritdoc />
    public string ProviderName => this.ItemProviderName;
  }
}
