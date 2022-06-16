// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.UserUpdated
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>
  /// an event notifying that a Sitefinity user was modified
  /// </summary>
  public class UserUpdated : UserEventBase
  {
    private bool approvalStatusChanged;
    private bool passwordChanged;

    /// <summary>
    /// Gets or sets a flag which indicates whether approval status of a Sitefinity user is changed
    /// </summary>
    public bool ApprovalStatusChanged
    {
      get => this.approvalStatusChanged;
      internal set => this.approvalStatusChanged = value;
    }

    /// <summary>
    /// Gets or sets a flag which indicates whether password of a Sitefinity user is changed
    /// </summary>
    public bool PasswordChanged
    {
      get => this.passwordChanged;
      internal set => this.passwordChanged = value;
    }
  }
}
