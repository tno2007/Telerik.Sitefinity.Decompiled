// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomiesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>Contains helper methods related to taxonomies</summary>
  internal static class TaxonomiesHelper
  {
    /// <summary>Gets the taxonomies names joined with ","</summary>
    /// <param name="taxonomyIds">The taxonomy ids.</param>
    internal static string GetTaxonomiesNames(Guid[] taxonomyIds)
    {
      IQueryable<Taxon> queryable = TaxonomyManager.GetManager().GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => taxonomyIds.Contains<Guid>(t.Id)));
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Taxon taxon in (IEnumerable<Taxon>) queryable)
        stringBuilder.Append(taxon.Title.GetString(CultureInfo.InvariantCulture, true)).Append(",");
      string str = stringBuilder.ToString();
      int length = str.LastIndexOf(",");
      return length <= -1 ? str : str.Substring(0, length);
    }

    /// <summary>Checking that is the root taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>Return <c>true</c> if the given taxonomy is root taxonomy and <c>false</c> otherwise.</returns>
    /// <exception cref="T:System.NullReferenceException">Thrown when <paramref name="taxonomy" /> is null.</exception>
    internal static bool IsRootTaxonomy(this ITaxonomy taxonomy)
    {
      if (taxonomy == null)
        throw new ArgumentNullException(nameof (taxonomy));
      return !taxonomy.IsSplitTaxonomy();
    }

    /// <summary>Checking that is a split taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>Return <c>true</c> if the given taxonomy is split taxonomy and <c>false</c> otherwise.</returns>
    /// <exception cref="T:System.NullReferenceException">Thrown when <paramref name="taxonomy" /> is null.</exception>
    internal static bool IsSplitTaxonomy(this ITaxonomy taxonomy)
    {
      if (taxonomy == null)
        throw new ArgumentNullException(nameof (taxonomy));
      return taxonomy.RootTaxonomyId.HasValue;
    }

    /// <summary>
    /// Gets for a given taxonomy the Id of the root taxonomy.
    /// If the provided taxonomy is the root, then its Id is returned.
    /// If the provided taxonomy is a split, then the its root Id is returned.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>Return the Id of the root taxonomy.</returns>
    /// <exception cref="T:System.NullReferenceException">Thrown when <paramref name="taxonomy" /> is null.</exception>
    internal static Guid GetRootTaxonomyId(this ITaxonomy taxonomy)
    {
      if (taxonomy == null)
        throw new ArgumentNullException(nameof (taxonomy));
      return !taxonomy.IsRootTaxonomy() ? taxonomy.RootTaxonomyId.GetValueOrDefault() : taxonomy.Id;
    }
  }
}
