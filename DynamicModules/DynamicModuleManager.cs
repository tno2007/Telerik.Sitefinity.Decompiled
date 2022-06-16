// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModuleManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.DynamicModules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.DynamicModules
{
  /// <summary>
  /// Manager class used by all dynamic modules. This class provides CRUD operations for the types of the dynamic modules.
  /// </summary>
  [BlankItemDelegate(typeof (DynamicModuleManager), "GetBlankItem")]
  public class DynamicModuleManager : 
    ManagerBase<DynamicModuleDataProvider>,
    ILifecycleManager,
    IManager,
    IDisposable,
    IProviderResolver,
    ILanguageDataManager,
    IDataSource,
    IGeoLocationManager,
    IRelatedDataSource,
    ISupportRecyclingManager
  {
    /// <summary>Returns the "updated-dynamic-content" string.</summary>
    public const string UpdateDynamicContentState = "updated-dynamic-content";
    /// <summary>Returns the "deleted-items-dynamic-content" string.</summary>
    public const string DeletedItemsDynamicContentState = "deleted-items-dynamic-content";
    private const string DeletedDynamicItemsKey = "deleted-dynamic-content";
    internal const string DefaultItemUrlName = "default-{0}";
    private const string ModuleNameConst = "DynamicModule";
    private ModuleBuilderManager moduleBuilderManager;
    private IEnumerable<DataProviderInfo> providerInfos;
    private IRecycleBinStrategy recycleBin;

    static DynamicModuleManager()
    {
      ManagerBase<DynamicModuleDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(DynamicModuleManager.Provider_Executing);
      ManagerBase<DynamicModuleDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(DynamicModuleManager.Provider_Executed);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> class with the default provider.
    /// </summary>
    public DynamicModuleManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> class with the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider to be used by the manager.</param>
    public DynamicModuleManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> class with the specified provider
    /// and transaction name.
    /// </summary>
    /// <param name="providerName">The name of the provider to be used by the manager.</param>
    /// <param name="transactionName">The name of the transaction to use for data operations.</param>
    public DynamicModuleManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <inheritdoc />
    public string Name => this.GetType().FullName;

    /// <inheritdoc />
    public string Title => this.ModuleName;

    /// <inheritdoc />
    public bool CanCreateProvider => true;

    /// <summary>
    /// Gets the strategy that encapsulates the lifecycle logic &amp; operations - check in/out/publish/un-publish/etc
    /// </summary>
    public ILifecycleDecorator Lifecycle => LifecycleFactory.CreateLifecycle((ILifecycleManager) this, new LifecycleItemCopyDelegate(this.CopyDynamicContent), new Type[1]
    {
      typeof (DynamicContent)
    });

    /// <inheritdoc />
    public string ProviderNameDefaultPrefix => "dynamicProvider";

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> AllProviders => this.GetAllProviderInfos<DynamicModuleDataProvider, DynamicModulesConfig>();

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> ProviderInfos
    {
      get
      {
        if (this.providerInfos == null)
          this.providerInfos = this.GetProviderInfos<DynamicModuleDataProvider>();
        return this.providerInfos;
      }
    }

    IEnumerable<DataProviderInfo> IDataSource.Providers => this.AllProviders;

    /// <summary>
    /// Gets the strategy that encapsulates the Recycle Bin operations like moving an item to, and restoring from the Recycle Bin.
    /// </summary>
    public IRecycleBinStrategy RecycleBin
    {
      get
      {
        if (this.recycleBin == null)
          this.recycleBin = RecycleBinStrategyFactory.CreateRecycleBin((IManager) this);
        return this.recycleBin;
      }
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    public override string ModuleName => "DynamicModule";

    /// <summary>
    /// Gets or sets an array of data source names that this data source depends on.
    /// For example Ecommerce Orders may depend on Ecommerce Products.
    /// </summary>
    /// <value></value>
    public string[] DependantDataSources { get; set; }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    internal ModuleBuilderManager ModuleBuilderMgr
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager(string.Empty, this.TransactionName);
        return this.moduleBuilderManager;
      }
    }

    /// <summary>
    /// Returns a blank <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> object from specified type.
    /// This object is bound when a new item is created.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> substitution object ready for serialization.</returns>
    public static object GetBlankItem(Type contentType) => DynamicModuleManager.GetBlankItem(contentType, (string) null);

    /// <summary>
    /// Returns a blank <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> object from specified type.
    /// This object is bound when a new item is created.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="providerName">Name of the provider in which the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> object should be created.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> substitution object ready for serialization.</returns>
    public static object GetBlankItem(Type contentType, string providerName)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(providerName);
      object dataItem;
      using (new ElevatedModeRegion((IManager) manager))
        dataItem = (object) manager.CreateDataItem(contentType);
      return new DynamicFieldsDataContractSurrogate().GetObjectToSerialize(dataItem, contentType);
    }

    /// <summary>
    /// Gets the default name of the provider for the specified dynamic module.
    /// </summary>
    /// <param name="dynamicModuleName">Name of the dynamic module.</param>
    /// <returns>The default name of the provider for the specified dynamic module.</returns>
    public static string GetDefaultProviderName(string dynamicModuleName)
    {
      if (!Bootstrapper.IsReady)
        return ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
      MultisiteContext.SiteDataSourceLinkProxy defaultProvider = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(dynamicModuleName);
      return defaultProvider == null ? ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName() : defaultProvider.ProviderName;
    }

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> with the default provider.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" />.</returns>
    public static DynamicModuleManager GetManager() => ManagerBase<DynamicModuleDataProvider>.GetManager<DynamicModuleManager>();

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> with the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider with which the manager ought to be initialized.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" />.</returns>
    public static DynamicModuleManager GetManager(string providerName) => ManagerBase<DynamicModuleDataProvider>.GetManager<DynamicModuleManager>(providerName);

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> with specified provider name
    /// and transaction name.
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider with which the manager ought to be initialized.
    /// </param>
    /// <param name="transactionName">
    /// Name of the transaction in which the manager ought to be participate.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" />.</returns>
    public static DynamicModuleManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<DynamicModuleDataProvider>.GetManager<DynamicModuleManager>(providerName, transactionName);
    }

    /// <summary>
    /// Recompiles the urls of all data items from the given type.
    /// </summary>
    /// <param name="itemType">Type of the items.</param>
    public void RecompileDataItemsUrls(Type itemType)
    {
      IQueryable<DynamicContent> dataItems = this.GetDataItems(itemType);
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      foreach (DynamicContent dynamicContent in (IEnumerable<DynamicContent>) dataItems)
      {
        if (!dynamicContent.IsDeleted)
        {
          foreach (CultureInfo availableCulture in dynamicContent.AvailableCultures)
          {
            SystemManager.CurrentContext.Culture = availableCulture;
            this.Provider.RecompileItemUrls<DynamicContent>(dynamicContent);
            this.RecompileChildrenUrlsHierarchically(dynamicContent);
          }
        }
      }
      SystemManager.CurrentContext.Culture = culture;
    }

    /// <summary>
    /// Recompiles the urls of all items under the given item in the hierarchy.
    /// </summary>
    /// <param name="item">The item.</param>
    public void RecompileChildrenUrlsHierarchically(DynamicContent item) => this.Provider.RecompileChildrenUrlsHierarchically<DynamicContent>((IHierarchicalItem) item);

    /// <summary>Creates a language data instance</summary>
    /// <returns>A language data instance</returns>
    public LanguageData CreateLanguageData() => this.Provider.CreateLanguageData();

    /// <summary>Creates a language data instance</summary>
    /// <param name="id">The id.</param>
    /// <returns>A language data instance with the specified id.</returns>
    public LanguageData CreateLanguageData(Guid id) => this.Provider.CreateLanguageData(id);

    /// <summary>Gets language data instance by its Id</summary>
    /// <param name="id">The id.</param>
    /// <returns>The language data instance by the specified Id</returns>
    public LanguageData GetLanguageData(Guid id) => this.Provider.GetLanguageData(id);

    /// <inheritdoc />
    [Obsolete("Use CreateProvider(providerName, providerTitle, parameters) instead.")]
    public string CreateProvider(string providerTitle, NameValueCollection parameters) => this.CreateProvider(ManagerExtensions.GenerateProviderName(this.ProviderNameDefaultPrefix, ConfigManager.GetManager().GetSection<DynamicModulesConfig>().Providers.Values.Select<DataProviderSettings, string>((Func<DataProviderSettings, string>) (p => p.Name))), providerTitle, parameters);

    /// <inheritdoc />
    public string CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters)
    {
      this.RegisterProvider<DynamicModuleDataProvider>(typeof (DynamicModulesConfig), providerName, providerTitle, parameters);
      this.providerInfos = (IEnumerable<DataProviderInfo>) null;
      return providerName;
    }

    /// <inheritdoc />
    public void DeleteProvider(string providerName)
    {
      ConfigManager manager = ConfigManager.GetManager();
      DynamicModulesConfig section = manager.GetSection<DynamicModulesConfig>();
      section.Providers.Remove(providerName);
      manager.SaveSection((ConfigSection) section);
      this.RemoveProvider(providerName);
    }

    /// <inheritdoc />
    public void EnableProvider(string providerName) => this.SetProviderState(true, providerName);

    /// <inheritdoc />
    public void DisableProvider(string providerName) => this.SetProviderState(false, providerName);

    /// <inheritdoc />
    public string GetDefaultProvider() => ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();

    /// <inheritdoc />
    IQueryable<T> IGeoLocationManager.FilterByGeoLocation<T>(
      IQueryable<T> itemsList,
      double latitude,
      double longitude,
      double radius,
      out IEnumerable<IGeoLocation> geoLocationsList,
      ItemFilter itemFilter = null)
    {
      return this.provider.FilterByGeoLocation<T>(itemsList, latitude, longitude, radius, out geoLocationsList, itemFilter);
    }

    /// <inheritdoc />
    IEnumerable<T> IGeoLocationManager.SortByDistance<T>(
      IEnumerable<T> itemsList,
      IEnumerable<IGeoLocation> geoLocationsList,
      double latitude,
      double longitude,
      DistanceSorting distanceSorting)
    {
      return this.provider.SortByDistance<T>(itemsList, geoLocationsList, latitude, longitude, distanceSorting);
    }

    /// <inheritdoc />
    public virtual IQueryable<T> GetRelatedItems<T>(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
      where T : IDataItem
    {
      return Queryable.OfType<T>(this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, typeof (T), status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection));
    }

    /// <inheritdoc />
    public virtual IQueryable GetRelatedItems(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
    {
      return this.Provider is IRelatedDataSource ? ((IRelatedDataSource) this.Provider).GetRelatedItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection) : RelatedDataHelper.GetRelatedItemsViaContains(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, (DataProviderBase) this.Provider, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection);
    }

    /// <inheritdoc />
    public Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      return this.Provider is IRelatedDataSource ? ((IRelatedDataSource) this.Provider).GetRelatedItems(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, status) : RelatedDataHelper.GetRelatedItemsViaContains(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, (DataProviderBase) this.Provider, status);
    }

    /// <inheritdoc />
    public IEnumerable<IDataItem> GetRelatedItemsList(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      return this.Provider is IRelatedDataSource ? ((IRelatedDataSource) this.Provider).GetRelatedItemsList(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, status) : RelatedDataHelper.GetRelatedItemsListViaContains(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, (DataProviderBase) this.Provider, status);
    }

    /// <summary>
    /// Creates a new item of the specified type and returns it.
    /// </summary>
    /// <param name="itemType">The type of the item to be created.</param>
    /// <returns>The newly created item.</returns>
    public DynamicContent CreateDataItem(Type itemType) => this.Provider.CreateDataItem(itemType);

    /// <summary>
    /// Create a new item of the specified type with the given id and application name.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <param name="id">Id with which the item should be created.</param>
    /// <param name="applicationName">Name of the application under which the item should be created.</param>
    /// <returns>An instance of the newly created item.</returns>
    public DynamicContent CreateDataItem(
      Type itemType,
      Guid id,
      string applicationName)
    {
      return this.Provider.CreateDataItem(itemType, id, applicationName);
    }

    /// <summary>Gets the item (by id) of the specified type.</summary>
    /// <param name="itemType">Type of the item to retrieve.</param>
    /// <param name="id">Id of the item to be retrieved.</param>
    /// <returns>An instance of the item.</returns>
    public DynamicContent GetDataItem(Type itemType, Guid id) => this.Provider.GetDataItem(itemType, id);

    /// <summary>Gets the query of dynamic data items.</summary>
    /// <param name="itemType">Type of data items for which to get the query.</param>
    /// <returns>The query of <see cref="!:IDynamicDataItem" /> objects.</returns>
    public IQueryable<DynamicContent> GetDataItems(Type itemType) => this.Provider.GetDataItems(itemType);

    /// <summary>
    /// Gets the count of specified type after applying the filter expression.
    /// </summary>
    /// <param name="itemType">Type of the items to count.</param>
    /// <param name="filterExpression">Filter expression (dynamic linq syntax) to apply before performing the count. Pass null if you want to count all items.</param>
    /// <returns>The number of items of specified type that fit the filter expression.</returns>
    public int GetCount(Type itemType, string filterExpression) => this.Provider.GetCount(itemType, filterExpression);

    /// <summary>Deletes the item of specified type by it's id.</summary>
    /// <param name="itemType">Type of the item to be deleted.</param>
    /// <param name="id">Id of the item to be deleted.</param>
    public void DeleteDataItem(Type itemType, Guid id) => this.DeleteDataItem(this.GetDataItem(itemType, id));

    /// <summary>Deletes the item.</summary>
    /// <param name="item">Item to be deleted.</param>
    /// <param name="language">The language version to delete.</param>
    public void DeleteDataItem(DynamicContent item, CultureInfo language = null)
    {
      string[] strArray = item != null ? this.GetAvailableLanguages(item, false) : throw new ArgumentNullException(nameof (item));
      bool flag;
      if (language == null || !SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        flag = true;
      }
      else
      {
        if (strArray.Length == 0)
          strArray = item.AvailableLanguages;
        flag = this.IsTheLastAvailableLanguage(language.Name, strArray);
      }
      if (flag)
      {
        this.DeleteItem(item);
        if ((item.Status == ContentLifecycleStatus.Master ? 1 : (item.Status == ContentLifecycleStatus.Deleted ? 1 : 0)) == 0)
          return;
        ILifecycleDataItem live = this.Lifecycle.GetLive((ILifecycleDataItem) item);
        if (live == null)
          return;
        this.RegisterDeleteOperation((object) live, strArray);
      }
      else
      {
        this.DeleteLanguageVersion((object) item, language);
        if (!(this.Lifecycle.GetLive((ILifecycleDataItem) item) is DynamicContent live))
          return;
        this.DeleteLanguageVersion((object) live, language);
      }
    }

    /// <summary>Deletes all items of the specified type.</summary>
    /// <param name="itemType">Type of the item.</param>
    public void DeleteDataItems(Type itemType)
    {
      foreach (DynamicContent dataItem in (IEnumerable<DynamicContent>) this.GetDataItems(itemType))
        this.DeleteDataItem(dataItem);
    }

    /// <summary>
    /// Refresh the item with values from the store.
    /// Reads all data and overwrites the dirty fields. The object will be clean, the
    /// changes are lost.
    /// </summary>
    /// <param name="item">The dynamic item</param>
    public void RefreshItem(DynamicContent item) => this.Provider.RefreshItem(item);

    /// <summary>
    /// Populates the ChildItems property of the given item and all its successors.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childType">Child items from all types are loaded. Keep this for backward compatibility.</param>
    [Obsolete("This method will be removed in future releases. Use the GetChildItems method instead.")]
    public void LoadChildItemsHierarchy(DynamicContent item, Type childType = null) => this.Provider.LoadChildItemsHierarchy(item, childType);

    /// <summary>
    /// Gets collection of child items which belong to the hierarchy of the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childTypes">Types of the children.</param>
    /// <returns>Collection of child items which belong to the hierarchy of the specified item</returns>
    public List<DynamicContent> GetChildItemsHierarchy(
      DynamicContent item,
      IEnumerable<Type> childTypes = null)
    {
      List<DynamicContent> childItemsHierarchy = new List<DynamicContent>();
      if (childTypes == null || childTypes.Count<Type>() == 0)
        childTypes = DynamicContentExtensions.GetChildItemTypes(item.GetType());
      if (childTypes == null || childTypes.Count<Type>() == 0)
        return childItemsHierarchy;
      foreach (Type childType in childTypes)
      {
        item.SystemChildItems = this.GetChildItems(item, childType);
        childItemsHierarchy.AddRange((IEnumerable<DynamicContent>) item.SystemChildItems);
        if (item.SystemChildItems != null && item.SystemChildItems.Count<DynamicContent>() > 0)
        {
          IEnumerable<Type> childItemTypes = DynamicContentExtensions.GetChildItemTypes(childType);
          if (childItemTypes != null)
          {
            foreach (DynamicContent systemChildItem in (IEnumerable<DynamicContent>) item.SystemChildItems)
              childItemsHierarchy.AddRange((IEnumerable<DynamicContent>) this.GetChildItemsHierarchy(systemChildItem, childItemTypes));
          }
        }
      }
      return childItemsHierarchy;
    }

    /// <summary>
    /// Gets the child items of the current item of specified type.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childType">Type of the children. If it is null returns an empty collection </param>
    /// <returns>The child items of the current item</returns>
    public IQueryable<DynamicContent> GetChildItems(
      DynamicContent item,
      Type childType)
    {
      return this.Provider.GetChildItems(item, childType);
    }

    /// <summary>
    /// Gets the child items of list of items from specified type.
    /// </summary>
    /// <param name="ids">Parent item IDs</param>
    /// <param name="childType">Type of the children. If it is null returns a collection for all child types</param>
    /// <returns>The child items from all parents</returns>
    public IQueryable<DynamicContent> GetChildItems(
      List<Guid> ids,
      Type childType)
    {
      return this.Provider.GetChildItems(ids, childType);
    }

    /// <summary>
    /// Determines whether [has child items] [the specified item].
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    ///   <c>true</c> if [has child items] [the specified item]; otherwise, <c>false</c>.
    /// </returns>
    public bool HasChildItems(DynamicContent item)
    {
      foreach (Type childItemType in DynamicContentExtensions.GetChildItemTypes(item.GetType()))
      {
        item.SystemHasChildItems = this.GetChildItems(item, childItemType).Any<DynamicContent>();
        if (item.SystemHasChildItems)
          break;
      }
      return item.SystemHasChildItems;
    }

    /// <summary>
    /// Gets the successor items of specified type for specific parent. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="parent">The parent item for which to retrieve the child items</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <returns>Collection of child items</returns>
    public IQueryable<DynamicContent> GetItemSuccessors(
      DynamicContent parent,
      Type childItemsType)
    {
      return this.Provider.GetItemSuccessors(parent, childItemsType);
    }

    /// <summary>
    /// Gets the successor items for collection of parent item IDs by child type. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="parentIDs">The parent item IDs for which to retrieve the child items.</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <param name="parentItemsType">The parent items type</param>
    /// <returns>Collection of child items</returns>
    public IQueryable<DynamicContent> GetItemSuccessors(
      List<Guid> parentIDs,
      Type childItemsType,
      Type parentItemsType)
    {
      return this.Provider.GetItemsSuccessors(parentIDs, childItemsType, parentItemsType);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item) => this.DeleteDataItem(item as DynamicContent);

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="language">The language version to delete.</param>
    public override void DeleteItem(object item, CultureInfo language) => this.DeleteDataItem(item as DynamicContent, language);

    /// <inheritdoc />
    public override IEnumerable<Type> GetKnownTypes() => this.ModuleBuilderMgr.GetKnownTypes();

    /// <summary>
    /// Updates the content links.
    /// Traverses all content links from all fields and sets the properties related to the dynamic item:
    /// ParentItemId, ParentItemType, ComponentPropertyName, ParentItemProviderName, ApplicationName
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="contentLinksManager">The content links manager.</param>
    public void UpdateContentLinks(DynamicContent item, ContentLinksManager contentLinksManager)
    {
      Guid guid = item.Id;
      if (item.Status != ContentLifecycleStatus.Master)
        guid = item.OriginalContentId;
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) item))
      {
        if (property.PropertyType == typeof (ContentLink[]) && property.GetValue((object) item) is ContentLink[] contentLinkArray)
        {
          for (int index = 0; index < contentLinkArray.Length; ++index)
          {
            ContentLink contentLink = contentLinkArray[index];
            contentLink.ParentItemId = guid;
            contentLink.ParentItemType = item.GetType().FullName;
            contentLink.ComponentPropertyName = property.Name;
            contentLink.ParentItemProviderName = ((IDataProviderBase) item.Provider).Name;
            contentLink.ApplicationName = contentLinksManager.Provider.ApplicationName;
          }
        }
      }
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Data.DataSource.DataProviderInfo" /> objects associated with the specified dynamic module.
    /// </summary>
    /// <param name="dynamicModuleName">Name of the dynamic module.</param>
    /// <returns>Collection of <see cref="T:Telerik.Sitefinity.Data.DataSource.DataProviderInfo" /> objects associated with the specified dynamic module.</returns>
    [Obsolete("This method returns only static providers. For all providers use extension method GetAllDataProviderInfos of IManager from Telerik.Sitefinity.Data namespace.")]
    public IEnumerable<DataProviderInfo> GetProviderInfos(
      string dynamicModuleName)
    {
      return this.GetProviderInfos<DynamicModuleDataProvider>(this.StaticProviders.Where<DynamicModuleDataProvider>((Func<DynamicModuleDataProvider, bool>) (p => p.ModuleName == dynamicModuleName || p.ModuleName.IsNullOrEmpty())));
    }

    /// <summary>Gets the valid providers for the current context</summary>
    /// <param name="moduleName">The module's name.</param>
    /// <returns>Collection of valid providers for the current context</returns>
    public IEnumerable<DataProviderBase> GetContextProviders(
      string moduleName)
    {
      return this.GetSiteProvidersInternal(moduleName);
    }

    /// <summary>
    /// Determines whether the specified item has content link (media) fields
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>True or false whether the specified item has content link (media) fields.</returns>
    internal static bool HasContentLinksFields(DynamicContent item)
    {
      foreach (PropertyDescriptor property in new DynamicFieldsTypeDescriptionProvider().GetTypeDescriptor(item.GetType(), (object) item).GetProperties())
      {
        if (property.PropertyType == typeof (ContentLink[]))
          return true;
      }
      return false;
    }

    /// <summary>Handles the Executing event of the Provider control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      DynamicModuleDataProvider provider1 = sender as DynamicModuleDataProvider;
      IList dirtyItems = provider1.GetDirtyItems();
      if (dirtyItems.Count == 0)
        return;
      Dictionary<Guid, string> dictionary1 = new Dictionary<Guid, string>();
      List<Guid> guidList1 = new List<Guid>();
      List<Guid> guidList2 = new List<Guid>();
      bool flag1 = false;
      if (provider1.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker data)
        flag1 = data.SkipAutoTracking;
      dictionary2 = (Dictionary<Guid, string>) null;
      bool flag2 = DynamicModuleManager.ShouldManuallyDeleteItems(provider1);
      if (flag2 && !(provider1.GetExecutionStateData("deleted-dynamic-content") is Dictionary<Guid, string> dictionary2))
        dictionary2 = new Dictionary<Guid, string>();
      bool suppressSecurityChecks = provider1.SuppressSecurityChecks;
      bool suppressEvents = provider1.SuppressEvents;
      try
      {
        provider1.SuppressSecurityChecks = true;
        provider1.SuppressEvents = true;
        for (int index = 0; index < dirtyItems.Count; ++index)
        {
          object itemInTransaction = dirtyItems[index];
          SecurityConstants.TransactionActionType dirtyItemStatus = provider1.GetDirtyItemStatus(itemInTransaction);
          if (itemInTransaction is DynamicContent)
          {
            DynamicContent dynamicContent = (DynamicContent) itemInTransaction;
            if (!flag1)
            {
              if (data == null)
                data = new TaxonomyStatisticsTracker();
              data.Track((object) dynamicContent, (DataProviderBase) provider1);
            }
            switch (dirtyItemStatus)
            {
              case SecurityConstants.TransactionActionType.Updated:
                if (DynamicModuleManager.HasContentLinksFields(dynamicContent))
                {
                  dictionary1[dynamicContent.Id] = dynamicContent.GetType().FullName;
                  break;
                }
                break;
              case SecurityConstants.TransactionActionType.Deleted:
                guidList1.Add(dynamicContent.Id);
                if (dynamicContent.Status == ContentLifecycleStatus.Temp)
                {
                  dictionary1[dynamicContent.OriginalContentId] = dynamicContent.GetType().FullName;
                  IContentLinksManager mappedRelatedManager = provider1.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
                  RelatedDataHelper.DeleteNotUsedRelations((IDataItem) dynamicContent, mappedRelatedManager);
                }
                else if (dynamicContent.Status == ContentLifecycleStatus.Master || dynamicContent.Status == ContentLifecycleStatus.Deleted)
                {
                  dictionary1.Remove(dynamicContent.Id);
                  IContentLinksManager mappedRelatedManager = provider1.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
                  RelatedDataHelper.DeleteItemRelations((IDataItem) dynamicContent, mappedRelatedManager);
                  PackagingOperations.DeleteAddonLinks(dynamicContent.Id, dynamicContent.GetType().FullName);
                  guidList2.Add(dynamicContent.Id);
                  ContentHelper.DeleteVersionItem(provider1.GetMappedRelatedManager<Item>(string.Empty) as VersionManager, dynamicContent.Id);
                }
                if (flag2)
                {
                  IOpenAccessDataProvider provider2 = provider1 as IOpenAccessDataProvider;
                  dictionary2[dynamicContent.Id] = DynamicModuleManager.GetDynamicItemTableName(dynamicContent, provider2.GetContext());
                  continue;
                }
                continue;
            }
            if (dynamicContent.Status != ContentLifecycleStatus.Master)
            {
              if (dynamicContent.Status == ContentLifecycleStatus.Temp)
              {
                switch (dirtyItemStatus)
                {
                  case SecurityConstants.TransactionActionType.New:
                    break;
                  case SecurityConstants.TransactionActionType.Updated:
                    if (!(dynamicContent.ItemDefaultUrl.ToString() == "/"))
                      continue;
                    break;
                  default:
                    continue;
                }
              }
              else
                continue;
            }
            string propertyName = "UrlName_" + LstringPropertyDescriptor.GetCultureSuffix(SystemManager.CurrentContext.CurrentSite.PublicCultures.Count > 1 ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture);
            string originalValue1 = provider1.GetOriginalValue<string>(dynamicContent, propertyName);
            Lstring urlName1 = dynamicContent.UrlName;
            object originalValue2 = provider1.GetOriginalValue<object>(dynamicContent, "SystemParentId");
            Guid systemParentId1 = dynamicContent.SystemParentId;
            if (urlName1 != (Lstring) originalValue1 || !systemParentId1.Equals(originalValue2))
              provider1.RecompileItemUrls<DynamicContent>(dynamicContent);
            if (dynamicContent.ApprovalWorkflowState == (Lstring) "Published" && dynamicContent.Status != ContentLifecycleStatus.Temp && dynamicContent.Status != ContentLifecycleStatus.PartialTemp)
            {
              if (urlName1 != (Lstring) originalValue1 || !systemParentId1.Equals(originalValue2))
              {
                provider1.RecompileChildrenUrlsHierarchically<DynamicContent>(itemInTransaction as IHierarchicalItem);
              }
              else
              {
                DynamicContent dynamicContent1 = provider1.GetDataItems(dynamicContent.GetType()).FirstOrDefault<DynamicContent>((Expression<Func<DynamicContent, bool>>) (dc => dc.OriginalContentId == dynamicContent.Id && (int) dc.Status == 2));
                if (dynamicContent1 != null)
                {
                  Lstring urlName2 = dynamicContent1.UrlName;
                  string originalValue3 = provider1.GetOriginalValue<string>(dynamicContent1, propertyName);
                  Guid systemParentId2 = dynamicContent1.SystemParentId;
                  object originalValue4 = provider1.GetOriginalValue<object>(dynamicContent1, "SystemParentId");
                  Lstring lstring = (Lstring) originalValue3;
                  if (urlName2 != lstring || !systemParentId2.Equals(originalValue4))
                    provider1.RecompileChildrenUrlsHierarchically<DynamicContent>((IHierarchicalItem) dynamicContent1);
                }
              }
            }
          }
        }
      }
      finally
      {
        provider1.SuppressSecurityChecks = suppressSecurityChecks;
        provider1.SuppressEvents = suppressEvents;
      }
      if (guidList2.Any<Guid>())
        provider1.SetExecutionStateData("deleted_workflow_items", (object) guidList2);
      if (dictionary1.Any<KeyValuePair<Guid, string>>())
        provider1.SetExecutionStateData("updated-dynamic-content", (object) dictionary1);
      if (guidList1.Any<Guid>())
        provider1.SetExecutionStateData("deleted-items-dynamic-content", (object) guidList1);
      if (dictionary2 != null && dictionary2.Any<KeyValuePair<Guid, string>>())
        provider1.SetExecutionStateData("deleted-dynamic-content", (object) dictionary2);
      if (data == null || !data.HasChanges())
        return;
      provider1.SetExecutionStateData("taxonomy_statistics_changes", (object) data);
    }

    /// <summary>Handles the Executed event of the Provider control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      DynamicModuleDataProvider provider = sender as DynamicModuleDataProvider;
      ContentLinksManager contentLinksManager = (ContentLinksManager) null;
      bool suppressSecurityChecks = provider.SuppressSecurityChecks;
      bool suppressEvents = provider.SuppressEvents;
      try
      {
        provider.SuppressSecurityChecks = true;
        provider.SuppressEvents = true;
        if (provider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker executionStateData1)
        {
          executionStateData1.SaveChanges();
          provider.SetExecutionStateData("taxonomy_statistics_changes", (object) null);
        }
        if (provider.GetExecutionStateData("updated-dynamic-content") is Dictionary<Guid, string> executionStateData2)
        {
          contentLinksManager = provider.GetRelatedManager<ContentLinksManager>((string) null);
          foreach (KeyValuePair<Guid, string> keyValuePair in executionStateData2)
          {
            Type itemType = TypeResolutionService.ResolveType(keyValuePair.Value);
            DynamicContent dataItem = provider.GetDataItem(itemType, keyValuePair.Key);
            DynamicContent masterItem = dataItem;
            if (dataItem.Status != ContentLifecycleStatus.Master && dataItem.Status != ContentLifecycleStatus.Deleted)
              masterItem = provider.GetDataItem(dataItem.GetType(), dataItem.OriginalContentId);
            IQueryable<DynamicContent> source = provider.GetDataItems(dataItem.GetType()).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.OriginalContentId == masterItem.Id));
            contentLinksManager.RemoveRedundantContentLinks((ILifecycleDataItem) masterItem, (ILifecycleDataItem[]) source.ToArray<DynamicContent>());
          }
          provider.SetExecutionStateData("updated-dynamic-content", (object) null);
        }
        if (provider.GetExecutionStateData("deleted-items-dynamic-content") is List<Guid> executionStateData3)
        {
          if (contentLinksManager == null)
            contentLinksManager = provider.GetRelatedManager<ContentLinksManager>((string) null);
          int count = 100;
          int num1 = executionStateData3.Count<Guid>();
          int num2 = 0;
          if (num1 > 0)
            num2 = (int) Math.Ceiling((double) num1 / (double) count);
          for (int index = 0; index < num2; ++index)
          {
            Guid[] pageItems = executionStateData3.Skip<Guid>(index * count).Take<Guid>(count).ToArray<Guid>();
            IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
            Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => pageItems.Contains<Guid>(cl.ParentItemId));
            foreach (ContentLink contentLink in (IEnumerable<ContentLink>) contentLinks.Where<ContentLink>(predicate))
              contentLinksManager.Delete(contentLink);
          }
          provider.SetExecutionStateData("deleted-items-dynamic-content", (object) null);
        }
        if (provider.GetExecutionStateData("deleted-dynamic-content") is Dictionary<Guid, string> executionStateData4 && executionStateData4.Any<KeyValuePair<Guid, string>>())
        {
          if (provider is IOpenAccessDataProvider)
          {
            try
            {
              SitefinityOAContext context = (provider as IOpenAccessDataProvider).GetContext();
              foreach (KeyValuePair<Guid, string> keyValuePair in executionStateData4)
                DynamicModuleManager.DeleteFromForeignTables(keyValuePair.Key, keyValuePair.Value, context);
              context.SaveChanges();
            }
            catch (Exception ex)
            {
              Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
            }
          }
          provider.SetExecutionStateData("deleted-dynamic-content", (object) null);
        }
        IApprovalWorkflowExtensions.DeleteApprovalRecords((DataProviderBase) provider);
        contentLinksManager?.Provider.CommitTransaction();
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.ModuleBuilder);
      }
      finally
      {
        provider.SuppressSecurityChecks = suppressSecurityChecks;
        provider.SuppressEvents = suppressEvents;
      }
    }

    private static void DeleteFromForeignTables(
      Guid id,
      string tableName,
      SitefinityOAContext context)
    {
      if (string.IsNullOrEmpty(tableName))
        return;
      string commandText = string.Format("delete from {0} where base_id=@p1", (object) tableName);
      OAParameter oaParameter = new OAParameter("@p1", (object) id);
      oaParameter.DbType = DbType.Guid;
      context.ExecuteNonQuery(commandText, (DbParameter) oaParameter);
    }

    private static string GetDynamicItemTableName(DynamicContent item, SitefinityOAContext context) => context.Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => pt.FullName == item.GetType().FullName))?.Table.Name;

    internal string[] GetAvailableLanguages(DynamicContent item, bool includeInvariantLanguage = true)
    {
      IEnumerable<string> source = (IEnumerable<string>) new string[0];
      PropertyDescriptor typeMainProperty = ModuleBuilderManager.GetTypeMainProperty(item.GetType());
      if (typeMainProperty != null)
      {
        Lstring lstring = typeMainProperty.GetValue((object) item) as Lstring;
        if (lstring != (Lstring) null)
          source = lstring.GetAvailableLanguagesIgnoringContext().Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name));
      }
      if (!includeInvariantLanguage)
        source = source.Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name));
      return source.ToArray<string>();
    }

    /// <summary>
    /// Moves the data items from the specified type to default item the parent type.
    /// </summary>
    /// <param name="dynamicType">The dynamic module type.</param>
    internal void MoveDataItemsToDefaultParentItem(DynamicModuleType dynamicType)
    {
      Type type = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
      if (this.GetDataItems(type).Count<DynamicContent>() == 0)
        return;
      if (dynamicType.ParentModuleTypeId == Guid.Empty)
      {
        this.ChangeDataItemsParent(type, Guid.Empty, (string) null);
      }
      else
      {
        DynamicContent defaultDataItem = this.GetDefaultDataItem(dynamicType.ParentModuleType);
        if (defaultDataItem == null)
        {
          defaultDataItem = this.CreateDefaultDataItem(dynamicType.ParentModuleType);
          this.BuildDataItemParentsHierarchy(defaultDataItem);
        }
        if (dynamicType.IsSelfReferencing)
          this.ChangeDataItemsParent((IList<DynamicContent>) this.GetDataItems(type).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => (Guid?) i.SystemParentId == new Guid?() || i.SystemParentId == Guid.Empty)).ToList<DynamicContent>(), defaultDataItem.Id, dynamicType.ParentModuleType.GetFullTypeName());
        else
          this.ChangeDataItemsParent(type, defaultDataItem.Id, dynamicType.ParentModuleType.GetFullTypeName());
      }
    }

    /// <summary>
    /// Moves the child items of the specified items to parent item.
    /// </summary>
    /// <param name="rootLevelItems">The root level items.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="parentType">Type of the parent.</param>
    internal void MoveChildItemsToParentItem(
      List<DynamicContent> rootLevelItems,
      Guid parentId,
      string parentType)
    {
      foreach (DynamicContent rootLevelItem in rootLevelItems)
        this.ChangeDataItemsParent((IList<DynamicContent>) this.GetChildItemsHierarchy(rootLevelItem), parentId, parentType);
    }

    /// <summary>
    /// Gets the default data item from the given dynamic type.
    /// </summary>
    /// <param name="dynamicModuleType">Type dynamic type.</param>
    /// <returns>The default item.</returns>
    internal DynamicContent GetDefaultDataItem(DynamicModuleType dynamicModuleType)
    {
      string dynamicModuleTypeFullName = dynamicModuleType.GetFullTypeName();
      Type itemType = TypeResolutionService.ResolveType(dynamicModuleTypeFullName);
      string title = Telerik.Sitefinity.Localization.Res.Get<DynamicModuleResources>().MovedItemsParentTitle;
      string predicate = string.Format("{0} = \"{1}\"", (object) dynamicModuleType.MainShortTextFieldName, (object) title);
      return this.GetDataItems(itemType).Where<DynamicContent>(predicate).FirstOrDefault<DynamicContent>() ?? this.Provider.GetDirtyItems().OfType<DynamicContent>().Where<DynamicContent>((Func<DynamicContent, bool>) (t => t.GetType().FullName == dynamicModuleTypeFullName && t.GetValue(dynamicModuleType.MainShortTextFieldName) != null && t.GetValue(dynamicModuleType.MainShortTextFieldName).ToString() == title)).FirstOrDefault<DynamicContent>();
    }

    /// <summary>
    /// Creates the default data item from the given dynamic type.
    /// </summary>
    /// <param name="dynamicModuleType">Type dynamic type.</param>
    /// <returns>The default item.</returns>
    internal DynamicContent CreateDefaultDataItem(DynamicModuleType dynamicModuleType)
    {
      Type itemType = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName());
      string itemsParentTitle = Telerik.Sitefinity.Localization.Res.Get<DynamicModuleResources>().MovedItemsParentTitle;
      using (new CultureRegion(CultureInfo.InvariantCulture))
      {
        DynamicContent dataItem = this.CreateDataItem(itemType);
        dataItem.SetValue(dynamicModuleType.MainShortTextFieldName, (object) itemsParentTitle);
        dataItem.SetValue("Owner", (object) SecurityManager.GetCurrentUserId());
        dataItem.SetValue("PublicationDate", (object) DateTime.UtcNow);
        dataItem.LastModifiedBy = SecurityManager.GetCurrentUserId();
        dataItem.UrlName = (Lstring) string.Format("default-{0}", (object) dynamicModuleType.TypeName).ToLower();
        dataItem.SetWorkflowStatus(this.Provider.ApplicationName, LifecycleExtensions.StatusDraft);
        this.Lifecycle.GetOrCreateLanguageData((ILifecycleDataItem) dataItem);
        this.Provider.RecompileItemUrls<DynamicContent>(dataItem);
        return dataItem;
      }
    }

    /// <summary>
    /// Builds the hierarchy of parents for the given data item.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    internal void BuildDataItemParentsHierarchy(DynamicContent dataItem)
    {
      DynamicModuleType dynamicModuleType = this.ModuleBuilderMgr.GetDynamicModuleType(dataItem.GetType());
      if (dynamicModuleType.ParentModuleType != null)
      {
        DynamicContent dataItem1 = this.GetDefaultDataItem(dynamicModuleType.ParentModuleType) ?? this.CreateDefaultDataItem(dynamicModuleType.ParentModuleType);
        dataItem.SystemParentItem = dataItem1;
        this.BuildDataItemParentsHierarchy(dataItem1);
      }
      using (new CultureRegion(CultureInfo.InvariantCulture))
        this.Provider.RecompileItemUrls<DynamicContent>(dataItem);
    }

    /// <summary>
    /// Changes the parent item of all data items from the given type.
    /// </summary>
    /// <param name="type">Type type.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="parentType">The parent item's type name.</param>
    internal void ChangeDataItemsParent(Type type, Guid parentId, string parentType) => this.ChangeDataItemsParent((IList<DynamicContent>) this.GetDataItems(type).ToList<DynamicContent>(), parentId, parentType);

    /// <summary>Changes the parent item of the specified data items.</summary>
    /// <param name="dataItems">The data items.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="parentType">The parent item's type name.</param>
    internal void ChangeDataItemsParent(
      IList<DynamicContent> dataItems,
      Guid parentId,
      string parentType)
    {
      foreach (DynamicContent dataItem in (IEnumerable<DynamicContent>) dataItems)
        dataItem.SetParent(parentId, parentType);
    }

    /// <summary>Gets the valid providers for the given site</summary>
    /// <param name="moduleName">The module's name.</param>
    /// <param name="siteId">The id of the given site.</param>
    /// <returns>Collection of valid providers for the given site</returns>
    internal IEnumerable<DataProviderBase> GetSiteProviders(
      string moduleName,
      Guid siteId)
    {
      return this.GetSiteProvidersInternal(moduleName, siteId);
    }

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal override Telerik.Sitefinity.Data.GetDefaultProvider DefaultProviderDelegate => (Telerik.Sitefinity.Data.GetDefaultProvider) (() => Config.Get<DynamicModulesConfig>().DefaultProvider);

    /// <summary>Collection of data provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<DynamicModulesConfig>().Providers;

    private static bool ShouldManuallyDeleteItems(DynamicModuleDataProvider provider)
    {
      bool flag = false;
      if (provider is IOpenAccessDataProvider provider1)
      {
        SitefinityOAContext context = provider1.GetContext();
        if (context != null)
        {
          DatabaseType dbType = context.OpenAccessConnection.DbType;
          flag = dbType == DatabaseType.MsSql || dbType == DatabaseType.SqlAzure;
        }
      }
      return flag;
    }

    private IEnumerable<DataProviderBase> GetSiteProvidersInternal(
      string moduleName,
      Guid siteId = default (Guid))
    {
      if (!(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext))
        return (IEnumerable<DataProviderBase>) this.Providers;
      if (siteId.IsEmpty())
        siteId = multisiteContext.CurrentSite.Id;
      string[] searchModules;
      if (string.IsNullOrEmpty(moduleName))
        searchModules = ModuleBuilderManager.GetModules().Active().Select<IDynamicModule, string>((Func<IDynamicModule, string>) (m => m.Name)).ToArray<string>();
      else
        searchModules = new string[1]{ moduleName };
      IEnumerable<ISiteDataSource> siteDataSources = multisiteContext.GetDataSourcesByManager(this.GetType().FullName).Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Sites.Contains<Guid>(siteId) && ((IEnumerable<string>) searchModules).Contains<string>(ds.Name)));
      HashSet<DataProviderBase> providersInternal = new HashSet<DataProviderBase>();
      foreach (ISiteDataSource siteDataSource in siteDataSources)
      {
        try
        {
          providersInternal.Add((DataProviderBase) this.ResolveProvider(siteDataSource.Provider, siteDataSource.Name));
        }
        catch
        {
        }
      }
      return (IEnumerable<DataProviderBase>) providersInternal;
    }

    private void CopyDynamicContent(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null)
    {
      DynamicContent component = source as DynamicContent;
      DynamicContent dynamicContent = destination as DynamicContent;
      dynamicContent.Urls.ClearDestinationUrls<DynamicContentUrlData>(component.Urls, new Action<DynamicContentUrlData>(((UrlDataProviderBase) this.Provider).Delete));
      component.Urls.CopyTo(dynamicContent.Urls, dynamicContent);
      dynamicContent.PublicationDate = component.PublicationDate;
      dynamicContent.ExpirationDate = component.ExpirationDate;
      dynamicContent.IncludeInSitemap = component.IncludeInSitemap;
      dynamicContent.SetParent(component.SystemParentId, component.SystemParentType);
      PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) component);
      PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties((object) dynamicContent);
      foreach (PropertyDescriptor sourceDescriptor in properties1)
      {
        PropertyDescriptor destinationDescriptor = properties2[sourceDescriptor.Name];
        MetafieldPropertyDescriptor propertyDescriptor1 = sourceDescriptor as MetafieldPropertyDescriptor;
        if (Attribute.GetCustomAttribute((MemberInfo) sourceDescriptor.PropertyType, typeof (ArtificialAssociationAttribute)) != null)
          Telerik.Sitefinity.Data.WcfHelpers.ReflectionHelper.CopyArtificialAssociation((object) source, (object) destination, sourceDescriptor, destinationDescriptor);
        else if (propertyDescriptor1 != null && propertyDescriptor1.MetaField.IsDynamic)
        {
          PropertyDescriptor propertyDescriptor2 = properties2[sourceDescriptor.Name];
          if ((destination.Status == ContentLifecycleStatus.Temp || destination.Status == ContentLifecycleStatus.PartialTemp) && propertyDescriptor2.PropertyType.Equals(typeof (DateTime)) && ((DateTime) propertyDescriptor2.GetValue((object) destination)).Equals(new DateTime()))
            propertyDescriptor2.SetValue((object) destination, (object) SqlDateTime.MinValue.Value);
          else
            propertyDescriptor2.SetValue((object) destination, sourceDescriptor.GetValue((object) source));
        }
      }
      destination.Version = source.Version;
    }

    /// <summary>Enables or disables a provider.</summary>
    /// <param name="enable">Determines whether to enable the provider (true) or disable it (false).</param>
    /// <param name="providerName">Name of the provider.</param>
    private void SetProviderState(bool enable, string providerName)
    {
      ConfigManager manager = ConfigManager.GetManager();
      DynamicModulesConfig section = manager.GetSection<DynamicModulesConfig>();
      if (!section.Providers.Keys.Contains(providerName) || section.Providers[providerName].Enabled == enable)
        return;
      section.Providers[providerName].Enabled = enable;
      manager.SaveSection((ConfigSection) section);
      if (enable)
        this.InstatiateProvider((IDataProviderSettings) section.Providers[providerName]);
      else
        this.RemoveProvider(providerName);
    }

    private void DeleteItem(DynamicContent item)
    {
      IEnumerable<Type> childItemTypes = DynamicContentExtensions.GetChildItemTypes(item.GetType());
      if (childItemTypes != null && childItemTypes.Count<Type>() > 0)
      {
        foreach (Type itemType in childItemTypes)
        {
          IQueryable<DynamicContent> dataItems = this.GetDataItems(itemType);
          Expression<Func<DynamicContent, bool>> predicate = (Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == item.Id);
          foreach (DynamicContent dynamicContent in (IEnumerable<DynamicContent>) dataItems.Where<DynamicContent>(predicate))
            this.DeleteDataItem(dynamicContent);
        }
      }
      string[] availableLanguages = this.GetAvailableLanguages(item, false);
      item.RegisterDeletedOperation(availableLanguages);
      this.Provider.DeleteDataItem(item);
    }
  }
}
