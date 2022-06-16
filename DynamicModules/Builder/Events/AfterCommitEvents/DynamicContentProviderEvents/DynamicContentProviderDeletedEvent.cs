// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents.DynamicContentProviderDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents
{
  /// <summary>
  /// Deleted event for <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> that is raised after committing transaction/save changes
  /// </summary>
  internal class DynamicContentProviderDeletedEvent : 
    DynamicContentProviderEventBase,
    IDynamicContentProviderDeletedEvent,
    IDynamicContentProviderEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents.DynamicContentProviderDeletedEvent" /> class.
    /// </summary>
    /// <param name="item">The item for which event is constructed.</param>
    /// <param name="itemState">The state of the item.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="userId">The id of the user who raises the event.</param>
    /// <param name="deletionDate">The deletion date.</param>
    public DynamicContentProviderDeletedEvent(
      DynamicContentProvider item,
      string itemState,
      string dataProviderName,
      Guid userId,
      DateTime deletionDate)
      : base(item.Id, item.GetType(), itemState, dataProviderName, userId)
    {
      this.DeletionDate = deletionDate;
      this.DynamicModuleProviderName = item.Name;
      this.ParentSecuredObjectId = item.ParentSecuredObjectId;
      this.ParentSecuredObjectType = item.ParentSecuredObjectType;
      this.ParentSecuredObjectTitle = item.ParentSecuredObjectTitle;
    }

    /// <summary>Gets or sets the deletion date.</summary>
    /// <value>The deletion date.</value>
    public DateTime DeletionDate { get; set; }

    /// <summary>
    /// Gets or sets the DynamicModuleProvider name to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> is related.
    /// </summary>
    /// <value>The DynamicModuleProvider name.</value>
    public string DynamicModuleProviderName { get; set; }

    /// <summary>Gets or sets the parent secured object id.</summary>
    /// <value>The parent secured object id.</value>
    public Guid ParentSecuredObjectId { get; set; }

    /// <summary>Gets or sets the type of the parent secured object.</summary>
    /// <value>The type of the parent secured object.</value>
    public string ParentSecuredObjectType { get; set; }

    /// <summary>Gets or sets the parent secured object title.</summary>
    /// <value>The parent secured object title.</value>
    public string ParentSecuredObjectTitle { get; set; }
  }
}
