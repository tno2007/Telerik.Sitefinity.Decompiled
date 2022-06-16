// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.UserFiles.UserFilesResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions.CodeQuality;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Libraries.UserFiles
{
  /// <summary>Represents string resources for User files.</summary>
  [ObjectInfo("UserFilesResources", ResourceClassId = "UserFilesResources")]
  [ApprovedBy("Boyan Rabchev", "2012/02/10")]
  public class UserFilesResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.UserFiles.UserFilesResources" /> class.
    /// </summary>
    public UserFilesResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.UserFiles.UserFilesResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public UserFilesResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>User files module labels</summary>
    [ResourceEntry("UserFilesResourcesTitle", Description = "The title of this class.", LastModified = "2012/02/09", Value = "User files module labels")]
    public string UserFilesResourcesTitle => this[nameof (UserFilesResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for synchronization module interface.
    /// </summary>
    [ResourceEntry("UserFilesResourcesDescription", Description = "Contains localizable resources for user files.", LastModified = "2012/02/09", Value = "User files module labels")]
    public string UserFilesResourcesDescription => this[nameof (UserFilesResourcesDescription)];

    /// <summary>User Files</summary>
    [ResourceEntry("UserFiles", Description = "Phrase: User Files", LastModified = "2012/01/31", Value = "User files")]
    public string UserFiles => this[nameof (UserFiles)];

    /// <summary>Folder</summary>
    [ResourceEntry("FolderResourceTitle", Description = "Word: Folder", LastModified = "2012/01/31", Value = "Folder")]
    public string FolderResourceTitle => this[nameof (FolderResourceTitle)];

    /// <summary>User files node title</summary>
    [ResourceEntry("UserFilesNodeTitle", Description = "User files", LastModified = "2012/01/31", Value = "User files")]
    public string UserFilesNodeTitle => this[nameof (UserFilesNodeTitle)];

    /// <summary>User files module title</summary>
    [ResourceEntry("UserFilesModuleTitle", Description = "User files", LastModified = "2012/01/31", Value = "User files")]
    public string UserFilesModuleTitle => this[nameof (UserFilesModuleTitle)];

    /// <summary>User files module description</summary>
    [ResourceEntry("UserFilesModuleDescription", Description = "User files", LastModified = "2012/01/31", Value = "User files")]
    public string UserFilesModuleDescription => this[nameof (UserFilesModuleDescription)];

    /// <summary>System folder Url Name</summary>
    [ResourceEntry("UserFileModuleUrlName", Description = "User files", LastModified = "2012/01/31", Value = "User-files")]
    public string UserFileModuleUrlName => this[nameof (UserFileModuleUrlName)];

    /// <summary>Items word shown on user files list</summary>
    [ResourceEntry("Items", Description = "items", LastModified = "2012/01/31", Value = "items")]
    public string Items => this[nameof (Items)];

    /// <summary>Edit a System folder</summary>
    [ResourceEntry("EditUserFile", Description = "Edit a System folder", LastModified = "2012/01/31", Value = "Edit a User file")]
    public string EditUserFile => this[nameof (EditUserFile)];

    /// <summary>Phrase: System folder name</summary>
    [ResourceEntry("UserFileName", Description = "The 'User file name' label.", LastModified = "2012/01/31", Value = "User file name")]
    public string UserFileName => this[nameof (UserFileName)];

    /// <summary>Phrase: Example: Downloadable goods</summary>
    [ResourceEntry("UserFileNameExample", Description = "The example on how to fill user file name field", LastModified = "2012/01/31", Value = "Example: Downloadable goods")]
    public string UserFileNameExample => this[nameof (UserFileNameExample)];

    /// <summary>Phrase: System folder name cannot be empty</summary>
    [ResourceEntry("UserFileNameCannotBeEmpty", Description = "The message shown when the user file name is empty.", LastModified = "2012/01/31", Value = "System folder name cannot be empty")]
    public string UserFileNameCannotBeEmpty => this[nameof (UserFileNameCannotBeEmpty)];

    /// <summary>Word: Description</summary>
    [ResourceEntry("Description", Description = "The 'Description' label.", LastModified = "2012/01/31", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Phrase: The description of this user file</summary>
    [ResourceEntry("UserFileDescription", Description = "The description of the Description field in the user file form.", LastModified = "2012/01/31", Value = "The description of this user file.")]
    public string UserFileDescription => this[nameof (UserFileDescription)];

    /// <summary>Phrase: Back to all user files</summary>
    [ResourceEntry("BackToAllUserFiles", Description = "The label for link to return to all user files.", LastModified = "2012/01/31", Value = "Back to all User files")]
    public string BackToAllUserFiles => this[nameof (BackToAllUserFiles)];

    /// <summary>Phrase: Upload files</summary>
    [ResourceEntry("UploadFilesButton", Description = "Upload files", LastModified = "2012/02/02", Value = "Upload files")]
    public string UploadFilesButton => this[nameof (UploadFilesButton)];

    /// <summary>Phrase: Upload files</summary>
    [ResourceEntry("UploadFiles", Description = "Upload files", LastModified = "2012/02/02", Value = "Upload files")]
    public string UploadFiles => this[nameof (UploadFiles)];

    /// <summary>Phrase: Manage User files</summary>
    [ResourceEntry("ManageUserFileLibraries", Description = "The text of 'Manage User files' link.", LastModified = "2012/02/02", Value = "Manage User files")]
    public string ManageUserFileLibraries => this[nameof (ManageUserFileLibraries)];

    /// <summary>Phrase: Filter User files</summary>
    [ResourceEntry("FilterUserFiles", Description = "phrase: Filter User files", LastModified = "2012/02/02", Value = "Filter User files")]
    public string FilterUserFiles => this[nameof (FilterUserFiles)];

    /// <summary>Phrase: All libraries</summary>
    [ResourceEntry("AllLibraries", Description = "phrase: All libraries", LastModified = "2012/02/02", Value = "All libraries")]
    public string AllLibraries => this[nameof (AllLibraries)];

    /// <summary>Phrase: My libraries</summary>
    [ResourceEntry("MyLibraries", Description = "phrase: My libraries", LastModified = "2012/02/02", Value = "My libraries")]
    public string MyLibraries => this[nameof (MyLibraries)];

    /// <summary>Phrase: Back to user files</summary>
    [ResourceEntry("BackToAllLibraries", Description = "phrase: Back to user files", LastModified = "2012/02/02", Value = "Back to user files")]
    public string BackToAllLibraries => this[nameof (BackToAllLibraries)];

    /// <summary>phrase: Permissions for User files</summary>
    [ResourceEntry("PermissionsForUserFiles", Description = "The 'Permissions for User files' label in the permissions dialog.", LastModified = "2012/02/02", Value = "Permissions for User files")]
    public string PermissionsForUserFiles => this[nameof (PermissionsForUserFiles)];

    /// <summary>Messsage: UserFileDocumentsTitle</summary>
    [ResourceEntry("UserFilesDocumentsTitle", Description = "Title of the user files at Documents page.", LastModified = "2012/02/02", Value = "User files Documents")]
    public string UserFilesDocumentsTitle => this[nameof (UserFilesDocumentsTitle)];

    /// <summary>Messsage: UserFileDocumentsHtmlTitle</summary>
    [ResourceEntry("UserFilesDocumentsHtmlTitle", Description = "Html Title of the UserFilesDocuments page.", LastModified = "2012/02/02", Value = "User files Documents")]
    public string UserFilesDocumentsHtmlTitle => this[nameof (UserFilesDocumentsHtmlTitle)];

    /// <summary>Messsage: User files page for selected library.</summary>
    [ResourceEntry("UserFilesDocumentsUrlName", Description = "Documents page for selected user file library.", LastModified = "2012/02/02", Value = "UserFilesDocuments")]
    public string UserFilesDocumentsUrlName => this[nameof (UserFilesDocumentsUrlName)];

    /// <summary>Messsage: UserFilesDocuments Description</summary>
    [ResourceEntry("UserFilesDocumentsDescription", Description = "Description of the UserFileDocuments page", LastModified = "2012/02/02", Value = "User files Documents Description")]
    public string UserFilesDocumentsDescription => this[nameof (UserFilesDocumentsDescription)];

    /// <summary>Message: Back to user files</summary>
    /// <value>The back to all user files</value>
    [ResourceEntry("BackToItems", Description = "The back to all user files button", LastModified = "2012/02/02", Value = "Back to User files")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>phrase: {0}</summary>
    [ResourceEntry("UserFileDocumentsTitleFormat", Description = "UserFile Documents Title Format", LastModified = "2012/02/06", Value = "{0}")]
    public string UserFileDocumentsTitleFormat => this[nameof (UserFileDocumentsTitleFormat)];

    /// <summary>Messsage: Manage User files</summary>
    [ResourceEntry("ManageUserFiles", Description = "Manage User files", LastModified = "2012/02/06", Value = "Manage User files")]
    public string ManageUserFiles => this[nameof (ManageUserFiles)];

    /// <summary>phrase: Filter items in this folder</summary>
    [ResourceEntry("FilterDocumentsInThisFolder", Description = "Filter items in this folder", LastModified = "2012/02/06", Value = "Filter items in this folder")]
    public string FilterDocumentsInThisFolder => this[nameof (FilterDocumentsInThisFolder)];

    /// <summary>phrase: Settings for User files</summary>
    [ResourceEntry("SettingsForUserFiles", Description = "Settings for User files", LastModified = "2012/02/06", Value = "Settings for User files")]
    public string SettingsForUserFiles => this[nameof (SettingsForUserFiles)];

    /// <summary>phrase: Create this Library</summary>
    [ResourceEntry("CreateThisLibrary", Description = "The text of 'Create this library', for user files", LastModified = "2012/02/15", Value = "Create this library")]
    public string CreateThisLibrary => this[nameof (CreateThisLibrary)];

    /// <summary>phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Saves the edited item changes", LastModified = "2012/02/15", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>
    /// phrase: You cannot delete this user file library as it is a default library
    /// </summary>
    [ResourceEntry("CannotDeleteDefaultUserFileLibraries", Description = "You cannot delete this user file library as it is a default library", LastModified = "2012/02/15", Value = "You cannot delete this user file library as it is a default library")]
    public string CannotDeleteDefaultUserFileLibraries => this[nameof (CannotDeleteDefaultUserFileLibraries)];
  }
}
