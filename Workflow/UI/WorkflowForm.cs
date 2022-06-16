// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>
  /// Dialog with the form for creating new and editing existing workflows.
  /// </summary>
  public class WorkflowForm : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowForm.ascx");
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.Scripts.WorkflowForm.js";
    private const string kendoScriptName = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Workflow/WorkflowDefinitionService.svc");
    private string bodyCssClass = "sfFormDialog";

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? WorkflowForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (WorkflowForm).FullName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Represents css class to be added to the body tag of the dialog
    /// </summary>
    public string BodyCssClass
    {
      get => this.bodyCssClass;
      set => this.bodyCssClass = value;
    }

    /// <summary>Gets the workflow title field</summary>
    protected virtual TextField WorkflowTitleField => this.Container.GetControl<TextField>("workflowTitleField", true);

    protected virtual ChoiceField EnableWorkflowField => this.Container.GetControl<ChoiceField>("enableWorkflowField", true);

    protected virtual ChoiceField AllowNotesField => this.Container.GetControl<ChoiceField>("allowNotesField", true);

    protected virtual ChoiceField AllowPublishersToSkipWorkflowField => this.Container.GetControl<ChoiceField>("allowPublishersToSkipWorkflowField", true);

    protected virtual ChoiceField AllowAdministratorsToSkipWorkflowField => this.Container.GetControl<ChoiceField>("allowAdministratorsToSkipWorkflowField", true);

    protected virtual Panel WorkflowPropertiesPanel => this.Container.GetControl<Panel>("workflowPropertiesPanel", true);

    protected virtual CommandBar WorkflowPropertiesCommandBar => this.Container.GetControl<CommandBar>("workflowPropertiesCommandBar", true);

    protected virtual HyperLink BackToWorkflowLink => this.Container.GetControl<HyperLink>("backToWorkflowLink", true);

    protected virtual WorkflowScopesGrid WorkflowScopesGrid => this.Container.GetControl<WorkflowScopesGrid>("workflowScopesGrid", true);

    protected virtual WorkflowApprovalGrid WorkflowApprovalGrid => this.Container.GetControl<WorkflowApprovalGrid>("workflowApprovalGrid", true);

    protected virtual HtmlControl WorkflowPropertiesDialogNewTitle => this.Container.GetControl<HtmlControl>("workflowPropertiesDialogNewTitle", true);

    protected virtual HtmlControl WorkflowPropertiesDialogEditTitle => this.Container.GetControl<HtmlControl>("workflowPropertiesDialogEditTitle", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("bodyCssClass", (object) this.BodyCssClass);
      controlDescriptor.AddProperty("webServiceUrl", (object) WorkflowForm.webServiceUrl);
      ApplicationRole applicationRole = Config.Get<SecurityConfig>().ApplicationRoles["Administrators"];
      controlDescriptor.AddProperty("adminRoleId", (object) applicationRole.Id);
      controlDescriptor.AddProperty("adminRoleName", (object) applicationRole.Name);
      controlDescriptor.AddElementProperty("backToWorkflowLink", this.BackToWorkflowLink.ClientID);
      controlDescriptor.AddElementProperty("workflowPropertiesPanel", this.WorkflowPropertiesPanel.ClientID);
      controlDescriptor.AddElementProperty("workflowPropertiesDialogNewTitle", this.WorkflowPropertiesDialogNewTitle.ClientID);
      controlDescriptor.AddElementProperty("workflowPropertiesDialogEditTitle", this.WorkflowPropertiesDialogEditTitle.ClientID);
      controlDescriptor.AddComponentProperty("workflowScopesGrid", this.WorkflowScopesGrid.ClientID);
      controlDescriptor.AddComponentProperty("workflowApprovalGrid", this.WorkflowApprovalGrid.ClientID);
      controlDescriptor.AddComponentProperty("workflowTitleField", this.WorkflowTitleField.ClientID);
      controlDescriptor.AddComponentProperty("allowPublishersToSkipWorkflowField", this.AllowPublishersToSkipWorkflowField.ClientID);
      controlDescriptor.AddComponentProperty("workflowPropertiesCommandBar", this.WorkflowPropertiesCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("enableWorkflowField", this.EnableWorkflowField.ClientID);
      controlDescriptor.AddComponentProperty("allowNotesField", this.AllowNotesField.ClientID);
      controlDescriptor.AddComponentProperty("allowPublishersToSkipWorkflowField", this.AllowPublishersToSkipWorkflowField.ClientID);
      controlDescriptor.AddComponentProperty("allowAdministratorsToSkipWorkflowField", this.AllowAdministratorsToSkipWorkflowField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (WorkflowForm).Assembly.FullName),
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.Scripts.WorkflowForm.js", typeof (WorkflowForm).Assembly.FullName)
    };
  }
}
