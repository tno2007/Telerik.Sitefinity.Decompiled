// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class ScopeDefinitionDialog : AjaxDialogBase
  {
    private static readonly string WorkflowServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Workflow/WorkflowDefinitionService.svc/");
    private static readonly string MultisiteServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Multisite/Multisite.svc/");
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.Scripts.ScopeDefinitionDialog.js";
    private static readonly string LayoutTemplateVppPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.ScopeDefinitionDialog.ascx");

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ScopeDefinitionDialog.LayoutTemplateVppPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (ScopeDefinitionDialog).FullName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual RadDialogManager DialogManager => this.Container.GetControl<RadDialogManager>("dialogManager", true);

    protected virtual DropDownList LangScopeValues => this.Container.GetControl<DropDownList>("langScopeValues", false);

    protected virtual DropDownList SiteScopeValues => this.Container.GetControl<DropDownList>("siteScopeValues", false);

    protected virtual FlatSelector ItemSelector => this.Container.GetControl<FlatSelector>("itemSelector", true);

    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    protected virtual CheckBox ApplyToAllContentTypes => this.Container.GetControl<CheckBox>("applyToAllContentTypes", true);

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
      controlDescriptor.AddElementProperty("langScopeValues", this.LangScopeValues.ClientID);
      controlDescriptor.AddElementProperty("siteScopeValues", this.SiteScopeValues.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("errorMessageWrapper", this.ErrorMessageWrapper.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddElementProperty("applyToAllContentTypes", this.ApplyToAllContentTypes.ClientID);
      controlDescriptor.AddProperty("workflowServiceUrl", (object) ScopeDefinitionDialog.WorkflowServiceUrl);
      controlDescriptor.AddProperty("multisiteServiceUrl", (object) ScopeDefinitionDialog.MultisiteServiceUrl);
      CultureInfo[] frontendLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;
      bool flag = frontendLanguages != null && ((IEnumerable<CultureInfo>) frontendLanguages).Count<CultureInfo>() > 1;
      controlDescriptor.AddProperty("isMultilingual", (object) flag);
      controlDescriptor.AddProperty("isMultisiteMode", (object) !SystemManager.CurrentContext.IsOneSiteMode);
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      SiteGridViewModel siteGridViewModel = new SiteGridViewModel()
      {
        Id = currentSite.Id,
        Name = currentSite.Name,
        SiteMapRootNodeId = currentSite.SiteMapRootNodeId
      };
      controlDescriptor.AddProperty("currentSite", (object) siteGridViewModel);
      controlDescriptor.AddComponentProperty("dialogManager", this.DialogManager.ClientID);
      controlDescriptor.AddComponentProperty("itemSelector", this.ItemSelector.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.Scripts.ScopeDefinitionDialog.js", typeof (ScopeDefinitionDialog).Assembly.FullName),
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.UI.Scripts.WorkflowUtils.js", Assembly.GetExecutingAssembly().FullName)
    };
  }
}
