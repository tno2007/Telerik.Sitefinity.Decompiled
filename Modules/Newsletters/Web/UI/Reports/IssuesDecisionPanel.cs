// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  public class IssuesDecisionPanel : SimpleScriptView
  {
    private Guid campaignId;
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.IssuesDecisionPanel.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.IssuesDecisionPanel.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel" /> class.
    /// </summary>
    public IssuesDecisionPanel() => this.LayoutTemplatePath = IssuesDecisionPanel.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

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

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets the campaign message body type.</summary>
    protected virtual MessageBodyType CampaignMessageBodyType { get; private set; }

    /// <summary>Gets the name of the root campaign.</summary>
    protected virtual string CampaignName { get; private set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets a reference to the rad window manager.</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("radWindowManager", true);

    /// <summary>Gets a reference to the campaign detail view dialog.</summary>
    protected virtual RadWindow CampaignDetailViewDialog => this.RadWindowManager.Windows.Cast<RadWindow>().First<RadWindow>((Func<RadWindow, bool>) (w => w.ID == "campaignDetailView"));

    /// <summary>Gets a reference to the create new issue button.</summary>
    protected virtual LinkButton CreateNewIssueButton => this.Container.GetControl<LinkButton>("createNewIssueButton", true);

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
      controlDescriptor.AddComponentProperty("campaignDetailView", this.CampaignDetailViewDialog.ClientID);
      controlDescriptor.AddProperty("campaignId", (object) this.CampaignId);
      controlDescriptor.AddProperty("campaignName", (object) this.CampaignName);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      controlDescriptor.AddElementProperty("createIssueButton", this.CreateNewIssueButton.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.IssuesDecisionPanel.js", typeof (IssuesDecisionPanel).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", typeof (IssuesDecisionPanel).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
