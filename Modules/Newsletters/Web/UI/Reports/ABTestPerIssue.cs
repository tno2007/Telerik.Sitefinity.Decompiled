// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestPerIssue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>Represents the control listing ab test per issue.</summary>
  public class ABTestPerIssue : SimpleView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.ABTestPerIssue.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestPerIssue" /> class.
    /// </summary>
    public ABTestPerIssue() => this.LayoutTemplatePath = ABTestPerIssue.layoutTemplatePath;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the root campaign id.</summary>
    /// <value>The root campaign id.</value>
    public Guid RootCampaignId { get; set; }

    /// <summary>Gets or sets the issue id.</summary>
    /// <value>The issue id.</value>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the label that will contain the name of the ab test.
    /// </summary>
    protected virtual ITextControl AbTestName => this.Container.GetControl<ITextControl>("abTestName", true);

    /// <summary>Gets the sample users value label.</summary>
    protected virtual ITextControl SampleUsersValueLabel => this.Container.GetControl<ITextControl>("sampleUsersValueLabel", true);

    /// <summary>Gets the winner value label.</summary>
    protected virtual ITextControl WinnerValueLabel => this.Container.GetControl<ITextControl>("winnerValueLabel", true);

    /// <summary>Gets the date sent value label.</summary>
    protected virtual ITextControl DateSentValueLabel => this.Container.GetControl<ITextControl>("dateSentValueLabel", true);

    /// <summary>Gets the date ended value label.</summary>
    protected virtual ITextControl DateEndedValueLabel => this.Container.GetControl<ITextControl>("dateEndedValueLabel", true);

    /// <summary>Gets the what was tested label.</summary>
    protected virtual Label WhatWasTestedLabel => this.Container.GetControl<Label>("whatWasTestedLabel", true);

    /// <summary>Gets the conclusion label.</summary>
    protected virtual Label ConclusionLabel => this.Container.GetControl<Label>("conclusionLabel", true);

    /// <summary>Gets the full report link.</summary>
    protected virtual HyperLink FullReportLink => this.Container.GetControl<HyperLink>("fullReportLink", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      ABCampaign abCampaign = manager.GetABCampaigns(this.RootCampaignId).Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (a => a.WinnerIssue.Id == this.IssueId)).FirstOrDefault<ABCampaign>();
      if (abCampaign != null)
      {
        this.AbTestName.Text = abCampaign.Name;
        this.SampleUsersValueLabel.Text = string.Format("{0} ({1}%)", (object) Convert.ToInt32(Math.Floor((double) (manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(abCampaign.CampaignA.List))).Count<Subscriber>() * abCampaign.TestingSamplePercentage) * 0.01)), (object) abCampaign.TestingSamplePercentage);
        string str = abCampaign.WinnerIssueOriginalId == abCampaign.CampaignA.Id ? Res.Get<NewslettersResources>().IssueA : Res.Get<NewslettersResources>().IssueB;
        this.WinnerValueLabel.Text = string.Format("{0} ({1})", (object) abCampaign.WinnerIssue.Name, (object) str);
        if (abCampaign.DateSent != DateTime.MinValue)
          this.DateSentValueLabel.Text = abCampaign.DateSent.ToSitefinityUITime().ToString("dd MMM yyyy, hh:mm");
        this.DateEndedValueLabel.Text = abCampaign.TestingEnds.ToSitefinityUITime().ToString("dd MMM yyyy, hh:mm");
        this.WhatWasTestedLabel.Text = abCampaign.TestingNote;
        this.ConclusionLabel.Text = abCampaign.Conclusion;
        this.FullReportLink.NavigateUrl = this.GetPageUrl(NewslettersModule.abTestReportPageId) + "/" + (object) abCampaign.Id + "/?providerName=" + this.ProviderName;
      }
      else
        this.FullReportLink.Visible = false;
    }

    private string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
