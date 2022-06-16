// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// The control that represents the clicked links on the sidebar of the command panel of the subscribers report.
  /// </summary>
  public class SubscribersReportClickedLinks : KendoView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.SubscribersReportClickedLinks.ascx");
    private static readonly string webServiceUrl = "~/Sitefinity/Services/Newsletters/IssueReport.svc/totallinkclicks/{0}/?provider={1}&search={2}";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.SubscribersReportClickedLinks.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks" /> class.
    /// </summary>
    public SubscribersReportClickedLinks() => this.LayoutTemplatePath = SubscribersReportClickedLinks.layoutTemplatePath;

    /// <summary>Gets the provider name.</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets the issue id.</summary>
    protected string IssueId { get; private set; }

    /// <summary>Gets a reference the search box.</summary>
    protected virtual BackendSearchBox SearchBox => this.Container.GetControl<BackendSearchBox>("searchBox", true);

    /// <summary>Gets a reference to the clicked link filters.</summary>
    protected virtual HtmlGenericControl ClickedLinkFilters => this.Container.GetControl<HtmlGenericControl>("clickedLinkFilters", true);

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
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
      controlDescriptor.AddElementProperty("clickedLinkFilters", this.ClickedLinkFilters.ClientID);
      string str = this.Page.ResolveUrl(string.Format(SubscribersReportClickedLinks.webServiceUrl, (object) this.IssueId, (object) this.ProviderName, (object) "{0}"));
      controlDescriptor.AddProperty("_webServiceUrl", (object) str);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.SubscribersReportClickedLinks.js", typeof (SubscribersReportClickedLinks).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ProviderName = SystemManager.CurrentHttpContext.Request.ParamsGet("providerName");
      string[] urlParameters = this.GetUrlParameters();
      if (urlParameters.Length < 2)
        throw new InvalidOperationException("It is unclear for which issue report ought to be generated. The id of the issue is missing from the url.");
      this.IssueId = urlParameters[1].IsGuid() ? urlParameters[1] : throw new ArgumentException("The url parameter carrying information about the issue id is not a valid GUID.");
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
