// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.OpenAccessMembershipProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// Represents OpenAccess implementation of data provider for Sitefinity membership services.
  /// </summary>
  public class OpenAccessMembershipProvider : 
    MembershipDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates new user with the specified username.</summary>
    /// <param name="email">Email of the user</param>
    /// <returns>The new user.</returns>
    public override User CreateUser(string email) => this.CreateUser(this.GetNewGuid(), email);

    /// <summary>
    /// Creates new user with the specified identity and username.
    /// </summary>
    /// <param name="id">The identity of the new user.</param>
    /// <param name="email">Email of the user</param>
    /// <returns>The new user.</returns>
    public override User CreateUser(Guid id, string email)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      if (!string.IsNullOrEmpty(email))
      {
        LoginUtils.CheckParameter(email, true, true, true, 256, nameof (email));
        LoginUtils.CheckValidEmail(email);
        if (this.UserExists(email))
          throw new ProviderException("Email already exists.");
      }
      User user = new User();
      user.ApplicationName = this.ApplicationName;
      user.Email = email;
      user.Id = id;
      User entity = user;
      ManagerInfo managerInfo = this.GetManagerInfo(typeof (UserManager).FullName, this.Name);
      entity.ManagerInfo = managerInfo;
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the user with the specified identity.</summary>
    /// <param name="id">id</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.User" />.</returns>
    public override User GetUser(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("id cannot be empty GUID.");
      string appName = this.ApplicationName;
      User user = this.GetContext().GetAll<User>().Where<User>((Expression<Func<User, bool>>) (u => u.Id == id && u.ApplicationName == appName)).SingleOrDefault<User>();
      if (user != null)
        ((IDataItem) user).Provider = (object) this;
      return user;
    }

    /// <summary>Gets a query for users.</summary>
    /// <returns>The query for users.</returns>
    public override IQueryable<User> GetUsers()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<User>((DataProviderBase) this).Where<User>((Expression<Func<User, bool>>) (r => r.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(User item)
    {
      if (item.Id == SecurityManager.GetCurrentUserId())
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().YouCantDeleteTheCurrentUser, (Exception) null);
      this.GetContext().Remove((object) item);
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new MembershipMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }
  }
}
