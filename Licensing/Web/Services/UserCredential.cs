// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.DataContracts.Base.UserCredential
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Licensing.DataContracts.Base
{
  /// <summary>User credential data contract</summary>
  [DataContract]
  public class UserCredential
  {
    /// <summary>Gets or sets the hashed key</summary>
    [DataMember]
    public string HashedKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to remember the user credentials
    /// </summary>
    [DataMember]
    public bool IsRemembered { get; set; }

    /// <summary>Gets or sets the user name</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the user password</summary>
    [DataMember]
    public string Password { get; set; }
  }
}
