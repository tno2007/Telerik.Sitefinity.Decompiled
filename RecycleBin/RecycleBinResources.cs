// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("RecycleBinResources", ResourceClassId = "RecycleBinResources", TitlePlural = "RecycleBinResourcesTitlePlural")]
  public class RecycleBinResources : Resource
  {
    /// <summary>Gets the recycle bin module title</summary>
    [ResourceEntry("RecycleBinResourcesTitle", Description = "The title of this class.", LastModified = "2014/05/17", Value = "Recycle Bin")]
    public string RecycleBinResourcesTitle => this[nameof (RecycleBinResourcesTitle)];

    /// <summary>Gets the recycle bin title</summary>
    [ResourceEntry("RecycleBinTitle", Description = "The title of this class.", LastModified = "2020/02/04", Value = "Recycle bin")]
    public string RecycleBinTitle => this[nameof (RecycleBinTitle)];

    /// <summary>Gets the recycle bin module title in plural</summary>
    /// <value>Title plural for the Recycle bin module.</value>
    [ResourceEntry("RecycleBinResourcesTitlePlural", Description = "Title plural for the Recycle bin module.", LastModified = "2014/05/17", Value = "Recycle Bin")]
    public string RecycleBinResourcesTitlePlural => this[nameof (RecycleBinResourcesTitlePlural)];

    /// <summary>Gets the Enable Recycle Bin phrase</summary>
    [ResourceEntry("EnableRecycleBinSetting", Description = "Phrase: Enable Recycle Bin", LastModified = "2014/05/19", Value = "Enable Recycle Bin")]
    public string EnableRecycleBinSetting => this[nameof (EnableRecycleBinSetting)];

    /// <summary>Gets the Disable Recycle Bin phrase</summary>
    [ResourceEntry("DisableRecycleBinSetting", Description = "Phrase: Disable Recycle Bin", LastModified = "2014/05/19", Value = "Disable Recycle Bin")]
    public string DisableRecycleBinSetting => this[nameof (DisableRecycleBinSetting)];

    /// <summary>
    /// Gets the Keep items in the Recycle Bin no longer than... phrase
    /// </summary>
    [ResourceEntry("RecycleBinPeriodSettingTitle", Description = " Phrase: Keep items in the Recycle Bin no longer than...", LastModified = "2014/05/19", Value = "Keep items in the Recycle Bin no longer than...")]
    public string RecycleBinPeriodSettingTitle => this[nameof (RecycleBinPeriodSettingTitle)];

    /// <summary>
    /// Gets the After this period items in the Recycle Bin are automatically deleted forever phrase
    /// </summary>
    [ResourceEntry("RecycleBinPeriodSettingDescription", Description = " Phrase: After this period items in the Recycle Bin are automatically deleted forever", LastModified = "2014/05/19", Value = "After this period items in the Recycle Bin are automatically deleted forever.")]
    public string RecycleBinPeriodSettingDescription => this[nameof (RecycleBinPeriodSettingDescription)];

    /// <summary>Gets the title of Recycle bin page.</summary>
    /// <value>Recycle Bin</value>
    [ResourceEntry("RecycleBinGroupPageTitle", Description = "The title of Recycle bin page.", LastModified = "2014/05/27", Value = "Recycle Bin")]
    public string RecycleBinGroupPageTitle => this[nameof (RecycleBinGroupPageTitle)];

    /// <summary>Gets the URL name of Recycle bin page.</summary>
    /// <value>Recycle Bin</value>
    [ResourceEntry("RecycleBinGroupPageUrlName", Description = "The URL name of Recycle bin page.", LastModified = "2014/05/27", Value = "RecycleBin")]
    public string RecycleBinGroupPageUrlName => this[nameof (RecycleBinGroupPageUrlName)];

    /// <summary>Gets the description of Recycle bin page.</summary>
    /// <value>Recycle Bin</value>
    [ResourceEntry("RecycleBinGroupPageDescription", Description = "The description of Recycle bin page.", LastModified = "2014/05/27", Value = "Recycle Bin")]
    public string RecycleBinGroupPageDescription => this[nameof (RecycleBinGroupPageDescription)];

    /// <summary>Gets the URL name of Recycle bin page.</summary>
    /// <value>Recycle Bin</value>
    [ResourceEntry("RecycleBinMasterPageUrl", Description = "The URL name of Recycle bin page.", LastModified = "2014/05/27", Value = "RecycleBin")]
    public string RecycleBinMasterPageUrl => this[nameof (RecycleBinMasterPageUrl)];

    /// <summary>Gets the unexpected lifecycle status error phrase</summary>
    [ResourceEntry("UnexpectedLifecycleStatusError", Description = " Phrase: Unexpected Lifecycle status of data item with id:{0} status: {1}. Only master items support the deleted status", LastModified = "2014/05/28", Value = "Unexpected Lifecycle status of data item with id:{0} status: {1}. Only master items support the deleted status.")]
    public string UnexpectedLifecycleStatusError => this[nameof (UnexpectedLifecycleStatusError)];

    /// <summary>Gets the existing page URL message header text.</summary>
    /// <value>The existing page URL message header text.</value>
    [ResourceEntry("ExistingPageUrlMessageHeaderText", Description = " Phrase: Oops. Existing URL", LastModified = "2014/07/07", Value = "Oops. Existing URL")]
    public string ExistingPageUrlMessageHeaderText => this[nameof (ExistingPageUrlMessageHeaderText)];

    /// <summary>Gets the existing page URL message body text.</summary>
    /// <value>The existing page URL message body text.</value>
    [ResourceEntry("ExistingPageUrlMessageBodyText", Description = " Phrase: <p>The page cannot be restored because a page with the same URL already exists: <i>{0}</i></p><p>In order to restore this page, you should delete the existing one or change its URL or parent.</p>", LastModified = "2014/07/07", Value = "<p>The page cannot be restored because a page with the same URL already exists: <i>{0}</i></p><p>In order to restore this page, you should delete the existing one or change its URL or parent.</p>")]
    public string ExistingPageUrlMessageBodyText => this[nameof (ExistingPageUrlMessageBodyText)];

    /// <summary>
    /// Gets the existing page URL message action button text.
    /// </summary>
    /// <value>The existing page URL message action button text.</value>
    [ResourceEntry("ExistingPageUrlMessageActionButtonText", Description = " Phrase: OK", LastModified = "2014/07/07", Value = "OK")]
    public string ExistingPageUrlMessageActionButtonText => this[nameof (ExistingPageUrlMessageActionButtonText)];

    /// <summary>
    /// Gets the existing content item URL message header text.
    /// </summary>
    /// <value>The existing URL message header text.</value>
    [ResourceEntry("ExistingContentItemUrlMessageHeaderText", Description = " Phrase: Oops. Existing URL", LastModified = "2014/07/07", Value = "Oops. Existing URL")]
    public string ExistingContentItemUrlMessageHeaderText => this[nameof (ExistingContentItemUrlMessageHeaderText)];

    /// <summary>Gets the existing content item URL message body text.</summary>
    /// <value>The existing content item URL message body text.</value>
    [ResourceEntry("ExistingContentItemUrlMessageBodyText", Description = " Phrase: The item cannot be restored because a item with the same URL already exists: <i>{0}</i><br />In order to restore this item, you should delete the existing one or change its URL or parent.", LastModified = "2014/07/07", Value = "The item cannot be restored because a item with the same URL already exists: <i>{0}</i><br />In order to restore this item, you should delete the existing one or change its URL or parent.")]
    public string ExistingContentItemUrlMessageBodyText => this[nameof (ExistingContentItemUrlMessageBodyText)];

    /// <summary>
    /// Gets the existing content item URL message action button text.
    /// </summary>
    /// <value>The existing content item URL message action button text.</value>
    [ResourceEntry("ExistingContentItemUrlMessageActionButtonText", Description = " Phrase: OK", LastModified = "2014/07/07", Value = "OK")]
    public string ExistingContentItemUrlMessageActionButtonText => this[nameof (ExistingContentItemUrlMessageActionButtonText)];

    /// <summary>Gets the missing template message header text.</summary>
    /// <value>The missing template message header text.</value>
    [ResourceEntry("MissingTemplateMessageHeaderText", Description = " Phrase: Missing template. Page will be restored to draft", LastModified = "2014/07/07", Value = "Missing template. Page will be restored to draft")]
    public string MissingTemplateMessageHeaderText => this[nameof (MissingTemplateMessageHeaderText)];

    /// <summary>Gets the missing template message body text.</summary>
    /// <value>The missing template message body text.</value>
    [ResourceEntry("MissingTemplateMessageBodyText", Description = " Phrase: We can't find the template of this page , so the page will be restored to draft with different layout.<br />Page widgets will not be lost.", LastModified = "2014/07/07", Value = "We can't find the template of this page , so the page will be restored to draft with different layout.<br />Page widgets will not be lost.")]
    public string MissingTemplateMessageBodyText => this[nameof (MissingTemplateMessageBodyText)];

    /// <summary>Gets the missing template message action button text.</summary>
    /// <value>The missing template message action button text.</value>
    [ResourceEntry("MissingTemplateMessageActionButtonText", Description = " Phrase: OK, Restore to Draft", LastModified = "2014/07/07", Value = "OK, Restore to Draft")]
    public string MissingTemplateMessageActionButtonText => this[nameof (MissingTemplateMessageActionButtonText)];

    /// <summary>Gets the missing parent page message header text.</summary>
    /// <value>The missing parent page message header text.</value>
    [ResourceEntry("MissingParentPageMessageHeaderText", Description = " Phrase: Missing parent. Page will be restored on top level", LastModified = "2014/07/07", Value = "Missing parent. Page will be restored on top level")]
    public string MissingParentPageMessageHeaderText => this[nameof (MissingParentPageMessageHeaderText)];

    /// <summary>Gets the missing parent page message body text.</summary>
    /// <value>The missing parent page message body text.</value>
    [ResourceEntry("MissingParentPageMessageBodyText", Description = " Phrase: It seems the parent of this page no more exists, so the page will be restored on top level.", LastModified = "2014/07/07", Value = "It seems the parent of this page no more exists, so the page will be restored on top level.")]
    public string MissingParentPageMessageBodyText => this[nameof (MissingParentPageMessageBodyText)];

    /// <summary>
    /// Gets the missing parent page message action button text.
    /// </summary>
    /// <value>The missing parent page message action button text.</value>
    [ResourceEntry("MissingParentPageMessageActionButtonText", Description = " Phrase: OK, Restore on top level", LastModified = "2014/07/07", Value = "OK, Restore on top level")]
    public string MissingParentPageMessageActionButtonText => this[nameof (MissingParentPageMessageActionButtonText)];

    /// <summary>
    /// Gets the phrase: Are you sure you want to permanently delete all these items?
    /// </summary>
    /// <value>Are you sure you want to permanently delete all these items?</value>
    [ResourceEntry("AreYouSureYouWantToDeleteAllItems", Description = " Phrase: Are you sure you want to permanently delete all these items?", LastModified = "2014/07/09", Value = "Are you sure you want to permanently delete all these items?")]
    public string AreYouSureYouWantToDeleteAllItems => this[nameof (AreYouSureYouWantToDeleteAllItems)];

    /// <summary>Gets the phrase: Yes, Empty Recycle Bin</summary>
    /// <value>Yes, Empty Recycle bin</value>
    [ResourceEntry("EmptyRecycleBinConfirmation", Description = " Phrase: Yes, Empty Recycle Bin", LastModified = "2014/07/09", Value = "Yes, Empty Recycle Bin")]
    public string EmptyRecycleBinConfirmation => this[nameof (EmptyRecycleBinConfirmation)];

    /// <summary>
    /// Gets the phrase: Are you sure you want to delete this item?&lt;br /&gt;This action is not undoable.
    /// </summary>
    /// <value>Are you sure you want to delete this item?&lt;br /&gt;This action is not undoable.</value>
    [ResourceEntry("AreYouSureYouWantToDeleteItem", Description = " Phrase: Are you sure you want to delete this item?&lt;br /&gt;This action is not undoable.", LastModified = "2014/07/09", Value = "Are you sure you want to delete this item?&lt;br /&gt;This action is not undoable.")]
    public string AreYouSureYouWantToDeleteItem => this[nameof (AreYouSureYouWantToDeleteItem)];

    /// <summary>Gets the phrase: Yes, Delete this item</summary>
    /// <value>Yes, Delete this item</value>
    [ResourceEntry("DeleteItemConfirmation", Description = " Phrase: Yes, Delete this item", LastModified = "2014/07/09", Value = "Yes, Delete this item")]
    public string DeleteItemConfirmation => this[nameof (DeleteItemConfirmation)];

    /// <summary>
    /// Gets the phrase: Are you sure you want to permanently delete selected items?
    /// </summary>
    /// <value>Are you sure you want to permanently delete selected items?</value>
    [ResourceEntry("AreYouSureYouWantToPermanentlyDeleteItems", Description = " Phrase: Are you sure you want to permanently delete selected items?", LastModified = "2014/07/09", Value = "Are you sure you want to permanently delete selected items?")]
    public string AreYouSureYouWantToPermanentlyDeleteItems => this[nameof (AreYouSureYouWantToPermanentlyDeleteItems)];

    /// <summary>Gets the phrase: Yes, Delete these items</summary>
    /// <value>Yes, Delete these items</value>
    [ResourceEntry("DeleteItemsConfirmation", Description = " Phrase: Yes, Delete these items", LastModified = "2014/07/09", Value = "Yes, Delete these items")]
    public string DeleteItemsConfirmation => this[nameof (DeleteItemsConfirmation)];

    /// <summary>Gets the phrase: Empty Recycle Bin</summary>
    /// <value>Empty Recycle Bin</value>
    [ResourceEntry("EmptyRecycleBin", Description = " Phrase: Empty Recycle Bin", LastModified = "2014/07/09", Value = "Empty Recycle Bin")]
    public string EmptyRecycleBin => this[nameof (EmptyRecycleBin)];

    /// <summary>Gets the phrase: Date deleted (Last on top)</summary>
    /// <value>Date deleted (Last on top)</value>
    [ResourceEntry("DateDeletedLastOnTop", Description = " Phrase: Date deleted (Last on top)", LastModified = "2014/07/09", Value = "Date deleted (Last on top)")]
    public string DateDeletedLastOnTop => this[nameof (DateDeletedLastOnTop)];

    /// <summary>Gets the phrase: Date deleted (First on top)</summary>
    /// <value>Date deleted (First on top)</value>
    [ResourceEntry("DateDeletedFirstOnTop", Description = " Phrase: Date deleted (First on top)", LastModified = "2014/07/09", Value = "Date deleted (First on top)")]
    public string DateDeletedFirstOnTop => this[nameof (DateDeletedFirstOnTop)];

    /// <summary>Gets the phrase: Item / Last Status</summary>
    /// <value>Item / Last Status</value>
    [ResourceEntry("ItemLastStatus", Description = " Phrase: Item / Last Status", LastModified = "2014/07/09", Value = "Item / Last Status")]
    public string ItemLastStatus => this[nameof (ItemLastStatus)];

    /// <summary>Gets the phrase: Parent (for hierarchical types)</summary>
    /// <value>Parent (for hierarchical types)</value>
    [ResourceEntry("ParentForHierarchicalTypes", Description = " Phrase: Parent (for hierarchical types)", LastModified = "2014/07/09", Value = "Parent (for hierarchical types)")]
    public string ParentForHierarchicalTypes => this[nameof (ParentForHierarchicalTypes)];

    /// <summary>Gets the phrase: Deleted by / On</summary>
    /// <value>Deleted by / On</value>
    [ResourceEntry("DeletedByOn", Description = " Phrase: Deleted by / On", LastModified = "2014/07/09", Value = "Deleted by / On")]
    public string DeletedByOn => this[nameof (DeletedByOn)];

    /// <summary>Gets the phrase: Filter items in Recycle bin</summary>
    /// <value>Filter items in Recycle bin</value>
    [ResourceEntry("FilterItemsInRecycleBin", Description = " Phrase: Filter items in Recycle bin", LastModified = "2014/07/09", Value = "Filter items in Recycle bin")]
    public string FilterItemsInRecycleBin => this[nameof (FilterItemsInRecycleBin)];

    /// <summary>Gets the phrase: All deleted items</summary>
    /// <value>All deleted items</value>
    [ResourceEntry("AllDeletedItems", Description = " Phrase: All deleted items", LastModified = "2014/07/09", Value = "All deleted items")]
    public string AllDeletedItems => this[nameof (AllDeletedItems)];

    /// <summary>Gets the phrase: Recycle Bin Settings</summary>
    /// <value>Recycle Bin Settings</value>
    [ResourceEntry("RecycleBinSettings", Description = " Phrase: Recycle Bin Settings", LastModified = "2014/07/09", Value = "Recycle Bin Settings")]
    public string RecycleBinSettings => this[nameof (RecycleBinSettings)];

    /// <summary>Gets the phrase: Manage Recycle Bin</summary>
    /// <value>Manage Recycle Bin</value>
    [ResourceEntry("ManageRecycleBin", Description = " Phrase: Manage Recycle Bin", LastModified = "2014/07/09", Value = "Manage Recycle Bin")]
    public string ManageRecycleBin => this[nameof (ManageRecycleBin)];

    /// <summary>Gets the word: Parent</summary>
    /// <value>The word: Parent</value>
    [ResourceEntry("Parent", Description = " Word: Parent", LastModified = "2014/07/11", Value = "Parent")]
    public string Parent => this[nameof (Parent)];

    /// <summary>Gets the missing parent page message header text.</summary>
    /// <value>The missing parent page message header text.</value>
    [ResourceEntry("MissingDynamicContentParentMessageHeaderText", Description = " Phrase: Missing parent", LastModified = "2014/07/17", Value = "Missing parent")]
    public string MissingDynamicContentParentMessageHeaderText => this[nameof (MissingDynamicContentParentMessageHeaderText)];

    /// <summary>Gets the missing parent page message body text.</summary>
    /// <value>The missing parent page message body text.</value>
    [ResourceEntry("MissingDynamicContentParentMessageBodyText", Description = " Phrase: The item cannot be restored because its parent is also in the Recycle Bin. To restore this item, restore its parent.", LastModified = "2014/07/17", Value = "The item cannot be restored because its parent is also in the Recycle Bin. To restore this item, restore its parent.")]
    public string MissingDynamicContentParentMessageBodyText => this[nameof (MissingDynamicContentParentMessageBodyText)];

    /// <summary>
    /// Gets the missing dynamic content parent message action button text.
    /// </summary>
    /// <value>The missing dynamic content parent message action button text.</value>
    [ResourceEntry("MissingDynamicContentParentMessageActionButtonText", Description = " Phrase: OK", LastModified = "2014/07/17", Value = "OK")]
    public string MissingDynamicContentParentMessageActionButtonText => this[nameof (MissingDynamicContentParentMessageActionButtonText)];

    /// <summary>Gets the batch restore conflict message body text.</summary>
    /// <value>The batch restore conflict message body text.</value>
    [ResourceEntry("BatchRestoreConflictMessageBodyText", Description = " Phrase: {0} item(s) have been successfully restored.<br />{1} item(s) haven't been restored. <i>Try to restore each of them separately and see the issues.</i>", LastModified = "2014/07/14", Value = "{0} item(s) have been successfully restored.<br />{1} item(s) haven't been restored. <i>Try to restore each of them separately and see the issues.</i>")]
    public string BatchRestoreConflictMessageBodyText => this[nameof (BatchRestoreConflictMessageBodyText)];

    /// <summary>
    /// Gets the batch restore conflict message action button text.
    /// </summary>
    /// <value>The batch restore conflict message action button text.</value>
    [ResourceEntry("BatchRestoreConflictMessageActionButtonText", Description = " Phrase: OK", LastModified = "2014/07/14", Value = "OK")]
    public string BatchRestoreConflictMessageActionButtonText => this[nameof (BatchRestoreConflictMessageActionButtonText)];

    /// <summary>Gets the recycle bin is empty.</summary>
    /// <value>The recycle bin is empty.</value>
    [ResourceEntry("RecycleBinIsEmpty", Description = " Phrase: Recycle Bin is empty", LastModified = "2014/07/17", Value = "Recycle Bin is empty")]
    public string RecycleBinIsEmpty => this[nameof (RecycleBinIsEmpty)];
  }
}
