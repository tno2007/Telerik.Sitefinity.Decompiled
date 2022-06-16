// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>Workflow Scopes Grid represented as field</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class WorkflowScopesGrid : FieldControl
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.UI.Scripts.WorkflowScopesGrid.js";
    private const string LayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowScopesGrid.ascx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid" /> class.
    /// </summary>
    public WorkflowScopesGrid() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowScopesGrid.ascx");

    /// <summary>Gets the Dialog manager helper</summary>
    protected internal RadDialogManager DialogManager => this.Container.GetControl<RadDialogManager>("dialogManager", true);

    /// <summary>Gets a reference to the label manager.</summary>
    /// <value>The label manager.</value>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Gets a reference to the label for adding scope.</summary>
    /// <value>The label.</value>
    protected virtual Label AddScopeBtnLabel => this.Container.GetControl<Label>("addScopeBtnLabel", true);

    /// <summary>Gets the reference to the send button.</summary>
    protected virtual HtmlGenericControl ScopesErrorMessage => this.Container.GetControl<HtmlGenericControl>("scopesErrorMessage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    /// <example>
    /// The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoAll;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("addScopeBtnLabel", this.AddScopeBtnLabel.ClientID);
      controlDescriptor.AddElementProperty("scopesErrorMessage", this.ScopesErrorMessage.ClientID);
      CultureInfo[] frontendLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;
      bool flag = frontendLanguages != null && ((IEnumerable<CultureInfo>) frontendLanguages).Count<CultureInfo>() > 1;
      controlDescriptor.AddProperty("isMultilingual", (object) flag);
      controlDescriptor.AddProperty("isMultisiteMode", (object) !SystemManager.CurrentContext.IsOneSiteMode);
      Telerik.Sitefinity.Multisite.ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      SiteGridViewModel siteGridViewModel = new SiteGridViewModel()
      {
        Id = currentSite.Id,
        Name = currentSite.Name,
        SiteMapRootNodeId = currentSite.SiteMapRootNodeId
      };
      controlDescriptor.AddProperty("currentSite", (object) siteGridViewModel);
      controlDescriptor.AddComponentProperty("dialogManager", this.DialogManager.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.UI.Scripts.WorkflowScopesGrid.js", typeof (WorkflowScopesGrid).Assembly.FullName),
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.UI.Scripts.WorkflowUtils.js", Assembly.GetExecutingAssembly().FullName)
    };
  }
}
