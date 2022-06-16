// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// The view for exploring the report for a given A/B test.
  /// </summary>
  internal class AbTestReportView : ViewModeControl<NoSidebarNewslettersControlPanel>
  {
    private Guid abTestId;
    private const string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.AbTestReportView.ascx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestReportView" /> class.
    /// </summary>
    public AbTestReportView() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.AbTestReportView.ascx");

    /// <summary>Gets the AbTest id.</summary>
    protected Guid AbTestId
    {
      get
      {
        if (this.abTestId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length == 0)
            throw new InvalidOperationException("It is unclear for which AbTest report ought to be generated. The id of the AbTest is missing from the url.");
          this.abTestId = urlParameters[0].IsGuid() ? new Guid(urlParameters[0]) : throw new ArgumentException("The url parameter carrying information about the AbTest id is not a valid GUID.");
        }
        return this.abTestId;
      }
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets the comparison grid.</summary>
    protected virtual AbTestComparisonGrid ComparisonGrid => this.Container.GetControl<AbTestComparisonGrid>("comparisonGrid", true);

    /// <summary>Gets the test sample label.</summary>
    protected virtual SitefinityLabel TestSampleLabel => this.Container.GetControl<SitefinityLabel>("testSampleLabel", true);

    /// <summary>Gets the winning criteria label.</summary>
    protected virtual SitefinityLabel WinningCriteriaLabel => this.Container.GetControl<SitefinityLabel>("winningCriteriaLabel", true);

    /// <summary>Gets the analysis control.</summary>
    protected virtual AbTestAnalysis Analysis => this.Container.GetControl<AbTestAnalysis>("analysis", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      string str1 = SystemManager.CurrentHttpContext.Request.ParamsGet("providerName");
      if (str1 != null)
        this.ProviderName = str1;
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      ABCampaign abTest = manager.GetABCampaign(this.AbTestId);
      this.Host.Title = abTest.Name;
      this.Host.IsBackLinkVisible = true;
      this.Host.BackLinkText = string.Format(Res.Get<Labels>().BackTo, (object) abTest.RootCampaign.Name);
      this.Host.BackLinkNavigateUrl = this.Host.GetPageUrl(NewslettersModule.campaignOverviewPageId) + "/" + (object) abTest.RootCampaign.Id + "/?providerName=" + this.ProviderName;
      this.ComparisonGrid.ProviderName = this.ProviderName;
      this.ComparisonGrid.AbTest = abTest;
      int int32 = Convert.ToInt32(Math.Floor((double) (manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(abTest.CampaignA.List))).Count<Subscriber>() * abTest.TestingSamplePercentage) * 0.01));
      this.TestSampleLabel.Text = Res.Get<NewslettersResources>().TestSentToSampleOfUsers.Arrange((object) int32, (object) abTest.TestingSamplePercentage);
      string str2;
      switch (abTest.WinningCondition)
      {
        case CampaignWinningCondition.MoreOpenedEmails:
          str2 = Res.Get<NewslettersResources>().MoreOpenedEmails;
          break;
        case CampaignWinningCondition.MoreLinkClicks:
          str2 = Res.Get<NewslettersResources>().MoreLinkClicks;
          break;
        case CampaignWinningCondition.LessBounces:
          str2 = Res.Get<NewslettersResources>().LessBounces;
          break;
        default:
          str2 = Res.Get<NewslettersResources>().Manually;
          break;
      }
      this.WinningCriteriaLabel.Text = Res.Get<NewslettersResources>().WinningCriteria + ": " + str2;
      this.Analysis.AbTest = abTest;
      this.Analysis.ProviderName = this.ProviderName;
    }
  }
}
