// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Represents string resources for workflow labels.</summary>
  [ObjectInfo("WorkflowResources", ResourceClassId = "WorkflowResources")]
  public class WorkflowResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Workflow.WorkflowResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public WorkflowResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Workflow.WorkflowResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public WorkflowResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>The title of this class.</summary>
    [ResourceEntry("WorkflowResourcesTitle", Description = "The title of this class.", LastModified = "2010/09/28", Value = "Workflow")]
    public string WorkflowResourcesTitle => this[nameof (WorkflowResourcesTitle)];

    /// <summary>The title plural of this class.</summary>
    [ResourceEntry("WorkflowResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2010/09/28", Value = "Workflows")]
    public string WorkflowResourcesTitlePlural => this[nameof (WorkflowResourcesTitlePlural)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("WorkflowResourcesDescription", Description = "The description of this class.", LastModified = "2010/09/28", Value = "Contains localizable resources for workflow module.")]
    public string WorkflowResourcesDescription => this[nameof (WorkflowResourcesDescription)];

    /// <summary>phrase: Define a workflow</summary>
    [ResourceEntry("CreateAWorkflow", Description = "phrase: Define a workflow", LastModified = "2010/09/28", Value = "Define a workflow")]
    public string CreateAWorkflow => this[nameof (CreateAWorkflow)];

    /// <summary>phrase: Back to workflow</summary>
    [ResourceEntry("BackToWorkflow", Description = "phrase: Back to workflow", LastModified = "2010/09/28", Value = "Back to workflow")]
    public string BackToWorkflow => this[nameof (BackToWorkflow)];

    /// <summary>Label for the workflow title.</summary>
    [ResourceEntry("WorkflowName", Description = "Label for the workflow title.", LastModified = "2010/09/28", Value = "Name")]
    public string WorkflowName => this[nameof (WorkflowName)];

    /// <summary>Validation message when the workflow title is empty.</summary>
    [ResourceEntry("WorkflowTitleEmpty", Description = "Validation message when the workflow title is empty.", LastModified = "2010/09/28", Value = "Name cannot be empty")]
    public string WorkflowTitleEmpty => this[nameof (WorkflowTitleEmpty)];

    /// <summary>Label of the button that saves workflow.</summary>
    [ResourceEntry("SaveWorkflow", Description = "Label of the button that saves workflow", LastModified = "2010/09/28", Value = "Save workflow")]
    public string SaveWorkflow => this[nameof (SaveWorkflow)];

    /// <summary>phrase: Workflow type</summary>
    [ResourceEntry("WorkflowType", Description = "phrase: Workflow type", LastModified = "2010/09/28", Value = "Workflow type")]
    public string WorkflowType => this[nameof (WorkflowType)];

    /// <summary>or with the elipsis</summary>
    [ResourceEntry("OrElipsis", Description = "or with the elipsis", LastModified = "2010/09/28", Value = "Or...")]
    public string OrElipsis => this[nameof (OrElipsis)];

    /// <summary>Title of the standard one step workflow</summary>
    [ResourceEntry("ApprovalBeforePublishing", Description = "Title of the standard one step workflow", LastModified = "2010/09/28", Value = "<strong>Approval before publishing</strong><span class='sfNote'>Steps: <em>Edit &gt; For Approval &gt; Published</em></span>")]
    public string ApprovalBeforePublishing => this[nameof (ApprovalBeforePublishing)];

    /// <summary>Title of the standard two step workflow</summary>
    [ResourceEntry("TwoLevelsOfApprovalBeforePublishing", Description = "Title of the standard two step workflow", LastModified = "2010/09/28", Value = "<strong>2 levels of approval before publishing</strong><span class='sfNote'>Steps: <em>Edit &gt; For Approval &gt; For Publishing &gt; Published</em></span>")]
    public string TwoLevelsOfApprovalBeforePublishing => this[nameof (TwoLevelsOfApprovalBeforePublishing)];

    /// <summary>Title of the standard two step workflow</summary>
    [ResourceEntry("ThreeLevelsOfApprovalBeforePublishing", Description = "Title of the standard three step workflow", LastModified = "2018/7/10", Value = "<strong>3 levels of approval before publishing</strong><span class='sfNote'>Steps: <em>Edit &gt; For Review &gt; For Approval &gt; For Publishing &gt; Published</em></span>")]
    public string ThreeLevelsOfApprovalBeforePublishing => this[nameof (ThreeLevelsOfApprovalBeforePublishing)];

    /// <summary>phrase: Custom workflow</summary>
    [ResourceEntry("CustomWorkflow", Description = "phrase: Custom workflow", LastModified = "2010/10/27", Value = "<strong>Custom workflow</strong><span class='sfNote'>Create your own workflow from scratch</span>")]
    public string CustomWorkflow => this[nameof (CustomWorkflow)];

    /// <summary>phrase: Allow administrators to skip the workflow</summary>
    [ResourceEntry("AllowAdministratorsToSkipWorkflow", Description = "phrase: Allow administrators to skip the workflow", LastModified = "2010/09/28", Value = "Allow administrators to skip the workflow")]
    public string AllowAdministratorsToSkipWorkflow => this[nameof (AllowAdministratorsToSkipWorkflow)];

    /// <summary>
    /// Description explaining what it means that administrators can skip the workflow
    /// </summary>
    [ResourceEntry("AllowAdministratorsToSkipWorkflowDescription", Description = "Description explaining what it means that administrators can skip the workflow", LastModified = "2010/09/28", Value = "short description goes here")]
    public string AllowAdministratorsToSkipWorkflowDescription => this[nameof (AllowAdministratorsToSkipWorkflowDescription)];

    /// <summary>
    /// Label for the checkbox indicating that workflow ought to be activated immediately.
    /// </summary>
    [ResourceEntry("ActivateWorkflowImmediately", Description = "Label for the checkbox indicating that workflow ought to be activated immediately", LastModified = "2010/09/28", Value = "This workflow is active")]
    public string ActivateWorkflowImmediately => this[nameof (ActivateWorkflowImmediately)];

    /// <summary>phrase: Approvers for Level {0}</summary>
    [ResourceEntry("SetApproversForLevel", Description = "phrase: Approvers for Level {0}", LastModified = "2018/08/03", Value = "Approvers for Level {0}")]
    public string SetApproversForLevel => this[nameof (SetApproversForLevel)];

    /// <summary>phrase: Set approvers for Level {0}</summary>
    [ResourceEntry("SetApproversForLevelN", Description = "phrase: Set approvers for Level {0}", LastModified = "2018/08/20", Value = "Set approvers for Level {0}")]
    public string SetApproversForLevelN => this[nameof (SetApproversForLevelN)];

    /// <summary>phrase: Set approvers for Level 1</summary>
    [ResourceEntry("SetApproversForLevel1", Description = "phrase: Set approvers for Level 1", LastModified = "2010/09/28", Value = "Set approvers for Level 1")]
    public string SetApproversForLevel1 => this[nameof (SetApproversForLevel1)];

    /// <summary>phrase: Set approvers</summary>
    [ResourceEntry("SetApprovers", Description = "phrase: Set approvers", LastModified = "2010/09/28", Value = "Set approvers")]
    public string SetApprovers => this[nameof (SetApprovers)];

    /// <summary>phrase: Set approvers for Level 2</summary>
    [ResourceEntry("SetApproversForLevel2", Description = "phrase: Set approvers for Level 2", LastModified = "2010/09/28", Value = "Set approvers for Level 2")]
    public string SetApproversForLevel2 => this[nameof (SetApproversForLevel2)];

    /// <summary>phrase: Set approvers for Level 2</summary>
    [ResourceEntry("SetApproversForLevel3", Description = "phrase: Set approvers for Level 3", LastModified = "2018/07/11", Value = "Set approvers for Level 3")]
    public string SetApproversForLevel3 => this[nameof (SetApproversForLevel3)];

    /// <summary>
    /// phrase: Select users or groups who will be able to approve items
    /// </summary>
    [ResourceEntry("SetApproversForLevel0Description", Description = "phrase: Select users or groups who will be able to approve items", LastModified = "2018/07/10", Value = "Select users or roles who are able to review items.<br /><strong>Note:</strong> Administrators are able to review by default")]
    public string SetApproversForLevel0Description => this[nameof (SetApproversForLevel0Description)];

    /// <summary>
    /// phrase: Select users or groups who will be able to approve items
    /// </summary>
    [ResourceEntry("SetApproversForLevel1Description", Description = "phrase: Select users or groups who will be able to approve items", LastModified = "2010/09/28", Value = "Select users or roles who are able to approve items.<br /><strong>Note:</strong> Administrators are able to approve by default")]
    public string SetApproversForLevel1Description => this[nameof (SetApproversForLevel1Description)];

    /// <summary>
    /// phrase: Select users or groups who will be able to approve items
    /// </summary>
    [ResourceEntry("SetApproversForLevel2Description", Description = "phrase: Select users or groups who will be able to approve items", LastModified = "2010/09/28", Value = "Select users or roles who are able to approve items.<br /><strong>Note:</strong> Administrators are able to approve by default")]
    public string SetApproversForLevel2Description => this[nameof (SetApproversForLevel2Description)];

    /// <summary>
    /// phrase: Select users or groups who will be able to approve items
    /// </summary>
    [ResourceEntry("SetApproversForLevel3Description", Description = "phrase: Select users or groups who will be able to approve items", LastModified = "2018/07/11", Value = "Select users or roles who are able to approve items.<br /><strong>Note:</strong> Administrators are able to approve by default")]
    public string SetApproversForLevel3Description => this[nameof (SetApproversForLevel3Description)];

    /// <summary>
    /// phrase: Notify users by email when an item is waiting for their approval.
    /// </summary>
    [ResourceEntry("NotifyUsersForApproval", Description = "phrase: Notify users by email when an item is waiting for their approval", LastModified = "2010/09/28", Value = "Notify users by email when an item is waiting for their approval")]
    public string NotifyUsersForApproval => this[nameof (NotifyUsersForApproval)];

    /// <summary>phrase: Add roles or users</summary>
    [ResourceEntry("AddRolesOrUserS", Description = "phrase: Add roles or users", LastModified = "2010/09/28", Value = "Add roles or users")]
    public string AddRolesOrUsers => this[nameof (AddRolesOrUsers)];

    /// <summary>word: Scope</summary>
    [ResourceEntry("Scope", Description = "word: Scope", LastModified = "2010/09/28", Value = "Scope")]
    public string Scope => this[nameof (Scope)];

    /// <summary>phrase: Where this workflow will apply</summary>
    [ResourceEntry("ScopeDescription", Description = "phrase: Where this workflow will apply", LastModified = "2018/08/06", Value = "Where this workflow will apply")]
    public string ScopeDescription => this[nameof (ScopeDescription)];

    /// <summary>phrase: Language version</summary>
    [ResourceEntry("LanguageVersion", Description = "phrase: Language version", LastModified = "2010/09/29", Value = "Language version")]
    public string LanguageVersion => this[nameof (LanguageVersion)];

    /// <summary>word: Content</summary>
    [ResourceEntry("Content", Description = "word: Content", LastModified = "2010/09/29", Value = "Content")]
    public string Content => this[nameof (Content)];

    /// <summary>phrase: Add content</summary>
    [ResourceEntry("AddContent", Description = "phrase: Add content", LastModified = "2010/09/29", Value = "Add content")]
    public string AddContent => this[nameof (AddContent)];

    /// <summary>phrase: All languages</summary>
    [ResourceEntry("AllLanguages", Description = "phrase: All languages", LastModified = "2010/09/29", Value = "All languages")]
    public string AllLanguages => this[nameof (AllLanguages)];

    /// <summary>phrase: Design your own workflow</summary>
    [ResourceEntry("DesignYourOwnWorkflow", Description = "phrase: Design your own workflow", LastModified = "2010/09/29", Value = "Design your own workflow")]
    public string DesignYourOwnWorkflow => this[nameof (DesignYourOwnWorkflow)];

    /// <summary>Title of the created on / owner column</summary>
    [ResourceEntry("CreatedOnOwner", Description = "Title of the created on / owner column", LastModified = "2010/09/29", Value = "Created on / owner")]
    public string CreatedOnOwner => this[nameof (CreatedOnOwner)];

    /// <summary>phrase: All workflows</summary>
    [ResourceEntry("AllWorkflows", Description = "phrase: All workflows", LastModified = "2010/09/29", Value = "All workflows")]
    public string AllWorkflows => this[nameof (AllWorkflows)];

    /// <summary>phrase: My workflows</summary>
    [ResourceEntry("MyWorkflows", Description = "phrase: My workflows", LastModified = "2010/09/29", Value = "My workflows")]
    public string MyWorkflows => this[nameof (MyWorkflows)];

    /// <summary>phrase: Active workflows</summary>
    [ResourceEntry("ActiveWorkflows", Description = "phrase: Active workflows", LastModified = "2010/09/29", Value = "Active workflows")]
    public string ActiveWorkflows => this[nameof (ActiveWorkflows)];

    /// <summary>phrase: Inactive workflows</summary>
    [ResourceEntry("InactiveWorkflows", Description = "phrase: Inactive workflows", LastModified = "2010/09/29", Value = "Inactive workflows")]
    public string InactiveWorkflows => this[nameof (InactiveWorkflows)];

    /// <summary>phrase: Workflow by content type</summary>
    [ResourceEntry("WorkflowByContentType", Description = "phrase: Workflow by content type", LastModified = "2010/09/30", Value = "Workflow by content type")]
    public string WorkflowByContentType => this[nameof (WorkflowByContentType)];

    /// <summary>word: Filter</summary>
    [ResourceEntry("Filter", Description = "word: Filter", LastModified = "2010/09/30", Value = "Filter")]
    public string Filter => this[nameof (Filter)];

    /// <summary>phrase: Settings for workflow</summary>
    [ResourceEntry("SettingsForWorkflow", Description = "phrase: Settings for workflow", LastModified = "2010/10/26", Value = "Settings for workflow")]
    public string SettingsForWorkflow => this[nameof (SettingsForWorkflow)];

    /// <summary>word: Permissions</summary>
    [ResourceEntry("Permissions", Description = "word: Permissions", LastModified = "2010/10/26", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>word: Workflow</summary>
    [ResourceEntry("Workflow", Description = "word: Workflow", LastModified = "2010/09/30", Value = "Workflow")]
    public string Workflow => this[nameof (Workflow)];

    /// <summary>Phrase: Workflow Scope</summary>
    [ResourceEntry("WorkflowScope", Description = "Phrase: Workflow Scope", LastModified = "2010/10/06", Value = "Workflow Scope")]
    public string WorkflowScope => this[nameof (WorkflowScope)];

    /// <summary>Phrase: Which {0} to include?</summary>
    [ResourceEntry("WhichContentToInclude", Description = "Phrase: Which {0} to include?", LastModified = "2010/10/07", Value = "Which {0} to include?")]
    public string WhichContentToInclude => this[nameof (WhichContentToInclude)];

    /// <summary>Phrase: Selection of {0}:</summary>
    [ResourceEntry("SelectionOfContent", Description = "Phrase: Selection of {0}:", LastModified = "2010/10/07", Value = "Selection of {0}:")]
    public string SelectionOfContent => this[nameof (SelectionOfContent)];

    /// <summary>Phrase: All {0}</summary>
    [ResourceEntry("AllContent", Description = "Phrase: All {0}", LastModified = "2010/10/07", Value = "All {0}")]
    public string AllContent => this[nameof (AllContent)];

    /// <summary>Phrase: All content</summary>
    [ResourceEntry("ALL_CONTENT", Description = "Phrase: All content", LastModified = "2010/11/12", Value = "All content and pages")]
    public string ALL_CONTENT => this[nameof (ALL_CONTENT)];

    /// <summary>Phrase: Advanced selection</summary>
    [ResourceEntry("AdvancedSelection", Description = "Phrase: Advanced selection", LastModified = "2010/10/08", Value = "Advanced selection")]
    public string AdvancedSelection => this[nameof (AdvancedSelection)];

    /// <summary>Phrase: From selected {0}</summary>
    [ResourceEntry("FromSelectedParent", Description = "Phrase: From selected {0}", LastModified = "2010/10/11", Value = "From selected {0}")]
    public string FromSelectedParent => this[nameof (FromSelectedParent)];

    /// <summary>phrase: Workflow scope title</summary>
    [ResourceEntry("WorkflowScopeTitle", Description = "phrase: Workflow scope title", LastModified = "2010/10/21", Value = "Workflow scope title")]
    public string WorkflowScopeTitle => this[nameof (WorkflowScopeTitle)];

    /// <summary>phrase: No workflows have been created yet</summary>
    [ResourceEntry("NoWorkflowsCreatedYet", Description = "phrase: No workflows have been created yet", LastModified = "2010/10/26", Value = "No workflows have been created yet")]
    public string NoWorkflowsCreatedYet => this[nameof (NoWorkflowsCreatedYet)];

    /// <summary>phrase: Define a workflow</summary>
    [ResourceEntry("DefineWorkflow", Description = "phrase: Define a workflow", LastModified = "2010/10/26", Value = "Define a workflow")]
    public string DefineWorkflow => this[nameof (DefineWorkflow)];

    /// <summary>word: Continue</summary>
    [ResourceEntry("Continue", Description = "phrase: Continue", LastModified = "2010/10/27", Value = "Continue")]
    public string Continue => this[nameof (Continue)];

    /// <summary>phrase: Back to workflow types</summary>
    [ResourceEntry("BackToWorkflowTypes", Description = "phrase: Back to workflow types", LastModified = "2010/10/27", Value = "Back to workflow types")]
    public string BackToWorkflowTypes => this[nameof (BackToWorkflowTypes)];

    /// <summary>phrase: Define a workflow: Properties</summary>
    [ResourceEntry("DefineWorkflowProperties", Description = "phrase: Define a workflow: Properties", LastModified = "2010/10/27", Value = "Define a workflow: Properties")]
    public string DefineWorkflowProperties => this[nameof (DefineWorkflowProperties)];

    /// <summary>phrase: Edit a workflow</summary>
    [ResourceEntry("EditWorkflow", Description = "phrase: Edit a workflow", LastModified = "2010/10/27", Value = "Edit a workflow")]
    public string EditWorkflow => this[nameof (EditWorkflow)];

    /// <summary>phrase: Define a workflow: Select a type</summary>
    [ResourceEntry("DefineWorkflowSelectType", Description = "phrase: Define a workflow: Select a type", LastModified = "2010/10/27", Value = "Define a workflow: Select a type")]
    public string DefineWorkflowSelectType => this[nameof (DefineWorkflowSelectType)];

    /// <summary>phrase: Define a workflow: Select a type</summary>
    [ResourceEntry("WorkflowSelectAtLeastOneType", Description = "phrase:Select at least one content type", LastModified = "2018/07/18", Value = "Select at least one content type")]
    public string WorkflowSelectAtLeastOneType => this[nameof (WorkflowSelectAtLeastOneType)];

    /// <summary>phrase: The defined scope is already added</summary>
    [ResourceEntry("ScopeIsAdded", Description = "phrase: This scope definition is already in use for this workflow.", LastModified = "2018/08/28", Value = "This scope definition is already in use for this workflow.")]
    public string ScopeIsAdded => this[nameof (ScopeIsAdded)];

    /// <summary>word: Remove</summary>
    [ResourceEntry("Remove", Description = "word: Remove", LastModified = "2018/07/18", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    /// <summary>word: Remove</summary>
    [ResourceEntry("ChangeScope", Description = "phrase: Change scope", LastModified = "2018/07/18", Value = "Change scope")]
    public string ChangeScope => this[nameof (ChangeScope)];

    /// <summary>phrase: select all</summary>
    [ResourceEntry("SelectAll", Description = "Workflow resource strings.", LastModified = "2018/07/20", Value = "Select All")]
    public string SelectAll => this[nameof (SelectAll)];

    /// <summary>phrase: Define a workflow: Upload file</summary>
    [ResourceEntry("DefineWorkflowUploadFile", Description = "phrase: Define a workflow: Upload file", LastModified = "2010/10/30", Value = "Define a workflow: Upload file")]
    public string DefineWorkflowUploadFile => this[nameof (DefineWorkflowUploadFile)];

    /// <summary>phrase: Upload a workflow file</summary>
    [ResourceEntry("UploadWorkflowFile", Description = "phrase: Upload a workflow file", LastModified = "2010/10/30", Value = "Upload a workflow file")]
    public string UploadWorkflowFile => this[nameof (UploadWorkflowFile)];

    /// <summary>phrase: Invalid workflow file</summary>
    [ResourceEntry("InvalidWorkflowFile", Description = "phrase: Invalid workflow file", LastModified = "2010/10/30", Value = "Invalid workflow file")]
    public string InvalidWorkflowFile => this[nameof (InvalidWorkflowFile)];

    /// <summary>phrase: Select a workflow file to upload</summary>
    [ResourceEntry("SelectWorkflowFileToUpload", Description = "phrase: Select a workflow file to upload", LastModified = "2010/10/30", Value = "Select a workflow file to upload")]
    public string SelectWorkflowFileToUpload => this[nameof (SelectWorkflowFileToUpload)];

    /// <summary>phrase: Permissions for workflow</summary>
    [ResourceEntry("PermissionsForWorkflow", Description = "phrase: Permissions for workflow", LastModified = "2010/11/01", Value = "Permissions for workflow")]
    public string PermissionsForWorkflow => this[nameof (PermissionsForWorkflow)];

    /// <summary>Phrase: Reject publishing</summary>
    [ResourceEntry("RejectPublishing", Description = "Phrase: Reject publishing", LastModified = "2010/10/06", Value = "Reject publishing")]
    public string RejectPublishing => this[nameof (RejectPublishing)];

    /// <summary>
    /// Phrase: Rejecting this page will return it to its author.
    /// </summary>
    [ResourceEntry("RejectingThisItem", Description = "Phrase: Rejecting this page will return it to its author.", LastModified = "2010/10/06", Value = "Rejecting this page will return it to its author.")]
    public string RejectingThisItem => this[nameof (RejectingThisItem)];

    /// <summary>Phrase:  Reason to reject (optional)</summary>
    [ResourceEntry("ReasonToReject", Description = "Phrase:  Reason to reject (optional)", LastModified = "2010/10/06", Value = "Reason to reject <span class='sfNote'>(optional)</span>")]
    public string ReasonToReject => this[nameof (ReasonToReject)];

    /// <summary>
    /// Phrase:  The reason will be sent to the author by email
    /// </summary>
    [ResourceEntry("TheReasonWillBeSentToTheAuthor", Description = "Phrase:  The reason will be sent to the author by email", LastModified = "2010/10/06", Value = "<p class='sfNote'>The reason will be sent to the author by email</p>")]
    public string TheReasonWillBeSentToTheAuthor => this[nameof (TheReasonWillBeSentToTheAuthor)];

    /// <summary>Phrase: Publish on</summary>
    [ResourceEntry("PublishOn", Description = "Phrase: Publication date", LastModified = "2011/09/01", Value = "Publication date <span class='sfNote'>(displayed on the public site)</span>")]
    public string PublishOn => this[nameof (PublishOn)];

    /// <summary>Label: Publication date</summary>
    [ResourceEntry("PublicationDate", Description = "Label: Publication date", LastModified = "2011/09/07", Value = "Publication date")]
    public string PublicationDate => this[nameof (PublicationDate)];

    /// <summary>Label: Last modified</summary>
    [ResourceEntry("LastModified", Description = "Label: Last modified", LastModified = "2011/09/07", Value = "Last modified")]
    public string LastModified => this[nameof (LastModified)];

    /// <summary>Phrase: Unpublish on</summary>
    [ResourceEntry("UnpublishOn", Description = "Phrase: Unpublish on", LastModified = "2009/11/23", Value = "Unpublish on")]
    public string UnpublishOn => this[nameof (UnpublishOn)];

    /// <summary>Phrase: Schedule publish / unpublish</summary>
    [ResourceEntry("SchedulePublishUnpublish", Description = "Phrase: Publish/Unpublish on Specific date", LastModified = "2011/09/14", Value = "Publish/Unpublish on Specific date")]
    public string SchedulePublishUnpublish => this[nameof (SchedulePublishUnpublish)];

    /// <summary>Phrase: Schedule</summary>
    [ResourceEntry("Schedule", Description = "Phrase: Schedule", LastModified = "2009/11/23", Value = "Schedule")]
    public string Schedule => this[nameof (Schedule)];

    /// <summary>
    /// Phrase: Scheduling was not made because the publication time is later then the time of unpublishing. Please, correct the dates.
    /// </summary>
    [ResourceEntry("UnpublishDateShouldBeAfterPublicationDate", Description = "Phrase: Scheduling was not made because the publication time is later then the time of unpublishing. Please, correct the dates.", LastModified = "2012/01/05", Value = "Scheduling was not made because the publication time is later then the time of unpublishing. Please, correct the dates.")]
    public string UnpublishDateShouldBeAfterPublicationDate => this[nameof (UnpublishDateShouldBeAfterPublicationDate)];

    /// <summary>
    /// Phrase: Scheduling was not made because no dates were selected. Please, specify a date.
    /// </summary>
    [ResourceEntry("SchedulingWasNotMade", Description = "Phrase: SchedulingWasNotMade", LastModified = "2010/10/06", Value = "Scheduling was not made because no dates were selected. Please, specify a date.")]
    public string SchedulingWasNotMade => this[nameof (SchedulingWasNotMade)];

    /// <summary>Phrase: Date to Unpublish (optional).</summary>
    [ResourceEntry("DateToUnpublish", Description = "Phrase: Date to Unpublish (optional)", LastModified = "2010/10/06", Value = "Date to Unpublish <span class='sfNote'>(optional)</span>")]
    public string DateToUnpublish => this[nameof (DateToUnpublish)];

    /// <summary>
    /// Phrase: Your {0} file has been uploaded to App_Data/Workflows
    /// </summary>
    [ResourceEntry("FileHasBeenUploaded", Description = "Phrase: Your {0} file has been uploaded to App_Data/Workflows", LastModified = "2010/11/12", Value = "Your {0} file has been uploaded to App_Data/Workflows")]
    public string FileHasBeenUploaded => this[nameof (FileHasBeenUploaded)];

    /// <summary>Title of the standard one step workflow</summary>
    [ResourceEntry("OneLevelOfApproval", Description = "Title of the standard one step workflow", LastModified = "2018/08/02", Value = "<strong>One level</strong> <div class='sfNote sfSmallerTxt'>Create &gt; Send for Approval &gt; Publish</div>")]
    public string OneLevelOfApproval => this[nameof (OneLevelOfApproval)];

    /// <summary>Description of the standard one step workflow</summary>
    [ResourceEntry("OneLevelOfApprovalDescription", Description = "Title of the standard one step workflow", LastModified = "2018/08/02", Value = "1 level of approval")]
    public string OneLevelOfApprovalDescription => this[nameof (OneLevelOfApprovalDescription)];

    /// <summary>Title of the standard two step workflow</summary>
    [ResourceEntry("TwoLevelsOfApproval", Description = "Title of the standard two step workflow", LastModified = "2018/08/03", Value = "<strong>Two levels</strong> <div class='sfNote sfSmallerTxt'>Create &gt; Send for Approval &gt; Send for Publishing &gt; Publish</div>")]
    public string TwoLevelsOfApproval => this[nameof (TwoLevelsOfApproval)];

    /// <summary>Description of the standard two step workflow</summary>
    [ResourceEntry("TwoLevelsOfApprovalDescription", Description = "Description of the standard two step workflow", LastModified = "2018/08/10", Value = "2 levels of approval")]
    public string TwoLevelsOfApprovalDescription => this[nameof (TwoLevelsOfApprovalDescription)];

    /// <summary>Title of the standard three step workflow</summary>
    [ResourceEntry("ThreeLevelsOfApproval", Description = "Title of the standard three step workflow", LastModified = "2018/08/30", Value = "<strong>Three levels</strong> <div class='sfNote sfSmallerTxt'>Create &gt; Send for Review &gt; Send for Approval &gt; <br /> Send for Publishing &gt; Publish</div>")]
    public string ThreeLevelsOfApproval => this[nameof (ThreeLevelsOfApproval)];

    /// <summary>Description of the standard three step workflow</summary>
    [ResourceEntry("ThreeLevelsOfApprovalDescription", Description = "Title of the standard three step workflow", LastModified = "2018/08/10", Value = "3 levels of approval")]
    public string ThreeLevelsOfApprovalDescription => this[nameof (ThreeLevelsOfApprovalDescription)];

    /// <summary>Description of custom workflow</summary>
    [ResourceEntry("CustomWorkflowDescription", Description = "Description of custom workflow", LastModified = "2018/08/10", Value = "Custom")]
    public string CustomWorkflowDescription => this[nameof (CustomWorkflowDescription)];

    /// <summary>Phrase: Levels of approval</summary>
    [ResourceEntry("LevelsOfApproval", Description = "Phrase: Levels of approval", LastModified = "2018/07/27", Value = "Levels of approval")]
    public string LevelsOfApproval => this[nameof (LevelsOfApproval)];

    /// <summary>
    /// Phrase: Who needs to be notified and approve content before publishing
    /// </summary>
    [ResourceEntry("LevelsOfApprovalDescription", Description = "Phrase: Who needs to be notified and approve content before publishing", LastModified = "2018/07/27", Value = "Who needs to be notified and approve content before publishing")]
    public string LevelsOfApprovalDescription => this[nameof (LevelsOfApprovalDescription)];

    /// <summary>Phrase: Why rejected?</summary>
    [ResourceEntry("WhyRejected", Description = "Phrase: Why rejected?", LastModified = "2010/11/11", Value = "Why rejected?")]
    public string WhyRejected => this[nameof (WhyRejected)];

    /// <summary>Phrase: Selected only</summary>
    [ResourceEntry("SelectedOnly", Description = "Phrase: Selected only", LastModified = "2010/11/11", Value = "Selected only")]
    public string SelectedOnly => this[nameof (SelectedOnly)];

    /// <summary>
    /// Phrase: Make sure the approvers have permissions to modify the content, subject to approval. Administrators are approvers by default.
    /// </summary>
    [ResourceEntry("SetApproversWarningMsg", Description = "Phrase: Make sure the approvers have permissions to modify the content, subject to approval. Administrators are approvers by default.", LastModified = "2010/11/17", Value = "Make sure the approvers have permissions to modify the content, subject to approval. Administrators are approvers by default.")]
    public string SetApproversWarningMsg => this[nameof (SetApproversWarningMsg)];

    /// <summary>Phrase: Saved at</summary>
    [ResourceEntry("SavedAt", Description = "Phrase: Saved at", LastModified = "2010/12/13", Value = "Saved at")]
    public string SavedAt => this[nameof (SavedAt)];

    /// <summary>Phrase: Saved on</summary>
    [ResourceEntry("SavedOn", Description = "Phrase: Saved on", LastModified = "2010/12/13", Value = "Saved on")]
    public string SavedOn => this[nameof (SavedOn)];

    /// <summary>Word: at</summary>
    [ResourceEntry("At", Description = "Word: at", LastModified = "2010/12/13", Value = "at")]
    public string At => this[nameof (At)];

    /// <summary>Word: on</summary>
    [ResourceEntry("On", Description = "Word: on", LastModified = "2010/12/13", Value = "on")]
    public string On => this[nameof (On)];

    /// <summary>Phrase: to be published on</summary>
    [ResourceEntry("ToBePublishedOn", Description = "Phrase: to be published on", LastModified = "2010/12/13", Value = "to be published on")]
    public string ToBePublishedOn => this[nameof (ToBePublishedOn)];

    /// <summary>Phrase: to be published at</summary>
    [ResourceEntry("ToBePublishedAt", Description = "Phrase: to be published at", LastModified = "2010/12/13", Value = "to be published at")]
    public string ToBePublishedAt => this[nameof (ToBePublishedAt)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UploadAndPublish", Description = "upload and publish workflow action name", LastModified = "2018/10/15", Value = "Publish")]
    public string UploadAndPublish => this[nameof (UploadAndPublish)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SaveAsPrivateDraftAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Save as Private draft")]
    public string SaveAsPrivateDraftAction => this[nameof (SaveAsPrivateDraftAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SaveAsDraftAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Save as Draft")]
    public string SaveAsDraftAction => this[nameof (SaveAsDraftAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UploadAndSaveAsDraftAction", Description = "a workflow action name", LastModified = "2018/10/15", Value = "Save as Draft")]
    public string UploadAndSaveAsDraftAction => this[nameof (UploadAndSaveAsDraftAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UploadAndSendForApprovalAction", Description = "a workflow action name", LastModified = "2018/10/15", Value = "Send for Approval")]
    public string UploadAndSendForApprovalAction => this[nameof (UploadAndSendForApprovalAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UploadAndSendForReviewAction", Description = "a workflow action name", LastModified = "2018/10/15", Value = "Send for Review")]
    public string UploadAndSendForReviewAction => this[nameof (UploadAndSendForReviewAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("PublishAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Publish")]
    public string PublishAction => this[nameof (PublishAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UnpublishAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Unpublish")]
    public string UnpublishAction => this[nameof (UnpublishAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("UpdateContent", Description = "a workflow action name", LastModified = "2011/01/28", Value = "Update content")]
    public string UpdateContent => this[nameof (UpdateContent)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("ScheduleAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Publish/Unpublish on Specific Date")]
    public string ScheduleAction => this[nameof (ScheduleAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("ScheduleOperationTitle", Description = "a workflow action name", LastModified = "2017/10/16", Value = "Schedule publish/unpublish")]
    public string ScheduleOperationTitle => this[nameof (ScheduleOperationTitle)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SendForApprovalAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Send for Approval")]
    public string SendForApprovalAction => this[nameof (SendForApprovalAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SendForReviewAction", Description = "a workflow action name", LastModified = "2018/7/10", Value = "Send for Review")]
    public string SendForReviewAction => this[nameof (SendForReviewAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SaveAsAwaitingReviewAction", Description = "a workflow action name", LastModified = "2018/07/11", Value = "Save as Awaiting review")]
    public string SaveAsAwaitingReviewAction => this[nameof (SaveAsAwaitingReviewAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("AwaitingApprovalAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Save as Awaiting approval")]
    public string AwaitingApprovalAction => this[nameof (AwaitingApprovalAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("AwaitingPublishingAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Save as Awaiting publishing")]
    public string AwaitingPublishingAction => this[nameof (AwaitingPublishingAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("RejectAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Reject")]
    public string RejectAction => this[nameof (RejectAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("DeleteAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Delete")]
    public string DeleteAction => this[nameof (DeleteAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("SendForPublishingAction", Description = "a workflow action name", LastModified = "2010/11/14", Value = "Send for Publishing")]
    public string SendForPublishingAction => this[nameof (SendForPublishingAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("StopSchedulingAction", Description = "a workflow action name", LastModified = "2018/04/17", Value = "Stop scheduling")]
    public string StopSchedulingAction => this[nameof (StopSchedulingAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("StopScheduleAction", Description = "a workflow action name", LastModified = "2010/12/01", Value = "Stop Schedule")]
    public string StopScheduleAction => this[nameof (StopScheduleAction)];

    /// <summary>workflow action button label</summary>
    [ResourceEntry("DiscardDraftAction", Description = "a workflow action name", LastModified = "2011/01/14", Value = "Discard Draft")]
    public string DiscardDraftAction => this[nameof (DiscardDraftAction)];

    [ResourceEntry("MobilePreview", Description = "phrase: Preview for Smartphones & Tablets", LastModified = "2012/02/03", Value = "Preview for Smartphones & Tablets")]
    public string MobilePreview => this[nameof (MobilePreview)];

    /// <summary>
    /// Phrase: This item has translated versions.
    /// If you publish the item all language translations will be also published also.
    /// Are you sure you want to continue publishing?
    /// </summary>
    [ResourceEntry("PublishInMultilingualWarning", Description = "Warning dipslayed when publishing an item", LastModified = "2011/01/07", Value = "This item has translated versions.\r\nIf you publish the item all translations will be also published.\r\nAre you sure you want to continue publishing?")]
    public string PublishInMultilingualWarning => this[nameof (PublishInMultilingualWarning)];

    /// <summary>
    /// Phrase: This item has translated versions.
    /// If you unpublish the item all language translations will be also published also.
    /// Are you sure you want to continue unpublishing?
    /// </summary>
    [ResourceEntry("UnPublishInMultilingualWarning", Description = "Warning dipslayed when unpublishing an item", LastModified = "2011/01/07", Value = "This item has translated versions.\r\nIf you unpublish the item all translations will be also unpublished.\r\nAre you sure you want to continue unpublishing?")]
    public string UnPublishInMultilingualWarning => this[nameof (UnPublishInMultilingualWarning)];

    /// <summary>Phrase: to be published at</summary>
    [ResourceEntry("PublishPageInMultilingualWarning", Description = "Warning dipslayed when publishing a page", LastModified = "2011/01/07", Value = "If you publish a synchronized page all translations will be published too.\r\nAre you sure you want to continue publishing?")]
    public string PublishPageInMultilingualWarning => this[nameof (PublishPageInMultilingualWarning)];

    /// <summary>
    /// Title of the message box shown when publishing page in multilingual mode
    /// </summary>
    [ResourceEntry("PromptTitlePublishPageInMultilingual", Description = "Title of the message box shown when publishing page in multilingual mode.", LastModified = "2011/01/12", Value = "Publish")]
    public string PromptTitlePublishPageInMultilingual => this[nameof (PromptTitlePublishPageInMultilingual)];

    /// <summary>
    /// Title of the message box shown when unpublishing page in multilingual mode
    /// </summary>
    [ResourceEntry("PromptTitleUnPublishPageInMultilingual", Description = "Title of the message box shown when unpublishing page in multilingual mode.", LastModified = "2011/01/12", Value = "UnPublish")]
    public string PromptTitleUnPublishPageInMultilingual => this[nameof (PromptTitleUnPublishPageInMultilingual)];

    /// <summary>All content types</summary>
    /// <value>All content types</value>
    [ResourceEntry("AllContentTypes", Description = "All content types", LastModified = "2015/02/19", Value = "All content types")]
    public string AllContentTypes => this[nameof (AllContentTypes)];

    /// <summary>by Content types...</summary>
    /// <value>by Content types...</value>
    [ResourceEntry("ByContentTypes", Description = "by Content types...", LastModified = "2015/02/19", Value = "by Content types...")]
    public string ByContentTypes => this[nameof (ByContentTypes)];

    /// <summary>by Sites...</summary>
    /// <value>by Sites...</value>
    [ResourceEntry("BySites", Description = "by Sites...", LastModified = "2015/02/19", Value = "by Sites...")]
    public string BySites => this[nameof (BySites)];

    /// <summary>by Languages...</summary>
    /// <value>by Languages...</value>
    [ResourceEntry("ByLanguages", Description = "by Languages...", LastModified = "2015/02/19", Value = "by Languages...")]
    public string ByLanguages => this[nameof (ByLanguages)];

    /// <summary>phrase: Select content types</summary>
    /// <value>Select content types</value>
    [ResourceEntry("SelectContentType", Description = "phrase: Select content types", LastModified = "2015/02/20", Value = "Select content types")]
    public string SelectContentType => this[nameof (SelectContentType)];

    /// <summary>phrase: Workflow by site</summary>
    /// <value>Workflow by site</value>
    [ResourceEntry("WorkflowBySite", Description = "phrase: Workflow by site", LastModified = "2015/02/23", Value = "Workflow by site")]
    public string WorkflowBySite => this[nameof (WorkflowBySite)];

    /// <summary>phrase: Workflow by language</summary>
    /// <value>Workflow by language</value>
    [ResourceEntry("WorkflowByLanguage", Description = "phrase: Workflow by language", LastModified = "2015/02/23", Value = "Workflow by language")]
    public string WorkflowByLanguage => this[nameof (WorkflowByLanguage)];

    /// <summary>phrase: close content type</summary>
    /// <value>close content type</value>
    [ResourceEntry("CloseContentScopeFilter", Description = "phrase: close content type", LastModified = "2015/02/23", Value = "close content type")]
    public string CloseContentScopeFilter => this[nameof (CloseContentScopeFilter)];

    /// <summary>phrase: close site</summary>
    /// <value>close site</value>
    [ResourceEntry("CloseSiteScopeFilter", Description = "phrase: close site", LastModified = "2015/02/23", Value = "close site")]
    public string CloseSiteScopeFilter => this[nameof (CloseSiteScopeFilter)];

    /// <summary>phrase: close language</summary>
    /// <value>close language</value>
    [ResourceEntry("closeCultureScopeFilter", Description = "phrase: close language", LastModified = "2015/02/23", Value = "close language")]
    public string closeCultureScopeFilter => this[nameof (closeCultureScopeFilter)];

    /// <summary>phrase: Workflow priority</summary>
    /// <value>Workflow priority</value>
    [ResourceEntry("WorkflowPriority", Description = "phrase: Workflow priority", LastModified = "2015/02/24", Value = "Workflow priority")]
    public string WorkflowPriority => this[nameof (WorkflowPriority)];

    /// <summary>
    /// Phrase: If a content item matches scopes of multiple workflows, the workflow with the most specific scope will be applied
    /// </summary>
    /// <value>If a content item matches scopes of multiple workflows, the workflow with the most specific scope will be applied</value>
    [ResourceEntry("WorkflowPriorityDescription", Description = "Phrase: If a content item matches scopes of multiple workflows, the workflow with the most specific scope will be applied", LastModified = "2015/02/24", Value = "If a content item matches scopes of multiple workflows, the workflow with the most specific scope will be applied")]
    public string WorkflowPriorityDescription => this[nameof (WorkflowPriorityDescription)];

    /// <summary>phrase: Learn more</summary>
    /// <value>Learn more</value>
    [ResourceEntry("LearnMore", Description = "phrase: Learn more", LastModified = "2015/02/24", Value = "Learn more")]
    public string LearnMore => this[nameof (LearnMore)];

    /// <summary>
    /// Gets External Link: Overview content lifecycle and workflow
    /// </summary>
    /// <value>Learn more</value>
    [ResourceEntry("ExternalLinkContentLifecycleWorkflow", Description = "External Link: Overview content lifecycle and workflow", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-content-lifecycle-and-workflow")]
    public string ExternalLinkContentLifecycleWorkflow => this[nameof (ExternalLinkContentLifecycleWorkflow)];

    /// <summary>phrase: by Priority</summary>
    /// <value>by Priority</value>
    [ResourceEntry("ByPriority", Description = "phrase: by Priority", LastModified = "2015/02/25", Value = "by Priority")]
    public string ByPriority => this[nameof (ByPriority)];

    /// <summary>phrase: Last created on top</summary>
    /// <value>Last created on top</value>
    [ResourceEntry("LastCreatedOnTop", Description = "phrase: Last created on top", LastModified = "2015/02/25", Value = "Last created on top")]
    public string LastCreatedOnTop => this[nameof (LastCreatedOnTop)];

    /// <summary>phrase: Last modified on top</summary>
    /// <value>Last modified on top</value>
    [ResourceEntry("LastModifiedOnTop", Description = "phrase: Last modified on top", LastModified = "2015/02/25", Value = "Last modified on top")]
    public string LastModifiedOnTop => this[nameof (LastModifiedOnTop)];

    /// <summary>phrase: Alphabetically (A-Z)</summary>
    /// <value>Alphabetically (A-Z)</value>
    [ResourceEntry("AlphabeticallyAZ", Description = "phrase: Alphabetically (A-Z)", LastModified = "2015/02/25", Value = "Alphabetically (A-Z)")]
    public string AlphabeticallyAZ => this[nameof (AlphabeticallyAZ)];

    /// <summary>phrase: Alphabetically (Z-A)</summary>
    /// <value>Alphabetically (Z-A)</value>
    [ResourceEntry("AlphabeticallyZA", Description = "phrase: Alphabetically (Z-A)", LastModified = "2015/02/25", Value = "Alphabetically (Z-A)")]
    public string AlphabeticallyZA => this[nameof (AlphabeticallyZA)];

    /// <summary>Gets External Link: No approval workflow</summary>
    [ResourceEntry("ExternalLinkNoApprovalWorkflow", Description = "External Link: No approval workflow", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-content-lifecycle-and-workflow")]
    public string ExternalLinkNoApprovalWorkflow => this[nameof (ExternalLinkNoApprovalWorkflow)];

    /// <summary>phrase: No approval workflow</summary>
    [ResourceEntry("NoApprovalWorkflow", Description = "phrase: No approval workflow", LastModified = "2018/08/30", Value = "<strong>No approval workflow</strong><div class='sfNote sfSmallerTxt'>Create &gt; Publish<div class=\"sfMTop5\">Exclude content from another workflow. <a href=\"{0}\" target=\"_blank\">Learn more </a></div></div>")]
    public string NoApprovalWorkflow => this[nameof (NoApprovalWorkflow)];

    /// <summary>phrase: No approval workflow</summary>
    [ResourceEntry("NoApprovalWorkflowPanelTitle", Description = "phrase: No approval workflow", LastModified = "2018/08/07", Value = "No approval worfklow")]
    public string NoApprovalWorkflowPanelTitle => this[nameof (NoApprovalWorkflowPanelTitle)];

    /// <summary>
    /// phrase: Content and pages in the selected scope don't require approval
    /// </summary>
    [ResourceEntry("NoApprovalWorkflowPanelBody", Description = "phrase: Content and pages in the selected scope don't require approval", LastModified = "2018/08/07", Value = "Content and pages in the selected scope don't require approval")]
    public string NoApprovalWorkflowPanelBody => this[nameof (NoApprovalWorkflowPanelBody)];

    /// <summary>phrase: Custom list of email addresses...</summary>
    /// <value>Custom list of email addresses...</value>
    [ResourceEntry("SendEmailToListOfEmailAddresses", Description = "phrase: Custom list of email addresses...", LastModified = "2015/02/24", Value = "Custom list of email addresses...")]
    public string SendEmailToListOfEmailAddresses => this[nameof (SendEmailToListOfEmailAddresses)];

    /// <summary>pfrase: Enter email addresses, one per line</summary>
    /// <value>Enter email addresses, one per line</value>
    [ResourceEntry("EnterEmailAddresses", Description = "pfrase: Enter email addresses, one per line", LastModified = "2019/07/19", Value = "Enter email addresses, one per line")]
    public string EnterEmailAddresses => this[nameof (EnterEmailAddresses)];

    /// <summary>phrase: Validate emails</summary>
    [ResourceEntry("ValidateEmails", Description = "phrase: Validate emails", LastModified = "2013/11/28", Value = "Validate emails")]
    public string ValidateEmails => this[nameof (ValidateEmails)];

    /// <summary>
    /// phrase: The following entries are not valid email addresses: {0}
    /// </summary>
    [ResourceEntry("WorkflowEmailListError", Description = "phrase: The following entries are not valid email addresses: <br />{0}", LastModified = "2013/11/22", Value = "The following entries are not valid email addresses: {0}")]
    public string WorkflowEmailListError => this[nameof (WorkflowEmailListError)];

    /// <summary>phrase: All entries are valid email addresses</summary>
    [ResourceEntry("WorkflowEmailListSuccess", Description = "phrase: All entries are valid email addresses", LastModified = "2013/11/22", Value = "All entries are valid email addresses")]
    public string WorkflowEmailListSuccess => this[nameof (WorkflowEmailListSuccess)];

    /// <summary>phrase: Who can skip the workflow?</summary>
    /// <value>Who can skip the workflow?</value>
    [ResourceEntry("AllowWorkflowToBeSkipped", Description = "phrase: Allow workflow to be skipped by...", LastModified = "2018/08/29", Value = "Allow workflow to be skipped by...")]
    public string AllowWorkflowToBeSkipped => this[nameof (AllowWorkflowToBeSkipped)];

    /// <summary>phrase: Approvers</summary>
    /// <value>Approvers</value>
    [ResourceEntry("AllowPublishersToSkipWorkflow", Description = "phrase: Approvers", LastModified = "2015/02/26", Value = "Approvers")]
    public string AllowPublishersToSkipWorkflow => this[nameof (AllowPublishersToSkipWorkflow)];

    /// <summary>phrase: Approvers for Level</summary>
    /// <value>Approvers for Level</value>
    [ResourceEntry("ApproversForLevel", Description = "phrase: Approvers for Level", LastModified = "2018/08/09", Value = "<b class=\"sfDialogHeadingStep\">{0}</b> Approvers for Level {0}")]
    public string ApproversForLevel => this[nameof (ApproversForLevel)];

    /// <summary>phrase: Approvers for Level 2</summary>
    /// <value>Approvers for Level 2</value>
    [ResourceEntry("AllowPublishersToSkipWorkflowLevelTwo", Description = "phrase: Approvers for Level 2", LastModified = "2015/02/26", Value = "Approvers for Level 2")]
    public string AllowPublishersToSkipWorkflowLevelTwo => this[nameof (AllowPublishersToSkipWorkflowLevelTwo)];

    /// <summary>phrase: Approvers for Level 3</summary>
    /// <value>Approvers for Level 2</value>
    [ResourceEntry("AllowPublishersToSkipWorkflowLevelThree", Description = "phrase: Approvers for Level 3", LastModified = "2015/02/26", Value = "Approvers for Level 3")]
    public string AllowPublishersToSkipWorkflowLevelThree => this[nameof (AllowPublishersToSkipWorkflowLevelThree)];

    /// <summary>phrase: No approvers</summary>
    /// <value>No approvers</value>
    [ResourceEntry("NoApprovers", Description = "phrase: No approvers", LastModified = "2015/02/26", Value = "No approvers")]
    public string NoApprovers => this[nameof (NoApprovers)];

    /// <summary>
    /// phrase: All users with proper permissions can create and publish content
    /// </summary>
    /// <value>All users with proper permissions can create and publish content</value>
    [ResourceEntry("NoApproversDescription", Description = "phrase: All users with proper permissions can create and publish content", LastModified = "2015/02/26", Value = "All users with proper permissions can create and publish content")]
    public string NoApproversDescription => this[nameof (NoApproversDescription)];

    /// <summary>phrase: No approval</summary>
    /// <value>No approval</value>
    [ResourceEntry("NoApproval", Description = "phrase: No approval", LastModified = "2015/02/26", Value = "No approval")]
    public string NoApproval => this[nameof (NoApproval)];

    /// <summary>phrase: Manage workflows</summary>
    /// <value>Manage workflows</value>
    [ResourceEntry("ManageWorkflows", Description = "phrase: Manage workflows", LastModified = "2015/02/26", Value = "Manage workflows")]
    public string ManageWorkflows => this[nameof (ManageWorkflows)];

    /// <summary>phrase: Status</summary>
    [ResourceEntry("Status", Description = "phrase: Status", LastModified = "2018/02/23", Value = "Status")]
    public string Status => this[nameof (Status)];

    /// <summary>phrase: Action</summary>
    [ResourceEntry("Action", Description = "phrase: Action", LastModified = "2018/08/22", Value = "Action")]
    public string Action => this[nameof (Action)];

    /// <summary>phrase: Published</summary>
    [ResourceEntry("Published", Description = "The text of published items.", LastModified = "2018/01/30", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>phrase: Published</summary>
    [ResourceEntry("Unpublished", Description = "The text of unpublished items.", LastModified = "2018/03/21", Value = "Unpublished")]
    public string Unpublished => this[nameof (Unpublished)];

    /// <summary>word: Drafts</summary>
    [ResourceEntry("Draft", Description = "The text of draft items.", LastModified = "2018/01/30", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("WaitingForApproval", Description = "The text of the 'Awaiting approval'.", LastModified = "2018/01/30", Value = "Awaiting approval")]
    public string WaitingForApproval => this[nameof (WaitingForApproval)];

    /// <summary>word: Scheduled</summary>
    [ResourceEntry("Scheduled", Description = "The text of scheduled items.", LastModified = "2018/01/30", Value = "Scheduled")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>word: My Items</summary>
    [ResourceEntry("MyItems", Description = "The text of my items label.", LastModified = "2018/01/30", Value = "MyItems")]
    public string MyItems => this[nameof (MyItems)];

    /// <summary>word: Add</summary>
    [ResourceEntry("AddScopeButtonLabel", Description = "The add scope button label.", LastModified = "2018/07/31", Value = "Add")]
    public string AddScopeButtonLabel => this[nameof (AddScopeButtonLabel)];

    /// <summary>word: Add</summary>
    [ResourceEntry("DefineScopeButtonLabel", Description = "Define button label.", LastModified = "2018/08/02", Value = "Define scope")]
    public string DefineScopeButtonLabel => this[nameof (DefineScopeButtonLabel)];

    /// <summary>word: Site</summary>
    [ResourceEntry("SiteColumnHeader", Description = "The site column header.", LastModified = "2018/06/28", Value = "Site")]
    public string SiteColumnHeader => this[nameof (SiteColumnHeader)];

    /// <summary>word: Language</summary>
    [ResourceEntry("LanguageColumnHeader", Description = "The language column header.", LastModified = "2018/06/28", Value = "Language")]
    public string LanguageColumnHeader => this[nameof (LanguageColumnHeader)];

    /// <summary>phrase: Content and pages</summary>
    [ResourceEntry("ContentColumnHeader", Description = "The content column header.", LastModified = "2018/06/28", Value = "Content and pages")]
    public string ContentColumnHeader => this[nameof (ContentColumnHeader)];

    /// <summary>
    /// Get phrase: 'You are about to send this item for approval'
    /// </summary>
    [ResourceEntry("SendToApprovalText", Description = "phrase: You are about to send this item for approval", LastModified = "2018/07/09", Value = "You are about to send this item for approval")]
    public string SendToApprovalText => this[nameof (SendToApprovalText)];

    /// <summary>Get phrase: 'Add notes for the approvers (optional)'</summary>
    [ResourceEntry("SendToApprovalNote", Description = "phrase: Add notes for the approvers (optional)", LastModified = "2018/07/09", Value = "Add notes for the approvers <span class='sfNote'>(optional)</span>")]
    public string SendToApprovalNote => this[nameof (SendToApprovalNote)];

    /// <summary>Get phrase 'Notes for approvers'</summary>
    [ResourceEntry("NotesForApprovers", Description = "phrase: Notes for approvers", LastModified = "2018/07/09", Value = "Notes for approvers")]
    public string NotesForApprovers => this[nameof (NotesForApprovers)];

    /// <summary>Get word 'Notes'</summary>
    [ResourceEntry("Notes", Description = "word: Notes", LastModified = "2018/07/20", Value = "Notes")]
    public string Notes => this[nameof (Notes)];

    /// <summary>Get phrase 'Allow notes to approvers'</summary>
    [ResourceEntry("AllowNotes", Description = "phrase: Users can add notes when sending content to the next level of approval", LastModified = "2018/07/20", Value = "Users can add notes when sending content to the next level of approval")]
    public string AllowNotes => this[nameof (AllowNotes)];

    /// <summary>
    /// Get phrase: Apply to all child pages of the selected pages
    /// </summary>
    [ResourceEntry("ApplyWorkflowToChildPagesCheckboxLabel", Description = "phrase: Apply to all child pages of the selected pages", LastModified = "2018/07/20", Value = "Apply to all child pages of the selected pages")]
    public string ApplyWorkflowToChildPagesCheckboxLabel => this[nameof (ApplyWorkflowToChildPagesCheckboxLabel)];

    /// <summary>phrase: Name/Action/Status</summary>
    [ResourceEntry("LevelActionStatusColumnHeader", Description = "The level/action/status column header.", LastModified = "2018/06/06", Value = "Level/Action/Status")]
    public string LevelActionStatusColumnHeader => this[nameof (LevelActionStatusColumnHeader)];

    /// <summary>phrase: Approvers</summary>
    [ResourceEntry("ApproversLabel", Description = "The approvers label.", LastModified = "2018/06/06", Value = "Approvers")]
    public string ApproversLabel => this[nameof (ApproversLabel)];

    /// <summary>phrase: Approvers</summary>
    [ResourceEntry("ApproversColumnHeader", Description = "The approvers label.", LastModified = "2018/06/06", Value = "Approvers")]
    public string ApproversColumnHeader => this[nameof (ApproversColumnHeader)];

    /// <summary>phrase: Notifications</summary>
    [ResourceEntry("NotificationsColumnHeader", Description = "The notifications column header.", LastModified = "2018/06/06", Value = "Notifications")]
    public string NotificationsColumnHeader => this[nameof (NotificationsColumnHeader)];

    /// <summary>phrase: On</summary>
    [ResourceEntry("NotificationsOnSwitch", Description = "Enabled notifications label.", LastModified = "2018/06/13", Value = "On")]
    public string NotificationsOnSwitch => this[nameof (NotificationsOnSwitch)];

    /// <summary>phrase: Off</summary>
    [ResourceEntry("NotificationsOffSwitch", Description = "Disabled notifications label.", LastModified = "2018/06/13", Value = "Off")]
    public string NotificationsOffSwitch => this[nameof (NotificationsOffSwitch)];

    /// <summary>phrase: Button label</summary>
    [ResourceEntry("ChangeLevelButtonLabel", Description = "Workflow Change Level button Label.", LastModified = "2018/06/12", Value = "Change levels")]
    public string ChangeLevelButtonLabel => this[nameof (ChangeLevelButtonLabel)];

    /// <summary>phrase: link label</summary>
    [ResourceEntry("ChangeApproversLabel", Description = "Workflow change approvers Label.", LastModified = "2018/07/30", Value = "Change approvers")]
    public string ChangeApproversLabel => this[nameof (ChangeApproversLabel)];

    /// <summary>phrase: link label</summary>
    [ResourceEntry("RenameLabel", Description = "Workflow rename approver level Label.", LastModified = "2018/07/30", Value = "Rename")]
    public string RenameLabel => this[nameof (RenameLabel)];

    /// <summary>phrase: link label</summary>
    [ResourceEntry("SetNotificationsLabel", Description = "Workflow set notifications label.", LastModified = "2018/07/30", Value = "Set notifications")]
    public string SetNotificationsLabel => this[nameof (SetNotificationsLabel)];

    /// <summary>phrase: header label</summary>
    [ResourceEntry("DefineLevelsOfApproval", Description = "Workflow Define levels of approval header label.", LastModified = "2018/07/30", Value = "Define levels of approval")]
    public string DefineLevelsOfApproval => this[nameof (DefineLevelsOfApproval)];

    /// <summary>phrase: header label</summary>
    [ResourceEntry("DefineScope", Description = "Workflow Define scope header label.", LastModified = "2018/07/30", Value = "Define scope")]
    public string DefineScope => this[nameof (DefineScope)];

    /// <summary>Gets phrase: Rejected item will be returned for {0}.</summary>
    [ResourceEntry("ReturnedForReviewFormatLabel", Description = "phrase: Rejected item will be returned for {0}.", LastModified = "2018/07/31", Value = "Rejected item will be returned for {0}.")]
    public string ReturnedForReviewFormatLabel => this[nameof (ReturnedForReviewFormatLabel)];

    /// <summary>
    /// Gets phrase: Rejected item will be returned to the author.
    /// </summary>
    [ResourceEntry("ReturnedToAuthorLabel", Description = "phrase: Rejected item will be returned to the author.", LastModified = "2018/07/31", Value = "Rejected item will be returned to the author.")]
    public string ReturnedToAuthorLabel => this[nameof (ReturnedToAuthorLabel)];

    /// <summary>phrase: header label</summary>
    [ResourceEntry("AdministratorsOnly", Description = "Workflow Administrators only label.", LastModified = "2018/08/01", Value = "Administrators only")]
    public string AdministratorsOnly => this[nameof (AdministratorsOnly)];

    /// <summary>phrase: checkbox label</summary>
    [ResourceEntry("ApplyToAllContentTypes", Description = "Apply to all newly created content types.", LastModified = "2018/08/01", Value = "Apply to all newly created content types")]
    public string ApplyToAllContentTypes => this[nameof (ApplyToAllContentTypes)];

    /// <summary>phrase: set notification dialog header</summary>
    [ResourceEntry("NotificationsForLavel", Description = "Notifications for level...", LastModified = "2018/08/03", Value = "Notifications for level {0}")]
    public string NotificationForLavel => this["NotificationsForLavel"];

    /// <summary>phrase: set notification dialog header</summary>
    [ResourceEntry("SetNotificationDialogDesc", Description = "Who will be notified when an item is sent for {0}?", LastModified = "2018/08/03", Value = "Who will be notified when an item is sent for {0}?")]
    public string SetNotificationDialogDesc => this[nameof (SetNotificationDialogDesc)];

    /// <summary>
    /// phrase: Notify users by email when an item is waiting for their approval.
    /// </summary>
    [ResourceEntry("NotifyAdministrators", Description = "word: Administrators", LastModified = "2018/08/03", Value = "Administrators")]
    public string NotifyAdministrators => this[nameof (NotifyAdministrators)];

    /// <summary>phrase: Define approval workflow</summary>
    [ResourceEntry("DefineApprovalWorkflow", Description = "phrase: Define approval workflow", LastModified = "2018/02/08", Value = "Define approval workflow")]
    public string DefineApprovalWorkflow => this[nameof (DefineApprovalWorkflow)];

    /// <summary>phrase: Edit approval workflow</summary>
    [ResourceEntry("EditApprovalWorkflow", Description = "phrase: Edit approval workflow", LastModified = "2018/09/06", Value = "Edit approval workflow")]
    public string EditApprovalWorkflow => this[nameof (EditApprovalWorkflow)];

    /// <summary>phrase: Define level</summary>
    [ResourceEntry("DefineLevel", Description = "phrase: Define levels", LastModified = "2018/08/09", Value = "Define levels")]
    public string DefineLevel => this[nameof (DefineLevel)];

    [ResourceEntry("SitesAll", Description = "", LastModified = "2018/08/08", Value = "All sites")]
    public string SitesAll => this[nameof (SitesAll)];

    /// <summary>phrase: and {0} more</summary>
    [ResourceEntry("AndMoreFormat", Description = "Suffix that is displayed when many items are selected.", LastModified = "2018/08/09", Value = " and {0} more")]
    public string AndMoreContentItemsFormat => this["AndMoreFormat"];

    /// <summary>phrase: and {0} more sites</summary>
    [ResourceEntry("AndMoreAreasFormat", Description = "Suffix that is displayed when many scope areas are selected.", LastModified = "2018/08/09", Value = " and {0} more areas")]
    public string AndMoreAreasFormat => this[nameof (AndMoreAreasFormat)];

    /// <summary>phrase: and {0} more</summary>
    [ResourceEntry("Separator", Description = "Value separator.", LastModified = "2018/08/09", Value = ", ")]
    public string Separator => this[nameof (Separator)];

    /// <summary>Gets NoLanuageItemFormat</summary>
    [ResourceEntry("ScopeItemFormatFull", Description = "Extended format for scope items.", LastModified = "2018/08/09", Value = "{0} ({1}): {2}")]
    public string ScopeItemFormatFull => this[nameof (ScopeItemFormatFull)];

    /// <summary>Gets NoLanuageItemFormat</summary>
    [ResourceEntry("ScopeItemFormatSimple", Description = "Simple format for scope items.", LastModified = "2018/08/09", Value = "{0}: {1}")]
    public string ScopeItemFormatSimple => this[nameof (ScopeItemFormatSimple)];

    /// <summary>phrase: Scope is required - vallidations message</summary>
    [ResourceEntry("ScopeIsRequired", Description = "Validation message", LastModified = "2018/08/16", Value = "Scope is required")]
    public string ScopeIsRequired => this[nameof (ScopeIsRequired)];

    /// <summary>
    /// phrase: Levels of approval are required - vallidations message
    /// </summary>
    [ResourceEntry("LevelsAreRequired", Description = "Validation message", LastModified = "2018/08/16", Value = "Levels of approval are required")]
    public string LevelsAreRequired => this[nameof (LevelsAreRequired)];

    /// <summary>phrase: Send for</summary>
    [ResourceEntry("SendFor", Description = "label: Send for", LastModified = "2018/08/22", Value = "Send for")]
    public string SendFor => this[nameof (SendFor)];

    /// <summary>phrase: Waiting for</summary>
    [ResourceEntry("WaitingFor", Description = "label: Waiting for", LastModified = "2018/08/22", Value = "Waiting for")]
    public string WaitingFor => this[nameof (WaitingFor)];

    /// <summary>phrase: Review</summary>
    [ResourceEntry("Review", Description = "label: Review", LastModified = "2018/08/22", Value = "Review")]
    public string Review => this[nameof (Review)];

    /// <summary>phrase: Approval</summary>
    [ResourceEntry("Approval", Description = "label: Approval", LastModified = "2018/08/22", Value = "Approval")]
    public string Approval => this[nameof (Approval)];

    /// <summary>phrase: Publishing</summary>
    [ResourceEntry("Publishing", Description = "label: Publishing", LastModified = "2018/08/22", Value = "Publishing")]
    public string Publishing => this[nameof (Publishing)];

    /// <summary>phrase: Content types and pages</summary>
    [ResourceEntry("ContentTypesHeader", Description = "phrase: Content types and pages", LastModified = "2018/08/29", Value = "Content types and pages")]
    public string ContentTypesHeader => this[nameof (ContentTypesHeader)];

    /// <summary>
    /// Gets phrase: The content in the defined scope does not exist anymore.
    /// </summary>
    [ResourceEntry("ContentScopeDeleted", Description = "phrase: The content in the defined scope does not exist anymore.", LastModified = "2018/08/29", Value = "The content in the defined scope does not exist anymore.")]
    public string ContentScopeDeleted => this[nameof (ContentScopeDeleted)];

    /// <summary>Gets phrase: This workflow is not applied anywhere.</summary>
    [ResourceEntry("WorkflowNotApplied", Description = "phrase: This workflow is not applied anywhere.", LastModified = "2018/08/29", Value = "This workflow is not applied anywhere.")]
    public string WorkflowNotApplied => this[nameof (WorkflowNotApplied)];
  }
}
