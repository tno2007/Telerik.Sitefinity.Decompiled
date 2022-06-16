// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncMap.SyncMapDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.SyncMap.Model;

namespace Telerik.Sitefinity.SyncMap
{
  /// <summary>
  /// Represents a base class for Site Settings data providers.
  /// </summary>
  public abstract class SyncMapDataProvider : DataProviderBase
  {
    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance.
    /// </summary>
    /// <param name="moduleName">Name of the external module to syncronize with.</param>
    /// <param name="appName">Name of the app from the external module.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal abstract SyncApp CreateApp(string moduleName, string appName);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> with the given id.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal abstract SyncApp CreateApp(Guid appId, string moduleName, string appName);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> for removal.
    /// </summary>
    /// <param name="app">The app to delete.</param>
    internal abstract void Delete(SyncApp app);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> object with the given id.
    /// </summary>
    /// <param name="appId">The setting id.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal abstract SyncApp GetApp(Guid appId);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> object with the module and application.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="appName">Name of the app.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> instance</returns>
    internal abstract SyncApp GetApp(string moduleName, string appName);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncApp" /> objects.
    /// </returns>
    internal abstract IQueryable<SyncApp> GetApps();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <param name="externalKey">The external key.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>
    /// Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance
    /// </returns>
    internal abstract SyncMapping CreateMapping(
      Guid appId,
      string externalKey,
      Guid itemId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> for removal.
    /// </summary>
    /// <param name="app">The mapping to delete.</param>
    internal abstract void Delete(SyncMapping mapping);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" />.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <param name="externalKey">The external key.</param>
    /// <param name="itemId">The setting id.</param>
    /// <returns>
    /// Returns <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> instance
    /// </returns>
    internal abstract SyncMapping GetMapping(
      Guid appId,
      string externalKey,
      Guid itemId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> objects for the application with the specified appId.
    /// </summary>
    /// <param name="appId">The app id.</param>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.SyncMap.Model.SyncMapping" /> objects.
    /// </returns>
    internal abstract IQueryable<SyncMapping> GetMappings(Guid appId);

    /// <summary>Get a list of types served by this manager</summary>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (SyncApp),
      typeof (SyncMapping)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => nameof (SyncMapDataProvider);
  }
}
