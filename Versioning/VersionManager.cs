// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.VersionManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Versioning.Cleaner;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning
{
  /// <summary>
  /// Represents an intermediary between version control objects and persistent data.
  /// </summary>
  public class VersionManager : ManagerBase<VersionDataProvider>
  {
    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.Versioning.VersionManager" /> class.
    /// </summary>
    static VersionManager()
    {
      if (ManagerBase<VersionDataProvider>.StaticProvidersCollection != null)
        return;
      VersionManager versionManager = new VersionManager();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Versioning.VersionManager" /> class with the default provider.
    /// </summary>
    public VersionManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Versioning.VersionManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public VersionManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.VersionManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public VersionManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Creates new version for the provided object using its identity.
    /// </summary>
    /// <param name="versionedItem">The versioned item.</param>
    public virtual Change CreateVersion(IDataItem versionedItem, bool isPublished) => this.Provider.CreateVersion(versionedItem, isPublished);

    /// <summary>
    /// Creates new version for the provided object using the specified identity.
    /// </summary>
    /// <param name="versionedItem">The versioned item.</param>
    /// <param name="itemId">The item id.</param>
    public virtual Change CreateVersion(object versionedItem, Guid itemId, bool isPublished) => this.Provider.CreateVersion(versionedItem, itemId, isPublished);

    /// <summary>Gets the latest version of the specified item.</summary>
    /// <param name="itemId">The item identity.</param>
    /// <returns></returns>
    public virtual void GetLatestVersion(object targetObject, Guid itemId) => this.Provider.GetLatestVersion(targetObject, itemId);

    /// <summary>Gets the specified version of the specified item.</summary>
    /// <param name="targetObject">The target object where to restore the version.</param>
    /// <param name="itemId">The item identity.</param>
    /// <param name="versionNumber">The version number.</param>
    public virtual void GetSpecificVersion(object targetObject, Guid itemId, int versionNumber) => this.Provider.GetSpecificVersion(targetObject, itemId, versionNumber);

    public virtual void GetSpecificVersionByChangeId(object targetObject, Guid changeId) => this.Provider.GetSpecificVersionByChangeId(targetObject, changeId);

    public IList<Change> GetItemVersionHistory(Guid itemId)
    {
      IQueryable<Change> queryable = this.Provider.GetChanges();
      if (this.Provider.ShouldApplyCultureFilter(itemId))
        queryable = this.Provider.ApplyCultureFilter(queryable);
      return (IList<Change>) queryable.Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.Id == itemId)).OrderByDescending<Change, int>((Expression<Func<Change, int>>) (o => o.Version)).ToList<Change>();
    }

    /// <summary>
    /// Deletes all changes with version number smaller or equal to the specified number.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    /// <param name="versionNumber">The version number.</param>
    public virtual void TruncateVersions(Guid itemId, int versionNumber) => this.Provider.TruncateVersions(itemId, versionNumber);

    /// <summary>
    /// Deletes all changes with dates older or equal to the specified date.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    /// <param name="date">The date.</param>
    public virtual void TruncateVersions(Guid itemId, DateTime date) => this.Provider.TruncateVersions(itemId, date);

    public IQueryable<Change> GetChanges() => this.Provider.GetChanges();

    /// <summary>Gets the next change in the history for an item</summary>
    /// <returns>null if this is the last</returns>
    public Change GetNextChange(Change change)
    {
      int verNum = change.Version;
      Guid itemid = change.Parent.Id;
      return this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.Id == itemid && c.Version > verNum && c.Culture == change.Culture)).OrderBy<Change, int>((Expression<Func<Change, int>>) (o => o.Version)).FirstOrDefault<Change>();
    }

    /// <summary>Gets the previous change in the history for an item</summary>
    /// <param name="change">The change.</param>
    /// <returns>null if this is the first</returns>
    public Change GetPreviousChange(Change change)
    {
      int verNum = change.Version;
      Guid itemid = change.Parent.Id;
      return this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.Id == itemid && c.Version < verNum && c.Culture == change.Culture)).OrderByDescending<Change, int>((Expression<Func<Change, int>>) (o => o.Version)).FirstOrDefault<Change>();
    }

    public void DeleteChange(Guid changeId)
    {
      Change change = this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Id == changeId)).FirstOrDefault<Change>();
      if (change == null)
        return;
      this.Provider.Delete(change);
    }

    /// <summary>
    /// Permanently removes an item and all associated changes.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    public virtual void DeleteItem(Guid itemId) => this.Provider.DeleteItem(itemId);

    /// <summary>Cleans stale changes for all items.</summary>
    internal void CleanUpVersioning()
    {
      IQueryable<Guid> source = this.Provider.GetItems().Select<Item, Guid>((Expression<Func<Item, Guid>>) (x => x.Id));
      int num = source.Count<Guid>();
      for (int index = 0; index <= num / 1000; ++index)
      {
        foreach (Guid itemId in (IEnumerable<Guid>) source.Skip<Guid>(index * 1000).Take<Guid>(1000))
        {
          try
          {
            this.PruneChanges(itemId);
            this.SaveChanges();
          }
          catch (Exception ex)
          {
            Log.Write((object) string.Format("Revision history cleanup failed for item with id {1}. Exception: {0}", (object) ex, (object) itemId), ConfigurationPolicy.ErrorLog);
          }
        }
      }
    }

    /// <summary>
    /// Prunes the changes. Performs a cleanup of stale version of the item. <see cref="T:Telerik.Sitefinity.Versioning.Cleaner.VersioningCleanerUtils" /> is used to identify the stale changes.
    /// </summary>
    /// <param name="itemId">The item identifier.</param>
    private void PruneChanges(Guid itemId)
    {
      if (!Config.Get<VersionConfig>().Cleaner.HistoryLimitEnabled)
        return;
      foreach (Guid changeId in new VersioningCleanerUtils().FilterChangesToKeep(this.Provider.GetItem(itemId)).Select<Change, Guid>((Func<Change, Guid>) (x => x.Id)))
        this.DeleteChange(changeId);
    }

    internal bool DependencyExists(string dependencyKey, Type taskType) => this.Provider.GetDependency(dependencyKey, taskType) != null;

    /// <summary>Creates new trunk for versioned items.</summary>
    /// <returns>The new trunk.</returns>
    public virtual Trunk CreateTrunk() => this.Provider.CreateTrunk();

    /// <summary>Creates new trunk with the specified identity.</summary>
    /// <param name="pageId">The identity of the new trunk.</param>
    /// <returns>The new trunk.</returns>
    public virtual Trunk CreateTrunk(Guid id) => this.Provider.CreateTrunk(id);

    /// <summary>Gets the trunk with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A content item.</returns>
    public virtual Trunk GetTrunk(Guid id) => this.Provider.GetTrunk(id);

    /// <summary>Gets a query for trunk items.</summary>
    /// <returns>The query for trunk items.</returns>
    public virtual IQueryable<Trunk> GetTrunks() => this.Provider.GetTrunks();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The trunk to delete.</param>
    public virtual void Delete(Trunk item) => this.Provider.Delete(item);

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<VersionConfig>().DefaultProvider);

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "VersionControl";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<VersionConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static VersionManager GetManager() => ManagerBase<VersionDataProvider>.GetManager<VersionManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static VersionManager GetManager(string providerName) => ManagerBase<VersionDataProvider>.GetManager<VersionManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static VersionManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<VersionDataProvider>.GetManager<VersionManager>(providerName, transactionName);
    }
  }
}
