// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.RetrySendTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  /// <summary>The scheduled task for checking bounced messages</summary>
  internal class RetrySendTask : ScheduledTask
  {
    internal static readonly string Name = typeof (RetrySendTask).FullName;
    private RetrySendTask.RetrySendTaskSettings settings = new RetrySendTask.RetrySendTaskSettings();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.RetrySendTask" /> class.
    /// </summary>
    public RetrySendTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.RetrySendTask" /> class.
    /// </summary>
    /// <param name="executeTime">The execute time.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="retryInfo">The info campaignId/subscriberIds.</param>
    public RetrySendTask(
      DateTime executeTime,
      string providerName,
      Dictionary<Guid, List<Guid>> retryInfo)
    {
      this.settings.ProviderName = providerName;
      this.settings.RetryInfo = retryInfo;
      this.ExecuteTime = executeTime;
    }

    /// <inheritdoc />
    public override string TaskName => RetrySendTask.Name;

    /// <inheritdoc />
    public override void ExecuteTask()
    {
      try
      {
        string rootUrl = NewslettersManager.GetRootUrl();
        NewslettersManager manager = NewslettersManager.GetManager(this.settings.ProviderName);
        int retryCount = Config.Get<NewslettersConfig>().BouncedMessagesRetryCount;
        foreach (Guid key in this.settings.RetryInfo.Keys)
        {
          Guid campaignId = key;
          List<Guid> subscriberIds = this.settings.RetryInfo[campaignId];
          Campaign campaign = manager.GetCampaign(campaignId);
          RawMessageSource rawMessage = new RawMessageSource(campaign.MessageBody, rootUrl);
          IEnumerable<Subscriber> source = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => subscriberIds.Contains(sub.Id) && sub.Lists.Contains(campaign.List) && sub.IsSuspended == false)).AsEnumerable<Subscriber>();
          IEnumerable<BounceStat> bounceStats = manager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == campaign.Id && subscriberIds.Contains(b.Subscriber.Id))).AsEnumerable<BounceStat>();
          IOrderedEnumerable<Subscriber> allSubscribers = source.Where<Subscriber>((Func<Subscriber, bool>) (sub => bounceStats.Where<BounceStat>((Func<BounceStat, bool>) (b => b.Subscriber.Id == sub.Id)).First<BounceStat>().RetryCount < retryCount)).OrderBy<Subscriber, Guid>((Func<Subscriber, Guid>) (sub => sub.Id));
          Dictionary<Guid, DeliveryEntry> dictionary = manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (e => subscriberIds.Contains(e.DeliverySubscriber.Id) && e.CampaignId == campaignId)).GroupBy<DeliveryEntry, Guid>((Expression<Func<DeliveryEntry, Guid>>) (e => e.DeliverySubscriber.Id)).ToDictionary<IGrouping<Guid, DeliveryEntry>, Guid, DeliveryEntry>((Func<IGrouping<Guid, DeliveryEntry>, Guid>) (g => g.Key), (Func<IGrouping<Guid, DeliveryEntry>, DeliveryEntry>) (g => g.First<DeliveryEntry>()));
          foreach (Subscriber subscriber in (IEnumerable<Subscriber>) allSubscribers)
          {
            DeliveryEntry deliveryEntry = dictionary[subscriber.Id];
            deliveryEntry.DeliveryDate = DateTime.UtcNow;
            deliveryEntry.DeliveryStatus = DeliveryStatus.Pending;
          }
          manager.SaveChanges();
          if (allSubscribers.Any<Subscriber>())
          {
            NewslettersManager.SendCampaignViaNotificationsService(rawMessage, campaign, (IEnumerable<Subscriber>) allSubscribers);
            NewslettersManager.SetNotificationsBackgroundStatsCollection(campaign.Id, manager.Provider.Name);
            foreach (BounceStat bounceStat in bounceStats.Where<BounceStat>((Func<BounceStat, bool>) (b => allSubscribers.Any<Subscriber>((Func<Subscriber, bool>) (s => s.Id == b.Subscriber.Id)))))
              bounceStat.IsProcessing = true;
            manager.SaveChanges();
          }
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("Error while retrying to send emails: {0}", (object) ex));
      }
    }

    /// <inheritdoc />
    public override string GetCustomData() => this.settings.ToString();

    /// <inheritdoc />
    public override void SetCustomData(string customData) => this.settings = RetrySendTask.RetrySendTaskSettings.Parse(customData);

    [DataContract]
    private class RetrySendTaskSettings
    {
      [DataMember]
      public string ProviderName { get; set; }

      [DataMember]
      public Dictionary<Guid, List<Guid>> RetryInfo { get; set; }

      public static RetrySendTask.RetrySendTaskSettings Parse(string data) => JsonUtility.FromJson<RetrySendTask.RetrySendTaskSettings>(data);

      public override string ToString() => this.ToJson<RetrySendTask.RetrySendTaskSettings>();
    }
  }
}
