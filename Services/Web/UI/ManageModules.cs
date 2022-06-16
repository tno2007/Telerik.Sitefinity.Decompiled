// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.UI.ManageModules
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Services.Web.UI
{
  /// <summary>Represents the module management backend UI</summary>
  public class ManageModules : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Services.Web.UI.Scripts.ManageModules.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModulesAndServices.Modules.ManageModules.ascx");
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/ModulesService");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ManageModules.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to the campaign preview window.</summary>
    protected virtual ModuleDetailsWindow ModuleDetailsWindow => this.Container.GetControl<ModuleDetailsWindow>("moduleDetailsWindow", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("modulesManagementGrid", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the base title of the type editor
    /// </summary>
    protected virtual HiddenField TypeEditorTitleLabel => this.Container.GetControl<HiddenField>("typeEditorTitleLabel", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the base title of the delete module window.
    /// </summary>
    protected virtual HiddenField DeleteModuleTitle => this.Container.GetControl<HiddenField>("deleteModuleTitleHidden", true);

    /// <summary>
    /// Gets the reference to the uninstall module confirmation dialog
    /// </summary>
    protected virtual PromptDialog UninstallConfirmationDialog => this.Container.GetControl<PromptDialog>("uninstallConfirmationDialog", true);

    /// <summary>
    /// Gets the reference to the deactivate module confirmation dialog
    /// </summary>
    protected virtual PromptDialog DeactivateConfirmationDialog => this.Container.GetControl<PromptDialog>("deactivateConfirmationDialog", true);

    /// <summary>
    /// Gets the reference to the delete module confirmation dialog
    /// </summary>
    protected virtual PromptDialog DeleteConfirmationDialog => this.Container.GetControl<PromptDialog>("deleteConfirmationDialog", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the reference to the license restriction dialog</summary>
    protected virtual PromptDialog LicenseRestrictionDialog => this.Container.GetControl<PromptDialog>("licenseRestrictionDialog", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

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
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      string str = ManageModules.webServiceUrl + "/modules";
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddComponentProperty("moduleDetailsWindow", this.ModuleDetailsWindow.ClientID);
      controlDescriptor.AddComponentProperty("uninstallConfirmationDialog", this.UninstallConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("deactivateConfirmationDialog", this.DeactivateConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("deleteConfirmationDialog", this.DeleteConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("licenseRestrictionDialog", this.LicenseRestrictionDialog.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Services.Web.UI.Scripts.ManageModules.js", this.GetType().Assembly.FullName)
    };
  }
}
