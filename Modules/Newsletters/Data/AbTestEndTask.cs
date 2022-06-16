// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.AbTestEndTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  public class AbTestEndTask : ScheduledTask
  {
    private Guid abTestId;
    internal const string taskName = "AbTestEndTask";
    private Guid siteId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.AbTestEndTask" /> class.
    /// </summary>
    public AbTestEndTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.AbTestEndTask" /> class.
    /// </summary>
    /// <param name="abTestId">The ab test id.</param>
    /// <param name="executeTime">The execute time.</param>
    public AbTestEndTask(Guid abTestId, DateTime executeTime)
    {
      this.abTestId = abTestId;
      this.Key = abTestId.ToString();
      this.ExecuteTime = executeTime;
      this.siteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => nameof (AbTestEndTask);

    /// <inheritdoc />
    public override void ExecuteTask()
    {
      using (SiteRegion.FromSiteId(this.siteId))
      {
        NewslettersManager manager = NewslettersManager.GetManager();
        manager.EndTesting(this.abTestId);
        manager.SaveChanges();
      }
    }

    /// <inheritdoc />
    public override string GetCustomData() => !(this.abTestId == Guid.Empty) ? string.Join(";", new string[2]
    {
      this.abTestId.ToString(),
      this.siteId.ToString()
    }) : throw new InvalidOperationException("Cannot schedule an A/B test end task without specifying the A/B test id.");

    /// <inheritdoc />
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      if (strArray.Length == 0)
        return;
      this.abTestId = new Guid(strArray[0]);
      if (strArray.Length <= 1)
        return;
      this.siteId = new Guid(strArray[1]);
    }
  }
}
