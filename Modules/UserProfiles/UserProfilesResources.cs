// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfilesResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  /// <summary>Localizable strings for the user profiles.</summary>
  [ObjectInfo(typeof (UserProfilesResources), Description = "UserProfilesResourcesDescription", Title = "UserProfilesResourcesTitle")]
  public class UserProfilesResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="!:NewsResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public UserProfilesResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="!:NewsResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public UserProfilesResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Help</summary>
    [ResourceEntry("UserProfilesResourcesTitle", Description = "The title of this class.", LastModified = "2011/01/27", Value = "User profiles labels")]
    public string UserProfilesResourcesTitle => this[nameof (UserProfilesResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("UserProfilesResourcesDescription", Description = "The description of this class.", LastModified = "2011/01/27", Value = "Contains localizable resources for user profiles user interface.")]
    public string UserProfilesResourcesDescription => this[nameof (UserProfilesResourcesDescription)];

    /// <summary>Description for user profile types page.</summary>
    /// <value>UserProfileTypesDescription.</value>
    [ResourceEntry("UserProfileTypesDescription", Description = "News", LastModified = "2009/12/18", Value = "profile types")]
    public string UserProfileTypesDescription => this[nameof (UserProfileTypesDescription)];

    /// <summary>Label: Profile types</summary>
    /// <value>Title for the user profiles types page.</value>
    [ResourceEntry("UserProfileTypesHtmlTitle", Description = "Html Title for the user profiles types page.", LastModified = "2011/01/28", Value = "Profile types")]
    public string UserProfileTypesHtmlTitle => this[nameof (UserProfileTypesHtmlTitle)];

    /// <summary>word: Roles.</summary>
    [ResourceEntry("UserProfileTypes", Description = "The name of the Sitefinity/Admin/ProfileTypes section.", LastModified = "2010/01/28", Value = "Profile Types")]
    public string UserProfileTypes => this[nameof (UserProfileTypes)];

    /// <summary>word: Roles.</summary>
    [ResourceEntry("UserProfileTypesUrlName", Description = "The UrlName of the Sitefinity/Admin/ProfileTypes page.", LastModified = "2013/07/02", Value = "ProfileTypes")]
    public string UserProfileTypesUrlName => this[nameof (UserProfileTypesUrlName)];

    /// <summary>The text of the back to profile types link</summary>
    [ResourceEntry("BackToItems", Description = "The text of the back to profile types link", LastModified = "2011/02/16", Value = "Back to Profile Types")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Phrase: user profiles</summary>
    [ResourceEntry("PluralItemName", Description = "Phrase: news", LastModified = "2011/01/27", Value = "user profiles")]
    public string PluralItemName => this[nameof (PluralItemName)];

    /// <summary>Phrase: Edit Profiles widget settings</summary>
    [ResourceEntry("EditProfilesWidgetSettings", Description = "Phrase: Edit Profiles widget settings", LastModified = "2011/02/09", Value = "Edit Profiles widget settings")]
    public string EditProfilesWidgetSettings => this[nameof (EditProfilesWidgetSettings)];

    /// <summary>Phrase: Show my email to others</summary>
    [ResourceEntry("ShowMyEmailToOthers", Description = "Phrase: Show my email to others", LastModified = "2011/02/17", Value = "Show my email to others")]
    public string ShowMyEmailToOthers => this[nameof (ShowMyEmailToOthers)];

    /// <summary>phrase: No profile types.</summary>
    [ResourceEntry("NoUserProfileTypes", Description = "phrase: No profile types.", LastModified = "2011/02/10", Value = "No profile types.")]
    public string NoUserProfileTypes => this[nameof (NoUserProfileTypes)];

    /// <summary>phrase: Name cannot be empty.</summary>
    [ResourceEntry("NameCannotBeEmpty", Description = "phrase: Name cannot be empty.", LastModified = "2011/02/03", Value = "Name cannot be empty.")]
    public string NameCannotBeEmpty => this[nameof (NameCannotBeEmpty)];

    /// <summary>Phrase: Title cannot be empty</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "phrase: Title cannot be empty", LastModified = "2011/02/09", Value = "Title cannot be empty")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>
    /// Phrase: Sorry, the name should start with letter and only letters (a-z), numbers (0-9), and underscores (_) are allowed.
    /// </summary>
    [ResourceEntry("ProfileNameNotValid", Description = "Title cannot be empty", LastModified = "2011/02/03", Value = "Sorry, the name should start with a letter and continue with letters (a-z), numbers (0-9), or underscores (_).")]
    public string ProfileNameNotValid => this[nameof (ProfileNameNotValid)];

    /// <summary>Phrase: You are not logged in.</summary>
    [ResourceEntry("YouAreNotLoggedIn", Description = "Phrase: You are not logged in.", LastModified = "2011/03/07", Value = "You are not logged in.")]
    public string YouAreNotLoggedIn => this[nameof (YouAreNotLoggedIn)];

    /// <summary>
    /// phrase: Open <i>Edit</i> and <i>Change password </i> in selected existing pages...
    /// </summary>
    /// <value>The open views in external pages.</value>
    [ResourceEntry("OpenViewsInExternalPages", Description = "phrase: Open <em>Edit</em> and <em>Change password </em> in selected existing pages...", LastModified = "2011/03/16", Value = "Open <em>Edit</em> and <em>Change password </em> in selected existing pages...")]
    public string OpenViewsInExternalPages => this[nameof (OpenViewsInExternalPages)];

    /// <summary>phrase: Page for editing profile</summary>
    /// <value>The open views in external pages.</value>
    [ResourceEntry("PageForEditingProfile", Description = "phrase: Page for editing profile", LastModified = "2011/03/16", Value = "Page for editing profile")]
    public string PageForEditingProfile => this[nameof (PageForEditingProfile)];

    /// <summary>phrase: Page for changing password</summary>
    /// <value>The open views in external pages.</value>
    [ResourceEntry("PageForChangingPassword", Description = "phrase: Page for changing password", LastModified = "2011/03/16", Value = "Page for changing password")]
    public string PageForChangingPassword => this[nameof (PageForChangingPassword)];

    /// <summary>phrase: This profile is in...</summary>
    [ResourceEntry("ThisProfileIsIn", Description = "phrase: This profile is in...", LastModified = "2011/03/18", Value = "This profile is in...")]
    public string ThisProfileIsIn => this[nameof (ThisProfileIsIn)];

    /// <summary>phrase: Read mode only</summary>
    [ResourceEntry("ReadModeOnly", Description = "phrase: Read mode only", LastModified = "2011/03/18", Value = "Read mode only")]
    public string ReadModeOnly => this[nameof (ReadModeOnly)];

    /// <summary>phrase: Edit mode only</summary>
    [ResourceEntry("EditModeOnly", Description = "phrase:Edit mode only", LastModified = "2011/03/18", Value = "Edit mode only")]
    public string EditModeOnly => this[nameof (EditModeOnly)];

    /// <summary>phrase: Both: Read mode that can be edited</summary>
    [ResourceEntry("BothModes", Description = "phrase:Both: Read mode that can be edited", LastModified = "2011/03/18", Value = "Both: Read mode that can be edited")]
    public string BothModes => this[nameof (BothModes)];

    /// <summary>phrase: Fine tune the selected type</summary>
    [ResourceEntry("FineTuneTheSelectedType", Description = "phrase:Fine tune the selected type", LastModified = "2011/03/18", Value = "Fine tune the selected type")]
    public string FineTuneTheSelectedType => this[nameof (FineTuneTheSelectedType)];

    /// <summary>phrase: Display the profile of...</summary>
    [ResourceEntry("DisplayTheProfileOf", Description = "phrase: Display the profile of...", LastModified = "2011/03/18", Value = "Display the profile of...")]
    public string DisplayTheProfileOf => this[nameof (DisplayTheProfileOf)];

    /// <summary>phrase: User logged to his/her account</summary>
    [ResourceEntry("UserLoggedToHisHerAccount", Description = "phrase: User logged to his/her account", LastModified = "2011/03/18", Value = "User logged to his/her account")]
    public string UserLoggedToHisHerAccount => this[nameof (UserLoggedToHisHerAccount)];

    /// <summary>phrase: Selected user:</summary>
    [ResourceEntry("SelectedUser", Description = "phrase:Selected user:", LastModified = "2011/03/18", Value = "Selected user:")]
    public string SelectedUser => this[nameof (SelectedUser)];

    /// <summary>phrase: No user selected</summary>
    [ResourceEntry("NoUserSelected", Description = "phrase:No user selected", LastModified = "2011/03/18", Value = "No user selected")]
    public string NoUserSelected => this[nameof (NoUserSelected)];

    /// <summary>phrase: Select a user</summary>
    [ResourceEntry("SelectAUser", Description = "phrase:Select a user", LastModified = "2011/03/18", Value = "Select a user")]
    public string SelectAUser => this[nameof (SelectAUser)];

    /// <summary>phrase: Select a role</summary>
    [ResourceEntry("SelectARole", Description = "phrase:Select a role", LastModified = "2011/03/23", Value = "Select a role")]
    public string SelectARole => this[nameof (SelectARole)];

    /// <summary>phrase: Which users to display?</summary>
    [ResourceEntry("WhichUsersToDisplay", Description = "phrase:Which users to display?", LastModified = "2011/03/23", Value = "Which users to display?")]
    public string WhichUsersToDisplay => this[nameof (WhichUsersToDisplay)];

    /// <summary>phrase: Profile type</summary>
    [ResourceEntry("ProfileType", Description = "phrase:Profile type", LastModified = "2011/03/18", Value = "Profile type")]
    public string ProfileType => this[nameof (ProfileType)];

    /// <summary>Word: Template</summary>
    [ResourceEntry("Template", Description = "Word:Template", LastModified = "2011/03/18", Value = "Template")]
    public string Template => this[nameof (Template)];

    /// <summary>phrase: 'Read mode' template</summary>
    [ResourceEntry("ReadModeTemplate", Description = "phrase:'Read mode' template", LastModified = "2011/03/18", Value = "'Read mode' template")]
    public string ReadModeTemplate => this[nameof (ReadModeTemplate)];

    /// <summary>phrase: Create New Template</summary>
    [ResourceEntry("CreateNewTemplate", Description = "phrase:Create New Template", LastModified = "2011/03/18", Value = "Create New Template")]
    public string CreateNewTemplate => this[nameof (CreateNewTemplate)];

    /// <summary>phrase: 'Edit mode' template</summary>
    [ResourceEntry("EditModeTemplate", Description = "phrase:'Edit mode' template", LastModified = "2011/03/18", Value = "'Edit mode' template")]
    public string EditModeTemplate => this[nameof (EditModeTemplate)];

    /// <summary>phrase: Template for not logged users</summary>
    [ResourceEntry("TemplateForNotLoggedUsers", Description = "phrase:Template for not logged users", LastModified = "2011/03/18", Value = "Template for not logged users")]
    public string TemplateForNotLoggedUsers => this[nameof (TemplateForNotLoggedUsers)];

    /// <summary>phrase: Change password' template</summary>
    [ResourceEntry("ChangePasswordTemplate", Description = "phrase: 'Change password' template", LastModified = "2011/03/18", Value = "'Change password' template")]
    public string ChangePasswordTemplate => this[nameof (ChangePasswordTemplate)];

    /// <summary>phrase: When the changes are saved...</summary>
    [ResourceEntry("WhenTheChangesAreSaved", Description = "phrase: When the changes are saved...", LastModified = "2011/03/18", Value = "When the changes are saved...")]
    public string WhenTheChangesAreSaved => this[nameof (WhenTheChangesAreSaved)];

    /// <summary>phrase: Switch to 'Read mode'</summary>
    [ResourceEntry("SwitchToReadMode", Description = "phrase: Switch to 'Read mode'", LastModified = "2011/03/18", Value = "Switch to 'Read mode'")]
    public string SwitchToReadMode => this[nameof (SwitchToReadMode)];

    /// <summary>phrase: Show message above the form</summary>
    [ResourceEntry("ShowMessageAboveTheForm", Description = "phrase: Show message above the form", LastModified = "2011/03/18", Value = "Show message above the form")]
    public string ShowMessageAboveTheForm => this[nameof (ShowMessageAboveTheForm)];

    /// <summary>phrase: Open a specially prepared page...</summary>
    [ResourceEntry("OpenASpeciallyPreparedPage", Description = "phrase: Open a specially prepared page...", LastModified = "2011/03/18", Value = "Open a specially prepared page...")]
    public string OpenASpeciallyPreparedPage => this[nameof (OpenASpeciallyPreparedPage)];

    /// <summary>phrase: Css class</summary>
    [ResourceEntry("CssClass", Description = "phrase: Css class", LastModified = "2011/03/18", Value = "Css class")]
    public string CssClass => this[nameof (CssClass)];

    /// <summary>phrase: Change page</summary>
    [ResourceEntry("ChangePage", Description = "phrase: Change page", LastModified = "2011/03/18", Value = "Change page")]
    public string ChangePage => this[nameof (ChangePage)];

    /// <summary>word: Profile</summary>
    [ResourceEntry("UserViewPropertyEditorTitle", Description = "word: Profile", LastModified = "2011/03/25", Value = "Profile")]
    public string UserViewPropertyEditorTitle => this[nameof (UserViewPropertyEditorTitle)];

    /// <summary>
    /// phrase: Stay in the same screen but display a message above the form...
    /// </summary>
    [ResourceEntry("StayInTheSameScreenDisplayMessage", Description = "phrase: Stay in the same screen but display a message above the form...", LastModified = "2011/03/25", Value = "Stay in the same screen but display a message above the form...")]
    public string StayInTheSameScreenDisplayMessage => this[nameof (StayInTheSameScreenDisplayMessage)];

    /// <summary>phrase: Changes are successfully saved.</summary>
    [ResourceEntry("ChangesAreSuccessfullySaved", Description = "phrase: Changes are successfully saved.", LastModified = "2011/03/25", Value = "Changes are successfully saved.")]
    public string ChangesAreSuccessfullySaved => this[nameof (ChangesAreSuccessfullySaved)];

    /// <summary>word: About</summary>
    [ResourceEntry("About", Description = "word: About", LastModified = "2011/03/07", Value = "About")]
    public string About => this[nameof (About)];

    /// <summary>word: Photo</summary>
    [ResourceEntry("Photo", Description = "word: Photo", LastModified = "2011/03/18", Value = "Photo")]
    public string Photo => this[nameof (Photo)];

    /// <summary>word: Manage User Profiles</summary>
    [ResourceEntry("ManageUserProfiles", Description = "word: Manage User Profiles", LastModified = "2011/02/02", Value = "Manage User Profiles")]
    public string ManageUserProfiles => this[nameof (ManageUserProfiles)];

    /// <summary>word: Manage Profile Types</summary>
    [ResourceEntry("ManageUserProfileTypes", Description = "word: Manage Profile Types", LastModified = "2011/03/19", Value = "Manage Profile types")]
    public string ManageUserProfileTypes => this[nameof (ManageUserProfileTypes)];

    /// <summary>Label: Title</summary>
    [ResourceEntry("Title", Description = "Label", LastModified = "2011/02/03", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>Label: Example for title field</summary>
    [ResourceEntry("ExampleTitle", Description = "Label: Example for title field", LastModified = "2011/02/03", Value = "<strong>Example:</strong> Forum profile")]
    public string ExampleTitle => this[nameof (ExampleTitle)];

    /// <summary>phrase: Settings for user profiles.</summary>
    [ResourceEntry("Settings", Description = "phrase: Settings for user profiles", LastModified = "2011/02/02", Value = "Settings for user profiles")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Messsage: Create profile type</summary>
    /// <value>Label of the dialog that creates a news item.</value>
    [ResourceEntry("CreateItem", Description = "Label of the dialog that creates a profile type.", LastModified = "2011/02/02", Value = "Create profile type")]
    public string CreateItem => this[nameof (CreateItem)];

    /// <summary>The title of the edit item dialog</summary>
    [ResourceEntry("EditItem", Description = "The title of the edit item dialog", LastModified = "2011/02/03", Value = "Edit a profile type")]
    public string EditItem => this[nameof (EditItem)];

    /// <summary>Label: Edit fields</summary>
    [ResourceEntry("EditFields", Description = "Label: Edit fields", LastModified = "2011/02/17", Value = "Add/Edit fields")]
    public string EditFields => this[nameof (EditFields)];

    /// <summary>phrase: Create a profile type</summary>
    [ResourceEntry("CreateNewItem", Description = "phrase: Create a profile type", LastModified = "2011/02/09", Value = "Create a profile type")]
    public string CreateNewItem => this[nameof (CreateNewItem)];

    /// <summary>phrase: Create this news</summary>
    [ResourceEntry("CreateThisItem", Description = "phrase: Create this news", LastModified = "2011/02/03", Value = "Create this profile type and go to add fields")]
    public string CreateThisItem => this[nameof (CreateThisItem)];

    /// <summary>phrase: Delete this image</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2011/02/04", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>Phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Phrase", LastModified = "2011/02/04", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Phrase: EditProfile</summary>
    [ResourceEntry("EditProfile", Description = "Phrase: EditProfile", LastModified = "2011/02/18", Value = "Edit Profile")]
    public string EditProfile => this[nameof (EditProfile)];

    /// <summary>Messsage: Cancel</summary>
    [ResourceEntry("Cancel", Description = "Label for the buttons that cancel the editing/inserting operation.", LastModified = "2011/02/18", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>Label: Profile provider</summary>
    [ResourceEntry("ProfileProvider", Description = "Label: Profile provider", LastModified = "2011/02/03", Value = "Profile provider")]
    public string ProfileProvider => this[nameof (ProfileProvider)];

    /// <summary>Title of user providers field in detail form.</summary>
    [ResourceEntry("UserProvidersFieldTitle", Description = "Title of user providers field in detail form.", LastModified = "2021/02/04", Value = "User groups this profile set is used by:")]
    public string UserProvidersFieldTitle => this[nameof (UserProvidersFieldTitle)];

    /// <summary>Label: Applied to</summary>
    /// <value>Label of the dialog that creates a news item.</value>
    [ResourceEntry("AppliedTo", Description = "Label: Applied to", LastModified = "2011/02/02", Value = "Applied to")]
    public string AppliedTo => this[nameof (AppliedTo)];

    /// <summary>Label: All user groups</summary>
    /// <value>Label of the dialog that creates a news item.</value>
    [ResourceEntry("AllUserProviders", Description = "Label: All user groups", LastModified = "2021/02/04", Value = "All user groups")]
    public string AllUserProviders => this[nameof (AllUserProviders)];

    /// <summary>Label: Specific user groups</summary>
    /// <value>Label of the dialog that creates a news item.</value>
    [ResourceEntry("UseSpecificUserProviders", Description = "Label: Specific user groups", LastModified = "2021/02/04", Value = "Specific user groups")]
    public string UseSpecificUserProviders => this[nameof (UseSpecificUserProviders)];

    /// <summary>Label: All user groups</summary>
    /// <value>Label of the dialog that creates a news item.</value>
    [ResourceEntry("UseAllUserProviders", Description = "Label: All user groups", LastModified = "2011/02/04", Value = "All user groups")]
    public string UseAllUserProviders => this[nameof (UseAllUserProviders)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for creating a user in the frontend.</value>
    [ResourceEntry("CreateNewItemViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "Register user")]
    public string CreateNewItemViewTitle => this[nameof (CreateNewItemViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for editing a user in the frontend.</value>
    [ResourceEntry("EditItemViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "Edit a profile")]
    public string EditItemViewTitle => this[nameof (EditItemViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for viewing a user profile in the frontend.</value>
    [ResourceEntry("ViewItemViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "View profile")]
    public string ViewItemViewTitle => this[nameof (ViewItemViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for creating a user in the backend.</value>
    [ResourceEntry("CreateNewItemBackendViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "Create a user")]
    public string CreateNewItemBackendViewTitle => this[nameof (CreateNewItemBackendViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for editing a user in the backend.</value>
    [ResourceEntry("EditItemBackendViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "Edit a profile")]
    public string EditItemBackendViewTitle => this[nameof (EditItemBackendViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for viewing a user profile in the backend.</value>
    [ResourceEntry("ViewItemBackendViewTitle", Description = "The title of a view definition.", LastModified = "2011/03/28", Value = "View own profile")]
    public string ViewItemBackendViewTitle => this[nameof (ViewItemBackendViewTitle)];

    /// <summary>The title of a view definition.</summary>
    /// <value>The title of the view which is used for viewing a user profile in the backend.</value>
    [ResourceEntry("EditOwnItemBackendViewTitle", Description = "The title of a view definition.", LastModified = "2011/04/11", Value = "Edit own profile")]
    public string EditOwnItemBackendViewTitle => this[nameof (EditOwnItemBackendViewTitle)];

    /// <summary>
    /// Message displayed when user tries to delete the built-in profile type.
    /// </summary>
    [ResourceEntry("ErrorDeleteBuiltInProfileType", Description = "Message displayed when user tries to delete the built-in profile type.", LastModified = "2011/03/28", Value = "You cannot delete the built-in 'Basic profile' type!")]
    public string ErrorDeleteBuiltInProfileType => this[nameof (ErrorDeleteBuiltInProfileType)];

    /// <summary>
    /// Message displayed when user tries to create a profile type that already exists.
    /// </summary>
    [ResourceEntry("ErrorProfileTypeAlreadyExists", Description = "Message displayed when user tries to create a profile type that already exists.", LastModified = "2011/03/29", Value = "A profile type with the specified name already exists. Please, select a different name.")]
    public string ErrorProfileTypeAlreadyExists => this[nameof (ErrorProfileTypeAlreadyExists)];

    /// <summary>
    /// Message displayed when user tries to update a profile type with a title that already exists.
    /// </summary>
    [ResourceEntry("ErrorProfileTypeTitleAlreadyExists", Description = "Message displayed when user tries to update a profile type with a title that already exists.", LastModified = "2011/04/04", Value = "A profile type with the specified title already exists. Please, select a different title.")]
    public string ErrorProfileTypeTitleAlreadyExists => this[nameof (ErrorProfileTypeTitleAlreadyExists)];

    /// <summary>Word: Nickname</summary>
    [ResourceEntry("Nickname", Description = "Word: Nickname", LastModified = "2012/01/21", Value = "Nickname")]
    public string Nickname => this[nameof (Nickname)];

    /// <summary>
    /// Phrase: If left blank, the username will be used as a nickname
    /// </summary>
    [ResourceEntry("NicknameDescription", Description = "Phrase: If left blank, the username will be used as a nickname", LastModified = "2012/02/24", Value = "If left blank, the username will be used as a nickname")]
    public string NicknameDescription => this[nameof (NicknameDescription)];

    /// <summary>phrase: Sort users by</summary>
    [ResourceEntry("SortUsersBy", Description = "phrase: Sort users by", LastModified = "2011/03/26", Value = "Sort users by")]
    public string SortUsersBy => this[nameof (SortUsersBy)];

    /// <summary>phrase: Open Single user profile in...</summary>
    [ResourceEntry("OpenSingleUserProfileInDotDotDot", Description = "phrase: Open single user profile in...", LastModified = "2011/03/16", Value = "Open single user profile in...")]
    public string OpenSingleUserProfileInDotDotDot => this[nameof (OpenSingleUserProfileInDotDotDot)];

    /// <summary>phrase: All registered users</summary>
    [ResourceEntry("AllRegisteredUsers", Description = "phrase: All registered users", LastModified = "2011/03/16", Value = "All registered users")]
    public string AllRegisteredUsers => this[nameof (AllRegisteredUsers)];

    /// <summary>phrase: All registered users</summary>
    [ResourceEntry("OneUserOnly", Description = "phrase: One user only:", LastModified = "2011/03/16", Value = "One user only:")]
    public string OneUserOnly => this[nameof (OneUserOnly)];

    /// <summary>phrase: All registered users</summary>
    [ResourceEntry("UsersByRoles", Description = "phrase: Users by roles...", LastModified = "2011/03/16", Value = "Users by roles...")]
    public string UsersByRoles => this[nameof (UsersByRoles)];

    /// <summary>phrase: Select a user...</summary>
    [ResourceEntry("SelectAyUser", Description = "phrase: Select a user...", LastModified = "2011/03/16", Value = "Select a user...")]
    public string SelectAyUser => this[nameof (SelectAyUser)];

    /// <summary>phrase: Select a user...</summary>
    [ResourceEntry("SelectRoles", Description = "phrase: Select roles...", LastModified = "2011/03/16", Value = "Select roles...")]
    public string SelectRoles => this[nameof (SelectRoles)];

    /// <summary>phrase: Select a user...</summary>
    [ResourceEntry("UserList", Description = "phrase: User list", LastModified = "2011/03/24", Value = "User list")]
    public string UserList => this[nameof (UserList)];

    /// <summary>phrase: (A-Z)</summary>
    [ResourceEntry("AscendingSuffix", Description = "phrase: (A-Z)", LastModified = "2011/03/25", Value = "(A-Z)")]
    public string AscendingSuffix => this[nameof (AscendingSuffix)];

    /// <summary>phrase: (Z-A)</summary>
    [ResourceEntry("DescendingSuffix", Description = "phrase: (Z-A)", LastModified = "2011/03/25", Value = "(Z-A)")]
    public string DescendingSuffix => this[nameof (DescendingSuffix)];

    /// <summary>word: Register</summary>
    /// <value>Register</value>
    [ResourceEntry("Register", Description = "Label: All user providers", LastModified = "2011/03/28", Value = "Register")]
    public string Register => this[nameof (Register)];

    /// <summary>Label: Re-type password</summary>
    /// <value>Re-type password</value>
    [ResourceEntry("ReTypePassword", Description = "Label: Re-type password", LastModified = "2011/03/28", Value = "Re-type password")]
    public string ReTypePassword => this[nameof (ReTypePassword)];

    /// <summary>phrase: on top</summary>
    [ResourceEntry("AscendingDateTimeSuffix", Description = "phrase: on top", LastModified = "2011/03/29", Value = "on top")]
    public string AscendingDateTimeSuffix => this[nameof (AscendingDateTimeSuffix)];

    /// <summary>phrase: on bottom</summary>
    [ResourceEntry("DescendingDateTimeSuffix", Description = "phrase: on bottom", LastModified = "2011/03/29", Value = "on bottom")]
    public string DescendingDateTimeSuffix => this[nameof (DescendingDateTimeSuffix)];

    /// <summary>phrase: General</summary>
    [ResourceEntry("RegistrationFormGeneralViewTitle", Description = "phrase: General", LastModified = "2011/03/31", Value = "General")]
    public string RegistrationFormGeneralViewTitle => this[nameof (RegistrationFormGeneralViewTitle)];

    /// <summary>phrase: Account activation</summary>
    [ResourceEntry("RegistrationFormAccountActivationViewTitle", Description = "phrase: Account activation", LastModified = "2011/03/31", Value = "Account activation")]
    public string RegistrationFormAccountActivationViewTitle => this[nameof (RegistrationFormAccountActivationViewTitle)];

    /// <summary>phrase: Provider users will be registered in...</summary>
    [ResourceEntry("ProviderUsersWillBeRegisteredIn", Description = "phrase: Provider users will be registered in...", LastModified = "2011/04/06", Value = "Provider, where the user will be registered...")]
    public string ProviderUsersWillBeRegisteredIn => this[nameof (ProviderUsersWillBeRegisteredIn)];

    /// <summary>phrase: Provider users will be registered in...</summary>
    [ResourceEntry("RolesRegisteredUsersWillBeAssignedTo", Description = "phrase: Roles registered users will be assigned to...", LastModified = "2011/04/06", Value = "Roles, which the user will be assigned to...")]
    public string RolesRegisteredUsersWillBeAssignedTo => this[nameof (RolesRegisteredUsersWillBeAssignedTo)];

    /// <summary>phrase: Register form template</summary>
    [ResourceEntry("RegisterFormTemplate", Description = "phrase: Register form template", LastModified = "2011/04/06", Value = "Registration form template")]
    public string RegisterFormTemplate => this[nameof (RegisterFormTemplate)];

    /// <summary>phrase: When the form is successfully submitted...</summary>
    [ResourceEntry("WhenTheFormIsSuccessfully", Description = "phrase: When the form is successfully submitted...", LastModified = "2012/01/05", Value = "When the form is successfully submitted...")]
    public string WhenTheFormIsSuccessfully => this[nameof (WhenTheFormIsSuccessfully)];

    /// <summary>phrase: Show a message:</summary>
    [ResourceEntry("ShowAMessage", Description = "phrase: Show a message:", LastModified = "2011/04/03", Value = "Show a message:")]
    public string ShowAMessage => this[nameof (ShowAMessage)];

    /// <summary>phrase: Open a specially prepared page...</summary>
    [ResourceEntry("OpenSpeciallyPreparedPage", Description = "phrase: Show a message:", LastModified = "2011/04/03", Value = "Open a specially prepared page...")]
    public string OpenSpeciallyPreparedPage => this[nameof (OpenSpeciallyPreparedPage)];

    /// <summary>phrase: Activate accounts...</summary>
    [ResourceEntry("ActivateAccounts", Description = "phrase: Activate accounts...", LastModified = "2011/04/03", Value = "Activate accounts...")]
    public string ActivateAccounts => this[nameof (ActivateAccounts)];

    /// <summary>phrase: Immediately</summary>
    [ResourceEntry("Immediately", Description = "phrase: Immediately", LastModified = "2011/04/03", Value = "Immediately")]
    public string Immediately => this[nameof (Immediately)];

    /// <summary>phrase: After a confirmation</summary>
    [ResourceEntry("AfterConfirmation", Description = "phrase: After a confirmation", LastModified = "2011/04/03", Value = "After a confirmation")]
    public string AfterConfirmation => this[nameof (AfterConfirmation)];

    /// <summary>
    /// phrase: An email providing a link for confirmation will be sent
    /// </summary>
    [ResourceEntry("AnEmailProvidingLinkWillBeSent", Description = "phrase: An email providing a link for confirmation will be sent", LastModified = "2011/04/03", Value = "An email providing a link for confirmation will be sent")]
    public string AnEmailProvidingLinkWillBeSent => this[nameof (AnEmailProvidingLinkWillBeSent)];

    /// <summary>phrase: Send an email for successful registration</summary>
    [ResourceEntry("SendAnEmailForSuccessfulRegistration", Description = "phrase: Send an email for successful registration", LastModified = "2011/04/03", Value = "Send an email for successful registration")]
    public string SendAnEmailForSuccessfulRegistration => this[nameof (SendAnEmailForSuccessfulRegistration)];

    /// <summary>phrase: after the confirmation</summary>
    [ResourceEntry("AfterTheConfirmation", Description = "phrase: after the confirmation", LastModified = "2011/04/03", Value = "after the confirmation")]
    public string AfterTheConfirmation => this[nameof (AfterTheConfirmation)];

    /// <summary>phrase: Confirmation email template</summary>
    [ResourceEntry("ConfirmationEmailTemplate", Description = "phrase: Confirmation email template", LastModified = "2011/04/03", Value = "Confirmation email template")]
    public string ConfirmationEmailTemplate => this[nameof (ConfirmationEmailTemplate)];

    /// <summary>phrase: 'Success' email template</summary>
    [ResourceEntry("SuccessEmailTemplate", Description = "phrase: 'Success' email template", LastModified = "2011/04/03", Value = "'Success' email template")]
    public string SuccessEmailTemplate => this[nameof (SuccessEmailTemplate)];

    /// <summary>phrase: Confirmation page</summary>
    [ResourceEntry("ConfirmationPage", Description = "phrase: Confirmation page", LastModified = "2011/04/03", Value = "Confirmation page")]
    public string ConfirmationPage => this[nameof (ConfirmationPage)];

    /// <summary>
    /// phrase: When the confirmation link in the email is clicked open a selected page
    /// </summary>
    [ResourceEntry("ConfirmationPageDescription", Description = "phrase: When the confirmation link in the email is clicked open a selected page", LastModified = "2011/04/03", Value = "When the confirmation link in the email is clicked open a selected page")]
    public string ConfirmationPageDescription => this[nameof (ConfirmationPageDescription)];

    /// <summary>phrase: User Profiles:</summary>
    [ResourceEntry("UserProfiles", Description = "phrase: User Profiles:", LastModified = "2011/04/04", Value = "User Profiles:")]
    public string UserProfiles => this[nameof (UserProfiles)];

    /// <summary>phrase: Preferred language:</summary>
    [ResourceEntry("PreferredLanguage", Description = "phrase: Preferred language:", LastModified = "2011/04/07", Value = "Preferred language:")]
    public string PreferredLanguage => this[nameof (PreferredLanguage)];

    /// <summary>phrase: Confirmation email subject</summary>
    [ResourceEntry("ConfirmationEmailSubject", Description = "phrase: Confirmation email subject", LastModified = "2011/04/11", Value = "Confirmation email subject")]
    public string ConfirmationEmailSubject => this[nameof (ConfirmationEmailSubject)];

    /// <summary>phrase: 'Success' email subject</summary>
    [ResourceEntry("SuccessEmailSubject", Description = "phrase: 'Success' email subject", LastModified = "2011/04/11", Value = "'Success' email subject")]
    public string SuccessEmailSubject => this[nameof (SuccessEmailSubject)];

    [ResourceEntry("UserAuthenticationAndSingleSignOnTitle", Description = "phrase: User authentication & Single sign on", LastModified = "2011/11/24", Value = "User authentication & Single sign on")]
    public string UserAuthenticationAndSingleSignOnTitle => this[nameof (UserAuthenticationAndSingleSignOnTitle)];

    /// <summary>
    /// phrase: 'What is the type of authentication you want to use for your website'
    /// </summary>
    [ResourceEntry("TypeOfAuthenticationLiteral", Description = "phrase: What is the type of authentication you want to use for your website", LastModified = "2011/11/25", Value = "What is the type of authentication you want to use for your website")]
    public string TypeOfAuthenticationLiteral => this[nameof (TypeOfAuthenticationLiteral)];

    /// <summary>phrase: 'Allow users to login with'</summary>
    [ResourceEntry("LoginWithLiteral", Description = "phrase: Allow users to login with", LastModified = "2011/11/25", Value = "Allow users to login with")]
    public string LoginWithLiteral => this[nameof (LoginWithLiteral)];

    /// <summary>Users section title: Users</summary>
    [ResourceEntry("UserSectionTitle", Description = "Control description: Title of the users toolbox section", LastModified = "2011/03/24", Value = "Users")]
    public string UserSectionTitle => this[nameof (UserSectionTitle)];

    /// <summary>Users section description: Users section</summary>
    [ResourceEntry("UserSectionDescription", Description = "Control description: Toolbox section with the widgets related to users and profiles.", LastModified = "2011/03/08", Value = "Toolbox section with the widgets related to users and profiles.")]
    public string UserSectionDescription => this[nameof (UserSectionDescription)];

    /// <summary>Control name: User profile</summary>
    [ResourceEntry("UserProfileViewTitle", Description = "Control title: User profile", LastModified = "2011/03/08", Value = "Profile")]
    public string UserProfileViewTitle => this[nameof (UserProfileViewTitle)];

    /// <summary>
    /// Control description: A control for displaying user profiles.
    /// </summary>
    [ResourceEntry("UserProfileViewDescription", Description = "Control description: A control for displaying user profiles.", LastModified = "2012/01/05", Value = "A control for displaying user profiles.")]
    public string UserProfileViewDescription => this[nameof (UserProfileViewDescription)];

    /// <summary>Control name: Users list</summary>
    [ResourceEntry("UserProfilesViewTitle", Description = "Control title: Users list", LastModified = "2011/03/08", Value = "Users list")]
    public string UserProfilesViewTitle => this[nameof (UserProfilesViewTitle)];

    /// <summary>
    /// Control description: A control for displaying a list of user.
    /// </summary>
    [ResourceEntry("UserProfilesViewDescription", Description = "Control description: A control for displaying user profiles.", LastModified = "2011/03/08", Value = "Displaying or editing the profile of the currently logged user")]
    public string UserProfilesViewDescription => this[nameof (UserProfilesViewDescription)];

    /// <summary>
    /// Control description: A control for displaying a list of user.
    /// </summary>
    [ResourceEntry("UsersListViewDescription", Description = "Control description: A control for displaying user profiles.", LastModified = "2011/03/08", Value = "List of users and their profiles")]
    public string UsersListViewDescription => this[nameof (UsersListViewDescription)];

    /// <summary>Control name: Registration</summary>
    [ResourceEntry("RegistrationWidgetTitle", Description = "Control title: Registration", LastModified = "2011/03/29", Value = "Registration")]
    public string RegistrationWidgetTitle => this[nameof (RegistrationWidgetTitle)];

    /// <summary>Control description: A form used to register users.</summary>
    [ResourceEntry("RegistrationWidgetDescription", Description = "Control description: A form used to register users.", LastModified = "2011/03/29", Value = "Form used to register users")]
    public string RegistrationWidgetDescription => this[nameof (RegistrationWidgetDescription)];

    /// <summary>Control name: Registration</summary>
    [ResourceEntry("AccountActivationWidgetTitle", Description = "Control title: Account Activation", LastModified = "2011/03/29", Value = "Account activation")]
    public string AccountActivationWidgetTitle => this[nameof (AccountActivationWidgetTitle)];

    /// <summary>
    /// Control description: A widget that should be placed on the page that will be used to activate user accounts.
    /// </summary>
    [ResourceEntry("AccountActivationWidgetDescription", Description = "Control description: A widget that should be placed on the page that will be used to activate user accounts.", LastModified = "2011/03/29", Value = "Put this widget on the page linked in the email for activation user account")]
    public string AccountActivationWidgetDescription => this[nameof (AccountActivationWidgetDescription)];

    /// <summary>phrase: Thank you for filling out our form.</summary>
    [ResourceEntry("SuccessThanksForFillingOutOurForm", Description = "phrase: Thank you for filling out our form.", LastModified = "2019/06/27", Value = "Thank you for filling out our form.")]
    public string SuccessThanksForFillingOutOurForm => this[nameof (SuccessThanksForFillingOutOurForm)];

    /// <summary>Control name: TwitterFeed (backward compatibility)</summary>
    [ResourceEntry("TwitterFeedTitle", Description = "Control title: Twitter feed", LastModified = "2011/11/07", Value = "Twitter feed <span class=\"sfNote\">(backward compatibility)</span>")]
    public string TwitterFeedTitle => this[nameof (TwitterFeedTitle)];

    /// <summary>
    /// Control description: A widget that shows lates twitter activities on a page after entering a twitter account.
    /// </summary>
    [ResourceEntry("TwitterFeedDescription", Description = "Control description: A widget that shows lates twitter activities on a page after entering a twitter account.", LastModified = "2011/11/07", Value = "A widget that shows lates twitter activities on a page after entering a twitter account.")]
    public string TwitterFeedDescription => this[nameof (TwitterFeedDescription)];

    /// <summary>Control name: TwitterWidget</summary>
    [ResourceEntry("TwitterWidgetTitle", Description = "Control title: Twitter widget", LastModified = "2013/04/04", Value = "Twitter widget")]
    public string TwitterWidgetTitle => this[nameof (TwitterWidgetTitle)];

    /// <summary>
    /// Control description: A widget that shows later twitter activities.
    /// </summary>
    [ResourceEntry("TwitterWidgetDescription", Description = "Control description: A widget that shows later twitter activities.", LastModified = "2011/11/07", Value = "A widget that shows later twitter activities.")]
    public string TwitterWidgetDescription => this[nameof (TwitterWidgetDescription)];

    /// <summary>phrase: Welcome</summary>
    [ResourceEntry("SuccessEmailDefaultSubject", Description = "The default subject of the success email", LastModified = "2011/04/12", Value = "Welcome")]
    public string SuccessEmailDefaultSubject => this[nameof (SuccessEmailDefaultSubject)];

    /// <summary>phrase: Registration successful</summary>
    [ResourceEntry("ConfirmationEmailDefaultSubject", Description = "The default subject of the confirmation email", LastModified = "2011/04/12", Value = "Registration successful")]
    public string ConfirmationEmailDefaultSubject => this[nameof (ConfirmationEmailDefaultSubject)];

    /// <summary>word: Users</summary>
    [ResourceEntry("Users", Description = "word: Users", LastModified = "2011/04/27", Value = "Users")]
    public string Users => this[nameof (Users)];

    /// <summary>phrase: Profile – read mode</summary>
    [ResourceEntry("ProfileReadMode", Description = "phrase: Profile – read mode", LastModified = "2011/04/27", Value = "Profile - read mode")]
    public string ProfileReadMode => this[nameof (ProfileReadMode)];

    /// <summary>phrase: Profile – edit mode</summary>
    [ResourceEntry("ProfileEditMode", Description = "phrase: Profile – edit mode", LastModified = "2011/04/27", Value = "Profile – edit mode")]
    public string ProfileEditMode => this[nameof (ProfileEditMode)];

    /// <summary>phrase: Profile – not logged user</summary>
    [ResourceEntry("ProfileNotLoggedUser", Description = "phrase: Profile – not logged user", LastModified = "2011/04/27", Value = "Profile – not logged user")]
    public string ProfileNotLoggedUser => this[nameof (ProfileNotLoggedUser)];

    /// <summary>phrase: Profile – password editor</summary>
    [ResourceEntry("ProfilePasswordEditor", Description = "phrase: Profile – password editor", LastModified = "2011/04/27", Value = "Profile – password editor")]
    public string ProfilePasswordEditor => this[nameof (ProfilePasswordEditor)];

    /// <summary>
    /// phrase: The selected template cannot be used with this profile type. Select another template or edit this template.
    /// </summary>
    [ResourceEntry("TheSelectedTemplateCannotBeUsed", Description = "phrase: The selected template cannot be used with this profile type. Select another template or edit this template.", LastModified = "2011/05/04", Value = "The selected template cannot be used with this profile type. Select another template or edit this template.")]
    public string TheSelectedTemplateCannotBeUsed => this[nameof (TheSelectedTemplateCannotBeUsed)];

    /// <summary>phrase: Current password</summary>
    [ResourceEntry("CurrentPassword", Description = "phrase: Current password", LastModified = "2012/02/15", Value = "Current password")]
    public string CurrentPassword => this[nameof (CurrentPassword)];

    /// <summary>phrase: Repeat new password</summary>
    [ResourceEntry("ConfirmNewPassword", Description = "phrase: Repeat new password", LastModified = "2016/12/23", Value = "Repeat new password")]
    public string ConfirmNewPassword => this[nameof (ConfirmNewPassword)];

    /// <summary>word: Login</summary>
    [ResourceEntry("Login", Description = "word: Login", LastModified = "2012/10/25", Value = "Login")]
    public string Login => this[nameof (Login)];

    /// <summary>word: Anonymous</summary>
    /// <value>Anonymous</value>
    [ResourceEntry("Anonymous", Description = "word: Anonymous", LastModified = "2013/07/02", Value = "Anonymous")]
    public string Anonymous => this[nameof (Anonymous)];

    /// <summary>phrase: Missing user</summary>
    /// <value>Missing user</value>
    [ResourceEntry("MissingUser", Description = "phrase: Missing user", LastModified = "2013/07/02", Value = "Missing user")]
    public string MissingUser => this[nameof (MissingUser)];

    /// <summary>phrase: Secret question</summary>
    /// <value>Secret question</value>
    [ResourceEntry("NewPasswordQuestion", Description = "phrase: Secret question", LastModified = "2016/12/23", Value = "Secret question")]
    public string NewPasswordQuestion => this[nameof (NewPasswordQuestion)];

    /// <summary>phrase: Secret answer</summary>
    /// <value>Secret answer</value>
    [ResourceEntry("NewPasswordAnswer", Description = "phrase: Secret answer", LastModified = "2016/12/23", Value = "Secret answer")]
    public string NewPasswordAnswer => this[nameof (NewPasswordAnswer)];

    /// <summary>phrase: Password</summary>
    /// <value>Password</value>
    [ResourceEntry("Password", Description = "phrase: Password", LastModified = "2015/03/17", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>
    /// phrase: 'Change password question and answer' template
    /// </summary>
    /// <value>'Change password question and answer' template</value>
    [ResourceEntry("ChangePasswordQuestionAndAnswerTemplate", Description = "phrase: 'Change password question and answer' template", LastModified = "2015/03/18", Value = "'Change password question and answer' template")]
    public string ChangePasswordQuestionAndAnswerTemplate => this[nameof (ChangePasswordQuestionAndAnswerTemplate)];

    /// <summary>
    /// Success! You have successfuly entered your security answer.
    /// </summary>
    /// <value>Success! You have successfuly entered your security answer.</value>
    [ResourceEntry("SuccessResetPasswordEmailSend", Description = "Reset password email was send.", LastModified = "2015/03/21", Value = "You sent a request to reset your password to {0}\nPlease use the link provided in your email to reset the password for your account.")]
    public string SuccessResetPasswordEmailSend => this[nameof (SuccessResetPasswordEmailSend)];

    /// <summary>
    /// phrase: Unhandled error occured when saving the profile.
    /// </summary>
    /// <value> An unhandled error occured when saving the profile. Please check your profile details.</value>
    [ResourceEntry("UnhandledProfileError", Description = "phrase: An unhandled error occured when saving the profile. Please check your profile details.", LastModified = "2015/06/18", Value = "An unhandled error occured when saving the profile. Please check your profile details.")]
    public string UnhandledProfileError => this[nameof (UnhandledProfileError)];

    /// <summary>Phrase: User management.</summary>
    /// <value>User management</value>
    [ResourceEntry("UserManagement", Description = "Phrase: User management.", LastModified = "2017/11/22", Value = "User management")]
    public string UserManagement => this[nameof (UserManagement)];

    /// <summary>Gets External Link: User management.</summary>
    /// <value>https://www.progress.com/documentation/sitefinity-cms/overview-users</value>
    [ResourceEntry("ExternalLinkUserManagement", Description = "External Link: User management", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-users")]
    public string ExternalLinkUserManagement => this[nameof (ExternalLinkUserManagement)];

    /// <summary>Phrase: Role management.</summary>
    /// <value>Role management</value>
    [ResourceEntry("RoleManagement", Description = "Phrase: Role management.", LastModified = "2017/11/22", Value = "Role management")]
    public string RoleManagement => this[nameof (RoleManagement)];

    /// <summary>Gets External Link: Role management.</summary>
    /// <value>https://www.progress.com/documentation/sitefinity-cms/overview-roles</value>
    [ResourceEntry("ExternalLinkRoleManagement", Description = "External Link: Role management", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-roles")]
    public string ExternalLinkRoleManagement => this[nameof (ExternalLinkRoleManagement)];

    /// <summary>Phrase: Permissions management.</summary>
    /// <value>Permissions management</value>
    [ResourceEntry("PermissionsManagement", Description = "Phrase: Permissions management.", LastModified = "2017/11/22", Value = "Permissions management")]
    public string PermissionsManagement => this[nameof (PermissionsManagement)];

    /// <summary>Gets External Link: Permissions management.</summary>
    /// <value>https://www.progress.com/documentation/sitefinity-cms/overview-permissions</value>
    [ResourceEntry("ExternalLinkPermissionsManagement", Description = "External Link: Permissions management", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-permissions")]
    public string ExternalLinkPermissionsManagement => this[nameof (ExternalLinkPermissionsManagement)];

    /// <summary>Gets External Link: Manage users</summary>
    [ResourceEntry("ExternalLinkManageUsers", Description = "External Link: Manage users", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-users")]
    public string ExternalLinkManageUsers => this[nameof (ExternalLinkManageUsers)];

    /// <summary>Gets External Link: Manage roles</summary>
    [ResourceEntry("ExternalLinkManageRoles", Description = "External Link: Manage roles", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-roles")]
    public string ExternalLinkManageRoles => this[nameof (ExternalLinkManageRoles)];

    /// <summary>Gets External Link: Manage permissions</summary>
    [ResourceEntry("ExternalLinkManagePermissions", Description = "External Link: Manage permissions", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-permissions")]
    public string ExternalLinkManagePermissions => this[nameof (ExternalLinkManagePermissions)];

    /// <summary>Gets External Link Label: Manage users</summary>
    [ResourceEntry("ExternalLinkLabelManageUsers", Description = "External Link Label: Manage users", LastModified = "2018/08/29", Value = "Manage users")]
    public string ExternalLinkLabelManageUsers => this[nameof (ExternalLinkLabelManageUsers)];

    /// <summary>Gets External Link Label: Manage roles</summary>
    [ResourceEntry("ExternalLinkLabelManageRoles", Description = "External Link Label: Manage roles", LastModified = "2018/08/29", Value = "Manage roles")]
    public string ExternalLinkLabelManageRoles => this[nameof (ExternalLinkLabelManageRoles)];

    /// <summary>Gets External Link Label: Manage permissions</summary>
    [ResourceEntry("ExternalLinkLabelManagePermissions", Description = "External Link Label: Manage permissions", LastModified = "2018/08/29", Value = "Manage permissions")]
    public string ExternalLinkLabelManagePermissions => this[nameof (ExternalLinkLabelManagePermissions)];
  }
}
