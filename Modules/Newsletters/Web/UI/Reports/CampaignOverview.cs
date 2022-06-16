// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverview
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// The view for exploring the report for a given campaign.
  /// </summary>
  public class CampaignOverview : ViewModeControl<NoSidebarNewslettersControlPanel>
  {
    private Guid campaignId;
    private const string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.CampaignOverview.ascx";

    /// <summary>Campaigns the overview.</summary>
    public CampaignOverview() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.CampaignOverview.ascx");

    /// <summary>Gets the campaign id.</summary>
    /// <value>The campaign id.</value>
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

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets a reference to the issues grid.</summary>
    protected virtual IssuesGrid IssuesGrid => this.Container.GetControl<IssuesGrid>("issuesGrid", true);

    /// <summary>Gets a reference to the issues decision panel.</summary>
    protected virtual IssuesDecisionPanel IssuesDecisionPanel => this.Container.GetControl<IssuesDecisionPanel>("issuesDecisionPanel", true);

    /// <summary>Gets a reference to the campaign overview chart.</summary>
    protected virtual CampaignOverviewChart CampaignOverviewChart => this.Container.GetControl<CampaignOverviewChart>("campaignOverviewChart", true);

    /// <summary>Gets a reference to the A/B Tests grid.</summary>
    protected virtual ABTestsGrid ABTestsGrid => this.Container.GetControl<ABTestsGrid>("abTestsGrid", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      string str = SystemManager.CurrentHttpContext.Request.ParamsGet("providerName");
      if (str != null)
        this.ProviderName = str;
      Campaign campaign = NewslettersManager.GetManager(this.ProviderName).GetCampaign(this.CampaignId);
      if (campaign != null)
        this.Host.Title = HttpUtility.HtmlEncode(campaign.Name.Length < 29 ? campaign.Name : campaign.Name.Substring(0, 26) + "...");
      this.IssuesGrid.CampaignId = this.CampaignId;
      this.IssuesGrid.ProviderName = this.ProviderName;
      this.ABTestsGrid.RootCampaignId = this.CampaignId;
      this.ABTestsGrid.ProviderName = this.ProviderName;
      this.IssuesDecisionPanel.ProviderName = this.ProviderName;
      this.CampaignOverviewChart.CampaignId = this.CampaignId;
      this.CampaignOverviewChart.ProviderName = this.ProviderName;
    }
  }
}
