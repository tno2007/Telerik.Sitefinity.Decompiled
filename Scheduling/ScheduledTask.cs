// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.ScheduledTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Scheduling.Model;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// Scheduled task execution implementations should inherit this base class
  /// </summary>
  public abstract class ScheduledTask
  {
    private string key;
    private bool enabled = true;
    private string concurrentTasksKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.ScheduledTask" /> class.
    /// </summary>
    public ScheduledTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.ScheduledTask" /> class.
    /// </summary>
    /// <param name="task">The task.</param>
    public ScheduledTask(ScheduledTaskData task) => this.CopyFromTaskData(task);

    /// <summary>Primary key (database/storage)</summary>
    public Guid Id { get; set; }

    /// <summary>Identifier used for Task Factory</summary>
    public virtual string TaskName => this.GetType().FullName;

    /// <summary>
    /// Custom identifier used for external dependencies (e.g. Id of a news item).
    /// This can be used to quickly find tasks that are dependant on some content object and have to be deleted or rescheduled
    /// </summary>
    public string Key
    {
      get
      {
        if (this.key == null)
          this.key = this.BuildUniqueKey();
        return this.key;
      }
      set => this.key = value;
    }

    /// <summary>
    /// Title that will be displayed in the UI to identify this task
    /// </summary>
    /// <remarks>
    /// This is used only for the UI, but should not be left empty, as it may lead to
    /// "empty" rows in a gird, for example.
    /// </remarks>
    public string Title { get; set; }

    /// <summary>Short text that explains the purpose of this task.</summary>
    /// <remarks>Used in the UI only. Can be safely left blank.</remarks>
    public string Description { get; set; }

    /// <summary>
    /// Records the last time this task was executed, or nothing (null) if it never was.
    /// </summary>
    public DateTime? LastExecutedTime { get; set; }

    /// <summary>
    /// Tells when the task is scheduled for execution in UTC format.
    /// </summary>
    /// <remarks>
    /// Note that for recurring tasks this may be when the next run is scheduled.
    /// </remarks>
    public DateTime ExecuteTime { get; set; }

    /// <summary>
    /// Key that determines the kind of logic that is used to determine when a recurring task should run.
    /// </summary>
    public string ScheduleSpecType { get; set; }

    /// <summary>
    /// Specification of the schedule of a recurring task (impelementation specific for the given <see cref="!:TypeOfSchedule" />.
    /// </summary>
    public string ScheduleSpec { get; set; }

    /// <summary>Indicates whether this task is being processed or not</summary>
    public bool IsRunning { get; set; }

    /// <summary>Indicates whether this task is recurring or not</summary>
    public bool IsRecurring { get; set; }

    /// <summary>Indicates whether this task is system task or not</summary>
    public bool IsSystem { get; set; }

    /// <summary>Indicates whether this task is enabled or not</summary>
    public bool Enabled
    {
      get => this.enabled;
      set => this.enabled = value;
    }

    /// <summary>
    /// Gets or sets the concurrent tasks key. When this key is set, the task will wait for
    /// other tasks with the same key to complete before starting.
    /// </summary>
    /// <value>The concurrent tasks key.</value>
    public string ConcurrentTasksKey
    {
      get => this.concurrentTasksKey;
      set => this.concurrentTasksKey = value;
    }

    /// <summary>
    /// Name of the provider that was used to create the data item
    /// </summary>
    public string ProviderName { get; set; }

    public string TransactionName { get; set; }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public string Language { get; set; }

    /// <summary>Gets or sets the site id.</summary>
    /// <value>The site id.</value>
    public Guid SiteId { get; set; }

    /// <summary>
    /// Gets any custom data that the task needs persisted. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.Scheduling.ScheduledTask.SetCustomData(System.String)" />should have implementation for deserialized the data
    /// </summary>
    /// <returns>string serialized custom task data</returns>
    public virtual string GetCustomData() => string.Empty;

    /// <summary>
    /// Sets the custom data. This is used when reviving a task from a persistent storage to deserialize any custom stored data
    /// </summary>
    public virtual void SetCustomData(string customData)
    {
    }

    /// <summary>
    /// Builds a unique key for the task based on the parameters.
    /// </summary>
    /// <returns></returns>
    public virtual string BuildUniqueKey() => string.Empty;

    /// <summary>Executes the task.</summary>
    public abstract void ExecuteTask();

    /// <summary>Copies to task data.</summary>
    /// <param name="persistedData">The persisted data.</param>
    public virtual void CopyToTaskData(ScheduledTaskData persistedData)
    {
      persistedData.TaskName = this.TaskName;
      persistedData.Key = this.Key;
      persistedData.Title = this.Title;
      persistedData.Description = this.Description;
      persistedData.Language = this.Language;
      persistedData.SiteId = this.SiteId;
      persistedData.Enabled = this.Enabled;
      persistedData.IsSystem = this.IsSystem;
      persistedData.ExecuteTime = this.ExecuteTime;
      persistedData.TypeOfSchedule = this.ScheduleSpecType;
      persistedData.ScheduleData = this.ScheduleSpec;
      persistedData.LastExecutedTime = this.LastExecutedTime;
      persistedData.ConcurrentTasksKey = this.ConcurrentTasksKey;
      persistedData.TaskData = this.GetCustomData();
    }

    /// <summary>Copies from task data.</summary>
    /// <param name="persistedData">The persisted data.</param>
    public virtual void CopyFromTaskData(ScheduledTaskData persistedData)
    {
      this.Id = persistedData.Id;
      this.Key = persistedData.Key;
      this.Title = persistedData.Title;
      this.Description = persistedData.Description;
      this.Language = persistedData.Language;
      this.SiteId = persistedData.SiteId;
      this.Enabled = persistedData.Enabled;
      this.IsSystem = persistedData.IsSystem;
      this.ExecuteTime = persistedData.ExecuteTime;
      this.ScheduleSpecType = persistedData.TypeOfSchedule;
      this.ScheduleSpec = persistedData.ScheduleData;
      this.LastExecutedTime = persistedData.LastExecutedTime;
      this.ConcurrentTasksKey = persistedData.ConcurrentTasksKey;
      this.SetCustomData(persistedData.TaskData);
    }

    /// <summary>
    /// Allows derived classes to tell whether they allow task to act as recurring.
    /// </summary>
    /// <returns>True if rescheduling is allowed, false otherwise.</returns>
    protected internal virtual bool CanReschedule() => true;

    protected virtual void OnProgressChanged(TaskProgressEventArgs eventArgs)
    {
      if (this.ProgressChanged == null)
        return;
      this.ProgressChanged((object) this, eventArgs);
    }

    protected internal virtual void UpdateProgress(int progress, string message = "")
    {
      TaskProgressEventArgs eventArgs = new TaskProgressEventArgs()
      {
        Progress = progress,
        StatusMessage = message
      };
      this.OnProgressChanged(eventArgs);
      if (eventArgs.Stopped)
        throw new TaskStoppedException();
    }

    /// <summary>
    /// Persists the state of the task to the database so it can be resumed in case it is stopped.
    /// </summary>
    internal void PersistState()
    {
      if (!(this.Id != Guid.Empty))
        return;
      SchedulingManager schedulingManager = new SchedulingManager();
      ScheduledTaskData taskData = schedulingManager.GetTaskData(this.Id);
      if (taskData.Status == TaskStatus.Stopped || taskData.ExecuteTime > DateTime.UtcNow)
        return;
      this.CopyToTaskData(taskData);
      schedulingManager.SaveChanges();
    }

    internal event EventHandler<TaskProgressEventArgs> ProgressChanged;
  }
}
