// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.ProfileEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>Base class for events notifying changes in profiles</summary>
  public class ProfileEventBase : EventBase, IEvent, IPropertyChangeDataEvent
  {
    private Guid userId;
    private string membershipProviderName;
    private Guid profileId;
    private string profileProviderName;
    private Type profileType;
    private IDictionary<string, PropertyChange> changedPropertyNames;

    /// <summary>
    /// Gets or sets the id of the user associated to the profile.
    /// </summary>
    /// <value>The user id.</value>
    public Guid UserId
    {
      get => this.userId;
      internal set => this.userId = value;
    }

    /// <summary>
    /// Gets or sets the name of the membership provider that the profile associated user belongs to .
    /// </summary>
    /// <value>The name of the membership provider.</value>
    public string MembershipProviderName
    {
      get => this.membershipProviderName;
      internal set => this.membershipProviderName = value;
    }

    /// <summary>Gets or sets the profile unique id.</summary>
    /// <value>The profile id.</value>
    public Guid ProfileId
    {
      get => this.profileId;
      internal set => this.profileId = value;
    }

    /// <summary>Gets or sets the name of the profile provider.</summary>
    /// <value>The name of the profile provider.</value>
    public string ProfileProviderName
    {
      get => this.profileProviderName;
      internal set => this.profileProviderName = value;
    }

    /// <summary>Gets or sets the type of the profile.</summary>
    /// <value>The type of the profile.</value>
    public Type ProfileType
    {
      get => this.profileType;
      internal set => this.profileType = value;
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
