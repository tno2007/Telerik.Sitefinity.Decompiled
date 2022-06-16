// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.GenericProviderResolutionService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.GenericContent
{
  /// <summary>
  /// Resolves the provider that should be used for a particular data item
  /// </summary>
  public static class GenericProviderResolutionService
  {
    private static Dictionary<string, Type> Cache = new Dictionary<string, Type>();
    private static Dictionary<Type, DataProviderBase> ProviderInstances = new Dictionary<Type, DataProviderBase>();
    private static string cacheKey = typeof (GenericProviderResolutionService).FullName + "CacheKey";

    static GenericProviderResolutionService() => GenericProviderResolutionService.ResolveGenericProvider += new EventHandler<ResolveGenericProviderEventArgs>(GenericProviderResolutionService.ResolveStaticTypes);

    private static void ResolveStaticTypes(object sender, ResolveGenericProviderEventArgs e)
    {
      Type type;
      if (e.ProvidersHash.TryGetValue(e.ContentItemTypeName, out type))
        return;
      lock (e.ProvidersHash)
      {
        if (e.ProvidersHash.TryGetValue(e.ContentItemTypeName, out type))
          return;
        Type componentType;
        try
        {
          componentType = TypeResolutionService.ResolveType(e.ContentItemTypeName);
        }
        catch
        {
          return;
        }
        Type attributeType = typeof (ContentProviderAttribute);
        if (!(TypeDescriptor.GetAttributes(componentType)[attributeType] is ContentProviderAttribute attribute))
          return;
        GenericProviderResolutionService.Cache.Add(e.ContentItemTypeName, attribute.ProviderType);
      }
    }

    /// <summary>Resolve a provider by the type of the item it serves</summary>
    /// <param name="dataItemTypeName"></param>
    /// <returns></returns>
    public static DataProviderBase Resolve(string dataItemTypeName)
    {
      GenericProviderResolutionService.OnResolveGenericProvider(new ResolveGenericProviderEventArgs(dataItemTypeName, (IDictionary<string, Type>) GenericProviderResolutionService.Cache));
      Type providerType;
      if (GenericProviderResolutionService.Cache.TryGetValue(dataItemTypeName, out providerType))
        return GenericProviderResolutionService.CreateInstance(providerType);
      lock (GenericProviderResolutionService.Cache)
      {
        if (GenericProviderResolutionService.Cache.TryGetValue(dataItemTypeName, out providerType))
          return GenericProviderResolutionService.CreateInstance(providerType);
        Telerik.Sitefinity.Abstractions.ThrowHelper.LocalizedThrowGlobal<TypeLoadException>("CannotResolveGenericProvider", (object) dataItemTypeName);
        return (DataProviderBase) null;
      }
    }

    /// <summary>Creates an instance of a data provider</summary>
    /// <param name="providerType">Type of the provider to instantiate</param>
    /// <returns>Instance</returns>
    private static DataProviderBase CreateInstance(Type providerType)
    {
      DataProviderBase instance;
      if (GenericProviderResolutionService.ProviderInstances.TryGetValue(providerType, out instance))
        return instance;
      lock (GenericProviderResolutionService.ProviderInstances)
        return !GenericProviderResolutionService.ProviderInstances.TryGetValue(providerType, out instance) ? (!ObjectFactory.IsTypeRegistered(providerType) ? Activator.CreateInstance(providerType) as DataProviderBase : ObjectFactory.Resolve(providerType) as DataProviderBase) : instance;
    }

    /// <summary>
    /// Register a content item type and a provider that serves it
    /// </summary>
    /// <param name="itemTypeName">String identifying the item</param>
    /// <param name="providerType">Type of the provider that serves the content item</param>
    /// <exception cref="T:System.ArgumentException">When <paramref name="itemTypeName" /> is null/empty,
    /// or if <paramref name="providerType" /> is null.</exception>
    public static void RegisterProvider(string itemTypeName, Type providerType)
    {
      if (string.IsNullOrEmpty(itemTypeName) || providerType == (Type) null)
        Telerik.Sitefinity.Abstractions.ThrowHelper.ThrowGlobal<ArgumentException>();
      else if (TypeResolutionService.ResolveType(itemTypeName, false) == (Type) null)
      {
        Telerik.Sitefinity.Abstractions.ThrowHelper.LocalizedThrowUnhandled<ArgumentException>("CannotResolveType", (object) itemTypeName);
      }
      else
      {
        if (GenericProviderResolutionService.Cache.ContainsKey(itemTypeName))
          return;
        lock (GenericProviderResolutionService.Cache)
        {
          if (GenericProviderResolutionService.Cache.ContainsKey(itemTypeName) || !typeof (DataProviderBase).IsAssignableFrom(providerType))
            return;
          GenericProviderResolutionService.Cache.Add(itemTypeName, providerType);
        }
      }
    }

    /// <summary>Hook to the event if you want to resolve</summary>
    public static event EventHandler<ResolveGenericProviderEventArgs> ResolveGenericProvider;

    /// <summary>Used in unit-testing to clear the cache</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void ClearCache()
    {
      lock (GenericProviderResolutionService.ProviderInstances)
        GenericProviderResolutionService.ProviderInstances.Clear();
      lock (GenericProviderResolutionService.Cache)
        GenericProviderResolutionService.Cache.Clear();
    }

    private static void OnResolveGenericProvider(ResolveGenericProviderEventArgs args)
    {
      if (GenericProviderResolutionService.ResolveGenericProvider == null)
        return;
      GenericProviderResolutionService.ResolveGenericProvider((object) typeof (GenericProviderResolutionService), args);
    }
  }
}
