// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.ContentRelationStatisticsClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Statistics;

namespace Telerik.Sitefinity.Clients
{
  /// <summary>
  /// Client for working with <see cref="T:Telerik.Sitefinity.Services.Statistics.IStatisticsService" /> related to Sitefinity content relations.
  /// </summary>
  internal class ContentRelationStatisticsClient : StatisticsClientBase
  {
    /// <summary>Updates the content relation.</summary>
    /// <param name="contentRelation">The content relation.</param>
    public void UpdateContentRelation(IContentRelation contentRelation) => this.Service.UpdateContentRelation(contentRelation);

    /// <summary>
    /// Removes content relations that are affected by an item that is deleted.
    /// </summary>
    /// <param name="deletedItemId">The deleted item id.</param>
    public void RemoveAffectedContentRelations(Guid deletedItemId) => this.Service.RemoveAffectedContentRelations(deletedItemId);

    /// <summary>Gets the content relations count.</summary>
    /// <param name="filter">The filter.</param>
    /// <param name="groupBy">The group by.</param>
    /// <returns>The count of the content relations.</returns>
    public int GetContentRelationsCount(string filter, string groupBy) => this.Service.GetContentRelationsCount(filter, groupBy);

    /// <summary>Gets the object ids of the relations.</summary>
    /// <param name="filter">The filter.</param>
    /// <returns>The object ids of the relations as enumerable of Guid.</returns>
    public IEnumerable<Guid> GetContentRelationObjectIds(string filter) => this.Service.GetContentRelationObjectIds(filter);

    /// <summary>Gets the subject ids of the relations.</summary>
    /// <param name="filter">The filter.</param>
    /// <returns>The subject ids of the relations as enumerable of Guid.</returns>
    public IEnumerable<Guid> GetContentRelationSubjectIds(string filter) => this.Service.GetContentRelationSubjectIds(filter);

    /// <summary>Gets the content relation keys.</summary>
    /// <param name="filter">The filter.</param>
    /// <returns>The keys of the relations as enumerable of Guid.</returns>
    public IEnumerable<Guid> GetContentRelationKeys(string filter) => this.Service.GetContentRelationKeys(filter);
  }
}
