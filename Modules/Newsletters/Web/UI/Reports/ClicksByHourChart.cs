// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>The chart representing clicks by hour statistics.</summary>
  public class ClicksByHourChart : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.ClicksByHourChart.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.ClicksByHourChart.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/IssueReport.svc/clicksbyhour/{0}/?provider={1}";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart" /> class.
    /// </summary>
    public ClicksByHourChart() => this.LayoutTemplatePath = ClicksByHourChart.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

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
    /// Gets a reference to the div element that will be transformed into a chart.
    /// </summary>
    protected virtual HtmlContainerControl Chart => this.Container.GetControl<HtmlContainerControl>("chart", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

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
      controlDescriptor.AddElementProperty("chart", this.Chart.ClientID);
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/IssueReport.svc/clicksbyhour/{0}/?provider={1}").Arrange((object) this.IssueId, (object) this.ProviderName);
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.ClicksByHourChart.js", typeof (ClicksByHourChart).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
