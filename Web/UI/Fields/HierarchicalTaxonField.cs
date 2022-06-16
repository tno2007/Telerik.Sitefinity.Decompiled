// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A field control for representing and editing hierarchical taxon properties
  /// </summary>
  [FieldDefinitionElement(typeof (HierarchicalTaxonFieldDefinitionElement))]
  public class HierarchicalTaxonField : TaxonField, IExpandableControl
  {
    private bool? expanded = new bool?(true);
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.HierarchicalTaxonField.ascx");
    private const string hierarchicalTaxonFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.HierarchicalTaxonField.js";
    private const string predecessorWebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/";
    private object value;

    /// <summary>
    /// Gets or sets the value of the property.
    /// The value should be either a Guid or a IList of Guids that represent the taxon pageId's
    /// </summary>
    /// <value>The value.</value>
    public override object Value
    {
      get => this.BindOnServer ? (object) this.TaxaSelector.SelectedTaxa : this.value;
      set
      {
        this.value = value;
        if (!this.BindOnServer || this.TaxaSelector == null)
          return;
        this.TaxaSelector.SelectedTaxa = value as TrackedList<Guid>;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    public bool AllowRootSelection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done-selecting button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the done-selecting button; otherwise, <c>false</c>.
    /// </value>
    public bool ShowDoneSelectingButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the create-new-taxon button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the create-new-taxon button; otherwise, <c>false</c>.
    /// </value>
    public bool ShowCreateNewTaxonButton { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootTaxonID { get; set; }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandButton;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.GetConditionalControl<WebControl>("expandTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets or sets a value indicating whether the Web server control is enabled.
    /// </summary>
    /// <returns>true if control is enabled; otherwise, false. The default is true.</returns>
    public override bool Enabled
    {
      get => base.Enabled;
      set
      {
        this.ChangeSelectedTaxaButton.Enabled = value;
        this.CreateTaxonButton.Enabled = value;
        base.Enabled = value;
        if (base.Enabled)
          return;
        this.TaxaSelector.HideSelectorButtons();
        this.ShowDoneSelectingButton = false;
        this.ShowCreateNewTaxonButton = false;
      }
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = HierarchicalTaxonField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label that displays the description of the hierarchical taxon field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.GetConditionalControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displayes the title of the hierarchical taxon field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the example of the heirarchical taxon field.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.GetConditionalControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the expand button.</summary>
    /// <value>The expand button.</value>
    protected internal virtual LinkButton ExpandButton => this.GetConditionalControl<LinkButton>("expandButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the link button which is used to change the selected taxa.
    /// </summary>
    protected internal virtual LinkButton ChangeSelectedTaxaButton => this.GetConditionalControl<LinkButton>("changeSelectedTaxaButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the link button which is used to open the dialog for creating a new
    /// taxon.
    /// </summary>
    protected internal virtual LinkButton CreateTaxonButton => this.GetConditionalControl<LinkButton>("createTaxonButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the web control which is displayed when user has selected a taxa.
    /// </summary>
    protected internal virtual Panel SelectedTaxaPanel => this.GetConditionalControl<Panel>("selectedTaxaPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to hierarchical selector used to select hierarchical taxa.
    /// </summary>
    protected internal virtual HierarchicalTaxonSelector TaxaSelector => this.GetConditionalControl<HierarchicalTaxonSelector>("taxaSelector", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the the client binder that binds the selected taxa.
    /// </summary>
    /// <value></value>
    protected internal override ClientBinder SelectedTaxaBinder => this.GetConditionalControl<ClientBinder>("selectedTaxaBinder", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.TitleLabel != null)
      {
        if (string.IsNullOrEmpty(this.Title))
          this.TitleLabel.SetTextOrHide(HttpUtility.HtmlEncode((string) this.Taxonomy.Title));
        else
          this.TitleLabel.SetTextOrHide(HttpUtility.HtmlEncode(this.Title), this.ResourceClassId);
      }
      if (this.DescriptionLabel != null)
        this.DescriptionLabel.SetTextOrHide(HttpUtility.HtmlEncode(this.Description), this.ResourceClassId);
      if (this.ExampleLabel != null)
        this.ExampleLabel.SetTextOrHide(this.Example, this.ResourceClassId);
      if (this.TaxaSelector != null)
      {
        this.TaxaSelector.BindOnServer = this.BindOnServer;
        if (this.BindOnServer)
        {
          this.TaxaSelector.ConfigureSelector((HierarchicalTaxonomy) this.Taxonomy, this.TaxonomyProvider);
          this.TaxaSelector.AllowMultipleSelection = this.AllowMultipleSelection;
          this.TaxaSelector.TabIndex = this.TabIndex;
          this.TaxaSelector.AllowRootSelection = this.AllowRootSelection;
          this.TaxaSelector.ShowCreateNewTaxonButton = false;
          this.TaxaSelector.ShowDoneSelectingButton = false;
          this.TaxaSelector.RootTaxonID = this.RootTaxonID;
        }
        else
        {
          this.TaxaSelector.ConfigureSelector((HierarchicalTaxonomy) this.Taxonomy, this.TaxonomyProvider);
          this.TaxaSelector.AllowMultipleSelection = this.AllowMultipleSelection;
          this.TaxaSelector.TabIndex = this.TabIndex;
          this.TaxaSelector.AllowRootSelection = this.AllowRootSelection;
          this.TaxaSelector.ShowCreateNewTaxonButton = this.ShowCreateNewTaxonButton;
          this.TaxaSelector.ShowDoneSelectingButton = this.ShowDoneSelectingButton;
          this.TaxaSelector.RootTaxonID = this.RootTaxonID;
        }
      }
      if (this.SelectedTaxaPanel != null)
        this.SelectedTaxaPanel.Style.Add("display", "none");
      if (this.ExpandControl != null && this.ExpandTarget != null)
      {
        this.ConfigureExpandableControl(this.ResourceClassId);
        bool? expanded1 = this.Expanded;
        this.Expanded = this.DisplayMode == FieldDisplayMode.Write ? expanded1 : new bool?(false);
        if (!this.Expanded.GetValueOrDefault())
          this.ExpandControl.TabIndex = this.TabIndex;
        if (this.DisplayMode == FieldDisplayMode.Read)
        {
          this.ExpandControl.Style.Add("display", "none");
          this.ExpandTarget.Style.Add("display", "none");
          this.TaxaSelector.Hidden = true;
        }
        else
        {
          bool? expanded2 = this.Expanded;
          if (expanded2.HasValue)
          {
            expanded2 = this.Expanded;
            if (!expanded2.Value)
              this.TaxaSelector.Hidden = true;
          }
        }
      }
      this.TabIndex = (short) 0;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IHierarchicalTaxonFieldDefinition taxonFieldDefinition))
        return;
      this.ResourceClassId = definition.ResourceClassId;
      bool? nullable = taxonFieldDefinition.AllowRootSelection;
      int num1;
      if (nullable.HasValue)
      {
        nullable = taxonFieldDefinition.AllowRootSelection;
        num1 = nullable.Value ? 1 : 0;
      }
      else
        num1 = 0;
      this.AllowRootSelection = num1 != 0;
      nullable = taxonFieldDefinition.ShowDoneSelectingButton;
      int num2;
      if (nullable.HasValue)
      {
        nullable = taxonFieldDefinition.ShowDoneSelectingButton;
        num2 = nullable.Value ? 1 : 0;
      }
      else
        num2 = 0;
      this.ShowDoneSelectingButton = num2 != 0;
      nullable = taxonFieldDefinition.ShowCreateNewTaxonButton;
      int num3;
      if (nullable.HasValue)
      {
        nullable = taxonFieldDefinition.ShowCreateNewTaxonButton;
        if (nullable.Value)
        {
          num3 = 1;
          goto label_11;
        }
      }
      num3 = this.AllowCreating ? 1 : 0;
label_11:
      this.ShowCreateNewTaxonButton = num3 != 0;
      this.RootTaxonID = taxonFieldDefinition.RootTaxonID;
      if (taxonFieldDefinition.ExpandableDefinition == null)
        return;
      this.ConfigureExpandableControl(taxonFieldDefinition.ExpandableDefinition);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && this.BindOnServer)
        return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor scriptDescriptor = this.GetLastScriptDescriptor();
      string empty = string.Empty;
      string absolute = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/");
      scriptDescriptor.AddProperty("_predecessorWebServiceUrl", (object) absolute);
      if (this.ChangeSelectedTaxaButton != null)
        scriptDescriptor.AddElementProperty("changeSelectedTaxaButton", this.ChangeSelectedTaxaButton.ClientID);
      if (this.CreateTaxonButton != null)
        scriptDescriptor.AddElementProperty("createTaxonButton", this.CreateTaxonButton.ClientID);
      if (this.SelectedTaxaPanel != null)
        scriptDescriptor.AddElementProperty("selectedTaxaPanel", this.SelectedTaxaPanel.ClientID);
      if (this.TaxaSelector != null)
        scriptDescriptor.AddComponentProperty("taxaSelector", this.TaxaSelector.ClientID);
      scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      if (this.DisplayMode == FieldDisplayMode.Write && !this.IsBackend())
      {
        IList<Guid> guidList = this.Value as IList<Guid>;
        scriptDescriptor.AddProperty("selectedFrontEndItemIds", (object) guidList);
      }
      scriptDescriptorList.Add((ScriptDescriptor) scriptDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && this.BindOnServer)
        return (IEnumerable<ScriptReference>) new ScriptReference[0];
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        this.GetExpandableExtenderScript(),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.HierarchicalTaxonField.js", typeof (HierarchicalTaxonField).Assembly.FullName)
      };
    }

    protected internal virtual ScriptControlDescriptor GetLastScriptDescriptor() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
  }
}
