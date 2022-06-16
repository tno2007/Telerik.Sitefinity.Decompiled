// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ManagerBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Represents base class for Sitefinity data managers.</summary>
  /// <typeparam name="TProviderBase">
  /// Defines the data provider type for this manager. The data provider must inherit form
  /// <see cref="T:System.Configuration.Provider.ProviderBase">System.Configuration.Provider.ProviderBase</see> class.
  /// </typeparam>
  public abstract class ManagerBase<TProviderBase> : 
    IManager,
    IDisposable,
    IProviderResolver,
    IModuleManager,
    IVirtualProviderResolver
    where TProviderBase : DataProviderBase
  {
    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    public static EventHandler<ExecutingEventArgs> Executing;
    /// <summary>Fired after executing data method.</summary>
    public static EventHandler<ExecutedEventArgs> Executed;
    protected TProviderBase provider;
    internal static volatile bool initialized;
    private static GetDefaultProvider defaultProvider;
    private static Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> providers;
    private static Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> virtualProviders;
    private static readonly object syncRoot = new object();
    private static readonly object providersSyncRoot = new object();
    private static readonly Dictionary<Type, TProviderBase> providersMapping = new Dictionary<Type, TProviderBase>();
    private const string ProviderConfigError = "There is no configuration for data provider with the name of \"{0}\" or it is not enabled for \"{1}\" manager. Please check the spelling of the name and whether such configuration exists.";
    private const string ProviderRestrictionError = "The data provider with the name of \"{0}\" is not accessible in the current context for \"{1}\" manager. Please check the spelling of the name and whether the provider is shared with the current site";

    /// <summary>
    /// Initializes a new instance of ManagerBase class with the default provider.
    /// </summary>
    protected ManagerBase()
      : this(string.Empty, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of ManagerBase class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    protected ManagerBase(string providerName)
      : this(providerName, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of ManagerBase class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    /// <param name="transactionName">The name of a distributed transaction. If empty string or null this manager will use separate transaction.</param>
    protected ManagerBase(string providerName, string transactionName)
    {
      this.Initialize();
      if (string.IsNullOrEmpty(providerName))
      {
        providerName = ManagerBase<TProviderBase>.ResolveDefaultProviderName(this.GetType());
        if (string.IsNullOrEmpty(providerName))
          throw new ArgumentException(string.Format("No Default Provider for module {0}", (object) this.ModuleName));
      }
      this.SetProvider(providerName, transactionName);
    }

    /// <summary>Gets or set the name of a named global transaction.</summary>
    /// <value>The name of the transaction.</value>
    public virtual string TransactionName { get; set; }

    /// <summary>Gets the current provider</summary>
    public virtual TProviderBase Provider => this.provider;

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    public virtual bool Suspended => this.provider.Suspended;

    DataProviderBase IManager.Provider => (DataProviderBase) this.provider;

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    [Obsolete("No longer returns the providers, created when creating a site in Multisite managerment, as those providers are not instantiated on startup. To get all providers, use GetAllProviderInfos() extension method of IManager, which returns a collection of IDataProviderInfo objects (Name, Title, etc.), that describes the provider without instantiating it. Having the information about the provider, it can be instantiated using GetManager() method and providing the name of the provider.To get static providers instantiated from the configuration, StaticProviders property can be used.")]
    public virtual Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> Providers => this.StaticProviders;

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    [Obsolete("No longer returns the providers, created when creating a site in Multisite managerment, as those providers are not instantiated on startup. To get all providers, use GetAllProviderInfos() extension method of IManager, which returns a collection of IDataProviderInfo objects (Name, Title, etc.), that describes the provider without instantiating it. Having the information about the provider, it can be instantiated using GetManager() method and providing the name of the provider.To get static providers instantiated from the configuration, StaticProviders property can be used.")]
    IEnumerable<DataProviderBase> IManager.Providers => ((IManager) this).StaticProviders;

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    IEnumerable<DataProviderBase> IManager.StaticProviders => (IEnumerable<DataProviderBase>) new BaseEnumerable<DataProviderBase, TProviderBase>((IEnumerable<TProviderBase>) this.StaticProviders);

    /// <summary>
    /// Gets a collection of all static providers configured for this manager.
    /// </summary>
    public virtual Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> StaticProviders => ManagerBase<TProviderBase>.StaticProvidersCollection;

    /// <inheritdoc />
    public virtual IEnumerable<Type> GetKnownTypes() => (IEnumerable<Type>) this.Provider.GetKnownTypes();

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal abstract GetDefaultProvider DefaultProviderDelegate { get; }

    /// <summary>The name of the module that this manager belongs to.</summary>
    public abstract string ModuleName { get; }

    /// <summary>Collection of data provider settings.</summary>
    protected internal abstract ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings { get; }

    ConfigElementDictionary<string, DataProviderSettings> IModuleManager.ProvidersSettings => this.ProvidersSettings;

    /// <summary>
    /// Gets the providers settings.
    /// If you override this method, you shoud also override TryGetProviderSettings
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<IDataProviderSettings> GetProvidersSettings() => this.ProvidersSettings.Cast<IDataProviderSettings>();

    /// <summary>
    /// Tries the get provider settings.
    /// If you override this method, you shoud also override GetProvidersSettings
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    protected virtual bool TryGetProviderSettings(
      string providerName,
      out IDataProviderSettings settings)
    {
      return this.TryGetProviderSettings(providerName, (string) null, out settings);
    }

    /// <summary>
    /// Tries the get provider settings.
    /// If you override this method, you shoud also override GetProvidersSettings
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="dataSourceName">Name of the data source.</param>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    protected virtual bool TryGetProviderSettings(
      string providerName,
      string dataSourceName,
      out IDataProviderSettings settings)
    {
      DataProviderSettings providerSettings1;
      if (this.ProvidersSettings.TryGetValue(providerName, out providerSettings1))
      {
        if (providerSettings1.Enabled)
        {
          settings = (IDataProviderSettings) providerSettings1;
          return true;
        }
      }
      else if (SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext)
      {
        ISiteDataSource siteDataSource = (!dataSourceName.IsNullOrEmpty() ? multisiteContext.GetDataSourcesByName(dataSourceName) : multisiteContext.GetDataSourcesByManager(this.GetType().FullName)).FirstOrDefault<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Provider == providerName));
        if (siteDataSource != null)
        {
          string str = providerName;
          if (this.ProvidersSettings.TryGetValue(ManagerBase<TProviderBase>.GetDefaultProviderName(), out providerSettings1) && providerSettings1.Enabled)
          {
            ref IDataProviderSettings local = ref settings;
            VirtualDataProviderSettings providerSettings2 = new VirtualDataProviderSettings();
            providerSettings2.Name = providerName;
            providerSettings2.Enabled = true;
            providerSettings2.ProviderType = providerSettings1.ProviderType;
            providerSettings2.Parameters = new NameValueCollection(providerSettings1.Parameters);
            local = (IDataProviderSettings) providerSettings2;
            settings.Parameters["applicationName"] = str;
            settings.Parameters["title"] = string.IsNullOrEmpty(siteDataSource.Title) ? providerName : siteDataSource.Title;
            return true;
          }
        }
      }
      settings = (IDataProviderSettings) null;
      return false;
    }

    /// <summary>
    /// Gets a collection of defined data providers for this manager.
    /// </summary>
    [Obsolete("No longer returns the providers, created when creating a site in Multisite managerment, as those providers are not instantiated on startup. To get all providers, use GetAllProviderInfos() extension method of IManager, which returns a collection of IDataProviderInfo objects (Name, Title, etc.), that describes the provider without instantiating it. Having the information about the provider, it can be instantiated using GetManager() method and providing the name of the provider.To get static providers instantiated from the configuration, StaticProvidersCollection property can be used.")]
    public static Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> ProvidersCollection => ManagerBase<TProviderBase>.StaticProvidersCollection;

    /// <summary>
    /// Gets a collection of defined data providers for this manager.
    /// </summary>
    public static Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase> StaticProvidersCollection => ManagerBase<TProviderBase>.providers;

    internal object ObjectContainer { get; set; }

    object IManager.ObjectContainer
    {
      get => this.ObjectContainer;
      set => this.ObjectContainer = value;
    }

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    public virtual void SaveChanges() => this.SaveChanges((string) null);

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    /// <param name="eventOrigin">The origin that will be set to the events, thrown for this transaction</param>
    public virtual void SaveChanges(string eventOrigin)
    {
      if (this.TransactionName != null)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().GlobalTransactionUsed);
      IOpenAccessDataProvider provider = (object) this.provider as IOpenAccessDataProvider;
      if (!string.IsNullOrEmpty(eventOrigin) && provider != null)
        provider.GetContext().ExecutionStateBag.Add("EventOriginKey", (object) eventOrigin);
      this.Provider.CommitTransaction();
    }

    /// <summary>
    /// Cancels any changes made to objects retrieved with this manager.
    /// </summary>
    public virtual void CancelChanges()
    {
      if (this.TransactionName != null)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().GlobalTransactionUsed);
      this.Provider.RollbackTransaction();
    }

    /// <summary>Sets the provider for this instance.</summary>
    /// <param name="providerName">The name of the provider</param>
    protected internal virtual void SetProvider(string providerName, string transactionName)
    {
      TProviderBase providerBase = this.ResolveProvider(providerName);
      this.TransactionName = transactionName;
      Type type = this.GetType();
      if (SystemManager.CurrentContext.IsDataProviderRestricted(type, providerName))
        throw new MissingProviderConfigurationException("The data provider with the name of \"{0}\" is not accessible in the current context for \"{1}\" manager. Please check the spelling of the name and whether the provider is shared with the current site".Arrange((object) providerName, (object) type.FullName));
      if (string.IsNullOrEmpty(transactionName))
      {
        this.provider = providerBase;
      }
      else
      {
        this.provider = (TProviderBase) providerBase.Clone();
        this.provider.TransactionName = transactionName;
      }
    }

    protected internal virtual TProviderBase ResolveProvider(
      string providerName,
      string dataSourceName = null)
    {
      TProviderBase providerBase;
      if (!ManagerBase<TProviderBase>.providers.TryGetValue(providerName, out providerBase) && !ManagerBase<TProviderBase>.virtualProviders.TryGetValue(providerName, out providerBase))
      {
        lock (ManagerBase<TProviderBase>.providersSyncRoot)
        {
          if (!ManagerBase<TProviderBase>.providers.TryGetValue(providerName, out providerBase))
          {
            if (!ManagerBase<TProviderBase>.virtualProviders.TryGetValue(providerName, out providerBase))
            {
              Type type = this.GetType();
              IDataProviderSettings settings;
              if (!this.TryGetProviderSettings(providerName, dataSourceName, out settings))
                throw new MissingProviderConfigurationException("There is no configuration for data provider with the name of \"{0}\" or it is not enabled for \"{1}\" manager. Please check the spelling of the name and whether such configuration exists.".Arrange((object) providerName, (object) type.FullName));
              providerBase = ManagerBase<TProviderBase>.InstantiateProvider(settings, ExceptionPolicyName.DataProviders, type);
              if ((object) providerBase != null)
              {
                if (settings is VirtualDataProviderSettings)
                {
                  providerBase.IsVirtual = true;
                  ManagerBase<TProviderBase>.virtualProviders.Unlock();
                  ManagerBase<TProviderBase>.virtualProviders.Add(providerBase);
                  ManagerBase<TProviderBase>.virtualProviders.Lock();
                }
                else
                {
                  ManagerBase<TProviderBase>.providers.Unlock();
                  ManagerBase<TProviderBase>.providers.Add(providerBase);
                  ManagerBase<TProviderBase>.providers.Lock();
                }
              }
            }
          }
        }
      }
      return providerBase;
    }

    /// <summary>Initializes the manger.</summary>
    protected internal virtual void Initialize()
    {
      if (ManagerBase<TProviderBase>.initialized)
        return;
      lock (ManagerBase<TProviderBase>.syncRoot)
      {
        if (ManagerBase<TProviderBase>.initialized)
          return;
        ManagerBase<TProviderBase>.providersMapping.Clear();
        this.OnInitializing();
        try
        {
          ManagerBase<TProviderBase>.defaultProvider = this.DefaultProviderDelegate;
          ManagerBase<TProviderBase>.providers = new Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>();
          ManagerBase<TProviderBase>.virtualProviders = new Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>();
          ManagerBase<TProviderBase>.InstantiateProviders(this.GetProvidersSettings(), this);
          ManagerBase<TProviderBase>.initialized = true;
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders))
            throw;
        }
        this.OnInitialized();
        SystemManager.Shutdown += new EventHandler<EventArgs>(ManagerBase<TProviderBase>.System_Shutdown);
      }
    }

    private static void System_Shutdown(object sender, EventArgs e)
    {
      ManagerBase<TProviderBase>.Uninitialize();
      SystemManager.Shutdown -= new EventHandler<EventArgs>(ManagerBase<TProviderBase>.System_Shutdown);
    }

    internal static void Uninitialize()
    {
      if (ManagerBase<TProviderBase>.providers != null)
      {
        lock (ManagerBase<TProviderBase>.providersSyncRoot)
        {
          if (ManagerBase<TProviderBase>.providers != null)
          {
            ManagerBase<TProviderBase>.providers.Unlock();
            ManagerBase<TProviderBase>.providers.Clear();
            ManagerBase<TProviderBase>.providers = (Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>) null;
            ManagerBase<TProviderBase>.providersMapping.Clear();
          }
          if (ManagerBase<TProviderBase>.virtualProviders != null)
          {
            ManagerBase<TProviderBase>.virtualProviders.Unlock();
            ManagerBase<TProviderBase>.virtualProviders.Clear();
            ManagerBase<TProviderBase>.virtualProviders = (Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>) null;
          }
        }
      }
      lock (ManagerBase<TProviderBase>.syncRoot)
        ManagerBase<TProviderBase>.initialized = false;
    }

    /// <summary>
    /// This method is called before data provider initialization,
    /// when the manager is instantiated for the first time in the application lifecycle.
    /// </summary>
    protected virtual void OnInitializing()
    {
    }

    /// <summary>
    /// This method is called after data provider initialization,
    /// when the manager is instantiated for the first time in the application lifecycle.
    /// </summary>
    protected virtual void OnInitialized()
    {
    }

    private static string ResolveDefaultProviderName(Type managerType)
    {
      if (!Bootstrapper.IsReady || !managerType.ImplementsInterface(typeof (IDataSource)) || !SystemManager.DataSourceRegistry.IsDataSourceRegistered(managerType.FullName))
        return ManagerBase<TProviderBase>.GetDefaultProviderName();
      MultisiteContext.SiteDataSourceLinkProxy defaultProvider = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(managerType.FullName);
      return defaultProvider == null ? ManagerBase<TProviderBase>.GetDefaultProviderName() : defaultProvider.ProviderName;
    }

    /// <inheritdoc />
    public virtual DataProviderBase GetDefaultContextProvider()
    {
      string defaultProviderName = ManagerBase<TProviderBase>.ResolveDefaultProviderName(this.GetType());
      return this.GetContextProviders().SingleOrDefault<DataProviderBase>((Func<DataProviderBase, bool>) (p => p.Name == defaultProviderName)) ?? (DataProviderBase) this.Provider;
    }

    /// <inheritdoc />
    public virtual IEnumerable<DataProviderBase> GetContextProviders() => this.GetSiteProviders();

    /// <summary>
    /// Checks if the manager supports cache for the given type.
    /// </summary>
    /// <param name="t"></param>
    /// <returns>True if the provider supports caching for the given type. Otherwise false.</returns>
    internal virtual bool IsCacheEnabledFor(Type t) => this.IsCacheEnabledFor(t, default (TProviderBase));

    /// <summary>
    /// Checks if the provider supports cache for the given type.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="prov"></param>
    /// <returns>True if the provider supports caching for the given type. Otherwise false.</returns>
    internal virtual bool IsCacheEnabledFor(Type t, TProviderBase prov)
    {
      if ((object) prov == null)
        prov = this.Provider;
      return prov.IsCacheEnabledFor(t);
    }

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <returns>The manager instance.</returns>
    public static T GetManager<T>() where T : ManagerBase<TProviderBase>, new() => ManagerBase<TProviderBase>.GetManager<T>((string) null, (string) null);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static T GetManager<T>(string providerName) where T : ManagerBase<TProviderBase>, new() => ManagerBase<TProviderBase>.GetManager<T>(providerName, (string) null);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static T GetManager<T>(string providerName, string transactionName) where T : ManagerBase<TProviderBase>, new()
    {
      if (!ManagerBase<TProviderBase>.initialized)
        new T();
      if (string.IsNullOrEmpty(providerName))
        providerName = ManagerBase<TProviderBase>.ResolveDefaultProviderName(typeof (T));
      T manager;
      if (transactionName == null)
      {
        manager = (T) ManagerBase.ResolveManager(typeof (T), providerName);
      }
      else
      {
        string key = typeof (T).FullName + providerName + transactionName;
        ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
        if (currentTransactions != null)
        {
          manager = (T) currentTransactions[(object) key];
          if ((object) manager == null)
          {
            manager = ManagerBase<TProviderBase>.InstantiateManager<T>(providerName, transactionName);
            currentTransactions[(object) key] = (object) manager;
          }
        }
        else
          manager = ManagerBase<TProviderBase>.InstantiateManager<T>(providerName, transactionName);
      }
      return manager;
    }

    private static T InstantiateManager<T>(string providerName, string transactionName)
    {
      Type[] types = new Type[2]
      {
        typeof (string),
        typeof (string)
      };
      string[] parameters = new string[2]
      {
        providerName,
        transactionName
      };
      ConstructorInfo constructor = typeof (T).GetConstructor(types);
      return !(constructor == (ConstructorInfo) null) ? (T) constructor.Invoke((object[]) parameters) : throw new TargetInvocationException("Missing constructor for distributed transaction in manager \"{0}\".".Arrange((object) typeof (T).FullName), (Exception) null);
    }

    /// <summary>Gets the default name of the provider.</summary>
    /// <returns>The name of the default provider.</returns>
    public static string GetDefaultProviderName() => ManagerBase<TProviderBase>.defaultProvider != null ? ManagerBase<TProviderBase>.defaultProvider() : string.Empty;

    private static void InstantiateProviders(
      IEnumerable<IDataProviderSettings> configProviders,
      ManagerBase<TProviderBase> manager)
    {
      lock (ManagerBase<TProviderBase>.providersSyncRoot)
      {
        if (ManagerBase<TProviderBase>.providers == null)
          ManagerBase<TProviderBase>.providers = new Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>();
        ManagerBase<TProviderBase>.providers.Unlock();
        if (ManagerBase<TProviderBase>.virtualProviders == null)
          ManagerBase<TProviderBase>.virtualProviders = new Telerik.Sitefinity.Abstractions.ProvidersCollection<TProviderBase>();
        ManagerBase<TProviderBase>.virtualProviders.Unlock();
        Type type = manager.GetType();
        using (new MethodPerformanceRegion("Instantiate '{0}' providers".Arrange((object) type.FullName)))
        {
          foreach (IDataProviderSettings providerSettings in configProviders.Where<IDataProviderSettings>((Func<IDataProviderSettings, bool>) (p => p.Enabled)))
          {
            TProviderBase providerBase = ManagerBase<TProviderBase>.InstantiateProvider(providerSettings, ExceptionPolicyName.IgnoreExceptions, type);
            if ((object) providerBase != null)
            {
              if (providerSettings is VirtualDataProviderSettings)
              {
                providerBase.IsVirtual = true;
                ManagerBase<TProviderBase>.virtualProviders.Add(providerBase);
              }
              else
                ManagerBase<TProviderBase>.providers.Add(providerBase);
            }
          }
        }
        ManagerBase<TProviderBase>.providers.Lock();
        ManagerBase<TProviderBase>.virtualProviders.Lock();
      }
    }

    internal static void EnsureInitialized<T>() where T : ManagerBase<TProviderBase>, new()
    {
      if (ManagerBase<TProviderBase>.initialized)
        return;
      new T();
    }

    private static TProviderBase InstantiateProvider(
      IDataProviderSettings providerSettings,
      ExceptionPolicyName policy,
      Type managerType)
    {
      return ManagerBase<TProviderBase>.InstantiateProvider(providerSettings, typeof (TProviderBase), policy, managerType);
    }

    private static TProviderBase InstantiateProvider(
      IDataProviderSettings providerSettings,
      Type providerType,
      ExceptionPolicyName policy,
      Type managerType)
    {
      long startTime = PerformanceMonitor.Begin();
      TProviderBase providerBase;
      try
      {
        Type providerType1 = providerSettings.ProviderType;
        providerBase = providerType.IsAssignableFrom(providerType1) ? ManagerBase<TProviderBase>.ResolveProvider(providerType1) : throw new ArgumentException("Invalid type specified" + " " + providerType.ToString());
        LicenseLimitations.ValidateModuleLicensed((object) providerBase);
        NameValueCollection providerParameters = ManagerBase.GetProviderParameters(providerSettings);
        providerBase.Initialize(providerSettings.Name, providerParameters, managerType);
        ManagerBase<TProviderBase>.CheckForUpgradeLegacy(providerBase, providerSettings, providerType1);
        providerBase.Executing += new EventHandler<ExecutingEventArgs>(ManagerBase<TProviderBase>.Provider_Executing);
        providerBase.Executed += new EventHandler<ExecutedEventArgs>(ManagerBase<TProviderBase>.Provider_Executed);
      }
      catch (Exception ex)
      {
        int policy1 = (int) policy;
        if (!Exceptions.HandleException(ex, (ExceptionPolicyName) policy1))
          return default (TProviderBase);
        throw;
      }
      finally
      {
        PerformanceMonitor.End("Sitefinity Core", "Data Provider Initialization", startTime);
      }
      return providerBase;
    }

    private static TProviderBase ResolveProvider(Type type)
    {
      TProviderBase providerBase;
      if (!ManagerBase<TProviderBase>.providersMapping.TryGetValue(type, out providerBase))
      {
        if (!ObjectFactory.IsTypeRegistered(type) && type.GetCustomAttributes(typeof (ApplyNoPoliciesAttribute), true).Length == 0)
          ObjectFactory.Container.RegisterType(type).Configure<Interception>().SetInterceptorFor(type, (ITypeInterceptor) new VirtualMethodInterceptor());
        providerBase = (TProviderBase) ObjectFactory.Resolve(type);
        ManagerBase<TProviderBase>.providersMapping.Add(type, providerBase);
      }
      return (TProviderBase) providerBase.CloneInternal();
    }

    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (ManagerBase<TProviderBase>.Executing != null)
        ManagerBase<TProviderBase>.Executing(sender, args);
      App.OnExecuting(sender, args);
    }

    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (ManagerBase<TProviderBase>.Executed != null)
        ManagerBase<TProviderBase>.Executed(sender, args);
      App.OnExecuted(sender, args);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.DisposeInternal();

    private void DisposeInternal()
    {
      if (this.ObjectContainer is IDisposable objectContainer)
        objectContainer.Dispose();
      this.ObjectContainer = (object) null;
      if ((object) this.Provider == null)
        return;
      this.Provider.DoWithRelatedManagers((Action<IManager>) (m => m.Dispose()));
    }

    /// <summary>
    /// When overridden creates new instance of data item and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>New data item.</returns>
    public virtual object CreateItem(Type itemType) => this.Provider.CreateItem(itemType);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public virtual object CreateItem(Type itemType, Guid id) => this.Provider.CreateItem(itemType, id);

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public virtual object GetItem(Type itemType, Guid id) => this.Provider.GetItem(itemType, id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public virtual object GetItemOrDefault(Type itemType, Guid id) => this.Provider.GetItemOrDefault(itemType, id);

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns></returns>
    public virtual IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public virtual IEnumerable GetItems(
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
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public virtual void DeleteItem(object item)
    {
      IHasContentChildren hasContentChildren = item as IHasContentChildren;
      IEnumerable<Content> children = (IEnumerable<Content>) null;
      if (hasContentChildren != null)
        children = hasContentChildren.ChildContentItems;
      if (item is IHasTrackingContext context)
      {
        string[] availableLanguages = this.GetAvailableLanguages(item, false);
        context.RegisterDeletedOperation(availableLanguages, (IEnumerable<IHasTrackingContext>) children);
      }
      this.Provider.DeleteItem(item);
    }

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="language">The language version to delete.</param>
    public virtual void DeleteItem(object item, CultureInfo language)
    {
      string[] availableLanguages = this.GetAvailableLanguages(item, false);
      if (language == null || availableLanguages.Length == 0 || this.IsTheLastAvailableLanguage(language.Name, availableLanguages))
      {
        this.DeleteItem(item);
        if (!(item is ILifecycleDataItem lifecycleDataItem) || (lifecycleDataItem.Status == ContentLifecycleStatus.Master ? 1 : (lifecycleDataItem.Status == ContentLifecycleStatus.Deleted ? 1 : 0)) == 0)
          return;
        object liveItem = this.GetLiveItem(item);
        if (liveItem == null)
          return;
        this.RegisterDeleteOperation(liveItem, availableLanguages);
      }
      else
      {
        this.DeleteLanguageVersion(item, language);
        object liveItem = this.GetLiveItem(item);
        if (liveItem == null)
          return;
        this.DeleteLanguageVersion(liveItem, language);
      }
    }

    private object GetLiveItem(object item)
    {
      switch (item)
      {
        case Content cnt2:
        case ILifecycleDataItem _:
          if (this is IContentLifecycleManager lifecycleManager1 && cnt2 != null && cnt2.SupportsContentLifecycle && cnt2.Status == ContentLifecycleStatus.Master)
            return (object) lifecycleManager1.GetLive(cnt2);
          ILifecycleManager lifecycleManager2 = this as ILifecycleManager;
          ILifecycleDataItem cnt1 = item as ILifecycleDataItem;
          return lifecycleManager2 != null && cnt1 != null ? (object) lifecycleManager2.Lifecycle.GetLive(cnt1) : (object) null;
        default:
          return item;
      }
    }

    internal void RegisterDeleteOperation(object item, string[] languages) => this.RegisterOperation(item, OperationStatus.Deleted, languages);

    internal void RegisterOperation(object item, OperationStatus operation, string[] languages)
    {
      if (!(item is IHasTrackingContext context))
        return;
      context.RegisterOperation(operation, languages);
    }

    /// <summary>
    /// Gets the all the secured objects which inherit permissions, through permissions hierarchy, from a secured object.
    /// </summary>
    /// <param name="root">The root object for which the permission inheritors are scanned. Must implement the ISecuredObject interface</param>
    /// <param name="inheritors">A empty list of ISecuredObject object. This argument is required for the recursion of the method.</param>
    /// <param name="permissionInheritorsOnly">if set to <c>true</c>, only the child objects for which the InheritsPermissions property is set to true are returned. Otherwise, all child objects are returned.</param>
    /// <param name="objectType">If set to null, all child objects are returned. If set to a Type, only child objects of the type are returned.</param>
    /// <returns>
    /// A List of ISecured objects which inherit permissions, through permissions hierarchy, from the root object.
    /// </returns>
    public List<ISecuredObject> GetPermissionsInheritors(
      ISecuredObject root,
      bool permissionInheritorsOnly,
      Type objectType)
    {
      return this.Provider.GetPermissionsInheritors(root, permissionInheritorsOnly, objectType);
    }

    /// <summary>
    /// This recursive method handles the correct behavior when the inheritance is broken or restored on a secured object:
    /// 1. The direct permissions inheritors (children in the tree) of the object (parent), are removed all their inherited permissions.
    ///   - When the inheritance on the root is broken, each child is  assigned the permissions from the parent object.
    ///   - When the inheritance on the root is restored, each child is  assigned the permissions from the parent's inherited permissions.
    ///   (the actions are the same, but the permissions set in the permissionsToAssignToChildren variable make the difference).
    /// 2. For each of the children, if it inherits permissions, the assignment (#1) continues recursively down to the grandchildren, and so on.
    /// 3. If down the tree, an offspring (not a direct child of the root) does not inherit permissions, the function will not continue to its children.
    /// </summary>
    /// <param name="brokenOrRestoredInheritanceRootObjectId">The broken or restored inheritance root object id.</param>
    /// <param name="permissionsToAssignToChildren">The permissions to assign to children.</param>
    /// <param name="currentlyScannedNode">The currently scanned node in the recursion.</param>
    /// <param name="transactionName">The name of the transaction for the child object changes.</param>
    [Obsolete("This method is part of break/restore inheritance behavior and it is not intended to use separately.")]
    public void ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(
      Guid brokenInheritanceRootObjectId,
      List<Permission> brokenInheritanceRootUninheritedPermissions,
      ISecuredObject currentlyScannedNode,
      string transactionName)
    {
      this.Provider.ResetPermissionsForChildNodesWhenInheritanceIsBrokenOrRestored(brokenInheritanceRootObjectId, brokenInheritanceRootUninheritedPermissions, currentlyScannedNode, transactionName);
    }

    /// <summary>
    /// Executes permission inheritance between a parent and a child hierarchical secured objects.
    /// </summary>
    /// <param name="parent">The permissions parent.</param>
    /// <param name="child">The permissions child.</param>
    public void CreatePermissionInheritanceAssociation(ISecuredObject parent, ISecuredObject child) => this.Provider.CreatePermissionInheritanceAssociation(parent, child);

    /// <summary>Breaks the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void BreakPermiossionsInheritance(ISecuredObject securedObject) => this.Provider.BreakPermiossionsInheritance(securedObject);

    /// <summary>Restores the permissions inheritance.</summary>
    /// <param name="securedObject">The secured object.</param>
    public void RestorePermissionsInheritance(ISecuredObject securedObject) => this.Provider.RestorePermissionInheritance(securedObject);

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public virtual Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Provider.GetPermission(permissionSet, objectId, principalId);
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public virtual IQueryable<Permission> GetPermissions() => this.Provider.GetPermissions();

    /// <summary>Gets a query for permissions.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <returns></returns>
    public virtual IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Permission => this.Provider.GetPermissions<TPermission>();

    /// <summary>Gets a query for permissions.</summary>
    /// <param name="actualType">The actual type.</param>
    /// <returns></returns>
    public virtual IQueryable<Permission> GetPermissions(Type actualType) => this.Provider.GetPermissions(actualType);

    /// <summary>Creates the permission.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public virtual Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Provider.CreatePermission(permissionSet, objectId, principalId);
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public virtual void DeletePermission(Permission permission) => this.Provider.DeletePermission(permission);

    /// <summary>
    /// Makes a deep copy of the permission from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source collection of permissions.</param>
    /// <param name="target">The target list.</param>
    /// <param name="sourceObjectId">The source object pageId.</param>
    /// <param name="targetObjectId">The target object pageId.</param>
    public virtual void CopyPermissions(
      IEnumerable<Permission> source,
      IList target,
      Guid sourceObjectId,
      Guid targetObjectId)
    {
      this.Provider.CopyPermissions(source, target, sourceObjectId, targetObjectId, false);
    }

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public virtual ISecuredObject GetSecurityRoot() => this.Provider.GetSecurityRoot();

    /// <summary>Gets the security root for this provider.</summary>
    public virtual ISecuredObject SecurityRoot => this.Provider.SecurityRoot;

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    public virtual ISecuredObject GetSecurityRoot(bool create) => this.Provider.GetSecurityRoot(create);

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      Permission permission,
      string transactionName)
    {
      this.Provider.AddPermissionToObject(securedObject, permission, transactionName);
    }

    /// <summary>
    /// Adds a permission to a secured object, and handles inheritance throughout the tree.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="managerInstace">This parameter is obsolete - Instance of the secured object's related manager.</param>
    /// <param name="permission">The permission.</param>
    /// <param name="transactionName">This parameter is obsolete - Name of the transaction.</param>
    public virtual void AddPermissionToObject(
      ISecuredObject securedObject,
      IManager managerInstace,
      Permission permission,
      string transactionName)
    {
      this.Provider.AddPermissionToObject(securedObject, managerInstace, permission, transactionName);
    }

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    public virtual IQueryable<PermissionsInheritanceMap> GetInheritanceMaps() => this.Provider.GetInheritanceMaps();

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    public virtual void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap)
    {
      this.provider.DeletePermissionsInheritanceMap(permissionsInheritanceMap);
    }

    /// <summary>Gets the result for a dynamic LINQ query as list.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The LINQ query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of items to skip. If the value is zero, no items are skipped.</param>
    /// <param name="take">The number of items to take. If the value is zero, all items are taken.</param>
    /// <param name="totalCount">The total count of items regardless skip and take.</param>
    /// <returns></returns>
    public virtual IList<TItem> GetList<TItem>(
      IQueryable<TItem> query,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount,
      ForEachItem<TProviderBase, TItem> forEach)
      where TItem : IDataItem
    {
      return this.Provider.GetList<TProviderBase, TItem>(query, filterExpression, sortExpression, skip, take, out totalCount, forEach);
    }

    /// <summary>
    /// Gets the result for a dynamic LINQ query as enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The LINQ query.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of items to skip. If the value is zero, no items are skipped.</param>
    /// <param name="take">The number of items to take. If the value is zero, all items are taken.</param>
    /// <param name="totalCount">The total count of items regardless skip and take.</param>
    /// <returns></returns>
    public virtual IEnumerable<T> GetEnumerable<T>(
      IQueryable<T> query,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.Provider.GetEnumerable<T>(query, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Joins the results from the specified data providers.
    /// If no data providers are specified, all declared providers for this manager will be used.
    /// </summary>
    /// <typeparam name="T">The type of data item.</typeparam>
    /// <param name="getQuery">The query delegate.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <param name="providers">The providers.</param>
    /// <returns></returns>
    public static IList<TItem> JoinResult<TItem>(
      GetQuery<TProviderBase, TItem> getQuery,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount,
      ForEachItem<TProviderBase, TItem> forEach,
      params string[] providerNames)
      where TItem : IDataItem
    {
      totalCount = 0;
      List<TItem> allItems = new List<TItem>();
      IManager manager;
      foreach (TProviderBase provider in !ManagerBase.TryGetMappedManager(typeof (TItem), (string) null, out manager) ? (IEnumerable<TProviderBase>) ManagerBase<TProviderBase>.StaticProvidersCollection : manager.GetContextProviders().Cast<TProviderBase>())
      {
        if (!(provider.ProviderGroup == "System") && (providerNames == null || providerNames.Length == 0 || ((IEnumerable<string>) providerNames).Contains<string>(provider.Name)))
        {
          int totalCount1;
          if (forEach != null)
            provider.GetList<TProviderBase, TItem>(getQuery(provider), filterExpression, sortExpression, 0, skip + take, out totalCount1, (ForEachItem<TProviderBase, TItem>) ((p, i) => allItems.Add(i)));
          else
            allItems.AddRange((IEnumerable<TItem>) provider.GetList<TProviderBase, TItem>(getQuery(provider), filterExpression, sortExpression, 0, skip + take, out totalCount1, (ForEachItem<TProviderBase, TItem>) null));
          totalCount += totalCount1;
        }
      }
      IEnumerable<TItem> source = (IEnumerable<TItem>) null;
      if (!string.IsNullOrEmpty(sortExpression))
        source = (IEnumerable<TItem>) ((IEnumerable<TItem>) allItems).AsQueryable<TItem>().OrderBy<TItem>(sortExpression);
      if (skip != 0)
        source = source != null ? source.Skip<TItem>(skip) : ((IEnumerable<TItem>) allItems).Skip<TItem>(skip);
      if (take != 0)
        source = source != null ? source.Take<TItem>(take) : ((IEnumerable<TItem>) allItems).Take<TItem>(take);
      if (source == null)
      {
        if (forEach != null)
        {
          foreach (TItem obj in allItems)
            forEach((TProviderBase) obj.Provider, obj);
        }
        return (IList<TItem>) allItems;
      }
      if (forEach == null)
        return (IList<TItem>) source.ToList<TItem>();
      List<TItem> objList = new List<TItem>(allItems.Count);
      foreach (TItem obj in source)
      {
        forEach((TProviderBase) obj.Provider, obj);
        objList.Add(obj);
      }
      return (IList<TItem>) objList;
    }

    /// <summary>
    /// Gets the total items count for the joined results from the specified providers.
    /// If no data providers are specified, all declared providers for this manager will be used.
    /// </summary>
    /// <typeparam name="T">The type of data item.</typeparam>
    /// <param name="getQuery">The query delegate.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="providers">The providers.</param>
    /// <returns>The total items count.</returns>
    public static int CountResult<TItem>(
      GetQuery<TProviderBase, TItem> getQuery,
      string filterExpression,
      params string[] providers)
      where TItem : IDataItem
    {
      IManager manager;
      IEnumerable<TProviderBase> providerBases = !ManagerBase.TryGetMappedManager(typeof (TItem), (string) null, out manager) ? (IEnumerable<TProviderBase>) ManagerBase<TProviderBase>.StaticProvidersCollection : manager.GetContextProviders().Cast<TProviderBase>();
      int num = 0;
      foreach (TProviderBase provider in providerBases)
      {
        if (providers == null || ((IEnumerable<string>) providers).Contains<string>(provider.Name))
          num += provider.GetCount<TItem>(getQuery(provider), filterExpression);
      }
      return num;
    }

    protected internal void InstatiateProvider(IDataProviderSettings configProvider) => ManagerBase<TProviderBase>.InstantiateProviders((IEnumerable<IDataProviderSettings>) new List<IDataProviderSettings>()
    {
      configProvider
    }, this);

    protected internal void RemoveProvider(string providerName)
    {
      lock (ManagerBase<TProviderBase>.providersSyncRoot)
      {
        ManagerBase<TProviderBase>.providers.Unlock();
        int num = ManagerBase<TProviderBase>.providers.Remove(providerName) ? 1 : 0;
        ManagerBase<TProviderBase>.providers.Lock();
        if (num != 0)
          return;
        ManagerBase<TProviderBase>.virtualProviders.Unlock();
        ManagerBase<TProviderBase>.virtualProviders.Remove(providerName);
        ManagerBase<TProviderBase>.virtualProviders.Lock();
      }
    }

    /// <summary>
    /// Gets the available languages of the specified <paramref name="item" />.
    /// </summary>
    /// <param name="item">The item which languages will be returned.</param>
    /// <param name="includeInvariantLanguage">
    /// Specifies whether to include the invariant language in the returned array.</param>
    /// <returns>
    /// An empty array if the item is not <see cref="T:Telerik.Sitefinity.Localization.ILocalizable" />,
    /// otherwise the AvailableLanguages of the item.
    /// </returns>
    protected internal string[] GetAvailableLanguages(object item, bool includeInvariantLanguage = true)
    {
      IEnumerable<string> source = (IEnumerable<string>) new string[0];
      if (item is ILocalizable localizableItem)
        source = localizableItem.GetAvailableLanguagesIgnoringContext().Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name));
      if (!includeInvariantLanguage)
        source = source.Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name));
      return source.ToArray<string>();
    }

    /// <summary>
    /// Determines whether the specified <paramref name="language" /> is the last language in
    /// the collection of <paramref name="availableLanguages" /> excluding the invariant language.
    /// </summary>
    /// <param name="language">The language to check for.</param>
    /// <param name="availableLanguages">The available languages.</param>
    /// <returns>Whether the specified <paramref name="language" /> is the last language in
    /// the collection of <paramref name="availableLanguages" /> excluding the invariant language.</returns>
    protected internal bool IsTheLastAvailableLanguage(string language, string[] availableLanguages)
    {
      if (AppSettings.CurrentSettings.LegacyMultilingual)
      {
        IEnumerable<string> source = ((IEnumerable<string>) availableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name));
        return source.Count<string>() == 1 && source.Contains<string>(language);
      }
      return ((IEnumerable<string>) availableLanguages).Count<string>() == 1 && ((IEnumerable<string>) availableLanguages).Contains<string>(language);
    }

    protected internal virtual void DeleteLanguageVersion(object item, CultureInfo language)
    {
      this.RegisterDeleteOperation(item, new string[1]
      {
        language.Name
      });
      LocalizationHelper.ClearLstringPropertiesForLanguage(item, language);
      if (!(item is ILifecycleDataItem))
        return;
      ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
      lifecycleDataItem.RemovePublishedTranslation(language.Name);
      LanguageData languageData1 = lifecycleDataItem.LanguageData.SingleOrDefault<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == language.Name));
      if (languageData1 != null)
        lifecycleDataItem.LanguageData.Remove(languageData1);
      if (!(language.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name))
        return;
      LanguageData languageData2 = lifecycleDataItem.LanguageData.SingleOrDefault<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == null));
      if (languageData2 == null)
        return;
      lifecycleDataItem.LanguageData.Remove(languageData2);
    }

    DataProviderBase IVirtualProviderResolver.ResolveVirtualProvider(
      string providerName,
      string dataSourceName = null)
    {
      return (DataProviderBase) this.ResolveProvider(providerName, dataSourceName);
    }

    private static void CheckForUpgradeLegacy(
      TProviderBase provider,
      IDataProviderSettings providerSettings,
      Type actualType)
    {
      if (!provider.CheckForUpdates || !(providerSettings is DataProviderSettings providerSettings1))
        return;
      string parameter = providerSettings1.Parameters["version"];
      Version result;
      if (string.IsNullOrEmpty(parameter) || !Version.TryParse(parameter, out result))
        return;
      Version version = actualType.Assembly.GetName().Version;
      if (!(result == (Version) null) && !(result < version))
        return;
      ConfigProvider provider1 = providerSettings1.Section.Provider;
      provider.Upgrade(result);
      bool disableSecurityChecks = ConfigProvider.DisableSecurityChecks;
      try
      {
        ConfigProvider.DisableSecurityChecks = true;
        providerSettings1.Parameters["version"] = version.ToString();
        provider1.SaveSection(providerSettings1.Section);
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      finally
      {
        ConfigProvider.DisableSecurityChecks = disableSecurityChecks;
      }
    }
  }
}
