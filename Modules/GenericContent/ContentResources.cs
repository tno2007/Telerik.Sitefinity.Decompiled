// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("ContentResources", ResourceClassId = "ContentResources")]
  public sealed class ContentResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ContentResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ContentResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Generic Content</summary>
    [ResourceEntry("ContentResourcesTitle", Description = "The title of this class.", LastModified = "2009/07/02", Value = "Generic Content")]
    public string ContentResourcesTitle => this[nameof (ContentResourcesTitle)];

    /// <summary>Generic Content</summary>
    [ResourceEntry("ContentResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/07/02", Value = "Generic Content")]
    public string ContentResourcesTitlePlural => this[nameof (ContentResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Generic Content user interface.
    /// </summary>
    [ResourceEntry("ContentResourcesDescription", Description = "The description of this class.", LastModified = "2009/07/02", Value = "Contains localizable resources for Generic Content user interface.")]
    public string ContentResourcesDescription => this[nameof (ContentResourcesDescription)];

    /// <summary>Content items' type name (plural)</summary>
    [ResourceEntry("ContentItemsPluralTypeName", Description = "Content items' type name (plural)", LastModified = "2013/04/03", Value = "Content Blocks")]
    public string ContentItemsPluralTypeName => this[nameof (ContentItemsPluralTypeName)];

    /// <summary>Content items' type name (singular)</summary>
    [ResourceEntry("ContentItemsSingleTypeName", Description = "Content items' type name (singular)", LastModified = "2013/04/03", Value = "Content")]
    public string ContentItemsSingleTypeName => this[nameof (ContentItemsSingleTypeName)];

    /// <summary>Phrase: Content blocks</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Content Blocks module.", LastModified = "2011/02/01", Value = "Content blocks")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>
    /// Phrase: Content blocks shared across pages and templates
    /// </summary>
    [ResourceEntry("ContentBlocksShared", Description = "Phrase: Content blocks shared across pages and templates", LastModified = "2012/11/12", Value = "Content blocks shared across pages and templates")]
    public string ContentBlocksShared => this[nameof (ContentBlocksShared)];

    /// <summary>Phrase: Manage Content Blocks</summary>
    [ResourceEntry("ManageContentBlocks", Description = "Phrase: Manage Content Blocks", LastModified = "2011/02/02", Value = "Manage Content Blocks")]
    public string ManageContentBlocks => this[nameof (ManageContentBlocks)];

    /// <summary>Content blocks page group</summary>
    [ResourceEntry("PageGroupNodeDescription", Description = "Description of the content blocks module's page group.", LastModified = "2011/02/01", Value = "Content blocks page group")]
    public string PageGroupNodeDescription => this[nameof (PageGroupNodeDescription)];

    /// <summary>Content blocks</summary>
    [ResourceEntry("PageGroupNodeTitle", Description = "Content blocks page title.", LastModified = "2011/02/01", Value = "Content blocks")]
    public string PageGroupNodeTitle => this[nameof (PageGroupNodeTitle)];

    /// <summary>Comments</summary>
    [ResourceEntry("CommentsUrlName", Description = "Comments URL", LastModified = "2013/07/02", Value = "Comments")]
    public string CommentsUrlName => this[nameof (CommentsUrlName)];

    /// <summary>Error Message: Invalid generic type specified.</summary>
    [ResourceEntry("InvalidGenericType", Description = "Error message.", LastModified = "2009/12/19", Value = "Invalid generic type specified.")]
    public string InvalidGenericType => this[nameof (InvalidGenericType)];

    /// <summary>Error Message: Invalid object type specified.</summary>
    [ResourceEntry("InvalidObjectType", Description = "Error message.", LastModified = "2009/07/02", Value = "Invalid object type specified.")]
    public string InvalidObjectType => this[nameof (InvalidObjectType)];

    /// <summary>Messsage: Workflow rules do not allow to publish: {0}</summary>
    [ResourceEntry("WorkflowRulesDoNotAllowToPublish", Description = "Workflow exception message", LastModified = "2010/12/15", Value = "Workflow rules do not allow to publish: {0}")]
    public string WorkflowRulesDoNotAllowToPublish => this[nameof (WorkflowRulesDoNotAllowToPublish)];

    /// <summary>
    /// Messsage: Workflow rules do not allow to unpublish: {0}
    /// </summary>
    [ResourceEntry("WorkflowRulesDoNotAllowToUnpublish", Description = "Workflow exception message", LastModified = "2010/12/15", Value = "Workflow rules do not allow to unpublish: {0}")]
    public string WorkflowRulesDoNotAllowToUnpublish => this[nameof (WorkflowRulesDoNotAllowToUnpublish)];

    /// <summary>
    /// Message: {0} item(s) are successfully published. {1} item(s) cannot be published (see below).
    /// </summary>
    [ResourceEntry("BatchPublishErrorHeading", Description = "Batch exception message for publishing", LastModified = "2011/07/28", Value = "{0} item(s) are successfully published. {1} item(s) cannot be published (see below).")]
    public string BatchPublishErrorHeading => this[nameof (BatchPublishErrorHeading)];

    /// <summary>
    /// Message: {0} item(s) are successfully unpublished. {1} item(s) cannot be unpublished (see below).
    /// </summary>
    [ResourceEntry("BatchUnpublishErrorHeading", Description = "Batch exception message for unpublishing", LastModified = "2011/07/28", Value = "{0} item(s) are successfully unpublished. {1} item(s) cannot be unpublished (see below).")]
    public string BatchUnpublishErrorHeading => this[nameof (BatchUnpublishErrorHeading)];

    /// <summary>
    /// Message: {0} item(s) are successfully deleted. {1} item(s) cannot be deleted (see below).
    /// </summary>
    [ResourceEntry("BatchDeleteErrorHeading", Description = "Batch exception message for deleting", LastModified = "2011/07/28", Value = "{0} item(s) are successfully deleted. {1} item(s) cannot be deleted (see below).")]
    public string BatchDeleteErrorHeading => this[nameof (BatchDeleteErrorHeading)];

    /// <summary>Message: Comments</summary>
    [ResourceEntry("Comments", Description = "Name of the comments view of generic content-based modules.", LastModified = "2009/10/28", Value = "Comments")]
    public string Comments => this[nameof (Comments)];

    /// <summary>Message: Comments</summary>
    [ResourceEntry("CommentsTitle", Description = "Title of the comments view of generic content-based modules.", LastModified = "2009/10/28", Value = "Comments")]
    public string CommentsTitle => this[nameof (CommentsTitle)];

    /// <summary>Message: Manage comments</summary>
    [ResourceEntry("CommentsDescription", Description = "Provides description for the comments view of generic content-based modules.", LastModified = "2009/10/28", Value = "Manage comments")]
    public string CommentsDescription => this[nameof (CommentsDescription)];

    /// <summary>Message: Comments</summary>
    [ResourceEntry("Fields", Description = "Name of the fields view of generic content-based modules.", LastModified = "2009/10/28", Value = "Fields")]
    public string Fields => this[nameof (Fields)];

    /// <summary>Message: Comments</summary>
    [ResourceEntry("FieldsTitle", Description = "Title of the fields view of generic content-based modules.", LastModified = "2009/10/28", Value = "Custom fields")]
    public string FieldsTitle => this[nameof (FieldsTitle)];

    /// <summary>Message: Manage fields</summary>
    [ResourceEntry("FieldsDescription", Description = "Provides description for the fields view of generic content-based modules.", LastModified = "2009/10/28", Value = "Manage custom fields")]
    public string FieldsDescription => this["CommentsDescription"];

    /// <summary>Word: Category</summary>
    [ResourceEntry("Category", Description = "word", LastModified = "2010/03/22", Value = "Category")]
    public string Category => this[nameof (Category)];

    /// <summary>Word: Department</summary>
    [ResourceEntry("Department", Description = "word: Department", LastModified = "2010/07/15", Value = "Department")]
    public string Department => this[nameof (Department)];

    /// <summary>Word: Department</summary>
    [ResourceEntry("DepartmentDescription", Description = "word: Description", LastModified = "2011/11/07", Value = "Department")]
    public string DepartmentDescription => this[nameof (DepartmentDescription)];

    /// <summary>Word: Tag</summary>
    [ResourceEntry("Tag", Description = "word", LastModified = "2010/01/14", Value = "Tag")]
    public string Tag => this[nameof (Tag)];

    /// <summary>Messsage: Workflow rules do not allow to delete {0}</summary>
    /// <value>Blog Posts</value>
    [ResourceEntry("WorkflowRulesDoNotAllowToDelete", Description = "Workflow exception message", LastModified = "2010/12/15", Value = "Workflow rules do not allow to delete {0}")]
    public string WorkflowRulesDoNotAllowToDelete => this[nameof (WorkflowRulesDoNotAllowToDelete)];

    /// <summary>
    /// Error Message: An item with the URL '{0}' already exists. Please change the Url or delete the existing URL first.
    /// </summary>
    [ResourceEntry("DuplicateUrlException", Description = "Error message.", LastModified = "2009/12/19", Value = "An item with the URL '{0}' already exists.")]
    public string DuplicateUrlException => this[nameof (DuplicateUrlException)];

    /// <summary>
    /// Error Message: An item with name '{0}' already exists. Please change the name or delete the existing item first.
    /// </summary>
    [ResourceEntry("DuplicateNameException", Description = "Error message.", LastModified = "2010/12/08", Value = "An item with name '{0}' already exists. Please change the name or delete the existing item first.")]
    public string DuplicateNameException => this[nameof (DuplicateNameException)];

    /// <summary>Phrase: Content items by category</summary>
    [ResourceEntry("ContentItemsByCategory", Description = "Phrase: Content items by category", LastModified = "2010/07/23", Value = "Content items by category")]
    public string ContentItemsByCategory => this[nameof (ContentItemsByCategory)];

    /// <summary>Phrase: Content items by tag</summary>
    [ResourceEntry("ContentItemsByTag", Description = "Phrase: Content items by tag", LastModified = "2010/07/23", Value = "Content items by tag")]
    public string ContentItemsByTag => this[nameof (ContentItemsByTag)];

    /// <summary>Phrase: Name / Email / Website / IP</summary>
    [ResourceEntry("CommentsAuthorInfoColumnHeaderText", Description = "Phrase: Name / Email / Website / IP", LastModified = "2010/10/15", Value = "Name / Email / Website / IP")]
    public string CommentsAuthorInfoColumnHeaderText => this[nameof (CommentsAuthorInfoColumnHeaderText)];

    /// <summary>Phrase: Status / Date / Comment</summary>
    [ResourceEntry("CommenstContentColumnHeaderText", Description = "Status / Date / Comment", LastModified = "2010/10/15", Value = "Status / Date / Comment")]
    public string CommenstContentColumnHeaderText => this[nameof (CommenstContentColumnHeaderText)];

    /// <summary>Phrase: Post</summary>
    [ResourceEntry("CommentedItemLinkHeaderText", Description = "Post", LastModified = "2010/10/15", Value = "Post")]
    public string CommentedItemLinkHeaderText => this[nameof (CommentedItemLinkHeaderText)];

    /// <summary>Phrase: Link to commented item</summary>
    [ResourceEntry("CommentedItemLinkClientTemplateText", Description = "Link to commented item", LastModified = "2010/10/15", Value = "Link to commented item")]
    public string CommentedItemLinkClientTemplate => this["CommentedItemLinkClientTemplateText"];

    /// <summary>phrase: Back to all content items</summary>
    [ResourceEntry("BackToAllItems", Description = "phrase: Back to all content items", LastModified = "2009/12/20", Value = "Back to all content items")]
    public string ContentItem_BackToAllItems => this["BackToAllItems"];

    /// <summary>phrase: Back to content blocks</summary>
    [ResourceEntry("BackToContentBlocks", Description = "phrase: Back to content blocks", LastModified = "2011/02/03", Value = "Back to content blocks")]
    public string BackToContentBlocks => this[nameof (BackToContentBlocks)];

    /// <summary>Back to Content blocks</summary>
    [ResourceEntry("BackToItems", Description = "The text of the back to Content blocks", LastModified = "2011/02/02", Value = "Back to Content blocks")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Back to Content blocks</summary>
    [ResourceEntry("BackToItem", Description = "The text of the back to the content block", LastModified = "2011/02/02", Value = "Back to the content block")]
    public string BackToItem => this[nameof (BackToItem)];

    /// <summary>Phrase: Create a content block</summary>
    [ResourceEntry("CreateNewItem", Description = "Phrase: Create a content block", LastModified = "2011/02/01", Value = "Create a content block")]
    public string CreateNewItem => this[nameof (CreateNewItem)];

    /// <summary>The title of the edit item dialog</summary>
    [ResourceEntry("EditItem", Description = "The title of the edit item dialog", LastModified = "2011/02/02", Value = "Edit a content block")]
    public string EditItem => this[nameof (EditItem)];

    /// <summary>phrase: Create this content block</summary>
    [ResourceEntry("CreateThisItem", Description = "phrase: Create this content block", LastModified = "2011/02/03", Value = "Create this content block")]
    public string CreateThisItem => this[nameof (CreateThisItem)];

    /// <summary>phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "phrase: Save changes", LastModified = "2010/02/02", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>word: Delete</summary>
    [ResourceEntry("Delete", Description = "word: Delete", LastModified = "2010/01/25", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>phrase: Permissions for content blocks</summary>
    [ResourceEntry("PermissionsForContentBlocks", Description = "phrase: Permissions for content blocks", LastModified = "2010/07/26", Value = "Permissions")]
    public string PermissionsForContentBlocks => this[nameof (PermissionsForContentBlocks)];

    /// <summary>word: Permissions</summary>
    [ResourceEntry("Permissions", Description = "word: Permissions", LastModified = "2010/01/29", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>phrase: Filter content blocks</summary>
    [ResourceEntry("FilterContentItems", Description = "phrase: Filter content blocks", LastModified = "2012/11/29", Value = "Filter content blocks")]
    public string FilterContentItems => this[nameof (FilterContentItems)];

    /// <summary>word: Title</summary>
    [ResourceEntry("Title", Description = "word: Title", LastModified = "2010/01/25", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>word: Actions</summary>
    [ResourceEntry("Actions", Description = "word: Actions", LastModified = "2010/01/25", Value = "Actions")]
    public string Actions => this[nameof (Actions)];

    /// <summary>word: Categories</summary>
    [ResourceEntry("Categories", Description = "word: Categories", LastModified = "2010/01/25", Value = "Categories")]
    public string Categories => this[nameof (Categories)];

    /// <summary>word: Tags</summary>
    [ResourceEntry("Tags", Description = "word: Tags", LastModified = "2010/01/25", Value = "Tags")]
    public string Tags => this[nameof (Tags)];

    /// <summary>word: Author</summary>
    [ResourceEntry("Author", Description = "word: Author", LastModified = "2010/01/25", Value = "Author")]
    public string Author => this[nameof (Author)];

    /// <summary>word: Publish</summary>
    [ResourceEntry("Publish", Description = "word: Publish", LastModified = "2010/01/25", Value = "Publish")]
    public string Publish => this[nameof (Publish)];

    /// <summary>word: Unpublish</summary>
    [ResourceEntry("Unpublish", Description = "word: Unpublish", LastModified = "2010/08/03", Value = "Unpublish")]
    public string Unpublish => this[nameof (Unpublish)];

    /// <summary>phrase: More actions with content items.</summary>
    [ResourceEntry("MoreActions", Description = "phrase: More actions with content items", LastModified = "2010/01/25", Value = "More actions with content items")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>phrase: Search content items...</summary>
    [ResourceEntry("Search", Description = "phrase: Search content items...", LastModified = "2010/01/25", Value = "Search content items...")]
    public string Search => this[nameof (Search)];

    /// <summary>phrase: Manage also.</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2010/01/25", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Comments for content items.</summary>
    [ResourceEntry("CommentsForContentItems", Description = "phrase: Comments for content items", LastModified = "2010/01/25", Value = "Comments for content items")]
    public string CommentsForContentItems => this[nameof (CommentsForContentItems)];

    /// <summary>phrase: Settings for content blocks</summary>
    [ResourceEntry("SettingsForContentBlocks", Description = "phrase: Settings for content blocks", LastModified = "2011/02/02", Value = "Settings for content blocks")]
    public string SettingsForContentBlocks => this[nameof (SettingsForContentBlocks)];

    /// <summary>word: Settings</summary>
    [ResourceEntry("Settings", Description = "word: Settings", LastModified = "2011/02/07", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>phrase: Custom Fields for generic content.</summary>
    [ResourceEntry("CustomFields", Description = "phrase: Custom Fields for generic content", LastModified = "2010/01/25", Value = "Custom Fields for generic content")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>word: Date</summary>
    [ResourceEntry("Date", Description = "word: Date", LastModified = "2010/01/26", Value = "Date")]
    public string Date => this[nameof (Date)];

    /// <summary>word: View</summary>
    [ResourceEntry("View", Description = "word: View", LastModified = "2010/01/28", Value = "View")]
    public string View => this[nameof (View)];

    /// <summary>word: Properties</summary>
    [ResourceEntry("ViewProperties", Description = "word: Properties", LastModified = "2013/04/12", Value = "Properties")]
    public string ViewProperties => this[nameof (ViewProperties)];

    /// <summary>word: Duplicate</summary>
    [ResourceEntry("Duplicate", Description = "word: Duplicate", LastModified = "2010/01/28", Value = "Duplicate")]
    public string Duplicate => this[nameof (Duplicate)];

    /// <summary>word: Content</summary>
    [ResourceEntry("Content", Description = "word: Content", LastModified = "2010/01/28", Value = "Content")]
    public string Content => this[nameof (Content)];

    /// <summary>word: History</summary>
    [ResourceEntry("History", Description = "word: History", LastModified = "2010/01/28", Value = "History")]
    public string History => this[nameof (History)];

    /// <summary>phrase: What do you want to do now?</summary>
    [ResourceEntry("WhatDoYouWantToDoNow", Description = "phrase: What do you want to do now?", LastModified = "2009/01/28", Value = "What do you want to do now?")]
    public string WhatDoYouWantToDoNow => this[nameof (WhatDoYouWantToDoNow)];

    /// <summary>phrase: No content blocks have been created yet</summary>
    [ResourceEntry("NoContentItems", Description = "phrase: No content blocks have been created yet", LastModified = "2011/02/03", Value = "No content blocks have been created yet")]
    public string NoContentItems => this[nameof (NoContentItems)];

    /// <summary>
    /// word: <strong>Edit...</strong>
    /// </summary>
    [ResourceEntry("Edit", Description = "word", LastModified = "2010/01/29", Value = "<strong>Edit...</strong>")]
    public string Edit => this[nameof (Edit)];

    /// <summary>Messsage: Cancel</summary>
    [ResourceEntry("Cancel", Description = "Label for the buttons that cancel the editing/inserting operation.", LastModified = "2010/02/03", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>phrase: More Options</summary>
    [ResourceEntry("MoreOptions", Description = "phrase", LastModified = "2010/02/04", Value = "More options")]
    public string MoreOptions => this[nameof (MoreOptions)];

    /// <summary>phrase: Categories and tags</summary>
    [ResourceEntry("CategoriesAndTags", Description = "phrase", LastModified = "2010/06/30", Value = "Categories and tags")]
    public string CategoriesAndTags => this[nameof (CategoriesAndTags)];

    /// <summary>
    /// phrase: More options <em class="sfNote">(URL, Comments)</em>
    /// </summary>
    [ResourceEntry("MoreOptionsURL", Description = "phrase: More options <em class='sfNote'>(URL, Comments)</em>", LastModified = "2010/03/17", Value = "More options <em class='sfNote'>(URL, Comments)</em>")]
    public string MoreOptionsURL => this[nameof (MoreOptionsURL)];

    /// <summary>phrase: Publication Date</summary>
    [ResourceEntry("PublicationDate", Description = "phrase: Publication Date", LastModified = "2010/02/04", Value = "Publication Date")]
    public string PublicationDate => this[nameof (PublicationDate)];

    /// <summary>Phrase: PublicationDate cannot be empty</summary>
    [ResourceEntry("PublicationDateCannotBeEmpty", Description = "PublicationDate cannot be empty.", LastModified = "2010/03/15", Value = "PublicationDate cannot be empty.")]
    public string PublicationDateCannotBeEmpty => this[nameof (PublicationDateCannotBeEmpty)];

    /// <summary>phrase: Expiration Date</summary>
    [ResourceEntry("ExpirationDate", Description = "phrase: Expiration Date", LastModified = "2010/02/04", Value = "Expiration Date")]
    public string ExpirationDate => this[nameof (ExpirationDate)];

    /// <summary>
    /// Used in the actions menu in the Backend Detail View form
    /// </summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/02/04", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>
    /// Used in the actions menu in the Backend Detail View form
    /// </summary>
    [ResourceEntry("SetPermissions", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/02/04", Value = "Set Permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>
    /// Used in the actions menu in the Backend Detail View form
    /// </summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/02/04", Value = "Revision History")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>Phrase: Title cannot be empty</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "Title cannot be empty", LastModified = "2010/02/04", Value = "Title cannot be empty")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>Phrase: Content cannot be empty</summary>
    [ResourceEntry("ContentCannotBeEmpty", Description = "Content cannot be empty", LastModified = "2010/02/04", Value = "Content cannot be empty")]
    public string ContentCannotBeEmpty => this[nameof (ContentCannotBeEmpty)];

    /// <summary>Phrase: URL</summary>
    [ResourceEntry("UrlName", Description = "URL", LastModified = "2010/02/04", Value = "URL")]
    public string UrlName => this[nameof (UrlName)];

    /// <summary>Phrase: URL cannot be empty</summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "URL cannot be empty", LastModified = "2010/02/04", Value = "URL cannot be empty")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>Word: Status</summary>
    [ResourceEntry("Status", Description = "Word: Status", LastModified = "2010/02/04", Value = "Status")]
    public string Status => this[nameof (Status)];

    /// <summary>Phrase: Publish on:</summary>
    [ResourceEntry("PublishOn", Description = "Phrase: Publish on:", LastModified = "2010/02/04", Value = "Publish on:")]
    public string PublishOn => this[nameof (PublishOn)];

    /// <summary>Phrase: Expires on:</summary>
    [ResourceEntry("ExpiresOn", Description = "Phrase: Expires on:", LastModified = "2010/02/04", Value = "Expires on:")]
    public string ExpiresOn => this[nameof (ExpiresOn)];

    /// <summary>Word: Language</summary>
    [ResourceEntry("Language", Description = "word: Language", LastModified = "2010/10/05", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>word: Owner</summary>
    [ResourceEntry("Owner", Description = "word: Owner", LastModified = "2010/07/21", Value = "Owner")]
    public string Owner => this[nameof (Owner)];

    /// <summary>The title of the edit comment dialog</summary>
    [ResourceEntry("EditComment", Description = "The title of the edit comment dialog.", LastModified = "2010/03/04", Value = "Edit a comment")]
    public string EditComment => this[nameof (EditComment)];

    /// <summary>The author name label</summary>
    [ResourceEntry("AuthorName", Description = "The author name label", LastModified = "2010/03/04", Value = "Name")]
    public string AuthorName => this[nameof (AuthorName)];

    /// <summary>The author email label</summary>
    [ResourceEntry("AuthorEmail", Description = "The author email label", LastModified = "2010/03/04", Value = "Email")]
    public string AuthorEmail => this[nameof (AuthorEmail)];

    /// <summary>The author website label</summary>
    [ResourceEntry("AuthorWebsite", Description = "The author website label.", LastModified = "2010/03/04", Value = "Website")]
    public string AuthorWebsite => this[nameof (AuthorWebsite)];

    /// <summary>IP address</summary>
    [ResourceEntry("IpAddress", Description = "The ip label.", LastModified = "2010/03/04", Value = "IP")]
    public string IpAddress => this[nameof (IpAddress)];

    /// <summary>Block this email</summary>
    [ResourceEntry("BlockThisEmail", Description = "The link for block the email.", LastModified = "2010/03/08", Value = "Block this email")]
    public string BlockThisEmail => this[nameof (BlockThisEmail)];

    /// <summary>Block this IP</summary>
    [ResourceEntry("BlockThisIp", Description = "The link for block the ip.", LastModified = "2010/03/08", Value = "Block this IP")]
    public string BlockThisIp => this[nameof (BlockThisIp)];

    /// <summary>Unblock</summary>
    [ResourceEntry("Unblock", Description = "The link for unblocking the ip or email.", LastModified = "2010/03/08", Value = "Unblock")]
    public string Unblock => this[nameof (Unblock)];

    /// <summary>Blocked</summary>
    [ResourceEntry("Blocked", Description = "The label for marking the ip as blocked.", LastModified = "2010/03/08", Value = "Blocked")]
    public string Blocked => this[nameof (Blocked)];

    /// <summary>Comment</summary>
    [ResourceEntry("Comment", Description = "The 'Comment' label in the create comment form.", LastModified = "2010/03/12", Value = "Comment")]
    public string Comment => this[nameof (Comment)];

    /// <summary>Your Name</summary>
    [ResourceEntry("YourName", Description = "The 'Your Name' label in the create comment form.", LastModified = "2010/03/12", Value = "Your name")]
    public string YourName => this[nameof (YourName)];

    /// <summary>Email</summary>
    [ResourceEntry("Email", Description = "The 'Email' label in the create comment form.", LastModified = "2010/04/23", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>Email</summary>
    [ResourceEntry("EmailOptional", Description = "The 'Email' label in the create comment form.", LastModified = "2011/03/31", Value = "Email <em class='sfNote'>(optional)</em>")]
    public string EmailOptional => this[nameof (EmailOptional)];

    /// <summary>Website</summary>
    [ResourceEntry("Website", Description = "The 'Website' label in the create comment form.", LastModified = "2010/03/12", Value = "Website")]
    public string Website => this[nameof (Website)];

    /// <summary>Word: optional</summary>
    [ResourceEntry("Optional", Description = "Word: optional", LastModified = "2010/03/12", Value = "optional")]
    public string Optional => this[nameof (Optional)];

    /// <summary>Website</summary>
    [ResourceEntry("WebsiteOptional", Description = "The 'Website' label in the create comment form.", LastModified = "2011/03/17", Value = "Website <em class='sfNote'>(optional)</em>")]
    public string WebsiteOptional => this[nameof (WebsiteOptional)];

    /// <summary>Submit</summary>
    [ResourceEntry("Submit", Description = "The text of the submit button in the create comment form.", LastModified = "2010/03/12", Value = "Submit")]
    public string Submit => this[nameof (Submit)];

    /// <summary>GenericContent</summary>
    [ResourceEntry("GeneriContentToolboxSectionTitle", Description = "Title of the generic content toolbox section", LastModified = "2010/02/08", Value = "Generic Content")]
    public string GeneriContentToolboxSectionTitle => this[nameof (GeneriContentToolboxSectionTitle)];

    /// <summary>Title of the generic content toolbox section</summary>
    [ResourceEntry("GenericContentToolboxSectionDescription", Description = "Describes the title of the generic content toolbox section.", LastModified = "2010/02/08", Value = "Title of the generic content toolbox section")]
    public string GenericContentToolboxSectionDescription => this[nameof (GenericContentToolboxSectionDescription)];

    /// <summary>Generic Content View</summary>
    [ResourceEntry("GenericContentViewTitle", Description = "Name of the GenericContentView control in the toolbox", LastModified = "2010/02/08", Value = "Generic content list")]
    public string GenericContentViewTitle => this[nameof (GenericContentViewTitle)];

    /// <summary>Name of the GenericContentView control in the toolbox</summary>
    [ResourceEntry("GenericContentViewDescription", Description = "Describes the generic content view title in the toolbox.", LastModified = "2010/02/08", Value = "Name of the GenericContentView control in the toolbox")]
    public string GenericContentViewDescription => this[nameof (GenericContentViewDescription)];

    /// <summary>Word: Hide</summary>
    [ResourceEntry("Hide", Description = "word: Hide", LastModified = "2010/03/04", Value = "Hide")]
    public string Hide => this[nameof (Hide)];

    /// <summary>Phrase: Hide and mark as spam</summary>
    [ResourceEntry("HideAndMarkAsSpam", Description = "Phrase: HideAndMarkAsSpam", LastModified = "2010/03/04", Value = "Hide and mark as spam")]
    public string HideAndMarkAsSpam => this[nameof (HideAndMarkAsSpam)];

    /// <summary>Phrase: All comments</summary>
    [ResourceEntry("AllComments", Description = "Phrase: All comments", LastModified = "2010/03/04", Value = "All Comments")]
    public string AllComments => this[nameof (AllComments)];

    /// <summary>Word: All comments</summary>
    [ResourceEntry("TodayComments", Description = "Word: Today", LastModified = "2010/03/04", Value = "Today")]
    public string TodayComments => this[nameof (TodayComments)];

    /// <summary>Word: Hidden</summary>
    [ResourceEntry("Hidden", Description = "Word: Hidden", LastModified = "2010/03/04", Value = "Hidden")]
    public string Hidden => this[nameof (Hidden)];

    /// <summary>Word: Published</summary>
    [ResourceEntry("Published", Description = "Word: Published", LastModified = "2010/03/04", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>Word: Spam</summary>
    [ResourceEntry("Spam", Description = "Word: Spam", LastModified = "2010/03/04", Value = "Spam")]
    public string Spam => this[nameof (Spam)];

    /// <summary>Phrase: Edit also...</summary>
    [ResourceEntry("EditAlso", Description = "Phrase: Edit also...", LastModified = "2010/03/04", Value = "Edit also...")]
    public string EditAlso => this[nameof (EditAlso)];

    /// <summary>Phrase: Permissions for this item</summary>
    [ResourceEntry("ItemPermissions", Description = "Phrase: Permissions for this item", LastModified = "2010/03/04", Value = "Permissions for this item")]
    public string ItemPermissions => this[nameof (ItemPermissions)];

    /// <summary>Phrase: Properties for this item</summary>
    [ResourceEntry("ItemProperties", Description = "Phrase: Properties for this item", LastModified = "2010/03/04", Value = "Properties for this item")]
    public string ItemProperties => this[nameof (ItemProperties)];

    /// <summary>Word: Reply</summary>
    [ResourceEntry("Reply", Description = "Word: Reply", LastModified = "2010/03/08", Value = "Reply")]
    public string Reply => this[nameof (Reply)];

    /// <summary>Phrase: Mark as spam</summary>
    [ResourceEntry("MarkAsSpam", Description = "Phrase: Mark as spam", LastModified = "2010/03/08", Value = "Mark as spam")]
    public string MarkAsSpam => this[nameof (MarkAsSpam)];

    /// <summary>Phrase: Mark as favorite</summary>
    [ResourceEntry("MarkAsFavorite", Description = "Phrase: Mark as favorite", LastModified = "2010/03/08", Value = "Mark as favorite")]
    public string MarkAsFavorite => this[nameof (MarkAsFavorite)];

    /// <summary>Phrase: Block IP...</summary>
    [ResourceEntry("BlockIp", Description = "Phrase: BlockIp", LastModified = "2010/03/08", Value = "Block IP...")]
    public string BlockIp => this[nameof (BlockIp)];

    /// <summary>Phrase: Block email...</summary>
    [ResourceEntry("BlockEmail", Description = "Phrase: Block email...", LastModified = "2010/03/08", Value = "Block email...")]
    public string BlockEmail => this[nameof (BlockEmail)];

    /// <summary>Phrase: All content blocks</summary>
    [ResourceEntry("AllItems", Description = "Phrase: All content blocks", LastModified = "2011/02/02", Value = "All content blocks")]
    public string AllItems => this[nameof (AllItems)];

    /// <summary>The text of "My content blocks" sidebar button</summary>
    [ResourceEntry("MyItems", Description = "The text of 'My content blocks' sidebar button", LastModified = "2011/02/01", Value = "My content blocks")]
    public string MyItems => this[nameof (MyItems)];

    /// <summary>
    /// phrase: Content blocks not used on any page or template
    /// </summary>
    [ResourceEntry("NotUsedItems", Description = "The text of 'Content blocks not used on any page or template' sidebar button.", LastModified = "2012/11/26", Value = "Content blocks <strong>not used</strong> on any page or template")]
    public string NotUsedItems => this[nameof (NotUsedItems)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("WaitingForApproval", Description = "The text of the 'Awaiting approval' button in the sidebar.", LastModified = "2010/11/08", Value = "Awaiting approval")]
    public string WaitingForApproval => this[nameof (WaitingForApproval)];

    /// <summary>phrase: Drafts</summary>
    [ResourceEntry("ByDateModified", Description = "The text of the date filter sidebar button.", LastModified = "2010/08/20", Value = "by Date modified...")]
    public string ByDateModified => this[nameof (ByDateModified)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2010/08/20", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>The text of last updated items sidebar button</summary>
    [ResourceEntry("DisplayLastUpdatedItems", Description = "The text of last updated items sidebar button", LastModified = "2010/08/20", Value = "Display items last updated in...")]
    public string DisplayLastUpdatedItems => this[nameof (DisplayLastUpdatedItems)];

    /// <summary>Phrase: AllowComments</summary>
    [ResourceEntry("AllowComments", Description = "Phrase: Allow comments", LastModified = "2010/09/20", Value = "Allow comments")]
    public string AllowComments => this[nameof (AllowComments)];

    /// <summary>phrase: Newest first</summary>
    [ResourceEntry("NewestFirst", Description = "Label text.", LastModified = "2010/04/06", Value = "Newest first")]
    public string NewestFirst => this[nameof (NewestFirst)];

    /// <summary>phrase: Oldest first</summary>
    [ResourceEntry("OldestFirst", Description = "Label text.", LastModified = "2010/04/06", Value = "Oldest first")]
    public string OldestFirst => this[nameof (OldestFirst)];

    /// <summary>phrase: By Title (A-Z)</summary>
    [ResourceEntry("ByTitleAsc", Description = "Label text.", LastModified = "2010/04/06", Value = "By Title (A-Z)")]
    public string ByTitleAsc => this[nameof (ByTitleAsc)];

    /// <summary>phrase: By Title (Z-A)</summary>
    [ResourceEntry("ByTitleDesc", Description = "Label text.", LastModified = "2010/04/06", Value = "By Title (Z-A)")]
    public string ByTitleDesc => this[nameof (ByTitleDesc)];

    /// <summary>phrase: NewModifiedFirst</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2012/01/22", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: By Last Name (A-Z)</summary>
    [ResourceEntry("ByLastNameAZ", Description = "Label text.", LastModified = "2011/03/18", Value = "By Last Name (A-Z)")]
    public string ByLastNameAZ => this[nameof (ByLastNameAZ)];

    /// <summary>phrase: By First Name (A-Z)</summary>
    [ResourceEntry("ByFirstNameAZ", Description = "Label text.", LastModified = "2011/03/18", Value = "By First Name (A-Z)")]
    public string ByFirstNameAZ => this[nameof (ByFirstNameAZ)];

    /// <summary>phrase: New Registered On Top</summary>
    [ResourceEntry("NewRegisteredOnTop", Description = "Label text.", LastModified = "2011/03/18", Value = "New Registered On Top")]
    public string NewRegisteredOnTop => this[nameof (NewRegisteredOnTop)];

    /// <summary>phrase: New Registered On Bottom</summary>
    [ResourceEntry("NewRegisteredOnBottom", Description = "Label text.", LastModified = "2011/03/18", Value = "New Registered On Bottom")]
    public string NewRegisteredOnBottom => this[nameof (NewRegisteredOnBottom)];

    /// <summary>phrase: ByStartDateNewestOnTop</summary>
    [ResourceEntry("ByStartDateNewestOnTop", Description = "ByStartDateNewestOnTop", LastModified = "2011/03/02", Value = "By Start date (newest on top)")]
    public string ByStartDateNewestOnTop => this[nameof (ByStartDateNewestOnTop)];

    /// <summary>phrase: ByStartDateOldestOnTop</summary>
    [ResourceEntry("ByStartDateOldestOnTop", Description = "ByStartDateOldestOnTop", LastModified = "2011/03/02", Value = "By Start date (oldest on top)")]
    public string ByStartDateOldestOnTop => this[nameof (ByStartDateOldestOnTop)];

    /// <summary>phrase: LastPublishedOnTop</summary>
    [ResourceEntry("LastPublishedOnTop", Description = "LastPublishedOnTop", LastModified = "2011/03/02", Value = "Last published on top")]
    public string LastPublishedOnTop => this[nameof (LastPublishedOnTop)];

    /// <summary>phrase: LastModifiedOnTop</summary>
    [ResourceEntry("LastModifiedOnTop", Description = "LastModifiedOnTop", LastModified = "2011/03/02", Value = "Last modified on top")]
    public string LastModifiedOnTop => this[nameof (LastModifiedOnTop)];

    /// <summary>Phrase: Default page</summary>
    [ResourceEntry("DefaultPage", Description = "Phrase: Default page", LastModified = "2010/08/06", Value = "Default page")]
    public string DefaultPage => this[nameof (DefaultPage)];

    /// <summary>Phrase: Name cannot be empty</summary>
    [ResourceEntry("NameCannotBeEmpty", Description = "Name cannot be empty", LastModified = "2010/09/04", Value = "Name cannot be empty")]
    public string NameCannotBeEmpty => this[nameof (NameCannotBeEmpty)];

    /// <summary>Phrase: Email cannot be empty</summary>
    [ResourceEntry("EmailCannotBeEmpty", Description = "Email cannot be empty", LastModified = "2010/09/04", Value = "Email cannot be empty")]
    public string EmailCannotBeEmpty => this[nameof (EmailCannotBeEmpty)];

    /// <summary>Phrase: Website cannot be empty</summary>
    [ResourceEntry("WebSiteCannotBeEmpty", Description = "Phares: Website cannot be empty", LastModified = "2012/01/05", Value = "Website cannot be empty")]
    public string WebSiteCannotBeEmpty => this[nameof (WebSiteCannotBeEmpty)];

    /// <summary>Phrase: Comment cannot be empty</summary>
    [ResourceEntry("CommentCannotBeEmpty", Description = "Comment cannot be empty", LastModified = "2010/09/04", Value = "Comment cannot be empty")]
    public string CommentCannotBeEmpty => this[nameof (CommentCannotBeEmpty)];

    /// <summary>
    /// Phrase: Page not valid. The code you entered is not valid.
    /// </summary>
    [ResourceEntry("PageNotValid", Description = "Phrase: Page not valid. The code you entered is not valid.", LastModified = "2010/09/08", Value = "The code you entered is not valid")]
    public string PageNotValid => this[nameof (PageNotValid)];

    /// <summary>
    /// Phrase: Thank you for the comment! Your comment must be approved first
    /// </summary>
    [ResourceEntry("CommentMessageModeration", Description = "Phrase: Thank you for the comment! Your comment must be approved first", LastModified = "2010/09/10", Value = "Thank you for the comment! Your comment must be approved first")]
    public string CommentMessageModeration => this[nameof (CommentMessageModeration)];

    /// <summary>Phrase: To post comments, you need to login.</summary>
    [ResourceEntry("PostingCommentsRequiresLogin", Description = "Phrase: To post comments, you need to login.", LastModified = "2010/09/10", Value = "To post comments, you need to login.")]
    public string PostingCommentsRequiresLogin => this[nameof (PostingCommentsRequiresLogin)];

    /// <summary>Phrase: You are not allowed to post comments.</summary>
    [ResourceEntry("PostingCommentsIsNotAllowed", Description = "Phrase: You are not allowed to post comments.", LastModified = "2011/01/10", Value = "You are not allowed to post comments.")]
    public string PostingCommentsIsNotAllowed => this[nameof (PostingCommentsIsNotAllowed)];

    /// <summary>Phrase: content item</summary>
    [ResourceEntry("ContentSingularItemName", Description = "Phrase: content item", LastModified = "2010/10/11", Value = "content item")]
    public string ContentSingularItemName => this[nameof (ContentSingularItemName)];

    /// <summary>Phrase: content items</summary>
    [ResourceEntry("ContentPluralItemName", Description = "Phrase: content items", LastModified = "2010/10/11", Value = "content items")]
    public string ContentPluralItemName => this[nameof (ContentPluralItemName)];

    /// <summary>Phrase: content items</summary>
    [ResourceEntry("Name", Description = "Phrase: Name", LastModified = "2010/10/11", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>Phrase: Posted on</summary>
    [ResourceEntry("PostedOn", Description = "Phrase: Posted on", LastModified = "2010/10/11", Value = "Posted on")]
    public string PostedOn => this[nameof (PostedOn)];

    /// <summary>Phrase: Use paging</summary>
    [ResourceEntry("PagingInProcessOfImplementation", Description = "Phrase: Use paging", LastModified = "2011/03/24", Value = "Use paging")]
    public string PagingInProcessOfImplementation => this[nameof (PagingInProcessOfImplementation)];

    /// <summary>Phrase: Divide the list on pages up to</summary>
    [ResourceEntry("DivideListPagesUpTo", Description = "Phrase: Divide the list on pages up to", LastModified = "2010/10/18", Value = "Divide the list on pages up to")]
    public string DivideListPagesUpTo => this[nameof (DivideListPagesUpTo)];

    /// <summary>Phrase: items per page</summary>
    [ResourceEntry("ItemsPerPageLowercase", Description = "Phrase: items per page", LastModified = "2010/10/18", Value = "items per page")]
    public string ItemsPerPageLowercase => this[nameof (ItemsPerPageLowercase)];

    /// <summary>Phrase: Show only limited number of items:</summary>
    [ResourceEntry("ShowOnlyLimitedNumberOfItems", Description = "Phrase: Show only limited number of items:", LastModified = "2010/10/18", Value = "Show only limited number of items:")]
    public string ShowOnlyLimitedNumberOfItems => this[nameof (ShowOnlyLimitedNumberOfItems)];

    /// <summary>Phrase: items in total</summary>
    [ResourceEntry("ItemsInTotalLowercase", Description = "Phrase: items in total", LastModified = "2010/10/18", Value = "items in total")]
    public string ItemsInTotalLowercase => this[nameof (ItemsInTotalLowercase)];

    /// <summary>Phrase: Show all published items at once</summary>
    [ResourceEntry("ShowAllPublisedItemsAtOnce", Description = "Phrase: Show all published items at once", LastModified = "2010/10/18", Value = "Show all published items at once")]
    public string ShowAllPublisedItemsAtOnce => this[nameof (ShowAllPublisedItemsAtOnce)];

    /// <summary>Phrase: No limit and paging</summary>
    [ResourceEntry("NoLimitAndPaging", Description = "Phrase: No limit and paging", LastModified = "2010/10/18", Value = "No limit and paging")]
    public string NoLimitAndPaging => this[nameof (NoLimitAndPaging)];

    /// <summary>Phrase: Use limit</summary>
    [ResourceEntry("UseLimit", Description = "Phrase: Use limit", LastModified = "2010/10/18", Value = "Use limit")]
    public string UseLimit => this[nameof (UseLimit)];

    /// <summary>Phrase: Create new template</summary>
    [ResourceEntry("CreateNewTemplate", Description = "Phrase: Create new template", LastModified = "2011/07/01", Value = "Create new template")]
    public string CreateNewTemplate => this[nameof (CreateNewTemplate)];

    /// <summary>Phrase: Edit Generic content list settings</summary>
    [ResourceEntry("EditGenericContentListSettings", Description = "Phrase: Edit Generic content list settings", LastModified = "2010/11/12", Value = "Edit Generic content list settings")]
    public string EditGenericContentListSettings => this[nameof (EditGenericContentListSettings)];

    /// <summary>Label: Create content</summary>
    [ResourceEntry("CreateContent", Description = "Label: Create content", LastModified = "2010/11/11", Value = "Enter content")]
    public string CreateContent => this[nameof (CreateContent)];

    /// <summary>Word: Edit</summary>
    [ResourceEntry("EditTemplate", Description = "Word: Edit", LastModified = "2010/10/21", Value = "Edit")]
    public string EditTemplate => this[nameof (EditTemplate)];

    /// <summary>Word: Edit</summary>
    [ResourceEntry("EditSelectedTemplate", Description = "phrase: Edit selected template", LastModified = "2011/11/22", Value = "Edit selected template")]
    public string EditSelectedTemplate => this[nameof (EditSelectedTemplate)];

    /// <summary>Phrase: - Select a template -</summary>
    [ResourceEntry("SelectTemplate", Description = "phrase: - Select a template -", LastModified = "2011/12/13", Value = "- Select a template -")]
    public string SelectTemplate => this[nameof (SelectTemplate)];

    /// <summary>Word: Edit</summary>
    [ResourceEntry("EditTemplateDots", Description = "Word: Edit", LastModified = "2011/06/30", Value = "Edit template...")]
    public string EditTemplateDots => this[nameof (EditTemplateDots)];

    /// <summary>Word: Edit selected template</summary>
    [ResourceEntry("EditSelectedTemplateDots", Description = "Word: Edit selected template...", LastModified = "2011/10/24", Value = "Edit selected template...")]
    public string EditSelectedTemplateDots => this[nameof (EditSelectedTemplateDots)];

    /// <summary>phrase: Generic Content data fields</summary>
    [ResourceEntry("GenericContentDataFields", Description = "phrase: Generic Content data fields", LastModified = "2010/11/29", Value = "Generic Content data fields")]
    public string GenericContentDataFields => this[nameof (GenericContentDataFields)];

    /// <summary>
    /// Phrase: All additional URLs redirect to the default one:
    /// </summary>
    [ResourceEntry("AllAditionalUrlsRedirectoToTheDefaultOne", Description = "Phrase: Additional URLs redirect to the default URL", LastModified = "2016/01/28", Value = "Additional URLs redirect to the default URL")]
    public string AllAditionalUrlsRedirectoToTheDefaultOne => this[nameof (AllAditionalUrlsRedirectoToTheDefaultOne)];

    /// <summary>Phrase: Additional URLs (one per line)</summary>
    [ResourceEntry("AdditionalUrlsOnePerLine", Description = "Phrase: Additional URLs (one per line)", LastModified = "2011/01/17", Value = "Additional URLs (one per line)")]
    public string AdditionalUrlsOnePerLine => this[nameof (AdditionalUrlsOnePerLine)];

    /// <summary>Phrase: Additional URLs</summary>
    [ResourceEntry("AdditionalUrls", Description = "Phrase: Additional URLs", LastModified = "2017/08/24", Value = "Additional URLs")]
    public string AdditionalUrls => this[nameof (AdditionalUrls)];

    /// <summary>Phrase: Allow multiple URLs for this item...</summary>
    [ResourceEntry("AllowMultipleURLsForThisItem", Description = "Phrase: Allow multiple URLs for this item...", LastModified = "2011/01/17", Value = "Allow multiple URLs for this item...")]
    public string AllowMultipleURLsForThisItem => this[nameof (AllowMultipleURLsForThisItem)];

    /// <summary>Phrase: Enable multiple URLs for this file...</summary>
    [ResourceEntry("AllowMultipleFileURLsForThisItem", Description = "Phrase: Enable multiple URLs for this file...", LastModified = "2016/01/28", Value = "Enable multiple URLs for this file...")]
    public string AllowMultipleFileURLsForThisItem => this[nameof (AllowMultipleFileURLsForThisItem)];

    /// <summary>Phrase: Additional URLs redirect to the default URL</summary>
    [ResourceEntry("AllAditionalFileUrlsRedirectoToTheDefaultOne", Description = "Phrase: Additional URLs redirect to the default URL", LastModified = "2016/01/28", Value = "Additional URLs redirect to the default URL")]
    public string AllAditionalFileUrlsRedirectoToTheDefaultOne => this[nameof (AllAditionalFileUrlsRedirectoToTheDefaultOne)];

    /// <summary>Phrase: Additional URLs</summary>
    [ResourceEntry("AdditionalFileUrls", Description = "Phrase: Additional URLs", LastModified = "2016/01/28", Value = "Additional URLs")]
    public string AdditionalFileUrls => this[nameof (AdditionalFileUrls)];

    /// <summary>phrase: Filter and edit...</summary>
    [ResourceEntry("FilterAndEdit", Description = "phrase: Filter and edit...", LastModified = "2011/01/17", Value = "Filter and edit...")]
    public string FilterAndEdit => this[nameof (FilterAndEdit)];

    /// <summary>Phrase: URL contains invalid symbols</summary>
    [ResourceEntry("UrlNameInvalidSymbols", Description = "The message shown when the url contains invalid symbols.", LastModified = "2010/07/13", Value = "The URL contains invalid symbols.")]
    public string UrlNameInvalidSymbols => this[nameof (UrlNameInvalidSymbols)];

    /// <summary>phrase: Select from existing shared content</summary>
    [ResourceEntry("SelectFromExistingSharedContent", Description = "phrase: Select from existing shared content", LastModified = "2011/01/26", Value = "Select from existing shared content")]
    public string SelectFromExistingSharedContent => this[nameof (SelectFromExistingSharedContent)];

    /// <summary>phrase: Change shared content</summary>
    [ResourceEntry("ChangeSharedContent", Description = "phrase: Change shared content", LastModified = "2013/07/16", Value = "Change shared content")]
    public string ChangeSharedContent => this[nameof (ChangeSharedContent)];

    /// <summary>phrase: Share this content across pages</summary>
    [ResourceEntry("ShareThisContentAcrossPages", Description = "phrase: Share this content across pages", LastModified = "2011/01/26", Value = "Share this content across pages")]
    public string ShareThisContentAcrossPages => this[nameof (ShareThisContentAcrossPages)];

    /// <summary>phrase: Select shared content</summary>
    [ResourceEntry("SelectSharedContentYouWantToUse", Description = "phrase: Select shared content", LastModified = "2012/11/30", Value = "Select shared content")]
    public string SelectSharedContentYouWantToUse => this[nameof (SelectSharedContentYouWantToUse)];

    /// <summary>phrase: Share this content</summary>
    [ResourceEntry("ShareThisContent", Description = "phrase: Share this content", LastModified = "2011/01/26", Value = "Share this content")]
    public string ShareThisContent => this[nameof (ShareThisContent)];

    /// <summary>phrase: Yes, Unshare this content</summary>
    [ResourceEntry("UnshareThisContent", Description = "phrase: Yes, Unshare this content", LastModified = "2011/01/26", Value = "Yes, Unshare this content")]
    public string UnshareThisContent => this[nameof (UnshareThisContent)];

    /// <summary>phrase: Shared content</summary>
    [ResourceEntry("SharedContent", Description = "phrase:Shared content", LastModified = "2011/01/26", Value = "Shared content")]
    public string SharedContent => this[nameof (SharedContent)];

    /// <summary>
    /// phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to Unshare this content?
    /// </summary>
    [ResourceEntry("AreYouSureYouWantToUnshareThisContent", Description = "phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to Unshare this content?", LastModified = "2012/01/05", Value = "<p>This content will not be shared anymore. The changes you make will not affect other pages.</p><p>Are you sure you want to Unshare this content?</p>")]
    public string AreYouSureYouWantToUnshareThisContent => this[nameof (AreYouSureYouWantToUnshareThisContent)];

    /// <summary>phrase: Edit this content</summary>
    [ResourceEntry("EditThisContent", Description = "phrase:Edit this content", LastModified = "2011/01/27", Value = "Edit this content")]
    public string EditThisContent => this[nameof (EditThisContent)];

    /// <summary>word: Unshare</summary>
    [ResourceEntry("Unshare", Description = "word:Unshare", LastModified = "2011/01/27", Value = "Unshare")]
    public string Unshare => this[nameof (Unshare)];

    /// <summary>
    /// phrase: This content is shared. Any changes will be reflected everywhere it is shared
    /// </summary>
    [ResourceEntry("ThisContentIsShared", Description = "phrase:This content is shared. Any changes will be reflected everywhere it is shared", LastModified = "2012/11/15", Value = "<strong>This content is shared.</strong><br />Any changes will be reflected everywhere it is shared")]
    public string ThisContentIsShared => this[nameof (ThisContentIsShared)];

    /// <summary>phrase: View affected pages and templates</summary>
    [ResourceEntry("ViewAffectedPages", Description = "phrase:View affected pages and templates", LastModified = "2012/11/15", Value = "View affected pages and templates")]
    public string ViewAffectedPages => this[nameof (ViewAffectedPages)];

    /// <summary>
    /// Title for the filtering section of content blocks per pages
    /// </summary>
    [ResourceEntry("ContentBlocksPerPages", Description = "Title for the filtering section of content blocks per pages", LastModified = "2011/02/01", Value = "Content blocks per pages")]
    public string ContentBlocksPerPages => this[nameof (ContentBlocksPerPages)];

    /// <summary>word: More</summary>
    [ResourceEntry("More", Description = "word: More", LastModified = "2011/02/01", Value = "More")]
    public string More => this[nameof (More)];

    /// <summary>word: Less</summary>
    [ResourceEntry("Less", Description = "word: Less", LastModified = "2011/02/01", Value = "Less")]
    public string Less => this[nameof (Less)];

    /// <summary>Phrase: Date / Owner</summary>
    [ResourceEntry("DateOwner", Description = "Phrase: Date / Owner", LastModified = "2011/02/02", Value = "Date / Owner")]
    public string DateOwner => this[nameof (DateOwner)];

    /// <summary>Phrase: Used on</summary>
    [ResourceEntry("UsedOn", Description = "Phrase: Used on", LastModified = "2011/02/02", Value = "Used in pages")]
    public string UsedOn => this[nameof (UsedOn)];

    /// <summary>label: uses this content</summary>
    [ResourceEntry("UsesContent", Description = "label", LastModified = "2012/11/14", Value = " uses this content")]
    public string UsesContent => this[nameof (UsesContent)];

    /// <summary>label: use this content</summary>
    [ResourceEntry("UseContent", Description = "label", LastModified = "2012/11/14", Value = " use this content")]
    public string UseContent => this[nameof (UseContent)];

    /// <summary>
    /// label: Any changes in this content will be reflected on all pages and templates where it is used
    /// </summary>
    [ResourceEntry("ContentPagesDialogDescription", Description = "label", LastModified = "2012/11/14", Value = "Any changes in this content will be reflected on all pages and templates where it is used")]
    public string ContentPagesDialogDescription => this[nameof (ContentPagesDialogDescription)];

    /// <summary>label: Content successfully updated in {0}.</summary>
    [ResourceEntry("SuccessfullyUpdatedContent", Description = "label: Content successfully updated in {0}.", LastModified = "2012/11/26", Value = "Content successfully updated in {0}.")]
    public string SuccessfullyUpdatedContent => this[nameof (SuccessfullyUpdatedContent)];

    /// <summary>phrase: Locked by {0}</summary>
    [ResourceEntry("LockedByFormat", Description = "phrase: Locked by {0}", LastModified = "2011/02/07", Value = "Locked by {0}")]
    public string LockedByFormat => this[nameof (LockedByFormat)];

    /// <summary>word: Shared</summary>
    [ResourceEntry("Shared", Description = "word: Shared", LastModified = "2011/02/15", Value = "<span class='sfShared'>Shared</span>")]
    public string Shared => this[nameof (Shared)];

    /// <summary>phrase: There is a newer version of this content</summary>
    [ResourceEntry("ThereIsNewerVersionOfThisContent", Description = "phrase: There is a newer version of this content", LastModified = "2011/02/15", Value = "There is a newer version of this content")]
    public string ThereIsNewerVersionOfThisContent => this[nameof (ThereIsNewerVersionOfThisContent)];

    /// <summary>phrase: with the newer version</summary>
    [ResourceEntry("WithTheNewerVersion", Description = "phrase: with the newer version", LastModified = "2011/02/15", Value = "with the newer version")]
    public string WithTheNewerVersion => this[nameof (WithTheNewerVersion)];

    /// <summary>phrase: This content is shared across</summary>
    [ResourceEntry("ThisContentIsSharedAccross", Description = "phrase: This content is shared across", LastModified = "2011/02/15", Value = "This content is <strong>shared</strong> across")]
    public string ThisContentIsSharedAccross => this[nameof (ThisContentIsSharedAccross)];

    /// <summary>
    /// Confirmation message: If you delete this content block it will be removed from all pages and templates where it is used. Are you sure you want to delete this content block?
    /// </summary>
    [ResourceEntry("DeleteSingleConfirmationMessage", Description = "Confirmation message: If you delete this content block it will be removed from all pages and templates where it is used. Are you sure you want to delete this content block??", LastModified = "2012/11/13", Value = "If you delete this content block it <b>will be removed from all pages and templates</b> where it is used. </br> Are you sure you want to delete this content block?")]
    public string DeleteSingleConfirmationMessage => this[nameof (DeleteSingleConfirmationMessage)];

    /// <summary>
    /// Confirmation message: If you delete these content blocks they <b>will be removed from all pages and templates</b> where they are used. Are you sure you want to delete these content blocks?
    /// </summary>
    [ResourceEntry("DeleteMultipleConfirmationMessage", Description = "If you delete these content blocks they <b>will be removed from all pages and templates</b> where they are used. Are you sure you want to delete these content blocks?", LastModified = "2012/11/13", Value = "If you delete these content blocks they <b>will be removed from all pages and templates</b> where they are used. </br> Are you sure you want to delete these content blocks?")]
    public string DeleteMultipleConfirmationMessage => this[nameof (DeleteMultipleConfirmationMessage)];

    /// <summary>phrase: (A-Z)</summary>
    [ResourceEntry("AZ", Description = "phrase: (A-Z) alphabetical sorting", LastModified = "2011/06/06", Value = "(A-Z)")]
    public string AZ => this[nameof (AZ)];

    /// <summary>phrase: (Z-A)</summary>
    [ResourceEntry("ZA", Description = "phrase: (Z-A) alphabetical sorting", LastModified = "2011/06/06", Value = "(Z-A)")]
    public string ZA => this[nameof (ZA)];

    /// <summary>
    /// Title of the column showing whether a content block is used on any page or template (shown in content blocks grid)
    /// </summary>
    /// <value>Translated version the default string 'Used'</value>
    [ResourceEntry("ContentBlocksUsedColumnHeaderText", Description = "Title of the column showing whether a content block is used on any page or template (shown in content blocks grid)", LastModified = "2012/11/13", Value = "Used")]
    public string ContentBlocksUsedColumnHeaderText => this[nameof (ContentBlocksUsedColumnHeaderText)];

    /// <summary>Word: Templates.</summary>
    [ResourceEntry("Templates", Description = "Templates", LastModified = "2012/11/14", Value = "Templates")]
    public string Templates => this[nameof (Templates)];

    /// <summary>{0} templates</summary>
    [ResourceEntry("TemplatesCount", Description = "The templates count displayed in the content blocks grid.", LastModified = "2012/11/13", Value = "{0} templates")]
    public string TemplatesCount => this[nameof (TemplatesCount)];

    /// <summary>1 template</summary>
    [ResourceEntry("TemplateCount", Description = "The template count displayed in the content blocks grid", LastModified = "2012/11/13", Value = "1 template")]
    public string TemplateCount => this[nameof (TemplateCount)];

    /// <summary>
    /// phrase: You are not allowed to see this content. Contact administrator for more information
    /// </summary>
    [ResourceEntry("NoViewPermissionsMessage", Description = "phrase: You are not allowed to see this content. Contact administrator for more information", LastModified = "2012/11/20", Value = "You are not allowed to see this content. Contact administrator for more information")]
    public string NoViewPermissionsMessage => this[nameof (NoViewPermissionsMessage)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in content blocks", LastModified = "2012/04/12", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/22", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: NewCreatedFirst</summary>
    [ResourceEntry("NewCreatedFirst", Description = "Label text.", LastModified = "2013/01/22", Value = "Last created on top")]
    public string NewCreatedFirst => this[nameof (NewCreatedFirst)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2013/01/22", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>label: uses this group</summary>
    [ResourceEntry("UseThisGroupSingle", Description = "label", LastModified = "2013/02/01", Value = " uses this group")]
    public string UseThisGroupSingle => this[nameof (UseThisGroupSingle)];

    /// <summary>label: use this group</summary>
    [ResourceEntry("UseThisGroupMultiple", Description = "label", LastModified = "2013/02/01", Value = " use this group")]
    public string UseThisGroupMultiple => this[nameof (UseThisGroupMultiple)];

    /// <summary>phrase: Content Blocks (shared)</summary>
    /// <value>Content Blocks (shared)</value>
    [ResourceEntry("ContentBlocksSharedTitle", Description = "phrase: Content Blocks (shared)", LastModified = "2013/07/02", Value = "Content Blocks (shared)")]
    public string ContentBlocksSharedTitle => this[nameof (ContentBlocksSharedTitle)];

    /// <summary>phrase: Content blocks</summary>
    /// <value>Content blocks</value>
    [ResourceEntry("ContentBlocksPluralTitle", Description = "phrase: Content blocks", LastModified = "2020/12/15", Value = "Content blocks")]
    public string ContentBlocksPluralTitle => this[nameof (ContentBlocksPluralTitle)];

    /// <summary>phrase: Content block</summary>
    /// <value>Content block</value>
    [ResourceEntry("ContentBlocksSingularTitle", Description = "phrase: Content block", LastModified = "2020/12/15", Value = "Content block")]
    public string ContentBlocksSingularTitle => this[nameof (ContentBlocksSingularTitle)];

    /// <summary>
    /// phrase: Content blocks will be removed from all pages and templates. This may result in broken pages on your site.
    /// </summary>
    /// <value>Content block</value>
    [ResourceEntry("ContentBlocksDeleteWarningMessagePlural", Description = "phrase: Content blocks will be removed from all pages and templates. This may result in broken pages on your site.", LastModified = "2021/01/27", Value = "Content blocks will be removed from all pages and templates. This may result in broken pages on your site.")]
    public string ContentBlocksDeleteWarningMessagePlural => this[nameof (ContentBlocksDeleteWarningMessagePlural)];

    /// <summary>
    /// phrase: This content block will be removed from all pages and templates. This may result in broken pages on your site.
    /// </summary>
    /// <value>Content block</value>
    [ResourceEntry("ContentBlocksDeleteWarningMessageSingular", Description = "phrase: This content block will be removed from all pages and templates. This may result in broken pages on your site.", LastModified = "2021/01/27", Value = "This content block will be removed from all pages and templates. This may result in broken pages on your site.")]
    public string ContentBlocksDeleteWarningMessageSingular => this[nameof (ContentBlocksDeleteWarningMessageSingular)];

    /// <summary>phrase: Select content types</summary>
    /// <value>Select content types</value>
    [ResourceEntry("SelectContentTypes", Description = "phrase: Select content types", LastModified = "2013/09/13", Value = "Select content types")]
    public string SelectContentTypes => this[nameof (SelectContentTypes)];

    /// <summary>ItemsGrid pager text format</summary>
    /// <value>{0} to {1} from {2} items</value>
    [ResourceEntry("ItemsGridPagerTextFormat", Description = "ItemsGrid pager text format", LastModified = "2014/01/07", Value = "{0} to {1} from {2} items")]
    public string ItemsGridPagerTextFormat => this[nameof (ItemsGridPagerTextFormat)];

    /// <summary>
    /// Gets External Link: Working with sitefinity content blocks
    /// </summary>
    [ResourceEntry("ExternalLinkWorkingWithSitefinityContentBlocks", Description = "External Link: Working with sitefinity content blocks", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/content-blocks")]
    public string ExternalLinkWorkingWithSitefinityContentBlocks => this[nameof (ExternalLinkWorkingWithSitefinityContentBlocks)];

    /// <summary>
    /// Gets External Link: Working with sitefinity content blocks widgets
    /// </summary>
    [ResourceEntry("ExternalLinkWorkingWithSitefinityContentBlocksWidgets", Description = "External Link: Working with sitefinity content blocks widgets", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/content-block-widget-mvc")]
    public string ExternalLinkWorkingWithSitefinityContentBlocksWidgets => this[nameof (ExternalLinkWorkingWithSitefinityContentBlocksWidgets)];

    /// <summary>The authors name label</summary>
    /// <value>Authors</value>
    [ResourceEntry("Authors", Description = "The authors name label", LastModified = "2018/02/23", Value = "Authors")]
    public string Authors => this[nameof (Authors)];

    /// <summary>phrase: Creator</summary>
    /// <value>Creator</value>
    [ResourceEntry("Creator", Description = "phrase: Creator", LastModified = "2018/02/23", Value = "Creator")]
    public string Creator => this[nameof (Creator)];

    /// <summary>phrase: Date modified</summary>
    /// <value>Date modified</value>
    [ResourceEntry("DateModified", Description = "phrase: Date modified", LastModified = "2018/02/23", Value = "Date modified")]
    public string DateModified => this[nameof (DateModified)];

    /// <summary>phrase: Date range</summary>
    /// <value>Date range</value>
    [ResourceEntry("DateRange", Description = "phrase: Date range", LastModified = "2018/02/23", Value = "Date range")]
    public string DateRange => this[nameof (DateRange)];

    /// <summary>Gets External Link: Work with content blocks</summary>
    [ResourceEntry("ExternalLinkWorkWithContentBlocks", Description = "External Link: Work with content blocks", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/content-blocks")]
    public string ExternalLinkWorkWithContentBlocks => this[nameof (ExternalLinkWorkWithContentBlocks)];

    /// <summary>Gets External Link: Add content blocks to pages</summary>
    [ResourceEntry("ExternalLinkAddContentBlocks", Description = "External Link: Add content blocks to pages", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/content-block-widget-mvc")]
    public string ExternalLinkAddContentBlocks => this[nameof (ExternalLinkAddContentBlocks)];

    /// <summary>Gets External Link Label: Work with content blocks</summary>
    [ResourceEntry("ExternalLinkLabelWorkWithContentBlocks", Description = "External Link Label: Work with content blocks", LastModified = "2018/08/29", Value = "Work with content blocks")]
    public string ExternalLinkLabelWorkWithContentBlocks => this[nameof (ExternalLinkLabelWorkWithContentBlocks)];

    /// <summary>Gets External Link Label: Add content blocks to pages</summary>
    [ResourceEntry("ExternalLinkLabelAddContentBlocks", Description = "External Link Label: Add content blocks to pages", LastModified = "2018/08/29", Value = "Add content blocks to pages")]
    public string ExternalLinkLabelAddContentBlocks => this[nameof (ExternalLinkLabelAddContentBlocks)];

    /// <summary>Content block with the same title already exists.</summary>
    /// <value>Content block with the same title already exists.</value>
    [ResourceEntry("ContentBlockDuplicateTitleErrorMessage", Description = "Content block with the same title already exists.", LastModified = "2020/01/18", Value = "Content block with the same title already exists.")]
    public string ContentBlockDuplicateTitleErrorMessage => this[nameof (ContentBlockDuplicateTitleErrorMessage)];

    /// <summary>Label: Learn how to...</summary>
    [ResourceEntry("LearnHowTo", Description = "Label: Learn how to...", LastModified = "2018/08/29", Value = "Learn how to...")]
    public string LearnHowTo => this[nameof (LearnHowTo)];

    /// <summary>
    /// Phrase: Allow external search engines to index this content and include in Sitemap
    /// </summary>
    [ResourceEntry("IncludeInSitemap", Description = "Phrase: Allow external search engines to index this content and include in Sitemap", LastModified = "2020/01/22", Value = "Allow external search engines to index this content and include in Sitemap")]
    public string IncludeInSitemap => this[nameof (IncludeInSitemap)];
  }
}
