// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ExternalPageField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Selectors;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a control to select an external page that might be either a page from the site tree or an external link.
  /// </summary>
  [RequiresDataItem]
  public class ExternalPageField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ExternalPageField.ascx");
    internal const string externalPageFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ExternalPageField.js";
    internal const string requiresDataItemScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    internal const string localizableFieldControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ExternalPageField" /> class.
    /// </summary>
    public ExternalPageField() => this.LayoutTemplatePath = ExternalPageField.layoutTemplatePath;

    public virtual IChoiceFieldDefinition IsExternalPageChoiceFieldDefinition { get; set; }

    /// <summary>The guid of the site page to be redirected to.</summary>
    private Guid InternalPageId { get; set; }

    /// <summary>The url of the external page to be redirected to.</summary>
    public virtual ITextFieldDefinition ExternalPageUrlFieldDefinition { get; set; }

    /// <summary>If set the page will be open in a new browser window.</summary>
    public virtual IChoiceFieldDefinition OpenInNewWindowChoiceFieldDefinition { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the field.
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected internal override WebControl DescriptionControl => (WebControl) null;

    protected internal override WebControl ExampleControl => (WebControl) null;

    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the is-external-page choice field.
    /// </summary>
    protected internal virtual ChoiceField IsExternalPageChoiceField => this.Container.GetControl<ChoiceField>("isExternalPageChoiceField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label displaying the currently selected url label.
    /// </summary>
    protected internal virtual HtmlGenericControl CurrentExternalLinkLabel => this.Container.GetControl<HtmlGenericControl>("currentExternalLinkLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the grouping tag around all the controls that are used when the page is marked as external.
    /// </summary>
    public virtual HtmlGenericControl ExternalPageControlsGroup => this.Container.GetControl<HtmlGenericControl>("externalPageControlsGroup", true);

    /// <summary>Gets the reference to the open dialog link.</summary>
    public virtual HtmlAnchor OpenDialogLink => this.Container.GetControl<HtmlAnchor>(nameof (OpenDialogLink), true);

    /// <summary>Gets the reference to the view external page link.</summary>
    public virtual HtmlAnchor ViewExternalPageLink => this.Container.GetControl<HtmlAnchor>(nameof (ViewExternalPageLink), true);

    /// <summary>
    /// Gets the reference to the DIV tag where the popup dialog resides.
    /// </summary>
    public virtual HtmlGenericControl DialogBoxDiv => this.Container.GetControl<HtmlGenericControl>("dialogBoxDiv", true);

    /// <summary>
    /// Gets the reference to the GenericPageSelector control.
    /// </summary>
    public virtual GenericPageSelector InternalPageSelector => this.Container.GetControl<GenericPageSelector>("internalSelector", true);

    /// <summary>
    /// Gets the reference to the ExternalPagesSelector control.
    /// </summary>
    public virtual ExternalPagesSelector ExternalPageSelector => this.Container.GetControl<ExternalPagesSelector>("externalSelector", true);

    /// <summary>
    /// Gets the reference to the page type choice field selector.
    /// </summary>
    public virtual ChoiceField PageTypeSelector => this.Container.GetControl<ChoiceField>("pageTypeSelector", true);

    /// <summary>
    /// Gets the reference to the multipage with the 2 possible option panels.
    /// </summary>
    public virtual RadMultiPage OptionsMultipage => this.Container.GetControl<RadMultiPage>("optionsMultipage", true);

    /// <summary>Gets the reference to the Done selecting button.</summary>
    public virtual LinkButton LnkDoneSelecting => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>Gets the reference to the Cancel button.</summary>
    public virtual LinkButton LnkCancel => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets a reference to the client label manager in the control.
    /// </summary>
    /// <value>The client label manager.</value>
    private ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.IsExternalPageChoiceField.Configure((IFieldDefinition) this.IsExternalPageChoiceFieldDefinition);
      this.ExternalPageSelector.UrlTextBox.Configure((IFieldDefinition) this.ExternalPageUrlFieldDefinition);
      this.ExternalPageSelector.OpenInNewWindowChoiceField.Configure((IFieldDefinition) this.OpenInNewWindowChoiceFieldDefinition);
      this.InternalPageSelector.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      this.InternalPageSelector.SetConstantFilter((string) null);
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureFields((IExternalPageFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="externalPageFieldDefinition">The external page field definition.</param>
    public virtual void ConfigureFields(
      IExternalPageFieldDefinition externalPageFieldDefinition)
    {
      this.IsExternalPageChoiceFieldDefinition = externalPageFieldDefinition.IsExternalPageChoiceFieldDefinition;
      this.ExternalPageUrlFieldDefinition = externalPageFieldDefinition.ExternalPageUrlFieldDefinition;
      this.OpenInNewWindowChoiceFieldDefinition = externalPageFieldDefinition.OpenInNewWindowChoiceFieldDefinition;
      this.InternalPageId = externalPageFieldDefinition.InternalPageId;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode != FieldDisplayMode.Write)
        throw new NotImplementedException("Read mode for this control is not implemented.");
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("isExternalPageChoiceField", this.IsExternalPageChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("internalPageSelector", this.InternalPageSelector.ClientID);
      controlDescriptor.AddComponentProperty("externalPageSelector", this.ExternalPageSelector.ClientID);
      controlDescriptor.AddElementProperty("currentExternalLinkLabel", this.CurrentExternalLinkLabel.ClientID);
      controlDescriptor.AddElementProperty("externalPageControlsGroup", this.ExternalPageControlsGroup.ClientID);
      controlDescriptor.AddElementProperty("openDialogLink", this.OpenDialogLink.ClientID);
      controlDescriptor.AddElementProperty("viewExternalPageLink", this.ViewExternalPageLink.ClientID);
      controlDescriptor.AddElementProperty("dialogBoxDiv", this.DialogBoxDiv.ClientID);
      controlDescriptor.AddComponentProperty("pageTypeSelector", this.PageTypeSelector.ClientID);
      controlDescriptor.AddComponentProperty("optionsMultipage", this.OptionsMultipage.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.LnkDoneSelecting.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.LnkCancel.ClientID);
      controlDescriptor.AddProperty("_siteRootPath", (object) SystemManager.CurrentHttpContext.Request.ApplicationPath);
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
      string fullName = typeof (ExternalPageField).Assembly.FullName;
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ExternalPageField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", name)
      };
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
