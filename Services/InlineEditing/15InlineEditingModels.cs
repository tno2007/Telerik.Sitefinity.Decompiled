// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.LifecycleStatusModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>
  /// Contains information about the LifecycleStatus of content items when in Inline Editing mode
  /// </summary>
  public class LifecycleStatusModel
  {
    public string DisplayStatus { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsEditable { get; set; }

    public bool IsStatusEditable { get; set; }

    public bool IsLocked { get; set; }

    public bool IsPublished { get; set; }

    public bool IsLockedByMe { get; set; }

    public string LockedByUsername { get; set; }

    public string WorkflowStatus { get; set; }
  }
}
