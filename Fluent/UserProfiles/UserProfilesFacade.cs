// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.UserProfiles
{
  /// <summary>
  /// This class provides fluent API with most common functionality for working with a collection of user profiles.
  /// </summary>
  public class UserProfilesFacade : 
    BasePluralFacade<UserProfilesFacade, UserProfileFacade, UserProfilesFacade, UserProfile>
  {
    private User user;
    private IQueryable<UserProfile> userProfiles;
    private UserProfileManager userProfileManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="user">The user to which the profiles belong to.</param>
    public UserProfilesFacade(AppSettings appSettings, User user)
      : base(appSettings)
    {
      this.user = user != null ? user : throw new ArgumentNullException(nameof (user));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    /// <param name="user">The user to which the profiles belong to.</param>
    /// <param name="userProfiles">The user profiles to be loaded into the facade.</param>
    public UserProfilesFacade(
      AppSettings appSettings,
      User user,
      IQueryable<UserProfile> userProfiles)
      : this(appSettings, user)
    {
      this.userProfiles = userProfiles != null ? userProfiles : throw new ArgumentNullException(nameof (userProfiles));
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade.UserProfileManager" /> to be used by this facade.
    /// </summary>
    protected virtual UserProfileManager UserProfileManager
    {
      get
      {
        if (this.userProfileManager == null)
          this.userProfileManager = UserProfileManager.GetManager(this.settings.UserProfileProviderName, this.settings.TransactionName);
        return this.userProfileManager;
      }
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade" /> object.</returns>
    public UserProfilesFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.settings.TransactionName);
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.UserProfiles.UserProfilesFacade" /> object.</returns>
    public UserProfilesFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.settings.TransactionName);
      return this;
    }

    /// <summary>Gets or sets the items.</summary>
    /// <value>The items.</value>
    protected virtual IQueryable<UserProfile> UserProfiles
    {
      get
      {
        if (this.userProfiles == null)
          this.userProfiles = this.LoadItems();
        return this.userProfiles;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<IQueryable<UserProfile>>(value, "Items");
        this.userProfiles = value;
      }
    }

    /// <summary>
    /// Called by <see cref="!:Items" /> if no items are loaded
    /// </summary>
    /// <returns>Queries the manager for all <typeparamref name="TContent" /> items</returns>
    protected override IQueryable<UserProfile> LoadItems() => this.GetManager().GetUserProfiles(this.user);

    protected override IManager InitializeManager() => (IManager) UserProfileManager.GetManager(this.settings.UserProfileProviderName, this.settings.TransactionName);

    public virtual UserProfileManager GetManager() => (UserProfileManager) base.GetManager();
  }
}
