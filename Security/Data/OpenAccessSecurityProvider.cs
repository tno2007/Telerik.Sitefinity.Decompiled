// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.OpenAccessSecurityProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// Represents security data provider that uses OpenAccess to store and retrieve security data.
  /// </summary>
  public class OpenAccessSecurityProvider : 
    SecurityDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "General"
    };

    /// <summary>Gets the security root with the provided key.</summary>
    /// <param name="key">The key of the security root to retrieve.</param>
    /// <returns></returns>
    public override SecurityRoot GetSecurityRoot(string key)
    {
      SecurityRoot securityRoot = this.GetContext().GetAll<SecurityRoot>().Where<SecurityRoot>((Expression<Func<SecurityRoot, bool>>) (r => r.ApplicationName == this.ApplicationName && r.Key == key)).FirstOrDefault<SecurityRoot>();
      if (securityRoot != null)
        ((IDataItem) securityRoot).Provider = (object) this;
      return securityRoot;
    }

    /// <summary>Gets the security root with the specified ID.</summary>
    /// <param name="pageId">The ID of the root.</param>
    /// <returns></returns>
    public override SecurityRoot GetSecurityRoot(Guid id)
    {
      SecurityRoot itemById = this.GetContext().GetItemById<SecurityRoot>(id.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets a query for security roots.</summary>
    /// <returns></returns>
    public override IQueryable<SecurityRoot> GetSecurityRoots()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<SecurityRoot>((DataProviderBase) this).Where<SecurityRoot>((Expression<Func<SecurityRoot, bool>>) (r => r.ApplicationName == appName));
    }

    /// <summary>Creates new security root with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public override SecurityRoot CreateSecurityRoot(string key) => this.CreateSecurityRoot(key, this.supportedPermissionSets);

    /// <summary>
    /// Creates a security root by specifying a key and supported permission sets
    /// </summary>
    /// <param name="key">Security root key name</param>
    /// <param name="supportedPermissionSets">Permission sets that should be supported by the security root</param>
    /// <returns>Security root that has a proper <paramref name="key" /> and <paramref name="supportedPermissionSets" /> </returns>
    public override SecurityRoot CreateSecurityRoot(
      string key,
      params string[] supportedPermissionSets)
    {
      SecurityRoot entity = new SecurityRoot(this.ApplicationName, this.GetNewGuid(), supportedPermissionSets, (IDictionary<string, string>) new Dictionary<string, string>())
      {
        Key = key
      };
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Deletes the security root.</summary>
    /// <param name="root">The root.</param>
    public override void DeleteSecurityRoot(SecurityRoot root) => this.GetContext().Remove((object) root);

    public override string[] SupportedPermissionSets => this.supportedPermissionSets;

    /// <summary>
    /// Crate a new user action bound to the current application
    /// </summary>
    /// <returns>New action</returns>
    public override UserAction CreateUserAction() => this.CreateUserAction(this.GetNewGuid());

    /// <summary>
    /// Create a new user action bound to the current application, and give the action <paramref name="pageId" />
    /// </summary>
    /// <param name="pageId">Id to set to the newly created user action</param>
    /// <returns>New user action</returns>
    public override UserAction CreateUserAction(Guid id)
    {
      UserAction entity = new UserAction(this.ApplicationName, id);
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Remove the <paramref name="action" /> from the current object scope
    /// </summary>
    /// <param name="action">User action to remove from the current scope</param>
    public override void DeleteUserAction(UserAction action) => this.GetContext().Remove((object) action);

    /// <summary>Get a user action by pageId</summary>
    /// <param name="pageId">Id of the user action</param>
    /// <returns>User action identified by <paramref name="pageId" /> or <c>null</c> if not found.</returns>
    public override UserAction GetUserAction(Guid id)
    {
      UserAction userAction = this.GetContext().GetAll<UserAction>().FirstOrDefault<UserAction>((Expression<Func<UserAction, bool>>) (a => a.Id == id));
      ((IDataItem) userAction).Provider = (object) this;
      return userAction;
    }

    /// <summary>
    /// Get a queryable object for all user actions in this application
    /// </summary>
    /// <returns>Queryable for all user actions in this application</returns>
    public override IQueryable<UserAction> GetUserActions()
    {
      string currentAppName = this.ApplicationName;
      return SitefinityQuery.Get<UserAction>((DataProviderBase) this).Where<UserAction>((Expression<Func<UserAction, bool>>) (userAction => userAction.ApplicationName == currentAppName));
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new SecurityMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }
  }
}
