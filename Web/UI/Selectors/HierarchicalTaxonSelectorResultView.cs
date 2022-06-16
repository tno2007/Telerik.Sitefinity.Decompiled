// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> control that works with hierarchical taxa (e.g. categories).
  /// </summary>
  public class HierarchicalTaxonSelectorResultView : TaxonSelectorResultView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.HierarchicalTaxonSelectorResultView.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.HierarchicalTaxonSelectorResultView.js";
    private const string predecessorAppendWebServiceUrl = "predecessor/";
    private bool hierarchicalTreeRootBindModeEnabled = true;

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
      get => !string.IsNullOrEmpty(HierarchicalTaxonSelectorResultView.layoutTemplatePath) ? HierarchicalTaxonSelectorResultView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    public bool AllowRootSelection { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootTaxonID { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds
    /// to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Enable or disable BindingMode = HierarchicalTreeRootBind. Default is true.
    /// </summary>
    public bool HierarchicalTreeRootBindModeEnabled
    {
      get => this.hierarchicalTreeRootBindModeEnabled;
      set => this.hierarchicalTreeRootBindModeEnabled = value;
    }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>Gets the control that selects taxa.</summary>
    protected virtual GenericPageSelector TaxaSelector => this.Container.GetControl<GenericPageSelector>("taxaSelector", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the choose button control.</summary>
    protected virtual Label ChooseButtonTextControl => this.Container.GetControl<Label>("chooseButtonTextControl", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the title label.</summary>
    public virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("lblSelectorTitle", true, TraverseMethod.DepthFirst);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("taxaSelector", this.TaxaSelector.ClientID);
      controlDescriptor.AddProperty("predecessorWebServiceUrl", (object) "predecessor/");
      controlDescriptor.AddProperty("_chooseButtonTextControlId", (object) this.ChooseButtonTextControl.ClientID);
      controlDescriptor.AddProperty("_selectLabel", (object) Res.Get<Labels>().Select);
      controlDescriptor.AddProperty("_changeLabel", (object) Res.Get<Labels>().Change);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
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
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.HierarchicalTaxonSelectorResultView.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.TaxaSelector == null)
        return;
      this.TaxaSelector.RootNodeID = this.RootTaxonID;
      this.TaxaSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      string str1 = VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl);
      string str2 = VirtualPathUtility.AppendTrailingSlash(str1 + (object) this.TaxonomyId);
      this.TaxaSelector.BindOnLoad = this.BindOnLoad;
      this.TaxaSelector.WebServiceUrl = str2;
      this.TaxaSelector.OrginalServiceBaseUrl = str2;
      this.TaxaSelector.ServiceChildItemsBaseUrl = str1 + "subtaxa/";
      this.TaxaSelector.ServicePredecessorBaseUrl = str1 + "predecessor/";
      this.TaxaSelector.ServiceTreeUrl = str2;
      this.TaxaSelector.HierarchicalTreeRootBindModeEnabled = this.HierarchicalTreeRootBindModeEnabled;
      this.TaxaSelector.TreeViewCssClass = "sfTreeView";
      this.TaxaSelector.UICulture = this.UICulture;
      this.TitleLabel.Text = this.ConfigureSelectorTitleLabels();
      this.TaxaSelector.ConstantFilter = (string) null;
    }
  }
}
