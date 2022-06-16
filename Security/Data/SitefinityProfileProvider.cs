// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.SitefinityProfileProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Profile;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// A wrapper class, working with Sitefinity's UserProfileManager (which instantiates the configured Sitefinity profile provider).
  /// This class is a standard profile provider (inherits from System.Web.Profile.ProfileProvider), to be used directly in Web.Config,
  /// plugging it into Sitefinity's profiles infrastructure.
  /// </summary>
  public class SitefinityProfileProvider : ProfileProvider
  {
    private UserProfileManager _profileManager;

    /// <summary>
    /// Deletes all user-profile data for profiles in which the last activity date occurred before the specified date
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are deleted. This argument is ignored</param>
    /// <param name="userInactiveSinceDate">A DateTime that identifies which user profiles are considered inactive. If the LastActivityDate value of a user profile occurs on or before this date and time, the profile is considered inactive</param>
    /// <returns>The number of profiles deleted from the data source</returns>
    public override int DeleteInactiveProfiles(
      ProfileAuthenticationOption authenticationOption,
      DateTime userInactiveSinceDate)
    {
      return 0;
    }

    /// <summary>
    /// Deletes profile properties and information for profiles that match the supplied list of user names
    /// </summary>
    /// <param name="usernames">A string array of user names for profiles to be deleted</param>
    /// <returns>The number of profiles deleted from the data source</returns>
    public override int DeleteProfiles(string[] usernames) => 0;

    /// <summary>
    /// Deletes profile properties and information for the supplied list of profiles
    /// </summary>
    /// <param name="profiles">A ProfileInfoCollection of information about profiles that are to be deleted</param>
    /// <returns>The number of profiles deleted from the data source</returns>
    public override int DeleteProfiles(ProfileInfoCollection profiles) => 0;

    /// <summary>
    /// Retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are returned. This argument is ignored</param>
    /// <param name="usernameToMatch">The user name to search for</param>
    /// <param name="userInactiveSinceDate">A DateTime that identifies which user profiles are considered inactive. If the LastActivityDate value of a user profile occurs on or before this date and time, the profile is considered inactive</param>
    /// <param name="pageIndex">The index of the page of results to return</param>
    /// <param name="pageSize">The size of the page of results to return</param>
    /// <param name="totalRecords">When this method returns, contains the total number of profiles</param>
    /// <returns>A ProfileInfoCollection containing user profile information for inactive profiles where the user name matches the supplied usernameToMatch parameter</returns>
    public override ProfileInfoCollection FindInactiveProfilesByUserName(
      ProfileAuthenticationOption authenticationOption,
      string usernameToMatch,
      DateTime userInactiveSinceDate,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      ProfileInfoCollection profilesByUserName = new ProfileInfoCollection();
      totalRecords = 0;
      return profilesByUserName;
    }

    /// <summary>
    ///  Retrieves profile information for profiles in which the user name matches the specified user names
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are returned. This argument is ignored</param>
    /// <param name="usernameToMatch">The user name to search for</param>
    /// <param name="pageIndex">The index of the page of results to return</param>
    /// <param name="pageSize">The size of the page of results to return</param>
    /// <param name="totalRecords">When this method returns, contains the total number of profiles</param>
    /// <returns>A ProfileInfoCollection containing user-profile information for profiles where the user name matches the supplied usernameToMatch parameter</returns>
    public override ProfileInfoCollection FindProfilesByUserName(
      ProfileAuthenticationOption authenticationOption,
      string usernameToMatch,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      ProfileInfoCollection profilesByUserName = new ProfileInfoCollection();
      totalRecords = 0;
      return profilesByUserName;
    }

    /// <summary>
    /// Retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are returned. This argument is ignored</param>
    /// <param name="userInactiveSinceDate">A DateTime that identifies which user profiles are considered inactive. If the LastActivityDate of a user profile occurs on or before this date and time, the profile is considered inactive</param>
    /// <param name="pageIndex">The index of the page of results to return</param>
    /// <param name="pageSize">The size of the page of results to return</param>
    /// <param name="totalRecords">When this method returns, contains the total number of profiles</param>
    /// <returns>A ProfileInfoCollection containing user-profile information about the inactive profiles</returns>
    public override ProfileInfoCollection GetAllInactiveProfiles(
      ProfileAuthenticationOption authenticationOption,
      DateTime userInactiveSinceDate,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      ProfileInfoCollection inactiveProfiles = new ProfileInfoCollection();
      totalRecords = 0;
      return inactiveProfiles;
    }

    /// <summary>
    /// Retrieves user profile data for all profiles in the data source
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are returned. This argument is ignored</param>
    /// <param name="pageIndex">The index of the page of results to return</param>
    /// <param name="pageSize">The size of the page of results to return</param>
    /// <param name="totalRecords">When this method returns, contains the total number of profiles</param>
    /// <returns>A ProfileInfoCollection containing user-profile information for all profiles in the data source</returns>
    public override ProfileInfoCollection GetAllProfiles(
      ProfileAuthenticationOption authenticationOption,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      ProfileInfoCollection allProfiles = new ProfileInfoCollection();
      int count = pageIndex * pageSize;
      IQueryable<UserProfile> source = this.profileManager.GetUserProfiles().Skip<UserProfile>(count).Take<UserProfile>(pageSize);
      totalRecords = source.Count<UserProfile>();
      foreach (UserProfile userProfile in (IEnumerable<UserProfile>) source)
        allProfiles.Add(new ProfileInfo(userProfile.User.UserName, false, userProfile.User.LastActivityDate, userProfile.LastModified, 0));
      return allProfiles;
    }

    /// <summary>
    /// Returns the number of profiles in which the last activity date occurred on or before the specified date
    /// </summary>
    /// <param name="authenticationOption">One of the ProfileAuthenticationOption values, specifying whether anonymous, authenticated, or both types of profiles are returned. This argument is ignored</param>
    /// <param name="userInactiveSinceDate">A DateTime that identifies which user profiles are considered inactive. If the LastActivityDate of a user profile occurs on or before this date and time, the profile is considered inactive</param>
    /// <returns>The number of profiles in which the last activity date occurred on or before the specified date</returns>
    public override int GetNumberOfInactiveProfiles(
      ProfileAuthenticationOption authenticationOption,
      DateTime userInactiveSinceDate)
    {
      return this.profileManager.GetUserProfiles().Where<UserProfile>((Expression<Func<UserProfile, bool>>) (p => p.LastModified < userInactiveSinceDate)).Count<UserProfile>();
    }

    /// <summary>
    /// Returns the collection of settings property values for the specified application instance and settings property group
    /// </summary>
    /// <param name="context">A SettingsContext describing the current application use</param>
    /// <param name="collection">A SettingsPropertyCollection containing the settings property group whose values are to be retrieved</param>
    /// <returns>A SettingsPropertyValueCollection containing the values for the specified settings property group</returns>
    public override SettingsPropertyValueCollection GetPropertyValues(
      SettingsContext context,
      SettingsPropertyCollection collection)
    {
      return new SettingsPropertyValueCollection();
    }

    /// <summary>
    /// Sets the values of the specified group of property settings
    /// </summary>
    /// <param name="context">A SettingsContext describing the current application usage</param>
    /// <param name="collection">A SettingsPropertyValueCollection representing the group of property settings to set</param>
    public override void SetPropertyValues(
      SettingsContext context,
      SettingsPropertyValueCollection collection)
    {
    }

    /// <summary>
    /// Gets or sets the name of the currently running application
    /// </summary>
    public override string ApplicationName
    {
      get => this.profileManager.Provider.ApplicationName;
      set => throw new NotImplementedException();
    }

    /// <summary>Profile a manager object for the set default provider</summary>
    internal UserProfileManager profileManager
    {
      get
      {
        if (this._profileManager == null)
          this._profileManager = UserProfileManager.GetManager();
        return this._profileManager;
      }
      set => this._profileManager = value;
    }
  }
}
