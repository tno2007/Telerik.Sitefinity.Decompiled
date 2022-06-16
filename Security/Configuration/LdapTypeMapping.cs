// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LdapTypeMapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Contains configuration for mapping the Sitefinity objects to LDAP objects.
  /// </summary>
  public class LdapTypeMapping : ConfigElement
  {
    internal LdapTypeMapping()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.LdapTypeMapping" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public LdapTypeMapping(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Name of the types mapping configuration.
    /// For example UserMapping, RoleMapping ...
    /// </summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public virtual string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of Sitefinity objects to LDAP fields mappings.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapPropertiesMapping")]
    [ConfigurationProperty("propertiesMapping", IsDefaultCollection = true)]
    [ConfigurationCollection(typeof (LdapPropertyMapping), AddItemName = "mapping")]
    public virtual ConfigElementDictionary<string, LdapPropertyMapping> PropertiesMapping
    {
      get => (ConfigElementDictionary<string, LdapPropertyMapping>) this["propertiesMapping"];
      set => this["propertiesMapping"] = (object) value;
    }

    /// <summary>
    /// Retreives the name of the property of the mapped Sitefinity object using the LDAP field
    /// </summary>
    /// <param name="ldapField">The LDAP field</param>
    /// <returns>The name of the property of the mapped Sitefinity object</returns>
    public string GetPropertyName(string ldapField)
    {
      if (this.PropertiesMapping == null || this.PropertiesMapping.Count == 0)
        return (string) null;
      return this.PropertiesMapping.Values.Where<LdapPropertyMapping>((Func<LdapPropertyMapping, bool>) (p => p.LdapField == ldapField)).FirstOrDefault<LdapPropertyMapping>()?.PropertyName;
    }

    /// <summary>
    /// Retreives the name of the LDAP field using the name of the proeprty of the mapped Sitefinity object
    /// </summary>
    /// <param name="propertyName">The name of the property of the Sitefinity object</param>
    /// <returns>The name of the LDAP field mapped to the respective property of the Sitefinity object.</returns>
    public string GetLdapFieldName(string propertyName)
    {
      if (this.PropertiesMapping == null || this.PropertiesMapping.Count == 0)
        return (string) null;
      return this.PropertiesMapping.ContainsKey(propertyName) ? this.PropertiesMapping[propertyName].LdapField : (string) null;
    }
  }
}
