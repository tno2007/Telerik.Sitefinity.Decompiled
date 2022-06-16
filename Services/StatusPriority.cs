// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusPriority
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Status priorities</summary>
  [Obsolete("Not used any more. The priority property of the status provider is now integer.")]
  public enum StatusPriority
  {
    /// <summary>Priority High</summary>
    High,
    /// <summary>Priority Medium</summary>
    Medium,
    /// <summary>Priority Low</summary>
    Low,
  }
}
