// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IStatusProviderRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Status provider registry object</summary>
  internal interface IStatusProviderRegistry
  {
    /// <summary>Get provider</summary>
    /// <param name="providerName">The providerName</param>
    /// <returns>return provider</returns>
    IStatusProvider GetProvider(string providerName);

    /// <summary>Get all providers</summary>
    /// <returns>List of providers</returns>
    IEnumerable<IStatusProvider> GetProviders();

    /// <summary>Registers a provider</summary>
    /// <typeparam name="T">Type object</typeparam>
    /// <param name="provider">The provider</param>
    void RegisterProvider<T>(IStatusProvider provider);

    /// <summary>Unregister all registered providers for statuses</summary>
    void Clear();
  }
}
