// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteDataSourceLinkViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>Represents the provider view model.</summary>
  [DataContract]
  public class SiteDataSourceLinkViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteDataSourceLinkViewModel" /> class.
    /// </summary>
    public SiteDataSourceLinkViewModel()
      : this((SiteDataSourceLink) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteDataSourceLinkViewModel" /> class.
    /// </summary>
    /// <param name="siteDataSourceLink">The site data source link.</param>
    public SiteDataSourceLinkViewModel(SiteDataSourceLink siteDataSourceLink)
    {
      if (siteDataSourceLink == null)
        return;
      IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().Single<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == siteDataSourceLink.DataSource.Name));
      string str = siteDataSourceLink.DataSource.Title;
      if (string.IsNullOrEmpty(str))
      {
        DataProviderInfo dataProviderInfo = dataSource.Providers.SingleOrDefault<DataProviderInfo>((Func<DataProviderInfo, bool>) (pi => pi.ProviderName == siteDataSourceLink.DataSource.Provider));
        str = dataProviderInfo == null ? siteDataSourceLink.DataSource.Title : dataProviderInfo.ProviderTitle;
      }
      this.Id = siteDataSourceLink.Id;
      this.ProviderName = siteDataSourceLink.DataSource.Provider;
      this.ProviderTitle = str;
      this.SiteId = siteDataSourceLink.Site.Id;
      this.DataSourceName = siteDataSourceLink.DataSource.Name;
      this.IsDefault = siteDataSourceLink.IsDefault;
      this.IsGlobalProvider = this.DataSourceName == typeof (UserManager).FullName && SecurityManager.IsGlobalUserProvider(this.ProviderName);
    }

    /// <summary>Gets or sets the provider id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the provider name.</summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the provider title.</summary>
    [DataMember]
    public string ProviderTitle { get; set; }

    /// <summary>Gets or sets the site id.</summary>
    [DataMember]
    public Guid SiteId { get; set; }

    /// <summary>Gets or sets the name of the data source.</summary>
    [DataMember]
    public string DataSourceName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is default for the site.
    /// </summary>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a global provider
    /// </summary>
    [DataMember]
    public bool IsGlobalProvider { get; set; }
  }
}
