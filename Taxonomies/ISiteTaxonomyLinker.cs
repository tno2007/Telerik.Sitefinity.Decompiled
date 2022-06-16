// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ISiteTaxonomyLinker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// A common interface for components dedicated to multisite operations for linking and unlinking of taxonomies and sites
  /// </summary>
  internal interface ISiteTaxonomyLinker
  {
    /// <summary>
    /// Creates a new split taxonomy for the specified site. The new split taxonomy is a copy of the currently used taxonomy by the site.
    /// The new split taxonomy is linked to the site.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="duplicateTaxa">A flag indicating if the newly created split taxonomy should have duplicates of the original taxonomy taxa.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    /// <returns>Returns the newly created split taxonomy.</returns>
    TTaxonomy SplitSiteTaxonomy<TTaxonomy>(
      Guid rootTaxonomyId,
      Guid? siteId = null,
      bool duplicateTaxa = false,
      bool performSaveChanges = true)
      where TTaxonomy : class, ITaxonomy;

    /// <summary>Arranges the target site to use the given taxonomy.</summary>
    /// <param name="taxonomyId">The taxonomy id to be used.</param>
    /// <param name="targetSiteId">The target site id.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    void UseTaxonomyInSite(Guid taxonomyId, Guid targetSiteId, bool performSaveChanges = true);

    /// <summary>Arranges the target site to use the given taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy to be used.</param>
    /// <param name="targetSiteId">The target site id.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    void UseTaxonomyInSite(ITaxonomy taxonomy, Guid targetSiteId, bool performSaveChanges = true);

    /// <summary>
    /// Breaks the link between a taxonomy and a site.
    /// Removes all taxa values of the given taxonomy from all content items in the site that no longer have intersection with that taxonomy in any site.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy id (root or split).</param>
    /// <param name="siteId">The site id.</param>
    void CleanOrphanedSiteTaxonomyUsage(Guid taxonomyId, Guid siteId);
  }
}
