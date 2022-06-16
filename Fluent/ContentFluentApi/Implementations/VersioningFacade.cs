// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.VersioningFacade`1
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

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Provides easy management of versions for a content item
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade that hosts the versioning facade</typeparam>
  public class VersioningFacade<TParentFacade> : BaseFacadeWithParent<TParentFacade>
    where TParentFacade : BaseFacade
  {
    private IDataItem dataItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.VersioningFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="versionedItem">IDataItem that is going to be versioned</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="versionedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public VersioningFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      IDataItem versionedItem)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<IDataItem>(versionedItem, nameof (versionedItem));
      this.dataItem = versionedItem;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) VersionManager.GetManager(this.settings.VersioningProviderName, this.settings.TransactionName);

    /// <summary>IDataItem that is going to be versioned</summary>
    /// <exception cref="T:System.InvalidOperationException">If, upon getting, returned value is null</exception>
    /// <exception cref="T:System.ArgumentNullException">If, upon setting, proposed value is null</exception>
    protected IDataItem VersionedItem
    {
      get
      {
        FacadeHelper.AssertNotNull<IDataItem>(this.dataItem, "Versioned item can not be null");
        return this.dataItem;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<IDataItem>(value, nameof (VersionedItem));
        this.dataItem = value;
      }
    }

    /// <summary>Returns versioning manager.</summary>
    /// <remarks>A shortcut for (VersionManager)this.GetManager()</remarks>
    protected VersionManager VersionManager => (VersionManager) this.GetManager();

    /// <summary>Creates a new version of the versioned item.</summary>
    /// <returns>
    /// An instance of parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade CreateNewVersion(VersionType versionType)
    {
      this.VersionManager.CreateVersion(this.VersionedItem, versionType == VersionType.Major);
      return this.Done();
    }

    /// <summary>Deletes all the versions of the versioned item.</summary>
    /// <returns>
    /// An instance of parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade Delete()
    {
      foreach (Change change in (IEnumerable<Change>) this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id))
        this.VersionManager.DeleteChange(change.Id);
      return this.Done();
    }

    /// <summary>Deletes the specified version of the versioned item.</summary>
    /// <param name="versionNumber">The number of the version to be deleted.</param>
    /// <returns>
    /// An instance of the parent facade that instantiated the versioning child facade.
    /// </returns>
    public TParentFacade Delete(int versionNumber)
    {
      Change change = this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).Where<Change>((Func<Change, bool>) (ch => ch.Version == versionNumber)).SingleOrDefault<Change>();
      if (change != null)
        this.VersionManager.DeleteChange(change.Id);
      return this.Done();
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
      foreach (Change change in this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).Where<Change>(filter))
        this.VersionManager.DeleteChange(change.Id);
      return this.Done();
    }

    /// <summary>Returns all the changes for the given versioned item.</summary>
    /// <returns>A query of the changes of the given versioned items.</returns>
    public IList<Change> Get() => this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id);

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
      Change change = this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).Where<Change>((Func<Change, bool>) (c => c.Version == versionNumber)).SingleOrDefault<Change>();
      FacadeHelper.AssertNotNull<Change>(change, "Can not revert to a null version");
      this.VersionManager.GetSpecificVersionByChangeId((object) this.VersionedItem, change.Id);
      this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.VersionedItem);
      this.VersionManager.CreateVersion(this.VersionedItem, true);
      return this.Done();
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
      IQueryable<Change> queryable = this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).AsQueryable<Change>();
      this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.VersionedItem);
      Change change = filter(queryable);
      FacadeHelper.AssertNotNull<Change>(change, "Can not revert, because the change filter left no results");
      this.VersionManager.GetSpecificVersionByChangeId((object) this.VersionedItem, change.Id);
      this.VersionManager.CreateVersion(this.VersionedItem, true);
      return this.Done();
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
      Change change = this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).Where<Change>((Func<Change, bool>) (c => c.Version == versionNumber)).SingleOrDefault<Change>();
      if (change == null)
      {
        result = true;
        this.VersionManager.GetSpecificVersionByChangeId((object) this.VersionedItem, change.Id);
        this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.VersionedItem);
        this.VersionManager.CreateVersion(this.VersionedItem, true);
      }
      else
        result = false;
      return this.Done();
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
      IQueryable<Change> queryable = this.VersionManager.GetItemVersionHistory(this.VersionedItem.Id).AsQueryable<Change>();
      Change change = filter(queryable);
      if (change == null)
      {
        this.VersionManager.GetSpecificVersionByChangeId((object) this.VersionedItem, change.Id);
        this.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId(this.VersionedItem);
        this.VersionManager.CreateVersion(this.VersionedItem, true);
        result = true;
      }
      else
        result = false;
      return this.Done();
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
