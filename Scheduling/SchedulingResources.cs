// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// Localization messages related to Sitefinity's scheduling sub-system
  /// </summary>
  [ObjectInfo("SchedulingResources", ResourceClassId = "SchedulingResources")]
  public class SchedulingResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.SchedulingResources" /> class.
    /// </summary>
    public SchedulingResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.SchedulingResources" /> class.
    /// </summary>
    /// <param name="provider">The provider.</param>
    public SchedulingResources(ResourceDataProvider provider)
      : base(provider)
    {
    }

    /// <summary>Translated message, similar to "Default Provider"</summary>
    /// <value>Name of the default provider for the publishing system.</value>
    [ResourceEntry("DefaultProvider", Description = "Name of the default provider for the publishing system.", LastModified = "2010/07/13", Value = "Default Provider")]
    public string DefaultProvider => this[nameof (DefaultProvider)];

    /// <summary>Translated message, similar to "Scheduling"</summary>
    /// <value>The title of this resource class.</value>
    [ResourceEntry("ModuleTitle", Description = "The title of this module ", LastModified = "2010/11/24", Value = "Scheduling")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Massage for describing Scheduling module.</summary>
    [ResourceEntry("ModuleDescription", Description = "The description of the Scheduling module", LastModified = "2011/08/08", Value = "Provides functionality for scheduled tasks")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>Translated message, similar to "Scheduling"</summary>
    /// <value>The title of this resource class.</value>
    [ResourceEntry("SchedulingResourcesTitle", Description = "The title of this resource class", LastModified = "2010/11/24", Value = "Scheduling")]
    public string SchedulingResourcesTitle => this[nameof (SchedulingResourcesTitle)];

    /// <summary>Translated message, similar to "Scheduling messages"</summary>
    /// <value>Error Messages Title plural</value>
    [ResourceEntry("SchedulingResourcesTitlePlural", Description = "The title plural of this class", LastModified = "2010/11/24", Value = "Scheduling messages")]
    public string SchedulingResourcesTitlePlural => this[nameof (SchedulingResourcesTitlePlural)];

    /// <summary>
    /// Translated message, similar to "Contains localizable resources for Sitefinity's scheduling sub-system"
    /// </summary>
    /// <value>The description of this class.</value>
    [ResourceEntry("SchedulingResourcesDescription", Description = "The description of this class", LastModified = "2012/01/05", Value = "Contains localizable resources for Sitefinity scheduling sub-system")]
    public string SchedulingResourcesDescription => this[nameof (SchedulingResourcesDescription)];

    /// <summary>
    /// Translated message, similar to "Scheduling configuration"
    /// </summary>
    /// <value>Configuration section description.</value>
    [ResourceEntry("SchedulingConfig", Description = "Configuration section description.", LastModified = "2010/11/24", Value = "Scheduling configuration")]
    public string SchedulingConfig => this[nameof (SchedulingConfig)];

    /// <summary>Translated message, similar to "Provider Settings""</summary>
    /// <value>Dictionary of settings for the initialization of scheduling providers</value>
    [ResourceEntry("ProviderSettings", Description = "Dictionary of settings for the initialization of scheduling providers", LastModified = "2010/11/24", Value = "Provider Settings")]
    public string ProviderSettings => this[nameof (ProviderSettings)];

    [ResourceEntry("scheduledTypestrategies", Description = "Dictionary of settings for the initialization of scheduling type strategies", LastModified = "2010/11/24", Value = "Scheduled Type Strategies")]
    public string ScheduledTypeStrategies => this["scheduledTypestrategies"];

    /// <summary>Translated message, similar to "Provider Settings""</summary>
    /// <value>Dictionary of settings for the initialization of scheduling providers</value>
    [ResourceEntry("dataItemsProviderSettings", Description = "Dictionary of settings for the initialization of scheduling providers", LastModified = "2010/11/24", Value = "Provider Data Items Settings")]
    public string DataItemsProviderSettings => this["dataItemsProviderSettings"];

    /// <summary>
    /// Message and description for the maximum days that a task would be visualized on the frontend after its last modification.
    /// Used mainly to filter out old irrelevant failed tasks.
    /// </summary>
    [ResourceEntry("runningTasksVisibilityMaxPeriod", Description = "Sets the maximum days after the last modification of a scheduled background task that would be returned from the frontend-facing service and displayed on the frontent for the correspoding content items.", LastModified = "2020/03/20", Value = "Sets the maximum days after the last modification of a scheduled background task that would be returned from the frontend-facing service and displayed on the frontent for the correspoding content items.")]
    public string RunningTasksVisibilityMaxPeriod => this["runningTasksVisibilityMaxPeriod"];

    /// <summary>Resource string: Disable scheduled tasks execution</summary>
    [ResourceEntry("DisableScheduledTasksExecutionConfigCaption", Description = "Disable scheduled tasks execution", LastModified = "2020/12/29", Value = "Disable scheduled tasks execution")]
    public string DisableScheduledTasksExecutionConfigCaption => this[nameof (DisableScheduledTasksExecutionConfigCaption)];

    /// <summary>
    /// Resource string: Indicates if the scheduled tasks execution is disabled on this instance. This setting should be used only in load balanced setup. Make sure there is at least one instance where scheduled tasks execution is enabled. Requires restart.
    /// </summary>
    [ResourceEntry("DisableScheduledTasksExecutionConfigDescription", Description = "Indicates if the scheduled tasks execution is disabled on this instance. This setting should be used only in load balanced setup. Make sure there is at least one instance where scheduled tasks execution is enabled. Requires restart.", LastModified = "2021/02/01", Value = "Indicates if the scheduled tasks execution is disabled on this instance. This setting should be used only in load balanced setup. Make sure there is at least one instance where scheduled tasks execution is enabled. Requires restart.")]
    public string DisableScheduledTasksExecutionConfigDescription => this[nameof (DisableScheduledTasksExecutionConfigDescription)];

    /// <summary>Title of the scheduling management page</summary>
    [ResourceEntry("SchedulingManagementPageTitle", Description = "The title used for scheduling management page", LastModified = "2021/01/08", Value = "Scheduled tasks")]
    public string SchedulingManagementPageTitle => this[nameof (SchedulingManagementPageTitle)];

    /// <summary>URL of the scheduling management page</summary>
    [ResourceEntry("SchedulingManagementPageUrlName", Description = "The url used for scheduling management page", LastModified = "2021/01/08", Value = "Scheduled-tasks")]
    public string SchedulingManagementPageUrlName => this[nameof (SchedulingManagementPageUrlName)];

    /// <summary>Task name column title</summary>
    [ResourceEntry("TaskNameColumnTitle", Description = "The title of the taskname column in scheduled task management grid", LastModified = "2021/01/08", Value = "Task / Item name")]
    public string TaskNameColumnTitle => this[nameof (TaskNameColumnTitle)];

    /// <summary>Status column title</summary>
    [ResourceEntry("StatusColumnTitle", Description = "The title of the status column in scheduled task management grid", LastModified = "2021/01/08", Value = "Status")]
    public string StatusColumnTitle => this[nameof (StatusColumnTitle)];

    /// <summary>Scheduled for column title</summary>
    [ResourceEntry("ScheduledForColumnTitle", Description = "The title of the Scheduled for column in scheduled task management grid", LastModified = "2021/01/08", Value = "Scheduled for")]
    public string ScheduledForColumnTitle => this[nameof (ScheduledForColumnTitle)];

    /// <summary>Executed on date column title</summary>
    [ResourceEntry("LastExecutedOnColumnTitle", Description = "The title of the Executed on date column in scheduled task management grid", LastModified = "2021/01/08", Value = "Executed on")]
    public string LastExecutedOnColumnTitle => this[nameof (LastExecutedOnColumnTitle)];

    /// <summary>Recurring interval label</summary>
    [ResourceEntry("RecurringIntervalLabel", Description = "text: Recurring interval", LastModified = "2021/01/08", Value = "Recurring interval:")]
    public string RecurringIntervalLabel => this[nameof (RecurringIntervalLabel)];

    /// <summary>Actions column title</summary>
    [ResourceEntry("ActionsColumnTitle", Description = "The title of the Actions column in scheduled task management grid", LastModified = "2021/01/08", Value = "Actions")]
    public string ActionsColumnTitle => this[nameof (ActionsColumnTitle)];

    /// <summary>Retry operation label</summary>
    [ResourceEntry("RetryOperationLabel", Description = "The Retry operation label", LastModified = "2021/01/08", Value = "Retry")]
    public string RetryOperationLabel => this[nameof (RetryOperationLabel)];

    /// <summary>Delete operation label</summary>
    [ResourceEntry("DeleteOperationLabel", Description = "The Delete operation label", LastModified = "2021/01/08", Value = "Delete")]
    public string DeleteOperationLabel => this[nameof (DeleteOperationLabel)];

    /// <summary>Start operation label</summary>
    [ResourceEntry("StartOperationLabel", Description = "The Start operation label", LastModified = "2021/01/08", Value = "Start")]
    public string StartOperationLabel => this[nameof (StartOperationLabel)];

    /// <summary>Delete scheduled task confirmation message</summary>
    [ResourceEntry("DeleteScheduledTaskConfirmation", Description = "Delete this task permanently? message", LastModified = "2021/01/08", Value = "Delete this task permanently?")]
    public string DeleteScheduledTaskConfirmation => this[nameof (DeleteScheduledTaskConfirmation)];

    /// <summary>Delete stopped scheduled task confirmation message</summary>
    [ResourceEntry("DeleteStoppedScheduledTaskConfirmationMessage", Description = "Delete this task permanently?", LastModified = "2021/01/08", Value = "Deleting the task may permanently corrupt items related to this task. <br/> <br/> Do you want to proceed?")]
    public string DeleteStoppedScheduledTaskConfirmationMessage => this[nameof (DeleteStoppedScheduledTaskConfirmationMessage)];

    /// <summary>Delete scheduled task confirmation button</summary>
    [ResourceEntry("DeleteScheduledTaskButton", Description = "Delete task button label", LastModified = "2021/01/08", Value = "Delete task")]
    public string DeleteScheduledTaskButton => this[nameof (DeleteScheduledTaskButton)];

    /// <summary>Canncel button label</summary>
    [ResourceEntry("CancelButtonLabel", Description = "Cancel button label", LastModified = "2021/01/08", Value = "Cancel")]
    public string CancelButtonLabel => this[nameof (CancelButtonLabel)];

    /// <summary>Sort label</summary>
    [ResourceEntry("SortLabel", Description = "Sort label", LastModified = "2021/01/08", Value = "Sort")]
    public string SortLabel => this[nameof (SortLabel)];

    /// <summary>ByScheduledDateLabel label</summary>
    [ResourceEntry("ByScheduledDateLabel", Description = "ByScheduledDateLabel label", LastModified = "2021/01/08", Value = "by Scheduled date")]
    public string ByScheduledDateLabel => this[nameof (ByScheduledDateLabel)];

    /// <summary>ByExecutionDateLabel label</summary>
    [ResourceEntry("ByExecutionDateLabel", Description = "ByExecutionDateLabel label", LastModified = "2021/01/08", Value = "by Execution date")]
    public string ByExecutionDateLabel => this[nameof (ByExecutionDateLabel)];

    /// <summary>AlphabeticallyAZ label</summary>
    [ResourceEntry("AlphabeticallyAZLabel", Description = "AlphabeticallyAZ label", LastModified = "2021/01/08", Value = "Alphabetically (A-Z)")]
    public string AlphabeticallyAZLabel => this[nameof (AlphabeticallyAZLabel)];

    /// <summary>AlphabeticallyZA label</summary>
    [ResourceEntry("AlphabeticallyZALabel", Description = "AlphabeticallyZA label", LastModified = "2021/01/08", Value = "Alphabetically (Z-A)")]
    public string AlphabeticallyZALabel => this[nameof (AlphabeticallyZALabel)];

    /// <summary>By Status label</summary>
    [ResourceEntry("ByStatusLabel", Description = "By Status label", LastModified = "2021/01/08", Value = "by Status")]
    public string ByStatusLabel => this[nameof (ByStatusLabel)];

    /// <summary>No scheduled tasks label</summary>
    [ResourceEntry("NoScheduledTasksLabel", Description = "No scheduled tasks have been executed yet", LastModified = "2021/01/08", Value = "Currently, there are no scheduled tasks")]
    public string NoScheduledTasksLabel => this[nameof (NoScheduledTasksLabel)];

    /// <summary>Generic error message for failed tasks</summary>
    [ResourceEntry("GenericErrorMessage", Description = "Generic error message for failed tasks", LastModified = "2021/01/08", Value = "Error message not set. Check the error log for more details.")]
    public string GenericErrorMessage => this[nameof (GenericErrorMessage)];

    /// <summary>The title used for the system status widget</summary>
    [ResourceEntry("StatusWidgetTitle", Description = "The title used for the system status widget", LastModified = "2021/01/08", Value = "Scheduled tasks")]
    public string StatusWidgetTitle => this[nameof (StatusWidgetTitle)];

    /// <summary>The description used in the system status widget</summary>
    [ResourceEntry("StatusWidgetDescriptionSingular", Description = "The description used in the system status widget", LastModified = "2021/01/08", Value = "{0} task failed to execute.")]
    public string StatusWidgetDescriptionSingular => this[nameof (StatusWidgetDescriptionSingular)];

    /// <summary>The description used in the system status widget</summary>
    [ResourceEntry("StatusWidgetDescriptionPlural", Description = "The description used in the system status widget", LastModified = "2021/01/08", Value = "{0} tasks failed to execute.")]
    public string StatusWidgetDescriptionPlural => this[nameof (StatusWidgetDescriptionPlural)];

    /// <summary>
    /// The label used for the link in the system status widget
    /// </summary>
    [ResourceEntry("StatusWidgetLinkLabel", Description = "The label used for the link in the system status widget", LastModified = "2021/01/08", Value = "Go to Scheduled tasks")]
    public string StatusWidgetLinkLabel => this[nameof (StatusWidgetLinkLabel)];

    /// <summary>The label used when there are no search results</summary>
    [ResourceEntry("SearchResultsNoRecordsLabel", Description = "The label used when there are no search results", LastModified = "2021/01/08", Value = "No records to display.")]
    public string SearchResultsNoRecordsLabel => this[nameof (SearchResultsNoRecordsLabel)];

    /// <summary>All tasks label.</summary>
    [ResourceEntry("AllTasksLabel", Description = "All tasks label", LastModified = "2021/01/15", Value = "All tasks")]
    public string AllTasksLabel => this[nameof (AllTasksLabel)];

    /// <summary>Pеnding status label.</summary>
    [ResourceEntry("PеndingLabel", Description = "Pеnding label", LastModified = "2021/01/15", Value = "Pending")]
    public string PеndingLabel => this[nameof (PеndingLabel)];

    /// <summary>Started status label.</summary>
    [ResourceEntry("StartedLabel", Description = "Started label", LastModified = "2021/01/15", Value = "Started")]
    public string StartedLabel => this[nameof (StartedLabel)];

    /// <summary>Stopped status label.</summary>
    [ResourceEntry("StoppedLabel", Description = "Stopped label", LastModified = "2021/01/15", Value = "Stopped")]
    public string StoppedLabel => this[nameof (StoppedLabel)];

    /// <summary>Failed status label.</summary>
    [ResourceEntry("FailedLabel", Description = "Failed label", LastModified = "2021/01/15", Value = "Failed")]
    public string FailedLabel => this[nameof (FailedLabel)];

    /// <summary>Recurring status label.</summary>
    [ResourceEntry("RecurringLabel", Description = "Recurring label", LastModified = "2021/01/15", Value = "Recurring")]
    public string RecurringLabel => this[nameof (RecurringLabel)];

    /// <summary>The Filter scheduled tasks label used as filter title</summary>
    [ResourceEntry("FilterScheduledTasksLabel", Description = "The Filter scheduled tasks label used as filter title", LastModified = "2021/01/08", Value = "Filter scheduled tasks")]
    public string FilterScheduledTasksLabel => this[nameof (FilterScheduledTasksLabel)];
  }
}
