// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecurityResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("SecurityResources", ResourceClassId = "SecurityResources")]
  public sealed class SecurityResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Security.SecurityResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public SecurityResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Security.SecurityResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public SecurityResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Help</summary>
    [ResourceEntry("SecurityResourcesTitle", Description = "The title of this class.", LastModified = "2009/05/13", Value = "Security")]
    public string SecurityResourcesTitle => this[nameof (SecurityResourcesTitle)];

    /// <summary>Help</summary>
    [ResourceEntry("SecurityResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/05/13", Value = "Security")]
    public string SecurityResourcesTitlePlural => this[nameof (SecurityResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("SecurityResourcesDescription", Description = "The description of this class.", LastModified = "2009/05/13", Value = "Contains localizable resources for security user interface.")]
    public string SecurityResourcesDescription => this[nameof (SecurityResourcesDescription)];

    /// <summary>
    /// Title for permission list of actions for content (ContentActionPermissionsList control)
    /// </summary>
    [ResourceEntry("ConfirmInheritPermissions", Description = "Content Permissions Title", LastModified = "2010/04/14", Value = "Are you sure you want to inherit permissions and lose all your custom changes?")]
    public string ConfirmInheritPermissions => this[nameof (ConfirmInheritPermissions)];

    /// <summary>
    /// Title for permission list of actions for content (ContentActionPermissionsList control)
    /// </summary>
    [ResourceEntry("ContentActionPermissionsListTitle", Description = "Content Permissions Title", LastModified = "2009/10/07", Value = "Content Permissions")]
    public string ContentActionPermissionsListTitle => this[nameof (ContentActionPermissionsListTitle)];

    /// <summary>
    /// Title for permission list of actions for global permissions (ContentActionPermissionsList control)
    /// </summary>
    [ResourceEntry("GlobalActionPermissionsListTitle", Description = "Global Permissions Title", LastModified = "2009/11/19", Value = "Global Permissions")]
    public string GlobalActionPermissionsListTitle => this[nameof (GlobalActionPermissionsListTitle)];

    /// <summary>
    /// Text for the "allowed users" label in a list of permissions
    /// </summary>
    [ResourceEntry("AllowedUsersPermissionsListTitle", Description = "Text for the allowed users label in a list of permissions", LastModified = "2009/10/07", Value = "Allowed Users:")]
    public string AllowedUsersPermissionsListTitle => this[nameof (AllowedUsersPermissionsListTitle)];

    /// <summary>
    /// Text for the "denied users" label in a list of permissions
    /// </summary>
    [ResourceEntry("DeniedUsersPermissionsListTitle", Description = "Text for the denied users label in a list of permissions", LastModified = "2009/10/07", Value = "Denied Users:")]
    public string DeniedUsersPermissionsListTitle => this[nameof (DeniedUsersPermissionsListTitle)];

    /// <summary>
    /// Text for the "Change" link in a list of permissions (for setting permissions)
    /// </summary>
    [ResourceEntry("ChangePermissionsLinkButtonText", Description = "Text for the Change link in a list of permissions (for setting permissions)", LastModified = "2009/10/07", Value = "Change")]
    public string ChangePermissionsLinkButtonText => this[nameof (ChangePermissionsLinkButtonText)];

    /// <summary>
    /// Text for the "Allowed users" title in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("AllowedUsersTitle", Description = "Text for the 'Allowed users' title in a users selection dialog, for a specific action", LastModified = "2009/10/12", Value = "Who can do this?")]
    public string AllowedUsersTitle => this[nameof (AllowedUsersTitle)];

    /// <summary>
    /// Text for the "Specific selected users" radio button in the users selection dialog, for a specific action. When not selected
    /// </summary>
    [ResourceEntry("SpecificSelectedUsersInactive", Description = "Text for the 'Specific selected users' radio button in a users selection dialog, for a specific action. When not selected", LastModified = "2010/02/26", Value = "Selected roles or users <span class='sfNote'>(plus administrator)</span>...")]
    public string SpecificSelectedUsersInactive => this[nameof (SpecificSelectedUsersInactive)];

    /// <summary>
    /// Text for the "Specific selected users" radio button in the users selection dialog, for a specific action. When selected
    /// </summary>
    [ResourceEntry("SpecificSelectedUsersActive", Description = "Text for the 'Specific selected users' radio button in a users selection dialog, for a specific action. When selected", LastModified = "2010/02/26", Value = "Selected roles or users <span class='sfNote'>(plus administrator)</span>:")]
    public string SpecificSelectedUsersActive => this[nameof (SpecificSelectedUsersActive)];

    /// <summary>
    /// Text for the "Specific denied users" option in the users selection dialog, for a specific action, when active
    /// </summary>
    [ResourceEntry("SpecificDeniedUsersActive", Description = "Text for the 'Specific denied users' option in the users selection dialog, for a specific action, when active", LastModified = "2010/02/26", Value = "Explicitly deny this to selected roles and users:")]
    public string SpecificDeniedUsersActive => this[nameof (SpecificDeniedUsersActive)];

    /// <summary>Word: Advanced</summary>
    [ResourceEntry("WordAdvanced", Description = "Word: Advancd", LastModified = "2010/02/26", Value = "Advanced")]
    public string WordAdvanced => this[nameof (WordAdvanced)];

    /// <summary>
    /// Text for the "Specific denied users" option in the users selection dialog, for a specific action, when inactive
    /// </summary>
    [ResourceEntry("SpecificDeniedUsersInactive", Description = "Text for the 'Specific denied users' option in the users selection dialog, for a specific action, when inactive", LastModified = "2010/02/26", Value = "Explicitly deny this to selected  roles and users...")]
    public string SpecificDeniedUsersInactive => this[nameof (SpecificDeniedUsersInactive)];

    /// <summary>
    /// Text for the "Select specific denied users" link-button in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("SelectSpecificDeniedUsers", Description = "Text for the 'Select specific denied users' link-button in the users selection dialog, for a specific action", LastModified = "2009/10/19", Value = "Select")]
    public string SelectSpecificDeniedUsers => this[nameof (SelectSpecificDeniedUsers)];

    /// <summary>
    /// Text for the "Close" link, which hides the users/roles selection box in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("CloseUsersSelectionBox", Description = "Text for the 'Close' link, which hides the users/roles selection box in the users selection dialog, for a specific action", LastModified = "2009/10/19", Value = "Close")]
    public string CloseUsersSelectionBox => this[nameof (CloseUsersSelectionBox)];

    /// <summary>
    /// Text for the "Save" link, which saves data and hides the users/roles selection box in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("SaveAndCloseUsersSelectionBox", Description = "Text for the 'Close' link, which saves data and hides the users/roles selection box in the users selection dialog, for a specific action", LastModified = "2009/10/20", Value = "Save")]
    public string SaveAndCloseUsersSelectionBox => this[nameof (SaveAndCloseUsersSelectionBox)];

    /// <summary>
    /// Text for the "Open" link, which hides the users/roles selection box in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("OpenUsersSelectionBox", Description = "Text for the 'Open' link, which hides the users/roles selection box in the users selection dialog, for a specific action", LastModified = "2009/10/19", Value = "Select users or roles")]
    public string OpenUsersSelectionBox => this[nameof (OpenUsersSelectionBox)];

    /// <summary>
    /// Represents 'users' selection option in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("UsersSectionTitle", Description = "Represents 'users' selection option in the users selection dialog, for a specific action", LastModified = "2009/10/12", Value = "Users")]
    public string UsersSectionTitle => this[nameof (UsersSectionTitle)];

    /// <summary>
    /// Represents 'roles' selection option in the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("RolesSectionTitle", Description = "Represents 'roles' selection option in the users selection dialog, for a specific action", LastModified = "2009/10/12", Value = "Roles")]
    public string RolesSectionTitle => this[nameof (RolesSectionTitle)];

    /// <summary>
    /// Close (or Cancel) button for  the users selection dialog, for a specific action
    /// </summary>
    [ResourceEntry("ActionUserSelectionCancelBtn", Description = "Close (or Cancel) button for  the users selection dialog, for a specific action", LastModified = "2009/10/12", Value = "Cancel")]
    public string ActionUserSelectionCancelBtn => this[nameof (ActionUserSelectionCancelBtn)];

    /// <summary>
    /// Text for label for selection specific users or roles for a specific action permission
    /// </summary>
    [ResourceEntry("SelectUsersOrRolesText", Description = "Text for label for selection specific users or roles for a specific action permission", LastModified = "2009/10/13", Value = "Select")]
    public string SelectUsersOrRolesText => this[nameof (SelectUsersOrRolesText)];

    /// <summary>
    /// "All Providers" text (users and roles) for a specific action permission
    /// </summary>
    [ResourceEntry("AllProvidersText", Description = "All Providers text (users and roles) for a specific action permission", LastModified = "2009/10/13", Value = "All Providers")]
    public string AllProvidersText => this[nameof (AllProvidersText)];

    /// <summary>Allow title text for the UserPermissionsList control</summary>
    [ResourceEntry("AllowUserPermissionsList", Description = "Allow title text for the UserPermissionsList control", LastModified = "2009/10/27", Value = "Allow")]
    public string AllowUserPermissionsList => this[nameof (AllowUserPermissionsList)];

    /// <summary>Deny title text for the UserPermissionsList control</summary>
    [ResourceEntry("DenyUserPermissionsList", Description = "Deny title text for the UserPermissionsList control", LastModified = "2009/10/27", Value = "Deny")]
    public string DenyUserPermissionsList => this[nameof (DenyUserPermissionsList)];

    /// <summary>
    /// Text for the "Change" link in a list of permissions on UserPermissionsList
    /// </summary>
    [ResourceEntry("ChangeUserPermissionsLinkButtonText", Description = "Text for the Change link in a list of permissions on UserPermissionsList", LastModified = "2009/10/07", Value = "Change")]
    public string ChangeUserPermissionsLinkButtonText => this[nameof (ChangeUserPermissionsLinkButtonText)];

    /// <summary>
    /// Text for the "Show users" link/label in a list of permissions on UserPermissionsList (for specific user/role selection)
    /// </summary>
    [ResourceEntry("UsrPermissionListUsersSectionTitle", Description = "Text for the 'Show users' link/label in a list of permissions on UserPermissionsList (for specific user/role selection)", LastModified = "2009/11/02", Value = "Users")]
    public string UsrPermissionListUsersSectionTitle => this[nameof (UsrPermissionListUsersSectionTitle)];

    /// <summary>
    /// Text for the "Show users" link/label in a list of permissions on UserPermissionsList (for specific user/role selection)
    /// </summary>
    [ResourceEntry("UsrPermissionListRolesSectionTitle", Description = "Text for the 'Show roles' link/label in a list of permissions on UserPermissionsList (for specific user/role selection)", LastModified = "2009/11/02", Value = "Roles")]
    public string UsrPermissionListRolesSectionTitle => this[nameof (UsrPermissionListRolesSectionTitle)];

    /// <summary>
    /// Text for the "Show advanced options" link in a User Permissions Editor control
    /// </summary>
    [ResourceEntry("UserPermissionsEditorShowAdvancedOptions", Description = "Text for the 'Show advanced options' link in a User Permissions Editor control", LastModified = "2009/10/29", Value = "Show Advanced Options")]
    public string UserPermissionsEditorShowAdvancedOptions => this[nameof (UserPermissionsEditorShowAdvancedOptions)];

    /// <summary>
    /// Text for the "Hide advanced options" link in a User Permissions Editor control
    /// </summary>
    [ResourceEntry("UserPermissionsEditorHideAdvancedOptions", Description = "Text for the 'Hide advanced options' link in a User Permissions Editor control", LastModified = "2009/10/29", Value = "Hide Advanced Options")]
    public string UserPermissionsEditorHideAdvancedOptions => this[nameof (UserPermissionsEditorHideAdvancedOptions)];

    /// <summary>
    /// Close (or Cancel) button for  the users selection dialog, for a User Permissions Editor control
    /// </summary>
    [ResourceEntry("UserPermissionsEditorCancelBtn", Description = "Close (or Cancel) button for  the users selection dialog, for a User Permissions Editor control", LastModified = "2009/10/30", Value = "Cancel")]
    public string UserPermissionsEditorCancelBtn => this[nameof (UserPermissionsEditorCancelBtn)];

    /// <summary>
    /// Save button for  the users selection dialog, for a User Permissions Editor control
    /// </summary>
    [ResourceEntry("UserPermissionsEditorSaveBtn", Description = "Close (or Cancel) button for  the users selection dialog, for a User Permissions Editor control", LastModified = "2009/10/30", Value = "Save")]
    public string UserPermissionsEditorSaveBtn => this[nameof (UserPermissionsEditorSaveBtn)];

    /// <summary>
    /// Window Title for editing permission actions per user (UserPermissionsEditor dialog)
    /// </summary>
    [ResourceEntry("UserPermissionsEditorPerUserTitle", Description = "Window Title for editing permission actions per user (UserPermissionsEditor dialog)", LastModified = "2009/11/05", Value = "Set permissions per user")]
    public string UserPermissionsEditorPerUserTitle => this[nameof (UserPermissionsEditorPerUserTitle)];

    /// <summary>
    /// Window Title for editing permission actions per role (UserPermissionsEditor dialog)
    /// </summary>
    [ResourceEntry("UserPermissionsEditorPerRoleTitle", Description = "Window Title for editing permission actions per role (UserPermissionsEditor dialog)", LastModified = "2009/11/05", Value = "Set permissions per role")]
    public string UserPermissionsEditorPerRoleTitle => this[nameof (UserPermissionsEditorPerRoleTitle)];

    /// <summary>
    /// Window Title for editing permitted users per action (ActionUsersSelection dialog)
    /// </summary>
    [ResourceEntry("ActionUsersSelectionTitle", Description = "Window Title for editing permitted users per action (ActionUsersSelection dialog)", LastModified = "2009/11/05", Value = "Set Permitted Users Per Action")]
    public string ActionUsersSelectionTitle => this[nameof (ActionUsersSelectionTitle)];

    /// <summary>Administrators Only - for permissions GUI</summary>
    [ResourceEntry("AdministratorsOnly", Description = "Administrators Only - for permissions GUI", LastModified = "2009/11/25", Value = "Administrators only")]
    public string AdministratorsOnly => this[nameof (AdministratorsOnly)];

    /// <summary>Permissions Global Tab Selector: By Section Title</summary>
    [ResourceEntry("PermissionsBySection", Description = "Permissions Global Tab Selector: By Section Title", LastModified = "2009/11/19", Value = "by Section")]
    public string PermissionsBySection => this[nameof (PermissionsBySection)];

    /// <summary>Permissions Global Tab Selector: By Section Title</summary>
    [ResourceEntry("PermissionsByPrincipal", Description = "Permissions Global Tab Selector: By Principal Title", LastModified = "2009/11/19", Value = "by Role or User")]
    public string PermissionsByPrincipal => this[nameof (PermissionsByPrincipal)];

    /// <summary>Permissions Global Tab Selector: By User Title</summary>
    [ResourceEntry("PermissionsByUser", Description = "Permissions Global Tab Selector: By User Title", LastModified = "2016/12/06", Value = "by User")]
    public string PermissionsByUser => this[nameof (PermissionsByUser)];

    /// <summary>Permissions Global Tab Selector: By User Title</summary>
    [ResourceEntry("PermissionsByRole", Description = "Permissions Global Tab Selector: By Role Title", LastModified = "2016/12/06", Value = "by Role")]
    public string PermissionsByRole => this[nameof (PermissionsByRole)];

    /// <summary>Module provider label text</summary>
    [ResourceEntry("ModuleProviderLabel", Description = "Module provider label text", LastModified = "2011/01/17", Value = "Module provider:")]
    public string ModuleProviderLabel => this[nameof (ModuleProviderLabel)];

    /// <summary>Phrase: No users or groups selected.</summary>
    [ResourceEntry("NoUsersOrGroupsAreSelected", Description = "Phrase: No users or groups selected.", LastModified = "2009/12/10", Value = "No users or groups selected.")]
    public string NoUsersOrGroupsAreSelected => this[nameof (NoUsersOrGroupsAreSelected)];

    /// <summary>
    /// Phrase: So only the Administrator will have permissions.
    /// </summary>
    [ResourceEntry("SoOnlyTheAdministratorWillHavePermissions", Description = "Phrase: So only the Administrator will have permissions.", LastModified = "2009/12/10", Value = "So only the Administrator will have permissions")]
    public string SoOnlyTheAdministratorWillHavePermissions => this[nameof (SoOnlyTheAdministratorWillHavePermissions)];

    /// <summary>Word: Done. used in the module permissions page</summary>
    [ResourceEntry("doneEditingPermissions", Description = "Word: Done. used in the module permissions page", LastModified = "2009/12/19", Value = "Done")]
    public string DoneEditingPermissions => this["doneEditingPermissions"];

    /// <summary>
    /// Sentence: This object's permissions are inherited from the its parent.
    /// </summary>
    [ResourceEntry("ThisObjectsPermissionsAreInherited", Description = "Label: This object's permissions are inherited from its parent.", LastModified = "2010/04/08", Value = "This object's permissions are inherited from its parent.")]
    public string ThisObjectsPermissionsAreInherited => this[nameof (ThisObjectsPermissionsAreInherited)];

    /// <summary>Phrase: Override the inherited permissions</summary>
    [ResourceEntry("OverrideTheInheritedPermissions", Description = "Phrase: Override the inherited permissions", LastModified = "2010/04/08", Value = "Override the inherited permissions")]
    public string OverrideTheInheritedPermissions => this[nameof (OverrideTheInheritedPermissions)];

    /// <summary>
    /// Sentence: This object's permissions override its parent's.
    /// </summary>
    [ResourceEntry("ThisObjectsPermissionsOverride", Description = "Sentence: This object's permissions override its parent's.", LastModified = "2010/04/08", Value = "This object's permissions override its parent's.")]
    public string ThisObjectsPermissionsOverride => this[nameof (ThisObjectsPermissionsOverride)];

    /// <summary>Phrase: Inherit permissions from parent object</summary>
    [ResourceEntry("InheritPermissionsFromParentObject", Description = "Phrase: Inherit permissions from parent object", LastModified = "2010/04/08", Value = "Inherit permissions from parent object")]
    public string InheritPermissionsFromParentObject => this[nameof (InheritPermissionsFromParentObject)];

    /// <summary>
    /// Phrase: This item inherits permissions from its parent.
    /// </summary>
    [ResourceEntry("ThisItemInheritsPermissionsFromItsParent", Description = "Phrase: This item inherits permissions from its parent.", LastModified = "2010/04/08", Value = "This item inherits permissions from its parent.")]
    public string ThisItemInheritsPermissionsFromItsParent => this[nameof (ThisItemInheritsPermissionsFromItsParent)];

    /// <summary>Button: Break inheritance</summary>
    [ResourceEntry("BreakInheritance", Description = "Button: Break inheritance", LastModified = "2010/04/08", Value = "Break inheritance")]
    public string BreakInheritance => this[nameof (BreakInheritance)];

    /// <summary>
    /// Phrase: in order to set permissions different from parent ones..
    /// </summary>
    [ResourceEntry("InOrderToSetDiferentPermissionsThanParentOnes", Description = "End of phrase: in order to set permissions different from those of the parent.", LastModified = "2010/04/08", Value = "in order to set permissions different from those of the parent.")]
    public string InOrderToSetDiferentPermissionsThanParentOnes => this[nameof (InOrderToSetDiferentPermissionsThanParentOnes)];

    /// <summary>
    /// Phrase: This item has permissions different from its parent.
    /// </summary>
    [ResourceEntry("ThisItemHasPermissionsDifferentFromItsParent", Description = "Phrase: This item has permissions different from its parent.", LastModified = "2010/04/08", Value = "This item has permissions different from its parent.")]
    public string ThisItemHasPermissionsDifferentFromItsParent => this[nameof (ThisItemHasPermissionsDifferentFromItsParent)];

    /// <summary>Button: Inherit permissions from parent</summary>
    [ResourceEntry("InheritPermissionsFromItsParent", Description = "Button: Inherit permissions from parent", LastModified = "2010/04/08", Value = "Inherit permissions from parent")]
    public string InheritPermissionsFromItsParent => this[nameof (InheritPermissionsFromItsParent)];

    /// <summary>word: Phrase: Permissions for all item types/names</summary>
    [ResourceEntry("GeneralPermissionsTitle", Description = "Phrase: Permissions for {0}", LastModified = "2010/07/16", Value = "Permissions for {0}")]
    public string GeneralPermissionsTitle => this[nameof (GeneralPermissionsTitle)];

    /// <summary>Gets the view backend permissions default message.</summary>
    [ResourceEntry("ViewBackendPermissionsDefaultMessage", Description = "Phrase: Roles and users set in <i>Create</i>, <i>Modify</i> or <i>Delete</i> news have this permissions by default. You can add more below", LastModified = "2014/12/04", Value = "Roles and users set in <i>Create</i>, <i>Modify</i> or <i>Delete</i> news have this permissions by default. You can add more below")]
    public string ViewBackendPermissionsDefaultMessage => this[nameof (ViewBackendPermissionsDefaultMessage)];

    [ResourceEntry("ExternalLinkViewPermissionTurnedOffMessage", Description = "External Link: View permission is turned off message", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/change-filtering-by-view-permissions")]
    public string ExternalLinkViewPermissionTurnedOffMessage => this[nameof (ExternalLinkViewPermissionTurnedOffMessage)];

    [ResourceEntry("ViewPermissionTurnedOffMessage", Description = "Phrase: View permission is turned off. Any changes will not be applied. Learn more", LastModified = "2017/11/23", Value = "View permission is turned off. Any changes will not be applied. <a href=\"{0}\" target=\"_blank\">Learn more</a>")]
    public string ViewPermissionTurnedOffMessage => this[nameof (ViewPermissionTurnedOffMessage)];

    /// <summary>Phrase: Please wait... It may take a while.</summary>
    [ResourceEntry("PleaseWaitItMayTakeAWhile", Description = "Phrase: Please wait... It may take a while.", LastModified = "2016/07/21", Value = "Please wait... It may take a while")]
    public string PleaseWaitItMayTakeAWhile => this[nameof (PleaseWaitItMayTakeAWhile)];

    /// <summary>Application predefined role name: Everyone</summary>
    [ResourceEntry("Everyone", Description = "Application predefined role name: Everyone", LastModified = "2009/10/13", Value = "Everyone")]
    public string Everyone => this[nameof (Everyone)];

    /// <summary>
    /// Represents a predefined role that is automatically assigned to every request including anonymous ones.
    /// </summary>
    [ResourceEntry("EveryoneDescription", Description = "Application predefined role name: Everyone", LastModified = "2009/10/13", Value = "Represents a predefined role that is automatically assigned to every request including anonymous ones.")]
    public string EveryoneDescription => this[nameof (EveryoneDescription)];

    /// <summary>Application predefined role name: Anonymous</summary>
    [ResourceEntry("Anonymous", Description = "Application predefined role name: Anonymous", LastModified = "2009/10/13", Value = "Anonymous")]
    public string Anonymous => this[nameof (Anonymous)];

    /// <summary>
    /// Represents a predefined role that is automatically assigned to all unauthenticated requests.
    /// </summary>
    [ResourceEntry("AnonymousDescription", Description = "Application predefined role name: Anonymous", LastModified = "2009/10/13", Value = "Represents a predefined role that is automatically assigned to all unauthenticated requests.")]
    public string AnonymousDescription => this[nameof (AnonymousDescription)];

    /// <summary>Application predefined role name: Authenticated</summary>
    [ResourceEntry("Authenticated", Description = "Application predefined role name: Authenticated", LastModified = "2009/10/13", Value = "Authenticated")]
    public string Authenticated => this[nameof (Authenticated)];

    /// <summary>
    /// Represents a predefined role that is automatically assigned to all authenticated requests.
    /// </summary>
    [ResourceEntry("AuthenticatedDescription", Description = "Application predefined role name: Authenticated", LastModified = "2009/10/13", Value = "Represents a predefined role that is automatically assigned to all authenticated requests.")]
    public string AuthenticatedDescription => this[nameof (AuthenticatedDescription)];

    /// <summary>Application predefined role name: Owner</summary>
    [ResourceEntry("Owner", Description = "Application predefined role name: Owner", LastModified = "2009/10/13", Value = "Owner")]
    public string Owner => this[nameof (Owner)];

    /// <summary>
    /// Represents a predefined role that is automatically assigned to a user when permissions are checked against an item that the user owns.
    /// </summary>
    [ResourceEntry("OwnerDescription", Description = "Application predefined role name: Owner", LastModified = "2009/10/13", Value = "Represents a predefined role that is automatically assigned to a user when permissions are checked against an item that the user owns.")]
    public string OwnerDescription => this[nameof (OwnerDescription)];

    /// <summary>Application predefined role name: Administrators</summary>
    [ResourceEntry("Administrators", Description = "Application predefined role name: Administrators", LastModified = "2009/10/13", Value = "Administrators")]
    public string Administrators => this[nameof (Administrators)];

    /// <summary>
    /// Members of this role have full rights over the entire system. All rights are automatically granted to members of this role.
    /// </summary>
    [ResourceEntry("AdministratorsDescription", Description = "Application predefined role name: Administrators", LastModified = "2009/10/13", Value = "Members of this role have full rights over the entire system. All rights are automatically granted to members of this role.")]
    public string AdministratorsDescription => this[nameof (AdministratorsDescription)];

    /// <summary>Word: permissions</summary>
    [ResourceEntry("Permissions", Description = "Permissions", LastModified = "2009/11/4", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>Word: Users</summary>
    [ResourceEntry("Users", Description = "Users", LastModified = "2009/11/4", Value = "Users")]
    public string Users => this[nameof (Users)];

    /// <summary>word: role</summary>
    [ResourceEntry("Role", Description = "word: Role", LastModified = "2009/11/4", Value = "Role")]
    public string Role => this[nameof (Role)];

    /// <summary>word: Roles</summary>
    [ResourceEntry("Roles", Description = "word: Roles", LastModified = "2009/11/4", Value = "Roles")]
    public string Roles => this[nameof (Roles)];

    /// <summary>word: Username</summary>
    [ResourceEntry("Username", Description = "word: Username", LastModified = "2009/11/19", Value = "Username")]
    public string Username => this[nameof (Username)];

    /// <summary>word: Allowed</summary>
    [ResourceEntry("Allowed", Description = "word: Allowed", LastModified = "2009/11/13", Value = "Allowed")]
    public string Allowed => this[nameof (Allowed)];

    /// <summary>word: provider</summary>
    [ResourceEntry("Provider", Description = "word: provider", LastModified = "2009/11/12", Value = "Provider")]
    public string Provider => this[nameof (Provider)];

    /// <summary>word: username / role</summary>
    [ResourceEntry("UsernameRole", Description = "word: email / role", LastModified = "2017/2/9", Value = "Email / Role")]
    public string UsernameRole => this[nameof (UsernameRole)];

    /// <summary>word: name / email</summary>
    [ResourceEntry("NameEmail", Description = "word: name / email", LastModified = "2017/2/9", Value = "Name")]
    public string NameEmail => this[nameof (NameEmail)];

    /// <summary>word: Registration</summary>
    [ResourceEntry("Registration", Description = "word: Registration", LastModified = "2009/11/13", Value = "Registration")]
    public string Registration => this[nameof (Registration)];

    /// <summary>Manage Users</summary>
    [ResourceEntry("ManageUsers", Description = "The title of ManageUsers security action.", LastModified = "2009/11/19", Value = "Manage Users")]
    public string ManageUsers => this[nameof (ManageUsers)];

    /// <summary>Allows or denies user account management.</summary>
    [ResourceEntry("ManageUsersDescription", Description = "Security action description.", LastModified = "2009/12/11", Value = "Allows or denies user account management.")]
    public string ManageUsersDescription => this[nameof (ManageUsersDescription)];

    /// <summary>Manage Roles</summary>
    [ResourceEntry("ManageRoles", Description = "The title of ManageRoles security action.", LastModified = "2009/11/19", Value = "Manage Roles")]
    public string ManageRoles => this[nameof (ManageRoles)];

    /// <summary>Allows or denies role management.</summary>
    [ResourceEntry("ManageRolesDescription", Description = "Security action description.", LastModified = "2009/12/11", Value = "Allows or denies role management.")]
    public string ManageRolesDescription => this[nameof (ManageRolesDescription)];

    /// <summary>View Permissions</summary>
    [ResourceEntry("ViewPermissions", Description = "The title of ViewPermissions security action.", LastModified = "2009/04/13", Value = "View Permissions")]
    public string ViewPermissions => this[nameof (ViewPermissions)];

    /// <summary>Allows or denies viewing permissions.</summary>
    [ResourceEntry("ViewPermissionsDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies viewing permissions.")]
    public string ViewPermissionsDescription => this[nameof (ViewPermissionsDescription)];

    /// <summary>Label: Comments Permissions</summary>
    [ResourceEntry("CommentsPermissions", Description = "Permissions title.", LastModified = "2009/08/19", Value = "Comments Permissions")]
    public string CommentsPermissions => this[nameof (CommentsPermissions)];

    /// <summary>Change Permissions</summary>
    [ResourceEntry("ChangePermissions", Description = "The title of Change Permissions security action.", LastModified = "2009/04/13", Value = "Change {0} permissions")]
    public string ChangePermissions => this[nameof (ChangePermissions)];

    /// <summary>
    /// Allows or denies changing the permissions of a data item.
    /// </summary>
    [ResourceEntry("ChangePermissionsDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies changing the permissions of a data item.")]
    public string ChangePermissionsDescription => this[nameof (ChangePermissionsDescription)];

    /// <summary>View Configurations</summary>
    [ResourceEntry("ViewConfigurations", Description = "The title of ViewConfigurations security action.", LastModified = "2009/04/13", Value = "View Configurations")]
    public string ViewConfigurations => this[nameof (ViewConfigurations)];

    /// <summary>Allows or denies viewing system configurations.</summary>
    [ResourceEntry("ViewConfigurationsDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies viewing system configurations.")]
    public string ViewConfigurationsDescription => this[nameof (ViewConfigurationsDescription)];

    /// <summary>Change Configurations</summary>
    [ResourceEntry("ChangeConfigurations", Description = "The title of ChangeConfigurations security action.", LastModified = "2009/04/13", Value = "Change Configurations")]
    public string ChangeConfigurations => this[nameof (ChangeConfigurations)];

    /// <summary>Allows or denies changing all system configurations.</summary>
    [ResourceEntry("ChangeConfigurationsDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies changing all system configurations.")]
    public string ChangeConfigurationsDescription => this[nameof (ChangeConfigurationsDescription)];

    /// <summary>Manage Labels</summary>
    [ResourceEntry("ManageLabels", Description = "The title of ManageLabels security action.", LastModified = "2009/11/19", Value = "Manage Labels")]
    public string ManageLabels => this[nameof (ManageLabels)];

    /// <summary>Allows or denies labels and messages management.</summary>
    [ResourceEntry("ManageLabelsDescription", Description = "Security action description.", LastModified = "2009/12/11", Value = "Allows or denies labels and messages management.")]
    public string ManageLabelsDescription => this[nameof (ManageLabelsDescription)];

    /// <summary>Label: Manage Files</summary>
    [ResourceEntry("ManageFiles", Description = "The title of ManageFiles security action.", LastModified = "2009/11/19", Value = "Manage Files")]
    public string ManageFiles => this[nameof (ManageFiles)];

    /// <summary>
    /// Allows or denies the access to the backend area of the site.
    /// </summary>
    [ResourceEntry("ManageFilesDescription", Description = "Security action description.", LastModified = "2009/12/11", Value = "Allows or denies the permission to manage files.")]
    public string ManageFilesDescription => this[nameof (ManageFilesDescription)];

    /// <summary>Manage Licenses</summary>
    [ResourceEntry("ManageLicenses", Description = "The title of ManageLicenses security action.", LastModified = "2009/11/19", Value = "Manage Licenses")]
    public string ManageLicenses => this[nameof (ManageLicenses)];

    /// <summary>Allows or denies the permission to manage licenses.</summary>
    [ResourceEntry("ManageLicensesDescription", Description = "Security action description.", LastModified = "2009/12/11", Value = "Allows or denies the permission to manage licenses.")]
    public string ManageLicensesDescription => this[nameof (ManageLicensesDescription)];

    /// <summary>
    /// Label: The default membership provider for frontend (public) users.
    /// </summary>
    [ResourceEntry("FrontendUsersDescription", Description = "Description of the default backend role provider.", LastModified = "2009/05/19", Value = "The default membership provider for frontend (public) users.")]
    public string FrontendUsersDescription => this[nameof (FrontendUsersDescription)];

    /// <summary>Label: Access Backend</summary>
    [ResourceEntry("AccessBackend", Description = "The title of AccessBackend security action.", LastModified = "2009/05/19", Value = "Access Backend")]
    public string AccessBackend => this[nameof (AccessBackend)];

    /// <summary>Backend Permissions</summary>
    [ResourceEntry("BackendPermissions", Description = "Permission title.", LastModified = "2009/05/19", Value = "Backend Permissions")]
    public string BackendPermissions => this[nameof (BackendPermissions)];

    /// <summary>
    /// Allows or denies the access to the backend area of the site.
    /// </summary>
    [ResourceEntry("AccessBackendDescription", Description = "Security action description.", LastModified = "2009/05/19", Value = "Allows or denies the access to the backend area of the site.")]
    public string AccessBackendDescription => this[nameof (AccessBackendDescription)];

    /// <summary>Represents general permissions for backend access.</summary>
    [ResourceEntry("BackendPermissionsDescription", Description = "Permission description.", LastModified = "2009/05/19", Value = "Represents general permissions for backend access.")]
    public string BackendPermissionsDescription => this[nameof (BackendPermissionsDescription)];

    /// <summary>Represents general permissions for backend access.</summary>
    [ResourceEntry("UseBrowseAndEdit", Description = "Permission title.", LastModified = "2011/01/03", Value = "Use in-line editing")]
    public string UseBrowseAndEdit => this[nameof (UseBrowseAndEdit)];

    /// <summary>Represents general permissions for backend access.</summary>
    [ResourceEntry("UseBrowseAndEditDescription", Description = "Permission description.", LastModified = "2011/01/03", Value = "Represents general permissions for using the in-line editing functionality.")]
    public string UseBrowseAndEditDescription => this[nameof (UseBrowseAndEditDescription)];

    /// <summary>Represents general permissions for backend access.</summary>
    [ResourceEntry("ManageUserProfiles", Description = "Permission title.", LastModified = "2011/01/28", Value = "Manage user profiles")]
    public string ManageUserProfiles => this[nameof (ManageUserProfiles)];

    /// <summary>Represents general permissions for backend access.</summary>
    [ResourceEntry("ManageUserProfilesDescription", Description = "Permission description.", LastModified = "2011/01/28", Value = "Represents general permissions for managing user profiles and profile types.")]
    public string ManageUserProfilesDescription => this[nameof (ManageUserProfilesDescription)];

    /// <summary>Translated text, similar to "Manage backend pages"</summary>
    /// <value>Title of the 'ManageBackendPages' security action</value>
    [ResourceEntry("ManageBackendPages", Description = "Title of the 'ManageBackendPages' security action", LastModified = "2011/01/14", Value = "Manage backend pages")]
    public string ManageBackendPages => this[nameof (ManageBackendPages)];

    /// <summary>
    /// Translated text, similar to "Security action required to edit backend pages via the UI"
    /// </summary>
    /// <value>Description of the 'ManageBackendPages' security action</value>
    [ResourceEntry("ManageBackendPagesDescription", Description = "Description of the 'ManageBackendPages' security action", LastModified = "2011/01/14", Value = "Security action required to edit backend pages via the UI")]
    public string ManageBackendPagesDescription => this[nameof (ManageBackendPagesDescription)];

    /// <summary>
    /// Translated text, similar to "Access publishing system backend pages"
    /// </summary>
    /// <value>Title of the 'ManagePublishingSystem' security action</value>
    [ResourceEntry("ManagePublishingSystem", Description = "Title of the 'ManagePublishingSystem' security action", LastModified = "2011/01/17", Value = "Access publishing system backend pages")]
    public string ManagePublishingSystem => this[nameof (ManagePublishingSystem)];

    /// <summary>
    /// Translated text, similar to "Security action required to access publishing system-related pages via the Backend UI"
    /// </summary>
    /// <value>Description of the 'ManagePublishingSystem' security action</value>
    [ResourceEntry("ManagePublishingSystemDescription", Description = "Description of the 'ManagePublishingSystem' security action", LastModified = "2011/01/17", Value = "Security action required to access publishing system-related pages via the Backend UI")]
    public string ManagePublishingSystemDescription => this[nameof (ManagePublishingSystemDescription)];

    /// <summary>
    /// Translated text, similar to "Access search &amp; indexing backend pages"
    /// </summary>
    /// <value>Title of the 'ManageSearchIndices' security action</value>
    [ResourceEntry("ManageSearchIndices", Description = "Title of the 'ManagePublishingSystem' security action", LastModified = "2011/01/24", Value = "Access search & indexing backend pages")]
    public string ManageSearchIndices => this[nameof (ManageSearchIndices)];

    /// <summary>
    /// Translated text, similar to "Security action required to access the Backend UI for managing search indices"
    /// </summary>
    /// <value>Description of the 'ManageSearchIndices' security action</value>
    [ResourceEntry("ManageSearchIndicesDescription", Description = "Description of the 'ManageSearchIndices' security action", LastModified = "2011/01/24", Value = "Security action required to access the Backend UI for managing search indices")]
    public string ManageSearchIndicesDescription => this[nameof (ManageSearchIndicesDescription)];

    /// <summary>Translated text, similar to "Access widgets editor"</summary>
    /// <value>Title of the 'ManageWidgets' security action</value>
    [ResourceEntry("ManageWidgets", Description = "Title of the 'ManageWidgets' security action", LastModified = "2011/01/24", Value = "Access widgets editor")]
    public string ManageWidgets => this[nameof (ManageWidgets)];

    /// <summary>
    /// Translated text, similar to "Security action required to access the Backend UI for editing/designing widgets"
    /// </summary>
    /// <value>Description of the 'ManageWidgets' security action</value>
    [ResourceEntry("ManageWidgetsDescription", Description = "Description of the 'ManageSearchIndices' security action", LastModified = "2011/01/24", Value = "Security action required to access the Backend UI for editing/designing widgets")]
    public string ManageWidgetsDescription => this[nameof (ManageWidgetsDescription)];

    /// <summary>
    /// Translated text, similar to "Access newsletters module"
    /// </summary>
    /// <value>Title of the 'ManageNewsletters' security action</value>
    [ResourceEntry("ManageNewsletters", Description = "Title of the 'ManageNewsletters' security action", LastModified = "2011/10/28", Value = "Access Email campaigns module")]
    public string ManageNewsletters => this[nameof (ManageNewsletters)];

    /// <summary>
    /// Translated text, similar to "Security action that allows access to the backend pages of the newsletters module"
    /// </summary>
    /// <value>Description of the 'ManageNewsletters' security action</value>
    [ResourceEntry("ManageNewslettersDescription", Description = "Description of the 'ManageNewsletters' security action", LastModified = "2011/01/24", Value = "Security action that allows access to the backend pages of the newsletters module")]
    public string ManageNewslettersDescription => this[nameof (ManageNewslettersDescription)];

    /// <summary>Translated text, similar to "Access ecommerce module"</summary>
    /// <value>Title of the 'ManageEcommerce' security action</value>
    [ResourceEntry("ManageEcommerce", Description = "Title of the 'ManageEcommerce' security action", LastModified = "2011/01/24", Value = "Access ecommerce module")]
    public string ManageEcommerce => this[nameof (ManageEcommerce)];

    /// <summary>
    /// Translated text, similar to "Security action that allows access to the backend pages of the ecommerce module"
    /// </summary>
    /// <value>Description of the 'ManageEcommerce' security action</value>
    [ResourceEntry("ManageEcommerceDescription", Description = "Description of the 'ManageEcommerce' security action", LastModified = "2011/01/24", Value = "Security action that allows access to the backend pages of the ecommerce module")]
    public string ManageEcommerceDescription => this[nameof (ManageEcommerceDescription)];

    /// <summary>Translated text, similar to "Access Site Sync module"</summary>
    /// <value>Title of the 'SiteSynchronization' security action</value>
    [ResourceEntry("SiteSynchronization", Description = "Title of the 'SiteSynchronization' security action", LastModified = "2016/09/13", Value = "Access Site Sync module")]
    public string SiteSynchronization => this[nameof (SiteSynchronization)];

    /// <summary>
    /// Translated text, similar to "Security action that allows access to the backend pages of the Site Sync module"
    /// </summary>
    /// <value>Description of the 'SiteSynchronization' security action</value>
    [ResourceEntry("SiteSynchronizationDescription", Description = "Description of the 'SiteSynchronization' security action", LastModified = "2016/09/13", Value = "Security action that allows access to the backend pages of the Site Sync module")]
    public string SiteSynchronizationDescription => this[nameof (SiteSynchronizationDescription)];

    /// <summary>Translated text, similar to "Access User files"</summary>
    /// <value>Title of the 'ManageUserFiles' security action</value>
    [ResourceEntry("ManageUserFiles", Description = "Title of the 'ManageUserFiles' security action", LastModified = "2012/02/06", Value = "Access User files")]
    public string ManageUserFiles => this[nameof (ManageUserFiles)];

    /// <summary>
    /// Translated text, similar to "Security action that allows access to the backend pages of the User files"
    /// </summary>
    /// <value>Description of the 'ManageUserFiles' security action</value>
    [ResourceEntry("ManageUserFilesDescription", Description = "Description of the 'ManageUserFiles' security action", LastModified = "2012/02/06", Value = "Security action that allows access to the backend pages of the User files library")]
    public string ManageUserFilesDescription => this[nameof (ManageUserFilesDescription)];

    /// <summary>
    /// Translated text, similar to "Access multisite management"
    /// </summary>
    /// <value>Title of the 'ManageMultisiteManagement' security action</value>
    [ResourceEntry("ManageMultisiteManagement", Description = "Title of the 'ManageMultisiteManagement' security action", LastModified = "2012/08/06", Value = "Access multisite management")]
    public string ManageMultisiteManagement => this[nameof (ManageMultisiteManagement)];

    /// <summary>
    /// Translated text, similar to "Security action that allows access to the backend pages of the Multisite management"
    /// </summary>
    /// <value>Description of the 'ManageMultisiteManagement' security action</value>
    [ResourceEntry("ManageMultisiteManagementDescription", Description = "Description of the 'ManageMultisiteManagement' security action", LastModified = "2012/08/06", Value = "Security action that allows access to the backend pages of the Multisite management")]
    public string ManageMultisiteManagementDescription => this[nameof (ManageMultisiteManagementDescription)];

    /// <summary>Gets Access Connector for Sitefinity Insight</summary>
    /// <value>Access Connector for Sitefinity Insight</value>
    [ResourceEntry("AccessDataIntelligenceConnectorTitle", Description = "Phrase: Access Connector for Sitefinity Insight", LastModified = "2020/03/10", Value = "Access Connector for Sitefinity Insight")]
    public string AccessDataIntelligenceConnectorTitle => this[nameof (AccessDataIntelligenceConnectorTitle)];

    /// <summary>
    /// Gets Security action that allows access to the backend pages of the Connector for Sitefinity Insight.
    /// </summary>
    /// <value>Security action that allows access to the backend pages of the Connector for Sitefinity Insight.</value>
    [ResourceEntry("AccessDataIntelligenceConnectorDescription", Description = "Phrase: Security action that allows access to the backend pages of the Connector for Sitefinity Insight.", LastModified = "2020/03/10", Value = "Security action that allows access to the backend pages of the Connector for Sitefinity Insight.")]
    public string AccessDataIntelligenceConnectorDescription => this[nameof (AccessDataIntelligenceConnectorDescription)];

    /// <summary>
    /// Gets the title for the global access permission action for the Translations Module.
    /// </summary>
    /// <value>Access Translations Module</value>
    [ResourceEntry("AccessTranslationsTitle", Description = "Phrase: Access Translations module", LastModified = "2015/05/21", Value = "Access Translations module")]
    public string AccessTranslationsTitle => this[nameof (AccessTranslationsTitle)];

    /// <summary>
    /// Gets Security action that allows access to the backend pages of the Connector for Translations module.
    /// </summary>
    /// <value>Security action that allows access to the backend pages of the Connector for Translations module.</value>
    [ResourceEntry("AccessTranslationsDescription", Description = "Phrase: Security action that allows access to the backend pages of the Translations module.", LastModified = "2015/05/21", Value = "Security action that allows access to the backend pages of the Translations module.")]
    public string AccessTranslationsDescription => this[nameof (AccessTranslationsDescription)];

    /// <summary>Label: Controls Permissions</summary>
    [ResourceEntry("ControlsPermissions", Description = "Permissions title.", LastModified = "2009/08/19", Value = "Controls Permissions")]
    public string ControlsPermissions => this[nameof (ControlsPermissions)];

    /// <summary>
    /// Message: Represents security permissions for controls.
    /// </summary>
    [ResourceEntry("ControlsPermissionsDescription", Description = "Permissions description.", LastModified = "2009/08/19", Value = "Represents security permissions for controls.")]
    public string ControlsPermissionsDescription => this[nameof (ControlsPermissionsDescription)];

    /// <summary>Label: Instantiate a widget</summary>
    [ResourceEntry("MoveControlActionName", Description = "The title of MoveControl security action.", LastModified = "2010/05/04", Value = "Move a widget")]
    public string MoveControlActionName => this[nameof (MoveControlActionName)];

    /// <summary>Label: Edit widget properties</summary>
    [ResourceEntry("EditControlPropertiesActionName", Description = "The title of EditControlProperties security action.", LastModified = "2010/04/28", Value = "Edit widget properties")]
    public string EditControlPropertiesActionName => this[nameof (EditControlPropertiesActionName)];

    /// <summary>Label: View a widget</summary>
    [ResourceEntry("ViewControlActionName", Description = "The title of ViewControl security action.", LastModified = "2009/04/13", Value = "View a widget")]
    public string ViewControlActionName => this[nameof (ViewControlActionName)];

    /// <summary>Label: Delete a widget.</summary>
    [ResourceEntry("DeleteControlActionName", Description = "The title of DeleteControl security action.", LastModified = "2010/04/28", Value = "Delete a widget")]
    public string DeleteControlActionName => this[nameof (DeleteControlActionName)];

    /// <summary>Label: Change Owner</summary>
    [ResourceEntry("ChangeControlOwnerActionName", Description = "The title of ChangeControlOwner security action.", LastModified = "2010/04/28", Value = "Change widget owner")]
    public string ChangeControlOwnerActionName => this[nameof (ChangeControlOwnerActionName)];

    /// <summary>
    /// Allows or denies the user to move widgets up or down in a zone (Page Editor).
    /// </summary>
    [ResourceEntry("MoveControlActionDescription", Description = "Security action description.", LastModified = "2010/05/04", Value = "Allows or denies the user to move widgets up or down in a zone (Page Editor).")]
    public string MoveControlActionDescription => this[nameof (MoveControlActionDescription)];

    /// <summary>Allows or denies changes to a widget's properties.</summary>
    [ResourceEntry("EditControlPropertiesActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changes to a widget's properties.")]
    public string EditControlPropertiesActionDescription => this[nameof (EditControlPropertiesActionDescription)];

    /// <summary>
    /// Allows or denies viewing (rendering) a widget on a page
    /// </summary>
    [ResourceEntry("ViewControlActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies viewing (rendering) a widget on a page")]
    public string ViewControlActionDescription => this[nameof (ViewControlActionDescription)];

    /// <summary>Allows or denies deleting a widget from a page.</summary>
    [ResourceEntry("DeleteControlActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies deleting a widget from a page.")]
    public string DeleteControlActionDescription => this[nameof (DeleteControlActionDescription)];

    /// <summary>Allows or denies changing the ownership of a widget.</summary>
    [ResourceEntry("ChangeControlOwnerActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changing the ownership of a widget.")]
    public string ChangeControlOwnerActionDescription => this[nameof (ChangeControlOwnerActionDescription)];

    /// <summary>Change Permissions</summary>
    [ResourceEntry("ChangeControlPermissionsActionName", Description = "The title of ChangeControlPermissions security action", LastModified = "2010/04/28", Value = "Change widget permissions")]
    public string ChangeControlPermissionsActionName => this[nameof (ChangeControlPermissionsActionName)];

    /// <summary>
    /// Allows or denies changing the permissions of a widget.
    /// </summary>
    [ResourceEntry("ChangeControlPermissionsActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changing the permissions of a widget.")]
    public string ChangeControlPermissionsActionDescription => this[nameof (ChangeControlPermissionsActionDescription)];

    /// <summary>Label: Controls Permissions</summary>
    [ResourceEntry("LayoutElementPermissionSetName", Description = "Permissions title.", LastModified = "2009/08/19", Value = "Layout Element Permissions")]
    public string LayoutElementPermissionSetName => this[nameof (LayoutElementPermissionSetName)];

    /// <summary>
    /// Message: Represents security permissions for controls.
    /// </summary>
    [ResourceEntry("LayoutElementPermissionSetDescription", Description = "Permissions description.", LastModified = "2009/08/19", Value = "Represents security permissions for layout elements.")]
    public string LayoutElementPermissionSetDescription => this[nameof (LayoutElementPermissionSetDescription)];

    /// <summary>Label: Move layout action title</summary>
    [ResourceEntry("MoveLayoutActionName", Description = "The title of move layout element security action.", LastModified = "2010/05/04", Value = "Move a layout element")]
    public string MoveLayoutActionName => this[nameof (MoveLayoutActionName)];

    /// <summary>
    /// Allows or denies the user to move layout elements up or down in a zone (Page Editor).
    /// </summary>
    [ResourceEntry("MoveLayoutActionDescription", Description = "Security action description.", LastModified = "2010/05/04", Value = "Allows or denies the user to move layout elements up or down in a zone (Page Editor).")]
    public string MoveLayoutActionDescription => this[nameof (MoveLayoutActionDescription)];

    /// <summary>Label: Edit layout element properties</summary>
    [ResourceEntry("EditLayoutPropertiesActionName", Description = "The title of EditControlProperties security action.", LastModified = "2010/04/28", Value = "Edit layout element properties")]
    public string EditLayoutPropertiesActionName => this[nameof (EditLayoutPropertiesActionName)];

    /// <summary>Label: View a layout element</summary>
    [ResourceEntry("ViewLayoutActionName", Description = "The title of view security action.", LastModified = "2009/04/13", Value = "View a layout element")]
    public string ViewLayoutActionName => this[nameof (ViewLayoutActionName)];

    /// <summary>
    /// Allows or denies viewing (rendering) a layout element on a page
    /// </summary>
    [ResourceEntry("ViewLayoutActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies viewing (rendering) a layout element on a page")]
    public string ViewLayoutActionDescription => this[nameof (ViewLayoutActionDescription)];

    /// <summary>
    /// Allows or denies changes to a layout element's properties.
    /// </summary>
    [ResourceEntry("EditLayoutPropertiesActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changes to a layout element's properties.")]
    public string EditLayoutPropertiesActionDescription => this[nameof (EditLayoutPropertiesActionDescription)];

    /// <summary>Label: Delete a layout element.</summary>
    [ResourceEntry("DeleteLayoutActionName", Description = "The title of delete security action.", LastModified = "2010/04/28", Value = "Delete a layout element.")]
    public string DeleteLayoutActionName => this[nameof (DeleteLayoutActionName)];

    /// <summary>
    /// Allows or denies deleting a layout element from a page.
    /// </summary>
    [ResourceEntry("DeleteLayoutActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies deleting a layout element from a page.")]
    public string DeleteLayoutActionDescription => this[nameof (DeleteLayoutActionDescription)];

    /// <summary>Label: Change layout element owner</summary>
    [ResourceEntry("ChangeLayoutOwnerActionName", Description = "The title of change owner security action.", LastModified = "2010/04/28", Value = "Change layout element owner")]
    public string ChangeLayoutOwnerActionName => this[nameof (ChangeLayoutOwnerActionName)];

    /// <summary>
    /// Allows or denies changing the ownership of a layout element.
    /// </summary>
    [ResourceEntry("ChangeLayoutOwnerActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changing the ownership of a layout element.")]
    public string ChangeLayoutOwnerActionDescription => this[nameof (ChangeLayoutOwnerActionDescription)];

    /// <summary>Change layout element permissions.</summary>
    [ResourceEntry("ChangeLayoutPermissionsActionName", Description = "The title of ChangeControlPermissions security action", LastModified = "2010/04/28", Value = "Change layout element permissions.")]
    public string ChangeLayoutPermissionsActionName => this[nameof (ChangeLayoutPermissionsActionName)];

    /// <summary>
    /// Allows or denies changing the permissions of a layout element.
    /// </summary>
    [ResourceEntry("ChangeLayoutPermissionsActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies changing the permissions of a widget.")]
    public string ChangeLayoutPermissionsActionDescription => this[nameof (ChangeLayoutPermissionsActionDescription)];

    /// <summary>Drop widgets or layout elements.</summary>
    [ResourceEntry("DropOnLayoutActionName", Description = "The title of drop on layout security action", LastModified = "2010/04/28", Value = "Drop widgets or layout elements.")]
    public string DropOnLayoutActionName => this[nameof (DropOnLayoutActionName)];

    /// <summary>Allows or denies dropping on a layout element.</summary>
    [ResourceEntry("DropOnLayoutActionDescription", Description = "Security action description.", LastModified = "2010/04/28", Value = "Allows or denies dropping on a layout element.")]
    public string DropOnLayoutActionDescription => this[nameof (DropOnLayoutActionDescription)];

    /// <summary>General Permissions</summary>
    [ResourceEntry("GeneralPermissions", Description = "Permission title.", LastModified = "2009/04/13", Value = "General Permissions")]
    public string GeneralPermissions => this[nameof (GeneralPermissions)];

    /// <summary>
    /// Represents the most common application security permissions.
    /// </summary>
    [ResourceEntry("GeneralPermissionsDescription", Description = "Permission description.", LastModified = "2009/04/13", Value = "Represents the most common application security permissions.")]
    public string GeneralPermissionsDescription => this[nameof (GeneralPermissionsDescription)];

    /// <summary>Label: Create</summary>
    [ResourceEntry("Create", Description = "The title of Create security action.", LastModified = "2009/04/13", Value = "Create {0}")]
    public string Create => this[nameof (Create)];

    /// <summary>Label: Create</summary>
    [ResourceEntry("Modify", Description = "The title of Modify security action.", LastModified = "2009/04/13", Value = "Modify {0}")]
    public string Modify => this[nameof (Modify)];

    /// <summary>Label: View</summary>
    [ResourceEntry("View", Description = "The title of View security action.", LastModified = "2009/04/13", Value = "View {0}")]
    public string View => this[nameof (View)];

    /// <summary>Label: Delete</summary>
    [ResourceEntry("Delete", Description = "The title of Delete security action.", LastModified = "2009/04/01", Value = "Delete {0}")]
    public string Delete => this[nameof (Delete)];

    /// <summary>Label: Change Owner</summary>
    [ResourceEntry("ChangeOwner", Description = "The title of Change Owner security action.", LastModified = "2009/04/13", Value = "Change {0} owner")]
    public string ChangeOwner => this[nameof (ChangeOwner)];

    /// <summary>Unlock</summary>
    [ResourceEntry("Unlock", Description = "The title of Unlock security action.", LastModified = "2018/11/02", Value = "Unlock {0}")]
    public string Unlock => this[nameof (Unlock)];

    /// <summary>Allows or denies unlocking a data item.</summary>
    [ResourceEntry("UnlockDescription", Description = "Unlock action description.", LastModified = "2018/11/02", Value = "Allows or denies unlocking of a data item.")]
    public string UnlockDescription => this[nameof (UnlockDescription)];

    /// <summary>Label: UnlockBlogPost</summary>
    [ResourceEntry("UnlockBlogPost", Description = "The title of unlock blog post security action.", LastModified = "2018/11/02", Value = "Unlock blog post")]
    public string UnlockBlogPost => this[nameof (UnlockBlogPost)];

    /// <summary>Label: UnlockBlogPost</summary>
    [ResourceEntry("UnlockBlogPostDescription", Description = "The desc of unlock blog post security action.", LastModified = "2018/11/02", Value = "Allows or denies unlock the blog post.")]
    public string UnlockBlogPostDescription => this[nameof (UnlockBlogPostDescription)];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("CreateDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies the creation of new data items.")]
    public string CreateDescription => this[nameof (CreateDescription)];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("ModifyDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies changes to an existing data item.")]
    public string ModifyDescription => this[nameof (ModifyDescription)];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("EditDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies changes to an existing data item.")]
    public string EditDescription => this[nameof (EditDescription)];

    /// <summary>Allows or denies viewing a particular data item.</summary>
    [ResourceEntry("ViewDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies viewing a particular data item.")]
    public string ViewDescription => this[nameof (ViewDescription)];

    /// <summary>Allows or denies deleting a particular data item.</summary>
    [ResourceEntry("DeleteDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies deleting a particular data item.")]
    public string DeleteDescription => this[nameof (DeleteDescription)];

    /// <summary>
    /// Allows or denies changing the ownership of a data item.
    /// </summary>
    [ResourceEntry("ChangeOwnerDescription", Description = "Security action description.", LastModified = "2009/04/13", Value = "Allows or denies changing the ownership of a data item.")]
    public string ChangeOwnerDescription => this[nameof (ChangeOwnerDescription)];

    /// <summary>Label: Page Templates Permissions</summary>
    [ResourceEntry("PageTemplatesPermissions", Description = "Permissions title.", LastModified = "2009/08/19", Value = "Page Templates Permissions")]
    public string PageTemplatesPermissions => this[nameof (PageTemplatesPermissions)];

    /// <summary>
    /// Message: Represents security permissions for page templates.
    /// </summary>
    [ResourceEntry("PageTemplatesPermissionsDescription", Description = "Permissions description.", LastModified = "2009/08/19", Value = "Represents security permissions for page templates.")]
    public string PageTemplatesPermissionsDescription => this[nameof (PageTemplatesPermissionsDescription)];

    /// <summary>Blog Permissions</summary>
    [ResourceEntry("BlogPermissions", Description = "Permission title.", LastModified = "2010/07/20", Value = "Blog Permissions")]
    public string BlogPermissions => this[nameof (BlogPermissions)];

    /// <summary>
    /// Represents the most common application security permissions.
    /// </summary>
    [ResourceEntry("BlogPermissionsDescription", Description = "Permission description.", LastModified = "2010/07/20", Value = "Represents the most common application security permissions.")]
    public string BlogPermissionsDescription => this[nameof (BlogPermissionsDescription)];

    /// <summary>Label: Create</summary>
    [ResourceEntry("CreateBlog", Description = "The title of CreateBlog security action.", LastModified = "2010/07/20", Value = "Create a blog")]
    public string CreateBlog => this[nameof (CreateBlog)];

    /// <summary>Label: View</summary>
    [ResourceEntry("ViewBlog", Description = "The title of ViewBlog security action.", LastModified = "2010/07/20", Value = "View a blog")]
    public string ViewBlog => this[nameof (ViewBlog)];

    /// <summary>Label: Delete</summary>
    [ResourceEntry("DeleteBlog", Description = "The title of DeleteBlog security action.", LastModified = "2010/07/20", Value = "Delete blog and posts")]
    public string DeleteBlog => this[nameof (DeleteBlog)];

    /// <summary>Label: Change Blog Owner</summary>
    [ResourceEntry("ChangeBlogOwner", Description = "The title of ChangeBlogOwner security action.", LastModified = "2010/07/20", Value = "Change a blog's owner")]
    public string ChangeBlogOwner => this["ChangeOwner"];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("CreateBlogDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies the creation of a new blog.")]
    public string CreateBlogDescription => this[nameof (CreateBlogDescription)];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("ModifyBlogDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies changes to an existing blog.")]
    public string ModifyBlogDescription => this[nameof (ModifyBlogDescription)];

    /// <summary>Allows or denies viewing a particular data item.</summary>
    [ResourceEntry("ViewBlogDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies viewing a particular blog.")]
    public string ViewBlogDescription => this[nameof (ViewBlogDescription)];

    /// <summary>Allows or denies deleting a particular data item.</summary>
    [ResourceEntry("DeleteBlogDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies deleting a particular blog.")]
    public string DeleteBlogDescription => this["DeleteDescription"];

    /// <summary>
    /// Allows or denies changing the ownership of a data item.
    /// </summary>
    [ResourceEntry("ChangeBlogOwnerDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies changing the ownership of a blog.")]
    public string ChangeBlogOwnerDescription => this[nameof (ChangeBlogOwnerDescription)];

    /// <summary>Change Permissions</summary>
    [ResourceEntry("ChangeBlogPermissions", Description = "The title of Change Permissions security action.", LastModified = "2010/07/20", Value = "Change a blog's permissions")]
    public string ChangeBlogPermissions => this[nameof (ChangeBlogPermissions)];

    /// <summary>
    /// Allows or denies changing the permissions of a data item.
    /// </summary>
    [ResourceEntry("ChangeBlogPermissionsDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies changing the permissions of a blog.")]
    public string ChangeBlogPermissionsDescription => this[nameof (ChangeBlogPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Blog Post Permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("BlogPostPermissions", Description = "Blog post permissions title.", LastModified = "2010/04/06", Value = "Blog Post Permissions")]
    public string BlogPostPermissions => this[nameof (BlogPostPermissions)];

    /// <summary>ViewBlogPost action</summary>
    [ResourceEntry("ViewBlogPost", Description = "Action text of the view action for a blog post.", LastModified = "2010/04/01", Value = "View blog post")]
    public string ViewBlogPost => this[nameof (ViewBlogPost)];

    /// <summary>ManageBlogPost action</summary>
    [ResourceEntry("ManageBlogPost", Description = "Action text of the manage action for a blog post.", LastModified = "2010/04/01", Value = "Modify blog and manage posts")]
    public string ManageBlogPost => this[nameof (ManageBlogPost)];

    /// <summary>
    /// Message: Allows or denies managing a blog or its posts.
    /// </summary>
    [ResourceEntry("ManageBlogPostDescription", Description = "Blog post editing description.", LastModified = "2010/04/01", Value = "Allows or denies updating a blog and managing its posts.")]
    public string ManageBlogPostDescription => this[nameof (ManageBlogPostDescription)];

    /// <summary>ModifyBlogPost action</summary>
    [ResourceEntry("ChangeBlogPostOwner", Description = "Action text of the change owner action for a blog post.", LastModified = "2010/04/01", Value = "Change blog post's owner")]
    public string ChangeBlogPostOwner => this[nameof (ChangeBlogPostOwner)];

    /// <summary>ModifyBlogPost action</summary>
    [ResourceEntry("ChangeBlogPostPermissions", Description = "Action text of the change permissions action for a blog post.", LastModified = "2010/04/01", Value = "Change blog post's permissions")]
    public string ChangeBlogPostPermissions => this[nameof (ChangeBlogPostPermissions)];

    /// <summary>
    /// Message: Represents security permissions for blog posts.
    /// </summary>
    [ResourceEntry("BlogPostPermissionsDescription", Description = "Blog post permissions description.", LastModified = "2010/04/01", Value = "Represents security permissions for blog posts.")]
    public string BlogPostPermissionsDescription => this[nameof (BlogPostPermissionsDescription)];

    /// <summary>
    /// Message: Allows or denies the creation of a new blog post.
    /// </summary>
    [ResourceEntry("ViewBlogPostDescription", Description = "Blog post viewing description.", LastModified = "2010/04/01", Value = "Allows or denies viewing a specific blog post.")]
    public string ViewBlogPostDescription => this[nameof (ViewBlogPostDescription)];

    /// <summary>
    /// Message: Allows or denies change of a specific blog post's owner.
    /// </summary>
    [ResourceEntry("ChangeBlogPostOwnerDescription", Description = "Blog post change owner description.", LastModified = "2010/04/01", Value = "Allows or denies changing specific blog post's owner.")]
    public string ChangeBlogPostOwnerDescription => this[nameof (ChangeBlogPostOwnerDescription)];

    /// <summary>
    /// Message: Allows or denies changing a specific blog post's permissions.
    /// </summary>
    [ResourceEntry("ChangeBlogPostPermissionsDescription", Description = "Blog post change permissions description.", LastModified = "2010/04/01", Value = "Allows or denies changing a specific blog post's permissions.")]
    public string ChangeBlogPostPermissionsDescription => this[nameof (ChangeBlogPostPermissionsDescription)];

    /// <summary>Translated message, similar to "Image Permissions"</summary>
    /// <value>Image permissions title.</value>
    [ResourceEntry("ImagePermissions", Description = "Image permissions title.", LastModified = "2010/04/06", Value = "Image Permissions")]
    public string ImagePermissions => this[nameof (ImagePermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for images."
    /// </summary>
    /// <value>Image permissions description</value>
    [ResourceEntry("ImagePermissionsDescription", Description = "Image permissions description", LastModified = "2010/04/06", Value = "Represents security permissions for images.")]
    public string ImagePermissionsDescription => this[nameof (ImagePermissionsDescription)];

    /// <summary>Translated message, similar to "View image"</summary>
    /// <value>Action title for viewing an image</value>
    [ResourceEntry("ViewImage", Description = "Action title for viewing an image", LastModified = "2010/04/06", Value = "View images")]
    public string ViewImage => this[nameof (ViewImage)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot view images"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewImageDescription", Description = "Security action description.", LastModified = "2010/04/06", Value = "Specify who can/cannot view images")]
    public string ViewImageDescription => this[nameof (ViewImageDescription)];

    /// <summary>Translated message, similar to "Upload image"</summary>
    /// <value>'Upload image' security action title</value>
    [ResourceEntry("CreateImage", Description = "'Upload Image' security action title", LastModified = "2010/04/06", Value = "Upload image")]
    public string CreateImage => this[nameof (CreateImage)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create images."
    /// </summary>
    /// <value>'Upload image' security action description.</value>
    [ResourceEntry("CreateImageDescription", Description = "'Upload image' security action description.", LastModified = "2010/04/06", Value = "Specify who can/cannot create images.")]
    public string CreateImageDescription => this[nameof (CreateImageDescription)];

    /// <summary>Translated message, similar to "Modify image"</summary>
    /// <value>'Modify image' security action title</value>
    [ResourceEntry("ModifyImage", Description = "'Modify image' security action title", LastModified = "2010/04/06", Value = "Modify image")]
    public string ModifyImage => this[nameof (ModifyImage)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify an image."
    /// </summary>
    /// <value>'Modify image' security action description</value>
    [ResourceEntry("ModifyImageDescription", Description = "'Modify image' security action description", LastModified = "2010/04/06", Value = "Specify who can/cannot modify an image.")]
    public string ModifyImageDescription => this[nameof (ModifyImageDescription)];

    /// <summary>Translated message, similar to "Delete image"</summary>
    /// <value>'Delete image' security action title</value>
    [ResourceEntry("DeleteImage", Description = "'Delete image' security action title", LastModified = "2010/04/06", Value = "Delete image")]
    public string DeleteImage => this[nameof (DeleteImage)];

    /// <summary>Translated message, similar to ""</summary>
    /// <value>'Delete image' security action description</value>
    [ResourceEntry("DeleteImageDescription", Description = "'Delete image' security action description", LastModified = "2010/04/06", Value = "'Delete image' security action description")]
    public string DeleteImageDescription => this[nameof (DeleteImageDescription)];

    /// <summary>Translated message, similar to "Change image owner"</summary>
    /// <value></value>
    [ResourceEntry("ChangeImageOwner", Description = "", LastModified = "2010/04/06", Value = "Change image owner")]
    public string ChangeImageOwner => this[nameof (ChangeImageOwner)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the ownership of an image"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeImageOwnerDescription", Description = "Translated message, similar to 'Allows or denies changing the ownership of an image'", LastModified = "2010/04/06", Value = "Allows or denies changing the ownership of an image.")]
    public string ChangeImageOwnerDescription => this[nameof (ChangeImageOwnerDescription)];

    /// <summary>Translated message, similar to ""</summary>
    /// <value></value>
    [ResourceEntry("ChangeImagePermissions", Description = "", LastModified = "2010/04/06", Value = "Change image permissions")]
    public string ChangeImagePermissions => this[nameof (ChangeImagePermissions)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the permissions of an image"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeImagePermissionsDescription", Description = "Translated message, similar to 'Allows or denies changing the permissions of an image'", LastModified = "2010/04/06", Value = "Allows or denies changing the permissions of an image.")]
    public string ChangeImagePermissionsDescription => this[nameof (ChangeImagePermissionsDescription)];

    /// <summary>Translated message, similar to "Unlock image"</summary>
    /// <value></value>
    [ResourceEntry("UnlockImage", Description = "", LastModified = "2018/11/14", Value = "Unlock images")]
    public string UnlockImage => this[nameof (UnlockImage)];

    /// <summary>
    /// Translated message, similar to "Allows or denies unlocking an image"
    /// </summary>
    /// <value></value>
    [ResourceEntry("UnlockImageDescription", Description = "Translated message, similar to 'Allows or denies unlocking an image'", LastModified = "2010/04/06", Value = "Allows or denies unlocking an image.")]
    public string UnlockImageDescription => this[nameof (UnlockImageDescription)];

    /// <summary>ManageImage action</summary>
    [ResourceEntry("ManageImage", Description = "Action text of the manage action for an image.", LastModified = "2010/07/13", Value = "Modify library and manage images")]
    public string ManageImage => this[nameof (ManageImage)];

    /// <summary>
    /// Message: Allows or denies updating a library and managing its images.
    /// </summary>
    [ResourceEntry("ManageImageDescription", Description = "Image editing description.", LastModified = "2012/01/05", Value = "Allows or denies updating a library and managing its images.")]
    public string ManageImageDescription => this[nameof (ManageImageDescription)];

    /// <summary>
    /// Translated message, similar to "Image Library Permissions"
    /// </summary>
    /// <value>Image Library permissions title.</value>
    [ResourceEntry("AlbumPermissions", Description = "Image Library permissions title.", LastModified = "2010/07/13", Value = "Image Library Permissions")]
    public string AlbumPermissions => this[nameof (AlbumPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for image libraries."
    /// </summary>
    /// <value>Library permissions description</value>
    [ResourceEntry("AlbumPermissionsDescription", Description = "Represents security permissions for image libraries.", LastModified = "2010/07/13", Value = "Represents security permissions for image libraries.")]
    public string AlbumPermissionsDescription => this[nameof (AlbumPermissionsDescription)];

    /// <summary>Translated message, similar to "View image library"</summary>
    /// <value>Action title for viewing an image library</value>
    [ResourceEntry("ViewAlbum", Description = "Action title for viewing an image library", LastModified = "2010/07/13", Value = "View image library")]
    public string ViewAlbum => this[nameof (ViewAlbum)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot view image libraries"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewAlbumDescription", Description = "Security action description.", LastModified = "2010/07/13", Value = "Specify who can/cannot view image libraries")]
    public string ViewAlbumDescription => this[nameof (ViewAlbumDescription)];

    /// <summary>Translated message, similar to "Create image library"</summary>
    /// <value>'Create image library' security action title</value>
    [ResourceEntry("CreateAlbum", Description = "'Create image library' security action title", LastModified = "2010/07/13", Value = "Create image library")]
    public string CreateAlbum => this[nameof (CreateAlbum)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create image libraries."
    /// </summary>
    /// <value>'Create image library' security action description.</value>
    [ResourceEntry("CreateAlbumDescription", Description = "'Create image library' security action description.", LastModified = "2010/07/13", Value = "Specify who can/cannot create image libraries.")]
    public string CreateAlbumDescription => this[nameof (CreateAlbumDescription)];

    /// <summary>Translated message, similar to "Modify image library"</summary>
    /// <value>'Modify image library' security action title</value>
    [ResourceEntry("ModifyAlbum", Description = "'Modify image library' security action title", LastModified = "2010/07/13", Value = "Modify image library")]
    public string ModifyAlbum => this[nameof (ModifyAlbum)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify an image library."
    /// </summary>
    /// <value>'Modify image library' security action description</value>
    [ResourceEntry("ModifyAlbumDescription", Description = "'Modify image library' security action description", LastModified = "2010/07/13", Value = "Specify who can/cannot modify an image library.")]
    public string ModifyAlbumDescription => this[nameof (ModifyAlbumDescription)];

    /// <summary>Translated message, similar to "Delete image library"</summary>
    /// <value>'Delete image library' security action title</value>
    [ResourceEntry("DeleteAlbum", Description = "'Delete image library' security action title", LastModified = "2010/07/13", Value = "Delete image library")]
    public string DeleteAlbum => this[nameof (DeleteAlbum)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete an image library"
    /// </summary>
    /// <value>'Delete image library' security action description</value>
    [ResourceEntry("DeleteAlbumDescription", Description = "'Delete image library' security action description", LastModified = "2010/07/13", Value = "Specify who can/cannot delete an image library")]
    public string DeleteAlbumDescription => this[nameof (DeleteAlbumDescription)];

    /// <summary>
    /// Translated message, similar to "Change image library owner"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeAlbumOwner", Description = "'Change image library owner' security action title", LastModified = "2010/07/13", Value = "Change image library owner")]
    public string ChangeAlbumOwner => this[nameof (ChangeAlbumOwner)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the ownership of an image library"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeAlbumOwnerDescription", Description = "Translated message, similar to 'Allows or denies changing the ownership of an image library'", LastModified = "2010/07/13", Value = "Allows or denies changing the ownership of an image library.")]
    public string ChangeAlbumOwnerDescription => this[nameof (ChangeAlbumOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change image library permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeAlbumPermissions", Description = "'Change image library permissions' security action name", LastModified = "2010/07/13", Value = "Change image library permissions")]
    public string ChangeAlbumPermissions => this[nameof (ChangeAlbumPermissions)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the permissions of an image library"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeAlbumPermissionsDescription", Description = "Translated message, similar to 'Allows or denies changing the permissions of an image library'", LastModified = "2010/07/13", Value = "Allows or denies changing the permissions of an image library.")]
    public string ChangeAlbumPermissionsDescription => this[nameof (ChangeAlbumPermissionsDescription)];

    /// <summary>ManageThisAlbum action</summary>
    [ResourceEntry("ManageThisAlbum", Description = "Action text of the modify action for this image library.", LastModified = "2010/07/13", Value = "Update this library and manage its images")]
    public string ManageThisAlbum => this[nameof (ManageThisAlbum)];

    /// <summary>Translated message, similar to "Document Permissions"</summary>
    /// <value>Document permissions title.</value>
    [ResourceEntry("DocumentPermissions", Description = "Document permissions title.", LastModified = "2010/05/11", Value = "Document Permissions")]
    public string DocumentPermissions => this[nameof (DocumentPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for Documents."
    /// </summary>
    /// <value>Document permissions description</value>
    [ResourceEntry("DocumentPermissionsDescription", Description = "Document permissions description", LastModified = "2010/05/11", Value = "Represents security permissions for Documents.")]
    public string DocumentPermissionsDescription => this[nameof (DocumentPermissionsDescription)];

    /// <summary>Translated message, similar to "View document"</summary>
    /// <value>Action title for viewing a Document</value>
    [ResourceEntry("ViewDocument", Description = "Action title for viewing a Document", LastModified = "2010/05/11", Value = "View document")]
    public string ViewDocument => this[nameof (ViewDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot View documents"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewDocumentDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Specify who can/cannot View documents")]
    public string ViewDocumentDescription => this[nameof (ViewDocumentDescription)];

    /// <summary>Translated message, similar to "Create Document"</summary>
    /// <value>'Create Document' security action title</value>
    [ResourceEntry("CreateDocument", Description = "'Create Document' security action title", LastModified = "2010/05/11", Value = "Upload document")]
    public string CreateDocument => this[nameof (CreateDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create Documents."
    /// </summary>
    /// <value>'Create Document' security action description.</value>
    [ResourceEntry("CreateDocumentDescription", Description = "'Create Document' security action description.", LastModified = "2010/05/11", Value = "Specify who can/cannot create Documents.")]
    public string CreateDocumentDescription => this[nameof (CreateDocumentDescription)];

    /// <summary>Translated message, similar to "Modify document"</summary>
    /// <value>'Modify document' security action title</value>
    [ResourceEntry("ModifyDocument", Description = "'Modify document' security action title", LastModified = "2010/05/11", Value = "Modify document")]
    public string ModifyDocument => this[nameof (ModifyDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a Document."
    /// </summary>
    /// <value>'Modify document' security action description</value>
    [ResourceEntry("ModifyDocumentDescription", Description = "'Modify document' security action description", LastModified = "2010/05/11", Value = "Specify who can/cannot modify a Document.")]
    public string ModifyDocumentDescription => this[nameof (ModifyDocumentDescription)];

    /// <summary>Translated message, similar to "Delete document"</summary>
    /// <value>'Delete document' security action title</value>
    [ResourceEntry("DeleteDocument", Description = "'Delete document' security action title", LastModified = "2010/05/11", Value = "Delete document")]
    public string DeleteDocument => this[nameof (DeleteDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete a Document."
    /// </summary>
    /// <value>'Delete document' security action description</value>
    [ResourceEntry("DeleteDocumentDescription", Description = "'Delete document' security action description", LastModified = "2010/05/11", Value = "Specify who can/cannot modify a Document.")]
    public string DeleteDocumentDescription => this[nameof (DeleteDocumentDescription)];

    /// <summary>
    /// Translated message, similar to "Change document owner"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentOwner", Description = "", LastModified = "2010/05/11", Value = "Change document owner")]
    public string ChangeDocumentOwner => this[nameof (ChangeDocumentOwner)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change document owner."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentOwnerDescription", Description = "", LastModified = "2010/05/11", Value = "Specify who can/cannot Change document owner")]
    public string ChangeDocumentOwnerDescription => this[nameof (ChangeDocumentOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change document permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentPermissions", Description = "", LastModified = "2010/05/11", Value = "Change document permissions")]
    public string ChangeDocumentPermissions => this[nameof (ChangeDocumentPermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change document permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentPermissionsDescription", Description = "", LastModified = "2010/05/11", Value = "Specify who can/cannot Change document permissions")]
    public string ChangeDocumentPermissionsDescription => this[nameof (ChangeDocumentPermissionsDescription)];

    /// <summary>Translated message, similar to "Unlock document"</summary>
    /// <value></value>
    [ResourceEntry("UnlockDocument", Description = "", LastModified = "2018/11/12", Value = "Unlock document")]
    public string UnlockDocument => this[nameof (UnlockDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot unlock document."
    /// </summary>
    /// <value></value>
    [ResourceEntry("UnlockDocumentDescription", Description = "", LastModified = "2018/11/12", Value = "Specify who can/cannot unlock document")]
    public string UnlockDocumentDescription => this[nameof (UnlockDocumentDescription)];

    /// <summary>
    /// Translated message, similar to "Modify library and manage documents"
    /// </summary>
    /// <value>'Manage document' security action title</value>
    [ResourceEntry("ManageDocument", Description = "'Manage document' security action title", LastModified = "2010/07/28", Value = "Modify library and manage documents")]
    public string ManageDocument => this[nameof (ManageDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a library and manage documents."
    /// </summary>
    /// <value>'Modify document' security action description</value>
    [ResourceEntry("ManageDocumentDescription", Description = "'Manage document' security action description", LastModified = "2010/07/28", Value = "Specify who can/cannot modify a library and manage documents.")]
    public string ManageDocumentDescription => this[nameof (ManageDocumentDescription)];

    /// <summary>
    /// "document" - text to be displayed for permissions related to documents (i.e. the text in the General permission set of the libraries root).
    /// </summary>
    /// <value></value>
    [ResourceEntry("documentGeneralPermissionSetTitleResource", Description = "document - text to be displayed for permissions related to documents (i.e. the text in the General permission set of the libraries root).", LastModified = "2010/06/08", Value = "document")]
    public string documentGeneralPermissionSetTitleResource => this[nameof (documentGeneralPermissionSetTitleResource)];

    /// <summary>
    /// Translated message, similar to "Document library Permissions"
    /// </summary>
    /// <value>Document library permissions title.</value>
    [ResourceEntry("DocumentLibraryPermissions", Description = "Document library permissions title.", LastModified = "2010/07/24", Value = "Document library Permissions")]
    public string DocumentLibraryPermissions => this[nameof (DocumentLibraryPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for Document libraries."
    /// </summary>
    /// <value>Document library permissions description</value>
    [ResourceEntry("DocumentLibraryPermissionsDescription", Description = "Document library permissions description", LastModified = "2010/07/24", Value = "Represents security permissions for Document libraries.")]
    public string DocumentLibraryPermissionsDescription => this[nameof (DocumentLibraryPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "View document library"
    /// </summary>
    /// <value>Action title for viewing a Document library</value>
    [ResourceEntry("ViewDocumentLibrary", Description = "Action title for viewing a Document library", LastModified = "2010/07/24", Value = "View document library")]
    public string ViewDocumentLibrary => this[nameof (ViewDocumentLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot View document libraries"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewDocumentLibraryDescription", Description = "Security action description.", LastModified = "2010/07/24", Value = "Specify who can/cannot View document libraries")]
    public string ViewDocumentLibraryDescription => this[nameof (ViewDocumentLibraryDescription)];

    /// <summary>
    /// Translated message, similar to "Create Document library"
    /// </summary>
    /// <value>'Create Document library' security action title</value>
    [ResourceEntry("CreateDocumentLibrary", Description = "'Create Document library' security action title", LastModified = "2010/07/24", Value = "Create document library")]
    public string CreateDocumentLibrary => this[nameof (CreateDocumentLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create Document libraries."
    /// </summary>
    /// <value>'Create Document library' security action description.</value>
    [ResourceEntry("CreateDocumentLibraryDescription", Description = "'Create Document library' security action description.", LastModified = "2010/07/24", Value = "Specify who can/cannot create Document libraries.")]
    public string CreateDocumentLibraryDescription => this[nameof (CreateDocumentLibraryDescription)];

    /// <summary>
    /// Translated message, similar to "Modify document library"
    /// </summary>
    /// <value>'Modify document library' security action title</value>
    [ResourceEntry("ModifyDocumentLibrary", Description = "'Modify document library' security action title", LastModified = "2010/07/24", Value = "Modify document library")]
    public string ModifyDocumentLibrary => this[nameof (ModifyDocumentLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a Document library."
    /// </summary>
    /// <value>'Modify document library' security action description</value>
    [ResourceEntry("ModifyDocumentLibraryDescription", Description = "'Modify document library' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot modify a Document library.")]
    public string ModifyDocumentLibraryDescription => this[nameof (ModifyDocumentLibraryDescription)];

    /// <summary>
    /// Translated message, similar to "Delete document library"
    /// </summary>
    /// <value>'Delete document library' security action title</value>
    [ResourceEntry("DeleteDocumentLibrary", Description = "'Delete document library' security action title", LastModified = "2010/07/24", Value = "Delete document library")]
    public string DeleteDocumentLibrary => this[nameof (DeleteDocumentLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete a Document library."
    /// </summary>
    /// <value>'Delete document library' security action description</value>
    [ResourceEntry("DeleteDocumentLibraryDescription", Description = "'Delete document library' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot modify a Document library.")]
    public string DeleteDocumentLibraryDescription => this[nameof (DeleteDocumentLibraryDescription)];

    /// <summary>
    /// Translated message, similar to "Change document library owner"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentLibraryOwner", Description = "'Change document library owner' security action title", LastModified = "2010/07/24", Value = "Change document library owner")]
    public string ChangeDocumentLibraryOwner => this[nameof (ChangeDocumentLibraryOwner)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change document library owner."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentLibraryOwnerDescription", Description = "'Change document library owner' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot Change document library owner")]
    public string ChangeDocumentLibraryOwnerDescription => this[nameof (ChangeDocumentLibraryOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change document library permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentLibraryPermissions", Description = "'Change document library permissions' security action title", LastModified = "2010/07/24", Value = "Change document library permissions")]
    public string ChangeDocumentLibraryPermissions => this[nameof (ChangeDocumentLibraryPermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change document library permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeDocumentLibraryPermissionsDescription", Description = "'Change document library permissions' security action description", LastModified = "2010/05/11", Value = "Specify who can/cannot Change document library permissions")]
    public string ChangeDocumentLibraryPermissionsDescription => this[nameof (ChangeDocumentLibraryPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Manage this document library"
    /// </summary>
    /// <value>'Manage document library' security action title</value>
    [ResourceEntry("ManageThisDocumentLibrary", Description = "'Manage document library' security action title", LastModified = "2010/07/28", Value = "Manage this document library")]
    public string ManageThisDocumentLibrary => this[nameof (ManageThisDocumentLibrary)];

    /// <summary>
    /// Translated message, similar to "User file Permissions"
    /// </summary>
    /// <value>user file permissions title.</value>
    [ResourceEntry("UserFilePermissions", Description = "User file permissions title.", LastModified = "2012/02/06", Value = "User file Permissions")]
    public string UserFilePermissions => this[nameof (UserFilePermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for Document libraries."
    /// </summary>
    /// <value>User file permissions description</value>
    [ResourceEntry("UserFilePermissionsDescription", Description = "User file permissions description", LastModified = "2012/02/06", Value = "Represents security permissions for Document libraries.")]
    public string UserFilePermissionsDescription => this[nameof (UserFilePermissionsDescription)];

    /// <summary>Translated message, similar to "View user file"</summary>
    /// <value>Action title for viewing a User file</value>
    [ResourceEntry("ViewUserFile", Description = "Action title for viewing a User file", LastModified = "2012/02/06", Value = "View user file")]
    public string ViewUserFile => this[nameof (ViewUserFile)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot View document libraries"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewUserFileDescription", Description = "Security action description.", LastModified = "2012/02/06", Value = "Specify who can/cannot View document libraries")]
    public string ViewUserFileDescription => this[nameof (ViewUserFileDescription)];

    /// <summary>Translated message, similar to "Create User file"</summary>
    /// <value>'Create User file' security action title</value>
    [ResourceEntry("CreateUserFile", Description = "'Create User file' security action title", LastModified = "2012/02/06", Value = "Create user file")]
    public string CreateUserFile => this[nameof (CreateUserFile)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create Document libraries."
    /// </summary>
    /// <value>'Create User file' security action description.</value>
    [ResourceEntry("CreateUserFileDescription", Description = "'Create User file' security action description.", LastModified = "2012/02/06", Value = "Specify who can/cannot create Document libraries.")]
    public string CreateUserFileDescription => this[nameof (CreateUserFileDescription)];

    /// <summary>Translated message, similar to "Modify user file"</summary>
    /// <value>'Modify user file' security action title</value>
    [ResourceEntry("ModifyUserFile", Description = "'Modify user file' security action title", LastModified = "2012/02/06", Value = "Modify user file")]
    public string ModifyUserFile => this[nameof (ModifyUserFile)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a User file."
    /// </summary>
    /// <value>'Modify user file' security action description</value>
    [ResourceEntry("ModifyUserFileDescription", Description = "'Modify user file' security action description", LastModified = "2012/02/06", Value = "Specify who can/cannot modify a User file.")]
    public string ModifyUserFileDescription => this[nameof (ModifyUserFileDescription)];

    /// <summary>Translated message, similar to "Delete user file"</summary>
    /// <value>'Delete user file' security action title</value>
    [ResourceEntry("DeleteUserFile", Description = "'Delete user file' security action title", LastModified = "2012/02/06", Value = "Delete user file")]
    public string DeleteUserFile => this[nameof (DeleteUserFile)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete a User file."
    /// </summary>
    /// <value>'Delete user file' security action description</value>
    [ResourceEntry("DeleteUserFileDescription", Description = "'Delete user file' security action description", LastModified = "2012/02/06", Value = "Specify who can/cannot modify a User file.")]
    public string DeleteUserFileDescription => this[nameof (DeleteUserFileDescription)];

    /// <summary>
    /// Translated message, similar to "Change user file owner"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeUserFileOwner", Description = "'Change user file owner' security action title", LastModified = "2012/02/06", Value = "Change user file owner")]
    public string ChangeUserFileOwner => this[nameof (ChangeUserFileOwner)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change user file owner."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeUserFileOwnerDescription", Description = "'Change user file owner' security action description", LastModified = "2012/02/06", Value = "Specify who can/cannot Change user file owner")]
    public string ChangeUserFileOwnerDescription => this[nameof (ChangeUserFileOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change user file permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeUserFilePermissions", Description = "'Change user file permissions' security action title", LastModified = "2012/02/06", Value = "Change user file permissions")]
    public string ChangeUserFilePermissions => this[nameof (ChangeUserFilePermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change user file permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeUserFilePermissionsDescription", Description = "'Change user file permissions' security action description", LastModified = "2012/02/06", Value = "Specify who can/cannot Change user file permissions")]
    public string ChangeUserFilePermissionsDescription => this[nameof (ChangeUserFilePermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Manage this user file"
    /// </summary>
    /// <value>'Manage user file' security action title</value>
    [ResourceEntry("ManageThisUserFile", Description = "'Manage user file' security action title", LastModified = "2012/02/06", Value = "Manage this user file")]
    public string ManageThisUserFile => this[nameof (ManageThisUserFile)];

    /// <summary>
    /// Translated message, similar to "UserFileDocument Permissions"
    /// </summary>
    /// <value>UserFileDocument permissions title.</value>
    [ResourceEntry("UserFileDocumentPermissions", Description = "UserFileDocument permissions title.", LastModified = "2012/02/09", Value = "UserFileDocument Permissions")]
    public string UserFileDocumentPermissions => this[nameof (UserFileDocumentPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for UserFileDocuments."
    /// </summary>
    /// <value>UserFileDocument permissions description</value>
    [ResourceEntry("UserFileDocumentPermissionsDescription", Description = "UserFileDocument permissions description", LastModified = "2012/02/09", Value = "Represents security permissions for UserFileDocuments.")]
    public string UserFileDocumentPermissionsDescription => this[nameof (UserFileDocumentPermissionsDescription)];

    /// <summary>Translated message, similar to "View document"</summary>
    /// <value>Action title for viewing a UserFileDocument</value>
    [ResourceEntry("ViewUserFileDocument", Description = "Action title for viewing a UserFileDocument", LastModified = "2012/02/09", Value = "View this item")]
    public string ViewUserFileDocument => this[nameof (ViewUserFileDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot View documents"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewUserFileDocumentDescription", Description = "Security action description.", LastModified = "2012/02/09", Value = "Specify who can/cannot View documents")]
    public string ViewUserFileDocumentDescription => this[nameof (ViewUserFileDocumentDescription)];

    /// <summary>
    /// Translated message, similar to "Modify library and manage documents"
    /// </summary>
    /// <value>'Manage document' security action title</value>
    [ResourceEntry("ManageUserFileDocument", Description = "'Manage document' security action title", LastModified = "2010/07/28", Value = "Modify folder and manage its items")]
    public string ManageUserFileDocument => this[nameof (ManageUserFileDocument)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a library and manage documents."
    /// </summary>
    /// <value>'Modify document' security action description</value>
    [ResourceEntry("ManageUserFileDocumentDescription", Description = "'Manage document' security action description", LastModified = "2010/07/28", Value = "Specify who can/cannot modify a library and manage documents.")]
    public string ManageUserFileDocumentDescription => this[nameof (ManageUserFileDocumentDescription)];

    /// <summary>
    /// Translated message, similar to "Change document permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeuserFileDocumentPermissions", Description = "", LastModified = "2012/02/09", Value = "Change item's permissions")]
    public string ChangeuserFileDocumentPermissions => this[nameof (ChangeuserFileDocumentPermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot Change document permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeUserFileDocumentPermissionsDescription", Description = "", LastModified = "2012/02/09", Value = "Specify who can/cannot Change items permissions")]
    public string ChangeUserFileDocumentPermissionsDescription => this[nameof (ChangeUserFileDocumentPermissionsDescription)];

    /// <summary>Translated message, similar to "Video Permissions"</summary>
    /// <value>Video permissions title.</value>
    [ResourceEntry("VideoPermissions", Description = "Video permissions title.", LastModified = "2010/05/11", Value = "Video Permissions")]
    public string VideoPermissions => this[nameof (VideoPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for videos."
    /// </summary>
    /// <value>Video permissions description</value>
    [ResourceEntry("VideoPermissionsDescription", Description = "Video permissions description", LastModified = "2010/05/11", Value = "Represents security permissions for videos.")]
    public string VideoPermissionsDescription => this[nameof (VideoPermissionsDescription)];

    /// <summary>Translated message, similar to "View video"</summary>
    /// <value>Action title for viewing a video</value>
    [ResourceEntry("ViewVideo", Description = "Action title for viewing a video", LastModified = "2010/05/11", Value = "View video")]
    public string ViewVideo => this[nameof (ViewVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot view videos"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewVideoDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Specify who can/cannot view videos")]
    public string ViewVideoDescription => this[nameof (ViewVideoDescription)];

    /// <summary>Translated message, similar to "Create video"</summary>
    /// <value>'Create video' security action title</value>
    [ResourceEntry("CreateVideo", Description = "'Create video' security action title", LastModified = "2010/05/11", Value = "Upload video")]
    public string CreateVideo => this[nameof (CreateVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create videos."
    /// </summary>
    /// <value>'Create video' security action description.</value>
    [ResourceEntry("CreateVideoDescription", Description = "'Create video' security action description.", LastModified = "2010/05/11", Value = "Specify who can/cannot create videos.")]
    public string CreateVideoDescription => this[nameof (CreateVideoDescription)];

    /// <summary>Translated message, similar to "Modify video"</summary>
    /// <value>'Modify video' security action title</value>
    [ResourceEntry("ModifyVideo", Description = "'Modify video' security action title", LastModified = "2010/05/11", Value = "Modify video")]
    public string ModifyVideo => this[nameof (ModifyVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a video."
    /// </summary>
    /// <value>'Modify video' security action description</value>
    [ResourceEntry("ModifyVideoDescription", Description = "'Modify video' security action description", LastModified = "2010/05/11", Value = "Specify who can/cannot modify a video.")]
    public string ModifyVideoDescription => this[nameof (ModifyVideoDescription)];

    /// <summary>
    /// Translated message, similar to "Modify library and manage videos"
    /// </summary>
    /// <value>'Manage video' security action title</value>
    [ResourceEntry("ManageVideo", Description = "'Manage video' security action title", LastModified = "2010/07/28", Value = "Modify library and manage videos")]
    public string ManageVideo => this[nameof (ManageVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a library and manage videos."
    /// </summary>
    /// <value>'Modify video' security action description</value>
    [ResourceEntry("ManageVideoDescription", Description = "'Manage video' security action description", LastModified = "2010/07/28", Value = "Specify who can/cannot modify a library and manage videos.")]
    public string ManageVideoDescription => this[nameof (ManageVideoDescription)];

    /// <summary>Translated message, similar to "Delete video"</summary>
    /// <value>'Delete video' security action title</value>
    [ResourceEntry("DeleteVideo", Description = "'Delete video' security action title", LastModified = "2010/05/11", Value = "Delete video")]
    public string DeleteVideo => this[nameof (DeleteVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete a video."
    /// </summary>
    /// <value>'Delete video' security action description</value>
    [ResourceEntry("DeleteVideoDescription", Description = "'Delete video' security action description", LastModified = "2010/05/11", Value = "Specify who can/cannot modify a video.")]
    public string DeleteVideoDescription => this[nameof (DeleteVideoDescription)];

    /// <summary>Translated message, similar to "Change video owner"</summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoOwner", Description = "", LastModified = "2010/05/11", Value = "Change video owner")]
    public string ChangeVideoOwner => this[nameof (ChangeVideoOwner)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot change video owner."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoOwnerDescription", Description = "", LastModified = "2010/05/11", Value = "Specify who can/cannot change video owner")]
    public string ChangeVideoOwnerDescription => this[nameof (ChangeVideoOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change video permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoPermissions", Description = "", LastModified = "2010/05/11", Value = "Change video permissions")]
    public string ChangeVideoPermissions => this[nameof (ChangeVideoPermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot change video permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoPermissionsDescription", Description = "", LastModified = "2010/05/11", Value = "Specify who can/cannot change video permissions")]
    public string ChangeVideoPermissionsDescription => this[nameof (ChangeVideoPermissionsDescription)];

    /// <summary>Translated message, similar to "Unlock video"</summary>
    /// <value></value>
    [ResourceEntry("UnlockVideo", Description = "", LastModified = "2018/11/14", Value = "Unlock video")]
    public string UnlockVideo => this[nameof (UnlockVideo)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot unlock video."
    /// </summary>
    /// <value></value>
    [ResourceEntry("UnlockVideoDescription", Description = "", LastModified = "2018/11/14", Value = "Specify who can/cannot unlock video")]
    public string UnlockVideoDescription => this[nameof (UnlockVideoDescription)];

    /// <summary>
    /// Translated message, similar to "Video Library Permissions"
    /// </summary>
    /// <value>Video library permissions title.</value>
    [ResourceEntry("VideoLibraryPermissions", Description = "Video library permissions title.", LastModified = "2010/07/24", Value = "Video Library Permissions")]
    public string VideoLibraryPermissions => this[nameof (VideoLibraryPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for video libraries."
    /// </summary>
    /// <value>Video library permissions description</value>
    [ResourceEntry("VideoLibraryPermissionsDescription", Description = "Video library permissions description", LastModified = "2010/07/24", Value = "Represents security permissions for video libraries.")]
    public string VideoLibraryPermissionsDescription => this[nameof (VideoLibraryPermissionsDescription)];

    /// <summary>Translated message, similar to "View video library"</summary>
    /// <value>Action title for viewing a video library</value>
    [ResourceEntry("ViewVideoLibrary", Description = "Action title for viewing a video library", LastModified = "2010/07/24", Value = "View video library")]
    public string ViewVideoLibrary => this[nameof (ViewVideoLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot view video libraries"
    /// </summary>
    /// <value>Security action title description.</value>
    [ResourceEntry("ViewVideoLibraryDescription", Description = "Security action description.", LastModified = "2010/07/24", Value = "Specify who can/cannot view video libraries")]
    public string ViewVideoLibraryDescription => this[nameof (ViewVideoLibraryDescription)];

    /// <summary>Translated message, similar to "Create video library"</summary>
    /// <value>'Create video library' security action title</value>
    [ResourceEntry("CreateVideoLibrary", Description = "'Create video library' security action title", LastModified = "2010/07/24", Value = "Create video library")]
    public string CreateVideoLibrary => this[nameof (CreateVideoLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create video libraries."
    /// </summary>
    /// <value>'Create video library' security action description.</value>
    [ResourceEntry("CreateVideoLibraryDescription", Description = "'Create video library' security action description.", LastModified = "2010/07/24", Value = "Specify who can/cannot create video libraries.")]
    public string CreateVideoLibraryDescription => this[nameof (CreateVideoLibraryDescription)];

    /// <summary>Translated message, similar to "Modify video library"</summary>
    /// <value>'Modify video library' security action title</value>
    [ResourceEntry("ModifyVideoLibrary", Description = "'Modify video library' security action title", LastModified = "2010/07/24", Value = "Modify video library")]
    public string ModifyVideoLibrary => this[nameof (ModifyVideoLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot modify a video library."
    /// </summary>
    /// <value>'Modify video library' security action description</value>
    [ResourceEntry("ModifyVideoLibraryDescription", Description = "'Modify video library' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot modify a video library.")]
    public string ModifyVideoLibraryDescription => this[nameof (ModifyVideoLibraryDescription)];

    /// <summary>Translated message, similar to "Delete video library"</summary>
    /// <value>'Delete video library' security action title</value>
    [ResourceEntry("DeleteVideoLibrary", Description = "'Delete video library' security action title", LastModified = "2010/07/24", Value = "Delete video library")]
    public string DeleteVideoLibrary => this[nameof (DeleteVideoLibrary)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete a video library."
    /// </summary>
    /// <value>'Delete video library' security action description</value>
    [ResourceEntry("DeleteVideoLibraryDescription", Description = "'Delete video library' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot modify a video library.")]
    public string DeleteVideoLibraryDescription => this[nameof (DeleteVideoLibraryDescription)];

    /// <summary>
    /// Translated message, similar to "Change video library owner"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoLibraryOwner", Description = "'Change video library owner' security action title", LastModified = "2010/07/24", Value = "Change video library owner")]
    public string ChangeVideoLibraryOwner => this[nameof (ChangeVideoLibraryOwner)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot change video library owner."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoLibraryOwnerDescription", Description = "'Change video library owner' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot change video library owner")]
    public string ChangeVideoLibraryOwnerDescription => this[nameof (ChangeVideoLibraryOwnerDescription)];

    /// <summary>
    /// Translated message, similar to "Change video library permissions"
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoLibraryPermissions", Description = "'Change video library permissions' security action title", LastModified = "2010/07/24", Value = "Change video library permissions")]
    public string ChangeVideoLibraryPermissions => this[nameof (ChangeVideoLibraryPermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot change video library permissions."
    /// </summary>
    /// <value></value>
    [ResourceEntry("ChangeVideoLibraryPermissionsDescription", Description = "'Change video library permissions' security action description", LastModified = "2010/07/24", Value = "Specify who can/cannot change video permissions")]
    public string ChangeVideoLibraryPermissionsDescription => this[nameof (ChangeVideoLibraryPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Manage this video library"
    /// </summary>
    /// <value>'Manage video library' security action title</value>
    [ResourceEntry("ManageThisVideoLibrary", Description = "'Manage video library' security action title", LastModified = "2010/07/28", Value = "Manage this video library")]
    public string ManageThisVideoLibrary => this[nameof (ManageThisVideoLibrary)];

    /// <summary>Message: Permissions</summary>
    /// <value>Permissions</value>
    [ResourceEntry("GenericContentPermissionsTitle", Description = "Generic Content  Permissions Title", LastModified = "2009/11/23", Value = "Permissions")]
    public string GenericContentPermissionsTitle => this[nameof (GenericContentPermissionsTitle)];

    /// <summary>Label: Create comments</summary>
    [ResourceEntry("CreateComments", Description = "The title of Create comments security action.", LastModified = "2010/06/04", Value = "Create comments")]
    public string CreateComments => this[nameof (CreateComments)];

    /// <summary>Label: Modify comments</summary>
    [ResourceEntry("ModifyComments", Description = "The title of Modify comments security action.", LastModified = "2010/06/04", Value = "Modify comments")]
    public string ModifyComments => this[nameof (ModifyComments)];

    /// <summary>Label: Edit comments</summary>
    [ResourceEntry("EditComments", Description = "The title of Edit comments security action.", LastModified = "2010/06/04", Value = "Edit comments")]
    public string EditComments => this[nameof (EditComments)];

    /// <summary>Label: View comments</summary>
    [ResourceEntry("ViewComments", Description = "The title of View comments security action.", LastModified = "2010/06/04", Value = "View comments")]
    public string ViewComments => this[nameof (ViewComments)];

    /// <summary>Label: Delete comments</summary>
    [ResourceEntry("DeleteComments", Description = "The title of Delete comments security action.", LastModified = "2010/06/04", Value = "Delete comments")]
    public string DeleteComments => this[nameof (DeleteComments)];

    /// <summary>Label: Change Owner</summary>
    [ResourceEntry("ChangeCommentsOwner", Description = "The title of Change Comments Owner security action.", LastModified = "2010/06/04", Value = "Change comments owner")]
    public string ChangeCommentsOwner => this[nameof (ChangeCommentsOwner)];

    /// <summary>Allows or denies the creation of comments.</summary>
    [ResourceEntry("CreateCommentsDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies the creation of comments.")]
    public string CreateCommentsDescription => this[nameof (CreateCommentsDescription)];

    /// <summary>Allows or denies changes to comments</summary>
    [ResourceEntry("ModifyCommentsDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies changes to comments.")]
    public string ModifyCommentsDescription => this[nameof (ModifyCommentsDescription)];

    /// <summary>Allows or denies changes to comments.</summary>
    [ResourceEntry("EditCommentsDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies changes to comments.")]
    public string EditCommentsDescription => this[nameof (EditCommentsDescription)];

    /// <summary>Allows or denies viewing comments.</summary>
    [ResourceEntry("ViewCommentsDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies viewing comments")]
    public string ViewCommentsDescription => this[nameof (ViewCommentsDescription)];

    /// <summary>Allows or denies deleting comments.</summary>
    [ResourceEntry("DeleteCommentsDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies deleting comments.")]
    public string DeleteCommentsDescription => this[nameof (DeleteCommentsDescription)];

    /// <summary>Allows or denies changing the ownership of comments.</summary>
    [ResourceEntry("ChangeCommentsOwnerDescription", Description = "Security action description.", LastModified = "2010/06/04", Value = "Allows or denies changing the ownership of comments.")]
    public string ChangeCommentsOwnerDescription => this[nameof (ChangeCommentsOwnerDescription)];

    /// <summary>
    /// Message: Represents security permissions for comments.
    /// </summary>
    [ResourceEntry("CommentsPermissionsDescription", Description = "Permissions description.", LastModified = "2009/08/19", Value = "Represents security permissions for comments.")]
    public string CommentsPermissionsDescription => this[nameof (CommentsPermissionsDescription)];

    /// <summary>Translated message, similar to "View comments"</summary>
    /// <value>The title of ViewComments security action.</value>
    [ResourceEntry("ViewCommentActionTitle", Description = "The title of ViewComments security action.", LastModified = "2010/06/14", Value = "View comments")]
    public string ViewCommentActionTitle => this[nameof (ViewCommentActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies viewing comments"
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("ViewCommentsActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies viewing comments")]
    public string ViewCommentsActionDescription => this[nameof (ViewCommentsActionDescription)];

    /// <summary>Translated message, similar to "Write comments"</summary>
    /// <value>The title of ViewComments security action.</value>
    [ResourceEntry("CreateCommentActionTitle", Description = "The title of CreateComments security action.", LastModified = "2010/06/14", Value = "Write comments")]
    public string CreateCommentActionTitle => this[nameof (CreateCommentActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies writing new comments"
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("CreateCommentsActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies writing new comments")]
    public string CreateCommentsActionDescription => this[nameof (CreateCommentsActionDescription)];

    /// <summary>Translated message, similar to "Modify comments"</summary>
    /// <value>The title of ViewComments security action.</value>
    [ResourceEntry("ModifyCommentActionTitle", Description = "The title of ModifyComments security action.", LastModified = "2010/06/14", Value = "Modify comments")]
    public string ModifyCommentActionTitle => this[nameof (ModifyCommentActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies modifying existing comments."
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("ModifyCommentsActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies modifying existing comments.")]
    public string ModifyCommentsActionDescription => this[nameof (ModifyCommentsActionDescription)];

    /// <summary>Translated message, similar to "Delete comments"</summary>
    /// <value>The title of DeleteComments security action.</value>
    [ResourceEntry("DeleteCommentActionTitle", Description = "The title of DeleteComments security action.", LastModified = "2010/06/14", Value = "Delete comments")]
    public string DeleteCommentActionTitle => this[nameof (DeleteCommentActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies deleting comments."
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("DeleteCommentsActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies deleting comments.")]
    public string DeleteCommentsActionDescription => this[nameof (DeleteCommentsActionDescription)];

    /// <summary>
    /// Translated message, similar to "Change comment ownership"
    /// </summary>
    /// <value>The title of DeleteComments security action.</value>
    [ResourceEntry("ChangeCommentOwnerActionTitle", Description = "The title of ChangeCommentOwner security action.", LastModified = "2010/06/14", Value = "Change comment ownership")]
    public string ChangeCommentOwnerActionTitle => this[nameof (ChangeCommentOwnerActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the owner of a comment."
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("ChangeCommentOwnerActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies changing the owner of a comment.")]
    public string ChangeCommentOwnerActionDescription => this[nameof (ChangeCommentOwnerActionDescription)];

    /// <summary>
    /// Translated message, similar to "Change comment permissions"
    /// </summary>
    /// <value>The title of DeleteComments security action.</value>
    [ResourceEntry("ChangeCommentPermissionsActionTitle", Description = "The title of ChangeCommentPermissions security action.", LastModified = "2010/06/14", Value = "Change comment permissions")]
    public string ChangeCommentPermissionsActionTitle => this[nameof (ChangeCommentPermissionsActionTitle)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing security permissions for a comment."
    /// </summary>
    /// <value>Security action description</value>
    [ResourceEntry("ChangeCommentPermissionsActionDescription", Description = "Security action description.", LastModified = "2010/06/14", Value = "Allows or denies changing security permissions for a comment.")]
    public string ChangeCommentPermissionsActionDescription => this[nameof (ChangeCommentPermissionsActionDescription)];

    /// <summary>Change comments Permissions security action</summary>
    [ResourceEntry("ChangeCommentsPermissions", Description = "The title of Change comments Permissions security action.", LastModified = "2010/06/04", Value = "Change comments permissions")]
    public string ChangeCommentsPermissions => this[nameof (ChangeCommentsPermissions)];

    /// <summary>
    /// Change comments Permissions security action description
    /// </summary>
    [ResourceEntry("ChangeCommentsPermissionsDescription", Description = "The description of Change Permissions security action.", LastModified = "2010/06/04", Value = "Allows or denies changing the permissions of comments")]
    public string ChangeCommentsPermissionsDescription => this[nameof (ChangeCommentsPermissionsDescription)];

    /// <summary>Page permissions title</summary>
    /// <value></value>
    [ResourceEntry("PagePermissions", Description = "Page permissions title.", LastModified = "2010/04/29", Value = "Page Permissions")]
    public string PagePermissions => this[nameof (PagePermissions)];

    /// <summary>
    /// Message: Represents security permissions for page actions.
    /// </summary>
    [ResourceEntry("PagePermissionsDescription", Description = "Blog post permissions description.", LastModified = "2010/04/29", Value = "Represents security permissions for page actions.")]
    public string PagePermissionsDescription => this[nameof (PagePermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Create widgets and layout elements"
    /// </summary>
    /// <value>Create widgets and layout elements action title.</value>
    [ResourceEntry("CreateChildControlsActionName", Description = "Create widgets and layout elements action title.", LastModified = "2010/05/11", Value = "Add widgets and layout elements to the page and its child pages")]
    public string CreateChildControlsActionName => this[nameof (CreateChildControlsActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies creating widgets and layout elements on a page."
    /// </summary>
    /// <value>Create widgets and layout elements action description.</value>
    [ResourceEntry("CreateChildControlsActionDescription", Description = "Create widgets and layout elements action description.", LastModified = "2010/05/11", Value = "Allows or denies creating widgets and layout elements on a page.")]
    public string CreateChildControlsActionDescription => this[nameof (CreateChildControlsActionDescription)];

    /// <summary>Translated message, similar to "Edit page content"</summary>
    /// <value>Open a page in edit mode</value>
    [ResourceEntry("EditContentActionName", Description = "Edit content action title", LastModified = "2010/06/30", Value = "Edit content of this page and its child pages")]
    public string EditContentActionName => this[nameof (EditContentActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies opening a page in edit mode"
    /// </summary>
    /// <value>Open a page in edit mode</value>
    [ResourceEntry("EditContentActionDescription", Description = "Description of action EditContent", LastModified = "2010/06/30", Value = "Allows or denies opening a page in edit mode")]
    public string EditContentActionDescription => this[nameof (EditContentActionDescription)];

    /// <summary>Translated message, similar to "Create a page."</summary>
    /// <value>Create a page action description.</value>
    [ResourceEntry("CreatePageActionName", Description = "Create a page action description.", LastModified = "2010/05/11", Value = "Create child pages of this page")]
    public string CreatePageActionName => this[nameof (CreatePageActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies the creation of new pages."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("CreatePageActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies the creation of new pages.")]
    public string CreatePageActionDescription => this[nameof (CreatePageActionDescription)];

    /// <summary>Translated message, similar to "Modify a page"</summary>
    /// <value>The title of Modify security action.</value>
    [ResourceEntry("ModifyPageActionName", Description = "The title of Modify security action.", LastModified = "2010/05/11", Value = "Modify properties of this page and its child pages")]
    public string ModifyPageActionName => this[nameof (ModifyPageActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changes to an existing page, such as reordering or changing properties."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("ModifyPageActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies changes to an existing page, such as reordering or changing properties.")]
    public string ModifyPageActionDescription => this[nameof (ModifyPageActionDescription)];

    /// <summary>Translated message, similar to "View a page"</summary>
    /// <value>The title of View Page security action.</value>
    [ResourceEntry("ViewPageActionName", Description = "The title of View Page security action.", LastModified = "2010/05/11", Value = "View this page and its child pages")]
    public string ViewPageActionName => this[nameof (ViewPageActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies unlocking of a page"
    /// </summary>
    /// <value>The title of View Page security action.</value>
    [ResourceEntry("UnlockPagesActionDescription", Description = "The description of Unlock Page security action.", LastModified = "2018/11/16", Value = "Allows or denies unlocking of a page.")]
    public string UnlockPagesActionDescription => this[nameof (UnlockPagesActionDescription)];

    /// <summary>
    /// Translated message, similar to "Allows or denies viewing (browsing) a particular page."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("ViewPageActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies viewing (browsing) a particular page.")]
    public string ViewPageActionDescription => this[nameof (ViewPageActionDescription)];

    /// <summary>Translated message, similar to "Delete a page"</summary>
    /// <value>The title of Delete Page security action.</value>
    [ResourceEntry("DeletePageActionName", Description = "The title of Delete Page security action.", LastModified = "2010/05/11", Value = "Delete this page and its child pages")]
    public string DeletePageActionName => this[nameof (DeletePageActionName)];

    /// <summary>Translated message, similar to "Unlock pages"</summary>
    [ResourceEntry("UnlockPageActionName", Description = "The title of Unlock Page security action.", LastModified = "2018/11/16", Value = "Unlock this page and its child pages.")]
    public string UnlockPageActionName => this[nameof (UnlockPageActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies the deletion of a page."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("DeletePageActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies the deletion of a page.")]
    public string DeletePageActionDescription => this[nameof (DeletePageActionDescription)];

    /// <summary>Translated message, similar to "Change page owner"</summary>
    /// <value>The title of ChangePermissions security action.</value>
    [ResourceEntry("ChangePageOwnerActionName", Description = "The title of Change Page Owner security action.", LastModified = "2010/05/11", Value = "Change owner of this page and its child pages")]
    public string ChangePageOwnerActionName => this[nameof (ChangePageOwnerActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the ownership of a page."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("ChangePageOwnerActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies changing the ownership of a page.")]
    public string ChangePageOwnerActionDescription => this[nameof (ChangePageOwnerActionDescription)];

    /// <summary>
    /// Translated message, similar to "Change page permissions"
    /// </summary>
    /// <value>The title of ChangePermissions security action.</value>
    [ResourceEntry("ChangePagePermissionsActionName", Description = "The title of ChangePermissions security action.", LastModified = "2010/05/11", Value = "Change permissions of this page and its child pages")]
    public string ChangePagePermissionsActionName => this[nameof (ChangePagePermissionsActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing permissions for a page."
    /// </summary>
    /// <value>Security action description.</value>
    [ResourceEntry("ChangePagePermissionsActionDescription", Description = "Security action description.", LastModified = "2010/05/11", Value = "Allows or denies changing permissions for a page.")]
    public string ChangePagePermissionsActionDescription => this[nameof (ChangePagePermissionsActionDescription)];

    /// <summary>Taxonomy permissions title</summary>
    /// <value></value>
    [ResourceEntry("TaxonomyPermissions", Description = "Taxonomy permissions title.", LastModified = "2010/06/22", Value = "Classification Permissions")]
    public string TaxonomyPermissions => this[nameof (TaxonomyPermissions)];

    /// <summary>
    /// Message: Represents security permissions for classification actions.
    /// </summary>
    [ResourceEntry("TaxonomyPermissionsDescription", Description = "Taxonomy permissions description.", LastModified = "2010/06/22", Value = "Represents security permissions for classification actions.")]
    public string TaxonomyPermissionsDescription => this[nameof (TaxonomyPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "View a classification"
    /// </summary>
    /// <value>View taxonomy action title.</value>
    [ResourceEntry("ViewTaxonomyActionName", Description = "View taxonomy action title.", LastModified = "2010/06/22", Value = "View classification")]
    public string ViewTaxonomyActionName => this[nameof (ViewTaxonomyActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies viewing a classification."
    /// </summary>
    /// <value>View taxonomy action description.</value>
    [ResourceEntry("ViewTaxonomyActionDescription", Description = "View taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies viewing a classification.")]
    public string ViewTaxonomyActionDescription => this[nameof (ViewTaxonomyActionDescription)];

    /// <summary>
    /// Translated message, similar to "Create a classification"
    /// </summary>
    /// <value>Create taxonomy action title.</value>
    [ResourceEntry("CreateTaxonomyActionName", Description = "Create taxonomy action title.", LastModified = "2010/06/22", Value = "Create classification")]
    public string CreateTaxonomyActionName => this[nameof (CreateTaxonomyActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies creating a classification."
    /// </summary>
    /// <value>Create taxonomy action description.</value>
    [ResourceEntry("CreateTaxonomyActionDescription", Description = "Create taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies creating a classification.")]
    public string CreateTaxonomyActionDescription => this[nameof (CreateTaxonomyActionDescription)];

    /// <summary>
    /// Translated message, similar to "Modify a classification"
    /// </summary>
    /// <value>Modify taxonomy action title.</value>
    [ResourceEntry("ModifyTaxonomyActionName", Description = "Modify taxonomy action title.", LastModified = "2010/07/19", Value = "Modify classification and manage classification items")]
    public string ModifyTaxonomyActionName => this[nameof (ModifyTaxonomyActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies modifying a classification."
    /// </summary>
    /// <value>Modify taxonomy action description.</value>
    [ResourceEntry("ModifyTaxonomyActionDescription", Description = "Modify taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies modifying a classification.")]
    public string ModifyTaxonomyActionDescription => this[nameof (ModifyTaxonomyActionDescription)];

    /// <summary>
    /// Translated message, similar to "Delete a classification"
    /// </summary>
    /// <value>Delete taxonomy action title.</value>
    [ResourceEntry("DeleteTaxonomyActionName", Description = "Delete taxonomy action title.", LastModified = "2010/06/22", Value = "Delete classification")]
    public string DeleteTaxonomyActionName => this[nameof (DeleteTaxonomyActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies deleting a classification."
    /// </summary>
    /// <value>Delete taxonomy action description.</value>
    [ResourceEntry("DeleteTaxonomyActionDescription", Description = "Delete taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies deleting a classification.")]
    public string DeleteTaxonomyActionDescription => this[nameof (DeleteTaxonomyActionDescription)];

    /// <summary>
    /// Translated message, similar to "Change classification owner"
    /// </summary>
    /// <value>Change owner taxonomy action title.</value>
    [ResourceEntry("ChangeTaxonomyOwnerActionName", Description = "Change owner of taxonomy action title.", LastModified = "2010/06/22", Value = "Change classification owner")]
    public string ChangeTaxonomyOwnerActionName => this[nameof (ChangeTaxonomyOwnerActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the ownership of a classification."
    /// </summary>
    /// <value>Change owner of a taxonomy action description.</value>
    [ResourceEntry("ChangeTaxonomyOwnerActionDescription", Description = "Change owner of a taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies changing the ownership of a classification.")]
    public string ChangeTaxonomyOwnerActionDescription => this[nameof (ChangeTaxonomyOwnerActionDescription)];

    /// <summary>
    /// Translated message, similar to "Change taxonomy permissions"
    /// </summary>
    /// <value>Change permissions of a taxonomy action title.</value>
    [ResourceEntry("ChangeTaxonomyPermissionsActionName", Description = "Change taxonomy permissions action title.", LastModified = "2010/06/22", Value = "Change classification permissions")]
    public string ChangeTaxonomyPermissionsActionName => this[nameof (ChangeTaxonomyPermissionsActionName)];

    /// <summary>
    /// Translated message, similar to "Allows or denies changing the permissions of a classification."
    /// </summary>
    /// <value>Change permissions of a taxonomy action description.</value>
    [ResourceEntry("ChangeTaxonomyPermissionsActionDescription", Description = "Change permissions of a taxonomy action description.", LastModified = "2010/06/22", Value = "Allows or denies changing the permissions of a classification.")]
    public string ChangeTaxonomyPermissionsActionDescription => this[nameof (ChangeTaxonomyPermissionsActionDescription)];

    /// <summary>Label: Page Templates Permissions</summary>
    [ResourceEntry("FormsPermissions", Description = "Forms permissions title.", LastModified = "2010/08/25", Value = "Forms Permissions")]
    public string FormsPermissions => this[nameof (FormsPermissions)];

    /// <summary>Message: Represents security permissions for forms.</summary>
    [ResourceEntry("FormsPermissionsDescription", Description = "Permissions description.", LastModified = "2010/08/25", Value = "Represents security permissions for forms.")]
    public string FormsPermissionsDescription => this[nameof (FormsPermissionsDescription)];

    /// <summary>Label: View form permissions</summary>
    [ResourceEntry("ViewForm", Description = "View form permissions title.", LastModified = "2018/10/31", Value = "View form")]
    public string ViewForm => this[nameof (ViewForm)];

    /// <summary>Label: Create form permissions</summary>
    [ResourceEntry("CreateForm", Description = "Create form permissions title.", LastModified = "2018/10/31", Value = "Create form")]
    public string CreateForm => this[nameof (CreateForm)];

    /// <summary>Label: Modify form permissions</summary>
    [ResourceEntry("ModifyForm", Description = "Modify form permissions title.", LastModified = "2018/10/31", Value = "Modify form")]
    public string ModifyForm => this[nameof (ModifyForm)];

    /// <summary>Label: Delete form permissions</summary>
    [ResourceEntry("DeleteForm", Description = "Delete form permissions title.", LastModified = "2018/10/31", Value = "Delete form")]
    public string DeleteForm => this[nameof (DeleteForm)];

    /// <summary>Label: Unlock form permissions</summary>
    [ResourceEntry("UnlockForm", Description = "Unlock form permissions title.", LastModified = "2018/10/31", Value = "Unlock form")]
    public string UnlockForm => this[nameof (UnlockForm)];

    /// <summary>Label: Change form owner permissions</summary>
    [ResourceEntry("ChangeFormOwner", Description = "Change form owner permissions title.", LastModified = "2018/10/31", Value = "Change form owner")]
    public string ChangeFormOwner => this[nameof (ChangeFormOwner)];

    /// <summary>Label: Manage Responses Permissions</summary>
    [ResourceEntry("ManageResponses", Description = "Manage responses permissions title.", LastModified = "2018/10/31", Value = "Manage responses")]
    public string ManageResponses => this[nameof (ManageResponses)];

    /// <summary>
    /// Message: Represents security permissions for form responses.
    /// </summary>
    [ResourceEntry("ManageResponsesDescription", Description = "Permissions description.", LastModified = "2018/10/31", Value = "View, Edit, and Delete responses")]
    public string ManageResponsesDescription => this[nameof (ManageResponsesDescription)];

    /// <summary>Label: View Responses Permissions</summary>
    [ResourceEntry("ViewResponses", Description = "Forms permissions title.", LastModified = "2018/10/31", Value = "View responses")]
    public string ViewResponses => this[nameof (ViewResponses)];

    /// <summary>Label: Workflow Permissions</summary>
    [ResourceEntry("WorkflowDefinitionPermissions", Description = "Workflow permissions title.", LastModified = "2010/10/28", Value = "Workflow Permissions")]
    public string WorkflowDefinitionPermissions => this[nameof (WorkflowDefinitionPermissions)];

    /// <summary>
    /// Message: Represents security permissions for Workflow.
    /// </summary>
    [ResourceEntry("WorkflowDefinitionPermissionsDescription", Description = "Permissions description.", LastModified = "2010/10/28", Value = "Represents security permissions for Workflow.")]
    public string WorkflowDefinitionPermissionsDescription => this[nameof (WorkflowDefinitionPermissionsDescription)];

    /// <summary>
    /// Translated message, similar to "Multisite management Permissions"
    /// </summary>
    /// <value>Multisite management permissions title.</value>
    [ResourceEntry("MultisiteManagementPermissions", Description = "Multisite management permissions title.", LastModified = "2012/08/06", Value = "Multisite management Permissions")]
    public string MultisiteManagementPermissions => this[nameof (MultisiteManagementPermissions)];

    /// <summary>
    /// Translated message, similar to "Represents security permissions for Multisite management."
    /// </summary>
    /// <value>Multisite management permissions description</value>
    [ResourceEntry("MultisiteManagementPermissionsDescription", Description = "Multisite management permissions description", LastModified = "2012/08/06", Value = "Represents security permissions for Multisite management.")]
    public string MultisiteManagementPermissionsDescription => this[nameof (MultisiteManagementPermissionsDescription)];

    /// <summary>Translated message, similar to "Access the site"</summary>
    /// <value>'Access the site' security action title</value>
    [ResourceEntry("AccessSite", Description = "'Access the site' security action title", LastModified = "2012/08/06", Value = "Access the site")]
    public string AccessSite => this[nameof (AccessSite)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot access sites"
    /// </summary>
    /// <value>'Access the site' security action description.</value>
    [ResourceEntry("AccessSiteDescription", Description = "'Access the site' security action description.", LastModified = "2012/08/06", Value = "Specify who can/cannot access sites")]
    public string AccessSiteDescription => this[nameof (AccessSiteDescription)];

    /// <summary>
    /// Translated message, similar to "Create and Edit a site"
    /// </summary>
    /// <value>'Create and Edit a site' security action title</value>
    [ResourceEntry("CreateEditSite", Description = "'Create and Edit a site' security action title", LastModified = "2012/09/24", Value = "Create and Edit a site")]
    public string CreateEditSite => this[nameof (CreateEditSite)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot create and edit sites."
    /// </summary>
    /// <value>'Create and edit a site' security action description.</value>
    [ResourceEntry("CreateEditSiteDescription", Description = "'Create and Edit a site' security action description.", LastModified = "2012/09/24", Value = "Specify who can/cannot create and edit sites.")]
    public string CreateEditSiteDescription => this[nameof (CreateEditSiteDescription)];

    /// <summary>Translated message, similar to "Delete a site"</summary>
    /// <value>'Delete a site' security action title</value>
    [ResourceEntry("DeleteSite", Description = "'Delete a site' security action title", LastModified = "2012/08/07", Value = "Delete a site")]
    public string DeleteSite => this[nameof (DeleteSite)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot delete sites."
    /// </summary>
    /// <value>'Delete a site' security action description</value>
    [ResourceEntry("DeleteSiteDescription", Description = "'Delete a site' security action description", LastModified = "2012/08/07", Value = "Specify who can/cannot delete sites.")]
    public string DeleteSiteDescription => this[nameof (DeleteSiteDescription)];

    /// <summary>Translated message, similar to "Configure modules"</summary>
    /// <value>'Configure modules' security action title</value>
    [ResourceEntry("ConfigureModules", Description = "'Configure modules' security action title", LastModified = "2012/08/07", Value = "Configure modules")]
    public string ConfigureModules => this[nameof (ConfigureModules)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot configure modules."
    /// </summary>
    /// <value>'Configure modules' security action description</value>
    [ResourceEntry("ConfigureModulesDescription", Description = "'Configure modules' security action description", LastModified = "2012/08/07", Value = "Specify who can/cannot configure modules.")]
    public string ConfigureModulesDescription => this[nameof (ConfigureModulesDescription)];

    /// <summary>Translated message, similar to "Start/stop a site"</summary>
    /// <value>'Start/stop a site' security action title</value>
    [ResourceEntry("StartStopSite", Description = "'Start/stop a site' security action title", LastModified = "2012/08/07", Value = "Start/stop a site")]
    public string StartStopSite => this[nameof (StartStopSite)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot start/stop sites."
    /// </summary>
    /// <value>'Start/stop a site' security action description</value>
    [ResourceEntry("StartStopSiteDescription", Description = "'Start/stop a site' security action description", LastModified = "2012/08/07", Value = "Specify who can/cannot start/stop sites.")]
    public string StartStopSiteDescription => this[nameof (StartStopSiteDescription)];

    /// <summary>Translated message, similar to "Change permissions"</summary>
    /// <value>'Change permissions' security action title</value>
    [ResourceEntry("ChangeSitePermissions", Description = "'Change permissions' security action title", LastModified = "2012/08/07", Value = "Change permissions")]
    public string ChangeSitePermissions => this[nameof (ChangeSitePermissions)];

    /// <summary>
    /// Translated message, similar to "Specify who can/cannot change site permissions."
    /// </summary>
    /// <value>'Change permissions' security action description</value>
    [ResourceEntry("ChangeSitePermissionsDescription", Description = "'Change permissions' security action description", LastModified = "2012/08/07", Value = "Specify who can/cannot change site permissions")]
    public string ChangeSitePermissionsDescription => this[nameof (ChangeSitePermissionsDescription)];

    /// <summary>Label: Customized title of Modify security action.</summary>
    [ResourceEntry("ModifyThisItem", Description = "Customized title of Modify security action.", LastModified = "2010/06/14", Value = "Modify this {0}")]
    public string ModifyThisItem => this[nameof (ModifyThisItem)];

    /// <summary>Label: Customized title of View security action.</summary>
    [ResourceEntry("ViewThisItem", Description = "Customized title of View security action.", LastModified = "2010/06/14", Value = "View this {0}")]
    public string ViewThisItem => this[nameof (ViewThisItem)];

    /// <summary>Label: Customized title of the Delete security action</summary>
    [ResourceEntry("DeleteThisItem", Description = "Customized title of the Delete security action.", LastModified = "2010/06/14", Value = "Delete this {0}")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>Label: Customized title of the Unlock security action</summary>
    [ResourceEntry("UnlockThisItem", Description = "Customized title of the Unlock security action.", LastModified = "2018/11/16", Value = "Unlock this {0}")]
    public string UnlockThisItem => this[nameof (UnlockThisItem)];

    /// <summary>
    /// Label: Customized title of the Change Owner security action
    /// </summary>
    [ResourceEntry("ChangeOwnerOfThisItem", Description = "Customized title of the Change Owner security action.", LastModified = "2010/06/14", Value = "Change owner of this {0}")]
    public string ChangeOwnerOfThisItem => this[nameof (ChangeOwnerOfThisItem)];

    /// <summary>
    /// Customized title of the Change Permssions security action
    /// </summary>
    [ResourceEntry("ChangePermissionsOfThisItem", Description = "Customized title of the Change Permssions security action.", LastModified = "2010/06/14", Value = "Change permissions of this {0}")]
    public string ChangePermissionsOfThisItem => this[nameof (ChangePermissionsOfThisItem)];

    /// <summary>ViewThisBlog action</summary>
    [ResourceEntry("ViewThisBlog", Description = "Action text of the view action for this blog.", LastModified = "2010/07/20", Value = "View this blog")]
    public string ViewThisBlog => this[nameof (ViewThisBlog)];

    /// <summary>ManageThisBlog action</summary>
    [ResourceEntry("ManageThisBlog", Description = "Action text of the modify action for this blog post.", LastModified = "2010/07/20", Value = "Update this blog and manage its blog posts")]
    public string ManageThisBlog => this[nameof (ManageThisBlog)];

    /// <summary>DeleteThisBlog action</summary>
    [ResourceEntry("DeleteThisBlog", Description = "Action text of the delete action for this blog.", LastModified = "2010/07/20", Value = "Delete this blog and its posts")]
    public string DeleteThisBlog => this[nameof (DeleteThisBlog)];

    /// <summary>ChangeThisBlogPermissions action</summary>
    [ResourceEntry("ChangeThisBlogPermissions", Description = "Action text of the change permissions action for this blog.", LastModified = "2010/07/20", Value = "Change this blog's permissions")]
    public string ChangeThisBlogPermissions => this[nameof (ChangeThisBlogPermissions)];

    /// <summary>ModifyThisBlogPost action</summary>
    [ResourceEntry("ChangeThisBlogOwner", Description = "Action text of the change owner action for this blog.", LastModified = "2010/07/20", Value = "Change this blog's owner")]
    public string ChangeThisBlogOwner => this[nameof (ChangeThisBlogOwner)];

    /// <summary>ViewThisBlogPost action</summary>
    [ResourceEntry("ViewThisBlogPost", Description = "Action text of the view action for this blog post.", LastModified = "2010/06/14", Value = "View this blog post")]
    public string ViewThisBlogPost => this[nameof (ViewThisBlogPost)];

    /// <summary>UnlockThisBlogPost action</summary>
    [ResourceEntry("UnlockThisBlogPost", Description = "Action text of the Unlock action for this blog post.", LastModified = "2018/11/16", Value = "Unlock this blog post")]
    public string UnlockThisBlogPost => this[nameof (UnlockThisBlogPost)];

    /// <summary>ManageThisBlogPost action</summary>
    [ResourceEntry("ManageThisBlogPost", Description = "Action text of the modify action for this blog post.", LastModified = "2010/07/20", Value = "Manage this blog post")]
    public string ManageThisBlogPost => this[nameof (ManageThisBlogPost)];

    /// <summary>ModifyThisBlogPost action</summary>
    [ResourceEntry("DeleteThisBlogPost", Description = "Action text of the delete action for this blog post.", LastModified = "2010/06/14", Value = "Delete this blog post")]
    public string DeleteThisBlogPost => this[nameof (DeleteThisBlogPost)];

    /// <summary>ModifyThisBlogPost action</summary>
    [ResourceEntry("ChangeThisBlogPostPermissions", Description = "Action text of the change permissions action for this blog post.", LastModified = "2010/06/14", Value = "Change this blog post's permissions")]
    public string ChangeThisBlogPostPermissions => this[nameof (ChangeThisBlogPostPermissions)];

    /// <summary>ModifyThisBlogPost action</summary>
    [ResourceEntry("ChangeThisBlogPostOwner", Description = "Action text of the change owner action for this blog post.", LastModified = "2010/06/14", Value = "Change this blog post's owner")]
    public string ChangeThisBlogPostOwner => this[nameof (ChangeThisBlogPostOwner)];

    /// <summary>ViewThisImage action</summary>
    [ResourceEntry("ViewThisImage", Description = "Action text of the view action for this image.", LastModified = "2010/06/14", Value = "View this image")]
    public string ViewThisImage => this[nameof (ViewThisImage)];

    /// <summary>UnlockThisImage action</summary>
    [ResourceEntry("UnlockThisImage", Description = "Action text of the Unlock action for this image.", LastModified = "2018/11/16", Value = "Unlock this image")]
    public string UnlockThisImage => this[nameof (UnlockThisImage)];

    /// <summary>ModifyThisImage action</summary>
    [ResourceEntry("ModifyThisImage", Description = "Action text of the modify action for this image.", LastModified = "2010/06/14", Value = "Modify this image")]
    public string ModifyThisImage => this[nameof (ModifyThisImage)];

    /// <summary>ModifyThisImage action</summary>
    [ResourceEntry("DeleteThisImage", Description = "Action text of the delete action for this image.", LastModified = "2010/06/14", Value = "Delete this image")]
    public string DeleteThisImage => this[nameof (DeleteThisImage)];

    /// <summary>ModifyThisImage action</summary>
    [ResourceEntry("ChangeThisImagePermissions", Description = "Action text of the change permissions action for this image.", LastModified = "2010/06/14", Value = "Change this image's permissions")]
    public string ChangeThisImagePermissions => this[nameof (ChangeThisImagePermissions)];

    /// <summary>ModifyThisImage action</summary>
    [ResourceEntry("ChangeThisImageOwner", Description = "Action text of the change owner action for this image.", LastModified = "2010/06/14", Value = "Change this image's owner")]
    public string ChangeThisImageOwner => this[nameof (ChangeThisImageOwner)];

    /// <summary>ViewThisAlbum action</summary>
    [ResourceEntry("ViewThisAlbum", Description = "Action text of the view action for this image library.", LastModified = "2010/07/13", Value = "View this image library")]
    public string ViewThisAlbum => this[nameof (ViewThisAlbum)];

    /// <summary>ModifyThisAlbum action</summary>
    [ResourceEntry("ModifyThisAlbum", Description = "Action text of the modify action for this image library.", LastModified = "2010/07/13", Value = "Modify this image library")]
    public string ModifyThisAlbum => this[nameof (ModifyThisAlbum)];

    /// <summary>DeleteThisAlbum action</summary>
    [ResourceEntry("DeleteThisAlbum", Description = "Action text of the delete action for this image library.", LastModified = "2010/07/13", Value = "Delete this image library")]
    public string DeleteThisAlbum => this[nameof (DeleteThisAlbum)];

    /// <summary>ChangeThisAlbumPermissions action</summary>
    [ResourceEntry("ChangeThisAlbumPermissions", Description = "Action text of the change permissions action for this image library.", LastModified = "2010/07/13", Value = "Change this image library's permissions")]
    public string ChangeThisAlbumPermissions => this[nameof (ChangeThisAlbumPermissions)];

    /// <summary>ChangeThisAlbumOwner action</summary>
    [ResourceEntry("ChangeThisAlbumOwner", Description = "Action text of the change owner action for this image library.", LastModified = "2010/07/13", Value = "Change this image library's owner")]
    public string ChangeThisAlbumOwner => this[nameof (ChangeThisAlbumOwner)];

    /// <summary>ViewThisVideo action</summary>
    [ResourceEntry("ViewThisVideo", Description = "Action text of the view action for this video.", LastModified = "2010/06/14", Value = "View this video")]
    public string ViewThisVideo => this[nameof (ViewThisVideo)];

    /// <summary>UnlockThisVideo action</summary>
    [ResourceEntry("UnlockThisVideo", Description = "Action text of the Unlock action for this video.", LastModified = "2018/11/16", Value = "Unlock this video")]
    public string UnlockThisVideo => this[nameof (UnlockThisVideo)];

    /// <summary>ModifyThisVideo action</summary>
    [ResourceEntry("ModifyThisVideo", Description = "Action text of the modify action for this video.", LastModified = "2010/06/14", Value = "Modify this video")]
    public string ModifyThisVideo => this[nameof (ModifyThisVideo)];

    /// <summary>ModifyThisVideo action</summary>
    [ResourceEntry("DeleteThisVideo", Description = "Action text of the delete action for this video.", LastModified = "2010/06/14", Value = "Delete this video")]
    public string DeleteThisVideo => this[nameof (DeleteThisVideo)];

    /// <summary>ModifyThisVideo action</summary>
    [ResourceEntry("ChangeThisVideoPermissions", Description = "Action text of the change permissions action for this video.", LastModified = "2010/06/14", Value = "Change this video's permissions")]
    public string ChangeThisVideoPermissions => this[nameof (ChangeThisVideoPermissions)];

    /// <summary>ModifyThisVideo action</summary>
    [ResourceEntry("ChangeThisVideoOwner", Description = "Action text of the change owner action for this video.", LastModified = "2010/06/14", Value = "Change this video's owner")]
    public string ChangeThisVideoOwner => this[nameof (ChangeThisVideoOwner)];

    /// <summary>ViewThisVideoLibrary action</summary>
    [ResourceEntry("ViewThisVideoLibrary", Description = "Action text of the view action for this video library.", LastModified = "2010/07/26", Value = "View this video library")]
    public string ViewThisVideoLibrary => this[nameof (ViewThisVideoLibrary)];

    /// <summary>ModifyThisVideoLibrary action</summary>
    [ResourceEntry("ModifyThisVideoLibrary", Description = "Action text of the modify action for this video library.", LastModified = "2010/07/26", Value = "Modify this video library")]
    public string ModifyThisVideoLibrary => this[nameof (ModifyThisVideoLibrary)];

    /// <summary>DeleteThisVideoLibrary action</summary>
    [ResourceEntry("DeleteThisVideoLibrary", Description = "Action text of the delete action for this video library.", LastModified = "2010/07/26", Value = "Delete this video library")]
    public string DeleteThisVideoLibrary => this[nameof (DeleteThisVideoLibrary)];

    /// <summary>ChangeThisVideoLibraryPermissions action</summary>
    [ResourceEntry("ChangeThisVideoLibraryPermissions", Description = "Action text of the change permissions action for this video library.", LastModified = "2010/07/26", Value = "Change this video library's permissions")]
    public string ChangeThisVideoLibraryPermissions => this[nameof (ChangeThisVideoLibraryPermissions)];

    /// <summary>ChangeThisVideoLibraryOwner action</summary>
    [ResourceEntry("ChangeThisVideoLibraryOwner", Description = "Action text of the change owner action for this video library.", LastModified = "2010/07/26", Value = "Change this video library's owner")]
    public string ChangeThisVideoLibraryOwner => this[nameof (ChangeThisVideoLibraryOwner)];

    /// <summary>ViewThisDocument action</summary>
    [ResourceEntry("ViewThisDocument", Description = "Action text of the view action for this document.", LastModified = "2010/06/14", Value = "View this document")]
    public string ViewThisDocument => this[nameof (ViewThisDocument)];

    /// <summary>UnlockThisDocument action</summary>
    [ResourceEntry("UnlockThisDocument", Description = "Action text of the Unlock action for this document.", LastModified = "2018/11/16", Value = "Unlock this document")]
    public string UnlockThisDocument => this[nameof (UnlockThisDocument)];

    /// <summary>ModifyThisDocument action</summary>
    [ResourceEntry("ModifyThisDocument", Description = "Action text of the modify action for this document.", LastModified = "2010/06/14", Value = "Modify this document")]
    public string ModifyThisDocument => this[nameof (ModifyThisDocument)];

    /// <summary>ModifyThisDocument action</summary>
    [ResourceEntry("DeleteThisDocument", Description = "Action text of the delete action for this document.", LastModified = "2010/06/14", Value = "Delete this document")]
    public string DeleteThisDocument => this[nameof (DeleteThisDocument)];

    /// <summary>ModifyThisDocument action</summary>
    [ResourceEntry("ChangeThisDocumentPermissions", Description = "Action text of the change permissions action for this document.", LastModified = "2010/06/14", Value = "Change this document's permissions")]
    public string ChangeThisDocumentPermissions => this[nameof (ChangeThisDocumentPermissions)];

    /// <summary>ModifyThisDocument action</summary>
    [ResourceEntry("ChangeThisDocumentOwner", Description = "Action text of the change owner action for this document.", LastModified = "2010/06/14", Value = "Change this document's owner")]
    public string ChangeThisDocumentOwner => this[nameof (ChangeThisDocumentOwner)];

    /// <summary>ViewThisDocumentLibrary action</summary>
    [ResourceEntry("ViewThisDocumentLibrary", Description = "Action text of the view action for this document library.", LastModified = "2010/07/26", Value = "View this document library")]
    public string ViewThisDocumentLibrary => this[nameof (ViewThisDocumentLibrary)];

    /// <summary>ModifyThisDocumentLibrary action</summary>
    [ResourceEntry("ModifyThisDocumentLibrary", Description = "Action text of the modify action for this document library.", LastModified = "2010/07/26", Value = "Modify this document library")]
    public string ModifyThisDocumentLibrary => this[nameof (ModifyThisDocumentLibrary)];

    /// <summary>DeleteThisDocumentLibrary action</summary>
    [ResourceEntry("DeleteThisDocumentLibrary", Description = "Action text of the delete action for this document library.", LastModified = "2010/07/26", Value = "Delete this document library")]
    public string DeleteThisDocumentLibrary => this[nameof (DeleteThisDocumentLibrary)];

    /// <summary>ChangeThisDocumentLibraryPermissions action</summary>
    [ResourceEntry("ChangeThisDocumentLibraryPermissions", Description = "Action text of the change permissions action for this document library.", LastModified = "2010/07/26", Value = "Change this document library's permissions")]
    public string ChangeThisDocumentLibraryPermissions => this[nameof (ChangeThisDocumentLibraryPermissions)];

    /// <summary>ChangeThisDocumentLibraryOwner action</summary>
    [ResourceEntry("ChangeThisDocumentLibraryOwner", Description = "Action text of the change owner action for this document library.", LastModified = "2010/07/26", Value = "Change this document library's owner")]
    public string ChangeThisDocumentLibraryOwner => this[nameof (ChangeThisDocumentLibraryOwner)];

    /// <summary>ViewThisUserFileLibrary action</summary>
    [ResourceEntry("ViewThisUserFileLibrary", Description = "Action text of the view action for this user file library.", LastModified = "2012/02/06", Value = "View this folder")]
    public string ViewThisUserFileLibrary => this[nameof (ViewThisUserFileLibrary)];

    /// <summary>ModifyThisUserFileLibrary action</summary>
    [ResourceEntry("ModifyThisUserFileLibrary", Description = "Action text of the modify action for this user file library.", LastModified = "2012/02/06", Value = "Update folder and manage its items")]
    public string ModifyThisUserFileLibrary => this[nameof (ModifyThisUserFileLibrary)];

    /// <summary>DeleteThisUserFileLibrary action</summary>
    [ResourceEntry("DeleteThisUserFileLibrary", Description = "Action text of the delete action for this user file library.", LastModified = "2012/02/06", Value = "Delete this folder")]
    public string DeleteThisUserFileLibrary => this[nameof (DeleteThisUserFileLibrary)];

    /// <summary>ChangeThisUserFileLibraryPermissions action</summary>
    [ResourceEntry("ChangeThisUserFileLibraryPermissions", Description = "Action text of the change permissions action for this user file library.", LastModified = "2012/02/06", Value = "Change this folder's permissions")]
    public string ChangeThisUserFileLibraryPermissions => this[nameof (ChangeThisUserFileLibraryPermissions)];

    /// <summary>ChangeThisUserFileLibraryOwner action</summary>
    [ResourceEntry("ChangeThisUserFileLibraryOwner", Description = "Action text of the change owner action for this user file library.", LastModified = "2012/02/06", Value = "Change this folder's owner")]
    public string ChangeThisUserFileLibraryOwner => this[nameof (ChangeThisUserFileLibraryOwner)];

    /// <summary>ModifyATaxonomy action</summary>
    [ResourceEntry("ModifyATaxonomy", Description = "Action text of the modify action for a taxonomy.", LastModified = "2010/07/19", Value = "Modify classification and manage {0} items")]
    public string ModifyATaxonomy => this[nameof (ModifyATaxonomy)];

    /// <summary>View action: View pages</summary>
    [ResourceEntry("ViewPages", Description = "Action text of the View action for the root node of pages.", LastModified = "2011/05/17", Value = "View pages")]
    public string ViewPages => this[nameof (ViewPages)];

    /// <summary>Unlock action: Unlock pages</summary>
    [ResourceEntry("UnlockPages", Description = "Action text of the Unlock action for the root node of pages.", LastModified = "2011/05/17", Value = "Unlock pages")]
    public string UnlockPages => this[nameof (UnlockPages)];

    /// <summary>Create action: Create pages</summary>
    [ResourceEntry("CreatePages", Description = "Action text of the Create action for the root node of pages.", LastModified = "2011/05/17", Value = "Create pages")]
    public string CreatePages => this[nameof (CreatePages)];

    /// <summary>View action: Modify page properties</summary>
    [ResourceEntry("ModifyPages", Description = "Action text of the Modify action for the root node of pages.", LastModified = "2011/05/17", Value = "Modify page properties")]
    public string ModifyPages => this[nameof (ModifyPages)];

    /// <summary>Delete action: Delete pages</summary>
    [ResourceEntry("DeletePages", Description = "Action text of the Create action for the root node of pages.", LastModified = "2011/05/17", Value = "Delete pages")]
    public string DeletePages => this[nameof (DeletePages)];

    /// <summary>Edit action: Edit page content</summary>
    [ResourceEntry("EditPagesContent", Description = "Action text of the Edit action for the root node of pages.", LastModified = "2011/05/17", Value = "Edit page content")]
    public string EditPagesContent => this[nameof (EditPagesContent)];

    /// <summary>
    /// Create Controls action: Add widgets and layout elements to the page
    /// </summary>
    [ResourceEntry("CreatePagesChildControls", Description = "Action text of the Add Widgets action for the root node of pages.", LastModified = "2011/05/17", Value = "Add widgets and layout elements to the page")]
    public string CreatePagesChildControls => this[nameof (CreatePagesChildControls)];

    /// <summary>Change Permissions action: Change permissions</summary>
    [ResourceEntry("ChangePagesPermissions", Description = "Action text of the Change Permissions action for the root node of pages.", LastModified = "2011/05/17", Value = "Change permissions")]
    public string ChangePagesPermissions => this[nameof (ChangePagesPermissions)];

    /// <summary>Change Owner action: Change page owner</summary>
    [ResourceEntry("ChangePagesOwner", Description = "Action text of the Change Owner action for the root node of pages.", LastModified = "2011/05/17", Value = "Change page owner")]
    public string ChangePagesOwner => this[nameof (ChangePagesOwner)];

    /// <summary>Text of the delete action for a site</summary>
    [ResourceEntry("DeleteThisSite", Description = "Text of the delete action for a site.", LastModified = "2012/08/07", Value = "Delete this site")]
    public string DeleteThisSite => this[nameof (DeleteThisSite)];

    /// <summary>Text of the configure modules action for a site</summary>
    [ResourceEntry("ConfigureThisSiteModules", Description = "Text of the configure modules action for a site.", LastModified = "2012/08/07", Value = "Configure this site modules")]
    public string ConfigureThisSiteModules => this[nameof (ConfigureThisSiteModules)];

    /// <summary>Text of the start/stop action for a site</summary>
    [ResourceEntry("StartStopThisSite", Description = "Text of the start/stop action for a site.", LastModified = "2012/08/07", Value = "Set online/offline this site")]
    public string StartStopThisSite => this[nameof (StartStopThisSite)];

    /// <summary>Text of the change permissions action for a site</summary>
    [ResourceEntry("ChangePermissionsForThisSite", Description = "Text of the change permissions action for a site.", LastModified = "2012/08/07", Value = "Change permissions for this site")]
    public string ChangePermissionsForThisSite => this[nameof (ChangePermissionsForThisSite)];

    /// <summary>Phrase: Sitemap Generation Permissions</summary>
    /// <value></value>
    [ResourceEntry("SitemapGenerationPermissions", Description = "Phrase: Sitemap Generation Permissions", LastModified = "2014/11/27", Value = "Sitemap Generation Permissions")]
    public string SitemapGenerationPermissions => this[nameof (SitemapGenerationPermissions)];

    /// <summary>
    /// Phrase: Represents security permissions for sitemap generation actions.
    /// </summary>
    [ResourceEntry("SitemapGenerationPermissionsDescription", Description = "Phrase: Represents security permissions for sitemap generation actions.", LastModified = "2014/11/27", Value = "Represents security permissions for sitemap generation actions.")]
    public string SitemapGenerationPermissionsDescription => this[nameof (SitemapGenerationPermissionsDescription)];

    /// <summary>Phrase: View Backend Link</summary>
    [ResourceEntry("ViewBackendLinkTitle", Description = "Phrase: View {0} backend link", LastModified = "2014/11/27", Value = "View {0} backend link")]
    public string ViewBackendLinkTitle => this[nameof (ViewBackendLinkTitle)];

    /// <summary>
    /// Phrase: Allows or denies access to the backend menu link of the module.
    /// </summary>
    [ResourceEntry("ViewBackendLinkDescription", Description = "Phrase: Allows or denies access to the backend menu link of the module.", LastModified = "2014/11/27", Value = "Allows or denies access to the backend menu link of the module.")]
    public string ViewBackendLinkDescription => this[nameof (ViewBackendLinkDescription)];

    /// <summary>Phrase: Change backend link permissions</summary>
    [ResourceEntry("ChangeBackendLinkPermissions", Description = "Phrase: Change {0} backend link permissions", LastModified = "2014/11/27", Value = "Change {0} backend link permissions")]
    public string ChangeBackendLinkPermissions => this[nameof (ChangeBackendLinkPermissions)];

    /// <summary>
    /// Phrase: Allows or denies changing the view backend link permission.
    /// </summary>
    [ResourceEntry("ChangeBackendLinkPermissionsDescription", Description = "Phrase: Allows or denies changing the view backend link permission.", LastModified = "2014/11/27", Value = "Allows or denies changing the view backend link permission.")]
    public string ChangeBackendLinkPermissionsDescription => this[nameof (ChangeBackendLinkPermissionsDescription)];

    /// <summary>Module titles for the General permission set: Blog</summary>
    /// <value>blog</value>
    [ResourceEntry("BlogGeneralActionTitle", Description = "Module titles for the General permission set: Blog.", LastModified = "2010/05/15", Value = "blog")]
    public string BlogGeneralActionTitle => this[nameof (BlogGeneralActionTitle)];

    /// <summary>Module titles for the General permission set: Event</summary>
    /// <value>event</value>
    [ResourceEntry("EventGeneralActionTitle", Description = "Module titles for the General permission set: Event.", LastModified = "2012/11/14", Value = "event")]
    public string EventGeneralActionTitle => this[nameof (EventGeneralActionTitle)];

    /// <summary>
    /// Module titles for the General permission set: Content block
    /// </summary>
    /// <value>event</value>
    [ResourceEntry("ContentGeneralActionTitle", Description = "Module titles for the General permission set: Content block.", LastModified = "2010/05/15", Value = "content block")]
    public string ContentGeneralActionTitle => this[nameof (ContentGeneralActionTitle)];

    /// <summary>Module titles for the General permission set: News</summary>
    /// <value>event</value>
    [ResourceEntry("NewsActionPermissionsListTitle", Description = "Module titles for the General permission set: News.", LastModified = "2010/05/15", Value = "news")]
    public string NewsActionPermissionsListTitle => this[nameof (NewsActionPermissionsListTitle)];

    /// <summary>Module titles for the General permission set: News</summary>
    /// <value>event</value>
    [ResourceEntry("WorkflowActionPermissionsListTitle", Description = "Module titles for the General permission set: Workflow.", LastModified = "2010/11/08", Value = "workflow")]
    public string WorkflowActionPermissionsListTitle => this[nameof (WorkflowActionPermissionsListTitle)];

    /// <summary>Security action: Change the owner of this site</summary>
    [ResourceEntry("ChangeOwnerForThisSite", Description = "Security action: Change the owner of this site", LastModified = "2012/10/03", Value = "Change the owner of this site")]
    public string ChangeOwnerForThisSite => this[nameof (ChangeOwnerForThisSite)];

    /// <summary>
    /// Error message: Granular permissions are disabled for your license.
    /// </summary>
    /// <value>Granular permissions are disabled for your license.</value>
    [ResourceEntry("GranularPermissionsAreDisabled", Description = "Error message: Granular permissions are disabled for your license.", LastModified = "2010/10/15", Value = "Granular permissions are disabled for your license.")]
    public string GranularPermissionsAreDisabled => this[nameof (GranularPermissionsAreDisabled)];

    /// <summary>
    /// Error message: Granular permissions for pages are disabled for your license.
    /// </summary>
    /// <value>Granular permissions for pages are disabled for your license.</value>
    [ResourceEntry("GranularPermissionsForPagesAreDisabled", Description = "Error message: Granular permissions for pages are disabled for your license.", LastModified = "2010/12/22", Value = "Granular permissions for pages are disabled for your license.")]
    public string GranularPermissionsForPagesAreDisabled => this[nameof (GranularPermissionsForPagesAreDisabled)];

    /// <summary>
    /// Error message: Permission setting for this object is disabled on your license.
    /// </summary>
    [ResourceEntry("PermissionSettingForThisObjectIsDisabledOnYourLicense", Description = "Error message: Permission setting for this object is disabled on your license.", LastModified = "2010/12/22", Value = "The license will not be updated because ")]
    public string PermissionSettingForThisObjectIsDisabledOnYourLicense => this[nameof (PermissionSettingForThisObjectIsDisabledOnYourLicense)];

    /// <summary>
    /// Error message: Load balancing is disabled for your license.
    /// </summary>
    /// <value>Load balancing is disabled for your license.</value>
    [ResourceEntry("LoadBalancingIsDisabled", Description = "Error message: Load balancing is disabled for your license.", LastModified = "2011/02/18", Value = "Load balancing is disabled for your license.")]
    public string LoadBalancingIsDisabled => this[nameof (LoadBalancingIsDisabled)];

    /// <summary>Label: Backend Roles</summary>
    [ResourceEntry("BackendRoles", Description = "The title of the default backend role provider.", LastModified = "2009/05/19", Value = "Backend Roles")]
    public string BackendRoles => this[nameof (BackendRoles)];

    /// <summary>
    /// Label: The default role provider for backend users (site administrators).
    /// </summary>
    [ResourceEntry("BackendRolesDescription", Description = "Description of the default backend role provider.", LastModified = "2009/05/19", Value = "The default role provider for backend users (site administrators).")]
    public string BackendRolesDescription => this[nameof (BackendRolesDescription)];

    /// <summary>Label: Backend Roles</summary>
    [ResourceEntry("FrontendRoles", Description = "The title of the default frontend role provider.", LastModified = "2009/05/19", Value = "Public Roles")]
    public string FrontendRoles => this[nameof (FrontendRoles)];

    /// <summary>
    /// Label: The default role provider for frontend (public) users.
    /// </summary>
    [ResourceEntry("FrontendRolesDescription", Description = "Description of the default backend role provider.", LastModified = "2009/05/19", Value = "The default role provider for frontend (public) users.")]
    public string FrontendRolesDescription => this[nameof (FrontendRolesDescription)];

    /// <summary>Label: Backend Roles</summary>
    [ResourceEntry("AppRoles", Description = "The title of the application role provider.", LastModified = "2009/05/19", Value = "Application Roles")]
    public string AppRoles => this[nameof (AppRoles)];

    /// <summary>
    /// Label: Stores information about application roles.
    /// Application roles are special roles that are assigned to users automatically based on predefined conditions.
    /// </summary>
    [ResourceEntry("AppRolesDescription", Description = "Description of the application role provider.", LastModified = "2012/01/05", Value = "Stores information about application roles. Application roles are special roles that are assigned to users automatically based on predefined conditions.")]
    public string AppRolesDescription => this[nameof (AppRolesDescription)];

    /// <summary>Label: Backend Users provider</summary>
    [ResourceEntry("Default", Description = "The title of the default backend membership provider.", LastModified = "2010/07/30", Value = "Default")]
    public string Default => this[nameof (Default)];

    /// <summary>Label: Backend Users role name</summary>
    [ResourceEntry("BackendUsers", Description = "Name of the backend users role name.", LastModified = "2010/07/26", Value = "all Backend users")]
    public string BackendUsers => this[nameof (BackendUsers)];

    /// <summary>
    /// Label: The default membership provider for backend users (site administrators).
    /// </summary>
    [ResourceEntry("BackendUsersDescription", Description = "Description of the default backend role provider.", LastModified = "2009/05/19", Value = "The default membership provider for backend users (site administrators).")]
    public string BackendUsersDescription => this[nameof (BackendUsersDescription)];

    /// <summary>Label: Backend Users</summary>
    [ResourceEntry("BackendUsersRole", Description = "The title of application role for backend users.", LastModified = "2010/03/16", Value = "Backend users")]
    public string BackendUsersRole => this[nameof (BackendUsersRole)];

    /// <summary>
    /// Application role for users which are allowed to access the backend.
    /// </summary>
    [ResourceEntry("BackendUsersRoleDescription", Description = "Application role for users which are allowed to access the backend.", LastModified = "2010/03/16", Value = "The description of application role for backend users.")]
    public string BackendUsersRoleDescription => this[nameof (BackendUsersRoleDescription)];

    /// <summary>Authors role title</summary>
    [ResourceEntry("Authors", Description = "Authors role title.", LastModified = "2010/07/30", Value = "Authors")]
    public string Authors => this[nameof (Authors)];

    /// <summary>Designers role title</summary>
    [ResourceEntry("Designers", Description = "Designers role title.", LastModified = "2010/07/30", Value = "Designers")]
    public string Designers => this[nameof (Designers)];

    /// <summary>Editors role title</summary>
    [ResourceEntry("Editors", Description = "Editors role title.", LastModified = "2010/07/30", Value = "Editors")]
    public string Editors => this[nameof (Editors)];

    /// <summary>
    /// Application role for users which are allowed to author content.
    /// </summary>
    [ResourceEntry("AuthorsRoleDescription", Description = "Application role for users which are allowed to author content.", LastModified = "2010/07/29", Value = "The description of application role for authors.")]
    public string AuthorsRoleDescription => this[nameof (AuthorsRoleDescription)];

    /// <summary>
    /// Application role for users which are allowed to design.
    /// </summary>
    [ResourceEntry("DesignersRoleDescription", Description = "Application role for users which are allowed to design.", LastModified = "2010/07/29", Value = "The description of application role for backend users.")]
    public string DesignersRoleDescription => this[nameof (DesignersRoleDescription)];

    /// <summary>
    /// Application role for users which are allowed to design.
    /// </summary>
    [ResourceEntry("FrontendUsersRoleDescription", Description = "Application role for users which are allowed to design.", LastModified = "2012/01/24", Value = "The description of application role for fronend users.")]
    public string FrontendUsersRoleDescription => this[nameof (FrontendUsersRoleDescription)];

    /// <summary>Application role for users which are allowed to edit.</summary>
    [ResourceEntry("EditorsRoleDescription", Description = "Application role for users which are allowed to edit.", LastModified = "2010/07/29", Value = "The description of application role for backend users.")]
    public string EditorsRoleDescription => this[nameof (EditorsRoleDescription)];

    /// <summary>Label: Public Users</summary>
    [ResourceEntry("FrontendUsers", Description = "The title of the default frontend membership provider.", LastModified = "2009/05/19", Value = "Public Users")]
    public string FrontendUsers => this[nameof (FrontendUsers)];

    /// <summary>Title: Permissions for frontend pages</summary>
    /// <value></value>
    [ResourceEntry("PermissionsForFrontendPages", Description = "Title: Permissions for frotend pages.", LastModified = "2010/05/03", Value = "Permissions for frontend pages")]
    public string PermissionsForFrontendPages => this[nameof (PermissionsForFrontendPages)];

    /// <summary>Translated message, similar to "Select a section"</summary>
    /// <value>Label in Global Permissions view</value>
    [ResourceEntry("SelectSection", Description = "Label in Global Permissions view", LastModified = "2010/10/12", Value = "Select a section")]
    public string SelectSection => this[nameof (SelectSection)];

    /// <summary>Phrase: Create a role</summary>
    [ResourceEntry("CreateARole", Description = "Text for the create a role button", LastModified = "2009/11/4", Value = "Create a role")]
    public string CreateARole => this[nameof (CreateARole)];

    /// <summary>Phrase: Registered on</summary>
    [ResourceEntry("RegisteredOn", Description = "Displays the registration date in grids that list users.", LastModified = "2010/04/28", Value = "Registered on")]
    public string RegisteredOn => this[nameof (RegisteredOn)];

    /// <summary>Phrase: Assign or unassign users</summary>
    [ResourceEntry("AssignOrUnassignUsers", Description = "Phrase: assign or unassign users", LastModified = "2009/11/4", Value = "Assign or Unassign Users")]
    public string AssignOrUnassignUsers => this[nameof (AssignOrUnassignUsers)];

    /// <summary>Phrase: Assign or Unassign Users to Role '{0}'</summary>
    [ResourceEntry("AssignOrUnassignUsersToRoleName", Description = "Phrase: Assign or Unassign Users to Role '{0}'", LastModified = "2010/04/28", Value = "Assign or Unassign Users <strong>to <em>{0}</em> role</strong>")]
    public string AssignOrUnassignUsersToRoleName => this[nameof (AssignOrUnassignUsersToRoleName)];

    /// <summary>phrase: List of available roles</summary>
    [ResourceEntry("ListOfAvailableRoles", Description = "phrase: List of available roles", LastModified = "2009/11/4", Value = "List of available roles")]
    public string ListOfAvailableRoles => this[nameof (ListOfAvailableRoles)];

    /// <summary>phrase: Create this user</summary>
    [ResourceEntry("CreateThisUser", Description = "phrase: Create this user", LastModified = "2009/11/4", Value = "Create this user")]
    public string CreateThisUser => this[nameof (CreateThisUser)];

    /// <summary>phrase: Membership Providers</summary>
    [ResourceEntry("MembershipProviders", Description = "phrase: Membership Providers", LastModified = "2009/11/4", Value = "Membership Providers")]
    public string MembershipProviders => this[nameof (MembershipProviders)];

    /// <summary>phrase: Create a user</summary>
    [ResourceEntry("CreateAUser", Description = "phrase: Create a user", LastModified = "2009/11/4", Value = "Create a user")]
    public string CreateAUser => this[nameof (CreateAUser)];

    /// <summary>create or modify users</summary>
    [ResourceEntry("CreateOrModifyUsers", Description = "create or modify users", LastModified = "2009/11/4", Value = "create or modify users")]
    public string CreateOrModifyUsers => this[nameof (CreateOrModifyUsers)];

    /// <summary>phrase: change user's password</summary>
    [ResourceEntry("ChangeUsersPassword", Description = "phrase: change user's password", LastModified = "2010/09/21", Value = "change user's password")]
    public string ChangeUsersPassword => this[nameof (ChangeUsersPassword)];

    /// <summary>create or modify roles</summary>
    [ResourceEntry("CreateOrModifyRoles", Description = "create or modify roles", LastModified = "2009/11/4", Value = "create or modify roles")]
    public string CreateOrModifyRoles => this[nameof (CreateOrModifyRoles)];

    /// <summary>phrase: The password you have entered is not valid</summary>
    [ResourceEntry("InvalidPassword", Description = "phrase: The password you have entered is not valid.", LastModified = "2009/11/4", Value = "The password you have entered is not valid.")]
    public string InvalidPassword => this[nameof (InvalidPassword)];

    /// <summary>phrase: New user has been successfully created</summary>
    [ResourceEntry("NewUserCreated", Description = "phrase: New user has been successfully created.", LastModified = "2009/11/11", Value = "New user has been successfully created.")]
    public string NewUserCreated => this[nameof (NewUserCreated)];

    /// <summary>phrase: Edit user</summary>
    [ResourceEntry("EditUser", Description = "phrase: Edit user", LastModified = "2009/11/11", Value = "Edit user")]
    public string EditUser => this[nameof (EditUser)];

    /// <summary>phrase: The username cannot be changed</summary>
    [ResourceEntry("UsernameCannotBeChanged", Description = "phrase: The username cannot be changed", LastModified = "2009/11/11", Value = "The username cannot be changed")]
    public string UsernameCannotBeChanged => this[nameof (UsernameCannotBeChanged)];

    /// <summary>phrase: Once created the username cannot be changed</summary>
    [ResourceEntry("OnceCreatedUsernameCannotBeChanged", Description = "phrase: The username cannot be changed", LastModified = "2009/11/11", Value = "Once created the username cannot be changed")]
    public string OnceCreatedUsernameCannotBeChanged => this[nameof (OnceCreatedUsernameCannotBeChanged)];

    /// <summary>phrase: Reset password</summary>
    [ResourceEntry("ResetPassword", Description = "phrase: Reset password", LastModified = "2009/11/11", Value = "Reset password")]
    public string ResetPassword => this[nameof (ResetPassword)];

    /// <summary>
    /// phrase: Click to automatically create a new password for this user.
    /// </summary>
    [ResourceEntry("ResetPasswordDescription", Description = "phrase: Click to automatically create a new password for this user.", LastModified = "2009/11/11", Value = "Click to automatically create a new password for this user.")]
    public string ResetPasswordDescription => this[nameof (ResetPasswordDescription)];

    /// <summary>phrase: Change password</summary>
    [ResourceEntry("ChangePassword", Description = "phrase: Change password", LastModified = "2010/09/20", Value = "Change password")]
    public string ChangePassword => this[nameof (ChangePassword)];

    /// <summary>phrase: Change password</summary>
    [ResourceEntry("Password", Description = "phrase: Password", LastModified = "2016/11/30", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>phrase: Who can</summary>
    [ResourceEntry("WhoCan", Description = "phrase: Who can", LastModified = "2009/11/13", Value = "Who can")]
    public string WhoCan => this[nameof (WhoCan)];

    /// <summary>phrase: Who can...</summary>
    [ResourceEntry("WhoCanDots", Description = "phrase: Who can...", LastModified = "2011/03/17", Value = "Who can...")]
    public string WhoCanDots => this[nameof (WhoCanDots)];

    /// <summary>phrase: Add roles or users</summary>
    [ResourceEntry("AddRolesOrUsers", Description = "phrase: Add roles or users", LastModified = "2009/11/13", Value = "Add roles or users")]
    public string AddRolesOrUsers => this[nameof (AddRolesOrUsers)];

    /// <summary>phrase: Click on a user to select it</summary>
    [ResourceEntry("ClickOnUserToSelectIt", Description = "phrase: Click on a user to select it", LastModified = "2009/11/13", Value = "Click on a user to select it")]
    public string ClickOnUserToSelectIt => this[nameof (ClickOnUserToSelectIt)];

    /// <summary>phrase: Click on a role to select it</summary>
    [ResourceEntry("ClickOnRoleToSelectIt", Description = "phrase: Click on a role to select it", LastModified = "2009/11/13", Value = "Click on a role to select it")]
    public string ClickOnRoleToSelectIt => this[nameof (ClickOnRoleToSelectIt)];

    /// <summary>phrase: Explicitly denied:</summary>
    [ResourceEntry("ExplicitlyDenied", Description = "phrase: Explicitly denied:", LastModified = "2009/11/13", Value = "Explicitly denied")]
    public string ExplicitlyDenied => this[nameof (ExplicitlyDenied)];

    /// <summary>phrase: All users</summary>
    [ResourceEntry("AllUsers", Description = "phrase: All users", LastModified = "2009/11/16", Value = "All users")]
    public string AllUsers => this[nameof (AllUsers)];

    /// <summary>phrase: All Users {0}</summary>
    [ResourceEntry("AllUsersCount", Description = "phrase: All Users {0}", LastModified = "2010/04/28", Value = "All Users {0}")]
    public string AllUsersCount => this[nameof (AllUsersCount)];

    /// <summary>phrase: Users in role {0} {1}</summary>
    [ResourceEntry("UsersInRoleNameCount", Description = "phrase: Users in role {0} {1}", LastModified = "2010/04/28", Value = "Assigned {0} {1}")]
    public string UsersInRoleNameCount => this[nameof (UsersInRoleNameCount)];

    /// <summary>phrase: User with this username already exists</summary>
    [ResourceEntry("UserWithThisUsernameExists", Description = "phrase: User with this email already exists", LastModified = "2016/12/20", Value = "User with this email already exists")]
    public string UserWithThisUsernameExists => this[nameof (UserWithThisUsernameExists)];

    /// <summary>phrase: Administrators role cannot be deleted</summary>
    [ResourceEntry("AdministratorsRoleCannotBeDeleted", Description = "phrase: System role cannot be deleted", LastModified = "2009/11/19", Value = "The selected role is a role used by the system and cannot be deleted.")]
    public string AdministratorsRoleCannotBeDeleted => this[nameof (AdministratorsRoleCannotBeDeleted)];

    /// <summary>
    /// phrase: The role name '{0}' is reserved for use by the system.
    /// </summary>
    [ResourceEntry("RoleNameIsReservedByTheSystem", Description = "phrase: The role name '{0}' is reserved for use by the system.", LastModified = "2010/04/15", Value = "The role name '{0}' is reserved for use by the system.")]
    public string RoleNameIsReservedByTheSystem => this[nameof (RoleNameIsReservedByTheSystem)];

    /// <summary>
    /// phrase: User is last administrator and cannot be deleted
    /// </summary>
    [ResourceEntry("UserIsLastAdministrator", Description = "phrase: User is last administrator and cannot be deleted.", LastModified = "2009/11/19", Value = "User is last administrator and cannot be deleted.")]
    public string UserIsLastAdministrator => this[nameof (UserIsLastAdministrator)];

    /// <summary>phrase: Creation date</summary>
    [ResourceEntry("CreationDate", Description = "phrase: Creation date", LastModified = "2009/11/19", Value = "Creation date")]
    public string CreationDate => this[nameof (CreationDate)];

    /// <summary>phrase: Selected users</summary>
    [ResourceEntry("SelectedUsers", Description = "phrase: Selected users", LastModified = "2009/11/20", Value = "Selected users")]
    public string SelectedUsers => this[nameof (SelectedUsers)];

    /// <summary>word: Assigned</summary>
    [ResourceEntry("Assigned", Description = "word: Assigned", LastModified = "2009/11/20", Value = "Assigned")]
    public string Assigned => this[nameof (Assigned)];

    /// <summary>Phrase: Global Permissions Title</summary>
    [ResourceEntry("GlobalPermissionsTitle", Description = "Global Permissions Title", LastModified = "2009/11/19", Value = "Permissions")]
    public string GlobalPermissionsTitle => this[nameof (GlobalPermissionsTitle)];

    /// <summary>Phrase: This user can access site backend</summary>
    [ResourceEntry("ThisUserCanAccessSiteBackend", Description = "This user can access site backend", LastModified = "2016/12/06", Value = "This user can access site backend")]
    public string ThisUserCanAccessSiteBackend => this[nameof (ThisUserCanAccessSiteBackend)];

    /// <summary>
    /// Phrase: This user can access site backend of all sites
    /// </summary>
    [ResourceEntry("ThisUserCanAccessSiteBackendOfAllSites", Description = "This user can access site backend of all sites", LastModified = "2016/12/06", Value = "This user can access site backend of all sites")]
    public string ThisUserCanAccessSiteBackendOfAllSites => this[nameof (ThisUserCanAccessSiteBackendOfAllSites)];

    /// <summary>Phrase: Manage content or settings</summary>
    [ResourceEntry("InOrderToManageContentOrSettings", Description = "Manage content or settings", LastModified = "2016/12/06", Value = "Manage content or settings")]
    public string InOrderToManageContentOrSettings => this[nameof (InOrderToManageContentOrSettings)];

    /// <summary>
    /// phrase: This is the currently logged user and cannot be deleted.
    /// </summary>
    [ResourceEntry("UserIsCurrentlyLogged", Description = "phrase: This is the currently logged user and cannot be deleted.", LastModified = "2011/05/16", Value = "This is the currently logged user and cannot be deleted.")]
    public string UserIsCurrentlyLogged => this[nameof (UserIsCurrentlyLogged)];

    /// <summary>
    /// phrase: Missing configuration for the requesting relying party "{0}"
    /// </summary>
    [ResourceEntry("MissingConfigurationForRelyingParty", Description = "Missing configuration for the requesting relying party \"{0}\"", LastModified = "2015/09/15", Value = "Missing configuration for the requesting relying party \"{0}\"")]
    public string MissingConfigurationForRelyingParty => this[nameof (MissingConfigurationForRelyingParty)];

    /// <summary>
    /// Translated message, similar to "Missing current principal."
    /// </summary>
    /// <value>Error message shown when checking for permissions and the current user couldn't be retrieved.</value>
    [ResourceEntry("MissingCurrentPrincipal", Description = "Error message shown when checking for permissions and the current user couldn't be retrieved.", LastModified = "2010/06/09", Value = "Missing current principal.")]
    public string MissingCurrentPrincipal => this[nameof (MissingCurrentPrincipal)];

    /// <summary>
    /// Translated message, similar to "{0} with ID {1} does not support permission set {2}. It supports {3}."
    /// </summary>
    /// <value>Error message shown when we check whether a secured object is granted some action of a permission set that is not supported.</value>
    [ResourceEntry("SecuredObjectWithIdDoesNotSupportPermissionSet", Description = "Error message shown when we check whether a secured object is granted some action of a permission set that is not supported.", LastModified = "2010/06/09", Value = "{0} with ID {1} does not support permission set {2}. It supports {3}.")]
    public string SecuredObjectWithIdDoesNotSupportPermissionSet => this[nameof (SecuredObjectWithIdDoesNotSupportPermissionSet)];

    /// <summary>
    /// Translated message, similar to "You are not authorized to '{0}' ('{1}')."
    /// </summary>
    /// <value>
    /// More detailed error message displayed when a permission is not granted.
    /// </value>
    /// <remarks>
    /// {0} is a placeholder for a security action title.
    /// {1} is a placeholder for a permission set title.
    /// </remarks>
    [ResourceEntry("NotAuthorizedToDoSetAction", Description = "More detailed error message displayed when a permission is not granted.", LastModified = "2010/05/26", Value = "You are not authorized to '{0}' ('{1}').")]
    public string NotAuthorizedToDoSetAction => this[nameof (NotAuthorizedToDoSetAction)];

    /// <summary>Message: You cannot unassign all administrators.</summary>
    [ResourceEntry("CantUnassignAllAdministrators", Description = "Message: You cannot unassign all administrators.", LastModified = "2009/12/10", Value = "You cannot unassign all administrators.")]
    public string CantUnassignAllAdministrators => this[nameof (CantUnassignAllAdministrators)];

    /// <summary>
    /// Message: You cannot remove the Default user group. This is the only site that has it selected and it must be selected in at least one site.
    /// </summary>
    [ResourceEntry("CantUnassignDefaultUsersProviderFromAllSites", Description = "Message: You cannot remove the Default user group. This is the only site that has it selected and it must be selected in at least one site.", LastModified = "2021/01/01", Value = "You cannot remove the Default user group. This is the only site that has it selected and it must be selected in at least one site.")]
    public string CantUnassignDefaultUsersProviderFromAllSites => this[nameof (CantUnassignDefaultUsersProviderFromAllSites)];

    /// <summary>Could not retrieve a security root for {0}.</summary>
    [ResourceEntry("NoSecurityRoot", Description = "Message: Could not retrieve a security root for {0}.", LastModified = "2010/04/06", Value = "Could not retrieve a security root for {0}.")]
    public string NoSecurityRoot => this[nameof (NoSecurityRoot)];

    /// <summary>
    /// Edit - appears as title of the dialog when the user tries to edit an object without sufficient permissions
    /// </summary>
    [ResourceEntry("NoEditPermissionsViewDialogTitle", Description = "Title: Edit", LastModified = "2010/07/08", Value = "Edit {0}")]
    public string NoEditPermissionsViewDialogTitle => this[nameof (NoEditPermissionsViewDialogTitle)];

    /// <summary>
    /// No Permissions To Set As Homepage - appears on click in the action menu of pages
    /// </summary>
    [ResourceEntry("NoPermissionsToSetAsHomepage", Description = "Title: No permissions", LastModified = "2011/02/22", Value = "You do not have enough permissions to set this page as a homepage")]
    public string NoPermissionsToSetAsHomepage => this[nameof (NoPermissionsToSetAsHomepage)];

    /// <summary>
    /// You do not have the permissions to perform this action. Contact your system administrator for assistance. Click View to open the item in read-only mode. Press Cancel to exit.
    /// </summary>
    [ResourceEntry("NoEditPermissionsPreviewOnlyConfirmation", Description = "Message: You do not have the permissions to perform this action. Contact your system administrator for assistance. Click View to open the item in read-only mode. Press Cancel to exit.", LastModified = "2010/07/08", Value = "<p>You do not have the permissions to perform this action.<br />Contact your system administrator for assistance.<br /><br />Click <em>View</em> to open the item in read-only mode. Press <em>Cancel</em> to exit.</p>")]
    public string NoEditPermissionsPreviewOnlyConfirmation => this[nameof (NoEditPermissionsPreviewOnlyConfirmation)];

    /// <summary>
    /// You do not have the permissions to perform this action. Contact your system administrator for assistance.
    /// </summary>
    [ResourceEntry("NoEditPermissionsPreviewOnlyConfirmationNoViewOption", Description = "Message: You do not have the permissions to perform this action. Contact your system administrator for assistance.", LastModified = "2010/07/09", Value = "<p>You do not have the permissions to perform this action.<br />Contact your system administrator for assistance.</p>")]
    public string NoEditPermissionsPreviewOnlyConfirmationNoViewOption => this[nameof (NoEditPermissionsPreviewOnlyConfirmationNoViewOption)];

    /// <summary>View</summary>
    [ResourceEntry("NoEditPermissionsViewObject", Description = "Button: View object", LastModified = "2010/07/08", Value = "View")]
    public string NoEditPermissionsViewObject => this[nameof (NoEditPermissionsViewObject)];

    /// <summary>
    /// Message: You are not authorized to create widgets and layout elements on this page
    /// </summary>
    [ResourceEntry("NotAuthorizedToManageControlsOnThisPage", Description = "Message: You are not authorized to create widgets and layout elements on this page", LastModified = "2010/08/05", Value = "You are not authorized to create widgets and layout elements on this page")]
    public string NotAuthorizedToManageControlsOnThisPage => this[nameof (NotAuthorizedToManageControlsOnThisPage)];

    /// <summary>
    /// Message: You are not authorized to create widgets and layout elements on this template
    /// </summary>
    [ResourceEntry("NotAuthorizedToManageControlsOnThisTemplate", Description = "Message: You are not authorized to create widgets and layout elements on this template", LastModified = "2010/08/05", Value = "You are not authorized to create widgets and layout elements on this template")]
    public string NotAuthorizedToManageControlsOnThisTemplate => this[nameof (NotAuthorizedToManageControlsOnThisTemplate)];

    /// <summary>
    /// Message: You are not authorized to create widgets and layout elements on this form
    /// </summary>
    [ResourceEntry("NotAuthorizedToManageControlsOnThisForm", Description = "Message: You are not authorized to create widgets and layout elements on this form", LastModified = "2010/08/05", Value = "You are not authorized to create widgets and layout elements on this form")]
    public string NotAuthorizedToManageControlsOnThisForm => this[nameof (NotAuthorizedToManageControlsOnThisForm)];

    /// <summary>
    /// Translated message, similar to "{0} has been logged out."
    /// </summary>
    /// <value>Message: '{0} has been logged out.', where {0} is placeholder for user name</value>
    [ResourceEntry("UserNameHasBeenLoggedOut", Description = "Message: '{0} has been logged out.', where {0} is placeholder for user name", LastModified = "2010/10/12", Value = "{0} has been logged out.")]
    public string UserNameHasBeenLoggedOut => this[nameof (UserNameHasBeenLoggedOut)];

    /// <summary>
    /// Error message: An application role with the name {0} already exists in the system.
    /// </summary>
    /// <value>Error message: An application role with the name {0} already exists in the system.</value>
    [ResourceEntry("ApplicationRoleAlreadyExistsWithThisName", Description = "Error message: An application role with the name {0} already exists in the system.", LastModified = "2011/02/23", Value = "An application role with the name {0} already exists in the system.")]
    public string ApplicationRoleAlreadyExistsWithThisName => this[nameof (ApplicationRoleAlreadyExistsWithThisName)];

    /// <summary>
    /// Message shown when user does not have rights to edit the item"
    /// </summary>
    /// <value>You can only view this item. Contact your administrator for permissions to edit the item.</value>
    [ResourceEntry("NoPermissionsToEditItem", Description = "Message shown when user does not have rights to edit the item", LastModified = "2018/11/23", Value = "You can only view this item. Contact your administrator for permissions to edit the item.")]
    public string NoPermissionsToEditItem => this[nameof (NoPermissionsToEditItem)];

    /// <summary>
    /// FAQ: Information about database setup at project Startup
    /// </summary>
    [ResourceEntry("DatabaseFAQ", Description = "DatabaseFAQ", LastModified = "2010/07/26", Value = "<h4>Do I need to create the database beforehand?</h4><p>No. If you select <em>Microsoft SQL Server Express</em>, the database will be added automatically. If you select <em>Microsoft SQL Server</em>, the database user should have permissions to create a database or  you would need to create the database manually.</p><h4>What if I check <em>Windows Authentication for SQL Server connection</em>?</h4><p>You need to make sure that the account that runs the ASPNET process has the appropriate permissions to the database.</p>")]
    public string DatabaseFAQ => this[nameof (DatabaseFAQ)];

    /// <summary>Message: Invalid UserName</summary>
    /// <value>The username you have entered is not valid.</value>
    [ResourceEntry("InvalidUserName", Description = "phrase: The username you have entered is not valid.", LastModified = "2009/11/24", Value = "The username you have entered is not valid.")]
    public string InvalidUserName => this[nameof (InvalidUserName)];

    /// <summary>
    /// phrase: The password is too short. It should be at least {0} character(s) long.
    /// </summary>
    [ResourceEntry("PasswordTooShort", Description = "phrase: The password is too short. It should be at least {0} character(s) long.", LastModified = "2012/01/05", Value = "The password is too short. It should be at least {0} character(s) long.")]
    public string PasswordTooShort => this[nameof (PasswordTooShort)];

    /// <summary>
    /// phrase: Invalid password. It should contain at least {0} alpha-numeric character(s).
    /// </summary>
    [ResourceEntry("PasswordNeedsMoreAlphaNumericCharacters", Description = "phrase: Invalid password. It should contain at least {0} alpha-numeric character(s).", LastModified = "2012/01/05", Value = "Invalid password. It should contain at least {0} alpha-numeric character(s).")]
    public string PasswordNeedsMoreAlphaNumericCharacters => this[nameof (PasswordNeedsMoreAlphaNumericCharacters)];

    /// <summary>
    /// phrase: Password too week. It does not match the strength regular expression.
    /// </summary>
    [ResourceEntry("PasswordTooWeek", Description = "phrase: Password too week. It does not match the strength regular expression.", LastModified = "2010/09/21", Value = "Password too week. It does not match the strength regular expression.")]
    public string PasswordTooWeek => this[nameof (PasswordTooWeek)];

    /// <summary>Message: Invalid Question</summary>
    /// <value>The question you have entered is not valid.</value>
    [ResourceEntry("InvalidQuestion", Description = "phrase: The question you have entered is not valid.", LastModified = "2009/11/24", Value = "The question you have entered is not valid.")]
    public string InvalidQuestion => this[nameof (InvalidQuestion)];

    /// <summary>Message: Invalid Answer</summary>
    /// <value>The answer you have entered is not valid.</value>
    [ResourceEntry("InvalidAnswer", Description = "phrase: The answer you have entered is not valid.", LastModified = "2009/11/24", Value = "The answer you have entered is not valid.")]
    public string InvalidAnswer => this[nameof (InvalidAnswer)];

    /// <summary>Message: Invalid Email</summary>
    /// <value>The email you have entered is not valid.</value>
    [ResourceEntry("InvalidEmail", Description = "phrase: The email you have entered is not valid.", LastModified = "2009/11/24", Value = "The email you have entered is not valid.")]
    public string InvalidEmail => this[nameof (InvalidEmail)];

    /// <summary>Message: Duplicate Email</summary>
    /// <value>User with this email already exists.</value>
    [ResourceEntry("DuplicateEmail", Description = "phrase: User with this email already exists.", LastModified = "2009/11/24", Value = "User with this email already exists.")]
    public string DuplicateEmail => this[nameof (DuplicateEmail)];

    /// <summary>Message: User Rejected</summary>
    /// <value>The user was rejected.</value>
    [ResourceEntry("UserRejected", Description = "prhase: The user was rejected.", LastModified = "2009/11/24", Value = "The user was rejected.")]
    public string UserRejected => this[nameof (UserRejected)];

    /// <summary>Message: Invalid Provider UserKey</summary>
    /// <value>Invalid provider user key.</value>
    [ResourceEntry("InvalidProviderUserKey", Description = "phrase: Invalid provider user key.", LastModified = "2009/11/24", Value = "Invalid provider user key.")]
    public string InvalidProviderUserKey => this[nameof (InvalidProviderUserKey)];

    /// <summary>Message: Duplicate Provider UserKey</summary>
    /// <value>Duplicate provider user key.</value>
    [ResourceEntry("DuplicateProviderUserKey", Description = "phrase: Duplicate provider user key.", LastModified = "2009/11/24", Value = "Duplicate Provider UserKey")]
    public string DuplicateProviderUserKey => this[nameof (DuplicateProviderUserKey)];

    /// <summary>Message: Provider Error</summary>
    /// <value>The membership provider has encountered an internal error.</value>
    [ResourceEntry("ProviderError", Description = "phrase: The membership provider has encountered an internal error.", LastModified = "2009/11/24", Value = "The membership provider has encountered an internal error.")]
    public string ProviderError => this[nameof (ProviderError)];

    /// <summary>Message: Role exists on provider</summary>
    /// <value>The role with the name '{0}' already exists on the '{1}' role provider. Please choose a different role name.</value>
    [ResourceEntry("RoleExistsOnProvider", Description = "Message: Role with such name already exists on the role provider", LastModified = "2009/12/08", Value = "The role with the name '{0}' already exists on the '{1}' role provider. Please choose a different role name.")]
    public string RoleExistsOnProvider => this[nameof (RoleExistsOnProvider)];

    /// <summary>
    /// A translated message for something like 'You are about to delete your own user account. This change is irreversible and you will be logged out.'
    /// </summary>
    /// <value>Warning that is show to the user to confirm that they want to delete their own account.</value>
    [ResourceEntry("DeleteCurrentUserWarning", Description = "Warning that is show to the user to confirm that they want to delete their own account.", LastModified = "2010/03/25", Value = "You are about to delete your own user account. This change is irreversible and you will be logged out.")]
    public string DeleteCurrentUserWarning => this[nameof (DeleteCurrentUserWarning)];

    /// <summary>
    /// A translated message for something like 'Role was succesfully saved.'
    /// </summary>
    /// <value>Message that notifies that the new role was succesfully saved.</value>
    [ResourceEntry("RoleWasSuccesfullySaved", Description = "Message that notifies that the new role was succesfully saved.", LastModified = "2012/01/05", Value = "Role was successfully saved.")]
    public string RoleWasSuccesfullySaved => this[nameof (RoleWasSuccesfullySaved)];

    /// <summary>
    /// A translated message for something like 'Role was succesfully deleted.'
    /// </summary>
    /// <value>Message that notifies that a role was succesfully deleted.</value>
    [ResourceEntry("RoleWasSuccesfullyDeleted", Description = "Message that notifies that a role was succesfully deleted.", LastModified = "2012/01/05", Value = "Role was successfully deleted.")]
    public string RoleWasSuccesfullyDeleted => this[nameof (RoleWasSuccesfullyDeleted)];

    /// <summary>
    /// A translated message for something like 'Are you sure you want to delete this role (users will be removed from the role automatically)?'
    /// </summary>
    /// <value>Asks the user for confirmation before deleting a role.</value>
    [ResourceEntry("RoleDeletionConfirmation", Description = "Asks the user for confirmation before deleting a role.", LastModified = "2010/03/25", Value = "Are you sure you want to delete this role (users will be removed from the role automatically)?")]
    public string RoleDeletionConfirmation => this[nameof (RoleDeletionConfirmation)];

    /// <summary>
    /// A translated message for something like 'You can not create a role with no name.'
    /// </summary>
    /// <value>Notifies the user when they try to create a role with no name.</value>
    [ResourceEntry("CannotCreateRoleWithoutName", Description = "Notifies the user when they try to create a role with no name.", LastModified = "2010/03/25", Value = "You can not create a role with no name.")]
    public string CannotCreateRoleWithoutName => this[nameof (CannotCreateRoleWithoutName)];

    /// <summary>
    /// A translated message for something like "There can be only one 'Administrators' role"
    /// </summary>
    /// <value>Warns the user whey they try to create a new role named "Administrators".</value>
    [ResourceEntry("ThereCanBeOnlyOneAdministratorsRole", Description = "Warns the user whey they try to create a new role named 'Administrators'", LastModified = "2010/03/26", Value = "There can be only one 'Administrators' role.")]
    public string ThereCanBeOnlyOneAdministratorsRole => this[nameof (ThereCanBeOnlyOneAdministratorsRole)];

    /// <summary>
    /// Translated message, similar to "SMTP timeout must be non-negative."
    /// </summary>
    /// <value>Error message displayed when the user tries to enter a value for SMTP timeout that is less than zero.</value>
    [ResourceEntry("SmtpTimeoutMustBeNonNegative", Description = "Error message displayed when the user tries to enter a value for SMTP timeout that is less than zero.", LastModified = "2010/03/29", Value = "SMTP timeout must be non-negative.")]
    public string SmtpTimeoutMustBeNonNegative => this[nameof (SmtpTimeoutMustBeNonNegative)];

    /// <summary>
    /// Translated message, similar to "SMTP port must be non-negative."
    /// </summary>
    /// <value>Error message displayed when the user tries to enter a value for SMTP port that is less than zero.</value>
    [ResourceEntry("SmtpPortMustBeNonNegative", Description = "Error message displayed when the user tries to enter a value for SMTP port that is less than zero.", LastModified = "2010/03/29", Value = "SMTP port must be non-negative.")]
    public string SmtpPortMustBeNonNegative => this[nameof (SmtpPortMustBeNonNegative)];

    /// <summary>
    /// Translated message, similar to "You can't delete the current user."
    /// </summary>
    /// <value>Error message when trying to delete the current user.</value>
    [ResourceEntry("YouCantDeleteTheCurrentUser", Description = "Error message when trying to delete the current user.", LastModified = "2011/04/22", Value = "You can't delete the current user.")]
    public string YouCantDeleteTheCurrentUser => this[nameof (YouCantDeleteTheCurrentUser)];

    [ResourceEntry("RolesPermissionsMessage", Description = "", LastModified = "2016/04/27", Value = "<p><strong>Designers</strong> and <strong>Authors</strong> are system roles not used in this instance. You don't need to change their permissions.</p><br><p><strong>Administrator</strong> and <strong>Editor</strong> are system roles managed in Account Administration.</p>")]
    public string RolesPermissionsMessage => this[nameof (RolesPermissionsMessage)];

    /// <summary>Gets the used also in.</summary>
    /// <value>The used also in.</value>
    [ResourceEntry("UsedIn", Description = "Phrase: Used also in", LastModified = "2016/12/06", Value = "Used also in")]
    public string UsedIn => this[nameof (UsedIn)];

    /// <summary>Phrase: more sites</summary>
    [ResourceEntry("Sites", Description = "Phrase: more sites", LastModified = "2016/12/06", Value = "more sites")]
    public string Sites => this[nameof (Sites)];

    /// <summary>Phrase: more site</summary>
    [ResourceEntry("Site", Description = "Phrase: more site", LastModified = "2012/12/06", Value = "more site")]
    public string Site => this[nameof (Site)];

    /// <summary>Gets External Link: Managing permissions</summary>
    [ResourceEntry("ExternalLinkManagingPermissions", Description = "External Link: Managing permissions", LastModified = "2018/10/15", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-permissions")]
    public string ExternalLinkManagingPermissions => this[nameof (ExternalLinkManagingPermissions)];
  }
}
