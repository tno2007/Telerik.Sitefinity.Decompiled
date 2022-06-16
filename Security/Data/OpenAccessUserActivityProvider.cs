// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.OpenAccessUserActivityProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Licensing.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Security.Data
{
  public class OpenAccessUserActivityProvider : 
    UserActivityProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>
    /// Returns the user activity record for the specified userId and providerName.
    /// </summary>
    /// <param name="userId">The userId of the user.</param>
    /// <param name="providerName">The provider name.</param>
    /// <returns>UserActivity record or null if none was found.</returns>
    public override UserActivity GetUserActivity(Guid userId, string providerName) => this.GetUserActivities().Where<UserActivity>((Expression<Func<UserActivity, bool>>) (ua => ua.UserId == userId && ua.ProviderName == providerName)).SingleOrDefault<UserActivity>();

    /// <inheritdoc />
    public override UserActivity CreateUserActivity(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate)
    {
      return this.CreateUserActivity(userId, loginIp, providerName, isBackendUser, lastLoginDate, lastActivityDate, (string) null);
    }

    /// <inheritdoc />
    public override UserActivity CreateUserActivity(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      string tokenId)
    {
      UserActivity entity = new UserActivity();
      entity.IsBackendUser = isBackendUser;
      entity.IsLoggedIn = true;
      entity.LastActivityDate = lastActivityDate;
      entity.LastLoginDate = lastLoginDate;
      entity.LoginIP = loginIp;
      entity.ProviderName = providerName;
      entity.UserId = userId;
      entity.TokenId = tokenId;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Deletes the activity for the given userId and providerName.
    /// </summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="providerName">The name of the provider for the user.</param>
    public override void DeleteUserActivity(Guid userId, string providerName)
    {
      UserActivity userActivity = this.GetUserActivity(userId, providerName);
      this.GetContext().Remove((object) userActivity);
    }

    /// <summary>Gets a query for user activity records.</summary>
    /// <returns>The query for user activity records.</returns>
    public override IQueryable<UserActivity> GetUserActivities() => SitefinityQuery.Get<UserActivity>((DataProviderBase) this).Select<UserActivity, UserActivity>((Expression<Func<UserActivity, UserActivity>>) (r => r));

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new UserActivityMetadaSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }
  }
}
