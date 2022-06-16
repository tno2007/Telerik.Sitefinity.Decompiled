// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.ProcessingTaskInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Scheduling.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// Represents view model information for long running tasks on system objects - progress report, failure, available commands
  /// </summary>
  [DataContract]
  public class ProcessingTaskInfo
  {
    public ProcessingTaskInfo()
    {
    }

    public ProcessingTaskInfo(ScheduledTaskData task)
    {
      this.TaskType = LibraryTasks.None;
      if (task.TaskName == "LibraryRelocationTask")
      {
        this.Description = Res.Get<LibrariesResources>(task.Title);
        this.TaskType = LibraryTasks.Relocate;
      }
      else if (task.TaskName == typeof (LibraryThumbnailsRegenerateTask).FullName)
      {
        this.Description = Res.Get<LibrariesResources>().RegeneratingThumbnails;
        this.TaskType = LibraryTasks.Regenerate;
      }
      this.TaskName = task.Title;
      this.Status = task.Status.ToString();
      this.ProgressStatus = task.Progress;
      if (task.Status == TaskStatus.Failed)
        this.FailureReport = task.StatusMessage;
      if (task.Status == TaskStatus.Started)
      {
        this.AvailableCommand = "stop-task";
        this.AvailableCommandTitle = "Stop";
      }
      else
      {
        this.AvailableCommand = "restart-task";
        this.AvailableCommandTitle = "Resume";
      }
      this.TaskId = task.Id.ToString();
    }

    [DataMember]
    public LibraryTasks TaskType { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string TaskName { get; set; }

    [DataMember]
    public string Status { get; set; }

    [DataMember]
    public string AvailableCommand { get; set; }

    [DataMember]
    public string AvailableCommandTitle { get; set; }

    [DataMember]
    public string FailureReport { get; set; }

    [DataMember]
    public string ProgressReport { get; set; }

    [DataMember]
    public int ProgressStatus { get; set; }

    [DataMember]
    public string TaskId { get; set; }
  }
}
