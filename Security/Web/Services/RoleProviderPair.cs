// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.RoleProviderPair
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Class used in WCF to transfer the information about a role.
  /// </summary>
  [DataContract]
  public class RoleProviderPair
  {
    /// <summary>Gets or sets the name of the role.</summary>
    [DataMember]
    public string RoleName { get; set; }

    /// <summary>
    /// Gets of sets the ID of the role, converted into string.
    /// </summary>
    [DataMember]
    public string RoleId { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider to which role belongs to.
    /// </summary>
    [DataMember]
    public string ProviderName { get; set; }
  }
}
