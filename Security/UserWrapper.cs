// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Security;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  public class UserWrapper : User
  {
    private bool isDirty;
    private bool isNew;
    private bool isDeleted;

    public UserWrapper(Guid id, string userName)
    {
      this.Id = id;
      this.SetUserName(userName);
      this.isNew = true;
      this.isDirty = false;
    }

    /// <summary>
    /// Creates an instance of UserWrapper by copying properties from a <see cref="T:System.Web.Security.MembershipUser" /> object.
    /// </summary>
    /// <param name="user">A MembershipUser instance.</param>
    /// <param name="applicationName">The application name for the membership provider.</param>
    /// <returns>A UserWrapper object.</returns>
    public static UserWrapper CopyFrom(MembershipUser user, string applicationName)
    {
      ManagerInfo managerInfo = new ManagerInfo();
      managerInfo.ProviderName = user.ProviderName;
      managerInfo.ManagerType = typeof (UserManager).FullName;
      managerInfo.ApplicationName = applicationName;
      UserWrapper userWrapper = new UserWrapper(Guid.Parse(user.ProviderUserKey.ToString()), user.UserName);
      userWrapper.ApplicationName = applicationName;
      userWrapper.Comment = user.Comment;
      userWrapper.Email = user.Email;
      userWrapper.IsApproved = user.IsApproved;
      userWrapper.ManagerInfo = managerInfo;
      userWrapper.SetCreationDate(user.CreationDate);
      userWrapper.SetIsLockedOut(user.IsLockedOut);
      userWrapper.SetLastLockoutDate(user.LastLockoutDate);
      userWrapper.SetLastPasswordChangedDate(user.LastPasswordChangedDate);
      userWrapper.SetPasswordQuestion(user.PasswordQuestion);
      userWrapper.IsNew = false;
      userWrapper.IsDirty = false;
      return userWrapper;
    }

    public static UserWrapper CopyFrom(User user)
    {
      UserWrapper userWrapper = new UserWrapper(user.Id, user.UserName);
      userWrapper.ApplicationName = user.ApplicationName;
      userWrapper.Comment = user.Comment;
      userWrapper.Email = user.Email;
      userWrapper.IsApproved = user.IsApproved;
      userWrapper.IsLoggedIn = user.IsLoggedIn;
      userWrapper.LastActivityDate = user.LastActivityDate.ToUniversalTime();
      userWrapper.LastLoginDate = user.LastLoginDate.ToUniversalTime();
      userWrapper.ManagerInfo = user.ManagerInfo;
      userWrapper.SetCreationDate(user.CreationDate);
      userWrapper.SetIsLockedOut(user.IsLockedOut);
      userWrapper.SetLastLockoutDate(user.LastLockoutDate);
      userWrapper.SetLastPasswordChangedDate(user.LastPasswordChangedDate);
      userWrapper.SetPasswordQuestion(user.PasswordQuestion);
      userWrapper.IsNew = false;
      userWrapper.IsDirty = false;
      return userWrapper;
    }

    public void CopyDetailsToMembershipUser(ref MembershipUser user)
    {
      user.Comment = this.Comment;
      user.Email = this.Email;
      user.IsApproved = this.IsApproved;
      user.LastLoginDate = this.LastLoginDate;
      if (this.LastLoginDate == DateTime.MinValue)
        user.LastLoginDate = new DateTime(1753, 1, 1, 12, 0, 0);
      user.LastActivityDate = this.LastActivityDate;
      if (!(this.LastActivityDate == DateTime.MinValue))
        return;
      user.LastActivityDate = new DateTime(1753, 1, 1, 12, 0, 0);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this user record has been modified since any last provider commit.
    /// </summary>
    public bool IsDirty
    {
      get => this.isDirty;
      set => this.isDirty = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user record is newly added and not yet committed to the provider.
    /// </summary>
    public bool IsNew
    {
      get => this.isNew;
      set => this.isNew = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user record was marked for deletion but not yet committed to the provider.
    /// </summary>
    public bool IsDeleted
    {
      get => this.isDeleted;
      set => this.isDeleted = value;
    }

    /// <summary>Gets or sets the first name.</summary>
    /// <value>The first name.</value>
    public string FirstName1
    {
      get => base.FirstName;
      set
      {
        base.FirstName = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the last name.</summary>
    /// <value>The last name.</value>
    public string LastName1
    {
      get => base.LastName;
      set
      {
        base.LastName = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the first name.</summary>
    /// <value>The first name.</value>
    public new string FirstName
    {
      get => base.FirstName ?? string.Empty;
      set
      {
        base.FirstName = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the last name.</summary>
    /// <value>The last name.</value>
    public new string LastName
    {
      get => base.LastName ?? string.Empty;
      set
      {
        base.LastName = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets application-specific information for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// Application-specific information for the membership user.
    /// </returns>
    public override string Comment
    {
      get => base.Comment;
      set
      {
        base.Comment = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the e-mail address for the membership user.
    /// </summary>
    /// <value></value>
    /// <returns>The e-mail address for the membership user.</returns>
    public override string Email
    {
      get => base.Email;
      set
      {
        base.Email = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets whether the membership user can be authenticated.
    /// </summary>
    /// <value></value>
    /// <returns>true if the user can be authenticated; otherwise, false.</returns>
    public override bool IsApproved
    {
      get => base.IsApproved;
      set
      {
        base.IsApproved = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the date and time when the membership user was last authenticated or accessed the application.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the membership user was last authenticated or accessed the application.
    /// </returns>
    public override DateTime LastActivityDate
    {
      get => base.LastActivityDate;
      set
      {
        base.LastActivityDate = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the date and time when the user was last authenticated.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The date and time when the user was last authenticated.
    /// </returns>
    public override DateTime LastLoginDate
    {
      get => base.LastLoginDate;
      set
      {
        base.LastLoginDate = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the password.</summary>
    /// <value>The password.</value>
    public override string Password
    {
      get => base.Password;
      set
      {
        base.Password = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the suffix added to the password.</summary>
    /// <value>The salt.</value>
    public override string Salt
    {
      get => base.Salt;
      set
      {
        base.Salt = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the password answer.</summary>
    /// <value>The password answer.</value>
    public override string PasswordAnswer
    {
      get => base.PasswordAnswer;
      set
      {
        base.PasswordAnswer = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the failed password attempt count.</summary>
    /// <value>The failed password attempt count.</value>
    public override int FailedPasswordAttemptCount
    {
      get => base.FailedPasswordAttemptCount;
      set
      {
        base.FailedPasswordAttemptCount = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the failed password answer attempt count.
    /// </summary>
    /// <value>The failed password answer attempt count.</value>
    public override int FailedPasswordAnswerAttemptCount
    {
      get => base.FailedPasswordAnswerAttemptCount;
      set
      {
        base.FailedPasswordAnswerAttemptCount = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the password format.</summary>
    /// <value>The password format.</value>
    public override int PasswordFormat
    {
      get => base.PasswordFormat;
      set
      {
        base.PasswordFormat = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the failed password attempt window start.
    /// </summary>
    /// <value>The failed password attempt window start.</value>
    public override DateTime FailedPasswordAttemptWindowStart
    {
      get => base.FailedPasswordAttemptWindowStart;
      set
      {
        base.FailedPasswordAttemptWindowStart = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the failed password answer attempt window start.
    /// </summary>
    /// <value>The failed password answer attempt window start.</value>
    public override DateTime FailedPasswordAnswerAttemptWindowStart
    {
      get => base.FailedPasswordAnswerAttemptWindowStart;
      set
      {
        base.FailedPasswordAnswerAttemptWindowStart = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the manager info.</summary>
    /// <value>The manager info.</value>
    public override ManagerInfo ManagerInfo
    {
      get => base.ManagerInfo;
      set
      {
        base.ManagerInfo = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is logged in.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if user is logged in; otherwise, <c>false</c>.
    /// </value>
    public override bool IsLoggedIn
    {
      get => base.IsLoggedIn;
      set
      {
        base.IsLoggedIn = value;
        this.isDirty = true;
      }
    }

    /// <summary>Gets or sets the user IP address of the last login.</summary>
    /// <value>The last login ip.</value>
    public override string LastLoginIp
    {
      get => base.LastLoginIp;
      set
      {
        base.LastLoginIp = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this user is backend user.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if user is backend user; otherwise, <c>false</c>.
    /// </value>
    public override bool IsBackendUser
    {
      get => base.IsBackendUser;
      set
      {
        base.IsBackendUser = value;
        this.isDirty = true;
      }
    }

    /// <summary>
    /// Gets or sets the name of the application to which this data item belongs to.
    /// </summary>
    /// <value>The name of the application.</value>
    public override string ApplicationName
    {
      get => base.ApplicationName;
      set
      {
        base.ApplicationName = value;
        this.isDirty = true;
      }
    }
  }
}
