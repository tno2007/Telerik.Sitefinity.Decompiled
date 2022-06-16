// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LdapMappingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Reflection;
using System.Web.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Defines Mapping between Ldap entry and Sitefinity Type
  /// </summary>
  public class LdapMappingConfig : ConfigElement
  {
    /// <summary>User mapping key</summary>
    public const string UserMappingName = "UserMapping";
    /// <summary>Role mapping key</summary>
    public const string RoleMappingName = "RoleMapping";

    internal LdapMappingConfig()
      : base(false)
    {
    }

    /// <summary>Create Instance of LpdaMappingConfig</summary>
    /// <param name="parent">parent config element</param>
    public LdapMappingConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Get the ldap field name which is mapped to the Sitefinity object property
    /// </summary>
    /// <param name="mi">MemberInfo object of the Sitefinity object property</param>
    /// <param name="result">Mapped ldap field</param>
    /// <returns>True if mapping exists</returns>
    public bool TryReverseMapping(MemberInfo mi, out string result)
    {
      result = !typeof (MembershipUser).IsAssignableFrom(mi.DeclaringType) ? this.RoleMapping.GetLdapFieldName(mi.Name) : this.UserMapping.GetLdapFieldName(mi.Name);
      return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Get the ldap field name which is mapped to the Sitefinity object property
    /// </summary>
    /// <param name="mi">MemberInfo object of the Sitefinity object property</param>
    /// <returns></returns>
    public string ReverseMapping(MemberInfo mi)
    {
      string str = !typeof (MembershipUser).IsAssignableFrom(mi.DeclaringType) ? this.RoleMapping.GetLdapFieldName(mi.Name) : this.UserMapping.GetLdapFieldName(mi.Name);
      return !string.IsNullOrEmpty(str) ? str : throw new ArgumentOutOfRangeException("Unable to find mapping for property:" + mi.Name);
    }

    /// <summary>Mappings for User object in LDAP</summary>
    public virtual LdapTypeMapping UserMapping => this.TypesMapping[nameof (UserMapping)];

    /// <summary>Mappings for the Role object in LDAP</summary>
    public virtual LdapTypeMapping RoleMapping => this.TypesMapping[nameof (RoleMapping)];

    /// <summary>
    /// Gets a collection of Sitefinity objects that should be mapped to LDAP objects.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapTypesMapping")]
    [ConfigurationProperty("typesMapping", IsDefaultCollection = true)]
    [ConfigurationCollection(typeof (LdapTypeMapping), AddItemName = "type")]
    public virtual ConfigElementDictionary<string, LdapTypeMapping> TypesMapping
    {
      get => (ConfigElementDictionary<string, LdapTypeMapping>) this["typesMapping"];
      set => this["typesMapping"] = (object) value;
    }

    /// <summary>Fired when configuration is loaded</summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.AddDefaultMappings();
    }

    /// <summary>
    /// Adding the default mappings for Usres and Roles to LDAP fields
    /// </summary>
    public virtual void AddDefaultMappings()
    {
      lock (this)
      {
        if (!this.TypesMapping.ContainsKey("RoleMapping"))
        {
          LdapTypeMapping element = new LdapTypeMapping((ConfigElement) this.TypesMapping);
          element.Name = "RoleMapping";
          if (element.PropertiesMapping == null)
            element.PropertiesMapping = new ConfigElementDictionary<string, LdapPropertyMapping>();
          element.PropertiesMapping.Add("Name", new LdapPropertyMapping("Name", "sAMAccountName", (ConfigElement) element.PropertiesMapping));
          element.PropertiesMapping.Add("Id", new LdapPropertyMapping("Id", "objectGUID", (ConfigElement) element.PropertiesMapping));
          element.PropertiesMapping.Add("LastModified", new LdapPropertyMapping("LastModified", "whenChanged", (ConfigElement) element.PropertiesMapping));
          this.TypesMapping.Add("RoleMapping", element);
        }
        if (this.TypesMapping.ContainsKey("UserMapping"))
          return;
        LdapTypeMapping element1 = new LdapTypeMapping((ConfigElement) this.TypesMapping);
        element1.Name = "UserMapping";
        if (element1.PropertiesMapping == null)
          element1.PropertiesMapping = new ConfigElementDictionary<string, LdapPropertyMapping>();
        element1.PropertiesMapping.Add("Id", new LdapPropertyMapping("Id", "objectGUID", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("Comment", new LdapPropertyMapping("Comment", "info", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("FirstName", new LdapPropertyMapping("FirstName", "givenName", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("UserName", new LdapPropertyMapping("UserName", "sAMAccountName", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("Email", new LdapPropertyMapping("Email", "mail", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("LastName", new LdapPropertyMapping("LastName", "sn", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("LastActivityDate", new LdapPropertyMapping("LastActivityDate", "whenChanged", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("LastLoginDate", new LdapPropertyMapping("LastLoginDate", "lastLogon", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("CreationDate", new LdapPropertyMapping("CreationDate", "whenCreated", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("LastModified", new LdapPropertyMapping("LastModified", "whenChanged", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("MemberOf", new LdapPropertyMapping("memberOf", "memberOf", (ConfigElement) element1.PropertiesMapping));
        element1.PropertiesMapping.Add("IsApproved", new LdapPropertyMapping("IsApproved", "userAccountControl", (ConfigElement) element1.PropertiesMapping));
        this.TypesMapping.Add("UserMapping", element1);
      }
    }
  }
}
