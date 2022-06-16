// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.Helpers.LdapBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.DirectoryServices.Protocols;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Ldap.Helpers
{
  /// <summary>Build Sitefinity Objects from Ldap Result Entry</summary>
  public class LdapBuilder
  {
    static LdapBuilder() => ObjectFactory.Container.RegisterType<LdapBuilder>((LifetimeManager) new ContainerControlledLifetimeManager());

    /// <summary>Build Sitefinity Object from Ldap Result Entry</summary>
    /// <typeparam name="T"> Role , User , UserLink</typeparam>
    /// <param name="entry"> Result from search in Ldap</param>
    /// <param name="managerInfo">Provider manager info</param>
    /// <param name="applicationName">Provider application name</param>
    /// <returns>Role , User , UserLink</returns>
    public virtual T Build<T>(
      SearchResultEntry entry,
      ManagerInfo managerInfo,
      string applicationName)
      where T : class
    {
      if (typeof (T) == typeof (User))
        return this.UserFromSearchEntry(entry, managerInfo, applicationName) as T;
      if (typeof (T) == typeof (Role))
        return this.RoleFromEntry(entry, applicationName) as T;
      if (typeof (T) == typeof (UserLink))
        return this.UserLinkFromEntry(entry, managerInfo, applicationName) as T;
      throw new ArgumentException("Unable to parse type of " + typeof (T).FullName);
    }

    public static LdapBuilder GetBuilder() => ObjectFactory.Resolve<LdapBuilder>();

    /// <summary>Build User</summary>
    /// <param name="entry">Result from search in Ldap</param>
    /// <param name="managerInfo">Provider manager info</param>
    /// <param name="applicationName">Provider application name</param>
    /// <returns>User</returns>
    protected virtual User UserFromSearchEntry(
      SearchResultEntry entry,
      ManagerInfo managerInfo,
      string applicationName)
    {
      User user = new User();
      LdapTypeMapping userMapping = Config.Get<SecurityConfig>().LdapConnections.LdapMapping.UserMapping;
      user.ApplicationName = applicationName;
      string attributeValue1 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("Comment"));
      if (attributeValue1 != null)
        user.Comment = attributeValue1;
      string attributeValue2 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("Email"));
      if (attributeValue2 != null)
        user.Email = attributeValue2;
      else
        user.Email = string.Empty;
      string attributeValue3 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("Username"));
      if (attributeValue3 != null)
        user.SetUserName(attributeValue3);
      string attributeValue4 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("FirstName"));
      user.FirstName = attributeValue4 == null ? string.Empty : attributeValue4;
      user.Id = new Guid(LdapUtilities.GetAttributeValue<byte[]>(entry, userMapping.GetLdapFieldName("Id")));
      string attributeValue5 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("LastModified"));
      if (attributeValue5 != null)
        user.LastModified = LdapUtilities.DateFromGeneralizedTime(attributeValue5);
      string attributeValue6 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("CreationDate"));
      if (attributeValue6 != null)
        user.SetCreationDate(LdapUtilities.DateFromGeneralizedTime(attributeValue6));
      string attributeValue7 = LdapUtilities.GetAttributeValue<string>(entry, userMapping.GetLdapFieldName("LastName"));
      user.LastName = attributeValue7 == null ? string.Empty : attributeValue7;
      user.ManagerInfo = managerInfo;
      user.IsApproved = false;
      if (entry.Attributes[userMapping.GetLdapFieldName("IsApproved")] != null && entry.Attributes[userMapping.GetLdapFieldName("IsApproved")].Count > 0)
      {
        int result = 0;
        for (int index = 0; index < entry.Attributes[userMapping.GetLdapFieldName("IsApproved")].Count; ++index)
        {
          if (int.TryParse((string) entry.Attributes[userMapping.GetLdapFieldName("IsApproved")][index], out result) && (result & 2) == 0)
          {
            user.IsApproved = true;
            break;
          }
        }
      }
      return user;
    }

    /// <summary>Build Role</summary>
    /// <param name="entry">Result from search in Ldap</param>
    /// <param name="applicationName"></param>
    /// <returns>Role</returns>
    protected virtual Role RoleFromEntry(SearchResultEntry entry, string applicationName)
    {
      if (entry == null)
        return (Role) null;
      LdapTypeMapping roleMapping = Config.Get<SecurityConfig>().LdapConnections.LdapMapping.RoleMapping;
      Role role = new Role();
      role.Name = LdapUtilities.GetAttributeValue<string>(entry, roleMapping.GetLdapFieldName("Name"));
      byte[] attributeValue1 = LdapUtilities.GetAttributeValue<byte[]>(entry, roleMapping.GetLdapFieldName("Id"));
      role.Id = new Guid(attributeValue1);
      string attributeValue2 = LdapUtilities.GetAttributeValue<string>(entry, roleMapping.GetLdapFieldName("LastModified"));
      if (attributeValue2 != null)
        role.LastModified = LdapUtilities.DateFromGeneralizedTime(attributeValue2);
      role.ApplicationName = applicationName;
      return role;
    }

    /// <summary>Build UserLink</summary>
    /// <param name="entry">Result from search in Ldap</param>
    /// <param name="managerInfo"></param>
    /// <param name="applicationName"></param>
    /// <returns>UserLink</returns>
    protected virtual UserLink UserLinkFromEntry(
      SearchResultEntry entry,
      ManagerInfo managerInfo,
      string applicationName)
    {
      LdapTypeMapping userMapping = Config.Get<SecurityConfig>().LdapConnections.LdapMapping.UserMapping;
      return new UserLink()
      {
        Id = Guid.NewGuid(),
        UserId = new Guid(LdapUtilities.GetAttributeValue<byte[]>(entry, userMapping.GetLdapFieldName("Id"))),
        MembershipManagerInfo = managerInfo,
        ApplicationName = applicationName
      };
    }
  }
}
