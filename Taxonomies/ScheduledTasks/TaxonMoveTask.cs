// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonMoveTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>
  /// Background task for moving items to different classifications (tags and categories)
  /// </summary>
  internal class TaxonMoveTask : TaxonTask
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonMoveTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    public TaxonMoveTask()
      : base((ITaxonomy) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonMoveTask" /> class. Asynchronous task for moving items labeled with a taxon, to another taxon.
    /// </summary>
    /// <param name="taxonomy">Parent taxonomy of taxa that are being moved</param>
    public TaxonMoveTask(ITaxonomy taxonomy)
      : base(taxonomy)
    {
    }
  }
}
