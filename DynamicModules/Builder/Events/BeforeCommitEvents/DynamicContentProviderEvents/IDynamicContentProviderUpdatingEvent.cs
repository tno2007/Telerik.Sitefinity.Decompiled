// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.DynamicContentProviderEvents.IDynamicContentProviderUpdatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.DynamicContentProviderEvents
{
  /// <summary>
  /// Event for updating <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> that is raised before committing transaction/save changes
  /// </summary>
  public interface IDynamicContentProviderUpdatingEvent : 
    IDynamicContentProviderEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPreProcessingEvent
  {
    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> processed in the event.
    /// </summary>
    /// <value>The item.</value>
    DynamicContentProvider Item { get; set; }
  }
}
