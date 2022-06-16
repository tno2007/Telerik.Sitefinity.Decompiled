// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.Reports.CampaignReport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Data.Reports
{
  /// <summary>The class that the campaign reports are bound to.</summary>
  [Obsolete("Use IssueStatistics instead.")]
  public class CampaignReport : NewslettersReportBase
  {
    private string campaignName;
    private int totalRecipients;
    private int? successfulRecipients;
    private int? recipientsWhoOpened;
    private int totalTimesOpened;
    private DateTime? lastOpenedDate;
    private int? recipientsWhoClicked;
    private int totalClicks;
    private DateTime? lastClick;
    private int totalUnsubscriptions;
    private int? totalUnopened;
    private int? totalBounced;
    private double? clickRate;
    private Guid campaignId;
    private Campaign campaign;
    private IList<ClickedLinkCampaignRecord> clickedLinks;

    public CampaignReport(string providerName, Guid campaignId)
      : base(providerName)
    {
      this.campaignId = campaignId;
    }

    /// <summary>
    /// Gets the name of the campaign for which the report is being generated.
    /// </summary>
    public string CampaignName
    {
      get
      {
        if (string.IsNullOrEmpty(this.campaignName))
          this.campaignName = this.Campaign.Name;
        return this.campaignName;
      }
    }

    /// <summary>Gets the total number of recipients of the campaign.</summary>
    public int TotalRecipients
    {
      get
      {
        if (this.totalRecipients == 0)
          this.totalRecipients = this.Manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (d => d.CampaignId == this.Campaign.Id)).GroupBy<DeliveryEntry, Subscriber>((Expression<Func<DeliveryEntry, Subscriber>>) (d => d.DeliverySubscriber)).Count<IGrouping<Subscriber, DeliveryEntry>>();
        return this.totalRecipients;
      }
    }

    /// <summary>
    /// Gets or sets the percentage of the subscribers that opened an email and clicked on the links.
    /// </summary>
    public double ClickRate
    {
      get
      {
        if (!this.clickRate.HasValue)
          this.clickRate = new double?((double) this.RecipientsWhoClicked / (double) this.RecipientsWhoOpened);
        if (double.IsNaN(this.clickRate.Value))
          this.clickRate = new double?(0.0);
        return this.clickRate.Value;
      }
    }

    /// <summary>
    /// Gets or sets the total number of emails that have bounced.
    /// </summary>
    public int TotalBounced
    {
      get
      {
        if (!this.totalBounced.HasValue)
          this.totalBounced = new int?(this.Manager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (bs => bs.Campaign.Id == this.campaignId)).Count<BounceStat>());
        return this.totalBounced.Value;
      }
    }

    /// <summary>
    /// Gets or sets the total number of sent emails that have not been opened.
    /// </summary>
    public int TotalUnopened
    {
      get
      {
        if (!this.totalUnopened.HasValue)
          this.totalUnopened = new int?(this.TotalRecipients - this.RecipientsWhoOpened);
        return this.totalUnopened.Value;
      }
    }

    /// <summary>
    /// Gets or sets the total number of subscribers that have unsubscribed.
    /// </summary>
    public int TotalUnsubscriptions => this.Campaign.List != null ? this.Campaign.List.TotalUnsubscriptions : 0;

    /// <summary>
    /// Gets or sets the date and time of the last link click.
    /// </summary>
    public DateTime? LastClick
    {
      get
      {
        if (!this.lastClick.HasValue)
        {
          LinkClickStat linkClickStat = this.Manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == this.Campaign.Id)).OrderByDescending<LinkClickStat, DateTime>((Expression<Func<LinkClickStat, DateTime>>) (cs => cs.DateTimeClicked)).FirstOrDefault<LinkClickStat>();
          if (linkClickStat != null)
            this.lastClick = new DateTime?(linkClickStat.DateTimeClicked);
        }
        return this.lastClick;
      }
    }

    /// <summary>Gets or sets the total number of link clicks.</summary>
    public int TotalClicks
    {
      get
      {
        if (this.totalClicks == 0)
          this.totalClicks = this.Manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == this.campaignId)).Count<LinkClickStat>();
        return this.totalClicks;
      }
    }

    /// <summary>
    /// Gets the unique number of recipients that clicked on the links in campaign.
    /// </summary>
    /// <remarks>
    /// If the same subscriber clicks twice, it will be counted only once.
    /// </remarks>
    public int RecipientsWhoClicked
    {
      get
      {
        if (!this.recipientsWhoClicked.HasValue)
          this.recipientsWhoClicked = new int?(this.Manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == this.campaignId)).GroupBy<LinkClickStat, Guid>((Expression<Func<LinkClickStat, Guid>>) (cs => cs.SubscriberId)).Count<IGrouping<Guid, LinkClickStat>>());
        return this.recipientsWhoClicked.Value;
      }
    }

    /// <summary>
    /// Gets or sets the date and time of the last email that has been opened.
    /// </summary>
    public DateTime? LastOpenedDate
    {
      get
      {
        if (!this.lastOpenedDate.HasValue)
        {
          OpenStat openStat = this.Manager.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (cs => cs.CampaignId == this.Campaign.Id)).OrderByDescending<OpenStat, DateTime>((Expression<Func<OpenStat, DateTime>>) (cs => cs.OpenedDate)).FirstOrDefault<OpenStat>();
          if (openStat != null)
            this.lastOpenedDate = new DateTime?(openStat.OpenedDate);
        }
        return this.lastOpenedDate;
      }
    }

    /// <summary>
    /// Gets or sets the total number of times messages have been opened.
    /// </summary>
    public int TotalTimesOpened
    {
      get
      {
        if (this.totalTimesOpened == 0)
        {
          Guid campaignId = this.Campaign.Id;
          this.totalTimesOpened = this.Manager.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (os => os.CampaignId == campaignId)).Count<OpenStat>();
        }
        return this.totalTimesOpened;
      }
    }

    /// <summary>Gets the number of recipients that opened the email.</summary>
    /// <remarks>
    /// If the same subscriber opens email twice; it will be counted only once.
    /// </remarks>
    public int RecipientsWhoOpened
    {
      get
      {
        if (!this.recipientsWhoOpened.HasValue)
          this.recipientsWhoOpened = new int?(this.Manager.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (cs => cs.CampaignId == this.campaignId)).GroupBy<OpenStat, Guid>((Expression<Func<OpenStat, Guid>>) (cs => cs.SubscriberId)).Count<IGrouping<Guid, OpenStat>>());
        return this.recipientsWhoOpened.Value;
      }
    }

    /// <summary>
    /// Gets the total number of emails that have been delivered successfully.
    /// </summary>
    [Obsolete("Use SuccessfulDeliveries property instead.")]
    public int SuccessfulRecipients => this.SuccessfulDeliveries;

    /// <summary>
    /// Gets the total number of emails that have been delivered successfully.
    /// </summary>
    public int SuccessfulDeliveries
    {
      get
      {
        if (!this.successfulRecipients.HasValue)
          this.successfulRecipients = new int?(this.Manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (d => d.CampaignId == this.Campaign.Id && (int) d.DeliveryStatus == 0)).Count<DeliveryEntry>() - this.TotalBounced);
        return this.successfulRecipients.Value;
      }
    }

    /// <summary>
    /// Gets a collection of all the links that have been clicked.
    /// </summary>
    public IList<ClickedLinkCampaignRecord> ClickedLinks
    {
      get
      {
        if (this.clickedLinks == null)
          this.clickedLinks = (IList<ClickedLinkCampaignRecord>) this.GetClickedLinks().AsQueryable<ClickedLinkCampaignRecord>().OrderByDescending<ClickedLinkCampaignRecord, int>((Expression<Func<ClickedLinkCampaignRecord, int>>) (c => c.ClicksCount)).ToList<ClickedLinkCampaignRecord>();
        return this.clickedLinks;
      }
    }

    /// <summary>
    /// Gets the campaign object for which the report is being generated.
    /// </summary>
    protected Campaign Campaign
    {
      get
      {
        if (this.campaign == null)
          this.campaign = this.Manager.GetCampaign(this.campaignId);
        return this.campaign;
      }
    }

    private IList<ClickedLinkCampaignRecord> GetClickedLinks()
    {
      List<ClickedLinkCampaignRecord> clickedLinks = new List<ClickedLinkCampaignRecord>();
      List<\u003C\u003Ef__AnonymousType55<string, int>> list = this.Manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == this.Campaign.Id)).GroupBy<LinkClickStat, string>((Expression<Func<LinkClickStat, string>>) (cs => cs.Url)).Select(lnk => new
      {
        Url = lnk.Key,
        ClicksCount = lnk.Count<LinkClickStat>()
      }).ToList();
      for (int index = 0; index < list.Count; ++index)
      {
        ClickedLinkCampaignRecord linkCampaignRecord = new ClickedLinkCampaignRecord()
        {
          Url = list[index].Url,
          ClicksCount = list[index].ClicksCount
        };
        clickedLinks.Add(linkCampaignRecord);
      }
      return (IList<ClickedLinkCampaignRecord>) clickedLinks;
    }
  }
}
