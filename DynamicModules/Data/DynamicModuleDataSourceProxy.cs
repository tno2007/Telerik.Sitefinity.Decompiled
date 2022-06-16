// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Data.DynamicModuleDataSourceProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.DynamicModules.Data
{
  internal class DynamicModuleDataSourceProxy : IMultisiteDataSource, IDataSource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Data.DynamicModuleDataSourceProxy" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="title">The title.</param>
    /// <param name="canCreateProvider">if set to <c>true</c> [can create provider].</param>
    /// <param name="allowMultipleProviders">if set to <c>true</c> [allows selection of multiple providers in Multisite].</param>
    /// <param name="providerInfos">The provider info.</param>
    internal DynamicModuleDataSourceProxy(
      string name,
      string title,
      bool canCreateProvider,
      bool allowMultipleProviders,
      IEnumerable<DataProviderInfo> providerInfos)
    {
      this.Name = name;
      this.Title = title;
      this.CanCreateProvider = canCreateProvider;
      this.AllowMultipleProviders = allowMultipleProviders;
      this.ProviderInfos = providerInfos;
    }

    /// <inheritdoc />
    public string ModuleName => this.Name;

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public string Title { get; private set; }

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> ProviderInfos { get; private set; }

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> Providers => DynamicModuleManager.GetManager().AllProviders;

    /// <inheritdoc />
    public bool CanCreateProvider { get; private set; }

    /// <inheritdoc />
    public bool AllowMultipleProviders { get; set; }

    /// <inheritdoc />
    public string[] DependantDataSources { get; set; }

    /// <inheritdoc />
    public void DeleteProvider(string providerName)
    {
      ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager();
      manager1.DeleteDynamicContentProviders(providerName);
      manager1.SaveChanges();
      DynamicModuleManager manager2 = DynamicModuleManager.GetManager();
      manager2.DeleteProvider(providerName);
      this.RefreshProviders(manager2);
    }

    /// <inheritdoc />
    public void EnableProvider(string providerName)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager();
      manager.EnableProvider(providerName);
      this.RefreshProviders(manager);
    }

    /// <inheritdoc />
    public void DisableProvider(string providerName)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager();
      manager.DisableProvider(providerName);
      this.RefreshProviders(manager);
    }

    /// <inheritdoc />
    public string CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager();
      if (parameters == null)
        parameters = new NameValueCollection();
      parameters["moduleName"] = this.Name;
      manager.CreateProvider(providerName, providerTitle, parameters);
      this.RefreshProviders(manager);
      EventHub.Raise(DataProviderEventFactory.CreateDataProviderCreatedEvent(manager.GetType(), providerName, this.Name), false);
      return providerName;
    }

    /// <inheritdoc />
    public string GetDefaultProvider() => DynamicModuleManager.GetDefaultProviderName(this.Name);

    /// <inheritdoc />
    public string ProviderNameDefaultPrefix => "dynamicProvider";

    private void RefreshProviders(DynamicModuleManager manager) => this.ProviderInfos = manager.GetProviderInfos(this.Name);
  }
}
