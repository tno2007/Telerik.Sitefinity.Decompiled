// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>
  /// Wizard wrapper for creating newsletter campaigns in Sitefinity.
  /// </summary>
  [Obsolete("Use CampaignDetailView instead.")]
  public class CampaignWizard : AjaxDialogBase
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignWizard.js";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string campaignServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.CampaignWizard.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignWizard.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the wizard title literal.</summary>
    /// <value>The wizard title literal.</value>
    protected virtual Label WizardTitleLabel => this.Container.GetControl<Label>("wizardTitle", true);

    /// <summary>Gets the reference to the wizard control.</summary>
    protected virtual SitefinityWizard Wizard => this.Container.GetControl<SitefinityWizard>("campaignWizard", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      SitefinityWizardStepInfo step1 = new SitefinityWizardStepInfo("SelectList", (Control) new SelectListStep());
      step1.Title = Res.Get<NewslettersResources>().SelectMailingListStepTitle;
      SitefinityWizardStepInfo step2 = new SitefinityWizardStepInfo("CampaignBasicInfo", (Control) new CampaignBasicInfo());
      step2.Title = Res.Get<NewslettersResources>().CampaignInfo;
      SitefinityWizardStepInfo step3 = new SitefinityWizardStepInfo("ChooseCampaignType", (Control) new CampaignTypeStep());
      step3.Title = Res.Get<NewslettersResources>().ChooseTheTypeOfCampaign;
      SitefinityWizardStepInfo step4 = new SitefinityWizardStepInfo("CampaignMessageStep", (Control) new CampaignMessageStep());
      step3.Title = Res.Get<NewslettersResources>().ComposeMessage;
      SitefinityWizardStepInfo step5 = new SitefinityWizardStepInfo("ConfirmCampaign", (Control) new ConfirmCampaign());
      step5.Title = Res.Get<NewslettersResources>().ConfirmCampaign;
      this.Wizard.AddStep(step1);
      this.Wizard.AddStep(step2);
      this.Wizard.AddStep(step3);
      this.Wizard.AddStep(step4);
      this.Wizard.AddStep(step5);
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard", this.ClientID);
      controlDescriptor.AddProperty("campaignServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Newsletters/Campaign.svc"));
      controlDescriptor.AddElementProperty("wizardTitleLabel", this.WizardTitleLabel.ClientID);
      controlDescriptor.AddProperty("wizardCreateTitle", (object) Res.Get<NewslettersResources>().CreateACampaign);
      controlDescriptor.AddProperty("wizardEditTitle", (object) Res.Get<NewslettersResources>().EditCampaign);
      controlDescriptor.AddProperty("_sendTestEmailsDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (TestEmailsForm).Name));
      controlDescriptor.AddComponentProperty("wizardControl", this.Wizard.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddProperty("_testMessageSentSuccessfully", (object) Res.Get<NewslettersResources>().TestMessageSentSuccessfully);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (CampaignWizard).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.CampaignWizard.js", typeof (CampaignWizard).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
