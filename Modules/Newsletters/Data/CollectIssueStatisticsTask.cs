// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.CollectIssueStatisticsTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  internal class CollectIssueStatisticsTask : ScheduledTask
  {
    private Guid issueId;
    internal const string taskName = "CollectCampaignStatistics";
    private Guid siteId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.CollectIssueStatisticsTask" /> class.
    /// </summary>
    public CollectIssueStatisticsTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.CollectIssueStatisticsTask" /> class.
    /// </summary>
    /// <param name="issueId">The issue id.</param>
    /// <param name="executeTime">The execute time.</param>
    public CollectIssueStatisticsTask(Guid issueId, DateTime executeTime)
    {
      this.issueId = issueId;
      this.Key = issueId.ToString();
      this.ExecuteTime = executeTime;
      this.siteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => "CollectCampaignStatistics";

    /// <inheritdoc />
    public override void ExecuteTask()
    {
      using (SiteRegion.FromSiteId(this.siteId))
        NewslettersManager.CollectStatisticsFromNotificationService(this.issueId, (string) null);
    }

    /// <inheritdoc />
    public override string GetCustomData() => !(this.issueId == Guid.Empty) ? string.Join(";", new string[2]
    {
      this.issueId.ToString(),
      this.siteId.ToString()
    }) : throw new InvalidOperationException("Cannot schedule an issue statistics collection task without specifying the issue id.");

    /// <inheritdoc />
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      if (strArray.Length == 0)
        return;
      this.issueId = new Guid(strArray[0]);
      if (strArray.Length <= 1)
        return;
      this.siteId = new Guid(strArray[1]);
    }
  }
}
