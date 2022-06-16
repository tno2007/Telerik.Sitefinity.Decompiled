// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.TaxonSelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A base class for <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> derived classes that work with taxa.
  /// </summary>
  public abstract class TaxonSelectorResultView : SelectorResultView
  {
    private ITaxonomy taxonomy;
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.TaxonSelectorResultView.js";

    /// <summary>
    /// Gets or sets a value indicating whether more than one taxon can be selected.
    /// </summary>
    /// <value>
    /// <c>true</c> if the field allows multiple selection; otherwise, <c>false</c>.
    /// </value>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Model.ITaxonomy" /> representing the taxonomy to which the taxon field is bound to.
    /// </summary>
    protected virtual ITaxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.taxonomy = TaxonomyManager.GetManager(this.TaxonomyProvider).GetTaxonomy(this.TaxonomyId);
        return this.taxonomy;
      }
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public string TaxonomyProvider { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>Gets or sets the taxonomy id.</summary>
    /// <value>The taxonomy id.</value>
    public virtual Guid TaxonomyId { get; set; }

    protected virtual string ConfigureSelectorTitleLabels()
    {
      ITaxonomy taxonomy = TaxonomyManager.GetManager(this.TaxonomyProvider).GetTaxonomy(this.TaxonomyId);
      Lstring taxonName = taxonomy.TaxonName;
      if (taxonomy.Name == Res.Get<TaxonomyResources>().Categories || taxonomy.Name == Res.Get<TaxonomyResources>().Tags || taxonomy.Name == Res.Get<TaxonomyResources>().Departments)
        taxonName.Value = taxonName.Value.ToLower();
      return string.Format(Res.Get<TaxonomyResources>().SelectATaxonName, (object) HttpUtility.HtmlEncode(taxonName.Value));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("allowMultipleSelection", (object) this.AllowMultipleSelection);
      string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl));
      controlDescriptor.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor.AddProperty("taxonomyProvider", (object) this.TaxonomyProvider);
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
        Name = "Telerik.Sitefinity.Web.Scripts.TaxonSelectorResultView.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
