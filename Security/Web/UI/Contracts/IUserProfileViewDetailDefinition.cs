// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Contracts.IUserProfileViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Security.Web.UI.Contracts
{
  /// <summary>Base interface for the detail user profile view.</summary>
  public interface IUserProfileViewDetailDefinition : 
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the full name of the profile type.</summary>
    /// <value>The full name of the profile type.</value>
    string ProfileTypeFullName { get; set; }

    /// <summary>
    /// Gets or sets the action that will be executed on successful user profile submission.
    /// </summary>
    /// <value>The success action.</value>
    SubmittingSuccessAction? SubmittingUserProfileSuccessAction { get; set; }

    /// <summary>
    /// Gets or sets the message to show on successfull user profile submission.
    /// </summary>
    /// <value>The success message.</value>
    string SubmitSuccessMessage { get; set; }

    /// <summary>Gets or sets the name of the users provider to use.</summary>
    /// <value>The name of the users provider to use.</value>
    string Provider { get; set; }

    /// <summary>
    /// Gets or sets the name of the template that will be instanciated when the view is displayed for a not logged in user.
    /// </summary>
    /// <value>The name of the template.</value>
    string NoUserTemplatePath { get; set; }

    /// <summary>Gets or sets the id of the user to show.</summary>
    /// <value>The user id.</value>
    Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page responsible for showing the edit profile widget.
    /// </summary>
    /// <value>The edit profile page id.</value>
    Guid? EditProfilePageId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page responsible for showing the change password widget.
    /// </summary>
    /// <value>The change password page id.</value>
    Guid? ChangePasswordPageId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page responsible for showing the change security question and answer widget.
    /// </summary>
    Guid? ChangePasswordQuestionAndAnswerPageId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page that the will be shown when a use profile change was made.
    /// </summary>
    Guid? RedirectOnSubmitPageId { get; set; }

    /// <summary>
    /// Gets or sets the template key to use when loading the template for not authenticated users.
    /// </summary>
    string NotLoggedTemplateKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the links for change password and edit modes.
    /// </summary>
    bool? ShowAdditionalModesLinks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to the edit mode and change password modes are on different pages and the request should be redirected to them or not and the modes will be switched on the same control.
    /// </summary>
    bool? OpenViewsInExternalPages { get; set; }
  }
}
