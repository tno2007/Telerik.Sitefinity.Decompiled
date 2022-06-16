// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.OpenAccessRoleProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// This class is implementation of Sitefinity 4.1 role provider for OpenAccess database.
  /// </summary>
  public class OpenAccessRoleProvider : 
    RoleDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates new security role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public override Role CreateRole(string roleName) => this.CreateRole(this.GetNewGuid(), roleName);

    /// <summary>
    /// Creates new security role with the specified identity and name.
    /// </summary>
    /// <param name="pageId">The identity of the new role.</param>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public override Role CreateRole(Guid id, string roleName)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      if (!string.IsNullOrEmpty(roleName))
      {
        LoginUtils.CheckParameter(roleName, true, true, true, 256, nameof (roleName));
        if (this.RoleExists(roleName))
          throw new ProviderException(Res.Get<ErrorMessages>().RoleAlreadyExists.Arrange((object) roleName));
      }
      Role entity = new Role()
      {
        ApplicationName = this.ApplicationName,
        Id = id,
        Name = roleName
      };
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override Role GetRole(Guid id)
    {
      Role role = !(id == Guid.Empty) ? this.GetContext().GetItemById<Role>(id.ToString()) : throw new ArgumentException("id cannot be empty GUID.");
      ((IDataItem) role).Provider = (object) this;
      return role;
    }

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public override IQueryable<Role> GetRoles()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Role>((DataProviderBase) this).Where<Role>((Expression<Func<Role, bool>>) (r => r.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(Role item) => this.GetContext().Remove((object) item);

    /// <summary>Creates new user link.</summary>
    /// <returns></returns>
    public override UserLink CreateUserLink() => this.CreateUserLink(this.GetNewGuid());

    /// <summary>
    /// Creates new user link for the specified user identity.
    /// </summary>
    /// <param name="pageId">The identity.</param>
    /// <returns></returns>
    public override UserLink CreateUserLink(Guid id)
    {
      UserLink entity = !(id == Guid.Empty) ? new UserLink()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      } : throw new ArgumentNullException(nameof (id));
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override UserLink GetUserLink(Guid id)
    {
      UserLink userLink = !(id == Guid.Empty) ? this.GetContext().GetItemById<UserLink>(id.ToString()) : throw new ArgumentException("id cannot be empty GUID.");
      ((IDataItem) userLink).Provider = (object) this;
      return userLink;
    }

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public override IQueryable<UserLink> GetUserLinks() => SitefinityQuery.Get<UserLink>((DataProviderBase) this).Where<UserLink>((Expression<Func<UserLink, bool>>) (r => r.ApplicationName == this.ApplicationName));

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(UserLink item) => this.GetContext().Remove((object) item);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new RoleMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Commits the provided transaction.</summary>
    public override void CommitTransaction()
    {
      IEnumerable<IDataEvent> dataEvents = this.CollectEventsData();
      base.CommitTransaction();
      if (!dataEvents.Any<IDataEvent>())
        return;
      this.RaiseEvents(dataEvents, false);
    }

    private IEnumerable<IDataEvent> CollectEventsData()
    {
      List<IDataEvent> dataEventList = new List<IDataEvent>();
      IList dirtyItems = this.GetDirtyItems();
      if (dirtyItems.Count > 0)
      {
        if (!this.SuppressEvents)
        {
          try
          {
            foreach (object obj in (IEnumerable) dirtyItems)
            {
              if (obj is Role itemInTransaction)
              {
                SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus((object) itemInTransaction);
                IRoleEvent roleEvent = (IRoleEvent) new RoleEvent()
                {
                  ItemId = itemInTransaction.Id,
                  ItemType = itemInTransaction.GetType(),
                  ProviderName = this.Name,
                  RoleName = itemInTransaction.Name,
                  Action = dirtyItemStatus.ToString()
                };
                dataEventList.Add((IDataEvent) roleEvent);
              }
            }
          }
          catch (Exception ex)
          {
            this.RollbackTransaction();
            throw ex;
          }
        }
      }
      return (IEnumerable<IDataEvent>) dataEventList;
    }

    private void RaiseEvent(IEvent eventData, object origin, bool throwExceptions)
    {
      if (origin != null)
        eventData.Origin = (string) origin;
      EventHub.Raise(eventData, throwExceptions);
    }

    private void RaiseEvents(IEnumerable<IDataEvent> events, bool throwExceptions)
    {
      object origin;
      this.TryGetExecutionStateData("EventOriginKey", out origin);
      foreach (IEvent eventData in events)
        this.RaiseEvent(eventData, origin, throwExceptions);
    }
  }
}
