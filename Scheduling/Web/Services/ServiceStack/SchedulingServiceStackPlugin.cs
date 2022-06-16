// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.SchedulingServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.Dto;

namespace Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack
{
  internal class SchedulingServiceStackPlugin : IPlugin
  {
    internal const string ServiceBaseUrl = "/sitefinity/scheduling";
    internal static readonly string GetScheduledTasks = "/sitefinity/scheduling" + "/scheduled-tasks";
    internal static readonly string GetScheduledTasksStatusFilter = "/sitefinity/scheduling" + "/scheduled-task-statuses";

    /// <summary>Adding the service routes</summary>
    /// <param name="appHost">The service stack appHost</param>
    public void Register(IAppHost appHost)
    {
      if (appHost == null)
        throw new ArgumentNullException(nameof (appHost));
      appHost.RegisterService(typeof (SchedulingService), "/sitefinity/scheduling");
      appHost.Routes.Add<Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.Dto.GetScheduledTasks>(SchedulingServiceStackPlugin.GetScheduledTasks, "GET").Add<GetFilterByRequest>(SchedulingServiceStackPlugin.GetScheduledTasksStatusFilter, "GET").Add<ManageScheduleTask>(SchedulingServiceStackPlugin.GetScheduledTasks, "PUT");
    }
  }
}
