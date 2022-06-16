// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapCredentialsCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Net;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  /// Used to cache credentials for connecting to an LDAP directory when Forms authentication is used.
  /// By using the credentials of the logged in user, we can avoid hardcoding a user and password in the configuration settings for LDAP
  /// </summary>
  public class LdapCredentialsCache
  {
    private static Dictionary<Guid, NetworkCredential> userCache = new Dictionary<Guid, NetworkCredential>();

    private LdapCredentialsCache()
    {
    }

    /// <summary>Adds the credential.</summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    public static void AddCredential(Guid userId, string userName, string password)
    {
      NetworkCredential credential = new NetworkCredential(userName, password);
      if (!SystemManager.HttpContextItems.Contains((object) "ldapcredentials"))
        SystemManager.HttpContextItems.Add((object) "ldapcredentials", (object) credential);
      else
        SystemManager.HttpContextItems[(object) "ldapcredentials"] = (object) credential;
      LdapCredentialsCache.AddCredential(userId, credential);
    }

    /// <summary>Adds the credential.</summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="credential">The credential.</param>
    public static void AddCredential(Guid userId, NetworkCredential credential)
    {
      lock (LdapCredentialsCache.userCache)
      {
        if (!LdapCredentialsCache.userCache.ContainsKey(userId))
          LdapCredentialsCache.userCache.Add(userId, credential);
        else
          LdapCredentialsCache.userCache[userId] = credential;
      }
    }

    /// <summary>Gets the credential.</summary>
    /// <param name="userId">The user ID.</param>
    /// <returns></returns>
    public static NetworkCredential GetCredential(Guid userId)
    {
      if (userId == Guid.Empty && SystemManager.HttpContextItems[(object) "ldapcredentials"] != null)
        return SystemManager.HttpContextItems[(object) "ldapcredentials"] as NetworkCredential;
      NetworkCredential credential = (NetworkCredential) null;
      LdapCredentialsCache.userCache.TryGetValue(userId, out credential);
      return credential;
    }
  }
}
