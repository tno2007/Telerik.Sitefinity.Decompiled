// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>
  /// Wizard wrapper for creating newsletter campaigns in Sitefinity using the Kendo UI.
  /// </summary>
  public class CampaignDetailView : AjaxDialogBase
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignDetailView.js";
    private const string kendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    private const string campaignServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.CampaignDetailView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignDetailView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the dialog title label.</summary>
    protected virtual Label DialogTitleLabel => this.Container.GetControl<Label>("dialogTitle", true);

    /// <summary>Gets a reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets a reference to the back link label.</summary>
    protected virtual Label BackLinkLabel => this.Container.GetControl<Label>("backLink", true);

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public virtual string ProviderName { get; set; }

    /// <summary>Gets the reference to the send button.</summary>
    protected virtual HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Gets a reference to the back link anchor.</summary>
    protected virtual HtmlAnchor BackLinkAnchor => this.Container.GetControl<HtmlAnchor>("backLinkAnchor", true);

    /// <summary>Gets a reference to the issue properties view.</summary>
    protected virtual IssuePropertiesView IssuePropertiesView => this.Container.GetControl<IssuePropertiesView>(nameof (IssuePropertiesView), true);

    /// <summary>Gets a reference to the campaign properties view.</summary>
    protected virtual CampaignPropertiesView CampaignPropertiesView => this.Container.GetControl<CampaignPropertiesView>(nameof (CampaignPropertiesView), true);

    /// <summary>Gets a reference to the issue message view.</summary>
    protected virtual IssueMessageView IssueMessageView => this.Container.GetControl<IssueMessageView>(nameof (IssueMessageView), true);

    /// <summary>Gets a reference to the mailing list selector.</summary>
    protected virtual MailingListSelector MailingListSelector => this.Container.GetControl<MailingListSelector>("mailingListSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      if (str != null)
        this.ProviderName = str;
      this.CampaignPropertiesView.Host = this;
    }

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
      controlDescriptor.AddElementProperty("dialogTitleLabel", this.DialogTitleLabel.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("backLinkLabel", this.BackLinkLabel.ClientID);
      controlDescriptor.AddElementProperty("backLinkAnchor", this.BackLinkAnchor.ClientID);
      controlDescriptor.AddProperty("campaignServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc"));
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddComponentProperty("mailingListSelector", this.MailingListSelector.ClientID);
      controlDescriptor.AddComponentProperty("campaignPropertiesView", this.CampaignPropertiesView.ClientID);
      controlDescriptor.AddComponentProperty("issuePropertiesView", this.IssuePropertiesView.ClientID);
      controlDescriptor.AddComponentProperty("issueMessageView", this.IssueMessageView.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignDetailView.js", typeof (CampaignDetailView).Assembly.FullName)
    };
  }
}
