// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.DefinitionsHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.QueryBuilder;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>A helper class for modules definitions.</summary>
  public static class DefinitionsHelper
  {
    private static JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
    /// <summary>
    /// Common name for the command used to create a new item.
    /// </summary>
    public const string CreateCommandName = "create";
    /// <summary>Common name for the command used to save an item.</summary>
    public const string SaveCommandName = "save";
    /// <summary>
    /// Common name for a command used to cancel an operation.
    /// </summary>
    public const string CancelCommandName = "cancel";
    /// <summary>
    /// Common name used for a command that saves an item and resets the entry form.
    /// </summary>
    public const string SaveAndContinueCommandName = "saveAndContinue";
    /// <summary>
    /// Common name used for a command that publishes an item.
    /// </summary>
    public const string PublishCommandName = "publish";
    /// <summary>
    /// Common name used for a command that unpublishes an item.
    /// </summary>
    public const string UnpublishCommandName = "unpublish";
    /// <summary>
    /// Common name used for a command that performs a batch publish
    /// </summary>
    public const string GroupPublishCommandName = "groupPublish";
    /// <summary>
    /// Common name used for a command that performs a batch unpublish
    /// </summary>
    public const string GroupUnpublishCommandName = "groupUnpublish";
    /// <summary>Common name used for a command that delets an item.</summary>
    public const string DeleteCommandName = "delete";
    /// <summary>
    /// Common name used for a command that deactivates an item.
    /// </summary>
    public const string DeactivateCommandName = "deactivate";
    /// <summary>
    /// Common name used for a command that performs a batch delete.
    /// </summary>
    public const string GroupDeleteCommandName = "groupDelete";
    /// <summary>A name used for a command that delets an template.</summary>
    public const string DeleteTemplateCommandName = "deleteTemplate";
    /// <summary>
    /// A name used for a command that performs a batch delete of templates.
    /// </summary>
    public const string GroupDeleteTemplateCommandName = "groupDeleteTemplate";
    /// <summary>
    /// Common name used for a command that performs a serarch.
    /// </summary>
    public const string SearchCommandName = "search";
    /// <summary>Common name used for a command that closes serarch.</summary>
    public const string CloseSearchCommandName = "closeSearch";
    /// <summary>
    /// Common name used for a command that displays an item in a view mode.
    /// </summary>
    public const string ViewCommandName = "view";
    /// <summary>
    /// Common name used for a command that displays all libraries.
    /// </summary>
    public const string ViewLibrariesCommandName = "viewLibraries";
    /// <summary>
    /// Common name used for a command that displays items by parent.
    /// </summary>
    public const string ViewItemsByParentCommandName = "viewItemsByParent";
    /// <summary>
    /// Common name used for a command that displays marked items.
    /// </summary>
    public const string ViewMarkedItemsCommandName = "viewMarkedItems";
    /// <summary>
    /// Common name used for a command that duplicates an item.
    /// </summary>
    public const string DuplicateCommandName = "duplicate";
    /// <summary>Common name used for a command that edits an item.</summary>
    public const string EditCommandName = "edit";
    /// <summary>Common name used for a command that edits a parent.</summary>
    public const string ParentPropertiesCommandName = "parentProperties";
    /// <summary>
    /// Common name used for a command that edits the album properties.
    /// </summary>
    public const string EditLibraryPropertiesCommandName = "editLibraryProperties";
    /// <summary>
    /// Common name used for a command that edits image, video or document properties.
    /// </summary>
    public const string EditMediaContentPropertiesCommandName = "editMediaContentProperties";
    /// <summary>
    /// Common name used for a command that displays the history of an item.
    /// </summary>
    public const string HistoryCommandName = "history";
    /// <summary>
    /// Common name used for a command that displays the history of an item.
    /// </summary>
    public const string HistoryGridCommandName = "historygrid";
    /// <summary>
    /// Common name used for a command that provides more information about the author of an item.
    /// </summary>
    public const string AuthorCommandName = "author";
    /// <summary>
    /// Common name used for a command that opens an item in a preview mode.
    /// </summary>
    public const string PreviewCommandName = "preview";
    /// <summary>
    /// Common name used for a command that saves item temp version and opens link
    /// </summary>
    public const string SaveTempAndOpenLinkCommandName = "saveTempAndOpenLink";
    /// <summary>
    /// Common name used for a command that opens an item in a preview mode.
    /// </summary>
    public const string ViewPropertiesCommandName = "viewProperties";
    /// <summary>
    /// Common name used for a command that opens an item histiry version in a preview mode.
    /// </summary>
    public const string VersionPreviewCommandName = "versionPreview";
    /// <summary>Common name used for a the filter command.</summary>
    public const string FilterCommanName = "filter";
    /// <summary>
    /// Temporaly used for the commands which are not implemented yet (clicking such a
    /// command should display an alert.
    /// </summary>
    public const string NotImplementedCommandName = "notImplemented";
    /// <summary>
    /// Common name used for a command that shows the permissions for a given secured
    /// object.
    /// </summary>
    public const string PermissionsCommandName = "permissions";
    /// <summary>The permissions command for dynamic content.</summary>
    public const string PermissionsDynamicContentCommandName = "permissionsDynamicContent";
    /// <summary>Used to open the configuration settings</summary>
    public const string SettingsCommandName = "settings";
    /// <summary>Used to open the content locations settings</summary>
    public const string ManageContentLocationsCommandName = "manageContentLocations";
    /// <summary>Used to open the thumbnail settings</summary>
    public const string ThumbnailSettingsCommandName = "thumbnailSettingsPage";
    /// <summary>
    /// Common name used for a command that navigates user to the comments screen.
    /// </summary>
    public const string CommentsCommandName = "comments";
    /// <summary>
    /// Common name used for a command that navigates user to comments filtered by parent.
    /// </summary>
    public const string ParentCommentsCommandName = "parentComments";
    /// <summary>Common name used for a command that edits a comment.</summary>
    public const string EditCommentCommandName = "editComment";
    /// <summary>
    /// Common name used for a command that creates a comment.
    /// </summary>
    public const string CreateCommentCommandName = "createComment";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for working
    /// editing the module structure.
    /// </summary>
    public const string ModuleEditor = "moduleEditor";
    /// <summary>
    /// Common name used for a command that blocks the comments from the user with the specified email.
    /// </summary>
    public const string BlockEmailCommandName = "blockEmail";
    /// <summary>
    /// Common name used for a command that blocks the comments from the user with the specified ip.
    /// </summary>
    public const string BlockIpCommandName = "blockIp";
    /// <summary>
    /// Common name used for a command that navigates user to the upload screen.
    /// </summary>
    public const string UploadCommandName = "upload";
    /// <summary>
    /// Common name used for a command that navigates user to the upload screen.
    /// </summary>
    public const string Html5UploadCommandName = "html5upload";
    /// <summary>
    /// Common name used for a command that creates album and navigates user to the upload screen.
    /// </summary>
    public const string CreateAndUploadCommandName = "createAndUpload";
    /// <summary>
    /// Common name used for a command that navigates user to the bulk edit screen.
    /// </summary>
    public const string BulkEditCommandName = "bulkEdit";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for selecting other album.
    /// </summary>
    public const string SelectLibraryCommandName = "selectLibrary";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for selecting other blog.
    /// </summary>
    public const string SelectBlogCommandName = "selectBlog";
    /// <summary>Common name used for a command that shows all items.</summary>
    public const string ShowAllItemsCommandName = "showAllItems";
    /// <summary>
    /// Common name used for a command that shows the items of the currently logged user.
    /// </summary>
    public const string ShowMyItemsCommandName = "showMyItems";
    /// <summary>
    /// Common name used for a command that shows master items.
    /// </summary>
    public const string ShowMasterItemsCommandName = "showMasterItems";
    /// <summary>
    /// Common name used for a command that shows published items.
    /// </summary>
    public const string ShowPublishedItemsCommandName = "showPublishedItems";
    /// <summary>
    /// Common name used for a command that shows published items.
    /// </summary>
    public const string ShowScheduledItemsCommandName = "showScheduledItems";
    /// <summary>
    /// Common name used for a command that shows all content items that are pending approval.
    /// </summary>
    public const string PendingApprovalItemsCommandName = "showPendingApprovalItems";
    /// <summary>
    /// Common name used for a command that shows all content items that are pending review.
    /// </summary>
    public const string PendingReviewItemsCommandName = "showPendingReviewItems";
    /// <summary>
    /// Common name used for a command that shows all content items that are pending publishing.
    /// </summary>
    public const string PendingPublishingItemsCommandName = "showPendingPublishingItems";
    /// <summary>
    /// Common name used for a command that shows all content items that are rejected.
    /// </summary>
    public const string RejectedItemsCommandName = "showRejectedItems";
    /// <summary>
    /// Common name used for a command that shows all items that are pending approval.
    /// </summary>
    public const string PendingApprovalPagesCommandName = "showPendingApprovalPages";
    /// <summary>
    /// Common name used for a command that shows all items that are pending review.
    /// </summary>
    public const string PendingReviewPagesCommandName = "showPendingReviewPages";
    /// <summary>
    /// Common name used for a command that shows all items that are pending publishing.
    /// </summary>
    public const string PendingPublishingPagesCommandName = "showPendingPublishingPages";
    /// <summary>
    /// Common name used for a command that shows all items that are rejected.
    /// </summary>
    public const string RejectedPagesCommandName = "showRejectedPages";
    /// <summary>
    /// Common name used for a command that shows all items that are pending approval.
    /// </summary>
    public const string AwaitingMyActionPagesCommandName = "showAwaitingMyActionPages";
    /// <summary>
    /// Common name used for a command that shows not used items.
    /// </summary>
    public const string ShowNotUsedItemsCommandName = "showNotUsedItems";
    /// <summary>
    /// Common name used for a command that embeds an image, video or document.
    /// </summary>
    public const string EmbedMediaContentCommandName = "embedMediaContent";
    /// <summary>
    /// Common name used for a command that displays the original image.
    /// </summary>
    public const string ViewOriginalImageCommandName = "viewOriginalImage";
    /// <summary>
    /// Common name used for a command that displays the original image.
    /// </summary>
    public const string ViewAllThumbnailSizesCommandName = "viewAllThumbnailSizes";
    /// <summary>
    /// Common name used for a command that displays the original document.
    /// </summary>
    public const string ViewOriginalDocumentCommandName = "viewOriginalDocument";
    /// <summary>
    /// Common name used for a command that plays the original video.
    /// </summary>
    public const string PlayOriginalVideoCommandName = "playOriginalVideo";
    /// <summary>
    /// Common name used for a command that shows widget sections except given sectionIds
    /// </summary>
    public const string ShowSectionsExceptCommandName = "showSectionsExcept";
    /// <summary>
    /// Common name used for a command that shows widget sections except given sectionIds and resets the filtering.
    /// </summary>
    public const string ShowSectionsExceptAndResetFilterCommandName = "showSectionsExceptAndResetFilter";
    /// <summary>
    /// Common name used for a command that hides widget sections except given sectionIds
    /// </summary>
    public const string HideSectionsExceptCommandName = "hideSectionsExcept";
    /// <summary>
    /// Common name used for a command that filters the grid by a tag.
    /// </summary>
    public const string TagFilterCommandName = "tagFilter";
    /// <summary>
    /// Common name used for a command that filters the grid by a category.
    /// </summary>
    public const string CategoryFilterCommandName = "categoryFilter";
    /// <summary>
    /// Common name used for a command that filters the grid by a tag.
    /// </summary>
    public const string TagShowFilterCommandName = "hideAllWidgets";
    /// <summary>
    /// Common name used for a command that filters the grid by a category.
    /// </summary>
    public const string CategoryShowFilterCommandName = "hideAllWidgets";
    /// <summary>
    /// Common name used for a command for downloading the media content.
    /// </summary>
    public const string DownloadCommandName = "download";
    /// <summary>
    /// Common name used for a command that switches to grid view state.
    /// </summary>
    public const string GridViewStateCommandName = "gridViewState";
    /// <summary>
    /// Common name used for a command that switches to list view state.
    /// </summary>
    public const string ListViewStateCommandName = "listViewState";
    /// <summary>
    /// Common name used for a command that switches to tree view state.
    /// </summary>
    public const string TreeViewStateCommandName = "treeViewState";
    /// <summary>Common name used for a command that inserts tags.</summary>
    public const string InsertTagsCommandName = "insertTags";
    /// <summary>
    /// Common name used for a command that opens dialog displaying the pages using current template.
    /// </summary>
    public const string TemplatePagesCommandName = "templatePages";
    /// <summary>
    /// Common name used for a command that shows the flat taxonomies.
    /// </summary>
    public const string ShowFlatTaxonomiesCommandName = "showFlatTaxonomies";
    /// <summary>
    /// Common name used for a command that shows the hierarchical taxonomies.
    /// </summary>
    public const string ShowHierarchicalTaxonomiesCommandName = "showHierarchicalTaxonomies";
    /// <summary>
    /// Common name used for a command that shows not used in any site taxonomies.
    /// </summary>
    public const string ShowNotUsedTaxonomiesCommandName = "showNotUsedTaxonomies";
    /// <summary>Common name for creating a forum group.</summary>
    internal const string CreateForumGroupCommandName = "createForumGroup";
    /// <summary>Common name for editing a forum group.</summary>
    internal const string EditForumGroupCommandName = "editForumGroup";
    /// <summary>Common name used for a subscribe command.</summary>
    internal const string SubscribeCommandName = "subscribe";
    /// <summary>Common name used for a subscribe command.</summary>
    internal const string UnsubscribeCommandName = "unsubscribe";
    /// <summary>Common name used for a not shared command.</summary>
    internal const string ShowNotSharedCommandName = "showNotShared";
    /// <summary>
    /// Defines the json represented command argument object for an empty filter expression.
    /// </summary>
    public const string EmptyFilterCommandArgument = "{ filterExpression: '' }";
    /// <summary>
    /// Common name used for a command that opens a dialog for custom sorting expression.
    /// </summary>
    public const string EditCustomSortingExpression = "editCustomSortingExpression";
    /// <summary>
    /// Common name for a command that opens dialog for reorering images/videos, etc.
    /// </summary>
    public const string ReorderCommandName = "reorder";
    public const string ShowMoreTranslationsCommandName = "showMoreTranslations";
    public const string HideMoreTranslationsCommandName = "hideMoreTranslations";
    private const string ShowSectionsExceptCommandArgumentPropertyName = "sectionIds";
    public const string RestoreVersionAsNewCommandName = "restoreVersionAsNew";
    public const string DeleteVersionCommandName = "deleteVersion";
    public const string ExportCommandName = "export";
    /// <summary>
    /// Common name used for a command that changes the language.
    /// </summary>
    public const string ChangeLanguageCommandName = "changeLanguage";
    /// <summary>Common name used for a command that starts indexing.</summary>
    public const string ReindexCommandName = "reindex";
    /// <summary>
    /// Define a Dynamic-Linq filter for items that are ready to go live
    /// </summary>
    public const string PublishedOrScheduledFilterExpression = "Visible = true AND Status = Live";
    /// <summary>
    /// Same as <see cref="F:Telerik.Sitefinity.Modules.DefinitionsHelper.PublishedOrScheduledFilterExpression" />, but this one can be used in templates
    /// </summary>
    public static readonly string PublishedFilterExpression = "Visible = true AND Status = Live";
    public const string ContentLifecycleStatusName = "Status";
    public const string WorkflowStateName = "ApprovalWorkflowState";
    public const string PublishedDraftStatusFilterExpression = "Master";
    public const string PublishedDraftWorkflowStateFilterExpression = "Published";
    public const string NotPublishedDraftStatusFilterExpression = "Master";
    public const string NotPublishedDraftWorkflowStateFilterExpression = "Draft";
    public const string ScheduledItemsStatusFilterExpression = "Master";
    public const string ScheduledItemsWorkflowStateFilterExpression = "Scheduled";
    public const string PendingApprovalItemsStatusFilterExpression = "Master";
    public const string PendingApprovalItemsWorkflowStateFilterExpression = "AwaitingApproval";
    public const string PendingReviewItemsStatusFilterExpression = "Master";
    public const string PendingReviewItemsWorkflowStateFilterExpression = "AwaitingReview";
    public const string PendingPublishingItemsStatusFilterExpression = "Master";
    public const string PendingPublishingItemsWorkflowStateFilterExpression = "AwaitingPublishing";
    public const string RejectedItemsStatusFilterExpression = "Master";
    public const string RejectedItemsWorkflowStateFilterExpression = "Rejected";
    /// <summary>
    /// Define a Dynamic Linq filter for draft (master) items that have public versions
    /// </summary>
    public static readonly string PublishedDraftsFilterExpression = "Status=Master AND ApprovalWorkflowState=\"Published\"";
    /// <summary>
    /// Filter expression for drafts that have no public version
    /// </summary>
    public static readonly string NotPublishedDraftsFilterExpression = "Status = Master AND ApprovalWorkflowState = \"Draft\"";
    /// <summary>Filter expression for schedlued items</summary>
    public static readonly string ScheduledItemsFilterExpression = "Status = Master AND ApprovalWorkflowState = \"Scheduled\"";
    /// <summary>Filter expression for items that are pending approval</summary>
    public static readonly string PendingApprovalItemsFilterExpression = "Status = Master AND ApprovalWorkflowState = \"AwaitingApproval\"";
    /// <summary>Filter expression for items that are pending review</summary>
    public static readonly string PendingReviewItemsFilterExpression = "Status = Master AND ApprovalWorkflowState = \"AwaitingReview\"";
    /// <summary>
    /// Filter expression for items that are pending publishing
    /// </summary>
    public static readonly string PendingPublishingItemsFilterExpression = "Status = Master AND ApprovalWorkflowState = \"AwaitingPublishing\"";
    /// <summary>Filter expression for items that are rejected</summary>
    public static readonly string RejectedItemsFilterExpression = "Status = Master AND (ApprovalWorkflowState = \"Rejected\" OR ApprovalWorkflowState = \"RejectedForReview\" OR ApprovalWorkflowState = \"RejectedForApproval\" OR ApprovalWorkflowState = \"RejectedForPublishing\")";
    public const string ShareLinkCommandName = "shareLink";
    public const string UnlockPageCommandName = "unlockPage";
    public const string UnlockFormCommandName = "unlockForm";
    public const string ChangeTemplateCommandName = "changeTemplate";
    public const string BatchChangeTemplateCommandName = "batchChangeTemplate";
    public const string BatchChangeOwnerCommandName = "batchChangeOwner";
    public const string BatchPublishCommandName = "batchPublishPage";
    public const string BatchUnpublishCommandName = "batchUnpublishPage";
    public const string ShareWithCommandName = "shareWith";
    /// <summary>
    /// Common name used for a command that filters only empty blogs.
    /// </summary>
    public const string ShowEmptyBlogsCommandName = "showEmptyBlogs";
    /// <summary>
    /// Common name for the command used to create a new item child of the selected one.
    /// </summary>
    public const string CreateChildCommandName = "createChild";
    /// <summary>common name used to make an item active</summary>
    public const string MakeActiveCommandName = "makeActive";
    /// <summary>common name used to make an item inactive</summary>
    public const string MakeInActiveCommandName = "makeInActive";
    /// <summary>
    /// Common name used for a command that sets the images as album cover.
    /// </summary>
    public const string SetAsCoverCommandName = "setAsCover";
    /// <summary>
    /// The name of the section placed as a toolbar in the detail dialogs.
    /// </summary>
    public const string ToolbarSectionName = "toolbarSection";
    /// <summary>
    /// The name of the section placed as a sidebar in the detail dialogs.
    /// </summary>
    public const string SidebarSectionName = "SidebarSection";
    public const string AdvancedSectionName = "AdvancedSection";
    public const string MoreOptionsSectionName = "MoreOptionsSection";
    /// <summary>
    /// The name of the section where warning messages are shown.
    /// </summary>
    public const string HeaderSection = "HeaderSection";
    [Obsolete]
    private static readonly string UrlRegExBaseDotNet = DefinitionsHelper.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\'\\.";
    [Obsolete]
    public static readonly string UrlRegularExpressionFilterDotNet = "[^" + DefinitionsHelper.UrlRegExBaseDotNet + "]+";
    [Obsolete]
    public static readonly string UrlRegularExpressionDotNet = "[" + DefinitionsHelper.UrlRegExBaseDotNet + "]+";
    [Obsolete]
    public static readonly string UrlRegularExpressionFilterForValidator = "^[" + DefinitionsHelper.RgxStrategy.UrlRegExBase + "]+$";
    [Obsolete]
    public static readonly string UrlRegularExpressionFilterForAltUrlValidator = "^~/([" + DefinitionsHelper.RgxStrategy.UrlRegExBase + "]+/?)+$";
    [Obsolete]
    public static readonly string SeoRegularExpressionFilterForValidator = "^[" + DefinitionsHelper.RgxStrategy.SeoRegExBase + "]+$";
    internal const string StatusFilterTemplateName = "StatusFilterTemplate";
    /// <summary>
    /// The regular expression filter used in MirrorField for URLs
    /// </summary>
    public static readonly string UrlRegularExpressionFilter = "[^" + DefinitionsHelper.RgxStrategy.UrlRegExBase + "]+|\\.+$";
    /// <summary>
    /// The regular expression filter used in MirrorField for URLs for Libraries module
    /// </summary>
    public static readonly string UrlRegularExpressionFilterForLibraries = "[^" + DefinitionsHelper.RgxStrategy.UnicodeCategories + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "\\-\\!\\$\\(\\)\\=\\@\\d_\\']+|\\.+$";
    /// <summary>
    /// The regular expression filter used in MirrorField for title for search engines
    /// </summary>
    public static readonly string SeoRegularExpressionFilter = "[^" + DefinitionsHelper.RgxStrategy.SeoRegExBase + "]+";
    /// <summary>
    /// The regular expression filter used in MirrorField for title for Pages module
    /// </summary>
    public static readonly string UrlPagesRegularExpressionFilterForPages = "[^" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\']+";
    /// <summary>The regular expression filter used for dotnet</summary>
    public static readonly string UrlRegularExpressionDotNetFilter = "[^" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\'~\\.]+|\\.+$";
    /// <summary>
    /// The regular expression validator used for most common server validate urls
    /// </summary>
    public static readonly string UrlRegularExpressionDotNetValidator = "^[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "#" + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "!\\?\\+=&%@!_/\\-\\d\\(\\)~\\.]*[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "#" + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "!\\?\\+=&%@!_/\\-\\d\\(\\)~]+$";
    /// <summary>All alowed characters in URL node except dot.</summary>
    private static readonly string UrlAllowedNodeCharactersNoDot = "[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "\\-\\!\\$\\(\\)\\@\\d_\\'~]";
    /// <summary>
    /// Incomplete regexp (no opening and closing '^' and '$') for node (file or dir) names.
    /// Validation enforces valid characters and that it doesn't end with dot and
    /// there are no 2 dots next to each other.
    /// </summary>
    private static readonly string UrlRegExpNodeNameFragmentValidator = "(\\." + DefinitionsHelper.UrlAllowedNodeCharactersNoDot + "|" + DefinitionsHelper.UrlAllowedNodeCharactersNoDot + ")";
    /// <summary>
    /// The regular expression filter used in the validators for URL fields for content
    /// </summary>
    public static readonly string UrlRegularExpressionFilterForContentValidator = "^" + DefinitionsHelper.UrlRegExpNodeNameFragmentValidator + "+$";
    /// <summary>
    /// The regular expression filter used in the validators for URL fields for Libraries content
    /// </summary>
    public static readonly string UrlRegularExpressionFilterForLibrariesContentValidator = "^[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\'~]+$";
    /// <summary>Used for additional url validation for content</summary>
    public static readonly string UrlRegularExpressionFilterForAdditionalContentUrlsValidator = "^[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "#" + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "!\\?\\+=&%@!_/\\-\\d\\(\\)~\\.]*[" + DefinitionsHelper.RgxStrategy.UnicodeCategories + "#" + DefinitionsHelper.RgxStrategy.AllowedSpecialSymbols + "!\\?\\+=&%@!_/\\-\\d\\(\\)~]+$";
    /// <summary>Characters allowed in URL encoded strings.</summary>
    private static readonly string UrlEncodedAllowedCharacters = DefinitionsHelper.RgxStrategy.UnicodeCategories + "\\d_\\-\\.\\+\\%";
    /// <summary>Optional GET query string</summary>
    private static readonly string UrlQueryValidator = "\\?[" + DefinitionsHelper.UrlEncodedAllowedCharacters + "\\=\\&]*";
    /// <summary>Validates URL path consisting of multiple nodes.</summary>
    private static readonly string UrlMultinodePathValidator = "(" + DefinitionsHelper.UrlRegExpNodeNameFragmentValidator + "\\/|" + DefinitionsHelper.UrlRegExpNodeNameFragmentValidator + ")+";
    /// <summary>
    /// Used for additional url validation for Libraries content
    /// </summary>
    public static readonly string UrlRegularExpressionFilterForAdditionalLibrariesContentUrlsValidator = "^~?/?" + DefinitionsHelper.UrlMultinodePathValidator + "$";
    /// <summary>
    /// The regular expression filter used in the validators for additional URLs for pages
    /// Must start with "~/", then node names follow and optional query string is allowed.
    /// </summary>
    public static readonly string UrlRegularExpressionFilterForAdditionalPagesUrlsValidator = "^~/" + DefinitionsHelper.UrlMultinodePathValidator + "(" + DefinitionsHelper.UrlQueryValidator + ")?$";
    /// <summary>
    /// The regular expression filter used in the validators for for 'title for search engines' fields.
    /// </summary>
    public static readonly string UrlRegularExpressionSeoFilterForPagesValidator = "^[" + DefinitionsHelper.RgxStrategy.SeoRegExBase + "]+$";
    public const string FloatRegularExpressionForPriorityValidatior = "^[0-9]*(?:\\.[0-9]*)?$";
    /// <summary>The number of language items in a row.</summary>
    public const int LanguageItemsPerRow = 6;
    public const string StatusSectionName = "statusSection";
    public const string CloseDialogCommandName = "closeDialog";
    public const string RestoreControlTemplateCommandName = "restoreTemplate";
    /// <summary>The 'set taxonomy' command name</summary>
    public const string SetTaxonomyCommandName = "setTaxonomy";
    /// <summary>The 'show taxonomy site' command name</summary>
    public const string ShowTaxonomySitesCommandName = "showSharedSites";
    /// <summary>The 'Use classification in' command name</summary>
    public const string UseClassificationIn = "useClassificationIn";
    internal static readonly IDictionary<string, WorkflowType[]> AllowedWorkflowTypesForCommandMap = (IDictionary<string, WorkflowType[]>) new Dictionary<string, WorkflowType[]>()
    {
      {
        "showPendingApprovalItems",
        new WorkflowType[3]
        {
          WorkflowType.StandardOneStep,
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showPendingApprovalPages",
        new WorkflowType[3]
        {
          WorkflowType.StandardOneStep,
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showRejectedItems",
        new WorkflowType[3]
        {
          WorkflowType.StandardOneStep,
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showRejectedPages",
        new WorkflowType[3]
        {
          WorkflowType.StandardOneStep,
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showPendingReviewItems",
        new WorkflowType[1]{ WorkflowType.StandardThreeStep }
      },
      {
        "showPendingReviewPages",
        new WorkflowType[1]{ WorkflowType.StandardThreeStep }
      },
      {
        "showPendingPublishingItems",
        new WorkflowType[2]
        {
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showPendingPublishingPages",
        new WorkflowType[2]
        {
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      },
      {
        "showAwaitingMyActionPages",
        new WorkflowType[3]
        {
          WorkflowType.StandardOneStep,
          WorkflowType.StandardTwoStep,
          WorkflowType.StandardThreeStep
        }
      }
    };
    private static RegexStrategy regexStrategy = (RegexStrategy) null;

    public static DefinitionsHelper.IAddResourceStringFluent AddResourceString(
      this ContentViewDefinitionElement cfg,
      string classId,
      string messageKey)
    {
      return new DefinitionsHelper.AddResourceStringFluent(cfg).AddResourceString(classId, messageKey);
    }

    public static string GetLabel(string resourceClassId, string key)
    {
      if (key == null)
        return (string) null;
      return !string.IsNullOrEmpty(resourceClassId) ? Res.Get(resourceClassId, key) : key;
    }

    /// <summary>Creates the action menu widget element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="name">The name.</param>
    /// <param name="wrapperTagName">Name of the wrapper tag.</param>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="text">The text.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <returns></returns>
    public static CommandWidgetElement CreateActionMenuCommand(
      ConfigElement parent,
      string name,
      HtmlTextWriterTag wrapperTagKey,
      string commandName,
      string text,
      string resourceClassId)
    {
      CommandWidgetElement actionMenuCommand = new CommandWidgetElement(parent);
      actionMenuCommand.Name = name;
      actionMenuCommand.WrapperTagKey = wrapperTagKey;
      actionMenuCommand.CommandName = commandName;
      actionMenuCommand.Text = text;
      actionMenuCommand.ResourceClassId = resourceClassId;
      actionMenuCommand.WidgetType = typeof (CommandWidget);
      return actionMenuCommand;
    }

    /// <summary>Creates the action menu command.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="name">The name.</param>
    /// <param name="wrapperTagKey">The wrapper tag key.</param>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="text">The text.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="cssClass">The CSS class.</param>
    /// <returns></returns>
    public static CommandWidgetElement CreateActionMenuCommand(
      ConfigElement parent,
      string name,
      HtmlTextWriterTag wrapperTagKey,
      string commandName,
      string text,
      string resourceClassId,
      string cssClass)
    {
      CommandWidgetElement actionMenuCommand = DefinitionsHelper.CreateActionMenuCommand(parent, name, wrapperTagKey, commandName, text, resourceClassId);
      actionMenuCommand.CssClass = cssClass;
      return actionMenuCommand;
    }

    /// <summary>Creates the actions menu separator.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="name">The name.</param>
    /// <param name="wrapperTagName">Name of the wrapper tag.</param>
    /// <param name="cssClass">The CSS class.</param>
    /// <param name="text">The text.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <returns></returns>
    public static WidgetElement CreateActionMenuSeparator(
      ConfigElement parent,
      string name,
      HtmlTextWriterTag WrapperTagKey,
      string cssClass,
      string text,
      string resourceClassId)
    {
      LiteralWidgetElement actionMenuSeparator = new LiteralWidgetElement(parent);
      actionMenuSeparator.Name = name;
      actionMenuSeparator.WrapperTagKey = WrapperTagKey;
      actionMenuSeparator.CssClass = cssClass;
      actionMenuSeparator.Text = text;
      actionMenuSeparator.ResourceClassId = resourceClassId;
      actionMenuSeparator.WidgetType = typeof (LiteralWidget);
      actionMenuSeparator.IsSeparator = true;
      return (WidgetElement) actionMenuSeparator;
    }

    /// <summary>Fills the action menu items.</summary>
    /// <param name="menuItems">The menu items.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    public static void FillActionMenuItems(
      ConfigElementList<WidgetElement> menuItems,
      ConfigElement parent,
      string resourceClassId,
      bool addDuplicateActionCommand = false)
    {
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "View", HtmlTextWriterTag.Li, "preview", "View", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "ViewProperties", HtmlTextWriterTag.Li, "viewProperties", "ViewProperties", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", resourceClassId, "sfDeleteItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Publish", HtmlTextWriterTag.Li, "publish", "Publish", resourceClassId, "sfPublishItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Unpublish", HtmlTextWriterTag.Li, "unpublish", "Unpublish", resourceClassId, "sfUnpublishItm"));
      if (addDuplicateActionCommand)
        menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Duplicate", HtmlTextWriterTag.Li, "duplicate", "Duplicate", resourceClassId, "sfDuplicateItm"));
      menuItems.Add(DefinitionsHelper.CreateActionMenuSeparator((ConfigElement) menuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Content", HtmlTextWriterTag.Li, "edit", "Content", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Permissions", HtmlTextWriterTag.Li, "permissions", "Permissions", resourceClassId));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "History", HtmlTextWriterTag.Li, "historygrid", "HistoryMenuItemTitle", "VersionResources"));
    }

    /// <summary>Fills the more actions menu items.</summary>
    /// <param name="menuItems">The menu items.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    public static void FillMoreActionsMenuItems(
      ConfigElementList<WidgetElement> menuItems,
      ConfigElement parent,
      string resourceClassId)
    {
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "View", HtmlTextWriterTag.Li, "notImplemented", "View", resourceClassId));
    }

    public static CommandToolboxItemElement CreateOkDialogCommand(
      ConfigElement config)
    {
      return DefinitionsHelper.CreatePromptDialogCommand(config, "Ok", "ok", CommandType.NormalButton, "LI", (string) null);
    }

    public static CommandToolboxItemElement CreatePromptDialogCommand(
      ConfigElement parent,
      string text,
      string commandName,
      CommandType commandType,
      string wrapperTagName,
      string resourceClassId)
    {
      return DefinitionsHelper.CreateToolboxCommand(parent, text, commandName, commandType, wrapperTagName, resourceClassId);
    }

    public static CommandToolboxItemElement CreateToolboxCommand(
      ConfigElement parent,
      string text,
      string commandName,
      CommandType commandType,
      string wrapperTagName,
      string resourceClassId)
    {
      CommandToolboxItemElement toolboxCommand = new CommandToolboxItemElement(parent);
      toolboxCommand.WrapperTagName = wrapperTagName;
      toolboxCommand.CommandName = commandName;
      toolboxCommand.Text = text;
      toolboxCommand.CommandType = commandType;
      return toolboxCommand;
    }

    /// <summary>Creates the dialog element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="openOnCommandName">The name of the dialog that will fire this dialog.</param>
    /// <param name="name">The name.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public static DialogElement CreateDialogElement(
      ConfigElement parent,
      string openOnCommandName,
      string name,
      string parameters)
    {
      return new DialogElement(parent)
      {
        OpenOnCommandName = openOnCommandName,
        Name = name,
        CssClass = "sfMaximizedWindow",
        InitialBehaviors = WindowBehaviors.Maximize,
        Behaviors = WindowBehaviors.None,
        Width = Unit.Percentage(100.0),
        Height = Unit.Percentage(100.0),
        VisibleTitleBar = false,
        VisibleStatusBar = false,
        IsModal = false,
        Parameters = parameters
      };
    }

    /// <summary>Creates the modal dialog element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="openOnCommandName">The name of the dialog that will fire this dialog.</param>
    /// <param name="name">The name.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="width">Dialog's width in pixels.</param>
    /// <param name="height">Dialog's height in pixels.</param>
    /// <returns></returns>
    public static DialogElement CreateModalDialogElement(
      ConfigElement parent,
      string openOnCommandName,
      string name,
      string parameters,
      int width,
      int height)
    {
      return new DialogElement(parent)
      {
        OpenOnCommandName = openOnCommandName,
        Name = name,
        InitialBehaviors = WindowBehaviors.None,
        Behaviors = WindowBehaviors.Close,
        AutoSizeBehaviors = WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height,
        Width = Unit.Pixel(width),
        Height = Unit.Pixel(height),
        VisibleTitleBar = false,
        VisibleStatusBar = false,
        IsModal = true,
        ReloadOnShow = new bool?(true),
        Parameters = parameters
      };
    }

    /// <summary>
    /// Gets all taxonomies used by the content type and create a command item for them
    /// and respective link
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="gridView">The grid view element.</param>
    /// <param name="sidebar">The sidebar section.</param>
    public static void CreateTaxonomyLinks(WidgetBarSectionElement sidebar)
    {
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.CategoriesTaxonomyId, "hideAllWidgets", (string) null, sidebar);
      DefinitionsHelper.CreateTaxonomyLink(TaxonomyManager.TagsTaxonomyId, "hideAllWidgets", (string) null, sidebar);
    }

    /// <summary>
    /// Creates the taxonomy side bar section for a specific type.
    /// </summary>
    /// <param name="sections">The container parent section.</param>
    /// <param name="sectionElement">The specific widget bar section.</param>
    /// <param name="title">The title for the filter.</param>
    /// <param name="externalWrappingId">The external wrapper tags Ids.</param>
    public static string[] CreateTaxonomySection<T>(
      ConfigElementList<WidgetBarSectionElement> sections,
      WidgetBarSectionElement sectionElement,
      string title,
      string resourceClassId,
      params string[] externalWrappingId)
    {
      if (sections == null)
        throw new ArgumentNullException("sections == null!");
      if (sectionElement == null)
        throw new ArgumentNullException("sectionElement == null!");
      TaxonomyManager manager = TaxonomyManager.GetManager();
      using (new ElevatedModeRegion((IManager) manager))
      {
        TaxonomyPropertyDescriptor[] propertiesForType = OrganizerBase.GetPropertiesForType(typeof (T));
        List<Tuple<TaxonomyPropertyDescriptor, ITaxonomy>> source = new List<Tuple<TaxonomyPropertyDescriptor, ITaxonomy>>();
        foreach (TaxonomyPropertyDescriptor propertyDescriptor in propertiesForType)
        {
          ITaxonomy taxonomy = manager.GetTaxonomy(propertyDescriptor.TaxonomyId);
          source.Add(new Tuple<TaxonomyPropertyDescriptor, ITaxonomy>(propertyDescriptor, taxonomy));
        }
        source.Sort((Comparison<Tuple<TaxonomyPropertyDescriptor, ITaxonomy>>) ((a, b) => a.Item2.Name.CompareTo(b.Item2.Name)));
        string[] array = source.Select<Tuple<TaxonomyPropertyDescriptor, ITaxonomy>, string>((Func<Tuple<TaxonomyPropertyDescriptor, ITaxonomy>, string>) (t => string.Format("{0}FilterSection", (object) t.Item2.Name.TocamelCase()))).Union<string>((IEnumerable<string>) externalWrappingId).ToArray<string>();
        foreach (Tuple<TaxonomyPropertyDescriptor, ITaxonomy> tuple in source)
        {
          ITaxonomy taxonomy = tuple.Item2;
          TaxonomyPropertyDescriptor propertyDescriptor = tuple.Item1;
          string str = taxonomy.Name.TocamelCase();
          WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) sections)
          {
            Name = taxonomy.Name,
            CssClass = "sfFilterBy sfSeparator",
            WrapperTagId = string.Format("{0}FilterSection", (object) str),
            Visible = new bool?(false)
          };
          string key = string.Format("{0}ItemsBy{1}", (object) title, (object) taxonomy.TaxonName);
          if (!string.IsNullOrEmpty(Res.Get(resourceClassId, key, SystemManager.CurrentContext.Culture, true, false)))
          {
            element1.ResourceClassId = resourceClassId;
            element1.Title = key;
          }
          else
            element1.Title = string.Format("{0} items by {1}", (object) title, (object) taxonomy.TaxonName);
          sections.Add(element1);
          ConfigElementList<WidgetElement> items = element1.Items;
          CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
          element2.Name = string.Format("Close{0}", (object) str);
          element2.CommandName = "showSectionsExceptAndResetFilter";
          element2.CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(array);
          element2.ButtonType = CommandButtonType.SimpleLinkButton;
          element2.Text = string.Format("Close {0}", (object) taxonomy.Name);
          element2.CssClass = "sfCloseFilter";
          element2.WidgetType = typeof (CommandWidget);
          element2.IsSeparator = false;
          items.Add((WidgetElement) element2);
          DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) element1.Items);
          commandWidgetElement.Name = string.Format("{0}Filter", (object) taxonomy.Name);
          commandWidgetElement.CommandName = string.Format("filterBy_Classification_{0}", (object) propertyDescriptor.Name);
          commandWidgetElement.IsFilterCommand = true;
          commandWidgetElement.WidgetType = typeof (DynamicCommandWidget);
          commandWidgetElement.IsSeparator = false;
          commandWidgetElement.SelectedItemCssClass = "sfSel";
          commandWidgetElement.ParentDataKeyName = "ParentTaxonId";
          DynamicCommandWidgetElement element3 = commandWidgetElement;
          if (propertyDescriptor.TaxonomyType == TaxonomyType.Flat)
          {
            element3.PageSize = 30;
            element3.MoreLinkText = string.Format("Show more {0}", (object) taxonomy.Name.ToLower());
            element3.MoreLinkCssClass = "sfShowMore";
            element3.LessLinkText = string.Format("Show less {0}", (object) taxonomy.Name.ToLower());
            element3.LessLinkCssClass = "sfShowMore";
          }
          else if (propertyDescriptor.TaxonomyType == TaxonomyType.Hierarchical)
            element3.PageSize = 0;
          element3.ClientItemTemplate = "<a href='javascript:void(0);' class='sf_binderCommand_" + element3.CommandName + "'>{{ Title.htmlEncode() }}</a> <span class='sfCount'>({{ItemsCount}})</span>";
          element3.SortExpression = "Title";
          if (taxonomy.GetType() == typeof (FlatTaxonomy))
          {
            element3.BindTo = BindCommandListTo.Client;
            element3.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", (object) taxonomy.Id);
          }
          else if (taxonomy.GetType() == typeof (HierarchicalTaxonomy))
          {
            element3.BindTo = BindCommandListTo.HierarchicalData;
            element3.BaseServiceUrl = string.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", (object) taxonomy.Id);
            element3.ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/";
            element3.PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/";
          }
          element3.UrlParameters.Add("itemType", typeof (T).AssemblyQualifiedName);
          element1.Items.Add((WidgetElement) element3);
          DefinitionsHelper.CreateTaxonomyLink(taxonomy.Id, "hideSectionsExcept", DefinitionsHelper.ConstructDisplaySectionsCommandArgument(element1.WrapperTagId), sectionElement);
        }
        return array;
      }
    }

    /// <summary>Creates the recycle bin section for a specific type.</summary>
    /// <param name="configElementList">The config element list parent section.</param>
    internal static void CreateRecycleBinLink(
      ConfigElementList<WidgetBarSectionElement> configElementList,
      string contentTypeName)
    {
      string recycleBinPageUrl = RouteHelper.ResolveUrl(RouteHelper.CreateNodeReference(Telerik.Sitefinity.Constants.RecycleBinGroupPageId), UrlResolveOptions.Rooted);
      DefinitionsHelper.CreateRecycleBinLink(configElementList, recycleBinPageUrl, contentTypeName);
    }

    /// <summary>Creates the recycle bin link.</summary>
    /// <param name="configElementList">The config element list.</param>
    /// <param name="recycleBinPageUrl">The recycle bin page URL.</param>
    /// <param name="contentTypeName">Name of the content type.</param>
    internal static void CreateRecycleBinLink(
      ConfigElementList<WidgetBarSectionElement> configElementList,
      string recycleBinPageUrl,
      string contentTypeName)
    {
      WidgetBarSectionElement barSectionElement = new WidgetBarSectionElement((ConfigElement) configElementList);
      barSectionElement.Name = "recycleBinWidget";
      barSectionElement.Title = string.Empty;
      barSectionElement.CssClass = "sfWidgetsList";
      barSectionElement.WrapperTagId = "recycleBinSection";
      barSectionElement.ModuleName = "RecycleBin";
      WidgetBarSectionElement element1 = barSectionElement;
      ConfigElementList<WidgetElement> items = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "ExternalLink2";
      element2.CommandName = "internalLinkCommand";
      element2.ButtonType = CommandButtonType.SimpleLinkButton;
      element2.Text = "Recycle Bin";
      element2.CssClass = "sfRecycleBinWidget";
      element2.WidgetType = typeof (CommandWidget);
      element2.IsSeparator = false;
      element2.OpenInSameWindow = true;
      element2.NavigateUrl = string.Format("{0}#/filter/type/{1}", (object) recycleBinPageUrl, (object) contentTypeName);
      items.Add((WidgetElement) element2);
      configElementList.Add(element1);
    }

    internal static void AppendStatusFilters(ConfigElementCollection parent)
    {
      ConfigElementCollection elementCollection = parent;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) parent);
      element.Name = "StatusFilterTemplate";
      element.ButtonType = CommandButtonType.SimpleLinkButton;
      element.CssClass = "";
      element.WidgetType = typeof (CommandWidget);
      element.IsSeparator = false;
      element.Visible = new bool?(false);
      elementCollection.Add((ConfigElement) element);
    }

    public static void CreateTaxonomyLink(
      Guid id,
      string commandName,
      string commandArgument,
      WidgetBarSectionElement sidebar)
    {
      Taxonomy taxonomy = TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == id)).SingleOrDefault<Taxonomy>();
      if (taxonomy == null)
        return;
      string str = HttpUtility.HtmlEncode((string) taxonomy.Title);
      string name = taxonomy.Name;
      ConfigElementList<WidgetElement> items = sidebar.Items;
      CommandWidgetElement element = new CommandWidgetElement((ConfigElement) sidebar.Items);
      element.Name = name + "Command";
      element.CommandName = commandName;
      element.CommandArgument = commandArgument;
      element.ButtonType = CommandButtonType.SimpleLinkButton;
      element.Text = "by " + str + "...";
      element.WidgetType = typeof (CommandWidget);
      items.Add((WidgetElement) element);
    }

    /// <summary>Creates the toolbar in the backend details form.</summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
    internal static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      string resourceClassId,
      bool isCreateMode)
    {
      DefinitionsHelper.CreateBackendFormToolbar(detailView, resourceClassId, isCreateMode, "ThisItem", false, false, showPermissions: false);
    }

    /// <summary>
    /// Creates the toolbar in the backend details form for preview mode.
    /// </summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
    /// <param name="itemName">Name of the item.</param>
    internal static void CreateHistoryPreviewToolbar(
      DetailFormViewElement detailView,
      string resourceClassId)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "History",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfDetachedBtnArea"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "CopyAsNewestWidgetElement";
      element2.ButtonType = CommandButtonType.Standard;
      element2.CommandName = "restoreVersionAsNew";
      element2.Text = "CopyAsNewest";
      element2.ResourceClassId = typeof (Labels).Name;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "DeleteVersionWidgetElement";
      element3.ButtonType = CommandButtonType.Standard;
      element3.CommandName = "delete";
      element3.Text = "Delete";
      element3.ResourceClassId = resourceClassId;
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "CancelWidgetElement";
      element4.ButtonType = CommandButtonType.Cancel;
      element4.CommandName = "cancel";
      element4.Text = "BackToRevisionHistory";
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.WidgetType = typeof (CommandWidget);
      element4.ResourceClassId = typeof (Labels).Name;
      items3.Add((WidgetElement) element4);
      detailView.Toolbar.Sections.Add(element1);
    }

    /// <summary>
    /// Creates the toolbar in the backend details form for preview mode.
    /// </summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
    /// <param name="itemName">Name of the item.</param>
    internal static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      string resourceClassId,
      bool isCreateMode,
      string itemName,
      bool addRevisionHistory)
    {
      DefinitionsHelper.CreateBackendFormToolbar(detailView, resourceClassId, isCreateMode, itemName, addRevisionHistory, true);
    }

    /// <summary>Creates the toolbar in the backend details form.</summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
    /// <param name="itemName">Name of the item.</param>
    internal static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      string resourceClassId,
      bool isCreateMode,
      string itemName,
      bool addRevisionHistory,
      bool showPreview,
      string backToItems = "BackToItems",
      bool showPermissions = true)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "BackendForm",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "SaveChangesWidgetElement";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = isCreateMode ? "Create" + itemName : "SaveChanges";
      element2.ResourceClassId = resourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      if (showPreview)
      {
        ConfigElementList<WidgetElement> items2 = element1.Items;
        CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
        element3.Name = "PreviewWidgetElement";
        element3.ButtonType = CommandButtonType.Standard;
        element3.CommandName = "preview";
        element3.Text = "Preview";
        element3.ResourceClassId = typeof (Labels).Name;
        element3.WrapperTagKey = HtmlTextWriterTag.Span;
        element3.WidgetType = typeof (CommandWidget);
        items2.Add((WidgetElement) element3);
      }
      if (!isCreateMode)
      {
        ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) element1.Items);
        menuWidgetElement.Name = "moreActions";
        menuWidgetElement.Text = "MoreActionsLink";
        menuWidgetElement.ResourceClassId = typeof (Labels).Name;
        menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Div;
        menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
        menuWidgetElement.CssClass = "sfInlineBlock sfAlignMiddle";
        ActionMenuWidgetElement element4 = menuWidgetElement;
        ConfigElementList<WidgetElement> menuItems1 = element4.MenuItems;
        CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
        element5.Name = "delete";
        element5.Text = "DeleteThisItem";
        element5.CommandName = "delete";
        element5.ResourceClassId = resourceClassId;
        element5.WidgetType = typeof (CommandWidget);
        element5.CssClass = "sfDeleteItm";
        menuItems1.Add((WidgetElement) element5);
        if (addRevisionHistory)
        {
          ConfigElementList<WidgetElement> menuItems2 = element4.MenuItems;
          CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
          element6.Name = "history";
          element6.Text = "ReviewHistory";
          element6.CommandName = "history";
          element6.ResourceClassId = resourceClassId;
          element6.WidgetType = typeof (CommandWidget);
          menuItems2.Add((WidgetElement) element6);
        }
        if (showPermissions)
        {
          ConfigElementList<WidgetElement> menuItems3 = element4.MenuItems;
          CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element4.MenuItems);
          element7.Name = "permissions";
          element7.ButtonType = CommandButtonType.SimpleLinkButton;
          element7.Text = "SetPermissions";
          element7.CommandName = "permissions";
          element7.ResourceClassId = resourceClassId;
          element7.WidgetType = typeof (CommandWidget);
          menuItems3.Add((WidgetElement) element7);
        }
        element1.Items.Add((WidgetElement) element4);
      }
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element8 = new CommandWidgetElement((ConfigElement) element1.Items);
      element8.Name = "CancelWidgetElement";
      element8.ButtonType = CommandButtonType.Cancel;
      element8.CommandName = "cancel";
      element8.Text = backToItems;
      element8.ResourceClassId = resourceClassId;
      element8.WrapperTagKey = HtmlTextWriterTag.Span;
      element8.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element8);
      detailView.Toolbar.Sections.Add(element1);
    }

    /// <summary>Creates the toolbar in the comment backend form.</summary>
    /// <param name="detailView">The detail view.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    internal static void CreateCommentBackendFormToolbar(
      DetailFormViewElement detailView,
      string resourceClassId)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "Comments",
        WrapperTagKey = HtmlTextWriterTag.Div
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "Edit";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = "SaveChanges";
      element2.ResourceClassId = resourceClassId;
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CancelWidgetElement";
      element3.ButtonType = CommandButtonType.Cancel;
      element3.CommandName = "cancel";
      element3.Text = "Cancel";
      element3.ResourceClassId = resourceClassId;
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = element1.Items;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element1.Items);
      element4.Name = "DeleteWidgetElement";
      element4.ButtonType = CommandButtonType.Standard;
      element4.CommandName = "delete";
      element4.Text = "Delete";
      element4.ResourceClassId = resourceClassId;
      element4.WrapperTagKey = HtmlTextWriterTag.Span;
      element4.CssClass = "sfDeleteLinkBtn";
      element4.WidgetType = typeof (CommandWidget);
      items3.Add((WidgetElement) element4);
      detailView.Toolbar.Sections.Add(element1);
    }

    /// <summary>
    /// Creates the link which displays 'Not implemented' alert.
    /// </summary>
    /// <param name="gridView">The grid view.</param>
    public static void CreateNotImplementedLink(MasterGridViewElement gridView)
    {
      LinkElement element = new LinkElement((ConfigElement) gridView.LinksConfig)
      {
        Name = "notImplementedLink",
        CommandName = "notImplemented",
        NavigateUrl = string.Format("javascript:alert('{0}');", (object) Res.Get<Labels>().InProcessOfImplementationNoHtml)
      };
      gridView.LinksConfig.Add(element);
    }

    /// <summary>Get base url</summary>
    /// <returns></returns>
    public static string GetBaseUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : siteMapNode.Url;
    }

    /// <summary>Creates a default search button widget element.</summary>
    /// <param name="parent">The parent of the search button widget element to return.</param>
    /// <returns></returns>
    public static WidgetElement CreateSearchButtonWidget(
      ConfigElement parent,
      Type persistentTypeToSearch,
      string commandName)
    {
      SearchWidgetElement searchButtonWidget = new SearchWidgetElement(parent);
      searchButtonWidget.Name = commandName;
      searchButtonWidget.WidgetType = typeof (SearchWidget);
      searchButtonWidget.CommandName = commandName;
      searchButtonWidget.PersistentTypeToSearch = persistentTypeToSearch;
      searchButtonWidget.CssClass = "sfSeparator";
      return (WidgetElement) searchButtonWidget;
    }

    /// <summary>/ Creates a default search button widget element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="persistentTypeToSearch">The persistent type to search.</param>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="closeSearchCommandName">Name of the close search command.</param>
    public static WidgetElement CreateSearchButtonWidget(
      ConfigElement parent,
      Type persistentTypeToSearch,
      string commandName,
      string closeSearchCommandName)
    {
      SearchWidgetElement searchButtonWidget = new SearchWidgetElement(parent);
      searchButtonWidget.Name = commandName;
      searchButtonWidget.WidgetType = typeof (SearchWidget);
      searchButtonWidget.CommandName = commandName;
      searchButtonWidget.CloseSearchCommandName = closeSearchCommandName;
      searchButtonWidget.PersistentTypeToSearch = persistentTypeToSearch;
      searchButtonWidget.CssClass = "sfSeparator";
      return (WidgetElement) searchButtonWidget;
    }

    /// <summary>Creates a default search button widget element.</summary>
    /// <param name="parent">The parent of the search button widget element to return.</param>
    /// <returns></returns>
    public static WidgetElement CreateSearchButtonWidget(
      ConfigElement parent,
      Type persistentTypeToSearch)
    {
      return DefinitionsHelper.CreateSearchButtonWidget(parent, persistentTypeToSearch, "search");
    }

    /// <summary>Gets the extenal client scripts.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetExtenalClientScripts(
      string key,
      string value)
    {
      return new Dictionary<string, string>()
      {
        {
          key,
          value
        }
      };
    }

    /// <summary>
    /// Constructs the show sections except command argument from list of sectionId
    /// to be excluded.
    /// </summary>
    /// <param name="sectionIds">The section ids.</param>
    /// <returns></returns>
    public static string ConstructDisplaySectionsCommandArgument(params string[] sectionIds)
    {
      Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
      List<string> stringList = new List<string>();
      foreach (string sectionId in sectionIds)
        stringList.Add(sectionId);
      dictionary.Add(nameof (sectionIds), stringList);
      return DefinitionsHelper.jsSerializer.Serialize((object) dictionary);
    }

    /// <summary>
    /// Constructs a sipmle command argument with a value property.
    /// </summary>
    /// <param name="value">The value of the command argument.</param>
    /// <returns>The serialized command argument.</returns>
    public static string ConstructValueCommandArgument(object value)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>()
      {
        {
          nameof (value),
          value
        }
      };
      return DefinitionsHelper.jsSerializer.Serialize((object) dictionary);
    }

    /// <summary>
    /// Constructs a sipmle command argument with a value property.
    /// </summary>
    /// <param name="value">The value of the command argument.</param>
    /// <returns>The serialized command argument.</returns>
    public static string ConstructFilterCommandArgument(object value)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>()
      {
        {
          "filterExpression",
          value
        }
      };
      return DefinitionsHelper.jsSerializer.Serialize((object) dictionary);
    }

    /// <summary>Gets the predefined date filtering ranges.</summary>
    /// <returns></returns>
    public static void GetPredefinedDateFilteringRanges(
      ConfigElementList<FilterRangeElement> dictionary)
    {
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(1, 0, 0, 0).ToString(), Res.Get<Labels>().LastOneDay));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(2, 0, 0, 0).ToString(), Res.Get<Labels>().LastThreeDays));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(7, 0, 0, 0).ToString(), Res.Get<Labels>().LastOneWeek));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(31, 0, 0, 0).ToString(), Res.Get<Labels>().LastOneMonth));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(186, 0, 0, 0).ToString(), Res.Get<Labels>().LastSixMonths));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(365, 0, 0, 0).ToString(), Res.Get<Labels>().LastOneYear));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(730, 0, 0, 0).ToString(), Res.Get<Labels>().LastTwoYears));
      dictionary.Add(DefinitionsHelper.CreateFilterRangeElement((ConfigElement) dictionary, new TimeSpan(1825, 0, 0, 0).ToString(), Res.Get<Labels>().LastFiveYears));
    }

    private static FilterRangeElement CreateFilterRangeElement(
      ConfigElement parent,
      string key,
      string value)
    {
      return new FilterRangeElement(parent)
      {
        Key = key,
        Value = value
      };
    }

    /// <summary>
    /// Adds a predefined definition for the comments in a separate section.
    /// </summary>
    /// <param name="detailView">The detail view to wich to add the section.</param>
    public static void AddCommentsFieldSectionDefinition(
      DetailViewDefinitionFacade fluentDetailView,
      string ResourceClassName,
      FieldDisplayMode displayMode)
    {
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade = fluentDetailView.AddSection("commentsConfigSection");
      if (displayMode != FieldDisplayMode.Write)
        return;
      definitionFacade.AddAllowCommentsExpandableFieldAndContinue();
    }

    /// <summary>Creates the dynamic item element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="title">The title.</param>
    /// <param name="value">The value.</param>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public static DynamicItemElement CreateDynamicItemElement(
      ConfigElement parent,
      string title,
      string value,
      string resourceKey,
      NameValueCollection parameters)
    {
      DynamicItemElement dynamicItemElement = new DynamicItemElement(parent);
      dynamicItemElement.Title = title;
      dynamicItemElement.Value = value;
      if (resourceKey != null)
        dynamicItemElement.ResourceClassId = resourceKey;
      return dynamicItemElement;
    }

    /// <summary>
    /// Gets the combined filter expression for the given <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDefinition" /> instance.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    public static string GetFilterExpression(IContentViewMasterDefinition definition) => DefinitionsHelper.GetFilterExpression(definition.FilterExpression, definition.AdditionalFilter);

    /// <summary>
    /// Gets the combined filter expression for the given <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDefinition" /> instance.
    /// </summary>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="additionalFilter">The additional filter.</param>
    /// <returns></returns>
    public static string GetFilterExpression(string filterExpression, QueryData additionalFilter)
    {
      string str = (string) null;
      if (additionalFilter != null)
        str = LinqTranslator.ToDynamicLinq(additionalFilter);
      return string.IsNullOrEmpty(filterExpression) || string.IsNullOrEmpty(str) ? (string.IsNullOrEmpty(str) ? filterExpression : str) : string.Format("({0}) AND ({1})", (object) filterExpression, (object) str);
    }

    internal static TSection GetConfig<TSection>(ConfigElement element) where TSection : ConfigSection, new() => element.Section is TSection ? (TSection) element.Section : Telerik.Sitefinity.Configuration.Config.Get<TSection>();

    internal static string GetDefaultProvider(ConfigElement config) => config.Section is ContentModuleConfigBase ? ((ModuleConfigBase) config.Section).DefaultProvider : string.Empty;

    internal static IEnumerable<SortingExpressionElement> GetSortingExpressionSettings(
      Type type,
      bool isCustom,
      ConfigSection configSection)
    {
      if (!(configSection is ContentViewConfig contentViewConfig) && SystemManager.Initializing)
      {
        IDictionary httpContextItems = SystemManager.HttpContextItems;
        if (httpContextItems != null && httpContextItems[(object) "sf_InstallContext"] is InstallContext installContext)
          contentViewConfig = installContext.GetConfig<ContentViewConfig>();
      }
      if (contentViewConfig == null)
        contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      return contentViewConfig.SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == type.FullName && s.IsCustom == isCustom));
    }

    internal static void AddPersonalizationColumn(GridViewModeElement parent, bool isBackend)
    {
      DynamicColumnElement dynamicColumnElement = new DynamicColumnElement((ConfigElement) parent.ColumnsConfig);
      dynamicColumnElement.Name = "Personalization";
      dynamicColumnElement.HeaderText = "";
      dynamicColumnElement.DynamicMarkupGenerator = typeof (PersonalizationColumnMarkupGenerator);
      dynamicColumnElement.ItemCssClass = "sfPersonalized";
      dynamicColumnElement.HeaderCssClass = "sfPersonalized";
      dynamicColumnElement.DisableSorting = new bool?(true);
      dynamicColumnElement.ModuleName = "Personalization";
      DynamicColumnElement element = dynamicColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    internal static void AddVariationsColumn(GridViewModeElement parent, bool isBackend)
    {
      DynamicColumnElement dynamicColumnElement = new DynamicColumnElement((ConfigElement) parent.ColumnsConfig);
      dynamicColumnElement.Name = "Variations";
      dynamicColumnElement.HeaderText = "";
      dynamicColumnElement.DynamicMarkupGenerator = typeof (VariationColumnMarkupGenerator);
      dynamicColumnElement.ItemCssClass = "sfPersonalized";
      dynamicColumnElement.HeaderCssClass = "sfPersonalized";
      dynamicColumnElement.DisableSorting = new bool?(true);
      DynamicColumnElement element = dynamicColumnElement;
      parent.ColumnsConfig.Add((ColumnElement) element);
    }

    internal static void AddTranslationsColumn(GridViewModeElement parent, bool isBackend)
    {
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) parent.ColumnsConfig);
      dynamicColumnElement1.Name = "Translations";
      dynamicColumnElement1.HeaderText = "Translations";
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (LanguagesColumnMarkupGenerator);
      dynamicColumnElement1.ItemCssClass = "sfLanguagesCol";
      dynamicColumnElement1.HeaderCssClass = "sfLanguagesCol";
      dynamicColumnElement1.DisableSorting = new bool?(true);
      DynamicColumnElement dynamicColumnElement2 = dynamicColumnElement1;
      dynamicColumnElement2.GeneratorSettingsElement = (DynamicMarkupGeneratorElement) new LanguagesColumnMarkupGeneratorElement((ConfigElement) dynamicColumnElement2)
      {
        LanguageSource = (isBackend ? LanguageSource.Backend : LanguageSource.Frontend),
        ItemsInGroupCount = 6,
        ContainerTag = "div",
        GroupTag = "div",
        ItemTag = "div",
        ContainerClass = string.Empty,
        GroupClass = string.Empty,
        ItemClass = string.Empty
      };
      parent.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
    }

    internal static void AddLanguageSection(
      MasterGridViewElement pagesBackendMaster,
      bool isBackend)
    {
      LocalizationWidgetBarSectionElement barSectionElement = new LocalizationWidgetBarSectionElement((ConfigElement) pagesBackendMaster.SidebarConfig.Sections);
      barSectionElement.Name = "Languages";
      barSectionElement.Title = "Languages";
      barSectionElement.ResourceClassId = typeof (PageResources).Name;
      barSectionElement.CssClass = "sfFirst sfSeparator sfLangSelector";
      barSectionElement.WrapperTagId = "languagesSection";
      LocalizationWidgetBarSectionElement element1 = barSectionElement;
      ConfigElementList<WidgetElement> items = element1.Items;
      LanguagesDropDownListWidgetElement element2 = new LanguagesDropDownListWidgetElement((ConfigElement) element1.Items);
      element2.Name = "Languages";
      element2.Text = "Languages";
      element2.CssClass = "";
      element2.WidgetType = typeof (LanguagesDropDownListWidget);
      element2.IsSeparator = false;
      element2.LanguageSource = isBackend ? LanguageSource.Backend : LanguageSource.Frontend;
      element2.AddAllLanguagesOption = false;
      element2.CommandName = "changeLanguage";
      items.Add((WidgetElement) element2);
      pagesBackendMaster.SidebarConfig.Sections.Add((WidgetBarSectionElement) element1);
    }

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (DefinitionsHelper.regexStrategy == null)
        {
          try
          {
            DefinitionsHelper.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
          }
          catch (Exception ex)
          {
            DefinitionsHelper.regexStrategy = new RegexStrategy();
          }
        }
        return DefinitionsHelper.regexStrategy;
      }
    }

    internal static void AddPageStatsColumn(GridViewModeElement treeTableMode)
    {
      DynamicColumnElement dynamicColumnElement = new DynamicColumnElement((ConfigElement) treeTableMode.ColumnsConfig);
      dynamicColumnElement.Name = "GoogleStats";
      dynamicColumnElement.HeaderText = "Analytics";
      dynamicColumnElement.ItemCssClass = "sfStatsColumn";
      dynamicColumnElement.HeaderCssClass = "sfStatsColumn";
      dynamicColumnElement.DisableSorting = new bool?(true);
      dynamicColumnElement.ModuleName = "Analytics";
      dynamicColumnElement.TitleText = "Analytics";
      dynamicColumnElement.DynamicMarkupGenerator = typeof (GoogleStatsColumnGenerator);
      DynamicColumnElement element = dynamicColumnElement;
      treeTableMode.ColumnsConfig.Add((ColumnElement) element);
    }

    internal static void AddContentStatsColumn(
      GridViewModeElement treeTableMode,
      string contentType)
    {
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) treeTableMode.ColumnsConfig);
      dynamicColumnElement1.Name = "GoogleStats";
      dynamicColumnElement1.HeaderText = "Analytics";
      dynamicColumnElement1.ItemCssClass = "sfStatsColumn";
      dynamicColumnElement1.HeaderCssClass = "sfStatsColumn";
      dynamicColumnElement1.DisableSorting = new bool?(true);
      dynamicColumnElement1.ModuleName = "Analytics";
      dynamicColumnElement1.TitleText = "Analytics";
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (GoogleStatsContentColumnGenerator);
      DynamicColumnElement dynamicColumnElement2 = dynamicColumnElement1;
      dynamicColumnElement2.GeneratorSettingsElement = new DynamicMarkupGeneratorElement((ConfigElement) dynamicColumnElement2)
      {
        Settings = contentType
      };
      treeTableMode.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
    }

    internal static void AddDynamicContentStatsColumn(
      GridViewModeElement treeTableMode,
      string contentType)
    {
      DynamicColumnElement dynamicColumnElement1 = new DynamicColumnElement((ConfigElement) treeTableMode.ColumnsConfig);
      dynamicColumnElement1.Name = "GoogleStats";
      dynamicColumnElement1.HeaderText = "Analytics";
      dynamicColumnElement1.ItemCssClass = "sfStatsColumn";
      dynamicColumnElement1.HeaderCssClass = "sfStatsColumn";
      dynamicColumnElement1.DisableSorting = new bool?(true);
      dynamicColumnElement1.ModuleName = "Analytics";
      dynamicColumnElement1.TitleText = "Analytics";
      dynamicColumnElement1.DynamicMarkupGenerator = typeof (GoogleStatsDynamicContentColumnGenerator);
      DynamicColumnElement dynamicColumnElement2 = dynamicColumnElement1;
      dynamicColumnElement2.GeneratorSettingsElement = new DynamicMarkupGeneratorElement((ConfigElement) dynamicColumnElement2)
      {
        Settings = contentType
      };
      treeTableMode.ColumnsConfig.Add((ColumnElement) dynamicColumnElement2);
    }

    public interface IAddResourceStringFluent
    {
      DefinitionsHelper.IAddResourceStringFluent AddResourceString(
        string resourceClass,
        string messageKey);
    }

    private class AddResourceStringFluent : DefinitionsHelper.IAddResourceStringFluent
    {
      private ContentViewDefinitionElement cfg;

      public AddResourceStringFluent(ContentViewDefinitionElement config) => this.cfg = config;

      public DefinitionsHelper.IAddResourceStringFluent AddResourceString(
        string resourceClass,
        string messageKey)
      {
        this.cfg.LabelsConfig.Add(new LabelDefinitionElement((ConfigElement) this.cfg.LabelsConfig)
        {
          CompoundKey = resourceClass + messageKey,
          ClassId = resourceClass,
          MessageKey = messageKey
        });
        return (DefinitionsHelper.IAddResourceStringFluent) this;
      }
    }
  }
}
