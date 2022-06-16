// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceBus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services
{
  /// <summary>A factory for registering and resolving services</summary>
  public static class ServiceBus
  {
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    /// <summary>Registers the service.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service">The service.</param>
    public static void RegisterService<T>(object service) => ServiceBus.RegisterService(typeof (T), service);

    /// <summary>Registers the service.</summary>
    /// <param name="type">The type.</param>
    /// <param name="service">The service.</param>
    public static void RegisterService(Type type, object service)
    {
      lock (ServiceBus.services)
      {
        if (!type.IsAssignableFrom(service.GetType()))
          throw new InvalidOperationException("A service of type '{0}' attempts to register with a service type {1} which is not implemented by the actual service.".Arrange((object) service.GetType(), (object) type.FullName));
        if (ServiceBus.services.ContainsKey(type))
          throw new InvalidOperationException("A service of type '{0}' is already registerd. Only one registration is allowed.".Arrange((object) type.FullName));
        ServiceBus.services.Add(type, service);
      }
    }

    /// <summary>Unregisters the service.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service">The service.</param>
    public static void UnregisterService<T>() => ServiceBus.UnregisterService(typeof (T));

    /// <summary>Unregisters the service.</summary>
    /// <param name="type">The type.</param>
    /// <param name="service">The service.</param>
    public static void UnregisterService(Type type)
    {
      lock (ServiceBus.services)
        ServiceBus.services.Remove(type);
    }

    /// <summary>
    /// Resolves the service of the specified type, or returns null if such service is not registered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ResolveService<T>() => (T) ServiceBus.ResolveService(typeof (T));

    /// <summary>
    /// Resolves the service of the specified type, or returns null if such service is not registered.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static object ResolveService(Type type)
    {
      object obj;
      if ((ServiceBus.services.TryGetValue(type, out obj) || ServiceBus.TryResolveFromObjectFactory(type, out obj)) && obj is IService service && service.Status != ServiceStatus.Started)
        service.Start();
      return obj;
    }

    private static bool TryResolveFromObjectFactory(Type type, out object obj)
    {
      if (ObjectFactory.IsTypeRegistered(type))
      {
        obj = ObjectFactory.Resolve(type);
        return true;
      }
      obj = (object) null;
      return false;
    }

    internal static void Clear()
    {
      lock (ServiceBus.services)
        ServiceBus.services.Clear();
    }
  }
}
