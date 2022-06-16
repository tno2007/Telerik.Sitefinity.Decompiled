// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ManagerFactory`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Acquires instances of <see cref="!:TManager" />.
  /// </summary>
  /// <typeparam name="TManager">The type of the manager to acquire.</typeparam>
  public class ManagerFactory<TManager> : IManagerFactory<TManager> where TManager : IManager
  {
    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> using the default provider.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    public TManager GetManager() => this.GetManager((string) null, (string) null);

    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> by explicitly specifying the required provider to use.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    public TManager GetManager(string providerName) => this.GetManager(providerName, (string) null);

    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> by explicitly specifying the required provider to use and the transaction
    /// that the provider should use.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction the provider will use.</param>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    public TManager GetManager(string providerName, string transactionName)
    {
      List<ParameterOverride> parameterOverrideList = new List<ParameterOverride>(2);
      if (providerName != null)
        parameterOverrideList.Add(new ParameterOverride(nameof (providerName), (object) providerName));
      if (transactionName != null)
        parameterOverrideList.Add(new ParameterOverride(nameof (transactionName), (object) transactionName));
      return ObjectFactory.Container.Resolve<TManager>((ResolverOverride[]) parameterOverrideList.ToArray());
    }
  }
}
