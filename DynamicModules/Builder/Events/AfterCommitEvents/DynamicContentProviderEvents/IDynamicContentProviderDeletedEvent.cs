// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents.IDynamicContentProviderDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents
{
  /// <summary>
  /// Event for deleted <see cref="!:DynamicContentProvider" /> that is raised after committing transaction/save changes
  /// </summary>
  public interface IDynamicContentProviderDeletedEvent : 
    IDynamicContentProviderEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPostProcessingEvent
  {
    /// <summary>Gets the deletion date.</summary>
    DateTime DeletionDate { get; }

    /// <summary>Gets item's id.</summary>
    new Guid ItemId { get; }

    /// <summary>
    /// Gets the DynamicModuleProvider name to which the <see cref="!:DynamicContentProvider" /> is related.
    /// </summary>
    string DynamicModuleProviderName { get; }

    /// <summary>Gets the parent secured object id.</summary>
    /// <value>The parent secured object id.</value>
    Guid ParentSecuredObjectId { get; }

    /// <summary>Gets the type of the parent secured object.</summary>
    /// <value>The type of the parent secured object.</value>
    string ParentSecuredObjectType { get; }

    /// <summary>Gets the parent secured object title.</summary>
    /// <value>The parent secured object title.</value>
    string ParentSecuredObjectTitle { get; }
  }
}
