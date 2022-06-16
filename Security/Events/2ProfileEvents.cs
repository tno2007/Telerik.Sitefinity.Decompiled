// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.ProfileUpdating
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>
  /// an event notifying that a Sitefinity profile is being modified
  /// </summary>
  public class ProfileUpdating : ProfileEventBase
  {
    private UserProfile profile;

    /// <summary>Gets or Sets users profile</summary>
    public UserProfile Profile
    {
      get => this.profile;
      internal set => this.profile = value;
    }
  }
}
