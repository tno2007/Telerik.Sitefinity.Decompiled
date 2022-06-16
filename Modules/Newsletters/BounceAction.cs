// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.BounceAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// Defines the possible actions to be undertaken when a message bounces.
  /// </summary>
  public enum BounceAction
  {
    /// <summary>Do nothing. No action will be taken.</summary>
    DoNothing = 0,
    /// <summary>User will be suspended.</summary>
    SuspendUser = 1,
    /// <summary>User will be deleted.</summary>
    DeleteUser = 2,
    /// <summary>The operation will be retried later.</summary>
    RetryLater = 4,
  }
}
