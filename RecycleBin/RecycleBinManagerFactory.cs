// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinManagerFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// The entry point for acquiring instances of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" />.
  /// </summary>
  public class RecycleBinManagerFactory
  {
    /// <summary>
    /// Get the currently configured instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> using the default provider.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /></returns>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> instance failed.</exception>
    public static IRecycleBinManager GetManager() => RecycleBinManagerFactory.GetManager((string) null);

    /// <summary>
    /// Get the currently configured instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> by explicitly specifying the required provider to use.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /></returns>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> instance failed.</exception>
    public static IRecycleBinManager GetManager(string providerName) => RecycleBinManagerFactory.GetManager(providerName, (string) null);

    /// <summary>
    /// Get the currently configured instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> by explicitly specifying the required provider to use and the transaction
    /// that the provider should use.
    /// </summary>
    /// <remarks>The current registered instance at the ObjectFactory.Container is used.</remarks>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction the provider will use.</param>
    /// <returns>Instance of <see cref="!:RecycleBinManager" /></returns>
    /// <exception cref="T:Telerik.Microsoft.Practices.Unity.ResolutionFailedException">The exception thrown by the Unity container when an attempt to resolve
    /// the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinManager" /> instance failed.</exception>
    public static IRecycleBinManager GetManager(
      string providerName,
      string transactionName)
    {
      return ObjectFactory.Container.Resolve<IManagerFactory<IRecycleBinManager>>().GetManager(providerName, transactionName);
    }
  }
}
