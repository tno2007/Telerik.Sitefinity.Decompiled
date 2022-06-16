// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Data.Reports;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The view for exploring the report for a given campaign.
  /// </summary>
  [Obsolete("Use CampaignOverview instead.")]
  public class CampaignReportView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.CampaignReportView.ascx";
    private Guid campaignId;
    private CampaignReport campaignReport;
    private ITextControl clickRate;
    private ITextControl lastClick;
    private ITextControl lastOpened;
    private ITextControl recipientsWhoClicked;
    private ITextControl recipientsWhoOpened;
    private ITextControl successfulRecipients;
    private ITextControl totalBounced;
    private ITextControl totalClicks;
    private ITextControl totalRecipients;
    private ITextControl totalTimesOpened;
    private ITextControl totalUnopened;
    private ITextControl totalUnsubscriptions;

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

    public CampaignReport CampaignReport
    {
      get
      {
        if (this.campaignReport == null)
          this.campaignReport = new CampaignReport(string.Empty, this.CampaignId);
        return this.campaignReport;
      }
      set => this.campaignReport = value;
    }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath(CampaignReportView.layoutTemplatePath) : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the control that displays the total number of times a campaign message
    /// has been opened (if the same recipients opens the campaign twice, both times will count).
    /// </summary>
    protected virtual ITextControl TotalTimesOpened
    {
      get
      {
        if (this.totalTimesOpened == null)
          this.totalTimesOpened = this.Container.GetControl<ITextControl>("totalTimesOpened", true);
        return this.totalTimesOpened;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the total number of recipients of this campaign.
    /// </summary>
    protected virtual ITextControl TotalRecipients
    {
      get
      {
        if (this.totalRecipients == null)
          this.totalRecipients = this.Container.GetControl<ITextControl>("totalRecipients", true);
        return this.totalRecipients;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the unique number of recipients that clicked on
    /// the link in the campaign.
    /// </summary>
    protected virtual ITextControl RecipientsWhoClicked
    {
      get
      {
        if (this.recipientsWhoClicked == null)
          this.recipientsWhoClicked = this.Container.GetControl<ITextControl>("recipientsWhoClicked", true);
        return this.recipientsWhoClicked;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the unique number of recipients that have opened
    /// the campaign message.
    /// </summary>
    protected virtual ITextControl RecipientsWhoOpened
    {
      get
      {
        if (this.recipientsWhoOpened == null)
          this.recipientsWhoOpened = this.Container.GetControl<ITextControl>("recipientsWhoOpened", true);
        return this.recipientsWhoOpened;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the total number of clicks made in a campaign.
    /// </summary>
    protected virtual ITextControl TotalClicks
    {
      get
      {
        if (this.totalClicks == null)
          this.totalClicks = this.Container.GetControl<ITextControl>("totalClicks", true);
        return this.totalClicks;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the date of the last click.
    /// </summary>
    public virtual ITextControl LastClick
    {
      get
      {
        if (this.lastClick == null)
          this.lastClick = this.Container.GetControl<ITextControl>("lastClickDate", true);
        return this.lastClick;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the date when the last message was opened.
    /// </summary>
    public virtual ITextControl LastOpened
    {
      get
      {
        if (this.lastOpened == null)
          this.lastOpened = this.Container.GetControl<ITextControl>("lastOpenDate", true);
        return this.lastOpened;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the number of successful recipients.
    /// </summary>
    public virtual ITextControl SuccessfulRecipients
    {
      get
      {
        if (this.successfulRecipients == null)
          this.successfulRecipients = this.Container.GetControl<ITextControl>("successfullRecipients", true);
        return this.successfulRecipients;
      }
    }

    protected virtual RadGrid ClickedLinksGrid => this.Container.GetControl<RadGrid>("clickedLinksGrid", true);

    /// <summary>
    /// Gets the reference to the control that displays the total number of unopened messasges.
    /// </summary>
    protected virtual ITextControl TotalUnopened
    {
      get
      {
        if (this.totalUnopened == null)
          this.totalUnopened = this.Container.GetControl<ITextControl>("totalUnopened", true);
        return this.totalUnopened;
      }
    }

    /// <summary>
    /// Gets the control that displays the number of messages that have bounced.
    /// </summary>
    protected virtual ITextControl TotalBounced
    {
      get
      {
        if (this.totalBounced == null)
          this.totalBounced = this.Container.GetControl<ITextControl>("totalBounced", true);
        return this.totalBounced;
      }
    }

    /// <summary>
    /// Gets the control that displays the click rate of the campaign.
    /// </summary>
    protected virtual ITextControl ClickRate
    {
      get
      {
        if (this.clickRate == null)
          this.clickRate = this.Container.GetControl<ITextControl>("clickRate", true);
        return this.clickRate;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the total number of unsubscriptions that resulted
    /// from this campaign.
    /// </summary>
    protected virtual ITextControl TotalUnsubscriptions => this.totalUnsubscriptions == null ? (this.totalUnsubscriptions = this.Container.GetControl<ITextControl>("totalUnsubscriptions", true)) : this.totalUnsubscriptions;

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.Host.Title = string.Format("{0} | {1}", (object) Res.Get<NewslettersResources>().Reports, (object) this.CampaignReport.CampaignName);
      this.SetReport();
      this.ClickedLinksGrid.NeedDataSource += new GridNeedDataSourceEventHandler(this.ClickedLinksGrid_NeedDataSource);
      base.InitializeControls(viewContainer);
    }

    public void SetReport()
    {
      this.ClickRate.Text = this.CampaignReport.ClickRate.ToString("P");
      DateTime localTime;
      if (this.CampaignReport.LastClick.HasValue)
      {
        ITextControl lastClick = this.LastClick;
        localTime = this.CampaignReport.LastClick.Value;
        localTime = localTime.ToLocalTime();
        string str = localTime.ToString();
        lastClick.Text = str;
      }
      if (this.CampaignReport.LastOpenedDate.HasValue)
      {
        ITextControl lastOpened = this.LastOpened;
        localTime = this.CampaignReport.LastOpenedDate.Value;
        localTime = localTime.ToLocalTime();
        string str = localTime.ToString();
        lastOpened.Text = str;
      }
      this.RecipientsWhoClicked.Text = this.CampaignReport.RecipientsWhoClicked.ToString();
      this.RecipientsWhoOpened.Text = this.CampaignReport.RecipientsWhoOpened.ToString();
      this.SuccessfulRecipients.Text = this.CampaignReport.SuccessfulDeliveries.ToString();
      this.TotalBounced.Text = this.CampaignReport.TotalBounced.ToString();
      this.TotalClicks.Text = this.CampaignReport.TotalClicks.ToString();
      this.TotalRecipients.Text = this.CampaignReport.TotalRecipients.ToString();
      this.TotalTimesOpened.Text = this.CampaignReport.TotalTimesOpened.ToString();
      this.TotalUnopened.Text = this.CampaignReport.TotalUnopened.ToString();
      this.TotalUnsubscriptions.Text = this.CampaignReport.TotalUnsubscriptions.ToString();
    }

    /// <summary>
    /// Handles the need data source event of the clicked links grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ClickedLinksGrid_NeedDataSource(
      object sender,
      GridNeedDataSourceEventArgs e)
    {
      this.ClickedLinksGrid.DataSource = (object) this.CampaignReport.ClickedLinks;
    }
  }
}
