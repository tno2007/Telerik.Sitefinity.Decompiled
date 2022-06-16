// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfilesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.DynamicData;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  public static class UserProfilesHelper
  {
    public static void CreateUserProfileType(
      UserProfileTypeViewModel profileTypeData,
      string metaDataProviderName,
      bool restartApplication = true)
    {
      ConfigManager manager1 = ConfigManager.GetManager();
      UserProfilesConfig section1 = manager1.GetSection<UserProfilesConfig>();
      string key = typeof (UserProfile).Namespace + "." + profileTypeData.Name;
      if (section1.ProfileTypesSettings.ContainsKey(key))
        throw new ArgumentException(Res.Get<UserProfilesResources>().ErrorProfileTypeAlreadyExists);
      string userFriendlyName = profileTypeData.Title;
      MetadataManager manager2 = MetadataManager.GetManager();
      if (manager2.GetMetaTypeDescriptions().Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (td => td.UserFriendlyName == userFriendlyName)).Count<MetaTypeDescription>() > 0)
        throw new ArgumentException(Res.Get<UserProfilesResources>().ErrorProfileTypeTitleAlreadyExists);
      string name = profileTypeData.Name;
      MetaType metaType = manager2.CreateMetaType(typeof (UserProfile).Namespace, profileTypeData.Name);
      metaType.BaseClassName = typeof (UserProfile).FullName;
      metaType.IsDynamic = true;
      metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
      MetaTypeDescription metaTypeDescription = manager2.CreateMetaTypeDescription(metaType.Id);
      UserProfilesHelper.UpdateUserProfileType(metaType, metaTypeDescription, section1, profileTypeData);
      manager2.SaveChanges();
      manager1.SaveSection((ConfigSection) section1);
      string fullTypeName = metaType.FullTypeName;
      profileTypeData.DynamicTypeName = fullTypeName;
      string viewDefinitionName = UserProfilesHelper.GetContentViewDefinitionName(profileTypeData.Name);
      ContentViewConfig section2 = manager1.GetSection<ContentViewConfig>();
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls = section2.ContentViewControls;
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer((ConfigElement) contentViewControls, viewDefinitionName).SetContentTypeName(fullTypeName);
      ContentViewControlElement element = fluentContentView.Get();
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.BackendCreate, FieldDisplayMode.Write);
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.BackendEdit, FieldDisplayMode.Write);
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.BackendView, FieldDisplayMode.Read);
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.FrontendCreate, FieldDisplayMode.Write);
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.FrontendEdit, FieldDisplayMode.Write);
      UserProfilesHelper.InsertDetailView(fluentContentView, ProfileTypeViewKind.FrontendView, FieldDisplayMode.Read);
      contentViewControls.Add(element);
      manager1.SaveSection((ConfigSection) section2);
      UserProfileManager.GetManager().GetUserProfiles().Count<UserProfile>();
    }

    private static void InsertDetailView(
      ContentViewControlDefinitionFacade fluentContentView,
      ProfileTypeViewKind viewKind,
      FieldDisplayMode displayMode)
    {
      DetailViewDefinitionFacade definitionFacade = fluentContentView.AddDetailView(UserProfilesHelper.GetContentViewName(viewKind)).SetTitle(UserProfilesHelper.GetContentViewTitle(viewKind)).HideTopToolbar().SetDisplayMode(displayMode).LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element = definitionFacade.Get();
      string fieldsSectionName = CustomFieldsContext.CustomFieldsSectionName;
      ContentViewSectionElement viewSectionElement;
      if (!element.Sections.TryGetValue(fieldsSectionName, out viewSectionElement))
        viewSectionElement = definitionFacade.AddExpandableSection(fieldsSectionName).SetDisplayMode(element.DisplayMode).Get();
      fluentContentView.Get().ViewsConfig.Add((ContentViewDefinitionElement) element);
    }

    public static void SaveUserProfileType(
      Guid userProfileTypeId,
      UserProfileTypeViewModel profileTypeData,
      string metaDataProviderName)
    {
      string userFriendlyName = profileTypeData.Title;
      MetadataManager manager1 = MetadataManager.GetManager();
      if (manager1.GetMetaTypeDescriptions().Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (td => td.UserFriendlyName == userFriendlyName && td.Id != userProfileTypeId)).Count<MetaTypeDescription>() > 0)
        throw new ArgumentException(Res.Get<UserProfilesResources>().ErrorProfileTypeTitleAlreadyExists);
      MetaTypeDescription metaTypeDescription = manager1.GetMetaTypeDescription(profileTypeData.Id);
      MetaType metaType = manager1.GetMetaType(metaTypeDescription.MetaTypeId);
      metaTypeDescription.UserFriendlyName = userFriendlyName;
      ConfigManager manager2 = ConfigManager.GetManager();
      UserProfilesConfig section = manager2.GetSection<UserProfilesConfig>();
      UserProfilesHelper.UpdateUserProfileType(metaType, metaTypeDescription, section, profileTypeData);
      manager1.SaveChanges();
      manager2.SaveSection((ConfigSection) section);
    }

    public static IQueryable<UserProfileTypeViewModel> GetUserProfileTypes(
      string metaDataProviderName)
    {
      Telerik.Sitefinity.Fluent.AppSettings appSettings = App.Prepare();
      if (!string.IsNullOrEmpty(metaDataProviderName))
        appSettings.MetadataProviderName = metaDataProviderName;
      FluentSitefinity fluentSitefinity = appSettings.WorkWith();
      List<UserProfileTypeViewModel> source = new List<UserProfileTypeViewModel>();
      Assembly assembly = typeof (UserProfile).Assembly;
      foreach (ProfileTypeSettings profileTypesSetting in (ConfigElementCollection) Telerik.Sitefinity.Configuration.Config.Get<UserProfilesConfig>().ProfileTypesSettings)
      {
        Type type = TypeResolutionService.ResolveType(profileTypesSetting.GetKey(), false);
        if (type != (Type) null)
        {
          DynamicTypeFacade dynamicTypeFacade = fluentSitefinity.DynamicData().Type(type);
          UserProfileTypeViewModel userProfileType = UserProfilesHelper.GetUserProfileType(fluentSitefinity.DynamicData().TypeDescription(type).Get(), dynamicTypeFacade.Get(), profileTypesSetting);
          source.Add(userProfileType);
        }
      }
      return source.AsQueryable<UserProfileTypeViewModel>();
    }

    public static List<string> GetUserProfileTypeNames() => Telerik.Sitefinity.Configuration.Config.Get<UserProfilesConfig>().ProfileTypesSettings.Keys.ToList<string>();

    internal static UserProfileTypeViewModel GetUserProfileType(
      MetaTypeDescription metaTypeDescription,
      MetaType metaType,
      ProfileTypeSettings pts)
    {
      UserProfileTypeViewModel userProfileType = new UserProfileTypeViewModel(metaTypeDescription, metaType);
      userProfileType.ProfileProviderName = pts.ProfileProvider;
      bool? membershipProviders = pts.UseAllMembershipProviders;
      bool flag = true;
      userProfileType.MembershipProvidersUsage = membershipProviders.GetValueOrDefault() == flag & membershipProviders.HasValue ? MembershipProvidersUsage.AllProviders : MembershipProvidersUsage.SpecifiedProviders;
      userProfileType.SelectedMembershipProviders = UserProfileTypeViewModel.GetMembershipProviders(pts.MembershipProviders.Select<MembershipProviderElement, string>((Func<MembershipProviderElement, string>) (mp => mp.ProviderName)).ToArray<string>()).ToArray();
      return userProfileType;
    }

    internal static void UpdateUserProfileType(
      MetaType metaType,
      MetaTypeDescription typeDescription,
      UserProfilesConfig profilesConfig,
      UserProfileTypeViewModel profileTypeData)
    {
      string fullTypeName = metaType.FullTypeName;
      typeDescription.UserFriendlyName = profileTypeData.Title;
      UserProfilesHelper.UpdateConfiguration(profilesConfig, fullTypeName, profileTypeData);
    }

    internal static void UpdateConfiguration(
      UserProfilesConfig profilesConfig,
      string metaTypeFullName,
      UserProfileTypeViewModel profileTypeData)
    {
      ProfileTypeSettings profileTypeSettings = UserProfilesHelper.GetProfileTypeSettings(profilesConfig, metaTypeFullName);
      profileTypeSettings.ProfileProvider = profileTypeData.ProfileProviderName;
      profileTypeSettings.UseAllMembershipProviders = new bool?(profileTypeData.MembershipProvidersUsage == MembershipProvidersUsage.AllProviders);
      profileTypeSettings.MembershipProviders.Clear();
      if (profileTypeData.MembershipProvidersUsage != MembershipProvidersUsage.SpecifiedProviders)
        return;
      foreach (ProviderViewModel membershipProvider in profileTypeData.SelectedMembershipProviders)
        profileTypeSettings.MembershipProviders.Add(new MembershipProviderElement((ConfigElement) profileTypeSettings.MembershipProviders)
        {
          ProviderName = membershipProvider.ProviderName
        });
    }

    public static bool DeleteUserProfileType(
      Guid userProfileTypeId,
      string metaDataProviderName,
      bool resetModel = true)
    {
      Telerik.Sitefinity.Fluent.AppSettings appSettings = App.Prepare();
      if (!string.IsNullOrEmpty(metaDataProviderName))
        appSettings.MetadataProviderName = metaDataProviderName;
      DynamicTypeDescriptionFacade descriptionFacade = appSettings.WorkWith().DynamicData().TypeDescription(userProfileTypeId);
      DynamicTypeFacade dynamicTypeFacade = descriptionFacade.DynamicType();
      Type clrType = dynamicTypeFacade.Get().ClrType;
      UserProfileManager userProfileManager = !(clrType == typeof (SitefinityProfile)) ? UserProfilesHelper.GetUserProfileManager(clrType) : throw new InvalidOperationException(Res.Get<UserProfilesResources>().ErrorDeleteBuiltInProfileType);
      userProfileManager.DeleteProfilesForProfileType(clrType);
      userProfileManager.SaveChanges();
      descriptionFacade.Delete();
      dynamicTypeFacade.Delete();
      ConfigManager manager = ConfigManager.GetManager();
      UserProfilesConfig section1 = manager.GetSection<UserProfilesConfig>();
      section1.ProfileTypesSettings.Remove(dynamicTypeFacade.Get().FullTypeName);
      ContentViewConfig section2 = manager.GetSection<ContentViewConfig>();
      string viewDefinitionName = UserProfilesHelper.GetContentViewDefinitionName(clrType);
      section2.ContentViewControls.Remove(viewDefinitionName);
      dynamicTypeFacade.SaveChanges(resetModel);
      manager.SaveSection((ConfigSection) section1);
      manager.SaveSection((ConfigSection) section2);
      return true;
    }

    public static UserProfileTypeViewModel GetUserProfileType(
      Guid userProfileTypeId,
      string metaDataProviderName)
    {
      Telerik.Sitefinity.Fluent.AppSettings appSettings = App.Prepare();
      if (!string.IsNullOrEmpty(metaDataProviderName))
        appSettings.MetadataProviderName = metaDataProviderName;
      return UserProfilesHelper.GetUserProfileType(appSettings.WorkWith().DynamicData().TypeDescription(userProfileTypeId));
    }

    internal static UserProfileTypeViewModel GetUserProfileType(
      DynamicTypeDescriptionFacade typeDescriptionFacade)
    {
      MetaTypeDescription metaTypeDescription = typeDescriptionFacade.Get();
      MetaType metaType1 = typeDescriptionFacade.DynamicType().Get();
      ProfileTypeSettings profileTypeSettings;
      Telerik.Sitefinity.Configuration.Config.Get<UserProfilesConfig>().ProfileTypesSettings.TryGetValue(metaType1.FullTypeName, out profileTypeSettings);
      MetaType metaType2 = metaType1;
      ProfileTypeSettings pts = profileTypeSettings;
      return UserProfilesHelper.GetUserProfileType(metaTypeDescription, metaType2, pts);
    }

    public static UserProfileTypeViewModel GetUserProfileType(
      Type clrType,
      string metaDataProviderName)
    {
      Telerik.Sitefinity.Fluent.AppSettings appSettings = App.Prepare();
      if (!string.IsNullOrEmpty(metaDataProviderName))
        appSettings.MetadataProviderName = metaDataProviderName;
      return UserProfilesHelper.GetUserProfileType(appSettings.WorkWith().DynamicData().TypeDescription(clrType));
    }

    /// <summary>Gets the settings for the specified profile type.</summary>
    /// <param name="profilesConfig">The profiles config.</param>
    /// <param name="profileTypeName">The name of the profile type. <see cref="M:Telerik.Sitefinity.Modules.UserProfiles.UserProfilesHelper.GetProfileTypeName(System.Type)" />This is the FullName of the dynamic type that represents the profile type.</param>
    /// <returns></returns>
    public static ProfileTypeSettings GetProfileTypeSettings(
      UserProfilesConfig profilesConfig,
      string profileTypeName,
      bool createIfMissing = true)
    {
      ConfigElementDictionary<string, ProfileTypeSettings> profileTypesSettings = profilesConfig.ProfileTypesSettings;
      ProfileTypeSettings element;
      if (!profileTypesSettings.TryGetValue(profileTypeName, out element) && createIfMissing)
      {
        element = new ProfileTypeSettings((ConfigElement) profileTypesSettings);
        element.Name = profileTypeName;
        profileTypesSettings.Add(element);
      }
      return element;
    }

    /// <summary>Gets the profile type settings.</summary>
    /// <param name="profileTypeName">Name of the profile type.</param>
    /// <param name="createIfMissing">If true, new settings object is created and returned. It is also added to the collection of settings.</param>
    /// <returns></returns>
    public static ProfileTypeSettings GetProfileTypeSettings(
      string profileTypeName,
      bool createIfMissing = true)
    {
      return UserProfilesHelper.GetProfileTypeSettings(Telerik.Sitefinity.Configuration.Config.Get<UserProfilesConfig>(), profileTypeName, createIfMissing);
    }

    /// <summary>
    /// Gets the name of a view for the automatically created ContentView definition for a profile type.
    /// </summary>
    /// <param name="viewKind">Kind of the view.</param>
    /// <returns></returns>
    public static string GetContentViewName(ProfileTypeViewKind viewKind) => "View" + viewKind.ToString();

    /// <summary>
    /// Gets the title of a view for the automatically created ContentView definition for a profile type. Note, that strings returned from this methods are actually
    /// keys in the user profile resources. They are used to get localized value of the title.
    /// </summary>
    /// <param name="viewKind">Kind of the view.</param>
    /// <returns></returns>
    public static string GetContentViewTitle(ProfileTypeViewKind viewKind)
    {
      string str = "";
      if (viewKind == ProfileTypeViewKind.BackendCreate || viewKind == ProfileTypeViewKind.FrontendCreate)
        str += "CreateNewItem";
      else if (viewKind == ProfileTypeViewKind.BackendEdit || viewKind == ProfileTypeViewKind.FrontendEdit)
        str += "EditItem";
      else if (viewKind == ProfileTypeViewKind.BackendView || viewKind == ProfileTypeViewKind.FrontendView)
        str += "ViewItem";
      else if (viewKind == ProfileTypeViewKind.BackendOwnEdit)
        str += "EditOwnItem";
      if (viewKind == ProfileTypeViewKind.BackendCreate || viewKind == ProfileTypeViewKind.BackendEdit || viewKind == ProfileTypeViewKind.BackendView || viewKind == ProfileTypeViewKind.BackendOwnEdit)
        str += "Backend";
      return str + "ViewTitle";
    }

    /// <summary>
    /// Gets the name of the automatically created ContentView definition for a profile type.
    /// </summary>
    /// <param name="dynamicType">The name of the dynamic type that represents the profile type.</param>
    /// <returns></returns>
    public static string GetContentViewDefinitionName(Type dynamicType) => UserProfilesHelper.GetContentViewDefinitionName(dynamicType.Name);

    public static string GetContentViewDefinitionName(string dynamicTypeName) => "ProfileType_" + dynamicTypeName;

    /// <summary>
    /// Gets the name of the section in the automatically created ContentView definition for a profile type.
    /// </summary>
    /// <returns></returns>
    public static string GetDefaultSectionName() => "MainSection";

    /// <summary>
    /// Returns user profile objects for each profile type that is used with the given user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="missingProfiles">A list that will be filled with the names of the profile types that the user does not have profile for.</param>
    /// <param name="returnEmptyObjectsForMissingProfiles">Whether to return empty objects for missing profiles.</param>
    /// <returns></returns>
    public static Dictionary<string, UserProfile> GetUserProfiles(
      User user,
      out List<string> missingProfiles,
      bool returnEmptyObjectsForMissingProfiles = false)
    {
      string providerName = user.ProviderName;
      Dictionary<string, UserProfile> userProfiles = new Dictionary<string, UserProfile>();
      missingProfiles = new List<string>();
      foreach (string userProfileTypeName in UserProfilesHelper.GetUserProfileTypeNames())
      {
        if (providerName == null || UserProfilesHelper.IsProfileTypeAvailable(userProfileTypeName, providerName))
        {
          UserProfileManager manager = UserProfileManager.GetManager(UserProfilesHelper.GetProfileTypeSettings(userProfileTypeName).ProfileProvider);
          UserProfile userProfile = manager.GetUserProfile(user, userProfileTypeName);
          if (userProfile == null)
          {
            missingProfiles.Add(userProfileTypeName);
            if (returnEmptyObjectsForMissingProfiles)
              userProfile = manager.CreateProfile(user, userProfileTypeName);
          }
          else if (userProfile != null)
          {
            IDataItem dataItem = (IDataItem) userProfile;
            if (dataItem.Provider == null)
              dataItem.Provider = (object) manager.Provider;
          }
          userProfiles.Add(userProfileTypeName, userProfile);
        }
      }
      return userProfiles;
    }

    /// <summary>Deletes all user profiles for the specified user.</summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static void DeleteUserProfiles(User user)
    {
      string providerName = user.ProviderName;
      Dictionary<string, UserProfile> dictionary = new Dictionary<string, UserProfile>();
      foreach (string userProfileTypeName in UserProfilesHelper.GetUserProfileTypeNames())
      {
        if (providerName == null || UserProfilesHelper.IsProfileTypeAvailable(userProfileTypeName, providerName))
        {
          UserProfileManager manager = UserProfileManager.GetManager(UserProfilesHelper.GetProfileTypeSettings(userProfileTypeName).ProfileProvider);
          UserProfile userProfile = manager.GetUserProfile(user, userProfileTypeName);
          if (userProfile != null)
          {
            manager.Delete(userProfile);
            manager.SaveChanges();
          }
        }
      }
    }

    /// <summary>
    /// Returns empty user profile objects for all profile types that are used with the given membership provider.
    /// </summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <returns></returns>
    public static Dictionary<string, UserProfile> GetEmptyUserProfiles(
      string membershipProviderName)
    {
      Dictionary<string, UserProfile> emptyUserProfiles = new Dictionary<string, UserProfile>();
      foreach (string userProfileTypeName in UserProfilesHelper.GetUserProfileTypeNames())
      {
        if (membershipProviderName == null || UserProfilesHelper.IsProfileTypeAvailable(userProfileTypeName, membershipProviderName))
        {
          UserProfile userProfile = UserProfileManager.GetManager(UserProfilesHelper.GetProfileTypeSettings(userProfileTypeName).ProfileProvider).CreateItem(TypeResolutionService.ResolveType(userProfileTypeName)) as UserProfile;
          emptyUserProfiles.Add(userProfileTypeName, userProfile);
        }
      }
      return emptyUserProfiles;
    }

    /// <summary>
    /// Returns whether the specified profile type is available for the specified membership provider.
    /// </summary>
    /// <param name="profileTypeName">The profile type to check.</param>
    /// <param name="membershipProviderName">The membership provider.</param>
    /// <returns></returns>
    public static bool IsProfileTypeAvailable(string profileTypeName, string membershipProviderName)
    {
      ProfileTypeSettings profileTypeSettings = UserProfilesHelper.GetProfileTypeSettings(profileTypeName);
      bool? membershipProviders = profileTypeSettings.UseAllMembershipProviders;
      bool flag = false;
      return !(membershipProviders.GetValueOrDefault() == flag & membershipProviders.HasValue) || profileTypeSettings.MembershipProviders.Where<MembershipProviderElement>((Func<MembershipProviderElement, bool>) (mpe => mpe.ProviderName == membershipProviderName)).Count<MembershipProviderElement>() != 0;
    }

    /// <summary>
    /// Returns the name of the profile provider for the specified profile type.
    /// </summary>
    /// <param name="profileType">The name of the profile type.</param>
    /// <returns></returns>
    public static string GetUserProfilesProvider(string profileType) => (UserProfilesHelper.GetProfileTypeSettings(profileType, false) ?? throw new ArgumentException("The profile type '" + profileType + "' does not exist!")).ProfileProvider;

    /// <summary>Returns whether the specified profile type exists.</summary>
    /// <param name="profileType">Type profile type.</param>
    /// <returns></returns>
    public static bool ProfileTypeExists(string profileType) => UserProfilesHelper.GetProfileTypeSettings(profileType, false) != null;

    /// <summary>
    /// Returns the name of the profile provider for the specified profile type.
    /// </summary>
    /// <returns></returns>
    public static string GetUserProfilesProvider<PT>() where PT : UserProfile => UserProfilesHelper.GetUserProfilesProvider(UserProfilesHelper.GetProfileTypeName(typeof (PT)));

    /// <summary>
    /// Returns a new UserProfileManager instance for the specified profile type. One profile type is always handled
    /// by a single profile provider.
    /// </summary>
    /// <returns></returns>
    public static UserProfileManager GetUserProfileManager<PT>() where PT : UserProfile => UserProfilesHelper.GetUserProfileManager(typeof (PT));

    /// <summary>
    /// Returns a new UserProfileManager instance for the specified profile type. One profile type is always handled
    /// by a single profile provider.
    /// </summary>
    /// <returns></returns>
    public static UserProfileManager GetUserProfileManager(
      Type profileType,
      string transactionName = null)
    {
      return UserProfileManager.GetManager(UserProfilesHelper.GetUserProfilesProvider(UserProfilesHelper.GetProfileTypeName(profileType)), transactionName);
    }

    /// <summary>
    /// Returns the name of the profile type that the given dynamic type represents.
    /// </summary>
    /// <param name="dynamicType">Type of the dynamic.</param>
    /// <returns></returns>
    public static string GetProfileTypeName(Type dynamicType) => dynamicType.FullName;

    /// <summary>Gets the display name of the user.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public static string GetUserDisplayName(Guid userId) => ObjectFactory.Resolve<IUserDisplayNameBuilder>().GetUserDisplayName(userId);

    /// <summary>Gets the avatar image URL.</summary>
    /// <param name="userId">The user id.</param>
    /// <param name="image">The image.</param>
    /// <returns>Url for the image</returns>
    public static string GetAvatarImageUrl(Guid userId, out Telerik.Sitefinity.Libraries.Model.Image image) => ObjectFactory.Resolve<IUserDisplayNameBuilder>().GetAvatarImageUrl(userId, out image);

    /// <summary>
    /// Return if the user is logged with external provider or not
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>true - External User, false - Local User</returns>
    public static bool IsExternalUser(Guid userId) => ObjectFactory.Resolve<IUserDisplayNameBuilder>().IsExternalUser(userId);

    /// <summary>Gets the avatar image URL.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns>Url for the image</returns>
    internal static string GetAvatarImageUrl(Guid userId) => ObjectFactory.Resolve<IUserDisplayNameBuilder>().GetAvatarImageUrl(userId, out Telerik.Sitefinity.Libraries.Model.Image _);

    /// <summary>Returns the absolute thumbnail url of the avatar.</summary>
    /// <param name="sfProfile">The sitefinity profile.</param>
    /// <returns>The avatar thumbnail url.</returns>
    internal static string GetAvatarThumbnailUrl(SitefinityProfile sfProfile) => sfProfile.Avatar != null && sfProfile.Avatar.GetLinkedItem() is Telerik.Sitefinity.Libraries.Model.Image linkedItem ? linkedItem.ThumbnailUrl ?? linkedItem.MediaUrl : RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Absolute);

    /// <summary>
    /// Builds a query for searching users by first name or last name
    /// </summary>
    /// <param name="searchTerm">The phrase that is to be searched</param>
    /// <returns>Dynamic linq query for searching in FirstName or LastName with StartsWith</returns>
    internal static string BuildFirstNameLastNameFilter(string searchTerm)
    {
      string[] strArray = searchTerm.Split(new char[1]
      {
        ' '
      }, StringSplitOptions.RemoveEmptyEntries);
      List<string> values = new List<string>();
      foreach (string str in strArray)
        values.Add("FirstName.ToUpper().StartsWith(\"" + str + "\".ToUpper()) || LastName.ToUpper().StartsWith(\"" + str + "\".ToUpper())");
      return string.Join(" || ", (IEnumerable<string>) values);
    }

    /// <summary>
    /// Build a search query that is used to query Users (in sitefinity or in external providers)
    /// </summary>
    /// <param name="searchString">The phrase that is to be searched</param>
    /// <param name="additionalFilterExpression">Additional filter that will be added with OR operation</param>
    /// <param name="userIdsToBeIncluded">The ids of users that to be included in the filter (e.g. found in sf profiles)</param>
    /// <returns>Dynamic linq query for searching in users</returns>
    internal static string BuildUsersSearchFilterInlcuingResultsFromSitefinityProfile(
      string searchString,
      string additionalFilterExpression,
      IEnumerable<string> userIdsToBeIncluded)
    {
      string str1 = string.Join(" || ", userIdsToBeIncluded.Select<string, string>((Func<string, string>) (id => "Id == " + id)));
      string str2 = "Email.ToUpper().StartsWith(\"" + searchString + "\".ToUpper()) || UserName.ToUpper().StartsWith(\"" + searchString + "\".ToUpper()) || " + additionalFilterExpression;
      return string.IsNullOrEmpty(str1) ? str2 : str1 + " || " + str2;
    }
  }
}
