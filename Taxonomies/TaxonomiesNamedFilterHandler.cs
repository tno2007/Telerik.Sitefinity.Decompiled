// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomiesNamedFilterHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>Filters taxonomies in taxonomy web service</summary>
  internal class TaxonomiesNamedFilterHandler : ITaxonomiesNamedFilterHandler
  {
    /// <summary>ShowNotUsedTaxonomies filter</summary>
    public const string ShowNotUsedTaxonomies = "ShowNotUsedTaxonomies";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomiesNamedFilterHandler" /> class.
    /// </summary>
    public TaxonomiesNamedFilterHandler()
      : this((TaxonomyManager) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomiesNamedFilterHandler" /> class.
    /// </summary>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    public TaxonomiesNamedFilterHandler(TaxonomyManager taxonomyManager)
    {
      if (taxonomyManager == null)
        taxonomyManager = TaxonomyManager.GetManager();
      this.TaxonomyManager = taxonomyManager;
    }

    private TaxonomyManager TaxonomyManager { get; set; }

    /// <summary>
    /// Gets a filtered query of taxonomies depending on a given named filter.
    /// </summary>
    /// <param name="filterName">Filter name.</param>
    /// <returns>IQueryable of filtered taxonomies</returns>
    public IQueryable<Taxonomy> GetFilteredTaxonomies(string filterName) => filterName == "ShowNotUsedTaxonomies" ? this.TaxonomyManager.FilterByNotUsedTaxonomies() : this.TaxonomyManager.GetTaxonomies<Taxonomy>();
  }
}
