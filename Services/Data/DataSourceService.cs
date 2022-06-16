// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Data.DataSourceService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Data
{
  /// <summary>
  /// Service that provides methods for working with data sources.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class DataSourceService : IDataSourceService
  {
    /// <summary>
    ///     <para>The web service relative URL of the DataSource Service.</para>
    ///     <para>'Sitefinity/Services/DataSourceService'</para>
    /// </summary>
    internal const string WebServiceUrl = "Sitefinity/Services/DataSourceService";

    /// <inheritdoc />
    public CollectionContext<DataProviderViewModel> GetDataSourceProviders(
      string siteId,
      string dataSourceName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool addDefaultSiteProvider)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataSourceProvidersInternal(siteId, dataSourceName, addDefaultSiteProvider, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<DataProviderViewModel> GetDataSourceProvidersInXml(
      string siteId,
      string dataSourceName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool addDefaultSiteProvider)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataSourceProvidersInternal(siteId, dataSourceName, addDefaultSiteProvider, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<DataProviderViewModel> GetTypeProviders(
      string siteId,
      string typeName)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataSourceProvidersInternal(siteId, typeName);
    }

    private CollectionContext<DataProviderViewModel> GetDataSourceProvidersInternal(
      string siteId,
      string dataSourceName,
      bool addDefaultSiteProvider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guard.ArgumentNotNullOrEmpty(dataSourceName, nameof (dataSourceName));
      IEnumerable<ISiteDataSource> source = Enumerable.Empty<ISiteDataSource>();
      IEnumerable<DataProviderInfo> providerInfos = Enumerable.Empty<DataProviderInfo>();
      if (SystemManager.MultisiteContext is MultisiteContext multisiteContext)
      {
        Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(siteId);
        if (guid != Guid.Empty)
        {
          source = multisiteContext.GetSiteById(guid).GetProviders(dataSourceName).Select<MultisiteContext.SiteDataSourceLinkProxy, ISiteDataSource>((Func<MultisiteContext.SiteDataSourceLinkProxy, ISiteDataSource>) (l => l.DataSource));
        }
        else
        {
          source = multisiteContext.GetDataSourcesByName(dataSourceName);
          IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().FirstOrDefault<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == dataSourceName));
          if (dataSource != null)
            providerInfos = dataSource.ProviderInfos;
        }
      }
      IQueryable<DataProviderViewModel> queryable = providerInfos.Select<DataProviderInfo, DataProviderViewModel>((Func<DataProviderInfo, DataProviderViewModel>) (pi => new DataProviderViewModel(pi))).Concat<DataProviderViewModel>(source.Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => !providerInfos.Any<DataProviderInfo>((Func<DataProviderInfo, bool>) (pi => pi.ProviderName.Equals(ds.Provider))))).Select<ISiteDataSource, DataProviderViewModel>((Func<ISiteDataSource, DataProviderViewModel>) (ds => new DataProviderViewModel()
      {
        Name = ds.Provider,
        Title = ds.Title
      }))).AsQueryable<DataProviderViewModel>();
      if (addDefaultSiteProvider)
      {
        DataProviderViewModel providerViewModel = new DataProviderViewModel()
        {
          Name = "sf-site-default-provider",
          Title = "Default source for the current site"
        };
        queryable = queryable.Concat<DataProviderViewModel>((IEnumerable<DataProviderViewModel>) new DataProviderViewModel[1]
        {
          providerViewModel
        });
      }
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<DataProviderViewModel>(filter);
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<DataProviderViewModel>(sortExpression);
      int num = queryable.Count<DataProviderViewModel>();
      if (skip > 0)
        queryable = queryable.Skip<DataProviderViewModel>(skip);
      if (take > 0)
        queryable = queryable.Take<DataProviderViewModel>(take);
      ServiceUtility.DisableCache();
      return new CollectionContext<DataProviderViewModel>((IEnumerable<DataProviderViewModel>) queryable)
      {
        TotalCount = num
      };
    }

    private CollectionContext<DataProviderViewModel> GetDataSourceProvidersInternal(
      string siteId,
      string typeName)
    {
      Guard.ArgumentNotNullOrEmpty(typeName, nameof (typeName));
      string dataSourceName = !typeof (DynamicContent).IsAssignableFrom(TypeResolutionService.ResolveType(typeName)) ? ManagerBase.GetMappedManager(typeName).GetType().FullName : ModuleBuilderManager.GetModules().GetTypeByFullName(typeName).ModuleName;
      return this.GetDataSourceProvidersInternal(siteId, dataSourceName, false, (string) null, 0, 0, (string) null);
    }
  }
}
