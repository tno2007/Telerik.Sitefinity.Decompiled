// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  public class DynamicContentUpdatedEvent : 
    DynamicContentEventBase,
    IDynamicContentUpdatedEvent,
    IDynamicContentEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatedEvent" /> class.
    /// </summary>
    public DynamicContentUpdatedEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatedEvent" /> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="originalContentId">The original content id.</param>
    /// <param name="status">The status.</param>
    /// <param name="language">The language.</param>
    /// <param name="item">The item.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="modificationDate">The modification date.</param>
    /// <param name="visible">if set to <c>true</c> [visible].</param>
    internal DynamicContentUpdatedEvent(
      DynamicContent item,
      Type itemType,
      string action,
      string dataProviderName,
      Guid originalContentId,
      string status,
      string language,
      Guid userId,
      DateTime modificationDate)
      : base(item.Id, itemType, action, dataProviderName, originalContentId, status, language)
    {
      ((IDynamicContentUpdatedEvent) this).Item = item;
      this.UserId = userId;
      this.ModificationDate = modificationDate;
      this.Visible = item.Visible;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatedEvent" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    internal DynamicContentUpdatedEvent(DynamicContentUpdatedEvent source)
      : base((DynamicContentEventBase) source)
    {
      this.UserId = source.UserId;
      ((IDynamicContentUpdatedEvent) this).Item = ((IDynamicContentUpdatedEvent) source).Item;
      this.ModificationDate = source.ModificationDate;
      this.Visible = source.Visible;
    }

    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    DynamicContent IDynamicContentUpdatedEvent.Item { get; set; }

    /// <inheritdoc />
    public DateTime ModificationDate { get; set; }
  }
}
