// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.ABCampaignReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Data.Reports;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// This page displays the report for the A/B campaign testing.
  /// </summary>
  [Obsolete("Use AbTestReportView instead.")]
  public class ABCampaignReportView : ViewModeControl<NewslettersControlPanel>
  {
    private Guid abCampaignId;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.ABCampaignReportView.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/ABCampaign.svc";

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ABCampaignReportView.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    protected virtual Guid ABCampaignId
    {
      get
      {
        if (this.abCampaignId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length == 0)
            throw new InvalidOperationException("It is unclear for which A/B campaign report ought to be generated. The id of the A/B campaign is missing from the url.");
          this.abCampaignId = urlParameters[0].IsGuid() ? new Guid(urlParameters[0]) : throw new ArgumentException("The url parameter carrying information about the A/B campaign id is not a valid GUID.");
        }
        return this.abCampaignId;
      }
    }

    protected virtual ITextControl MailingList => this.Container.GetControl<ITextControl>("mailingList", true);

    protected virtual ITextControl MailingListSubscriberCount => this.Container.GetControl<ITextControl>("mailingListSubscriberCount", true);

    protected virtual ITextControl TestSample => this.Container.GetControl<ITextControl>("testSample", true);

    protected virtual ITextControl TestSampleCount => this.Container.GetControl<ITextControl>("testSampleCount", true);

    protected virtual ITextControl WinningCondition => this.Container.GetControl<ITextControl>("winningCondition", true);

    protected virtual ITextControl TestingPeriodEndsOn => this.Container.GetControl<ITextControl>("testingPeriodEndsOn", true);

    protected virtual ITextControl TestingStatus => this.Container.GetControl<ITextControl>("testingStatus", true);

    protected virtual ITextControl CampaignAName => this.Container.GetControl<ITextControl>("campaignAName", true);

    protected virtual HyperLink CampaignAFullReport => this.Container.GetControl<HyperLink>("campaignAFullReport", true);

    protected virtual ITextControl CampaignBName => this.Container.GetControl<ITextControl>("campaignBName", true);

    protected virtual HyperLink CampaignBFullReport => this.Container.GetControl<HyperLink>("campaignBFullReport", true);

    protected virtual ITextControl SoFarSentMessagesCount => this.Container.GetControl<ITextControl>("soFarSentMessagesCount", true);

    protected virtual ITextControl SoFarSentMessagesPercentage => this.Container.GetControl<ITextControl>("soFarSentMessagesPercentage", true);

    protected virtual ITextControl CampaignAType => this.Container.GetControl<ITextControl>("campaignAType", true);

    protected virtual ITextControl CampaignBType => this.Container.GetControl<ITextControl>("campaignBType", true);

    protected virtual ITextControl CampaignAOpenedMessages => this.Container.GetControl<ITextControl>("campaignAOpenedMessages", true);

    protected virtual ITextControl CampaignBOpenedMessages => this.Container.GetControl<ITextControl>("campaignBOpenedMessages", true);

    /// <summary>Gets the make campaign A winner button.</summary>
    protected virtual LinkButton MakeCampaignAWinnerButton => this.Container.GetControl<LinkButton>("makeCampaignAWinnerButton", true);

    /// <summary>Gets the make campaign B winner button.</summary>
    protected virtual LinkButton MakeCampaignBWinnerButton => this.Container.GetControl<LinkButton>("makeCampaignBWinnerButton", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the web service url.
    /// </summary>
    protected virtual HiddenField WebServiceUrlHidden => this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the id of the A/B campaign.
    /// </summary>
    protected virtual HiddenField ABCampaignIdHidden => this.Container.GetControl<HiddenField>("abCampaignIdHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the id of Campaign A.
    /// </summary>
    protected virtual HiddenField CampaignAIdHidden => this.Container.GetControl<HiddenField>("campaignAIdHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the id of Campaign B.
    /// </summary>
    protected virtual HiddenField CampaignBIdHidden => this.Container.GetControl<HiddenField>("campaignBIdHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the name of Campaign A.
    /// </summary>
    protected virtual HiddenField CampaignANameHidden => this.Container.GetControl<HiddenField>("campaignANameHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the name of Campaign B.
    /// </summary>
    protected virtual HiddenField CampaignBNameHidden => this.Container.GetControl<HiddenField>("campaignBNameHidden", true);

    protected virtual HiddenField ABCampaignWinningConditionHidden => this.Container.GetControl<HiddenField>("abCampaignWinningConditionHidden", true);

    protected virtual Label CampaignAClickedLinks => this.Container.GetControl<Label>("campaignAClickedLinks", true);

    protected virtual Label CampaignBClickedLinks => this.Container.GetControl<Label>("campaignBClickedLinks", true);

    protected virtual HyperLink CampaignAClickedLinksFullReport => this.Container.GetControl<HyperLink>("campaignAClickedLinksFullReport", true);

    protected virtual HyperLink CampaignBClickedLinksFullReport => this.Container.GetControl<HyperLink>("campaignBClickedLinksFullReport", true);

    protected override void InitializeControls(Control viewContainer)
    {
      ABCampaignReport abCampaignReport = new ABCampaignReport(string.Empty, this.ABCampaignId);
      this.MailingList.Text = abCampaignReport.MailingList == null ? Res.Get<NewslettersResources>().CampaignStateMissingMailingList : (string) abCampaignReport.MailingList.Title;
      this.Host.Title = string.Format(Res.Get<NewslettersResources>().ABTestReport, (object) abCampaignReport.Name);
      ITextControl listSubscriberCount = this.MailingListSubscriberCount;
      int num = abCampaignReport.MailingListSubscriberCount;
      string str1 = string.Format("({0} {1})", (object) num.ToString(), (object) Res.Get<NewslettersResources>().Subscribers);
      listSubscriberCount.Text = str1;
      this.TestSample.Text = abCampaignReport.TestSample.ToString("P");
      this.TestSampleCount.Text = string.Format("({0} {1})", (object) abCampaignReport.TestSampleCount, (object) Res.Get<NewslettersResources>().Subscribers);
      switch (abCampaignReport.WinningCondition)
      {
        case CampaignWinningCondition.MoreOpenedEmails:
          this.WinningCondition.Text = Res.Get<NewslettersResources>().MoreOpenedEmails;
          break;
        case CampaignWinningCondition.MoreLinkClicks:
          this.WinningCondition.Text = Res.Get<NewslettersResources>().MoreLinkClicks;
          break;
        case CampaignWinningCondition.LessBounces:
          this.WinningCondition.Text = Res.Get<NewslettersResources>().LessBounces;
          break;
        case CampaignWinningCondition.ManualDecision:
          this.WinningCondition.Text = Res.Get<NewslettersResources>().ManualWinningDecision;
          break;
      }
      this.TestingPeriodEndsOn.Text = abCampaignReport.EndTestingOn.ToSitefinityUITime().ToString("MM/dd/yyyy hh:mm tt");
      switch (abCampaignReport.TestingStatus)
      {
        case ABTestingStatus.Scheduled:
          this.TestingStatus.Text = Res.Get<NewslettersResources>().Scheduled;
          break;
        case ABTestingStatus.Stopped:
          this.TestingStatus.Text = Res.Get<NewslettersResources>().Stopped;
          break;
        case ABTestingStatus.InProgress:
          this.TestingStatus.Text = Res.Get<NewslettersResources>().InProgress;
          break;
        case ABTestingStatus.Done:
          this.TestingStatus.Text = Res.Get<NewslettersResources>().Done;
          break;
      }
      this.CampaignAName.Text = abCampaignReport.CampaignA.Name;
      this.CampaignBName.Text = abCampaignReport.CampaignB.Name;
      this.CampaignAFullReport.NavigateUrl = this.GetCampaignFullReportUrl(abCampaignReport.CampaignA.RootCampaign.Id, abCampaignReport.CampaignA.Id);
      this.CampaignBFullReport.NavigateUrl = this.GetCampaignFullReportUrl(abCampaignReport.CampaignB.RootCampaign.Id, abCampaignReport.CampaignB.Id);
      ITextControl sentMessagesCount = this.SoFarSentMessagesCount;
      num = abCampaignReport.SoFarSentMessages;
      string str2 = num.ToString();
      sentMessagesCount.Text = str2;
      this.SoFarSentMessagesPercentage.Text = string.Format("({0} {1})", (object) abCampaignReport.SoFarSentMessagesPercentage.ToString("P"), (object) Res.Get<NewslettersResources>().OfTheTestSample);
      switch (abCampaignReport.CampaignA.MessageBody.MessageBodyType)
      {
        case MessageBodyType.PlainText:
          this.CampaignAType.Text = Res.Get<NewslettersResources>().PlainTextCampaignType;
          break;
        case MessageBodyType.HtmlText:
          this.CampaignAType.Text = Res.Get<NewslettersResources>().HtmlCampaignType;
          break;
        case MessageBodyType.InternalPage:
          this.CampaignAType.Text = Res.Get<NewslettersResources>().StandardCampaignType;
          break;
      }
      switch (abCampaignReport.CampaignB.MessageBody.MessageBodyType)
      {
        case MessageBodyType.PlainText:
          this.CampaignBType.Text = Res.Get<NewslettersResources>().PlainTextCampaignType;
          break;
        case MessageBodyType.HtmlText:
          this.CampaignBType.Text = Res.Get<NewslettersResources>().HtmlCampaignType;
          break;
        case MessageBodyType.InternalPage:
          this.CampaignBType.Text = Res.Get<NewslettersResources>().StandardCampaignType;
          break;
      }
      ITextControl campaignAopenedMessages = this.CampaignAOpenedMessages;
      num = abCampaignReport.CampaignAReport.TotalTimesOpened;
      string str3 = num.ToString();
      campaignAopenedMessages.Text = str3;
      ITextControl campaignBopenedMessages = this.CampaignBOpenedMessages;
      num = abCampaignReport.CampaignBReport.TotalTimesOpened;
      string str4 = num.ToString();
      campaignBopenedMessages.Text = str4;
      this.WebServiceUrlHidden.Value = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Newsletters/ABCampaign.svc"));
      HiddenField campaignIdHidden = this.ABCampaignIdHidden;
      Guid guid = this.ABCampaignId;
      string str5 = guid.ToString();
      campaignIdHidden.Value = str5;
      HiddenField campaignAidHidden = this.CampaignAIdHidden;
      guid = abCampaignReport.CampaignA.Id;
      string str6 = guid.ToString();
      campaignAidHidden.Value = str6;
      this.CampaignBIdHidden.Value = abCampaignReport.CampaignB.Id.ToString();
      this.CampaignANameHidden.Value = abCampaignReport.CampaignA.Name;
      this.CampaignBNameHidden.Value = abCampaignReport.CampaignB.Name;
      this.ABCampaignWinningConditionHidden.Value = abCampaignReport.WinningCondition.ToString();
      Label campaignAclickedLinks = this.CampaignAClickedLinks;
      num = abCampaignReport.CampaignAReport.ClickedLinks.Sum<ClickedLinkCampaignRecord>((Func<ClickedLinkCampaignRecord, int>) (link => link.ClicksCount));
      string str7 = num.ToString();
      campaignAclickedLinks.Text = str7;
      Label campaignBclickedLinks = this.CampaignBClickedLinks;
      num = abCampaignReport.CampaignBReport.ClickedLinks.Sum<ClickedLinkCampaignRecord>((Func<ClickedLinkCampaignRecord, int>) (link => link.ClicksCount));
      string str8 = num.ToString();
      campaignBclickedLinks.Text = str8;
      this.CampaignAClickedLinksFullReport.NavigateUrl = this.GetCampaignFullReportUrl(abCampaignReport.CampaignA.RootCampaign.Id, abCampaignReport.CampaignA.Id);
      this.CampaignBClickedLinksFullReport.NavigateUrl = this.GetCampaignFullReportUrl(abCampaignReport.CampaignB.RootCampaign.Id, abCampaignReport.CampaignB.Id);
    }

    private string GetCampaignFullReportUrl(Guid rootCampaignId, Guid issueId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.issueReportsPageId, false);
      if (siteMapNode == null)
        return "#";
      return VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) rootCampaignId + "/" + (object) issueId + "/from/abCampaignReport");
    }
  }
}
