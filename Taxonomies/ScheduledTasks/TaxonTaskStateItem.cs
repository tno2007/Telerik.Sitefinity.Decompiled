// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskStateItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>Used to persist the item state of a Taxon Task.</summary>
  [Serializable]
  internal class TaxonTaskStateItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskStateItem" /> class.
    /// </summary>
    public TaxonTaskStateItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTaskStateItem" /> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="name">The name.</param>
    public TaxonTaskStateItem(Guid id, string name)
    {
      this.Id = id;
      this.Name = name;
    }

    /// <summary>Gets or sets the id.</summary>
    /// <value>The id.</value>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name { get; set; }
  }
}
