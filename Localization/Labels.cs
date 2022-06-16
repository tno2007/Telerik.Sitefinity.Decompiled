// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Labels
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("Labels", ResourceClassId = "Labels")]
  public sealed class Labels : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Labels" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public Labels()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Labels" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public Labels(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Help</summary>
    [ResourceEntry("LabelsTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Labels")]
    public string LabelsTitle => this[nameof (LabelsTitle)];

    /// <summary>
    /// Contains information about the labels section of the Layout editor
    /// </summary>
    [ResourceEntry("LabelInfo", Description = "The title of this class.", LastModified = "2014/03/12", Value = "Displayed in page editor only in order to differentiate columns")]
    public string LabelInfo => this[nameof (LabelInfo)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("LabelsDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for common user interface labels.")]
    public string LabelsDescription => this[nameof (LabelsDescription)];

    /// <summary>The title plural of this class.</summary>
    [ResourceEntry("LabelsTitlePlural", Description = "The title plural of this class.", LastModified = "2009/04/30", Value = "Labels")]
    public string LabelsTitlePlural => this[nameof (LabelsTitlePlural)];

    /// <summary>Label: by</summary>
    [ResourceEntry("By", Description = "label", LastModified = "2009/10/23", Value = "by")]
    public string By => this[nameof (By)];

    /// <summary>Label: on</summary>
    [ResourceEntry("On", Description = "label", LastModified = "2010/11/11", Value = "on")]
    public string On => this[nameof (On)];

    [ResourceEntry("TurnOn", Description = "label", LastModified = "2018/03/07", Value = "On")]
    public string TurnOn => this[nameof (TurnOn)];

    [ResourceEntry("TurnOff", Description = "label", LastModified = "2018/03/07", Value = "Off")]
    public string TurnOff => this[nameof (TurnOff)];

    /// <summary>Label: Version</summary>
    [ResourceEntry("Version", Description = "Version", LastModified = "2010/10/03", Value = "Version")]
    public string Version => this[nameof (Version)];

    /// <summary>Label: Notes</summary>
    [ResourceEntry("Notes", Description = "Notes", LastModified = "2010/10/03", Value = "Notes")]
    public string Notes => this[nameof (Notes)];

    /// <summary>Label: Layout</summary>
    [ResourceEntry("Layout", Description = "label", LastModified = "2009/10/23", Value = "Layout")]
    public string Layout => this[nameof (Layout)];

    /// <summary>Phrase: There is no text.</summary>
    [ResourceEntry("NoText", Description = "Phrase: There is no text.", LastModified = "2012/11/01", Value = "There is no text.")]
    public string NoText => this[nameof (NoText)];

    /// <summary>Label: Gadgets</summary>
    [ResourceEntry("Gadgets", Description = "label", LastModified = "2009/10/23", Value = "Gadgets")]
    public string Gadgets => this[nameof (Gadgets)];

    /// <summary>
    /// You do not have the permissions to access this page. Try using other credentials or contact the administrator.
    /// </summary>
    [ResourceEntry("LoggedAs", Description = "label", LastModified = "2009/05/26", Value = "You do not have the permissions to access this page. Try using other credentials or contact the administrator.")]
    public string LoggedAs => this[nameof (LoggedAs)];

    /// <summary>Label: What do you want to do?</summary>
    [ResourceEntry("WhatToDo", Description = "label", LastModified = "2009/05/26", Value = "What do you want to do?")]
    public string WhatToDo => this[nameof (WhatToDo)];

    /// <summary>
    /// Label: {0} has been locked for editing by {1} since {2}
    /// </summary>
    [ResourceEntry("HasBeenLockedForEditingBySince", Description = "label", LastModified = "2010/10/04", Value = "{0} has been locked for editing by {1} since {2}")]
    public string HasBeenLockedForEditingBySince => this[nameof (HasBeenLockedForEditingBySince)];

    /// <summary>phrase: What do you want to do now?</summary>
    [ResourceEntry("WhatDoYouWantToDoNow", Description = "phrase: What do you want to do now?", LastModified = "2009/01/14", Value = "What do you want to do now?")]
    public string WhatDoYouWantToDoNow => this[nameof (WhatDoYouWantToDoNow)];

    /// <summary>Label: Logout</summary>
    [ResourceEntry("Logout", Description = "Label", LastModified = "2009/05/26", Value = "Logout")]
    public string Logout => this[nameof (Logout)];

    /// <summary>Label: Switch User</summary>
    [ResourceEntry("SwitchUser", Description = "Label", LastModified = "2009/05/26", Value = "Switch User")]
    public string SwitchUser => this[nameof (SwitchUser)];

    /// <summary>Label: Change Password</summary>
    [ResourceEntry("ChangePassword", Description = "Label", LastModified = "2009/05/26", Value = "Change Password")]
    public string ChangePassword => this[nameof (ChangePassword)];

    /// <summary>Label: Change password</summary>
    [ResourceEntry("ChangePasswordText", Description = "Label", LastModified = "2009/05/26", Value = "Change password")]
    public string ChangePasswordText => this[nameof (ChangePasswordText)];

    /// <summary>Label: Change Password</summary>
    [ResourceEntry("RegisterUser", Description = "Create User", LastModified = "2009/05/26", Value = "Create User")]
    public string RegisterUser => this[nameof (RegisterUser)];

    /// <summary>Incorrect Username/Password Combination.</summary>
    [ResourceEntry("IncorrectUsernamePassword", Description = "Label for incorrect credentials.", LastModified = "2016/12/08", Value = "Incorrect credentials")]
    public string IncorrectUsernamePassword => this[nameof (IncorrectUsernamePassword)];

    /// <summary>
    /// Message: You do not have a permission to access "{0}".<br />Contact your administrator for more information.
    /// </summary>
    [ResourceEntry("AutomaticallyNavigated", Description = "Message: You do not have a permission to access \"{0}\".<br/>Contact your administrator for more information.", LastModified = "2012/10/09", Value = "You do not have a permission to access \"{0}\".<br/>Contact your administrator for more information.")]
    public string AutomaticallyNavigated => this[nameof (AutomaticallyNavigated)];

    /// <summary>
    /// Message: You do not have a permission to access current site - \"{0}\".<br />Contact your administrator for more information or choose a different site.
    /// </summary>
    [ResourceEntry("SiteNotAccessible", Description = "You do not have a permission to access current site - \"{0}\".<br/>Contact your administrator for more information or choose a different site.", LastModified = "2018/02/08", Value = "You do not have a permission to access current site - \"{0}\".<br/>Contact your administrator for more information or choose a different site.")]
    public string SiteNotAccessible => this[nameof (SiteNotAccessible)];

    /// <summary>Label: Password Recovery</summary>
    [ResourceEntry("PasswordRecovery", Description = "label", LastModified = "2009/05/26", Value = "Password Recovery")]
    public string PasswordRecovery => this[nameof (PasswordRecovery)];

    /// <summary>Word: Forgot your password?</summary>
    [ResourceEntry("ForgotYourPassword", Description = "label", LastModified = "2009/05/26", Value = "Forgot your password?")]
    public string ForgotYourPassword => this[nameof (ForgotYourPassword)];

    /// <summary>Word: Register</summary>
    [ResourceEntry("Register", Description = "word", LastModified = "2009/05/26", Value = "Register")]
    public string Register => this[nameof (Register)];

    /// <summary>Word: Help</summary>
    [ResourceEntry("Help", Description = "word", LastModified = "2009/05/26", Value = "Help")]
    public string Help => this[nameof (Help)];

    /// <summary>Word: Status</summary>
    [ResourceEntry("Status", Description = "word", LastModified = "2009/05/26", Value = "Status")]
    public string Status => this[nameof (Status)];

    /// <summary>Word: Status:</summary>
    [ResourceEntry("StatusColonSpace", Description = "word", LastModified = "2011/03/18", Value = "Status: ")]
    public string StatusColonSpace => this[nameof (StatusColonSpace)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("Password", Description = "word", LastModified = "2009/05/21", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>phrase: Old password</summary>
    [ResourceEntry("OldPasswordText", Description = "Change password - Old password", LastModified = "2010/09/20", Value = "Old password")]
    public string OldPasswordText => this[nameof (OldPasswordText)];

    /// <summary>phrase: New password</summary>
    [ResourceEntry("NewPasswordText", Description = "Change password - New password", LastModified = "2010/09/20", Value = "New password")]
    public string NewPasswordText => this[nameof (NewPasswordText)];

    /// <summary>phrase: Confirm new password</summary>
    [ResourceEntry("ConfirmNewPasswordText", Description = "Change password - Confirm new password", LastModified = "2010/09/20", Value = "Confirm password")]
    public string ConfirmNewPasswordText => this[nameof (ConfirmNewPasswordText)];

    /// <summary>phrase: Password successfully changed</summary>
    [ResourceEntry("PasswordSuccessfullyChanged", Description = "phrase: Password successfully changed", LastModified = "2012/02/14", Value = "Password successfully changed")]
    public string PasswordSuccessfullyChanged => this[nameof (PasswordSuccessfullyChanged)];

    /// <summary>phrase: Proceed to your account</summary>
    [ResourceEntry("ProceedToYourAccount", Description = "phrase: Proceed to your account", LastModified = "2012/02/14", Value = "Proceed to your account")]
    public string ProceedToYourAccount => this[nameof (ProceedToYourAccount)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("Provider", Description = "word", LastModified = "2009/05/21", Value = "Authentication Provider")]
    public string Provider => this[nameof (Provider)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("Username", Description = "word", LastModified = "2016/12/08", Value = "Email / Username")]
    public string Username => this[nameof (Username)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("EmailOrUsername", Description = "phrase: Email or username", LastModified = "2017/02/06", Value = "Email or username")]
    public string EmailOrUsername => this[nameof (EmailOrUsername)];

    /// <summary>Word: Name</summary>
    [ResourceEntry("UserFirstAndLastNameName", Description = "Word: Name", LastModified = "2010/02/02", Value = "Name")]
    public string UserFirstAndLastNameName => this[nameof (UserFirstAndLastNameName)];

    /// <summary>Phrase: First Name</summary>
    [ResourceEntry("FirstName", Description = "Phrase: First Name", LastModified = "2010/02/02", Value = "First name")]
    public string FirstName => this[nameof (FirstName)];

    /// <summary>Phrase: Last Name</summary>
    [ResourceEntry("LastName", Description = "Phrase: Last Name", LastModified = "2010/02/02", Value = "Last name")]
    public string LastName => this[nameof (LastName)];

    /// <summary>Word: Roles</summary>
    [ResourceEntry("Roles", Description = "word", LastModified = "2009/05/21", Value = "Roles")]
    public string Roles => this[nameof (Roles)];

    /// <summary>Word: Users</summary>
    [ResourceEntry("Users", Description = "word", LastModified = "2009/05/21", Value = "Users")]
    public string Users => this[nameof (Users)];

    /// <summary>Message displayed to user when no users exist</summary>
    [ResourceEntry("NoUsersCreatedMessage", Description = "Message displayed to user when no users exist", LastModified = "2021/01/20", Value = "No users have been created yet")]
    public string NoUsersCreatedMessage => this[nameof (NoUsersCreatedMessage)];

    /// <summary>Title of the form for creating a new user.</summary>
    [ResourceEntry("CreateAUser", Description = "Title of the form for creating a new user", LastModified = "2021/01/20", Value = "Create a user")]
    public string CreateAUser => this[nameof (CreateAUser)];

    /// <summary>Word: Email</summary>
    [ResourceEntry("Email", Description = "word", LastModified = "2009/05/21", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>Word: Done</summary>
    [ResourceEntry("Done", Description = "word", LastModified = "2009/05/21", Value = "Done")]
    public string Done => this[nameof (Done)];

    /// <summary>Phase: Add these tags</summary>
    [ResourceEntry("AddTheseTags", Description = "phrase", LastModified = "2009/05/21", Value = "Add these tags")]
    public string AddTheseTags => this[nameof (AddTheseTags)];

    /// <summary>Phase: Basic Templates</summary>
    [ResourceEntry("BasicTemplates", Description = "word", LastModified = "2019/03/20", Value = "Hybrid Templates")]
    public string BasicTemplates => this[nameof (BasicTemplates)];

    /// <summary>Phase: Select a section</summary>
    [ResourceEntry("SelectASection", Description = "word", LastModified = "2009/05/21", Value = "Select a section")]
    public string SelectASection => this[nameof (SelectASection)];

    /// <summary>Phase: Select a category</summary>
    [ResourceEntry("SelectACategory", Description = "word", LastModified = "2009/05/21", Value = "Select a category")]
    public string SelectACategory => this[nameof (SelectACategory)];

    /// <summary>phrase: Select a tag</summary>
    [ResourceEntry("SelectATag", Description = "phrase: Select a tag", LastModified = "2010/09/08", Value = "Select a tag")]
    public string SelectATag => this[nameof (SelectATag)];

    /// <summary>
    /// Instruction: Separate tags with a space. To join two words in one tag use double quotes.<br /><strong>Example:</strong><em>sport London summer "daily commute"</em>
    /// </summary>
    [ResourceEntry("AddTagsExample", Description = "Instruction", LastModified = "2009/05/21", Value = "Separate tags with a space. To join two words in one tag use double quotes.<br /><strong>Example:</strong><em>sport London summer \"daily commute\"</em>")]
    public string AddTagsExample => this[nameof (AddTagsExample)];

    /// <summary>Phase: Select a master page</summary>
    [ResourceEntry("SelectAMasterpage", Description = "Phase: Select a master page", LastModified = "2012/01/05", Value = "Select a master page")]
    public string SelectAMasterpage => this[nameof (SelectAMasterpage)];

    /// <summary>Phase: Actions with role</summary>
    [ResourceEntry("ActionsWithRole", Description = "word", LastModified = "2009/05/21", Value = "Actions with role")]
    public string ActionsWithRole => this[nameof (ActionsWithRole)];

    /// <summary>Phase: Creation Date</summary>
    [ResourceEntry("CreationDate", Description = "word", LastModified = "2009/05/21", Value = "Creation Date")]
    public string CreationDate => this[nameof (CreationDate)];

    /// <summary>Word: Submit</summary>
    [ResourceEntry("Submit", Description = "word", LastModified = "2009/05/21", Value = "Submit")]
    public string Submit => this[nameof (Submit)];

    /// <summary>Word: User</summary>
    [ResourceEntry("User", Description = "word", LastModified = "2009/05/28", Value = "User")]
    public string User => this[nameof (User)];

    /// <summary>Word: LOGIN</summary>
    [ResourceEntry("LoginCaps", Description = "word", LastModified = "2010/07/26", Value = "Login")]
    public string LoginCaps => this[nameof (LoginCaps)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("Login", Description = "word", LastModified = "2009/05/21", Value = "Login")]
    public string Login => this[nameof (Login)];

    /// <summary>Word: Login</summary>
    [ResourceEntry("PasswordEmpty", Description = "word", LastModified = "2009/05/21", Value = "Password is required")]
    public string PasswordEmpty => this[nameof (PasswordEmpty)];

    /// <summary>Word: Email / Username is required</summary>
    [ResourceEntry("UsernameEmpty", Description = "word", LastModified = "2016/12/08", Value = "Email / Username is required")]
    public string UsernameEmpty => this[nameof (UsernameEmpty)];

    /// <summary>Word: Remember me</summary>
    [ResourceEntry("RememberMe", Description = "word", LastModified = "2016/12/09", Value = "Remember me")]
    public string RememberMe => this[nameof (RememberMe)];

    /// <summary>Word: Remember my choice</summary>
    [ResourceEntry("RememberMyChoice", Description = "word", LastModified = "2016/12/13", Value = "Remember my choice")]
    public string RememberMyChoice => this[nameof (RememberMyChoice)];

    /// <summary>Word: Request for permission</summary>
    [ResourceEntry("RequestForPermission", Description = "word", LastModified = "2016/12/13", Value = "Request for permission")]
    public string RequestForPermission => this[nameof (RequestForPermission)];

    /// <summary>Word: Grand access</summary>
    [ResourceEntry("GrandAccess", Description = "word", LastModified = "2016/12/13", Value = "Grand access")]
    public string GrandAccess => this[nameof (GrandAccess)];

    /// <summary>Word: Grand access to:</summary>
    [ResourceEntry("GrandAccessTo", Description = "word", LastModified = "2016/12/13", Value = "Grand access to:")]
    public string GrandAccessTo => this[nameof (GrandAccessTo)];

    /// <summary>Word: Create</summary>
    [ResourceEntry("Create", Description = "word", LastModified = "2009/04/13", Value = "Create")]
    public string Create => this[nameof (Create)];

    /// <summary>Word: Create...</summary>
    [ResourceEntry("CreateEllipsis", Description = "word", LastModified = "2009/04/13", Value = "Create...")]
    public string CreateEllipsis => this[nameof (CreateEllipsis)];

    /// <summary>phrase: Create a</summary>
    [ResourceEntry("CreateA", Description = "phrase: Create a", LastModified = "2010/01/07", Value = "Create a")]
    public string CreateA => this[nameof (CreateA)];

    /// <summary>phrase: Create a {0}</summary>
    [ResourceEntry("CreateAParameter", Description = "phrase: Create a {0}", LastModified = "2010/01/07", Value = "Create a {0}")]
    public string CreateAParameter => this[nameof (CreateAParameter)];

    /// <summary>phrase: Create an {0}</summary>
    [ResourceEntry("CreateAnParameter", Description = "phrase: Create an {0}", LastModified = "2011/03/29", Value = "Create an {0}")]
    public string CreateAnParameter => this[nameof (CreateAnParameter)];

    /// <summary>Word: Edit</summary>
    [ResourceEntry("Edit", Description = "word", LastModified = "2009/04/13", Value = "Edit")]
    public string Edit => this[nameof (Edit)];

    /// <summary>Word: Edit...</summary>
    [ResourceEntry("EditEllipsis", Description = "word", LastModified = "2009/04/13", Value = "Edit...")]
    public string EditEllipsis => this[nameof (EditEllipsis)];

    /// <summary>Word: View</summary>
    [ResourceEntry("View", Description = "word", LastModified = "2009/04/13", Value = "View")]
    public string View => this[nameof (View)];

    /// <summary>Phrase: View results</summary>
    [ResourceEntry("ViewResults", Description = "Phrase: View results", LastModified = "2017/08/23", Value = "View results")]
    public string ViewResults => this[nameof (ViewResults)];

    /// <summary>phrase: View original size</summary>
    [ResourceEntry("ViewOriginalSize", Description = "phrase: View original size", LastModified = "2011/12/01", Value = "View original size")]
    public string ViewOriginalSize => this[nameof (ViewOriginalSize)];

    /// <summary>phrase: Play video</summary>
    [ResourceEntry("PlayVideo", Description = "phrase: Play video", LastModified = "2011/12/01", Value = "Play video")]
    public string PlayVideo => this[nameof (PlayVideo)];

    /// <summary>phrase: Set as primary image</summary>
    [ResourceEntry("SetAsPrimaryImage", Description = "phrase: Set as primary image", LastModified = "2011/12/01", Value = "Set as primary image")]
    public string SetAsPrimaryImage => this[nameof (SetAsPrimaryImage)];

    /// <summary>phrase: Set as primary video</summary>
    [ResourceEntry("SetAsPrimaryVideo", Description = "phrase: Set as primary video", LastModified = "2011/12/01", Value = "Set as primary video")]
    public string SetAsPrimaryVideo => this[nameof (SetAsPrimaryVideo)];

    /// <summary>Word: Change</summary>
    [ResourceEntry("Change", Description = "word", LastModified = "2009/04/13", Value = "Change")]
    public string Change => this[nameof (Change)];

    /// <summary>Word: Change Owner</summary>
    [ResourceEntry("ChangeOwner", Description = "word", LastModified = "2009/04/13", Value = "Change owner")]
    public string ChangeOwner => this[nameof (ChangeOwner)];

    /// <summary>Word: Change</summary>
    [ResourceEntry("ChangeBtnIn", Description = "word", LastModified = "2009/04/13", Value = "<strong class=\"sfLinkBtnIn\">Change</strong>")]
    public string ChangeBtnIn => this[nameof (ChangeBtnIn)];

    /// <summary>Word: Change...</summary>
    [ResourceEntry("ChangeEllipsis", Description = "word", LastModified = "2009/04/13", Value = "Change...")]
    public string ChangeEllipsis => this[nameof (ChangeEllipsis)];

    /// <summary>Word: Save".</summary>
    [ResourceEntry("Save", Description = "word", LastModified = "2009/04/01", Value = "Save")]
    public string Save => this[nameof (Save)];

    /// <summary>Word: New</summary>
    [ResourceEntry("New", Description = "word", LastModified = "2009/04/01", Value = "New")]
    public string New => this[nameof (New)];

    /// <summary>Word: Delete</summary>
    [ResourceEntry("Delete", Description = "word", LastModified = "2009/04/01", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>Word: Deactivate</summary>
    [ResourceEntry("Deactivate", Description = "word", LastModified = "2019/06/21", Value = "Deactivate")]
    public string Deactivate => this[nameof (Deactivate)];

    /// <summary>Phrase: Yes, Delete this item</summary>
    [ResourceEntry("YesDelete", Description = "word", LastModified = "2011/01/04", Value = "Yes, Delete this item")]
    public string YesDelete => this[nameof (YesDelete)];

    /// <summary>Phrase: Delete page profile</summary>
    [ResourceEntry("YesDeletePageProfile", Description = "Phrase: Delete page profile", LastModified = "2017/02/07", Value = "Delete page profile")]
    public string YesDeletePageProfile => this[nameof (YesDeletePageProfile)];

    /// <summary>Phrase: Delete library profile</summary>
    [ResourceEntry("YesDeleteMediaProfile", Description = "Phrase: Delete library profile", LastModified = "2017/02/07", Value = "Delete library profile")]
    public string YesDeleteMediaProfile => this[nameof (YesDeleteMediaProfile)];

    /// <summary>Phrase: Yes, move</summary>
    [ResourceEntry("YesMove", Description = "word", LastModified = "2013/07/24", Value = "Yes, move")]
    public string YesMove => this[nameof (YesMove)];

    /// <summary>Phrase: Yes, Delete these items</summary>
    [ResourceEntry("YesDeleteTheseItems", Description = "Phrase: Yes, Delete these items", LastModified = "2011/01/11", Value = "Yes, Delete these items")]
    public string YesDeleteTheseItems => this[nameof (YesDeleteTheseItems)];

    /// <summary>Phrase: Yes, Delete this version</summary>
    [ResourceEntry("YesDeleteThisVersion", Description = "Phrase: Yes, Delete this version", LastModified = "2015/10/12", Value = "Yes, Delete this version")]
    public string YesDeleteThisVersion => this[nameof (YesDeleteThisVersion)];

    /// <summary>Word: Compare</summary>
    [ResourceEntry("Compare", Description = "Compare", LastModified = "2010/10/03", Value = "Compare")]
    public string Compare => this[nameof (Compare)];

    /// <summary>Phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Phrase", LastModified = "2009/05/18", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Word: cancel</summary>
    [ResourceEntry("Cancel", Description = "word", LastModified = "2009/05/18", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>Word: Views</summary>
    [ResourceEntry("Views", Description = "word", LastModified = "2009/05/18", Value = "Views")]
    public string Views => this[nameof (Views)];

    /// <summary>Word: List</summary>
    [ResourceEntry("List", Description = "word", LastModified = "2009/05/18", Value = "List")]
    public string List => this[nameof (List)];

    /// <summary>Word: Grid</summary>
    [ResourceEntry("Grid", Description = "word", LastModified = "2009/05/18", Value = "Grid")]
    public string Grid => this[nameof (Grid)];

    /// <summary>Word: Boxes</summary>
    [ResourceEntry("Boxes", Description = "word", LastModified = "2009/05/18", Value = "Boxes")]
    public string Boxes => this[nameof (Boxes)];

    /// <summary>Word: Box</summary>
    [ResourceEntry("Box", Description = "word", LastModified = "2011/03/07", Value = "Box")]
    public string Box => this[nameof (Box)];

    /// <summary>phrase: Thumbnails view</summary>
    [ResourceEntry("ThumbnailsView", Description = "The tooltip of the 'Thumbnails view' state button in the toolbar.", LastModified = "2011/03/08", Value = "Thumbnails view")]
    public string ThumbnailsView => this[nameof (ThumbnailsView)];

    /// <summary>phrase: List view</summary>
    [ResourceEntry("ListView", Description = "The tooltip of the 'List view' state button in the toolbar.", LastModified = "2011/03/08", Value = "List view")]
    public string ListView => this[nameof (ListView)];

    /// <summary>Word: Alphabetically</summary>
    [ResourceEntry("Alphabetically", Description = "word", LastModified = "2009/05/18", Value = "Alphabetically")]
    public string Alphabetically => this[nameof (Alphabetically)];

    /// <summary>Word: Categorized</summary>
    [ResourceEntry("Categorized", Description = "word", LastModified = "2009/05/18", Value = "Categorized")]
    public string Categorized => this[nameof (Categorized)];

    /// <summary>Word: or</summary>
    [ResourceEntry("or", Description = "word", LastModified = "2009/05/18", Value = "or")]
    public string or => this[nameof (or)];

    /// <summary>Word: default</summary>
    [ResourceEntry("Default", Description = "word", LastModified = "2009/05/18", Value = "Default")]
    public string Default => this[nameof (Default)];

    /// <summary>Word: search</summary>
    [ResourceEntry("Search", Description = "word", LastModified = "2009/05/29", Value = "Search...")]
    public string Search => this[nameof (Search)];

    /// <summary>Word: search</summary>
    [ResourceEntry("SearchLabel", Description = "word", LastModified = "2010/10/27", Value = "Search")]
    public string SearchLabel => this[nameof (SearchLabel)];

    /// <summary>Phrase: Back to simple search</summary>
    [ResourceEntry("BackToSimpleSearch", Description = "Phrase", LastModified = "2010/05/14", Value = "Back to simple search")]
    public string BackToSimpleSearch => this[nameof (BackToSimpleSearch)];

    /// <summary>Phrase: AdvancedSearch</summary>
    [ResourceEntry("AdvancedSearch", Description = "Phrase", LastModified = "2010/05/14", Value = "Advanced Search")]
    public string AdvancedSearch => this[nameof (AdvancedSearch)];

    /// <summary>Phrase: Searching...</summary>
    [ResourceEntry("Searching", Description = "phrase", LastModified = "2010/05/12", Value = "Searching...")]
    public string Searching => this[nameof (Searching)];

    /// <summary>Phrase: Create new label</summary>
    [ResourceEntry("CreateNewLabel", Description = "phrase", LastModified = "2009/05/29", Value = "Create a label")]
    public string CreateNewLabel => this[nameof (CreateNewLabel)];

    /// <summary>Phrase: Save Changes Label</summary>
    [ResourceEntry("SaveChangesLabel", Description = "phrase", LastModified = "2009/05/29", Value = "Save changes")]
    public string SaveChangesLabel => this[nameof (SaveChangesLabel)];

    /// <summary>Phrase: Delete Label</summary>
    [ResourceEntry("DeleteLabel", Description = "phrase", LastModified = "2009/08/06", Value = "Delete")]
    public string DeleteLabel => this[nameof (DeleteLabel)];

    /// <summary>Phrase: Sort by</summary>
    [ResourceEntry("SortBy", Description = "phrase", LastModified = "2009/05/29", Value = "Sort by")]
    public string SortBy => this[nameof (SortBy)];

    /// <summary>Phrase: Default Sorting</summary>
    [ResourceEntry("DefaultSorting", Description = "phrase: Default Sorting", LastModified = "2011/06/20", Value = "Default Sorting")]
    public string DefaultSorting => this[nameof (DefaultSorting)];

    /// <summary>Phrase: Assign to role...</summary>
    [ResourceEntry("AssignToRole", Description = "phrase", LastModified = "2009/05/29", Value = "Assign to role...")]
    public string AssignToRole => this[nameof (AssignToRole)];

    /// <summary>Phrase: Unassign from role...</summary>
    [ResourceEntry("UnassignFromRole", Description = "phrase", LastModified = "2009/05/29", Value = "Unassign from role...")]
    public string UnassignFromRole => this[nameof (UnassignFromRole)];

    /// <summary>Phrase: Find Users...</summary>
    [ResourceEntry("FindUsers", Description = "phrase", LastModified = "2009/05/29", Value = "Find Users...")]
    public string FindUsers => this[nameof (FindUsers)];

    /// <summary>Phrase: Close Search</summary>
    [ResourceEntry("CloseSearch", Description = "phrase", LastModified = "2009/05/29", Value = "Close search")]
    public string CloseSearch => this[nameof (CloseSearch)];

    /// <summary>Word: Close</summary>
    [ResourceEntry("Close", Description = "word", LastModified = "2010/01/14", Value = "Close")]
    public string Close => this[nameof (Close)];

    /// <summary>Phrase: Assign users to</summary>
    [ResourceEntry("AssignUsersTo", Description = "phrase", LastModified = "2009/05/29", Value = "Assign users to")]
    public string AssignUsersTo => this[nameof (AssignUsersTo)];

    /// <summary>Phrase: Unassign users from</summary>
    [ResourceEntry("UnassignUsersFrom", Description = "phrase", LastModified = "2009/05/29", Value = "Unassign users from")]
    public string UnassignUsersFrom => this[nameof (UnassignUsersFrom)];

    /// <summary>Word: Assign</summary>
    [ResourceEntry("Assign", Description = "word", LastModified = "2009/05/29", Value = "Assign")]
    public string Assign => this[nameof (Assign)];

    /// <summary>Word: Unassign</summary>
    [ResourceEntry("Unassign", Description = "word", LastModified = "2009/05/29", Value = "Unassign")]
    public string Unassign => this[nameof (Unassign)];

    /// <summary>Unknown</summary>
    [ResourceEntry("Unknown", Description = "word", LastModified = "2011/03/29", Value = "Unknown")]
    public string Unknown => this[nameof (Unknown)];

    /// <summary>Word: Sort</summary>
    [ResourceEntry("Sort", Description = "word", LastModified = "2009/05/29", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>Word: alphabet</summary>
    [ResourceEntry("Alphabet", Description = "word", LastModified = "2009/05/29", Value = "Alphabet")]
    public string Alphabet => this[nameof (Alphabet)];

    /// <summary>Word: date</summary>
    [ResourceEntry("Date", Description = "word", LastModified = "2009/05/29", Value = "Date")]
    public string Date => this[nameof (Date)];

    /// <summary>Word: Date / Owner</summary>
    [ResourceEntry("DateOwner", Description = "word", LastModified = "2010/09/30", Value = "Date / Owner")]
    public string DateOwner => this[nameof (DateOwner)];

    /// <summary>Phrase: By date</summary>
    [ResourceEntry("ByDate", Description = "word", LastModified = "2009/05/29", Value = "By date")]
    public string ByDate => this[nameof (ByDate)];

    /// <summary>Phrase: Edit all labels listed below</summary>
    [ResourceEntry("EditAllLabelsListedBelow", Description = "phrase", LastModified = "2009/05/29", Value = "Edit all labels listed below")]
    public string EditAllLabelsListedBelow => this[nameof (EditAllLabelsListedBelow)];

    /// <summary>Word: finished</summary>
    [ResourceEntry("Finished", Description = "word", LastModified = "2009/05/29", Value = "Finished")]
    public string Finished => this[nameof (Finished)];

    /// <summary>Phrase: Close this batch editor</summary>
    [ResourceEntry("CloseThisBatchEditor", Description = "phrase", LastModified = "2009/05/29", Value = "Close this batch editor")]
    public string CloseThisBatchEditor => this[nameof (CloseThisBatchEditor)];

    /// <summary>Phrase: This label is used in</summary>
    [ResourceEntry("ThisLabelIsUsedIn", Description = "Phrase", LastModified = "2009/05/18", Value = "This label is used in")]
    public string ThisLabelIsUsedIn => this[nameof (ThisLabelIsUsedIn)];

    /// <summary>Word: description</summary>
    [ResourceEntry("Description", Description = "Word", LastModified = "2009/05/18", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Word: Role</summary>
    [ResourceEntry("Role", Description = "Role label.", LastModified = "2009/07/02", Value = "Role")]
    public string Role => this[nameof (Role)];

    /// <summary>Phrase: How to use in aspx or ascx file?</summary>
    [ResourceEntry("HowToUseInAspxOrAscxFile", Description = "Phrase", LastModified = "2009/05/18", Value = "How to use in aspx or ascx file?")]
    public string HowToUseInAspxOrAscxFile => this[nameof (HowToUseInAspxOrAscxFile)];

    /// <summary>Phrase: Use this key.</summary>
    [ResourceEntry("UseThisKey", Description = "Phrase", LastModified = "2009/05/18", Value = "Use this key")]
    public string UseThisKey => this[nameof (UseThisKey)];

    /// <summary>Phrase: "Plain text"</summary>
    [ResourceEntry("ConfigPlainTextOption", Description = "label", LastModified = "2018/03/20", Value = "Plain text")]
    public string ConfigPlainTextOption => this[nameof (ConfigPlainTextOption)];

    /// <summary>Phrase: "Encrypted"</summary>
    [ResourceEntry("ConfigEncryptOption", Description = "label", LastModified = "2018/03/20", Value = "Encrypted")]
    public string ConfigEncryptOption => this[nameof (ConfigEncryptOption)];

    /// <summary>Phrase: "Key to app setting"</summary>
    [ResourceEntry("ConfigAppSettingKeyOption", Description = "label", LastModified = "2018/03/20", Value = "Key to app setting")]
    public string ConfigAppSettingKeyOption => this[nameof (ConfigAppSettingKeyOption)];

    /// <summary>Phrase: "Key to environment variables"</summary>
    [ResourceEntry("ConfigEnvVariableKeyOption", Description = "label", LastModified = "2018/03/20", Value = "Key to environment variables")]
    public string ConfigEnvVariableKeyOption => this[nameof (ConfigEnvVariableKeyOption)];

    /// <summary>Phrase: "Show encryption options"</summary>
    [ResourceEntry("ShowEncryptionOptions", Description = "label", LastModified = "2018/03/20", Value = "Show encryption options")]
    public string ShowEncryptionOptions => this[nameof (ShowEncryptionOptions)];

    /// <summary>Phrase: "Hide encryption options"</summary>
    [ResourceEntry("HideEncryptionOptions", Description = "label", LastModified = "2018/03/20", Value = "Hide encryption options")]
    public string HideEncryptionOptions => this[nameof (HideEncryptionOptions)];

    /// <summary>Phrase: "Set default policy"</summary>
    [ResourceEntry("DefaultPolicy", Description = "label", LastModified = "2009/06/01", Value = "Default policy")]
    public string DefaultPolicy => this[nameof (DefaultPolicy)];

    /// <summary>Phrase: "Create new policy"</summary>
    [ResourceEntry("CreateNewPolicy", Description = "phrase", LastModified = "2009/06/01", Value = "Create new policy")]
    public string CreateNewPolicy => this[nameof (CreateNewPolicy)];

    /// <summary>Phrase: "Create new policy"</summary>
    [ResourceEntry("ShowPoliciesFor", Description = "label", LastModified = "2009/06/01", Value = "policies for:")]
    public string ShowPoliciesFor => this[nameof (ShowPoliciesFor)];

    /// <summary>Label: "CreateNew"</summary>
    [ResourceEntry("CreateNew", Description = "label", LastModified = "2009/06/01", Value = "Create new")]
    public string CreateNew => this[nameof (CreateNew)];

    /// <summary>Label: "Reorder"</summary>
    [ResourceEntry("Reorder", Description = "label", LastModified = "2009/06/01", Value = "Reorder")]
    public string Reorder => this[nameof (Reorder)];

    /// <summary>Label: "Reorder image"</summary>
    [ResourceEntry("ReorderImages", Description = "label", LastModified = "2020/02/13", Value = "Reorder images")]
    public string ReorderImages => this[nameof (ReorderImages)];

    /// <summary>Label: "Reorder documents"</summary>
    [ResourceEntry("ReorderDocuments", Description = "label", LastModified = "2020/06/17", Value = "Reorder documents")]
    public string ReorderDocuments => this[nameof (ReorderDocuments)];

    /// <summary>Label: "Reorder videos"</summary>
    [ResourceEntry("ReorderVideos", Description = "label", LastModified = "2020/06/17", Value = "Reorder videos")]
    public string ReorderVideos => this[nameof (ReorderVideos)];

    /// <summary>Label: "Reorder"</summary>
    [ResourceEntry("DoneReordering", Description = "label", LastModified = "2009/06/01", Value = "Done reordering")]
    public string DoneReordering => this[nameof (DoneReordering)];

    /// <summary>Label: Which image to upload?</summary>
    [ResourceEntry("WhichImageToUpload", Description = "label", LastModified = "2010/05/19", Value = "Which image to upload?")]
    public string WhichImageToUpload => this[nameof (WhichImageToUpload)];

    /// <summary>Label: Where to store the uploaded image?</summary>
    [ResourceEntry("WhereToStoreTheUploadedImage", Description = "label", LastModified = "2010/05/19", Value = "Where to store the uploaded image?")]
    public string WhereToStoreTheUploadedImage => this[nameof (WhereToStoreTheUploadedImage)];

    /// <summary>
    /// phrase: Title <span class="sfNote">(also displayed as a tooltip by modern browsers)</span>
    /// </summary>
    [ResourceEntry("ImageTitleWithNote", Description = "phrase: Title <span class='sfNote'>(also displayed as a tooltip by modern browsers)</span>", LastModified = "2012/01/06", Value = "Title <span class='sfNote'>(also displayed as a tooltip by modern browsers)</span>")]
    public string ImageTitleWithNote => this[nameof (ImageTitleWithNote)];

    /// <summary>
    /// phrase: Alternative text <span class="sfNote">(for visually impaired people and search engines)</span>
    /// </summary>
    [ResourceEntry("AlternativeTextWithNote", Description = "phrase: Alternative text <span class='sfNote'>(for visually impaired people and search engines)</span>", LastModified = "2012/01/06", Value = "Alternative text <span class='sfNote'>(for visually impaired people and search engines)</span>")]
    public string AlternativeTextWithNote => this[nameof (AlternativeTextWithNote)];

    /// <summary>Label: What title to display for this document?</summary>
    [ResourceEntry("WhatTitleToDipsplayForThisDocument", Description = "label", LastModified = "2010/08/18", Value = "What title to display for this document?")]
    public string WhatTitleToDipsplayForThisDocument => this[nameof (WhatTitleToDipsplayForThisDocument)];

    /// <summary>Label: Resizing options</summary>
    [ResourceEntry("ResizingOptions", Description = "label", LastModified = "2010/05/19", Value = "Resizing options")]
    public string ResizingOptions => this[nameof (ResizingOptions)];

    /// <summary>Label: Don't resize, show original {0}</summary>
    [ResourceEntry("ShowOriginal", Description = "label", LastModified = "2010/05/19", Value = "Don't resize, show original {0}")]
    public string ShowOriginal => this[nameof (ShowOriginal)];

    /// <summary>Label: Resize the {0} width to...</summary>
    [ResourceEntry("ResizeWidthTo", Description = "label", LastModified = "2010/05/19", Value = "Resize the {0} width to...")]
    public string ResizeWidthTo => this[nameof (ResizeWidthTo)];

    /// <summary>Label: Thumbnail: 100 px width</summary>
    [ResourceEntry("ResizeSizeThumbnail", Description = "label", LastModified = "2010/05/19", Value = "Thumbnail: 100 px width")]
    public string ResizeSizeThumbnail => this[nameof (ResizeSizeThumbnail)];

    /// <summary>Label: Small: 240 px width</summary>
    [ResourceEntry("ResizeSizeSmall", Description = "label", LastModified = "2010/05/19", Value = "Small: 240 px width")]
    public string ResizeSizeSmall => this[nameof (ResizeSizeSmall)];

    /// <summary>Label: Medium: 500 px width</summary>
    [ResourceEntry("ResizeSizeMedium", Description = "label", LastModified = "2010/05/19", Value = "Medium: 500 px width")]
    public string ResizeSizeMedium => this[nameof (ResizeSizeMedium)];

    /// <summary>Label: Large: 800 px width</summary>
    [ResourceEntry("ResizeSizeLarge", Description = "label", LastModified = "2010/05/19", Value = "Large: 800 px width")]
    public string ResizeSizeLarge => this[nameof (ResizeSizeLarge)];

    /// <summary>Label: Extra-large: 1024 px width</summary>
    [ResourceEntry("ResizeSizeExtraLarge", Description = "label", LastModified = "2010/05/19", Value = "Extra-large: 1024 px width")]
    public string ResizeSizeExtraLarge => this[nameof (ResizeSizeExtraLarge)];

    /// <summary>Label: Filter by type</summary>
    [ResourceEntry("FilterByResourceEntryType", Description = "label", LastModified = "2010/10/18", Value = "Filter by type")]
    public string FilterByResourceEntryType => this[nameof (FilterByResourceEntryType)];

    /// <summary>Label: Open Single item in...</summary>
    [ResourceEntry("OpenSingleItemInDotDotDot", Description = "label", LastModified = "2011/03/16", Value = "Open single item in...")]
    public string OpenSingleItemInDotDotDot => this[nameof (OpenSingleItemInDotDotDot)];

    /// <summary>Action: "Filter"</summary>
    [ResourceEntry("Filter", Description = "The text to be shown in the buton inside the DateFilteringWidget custom range section.", LastModified = "2010/06/01", Value = "Filter")]
    public string Filter => this[nameof (Filter)];

    /// <summary>Phrase: "SelectCriteria"</summary>
    [ResourceEntry("SelectCriteria", Description = "The text to be shown for select option custom sorting expression dialog.", LastModified = "2010/05/13", Value = "- Select criteria -")]
    public string SelectCriteria => this[nameof (SelectCriteria)];

    /// <summary>Phrase: "custom Range"</summary>
    [ResourceEntry("CustomRange", Description = "The text of the DateFilteringWidget show-custom-range button.", LastModified = "2010/05/31", Value = "Custom range...")]
    public string CustomRange => this[nameof (CustomRange)];

    /// <summary>Phrase: "All the time"</summary>
    [ResourceEntry("AllTheTime", Description = "The text of the DateFilteringWidget clear date filter button.", LastModified = "2010/06/02", Value = "Any time")]
    public string AllTheTime => this[nameof (AllTheTime)];

    /// <summary>Phrase: Last 1 day</summary>
    [ResourceEntry("LastOneDay", Description = "Phrase: Last 1 day", LastModified = "2010/05/31", Value = "Last <strong>1 day</strong>")]
    public string LastOneDay => this[nameof (LastOneDay)];

    /// <summary>Phrase: Last 3 days</summary>
    [ResourceEntry("LastThreeDays", Description = "Phrase: Last 3 days", LastModified = "2010/05/31", Value = "Last <strong>3 days</strong>")]
    public string LastThreeDays => this[nameof (LastThreeDays)];

    /// <summary>Phrase: Last 1 week</summary>
    [ResourceEntry("LastOneWeek", Description = "Phrase: Last 1 week", LastModified = "2010/05/31", Value = "Last <strong>1 week</strong>")]
    public string LastOneWeek => this[nameof (LastOneWeek)];

    /// <summary>Phrase: Last 1 month</summary>
    [ResourceEntry("LastOneMonth", Description = "Phrase: Last 1 month", LastModified = "2010/06/03", Value = "Last <strong>1 month</strong>")]
    public string LastOneMonth => this[nameof (LastOneMonth)];

    /// <summary>Phrase: Last 6 months</summary>
    [ResourceEntry("LastSixMonths", Description = "Phrase: Last 6 months", LastModified = "2010/06/03", Value = "Last <strong>6 months</strong>")]
    public string LastSixMonths => this[nameof (LastSixMonths)];

    /// <summary>word: last</summary>
    [ResourceEntry("Last", Description = "word: last", LastModified = "2012/01/20", Value = "last")]
    public string Last => this[nameof (Last)];

    /// <summary>word: Last</summary>
    [ResourceEntry("LastTitleCase", Description = "word: Last", LastModified = "2012/01/20", Value = "Last")]
    public string LastTitleCase => this[nameof (LastTitleCase)];

    /// <summary>Phrase: Last 1 year</summary>
    [ResourceEntry("LastOneYear", Description = "Phrase: Last 1 year", LastModified = "2010/06/03", Value = "Last <strong>1 year</strong>")]
    public string LastOneYear => this[nameof (LastOneYear)];

    /// <summary>Phrase: Last 2 years</summary>
    [ResourceEntry("LastTwoYears", Description = "Phrase: Last 2 years", LastModified = "2010/06/03", Value = "Last <strong>2 years</strong>")]
    public string LastTwoYears => this[nameof (LastTwoYears)];

    /// <summary>Phrase: Last 5 years</summary>
    [ResourceEntry("LastFiveYears", Description = "Phrase: Last 5 years", LastModified = "2010/06/03", Value = "Last <strong>5 years</strong>")]
    public string LastFiveYears => this[nameof (LastFiveYears)];

    /// <summary>Phrase: with the same layout as the list page</summary>
    [ResourceEntry("WithTheSameLayoutAsTheListPage", Description = "Phrase: with the same layout as the list page", LastModified = "2011/08/01", Value = "with the same layout as the list page")]
    public string WithTheSameLayoutAsTheListPage => this[nameof (WithTheSameLayoutAsTheListPage)];

    /// <summary>Phrase: Detail templates</summary>
    [ResourceEntry("DetailTemplates", Description = "Phrase: Detail templates", LastModified = "2011/08/01", Value = "Detail templates")]
    public string DetailTemplates => this[nameof (DetailTemplates)];

    /// <summary>Phrase: Selected existing page ...</summary>
    [ResourceEntry("SelectedExistingPage", Description = "Phrase: Selected existing page ...", LastModified = "2011/08/01", Value = "Selected existing page ...")]
    public string SelectedExistingPage => this[nameof (SelectedExistingPage)];

    /// <summary>Phrase: Auto-generated page</summary>
    [ResourceEntry("AutoGeneratedPage", Description = "Phrase: Auto-generated page", LastModified = "2011/08/01", Value = "Auto-generated page")]
    public string AutoGeneratedPage => this[nameof (AutoGeneratedPage)];

    /// <summary>
    /// This text appears on the login widget for the backend login for the Sitefinity evaluation version.
    /// </summary>
    /// <value>To enter the demo you can login as a default user:</value>
    [ResourceEntry("LoginDemoCredentialsHint", Description = "This text appears on the login widget for the backend login for the Sitefinity evaluation version.", LastModified = "2012/11/01", Value = "To enter the demo you can login as a default user:")]
    public string LoginDemoCredentialsHint => this[nameof (LoginDemoCredentialsHint)];

    /// <summary>
    /// This text appears on the login widget for the backend login for the Sitefinity evaluation version.
    /// </summary>
    /// <value>What are the default username and password?</value>
    [ResourceEntry("LoginDemoCredentialsQuestion", Description = "This text appears on the login widget for the backend login for the Sitefinity evaluation version.", LastModified = "2012/11/01", Value = "What are the default username and password?")]
    public string LoginDemoCredentialsQuestion => this[nameof (LoginDemoCredentialsQuestion)];

    /// <summary>Phrase: From the currently open {0}</summary>
    /// <value>From the currently open {0}</value>
    [ResourceEntry("FilterByCurrentlyOpenParent", Description = "Phrase: From the currently open {0}", LastModified = "2014/06/11", Value = "From the currently open {0}")]
    public string FilterByCurrentlyOpenParent => this[nameof (FilterByCurrentlyOpenParent)];

    /// <summary>Phrase: From selected {0} only...</summary>
    /// <value>From selected {0} only...</value>
    [ResourceEntry("FilterBySelectedParents", Description = "Phrase: From selected {0} only...", LastModified = "2014/06/11", Value = "From selected {0} only...")]
    public string FilterBySelectedParents => this[nameof (FilterBySelectedParents)];

    /// <summary>Phrase: From all {0}</summary>
    /// <value>From all {0}</value>
    [ResourceEntry("FilterByAllParents", Description = "Phrase: From all {0}", LastModified = "2014/06/20", Value = "From all {0}")]
    public string FilterByAllParents => this[nameof (FilterByAllParents)];

    /// <summary>Prhase: From the currently open parent</summary>
    /// <value>From the currently open parent</value>
    [ResourceEntry("DefaultFilterByCurrentlyOpenParent", Description = "Prhase: From the currently open parent", LastModified = "2014/06/20", Value = "From the currently open parent")]
    public string DefaultFilterByCurrentlyOpenParent => this[nameof (DefaultFilterByCurrentlyOpenParent)];

    /// <summary>Phrase: From selected parents only...</summary>
    /// <value>Frome selected parents only...</value>
    [ResourceEntry("DefaultFilterBySelectedParents", Description = "Phrase: From selected parents only...", LastModified = "2014/06/20", Value = "From selected parents only...")]
    public string DefaultFilterBySelectedParents => this[nameof (DefaultFilterBySelectedParents)];

    /// <summary>Phrase: From all parents</summary>
    /// <value>From all parents</value>
    [ResourceEntry("DefaultFilterByAllParents", Description = "Phrase: From all parents", LastModified = "2014/06/20", Value = "From all parents")]
    public string DefaultFilterByAllParents => this[nameof (DefaultFilterByAllParents)];

    /// <summary>Phrase: One particular item only...</summary>
    /// <value>One particular item only...</value>
    [ResourceEntry("DefaultOneParticularItem", Description = "Phrase: One particular item only...", LastModified = "2014/06/20", Value = "One particular item only...")]
    public string DefaultOneParticularItem => this[nameof (DefaultOneParticularItem)];

    /// <summary>Phrase: All published items</summary>
    /// <value>All published items</value>
    [ResourceEntry("DefaultAllPublishedItems", Description = "Phrase: All published items", LastModified = "2014/06/20", Value = "All published items")]
    public string DefaultAllPublishedItems => this[nameof (DefaultAllPublishedItems)];

    /// <summary>Phrase: Selection of items</summary>
    /// <value>Selection of items</value>
    [ResourceEntry("DefaultSelectionOfItems", Description = "Phrase: Selection of items", LastModified = "2014/06/20", Value = "Selection of items")]
    public string DefaultSelectionOfItems => this[nameof (DefaultSelectionOfItems)];

    /// <summary>Phrase: Registration Confirmation Email</summary>
    [ResourceEntry("RegistrationConfirmationEmail", Description = "Phrase: Default Registration Confirmation message for user registration.", LastModified = "2015/06/11", Value = "Welcome to <%\\s*SiteName\\s*%>.<br /><br />Please follow this link in order to confirm your registration:<br /><a href=\"<%\\s*ConfirmationUrl\\s*%>\"><%\\s*ConfirmationUrl\\s*%></a><br /><br />Thank you,<br />The <%\\s*SiteName\\s*%> Team")]
    public string RegistrationConfirmationEmail => this[nameof (RegistrationConfirmationEmail)];

    /// <summary>Phrase: Registration Success Email.</summary>
    [ResourceEntry("RegistrationSuccessEmail", Description = "Phrase: Default Registration Success message for user registration.", LastModified = "2015/06/11", Value = "Thank you for registering on <%\\s*SiteName\\s*%><br />Your username is: <%\\s*UserName\\s*%><br /><br />Enjoy your membership,<br />The <%\\s*SiteName\\s*%> Team")]
    public string RegistrationSuccessEmail => this[nameof (RegistrationSuccessEmail)];

    /// <summary>Button: "Add a data field"</summary>
    [ResourceEntry("AddDataField", Description = "Button: Add a data field.", LastModified = "2010/09/13", Value = "Add a data field")]
    public string AddNewField => this["AddDataField"];

    /// <summary>Button: "Update data field"</summary>
    [ResourceEntry("UpdateDataField", Description = "Button: Update data field.", LastModified = "2010/09/13", Value = "Update data field")]
    public string UpdateDataField => this[nameof (UpdateDataField)];

    /// <summary>Button: "Delete data field"</summary>
    [ResourceEntry("DeleteDataField", Description = "Button: Delete data field.", LastModified = "2010/09/13", Value = "Delete data field")]
    public string DeleteNewField => this[nameof (DeleteNewField)];

    /// <summary>Label: Title</summary>
    [ResourceEntry("MetaFieldTitle", Description = "Label: Title", LastModified = "2010/10/01", Value = "Title")]
    public string MetaFieldTitle => this[nameof (MetaFieldTitle)];

    /// <summary>Label: Name</summary>
    [ResourceEntry("MetaFieldName", Description = "Label: Name", LastModified = "2010/09/13", Value = "Name")]
    public string MetaFieldName => this[nameof (MetaFieldName)];

    /// <summary>Label: Type</summary>
    [ResourceEntry("MetaFieldType", Description = "Label: Type", LastModified = "2010/09/13", Value = "Type")]
    public string MetaFieldType => this[nameof (MetaFieldType)];

    /// <summary>Label: DB Type</summary>
    [ResourceEntry("MetaFieldDBType", Description = "Label: DB Type", LastModified = "2010/09/13", Value = "DB Type")]
    public string MetaFieldDBType => this[nameof (MetaFieldDBType)];

    /// <summary>Label: SQL DB Type</summary>
    [ResourceEntry("MetaFieldMsSQLDBType", Description = "Label: SQL DB Type", LastModified = "2010/09/13", Value = "SQL DB Type")]
    public string MetaFieldMsSQLDBType => this[nameof (MetaFieldMsSQLDBType)];

    /// <summary>Label: Max length</summary>
    [ResourceEntry("MetaFieldMaxLength", Description = "Label: Max length", LastModified = "2010/09/13", Value = "Max length")]
    public string MetaFieldMaxLength => this[nameof (MetaFieldMaxLength)];

    /// <summary>Label: Default value</summary>
    [ResourceEntry("MetaFieldDefaultValue", Description = "Label: Default value", LastModified = "2010/09/13", Value = "Default value")]
    public string MetaFieldDefaultValue => this[nameof (MetaFieldDefaultValue)];

    /// <summary>Label: Precision</summary>
    [ResourceEntry("MetaFieldPrecision", Description = "Label: Precision", LastModified = "2010/09/13", Value = "Precision")]
    public string MetaFieldPrecision => this[nameof (MetaFieldPrecision)];

    /// <summary>Label: Classification provider</summary>
    [ResourceEntry("MetaFieldTaxonomyProvider", Description = "Label: Classification provider", LastModified = "2010/09/13", Value = "Classification provider")]
    public string MetaFieldTaxonomyProvider => this[nameof (MetaFieldTaxonomyProvider)];

    /// <summary>Label: Classification</summary>
    [ResourceEntry("MetaFieldTaxonomy", Description = "Label: Classification", LastModified = "2010/09/13", Value = "Classification")]
    public string MetaFieldTaxonomy => this[nameof (MetaFieldTaxonomy)];

    /// <summary>Label: Allow multiple classifications selection</summary>
    [ResourceEntry("MetaFieldAllowMultipleTaxonomies", Description = "Label: Allow multiple classifications selection", LastModified = "2010/09/13", Value = "Allow multiple classifications selection")]
    public string MetaFieldAllowMultipleTaxonomies => this[nameof (MetaFieldAllowMultipleTaxonomies)];

    /// <summary>Meta type title: Unique Identifier</summary>
    [ResourceEntry("MetaFieldTypeGuid", Description = "Meta type title: Unique identifier", LastModified = "2010/10/13", Value = "Unique identifier")]
    public string MetaFieldTypeGuid => this[nameof (MetaFieldTypeGuid)];

    /// <summary>Meta type title: Localized text</summary>
    [ResourceEntry("MetaFieldTypeLString", Description = "Meta type title: Localized text", LastModified = "2010/09/13", Value = "Localized text")]
    public string MetaFieldTypeLString => this[nameof (MetaFieldTypeLString)];

    /// <summary>Meta type title: Text</summary>
    [ResourceEntry("MetaFieldTypeString", Description = "Meta type title: Text", LastModified = "2010/09/13", Value = "Text")]
    public string MetaFieldTypeString => this[nameof (MetaFieldTypeString)];

    /// <summary>Meta type title: Integer</summary>
    [ResourceEntry("MetaFieldTypeInt", Description = "Meta type title: Integer", LastModified = "2010/09/13", Value = "Integer")]
    public string MetaFieldTypeInt => this[nameof (MetaFieldTypeInt)];

    /// <summary>Meta type title: Decmial</summary>
    [ResourceEntry("MetaFieldTypeDecimal", Description = "Meta type title: Decimal", LastModified = "2010/09/13", Value = "Decimal")]
    public string MetaFieldTypeDecimal => this[nameof (MetaFieldTypeDecimal)];

    /// <summary>Meta type title: Date</summary>
    [ResourceEntry("MetaFieldTypeDate", Description = "Meta type title: Date", LastModified = "2010/09/13", Value = "Date")]
    public string MetaFieldTypeDate => this[nameof (MetaFieldTypeDate)];

    /// <summary>Meta type title: Boolean</summary>
    [ResourceEntry("MetaFieldTypeBoolean", Description = "Meta type title: Boolean", LastModified = "2010/09/13", Value = "Yes/No")]
    public string MetaFieldTypeBoolean => this[nameof (MetaFieldTypeBoolean)];

    /// <summary>Meta type title: Taxonomy</summary>
    [ResourceEntry("MetaFieldTypeTaxonomy", Description = "Meta type title: Taxonomy", LastModified = "2010/09/13", Value = "Classification")]
    public string MetaFieldTypeTaxonomy => this[nameof (MetaFieldTypeTaxonomy)];

    /// <summary>
    /// Meta type error message: A field by this name is already defined for this type.
    /// </summary>
    [ResourceEntry("MetaFieldDuplicateFieldName", Description = "Meta type error message: A field by this name is already defined for this type.", LastModified = "2010/09/13", Value = "A field by this name is already defined for this type.")]
    public string MetaFieldDuplicateFieldName => this[nameof (MetaFieldDuplicateFieldName)];

    /// <summary>
    /// Publishing Structure Field Control: Edit structure link
    /// </summary>
    [ResourceEntry("EditStructure", Description = "Publishing Structure Field Control: Edit structure link", LastModified = "2010/10/12", Value = "<span class='sfLinkBtnIn'>Change Data structure...</span>")]
    public string EditStructure => this[nameof (EditStructure)];

    /// <summary>Label: Data</summary>
    [ResourceEntry("Data", Description = "Data", LastModified = "2011/07/04", Value = "Data")]
    public string Data => this[nameof (Data)];

    /// <summary>
    /// Publishing Structure Field Control: {0} Fields defined
    /// </summary>
    [ResourceEntry("NumFieldsDefined", Description = "Publishing Structure Field Control: {0} Fields defined", LastModified = "2010/10/12", Value = "{0} Fields defined")]
    public string NumFieldsDefined => this[nameof (NumFieldsDefined)];

    /// <summary>
    /// Validation error: No fields defined for this publishing point
    /// </summary>
    [ResourceEntry("NoFieldsDefinedForThisPublishingPoint", Description = "Validation error: No fields defined for this publishing point", LastModified = "2010/10/12", Value = "No fields defined for this publishing point")]
    public string NoFieldsDefinedForThisPublishingPoint => this[nameof (NoFieldsDefinedForThisPublishingPoint)];

    /// <summary>Install</summary>
    [ResourceEntry("ActionInstall", Description = "The text to be shown in actions menu item install", LastModified = "2012/06/29", Value = "Install")]
    public string ActionInstall => this[nameof (ActionInstall)];

    /// <summary>Uninstall</summary>
    [ResourceEntry("ActionUninstall", Description = "The text to be shown in actions menu item uninstall", LastModified = "2012/06/29", Value = "Uninstall")]
    public string ActionUninstall => this[nameof (ActionUninstall)];

    /// <summary>Activate</summary>
    [ResourceEntry("ActionActivate", Description = "The text to be shown in actions menu item Activate", LastModified = "2012/06/29", Value = "Activate")]
    public string ActionActivate => this[nameof (ActionActivate)];

    /// <summary>Deactivate</summary>
    [ResourceEntry("ActionDeactivate", Description = "The text to be shown in actions menu item Deactivate", LastModified = "2012/06/29", Value = "Deactivate")]
    public string ActionDeactivate => this[nameof (ActionDeactivate)];

    /// <summary>Delete</summary>
    [ResourceEntry("ActionDelete", Description = "The text to be shown in actions menu item Delete", LastModified = "2012/06/29", Value = "Delete")]
    public string ActionDelete => this[nameof (ActionDelete)];

    /// <summary>Install a module</summary>
    [ResourceEntry("InstallModule", Description = "The text to be shown for installing a module", LastModified = "2012/06/13", Value = "Install a module")]
    public string InstallModule => this[nameof (InstallModule)];

    /// <summary>Uninstall this module</summary>
    [ResourceEntry("UninstallModule", Description = "The text to be shown for uninstalling a module", LastModified = "2012/06/26", Value = "Uninstall this module")]
    public string UninstallModule => this[nameof (UninstallModule)];

    /// <summary>Are you sure you want to uninstall this module?</summary>
    [ResourceEntry("UninstallModuleTitle", Description = "The text to be shown for uninstalling a module", LastModified = "2012/06/26", Value = "Are you sure you want to uninstall this module?")]
    public string UninstallModuleTitle => this[nameof (UninstallModuleTitle)];

    /// <summary>
    /// The content, settings and widgets will be deleted. The module can be installed later, but content and settings will be lost.
    /// </summary>
    [ResourceEntry("UninstallModuleMessage", Description = "The text message to be shown for uninstalling a module", LastModified = "2012/07/02", Value = "The content, settings and widgets will be deleted. The module can be installed later, but content and settings will be lost.")]
    public string UninstallModuleMessage => this[nameof (UninstallModuleMessage)];

    /// <summary>Deactivate this module</summary>
    [ResourceEntry("DeactivateModule", Description = "The text to be shown for deactivating a module", LastModified = "2012/06/26", Value = "Deactivate this module")]
    public string DeactivateModule => this[nameof (DeactivateModule)];

    /// <summary>Are you sure you want to deactivate this module?</summary>
    [ResourceEntry("DeactivateModuleTitle", Description = "The text to be shown for deactivating a module", LastModified = "2012/06/26", Value = "Are you sure you want to deactivate this module?")]
    public string DeactivateModuleTitle => this[nameof (DeactivateModuleTitle)];

    /// <summary>
    /// Deactivation may prevent some of the modules from functioning properly.
    /// </summary>
    [ResourceEntry("DeactivateSystemModuleWarningMessage", Description = "The text message to be shown for deactivating a module", LastModified = "2012/07/02", Value = "Deactivation may prevent some of the modules from functioning properly.")]
    public string DeactivateSystemModuleWarningMessage => this[nameof (DeactivateSystemModuleWarningMessage)];

    /// <summary>
    /// The content and settings will be kept, but hidden. Module widgets will not be active. The module can be activated later.
    /// </summary>
    [ResourceEntry("DeactivateModuleMessage", Description = "The text message to be shown for deactivating a module", LastModified = "2012/06/26", Value = "The content and settings will be kept, but hidden. Module widgets will not be active. The module can be activated later.")]
    public string DeactivateModuleMessage => this[nameof (DeactivateModuleMessage)];

    /// <summary>Delete this module</summary>
    [ResourceEntry("DeleteModule", Description = "The text to be shown for deleting a module", LastModified = "2012/06/26", Value = "Delete this module")]
    public string DeleteModule => this[nameof (DeleteModule)];

    /// <summary>Are you sure you want to delete this module?</summary>
    [ResourceEntry("DeleteModuleTitle", Description = "The text to be shown for deleting a module", LastModified = "2012/06/26", Value = "Are you sure you want to delete this module?")]
    public string DeleteModuleTitle => this[nameof (DeleteModuleTitle)];

    /// <summary>
    /// Delete this module from the list of uninstalled modules.
    /// </summary>
    [ResourceEntry("DeleteModuleMessage", Description = "The text message to be shown for deleting a module", LastModified = "2012/06/26", Value = "Delete this module from the list of uninstalled modules.")]
    public string DeleteModuleMessage => this[nameof (DeleteModuleMessage)];

    /// <summary>
    /// Module with key: {0} does not exist in the configuration.
    /// </summary>
    [ResourceEntry("ModuleDoesNotExist", Description = "The text message to be shown for deleting a module", LastModified = "2012/07/02", Value = "Module with key: {0} does not exist in the configuration.")]
    public string ModuleDoesNotExist => this[nameof (ModuleDoesNotExist)];

    /// <summary>Search...</summary>
    [ResourceEntry("SearchModule", Description = "The text to be shown for searching a module", LastModified = "2012/06/14", Value = "Search...")]
    public string SearchModule => this[nameof (SearchModule)];

    /// <summary>Modules</summary>
    [ResourceEntry("ModulesManagementGridModulesHeader", Description = "The text to be shown in the grid for modules column header ", LastModified = "2012/06/14", Value = "Modules")]
    public string ModulesManagementGridModulesHeader => this[nameof (ModulesManagementGridModulesHeader)];

    /// <summary>Description</summary>
    [ResourceEntry("ModulesManagementGridDescriptionHeader", Description = "The text to be shown in the grid for description column header ", LastModified = "2012/06/14", Value = "Description")]
    public string ModulesManagementGridDescriptionHeader => this[nameof (ModulesManagementGridDescriptionHeader)];

    /// <summary>Actions</summary>
    [ResourceEntry("ModulesManagementGridActionsHeader", Description = "The text to be shown in the grid for actions column header ", LastModified = "2012/06/14", Value = "Actions")]
    public string ModulesManagementGridActionsHeader => this[nameof (ModulesManagementGridActionsHeader)];

    /// <summary>Built with Module builder</summary>
    [ResourceEntry("BuildWithModuleBuilder", Description = "The text message to be shown for dynamic modules in the grid", LastModified = "2012/07/02", Value = "Built with Module builder")]
    public string BuildWithModuleBuilder => this[nameof (BuildWithModuleBuilder)];

    /// <summary>Could not load type: {0}, please verify the name.</summary>
    [ResourceEntry("InstallModuleCouleNotLoadType", Description = "The text message to be shown when you try to install a module with type that can't be find", LastModified = "2012/07/02", Value = "Could not load type: {0}, please verify the name.")]
    public string InstallModuleCouleNotLoadType => this[nameof (InstallModuleCouleNotLoadType)];

    /// <summary>
    /// The given type: {0} should implement (directly or indirectly): {1}, but it does not.
    /// </summary>
    [ResourceEntry("InstallModuleTypeShouldDerrive", Description = "The text message to be shown when you try to install a module with type that does not derive from the expected one", LastModified = "2012/07/02", Value = "The given type: {0} should implement (directly or indirectly): {1}, but it does not.")]
    public string InstallModuleTypeShouldDerrive => this[nameof (InstallModuleTypeShouldDerrive)];

    /// <summary>
    /// The module with type: {0} has already been used in other module.
    /// </summary>
    [ResourceEntry("ModuleTypeAlreadyInUse", Description = "The text message to be shown when you try to install a module with type that has been used in other module already", LastModified = "2012/07/02", Value = "The module with type: {0} has already been used in other module.")]
    public string ModuleTypeAlreadyInUse => this[nameof (ModuleTypeAlreadyInUse)];

    /// <summary>Edit a module</summary>
    [ResourceEntry("EditModule", Description = "The text to be shown for editing a module", LastModified = "2012/06/26", Value = "Edit a module")]
    public string EditModule => this[nameof (EditModule)];

    /// <summary>Back to modules page</summary>
    [ResourceEntry("BackToModules", Description = "The text to be shown for editing a module", LastModified = "2012/06/26", Value = "Back to Modules")]
    public string BackToModules => this[nameof (BackToModules)];

    /// <summary>Start module</summary>
    [ResourceEntry("ModuleStartMode", Description = "The text to be shown for the start mode of a module", LastModified = "2012/06/26", Value = "Start module...")]
    public string ModuleStartMode => this[nameof (ModuleStartMode)];

    /// <summary>Start module when the module is called for first time</summary>
    [ResourceEntry("StartOnFirstCall", Description = "The text to be shown for the StartOnFirstCall start mode of a module", LastModified = "2012/06/26", Value = "When the module is opened for first time")]
    public string StartOnFirstCall => this[nameof (StartOnFirstCall)];

    /// <summary>
    /// Start module when the whole application is opened for first time
    /// </summary>
    [ResourceEntry("StartOnApplicationStart", Description = "The text to be shown for the StartOnApplicationStart start mode of a module", LastModified = "2012/06/26", Value = "When the whole application is opened for first time")]
    public string StartOnApplicationStart => this[nameof (StartOnApplicationStart)];

    /// <summary>Start module</summary>
    [ResourceEntry("DoNotStartModule", Description = "The text to be shown for the DoNotStartModule start mode of a module", LastModified = "2012/06/26", Value = "Do not start")]
    public string DoNotStartModule => this[nameof (DoNotStartModule)];

    /// <summary>Module name</summary>
    [ResourceEntry("ModuleNameTitle", Description = "The text to be shown for the name of a module", LastModified = "2012/06/26", Value = "Name")]
    public string ModuleNameTitle => this[nameof (ModuleNameTitle)];

    /// <summary>Module name example</summary>
    [ResourceEntry("ModuleNameExample", Description = "The text to be shown for the name example of a module", LastModified = "2012/06/26", Value = "Example: News")]
    public string ModuleNameExample => this[nameof (ModuleNameExample)];

    /// <summary>Module description</summary>
    [ResourceEntry("ModuleDescriptionTitle", Description = "The text to be shown for the description of a module", LastModified = "2012/06/26", Value = "Description")]
    public string ModuleDescriptionTitle => this[nameof (ModuleDescriptionTitle)];

    /// <summary>Module type</summary>
    [ResourceEntry("ModuleTypeTitle", Description = "The text to be shown for the type of a module", LastModified = "2012/06/26", Value = "Type")]
    public string ModuleTypeTitle => this[nameof (ModuleTypeTitle)];

    /// <summary>Module type</summary>
    [ResourceEntry("ModuleTypeExample", Description = "The text to be shown for the type example of a module", LastModified = "2012/06/26", Value = "Example: MyModuleProject.MyModule")]
    public string ModuleTypeExample => this[nameof (ModuleTypeExample)];

    /// <summary>
    /// The install operation is restricted by the used license.
    /// </summary>
    [ResourceEntry("LicenseNotGrantedInstallTitle", Description = "The text to be shown as a title when trying to install a module which is not included in the applied License.", LastModified = "2012/06/26", Value = "Module cannot be installed")]
    public string LicenseNotGrantedInstallTitle => this[nameof (LicenseNotGrantedInstallTitle)];

    /// <summary>
    /// Your license does not allow to install and use this module.
    /// </summary>
    [ResourceEntry("LicenseNotGrantedInstallMessage", Description = "The text to be shown as a main message when trying to install a module which is not included in the applied License.", LastModified = "2012/06/29", Value = "To be able to install it, you need to purchase a higher license.")]
    public string LicenseNotGrantedInstallMessage => this[nameof (LicenseNotGrantedInstallMessage)];

    /// <summary>Module cannot be activated</summary>
    [ResourceEntry("LicenseNotGrantedActivateTitle", Description = "The text to be shown as a title when trying to activate a module which is not included in the applied License.", LastModified = "2012/07/02", Value = "Module cannot be activated")]
    public string LicenseNotGrantedActivateTitle => this[nameof (LicenseNotGrantedActivateTitle)];

    /// <summary>
    /// Your license does not allow to activate and use this module.
    /// </summary>
    [ResourceEntry("LicenseNotGrantedActivateMessage", Description = "The text to be shown as a main message when trying to activate a module which is not included in the applied License.", LastModified = "2012/07/02", Value = "To be able to activate it, you need to purchase a higher license.")]
    public string LicenseNotGrantedActivateMessage => this[nameof (LicenseNotGrantedActivateMessage)];

    /// <summary>Module cannot be edited</summary>
    [ResourceEntry("LicenseNotGrantedEditTitle", Description = "The text to be shown as a title when trying to edit a module which is not included in the applied License.", LastModified = "2012/07/02", Value = "Module cannot be edited")]
    public string LicenseNotGrantedEditTitle => this[nameof (LicenseNotGrantedEditTitle)];

    /// <summary>
    /// Your license does not allow to edit and use this module.
    /// </summary>
    [ResourceEntry("LicenseNotGrantedEditMessage", Description = "The text to be shown as a main message when trying to edit a module which is not included in the applied License.", LastModified = "2012/07/02", Value = "To be able to edit it, you need to purchase a higher license.")]
    public string LicenseNotGrantedEditMessage => this[nameof (LicenseNotGrantedEditMessage)];

    /// <summary>Contact sitefinitysales@progress.com for more details</summary>
    [ResourceEntry("ContactSitefinitySalesMessage", Description = "The text to be shown when you want to give a reference to the Sitefinity sales department.", LastModified = "2012/07/02", Value = "Contact sitefinitysales@progress.com for more details")]
    public string ContactSitefinitySalesMessage => this[nameof (ContactSitefinitySalesMessage)];

    /// <summary>
    /// The license you are currently using does not support this module.
    /// </summary>
    [ResourceEntry("ModuleNotSupportedByLicenseMessage", Description = "The text to be shown when a module is not supported by the the applied license.", LastModified = "2012/07/02", Value = "The license you are currently using does not support this module.")]
    public string ModuleNotSupportedByLicenseMessage => this[nameof (ModuleNotSupportedByLicenseMessage)];

    /// <summary>Module type cannot be empty.</summary>
    [ResourceEntry("ModuleTypeCannotBeEmpty", Description = "phrase: Name cannot be empty.", LastModified = "2012/06/26", Value = "Type cannot be empty.")]
    public string ModuleTypeCannotBeEmpty => this[nameof (ModuleTypeCannotBeEmpty)];

    /// <summary>
    /// Activation / installation of some modules has failed. For more details check the failed modules bellow or see the application error log.
    /// </summary>
    [ResourceEntry("ModuleOperationError", Description = "Activation / installation of some modules has failed. For more details check the failed modules bellow or see the application error log.", LastModified = "2016/05/13", Value = "Activation / installation of some modules has failed. For more details check the failed modules bellow or see the application error log.")]
    public string ModuleOperationError => this[nameof (ModuleOperationError)];

    /// <summary>Not installed</summary>
    [ResourceEntry("NotInstalled", Description = "Not installed", LastModified = "2012/07/13", Value = "Not installed")]
    public string NotInstalled => this[nameof (NotInstalled)];

    /// <summary>Inactive</summary>
    [ResourceEntry("Inactive", Description = "Inactive", LastModified = "2012/07/13", Value = "Inactive")]
    public string Inactive => this[nameof (Inactive)];

    /// <summary>Active</summary>
    [ResourceEntry("Active", Description = "Active", LastModified = "2012/07/13", Value = "Active")]
    public string Active => this[nameof (Active)];

    /// <summary>Failed</summary>
    [ResourceEntry("Failed", Description = "Failed", LastModified = "2012/07/13", Value = "Failed")]
    public string Failed => this[nameof (Failed)];

    /// <summary>Phrase: Change classifications for this site...</summary>
    [ResourceEntry("SetTaxonomyForThisSite", Description = "Phrase: Change classifications for this site...", LastModified = "2015/01/26", Value = "Change classifications for this site...")]
    public string SetTaxonomyForThisSite => this[nameof (SetTaxonomyForThisSite)];

    /// <summary>Phrase: Site</summary>
    [ResourceEntry("Site", Description = "Phrase: Site", LastModified = "2015/02/04", Value = "Site")]
    public string Site => this[nameof (Site)];

    /// <summary>Phrase: Use classification in...</summary>
    [ResourceEntry("UseClassificationIn", Description = "Phrase: Use classification in...", LastModified = "2015/02/03", Value = "Use classification in...")]
    public string UseClassificationIn => this[nameof (UseClassificationIn)];

    /// <summary>Phrase: This list</summary>
    /// <value>This list</value>
    [ResourceEntry("ThisList", Description = "Phrase: This list", LastModified = "2015/04/20", Value = "This list")]
    public string ThisList => this[nameof (ThisList)];

    /// <summary>Performance optimization</summary>
    [ResourceEntry("BackendPerformanceOptimizationHtmlTitle", Description = "The html title of the Performance Optimization page.", LastModified = "2012/07/10", Value = "Performance optimization")]
    public string BackendPerformanceOptimizationHtmlTitle => this[nameof (BackendPerformanceOptimizationHtmlTitle)];

    /// <summary>Your site will be running faster</summary>
    [ResourceEntry("PerfOptimizationInfoHeader", Description = "Your site will be running faster", LastModified = "2012/07/09", Value = "Your site will be running faster")]
    public string PerfOptimizationInfoHeader => this[nameof (PerfOptimizationInfoHeader)];

    /// <summary>
    /// Sitefinity is going to perform database optimization procedure which may take up to 2 hours.
    /// </summary>
    [ResourceEntry("PerfOptimizationInfoDescription1", Description = "Sitefinity is going to perform database optimization procedure which may take up to 2 hours.", LastModified = "2012/07/09", Value = "Sitefinity is going to perform database optimization procedure which may take up to 2 hours.")]
    public string PerfOptimizationInfoDescription1 => this[nameof (PerfOptimizationInfoDescription1)];

    /// <summary>Up to {0} hours</summary>
    [ResourceEntry("PerfOptimizationInfoDescription2", Description = "Up to 2 hours", LastModified = "2012/07/09", Value = "Up to 2 hours")]
    public string PerfOptimizationInfoDescription2 => this[nameof (PerfOptimizationInfoDescription2)];

    /// <summary>
    /// The result of these optimizations will affect page performance during edit, publishing and frontend startup time - e.g. your site will be running faster.
    /// </summary>
    [ResourceEntry("PerfOptimizationInfoDescription3", Description = "The result of these optimizations will affect page performance during edit, publishing and frontend startup time - e.g. your site will be running faster.", LastModified = "2012/07/09", Value = "The result of these optimizations will affect page performance during edit, publishing and frontend startup time &mdash; e.g. your site will be running faster.")]
    public string PerfOptimizationInfoDescription3 => this[nameof (PerfOptimizationInfoDescription3)];

    /// <summary>Please note that:</summary>
    [ResourceEntry("PerfOptimizationInfoDescription4", Description = "Please note that:", LastModified = "2012/07/09", Value = "Please note that:")]
    public string PerfOptimizationInfoDescription4 => this[nameof (PerfOptimizationInfoDescription4)];

    /// <summary>
    /// It's advisable to have backup of your database, even there is no risk of losing any data.
    /// </summary>
    [ResourceEntry("PerfOptimizationInfoDescription5", Description = "It's advisable to have backup of your database, even there is no risk of losing any data.", LastModified = "2012/07/09", Value = "It's advisable to have backup of your database, even there is no risk of losing any data.")]
    public string PerfOptimizationInfoDescription5 => this[nameof (PerfOptimizationInfoDescription5)];

    /// <summary>
    /// The site should not be used in production during the optimization procedure.
    /// </summary>
    [ResourceEntry("PerfOptimizationInfoDescription6", Description = "The site should not be used in production during the optimization procedure.", LastModified = "2012/07/09", Value = "The site should not be used in production during the optimization procedure.")]
    public string PerfOptimizationInfoDescription6 => this[nameof (PerfOptimizationInfoDescription6)];

    /// <summary>Start optimization</summary>
    [ResourceEntry("PerfOptimizationStart", Description = "Start optimization", LastModified = "2012/07/09", Value = "Start optimization")]
    public string PerfOptimizationStart => this[nameof (PerfOptimizationStart)];

    /// <summary>Optimization is in progress...</summary>
    [ResourceEntry("PerfOptimizationInProgress", Description = "Optimization is in progress...", LastModified = "2012/07/09", Value = "Optimization is in progress...")]
    public string PerfOptimizationInProgress => this[nameof (PerfOptimizationInProgress)];

    /// <summary>
    /// Please wait, while this process finishes. It may take up to 2 hours depending on the database size.
    /// </summary>
    [ResourceEntry("PerfOptimizationInProgressWait", Description = "Please wait, while this process finishes. It may take up to 2 hours depending on the database size.", LastModified = "2012/07/09", Value = "Please wait, while this process finishes. It may take up to 2 hours depending on the database size.")]
    public string PerfOptimizationInProgressWait => this[nameof (PerfOptimizationInProgressWait)];

    /// <summary>Performance optimization is completed.</summary>
    [ResourceEntry("PerfOptimizationCompleted", Description = "Performance optimization is completed.", LastModified = "2012/07/09", Value = "Performance optimization is completed.")]
    public string PerfOptimizationCompleted => this[nameof (PerfOptimizationCompleted)];

    /// <summary>Performance optimization has failed</summary>
    [ResourceEntry("PerfOptimizationFailed", Description = "The text message to be shown when the performance optimization has failed", LastModified = "2012/07/09", Value = "Performance optimization has failed")]
    public string PerfOptimizationFailed => this[nameof (PerfOptimizationFailed)];

    /// <summary>Error details</summary>
    [ResourceEntry("PerfOptimizationFailureDetails", Description = "Performance optimization failure details.", LastModified = "2012/07/09", Value = "Error details")]
    public string PerfOptimizationFailureDetails => this[nameof (PerfOptimizationFailureDetails)];

    /// <summary>
    /// Please send error details as a ticket or write to support@sitefinity.com
    /// </summary>
    [ResourceEntry("PerfOptimizationFailureNotification", Description = "Information for the steps to inform the support for the problem", LastModified = "2012/07/09", Value = "Please send error details as a ticket or write to support@sitefinity.com")]
    public string PerfOptimizationFailureNotification => this[nameof (PerfOptimizationFailureNotification)];

    /// <summary>Go to administration</summary>
    [ResourceEntry("PerfOptimizationAdministrationNavigation", Description = "Go to administration text", LastModified = "2012/07/09", Value = "Go to administration")]
    public string PerfOptimizationAdministrationNavigation => this[nameof (PerfOptimizationAdministrationNavigation)];

    /// <summary>Title of the toolbox section for dashboard</summary>
    [ResourceEntry("DashboardToolboxSectionTitle", Description = "Title of the toolbox section for Dashboard controls.", LastModified = "2013/09/25", Value = "Dashboard")]
    public string DashboardToolboxSectionTitle => this[nameof (DashboardToolboxSectionTitle)];

    /// <summary>Description of the toolbox section for dashboard</summary>
    [ResourceEntry("DashboardToolboxSectionDescription", Description = "Description of the toolbox section for the Dashboard controls.", LastModified = "2013/08/09", Value = "This section contains public controls of the Dashboard module.")]
    public string DashboardToolboxSectionDescription => this[nameof (DashboardToolboxSectionDescription)];

    /// <summary>Phrase: "All Providers"</summary>
    [ResourceEntry("AllProvidersText", Description = "The text to be shown for select option in user/roles admin UI.", LastModified = "2009/06/30", Value = "All Providers")]
    public string AllProvidersText => this[nameof (AllProvidersText)];

    /// <summary>Error message for external rendereres</summary>
    [ResourceEntry("CannotProcessExternalRendererPage", Description = "The text to be shown for select option in user/roles admin UI.", LastModified = "2020/08/04", Value = "Sitefinity cannot process the page for external renderer {0}")]
    public string CannotProcessExternalRendererPage => this[nameof (CannotProcessExternalRendererPage)];

    /// <summary>Phrase: "All Providers"</summary>
    [ResourceEntry("SwitchNewInterface", Description = "The text shown when switching to the new interface per user.", LastModified = "2018/02/27", Value = "Switch new interface")]
    public string SwitchNewInterface => this[nameof (SwitchNewInterface)];

    /// <summary>Phrase: "Preferences"</summary>
    [ResourceEntry("UserPreferences", Description = "Label for user preferences option in the user menu.", LastModified = "2018/02/27", Value = "Preferences")]
    public string UserPreferences => this[nameof (UserPreferences)];

    /// <summary>Phrase: "Keyboard shortcuts"</summary>
    [ResourceEntry("KeyboardShortcuts", Description = "Label for keyboard shortcuts option in the user menu.", LastModified = "2020/09/18", Value = "Keyboard shortcuts")]
    public string KeyboardShortcuts => this[nameof (KeyboardShortcuts)];

    /// <summary>Phrase: All Users</summary>
    [ResourceEntry("AllUsers", Description = "The text to be shown All Users button in User UI.", LastModified = "2009/06/30", Value = "All Users")]
    public string AllUsers => this[nameof (AllUsers)];

    /// <summary>Phrase: All Roles</summary>
    [ResourceEntry("AllRoles", Description = "The text to be shown All Roles button in User UI.", LastModified = "2009/12/17", Value = "All Roles")]
    public string AllRoles => this[nameof (AllRoles)];

    /// <summary>Phrase: All Providers</summary>
    [ResourceEntry("AllProviders", Description = "The text to be shown All Providers button in User UI.", LastModified = "2010/10/06", Value = "All Providers")]
    public string AllProviders => this[nameof (AllProviders)];

    /// <summary>
    /// phrase: You have unsaved changes! Do you want to leave this page?
    /// </summary>
    [ResourceEntry("YouHaveUnsavedChangesWantToLeavePage", Description = "phrase: You have unsaved changes! Do you want to leave this page?", LastModified = "2010/10/14", Value = "Are you sure you want to navigate away from this page?\n\nYou have unsaved changes.\n\nPress OK to continue, or Cancel to stay on the current page.")]
    public string YouHaveUnsavedChangesWantToLeavePage => this[nameof (YouHaveUnsavedChangesWantToLeavePage)];

    /// <summary>phrase: Do you want to leave this page?</summary>
    [ResourceEntry("YouWantToLeavePage", Description = "phrase: Do you want to leave this page?", LastModified = "2011/09/14", Value = "Are you sure you want to navigate away from this page?\n\n{0}\n\nPress OK to continue, or Cancel to stay on the current page.")]
    public string YouWantToLeavePage => this[nameof (YouWantToLeavePage)];

    /// <summary>Phrase: Browse Users</summary>
    [ResourceEntry("BrowseUsers", Description = "The text to be shown for Browse Users button in user admin UI.", LastModified = "2009/06/30", Value = "Browse Users")]
    public string BrowseUsers => this[nameof (BrowseUsers)];

    /// <summary>Phrase: Users by role</summary>
    [ResourceEntry("UsersByRole", Description = "The text to be shown for Users by role section.", LastModified = "2009/06/30", Value = "Users by role")]
    public string UsersByRole => this[nameof (UsersByRole)];

    /// <summary>Phrase: Users by role</summary>
    [ResourceEntry("UsersWithoutRole", Description = "The text to be shown for Users without role section.", LastModified = "2009/06/30", Value = "Users without role")]
    public string UsersWithoutRole => this[nameof (UsersWithoutRole)];

    /// <summary>Phrase: Manage Roles</summary>
    [ResourceEntry("ManageRoles", Description = "The text to be shown for Manage Roles section.", LastModified = "2009/06/30", Value = "Manage Roles")]
    public string ManageRoles => this[nameof (ManageRoles)];

    /// <summary>Phrase: Create a role</summary>
    [ResourceEntry("CreateRoleLabel", Description = "The text to be shown for Create Role form.", LastModified = "2009/07/01", Value = "Create a role")]
    public string CreateRoleLabel => this[nameof (CreateRoleLabel)];

    /// <summary>Phrase: More info</summary>
    [ResourceEntry("MoreInfo", Description = "TMore info", LastModified = "2009/07/01", Value = "More info")]
    public string MoreInfo => this[nameof (MoreInfo)];

    /// <summary>Phrase: Example role name.</summary>
    [ResourceEntry("ExampleForNewRoleName", Description = "Example role name.", LastModified = "2009/07/01", Value = "<strong>Example: </strong><q>Authors</q>")]
    public string ExampleForNewRoleName => this[nameof (ExampleForNewRoleName)];

    /// <summary>Phrase: What is a role?</summary>
    [ResourceEntry("WhatIsRoleQuestion", Description = "What is a role?", LastModified = "2009/07/01", Value = "What is a role?")]
    public string WhatIsRoleQuestion => this[nameof (WhatIsRoleQuestion)];

    /// <summary>
    /// Phrase: A role is a group of users associated with a set of user permissions.
    /// </summary>
    [ResourceEntry("RoleHelpExplanation", Description = "Role Help Explanation", LastModified = "2009/07/01", Value = "A role is a group of users associated with a set of user permissions.")]
    public string RoleHelpExplanation => this[nameof (RoleHelpExplanation)];

    /// <summary>Phrase: Last modified</summary>
    [ResourceEntry("LastModified", Description = "phrase: Last modified", LastModified = "2009/07/02", Value = "Last modified")]
    public string LastModified => this[nameof (LastModified)];

    /// <summary>Phrase: used in</summary>
    [ResourceEntry("UsedIn", Description = "phrase: Used in", LastModified = "2009/07/02", Value = "Used in")]
    public string UsedIn => this[nameof (UsedIn)];

    /// <summary>Phrase: Are you sure you want to delete this user?</summary>
    [ResourceEntry("QuestionBeforeDeletingUser", Description = "The question asked before deleting a membership user in Users administration.", LastModified = "2009/07/02", Value = "Are you sure you want to delete this user?")]
    public string QuestionBeforeDeletingUser => this[nameof (QuestionBeforeDeletingUser)];

    /// <summary>
    /// Phrase: Are you sure you want to delete the selected users? (for multiple users)
    /// </summary>
    [ResourceEntry("QuestionBeforeDeletingUsers", Description = "The question asked before deleting multiple membership users in Users administration.", LastModified = "2009/12/24", Value = "Are you sure you want to delete the selected users?")]
    public string QuestionBeforeDeletingUsers => this[nameof (QuestionBeforeDeletingUsers)];

    /// <summary>Phrase: Are you sure you want to delete this item?</summary>
    [ResourceEntry("QuestionBeforeDeletingItem", Description = "The question asked before deleting a content item.", LastModified = "2009/11/19", Value = "Are you sure you want to delete this item?")]
    public string QuestionBeforeDeletingItem => this[nameof (QuestionBeforeDeletingItem)];

    /// <summary>
    /// Phrase: Are you sure you want to delete this profile permanently? Upon deletion any pages using this profile will fallback to the default one.
    /// </summary>
    [ResourceEntry("QuestionBeforeDeletingPageCacheProfile", Description = "The question asked before deleting a page cache profile from the basic settings.", LastModified = "2017/02/07", Value = "Are you sure you want to delete this profile permanently?<br/>Upon deletion any pages using this profile will fallback to the default one.")]
    public string QuestionBeforeDeletingPageCacheProfile => this[nameof (QuestionBeforeDeletingPageCacheProfile)];

    /// <summary>
    /// Phrase: Are you sure you want to delete this profile permanently? Upon deletion any libraries using this profile will fallback to the default one.
    /// </summary>
    [ResourceEntry("QuestionBeforeDeletingMediaCacheProfile", Description = "The question asked before deleting a media cache profile from the basic settings.", LastModified = "2017/02/07", Value = "Are you sure you want to delete this profile permanently?<br/>Upon deletion any libraries using this profile will fallback to the default one.")]
    public string QuestionBeforeDeletingMediaCacheProfile => this[nameof (QuestionBeforeDeletingMediaCacheProfile)];

    /// <summary>label: Mandatory fields</summary>
    [ResourceEntry("MandatoryFields", Description = "Mandatory fields label.", LastModified = "2009/07/02", Value = "Mandatory fields")]
    public string MandatoryFields => this[nameof (MandatoryFields)];

    /// <summary>Label: Membership Info</summary>
    [ResourceEntry("MembershipInfo", Description = "Membership Info label.", LastModified = "2009/07/02", Value = "Membership Info")]
    public string MembershipInfo => this[nameof (MembershipInfo)];

    /// <summary>Phrase: Back to users</summary>
    [ResourceEntry("BackToUsers", Description = "Text for Back to users link in User administration UI.", LastModified = "2009/07/02", Value = "Back to Users")]
    public string BackToUsers => this[nameof (BackToUsers)];

    /// <summary>Phrase: Back to Site Sync</summary>
    /// <value>Back to Site Sync</value>
    [ResourceEntry("BackToSites", Description = "Text for Back to Site Sync.", LastModified = "2016/09/13", Value = "Back to Site Sync")]
    public string BackToSites => this[nameof (BackToSites)];

    /// <summary>phrase: Back to comments</summary>
    [ResourceEntry("BackToComments", Description = "phrase: Back to Comments", LastModified = "2010/11/12", Value = "Back to Comments")]
    public string BackToComments => this[nameof (BackToComments)];

    /// <summary>Phrase: Back to profile</summary>
    [ResourceEntry("BackToProfile", Description = "Text for back to profile link in change password dialog.", LastModified = "2010/09/20", Value = "Back to profile")]
    public string BackToProfile => this[nameof (BackToProfile)];

    /// <summary>Phrase: Return to policies</summary>
    [ResourceEntry("ReturnToPolicies", Description = "Return to policies", LastModified = "2009/07/02", Value = "Return to policies")]
    public string ReturnToPolicies => this[nameof (ReturnToPolicies)];

    /// <summary>Phrase: Back to users</summary>
    [ResourceEntry("UserCommentLabelText", Description = "Text for comment label in User administration UI.", LastModified = "2009/07/02", Value = "Comment")]
    public string UserCommentLabelText => this[nameof (UserCommentLabelText)];

    /// <summary>Phrase: Back to users</summary>
    [ResourceEntry("UserApproveLabelText", Description = "Text for approve user label in User administration UI.", LastModified = "2009/07/03", Value = "Approve user")]
    public string UserApproveLabelText => this[nameof (UserApproveLabelText)];

    /// <summary>Phrase: Save Changes Label</summary>
    [ResourceEntry("SectionSaved", Description = "phrase", LastModified = "2009/07/20", Value = "Configuration section saved successfully.")]
    public string SectionSaved => this[nameof (SectionSaved)];

    /// <summary>Phrase: Are you sure you want to delete this section?</summary>
    [ResourceEntry("ConfirmDelete", Description = "phrase", LastModified = "2009/07/20", Value = "Are you sure you want to delete this section?")]
    public string ConfirmDelete => this[nameof (ConfirmDelete)];

    /// <summary>Word: show</summary>
    [ResourceEntry("Show", Description = "word", LastModified = "2009/07/22", Value = "Show")]
    public string Show => this[nameof (Show)];

    /// <summary>Phrase: Labels in</summary>
    [ResourceEntry("LabelsIn", Description = "Labels in", LastModified = "2009/07/22", Value = "Labels in")]
    public string LabelsIn => this[nameof (LabelsIn)];

    /// <summary>phrase: What is this label used for.</summary>
    [ResourceEntry("WhatIsThisLabelUsedFor", Description = "phrase", LastModified = "2009/07/27", Value = "What is this label used for.")]
    public string WhatIsThisLabelUsedFor => this[nameof (WhatIsThisLabelUsedFor)];

    /// <summary>word: Example</summary>
    [ResourceEntry("Example", Description = "word", LastModified = "2009/07/27", Value = "Example")]
    public string Example => this[nameof (Example)];

    /// <summary>phrase: This is the button for saving news</summary>
    [ResourceEntry("ThisIsTheButtonForSavingNews", Description = "phrase", LastModified = "2009/07/27", Value = "This is the button for saving news")]
    public string ThisIsTheButtonForSavingNews => this[nameof (ThisIsTheButtonForSavingNews)];

    /// <summary>word: Key</summary>
    [ResourceEntry("Key", Description = "word", LastModified = "2009/07/27", Value = "Key")]
    public string Key => this[nameof (Key)];

    /// <summary>phrase: Used in code to display this label</summary>
    [ResourceEntry("UsedInCodeToDisplayThisLabel", Description = "phrase", LastModified = "2009/07/27", Value = "Used in code to display this label")]
    public string UsedInCodeToDisplayThisLabel => this[nameof (UsedInCodeToDisplayThisLabel)];

    /// <summary>word: Type</summary>
    [ResourceEntry("Type", Description = "word", LastModified = "2009/07/27", Value = "Type")]
    public string Type => this[nameof (Type)];

    /// <summary>
    /// phrase: This is the "class-pageId" value used in the code
    /// </summary>
    [ResourceEntry("ThisIsTheClassIdValueUsedInCode", Description = "phrase", LastModified = "2009/07/27", Value = "This is the \"class-id\" value used in the code")]
    public string ThisIsTheClassIdValueUsedInCode => this[nameof (ThisIsTheClassIdValueUsedInCode)];

    /// <summary>word: Label</summary>
    [ResourceEntry("Label", Description = "word", LastModified = "2009/07/27", Value = "Label")]
    public string Label => this[nameof (Label)];

    /// <summary>word: All</summary>
    [ResourceEntry("All", Description = "word", LastModified = "2009/07/28", Value = "All")]
    public string All => this[nameof (All)];

    /// <summary>word: None</summary>
    [ResourceEntry("None", Description = "word: None", LastModified = "2010/11/03", Value = "None")]
    public string None => this[nameof (None)];

    /// <summary>word: Selected</summary>
    [ResourceEntry("Selected", Description = "word", LastModified = "2009/07/28", Value = "Selected")]
    public string Selected => this[nameof (Selected)];

    /// <summary>word: Select</summary>
    [ResourceEntry("Select", Description = "Label", LastModified = "2009/09/24", Value = "Select")]
    public string Select => this[nameof (Select)];

    /// <summary>word: Last Edited</summary>
    [ResourceEntry("LastEdited", Description = "LastEdited", LastModified = "2010/10/03", Value = "Last edited")]
    public string LastEdited => this[nameof (LastEdited)];

    /// <summary>word: Select...</summary>
    [ResourceEntry("SelectDotDotDot", Description = "Label", LastModified = "2010/05/19", Value = "Select...")]
    public string SelectDotDotDot => this[nameof (SelectDotDotDot)];

    /// <summary>word: Change...</summary>
    [ResourceEntry("ChangeDotDotDot", Description = "Label", LastModified = "2010/08/18", Value = "Change...")]
    public string ChangeDotDotDot => this[nameof (ChangeDotDotDot)];

    /// <summary>word: Select {0}</summary>
    [ResourceEntry("SelectParameter", Description = "Label", LastModified = "2010/03/25", Value = "Select {0}")]
    public string SelectParameter => this[nameof (SelectParameter)];

    /// <summary>phrase: Select a masterpage you have uploaded</summary>
    [ResourceEntry("SelectMasterPage", Description = "phrase", LastModified = "2009/11/06", Value = "Select the master page you have uploaded")]
    public string SelectMasterPage => this[nameof (SelectMasterPage)];

    /// <summary>phrase: Name cannot be empty</summary>
    [ResourceEntry("NameRequired", Description = "phrase", LastModified = "2009/11/06", Value = "Name cannot be empty")]
    public string NameRequired => this[nameof (NameRequired)];

    /// <summary>
    /// phrase: Url cannot contain spaces or special characters!
    /// </summary>
    [ResourceEntry("UrlNameInvalid", Description = "phrase", LastModified = "2009/11/06", Value = "Url cannot contain spaces or special characters!")]
    public string UrlNameInvalid => this[nameof (UrlNameInvalid)];

    /// <summary>phrase: Url cannot be empty.</summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "phrase: Url cannot be empty.", LastModified = "2010/03/15", Value = "Url cannot be empty.")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>phrase: Which versions you want to delete?</summary>
    [ResourceEntry("WhichVersionsYouWantToDelete", Description = "phrase", LastModified = "2009/07/28", Value = "Which versions you want to delete?")]
    public string WhichVersionsYouWantToDelete => this[nameof (WhichVersionsYouWantToDelete)];

    /// <summary>phrase: "{0}" label has been deleted.</summary>
    [ResourceEntry("LabelHasBeenDeleted", Description = "phrase", LastModified = "2009/07/29", Value = "\"{0}\" label has been deleted.")]
    public string LabelHasBeenDeleted => this[nameof (LabelHasBeenDeleted)];

    /// <summary>phrase: "{0}" label has been created.</summary>
    [ResourceEntry("LabelHasBeenCreated", Description = "phrase", LastModified = "2009/07/29", Value = "\"{0}\" label has been created.")]
    public string LabelHasBeenCreated => this[nameof (LabelHasBeenCreated)];

    /// <summary>phrase: "{0}" label has been updated.</summary>
    [ResourceEntry("LabelHasBeenUpdated", Description = "", LastModified = "2009/07/29", Value = "\"{0}\" label has been updated.")]
    public string LabelHasBeenUpdated => this[nameof (LabelHasBeenUpdated)];

    /// <summary>phrase: Changes have been saved</summary>
    [ResourceEntry("ChangesHaveBeenSaved", Description = "phrase", LastModified = "2009/07/30", Value = "Changes have been saved")]
    public string ChangesHaveBeenSaved => this[nameof (ChangesHaveBeenSaved)];

    /// <summary>phrase: Default labels</summary>
    [ResourceEntry("DefaultLabels", Description = "phrase", LastModified = "2009/07/30", Value = "Default labels")]
    public string DefaultLabels => this[nameof (DefaultLabels)];

    /// <summary>phrase: Your changed labels</summary>
    [ResourceEntry("YourChangedLabels", Description = "phrase", LastModified = "2009/07/30", Value = "Your changed labels")]
    public string YourChangedLabels => this[nameof (YourChangedLabels)];

    /// <summary>phrase: More about this label</summary>
    [ResourceEntry("MoreAboutThisLabel", Description = "phrase", LastModified = "2009/07/30", Value = "More about this label")]
    public string MoreAboutThisLabel => this[nameof (MoreAboutThisLabel)];

    /// <summary>phrase: All language versions</summary>
    [ResourceEntry("AllLanguageVersions", Description = "phrase", LastModified = "2009/07/30", Value = "All language versions")]
    public string AllLanguageVersions => this[nameof (AllLanguageVersions)];

    /// <summary>phrase: Edit all labels</summary>
    [ResourceEntry("EditAllLabels", Description = "phrase", LastModified = "2009/07/30", Value = "Edit all labels")]
    public string EditAllLabels => this[nameof (EditAllLabels)];

    /// <summary>phrase: Edit description</summary>
    [ResourceEntry("EditDescription", Description = "phrase", LastModified = "2009/07/30", Value = "Edit description")]
    public string EditDescription => this[nameof (EditDescription)];

    /// <summary>phrase: Back to all labels</summary>
    [ResourceEntry("BackToAllLabels", Description = "phrase", LastModified = "2009/07/30", Value = "Back to all labels")]
    public string BackToAllLabels => this[nameof (BackToAllLabels)];

    /// <summary>phrase: Email/Username cannot be empty</summary>
    [ResourceEntry("UsernameCannotBeEmpty", Description = "phrase", LastModified = "2016/12/08", Value = "Enter your email / username")]
    public string UsernameCannotBeEmpty => this[nameof (UsernameCannotBeEmpty)];

    /// <summary>phrase: Name cannot be empty.</summary>
    [ResourceEntry("NameCannotBeEmpty", Description = "phrase: Name cannot be empty.", LastModified = "2011/02/03", Value = "Name cannot be empty.")]
    public string NameCannotBeEmpty => this[nameof (NameCannotBeEmpty)];

    /// <summary>phrase: Password cannot be empty</summary>
    [ResourceEntry("PasswordCannotBeEmpty", Description = "phrase", LastModified = "2009/07/30", Value = "Enter your password")]
    public string PasswordCannotBeEmpty => this[nameof (PasswordCannotBeEmpty)];

    /// <summary>phrase: Enter your old password.</summary>
    [ResourceEntry("OldPasswordCannotBeEmpty", Description = "phrase", LastModified = "2010/09/20", Value = "Enter your old password")]
    public string OldPasswordCannotBeEmpty => this[nameof (OldPasswordCannotBeEmpty)];

    /// <summary>phrase: Enter your new password.</summary>
    [ResourceEntry("NewPasswordCannotBeEmpty", Description = "phrase", LastModified = "2010/09/20", Value = "Enter your new password")]
    public string NewPasswordCannotBeEmpty => this[nameof (NewPasswordCannotBeEmpty)];

    /// <summary>phrase: Re-enter your new password.</summary>
    [ResourceEntry("ConfirmNewPasswordCannotBeEmpty", Description = "phrase", LastModified = "2010/09/20", Value = "Re-enter your new password")]
    public string ConfirmNewPasswordCannotBeEmpty => this[nameof (ConfirmNewPasswordCannotBeEmpty)];

    /// <summary>phrase: Login to manage the site</summary>
    [ResourceEntry("LoginToManage", Description = "phrase", LastModified = "2009/07/30", Value = "Login to manage the site")]
    public string LoginToManage => this[nameof (LoginToManage)];

    /// <summary>phrase: Login with email address</summary>
    [ResourceEntry("LoginWithEmail", Description = "phrase", LastModified = "2016/12/09", Value = "Login with email address")]
    public string LoginWithEmail => this[nameof (LoginWithEmail)];

    /// <summary>phrase: or use your account in...</summary>
    [ResourceEntry("OrUseYourAccount", Description = "phrase", LastModified = "2016/12/09", Value = "or use your account in...")]
    public string OrUseYourAccount => this[nameof (OrUseYourAccount)];

    /// <summary>phrase: Login to manage the site</summary>
    [ResourceEntry("NoBookmarksMessage", Description = "phrase", LastModified = "2009/09/25", Value = "There are no social bookmarks")]
    public string NoBookmarksMessage => this[nameof (NoBookmarksMessage)];

    /// <summary>phrase: Back to all labels</summary>
    [ResourceEntry("BackToAllItems", Description = "phrase for module dialogs", LastModified = "2009/10/14", Value = "Back to all items")]
    public string BackToAllItems => this[nameof (BackToAllItems)];

    /// <summary>phrase: Back to {0}</summary>
    [ResourceEntry("BackTo", Description = "Back to {0}", LastModified = "2010/10/14", Value = "Back to {0}")]
    public string BackTo => this[nameof (BackTo)];

    /// <summary>phrase: Back to all {0}</summary>
    [ResourceEntry("BackToAllItemsParameter", Description = "phrase: Back to all {0}", LastModified = "2010/03/18", Value = "Back to all {0}")]
    public string BackToAllItemsParameter => this[nameof (BackToAllItemsParameter)];

    /// <summary>Label: Return to pages</summary>
    [ResourceEntry("ReturnToPages", Description = "Label", LastModified = "2009/10/16", Value = "Return to pages")]
    public string ReturnToPages => this[nameof (ReturnToPages)];

    /// <summary>Label: Back to Templates</summary>
    [ResourceEntry("ReturnToTemplates", Description = "Label", LastModified = "2009/11/19", Value = "Back to Templates")]
    public string ReturnToTemplates => this[nameof (ReturnToTemplates)];

    /// <summary>Label: Return to properties</summary>
    [ResourceEntry("ReturnToProperties", Description = "Label", LastModified = "2009/11/05", Value = "Return to properties")]
    public string ReturnToProperties => this[nameof (ReturnToProperties)];

    /// <summary>phrase: Back to revision history</summary>
    [ResourceEntry("BackToRevisionHistory", Description = "phrase for module dialogs", LastModified = "2009/10/14", Value = "Back to Revision History")]
    public string BackToRevisionHistory => this[nameof (BackToRevisionHistory)];

    /// <summary>phrase: Back to revision history</summary>
    [ResourceEntry("BackToRevisionHistoryOf", Description = "phrase for module dialogs", LastModified = "2013/11/01", Value = "Back to Revision History of")]
    public string BackToRevisionHistoryOf => this[nameof (BackToRevisionHistoryOf)];

    /// <summary>phrase: Back to reviewed version</summary>
    [ResourceEntry("BackToReviewedVersion", Description = "phrase for module dialogs", LastModified = "2010/10/15", Value = "Back to reviewed version")]
    public string BackToReviewedVersion => this[nameof (BackToReviewedVersion)];

    /// <summary>Label: Properties</summary>
    [ResourceEntry("Properties", Description = "Label", LastModified = "2009/11/05", Value = "Properties")]
    public string Properties => this[nameof (Properties)];

    /// <summary>Label: Name</summary>
    [ResourceEntry("Name", Description = "Label", LastModified = "2009/10/16", Value = "Name <span class=\"sfExLab\">(Displayed in navigation. <strong>Example:</strong> <em>About Us</em>)</span>")]
    public string Name => this[nameof (Name)];

    /// <summary>Label: TemplateName</summary>
    [ResourceEntry("TemplateName", Description = "Label", LastModified = "2009/10/16", Value = "Name")]
    public string TemplateName => this[nameof (TemplateName)];

    /// <summary>Label: Section</summary>
    [ResourceEntry("Parent", Description = "Label", LastModified = "2009/10/16", Value = "Section")]
    public string Parent => this[nameof (Parent)];

    /// <summary>Label: Parent {0}</summary>
    [ResourceEntry("ParentParameter", Description = "Label: Parent {0}", LastModified = "2009/10/16", Value = "Parent {0}")]
    public string ParentParameter => this[nameof (ParentParameter)];

    /// <summary>Label: No section</summary>
    [ResourceEntry("NoSection", Description = "Label", LastModified = "2009/11/25", Value = "No section")]
    public string NoSection => this[nameof (NoSection)];

    /// <summary>Label: Parent section</summary>
    [ResourceEntry("ParentSection", Description = "ParentSection", LastModified = "2009/11/25", Value = "Parent Section")]
    public string ParentSection => this[nameof (ParentSection)];

    /// <summary>Label: No Parent</summary>
    [ResourceEntry("NoParentSection", Description = "NoParentSection", LastModified = "2009/10/16", Value = "No parent section")]
    public string NoParentSection => this[nameof (NoParentSection)];

    /// <summary>Label: URL</summary>
    [ResourceEntry("Url", Description = "Label", LastModified = "2009/10/16", Value = "URL")]
    public string Url => this[nameof (Url)];

    /// <summary>Label: Create this section</summary>
    [ResourceEntry("CreateThisSection", Description = "Label", LastModified = "2009/10/16", Value = "Create this section")]
    public string CreateThisSection => this[nameof (CreateThisSection)];

    /// <summary>Label: Create this section and add another</summary>
    [ResourceEntry("CreateThisSectionAndAddAnother", Description = "Label", LastModified = "2009/10/16", Value = "Create this section and add another")]
    public string CreateThisSectionAndAddAnother => this[nameof (CreateThisSectionAndAddAnother)];

    /// <summary>Label: Create a Section</summary>
    [ResourceEntry("CreateSection", Description = "Label", LastModified = "2009/10/19", Value = "Create a Section")]
    public string CreateSection => this[nameof (CreateSection)];

    /// <summary>Label: Edit Section Properties</summary>
    [ResourceEntry("EditSectionProperties", Description = "Label", LastModified = "2009/11/10", Value = "Edit Section Properties")]
    public string EditSectionProperties => this[nameof (EditSectionProperties)];

    /// <summary>Label: Create a Page</summary>
    [ResourceEntry("CreatePage", Description = "Label", LastModified = "2009/10/22", Value = "Create a Page")]
    public string CreatePage => this[nameof (CreatePage)];

    /// <summary>Label: Create a Template</summary>
    [ResourceEntry("CreateTemplate", Description = "Label", LastModified = "2009/11/17", Value = "Create a Template")]
    public string CreateTemplate => this[nameof (CreateTemplate)];

    /// <summary>Label: Edit Page Properties</summary>
    [ResourceEntry("EditPageProperties", Description = "Label", LastModified = "2009/11/10", Value = "Edit Page Properties")]
    public string EditPageProperties => this[nameof (EditPageProperties)];

    /// <summary>Label: Edit Template Properties</summary>
    [ResourceEntry("EditTemplateProperties", Description = "Label", LastModified = "2010/07/26", Value = "Edit template properties")]
    public string EditTemplateProperties => this[nameof (EditTemplateProperties)];

    /// <summary>Label: Selected section...</summary>
    [ResourceEntry("SelectedSection", Description = "Label", LastModified = "2009/11/25", Value = "Selected section...")]
    public string SelectedSection => this[nameof (SelectedSection)];

    /// <summary>Label: Selected parent section...</summary>
    [ResourceEntry("SelectedParentSection", Description = "Label", LastModified = "2009/11/25", Value = "Selected parent section...")]
    public string SelectedParentSection => this[nameof (SelectedParentSection)];

    /// <summary>Label: Create this page and go to select a template</summary>
    [ResourceEntry("CreateThisPage", Description = "Label", LastModified = "2009/10/19", Value = "Create and go to select a template")]
    public string CreateThisPage => this[nameof (CreateThisPage)];

    /// <summary>phrase: Create this</summary>
    [ResourceEntry("CreateThis", Description = "phrase: Create this", LastModified = "2010/03/05", Value = "Create this")]
    public string CreateThis => this[nameof (CreateThis)];

    /// <summary>Label: Create this template</summary>
    [ResourceEntry("CreateThisTemplate", Description = "Label", LastModified = "2009/11/17", Value = "Create this template")]
    public string CreateThisTemplate => this[nameof (CreateThisTemplate)];

    /// <summary>Label: Create this page and add another</summary>
    [ResourceEntry("CreateThisPageAndAddAnother", Description = "Label", LastModified = "2009/10/19", Value = "Create and add another page")]
    public string CreateThisPageAndAddAnother => this[nameof (CreateThisPageAndAddAnother)];

    /// <summary>Label: Select a Layout</summary>
    [ResourceEntry("SelectLayout", Description = "Label", LastModified = "2009/10/22", Value = "Select a Layout")]
    public string SelectLayout => this[nameof (SelectLayout)];

    /// <summary>Label: Select another Template</summary>
    [ResourceEntry("SelectAnotherTemplate", Description = "Label", LastModified = "2009/11/10", Value = "Select another Template")]
    public string SelectAnotherTemplate => this[nameof (SelectAnotherTemplate)];

    /// <summary>Label: View in a new window</summary>
    [ResourceEntry("ViewTemplate", Description = "Label", LastModified = "2009/10/22", Value = "View in a new window")]
    public string ViewTemplate => this[nameof (ViewTemplate)];

    /// <summary>Label: Title</summary>
    [ResourceEntry("Title", Description = "Label", LastModified = "2009/11/02", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>Label: Click to add a title</summary>
    [ResourceEntry("ClickToAddTitle", Description = "Label", LastModified = "2009/11/02", Value = "Click to add a title")]
    public string ClickToAddTitle => this[nameof (ClickToAddTitle)];

    /// <summary>Label: Click to add</summary>
    [ResourceEntry("ClickToAdd", Description = "Click to add", LastModified = "2010/03/04", Value = "Click to add")]
    public string ClickToAdd => this[nameof (ClickToAdd)];

    /// <summary>Label: Description</summary>
    [ResourceEntry("PageDescription", Description = "Label", LastModified = "2009/11/02", Value = "Description")]
    public string PageDescription => this[nameof (PageDescription)];

    /// <summary>Label: Click to add a description</summary>
    [ResourceEntry("ClickToAddDescription", Description = "Label", LastModified = "2009/11/02", Value = "Click to add a description")]
    public string ClickToAddDescription => this[nameof (ClickToAddDescription)];

    /// <summary>Label: Keywords</summary>
    [ResourceEntry("Keywords", Description = "Label", LastModified = "2009/11/02", Value = "Keywords")]
    public string Keywords => this[nameof (Keywords)];

    /// <summary>Label: Click to add keywords</summary>
    [ResourceEntry("ClickToAddKeywords", Description = "Label", LastModified = "2009/11/02", Value = "Click to add keywords")]
    public string ClickToAddKeywords => this[nameof (ClickToAddKeywords)];

    /// <summary>Label: Click to add Tags</summary>
    [ResourceEntry("ClickToAddTags", Description = "Label", LastModified = "2009/11/02", Value = "Click to add Tags")]
    public string ClickToAddTags => this[nameof (ClickToAddTags)];

    /// <summary>Label: Click to add a Category</summary>
    [ResourceEntry("ClickToAddCategory", Description = "Label", LastModified = "2009/11/02", Value = "Click to add a Category")]
    public string ClickToAddCategory => this[nameof (ClickToAddCategory)];

    /// <summary>Label: Show in navigation</summary>
    [ResourceEntry("ShowInNavigation", Description = "Label", LastModified = "2009/11/02", Value = "Show in navigation")]
    public string ShowInNavigation => this[nameof (ShowInNavigation)];

    /// <summary>Label: Show in navigation Template</summary>
    [ResourceEntry("ShowInNavigationTemplate", Description = "Label", LastModified = "2009/11/02", Value = "Show in navigation all pages using this template <br /><span class=\"sfExLab\">This option can be customized per individual page.</span>")]
    public string ShowInNavigationTemplate => this[nameof (ShowInNavigationTemplate)];

    /// <summary>Label: No template - start from scratch</summary>
    [ResourceEntry("NoTemplate", Description = "Label", LastModified = "2009/11/06", Value = "No template - start from scratch")]
    public string NoTemplate => this[nameof (NoTemplate)];

    /// <summary>
    /// phrase: Use the selected template as a default template when you create a new page
    /// </summary>
    [ResourceEntry("UseAsDefaultTemplate", Description = "phrase: Use the selected template as a default template when you create a new page", LastModified = "2010/07/15", Value = "Use the selected template as a default template when you create a new page")]
    public string UseAsDefaultTemplate => this[nameof (UseAsDefaultTemplate)];

    /// <summary>Label: Include ScriptManger</summary>
    [ResourceEntry("IncludeScriptManger", Description = "Label", LastModified = "2009/11/03", Value = "Include ScriptManger")]
    public string IncludeScriptManger => this[nameof (IncludeScriptManger)];

    /// <summary>Word: to</summary>
    [ResourceEntry("To", Description = "word: to", LastModified = "2009/11/04", Value = "to")]
    public string To => this[nameof (To)];

    /// <summary>Word: from</summary>
    /// <value>from</value>
    [ResourceEntry("LowerFrom", Description = "word: from", LastModified = "2017/09/01", Value = "from")]
    public string LowerFrom => this[nameof (LowerFrom)];

    /// <summary>Word: To</summary>
    [ResourceEntry("CapitalTo", Description = "word: To", LastModified = "2010/06/16", Value = "To")]
    public string CapitalTo => this[nameof (CapitalTo)];

    /// <summary>Word: "From"</summary>
    [ResourceEntry("CapitalFrom", Description = "Word: From", LastModified = "2010/06/16", Value = "From")]
    public string CapitalFrom => this[nameof (CapitalFrom)];

    /// <summary>phrase: Create and add another</summary>
    [ResourceEntry("CreateAndAddAnother", Description = "phrase: Create and add another", LastModified = "2009/11/04", Value = "Create and add another")]
    public string CreateAndAddAnother => this[nameof (CreateAndAddAnother)];

    /// <summary>word: Required</summary>
    [ResourceEntry("Required", Description = "word: Required", LastModified = "2009/11/04", Value = "Required")]
    public string Required => this[nameof (Required)];

    /// <summary>Label: Select policy</summary>
    [ResourceEntry("SelectPolicy", Description = "label: Select policy", LastModified = "2009/11/06", Value = "Select policy")]
    public string SelectPolicy => this[nameof (SelectPolicy)];

    /// <summary>Label: Select another policy...</summary>
    [ResourceEntry("SelectAnotherPolicy", Description = "label: Select policy", LastModified = "2009/11/06", Value = "Select another policy...")]
    public string SelectAnotherPolicy => this[nameof (SelectAnotherPolicy)];

    /// <summary>phrase: Advanced filter</summary>
    [ResourceEntry("AdvancedFilter", Description = "phrase: Advanced filter", LastModified = "2009/11/12", Value = "Advanced filter")]
    public string AdvancedFilter => this[nameof (AdvancedFilter)];

    /// <summary>
    /// phrase: Advanced <em class="sfNote">(Synonyms, Name used in code)</em>
    /// </summary>
    [ResourceEntry("AdvancedSynonymsNameInCode", Description = "phrase: Advanced <em class='sfNote'>(Synonyms, Name used in code)</em>", LastModified = "2009/11/12", Value = "Advanced <em class='sfNote'>(Synonyms, Name used in code)</em>")]
    public string AdvancedSynonymsNameInCode => this[nameof (AdvancedSynonymsNameInCode)];

    /// <summary>word: Add</summary>
    [ResourceEntry("Add", Description = "word: Add", LastModified = "2010/02/25", Value = "Add")]
    public string Add => this[nameof (Add)];

    /// <summary>phrase: Add tags.</summary>
    [ResourceEntry("AddTags", Description = "phrase: Add tags", LastModified = "2009/11/13", Value = "Add tags")]
    public string AddTags => this[nameof (AddTags)];

    /// <summary>phrase: Expand all</summary>
    [ResourceEntry("ExpandAll", Description = "phrase: Expand all", LastModified = "2009/11/17", Value = "Expand all")]
    public string ExpandAll => this[nameof (ExpandAll)];

    /// <summary>phrase: Collapse all</summary>
    [ResourceEntry("CollapseAll", Description = "phrase: Collapse all", LastModified = "2009/11/17", Value = "Collapse all")]
    public string CollapseAll => this[nameof (CollapseAll)];

    /// <summary>phrase: Back to top</summary>
    [ResourceEntry("BackToTop", Description = "phrase: Back to top", LastModified = "2011/03/30", Value = "Back to top")]
    public string BackToTop => this[nameof (BackToTop)];

    /// <summary>label: {0} page uses this template</summary>
    [ResourceEntry("OnePageUsesTemplate", Description = "label", LastModified = "2009/11/18", Value = "{0} page uses this template")]
    public string OnePageUsesTemplate => this[nameof (OnePageUsesTemplate)];

    /// <summary>label: {0} pages use this template</summary>
    [ResourceEntry("ManyPagesUseTemplate", Description = "label", LastModified = "2009/11/18", Value = "{0} pages use this template")]
    public string ManyPagesUseTemplate => this[nameof (ManyPagesUseTemplate)];

    /// <summary>label: Use template</summary>
    [ResourceEntry("UseTemplate", Description = "Use template", LastModified = "2010/10/04", Value = "Use template")]
    public string UseTemplate => this[nameof (UseTemplate)];

    /// <summary>label: Don't use template (start from scratch)</summary>
    [ResourceEntry("DontUseTemplate", Description = "Don't use template (start from scratch)", LastModified = "2010/10/04", Value = "Don't use template (start from scratch)")]
    public string DontUseTemplate => this[nameof (DontUseTemplate)];

    /// <summary>
    /// label: Template changes will affect all of these pages:
    /// </summary>
    [ResourceEntry("TemplatePagesMsg", Description = "label", LastModified = "2009/11/18", Value = "Template changes will affect all of these pages:")]
    public string TemplatePagesMsg => this[nameof (TemplatePagesMsg)];

    /// <summary>phrase: Resource key</summary>
    [ResourceEntry("ResourceKey", Description = "phrase: Resource key", LastModified = "2009/11/18", Value = "Resource key")]
    public string ResourceKey => this[nameof (ResourceKey)];

    /// <summary>phrase: Selected items</summary>
    [ResourceEntry("SelectedItems", Description = "phrase: Selected items", LastModified = "2009/11/20", Value = "Selected items")]
    public string SelectedItems => this[nameof (SelectedItems)];

    /// <summary>phrase: all items</summary>
    [ResourceEntry("AllItems", Description = "phrase: All items", LastModified = "2009/11/20", Value = "All items")]
    public string AllItems => this[nameof (AllItems)];

    /// <summary>word: Advanced</summary>
    [ResourceEntry("Advanced", Description = "word: Advanced", LastModified = "2009/11/21", Value = "Advanced")]
    public string Advanced => this[nameof (Advanced)];

    /// <summary>word: Simpler</summary>
    [ResourceEntry("Simpler", Description = "word: Simpler", LastModified = "2009/11/21", Value = "Simple")]
    public string Simpler => this[nameof (Simpler)];

    /// <summary>word: Loading...</summary>
    [ResourceEntry("Loading", Description = "word: Loading", LastModified = "2009/11/21", Value = "Loading...")]
    public string Loading => this[nameof (Loading)];

    /// <summary>Word: Hide</summary>
    [ResourceEntry("Hide", Description = "word", LastModified = "2009/11/23", Value = "Hide")]
    public string Hide => this[nameof (Hide)];

    /// <summary>Phrase: Mark as Spam</summary>
    [ResourceEntry("MarkAsSpam", Description = "phrase", LastModified = "2009/11/23", Value = "Mark as Spam")]
    public string MarkAsSpam => this[nameof (MarkAsSpam)];

    /// <summary>Word: Publish</summary>
    [ResourceEntry("Publish", Description = "word", LastModified = "2009/11/23", Value = "Publish")]
    public string Publish => this[nameof (Publish)];

    /// <summary>Word: Author</summary>
    [ResourceEntry("Author", Description = "word", LastModified = "2009/11/23", Value = "Author")]
    public string Author => this[nameof (Author)];

    /// <summary>Word: Comment</summary>
    [ResourceEntry("Comment", Description = "word", LastModified = "2009/11/23", Value = "Comment")]
    public string Comment => this[nameof (Comment)];

    /// <summary>Phrase: Commented Item</summary>
    [ResourceEntry("CommentedItem", Description = "phrase", LastModified = "2009/11/23", Value = "Commented Item")]
    public string CommentedItem => this[nameof (CommentedItem)];

    /// <summary>Phrase: More actions...</summary>
    [ResourceEntry("MoreActions", Description = "phrase", LastModified = "2010/07/26", Value = "More actions")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>Phrase: More actions</summary>
    [ResourceEntry("MoreActionsLink", Description = "phrase", LastModified = "2010/07/21", Value = "More actions")]
    public string MoreActionsLink => this[nameof (MoreActionsLink)];

    /// <summary>Phrase: Mark as Favourite</summary>
    [ResourceEntry("MarkAsFavourite", Description = "phrase", LastModified = "2009/11/23", Value = "Mark as Favourite")]
    public string MarkAsFavourite => this[nameof (MarkAsFavourite)];

    /// <summary>Phrase: Block IP</summary>
    [ResourceEntry("BlockIP", Description = "phrase", LastModified = "2009/11/23", Value = "Block IP")]
    public string BlockIP => this[nameof (BlockIP)];

    /// <summary>Phrase: Block Email</summary>
    [ResourceEntry("BlockEmail", Description = "phrase", LastModified = "2009/11/23", Value = "Block Email")]
    public string BlockEmail => this[nameof (BlockEmail)];

    /// <summary>Word: Blocked</summary>
    [ResourceEntry("Blocked", Description = "word", LastModified = "2009/11/30", Value = "Blocked")]
    public string Blocked => this[nameof (Blocked)];

    /// <summary>Phrase: Unblock IP</summary>
    [ResourceEntry("UnblockIP", Description = "phrase", LastModified = "2009/11/30", Value = "Unblock IP")]
    public string UnblockIP => this[nameof (UnblockIP)];

    /// <summary>Phrase: Unblock Email</summary>
    [ResourceEntry("UnblockEmail", Description = "phrase", LastModified = "2009/11/30", Value = "Unblock Email")]
    public string UnblockEmail => this[nameof (UnblockEmail)];

    /// <summary>Phrase: Filter and Edit</summary>
    [ResourceEntry("FilterAndEdit", Description = "phrase", LastModified = "2009/11/23", Value = "Filter and Edit")]
    public string FilterAndEdit => this[nameof (FilterAndEdit)];

    /// <summary>Phrase: All Comments</summary>
    [ResourceEntry("AllComments", Description = "phrase", LastModified = "2009/11/23", Value = "All Comments")]
    public string AllComments => this[nameof (AllComments)];

    /// <summary>Word: Today</summary>
    [ResourceEntry("Today", Description = "word", LastModified = "2009/11/23", Value = "Today")]
    public string Today => this[nameof (Today)];

    /// <summary>Phrase: Edit Also</summary>
    [ResourceEntry("EditAlso", Description = "phrase", LastModified = "2010/07/26", Value = "Edit also")]
    public string EditAlso => this[nameof (EditAlso)];

    /// <summary>Phrase: Items in this Module</summary>
    [ResourceEntry("ItemsInThisModule", Description = "phrase", LastModified = "2009/11/23", Value = "Items in this Module")]
    public string ItemsInThisModule => this[nameof (ItemsInThisModule)];

    /// <summary>Phrase: Permissions for This Module</summary>
    [ResourceEntry("PermissionsForThisModule", Description = "phrase", LastModified = "2009/11/23", Value = "Permissions for This Module")]
    public string PermissionsForThisModule => this[nameof (PermissionsForThisModule)];

    /// <summary>Phrase: Return to All Comments for</summary>
    [ResourceEntry("ReturnToAllCommentsFor", Description = "phrase", LastModified = "2009/11/23", Value = "Return to All Comments for")]
    public string ReturnToAllCommentsFor => this[nameof (ReturnToAllCommentsFor)];

    /// <summary>Phrase: Edit a Comment</summary>
    [ResourceEntry("EditAComment", Description = "phrase", LastModified = "2009/11/23", Value = "Edit a Comment")]
    public string EditAComment => this[nameof (EditAComment)];

    /// <summary>Word: Preview</summary>
    [ResourceEntry("Preview", Description = "word", LastModified = "2009/11/23", Value = "Preview")]
    public string Preview => this[nameof (Preview)];

    /// <summary>Phrase: Pages displaying this item</summary>
    [ResourceEntry("DisplayPages", Description = "phrase", LastModified = "2018/08/20", Value = "Pages displaying this item")]
    public string DisplayPages => this[nameof (DisplayPages)];

    /// <summary>Word: Website</summary>
    [ResourceEntry("Website", Description = "word", LastModified = "2009/11/23", Value = "Website")]
    public string Website => this[nameof (Website)];

    /// <summary>Phrase: Posted on</summary>
    [ResourceEntry("PostedOn", Description = "phrase", LastModified = "2009/11/23", Value = "Posted on")]
    public string PostedOn => this[nameof (PostedOn)];

    /// <summary>Phrase: IP Address</summary>
    [ResourceEntry("IpAddress", Description = "phrase", LastModified = "2009/11/23", Value = "IP Address")]
    public string IpAddress => this[nameof (IpAddress)];

    /// <summary>Word: Published</summary>
    [ResourceEntry("Published", Description = "word", LastModified = "2009/11/23", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>Word: Hidden</summary>
    [ResourceEntry("Hidden", Description = "word", LastModified = "2009/11/23", Value = "Hidden")]
    public string Hidden => this[nameof (Hidden)];

    /// <summary>
    /// Phrase: Draft is not publically visible on the website
    /// </summary>
    [ResourceEntry("DraftIsNotPublicallyVisibleOnTheWebsite", Description = "phrase", LastModified = "2010/07/26", Value = "Draft is not publicly visible on the website")]
    public string DraftIsNotPublicallyVisibleOnTheWebsite => this[nameof (DraftIsNotPublicallyVisibleOnTheWebsite)];

    /// <summary>Word: Comments</summary>
    [ResourceEntry("Comments", Description = "word", LastModified = "2009/11/23", Value = "Comments")]
    public string Comments => this[nameof (Comments)];

    /// <summary>Word: Spam</summary>
    [ResourceEntry("Spam", Description = "word", LastModified = "2009/11/23", Value = "Spam")]
    public string Spam => this[nameof (Spam)];

    /// <summary>Word: Character</summary>
    [ResourceEntry("CharacterSingular", Description = "Word", LastModified = "2009/11/24", Value = "character")]
    public string CharacterSingular => this[nameof (CharacterSingular)];

    /// <summary>Word: Characters</summary>
    [ResourceEntry("CharacterPlural", Description = "Word", LastModified = "2009/11/24", Value = "characters")]
    public string CharacterPlural => this[nameof (CharacterPlural)];

    /// <summary>
    /// phrase: At least <strong>{0} {1}</strong>
    /// </summary>
    [ResourceEntry("PasswordLengthHint", Description = "phrase: At least <strong>{0} {1}</strong", LastModified = "2010/09/22", Value = "At least <strong>{0} {1}</strong>")]
    public string PasswordLengthHint => this[nameof (PasswordLengthHint)];

    /// <summary>
    /// phrase: At least <strong>{0} non alphanumeric {1}</strong>
    /// </summary>
    [ResourceEntry("PasswordAlphaNumCharactersHint", Description = "phrase: At least <strong>{0} non alphanumeric {1}</strong>", LastModified = "2010/09/22", Value = "At least <strong>{0} non alphanumeric {1}</strong>")]
    public string PasswordAlphaNumCharactersHint => this[nameof (PasswordAlphaNumCharactersHint)];

    /// <summary>Phrase: The password must be at least {0} {1} long</summary>
    [ResourceEntry("ThePasswordMustBeAtLeastLong", Description = "The password must be at least \"{0}\" \"{1}\" long", LastModified = "2009/11/24", Value = "The password must be at least {0} {1} long")]
    public string ThePasswordMustBeAtLeastLong => this[nameof (ThePasswordMustBeAtLeastLong)];

    /// <summary>
    /// Phrase: and must contain no less than {0} non alphanumeric {1}
    /// </summary>
    [ResourceEntry("AndMustContainNoLessThanNonAlphanumeric", Description = "phrase: and must contain no less than \"{0}\" non alphanumeric \"{1}\"]", LastModified = "2009/11/24", Value = "and must contain no less than {0} non alphanumeric {1}")]
    public string AndMustContainNoLessThanNonAlphanumeric => this[nameof (AndMustContainNoLessThanNonAlphanumeric)];

    /// <summary>word: Simple</summary>
    [ResourceEntry("Simple", Description = "word: Simple", LastModified = "2009/11/24", Value = "Simple")]
    public string Simple => this[nameof (Simple)];

    /// <summary>phrase: No sections have been created yet.</summary>
    [ResourceEntry("NoCreatedSections", Description = "phrase", LastModified = "2009/11/25", Value = "No sections have been created yet.")]
    public string NoCreatedSections => this[nameof (NoCreatedSections)];

    /// <summary>
    /// phrase: <em>No section selected</em>
    /// </summary>
    [ResourceEntry("NoSectionSelected", Description = "phrase", LastModified = "2009/11/25", Value = "<em>No section selected</em>")]
    public string NoSectionSelected => this[nameof (NoSectionSelected)];

    /// <summary>
    /// phrase: The page title that is displayed in the browser title bar.
    /// </summary>
    [ResourceEntry("TitleToolTip", Description = "phrase", LastModified = "2009/12/08", Value = "The page title that is displayed in the browser title bar.")]
    public string TitleToolTip => this[nameof (TitleToolTip)];

    /// <summary>
    /// phrase: This text describes the content of your page. Invisible to the readers, it can boost your rankings on some engines.
    /// </summary>
    [ResourceEntry("DescriptionToolTip", Description = "phrase", LastModified = "2009/12/08", Value = "This text describes the content of your page. Invisible to the readers, it can boost your rankings on some engines.")]
    public string DescriptionToolTip => this[nameof (DescriptionToolTip)];

    /// <summary>
    /// phrase: Metadata keywords are used by browsers or search engines to find and categorize your page.
    /// </summary>
    [ResourceEntry("KeywordsToolTip", Description = "phrase", LastModified = "2009/12/08", Value = "Metadata keywords are used by browsers or search engines to find and categorize your page.")]
    public string KeywordsToolTip => this[nameof (KeywordsToolTip)];

    /// <summary>
    /// phrase: Separate keywords with spaces. <strong>Example:</strong> music guitar song
    /// </summary>
    [ResourceEntry("KeywordsExampleText", Description = "phrase", LastModified = "2009/12/08", Value = "Separate keywords with spaces. <strong>Example:</strong> music guitar song")]
    public string KeywordsExampleText => this[nameof (KeywordsExampleText)];

    /// <summary>phrase: About Title</summary>
    [ResourceEntry("AboutTitle", Description = "phrase", LastModified = "2009/12/08", Value = "About Title")]
    public string AboutTitle => this[nameof (AboutTitle)];

    /// <summary>Word: About</summary>
    [ResourceEntry("About", Description = "Word: About", LastModified = "2011/03/07", Value = "About")]
    public string About => this[nameof (About)];

    /// <summary>phrase: About Description</summary>
    [ResourceEntry("AboutDescription", Description = "phrase", LastModified = "2009/12/08", Value = "About Description")]
    public string AboutDescription => this[nameof (AboutDescription)];

    /// <summary>phrase: About Keywords</summary>
    [ResourceEntry("AboutKeywords", Description = "phrase", LastModified = "2009/12/08", Value = "About Keywords")]
    public string AboutKeywords => this[nameof (AboutKeywords)];

    /// <summary>phrase: Edit label</summary>
    [ResourceEntry("EditLabel", Description = "phrase: Edit label", LastModified = "2009/12/08", Value = "Edit label")]
    public string EditLabel => this[nameof (EditLabel)];

    /// <summary>phrase: OK, Continue</summary>
    [ResourceEntry("Continue", Description = "phrase", LastModified = "2009/12/09", Value = "OK, Continue")]
    public string Continue => this[nameof (Continue)];

    /// <summary>Label: Continue</summary>
    [ResourceEntry("ContinueCommand", Description = "Label: Continue", LastModified = "2010/11/05", Value = "Continue")]
    public string ContinueCommand => this[nameof (ContinueCommand)];

    /// <summary>Phrase: Back to Templates</summary>
    [ResourceEntry("BackToTemplates", Description = "Phrase: Back to Templates", LastModified = "2010/10/12", Value = "Back to Templates")]
    public string BackToTemplates => this[nameof (BackToTemplates)];

    /// <summary>Caution!</summary>
    [ResourceEntry("BeCareful", Description = "phrase", LastModified = "2009/12/09", Value = "Caution!")]
    public string BeCareful => this[nameof (BeCareful)];

    /// <summary>
    /// Backend Pages are pages forming this site backend interface (the system you are using now).
    /// You are able to create new backend pages, edit or delete existing ones. So, you can easily destroy the whole backend interface
    /// if you don't have the expertise needed.
    /// </summary>
    [ResourceEntry("BackendPagesDefaultMsg", Description = "The message which is displayed when the BackendPages page is accessed.", LastModified = "2009/12/09", Value = "Backend Pages are pages forming this site backend interface (the system you are using now). <br /> You are able to create new backend pages, edit or delete existing ones. <br/> So, <strong class='sfEmphazie'>you can easily destroy the whole backend interface</strong> if you don't have the expertise needed.")]
    public string BackendPagesDefaultMsg => this[nameof (BackendPagesDefaultMsg)];

    /// <summary>phrase: Don't show me this message again</summary>
    [ResourceEntry("DontShowMe", Description = "phrase", LastModified = "2009/12/09", Value = "Don't show me this message again")]
    public string DontShowMe => this[nameof (DontShowMe)];

    /// <summary>word: Contains</summary>
    [ResourceEntry("Contains", Description = "word: Contains", LastModified = "2009/12/09", Value = "Contains")]
    public string Contains => this[nameof (Contains)];

    /// <summary>word: Actions</summary>
    [ResourceEntry("Actions", Description = "word: Actions", LastModified = "2009/12/12", Value = "Actions")]
    public string Actions => this[nameof (Actions)];

    /// <summary>phrase: No items empty</summary>
    [ResourceEntry("NoItemsEmpty", Description = "phrase: No items (empty)", LastModified = "2009/12/12", Value = "No items (empty)")]
    public string NoItemsEmpty => this[nameof (NoItemsEmpty)];

    /// <summary>used as in '1 more item'</summary>
    [ResourceEntry("MoreItem", Description = "used as in '1 more item'", LastModified = "2009/12/12", Value = " more item")]
    public string MoreItem => this[nameof (MoreItem)];

    /// <summary>used as in '4 more items'</summary>
    [ResourceEntry("MoreItems", Description = "used as in '4 more items'", LastModified = "2009/12/12", Value = " more items")]
    public string MoreItems => this[nameof (MoreItems)];

    /// <summary>word: Permissions</summary>
    [ResourceEntry("Permissions", Description = "word: Permissions", LastModified = "2009/12/12", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>phrase: For developers</summary>
    [ResourceEntry("ForDevelopers", Description = "phrase: For developers", LastModified = "2009/12/12", Value = "For developers")]
    public string ForDevelopers => this[nameof (ForDevelopers)];

    /// <summary>phrase: Edit properties</summary>
    [ResourceEntry("EditProperties", Description = "phrase: Edit properties", LastModified = "2009/12/12", Value = "Edit properties")]
    public string EditProperties => this[nameof (EditProperties)];

    /// <summary>word: Duplicate</summary>
    [ResourceEntry("Duplicate", Description = "word: Duplicate", LastModified = "2009/12/13", Value = "Duplicate")]
    public string Duplicate => this[nameof (Duplicate)];

    /// <summary>phrase: Bulk operations</summary>
    [ResourceEntry("BulkOperations", Description = "phrase: Bulk operations", LastModified = "2009/12/14", Value = "Bulk operations")]
    public string BulkOperations => this[nameof (BulkOperations)];

    /// <summary>phrase: Bulk edit</summary>
    [ResourceEntry("TagsBulkEdit", Description = "phrase: Bulk edit", LastModified = "2009/01/11", Value = "Bulk edit <span class='sfExtraInfo'>(URLs and synonyms)</span>")]
    public string TagsBulkEdit => this[nameof (TagsBulkEdit)];

    /// <summary>Confirmation message for bulk delete.</summary>
    [ResourceEntry("AreYouSureYouWantToDeleteSelectedItems", Description = "Confirmation message for bulk delete.", LastModified = "2009/12/14", Value = "Are you sure you want to delete selected items?")]
    public string AreYouSureYouWantToDeleteSelectedItems => this[nameof (AreYouSureYouWantToDeleteSelectedItems)];

    /// <summary>phrase: Create new item</summary>
    [ResourceEntry("CreateNewItem", Description = "phrase: Create new item", LastModified = "2009/12/17", Value = "Create new item")]
    public string CreateNewItem => this[nameof (CreateNewItem)];

    /// <summary>phrase: Save item</summary>
    [ResourceEntry("SaveItem", Description = "phrase: Save item", LastModified = "2009/12/17", Value = "Save item")]
    public string SaveItem => this[nameof (SaveItem)];

    /// <summary>phrase: Save and add another item</summary>
    [ResourceEntry("SaveAndAddAnotherItem", Description = "phrase: Save and add another item", LastModified = "2009/12/17", Value = "Save and add another item")]
    public string SaveAndAddAnotherItem => this[nameof (SaveAndAddAnotherItem)];

    /// <summary>phrase: Use Selected</summary>
    [ResourceEntry("UseSelected", Description = "phrase: Use Selected", LastModified = "2009/12/17", Value = "Use Selected")]
    public string UseSelected => this[nameof (UseSelected)];

    /// <summary>View only App_Master folder</summary>
    [ResourceEntry("ViewAppMaster", Description = "phrase", LastModified = "2009/12/17", Value = "View only App_Master folder")]
    public string ViewAppMaster => this[nameof (ViewAppMaster)];

    /// <summary>All Folders</summary>
    [ResourceEntry("AllFolders", Description = "phrase", LastModified = "2009/12/17", Value = "All Folders")]
    public string AllFolders => this[nameof (AllFolders)];

    /// <summary>Create a template from selected master page</summary>
    [ResourceEntry("CreateTemplFromMasterPage", Description = "phrase", LastModified = "2009/12/17", Value = "Create a template from selected master page")]
    public string CreateTemplFromMasterPage => this[nameof (CreateTemplFromMasterPage)];

    /// <summary>Browse other folders</summary>
    [ResourceEntry("BrowseOtherFolders", Description = "phrase", LastModified = "2009/12/19", Value = "Browse other folders")]
    public string BrowseOtherFolders => this[nameof (BrowseOtherFolders)];

    /// <summary>Folder: {0}</summary>
    [ResourceEntry("AppMasterFolder", Description = "phrase", LastModified = "2014/03/04", Value = "Folder: <strong>{0}</strong>")]
    public string AppMasterFolder => this[nameof (AppMasterFolder)];

    /// <summary>word: history</summary>
    [ResourceEntry("History", Description = "word: history", LastModified = "2009/12/20", Value = "History")]
    public string History => this[nameof (History)];

    /// <summary>word: Unpublish</summary>
    [ResourceEntry("Unpublish", Description = "word: Unpublish", LastModified = "2009/12/21", Value = "Unpublish")]
    public string Unpublish => this[nameof (Unpublish)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "phrase: Set permissions", LastModified = "2009/12/21", Value = "Set permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>phrase: Set permissions for {0}.</summary>
    [ResourceEntry("SetPermissionsForItem", Description = "phrase: Set permissions for {0}.", LastModified = "2010/01/14", Value = "Set <strong>permissions</strong> for {0}.")]
    public string SetPermissionsForItem => this[nameof (SetPermissionsForItem)];

    /// <summary>phrase: Review history</summary>
    [ResourceEntry("ReviewHistory", Description = "phrase: Review history", LastModified = "2009/12/21", Value = "Review history")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>
    /// phrase: No master files have been uploaded to {0} folder yet
    /// </summary>
    [ResourceEntry("NoMasterFiles", Description = "phrase", LastModified = "2014/03/04", Value = "No master files have been uploaded to <em>{0}</em> folder yet")]
    public string NoMasterFiles => this[nameof (NoMasterFiles)];

    /// <summary>phrase: How to upload</summary>
    [ResourceEntry("HowToUpload", Description = "phrase", LastModified = "2009/12/22", Value = "How to upload")]
    public string HowToUpload => this[nameof (HowToUpload)];

    /// <summary>phrase: URL</summary>
    [ResourceEntry("UrlName", Description = "phrase", LastModified = "2010/01/05", Value = "URL")]
    public string UrlName => this[nameof (UrlName)];

    /// <summary>phrase: Applied to</summary>
    [ResourceEntry("AppliedTo", Description = "phrase", LastModified = "2010/01/05", Value = "Applied to")]
    public string AppliedTo => this[nameof (AppliedTo)];

    /// <summary>word: Move</summary>
    [ResourceEntry("Move", Description = "word: Move", LastModified = "2010/01/07", Value = "Move")]
    public string Move => this[nameof (Move)];

    /// <summary>word: Move...</summary>
    [ResourceEntry("MoveEllipsis", Description = "word: Move...", LastModified = "2010/01/07", Value = "Move...")]
    public string MoveEllipsis => this[nameof (MoveEllipsis)];

    /// <summary>word: Analytics...</summary>
    [ResourceEntry("Analytics", Description = "word: Analytics...", LastModified = "2013/08/27", Value = "Analytics...")]
    public string Analytics => this[nameof (Analytics)];

    /// <summary>word: Move...</summary>
    [ResourceEntry("Stats", Description = "word: Stats", LastModified = "2013/08/27", Value = "Stats")]
    public string Stats => this[nameof (Stats)];

    /// <summary>word: Top (direction)</summary>
    [ResourceEntry("Top", Description = "word: Top (direction)", LastModified = "2011/03/28", Value = "Top")]
    public string Top => this[nameof (Top)];

    /// <summary>word: Bottom (direction)</summary>
    [ResourceEntry("Bottom", Description = "word: Bottom (direction)", LastModified = "2011/03/28", Value = "Bottom")]
    public string Bottom => this[nameof (Bottom)];

    /// <summary>word: Up (direction)</summary>
    [ResourceEntry("Up", Description = "word: Up (direction)", LastModified = "2010/01/07", Value = "Up")]
    public string Up => this[nameof (Up)];

    /// <summary>word: Down (direction)</summary>
    [ResourceEntry("Down", Description = "word: Down (direction)", LastModified = "2010/01/07", Value = "Down")]
    public string Down => this[nameof (Down)];

    /// <summary>phrase: No {0} have been created yet</summary>
    [ResourceEntry("NoItemsHaveBeenCreatedYet", Description = "phrase: No {0} have been created yet", LastModified = "2010/08/02", Value = "No {0} have been created yet")]
    public string NoItemsHaveBeenCreatedYet => this[nameof (NoItemsHaveBeenCreatedYet)];

    /// <summary>phrase: Create a {0}</summary>
    [ResourceEntry("CreateYourFirstItem", Description = "phrase: Create a {0}", LastModified = "2010/08/02", Value = "Create a {0}")]
    public string CreateYourFirstItem => this[nameof (CreateYourFirstItem)];

    /// <summary>word: items</summary>
    [ResourceEntry("Items", Description = "word: items", LastModified = "2010/01/15", Value = "items")]
    public string Items => this[nameof (Items)];

    /// <summary>word: item</summary>
    [ResourceEntry("Item", Description = "word: item", LastModified = "2011/05/13", Value = "item")]
    public string Item => this[nameof (Item)];

    /// <summary>phrase: Change parent of {0} selected items</summary>
    [ResourceEntry("ChangeParentOfSelectedItems", Description = "phrase: Change parent of {0} selected items.", LastModified = "2010/01/16", Value = "Change parent <strong>of {0} selected items</strong>")]
    public string ChangeParentOfSelectedItems => this[nameof (ChangeParentOfSelectedItems)];

    /// <summary>phrase: Done with selecting</summary>
    [ResourceEntry("DoneWithSelecting", Description = "phrase: Done selecting", LastModified = "2010/08/02", Value = "Done selecting")]
    public string DoneWithSelecting => this[nameof (DoneWithSelecting)];

    /// <summary>phrase: Move up (Listbox button)</summary>
    [ResourceEntry("MoveUp", Description = "phrase: Move up (Listbox button)", LastModified = "2010/01/27", Value = "Move up")]
    public string MoveUp => this[nameof (MoveUp)];

    /// <summary>phrase: Move down (Listbox button)</summary>
    [ResourceEntry("MoveDown", Description = "phrase: Move down (Listbox button)", LastModified = "2010/01/27", Value = "Move down")]
    public string MoveDown => this[nameof (MoveDown)];

    /// <summary>
    /// phrase: {items count} are about to be deleted. Do you want to continue?
    /// </summary>
    [ResourceEntry("ItemsAreAboutToBeDeleted", Description = "phrase:{0} items are about to be deleted. Do you want to continue?", LastModified = "2010/01/27", Value = "{0} items are about to be deleted. Do you want to continue?")]
    public string ItemsAreAboutToBeDeleted => this[nameof (ItemsAreAboutToBeDeleted)];

    /// <summary>Confirmation message for single item deleting.</summary>
    [ResourceEntry("AreYouSureYouWantToDeleteItem", Description = "Confirmation message for single item deleting.", LastModified = "2010/02/08", Value = "Are you sure you want to delete this item?")]
    public string AreYouSureYouWantToDeleteItem => this[nameof (AreYouSureYouWantToDeleteItem)];

    /// <summary>phrase: Select from existing</summary>
    [ResourceEntry("SelectFromExisting", Description = "phrase: Select from existing", LastModified = "2010/02/25", Value = "Select from existing")]
    public string SelectFromExisting => this[nameof (SelectFromExisting)];

    /// <summary>phrase: Close existing</summary>
    [ResourceEntry("CloseExisting", Description = "phrase: Close existing", LastModified = "2010/02/26", Value = "Close existing")]
    public string CloseExisting => this[nameof (CloseExisting)];

    /// <summary>phrase: Show all</summary>
    [ResourceEntry("ShowAll", Description = "phrase: Show all", LastModified = "2010/02/25", Value = "Show all")]
    public string ShowAll => this[nameof (ShowAll)];

    /// <summary>Word: Draft</summary>
    [ResourceEntry("Draft", Description = "word", LastModified = "2010/02/26", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    /// <summary>Word: Drafts</summary>
    [ResourceEntry("Drafts", Description = "word", LastModified = "2010/02/26", Value = "Drafts")]
    public string Drafts => this[nameof (Drafts)];

    /// <summary>word: Schedule</summary>
    [ResourceEntry("Schedule", Description = "word: Schedule", LastModified = "2010/04/26", Value = "Schedule")]
    public string Schedule => this[nameof (Schedule)];

    /// <summary>Word: Scheduled</summary>
    [ResourceEntry("Scheduled", Description = "word", LastModified = "2010/02/26", Value = "Scheduled")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>Word: Scheduled...</summary>
    [ResourceEntry("ScheduledDotDotDot", Description = "word", LastModified = "2010/02/26", Value = "Scheduled...")]
    public string ScheduledDotDotDot => this[nameof (ScheduledDotDotDot)];

    /// <summary>phrase: Thumbnail</summary>
    [ResourceEntry("Thumbnail", Description = "phrase: Thumbnail", LastModified = "2010/10/08", Value = "Thumbnail")]
    public string Thumbnail => this[nameof (Thumbnail)];

    /// <summary>phrase: Click to select</summary>
    [ResourceEntry("ClickToSelect", Description = "phrase: Click to select", LastModified = "2010/03/04", Value = "Click to select")]
    public string ClickToSelect => this[nameof (ClickToSelect)];

    /// <summary>phrase: Show only most popular</summary>
    [ResourceEntry("ShowOnlyMostPopular", Description = "phrase: Show only most popular", LastModified = "2010/03/04", Value = "Show only most popular")]
    public string ShowOnlyMostPopular => this[nameof (ShowOnlyMostPopular)];

    /// <summary>phrase: Most popular</summary>
    [ResourceEntry("MostPopular", Description = "phrase: Most popular", LastModified = "2010/03/04", Value = "Most popular")]
    public string MostPopular => this[nameof (MostPopular)];

    /// <summary>phrase: Start typing and {0} will be hinted</summary>
    [ResourceEntry("StartTypingAndItemsWillBeHinted", Description = "phrase: Start typing and will be hinted", LastModified = "2010/03/04", Value = "Start typing and will be hinted")]
    public string StartTypingAndItemsWillBeHinted => this[nameof (StartTypingAndItemsWillBeHinted)];

    /// <summary>phrase: Opening existing...</summary>
    [ResourceEntry("OpeningExisting", Description = "phrase: Opening existing...", LastModified = "2010/03/04", Value = "Opening existing...")]
    public string OpeningExisting => this[nameof (OpeningExisting)];

    /// <summary>phrase: Opening all...</summary>
    [ResourceEntry("OpeningAll", Description = "phrase: Opening all...", LastModified = "2010/03/04", Value = "Opening all...")]
    public string OpeningAll => this[nameof (OpeningAll)];

    /// <summary>phrase: -- no parent --</summary>
    [ResourceEntry("NoParent", Description = "phrase: -- no parent --", LastModified = "2010/01/16", Value = "-- no parent --")]
    public string NoParent => this[nameof (NoParent)];

    /// <summary>Running Since</summary>
    [ResourceEntry("RunningSince", Description = "The label for 'Running Since' system information screen.", LastModified = "2010/02/08", Value = "Running since")]
    public string RunningSince => this[nameof (RunningSince)];

    /// <summary>Saving ...</summary>
    [ResourceEntry("SavingImgAlt", Description = "Alternative text for loading/saving image in create/edit item form", LastModified = "2010/02/08", Value = "Saving ...")]
    public string SavingImgAlt => this[nameof (SavingImgAlt)];

    /// <summary>The licensed user limit is reached.</summary>
    /// <value>The user limit reached.</value>
    [ResourceEntry("UserLimitReached", Description = "Licensed user limit was reached.", LastModified = "2010/03/12", Value = "Maximum allowed users limit was reached.")]
    public string UserLimitReached => this[nameof (UserLimitReached)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("UserNotFound", Description = "User not found in any provider.", LastModified = "2010/03/12", Value = "User Not Found")]
    public string UserNotFound => this[nameof (UserNotFound)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("UserAlreadyLoggedIn", Description = "User already logged in.", LastModified = "2010/03/12", Value = "User Already Logged In")]
    public string UserAlreadyLoggedIn => this[nameof (UserAlreadyLoggedIn)];

    /// <summary>User not found in any provider.</summary>
    [ResourceEntry("UserRevoked", Description = "User revoked", LastModified = "2012/01/05", Value = "Either your user was deleted or user rights to access or modify the site were changed.")]
    public string UserRevoked => this[nameof (UserRevoked)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("UserLoggedFromDifferentIp", Description = "User already logged from different IP address.", LastModified = "2010/03/12", Value = "User is logged in from different IP address")]
    public string UserLoggedFromDifferentIp => this[nameof (UserLoggedFromDifferentIp)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("SessionExpired", Description = "Your server session has expired.", LastModified = "2010/03/12", Value = "Server Session Expired")]
    public string SessionExpired => this[nameof (SessionExpired)];

    /// <summary>
    /// Message displayed when a request is made after, e.g., the useer has logged off from another browser tab.
    /// </summary>
    /// <value>You have been logged out from another computer or browser window.</value>
    [ResourceEntry("UserLoggedOff", Description = "Server session expired.", LastModified = "2011/01/06", Value = "You have been logged out from another computer or browser window.")]
    public string UserLoggedOff => this[nameof (UserLoggedOff)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("UserLoggedFromDifferentComputer", Description = "User already logged on a different computer", LastModified = "2010/03/12", Value = "User already logged on a different computer")]
    public string UserLoggedFromDifferentComputer => this[nameof (UserLoggedFromDifferentComputer)];

    /// <summary>User not found in any provider.</summary>
    /// <value>The user not found.</value>
    [ResourceEntry("UnknownReason", Description = "Unknown reason for deny login or logoff", LastModified = "2010/03/12", Value = "You are logged off. Contact the system administrator.")]
    public string UnknownReason => this[nameof (UnknownReason)];

    /// <summary>Need administrative rights to logout users</summary>
    /// <value>NeedAdminRights</value>
    [ResourceEntry("NeedAdminRights", Description = "Need administrative rights to logout users", LastModified = "2010/03/12", Value = "You need administrative rights to logout users")]
    public string NeedAdminRights => this[nameof (NeedAdminRights)];

    /// <summary>Gets the self logof label.</summary>
    /// <value>The self logof label.</value>
    [ResourceEntry("SelfLogoffLabel", Description = "You already logged in on a different computer or browser.", LastModified = "2010/03/12", Value = "Someone is <strong>already using</strong> this username and password from another computer or browser. <br />To proceed, you need to <strong>log him/her off</strong>.")]
    public string SelfLogoffLabel => this[nameof (SelfLogoffLabel)];

    /// <summary>Gets the deny logon mesage.</summary>
    /// <value>The deny logon mesage.</value>
    [ResourceEntry("DenyLogonMesage", Description = "Denied log in", LastModified = "2010/03/12", Value = "Sorry, you cannot log in because the maximum user quota has been reached.")]
    public string DenyLogonMesage => this[nameof (DenyLogonMesage)];

    /// <summary>Gets the login retry message.</summary>
    /// <value>The login retry message.</value>
    [ResourceEntry("LoginRetryMessage", Description = "Login Retry Message", LastModified = "2010/03/12", Value = "You can wait for a user to log off and try to login again later.")]
    public string LoginRetryMessage => this[nameof (LoginRetryMessage)];

    /// <summary>Gets the user not selected.</summary>
    /// <value>The user not selected.</value>
    [ResourceEntry("UserNotSelected", Description = "User Not Selected Message", LastModified = "2010/03/12", Value = "Please, select a user to log out.")]
    public string UserNotSelected => this[nameof (UserNotSelected)];

    /// <summary>Gets the logout user confirm.</summary>
    /// <value>The logout user confirm.</value>
    [ResourceEntry("LogoutUserConfirm", Description = "Logout User Confirm", LastModified = "2010/03/12", Value = "The selected user will be logged out and you will be logged in, continue?")]
    public string LogoutUserConfirm => this[nameof (LogoutUserConfirm)];

    /// <summary>Gets the logout user confirm.</summary>
    /// <value>The logout user confirm.</value>
    [ResourceEntry("SelfLogoutUserConfirm", Description = "Self Logout User Confirm", LastModified = "2010/03/12", Value = "The other user will be logged off from the system. Are you sure you want to continue? ")]
    public string SelfLogoutUserConfirm => this[nameof (SelfLogoutUserConfirm)];

    /// <summary>Gets the user limit label.</summary>
    /// <value>The user limit label.</value>
    [ResourceEntry("UserLimitLabel", Description = "User Limit Label", LastModified = "2010/03/12", Value = "Maximum simultaneous users allowed limit is reached.")]
    public string UserLimitLabel => this[nameof (UserLimitLabel)];

    /// <summary>Gets the logout button text.</summary>
    /// <value>The logout button text.</value>
    [ResourceEntry("LogoutButtonText", Description = "Logout Button Text", LastModified = "2010/03/12", Value = "Force Logout selected user and login me")]
    public string LogoutButtonText => this[nameof (LogoutButtonText)];

    /// <summary>Gets the logout button text.</summary>
    /// <value>The logout button text.</value>
    [ResourceEntry("SelftLogoutButtonText", Description = "Self Logout Button Text", LastModified = "2010/03/12", Value = "Log the other user off and enter")]
    public string SelftLogoutButtonText => this[nameof (SelftLogoutButtonText)];

    /// <summary>Gets the label for filter logged in users.</summary>
    /// <value>The filter logged in users text.</value>
    [ResourceEntry("FilterLoggedInUsersText", Description = "Message shown in users page.", LastModified = "2010/03/12", Value = "Online backend users")]
    public string FilterLoggedInUsersText => this[nameof (FilterLoggedInUsersText)];

    /// <summary>Gets the label for filter logged in users.</summary>
    /// <value>The filter logged in users text.</value>
    [ResourceEntry("UserOnline", Description = "Message shown in users page.", LastModified = "2010/03/18", Value = "Online")]
    public string UserOnline => this[nameof (UserOnline)];

    /// <summary>Gets the label for filter logged in users.</summary>
    /// <value>The filter logged in users text.</value>
    [ResourceEntry("UserOffline", Description = "Message shown in users page.", LastModified = "2010/03/18", Value = "Offline")]
    public string UserOffline => this[nameof (UserOffline)];

    /// <summary>word: Upload</summary>
    [ResourceEntry("Upload", Description = "word: Upload {0}", LastModified = "2011/01/14", Value = "Upload {0}")]
    public string Upload => this[nameof (Upload)];

    /// <summary>word: Multiple {0} can be uploaded at a time</summary>
    [ResourceEntry("MultipleCanBeUploadedAtATime", Description = "word: Multiple {0} can be uploaded at a time", LastModified = "2010/09/30", Value = "Multiple {0} can be uploaded at a time")]
    public string MultipleCanBeUploadedAtATime => this[nameof (MultipleCanBeUploadedAtATime)];

    /// <summary>word: Status / Page</summary>
    [ResourceEntry("StatusPage", Description = "word: Status / Page", LastModified = "2010/09/30", Value = "Status / Page")]
    public string StatusPage => this[nameof (StatusPage)];

    /// <summary>word: Only one {0} can be uploaded at a time</summary>
    [ResourceEntry("OnlyOneCanBeUploadedAtATime", Description = "word: Only one {0} can be uploaded at a time", LastModified = "2010/09/30", Value = "Only one {0} can be uploaded at a time")]
    public string OnlyOneCanBeUploadedAtATime => this[nameof (OnlyOneCanBeUploadedAtATime)];

    /// <summary>phrase: Create a new {0}.</summary>
    [ResourceEntry("CreateANewItem", Description = "phrase: Create a new {0}.", LastModified = "2010/03/25", Value = "Create a new {0}.")]
    public string CreateANewItem => this[nameof (CreateANewItem)];

    /// <summary>
    /// "Message shown in users edit screen to confirm user log-off
    /// </summary>
    [ResourceEntry("ForceLogoutUserConfirmation", Description = "Message shown in users edit screen to confirm user log-off.", LastModified = "2010/03/18", Value = "You are about to force logout {0}, continue ?")]
    public string ForceLogoutUserAlert => this["ForceLogoutUserConfirmation"];

    /// <summary>Translated message, similar to "Force logout"</summary>
    /// <value>Message shown in users edit screen as the text of a button that logs out a user.</value>
    [ResourceEntry("ForceLogout", Description = "Message shown in users edit screen as the text of a button that logs out a user.", LastModified = "2010/10/11", Value = "Force logout")]
    public string ForceLogout => this[nameof (ForceLogout)];

    /// <summary>Label shown in taxonomy control designer modes</summary>
    [ResourceEntry("NavigationCloud", Description = "Label shown in taxonomy control designer modes", LastModified = "2011/09/08", Value = "Cloud")]
    public string NavigationCloud => this["Cloud"];

    /// <summary>Label shown in taxonomy control designer modes</summary>
    [ResourceEntry("NavigationMenu", Description = "Label shown in taxonomy control designer modes", LastModified = "2011/10/28", Value = "Horizontal with drop-down menus")]
    public string NavigationMenu => this["Menu"];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeHorizontalSimple", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Horizontal")]
    public string NavigationModeHorizontalSimple => this[nameof (NavigationModeHorizontalSimple)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeHorizontalDropDownMenu", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Horizontal with drop down menus")]
    public string NavigationModeHorizontalDropDownMenu => this[nameof (NavigationModeHorizontalDropDownMenu)];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeHorizontalTabs", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Horizontal with tabs")]
    public string NavigationModeHorizontalTabs => this["NavigationModeHorziontalTabs"];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeVerticalSimple", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Vertical")]
    public string NavigationModeVerticalSimple => this[nameof (NavigationModeVerticalSimple)];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeVerticalTree", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Tree (vertical with sub-levels)")]
    public string NavigationModeVerticalTree => this[nameof (NavigationModeVerticalTree)];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeSiteMapInRows", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Sitemap divided in rows")]
    public string NavigationModeSiteMapInRows => this[nameof (NavigationModeSiteMapInRows)];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeSiteMapInColumns", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Sitemap divided in columns")]
    public string NavigationModeSiteMapInColumns => this[nameof (NavigationModeSiteMapInColumns)];

    /// <summary>"Label shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeCustomNavigation", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Custom Navigation")]
    public string NavigationModeCustomNavigation => this[nameof (NavigationModeCustomNavigation)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationModeHeading", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "What type of navigation to use?")]
    public string NavigationModeHeading => this[nameof (NavigationModeHeading)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSettingsHeading", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Fine tune the selected type")]
    public string NavigationSettingsHeading => this[nameof (NavigationSettingsHeading)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeading", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Links to which pages to display?")]
    public string NavigationSelectionHeading => this[nameof (NavigationSelectionHeading)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingHorizontalSimple", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Links to which pages to display?|")]
    public string NavigationSelectionHeadingHorizontalSimple => this[nameof (NavigationSelectionHeadingHorizontalSimple)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingHorizontalDropDownMenu", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "In the top bar: Links to which pages to display?|Their child pages will be in the drop down menus")]
    public string NavigationSelectionHeadingHorizontalDropDownMenu => this[nameof (NavigationSelectionHeadingHorizontalDropDownMenu)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingHorizontalTabs", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "In the top bar: Links to which pages to display?|Their child pages will be in a strip below")]
    public string NavigationSelectionHeadingHorizontalTabs => this[nameof (NavigationSelectionHeadingHorizontalTabs)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingVerticalSimple", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Links to which pages to display?|")]
    public string NavigationSelectionHeadingVerticalSimple => this[nameof (NavigationSelectionHeadingVerticalSimple)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingVerticalTree", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Initially: Links to which pages to display?|Their child pages will be expanded")]
    public string NavigationSelectionHeadingVerticalTree => this[nameof (NavigationSelectionHeadingVerticalTree)];

    /// <summary>phrase: Their child pages will be expanded</summary>
    [ResourceEntry("TheirChildPagesWillBeExpanded", Description = "phrase: Their child pages will be expanded", LastModified = "2010/10/18", Value = "Their child pages will be expanded")]
    public string TheirChildPagesWillBeExpanded => this[nameof (TheirChildPagesWillBeExpanded)];

    /// <summary>phrase: No page is selected</summary>
    [ResourceEntry("NoPageIsSelected", Description = "phrase: No page is selected", LastModified = "2010/10/18", Value = "No page is selected")]
    public string NoPageIsSelected => this[nameof (NoPageIsSelected)];

    /// <summary>phrase: No pages are selected</summary>
    [ResourceEntry("NoPagesAreSelected", Description = "phrase: No pages are selected", LastModified = "2010/10/18", Value = "No pages are selected")]
    public string NoPagesAreSelected => this[nameof (NoPagesAreSelected)];

    /// <summary>phrase: How many levels to expand initially</summary>
    [ResourceEntry("HowManyLevelsToExpandInitially", Description = "phrase: How many levels to expand initially", LastModified = "2010/10/18", Value = "How many levels to expand initially")]
    public string HowManyLevelsToExpandInitially => this[nameof (HowManyLevelsToExpandInitially)];

    /// <summary>phrase: All existing levels</summary>
    [ResourceEntry("AllExistingLevels", Description = "phrase: All existing levels", LastModified = "2010/10/18", Value = "All existing levels")]
    public string AllExistingLevels => this[nameof (AllExistingLevels)];

    /// <summary>phrase: level(s)</summary>
    [ResourceEntry("NumberOfLevelsLowercase", Description = "phrase: level(s)", LastModified = "2010/10/18", Value = "level(s)")]
    public string NumberOfLevelsLowercase => this[nameof (NumberOfLevelsLowercase)];

    /// <summary>phrase: Show all levels expanded initially</summary>
    [ResourceEntry("ShowAllLevelsExpandedInitially", Description = "phrase: Show all levels expanded initially", LastModified = "2010/10/18", Value = "Show all levels expanded initially")]
    public string ShowAllLevelsExpandedInitially => this[nameof (ShowAllLevelsExpandedInitially)];

    /// <summary>phrase: Allow collapsing</summary>
    [ResourceEntry("AllowCollapsing", Description = "phrase: Allow collapsing", LastModified = "2010/10/18", Value = "Allow collapsing")]
    public string AllowCollapsing => this[nameof (AllowCollapsing)];

    /// <summary>phrase: Open drop down menu on</summary>
    [ResourceEntry("OpenDropdownMenuOn", Description = "phrase: Open drop down menu on", LastModified = "2010/10/18", Value = "Open drop down menu on")]
    public string OpenDropdownMenuOn => this[nameof (OpenDropdownMenuOn)];

    /// <summary>phrase: Mouse over</summary>
    [ResourceEntry("MouseOver", Description = "phrase: Mouse over", LastModified = "2010/10/18", Value = "Mouse over")]
    public string MouseOver => this[nameof (MouseOver)];

    /// <summary>word: Click</summary>
    [ResourceEntry("Click", Description = "word: Click", LastModified = "2010/10/18", Value = "Click")]
    public string Click => this[nameof (Click)];

    /// <summary>phrase: Headings should be links</summary>
    [ResourceEntry("HeadingsShouldBeLinks", Description = "phrase: Headings should be links", LastModified = "2010/10/18", Value = "Headings should be links")]
    public string HeadingsShouldBeLinks => this[nameof (HeadingsShouldBeLinks)];

    /// <summary>phrase: Design settings</summary>
    [ResourceEntry("DesignSettings", Description = "phrase: Design settings", LastModified = "2010/10/18", Value = "Design settings")]
    public string DesignSettings => this[nameof (DesignSettings)];

    /// <summary>
    /// phrase: Wrapper CSS class &lt;em class="sfNote"&gt;(Skin)&lt;/em&gt;
    /// </summary>
    [ResourceEntry("WrapperClassTemplate", Description = "phrase: Wrapper CSS class <em class='sfNote'>(Skin)</em>", LastModified = "2010/10/18", Value = "Wrapper CSS class <em class='sfNote'>(Skin)</em>")]
    public string WrapperClassTemplate => this[nameof (WrapperClassTemplate)];

    /// <summary>phrase: Custom template path</summary>
    [ResourceEntry("CustomTemplatePath", Description = "phrase: Custom template path", LastModified = "2010/10/18", Value = "Custom template path")]
    public string CustomTemplatePath => this[nameof (CustomTemplatePath)];

    /// <summary>
    /// phrase: &lt;strong&gt;Example:&lt;/strong&gt; ~/YourFolder/Navigation.ascx
    /// </summary>
    [ResourceEntry("CustomTemplatePathExample", Description = "phrase: <strong>Example:</strong> ~/YourFolder/Navigation.ascx", LastModified = "2010/10/18", Value = "<strong>Example:</strong> ~/YourFolder/Navigation.ascx")]
    public string CustomTemplatePathExample => this[nameof (CustomTemplatePathExample)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingSiteMapInColumns", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Links to which pages to display as column headings?|Their child pages will be listed below them")]
    public string NavigationSelectionHeadingSiteMapInColumns => this[nameof (NavigationSelectionHeadingSiteMapInColumns)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingSiteMapInRows", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Links to which pages to display as row headings?|Their child pages will be listed below them")]
    public string NavigationSelectionHeadingSiteMapInRows => this[nameof (NavigationSelectionHeadingSiteMapInRows)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionHeadingCustomNavigation", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/20", Value = "Links to which pages to display?|")]
    public string NavigationSelectionHeadingCustomNavigation => this[nameof (NavigationSelectionHeadingCustomNavigation)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionTopLevelPages", Description = "Label shown in navigation control desginer modes", LastModified = "2010/08/19", Value = "Top level pages")]
    public string NavigationSelectionTopLevelPages => this[nameof (NavigationSelectionTopLevelPages)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("TopLevelPages", Description = "Label shown in navigation control desginer modes", LastModified = "2013/06/24", Value = "Top-level pages (and their child-pages if template allows)")]
    public string TopLevelPages => this[nameof (TopLevelPages)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionSelectedPageChildren", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "All pages under particular page")]
    public string NavigationSelectionSelectedPageChildren => this[nameof (NavigationSelectionSelectedPageChildren)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionCurrentPageChildren", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "All pages under currently opened page")]
    public string NavigationSelectionCurrentPageChildren => this[nameof (NavigationSelectionCurrentPageChildren)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionCurrentPageSiblings", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "All sibling pages of currently opened page")]
    public string NavigationSelectionCurrentPageSiblings => this[nameof (NavigationSelectionCurrentPageSiblings)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationSelectionCustomSelection", Description = "Label shown in navigation control desginer modes", LastModified = "2010/04/16", Value = "Custom selection of pages")]
    public string NavigationSelectionCustomSelection => this[nameof (NavigationSelectionCustomSelection)];

    /// <summary>"Message shown in navigation control desginer</summary>
    [ResourceEntry("NavigationDesignerTitle", Description = "Title shown in navigation control desginer", LastModified = "2010/04/16", Value = "Navigation")]
    public string NavigationDesignerTitle => this[nameof (NavigationDesignerTitle)];

    /// <summary>Phrase: All pages under particular parent page...</summary>
    [ResourceEntry("AllPagesUnderParticularParentPage", Description = "Phrase: All pages under particular parent page...", LastModified = "2010/10/06", Value = "All pages under particular parent page...")]
    public string AllPagesUnderParticularParentPage => this[nameof (AllPagesUnderParticularParentPage)];

    /// <summary>Phrase: All pages under</summary>
    [ResourceEntry("AllPagesUnder", Description = "Phrase: All pages under ", LastModified = "2010/10/06", Value = "All pages under ")]
    public string AllPagesUnder => this[nameof (AllPagesUnder)];

    /// <summary>Phrase: Custom selection of pages...</summary>
    [ResourceEntry("CustomSelectionOfPages", Description = "Phrase: Custom selection of pages...", LastModified = "2010/10/06", Value = "Custom selection of pages...")]
    public string CustomSelectionOfPages => this[nameof (CustomSelectionOfPages)];

    /// <summary>phrase: Bulk edit</summary>
    [ResourceEntry("BulkEdit", Description = "phrase: Bulk edit", LastModified = "2010/04/26", Value = "Bulk edit")]
    public string BulkEdit => this[nameof (BulkEdit)];

    /// <summary>word: Owner</summary>
    [ResourceEntry("Owner", Description = "word: Owner", LastModified = "2010/04/26", Value = "Owner")]
    public string Owner => this[nameof (Owner)];

    /// <summary>phrase: Manage also</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2010/04/27", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Custom fields</summary>
    [ResourceEntry("CustomFields", Description = "phrase: Custom fields", LastModified = "2010/04/27", Value = "Custom fields")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>phrase: Custom fields for pages</summary>
    [ResourceEntry("CustomFieldsForPages", Description = "phrase: Custom fields for pages", LastModified = "2019/03/12", Value = "Custom fields for pages")]
    public string CustomFieldsForPages => this[nameof (CustomFieldsForPages)];

    /// <summary>phrase: Page Templates</summary>
    [ResourceEntry("PageTemplates", Description = "phrase: Page Templates", LastModified = "2016/07/28", Value = "Page Templates")]
    public string PageTemplates => this[nameof (PageTemplates)];

    /// <summary>word: Classifications</summary>
    [ResourceEntry("Classifications", Description = "word: Classifications", LastModified = "2016/07/28", Value = "Classifications")]
    public string Classifications => this[nameof (Classifications)];

    /// <summary>word: Content</summary>
    [ResourceEntry("Content", Description = "word: Content", LastModified = "2010/04/27", Value = "Content")]
    public string Content => this[nameof (Content)];

    /// <summary>phrase: Revision History</summary>
    [ResourceEntry("RevisionHistory", Description = "phrase: Revision History", LastModified = "2010/04/27", Value = "Revision History")]
    public string RevisionHistory => this[nameof (RevisionHistory)];

    /// <summary>word: Under</summary>
    [ResourceEntry("Under", Description = "word: Under", LastModified = "2010/04/30", Value = "Under")]
    public string Under => this[nameof (Under)];

    /// <summary>phrase: RemoveThisRule</summary>
    [ResourceEntry("RemoveThisRule", Description = "phrase: RemoveThisRule", LastModified = "2010/04/30", Value = "Remove this rule")]
    public string RemoveThisRule => this[nameof (RemoveThisRule)];

    /// <summary>phrase: AddAnotherSortingRule</summary>
    [ResourceEntry("AddAnotherSortingRule", Description = "phrase: AddAnotherSortingRule", LastModified = "2010/04/30", Value = "Add another sorting rule")]
    public string AddAnotherSortingRule => this[nameof (AddAnotherSortingRule)];

    /// <summary>phrase: ThenBy</summary>
    [ResourceEntry("ThenBy", Description = "phrase: ThenBy", LastModified = "2010/04/30", Value = "Then by")]
    public string ThenBy => this[nameof (ThenBy)];

    /// <summary>phrase: RemoveAllRules</summary>
    [ResourceEntry("RemoveAllRules", Description = "phrase: RemoveAllRules", LastModified = "2010/04/30", Value = "Remove all")]
    public string RemoveAllRules => this[nameof (RemoveAllRules)];

    /// <summary>word: Remove</summary>
    [ResourceEntry("Remove", Description = "word: Remove", LastModified = "2010/04/30", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    /// <summary>phrase: Done selecting</summary>
    [ResourceEntry("DoneSelecting", Description = "phrase: Done selecting", LastModified = "2010/05/11", Value = "Done selecting")]
    public string DoneSelecting => this[nameof (DoneSelecting)];

    /// <summary>word: CSS</summary>
    [ResourceEntry("CSS", Description = "word: CSS", LastModified = "2010/05/12", Value = "CSS")]
    public string CSS => this[nameof (CSS)];

    /// <summary>word: CSS</summary>
    [ResourceEntry("Feed", Description = "word: Feed", LastModified = "2010/07/21", Value = "Feed")]
    public string Feed => this[nameof (Feed)];

    /// <summary>word: Search Box</summary>
    [ResourceEntry("SearchBox", Description = "word: Search Box", LastModified = "2010/12/10", Value = "Search Box")]
    public string SearchBox => this[nameof (SearchBox)];

    /// <summary>word: Search Results</summary>
    [ResourceEntry("SearchResults", Description = "word: Search Results", LastModified = "2010/12/10", Value = "Search Results")]
    public string SearchResults => this[nameof (SearchResults)];

    /// <summary>word: Media</summary>
    [ResourceEntry("Media", Description = "word: Media", LastModified = "2010/05/12", Value = "Media")]
    public string Media => this[nameof (Media)];

    /// <summary>phrase: Java Script</summary>
    [ResourceEntry("JavaScript", Description = "phrase: Java Script", LastModified = "2010/05/13", Value = "Java Script")]
    public string JavaScript => this[nameof (JavaScript)];

    /// <summary>phrase: Google Analytics</summary>
    [ResourceEntry("GoogleAnalytics", Description = "phrase: Google Analytics", LastModified = "2010/05/13", Value = "Google Analytics")]
    public string GoogleAnalytics => this[nameof (GoogleAnalytics)];

    /// <summary>Phrase: File</summary>
    [ResourceEntry("File", Description = "phrase", LastModified = "2010/05/29", Value = "File")]
    public string File => this[nameof (File)];

    /// <summary>
    /// Text of the link to the first page in the paging control
    /// </summary>
    [ResourceEntry("Paging_First", Description = "Text of the link to the first page in the paging control", LastModified = "2010/05/31", Value = "First")]
    public string Paging_First => this[nameof (Paging_First)];

    /// <summary>
    /// Text of the link to the previous page in the paging control
    /// </summary>
    [ResourceEntry("Paging_Prev", Description = "Text of the link to the previous page in the paging control", LastModified = "2010/05/31", Value = "Prev")]
    public string Paging_Prev => this[nameof (Paging_Prev)];

    /// <summary>
    /// Text of the link to the next page in the paging control
    /// </summary>
    [ResourceEntry("Paging_Next", Description = "Text of the link to the next page in the paging control", LastModified = "2010/05/31", Value = "Next")]
    public string Paging_Next => this[nameof (Paging_Next)];

    /// <summary>
    /// Text of the link to the last page in the paging control
    /// </summary>
    [ResourceEntry("Paging_Last", Description = "Text of the link to the last page in the paging control", LastModified = "2010/05/31", Value = "Last")]
    public string Paging_Last => this[nameof (Paging_Last)];

    /// <summary>Phrase: Size</summary>
    [ResourceEntry("Size", Description = "phrase", LastModified = "2010/05/29", Value = "Size")]
    public string Size => this[nameof (Size)];

    /// <summary>word: Uploaded on</summary>
    [ResourceEntry("UploadedOn", Description = "word: Uploaded on", LastModified = "2010/05/29", Value = "Uploaded on")]
    public string UploadedOn => this[nameof (UploadedOn)];

    /// <summary>word: Download</summary>
    [ResourceEntry("Download", Description = "word: Download", LastModified = "2010/06/01", Value = "Download")]
    public string Download => this[nameof (Download)];

    /// <summary>phrase: Revert to this version</summary>
    [ResourceEntry("CopyAsNewest", Description = "Copy this version as a base of the news item", LastModified = "2010/06/03", Value = "Revert to this version")]
    public string CopyAsNewest => this[nameof (CopyAsNewest)];

    /// <summary>word: note</summary>
    [ResourceEntry("note", Description = "note", LastModified = "2010/06/14", Value = "note")]
    public string note => this[nameof (note)];

    /// <summary>phrase: Basic options only</summary>
    [ResourceEntry("BasicToolsOnly", Description = "phrase: Basic options only", LastModified = "2010/06/02", Value = "Basic options only")]
    public string BasicToolsOnly => this[nameof (BasicToolsOnly)];

    /// <summary>phrase: More formatting options</summary>
    [ResourceEntry("MoreFormattingTools", Description = "phrase: More formatting options", LastModified = "2010/06/02", Value = "More formatting options")]
    public string MoreFormattingTools => this[nameof (MoreFormattingTools)];

    /// <summary>phrase: Formatting options</summary>
    [ResourceEntry("FormattingOptions", Description = "phrase: Formatting options", LastModified = "2010/11/14", Value = "Formatting options")]
    public string FormattingOptions => this[nameof (FormattingOptions)];

    /// <summary>phrase: Hide formatting options</summary>
    [ResourceEntry("HideFormattingOptions", Description = "phrase: Hide formatting options", LastModified = "2010/11/14", Value = "Hide formatting options")]
    public string HideFormattingOptions => this[nameof (HideFormattingOptions)];

    /// <summary>phrase: Remove this category</summary>
    [ResourceEntry("RemoveThisCategory", Description = "phrase: Remove this category", LastModified = "2010/06/09", Value = "Remove this category")]
    public string RemoveThisCategory => this[nameof (RemoveThisCategory)];

    /// <summary>phrase: Remove this tag</summary>
    [ResourceEntry("RemoveThisTag", Description = "phrase: Remove this tag", LastModified = "2010/06/09", Value = "Remove this tag")]
    public string RemoveThisTag => this[nameof (RemoveThisTag)];

    /// <summary>phrase: (N/A)</summary>
    [ResourceEntry("NotAvailable", Description = "phrase: (N/A)", LastModified = "2010/06/09", Value = "<span class='sfNote'>(N/A)</span>")]
    public string NotAvailable => this[nameof (NotAvailable)];

    /// <summary>phrase: (N/A)</summary>
    [ResourceEntry("Previous", Description = "phrase: (N/A)", LastModified = "2010/06/22", Value = "Previous")]
    public string Previous => this[nameof (Previous)];

    /// <summary>phrase: (N/A)</summary>
    [ResourceEntry("Next", Description = "phrase: (N/A)", LastModified = "2010/06/22", Value = "Next")]
    public string Next => this[nameof (Next)];

    /// <summary>phrase: Older version</summary>
    [ResourceEntry("PreviousVersion", Description = "phrase: Older version", LastModified = "2010/06/29", Value = "Older version")]
    public string PreviousVersion => this[nameof (PreviousVersion)];

    /// <summary>phrase: Newer version</summary>
    [ResourceEntry("NextVersion", Description = "phrase: Newer version", LastModified = "2010/06/29", Value = "Newer version")]
    public string NextVersion => this[nameof (NextVersion)];

    /// <summary>phrase: Close categories</summary>
    [ResourceEntry("CloseCategories", Description = "phrase: Close categories", LastModified = "2010/07/22", Value = "Close categories")]
    public string CloseCategories => this[nameof (CloseCategories)];

    /// <summary>Close categories</summary>
    [ResourceEntry("CloseTags", Description = "The link for closing the tag filter widget in the sidebar.", LastModified = "2010/06/02", Value = "Close tags")]
    public string CloseTags => this[nameof (CloseTags)];

    /// <summary>phrase: {0} more tags</summary>
    [ResourceEntry("ShowMoreTags", Description = "phrase: {0} more tags", LastModified = "2010/05/18", Value = "{0} more tags")]
    public string ShowMoreTags => this[nameof (ShowMoreTags)];

    /// <summary>phrase: {0} less tags</summary>
    [ResourceEntry("ShowLessTags", Description = "phrase: {0} less tags", LastModified = "2010/05/18", Value = "{0} less tags")]
    public string ShowLessTags => this[nameof (ShowLessTags)];

    /// <summary>phrase: {0} more categories</summary>
    [ResourceEntry("ShowMoreCategories", Description = "phrase: {0} more categories", LastModified = "2012/10/30", Value = "{0} more categories")]
    public string ShowMoreCategories => this[nameof (ShowMoreCategories)];

    /// <summary>phrase: {0} less categories</summary>
    [ResourceEntry("ShowLessCategories", Description = "phrase: {0} less categories", LastModified = "2012/10/30", Value = "{0} less categories")]
    public string ShowLessCategories => this[nameof (ShowLessCategories)];

    /// <summary>phrase: More Options</summary>
    [ResourceEntry("MoreOptions", Description = "phrase", LastModified = "2010/07/29", Value = "More options")]
    public string MoreOptions => this[nameof (MoreOptions)];

    /// <summary>phrase: Text to display</summary>
    [ResourceEntry("TextToDisplay", Description = "phrase: Text to display", LastModified = "2010/07/29", Value = "Text to display")]
    public string TextToDisplay => this[nameof (TextToDisplay)];

    /// <summary>
    /// phrase: Tooltip (appears when the cursor is pointed to the link)
    /// </summary>
    [ResourceEntry("Tooltip", Description = "phrase: Tooltip (appears when the cursor is pointed to the link)", LastModified = "2010/07/29", Value = "Tooltip <span class='sfLblNote'>(appears when the cursor is pointed to the link)</span>")]
    public string Tooltip => this[nameof (Tooltip)];

    /// <summary>phrase: Web address</summary>
    [ResourceEntry("WebAddress", Description = "phrase: Web address", LastModified = "2010/07/29", Value = "Web address")]
    public string WebAddress => this[nameof (WebAddress)];

    /// <summary>phrase: CSS class</summary>
    [ResourceEntry("CssClass", Description = "phrase: CSS class", LastModified = "2010/07/29", Value = "CSS class")]
    public string CssClass => this[nameof (CssClass)];

    /// <summary>phrase: Open this link in a new window</summary>
    [ResourceEntry("OpenInNewWindow", Description = "phrase: Open this link in a new window", LastModified = "2010/07/29", Value = "Open this link in a new window")]
    public string OpenInNewWindow => this[nameof (OpenInNewWindow)];

    /// <summary>phrase: Narrow by typing</summary>
    [ResourceEntry("NarrowByTyping", Description = "phrase: Narrow by typing", LastModified = "2010/07/29", Value = "Narrow by typing")]
    public string NarrowByTyping => this[nameof (NarrowByTyping)];

    /// <summary>phrase: Search by image, library, tag, category</summary>
    [ResourceEntry("SearchByImageLibraryTagCategory", Description = "phrase: Search by image, library, tag, category...", LastModified = "2014/12/17", Value = "Search by image, library, tag, category...")]
    public string SearchByImageLibraryTagCategory => this[nameof (SearchByImageLibraryTagCategory)];

    /// <summary>phrase: Search by title</summary>
    [ResourceEntry("SearchByTitle", Description = "phrase: Search by title...", LastModified = "2015/11/30", Value = "Search by title...")]
    public string SearchByTitle => this[nameof (SearchByTitle)];

    /// <summary>phrase: Search by email</summary>
    [ResourceEntry("SearchByEmail", Description = "phrase: Search by email...", LastModified = "2018/8/16", Value = "Search by email...")]
    public string SearchByEmail => this[nameof (SearchByEmail)];

    /// <summary>phrase: Select a page</summary>
    [ResourceEntry("SelectPage", Description = "phrase: Select a page", LastModified = "2010/07/29", Value = "Select a page")]
    public string SelectPage => this[nameof (SelectPage)];

    /// <summary>phrase: Clear selected page</summary>
    [ResourceEntry("ClearSelectedPage", Description = "phrase: Clear selected page", LastModified = "2011/11/08", Value = "Clear selected page")]
    public string ClearSelectedPage => this[nameof (ClearSelectedPage)];

    /// <summary>phrase: Select a page...</summary>
    [ResourceEntry("SelectPageExtended", Description = "phrase: Select a page...", LastModified = "2011/03/21", Value = "Select a page...")]
    public string SelectPageExtended => this[nameof (SelectPageExtended)];

    /// <summary>phrase: Clear filter</summary>
    [ResourceEntry("ClearFilter", Description = "'Clear filter' button text", LastModified = "2010/07/31", Value = "Clear filter")]
    public string ClearFilter => this[nameof (ClearFilter)];

    /// <summary>phrase: In process of implementation.</summary>
    [ResourceEntry("InProcessOfImplementation", Description = "'In process of implementation.' text", LastModified = "2010/07/31", Value = "<span class='sfNote'>(In process of implementation.)</span>")]
    public string InProcessOfImplementation => this[nameof (InProcessOfImplementation)];

    /// <summary>phrase: In process of implementation with no html.</summary>
    [ResourceEntry("InProcessOfImplementationNoHtml", Description = "'In process of implementation.' text with no html", LastModified = "2010/07/31", Value = "In process of implementation.")]
    public string InProcessOfImplementationNoHtml => this[nameof (InProcessOfImplementationNoHtml)];

    /// <summary>phrase: Example: http://weather.com</summary>
    [ResourceEntry("WebAddressExample", Description = "phrase: Example: http://weather.com", LastModified = "2010/08/05", Value = "<strong>Example:</strong> http://weather.com")]
    public string WebAddressExample => this[nameof (WebAddressExample)];

    /// <summary>phrase: Example: Weather forecast</summary>
    [ResourceEntry("WebAddressTextToDisplayExample", Description = "phrase: Example: Weather forecast", LastModified = "2010/08/05", Value = "<strong>Example:</strong> Weather forecast")]
    public string WebAddressTextToDisplayExample => this[nameof (WebAddressTextToDisplayExample)];

    /// <summary>phrase: Page from this site</summary>
    [ResourceEntry("PageFromThisSite", Description = "phrase: Page from this site", LastModified = "2010/08/10", Value = "Page from this site")]
    public string PageFromThisSite => this[nameof (PageFromThisSite)];

    /// <summary>phrase: Example: johnsmith@gmail.com</summary>
    [ResourceEntry("EmailAddressExample", Description = "phrase: Example: johnsmith@gmail.com", LastModified = "2010/08/10", Value = "<strong>Example:</strong> johnsmith@gmail.com")]
    public string EmailAddressExample => this[nameof (EmailAddressExample)];

    /// <summary>phrase: Email address</summary>
    [ResourceEntry("EmailAddress", Description = "phrase: Email address", LastModified = "2010/08/10", Value = "Email address")]
    public string EmailAddress => this[nameof (EmailAddress)];

    /// <summary>phrase: Example: Send email to John</summary>
    [ResourceEntry("EmailTextToDisplayExample", Description = "phrase: Example: Send email to John", LastModified = "2010/08/10", Value = "<strong>Example:</strong> Send email to John")]
    public string EmailTextToDisplayExample => this[nameof (EmailTextToDisplayExample)];

    /// <summary>phrase: Insert a link</summary>
    [ResourceEntry("InsertLink", Description = "phrase: Insert a link", LastModified = "2010/08/11", Value = "Insert a link")]
    public string InsertLink => this[nameof (InsertLink)];

    /// <summary>phrase: Link to</summary>
    [ResourceEntry("LinkTo", Description = "phrase: Insert a link", LastModified = "2010/08/11", Value = "LinkTo")]
    public string LinkTo => this[nameof (LinkTo)];

    /// <summary>phrase: Link to:</summary>
    [ResourceEntry("LinkToColon", Description = "phrase: Link to:", LastModified = "2011/03/21", Value = "Link to: ")]
    public string LinkToColon => this[nameof (LinkToColon)];

    /// <summary>phrase: From other sites</summary>
    [ResourceEntry("FromOtherSites", Description = "phrase: From other sites", LastModified = "2010/08/17", Value = "From other sites")]
    public string FromOtherSites => this[nameof (FromOtherSites)];

    /// <summary>phrase: From this site</summary>
    [ResourceEntry("FromThisSite", Description = "phrase: From this site", LastModified = "2010/08/17", Value = "From this site")]
    public string FromThisSite => this[nameof (FromThisSite)];

    /// <summary>phrase: Select Pages</summary>
    [ResourceEntry("SelectPages", Description = "phrase: Select Pages", LastModified = "2010/08/17", Value = "Select Pages")]
    public string SelectPages => this[nameof (SelectPages)];

    /// <summary>phrase: Select pages</summary>
    [ResourceEntry("SelectPagesLowerCase", Description = "phrase: Select pages", LastModified = "2010/10/06", Value = "Select pages")]
    public string SelectPagesLowerCase => this[nameof (SelectPagesLowerCase)];

    /// <summary>phrase: Select pages...</summary>
    [ResourceEntry("SelectPagesWithDots", Description = "phrase: Select pages...", LastModified = "2010/10/06", Value = "Select pages...")]
    public string SelectPagesWithDots => this[nameof (SelectPagesWithDots)];

    /// <summary>phrase: Select pages from</summary>
    [ResourceEntry("SelectPagesFrom", Description = "Select pages from", LastModified = "2018/08/10", Value = "Select pages from")]
    public string SelectPagesFrom => this[nameof (SelectPagesFrom)];

    /// <summary>phrase: Select parent...</summary>
    [ResourceEntry("SelectParentWithDots", Description = "phrase: Select parent...", LastModified = "2010/10/06", Value = "Select parent...")]
    public string SelectParentWithDots => this[nameof (SelectParentWithDots)];

    /// <summary>phrase: Add other pages...</summary>
    [ResourceEntry("AddOtherPagesWithDots", Description = "phrase: Add other pages...", LastModified = "2010/10/06", Value = "Add other pages...")]
    public string AddOtherPagesWithDots => this[nameof (AddOtherPagesWithDots)];

    /// <summary>
    /// label: Name. Used in the dialog for selecting external pages.
    /// </summary>
    [ResourceEntry("PageName", Description = "label: Name. Used in the dialog for selecting external pages.", LastModified = "2010/08/20", Value = "Name")]
    public string PageName => this[nameof (PageName)];

    /// <summary>
    /// Message: Please specify valid name and URL for all external pages.
    /// </summary>
    [ResourceEntry("PleaseSpecifyValidPages", Description = "Message: Please specify valid name and URL for all external pages.", LastModified = "2010/08/20", Value = "Please specify valid name and URL for all external pages.")]
    public string PleaseSpecifyValidPages => this[nameof (PleaseSpecifyValidPages)];

    /// <summary>phrase: Last 1 day</summary>
    [ResourceEntry("Last1Day", Description = "phrase: Last 1 day", LastModified = "2010/08/31", Value = "Last 1 day")]
    public string Last1Day => this[nameof (Last1Day)];

    /// <summary>phrase: Last 3 days</summary>
    [ResourceEntry("Last3Days", Description = "phrase: Last 3 days", LastModified = "2010/08/31", Value = "Last 3 days")]
    public string Last3Days => this[nameof (Last3Days)];

    /// <summary>phrase: Last 1 week</summary>
    [ResourceEntry("Last1Week", Description = "phrase: Last 1 week", LastModified = "2010/08/31", Value = "Last 1 week")]
    public string Last1Week => this[nameof (Last1Week)];

    /// <summary>phrase: Last 1 month</summary>
    [ResourceEntry("Last1Month", Description = "phrase: Last 1 month", LastModified = "2010/08/31", Value = "Last 1 month")]
    public string Last1Month => this[nameof (Last1Month)];

    /// <summary>phrase: Last 6 months</summary>
    [ResourceEntry("Last6Months", Description = "phrase: Last 6 months", LastModified = "2010/08/31", Value = "Last 6 months")]
    public string Last6Months => this[nameof (Last6Months)];

    /// <summary>phrase: Last 1 year</summary>
    [ResourceEntry("Last1Year", Description = "phrase: Last 1 year", LastModified = "2010/08/31", Value = "Last 1 year")]
    public string Last1Year => this[nameof (Last1Year)];

    /// <summary>phrase: Last 2 years</summary>
    [ResourceEntry("Last2Years", Description = "phrase: Last 2 years", LastModified = "2010/08/31", Value = "Last 2 years")]
    public string Last2Years => this[nameof (Last2Years)];

    /// <summary>phrase: Last 5 years</summary>
    [ResourceEntry("Last5Years", Description = "phrase: Last 5 years", LastModified = "2010/08/31", Value = "Last 5 years")]
    public string Last5Years => this[nameof (Last5Years)];

    /// <summary>phrase: Display news published in...</summary>
    [ResourceEntry("DisplayNewsPublishedIn", Description = "phrase: Display news published in...", LastModified = "2010/08/31", Value = "Display news published in...")]
    public string DisplayNewsPublishedIn => this[nameof (DisplayNewsPublishedIn)];

    /// <summary>phrase: Display products added in...</summary>
    [ResourceEntry("DisplayProductsAddedIn", Description = "phrase: Display products added in...", LastModified = "2011/06/15", Value = "Display products added in...")]
    public string DisplayProductsAddedIn => this[nameof (DisplayProductsAddedIn)];

    /// <summary>phrase: Select Dates</summary>
    [ResourceEntry("SelectDates", Description = "phrase: Select Dates", LastModified = "2010/08/31", Value = "Select Dates")]
    public string SelectDates => this[nameof (SelectDates)];

    /// <summary>word: From</summary>
    [ResourceEntry("From", Description = "word: From", LastModified = "2010/08/31", Value = "From")]
    public string From => this[nameof (From)];

    /// <summary>Label: Unlock</summary>
    [ResourceEntry("Unlock", Description = "Label: Unlock", LastModified = "2010/08/30", Value = "Unlock")]
    public string Unlock => this[nameof (Unlock)];

    /// <summary>Label: ViewReadOnly</summary>
    [ResourceEntry("ViewReadOnly", Description = "Label: ViewReadOnly", LastModified = "2010/08/30", Value = "View as read-only")]
    public string ViewReadOnly => this[nameof (ViewReadOnly)];

    /// <summary>phrase: Export language pack</summary>
    [ResourceEntry("ExportLanguagePack", Description = "phrase: Export language pack", LastModified = "2010/08/20", Value = "Export language pack")]
    public string ExportLanguagePack => this[nameof (ExportLanguagePack)];

    /// <summary>phrase: Import language pack</summary>
    [ResourceEntry("ImportLanguagePack", Description = "phrase : Import language pack", LastModified = "2010/08/20", Value = "Import language pack")]
    public string ImportLanguagePack => this[nameof (ImportLanguagePack)];

    /// <summary>
    /// phrase: Select a language &lt;span class='sfLblNote'&gt;for which you will import labels&lt;/span&gt;
    /// </summary>
    [ResourceEntry("SelectLanguageToImportFrom", Description = "phrase : Select a language <span class='sfLblNote'>for which you will import labels</span>", LastModified = "2010/10/18", Value = "Select a language <span class='sfLblNote'>for which you will import labels</span>")]
    public string SelectLanguageToImportFrom => this[nameof (SelectLanguageToImportFrom)];

    /// <summary>
    /// phrase: Select a .CSV file &lt;span class='sfLblNote'&gt;with the translated labels for the selected language&lt;/span&gt;
    /// </summary>
    [ResourceEntry("SelectXLSXFile", Description = "phrase : Select a .XLSX file <span class='sfLblNote'>with the translated labels for the selected language</span>", LastModified = "2010/10/18", Value = "Select a .XLSX file <span class='sfLblNote'>with the translated labels for the selected language</span>")]
    public string SelectXLSXFile => this[nameof (SelectXLSXFile)];

    /// <summary>word: Import</summary>
    [ResourceEntry("Import", Description = "phrase : Import language pack", LastModified = "2010/08/20", Value = "Import")]
    public string Import => this[nameof (Import)];

    /// <summary>
    /// phrase: Translate all missing labels (using Google Translator)
    /// </summary>
    [ResourceEntry("TranslateAllMissingLabelsGoogle", Description = "phrase: Translate all missing labels (using Google Translator)", LastModified = "2010/08/20", Value = "Translate all missing labels (using Google Translator)")]
    public string TranslateAllMissingLabelsGoogle => this[nameof (TranslateAllMissingLabelsGoogle)];

    /// <summary>phrase: Display posts published in...</summary>
    [ResourceEntry("DisplayPostsPublishedIn", Description = "phrase: Display posts published in...", LastModified = "2010/09/08", Value = "Display posts published in...")]
    public string DisplayPostsPublishedIn => this[nameof (DisplayPostsPublishedIn)];

    /// <summary>phrase: Display events published in...</summary>
    [ResourceEntry("DisplayEvetnsPublishedIn", Description = "phrase: Display events published in...", LastModified = "2011/11/03", Value = "Display events published in...")]
    public string DisplayEvetnsPublishedIn => this[nameof (DisplayEvetnsPublishedIn)];

    /// <summary>phrase: Registration date</summary>
    [ResourceEntry("RegistrationDate", Description = "phrase: Registration date", LastModified = "2010/09/15", Value = "Registration date")]
    public string RegistrationDate => this[nameof (RegistrationDate)];

    /// <summary>phrase: Last login</summary>
    [ResourceEntry("LastLogin", Description = "phrase: Last login", LastModified = "2010/09/15", Value = "Last login")]
    public string LastLogin => this[nameof (LastLogin)];

    /// <summary>phrase: Last activity</summary>
    [ResourceEntry("LastActivity", Description = "phrase: Last activity", LastModified = "2010/09/15", Value = "Last activity")]
    public string LastActivity => this[nameof (LastActivity)];

    /// <summary>phrase: Everyone can view this post on the website</summary>
    [ResourceEntry("PublishedStatusDescription", Description = "phrase: Everyone can view this post on the website", LastModified = "2010/10/01", Value = "Everyone can view this post on the website")]
    public string PublishedStatusDescription => this[nameof (PublishedStatusDescription)];

    /// <summary>
    /// phrase: This post will be automatically published on the website.
    /// </summary>
    [ResourceEntry("ScheduledStatusDescription", Description = "phrase: This post will be automatically published on the website.", LastModified = "2010/10/01", Value = "This post will be automatically published on the website.")]
    public string ScheduledStatusDescription => this[nameof (ScheduledStatusDescription)];

    /// <summary>phrase: Select roles or users</summary>
    [ResourceEntry("SelectRolesOrUsers", Description = "phrase: Select roles or users", LastModified = "2010/10/01", Value = "Select roles or users")]
    public string SelectRolesOrUsers => this[nameof (SelectRolesOrUsers)];

    /// <summary>
    /// Message: Warning: changing the template might break the layout of the page.
    /// </summary>
    [ResourceEntry("TemplateChangeWarning", Description = "Message: Warning: changing the template might break the layout of the page.", LastModified = "2010/09/30", Value = "<strong>Warning:</strong> changing the template might break the layout of the page.")]
    public string TemplateChangeWarning => this[nameof (TemplateChangeWarning)];

    /// <summary>phrase: Accept changes</summary>
    [ResourceEntry("AcceptChanges", Description = "phrase: Accept changes", LastModified = "2010/10/01", Value = "Accept changes")]
    public string AcceptChanges => this[nameof (AcceptChanges)];

    /// <summary>Message: All translations are synced. ...</summary>
    [ResourceEntry("TranslationsAreSynchronized", Description = "Message: All translations are synced. ...", LastModified = "2011/01/04", Value = "<strong>All translations of this page are synced</strong> <span class='sfNote'>Adding/deleting widgets to this page will affect all its translations</span>.")]
    public string TranslationsAreSynchronized => this[nameof (TranslationsAreSynchronized)];

    /// <summary>Message: All translations are synced. ...</summary>
    [ResourceEntry("TemplateTranslationsAreSynchronized", Description = "Message: All translations are synced. ...", LastModified = "2011/04/21", Value = "<strong>All translations of this template are synced</strong> <span class='sfNote'>Adding/deleting widgets to this template will affect all its translations</span>.")]
    public string TemplateTranslationsAreSynchronized => this[nameof (TemplateTranslationsAreSynchronized)];

    /// <summary>Label: Manage templates</summary>
    [ResourceEntry("ManageTemplates", Description = "Label: Manage templates", LastModified = "2019/03/12", Value = "Manage templates")]
    public string ManageTemplates => this[nameof (ManageTemplates)];

    /// <summary>Label: Stop syncing</summary>
    [ResourceEntry("StopSyncing", Description = "Label: Stop syncing", LastModified = "2010/10/06", Value = "Stop syncing")]
    public string StopSyncing => this[nameof (StopSyncing)];

    /// <summary>Label: Stop syncing</summary>
    [ResourceEntry("StopSyncingTranslationsOfThisPage", Description = "Label: Stop syncing", LastModified = "2019/12/13", Value = "Stop syncing translations of this page?")]
    public string StopSyncingTranslationsOfThisPage => this[nameof (StopSyncingTranslationsOfThisPage)];

    /// <summary>Label: Show other translations</summary>
    [ResourceEntry("ShowOtherTranslations", Description = "Label: Show other translations", LastModified = "2010/10/06", Value = "Show other translations")]
    public string ShowOtherTranslations => this[nameof (ShowOtherTranslations)];

    /// <summary>Label: Show other translations</summary>
    [ResourceEntry("HideOtherTranslations", Description = "Label: Hide other translations", LastModified = "2010/11/16", Value = "Hide other translations")]
    public string HideOtherTranslations => this[nameof (HideOtherTranslations)];

    /// <summary>Label: Compare translations</summary>
    [ResourceEntry("CompareTranslations", Description = "Label: Compare translations", LastModified = "2010/10/21", Value = "Compare translations")]
    public string CompareTranslations => this[nameof (CompareTranslations)];

    /// <summary>Label: Close comparison</summary>
    [ResourceEntry("CloseComparison", Description = "Label: Close comparison", LastModified = "2011/04/27", Value = "Close comparison")]
    public string CloseComparison => this[nameof (CloseComparison)];

    /// <summary>Label: Other language versions</summary>
    [ResourceEntry("OtherLanguageVersions", Description = "Label: Other language versions:", LastModified = "2010/10/06", Value = "Other language versions:")]
    public string OtherLanguageVersions => this[nameof (OtherLanguageVersions)];

    /// <summary>Button: Copy from another language</summary>
    [ResourceEntry("CopyOtherLanguage", Description = "Button: Copy from another language", LastModified = "2010/10/11", Value = "Copy from another language")]
    public string CopyOtherLanguage => this[nameof (CopyOtherLanguage)];

    /// <summary>Button: Start from scratch</summary>
    [ResourceEntry("StartFromScratch", Description = "Button: Start from scratch", LastModified = "2010/10/11", Value = "Start from scratch")]
    public string StartFromScratch => this[nameof (StartFromScratch)];

    /// <summary>Title: Copy content from another language</summary>
    [ResourceEntry("TitleCopyOtherLanguage", Description = "Title: Copy content from another language", LastModified = "2010/10/11", Value = "Copy content from another language")]
    public string TitleCopyOtherLanguage => this[nameof (TitleCopyOtherLanguage)];

    /// <summary>Label: Select a language to copy from</summary>
    [ResourceEntry("SelectSourceVersion", Description = "Label: Select a language to copy from", LastModified = "2010/10/11", Value = "Select a language to copy from")]
    public string SelectSourceVersion => this[nameof (SelectSourceVersion)];

    /// <summary>Label: None (start from scratch)</summary>
    [ResourceEntry("NoneStartFromScratch", Description = "Label: None (start from scratch)", LastModified = "2010/10/11", Value = "None (start from scratch)")]
    public string NoneStartFromScratch => this[nameof (NoneStartFromScratch)];

    /// <summary>
    /// Label: Keep all language versions synced as a one page
    /// </summary>
    [ResourceEntry("KeepPagesSynced", Description = "Label: Keep all language versions synced as a one page", LastModified = "2010/10/11", Value = "Keep all language versions synced as one page")]
    public string KeepPagesSynced => this[nameof (KeepPagesSynced)];

    /// <summary>
    /// Label: Description for "Keep all language versions synced" checkbox
    /// </summary>
    [ResourceEntry("KeepPagesSyncedDescr", Description = "Label: Description for 'Keep all language versions synced' checkbox", LastModified = "2010/10/11", Value = "If a widget is added to (or deleted from) a language version it will be added to (or deleted from) all language versions. The same widget will automatically display content in different languages according to selection of the user.")]
    public string KeepPagesSyncedDescr => this[nameof (KeepPagesSyncedDescr)];

    /// <summary>
    /// Message: Are you sure you want to stop syncing translations of this page? There is no undo. Stopping synchronization is permanent for this page
    /// </summary>
    [ResourceEntry("StopSyncWarningMessage", Description = "Message: Stop page synchronization: This action is NOT undoable! Are you sure?", LastModified = "2010/10/13", Value = "Are you sure you want to stop syncing translations of this page?<br />There is no undo. <strong class='sfImportantWarning'>Stopping synchronization is permanent for this page</strong>")]
    public string StopSyncWarningMessage => this[nameof (StopSyncWarningMessage)];

    /// <summary>Title: Warning</summary>
    [ResourceEntry("Warning", Description = "Title: Warning", LastModified = "2010/10/13", Value = "Warning")]
    public string Warning => this[nameof (Warning)];

    /// <summary>Label: Ok</summary>
    [ResourceEntry("Ok", Description = "Label: Ok", LastModified = "2010/10/13", Value = "OK")]
    public string Ok => this[nameof (Ok)];

    /// <summary>phrase: Narrow selection</summary>
    [ResourceEntry("NarrowSelection", Description = "phrase: Narrow selection", LastModified = "2010/09/06", Value = "Narrow selection")]
    public string NarrowSelection => this[nameof (NarrowSelection)];

    /// <summary>Narrow selection complementory text</summary>
    [ResourceEntry("NarrowSelectionComplementoryText", Description = "Narrow selection complementory text", LastModified = "2010/09/06", Value = "Narrow selection <span class='sfNote'>(by category, tag, date)</span>")]
    public string NarrowSelectionComplementoryText => this[nameof (NarrowSelectionComplementoryText)];

    /// <summary>
    /// phrase: The selected item was deleted. Please select another item.
    /// </summary>
    [ResourceEntry("SelectedItemWasDeletedSelectAnotherItem", Description = "phrase: The selected item was deleted. Please select another item.", LastModified = "2010/09/08", Value = "The selected item was deleted. Please select another item.")]
    public string SelectedItemWasDeletedSelectAnotherItem => this[nameof (SelectedItemWasDeletedSelectAnotherItem)];

    /// <summary>phrase: The selected item was deleted</summary>
    [ResourceEntry("SelectedItemWasDeleted", Description = "phrase: The selected item was deleted", LastModified = "2010/09/08", Value = "The selected item was deleted")]
    public string SelectedItemWasDeleted => this[nameof (SelectedItemWasDeleted)];

    /// <summary>phrase: Narrow by typing title or author or date</summary>
    [ResourceEntry("NarrowByTypingTitleOrAuthorOrDate", Description = "phrase: Narrow by typing title or author or date", LastModified = "2010/09/08", Value = "Narrow by typing title or author or date")]
    public string NarrowByTypingTitleOrAuthorOrDate => this[nameof (NarrowByTypingTitleOrAuthorOrDate)];

    /// <summary>phrase: Narrow by typing title or date</summary>
    [ResourceEntry("NarrowByTypingTitleOrDate", Description = "phrase: Narrow by typing title or date", LastModified = "2010/09/08", Value = "Narrow by typing title or date")]
    public string NarrowByTypingTitleOrDate => this[nameof (NarrowByTypingTitleOrDate)];

    /// <summary>phrase: No content selected</summary>
    [ResourceEntry("NoContentSelected", Description = "phrase: No content selected", LastModified = "2010/09/08", Value = "No content selected")]
    public string NoContentSelected => this[nameof (NoContentSelected)];

    /// <summary>phrase: Advanced selection</summary>
    [ResourceEntry("AdvancedSelection", Description = "phrase: Advanced selection", LastModified = "2010/09/08", Value = "Advanced selection")]
    public string AdvancedSelection => this[nameof (AdvancedSelection)];

    /// <summary>phrase: Choose News</summary>
    [ResourceEntry("ChooseNews", Description = "phrase: Choose News", LastModified = "2010/09/08", Value = "Choose News")]
    public string ChooseNews => this[nameof (ChooseNews)];

    /// <summary>phrase: All published news</summary>
    [ResourceEntry("AllPublishedNews", Description = "phrase: All published news", LastModified = "2010/09/08", Value = "All published news")]
    public string AllPublishedNews => this[nameof (AllPublishedNews)];

    /// <summary>phrase: One particular news item only...</summary>
    [ResourceEntry("OneParticularNewsOnly", Description = "phrase: One particular news item only...", LastModified = "2010/09/08", Value = "One particular news item only...")]
    public string OneParticularNewsOnly => this[nameof (OneParticularNewsOnly)];

    /// <summary>phrase: Selection of news:</summary>
    [ResourceEntry("SelectionOfNews", Description = "phrase: Selection of news:", LastModified = "2010/09/08", Value = "Selection of news:")]
    public string SelectionOfNews => this[nameof (SelectionOfNews)];

    /// <summary>phrase: Which news to display?</summary>
    [ResourceEntry("WhichNewsToDisplay", Description = "phrase: Which news to display?", LastModified = "2010/09/08", Value = "Which news to display?")]
    public string WhichNewsToDisplay => this[nameof (WhichNewsToDisplay)];

    /// <summary>phrase: Select news</summary>
    [ResourceEntry("SelectNews", Description = "phrase: Select news", LastModified = "2010/09/08", Value = "Select news")]
    public string SelectNews => this[nameof (SelectNews)];

    /// <summary>phrase: No news have been created yet</summary>
    [ResourceEntry("NoNewsHaveBeenCreatedYet", Description = "phrase: No news have been created yet", LastModified = "2010/09/08", Value = "No news have been created yet")]
    public string NoNewsHaveBeenCreatedYet => this[nameof (NoNewsHaveBeenCreatedYet)];

    /// <summary>phrase: Which items to display?</summary>
    [ResourceEntry("WhichContentItemsToDisplay", Description = "phrase: Which items to display?", LastModified = "2012/04/21", Value = "Which items to display?")]
    public string WhichContentItemsToDisplay => this[nameof (WhichContentItemsToDisplay)];

    /// <summary>phrase: All published items</summary>
    [ResourceEntry("AllPublishedItems", Description = "phrase: All published items", LastModified = "2012/04/21", Value = "All published items")]
    public string AllPublishedItems => this[nameof (AllPublishedItems)];

    /// <summary>phrase: Selection of items:</summary>
    [ResourceEntry("SelectionOfItems", Description = "phrase: Selection of items:", LastModified = "2012/04/21", Value = "Selection of items:")]
    public string SelectionOfItems => this[nameof (SelectionOfItems)];

    /// <summary>phrase: Display items published in...</summary>
    [ResourceEntry("DisplayItemsPublishedIn", Description = "phrase: Display items published in...", LastModified = "2012/04/21", Value = "Display items published in...")]
    public string DisplayItemsPublishedIn => this[nameof (DisplayItemsPublishedIn)];

    /// <summary>phrase: Select Product</summary>
    /// <value>Select Product</value>
    [ResourceEntry("ChooseProduct", Description = "phrase: Select Product", LastModified = "2013/10/28", Value = "Select Product")]
    public string ChooseProduct => this[nameof (ChooseProduct)];

    /// <summary>phrase: Which products to display?</summary>
    [ResourceEntry("WhichProductsToDisplay", Description = "phrase: Which products to display?", LastModified = "2011/06/14", Value = "Which products to display?")]
    public string WhichProductsToDisplay => this[nameof (WhichProductsToDisplay)];

    /// <summary>phrase: All published products</summary>
    [ResourceEntry("AllPublishedProducts", Description = "phrase: All published products", LastModified = "2011/06/14", Value = "All published products")]
    public string AllPublishedProducts => this[nameof (AllPublishedProducts)];

    /// <summary>phrase: One particular product only...</summary>
    [ResourceEntry("OneParticularProductOnly", Description = "phrase: One particular product only...", LastModified = "2011/06/14", Value = "One particular product only...")]
    public string OneParticularProductOnly => this[nameof (OneParticularProductOnly)];

    /// <summary>phrase: Selection of products:</summary>
    [ResourceEntry("SelectionOfProducts", Description = "phrase: Selection of products:", LastModified = "2011/06/14", Value = "Selection of products:")]
    public string SelectionOfProducts => this[nameof (SelectionOfProducts)];

    /// <summary>phrase: No products have been created yet</summary>
    [ResourceEntry("NoProductsHaveBeenCreatedYet", Description = "phrase: No products have been created yet", LastModified = "2011/06/14", Value = "No products have been created yet")]
    public string NoProductsHaveBeenCreatedYet => this[nameof (NoProductsHaveBeenCreatedYet)];

    /// <summary>phrase: Select product</summary>
    [ResourceEntry("SelectProduct", Description = "phrase: Select product", LastModified = "2011/06/16", Value = "Select product")]
    public string SelectProduct => this[nameof (SelectProduct)];

    /// <summary>phrase: Order Invoice page</summary>
    /// <value>Order Invoice page</value>
    [ResourceEntry("ChooseOrderInvoicePage", Description = "phrase: Order Invoice page", LastModified = "2013/10/28", Value = "Order Invoice page")]
    public string ChooseOrderInvoicePage => this[nameof (ChooseOrderInvoicePage)];

    /// <summary>phrase: Select</summary>
    /// <value>Select</value>
    [ResourceEntry("SelectOrderInvoicePage", Description = "phrase: Select", LastModified = "2013/10/28", Value = "Select")]
    public string SelectOrderInvoicePage => this[nameof (SelectOrderInvoicePage)];

    /// <summary>phrase: Select a blog</summary>
    [ResourceEntry("SelectBlog", Description = "phrase: Select a blog", LastModified = "2010/09/08", Value = "Select a blog")]
    public string SelectBlog => this[nameof (SelectBlog)];

    /// <summary>phrase: Choose Blogs</summary>
    [ResourceEntry("ChooseBlogs", Description = "phrase: Choose Blogs", LastModified = "2010/09/08", Value = "Choose Blogs")]
    public string ChooseBlogs => this[nameof (ChooseBlogs)];

    /// <summary>phrase: Narrow by typing blog title</summary>
    [ResourceEntry("NarrowBlogTitle", Description = "phrase: Narrow by typing blog title", LastModified = "2010/09/08", Value = "Narrow by typing blog title")]
    public string NarrowBlogTitle => this[nameof (NarrowBlogTitle)];

    /// <summary>phrase: Narrow by typing post title</summary>
    [ResourceEntry("NarrowPostTitle", Description = "phrase: Narrow by typing post title", LastModified = "2010/09/08", Value = "Narrow by typing post title")]
    public string NarrowPostTitle => this[nameof (NarrowPostTitle)];

    /// <summary>phrase: Choose a Blog Post</summary>
    [ResourceEntry("ChooseBlogPost", Description = "phrase: Choose a Blog Post", LastModified = "2010/09/08", Value = "Choose a Blog Post")]
    public string ChooseBlogPost => this[nameof (ChooseBlogPost)];

    /// <summary>phrase: Which blog posts to display?</summary>
    [ResourceEntry("WhichBlogPostsToDisplay", Description = "phrase: Which blog posts to display?", LastModified = "2010/09/08", Value = "Which blog posts to display?")]
    public string WhichBlogPostsToDisplay => this[nameof (WhichBlogPostsToDisplay)];

    /// <summary>phrase: From all blogs</summary>
    [ResourceEntry("FromAllBlogs", Description = "phrase: From all blogs", LastModified = "2010/09/08", Value = "From all blogs")]
    public string FromAllBlogs => this[nameof (FromAllBlogs)];

    /// <summary>phrase: From selected blogs only...</summary>
    [ResourceEntry("FromSelectedBlogsOnly", Description = "phrase: From selected blogs only...", LastModified = "2010/09/08", Value = "From selected blogs only...")]
    public string FromSelectedBlogsOnly => this[nameof (FromSelectedBlogsOnly)];

    /// <summary>phrase: Blog is not selected</summary>
    [ResourceEntry("BlogNotSelected", Description = "phrase: Blog is not selected", LastModified = "2010/09/08", Value = "Blog is not selected")]
    public string BlogNotSelected => this[nameof (BlogNotSelected)];

    /// <summary>phrase: not selected</summary>
    [ResourceEntry("NotSelected", Description = "phrase: not selected", LastModified = "2011/07/04", Value = "not selected")]
    public string NotSelected => this[nameof (NotSelected)];

    /// <summary>phrase: All published posts from selected blogs</summary>
    [ResourceEntry("AllPublishedPostsFromSelectedBlogs", Description = "phrase: All published posts from selected blogs", LastModified = "2010/09/08", Value = "All published posts from selected blogs")]
    public string AllPublishedPostsFromSelectedBlogs => this[nameof (AllPublishedPostsFromSelectedBlogs)];

    /// <summary>phrase: One particular post only...</summary>
    [ResourceEntry("OneParticularPostOnly", Description = "phrase: One particular post only...", LastModified = "2010/09/08", Value = "One particular post only...")]
    public string OneParticularPostOnly => this[nameof (OneParticularPostOnly)];

    /// <summary>phrase: No selected blog post</summary>
    [ResourceEntry("NoSelectedBlogPost", Description = "phrase: No selected blog post", LastModified = "2010/09/08", Value = "No selected blog post")]
    public string NoSelectedBlogPost => this[nameof (NoSelectedBlogPost)];

    /// <summary>phrase: Select a blog post</summary>
    [ResourceEntry("SelectBlogPost", Description = "phrase: Select a blog post", LastModified = "2010/09/08", Value = "Select a blog post")]
    public string SelectBlogPost => this[nameof (SelectBlogPost)];

    /// <summary>phrase: Selection of posts:</summary>
    [ResourceEntry("SelectionOfPosts", Description = "phrase: Selection of posts:", LastModified = "2010/09/08", Value = "Selection of posts:")]
    public string SelectionOfPosts => this[nameof (SelectionOfPosts)];

    /// <summary>phrase: by Categories...</summary>
    [ResourceEntry("ByCategories", Description = "phrase: by Categories...", LastModified = "2010/09/08", Value = "by Categories...")]
    public string ByCategories => this[nameof (ByCategories)];

    /// <summary>phrase: by Tags...</summary>
    [ResourceEntry("ByTags", Description = "phrase: by Tags...", LastModified = "2010/09/08", Value = "by Tags...")]
    public string ByTags => this[nameof (ByTags)];

    /// <summary>phrase: by Dates...</summary>
    [ResourceEntry("ByDates", Description = "phrase: by Dates...", LastModified = "2010/09/08", Value = "by Dates...")]
    public string ByDates => this[nameof (ByDates)];

    /// <summary>phrase: No blogs are created</summary>
    [ResourceEntry("NoBlogsAreCreated", Description = "phrase: No blogs are created", LastModified = "2010/09/08", Value = "No blogs are created")]
    public string NoBlogsAreCreated => this[nameof (NoBlogsAreCreated)];

    /// <summary>
    /// phrase: The selected blog was deleted. Please select another blog.
    /// </summary>
    [ResourceEntry("SelectedBlogWasDeletedSelectAnotherBlog", Description = "phrase: The selected blog was deleted. Please select another blog.", LastModified = "2010/09/08", Value = "The selected blog was deleted. Please select another blog.")]
    public string SelectedBlogWasDeletedSelectAnotherBlog => this[nameof (SelectedBlogWasDeletedSelectAnotherBlog)];

    /// <summary>phrase: The selected blog was deleted</summary>
    [ResourceEntry("SelectedBlogWasDeleted", Description = "phrase: The selected blog was deleted", LastModified = "2010/09/08", Value = "The selected blog was deleted")]
    public string SelectedBlogWasDeleted => this[nameof (SelectedBlogWasDeleted)];

    /// <summary>phrase: Choose Lists</summary>
    [ResourceEntry("ChooseLists", Description = "phrase: Choose Lists", LastModified = "2011/04/08", Value = "Choose Lists")]
    public string ChooseLists => this[nameof (ChooseLists)];

    /// <summary>phrase: Narrow by typing list title</summary>
    [ResourceEntry("NarrowListTitle", Description = "phrase: Narrow by typing list title", LastModified = "2011/04/08", Value = "Narrow by typing list title")]
    public string NarrowListTitle => this[nameof (NarrowListTitle)];

    /// <summary>phrase: Choose a List Item</summary>
    [ResourceEntry("ChooseListItem", Description = "phrase: Choose a List Item", LastModified = "2011/04/08", Value = "Choose a List Item")]
    public string ChooseListItem => this[nameof (ChooseListItem)];

    /// <summary>phrase: Narrow by typing list item title</summary>
    [ResourceEntry("NarrowListItemTitle", Description = "phrase: Narrow by typing list item title", LastModified = "2011/04/08", Value = "Narrow by typing list item title")]
    public string NarrowListItemTitle => this[nameof (NarrowListItemTitle)];

    /// <summary>phrase: Which list items to display?</summary>
    [ResourceEntry("WhichListItemsToDisplay", Description = "phrase: Which list items to display?", LastModified = "2011/04/08", Value = "Which list items to display?")]
    public string WhichListItemsToDisplay => this[nameof (WhichListItemsToDisplay)];

    /// <summary>phrase: From all lists</summary>
    [ResourceEntry("FromAllLists", Description = "phrase: From all lists", LastModified = "2011/04/08", Value = "From all lists")]
    public string FromAllLists => this[nameof (FromAllLists)];

    /// <summary>phrase: From selected lists only...</summary>
    [ResourceEntry("FromSelectedListsOnly", Description = "phrase: From selected lists only...", LastModified = "2011/04/08", Value = "From selected lists only...")]
    public string FromSelectedListsOnly => this[nameof (FromSelectedListsOnly)];

    /// <summary>phrase: List is not selected</summary>
    [ResourceEntry("ListNotSelected", Description = "phrase: List is not selected", LastModified = "2011/04/08", Value = "List is not selected")]
    public string ListNotSelected => this[nameof (ListNotSelected)];

    /// <summary>phrase: Select a list</summary>
    [ResourceEntry("SelectList", Description = "phrase: Select a list", LastModified = "2011/04/08", Value = "Select a list")]
    public string SelectList => this[nameof (SelectList)];

    /// <summary>phrase: All published items from selected lists</summary>
    [ResourceEntry("AllPublishedItemsFromSelectedLists", Description = "phrase: All published items from selected lists", LastModified = "2011/04/08", Value = "All published items from selected lists")]
    public string AllPublishedItemsFromSelectedLists => this[nameof (AllPublishedItemsFromSelectedLists)];

    /// <summary>phrase: One particular list item only...</summary>
    [ResourceEntry("OneParticularListItemOnly", Description = "phrase: One particular list item only...", LastModified = "2011/04/08", Value = "One particular list item only...")]
    public string OneParticularListItemOnly => this[nameof (OneParticularListItemOnly)];

    /// <summary>phrase: No selected list item</summary>
    [ResourceEntry("NoSelectedListItem", Description = "phrase: No selected list item", LastModified = "2011/04/08", Value = "No selected list item")]
    public string NoSelectedListItem => this[nameof (NoSelectedListItem)];

    /// <summary>phrase: Select a list item</summary>
    [ResourceEntry("SelectListItem", Description = "phrase: Select a list item", LastModified = "2011/04/08", Value = "Select a list item")]
    public string SelectListItem => this[nameof (SelectListItem)];

    /// <summary>phrase: Selection of list item:</summary>
    [ResourceEntry("SelectionOfListItems", Description = "phrase: Selection of list items:", LastModified = "2011/04/08", Value = "Selection of list items:")]
    public string SelectionOfListItems => this[nameof (SelectionOfListItems)];

    /// <summary>phrase: Display list items published in...</summary>
    [ResourceEntry("DisplayListItemsPublishedIn", Description = "phrase: Display list items published in...", LastModified = "2011/04/08", Value = "Display list items published in...")]
    public string DisplayListItemsPublishedIn => this[nameof (DisplayListItemsPublishedIn)];

    /// <summary>phrase: No lists are created</summary>
    [ResourceEntry("NoListsAreCreated", Description = "phrase: No lists are created", LastModified = "2011/04/08", Value = "No lists are created")]
    public string NoListsAreCreated => this[nameof (NoListsAreCreated)];

    /// <summary>phrase: The selected list was deleted</summary>
    [ResourceEntry("SelectedListWasDeleted", Description = "phrase: The selected list was deleted", LastModified = "2011/04/08", Value = "The selected list was deleted")]
    public string SelectedListWasDeleted => this[nameof (SelectedListWasDeleted)];

    /// <summary>
    /// phrase: The selected list was deleted. Please select another list.
    /// </summary>
    [ResourceEntry("SelectedListWasDeletedSelectAnotherList", Description = "phrase: The selected list was deleted. Please select another list.", LastModified = "2011/04/08", Value = "The selected list was deleted. Please select another list.")]
    public string SelectedListWasDeletedSelectAnotherList => this[nameof (SelectedListWasDeletedSelectAnotherList)];

    /// <summary>phrase: Selection of events</summary>
    [ResourceEntry("SelectionOfEvents", Description = "phrase: Selection of events", LastModified = "2010/09/08", Value = "Selection of events")]
    public string SelectionOfEvents => this[nameof (SelectionOfEvents)];

    /// <summary>phrase: Select an event</summary>
    [ResourceEntry("SelectEvent", Description = "phrase: Select an event", LastModified = "2010/09/08", Value = "Select an event")]
    public string SelectEvent => this[nameof (SelectEvent)];

    /// <summary>phrase: One particular event only...</summary>
    [ResourceEntry("OneParticularEventOnly", Description = "phrase: One particular event only...", LastModified = "2010/09/08", Value = "One particular event only...")]
    public string OneParticularEventOnly => this[nameof (OneParticularEventOnly)];

    /// <summary>phrase: All published events</summary>
    [ResourceEntry("AllPublishedEvents", Description = "phrase: All published events", LastModified = "2010/09/08", Value = "All published events")]
    public string AllPublishedEvents => this[nameof (AllPublishedEvents)];

    /// <summary>phrase: Choose events</summary>
    [ResourceEntry("ChooseEvents", Description = "phrase: Choose events", LastModified = "2010/09/08", Value = "Choose events")]
    public string ChooseEvents => this[nameof (ChooseEvents)];

    /// <summary>phrase: Which events to display?</summary>
    [ResourceEntry("WhichEventsToDisplay", Description = "phrase: Which events to display?", LastModified = "2010/09/08", Value = "Which events to display?")]
    public string WhichEventsToDisplay => this[nameof (WhichEventsToDisplay)];

    /// <summary>phrase: All upcomming, current and past events</summary>
    [ResourceEntry("AllUpcommingCurrentAndPastEvents", Description = "phrase: All upcoming, current, and past events", LastModified = "2010/09/08", Value = "All upcoming, current, and past events")]
    public string AllUpcommingCurrentAndPastEvents => this[nameof (AllUpcommingCurrentAndPastEvents)];

    /// <summary>phrase: Upcomming events only</summary>
    [ResourceEntry("UpcommingEventsOnly", Description = "phrase: Upcoming events only", LastModified = "2010/09/08", Value = "Upcoming events only")]
    public string UpcommingEventsOnly => this[nameof (UpcommingEventsOnly)];

    /// <summary>phrase: Current events only</summary>
    [ResourceEntry("CurrentEventsOnly", Description = "phrase: Current events only", LastModified = "2010/09/08", Value = "Current events only")]
    public string CurrentEventsOnly => this[nameof (CurrentEventsOnly)];

    /// <summary>phrase: Past events only</summary>
    [ResourceEntry("PastEventsOnly", Description = "phrase: Past events only", LastModified = "2010/09/08", Value = "Past events only")]
    public string PastEventsOnly => this[nameof (PastEventsOnly)];

    /// <summary>phrase: Upcomming and current events</summary>
    [ResourceEntry("UpcommingAndCurrentEvents", Description = "phrase: Upcoming and current events", LastModified = "2010/09/08", Value = "Upcoming and current events")]
    public string UpcommingAndCurrentEvents => this[nameof (UpcommingAndCurrentEvents)];

    /// <summary>phrase: Current and past events</summary>
    [ResourceEntry("CurrentAndPastEvents", Description = "phrase: Current and past events", LastModified = "2010/09/08", Value = "Current and past events")]
    public string CurrentAndPastEvents => this[nameof (CurrentAndPastEvents)];

    /// <summary>phrase: No events have been created yet</summary>
    [ResourceEntry("NoEventsHaveBeenCreatedYet", Description = "phrase: No events have been created yet", LastModified = "2010/09/08", Value = "No events have been created yet")]
    public string NoEventsHaveBeenCreatedYet => this[nameof (NoEventsHaveBeenCreatedYet)];

    /// <summary>phrase: All published images from selected library</summary>
    [ResourceEntry("AllPublishedImagesFromSelectedAlbum", Description = "phrase: All published images from selected library", LastModified = "2011/07/13", Value = "All published images from selected library")]
    public string AllPublishedImagesFromSelectedAlbum => this[nameof (AllPublishedImagesFromSelectedAlbum)];

    /// <summary>phrase: Selection of images:</summary>
    [ResourceEntry("SelectionOfImages", Description = "phrase: Selection of images:", LastModified = "2011/02/16", Value = "Selection of images:")]
    public string SelectionOfImages => this[nameof (SelectionOfImages)];

    /// <summary>phrase: Display images published in...</summary>
    [ResourceEntry("DisplayImagesPublishedIn", Description = "phrase: Display images published in...", LastModified = "2010/02/16", Value = "Display images published in...")]
    public string DisplayImagesPublishedIn => this[nameof (DisplayImagesPublishedIn)];

    /// <summary>phrase: All published videos from selected library</summary>
    [ResourceEntry("AllPublishedVideosFromSelectedLibrary", Description = "phrase: All published videos from selected library", LastModified = "2011/02/16", Value = "All published videos from selected library")]
    public string AllPublishedVideosFromSelectedLibrary => this[nameof (AllPublishedVideosFromSelectedLibrary)];

    /// <summary>phrase: Selection of videos:</summary>
    [ResourceEntry("SelectionOfVideos", Description = "phrase: Selection of videos:", LastModified = "2011/02/16", Value = "Selection of videos:")]
    public string SelectionOfVideos => this[nameof (SelectionOfVideos)];

    /// <summary>phrase: Display videos published in...</summary>
    [ResourceEntry("DisplayVideosPublishedIn", Description = "phrase: Display videos published in...", LastModified = "2010/02/16", Value = "Display videos published in...")]
    public string DisplayVideosPublishedIn => this[nameof (DisplayVideosPublishedIn)];

    /// <summary>phrase: All published documents from selected library</summary>
    [ResourceEntry("AllPublishedDocumentsFromSelectedLibrary", Description = "phrase: All published documents from selected library", LastModified = "2011/02/16", Value = "All published documents from selected library")]
    public string AllPublishedDocumentsFromSelectedLibrary => this[nameof (AllPublishedDocumentsFromSelectedLibrary)];

    /// <summary>phrase: Selection of documents:</summary>
    [ResourceEntry("SelectionOfDocuments", Description = "phrase: Selection of documents:", LastModified = "2011/02/16", Value = "Selection of documents:")]
    public string SelectionOfDocuments => this[nameof (SelectionOfDocuments)];

    /// <summary>phrase: Display documents published in...</summary>
    [ResourceEntry("DisplayDocumentsPublishedIn", Description = "phrase: Display documents published in...", LastModified = "2010/02/16", Value = "Display documents published in...")]
    public string DisplayDocumentsPublishedIn => this[nameof (DisplayDocumentsPublishedIn)];

    /// <summary>phrase: Make localizable</summary>
    /// <value>Make localizable</value>
    [ResourceEntry("MakeLocalizable", Description = "phrase: Make localizable", LastModified = "2012/12/05", Value = "Make localizable")]
    public string MakeLocalizable => this[nameof (MakeLocalizable)];

    [ResourceEntry("MakeLocalizableDescription", Description = "", LastModified = "2012/12/05", Value = "The data entered in this field can have different values for every language in the site")]
    public string MakeLocalizableDescription => this[nameof (MakeLocalizableDescription)];

    [ResourceEntry("DataCanBeTranslatedDescription", Description = "", LastModified = "2018/09/11", Value = ", e.g. the data can be translated")]
    public string DataCanBeTranslatedDescription => this[nameof (DataCanBeTranslatedDescription)];

    /// <summary>phrase: Select language version:</summary>
    [ResourceEntry("SelectLanguageVersion", Description = "phrase: Select language version", LastModified = "2010/10/08", Value = "Select language version")]
    public string SelectLanguageVersion => this[nameof (SelectLanguageVersion)];

    /// <summary>phrase: Select content:</summary>
    [ResourceEntry("SelectContent", Description = "phrase: Select content", LastModified = "2010/10/08", Value = "Select content")]
    public string SelectContent => this[nameof (SelectContent)];

    /// <summary>phrase: All languages</summary>
    [ResourceEntry("AllLanguages", Description = "phrase: All languages", LastModified = "2010/10/08", Value = "All languages")]
    public string AllLanguages => this[nameof (AllLanguages)];

    /// <summary>phrase: All content</summary>
    [ResourceEntry("AllContent", Description = "phrase: All content", LastModified = "2010/10/08", Value = "All content")]
    public string AllContent => this[nameof (AllContent)];

    /// <summary>word: Bulgarian</summary>
    [ResourceEntry("Bulgarian", Description = "word: Bulgarian", LastModified = "2010/10/08", Value = "Bulgarian")]
    public string Bulgarian => this[nameof (Bulgarian)];

    /// <summary>word: English</summary>
    [ResourceEntry("English", Description = "word: English", LastModified = "2010/10/08", Value = "English")]
    public string English => this[nameof (English)];

    /// <summary>word: Russian</summary>
    [ResourceEntry("Russian", Description = "word: Russian", LastModified = "2010/10/08", Value = "Russian")]
    public string Russian => this[nameof (Russian)];

    /// <summary>word: French</summary>
    [ResourceEntry("French", Description = "word: French", LastModified = "2010/10/08", Value = "French")]
    public string French => this[nameof (French)];

    /// <summary>word: Translations</summary>
    [ResourceEntry("Translations", Description = "word: Translations", LastModified = "2010/10/08", Value = "Translations")]
    public string Translations => this[nameof (Translations)];

    /// <summary>phrase: Argument {0} cannot be zero (0)</summary>
    /// <value>Error message. {0} is a placeholder for the argument name.</value>
    [ResourceEntry("ArgumentNameCannotBeZero", Description = "Error message. {0} is a placeholder for the argument name", LastModified = "2010/10/12", Value = "Argument {0} cannot be zero (0)")]
    public string ArgumentNameCannotBeZero => this[nameof (ArgumentNameCannotBeZero)];

    /// <summary>Custom sorting</summary>
    /// <value>Custom sorting</value>
    [ResourceEntry("CustomSorting", Description = "Custom sorting", LastModified = "2010/10/12", Value = "Custom sorting")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>Ascending</summary>
    /// <value>Ascending</value>
    [ResourceEntry("Ascending", Description = "Ascending", LastModified = "2010/10/12", Value = "Ascending")]
    public string Ascending => this[nameof (Ascending)];

    /// <summary>Ascending</summary>
    /// <value>Ascending</value>
    [ResourceEntry("Descending", Description = "Descending", LastModified = "2010/10/12", Value = "Descending")]
    public string Descending => this[nameof (Descending)];

    /// <summary>phrase: Select a CSS file</summary>
    /// <value>Ascending</value>
    [ResourceEntry("SelectCssFile", Description = "phrase: Select a CSS file", LastModified = "2010/10/18", Value = "Select a CSS file")]
    public string SelectCssFile => this[nameof (SelectCssFile)];

    /// <summary>phrase: Select a JavaScript file</summary>
    /// <value>Ascending</value>
    [ResourceEntry("SelectJsFile", Description = "phrase: Select a JavaScript file", LastModified = "2012/01/05", Value = "Select a JavaScript file")]
    public string SelectJsFile => this[nameof (SelectJsFile)];

    /// <summary>Select a template</summary>
    /// <value>Select a template</value>
    [ResourceEntry("SelectATemplate", Description = "phrase: Select a template", LastModified = "2010/10/18", Value = "Select a template")]
    public string SelectATemplate => this[nameof (SelectATemplate)];

    /// <summary>Generate thumbnails</summary>
    /// <value>Select a template</value>
    [ResourceEntry("GenerateThumbnails", Description = "phrase: Generate thumbnails", LastModified = "2010/10/18", Value = "Generate thumbnails")]
    public string GenerateThumbnails => this[nameof (GenerateThumbnails)];

    /// <summary>in order to create specific settings for {0}</summary>
    [ResourceEntry("CreateSpecificSettingsFor", Description = "in order to create specific settings for {0}", LastModified = "2012/09/24", Value = "in order to create specific settings for {0}")]
    public string CreateSpecificSettingsFor => this[nameof (CreateSpecificSettingsFor)];

    /// <summary>These settings are inherited from the</summary>
    [ResourceEntry("InheritedSettingsText", Description = "These settings are inherited from the", LastModified = "2012/09/24", Value = "These settings are inherited from the")]
    public string InheritedSettingsText => this[nameof (InheritedSettingsText)];

    /// <summary>
    /// These settings are inherited from the settings for All sites.
    /// </summary>
    [ResourceEntry("InheritedAllSitesSettingsText", Description = "These settings are inherited from the settings for All sites.", LastModified = "2018/09/18", Value = "These settings are inherited from the settings for All sites.")]
    public string InheritedAllSitesSettingsText => this[nameof (InheritedAllSitesSettingsText)];

    /// <summary>The settings for site {0} are different from the</summary>
    [ResourceEntry("SettingsStatusPerSite", Description = "The settings for site {0} are different from the", LastModified = "2012/09/26", Value = "The settings for site {0} are different from the")]
    public string SettingsStatusPerSite => this[nameof (SettingsStatusPerSite)];

    /// <summary>
    /// The settings for site {0} are different from the settings for All sites.
    /// </summary>
    [ResourceEntry("BasicSettingsStatusPerSite", Description = "The settings for site {0} are different from the settings for All sites.", LastModified = "2018/09/18", Value = "The settings for site {0} are different from the settings for All sites.")]
    public string BasicSettingsStatusPerSite => this[nameof (BasicSettingsStatusPerSite)];

    /// <summary>Global settings</summary>
    [ResourceEntry("GlobalSettings", Description = "Global settings", LastModified = "2012/09/24", Value = "Global settings")]
    public string GlobalSettings => this[nameof (GlobalSettings)];

    /// <summary>Break inheritance</summary>
    [ResourceEntry("BreakInheritance", Description = "Break inheritance", LastModified = "2012/09/24", Value = "Break inheritance")]
    public string BreakInheritance => this[nameof (BreakInheritance)];

    /// <summary>Inherit settings from Global profile</summary>
    [ResourceEntry("InheritGlobalProfileSettings", Description = "Inherit settings from Global profile", LastModified = "2012/09/26", Value = "Inherit settings from Global profile")]
    public string InheritGlobalProfileSettings => this[nameof (InheritGlobalProfileSettings)];

    /// <summary>Inherit settings from Global profile</summary>
    [ResourceEntry("InheritAllSitesSettings", Description = "Inherit from All sites settings", LastModified = "2018/09/18", Value = "Inherit from All sites settings")]
    public string InheritAllSitesSettings => this[nameof (InheritAllSitesSettings)];

    /// <summary>Phrase: Time zone settings</summary>
    [ResourceEntry("TimeZoneSettings", Description = "Phrase: Time zone settings", LastModified = "2012/10/12", Value = "Time zone settings")]
    public string TimeZoneSettings => this[nameof (TimeZoneSettings)];

    /// <summary>Phrase: Project name</summary>
    [ResourceEntry("ProjectName", Description = "Phrase: Project name", LastModified = "2010/10/21", Value = "Project name")]
    public string ProjectName => this[nameof (ProjectName)];

    /// <summary>Phrase: Time zone</summary>
    [ResourceEntry("TimeZone", Description = "Phrase: Time zone", LastModified = "2010/10/21", Value = "Time zone")]
    public string TimeZone => this[nameof (TimeZone)];

    /// <summary>
    /// Phrase: Automatically adjust clock for Daylight Saving Time
    /// </summary>
    [ResourceEntry("AutomaticallyAdjustClockForDaylightSavingTime", Description = "Phrase: Automatically adjust clock for Daylight Saving Time", LastModified = "2010/10/21", Value = "Automatically adjust clock for Daylight Saving Time")]
    public string AutomaticallyAdjustClockForDaylightSavingTime => this[nameof (AutomaticallyAdjustClockForDaylightSavingTime)];

    /// <summary>
    /// Phrase: Daylight Saving Time ends on 31 October at 04:00. The clock is set to go back 1 hour at that time.
    /// </summary>
    [ResourceEntry("DaylightSavingTimeEndsMessage", Description = "Phrase: Daylight Saving Time ends on 31 October at 04:00. The clock is set to go back 1 hour at that time.", LastModified = "2011/02/07", Value = "Daylight Saving Time ends on {0} at {1}. The clock is set to go back {2} hour at that time.")]
    public string DaylightSavingTimeEndsMessage => this[nameof (DaylightSavingTimeEndsMessage)];

    /// <summary>
    /// Phrase: Daylight Saving Time begins on 27 March 2011 at 03:00. The clock is set to go forward 1 hour at that time.
    /// </summary>
    [ResourceEntry("DaylightSavingTimeBeginsMessage", Description = "Phrase: Daylight Saving Time begins on 27 March 2011 at 03:00. The clock is set to go forward 1 hour at that time.", LastModified = "2011/02/07", Value = "Daylight Saving Time begins on {0} at {1}. The clock is set to go forward {2} hour at that time.")]
    public string DaylightSavingTimeBeginsMessage => this[nameof (DaylightSavingTimeBeginsMessage)];

    /// <summary>phrase: Set as default</summary>
    [ResourceEntry("SetAsDefault", Description = "phrase: Set as default", LastModified = "2010/10/23", Value = "Set as default")]
    public string SetAsDefault => this[nameof (SetAsDefault)];

    [ResourceEntry("AddLanguagesHellip", Description = "phrase: Add languages", LastModified = "2010/10/25", Value = "Add languages")]
    public string AddLanguagesHellip => this[nameof (AddLanguagesHellip)];

    /// <summary>phrase: Add a server</summary>
    [ResourceEntry("AddServer", Description = "phrase: Add a server", LastModified = "2011/10/05", Value = "Add a server")]
    public string AddServer => this[nameof (AddServer)];

    /// <summary>Phrase: Translate from:</summary>
    [ResourceEntry("TranslateFrom", Description = "Phrase: Translate from:", LastModified = "2010/10/21", Value = "Translate from:")]
    public string TranslateFrom => this[nameof (TranslateFrom)];

    /// <summary>phrase: Default language for the backend system</summary>
    [ResourceEntry("DefaultLanguageBackendSystem", Description = "phrase: Default language for the backend system", LastModified = "2010/10/26", Value = "Default language for the backend system")]
    public string DefaultLanguageBackendSystem => this[nameof (DefaultLanguageBackendSystem)];

    /// <summary>word: Languages</summary>
    [ResourceEntry("Languages", Description = "word: Languages", LastModified = "2010/10/26", Value = "Languages")]
    public string Languages => this[nameof (Languages)];

    /// <summary>phrase: Languages for public content</summary>
    [ResourceEntry("LanguagesPublicContent", Description = "phrase: Languages for public content", LastModified = "2010/11/05", Value = "Languages for public content")]
    public string LanguagesPublicContent => this[nameof (LanguagesPublicContent)];

    /// <summary>phrase: Languages for public content</summary>
    [ResourceEntry("EnabledLanguagesForPublicContent", Description = "phrase: Languages enabled for public content", LastModified = "2020/08/13", Value = "Languages enabled for public content")]
    public string EnabledLanguagesForPublicContent => this[nameof (EnabledLanguagesForPublicContent)];

    /// <summary>phrase: Use all languages enabled for the system</summary>
    [ResourceEntry("UseAllLanguages", Description = "phrase: Use all languages enabled for the system", LastModified = "2020/08/11", Value = "Use all languages enabled for the system")]
    public string UseAllLanguages => this[nameof (UseAllLanguages)];

    /// <summary>phrase: Use selected languages...</summary>
    [ResourceEntry("UseSelectedLanguages", Description = "phrase: Enabled languages for public content...", LastModified = "2020/08/11", Value = "Use selected languages...")]
    public string UseSelectedLanguages => this[nameof (UseSelectedLanguages)];

    /// <summary>phrase: Select at least one language.</summary>
    [ResourceEntry("AddAtLeastOneLanguage", Description = "phrase: Select at least one language", LastModified = "2020/08/13", Value = "Select at least one language")]
    public string AddAtLeastOneLanguage => this[nameof (AddAtLeastOneLanguage)];

    /// <summary>
    /// phrase: To enable more languages, go to Administration &gt; Settings &gt; Languages
    /// </summary>
    [ResourceEntry("EnableMoreLanguagesUrlLocation", Description = "phrase: To enable more languages, go to Administration > Settings > Languages.", LastModified = "2020/08/11", Value = "To enable more languages, go to <a href='{0}' target='_blank'>Administration &gt; Settings &gt; Languages</a>.")]
    public string EnableMoreLanguagesUrlLocation => this[nameof (EnableMoreLanguagesUrlLocation)];

    /// <summary>
    /// phrase: Languages are enabled for the system through Administration &gt; Settings &gt; Languages. When a language is enabled for the system it will be automatically added to this site.
    /// </summary>
    [ResourceEntry("EnableLanguagesTooltip", Description = "phrase: Languages are enabled for the system through Administration > Settings > Languages. When a language is enabled for the system it will be automatically added to this site.", LastModified = "2020/08/11", Value = "Languages are enabled for the system through <a href='{0}' target='_blank'>Administration &gt; Settings &gt; Languages</a>. When a language is enabled for the system it will be automatically added to this site.")]
    public string EnableLanguagesTooltip => this[nameof (EnableLanguagesTooltip)];

    /// <summary>phrase: Languages for this site</summary>
    [ResourceEntry("LanguagesForThisSite", Description = "phrase: Languages for this site", LastModified = "2020/08/11", Value = "Languages for this site")]
    public string LanguagesForThisSite => this[nameof (LanguagesForThisSite)];

    /// <summary>phrase: All languages enabled for the system</summary>
    [ResourceEntry("AllLanguagesEnabledForTheSystem", Description = "phrase: All languages enabled for the system", LastModified = "2020/08/11", Value = "All languages enabled for the system")]
    public string AllLanguagesEnabledForTheSystem => this[nameof (AllLanguagesEnabledForTheSystem)];

    /// <summary>phrase: Multiple Languages Description</summary>
    [ResourceEntry("MultipleLanguagesDescription", Description = "Description for more than 10 languages", LastModified = "2013/08/12", Value = "In case you plan to use more than 10 languages in the public site some additional configuration steps are required. <br/>Read <a href='{0}' target='_blank'>our documentation</a> for instructions.")]
    [Obsolete("Multilingual Split Tables mode has been depricated - no need to consider addtional configuration steps for more than 10 languages")]
    public string MultipleLanguagesDescription => this[nameof (MultipleLanguagesDescription)];

    /// <summary>Gets External Link: Managing languages</summary>
    [ResourceEntry("ExternalLinkManagingLanguages", Description = "External Link: Managing languages", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/administration-language-settings")]
    public string ExternalLinkManagingLanguages => this[nameof (ExternalLinkManagingLanguages)];

    /// <summary>phrase: Manage backend languages</summary>
    [ResourceEntry("ManageBackendLanguages", Description = "phrase: Manage backend languages", LastModified = "2010/10/26", Value = "Manage backend languages")]
    public string ManageBackendLanguages => this[nameof (ManageBackendLanguages)];

    /// <summary>phrase: Languages for the backend system</summary>
    [ResourceEntry("LanguagesForBackendSystem", Description = "phrase: Languages for the backend system", LastModified = "2010/10/26", Value = "Languages for the backend system")]
    public string LanguagesForBackendSystem => this[nameof (LanguagesForBackendSystem)];

    /// <summary>phrase: Close languages for the backend system</summary>
    [ResourceEntry("CloseLanguagesBackendSystem", Description = "phrase: Close languages for the backend system", LastModified = "2010/10/26", Value = "Close languages for the backend system")]
    public string CloseLanguagesBackendSystem => this[nameof (CloseLanguagesBackendSystem)];

    /// <summary>phrase: Multilingual URLs</summary>
    [ResourceEntry("MultilingualUrls", Description = "phrase: Multilingual URLs", LastModified = "2010/10/27", Value = "Multilingual URLs")]
    public string MultilingualUrls => this[nameof (MultilingualUrls)];

    /// <summary>word: Directories</summary>
    [ResourceEntry("Directories", Description = "word: Directories", LastModified = "2010/10/27", Value = "Directories")]
    public string Directories => this[nameof (Directories)];

    /// <summary>phrase: Different domains</summary>
    [ResourceEntry("DifferentDomains", Description = "phrase: Different domains", LastModified = "2010/10/27", Value = "Different domains")]
    public string DifferentDomains => this[nameof (DifferentDomains)];

    /// <summary>
    /// phrase: Example: domain.com/contact-us - English; domain.com/es/contacto - Spanish
    /// </summary>
    [ResourceEntry("ExampleDirectoriesUrlStrategy", Description = "phrase: Example: domain.com/contact-us - English; domain.com/es/contacto - Spanish", LastModified = "2010/10/27", Value = "<strong>Example:</strong><br />domain.com/contact-us - English;<br />domain.com/es/contacto - Spanish")]
    public string ExampleDirectoriesUrlStrategy => this[nameof (ExampleDirectoriesUrlStrategy)];

    /// <summary>
    /// phrase: Example: domain.com - English; es.domain.com or domain.es - Spanish
    /// </summary>
    [ResourceEntry("ExampleDomainsUrlStrategy", Description = "phrase: Example: domain.com - English; es.domain.com or domain.es - Spanish", LastModified = "2010/10/27", Value = "<strong>Example:</strong><br />domain.com - English;<br />es.domain.com or domain.es - Spanish")]
    public string ExampleDomainsUrlStrategy => this[nameof (ExampleDomainsUrlStrategy)];

    /// <summary>phrase: Specify domains for each language</summary>
    [ResourceEntry("SpecifyDomainForEachLanguage", Description = "phrase: Specify domains for each language", LastModified = "2019/08/28", Value = "Specify domains for each language")]
    public string SpecifyDomainForEachLanguage => this[nameof (SpecifyDomainForEachLanguage)];

    /// <summary>phrase: Settings have been updated.</summary>
    [ResourceEntry("SettingsUpdated", Description = "phrase: Settings have been updated.", LastModified = "2010/11/05", Value = "Settings have been updated.")]
    public string SettingsUpdated => this[nameof (SettingsUpdated)];

    /// <summary>Title: Language selector</summary>
    [ResourceEntry("LanguageSelectorDesignerEditorTitle", Description = "Title: Language selector", LastModified = "2010/10/22", Value = "Language selector")]
    public string LanguageSelectorDesignerEditorTitle => this[nameof (LanguageSelectorDesignerEditorTitle)];

    /// <summary>Label: Display language selector as...</summary>
    [ResourceEntry("LanguageSelectorModeText", Description = "Label: Display language selector as...", LastModified = "2010/10/22", Value = "Display language selector as...")]
    public string LanguageSelectorModeText => this[nameof (LanguageSelectorModeText)];

    /// <summary>Label - no translation action setting - header</summary>
    [ResourceEntry("HeaderLanguagesWithoutTranslation", Description = "Label - no translation action setting - header", LastModified = "2011/01/04", Value = "What to do with languages without translations?")]
    public string HeaderLanguagesWithoutTranslation => this[nameof (HeaderLanguagesWithoutTranslation)];

    /// <summary>Label - no translation action setting - explanation</summary>
    [ResourceEntry("HeaderLanguagesWithoutTranslationExpl", Description = "Label - no translation action setting - explanation", LastModified = "2011/01/04", Value = "Some pages or content items may not be translated to all languages. How should the language selector behave when a translation is missing?")]
    public string HeaderLanguagesWithoutTranslationExpl => this[nameof (HeaderLanguagesWithoutTranslationExpl)];

    /// <summary>Label: Hide the link to the missing translation</summary>
    [ResourceEntry("NoTranslationHideLink", Description = "Label: Hide the link to the missing translation", LastModified = "2010/10/26", Value = "Hide the link to the missing translation")]
    public string NoTranslationHideLink => this[nameof (NoTranslationHideLink)];

    /// <summary>
    /// Label: Redirect to the home page in the language of the missing translation
    /// </summary>
    [ResourceEntry("NoTranslationRedirectToPage", Description = "Label: Redirect to the home page in the language of the missing translation", LastModified = "2010/10/26", Value = "Redirect to the home page in the language of the missing translation")]
    public string NoTranslationRedirectToPage => this[nameof (NoTranslationRedirectToPage)];

    /// <summary>Label: Include the current language in the selector</summary>
    [ResourceEntry("IncludeCurrentLanguage", Description = "Label: Include the current language in the selector", LastModified = "2010/10/26", Value = "Include the current language in the selector")]
    public string IncludeCurrentLanguage => this[nameof (IncludeCurrentLanguage)];

    /// <summary>Label: Horizontal list</summary>
    [ResourceEntry("HorizontalList", Description = "Label: Horizontal list", LastModified = "2010/10/26", Value = "Horizontal list")]
    public string HorizontalList => this[nameof (HorizontalList)];

    /// <summary>Label: Vertical list</summary>
    [ResourceEntry("VerticalList", Description = "Label: Vertical list", LastModified = "2010/10/26", Value = "Vertical list")]
    public string VerticalList => this[nameof (VerticalList)];

    /// <summary>Label: Drop-down menu</summary>
    [ResourceEntry("DropDownMenu", Description = "Label: Drop-down menu", LastModified = "2010/10/26", Value = "Drop-down menu")]
    public string DropDownMenu => this[nameof (DropDownMenu)];

    /// <summary>
    /// Message: This control is only available in multilingual mode.
    /// </summary>
    [ResourceEntry("OnlyAvailableInMultilingualMode", Description = "Message: This control is only available in multilingual mode.", LastModified = "2012/01/05", Value = "This control is only available in multilingual mode.")]
    public string OnlyAvailableInMultilingualMode => this[nameof (OnlyAvailableInMultilingualMode)];

    /// <summary>
    /// message: This list contains only the languages enabled for public content. To enable more languages, go to Settings &gt; Languages
    /// </summary>
    [ResourceEntry("MultisiteMultilingualMessage", Description = "message: This list contains only the languages enabled for public content. To enable more languages, go to Settings &gt; Languages", LastModified = "2017/09/26", Value = "This list contains only the languages enabled for public content.<br />To enable more languages, go to <a href='{0}' target='_blank'>Settings &gt; Languages</a>.")]
    public string MultisiteMultilingualMessage => this[nameof (MultisiteMultilingualMessage)];

    /// <summary>
    /// message: Only languages enabled for the system are displayed here. To enable more languages, go to Settings &gt; Languages.
    /// </summary>
    [ResourceEntry("EnableSystemLanguagesMessage", Description = "message: Only languages enabled for the system are displayed here. To enable more languages, go to Settings > Languages.", LastModified = "2012/08/11", Value = "Only languages enabled for the system are displayed here. To enable more languages, go to <br /><a href='{0}' target='_blank'>Settings &gt; Languages</a>.")]
    public string EnableSystemLanguagesMessage => this[nameof (EnableSystemLanguagesMessage)];

    /// <summary>
    /// message: message: Languages added here may not be automatically added to all existsing sites. That depends on the properties of each site, set in Manage sites.
    /// </summary>
    [ResourceEntry("GlobalLanguagesManagementMessage", Description = "message: Languages added here may not be automatically added to all existsing sites. That depends on the properties of each site, set in Manage sites.", LastModified = "2020/08/13", Value = "Languages added here may not be automatically added to all existsing sites. That depends on the properties of each site, set in <a href='{0}' target='_blank'>Manage sites</a>.")]
    public string GlobalLanguagesManagementMessage => this[nameof (GlobalLanguagesManagementMessage)];

    /// <summary>
    /// message: Languages added here are not automatically added to your site, because it is configured to use selected languages only. You can configure languages used in your site in Site properties.
    /// </summary>
    [ResourceEntry("GlobalLanguagesManagementMessageSingleSite", Description = "message: Languages added here are not automatically added to your site, because it is configured to use selected languages only. You can configure languages used in your site in Site properties.", LastModified = "2020/08/13", Value = "Languages added here are not automatically added to your site, because it is configured to use selected languages only. You can configure languages used in your site in <a href='{0}' target='_blank'>Site properties</a>.")]
    public string GlobalLanguagesManagementMessageSingleSite => this[nameof (GlobalLanguagesManagementMessageSingleSite)];

    /// <summary>
    /// message: For each language, add language-specific domains for any of your sites in a comma-separated list.<br />Next, go to <a href="{0}">Manage sites</a> and for each site add its language-specific domains as aliases.
    /// </summary>
    [ResourceEntry("DomainLocalizationStrategyMessage", Description = "message: For each language, add language-specific domains for any of your sites in a comma-separated list.<br />Next, go to <a href='{0}'>Manage sites</a> and for each site add its language-specific domains as aliases.", LastModified = "2019/08/28", Value = "For each language, add language-specific domains for any of your sites in a comma-separated list.<br />Next, go to <a href='{0}'>Manage sites</a> and for each site add its language-specific domains as aliases.")]
    public string DomainLocalizationStrategyMessage => this[nameof (DomainLocalizationStrategyMessage)];

    /// <summary>phrase: Show cultures</summary>
    [ResourceEntry("ShowCultures", Description = "phrase: Show cultures", LastModified = "2010/10/28", Value = "Show cultures")]
    public string ShowCultures => this[nameof (ShowCultures)];

    /// <summary>phrase: Show only languages</summary>
    [ResourceEntry("ShowOnlyLanguages", Description = "phrase: Show only languages", LastModified = "2010/10/28", Value = "Show only languages")]
    public string ShowOnlyLanguages => this[nameof (ShowOnlyLanguages)];

    /// <summary>phrase: Select languages</summary>
    [ResourceEntry("SelectLanguages", Description = "phrase: Select languages", LastModified = "2010/11/01", Value = "Select languages")]
    public string SelectLanguages => this[nameof (SelectLanguages)];

    /// <summary>phrase: Invalid domain</summary>
    [ResourceEntry("InvalidDomain", Description = "phrase: Invalid domain", LastModified = "2010/10/28", Value = "Invalid domain")]
    public string InvalidDomain => this[nameof (InvalidDomain)];

    /// <summary>
    /// phrase: One or more of the selected languages are already added.
    /// </summary>
    [ResourceEntry("SelectedLanguagesAlreadyAdded", Description = "phrase: One or more of the selected languages are already added.", LastModified = "2010/11/02", Value = "One or more of the selected languages are already added.")]
    public string SelectedLanguagesAlreadyAdded => this[nameof (SelectedLanguagesAlreadyAdded)];

    /// <summary>Label: custom</summary>
    [ResourceEntry("CustomLowercase", Description = "Labels: custom", LastModified = "2010/11/11", Value = "custom")]
    public string CustomLowercase => this[nameof (CustomLowercase)];

    /// <summary>phrase: No comments</summary>
    [ResourceEntry("NoCommentsLabel", Description = "phrase: The label to be set if there are no comments.", LastModified = "2010/10/29", Value = "Go comment!")]
    public string NoCommentsLabel => this[nameof (NoCommentsLabel)];

    /// <summary>phrase: Go back</summary>
    [ResourceEntry("GoBack", Description = "phrase: Go back", LastModified = "2010/11/16", Value = "Go back")]
    public string GoBack => this[nameof (GoBack)];

    /// <summary>Message: The starting page is unpublished.</summary>
    [ResourceEntry("StartingPageUnpublished", Description = "Message: The starting page is unpublished.", LastModified = "2010/12/09", Value = "Warning: the selected starting page is unpublished.")]
    public string StartingPageUnpublished => this[nameof (StartingPageUnpublished)];

    /// <summary>
    /// Message: The selected starting page is not accessible.
    /// </summary>
    [ResourceEntry("StartingPageIsNotAccessible", Description = "Message: The selected starting page is not accessible.", LastModified = "2011/03/01", Value = "Warning: the selected starting page is not accessible.")]
    public string StartingPageIsNotAccessible => this[nameof (StartingPageIsNotAccessible)];

    /// <summary>phrase: Locked by {0} | {1}</summary>
    [ResourceEntry("LockedByFormat", Description = "phrase: Locked by {0} | {1}", LastModified = "2010/11/17", Value = "Locked by {0} | {1}")]
    public string LockedByFormat => this[nameof (LockedByFormat)];

    /// <summary>phrase: (newer than published)</summary>
    [ResourceEntry("NewerThanPublishedInParentheses", Description = "phrase: (newer than published)", LastModified = "2010/11/17", Value = "(newer than published)")]
    public string NewerThanPublishedInParentheses => this[nameof (NewerThanPublishedInParentheses)];

    /// <summary>phrase: newer than published</summary>
    [ResourceEntry("NewerThanPublished", Description = "phrase: newer than published", LastModified = "2018/03/29", Value = "newer than published")]
    public string NewerThanPublished => this[nameof (NewerThanPublished)];

    /// <summary>word: Back</summary>
    [ResourceEntry("Back", Description = "word: Back", LastModified = "2010/11/30", Value = "Back")]
    public string Back => this[nameof (Back)];

    /// <summary>phrase: CustomFieldsSection</summary>
    [ResourceEntry("CustomFieldsSection", Description = "phrase: CustomFieldsSection", LastModified = "2018/08/14", Value = "Custom fields")]
    public string CustomFieldsSection => this[nameof (CustomFieldsSection)];

    /// <summary>phrase: GoToTheSite</summary>
    [ResourceEntry("GoToTheSite", Description = "phrase: GoToTheSite", LastModified = "2010/12/10", Value = "Live site")]
    public string GoToTheSite => this[nameof (GoToTheSite)];

    /// <summary>phrase: Opens live site in a new window</summary>
    [ResourceEntry("GoToTheSiteTitle", Description = "phrase: Opens live site in a new window", LastModified = "2010/12/10", Value = "Opens live site in a new window")]
    public string GoToTheSiteTitle => this[nameof (GoToTheSiteTitle)];

    /// <summary>phrase: Yes, Stop syncing</summary>
    [ResourceEntry("YesStopSyncing", Description = "phrase: Yes, Stop syncing", LastModified = "2010/12/14", Value = "Yes, Stop syncing")]
    public string YesStopSyncing => this[nameof (YesStopSyncing)];

    /// <summary>The title of the widget.</summary>
    [ResourceEntry("WidgetTitle", Description = "The title of the widget.", LastModified = "2010/12/16", Value = "Widget title")]
    public string WidgetTitle => this[nameof (WidgetTitle)];

    /// <summary>The description of the widget.</summary>
    [ResourceEntry("WidgetDescription", Description = "The description of the widget.", LastModified = "2010/12/16", Value = "Widget description")]
    public string WidgetDescription => this[nameof (WidgetDescription)];

    /// <summary>NameForDevs</summary>
    [ResourceEntry("NameForDevs", Description = "word", LastModified = "2011/02/03", Value = "Developer name (used in code)")]
    public string NameForDevs => this[nameof (NameForDevs)];

    /// <summary>
    /// Error message: You do not have the permissions to delete this item.
    /// </summary>
    [ResourceEntry("YouDoNotHaveThePermissionsToDeleteThisItem", Description = "This error message appears when the user tries to delete a single item, but has no permissions to do it.", LastModified = "2010/12/21", Value = "You do not have the permissions to delete this item.")]
    public string YouDoNotHaveThePermissionsToDeleteThisItem => this[nameof (YouDoNotHaveThePermissionsToDeleteThisItem)];

    /// <summary>
    /// Error message: You do not have the permissions to delete these items.
    /// </summary>
    [ResourceEntry("YouDoNotHaveThePermissionsToDeleteTheseItems", Description = "This error message appears when the user tries to delete multiple items, but has no permissions delete any of them.", LastModified = "2010/12/21", Value = "You do not have the permissions to delete these items.")]
    public string YouDoNotHaveThePermissionsToDeleteTheseItems => this[nameof (YouDoNotHaveThePermissionsToDeleteTheseItems)];

    /// <summary>
    /// Error message: You do not have the permissions to delete {0} of the items.
    /// </summary>
    [ResourceEntry("YouDoNotHaveThePermissionsToDeleteSomeOfTheItems", Description = "This error message appears when the user tries to delete multiple items, but has no permissions delete some of them.", LastModified = "2010/12/21", Value = "You do not have the permissions to delete {0} of the items.")]
    public string YouDoNotHaveThePermissionsToDeleteSomeOfTheItems => this[nameof (YouDoNotHaveThePermissionsToDeleteSomeOfTheItems)];

    /// <summary>
    /// Confirmation message: Are you sure you want to delete this item?
    /// </summary>
    [ResourceEntry("WhatDoYouWantToDelete", Description = "This message appears when deleting an item in multilingual mode.", LastModified = "2018/03/23", Value = "Are you sure you want to delete this item?")]
    public string WhatDoYouWantToDelete => this[nameof (WhatDoYouWantToDelete)];

    /// <summary>Button label: Delete all translations</summary>
    [ResourceEntry("DeleteAllTranslations", Description = "Button label: Delete all translations", LastModified = "2010/12/23", Value = "Delete all translations")]
    public string DeleteAllTranslations => this[nameof (DeleteAllTranslations)];

    /// <summary>Button label: Delete only translation</summary>
    [ResourceEntry("DeleteOnlyTranslation", Description = "Button label: Delete only translation", LastModified = "2010/12/23", Value = "Delete only {0} translation")]
    public string DeleteOnlyTranslation => this[nameof (DeleteOnlyTranslation)];

    /// <summary>Button label: Delete permanently translation</summary>
    [ResourceEntry("DeletePermanentlyTranslation", Description = "Button label: Delete permanently translation", LastModified = "2018/03/22", Value = "Delete permanently {0} translation")]
    public string DeletePermanentlyTranslation => this[nameof (DeletePermanentlyTranslation)];

    /// <summary>Phrase: Changes are successfully saved.</summary>
    [ResourceEntry("ChangesSuccessfullySaved", Description = "Label: Changes are successfully saved.", LastModified = "2011/03/14", Value = "Changes are successfully saved.")]
    public string ChangesSuccessfullySaved => this[nameof (ChangesSuccessfullySaved)];

    /// <summary>phrase: Link to</summary>
    [ResourceEntry("LinkToRadiosLabel", Description = "phrase: Link to", LastModified = "2011/01/18", Value = "Link to")]
    public string LinkToRadiosLabel => this[nameof (LinkToRadiosLabel)];

    /// <summary>phrase: for all translations</summary>
    [ResourceEntry("ForAllTranslations", Description = "phrase: for all translations", LastModified = "2011/01/21", Value = "for all translations")]
    public string ForAllTranslations => this[nameof (ForAllTranslations)];

    /// <summary>phrase: Settings.</summary>
    [ResourceEntry("Settings", Description = "phrase: Settings", LastModified = "2011/02/02", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Label: Sitefinity CMS 13.3</summary>
    [ResourceEntry("Sitefinity40", Description = "Label: Sitefinity CMS 13.3", LastModified = "2020/08/24", Value = "Sitefinity CMS 13.3")]
    public string Sitefinity40 => this[nameof (Sitefinity40)];

    /// <summary>
    /// Label: Copyright © 2005-2021 Progress Software Corporation and/or one of its subsidiaries or affiliates. All rights reserved.
    /// </summary>
    [ResourceEntry("TelerikIncAllRightsReserved", Description = "Label: Copyright © 2005-2021 Progress Software Corporation and/or one of its subsidiaries or affiliates. All rights reserved.", LastModified = "2020/01/02", Value = "Copyright © 2005-2021 Progress Software Corporation and/or one of its subsidiaries or affiliates. All rights reserved.")]
    public string TelerikIncAllRightsReserved => this[nameof (TelerikIncAllRightsReserved)];

    /// <summary>Footer Label: Documentation</summary>
    [ResourceEntry("FooterDocumentation", Description = "Footer Label: Documentation", LastModified = "2011/01/14", Value = "Documentation")]
    public string FooterDocumentation => this[nameof (FooterDocumentation)];

    /// <summary>
    /// Gets External Link: Footer documentation external link
    /// </summary>
    [ResourceEntry("ExternalLinkFooterDocumentation", Description = "External Link: Footer documentation external link", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms?utm_source=sitefinity&utm_medium=admin&utm_campaign=documentation")]
    public string ExternalLinkFooterDocumentation => this[nameof (ExternalLinkFooterDocumentation)];

    /// <summary>Footer Label: Whats new</summary>
    [ResourceEntry("FooterWhatsNew", Description = "Footer Label: What's new", LastModified = "2018/04/17", Value = "What's new")]
    public string FooterWhatsNew => this[nameof (FooterWhatsNew)];

    /// <summary>Gets External Link: Footer Whats new external link</summary>
    [ResourceEntry("ExternalLinkFooterWhatsNew", Description = "External Link: Footer Whats new external link", LastModified = "2018/10/23", Value = "https://www.progress.com/sitefinity-cms/whats-new?ref=sf-dashboard")]
    public string ExternalLinkFooterWhatsNew => this[nameof (ExternalLinkFooterWhatsNew)];

    /// <summary>Footer Label: Feedback</summary>
    [ResourceEntry("FooterFeedback", Description = "Footer Label: Feedback", LastModified = "2018/04/17", Value = "Feedback")]
    public string FooterFeedback => this[nameof (FooterFeedback)];

    /// <summary>
    /// Gets External Link: Footer Feedback portal external link
    /// </summary>
    [ResourceEntry("ExternalLinkFooterFeedback", Description = "External Link: Footer Feedback portal external link", LastModified = "2020/03/18", Value = "https://sitefinity.ideas.aha.io/")]
    public string ExternalLinkFooterFeedback => this[nameof (ExternalLinkFooterFeedback)];

    /// <summary>
    /// Dashboard Label: Edit content directly browsing the site
    /// </summary>
    [ResourceEntry("EditContentDirectlyBrowsingTheSite", Description = "Dashboard Label: Edit content directly browsing the site", LastModified = "2011/01/14", Value = "Edit content directly browsing the site")]
    public string EditContentDirectlyBrowsingTheSite => this[nameof (EditContentDirectlyBrowsingTheSite)];

    /// <summary>Dashboard Label: Watch Video</summary>
    [ResourceEntry("WatchVideo", Description = "Dashboard Label: Watch Video", LastModified = "2011/01/14", Value = "Watch Video")]
    public string WatchVideo => this[nameof (WatchVideo)];

    /// <summary>Gets External Link: Dashboard Watch Video</summary>
    [ResourceEntry("ExternalLinkDashboardWatchVideo", Description = "Dashboard Label: Watch Video", LastModified = "2018/10/15", Value = "https://www.progress.com/video?product=sitefinity")]
    public string ExternalLinkDashboardWatchVideo => this[nameof (ExternalLinkDashboardWatchVideo)];

    /// <summary>Dashboard Label: Welcome to Sitefinity 13.3</summary>
    [ResourceEntry("WelcomeToSitefinity40", Description = "Dashboard Label: Welcome to Sitefinity 13.3", LastModified = "2020/08/24", Value = "Welcome to Sitefinity 13.3")]
    public string WelcomeToSitefinity40 => this[nameof (WelcomeToSitefinity40)];

    /// <summary>
    /// Dashboard general paragraph: The new release delivers a web content management system that empowers you to create websites, corporate intranets, community portals and blogs
    /// </summary>
    [ResourceEntry("DashboardGeneralParagraph", Description = "Dashboard general paragraph: The new release delivers a web content management system that empowers you to create websites, corporate intranets, community portals and blogs", LastModified = "2011/01/14", Value = "The new release delivers a web content management system that empowers you to create websites, corporate intranets, community portals and blogs")]
    public string DashboardGeneralParagraph => this[nameof (DashboardGeneralParagraph)];

    /// <summary>
    /// Dashboard list item 1: For general presentation of Sitefinity and its features, click on the overview video to the left
    /// </summary>
    [ResourceEntry("DashboardListItem1", Description = "Dashboard list item 1: For general presentation of Sitefinity and its features, click on the overview video to the left", LastModified = "2011/01/14", Value = "For general presentation of Sitefinity and its features, click on the overview video to the left")]
    public string DashboardListItem1 => this[nameof (DashboardListItem1)];

    /// <summary>
    /// Dashboard list item 2: To learn more details on specific parts of the product, explore the video tutorials below
    /// </summary>
    [ResourceEntry("DashboardListItem2", Description = "Dashboard list item 2: To learn more details on specific parts of the product, explore the video tutorials below", LastModified = "2011/01/14", Value = "To learn more details on specific parts of the product, explore the video tutorials below")]
    public string DashboardListItem2 => this[nameof (DashboardListItem2)];

    /// <summary>Dashboard Label: Getting started...</summary>
    [ResourceEntry("DashboardGettingStarted", Description = "Dashboard Label: Getting started", LastModified = "2016/02/15", Value = "Getting started")]
    public string DashboardGettingStarted => this[nameof (DashboardGettingStarted)];

    /// <summary>Dashboard Label: How to create a simple page?</summary>
    [ResourceEntry("DashboardHowToCreateASimplePage", Description = "Dashboard Label: How to create a simple page?", LastModified = "2011/01/14", Value = "How to create a simple page?")]
    public string DashboardHowToCreateASimplePage => this[nameof (DashboardHowToCreateASimplePage)];

    /// <summary>Gets External Link: How to create a simple page?</summary>
    [ResourceEntry("ExternalLinkDashboardHowToCreateASimplePage", Description = "External Link: How to create a simple page?", LastModified = "2018/10/23", Value = "https://www.progress.com/documentation/sitefinity-cms/create-a-page")]
    public string ExternalLinkDashboardHowToCreateASimplePage => this[nameof (ExternalLinkDashboardHowToCreateASimplePage)];

    /// <summary>Dashboard Label: How to create a simple blog?</summary>
    [ResourceEntry("DashboardHowToCreateASimpleBlog", Description = "Dashboard Label: How to create a simple blog?", LastModified = "2011/01/14", Value = "How to create a simple blog?")]
    public string DashboardHowToCreateASimpleBlog => this[nameof (DashboardHowToCreateASimpleBlog)];

    /// <summary>Gets External Link: How to create a simple blog?</summary>
    [ResourceEntry("ExternalLinkDashboardHowToCreateASimpleBlog", Description = "External Link: How to create a simple blog?", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/blog-posts-widget-mvc")]
    public string ExternalLinkDashboardHowToCreateASimpleBlog => this[nameof (ExternalLinkDashboardHowToCreateASimpleBlog)];

    /// <summary>Dashboard Label: How to set navigation?</summary>
    [ResourceEntry("DashboardHowToSetNavigation", Description = "Dashboard Label: How to set navigation?", LastModified = "2011/01/14", Value = "How to set navigation?")]
    public string DashboardHowToSetNavigation => this[nameof (DashboardHowToSetNavigation)];

    /// <summary>Gets External Link: How to set navigation?</summary>
    [ResourceEntry("ExternalLinkDashboardHowToSetNavigation", Description = "External Link: How to set navigation?", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/navigation-widget-mvc")]
    public string ExternalLinkDashboardHowToSetNavigation => this[nameof (ExternalLinkDashboardHowToSetNavigation)];

    /// <summary>Dashboard Label: Video tutorials about...</summary>
    [ResourceEntry("DashboardVideoTutorialsAbout", Description = "Dashboard Label: Video tutorials about...", LastModified = "2011/01/14", Value = "Video tutorials about...")]
    public string DashboardVideoTutorialsAbout => this[nameof (DashboardVideoTutorialsAbout)];

    /// <summary>Dashboard Label: Mobile web</summary>
    [ResourceEntry("MobileWeb", Description = "Dashboard Label: Mobile web", LastModified = "2012/02/24", Value = "Mobile web")]
    public string MobileWeb => this[nameof (MobileWeb)];

    /// <summary>Gets External Link: Mobile web</summary>
    [ResourceEntry("ExternalLinkMobileWeb", Description = "External Link: Mobile web", LastModified = "2018/10/22", Value = "https://www.progress.com/sitefinity-cms/platform/responsive-design")]
    public string ExternalLinkMobileWeb => this[nameof (ExternalLinkMobileWeb)];

    /// <summary>
    /// Dashboard Label: Learn how to use the responsive design techniques
    /// </summary>
    [ResourceEntry("MobileWebNote", Description = "Learn how to use the responsive design techniques", LastModified = "2012/02/24", Value = "Learn how to use the responsive design techniques")]
    public string MobileWebNote => this[nameof (MobileWebNote)];

    /// <summary>Dashboard Label: Template builder</summary>
    [ResourceEntry("TemplateBuilder", Description = "Dashboard Label: Template builder", LastModified = "2012/02/24", Value = "Template builder")]
    public string TemplateBuilder => this[nameof (TemplateBuilder)];

    /// <summary>Gets External Link: Template builder video</summary>
    [ResourceEntry("ExternalLinkTemplateBuilderVideo", Description = "External Link: Template builder video", LastModified = "2018/11/22", Value = "https://www.progress.com/documentation/sitefinity-cms/for-developers-use-custom-layout-templates")]
    public string ExternalLinkTemplateBuilderVideo => this[nameof (ExternalLinkTemplateBuilderVideo)];

    /// <summary>
    /// Dashboard Label: See how to easily build templates using the Template builder
    /// </summary>
    [ResourceEntry("TemplateBuilderNote", Description = "Dashboard Label: See how to easily build templates using the Template builder", LastModified = "2012/02/24", Value = "See how to easily build templates using the Template builder")]
    public string TemplateBuilderNote => this[nameof (TemplateBuilderNote)];

    /// <summary>Dashboard Label: Page layouts</summary>
    [ResourceEntry("DashboardPageLayouts", Description = "Dashboard Label: Page layouts", LastModified = "2011/01/14", Value = "Page layouts")]
    public string DashboardPageLayouts => this[nameof (DashboardPageLayouts)];

    /// <summary>Gets External Link: Page layouts</summary>
    [ResourceEntry("ExternalLinkDashboardPageLayouts", Description = "External Link: Page layouts", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/design-page-templates-in-the-layout-editor")]
    public string ExternalLinkDashboardPageLayouts => this[nameof (ExternalLinkDashboardPageLayouts)];

    /// <summary>
    /// Dashboard Label: Learn how to design and modify layouts of pages
    /// </summary>
    [ResourceEntry("DashboardLearnHowToDesignAndModifyLayoutsOfPages", Description = "Dashboard Label: Learn how to design and modify layouts of pages", LastModified = "2011/01/14", Value = "Learn how to design and modify layouts of pages")]
    public string DashboardLearnHowToDesignAndModifyLayoutsOfPages => this[nameof (DashboardLearnHowToDesignAndModifyLayoutsOfPages)];

    /// <summary>Dashboard Label: Forms</summary>
    [ResourceEntry("Forms", Description = "Dashboard Label: Forms", LastModified = "2011/01/14", Value = "Forms")]
    public string Forms => this[nameof (Forms)];

    /// <summary>Dashboard Label: Learn how to create and use forms</summary>
    [ResourceEntry("DashboardLearnHowToCreateAndUseForms", Description = "Dashboard Label: Learn how to create and use forms", LastModified = "2011/01/14", Value = "Learn how to create and use forms")]
    public string DashboardLearnHowToCreateAndUseForms => this[nameof (DashboardLearnHowToCreateAndUseForms)];

    /// <summary>
    /// Gets External Label: Learn how to create and use forms
    /// </summary>
    [ResourceEntry("ExternalLinkDashboardLearnHowToCreateAndUseForms", Description = "External Label: Learn how to create and use forms", LastModified = "2018/10/23", Value = "https://www.progress.com/documentation/sitefinity-cms/form-widget-mvc")]
    public string ExternalLinkDashboardLearnHowToCreateAndUseForms => this[nameof (ExternalLinkDashboardLearnHowToCreateAndUseForms)];

    /// <summary>Dashboard Label: Fluent API</summary>
    [ResourceEntry("DashboardFluentApi", Description = "Dashboard Label: Fluent API", LastModified = "2011/01/14", Value = "Fluent API")]
    public string DashboardFluentApi => this[nameof (DashboardFluentApi)];

    /// <summary>
    /// Dashboard Label: Discover how the Fluent API enables you to interact with the Sitefinity data
    /// </summary>
    [ResourceEntry("DashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData", Description = "Dashboard Label: Discover how the Fluent API enables you to interact with the Sitefinity data", LastModified = "2011/01/14", Value = "Discover how the Fluent API enables you to interact with the Sitefinity data")]
    public string DashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData => this[nameof (DashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData)];

    /// <summary>
    /// Gets External Link: Discover how the Fluent API enables you to interact with the Sitefinity data
    /// </summary>
    [ResourceEntry("ExternalLinkDashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData", Description = "External Link: Discover how the Fluent API enables you to interact with the Sitefinity data", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/for-developers-use-readable-code-with-the-fluent-api")]
    public string ExternalLinkDashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData => this[nameof (ExternalLinkDashboardDiscoverHowTheFluentApiEnablesYouToInteractWithTheSitefinityData)];

    /// <summary>Dashboard Label: Page setup with widgets</summary>
    [ResourceEntry("DashboardPageSetupWithWidgets", Description = "Dashboard Label: Page setup with widgets", LastModified = "2011/01/14", Value = "Page setup with widgets")]
    public string DashboardPageSetupWithWidgets => this[nameof (DashboardPageSetupWithWidgets)];

    /// <summary>Gets External Link: Page setup with widgets</summary>
    [ResourceEntry("ExternalLinkDashboardPageSetupWithWidgets", Description = "External Link: Page setup with widgets", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/add-widgets-on-pages-and-templates")]
    public string ExternalLinkDashboardPageSetupWithWidgets => this[nameof (ExternalLinkDashboardPageSetupWithWidgets)];

    /// <summary>
    /// Dashboard Label: Find out how to work with widgets to setup your pages
    /// </summary>
    [ResourceEntry("DashboardFindOutHowToWorkWithWidgetsToSetupYourPages", Description = "Dashboard Label: Find out how to work with widgets to setup your pages", LastModified = "2011/01/14", Value = "Find out how to work with widgets to setup your pages")]
    public string DashboardFindOutHowToWorkWithWidgetsToSetupYourPages => this[nameof (DashboardFindOutHowToWorkWithWidgetsToSetupYourPages)];

    /// <summary>Dashboard Label: Categories and tags</summary>
    [ResourceEntry("DashboardCategoriesAndTags", Description = "Dashboard Label: Categories and tags", LastModified = "2011/01/14", Value = "Categories and tags")]
    public string DashboardCategoriesAndTags => this[nameof (DashboardCategoriesAndTags)];

    /// <summary>Gets External Link: Categories and tags</summary>
    [ResourceEntry("ExternalLinkDashboardCategoriesAndTags", Description = "External Link: Categories and tags", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-classify-your-content-using-taxonomies")]
    public string ExternalLinkDashboardCategoriesAndTags => this[nameof (ExternalLinkDashboardCategoriesAndTags)];

    /// <summary>
    /// Dashboard Label: Learn how to organize the content in your website by using categories and tags
    /// </summary>
    [ResourceEntry("DashboardLearnHowToOrganizeTheContentInYourWebsiteByUsingCategoriesAndTags", Description = "Dashboard Label: Learn how to organize the content in your website by using categories and tags", LastModified = "2011/01/14", Value = "Learn how to organize the content in your website by using categories and tags")]
    public string DashboardLearnHowToOrganizeTheContentInYourWebsiteByUsingCategoriesAndTags => this[nameof (DashboardLearnHowToOrganizeTheContentInYourWebsiteByUsingCategoriesAndTags)];

    /// <summary>Dashboard Label: Web Analytics</summary>
    [ResourceEntry("DashboardWebAnalytics", Description = "Dashboard Label: Web Analytics", LastModified = "2011/01/14", Value = "Web Analytics")]
    public string DashboardWebAnalytics => this[nameof (DashboardWebAnalytics)];

    /// <summary>Gets External Link: Web Analytics</summary>
    [ResourceEntry("ExternalLinkDashboardWebAnalytics", Description = "External Link: Web Analytics", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-sitefinity-analytics")]
    public string ExternalLinkDashboardWebAnalytics => this[nameof (ExternalLinkDashboardWebAnalytics)];

    /// <summary>
    /// Dashboard Label: Learn how to measure your website performance using the web analytics feature
    /// </summary>
    [ResourceEntry("DashboardLearnHowToMeasureYourWebsitePerformanceUsingTheWebAnalyticsFeature", Description = "Dashboard Label: Learn how to measure your website performance using the web analytics feature", LastModified = "2011/01/14", Value = "Learn how to measure your website performance using the web analytics feature")]
    public string DashboardLearnHowToMeasureYourWebsitePerformanceUsingTheWebAnalyticsFeature => this[nameof (DashboardLearnHowToMeasureYourWebsitePerformanceUsingTheWebAnalyticsFeature)];

    /// <summary>Dashboard Label: Inline editing</summary>
    [ResourceEntry("DashboardInlineEditing", Description = "Dashboard Label: Inline editing", LastModified = "2011/01/14", Value = "Inline editing")]
    public string DashboardInlineEditing => this[nameof (DashboardInlineEditing)];

    /// <summary>Gets External Link: Inline editing</summary>
    [ResourceEntry("ExternalLinkDashboardInlineEditing", Description = "External Link: Inline editing", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/inline-editing")]
    public string ExternalLinkDashboardInlineEditing => this[nameof (ExternalLinkDashboardInlineEditing)];

    /// <summary>
    /// Dashboard Label: See how you can edit your content while browsing the live website
    /// </summary>
    [ResourceEntry("DashboardSeeHowYouCanEditYourContentWhileBrowsingTheLiveWebsite", Description = "Dashboard Label: See how you can edit your content while browsing the live website", LastModified = "2011/01/14", Value = "See how you can edit your content while browsing the live website")]
    public string DashboardSeeHowYouCanEditYourContentWhileBrowsingTheLiveWebsite => this[nameof (DashboardSeeHowYouCanEditYourContentWhileBrowsingTheLiveWebsite)];

    /// <summary>Dashboard Label: Revision history</summary>
    [ResourceEntry("DashboardRevisionHistory", Description = "Dashboard Label: Revision history", LastModified = "2011/01/14", Value = "Revision history")]
    public string DashboardRevisionHistory => this[nameof (DashboardRevisionHistory)];

    /// <summary>Label: Show the parent page in navigation</summary>
    [ResourceEntry("ShowParentPageInNavigation", Description = "Label: Show the parent page in navigation", LastModified = "2011/12/02", Value = "Show the parent page in navigation")]
    public string ShowParentPageInNavigation => this[nameof (ShowParentPageInNavigation)];

    /// <summary>Label: Show the selected page in navigation</summary>
    [ResourceEntry("ShowSelectedPageInNavigation", Description = "Label: Show the selected page in navigation", LastModified = "2011/12/09", Value = "Show the selected page in navigation")]
    public string ShowSelectedPageInNavigation => this[nameof (ShowSelectedPageInNavigation)];

    /// <summary>Label: Show the currently opened page in navigation</summary>
    [ResourceEntry("ShowCurrentPageInNavigation", Description = "Label: Show the currently opened page in navigation", LastModified = "2011/12/09", Value = "Show the currently opened page in navigation")]
    public string ShowCurrentPageInNavigation => this[nameof (ShowCurrentPageInNavigation)];

    /// <summary>Label: Group pages redirect to the first subpage</summary>
    [ResourceEntry("GroupPageNavigation", Description = "Label: Group pages redirect to the first subpage", LastModified = "2012/01/20", Value = "Group pages redirect to the first subpage")]
    public string GroupPageNavigation => this[nameof (GroupPageNavigation)];

    /// <summary>Label: Show my email to others</summary>
    [ResourceEntry("ShowEmailToOthers", Description = "Label: Show my email to others", LastModified = "2011/02/24", Value = "Show my email to others")]
    public string ShowEmailToOthers => this[nameof (ShowEmailToOthers)];

    /// <summary>
    /// Dashboard Label: Find out how to keep all the different versions of your content using the revision history
    /// </summary>
    [ResourceEntry("DashboardFindOutHowToKeepAllTheDifferentVersionsOfYourContentUsingTheRevisionHistory", Description = "Dashboard Label: Find out how to keep all the different versions of your content using the revision history", LastModified = "2011/01/14", Value = "Find out how to keep all the different versions of your content using the revision history")]
    public string DashboardFindOutHowToKeepAllTheDifferentVersionsOfYourContentUsingTheRevisionHistory => this[nameof (DashboardFindOutHowToKeepAllTheDifferentVersionsOfYourContentUsingTheRevisionHistory)];

    /// <summary>Dashboard Label: Documentation portal</summary>
    [ResourceEntry("DashboardFullDocumentation", Description = "Dashboard Label: Documentation portal", LastModified = "2012/02/23", Value = "Documentation portal")]
    public string DashboardFullDocumentation => this[nameof (DashboardFullDocumentation)];

    /// <summary>
    /// Label used in automatically created views for profile types.
    /// </summary>
    [ResourceEntry("ProfileFrontentCreate", Description = "Label used in automatically created views for profile types.", LastModified = "2011/02/25", Value = "<i>Create a profile</i> in the Frontend")]
    public string ProfileFrontentCreate => this[nameof (ProfileFrontentCreate)];

    /// <summary>
    /// Label used in automatically created views for profile types.
    /// </summary>
    [ResourceEntry("ProfileFrontentEdit", Description = "Label used in automatically created views for profile types.", LastModified = "2011/02/25", Value = "<i>Edit a profile</i> in the Frontend")]
    public string ProfileFrontentEdit => this[nameof (ProfileFrontentEdit)];

    /// <summary>
    /// Label used in automatically created views for profile types.
    /// </summary>
    [ResourceEntry("ProfileBackendCreate", Description = "Label used in automatically created views for profile types.", LastModified = "2011/02/25", Value = "<i>Create a profile</i> in the Backend")]
    public string ProfileBackendCreate => this[nameof (ProfileBackendCreate)];

    /// <summary>
    /// Label used in automatically created views for profile types.
    /// </summary>
    [ResourceEntry("ProfileBackendEdit", Description = "Label used in automatically created views for profile types.", LastModified = "2011/02/25", Value = "<i>Edit a profile</i> in the Backend")]
    public string ProfileBackendEdit => this[nameof (ProfileBackendEdit)];

    /// <summary>word: Select...</summary>
    [ResourceEntry("SelectImageDotDotDot", Description = "Select image...", LastModified = "2011/03/10", Value = "Select image...")]
    public string SelectImageDotDotDot => this[nameof (SelectImageDotDotDot)];

    /// <summary>word: Px...</summary>
    [ResourceEntry("Px", Description = "px.", LastModified = "2011/03/10", Value = "px")]
    public string Px => this[nameof (Px)];

    /// <summary>Word: Update</summary>
    [ResourceEntry("Update", Description = "Word: Update", LastModified = "2011/02/15", Value = "Update")]
    public string Update => this[nameof (Update)];

    /// <summary>Label: Back to edit page</summary>
    [ResourceEntry("BackToEditPage", Description = "Label: Back to edit page", LastModified = "2011/02/16", Value = "Back to edit page")]
    public string BackToEditPage => this[nameof (BackToEditPage)];

    /// <summary>Label: Don't display any single item page</summary>
    [ResourceEntry("DontDisplayAnySingleItemPage", Description = "Label: Don't display any single item page", LastModified = "2011/03/17", Value = "Don't display any single item page")]
    public string DontDisplayAnySingleItemPage => this[nameof (DontDisplayAnySingleItemPage)];

    /// <summary>Label: in the Backend</summary>
    [ResourceEntry("InTheBackend", Description = "Label: in the Backend", LastModified = "2011/03/28", Value = "in the Backend")]
    public string InTheBackend => this[nameof (InTheBackend)];

    /// <summary>Label: in the Public site</summary>
    [ResourceEntry("InTheFrontend", Description = "Label: in the Public site", LastModified = "2011/03/28", Value = "in the Public site")]
    public string InTheFrontend => this[nameof (InTheFrontend)];

    /// <summary>Label: Name</summary>
    [ResourceEntry("DisplayName", Description = "Label: Name", LastModified = "2011/03/22", Value = "Name")]
    public string DisplayName => this[nameof (DisplayName)];

    /// <summary>Label: (only list without links)</summary>
    [ResourceEntry("OnlyListWithoutLinks", Description = "Label: (only list without links)", LastModified = "2011/03/17", Value = "(only list without links)")]
    public string OnlyListWithoutLinks => this[nameof (OnlyListWithoutLinks)];

    /// <summary>word: Details</summary>
    [ResourceEntry("Details", Description = "word: Details", LastModified = "2011/03/28", Value = "Details")]
    public string Details => this[nameof (Details)];

    /// <summary>phrase: What's this?</summary>
    [ResourceEntry("WhatsThis", Description = "phrase: What's this?", LastModified = "2011/11/29", Value = "What's this?")]
    public string WhatsThis => this[nameof (WhatsThis)];

    /// <summary>word: Yes</summary>
    [ResourceEntry("Yes", Description = "word: Yes", LastModified = "2011/03/29", Value = "Yes")]
    public string Yes => this[nameof (Yes)];

    /// <summary>word: No</summary>
    [ResourceEntry("No", Description = "word: No", LastModified = "2011/03/29", Value = "No")]
    public string No => this[nameof (No)];

    /// <summary>phrase: Selected caching</summary>
    [ResourceEntry("SelectedCaching", Description = "phrase: Selected caching", LastModified = "2011/03/31", Value = "Selected caching")]
    public string SelectedCaching => this[nameof (SelectedCaching)];

    /// <summary>phrase: Enable caching</summary>
    [ResourceEntry("EnableCaching", Description = "phrase: Enable caching", LastModified = "2011/03/31", Value = "Enable caching")]
    public string EnableCaching => this[nameof (EnableCaching)];

    /// <summary>word: Duration</summary>
    [ResourceEntry("Duration", Description = "word: Duration", LastModified = "2011/03/31", Value = "Duration")]
    public string Duration => this[nameof (Duration)];

    /// <summary>phrase: Sliding expiration</summary>
    [ResourceEntry("SlidingExpiration", Description = "phrase: Sliding expiration", LastModified = "2011/03/31", Value = "Sliding expiration")]
    public string SlidingExpiration => this[nameof (SlidingExpiration)];

    /// <summary>phrase: No sliding expiration</summary>
    [ResourceEntry("NoSlidingExpiration", Description = "phrase: No sliding expiration", LastModified = "2011/03/31", Value = "No sliding expiration")]
    public string NoSlidingExpiration => this[nameof (NoSlidingExpiration)];

    /// <summary>phrase: The same as for the whole site</summary>
    [ResourceEntry("SameAsForWholeSite", Description = "phrase: The same as for the whole site", LastModified = "2011/03/31", Value = "The same as for the whole site")]
    public string SameAsForWholeSite => this[nameof (SameAsForWholeSite)];

    /// <summary>phrase: no cache</summary>
    [ResourceEntry("NoCache", Description = "phrase: no cache", LastModified = "2011/11/25", Value = "no cache")]
    public string NoCache => this[nameof (NoCache)];

    /// <summary>phrase: no explicit client caching</summary>
    [ResourceEntry("NoExplicitClientCaching", Description = "phrase: no explicit client caching", LastModified = "2011/11/25", Value = "no explicit client caching")]
    public string NoExplicitClientCaching => this[nameof (NoExplicitClientCaching)];

    /// <summary>phrase: No explicit client caching.</summary>
    [ResourceEntry("NoExplicitClientCachingMessage", Description = "phrase: No explicit client caching.", LastModified = "2011/11/25", Value = "No explicit client caching.")]
    public string NoExplicitClientCachingMessage => this[nameof (NoExplicitClientCachingMessage)];

    /// <summary>phrase: HTTP Header</summary>
    [ResourceEntry("HttpHeader", Description = "phrase: HTTP Header", LastModified = "2011/11/25", Value = "HTTP Header")]
    public string HttpHeader => this[nameof (HttpHeader)];

    /// <summary>phrase: These values can be changed in</summary>
    [ResourceEntry("CacheSettingsChanges", Description = "phrase: These values can be changed in", LastModified = "2011/04/01", Value = "These values can be changed in")]
    public string CacheSettingsChanges => this[nameof (CacheSettingsChanges)];

    /// <summary>
    /// phrase: Administration &gt; Settings &gt; Advanced settings &gt; System &gt; Output Cache Settings
    /// </summary>
    [ResourceEntry("CacheSettingsLocation", Description = "phrase: Administration > Basic Settings > Cache profiles", LastModified = "2011/04/01", Value = "Administration > Basic Settings > Cache profiles")]
    public string CacheSettingsLocation => this[nameof (CacheSettingsLocation)];

    /// <summary>
    /// Canonical Url is the normalized form of all urls that point to the same (similar) content. As a SEO feature it helps crawlers to preserve the page rank on your default location. When enabled, it helps search engines and browsers. You may set this for the whole site or to select it per page.
    /// </summary>
    [ResourceEntry("CanonicalUrlsDescription", Description = "Canonical Url is the normalized form of all urls that point to the same (similar) content. As a SEO feature it helps crawlers to preserve the page rank on your default location. When enabled, it helps search engines and browsers. You may set this for the whole site or to select it per page.", LastModified = "2013/05/17", Value = "Canonical Url is the normalized form of all urls that point to the same (similar) content. As a SEO feature it helps crawlers to preserve the page rank on your default location. When enabled, it helps search engines and browsers. You may set this for the whole site or to select it per page.")]
    public string CanonicalUrlsDescription => this[nameof (CanonicalUrlsDescription)];

    /// <summary>
    /// phrase: To change the setting for the whole site go to:
    /// </summary>
    [ResourceEntry("CanonicalUrlsSettingsChanges", Description = "phrase: To change the setting for the whole site go to: ", LastModified = "2013/05/17", Value = "To change the setting for the whole site go to: ")]
    public string CanonicalUrlsSettingsChanges => this[nameof (CanonicalUrlsSettingsChanges)];

    /// <summary>
    /// phrase: Administration &gt;Settings &gt;Advanced settings &gt;System &gt;Content Locations &gt;Static Pages Canonical Url
    /// </summary>
    [ResourceEntry("CanonicalUrlsSettingsLocation", Description = "phrase: Administration > Settings > Advanced settings > System > Content Locations > Static Pages Canonical Url", LastModified = "2013/05/17", Value = "Administration > Settings > Advanced settings > System > Content Locations >Static Pages Canonical Url")]
    public string CanonicalUrlsSettingsLocation => this[nameof (CanonicalUrlsSettingsLocation)];

    /// <summary>word: d</summary>
    [ResourceEntry("DaysAbbreviated", Description = "word: d", LastModified = "2011/04/01", Value = "d")]
    public string DaysAbbreviated => this[nameof (DaysAbbreviated)];

    /// <summary>word: hr</summary>
    [ResourceEntry("HoursAbbreviated", Description = "word: hr", LastModified = "2011/04/01", Value = "hr")]
    public string HoursAbbreviated => this[nameof (HoursAbbreviated)];

    /// <summary>word: min</summary>
    [ResourceEntry("MinutesAbbreviated", Description = "word: min", LastModified = "2011/04/01", Value = "min")]
    public string MinutesAbbreviated => this[nameof (MinutesAbbreviated)];

    /// <summary>word: sec</summary>
    [ResourceEntry("SecondsAbbreviated", Description = "word: sec", LastModified = "2011/04/01", Value = "sec")]
    public string SecondsAbbreviated => this[nameof (SecondsAbbreviated)];

    /// <summary>phrase: All published list items</summary>
    [ResourceEntry("AllPublishedListItems", Description = "phrase: All published list items", LastModified = "2011/09/05", Value = "All published list items")]
    public string AllPublishedListItems => this[nameof (AllPublishedListItems)];

    /// <summary>phrase: Which documents and files to display?</summary>
    [ResourceEntry("WhichDocumentsToDisplay", Description = "phrase: Which documents and files to display?", LastModified = "2011/09/05", Value = " Which documents and files to display")]
    public string WhichDocumentsToDisplay => this[nameof (WhichDocumentsToDisplay)];

    /// <summary>phrase: Language settings",</summary>
    [ResourceEntry("LanguageSettings", Description = "phrase: Language settings", LastModified = "2011/04/27", Value = "Language settings")]
    public string LanguageSettings => this[nameof (LanguageSettings)];

    /// <summary>phrase: All translations</summary>
    [ResourceEntry("AllTranslations", Description = "phrase: All translations", LastModified = "2011/04/27", Value = "All translations")]
    public string AllTranslations => this[nameof (AllTranslations)];

    /// <summary>phrase: Current translation only</summary>
    [ResourceEntry("CurrentTranslation", Description = "phrase: Current translation only", LastModified = "2011/04/27", Value = "Current translation only")]
    public string CurrentTranslation => this[nameof (CurrentTranslation)];

    /// <summary>phrase: Save to all translations</summary>
    [ResourceEntry("SaveAllTranslations", Description = "phrase: Save to all translations", LastModified = "2011/05/19", Value = "Save to all translations")]
    public string SaveAllTranslations => this[nameof (SaveAllTranslations)];

    /// <summary>phrase: Save to all translations</summary>
    [ResourceEntry("SaveAllTranslationsNewEditor", Description = "phrase: Save to all translations", LastModified = "2021/02/08", Value = "Save All Translations")]
    public string SaveAllTranslationsNewEditor => this[nameof (SaveAllTranslationsNewEditor)];

    /// <summary>phrase: Current translation only</summary>
    [ResourceEntry("WidgetSettings", Description = "phrase: Widget settings are applied to...", LastModified = "2011/05/04", Value = "Widget settings are applied to...")]
    public string WidgetSettings => this[nameof (WidgetSettings)];

    /// <summary>word: Image</summary>
    [ResourceEntry("Image", Description = "word: Image", LastModified = "2011/05/24", Value = "Image")]
    public string Image => this[nameof (Image)];

    /// <summary>phrase: Select Page</summary>
    [ResourceEntry("SelectPageButton", Description = "phrase: Select Page", LastModified = "2011/06/17", Value = "Select Page")]
    public string SelectPageButton => this[nameof (SelectPageButton)];

    /// <summary>phrase: Change Page</summary>
    [ResourceEntry("ChangePageButton", Description = "phrase: Change Page", LastModified = "2011/06/17", Value = "Change Page")]
    public string ChangePageButton => this[nameof (ChangePageButton)];

    /// <summary>phrase: Change Selection</summary>
    [ResourceEntry("ChangeSelection", Description = "phrase: Change selection", LastModified = "2012/12/03", Value = "Change selection")]
    public string ChangeSelection => this[nameof (ChangeSelection)];

    /// <summary>phrase: As set in Advanced mode</summary>
    [ResourceEntry("AsSetInAdvancedMode", Description = "phrase: As set in Advanced mode", LastModified = "2011/05/06", Value = "As set in Advanced mode")]
    public string AsSetInAdvancedMode => this[nameof (AsSetInAdvancedMode)];

    /// <summary>Edit separator in action menus.</summary>
    [ResourceEntry("EditSeparator", Description = "Edit separator in action menus", LastModified = "2011/07/23", Value = "Edit...")]
    public string EditSeparator => this[nameof (EditSeparator)];

    /// <summary>phrase: The URL you entered is invalid.</summary>
    [ResourceEntry("InvalidURL", Description = "phrase: The URL you entered is invalid.", LastModified = "2011/10/28", Value = "The URL you entered is invalid.")]
    public string InvalidURL => this[nameof (InvalidURL)];

    /// <summary>Phrase: My content</summary>
    [ResourceEntry("MyItems", Description = "phrase: My {0}", LastModified = "2018/02/13", Value = "My {0}")]
    public string MyItems => this[nameof (MyItems)];

    /// <summary>Show item count</summary>
    [ResourceEntry("ShowItemCount", Description = "Show item count", LastModified = "2011/09/08", Value = "Show item count")]
    public string ShowItemCount => this[nameof (ShowItemCount)];

    /// <summary>Show empty items</summary>
    [ResourceEntry("ShowEmptyItems", Description = "Show empty items", LastModified = "2011/09/08", Value = "Show empty items")]
    public string ShowEmptyItems => this[nameof (ShowEmptyItems)];

    /// <summary>SortOrder</summary>
    [ResourceEntry("SortOrder", Description = "SortOrder", LastModified = "2011/10/28", Value = "Sort items")]
    public string SortOrder => this[nameof (SortOrder)];

    /// <summary>SortOrder Alphabetical</summary>
    [ResourceEntry("SortOrderAlphabetical", Description = "SortOrderAlphabetical", LastModified = "2011/10/28", Value = "Alphabetically (A-Z)")]
    public string SortOrderAlphabetical => this[nameof (SortOrderAlphabetical)];

    /// <summary>SortOrder User Defined</summary>
    [ResourceEntry("SortOrderUserDefined", Description = "User Defined", LastModified = "2011/10/28", Value = "As they are ordered in departments")]
    public string SortOrderUserDefined => this[nameof (SortOrderUserDefined)];

    /// <summary>SortOrder Reverse Alphabetical</summary>
    [ResourceEntry("SortOrderReverseAlphabetical", Description = "Reverse Alphabetical", LastModified = "2011/10/28", Value = "Alphabetically (Z-A)")]
    public string SortOrderReverseAlphabetical => this[nameof (SortOrderReverseAlphabetical)];

    /// <summary>Which items to display?</summary>
    [ResourceEntry("WhichItemsToDisplay", Description = "Which items to display?", LastModified = "2011/10/28", Value = "<strong>Which items to display?</strong>")]
    public string WhichItemsToDisplay => this[nameof (WhichItemsToDisplay)];

    /// <summary>Items from top level only</summary>
    [ResourceEntry("ItemsFromTopLevelOnly", Description = "Items from top level only", LastModified = "2011/10/28", Value = "Items from top level only")]
    public string ItemsFromTopLevelOnly => this[nameof (ItemsFromTopLevelOnly)];

    /// <summary>HierarchicalListExpansion</summary>
    [ResourceEntry("HierarchicalListExpansion", Description = "HierarchicalListExpansion", LastModified = "2011/10/28", Value = "Expand / collapse")]
    public string HierarchicalListExpansion => this[nameof (HierarchicalListExpansion)];

    /// <summary>HierarchicalListExpandAll</summary>
    [ResourceEntry("HierarchicalListExpandAll", Description = "HierarchicalListExpandAll", LastModified = "2011/09/21", Value = "All items are <strong>expanded initially</strong>")]
    public string HierarchicalListExpandAll => this[nameof (HierarchicalListExpandAll)];

    /// <summary>HierarchicalListCollapseAll</summary>
    [ResourceEntry("HierarchicalListCollapseAll", Description = "HierarchicalListCollapseAll", LastModified = "2011/09/21", Value = "All items are <strong>collapsed initially</strong>")]
    public string HierarchicalListCollapseAll => this[nameof (HierarchicalListCollapseAll)];

    /// <summary>HierarchicalListExpandCurrent</summary>
    [ResourceEntry("HierarchicalListExpandCurrent", Description = "HierarchicalListExpandCurrent", LastModified = "2011/09/21", Value = "Only <strong>current level is expanded initially</strong>")]
    public string HierarchicalListExpandCurrent => this[nameof (HierarchicalListExpandCurrent)];

    /// <summary>
    /// Prompt message when opening a content item for edit and no actions are possible
    /// </summary>
    /// <value>You are not allowed to edit this item</value>
    [ResourceEntry("PromptMessageCannotEdit", Description = "Prompt message when opening a content item for edit and no actions are possible", LastModified = "2011/09/07", Value = "You are not allowed to edit this item")]
    public string PromptMessageCannotEdit => this[nameof (PromptMessageCannotEdit)];

    /// <summary>
    /// Explaination of prompt message when opening a content item for edit and no actions are possible
    /// </summary>
    /// <value>Ask your administrator for details, if you think there is something wrong</value>
    [ResourceEntry("PromptMessageCannotEditExplaination", Description = "Explaination of prompt message when opening a content item for edit and no actions are possible", LastModified = "2013/12/10", Value = "Ask your administrator for details, if you think there is something wrong")]
    public string PromptMessageCannotEditExplaination => this[nameof (PromptMessageCannotEditExplaination)];

    /// <summary>
    /// Description how to add more backend languages in basic settings &gt; languages
    /// </summary>
    /// <value>To add a new translation, go to Administration &gt; Interface labels and messages and import a language pack. You can download a language pack from Sitefinity Marketplace or you can create your own.</value>
    [ResourceEntry("ManageBackendLanguagesDescription", Description = "Description how to add more backend languages in basic settings > languages", LastModified = "2012/01/05", Value = "To add a new translation, go to Administration > Interface labels and messages and import a language pack. You can download a language pack from Sitefinity Marketplace or you can create your own.")]
    public string ManageBackendLanguagesDescription => this[nameof (ManageBackendLanguagesDescription)];

    /// <summary>
    /// Manage backend languages title in basic settings &gt; languages
    /// </summary>
    /// <value>Manage backend languages</value>
    [ResourceEntry("ManageBackendLanguagesTitle", Description = "Manage backend languages title in basic settings > languages", LastModified = "2011/09/14", Value = "Manage backend languages")]
    public string ManageBackendLanguagesTitle => this[nameof (ManageBackendLanguagesTitle)];

    /// <summary>Label: File Size</summary>
    [ResourceEntry("FileSize", Description = "Label: File Size", LastModified = "2011/09/28", Value = "File Size")]
    public string FileSize => this[nameof (FileSize)];

    /// <summary>Label: Image Size (optional)</summary>
    [ResourceEntry("ImageSize", Description = "Label: Image size", LastModified = "2013/10/09", Value = "Image size")]
    public string ImageSize => this[nameof (ImageSize)];

    /// <summary>Label: Change Image</summary>
    [ResourceEntry("ChangeImage", Description = "Label: Change Image", LastModified = "2011/09/28", Value = "Change Image")]
    public string ChangeImage => this[nameof (ChangeImage)];

    /// <summary>Label: Crop/Resize/Rotate...</summary>
    [ResourceEntry("CropResizeRotateDotDotDot", Description = "Label: Crop/Resize/Rotate...", LastModified = "2011/09/28", Value = "Crop/Resize/Rotate...")]
    public string CropResizeRotateDotDotDot => this[nameof (CropResizeRotateDotDotDot)];

    /// <summary>Word: Alignment</summary>
    [ResourceEntry("Alignment", Description = " Word: Alignment", LastModified = "2011/09/28", Value = "Alignment")]
    public string Alignment => this[nameof (Alignment)];

    /// <summary>Word: Left</summary>
    [ResourceEntry("Left", Description = " Word: Left", LastModified = "2011/09/28", Value = "Left")]
    public string Left => this[nameof (Left)];

    /// <summary>Word: Center</summary>
    [ResourceEntry("Center", Description = " Word: Center", LastModified = "2011/09/28", Value = "Center")]
    public string Center => this[nameof (Center)];

    /// <summary>Word: Center</summary>
    [ResourceEntry("Right", Description = " Word: Right", LastModified = "2011/09/28", Value = "Right")]
    public string Right => this[nameof (Right)];

    /// <summary>
    /// phrase: Some <strong>required properties</strong> are not set
    /// </summary>
    [ResourceEntry("SomeRequiredPropertiesAreNotSet", Description = "phrase: Some <strong>required properties</strong> are not set", LastModified = "2011/10/18", Value = "Some <strong>required properties</strong> are not set")]
    public string SomeRequiredPropertiesAreNotSet => this[nameof (SomeRequiredPropertiesAreNotSet)];

    /// <summary>Phrase: Site Sync</summary>
    [ResourceEntry("SynchronizationSettings", Description = "The 'Site Sync' title in basic settings view of 'Site Sync'", LastModified = "2016/09/13", Value = "Site Sync")]
    public string SynchronizationSettings => this[nameof (SynchronizationSettings)];

    /// <summary>Word: Save".</summary>
    [ResourceEntry("SaveAs", Description = "word", LastModified = "2011/10/19", Value = "Save as")]
    public string SaveAs => this[nameof (SaveAs)];

    /// <summary>word: Twitter widget</summary>
    [ResourceEntry("TwitterWidget", Description = "word: Twitter widget", LastModified = "2013/04/03", Value = "Twitter widget")]
    public string TwitterWidget => this[nameof (TwitterWidget)];

    /// <summary>word: Twitter feed</summary>
    [ResourceEntry("TwitterFeed", Description = "word: Twitter activity feed", LastModified = "2011/11/07", Value = "Twitter feed")]
    public string TwitterFeed => this[nameof (TwitterFeed)];

    /// <summary>word: Content tab</summary>
    [ResourceEntry("TwitterFeedContent", Description = "word: Content tab", LastModified = "2011/11/11", Value = "Content")]
    public string TwitterFeedContent => this[nameof (TwitterFeedContent)];

    /// <summary>word: Appearance tab</summary>
    [ResourceEntry("TwitterFeedAppearance", Description = "word: Appearance tab", LastModified = "2011/11/11", Value = "Appearance")]
    public string TwitterFeedAppearance => this[nameof (TwitterFeedAppearance)];

    /// <summary>word: Display tweets from a...</summary>
    [ResourceEntry("DisplayTweetsLabel", Description = "word: Display tweets from a...", LastModified = "2011/11/11", Value = "Display tweets from a...")]
    public string DisplayTweetsLabel => this[nameof (DisplayTweetsLabel)];

    /// <summary>word: Profile widget type label</summary>
    [ResourceEntry("ProfileWidgetTypeLabel", Description = "word: Profile widget type label", LastModified = "2011/11/11", Value = "Profile")]
    public string ProfileWidgetTypeLabel => this[nameof (ProfileWidgetTypeLabel)];

    /// <summary>word: Search widget type label</summary>
    [ResourceEntry("SearchWidgetTypeLabel", Description = "word: Search widget type label", LastModified = "2011/11/11", Value = "Search")]
    public string SearchWidgetTypeLabel => this[nameof (SearchWidgetTypeLabel)];

    /// <summary>word: Favorites widget type label</summary>
    [ResourceEntry("FavoritesWidgetTypeLabel", Description = "word: Favorites widget type label", LastModified = "2011/11/11", Value = "Favorites")]
    public string FavoritesWidgetTypeLabel => this[nameof (FavoritesWidgetTypeLabel)];

    /// <summary>word: List widget type label</summary>
    [ResourceEntry("ListWidgetTypeLabel", Description = "word: List widget type label", LastModified = "2011/11/11", Value = "List")]
    public string ListWidgetTypeLabel => this[nameof (ListWidgetTypeLabel)];

    /// <summary>word: Username of the twitter account</summary>
    [ResourceEntry("TwitterWidgetUsername", Description = "word: Username of the twitter account", LastModified = "2011/11/11", Value = "Username")]
    public string TwitterWidgetUsername => this[nameof (TwitterWidgetUsername)];

    /// <summary>
    /// word: Additionall info for the Twitter widget's Username property
    /// </summary>
    [ResourceEntry("TwitterWidgetUsernameExample", Description = "word: Additionall info for the Twitter widget's Username property", LastModified = "2011/11/11", Value = "Only tweets of this user will be displayed")]
    public string TwitterWidgetUsernameExample => this[nameof (TwitterWidgetUsernameExample)];

    /// <summary>word: Widget title</summary>
    [ResourceEntry("TwitterWidgetTitle", Description = "word: Widget title", LastModified = "2011/11/11", Value = "Title")]
    public string TwitterWidgetTitle => this[nameof (TwitterWidgetTitle)];

    /// <summary>word: Twitter widget subtitle label</summary>
    [ResourceEntry("TwitterWidgetSubtitle", Description = "word: Twitter widget subtitle", LastModified = "2011/11/11", Value = "Subtitle")]
    public string TwitterWidgetSubtitle => this[nameof (TwitterWidgetSubtitle)];

    /// <summary>word: Twitter widget search label</summary>
    [ResourceEntry("TwitterWidgetSearch", Description = "word: Twitter widget search label", LastModified = "2011/11/11", Value = "Search")]
    public string TwitterWidgetSearch => this[nameof (TwitterWidgetSearch)];

    /// <summary>
    /// phrase: Tweets matching searching words or a hash tag will be displayed
    /// </summary>
    [ResourceEntry("TwitterWidgetSearchExample", Description = "phrase: Tweets matching searching words or a hash tag will be displayed", LastModified = "2012/01/05", Value = "Tweets matching searching words or a hash tag will be displayed")]
    public string TwitterWidgetSearchExample => this[nameof (TwitterWidgetSearchExample)];

    /// <summary>word: Twitter widget timing label</summary>
    [ResourceEntry("TwitterWidgetTimingLabel", Description = "word: Twitter widget timing label", LastModified = "2011/11/11", Value = "Timing")]
    public string TwitterWidgetTimingLabel => this[nameof (TwitterWidgetTimingLabel)];

    /// <summary>word: Load new tweets automatically</summary>
    [ResourceEntry("TwitterWidgetLoadAuto", Description = "word: Load new tweets automatically", LastModified = "2011/11/11", Value = "Load new tweets automatically")]
    public string TwitterWidgetLoadAuto => this[nameof (TwitterWidgetLoadAuto)];

    /// <summary>word: Check for new tweets every...</summary>
    [ResourceEntry("TwitterWidgetLoadEvery", Description = "word: Check for new tweets every...", LastModified = "2011/11/11", Value = "Check for new tweets every...")]
    public string TwitterWidgetLoadEvery => this[nameof (TwitterWidgetLoadEvery)];

    /// <summary>word: Check for new tweets every 5 minutes</summary>
    [ResourceEntry("TwitterWidgetLoadEveryFiveChoiceText", Description = "word: Check for new tweets every 5 minutes", LastModified = "2011/11/11", Value = "5 minutes")]
    public string TwitterWidgetLoadEveryFiveChoiceText => this[nameof (TwitterWidgetLoadEveryFiveChoiceText)];

    /// <summary>word: Check for new tweets every 10 minutes</summary>
    [ResourceEntry("TwitterWidgetLoadEveryTenChoiceText", Description = "word: Check for new tweets every 10 minutes", LastModified = "2011/11/11", Value = "10 minutes")]
    public string TwitterWidgetLoadEveryTenChoiceText => this[nameof (TwitterWidgetLoadEveryTenChoiceText)];

    /// <summary>word: Check for new tweets every 30 minutes</summary>
    [ResourceEntry("TwitterWidgetLoadEveryThirtyChoiceText", Description = "word: Check for new tweets every 30 minutes", LastModified = "2011/11/11", Value = "30 minutes")]
    public string TwitterWidgetLoadEveryThirtyChoiceText => this[nameof (TwitterWidgetLoadEveryThirtyChoiceText)];

    /// <summary>word: Check for new tweets every 1 hour</summary>
    [ResourceEntry("TwitterWidgetLoadEveryOneHourChoiceText", Description = "word: Check for new tweets every 1 hour", LastModified = "2011/11/11", Value = "1 hour")]
    public string TwitterWidgetLoadEveryOneHourChoiceText => this[nameof (TwitterWidgetLoadEveryOneHourChoiceText)];

    /// <summary>word: Number of displayed tweets</summary>
    [ResourceEntry("TwitterWidgetNumberOfTweets", Description = "word: Number of displayed tweets", LastModified = "2011/11/11", Value = "Number of displayed tweets")]
    public string TwitterWidgetNumberOfTweets => this[nameof (TwitterWidgetNumberOfTweets)];

    /// <summary>word: Include...</summary>
    [ResourceEntry("TwitterWidgetInclude", Description = "word: Include...", LastModified = "2011/11/11", Value = "Include...")]
    public string TwitterWidgetInclude => this[nameof (TwitterWidgetInclude)];

    /// <summary>word: Timestamp</summary>
    [ResourceEntry("TwitterWidgetTimestampLabel", Description = "word: Timestamp", LastModified = "2011/11/11", Value = "Timestamp")]
    public string TwitterWidgetTimestampLabel => this[nameof (TwitterWidgetTimestampLabel)];

    /// <summary>word: Avatars</summary>
    [ResourceEntry("TwitterWidgetAvatarsLabel", Description = "word: Avatars", LastModified = "2011/11/11", Value = "Avatars")]
    public string TwitterWidgetAvatarsLabel => this[nameof (TwitterWidgetAvatarsLabel)];

    /// <summary>phrase: Hash tags</summary>
    [ResourceEntry("TwitterWidgetHashtagsLabel", Description = "phrase: Hash tags", LastModified = "2012/01/05", Value = "Hash tags")]
    public string TwitterWidgetHashtagsLabel => this[nameof (TwitterWidgetHashtagsLabel)];

    /// <summary>word: Size label</summary>
    [ResourceEntry("TwitterWidgetSizeLabel", Description = "word: Size label", LastModified = "2011/11/14", Value = "Size")]
    public string TwitterWidgetSizeLabel => this[nameof (TwitterWidgetSizeLabel)];

    /// <summary>word: Color scheme label</summary>
    [ResourceEntry("TwitterWidgetColorSchemeLabel", Description = "word: Color scheme label", LastModified = "2011/11/14", Value = "Color scheme")]
    public string TwitterWidgetColorSchemeLabel => this[nameof (TwitterWidgetColorSchemeLabel)];

    /// <summary>word: Auto size</summary>
    [ResourceEntry("TwitterWidgetAutoSize", Description = "word: Auto size", LastModified = "2011/11/14", Value = "Auto")]
    public string TwitterWidgetAutoSize => this[nameof (TwitterWidgetAutoSize)];

    /// <summary>word: Fixed size</summary>
    [ResourceEntry("TwitterWidgetFixedSize", Description = "word: Fixed size", LastModified = "2011/11/14", Value = "Fixed...")]
    public string TwitterWidgetFixedSize => this[nameof (TwitterWidgetFixedSize)];

    /// <summary>word: Default color scheme</summary>
    [ResourceEntry("TwitterWidgetDefaultColorScheme", Description = "word: Default color scheme", LastModified = "2011/11/14", Value = "Default")]
    public string TwitterWidgetDefaultColorScheme => this[nameof (TwitterWidgetDefaultColorScheme)];

    /// <summary>word: Custom color scheme</summary>
    [ResourceEntry("TwitterWidgetCustomColorScheme", Description = "word: Custom color scheme", LastModified = "2011/11/14", Value = "Custom")]
    public string TwitterWidgetCustomColorScheme => this[nameof (TwitterWidgetCustomColorScheme)];

    /// <summary>word: Width</summary>
    [ResourceEntry("TwitterWidgetWidth", Description = "word: Width", LastModified = "2011/11/14", Value = "Width")]
    public string TwitterWidgetWidth => this[nameof (TwitterWidgetWidth)];

    /// <summary>word: Pixels</summary>
    [ResourceEntry("TwitterWidgetPixelsLabel", Description = "word: Pixels", LastModified = "2011/11/14", Value = "px")]
    public string TwitterWidgetPixelsLabel => this[nameof (TwitterWidgetPixelsLabel)];

    /// <summary>word: Width</summary>
    [ResourceEntry("TwitterWidgetHeight", Description = "word: Height", LastModified = "2011/11/14", Value = "Height")]
    public string TwitterWidgetHeight => this[nameof (TwitterWidgetHeight)];

    /// <summary>word: Shell background</summary>
    [ResourceEntry("TwitterWidgetShellBackground", Description = "word: Shell background", LastModified = "2011/11/14", Value = "Shell background")]
    public string TwitterWidgetShellBackground => this[nameof (TwitterWidgetShellBackground)];

    /// <summary>word: Shell text color</summary>
    [ResourceEntry("TwitterWidgetShellTextColor", Description = "word: Shell text color", LastModified = "2011/11/14", Value = "Shell text color")]
    public string TwitterWidgetShellTextColor => this[nameof (TwitterWidgetShellTextColor)];

    /// <summary>word: Tweet background</summary>
    [ResourceEntry("TwitterWidgetTweetBackground", Description = "word: Tweet background", LastModified = "2011/11/14", Value = "Tweet background")]
    public string TwitterWidgetTweetBackground => this[nameof (TwitterWidgetTweetBackground)];

    /// <summary>word: Tweet text color</summary>
    [ResourceEntry("TwitterWidgetTweetTextColor", Description = "word: Tweet background", LastModified = "2011/11/14", Value = "Tweet text color")]
    public string TwitterWidgetTweetTextColor => this[nameof (TwitterWidgetTweetTextColor)];

    /// <summary>word: Links</summary>
    [ResourceEntry("TwitterWidgetLinks", Description = "word: Links", LastModified = "2011/11/14", Value = "Links")]
    public string TwitterWidgetLinks => this[nameof (TwitterWidgetLinks)];

    /// <summary>word: List of</summary>
    [ResourceEntry("TwitterWidgetListOfLabel", Description = "word: List of", LastModified = "2011/11/16", Value = "List of ")]
    public string TwitterWidgetListOfLabel => this[nameof (TwitterWidgetListOfLabel)];

    /// <summary>word: Include scrollbar</summary>
    [ResourceEntry("TwitterWidgetIncludeScrollBar", Description = "word: Include scrollbar", LastModified = "2011/11/16", Value = "Include scrollbar")]
    public string TwitterWidgetIncludeScrollBar => this[nameof (TwitterWidgetIncludeScrollBar)];

    /// <summary>
    /// phrase: If you add a Twitter feed here it will not be displayed before the page is published.To see the widget click Preview on the top of this page.
    /// </summary>
    [ResourceEntry("TwitterWidgetNotAvailableInEditMode", Description = "phrase: The Twitter widget could not be displayed here. To see it click the Preview button on top of this page or publish the page.", LastModified = "2011/11/29", Value = "The Twitter widget could not be displayed here. To see it click the Preview button on top of this page or publish the page.")]
    public string TwitterWidgetNotAvailableInEditMode => this[nameof (TwitterWidgetNotAvailableInEditMode)];

    /// <summary>Phrase: Logged in at the moment are:</summary>
    [ResourceEntry("LoggedInAtTheMoment", Description = "Phrase: Logged in at the moment are:", LastModified = "2011/12/05", Value = "Logged in at the moment are:")]
    public string LoggedInAtTheMoment => this[nameof (LoggedInAtTheMoment)];

    /// <summary>Phrase: You can wait for a user to log off or you can</summary>
    [ResourceEntry("WaitForUserToLogOffOr", Description = "Phrase: You can wait for a user to log off or you can", LastModified = "2011/12/05", Value = "You can wait for a user to log off or you can")]
    public string WaitForUserToLogOffOr => this[nameof (WaitForUserToLogOffOr)];

    /// <summary>Phrase: browse the public website</summary>
    [ResourceEntry("BrowsePublicWebsite", Description = "Phrase: browse the public website", LastModified = "2011/12/05", Value = "browse the public website")]
    public string BrowsePublicWebsite => this[nameof (BrowsePublicWebsite)];

    /// <summary>Phrase: Force someone to logout</summary>
    [ResourceEntry("ForceSomeloneToLogout", Description = "Phrase: Force someone to logout", LastModified = "2011/12/05", Value = "Force someone to logout")]
    public string ForceSomeloneToLogout => this[nameof (ForceSomeloneToLogout)];

    /// <summary>
    /// Text for the message when backend user limit is reached
    /// </summary>
    [ResourceEntry("UserLimitReachedMessage", Description = "Text for the message when backend user limit is reached", LastModified = "2011/12/05", Value = "You cannot be logged in because the maximum allowed users limit has been reached.<br />Up to {0} users can be logged in the backend at the same time")]
    public string UserLimitReachedMessage => this[nameof (UserLimitReachedMessage)];

    /// <summary>Phrase: Choose which user to be logged out:</summary>
    [ResourceEntry("ChooseWhichUserToLogout", Description = "Phrase: Choose which user to be logged out:", LastModified = "2011/12/06", Value = "Choose which user to be logged out:")]
    public string ChooseWhichUserToLogout => this[nameof (ChooseWhichUserToLogout)];

    /// <summary>word: Image</summary>
    [ResourceEntry("ImageSingular", Description = "word: Image", LastModified = "2011/11/20", Value = "Image")]
    public string ImageSingular => this[nameof (ImageSingular)];

    /// <summary>word: Images</summary>
    [ResourceEntry("ImagePlural", Description = "word: Images", LastModified = "2011/11/20", Value = "Images")]
    public string ImagePlural => this[nameof (ImagePlural)];

    /// <summary>word: Document</summary>
    [ResourceEntry("DocumentSingular", Description = "word: Document", LastModified = "2011/11/20", Value = "Document")]
    public string DocumentSingular => this[nameof (DocumentSingular)];

    /// <summary>word: Documents</summary>
    [ResourceEntry("DocumentPlural", Description = "word: Documents", LastModified = "2011/11/20", Value = "Documents")]
    public string DocumentPlural => this[nameof (DocumentPlural)];

    /// <summary>word: Video</summary>
    [ResourceEntry("VideoSingular", Description = "word: Video", LastModified = "2011/11/20", Value = "Video")]
    public string VideoSingular => this[nameof (VideoSingular)];

    /// <summary>word: Videos</summary>
    [ResourceEntry("VideoPlural", Description = "word: Videos", LastModified = "2011/11/20", Value = "Videos")]
    public string VideoPlural => this[nameof (VideoPlural)];

    /// <summary>
    /// Phrase: Text editor settings and Custom tool sets are applied only to the text editor in the Classic UI.<br />Learn how to modify text editor tool set in the New UI and MVC widgets.
    /// </summary>
    [ResourceEntry("TextEditorCompatibilityMessage", Description = "Phrase: The following settings are applied only to the text editors in the classic interface. In the new interface, you configure the text editor directly via the ~\\AdminApp\\config.json file.Learn more", LastModified = "2019/02/28", Value = "The following settings are applied only to the text editors in the classic interface.<br />In the new interface, you configure the text editor directly via the <b>~\\AdminApp\\config.json</b> file. <a href=\"{0}\">Learn more</a>")]
    public string TextEditorCompatibilityMessage => this[nameof (TextEditorCompatibilityMessage)];

    /// <summary>Gets External Link: New UI editor settings</summary>
    [ResourceEntry("ExternalLinkNewUiEditorSettings", Description = "External Link: New UI editor settings", LastModified = "2019/02/27", Value = "https://www.progress.com/documentation/sitefinity-cms/administration-configure-the-kendo-editor")]
    public string ExternalLinkNewUiEditorSettings => this[nameof (ExternalLinkNewUiEditorSettings)];

    /// <summary>Phrase: Text editor settings</summary>
    [ResourceEntry("TextEditorSettings", Description = "Phrase: Text editor settings", LastModified = "2011/11/24", Value = "Text editor settings")]
    public string TextEditorSettings => this[nameof (TextEditorSettings)];

    /// <summary>Phrase: Custom tool sets</summary>
    [ResourceEntry("CustomToolSets", Description = "Phrase: Custom tool sets", LastModified = "2011/11/24", Value = "Custom tool sets")]
    public string CustomToolSets => this[nameof (CustomToolSets)];

    /// <summary>Phrase: Create a tool set</summary>
    [ResourceEntry("CreateToolSet", Description = "Phrase: Create a tool set", LastModified = "2011/11/24", Value = "Create a tool set")]
    public string CreateToolSet => this[nameof (CreateToolSet)];

    /// <summary>Phrase: Edit tool set</summary>
    [ResourceEntry("EditToolSet", Description = "Phrase: Edit tool set", LastModified = "2011/12/02", Value = "Edit tool set")]
    public string EditToolSet => this[nameof (EditToolSet)];

    /// <summary>
    /// Phrase: Create a new tool set based on the default one
    /// </summary>
    [ResourceEntry("CreateNewBasedOnDefaultToolSet", Description = "Phrase: Create a new tool set based on the default one", LastModified = "2011/11/24", Value = "Create a new tool set based on the default one")]
    public string CreateNewBasedOnDefaultToolSet => this[nameof (CreateNewBasedOnDefaultToolSet)];

    /// <summary>Phrase: Upload a file</summary>
    [ResourceEntry("UploadFile", Description = "Phrase: Upload a file", LastModified = "2011/11/24", Value = "Upload a file")]
    public string UploadFile => this[nameof (UploadFile)];

    /// <summary>
    /// Phrase: Add your own tool set by uploading an .xml file
    /// </summary>
    [ResourceEntry("AddToolSetByUploadingXmlFile", Description = "Phrase: Add your own tool set by uploading an .xml file", LastModified = "2011/11/24", Value = "Add your own tool set by uploading an .xml file")]
    public string AddToolSetByUploadingXmlFile => this[nameof (AddToolSetByUploadingXmlFile)];

    /// <summary>Phrase: Select .xml file</summary>
    [ResourceEntry("SelectXmlFile", Description = "Phrase: Select .xml file", LastModified = "2011/12/02", Value = "Select .xml file")]
    public string SelectXmlFile => this[nameof (SelectXmlFile)];

    /// <summary>word: items</summary>
    [ResourceEntry("BrowseDotDotDot", Description = "word: Browse...", LastModified = "2011/12/02", Value = "Browse...")]
    public string BrowseDotDotDot => this[nameof (BrowseDotDotDot)];

    /// <summary>
    /// Phrase: Add your own tool set by uploading the .xml file
    /// </summary>
    [ResourceEntry("AreYouSureYouWantToDeleteAndRestoreItem", Description = "Phrase: Select .xml file", LastModified = "2011/12/02", Value = "Are you sure you want to delete this item and then restore the default one?")]
    public string AreYouSureYouWantToDeleteAndRestoreItem => this[nameof (AreYouSureYouWantToDeleteAndRestoreItem)];

    /// <summary>Phrase: Select .xml file</summary>
    [ResourceEntry("UsedInAllBackendContentTypes", Description = "Phrase: Used in all backend content types", LastModified = "2011/12/08", Value = "Used in all backend content types")]
    public string UsedInAllBackendContentTypes => this[nameof (UsedInAllBackendContentTypes)];

    /// <summary>Phrase: Select .xml file</summary>
    [ResourceEntry("UsedInThePublicSiteWhereThereAreComments", Description = "Phrase: Used in the public site where there are comments", LastModified = "2011/12/08", Value = "Used in the public site where there are comments")]
    public string UsedInThePublicSiteWhereThereAreComments => this[nameof (UsedInThePublicSiteWhereThereAreComments)];

    /// <summary>Phrase: Select .xml file</summary>
    [ResourceEntry("UsedInThePublicSiteWhereThereAreForums", Description = "Phrase: Used in the public site where there are forums", LastModified = "2012/01/21", Value = "Used in the public site where there are forums")]
    public string UsedInThePublicSiteWhereThereAreForums => this[nameof (UsedInThePublicSiteWhereThereAreForums)];

    /// <summary>Messsage: ForumsTitle</summary>
    /// <value>Title for the Forums module.</value>
    [ResourceEntry("ForumsTitle", Description = "Title for the Forums module.", LastModified = "2012/01/15", Value = "Forums")]
    public string ForumsTitle => this[nameof (ForumsTitle)];

    /// <summary>Messsage: forums</summary>
    /// <value>Forums Description.</value>
    [ResourceEntry("Forums module", Description = "Forums", LastModified = "2012/01/11", Value = "ForumsDescription")]
    public string ForumsDescription => this[nameof (ForumsDescription)];

    /// <summary>word: hour</summary>
    [ResourceEntry("Hour", Description = "word: hour", LastModified = "2012/01/12", Value = "hour")]
    public string Hour => this[nameof (Hour)];

    /// <summary>word: hours</summary>
    [ResourceEntry("Hours", Description = "word: hours", LastModified = "2012/01/12", Value = "hours")]
    public string Hours => this[nameof (Hours)];

    /// <summary>word: day</summary>
    [ResourceEntry("Day", Description = "word: day", LastModified = "2012/01/12", Value = "day")]
    public string Day => this[nameof (Day)];

    /// <summary>word: days</summary>
    [ResourceEntry("Days", Description = "word: days", LastModified = "2012/01/12", Value = "days")]
    public string Days => this[nameof (Days)];

    /// <summary>word: minute</summary>
    [ResourceEntry("Minute", Description = "word: minute", LastModified = "2012/01/12", Value = "minute")]
    public string Minute => this[nameof (Minute)];

    /// <summary>word: minutes</summary>
    [ResourceEntry("Minutes", Description = "word: minutes", LastModified = "2012/01/12", Value = "minutes")]
    public string Minutes => this[nameof (Minutes)];

    /// <summary>phrase: less than a minute ago</summary>
    [ResourceEntry("LessThanAMintueAgo", Description = "phrase: less than a minute ago", LastModified = "2012/01/30", Value = "less than a minute ago")]
    public string LessThanAMintueAgo => this[nameof (LessThanAMintueAgo)];

    /// <summary>word: ago</summary>
    [ResourceEntry("Ago", Description = "word: ago", LastModified = "2012/01/12", Value = "ago")]
    public string Ago => this[nameof (Ago)];

    /// <summary>word: and</summary>
    [ResourceEntry("And", Description = "word", LastModified = "2012/01/12", Value = "and")]
    public string And => this[nameof (And)];

    /// <summary>Word: Sitefinity</summary>
    [ResourceEntry("Sitefinity", Description = "Word: Sitefinity", LastModified = "2012/01/27", Value = "Sitefinity")]
    public string Sitefinity => this[nameof (Sitefinity)];

    /// <summary>phrase: To reset your password, enter your email</summary>
    [ResourceEntry("ToResetYourPasswordEnterYourEmail", Description = "phrase: To reset your password, enter your email", LastModified = "2012/02/14", Value = "To reset your password, enter your email")]
    public string ToResetYourPasswordEnterYourEmail => this[nameof (ToResetYourPasswordEnterYourEmail)];

    /// <summary>
    /// phrase: Password reset instructions have been sent to your email
    /// </summary>
    [ResourceEntry("PasswordResetInstructionsHaveBeenSentToYourEmail", Description = "phrase: Password reset instructions have been sent to your email", LastModified = "2012/02/14", Value = "Password reset instructions have been sent to your email")]
    public string PasswordResetInstructionsHaveBeenSentToYourEmail => this[nameof (PasswordResetInstructionsHaveBeenSentToYourEmail)];

    /// <summary>
    /// The body of the email sent to users for changing their passwords
    /// </summary>
    [ResourceEntry("LostPasswordEmailBody", Description = "The body of the email sent to users for changing their passwords", LastModified = "2012/02/15", Value = "Dear <%\\s*UserDisplayName\\s*%>, we received a request for password change for username <%\\s*UserName\\s*%> at <%\\s*SiteName\\s*%>.<br />\r\n<a href=\"<%\\s*ConfirmationUrl\\s*%>\" target=\"_blank\">Go to this page</a> to set your new password. The link will be active for one hour.<br />\r\n<br />\r\nRegards,<br />\r\nThe <%\\s*SiteName\\s*%> team")]
    public string LostPasswordEmailBody => this[nameof (LostPasswordEmailBody)];

    /// <summary>phrase: Continue to website</summary>
    [ResourceEntry("ContinueToWebsite", Description = "phrase: Continue to website", LastModified = "2012/02/15", Value = "Continue to website")]
    public string ContinueToWebsite => this[nameof (ContinueToWebsite)];

    /// <summary>What to include in the breadcrumb?</summary>
    [ResourceEntry("BreadcrumbIncludeOptions", Description = "What to include in the breadcrumb?", LastModified = "2012/02/14", Value = "What to include in the breadcrumb?")]
    public string BreadcrumbIncludeOptions => this[nameof (BreadcrumbIncludeOptions)];

    /// <summary>Full path to the current page</summary>
    [ResourceEntry("BreadcrumbShowFullPath", Description = "Full path to the current page", LastModified = "2012/02/14", Value = "Full path to the current page")]
    public string BreadcrumbShowFullPath => this[nameof (BreadcrumbShowFullPath)];

    /// <summary>Path starting from a specific page...</summary>
    [ResourceEntry("BreadcrumbSelectSpecificPage", Description = "Path starting from a specific page...", LastModified = "2012/02/14", Value = "Path starting from a specific page...")]
    public string BreadcrumbSelectSpecificPage => this[nameof (BreadcrumbSelectSpecificPage)];

    /// <summary>Show Home page link</summary>
    [ResourceEntry("BreadcrumbShowParentPage", Description = "Show Home page link", LastModified = "2012/02/14", Value = "Show Home page link")]
    public string BreadcrumbShowParentPage => this[nameof (BreadcrumbShowParentPage)];

    /// <summary>Show group pages in the breadcrumb</summary>
    [ResourceEntry("BreadcrumbShowGroupPages", Description = "Show group pages in the breadcrumb", LastModified = "2012/02/23", Value = "Show group pages in the breadcrumb")]
    public string BreadcrumbShowGroupPages => this[nameof (BreadcrumbShowGroupPages)];

    /// <summary>Show current page in the end of the breadcrumb</summary>
    [ResourceEntry("BreadcrumbShowCurrentPage", Description = "Show current page in the end of the breadcrumb", LastModified = "2012/02/14", Value = "Show current page in the end of the breadcrumb")]
    public string BreadcrumbShowCurrentPage => this[nameof (BreadcrumbShowCurrentPage)];

    /// <summary>Label</summary>
    [ResourceEntry("BreadcrumbLabel", Description = "Label", LastModified = "2012/02/14", Value = "Label")]
    public string BreadcrumbLabel => this[nameof (BreadcrumbLabel)];

    /// <summary>Example:</summary>
    [ResourceEntry("BreadcrumbLabelExample", Description = "Example: ", LastModified = "2012/02/14", Value = "Example: ")]
    public string BreadcrumbLabelExample => this[nameof (BreadcrumbLabelExample)];

    /// <summary>You are here:</summary>
    [ResourceEntry("BreadcrumbExampleText", Description = "You are here:", LastModified = "2012/02/14", Value = "<strong>Example: </strong> <em>You are here:</em>")]
    public string BreadcrumbExampleText => this[nameof (BreadcrumbExampleText)];

    /// <summary>
    /// Breadcrumb is visible when you are on a particular page
    /// </summary>
    [ResourceEntry("BreadcrumbOnTemplateMessage", Description = "Breadcrumb is visible when you are on a particular page.", LastModified = "2012/02/21", Value = "Breadcrumb is visible when you are on a particular page.")]
    public string BreadcrumbOnTemplateMessage => this[nameof (BreadcrumbOnTemplateMessage)];

    /// <summary>word: Template</summary>
    [ResourceEntry("Template", Description = "word: Template", LastModified = "2012/03/16", Value = "Template")]
    public string Template => this[nameof (Template)];

    /// <summary>Word: Subscribe</summary>
    [ResourceEntry("Subscribe", Description = "word", LastModified = "2011/04/24", Value = "Subscribe")]
    public string Subscribe => this[nameof (Subscribe)];

    /// <summary>Word: Unsubscribe</summary>
    [ResourceEntry("Unsubscribe", Description = "word", LastModified = "2011/04/24", Value = "Unsubscribe")]
    public string Unsubscribe => this[nameof (Unsubscribe)];

    /// <summary>Word: Language</summary>
    [ResourceEntry("Language", Description = "Word: Language", LastModified = "2012/09/05", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>Phrase: Used by</summary>
    [ResourceEntry("UsedBy", Description = "Phrase: Used by", LastModified = "2012/09/05", Value = "Used by")]
    public string UsedBy => this[nameof (UsedBy)];

    /// <summary>Phrase: Show more</summary>
    [ResourceEntry("MoreText", Description = "Phrase: Show more", LastModified = "2012/09/05", Value = "Show more")]
    public string MoreText => this[nameof (MoreText)];

    /// <summary>Phrase: Show less</summary>
    [ResourceEntry("LessText", Description = "Phrase: Show less", LastModified = "2012/09/05", Value = "Show less")]
    public string LessText => this[nameof (LessText)];

    /// <summary>Phrase: Yes, Remove language</summary>
    [ResourceEntry("RemoveLanguageButtonText", Description = "Phrase: Yes, Remove language", LastModified = "2012/09/05", Value = "Yes, Remove language")]
    public string RemoveLanguageButtonText => this[nameof (RemoveLanguageButtonText)];

    /// <summary>
    /// Phrase: Selected language will be permanently removed from all sites that are currently using it.
    /// </summary>
    [ResourceEntry("RemoveLanguageConfirmationMessageLine1", Description = "Phrase: Selected language will be permanently removed from all sites that are currently using it.", LastModified = "2012/09/05", Value = "Selected language will <strong>be permanently removed</strong> from all sites that are currently using it.")]
    public string RemoveLanguageConfirmationMessageLine1 => this[nameof (RemoveLanguageConfirmationMessageLine1)];

    /// <summary>
    /// Phrase: Are you sure you want to remove this language from all sites?
    /// </summary>
    [ResourceEntry("RemoveLanguageConfirmationMessageLine2", Description = "Phrase: Are you sure you want to remove this language from all sites?", LastModified = "2012/09/05", Value = "Are you sure you want to remove this language from all sites?")]
    public string RemoveLanguageConfirmationMessageLine2 => this[nameof (RemoveLanguageConfirmationMessageLine2)];

    /// <summary>
    /// Phrase: Selected language could not be removed because this is the default language for
    /// </summary>
    [ResourceEntry("CannotRemoveLanguageConfirmationMessage", Description = "Phrase: Selected language could not be removed because this is the default language for ", LastModified = "2012/09/21", Value = "Selected language could not be removed because this is the default language for ")]
    public string CannotRemoveLanguageConfirmationMessage => this[nameof (CannotRemoveLanguageConfirmationMessage)];

    /// <summary>Phrase: Narrow tags by typing...</summary>
    [ResourceEntry("NarrowTagsByTyping", Description = "Phrase: Narrow tags by typing...", LastModified = "2012/07/30", Value = "Narrow tags by typing...")]
    public string NarrowTagsByTyping => this[nameof (NarrowTagsByTyping)];

    /// <summary>Phrase: Select source</summary>
    [ResourceEntry("SelectSource", Description = "Phrase: Select source", LastModified = "2012/09/12", Value = "Select source")]
    public string SelectSource => this[nameof (SelectSource)];

    /// <summary>Word: SOURCES:</summary>
    [ResourceEntry("Sources", Description = "word: SOURCES:", LastModified = "2012/09/12", Value = "SOURCES:")]
    public string Sources => this[nameof (Sources)];

    /// <summary>Phease: Share with...</summary>
    [ResourceEntry("ShareWith", Description = "Phease: Share with...", LastModified = "2012/09/12", Value = "Share with...")]
    public string ShareWith => this[nameof (ShareWith)];

    /// <summary>Phease: Back to {0}</summary>
    [ResourceEntry("BackToSite", Description = "Phease: Back to {0}", LastModified = "2012/09/19", Value = "Back to {0}")]
    public string BackToSite => this[nameof (BackToSite)];

    /// <summary>phrase: All {0} sources</summary>
    [ResourceEntry("AllSources", Description = "phrase: All {0} sources", LastModified = "2012/09/19", Value = "All {0} sources")]
    public string AllSources => this[nameof (AllSources)];

    /// <summary>Word: Source</summary>
    [ResourceEntry("Source", Description = "Word: Source", LastModified = "2012/09/19", Value = "Source")]
    public string Source => this[nameof (Source)];

    /// <summary>phrase: {0} more</summary>
    [ResourceEntry("ShowMore", Description = "phrase: {0} more", LastModified = "2012/09/19", Value = "{0} more")]
    public string ShowMore => this[nameof (ShowMore)];

    /// <summary>phrase: {0} less</summary>
    [ResourceEntry("ShowLess", Description = "phrase: {0} less", LastModified = "2012/09/21", Value = "{0} less")]
    public string ShowLess => this[nameof (ShowLess)];

    /// <summary>
    /// Phrase: Currently selected source is no longer available. To setup this widget to display content properly you should configure another source.
    /// </summary>
    [ResourceEntry("DefinedProviderNotAvailable", Description = "Phrase: Currently selected source is no longer available. To setup this widget to display content properly you should configure another source.", LastModified = "2012/10/10", Value = "Currently selected source is no longer available. To setup this widget to display content properly you should configure another source.")]
    public string DefinedProviderNotAvailable => this[nameof (DefinedProviderNotAvailable)];

    /// <summary>Indefinite article: An</summary>
    [ResourceEntry("An", Description = "Indefinite article: An", LastModified = "2012/09/29", Value = "An")]
    public string An => this[nameof (An)];

    /// <summary>Indefinite article: A</summary>
    [ResourceEntry("A", Description = "Indefinite article: A", LastModified = "2012/09/29", Value = "A")]
    public string A => this[nameof (A)];

    /// <summary>Phrase: Default source</summary>
    [ResourceEntry("DefaultSource", Description = "Phrase: Default source", LastModified = "2012/10/10", Value = "Default source")]
    public string DefaultSource => this[nameof (DefaultSource)];

    /// <summary>Phrase: Try login again</summary>
    [ResourceEntry("TryLoginAgain", Description = "Phrase: Try login again", LastModified = "2012/10/09", Value = "Try login again")]
    public string TryLoginAgain => this[nameof (TryLoginAgain)];

    /// <summary>word: email / username</summary>
    /// <value>username</value>
    [ResourceEntry("UsernameLower", Description = "word: username", LastModified = "2016/12/08", Value = "email / username")]
    public string UsernameLower => this[nameof (UsernameLower)];

    /// <summary>word: password</summary>
    /// <value>password</value>
    [ResourceEntry("PasswordLower", Description = "word: password", LastModified = "2012/11/01", Value = "password")]
    public string PasswordLower => this[nameof (PasswordLower)];

    /// <summary>word: margin</summary>
    /// <value>margin</value>
    [ResourceEntry("Margin", Description = "word: margin", LastModified = "2012/11/01", Value = "Margin")]
    public string Margin => this[nameof (Margin)];

    /// <summary>Phrase: Today</summary>
    [ResourceEntry("ScheduleToday", Description = "Phrase: Today", LastModified = "2013/01/28", Value = "Today")]
    public string ScheduleToday => this[nameof (ScheduleToday)];

    /// <summary>Phrase: Tomorrow</summary>
    [ResourceEntry("ScheduleTomorrow", Description = "Phrase: Tomorrow", LastModified = "2013/01/28", Value = "Tomorrow")]
    public string ScheduleTomorrow => this[nameof (ScheduleTomorrow)];

    /// <summary>Phrase: Every day</summary>
    [ResourceEntry("ScheduleEveryDay", Description = "Phrase: Every day", LastModified = "2013/01/28", Value = "Every day")]
    public string ScheduleEveryDay => this[nameof (ScheduleEveryDay)];

    /// <summary>Phrase: Every Monday</summary>
    [ResourceEntry("ScheduleEveryMonday", Description = "Phrase: Every Monday", LastModified = "2013/01/28", Value = "Every Monday")]
    public string ScheduleEveryMonday => this[nameof (ScheduleEveryMonday)];

    /// <summary>Phrase: Every Tuesday</summary>
    [ResourceEntry("ScheduleEveryTuesday", Description = "Phrase: Every Tuesday", LastModified = "2013/01/28", Value = "Every Tuesday")]
    public string ScheduleEveryTuesday => this[nameof (ScheduleEveryTuesday)];

    /// <summary>Phrase: Every Wednesday</summary>
    [ResourceEntry("ScheduleEveryWednesday", Description = "Phrase: Every Wednesday", LastModified = "2013/01/28", Value = "Every Wednesday")]
    public string ScheduleEveryWednesday => this[nameof (ScheduleEveryWednesday)];

    /// <summary>Phrase: Every Thursday</summary>
    [ResourceEntry("ScheduleEveryThursday", Description = "Phrase: Every Thursday", LastModified = "2013/01/28", Value = "Every Thursday")]
    public string ScheduleEveryThursday => this[nameof (ScheduleEveryThursday)];

    /// <summary>Phrase:Every Friday</summary>
    [ResourceEntry("ScheduleEveryFriday", Description = "Phrase:Every Friday", LastModified = "2013/01/28", Value = "Every Friday")]
    public string ScheduleEveryFriday => this[nameof (ScheduleEveryFriday)];

    /// <summary>Phrase:Every Saturday</summary>
    [ResourceEntry("ScheduleEverySaturday", Description = "Phrase:Every Saturday", LastModified = "2013/01/28", Value = "Every Saturday")]
    public string ScheduleEverySaturday => this[nameof (ScheduleEverySaturday)];

    /// <summary>Phrase:Every Sunday</summary>
    [ResourceEntry("ScheduleEverySunday", Description = "Phrase:Every Sunday", LastModified = "2013/01/28", Value = "Every Sunday")]
    public string ScheduleEverySunday => this[nameof (ScheduleEverySunday)];

    /// <summary>Phrase: Specific date</summary>
    [ResourceEntry("ScheduleSpecificDate", Description = "Phrase:Specific date", LastModified = "2013/01/28", Value = "Specific date")]
    public string ScheduleSpecificDate => this[nameof (ScheduleSpecificDate)];

    /// <summary>Phrase: Schedule</summary>
    [ResourceEntry("ScheduleButtonLabel", Description = "Phrase:Schedule", LastModified = "2013/01/28", Value = "Schedule")]
    public string ScheduleButtonLabel => this[nameof (ScheduleButtonLabel)];

    /// <summary>Phrase: Cancel</summary>
    [ResourceEntry("CancelScheduleButtonLabel", Description = "Phrase:Cancel", LastModified = "2013/01/28", Value = "Cancel")]
    public string CancelScheduleButtonLabel => this[nameof (CancelScheduleButtonLabel)];

    /// <summary>Phrase: Schedule</summary>
    [ResourceEntry("ScheduleActionLabelDefault", Description = "Phrase:Schedule", LastModified = "2013/01/29", Value = "Schedule")]
    public string ScheduleActionLabelDefault => this[nameof (ScheduleActionLabelDefault)];

    /// <summary>Phrase: Save changes</summary>
    [ResourceEntry("ChangeScheduleButtonLabel", Description = "Phrase:Save changes", LastModified = "2013/01/28", Value = "Save changes")]
    public string ChangeScheduleButtonLabel => this[nameof (ChangeScheduleButtonLabel)];

    /// <summary>Phrase: Cancel</summary>
    [ResourceEntry("CancelSchedulingButtonLabel", Description = "Phrase:Cancel", LastModified = "2013/01/28", Value = "Cancel")]
    public string CancelSchedulingButtonLabel => this[nameof (CancelSchedulingButtonLabel)];

    /// <summary>phrase: Calendar is not selected</summary>
    /// <value>Calendar is not selected</value>
    [ResourceEntry("CalendarNotSelected", Description = "phrase: Calendar is not selected", LastModified = "2013/03/28", Value = "Calendar is not selected")]
    public string CalendarNotSelected => this[nameof (CalendarNotSelected)];

    /// <summary>phrase: Select calendar</summary>
    /// <value>Select calendar</value>
    [ResourceEntry("SelectCalendar", Description = "phrase: Select calendar", LastModified = "2013/03/28", Value = "Select calendar")]
    public string SelectCalendar => this[nameof (SelectCalendar)];

    /// <summary>phrase: Choose calendar</summary>
    /// <value>Choose calendar</value>
    [ResourceEntry("ChooseCalendar", Description = "phrase: Choose calendar", LastModified = "2013/03/28", Value = "Choose calendar")]
    public string ChooseCalendar => this[nameof (ChooseCalendar)];

    /// <summary>phrase: Narrow calendar title</summary>
    /// <value>Narrow calendar title</value>
    [ResourceEntry("NarrowCalendarTitle", Description = "phrase: Narrow calendar title", LastModified = "2013/03/28", Value = "Narrow calendar title")]
    public string NarrowCalendarTitle => this[nameof (NarrowCalendarTitle)];

    /// <summary>Paging, limit and sorting</summary>
    [ResourceEntry("PagingSectionComplementoryText", Description = "Paging, limit and sorting", LastModified = "2013/03/21", Value = "Paging, limit and sorting")]
    public string PagingSectionComplementoryText => this[nameof (PagingSectionComplementoryText)];

    /// <summary>Page for Single item</summary>
    [ResourceEntry("PageForSingleItem", Description = "Page for Single item", LastModified = "2013/04/17", Value = "Page for Single item<br/> <span class=\"sfNote\">This is the page which is opened clicking the name of a list item. There must be an Event widget on this page set to display Single item only.</span>")]
    public string PageForSingleItem => this[nameof (PageForSingleItem)];

    /// <summary>phrase: Allow users to add events to their calendar</summary>
    [ResourceEntry("AddEventsToCalendarOption", Description = "Allow users to add events to their calendar", LastModified = "2013/03/21", Value = "Allow users to add events to their calendar")]
    public string AddEventsToCalendarOption => this[nameof (AddEventsToCalendarOption)];

    /// <summary>
    /// phrase: The users can add events to Outlook, Google Calendar and iCal
    /// </summary>
    [ResourceEntry("AddEventsToOutlook", Description = "The users can add events to Outlook, Google Calendar and iCal", LastModified = "2013/03/21", Value = "The users can add events to Outlook, Google Calendar and iCal")]
    public string AddEventsToOutlook => this[nameof (AddEventsToOutlook)];

    /// <summary>Word: Daily</summary>
    [ResourceEntry("Daily", Description = "Word: Daily", LastModified = "2013/03/20", Value = "Daily")]
    public string Daily => this[nameof (Daily)];

    /// <summary>Word: Weekly</summary>
    [ResourceEntry("Weekly", Description = "Word: Weekly", LastModified = "2013/03/20", Value = "Weekly")]
    public string Weekly => this[nameof (Weekly)];

    /// <summary>Word: Monthly</summary>
    [ResourceEntry("Monthly", Description = "Word: Monthly", LastModified = "2013/03/20", Value = "Monthly")]
    public string Monthly => this[nameof (Monthly)];

    /// <summary>Word: Yearly</summary>
    [ResourceEntry("Yearly", Description = "Word: Yearly", LastModified = "2013/03/20", Value = "Yearly")]
    public string Yearly => this[nameof (Yearly)];

    /// <summary>Word: Repeat...</summary>
    [ResourceEntry("RepeatEllipsis", Description = "Word: Repeat...", LastModified = "2013/03/21", Value = "Repeat...")]
    public string RepeatEllipsis => this[nameof (RepeatEllipsis)];

    /// <summary>Word: after</summary>
    [ResourceEntry("After", Description = "Word: after", LastModified = "2013/03/22", Value = "after")]
    public string After => this[nameof (After)];

    /// <summary>Word: repeat(s)</summary>
    [ResourceEntry("Repeats", Description = "Word: repeat(s)", LastModified = "2013/03/22", Value = "repeat(s)")]
    public string Repeats => this[nameof (Repeats)];

    /// <summary>Phrase: no end date</summary>
    [ResourceEntry("NoEndDate", Description = "Phrase: no end date", LastModified = "2013/03/22", Value = "no end date")]
    public string NoEndDate => this[nameof (NoEndDate)];

    /// <summary>All day</summary>
    /// <value>All day</value>
    [ResourceEntry("AllDay", Description = "All day", LastModified = "2013/03/22", Value = "All day")]
    public string AllDay => this[nameof (AllDay)];

    /// <summary>Start time</summary>
    [ResourceEntry("StartTime", Description = "Start time", LastModified = "2013/03/25", Value = "Start time")]
    public string StartTime => this[nameof (StartTime)];

    /// <summary>End time</summary>
    [ResourceEntry("EndTime", Description = "End time", LastModified = "2013/03/25", Value = "End time")]
    public string EndTime => this[nameof (EndTime)];

    /// <summary>Every</summary>
    [ResourceEntry("Every", Description = "Every", LastModified = "2013/03/26", Value = "Every")]
    public string Every => this[nameof (Every)];

    /// <summary>Every weekday</summary>
    [ResourceEntry("EveryWeekday", Description = "Every weekday", LastModified = "2013/03/26", Value = "Every weekday")]
    public string EveryWeekday => this[nameof (EveryWeekday)];

    /// <summary>day(s)</summary>
    [ResourceEntry("DayOrDays", Description = "day(s)", LastModified = "2013/03/26", Value = "day(s)")]
    public string DayOrDays => this[nameof (DayOrDays)];

    /// <summary>Monday</summary>
    [ResourceEntry("Monday", Description = "Monday", LastModified = "2013/03/26", Value = "Monday")]
    public string Monday => this[nameof (Monday)];

    /// <summary>Tuesday</summary>
    [ResourceEntry("Tuesday", Description = "Tuesday", LastModified = "2013/03/26", Value = "Tuesday")]
    public string Tuesday => this[nameof (Tuesday)];

    /// <summary>Wednesday</summary>
    [ResourceEntry("Wednesday", Description = "Wednesday", LastModified = "2013/03/26", Value = "Wednesday")]
    public string Wednesday => this[nameof (Wednesday)];

    /// <summary>Thursday</summary>
    [ResourceEntry("Thursday", Description = "Thursday", LastModified = "2013/03/26", Value = "Thursday")]
    public string Thursday => this[nameof (Thursday)];

    /// <summary>Friday</summary>
    [ResourceEntry("Friday", Description = "Friday", LastModified = "2013/03/26", Value = "Friday")]
    public string Friday => this[nameof (Friday)];

    /// <summary>Saturday</summary>
    [ResourceEntry("Saturday", Description = "Saturday", LastModified = "2013/03/26", Value = "Saturday")]
    public string Saturday => this[nameof (Saturday)];

    /// <summary>Sunday</summary>
    [ResourceEntry("Sunday", Description = "Sunday", LastModified = "2013/03/26", Value = "Sunday")]
    public string Sunday => this[nameof (Sunday)];

    /// <summary>week(s)</summary>
    [ResourceEntry("WeekOrWeeks", Description = "week(s)", LastModified = "2013/03/26", Value = "week(s)")]
    public string WeekOrWeeks => this[nameof (WeekOrWeeks)];

    /// <summary>Day</summary>
    [ResourceEntry("DayCapital", Description = "Day", LastModified = "2013/03/26", Value = "Day")]
    public string DayCapital => this[nameof (DayCapital)];

    /// <summary>The</summary>
    [ResourceEntry("The", Description = "The", LastModified = "2013/03/26", Value = "The")]
    public string The => this[nameof (The)];

    /// <summary>month(s)</summary>
    [ResourceEntry("MonthOrMonths", Description = "month(s)", LastModified = "2013/03/26", Value = "month(s)")]
    public string MonthOrMonths => this[nameof (MonthOrMonths)];

    /// <summary>of every</summary>
    [ResourceEntry("OfEvery", Description = "of every", LastModified = "2013/03/26", Value = "of every")]
    public string OfEvery => this[nameof (OfEvery)];

    /// <summary>first</summary>
    [ResourceEntry("First", Description = "first", LastModified = "2013/03/26", Value = "first")]
    public string First => this[nameof (First)];

    /// <summary>second</summary>
    [ResourceEntry("Second", Description = "second", LastModified = "2013/03/26", Value = "second")]
    public string Second => this[nameof (Second)];

    /// <summary>third</summary>
    [ResourceEntry("Third", Description = "third", LastModified = "2013/03/26", Value = "third")]
    public string Third => this[nameof (Third)];

    /// <summary>fourth</summary>
    [ResourceEntry("Fourth", Description = "fourth", LastModified = "2013/03/26", Value = "fourth")]
    public string Fourth => this[nameof (Fourth)];

    /// <summary>weekday</summary>
    [ResourceEntry("Weekdays", Description = "weekday", LastModified = "2013/05/13", Value = "weekday")]
    public string Weekdays => this[nameof (Weekdays)];

    /// <summary>weekend day</summary>
    [ResourceEntry("WeekendDays", Description = "weekend day", LastModified = "2013/05/13", Value = "weekend day")]
    public string WeekendDays => this[nameof (WeekendDays)];

    /// <summary>of</summary>
    [ResourceEntry("Of", Description = "of", LastModified = "2013/03/26", Value = "of")]
    public string Of => this[nameof (Of)];

    /// <summary>January</summary>
    [ResourceEntry("January", Description = "January", LastModified = "2013/03/26", Value = "January")]
    public string January => this[nameof (January)];

    /// <summary>February</summary>
    [ResourceEntry("February", Description = "February", LastModified = "2013/03/26", Value = "February")]
    public string February => this[nameof (February)];

    /// <summary>March</summary>
    [ResourceEntry("March", Description = "March", LastModified = "2013/03/26", Value = "March")]
    public string March => this[nameof (March)];

    /// <summary>April</summary>
    [ResourceEntry("April", Description = "April", LastModified = "2013/03/26", Value = "April")]
    public string April => this[nameof (April)];

    /// <summary>May</summary>
    [ResourceEntry("May", Description = "May", LastModified = "2013/03/26", Value = "May")]
    public string May => this[nameof (May)];

    /// <summary>June</summary>
    [ResourceEntry("June", Description = "June", LastModified = "2013/03/26", Value = "June")]
    public string June => this[nameof (June)];

    /// <summary>July</summary>
    [ResourceEntry("July", Description = "July", LastModified = "2013/03/26", Value = "July")]
    public string July => this[nameof (July)];

    /// <summary>August</summary>
    [ResourceEntry("August", Description = "August", LastModified = "2013/03/26", Value = "August")]
    public string August => this[nameof (August)];

    /// <summary>September</summary>
    [ResourceEntry("September", Description = "September", LastModified = "2013/03/26", Value = "September")]
    public string September => this[nameof (September)];

    /// <summary>October</summary>
    [ResourceEntry("October", Description = "October", LastModified = "2013/03/26", Value = "October")]
    public string October => this[nameof (October)];

    /// <summary>November</summary>
    [ResourceEntry("November", Description = "November", LastModified = "2013/03/26", Value = "November")]
    public string November => this[nameof (November)];

    /// <summary>December</summary>
    [ResourceEntry("December", Description = "December", LastModified = "2013/03/26", Value = "December")]
    public string December => this[nameof (December)];

    /// <summary>phrase: Responsive design</summary>
    [ResourceEntry("ResponsiveDesign", Description = "phrase: Responsive design", LastModified = "2013/04/04", Value = "Responsive design")]
    public string ResponseDesign => this["ResponsiveDesign"];

    /// <summary>phrase: All columns are visible</summary>
    [ResourceEntry("AllColumnsAreVisible", Description = "phrase: All columns are visible", LastModified = "2013/04/04", Value = "All columns are visible")]
    public string AllColumnsAreVisible => this[nameof (AllColumnsAreVisible)];

    /// <summary>phrase: All columns are hidden</summary>
    [ResourceEntry("AllColumnsAreHidden", Description = "phrase: All columns are hidden", LastModified = "2013/04/23", Value = "All columns are hidden")]
    public string AllColumnsAreHidden => this[nameof (AllColumnsAreHidden)];

    /// <summary>phrase: Column 1 is hidden for Tablets</summary>
    [ResourceEntry("ColumnIsHidden", Description = "phrase: Column 1 is hidden for Tablets", LastModified = "2013/04/23", Value = "Column {0} is hidden for {1}")]
    public string ColumnIsHidden => this[nameof (ColumnIsHidden)];

    /// <summary>word: Change...</summary>
    [ResourceEntry("ChangeElipsis", Description = "word: Change...", LastModified = "2013/04/04", Value = "Change...")]
    public string ChangeElipsis => this[nameof (ChangeElipsis)];

    /// <summary>phrase: Change column visibility</summary>
    [ResourceEntry("ChangeColumnVisibility", Description = "phrase: Change column visibility", LastModified = "2013/04/05", Value = "Change column visibility")]
    public string ChangeColumnVisibility => this[nameof (ChangeColumnVisibility)];

    /// <summary>word: Column</summary>
    [ResourceEntry("Column", Description = "word: Column", LastModified = "2013/04/05", Value = "Column")]
    public string Column => this[nameof (Column)];

    /// <summary>phrase: is visible for</summary>
    [ResourceEntry("IsVisibleFor", Description = "phrase: is visible for", LastModified = "2013/04/08", Value = "is visible for")]
    public string IsVisibleFor => this[nameof (IsVisibleFor)];

    /// <summary>Phrase: Google Maps</summary>
    [ResourceEntry("GoogleMapsSettingsTitle", Description = "The 'Google Maps' title in Google Maps Basic Settings View.", LastModified = "2013/03/27", Value = "Google Maps")]
    public string GoogleMapsSettingsTitle => this[nameof (GoogleMapsSettingsTitle)];

    /// <summary>
    /// Phrase: In order to use Google Maps for all Address fields, you need  API Key.
    /// </summary>
    [ResourceEntry("GoogleMapsUsageDescription", Description = "Phrase: In order to use Google Maps for all Address fields, you need  API Key.", LastModified = "2013/03/27", Value = "In order to use Google Maps for all Address fields, you need  API Key.")]
    public string GoogleMapsUsageDescription => this[nameof (GoogleMapsUsageDescription)];

    /// <summary>
    /// Phrase: You are solely responsible for compliance with the Google Maps API Terms of Service and any other applicable Google licensing requirements related to your use or implementation of the Google Maps API. You are solely responsible for any and all fees that Google may assess related to your use or implementation of the Google Maps API.
    /// </summary>
    [ResourceEntry("GoogleAPILicenseDisclaimer", Description = "Phrase: You are solely responsible for compliance with the Google Maps API Terms of Service and any other applicable Google licensing requirements related to your use or implementation of the Google Maps API. You are solely responsible for any and all fees that Google may assess related to your use or implementation of the Google Maps API.", LastModified = "2013/04/11", Value = "You are solely responsible for compliance with the Google Maps API Terms of Service and any other applicable Google licensing requirements related to your use or implementation of the Google Maps API. You are solely responsible for any and all fees that Google may assess related to your use or implementation of the Google Maps API.")]
    public string GoogleAPILicenseDisclaimer => this[nameof (GoogleAPILicenseDisclaimer)];

    /// <summary>Phrase: Get Google Maps v3 API Key</summary>
    [ResourceEntry("GetGoogleMapsApiKey", Description = "Phrase: Google Maps v3 API Key", LastModified = "2013/03/27", Value = "Get Google Maps v3 API Key")]
    public string GetGoogleMapsApiKey => this[nameof (GetGoogleMapsApiKey)];

    /// <summary>
    /// External link: Get Google Maps v3 API Key External link
    /// </summary>
    [ResourceEntry("ExternalLinkGetGoogleMapsApiKey", Description = "ExternalLink: Google Maps v3 API Key", LastModified = "2018/06/29", Value = "https://developers.google.com/maps/documentation/javascript/get-api-key")]
    public string ExternalLinkGetGoogleMapsApiKey => this[nameof (ExternalLinkGetGoogleMapsApiKey)];

    /// <summary>Phrase: , it is free</summary>
    [ResourceEntry("GoogleMapsItIsFree", Description = "Phrase: , it is free", LastModified = "2013/03/27", Value = ", it is free")]
    public string GoogleMapsItIsFree => this[nameof (GoogleMapsItIsFree)];

    /// <summary>Phrase: Google Maps v3 API Key</summary>
    [ResourceEntry("GoogleMapsApiKey", Description = " Phrase: Google Maps v3 API Key", LastModified = "2013/03/27", Value = "Google Maps v3 API Key")]
    public string GoogleMapsApiKey => this[nameof (GoogleMapsApiKey)];

    /// <summary>Phrase: Limits and Billing</summary>
    [ResourceEntry("GoogleMapLimitsAndBilling", Description = " Phrase: Limits and Billing", LastModified = "2013/03/27", Value = "Limits and Billing")]
    public string GoogleMapLimitsAndBilling => this[nameof (GoogleMapLimitsAndBilling)];

    /// <summary>Phrase: Up to 25 000 map loads per day are permitted.</summary>
    [ResourceEntry("GoogleMapLimitsAndBillingDescription1", Description = " Phrase: Up to 25 000 map loads per day are permitted.", LastModified = "2013/03/27", Value = "Up to 25 000 map loads per day are permitted.")]
    public string GoogleMapLimitsAndBillingDescription1 => this[nameof (GoogleMapLimitsAndBillingDescription1)];

    /// <summary>
    /// Phrase: It is not free to use Google Maps if this site exceed the limits for more than 90 consecutive days
    /// </summary>
    [ResourceEntry("GoogleMapLimitsAndBillingDescription2", Description = " Phrase: It is not free to use Google Maps if this site exceed the limits for more than 90 consecutive days", LastModified = "2013/03/27", Value = "It is not free to use Google Maps if this site exceeds the limits for more than 90 consecutive days")]
    public string GoogleMapLimitsAndBillingDescription2 => this[nameof (GoogleMapLimitsAndBillingDescription2)];

    /// <summary>Phrase: More about Limits and Billing</summary>
    [ResourceEntry("GoogleMapMoreAboutLimits", Description = " Phrase: More about Limits and Billing", LastModified = "2013/03/27", Value = "More about Limits and Billing")]
    public string GoogleMapMoreAboutLimits => this[nameof (GoogleMapMoreAboutLimits)];

    /// <summary>External link: More about Limits and Billing</summary>
    [ResourceEntry("ExternalLinkGoogleMapMoreAboutLimits", Description = " External link: More about Limits and Billing", LastModified = "2018/06/29", Value = "https://developers.google.com/maps/documentation/javascript/usage")]
    public string ExternalLinkGoogleMapMoreAboutLimits => this[nameof (ExternalLinkGoogleMapMoreAboutLimits)];

    /// <summary>phrase: No calendars are created</summary>
    /// <value>No calendars are created</value>
    [ResourceEntry("NoCalendarsAreCreated", Description = "phrase: No calendars are created", LastModified = "2013/04/12", Value = "No calendars are created")]
    public string NoCalendarsAreCreated => this[nameof (NoCalendarsAreCreated)];

    /// <summary>
    /// phrase: The selected calendar was deleted. Please select another.
    /// </summary>
    /// <value>The selected calendar was deleted. Please select another.</value>
    [ResourceEntry("SelectedCalendarWasDeletedSelectAnother", Description = "phrase: The selected calendar was deleted. Please select another.", LastModified = "2013/04/12", Value = "The selected calendar was deleted. Please select another.")]
    public string SelectedCalendarWasDeletedSelectAnother => this[nameof (SelectedCalendarWasDeletedSelectAnother)];

    /// <summary>phrase: The selected calendar was deleted</summary>
    /// <value>The selected calendar was deleted</value>
    [ResourceEntry("SelectedCalendarWasDeleted", Description = "phrase: The selected calendar was deleted", LastModified = "2013/04/12", Value = "The selected calendar was deleted")]
    public string SelectedCalendarWasDeleted => this[nameof (SelectedCalendarWasDeleted)];

    /// <summary>phrase: From all calendars</summary>
    /// <value>From all calendars</value>
    [ResourceEntry("FromAllCalendars", Description = "phrase: From all calendars", LastModified = "2013/04/12", Value = "From all calendars")]
    public string FromAllCalendars => this[nameof (FromAllCalendars)];

    /// <summary>phrase: From selected calendars only</summary>
    /// <value>From selected calendars only</value>
    [ResourceEntry("FromSelectedCalendarsOnly", Description = "phrase: From selected calendars only", LastModified = "2013/04/12", Value = "From selected calendars only")]
    public string FromSelectedCalendarsOnly => this[nameof (FromSelectedCalendarsOnly)];

    /// <summary>phrase: All published events from selected calendar</summary>
    /// <value>All published events from selected calendars</value>
    [ResourceEntry("AllPublishedEventsFromSelectedCalendars", Description = "phrase: All published events from selected calendar", LastModified = "2013/04/15", Value = "All published events from selected calendars")]
    public string AllPublishedEventsFromSelectedCalendars => this[nameof (AllPublishedEventsFromSelectedCalendars)];

    /// <summary>phrase: No selected event</summary>
    /// <value>No selected event</value>
    [ResourceEntry("NoSelectedEvent", Description = "phrase: No selected event", LastModified = "2013/04/15", Value = "No selected event")]
    public string NoSelectedEvent => this[nameof (NoSelectedEvent)];

    /// <summary>phrase: Narrow event title</summary>
    /// <value>Narrow event title</value>
    [ResourceEntry("NarrowEventTitle", Description = "phrase: Narrow event title", LastModified = "2013/04/15", Value = "Narrow event title")]
    public string NarrowEventTitle => this[nameof (NarrowEventTitle)];

    /// <summary>phrase: Hello,</summary>
    [ResourceEntry("Greeting", Description = "phrase: Hello,", LastModified = "2013/05/21", Value = "Hello,")]
    public string Greeting => this[nameof (Greeting)];

    /// <summary>phrase: day</summary>
    /// <value>Day</value>
    [ResourceEntry("EveryDay", Description = "phrase: Day", LastModified = "2013/05/13", Value = "day")]
    public string EveryDay => this[nameof (EveryDay)];

    /// <summary>word: Navigation</summary>
    [ResourceEntry("Navigation", Description = "word: Navigation", LastModified = "2013/05/27", Value = "Navigation")]
    public string Navigation => this[nameof (Navigation)];

    /// <summary>phrase: Navigation - mobile</summary>
    [ResourceEntry("NavigationMobile", Description = "word: Navigation - mobile", LastModified = "2013/06/19", Value = "Navigation - mobile")]
    public string NavigationMobile => this[nameof (NavigationMobile)];

    /// <summary>word: Legend</summary>
    [ResourceEntry("Legend", Description = "word: Legend", LastModified = "2013/06/05", Value = "Legend")]
    public string Legend => this[nameof (Legend)];

    /// <summary>Phrase: Legend for the default templates</summary>
    [ResourceEntry("LegendForTheDefaultTemplate", Description = "Phrase: Legend for the default templates", LastModified = "2013/06/06", Value = "Legend for the default templates")]
    public string LegendForTheDefaultTemplate => this[nameof (LegendForTheDefaultTemplate)];

    /// <summary>Phrase: Horizontal (one-level)</summary>
    [ResourceEntry("HorizontalOneLevel", Description = "Phrase: Horizontal (one-level)", LastModified = "2013/06/06", Value = "Horizontal (one-level)")]
    public string HorizontalOneLevel => this[nameof (HorizontalOneLevel)];

    /// <summary>Phrase: Horizontal with dropdown-menus</summary>
    [ResourceEntry("HorizontalWithDropdownMenus", Description = "Phrase: Horizontal with dropdown-menus", LastModified = "2013/06/06", Value = "Horizontal with dropdown-menus")]
    public string HorizontalWithDropdownMenus => this[nameof (HorizontalWithDropdownMenus)];

    /// <summary>Phrase: Horizontal with tabs (up to 2 levers)</summary>
    [ResourceEntry("HorizontalWithTabs", Description = "Phrase: Horizontal with tabs (up to 2 levers)", LastModified = "2013/06/06", Value = "Horizontal with tabs (up to 2 levers)")]
    public string HorizontalWithTabs => this[nameof (HorizontalWithTabs)];

    /// <summary>Phrase: Vertical (one-level)</summary>
    [ResourceEntry("VerticalOneLevel", Description = "Phrase: Vertical (one-level)", LastModified = "2013/06/06", Value = "Vertical (one-level)")]
    public string VerticalOneLevel => this[nameof (VerticalOneLevel)];

    /// <summary>Phrase: Tree (vertical with sub-pages)</summary>
    [ResourceEntry("VerticalWithSubPages", Description = "Phrase: Tree (vertical with sub-pages)", LastModified = "2013/06/06", Value = "Tree (vertical with sub-pages)")]
    public string VerticalWithSubPages => this[nameof (VerticalWithSubPages)];

    /// <summary>Phrase: Sitemaps in columns</summary>
    [ResourceEntry("SitemapsInColumns", Description = "Phrase: Sitemaps in columns", LastModified = "2013/06/06", Value = "Sitemaps in columns")]
    public string SitemapsInColumns => this[nameof (SitemapsInColumns)];

    /// <summary>Phrase: Sitemaps in rows</summary>
    [ResourceEntry("SitemapInRows", Description = "Phrase: Sitemaps in rows", LastModified = "2013/06/06", Value = "Sitemaps in rows")]
    public string SitemapInRows => this[nameof (SitemapInRows)];

    /// <summary>Word: Menu</summary>
    [ResourceEntry("ToggleMenuLabel", Description = "Word: Menu", LastModified = "2013/06/11", Value = "Menu")]
    public string ToggleMenuLabel => this[nameof (ToggleMenuLabel)];

    /// <summary>Word: Created</summary>
    [ResourceEntry("Created", Description = "Word: Created", LastModified = "2013/06/19", Value = "Created")]
    public string Created => this[nameof (Created)];

    /// <summary>Word: Modified</summary>
    [ResourceEntry("Modified", Description = "Word: Modified", LastModified = "2013/06/19", Value = "Modified")]
    public string Modified => this[nameof (Modified)];

    /// <summary>Word: Deleted</summary>
    [ResourceEntry("Deleted", Description = "Word: Deleted", LastModified = "2013/06/19", Value = "Deleted")]
    public string Deleted => this[nameof (Deleted)];

    /// <summary>Word: General</summary>
    [ResourceEntry("General", Description = "Word: General", LastModified = "2013/06/13", Value = "General")]
    public string General => this[nameof (General)];

    /// <summary>Phrase: Render template</summary>
    [ResourceEntry("RenderTemplate", Description = "Phrase: Render template", LastModified = "2013/06/13", Value = "Render template")]
    public string RenderTemplate => this[nameof (RenderTemplate)];

    /// <summary>Phrase: Levels to include</summary>
    [ResourceEntry("LevelsToInclude", Description = "Phrase: Levels to include", LastModified = "2013/06/18", Value = "Levels to include")]
    public string LevelsToInclude => this[nameof (LevelsToInclude)];

    /// <summary>Phrase: only for multi-level templates</summary>
    [ResourceEntry("LevelsToIncludeDescription", Description = "Phrase: only for multi-level templates", LastModified = "2013/06/19", Value = "only for multi-level templates")]
    public string LevelsToIncludeDescription => this[nameof (LevelsToIncludeDescription)];

    /// <summary>Gets the RD title example.</summary>
    [ResourceEntry("RDTitleExample", Description = "Description for the responsive design title", LastModified = "2013/06/20", Value = "<strong>Example:</strong> <em>Toggle menu, Dropdown menu</em>")]
    public string RDTitleExample => this[nameof (RDTitleExample)];

    /// <summary>Gets the transformation CSS.</summary>
    [ResourceEntry("TransformationCss", Description = "TransformationCss", LastModified = "2013/06/20", Value = "Transformation CSS")]
    public string TransformationCss => this[nameof (TransformationCss)];

    /// <summary>Gets the name in code.</summary>
    [ResourceEntry("NameInCode", Description = "For developers: name used in code", LastModified = "2013/06/20", Value = "For developers: name used in code")]
    public string NameInCode => this[nameof (NameInCode)];

    /// <summary>Gets the name in code description.</summary>
    [ResourceEntry("NameInCodeDescription", Description = "Description for NameInCode", LastModified = "2013/06/20", Value = "<strong>Example:</strong> <em>DropdownMenu</em>")]
    public string NameInCodeDescription => this[nameof (NameInCodeDescription)];

    /// <summary>Gets the this transformation is active.</summary>
    [ResourceEntry("ThisTransformationIsActive", Description = "ThisTransformationIsActive", LastModified = "2013/06/20", Value = "This transformation is Active")]
    public string ThisTransformationIsActive => this[nameof (ThisTransformationIsActive)];

    /// <summary>Gets the add responsive design setting dialog title.</summary>
    [ResourceEntry("AddResponsiveDesignSetting", Description = "AddResponsiveDesignSetting title", LastModified = "2013/06/20", Value = "Add responsive design setting")]
    public string AddResponsiveDesignSetting => this[nameof (AddResponsiveDesignSetting)];

    /// <summary>Gets the edit responsive design setting dialog title.</summary>
    [ResourceEntry("EditResponsiveDesignSetting", Description = "EditResponsiveDesignSetting title", LastModified = "2013/06/21", Value = "Edit {0} setting")]
    public string EditResponsiveDesignSetting => this[nameof (EditResponsiveDesignSetting)];

    /// <summary>Gets the title cannot be empty error.</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "phrase: Title cannot be empty.", LastModified = "2010/06/21", Value = "Title cannot be empty.")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>word: Display</summary>
    [ResourceEntry("Display", Description = "Display", LastModified = "2013/06/24", Value = "Display")]
    public string Display => this[nameof (Display)];

    /// <summary>phrase: Responsive template CSS class</summary>
    [ResourceEntry("CssClassLabel", Description = "phrase: CSS class", LastModified = "2013/06/24", Value = "CSS class")]
    public string CssClassLabel => this[nameof (CssClassLabel)];

    /// <summary>
    /// phrase: <strong>Responsive design</strong><p class="sfNote">You can define how navigation is transformed for different screens and devices in Design &gt; Mobile and Responsive design</p>
    /// </summary>
    [ResourceEntry("ResponsiveDesignInfo", Description = "phrase: <strong>Responsive design</strong><p class='sfNote'>You can define how navigation is transformed for diferent screens and devices in Design > Mobile and Responsive design</p>", LastModified = "2013/06/24", Value = "<strong>Responsive design</strong><p class='sfNote'>You can define how navigation is transformed for different screens and devices in Design > Mobile and Responsive design</p>")]
    public string ResponsiveDesignInfo => this[nameof (ResponsiveDesignInfo)];

    /// <summary>
    /// phrase: Example: <i>topnav</i>
    /// </summary>
    [ResourceEntry("CssClassExample", Description = "phrase: Example: <i>topnav</i>", LastModified = "2013/06/24", Value = "Example: <i>topnav</i>")]
    public string CssClassExample => this[nameof (CssClassExample)];

    /// <summary>
    /// Title of the delete navigation transformation settings dialog
    /// </summary>
    [ResourceEntry("DeleteNavTransformationSettingTitle", Description = "Title of the delete navigation transformation settings dialog", LastModified = "2013/06/24", Value = "These settings cannot be deleted")]
    public string DeleteNavTransformationSettingTitle => this[nameof (DeleteNavTransformationSettingTitle)];

    /// <summary>
    /// Message of the delete navigation transformation settings dialog
    /// </summary>
    [ResourceEntry("DeleteNavTransformationSettingMessage", Description = "Message of the delete navigation transformation settings dialog", LastModified = "2013/06/24", Value = "You cannot delete this set because it is used in {0} responsive design rules.<br/><br/>You need to remove it from the rules first.")]
    public string DeleteNavTransformationSettingMessage => this[nameof (DeleteNavTransformationSettingMessage)];

    /// <summary>
    /// Title of the deactivate navigation transformation settings dialog
    /// </summary>
    [ResourceEntry("DeactivateNavTransformationSettingTitle", Description = "Title of the deactivate navigation transformation settings dialog", LastModified = "2013/06/24", Value = "These settings cannot be deactivated")]
    public string DeactivateNavTransformationSettingTitle => this[nameof (DeactivateNavTransformationSettingTitle)];

    /// <summary>
    /// Message of the deactivate navigation transformation settings dialog
    /// </summary>
    [ResourceEntry("DeactivateNavTransformationSettingMessage", Description = "Message of the deactivate navigation transformation settings dialog", LastModified = "2013/06/24", Value = "You cannot deactivate this set because it is used in {0} responsive design rules.<br/><br/>You need to remove it from the rules first.")]
    public string DeactivateNavTransformationSettingMessage => this[nameof (DeactivateNavTransformationSettingMessage)];

    /// <summary>Text of Regenerate button</summary>
    [ResourceEntry("Regenerate", Description = "Text of Regenerate button", LastModified = "2013/06/24", Value = "Regenerate")]
    public string Regenerate => this[nameof (Regenerate)];

    /// <summary>Message of proceed editing Name dialog</summary>
    [ResourceEntry("ProceedEditConfirmationDialogMessage", Description = "Message of proceed editing Name dialog", LastModified = "2013/06/24", Value = "These settings are used in {0} responsive rules.<br/>Changes you have made  will be reflected in all rules.<br/><br/>Are you sure you want to proceed?")]
    public string ProceedEditConfirmationDialogMessage => this[nameof (ProceedEditConfirmationDialogMessage)];

    /// <summary>The default text of the css field</summary>
    [ResourceEntry("DefaultStyleText", Description = "The default text of the css field", LastModified = "2013/06/25", Value = "type class(es)")]
    public string DefaultStyleText => this[nameof (DefaultStyleText)];

    /// <summary>phrase: Fit to area.</summary>
    /// <value>Fit to area</value>
    [ResourceEntry("FitToArea", Description = "phrase: Fit to area.", LastModified = "2013/05/30", Value = "Fit to area")]
    public string FitToArea => this[nameof (FitToArea)];

    /// <summary>
    /// phrase: Generated image will be resized to desired area.
    /// </summary>
    /// <value>Generated image will be resized to desired area</value>
    [ResourceEntry("FitToAreaDescription", Description = "phrase: Generated image will be resized to desired area.", LastModified = "2013/05/30", Value = "Generated image will be resized to desired area")]
    public string FitToAreaDescription => this[nameof (FitToAreaDescription)];

    /// <summary>Phrase: Crop to area</summary>
    /// <value>Crop to area</value>
    [ResourceEntry("CropToArea", Description = "Phrase: Crop to area", LastModified = "2013/05/30", Value = "Crop to area")]
    public string CropToArea => this[nameof (CropToArea)];

    /// <summary>
    /// Phrase: Generated image will be resized and cropped to desired area
    /// </summary>
    /// <value>Generated image will be resized and cropped to desired area</value>
    [ResourceEntry("CropToAreaDescription", Description = "Phrase: Generated image will be resized and cropped to desired area", LastModified = "2013/05/30", Value = "Generated image will be resized and cropped to desired area")]
    public string CropToAreaDescription => this[nameof (CropToAreaDescription)];

    /// <summary>Word: Original</summary>
    /// <value>Original</value>
    [ResourceEntry("Original", Description = "Word: Original", LastModified = "2013/05/30", Value = "Original")]
    public string Original => this[nameof (Original)];

    /// <summary>Word: Resized</summary>
    /// <value>Resized</value>
    [ResourceEntry("Resized", Description = "Word: Resized", LastModified = "2013/05/30", Value = "Resized")]
    public string Resized => this[nameof (Resized)];

    /// <summary>
    /// The text to be shown when a user action is to be confirmed.
    /// </summary>
    /// <value>Are you sure you want to proceed?</value>
    [ResourceEntry("ProceedWarningMessage", Description = "The text to be shown when a user action is to be confirmed.", LastModified = "2013/06/12", Value = "Are you sure you want to proceed?")]
    public string ProceedWarningMessage => this[nameof (ProceedWarningMessage)];

    /// <summary>
    /// The text to be shown when a user action is to be confirmed.
    /// </summary>
    [ResourceEntry("DuplicateSetting", Description = "The text to be shown when a user action is to be confirmed.", LastModified = "2013/07/01", Value = "There is another setting with the same name. Please rename it and then try to save it again.")]
    public string DuplicateSetting => this[nameof (DuplicateSetting)];

    /// <summary>Phrase: modified on</summary>
    /// <value>Modified on</value>
    [ResourceEntry("ModifiedOn", Description = "Phrase: modified on", LastModified = "2013/07/02", Value = "Modified on")]
    public string ModifiedOn => this[nameof (ModifiedOn)];

    /// <summary>word: by</summary>
    /// <value>by</value>
    [ResourceEntry("WordBy", Description = "word: by", LastModified = "2013/07/02", Value = "by")]
    public string WordBy => this[nameof (WordBy)];

    /// <summary>
    /// The text of the 'Awaiting approval' button in the master grid view sidebar.
    /// </summary>
    /// <value>Awaiting approval</value>
    [ResourceEntry("WaitingForApproval", Description = "The text of the 'Awaiting approval' button in the master grid view sidebar.", LastModified = "2013/07/16", Value = "Awaiting approval")]
    public string WaitingForApproval => this[nameof (WaitingForApproval)];

    /// <summary>
    /// The text of the 'Awaiting Publishing' button in the master grid view sidebar.
    /// </summary>
    /// <value>Awaiting Publishing</value>
    [ResourceEntry("WaitingForPublishing", Description = "The text of the 'Awaiting Publishing' button in the master grid view sidebar.", LastModified = "2018/08/16", Value = "Awaiting publishing")]
    public string WaitingForPublishing => this[nameof (WaitingForPublishing)];

    /// <summary>
    /// The text of the 'Awaiting Review' button in the master grid view sidebar.
    /// </summary>
    /// <value>Awaiting Review</value>
    [ResourceEntry("WaitingForReview", Description = "The text of the 'Awaiting Review' button in the master grid view sidebar.", LastModified = "2018/08/16", Value = "Awaiting review")]
    public string WaitingForReview => this[nameof (WaitingForReview)];

    /// <summary>phrase: A few seconds ago</summary>
    /// <value>A few seconds ago</value>
    [ResourceEntry("AFewSecondsAgo", Description = "phrase: A few seconds ago", LastModified = "2013/09/02", Value = "A few seconds ago")]
    public string AFewSecondsAgo => this[nameof (AFewSecondsAgo)];

    /// <summary>word: Yesterday</summary>
    /// <value>Yesterday</value>
    [ResourceEntry("Yesterday", Description = "word: Yesterday", LastModified = "2013/09/02", Value = "Yesterday")]
    public string Yesterday => this[nameof (Yesterday)];

    /// <summary>
    /// phrase: Are you sure you want to delete {0} field and all its data?
    /// </summary>
    /// <value>Are you sure you want to delete {0} field and all its data?</value>
    [ResourceEntry("ConfirmFieldDeleteMessage", Description = "phrase: Are you sure you want to delete {0} field and all its data?", LastModified = "2013/10/29", Value = "Are you sure you want to delete {0} field and all its data?")]
    public string ConfirmFieldDeleteMessage => this[nameof (ConfirmFieldDeleteMessage)];

    /// <summary>phrase: Delete this field and all its data</summary>
    [ResourceEntry("DeleteFieldLabel", Description = "phrase: Delete this field and all its data", LastModified = "2013/10/29", Value = "Delete this field and all its data")]
    public string DeleteFieldLabel => this[nameof (DeleteFieldLabel)];

    /// <summary>phrase: Built-in custom fields cannot be deleted.</summary>
    [ResourceEntry("BuitInFieldsDeleteMessage", Description = "phrase: Built-in custom fields cannot be deleted.", LastModified = "2013/10/29", Value = "Built-in custom fields cannot be deleted.")]
    public string BuitInFieldsDeleteMessage => this[nameof (BuitInFieldsDeleteMessage)];

    /// <summary>
    /// Tooltip: Please make sure you have generated the selected size in the libraries which contain the images displayed by this widget.
    /// </summary>
    /// <value>Please make sure you have generated the selected size in the libraries which contain the images displayed by this widget</value>
    [ResourceEntry("MakeSureThumbnailsExistToolTip", Description = "Tooltip: Please make sure you have generated the selected size in the libraries which contain the images displayed by this widget.", LastModified = "2013/10/25", Value = "Please make sure you have generated the selected size in the libraries which contain the images displayed by this widget")]
    public string MakeSureThumbnailsExistToolTip => this[nameof (MakeSureThumbnailsExistToolTip)];

    /// <summary>
    /// Label that appears on a checkbox in the bottom of library selectors.
    /// </summary>
    [ResourceEntry("IncludeChildLibraryItems", Description = "Label that appears on a checkbox in the bottom of library selectors.", LastModified = "2013/11/27", Value = "Include items from child libraries, when such exist")]
    public string IncludeChildLibraryItems => this[nameof (IncludeChildLibraryItems)];

    /// <summary>phrase: Share this form with selected sites</summary>
    [ResourceEntry("ShareThisFormWithSelectedSites", Description = "phrase: Share this form with selected sites", LastModified = "2014/01/29", Value = "Share this form with selected sites")]
    public string ShareThisFormWithSelectedSites => this[nameof (ShareThisFormWithSelectedSites)];

    /// <summary>Word: Limitations</summary>
    [ResourceEntry("Limitations", Description = "Word: Limitations", LastModified = "2014/01/24", Value = "Limitations")]
    public string Limitations => this[nameof (Limitations)];

    /// <summary>Label: Min Width</summary>
    [ResourceEntry("MinWidth", Description = "Label: Min Width", LastModified = "2014/01/23", Value = "Min width")]
    public string MinWidth => this[nameof (MinWidth)];

    /// <summary>Label: Min Height</summary>
    [ResourceEntry("MinHeight", Description = "Label: Min Height", LastModified = "2014/01/23", Value = "Min height")]
    public string MinHeight => this[nameof (MinHeight)];

    /// <summary>Label: Max Width</summary>
    [ResourceEntry("MaxWidth", Description = "Label: Max Width", LastModified = "2014/01/23", Value = "Max width")]
    public string MaxWidth => this[nameof (MaxWidth)];

    /// <summary>Label: Max Height</summary>
    [ResourceEntry("MaxHeight", Description = "Label: Max Height", LastModified = "2014/01/23", Value = "Max height")]
    public string MaxHeight => this[nameof (MaxHeight)];

    /// <summary>phrase: Max file size can be uploaded</summary>
    [ResourceEntry("UploadedFileMaxSize", Description = "phrase: Max file size can be uploaded ", LastModified = "2014/01/24", Value = "Max file size can be uploaded")]
    public string UploadedFileMaxSize => this[nameof (UploadedFileMaxSize)];

    /// <summary>phrase: Max file size can be uploaded</summary>
    [ResourceEntry("CheckRelatingDataMessageSingle", Description = "phrase: Delete only if there are no linked items", LastModified = "2014/03/20", Value = "Delete only if there are no linking items")]
    public string CheckRelatingDataMessageSingle => this[nameof (CheckRelatingDataMessageSingle)];

    /// <summary>phrase: Max file size can be uploaded</summary>
    [ResourceEntry("CheckRelatingDataMessageMultiple", Description = "phrase:  Delete only if there are no linked items", LastModified = "2014/03/20", Value = "Delete only if there are no linking items")]
    public string CheckRelatingDataMessageMultiple => this[nameof (CheckRelatingDataMessageMultiple)];

    /// <summary>
    /// phrase : Cannot be deleted becauce they are related to another items
    /// </summary>
    /// <value>Cannot be deleted becauce they are related to another items</value>
    [ResourceEntry("CheckRelatingDataPreconditionsFailed", Description = "phrase : Cannot be deleted because there are items linking to it", LastModified = "2014/03/19", Value = "Cannot be deleted because there are items linking to it")]
    public string CheckRelatingDataPreconditionsFailed => this[nameof (CheckRelatingDataPreconditionsFailed)];

    /// <summary>
    /// Gets a phrase : Are you sure you want to delete this item?
    /// </summary>
    /// <value>Are you sure you want to delete this item?</value>
    [ResourceEntry("SendToRecycleBinSingleConfirmationMessage", Description = "phrase : Are you sure you want to delete this item?", LastModified = "2018/03/23", Value = "Are you sure you want to delete this item?")]
    public string SendToRecycleBinSingleConfirmationMessage => this[nameof (SendToRecycleBinSingleConfirmationMessage)];

    /// <summary>
    /// Gets a phrase : Are you sure you want to move these items to the Recycle Bin?
    /// </summary>
    /// <value>Are you sure you want to move these items to the Recycle Bin?</value>
    [ResourceEntry("SendToRecycleBinMultipleConfirmationMessage", Description = "phrase : Are you sure you want to move these items to the Recycle Bin?", LastModified = "2014/06/04", Value = "Are you sure you want to move {0} items to the Recycle Bin?")]
    public string SendToRecycleBinMultipleConfirmationMessage => this[nameof (SendToRecycleBinMultipleConfirmationMessage)];

    /// <summary>Gets a Word: Undo</summary>
    /// <value>Undo</value>
    [ResourceEntry("UndoLabel", Description = "Word: Undo", LastModified = "2014/07/04", Value = "Undo")]
    public string UndoLabel => this[nameof (UndoLabel)];

    /// <summary>Phrase: The item has been moved to the Recycle Bin</summary>
    /// <value>The item has been moved to the Recycle Bin</value>
    [ResourceEntry("UndoDeleteMessageSingle", Description = "Phrase: The item has been moved to the Recycle Bin", LastModified = "2014/07/04", Value = "The item has been moved to the Recycle Bin")]
    public string UndoDeleteMessageSingle => this[nameof (UndoDeleteMessageSingle)];

    /// <summary>Phrase: The items have been moved to the Recycle Bin</summary>
    /// <value>The items have been moved to the Recycle Bin</value>
    [ResourceEntry("UndoDeleteMessagePlural", Description = "Phrase: The items have been moved to the Recycle Bin", LastModified = "2014/07/04", Value = "The items have been moved to the Recycle Bin")]
    public string UndoDeleteMessagePlural => this[nameof (UndoDeleteMessagePlural)];

    /// <summary>Word: Restore</summary>
    /// <value>Restore</value>
    [ResourceEntry("Restore", Description = " Word: Restore", LastModified = "2014/07/09", Value = "Restore")]
    public string Restore => this[nameof (Restore)];

    /// <summary>Phrase: Delete forever</summary>
    /// <value>Delete forever</value>
    [ResourceEntry("DeleteForever", Description = " Phrase: Delete forever", LastModified = "2014/07/09", Value = "Delete forever")]
    public string DeleteForever => this[nameof (DeleteForever)];

    /// <summary>Phrase: Delete Selected</summary>
    /// <value>Delete Selected</value>
    [ResourceEntry("DeleteSelected", Description = " Phrase: Delete Selected", LastModified = "2014/07/09", Value = "Delete Selected")]
    public string DeleteSelected => this[nameof (DeleteSelected)];

    /// <summary>Phrase: Restore Selected</summary>
    /// <value>Restore Selected</value>
    [ResourceEntry("RestoreSelected", Description = " Phrase: Restore Selected", LastModified = "2014/07/09", Value = "Restore Selected")]
    public string RestoreSelected => this[nameof (RestoreSelected)];

    /// <summary>Phrase: Yes, Move to the Recycle Bin</summary>
    /// <value>Yes, Move to the Recycle Bin</value>
    [ResourceEntry("YesMoveToRecycleBin", Description = "Phrase: Yes, Move to the Recycle Bin", LastModified = "2014/07/17", Value = "Yes, Move to the Recycle Bin")]
    public string YesMoveToRecycleBin => this[nameof (YesMoveToRecycleBin)];

    /// <summary>Phrase: Recycle bin</summary>
    /// <value>Recycle bin</value>
    [ResourceEntry("RecycleBin", Description = "Recycle bin", LastModified = "2019/03/12", Value = "Recycle bin")]
    public string RecycleBin => this[nameof (RecycleBin)];

    /// <summary>Phrase: Move all translations</summary>
    /// <value>Move all translations</value>
    [ResourceEntry("MoveAllTranslations", Description = "Phrase: Move all translations to the Recycle Bin", LastModified = "2018/03/23", Value = "Move all translations to the Recycle Bin")]
    public string MoveAllTranslations => this[nameof (MoveAllTranslations)];

    /// <summary>phrase: The username or password is incorrect</summary>
    [ResourceEntry("TfisErrorUnauthorized", Description = "phrase: The username or password is incorrect", LastModified = "2014/12/10", Value = "The username or password is incorrect")]
    public string TfisErrorUnauthorized => this[nameof (TfisErrorUnauthorized)];

    /// <summary>Change Change secret quetion</summary>
    /// <value>Change secret quetion</value>
    [ResourceEntry("ChangeQuestionAndAnswer", Description = "Change secret quetion", LastModified = "2016/12/23", Value = "Change secret quetion")]
    public string ChangeQuestionAndAnswer => this[nameof (ChangeQuestionAndAnswer)];

    /// <summary>
    /// phrase: To reset your password, enter your password answer
    /// </summary>
    /// <value>To reset your password, enter your password answer</value>
    [ResourceEntry("ToResetYourPasswordEnterYourSecurityAnswer", Description = "phrase: To reset your password, enter your password answer", LastModified = "2015/03/18", Value = "To reset your password, enter your password answer")]
    public string ToResetYourPasswordEnterYourSecurityAnswer => this[nameof (ToResetYourPasswordEnterYourSecurityAnswer)];

    /// <summary>Gets the export to XLIFF.</summary>
    /// <value>The export to XLIFF.</value>
    [ResourceEntry("ExportToXliff", Description = "phrase: Export to XLIFF", LastModified = "2015/03/24", Value = "Export to XLIFF")]
    public string ExportToXliff => this[nameof (ExportToXliff)];

    /// <summary>Gets the send for translation.</summary>
    /// <value>The send for translation.</value>
    [ResourceEntry("SendForTranslation", Description = "phrase: Send for translation", LastModified = "2015/03/24", Value = "Send for translation")]
    public string SendForTranslation => this[nameof (SendForTranslation)];

    /// <summary>Gets the unmark for translation.</summary>
    /// <value>The unmark for translation.</value>
    [ResourceEntry("UnmarkForTranslation", Description = "phrase: Unmark for translation", LastModified = "2015/03/24", Value = "Unmark for translation")]
    public string UnmarkForTranslation => this[nameof (UnmarkForTranslation)];

    /// <summary>Gets the translate.</summary>
    /// <value>The translate.</value>
    [ResourceEntry("Translate", Description = "word: Translate", LastModified = "2015/03/24", Value = "Translate")]
    public string Translate => this[nameof (Translate)];

    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    [ResourceEntry("ContentType", Description = "phrase: Content type", LastModified = "2015/03/24", Value = "Content type")]
    public string ContentType => this[nameof (ContentType)];

    /// <summary>Label: Custom</summary>
    /// <value>Custom</value>
    [ResourceEntry("ScheduleCustomDate", Description = "Label: Custom", LastModified = "2015/03/26", Value = "Custom")]
    public string ScheduleCustomDate => this[nameof (ScheduleCustomDate)];

    /// <summary>phrase: Load more...</summary>
    [ResourceEntry("LoadMoreLabel", Description = "phrase: Load more...", LastModified = "2015/03/24", Value = "Load more...")]
    public string LoadMoreLabel => this[nameof (LoadMoreLabel)];

    /// <summary>Label: There was an error parsing the template.</summary>
    /// <value>There was an error parsing the template.</value>
    [ResourceEntry("WrongCronPattern", Description = "Label: There was an error parsing the template.", LastModified = "2015/04/01", Value = "There was an error parsing the template.")]
    public string WrongCronPattern => this[nameof (WrongCronPattern)];

    /// <summary>label: BasicAndAdvancedSettings</summary>
    /// <value>Basic and advanced settings</value>
    [ResourceEntry("BasicAndAdvancedSettings", Description = "label: Basic and advanced settings", LastModified = "2018/11/20", Value = "Basic and advanced settings")]
    public string BasicAndAdvancedSettings => this[nameof (BasicAndAdvancedSettings)];

    /// <summary>Gets the project.</summary>
    /// <value>The project.</value>
    [ResourceEntry("Project", Description = "label: Project", LastModified = "2015/04/16", Value = "Project")]
    public string Project => this[nameof (Project)];

    /// <summary>Gets the start date.</summary>
    /// <value>The start date.</value>
    [ResourceEntry("StartDate", Description = "label: Start date", LastModified = "2015/04/16", Value = "Start date")]
    public string StartDate => this[nameof (StartDate)];

    /// <summary>Gets the label: Stop date.</summary>
    /// <value>End date.</value>
    [ResourceEntry("StopDate", Description = "label: Stop date", LastModified = "2017/08/16", Value = "Stop date")]
    public string StopDate => this[nameof (StopDate)];

    /// <summary>Gets the due date.</summary>
    /// <value>The due date.</value>
    [ResourceEntry("DueDate", Description = "label: Due date", LastModified = "2015/04/16", Value = "Due date")]
    public string DueDate => this[nameof (DueDate)];

    /// <summary>Gets the sent to.</summary>
    /// <value>The sent to.</value>
    [ResourceEntry("SentTo", Description = "label: Sent to", LastModified = "2015/04/16", Value = "Sent to")]
    public string SentTo => this[nameof (SentTo)];

    /// <summary>Label: SMTP server:</summary>
    /// <value>SMTP Server:</value>
    [ResourceEntry("SendGridHost", Description = "Label: SMTP server:", LastModified = "2015/07/01", Value = "SMTP Server:")]
    public string SendGridHost => this[nameof (SendGridHost)];

    /// <summary>Label: Port:</summary>
    /// <value>Port:</value>
    [ResourceEntry("Port", Description = "Label: Port:", LastModified = "2015/06/29", Value = "Port:")]
    public string Port => this[nameof (Port)];

    /// <summary>Label: Username:</summary>
    /// <value>Username:</value>
    [ResourceEntry("SendGridUserName", Description = "Label: Username:", LastModified = "2015/06/29", Value = "Username:")]
    public string SendGridUserName => this[nameof (SendGridUserName)];

    /// <summary>Label: Password:</summary>
    /// <value>Password:</value>
    [ResourceEntry("SendGridPassword", Description = "Label: Password:", LastModified = "2015/06/29", Value = "Password:")]
    public string SendGridPassword => this[nameof (SendGridPassword)];

    /// <summary>Label: Host:</summary>
    /// <value>Host:</value>
    [ResourceEntry("MandrillHost", Description = "Label: Host:", LastModified = "2015/06/29", Value = "Host:")]
    public string MandrillHost => this[nameof (MandrillHost)];

    /// <summary>Label: SMTP Username</summary>
    /// <value>SMTP Username:</value>
    [ResourceEntry("MandrillUserName", Description = "Label: SMTP Username", LastModified = "2015/06/29", Value = "SMTP Username:")]
    public string MandrillUserName => this[nameof (MandrillUserName)];

    /// <summary>Label: SMTP Password (API Key):</summary>
    /// <value>SMTP Password (API Key):</value>
    [ResourceEntry("MandrillPassword", Description = "Label: SMTP Password (API Key):", LastModified = "2015/06/29", Value = "SMTP Password (API Key):")]
    public string MandrillPassword => this[nameof (MandrillPassword)];

    /// <summary>Label: SMTP Hostname:</summary>
    /// <value>SMTP Hostname:</value>
    [ResourceEntry("MailGunHost", Description = "Label: SMTP Hostname:", LastModified = "2015/06/29", Value = "SMTP Hostname:")]
    public string MailGunHost => this[nameof (MailGunHost)];

    /// <summary>Label: Default SMTP Login:</summary>
    /// <value>Default SMTP Login:</value>
    [ResourceEntry("MailGunUserName", Description = "Label: Default SMTP Login:", LastModified = "2015/06/29", Value = "Default SMTP Login:")]
    public string MailGunUserName => this[nameof (MailGunUserName)];

    /// <summary>Label: Default Password:</summary>
    /// <value>Default Password:</value>
    [ResourceEntry("MailGunPassword", Description = "Label: Default Password:", LastModified = "2015/06/29", Value = "Default Password:")]
    public string MailGunPassword => this[nameof (MailGunPassword)];

    /// <summary>phrase: Sender Email address</summary>
    /// <value>Sender Email address</value>
    [ResourceEntry("SenderEmailAddress", Description = "phrase: Sender Email address", LastModified = "2015/06/30", Value = "Sender Email address")]
    public string SenderEmailAddress => this[nameof (SenderEmailAddress)];

    /// <summary>Gets phrase: The item has been locked by {0}{1}.</summary>
    /// <value>Sender Email address</value>
    [ResourceEntry("ItemIsLockedFormat", Description = "phrase: The item has been locked by {0}{1}.", LastModified = "2015/08/12", Value = "The item has been locked by {0}{1}.")]
    public string ItemIsLockedFormat => this[nameof (ItemIsLockedFormat)];

    /// <summary>Gets phrase: Do you want to unlock it?</summary>
    /// <value>Sender Email address</value>
    [ResourceEntry("ItemUnlockQuestion", Description = "phrase: Do you want to unlock it?", LastModified = "2015/08/12", Value = "Do you want to unlock it?")]
    public string ItemUnlockQuestion => this[nameof (ItemUnlockQuestion)];

    /// <summary>Gets phrase: Press OK to close the dialog.</summary>
    /// <value>Sender Email address</value>
    [ResourceEntry("ItemUnlockCloseAlert", Description = "phrase: Press OK to close the dialog.", LastModified = "2015/08/12", Value = "Press OK to close the dialog.")]
    public string ItemUnlockCloseAlert => this[nameof (ItemUnlockCloseAlert)];

    /// <summary>
    /// Gets phrase: Default value. Used in advanced settings view.
    /// </summary>
    /// <value>Default value</value>
    [ResourceEntry("ConfigItemSaveLocationDefaultValue", Description = "phrase: Default value", LastModified = "2016/07/08", Value = "Default values")]
    public string ConfigItemSaveLocationDefaultValue => this[nameof (ConfigItemSaveLocationDefaultValue)];

    /// <summary>Gets phrase: Saved on the file system</summary>
    /// <value>Saved on the file system</value>
    [ResourceEntry("ConfigItemSaveLocationFileSystem", Description = "phrase: Saved on the file system", LastModified = "2016/07/08", Value = "Saved on the file system")]
    public string ConfigItemSaveLocationFileSystem => this[nameof (ConfigItemSaveLocationFileSystem)];

    /// <summary>Gets phrase: Saved in the database</summary>
    /// <value>Saved in the database</value>
    [ResourceEntry("ConfigItemSaveLocationDatabase", Description = "phrase: Saved in the database", LastModified = "2016/07/08", Value = "Saved in the database")]
    public string ConfigItemSaveLocationDatabase => this[nameof (ConfigItemSaveLocationDatabase)];

    /// <summary>
    /// Gets phrase: Default value. Used in advanced settings view.
    /// </summary>
    /// <value>Default value</value>
    [ResourceEntry("ConfigItemSaveLocationDefaultValueInCollection", Description = "phrase: Default", LastModified = "2016/07/08", Value = "Default")]
    public string ConfigItemSaveLocationDefaultValueInCollection => this[nameof (ConfigItemSaveLocationDefaultValueInCollection)];

    /// <summary>Gets phrase: Saved on the file system</summary>
    /// <value>Saved on the file system</value>
    [ResourceEntry("ConfigItemSaveLocationFileSystemInCollection", Description = "phrase: File system", LastModified = "2016/07/08", Value = "File system")]
    public string ConfigItemSaveLocationFileSystemInCollectionInCollection => this["ConfigItemSaveLocationFileSystemInCollection"];

    /// <summary>Gets phrase: Saved in the database</summary>
    /// <value>Saved in the database</value>
    [ResourceEntry("ConfigItemSaveLocationDatabaseInCollection", Description = "phrase: Database", LastModified = "2016/07/08", Value = "Database")]
    public string ConfigItemSaveLocationDatabaseInCollection => this[nameof (ConfigItemSaveLocationDatabaseInCollection)];

    /// <summary>Gets phrase: Some default values are modified</summary>
    /// <value>Some default values are modified</value>
    [ResourceEntry("ConfigItemSaveLocationModifiedDefault", Description = "phrase: Some default values are modified", LastModified = "2016/10/03", Value = "Some default values are modified")]
    public string ConfigItemSaveLocationModifiedDefault => this[nameof (ConfigItemSaveLocationModifiedDefault)];

    /// <summary>Gets phrase: Default value</summary>
    /// <value>Default value</value>
    [ResourceEntry("DefaultValue", Description = "phrase: Default value", LastModified = "2016/07/22", Value = "Default value")]
    public string DefaultValue => this[nameof (DefaultValue)];

    /// <summary>Gets phrase: modified defaults</summary>
    /// <value>modified defaults</value>
    [ResourceEntry("ModifiedDefaults", Description = "phrase: modified default", LastModified = "2016/07/28", Value = "modified default")]
    public string ModifiedDefaults => this[nameof (ModifiedDefaults)];

    /// <summary>Gets phrase: Error moving item</summary>
    /// <value>Error moving item</value>
    [ResourceEntry("MoveConfigErrorMessage", Description = "phrase: Error moving item", LastModified = "2016/09/01", Value = "Error moving item")]
    public string MoveConfigErrorMessage => this[nameof (MoveConfigErrorMessage)];

    /// <summary>Gets phrase: Yes, move to file system</summary>
    /// <value>Yes, move to file system</value>
    [ResourceEntry("YesMoveToFileSystem", Description = "phrase: Yes, move to file system", LastModified = "2016/09/01", Value = "Yes, move to file system")]
    public string YesMoveToFileSystem => this[nameof (YesMoveToFileSystem)];

    /// <summary>
    /// Gets phrase: <p>You are about to move these settings from database to the file system.</p><p>Once moved the settings cannot be reverted back to the database. This operation will restart the application</p><p>Do you want to continue?</p>
    /// </summary>
    /// <value><p>You are about to move these settings from database to the file system.</p><p>Once moved the settings cannot be reverted back to the database. This operation will restart the application</p><p>Do you want to continue?</p></value>
    [ResourceEntry("MoveItemDescription", Description = "phrase: <p>You are about to move these settings from database to the file system.</p><p>Once moved the settings cannot be reverted back to the database. This operation will restart the application</p><p>Do you want to continue?</p>", LastModified = "2016/09/01", Value = "<p>You are about to move these settings from database to the file system.</p><p>Once moved the settings cannot be reverted back to the database. This operation will restart the application</p><p>Do you want to continue?</p>")]
    public string MoveItemDescription => this[nameof (MoveItemDescription)];

    /// <summary>Gets phrase: Move to File System</summary>
    /// <value>Move to File System</value>
    [ResourceEntry("MoveToFileSystem", Description = "phrase: Move to File System", LastModified = "2016/09/02", Value = "Move to File System")]
    public string MoveToFileSystem => this[nameof (MoveToFileSystem)];

    /// <summary>Gets word: Moving</summary>
    /// <value>Moving</value>
    [ResourceEntry("Moving", Description = "word: Moving", LastModified = "2016/09/02", Value = "Moving")]
    public string Moving => this[nameof (Moving)];

    /// <summary>Label: Registered with {0}</summary>
    /// <value>Registered with {0}</value>
    [ResourceEntry("RegisteredWith", Description = "Label: Registered with {0}", LastModified = "2016/12/19", Value = "Registered with {0}")]
    public string RegisteredWith => this[nameof (RegisteredWith)];

    /// <summary>
    /// Label: To complete the change of your email address, you are required to enter your password
    /// </summary>
    /// <value>To complete the change of your email address, you are required to enter your password</value>
    [ResourceEntry("CompleteChangeOfEmail", Description = "Label: To complete the change of your email address, you are required to enter your password", LastModified = "2016/12/19", Value = "To complete the change of your email address, you are required to enter your password")]
    public string CompleteChangeOfEmail => this[nameof (CompleteChangeOfEmail)];

    /// <summary>label: Try again</summary>
    /// <value>Try again</value>
    [ResourceEntry("TryAgain", Description = "label: Try again", LastModified = "2017/02/01", Value = "Try again")]
    public string TryAgain => this[nameof (TryAgain)];

    /// <summary>label: Login failed</summary>
    /// <value>Login failed</value>
    [ResourceEntry("LoginFailed", Description = "label: Login failed", LastModified = "2017/02/01", Value = "Login failed")]
    public string LoginFailed => this[nameof (LoginFailed)];

    /// <summary>label: Please try again or contact your administrator</summary>
    /// <value>Please try again or contact your administrator</value>
    [ResourceEntry("PleaseTryAgainOrContactYourAdministrator", Description = "label: Please try again or contact your administrator", LastModified = "2017/02/01", Value = "Please try again or contact your administrator")]
    public string PleaseTryAgainOrContactYourAdministrator => this[nameof (PleaseTryAgainOrContactYourAdministrator)];

    /// <summary>label: Until</summary>
    /// <value>Until</value>
    [ResourceEntry("Until", Description = "label: Until", LastModified = "2017/02/08", Value = "Until")]
    public string Until => this[nameof (Until)];

    /// <summary>label: Resend</summary>
    /// <value>Resend</value>
    [ResourceEntry("Resend", Description = "label: Resend", LastModified = "2017/02/08", Value = "Resend")]
    public string Resend => this[nameof (Resend)];

    /// <summary>label: Connect</summary>
    /// <value>Connect</value>
    [ResourceEntry("Connect", Description = "label: Connect", LastModified = "2017/05/16", Value = "Connect")]
    public string Connect => this[nameof (Connect)];

    /// <summary>label: Disconnect</summary>
    /// <value>Disconnect</value>
    [ResourceEntry("Disconnect", Description = "label: Disconnect", LastModified = "2017/05/16", Value = "Disconnect")]
    public string Disconnect => this[nameof (Disconnect)];

    /// <summary>word: Username</summary>
    /// <value>Username</value>
    [ResourceEntry("WordUsername", Description = "word: Username", LastModified = "2017/05/16", Value = "Username")]
    public string WordUsername => this[nameof (WordUsername)];

    /// <summary>word: Connection</summary>
    /// <value>Connection</value>
    [ResourceEntry("Connection", Description = "word: Connection", LastModified = "2017/05/16", Value = "Connection")]
    public string Connection => this[nameof (Connection)];

    /// <summary>label: Change connection</summary>
    /// <value>Change connection</value>
    [ResourceEntry("ChangeConnection", Description = "label: Change connection", LastModified = "2017/05/16", Value = "Change connection")]
    public string ChangeConnection => this[nameof (ChangeConnection)];

    /// <summary>label: Unable to connect. Check your credentials.</summary>
    /// <value>Unable to connect. Check your credentials.</value>
    [ResourceEntry("UnableToConnectCheckYourCredentials", Description = "label: Unable to connect. Check your credentials.", LastModified = "2017/05/26", Value = "Unable to connect. Check your credentials.")]
    public string UnableToConnectCheckYourCredentials => this[nameof (UnableToConnectCheckYourCredentials)];

    /// <summary>label: No categories have been created yet.</summary>
    /// <value>No categories have been created yet.</value>
    [ResourceEntry("NoCategoriesCreated", Description = "label: No categories have been created yet.", LastModified = "2017/05/19", Value = "No categories have been created yet.")]
    public string NoCategoriesCreated => this[nameof (NoCategoriesCreated)];

    /// <summary>Phrase: Add goal</summary>
    /// <value>Add goal</value>
    [ResourceEntry("AddGoal", Description = "Phrase: Add goal", LastModified = "2017/08/08", Value = "Add goal")]
    public string AddGoal => this[nameof (AddGoal)];

    /// <summary>Phrase: Select goal type</summary>
    /// <value>Select goal type</value>
    [ResourceEntry("SelectGoalType", Description = "Phrase: Select goal type", LastModified = "2017/08/08", Value = "Select goal type")]
    public string SelectGoalType => this[nameof (SelectGoalType)];

    /// <summary>
    /// Phrase: Form submission will track any Sitefinity form on your page variations
    /// </summary>
    /// <value>Form submission tracks any Sitefinity forms on page variations.</value>
    [ResourceEntry("FormSubmissionTrackingDescription", Description = "Phrase: Form submission will track any Sitefinity form on your page variations", LastModified = "2017/08/30", Value = "Form submission tracks any Sitefinity forms on page variations.")]
    public string FormSubmissionTrackingDescription => this[nameof (FormSubmissionTrackingDescription)];

    /// <summary>
    /// Gets External Link: No conversions available Sitefinity Insight.
    /// </summary>
    [ResourceEntry("ExternalLinkNoConversionAvailable", Description = "External Link: No conversions available Sitefinity Insight", LastModified = "2020/03/12", Value = "https://www.progress.com/documentation/sitefinity-cms/dec/define-and-track-conversions")]
    public string ExternalLinkNoConversionAvailable => this[nameof (ExternalLinkNoConversionAvailable)];

    /// <summary>
    /// Phrase: No conversions available. You first need to define a conversion in Sitefinity Insight.
    /// </summary>
    /// <value>No conversions available. You first need to define a conversion in Sitefinity Insight.</value>
    [ResourceEntry("NoConversionAvailable", Description = "Phrase: No conversions available. How to define Sitefinity Insight conversions for A/B test.", LastModified = "2020/03/11", Value = "No conversions available.<br/><a href='{0}' target='_blank'>How to define Sitefinity Insight conversions for A/B test</a>")]
    public string NoConversionAvailable => this[nameof (NoConversionAvailable)];

    /// <summary>label: Type part of page URL...</summary>
    /// <value>A/B test</value>
    [ResourceEntry("PartOfPageUrlPlaceholder", Description = "label: Type part of page URL...", LastModified = "2017/08/15", Value = "Type part of page URL...")]
    public string PartOfPageUrlPlaceholder => this[nameof (PartOfPageUrlPlaceholder)];

    /// <summary>Phrase: Set as primary</summary>
    /// <value>Set as primary</value>
    [ResourceEntry("SetAsPrimary", Description = "Phrase: Set as primary", LastModified = "2017/08/08", Value = "Set as primary")]
    public string SetAsPrimary => this[nameof (SetAsPrimary)];

    /// <summary>Phrase: Primary</summary>
    /// <value>Primary</value>
    [ResourceEntry("Primary", Description = "Phrase: Primary", LastModified = "2017/08/08", Value = "Primary")]
    public string Primary => this[nameof (Primary)];

    /// <summary>Variation name label</summary>
    /// <value>Name</value>
    [ResourceEntry("VariationNameLabel", Description = "Variation name label", LastModified = "2017/08/08", Value = "Name")]
    public string VariationNameLabel => this[nameof (VariationNameLabel)];

    /// <summary>Phrase: Traffic distribution</summary>
    /// <value>Traffic distribution</value>
    [ResourceEntry("TrafficDistribution", Description = "Phrase: Traffic distribution", LastModified = "2017/08/08", Value = "Traffic distribution")]
    public string TrafficDistribution => this[nameof (TrafficDistribution)];

    /// <summary>Phrase: Add variation</summary>
    /// <value>Add variation</value>
    [ResourceEntry("AddVariation", Description = "Phrase: Add variation", LastModified = "2017/08/08", Value = "Add variation")]
    public string AddVariation => this[nameof (AddVariation)];

    /// <summary>Gets the variation names required validation message.</summary>
    /// <value>Enter names for all page variations</value>
    [ResourceEntry("VariationNamesRequiredValidationMessage", Description = "Please fill the names of all variations validation message.", LastModified = "2017/08/30", Value = "Enter names for all page variations")]
    public string VariationNamesRequiredValidationMessage => this[nameof (VariationNamesRequiredValidationMessage)];

    /// <summary>Variation name max length message</summary>
    /// <value>Variation names cannot be longer than 255 characters</value>
    [ResourceEntry("VariationNamesLengthValidationMessage", Description = "Variation name max length message", LastModified = "2017/12/01", Value = "Variation names cannot be longer than 255 characters")]
    public string VariationNamesLengthValidationMessage => this[nameof (VariationNamesLengthValidationMessage)];

    /// <summary>
    /// Gets the variation names duplicate validation message.
    /// </summary>
    /// <value>Duplicate page variation name: {0}</value>
    [ResourceEntry("DuplicateVariationName", Description = "Duplicate variation name validation message.", LastModified = "2017/08/30", Value = "Duplicate page variation name: {0}")]
    public string DuplicateVariationName => this[nameof (DuplicateVariationName)];

    /// <summary>Gets variations count validation message.</summary>
    /// <value>Page variations must be more than {0} and less than {1}</value>
    [ResourceEntry("VariationsCountValidationMessage", Description = "Variations count validation message.", LastModified = "2017/08/30", Value = "Page variations must be more than {0} and less than {1}")]
    public string VariationsCountValidationMessage => this[nameof (VariationsCountValidationMessage)];

    /// <summary>
    /// Gets the variation distribution range validation message.
    /// </summary>
    /// <value>Traffic per variation must be between 0 and 100%</value>
    [ResourceEntry("VariationDistributionRangeValidationMessage", Description = "Variation distribution must be greater than 0 and less than 100 validation message.", LastModified = "2017/08/17", Value = "Traffic per variation must be between 0 and 100%")]
    public string VariationDistributionRangeValidationMessage => this[nameof (VariationDistributionRangeValidationMessage)];

    /// <summary>
    /// Gets the variation distributions sum validation message.
    /// </summary>
    /// <value>The sum of variations traffic must equal 100%</value>
    [ResourceEntry("VariationDistributionsSumValidationMessage", Description = "The sum of all distributions must be equal to 100% validation message.", LastModified = "2017/08/30", Value = "The sum of variations traffic must equal 100%")]
    public string VariationDistributionsSumValidationMessage => this[nameof (VariationDistributionsSumValidationMessage)];

    /// <summary>Gets the goal page required.</summary>
    /// <value>Select a page</value>
    [ResourceEntry("GoalPageRequired", Description = "Select a page validation message.", LastModified = "2017/08/30", Value = "Select a page")]
    public string GoalPageRequired => this[nameof (GoalPageRequired)];

    /// <summary>Gets the goal page URL required.</summary>
    /// <value>Enter page URL</value>
    [ResourceEntry("GoalPageUrlRequired", Description = "Enter page URL validation message.", LastModified = "2017/08/30", Value = "Enter page URL")]
    public string GoalPageUrlRequired => this[nameof (GoalPageUrlRequired)];

    /// <summary>Gets the goal conversion required.</summary>
    /// <value>The goal conversion required.</value>
    [ResourceEntry("GoalConversionRequired", Description = "Please select a conversion validation message.", LastModified = "2020/03/10", Value = "Define conversion in Sitefinity Insight or select another goal")]
    public string GoalConversionRequired => this[nameof (GoalConversionRequired)];

    /// <summary>word: Results</summary>
    /// <value>Results</value>
    [ResourceEntry("Results", Description = "word: Results", LastModified = "2017/08/11", Value = "Results")]
    public string Results => this[nameof (Results)];

    /// <summary>word: Report</summary>
    /// <value>Report</value>
    [ResourceEntry("Report", Description = "word: Report", LastModified = "2017/08/11", Value = "Report")]
    public string Report => this[nameof (Report)];

    /// <summary>
    /// Phrase: Tracking consent settings
    /// label: Consent to track user behaviour
    /// </summary>
    [ResourceEntry("TrackingConsentSettings", Description = "Phrase: Consent to track user behaviour", LastModified = "2017/08/10", Value = "Consent to track user behaviour")]
    public string TrackingConsentSettings => this[nameof (TrackingConsentSettings)];

    /// <summary>
    /// label: Ask users for consent to track their activities
    /// </summary>
    [ResourceEntry("TrackingConsent", Description = "Phrase: Ask users for consent to track their activities", LastModified = "2017/07/18", Value = "Ask users for consent to track their activities")]
    public string TrackingConsent => this[nameof (TrackingConsent)];

    /// <summary>label: Settings:</summary>
    [ResourceEntry("TrackingConsentSettingsDescription", Description = "Settings: ", LastModified = "2017/08/16", Value = "Settings: ")]
    public string TrackingConsentSettingsDescription => this[nameof (TrackingConsentSettingsDescription)];

    [ResourceEntry("TrackingConsentDefaultDomainDisplay", Description = "Phrase: Any domain", LastModified = "2017/09/05", Value = "Any domain")]
    public string TrackingConsentDefaultDomainDisplay => this[nameof (TrackingConsentDefaultDomainDisplay)];

    /// <summary>label: Tracking Activities</summary>
    [ResourceEntry("TrackingConsentDialogTitle", Description = "Phrase: <Dialog title goes here>", LastModified = "2017/07/18", Value = "&ltDialog title goes here&gt")]
    public string TrackingConsentDialogTitle => this[nameof (TrackingConsentDialogTitle)];

    /// <summary>label: This site wants to monitor some ...</summary>
    [ResourceEntry("TrackingConsentDialogDescription", Description = "Phrase: <Dialog description or question goes here>", LastModified = "2017/07/18", Value = "&ltDialog description or question goes here&gt")]
    public string TrackingConsentDialogDescription => this[nameof (TrackingConsentDialogDescription)];

    /// <summary>label: I accept</summary>
    [ResourceEntry("TrackingConsentDialogAccept", Description = "Phrase: I accept", LastModified = "2017/07/18", Value = "I accept")]
    public string TrackingConsentDialogAccept => this[nameof (TrackingConsentDialogAccept)];

    /// <summary>label: I refuse</summary>
    [ResourceEntry("TrackingConsentDialogReject", Description = "Phrase: I refuse", LastModified = "2017/07/18", Value = "I refuse")]
    public string TrackingConsentDialogReject => this[nameof (TrackingConsentDialogReject)];

    /// <summary>word: Rejected</summary>
    [ResourceEntry("Rejected", Description = "Word: Rejected", LastModified = "2018/08/16", Value = "Rejected")]
    public string Rejected => this[nameof (Rejected)];

    /// <summary>label: I refuse</summary>
    [ResourceEntry("TrackingConsentDomainDeleteConfirmation", Description = "Phrase: Are you sure you want to remove rules for this domain?", LastModified = "2017/08/31", Value = "Are you sure you want to remove rules for this domain?")]
    public string TrackingConsentDomainDeleteConfirmation => this[nameof (TrackingConsentDomainDeleteConfirmation)];

    /// <summary>label: Domain</summary>
    [ResourceEntry("TrackingConsentViewDomainTitle", Description = "Phrase: Domain", LastModified = "2017/09/04", Value = "Domain")]
    public string TrackingConsentViewDomainTitle => this[nameof (TrackingConsentViewDomainTitle)];

    /// <summary>label: Domain</summary>
    [ResourceEntry("TrackingConsentViewConsentTitle", Description = "Phrase: Domain", LastModified = "2017/09/04", Value = "Ask for user consent")]
    public string TrackingConsentViewConsentTitle => this[nameof (TrackingConsentViewConsentTitle)];

    /// <summary>label: Add domain consent</summary>
    [ResourceEntry("TrackingConsentViewAddTitle", Description = "Phrase: Add domain consent", LastModified = "2017/09/04", Value = "Add domain consent")]
    public string TrackingConsentViewAddTitle => this[nameof (TrackingConsentViewAddTitle)];

    /// <summary>label: Edit domain consent</summary>
    [ResourceEntry("TrackingConsentViewEditTitle", Description = "Phrase: Edit domain consent", LastModified = "2017/09/04", Value = "Edit domain consent")]
    public string TrackingConsentViewEditTitle => this[nameof (TrackingConsentViewEditTitle)];

    /// <summary>label: For all pages under domain...</summary>
    [ResourceEntry("TrackingConsentViewDomainDescription", Description = "Phrase: For all pages under domain...", LastModified = "2017/09/04", Value = "For all pages under domain...")]
    public string TrackingConsentViewDomainDescription => this[nameof (TrackingConsentViewDomainDescription)];

    /// <summary>label: Phrase: Example: mydomain.com</summary>
    [ResourceEntry("TrackingConsentViewDomainExample", Description = "Phrase: Example: mydomain.com", LastModified = "2017/09/12", Value = "Example: <em>mydomain.com</em>")]
    public string TrackingConsentViewDomainExample => this[nameof (TrackingConsentViewDomainExample)];

    /// <summary>label: Consent</summary>
    [ResourceEntry("TrackingConsentViewConsentDescription", Description = "Phrase: Consent", LastModified = "2017/09/04", Value = "Consent")]
    public string TrackingConsentViewConsentDescription => this[nameof (TrackingConsentViewConsentDescription)];

    /// <summary>label: Template for consent</summary>
    [ResourceEntry("TrackingConsentViewTemplateDescription", Description = "Phrase: Template for consent", LastModified = "2017/09/04", Value = "Template for consent")]
    public string TrackingConsentViewTemplateDescription => this[nameof (TrackingConsentViewTemplateDescription)];

    /// <summary>label: Add domain</summary>
    [ResourceEntry("TrackingConsentViewAddNew", Description = "Phrase: Add domain", LastModified = "2017/09/05", Value = "Add domain")]
    public string TrackingConsentViewAddNew => this[nameof (TrackingConsentViewAddNew)];

    /// <summary>label: Change</summary>
    [ResourceEntry("TrackingConsentViewTemplateChange", Description = "Phrase: Change", LastModified = "2017/09/05", Value = "Change")]
    public string TrackingConsentViewTemplateChange => this[nameof (TrackingConsentViewTemplateChange)];

    /// <summary>label: FAQ</summary>
    [ResourceEntry("TrackingConsentViewFaqTitle", Description = "Phrase: FAQ", LastModified = "2017/09/12", Value = "FAQ")]
    public string TrackingConsentViewFaqTitle => this[nameof (TrackingConsentViewFaqTitle)];

    /// <summary>label: What does "any domain" mean?</summary>
    [ResourceEntry("TrackingConsentViewFaqQuestion1", Description = "Phrase: What does \"any domain\" mean?", LastModified = "2017/09/12", Value = "What does \"any domain\" mean?")]
    public string TrackingConsentViewFaqQuestion1 => this[nameof (TrackingConsentViewFaqQuestion1)];

    /// <summary>
    /// label: Rules set for "any domain" are applied to all your domains (sites). If you have set rules for specific domains, they  override rules set for "any domain".
    /// </summary>
    [ResourceEntry("TrackingConsentViewFaqAnswer1", Description = "Phrase: Rules set for \"any domain\" are applied to all your domains (sites). If you have set rules for specific domains, they  override rules set for \"any domain\".", LastModified = "2017/09/12", Value = "Rules set for \"any domain\" are applied to all your domains (sites). If you have set rules for specific domains, they  override rules set for \"any domain\".")]
    public string TrackingConsentViewFaqAnswer1 => this[nameof (TrackingConsentViewFaqAnswer1)];

    /// <summary>
    /// label: How can I modify consent template and messages?
    /// </summary>
    [ResourceEntry("TrackingConsentViewFaqQuestion2", Description = "Phrase: How can I modify consent template and messages?", LastModified = "2017/09/12", Value = "How can I modify consent template and messages?")]
    public string TrackingConsentViewFaqQuestion2 => this[nameof (TrackingConsentViewFaqQuestion2)];

    [ResourceEntry("TrackingConsentViewFaqAnswer2", Description = "Phrase: Consent templates are .HTML files located in: ~/App_Data/Sitefinity/TrackingConsent/ You can modify and translate consent messages in the Labels & Messages section. Details.", LastModified = "2017/09/12", Value = "Consent templates are .HTML files located in: <i>~/App_Data/Sitefinity/TrackingConsent/</i>. You can modify and translate consent messages in the <a href='/Sitefinity/Administration/Labels'>Labels & Messages</a> section. <a href='https://www.progress.com/documentation/sitefinity-cms/tracking-consent/'>Details.</a>")]
    public string TrackingConsentViewFaqAnswer2 => this[nameof (TrackingConsentViewFaqAnswer2)];

    /// <summary>label: Domain cannot be empty.</summary>
    [ResourceEntry("TrackingConsentViewEmptyError", Description = "Phrase: Domain cannot be empty.", LastModified = "2017/09/05", Value = "Domain cannot be empty.")]
    public string TrackingConsentViewEmptyError => this[nameof (TrackingConsentViewEmptyError)];

    /// <summary>
    /// label: Domain name must contain only letters, numbers, dots or semi-colon.
    /// </summary>
    [ResourceEntry("TrackingConsentViewSpecialCharactersError", Description = "Phrase: Domain name must contain only letters, numbers, dots or semi-colon.", LastModified = "2017/09/14", Value = "Domain name must contain only letters, numbers, dots or semi-colon.")]
    public string TrackingConsentViewSpecialCharactersError => this[nameof (TrackingConsentViewSpecialCharactersError)];

    /// <summary>label: Domain name cannot end with dot symbol(.).</summary>
    [ResourceEntry("TrackingConsentViewDotAtTheEndError", Description = "Phrase: Domain name cannot end with dot symbol(.).", LastModified = "2017/09/12", Value = "Domain name cannot end with dot symbol(.).")]
    public string TrackingConsentViewDotAtTheEndError => this[nameof (TrackingConsentViewDotAtTheEndError)];

    /// <summary>label: Domain cannot be empty.</summary>
    [ResourceEntry("TrackingConsentViewUniqueError", Description = "Phrase: Domain must be unique.", LastModified = "2017/09/05", Value = "Domain must be unique.")]
    public string TrackingConsentViewUniqueError => this[nameof (TrackingConsentViewUniqueError)];

    /// <summary>label: Domain cannot be empty.</summary>
    [ResourceEntry("TrackingConsentViewSelectTemplate", Description = "Phrase: Select a template", LastModified = "2017/09/05", Value = "Select a template")]
    public string TrackingConsentViewSelectTemplate => this[nameof (TrackingConsentViewSelectTemplate)];

    /// <summary>label: Domain cannot be empty.</summary>
    [ResourceEntry("TrackingConsentViewFileDescription", Description = "Phrase: You can add or modify templates in ~/App_Data/Sitefinity/TrackingConsent folder", LastModified = "2017/09/05", Value = "You can add or modify templates in ~/App_Data/Sitefinity/TrackingConsent folder")]
    public string TrackingConsentViewFileDescription => this[nameof (TrackingConsentViewFileDescription)];

    /// <summary>label: Domain cannot be empty.</summary>
    [ResourceEntry("TrackingConsentViewFileError", Description = "Phrase: The file cannot be found", LastModified = "2017/09/05", Value = "The file cannot be found")]
    public string TrackingConsentViewFileError => this[nameof (TrackingConsentViewFileError)];

    /// <summary>Next page viewed goal type name</summary>
    /// <value>Next page viewed</value>
    [ResourceEntry("NextPageViewGoalType", Description = "Next page viewed goal type name", LastModified = "2017/10/17", Value = "Next page viewed")]
    public string NextPageViewGoalType => this[nameof (NextPageViewGoalType)];

    /// <summary>Form submission goal type name</summary>
    /// <value>Form submission</value>
    [ResourceEntry("FormSubmissionGoalType", Description = "Form submission goal type name", LastModified = "2017/10/17", Value = "Form submission")]
    public string FormSubmissionGoalType => this[nameof (FormSubmissionGoalType)];

    /// <summary>Sitefinity Insight conversion goal type name</summary>
    /// <value>Sitefinity Insight conversion</value>
    [ResourceEntry("DecConversionGoalType", Description = "Sitefinity Insight conversion goal type name", LastModified = "2020/03/13", Value = "Sitefinity Insight conversion")]
    public string DecConversionGoalType => this[nameof (DecConversionGoalType)];

    /// <summary>goal predicate: is</summary>
    /// <value>is</value>
    [ResourceEntry("IsGoalPredicate", Description = "goal predicate: is", LastModified = "2017/10/17", Value = "is")]
    public string IsGoalPredicate => this[nameof (IsGoalPredicate)];

    /// <summary>goal predicate: contains</summary>
    /// <value>contains</value>
    [ResourceEntry("ContainsGoalPredicate", Description = "goal predicate: contains", LastModified = "2017/10/17", Value = "contains")]
    public string ContainsGoalPredicate => this[nameof (ContainsGoalPredicate)];

    /// <summary>goal name: Next page viewed is</summary>
    /// <value>Next page viewed is</value>
    [ResourceEntry("NextPageViewedIsGoalName", Description = "goal name: Next page viewed is", LastModified = "2017/10/17", Value = "Next page viewed is")]
    public string NextPageViewedIsGoalName => this[nameof (NextPageViewedIsGoalName)];

    /// <summary>goal name: Next page viewed contains</summary>
    /// <value>Next page viewed contains</value>
    [ResourceEntry("NextPageViewedContainsGoalName", Description = "goal name: Next page viewed contains", LastModified = "2017/10/17", Value = "Next page viewed contains")]
    public string NextPageViewedContainsGoalName => this[nameof (NextPageViewedContainsGoalName)];

    /// <summary>goal name: Form submission</summary>
    /// <value>Form submission</value>
    [ResourceEntry("FormSubmissionGoalName", Description = "goal name: Form submission", LastModified = "2017/10/17", Value = "Form submission")]
    public string FormSubmissionGoalName => this[nameof (FormSubmissionGoalName)];

    /// <summary>goal name: Sitefinity Insight conversion is</summary>
    /// <value>Sitefinity Insight conversion is</value>
    [ResourceEntry("DecConversionIsGoalName", Description = "goal name: Sitefinity Insight conversion is", LastModified = "2020/03/11", Value = "Sitefinity Insight conversion is")]
    public string DecConversionIsGoalName => this[nameof (DecConversionIsGoalName)];

    /// <summary>Gets External link: Introduction to Toolbars</summary>
    /// <value>http://www.telerik.com/help/aspnet-ajax/editor-toolbar-intro.html</value>
    [ResourceEntry("ExternalLinkIntroductionToolbars", Description = "External link: Introduction to Toolbars", LastModified = "2017/11/21", Value = "http://www.telerik.com/help/aspnet-ajax/editor-toolbar-intro.html")]
    public string ExternalLinkIntroductionToolbars => this[nameof (ExternalLinkIntroductionToolbars)];

    /// <summary>Gets External link: Sitefinity video tutorials</summary>
    /// <value>https://www.progress.com/video?product=sitefinity</value>
    [ResourceEntry("ExternalLinkSitefinityVideoTutorials", Description = "External link: Sitefinity video tutorials", LastModified = "2018/10/16", Value = "https://www.progress.com/video?product=sitefinity")]
    public string ExternalLinkSitefinityVideoTutorials => this[nameof (ExternalLinkSitefinityVideoTutorials)];

    /// <summary>Gets External link: Getting started</summary>
    /// <value>https://www.progress.com/documentation/sitefinity-cms/installation</value>
    [ResourceEntry("ExternalLinkGettingStarted", Description = "External link: Getting started", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/installation")]
    public string ExternalLinkGettingStarted => this[nameof (ExternalLinkGettingStarted)];

    [ResourceEntry("ExternalLinkTraining", Description = "External link: Training", LastModified = "2018/10/15", Value = "https://www.progress.com/services/education/sitefinity?utm_source=sitefinity&utm_medium=admin&utm_campaign=training")]
    public string ExternalLinkTraining => this[nameof (ExternalLinkTraining)];

    /// <summary>Gets External link: Software Development Kit</summary>
    /// <value>https://github.com/sitefinity</value>
    [ResourceEntry("ExternalLinkSoftwareDevelopmentKit", Description = "External link: Software Development Kit", LastModified = "2018/10/16", Value = "https://github.com/sitefinity")]
    public string ExternalLinkSoftwareDevelopmentKit => this[nameof (ExternalLinkSoftwareDevelopmentKit)];

    [ResourceEntry("ExternalLinkMarketplace", Description = "External link: Marketplace", LastModified = "2018/10/15", Value = "https://www.progress.com/sitefinity-cms/marketplace?utm_source=sitefinity&utm_medium=admin&utm_campaign=marketplace")]
    public string ExternalLinkMarketplace => this[nameof (ExternalLinkMarketplace)];

    [ResourceEntry("ExternalLinkTemplateBuilder", Description = "External link: Template builder", LastModified = "2018/10/15", Value = "https://www.progress.com/sitefinity-cms/platform/sitefinity-vsix?utm_source=sitefinity&utm_medium=admin&utm_campaign=templatebuilder")]
    public string ExternalLinkTemplateBuilder => this[nameof (ExternalLinkTemplateBuilder)];

    [ResourceEntry("ExternalLinkVideos", Description = "External link: Videos", LastModified = "2017/10/22", Value = "https://www.progress.com/video?product=sitefinity&utm_source=sitefinity&utm_medium=admin&utm_campaign=videos")]
    public string ExternalLinkVideos => this[nameof (ExternalLinkVideos)];

    [ResourceEntry("ExternalLinkBlogs", Description = "External link: Blogs", LastModified = "2018/10/15", Value = "https://www.progress.com/blogs/web-experience?utm_source=sitefinity&utm_medium=admin&utm_campaign=blogs")]
    public string ExternalLinkBlogs => this[nameof (ExternalLinkBlogs)];

    [ResourceEntry("ExternalLinkForums", Description = "External link: Forums", LastModified = "2017/11/22", Value = "https://community.progress.com/?filter=sitefinity&utm_source=sitefinity&utm_medium=admin&utm_campaign=forums")]
    public string ExternalLinkForums => this[nameof (ExternalLinkForums)];

    [ResourceEntry("ExternalLinkWebinars", Description = "External link: Webinars", LastModified = "2018/10/15", Value = "https://www.progress.com/webinars?filter=product%5esitefinity&utm_source=sitefinity&utm_medium=admin&utm_campaign=webinars")]
    public string ExternalLinkWebinars => this[nameof (ExternalLinkWebinars)];

    /// <summary>Gets External link: Submit a ticket</summary>
    /// <value>https://www.sitefinity.com/login.aspx?ReturnUrl=%2faccount%2fsupport-tickets.aspx</value>
    [ResourceEntry("ExternalLinkSubmitTicket", Description = "External link: Submit a ticket", LastModified = "2017/11/22", Value = "https://www.sitefinity.com/login.aspx?ReturnUrl=%2faccount%2fsupport-tickets.aspx")]
    public string ExternalLinkSubmitTicket => this[nameof (ExternalLinkSubmitTicket)];

    /// <summary>Gets External link: Send a feature request</summary>
    /// <value>https://sitefinity.ideas.aha.io/</value>
    [ResourceEntry("ExternalLinkSendFeatureRequest", Description = "External link: Send a feature request", LastModified = "2020/03/18", Value = "https://sitefinity.ideas.aha.io/")]
    public string ExternalLinkSendFeatureRequest => this[nameof (ExternalLinkSendFeatureRequest)];

    [ResourceEntry("ExternalLinkFindPartner", Description = "External link: Find a partner", LastModified = "2017/11/22", Value = "https://www.progress.com/partners/partner-directory?Products=Sitefinity&utm_source=sitefinity&utm_medium=admin&utm_campaign=partners")]
    public string ExternalLinkFindPartner => this[nameof (ExternalLinkFindPartner)];

    [ResourceEntry("ExternalLinkReleaseNotes", Description = "External link: Release notes", LastModified = "2018/10/15", Value = "https://www.progress.com/sitefinity-cms/release-notes?utm_source=sitefinity&utm_medium=admin&utm_campaign=releasenotes")]
    public string ExternalLinkReleaseNotes => this[nameof (ExternalLinkReleaseNotes)];

    [ResourceEntry("ExternalLinkRoadmap", Description = "External link: Roadmap", LastModified = "2018/10/16", Value = "https://www.progress.com/sitefinity-cms/whats-new?utm_source=sitefinity&utm_medium=admin&utm_campaign=roadmap")]
    public string ExternalLinkRoadmap => this[nameof (ExternalLinkRoadmap)];

    /// <summary>Gets External Link: Custom fields overview</summary>
    [ResourceEntry("ExternalLinkCustomFieldsOverview", Description = "External Link: Custom fields overview", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-custom-fields")]
    public string ExternalLinkCustomFieldsOverview => this[nameof (ExternalLinkCustomFieldsOverview)];

    /// <summary>Gets External Link: Manage content</summary>
    [ResourceEntry("ExternalLinkManageContent", Description = "External Link: Manage content", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/manage-content")]
    public string ExternalLinkManageContent => this[nameof (ExternalLinkManageContent)];

    /// <summary>Gets External Link: Work with revision history</summary>
    [ResourceEntry("ExternalLinkWorkWithRevisionHistory", Description = "External Link: Work with revision history", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/revision-history-for-content-items")]
    public string ExternalLinkWorkWithRevisionHistory => this[nameof (ExternalLinkWorkWithRevisionHistory)];

    /// <summary>Gets External Link: Create and edit content</summary>
    [ResourceEntry("ExternalLinkCreateAndEditContent", Description = "External Link: Create and edit content", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-create-and-edit-content")]
    public string ExternalLinkCreateAndEditContent => this[nameof (ExternalLinkCreateAndEditContent)];

    /// <summary>Gets External Link Label: Manage content</summary>
    [ResourceEntry("ExternalLinkLabelManageContent", Description = "External Link Label: Manage content", LastModified = "2018/08/29", Value = "Manage content")]
    public string ExternalLinkLabelManageContent => this[nameof (ExternalLinkLabelManageContent)];

    /// <summary>Gets External Link Label: Work with revision history</summary>
    [ResourceEntry("ExternalLinkLabelWorkWithRevisionHistory", Description = "External Link Label: Work with revision history", LastModified = "2018/08/29", Value = "Work with revision history")]
    public string ExternalLinkLabelWorkWithRevisionHistory => this[nameof (ExternalLinkLabelWorkWithRevisionHistory)];

    /// <summary>Gets External Link Label: Create and edit content</summary>
    [ResourceEntry("ExternalLinkLabelCreateAndEditContent", Description = "External Link Label: Create and edit content", LastModified = "2018/08/29", Value = "Create and edit content")]
    public string ExternalLinkLabelCreateAndEditContent => this[nameof (ExternalLinkLabelCreateAndEditContent)];

    /// <summary>phrase: By {0}</summary>
    /// <value>By {0}</value>
    [ResourceEntry("FilterTitle", Description = "phrase: By {0}", LastModified = "2018/02/23", Value = "By {0}")]
    public string FilterTitle => this[nameof (FilterTitle)];

    /// <summary>phrase: Load balancing</summary>
    /// <value>Load balancing</value>
    [ResourceEntry("LoadBalancing", Description = "phrase: Load balancing", LastModified = "2018/08/06", Value = "Load balancing")]
    public string LoadBalancing => this[nameof (LoadBalancing)];

    /// <summary>phrase: Node {0} is not accessible.</summary>
    /// <value>Node {0} is not accessible.</value>
    [ResourceEntry("NodeIsNotAccessible", Description = "phrase: Node {0} is not accessible.", LastModified = "2018/08/06", Value = "Node {0} is not accessible.")]
    public string NodeIsNotAccessible => this[nameof (NodeIsNotAccessible)];

    /// <summary>word: More</summary>
    [ResourceEntry("More", Description = "word: More", LastModified = "2018/08/08", Value = "More")]
    public string More => this[nameof (More)];

    /// <summary>word: Less</summary>
    [ResourceEntry("Less", Description = "word: Less", LastModified = "2018/08/08", Value = "Less")]
    public string Less => this[nameof (Less)];

    /// <summary>
    /// phrase: This user is registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.
    /// </summary>
    /// <value>This user is registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.</value>
    [ResourceEntry("UserRegisteredWith", Description = "phrase: This user is registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.", LastModified = "2018/08/08", Value = "This user is registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.")]
    public string UserRegisteredWith => this[nameof (UserRegisteredWith)];

    /// <summary>
    /// phrase: You are registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.
    /// </summary>
    /// <value>You are registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.</value>
    [ResourceEntry("YouAreRegisteredWith", Description = "phrase: You are is registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.", LastModified = "2018/08/14", Value = "You are registered with {0} account. Some fields cannot be edited because their value is obtained from {0}.")]
    public string YouAreRegisteredWith => this[nameof (YouAreRegisteredWith)];

    /// <summary>Label: Learn how to...</summary>
    [ResourceEntry("LearnHowTo", Description = "Label: Learn how to...", LastModified = "2018/08/29", Value = "Learn how to...")]
    public string LearnHowTo => this[nameof (LearnHowTo)];

    /// <summary>Gets the revision history view FAQ title.</summary>
    /// <value>The revision history view FAQ title.</value>
    [ResourceEntry("RevisionHistoryViewFaqTitle", Description = "Label: FAQ", LastModified = "2018/11/01", Value = "FAQ")]
    public string RevisionHistoryViewFaqTitle => this[nameof (RevisionHistoryViewFaqTitle)];

    /// <summary>Gets the revision history view FAQ question.</summary>
    /// <value>The revision history view FAQ question.</value>
    [ResourceEntry("RevisionHistoryViewFaqQuestion", Description = "Phrase: How to shrink database size after cleaning old versions?", LastModified = "2018/11/01", Value = "How to shrink database size after cleaning old versions?")]
    public string RevisionHistoryViewFaqQuestion => this[nameof (RevisionHistoryViewFaqQuestion)];

    /// <summary>Gets the revision history view FAQ answer.</summary>
    /// <value>The revision history view FAQ answer.</value>
    [ResourceEntry("RevisionHistoryViewFaqAnswer", Description = "Phrase: After setting limits on your versions and the initial cleanup is executed by the system at the scheduled time, you need to perform the following steps...", LastModified = "2018/11/01", Value = "After setting limits on your versions and the initial cleanup is executed by the system at the scheduled time, you need to perform <a href='https://www.progress.com/documentation/sitefinity-cms/limit-the-number-of-revisions-stored#shrink-database-size-after-old-versions-cleanup' target=\"_blank\">the following steps...</a>")]
    public string RevisionHistoryViewFaqAnswer => this[nameof (RevisionHistoryViewFaqAnswer)];

    /// <summary>Gets the revision history last week.</summary>
    /// <value>The revision history last week.</value>
    [ResourceEntry("RevisionHistoryLastWeek", Description = "Phrase: last week", LastModified = "2018/11/01", Value = "last week")]
    public string RevisionHistoryLastWeek => this[nameof (RevisionHistoryLastWeek)];

    /// <summary>Gets the revision history last month.</summary>
    /// <value>The revision history last month.</value>
    [ResourceEntry("RevisionHistoryLastMonth", Description = "Phrase: last month", LastModified = "2018/11/01", Value = "last month")]
    public string RevisionHistoryLastMonth => this[nameof (RevisionHistoryLastMonth)];

    /// <summary>Gets the revision history last three months.</summary>
    /// <value>The revision history last three months.</value>
    [ResourceEntry("RevisionHistoryLastThreeMonths", Description = "Phrase: last 3 months", LastModified = "2018/11/01", Value = "last 3 months")]
    public string RevisionHistoryLastThreeMonths => this[nameof (RevisionHistoryLastThreeMonths)];

    /// <summary>Gets the revision history last six months.</summary>
    /// <value>The revision history last six months.</value>
    [ResourceEntry("RevisionHistoryLastSixMonths", Description = "Phrase: last 6 months", LastModified = "2018/11/01", Value = "last 6 months")]
    public string RevisionHistoryLastSixMonths => this[nameof (RevisionHistoryLastSixMonths)];

    /// <summary>Gets the revision history last year.</summary>
    /// <value>The revision history last year.</value>
    [ResourceEntry("RevisionHistoryLastYear", Description = "Phrase: last year", LastModified = "2018/11/01", Value = "last year")]
    public string RevisionHistoryLastYear => this[nameof (RevisionHistoryLastYear)];

    /// <summary>Gets the revision history time to keep label.</summary>
    /// <value>The revision history time to keep label.</value>
    [ResourceEntry("RevisionHistoryTimeToKeepLabel", Description = "Phrase: All published and draft versions created in the ", LastModified = "2018/11/01", Value = "All published and draft versions created in the")]
    public string RevisionHistoryTimeToKeepLabel => this[nameof (RevisionHistoryTimeToKeepLabel)];

    /// <summary>Gets the revision history keep at least.</summary>
    /// <value>The revision history keep at least.</value>
    [ResourceEntry("RevisionHistoryKeepAtLeastLabel", Description = "Phrase: Keep at least", LastModified = "2018/11/01", Value = "Keep at least ")]
    public string RevisionHistoryKeepAtLeastLabel => this[nameof (RevisionHistoryKeepAtLeastLabel)];

    /// <summary>Gets the revision history published versions label.</summary>
    /// <value>The revision history published versions label.</value>
    [ResourceEntry("RevisionHistoryPublishedVersionsLabel", Description = "Phrase: published versions for all time", LastModified = "2018/11/01", Value = " published versions for all time")]
    public string RevisionHistoryPublishedVersionsLabel => this[nameof (RevisionHistoryPublishedVersionsLabel)];

    /// <summary>Gets the revision history versions limit label.</summary>
    /// <value>The revision history versions limit label.</value>
    [ResourceEntry("RevisionHistoryVersionsLimitLabel", Description = "Phrase: Versions stored for any content or page", LastModified = "2018/11/01", Value = "Versions stored for any content or page")]
    public string RevisionHistoryVersionsLimitLabel => this[nameof (RevisionHistoryVersionsLimitLabel)];

    /// <summary>Gets the number of versions format.</summary>
    /// <value>The number of versions format.</value>
    [ResourceEntry("NumberOfVersionsFormat", Description = "Phrase: Number of versions stored must be between 1 and 100. ", LastModified = "2018/11/01", Value = "Number of versions stored must be between 1 and 100.")]
    public string NumberOfVersionsFormat => this[nameof (NumberOfVersionsFormat)];

    /// <summary>Gets the version cleaner scheduled label.</summary>
    /// <value>The version cleaner scheduled label.</value>
    [ResourceEntry("VersionCleanerScheduledLabel", Description = "Phrase: Cleanup of older versions will be executed on {0}. To change the time go to: Administration > Settings > Advanced settings > Version > Cleanup > Scheduled for", LastModified = "2018/11/01", Value = "Cleanup of older versions will be executed on {0}. To change the time go to: Administration > Settings > Advanced settings > Version > Cleanup > Scheduled for")]
    public string VersionCleanerScheduledLabel => this[nameof (VersionCleanerScheduledLabel)];

    /// <summary>Gets the version cleaner learn more label.</summary>
    /// <value>The version cleaner learn more label.</value>
    [ResourceEntry("VersionCleanerLearnMoreLabel", Description = "Phrase: Learn more", LastModified = "2018/11/01", Value = "Learn more")]
    public string VersionCleanerLearnMoreLabel => this[nameof (VersionCleanerLearnMoreLabel)];

    /// <summary>Gets the version cleaner running.</summary>
    /// <value>The version cleaner running.</value>
    [ResourceEntry("VersionCleanerRunning", Description = "Phrase: Changes cannot be saved because there is an ongoing cleanup of old versions.", LastModified = "2018/11/01", Value = "Changes cannot be saved because there is an ongoing cleanup of old versions.")]
    public string VersionCleanerRunning => this[nameof (VersionCleanerRunning)];

    /// <summary>Gets the cleaner title.</summary>
    /// <value>The cleaner title.</value>
    [ResourceEntry("CleanerTitle", Description = "Phrase: Cleanup", LastModified = "2018/11/01", Value = "Cleanup")]
    public string CleanerTitle => this[nameof (CleanerTitle)];

    /// <summary>Gets the time to run cleanup task.</summary>
    /// <value>The time to run cleanup task.</value>
    [ResourceEntry("TimeToRunCleanupTask", Description = "Phrase: Scheduled for", LastModified = "2018/11/01", Value = "Scheduled for")]
    public string TimeToRunCleanupTask => this[nameof (TimeToRunCleanupTask)];

    /// <summary>Gets the time to run cleanup task description.</summary>
    /// <value>The time to run cleanup task description.</value>
    [ResourceEntry("TimeToRunCleanupTaskDescription", Description = "Phrase: Sets the time when the revision history cleanup task will be executed. Time is in a 24-hour format, UTC.", LastModified = "2018/11/01", Value = "Sets the time when the revision history cleanup task will be executed. Time is in a 24-hour format, UTC.")]
    public string TimeToRunCleanupTaskDescription => this[nameof (TimeToRunCleanupTaskDescription)];

    /// <summary>Gets the unlimited.</summary>
    /// <value>The unlimited.</value>
    [ResourceEntry("Unlimited", Description = "Phrase: Unlimited", LastModified = "2018/11/01", Value = "Unlimited")]
    public string Unlimited => this[nameof (Unlimited)];

    /// <summary>Gets the limited.</summary>
    /// <value>The limited.</value>
    [ResourceEntry("Limited", Description = "Phrase: Limit versions to ...", LastModified = "2018/11/01", Value = "Limit versions to ...")]
    public string Limited => this[nameof (Limited)];

    /// <summary>Message shown when the item is locked by another user</summary>
    /// <value>You can only view this item. Contact your administrator for permissions to edit the item.</value>
    [ResourceEntry("ItemLockedCannotEdit", Description = "Message shown when the item is locked by another user", LastModified = "2018/11/23", Value = "You can only view this item. Contact your administrator to unlock and edit this item.")]
    public string ItemLockedCannotEdit => this[nameof (ItemLockedCannotEdit)];

    /// <summary>Gets phrase: Externally defined</summary>
    /// <value>Externally defined</value>
    [ResourceEntry("ExternallyDefined", Description = "phrase: Externally defined", LastModified = "2019/02/27", Value = "Externally defined")]
    public string ExternallyDefined => this[nameof (ExternallyDefined)];

    /// <summary>Gets the configuration property path title.</summary>
    /// <value>The configuration property path title.</value>
    [ResourceEntry("ConfigPropertyPathTitle", Description = "phrase: Path to configuration property", LastModified = "2019/02/27", Value = "Path to configuration property")]
    public string ConfigPropertyPathTitle => this[nameof (ConfigPropertyPathTitle)];

    /// <summary>Gets phrase: Other</summary>
    /// <value>Other</value>
    [ResourceEntry("Other", Description = "phrase: Other", LastModified = "2020/01/10", Value = "Other")]
    public string Other => this[nameof (Other)];

    [ResourceEntry("NoRecordsText", Description = "Grid no records to display text.", LastModified = "2020/01/31", Value = "No records to display.")]
    public string NoRecordsText => this[nameof (NoRecordsText)];

    [ResourceEntry("CreatedOn", Description = "Created on", LastModified = "2020/03/10", Value = "Created on")]
    public string CreatedOn => this[nameof (CreatedOn)];

    [ResourceEntry("CreatedBy", Description = "Created by", LastModified = "2020/03/10", Value = "Created by")]
    public string CreatedBy => this[nameof (CreatedBy)];

    /// <summary>Is condition operator</summary>
    /// <value>is</value>
    [ResourceEntry("IsOperator", Description = "is operator", LastModified = "2019/1/29", Value = "is")]
    public string IsOperator => this[nameof (IsOperator)];

    /// <summary>Isn't condition operator</summary>
    /// <value>isn't</value>
    [ResourceEntry("IsNotOperator", Description = "Is not operator", LastModified = "2019/1/29", Value = "isn't")]
    public string IsNotOperator => this[nameof (IsNotOperator)];

    /// <summary>Contains condition operator</summary>
    /// <value>contains</value>
    [ResourceEntry("ContainsOperator", Description = "contains operator", LastModified = "2019/1/29", Value = "contains")]
    public string ContainsOperator => this[nameof (ContainsOperator)];

    /// <summary>Does not contain condition operator</summary>
    /// <value>doesn't contain</value>
    [ResourceEntry("NotContainsOperator", Description = "doesn't contain operator", LastModified = "2019/1/29", Value = "doesn't contain")]
    public string NotContainsOperator => this[nameof (NotContainsOperator)];

    /// <summary>Is filled condition operator</summary>
    /// <value>is filled</value>
    [ResourceEntry("IsFilledOperator", Description = "is filled operator", LastModified = "2019/1/29", Value = "is filled")]
    public string IsFilledOperator => this[nameof (IsFilledOperator)];

    /// <summary>Is not filled condition operator</summary>
    /// <value>isn't filled</value>
    [ResourceEntry("IsNotFilledOperator", Description = "isn't filled operator", LastModified = "2019/3/25", Value = "isn't filled")]
    public string IsNotFilledOperator => this[nameof (IsNotFilledOperator)];

    /// <summary>File selected condition operator</summary>
    /// <value>file selected</value>
    [ResourceEntry("FileSelectedOperator", Description = "file selected operator", LastModified = "2019/1/29", Value = "file selected")]
    public string FileSelectedOperator => this[nameof (FileSelectedOperator)];

    /// <summary>No file selected condition operator</summary>
    /// <value>no file selected</value>
    [ResourceEntry("NoFileSelectedOperator", Description = "no file selected operator", LastModified = "2019/1/29", Value = "no file selected")]
    public string NoFileSelectedOperator => this[nameof (NoFileSelectedOperator)];

    /// <summary>Is before condition operator</summary>
    /// <value>is before</value>
    [ResourceEntry("IsBeforeOperator", Description = "is before operator", LastModified = "2019/1/29", Value = "is before")]
    public string IsBeforeOperator => this[nameof (IsBeforeOperator)];

    /// <summary>Is after condition operator</summary>
    /// <value>is after</value>
    [ResourceEntry("IsAfterOperator", Description = "is after operator", LastModified = "2019/1/29", Value = "is after")]
    public string IsAfterOperator => this[nameof (IsAfterOperator)];

    /// <summary>Is greater than condition operator</summary>
    /// <value>is greater than</value>
    [ResourceEntry("IsGreaterOperator", Description = "is greater than operator", LastModified = "2019/1/29", Value = "is greater than")]
    public string IsGreaterOperator => this[nameof (IsGreaterOperator)];

    /// <summary>Is less than condition operator</summary>
    /// <value>is less than</value>
    [ResourceEntry("IsLessOperator", Description = "is less than operator", LastModified = "2019/1/29", Value = "is less than")]
    public string IsLessOperator => this[nameof (IsLessOperator)];

    /// <summary>Is equal to condition operator</summary>
    /// <value>is equal to</value>
    [ResourceEntry("IsEqualOperator", Description = "is equal to operator", LastModified = "2019/1/29", Value = "is equal to")]
    public string IsEqualOperator => this[nameof (IsEqualOperator)];

    /// <summary>Is not equal to condition operator</summary>
    /// <value>is not equal to</value>
    [ResourceEntry("IsNotEqualOperator", Description = "is not equal to operator", LastModified = "2020/11/20", Value = "is not equal to")]
    public string IsNotEqualOperator => this[nameof (IsNotEqualOperator)];

    /// <summary>Show field action</summary>
    /// <value>Show field</value>
    [ResourceEntry("ShowField", Description = "Show field", LastModified = "2019/1/30", Value = "Show field")]
    public string ShowField => this[nameof (ShowField)];

    /// <summary>Hide field action</summary>
    /// <value>Hide field</value>
    [ResourceEntry("HideField", Description = "Hide field", LastModified = "2019/1/30", Value = "Hide field")]
    public string HideField => this[nameof (HideField)];

    /// <summary>Skip to step action</summary>
    /// <value>Skip to step</value>
    [ResourceEntry("SkipToStep", Description = "Skip to step", LastModified = "2019/1/30", Value = "Skip to step")]
    public string SkipToStep => this[nameof (SkipToStep)];

    /// <summary>Show message action</summary>
    /// <value>Show message after submitting</value>
    [ResourceEntry("ShowMessage", Description = "phrase: Show message after submitting", LastModified = "2019/1/30", Value = "Show message after submitting")]
    public string ShowMessage => this[nameof (ShowMessage)];

    /// <summary>Go to page action</summary>
    /// <value>Go to page after submitting</value>
    [ResourceEntry("GoToPageAfterSubmit", Description = "phrase: Go to page after submitting", LastModified = "2019/1/30", Value = "Go to page after submitting")]
    public string GoToPageAfterSubmit => this[nameof (GoToPageAfterSubmit)];

    /// <summary>FormStep label</summary>
    /// <value>Step {0}</value>
    [ResourceEntry("FormStep", Description = "label: Step 1", LastModified = "2019/1/30", Value = "Step {0}")]
    public string FormStep => this[nameof (FormStep)];

    /// <summary>Send email notifications action</summary>
    /// <value>Send email notifications to...</value>
    [ResourceEntry("SendEmailNotificationsTo", Description = "phrase: Send email notifications to...", LastModified = "2019/08/20", Value = "Send email notifications to...")]
    public string SendEmailNotificationsTo => this[nameof (SendEmailNotificationsTo)];
  }
}
