// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> control that works with flat taxa (e.g. tags).
  /// </summary>
  public class FlatTaxonSelectorResultView : TaxonSelectorResultView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.FlatTaxonSelectorResultView.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.FlatTaxonSelectorResultView.js";
    private const string serviceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
    private const string itemTemplate = "{{Title}}";

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
      get => !string.IsNullOrEmpty(FlatTaxonSelectorResultView.layoutTemplatePath) ? FlatTaxonSelectorResultView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>Gets the control that selects taxa.</summary>
    protected virtual FlatSelector TaxaSelector => this.Container.GetControl<FlatSelector>("taxaSelector", true, TraverseMethod.DepthFirst);

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
        Name = "Telerik.Sitefinity.Web.Scripts.FlatTaxonSelectorResultView.js"
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
      this.TaxaSelector.ItemType = typeof (FlatTaxon).AssemblyQualifiedName;
      this.TaxaSelector.DataKeyNames = "Id";
      this.TaxaSelector.DataMembers.Add(new DataMemberInfo()
      {
        ColumnTemplate = "{{Title}}",
        IsExtendedSearchField = true,
        Name = "Title",
        HeaderText = "Title",
        IsSearchField = true
      });
      this.TaxaSelector.ShowSelectedFilter = false;
      this.TaxaSelector.AllowMultipleSelection = true;
      this.TaxaSelector.BindOnLoad = this.BindOnLoad;
      string str = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc")) + (object) this.TaxonomyId);
      this.TaxaSelector.ServiceUrl = str;
      this.TaxaSelector.UICulture = this.UICulture;
      this.ClientBinder.ServiceUrl = str;
      this.TitleLabel.Text = this.ConfigureSelectorTitleLabels();
    }
  }
}
