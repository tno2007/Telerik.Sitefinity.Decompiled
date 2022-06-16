// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryResponseEditContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// A contract for events that contain data when form is in edit mode.
  /// </summary>
  public interface IFormEntryResponseEditContext
  {
    /// <summary>Gets the response entry id.</summary>
    /// <value>The response entry id.</value>
    Guid EntryId { get; }

    /// <summary>
    /// Gets the submission time, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    /// <value>The submission time.</value>
    DateTime SubmissionUtcTime { get; }
  }
}
