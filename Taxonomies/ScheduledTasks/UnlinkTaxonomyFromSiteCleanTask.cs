// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.UnlinkTaxonomyFromSiteCleanTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Scheduling;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// Defines the object to be used as scheduled task for Cleanup after removing the link between a site and a specified taxonomy (root or split).
  /// </summary>
  public class UnlinkTaxonomyFromSiteCleanTask : ScheduledTask
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.UnlinkTaxonomyFromSiteCleanTask" /> class.
    /// </summary>
    /// <remarks>
    /// This constructor is required for the reconstruction of the task when it is time for its execution.
    /// This operation is done internally by the <see cref="T:Telerik.Sitefinity.Scheduling.Scheduler" /> when the scheduler timer has elapsed.
    /// </remarks>
    public UnlinkTaxonomyFromSiteCleanTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.UnlinkTaxonomyFromSiteCleanTask" /> class.
    /// </summary>
    /// <param name="taxonomyId">The id of the taxonomy which is no longer linked to the site.</param>
    /// <param name="taxonomyProviderName">Name of the taxonomy provider holding the specified taxonomy.</param>
    /// <param name="siteId">The id of the site with which the taxonomy link is removed.</param>
    public UnlinkTaxonomyFromSiteCleanTask(
      Guid taxonomyId,
      string taxonomyProviderName,
      Guid siteId)
    {
      this.TaxonomyId = taxonomyId;
      this.TaxonomyProviderName = taxonomyProviderName;
      this.SiteId = siteId;
    }

    /// <summary>
    /// Gets or sets the id of the site which the taxonomy link is removed for.
    /// </summary>
    /// <value>The site id.</value>
    public new Guid SiteId { get; set; }

    /// <summary>
    /// Gets or sets the id of the taxonomy which is no longer linked to the specified site.
    /// </summary>
    /// <value>The taxonomy id.</value>
    public Guid TaxonomyId { get; set; }

    /// <summary>
    /// Gets or sets the name of the taxonomy provider holding the specified taxonomy.
    /// </summary>
    /// <value>The name of the taxonomy provider.</value>
    public string TaxonomyProviderName { get; set; }

    /// <summary>
    /// Performs the actual work on cleaning-up after removing the link between a site and a specified taxonomy (root or split)
    /// </summary>
    public override void ExecuteTask() => ObjectFactory.Container.Resolve<ISiteTaxonomyLinker>((ResolverOverride[]) new ParameterOverride[1]
    {
      new ParameterOverride("manager", (object) TaxonomyManager.GetManager(this.TaxonomyProviderName))
    }).CleanOrphanedSiteTaxonomyUsage(this.TaxonomyId, this.SiteId);

    /// <summary>
    /// Gets any data that the task needs to persist. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.Taxonomies.ScheduledTasks.UnlinkTaxonomyFromSiteCleanTask.SetCustomData(System.String)" /> should have implementation for deserializing the data.
    /// </summary>
    /// <returns>String containing the serialized task data.</returns>
    public override string GetCustomData() => this.SiteId.ToString() + ";" + (object) this.TaxonomyId + ";" + this.TaxonomyProviderName;

    /// <summary>
    /// Sets the task data when reviving the task from a persistent storage to deserialize the task stored data.
    /// </summary>
    /// <param name="customData">The stored serialized task data.</param>
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.SiteId = new Guid(strArray[0]);
      this.TaxonomyId = new Guid(strArray[1]);
      this.TaxonomyProviderName = strArray[2];
    }
  }
}
