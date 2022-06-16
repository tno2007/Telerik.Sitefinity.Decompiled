// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserDisplayInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// This class is used to serialize the MembershipUser display data through WCF service.
  /// </summary>
  [DataContract]
  [ManagerType("Telerik.Sitefinity.Security.UserManager")]
  public class WcfMembershipUserDisplayInfo : WcfItemBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserDisplayInfo" /> class.
    /// </summary>
    public WcfMembershipUserDisplayInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserDisplayInfo" /> class.
    /// </summary>
    /// <param name="user">The actual user to initialize with.</param>
    public WcfMembershipUserDisplayInfo(User user)
      : this(user.ProviderName, user.ProviderUserKey.ToString(), UserProfilesHelper.GetUserDisplayName(user.Id))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserDisplayInfo" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="userID">The user id.</param>
    /// <param name="displayName">The display name.</param>
    public WcfMembershipUserDisplayInfo(string providerName, string userID, string displayName)
    {
      this.ProviderName = providerName;
      this.UserID = userID;
      if (string.IsNullOrWhiteSpace(displayName))
      {
        User user = UserManager.GetManager(providerName).GetUser(new Guid(userID));
        if (user == null)
          return;
        this.DisplayName = UserProfilesHelper.GetUserDisplayName(user.Id);
      }
      else
        this.DisplayName = displayName;
    }

    /// <summary>Gets or sets the ID for the membership user.</summary>
    /// <value></value>
    /// <returns>ID of the membership user.</returns>
    [DataMember]
    public string UserID { get; set; }

    /// <summary>
    /// Gets or sets the name of the membership provider that stores and retrieves user information for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The name of the membership provider that stores and retrieves user information for the membership user.
    /// </returns>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value that will be displayed for each user (e.g. FirstName + LastName).
    /// </summary>
    [DataMember]
    public string DisplayName { get; set; }
  }
}
