// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>The manager class for the module builder module.</summary>
  public class ModuleBuilderManager : 
    ManagerBase<ModuleBuilderDataProvider>,
    IDynamicModuleSecurityManager,
    IManager,
    IDisposable,
    IProviderResolver
  {
    private static readonly Regex NameValidationRegex = new Regex("[^a-zA-Z0-9_.]+", RegexOptions.Compiled);
    private static Dictionary<string, string> mainProperties = new Dictionary<string, string>();
    private TaxonomyManager taxonomyManager;
    private MetadataManager metadataManager;
    private static IDictionary<string, bool> dynamicTypesFieldPermissionsCache = SystemManager.CreateStaticCache<string, bool>();
    private static IDictionary<string, string> dynamicmoduleNameForType = SystemManager.CreateStaticCache<string, string>();
    private static readonly object DynamicModuleCacheSync = new object();
    private const string RelatedMediaSectionName = "RelatedMedia";
    private const string RelatedDataSectionName = "RelatedData";
    private const string MainSectionName = "MainSection";
    private const string MainSectionTitle = "Main Section";
    private const string DynamicModulesCacheKey = "sf_dm_cache";

    static ModuleBuilderManager()
    {
      ManagerBase<ModuleBuilderDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(ModuleBuilderManager.Provider_Executing);
      ManagerBase<ModuleBuilderDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(ModuleBuilderManager.Provider_Executed);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> class.
    /// </summary>
    public ModuleBuilderManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> class.
    /// </summary>
    /// <param name="providerName">The name of the provider.</param>
    public ModuleBuilderManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> class.
    /// </summary>
    /// <param name="providerName">The name of the provider.</param>
    /// <param name="transactionName">The name of the transaction.</param>
    public ModuleBuilderManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    public override string ModuleName => "ModuleBuilder";

    /// <summary>Gets the module name validation regex.</summary>
    internal static Regex ModuleNameValidationRegex => ModuleBuilderManager.NameValidationRegex;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager.TaxonomyManager" />.
    /// </summary>
    internal MetadataManager MetadataManager
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = MetadataManager.GetManager((string) null, this.TransactionName);
        return this.metadataManager;
      }
    }

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => "OpenAccessProvider");

    /// <summary>Collection of data provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ModuleBuilderConfig>().Providers;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager.TaxonomyManager" />.
    /// </summary>
    protected TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager();
        return this.taxonomyManager;
      }
    }

    /// <summary>
    /// Gets the dynamic module meta cache. This cache is convenient to avoid database queries for the dynamic modules meta information.
    /// </summary>
    /// <returns>The cache</returns>
    public static DynamicModulesCache GetModules()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager["sf_dm_cache"] is DynamicModulesCache modules1))
      {
        lock (ModuleBuilderManager.DynamicModuleCacheSync)
        {
          if (!(cacheManager["sf_dm_cache"] is DynamicModulesCache modules1))
          {
            ModuleBuilderManager manager = ModuleBuilderManager.GetManager((string) null, "sf_dm_cache");
            using (new ElevatedModeRegion((IManager) manager))
              modules1 = new DynamicModulesCache(manager);
            TransactionManager.DisposeTransaction("sf_dm_cache");
            cacheManager.Add("sf_dm_cache", (object) modules1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (DynamicModule), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (DynamicModuleType), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (DynamicModuleField), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (DynamicContentProvider), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return modules1;
    }

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> with the default provider.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.</returns>
    public static ModuleBuilderManager GetManager() => ManagerBase<ModuleBuilderDataProvider>.GetManager<ModuleBuilderManager>();

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> with the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider with which the manager ought to be initialized.
    /// </param>
    /// <returns>An instance of the <see cref="!:CatalogManager" />.</returns>
    public static ModuleBuilderManager GetManager(string providerName) => ManagerBase<ModuleBuilderDataProvider>.GetManager<ModuleBuilderManager>(providerName);

    /// <summary>
    /// Gets a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> with specified provider name
    /// and transaction name.
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider with which the manager ought to be initialized.
    /// </param>
    /// <param name="transactionName">
    /// Name of the transaction in which the manager ought to be participate.
    /// </param>
    /// <returns>An instance of the <see cref="!:CatalogManager" />.</returns>
    public static ModuleBuilderManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ModuleBuilderDataProvider>.GetManager<ModuleBuilderManager>(providerName, transactionName);
    }

    /// <summary>Gets all active module's types</summary>
    /// <returns>Collection of types</returns>
    public static IEnumerable<IMetaType> GetActiveTypes() => (IEnumerable<IMetaType>) ModuleBuilderManager.GetModules().Active().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types));

    /// <inheritdoc />
    public override IEnumerable<Type> GetKnownTypes() => ModuleBuilderManager.GetModules().AllTypes().Select<IDynamicModuleType, Type>((Func<IDynamicModuleType, Type>) (t => this.ResolveDynamicClrType(t.GetFullTypeName())));

    /// <inheritdoc />
    public ISecuredObject CreateSecuredObject(
      ISecuredObject mainObject,
      string dynamicContentProviderName)
    {
      DynamicContentProvider dynamicContentProvider = this.CreateDynamicContentProvider(dynamicContentProviderName, mainObject);
      if (mainObject is IOwnership)
        dynamicContentProvider.Owner = ((IOwnership) mainObject).Owner;
      this.SetPermissionHolderInheritanceAssociation(dynamicContentProvider, mainObject);
      return (ISecuredObject) dynamicContentProvider;
    }

    /// <inheritdoc />
    public ISecuredObject GetSecuredObject(
      ISecuredObject securedObject,
      string dynamicContentProviderName)
    {
      string defaultProviderName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
      if (securedObject is IDynamicSecuredObject)
        defaultProviderName = DynamicModuleManager.GetDefaultProviderName(((IDynamicSecuredObject) securedObject).GetModuleName());
      if (dynamicContentProviderName.IsNullOrEmpty())
        dynamicContentProviderName = defaultProviderName;
      if (dynamicContentProviderName == ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName())
        return securedObject;
      IEnumerable<DynamicContentProvider> source = this.Provider.GetDirtyItems().OfType<DynamicContentProvider>();
      Func<DynamicContentProvider, bool> predicate = (Func<DynamicContentProvider, bool>) (p => p.Name == dynamicContentProviderName && p.ParentSecuredObjectId == securedObject.Id);
      return (ISecuredObject) (this.GetDynamicContentProviders().SingleOrDefault<DynamicContentProvider>(predicate) ?? source.SingleOrDefault<DynamicContentProvider>(predicate));
    }

    internal static void ClearModulesCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      lock (ModuleBuilderManager.DynamicModuleCacheSync)
        cacheManager.Remove("sf_dm_cache");
    }

    internal static PropertyDescriptor GetTypeMainProperty(Type type)
    {
      IDynamicModuleType dynamicModuleType = typeof (DynamicContent).IsAssignableFrom(type) ? ModuleBuilderManager.GetModules().GetTypeByFullName(type.FullName) : throw new ArgumentException("The type '{0}' should be of type '{1}'.".Arrange((object) type.FullName, (object) typeof (DynamicContent).FullName));
      return dynamicModuleType == null ? (PropertyDescriptor) null : TypeDescriptor.GetProperties(type)[dynamicModuleType.MainShortTextFieldName];
    }

    /// <summary>
    /// Returns the default namespace for all dynamic module types from a particular module. Telerik.Sitefinity.DynamicTypes.Model.{ModuleName}
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>The default namespace for all dynamic module types from a particular module.</returns>
    internal static string GetModuleNamespace(string moduleName) => "Telerik.Sitefinity.DynamicTypes.Model" + "." + ModuleBuilderManager.ModuleNameValidationRegex.Replace(moduleName, string.Empty);

    /// <summary>
    /// Returns the properly resolved ModuleName for the passed type. Builds cache per typeName and siteId
    /// </summary>
    /// <param name="typeName">the dynamicModuleType string</param>
    /// <param name="siteId">the current site Id</param>
    /// <returns>The resolved ModuleName for the passed type.</returns>
    internal static string GetModuleNameFromType(string typeName, string siteId)
    {
      string key = string.Format("{0}_{1}", (object) typeName, (object) siteId);
      string moduleName;
      if (!ModuleBuilderManager.dynamicmoduleNameForType.TryGetValue(key, out moduleName))
      {
        lock (ModuleBuilderManager.dynamicmoduleNameForType)
        {
          if (!ModuleBuilderManager.dynamicmoduleNameForType.TryGetValue(key, out moduleName))
          {
            moduleName = ModuleBuilderManager.GetManager().GetDynamicModuleType(typeName).ModuleName;
            ModuleBuilderManager.dynamicmoduleNameForType.Add(key, moduleName);
          }
        }
      }
      return moduleName;
    }

    /// <summary>Handles the Executing event of the Provider control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      ModuleBuilderDataProvider builderDataProvider = sender as ModuleBuilderDataProvider;
      IList dirtyItems = builderDataProvider.GetDirtyItems();
      bool suppressSecurityChecks = builderDataProvider.SuppressSecurityChecks;
      bool suppressEvents = builderDataProvider.SuppressEvents;
      try
      {
        builderDataProvider.SuppressSecurityChecks = true;
        builderDataProvider.SuppressEvents = true;
        List<DynamicModuleType> source1 = new List<DynamicModuleType>();
        List<DynamicModule> source2 = new List<DynamicModule>();
        for (int index = 0; index < dirtyItems.Count; ++index)
        {
          object itemInTransaction = dirtyItems[index];
          DynamicModuleType dynamicType = (DynamicModuleType) null;
          switch (itemInTransaction)
          {
            case DynamicModuleField _:
              Guid typeId = ((DynamicModuleField) itemInTransaction).ParentTypeId;
              if (!source1.Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == typeId)))
              {
                dynamicType = builderDataProvider.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.Id == typeId));
                if (dynamicType != null)
                {
                  source1.Add(dynamicType);
                  break;
                }
                break;
              }
              break;
            case DynamicModuleType _:
              dynamicType = (DynamicModuleType) itemInTransaction;
              break;
            case DynamicModule _:
              if (builderDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.Deleted)
              {
                PackagingOperations.DeleteAddonLinks((itemInTransaction as DynamicModule).Id, typeof (DynamicModule).FullName);
                break;
              }
              break;
          }
          if (dynamicType != null && !source2.Any<DynamicModule>((Func<DynamicModule, bool>) (m => m.Id == dynamicType.ParentModuleId)))
          {
            DynamicModule dynamicModule = builderDataProvider.GetDynamicModules().FirstOrDefault<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == dynamicType.ParentModuleId));
            if (dynamicModule != null)
              source2.Add(dynamicModule);
          }
        }
        foreach (DynamicModuleType dynamicModuleType in source1)
          dynamicModuleType.LastModified = DateTime.UtcNow;
        foreach (DynamicModule dynamicModule in source2)
          dynamicModule.LastModified = DateTime.UtcNow;
      }
      finally
      {
        builderDataProvider.SuppressSecurityChecks = suppressSecurityChecks;
        builderDataProvider.SuppressEvents = suppressEvents;
      }
    }

    /// <summary>Handles the Executed event of the Provider control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    internal static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    internal virtual DynamicModule CreateDynamicModule() => this.Provider.CreateDynamicModule();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    internal DynamicModule CreateDynamicModule(Guid id, string applicationName) => this.Provider.CreateDynamicModule(id, applicationName);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> with specified id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    internal DynamicModule CreateDynamicModule(Guid id) => this.Provider.CreateDynamicModule(id, (string) null);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    internal DynamicModule GetDynamicModule(Guid id) => this.Provider.GetDynamicModule(id);

    /// <summary>Gets the query of all dynamic modules.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> type.</returns>
    internal IQueryable<DynamicModule> GetDynamicModules() => (IQueryable<DynamicModule>) this.Provider.GetDynamicModules().OrderBy<DynamicModule, string>((Expression<Func<DynamicModule, string>>) (m => m.Title));

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicModule">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> to be deleted.</param>
    internal void Delete(DynamicModule dynamicModule)
    {
      foreach (DynamicModuleType type in dynamicModule.Types)
        this.Delete(type);
      IQueryable<DynamicContentProvider> contentProviders = this.GetDynamicContentProviders();
      Expression<Func<DynamicContentProvider, bool>> predicate = (Expression<Func<DynamicContentProvider, bool>>) (dcp => dcp.ParentSecuredObjectTitle == dynamicModule.Name);
      foreach (DynamicContentProvider dynamicContentProvider in (IEnumerable<DynamicContentProvider>) contentProviders.Where<DynamicContentProvider>(predicate))
        this.Delete(dynamicContentProvider);
      this.Provider.Delete(dynamicModule);
    }

    /// <summary>
    /// Determines weather the dynamic module with the specified name exists in the system.
    /// </summary>
    /// <param name="moduleName">
    /// Name of the dynamic module for which the existence should be determined.
    /// </param>
    /// <returns>
    /// True if dynamic module exists in the system; otherwise false.
    /// </returns>
    internal bool ModuleExists(string moduleName) => !string.IsNullOrEmpty(moduleName) && ModuleBuilderManager.GetModules().Any<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Name.Equals(moduleName, StringComparison.InvariantCultureIgnoreCase)));

    /// <summary>
    /// Determines weather the dynamic module with the specified Id exists in the system.
    /// </summary>
    /// <param name="moduleId">
    /// Id of the dynamic module for which the existence should be determined.
    /// </param>
    /// <returns>
    /// True if dynamic module exists in the system; otherwise false.
    /// </returns>
    internal bool ModuleExists(Guid moduleId) => !(moduleId == Guid.Empty) && ModuleBuilderManager.GetModules().Any<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == moduleId));

    /// <summary>
    /// Determines weather the dynamic module with the specified verification condition exists
    /// </summary>
    /// <param name="verification">
    /// The condition on which to determine if module exists
    /// </param>
    /// <returns>
    /// True if dynamic module exists in the system; otherwise false.
    /// </returns>
    internal bool ModuleExists(Func<IDynamicModule, bool> verification) => ModuleBuilderManager.GetModules().Any<IDynamicModule>(verification);

    internal void CopyModuleProperties(
      DynamicModule newDynamicModule,
      DynamicModule persistentDynamicModule,
      bool renameIfExists = true,
      bool moduleExisted = false)
    {
      string str = string.Empty;
      string testModuleName = newDynamicModule.Name;
      int num = 0;
      while (string.IsNullOrEmpty(str))
      {
        if (ModuleBuilderManager.GetModules().Any<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Name == testModuleName)) & renameIfExists)
        {
          ++num;
          testModuleName += (string) (object) num;
        }
        else
          str = testModuleName;
      }
      persistentDynamicModule.DefaultBackendDefinitionName = newDynamicModule.DefaultBackendDefinitionName;
      persistentDynamicModule.Description = newDynamicModule.Description;
      persistentDynamicModule.Name = str;
      persistentDynamicModule.Title = num > 0 ? newDynamicModule.Title + " " + num.ToString() : newDynamicModule.Title;
      if (!moduleExisted)
      {
        persistentDynamicModule.PageId = newDynamicModule.PageId;
        persistentDynamicModule.Status = DynamicModuleStatus.NotInstalled;
        persistentDynamicModule.Origin = newDynamicModule.Origin;
      }
      persistentDynamicModule.UrlName = num > 0 ? newDynamicModule.UrlName + "-" + num.ToString() : newDynamicModule.UrlName;
      persistentDynamicModule.Owner = newDynamicModule.Owner;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    internal DynamicModuleType CreateDynamicModuleType() => this.Provider.CreateDynamicModuleType();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module type ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    internal DynamicModuleType CreateDynamicModuleType(
      Guid id,
      string applicationName)
    {
      return this.Provider.CreateDynamicModuleType(id, applicationName);
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    internal DynamicModuleType GetDynamicModuleType(Guid id) => this.Provider.GetDynamicModuleType(id);

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> from the
    /// CLR type that represents that <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="clrType">
    /// CLR type that represents the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </returns>
    internal DynamicModuleType GetDynamicModuleType(Type clrType)
    {
      if (clrType == (Type) null)
        throw new ArgumentNullException(nameof (clrType));
      return this.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeName == clrType.Name && t.TypeNamespace == clrType.Namespace)).Single<DynamicModuleType>();
    }

    /// <summary>
    /// Returns the queue of the hierarchy between two types if there is any
    /// </summary>
    /// <param name="parentType">The parent type</param>
    /// <param name="childType">The child type</param>
    /// <returns>Queue of dynamic module types</returns>
    internal Stack<DynamicModuleType> GetHierarchyInDepth(
      Type parentType,
      Type childType)
    {
      Stack<DynamicModuleType> hierarchyInDepth = new Stack<DynamicModuleType>();
      IQueryable<DynamicModuleType> dynamicModuleTypes = this.GetDynamicModuleTypes();
      DynamicModuleType dynamicModuleType1 = dynamicModuleTypes.FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (dmt => dmt.TypeNamespace == childType.Namespace && dmt.TypeName == childType.Name));
      DynamicModuleType dynamicModuleType2 = dynamicModuleTypes.FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (dmt => dmt.TypeNamespace == parentType.Namespace && dmt.TypeName == parentType.Name));
      if (dynamicModuleType1 != null && dynamicModuleType2 != null)
      {
        DynamicModuleType dynamicModuleType3;
        for (dynamicModuleType3 = dynamicModuleType1; dynamicModuleType3 != null && dynamicModuleType3.Id != dynamicModuleType2.Id; dynamicModuleType3 = dynamicModuleType3.ParentModuleType)
          hierarchyInDepth.Push(dynamicModuleType3);
        hierarchyInDepth.Push(dynamicModuleType3);
        if (dynamicModuleType3 == null || dynamicModuleType3.Id != dynamicModuleType2.Id)
          hierarchyInDepth.Clear();
      }
      return hierarchyInDepth;
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> from the full
    /// name of the CLR type that represents that <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="fullTypeName">Full name of the CLR type.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </returns>
    internal DynamicModuleType GetDynamicModuleType(string fullTypeName) => !string.IsNullOrEmpty(fullTypeName) ? this.GetDynamicModuleType(this.ResolveDynamicClrType(fullTypeName)) : throw new ArgumentNullException(fullTypeName);

    /// <summary>Invalidates the field permissions per type cache.</summary>
    internal void InvalidateFieldPermissionsCheckCache()
    {
      lock (ModuleBuilderManager.dynamicTypesFieldPermissionsCache)
        ModuleBuilderManager.dynamicTypesFieldPermissionsCache.Clear();
    }

    /// <summary>Invalidates the module name per type cache.</summary>
    internal void InvalidateModuleNamePerTypeCache()
    {
      lock (ModuleBuilderManager.dynamicTypesFieldPermissionsCache)
        ModuleBuilderManager.dynamicTypesFieldPermissionsCache.Clear();
    }

    /// <summary>
    /// Determines whether permissions per field should be checked for the specified dynamic type.
    /// </summary>
    /// <param name="fullTypeName">The full name of the type</param>
    /// <returns>A value that indicates whether permissions per field should be checked for the specified dynamic type.</returns>
    internal bool ShouldCheckPermissionsPerField(string fullTypeName)
    {
      bool fieldPermissions;
      if (!ModuleBuilderManager.dynamicTypesFieldPermissionsCache.TryGetValue(fullTypeName, out fieldPermissions))
      {
        lock (ModuleBuilderManager.dynamicTypesFieldPermissionsCache)
        {
          if (!ModuleBuilderManager.dynamicTypesFieldPermissionsCache.TryGetValue(fullTypeName, out fieldPermissions))
          {
            IDynamicModuleType typeByFullName = ModuleBuilderManager.GetModules().GetTypeByFullName(fullTypeName);
            fieldPermissions = typeByFullName.CheckFieldPermissions;
            ModuleBuilderManager.dynamicTypesFieldPermissionsCache.Add(typeByFullName.GetFullTypeName(), fieldPermissions);
          }
        }
      }
      return fieldPermissions;
    }

    /// <summary>Gets the query of all dynamic module types.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> type.</returns>
    internal IQueryable<DynamicModuleType> GetDynamicModuleTypes() => (IQueryable<DynamicModuleType>) this.Provider.GetDynamicModuleTypes().OrderBy<DynamicModuleType, DateTime>((Expression<Func<DynamicModuleType, DateTime>>) (m => m.LastModified));

    internal IEnumerable<DynamicModuleType> GetActiveDynamicModuleTypes(
      IEnumerable<IDynamicModule> dynamicModulesQuery = null,
      IEnumerable<DynamicModuleType> dynamicModuleTypes = null)
    {
      if (dynamicModulesQuery == null)
        dynamicModulesQuery = (IEnumerable<IDynamicModule>) ModuleBuilderManager.GetModules();
      if (dynamicModuleTypes == null)
        dynamicModuleTypes = (IEnumerable<DynamicModuleType>) this.GetDynamicModuleTypes();
      IEnumerable<Guid> activeModuleIds = dynamicModulesQuery.Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Status == DynamicModuleStatus.Active)).Select<IDynamicModule, Guid>((Func<IDynamicModule, Guid>) (x => x.Id));
      return dynamicModuleTypes.Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (x => activeModuleIds.Contains<Guid>(x.ParentModuleId)));
    }

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="dynamicModuleType">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to be deleted.</param>
    internal void Delete(DynamicModuleType dynamicModuleType)
    {
      this.DeleteFieldsBackendSections(dynamicModuleType);
      foreach (DynamicModuleField field in dynamicModuleType.Fields)
        this.Delete(field);
      IQueryable<DynamicContentProvider> contentProviders = this.GetDynamicContentProviders();
      Expression<Func<DynamicContentProvider, bool>> predicate = (Expression<Func<DynamicContentProvider, bool>>) (p => p.ParentSecuredObjectId == dynamicModuleType.Id);
      foreach (DynamicContentProvider dynamicContentProvider in contentProviders.Where<DynamicContentProvider>(predicate).ToArray<DynamicContentProvider>())
        this.Delete(dynamicContentProvider);
      this.Provider.Delete(dynamicModuleType);
    }

    /// <summary>
    /// Copies the specified source dynamic module type to the specified target module type.
    /// </summary>
    /// <param name="sourceModule">The source module.</param>
    /// <param name="targetModule">The target module.</param>
    internal void CopyDynamicModuleTypes(DynamicModule sourceModule, DynamicModule targetModule)
    {
      foreach (DynamicModuleType type1 in sourceModule.Types)
      {
        DynamicModuleType type = type1;
        DynamicModuleType dynamicModuleType = this.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.Id == type.Id)).FirstOrDefault<DynamicModuleType>() ?? this.CreateDynamicModuleType(type.Id, this.provider.ApplicationName);
        dynamicModuleType.DisplayName = type.DisplayName;
        dynamicModuleType.PageId = type.PageId;
        dynamicModuleType.ParentModuleTypeId = type.ParentModuleTypeId;
        dynamicModuleType.IsSelfReferencing = type.IsSelfReferencing;
        dynamicModuleType.LastModified = type.LastModified;
        dynamicModuleType.TypeName = type.TypeName;
        dynamicModuleType.TypeNamespace = this.GetTypeNamespace(targetModule.Name);
        dynamicModuleType.MainShortTextFieldName = type.MainShortTextFieldName;
        dynamicModuleType.CheckFieldPermissions = type.CheckFieldPermissions;
        dynamicModuleType.Origin = type.Origin;
        this.SetParentModule(dynamicModuleType, targetModule);
        this.CopyBackendSections(type, dynamicModuleType);
        this.CopyDynamicFields(type, dynamicModuleType);
      }
    }

    /// <summary>
    /// Returns the default namespace for all dynamic module types from a particular module. Telerik.Sitefinity.DynamicTypes.Model.{ModuleName}
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>The default namespace for all dynamic module types from a particular module.</returns>
    internal string GetTypeNamespace(string moduleName) => ModuleBuilderManager.GetModuleNamespace(moduleName);

    /// <summary>Sets the parent module.</summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="parentModule">The parent module.</param>
    internal void SetParentModule(DynamicModuleType dynamicModuleType, DynamicModule parentModule) => this.Provider.SetParentModule(dynamicModuleType, parentModule);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    internal DynamicModuleField CreateDynamicModuleField() => this.Provider.CreateDynamicModuleField();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module field ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    internal DynamicModuleField CreateDynamicModuleField(
      Guid id,
      string applicationName)
    {
      return this.Provider.CreateDynamicModuleField(id, applicationName);
    }

    /// <summary>
    /// Creates a dynamic field in the dynamic type with the specified id.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="parentType">The parent type.</param>
    internal void CreateDynamicField(DynamicModuleField field, DynamicModuleType parentType)
    {
      DynamicModuleField persistentDynamicField = this.CreateDynamicModuleField(field.Id, field.ApplicationName);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) field);
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        Type attributeType = typeof (NonSerializableProperty);
        if (!(propertyDescriptor.Attributes[attributeType] is NonSerializableProperty))
        {
          object obj = propertyDescriptor.GetValue((object) field);
          if (obj != null)
            propertyDescriptor.SetValue((object) persistentDynamicField, obj);
        }
      }
      properties["ParentTypeId"].SetValue((object) persistentDynamicField, (object) parentType.Id);
      if (persistentDynamicField.SpecialType == FieldSpecialType.None)
      {
        if (properties["FieldNamespace"].GetValue((object) persistentDynamicField) == null)
          properties["FieldNamespace"].SetValue((object) persistentDynamicField, (object) parentType.GetFullTypeName());
        if (persistentDynamicField.ModuleName.IsNullOrEmpty())
          persistentDynamicField.ModuleName = parentType.ModuleName;
      }
      persistentDynamicField.FieldStatus = DynamicModuleFieldStatus.Added;
      if (parentType.Fields == null || ((IEnumerable<DynamicModuleField>) parentType.Fields).Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Id == persistentDynamicField.Id)))
        return;
      parentType.Fields = ((IEnumerable<DynamicModuleField>) parentType.Fields).Concat<DynamicModuleField>((IEnumerable<DynamicModuleField>) new DynamicModuleField[1]
      {
        persistentDynamicField
      }).ToArray<DynamicModuleField>();
    }

    /// <summary>
    /// Copies the dynamic fields of the specified source dynamic type to the specified target dynamic type.
    /// </summary>
    /// <param name="sourceType">Source dynamic type.</param>
    /// <param name="targetType">Target dynamic type.</param>
    internal void CopyDynamicFields(DynamicModuleType sourceType, DynamicModuleType targetType)
    {
      foreach (DynamicModuleField field1 in sourceType.Fields)
      {
        DynamicModuleField field = field1;
        DynamicModuleField dynamicField = this.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.Id == field.Id)).FirstOrDefault<DynamicModuleField>();
        if (dynamicField == null)
          this.CreateDynamicField(field, targetType);
        else if (targetType.Fields != null && !((IEnumerable<DynamicModuleField>) targetType.Fields).Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Id == dynamicField.Id)))
          targetType.Fields = ((IEnumerable<DynamicModuleField>) targetType.Fields).Concat<DynamicModuleField>((IEnumerable<DynamicModuleField>) new DynamicModuleField[1]
          {
            dynamicField
          }).ToArray<DynamicModuleField>();
      }
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    internal DynamicModuleField GetDynamicModuleField(Guid id) => this.Provider.GetDynamicModuleField(id);

    /// <summary>Gets the query of all dynamic module fields.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> type.</returns>
    internal virtual IQueryable<DynamicModuleField> GetDynamicModuleFields() => this.Provider.GetDynamicModuleFields();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <param name="dynamicModuleField">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> to be deleted.</param>
    internal void Delete(DynamicModuleField dynamicModuleField) => this.Provider.Delete(dynamicModuleField);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    internal FieldsBackendSection CreateFieldsBackendSection() => this.Provider.CreateFieldsBackendSection();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the fields back end section to be created.</param>
    /// <param name="applicationName">Application name under which the fields back end section ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    internal FieldsBackendSection CreateFieldsBackendSection(
      Guid id,
      string applicationName)
    {
      return this.Provider.CreateFieldsBackendSection(id, applicationName);
    }

    /// <summary>
    /// Copies the back end sections from the specified source dynamic type to the specified target dynamic type.
    /// </summary>
    /// <param name="sourceType">Source dynamic type.</param>
    /// <param name="targetType">Target dynamic type.</param>
    internal void CopyBackendSections(DynamicModuleType sourceType, DynamicModuleType targetType)
    {
      List<FieldsBackendSection> fieldsBackendSectionList = new List<FieldsBackendSection>();
      foreach (FieldsBackendSection section1 in sourceType.Sections)
      {
        FieldsBackendSection section = section1;
        FieldsBackendSection fieldsBackendSection = (FieldsBackendSection) null;
        if (targetType.Sections != null)
          fieldsBackendSection = ((IEnumerable<FieldsBackendSection>) targetType.Sections).Where<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (b => b.Id == section.Id)).FirstOrDefault<FieldsBackendSection>();
        if (fieldsBackendSection == null)
          fieldsBackendSection = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (b => b.Id == section.Id)).FirstOrDefault<FieldsBackendSection>();
        if (fieldsBackendSection == null)
          fieldsBackendSection = this.CreateFieldsBackendSection(section.Id, this.provider.ApplicationName);
        fieldsBackendSection.ParentTypeId = targetType.Id;
        fieldsBackendSection.IsExpandable = section.IsExpandable;
        fieldsBackendSection.Ordinal = section.Ordinal;
        fieldsBackendSection.Name = section.Name;
        fieldsBackendSection.Title = section.Title;
        fieldsBackendSection.IsExpandedByDefault = section.IsExpandedByDefault;
        fieldsBackendSectionList.Add(fieldsBackendSection);
      }
      targetType.Sections = fieldsBackendSectionList.ToArray();
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the fields back end section to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    internal FieldsBackendSection GetFieldsBackendSection(Guid id) => this.Provider.GetFieldsBackendSection(id);

    /// <summary>Gets the query of all field back end sections.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> type.</returns>
    internal IQueryable<FieldsBackendSection> GetFieldsBackendSections() => this.Provider.GetFieldsBackendSections();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <param name="fieldsBackendSection">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> to be deleted.</param>
    internal void Delete(FieldsBackendSection fieldsBackendSection) => this.Provider.Delete(fieldsBackendSection);

    /// <summary>Creates a new entry of the specified type.</summary>
    /// <param name="entryType">Type of the entry to be created.</param>
    /// <returns>The instance of the newly created entry.</returns>
    internal object CreateEntry(Type entryType)
    {
      if (typeof (DynamicModule).IsAssignableFrom(entryType))
        return (object) this.CreateDynamicModule();
      if (typeof (DynamicModuleType).IsAssignableFrom(entryType))
        return (object) this.CreateDynamicModuleType();
      if (typeof (DynamicModuleField).IsAssignableFrom(entryType))
        return (object) this.CreateDynamicModuleField();
      throw new NotSupportedException(string.Format("The entry of type '{0}' is not supported by IODataManager of type '{1}'", (object) entryType.FullName, (object) this.GetType().FullName));
    }

    /// <summary>Gets the entry of the specified type by it's id.</summary>
    /// <param name="entryType">Type of the entry to retrieve.</param>
    /// <param name="entryKey">The key which identifies the entry to be retrieved.</param>
    /// <returns>The entry.</returns>
    internal object GetEntry(Type entryType, string entryKey)
    {
      if (typeof (DynamicModule).IsAssignableFrom(entryType))
        return (object) this.GetDynamicModule(new Guid(entryKey));
      if (typeof (DynamicModuleType).IsAssignableFrom(entryType))
        return (object) this.GetDynamicModuleType(new Guid(entryKey));
      if (typeof (DynamicModuleField).IsAssignableFrom(entryType))
        return (object) this.GetDynamicModuleField(new Guid(entryKey));
      throw new NotSupportedException(string.Format("The entry of type '{0}' is not supported by IODataManager of type '{1}'", (object) entryType.FullName, (object) this.GetType().FullName));
    }

    /// <summary>Get a collection of entries of the specified type.</summary>
    /// <param name="entryType">Type of entries to be retrieved.</param>
    /// <returns>An instance of <see cref="T:System.Collections.IEnumerable" /> representing the entries.</returns>
    internal IEnumerable GetEntries(Type entryType)
    {
      if (typeof (DynamicModule).IsAssignableFrom(entryType))
        return (IEnumerable) this.GetDynamicModules().ToArray<DynamicModule>();
      if (typeof (DynamicModuleType).IsAssignableFrom(entryType))
        return (IEnumerable) this.GetDynamicModuleTypes().ToArray<DynamicModuleType>();
      if (typeof (DynamicModuleField).IsAssignableFrom(entryType))
        return (IEnumerable) this.GetDynamicModuleFields().ToArray<DynamicModuleField>();
      throw new NotSupportedException(string.Format("The entry of type '{0}' is not supported by IODataManager of type '{1}'", (object) entryType.FullName, (object) this.GetType().FullName));
    }

    /// <summary>Deletes the entry of the specified type and key.</summary>
    /// <param name="entryType">The type of the entry to be deleted.</param>
    /// <param name="entryKey">The key which identifies the entry to be deleted.</param>
    internal void DeleteEntry(Type entryType, string entryKey)
    {
      if (typeof (DynamicModule).IsAssignableFrom(entryType))
        this.Delete(this.GetDynamicModule(new Guid(entryKey)));
      else if (typeof (DynamicModuleType).IsAssignableFrom(entryType))
      {
        this.Delete(this.GetDynamicModuleType(new Guid(entryKey)));
      }
      else
      {
        if (!typeof (DynamicModuleField).IsAssignableFrom(entryType))
          throw new NotSupportedException(string.Format("The entry of type '{0}' is not supported by IODataManager of type '{1}'", (object) entryType.FullName, (object) this.GetType().FullName));
        this.Delete(this.GetDynamicModuleField(new Guid(entryKey)));
      }
    }

    /// <summary>
    /// This method is called just before the object is to be serialized and sent to the client.
    /// You can use this method to perform any last moment modifications to the entry.
    /// </summary>
    /// <param name="entry">The instance of the entry that will be sent to the client.</param>
    internal void PrepareForClient(object entry)
    {
      switch (entry)
      {
        case DynamicModule dynamicModule:
          this.LoadDynamicModuleGraph(dynamicModule);
          break;
        case DynamicModuleType dynamicModuleType:
          this.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
          break;
      }
    }

    /// <summary>
    /// Loads the complete object graph for the given instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicModule">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the object graph ought to be loaded.
    /// </param>
    internal virtual void LoadDynamicModuleGraph(DynamicModule dynamicModule) => this.LoadDynamicModuleGraph(dynamicModule, false);

    /// <summary>
    /// Loads the complete object graph for the given instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicModule">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the object graph ought to be loaded.
    /// </param>
    /// <param name="forceReload">A value that indicates whether to force the reload or not.</param>
    /// <param name="excludeSpecialFields">A value that indicates whether to exclude special fields or not.</param>
    internal virtual void LoadDynamicModuleGraph(
      DynamicModule dynamicModule,
      bool forceReload,
      bool excludeSpecialFields = false)
    {
      if (dynamicModule == null)
        throw new ArgumentNullException(nameof (dynamicModule));
      this.LoadDynamicModulesGraph(new DynamicModule[1]
      {
        dynamicModule
      }, forceReload, excludeSpecialFields);
    }

    internal void LoadDynamicModulesGraph(
      DynamicModule[] dynamicModules,
      bool forceReload,
      bool excludeSpecialFields = false)
    {
      Guid[] ids = ((IEnumerable<DynamicModule>) dynamicModules).Select<DynamicModule, Guid>((Func<DynamicModule, Guid>) (m => m.Id)).ToArray<Guid>();
      DynamicModuleType[] array = this.GetDynamicModuleTypes().Include<DynamicModuleType>((Expression<Func<DynamicModuleType, object>>) (t => t.Permissions)).Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => ids.Contains<Guid>(t.ParentModuleId))).ToArray<DynamicModuleType>();
      foreach (DynamicModule dynamicModule1 in dynamicModules)
      {
        DynamicModule dynamicModule = dynamicModule1;
        if (forceReload || dynamicModule.Types == null)
          dynamicModule.Types = ((IEnumerable<DynamicModuleType>) array).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleId == dynamicModule.Id)).ToArray<DynamicModuleType>();
        foreach (DynamicModuleType type in dynamicModule.Types)
        {
          if (string.IsNullOrEmpty(type.ModuleName))
            type.ModuleName = dynamicModule.Name;
        }
      }
      this.LoadDynamicModuleTypesGraph(array, excludeSpecialFields, forceReload);
    }

    /// <summary>
    /// Loads the complete object graph for the given instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="dynamicModuleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the object graph ought to be loaded.
    /// </param>
    /// <param name="excludeSpecialFields">
    /// Specifies whether special fields should be loaded.
    /// </param>
    /// <param name="forceReload">
    /// Specifies whether the reload should be forced.
    /// </param>
    internal virtual void LoadDynamicModuleTypeGraph(
      DynamicModuleType dynamicModuleType,
      bool excludeSpecialFields,
      bool forceReload = false)
    {
      if (dynamicModuleType == null)
        throw new ArgumentNullException(nameof (dynamicModuleType));
      this.LoadDynamicModuleTypesGraph(new DynamicModuleType[1]
      {
        dynamicModuleType
      }, excludeSpecialFields, forceReload);
    }

    internal void LoadDynamicModuleTypesGraph(
      DynamicModuleType[] dynamicModuleTypes,
      bool excludeSpecialFields,
      bool forceReload = false)
    {
      Guid[] ids = ((IEnumerable<DynamicModuleType>) dynamicModuleTypes).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Fields == null | forceReload)).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.Id)).ToArray<Guid>();
      if (ids.Length == 0)
        return;
      IQueryable<DynamicModuleField> source = this.GetDynamicModuleFields().Include<DynamicModuleField>((Expression<Func<DynamicModuleField, object>>) (f => f.Permissions)).Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => ids.Contains<Guid>(f.ParentTypeId)));
      if (excludeSpecialFields)
        source = source.Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => (int) f.SpecialType == 0));
      DynamicModuleField[] array1 = source.ToArray<DynamicModuleField>();
      FieldsBackendSection[] array2 = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => ids.Contains<Guid>(s.ParentTypeId))).ToArray<FieldsBackendSection>();
      foreach (DynamicModuleType dynamicModuleType1 in dynamicModuleTypes)
      {
        DynamicModuleType dynamicModuleType = dynamicModuleType1;
        DynamicModuleField[] array3 = ((IEnumerable<DynamicModuleField>) array1).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.ParentTypeId == dynamicModuleType.Id)).OrderBy<DynamicModuleField, int>((Func<DynamicModuleField, int>) (f => f.Ordinal)).ToArray<DynamicModuleField>();
        FieldsBackendSection[] array4 = ((IEnumerable<FieldsBackendSection>) array2).Where<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.ParentTypeId == dynamicModuleType.Id)).OrderBy<FieldsBackendSection, int>((Func<FieldsBackendSection, int>) (s => s.Ordinal)).ToArray<FieldsBackendSection>();
        List<DynamicModuleField> list = ((IEnumerable<DynamicModuleField>) array3).Join((IEnumerable<FieldsBackendSection>) array4, (Func<DynamicModuleField, Guid>) (f => f.ParentSectionId), (Func<FieldsBackendSection, Guid>) (s => s.Id), (f, s) => new
        {
          f = f,
          s = s
        }).OrderBy(_param1 => _param1.s.Ordinal).ThenBy(_param1 => _param1.f.Ordinal).ThenBy(_param1 => _param1.f.Name).Select(_param1 => _param1.f).ToList<DynamicModuleField>();
        foreach (DynamicModuleField dynamicModuleField in array3)
        {
          if (!list.Contains(dynamicModuleField))
            list.Add(dynamicModuleField);
        }
        dynamicModuleType.Fields = list.ToArray();
        dynamicModuleType.Sections = array4;
      }
    }

    /// <summary>
    /// Loads the dynamic content provider in the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    internal virtual void LoadDynamicModuleTypeContentProviders(DynamicModuleType dynamicModuleType)
    {
      if (dynamicModuleType == null)
        throw new ArgumentNullException(nameof (dynamicModuleType));
      if (dynamicModuleType.DynamicContentProviders != null)
        return;
      DynamicContentProvider[] array = this.GetDynamicContentProviders().Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.ParentSecuredObjectId == dynamicModuleType.Id)).ToArray<DynamicContentProvider>();
      dynamicModuleType.DynamicContentProviders = array;
    }

    /// <summary>Resolves the dynamic type from the full type name.</summary>
    /// <param name="fullTypeName">Full name of the type to be resolved.</param>
    /// <param name="throwOnError">A value that indicates whether to throw an exception if error occurs or not.</param>
    /// <returns>
    /// The instance of the <see cref="T:System.Type" /> resolved from the full type name.
    /// </returns>
    internal Type ResolveDynamicClrType(string fullTypeName, bool throwOnError = true) => !string.IsNullOrEmpty(fullTypeName) ? TypeResolutionService.ResolveType(fullTypeName, throwOnError) : throw new ArgumentNullException(nameof (fullTypeName));

    /// <summary>
    /// Gets the hierarchy of dynamic content types to which the provided type belongs.
    /// The content types are ordered from the most senior to most junior type in the hierarchy.
    /// </summary>
    /// <param name="type">The dynamic type.</param>
    /// <param name="getMultipleChildren">Indicates whether to fetch multiple children hierarchy.</param>
    /// <returns>Collection of dynamic content types.</returns>
    internal List<DynamicModuleType> GetContentTypesHierarchy(
      DynamicModuleType type,
      bool getMultipleChildren = false)
    {
      List<DynamicModuleType> contentTypesHierarchy = new List<DynamicModuleType>();
      if (getMultipleChildren)
      {
        DynamicModuleType rootType = this.GetRootType(type);
        contentTypesHierarchy.Add(rootType);
        List<DynamicModuleType> contentTypesSuccessors = this.GetContentTypesSuccessors(rootType, getMultipleChildren: true);
        contentTypesHierarchy.AddRange((IEnumerable<DynamicModuleType>) contentTypesSuccessors);
      }
      else
      {
        IList<DynamicModuleType> typesPredecessors = this.GetContentTypesPredecessors(type);
        List<DynamicModuleType> contentTypesSuccessors = this.GetContentTypesSuccessors(type);
        contentTypesHierarchy.AddRange((IEnumerable<DynamicModuleType>) typesPredecessors);
        contentTypesHierarchy.Add(type);
        contentTypesHierarchy.AddRange((IEnumerable<DynamicModuleType>) contentTypesSuccessors);
      }
      return contentTypesHierarchy;
    }

    /// <summary>
    /// Gets the root DynamicModuleType of a given DynamicModuleType
    /// </summary>
    /// <param name="type">The dynamic module type</param>
    /// <returns>The root dynamic module type.</returns>
    internal DynamicModuleType GetRootType(DynamicModuleType type)
    {
      while (type.ParentModuleTypeId != Guid.Empty && type.ParentModuleType != null)
        type = type.ParentModuleType;
      return type;
    }

    /// <summary>
    /// Gets predecessors of the provided dynamic content types.
    /// The content types are ordered from the most senior to most junior type in the hierarchy.
    /// </summary>
    /// <param name="type">The dynamic type.</param>
    /// <returns>Collection of dynamic content types.</returns>
    internal IList<DynamicModuleType> GetContentTypesPredecessors(
      DynamicModuleType type)
    {
      List<DynamicModuleType> typesPredecessors = new List<DynamicModuleType>();
      DynamicModuleType parentModuleType = type.ParentModuleType;
      if (parentModuleType != null && type.Id != parentModuleType.Id)
      {
        for (; parentModuleType != null; parentModuleType = parentModuleType.ParentModuleType)
          typesPredecessors.Insert(0, parentModuleType);
      }
      return (IList<DynamicModuleType>) typesPredecessors;
    }

    /// <summary>
    /// Gets predecessors of the provided CLR content types.
    /// The content types are ordered from the most senior to most junior type in the hierarchy.
    /// </summary>
    /// <param name="type">The CLR type.</param>
    /// <returns>Collection of CLR content types.</returns>
    internal IList<Type> GetContentTypesPredecessors(Type type)
    {
      List<Type> typesPredecessors = new List<Type>();
      foreach (DynamicModuleType typesPredecessor in (IEnumerable<DynamicModuleType>) this.GetContentTypesPredecessors(this.GetDynamicModuleType(type)))
      {
        Type type1 = this.ResolveDynamicClrType(typesPredecessor.GetFullTypeName());
        typesPredecessors.Add(type1);
      }
      return (IList<Type>) typesPredecessors;
    }

    /// <summary>
    /// Gets successors of the provided dynamic content types.
    /// The content types are ordered from the most senior to most junior type in the hierarchy.
    /// </summary>
    /// <param name="type">The dynamic type.</param>
    /// <param name="successors">Successors of the provided dynamic content types.</param>
    /// <param name="getMultipleChildren">Indicates whether to fetch multiple children.</param>
    /// <returns>Collection of dynamic content types.</returns>
    internal List<DynamicModuleType> GetContentTypesSuccessors(
      DynamicModuleType type,
      List<DynamicModuleType> successors = null,
      bool getMultipleChildren = false)
    {
      successors = successors ?? new List<DynamicModuleType>();
      if (!type.IsSelfReferencing)
      {
        if (getMultipleChildren)
        {
          Stack<DynamicModuleType> dynamicModuleTypeStack = new Stack<DynamicModuleType>();
          foreach (DynamicModuleType dynamicModuleType in this.GetChildTypes(type).Reverse<DynamicModuleType>())
            dynamicModuleTypeStack.Push(dynamicModuleType);
          while (dynamicModuleTypeStack.Count > 0)
          {
            DynamicModuleType parentType = dynamicModuleTypeStack.Pop();
            successors.Add(parentType);
            foreach (DynamicModuleType dynamicModuleType in this.GetChildTypes(parentType).Reverse<DynamicModuleType>())
              dynamicModuleTypeStack.Push(dynamicModuleType);
          }
        }
        else
        {
          for (DynamicModuleType parentType = this.GetChildTypes(type).FirstOrDefault<DynamicModuleType>(); parentType != null; parentType = this.GetChildTypes(parentType).FirstOrDefault<DynamicModuleType>())
          {
            successors.Add(parentType);
            if (parentType.IsSelfReferencing)
              break;
          }
        }
      }
      return successors;
    }

    /// <summary>
    /// Gets successors of the provided CLR content types.
    /// The content types are ordered from the most senior to most junior type in the hierarchy.
    /// </summary>
    /// <param name="type">The CLR type.</param>
    /// <param name="getMultipleChildren">Indicates whether to fetch multiple children.</param>
    /// <returns>Collection of CLR content types.</returns>
    internal IList<Type> GetContentTypesSuccessors(Type type, bool getMultipleChildren = false)
    {
      List<Type> contentTypesSuccessors = new List<Type>();
      foreach (DynamicModuleType contentTypesSuccessor in this.GetContentTypesSuccessors(this.GetDynamicModuleType(type), getMultipleChildren: getMultipleChildren))
      {
        Type type1 = this.ResolveDynamicClrType(contentTypesSuccessor.GetFullTypeName());
        contentTypesSuccessors.Add(type1);
      }
      return (IList<Type>) contentTypesSuccessors;
    }

    /// <summary>
    /// Gets value indicating if the DynamicModuleType is allowed to be deleted
    /// </summary>
    /// <param name="module">The module the type belongs to</param>
    /// <param name="dynamicType">The content type to be deleted</param>
    /// <returns>A value that indicates whether a dynamic module type can be deleted or not.</returns>
    internal bool CanDeleteDynamicModuleType(DynamicModule module, DynamicModuleType dynamicType) => this.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == module.Id)).Count<DynamicModuleType>() > 1 && !this.HasContentTypeChildren(module, dynamicType);

    internal void ModifyBackendForms(SectionFieldWrapper[] sectionsAndFields, Guid parentTypeId)
    {
      this.ClearDeletedSections(parentTypeId, ((IEnumerable<SectionFieldWrapper>) sectionsAndFields).Where<SectionFieldWrapper>((Func<SectionFieldWrapper, bool>) (s => s.IsSection)).ToArray<SectionFieldWrapper>());
      int num1 = 0;
      int num2 = 0;
      Guid guid = Guid.Empty;
      foreach (SectionFieldWrapper sectionsAndField in sectionsAndFields)
      {
        if (sectionsAndField.IsSection)
        {
          num2 = 0;
          FieldsBackendSection fieldsBackendSection = !(sectionsAndField.Id == Guid.Empty) ? this.GetFieldsBackendSection(sectionsAndField.Id) : this.CreateFieldsBackendSection();
          fieldsBackendSection.Title = sectionsAndField.Title;
          fieldsBackendSection.Ordinal = num1;
          fieldsBackendSection.ParentTypeId = parentTypeId;
          fieldsBackendSection.IsExpandedByDefault = sectionsAndField.IsExpandedByDefault;
          fieldsBackendSection.IsExpandable = sectionsAndField.IsTitleDisplayed;
          guid = fieldsBackendSection.Id;
          ++num1;
        }
        else
        {
          DynamicModuleField dynamicModuleField = this.GetDynamicModuleField(sectionsAndField.Id);
          dynamicModuleField.Ordinal = num2;
          dynamicModuleField.ParentSectionId = guid;
          ++num2;
        }
      }
    }

    internal void ModifyBackendGrid(GridColumnWrapper[] gridColumns, Guid parentTypeId)
    {
      IQueryable<DynamicModuleField> queryable = this.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.ParentTypeId == parentTypeId));
      List<DynamicModuleField> source2 = new List<DynamicModuleField>();
      string dateCreatedName = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.DateCreated));
      if (((IEnumerable<GridColumnWrapper>) gridColumns).Any<GridColumnWrapper>((Func<GridColumnWrapper, bool>) (c => c.Name == dateCreatedName)))
      {
        if (!queryable.Any<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.Name == dateCreatedName)))
        {
          DynamicModuleField transitientField = this.CreateTransitientField(queryable.First<DynamicModuleField>().ParentTypeId, dateCreatedName, "Date created", FieldSpecialType.DateCreated, true, 8, Guid.Empty);
          source2.Add(transitientField);
        }
      }
      string lastModifiedName = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.LastModified));
      if (((IEnumerable<GridColumnWrapper>) gridColumns).Any<GridColumnWrapper>((Func<GridColumnWrapper, bool>) (c => c.Name == lastModifiedName)))
      {
        if (!queryable.Any<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.Name == lastModifiedName)))
        {
          DynamicModuleField transitientField = this.CreateTransitientField(queryable.First<DynamicModuleField>().ParentTypeId, lastModifiedName, "Last modified", FieldSpecialType.LastModified, true, 7, Guid.Empty);
          source2.Add(transitientField);
        }
      }
      if (source2.Count > 0)
        queryable = queryable.Concat<DynamicModuleField>((IEnumerable<DynamicModuleField>) source2);
      foreach (DynamicModuleField dynamicModuleField in (IEnumerable<DynamicModuleField>) queryable)
      {
        DynamicModuleField field = dynamicModuleField;
        if (field.SpecialType != FieldSpecialType.Translations || ((IEnumerable<GridColumnWrapper>) gridColumns).Any<GridColumnWrapper>((Func<GridColumnWrapper, bool>) (x => x.Name == field.Name)))
        {
          field.ShowInGrid = false;
          field.GridColumnOrdinal = -1;
        }
      }
      int num = 0;
      foreach (GridColumnWrapper gridColumn1 in gridColumns)
      {
        GridColumnWrapper gridColumn = gridColumn1;
        DynamicModuleField dynamicModuleField = queryable.Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.Name == gridColumn.Name)).Single<DynamicModuleField>();
        dynamicModuleField.ShowInGrid = true;
        dynamicModuleField.GridColumnOrdinal = num;
        ++num;
      }
    }

    /// <summary>
    /// Check whether a dynamic content provider with the specified name already exists.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    /// <returns>A value that indicates whether a dynamic content provider with the specified name already exists.</returns>
    internal bool DynamicContentProviderExists(
      ISecuredObject dynamicModuleType,
      string dynamicContentProviderName)
    {
      return this.GetDynamicContentProviders().Any<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.Name == dynamicContentProviderName && p.ParentSecuredObjectId == dynamicModuleType.Id));
    }

    /// <summary>
    /// Creates an instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    /// <param name="parentSecuredObject">The parent secured object.</param>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </returns>
    internal DynamicContentProvider CreateDynamicContentProvider(
      Guid id,
      string dynamicContentProviderName,
      ISecuredObject parentSecuredObject)
    {
      DynamicContentProvider dynamicContentProvider = this.Provider.CreateDynamicContentProvider(id, this.Provider.ApplicationName, parentSecuredObject);
      dynamicContentProvider.Name = dynamicContentProviderName;
      return dynamicContentProvider;
    }

    /// <summary>
    /// Creates the dynamic content provider related to the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    /// <param name="parentSecuredObject">The parent secured object.</param>
    /// <returns>
    /// A dynamic content provider related to the specified dynamic module type.
    /// </returns>
    internal DynamicContentProvider CreateDynamicContentProvider(
      string dynamicContentProviderName,
      ISecuredObject parentSecuredObject)
    {
      return this.CreateDynamicContentProvider(this.Provider.GetNewGuid(), dynamicContentProviderName, parentSecuredObject);
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> with the specified name.
    /// </summary>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /></returns>
    internal DynamicContentProvider GetDynamicContentProvider(
      string dynamicContentProviderName)
    {
      if (string.IsNullOrEmpty(dynamicContentProviderName))
        throw new ArgumentNullException(dynamicContentProviderName);
      return this.Provider.GetDynamicContentProviders().FirstOrDefault<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.Name == dynamicContentProviderName));
    }

    /// <summary>
    /// Gets a list <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> type.
    /// </summary>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.</returns>
    internal IQueryable<DynamicContentProvider> GetDynamicContentProviders() => this.Provider.GetDynamicContentProviders();

    /// <summary>
    /// Deletes all instances of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> with specified name.
    /// </summary>
    /// <param name="name">The name.</param>
    internal void DeleteDynamicContentProviders(string name)
    {
      IQueryable<DynamicContentProvider> contentProviders = this.GetDynamicContentProviders();
      Expression<Func<DynamicContentProvider, bool>> predicate = (Expression<Func<DynamicContentProvider, bool>>) (dcp => dcp.Name == name);
      foreach (DynamicContentProvider dynamicContentProvider in (IEnumerable<DynamicContentProvider>) contentProviders.Where<DynamicContentProvider>(predicate))
        this.Delete(dynamicContentProvider);
    }

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="dynamicContentProvider">The dynamic content provider.</param>
    internal void Delete(DynamicContentProvider dynamicContentProvider) => this.Provider.Delete(dynamicContentProvider);

    /// <summary>
    /// Gets the secured objects in the context of the specified dynamic content provider.
    /// </summary>
    /// <param name="securedObjects">The secured objects.</param>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    /// <returns>Collection of secured objects in the context of the specified dynamic content provider.</returns>
    internal IEnumerable<ISecuredObject> GetSecuredObjects(
      IEnumerable<ISecuredObject> securedObjects,
      string dynamicContentProviderName)
    {
      string defaultProviderName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
      if (securedObjects.Count<ISecuredObject>() > 0 && securedObjects.First<ISecuredObject>() is IDynamicSecuredObject)
        defaultProviderName = DynamicModuleManager.GetDefaultProviderName(((IDynamicSecuredObject) securedObjects.First<ISecuredObject>()).GetModuleName());
      if (dynamicContentProviderName.IsNullOrEmpty())
        dynamicContentProviderName = defaultProviderName;
      if (defaultProviderName == dynamicContentProviderName && defaultProviderName == ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName())
        return securedObjects;
      Guid[] securedObjectIds = securedObjects.Select<ISecuredObject, Guid>((Func<ISecuredObject, Guid>) (s => s.Id)).ToArray<Guid>();
      List<DynamicContentProvider> list = this.GetDynamicContentProviders().Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.Name == dynamicContentProviderName && securedObjectIds.Contains<Guid>(p.ParentSecuredObjectId))).ToList<DynamicContentProvider>();
      if (list.Count <= 0)
        return securedObjects;
      List<ISecuredObject> securedObjects1 = new List<ISecuredObject>();
      foreach (ISecuredObject securedObject1 in securedObjects)
      {
        ISecuredObject securedObject = securedObject1;
        DynamicContentProvider dynamicContentProvider = list.SingleOrDefault<DynamicContentProvider>((Func<DynamicContentProvider, bool>) (p => p.ParentSecuredObjectId == securedObject.Id));
        if (dynamicContentProvider == null)
        {
          securedObjects1.Add(securedObject);
        }
        else
        {
          if (securedObject is IOwnership)
            dynamicContentProvider.Owner = ((IOwnership) securedObject).Owner;
          securedObjects1.Add((ISecuredObject) dynamicContentProvider);
        }
      }
      return (IEnumerable<ISecuredObject>) securedObjects1;
    }

    /// <summary>
    /// Determines weather the content type represented by the provided <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" />
    /// already exists in the system.
    /// </summary>
    /// <param name="contentTypeContext">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> that represents the content type
    /// (dynamic module).
    /// </param>
    /// <returns>True if the content type exists; otherwise false.</returns>
    internal bool ModuleExists(ContentTypeContext contentTypeContext)
    {
      if (contentTypeContext == null)
        throw new ArgumentNullException(nameof (contentTypeContext));
      if (contentTypeContext.ModuleId == Guid.Empty)
        throw new ArgumentException("The id of the module must be set when checking for it's existence.");
      return this.ModuleExists(contentTypeContext.ModuleId);
    }

    /// <summary>
    /// Creates a new content type (dynamic module) in the system.
    /// </summary>
    /// <param name="contentTypeContext">
    /// The instance of the <see cref="!:ContextTypeContext" /> that represents the content type
    /// (dynamic module).
    /// </param>
    /// <remarks>
    /// This method will create persistent type, persistent fields, definitions, widgets and everything
    /// else needed by the content type (dynamic module).
    /// </remarks>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> that has been created.
    /// </returns>
    internal virtual ContentTypeContext CreateModuleWithContentType(
      ContentTypeContext contentTypeContext)
    {
      if (contentTypeContext == null)
        throw new ArgumentNullException(nameof (contentTypeContext));
      this.ValidateContentTypeContext(contentTypeContext, true);
      DynamicModule dynamicModule = this.CreateDynamicModule();
      contentTypeContext.ModuleId = dynamicModule.Id;
      dynamicModule.Status = DynamicModuleStatus.NotInstalled;
      dynamicModule.Name = ModuleBuilderManager.RemoveSpecialCharactersLeaveSpaces(contentTypeContext.ContentTypeName);
      dynamicModule.Title = contentTypeContext.ContentTypeTitle;
      dynamicModule.UrlName = ModuleBuilderManager.ModuleNameValidationRegex.Replace(contentTypeContext.ContentTypeName, "-").ToLower();
      dynamicModule.Description = contentTypeContext.ContentTypeDescription;
      this.CreateContentTypePersistentType(dynamicModule, contentTypeContext, false, (Dictionary<string, Guid>) null);
      return contentTypeContext;
    }

    /// <summary>
    /// Updates the content type (dynamic module) in the system.
    /// </summary>
    /// <param name="contentTypeContext">The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> that represents the content type
    /// (dynamic module).</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="parentModule">The parent module.</param>
    /// <param name="keepIds">A value that indicates whether to keep the ids of items from the specified type or not.</param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> that has been updated.
    /// </returns>
    /// <remarks>
    /// This method will update the content type, however, in case the model of the content type is
    /// changed, new content type will be created, while the old one will not be deleted.
    /// </remarks>
    internal ContentTypeContext UpdateContentType(
      ContentTypeContext contentTypeContext,
      DynamicModuleType dynamicModuleType,
      DynamicModule parentModule,
      bool keepIds = false)
    {
      if (contentTypeContext == null)
        throw new ArgumentNullException("When updating a content type, the ContentTypeContext cannot be null.");
      if (contentTypeContext.ContentTypeId == Guid.Empty)
        throw new ArgumentException("When updating a content type, the id of the content type (dynamic module) must not be empty GUID.");
      this.UpdateContentTypeAreaName(contentTypeContext, dynamicModuleType, parentModule);
      DynamicModule dynamicModule = this.GetDynamicModule(contentTypeContext.ModuleId);
      this.LoadDynamicModuleGraph(dynamicModule);
      dynamicModuleType.DisplayName = contentTypeContext.ContentTypeItemTitle;
      dynamicModuleType.MainShortTextFieldName = contentTypeContext.MainShortTextFieldName;
      if (ModuleBuilderManager.mainProperties.ContainsKey(dynamicModuleType.GetFullTypeName()) && ModuleBuilderManager.mainProperties[dynamicModuleType.GetFullTypeName()] != contentTypeContext.MainShortTextFieldName)
        ModuleBuilderManager.mainProperties.Remove(dynamicModuleType.GetFullTypeName());
      dynamicModuleType.ParentModuleTypeId = contentTypeContext.ParentContentTypeId;
      dynamicModuleType.IsSelfReferencing = contentTypeContext.IsSelfReferencing;
      dynamicModuleType.CheckFieldPermissions = contentTypeContext.CheckFieldPermissions;
      if (!(dynamicModuleType.ParentModuleTypeId == Guid.Empty))
      {
        Guid parentModuleTypeId = dynamicModuleType.ParentModuleTypeId;
      }
      else
        dynamicModuleType.ParentModuleType = (DynamicModuleType) null;
      dynamicModuleType = this.UpdateContentTypePersistentType(dynamicModule, contentTypeContext, keepIds);
      return contentTypeContext;
    }

    /// <summary>Updates the dynamic module name and description.</summary>
    /// <param name="moduleId">The id of the module.</param>
    /// <param name="context">The module context.</param>
    /// <returns>The updated module context.</returns>
    internal ContentTypeSimpleContext UpdateDynamicModuleNameAndDescription(
      Guid moduleId,
      ContentTypeSimpleContext context)
    {
      if (context == null)
        throw new ArgumentNullException("When updating name or description of a content type, the ContentTypeSimpleContext cannot be null.");
      if (moduleId == Guid.Empty)
        throw new ArgumentException("When updating a content type, the id of the content type (dynamic module) must not be empty GUID.");
      if (!string.IsNullOrEmpty(context.ContentTypeTitle))
        this.UpdateModuleNameAndDescription(this.GetDynamicModule(moduleId), context);
      return context;
    }

    /// <summary>
    /// Validates that the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> contains all the needed information
    /// in order to successfully create or update a content type (dynamic module).
    /// </summary>
    /// <param name="contentTypeContext">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> that needs to be validated.
    /// </param>
    /// <param name="throwException">A value that indicates whether to throw an exception.</param>
    /// <returns>
    /// True if the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> is valid; otherwise false.
    /// </returns>
    internal virtual bool ValidateContentTypeContext(
      ContentTypeContext contentTypeContext,
      bool throwException)
    {
      if (contentTypeContext == null)
        throw new ArgumentNullException(nameof (contentTypeContext));
      if (string.IsNullOrEmpty(contentTypeContext.ContentTypeName))
      {
        if (throwException)
          throw new ArgumentException("ContentTypeContext must have 'ContentTypeName' property set.");
        return false;
      }
      if (ModuleBuilderManager.GetModules().Any<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Name == contentTypeContext.ContentTypeName)))
        throw new ArgumentException(string.Format("Dynamic module with name '{0}' already exists in the system.", (object) contentTypeContext.ContentTypeName));
      return true;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal DynamicModuleType CreateContentTypePersistentType(
      DynamicModule parentModule,
      ContentTypeContext context,
      bool keepIds,
      Dictionary<string, Guid> defaultSectionIds)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (context.ModuleId == Guid.Empty)
        throw new ArgumentException("The ModuleId property of the ContentTypeContext cannot be empty GUID.");
      if (context.Fields == null)
        throw new InvalidOperationException("In order to create a new content type, at least one field of the content type needs to be created.");
      if (context.Fields.Length == 0)
        throw new InvalidOperationException("In order to create a new content type, at least one field of the content type needs to be created.");
      DynamicModuleType typePersistentType = keepIds ? this.CreateDynamicModuleType(context.ContentTypeId, parentModule.ApplicationName) : this.CreateDynamicModuleType();
      typePersistentType.TypeName = context.ContentTypeItemName;
      typePersistentType.TypeNamespace = this.GetTypeNamespace(context.ContentTypeName);
      typePersistentType.DisplayName = context.ContentTypeItemTitle;
      this.SetParentModule(typePersistentType, parentModule);
      typePersistentType.MainShortTextFieldName = context.MainShortTextFieldName;
      typePersistentType.ParentModuleTypeId = context.ParentContentTypeId;
      typePersistentType.IsSelfReferencing = context.IsSelfReferencing;
      typePersistentType.CheckFieldPermissions = context.CheckFieldPermissions;
      typePersistentType.PageId = keepIds ? context.ContentTypePageId : Guid.Empty;
      typePersistentType.Origin = context.Origin;
      this.LoadDynamicModuleGraph(parentModule);
      List<DynamicModuleType> dynamicModuleTypeList = new List<DynamicModuleType>();
      if (parentModule.Types != null)
        dynamicModuleTypeList.AddRange((IEnumerable<DynamicModuleType>) parentModule.Types);
      dynamicModuleTypeList.Add(typePersistentType);
      parentModule.Types = dynamicModuleTypeList.ToArray();
      List<FieldsBackendSection> fieldsBackendSectionList = new List<FieldsBackendSection>();
      Guid empty1 = Guid.Empty;
      Guid sectionId1 = defaultSectionIds == null || !defaultSectionIds.TryGetValue("MainSection", out empty1) ? Guid.NewGuid() : empty1;
      FieldsBackendSection section = this.CreateSection(typePersistentType.Id, sectionId1, "MainSection", "Main Section", 0);
      fieldsBackendSectionList.Add(section);
      Guid empty2 = Guid.Empty;
      Guid id = defaultSectionIds == null || !defaultSectionIds.TryGetValue("Classification", out empty2) ? Guid.NewGuid() : empty2;
      FieldsBackendSection fieldsBackendSection1 = this.CreateFieldsBackendSection(id, this.Provider.ApplicationName);
      fieldsBackendSection1.ParentTypeId = typePersistentType.Id;
      fieldsBackendSection1.IsExpandable = true;
      fieldsBackendSection1.IsExpandedByDefault = true;
      fieldsBackendSection1.Ordinal = 1;
      fieldsBackendSection1.Name = "Classification";
      fieldsBackendSection1.Title = "Classification";
      fieldsBackendSectionList.Add(fieldsBackendSection1);
      Guid empty3 = Guid.Empty;
      FieldsBackendSection fieldsBackendSection2 = this.CreateFieldsBackendSection(defaultSectionIds == null || !defaultSectionIds.TryGetValue("MoreOptions", out empty3) ? Guid.NewGuid() : empty3, this.Provider.ApplicationName);
      fieldsBackendSection2.ParentTypeId = typePersistentType.Id;
      fieldsBackendSection2.IsExpandable = true;
      fieldsBackendSection2.IsExpandedByDefault = false;
      fieldsBackendSection2.Ordinal = 2;
      fieldsBackendSection2.Name = "MoreOptions";
      fieldsBackendSection2.Title = "More Options";
      fieldsBackendSectionList.Add(fieldsBackendSection2);
      typePersistentType.Sections = fieldsBackendSectionList.ToArray();
      List<DynamicModuleField> dynamicModuleFieldList = new List<DynamicModuleField>();
      int length = context.Fields.Length;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      Guid sectionId2 = Guid.Empty;
      Guid sectionId3 = Guid.Empty;
      for (int index = 0; index < length; ++index)
      {
        DynamicModuleField typePersistentField = this.CreateContentTypePersistentField(context, context.Fields[index], typePersistentType, parentModule, keepIds);
        if (typePersistentField.FieldType == FieldType.Address)
          this.CreateAddressFieldSection(typePersistentType, typePersistentField, fieldsBackendSection2, true, context.Fields[index].ParentSectionId);
        else if (typePersistentField.FieldType == FieldType.RelatedMedia)
        {
          flag1 = true;
          sectionId2 = context.Fields[index].ParentSectionId;
        }
        else if (typePersistentField.FieldType == FieldType.RelatedData)
        {
          flag2 = true;
          sectionId3 = context.Fields[index].ParentSectionId;
        }
        else if (SocialMediaSeoTagHelpers.IsSeoTagField(typePersistentField.Name))
          flag3 = true;
        else
          SocialMediaSeoTagHelpers.IsSocialMediaTagField(typePersistentField.Name);
        dynamicModuleFieldList.Add(typePersistentField);
      }
      Guid sectionId4 = Guid.Empty;
      if (flag3)
        sectionId4 = this.CreateOrGetFieldsBackendSection(typePersistentType, fieldsBackendSection2, sectionId4, Res.Get<ModuleBuilderResources>().SEOSectionName, Res.Get<ModuleBuilderResources>().SEOSectionTitle).Id;
      Guid guid1 = Guid.Empty;
      if (flag3)
        guid1 = this.CreateOrGetFieldsBackendSection(typePersistentType, fieldsBackendSection2, sectionId4, Res.Get<ModuleBuilderResources>().SocialMediaSectionName, Res.Get<ModuleBuilderResources>().SocialMediaSectionTitle).Id;
      Guid guid2 = Guid.Empty;
      if (flag1)
        guid2 = this.CreateOrGetFieldsBackendSection(typePersistentType, fieldsBackendSection2, sectionId2, "RelatedMedia", Res.Get<ModuleEditorResources>().RelatedMediaSectionTitle).Id;
      Guid guid3 = Guid.Empty;
      if (flag2)
        guid3 = this.CreateOrGetFieldsBackendSection(typePersistentType, fieldsBackendSection2, sectionId3, "RelatedData", Res.Get<ModuleEditorResources>().RelatedDataSectionTitle).Id;
      ModuleBuilderManager.SectionIds sectionIds = new ModuleBuilderManager.SectionIds()
      {
        ClassificationSectionId = id,
        DefaultSectionId = section.Id,
        RelatedDataSectionId = guid3,
        RelatedMediaSectionId = guid2,
        SeoSectionId = sectionId4,
        SocialMediaSectionId = guid1
      };
      this.SetFieldsOrder(dynamicModuleFieldList, sectionIds, context.MainShortTextFieldName);
      if (ModuleInstallerHelper.ContainsLocalizableFields((IEnumerable<IDynamicModuleField>) dynamicModuleFieldList.ToArray()) && !dynamicModuleFieldList.Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.Translations)))
        dynamicModuleFieldList.Add(this.CreateTranslationsField(typePersistentType, fieldsBackendSection2));
      dynamicModuleFieldList.Add(this.CreateUrlField(typePersistentType, fieldsBackendSection2));
      dynamicModuleFieldList.Add(this.CreateAuthorField(typePersistentType, fieldsBackendSection2));
      DynamicModuleField transitientField1 = this.CreateTransitientField(typePersistentType.Id, "PublicationDate", "Publication date", FieldSpecialType.PublicationDate, false, 5, fieldsBackendSection2.Id);
      dynamicModuleFieldList.Add(transitientField1);
      DynamicModuleField transitientField2 = this.CreateTransitientField(typePersistentType.Id, "LastModified", "Last modified", FieldSpecialType.LastModified, true, 7, fieldsBackendSection2.Id);
      dynamicModuleFieldList.Add(transitientField2);
      DynamicModuleField transitientField3 = this.CreateTransitientField(typePersistentType.Id, "DateCreated", "Created on", FieldSpecialType.DateCreated, true, 8, fieldsBackendSection2.Id);
      dynamicModuleFieldList.Add(transitientField3);
      dynamicModuleFieldList.Add(this.CreateActionsField(typePersistentType, fieldsBackendSection2));
      dynamicModuleFieldList.Add(this.CreateIncludeInSitemapField(typePersistentType, fieldsBackendSection2));
      if (context.ParentContentTypeId != Guid.Empty)
        dynamicModuleFieldList.Add(this.CreateParentIdField(typePersistentType, fieldsBackendSection2));
      typePersistentType.Fields = dynamicModuleFieldList.ToArray();
      return typePersistentType;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal DynamicModuleField CreateContentTypePersistentField(
      ContentTypeContext context,
      ContentTypeItemFieldContext fieldContext,
      DynamicModuleType parentType,
      DynamicModule parentModule,
      bool keepIds = false)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (fieldContext == null)
        throw new ArgumentNullException("field");
      DynamicModuleField typePersistentField = keepIds ? this.CreateDynamicModuleField(fieldContext.Id, parentType.ApplicationName) : this.CreateDynamicModuleField();
      FieldType result1;
      if (!Enum.TryParse<FieldType>(fieldContext.TypeName, out result1))
        throw new ArgumentException(string.Format("The field type '{0}' cannot be parsed.", (object) fieldContext.TypeName));
      typePersistentField.FieldType = result1;
      typePersistentField.Name = result1 != FieldType.Classification ? fieldContext.Name : (!(fieldContext.ClassificationId == TaxonomyManager.CategoriesTaxonomyId) ? (!(fieldContext.ClassificationId == TaxonomyManager.TagsTaxonomyId) ? fieldContext.Name : "Tags") : "Category");
      typePersistentField.IsHiddenField = fieldContext.IsHiddenField;
      typePersistentField.RelatedDataType = fieldContext.RelatedDataType;
      typePersistentField.RelatedDataProvider = fieldContext.RelatedDataProvider;
      typePersistentField.WidgetTypeName = fieldContext.WidgetTypeName;
      typePersistentField.FrontendWidgetTypeName = fieldContext.FrontendWidgetTypeName;
      typePersistentField.FrontendWidgetLabel = fieldContext.FrontendWidgetLabel;
      typePersistentField.DBType = fieldContext.DBType;
      typePersistentField.DecimalPlacesCount = fieldContext.DecimalPlacesCount;
      typePersistentField.ClassificationId = fieldContext.ClassificationId;
      typePersistentField.DBLength = fieldContext.DBLength;
      typePersistentField.AllowNulls = fieldContext.AllowNulls;
      typePersistentField.IncludeInIndexes = fieldContext.IncludeInIndexes;
      typePersistentField.ColumnName = fieldContext.ColumnName;
      typePersistentField.Title = fieldContext.Title;
      typePersistentField.ImageMaxSize = fieldContext.ImageMaxSize;
      typePersistentField.MediaType = fieldContext.MediaType;
      typePersistentField.FileMaxSize = fieldContext.FileMaxSize;
      typePersistentField.VideoMaxSize = fieldContext.VideoMaxSize;
      typePersistentField.ImageExtensions = fieldContext.ImageExtensions;
      typePersistentField.FileExtensions = fieldContext.FileExtensions;
      typePersistentField.VideoExtensions = fieldContext.VideoExtensions;
      typePersistentField.DdlChoiceDefaultValueIndex = fieldContext.DropDownListDefaulValueIndex;
      typePersistentField.ChoiceRenderType = fieldContext.ChoiceRenderType;
      typePersistentField.CheckedByDefault = fieldContext.CheckedByDefault;
      typePersistentField.InstructionalChoice = fieldContext.InstructionalChoice;
      typePersistentField.NumberUnit = fieldContext.NumberUnit;
      typePersistentField.IsTransient = fieldContext.IsTransient;
      typePersistentField.RegularExpression = fieldContext.RegularExpression;
      typePersistentField.AllowMultipleImages = fieldContext.AllowMultipleImages;
      typePersistentField.AllowMultipleFiles = fieldContext.AllowMultipleFiles;
      typePersistentField.AllowMultipleVideos = fieldContext.AllowMultipleVideos;
      typePersistentField.AllowImageLibrary = fieldContext.AllowImageLibrary;
      typePersistentField.CanSelectMultipleItems = fieldContext.CanSelectMultipleItems;
      typePersistentField.CanCreateItemsWhileSelecting = fieldContext.CanCreateItemsWhileSelecting;
      typePersistentField.IsRequiredToSelectDdlValue = fieldContext.IsRequiredToSelectDdlValue;
      typePersistentField.IsRequiredToSelectCheckbox = fieldContext.IsRequiredToSelectCheckbox;
      typePersistentField.MinNumberRange = fieldContext.MinNumberRange;
      typePersistentField.MaxNumberRange = fieldContext.MaxNumberRange;
      typePersistentField.InstructionalText = fieldContext.InstructionalText;
      typePersistentField.DefaultValue = fieldContext.DefaultValue;
      typePersistentField.IsRequired = fieldContext.IsRequired;
      typePersistentField.Ordinal = fieldContext.Ordinal;
      typePersistentField.RecommendedCharactersCount = fieldContext.RecommendedCharactersCount;
      int? nullable;
      if (fieldContext.MinLength.HasValue)
      {
        DynamicModuleField dynamicModuleField = typePersistentField;
        nullable = fieldContext.MinLength;
        int num = nullable.Value;
        dynamicModuleField.MinLength = num;
      }
      nullable = fieldContext.MaxLength;
      if (nullable.HasValue)
      {
        DynamicModuleField dynamicModuleField = typePersistentField;
        nullable = fieldContext.MaxLength;
        int num = nullable.Value;
        dynamicModuleField.MaxLength = num;
      }
      typePersistentField.LengthValidationMessage = fieldContext.LengthValidationMessage;
      typePersistentField.Choices = fieldContext.Choices;
      typePersistentField.TypeUIName = fieldContext.TypeUIName;
      typePersistentField.ParentTypeId = parentType.Id;
      typePersistentField.FieldNamespace = "Telerik.Sitefinity.DynamicTypes.Model" + "." + ModuleBuilderManager.ModuleNameValidationRegex.Replace(context.ContentTypeName, string.Empty) + "." + ModuleBuilderManager.ModuleNameValidationRegex.Replace(context.ContentTypeItemName, string.Empty);
      typePersistentField.ModuleName = parentModule.Name;
      typePersistentField.IsLocalizable = fieldContext.IsLocalizable;
      typePersistentField.DisableLinkParser = fieldContext.DisableLinkParser;
      AddressFieldMode result2;
      if (Enum.TryParse<AddressFieldMode>(fieldContext.AddressFieldMode, out result2))
        typePersistentField.AddressFieldMode = result2;
      return typePersistentField;
    }

    internal FieldsBackendSection CreateOrGetFieldsBackendSection(
      DynamicModuleType dynamicModuleType,
      FieldsBackendSection advancedSection,
      Guid sectionId,
      string sectionName,
      string sectionTitle)
    {
      FieldsBackendSection fieldsBackendSection1 = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Name == sectionName));
      if (fieldsBackendSection1 == null)
      {
        if (sectionId == Guid.Empty || ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Id == sectionId)) != null)
          sectionId = this.Provider.GetNewGuid();
        fieldsBackendSection1 = this.CreateFieldsBackendSection(sectionId, this.Provider.ApplicationName);
        fieldsBackendSection1.ParentTypeId = dynamicModuleType.Id;
        fieldsBackendSection1.IsExpandable = true;
        fieldsBackendSection1.IsExpandedByDefault = true;
        fieldsBackendSection1.Name = sectionName;
        fieldsBackendSection1.Title = sectionTitle;
        if (advancedSection != null)
        {
          fieldsBackendSection1.Ordinal = advancedSection.Ordinal;
          ++advancedSection.Ordinal;
        }
        else
        {
          FieldsBackendSection fieldsBackendSection2 = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id)).LastOrDefault<FieldsBackendSection>();
          fieldsBackendSection1.Ordinal = fieldsBackendSection2 != null ? fieldsBackendSection2.Ordinal + 1 : 0;
        }
        List<FieldsBackendSection> list = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).ToList<FieldsBackendSection>();
        list.Add(fieldsBackendSection1);
        dynamicModuleType.Sections = list.OrderBy<FieldsBackendSection, int>((Func<FieldsBackendSection, int>) (s => s.Ordinal)).ToArray<FieldsBackendSection>();
      }
      return fieldsBackendSection1;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal DynamicModuleType UpdateContentTypePersistentType(
      DynamicModule module,
      ContentTypeContext context,
      bool keepIds = false)
    {
      if (module == null)
        throw new ArgumentNullException(nameof (module));
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      DynamicModuleType dynamicModuleType = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.Id == context.ContentTypeId)).Single<DynamicModuleType>();
      List<DynamicModuleField> list = ((IEnumerable<DynamicModuleField>) dynamicModuleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.ParentTypeId == dynamicModuleType.Id)).ToList<DynamicModuleField>();
      List<DynamicModuleField> dynamicModuleFieldList = new List<DynamicModuleField>(list.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType != FieldSpecialType.None && !((IEnumerable<ContentTypeItemFieldContext>) context.Fields).Any<ContentTypeItemFieldContext>((Func<ContentTypeItemFieldContext, bool>) (cf => cf.Name == f.Name)))));
      FieldsBackendSection section1 = (FieldsBackendSection) null;
      FieldsBackendSection section2 = (FieldsBackendSection) null;
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      foreach (ContentTypeItemFieldContext field in context.Fields)
      {
        ContentTypeItemFieldContext contextField = field;
        DynamicModuleField dynamicModuleField = this.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.ParentTypeId == dynamicModuleType.Id && f.Name == contextField.Name)).SingleOrDefault<DynamicModuleField>();
        if (dynamicModuleField == null)
        {
          if (contextField.Name == "SystemParentId")
            section2 = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Id == contextField.ParentSectionId));
          else if (contextField.Name == "Translations")
          {
            section1 = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Id == contextField.ParentSectionId));
          }
          else
          {
            DynamicModuleField newField = this.CreateContentTypePersistentField(context, contextField, dynamicModuleType, module, keepIds);
            newField.FieldStatus = DynamicModuleFieldStatus.Added;
            if (newField.FieldType == FieldType.Address)
            {
              FieldsBackendSection advancedSection = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "MoreOptions")).SingleOrDefault<FieldsBackendSection>();
              this.CreateAddressFieldSection(dynamicModuleType, newField, advancedSection, true, contextField.ParentSectionId);
            }
            else if (newField.FieldType == FieldType.RelatedMedia && !SocialMediaSeoTagHelpers.IsSocialMediaSeoField(contextField.Name))
            {
              FieldsBackendSection advancedSection = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "MoreOptions")).SingleOrDefault<FieldsBackendSection>();
              FieldsBackendSection fieldsBackendSection = this.CreateOrGetFieldsBackendSection(dynamicModuleType, advancedSection, contextField.ParentSectionId, "RelatedMedia", Res.Get<ModuleEditorResources>().RelatedMediaSectionTitle);
              newField.ParentSectionId = fieldsBackendSection.Id;
            }
            else if (newField.FieldType == FieldType.RelatedData)
            {
              FieldsBackendSection advancedSection = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "MoreOptions")).SingleOrDefault<FieldsBackendSection>();
              FieldsBackendSection fieldsBackendSection = this.CreateOrGetFieldsBackendSection(dynamicModuleType, advancedSection, contextField.ParentSectionId, "RelatedData", Res.Get<ModuleEditorResources>().RelatedDataSectionTitle);
              newField.ParentSectionId = fieldsBackendSection.Id;
            }
            else
            {
              newField.ParentSectionId = this.GetParentSectionId(contextField, dynamicModuleType);
              contextField.ParentSectionId = newField.ParentSectionId;
            }
            newField.Ordinal = contextField.Ordinal != 0 ? contextField.Ordinal : ((IEnumerable<ContentTypeItemFieldContext>) context.Fields).Where<ContentTypeItemFieldContext>((Func<ContentTypeItemFieldContext, bool>) (f => f.ParentSectionId == newField.ParentSectionId && f.Name != newField.Name)).Count<ContentTypeItemFieldContext>();
            newField.GridColumnOrdinal = contextField.GridColumnOrdinal;
            newField.ShowInGrid = contextField.ShowInGrid;
            newField.Origin = contextField.Origin;
            dynamicModuleFieldList.Add(newField);
          }
        }
        else
        {
          if (StructureTransferBase.CurrentAddOnName == null || string.IsNullOrEmpty(dynamicModuleField.Origin) || dynamicModuleField.Origin == currentOrigin)
          {
            this.UpdateContentTypePersistentField(dynamicModuleField, contextField);
            dynamicModuleField.Ordinal = contextField.Ordinal;
            dynamicModuleField.GridColumnOrdinal = contextField.GridColumnOrdinal;
            dynamicModuleField.ShowInGrid = contextField.ShowInGrid;
            dynamicModuleField.ParentSectionId = this.GetParentSectionId(contextField, dynamicModuleType);
          }
          this.UpdateAddressFieldSectionTitle(dynamicModuleType, dynamicModuleField);
          dynamicModuleFieldList.Add(dynamicModuleField);
        }
      }
      foreach (DynamicModuleField dynamicModuleField in list.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (df => !df.IsTransient)))
      {
        DynamicModuleField dynamicField = dynamicModuleField;
        if (!((IEnumerable<ContentTypeItemFieldContext>) context.Fields).Any<ContentTypeItemFieldContext>((Func<ContentTypeItemFieldContext, bool>) (f => f.Name == dynamicField.Name)))
        {
          if (StructureTransferBase.IsDeleteAllowedForItem((IHasOrigin) dynamicField))
            dynamicField.FieldStatus = DynamicModuleFieldStatus.Removed;
          dynamicModuleFieldList.Add(dynamicField);
        }
      }
      dynamicModuleType.Fields = dynamicModuleFieldList.ToArray();
      this.ProcessTranslationsField(dynamicModuleType, list, section1);
      this.ProcessSystemParentIdField(dynamicModuleType, list, section2);
      return dynamicModuleType;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    internal void UpdateContentTypePersistentField(
      DynamicModuleField dynamicField,
      ContentTypeItemFieldContext fieldContext)
    {
      dynamicField.Title = fieldContext != null ? fieldContext.Title : throw new ArgumentNullException("field");
      dynamicField.ImageMaxSize = fieldContext.ImageMaxSize;
      dynamicField.WidgetTypeName = fieldContext.WidgetTypeName;
      dynamicField.FileMaxSize = fieldContext.FileMaxSize;
      dynamicField.VideoMaxSize = fieldContext.VideoMaxSize;
      dynamicField.NumberUnit = fieldContext.NumberUnit;
      dynamicField.RegularExpression = fieldContext.RegularExpression;
      dynamicField.CanSelectMultipleItems = fieldContext.CanSelectMultipleItems;
      dynamicField.MinNumberRange = fieldContext.MinNumberRange;
      dynamicField.MaxNumberRange = fieldContext.MaxNumberRange;
      dynamicField.InstructionalText = fieldContext.InstructionalText;
      dynamicField.IsRequired = fieldContext.IsRequired;
      int? nullable;
      if (fieldContext.MinLength.HasValue)
      {
        DynamicModuleField dynamicModuleField = dynamicField;
        nullable = fieldContext.MinLength;
        int num = nullable.Value;
        dynamicModuleField.MinLength = num;
      }
      nullable = fieldContext.MaxLength;
      if (nullable.HasValue)
      {
        DynamicModuleField dynamicModuleField = dynamicField;
        nullable = fieldContext.MaxLength;
        int num = nullable.Value;
        dynamicModuleField.MaxLength = num;
      }
      dynamicField.LengthValidationMessage = fieldContext.LengthValidationMessage;
      dynamicField.Choices = fieldContext.Choices;
      dynamicField.FileExtensions = fieldContext.FileExtensions;
      dynamicField.IsLocalizable = fieldContext.IsLocalizable;
      dynamicField.DisableLinkParser = fieldContext.DisableLinkParser;
      dynamicField.AllowMultipleImages = fieldContext.AllowMultipleImages;
      dynamicField.AllowMultipleVideos = fieldContext.AllowMultipleVideos;
      dynamicField.AllowMultipleFiles = fieldContext.AllowMultipleFiles;
      dynamicField.Ordinal = fieldContext.Ordinal;
      dynamicField.RecommendedCharactersCount = fieldContext.RecommendedCharactersCount;
      AddressFieldMode result;
      if (Enum.TryParse<AddressFieldMode>(fieldContext.AddressFieldMode, out result))
        dynamicField.AddressFieldMode = result;
      dynamicField.FrontendWidgetTypeName = fieldContext.FrontendWidgetTypeName;
      dynamicField.FrontendWidgetLabel = fieldContext.FrontendWidgetLabel;
      dynamicField.RelatedDataProvider = fieldContext.RelatedDataProvider;
    }

    /// <summary>
    /// Determines whether the specified dynamic module type has content type children.
    /// </summary>
    /// <param name="module">The dynamic module.</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <returns>
    ///  A value that indicates whether the specified dynamic module type has content type children.
    /// </returns>
    internal bool HasContentTypeChildren(DynamicModule module, DynamicModuleType dynamicModuleType)
    {
      bool flag = ((IEnumerable<DynamicModuleType>) module.Types).Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == dynamicModuleType.Id));
      return !dynamicModuleType.IsSelfReferencing & flag;
    }

    /// <summary>
    /// Gets the content child type of the specified dynamic module type.
    /// </summary>
    /// <param name="parentType">Type of the dynamic module.</param>
    /// <returns>Collection of content child types of the specified dynamic module type.</returns>
    internal IEnumerable<DynamicModuleType> GetChildTypes(
      DynamicModuleType parentType)
    {
      return (IEnumerable<DynamicModuleType>) this.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleTypeId == parentType.Id || t.IsSelfReferencing && t.Id == parentType.Id)).ToList<DynamicModuleType>();
    }

    /// <summary>
    /// Gets the content child type of the specified dynamic module type.
    /// </summary>
    /// <param name="parentType">Type of the dynamic module.</param>
    /// <returns>Collection of content child types of the specified dynamic module type.</returns>
    internal IEnumerable<Type> GetChildTypes(Type parentType)
    {
      IEnumerable<DynamicModuleType> childTypes = this.GetChildTypes(this.GetDynamicModuleType(parentType));
      if (childTypes != null && childTypes.Count<DynamicModuleType>() > 0)
      {
        foreach (DynamicModuleType dynamicModuleType in childTypes)
          yield return this.ResolveDynamicClrType(dynamicModuleType.GetFullTypeName());
      }
    }

    internal DynamicModuleField CreateIncludeInSitemapField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "IncludeInSitemap", "Include in sitemap", FieldSpecialType.IncludeInSitemap, false, 1, section.Id);
    }

    internal DynamicModuleField CreateUrlField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      string regularExpression = "^([\\.]?[\\p{L}-_!'()@\\d])+$";
      return this.CreateTransitientField(parentType.Id, "UrlName", "URL Name", FieldSpecialType.UrlName, false, 2, section.Id, true, regularExpression);
    }

    internal DynamicModuleField CreateAuthorField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "Author", "Author", FieldSpecialType.Author, true, 4, section.Id);
    }

    internal DynamicModuleField CreateActionsField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "Actions", "Actions", FieldSpecialType.Actions, true, 3, section.Id);
    }

    internal DynamicModuleField CreateParentIdField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "SystemParentId", "Parent", FieldSpecialType.ParentId, true, 6, section.Id);
    }

    internal DynamicModuleField CreateTranslationsField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "Translations", "Translations", FieldSpecialType.Translations, true, 1, section.Id);
    }

    internal DynamicModuleField CreateTransitientField(
      Guid parentTypeId,
      string dynamicFieldName,
      string dynamicFieldTitle,
      FieldSpecialType dynamicFieldSpecialType,
      bool showInGrid,
      int ordinalNumber,
      Guid sectionId,
      bool isRequired = false,
      string regularExpression = null)
    {
      DynamicModuleField dynamicModuleField = this.CreateDynamicModuleField();
      dynamicModuleField.ParentTypeId = parentTypeId;
      dynamicModuleField.Name = dynamicFieldName;
      dynamicModuleField.Title = dynamicFieldTitle;
      dynamicModuleField.SpecialType = dynamicFieldSpecialType;
      dynamicModuleField.ShowInGrid = showInGrid;
      dynamicModuleField.GridColumnOrdinal = ordinalNumber;
      dynamicModuleField.Ordinal = ordinalNumber;
      dynamicModuleField.ParentSectionId = sectionId;
      dynamicModuleField.IsTransient = true;
      dynamicModuleField.IsRequired = isRequired;
      dynamicModuleField.RegularExpression = regularExpression;
      return dynamicModuleField;
    }

    /// <summary>Creates the publication date field.</summary>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="section">The section.</param>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /></returns>
    protected DynamicModuleField CreatePublicationDateField(
      DynamicModuleType parentType,
      FieldsBackendSection section)
    {
      return this.CreateTransitientField(parentType.Id, "PublicationDate", "Publication date", FieldSpecialType.PublicationDate, true, 5, section.Id);
    }

    /// <summary>Creates the address field section.</summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="dynamicField">The dynamic field.</param>
    /// <param name="advancedSection">The advanced section.</param>
    /// <param name="addSectionToType">A value that indicates whether to add the section to the module type.</param>
    /// <param name="sectionId">The section identifier.</param>
    protected void CreateAddressFieldSection(
      DynamicModuleType dynamicModuleType,
      DynamicModuleField dynamicField,
      FieldsBackendSection advancedSection,
      bool addSectionToType,
      Guid sectionId)
    {
      FieldsBackendSection fieldsBackendSection1 = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Id == sectionId));
      if (fieldsBackendSection1 == null)
      {
        if (sectionId == Guid.Empty)
          sectionId = this.Provider.GetNewGuid();
        fieldsBackendSection1 = this.CreateFieldsBackendSection(sectionId, this.Provider.ApplicationName);
      }
      fieldsBackendSection1.ParentTypeId = dynamicModuleType.Id;
      fieldsBackendSection1.IsExpandable = true;
      fieldsBackendSection1.IsExpandedByDefault = true;
      fieldsBackendSection1.Name = dynamicField.Name;
      fieldsBackendSection1.Title = dynamicField.Title;
      if (advancedSection != null)
      {
        fieldsBackendSection1.Ordinal = advancedSection.Ordinal;
        ++advancedSection.Ordinal;
      }
      else
      {
        FieldsBackendSection fieldsBackendSection2 = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id)).LastOrDefault<FieldsBackendSection>();
        fieldsBackendSection1.Ordinal = fieldsBackendSection2 != null ? fieldsBackendSection2.Ordinal + 1 : 0;
      }
      dynamicField.ParentSectionId = fieldsBackendSection1.Id;
      if (!addSectionToType)
        return;
      List<FieldsBackendSection> list = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).ToList<FieldsBackendSection>();
      list.Add(fieldsBackendSection1);
      dynamicModuleType.Sections = list.OrderBy<FieldsBackendSection, int>((Func<FieldsBackendSection, int>) (s => s.Ordinal)).ToArray<FieldsBackendSection>();
    }

    /// <summary>Sets the fields ordinal.</summary>
    /// <param name="fields">The fields.</param>
    /// <param name="defaultSectionId">The default section identifier.</param>
    /// <param name="classificationSectionId">The classification section identifier.</param>
    /// <param name="relatedMediaSectionId">The related media section identifier.</param>
    /// <param name="relatedDataSectionId">The related data section identifier.</param>
    /// <param name="mainShortTextFieldName">Name of the main short text field.</param>
    protected void SetFieldsOrdinal(
      List<DynamicModuleField> fields,
      Guid defaultSectionId,
      Guid classificationSectionId,
      Guid relatedMediaSectionId,
      Guid relatedDataSectionId,
      string mainShortTextFieldName)
    {
      int ordinalCounter1 = 0;
      int ordinalCounter2 = this.SetSection(fields, defaultSectionId, ordinalCounter1, true, (Func<DynamicModuleField, bool>) (f => f.Name.Equals(mainShortTextFieldName)));
      int ordinalCounter3 = this.SetSection(fields, defaultSectionId, ordinalCounter2, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.LongText && f.WidgetTypeName.EndsWith("HtmlField")));
      int ordinalCounter4 = this.SetSection(fields, defaultSectionId, ordinalCounter3, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.LongText && f.WidgetTypeName.EndsWith("TextField")));
      int ordinalCounter5 = this.SetSection(fields, defaultSectionId, ordinalCounter4, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.ShortText && f.Name != mainShortTextFieldName));
      int ordinalCounter6 = this.SetSection(fields, defaultSectionId, ordinalCounter5, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.MultipleChoice || f.FieldType == FieldType.Choices));
      int ordinalCounter7 = this.SetSection(fields, defaultSectionId, ordinalCounter6, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.YesNo));
      int ordinalCounter8 = this.SetSection(fields, defaultSectionId, ordinalCounter7, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.DateTime));
      int ordinalCounter9 = this.SetSection(fields, defaultSectionId, ordinalCounter8, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Number));
      int ordinalCounter10 = this.SetSection(fields, defaultSectionId, ordinalCounter9, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.MediaType.Equals("image")));
      int ordinalCounter11 = this.SetSection(fields, relatedMediaSectionId, ordinalCounter10, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedMedia && f.MediaType.Equals("image")));
      int ordinalCounter12 = this.SetSection(fields, defaultSectionId, ordinalCounter11, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.MediaType.Equals("video")));
      int ordinalCounter13 = this.SetSection(fields, relatedMediaSectionId, ordinalCounter12, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedMedia && f.MediaType.Equals("video")));
      int ordinalCounter14 = this.SetSection(fields, defaultSectionId, ordinalCounter13, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.MediaType.Equals("file")));
      int ordinalCounter15 = this.SetSection(fields, relatedMediaSectionId, ordinalCounter14, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedMedia && f.MediaType.Equals("file")));
      int ordinalCounter16 = this.SetSection(fields, defaultSectionId, ordinalCounter15, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Guid));
      int ordinalCounter17 = this.SetSection(fields, defaultSectionId, ordinalCounter16, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.GuidArray));
      int ordinalCounter18 = this.SetSection(fields, classificationSectionId, ordinalCounter17, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Classification));
      this.SetSection(fields, relatedDataSectionId, ordinalCounter18, false, (Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedData));
    }

    private static string RemoveSpecialCharactersLeaveSpaces(string input) => new Regex("([^\\w\\s])", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant).Replace(input, string.Empty);

    private void SetFieldsOrder(
      List<DynamicModuleField> fields,
      ModuleBuilderManager.SectionIds sectionIds,
      string mainShortTextFieldName)
    {
      this.SetFieldsOrdinal(fields, sectionIds.DefaultSectionId, sectionIds.ClassificationSectionId, sectionIds.RelatedMediaSectionId, sectionIds.RelatedDataSectionId, mainShortTextFieldName);
      int count = fields.Count;
      int ordinalCounter = this.SetSection(fields, sectionIds.SeoSectionId, count, false, (Func<DynamicModuleField, bool>) (f => SocialMediaSeoTagHelpers.IsSeoTagField(f.Name)));
      this.SetSection(fields, sectionIds.SocialMediaSectionId, ordinalCounter, false, (Func<DynamicModuleField, bool>) (f => SocialMediaSeoTagHelpers.IsSocialMediaTagField(f.Name)));
    }

    private void DeleteFieldsBackendSections(DynamicModuleType dynamicType)
    {
      foreach (FieldsBackendSection section in dynamicType.Sections)
        this.Delete(section);
    }

    private void UpdateAddressFieldSectionTitle(
      DynamicModuleType dynamicModuleType,
      DynamicModuleField field)
    {
      if (field.FieldType != FieldType.Address)
        return;
      FieldsBackendSection fieldsBackendSection = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).Where<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Name == field.Name)).FirstOrDefault<FieldsBackendSection>();
      if (fieldsBackendSection == null)
        return;
      fieldsBackendSection.Title = field.Title;
    }

    private void SetPermissionHolderInheritanceAssociation(
      DynamicContentProvider permissionHolder,
      ISecuredObject mainObject)
    {
      switch (mainObject)
      {
        case DynamicModuleType dynamicModuleType:
          ISecuredObject securedObject = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) this, (ISecuredObject) this.GetItem(typeof (DynamicModule), dynamicModuleType.ParentModuleId), permissionHolder.Name);
          permissionHolder.CanInheritPermissions = true;
          permissionHolder.InheritsPermissions = true;
          this.Provider.CreatePermissionInheritanceAssociation(securedObject, (ISecuredObject) permissionHolder);
          break;
        case DynamicModule _:
          permissionHolder.CanInheritPermissions = true;
          permissionHolder.InheritsPermissions = true;
          this.Provider.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) permissionHolder);
          break;
      }
    }

    /// <summary>Updates AreaName and fixes widget templates.</summary>
    /// <param name="contentTypeContext">The content type context.</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="parentModule">The parent module.</param>
    private void UpdateContentTypeAreaName(
      ContentTypeContext contentTypeContext,
      DynamicModuleType dynamicModuleType,
      DynamicModule parentModule)
    {
      string oldAreaName = string.Format("{0} - {1}", (object) parentModule.Title, (object) dynamicModuleType.DisplayName);
      string str = string.Format("{0} - {1}", (object) parentModule.Title, (object) contentTypeContext.ContentTypeItemTitle);
      if (str == oldAreaName)
        return;
      this.UpdateWidgetsAreaName(oldAreaName, str);
      Type dataItemType = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName(), false);
      if (dataItemType == (Type) null)
        return;
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (DynamicContentViewMaster), dataItemType, (string) null, str, string.Format("{0} - list", (object) str));
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (DynamicContentViewDetail), dataItemType, (string) null, str, string.Format("{0} - single", (object) str));
    }

    private void UpdateModuleNameAndDescription(
      DynamicModule dynamicModule,
      ContentTypeSimpleContext contentTypeSimpleContext)
    {
      string title = dynamicModule.Title;
      string contentTypeTitle = contentTypeSimpleContext.ContentTypeTitle;
      dynamicModule.Title = contentTypeSimpleContext.ContentTypeTitle;
      dynamicModule.Description = contentTypeSimpleContext.ContentTypeDescription;
      if (dynamicModule.Status != DynamicModuleStatus.Active)
        return;
      App.WorkWith().Page(dynamicModule.PageId).Do((System.Action<PageNode>) (p => p.Title = (Lstring) contentTypeSimpleContext.ContentTypeTitle)).SaveChanges();
      IQueryable<DynamicModuleType> dynamicModuleTypes = this.GetDynamicModuleTypes();
      Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == dynamicModule.Id);
      foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
      {
        ConfigManager manager = ConfigManager.GetManager();
        DynamicModulesConfig section = manager.GetSection<DynamicModulesConfig>();
        section.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(dynamicModuleType.GetFullTypeName())].ViewsConfig[ModuleNamesGenerator.GenerateListViewName(dynamicModuleType.DisplayName)].Title = contentTypeSimpleContext.ContentTypeTitle;
        manager.SaveSection((ConfigSection) section);
      }
      this.UpdateWidgetsAreaName(title, contentTypeTitle);
    }

    private void UpdateWidgetsAreaName(string oldAreaName, string newAreaName)
    {
      PageManager manager = PageManager.GetManager();
      IQueryable<ControlPresentation> presentationItems = manager.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (i => i.AreaName == oldAreaName);
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
        controlPresentation.AreaName = newAreaName;
      manager.SaveChanges();
    }

    private FieldsBackendSection CreateSection(
      Guid dynamicModuleTypeId,
      Guid sectionId,
      string sectionName,
      string sectionTitle,
      int ordinal,
      bool isExpandable = false)
    {
      FieldsBackendSection fieldsBackendSection = this.CreateFieldsBackendSection(sectionId, this.Provider.ApplicationName);
      fieldsBackendSection.ParentTypeId = dynamicModuleTypeId;
      fieldsBackendSection.IsExpandable = isExpandable;
      fieldsBackendSection.Ordinal = ordinal;
      fieldsBackendSection.Name = "MainSection";
      fieldsBackendSection.Title = "Main Section";
      return fieldsBackendSection;
    }

    private int SetSection(
      List<DynamicModuleField> fields,
      Guid sectionId,
      int ordinalCounter,
      bool showInGrid,
      Func<DynamicModuleField, bool> filter)
    {
      foreach (DynamicModuleField dynamicModuleField in fields.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => filter(f))))
      {
        dynamicModuleField.ShowInGrid = showInGrid;
        dynamicModuleField.GridColumnOrdinal = ordinalCounter++;
        dynamicModuleField.Ordinal = ordinalCounter++;
        dynamicModuleField.ParentSectionId = sectionId;
      }
      return ordinalCounter;
    }

    /// <summary>
    /// Adds or marks the special translations field as Removed according to the multilingual state in which the dynamicModuleType is
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="originalFields">The original fields.</param>
    /// <param name="section">The section.</param>
    private void ProcessTranslationsField(
      DynamicModuleType dynamicModuleType,
      List<DynamicModuleField> originalFields,
      FieldsBackendSection section)
    {
      List<DynamicModuleField> dynamicModuleFieldList = new List<DynamicModuleField>((IEnumerable<DynamicModuleField>) dynamicModuleType.Fields);
      if (ModuleInstallerHelper.ContainsLocalizableFields((IEnumerable<IDynamicModuleField>) dynamicModuleType.Fields))
      {
        if (!originalFields.Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.Translations)))
        {
          if (section == null)
            section = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Name == "MoreOptions"));
          if (section != null)
          {
            DynamicModuleField translationsField = this.CreateTranslationsField(dynamicModuleType, section);
            dynamicModuleFieldList.Add(translationsField);
          }
        }
      }
      else if (originalFields.Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.Translations)))
      {
        DynamicModuleField dynamicModuleField = originalFields.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.Translations)).SingleOrDefault<DynamicModuleField>();
        this.Delete(dynamicModuleField);
        dynamicModuleFieldList.Remove(dynamicModuleField);
      }
      dynamicModuleType.Fields = dynamicModuleFieldList.ToArray();
    }

    /// <summary>
    /// Adds or removes the SystemParentId field according to the context
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="originalFields">The original fields.</param>
    /// <param name="section">The section.</param>
    private void ProcessSystemParentIdField(
      DynamicModuleType dynamicModuleType,
      List<DynamicModuleField> originalFields,
      FieldsBackendSection section)
    {
      List<DynamicModuleField> dynamicModuleFieldList = new List<DynamicModuleField>((IEnumerable<DynamicModuleField>) dynamicModuleType.Fields);
      if (dynamicModuleType.ParentModuleTypeId != Guid.Empty)
      {
        if (originalFields.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.ParentId)).SingleOrDefault<DynamicModuleField>() == null)
        {
          if (section == null)
            section = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).FirstOrDefault<FieldsBackendSection>((Func<FieldsBackendSection, bool>) (s => s.Name == "MoreOptions"));
          if (section != null)
          {
            DynamicModuleField parentIdField = this.CreateParentIdField(dynamicModuleType, section);
            dynamicModuleFieldList.Add(parentIdField);
          }
        }
      }
      else
      {
        DynamicModuleField dynamicModuleField = originalFields.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.ParentId)).SingleOrDefault<DynamicModuleField>();
        if (dynamicModuleField != null)
        {
          this.Delete(dynamicModuleField);
          dynamicModuleFieldList.Remove(dynamicModuleField);
        }
      }
      dynamicModuleType.Fields = dynamicModuleFieldList.ToArray();
    }

    /// <summary>
    /// Gets parent section id for the field, if Guid.Empty returns the id of the MainSection
    /// </summary>
    /// <param name="contextField">The context field.</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <returns>The parent section id for the field.</returns>
    private Guid GetParentSectionId(
      ContentTypeItemFieldContext contextField,
      DynamicModuleType dynamicModuleType)
    {
      if (contextField.ParentSectionId != Guid.Empty)
        return contextField.ParentSectionId;
      FieldsBackendSection fieldsBackendSection1 = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "MainSection")).SingleOrDefault<FieldsBackendSection>();
      if (fieldsBackendSection1 == null)
      {
        Guid newGuid = this.Provider.GetNewGuid();
        fieldsBackendSection1 = this.CreateSection(dynamicModuleType.Id, newGuid, "MainSection", "Main Section", 0);
        List<FieldsBackendSection> list = ((IEnumerable<FieldsBackendSection>) dynamicModuleType.Sections).ToList<FieldsBackendSection>();
        list.Insert(0, fieldsBackendSection1);
        dynamicModuleType.Sections = list.ToArray();
      }
      string name = contextField.Name;
      if (SocialMediaSeoTagHelpers.IsSeoTagField(name))
        return this.GetMetadataSection(dynamicModuleType, Res.Get<ModuleBuilderResources>().SEOSectionName, Res.Get<ModuleBuilderResources>().SEOSectionTitle, fieldsBackendSection1.Ordinal + 1).Id;
      if (SocialMediaSeoTagHelpers.IsSocialMediaTagField(name))
        return this.GetMetadataSection(dynamicModuleType, Res.Get<ModuleBuilderResources>().SocialMediaSectionName, Res.Get<ModuleBuilderResources>().SocialMediaSectionTitle, fieldsBackendSection1.Ordinal + 2).Id;
      FieldType result;
      if (Enum.TryParse<FieldType>(contextField.TypeName, out result) && result.Equals((object) FieldType.Classification))
      {
        FieldsBackendSection fieldsBackendSection2 = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "Classification")).SingleOrDefault<FieldsBackendSection>();
        if (fieldsBackendSection2 != null)
          return fieldsBackendSection2.Id;
      }
      return fieldsBackendSection1.Id;
    }

    private FieldsBackendSection GetMetadataSection(
      DynamicModuleType dynamicModuleType,
      string sectionName,
      string sectionTitle,
      int ordinal)
    {
      FieldsBackendSection metadataSection = this.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == sectionName)).SingleOrDefault<FieldsBackendSection>();
      if (metadataSection == null)
      {
        this.Provider.GetNewGuid();
        FieldsBackendSection advancedSection = this.GetFieldsBackendSections().FirstOrDefault<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == dynamicModuleType.Id && s.Name == "MoreOptions"));
        metadataSection = this.CreateOrGetFieldsBackendSection(dynamicModuleType, advancedSection, Guid.NewGuid(), sectionName, sectionTitle);
      }
      return metadataSection;
    }

    private void ClearDeletedSections(Guid parentTypeId, SectionFieldWrapper[] sections)
    {
      IQueryable<FieldsBackendSection> fieldsBackendSections = this.GetFieldsBackendSections();
      Expression<Func<FieldsBackendSection, bool>> predicate = (Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == parentTypeId);
      foreach (FieldsBackendSection fieldsBackendSection in (IEnumerable<FieldsBackendSection>) fieldsBackendSections.Where<FieldsBackendSection>(predicate))
      {
        bool flag = true;
        foreach (SectionFieldWrapper section in sections)
        {
          if (fieldsBackendSection.Id == section.Id && section.Id != Guid.Empty)
          {
            flag = false;
            break;
          }
        }
        if (flag)
          this.Delete(fieldsBackendSection);
      }
    }

    /// <summary>Fields sections' ids.</summary>
    private class SectionIds
    {
      internal Guid DefaultSectionId { get; set; }

      internal Guid ClassificationSectionId { get; set; }

      internal Guid RelatedMediaSectionId { get; set; }

      internal Guid RelatedDataSectionId { get; set; }

      internal Guid SeoSectionId { get; set; }

      internal Guid SocialMediaSectionId { get; set; }
    }
  }
}
