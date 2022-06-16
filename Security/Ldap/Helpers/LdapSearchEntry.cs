// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.Helpers.LdapFilterEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Ldap.Helpers
{
  /// <summary>Filter class used for building Filter for Ldap Query</summary>
  public class LdapFilterEntry
  {
    /// <summary>Filter for Ldap Query</summary>
    private string filter = string.Empty;

    /// <summary>Default Constructor</summary>
    public LdapFilterEntry()
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="LdapFilterEntry">Filter Entry</param>
    public LdapFilterEntry(LdapFilterEntry LdapFilterEntry) => this.filter = LdapFilterEntry.filter;

    /// <summary>Constructor</summary>
    /// <param name="filter">Format : (Key=Value)</param>
    public LdapFilterEntry(string filter) => this.filter = filter;

    /// <summary>Constructor</summary>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    public LdapFilterEntry(string key, string value) => this.filter = string.Format("({0}={1})", (object) key, (object) value);

    /// <summary>Gets or sets the filter.</summary>
    /// <value>The filter.</value>
    public string Filter
    {
      get => this.filter;
      set => this.filter = value;
    }

    /// <summary>
    /// Overloaded Operator for And ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1">First Filter</param>
    /// <param name="ldapFilterEntry2">Second Filter</param>
    /// <returns>Combine Filter</returns>
    public static LdapFilterEntry operator &(
      LdapFilterEntry ldapFilterEntry1,
      LdapFilterEntry ldapFilterEntry2)
    {
      if (string.IsNullOrEmpty(ldapFilterEntry1.Filter))
        return ldapFilterEntry2;
      if (string.IsNullOrEmpty(ldapFilterEntry2.Filter))
        return ldapFilterEntry1;
      return ldapFilterEntry1.Filter.Length > 3 && ldapFilterEntry1.Filter[1] == '&' ? new LdapFilterEntry(ldapFilterEntry1.Filter.Insert(2, ldapFilterEntry2.ToString())) : new LdapFilterEntry(string.Format("(&{0}{1})", (object) ldapFilterEntry1, (object) ldapFilterEntry2));
    }

    /// <summary>
    /// Overloaded Operator for And ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1">First Filter</param>
    /// <param name="ldapFilterEntry2">Second Filter</param>
    /// <returns>Combine Filter</returns>
    public static LdapFilterEntry operator &(
      string ldapFilterEntry1,
      LdapFilterEntry ldapFilterEntry2)
    {
      return new LdapFilterEntry(ldapFilterEntry1) & ldapFilterEntry2;
    }

    /// <summary>
    /// Overloaded Operator for And ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1">First Filter</param>
    /// <param name="ldapFilterEntry2">Second Filter</param>
    /// <returns></returns>
    public static LdapFilterEntry operator &(
      LdapFilterEntry ldapFilterEntry1,
      string ldapFilterEntry2)
    {
      return ldapFilterEntry1 & new LdapFilterEntry(ldapFilterEntry2);
    }

    /// <summary>
    /// Overloaded Operator for OR ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1">First Filter</param>
    /// <param name="ldapFilterEntry2">Second Filter</param>
    /// <returns>Combine Filter</returns>
    public static LdapFilterEntry operator |(
      LdapFilterEntry ldapFilterEntry1,
      LdapFilterEntry ldapFilterEntry2)
    {
      if (string.IsNullOrEmpty(ldapFilterEntry1.Filter))
        return ldapFilterEntry2;
      if (string.IsNullOrEmpty(ldapFilterEntry2.Filter))
        return ldapFilterEntry1;
      return ldapFilterEntry1.Filter.Length > 3 && ldapFilterEntry1.Filter[1] == '|' ? new LdapFilterEntry(ldapFilterEntry1.Filter.Insert(2, ldapFilterEntry2.ToString())) : new LdapFilterEntry(string.Format("(|{0}{1})", (object) ldapFilterEntry1, (object) ldapFilterEntry2));
    }

    /// <summary>
    /// Overloaded Operator for OR ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1">First Filter</param>
    /// <param name="ldapFilterEntry2">Second Filter</param>
    /// <returns>Combine Filter</returns>
    public static LdapFilterEntry operator |(
      string ldapFilterEntry1,
      LdapFilterEntry ldapFilterEntry2)
    {
      return new LdapFilterEntry(ldapFilterEntry1) | ldapFilterEntry2;
    }

    /// <summary>
    /// Overloaded Operator for OR ldapFilterEntry1 and ldapFilterEntry2
    /// </summary>
    /// <param name="ldapFilterEntry1"></param>
    /// <param name="ldapFilterEntry2"></param>
    /// <returns></returns>
    public static LdapFilterEntry operator |(
      LdapFilterEntry ldapFilterEntry1,
      string ldapFilterEntry2)
    {
      return ldapFilterEntry1 | new LdapFilterEntry(ldapFilterEntry2);
    }

    /// <summary>Ldap Filter</summary>
    /// <returns>Ldap filter for Search Query</returns>
    public override string ToString() => this.Filter;
  }
}
