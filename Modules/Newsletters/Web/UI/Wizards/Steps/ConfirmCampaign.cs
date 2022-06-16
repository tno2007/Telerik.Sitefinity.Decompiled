// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps
{
  /// <summary>
  /// Last step of the campaign wizard that lets user see all the relevant information and save the
  /// campaign.
  /// </summary>
  public class ConfirmCampaign : SitefinityWizardStepControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.Steps.ConfirmCampaign.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.ConfirmCampaign.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ConfirmCampaign.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the save campaign button</summary>
    protected virtual LinkButton SaveCampaignButton => this.Container.GetControl<LinkButton>("saveCampaignButton", true);

    /// <summary>Gets the reference to the preview campaign button</summary>
    protected virtual LinkButton PreviewCampaignButton => this.Container.GetControl<LinkButton>("previewCampaignButton", true);

    /// <summary>Gets the reference to the send test button</summary>
    protected virtual LinkButton SendTestButton => this.Container.GetControl<LinkButton>("sendTestButton", true);

    /// <summary>Gets the reference to the send button</summary>
    protected virtual LinkButton SendButton => this.Container.GetControl<LinkButton>("sendButton", true);

    /// <summary>Gets the reference to the schedule delivery button</summary>
    protected virtual LinkButton ScheduleDeliveryButton => this.Container.GetControl<LinkButton>("scheduleDeliveryButton", true);

    /// <summary>Gets the reference to the discard campaign button</summary>
    protected virtual LinkButton DiscardCampaignButton => this.Container.GetControl<LinkButton>("discardCampaignButton", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control for sending the test emails.
    /// </summary>
    protected virtual PromptDialog SendTestPrompt => this.Container.GetControl<PromptDialog>("sendTestPrompt", true);

    /// <summary>Gets the reference to the schedule campaign dialog.</summary>
    protected virtual RadWindow ScheduleCampaignDialog => this.Container.GetControl<RadWindow>("scheduleCampaignDialog", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Count<ScriptDescriptor>() != 0 ? (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>() : new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("saveCampaignButton", this.SaveCampaignButton.ClientID);
      controlDescriptor.AddElementProperty("previewCampaignButton", this.PreviewCampaignButton.ClientID);
      controlDescriptor.AddElementProperty("sendTestButton", this.SendTestButton.ClientID);
      controlDescriptor.AddElementProperty("sendButton", this.SendButton.ClientID);
      controlDescriptor.AddElementProperty("scheduleDeliveryButton", this.ScheduleDeliveryButton.ClientID);
      controlDescriptor.AddComponentProperty("scheduleCampaignDialog", this.ScheduleCampaignDialog.ClientID);
      controlDescriptor.AddElementProperty("discardCampaignButton", this.DiscardCampaignButton.ClientID);
      controlDescriptor.AddComponentProperty("sendTestPrompt", this.SendTestPrompt.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.ConfirmCampaign.js", typeof (ConfirmCampaign).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
