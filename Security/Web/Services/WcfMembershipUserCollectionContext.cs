// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Implements <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" />
  /// that holds <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUser" />
  /// and adds some additional information that can be sent to the server
  /// </summary>
  [DataContract]
  public class WcfMembershipUserCollectionContext : CollectionContext<WcfMembershipUser>
  {
    private Dictionary<string, int> numberOfUsersInRoles;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfMembershipUserCollectionContext" /> class.
    /// </summary>
    /// <param name="wcfUsers">The WCF users.</param>
    public WcfMembershipUserCollectionContext(IEnumerable<WcfMembershipUser> wcfUsers)
      : base(wcfUsers)
    {
    }

    /// <summary>Gets or sets the number of users in roles.</summary>
    /// <value>The number of users in roles.</value>
    [DataMember]
    public Dictionary<string, int> NumberOfUsersInRoles
    {
      get
      {
        if (this.numberOfUsersInRoles == null)
          this.numberOfUsersInRoles = new Dictionary<string, int>();
        return this.numberOfUsersInRoles;
      }
      set => this.numberOfUsersInRoles = value;
    }
  }
}
