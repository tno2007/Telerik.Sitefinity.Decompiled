// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// Selector used in <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField" />.
  /// Should not be confused with <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector" />,
  /// which is used for selecting the parent of hierarchical taxa in the field control for categories -
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField" />.
  /// The selectors differ visually. What's more, this selector works with the whole data items, and the other selector uses IDs only.
  /// </summary>
  public class HierarchicalTaxonSelector : GenericHierarchicalSelector
  {
    private string singleTaxonName;
    /// <summary>Name of the JS component class name</summary>
    public const string ScriptClassName = "Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector";
    /// <summary>Name of the embedded script</summary>
    public const string ScriptName = "Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.HierarchicalTaxonSelector.js";
    /// <summary>
    /// Name of the embedded template. To generate the default template path, pass it to <c>ControlUtilities.ToVppPath</c>
    /// </summary>
    public const string EmbeddedTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.HierarchicalTaxonSelector.ascx";

    /// <summary>
    /// Name of the JS component. Passed to the default <c>ScriptControlDescriptor</c> in <c>GetScriptDescriptors</c>
    /// </summary>
    /// <value></value>
    protected override string ClientComponentName => "Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector";

    /// <summary>
    /// Default value of <c>LayoutTemplatePath</c> when no value is explicitly set by the user
    /// </summary>
    /// <value></value>
    protected override string DefaultLayoutTemplatePath => ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.HierarchicalTaxonSelector.ascx");

    /// <summary>
    /// Get or set the hierarchical taxonomy ID. Required to generate proper service url when binding on the client
    /// </summary>
    public Guid TaxonomyId { get; set; }

    /// <summary>
    /// The mode which determines which taxonomy to use based on the site context.
    /// </summary>
    /// <value>The skip site context.</value>
    public string SiteContextMode { get; set; }

    /// <summary>
    /// Get or set the base web service url. Overrides all other service urls. Used to generate urls based on taxon and taxonomy on the client
    /// </summary>
    public string BaseWebServiceUrl { get; set; }

    /// <summary>Text above the radio buttons (title of the control)</summary>
    protected virtual string ParentTaxonText => Res.Get<TaxonomyResources>().ParentTaxonName.Arrange((object) this.SingleTaxonName);

    /// <summary>
    /// Text above the radio button that expands the RadTreeView for selecting a parent item (e.g. "select parent page")
    /// </summary>
    /// <value></value>
    protected override string NodesRadioText => Res.Get<TaxonomyResources>().SelectAParent;

    /// <summary>
    /// Message that will be displayed when a user wants to select a parent node when there are no nodes returned by the web service
    /// </summary>
    /// <value></value>
    protected override string NoItemsHaveBeenCreatedYetText => Res.Get<TaxonomyResources>().NoItemsHaveBeenCreated;

    /// <summary>
    /// Default value of <c>PagesRadioText</c> when user hasn't explicitly set value
    /// </summary>
    /// <value></value>
    protected override string DefaultPagesRadioText => Res.Get<TaxonomyResources>().SelectATaxonName;

    /// <summary>
    /// Default value of <c>RootRadioTextTemplate</c> when user has't explicitly set value
    /// </summary>
    /// <value></value>
    protected override string DefaultRootRadioTextTemplate => Res.Get<TaxonomyResources>().NoParentTaxonNameDescription;

    /// <summary>
    /// Default value of <c>SelectorTitleTextTemplate</c> when user hasn't explicitly set a value
    /// </summary>
    /// <value></value>
    protected override string DefaultSelectorTitleTextTemplate => Res.Get<TaxonomyResources>().SelectATaxonName;

    /// <summary>Display name of the hierarchical taxon</summary>
    protected virtual string SingleTaxonName => this.singleTaxonName == null ? string.Empty : this.singleTaxonName.ToLower();

    /// <summary>Text control above radio buttons</summary>
    protected virtual ITextControl ParentTaxonLabel => this.Container.GetControl<ITextControl>("parentTaxonLabel", true);

    /// <summary>Configures the selector</summary>
    /// <param name="webServiceUrl">Web service url</param>
    /// <param name="taxonomyId">Id of the hierarchical taxonomy</param>
    /// <param name="provider">Name of the taxonomy provider</param>
    public override void ConfigureSelector(string webServiceUrl, Guid taxonomyId, string provider)
    {
      HierarchicalTaxonomy taxonomy = TaxonomyManager.GetManager(provider).GetTaxonomy<HierarchicalTaxonomy>(taxonomyId);
      if (taxonomy != null)
        this.singleTaxonName = (string) taxonomy.TaxonName;
      this.ParentTaxonLabel.Text = HttpUtility.HtmlEncode(this.ParentTaxonText);
      this.TaxonomyId = taxonomyId;
      this.BaseWebServiceUrl = webServiceUrl;
      bool result = false;
      bool.TryParse(this.Context.Request.QueryString["skipSiteContext"], out result);
      if (result)
        this.SiteContextMode = "skipSiteContext";
      base.ConfigureSelector(webServiceUrl, Guid.Empty, provider);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ParentTaxonLabel.Text = HttpUtility.HtmlEncode(this.ParentTaxonText);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors[scriptDescriptors.Count - 1];
      controlDescriptor.AddProperty("_taxonomyId", (object) this.TaxonomyId);
      controlDescriptor.AddProperty("_baseServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.BaseWebServiceUrl));
      controlDescriptor.AddProperty("_selectorTitleText", (object) this.SingleTaxonName);
      controlDescriptor.AddProperty("_rootRadioText", (object) this.SingleTaxonName);
      controlDescriptor.AddProperty("_siteContextMode", (object) this.SiteContextMode);
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
      new ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.HierarchicalTaxonSelector.js", typeof (HierarchicalTaxonSelector).Assembly.FullName)
    };
  }
}
