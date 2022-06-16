// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.PackagingManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Packaging.Configuration;
using Telerik.Sitefinity.Packaging.Data;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>Manages the data layer for the Packaging module.</summary>
  /// <seealso cref="!:Telerik.Sitefinity.Data.ManagerBase&lt;Telerik.Sitefinity.Packaging.Data.PackagingDataProvider&gt;" />
  internal class PackagingManager : 
    ManagerBase<PackagingDataProvider>,
    ISyncLockSupportManager,
    IManager,
    IDisposable,
    IProviderResolver,
    ISyncLockSupport
  {
    private static IEnumerable<string> installedAddons;
    public const string AddonsRootFolderPathName = "App_Data\\Sitefinity\\AddOns";
    private const string RelativeRootPrefix = "~/";
    private const string AddonsCacheKey = "sf_addns_cache";
    private static readonly object AddonsCacheSync = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> class.
    /// </summary>
    public PackagingManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public PackagingManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">The name of a distributed transaction. If empty string or null this manager will use separate transaction.</param>
    public PackagingManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the installed addons.</summary>
    /// <value>The installed addons.</value>
    public static IEnumerable<string> InstalledAddons
    {
      get
      {
        ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
        if (!(cacheManager["sf_addns_cache"] is IEnumerable<string> list1))
        {
          lock (PackagingManager.AddonsCacheSync)
          {
            if (!(cacheManager["sf_addns_cache"] is IEnumerable<string> list1))
            {
              PackagingManager manager = PackagingManager.GetManager((string) null, "sf_addns_cache");
              using (new ElevatedModeRegion((IManager) manager))
                list1 = (IEnumerable<string>) manager.GetAddons().Select<Addon, string>((Expression<Func<Addon, string>>) (a => a.Name)).ToList<string>();
              TransactionManager.DisposeTransaction("sf_addns_cache");
              cacheManager.Add("sf_addns_cache", (object) list1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Addon), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
            }
          }
        }
        return list1;
      }
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "Packaging";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<PackagingConfig>().Providers;

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<PackagingConfig>().DefaultProvider);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> using the default provider
    /// </summary>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /></returns>
    public static PackagingManager GetManager() => ManagerBase<PackagingDataProvider>.GetManager<PackagingManager>();

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> by explicitly specifying the provider name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /></returns>
    public static PackagingManager GetManager(string providerName) => ManagerBase<PackagingDataProvider>.GetManager<PackagingManager>(providerName);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /> by explicitly specifying the provider and transaction name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">The name of a named transaction to be used.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingManager" /></returns>
    public static PackagingManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<PackagingDataProvider>.GetManager<PackagingManager>(providerName, transactionName);
    }

    /// <summary>
    /// Creates new instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" /></returns>
    public Package CreatePackage() => this.Provider.CreatePackage();

    /// <summary>
    /// Creates new instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <param name="id">The id of the package</param>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" /></returns>
    public Package CreatePackage(Guid id) => this.Provider.CreatePackage(id);

    /// <summary>
    /// Deletes the provided <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <param name="package">The <see cref="N:Telerik.Sitefinity.Packaging.Package" /> to delete.</param>
    public void DeletePackage(Package package) => this.Provider.DeletePackage(package);

    /// <summary>
    /// Creates new instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="N:Telerik.Sitefinity.Packaging.Package" /></param>
    /// <returns>New instance of <see cref="N:Telerik.Sitefinity.Packaging.Package" /></returns>
    public Package GetPackage(Guid id) => this.Provider.GetPackage(id);

    /// <summary>
    /// Get a query for all <see cref="N:Telerik.Sitefinity.Packaging.Package" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="N:Telerik.Sitefinity.Packaging.Package" /> items</returns>
    public IQueryable<Package> GetPackages() => this.Provider.GetPackages();

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" />.
    /// </summary>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /></returns>
    public Addon CreateAddon() => this.Provider.CreateAddon();

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /></param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /></returns>
    public Addon CreateAddon(Guid id) => this.Provider.CreateAddon(id);

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" />.
    /// </summary>
    /// <param name="addon">The <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /> to delete.</param>
    public void DeleteAddon(Addon addon) => this.Provider.DeleteAddon(addon);

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /></param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /></returns>
    public Addon GetAddon(Guid id) => this.Provider.GetAddon(id);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.Packaging.Model.Addon" /> items</returns>
    public IQueryable<Addon> GetAddons() => this.Provider.GetAddons();

    /// <summary>Sets the status.</summary>
    /// <param name="addon">The add-on.</param>
    /// <param name="structureStatus">The structure status.</param>
    /// <param name="dataStatus">The data status.</param>
    /// <param name="siteId">The site identifier.</param>
    public void SetStatus(
      Addon addon,
      StructureStatus structureStatus,
      DataStatus dataStatus,
      Guid siteId = default (Guid))
    {
      if (siteId == Guid.Empty)
        siteId = SystemManager.CurrentContext.CurrentSite.Id;
      AddonStatus addonStatus = addon.Statuses.SingleOrDefault<AddonStatus>((Func<AddonStatus, bool>) (a => a.SiteId == siteId));
      if (addonStatus == null)
      {
        addonStatus = new AddonStatus();
        addonStatus.SiteId = siteId;
        addonStatus.AddonId = addon.Id;
        addon.Statuses.Add(addonStatus);
      }
      addonStatus.DataStatus = dataStatus;
      addonStatus.StructureStatus = structureStatus;
    }

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" />.
    /// </summary>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /></returns>
    public AddonLink CreateAddonLink() => this.Provider.CreateAddonLink();

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /></param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /></returns>
    public AddonLink CreateAddonLink(Guid id) => this.Provider.CreateAddonLink(id);

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" />.
    /// </summary>
    /// <param name="addonLink">The <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /> to delete.</param>
    public void DeleteAddonLink(AddonLink addonLink) => this.Provider.DeleteAddonLink(addonLink);

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /></param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /></returns>
    public AddonLink GetAddonLink(Guid id) => this.Provider.GetAddonLink(id);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.Packaging.Model.AddonLink" /> items</returns>
    public IQueryable<AddonLink> GetAddonLinks() => this.Provider.GetAddonLinks();

    Lock ISyncLockSupport.CreateLock(string lockId) => this.Provider.CreateLock(lockId);

    IQueryable<Lock> ISyncLockSupport.GetLocks() => this.Provider.GetLocks();

    void ISyncLockSupport.DeleteLock(Lock obj) => this.Provider.DeleteLock(obj);
  }
}
