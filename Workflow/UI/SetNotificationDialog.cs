// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.SetNotificationDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class SetNotificationDialog : AjaxDialogBase
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.Scripts.SetNotificationDialog.js";
    private static readonly string LayoutTemplateVppPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.SetNotificationDialog.ascx");

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SetNotificationDialog.LayoutTemplateVppPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (SetNotificationDialog).FullName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the workflow notify approvers choice field</summary>
    protected virtual ChoiceField NotifyApprovers => this.Container.GetControl<ChoiceField>("notifyApprovers", true);

    /// <summary>Gets the workflow notify administrators choice field</summary>
    protected virtual ChoiceField NotifyAdministrators => this.Container.GetControl<ChoiceField>("notifyAdministrators", true);

    /// <summary>Gets the workflow custom recipients choice field</summary>
    protected virtual MultilineTextField CustomRecipients => this.Container.GetControl<MultilineTextField>("customRecipients", true);

    protected virtual RadDialogManager DialogManager => this.Container.GetControl<RadDialogManager>("dialogManager", true);

    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    protected virtual Label SetNotificationDialogLabel => this.Container.GetControl<Label>("setNotificationDialogLabel", true);

    protected virtual Label SetNotificationDesc => this.Container.GetControl<Label>("setNotificationDesc", true);

    protected virtual ChoiceField CustomRecipientsCheck => this.Container.GetControl<ChoiceField>("customRecipientsCheck", true);

    /// <summary>Gets a reference to the label manager.</summary>
    /// <value>The label manager.</value>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Gets the error message wrapper.</summary>
    /// <value>The error message wrapper.</value>
    protected virtual HtmlContainerControl ErrorMessageWrapper => this.Container.GetControl<HtmlContainerControl>("errorMessageWrapper", true);

    /// <summary>Gets the error message label.</summary>
    /// <value>The error message label.</value>
    protected virtual Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("setNotificationDialogLabel", this.SetNotificationDialogLabel.ClientID);
      controlDescriptor.AddElementProperty("setNotificationDesc", this.SetNotificationDesc.ClientID);
      controlDescriptor.AddElementProperty("errorMessageWrapper", this.ErrorMessageWrapper.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddComponentProperty("customRecipientsCheck", this.CustomRecipientsCheck.ClientID);
      controlDescriptor.AddComponentProperty("customRecipients", this.CustomRecipients.ClientID);
      controlDescriptor.AddComponentProperty("notifyAdministrators", this.NotifyAdministrators.ClientID);
      controlDescriptor.AddComponentProperty("notifyApprovers", this.NotifyApprovers.ClientID);
      controlDescriptor.AddComponentProperty("dialogManager", this.DialogManager.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.Scripts.SetNotificationDialog.js", typeof (SetNotificationDialog).Assembly.FullName),
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.UI.Scripts.WorkflowUtils.js", Assembly.GetExecutingAssembly().FullName)
    };
  }
}
