// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog for selecting providers for a data source.</summary>
  public class DataSourceProviderSelectorDialog : KendoWindow
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.DataSourceProviderSelectorDialog.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.DataSourceProviderSelectorDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog" /> class.
    /// </summary>
    public DataSourceProviderSelectorDialog() => this.LayoutTemplatePath = DataSourceProviderSelectorDialog.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a reference to the outer div containing the window content.
    /// </summary>
    /// <value></value>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("dataSourceProviderSelectorDialogWrapper", true);

    /// <summary>Gets the dialog title.</summary>
    protected virtual Label DialogTitle => this.Container.GetControl<Label>("dialogTitle", true);

    /// <summary>Gets the cancel button.</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancelSelecting", true);

    /// <summary>Gets the done button.</summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the add provider button.</summary>
    protected virtual LinkButton AddProviderButton => this.Container.GetControl<LinkButton>("lnkAddProvider", true);

    /// <summary>Gets the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the sources grid.</summary>
    protected virtual HtmlContainerControl SourcesGrid => this.Container.GetControl<HtmlContainerControl>("sourcesGrid", true);

    /// <summary>Gets the search box.</summary>
    protected virtual BackendSearchBox SearchBox => this.Container.GetControl<BackendSearchBox>("searchBox", true);

    /// <summary>Gets the checkbox for selecting all sources.</summary>
    protected virtual HtmlInputCheckBox SelectAllSourcesCheckbox => this.Container.GetControl<HtmlInputCheckBox>("selectAllSourcesCheckbox", true);

    /// <summary>Gets the delete provider confirm dialog.</summary>
    protected virtual PromptDialog DisableProviderConfirmDialog => this.Container.GetControl<PromptDialog>("disableProviderConfirmDialog", true);

    /// <summary>Gets the content source selector dialog.</summary>
    protected virtual DataSourceCreateProviderDialog DataSourceCreateProviderDialog => this.Container.GetControl<DataSourceCreateProviderDialog>("dataSourceCreateProviderDialog", true);

    /// <summary>Gets the error message wrapper.</summary>
    /// <value>The error message wrapper.</value>
    protected virtual HtmlContainerControl ErrorMessageWrapper => this.Container.GetControl<HtmlContainerControl>("errorMessageWrapper", true);

    /// <summary>Gets the error message label.</summary>
    /// <value>The error message label.</value>
    protected virtual Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/Multisite/Multisite.svc/");
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("addProviderButton", this.AddProviderButton.ClientID);
      controlDescriptor.AddElementProperty("sourcesGrid", this.SourcesGrid.ClientID);
      controlDescriptor.AddElementProperty("selectAllSourcesCheckbox", this.SelectAllSourcesCheckbox.ClientID);
      controlDescriptor.AddElementProperty("errorMessageWrapper", this.ErrorMessageWrapper.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("disableProviderConfirmDialog", this.DisableProviderConfirmDialog.ClientID);
      controlDescriptor.AddComponentProperty("dataSourceCreateProviderDialog", this.DataSourceCreateProviderDialog.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (DataSourceProviderSelectorDialog).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.ExpandableLabel.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.DataSourceProviderSelectorDialog.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
