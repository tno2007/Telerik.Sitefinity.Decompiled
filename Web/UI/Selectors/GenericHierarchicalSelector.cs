// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Selector for selecting a hierarchical taxon.</summary>
  public class GenericHierarchicalSelector : SimpleScriptView
  {
    private const string selectorScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.GenericHierarchicalSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.GenericHierarchicalSelector.ascx");
    private bool bindOnLoad = true;
    private string selectorTitleTextTemplate;
    private string rootRadioTextTemplate;
    private string pagesRadioText;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.DefaultLayoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Default value of <c>LayoutTemplatePath</c> when no value is explicitly set by the user
    /// </summary>
    protected virtual string DefaultLayoutTemplatePath => GenericHierarchicalSelector.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the name of the taxonomy provider from which the taxon ought to be selected.
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// Gets or sets the value that determines whether the root (taxonomy itself) can be selected.
    /// </summary>
    public bool AllowRootSelection { get; set; }

    /// <summary>Gets or sets the text of the selector title.</summary>
    public string SelectorTitleTextTemplate
    {
      get => this.selectorTitleTextTemplate ?? this.DefaultSelectorTitleTextTemplate;
      set => this.selectorTitleTextTemplate = value;
    }

    /// <summary>
    /// Default value of <c>SelectorTitleTextTemplate</c> when user hasn't explicitly set a value
    /// </summary>
    protected virtual string DefaultSelectorTitleTextTemplate => Res.Get<Labels>().SelectParameter;

    /// <summary>Gets or sets the text of the root radio title.</summary>
    public string RootRadioTextTemplate
    {
      get => this.rootRadioTextTemplate ?? this.DefaultRootRadioTextTemplate;
      set => this.rootRadioTextTemplate = value;
    }

    /// <summary>
    /// Default value of <c>RootRadioTextTemplate</c> when user has't explicitly set value
    /// </summary>
    protected virtual string DefaultRootRadioTextTemplate => Res.Get<PageResources>().PagesRootLabel;

    /// <summary>Gets or sets the text of the taxa radio title.</summary>
    public string PagesRadioText
    {
      get => this.pagesRadioText ?? this.DefaultPagesRadioText;
      set => this.pagesRadioText = value;
    }

    /// <summary>
    /// Default value of <c>PagesRadioText</c> when user hasn't explicitly set value
    /// </summary>
    protected virtual string DefaultPagesRadioText => Res.Get<PageResources>().SelectAParent;

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootNodeID { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets the tree view.</summary>
    /// <value>The tree view.</value>
    protected virtual RadTreeView TreeView => this.Container.GetControl<RadTreeView>("treeView", true);

    /// <summary>
    /// Gets the reference to the selector title text control.
    /// </summary>
    protected virtual Label SelectorTitle => this.Container.GetControl<Label>("selectorTitle", true);

    /// <summary>
    /// Gets the reference to the button that when clicked applies the selection.
    /// </summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    /// <summary>
    /// Gets the reference to the client binder that binds the taxa on the client side.
    /// </summary>
    protected virtual RadTreeBinder TreeBinder => this.Container.GetControl<RadTreeBinder>("treeBinder", true);

    /// <summary>
    /// Gets the reference to the control that holds the tree selector related controls and elements.
    /// </summary>
    protected virtual HtmlGenericControl TreePanel => this.Container.GetControl<HtmlGenericControl>("treePanel", true);

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
    protected virtual RadioButton NodesRadio => this.Container.GetControl<RadioButton>("nodesRadio", true);

    /// <summary>
    /// Gets the reference for the label of the taxa radio radio button.
    /// </summary>
    protected virtual Label PagesRadioLabel => this.Container.GetControl<Label>("nodesRadioLabel", true);

    /// <summary>
    /// Gets the reference for the label that is displayed when no taxa has been created yet.
    /// </summary>
    protected virtual Label NoNodesCreatedLabel => this.Container.GetControl<Label>("noNodesCreatedLabel", true);

    /// <summary>
    /// Gets the reference for the panel that is displayed when no taxa has been created yet.
    /// </summary>
    protected virtual HtmlGenericControl NoNodesPanel => this.Container.GetControl<HtmlGenericControl>("noNodesPanel", true);

    /// <summary>Configures the selector.</summary>
    /// <param name="rootId">The root id.</param>
    /// <param name="taxonomyProvider">The taxonomy provider.</param>
    public virtual void ConfigureSelector(string webServiceUrl, Guid rootId, string provider)
    {
      this.RootNodeID = rootId;
      this.Provider = provider;
      this.WebServiceUrl = webServiceUrl;
      this.ChildControlsCreated = false;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.RootSelector.Visible = this.AllowRootSelection;
      string webServiceUrl = this.WebServiceUrl;
      if (!webServiceUrl.EndsWith("/"))
        webServiceUrl += "/";
      this.TreeBinder.ServiceUrl = webServiceUrl + "hierarchy/{0}";
      this.TreeBinder.ServiceChildItemsBaseUrl = this.WebServiceUrl + "hierarchy/";
      this.TreeBinder.ServicePredecessorBaseUrl = this.WebServiceUrl + "predecessor/";
      this.TreeBinder.RootTaxonID = this.RootNodeID;
      this.TreeView.MultipleSelect = false;
      this.TreeView.CheckBoxes = false;
      this.TreeBinder.AllowMultipleSelection = false;
      this.DoneButton.TabIndex = this.TabIndex;
      this.TabIndex = (short) 0;
      this.NoNodesCreatedLabel.Text = this.NoItemsHaveBeenCreatedYetText;
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ClientComponentName, this.ClientID);
      controlDescriptor.AddElementProperty("selectorTitle", this.SelectorTitle.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("treeView", this.TreeView.ClientID);
      controlDescriptor.AddElementProperty("rootRadio", this.RootRadio.ClientID);
      controlDescriptor.AddElementProperty("nodesRadio", this.NodesRadio.ClientID);
      controlDescriptor.AddElementProperty("rootRadioLabel", this.RootRadioLabel.ClientID);
      controlDescriptor.AddElementProperty("nodesRadioLabel", this.PagesRadioLabel.ClientID);
      controlDescriptor.AddElementProperty("noNodesCreatedLabel", this.NoNodesCreatedLabel.ClientID);
      controlDescriptor.AddElementProperty("treePanel", this.TreePanel.ClientID);
      controlDescriptor.AddElementProperty("noNodesPanel", this.NoNodesPanel.ClientID);
      controlDescriptor.AddComponentProperty("treeBinder", this.TreeBinder.ClientID);
      controlDescriptor.AddProperty("_noNodesHaveBeenCreatedText", (object) this.NoItemsHaveBeenCreatedYetText);
      controlDescriptor.AddProperty("_rootNodeId", (object) this.RootNodeID);
      controlDescriptor.AddProperty("_selectorTitleTextTemplate", (object) this.SelectorTitleTextTemplate);
      controlDescriptor.AddProperty("_rootRadioTextTemplate", (object) this.RootRadioTextTemplate);
      controlDescriptor.AddProperty("_nodesRadioText", (object) this.NodesRadioText);
      controlDescriptor.AddProperty("_allowRootSelection", (object) this.AllowRootSelection);
      controlDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Text above the radio button that expands the RadTreeView for selecting a parent item (e.g. "select parent page")
    /// </summary>
    protected virtual string NodesRadioText => Res.Get<PageResources>().SelectAParent;

    /// <summary>
    /// Message that will be displayed when a user wants to select a parent node when there are no nodes returned by the web service
    /// </summary>
    protected virtual string NoItemsHaveBeenCreatedYetText => Res.Get<PageResources>().NoPagesHaveBeenCreatedYet;

    /// <summary>
    /// Name of the JS component. Passed to the default <c>ScriptControlDescriptor</c> in <c>GetScriptDescriptors</c>
    /// </summary>
    protected virtual string ClientComponentName => typeof (GenericHierarchicalSelector).FullName;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.GenericHierarchicalSelector.js", typeof (GenericHierarchicalSelector).Assembly.FullName)
    };
  }
}
