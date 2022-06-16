// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.RoleProviderWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Security;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Class used to wrap ASP.NET role provider as Sitefinity 4.1 role provider.
  /// </summary>
  internal class RoleProviderWrapper : RoleDataProvider
  {
    private ManagerInfo managerInfo;
    private RoleProvider roleProvider;
    private Dictionary<Guid, RoleWrapper> roleList;
    private Dictionary<Guid, UserLinkWrapper> userLinkList;

    internal RoleProviderWrapper(RoleProvider roleProvider)
    {
      this.roleProvider = roleProvider != null ? roleProvider : throw new ArgumentNullException(nameof (roleProvider));
      this.roleList = new Dictionary<Guid, RoleWrapper>();
      this.userLinkList = new Dictionary<Guid, UserLinkWrapper>();
      this.Initialize(this.Name, new NameValueCollection(), typeof (RoleManager), true);
    }

    private char GetSecondReservedCharacter(char input)
    {
      switch ((int) input % 4)
      {
        case 0:
          return '8';
        case 1:
          return '9';
        case 2:
          return 'a';
        case 3:
          return 'b';
        default:
          return input;
      }
    }

    /// <summary>
    /// A method that allegedly returns low-collision probability GUID hash from two strings.
    /// </summary>
    /// <param name="string1">The first string.</param>
    /// <param name="string2">The second string</param>
    /// <returns>A Guid hash constructed from the two strings.</returns>
    private Guid GetGuidFromStrings(string string1, string string2)
    {
      Crc32 crc32 = new Crc32();
      int int32_1 = BitConverter.ToInt32(crc32.ComputeHash(Encoding.ASCII.GetBytes(string1)), 0);
      int int32_2 = BitConverter.ToInt32(crc32.ComputeHash(Encoding.ASCII.GetBytes(string2)), 0);
      long num1 = (long) (int32_1 * int32_2);
      long num2 = (long) (int32_2 * int32_2);
      char[] charArray = new Guid(((IEnumerable<byte>) BitConverter.GetBytes(num1)).Concat<byte>((IEnumerable<byte>) BitConverter.GetBytes(num2)).ToArray<byte>()).ToString().Replace("{", string.Empty).Replace("}", string.Empty).Replace("-", string.Empty).ToCharArray();
      charArray[15] = '4';
      charArray[20] = this.GetSecondReservedCharacter(charArray[20]);
      return new Guid(new string(charArray));
    }

    /// <summary>Creates new security role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public override Role CreateRole(string roleName) => this.CreateRole(Guid.NewGuid(), roleName);

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
      RoleWrapper role = new RoleWrapper(id, roleName);
      this.roleList[id] = role;
      return (Role) role;
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override Role GetRole(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("ID cannot be empty GUID.");
      if (this.roleList.ContainsKey(id))
        return (Role) this.roleList[id];
      string[] allRoles = this.roleProvider.GetAllRoles();
      for (int index = 0; index < allRoles.Length; ++index)
      {
        if (this.GetGuidFromStrings(this.Name, allRoles[index]) == id)
        {
          this.roleList[id] = new RoleWrapper(id, allRoles[index]);
          this.roleList[id].IsNew = false;
          return (Role) this.roleList[id];
        }
      }
      return (Role) null;
    }

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public override IQueryable<Role> GetRoles()
    {
      string[] allRoles = this.roleProvider.GetAllRoles();
      List<RoleWrapper> source = new List<RoleWrapper>();
      for (int index = 0; index < allRoles.Length; ++index)
      {
        RoleWrapper roleWrapper = new RoleWrapper(this.GetGuidFromStrings(this.Name, allRoles[index]), allRoles[index]);
        roleWrapper.ApplicationName = this.ApplicationName;
        source.Add(roleWrapper);
      }
      return ((IEnumerable<Role>) source).AsQueryable<Role>();
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(Role item)
    {
      if (!this.roleList.ContainsKey(item.Id))
        return;
      this.roleList[item.Id].IsDeleted = true;
    }

    /// <summary>Creates new user link.</summary>
    /// <returns></returns>
    public override UserLink CreateUserLink() => this.CreateUserLink(Guid.NewGuid());

    /// <summary>
    /// Creates new user link for the specified user identity.
    /// </summary>
    /// <param name="pageId">The identity.</param>
    /// <returns></returns>
    public override UserLink CreateUserLink(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("id cannot be empty GUID.");
      UserLinkWrapper userLinkWrapper = new UserLinkWrapper();
      userLinkWrapper.ApplicationName = this.ApplicationName;
      userLinkWrapper.Id = id;
      UserLinkWrapper userLink = userLinkWrapper;
      ((IDataItem) userLink).Provider = (object) this;
      this.userLinkList[userLink.Id] = userLink;
      return (UserLink) userLink;
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override UserLink GetUserLink(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("id cannot be empty GUID.");
      if (this.userLinkList.ContainsKey(id))
        return (UserLink) this.userLinkList[id];
      return this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (ul => ul.Id == id)).SingleOrDefault<UserLink>();
    }

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public override IQueryable<UserLink> GetUserLinks()
    {
      IEnumerable<MembershipDataProvider> membershipDataProviders = UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>();
      Dictionary<Guid, UserLink> dictionary = new Dictionary<Guid, UserLink>();
      string[] allRoles = this.roleProvider.GetAllRoles();
      for (int index1 = 0; index1 < allRoles.Length; ++index1)
      {
        string[] usersInRole = this.roleProvider.GetUsersInRole(allRoles[index1]);
        foreach (MembershipDataProvider membershipDataProvider in membershipDataProviders)
        {
          double num = Math.Ceiling((double) usersInRole.Length / 500.0);
          for (int index2 = 0; (double) index2 < num; ++index2)
          {
            string[] currentSplit = ((IEnumerable<string>) usersInRole).Skip<string>(index2 * 500).Take<string>(500).ToArray<string>();
            IQueryable<User> users = membershipDataProvider.GetUsers();
            Expression<Func<User, bool>> predicate = (Expression<Func<User, bool>>) (u => currentSplit.Contains<string>(u.UserName));
            foreach (User user in (IEnumerable<User>) users.Where<User>(predicate))
            {
              UserLinkWrapper userLinkWrapper = new UserLinkWrapper();
              userLinkWrapper.IsNew = false;
              Guid guidFromStrings = this.GetGuidFromStrings(membershipDataProvider.Name + user.UserName, allRoles[index1]);
              if (!dictionary.ContainsKey(guidFromStrings))
              {
                userLinkWrapper.Id = guidFromStrings;
                userLinkWrapper.ApplicationName = this.ApplicationName;
                userLinkWrapper.Role = new Role()
                {
                  Name = allRoles[index1],
                  Id = this.GetGuidFromStrings(this.Name, allRoles[index1])
                };
                userLinkWrapper.UserId = user.Id;
                userLinkWrapper.MembershipManagerInfo = user.ManagerInfo;
                dictionary[guidFromStrings] = (UserLink) userLinkWrapper;
              }
              this.userLinkList[userLinkWrapper.Id] = userLinkWrapper;
            }
          }
        }
      }
      return dictionary.Values.AsQueryable<UserLink>();
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(UserLink item)
    {
      if (!this.userLinkList.ContainsKey(item.Id))
        return;
      this.userLinkList[item.Id].IsDeleted = true;
    }

    /// <summary>Gets a list of the roles that a specified user is in.</summary>
    /// <param name="userName">The user to return a list of roles for.</param>
    /// <returns>
    /// A string array containing the names of all the roles that the specified
    /// user is in for the configured applicationName.
    /// </returns>
    public override IQueryable<Role> GetRolesForUser(Guid userId)
    {
      string transactionName = this.TransactionName.IsNullOrEmpty() ? (string) null : this.TransactionName;
      User user = SecurityManager.GetUser(userId, transactionName, out string _);
      return user != null ? ((IEnumerable<string>) this.roleProvider.GetRolesForUser(user.UserName)).Select<string, Role>((Func<string, Role>) (roleName => new Role()
      {
        Name = roleName,
        Id = this.GetGuidFromStrings(this.Name, roleName)
      })).AsQueryable<Role>() : ((IEnumerable<Role>) new Role[0]).AsQueryable<Role>();
    }

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleId">The role to search in.</param>
    public override bool IsUserInRole(Guid userId, Guid roleId)
    {
      Role role = this.GetRole(roleId);
      return this.IsUserInRole(userId, role.Name);
    }

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public override bool IsUserInRole(Guid userId, string roleName) => this.roleProvider.IsUserInRole(SecurityManager.GetUser(userId, this.TransactionName).UserName, roleName);

    /// <summary>Adds the provided user to the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public override void AddUserToRole(User user, Role role)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      if (role == null)
        throw new ArgumentNullException(nameof (role));
      if (SecurityManager.UnassignableRoles.Contains(role.Id))
        throw new ArgumentException(string.Format("Users cannot be assigned to role {0} (Role ID: {1})", (object) role.Name, (object) role.Id.ToString()), nameof (role));
      string name = role.Name;
      string userName = user.UserName;
      if (this.roleProvider.IsUserInRole(userName, name))
        return;
      this.roleProvider.AddUsersToRoles(new string[1]
      {
        userName
      }, new string[1]{ name });
    }

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public override void RemoveUserFromRole(Guid userId, Role role) => this.RemoveUserFromRole(SecurityManager.GetUser(userId, this.TransactionName), role);

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public new virtual void RemoveUserFromRole(User user, Role role)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      string roleName = role != null ? role.Name : throw new ArgumentNullException(nameof (role));
      string userName = user.UserName;
      if (!this.roleProvider.IsUserInRole(userName, roleName))
        return;
      this.roleProvider.RemoveUsersFromRoles(new string[1]
      {
        userName
      }, new string[1]{ roleName });
    }

    /// <summary>
    /// Gets or sets the name of the application to store and retrieve role information for.
    /// </summary>
    /// <returns>
    /// The name of the application to store and retrieve role information for.
    /// </returns>
    public override string ApplicationName => this.roleProvider.ApplicationName;

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    public override string Name => this.roleProvider.Name;

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    public override string Title => this.roleProvider.Name;

    /// <summary>
    /// Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A brief, friendly description suitable for display in administrative tools or other UIs.
    /// </returns>
    public override string Description => this.roleProvider.Description;

    /// <summary>Gets the manager info.</summary>
    /// <value>The manager info.</value>
    public ManagerInfo ManagerInfo
    {
      get
      {
        if (this.managerInfo == null)
          this.managerInfo = new ManagerInfo()
          {
            ApplicationName = this.ApplicationName,
            ManagerType = typeof (RoleManager).FullName,
            ProviderName = this.Name,
            Id = Guid.NewGuid()
          };
        return this.managerInfo;
      }
    }

    public override IList<User> GetUsersInRole(
      Role role,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      List<User> usersInRole = new List<User>();
      string[] usernames = this.roleProvider.GetUsersInRole(role.Name);
      foreach (MembershipDataProvider membershipDataProvider in UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>())
      {
        List<User> collection1;
        if (membershipDataProvider is MembershipProviderWrapper)
        {
          collection1 = UserManager.GetManager(membershipDataProvider.Name).GetUsers().Where<User>((Expression<Func<User, bool>>) (u => usernames.Contains<string>(u.UserName))).ToList<User>();
        }
        else
        {
          collection1 = new List<User>();
          double num = Math.Ceiling((double) usersInRole.Count / 500.0);
          for (int index = 0; (double) index < num; ++index)
          {
            string[] currentSplit = ((IEnumerable<string>) usernames).Skip<string>(index * 500).Take<string>(500).ToArray<string>();
            IQueryable<User> collection2 = membershipDataProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => currentSplit.Contains<string>(u.UserName)));
            collection1.AddRange((IEnumerable<User>) collection2);
          }
        }
        usersInRole.AddRange((IEnumerable<User>) collection1);
      }
      totalCount = usersInRole.Count;
      return (IList<User>) usersInRole;
    }

    public override ManagerInfo GetManagerInfo(string managerType, string providerName) => new ManagerInfo()
    {
      ApplicationName = this.ApplicationName,
      ManagerType = managerType,
      ProviderName = providerName,
      Id = Guid.NewGuid()
    };

    /// <summary>Commits the provided transaction.</summary>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public override void CommitTransaction()
    {
      foreach (RoleWrapper roleWrapper in this.roleList.Values)
      {
        if (roleWrapper.IsNew)
        {
          this.roleProvider.CreateRole(roleWrapper.Name);
          roleWrapper.IsNew = false;
          roleWrapper.IsDirty = false;
        }
        if (roleWrapper.IsDirty)
          roleWrapper.IsDirty = false;
        if (roleWrapper.IsDeleted)
        {
          this.roleProvider.DeleteRole(roleWrapper.Name, false);
          this.roleList.Remove(roleWrapper.Id);
        }
      }
      foreach (UserLinkWrapper userLinkWrapper in this.userLinkList.Values)
      {
        if (userLinkWrapper.IsNew)
          this.roleProvider.AddUsersToRoles(new string[1]
          {
            new UserManager(userLinkWrapper.MembershipManagerInfo.ProviderName).GetUser(userLinkWrapper.UserId).UserName
          }, new string[1]{ userLinkWrapper.Role.Name });
        if (userLinkWrapper.IsDeleted)
          this.roleProvider.RemoveUsersFromRoles(new string[1]
          {
            new UserManager(userLinkWrapper.MembershipManagerInfo.ProviderName).GetUser(userLinkWrapper.UserId).UserName
          }, new string[1]{ userLinkWrapper.Role.Name });
      }
      this.roleList.Clear();
      this.userLinkList.Clear();
    }
  }
}
