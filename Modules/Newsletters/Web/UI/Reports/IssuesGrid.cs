// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  public class IssuesGrid : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.IssuesGrid.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.IssuesGrid.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc/issues/{0}/?provider={1}&sortExpression=DeliveryDate DESC";
    private const string issueServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc/issue/{0}/?provider={1}";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid" /> class.
    /// </summary>
    public IssuesGrid() => this.LayoutTemplatePath = IssuesGrid.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the campaign id.</summary>
    public Guid CampaignId { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for sent issues.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("sentIssuesGrid", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for scheduled issues.
    /// </summary>
    protected virtual HtmlContainerControl ScheduledIssuesGrid => this.Container.GetControl<HtmlContainerControl>("scheduledIssuesGrid", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for draft issues.
    /// </summary>
    protected virtual HtmlContainerControl DraftIssuesGrid => this.Container.GetControl<HtmlContainerControl>("draftIssuesGrid", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the campaign preview window.</summary>
    protected virtual CampaignPreviewWindow CampaignPreviewWindow => this.Container.GetControl<CampaignPreviewWindow>("campaignPreviewWindow", true);

    /// <summary>Gets a reference to the issues tab strip.</summary>
    protected virtual RadTabStrip IssuesTabStrip => this.Container.GetControl<RadTabStrip>("issuesTabStrip", true);

    /// <summary>Gets a reference to the campaign detail view window.</summary>
    protected virtual RadWindow CampaignDetailView => this.Container.GetControl<RadWindow>("campaignDetailView", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (this.IssuesTabStrip.FindTabByValue("sent").Visible)
        controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      if (this.IssuesTabStrip.FindTabByValue("scheduled").Visible)
        controlDescriptor.AddElementProperty("scheduledIssuesGrid", this.ScheduledIssuesGrid.ClientID);
      if (this.IssuesTabStrip.FindTabByValue("draft").Visible)
        controlDescriptor.AddElementProperty("draftIssuesGrid", this.DraftIssuesGrid.ClientID);
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc/issues/{0}/?provider={1}&sortExpression=DeliveryDate DESC").Arrange((object) this.CampaignId.ToString(), (object) this.ProviderName);
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddProperty("issueServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc/issue/{0}/?provider={1}").Arrange((object) "{0}", (object) this.ProviderName));
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("campaignPreviewWindow", this.CampaignPreviewWindow.ClientID);
      controlDescriptor.AddComponentProperty("issuesTabStrip", this.IssuesTabStrip.ClientID);
      controlDescriptor.AddComponentProperty("campaignDetailView", this.CampaignDetailView.ClientID);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      Campaign campaign = NewslettersManager.GetManager(this.ProviderName).GetCampaign(this.CampaignId);
      if (campaign != null)
        controlDescriptor.AddProperty("campaignName", (object) campaign.Name);
      controlDescriptor.AddProperty("_cookiePath", (object) this.Page.Request.ApplicationPath);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.IssuesGrid.js", typeof (IssuesGrid).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", typeof (IssuesGrid).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      RadTab tabByValue1 = this.IssuesTabStrip.FindTabByValue("sent");
      RadTab tabByValue2 = this.IssuesTabStrip.FindTabByValue("scheduled");
      RadTab tabByValue3 = this.IssuesTabStrip.FindTabByValue("draft");
      tabByValue1.Visible = (manager.GetIssues(this.CampaignId).Any<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 4 || (int) i.CampaignState == 2)) ? 1 : 0) != 0;
      tabByValue2.Visible = (manager.GetIssues(this.CampaignId).Any<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 3)) ? 1 : 0) != 0;
      tabByValue3.Visible = (manager.GetIssues(this.CampaignId).Any<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 0 || (int) i.CampaignState == 1)) ? 1 : 0) != 0;
      this.Visible = tabByValue1.Visible || tabByValue2.Visible || tabByValue3.Visible;
      if (!this.Visible)
        return;
      RadMultiPage control = this.Container.GetControl<RadMultiPage>("issuesMultiPage", true);
      if (tabByValue1.Visible)
      {
        this.IssuesTabStrip.SelectedIndex = tabByValue1.Index;
        control.SelectedIndex = tabByValue1.Index;
      }
      else if (tabByValue2.Visible)
      {
        this.IssuesTabStrip.SelectedIndex = tabByValue2.Index;
        control.SelectedIndex = tabByValue2.Index;
      }
      else
      {
        this.IssuesTabStrip.SelectedIndex = tabByValue3.Index;
        control.SelectedIndex = tabByValue3.Index;
      }
    }
  }
}
