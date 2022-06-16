// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusProviderRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Status provider registry class</summary>
  internal class StatusProviderRegistry : IStatusProviderRegistry
  {
    private Dictionary<string, IStatusProvider> providers;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.StatusProviderRegistry" /> class.
    /// </summary>
    public StatusProviderRegistry() => this.providers = new Dictionary<string, IStatusProvider>();

    /// <summary>Get status provider by name</summary>
    /// <param name="providerName">The provider name</param>
    /// <returns>the provider</returns>
    public IStatusProvider GetProvider(string providerName) => this.providers[providerName];

    /// <summary>Get status providers</summary>
    /// <returns>All providers</returns>
    public IEnumerable<IStatusProvider> GetProviders() => (IEnumerable<IStatusProvider>) this.providers.Values;

    /// <summary>Register provider</summary>
    /// <typeparam name="T">Provider type T</typeparam>
    /// <param name="provider">Object provider</param>
    public void RegisterProvider<T>(IStatusProvider provider) => this.RegisterProvider(provider);

    /// <summary>Register provider</summary>
    /// <param name="provider">The provider</param>
    public void RegisterProvider(IStatusProvider provider) => this.providers.Add(provider.Name, provider);

    /// <summary>Unregister all registered providers for statuses</summary>
    public void Clear() => this.providers.Clear();
  }
}
