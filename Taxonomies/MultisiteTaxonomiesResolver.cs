// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.MultisiteTaxonomiesResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// A component that determines which taxonomy to use in multisite scenario based on the specified site settings.
  /// </summary>
  internal class MultisiteTaxonomiesResolver
  {
    private TaxonomyManager manager;

    /// <summary>Gets an instance of multi-site taxonomies resolver.</summary>
    /// <param name="manager">The manager.</param>
    /// <returns>Returns multi-site taxonomies resolver</returns>
    public static MultisiteTaxonomiesResolver GetMultisiteTaxonomiesResolver(
      TaxonomyManager manager = null)
    {
      MultisiteTaxonomiesResolver taxonomiesResolver = ObjectFactory.Resolve<MultisiteTaxonomiesResolver>();
      taxonomiesResolver.Manager = manager;
      return taxonomiesResolver;
    }

    /// <summary>Gets or sets the manager.</summary>
    /// <value>The manager.</value>
    public TaxonomyManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = TaxonomyManager.GetManager();
        return this.manager;
      }
      set => this.manager = value;
    }

    /// <summary>
    /// Returns the id of the taxonomy which is related to the current site.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <returns>The Id of the taxonomy to use for the current site.</returns>
    public Guid ResolveSiteTaxonomyId(Guid taxonomyId) => this.ResolveSiteTaxonomyId(taxonomyId, new Guid?());

    /// <summary>
    /// Returns the id of the taxonomy which is related to the specified site.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <param name="siteId">The site identifier.</param>
    /// <returns>The Id of the taxonomy to use for the specified site.</returns>
    public Guid ResolveSiteTaxonomyId(Guid taxonomyId, Guid? siteId) => this.ResolveSiteTaxonomyId(this.Manager.GetTaxonomy(taxonomyId), siteId);

    /// <summary>
    /// Returns the id of the taxonomy which is related to the specified site.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="siteId">The site identifier.</param>
    /// <returns>The Id of the taxonomy to use for the specified site.</returns>
    public Guid ResolveSiteTaxonomyId(ITaxonomy taxonomy, Guid? siteId)
    {
      if (this.IsSiteTaxonomy(taxonomy, siteId))
        return taxonomy.Id;
      Taxonomy splitTaxonomy = this.Manager.GetSplitTaxonomy<Taxonomy>(taxonomy.Id, siteId);
      return splitTaxonomy == null ? taxonomy.Id : splitTaxonomy.Id;
    }

    /// <summary>
    /// Returns the taxonomy which is related to the current site.
    /// </summary>
    /// <typeparam name="T">The type of the T.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>The taxonomy to use for the current site.</returns>
    public T ResolveSiteTaxonomy<T>(T taxonomy) where T : class, ITaxonomy => this.ResolveSiteTaxonomy<T>(taxonomy, new Guid?());

    /// <summary>
    /// Returns the taxonomy which is related to the specified site.
    /// </summary>
    /// <typeparam name="T">The type of the T.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="siteId">The site identifier.</param>
    /// <returns>The taxonomy to use for the specified site.</returns>
    public T ResolveSiteTaxonomy<T>(T taxonomy, Guid? siteId) where T : class, ITaxonomy
    {
      Guard.ArgumentNotNull((object) taxonomy, nameof (taxonomy));
      return this.IsSiteTaxonomy((ITaxonomy) taxonomy, siteId) ? taxonomy : this.Manager.GetSiteTaxonomy<T>(taxonomy.Id, siteId);
    }

    private bool IsSiteTaxonomy(ITaxonomy taxonomy, Guid? siteId)
    {
      if (!taxonomy.IsSplitTaxonomy())
        return false;
      if (this.Manager.IsSplitTaxonomyInternal(taxonomy, siteId))
        return true;
      throw new ArgumentException(string.Format("The taxonomy with the specified id {0} is not related to the specified site {1}", (object) taxonomy.Id, (object) siteId));
    }
  }
}
