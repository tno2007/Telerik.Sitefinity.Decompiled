// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfRole
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Class used to transfer roles through wcf services.</summary>
  [DataContract]
  [ManagerType("Telerik.Sitefinity.Security.RoleManager, Telerik.Sitefinity")]
  public class WcfRole : WcfItemBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfRole" /> class.
    /// </summary>
    public WcfRole()
    {
    }

    public WcfRole(Role role)
      : this(role.Name, role.Id, role.Users.Count, (DataProviderBase) ((IDataItem) role).Provider == null ? string.Empty : ((DataProviderBase) ((IDataItem) role).Provider).Name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfRole" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="roleId">The role ID.</param>
    /// <param name="usersInThisRole">The users in this role.</param>
    /// <param name="providerName">The name of role provider.</param>
    public WcfRole(string name, Guid roleId, int usersInThisRole, string providerName)
    {
      this.Name = name;
      this.Id = roleId;
      this.UsersInRole = usersInThisRole;
      this.ProviderName = providerName;
    }

    /// <summary>Gets or sets the role name.</summary>
    /// <value>The role name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the role pageId. Used in permissions.</summary>
    /// <value>The role pageId.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the number of users in the role.</summary>
    /// <value>The number of users in the role.</value>
    [DataMember]
    public int UsersInRole { get; set; }

    /// <summary>Gets or sets the name of the role provider.</summary>
    /// <value>The name of the role provider.</value>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the name of the role provider.</summary>
    /// <value>The name of the role provider.</value>
    [DataMember]
    public string ProviderTitle { get; set; }
  }
}
