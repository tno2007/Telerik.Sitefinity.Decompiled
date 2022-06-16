// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Documents.DocumentsResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Libraries.Documents
{
  /// <summary>
  /// Represents string resources for Documents module user interface.
  /// </summary>
  [ObjectInfo("DocumentsResources", ResourceClassId = "DocumentsResources")]
  public class DocumentsResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentsResources" /> class.
    /// </summary>
    public DocumentsResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Documents.DocumentsResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public DocumentsResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Messsage: type name Document class</summary>
    [ResourceEntry("DocumentsPluralTypeName", Description = "Documents type name", LastModified = "2010/01/29", Value = "Documents")]
    public string DocumentsPluralTypeName => this[nameof (DocumentsPluralTypeName)];

    /// <summary>Messsage: type name Document class</summary>
    [ResourceEntry("DocumentsSingleTypeName", Description = "Documents type name", LastModified = "2010/01/29", Value = "Document")]
    public string DocumentsSingleTypeName => this[nameof (DocumentsSingleTypeName)];

    /// <summary>Messsage: type name Document class</summary>
    [ResourceEntry("DocumentLibrariesPluralTypeName", Description = "Documents type name", LastModified = "2010/01/29", Value = "Document Libraries")]
    public string DocumentLibrariesPluralTypeName => this[nameof (DocumentLibrariesPluralTypeName)];

    /// <summary>Messsage: type name DocumentLibrary class</summary>
    [ResourceEntry("DocumentLibrarySingleTypeName", Description = "Documents type name", LastModified = "2010/01/29", Value = "Document Library")]
    public string DocumentLibrarySingleTypeName => this[nameof (DocumentLibrarySingleTypeName)];

    /// <summary>Documents</summary>
    [ResourceEntry("DocumentsResourcesTitle", Description = "The title of this class.", LastModified = "2010/05/12", Value = "Documents & Files")]
    public string DocumentsResourcesTitle => this[nameof (DocumentsResourcesTitle)];

    /// <summary>Documents</summary>
    [ResourceEntry("DocumentsResourcesTitlePlural", Description = "The title of this class in plural.", LastModified = "2010/05/12", Value = "Documents & Files")]
    public string DocumentsResourcesTitlePlural => this[nameof (DocumentsResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Documents user interface.
    /// </summary>
    [ResourceEntry("DocumentsResourcesDescription", Description = "The description of this class.", LastModified = "2010/05/12", Value = "Contains localizable resources for Documents & Files user interface.")]
    public string DocumentsResourcesDescription => this[nameof (DocumentsResourcesDescription)];

    /// <summary>More actions</summary>
    [ResourceEntry("MoreActions", Description = "The text of the more action menu in grid toolbar.", LastModified = "2010/10/25", Value = "More actions")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>Publish</summary>
    [ResourceEntry("Publish", Description = "Label of the publish action.", LastModified = "2010/07/29", Value = "Publish")]
    public string Publish => this[nameof (Publish)];

    /// <summary>word: Unpublish</summary>
    [ResourceEntry("Unpublish", Description = "word: Unpublish", LastModified = "2010/08/03", Value = "Unpublish")]
    public string Unpublish => this[nameof (Unpublish)];

    /// <summary>Documents</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Documents module.", LastModified = "2010/05/13", Value = "Documents & Files")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>word: Document</summary>
    [ResourceEntry("Document", Description = "The 'Documents' string in singular.", LastModified = "2010/05/13", Value = "Document or other file")]
    public string Document => this[nameof (Document)];

    /// <summary>Messsage: Manage Documents</summary>
    [ResourceEntry("ManageDocuments", Description = "Manage Documents", LastModified = "2010/10/01", Value = "Manage Documents & Files")]
    public string ManageDocuments => this[nameof (ManageDocuments)];

    /// <summary>word: a document</summary>
    [ResourceEntry("DocumentWithArticle", Description = "a document", LastModified = "2010/08/17", Value = "a document or other file")]
    public string DocumentWithArticle => this[nameof (DocumentWithArticle)];

    /// <summary>word: Documents or other files</summary>
    [ResourceEntry("Documents", Description = "The 'Documents' string in plural.", LastModified = "2010/05/13", Value = "documents or other files")]
    public string Documents => this[nameof (Documents)];

    /// <summary>word: Documents</summary>
    [ResourceEntry("DocumentsOnly", Description = "The 'Documents' string in plural.", LastModified = "2011/09/13", Value = "documents")]
    public string DocumentsOnly => this[nameof (DocumentsOnly)];

    /// <summary>Phrase: No documents have been uploaded yet</summary>
    [ResourceEntry("NoItemsExist", Description = "The text of the message that user is presented with on the decision screen.", LastModified = "2010/05/14", Value = "No documents or files have been uploaded yet")]
    public string NoItemsExist => this[nameof (NoItemsExist)];

    /// <summary>Phrase: What do you want to do now?</summary>
    [ResourceEntry("WhatDoYouWantToDoNow", Description = "The title of the decision screen when there are no videos.", LastModified = "2010/05/14", Value = "What do you want to do now?")]
    public string WhatDoYouWantToDoNow => this[nameof (WhatDoYouWantToDoNow)];

    /// <summary>phrase: Filter Documents</summary>
    [ResourceEntry("FilterDocuments", Description = "The 'Filter Documents & Files' label in the grid sidebar.", LastModified = "2010/05/13", Value = "Filter Documents & Files")]
    public string FilterDocuments => this[nameof (FilterDocuments)];

    [ResourceEntry("FilterDocumentsBy", Description = "The 'Filter documents & files' label in the sidebar.", LastModified = "2013/03/11", Value = "Filter documents & files<br/><span class='sfNote'>by status, tag, category, etc.</span>")]
    public string FilterDocumentsBy => this[nameof (FilterDocumentsBy)];

    /// <summary>phrase: Filter Documents in this library</summary>
    [ResourceEntry("FilterDocumentsInThisLibrary", Description = "The 'Filter Documents in this library' label in the grid sidebar.", LastModified = "2010/05/13", Value = "Filter items in this library")]
    public string FilterDocumentsInThisLibrary => this[nameof (FilterDocumentsInThisLibrary)];

    /// <summary>phrase: All Documents</summary>
    [ResourceEntry("AllDocuments", Description = "The 'All Documents' link in the grid sidebar.", LastModified = "2010/05/13", Value = "All items")]
    public string AllDocuments => this[nameof (AllDocuments)];

    /// <summary>phrase: My Documents</summary>
    [ResourceEntry("MyDocuments", Description = "The 'My Documents' link in the grid sidebar.", LastModified = "2010/05/13", Value = "My items")]
    public string MyDocuments => this[nameof (MyDocuments)];

    /// <summary>phrase: Documents by Library</summary>
    [ResourceEntry("DocumentsByLibrary", Description = "The 'Documents by Library' label in the grid sidebar.", LastModified = "2010/05/13", Value = "Items by Library")]
    public string DocumentsByLibrary => this[nameof (DocumentsByLibrary)];

    /// <summary>phrase: Documents By Date</summary>
    [ResourceEntry("DocumentsByDate", Description = "phrase: Documents By Date", LastModified = "2010/08/20", Value = "Display items modified in...")]
    public string DocumentsByDate => this[nameof (DocumentsByDate)];

    /// <summary>Upload documents button</summary>
    [ResourceEntry("UploadDocumentsButton", Description = "The text of the upload button in grid toolbar.", LastModified = "2010/08/25", Value = "Upload documents or other files")]
    public string UploadDocumentsButton => this[nameof (UploadDocumentsButton)];

    /// <summary>phrase: Documents by category</summary>
    [ResourceEntry("DocumentsByCategory", Description = "The 'Documents by category' label in the grid sidebar.", LastModified = "2010/07/22", Value = "Items by category")]
    public string DocumentsByCategory => this[nameof (DocumentsByCategory)];

    /// <summary>phrase: Documents by tag</summary>
    [ResourceEntry("DocumentsByTag", Description = "The 'Documents by tag' label in the grid sidebar.", LastModified = "2010/07/22", Value = "Items by tag")]
    public string DocumentsByTag => this[nameof (DocumentsByTag)];

    /// <summary>phrase: Permissions for Documents</summary>
    [ResourceEntry("PermissionsForDocuments", Description = "The 'Permissions for Documents' label in the permissions dialog.", LastModified = "2010/07/27", Value = "Permissions for Documents & Files")]
    public string PermissionsForDocuments => this[nameof (PermissionsForDocuments)];

    /// <summary>phrase: Draft Documents</summary>
    [ResourceEntry("DraftDocuments", Description = "The 'Draft Documents' link in the permissions dialog.", LastModified = "2010/07/26", Value = "Draft")]
    public string DraftDocuments => this[nameof (DraftDocuments)];

    /// <summary>phrase: Published Documents</summary>
    [ResourceEntry("PublishedDocuments", Description = "The 'Published Documents' link in the grid sidebar.", LastModified = "2010/07/26", Value = "Published")]
    public string PublishedDocuments => this[nameof (PublishedDocuments)];

    /// <summary>phrase: Scheduled Documents</summary>
    [ResourceEntry("ScheduledDocuments", Description = "The link for displaying scheduled documents in the sidebar.", LastModified = "2010/08/20", Value = "Scheduled")]
    public string ScheduledDocuments => this[nameof (ScheduledDocuments)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2010/06/02", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>phrase: Settings for Documents</summary>
    [ResourceEntry("SettingsForDocuments", Description = "The 'Settings for Documents' label in the grid sidebar.", LastModified = "2010/05/13", Value = "Settings for Documents & Files")]
    public string SettingsForDocuments => this[nameof (SettingsForDocuments)];

    [ResourceEntry("PagesWhereDocumentsAndFilesArePublished", Description = "Pages where Documents & Files are published", LastModified = "2013/03/25", Value = "Pages where Documents & Files are published")]
    public string PagesWhereDocumentsAndFilesArePublished => this[nameof (PagesWhereDocumentsAndFilesArePublished)];

    /// <summary>Phrase: Library name</summary>
    [ResourceEntry("LibraryName", Description = "The 'Library name' label.", LastModified = "2010/05/12", Value = "Library name")]
    public string LibraryName => this[nameof (LibraryName)];

    /// <summary>Example: Financial report</summary>
    [ResourceEntry("LibraryNameExample", Description = "The example how to fill library name field.", LastModified = "2010/05/12", Value = "Example: Financial report")]
    public string LibraryNameExample => this[nameof (LibraryNameExample)];

    /// <summary>Phrase: Library name cannot be empty</summary>
    [ResourceEntry("LibraryNameCannotBeEmpty", Description = "The message shown when the library name is empty.", LastModified = "2010/05/12", Value = "Library name cannot be empty")]
    public string LibraryNameCannotBeEmpty => this[nameof (LibraryNameCannotBeEmpty)];

    /// <summary>Word: Description</summary>
    [ResourceEntry("Description", Description = "The 'Description' label.", LastModified = "2010/05/12", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Phrase: The description of this library</summary>
    [ResourceEntry("LibraryDescription", Description = "The description of the Description field in the library form.", LastModified = "2010/05/12", Value = "The description of this library.")]
    public string LibraryDescription => this[nameof (LibraryDescription)];

    /// <summary>Phrase: Create and go back</summary>
    [ResourceEntry("CreateThisLibrary", Description = "The 'Create and go back' button in the library form.", LastModified = "2013/03/27", Value = "Create and go back")]
    public string CreateThisLibrary => this[nameof (CreateThisLibrary)];

    /// <summary>Phrase: Save Changes</summary>
    [ResourceEntry("SaveChanges", Description = "The label for saving changes.", LastModified = "2010/07/27", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Phrase: Create a library</summary>
    [ResourceEntry("CreateLibrary", Description = "The label for creating a library.", LastModified = "2010/08/16", Value = "Create a library")]
    public string CreateLibrary => this[nameof (CreateLibrary)];

    /// <summary>Phrase: Merge libraries</summary>
    [ResourceEntry("MergeLibraries", Description = "The label for merging libraries.", LastModified = "2010/05/26", Value = "Merge libraries")]
    public string MergeLibraries => this[nameof (MergeLibraries)];

    /// <summary>Phrase: Create and add another library</summary>
    [ResourceEntry("CreateAndAddAnotherLibrary", Description = "The 'Create and add another library' button in the library form.", LastModified = "2010/05/12", Value = "Create and add another library")]
    public string CreateAndAddAnotherLibrary => this[nameof (CreateAndAddAnotherLibrary)];

    /// <summary>Phrase: Title / Status</summary>
    [ResourceEntry("TitleStatus", Description = "Header text for the title column in the backend grid", LastModified = "2010/05/13", Value = "Title / Status")]
    public string TitleStatus => this[nameof (TitleStatus)];

    /// <summary>Phrase: Embed link to this file</summary>
    [ResourceEntry("EmbedThisDocument", Description = "Text of link for embedding a document in the backend grid", LastModified = "2010/05/13", Value = "Embed link to this file")]
    public string EmbedThisDocument => this[nameof (EmbedThisDocument)];

    /// <summary>Phrase: Library / Categories / Tags</summary>
    [ResourceEntry("LibraryCategoriesTags", Description = "Header text for corresponding column in backend grid", LastModified = "2010/05/13", Value = "Library / Categories / Tags")]
    public string LibraryCategoriesTags => this[nameof (LibraryCategoriesTags)];

    /// <summary>Phrase: Max library size</summary>
    [ResourceEntry("MaxLibrarySize", Description = "The title of the field for setting max library size.", LastModified = "2010/05/14", Value = "Max library size")]
    public string MaxLibrarySize => this[nameof (MaxLibrarySize)];

    /// <summary>Phrase: Max document size</summary>
    [ResourceEntry("MaxDocumentSize", Description = "The title of the field for setting max document size.", LastModified = "2010/05/14", Value = "Max document/file size")]
    public string MaxDocumentSize => this[nameof (MaxDocumentSize)];

    /// <summary>phrase: Change library</summary>
    [ResourceEntry("ChangeLibraryInSpan", Description = "phrase: Change library", LastModified = "2010/05/14", Value = "<span class='sfLinkBtnIn'>Change library</span>")]
    public string ChangeLibraryInSpan => this[nameof (ChangeLibraryInSpan)];

    /// <summary>Phrase: Edit Document</summary>
    [ResourceEntry("EditDocument", Description = "The title of the dialog for editing documents.", LastModified = "2010/05/15", Value = "Edit properties")]
    public string EditDocument => this[nameof (EditDocument)];

    /// <summary>Phrase: Change</summary>
    [ResourceEntry("Change", Description = "The text of the button for changing the video thumbnail.", LastModified = "2010/10/07", Value = "Change")]
    public string Change => this[nameof (Change)];

    /// <summary>Phrase: Open the file</summary>
    [ResourceEntry("OpenFile", Description = "The text of the button for opening a file in edit properties form.", LastModified = "2010/05/19", Value = "Open the file")]
    public string OpenFile => this[nameof (OpenFile)];

    /// <summary>Phrase: Replace the file</summary>
    [ResourceEntry("ReplaceFile", Description = "The text of the button for replacing a file in edit properties form.", LastModified = "2010/05/19", Value = "Replace the file")]
    public string ReplaceFile => this[nameof (ReplaceFile)];

    /// <summary>Phrase: {0} more libraries</summary>
    [ResourceEntry("ShowMoreLibraries", Description = "Phrase: {0} more libraries", LastModified = "2010/05/18", Value = "{0} more libraries")]
    public string ShowMoreLibraries => this[nameof (ShowMoreLibraries)];

    /// <summary>Phrase: {0} less libraries</summary>
    [ResourceEntry("ShowLessLibraries", Description = "Phrase: {0} less libraries", LastModified = "2010/05/18", Value = "{0} less libraries")]
    public string ShowLessLibraries => this[nameof (ShowLessLibraries)];

    /// <summary>Phrase: Sort documents:</summary>
    [ResourceEntry("SortDocuments", Description = "Phrase: Sort documents: ", LastModified = "2010/05/18", Value = "Sort items: ")]
    public string SortDocuments => this[nameof (SortDocuments)];

    /// <summary>Word: Parts</summary>
    [ResourceEntry("Parts", Description = "Word: Parts", LastModified = "2010/07/26", Value = "Other details")]
    public string Parts => this[nameof (Parts)];

    /// <summary>Example: 24 pages</summary>
    [ResourceEntry("PartsExample", Description = "The example how to fill parts field.", LastModified = "2010/06/21", Value = "<strong>Example:</strong> <em>24 pages</em>")]
    public string PartsExample => this[nameof (PartsExample)];

    /// <summary>phrase: Delete this document</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>Permissions</summary>
    [ResourceEntry("Permissions", Description = "The link that navigates to permissions dialog in the sidebar.", LastModified = "2010/03/17", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "The text of the cancel button.", LastModified = "2010/03/30", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>phrase: ReviewHistory</summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Revision History")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "The text of the 'Set permissions' link in the action menu.", LastModified = "2010/03/22", Value = "Set Permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>phrase: All published documents and files</summary>
    [ResourceEntry("AllPublishedDocumentsAndFiles", Description = "phrase: All published documents and files", LastModified = "2010/05/27", Value = "All published documents and files")]
    public string AllPublishedDocumentsAndFiles => this[nameof (AllPublishedDocumentsAndFiles)];

    /// <summary>phrase: Choose library</summary>
    [ResourceEntry("ChooseLibrary", Description = "Phrase: Choose library", LastModified = "2010/05/27", Value = "Choose library")]
    public string ChooseLibrary => this[nameof (ChooseLibrary)];

    /// <summary>phrase: From selected library...</summary>
    [ResourceEntry("FromSelectedLibrary", Description = "Phrase: From selected library...", LastModified = "2011/09/12", Value = "From selected library...")]
    public string FromSelectedLibrary => this[nameof (FromSelectedLibrary)];

    /// <summary>phrase: No library selected</summary>
    [ResourceEntry("NoLibrarySelected", Description = "Phrase: No library selected", LastModified = "2010/05/27", Value = "No library selected")]
    public string NoLibrarySelected => this[nameof (NoLibrarySelected)];

    /// <summary>phrase: No library selected</summary>
    [ResourceEntry("AsOrderedInLibrary", Description = "Phrase: As they are ordered in library", LastModified = "2011/09/07", Value = "As they are ordered in library")]
    public string AsOrderedInLibrary => this[nameof (AsOrderedInLibrary)];

    /// <summary>word: Table</summary>
    [ResourceEntry("Table", Description = "Word: Table", LastModified = "2010/05/27", Value = "Table")]
    public string Table => this[nameof (Table)];

    /// <summary>word: List</summary>
    [ResourceEntry("List", Description = "Word: List", LastModified = "2010/05/27", Value = "List")]
    public string List => this[nameof (List)];

    /// <summary>phrase: No icons</summary>
    [ResourceEntry("NoIcons", Description = "Phrase: No icons", LastModified = "2010/05/27", Value = "No icons")]
    public string NoIcons => this[nameof (NoIcons)];

    /// <summary>phrase: Big icons</summary>
    [ResourceEntry("BigIcons", Description = "Phrase: Big icons", LastModified = "2010/05/27", Value = "Big icons")]
    public string BigIcons => this[nameof (BigIcons)];

    /// <summary>phrase: Small icons</summary>
    [ResourceEntry("SmallIcons", Description = "Phrase: Small icons", LastModified = "2010/05/27", Value = "Small icons")]
    public string SmallIcons => this[nameof (SmallIcons)];

    /// <summary>phrase: Display icons</summary>
    [ResourceEntry("DisplayIcons", Description = "Phrase: Display icons", LastModified = "2010/05/27", Value = "Display icons")]
    public string DisplayIcons => this[nameof (DisplayIcons)];

    /// <summary>Library</summary>
    [ResourceEntry("Library", Description = "word: Library", LastModified = "2010/05/26", Value = "Library")]
    public string Library => this[nameof (Library)];

    /// <summary>
    /// phrase: <i>{0}</i>
    /// </summary>
    [ResourceEntry("LibraryDocumentsTitleFormat", Description = "phrase: <i>{0}</i>", LastModified = "2011/02/17", Value = "<i>{0}</i>")]
    public string LibraryDocumentsTitleFormat => this[nameof (LibraryDocumentsTitleFormat)];

    /// <summary>Common library</summary>
    [ResourceEntry("CommonLibrary", Description = "phrase: Common library", LastModified = "2010/06/18", Value = "Common library")]
    public string CommonLibrary => this[nameof (CommonLibrary)];

    /// <summary>Libraries</summary>
    [ResourceEntry("Libraries", Description = "word: Libraries", LastModified = "2010/05/26", Value = "Libraries")]
    public string Libraries => this[nameof (Libraries)];

    /// <summary>Message: Back to all documents</summary>
    /// <value>Text of the link that will show the view that lists all documents.</value>
    [ResourceEntry("BackToAllDocuments", Description = "Text of the link that will show the view that lists all documents.", LastModified = "2010/05/28", Value = "Back to Documents & Files")]
    public string BackToAllDocuments => this[nameof (BackToAllDocuments)];

    /// <summary>Message: Back to all items</summary>
    /// <value>The back to all items button</value>
    [ResourceEntry("BackToItems", Description = "The back to all items button", LastModified = "2010/10/16", Value = "Back to all items")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Message: View all documents</summary>
    /// <value>Text of the link that will show the view that lists all documents.</value>
    [ResourceEntry("BackToDocuments", Description = "Text of the link that will show the view that lists all documents.", LastModified = "2010/05/28", Value = "View all documents and other files")]
    public string BackToDocuments => this[nameof (BackToDocuments)];

    /// <summary>Phrase: Back to all libraries</summary>
    [ResourceEntry("BackToAllLibraries", Description = "The label for link to return to all libraries.", LastModified = "2010/05/28", Value = "Back to all libraries")]
    public string BackToAllLibraries => this[nameof (BackToAllLibraries)];

    /// <summary>Phrase: Upload documents</summary>
    [ResourceEntry("UploadDocuments", Description = "The label for uploading documents.", LastModified = "2010/06/27", Value = "Upload documents or other files")]
    public string UploadDocuments => this[nameof (UploadDocuments)];

    /// <summary>Phrase: Upload documents or other files</summary>
    [ResourceEntry("UploadDocumentsOrOtherFiles", Description = "The label for uploading documents", LastModified = "2010/12/20", Value = "Upload documents or other files")]
    public string UploadDocumentsOrOtherFiles => this[nameof (UploadDocumentsOrOtherFiles)];

    /// <summary>Phrase: Upload a document</summary>
    [ResourceEntry("UploadNew", Description = "Upload new...", LastModified = "2010/08/02", Value = "Upload new...")]
    public string UploadNew => this[nameof (UploadNew)];

    /// <summary>Phrase: Last uploaded on</summary>
    [ResourceEntry("LastUploaded", Description = "'Last uploaded on' label in the libraries grid.", LastModified = "2013/03/06", Value = "Last uploaded on")]
    public string LastUploaded => this[nameof (LastUploaded)];

    /// <summary>Phrase: Below the description</summary>
    [ResourceEntry("BelowDescription", Description = "'Below the description' label in the downloadlist designers.", LastModified = "2010/05/26", Value = "Below the description")]
    public string BelowDescription => this[nameof (BelowDescription)];

    /// <summary>Phrase: Above the description</summary>
    [ResourceEntry("AboveDescription", Description = "'Above the description' label in the downloadlist designers.", LastModified = "2010/05/26", Value = "Above the description")]
    public string AboveDescription => this[nameof (AboveDescription)];

    /// <summary>Phrase: Above the description</summary>
    [ResourceEntry("PositionOfDownloadLinkOnDetailPage", Description = "'Position of the Download link on the detail page' label in the downloadlist designers.", LastModified = "2010/05/26", Value = "Position of the <em>Download</em> link on the Detail page")]
    public string PositionOfDownloadLinkOnDetailPage => this[nameof (PositionOfDownloadLinkOnDetailPage)];

    /// <summary>phrase: List + Detail page</summary>
    [ResourceEntry("ListDetailPage", Description = "Phrase: List + Detail page", LastModified = "2011/03/21", Value = "List + Detail page <span class='sfNote'>Download link is on the Detail page.</span>")]
    public string ListDetailPage => this[nameof (ListDetailPage)];

    /// <summary>phrase: Table + Detail page</summary>
    [ResourceEntry("TableDetailPage", Description = "Phrase: Table + Detail page", LastModified = "2011/03/21", Value = "Table + Detail page <span class='sfNote'>Download links are both on the Detail and the Table page</span>")]
    public string TableDetailPage => this[nameof (TableDetailPage)];

    /// <summary>phrase: Select a document</summary>
    [ResourceEntry("DocumentLinkPropertyEditorTitle", Description = "Phrase: Select a document", LastModified = "2013/02/25", Value = "Select a document or other file")]
    public string DocumentLinkPropertyEditorTitle => this[nameof (DocumentLinkPropertyEditorTitle)];

    /// <summary>phrase: Which document to upload?</summary>
    [ResourceEntry("WhichDocumentToUpload", Description = "Phrase: Which document to upload?", LastModified = "2010/06/17", Value = "Which document or other file to upload?")]
    public string WhichDocumentToUpload => this[nameof (WhichDocumentToUpload)];

    /// <summary>phrase: Where to store the uploaded document?</summary>
    [ResourceEntry("WhereToStoreTheUploadedDocument", Description = "Phrase: Where to store the uploaded document?", LastModified = "2010/06/17", Value = "Where to store the uploaded document/file?")]
    public string WhereToStoreTheUploadedDocument => this[nameof (WhereToStoreTheUploadedDocument)];

    /// <summary>phrase: From already uploaded</summary>
    [ResourceEntry("FromAlreadyUploadedDocuments", Description = "Phrase: From already uploaded", LastModified = "2010/06/17", Value = "From already uploaded")]
    public string FromAlreadyUploadedDocuments => this[nameof (FromAlreadyUploadedDocuments)];

    /// <summary>phrase: Display icons in the table</summary>
    [ResourceEntry("DisplayIconsInTable", Description = "Phrase: Display icons in the table", LastModified = "2010/05/27", Value = "Display icons in the table")]
    public string DisplayIconsInTable => this[nameof (DisplayIconsInTable)];

    /// <summary>phrase: Download link is on the Detail page.</summary>
    [ResourceEntry("DownloadLinkIsOnTheDetailPage", Description = "Phrase: Download link is on the Detail page.", LastModified = "2010/05/27", Value = "Download link is on the Detail page.")]
    public string DownloadLinkIsOnTheDetailPage => this[nameof (DownloadLinkIsOnTheDetailPage)];

    /// <summary>
    /// phrase: Download Links Are Both On the Detail and the Table page
    /// </summary>
    [ResourceEntry("DownloadLinksAreBothOnTheDetailAndTheTablePage", Description = "phrase: Download links are both on the Detail and the Table page", LastModified = "2010/06/16", Value = "Download links are both on the Detail and the Table page")]
    public string DownloadLinksAreBothOnTheDetailAndTheTablePage => this[nameof (DownloadLinksAreBothOnTheDetailAndTheTablePage)];

    /// <summary>Phrase: Details (Author, Description, Parts)</summary>
    [ResourceEntry("Details", Description = "The title of the 'Details' section.", LastModified = "2010/06/21", Value = "Details <em class='sfNote'>(Author, Description, Parts)</em>")]
    public string Details => this[nameof (Details)];

    /// <summary>
    /// phrase: A document was not selected or has been deleted. Please select another one.
    /// </summary>
    [ResourceEntry("DocumentWasNotSelectedOrHasBeenDeleted", Description = "phrase: A document was not selected or has been deleted. Please select another one.", LastModified = "2010/07/20", Value = "A document or file was not selected or has been deleted. Please select another one.")]
    public string DocumentWasNotSelectedOrHasBeenDeleted => this[nameof (DocumentWasNotSelectedOrHasBeenDeleted)];

    /// <summary>The title of the edit item dialog</summary>
    [ResourceEntry("EditItem", Description = "The title of the edit item dialog", LastModified = "2010/08/11", Value = "Edit a document or other file")]
    public string EditItem => this[nameof (EditItem)];

    /// <summary>The title of the create document dialog</summary>
    [ResourceEntry("CreateItem", Description = "The title of the create document dialog", LastModified = "2010/11/23", Value = "Create a document")]
    public string CreateItem => this[nameof (CreateItem)];

    /// <summary>The title of the view item dialog</summary>
    [ResourceEntry("ViewItem", Description = "The title of the view item dialog", LastModified = "2010/09/23", Value = "View a document or other file")]
    public string ViewItem => this[nameof (ViewItem)];

    /// <summary>word: Delete</summary>
    [ResourceEntry("Delete", Description = "The text of the delete button.", LastModified = "2010/08/11", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>word: document</summary>
    [ResourceEntry("DocumentSingularItemName", Description = "word: document", LastModified = "2010/10/11", Value = "document or other file")]
    public string DocumentSingularItemName => this[nameof (DocumentSingularItemName)];

    /// <summary>word: documents</summary>
    [ResourceEntry("DocumentPluralItemName", Description = "word: documents", LastModified = "2010/10/11", Value = "documents or other files")]
    public string DocumentPluralItemName => this[nameof (DocumentPluralItemName)];

    /// <summary>Phrase: Select a document</summary>
    [ResourceEntry("SelectDocument", Description = "Phrase: Select a document", LastModified = "2010/11/12", Value = "Select a document or other file")]
    public string SelectDocument => this[nameof (SelectDocument)];

    /// <summary>Phrase: Edit Download list settings</summary>
    [ResourceEntry("EditDownloadListSettings", Description = "Phrase: Edit Download list settings", LastModified = "2010/11/12", Value = "Edit Download list settings")]
    public string EditDownloadListSettings => this[nameof (EditDownloadListSettings)];

    /// <summary>Phrase: Narrow by typing title</summary>
    [ResourceEntry("NarrowByTypingTitle", Description = "Phrase: Narrow by typing title", LastModified = "2010/10/20", Value = "Narrow by typing title")]
    public string NarrowByTypingTitle => this[nameof (NarrowByTypingTitle)];

    /// <summary>phrase: Documents data fields</summary>
    [ResourceEntry("DocumentsDataFields", Description = "phrase: Documents data fields", LastModified = "2010/11/29", Value = "Documents data fields")]
    public string DocumentsDataFields => this[nameof (DocumentsDataFields)];

    /// <summary>Phrase: Create and go to upload documents</summary>
    [ResourceEntry("CreateAndGoToUploadDocuments", Description = "phrase: Create and go to upload documents.", LastModified = "2011/01/24", Value = "Create and go to upload documents")]
    public string CreateAndGoToUploadDocuments => this[nameof (CreateAndGoToUploadDocuments)];

    /// <summary>Phrase: Manage Document Libraries</summary>
    [ResourceEntry("ManageDocumentLibraries", Description = "The text of 'Manage Document Libraries' link.", LastModified = "2010/07/19", Value = "Manage Document Libraries")]
    public string ManageDocumentLibraries => this[nameof (ManageDocumentLibraries)];

    /// <summary>label: MB</summary>
    [ResourceEntry("Mb", Description = "label: MB", LastModified = "2011/07/13", Value = "MB")]
    public string Mb => this[nameof (Mb)];

    /// <summary>label: KB</summary>
    [ResourceEntry("Kb", Description = "label: KB", LastModified = "2011/07/13", Value = "KB")]
    public string Kb => this[nameof (Kb)];

    /// <summary>phrase: Reorder documents</summary>
    [ResourceEntry("ReorderDocuments", Description = "phrase: Reorder documents", LastModified = "2011/09/07", Value = "<em>Reorder</em> documents")]
    public string ReorderDocuments => this[nameof (ReorderDocuments)];

    /// <summary>
    /// The title of the dialog for upload a document or other file.
    /// </summary>
    /// <value>'Upload document or other file' in the Backend</value>
    [ResourceEntry("UploadDocument", Description = "The title of the dialog for upload a document or other file.", LastModified = "2014/04/07", Value = "'Upload document or other file' in the Backend")]
    public string UploadDocument => this[nameof (UploadDocument)];

    /// <summary>Phrase: Edit user file properties</summary>
    /// <value>Edit user file properties</value>
    [ResourceEntry("EditUserFile", Description = "Phrase: Edit user file properties", LastModified = "2014/04/09", Value = "Edit user file properties")]
    public string EditUserFile => this[nameof (EditUserFile)];

    /// <summary>Phrase: Revision history of user file</summary>
    /// <value>Revision history of user file</value>
    [ResourceEntry("UserFileVersionPreview", Description = "Phrase: Revision history of user file", LastModified = "2014/04/09", Value = "Revision history of user file")]
    public string UserFileVersionPreview => this[nameof (UserFileVersionPreview)];

    /// <summary>Phrase: Preview user file</summary>
    /// <value>Preview user file</value>
    [ResourceEntry("PreviewUserFile", Description = "Phrase: Preview user file", LastModified = "2014/04/09", Value = "Preview user file")]
    public string PreviewUserFile => this[nameof (PreviewUserFile)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: Last uploaded on top</summary>
    [ResourceEntry("NewUploadedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last uploaded on top")]
    public string NewUploadedFirst => this[nameof (NewUploadedFirst)];

    /// <summary>phrase: by Hierarchy</summary>
    [ResourceEntry("ByHierarchy", Description = "Label text.", LastModified = "2010/04/06", Value = "by Hierarchy")]
    public string ByHierarchy => this[nameof (ByHierarchy)];

    /// <summary>phrase: by Status</summary>
    [ResourceEntry("ByStatus", Description = "Label text.", LastModified = "2010/04/06", Value = "by Status")]
    public string ByStatus => this[nameof (ByStatus)];

    /// <summary>phrase: by Template</summary>
    [ResourceEntry("SortByTemplate", Description = "Label text.", LastModified = "2010/04/06", Value = "by Template")]
    public string SortByTemplate => this[nameof (SortByTemplate)];

    /// <summary>phrase: Alphabetically (A-Z)</summary>
    [ResourceEntry("AlphabeticallyAsc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (A-Z)")]
    public string AlphabeticallyAsc => this[nameof (AlphabeticallyAsc)];

    /// <summary>phrase: Alphabetically (Z-A)</summary>
    [ResourceEntry("AlphabeticallyDesc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (Z-A)")]
    public string AlphabeticallyDesc => this[nameof (AlphabeticallyDesc)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in documents", LastModified = "2012/04/12", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2013/01/17", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/22", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: Publish documents</summary>
    [ResourceEntry("PublishDocuments", Description = "Publish documents.", LastModified = "2013/02/28", Value = "Publish documents")]
    public string PublishDocuments => this[nameof (PublishDocuments)];

    /// <summary>phrase: Unpublish documents</summary>
    [ResourceEntry("UnpublishDocuments", Description = "Unpublish documents.", LastModified = "2013/02/28", Value = "Unpublish documents")]
    public string UnpublishDocuments => this[nameof (UnpublishDocuments)];

    /// <summary>phrase: Bulk edit document properties</summary>
    [ResourceEntry("BulkEditDocumentProperties", Description = "Bulk edit document properties", LastModified = "2013/02/28", Value = "Bulk edit document properties")]
    public string BulkEditDocumentProperties => this[nameof (BulkEditDocumentProperties)];

    [ResourceEntry("DocumentsAndFilesByLibrary", Description = "Documents & files by library", LastModified = "2013/03/11", Value = "Documents & files by library")]
    public string DocumentsAndFilesByLibrary => this[nameof (DocumentsAndFilesByLibrary)];

    [ResourceEntry("AllDocumentsAndFiles", Description = "phrase: All documents & files", LastModified = "2013/03/28", Value = "All documents & files")]
    public string AllDocumentsAndFiles => this[nameof (AllDocumentsAndFiles)];

    /// <summary>phrase: Manage also.</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2013/09/25", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Comments for documents.</summary>
    [ResourceEntry("CommentsForDocuments", Description = "phrase: Comments for documents", LastModified = "2013/09/25", Value = "Comments for documents")]
    public string CommentsForDocuments => this[nameof (CommentsForDocuments)];

    /// <summary>Phrase: Edit document properties</summary>
    /// <value>Edit document properties</value>
    [ResourceEntry("EditDocumentProperties", Description = "Phrase: Edit document properties", LastModified = "2014/04/09", Value = "Edit document properties")]
    public string EditDocumentProperties => this[nameof (EditDocumentProperties)];

    /// <summary>Phrase: Preview document</summary>
    /// <value>Preview document</value>
    [ResourceEntry("PreviewDocument", Description = "Phrase: Preview document", LastModified = "2014/04/09", Value = "Preview document")]
    public string PreviewDocument => this[nameof (PreviewDocument)];

    /// <summary>Phrase: Revision history of document</summary>
    /// <value>Revision history of document</value>
    [ResourceEntry("DocumentVersionHistory", Description = "Phrase: Revision history of document", LastModified = "2014/04/09", Value = "Revision history of document")]
    public string DocumentVersionHistory => this[nameof (DocumentVersionHistory)];

    /// <summary>Label text</summary>
    /// <value>As manually ordered</value>
    [ResourceEntry("AsManuallyOrdered", Description = "Label text", LastModified = "2014/04/23", Value = "As manually ordered")]
    public string AsManuallyOrdered => this[nameof (AsManuallyOrdered)];

    /// <summary>Word: Language</summary>
    [ResourceEntry("Language", Description = "word: Language", LastModified = "2016/05/31", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>Phrase: Upload documents</summary>
    [ResourceEntry("UploadDocumentsLabel", Description = "phrase: Upload documents", LastModified = "2020/6/03", Value = "Upload documents")]
    public string UploadDocumentsLabel => this[nameof (UploadDocumentsLabel)];

    /// <summary>Permissions for documents</summary>
    [ResourceEntry("PermissionsForDocumentsOperationTitle", Description = "The title of the permissions dialog.", LastModified = "2020/6/03", Value = "Permissions for documents")]
    public string PermissionsForDocumentsOperationTitle => this[nameof (PermissionsForDocumentsOperationTitle)];

    /// <summary>Custom fields for documents</summary>
    [ResourceEntry("CustomFields", Description = "The link that navigates to the dialog for managing custom fields in the sidebar.", LastModified = "2020/6/03", Value = "Custom fields for documents")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>Message: Back to documents</summary>
    [ResourceEntry("BackToDocumentsOperationTitle", Description = "Text of the link that will return back to documents.", LastModified = "2020/6/03", Value = "Back to documents")]
    public string BackToDocumentsOperationTitle => this[nameof (BackToDocumentsOperationTitle)];

    /// <summary>Pages where documents are published</summary>
    [ResourceEntry("PagesWhereDocumentsArePublished", Description = "Pages where documents are published", LastModified = "2020/6/04", Value = "Pages where documents are published")]
    public string PagesWhereDocumentsArePublished => this[nameof (PagesWhereDocumentsArePublished)];

    [ResourceEntry("AdminAppEditOperationTitle", Description = "Title for the 'Edit' operation in the Admin app. (Alternative to 'Title and properties')", LastModified = "2020/6/04", Value = "Document properties")]
    public string AdminAppEditOperationTitle => this[nameof (AdminAppEditOperationTitle)];

    [ResourceEntry("DocumentsLibrary", Description = "Title for the library column in Iris", LastModified = "2020/6/08", Value = "Documents library")]
    public string DocumentsLibrary => this[nameof (DocumentsLibrary)];

    /// <summary>Gets Title for the bulk edit of documents</summary>
    [ResourceEntry("DocumentPropertiesBulk", Description = "Document properties (bulk).", LastModified = "2020/07/17", Value = "Document properties (bulk)")]
    public string DocumentPropertiesBulk => this[nameof (DocumentPropertiesBulk)];
  }
}
