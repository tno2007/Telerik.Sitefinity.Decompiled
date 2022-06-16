// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of а master user profile view.
  /// </summary>
  public class UserProfileViewMasterDefinition : 
    ContentViewMasterDefinition,
    IUserProfileViewMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private string provider;
    private string profileTypeFullName;
    private Guid? userId;
    private Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode? usersDisplayMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileViewMasterDefinition" /> class.
    /// </summary>
    public UserProfileViewMasterDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileViewMasterDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public UserProfileViewMasterDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the name of the users provider to use.</summary>
    /// <value>The name of the users provider to use.</value>
    public string Provider
    {
      get => this.ResolveProperty<string>(nameof (Provider), this.provider);
      set => this.provider = value;
    }

    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    public string ProfileTypeFullName
    {
      get => this.ResolveProperty<string>(nameof (ProfileTypeFullName), this.profileTypeFullName);
      set => this.profileTypeFullName = value;
    }

    /// <summary>Gets or sets which set of users to show.</summary>
    /// <value>The users display mode.</value>
    public Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode? UsersDisplayMode
    {
      get => this.ResolveProperty<Telerik.Sitefinity.Security.Web.UI.UsersDisplayMode?>(nameof (UsersDisplayMode), this.usersDisplayMode);
      set => this.usersDisplayMode = value;
    }

    /// <summary>Gets or sets the id of the user to show.</summary>
    /// <value>The user id.</value>
    public Guid? UserId
    {
      get => this.ResolveProperty<Guid?>(nameof (UserId), this.userId);
      set => this.userId = value;
    }

    /// <summary>
    /// Gets the full title of the selected user (if any) in the form: FirstName LastName (UserName)
    /// </summary>
    public string SelectedUserTitle
    {
      get
      {
        if (this.userId.HasValue)
        {
          Guid? userId1 = this.userId;
          Guid empty = Guid.Empty;
          if ((userId1.HasValue ? (userId1.HasValue ? (userId1.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
          {
            Guid? userId2 = this.userId;
            User user = UserManager.GetManager(this.Provider).GetUser(userId2.Value);
            return user != null ? string.Format("{0} ({1})", (object) UserProfilesHelper.GetUserDisplayName(user.Id), (object) user.UserName) : string.Empty;
          }
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// Returns the full name of the ItemInfoDefinition type used to transfer the Roles property.
    /// </summary>
    public string RolesItemInfoName => typeof (ItemInfoDefinition).FullName;
  }
}
