// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ContentLifecycleMessages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Localization messages for content lifcycle</summary>
  [ObjectInfo("ContentLifecycleMessages", ResourceClassId = "ContentLifecycleMessages")]
  public class ContentLifecycleMessages : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.ContentLifecycleMessages" /> class.
    /// </summary>
    public ContentLifecycleMessages()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.ContentLifecycleMessages" /> class.
    /// </summary>
    /// <param name="dataProvider">Localization provider.</param>
    public ContentLifecycleMessages(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Translated message, similar to "Content Lifecycle"</summary>
    /// <value>Title of the content lifecycle localization class.</value>
    [ResourceEntry("ContentLifecycleMessagesTitle", Description = "Title of the content lifecycle localization class.", LastModified = "2010/07/05", Value = "Content Lifecycle")]
    public string ContentLifecycleMessagesTitle => this[nameof (ContentLifecycleMessagesTitle)];

    /// <summary>
    /// Translated message, similar to "Content Lifecycle Messages"
    /// </summary>
    /// <value>Plural form of this localization class' title.</value>
    [ResourceEntry("ContentLifecycleMessagesTitlePlural", Description = "Plural form of this localization class' title.", LastModified = "2010/07/05", Value = "Content Lifecycle Messages")]
    public string ContentLifecycleMessagesTitlePlural => this[nameof (ContentLifecycleMessagesTitlePlural)];

    /// <summary>
    /// Translated message, similar to "Contains localizable resources for Content Lifecycle user interface."
    /// </summary>
    /// <value>Description of the content lifecycle localization class.</value>
    [ResourceEntry("ContentLifecycleMessagesDescription", Description = "Description of the content lifecycle localization class.", LastModified = "2010/07/05", Value = "Contains localizable resources for Content Lifecycle user interface.")]
    public string ContentLifecycleMessagesDescription => this[nameof (ContentLifecycleMessagesDescription)];

    /// <summary>Translated message, similar to "Published."</summary>
    /// <value>Label for the published (live) status of a content item.</value>
    [ResourceEntry("Published", Description = "Label for the published (live) status of a content item.", LastModified = "2010/07/05", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>Translated message, similar to "Scheduled"</summary>
    /// <value>Label for a live (published) item that is not immediately visible.</value>
    [ResourceEntry("Scheduled", Description = "Label for the published (live) status of a content item.", LastModified = "2010/08/02", Value = "Scheduled")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>
    /// Translated message, similar to "Draft Newer Than Published."
    /// </summary>
    /// <value>Status message shown when an item's master version is newer than the published (live) version.</value>
    [ResourceEntry("DraftNewerThanPublished", Description = "Label for the published (live) status of a content item.", LastModified = "2010/08/04", Value = "Draft newer than published")]
    public string DraftNewerThanPublished => this[nameof (DraftNewerThanPublished)];

    /// <summary>
    /// Translated message, similar to "Private copy locked by {0}."
    /// </summary>
    /// <value>Label notifying that a user owns a private copy of an item (that is locked). {0} is placeholder for the user name.</value>
    [ResourceEntry("LockedByUserName", Description = "Label notifying that a user owns a private copy of an item (that is locked). {0} is placeholder for the user name.", LastModified = "2010/07/07", Value = "Private copy locked by {0}")]
    public string LockedByUserName => this[nameof (LockedByUserName)];

    /// <summary>Translated message, similar to "Draft"</summary>
    /// <value>Status of an item that is in draft state.</value>
    [ResourceEntry("Draft", Description = "Status of an item that is in draft state.", LastModified = "2010/07/08", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    /// <summary>Translated message, similar to "Draft"</summary>
    /// <value>Status of an item that is in draft state.</value>
    [ResourceEntry("LockedFieldMessage", Description = "Message shown on read-only common when the item is opened for editing by another user.", LastModified = "2018/07/24", Value = "Field is currently locked for editing by another user")]
    public string LockedFieldMessage => this[nameof (LockedFieldMessage)];

    /// <summary>
    /// Translated message, similar to "The item was unlocked by another user while you were editing and you cannot save/publish it."
    /// </summary>
    [ResourceEntry("ItemUnlockedWhileEditing", Description = "Error message", LastModified = "2012/01/05", Value = "The item was unlocked by another user while you were editing and you cannot save/publish it.")]
    public string ItemUnlockedWhileEditing => this[nameof (ItemUnlockedWhileEditing)];

    /// <summary>
    /// Translated message, similar to "The item was unlocked by {0} while you were editing and you cannot save/publish it"
    /// </summary>
    [ResourceEntry("ItemUnlockedWhileEditingBy", Description = "Error message. {0} is placeholder for user name.", LastModified = "2012/01/05", Value = "The item was unlocked by {0} while you were editing and you cannot save/publish it")]
    public string ItemUnlockedWhileEditingBy => this[nameof (ItemUnlockedWhileEditingBy)];

    /// <summary>
    /// Translated message, similar to "The item was deleted by another user while you were editing and you cannot save/publish it."
    /// </summary>
    [ResourceEntry("ItemDeletedWhileEditing", Description = "Error message", LastModified = "2012/01/05", Value = "The item was deleted by another user while you were editing and you cannot save/publish it.")]
    public string ItemDeletedWhileEditing => this[nameof (ItemDeletedWhileEditing)];

    /// <summary>
    /// Translated message, similar to "Published version &gt; draft version =&gt; data corruption."
    /// </summary>
    /// <value>Error message</value>
    [ResourceEntry("LiveVersionGreaterThanMasterVersion", Description = "Error message", LastModified = "2010/08/25", Value = "Published version > draft version => data corruption.")]
    public string LiveVersionGreaterThanMasterVersion => this[nameof (LiveVersionGreaterThanMasterVersion)];

    /// <summary>
    /// Translated message, similar to "You can not check in since the item is locked by another user and is not admin."
    /// </summary>
    /// <value>Error message shown when user can not check in.</value>
    [ResourceEntry("CannotCheckIn", Description = "Error message shown when user can not check in.", LastModified = "2011/03/11", Value = "You can not check in since the item is locked by another user and is not admin.")]
    public string CannotCheckIn => this[nameof (CannotCheckIn)];

    /// <summary>
    /// Translated message, similar to "The content item has already been checked out by another user and you cannot unlock it."
    /// </summary>
    [ResourceEntry("AlreadyCheckedOut", Description = "Error message shown when user can not check out.", LastModified = "2012/01/05", Value = "The content item has already been checked out by another user and you cannot unlock it.")]
    public string AlreadyCheckedOut => this[nameof (AlreadyCheckedOut)];

    /// <summary>
    /// Translated message, similar to "This item has already been locked by {0} and you cannot continue editing it"
    /// </summary>
    [ResourceEntry("AlreadyLockedBy", Description = "Error message when user wants to edit an item that has already been locked", LastModified = "2012/01/05", Value = "This item has already been locked by {0} and you cannot continue editing it")]
    public string AlreadyLockedBy => this[nameof (AlreadyLockedBy)];

    /// <summary>
    /// Translated message, similar to "Master version was not found. All items should always have one and only one master."
    /// </summary>
    /// <value>Error message shown when a master (draft) cannot be found.</value>
    [ResourceEntry("MasterNotFound", Description = "Error message shown when a master (draft) cannot be found.", LastModified = "2010/07/06", Value = "Master version was not found. All items should always have one and only one master.")]
    public string MasterNotFound => this[nameof (MasterNotFound)];

    /// <summary>
    /// Translated message, similar to "The item is in an invalid state. It should be master (draft)."
    /// </summary>
    /// <value>Error message shown when an item is expected to be master (draft), but it is not.</value>
    [ResourceEntry("MasterExpected", Description = "Error message shown when an item is expected to be master (draft), but it is not.", LastModified = "2010/07/06", Value = "The item is in an invalid state. It should be master (draft).")]
    public string MasterExpected => this[nameof (MasterExpected)];

    /// <summary>
    /// Translated message, similar to "Cannot check in item that is not in temp state."
    /// </summary>
    /// <value>Error message shown when an item that is not temp is checked in.</value>
    [ResourceEntry("TempExpectedForCheckIn", Description = "Error message shown when an item that is not temp is checked in.", LastModified = "2010/07/06", Value = "Cannot check in item that is not in temp state.")]
    public string TempExpectedForCheckIn => this[nameof (TempExpectedForCheckIn)];

    /// <summary>
    /// Translated message, similar to "Cannot unpublish an item that is not published."
    /// </summary>
    /// <value>Error message shown when an non-live item is unpublished.</value>
    [ResourceEntry("LiveExpectedForUnpublish", Description = "Error message shown when an non-live item is unpublished.", LastModified = "2010/08/03", Value = "Cannot unpublish an item that is not published.")]
    public string LiveExpectedForUnpublish => this[nameof (LiveExpectedForUnpublish)];

    /// <summary>
    /// Translated message, similar to "Cannot check out item that is not in master state."
    /// </summary>
    /// <value>Error message shown when an item that is not master is checked in.</value>
    [ResourceEntry("MasterExpectedForCheckOut", Description = "Error message shown when an item that is not master is checked in.", LastModified = "2010/07/06", Value = "Cannot check out item that is not in master state.")]
    public string MasterExpectedForCheckOut => this[nameof (MasterExpectedForCheckOut)];

    /// <summary>
    /// Translated message, similar to "Cannot edit an item that is not in live state."
    /// </summary>
    /// <value>Error message shown when an item that is not live is edited.</value>
    [ResourceEntry("LiveExpectedForEdit", Description = "Error message shown when an item that is not live is edited.", LastModified = "2010/07/06", Value = "Cannot edit an item that is not in live state.")]
    public string LiveExpectedForEdit => this[nameof (LiveExpectedForEdit)];

    /// <summary>
    /// Translated message, similar to "Cannot publish an item that is not a master."
    /// </summary>
    /// <value>Error message shown when an item that is not master is published.</value>
    [ResourceEntry("MasterExpectedForPublish", Description = "Error message shown when an item that is not master is published.", LastModified = "2010/07/06", Value = "Cannot publish an item that is not a master.")]
    public string MasterExpectedForPublish => this[nameof (MasterExpectedForPublish)];

    /// <summary>
    /// Translated message, similar to "Cannot schedule an item that is not a master."
    /// </summary>
    /// <value>Error message shown when an item that is not master is scheduled.</value>
    [ResourceEntry("MasterExpectedForSchedule", Description = "Error message shown when an item that is not master is scheduled.", LastModified = "2010/07/21", Value = "Cannot schedule an item that is not a master.")]
    public string MasterExpectedForSchedule => this[nameof (MasterExpectedForSchedule)];

    /// <summary>
    /// Translated message, similar to "You are trying to schedule an item with expiration date before the publication date."
    /// </summary>
    /// <value>Error message shown when an item is scheduled so that it will expire before it gets to the public side.</value>
    [ResourceEntry("ScheduleExpiresBeforePublish", Description = "Error message shown when an item is scheduled so that it will expire before it gets to the public side.", LastModified = "2010/07/21", Value = "You are trying to schedule an item with expiration date before the publication date.")]
    public string ScheduleExpiresBeforePublish => this[nameof (ScheduleExpiresBeforePublish)];

    /// <summary>
    /// Error message shown when an item is opened in backend and frontend at the same time. And it is updated in the backend.
    /// </summary>
    /// <value>An item on the page was updated more than once and cannot be published. To proceed, refresh the page (all unsaved changes will be lost).</value>
    [ResourceEntry("ItemUpdateWhileEditing", Description = "Error message shown when an item is opened in backend and frontend at the same time. And it is updated in the backend.", LastModified = "2013/10/03", Value = "An item on the page was updated more than once and cannot be published. To proceed, refresh the page (all unsaved changes will be lost).")]
    public string ItemUpdateWhileEditing => this[nameof (ItemUpdateWhileEditing)];
  }
}
