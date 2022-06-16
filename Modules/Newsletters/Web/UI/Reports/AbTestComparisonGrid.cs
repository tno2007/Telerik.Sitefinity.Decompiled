// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestComparisonGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// Control that displays statistics of A and B issues of an A/B test.
  /// </summary>
  public class AbTestComparisonGrid : SimpleView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.AbTestComparisonGrid.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestComparisonGrid" /> class.
    /// </summary>
    public AbTestComparisonGrid() => this.LayoutTemplatePath = AbTestComparisonGrid.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the ab test.</summary>
    public ABCampaign AbTest { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the issue A name label.</summary>
    protected virtual Label IssueANameLabel => this.Container.GetControl<Label>("issueANameLabel", true);

    /// <summary>Gets the issue A delivered label.</summary>
    protected virtual Label IssueADeliveredLabel => this.Container.GetControl<Label>("issueADeliveredLabel", true);

    /// <summary>Gets the issue A unique openings label.</summary>
    protected virtual Label IssueAUniqueOpeningsLabel => this.Container.GetControl<Label>("issueAUniqueOpeningsLabel", true);

    /// <summary>Gets the issue A unique clicks label.</summary>
    protected virtual Label IssueAUniqueClicksLabel => this.Container.GetControl<Label>("issueAUniqueClicksLabel", true);

    /// <summary>Gets the issue A unsubscribed label.</summary>
    protected virtual Label IssueAUnsubscribedLabel => this.Container.GetControl<Label>("issueAUnsubscribedLabel", true);

    /// <summary>Gets the issue A opened in first hours label.</summary>
    protected virtual Label IssueAOpenedInFirstHoursLabel => this.Container.GetControl<Label>("issueAOpenedInFirstHoursLabel", true);

    /// <summary>Gets the issue B name label.</summary>
    protected virtual Label IssueBNameLabel => this.Container.GetControl<Label>("issueBNameLabel", true);

    /// <summary>Gets the issue B delivered label.</summary>
    protected virtual Label IssueBDeliveredLabel => this.Container.GetControl<Label>("issueBDeliveredLabel", true);

    /// <summary>Gets the issue B unique openings label.</summary>
    protected virtual Label IssueBUniqueOpeningsLabel => this.Container.GetControl<Label>("issueBUniqueOpeningsLabel", true);

    /// <summary>Gets the issue B unique clicks label.</summary>
    protected virtual Label IssueBUniqueClicksLabel => this.Container.GetControl<Label>("issueBUniqueClicksLabel", true);

    /// <summary>Gets the issue B unsubscribed label.</summary>
    protected virtual Label IssueBUnsubscribedLabel => this.Container.GetControl<Label>("issueBUnsubscribedLabel", true);

    /// <summary>Gets the issue B opened in first hours label.</summary>
    protected virtual Label IssueBOpenedInFirstHoursLabel => this.Container.GetControl<Label>("issueBOpenedInFirstHoursLabel", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      string str = "{0} ({1})";
      IssueStatistics issueStatistics1 = new IssueStatistics(this.AbTest.CampaignA.Id, manager);
      this.IssueANameLabel.Text = str.Arrange((object) this.AbTest.CampaignA.Name, (object) Res.Get<NewslettersResources>().IssueA);
      this.IssueADeliveredLabel.Text = issueStatistics1.DeliveredMailsCount.ToString();
      this.IssueAUniqueOpeningsLabel.Text = issueStatistics1.UniqueOpeningsCount.ToString();
      this.IssueAUniqueClicksLabel.Text = issueStatistics1.ClickedIssuesCount.ToString();
      this.IssueAUnsubscribedLabel.Text = issueStatistics1.UnsubscribedCount.ToString();
      this.IssueAOpenedInFirstHoursLabel.Text = issueStatistics1.OpenedInFirstHoursCount.ToString();
      IssueStatistics issueStatistics2 = new IssueStatistics(this.AbTest.CampaignB.Id, manager);
      this.IssueBNameLabel.Text = str.Arrange((object) this.AbTest.CampaignA.Name, (object) Res.Get<NewslettersResources>().IssueB);
      this.IssueBDeliveredLabel.Text = issueStatistics2.DeliveredMailsCount.ToString();
      this.IssueBUniqueOpeningsLabel.Text = issueStatistics2.UniqueOpeningsCount.ToString();
      this.IssueBUniqueClicksLabel.Text = issueStatistics2.ClickedIssuesCount.ToString();
      this.IssueBUnsubscribedLabel.Text = issueStatistics2.UnsubscribedCount.ToString();
      this.IssueBOpenedInFirstHoursLabel.Text = issueStatistics2.OpenedInFirstHoursCount.ToString();
    }
  }
}
