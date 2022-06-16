// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.IManagerFactory`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Defines the common methods to acquire <see cref="T:Telerik.Sitefinity.Data.IManager" /> instances.
  /// </summary>
  /// <typeparam name="TManager">The type of the manager to acquire</typeparam>
  public interface IManagerFactory<TManager> where TManager : IManager
  {
    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> using the default provider.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <exception cref="!:ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    TManager GetManager();

    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> by explicitly specifying the required provider to use.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    /// <exception cref="!:ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    TManager GetManager(string providerName);

    /// <summary>
    /// Get the currently configured instance of <see cref="!:TManager" /> by explicitly specifying the required provider to use and the transaction
    /// that the provider should use.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction the provider will use.</param>
    /// <returns>Instance of <see cref="!:TManager" /></returns>
    /// <exception cref="!:ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="!:TManager" /> instance failed.</exception>
    TManager GetManager(string providerName, string transactionName);
  }
}
