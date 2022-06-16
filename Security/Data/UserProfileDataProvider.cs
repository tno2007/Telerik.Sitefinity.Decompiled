// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.UserProfileDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>Base abstract class for a Profile Data Provider</summary>
  public abstract class UserProfileDataProvider : 
    UrlDataProviderBase,
    IDataEventProvider,
    IOrganizableProvider
  {
    /// <summary>Creates new profile.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <returns></returns>
    public abstract UserProfile CreateProfile(User user);

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns></returns>
    public abstract UserProfile CreateProfile(User user, Guid profileID);

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns></returns>
    public abstract UserProfile CreateProfile(
      User user,
      Guid profileID,
      Type profileType);

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public abstract UserProfile CreateProfile(User user, string profileTypeName);

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile id.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public abstract UserProfile CreateProfile(
      User user,
      Guid profileId,
      string profileTypeName);

    public abstract UserProfile CreateProfile(Guid profileId, string profileTypeName);

    /// <summary>Gets a profile with the specified ID.</summary>
    /// <param name="ProfileID">The ID to search for.</param>
    /// <returns>A profile item.</returns>
    public abstract UserProfile GetProfile(Type profileType, Guid id);

    /// <summary>Gets all profiles related to the specified user.</summary>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    public abstract IQueryable<UserProfile> GetUserProfiles(User user);

    /// <summary>
    /// Gets all profiles related to the specified user by id.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public abstract IQueryable<UserProfile> GetUserProfiles(Guid userId);

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public abstract IQueryable<UserProfile> GetUserProfiles();

    /// <summary>Gets a query for profiles.</summary>
    /// <returns>The query for profiles.</returns>
    public abstract IQueryable<T> GetUserProfiles<T>() where T : UserProfile;

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Type of the profile.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual TUserProfileType GetUserProfile<TUserProfileType>(User user) where TUserProfileType : UserProfile => this.GetUserProfile(user, typeof (TUserProfileType)) as TUserProfileType;

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Type of the profile.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(User user, Type profileType)
    {
      if (profileType == (Type) null)
        throw new ArgumentNullException(nameof (profileType));
      return this.GetUserProfile(user, profileType.FullName);
    }

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeName">The full name of the profile type.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(Guid userId, string profileTypeName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UserProfileDataProvider.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new UserProfileDataProvider.\u003C\u003Ec__DisplayClass13_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.profileTypeName = profileTypeName;
      if (userId == Guid.Empty)
        throw new ArgumentNullException(nameof (userId));
      // ISSUE: reference to a compiler-generated field
      if (string.IsNullOrEmpty(cDisplayClass130.profileTypeName))
        throw new ArgumentNullException(nameof (profileTypeName), "The profile type full name should be specified.");
      try
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        return this.GetUserProfiles(userId).Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>((Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Call(up, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass130, typeof (UserProfileDataProvider.\u003C\u003Ec__DisplayClass13_0)), FieldInfo.GetFieldFromHandle(__fieldref (UserProfileDataProvider.\u003C\u003Ec__DisplayClass13_0.profileTypeName)))), parameterExpression)).SingleOrDefault<UserProfile>();
      }
      catch (InvalidOperationException ex)
      {
        // ISSUE: reference to a compiler-generated field
        throw new InvalidOperationException(string.Format("There are more than one user profile of type: \"{0}\" for the user with Id: \"{1}\"", (object) cDisplayClass130.profileTypeName, (object) userId), (Exception) ex);
      }
    }

    /// <summary>Gets the user's profile of the specified type.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeName">FullName of the profile type to obtain.</param>
    /// <remarks>Use this method if there is only one profile of the specified type for the specified user.</remarks>
    /// <exception cref="T:System.InvalidOperationException">Thrown if there is more than one user profile of the specified type.</exception>
    public virtual UserProfile GetUserProfile(User user, string profileTypeName) => this.GetUserProfile(user.Id, profileTypeName);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The profile to delete.</param>
    public abstract void Delete(UserProfile item);

    /// <summary>Deletes all profiles for the specified profile type.</summary>
    /// <param name="profileType">The profile type whose profiles to delete.</param>
    public virtual void DeleteProfilesForProfileType(Type profileType)
    {
      string profileTypeName = profileType.FullName;
      IQueryable<UserProfileLink> userProfileLinks = this.GetUserProfileLinks();
      Expression<Func<UserProfileLink, bool>> predicate = (Expression<Func<UserProfileLink, bool>>) (upl => upl.UserProfileTypeName == profileTypeName);
      foreach (UserProfileLink userProfileLink in (IEnumerable<UserProfileLink>) userProfileLinks.Where<UserProfileLink>(predicate))
        this.Delete(userProfileLink);
    }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance.
    /// </summary>
    /// <returns></returns>
    public abstract UserProfileLink CreateUserProfileLink();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance with the specified user identity.
    /// </summary>
    /// <param name="id">The identity of the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" />.</param>
    /// <returns></returns>
    public abstract UserProfileLink CreateUserProfileLink(Guid id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> instance for the specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the user.</param>
    public abstract UserProfileLink GetUserProfileLink(Guid id);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.
    /// </summary>
    /// <returns>The query for <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> items.</returns>
    public abstract IQueryable<UserProfileLink> GetUserProfileLinks();

    /// <summary>
    /// Deletes the specified <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" />.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.Security.Model.UserProfileLink" /> to delete.</param>
    public abstract void Delete(UserProfileLink item);

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <returns></returns>
    public override Type GetUrlTypeFor(Type itemType)
    {
      if (typeof (UserProfile).IsAssignableFrom(itemType))
        return typeof (UserProfileUrlData);
      throw new ArgumentException("Unknown type specified.");
    }

    protected override bool MatchUrlData(UrlData urlData, Type itemType) => urlData != null && urlData.Parent != null && urlData.Parent.GetType() == itemType;

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => nameof (UserProfileDataProvider);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type profileType, Guid id)
    {
      if (typeof (UserProfile).IsAssignableFrom(profileType))
        return (object) this.CreateProfile(id, profileType.FullName);
      throw DataProviderBase.GetInvalidItemTypeException(profileType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type profileType, Guid id)
    {
      if (profileType == (Type) null)
        throw new ArgumentNullException("itemType");
      return typeof (UserProfile).IsAssignableFrom(profileType) ? (object) this.GetProfile(profileType, id) : throw DataProviderBase.GetInvalidItemTypeException(profileType, typeof (UserProfile));
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (!(itemType == typeof (UserProfile)))
        return base.GetItem(itemType, id);
      return (object) this.GetUserProfiles().Where<UserProfile>((Expression<Func<UserProfile, bool>>) (q => q.Id == id)).FirstOrDefault<UserProfile>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (typeof (UserProfile).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions(SitefinityQuery.Get(itemType, (DataProviderBase) this), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (UserProfile));
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (item is UserProfile securedObject)
        this.Delete(securedObject);
      this.providerDecorator.DeletePermissions((object) securedObject);
      throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), typeof (UserProfile));
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (UserProfile),
      typeof (UserProfileLink)
    };

    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      List<IEvent> dataEventItems = new List<IEvent>();
      IList dirtyItems = this.GetDirtyItems();
      if (dirtyItems.Count == 0)
        return (ICollection<IEvent>) dataEventItems;
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        UserProfile userProfile = obj as UserProfile;
        ProfileEventBase profileEvent = (ProfileEventBase) null;
        if (userProfile != null)
        {
          object origin;
          this.TryGetExecutionStateData("EventOriginKey", out origin);
          switch (this.GetDirtyItemStatus((object) userProfile))
          {
            case SecurityConstants.TransactionActionType.New:
              profileEvent = (ProfileEventBase) new ProfileCreating()
              {
                Profile = userProfile
              };
              this.PopulateProfileEventBase(ref profileEvent, userProfile);
              this.RaiseEvent((IEvent) profileEvent, origin, true, true);
              profileEvent = (ProfileEventBase) new ProfileCreated();
              break;
            case SecurityConstants.TransactionActionType.Updated:
              profileEvent = (ProfileEventBase) new ProfileUpdating()
              {
                Profile = userProfile
              };
              this.PopulateProfileEventBase(ref profileEvent, userProfile);
              this.RaiseEvent((IEvent) profileEvent, origin, true, true);
              profileEvent = (ProfileEventBase) new ProfileUpdated();
              break;
            case SecurityConstants.TransactionActionType.Deleted:
              profileEvent = (ProfileEventBase) new ProfileDeleting()
              {
                Profile = userProfile
              };
              this.PopulateProfileEventBase(ref profileEvent, userProfile);
              this.RaiseEvent((IEvent) profileEvent, origin, true, true);
              profileEvent = (ProfileEventBase) new ProfileDeleted();
              break;
          }
          this.PopulateProfileEventBase(ref profileEvent, userProfile);
          dataEventItems.Add((IEvent) profileEvent);
        }
      }
      return (ICollection<IEvent>) dataEventItems;
    }

    private void PopulateProfileEventBase(ref ProfileEventBase profileEvent, UserProfile profile)
    {
      if (profile.UserLinks.Count <= 0)
        return;
      UserProfileLink userLink = profile.UserLinks[0];
      profileEvent.UserId = userLink.UserId;
      profileEvent.MembershipProviderName = userLink.MembershipManagerInfo.ProviderName;
      profileEvent.ProfileId = profile.Id;
      profileEvent.ProfileType = profile.GetType();
      profileEvent.ProfileProviderName = profile.GetProviderName();
      DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) profileEvent, (IDataItem) profile, (CultureInfo) null);
    }

    private void RaiseEvent(
      IEvent eventData,
      object origin,
      bool rollbackTransaction,
      bool throwExceptions)
    {
      if (origin != null)
        eventData.Origin = (string) origin;
      try
      {
        EventHub.Raise(eventData, throwExceptions);
      }
      catch (Exception ex)
      {
        if (rollbackTransaction)
          this.RollbackTransaction();
        throw ex;
      }
    }

    public bool DataEventsEnabled => true;

    public bool ApplyDataEventItemFilter(IDataItem item) => true;

    public IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (SitefinityProfile))
      {
        this.CurrentTaxonomyProperty = propertyName;
        int? totalCount1 = new int?();
        IQueryable<SitefinityProfile> items = (IQueryable<SitefinityProfile>) this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref totalCount1);
        IQueryable<SitefinityProfile> source;
        if (isSingleTaxon)
          source = items.Where<SitefinityProfile>((Expression<Func<SitefinityProfile, bool>>) (i => i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId));
        else
          source = items.Where<SitefinityProfile>((Expression<Func<SitefinityProfile, bool>>) (i => i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty).Any<Guid>((Func<Guid, bool>) (t => t == taxonId))));
        if (totalCount.HasValue)
          totalCount = new int?(source.Count<SitefinityProfile>());
        if (skip > 0)
          source = source.Skip<SitefinityProfile>(skip);
        if (take > 0)
          source = source.Take<SitefinityProfile>(take);
        return (IEnumerable) source;
      }
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (SitefinityProfile));
    }
  }
}
