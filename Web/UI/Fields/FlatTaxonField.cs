// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Data;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A field control for representing and editing flat taxon properties
  /// </summary>
  [FieldDefinitionElement(typeof (FlatTaxonFieldDefinitionElement))]
  public class FlatTaxonField : TaxonField, IExpandableControl
  {
    private int initialTaxaCount = -1;
    private bool? expanded = new bool?(false);
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.FlatTaxonField.ascx");
    private const string flatTaxonFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FlatTaxonField.js";
    private const string inputHintVisibleClass = "sfInputHintVisible";
    private const string inputHintHiddenClass = "sfInputHintHidden";
    private object value;
    private TrackedList<Guid> selectedTaxa;
    private const int DefaultTaxonsCount = 30;

    /// <summary>
    /// Gets or sets the value of the property.
    /// The value should be either a Guid or a IList of Guids that represent the taxon pageId's
    /// </summary>
    /// <value>The value.</value>
    public override object Value
    {
      get
      {
        if (!this.BindOnServer || this.DisplayMode != FieldDisplayMode.Write)
          return this.value;
        if (this.selectedTaxa == null)
          this.selectedTaxa = new TrackedList<Guid>();
        this.selectedTaxa.Clear();
        foreach (ControlItem checkedNode in (IEnumerable<RadTreeNode>) this.FlatTaxaTree.CheckedNodes)
          this.selectedTaxa.Add(new Guid(checkedNode.Value));
        return (object) this.selectedTaxa;
      }
      set
      {
        this.value = value;
        if (!this.BindOnServer || this.DisplayMode != FieldDisplayMode.Write || this.value == null)
          return;
        this.selectedTaxa = value as TrackedList<Guid>;
        this.FlatTaxaTree.ClearCheckedNodes();
        foreach (Guid selectedTaxon in this.selectedTaxa)
          this.FlatTaxaTree.FindNodeByValue(selectedTaxon.ToString()).Checked = true;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether the Web server control is enabled.
    /// </summary>
    /// <returns>true if control is enabled; otherwise, false. The default is true.</returns>
    public override bool Enabled
    {
      get => base.Enabled;
      set
      {
        base.Enabled = value;
        if (base.Enabled)
          return;
        this.ShowAllTaxaButton.Style.Add("display", "none");
        this.ExpandControl.Style.Add("display", "none");
        this.ExpandTarget.Style.Add("display", "none");
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
          base.LayoutTemplatePath = FlatTaxonField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    private int InitialTaxaCount
    {
      get
      {
        if (this.initialTaxaCount < 0)
          this.initialTaxaCount = this.GetTotalTaxaCount();
        return this.initialTaxaCount;
      }
    }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    public WebControl ExpandControl => (WebControl) this.ExpandButton;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    public WebControl ExpandTarget => this.GetConditionalControl<WebControl>("expandTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that displays a server-bound list of tags.
    /// </summary>
    public WebControl BindOnServerPanel => this.GetConditionalControl<WebControl>("bindOnServerPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the FlatTaxaTree tree view that displays a server-bound list of tags.
    /// </summary>
    public RadTreeView FlatTaxaTree => this.GetConditionalControl<RadTreeView>("flatTaxaTree", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the title of the most popular existing taxa panel.
    /// </summary>
    protected string MostPopularTaxaTitle => Res.Get<Labels>().MostPopular + " " + HttpUtility.HtmlEncode(this.Taxonomy.Title.ToLower());

    /// <summary>Gets the title of the all existing taxa panel.</summary>
    protected string AllTaxaTitle => Res.Get<Labels>().All + " " + this.Taxonomy.Title.ToLower();

    /// <summary>Gets the text to be displayed as the input hint.</summary>
    protected string TaxaInputHintText => Res.Get<Labels>().StartTypingAndItemsWillBeHinted.Arrange((object) this.Taxonomy.Title.ToLower());

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
    /// Gets the reference to the label control that displays the title of the flat taxon field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label control that displays the description of the flat taxon field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.GetConditionalControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label control that displays the example of the flat taxon fields.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.GetConditionalControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the client binder component that is used to bind the existing taxa.
    /// </summary>
    protected virtual GenericCollectionBinder ExistingTaxaBinder => this.GetConditionalControl<GenericCollectionBinder>("existingTaxaBinder", true);

    /// <summary>
    /// Gets the reference to text box control used for entering taxa.
    /// </summary>
    protected virtual TextBox TaxaInput => this.GetConditionalControl<TextBox>("taxaInput", true);

    /// <summary>
    /// Gets the reference to the link button used for adding taxa.
    /// </summary>
    protected virtual LinkButton AddTaxaButton => this.GetConditionalControl<LinkButton>("addTaxaButton", true);

    /// <summary>
    /// Gets the reference to the link button that expands the flat taxon field control.
    /// </summary>
    protected virtual LinkButton ExpandButton => this.GetConditionalControl<LinkButton>("expandButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the the client binder that binds the selected taxa.
    /// </summary>
    /// <value></value>
    protected internal override ClientBinder SelectedTaxaBinder => this.GetConditionalControl<ClientBinder>("selectedTaxaBinder", true);

    /// <summary>
    /// Gets the reference to the control that represents the panel with the user interface for existing taxa.
    /// </summary>
    protected internal HtmlGenericControl ExistingTaxaPanel => this.GetConditionalControl<HtmlGenericControl>("existingTaxaPanel", true);

    /// <summary>
    /// Gets the reference to the control that represents the panel that contains the command for opening existing taxa panel (and few other controls).
    /// </summary>
    protected internal HtmlGenericControl SelectFromExistingPanel => this.GetConditionalControl<HtmlGenericControl>("selectFromExistingPanel", true);

    /// <summary>
    /// Gets the reference to the link button which displays the 30 most popular existing taxons.
    /// </summary>
    protected internal LinkButton SelectFromExistingButton => this.GetConditionalControl<LinkButton>("selectFromExistingButton", true);

    /// <summary>
    /// Gets the reference to the link button which closes the panel with existing taxa.
    /// </summary>
    protected internal LinkButton CloseExistingButton => this.GetConditionalControl<LinkButton>("closeExistingButton", true);

    /// <summary>
    /// Gets the reference to the link button that displays all the taxa in the current taxonomy.
    /// </summary>
    protected internal LinkButton ShowAllTaxaButton => this.GetConditionalControl<LinkButton>("showAllTaxaButton", true);

    /// <summary>
    /// Gets the reference to the link button that displays only the most popular taxa in the current taxonomy.
    /// </summary>
    protected internal LinkButton ShowOnlyMostPopularTaxaButton => this.GetConditionalControl<LinkButton>("showOnlyMostPopularTaxa", true);

    /// <summary>
    /// Gets the label that displays the total count of taxa in the taxonomy.
    /// </summary>
    protected internal virtual Label TaxaTotalCount => this.GetConditionalControl<Label>("taxaTotalCount", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the text control that displays the taxonomy title.
    /// </summary>
    protected internal virtual Label TaxonomyTitle => this.GetConditionalControl<Label>("taxonomyTitle", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the auto complete extender for the taxa input field.
    /// </summary>
    protected internal virtual AutoCompleteExtender TaxaAutoComplete => this.GetConditionalControl<AutoCompleteExtender>("taxaAutoComplete", true);

    /// <summary>
    /// Gets the reference to the label that displays the title of the currently displayed existing taxa.
    /// </summary>
    protected internal virtual Label ExistingTaxaTitle => this.GetConditionalControl<Label>("existingTaxaTitle", true);

    /// <summary>
    /// Gets the reference to the label that represents the input hint for the taxa textbox.
    /// </summary>
    protected internal virtual Label TaxaInputHint => this.GetConditionalControl<Label>("taxaInputHint", true);

    /// <summary>
    /// Gets the reference to the control that represents the loader for existing taxa.
    /// </summary>
    protected internal virtual HtmlGenericControl OpeningExistingLoader => this.GetConditionalControl<HtmlGenericControl>("openingExistingLoader", true);

    /// <summary>
    /// Gets the reference to the control that represents the loader for all taxa.
    /// </summary>
    protected internal virtual HtmlGenericControl OpeningAllLoader => this.GetConditionalControl<HtmlGenericControl>("openingAllLoader", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (string.IsNullOrEmpty(this.Title))
        this.TitleLabel.SetTextOrHide(HttpUtility.HtmlEncode((string) this.Taxonomy.Title));
      else
        this.TitleLabel.SetTextOrHide(HttpUtility.HtmlEncode(this.Title), this.ResourceClassId);
      if (this.DescriptionLabel != null)
      {
        if (!this.AllowCreating)
          this.Description = Res.Get<TaxonomyResources>().TagsFieldInstructionsNoCreatingAllowed;
        if (string.IsNullOrEmpty(this.Description))
          this.DescriptionLabel.SetTextOrHide(Res.Get<TaxonomyResources>().FlatTaxonFieldDescription.Arrange((object) HttpUtility.HtmlEncode(this.Taxonomy.Title.ToLower())));
        else
          this.DescriptionLabel.SetTextOrHide(this.Description, this.ResourceClassId);
      }
      if (this.TaxonomyTitle != null)
        this.TaxonomyTitle.Text = HttpUtility.HtmlEncode((string) this.Taxonomy.Title);
      if (this.BindOnServer && this.DisplayMode == FieldDisplayMode.Write)
      {
        this.ExpandButton.Visible = false;
        this.ExpandTarget.Visible = false;
        this.BindOnServerPanel.Visible = true;
        HierarchicalTaxonomyDataSource taxonomyDataSource = new HierarchicalTaxonomyDataSource();
        taxonomyDataSource.RootTaxonomyId = this.TaxonomyId;
        taxonomyDataSource.TaxonomyProvider = this.TaxonomyProvider;
        taxonomyDataSource.BuildDataSource<FlatTaxon>();
        this.FlatTaxaTree.DataTextField = "Title";
        this.FlatTaxaTree.DataFieldID = "Id";
        this.FlatTaxaTree.DataValueField = "Id";
        this.FlatTaxaTree.DataFieldParentID = "ParentId";
        this.FlatTaxaTree.DataSource = (object) taxonomyDataSource.DataSource;
        this.FlatTaxaTree.DataBind();
      }
      else
      {
        if (this.TaxaTotalCount != null)
        {
          if (this.InitialTaxaCount > 30)
          {
            this.TaxaTotalCount.Text = this.InitialTaxaCount.ToString();
            this.ExistingTaxaTitle.Style.Add("display", "");
            this.ExistingTaxaTitle.Text = this.MostPopularTaxaTitle;
          }
          else if (this.InitialTaxaCount > 0)
          {
            this.ShowAllTaxaButton.Style.Add("display", "none");
            this.ExistingTaxaTitle.Style.Add("display", "none");
          }
          else
          {
            this.SelectFromExistingPanel.Style.Add("display", "none");
            this.ExistingTaxaTitle.Style.Add("display", "none");
          }
          this.ShowOnlyMostPopularTaxaButton.Style.Add("display", "none");
        }
        if (this.DisplayMode == FieldDisplayMode.Write)
        {
          this.TaxaInputHint.Text = this.TaxaInputHintText;
          this.TaxaInputHint.CssClass = "sfInputHintVisible";
          this.ExampleLabel.SetTextOrHide(this.Example, this.ResourceClassId);
          if (this.ExistingTaxaBinder != null)
            this.ExistingTaxaBinder.ServiceUrl = this.WebServiceUrl;
          this.TaxaInput.TabIndex = this.TabIndex;
          this.AddTaxaButton.TabIndex = this.TabIndex;
          this.SelectFromExistingButton.TabIndex = this.TabIndex;
          if (!this.Expanded.GetValueOrDefault())
            this.ExpandControl.TabIndex = this.TabIndex;
          this.TabIndex = (short) 0;
          this.BindOnServerPanel.Visible = false;
        }
        if (this.DisplayMode != FieldDisplayMode.Read)
          return;
        if (this.ExpandControl != null)
          this.ExpandControl.Style.Add("display", "none");
        if (this.ExpandControl == null)
          return;
        this.ExpandTarget.Style.Add("display", "none");
      }
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IFlatTaxonFieldDefinition taxonFieldDefinition))
        return;
      this.WebServiceUrl = VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl) + (object) this.TaxonomyId;
      this.ResourceClassId = definition.ResourceClassId;
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
      ScriptControlDescriptor descriptorPrivate = this.GetLastScriptDescriptor_Private();
      scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      descriptorPrivate.AddProperty("_initialTaxaCount", (object) this.InitialTaxaCount);
      descriptorPrivate.AddProperty("_mostPopularTaxaTitle", (object) this.MostPopularTaxaTitle);
      descriptorPrivate.AddProperty("_allTaxaTitle", (object) this.AllTaxaTitle);
      descriptorPrivate.AddProperty("_inputHintVisibleClass", (object) "sfInputHintVisible");
      descriptorPrivate.AddProperty("_inputHintHiddenClass", (object) "sfInputHintHidden");
      descriptorPrivate.AddElementProperty("taxaInput", this.TaxaInput.ClientID);
      descriptorPrivate.AddElementProperty("taxaInputHint", this.TaxaInputHint.ClientID);
      descriptorPrivate.AddElementProperty("addTaxaButton", this.AddTaxaButton.ClientID);
      descriptorPrivate.AddElementProperty("existingTaxaPanel", this.ExistingTaxaPanel.ClientID);
      descriptorPrivate.AddComponentProperty("existingTaxaBinder", this.ExistingTaxaBinder.ClientID);
      descriptorPrivate.AddComponentProperty("selectedTaxaBinder", this.SelectedTaxaBinder.ClientID);
      descriptorPrivate.AddElementProperty("selectFromExistingPanel", this.SelectFromExistingPanel.ClientID);
      descriptorPrivate.AddElementProperty("selectFromExistingButton", this.SelectFromExistingButton.ClientID);
      descriptorPrivate.AddElementProperty("closeExistingButton", this.CloseExistingButton.ClientID);
      descriptorPrivate.AddComponentProperty("taxaAutoComplete", this.TaxaAutoComplete.ClientID);
      descriptorPrivate.AddElementProperty("showAllTaxaButton", this.ShowAllTaxaButton.ClientID);
      descriptorPrivate.AddElementProperty("showOnlyMostPopularTaxaButton", this.ShowOnlyMostPopularTaxaButton.ClientID);
      descriptorPrivate.AddElementProperty("existingTaxaTitle", this.ExistingTaxaTitle.ClientID);
      descriptorPrivate.AddElementProperty("openingExistingLoader", this.OpeningExistingLoader.ClientID);
      descriptorPrivate.AddElementProperty("openingAllLoader", this.OpeningAllLoader.ClientID);
      descriptorPrivate.AddProperty("_bindOnServer", (object) this.BindOnServer);
      descriptorPrivate.AddProperty("_enabled", (object) this.Enabled);
      descriptorPrivate.AddProperty("_defaultTaxonsCount", (object) 30);
      scriptDescriptorList.Add((ScriptDescriptor) descriptorPrivate);
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
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FlatTaxonField.js", typeof (FlatTaxonField).Assembly.FullName)
      };
    }

    private int GetTotalTaxaCount()
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(this.TaxonomyProvider);
      Guid taxonomyId = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver().ResolveSiteTaxonomyId(this.TaxonomyId);
      return manager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId)).Count<FlatTaxon>();
    }

    /// <summary>Gets the last script descriptor.</summary>
    /// <returns></returns>
    protected internal new ScriptControlDescriptor GetLastScriptDescriptor_Private() => (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
  }
}
