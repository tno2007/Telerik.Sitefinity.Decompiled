// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.IDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Data.DataSource
{
  /// <summary>
  /// Base interface for data sources that work with multiple providers (e.g. managers)
  /// </summary>
  internal interface IDataSource
  {
    /// <summary>Gets the name used to refer to the data source</summary>
    string Name { get; }

    /// <summary>
    /// Gets the name of the module the data source belongs to.
    /// </summary>
    /// <value>The name of the module.</value>
    string ModuleName { get; }

    /// <summary>
    /// Gets a brief title suitable for display in user interfaces (UIs).
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Data.DataSource.DataProviderInfo" /> that are enabled and registered for use by the <see cref="T:Telerik.Sitefinity.Data.DataSource.IDataSource" /> object.
    /// </summary>
    IEnumerable<DataProviderInfo> ProviderInfos { get; }

    /// <summary>
    /// Gets all configured providers for the current data source (including the disabled ones).
    /// </summary>
    /// <value>All providers.</value>
    IEnumerable<DataProviderInfo> Providers { get; }

    /// <summary>Gets whether a data source can create a provider</summary>
    bool CanCreateProvider { get; }

    /// <summary>
    /// An array of dependant data source names that this data source depends on.
    /// For example Ecommerce Orders may depend on Ecommerce Products.
    /// </summary>
    string[] DependantDataSources { get; }

    /// <summary>
    /// Creates a new provider and adds it to the ProviderInfos collection
    /// </summary>
    /// <param name="providerName">The unique name of the provider by which it will be identified.</param>
    /// <param name="providerTitle">The title of the provider</param>
    /// <param name="parameters">Custom parameters specific to given provider</param>
    /// <returns>The name of the provider</returns>
    string CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters);

    /// <summary>
    /// Deletes a provider and removes it from the ProviderInfos collection
    /// </summary>
    /// <param name="providerName">The name of the provider</param>
    void DeleteProvider(string providerName);

    /// <summary>Enables the provider in the configuration.</summary>
    void EnableProvider(string providerName);

    /// <summary>Disables the provider in the configuration.</summary>
    void DisableProvider(string providerName);

    /// <summary>Gets the default provider</summary>
    string GetDefaultProvider();

    /// <summary>
    /// Gets default prefix that would be used when creating a new provider.
    /// </summary>
    /// <returns></returns>
    string ProviderNameDefaultPrefix { get; }
  }
}
