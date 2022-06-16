// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class WorkflowTypeSelectorDialog : AjaxDialogBase
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.Scripts.WorkflowTypeSelectorDialog.js";
    private static readonly string LayoutTemplateVppPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowTypeSelectorDialog.ascx");
    private string bodyCssClass = "sfSelectorDialog";

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? WorkflowTypeSelectorDialog.LayoutTemplateVppPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (WorkflowTypeSelectorDialog).FullName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the css class to be added to the body tag of the dialog
    /// </summary>
    public string BodyCssClass
    {
      get => this.bodyCssClass;
      set => this.bodyCssClass = value;
    }

    protected virtual HtmlAnchor DoneButton => this.Container.GetControl<HtmlAnchor>("doneButton", true);

    protected virtual HtmlAnchor CancelButton => this.Container.GetControl<HtmlAnchor>("cancelButton", true);

    protected virtual RadioButtonList LevelsOfApprovalSelector => this.Container.GetControl<RadioButtonList>("workflowTypesField", true);

    /// <summary>Gets a reference to the label manager.</summary>
    /// <value>The label manager.</value>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container) => this.PopulateRadioButtonList();

    /// <summary>Sets the list items of the RadioButtonList</summary>
    private void PopulateRadioButtonList()
    {
      this.LevelsOfApprovalSelector.Items.Add(new ListItem()
      {
        Text = Res.Get<WorkflowResources>().OneLevelOfApproval,
        Value = "1"
      });
      this.LevelsOfApprovalSelector.Items.Add(new ListItem()
      {
        Text = Res.Get<WorkflowResources>().TwoLevelsOfApproval,
        Value = "2"
      });
      this.LevelsOfApprovalSelector.Items.Add(new ListItem()
      {
        Text = Res.Get<WorkflowResources>().ThreeLevelsOfApproval,
        Value = "4"
      });
      this.LevelsOfApprovalSelector.Items.Add(new ListItem()
      {
        Text = Res.Get<WorkflowResources>().NoApprovalWorkflow.Arrange((object) Res.Get<WorkflowResources>().ExternalLinkNoApprovalWorkflow),
        Value = "0"
      });
      this.LevelsOfApprovalSelector.SelectedValue = "1";
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("bodyCssClass", (object) this.BodyCssClass);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("levelsOfApprovalSelector", this.LevelsOfApprovalSelector.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.Scripts.WorkflowTypeSelectorDialog.js", typeof (WorkflowTypeSelectorDialog).Assembly.FullName)
    };
  }
}
