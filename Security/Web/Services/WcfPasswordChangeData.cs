// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfPasswordChangeData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// This class is used to serialize the password change data through WCF service.
  /// </summary>
  [DataContract]
  [ManagerType("Telerik.Sitefinity.Security.UserManager")]
  public class WcfPasswordChangeData : WcfItemBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPasswordChangeData" /> class.
    /// </summary>
    public WcfPasswordChangeData()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPasswordChangeData" /> class.
    /// </summary>
    /// <param name="oldPassword">The old (current) password.</param>
    /// <param name="newPassword">The new (changed) password.</param>
    public WcfPasswordChangeData(string oldPassword, string newPassword)
    {
      this.OldPassword = oldPassword;
      this.NewPassword = newPassword;
    }

    /// <summary>The old (current) password.</summary>
    [DataMember]
    public string OldPassword { get; set; }

    /// <summary>The new (changed) password.</summary>
    [DataMember]
    public string NewPassword { get; set; }
  }
}
