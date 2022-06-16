// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.ApprovalWorkflowResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Represents string resources for workflow labels.</summary>
  [ObjectInfo("ApprovalWorkflowResources", ResourceClassId = "ApprovalWorkflowResources")]
  public class ApprovalWorkflowResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Workflow.ApprovalWorkflowResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ApprovalWorkflowResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Workflow.ApprovalWorkflowResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ApprovalWorkflowResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>The title of this class.</summary>
    [ResourceEntry("ApprovalWorkflowResourcesTitle", Description = "The title of this class.", LastModified = "2010/09/28", Value = "Approval Workflow")]
    public string ApprovalWorkflowResourcesTitle => this[nameof (ApprovalWorkflowResourcesTitle)];

    /// <summary>The title plural of this class.</summary>
    [ResourceEntry("ApprovalWorkflowResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2010/09/28", Value = "Approval workflows")]
    public string ApprovalWorkflowResourcesTitlePlural => this[nameof (ApprovalWorkflowResourcesTitlePlural)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("ApprovalWorkflowResourcesDescription", Description = "The description of this class.", LastModified = "2010/09/28", Value = "Contains localizable resources for workflow module.")]
    public string ApprovalWorkflowResourcesDescription => this[nameof (ApprovalWorkflowResourcesDescription)];

    /// <summary>phrase: Awaiting review</summary>
    [ResourceEntry("AwaitingReview", Description = "The text of the 'Awaiting review'.", LastModified = "2018/07/11", Value = "Awaiting review")]
    public string AwaitingReview => this[nameof (AwaitingReview)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("AwaitingApproval", Description = "phrase: Awaiting approval", LastModified = "2010/11/14", Value = "Awaiting approval")]
    public string AwaitingApproval => this[nameof (AwaitingApproval)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("AwaitingPublishing", Description = "phrase: Awaiting approval", LastModified = "2010/11/14", Value = "Awaiting publishing")]
    public string AwaitingPublishing => this[nameof (AwaitingPublishing)];

    /// <summary>phrase: Published</summary>
    [ResourceEntry("Published", Description = "word: Published", LastModified = "2010/11/30", Value = "Published[[[content.ExpirationDate], scheduled unpublish on {{content.ExpirationDate:dd/MM/yy 'at' HH:mm}}]]")]
    public string Published => this[nameof (Published)];

    /// <summary>word: Published</summary>
    [ResourceEntry("Unpublished", Description = "word: Unpublished", LastModified = "2010/11/15", Value = "Unpublished")]
    public string Unpublished => this[nameof (Unpublished)];

    /// <summary>word: Draft</summary>
    [ResourceEntry("Draft", Description = "word: Draft", LastModified = "2010/11/14", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    /// <summary>word: ScheduledDraft</summary>
    [ResourceEntry("ScheduledDraft", Description = "word: Draft", LastModified = "2015/03/10", Value = "Sheduled {0}, Draft")]
    public string ScheduledDraft => this[nameof (ScheduledDraft)];

    /// <summary>word: ScheduledPublicationDate</summary>
    [ResourceEntry("ScheduledPublicationDate", Description = "word: Draft", LastModified = "2015/03/10", Value = "publish on {{content.PublicationDate:dd/MM/yy 'at' HH:mm}}")]
    public string ScheduledPublicationDate => this[nameof (ScheduledPublicationDate)];

    /// <summary>word: ScheduledExpirationDate</summary>
    [ResourceEntry("ScheduledExpirationDate", Description = "word: Draft", LastModified = "2015/03/10", Value = "[[[content.ExpirationDate]unpublish on {{content.ExpirationDate:dd/MM/yy 'at' HH:mm}}]]")]
    public string ScheduledExpirationDate => this[nameof (ScheduledExpirationDate)];

    /// <summary>word: Rejected</summary>
    [ResourceEntry("Rejected", Description = "word: Rejected", LastModified = "2010/11/14", Value = "Rejected")]
    public string Rejected => this[nameof (Rejected)];

    /// <summary>phrase: Rejected publishing</summary>
    [ResourceEntry("RejectedForPublishing", Description = "phrase: Rejected publishing", LastModified = "2018/09/11", Value = "Rejected publishing")]
    public string RejectedForPublishing => this[nameof (RejectedForPublishing)];

    /// <summary>phrase: Rejected approval</summary>
    [ResourceEntry("RejectedForApproval", Description = "phrase: Rejected approval", LastModified = "2018/09/11", Value = "Rejected approval")]
    public string RejectedForApproval => this[nameof (RejectedForApproval)];

    /// <summary>word: Rejected</summary>
    [ResourceEntry("RejectedForPublishingMessage", Description = "word: Rejected", LastModified = "2010/11/14", Value = "Rejected for publishing")]
    public string RejectedForPublishingMessage => this[nameof (RejectedForPublishingMessage)];

    /// <summary>word: Rejected</summary>
    [ResourceEntry("RejectedForReview", Description = "word: Rejected", LastModified = "2018/09/11", Value = "Rejected")]
    public string RejectedForReview => this[nameof (RejectedForReview)];

    /// <summary>
    /// phrase: Scheduled publish on {{content.PublicationDate:dd/MM/yy 'at' hh:mm}}[[[content.ExpirationDate], unpublish on {{content.ExpirationDate:dd/MM/yy 'at' HH:mm}}]]
    /// </summary>
    [ResourceEntry("Scheduled", Description = "phrase: Scheduled publish on {{content.PublicationDate:dd/MM/yy 'at' HH:mm}}[[[content.ExpirationDate], unpublish on {{content.ExpirationDate:dd/MM/yy 'at' HH:mm}}]]", LastModified = "2010/11/14", Value = "Scheduled publish on {{content.PublicationDate:dd/MM/yy 'at' HH:mm}}[[[content.ExpirationDate], unpublish on {{content.ExpirationDate:dd/MM/yy 'at' HH:mm}}]]")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>phrase: Successfully saved as awaiting approval</summary>
    [ResourceEntry("AwaitingApprovalMessage", Description = "phrase: Successfully saved as awaiting approval", LastModified = "2010/11/15", Value = "Successfully saved as awaiting approval")]
    public string AwaitingApprovalMessage => this[nameof (AwaitingApprovalMessage)];

    /// <summary>phrase: Successfully saved as awaiting review</summary>
    [ResourceEntry("AwaitingReviewMessage", Description = "phrase: Successfully saved as awaiting review", LastModified = "2018/07/11", Value = "Successfully saved as awaiting review")]
    public string AwaitingReviewMessage => this[nameof (AwaitingReviewMessage)];

    /// <summary>phrase: Successfully saved as awaiting approval</summary>
    [ResourceEntry("AwaitingPublishingMessage", Description = "phrase: Successfully sent for publishing", LastModified = "2010/11/15", Value = "Successfully saved as awaiting publishing")]
    public string AwaitingPublishingMessage => this[nameof (AwaitingPublishingMessage)];

    /// <summary>phrase: Successfully published</summary>
    [ResourceEntry("PublishedMessage", Description = "phrase: Successfully published", LastModified = "2010/11/15", Value = "Successfully published")]
    public string PublishedMessage => this[nameof (PublishedMessage)];

    /// <summary>phrase: Successfully published</summary>
    [ResourceEntry("UnpublishedMessage", Description = "phrase: Successfully unpublished", LastModified = "2010/11/15", Value = "Successfully unpublished")]
    public string UnpublishedMessage => this[nameof (UnpublishedMessage)];

    /// <summary>phrase: Your draft has been saved</summary>
    [ResourceEntry("DraftMessage", Description = "phrase: Your draft has been saved", LastModified = "2010/11/15", Value = "Your draft has been saved")]
    public string DraftMessage => this[nameof (DraftMessage)];

    /// <summary>phrase: Successfully rejected</summary>
    [ResourceEntry("RejectedMessage", Description = "phrase: Successfully rejected", LastModified = "2010/11/15", Value = "Successfully rejected")]
    public string RejectedMessage => this[nameof (RejectedMessage)];

    /// <summary>phrase: Successfully scheduled</summary>
    [ResourceEntry("ScheduledMessage", Description = "phrase: Successfully scheduled", LastModified = "2010/11/15", Value = "Successfully scheduled")]
    public string ScheduledMessage => this[nameof (ScheduledMessage)];

    [ResourceEntry("PublishedAndDraftMessage", Description = "word: Draft", LastModified = "2010/11/14", Value = "Your draft has been saved")]
    public string PublishedAndDraftMessage => this[nameof (PublishedAndDraftMessage)];

    /// <summary>phrase: Request for {0}</summary>
    [ResourceEntry("ItemAwaitingForAction", Description = "phrase: Request for {0}", LastModified = "2019/06/13", Value = "Request for {0}")]
    public string ItemAwaitingForAction => this[nameof (ItemAwaitingForAction)];

    /// <summary>phrase: Request for review</summary>
    [ResourceEntry("ItemAwaitingReview", Description = "phrase: Request for review", LastModified = "2018/07/25", Value = "Request for review")]
    public string ItemAwaitingReview => this[nameof (ItemAwaitingReview)];

    /// <summary>phrase: Request for approval</summary>
    [ResourceEntry("ItemAwaitingApproval", Description = "phrase: Request for approval", LastModified = "2018/07/25", Value = "Request for approval")]
    public string ItemAwaitingApproval => this[nameof (ItemAwaitingApproval)];

    /// <summary>phrase: Request for publishing</summary>
    [ResourceEntry("ItemAwaitingPublishing", Description = "phrase: Request for publishing", LastModified = "2018/07/25", Value = "Request for publishing")]
    public string ItemAwaitingPublishing => this[nameof (ItemAwaitingPublishing)];

    /// <summary>phrase: Rejected {0}</summary>
    [ResourceEntry("ItemRejected", Description = "phrase: Rejected {0}", LastModified = "2018/07/25", Value = "Rejected {0}")]
    public string ItemRejected => this[nameof (ItemRejected)];

    /// <summary>phrase: Rejected {0}</summary>
    [ResourceEntry("ItemRejectedForPublishing", Description = "phrase: Rejected {0}", LastModified = "2018/07/25", Value = "Rejected {0}")]
    public string ItemRejectedForPublishing => this[nameof (ItemRejectedForPublishing)];

    /// <summary>phrase: Rejected {0}</summary>
    [ResourceEntry("ItemRejectedForReview", Description = "phrase: Rejected {0}", LastModified = "2018/07/25", Value = "Rejected {0}")]
    public string ItemRejectedForReview => this[nameof (ItemRejectedForReview)];

    /// <summary>phrase: {1} sent the following {0} for review:</summary>
    [ResourceEntry("ItemAwaitingReviewDetailedMessage", Description = "phrase: {1} sent the following {0} for review:", LastModified = "2018/07/25", Value = "{1} sent the following {0} for review:")]
    public string ItemAwaitingReviewDetailedMessage => this[nameof (ItemAwaitingReviewDetailedMessage)];

    /// <summary>phrase: A user sent the following {0} for review:</summary>
    [ResourceEntry("ItemAwaitingReviewDetailedMessageNoUser", Description = "phrase: A user sent the following {0} for review:", LastModified = "2018/07/25", Value = "A user sent the following {0} for review:")]
    public string ItemAwaitingReviewDetailedMessageNoUser => this[nameof (ItemAwaitingReviewDetailedMessageNoUser)];

    /// <summary>phrase: {1} sent the following {0} for approval:</summary>
    [ResourceEntry("ItemAwaitingApprovalDetailedMessage", Description = "phrase: {1} sent the following {0} for approval:", LastModified = "2018/07/25", Value = "{1} sent the following {0} for approval:")]
    public string ItemAwaitingApprovalDetailedMessage => this[nameof (ItemAwaitingApprovalDetailedMessage)];

    /// <summary>phrase: A user sent the following {0} for approval:</summary>
    [ResourceEntry("ItemAwaitingApprovalDetailedMessageNoUser", Description = "phrase: A user sent the following {0} for approval:", LastModified = "2018/07/25", Value = "A user sent the following {0} for approval:")]
    public string ItemAwaitingApprovalDetailedMessageNoUser => this[nameof (ItemAwaitingApprovalDetailedMessageNoUser)];

    /// <summary>phrase: {1} sent the following {0} for publishing:</summary>
    [ResourceEntry("ItemAwaitingPublishingDetailedMessage", Description = "phrase: {1} sent the following {0} for publishing:", LastModified = "2018/07/25", Value = "{1} sent the following {0} for publishing:")]
    public string ItemAwaitingPublishingDetailedMessage => this[nameof (ItemAwaitingPublishingDetailedMessage)];

    /// <summary>
    /// phrase: This message is to notify you that a new {0} is waiting for publishing
    /// </summary>
    [ResourceEntry("ItemAwaitingPublishingDetailedMessageNoUser", Description = "phrase: A user sent the following {0} for publishing:", LastModified = "2018/07/25", Value = "A user sent the following {0} for publishing:")]
    public string ItemAwaitingPublishingDetailedMessageNoUser => this[nameof (ItemAwaitingPublishingDetailedMessageNoUser)];

    /// <summary>
    /// phrase: Your {0} sent for publishing was rejected by {1}:
    /// </summary>
    [ResourceEntry("ItemRejectedForPublishingDetailedMessage", Description = "phrase: Your {0} sent for publishing was rejected by {1}:", LastModified = "2018/07/25", Value = "Your {0} sent for publishing was rejected by {1}:")]
    public string ItemRejectedForPublishingDetailedMessage => this[nameof (ItemRejectedForPublishingDetailedMessage)];

    /// <summary>Phrase resource</summary>
    [ResourceEntry("ItemRejectedForPublishingDetailedMessageNoUser", Description = "phrase: Your {0} sent for publishing was rejected:", LastModified = "2018/07/25", Value = "Your {0} sent for publishing was rejected:")]
    public string ItemRejectedForPublishingDetailedMessageNoUser => this[nameof (ItemRejectedForPublishingDetailedMessageNoUser)];

    /// <summary>phrase: Your {0} sent for review was rejected by {1}:</summary>
    [ResourceEntry("ItemRejectedForReviewDetailedMessage", Description = "phrase: Your {0} sent for review was rejected by {1}:", LastModified = "2018/07/25", Value = "Your {0} sent for review was rejected by {1}:")]
    public string ItemRejectedForReviewDetailedMessage => this[nameof (ItemRejectedForReviewDetailedMessage)];

    /// <summary>phrase: Your {0} sent for review was rejected by {1}:</summary>
    [ResourceEntry("ItemRejectedForReviewDetailedMessageNoUser", Description = "phrase: Your {0} sent for review was rejected:", LastModified = "2018/07/25", Value = "Your {0} sent for review was rejected:")]
    public string ItemRejectedForReviewDetailedMessageNoUser => this[nameof (ItemRejectedForReviewDetailedMessageNoUser)];

    /// <summary>
    /// phrase: Your {0} sent for approval was rejected by {1}:
    /// </summary>
    [ResourceEntry("ItemRejectedDetailedMessage", Description = "phrase: Your {0} sent for approval was rejected by {1}:", LastModified = "2018/07/25", Value = "Your {0} sent for approval was rejected by {1}:")]
    public string ItemRejectedDetailedMessage => this[nameof (ItemRejectedDetailedMessage)];

    /// <summary>phrase: Your {0} sent for approval was rejected:</summary>
    [ResourceEntry("ItemRejectedDetailedMessageNoUser", Description = "phrase: Your {0} sent for approval was rejected:", LastModified = "2018/07/25", Value = "Your {0} sent for approval was rejected:")]
    public string ItemRejectedDetailedMessageNoUser => this[nameof (ItemRejectedDetailedMessageNoUser)];

    /// <summary>phrase: Sent by: {0} on {1} at {2}</summary>
    [ResourceEntry("SentByOnAt", Description = "phrase: Sent by: {0} on {1} at {2}", LastModified = "2013/05/21", Value = "Sent by: {0} on {1} at {2}")]
    public string SentByOnAt => this[nameof (SentByOnAt)];

    /// <summary>Gets resource string</summary>
    [ResourceEntry("GoToItemLinkText", Description = "word: Go to {0}", LastModified = "2018/07/25", Value = "Go to {0}")]
    public string GoToItemLinkText => this[nameof (GoToItemLinkText)];

    /// <summary>Gets resource string</summary>
    [ResourceEntry("NoteForApproversText", Description = "phrase: Note for approvers:", LastModified = "2018/07/25", Value = "Note for approvers:")]
    public string NoteForApproversText => this[nameof (NoteForApproversText)];

    /// <summary>Gets resource string</summary>
    [ResourceEntry("RejectionReason", Description = "phrase: Reason for rejection:", LastModified = "2018/08/02", Value = "Reason for rejection:")]
    public string RejectionReason => this[nameof (RejectionReason)];
  }
}
