// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Ldap;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Ldap.Helpers;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  /// Represents encapsulation of the communication to the LDAP providers and operations
  /// with the LDAP server
  /// </summary>
  public class LdapFacade : IDisposable, ILdapFacade
  {
    protected LdapConnection connection;
    protected LdapSettingsConfig currentSettings;
    protected LdapMappingConfig mappings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Ldap.LdapFacade" /> class.
    /// </summary>
    public LdapFacade()
    {
      this.connection = (LdapConnection) null;
      this.currentSettings = (LdapSettingsConfig) null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Ldap.LdapFacade" /> class.
    /// </summary>
    /// <param name="connectionName">Name of the connection.</param>
    public LdapFacade(string connectionName) => this.LdapConnectionName = connectionName;

    /// <summary>Generates cache key for each query</summary>
    /// <param name="query">LdapQuery</param>
    /// <returns>The key</returns>
    protected virtual string GetQueryCacheKey(LdapQuery query) => this.GetQueryCacheKey(query, false);

    /// <summary>Generates cache key for each query</summary>
    /// <param name="query">Ldap query</param>
    /// <param name="countOnly">count only</param>
    /// <returns>The key</returns>
    protected virtual string GetQueryCacheKey(LdapQuery query, bool countOnly)
    {
      LdapSettingsConfig connectionSettings = this.ConnectionSettings;
      string empty = string.Empty;
      if (query.SelectColumns.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string selectColumn in query.SelectColumns)
        {
          stringBuilder.Append(selectColumn);
          stringBuilder.Append(";");
        }
        empty = stringBuilder.ToString();
      }
      return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", (object) connectionSettings.Name, (object) query.Filter, (object) query.Skip, (object) query.Take, (object) empty, (object) countOnly);
    }

    /// <summary>Adding the result to the cache for latter retreive</summary>
    /// <param name="query">Query passed to the search</param>
    /// <param name="result">The result returned from the search</param>
    protected virtual void AddResultToCache(LdapQuery query, IEnumerable<SearchResultEntry> result)
    {
      if (this.ConnectionSettings.ResultCacheExpirationTime == 0 || result == null || result.Count<SearchResultEntry>() == 0)
        return;
      string queryCacheKey = this.GetQueryCacheKey(query);
      if (this.Cache.Contains(queryCacheKey))
        return;
      this.Cache.Add(queryCacheKey, (object) result, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new AbsoluteTime(TimeSpan.FromMinutes((double) this.ConnectionSettings.ResultCacheExpirationTime)));
    }

    /// <summary>Returns result from cache</summary>
    /// <param name="query">Ldap Query</param>
    /// <returns>Collections of items</returns>
    protected virtual IEnumerable<SearchResultEntry> GetResultFromCache(
      LdapQuery query)
    {
      if (this.ConnectionSettings.ResultCacheExpirationTime == 0)
        return (IEnumerable<SearchResultEntry>) null;
      string queryCacheKey = this.GetQueryCacheKey(query);
      return this.Cache.Contains(queryCacheKey) ? (IEnumerable<SearchResultEntry>) this.Cache.GetData(queryCacheKey) : (IEnumerable<SearchResultEntry>) null;
    }

    /// <summary>
    /// Specifies the connection name to be used by the facade
    /// </summary>
    public virtual string LdapConnectionName { get; set; }

    /// <summary>Cache of the LDAP query results</summary>
    public virtual ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    /// <summary>Connection Settings</summary>
    public virtual LdapSettingsConfig ConnectionSettings
    {
      get
      {
        if (this.currentSettings == null)
        {
          SecurityConfig securityConfig = Config.Get<SecurityConfig>();
          ConfigElementDictionary<string, LdapSettingsConfig> connections = securityConfig.LdapConnections.Connections;
          string key = securityConfig.LdapConnections.DefaultLdapConnection;
          if (!string.IsNullOrEmpty(this.LdapConnectionName))
            key = this.LdapConnectionName;
          LdapSettingsConfig ldapSettingsConfig;
          this.currentSettings = !connections.TryGetValue(key, out ldapSettingsConfig) ? connections.Values.FirstOrDefault<LdapSettingsConfig>() : ldapSettingsConfig;
        }
        return this.currentSettings;
      }
    }

    public virtual LdapMappingConfig Mappings
    {
      get
      {
        if (this.mappings == null)
          this.mappings = Config.Get<SecurityConfig>().LdapConnections.LdapMapping;
        return this.mappings;
      }
    }

    /// <summary>Get ldap connection</summary>
    /// <returns>ldap connection with default settings</returns>
    protected virtual LdapConnection GetConnection() => this.GetConnection(this.ConnectionSettings, (string) null, (string) null);

    /// <summary>Get ldap connection  by username and password</summary>
    /// <param name="userName">Username</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    protected virtual LdapConnection GetConnection(string userName, string password) => this.GetConnection(this.ConnectionSettings, userName, password);

    protected virtual LdapConnection GetConnection(
      LdapSettingsConfig settings,
      string userName,
      string password)
    {
      LdapConnection ldapConnection = (LdapConnection) null;
      string connectionCacheKey = this.GetConnectionCacheKey();
      if (this.connection == null)
      {
        if (SystemManager.HttpContextItems != null && SystemManager.HttpContextItems[(object) connectionCacheKey] != null)
        {
          this.connection = (LdapConnection) SystemManager.HttpContextItems[(object) connectionCacheKey];
          return this.connection;
        }
        LdapDirectoryIdentifier directoryIdentifier = this.GetLdapDirectoryIdentifier(settings);
        NetworkCredential networkCredential = this.GetNetworkCredential(settings, userName, password);
        if (settings.ConnectWithLogOnCredentials && networkCredential == null)
          return (LdapConnection) null;
        ldapConnection = this.BuildLdapConnection(settings, directoryIdentifier, networkCredential);
      }
      if (ldapConnection != null)
      {
        if (SystemManager.HttpContextItems != null)
          SystemManager.HttpContextItems[(object) connectionCacheKey] = (object) ldapConnection;
        this.connection = ldapConnection;
      }
      return this.connection;
    }

    /// <summary>
    /// Builds the LDAP connection using the provided settings LdapDirectoryIdentifier and NetworkCredential.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="identifier">The LdapDirectoryIdentifier.</param>
    /// <param name="credential">The NetworkCredential used to identify.</param>
    /// <returns></returns>
    protected virtual LdapConnection BuildLdapConnection(
      LdapSettingsConfig settings,
      LdapDirectoryIdentifier identifier,
      NetworkCredential credential)
    {
      LdapConnection ldapConnection = new LdapConnection(identifier, credential);
      ldapConnection.AuthType = settings.AuthenticationType == AuthType.Ntlm ? AuthType.Negotiate : settings.AuthenticationType;
      ldapConnection.SessionOptions.ProtocolVersion = settings.LdapVersion;
      ldapConnection.Timeout = TimeSpan.FromSeconds(20.0);
      if (this.ConnectionSettings.UseSsl)
      {
        ldapConnection.SessionOptions.SecureSocketLayer = true;
        ldapConnection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(this.VerificateServerCertificate);
      }
      return ldapConnection;
    }

    /// <summary>
    /// Gets the network credential used to connect to the ldap server.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    protected virtual NetworkCredential GetNetworkCredential(
      LdapSettingsConfig settings,
      string userName,
      string password)
    {
      NetworkCredential networkCredential = (NetworkCredential) null;
      if (settings.ConnectWithLogOnCredentials)
      {
        if (settings.AuthenticationType == AuthType.Ntlm)
          networkCredential = CredentialCache.DefaultNetworkCredentials;
        else if (userName != null)
          networkCredential = new NetworkCredential(userName, password);
        else if (Thread.CurrentPrincipal.Identity is SitefinityIdentity identity)
          networkCredential = LdapCredentialsCache.GetCredential(identity.UserId);
      }
      else
        networkCredential = new NetworkCredential(settings.ConnectionUsername, settings.ConnectionPassword, settings.ConnectionDomain);
      return networkCredential;
    }

    /// <summary>Gets the LDAP directory identifier.</summary>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    protected virtual LdapDirectoryIdentifier GetLdapDirectoryIdentifier(
      LdapSettingsConfig settings)
    {
      return new LdapDirectoryIdentifier(settings.ServerName.Split(new char[1]
      {
        ';'
      }, StringSplitOptions.RemoveEmptyEntries), settings.Port, true, false);
    }

    protected virtual string GetConnectionCacheKey() => "ldapconn_" + this.LdapConnectionName;

    /// <summary>Clears the connections this instance.</summary>
    protected virtual void ClearConnection()
    {
      this.connection = (LdapConnection) null;
      SystemManager.HttpContextItems.Remove((object) this.GetConnectionCacheKey());
    }

    protected virtual bool VerificateServerCertificate(
      LdapConnection connection,
      X509Certificate certificate)
    {
      return true;
    }

    /// <summary>Add search option control to Search request</summary>
    /// <param name="search">Search request</param>
    protected virtual void AddSearchOptionsControl(DirectoryRequest search)
    {
      SearchOptionsControl control = new SearchOptionsControl(SearchOption.DomainScope);
      search.Controls.Add((DirectoryControl) control);
    }

    /// <summary>Create instance of Search request</summary>
    /// <param name="query">ldap query</param>
    /// <param name="rootDN">root distinguished name</param>
    /// <param name="pageSize">entry's count per request</param>
    /// <returns>Search request</returns>
    protected virtual SearchRequest PrepareSearchRequest(
      LdapQuery query,
      string rootDN,
      int pageSize)
    {
      string[] attributes = this.GetAttributes(query);
      string ldapFilter = query.Filter.ToString();
      SearchRequest search = new SearchRequest(rootDN, ldapFilter, SearchScope.Subtree, attributes);
      if (query.OrderColumns.Count > 0)
      {
        SortRequestControl control = new SortRequestControl(this.ToSortKeys((IEnumerable<SortOptions>) query.OrderColumns));
        search.Controls.Add((DirectoryControl) control);
      }
      PageResultRequestControl control1 = new PageResultRequestControl(pageSize);
      search.Controls.Add((DirectoryControl) control1);
      this.AddSearchOptionsControl((DirectoryRequest) search);
      return search;
    }

    /// <summary>Gets the attributes.</summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    protected virtual string[] GetAttributes(LdapQuery query)
    {
      IEnumerable<string> strings1 = this.Mappings.UserMapping.PropertiesMapping.Values.Select<LdapPropertyMapping, string>((Func<LdapPropertyMapping, string>) (u => u.LdapField));
      IEnumerable<string> strings2 = this.Mappings.RoleMapping.PropertiesMapping.Values.Select<LdapPropertyMapping, string>((Func<LdapPropertyMapping, string>) (r => r.LdapField));
      if (!query.HasCount || !strings1.Any<string>() || !strings2.Any<string>())
        return strings2.Union<string>(strings1.Except<string>(strings2)).ToArray<string>();
      string str1 = strings1.First<string>();
      string str2 = strings2.First<string>();
      return str1 == str2 ? new string[1]{ str1 } : new string[2]
      {
        str1,
        str2
      };
    }

    /// <summary>Toes the sort keys.</summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    protected virtual SortKey[] ToSortKeys(IEnumerable<SortOptions> items)
    {
      List<SortKey> sortKeyList = new List<SortKey>();
      foreach (SortOptions sortOptions in items)
        sortKeyList.Add(new SortKey()
        {
          AttributeName = sortOptions.Column,
          ReverseOrder = !sortOptions.Ascending
        });
      return sortKeyList.ToArray();
    }

    /// <summary>Execute LDAP query against LDAP server</summary>
    /// <param name="query">LDAP query</param>
    /// <param name="rootDN">root distinguished name</param>
    /// <param name="pageSize">entry's count per request</param>
    /// <returns>collection of search results</returns>
    protected virtual IEnumerable<SearchResultEntry> Search(
      LdapQuery query,
      string rootDN,
      int pageSize)
    {
      if (this.GetConnection() == null)
        return (IEnumerable<SearchResultEntry>) new List<SearchResultEntry>();
      IEnumerable<SearchResultEntry> resultFromCache = this.GetResultFromCache(query);
      if (resultFromCache != null)
        return resultFromCache;
      int num = 0;
      List<SearchResultEntry> result = new List<SearchResultEntry>();
      SearchRequest request = this.PrepareSearchRequest(query, rootDN, pageSize);
      PageResultRequestControl resultRequestControl = (PageResultRequestControl) null;
      foreach (DirectoryControl control in (CollectionBase) request.Controls)
      {
        if (control.GetType() == typeof (PageResultRequestControl))
        {
          resultRequestControl = control as PageResultRequestControl;
          break;
        }
      }
      while (true)
      {
        SearchResponse searchResponse;
        try
        {
          searchResponse = (SearchResponse) this.GetConnection().SendRequest((DirectoryRequest) request);
        }
        catch (ObjectDisposedException ex)
        {
          this.ClearConnection();
          searchResponse = (SearchResponse) this.GetConnection().SendRequest((DirectoryRequest) request);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders);
          return (IEnumerable<SearchResultEntry>) result;
        }
        if (((IEnumerable<DirectoryControl>) searchResponse.Controls).Where<DirectoryControl>((Func<DirectoryControl, bool>) (cs => cs.GetType() == typeof (PageResultResponseControl))).FirstOrDefault<DirectoryControl>() is PageResultResponseControl resultResponseControl)
        {
          if (num < query.Skip && searchResponse.Entries.Count + num < query.Skip)
          {
            num += searchResponse.Entries.Count;
          }
          else
          {
            int index;
            for (index = 0; index < searchResponse.Entries.Count && num < query.Skip; ++index)
              ++num;
            for (; index < searchResponse.Entries.Count && (result.Count < query.Take || query.Take == 0); ++index)
            {
              ++num;
              result.Add(searchResponse.Entries[index]);
            }
          }
          if (resultResponseControl.Cookie.Length != 0 && result.Count != query.Take)
            resultRequestControl.Cookie = resultResponseControl.Cookie;
          else
            goto label_26;
        }
        else
          break;
      }
      throw new Exception(string.Format("LDAP provider does not support paging for this request: '{0}'", (object) query.Filter));
