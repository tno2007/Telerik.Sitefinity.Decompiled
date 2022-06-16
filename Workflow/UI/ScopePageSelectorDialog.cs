// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class ScopePageSelectorDialog : AjaxDialogBase
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.Scripts.ScopePageSelectorDialog.js";
    private static readonly string LayoutTemplateVppPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.ScopePageSelectorDialog.ascx");
    private string bodyCssClass = "sfSelectorDialog";

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ScopePageSelectorDialog.LayoutTemplateVppPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (ScopePageSelectorDialog).FullName;

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

    /// <summary>Gets the reference to the page selector control</summary>
    protected virtual GenericPageSelector PageSelector => this.Container.GetControl<GenericPageSelector>("wfscope_pageSelector", true);

    /// <summary>
    /// Gets the reference to the apply workflow radio button selector
    /// </summary>
    protected virtual RadioButtonList ApplyWorkflowSelector => this.Container.GetControl<RadioButtonList>("wfscope_applyWorkflowSelector", true);

    /// <summary>Gets the reference to the selected pages panel</summary>
    protected virtual Panel SelectedPagesPanel => this.Container.GetControl<Panel>("wfscope_selectedPagesPanel", true);

    /// <summary>Gets the "ApplyToChildPagesCheckbox" control</summary>
    protected virtual CheckBox ApplyToChildPagesCheckbox => this.Container.GetControl<CheckBox>("wfscope_applyToChildPagesCheckbox", true);

    /// <summary>Gets the done button control</summary>
    protected virtual HtmlAnchor DoneButton => this.Container.GetControl<HtmlAnchor>("doneButton", true);

    /// <summary>Gets cancel button control</summary>
    protected virtual HtmlAnchor CancelButton => this.Container.GetControl<HtmlAnchor>("cancelButton", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container) => this.PageSelector.TreeViewCssClass = "sfTreeView";

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("bodyCssClass", (object) this.BodyCssClass);
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddElementProperty("applyWorkflowSelector", this.ApplyWorkflowSelector.ClientID);
      controlDescriptor.AddElementProperty("selectedPagesPanel", this.SelectedPagesPanel.ClientID);
      controlDescriptor.AddElementProperty("applyToChildPagesCheckbox", this.ApplyToChildPagesCheckbox.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.Scripts.ScopePageSelectorDialog.js", typeof (ScopePageSelectorDialog).Assembly.FullName)
    };
  }
}
