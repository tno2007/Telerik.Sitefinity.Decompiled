// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog
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
  /// <summary>A dialog for creating providers for a data source.</summary>
  public class DataSourceCreateProviderDialog : KendoWindow
  {
    private static readonly string DialogLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.DataSourceCreateProviderDialog.ascx");
    internal const string ScriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.DataSourceCreateProviderDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog" /> class.
    /// </summary>
    public DataSourceCreateProviderDialog() => this.LayoutTemplatePath = DataSourceCreateProviderDialog.DialogLayoutTemplatePath;

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
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("dataSourceCreateProviderDialogWrapper", true);

    /// <summary>Gets the dialog title.</summary>
    protected virtual Label DialogTitle => this.Container.GetControl<Label>("dialogTitle", true);

    /// <summary>Gets the provider title.</summary>
    protected virtual TextBox ProviderName => this.Container.GetControl<TextBox>("providerName", true);

    /// <summary>Gets the cancel button.</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancelSelecting", true);

    /// <summary>Gets the done button.</summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the error message wrapper.</summary>
    /// <value>The error message wrapper.</value>
    protected virtual HtmlContainerControl ErrorMessageWrapper => this.Container.GetControl<HtmlContainerControl>("errorMessageWrapper", true);

    /// <summary>Gets the error message label.</summary>
    /// <value>The error message label.</value>
    protected virtual Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container) => base.InitializeControls(container);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("providerName", this.ProviderName.ClientID);
      controlDescriptor.AddElementProperty("errorMessageWrapper", this.ErrorMessageWrapper.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      List<System.Web.UI.ScriptReference> scriptReferences = new List<System.Web.UI.ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (DataSourceCreateProviderDialog).Assembly.FullName;
      scriptReferences.Add(new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.ExpandableLabel.js", fullName));
      scriptReferences.Add(new System.Web.UI.ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.DataSourceCreateProviderDialog.js", fullName));
      return (IEnumerable<System.Web.UI.ScriptReference>) scriptReferences;
    }
  }
}
