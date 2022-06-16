// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Versioning.ItemVersioningFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Fluent.Versioning
{
  /// <summary>
  /// Fluent API that provides versioning functionality for the items that support versioning.
  /// </summary>
  public class ItemVersioningFacade<TParentFacade> where TParentFacade : class
  {
    private AppSettings appSettings;
    private IDataItem versionedItem;
    private TParentFacade parentFacade;
    private VersionManager versionManager;

    /// <summary>Creates a new instance of the versioning facade.</summary>
    /// <param name="versionedItem">An item being versioned.</param>
    /// <param name="parentFacade">The facade that has invoked the versioning facade as a child facade.</param>
    /// <param name="appSettings">AppSettings used to work with this fluent API request.</param>
    public ItemVersioningFacade(
      IDataItem versionedItem,
      TParentFacade parentFacade,
      AppSettings appSettings)
    {
      this.versionedItem = versionedItem;
      this.parentFacade = parentFacade;
      this.appSettings = appSettings;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.Versioning.ItemVersioningFacade`1.VersionManager" /> initialized with proper provider name and transaction name.
    /// </summary>
    protected VersionManager VersionManager
    {
      get
      {
        if (this.versionManager == null)
          this.versionManager = VersionManager.GetManager(this.appSettings.VersioningProviderName, this.appSettings.TransactionName);
        return this.versionManager;
      }
    }

    /// <summary>Creates a new version of the versioned item.</summary>
    /// <returns>
    /// An instance of parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade CreateNewVersion(VersionType versionType)
    {
      this.VersionManager.CreateVersion(this.versionedItem, versionType == VersionType.Major);
      return this.parentFacade;
    }

    /// <summary>Creates a new version of the versioned item.</summary>
    /// <returns>
    /// An instance of parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade CreateNewVersion(VersionType versionType, Guid itemId)
    {
      this.VersionManager.CreateVersion((object) this.versionedItem, itemId, versionType == VersionType.Major);
      return this.parentFacade;
    }

    /// <summary>Deletes all the versions of the versioned item.</summary>
    /// <returns>
    /// An instance of parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade Delete()
    {
      foreach (Change change in (IEnumerable<Change>) this.VersionManager.GetItemVersionHistory(this.versionedItem.Id))
        this.VersionManager.DeleteChange(change.Id);
      return this.parentFacade;
    }

    /// <summary>Deletes the specified version of the versioned item.</summary>
    /// <param name="versionNumber">The number of the version to be deleted.</param>
    /// <returns>
    /// An instance of the parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade Delete(int versionNumber)
    {
      Change change = this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).Where<Change>((Func<Change, bool>) (ch => ch.Version == versionNumber)).SingleOrDefault<Change>();
      if (change != null)
        this.VersionManager.DeleteChange(change.Id);
      return this.parentFacade;
    }

    /// <summary>
    /// Deletes the versions of the versioned item that satisfy the given filter.
    /// </summary>
    /// <param name="filter">The delegate used to filter the query of the versions of the versioned item.</param>
    /// <returns>
    /// An instance of the parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade Delete(Func<Change, bool> filter)
    {
      foreach (Change change in this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).Where<Change>(filter))
        this.VersionManager.DeleteChange(change.Id);
      return this.parentFacade;
    }

    /// <summary>Returns all the changes for the given versioned item.</summary>
    /// <returns>A query of the changes of the given versioned items.</returns>
    public IQueryable<Change> Get() => this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).AsQueryable<Change>();

    /// <summary>
    /// Reverts the versioned item to the specified version number.
    /// </summary>
    /// <param name="versionNumber">Version number to which the item ought to be reverted.</param>
    /// <exception cref="T:System.ArgumentException">
    /// Thrown if the specified version number cannot be found for the versioned item.
    /// </exception>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade RevertTo(int versionNumber)
    {
      this.VersionManager.GetSpecificVersionByChangeId((object) this.versionedItem, (this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).Where<Change>((Func<Change, bool>) (c => c.Version == versionNumber)).SingleOrDefault<Change>() ?? throw new ArgumentException("No such version.")).Id);
      this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.versionedItem);
      this.VersionManager.CreateVersion(this.versionedItem, true);
      return this.parentFacade;
    }

    /// <summary>
    /// Reverts the versioned item to the specified version of the item.
    /// </summary>
    /// <param name="filter">The delegate used to get the version to which the item ought to be reverted.</param>
    /// <exception cref="T:System.ArgumentException">
    /// Thrown if the specified version cannot be found for the versioned item.
    /// </exception>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade RevertTo(Func<IQueryable<Change>, Change> filter)
    {
      IQueryable<Change> queryable = this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).AsQueryable<Change>();
      this.VersionManager.GetSpecificVersionByChangeId((object) this.versionedItem, (filter(queryable) ?? throw new ArgumentException("No such version.")).Id);
      this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.versionedItem);
      this.VersionManager.CreateVersion(this.versionedItem, true);
      return this.parentFacade;
    }

    /// <summary>
    /// Tries to revert the versioned item to the specified version number.
    /// </summary>
    /// <param name="versionNumber">Version number to which the item ought to be reverted.</param>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade TryRevertTo(int versionNumber) => this.TryRevertTo(versionNumber, out bool _);

    /// <summary>
    /// Tries to revert the versioned item to the specified version number.
    /// </summary>
    /// <param name="versionNumber">Version number to which the item ought to be reverted.</param>
    /// <param name="result">True if the item was successfully reverted; otherwise false.</param>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade TryRevertTo(int versionNumber, out bool result)
    {
      Change change = this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).Where<Change>((Func<Change, bool>) (c => c.Version == versionNumber)).SingleOrDefault<Change>();
      if (change == null)
      {
        result = true;
        this.VersionManager.GetSpecificVersionByChangeId((object) this.versionedItem, change.Id);
        this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.versionedItem);
        this.VersionManager.CreateVersion(this.versionedItem, true);
      }
      else
        result = false;
      return this.parentFacade;
    }

    /// <summary>
    /// Tries to revert the versioned item to the specified version.
    /// </summary>
    /// <param name="filter">The delegate used to get the version to which the item ought to be reverted.</param>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter) => this.TryRevertTo(filter, out bool _);

    /// <summary>
    /// Tries to revert the versioned item to the specified version.
    /// </summary>
    /// <param name="filter">The delegate used to get the version to which the item ought to be reverted.</param>
    /// <param name="result">An instance of the parent facade that instantiated the versioning child facade.</param>
    /// <returns>An instance of the parent facade that instantiated the versioning child facade.</returns>
    public TParentFacade TryRevertTo(Func<IQueryable<Change>, Change> filter, out bool result)
    {
      IQueryable<Change> queryable = this.VersionManager.GetItemVersionHistory(this.versionedItem.Id).AsQueryable<Change>();
      Change change = filter(queryable);
      if (change == null)
      {
        this.VersionManager.GetSpecificVersionByChangeId((object) this.versionedItem, change.Id);
        this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.versionedItem);
        this.VersionManager.CreateVersion(this.versionedItem, true);
        result = true;
      }
      else
        result = false;
      return this.parentFacade;
    }

    protected void ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(IDataItem dataItem)
    {
      if (!(dataItem is PageDraft pageDraft))
        return;
      foreach (PageDraftControl control in (IEnumerable<PageDraftControl>) pageDraft.Controls)
      {
        control.SupportedPermissionSets = control.IsLayoutControl ? ControlData.LayoutPermissionSets : ControlData.ControlPermissionSets;
        control.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId((IManager) this.VersionManager);
      }
    }
  }
}
