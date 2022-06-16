// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>Represents the view for configuring site modules.</summary>
  public class ConfigureModulesView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.ConfigureModulesView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.ConfigureModulesView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView" /> class.
    /// </summary>
    public ConfigureModulesView() => this.LayoutTemplatePath = ConfigureModulesView.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ConfigureModulesView).FullName;

    /// <summary>Gets the configure modules view wrapper.</summary>
    protected virtual HtmlGenericControl ConfigureModulesViewWrapper => this.Container.GetControl<HtmlGenericControl>("configureModulesViewWrapper", true);

    /// <summary>Gets a reference to the back link.</summary>
    protected virtual HtmlAnchor BackLink => this.Container.GetControl<HtmlAnchor>("backLink", true);

    /// <summary>Gets the dialog title label.</summary>
    protected virtual SitefinityLabel DialogTitle => this.Container.GetControl<SitefinityLabel>("dialogTitle", true);

    /// <summary>Gets the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the data sources table body.</summary>
    protected virtual HtmlGenericControl DataSourcesTableBody => this.Container.GetControl<HtmlGenericControl>("dataSourcesTableBody", true);

    /// <summary>Gets the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlGenericControl ButtonsPanel => this.Container.GetControl<HtmlGenericControl>("buttonsPanel", true);

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Gets a reference to the save button.</summary>
    protected virtual HtmlAnchor SaveButton => this.Container.GetControl<HtmlAnchor>("saveButton", true);

    /// <summary>Gets a reference to the cancel link.</summary>
    protected virtual HtmlAnchor CancelLink => this.Container.GetControl<HtmlAnchor>("cancelLink", true);

    /// <summary>Gets a reference to the create button.</summary>
    protected virtual HtmlAnchor CreateButton => this.Container.GetControl<HtmlAnchor>("createButton", true);

    /// <summary>Gets the content source selector dialog.</summary>
    protected virtual DataSourceProviderSelectorDialog DataSourceProviderSelectorDialog => this.Container.GetControl<DataSourceProviderSelectorDialog>("dataSourceProviderSelectorDialog", true);

    /// <summary>Gets the checkbox for selecting all data sources.</summary>
    protected virtual HtmlInputCheckBox AllDataSourcesCheckbox => this.Container.GetControl<HtmlInputCheckBox>("allDataSourcesCheckbox", true);

    /// <summary>
    /// Gets a reference to the site configuration mode container.
    /// </summary>
    protected virtual HtmlGenericControl SiteConfigurationModeContainer => this.Container.GetControl<HtmlGenericControl>("siteConfiguration", true);

    /// <summary>
    /// Gets a reference to the manually configured mode container.
    /// </summary>
    protected virtual HtmlGenericControl ManuallyConfiguredModeContainer => this.Container.GetControl<HtmlGenericControl>("manuallyConfigured", true);

    /// <summary>
    /// Gets a reference to the configured by deployment mode container.
    /// </summary>
    protected virtual HtmlGenericControl ConfiguredByDeploymentModeContainer => this.Container.GetControl<HtmlGenericControl>("configuredByDeployment", true);

    /// <summary>Gets a reference to configure manually link.</summary>
    protected virtual HtmlAnchor ConfigureManuallyBtn => this.Container.GetControl<HtmlAnchor>("configureManuallyBtn", true);

    /// <summary>Gets a reference to configure by deployment link.</summary>
    protected virtual HtmlAnchor ConfigureByDeploymentBtn => this.Container.GetControl<HtmlAnchor>("configureWithDeploymentBtn", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!PackagingOperations.IsMultisiteImportExportDisabled())
        return;
      this.SiteConfigurationModeContainer.Visible = false;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("configureModulesViewWrapper", this.ConfigureModulesViewWrapper.ClientID);
      controlDescriptor.AddElementProperty("dataSourcesTableBody", this.DataSourcesTableBody.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("createButton", this.CreateButton.ClientID);
      controlDescriptor.AddComponentProperty("dataSourceProviderSelectorDialog", this.DataSourceProviderSelectorDialog.ClientID);
      controlDescriptor.AddElementProperty("allDataSourcesCheckbox", this.AllDataSourcesCheckbox.ClientID);
      if (this.SiteConfigurationModeContainer.Visible)
      {
        controlDescriptor.AddElementProperty("manuallyConfiguredModeContainer", this.ManuallyConfiguredModeContainer.ClientID);
        controlDescriptor.AddElementProperty("configureByDeploymentBtn", this.ConfigureByDeploymentBtn.ClientID);
        controlDescriptor.AddElementProperty("configuredByDeploymentModeContainer", this.ConfiguredByDeploymentModeContainer.ClientID);
        controlDescriptor.AddElementProperty("configureManuallyBtn", this.ConfigureManuallyBtn.ClientID);
      }
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/Multisite/Multisite.svc/");
      controlDescriptor.AddProperty("webServiceUrl", (object) str);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.ConfigureModulesView.js", typeof (ConfigureModulesView).Assembly.FullName)
    };
  }
}
