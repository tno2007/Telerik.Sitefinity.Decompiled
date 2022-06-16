// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.MultisiteTaxonomyGuard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  internal class MultisiteTaxonomyGuard
  {
    public TaxonomyManager TaxonomyManager { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.MultisiteTaxonomyGuard" /> class.
    /// </summary>
    public MultisiteTaxonomyGuard()
      : this((TaxonomyManager) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.MultisiteTaxonomyGuard" /> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    public MultisiteTaxonomyGuard(TaxonomyManager manager)
    {
      if (manager == null)
        manager = TaxonomyManager.GetManager();
      this.TaxonomyManager = manager;
    }

    /// <summary>
    /// Determines whether the given taxonomy is root taxonomy.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy identifier.</param>
    /// <param name="throwException">if set to <c>true</c> throw exception.</param>
    /// <returns>Return <c>true</c> if taxonomy is root.</returns>
    internal bool IsRootTaxonomy(Guid taxonomyId, bool throwException = true) => this.IsRootTaxonomy((ITaxonomy) (this.TaxonomyManager.GetTaxonomies<Taxonomy>().FirstOrDefault<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == taxonomyId)) ?? throw new ItemNotFoundException(string.Format("Taxonomy with id {0} not found!", (object) taxonomyId))), throwException);

    /// <summary>
    /// Determines whether the given taxonomy is root taxonomy.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="throwException">if set to <c>true</c> throw exception.</param>
    /// <returns>Return <c>true</c> if taxonomy is root.</returns>
    internal bool IsRootTaxonomy(ITaxonomy taxonomy, bool throwException = true)
    {
      int num = taxonomy.IsRootTaxonomy() ? 1 : 0;
      if (!(num == 0 & throwException))
        return num != 0;
      throw new ArgumentException(string.Format("Taxonomy with ID {0} is not a root taxonomy.", (object) taxonomy.Id));
    }

    /// <summary>
    /// Determines whether the given taxonomy is split taxonomy.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy identifier.</param>
    /// <param name="throwException">if set to <c>true</c> throw exception.</param>
    /// <returns>Return <c>true</c> if taxonomy is split.</returns>
    internal bool IsSplitTaxonomy(Guid taxonomyId, bool throwException = true) => this.IsSplitTaxonomy(this.TaxonomyManager.GetTaxonomy(taxonomyId), throwException);

    /// <summary>
    /// Determines whether the given taxonomy is split taxonomy.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="throwException">if set to <c>true</c> throw exception.</param>
    /// <returns>Return <c>true</c> if taxonomy is split.</returns>
    internal bool IsSplitTaxonomy(ITaxonomy taxonomy, bool throwException = true)
    {
      int num = taxonomy.IsSplitTaxonomy() ? 1 : 0;
      if (!(num == 0 & throwException))
        return num != 0;
      throw new ArgumentException(string.Format("Taxonomy with ID {0} is not a split taxonomy.", (object) taxonomy.Id));
    }
  }
}
