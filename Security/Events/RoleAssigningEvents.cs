// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.RoleAssignEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>Base class for events notifying changes in users</summary>
  public class RoleAssignEventBase : EventBase, IEvent
  {
    private readonly UserLinkInfo userLinkInfo;

    internal RoleAssignEventBase(UserLinkInfo userLinkInfo) => this.userLinkInfo = userLinkInfo;

    /// <summary>Gets or sets the role id.</summary>
    /// <value>The role id.</value>
    public Guid RoleId => this.userLinkInfo.RoleId;

    /// <summary>Gets or sets the name of the role.</summary>
    /// <value>The name of the role.</value>
    public string RoleName => this.userLinkInfo.RoleName;

    /// <summary>Gets or sets the name of the role provider.</summary>
    /// <value>The name of the role provider.</value>
    public string RoleProviderName => this.userLinkInfo.RoleProviderName;

    /// <summary>Gets or sets the id of the user.</summary>
    /// <value>The user id.</value>
    public Guid UserId => this.userLinkInfo.UserId;

    /// <summary>Gets or sets the name of the user provider.</summary>
    /// <value>The name of the user provider.</value>
    public string MembershipProviderName => this.userLinkInfo.MembershipProviderName;
  }
}
