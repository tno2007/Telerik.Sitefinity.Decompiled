// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssueReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>The view for exploring the report for a given issue.</summary>
  internal class IssueReportView : ViewModeControl<NoSidebarNewslettersControlPanel>
  {
    private Guid issueId;
    private Guid campaignId;
    private string subscribersReportUrl;
    private static readonly string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.IssueReportView.ascx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssueReportView" /> class.
    /// </summary>
    public IssueReportView() => this.LayoutTemplatePath = ControlUtilities.ToVppPath(IssueReportView.layoutTemplatePath);

    /// <summary>Gets the campaign id.</summary>
    protected Guid CampaignId
    {
      get
      {
        if (this.campaignId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length == 0)
            throw new InvalidOperationException("It is unclear for which campaign report ought to be generated. The id of the campaign is missing from the url.");
          this.campaignId = urlParameters[0].IsGuid() ? new Guid(urlParameters[0]) : throw new ArgumentException("The url parameter carrying information about the campaign id is not a valid GUID.");
        }
        return this.campaignId;
      }
    }

    /// <summary>Gets the issue id.</summary>
    protected Guid IssueId
    {
      get
      {
        if (this.issueId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length < 2)
            throw new InvalidOperationException("It is unclear for which issue report ought to be generated. The id of the issue is missing from the url.");
          this.issueId = urlParameters[1].IsGuid() ? new Guid(urlParameters[1]) : throw new ArgumentException("The url parameter carrying information about the issue id is not a valid GUID.");
        }
        return this.issueId;
      }
    }

    /// <summary>
    /// Gets or sets the url of the page with subscribers report.
    /// </summary>
    public string SubscribersReportUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.subscribersReportUrl))
          this.subscribersReportUrl = this.Host.GetPageUrl(NewslettersModule.subscribersReportPageId) + "/" + (object) this.CampaignId + "/" + (object) this.IssueId;
        return this.subscribersReportUrl;
      }
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets a reference to the statistics title label.</summary>
    protected virtual Label StatisticsTitle => this.Container.GetControl<Label>("statisticsTitle", true);

    /// <summary>
    /// Gets a reference to the mailing list statistics label.
    /// </summary>
    protected virtual Label MailingListStatisticsLabel => this.Container.GetControl<Label>("mailingListStatisticsLabel", true);

    /// <summary>Gets a reference to the bounce statistics label.</summary>
    protected virtual Label BounceStatisticsLabel => this.Container.GetControl<Label>("bounceStatisticsLabel", true);

    /// <summary>
    /// Gets a reference to the label for delivered mails count.
    /// </summary>
    protected virtual Label DeliveredLabel => this.Container.GetControl<Label>("deliveredLabel", true);

    /// <summary>Gets a reference to the delivery statistics label.</summary>
    protected virtual Label DeliveryStatisticsLabel => this.Container.GetControl<Label>("deliveryStatisticsLabel", true);

    /// <summary>Gets a reference to the unique openings label.</summary>
    protected virtual Label UniqueOpeningsLabel => this.Container.GetControl<Label>("uniqueOpeningsLabel", true);

    /// <summary>
    /// Gets a reference to the unique openings statistics label.
    /// </summary>
    protected virtual Label UniqueOpeningsStatisticsLabel => this.Container.GetControl<Label>("uniqueOpeningsStatisticsLabel", true);

    /// <summary>Gets a reference to the unique clicks label.</summary>
    protected virtual Label UniqueClicksLabel => this.Container.GetControl<Label>("uniqueClicksLabel", true);

    /// <summary>Gets a reference to the unique clicks label.</summary>
    protected virtual Label UniqueClicksStatisticsLabel => this.Container.GetControl<Label>("uniqueClicksStatisticsLabel", true);

    /// <summary>Gets a reference to the unsubscribed label.</summary>
    protected virtual Label UnsubscribedLabel => this.Container.GetControl<Label>("unsubscribedLabel", true);

    /// <summary>
    /// Gets a reference to the unsubscribed statistics label.
    /// </summary>
    protected virtual Label UnsubscribedStatisticsLabel => this.Container.GetControl<Label>("unsubscribedStatisticsLabel", true);

    /// <summary>
    /// Gets a reference to the opened in first 48 hours label.
    /// </summary>
    protected virtual Label OpenedInFirstHoursLabel => this.Container.GetControl<Label>("openedInFirstHoursLabel", true);

    /// <summary>
    /// Gets a reference to the opened in the first 48 hours statistics label.
    /// </summary>
    protected virtual Label OpenedInFirstHoursStatisticsLabel => this.Container.GetControl<Label>("openedInFirstHoursStatisticsLabel", true);

    /// <summary>Gets a reference to the clicked links grid.</summary>
    protected virtual ClickedLinksGrid ClickedLinksStatGrid => this.Container.GetControl<ClickedLinksGrid>("clickedLinksStatGrid", true);

    /// <summary>Gets a reference to the clicks by hour chart.</summary>
    protected virtual ClicksByHourChart ClicksByHourChart => this.Container.GetControl<ClicksByHourChart>("clicksByHourChart", true);

    /// <summary>
    /// Gets a reference to the subscribers link click stat grid.
    /// </summary>
    protected virtual SubscribersLinkClickStatGrid SubscribersLinkClickStatGrid => this.Container.GetControl<SubscribersLinkClickStatGrid>("subscribersLinkClickStatGrid", true);

    /// <summary>Gets a reference to view all subscribers link.</summary>
    protected virtual HtmlAnchor ViewAllSubscribersLink => this.Container.GetControl<HtmlAnchor>("viewAllSubscribersLink", true);

    /// <summary>Gets a reference to the A/B Test.</summary>
    protected virtual ABTestPerIssue ABTestPerIssue => this.Container.GetControl<ABTestPerIssue>("abTestPerIssue", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      string str = SystemManager.CurrentHttpContext.Request.ParamsGet("providerName");
      if (str != null)
        this.ProviderName = str;
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      Campaign issue = manager.GetIssue(this.IssueId);
      if (issue != null)
      {
        this.Host.Title = HttpUtility.HtmlEncode(issue.Name.Length < 29 ? issue.Name : issue.Name.Substring(0, 26) + "...");
        this.InitializeStatistics(issue, manager);
        this.ClickedLinksStatGrid.IssueId = this.IssueId;
        this.ClickedLinksStatGrid.ProviderName = this.ProviderName;
        this.ClicksByHourChart.IssueId = this.IssueId;
        this.ClicksByHourChart.ProviderName = this.ProviderName;
        this.SubscribersLinkClickStatGrid.IssueId = this.IssueId;
        this.SubscribersLinkClickStatGrid.ProviderName = this.ProviderName;
        this.ABTestPerIssue.RootCampaignId = this.CampaignId;
        this.ABTestPerIssue.IssueId = this.IssueId;
        this.ABTestPerIssue.ProviderName = this.ProviderName;
      }
      this.Host.IsBackLinkVisible = true;
      if (this.GetUrlParameterString(false).Contains("from/abCampaignReport"))
      {
        this.Host.BackLinkText = Res.Get<NewslettersResources>().BackToABCampaignReport;
        this.Host.BackLinkNavigateUrl = this.Page.Request.UrlReferrer.AbsolutePath;
      }
      else
      {
        Campaign campaign = manager.GetCampaign(this.CampaignId);
        if (campaign != null)
        {
          string s = campaign.Name.Length < 29 ? campaign.Name : campaign.Name.Substring(0, 26) + "...";
          this.Host.BackLinkText = string.Format(Res.Get<Labels>().BackTo, (object) HttpUtility.HtmlEncode(s));
          this.Host.BackLinkNavigateUrl = this.Host.GetPageUrl(NewslettersModule.campaignOverviewPageId) + "/" + (object) this.CampaignId;
        }
      }
      this.ClickedLinksStatGrid.SubscribersReportUrl = this.SubscribersReportUrl;
      this.ViewAllSubscribersLink.HRef = this.SubscribersReportUrl;
    }

    private void InitializeStatistics(Campaign issue, NewslettersManager manager)
    {
      IssueStatistics issueStatistics1 = new IssueStatistics(this.IssueId, manager);
      IssueStatistics issueStatistics2 = manager.GetIssues(issue.RootCampaign).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => i.DeliveryDate < issue.DeliveryDate && (int) i.CampaignState == 4)).OrderByDescending<Campaign, DateTime>((Expression<Func<Campaign, DateTime>>) (i => i.DeliveryDate)).Select<Campaign, IssueStatistics>((Expression<Func<Campaign, IssueStatistics>>) (i => new IssueStatistics(i.Id, manager))).FirstOrDefault<IssueStatistics>();
      this.StatisticsTitle.Text = Res.Get<NewslettersResources>().StatisticsFor.Arrange((object) HttpUtility.HtmlEncode(issue.Name));
      string s = issue.List != null ? issue.List.Title.Value : string.Empty;
      this.MailingListStatisticsLabel.Text = Res.Get<NewslettersResources>().MailingListStatistics.Arrange((object) issueStatistics1.SentMailsCount, (object) HttpUtility.HtmlEncode(s));
      this.BounceStatisticsLabel.Text = Res.Get<NewslettersResources>().BounceStatistics.Arrange((object) issueStatistics1.WeakBouncesCount, (object) issueStatistics1.WeakBouncesRate, (object) issueStatistics1.StrongBouncesCount, (object) issueStatistics1.StrongBouncesRate);
      this.DeliveredLabel.Text = issueStatistics1.DeliveredMailsCount.ToString();
      if (issueStatistics2 != null)
        this.DeliveryStatisticsLabel.Text = Res.Get<NewslettersResources>().DeliveryStatisticsComparison.Arrange((object) issueStatistics1.DeliveredMailsRate, (object) issueStatistics2.DeliveredMailsRate);
      else
        this.DeliveryStatisticsLabel.Text = Res.Get<NewslettersResources>().DeliveryStatistics.Arrange((object) issueStatistics1.DeliveredMailsRate);
      this.UniqueOpeningsLabel.Text = issueStatistics1.UniqueOpeningsCount.ToString();
      if (issueStatistics2 != null)
        this.UniqueOpeningsStatisticsLabel.Text = Res.Get<NewslettersResources>().UniqueOpeningsStatisticsComparison.Arrange((object) issueStatistics1.UniqueOpeningsRate, (object) issueStatistics2.UniqueOpeningsRate);
      else
        this.UniqueOpeningsStatisticsLabel.Text = Res.Get<NewslettersResources>().UniqueOpeningsStatistics.Arrange((object) issueStatistics1.UniqueOpeningsRate);
      this.UniqueClicksLabel.Text = issueStatistics1.ClickedIssuesCount.ToString();
      if (issueStatistics2 != null)
        this.UniqueClicksStatisticsLabel.Text = Res.Get<NewslettersResources>().UniqueClicksStatisticsComparison.Arrange((object) issueStatistics1.ClickThroughRate, (object) issueStatistics2.ClickThroughRate);
      else
        this.UniqueClicksStatisticsLabel.Text = Res.Get<NewslettersResources>().UniqueClicksStatistics.Arrange((object) issueStatistics1.ClickThroughRate);
      this.UnsubscribedLabel.Text = issueStatistics1.UnsubscribedCount.ToString();
      if (issueStatistics2 != null)
        this.UnsubscribedStatisticsLabel.Text = Res.Get<NewslettersResources>().UnsubscribeStatisticsComparison.Arrange((object) issueStatistics1.UnsubscribedRate, (object) issueStatistics2.UnsubscribedRate);
      else
        this.UnsubscribedStatisticsLabel.Text = Res.Get<NewslettersResources>().UnsubscribeStatistics.Arrange((object) issueStatistics1.UnsubscribedRate);
      this.OpenedInFirstHoursLabel.Text = issueStatistics1.OpenedInFirstHoursCount.ToString();
      if (issueStatistics2 != null)
        this.OpenedInFirstHoursStatisticsLabel.Text = Res.Get<NewslettersResources>().OpenedInFirstHoursStatisticsComparison.Arrange((object) issueStatistics1.OpenedInFirstHoursRate, (object) issueStatistics2.OpenedInFirstHoursRate);
      else
        this.OpenedInFirstHoursStatisticsLabel.Text = Res.Get<NewslettersResources>().OpenedInFirstHoursStatistics.Arrange((object) issueStatistics1.OpenedInFirstHoursRate);
    }
  }
}
