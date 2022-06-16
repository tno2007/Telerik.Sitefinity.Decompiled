// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.BounceChecker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  /// <summary>
  /// This class provides functionality for checking the bounced messages.
  /// </summary>
  public class BounceChecker
  {
    private const int DefaultChunkSize = 500;

    /// <summary>
    /// This method checks for bounced messages through POP client.
    /// </summary>
    /// <param name="newslettersManager">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> to be used to perform data operations.
    /// </param>
    public static void CheckBouncedMessages(NewslettersManager newslettersManager, int chunkSize = 500)
    {
      if (chunkSize <= 0)
        chunkSize = 500;
      IEnumerable<Message> andDeleteMessages = ObjectFactory.Resolve<IMessageReceiver>().GetAndDeleteMessages();
      string format = "Error checking message for bounce: {0}";
      Dictionary<Guid, List<Guid>> retryInfo = new Dictionary<Guid, List<Guid>>();
      int num = 0;
      foreach (Message message in andDeleteMessages)
      {
        try
        {
          if (BounceChecker.CheckBounceMessage(newslettersManager, message) == BounceAction.RetryLater)
          {
            Guid messageCampaignId = MessageParser.GetMessageCampaignId(message.RawMessage);
            Guid messageSubscriberId = MessageParser.GetMessageSubscriberId(message.RawMessage);
            if (!retryInfo.ContainsKey(messageCampaignId))
              retryInfo.Add(messageCampaignId, new List<Guid>());
            retryInfo[messageCampaignId].Add(messageSubscriberId);
          }
          ++num;
          if (num % chunkSize == 0)
          {
            BounceChecker.SaveChanges(newslettersManager);
            BounceChecker.ScheduleRetry(retryInfo, newslettersManager);
          }
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format(format, (object) ex));
        }
      }
      BounceChecker.SaveChanges(newslettersManager);
      BounceChecker.ScheduleRetry(retryInfo, newslettersManager);
    }

    private static void ScheduleRetry(
      Dictionary<Guid, List<Guid>> retryInfo,
      NewslettersManager newslettersManager)
    {
      if (retryInfo.Count <= 0)
        return;
      RetrySendTask task = new RetrySendTask(DateTime.UtcNow.AddMinutes((double) Config.Get<NewslettersConfig>().BouncedMessagesRetryIntervalMinutes), newslettersManager.Provider.Name, retryInfo);
      SchedulingManager manager = SchedulingManager.GetManager();
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
    }

    private static void SaveChanges(NewslettersManager newslettersManager)
    {
      if (newslettersManager.TransactionName == null)
        newslettersManager.SaveChanges();
      else
        TransactionManager.CommitTransaction(newslettersManager.TransactionName);
    }

    internal static BounceAction CheckBounceMessage(
      NewslettersManager newslettersManager,
      Message message)
    {
      BounceStatus messageStatus = MessageParser.GetMessageStatus(message.RawMessage);
      Guid messageCampaignId = MessageParser.GetMessageCampaignId(message.RawMessage);
      Guid messageSubscriberId = MessageParser.GetMessageSubscriberId(message.RawMessage);
      BounceChecker.WriteStatistics(newslettersManager, messageStatus, messageCampaignId, messageSubscriberId);
      return BounceChecker.PerformBounceAction(newslettersManager, messageStatus, messageSubscriberId);
    }

    private static void WriteStatistics(
      NewslettersManager newslettersManager,
      BounceStatus status,
      Guid campaignId,
      Guid subscriberId)
    {
      if (status == BounceStatus.Normal || !(campaignId != Guid.Empty) || !(subscriberId != Guid.Empty))
        return;
      Campaign campaign = (Campaign) null;
      try
      {
        campaign = newslettersManager.GetCampaign(campaignId);
      }
      catch (Exception ex)
      {
      }
      Subscriber subscriber = (Subscriber) null;
      try
      {
        subscriber = newslettersManager.GetSubscriber(subscriberId);
      }
      catch (Exception ex)
      {
      }
      if (campaign == null || subscriber == null)
        return;
      BounceStat bounceStat = newslettersManager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == campaign.Id && b.Subscriber.Id == subscriber.Id)).FirstOrDefault<BounceStat>();
      if (bounceStat == null)
        bounceStat = newslettersManager.CreateBounceStat();
      else
        ++bounceStat.RetryCount;
      bounceStat.Campaign = campaign;
      bounceStat.Subscriber = subscriber;
      bounceStat.SmtpStatus = Enum.GetName(typeof (Telerik.Sitefinity.Newsletters.Model.MessageStatus), (object) status);
      bounceStat.BounceStatus = status;
      bounceStat.IsProcessing = false;
    }

    private static BounceAction PerformBounceAction(
      NewslettersManager newslettersManager,
      BounceStatus status,
      Guid subscriberId)
    {
      BounceAction bounceAction = BounceAction.DoNothing;
      if (subscriberId != Guid.Empty)
      {
        BounceActionResolver bounceActionResolver = ObjectFactory.Resolve<BounceActionResolver>();
        bounceAction = bounceActionResolver.ResolveAction(status);
        bounceActionResolver.PerformAction(newslettersManager, subscriberId, bounceAction);
      }
      return bounceAction;
    }
  }
}
