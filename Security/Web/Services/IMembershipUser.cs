// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.IMembershipUser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Interface defining properties for MembershipUser the WCF.
  /// </summary>
  public interface IMembershipUser
  {
    /// <summary>Gets or sets the comment.</summary>
    /// <value>The comment.</value>
    string Comment { get; set; }

    /// <summary>Gets or sets the email.</summary>
    /// <value>The email.</value>
    string Email { get; set; }

    /// <summary>Gets or sets the last login date.</summary>
    /// <value>The last login date.</value>
    DateTime LastLoginDate { get; set; }

    /// <summary>Gets the name of the user.</summary>
    /// <value>The name of the user.</value>
    string UserName { get; }

    /// <summary>Gets the creation date.</summary>
    /// <value>The creation date.</value>
    DateTime CreationDate { get; }

    /// <summary>Gets the last lockout date.</summary>
    /// <value>The last lockout date.</value>
    DateTime LastLockoutDate { get; }

    /// <summary>Gets the last password changed date.</summary>
    /// <value>The last password changed date.</value>
    DateTime LastPasswordChangedDate { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this user is approved.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this user is approved; otherwise, <c>false</c>.
    /// </value>
    bool IsApproved { get; set; }

    /// <summary>
    /// Gets a value indicating whether this user is locked out.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this user is locked out; otherwise, <c>false</c>.
    /// </value>
    bool IsLockedOut { get; }
  }
}
