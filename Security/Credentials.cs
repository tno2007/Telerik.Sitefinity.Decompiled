// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Credentials
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents credential information for web services authentication.
  /// </summary>
  [DataContract]
  public class Credentials
  {
    /// <summary>
    /// Gets or sets the name of the membership provider against which the credentials should be verified.
    /// </summary>
    /// <value>The name of the membership provider.</value>
    [DataMember]
    public string MembershipProvider { get; set; }

    /// <summary>Gets or sets the authentication username.</summary>
    /// <value>The name of the user.</value>
    [DataMember]
    public string UserName { get; set; }

    /// <summary>Gets or sets the password for authentication.</summary>
    /// <value>The password.</value>
    [DataMember]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the authentication cookie should be persisted after the current session.
    /// </summary>
    /// <value><c>true</c> if cookie should be persisted; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Persistent { get; set; }
  }
}
