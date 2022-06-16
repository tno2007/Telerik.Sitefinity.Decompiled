// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.DataSourceProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.DataSource
{
  internal class DataSourceProxy : IDataSource
  {
    private readonly string moduleName;
    private readonly string name;
    private readonly Type managerType;

    internal DataSourceProxy(Type managerType)
      : this((string) null, managerType)
    {
    }

    internal DataSourceProxy(string moduleName, Type managerType)
      : this(moduleName, managerType, true)
    {
    }

    internal DataSourceProxy(
      string moduleName,
      Type managerType,
      bool allowMultipleProviders,
      params string[] dependantDataSources)
    {
      if (!typeof (IDataSource).IsAssignableFrom(managerType))
        throw new ArgumentException("'managerType' should be assignable from 'Telerik.Sitefinity.Data.DataSource.IDataSource'");
      this.managerType = typeof (IManager).IsAssignableFrom(managerType) ? managerType : throw new ArgumentException("'managerType' should be assignable from 'Telerik.Sitefinity.Data.IManager'");
      this.name = managerType.FullName;
      this.moduleName = moduleName;
      this.DependantDataSources = dependantDataSources;
    }

    /// <summary>
    /// An array of dependant data source names that this data source depends on.
    /// For example Ecommerce Orders is always selected together with Ecommerce Products.
    /// </summary>
    /// <value></value>
    public string[] DependantDataSources { get; set; }

    /// <inheritdoc />
    public string ModuleName => this.moduleName;

    /// <inheritdoc />
    public string Name => this.name;

    /// <inheritdoc />
    public string Title => this.GetManager().Title;

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> ProviderInfos => this.GetManager().ProviderInfos;

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> Providers => this.GetManager().Providers;

    /// <inheritdoc />
    public bool CanCreateProvider => this.GetManager().CanCreateProvider;

    /// <inheritdoc />
    public bool AllowMultipleProviders { get; set; }

    /// <inheritdoc />
    public string CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters)
    {
      IDataSource manager = this.GetManager();
      if (!manager.CanCreateProvider)
        throw new NotImplementedException();
      manager.CreateProvider(providerName, providerTitle, parameters);
      EventHub.Raise(DataProviderEventFactory.CreateDataProviderCreatedEvent(this.managerType, providerName), false);
      return providerName;
    }

    /// <inheritdoc />
    public void DeleteProvider(string providerName)
    {
      if (!this.CanCreateProvider)
        throw new NotImplementedException();
      this.GetManager().DeleteProvider(providerName);
    }

    /// <inheritdoc />
    public void EnableProvider(string providerName) => this.GetManager().EnableProvider(providerName);

    /// <inheritdoc />
    public void DisableProvider(string providerName) => this.GetManager().DisableProvider(providerName);

    protected IDataSource GetManager() => (IDataSource) ManagerBase.GetManager(this.managerType);

    /// <inheritdoc />
    public string GetDefaultProvider() => this.GetManager().GetDefaultProvider();

    /// <inheritdoc />
    public string ProviderNameDefaultPrefix => this.GetManager().ProviderNameDefaultPrefix;
  }
}
