// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Data;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>Selector for selecting a hierarchical taxon.</summary>
  public class HierarchicalTaxonSelector : SimpleScriptView
  {
    private HierarchicalTaxonomy taxonomy;
    private string newTaxonDialogUrl;
    private Guid taxonomyId;
    private TrackedList<Guid> selectedTaxa;
    private const string selectorScript = "Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.Scripts.HierarchicalTaxonSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.HierarchicalTaxonSelector.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? HierarchicalTaxonSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the pageId of the taxonomy from which the taxon ought to be selected.
    /// </summary>
    public Guid TaxonomyId
    {
      get => this.taxonomyId;
      set
      {
        this.taxonomyId = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the name of the taxonomy provider from which the taxon ought to be selected.
    /// </summary>
    public string TaxonomyProvider { get; set; }

    /// <summary>
    /// Gets or sets the value that determines whether the root (taxonomy itself) can be selected.
    /// </summary>
    public bool AllowRootSelection { get; set; }

    /// <summary>
    /// Gets or sets the valud that determines whether multiple taxa can be selected.
    /// </summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Determines whether the done selecting button will be displayed or note.
    /// </summary>
    public bool ShowDoneSelectingButton { get; set; }

    /// <summary>
    /// Determines whether the new taxon button will be displayed or note.
    /// </summary>
    public bool ShowCreateNewTaxonButton { get; set; }

    /// <summary>
    /// Gets the instance of taxonomy from which the taxon ought to be selected.
    /// </summary>
    protected HierarchicalTaxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null && this.TaxonomyId != Guid.Empty)
          this.taxonomy = (HierarchicalTaxonomy) TaxonomyManager.GetManager(this.TaxonomyProvider).GetTaxonomy(this.TaxonomyId);
        return this.taxonomy;
      }
    }

    /// <summary>Gets the url of the new taxon dialog.</summary>
    protected string NewTaxonDialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.newTaxonDialogUrl))
        {
          this.newTaxonDialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (NewHierarchicalTaxonSimpleDialog).Name);
          this.newTaxonDialogUrl = VirtualPathUtility.AppendTrailingSlash(this.newTaxonDialogUrl);
          this.newTaxonDialogUrl = this.newTaxonDialogUrl + "?taxonomyId=" + (object) this.Taxonomy.Id + "&taxonomyProvider=" + this.TaxonomyProvider + "&taxonomyTitle=" + (object) this.Taxonomy.Title + "&taxonName=" + (object) this.Taxonomy.TaxonName;
        }
        return this.newTaxonDialogUrl;
      }
    }

    /// <summary>Gets or sets the text of the selector title.</summary>
    public string SelectorTitleTextTemplate { get; set; }

    /// <summary>Gets or sets the text of the create taxon button.</summary>
    public string CreateTaxonButtonTextTemplate { get; set; }

    /// <summary>Gets or sets the text of the root radio title.</summary>
    public string RootRadioTextTemplate { get; set; }

    /// <summary>Gets or sets the text of the taxa radio title.</summary>
    public string TaxaRadioText { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootTaxonID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the selector will be bound on the server.
    /// </summary>
    public bool BindOnServer { get; set; }

    internal bool Hidden { get; set; }

    /// <summary>
    /// Returns a list of the selected taxons when BindOnServer is true.
    /// </summary>
    public TrackedList<Guid> SelectedTaxa
    {
      get
      {
        if (this.selectedTaxa == null)
          this.selectedTaxa = new TrackedList<Guid>();
        this.selectedTaxa.Clear();
        if (this.BindOnServer)
        {
          foreach (ControlItem controlItem in !this.AllowMultipleSelection ? (IEnumerable<RadTreeNode>) this.TreeView.SelectedNodes : (IEnumerable<RadTreeNode>) this.TreeView.CheckedNodes)
            this.selectedTaxa.Add(new Guid(controlItem.Value));
        }
        return this.selectedTaxa;
      }
      set
      {
        this.selectedTaxa = value;
        if (!this.BindOnServer || this.selectedTaxa == null)
          return;
        this.TreeView.ClearCheckedNodes();
        foreach (Guid selectedTaxon in this.selectedTaxa)
        {
          RadTreeNode nodeByValue = this.TreeView.FindNodeByValue(selectedTaxon.ToString());
          nodeByValue.Checked = true;
          if (!this.AllowMultipleSelection)
            nodeByValue.Selected = true;
        }
      }
    }

    /// <summary>Gets the tree view.</summary>
    /// <value>The tree view.</value>
    protected virtual RadTreeView TreeView => this.Container.GetControl<RadTreeView>("taxaTree", true);

    /// <summary>
    /// Gets the reference to the selector title text control.
    /// </summary>
    protected virtual Label SelectorTitle => this.Container.GetControl<Label>("selectorTitle", true);

    /// <summary>
    /// Gets the reference to the button that opens a dialog for creating a new taxon.
    /// </summary>
    protected virtual LinkButton CreateTaxonButton => this.Container.GetControl<LinkButton>("createTaxonButton", true);

    /// <summary>
    /// Gets the reference to the button that when clicked applies the selection.
    /// </summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    /// <summary>
    /// Gets the reference to the client binder that binds the taxa on the client side.
    /// </summary>
    protected virtual RadTreeBinder TaxaTreeBinder => this.Container.GetControl<RadTreeBinder>("taxaTreeBinder", true);

    /// <summary>
    /// Gets the reference to the control that holds the tree selector related controls and elements.
    /// </summary>
    protected virtual HtmlGenericControl TreePanel => this.Container.GetControl<HtmlGenericControl>("treePanel", true);

    /// <summary>
    /// Gets the reference to the element displaying no categories have been created yet message
    /// </summary>
    protected virtual HtmlGenericControl LabelNoCategories => this.Container.GetControl<HtmlGenericControl>("labelNoCategories", true);

    /// <summary>Gets the reference to the new taxon dialog.</summary>
    protected virtual Telerik.Web.UI.RadWindow NewTaxonDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("newTaxonDialog", true);

    /// <summary>
    /// Gets the generic html control that provides the options between selecting the root or from taxa as the
    /// taxon.
    /// </summary>
    protected virtual HtmlGenericControl RootSelector => this.Container.GetControl<HtmlGenericControl>("rootSelector", true);

    /// <summary>
    /// Gets the reference to the radio button that indicates that the root should be selected.
    /// </summary>
    protected virtual RadioButton RootRadio => this.Container.GetControl<RadioButton>("rootRadio", true);

    /// <summary>
    /// Gets the reference for the label of the root radio button.
    /// </summary>
    protected virtual Label RootRadioLabel => this.Container.GetControl<Label>("rootRadioLabel", true);

    /// <summary>
    /// Gets the reference to the radio button that indicates that the taxon from the taxa should be selected.
    /// </summary>
    protected virtual RadioButton TaxaRadio => this.Container.GetControl<RadioButton>("taxaRadio", true);

    /// <summary>
    /// Gets the reference for the label of the taxa radio radio button.
    /// </summary>
    protected virtual Label TaxaRadioLabel => this.Container.GetControl<Label>("taxaRadioLabel", true);

    /// <summary>
    /// Gets the reference for the label that is displayed when no taxa has been created yet.
    /// </summary>
    protected virtual Label NoTaxaCreatedLabel => this.Container.GetControl<Label>("noTaxaCreatedLabel", false);

    /// <summary>Configures the taxon selector.</summary>
    /// <param name="taxonomy">An instance of hierarchical taxonomy from which the taxon ought to be selected.</param>
    public virtual void ConfigureSelector(HierarchicalTaxonomy taxonomy, string taxonomyProvider)
    {
      this.taxonomy = taxonomy;
      this.TaxonomyId = this.taxonomy.Id;
      this.TaxonomyProvider = taxonomyProvider;
      this.ChildControlsCreated = false;
    }

    /// <summary>Hides the buttons.</summary>
    public virtual void HideSelectorButtons()
    {
      this.ShowDoneSelectingButton = false;
      this.ShowCreateNewTaxonButton = false;
      this.DoneButton.Style.Add("display", "none");
      this.CreateTaxonButton.Style.Add("display", "none");
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SelectorTitleTextTemplate = Res.Get<Labels>().SelectParameter;
      this.CreateTaxonButtonTextTemplate = Res.Get<Labels>().CreateAParameter;
      this.RootRadioTextTemplate = Res.Get<TaxonomyResources>().HierarchicalTaxonParentRootLabel;
      this.TaxaRadioText = Res.Get<TaxonomyResources>().SelectAParent;
      if (this.Taxonomy != null)
        this.NewTaxonDialog.NavigateUrl = this.NewTaxonDialogUrl;
      this.RootSelector.Visible = this.AllowRootSelection;
      this.TaxaTreeBinder.AllowMultipleSelection = this.AllowMultipleSelection;
      this.TaxaTreeBinder.RootTaxonID = this.RootTaxonID;
      this.TreeView.MultipleSelect = this.AllowMultipleSelection;
      this.TreeView.CheckBoxes = this.AllowMultipleSelection;
      if (!this.ShowDoneSelectingButton)
        this.DoneButton.Style.Add("display", "none");
      if (!this.ShowCreateNewTaxonButton)
        this.CreateTaxonButton.Style.Add("display", "none");
      this.DoneButton.TabIndex = this.TabIndex;
      this.CreateTaxonButton.TabIndex = this.TabIndex;
      this.TabIndex = (short) 0;
      if (!this.BindOnServer)
        return;
      this.TaxaTreeBinder.Visible = false;
      HierarchicalTaxonomyDataSource taxonomyDataSource = new HierarchicalTaxonomyDataSource();
      taxonomyDataSource.RootTaxonomyId = this.TaxonomyId;
      taxonomyDataSource.TaxonomyProvider = this.TaxonomyProvider;
      taxonomyDataSource.BuildDataSource<HierarchicalTaxon>();
      this.TreeView.DataTextField = "Title";
      this.TreeView.DataFieldID = "Id";
      this.TreeView.DataValueField = "Id";
      this.TreeView.DataFieldParentID = "ParentId";
      this.TreeView.DataSource = (object) taxonomyDataSource.DataSource;
      this.TreeView.DataBind();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (HierarchicalTaxonSelector).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("selectorTitle", this.SelectorTitle.ClientID);
      controlDescriptor.AddElementProperty("labelNoCategories", this.LabelNoCategories.ClientID);
      controlDescriptor.AddElementProperty("createTaxonButton", this.CreateTaxonButton.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("rootRadio", this.RootRadio.ClientID);
      controlDescriptor.AddElementProperty("taxaRadio", this.TaxaRadio.ClientID);
      controlDescriptor.AddElementProperty("rootRadioLabel", this.RootRadioLabel.ClientID);
      controlDescriptor.AddElementProperty("taxaRadioLabel", this.TaxaRadioLabel.ClientID);
      if (this.NoTaxaCreatedLabel != null)
        controlDescriptor.AddElementProperty("noTaxaCreatedLabel", this.NoTaxaCreatedLabel.ClientID);
      controlDescriptor.AddComponentProperty("newTaxonDialog", this.NewTaxonDialog.ClientID);
      controlDescriptor.AddElementProperty("treePanel", this.TreePanel.ClientID);
      if (this.TaxaTreeBinder != null && this.TaxaTreeBinder.Visible)
        controlDescriptor.AddComponentProperty("taxaTreeBinder", this.TaxaTreeBinder.ClientID);
      controlDescriptor.AddProperty("_allowRootSelection", (object) this.AllowRootSelection);
      controlDescriptor.AddProperty("_showDoneSelectingButton", (object) this.ShowDoneSelectingButton);
      controlDescriptor.AddProperty("_showCreateNewTaxonButton", (object) this.ShowCreateNewTaxonButton);
      controlDescriptor.AddProperty("_noTaxaHaveBeenCreatedText", (object) Res.Get<TaxonomyResources>().NoTaxaHaveBeenCreatedYet);
      controlDescriptor.AddProperty("taxonomyId", (object) this.TaxonomyId);
      controlDescriptor.AddProperty("_selectorTitleTextTemplate", (object) this.SelectorTitleTextTemplate);
      controlDescriptor.AddProperty("_createTaxonButtonTextTemplate", (object) this.CreateTaxonButtonTextTemplate);
      controlDescriptor.AddProperty("_rootRadioTextTemplate", (object) this.RootRadioTextTemplate);
      controlDescriptor.AddProperty("_taxaRadioText", (object) Res.Get<TaxonomyResources>().SelectAParent);
      if (this.Taxonomy != null)
      {
        controlDescriptor.AddProperty("_selectorTitleText", (object) this.Taxonomy.Title.ToLower());
        controlDescriptor.AddProperty("_createTaxonButtonText", (object) this.Taxonomy.TaxonName.ToLower());
        controlDescriptor.AddProperty("_rootRadioText", (object) this.Taxonomy.TaxonName.Value);
      }
      controlDescriptor.AddProperty("_bindOnServer", (object) this.BindOnServer);
      controlDescriptor.AddProperty("_hidden", (object) this.Hidden);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.Scripts.HierarchicalTaxonSelector.js", typeof (HierarchicalTaxonSelector).Assembly.FullName)
    };
  }
}
