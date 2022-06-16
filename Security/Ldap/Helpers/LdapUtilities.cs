// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.Helpers.LdapUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Linq;

namespace Telerik.Sitefinity.Security.Ldap.Helpers
{
  /// <summary>
  /// Contains Utility methods used in communication with Ldap
  /// </summary>
  public static class LdapUtilities
  {
    /// <summary>Convert Guid To Ldap format</summary>
    /// <param name="guid">Guid</param>
    /// <returns>string - Guid in Ldap Format</returns>
    public static string ToLdapFormat(this Guid value) => LdapFormatter.GetFormatter().ToLdapFormat(value);

    /// <summary>
    /// Converts string in generalized time format to DateTime
    /// </summary>
    /// <param name="date">The date in generalized time format</param>
    /// <returns>Converted Datetime</returns>
    public static DateTime DateFromGeneralizedTime(string date) => LdapFormatter.GetFormatter().DateFromGeneralizedTime(date);

    /// <summary>creates geeralized time string from datetime</summary>
    /// <param name="date">Datetime</param>
    /// <returns>Datetime converted to LdapFormat</returns>
    public static string DateToGeneralizedTime(DateTime date) => LdapFormatter.GetFormatter().DateToGeneralizedTime(date);

    /// <summary>Get Attribute from Search Result Entry</summary>
    /// <typeparam name="T">return type of the attribute value</typeparam>
    /// <param name="entry">Search result entry</param>
    /// <param name="attributeName">name of the search result entry attribute</param>
    /// <returns>value of attribute or default value of T </returns>
    public static T GetAttributeValue<T>(SearchResultEntry entry, string attributeName)
    {
      string lower = attributeName.ToLower(CultureInfo.InvariantCulture);
      return entry.Attributes.Contains(lower) ? (T) ((IEnumerable<object>) entry.Attributes[lower].GetValues(typeof (T))).FirstOrDefault<object>() : default (T);
    }

    /// <summary>Parse ldap distinguished name</summary>
    /// <param name="distiguishedName">ldap distinguished name</param>
    /// <returns>entry name</returns>
    public static string GetNameFromDn(string distinguishedName) => LdapFormatter.GetFormatter().GetNameFromDn(distinguishedName);
  }
}
