// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// The control that represents the sidebar of the command panel of the subscribers report.
  /// </summary>
  public class SubscribersReportSidebar : SimpleScriptView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.SubscribersReportSidebar.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.SubscribersReportSidebar.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar" /> class.
    /// </summary>
    public SubscribersReportSidebar() => this.LayoutTemplatePath = SubscribersReportSidebar.layoutTemplatePath;

    /// <summary>Gets the provider name.</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets the issue id.</summary>
    protected Guid IssueId { get; private set; }

    /// <summary>Gets a reference to the "All Subscribers" link.</summary>
    protected virtual HyperLink AllSubscribers => this.Container.GetControl<HyperLink>("allSubscribers", true);

    /// <summary>Gets a reference to the "Opened" link.</summary>
    protected virtual HyperLink OpenedSubscribers => this.Container.GetControl<HyperLink>("openedSubscribers", true);

    /// <summary>Gets a reference to the clicked subscribers link.</summary>
    protected virtual HyperLink ClickedSubscribers => this.Container.GetControl<HyperLink>("clickedSubscribers", true);

    /// <summary>
    /// Gets a reference to the not delivered subscribers link.
    /// </summary>
    protected virtual HyperLink NotDeliveredSubscribers => this.Container.GetControl<HyperLink>("notDeliveredSubscribers", true);

    /// <summary>
    /// Gets a reference to the invalid email subscribers link.
    /// </summary>
    protected virtual HyperLink InvalidEmailSubscribers => this.Container.GetControl<HyperLink>("invalidEmailSubscribers", true);

    /// <summary>
    /// Gets a reference to the unsubscribed subscribers links.
    /// </summary>
    protected virtual HyperLink UnsubscribedSubscribers => this.Container.GetControl<HyperLink>("unsubscribedSubscribers", true);

    /// <summary>Gets a reference to the "by clicked links" link.</summary>
    protected virtual HyperLink ByClickedLinks => this.Container.GetControl<HyperLink>("byClickedLinks", true);

    /// <summary>Gets a reference to the filters container.</summary>
    protected virtual HtmlContainerControl FiltersContainer => this.Container.GetControl<HtmlContainerControl>("filtersContainer", true);

    /// <summary>Gets a reference to the clicked links container.</summary>
    protected virtual HtmlContainerControl ClickedLinksContainer => this.Container.GetControl<HtmlContainerControl>("clickedLinksContainer", true);

    /// <summary>Gets a reference to the clicked links.</summary>
    protected virtual SubscribersReportClickedLinks ClickedLinks => this.Container.GetControl<SubscribersReportClickedLinks>("clickedLinks", true);

    /// <summary>Gets a reference to the back from clicked links.</summary>
    protected virtual HyperLink BackFromClickedLinks => this.Container.GetControl<HyperLink>("backFromClickedLinks", true);

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

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
      controlDescriptor.AddElementProperty("allSubscribers", this.AllSubscribers.ClientID);
      controlDescriptor.AddElementProperty("openedSubscribers", this.OpenedSubscribers.ClientID);
      controlDescriptor.AddElementProperty("clickedSubscribers", this.ClickedSubscribers.ClientID);
      controlDescriptor.AddElementProperty("notDeliveredSubscribers", this.NotDeliveredSubscribers.ClientID);
      controlDescriptor.AddElementProperty("invalidEmailSubscribers", this.InvalidEmailSubscribers.ClientID);
      controlDescriptor.AddElementProperty("unsubscribedSubscribers", this.UnsubscribedSubscribers.ClientID);
      controlDescriptor.AddElementProperty("byClickedLinks", this.ByClickedLinks.ClientID);
      controlDescriptor.AddElementProperty("filtersContainer", this.FiltersContainer.ClientID);
      controlDescriptor.AddElementProperty("clickedLinksContainer", this.ClickedLinksContainer.ClientID);
      controlDescriptor.AddComponentProperty("clickedLinks", this.ClickedLinks.ClientID);
      controlDescriptor.AddElementProperty("backFromClickedLinks", this.BackFromClickedLinks.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.SubscribersReportSidebar.js", typeof (SubscribersReportSidebar).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ProviderName = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      string[] urlParameters = this.GetUrlParameters();
      if (urlParameters.Length < 2)
        throw new InvalidOperationException("It is unclear for which issue report ought to be generated. The id of the issue is missing from the url.");
      this.IssueId = urlParameters[1].IsGuid() ? new Guid(urlParameters[1]) : throw new ArgumentException("The url parameter carrying information about the issue id is not a valid GUID.");
      int num = NewslettersManager.GetManager(this.ProviderName).GetIssueSubscriberReports().Count<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.IssueId == this.IssueId));
      this.AllSubscribers.Text = Res.Get<NewslettersResources>().AllSubscribers + " (" + num.ToString() + ")";
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
