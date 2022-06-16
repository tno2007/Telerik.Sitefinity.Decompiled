// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Data.PackagingDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Packaging.Data
{
  /// <summary>
  /// Defines the basic functionality that should be implemented by Packaging data providers.
  /// </summary>
  [ApplyNoPolicies]
  internal abstract class PackagingDataProvider : DataProviderBase, ISyncLockSupport
  {
    /// <inheritdoc />
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (Package)
    };

    /// <inheritdoc />
    public override string RootKey => typeof (PackagingDataProvider).Name;

    /// <inheritdoc />
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return itemType == typeof (Package) ? (IEnumerable) DataProviderBase.SetExpressions<Package>(this.GetPackages(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount) : base.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <inheritdoc />
    public override void DeleteItem(object item)
    {
      if (item.GetType() == typeof (Package))
        this.DeletePackage((Package) item);
      else
        base.DeleteItem(item);
    }

    /// <summary>
    /// Creates new instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" /></returns>
    internal abstract Package CreatePackage();

    /// <summary>
    /// Creates new instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="N:Telerik.Sitefinity.Packaging.Package" /></param>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging" /></returns>
    internal abstract Package CreatePackage(Guid id);

    /// <summary>
    /// Get a query for all <see cref="N:Telerik.Sitefinity.Packaging.Package" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="N:Telerik.Sitefinity.Packaging.Package" /> items</returns>
    internal abstract IQueryable<Package> GetPackages();

    /// <summary>
    /// Search for a <see cref="N:Telerik.Sitefinity.Packaging" /> by its identity.
    /// </summary>
    /// <param name="id">The id of the <see cref="N:Telerik.Sitefinity.Packaging.Package" /></param>
    /// <returns>The found <see cref="N:Telerik.Sitefinity.Packaging.Package" /> item.</returns>
    internal abstract Package GetPackage(Guid id);

    /// <summary>
    /// Deletes the provided <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <param name="package">The <see cref="N:Telerik.Sitefinity.Packaging.Package" /> to delete.</param>
    internal abstract void DeletePackage(Package package);

    /// <summary>
    /// Creates new instance of <see cref="!:Addon" />.
    /// </summary>
    /// <returns>New instance of <see cref="!:Addon" /></returns>
    internal abstract Addon CreateAddon();

    /// <summary>
    /// Creates new instance of <see cref="!:Addon" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="!:Addon" /></param>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging" /></returns>
    internal abstract Addon CreateAddon(Guid id);

    /// <summary>
    /// Get a query for all <see cref="!:Addon" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="!:Addon" /> items</returns>
    internal abstract IQueryable<Addon> GetAddons();

    /// <summary>
    /// Search for a <see cref="N:Telerik.Sitefinity.Packaging" /> by its identity.
    /// </summary>
    /// <param name="id">The id of the <see cref="!:Addon" /></param>
    /// <returns>The found <see cref="!:Addon" /> item.</returns>
    internal abstract Addon GetAddon(Guid id);

    /// <summary>
    /// Deletes the provided <see cref="!:Addon" />.
    /// </summary>
    /// <param name="addon">The <see cref="!:Addon" /> to delete.</param>
    internal abstract void DeleteAddon(Addon addon);

    /// <summary>
    /// Creates new instance of <see cref="!:AddonLink" />.
    /// </summary>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging" /></returns>
    internal abstract AddonLink CreateAddonLink();

    /// <summary>
    /// Creates new instance of <see cref="!:AddonLink" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="!:AddonLink" /></param>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging" /></returns>
    internal abstract AddonLink CreateAddonLink(Guid id);

    /// <summary>
    /// Get a query for all <see cref="!:AddonLink" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="!:AddonLink" /> items</returns>
    internal abstract IQueryable<AddonLink> GetAddonLinks();

    /// <summary>
    /// Get a query for all <see cref="!:AddonLink" /> items.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// IQueryable object for all <see cref="!:AddonLink" /> items
    /// </returns>
    internal abstract AddonLink GetAddonLink(Guid id);

    /// <summary>
    /// Deletes the provided <see cref="!:AddonLink" />.
    /// </summary>
    /// <param name="addonLink">The <see cref="!:AddonLink" /> to delete.</param>
    internal abstract void DeleteAddonLink(AddonLink addonLink);

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="lockId">The lock id.</param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /></returns>
    internal abstract Lock CreateLock(string lockId);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items</returns>
    internal abstract IQueryable<Lock> GetLocks();

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="obj">The <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> to delete.</param>
    internal abstract void DeleteLock(Lock obj);

    Lock ISyncLockSupport.CreateLock(string lockId) => this.CreateLock(lockId);

    IQueryable<Lock> ISyncLockSupport.GetLocks() => this.GetLocks();

    void ISyncLockSupport.DeleteLock(Lock obj) => this.DeleteLock(obj);
  }
}
