// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapProviderException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration.Provider;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  /// </summary>
  [Serializable]
  public class LdapProviderException : ProviderException
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Security.Ldap.LdapProviderException" /> class
    /// </summary>
    public LdapProviderException()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Security.Ldap.LdapProviderException" /> class
    /// <param name="msg">error message</param>
    /// </summary>
    public LdapProviderException(string msg)
      : this(msg, (Exception) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Security.Ldap.LdapProviderException" /> class
    /// </summary>
    /// <param name="msg">error message</param>
    /// <param name="e">Exception</param>
    public LdapProviderException(string msg, Exception e)
      : base(msg, e)
    {
    }
  }
}
