// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.UserActivityProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing.Model;

namespace Telerik.Sitefinity.Security.Data
{
  public abstract class UserActivityProvider : DataProviderBase
  {
    /// <summary>
    /// Returns the user activity record for the specified userId and providerName.
    /// </summary>
    /// <param name="userId">The userId of the user.</param>
    /// <param name="providerName">The provider name.</param>
    /// <returns>UserActivity record or null if none was found.</returns>
    public abstract UserActivity GetUserActivity(Guid userId, string providerName);

    /// <summary>Creates a new user activity record.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="loginIp">The IP address he logged in from.</param>
    /// <param name="providerName">The name of the membership user provider.</param>
    /// <param name="isBackendUser">Whether the user is a backend user.</param>
    /// <param name="lastLoginDate">The date of the last login of the user.</param>
    /// <param name="lastActivityDate">The date of the last activity of the user.</param>
    /// <returns>The new user activity record.</returns>
    public abstract UserActivity CreateUserActivity(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate);

    /// <summary>Creates a new user activity record.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="loginIp">The IP address he logged in from.</param>
    /// <param name="providerName">The name of the membership user provider.</param>
    /// <param name="isBackendUser">Whether the user is a backend user.</param>
    /// <param name="lastLoginDate">The date of the last login of the user.</param>
    /// <param name="lastActivityDate">The date of the last activity of the user.</param>
    /// <param name="tokenId">The authentication token of the user.</param>
    /// <returns>The new user activity record.</returns>
    public abstract UserActivity CreateUserActivity(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      string tokenId);

    /// <summary>
    /// Deletes the activity for the given userId and providerName.
    /// </summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="providerName">The name of the provider for the user.</param>
    public abstract void DeleteUserActivity(Guid userId, string providerName);

    /// <summary>Gets a query for user activity records.</summary>
    /// <returns>The query for user activity records.</returns>
    public abstract IQueryable<UserActivity> GetUserActivities();

    /// <summary>Get a list of types served by this provider</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (UserActivity)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (UserActivityProvider);
  }
}
