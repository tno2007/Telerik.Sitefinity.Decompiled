// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncMap.SyncMapManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.SyncMap.Configuration;
using Telerik.Sitefinity.SyncMap.Model;

namespace Telerik.Sitefinity.SyncMap
{
  /// <summary>Managers class for the Site setting.</summary>
  internal class SyncMapManager : ManagerBase<SyncMapDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SyncMap.SyncMapManager" /> class.
    /// </summary>
    public SyncMapManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SyncMap.SyncMapManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public SyncMapManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SyncMap.SyncMapManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public SyncMapManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance.
    /// </summary>
    /// <param name="moduleName">Name of the external module to syncronize with.</param>
    /// <param name="appName">Name of the app from the external module.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    public SyncApp CreateApp(string moduleName, string appName) => this.Provider.CreateApp(moduleName, appName);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> with the given id.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal SyncApp CreateApp(Guid appId, string moduleName, string appName) => this.Provider.CreateApp(appId, moduleName, appName);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> for removal.
    /// </summary>
    /// <param name="app">The app to delete.</param>
    internal void Delete(SyncApp app) => this.Provider.Delete(app);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> object with the given id.
    /// </summary>
    /// <param name="appId">The setting id.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal SyncApp GetApp(Guid appId) => this.Provider.GetApp(appId);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> object with the module and application.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="appName">Name of the app.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal SyncApp GetApp(string moduleName, string appName) => this.Provider.GetApp(moduleName, appName);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> objects.
    /// </returns>
    internal IQueryable<SyncApp> GetApps() => this.Provider.GetApps();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <param name="externalKey">The external key.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>
    /// Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance
    /// </returns>
    internal SyncMapping CreateMapping(Guid appId, string externalKey, Guid itemId) => this.Provider.CreateMapping(appId, externalKey, itemId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> for removal.
    /// </summary>
    /// <param name="app">The mapping to delete.</param>
    internal void Delete(SyncMapping mapping) => this.Provider.Delete(mapping);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" />.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <param name="externalKey">The external key.</param>
    /// <param name="itemId">The setting id.</param>
    /// <returns>
    /// Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance
    /// </returns>
    internal SyncMapping Get(Guid appId, string externalKey, Guid itemId) => this.Provider.GetMapping(appId, externalKey, itemId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> objects for the application with the specified appId.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> objects.
    /// </returns>
    internal IQueryable<SyncMapping> GetMappings(Guid appId) => this.Provider.GetMappings(appId);

    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<SyncMapConfig>().DefaultProvider);

    public override string ModuleName => string.Empty;

    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<SyncMapConfig>().Providers;

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Get an instance of the SiteSettingsManager using the default provider
    /// </summary>
    /// <returns>Instance of SiteSettingsManager</returns>
    public static SyncMapManager GetManager() => ManagerBase<SyncMapDataProvider>.GetManager<SyncMapManager>();

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static SyncMapManager GetManager(string providerName) => ManagerBase<SyncMapDataProvider>.GetManager<SyncMapManager>(providerName);

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static SyncMapManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<SyncMapDataProvider>.GetManager<SyncMapManager>(providerName, transactionName);
    }
  }
}
