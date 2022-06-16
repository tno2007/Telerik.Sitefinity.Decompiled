// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.IDynamicContentCreatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  /// <summary>
  /// Event for creating dynamic content that is raised before committing transaction/save changes
  /// </summary>
  public interface IDynamicContentCreatingEvent : IDynamicContentEvent, IEvent, IPreProcessingEvent
  {
    /// <summary>Gets or sets the dynamic item processed in the event.</summary>
    /// <value>The item.</value>
    DynamicContent Item { get; set; }
  }
}
