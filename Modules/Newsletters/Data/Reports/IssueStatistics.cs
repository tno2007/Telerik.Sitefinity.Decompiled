// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssueStatistics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// Objects of this class calculate issue statistics. All properties are lazy.
  /// </summary>
  internal class IssueStatistics
  {
    private Guid issueId;
    private NewslettersManager manager;
    private Lazy<int> sentMailsCount;
    private Lazy<int> bouncesCount;
    private Lazy<int> weakBouncesCount;
    private Lazy<int> strongBouncesCount;
    private Lazy<int> weakBouncesRate;
    private Lazy<int> strongBouncesRate;
    private Lazy<int> deliveredMailsCount;
    private Lazy<int> deliveredMailsRate;
    private Lazy<int> uniqueOpeningsCount;
    private Lazy<int> uniqueOpeningsRate;
    private Lazy<int> clickedIssuesCount;
    private Lazy<int> clickThroughRate;
    private Lazy<int> openedInFirstHoursCount;
    private Lazy<int> openedInFirstHoursRate;
    private Lazy<int> unsubscribedCount;
    private Lazy<int> unsubscribedRate;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssueStatistics" /> class.
    /// </summary>
    /// <param name="issueId">The issue id.</param>
    /// <param name="manager">The manager.</param>
    public IssueStatistics(Guid issueId, NewslettersManager manager)
    {
      IssueStatistics issueStatistics = this;
      this.issueId = issueId;
      this.manager = manager;
      // ISSUE: reference to a compiler-generated field
      this.sentMailsCount = new Lazy<int>((Func<int>) (() => manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (d => d.CampaignId == this.issueId)).Count<DeliveryEntry>()));
      // ISSUE: reference to a compiler-generated field
      this.bouncesCount = new Lazy<int>((Func<int>) (() => manager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == this.issueId && (int) b.BounceStatus != 2 && !b.IsProcessing)).GroupBy<BounceStat, Subscriber>((Expression<Func<BounceStat, Subscriber>>) (b => b.Subscriber)).Count<IGrouping<Subscriber, BounceStat>>()));
      // ISSUE: reference to a compiler-generated field
      this.weakBouncesCount = new Lazy<int>((Func<int>) (() => manager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == this.issueId && (int) b.BounceStatus == 0 && !b.IsProcessing)).GroupBy<BounceStat, Subscriber>((Expression<Func<BounceStat, Subscriber>>) (b => b.Subscriber)).Count<IGrouping<Subscriber, BounceStat>>()));
      // ISSUE: reference to a compiler-generated field
      this.strongBouncesCount = new Lazy<int>((Func<int>) (() => manager.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == this.issueId && (int) b.BounceStatus == 1 && !b.IsProcessing)).GroupBy<BounceStat, Subscriber>((Expression<Func<BounceStat, Subscriber>>) (b => b.Subscriber)).Count<IGrouping<Subscriber, BounceStat>>()));
      this.weakBouncesRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.WeakBouncesCount, issueStatistics.SentMailsCount)));
      this.strongBouncesRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.StrongBouncesCount, issueStatistics.SentMailsCount)));
      // ISSUE: reference to a compiler-generated field
      this.deliveredMailsCount = new Lazy<int>((Func<int>) (() => Math.Max(0, issueStatistics.manager.GetDeliveryEntries().Count<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (entry => entry.CampaignId == this.issueId && (int) entry.DeliveryStatus == 0)) - issueStatistics.BouncesCount)));
      this.deliveredMailsRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.DeliveredMailsCount, issueStatistics.SentMailsCount)));
      this.uniqueOpeningsCount = new Lazy<int>((Func<int>) (() => manager.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (d => d.CampaignId == issueStatistics.issueId)).GroupBy<OpenStat, Guid>((Expression<Func<OpenStat, Guid>>) (d => d.SubscriberId)).Count<IGrouping<Guid, OpenStat>>()));
      this.uniqueOpeningsRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.UniqueOpeningsCount, issueStatistics.DeliveredMailsCount)));
      this.clickedIssuesCount = new Lazy<int>((Func<int>) (() => manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == issueStatistics.issueId)).GroupBy<LinkClickStat, Guid>((Expression<Func<LinkClickStat, Guid>>) (cs => cs.SubscriberId)).Count<IGrouping<Guid, LinkClickStat>>()));
      this.clickThroughRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.ClickedIssuesCount, issueStatistics.UniqueOpeningsCount)));
      this.openedInFirstHoursCount = new Lazy<int>((Func<int>) (() => issueStatistics.GetOpenedInFirstHoursCount()));
      this.openedInFirstHoursRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.OpenedInFirstHoursCount, issueStatistics.DeliveredMailsCount)));
      // ISSUE: reference to a compiler-generated field
      this.unsubscribedCount = new Lazy<int>((Func<int>) (() => manager.GetUnsubscriptionInfos().Count<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (u => u.Issue.Id == this.issueId))));
      this.unsubscribedRate = new Lazy<int>((Func<int>) (() => IssueStatistics.SafeRoundendPercentage(issueStatistics.UnsubscribedCount, issueStatistics.DeliveredMailsCount)));
    }

    /// <summary>Gets the sent mails count.</summary>
    public int SentMailsCount => this.sentMailsCount.Value;

    /// <summary>Gets the bounces count.</summary>
    public int BouncesCount => this.bouncesCount.Value;

    /// <summary>Gets the weak bounces count.</summary>
    public int WeakBouncesCount => this.weakBouncesCount.Value;

    /// <summary>Gets the strong bounces count.</summary>
    public int StrongBouncesCount => this.strongBouncesCount.Value;

    /// <summary>Gets the weak bounces rate.</summary>
    public int WeakBouncesRate => this.weakBouncesRate.Value;

    /// <summary>Gets the strong bounces rate.</summary>
    public int StrongBouncesRate => this.strongBouncesRate.Value;

    /// <summary>Gets the delivered mails count.</summary>
    public int DeliveredMailsCount => this.deliveredMailsCount.Value;

    /// <summary>Gets the delivered mails rate.</summary>
    public int DeliveredMailsRate => this.deliveredMailsRate.Value;

    /// <summary>Gets the unique openings count.</summary>
    public int UniqueOpeningsCount => this.uniqueOpeningsCount.Value;

    /// <summary>Gets the unique openings rate.</summary>
    public int UniqueOpeningsRate => this.uniqueOpeningsRate.Value;

    /// <summary>Gets the clicked issues count.</summary>
    public int ClickedIssuesCount => this.clickedIssuesCount.Value;

    /// <summary>Gets the click through rate.</summary>
    public int ClickThroughRate => this.clickThroughRate.Value;

    /// <summary>Gets the opened in first 48 hours count.</summary>
    public int OpenedInFirstHoursCount => this.openedInFirstHoursCount.Value;

    /// <summary>Gets the opened in first 48 hours rate.</summary>
    public int OpenedInFirstHoursRate => this.openedInFirstHoursRate.Value;

    /// <summary>
    /// Gets the count of the subscribers that unsubscribed from this issue.
    /// </summary>
    public int UnsubscribedCount => this.unsubscribedCount.Value;

    /// <summary>Gets the unsubscribed rate.</summary>
    public int UnsubscribedRate => this.unsubscribedRate.Value;

    private static int SafeRoundendPercentage(int part, int whole) => whole != 0 ? (int) Math.Round((double) part / (double) whole * 100.0) : 0;

    private int GetOpenedInFirstHoursCount()
    {
      DateTime? nullable = this.manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (d => d.CampaignId == this.issueId)).OrderByDescending<DeliveryEntry, DateTime>((Expression<Func<DeliveryEntry, DateTime>>) (d => d.DeliveryDate)).Select<DeliveryEntry, DateTime?>((Expression<Func<DeliveryEntry, DateTime?>>) (d => (DateTime?) d.DeliveryDate)).FirstOrDefault<DateTime?>();
      if (!nullable.HasValue)
        return 0;
      DateTime dateTimeFirstHours = nullable.Value.AddHours(48.0);
      return this.manager.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (d => d.CampaignId == this.issueId && d.OpenedDate < dateTimeFirstHours)).GroupBy<OpenStat, Guid>((Expression<Func<OpenStat, Guid>>) (d => d.SubscriberId)).Count<IGrouping<Guid, OpenStat>>();
    }
  }
}
