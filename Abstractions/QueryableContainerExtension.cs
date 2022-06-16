// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.QueryableContainerExtension
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Container extension to extension that allows to test if a type is registered with container.
  /// </summary>
  public class QueryableContainerExtension : UnityContainerExtension
  {
    private const int WaitTimeout = 5000;
    private List<RegisterEventArgs> registrations = new List<RegisterEventArgs>();
    private ReaderWriterLockWrapper readWriteLock = new ReaderWriterLockWrapper();
    private ConcurrentDictionary<string, RegisterEventArgs> argsByNameCache = new ConcurrentDictionary<string, RegisterEventArgs>();

    /// <summary>Gets all registered types.</summary>
    public List<RegisterEventArgs> Registrations => this.registrations;

    /// <summary>
    /// Initial the container with this extension's functionality.
    /// </summary>
    protected override void Initialize() => this.Context.Registering += new EventHandler<RegisterEventArgs>(this.Context_Registering);

    private void Context_Registering(object sender, RegisterEventArgs e) => this.AddRegistration(e);

    private void AddRegistration(RegisterEventArgs registration)
    {
      if (registration == null)
        return;
      this.readWriteLock.EnterForWrite((Action) (() => this.AddRegistrationToListUnsafe(registration)));
    }

    private void AddRegistrationToListUnsafe(RegisterEventArgs registration)
    {
      RegisterEventArgs registerEventArgs = this.Registrations.FirstOrDefault<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (a => a.TypeFrom == registration.TypeFrom && a.TypeTo == registration.TypeTo && a.Name == registration.Name));
      if (registerEventArgs != null)
        this.Registrations.Remove(registerEventArgs);
      this.Registrations.Add(registration);
      this.argsByNameCache.Clear();
    }

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered<T>() => this.IsTypeRegistered(typeof (T), (Type) null, (string) null, false);

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="T"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <param name="namedInstance">The name of the instance.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered<T>(string namedInstance) => this.IsTypeRegistered(typeof (T), (Type) null, namedInstance, false);

    /// <summary>
    /// Test if a type is registered as singleton with container.
    /// </summary>
    /// <typeparam name="TFrom"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <typeparam name="TTo"><see cref="T:System.Type" /> that will actually be returned.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered<TFrom, TTo>() => this.IsTypeRegistered(typeof (TFrom), typeof (TTo), (string) null, false);

    /// <summary>Test if a type is registered with container.</summary>
    /// <typeparam name="TFrom"><see cref="T:System.Type" /> that will be requested.</typeparam>
    /// <typeparam name="TTo"><see cref="T:System.Type" /> that will actually be returned.</typeparam>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegisteredAsSingleton<TFrom, TTo>() => this.IsTypeRegistered(typeof (TFrom), typeof (TTo), (string) null, true);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="type"><see cref="T:System.Type" /> that will be requested.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered(Type type) => this.IsTypeRegistered(type, (Type) null, (string) null, false);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="from"><see cref="T:System.Type" /> that will be requested.</param>
    /// <param name="to"><see cref="T:System.Type" /> that will actually be returned.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered(Type from, Type to) => this.IsTypeRegistered(from, to, (string) null, false);

    /// <summary>Test if a type is registered with container.</summary>
    /// <param name="from"><see cref="T:System.Type" /> that will be requested.</param>
    /// <param name="to"><see cref="T:System.Type" /> that will actually be returned.</param>
    /// <param name="namedInstance">The name of the instance.</param>
    /// <param name="isSingleton">If true the type is tested if it is registered as singleton.</param>
    /// <returns>True if specified type is registered otherwise false.</returns>
    public bool IsTypeRegistered(Type from, Type to, string namedInstance, bool isSingleton)
    {
      if (from == (Type) null)
        return false;
      bool isTypeRegistered = false;
      this.readWriteLock.EnterForRead((Action) (() =>
      {
        IEnumerable<RegisterEventArgs> source = this.Registrations.Where<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => e.TypeFrom == from));
        if (to != (Type) null)
          source = source.Where<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => e.TypeTo == to));
        if (!string.IsNullOrEmpty(namedInstance))
          source = source.Where<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => e.Name == namedInstance));
        if (isSingleton)
          source = source.Where<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => typeof (ContainerControlledLifetimeManager).IsInstanceOfType((object) e.LifetimeManager)));
        isTypeRegistered = source.Any<RegisterEventArgs>();
      }));
      return isTypeRegistered;
    }

    /// <summary>
    /// Gets an array of <see cref="T:Telerik.Microsoft.Practices.Unity.RegisterEventArgs" /> for registered types
    /// that inherit form the provided type.
    /// </summary>
    /// <param name="baseType">The base type to search for.</param>
    /// <returns><see cref="T:System.Collections.Generic.IEnumerable`1" /></returns>
    public IEnumerable<RegisterEventArgs> GetArgsForType(Type baseType)
    {
      IEnumerable<RegisterEventArgs> args = (IEnumerable<RegisterEventArgs>) null;
      this.readWriteLock.EnterForRead((Action) (() => args = (IEnumerable<RegisterEventArgs>) this.Registrations.Where<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => baseType.IsAssignableFrom(e.TypeTo))).OrderBy<RegisterEventArgs, string>((Func<RegisterEventArgs, string>) (a => a.Name)).ToList<RegisterEventArgs>()));
      return args;
    }

    /// <summary>
    /// Gets the arguments for the first registered type that matches the parameters.
    /// </summary>
    /// <param name="name">The name by which the type was registered.</param>
    /// <param name="baseType">A base type that the named type must inherit.</param>
    /// <returns><see cref="T:Telerik.Microsoft.Practices.Unity.RegisterEventArgs" /></returns>
    public RegisterEventArgs GetArgsByName(string name, Type baseType)
    {
      if (string.IsNullOrEmpty(name))
        return (RegisterEventArgs) null;
      string key = this.GetArgsByNameCacheKey(name, baseType);
      RegisterEventArgs argsByName;
      if (this.argsByNameCache.ContainsKey(key) && this.argsByNameCache.TryGetValue(key, out argsByName))
        return argsByName;
      RegisterEventArgs args = (RegisterEventArgs) null;
      this.readWriteLock.EnterForRead((Action) (() =>
      {
        args = !(baseType == (Type) null) ? this.Registrations.FirstOrDefault<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => name.Equals(e.Name, StringComparison.OrdinalIgnoreCase) && baseType.IsAssignableFrom(e.TypeTo))) : this.Registrations.FirstOrDefault<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        this.argsByNameCache.TryAdd(key, args);
      }));
      return args;
    }

    /// <summary>Gets the type for the first match of the parameters.</summary>
    /// <param name="name">The name by which the type was registered.</param>
    /// <param name="baseType">A base type that the named type must inherit.</param>
    /// <returns>The type if found else null.</returns>
    public Type GetNamedType(string name, Type baseType)
    {
      RegisterEventArgs argsByName = this.GetArgsByName(name, baseType);
      if (argsByName == null)
        return (Type) null;
      return argsByName.TypeTo != (Type) null ? argsByName.TypeTo : argsByName.TypeFrom;
    }

    private string GetArgsByNameCacheKey(string name, Type baseType) => name + baseType.AssemblyQualifiedName;
  }
}
