// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserDisplayNameBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security
{
  /// <summary>A class used for getting display name for users.</summary>
  public class UserDisplayNameBuilder : IUserDisplayNameBuilder
  {
    public const string UserDisplayNameCacheKey = "UserDisplayNameCache";
    private static object syncLock = new object();

    /// <summary>Returns the display name for the specified user</summary>
    /// <param name="userId">user id</param>
    /// <returns></returns>
    public virtual string GetUserDisplayName(Guid userId)
    {
      if (userId == Guid.Empty)
        return string.Empty;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      dictionary = currentHttpContext.Items[(object) "UserDisplayNameCache"] as Dictionary<Guid, string>;
      string userDisplayName = string.Empty;
      if (dictionary != null && dictionary.TryGetValue(userId, out userDisplayName))
        return userDisplayName;
      if (dictionary == null)
      {
        lock (UserDisplayNameBuilder.syncLock)
        {
          if (!(currentHttpContext.Items[(object) "UserDisplayNameCache"] is Dictionary<Guid, string> dictionary))
          {
            dictionary = new Dictionary<Guid, string>();
            currentHttpContext.Items[(object) "UserDisplayNameCache"] = (object) dictionary;
          }
        }
      }
      string profileTypeName = UserProfilesHelper.GetProfileTypeName(typeof (SitefinityProfile));
      SitefinityProfile userProfile = UserProfileManager.GetManager().GetUserProfile(userId, profileTypeName) as SitefinityProfile;
      bool flag = false;
      if (userProfile != null)
      {
        string firstName = userProfile.FirstName;
        string lastName = userProfile.LastName;
        if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
        {
          userDisplayName = firstName + " " + lastName;
          flag = true;
        }
      }
      if (!flag)
      {
        User user = UserManager.FindUser(userId);
        if (user != null)
        {
          userDisplayName = user.FirstName + " " + user.LastName;
          flag = true;
        }
      }
      if (!flag)
        userDisplayName = !((IEnumerable<string>) SecurityManager.SystemAccountIDs).Contains<string>(userId.ToString().ToUpperInvariant()) ? Res.Get<Labels>().UserNotFound : "System";
      if (!dictionary.ContainsKey(userId))
      {
        lock (dictionary)
        {
          if (!dictionary.ContainsKey(userId))
            dictionary.Add(userId, userDisplayName);
        }
      }
      return userDisplayName;
    }

    /// <summary>Returns the url for the avatar of the user</summary>
    /// <param name="userId">user id</param>
    /// <param name="image"></param>
    /// <returns></returns>
    public virtual string GetAvatarImageUrl(Guid userId, out Telerik.Sitefinity.Libraries.Model.Image image)
    {
      if (UserProfilesHelper.GetUserProfileManager<SitefinityProfile>().GetUserProfile(userId, typeof (SitefinityProfile).FullName) is SitefinityProfile userProfile && userProfile.Avatar != null)
      {
        image = userProfile.Avatar.GetLinkedItem() as Telerik.Sitefinity.Libraries.Model.Image;
        if (image != null)
          return image.MediaUrl;
      }
      image = (Telerik.Sitefinity.Libraries.Model.Image) null;
      return RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted);
    }

    /// <inheritdoc />
    public virtual bool IsExternalUser(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null && cachedUserProfile.IsExternalUser;
    }

    /// <summary>
    /// Display names are cached during a single request
    /// Here we try to load it from the context if cached
    /// </summary>
    /// <param name="displayName"></param>
    /// <returns></returns>
    protected bool TryGetDisplayNameFromCache(Guid id, out string displayName)
    {
      displayName = (string) null;
      if (!(SystemManager.CurrentHttpContext.Items[(object) "UserDisplayNameCache"] is Dictionary<Guid, string>))
      {
        Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
      }
      return false;
    }

    public string GetUserNickname(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null ? cachedUserProfile.Nickname : string.Empty;
    }

    public string GetAboutInfo(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null ? cachedUserProfile.About : string.Empty;
    }
  }
}
