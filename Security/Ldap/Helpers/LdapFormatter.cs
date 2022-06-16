// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.Helpers.LdapFormatter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Security.Ldap.Helpers
{
  public class LdapFormatter
  {
    /// <summary>Date Format in Ldap</summary>
    private string defaultGeneralizedTimeMask = "yyyyMMddHHmmss.0Z";
    private string generalizedTimeMask;

    static LdapFormatter() => ObjectFactory.Container.RegisterType<LdapFormatter>((LifetimeManager) new ContainerControlledLifetimeManager());

    public static LdapFormatter GetFormatter() => ObjectFactory.Resolve<LdapFormatter>();

    /// <summary>Convert Guid To Ldap format</summary>
    /// <param name="guid">Guid</param>
    /// <returns>string - Guid in Ldap Format</returns>
    public virtual string ToLdapFormat(Guid value)
    {
      byte[] byteArray = value.ToByteArray();
      string ldapFormat = "";
      foreach (byte num in byteArray)
        ldapFormat = ldapFormat + "\\" + num.ToString("x2");
      return ldapFormat;
    }

    /// <summary>
    /// Converts string in generalized time format to DateTime
    /// </summary>
    /// <param name="date">The date in generalized time format</param>
    /// <returns>Converted Datetime</returns>
    public virtual DateTime DateFromGeneralizedTime(string date) => DateTime.ParseExact(date, this.GeneralizedTimeMask, (IFormatProvider) CultureInfo.InvariantCulture);

    /// <summary>creates geeralized time string from datetime</summary>
    /// <param name="date">Datetime</param>
    /// <returns>Datetime converted to LdapFormat</returns>
    public virtual string DateToGeneralizedTime(DateTime date) => date.ToUniversalTime().ToString(this.GeneralizedTimeMask);

    public virtual string GeneralizedTimeMask
    {
      get => string.IsNullOrEmpty(this.generalizedTimeMask) ? this.defaultGeneralizedTimeMask : this.generalizedTimeMask;
      set => this.generalizedTimeMask = value;
    }

    /// <summary>Parse ldap distinguished name</summary>
    /// <param name="distiguishedName">ldap distinguished name</param>
    /// <returns>entry name</returns>
    public virtual string GetNameFromDn(string distinguishedName)
    {
      int length = distinguishedName.IndexOf(',');
      return length > 0 ? distinguishedName.Substring(0, length).Replace(",", "").Replace("CN=", "") : distinguishedName;
    }
  }
}
