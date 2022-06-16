// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.WcfScheduledTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Scheduling.Model;

namespace Telerik.Sitefinity.Scheduling.Web.Services
{
  [DataContract]
  public class WcfScheduledTask
  {
    public WcfScheduledTask()
    {
    }

    public WcfScheduledTask(ScheduledTaskData data)
    {
      this.Id = data.Id;
      this.Description = data.Description;
      this.TaskName = data.TaskName;
      this.Status = data.Status;
      this.StatusMessage = data.StatusMessage;
      this.ProgressStatus = data.Progress;
      this.TaskData = data.TaskData;
      this.Title = data.Title;
      this.IsManageable = false;
      this.ConcurrentTaskKey = data.ConcurrentTasksKey;
    }

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string TaskName { get; set; }

    [DataMember]
    public TaskStatus Status { get; set; }

    [DataMember]
    public string StatusMessage { get; set; }

    [DataMember]
    public int ProgressStatus { get; set; }

    [DataMember]
    public string TaskData { get; set; }

    [DataMember]
    public string ConcurrentTaskKey { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public bool IsManageable { get; set; }
  }
}
