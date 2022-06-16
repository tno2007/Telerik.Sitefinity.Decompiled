// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.CampaignDeliveryTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  /// <summary>
  /// The scheduled task for delivering a newsletter campaign.
  /// </summary>
  public class CampaignDeliveryTask : ScheduledTask
  {
    private Guid campaignId;
    internal const string taskName = "CampaignDeliveryTask";
    private Guid siteId;

    public CampaignDeliveryTask()
    {
    }

    public CampaignDeliveryTask(Guid campaignId, DateTime executeTime)
    {
      this.campaignId = campaignId;
      this.Key = campaignId.ToString();
      this.ExecuteTime = executeTime;
      this.siteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => nameof (CampaignDeliveryTask);

    public override void ExecuteTask()
    {
      using (SiteRegion.FromSiteId(this.siteId))
      {
        NewslettersManager manager = NewslettersManager.GetManager();
        bool flag = false;
        Guid campaignId = this.campaignId;
        ref bool local = ref flag;
        manager.SendCampaignSynchronized(campaignId, out local);
      }
    }

    public override string GetCustomData() => !(this.campaignId == Guid.Empty) ? string.Join(";", new string[2]
    {
      this.campaignId.ToString(),
      this.siteId.ToString()
    }) : throw new InvalidOperationException("Cannot schedule a campaign delivery task without specifying the campaign id");

    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      if (strArray.Length == 0)
        return;
      this.campaignId = new Guid(strArray[0]);
      if (strArray.Length <= 1)
        return;
      this.siteId = new Guid(strArray[1]);
    }
  }
}
