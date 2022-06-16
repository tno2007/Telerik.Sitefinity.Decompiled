// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.CommentsResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Comments
{
  /// <summary>Localizable strings for the News module</summary>
  [ObjectInfo(typeof (CommentsResources), Description = "CommentsResourcesDescription", Title = "CommentsResourcesTitle")]
  internal class CommentsResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Comments.CommentsResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public CommentsResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Comments.CommentsResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public CommentsResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Resources for Comments</summary>
    [ResourceEntry("CommentsResourcesTitle", Description = "The title of this class.", LastModified = "2013/08/13", Value = "Comments module labels")]
    public string CommentsResourcesTitle => this[nameof (CommentsResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("CommentsResourcesDescription", Description = "The description of this class.", LastModified = "2013/08/13", Value = "Contains localizable resources for security user interface.")]
    public string CommentsResourcesDescription => this[nameof (CommentsResourcesDescription)];

    /// <summary>Comments class plural</summary>
    [ResourceEntry("CommentsPluralTypeName", Description = "Comments class plural", LastModified = "2013/08/13", Value = "Comments")]
    public string CommentsPluralTypeName => this[nameof (CommentsPluralTypeName)];

    /// <summary>Comments class singular</summary>
    [ResourceEntry("CommentsSingleTypeName", Description = "Comments class singular", LastModified = "2013/08/13", Value = "Comment")]
    public string CommentsSingleTypeName => this[nameof (CommentsSingleTypeName)];

    /// <summary>Comments</summary>
    [ResourceEntry("Comments", Description = "Word: Comments", LastModified = "2013/08/14", Value = "Comment")]
    public string Comments => this[nameof (Comments)];

    /// <summary>Date / Status /  Comment</summary>
    [ResourceEntry("CommentsGridCommentCol", Description = "Phrase: Date / Status /  Comment", LastModified = "2013/08/19", Value = "Date / Status /  Comment")]
    public string CommentsGridCommentCol => this[nameof (CommentsGridCommentCol)];

    /// <summary>Author / Email / IP</summary>
    [ResourceEntry("CommentsGridAuthorCol", Description = "Phrase: Author / Email / IP", LastModified = "2013/08/19", Value = "Author / Email / IP")]
    public string CommentsGridAuthorCol => this[nameof (CommentsGridAuthorCol)];

    /// <summary>Commented item</summary>
    [ResourceEntry("CommentsGridItemCol", Description = "Phrase: Commented item", LastModified = "2013/08/19", Value = "Commented item")]
    public string CommentsGridItemCol => this[nameof (CommentsGridItemCol)];

    /// <summary>Spam</summary>
    [ResourceEntry("Spam", Description = "Phrase: Spam", LastModified = "2013/08/19", Value = "Spam")]
    public string Spam => this[nameof (Spam)];

    /// <summary>WaitingApproval</summary>
    [ResourceEntry("WaitingApproval", Description = "Phrase: Waiting for approval", LastModified = "2013/08/19", Value = "Waiting for approval")]
    public string WaitingApproval => this[nameof (WaitingApproval)];

    /// <summary>WaitingApproval</summary>
    [ResourceEntry("Published", Description = "Phrase: Published", LastModified = "2013/08/19", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>Hidden</summary>
    [ResourceEntry("Hidden", Description = "Phrase: Hidden", LastModified = "2013/08/19", Value = "Hidden")]
    public string Hidden => this[nameof (Hidden)];

    /// <summary>Hide</summary>
    [ResourceEntry("Hide", Description = "Phrase: Hide", LastModified = "2013/08/19", Value = "Hide")]
    public string Hide => this[nameof (Hide)];

    /// <summary>Unblock IP</summary>
    [ResourceEntry("UnblockIp", Description = "Phrase: Unblock IP", LastModified = "2013/08/19", Value = "Unblock IP")]
    public string UnblockIp => this[nameof (UnblockIp)];

    /// <summary>Unmark as Spam</summary>
    [ResourceEntry("UnmarkSpam", Description = "Phrase: Unmark as Spam", LastModified = "2013/08/19", Value = "Unmark as Spam")]
    public string UnmarkSpam => this[nameof (UnmarkSpam)];

    /// <summary>Reply to comment</summary>
    [ResourceEntry("ReplyComment", Description = "Phrase: Reply to comment", LastModified = "2013/08/19", Value = "Reply to comment")]
    public string ReplyComment => this[nameof (ReplyComment)];

    /// <summary>Mark as spam</summary>
    [ResourceEntry("MarkSpam", Description = "Phrase: Mark as spam", LastModified = "2013/08/19", Value = "Mark as spam")]
    public string MarkSpam => this[nameof (MarkSpam)];

    /// <summary>Manage comments</summary>
    [ResourceEntry("ManageComments", Description = "Phrase: Manage comments", LastModified = "2013/08/19", Value = "Manage comments")]
    public string ManageComments => this[nameof (ManageComments)];

    /// <summary>Filter comments</summary>
    [ResourceEntry("FilterComments", Description = "Phrase:Filter comments", LastModified = "2013/08/19", Value = "Filter comments")]
    public string FilterComments => this[nameof (FilterComments)];

    /// <summary>All comments</summary>
    [ResourceEntry("AllComments", Description = "Phrase:All comments", LastModified = "2013/08/19", Value = "All comments")]
    public string AllComments => this[nameof (AllComments)];

    /// <summary>Comments on my items</summary>
    [ResourceEntry("MyComments", Description = "Phrase:Comments on my items", LastModified = "2013/08/19", Value = "Comments on my items")]
    public string MyComments => this[nameof (MyComments)];

    /// <summary>Other filter options</summary>
    [ResourceEntry("OtherFilterOptions", Description = "Phrase:Other filter options", LastModified = "2013/08/19", Value = "Other filter options")]
    public string OtherFilterOptions => this[nameof (OtherFilterOptions)];

    /// <summary>Display comments in...</summary>
    [ResourceEntry("DisplayCommentsIn", Description = "Phrase:Display comments in...", LastModified = "2013/08/19", Value = "Display comments in...")]
    public string DisplayCommentsIn => this[nameof (DisplayCommentsIn)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2013/08/20", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>phrase: by Date modified...</summary>
    [ResourceEntry("ByDateModified", Description = "The text of the date filter sidebar button.", LastModified = "2013/08/20", Value = "by Date modified...")]
    public string ByDateModified => this[nameof (ByDateModified)];

    /// <summary>phrase: Settings for comments.</summary>
    [ResourceEntry("Settings", Description = "phrase: Settings for comments", LastModified = "2013/08/20", Value = "Settings for comments")]
    public string Settings => this[nameof (Settings)];

    /// <summary>phrase: Custom Fields for comments.</summary>
    [ResourceEntry("CustomFields", Description = "phrase: Custom Fields for comments", LastModified = "2013/08/20", Value = "Custom Fields for comments")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>Control name: CommentsWidget</summary>
    [ResourceEntry("CommentsTitle", Description = "Control title: Comments", LastModified = "2013/08/22", Value = "Comments")]
    public string CommentsTitle => this[nameof (CommentsTitle)];

    /// <summary>
    /// Control description: A widget that shows the comments in the particular thread.
    /// </summary>
    [ResourceEntry("CommentsDescription", Description = "Control description: A widget that shows the comments in the particular thread.", LastModified = "2013/08/22", Value = "A widget that shows the comments in the particular thread.")]
    public string CommentsDescription => this[nameof (CommentsDescription)];

    /// <summary>Phrase: Access Comments module</summary>
    /// <value>Access Comments module</value>
    [ResourceEntry("AccessCommentsTitle", Description = "Title of the 'AccessCommentsTitle' security action", LastModified = "2013/08/26", Value = "Access Comments")]
    public string AccessCommentsTitle => this[nameof (AccessCommentsTitle)];

    /// <summary>
    /// Phrase: Security action that allows access to the backend pages of the Comments module
    /// </summary>
    /// <value>Security action that allows access to the backend pages of the Comments module</value>
    [ResourceEntry("AccessCommentsDescription", Description = "Description of the 'AccessCommentsDescription' security action", LastModified = "2013/08/26", Value = "Security action that allows access to the backend pages of the Comments module")]
    public string AccessCommentsDescription => this[nameof (AccessCommentsDescription)];

    /// <summary>phrase: by Content type...</summary>
    [ResourceEntry("ByContentType", Description = "The text of the content filter sidebar button.", LastModified = "2013/08/26", Value = "by Content type...")]
    public string ByContentType => this[nameof (ByContentType)];

    /// <summary>Phrase: Close content types</summary>
    [ResourceEntry("CloseContentFilter", Description = "The link for closing the content types filter widget in the sidebar.", LastModified = "2013/08/20", Value = "Close content types")]
    public string CloseContentFilter => this[nameof (CloseContentFilter)];

    /// <summary>phrase: Comments by content type</summary>
    [ResourceEntry("ByContentTypeHeader", Description = "The title of the comments filter widget.", LastModified = "2013/08/20", Value = "Comments by content type")]
    public string ByContentTypeHeader => this[nameof (ByContentTypeHeader)];

    /// <summary>Inappropriate</summary>
    [ResourceEntry("Inappropriate", Description = "Phrase: Inappropriate", LastModified = "2013/08/19", Value = "Inappropriate")]
    public string Inappropriate => this[nameof (Inappropriate)];

    /// <summary>Back to all Comments</summary>
    [ResourceEntry("BackToComments", Description = "Phrase: Back to all Comments", LastModified = "2013/08/28", Value = "Back to all Comments")]
    public string BackToComments => this[nameof (BackToComments)];

    /// <summary>Allow comments for this item</summary>
    [ResourceEntry("AllowComments", Description = "Phrase: Allow comments for this item", LastModified = "2013/08/28", Value = "Allow comments for this item")]
    public string AllowComments => this[nameof (AllowComments)];

    /// <summary>Close comments for this item</summary>
    [ResourceEntry("CloseComments", Description = "Phrase:  Close comments for this item", LastModified = "2013/08/28", Value = " Close comments for this item")]
    public string CloseComments => this[nameof (CloseComments)];

    /// <summary>Do not require approval</summary>
    [ResourceEntry("DoNotRequireApproval", Description = "Phrase: Do not require approval", LastModified = "2013/08/28", Value = "Do not require approval")]
    public string DoNotRequireApproval => this[nameof (DoNotRequireApproval)];

    /// <summary>Require approval</summary>
    [ResourceEntry("RequireApproval", Description = "Phrase: Require approval", LastModified = "2013/08/28", Value = "Require approval")]
    public string RequireApproval => this[nameof (RequireApproval)];

    /// <summary>Do not require authorization</summary>
    [ResourceEntry("DoNotRequireAuthorizationCaption", Description = "Phrase: Back to all Comments", LastModified = "2013/08/28", Value = "Do not require authorization")]
    public string DoNotRequireAuthorizationCaption => this[nameof (DoNotRequireAuthorizationCaption)];

    /// <summary>Require authorization</summary>
    [ResourceEntry("RequireAuthorizationCaption", Description = "Phrase: Require authorization", LastModified = "2013/08/28", Value = "Require authorization")]
    public string RequireAuthorizationCaption => this[nameof (RequireAuthorizationCaption)];

    /// <summary>Comments options</summary>
    [ResourceEntry("CommentsOptions", Description = "Phrase: Comments options", LastModified = "2013/08/28", Value = "Comments options")]
    public string CommentsOptions => this[nameof (CommentsOptions)];

    /// <summary>Edit comment title</summary>
    [ResourceEntry("EditComment", Description = "Edit comment dialog title", LastModified = "2013/09/03", Value = "Edit comment")]
    public string EditComment => this[nameof (EditComment)];

    /// <summary>Name text</summary>
    [ResourceEntry("Name", Description = "word: Name", LastModified = "2013/09/03", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>Email text</summary>
    [ResourceEntry("Email", Description = "word: Email", LastModified = "2013/09/03", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>Posted on text</summary>
    [ResourceEntry("PostedOn", Description = "phrase: Posted on", LastModified = "2013/09/03", Value = "Posted on")]
    public string PostedOn => this[nameof (PostedOn)];

    /// <summary>IP text</summary>
    [ResourceEntry("IP", Description = "phrase: IP address", LastModified = "2013/09/03", Value = "IP")]
    public string IP => this[nameof (IP)];

    /// <summary>Mark as inappropriate</summary>
    [ResourceEntry("MarkAsInappropriate", Description = "phrase: Mark as inappropriate", LastModified = "2013/09/03", Value = "Mark as inappropriate")]
    public string MarkAsInappropriate => this[nameof (MarkAsInappropriate)];

    /// <summary>Unmark as inappropriate</summary>
    [ResourceEntry("UnmarkAsInappropriate", Description = "phrase: Unmark as inappropriate", LastModified = "2013/09/03", Value = "Unmark as inappropriate")]
    public string UnmarkAsInappropriate => this[nameof (UnmarkAsInappropriate)];

    /// <summary>Posted days ago</summary>
    [ResourceEntry("PostedDaysAgo", Description = "phrase: Posted {0} days ago", LastModified = "2013/09/03", Value = "Posted {0} days ago")]
    public string PostedDaysAgo => this[nameof (PostedDaysAgo)];

    /// <summary>Posted days ago</summary>
    [ResourceEntry("PostedHoursAgo", Description = "phrase: Posted {0} hours ago", LastModified = "2013/09/03", Value = "Posted {0} hours ago")]
    public string PostedHoursAgo => this[nameof (PostedHoursAgo)];

    /// <summary>Posted days ago</summary>
    [ResourceEntry("PostedMinutesAgo", Description = "phrase: Posted {0} minutes ago", LastModified = "2013/09/03", Value = "Posted {0} minutes ago")]
    public string PostedMinutesAgo => this[nameof (PostedMinutesAgo)];

    /// <summary>All {0} comments</summary>
    [ResourceEntry("CommentsCountBackend", Description = "phrase: All {0} comments", LastModified = "2013/09/09", Value = "All {0} comments")]
    public string CommentsCountBackend => this[nameof (CommentsCountBackend)];

    /// <summary>View item</summary>
    [ResourceEntry("ViewItem", Description = "phrase: View item", LastModified = "2013/09/09", Value = "View item")]
    public string ViewItem => this[nameof (ViewItem)];

    /// <summary>phrase: No comments have been submitted yet.</summary>
    [ResourceEntry("NoComments", Description = "phrase: No comments have been submitted yet", LastModified = "2013/09/10", Value = "No comments have been submitted yet")]
    public string NoComments => this[nameof (NoComments)];

    /// <summary>phrase: Comment settings.</summary>
    [ResourceEntry("SettingForComments", Description = "phrase: Comment settings", LastModified = "2013/09/10", Value = "Comment settings")]
    public string SettingForComments => this[nameof (SettingForComments)];

    /// <summary>phrase: Anonymous user</summary>
    [ResourceEntry("AnonymousUser", Description = "phrase: Anonymous user", LastModified = "2013/09/13", Value = "Anonymous user")]
    public string AnonymousUser => this[nameof (AnonymousUser)];

    /// <summary>phrase: Page Comments</summary>
    [ResourceEntry("PageComments", Description = "phrase: Page Comments", LastModified = "2013/10/10", Value = "Page Comments")]
    public string PageComments => this[nameof (PageComments)];

    /// <summary>Word: Reply</summary>
    [ResourceEntry("Reply", Description = "Word: Reply", LastModified = "2013/08/22", Value = "Reply")]
    public string Reply => this[nameof (Reply)];

    /// <summary>Phrase: Cancel reply</summary>
    [ResourceEntry("CancelReply", Description = "Phrase: Cancel reply", LastModified = "2013/08/22", Value = "Cancel reply")]
    public string CancelReply => this[nameof (CancelReply)];

    /// <summary>Phrase: Leave a comment</summary>
    [ResourceEntry("LeaveComment", Description = "Phrase: Leave a comment", LastModified = "2013/08/22", Value = "Leave a comment")]
    public string LeaveComment => this[nameof (LeaveComment)];

    /// <summary>Phrase: Show oldest on top</summary>
    [ResourceEntry("ShowOldestOnTop", Description = "Phrase: Show oldest on top", LastModified = "2013/08/22", Value = "Show oldest on top")]
    public string ShowOldestOnTop => this[nameof (ShowOldestOnTop)];

    /// <summary>Phrase: Show newest on top</summary>
    [ResourceEntry("ShowNewestOnTop", Description = "Phrase: Show newest on top", LastModified = "2013/08/22", Value = "Show newest on top")]
    public string ShowNewestOnTop => this[nameof (ShowNewestOnTop)];

    /// <summary>Phrase: Load more comments</summary>
    [ResourceEntry("LoadMoreComments", Description = "Phrase: Load more comments", LastModified = "2013/08/22", Value = "Load more comments")]
    public string LoadMoreComments => this[nameof (LoadMoreComments)];

    /// <summary>Shows label with comments count</summary>
    [ResourceEntry("CommentsCount", Description = "Shows label with comments count", LastModified = "2013/08/22", Value = "{0} comments")]
    public string CommentsCount => this[nameof (CommentsCount)];

    /// <summary>phrase: Shows label with comment count, single</summary>
    [ResourceEntry("CommentCount", Description = "phrase: Shows label with comment count, single", LastModified = "2013/09/16", Value = "{0} comment")]
    public string CommentCount => this[nameof (CommentCount)];

    /// <summary>phrase: 99+</summary>
    [ResourceEntry("NinetyNinePlus", Description = "phrase: 99+", LastModified = "2013/09/18", Value = "99+")]
    public string NinetyNinePlus => this[nameof (NinetyNinePlus)];

    /// <summary>Shows label with new comments count</summary>
    [ResourceEntry("NewCommentsCount", Description = "Shows label with new comments count", LastModified = "2013/08/26", Value = "Show {0} new comments")]
    public string NewCommentsCount => this[nameof (NewCommentsCount)];

    /// <summary>phrase: Your name</summary>
    [ResourceEntry("NameWatermark", Description = "phrase: Your name", LastModified = "2013/08/26", Value = "Your name")]
    public string NameWatermark => this[nameof (NameWatermark)];

    /// <summary>phrase: Email (optional)</summary>
    [ResourceEntry("EmailWatermark", Description = "phrase: Email (optional)", LastModified = "2013/08/26", Value = "Email (optional)")]
    public string EmailWatermark => this[nameof (EmailWatermark)];

    /// <summary>Shows text when replying to existing comment</summary>
    [ResourceEntry("ReplyToComment", Description = "Shows text when replying to existing comment", LastModified = "2013/08/26", Value = "Reply to {0}'s <a class= 'a-comment'>comment</a>")]
    public string ReplyToComment => this[nameof (ReplyToComment)];

    /// <summary>
    /// phrase: Custom content type or ID associated to comments
    /// </summary>
    [ResourceEntry("ThreadKeyText", Description = "phrase: Custom content type or ID associated to comments", LastModified = "2013/10/10", Value = "Custom content type or ID associated to comments")]
    public string ThreadKeyText => this[nameof (ThreadKeyText)];

    /// <summary>phrase: Title (used in Comments Backend)</summary>
    [ResourceEntry("ThreadTitle", Description = "phrase: Title (used in Comments Backend)", LastModified = "2013/10/10", Value = "Title (used in Comments Backend)")]
    public string ThreadTitle => this[nameof (ThreadTitle)];

    /// <summary>
    /// The text that will be displayed when the thread is closed
    /// </summary>
    [ResourceEntry("ClosedThreadText", Description = "The text that will be displayed when the thread is closed", LastModified = "2013/09/13", Value = "Comments are not allowed anymore")]
    public string ClosedThreadText => this[nameof (ClosedThreadText)];

    /// <summary>
    /// The text that will be displayed when requires approval is set for this thread
    /// </summary>
    [ResourceEntry("RequiredApprovalMessage", Description = "The text that will be displayed when requires approval is set for this thread", LastModified = "2013/10/09", Value = "Thank you for the comment! Your comment must be approved first")]
    public string RequiredApprovalMessage => this[nameof (RequiredApprovalMessage)];

    /// <summary>
    /// The text that will be displayed when the thread is restricted to authenticated users
    /// </summary>
    [ResourceEntry("RestrictToAuthenticatedText", Description = "The text that will be displayed when the thread is restricted to authenticated users", LastModified = "2013/09/13", Value = "<a href='{0}'>Login</a> to be able to comment")]
    public string RestrictToAuthenticatedText => this[nameof (RestrictToAuthenticatedText)];

    /// <summary>phrase:Comments form</summary>
    [ResourceEntry("CommentsForm", Description = "phrase:Comments form", LastModified = "2013/09/20", Value = "Comments form")]
    public string CommentsForm => this[nameof (CommentsForm)];

    /// <summary>phrase: Comments list</summary>
    [ResourceEntry("CommentsListViewTemplateName", Description = "phrase: Comments list", LastModified = "2013/09/20", Value = "Comments list")]
    public string CommentsListViewTemplateName => this[nameof (CommentsListViewTemplateName)];

    /// <summary>Comments template label in designer</summary>
    [ResourceEntry("CommentsTemplate", Description = "Comments template label in designer", LastModified = "2013/09/13", Value = "Comments template")]
    public string CommentsTemplate => this[nameof (CommentsTemplate)];

    /// <summary>Submit comment view template label in designer</summary>
    [ResourceEntry("SubmitCommentTemplate", Description = "Submit comment view template label in designer", LastModified = "2013/09/13", Value = "Submit comment view template")]
    public string SubmitCommentTemplate => this[nameof (SubmitCommentTemplate)];

    /// <summary>
    /// Submit comment view template label for unauthenticated user in designer
    /// </summary>
    [ResourceEntry("SubmitCommentUnauthenticatedTemplate", Description = "Submit comment view template label for unauthenticated user in designer", LastModified = "2013/09/13", Value = "Submit comment view template for unauthenticated user")]
    public string SubmitCommentUnauthenticatedTemplate => this[nameof (SubmitCommentUnauthenticatedTemplate)];

    /// <summary>Comments List View template label in designer</summary>
    [ResourceEntry("CommentsListViewTemplate", Description = "Comments List View template label in designer", LastModified = "2013/09/13", Value = "Comments list view template")]
    public string CommentsListViewTemplate => this[nameof (CommentsListViewTemplate)];

    /// <summary>You typed the code incorrectly. Please try again</summary>
    [ResourceEntry("CaptchavalidationError", Description = "You typed the code incorrectly. Please try again", LastModified = "2013/09/13", Value = "You typed the code incorrectly. Please try again")]
    public string CaptchavalidationError => this[nameof (CaptchavalidationError)];

    /// <summary>
    /// Comment cannot be submitted at the moment. The comment data arguments are not valid.
    /// </summary>
    [ResourceEntry("CannotSubmitInvalidCommentArgs", Description = "Comment cannot be submitted at the moment. The comment data arguments are not valid.", LastModified = "2013/11/07", Value = "Comment cannot be submitted at the moment. The comment data arguments are not valid.")]
    public string CannotSubmitInvalidCommentArgs => this[nameof (CannotSubmitInvalidCommentArgs)];

    /// <summary>
    /// Comment cannot be submitted at the moment. The thread data arguments are not valid.
    /// </summary>
    [ResourceEntry("CannotSubmitInvalidThreadArgs", Description = "Comment cannot be submitted at the moment. The thread data arguments are not valid.", LastModified = "2013/11/07", Value = "Comment cannot be submitted at the moment. The thread data arguments are not valid.")]
    public string CannotSubmitInvalidThreadArgs => this[nameof (CannotSubmitInvalidThreadArgs)];

    /// <summary>
    /// Comment cannot be submitted at the moment. The group data arguments are not valid.
    /// </summary>
    [ResourceEntry("CannotSubmitInvalidGroupArgs", Description = "Comment cannot be submitted at the moment. The group data arguments are not valid.", LastModified = "2013/11/07", Value = "Comment cannot be submitted at the moment. The group data arguments are not valid.")]
    public string CannotSubmitInvalidGroupArgs => this[nameof (CannotSubmitInvalidGroupArgs)];

    /// <summary>
    /// Comment cannot be submitted at the moment. Please refresh the page and try again.
    /// </summary>
    [ResourceEntry("CannotSubmitCommentMessage", Description = "Comment cannot be submitted at the moment. Please refresh the page and try again.", LastModified = "2013/09/20", Value = "Comment cannot be submitted at the moment. Please refresh the page and try again.")]
    public string CannotSubmitCommentMessage => this[nameof (CannotSubmitCommentMessage)];

    /// <summary>phrase: New code</summary>
    [ResourceEntry("NewCode", Description = "phrase: New code", LastModified = "2013/10/02", Value = "New code")]
    public string NewCode => this[nameof (NewCode)];

    /// <summary>phrase: Please type the code above</summary>
    [ResourceEntry("TypeCodeAbove", Description = "phrase: Please type the code above", LastModified = "2013/10/02", Value = "Please type the code above")]
    public string TypeCodeAbove => this[nameof (TypeCodeAbove)];

    /// <summary>phrase: Comments Url Name</summary>
    [ResourceEntry("CommentsUrlName", Description = "phrase: Comments Url Name", LastModified = "2013/10/02", Value = "Comments")]
    public string CommentsUrlName => this[nameof (CommentsUrlName)];

    /// <summary>phrase: Comment cannot be empty</summary>
    [ResourceEntry("EmptyCommentErrorMessage", Description = "phrase: Comment cannot be empty", LastModified = "2013/10/25", Value = "Comment cannot be empty")]
    public string EmptyCommentErrorMessage => this[nameof (EmptyCommentErrorMessage)];

    /// <summary>
    /// The text that will appear when the user could subscribe to comments
    /// </summary>
    [ResourceEntry("SubscribeToComments", Description = "The text that will appear when the user could subscribe to comments", LastModified = "2013/10/22", Value = "Subscribe to new comments")]
    public string SubscribeToComments => this[nameof (SubscribeToComments)];

    /// <summary>
    /// The text that will appear when the user could unsubscribe to comments
    /// </summary>
    [ResourceEntry("CurrentlySubscribed", Description = "The text that will appear when the user could unsubscribe to comments", LastModified = "2013/10/22", Value = "You are subscribed to new comments")]
    public string CurrentlySubscribed => this[nameof (CurrentlySubscribed)];

    /// <summary>Text that will appear once you subscribe to comments</summary>
    [ResourceEntry("SuccessfullySubscribed", Description = "Text that will appear once you subscribe to comments", LastModified = "2013/10/22", Value = "You are successfully subscribed to new comments")]
    public string SuccessfullySubscribed => this[nameof (SuccessfullySubscribed)];

    /// <summary>
    /// Text that will appear once you unsubscribe to comments
    /// </summary>
    [ResourceEntry("SuccessfullyUnsubscribed", Description = "Text that will appear once you unsubscribe to comments", LastModified = "2013/10/22", Value = "You are successfully unsubscribed to new comments")]
    public string SuccessfullyUnsubscribed => this[nameof (SuccessfullyUnsubscribed)];

    /// <summary>
    /// The label displayed before the rating control in the Comments Submit Form
    /// </summary>
    /// <value>Rating</value>
    [ResourceEntry("SubmitFormRatingLabel", Description = "The label displayed before the rating control in the Comments Submit Form", LastModified = "2013/11/18", Value = "Rating: ")]
    public string SubmitFormRatingLabel => this[nameof (SubmitFormRatingLabel)];

    /// <summary>phrase: Average Rating</summary>
    /// <value>Average Rating</value>
    [ResourceEntry("AverageRating", Description = "phrase: Average Rating", LastModified = "2013/11/20", Value = "Average rating:")]
    public string AverageRating => this[nameof (AverageRating)];

    /// <summary>phrase : Rating</summary>
    /// <value>Rating</value>
    [ResourceEntry("Rating", Description = "phrase : Rating", LastModified = "2013/11/20", Value = "Rating")]
    public string Rating => this[nameof (Rating)];

    /// <summary>phrase: reviews</summary>
    /// <value>reviews</value>
    [ResourceEntry("Reviews", Description = "phrase: reviews", LastModified = "2013/11/20", Value = "reviews")]
    public string Reviews => this[nameof (Reviews)];

    /// <summary>phrase: Average Rating</summary>
    /// <value>Average Rating</value>
    [ResourceEntry("AverageRatingBasedOnPublishedItems", Description = "phrase: Based on published items", LastModified = "2013/11/20", Value = "Based on published items")]
    public string AverageRatingBasedOnPublishedItems => this[nameof (AverageRatingBasedOnPublishedItems)];

    /// <summary>phrase: Write a review</summary>
    /// <value>Write a review</value>
    [ResourceEntry("WriteAReview", Description = "phrase: Write a review", LastModified = "2013/12/18", Value = "Write a review")]
    public string WriteAReview => this[nameof (WriteAReview)];

    /// <summary>phrase: Rating is required</summary>
    /// <value>Rating is required</value>
    [ResourceEntry("NoRatingErrorMessage", Description = "phrase: Rating is required", LastModified = "2013/11/28", Value = "Rating is required")]
    public string NoRatingErrorMessage => this[nameof (NoRatingErrorMessage)];

    /// <summary>
    /// A message displayed when ratings are allowed and a comment is submitted without rating.
    /// </summary>
    /// <value>Comment cannot be submitted without rating.</value>
    [ResourceEntry("RatingIsRequiredMessage", Description = "A message displayed when ratings are allowed and a comment is submitted without rating.", LastModified = "2013/11/29", Value = "Comment cannot be submitted without rating.")]
    public string RatingIsRequiredMessage => this[nameof (RatingIsRequiredMessage)];

    /// <summary>
    /// An error message displayed when multiple comments with rating per user are submitted.
    /// </summary>
    /// <value>Only one comment with rating is allowed per user.</value>
    [ResourceEntry("OnlyOneCommentWithRatingAllowedMessage", Description = "An error message displayed when multiple comments with rating per user are submitted.", LastModified = "2013/11/29", Value = "Only one comment with rating is allowed per user.")]
    public string OnlyOneCommentWithRatingAllowedMessage => this[nameof (OnlyOneCommentWithRatingAllowedMessage)];

    /// <summary>Phrase: All</summary>
    /// <value>All</value>
    [ResourceEntry("All", Description = "Phrase: All", LastModified = "2013/12/14", Value = "All")]
    public string All => this[nameof (All)];

    /// <summary>Phrase:With Rating</summary>
    /// <value>With Rating</value>
    [ResourceEntry("WithRating", Description = "Phrase:With Rating", LastModified = "2014/01/07", Value = "With Rating")]
    public string WithRating => this[nameof (WithRating)];

    /// <summary>phrase: Comments for</summary>
    /// <value>Comments for</value>
    [ResourceEntry("CommentsFor", Description = "phrase: Comments for", LastModified = "2013/12/21", Value = "Comments for")]
    public string CommentsFor => this[nameof (CommentsFor)];

    /// <summary>phrase: Products</summary>
    /// <value>Products</value>
    [ResourceEntry("ProductsThreadTitle", Description = "phrase: Products", LastModified = "2013/12/21", Value = "Products")]
    public string ProductsThreadTitle => this[nameof (ProductsThreadTitle)];

    /// <summary>phrase: Forms</summary>
    /// <value>Forms</value>
    [ResourceEntry("FormsThreadTitle", Description = "phrase: Forms", LastModified = "2013/12/21", Value = "Forms")]
    public string FormsThreadTitle => this[nameof (FormsThreadTitle)];

    /// <summary>phrase: Documents</summary>
    /// <value>Documents</value>
    [ResourceEntry("DocumentsThreadTitle", Description = "phrase: Documents", LastModified = "2013/12/21", Value = "Documents")]
    public string DocumentsThreadTitle => this[nameof (DocumentsThreadTitle)];

    /// <summary>phrase: Images</summary>
    /// <value>Images</value>
    [ResourceEntry("ImagesThreadTitle", Description = "phrase: Images", LastModified = "2013/12/21", Value = "Images")]
    public string ImagesThreadTitle => this[nameof (ImagesThreadTitle)];

    /// <summary>phrase: Videos</summary>
    /// <value>Videos</value>
    [ResourceEntry("VideosThreadTitle", Description = "phrase: Videos", LastModified = "2013/12/21", Value = "Videos")]
    public string VideosThreadTitle => this[nameof (VideosThreadTitle)];

    /// <summary>phrase: Blog posts</summary>
    /// <value>Blog posts</value>
    [ResourceEntry("BlogPostsThreadTitle", Description = "phrase: Blog posts", LastModified = "2013/12/21", Value = "Blog posts")]
    public string BlogPostsThreadTitle => this[nameof (BlogPostsThreadTitle)];

    /// <summary>phrase: Events</summary>
    /// <value>Events</value>
    [ResourceEntry("EventsThreadTitle", Description = "phrase: Events", LastModified = "2013/12/21", Value = "Events")]
    public string EventsThreadTitle => this[nameof (EventsThreadTitle)];

    /// <summary>phrase: List items</summary>
    /// <value>List items</value>
    [ResourceEntry("ListItemsThreadTitle", Description = "phrase: List items", LastModified = "2013/12/21", Value = "List items")]
    public string ListItemsThreadTitle => this[nameof (ListItemsThreadTitle)];

    /// <summary>phrase: News</summary>
    /// <value>News</value>
    [ResourceEntry("NewsItemsThreadTitle", Description = "phrase: News", LastModified = "2013/12/21", Value = "News")]
    public string NewsItemsThreadTitle => this[nameof (NewsItemsThreadTitle)];

    /// <summary>phrase: Shows label with review count, single</summary>
    /// <value>{0} review</value>
    [ResourceEntry("ReviewCount", Description = "phrase: Shows label with review count, single", LastModified = "2014/01/03", Value = "{0} review")]
    public string ReviewCount => this[nameof (ReviewCount)];

    /// <summary>phrase: Shows label with reviews count</summary>
    /// <value>{0} review</value>
    [ResourceEntry("ReviewsCount", Description = "phrase: Shows label with reviews count", LastModified = "2014/01/03", Value = "{0} reviews")]
    public string ReviewsCount => this[nameof (ReviewsCount)];

    /// <summary>
    /// The text that will be displayed when requires approval is set for this thread and ratings are enabled
    /// </summary>
    /// <value>Thank you for the review! Your review must be approved first</value>
    [ResourceEntry("RequiredApprovalMessageReviews", Description = "The text that will be displayed when requires approval is set for this thread and ratings are enabled", LastModified = "2014/01/03", Value = "Thank you for the review! Your review must be approved first")]
    public string RequiredApprovalMessageReviews => this[nameof (RequiredApprovalMessageReviews)];

    /// <summary>Phrase:Without Rating</summary>
    /// <value>Without Rating</value>
    [ResourceEntry("WithoutRating", Description = "Phrase:Without Rating", LastModified = "2014/01/07", Value = "Without Rating")]
    public string WithoutRating => this[nameof (WithoutRating)];

    /// <summary>phrase: review</summary>
    /// <value>review</value>
    [ResourceEntry("Review", Description = "phrase: review", LastModified = "2014/01/07", Value = "review")]
    public string Review => this[nameof (Review)];

    /// <summary>
    /// The text that will be displayed when the user has already submitted a review
    /// </summary>
    /// <value>You've already submitted a review for this item</value>
    [ResourceEntry("AlreadySubmittedReviewMessage", Description = "The text that will be displayed when the user has already submitted a review", LastModified = "2014/01/08", Value = "You've already submitted a review for this item")]
    public string AlreadySubmittedReviewMessage => this[nameof (AlreadySubmittedReviewMessage)];

    /// <summary>
    /// The text that is displayed when a review is submitted successfully
    /// </summary>
    /// <value>Thank you! Your review has been submitted successfully</value>
    [ResourceEntry("ReviewSubmittedSuccessfullyMessage", Description = "The text that is displayed when a review is submitted successfully", LastModified = "2014/01/08", Value = "Thank you! Your review has been submitted successfully")]
    public string ReviewSubmittedSuccessfullyMessage => this[nameof (ReviewSubmittedSuccessfullyMessage)];

    /// <summary>Control title: Reviews</summary>
    /// <value>Reviews</value>
    [ResourceEntry("ReviewsTitle", Description = "Control title: Reviews", LastModified = "2014/01/08", Value = "Reviews")]
    public string ReviewsTitle => this[nameof (ReviewsTitle)];

    /// <summary>
    /// The text that will appear when the user could subscribe to reviews
    /// </summary>
    /// <value>Subscribe to new reviews</value>
    [ResourceEntry("SubscribeToReviews", Description = "The text that will appear when the user could subscribe to reviews", LastModified = "2014/01/08", Value = "Subscribe to new reviews")]
    public string SubscribeToReviews => this[nameof (SubscribeToReviews)];

    /// <summary>
    /// The text that will appear when the user could unsubscribe to reviews
    /// </summary>
    /// <value>You are subscribed to new reviews</value>
    [ResourceEntry("CurrentlySubscribedToReviews", Description = "The text that will appear when the user could unsubscribe to reviews", LastModified = "2014/01/08", Value = "You are subscribed to new reviews")]
    public string CurrentlySubscribedToReviews => this[nameof (CurrentlySubscribedToReviews)];

    /// <summary>Text that will appear once you subscribe to reviews</summary>
    /// <value>You are successfully subscribed to new reviews</value>
    [ResourceEntry("SuccessfullySubscribedToReviews", Description = "Text that will appear once you subscribe to reviews", LastModified = "2014/01/08", Value = "You are successfully subscribed to new reviews")]
    public string SuccessfullySubscribedToReviews => this[nameof (SuccessfullySubscribedToReviews)];

    /// <summary>
    /// Text that will appear once you unsubscribe to new reviews
    /// </summary>
    /// <value>You are successfully unsubscribed to new reviews</value>
    [ResourceEntry("SuccessfullyUnsubscribedFromReviews", Description = "Text that will appear once you unsubscribe to new reviews", LastModified = "2014/01/08", Value = "You are successfully unsubscribed to new reviews")]
    public string SuccessfullyUnsubscribedFromReviews => this[nameof (SuccessfullyUnsubscribedFromReviews)];

    /// <summary>
    /// The text that will be displayed when the thread is restricted to authenticated users
    /// </summary>
    /// <value><a href="{0}">Login</a> to be able to write a review</value>
    [ResourceEntry("RestrictToAuthenticatedReviewText", Description = "The text that will be displayed when the thread is restricted to authenticated users", LastModified = "2014/01/08", Value = "<a href='{0}'>Login</a> to be able to write a review")]
    public string RestrictToAuthenticatedReviewText => this[nameof (RestrictToAuthenticatedReviewText)];

    /// <summary>phrase: Review cannot be empty</summary>
    /// <value>Review cannot be empty</value>
    [ResourceEntry("EmptyReviewErrorMessage", Description = "phrase: Review cannot be empty", LastModified = "2014/01/13", Value = "Review cannot be empty")]
    public string EmptyReviewErrorMessage => this[nameof (EmptyReviewErrorMessage)];
  }
}
