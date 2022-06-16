// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.CacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.SPI.dataobjects;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Represents a helper class for working with cache dependencies.
  /// </summary>
  public static class CacheDependency
  {
    private static IList<ICacheDependencyHandler> handlers;
    private static object syncLock = new object();
    /// <summary>
    /// Types for which the cache dependency will fire (except ICacheItem and IDataItem)
    /// </summary>
    private static Type[] cacheDependencyKnownTypes = new Type[17]
    {
      typeof (Permission),
      typeof (Content),
      typeof (PageData),
      typeof (DraftData),
      typeof (ConfigSection),
      typeof (ControlPresentation),
      typeof (PageNode),
      typeof (PageData),
      typeof (PageTemplate),
      typeof (SecurityRoot),
      typeof (DynamicContent),
      typeof (XmlConfigItem),
      typeof (WorkflowDefinition),
      typeof (DynamicModule),
      typeof (DynamicModuleType),
      typeof (DynamicModuleField),
      typeof (Addon)
    };

    /// <summary>
    /// Subscribes for notification when the provided item is changed.
    /// This overload subscribes to all handlers.
    /// The callback delegate maybe invoked multiple times for the same item.
    /// </summary>
    /// <param name="trackedItem">The tracked item.</param>
    /// <param name="callback">The callback delegate.</param>
    public static void Subscribe(object trackedItem, ChangedCallback callback)
    {
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
        CacheDependency.Handlers[index].Subscribe(trackedItem, callback);
    }

    /// <summary>
    /// Subscribes for notification when the provided item is changed.
    /// This overload subscribes only to handlers that inherit from the specified type.
    /// </summary>
    /// <param name="handlerType">The type of the handler to subscribe to.</param>
    /// <param name="trackedItem">The tracked item.</param>
    /// <param name="callback">The callback delegate.</param>
    public static void Subscribe(Type handlerType, object trackedItem, ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.FullName == handler.GetType().FullName)
          handler.Subscribe(trackedItem, callback);
      }
    }

    /// <summary>
    /// Subscribes for notification when an item of the provided type is changed.
    /// This overload subscribes to all handlers.
    /// The callback delegate maybe invoked multiple times for the same item
    /// </summary>
    /// <param name="trackedType">Type to track.</param>
    /// <param name="callback">The callback.</param>
    public static void Subscribe(Type trackedType, ChangedCallback callback)
    {
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
        CacheDependency.Handlers[index].Subscribe(trackedType, callback);
    }

    /// <summary>
    /// Subscribes for notification when an item of the provided type is changed.
    /// This overload subscribes only to handlers that inherit from the specified type.
    /// </summary>
    /// <param name="handlerType">The type of the handler to subscribe to.</param>
    /// <param name="trackedType">Type to track.</param>
    /// <param name="callback">The callback.</param>
    public static void Subscribe(Type handlerType, Type trackedType, ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.FullName == handler.GetType().FullName)
          handler.Subscribe(trackedType, callback);
      }
    }

    /// <summary>
    /// Subscribes for notification when an item with the provided key and type is changed.
    /// </summary>
    /// <param name="trackedType">The type of the tracked item.</param>
    /// <param name="key">The primary key of the tracked item.</param>
    /// <param name="callback">The callback delegate.</param>
    public static void Subscribe(Type trackedType, string key, ChangedCallback callback)
    {
      if (trackedType == (Type) null)
        throw new ArgumentNullException(nameof (trackedType));
      foreach (ICacheDependencyHandler handler in (IEnumerable<ICacheDependencyHandler>) CacheDependency.Handlers)
        handler.Subscribe(trackedType, key, callback);
    }

    /// <summary>
    /// Subscribes for notification when an item with the provided key and type is changed.
    /// This overload subscribes only to handlers that has specified key inherit from the specified type.
    /// </summary>
    /// <param name="handlerType">The type of the handler to subscribe to.</param>
    /// <param name="trackedType">Type to track.</param>
    /// <param name="key">The key of item to track</param>
    /// <param name="callback">The callback.</param>
    public static void Subscribe(
      Type handlerType,
      Type trackedType,
      string key,
      ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.FullName == handler.GetType().FullName)
          handler.Subscribe(trackedType, key, callback);
      }
    }

    /// <summary>
    /// Unsubscribe the specified tracked item from all handlers.
    /// </summary>
    /// <param name="trackedItem">The tracked item.</param>
    /// <param name="callback">The callback.</param>
    public static void Unsubscribe(object trackedItem, ChangedCallback callback)
    {
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
        CacheDependency.Handlers[index].Unsubscribe(trackedItem, callback);
    }

    /// <summary>
    /// Unsubscribe the specified tracked item from handlers that inherit form the specified type.
    /// </summary>
    /// <param name="handlerType">Type of the handler.</param>
    /// <param name="trackedItem">The tracked item.</param>
    /// <param name="callback">The callback.</param>
    public static void Unsubscribe(Type handlerType, object trackedItem, ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.IsAssignableFrom(handler.GetType()))
          handler.Unsubscribe(trackedItem, callback);
      }
    }

    /// <summary>
    /// Unsubscribe the specified tracked type from all handlers.
    /// </summary>
    /// <param name="trackedType">The tracked type.</param>
    /// <param name="callback">The callback.</param>
    public static void Unsubscribe(Type trackedType, ChangedCallback callback)
    {
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
        CacheDependency.Handlers[index].Unsubscribe(trackedType, callback);
    }

    /// <summary>
    /// Unsubscribe the specified tracked type from handlers that inherit form the specified type.
    /// </summary>
    /// <param name="handlerType">Type of the handler.</param>
    /// <param name="trackedType">The tracked type.</param>
    /// <param name="callback">The callback.</param>
    public static void Unsubscribe(Type handlerType, Type trackedType, ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.IsAssignableFrom(handler.GetType()))
          handler.Unsubscribe(trackedType, callback);
      }
    }

    /// <summary>
    /// Unsubscribe the specified tracked type from handlers that inherit form the specified type.
    /// </summary>
    /// <param name="handlerType">Type of the handler.</param>
    /// <param name="trackedType">The tracked type.</param>
    /// <param name="key">The key of the cached item.</param>
    /// <param name="callback">The callback.</param>
    public static void Unsubscribe(
      Type handlerType,
      Type trackedType,
      string key,
      ChangedCallback callback)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      for (int index = 0; index < CacheDependency.Handlers.Count; ++index)
      {
        ICacheDependencyHandler handler = CacheDependency.Handlers[index];
        if (handlerType.IsAssignableFrom(handler.GetType()))
          handler.Unsubscribe(trackedType, key, callback);
      }
    }

    internal static IEnumerable<CacheDependencyKey> NotifyInternal(
      object[] items)
    {
      List<CacheDependencyKey> list = ((IEnumerable<object>) items).Select<object, CacheDependencyKey>((Func<object, CacheDependencyKey>) (item =>
      {
        Type type = item as Type;
        if (!(type != (Type) null))
          return new CacheDependencyKey()
          {
            Type = item.GetType(),
            Key = CacheDependency.GetItemKey(item)
          };
        return new CacheDependencyKey() { Type = type };
      })).ToList<CacheDependencyKey>();
      CacheDependency.NotifyInternal((IEnumerable<CacheDependencyKey>) list);
      return (IEnumerable<CacheDependencyKey>) list;
    }

    internal static void NotifyInternal(IEnumerable<CacheDependencyKey> items)
    {
      foreach (ICacheDependencyHandler handler in (IEnumerable<ICacheDependencyHandler>) CacheDependency.Handlers)
      {
        foreach (CacheDependencyKey key in items)
          handler.Notify(key);
      }
    }

    /// <summary>Notifies the specified items.</summary>
    /// <param name="handlers"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    [Obsolete("Use Notify method that accepts a list of CacheDependencyKey objects")]
    public static IList<CacheDependencyKey> NotifyInternal(
      IEnumerable<ICacheDependencyHandler> handlers,
      object[] items)
    {
      List<CacheDependencyKey> cacheDependencyKeyList = new List<CacheDependencyKey>();
      foreach (ICacheDependencyHandler handler in handlers)
      {
        foreach (object itemType in items)
        {
          if ((object) (itemType as Type) != null)
            cacheDependencyKeyList.AddRange((IEnumerable<CacheDependencyKey>) handler.Notify((Type) itemType));
          else
            cacheDependencyKeyList.AddRange((IEnumerable<CacheDependencyKey>) handler.Notify(itemType));
        }
      }
      return (IList<CacheDependencyKey>) cacheDependencyKeyList;
    }

    [Obsolete("Use Notify method that accepts a list of CacheDependencyKey objects")]
    public static IList<CacheDependencyKey> NotifyInternal(
      IEnumerable<ICacheDependencyHandler> handlers,
      IList<CacheDependencyKey> items)
    {
      List<CacheDependencyKey> cacheDependencyKeyList = new List<CacheDependencyKey>();
      foreach (ICacheDependencyHandler handler in handlers)
      {
        foreach (CacheDependencyKey key in (IEnumerable<CacheDependencyKey>) items)
          cacheDependencyKeyList.AddRange((IEnumerable<CacheDependencyKey>) handler.Notify(key));
      }
      return (IList<CacheDependencyKey>) cacheDependencyKeyList;
    }

    [Obsolete("Use NotifyAll method")]
    public static IList<CacheDependencyKey> NotifyAllInternal(
      IEnumerable<ICacheDependencyHandler> handlers)
    {
      List<CacheDependencyKey> cacheDependencyKeyList = new List<CacheDependencyKey>();
      foreach (ICacheDependencyHandler handler in handlers)
        cacheDependencyKeyList.AddRange((IEnumerable<CacheDependencyKey>) handler.NotifyAll());
      return (IList<CacheDependencyKey>) cacheDependencyKeyList;
    }

    public static void Notify(object[] items) => CacheDependency.Notify(items, (string) null);

    public static void Notify(object[] items, string connection)
    {
      IEnumerable<CacheDependencyKey> cacheDependencyKeys = CacheDependency.NotifyInternal(items);
      if (!cacheDependencyKeys.Any<CacheDependencyKey>())
        return;
      SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) new InvalidateCacheMessage(cacheDependencyKeys, connection));
    }

    public static void Notify(IList<CacheDependencyKey> items) => CacheDependency.Notify(items, (string) null);

    public static void Notify(IList<CacheDependencyKey> items, string connection)
    {
      CacheDependency.NotifyInternal((IEnumerable<CacheDependencyKey>) items);
      if (items != null && items.Count > 0)
        SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) new InvalidateCacheMessage((IEnumerable<CacheDependencyKey>) items, connection));
      CacheDependency.InvokeCacheDependencyNotify((IEnumerable<CacheDependencyKey>) items);
    }

    public static void NotifyWithoutSendingSystemMessage(IList<CacheDependencyKey> items) => CacheDependency.NotifyInternal((IEnumerable<CacheDependencyKey>) items);

    internal static void NotifyAndRaiseEvent(IList<CacheDependencyKey> items)
    {
      CacheDependency.NotifyInternal((IEnumerable<CacheDependencyKey>) items);
      CacheDependency.InvokeCacheDependencyNotify((IEnumerable<CacheDependencyKey>) items);
    }

    /// TODO-CD: Obsolete
    ///             <summary>
    /// Notifies all cached items to invalidate regardless if they are changed or not.
    /// </summary>
    public static void NotifyAll()
    {
      IList<CacheDependencyKey> items = CacheDependency.NotifyAllInternal((IEnumerable<ICacheDependencyHandler>) CacheDependency.Handlers);
      if (items.Count <= 0)
        return;
      SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) new InvalidateCacheMessage((IEnumerable<CacheDependencyKey>) items));
    }

    /// <summary>
    /// Notifies all cached items to invalidate regardless if they are changed or not
    /// that are subscribed to the specified handler.
    /// </summary>
    /// <param name="handlerType">The type of the handler.</param>
    public static void NotifyAll(Type handlerType)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      CacheDependency.NotifyAllInternal(CacheDependency.Handlers.Where<ICacheDependencyHandler>((Func<ICacheDependencyHandler, bool>) (h => handlerType.IsAssignableFrom(h.GetType()))));
    }

    private static void InvokeCacheDependencyNotify(IEnumerable<CacheDependencyKey> items)
    {
      if (!Bootstrapper.IsReady || CacheDependency.CacheDependencyNotify == null)
        return;
      CacheDependency.CacheDependencyNotify(items);
    }

    /// <summary>Gets the item key.</summary>
    /// <param name="item">The item.</param>
    /// <returns>The the item key.</returns>
    public static string GetItemKey(object item)
    {
      string key;
      CacheDependency.TryGetWellKnownItemKey(item, out key);
      return key;
    }

    /// <summary>
    /// Resolves the key from the specified item if the item is assignable from one of the
    /// dependencies well known types or implements ICacheITem interface
    /// </summary>
    /// <param name="item">item to get the key from</param>
    /// <param name="key">key</param>
    /// <returns>true of the key was resolved</returns>
    public static bool TryGetWellKnownItemKey(object item, out string key)
    {
      switch (item)
      {
        case null:
          throw new ArgumentNullException(nameof (item));
        case Guid _:
          key = item.ToString();
          return true;
        case ICacheItem cacheItem:
          key = cacheItem.GetKey();
          return true;
        default:
          Type itemType = item.GetType();
          if (((IEnumerable<Type>) CacheDependency.cacheDependencyKnownTypes).Any<Type>((Func<Type, bool>) (cacheDependecyKnowType => cacheDependecyKnowType.IsAssignableFrom(itemType))))
          {
            switch (item)
            {
              case IDataItem dataItem:
                key = dataItem.Id.ToString();
                return true;
              case PersistenceCapable _:
                IObjectContext context = Database.GetContext(item);
                if (context == null || context.IsNew(item) && context.IsRemoved(item))
                {
                  key = (string) null;
                  break;
                }
                IObjectId objectId = context.GetObjectId(item);
                key = objectId.ToString();
                return true;
              default:
                key = (string) null;
                break;
            }
          }
          else
            key = (string) null;
          return false;
      }
    }

    /// <summary>
    /// Occurs when on CacheDependency.Notify only on the node the calling method.
    /// </summary>
    internal static event CacheDependency.NotifyCallback CacheDependencyNotify;

    private static IList<ICacheDependencyHandler> Handlers
    {
      get
      {
        if (CacheDependency.handlers == null)
        {
          lock (CacheDependency.syncLock)
          {
            if (CacheDependency.handlers == null)
            {
              CacheDependency.handlers = (IList<ICacheDependencyHandler>) new List<ICacheDependencyHandler>();
              foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) Config.Get<SystemConfig>().CacheDependencyHandlers.Values)
              {
                ICacheDependencyHandler instance = (ICacheDependencyHandler) Activator.CreateInstance(providerSettings.ProviderType);
                instance.Initialize(providerSettings.Parameters);
                CacheDependency.Handlers.Add(instance);
              }
            }
          }
        }
        return CacheDependency.handlers;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    internal delegate void NotifyCallback(IEnumerable<CacheDependencyKey> items);
  }
}
