// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// Used to persist the state of a Taxon Task to allow task resume.
  /// </summary>
  [Serializable]
  internal class TaxonTaskState
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskState" /> class. Used to persist the state of a Taxon Task to allow task resume.
    /// </summary>
    public TaxonTaskState()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskState" /> class. Used to persist the state of a Taxon Task to allow task resume.
    /// </summary>
    /// <param name="taxonTask">The Taxon move task.</param>
    public TaxonTaskState(TaxonTask taxonTask)
    {
      this.SourceTaxa = taxonTask.SourceTaxa;
      this.TargetTaxon = taxonTask.TargetTaxon;
      this.TaxonomySingularName = taxonTask.TaxonomySingularName;
      this.TaxonomyPluralName = taxonTask.TaxonomyPluralName;
    }

    /// <summary>Gets or sets the list of ids and names of the items.</summary>
    public List<TaxonTaskStateItem> SourceTaxa { get; set; }

    /// <summary>
    /// Gets or sets the id and name of the item that is created.
    /// </summary>
    public TaxonTaskStateItem TargetTaxon { get; set; }

    /// <summary>Gets or sets the singular name of the taxonomy.</summary>
    public string TaxonomySingularName { get; set; }

    /// <summary>Gets or sets the plural name of the taxonomy.</summary>
    public string TaxonomyPluralName { get; set; }
  }
}
