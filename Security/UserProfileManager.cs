// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserProfileManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Manager class for profiles.</summary>
  public class UserProfileManager : 
    ManagerBase<UserProfileDataProvider>,
    IContentManager,
    IManager,
    IDisposable,
    IProviderResolver
  {
    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.Security.UserProfileManager" /> class.
    /// </summary>
    static UserProfileManager()
    {
      ManagerBase<UserProfileDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(UserProfileManager.Provider_Executing);
      ManagerBase<UserProfileDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(UserProfileManager.Provider_Executed);
      if (ManagerBase<UserProfileDataProvider>.StaticProvidersCollection != null)
        return;
      UserProfileManager userProfileManager = new UserProfileManager();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserProfileManager" /> class with the default provider.
    /// </summary>
    public UserProfileManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserProfileManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public UserProfileManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.UserProfileManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public UserProfileManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the default data provider</summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() =>
    {
      string empty = string.Empty;
      ConfigElementDictionary<string, DataProviderSettings> providers = Config.Get<UserProfilesConfig>().Providers;
      if (providers.Keys.Count > 0)
        empty = providers.Keys.ElementAt<string>(0).ToString();
      return empty;
    });

    /// <summary>Gets the profile module name</summary>
    public override string ModuleName => "Security";

    /// <summary>Gets the configured provider settings</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<UserProfilesConfig>().Providers;

    /// <summary>Gets an instance for UserProfileManager.</summary>
    /// <returns>An instance of UserProfileManager.</returns>
    public static UserProfileManager GetManager() => UserProfileManager.GetManager(string.Empty);

    /// <summary>
    /// Retrieves the instance of the profile manager, by provider name
    /// </summary>
    /// <param name="providerName">The name of the provider</param>
    /// <returns>Instance of the profile manager</returns>
    public static UserProfileManager GetManager(string providerName) => ManagerBase<UserProfileDataProvider>.GetManager<UserProfileManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static UserProfileManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<UserProfileDataProvider>.GetManager<UserProfileManager>(providerName, transactionName);
    }

    /// <summary>Get url from a locatable item</summary>
    /// <param name="item">Locatable item</param>
    /// <returns>Url of the locatable item</returns>
    public string GetItemUrl(ILocatable item) => this.Provider.GetItemUrl(item);

    /// <summary>Retrieve a content item by its url, ignoring status</summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public IDataItem GetItemFromUrl(Type itemType, string url, out string redirectUrl) => this.Provider.GetItemFromUrl(itemType, url, out redirectUrl);

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">Since user profiles do not support lifecycle this parameters value will be overriden to false.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.Provider.GetItemFromUrl(itemType, url, false, out redirectUrl);
    }

    /// <summary>Gets the items.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <returns></returns>
    public IQueryable<TItem> GetItems<TItem>() where TItem : IContent
    {
      if (typeof (UserProfile).IsAssignableFrom(typeof (TItem)))
        return this.GetUserProfiles() as IQueryable<TItem>;
      throw new ArgumentException("Unable to cast TItem to UserProfile");
    }

    /// <summary>Recompiles the URLs of the item.</summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item">The content item.</param>
    public void RecompileItemUrls<TItem>(TItem item) where TItem : ILocatable => this.Provider.RecompileItemUrls<TItem>(item);

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> item to the current URLs collection for this item.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <param name="url">The URL string value that should be added.</param>
    /// <param name="isDefault"></param>
    /// <param name="redirectToDefault"></param>
    public void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable => this.Provider.AddItemUrl<T>(item, url, isDefault, redirectToDefault);

    /// <summary>
    /// Removes all urls from the item satisfying the condition that is checked in the predicate function.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    public void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable => this.Provider.RemoveItemUrls<TItem>(item, predicate);

    /// <summary>Clears the Urls collection for this item.</summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
    public void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.Provider.ClearItemUrls<TItem>(item, excludeDefault);

    /// <summary>Recompiles the and validate urls.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="content">The content.</param>
    public virtual void RecompileAndValidateUrls<TLocatable>(TLocatable content) where TLocatable : ILocatable
    {
      this.Provider.RecompileItemUrls<TLocatable>(content);
      this.ValidateUrlConstraints<TLocatable>(content);
    }

    /// <summary>Validates the URL constraints.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="item">The item.</param>
    public virtual void ValidateUrlConstraints<TLocatable>(TLocatable item) where TLocatable : ILocatable
    {
      Guid id = item.Id;
      List<UrlData> list = item.Urls.ToList<UrlData>();
      if (list.Count <= 0)
        return;
      Type urlTypeFor = this.Provider.GetUrlTypeFor(item.GetType());
      foreach (UrlData urlData in list)
      {
        string url = urlData.Url;
        if (this.Provider.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url && u.Parent.Id != id)).FirstOrDefault<UrlData>() != null)
        {
          if (item.AutoGenerateUniqueUrl)
          {
            item.UrlName = (Lstring) ((string) item.UrlName + SecurityManager.GetRandomKey(6));
            this.Provider.RecompileItemUrls<TLocatable>(item);
          }
          else
          {
            this.CancelChanges();
            this.ThrowDuplicateUrlException(url);
          }
        }
      }
    }

    /// <summary>Creates new profile.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfile CreateProfile(User user) => this.Provider.CreateProfile(user);

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfile CreateProfile(User user, string profileTypeName) => this.Provider.CreateProfile(user, profileTypeName);

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfile CreateProfile(User user, Guid profileId) => this.Provider.CreateProfile(user, profileId);

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfile CreateProfile(
      User user,
      Guid profileId,
      Type profileType)
    {
      return this.Provider.CreateProfile(user, profileId, profileType);
    }

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile identity.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfile CreateProfile(
      User user,
      Guid profileId,
      string profileTypeName)
    {
      return this.Provider.CreateProfile(user, profileId, profileTypeName);
    }

    /// <summary>Gets all profiles related to the specified user.</summary>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    public virtual IQueryable<UserProfile> GetUserProfiles(User user) => this.Provider.GetUserProfiles(user);

    /// <summary>
    /// Gets all profiles related to the specified user by id.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public virtual IQueryable<UserProfile> GetUserProfiles(Guid userId) => this.Provider.GetUserProfiles(userId);

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public virtual IQueryable<UserProfile> GetUserProfiles() => this.Provider.GetUserProfiles();

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public virtual IQueryable<T> GetUserProfiles<T>() where T : UserProfile => this.Provider.GetUserProfiles<T>();

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Type of the profile.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual TUserProfileType GetUserProfile<TUserProfileType>(User user) where TUserProfileType : UserProfile => this.Provider.GetUserProfile<TUserProfileType>(user);

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Type of the profile.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(User user, Type profileType) => this.Provider.GetUserProfile(user, profileType);

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeName">The full name of the profile type.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(Guid userId, string profileTypeName) => this.Provider.GetUserProfile(userId, profileTypeName);

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeName">FullName of the profile type to obtain.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(User user, string profileTypeName) => this.Provider.GetUserProfile(user, profileTypeName);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.
    /// </summary>
    /// <returns>The query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.</returns>
    public virtual IQueryable<UserProfileLink> GetUserProfileLinks() => this.Provider.GetUserProfileLinks();

    /// <summary>Deletes a profile</summary>
    /// <param name="item">The profile to delete</param>
    public virtual void Delete(UserProfile item) => this.Provider.Delete(item);

    /// <summary>Deletes all profiles for the specified profile type.</summary>
    /// <param name="profileType">The profile type whose profiles to delete.</param>
    public virtual void DeleteProfilesForProfileType(Type profileType) => this.Provider.DeleteProfilesForProfileType(profileType);

    internal void SetPreference<TType>(Guid userid, string key, object value)
    {
      if (value == null)
        return;
      if (!(this.GetUserProfile(userid, typeof (SitefinityProfile).FullName) is SitefinityProfile userProfile))
        throw new ItemNotFoundException();
      JObject jobject = string.IsNullOrEmpty(userProfile.Preferences) ? new JObject() : JObject.Parse(userProfile.Preferences);
      Type type = value.GetType();
      JToken jtoken = type.IsPrimitive || object.Equals((object) type, (object) typeof (string)) ? (JToken) value.ToString() : (JToken) JObject.FromObject(value);
      jobject[key] = jtoken;
      if (!(jobject.ToString(Formatting.None) != userProfile.Preferences))
        return;
      userProfile.Preferences = jobject.ToString(Formatting.None);
      this.SaveChanges();
    }

    internal void DeletePreference(Guid userId, string key)
    {
      if (!(this.GetUserProfile(userId, typeof (SitefinityProfile).FullName) is SitefinityProfile userProfile))
        throw new ItemNotFoundException();
      JObject jobject = string.IsNullOrEmpty(userProfile.Preferences) ? new JObject() : JObject.Parse(userProfile.Preferences);
      jobject.Remove(key);
      if (!(jobject.ToString(Formatting.None) != userProfile.Preferences))
        return;
      userProfile.Preferences = jobject.ToString(Formatting.None);
      this.SaveChanges();
    }

    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      UserProfileDataProvider provider = sender as UserProfileDataProvider;
      bool flag = false;
      if (provider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker data)
        flag = data.SkipAutoTracking;
      foreach (object dirtyItem in (IEnumerable) provider.GetDirtyItems())
      {
        if (dirtyItem is SitefinityProfile)
        {
          SitefinityProfile sitefinityProfile = (SitefinityProfile) dirtyItem;
          if (!flag)
          {
            if (data == null)
              data = new TaxonomyStatisticsTracker();
            data.Track((object) sitefinityProfile, (DataProviderBase) provider);
          }
        }
      }
      if (data == null || !data.HasChanges())
        return;
      provider.SetExecutionStateData("taxonomy_statistics_changes", (object) data);
    }

    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      UserProfileDataProvider profileDataProvider = sender as UserProfileDataProvider;
      if (!(profileDataProvider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker executionStateData))
        return;
      executionStateData.SaveChanges();
      profileDataProvider.SetExecutionStateData("taxonomy_statistics_changes", (object) null);
    }

    private void ThrowDuplicateUrlException(string url) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ContentResources>().DuplicateUrlException, (object) url), (Exception) null);
  }
}
