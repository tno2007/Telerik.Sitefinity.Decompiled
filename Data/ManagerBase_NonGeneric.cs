// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ManagerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Helper class that is used to get instances of IManager
  /// </summary>
  public static class ManagerBase
  {
    private static Dictionary<Type, MethodInfo> methodCache = new Dictionary<Type, MethodInfo>();
    private static Type[] getManagerSignature = new Type[1]
    {
      typeof (string)
    };
    private static Type[] getManagerInTransactionSignature;
    private static Dictionary<Type, Type> managerCache = new Dictionary<Type, Type>();
    private static Dictionary<Type, BlankItemDelegate> blankItemDelegatesCache = new Dictionary<Type, BlankItemDelegate>();
    private static Dictionary<Type, MethodInfo> methodInTransactionCache = new Dictionary<Type, MethodInfo>();
    private static readonly Dictionary<Type, Tuple<HashSet<string>, HashSet<string>>> registeredManagers;

    /// <summary>Initializes the manager base state</summary>
    static ManagerBase()
    {
      ManagerBase.getManagerInTransactionSignature = new Type[2]
      {
        typeof (string),
        typeof (string)
      };
      ManagerBase.registeredManagers = new Dictionary<Type, Tuple<HashSet<string>, HashSet<string>>>();
      SystemManager.Shutdown += new EventHandler<EventArgs>(ManagerBase.SystemManager_Shutdown);
    }

    private static void SystemManager_Shutdown(object sender, EventArgs e)
    {
      ManagerBase.methodCache.Clear();
      ManagerBase.managerCache.Clear();
      ManagerBase.methodInTransactionCache.Clear();
      ManagerBase.blankItemDelegatesCache.Clear();
    }

    /// <summary>Fired when a manager type needs to be resolved</summary>
    public static event EventHandler<NeeedsManagerTypeEventArgs> NeedsManagerType;

    /// <summary>Fires the NeedsManagerType event</summary>
    /// <param name="args">Event data</param>
    /// <returns>Result of the NeedsManagerType event: manager type.</returns>
    private static Type OnNeedsManagerType(NeeedsManagerTypeEventArgs args)
    {
      if (ManagerBase.NeedsManagerType != null)
        ManagerBase.NeedsManagerType((object) null, args);
      return args.ManagerType;
    }

    private static Type GetManagerTypeFromAttribute(Type itemType)
    {
      if (TypeDescriptor.GetAttributes(itemType)[typeof (ManagerTypeAttribute)] is ManagerTypeAttribute attribute1 && attribute1.ManagerType != (Type) null && typeof (IManager).IsAssignableFrom(attribute1.ManagerType))
        return attribute1.ManagerType;
      return TypeDescriptor.GetAttributes(itemType)[typeof (InheritableManagerTypeAttribute)] is InheritableManagerTypeAttribute attribute2 && attribute2.ManagerType != (Type) null && typeof (IManager).IsAssignableFrom(attribute2.ManagerType) ? attribute2.ManagerType : (Type) null;
    }

    /// <summary>Gets a mapping from the configuration</summary>
    /// <param name="itemType">Content item type</param>
    /// <returns>Type of the manager that serves the content item type, as specified in the configuration</returns>
    private static Type GetManagerTypeFromConfiguration(Type itemType)
    {
      ItemToManagerMappingConfig managerMappingConfig = Config.Get<ItemToManagerMappingConfig>();
      ItemManagerMappingElement managerMappingElement = (ItemManagerMappingElement) null;
      if (managerMappingConfig.Map.TryGetValue(itemType, out managerMappingElement))
      {
        Type managerType = managerMappingElement.ManagerType;
        if (managerType != (Type) null && typeof (IManager).IsAssignableFrom(managerType))
          return managerType;
      }
      return (Type) null;
    }

    /// <summary>
    /// Combines the logic for mapping a manager type to a certain content type.
    /// </summary>
    /// <param name="itemType">Content item type</param>
    /// <returns>Type of the manager that serves the content item type</returns>
    private static Type GetMappedManagerTypeInternal(Type itemType, bool throwException = true)
    {
      Type managerType;
      if (!ManagerBase.managerCache.TryGetValue(itemType, out managerType))
      {
        lock (ManagerBase.managerCache)
        {
          if (!ManagerBase.managerCache.TryGetValue(itemType, out managerType))
          {
            managerType = ManagerBase.GetManagerTypeFromConfiguration(itemType);
            if (managerType == (Type) null)
              managerType = ManagerBase.GetManagerTypeFromAttribute(itemType);
            Type type = ManagerBase.OnNeedsManagerType(new NeeedsManagerTypeEventArgs(itemType, managerType));
            if (type != (Type) null)
              managerType = type;
            if (managerType == (Type) null)
            {
              if (!throwException)
                return (Type) null;
              throw new NotSupportedException(string.Format(Res.Get<ErrorMessages>().CannotInferManagerTypeFromItemType, (object) itemType));
            }
            ManagerBase.managerCache.Add(itemType, managerType);
          }
        }
      }
      return managerType;
    }

    private static BlankItemDelegate GetBlankItemDelegateFromAttribute(
      Type itemType)
    {
      BlankItemDelegate delegateFromAttribute = (BlankItemDelegate) null;
      if (!(TypeDescriptor.GetAttributes(itemType)[typeof (BlankItemDelegateAttribute)] is BlankItemDelegateAttribute attribute))
      {
        Type managerType;
        ManagerBase.TryGetMappedManagerType(itemType, out managerType);
        if (managerType != (Type) null)
          attribute = TypeDescriptor.GetAttributes(managerType)[typeof (BlankItemDelegateAttribute)] as BlankItemDelegateAttribute;
      }
      if (attribute != null && attribute.ItemDelegate != null)
        delegateFromAttribute = attribute.ItemDelegate;
      return delegateFromAttribute;
    }

    public static BlankItemDelegate GetMappedBlankItemDelegate(Type itemType)
    {
      BlankItemDelegate delegateFromAttribute;
      lock (ManagerBase.managerCache)
      {
        if (!ManagerBase.blankItemDelegatesCache.TryGetValue(itemType, out delegateFromAttribute))
        {
          delegateFromAttribute = ManagerBase.GetBlankItemDelegateFromAttribute(itemType);
          if (delegateFromAttribute != null)
            ManagerBase.blankItemDelegatesCache.Add(itemType, delegateFromAttribute);
        }
      }
      return delegateFromAttribute;
    }

    /// <summary>Optimizes method lookup by caching used methods</summary>
    /// <param name="managerType">Type of the manager from which to get "GetManager(string)"</param>
    /// <returns>Cached method reflection info</returns>
    private static MethodInfo GetMethod(Type managerType)
    {
      if (managerType == (Type) null)
        throw new ArgumentNullException(nameof (managerType));
      MethodInfo method;
      if (!ManagerBase.methodCache.TryGetValue(managerType, out method))
      {
        lock (ManagerBase.methodCache)
        {
          if (!ManagerBase.methodCache.TryGetValue(managerType, out method))
          {
            string name = "GetManager";
            method = managerType.GetMethod(name, ManagerBase.getManagerSignature);
            if (method == (MethodInfo) null)
              throw new InvalidOperationException("No '{0}' method found in type {1}.".Arrange((object) name, (object) managerType.FullName));
            if (method != (MethodInfo) null && method.IsGenericMethod)
              method = method.MakeGenericMethod(managerType);
            ManagerBase.methodCache.Add(managerType, method);
          }
        }
      }
      return method;
    }

    private static MethodInfo GetManagerMethodInTransaction(Type managerType)
    {
      if (managerType == (Type) null)
        throw new ArgumentNullException(nameof (managerType));
      MethodInfo methodInTransaction;
      if (!ManagerBase.methodInTransactionCache.TryGetValue(managerType, out methodInTransaction))
      {
        lock (ManagerBase.methodInTransactionCache)
        {
          if (!ManagerBase.methodInTransactionCache.TryGetValue(managerType, out methodInTransaction))
          {
            Type baseType = managerType.BaseType;
            while (!baseType.IsGenericType || !(baseType.GetGenericTypeDefinition() == typeof (ManagerBase<>)) || !typeof (DataProviderBase).IsAssignableFrom(baseType.GetGenericArguments()[0]) || baseType == typeof (object))
              baseType = baseType.BaseType;
            if (baseType != typeof (object))
            {
              methodInTransaction = baseType.GetMethod("GetManager", ManagerBase.getManagerInTransactionSignature);
              if (methodInTransaction != (MethodInfo) null)
                methodInTransaction = methodInTransaction.MakeGenericMethod(managerType);
            }
            else
              methodInTransaction = (MethodInfo) null;
            ManagerBase.methodInTransactionCache.Add(managerType, methodInTransaction);
          }
        }
      }
      return methodInTransaction;
    }

    private static void RegisterManager(Type managerType, string providerName)
    {
      if (!(managerType.GetConstructor(new Type[1]
      {
        typeof (string)
      }) != (ConstructorInfo) null))
        throw new TargetInvocationException(string.Format("Manager type {0} is missing a constructor with 1 argument: (string dataProviderName)", (object) managerType.FullName), (Exception) null);
      ObjectFactory.Container.RegisterType(managerType, managerType, providerName.ToUpperInvariant(), (LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) providerName
      }));
    }

    internal static void EnsureManagerRegistered(
      Type managerType,
      string providerName,
      out bool justRegistered)
    {
      justRegistered = false;
      Tuple<HashSet<string>, HashSet<string>> tuple;
      if (!ManagerBase.registeredManagers.TryGetValue(managerType, out tuple))
      {
        lock (ManagerBase.registeredManagers)
        {
          if (!ManagerBase.registeredManagers.TryGetValue(managerType, out tuple))
          {
            HashSet<string> stringSet = (HashSet<string>) null;
            try
            {
              stringSet = (Activator.CreateInstance(managerType) as IManager).StaticProviders.Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)).ToHashSet<string>();
            }
            catch
            {
            }
            tuple = new Tuple<HashSet<string>, HashSet<string>>(new HashSet<string>(), stringSet);
            ManagerBase.registeredManagers.Add(managerType, tuple);
          }
        }
      }
      if (tuple.Item1.Contains(providerName))
        return;
      lock (tuple)
      {
        if (tuple.Item1.Contains(providerName))
          return;
        ManagerBase.ValidateProviderName(managerType, tuple.Item2, providerName);
        ManagerBase.RegisterManager(managerType, providerName);
        tuple.Item1.Add(providerName);
        justRegistered = true;
      }
    }

    internal static void ClearRegisteredManagersCache() => ManagerBase.registeredManagers.Clear();

    private static void ValidateProviderName(
      Type managerType,
      HashSet<string> staticProviders,
      string providerName)
    {
      if (staticProviders != null && !staticProviders.Contains(providerName) && (!typeof (IDataSource).IsAssignableFrom(managerType) || !(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext) || !multisiteContext.GetDataSourcesByManager(managerType.FullName).Any<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Provider == providerName))))
      {
        try
        {
          if ((Activator.CreateInstance(managerType) as IModuleManager).ProvidersSettings.ContainsKey(providerName))
            return;
        }
        catch
        {
        }
        throw new InvalidOperationException("Missing provider '{0}' for '{1}' manager".Arrange((object) providerName, (object) managerType));
      }
    }

    internal static IManager ResolveManager(Type managerType, string providerName)
    {
      bool justRegistered;
      ManagerBase.EnsureManagerRegistered(managerType, providerName, out justRegistered);
      string upperInvariant = providerName.ToUpperInvariant();
      IManager manager = (IManager) ObjectFactory.Resolve(managerType, upperInvariant);
      if (justRegistered)
      {
        if (SystemManager.DelayedDatabaseInit)
          SystemManager.AddDefaultDataInitializer(new Action(manager.Provider.OnInitialized));
        else if (manager.Provider != null)
          manager.Provider.OnInitialized();
      }
      return manager;
    }

    internal static NameValueCollection GetProviderParameters(
      IDataProviderSettings providerSettings)
    {
      NameValueCollection parameters = providerSettings.Parameters;
      NameValueCollection providerParameters = new NameValueCollection(parameters.Count, (IEqualityComparer) StringComparer.Ordinal);
      providerParameters["description"] = providerSettings.Description;
      providerParameters["resourceClassId"] = providerSettings.ResourceClassId;
      if (providerSettings is ITitledConfigElement titledConfigElement)
        providerParameters["title"] = titledConfigElement.Title;
      foreach (string name in (NameObjectCollectionBase) parameters)
        providerParameters[name] = parameters[name];
      return providerParameters;
    }

    /// <summary>Get manager from type and provider name</summary>
    /// <param name="managerTypeName">Full type name of manager to use</param>
    /// <param name="providerName">Name of the provider. Null or Empty will return the default.</param>
    /// <returns>Manager instance</returns>
    public static IManager GetManager(string managerTypeName, string providerName) => !string.IsNullOrEmpty(managerTypeName) ? ManagerBase.GetManager(TypeResolutionService.ResolveType(managerTypeName), providerName) : throw new ArgumentNullException(nameof (managerTypeName));

    /// <summary>Get manager from type and provider name</summary>
    /// <param name="managerType">Type of the manager to get</param>
    /// <param name="providerName">Name of the provider. Null or Empty will return the default.</param>
    /// <returns>Manager instance</returns>
    public static IManager GetManager(Type managerType, string providerName)
    {
      MethodInfo methodInfo = !(managerType == (Type) null) ? ManagerBase.GetMethod(managerType) : throw new ArgumentNullException(nameof (managerType));
      try
      {
        return methodInfo.Invoke((object) null, new object[1]
        {
          (object) providerName
        }) as IManager;
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    /// <summary>Get manager using the default provider</summary>
    /// <param name="managerType">Type of the manager to get</param>
    /// <returns>Manager instance</returns>
    public static IManager GetManager(Type managerType) => ManagerBase.GetManager(managerType, string.Empty);

    /// <summary>Get manager using the default provider</summary>
    /// <param name="managerTypeName">Full type name of the provider to use</param>
    /// <returns>Manager instance</returns>
    public static IManager GetManager(string managerTypeName) => ManagerBase.GetManager(managerTypeName, string.Empty);

    public static IManager GetManagerInTransaction(
      Type managerType,
      string providerName,
      string transactionName)
    {
      MethodInfo methodInfo = !(managerType == (Type) null) ? ManagerBase.GetManagerMethodInTransaction(managerType) : throw new ArgumentNullException(nameof (managerType));
      if (methodInfo == (MethodInfo) null)
        return (IManager) null;
      try
      {
        return methodInfo.Invoke((object) null, new object[2]
        {
          (object) providerName,
          (object) transactionName
        }) as IManager;
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    /// <summary>Gets a manager in named transaction.</summary>
    /// <param name="itemType">Type of the content item the manager serves</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      Type itemType,
      string providerName,
      string transactionName)
    {
      return ManagerBase.GetMappedManagerInTransaction(itemType, (Type) null, providerName, transactionName);
    }

    /// <summary>Gets a manager in named transaction.</summary>
    /// <param name="itemType">Type of the content item the manager serves</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>
    /// Instance or null of the manager doesn't support managed transactions
    /// </returns>
    public static IManager GetMappedManagerInTransaction(
      Type itemType,
      Type managerType,
      string providerName,
      string transactionName)
    {
      if (managerType == (Type) null)
        managerType = ManagerBase.GetMappedManagerType(itemType);
      MethodInfo methodInTransaction = ManagerBase.GetManagerMethodInTransaction(managerType);
      if (!(methodInTransaction != (MethodInfo) null))
        return (IManager) null;
      return (IManager) methodInTransaction.Invoke((object) null, new object[2]
      {
        (object) providerName,
        (object) transactionName
      });
    }

    /// <summary>Gets the mapped manager in transaction.</summary>
    /// <param name="managerInstance">The manager instance.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      IManager managerInstance,
      string providerName,
      string transactionName)
    {
      MethodInfo methodInTransaction = ManagerBase.GetManagerMethodInTransaction(managerInstance.GetType());
      if (!(methodInTransaction != (MethodInfo) null))
        return (IManager) null;
      return (IManager) methodInTransaction.Invoke((object) null, new object[2]
      {
        (object) providerName,
        (object) transactionName
      });
    }

    /// <summary>
    /// Gets a manager with the default provider in named transaction.
    /// </summary>
    /// <param name="itemType">Type of the content item the manager serves</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      Type itemType,
      string transactionName)
    {
      return ManagerBase.GetMappedManagerInTransaction(itemType, string.Empty, transactionName);
    }

    /// <summary>Gets the mapped manager in transaction.</summary>
    /// <param name="managerInstance">An instance of the manager, of the required type.</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      IManager managerInstance,
      string transactionName)
    {
      return ManagerBase.GetMappedManagerInTransaction(managerInstance, string.Empty, transactionName);
    }

    /// <summary>Gets a manager in named transaction.</summary>
    /// <param name="itemTypeName">Type name of the content item the manager serves</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      string itemTypeName,
      string providerName,
      string transactionName)
    {
      Type itemType = TypeResolutionService.ResolveType(itemTypeName, false, true);
      return itemType != (Type) null ? ManagerBase.GetMappedManagerInTransaction(itemType, providerName, transactionName) : (IManager) null;
    }

    /// <summary>
    /// Gets a manager with the default provider in named transaction.
    /// </summary>
    /// <param name="itemTypeName">Type name of the content item the manager serves</param>
    /// <param name="transactionName">Named transaction</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    public static IManager GetMappedManagerInTransaction(
      string itemTypeName,
      string transactionName)
    {
      return ManagerBase.GetMappedManagerInTransaction(itemTypeName, string.Empty, transactionName);
    }

    /// <summary>
    /// Get a manager using the specified provider name.
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemType">Type of the content item the manager serves</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <returns>Manager instance</returns>
    public static IManager GetMappedManager(Type itemType, string providerName) => !(itemType == (Type) null) ? ManagerBase.GetManager(ManagerBase.GetMappedManagerType(itemType), providerName) : throw new ArgumentNullException(nameof (itemType));

    public static bool TryGetMappedManager(
      Type itemType,
      string providerName,
      out IManager manager)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      Type managerType;
      if (ManagerBase.TryGetMappedManagerType(itemType, out managerType))
      {
        if (providerName.IsNullOrEmpty() && managerType.Equals(typeof (DynamicModuleManager)))
          providerName = DynamicModuleManager.GetDefaultProviderName(ModuleBuilderManager.GetModules().Single<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Types.Any<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.FullTypeName == itemType.FullName)))).Name);
        manager = ManagerBase.GetManager(managerType, providerName);
      }
      else
        manager = (IManager) null;
      return manager != null;
    }

    /// <summary>
    /// Get the default manager.
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemType">Type of the content item the manager serves</param>
    /// <returns>Manager instance</returns>
    public static IManager GetMappedManager(Type itemType) => ManagerBase.GetMappedManager(itemType, string.Empty);

    /// <summary>
    /// Get a manager using the specified provider name.
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemTypeName">Type of the item that has a manager mapped to it</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <returns></returns>
    public static IManager GetMappedManager(string itemTypeName, string providerName) => !string.IsNullOrEmpty(itemTypeName) ? ManagerBase.GetMappedManager(TypeResolutionService.ResolveType(itemTypeName), providerName) : throw new ArgumentNullException(nameof (itemTypeName));

    /// <summary>
    /// Get a manager using the default provider
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemTypeName">Type of the item that has a manager mapped to it</param>
    /// <returns>Manager instance</returns>
    public static IManager GetMappedManager(string itemTypeName) => ManagerBase.GetMappedManager(itemTypeName, string.Empty);

    /// <summary>
    /// Get a manager type using the default provider
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemTypeName">Type of the item that has a manager mapped to it</param>
    /// <returns>Manager instance</returns>
    public static Type GetMappedManagerType(string itemTypeName) => !string.IsNullOrEmpty(itemTypeName) ? ManagerBase.GetMappedManagerType(TypeResolutionService.ResolveType(itemTypeName)) : throw new ArgumentNullException(nameof (itemTypeName));

    /// <summary>
    /// Get a manager type using the default provider
    /// Type of the manager itself is inferred from the content item type.
    /// </summary>
    /// <param name="itemType">Type of the item that has a manager mapped to it</param>
    /// <returns>Manager instance</returns>
    public static Type GetMappedManagerType(Type itemType) => !(itemType == (Type) null) ? ManagerBase.GetMappedManagerTypeInternal(itemType) : throw new ArgumentNullException(nameof (itemType));

    public static bool TryGetMappedManagerType(Type itemType, out Type managerType)
    {
      managerType = !(itemType == (Type) null) ? ManagerBase.GetMappedManagerTypeInternal(itemType, false) : throw new ArgumentNullException(nameof (itemType));
      return managerType != (Type) null;
    }
  }
}