label_26:
      this.AddResultToCache(query, (IEnumerable<SearchResultEntry>) result);
      return (IEnumerable<SearchResultEntry>) result;
    }

    /// <summary>
    /// User search  against active directory with specific filter
    /// </summary>
    /// <param name="filter"> user filter</param>
    /// <param name="attributes">attributes returned from searched entries</param>
    /// <returns>collection fo search result entries</returns>
    protected virtual IEnumerable<SearchResultEntry> UserSearch(
      LdapFilterEntry filter,
      params string[] attributes)
    {
      if (attributes == null)
        attributes = new string[0];
      LdapQuery query = new LdapQuery()
      {
        Filter = new StringBuilder((filter & new LdapFilterEntry(this.ConnectionSettings.UserFilter)).ToString())
      };
      foreach (string attribute in attributes)
        query.SelectColumns.Add(attribute);
      try
      {
        return this.Search(query, this.ConnectionSettings.UserDns, this.ConnectionSettings.MaxReturnedUsers);
      }
      catch (ObjectDisposedException ex)
      {
        this.ClearConnection();
        return this.Search(query, this.ConnectionSettings.UserDns, this.ConnectionSettings.MaxReturnedUsers);
      }
    }

    /// <summary>
    /// User search  against active directory with specific filter
    /// </summary>
    /// <param name="query"> ldap query</param>
    /// <returns>collection fo search result entries</returns>
    public virtual IEnumerable<SearchResultEntry> UserSearch(
      LdapQuery query)
    {
      query.Filter = new StringBuilder((query.Filter.ToString() & new LdapFilterEntry(this.ConnectionSettings.UserFilter)).ToString());
      try
      {
        return this.Search(query, this.ConnectionSettings.UserDns, this.ConnectionSettings.MaxReturnedUsers);
      }
      catch (ObjectDisposedException ex)
      {
        this.ClearConnection();
        return this.Search(query, this.ConnectionSettings.UserDns, this.ConnectionSettings.MaxReturnedUsers);
      }
    }

    /// <summary>Role search against active directory</summary>
    /// <param name="query">Ldap query</param>
    /// <returns>collection of search result</returns>
    public virtual IEnumerable<SearchResultEntry> RoleSearch(
      LdapQuery query)
    {
      query.Filter = new StringBuilder((new LdapFilterEntry(query.Filter.ToString()) & new LdapFilterEntry(this.ConnectionSettings.RolesFilter)).ToString());
      try
      {
        return this.Search(query, this.ConnectionSettings.RolesDNs, this.ConnectionSettings.MaxReturnedRoles);
      }
      catch (ObjectDisposedException ex)
      {
        this.ClearConnection();
        return this.Search(query, this.ConnectionSettings.RolesDNs, this.ConnectionSettings.MaxReturnedRoles);
      }
    }

    /// <summary>
    /// Role search  against active directory with specific filter
    /// </summary>
    /// <param name="filter"> role filter</param>
    /// <param name="attributes">attributes returned from searched entries</param>
    /// <returns>collection fo search result entries</returns>
    protected virtual IEnumerable<SearchResultEntry> RolesSearch(
      LdapFilterEntry filter,
      params string[] attributes)
    {
      if (attributes == null)
        attributes = new string[0];
      LdapQuery query = new LdapQuery()
      {
        Filter = new StringBuilder((filter & new LdapFilterEntry(this.ConnectionSettings.RolesFilter)).ToString())
      };
      foreach (string attribute in attributes)
        query.SelectColumns.Add(attribute);
      try
      {
        return this.Search(query, this.ConnectionSettings.RolesDNs, this.ConnectionSettings.MaxReturnedRoles);
      }
      catch (ObjectDisposedException ex)
      {
        this.ClearConnection();
        return this.Search(query, this.ConnectionSettings.RolesDNs, this.ConnectionSettings.MaxReturnedRoles);
      }
    }

    /// <summary>Close the connection to Ldap</summary>
    public virtual void Dispose()
    {
      if (this.connection == null)
        return;
      this.connection = (LdapConnection) null;
    }

    /// <summary>Returns the first find result</summary>
    /// <param name="userID">User ID</param>
    /// <param name="attributes"> selected  attributes</param>
    /// <returns></returns>
    public virtual SearchResultEntry GetUserById(
      Guid userID,
      params string[] attributes)
    {
      IEnumerable<SearchResultEntry> source = this.UserSearch(this.FilterGetUserByID(userID), attributes);
      return source.Count<SearchResultEntry>() == 0 ? (SearchResultEntry) null : source.First<SearchResultEntry>();
    }

    /// <summary>Search Against active directory</summary>
    /// <param name="roleDN">Role Distinguished name</param>
    /// <returns>collection ot user search result entries</returns>
    public virtual IEnumerable<SearchResultEntry> GetUsersByRoleDN(
      string roleDN)
    {
      return this.UserSearch(new LdapFilterEntry(LdapAttributeNames.memberOf, roleDN));
    }

    /// <summary>Search Against active directory</summary>
    /// <param name="username">user username</param>
    /// <param name="attributes"> selected  attributes</param>
    /// <returns>user search result entry</returns>
    public virtual SearchResultEntry GetUserByUsername(
      string username,
      params string[] attributes)
    {
      IEnumerable<SearchResultEntry> source = this.UserSearch(this.FilterGetUserByUsername(username), attributes);
      int num = source.Count<SearchResultEntry>();
      if (num == 0)
        return (SearchResultEntry) null;
      if (num > 1)
        throw new LdapProviderException("More than one user found with name:" + username);
      return source.FirstOrDefault<SearchResultEntry>();
    }

    /// <summary>Search Against active directory</summary>
    /// <param name="email">user email</param>
    /// <param name="attributes"> selected attributes</param>
    /// <returns>user search result entry</returns>
    public virtual SearchResultEntry GetUserByEmail(
      string email,
      params string[] attributes)
    {
      IEnumerable<SearchResultEntry> source = this.UserSearch(this.FilterGetUserByEmail(email), attributes);
      return source.Count<SearchResultEntry>() > 0 ? source.First<SearchResultEntry>() : (SearchResultEntry) null;
    }

    /// <summary>Search Against active directory</summary>
    /// <param name="attributes"> selected  attributes</param>
    /// <returns>collection of user search result entries</returns>
    public virtual IEnumerable<SearchResultEntry> GetUsers(
      params string[] attributes)
    {
      return this.UserSearch(this.FilterGetUsers(), attributes);
    }

    /// <summary>Authenticate User against LDAP</summary>
    /// <param name="userName">Username</param>
    /// <param name="password">User password</param>
    /// <param name="userId">user guid</param>
    /// <returns>check if supplied arguments are valid username and password</returns>
    public virtual bool AuthenticateUser(string userName, string password, out Guid userId)
    {
      userId = Guid.Empty;
      try
      {
        using (LdapConnection connection = this.GetConnection(userName, password))
        {
          string domain = (string) null;
          string str = this.ConnectionSettings.ConnectionDomain.Trim();
          if (!userName.EndsWith('@'.ToString() + str))
            domain = str;
          NetworkCredential newCredential = new NetworkCredential(userName, password, domain);
          connection.Bind(newCredential);
          SearchResultEntry userByUsername = this.GetUserByUsername(userName, new string[1]
          {
            LdapAttributeNames.ObjectGuidAttribute
          });
          userId = new Guid(LdapUtilities.GetAttributeValue<byte[]>(userByUsername, LdapAttributeNames.ObjectGuidAttribute));
          this.ClearConnection();
        }
      }
      catch (LdapException ex)
      {
        if (ex.ErrorCode == 81)
          Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions);
        return false;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        return false;
      }
      return true;
    }

    /// <summary>Filter that gets user by email.</summary>
    /// <param name="email">The email.</param>
    /// <returns></returns>
    protected virtual LdapFilterEntry FilterGetUserByEmail(string email) => new LdapFilterEntry(LdapAttributeNames.emailAttribute, email);

    /// <summary>User filter by user pageId</summary>
    /// <param name="userID">user guid </param>
    /// <returns>filter for users by user pageId</returns>
    protected virtual LdapFilterEntry FilterGetUserByID(Guid userID) => new LdapFilterEntry(LdapAttributeNames.ObjectGuidAttribute, userID.ToLdapFormat());

    /// <summary>User filter by user Username</summary>
    /// <param name="username">username</param>
    /// <returns>filter for users by username</returns>
    protected virtual LdapFilterEntry FilterGetUserByUsername(string username) => new LdapFilterEntry(LdapAttributeNames.UserNameAttribute, username);

    /// <summary>User filter</summary>
    /// <returns>user filter </returns>
    protected virtual LdapFilterEntry FilterGetUsers() => new LdapFilterEntry(this.ConnectionSettings.UserFilter);

    /// <summary>Gets all roles.</summary>
    /// <returns></returns>
    public virtual IEnumerable<SearchResultEntry> GetAllRoles() => this.RolesSearch(LdapFacade.FilterGetRoles());

    /// <summary>Gets the users from role entry.</summary>
    /// <param name="roleEntry">The role entry.</param>
    /// <returns></returns>
    public virtual IEnumerable<SearchResultEntry> GetUsersFromRoleEntry(
      SearchResultEntry roleEntry)
    {
      return this.UserSearch(this.FilterGetUsers() & new LdapFilterEntry(LdapAttributeNames.memberOf, roleEntry.DistinguishedName), LdapAttributeNames.ObjectGuidAttribute);
    }

    /// <summary>Determines whether user is in role.</summary>
    /// <param name="roleEntry">The role entry.</param>
    /// <param name="userId">The user pageId.</param>
    /// <returns>
    /// 	<c>true</c> if [is user in role] [the specified role entry]; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsUserInRole(SearchResultEntry roleEntry, Guid userId) => this.UserSearch(this.FilterGetUserByID(userId) & new LdapFilterEntry(LdapAttributeNames.memberOf, roleEntry.DistinguishedName)).Any<SearchResultEntry>();

    /// <summary>returns role by pageId</summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="T:Telerik.Sitefinity.Security.Ldap.LdapProviderException"></exception>
    public virtual SearchResultEntry GetRole(Guid roleId)
    {
      LdapFilterEntry ldapFilterEntry = new LdapFilterEntry(LdapAttributeNames.RoleIdAttribute, roleId.ToLdapFormat());
      IEnumerable<SearchResultEntry> source = this.RolesSearch(LdapFacade.FilterGetRoles() & ldapFilterEntry);
      return source.Count<SearchResultEntry>() <= 1 ? source.FirstOrDefault<SearchResultEntry>() : throw new LdapProviderException("More than one role found for ID:" + (object) roleId);
    }

    /// <summary>Gets the role by its name</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns></returns>
    public virtual SearchResultEntry GetRole(string roleName)
    {
      IEnumerable<SearchResultEntry> source = this.RolesSearch(new LdapFilterEntry(LdapAttributeNames.RoleNameAttribute, roleName));
      return source.Count<SearchResultEntry>() <= 1 ? source.FirstOrDefault<SearchResultEntry>() : throw new LdapProviderException("More than one role found for role name:" + roleName);
    }

    /// <summary>Get roles by roles distinguished names</summary>
    /// <param name="roleDNs">collection of distinguished names</param>
    /// <returns>collection of roles search result entries</returns>
    public virtual IEnumerable<SearchResultEntry> GetRolesFromRoleDns(
      IEnumerable<string> roleDNs)
    {
      LdapFilterEntry filter = new LdapFilterEntry();
      foreach (string roleDn in roleDNs)
        filter |= new LdapFilterEntry(LdapAttributeNames.distinguishedNameAttribute, roleDn);
      return string.IsNullOrEmpty(filter.ToString()) ? (IEnumerable<SearchResultEntry>) new List<SearchResultEntry>() : this.RolesSearch(filter);
    }

    /// <summary>Roles filter</summary>
    /// <returns>roles filter</returns>
    public static LdapFilterEntry FilterGetRoles() => new LdapFilterEntry(LdapAttributeNames.objectClassAttribute, LdapAttributeNames.objectGroupAttribute);
  }
}
