// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>
  /// Dialog for simplified user interface for creating new hierarchical taxon.
  /// </summary>
  public class NewHierarchicalTaxonSimpleDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.NewHierarchicalTaxonSimpleDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.Scripts.NewHierarchicalTaxonSimpleDialog.js";
    private const string webServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? NewHierarchicalTaxonSimpleDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the label that represents the dialog title.
    /// </summary>
    protected virtual Label DialogTitleElement => this.Container.GetControl<Label>("dialogTitle", true);

    /// <summary>
    /// Gets the reference to the text field component that represents the taxon title.
    /// </summary>
    protected virtual TextField TaxonTitleField => this.Container.GetControl<TextField>("taxonTitleField", true);

    /// <summary>
    /// Gets the reference to the button that when clicked creates a new taxon.
    /// </summary>
    protected virtual LinkButton CreateButton => this.Container.GetControl<LinkButton>("createButton", true);

    /// <summary>
    /// Gets the reference to the button that when clicked closes the dialog.
    /// </summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    /// <summary>
    /// Gets the reference to the hierarchical taxon selector used to select the parent of the
    /// new taxon.
    /// </summary>
    protected virtual HierarchicalTaxonSelector ParentSelector => this.Container.GetControl<HierarchicalTaxonSelector>("parentSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (NewHierarchicalTaxonSimpleDialog).FullName, this.ClientID);
      controlDescriptor.AddProperty("_createALabel", (object) Res.Get<Labels>().CreateA);
      controlDescriptor.AddProperty("_createThisLabel", (object) Res.Get<Labels>().CreateThis);
      controlDescriptor.AddProperty("_noParentLabel", (object) Res.Get<TaxonomyResources>().HierarchicalTaxonParentRootLabel);
      controlDescriptor.AddProperty("_selectAParentLabel", (object) Res.Get<TaxonomyResources>().SelectAParent);
      controlDescriptor.AddProperty("_selectorTitleLabel", (object) Res.Get<Labels>().ParentParameter);
      controlDescriptor.AddProperty("_webServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/"));
      controlDescriptor.AddElementProperty("dialogTitleElement", this.DialogTitleElement.ClientID);
      controlDescriptor.AddElementProperty("createButton", this.CreateButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddComponentProperty("parentSelector", this.ParentSelector.ClientID);
      controlDescriptor.AddComponentProperty("taxonTitleField", this.TaxonTitleField.ClientID);
      controlDescriptor.AddProperty("_urlRegexFilter", (object) DefinitionsHelper.UrlRegularExpressionFilter);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
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
      new ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.Scripts.NewHierarchicalTaxonSimpleDialog.js", typeof (NewHierarchicalTaxonSimpleDialog).Assembly.FullName)
    };
  }
}
