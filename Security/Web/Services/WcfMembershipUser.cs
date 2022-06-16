// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// This class is used to serialize the MembershipUser data through WCF service.
  /// </summary>
  [DataContract]
  [ManagerType("Telerik.Sitefinity.Security.UserManager")]
  public class WcfMembershipUser : WcfItemBase, IEquatable<WcfMembershipUser>
  {
    private RoleProviderPair[] rolesOfUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser" /> class.
    /// </summary>
    public WcfMembershipUser()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser" /> class.
    /// </summary>
    /// <param name="user">The actual user to initialize with.</param>
    public WcfMembershipUser(User user)
      : this(user.ProviderName, user.ProviderUserKey, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved, user.IsLockedOut, user.CreationDate, user.LastActivityDate, user.LastActivityDate, user.LastPasswordChangedDate, user.LastLockoutDate, user.ProviderUserKey.ToString(), UserProfilesHelper.GetUserDisplayName(user.Id), false, false, user.ExternalProviderName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="name">The name.</param>
    /// <param name="providerUserKey">The provider user key.</param>
    /// <param name="email">The email.</param>
    /// <param name="passwordQuestion">The password question.</param>
    /// <param name="comment">The comment.</param>
    /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
    /// <param name="isLockedOut">if set to <c>true</c> [is locked out].</param>
    /// <param name="creationDate">The creation date.</param>
    /// <param name="lastLoginDate">The last login date.</param>
    /// <param name="lastActivityDate">The last activity date.</param>
    /// <param name="lastPasswordChangedDate">The last password changed date.</param>
    /// <param name="lastLockoutDate">The last lockout date.</param>
    public WcfMembershipUser(
      string providerName,
      object providerUserKey,
      string email,
      string passwordQuestion,
      string comment,
      bool isApproved,
      bool isLockedOut,
      DateTime creationDate,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      DateTime lastPasswordChangedDate,
      DateTime lastLockoutDate,
      string userID,
      string displayName,
      bool isLoggedIn,
      bool isBackendUser,
      string externalProviderName,
      string externalProviderId = null)
    {
      this.ProviderName = providerName;
      this.ProviderUserKey = providerUserKey;
      this.Email = email ?? string.Empty;
      this.PasswordQuestion = passwordQuestion;
      this.Comment = comment;
      this.IsApproved = isApproved;
      this.IsLockedOut = isLockedOut;
      this.CreationDate = Utility.IsDateLikeNullValue(new DateTime?(creationDate)) ? new DateTime(2000, 1, 1) : creationDate;
      this.LastLoginDate = Utility.IsDateLikeNullValue(new DateTime?(lastLoginDate)) ? new DateTime?() : new DateTime?(lastLoginDate);
      this.LastActivityDate = Utility.IsDateLikeNullValue(new DateTime?(lastActivityDate)) ? new DateTime?() : new DateTime?(lastActivityDate);
      this.LastPasswordChangedDate = Utility.IsDateLikeNullValue(new DateTime?(lastPasswordChangedDate)) ? new DateTime?() : new DateTime?(lastPasswordChangedDate);
      this.LastLockoutDate = Utility.IsDateLikeNullValue(new DateTime?(lastLockoutDate)) ? new DateTime?() : new DateTime?(lastLockoutDate);
      this.Comment = comment;
      this.UserID = userID;
      this.IsBackendUser = isBackendUser;
      this.ExternalProviderId = externalProviderId;
      this.ExternalProviderName = externalProviderName;
      DateTime universalTime = (DateTime.UtcNow - Config.Get<SecurityConfig>().AuthCookieTimeout).ToUniversalTime();
      if (string.IsNullOrWhiteSpace(displayName))
      {
        User user = UserManager.GetManager(providerName).GetUser(new Guid(userID));
        if (user == null)
          return;
        this.IsLoggedIn = user.IsLoggedIn && user.LastActivityDate > universalTime;
        this.IsBackendUser = user.IsBackendUser;
        this.DisplayName = UserProfilesHelper.GetUserDisplayName(user.Id);
      }
      else
      {
        int num;
        if (isLoggedIn)
        {
          DateTime? lastActivityDate1 = this.LastActivityDate;
          DateTime dateTime = universalTime;
          num = lastActivityDate1.HasValue ? (lastActivityDate1.GetValueOrDefault() > dateTime ? 1 : 0) : 0;
        }
        else
          num = 0;
        this.IsLoggedIn = num != 0;
        this.DisplayName = displayName;
      }
    }

    /// <summary>
    /// Gets the user identifier from the membership data source for the user.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The user identifier from the membership data source for the user.
    /// </returns>
    [DataMember]
    public object ProviderUserKey { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the membership user was last authenticated or accessed the application.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the membership user was last authenticated or accessed the application.
    /// </returns>
    [DataMember]
    public DateTime? LastActivityDate { get; set; }

    /// <summary>
    /// Gets or sets application-specific information for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// Application-specific information for the membership user.
    /// </returns>
    [DataMember]
    public string Comment { get; set; }

    /// <summary>Gets or sets the ID for the membership user.</summary>
    /// <value></value>
    /// <returns>ID of the membership user.</returns>
    [DataMember]
    public string UserID { get; set; }

    /// <summary>
    /// Gets or sets a value that will be displayed for each user (e.g. FirstName + LastName).
    /// </summary>
    [DataMember]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the e-mail address for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>The e-mail address for the membership user.</returns>
    [DataMember]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was last authenticated.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the user was last authenticated.
    /// </returns>
    [DataMember]
    public DateTime? LastLoginDate { get; set; }

    /// <summary>Gets or sets the password.</summary>
    /// <value>The password.</value>
    [DataMember]
    public string Password { get; set; }

    /// <summary>
    /// Gets the date and time when the user was added to the membership data store.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the user was added to the membership data store.
    /// </returns>
    [DataMember]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets the most recent date and time that the membership user was locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.DateTime" /> object that represents the most recent date and time that the membership user was locked out.
    /// </returns>
    [DataMember]
    public DateTime? LastLockoutDate { get; set; }

    /// <summary>
    /// Gets the date and time when the membership user's password was last updated.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the membership user's password was last updated.
    /// </returns>
    [DataMember]
    public DateTime? LastPasswordChangedDate { get; set; }

    /// <summary>
    /// Gets or sets whether the membership user can be authenticated.
    /// </summary>
    /// <value></value>
    /// <returns>true if the user can be authenticated; otherwise, false.</returns>
    [DataMember]
    public bool IsApproved { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is logged in.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is logged in; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsLoggedIn { get; set; }

    /// <summary>
    /// Gets a value indicating whether the membership user is locked out and unable to be validated.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership user is locked out and unable to be validated; otherwise, false.
    /// </returns>
    [DataMember]
    public bool IsLockedOut { get; set; }

    /// <summary>
    /// Gets the name of the membership provider that stores and retrieves user information for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The name of the membership provider that stores and retrieves user information for the membership user.
    /// </returns>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets the id of the user in the external provider or null if the user is local.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The id of the user in the external provider or null if the user is local.
    /// </returns>
    [DataMember]
    public string ExternalProviderId { get; set; }

    /// <summary>
    /// Gets the name of the external provider or null if the user is local.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The external provider name of the user or null if the user is local.
    /// </returns>
    [DataMember]
    public string ExternalProviderName { get; set; }

    /// <summary>Gets the password question for the membership user.</summary>
    /// <value></value>
    /// <returns>The password question for the membership user.</returns>
    [DataMember]
    public string PasswordQuestion { get; set; }

    /// <summary>Gets or sets the password answer.</summary>
    /// <value>The password answer.</value>
    [DataMember]
    public string PasswordAnswer { get; set; }

    [DataMember]
    public string ProfileData { get; set; }

    /// <summary>Gets or sets the roles of user.</summary>
    /// <value>The roles of user.</value>
    [DataMember]
    public RoleProviderPair[] RolesOfUser
    {
      get
      {
        if (this.rolesOfUser == null)
          this.rolesOfUser = new RoleProviderPair[0];
        return this.rolesOfUser;
      }
      set => this.rolesOfUser = value;
    }

    /// <summary>
    /// Converts <see cref="P:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser.RolesOfUser" /> to a comma-separated list of role names
    /// </summary>
    [DataMember]
    public string RoleNamesOfUser
    {
      get => this.RolesOfUser != null && this.RolesOfUser.Length != 0 ? string.Join(", ", ((IEnumerable<RoleProviderPair>) this.RolesOfUser).Select<RoleProviderPair, string>((Func<RoleProviderPair, string>) (p => p.RoleName)).ToArray<string>()) : string.Empty;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this user is backend user. And can be logged off forcefully
    /// </summary>
    [DataMember]
    public bool IsBackendUser { get; set; }

    [DataMember]
    public string AvatarImageUrl { get; set; }

    [DataMember]
    public string AvatarThumbnailUrl { get; set; }

    /// <summary>
    /// Gets or sets the smaller dimension of the avatar image from the AvatarImageUrl. This is used in order
    /// to be able to display the image on the client without changing proportions. This is done by only setting
    /// the attribute of the smaller side (width or height).
    /// </summary>
    /// <value>true if the smaller dimension of the avatar image is its width.</value>
    [DataMember]
    public bool AvatarImageSmallerWidth { get; set; }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
    /// </returns>
    public bool Equals(WcfMembershipUser other) => other != null && this.UserID == other.UserID && this.ProviderName == other.ProviderName;
  }
}
