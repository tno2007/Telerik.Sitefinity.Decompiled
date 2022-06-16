﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.DynamicContentCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  public class DynamicContentCreatedEvent : 
    DynamicContentEventBase,
    IDynamicContentCreatedEvent,
    IDynamicContentEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentCreatedEvent" /> class.
    /// </summary>
    public DynamicContentCreatedEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentCreatedEvent" /> class.
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
    /// <param name="creationDate">The creation date.</param>
    /// <param name="visible">if set to <c>true</c> [visible].</param>
    internal DynamicContentCreatedEvent(
      DynamicContent item,
      Type itemType,
      string action,
      string dataProviderName,
      Guid originalContentId,
      string status,
      string language,
      Guid userId,
      DateTime creationDate)
      : base(item.Id, itemType, action, dataProviderName, originalContentId, status, language)
    {
      ((IDynamicContentCreatedEvent) this).Item = item;
      this.UserId = userId;
      this.CreationDate = creationDate;
      this.Visible = item.Visible;
    }

    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    DynamicContent IDynamicContentCreatedEvent.Item { get; set; }

    /// <inheritdoc />
    public DateTime CreationDate { get; set; }
  }
}
