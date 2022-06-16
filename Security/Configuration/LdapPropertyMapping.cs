// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LdapPropertyMapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Contains configuration for mapping the Sitefinity object properties to
  /// the respective LDAP object properties
  /// </summary>
  public class LdapPropertyMapping : ConfigElement
  {
    internal LdapPropertyMapping()
      : base(false)
    {
    }

    /// <summary>Create an instance of LdapPropertyMapping</summary>
    /// <param name="parent">parent config element</param>
    public LdapPropertyMapping(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Crate an instance of LpdaPropertyMapping</summary>
    /// <param name="propertyName">property name</param>
    /// <param name="ldapField">ldap attribute name</param>
    /// <param name="parent">parent config element</param>
    public LdapPropertyMapping(string propertyName, string ldapField, ConfigElement parent)
      : base(parent)
    {
      this[nameof (propertyName)] = (object) propertyName;
      this[nameof (ldapField)] = (object) ldapField;
    }

    /// <summary>
    /// Name of the property of the Sitefinity object to map to LDAP field
    /// </summary>
    [ConfigurationProperty("propertyName", IsKey = true, IsRequired = true)]
    public virtual string PropertyName
    {
      get => (string) this["propertyName"];
      set => this["propertyName"] = (object) value;
    }

    /// <summary>Name of the LDAP field to map the property</summary>
    [ConfigurationProperty("ldapField", IsRequired = true)]
    public virtual string LdapField
    {
      get => (string) this["ldapField"];
      set => this["ldapField"] = (object) value;
    }
  }
}
