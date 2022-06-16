// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingTaskFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Scheduling
{
  public class SchedulingTaskFactory
  {
    public static ScheduledTask ResolveTask(ScheduledTaskData data)
    {
      ScheduledTask scheduledTask;
      switch (data.TaskName)
      {
        case "AbTestEndTask":
          scheduledTask = (ScheduledTask) new AbTestEndTask();
          break;
        case "AbTestSendTask":
          scheduledTask = (ScheduledTask) new AbTestSendTask();
          break;
        case "BounceCheckTask":
          scheduledTask = (ScheduledTask) new BounceCheckTask();
          break;
        case "CampaignDeliveryTask":
          scheduledTask = (ScheduledTask) new CampaignDeliveryTask();
          break;
        case "CollectCampaignStatistics":
          scheduledTask = (ScheduledTask) new CollectIssueStatisticsTask();
          break;
        case "LibraryMoveTask":
          scheduledTask = (ScheduledTask) new LibraryMoveTask();
          break;
        case "LibraryRelocationTask":
          scheduledTask = (ScheduledTask) new LibraryRelocationTask();
          break;
        case "PublishingSystemInvokerTask":
          scheduledTask = (ScheduledTask) new InboundPipeInvokeTask();
          break;
        case "WorkflowCallTask":
          scheduledTask = (ScheduledTask) new WorkflowCallTask();
          break;
        default:
          scheduledTask = Activator.CreateInstance(TypeResolutionService.ResolveType(data.TaskName)) as ScheduledTask;
          break;
      }
      scheduledTask.CopyFromTaskData(data);
      return scheduledTask;
    }
  }
}
