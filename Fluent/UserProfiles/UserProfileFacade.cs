// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.UserProfiles.UserProfileFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.UserProfiles
{
  /// <summary>Manage individual user profile.</summary>
  public class UserProfileFacade : 
    BaseSingularFacade<UserProfileFacade, UserProfileFacade, UserProfile>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserProfileFacade&lt;TParentFacade, TCommentedItem&gt;" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="commentedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public UserProfileFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserProfileFacade&lt;TParentFacade, TCommentedItem&gt;" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="userProfileId">The user profile id.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="commentedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When userProfileId is emtpy Guid</exception>
    public UserProfileFacade(AppSettings settings, Guid userProfileId)
      : base(settings, userProfileId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserProfileFacade&lt;TParentFacade, TCommentedItem&gt;" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="userProfile">The user profile.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentNullException">When userProfile is null</exception>
    public UserProfileFacade(AppSettings settings, UserProfile userProfile)
      : base(settings, userProfile)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserProfileFacade&lt;TParentFacade, TCommentedItem&gt;" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Name of the profile type (type.FullName).</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="user" /> is null</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="profileTypeName" /> is null or empty string</exception>
    public UserProfileFacade(AppSettings settings, User user, string profileTypeName)
      : base(settings)
    {
      FacadeHelper.AssertArgumentNotNull<User>(user, nameof (user));
      FacadeHelper.AssertArgumentNotNull<string>(profileTypeName, nameof (profileTypeName));
      this.Item = this.LoadUserProfile(user, profileTypeName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:UserProfileFacade&lt;TParentFacade, TCommentedItem&gt;" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="user">The user.</param>
    /// <param name="profileType">Type of the profile.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="user" /> is null</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="profileType" /> is null </exception>
    public UserProfileFacade(AppSettings settings, User user, Type profileType)
      : this(settings, user, profileType.FullName)
    {
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="M:Telerik.Sitefinity.Fluent.UserProfiles.UserProfileFacade.GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) UserProfileManager.GetManager(this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>Creates new profile.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfileFacade Create(User user)
    {
      this.Item = this.GetManager().CreateProfile(user);
      return this;
    }

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfileFacade Create(User user, Guid profileId)
    {
      this.Item = this.GetManager().CreateProfile(user, profileId);
      return this;
    }

    /// <summary>Creates new profile with the specified identity.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileID">The profile identity.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfileFacade Create(
      User user,
      Guid profileId,
      Type profileType)
    {
      this.Item = this.GetManager().CreateProfile(user, profileId, profileType);
      return this;
    }

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfileFacade Create(User user, string profileTypeName)
    {
      this.Item = this.GetManager().CreateProfile(user, profileTypeName);
      return this;
    }

    /// <summary>Creates new profile from the specified type.</summary>
    /// <param name="user">The user to create profile for.</param>
    /// <param name="profileId">The profile id.</param>
    /// <param name="profileTypeName">The name of the profile type.</param>
    /// <returns>The created profile instance.</returns>
    public virtual UserProfileFacade Create(
      User user,
      Guid profileId,
      string profileTypeName)
    {
      this.Item = this.GetManager().CreateProfile(user, profileId, profileTypeName);
      return this;
    }

    /// <summary>Gets the manager.</summary>
    /// <returns></returns>
    protected virtual UserProfileManager GetManager() => UserProfileManager.GetManager(this.settings.UserProfileProviderName, this.settings.TransactionName);

    /// <summary>
    /// Performs an arbitrary action on the content object and Invoking a url recompilation for the item.
    /// </summary>
    /// <param name="setAction">An action to be performed on the content object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the content object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public override UserProfileFacade Do(Action<UserProfile> setAction)
    {
      FacadeHelper.Assert(this.Item != null, "Not initialize item");
      setAction(this.Item);
      this.GetManager().RecompileItemUrls<UserProfile>(this.Item);
      return this.GetCurrentFacade();
    }

    private UserProfile LoadUserProfile(User user, string profileTypeName) => this.GetManager().GetUserProfile(user, profileTypeName);
  }
}
