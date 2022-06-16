// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.CreateSiteTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>The scheduled task for creating site</summary>
  internal class CreateSiteTask : ScheduledTask
  {
    internal static readonly string Name = typeof (CreateSiteTask).FullName;

    public override string TaskName => CreateSiteTask.Name;

    public CreateSiteTaskSettings Settings { get; set; }

    public override string GetCustomData() => this.Settings.ToString();

    internal static CreateSiteTaskSettings GetTaskSettings(
      ScheduledTaskData task)
    {
      return CreateSiteTaskSettings.Parse(task.TaskData);
    }

    public override void SetCustomData(string customData) => this.Settings = CreateSiteTaskSettings.Parse(customData);

    public override void ExecuteTask()
    {
      using (new UnrestrictedModeRegion())
      {
        new MultisiteService().SaveSite(Guid.Empty.ToString(), this.Settings.Model, out bool _);
        SystemManager.GetCacheManager(CacheManagerInstance.UserActivities).Remove(this.Settings.CurrentUserId.ToString());
      }
    }
  }
}
