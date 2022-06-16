// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.LifecycleResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>Lifecycle status of related data.</summary>
  public class LifecycleResponse
  {
    /// <summary>The displayed status.</summary>
    public string DisplayStatus { get; set; }

    /// <summary>The workflow status.</summary>
    public string WorkflowStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is locked
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is locked and the owner is the currently logged in user
    /// </summary>
    public bool IsLockedByMe { get; set; }
  }
}
