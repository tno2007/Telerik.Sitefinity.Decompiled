// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>Represents the control listing all sent ab tests.</summary>
  public class ABTestsGrid : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.ABTestsGrid.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.ABTestsGrid.ascx");
    private static readonly string webServiceUrl = "~/Sitefinity/Services/Newsletters/ABCampaign.svc/abtests/{0}/?provider={1}&sortExpression=DateSent DESC&filter=";

    /// <summary>The grid representing A/B tests.</summary>
    public ABTestsGrid() => this.LayoutTemplatePath = ABTestsGrid.layoutTemplatePath;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the root campaign id.</summary>
    /// <value>The root campaign id.</value>
    public Guid RootCampaignId { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for sent tests.
    /// </summary>
    protected virtual HtmlContainerControl SentTestsGrid => this.Container.GetControl<HtmlContainerControl>("sentTestsGrid", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for scheduled tests.
    /// </summary>
    protected virtual HtmlContainerControl ScheduledTestsGrid => this.Container.GetControl<HtmlContainerControl>("scheduledTestsGrid", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid for draft tests.
    /// </summary>
    protected virtual HtmlContainerControl DraftTestsGrid => this.Container.GetControl<HtmlContainerControl>("draftTestsGrid", true);

    protected virtual RadTabStrip AbTestsTabStrip => this.Container.GetControl<RadTabStrip>("abTestsTabStrip", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      RadTab tabByValue1 = this.AbTestsTabStrip.FindTabByValue("sent");
      RadTab tabByValue2 = this.AbTestsTabStrip.FindTabByValue("scheduled");
      RadTab tabByValue3 = this.AbTestsTabStrip.FindTabByValue("draft");
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      tabByValue1.Visible = (manager.GetABCampaigns(this.RootCampaignId).Any<ABCampaign>((Expression<Func<ABCampaign, bool>>) (c => (int) c.ABTestingStatus == 3 || (int) c.ABTestingStatus == 2)) ? 1 : 0) != 0;
      tabByValue2.Visible = (manager.GetABCampaigns(this.RootCampaignId).Any<ABCampaign>((Expression<Func<ABCampaign, bool>>) (c => (int) c.ABTestingStatus == 0)) ? 1 : 0) != 0;
      tabByValue3.Visible = (manager.GetABCampaigns(this.RootCampaignId).Any<ABCampaign>((Expression<Func<ABCampaign, bool>>) (c => (int) c.ABTestingStatus == 1)) ? 1 : 0) != 0;
      this.Visible = tabByValue1.Visible || tabByValue2.Visible || tabByValue3.Visible;
      if (!this.Visible)
        return;
      RadMultiPage control = this.Container.GetControl<RadMultiPage>("abTestsMultiPage", true);
      if (tabByValue1.Visible)
      {
        this.AbTestsTabStrip.SelectedIndex = tabByValue1.Index;
        control.SelectedIndex = tabByValue1.Index;
      }
      else if (tabByValue2.Visible)
      {
        this.AbTestsTabStrip.SelectedIndex = tabByValue2.Index;
        control.SelectedIndex = tabByValue2.Index;
      }
      else
      {
        this.AbTestsTabStrip.SelectedIndex = tabByValue3.Index;
        control.SelectedIndex = tabByValue3.Index;
      }
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (this.AbTestsTabStrip.FindTabByValue("sent").Visible)
        controlDescriptor.AddElementProperty("sentTestsGrid", this.SentTestsGrid.ClientID);
      if (this.AbTestsTabStrip.FindTabByValue("scheduled").Visible)
        controlDescriptor.AddElementProperty("scheduledTestsGrid", this.ScheduledTestsGrid.ClientID);
      if (this.AbTestsTabStrip.FindTabByValue("draft").Visible)
        controlDescriptor.AddElementProperty("draftTestsGrid", this.DraftTestsGrid.ClientID);
      string str = this.Page.ResolveUrl(ABTestsGrid.webServiceUrl).Arrange((object) this.RootCampaignId, (object) this.ProviderName);
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddProperty("_reportUrl", (object) (this.GetPageUrl(NewslettersModule.abTestReportPageId) + "/{0}/?providerName=" + this.ProviderName));
      controlDescriptor.AddComponentProperty("abTestsTabStrip", this.AbTestsTabStrip.ClientID);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("_cookiePath", (object) this.Page.Request.ApplicationPath);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.ABTestsGrid.js", typeof (ABTestsGrid).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", typeof (ABTestsGrid).Assembly.FullName)
    };

    private string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
