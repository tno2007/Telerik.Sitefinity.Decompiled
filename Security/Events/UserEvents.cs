// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.UserEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>Base class for events notifying changes in users</summary>
  public class UserEventBase : EventBase, IPropertyChangeDataEvent
  {
    private Guid userId;
    private string userName;
    private string membershipProviderName;
    private bool isApproved;
    private string email;
    private string password;
    private int passwordFormat;
    private string externalProviderName;
    private string externalId;
    private IDictionary<string, PropertyChange> changedPropertyNames;

    /// <summary>Gets or sets the id of the user.</summary>
    /// <value>The user id.</value>
    public Guid UserId
    {
      get => this.userId;
      internal set => this.userId = value;
    }

    /// <summary>Gets or sets the UserName of the user.</summary>
    /// <value>The user UserName.</value>
    public string UserName
    {
      get => this.userName;
      internal set => this.userName = value;
    }

    /// <summary>
    /// Gets or sets the name of the membership provider that the associated user belongs to.
    /// </summary>
    /// <value>The name of the membership provider.</value>
    public string MembershipProviderName
    {
      get => this.membershipProviderName;
      internal set => this.membershipProviderName = value;
    }

    /// <summary>Gets or sets the approval status of the user.</summary>
    /// <value>The approval status of the user.</value>
    public bool IsApproved
    {
      get => this.isApproved;
      internal set => this.isApproved = value;
    }

    /// <summary>Gets or sets the Email of the user.</summary>
    /// <value>The Email of the user.</value>
    public string Email
    {
      get => this.email;
      internal set => this.email = value;
    }

    /// <summary>Gets or sets the Password of the user.</summary>
    /// <value>The Password of the user.</value>
    public string Password
    {
      get => this.password;
      internal set => this.password = value;
    }

    /// <summary>Gets or sets the PasswordFormat of the user.</summary>
    /// <value>The PasswordFormat of the user.</value>
    public int PasswordFormat
    {
      get => this.passwordFormat;
      internal set => this.passwordFormat = value;
    }

    /// <summary>
    /// Gets or sets the name of the external provider that the associated user belongs to.
    /// </summary>
    /// <value>The name of the external provider.</value>
    public string ExternalProviderName
    {
      get => this.externalProviderName;
      internal set => this.externalProviderName = value;
    }

    /// <summary>
    /// Gets or sets the id of the user that is associated to it in the external provider.
    /// </summary>
    /// <value>The id of the user in the external provider.</value>
    public string ExternalId
    {
      get => this.externalId;
      internal set => this.externalId = value;
    }

    /// <inheritdoc />
    public IDictionary<string, PropertyChange> ChangedProperties
    {
      get
      {
        if (this.changedPropertyNames == null)
          this.changedPropertyNames = (IDictionary<string, PropertyChange>) new Dictionary<string, PropertyChange>();
        return this.changedPropertyNames;
      }
    }
  }
}
