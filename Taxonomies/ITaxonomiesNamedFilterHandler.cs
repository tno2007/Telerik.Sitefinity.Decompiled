// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ITaxonomiesNamedFilterHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Interface for filtering taxonomies in taxonomy web service
  /// </summary>
  internal interface ITaxonomiesNamedFilterHandler
  {
    /// <summary>
    /// Gets a filtered query of taxonomies depending on a given named filter.
    /// </summary>
    /// <param name="filterName">Filter name.</param>
    /// <returns>IQueryable of filtered taxonomies</returns>
    IQueryable<Taxonomy> GetFilteredTaxonomies(string filterName);
  }
}
