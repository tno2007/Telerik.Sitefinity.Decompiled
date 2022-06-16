// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SitefinityUserDisplayNameBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security
{
  /// <summary>A class used for getting display name for users.</summary>
  public class SitefinityUserDisplayNameBuilder : IUserDisplayNameBuilder
  {
    /// <inheritdoc />
    public string GetUserDisplayName(Guid userId)
    {
      if (userId == Guid.Empty)
        return string.Empty;
      return ((IEnumerable<string>) SecurityManager.SystemAccountIDs).Contains<string>(userId.ToString().ToUpperInvariant()) ? "System" : this.BuildDisplayName((ICacheUserProfile) UserManager.GetCachedUserProfile(userId));
    }

    /// <inheritdoc />
    public string GetAvatarImageUrl(Guid userId, out Telerik.Sitefinity.Libraries.Model.Image image)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      ContentLink contentLink;
      if (cachedUserProfile != null && cachedUserProfile.TryGetValue<ContentLink>("Avatar", out contentLink))
      {
        image = contentLink.GetLinkedItem() as Telerik.Sitefinity.Libraries.Model.Image;
        if (image != null)
          return image.MediaUrl;
      }
      image = (Telerik.Sitefinity.Libraries.Model.Image) null;
      return RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted);
    }

    /// <inheritdoc />
    public bool IsExternalUser(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null && cachedUserProfile.IsExternalUser;
    }

    /// <summary>
    /// Builds the display name from a <see cref="T:Telerik.Sitefinity.Security.ICacheUserProfile" /> .
    /// </summary>
    protected virtual string BuildDisplayName(ICacheUserProfile userProfile)
    {
      if (userProfile == null)
        return Res.Get<Labels>().UserNotFound;
      string str = userProfile.FirstName + " " + userProfile.LastName;
      return string.IsNullOrWhiteSpace(str) ? Res.Get<Labels>().Unknown : str;
    }

    /// <inheritdoc />
    public string GetUserNickname(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null ? cachedUserProfile.Nickname : string.Empty;
    }

    /// <inheritdoc />
    public string GetAboutInfo(Guid userId)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
      return cachedUserProfile != null ? cachedUserProfile.About : string.Empty;
    }
  }
}
