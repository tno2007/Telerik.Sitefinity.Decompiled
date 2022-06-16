// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IUserDisplayNameBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  public interface IUserDisplayNameBuilder
  {
    /// <summary>Returns the nickname for the specified user</summary>
    /// <param name="userId">user id</param>
    string GetUserNickname(Guid userId);

    /// <summary>Returns the about info for the specified user</summary>
    /// <param name="userId">user id</param>
    string GetAboutInfo(Guid userId);

    /// <summary>Returns the display name for the specified user</summary>
    /// <param name="userId">user id</param>
    string GetUserDisplayName(Guid userId);

    /// <summary>Returns the url for the avatar of the user</summary>
    /// <param name="userId">user id</param>
    /// <param name="image"></param>
    /// <returns></returns>
    string GetAvatarImageUrl(Guid userId, out Telerik.Sitefinity.Libraries.Model.Image image);

    /// <summary>
    /// Return if the user is logged with external provider or not
    /// </summary>
    /// <param name="userId">user id</param>
    /// <returns>true - External User, false - Local User</returns>
    bool IsExternalUser(Guid userId);
  }
}
