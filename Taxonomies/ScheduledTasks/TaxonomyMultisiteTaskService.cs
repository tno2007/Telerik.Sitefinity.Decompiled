// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonomyMultisiteTaskService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// A service implementation for managing multisite taxonomy <see cref="T:Telerik.Sitefinity.Scheduling.ScheduledTask" /> objects
  /// used for managing item to taxon relations in multisite context (split, sync of taxonomies)
  /// </summary>
  public class TaxonomyMultisiteTaskService : ITaxonomyMultisiteTaskService
  {
    private const int TaxonomyUnlinkTaskDelayInMinutes = 0;

    /// <summary>
    /// Creates a new scheduled task for cleaning after the link between a taxonomy and a site is broken.
    /// The newly created scheduled task is automatically registered in the scheduling manager.
    /// </summary>
    /// <param name="siteId">The Id of the site with which the link is broken.</param>
    /// <param name="taxonomyId">The Id of the taxonomy which is breaking the link with the site.</param>
    /// <param name="taxonomyProviderName">Name of the taxonomy provider.</param>
    /// <returns>The Id of the newly created schedule task.</returns>
    public virtual Guid CreateTaxonomyUnlinkFromSiteCleanTask(
      Guid siteId,
      Guid taxonomyId,
      string taxonomyProviderName)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      UnlinkTaxonomyFromSiteCleanTask task = new UnlinkTaxonomyFromSiteCleanTask(taxonomyId, taxonomyProviderName, siteId);
      task.ExecuteTime = DateTime.UtcNow.AddMinutes(0.0);
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
      return task.Id;
    }

    /// <summary>
    /// Creates a new scheduled task for cleaning after the link between a taxonomy and a site is broken.
    /// The newly created scheduled task is automatically registered in the scheduling manager.
    /// </summary>
    /// <param name="siteId">The Id of the site with which the link is broken.</param>
    /// <param name="taxonomy">The taxonomy which is breaking the link with the site.</param>
    /// <returns>The Id of the newly created schedule task.</returns>
    public virtual Guid CreateTaxonomyUnlinkFromSiteCleanTask(Guid siteId, ITaxonomy taxonomy)
    {
      string providerName = ObjectFactory.Resolve<IProviderNameResolver>().GetProviderName(taxonomy.Provider);
      return this.CreateTaxonomyUnlinkFromSiteCleanTask(siteId, taxonomy.Id, providerName);
    }
  }
}
