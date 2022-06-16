// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of а detail user profile view.
  /// </summary>
  public class UserProfileDetailDefinition : 
    ContentViewDetailDefinition,
    IUserProfileViewDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private string profileTypeFullName;
    private SubmittingSuccessAction? submittingUserProfileSuccessAction;
    private string submitSuccessMessage;
    private Guid? userId;
    private string provider;
    private string noUserTemplatePath;
    private string notLoggedTemplateKey;
    private Guid? editProfilePageId;
    private Guid? changePasswordPageId;
    private Guid? changePasswordQuestionAndAnswerPageId;
    private Guid? redirectOnSubmitPageId;
    private bool? showAdditionalModesLinks;
    private bool? openViewsInExternalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileDetailDefinition" /> class.
    /// </summary>
    public UserProfileDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Definitions.UserProfileDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public UserProfileDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public UserProfileDetailDefinition GetDefinition() => this;

    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    public string ProfileTypeFullName
    {
      get => this.ResolveProperty<string>(nameof (ProfileTypeFullName), this.profileTypeFullName);
      set => this.profileTypeFullName = value;
    }

    /// <summary>
    /// Gets or sets the action that will be executed on successful user profile submission.
    /// </summary>
    /// <value>The success action.</value>
    public SubmittingSuccessAction? SubmittingUserProfileSuccessAction
    {
      get => this.ResolveProperty<SubmittingSuccessAction?>(nameof (SubmittingUserProfileSuccessAction), this.submittingUserProfileSuccessAction);
      set => this.submittingUserProfileSuccessAction = value;
    }

    /// <summary>
    /// Gets or sets the message to show on successfull user profile submission.
    /// </summary>
    /// <value>The success message.</value>
    public string SubmitSuccessMessage
    {
      get => this.ResolveProperty<string>(nameof (SubmitSuccessMessage), this.submitSuccessMessage);
      set => this.submitSuccessMessage = value;
    }

    /// <summary>Gets or sets the name of the users provider to use.</summary>
    /// <value>The name of the users provider to use.</value>
    public string Provider
    {
      get => this.ResolveProperty<string>(nameof (Provider), this.provider);
      set => this.provider = value;
    }

    /// <summary>
    /// Gets or sets the path of the template that will be instanciated when the view is displayed for a not logged in user.
    /// </summary>
    /// <value>The path of the template.</value>
    public string NoUserTemplatePath
    {
      get => this.ResolveProperty<string>(nameof (NoUserTemplatePath), this.noUserTemplatePath);
      set => this.noUserTemplatePath = value;
    }

    /// <summary>Gets or sets the id of the user.</summary>
    /// <value>The user id.</value>
    public Guid? UserId
    {
      get => this.ResolveProperty<Guid?>(nameof (UserId), this.userId);
      set => this.userId = value;
    }

    /// <summary>Gets the full name of the selected user.</summary>
    public string SelectedUserFullName
    {
      get
      {
        if (!this.UserId.HasValue || !(this.UserId.Value != Guid.Empty))
          return string.Empty;
        Guid id = this.UserId.Value;
        User user = !this.Provider.IsNullOrEmpty() ? UserManager.GetManager(this.Provider).GetUser(id) : UserManager.GetManager().GetUser(id);
        return user != null ? string.Format("{0} ({1})", (object) UserProfilesHelper.GetUserDisplayName(this.UserId.Value), (object) user.UserName) : string.Empty;
      }
    }

    /// <summary>
    /// Gets or sets the page id for the page responsible for showing the edit profile widget.
    /// </summary>
    /// <value>The edit profile page id.</value>
    public Guid? EditProfilePageId
    {
      get => this.ResolveProperty<Guid?>(nameof (EditProfilePageId), this.editProfilePageId);
      set => this.editProfilePageId = value;
    }

    /// <summary>
    /// Gets or sets the page id for the page responsible for showing the change password widget.
    /// </summary>
    /// <value>The change password page id.</value>
    public Guid? ChangePasswordPageId
    {
      get => this.ResolveProperty<Guid?>(nameof (ChangePasswordPageId), this.changePasswordPageId);
      set => this.changePasswordPageId = value;
    }

    public Guid? ChangePasswordQuestionAndAnswerPageId
    {
      get => this.ResolveProperty<Guid?>(nameof (ChangePasswordQuestionAndAnswerPageId), this.changePasswordQuestionAndAnswerPageId);
      set => this.changePasswordQuestionAndAnswerPageId = value;
    }

    /// <summary>
    /// Gets or sets the id of the page that the will be shown when a use profile change was made.
    /// </summary>
    public Guid? RedirectOnSubmitPageId
    {
      get => this.ResolveProperty<Guid?>(nameof (RedirectOnSubmitPageId), this.redirectOnSubmitPageId);
      set => this.redirectOnSubmitPageId = value;
    }

    /// <summary>Gets or sets the not logged template key.</summary>
    /// <value>The not logged template key.</value>
    public string NotLoggedTemplateKey
    {
      get => this.ResolveProperty<string>(nameof (NotLoggedTemplateKey), this.notLoggedTemplateKey);
      set => this.notLoggedTemplateKey = value;
    }

    /// <summary>Gets or sets the show additional modes links.</summary>
    /// <value>The show additional modes links.</value>
    public bool? ShowAdditionalModesLinks
    {
      get => this.ResolveProperty<bool?>(nameof (ShowAdditionalModesLinks), this.showAdditionalModesLinks, new bool?(true));
      set => this.showAdditionalModesLinks = value;
    }

    /// <summary>Gets or sets the open views in external pages.</summary>
    /// <value>The open views in external pages.</value>
    public bool? OpenViewsInExternalPages
    {
      get => this.ResolveProperty<bool?>(nameof (OpenViewsInExternalPages), this.openViewsInExternalPages);
      set => this.openViewsInExternalPages = value;
    }
  }
}
