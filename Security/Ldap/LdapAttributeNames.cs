// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapAttributeNames
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>contains all the attributes names needed</summary>
  public static class LdapAttributeNames
  {
    /// <summary>Object Class Attribute</summary>
    public static string objectClassAttribute = "objectClass";
    /// <summary>Group attribute</summary>
    public static string objectGroupAttribute = "group";
    /// <summary>Ldap email Attribute</summary>
    public static string emailAttribute = "mail";
    /// <summary>Ldap distinguished name</summary>
    public static string distinguishedNameAttribute = "distinguishedname";
    /// <summary>ldap memberOf</summary>
    public static string memberOf = nameof (memberOf);
    private static string objectGuidAttribute = "";
    private static string roleIdAttribute = "";
    private static string userNameAttributeValue = "";
    private static string roleNameAttributeValue = "";
    /// <summary>Ldap when changed attribute</summary>
    public static string whenChangedAttribute = "whenChanged";

    /// <summary>
    /// Ldap attribute used for mapping and search by username, usually sAMAccountName but can be also UserPrincipalName etc.
    /// </summary>
    public static string UserNameAttribute
    {
      get
      {
        if (string.IsNullOrEmpty(LdapAttributeNames.userNameAttributeValue))
          LdapAttributeNames.GetFromConfig(LdapAttributeNames.LdapMappingType.UsersMapping, "UserName", "sAMAccountName", ref LdapAttributeNames.userNameAttributeValue);
        return LdapAttributeNames.userNameAttributeValue;
      }
      set => LdapAttributeNames.userNameAttributeValue = value;
    }

    public static string RoleNameAttribute
    {
      get
      {
        if (string.IsNullOrEmpty(LdapAttributeNames.roleNameAttributeValue))
          LdapAttributeNames.GetFromConfig(LdapAttributeNames.LdapMappingType.RolesMapping, "Name", "sAMAccountName", ref LdapAttributeNames.roleNameAttributeValue);
        return LdapAttributeNames.roleNameAttributeValue;
      }
      set => LdapAttributeNames.roleNameAttributeValue = value;
    }

    /// <summary>
    /// Ldap attribute used for unique Id of a User , should be GUID
    /// </summary>
    public static string ObjectGuidAttribute
    {
      get
      {
        if (string.IsNullOrEmpty(LdapAttributeNames.objectGuidAttribute))
          LdapAttributeNames.GetFromConfig(LdapAttributeNames.LdapMappingType.UsersMapping, "Id", "objectGUID", ref LdapAttributeNames.objectGuidAttribute);
        return LdapAttributeNames.objectGuidAttribute;
      }
      set => LdapAttributeNames.objectGuidAttribute = value;
    }

    /// Ldap attribute used for Id of a Role , should be GUID
    public static string RoleIdAttribute
    {
      get
      {
        if (string.IsNullOrEmpty(LdapAttributeNames.roleIdAttribute))
          LdapAttributeNames.GetFromConfig(LdapAttributeNames.LdapMappingType.RolesMapping, "Id", "objectGUID", ref LdapAttributeNames.roleIdAttribute);
        return LdapAttributeNames.roleIdAttribute;
      }
      set => LdapAttributeNames.roleIdAttribute = value;
    }

    private static void GetFromConfig(
      LdapAttributeNames.LdapMappingType mappingType,
      string propertyMappingName,
      string defaultLdapField,
      ref string attributeCache)
    {
      lock (attributeCache)
      {
        if (!string.IsNullOrEmpty(attributeCache))
          return;
        LdapMappingConfig ldapMapping = Config.Get<SecurityConfig>().LdapConnections.LdapMapping;
        LdapPropertyMapping ldapPropertyMapping;
        if (mappingType == LdapAttributeNames.LdapMappingType.UsersMapping)
          ldapMapping.UserMapping.PropertiesMapping.TryGetValue(propertyMappingName, out ldapPropertyMapping);
        else
          ldapMapping.RoleMapping.PropertiesMapping.TryGetValue(propertyMappingName, out ldapPropertyMapping);
        if (ldapPropertyMapping != null)
          attributeCache = ldapPropertyMapping.LdapField;
        else
          attributeCache = defaultLdapField;
      }
    }

    private enum LdapMappingType
    {
      UsersMapping,
      RolesMapping,
    }
  }
}
