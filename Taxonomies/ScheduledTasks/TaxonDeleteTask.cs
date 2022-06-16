// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonDeleteTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// Background task for deleting classifications(tags and categories)
  /// </summary>
  internal class TaxonDeleteTask : TaxonTask
  {
    /// <summary>The taxon move task name</summary>
    public const string TaxonDeleteTaskName = "TaxonDeleteTask";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonDeleteTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    public TaxonDeleteTask()
      : base((ITaxonomy) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonDeleteTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    /// <param name="taxonomy">Parent taxonomy of taxa that are being deleted</param>
    public TaxonDeleteTask(ITaxonomy taxonomy)
      : base(taxonomy)
    {
    }

    /// <inheritdoc />
    public override string TaskName => nameof (TaxonDeleteTask);

    /// <inheritdoc />
    public override void SetCustomData(string customData) => this.SourceTaxa = this.Serializer.Deserialize<TaxonTaskState>(customData).SourceTaxa;

    /// <inheritdoc />
    public override string BuildUniqueKey() => this.GetCustomData();

    /// <inheritdoc />
    public override string GetCustomData() => this.Serializer.Serialize((object) new TaxonTaskState((TaxonTask) this));

    internal override void AfterTaxonItemsUpdate()
    {
      TaxonomyManager manager = TaxonomyManager.GetManager();
      foreach (TaxonTaskStateItem sourceTaxon in this.SourceTaxa)
      {
        ITaxon taxon = manager.GetTaxon(sourceTaxon.Id);
        manager.Delete(taxon);
      }
      manager.SaveChanges();
    }
  }
}
