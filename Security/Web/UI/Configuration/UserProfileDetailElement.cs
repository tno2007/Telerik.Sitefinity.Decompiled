// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Configuration.UserProfileDetailElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Web.UI.Contracts;
using Telerik.Sitefinity.Security.Web.UI.Definitions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Configuration
{
  /// <summary>
  /// The configuration element for the detail user profile view.
  /// </summary>
  public class UserProfileDetailElement : 
    ContentViewDetailElement,
    IUserProfileViewDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Configuration.UserProfileDetailElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public UserProfileDetailElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new UserProfileDetailDefinition((ConfigElement) this);

    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    [ConfigurationProperty("profileTypeFullName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProfileTypeFullNameDescription", Title = "ProfileTypeFullNameCaption")]
    public string ProfileTypeFullName
    {
      get => (string) this["profileTypeFullName"];
      set => this["profileTypeFullName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the action that will be executed on successful user profile submission.
    /// </summary>
    /// <value>The success action.</value>
    [ConfigurationProperty("submittingUserProfileSuccessAction")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SubmittingUserProfileSuccessActionDescription", Title = "SubmittingUserProfileSuccessActionCaption")]
    public SubmittingSuccessAction? SubmittingUserProfileSuccessAction
    {
      get => (SubmittingSuccessAction?) this["submittingUserProfileSuccessAction"];
      set => this["submittingUserProfileSuccessAction"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message to show on successful user profile submission.
    /// </summary>
    /// <value>The success message.</value>
    [ConfigurationProperty("submitSuccessMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SubmitSuccessMessageDescription", Title = "SubmitSuccessMessageCaption")]
    public string SubmitSuccessMessage
    {
      get => (string) this["submitSuccessMessage"];
      set => this["submitSuccessMessage"] = (object) value;
    }

    /// <summary>Get the data provider for this element.</summary>
    [ConfigurationProperty("provider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProviderDescription", Title = "ProviderCaption")]
    public string Provider
    {
      get => (string) this["provider"];
      set => this["provider"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the template that will be instanciated when the view is displayed for a not logged in user.
    /// </summary>
    /// <value>The name of the template.</value>
    [ConfigurationProperty("noUserTemplatePath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NoUserTemplateNameDescription", Title = "NoUserTemplateNameCaption")]
    public string NoUserTemplatePath
    {
      get => (string) this["noUserTemplatePath"];
      set => this["noUserTemplatePath"] = (object) value;
    }

    /// <summary>Gets or sets the id of the user.</summary>
    /// <value>The user id.</value>
    [ConfigurationProperty("userId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UserIdDescription", Title = "UserIdCaption")]
    public Guid? UserId
    {
      get => (Guid?) this["userId"];
      set => this["userId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the page id for the page responsible for showing the edit profile widget.
    /// </summary>
    /// <value>The edit profile page id.</value>
    [ConfigurationProperty("editProfilePageId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditProfilePageIdDescription", Title = "EditProfilePageIdCaption")]
    public Guid? EditProfilePageId
    {
      get => (Guid?) this["editProfilePageId"];
      set => this["editProfilePageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the page responsible for showing the change password widget.
    /// </summary>
    /// <value>The change password page id.</value>
    [ConfigurationProperty("changePasswordPageId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChangePasswordPageIdDescription", Title = "ChangePasswordPageIdCaption")]
    public Guid? ChangePasswordPageId
    {
      get => (Guid?) this["changePasswordPageId"];
      set => this["changePasswordPageId"] = (object) value;
    }

    [ConfigurationProperty("changePasswordQuestionAndAnswerPageId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChangePasswordQuestionAndAnswerPageIdDescription", Title = "ChangePasswordQuestionAndAnswerPageIdCaption")]
    public Guid? ChangePasswordQuestionAndAnswerPageId
    {
      get => (Guid?) this["changePasswordQuestionAndAnswerPageId"];
      set => this["changePasswordQuestionAndAnswerPageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the page that the will be shown when a use profile change was made.
    /// </summary>
    [ConfigurationProperty("redirectOnSubmitPageId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RedirectOnSubmitPageIdDescription", Title = "RedirectOnSubmitPageIdCaption")]
    public Guid? RedirectOnSubmitPageId
    {
      get => (Guid?) this["redirectOnSubmitPageId"];
      set => this["redirectOnSubmitPageId"] = (object) value;
    }

    /// <summary>Gets or sets the not logged template key.</summary>
    /// <value>The not logged template key.</value>
    [ConfigurationProperty("notLoggedTemplateKey")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NotLoggedTemplateKeyDescription", Title = "NotLoggedTemplateKeyCaption")]
    public string NotLoggedTemplateKey
    {
      get => (string) this["notLoggedTemplateKey"];
      set => this["notLoggedTemplateKey"] = (object) value;
    }

    /// <summary>Gets or sets the show additional modes links.</summary>
    /// <value>The show additional modes links.</value>
    [ConfigurationProperty("showAdditionalModesLinks", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowAdditionalModesLinksDescription", Title = "ShowAdditionalModesLinksCaption")]
    public bool? ShowAdditionalModesLinks
    {
      get => (bool?) this["showAdditionalModesLinks"];
      set => this["showAdditionalModesLinks"] = (object) value;
    }

    /// <summary>Gets or sets the open views in external pages.</summary>
    /// <value>The open views in external pages.</value>
    [ConfigurationProperty("openViewsInExternalPages")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OpenViewsInExternalPagesDescription", Title = "OpenViewsInExternalPagesCaption")]
    public bool? OpenViewsInExternalPages
    {
      get => (bool?) this["openViewsInExternalPages"];
      set => this["openViewsInExternalPages"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of UserProfileDetailElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct UserProfileViewDetailProps
    {
      public const string ProfileViewMode = "profileViewMode";
      public const string ProfileTypeFullName = "profileTypeFullName";
      public const string SubmittingUserProfileSuccessAction = "submittingUserProfileSuccessAction";
      public const string SubmitSuccessMessage = "submitSuccessMessage";
      public const string DisplayCurrentUser = "displayCurrentUser";
      public const string Provider = "provider";
      public const string NoUserTemplatePath = "noUserTemplatePath";
      public const string UserId = "userId";
      public const string EditProfilePageId = "editProfilePageId";
      public const string ChangePasswordPageId = "changePasswordPageId";
      public const string ChangePasswordQuestionAndAnswerPageId = "changePasswordQuestionAndAnswerPageId";
      public const string NotLoggedTemplateKey = "notLoggedTemplateKey";
      public const string ShowAdditionalModesLinks = "showAdditionalModesLinks";
      public const string OpenViewsInExternalPages = "openViewsInExternalPages";
      public const string RedirectOnSubmitPageId = "redirectOnSubmitPageId";
    }
  }
}
