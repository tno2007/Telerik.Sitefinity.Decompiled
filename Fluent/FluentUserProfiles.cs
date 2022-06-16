// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.FluentUserProfiles
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Fluent.UserProfiles;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Aggregation class for all facades related to user profiles.
  /// </summary>
  public class FluentUserProfiles
  {
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentUserProfiles" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    public FluentUserProfiles(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Provides the methods for working with a single user profile. Use this method when you want to work with an existing user profile.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfileFacade" /> that provides fluent API for working with a single user profile.
    /// </returns>
    public UserProfileFacade UserProfile() => new UserProfileFacade(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single user profile. Use this method when you want to work with an existing user profile.
    /// </summary>
    /// <param name="userProfileId">The user profile id.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfileFacade" /> that provides fluent API for working with a single user profile.
    /// </returns>
    public UserProfileFacade UserProfile(Guid userProfileId) => new UserProfileFacade(this.appSettings, userProfileId);

    /// <summary>
    /// Provides the methods for working with a single user profile. Use this method when you want to work with an existing user profile.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeName">FullName of the profile type to obtain.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfileFacade" /> that provides fluent API for working with a single user profile.
    /// </returns>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public UserProfileFacade UserProfile(User user, string profileTypeName) => new UserProfileFacade(this.appSettings, user, profileTypeName);

    /// <summary>
    /// Provides the methods for working with a collection of user's profiles.
    /// </summary>
    /// <param name="user">The user to which the profiles belong to.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade" /> that provides fluent API for working with multiple user profiles.
    /// </returns>
    public UserProfilesFacade UserProfiles(User user) => new UserProfilesFacade(this.appSettings, user);
  }
}
