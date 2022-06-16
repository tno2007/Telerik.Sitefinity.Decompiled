// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserProfileView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI.Cache;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Security.Web.UI.Designers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// This control acts as a container for the views that display user profiles.
  /// </summary>
  [RequireScriptManager]
  [ControlDesigner(typeof (UserProfileDesigner))]
  [PropertyEditorTitle(typeof (UserProfilesResources), "UserViewPropertyEditorTitle")]
  public class UserProfileView : ContentView
  {
    private UserProfileOutputCacheVariation outputCacheVariation;
    private IUserProfileViewDetailDefinition userProfileViewDetailDefinition;
    private UserProfileViewMode? profileViewMode;
    private bool? displayCurrentUser;
    private bool allowCurrentProfileUpdates = true;
    private string readModeViewName;
    private string writeModeViewName;
    private string changePasswordModeViewName;
    private string changeQuestionAndAnswerModelViewName;
    private string viewNameUrlKey;
    internal const string TemplateFilterAnonymousUserCondition = "anonymousUser";
    internal const string TemplateFilterEditModeCondtition = "editMode";
    internal const string TemplateFilterChangePasswordModeCondition = "changePasswordMode";
    internal const string TemplateFilterChangeQuestionAndAnswerModeCondition = "changeQuestionAndAnswerMode";
    internal const string DefaultViewNameUrlKey = "UserProfileViewName";

    /// <summary>
    /// 
    /// Gets or sets the name of the read mode view.
    /// </summary>
    /// <value>The name of the read mode view.</value>
    public string ReadModeViewName
    {
      get
      {
        if (this.readModeViewName.IsNullOrEmpty())
          this.readModeViewName = "UserProfilesFrontendDetailsRead";
        return this.readModeViewName;
      }
      set => this.readModeViewName = value;
    }

    /// <summary>Gets or sets the name of the write mode view.</summary>
    /// <value>The name of the write mode view.</value>
    public string WriteModeViewName
    {
      get
      {
        if (this.writeModeViewName.IsNullOrEmpty())
          this.writeModeViewName = "UserProfilesFrontendDetailsWrite";
        return this.writeModeViewName;
      }
      set => this.writeModeViewName = value;
    }

    /// <summary>
    /// Gets or sets the name of the change password mode view.
    /// </summary>
    /// <value>The name of the change password mode view.</value>
    public string ChangePasswordModeViewName
    {
      get
      {
        if (this.changePasswordModeViewName.IsNullOrEmpty())
          this.changePasswordModeViewName = "ChangePasswordDetailView";
        return this.changePasswordModeViewName;
      }
      set => this.changePasswordModeViewName = value;
    }

    public string ChangeQuestionAndAnswerModeViewName
    {
      get
      {
        if (this.changeQuestionAndAnswerModelViewName.IsNullOrEmpty())
          this.changeQuestionAndAnswerModelViewName = "ChangeQuestionAndAnswerDetailView";
        return this.changeQuestionAndAnswerModelViewName;
      }
      set => this.changeQuestionAndAnswerModelViewName = value;
    }

    /// <summary>
    /// Gets or sets whether current users can update their profiles
    /// </summary>
    public bool AllowCurrentProfileUpdates
    {
      get => this.allowCurrentProfileUpdates;
      set => this.allowCurrentProfileUpdates = value;
    }

    /// <summary>Gets or sets the display mode of the content view.</summary>
    /// <remarks>
    /// Note that this enumeration differs from the FieldDisplayMode.
    /// </remarks>
    public override ContentViewDisplayMode ContentViewDisplayMode
    {
      get => ContentViewDisplayMode.Automatic;
      set => base.ContentViewDisplayMode = value;
    }

    /// <summary>
    /// Gets or sets the name of the configuration definition for the whole control. From this definition
    /// control can find out all other configurations needed in order to construct views.
    /// </summary>
    /// <value>The name of the control definition.</value>
    public override string ControlDefinitionName
    {
      get => string.IsNullOrEmpty(base.ControlDefinitionName) ? "FrontendSingleProfile" : base.ControlDefinitionName;
      set => base.ControlDefinitionName = value;
    }

    /// <summary>
    /// Gets the name of the detail view to be loaded when the control is in the ContentViewDisplayMode.Detail.
    /// In order to change the detailViewName change the view names of the supported modes.
    /// </summary>
    /// <value></value>
    public override string DetailViewName
    {
      get => string.IsNullOrEmpty(base.DetailViewName) ? this.ReadModeViewName : base.DetailViewName;
      set => base.DetailViewName = value;
    }

    /// <summary>
    /// Gets or sets the name of the master view to be loaded when
    /// control is in the ContentViewDisplayMode.Master
    /// </summary>
    /// <value></value>
    public override string MasterViewName
    {
      get => string.IsNullOrEmpty(base.MasterViewName) ? "UserProfilesFrontendList" : base.MasterViewName;
      set => base.MasterViewName = value;
    }

    /// <summary>
    /// Gets or sets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public override string EmptyLinkText => Res.Get<UserProfilesResources>().EditProfilesWidgetSettings;

    /// <summary>Gets the user profile detail view definition.</summary>
    public IUserProfileViewDetailDefinition UserProfileDetailViewDefinition
    {
      get
      {
        if (this.userProfileViewDetailDefinition == null)
          this.userProfileViewDetailDefinition = this.DetailViewDefinition is IUserProfileViewDetailDefinition ? (IUserProfileViewDetailDefinition) this.DetailViewDefinition : throw new NotSupportedException("The UserProfileView control requires a detail definition instance implementing the IUserProfileViewDetailDefinition interface.");
        return this.userProfileViewDetailDefinition;
      }
    }

    /// <summary>Gets or sets the profile view mode.</summary>
    /// <value>The profile view mode.</value>
    public UserProfileViewMode? ProfileViewMode
    {
      get => this.profileViewMode;
      set => this.profileViewMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the current user's profile.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to display the current user's profile; otherwise, <c>false</c>.
    /// </value>
    public bool? DisplayCurrentUser
    {
      get => this.displayCurrentUser;
      set => this.displayCurrentUser = value;
    }

    /// <summary>
    /// Gets the title of the page responsible for showing the edit profile widget.
    /// </summary>
    public virtual string EditProfilePageTitle
    {
      get
      {
        string profilePageTitle = string.Empty;
        if (this.ControlDefinition.Views["UserProfilesFrontendDetailsRead"] is IUserProfileViewDetailDefinition view)
        {
          Guid? editProfilePageId = view.EditProfilePageId;
          if (editProfilePageId.HasValue)
          {
            editProfilePageId = view.EditProfilePageId;
            if (editProfilePageId.Value != Guid.Empty)
            {
              SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
              editProfilePageId = view.EditProfilePageId;
              string key = editProfilePageId.Value.ToString();
              SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(key);
              if (siteMapNodeFromKey != null)
                profilePageTitle = siteMapNodeFromKey.Title;
            }
          }
        }
        return profilePageTitle;
      }
    }

    /// <summary>
    /// Gets the title of the page responsible for showing the change password widget.
    /// </summary>
    public virtual string ChangePasswordPageTitle
    {
      get
      {
        string passwordPageTitle = string.Empty;
        if (this.ControlDefinition.Views["UserProfilesFrontendDetailsRead"] is IUserProfileViewDetailDefinition view)
        {
          Guid? changePasswordPageId = view.ChangePasswordPageId;
          if (changePasswordPageId.HasValue)
          {
            changePasswordPageId = view.ChangePasswordPageId;
            if (changePasswordPageId.Value != Guid.Empty)
            {
              SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
              changePasswordPageId = view.ChangePasswordPageId;
              string key = changePasswordPageId.Value.ToString();
              SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(key);
              if (siteMapNodeFromKey != null)
                passwordPageTitle = siteMapNodeFromKey.Title;
            }
          }
        }
        return passwordPageTitle;
      }
    }

    /// <summary>
    /// Gets the title of the page responsible for showing the change password question and answer widget.
    /// </summary>
    public virtual string ChangeQuestionAndAnswerPageTitle
    {
      get
      {
        string andAnswerPageTitle = string.Empty;
        if (this.ControlDefinition.Views["UserProfilesFrontendDetailsRead"] is IUserProfileViewDetailDefinition view)
        {
          Guid? questionAndAnswerPageId = view.ChangePasswordQuestionAndAnswerPageId;
          if (questionAndAnswerPageId.HasValue)
          {
            questionAndAnswerPageId = view.ChangePasswordQuestionAndAnswerPageId;
            if (questionAndAnswerPageId.Value != Guid.Empty)
            {
              SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
              questionAndAnswerPageId = view.ChangePasswordQuestionAndAnswerPageId;
              string key = questionAndAnswerPageId.Value.ToString();
              SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(key);
              if (siteMapNodeFromKey != null)
                andAnswerPageTitle = siteMapNodeFromKey.Title;
            }
          }
        }
        return andAnswerPageTitle;
      }
    }

    /// <summary>
    /// Gets the title of the page that will be shown when a user profile change was made.
    /// </summary>
    public virtual string RedirectOnSubmitPageTitle
    {
      get
      {
        string onSubmitPageTitle = string.Empty;
        if (this.ControlDefinition.Views["UserProfilesFrontendDetailsWrite"] is IUserProfileViewDetailDefinition view)
        {
          Guid? redirectOnSubmitPageId = view.RedirectOnSubmitPageId;
          if (redirectOnSubmitPageId.HasValue)
          {
            redirectOnSubmitPageId = view.RedirectOnSubmitPageId;
            if (redirectOnSubmitPageId.Value != Guid.Empty)
            {
              SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
              redirectOnSubmitPageId = view.RedirectOnSubmitPageId;
              string key = redirectOnSubmitPageId.Value.ToString();
              SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(key);
              if (siteMapNodeFromKey != null)
                onSubmitPageTitle = siteMapNodeFromKey.Title;
            }
          }
        }
        return onSubmitPageTitle;
      }
    }

    /// <summary>
    /// Gets or sets the name of the query string parameter that passes the template to be used.
    /// </summary>
    public override string ViewNameUrlKey
    {
      get
      {
        if (this.viewNameUrlKey == null)
          this.viewNameUrlKey = "UserProfileViewName";
        return this.viewNameUrlKey;
      }
      set => this.viewNameUrlKey = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool? DisableCanonicalUrlMetaTag
    {
      get => base.DisableCanonicalUrlMetaTag;
      set => base.DisableCanonicalUrlMetaTag = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool DisableModifyPageInfo
    {
      get => true;
      set
      {
      }
    }

    /// <inheritdoc />
    protected override void SubscribeCacheDependency()
    {
      if (this.outputCacheVariation != null && this.outputCacheVariation.NoCache)
        return;
      base.SubscribeCacheDependency();
    }

    protected override void CreateChildControls()
    {
      this.outputCacheVariation = new UserProfileOutputCacheVariation();
      PageRouteHandler.RegisterCustomOutputCacheVariation((ICustomOutputCacheVariation) this.outputCacheVariation);
      base.CreateChildControls();
    }

    /// <summary>
    /// Resolves a user profile instance for the current or the specified user.
    /// </summary>
    protected override void ResolveDetailItem()
    {
      if (this.UserProfileDetailViewDefinition.ProfileTypeFullName.IsNullOrEmpty())
        throw new ArgumentNullException("ProfileTypeFullName", "The definition of the detail view should contain the full name of the profile type to display.");
      User userToDisplay = this.GetUserToDisplay();
      if (userToDisplay != null)
      {
        UserProfile userProfile = (UserProfile) null;
        string profileTypeFullName = this.UserProfileDetailViewDefinition.ProfileTypeFullName;
        UserProfileManager manager = UserProfileManager.GetManager(UserProfilesHelper.GetUserProfilesProvider(profileTypeFullName));
        if (UserProfilesHelper.ProfileTypeExists(profileTypeFullName))
          userProfile = manager.GetUserProfile(userToDisplay, profileTypeFullName);
        if (userProfile != null)
          this.DetailViewDefinition.DataItemId = userProfile.Id;
        else if (userProfile == null)
          userProfile = (UserProfile) (manager.CreateProfile(userToDisplay, profileTypeFullName) as SitefinityProfile);
        this.DetailItem = (IDataItem) userProfile;
      }
      else
        base.ResolveDetailItem();
      if (this.outputCacheVariation == null || this.DetailItem == null || !(this.DetailItem is UserProfile))
        return;
      this.outputCacheVariation.ForceNoCache();
    }

    /// <summary>
    ///  ContentViewDisplayMode property is not working for UserProfileView.
    /// </summary>
    protected override void ResolvePageTitle()
    {
    }

    private User GetUserToDisplay()
    {
      UserManager manager = UserManager.GetManager(this.UserProfileDetailViewDefinition.Provider);
      Guid empty1 = Guid.Empty;
      string empty2 = string.Empty;
      Guid userId;
      if (this.ProfileViewMode.HasValue && this.ProfileViewMode.Value == UserProfileViewMode.Read && this.DisplayCurrentUser.HasValue && !this.DisplayCurrentUser.Value)
      {
        Guid? nullable = this.UserProfileDetailViewDefinition.UserId;
        nullable = nullable.HasValue ? this.UserProfileDetailViewDefinition.UserId : throw new ArgumentNullException("UserId", "The user id must be specified.");
        if (!(nullable.Value == Guid.Empty))
        {
          nullable = this.UserProfileDetailViewDefinition.UserId;
          userId = nullable.Value;
          goto label_8;
        }
      }
      else
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity == null)
          return (User) null;
        userId = currentIdentity.UserId;
        manager = UserManager.GetManager(currentIdentity.MembershipProvider);
      }
label_8:
      return userId != Guid.Empty ? manager.GetUser(userId) : (User) null;
    }
  }
}
