// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.OpenAccessProfileProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.Security;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>An OpenAccess provider for profile data</summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessProfileProvider : 
    UserProfileDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider,
    IValidatingProfileProvider
  {
    private bool isNickNameUnique = true;

    /// <summary>Creates new profile.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <returns>The created profile instance.</returns>
    public override UserProfile CreateProfile(User user) => this.CreateProfile(user, this.GetNewGuid(), typeof (UserProfile));

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public override UserProfile CreateProfile(User user, Guid profileId) => this.CreateProfile(user, profileId, typeof (UserProfile));

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public override UserProfile CreateProfile(
      User user,
      Guid profileId,
      Type profileType)
    {
      return this.CreateProfile(user, profileId, profileType.FullName);
    }

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public override UserProfile CreateProfile(User user, string profileTypeName) => this.CreateProfile(user, this.GetNewGuid(), profileTypeName);

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile id.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public override UserProfile CreateProfile(
      User user,
      Guid profileId,
      string profileTypeName)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user), "Can't create profile for a user that is not set to an instance of an object.");
      if (profileId == Guid.Empty)
        throw new ArgumentNullException(nameof (profileId), "Can't create a profile instance with an empty guid as identity.");
      UserProfile instance = (UserProfile) (this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(profileTypeName) ?? throw new ArgumentNullException(string.Format("Can't create a profile instance because no dynamic type with name \"{0}\" was found", (object) profileTypeName))).CreateInstance((object) null);
      instance.Id = profileId;
      instance.ApplicationName = this.ApplicationName;
      instance.Owner = user.Id;
      instance.DateCreated = DateTime.Now;
      instance.LastModified = instance.DateCreated;
      UserProfileLink userProfileLink = this.CreateUserProfileLink();
      userProfileLink.MembershipManagerInfo = this.GetManagerInfo(user.ManagerInfo.ManagerType, user.ManagerInfo.ProviderName);
      userProfileLink.UserId = user.Id;
      userProfileLink.UserProfileTypeName = profileTypeName;
      userProfileLink.Profile = instance;
      ((IDataItem) instance).Provider = (object) this;
      this.GetContext().Add((object) instance);
      return instance;
    }

    public override UserProfile CreateProfile(Guid profileId, string profileTypeName)
    {
      UserProfile instance = (UserProfile) (this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(profileTypeName) ?? throw new ArgumentNullException(string.Format("Can't create a profile instance because no dynamic type with name \"{0}\" was found", (object) profileTypeName))).CreateInstance((object) null);
      instance.Id = profileId;
      instance.ApplicationName = this.ApplicationName;
      instance.DateCreated = DateTime.Now;
      instance.LastModified = instance.DateCreated;
      ((IDataItem) instance).Provider = (object) this;
      if (profileId != Guid.Empty)
        this.GetContext().Add((object) instance);
      return instance;
    }

    /// <summary>Gets the profile.</summary>
    /// <param name="profileID">The profile ID.</param>
    /// <returns></returns>
    public override UserProfile GetProfile(Type profileType, Guid profileID)
    {
      UserProfile profile = !(profileID == Guid.Empty) ? this.GetContext().GetItemById<UserProfile>(profileID.ToString()) : throw new ArgumentException("id cannot be empty GUID.");
      ((IDataItem) profile).Provider = (object) this;
      return profile;
    }

    /// <summary>Gets all profiles related to the specified user.</summary>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    public override IQueryable<UserProfile> GetUserProfiles(User user) => user != null ? this.GetUserProfiles(user.Id) : throw new ArgumentNullException(nameof (user), "Can't get profiles for a user that is not set to an instance of an object.");

    /// <summary>Gets all profiles related to the specified user.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public override IQueryable<UserProfile> GetUserProfiles(Guid userId)
    {
      if (userId == Guid.Empty)
        throw new ArgumentNullException(nameof (userId), "id cannot be empty GUID.");
      return this.GetUserProfileLinks().Where<UserProfileLink>((Expression<Func<UserProfileLink, bool>>) (upl => upl.UserId == userId)).Select<UserProfileLink, UserProfile>((Expression<Func<UserProfileLink, UserProfile>>) (upl => upl.Profile)).ToList<UserProfile>().AsQueryable<UserProfile>();
    }

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public override IQueryable<UserProfile> GetUserProfiles()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<UserProfile>((DataProviderBase) this).Where<UserProfile>((Expression<Func<UserProfile, bool>>) (profiles => profiles.ApplicationName == appName));
    }

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public override IQueryable<T> GetUserProfiles<T>() => SitefinityQuery.Get<T>((DataProviderBase) this).Select<T, T>((Expression<Func<T, T>>) (profiles => profiles));

    /// <summary>Deletes the specified profile.</summary>
    /// <param name="item">The profile to delete.</param>
    public override void Delete(UserProfile item)
    {
      this.providerDecorator.DeletePermissions((object) item);
      this.GetContext().Remove((object) item);
    }

    protected internal override string GetUrlPart<T>(string key, string format, T item)
    {
      string urlPart = base.GetUrlPart<T>(key, format, item);
      return key == "User.Id" ? urlPart.ComputeSha256Hash() : urlPart;
    }

    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      string str = config["isNickNameUnique"];
      if (!string.IsNullOrEmpty(str) && !bool.Parse(str))
        this.isNickNameUnique = false;
      base.Initialize(providerName, config, managerType);
    }

    public override void CommitTransaction()
    {
      if (this.isNickNameUnique)
        this.ValidateProfileNickName();
      base.CommitTransaction();
    }

    public override void FlushTransaction()
    {
      if (this.isNickNameUnique)
        this.ValidateProfileNickName();
      base.FlushTransaction();
    }

    private void ValidateProfileNickName()
    {
      IEnumerable<SitefinityProfile> source = this.GetDirtyItems().OfType<SitefinityProfile>();
      if (source.Count<SitefinityProfile>() == 0)
        return;
      foreach (SitefinityProfile sitefinityProfile in source)
      {
        ((IDataItem) sitefinityProfile).Transaction = (object) this.TransactionName;
        switch (this.GetDirtyItemStatus((object) sitefinityProfile))
        {
          case SecurityConstants.TransactionActionType.New:
            this.Validate(sitefinityProfile);
            continue;
          case SecurityConstants.TransactionActionType.Updated:
            if (sitefinityProfile.NickNameChanged)
            {
              this.Validate(sitefinityProfile);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }

    private void Validate(SitefinityProfile profile)
    {
      IValidatingProfileProvider validatingProfileProvider = (IValidatingProfileProvider) this;
      if (validatingProfileProvider == null)
        return;
      if (this.IsNicknameGuid(profile.Nickname))
        throw new ModelValidationException("Nickname cannot be a Guid. Please change the nickname.", "ErrorMessages", "NickNameGuidError");
      if (validatingProfileProvider.CheckForUniqueness<SitefinityProfile>(profile, "Nickname", profile.Nickname))
        return;
      bool flag = string.IsNullOrEmpty(profile.User.Password) && string.IsNullOrEmpty(profile.User.Salt);
      if (profile.NickNameAutoGenerated | flag)
      {
        this.AddSuffixNickname(profile);
      }
      else
      {
        this.RollbackTransaction();
        throw new ModelValidationException("A user with this nickname already exists.", "ErrorMessages", "NicknameAlreadyUsed");
      }
    }

    private void AddSuffixNickname(SitefinityProfile profile)
    {
      int num = this.GetContext().GetAll<SitefinityProfile>().Count<SitefinityProfile>((Expression<Func<SitefinityProfile, bool>>) (x => x.Nickname.Contains(profile.Nickname)));
      profile.Nickname = profile.Nickname + "_" + (num + 1).ToString();
    }

    private bool IsNicknameGuid(string nickName) => Guid.TryParse(nickName, out Guid _);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new UserProfileMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance.
    /// </summary>
    /// <returns></returns>
    public override UserProfileLink CreateUserProfileLink() => this.CreateUserProfileLink(this.GetNewGuid());

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance with the specified user identity.
    /// </summary>
    /// <param name="id">The identity of the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" />.</param>
    /// <returns></returns>
    public override UserProfileLink CreateUserProfileLink(Guid id)
    {
      UserProfileLink entity = new UserProfileLink()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      };
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance for the specified identity.
    /// </summary>
    /// <param name="id">The identity of the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" />.</param>
    /// <returns></returns>
    public override UserProfileLink GetUserProfileLink(Guid userId)
    {
      UserProfileLink userProfileLink = !(userId == Guid.Empty) ? this.GetContext().GetItemById<UserProfileLink>(userId.ToString()) : throw new ArgumentException("id cannot be empty GUID.");
      ((IDataItem) userProfileLink).Provider = (object) this;
      return userProfileLink;
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.
    /// </summary>
    /// <returns>
    /// The query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.
    /// </returns>
    public override IQueryable<UserProfileLink> GetUserProfileLinks()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<UserProfileLink>((DataProviderBase) this).Where<UserProfileLink>((Expression<Func<UserProfileLink, bool>>) (upl => upl.ApplicationName == appName));
    }

    /// <summary>
    /// Deletes the specified <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" />.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> to delete.</param>
    public override void Delete(UserProfileLink userProfileLink) => this.GetContext().Remove((object) userProfileLink);

    /// <inheritdoc />
    bool IValidatingProfileProvider.CheckForUniqueness<TProfile>(
      TProfile instance,
      string fieldName,
      string fieldValue)
    {
      return ((IValidatingProfileProvider) this).CheckForUniqueness<TProfile>(instance, string.Format("{0} == \"{1}\"", (object) fieldName, (object) fieldValue));
    }

    /// <inheritdoc />
    bool IValidatingProfileProvider.CheckForUniqueness<TProfile>(
      TProfile instance,
      string expression)
    {
      string appName = this.ApplicationName;
      Guid instanceId = instance.Id;
      return Queryable.Count<TProfile>(SitefinityQuery.Get<TProfile>((DataProviderBase) this).Where<TProfile>((Expression<Func<TProfile, bool>>) (p => p.ApplicationName == appName && p.Id != instanceId)).Where<TProfile>(expression)) == 0;
    }

    /// <inheritdoc />
    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      string str = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "\"" : "";
      if (upgradingFromSchemaVersionNumber < 2137)
      {
        OpenAccessConnection.Upgrade(context, "OpenAccessProfileProvider: Upgrade to 5.0 - Add 'Nickname' column to SitefinityProfile", string.Format("alter table {0}sf_sitefinity_profile{0} add {0}nickname{0} {1}", (object) str, (object) context.DatabaseContext.GetTextMapping(false, true, new int?(64))));
        OpenAccessConnection.Upgrade(context, "OpenAccessProfileProvider: Upgrade to 5.0 - Populate 'Nickname' column of SitefinityProfile", string.Format("update {0}sf_sitefinity_profile{0} set {0}nickname{0} = {0}sf_sitefinity_profile{0}.{0}id{0}", (object) str));
      }
      if (upgradingFromSchemaVersionNumber <= 2137 || upgradingFromSchemaVersionNumber >= 3040)
        return;
      string upgradeScript;
      switch (context.DatabaseContext.DatabaseType)
      {
        case DatabaseType.Oracle:
          upgradeScript = "DROP INDEX \"sf_idx_profile_nickname\"";
          break;
        case DatabaseType.MySQL:
          upgradeScript = "DROP INDEX sf_idx_profile_nickname ON sf_sitefinity_profile";
          break;
        default:
          upgradeScript = "DROP INDEX sf_sitefinity_profile.sf_idx_profile_nickname";
          break;
      }
      OpenAccessConnection.Upgrade(context, "OpenAccessProfileProvider: Upgrade to 5.1 - Deletes Unique index for 'Nickname' sf_idx_profile_nickname of SitefinityProfile", upgradeScript);
    }

    /// <inheritdoc />
    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
    }
  }
}
