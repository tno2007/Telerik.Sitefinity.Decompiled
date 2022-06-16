// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.IMultisiteService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Multisite.Web.Services
{
  /// <summary>
  /// Interface that defines the members of the service working with multi sites.
  /// </summary>
  [ServiceContract]
  internal interface IMultisiteService
  {
    /// <summary>
    /// Creates a site. The created site is returned in JSON format.
    /// </summary>
    /// <param name="site">The view model of the site object.</param>
    /// <returns>The saved site.</returns>
    [WebHelp(Comment = "Creates a site. The created site is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/")]
    [OperationContract]
    SiteConfigurationViewModel CreateSite(SiteConfigurationViewModel site);

    /// <summary>
    /// Creates a site. The created site is returned in XML format.
    /// </summary>
    /// <param name="site">The view model of the site object.</param>
    /// <returns>The created site.</returns>
    [WebHelp(Comment = "Creates a site. The created site is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/")]
    [OperationContract]
    SiteConfigurationViewModel CreateSiteInXml(
      SiteConfigurationViewModel site);

    /// <summary>
    /// Creates a site using scheduled task. The scheduled task status can be retrieved using the CheckSiteInitializationStatus method.
    /// </summary>
    /// <param name="site">The view model of the site object.</param>
    [WebHelp(Comment = "Creates a site using scheduled task. The scheduled task status can be retrieved using the CheckSiteInitializationStatus method.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/createSiteAsync/")]
    [OperationContract]
    void CreateSiteAsync(SiteConfigurationViewModel site);

    /// <summary>Checks the status of the site initialization</summary>
    /// <param name="siteName">The name of the site.</param>
    [WebHelp(Comment = "Checks the status of the site initialization")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/checkSiteInitializationStatus/?siteName={siteName}")]
    [OperationContract]
    SiteInitializationStatus CheckSiteInitializationStatus(string siteName);

    /// <summary>
    /// Saves a site. If the site with specified id exists that site will be updated; otherwise new site will be created.
    /// The saved site is returned in JSON format.
    /// </summary>
    /// <param name="siteId">Id of the site to be saved.</param>
    /// <param name="site">The view model of the site object.</param>
    /// &gt;
    ///             <returns>The saved site.</returns>
    [WebHelp(Comment = "Saves a site. If the site with specified id exists that site will be updated; otherwise new site will be created. The saved site is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{siteId}/")]
    [OperationContract]
    SiteConfigurationViewModel SaveSite(
      string siteId,
      SiteConfigurationViewModel site);

    /// <summary>
    /// Saves a site. If the site with specified id exists that site will be updated; otherwise new site will be created.
    /// The saved site is returned in XML format.
    /// </summary>
    /// <param name="siteId">Id of the site to be saved.</param>
    /// <param name="site">The view model of the site object.</param>
    /// <returns>The saved site.</returns>
    [WebHelp(Comment = "Saves a site. If the site with specified id exists that site will be updated; otherwise new site will be created. The saved site is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{siteId}/")]
    [OperationContract]
    SiteConfigurationViewModel SaveSiteInXml(
      string siteId,
      SiteConfigurationViewModel site);

    /// <summary>
    /// Gets a collection of all sites. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all sites. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> GetSites(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets a collection of all sites. The results are returned in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all sites. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> GetSitesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets a collection of all sites for current user. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all sites. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "user/{userId}/sites/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> GetSitesForUser(
      string userId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets a collection of all sites for current user. The results are returned in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all sites. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "user/{userId}/sites/xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> GetSitesForUserInXml(
      string userId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the site and returns it in JSON format.</summary>
    /// <param name="siteId">Id of the site that ought to be retrieved.</param>
    /// <returns>An instance of SiteViewModel.</returns>
    [WebHelp(Comment = "Gets the site and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{siteId}/")]
    [OperationContract]
    SiteViewModel GetSite(string siteId);

    /// <summary>Gets the site and returns it in XML format.</summary>
    /// <param name="siteId">Id of the site that ought to be retrieved.</param>
    /// <returns>An instance of SiteViewModel.</returns>
    [WebHelp(Comment = "Gets the site and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{siteId}/")]
    [OperationContract]
    SiteViewModel GetSiteInXml(string siteId);

    /// <summary>
    /// Deletes a site with the specified Id. The results are returned in JSON format.
    /// </summary>
    /// <param name="siteId">The site Id.</param>
    [WebHelp(Comment = "Deletes a site with the specified Id. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{siteId}/")]
    [OperationContract]
    bool DeleteSite(string siteId);

    /// <summary>
    /// Deletes a site with the specified Id. The results are returned in XML format.
    /// </summary>
    /// <param name="siteId">The site Id.</param>
    [WebHelp(Comment = "Deletes a site with the specified Id. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{siteId}/")]
    [OperationContract]
    bool DeleteSiteInXml(string siteId);

    /// <summary>
    /// Sets the IsOffline property of a specific site object. The result is returned in JSON format.
    /// </summary>
    /// <param name="site">The view model of the site object.</param>
    /// <returns>The current value of the IsOffline property.</returns>
    [WebHelp(Comment = "Sets the IsOffline property of a specific site object. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/offline/")]
    [OperationContract]
    bool SetIsOffline(SiteViewModel site);

    /// <summary>
    /// Sets the IsOffline property of a specific site object. The result is returned in XML format.
    /// </summary>
    /// <param name="site">The view model of the site object.</param>
    /// <returns>The current value of the IsOffline property.</returns>
    [WebHelp(Comment = "Sets the IsOffline property of a specific site object. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/offline/")]
    [OperationContract]
    bool SetIsOfflineInXml(SiteViewModel site);

    /// <summary>
    /// Sets the IsDefault property of a specific site object. The result is returned in JSON format.
    /// </summary>
    /// <param name="site">The id of the site.</param>
    /// <returns>The current value of the IsDefault property.</returns>
    [WebHelp(Comment = "Sets the IsDefault property of a site with the specified Id. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/default/{siteId}/")]
    [OperationContract]
    bool SetDefault(string siteId);

    /// <summary>
    /// Sets the IsDefault property of a specific site object. The result is returned in XML format.
    /// </summary>
    /// <param name="site">The id of the site.</param>
    /// <returns>The current value of the IsDefault property.</returns>
    [WebHelp(Comment = "Sets the IsDefault property of a site with the specified Id. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/default/{siteId}/")]
    [OperationContract]
    bool SetDefaultInXml(string siteId);

    /// <summary>
    /// Sets the SiteConfigurationMode property of a specific site object. The result is returned in JSON format.
    /// </summary>
    /// <param name="siteId">The site identifier.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>
    /// The current value of the SiteConfigurationMode property.
    /// </returns>
    [WebHelp(Comment = "Sets the SiteConfigurationMode property of a site with the specified Id. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{siteId}/config/mode")]
    [OperationContract]
    SiteConfigurationModeViewModel SetSiteConfigurationMode(
      string siteId,
      SiteConfigurationModeViewModel mode);

    /// <summary>
    /// Sets the SiteConfigurationMode property of a specific site object. The result is returned in XML format.
    /// </summary>
    /// <param name="site">The id of the site.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>
    /// The current value of the SiteConfigurationMode property.
    /// </returns>
    [WebHelp(Comment = "Sets the SiteConfigurationMode property of a site with the specified Id. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{siteId}/config/mode")]
    [OperationContract]
    SiteConfigurationModeViewModel SetSiteConfigurationModeInXml(
      string siteId,
      SiteConfigurationModeViewModel mode);

    /// <summary>
    /// Sets the IsLocatedInMainMenu property of specified sites to true and to false for the rest of the sites. The results are returned in JSON format.
    /// </summary>
    /// <param name="ids">The sites ids.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> located in main menu objects.</returns>
    [WebHelp(Comment = "Sets the IsLocatedInMainMenu property of specified sites to true and to false for the rest of the sites. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/locateinmainmenu/")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> LocateInMainMenu(
      string[] ids);

    /// <summary>
    /// Sets the IsLocatedInMainMenu property of specified sites to true and to false for the rest of the sites. The results are returned in XML format.
    /// </summary>
    /// <param name="ids">The sites ids.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> located in main menu objects.</returns>
    [WebHelp(Comment = "Sets the IsLocatedInMainMenu property of specified sites to true and to false for the rest of the sites. The results are returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/locateinmainmenu/")]
    [OperationContract]
    CollectionContext<SiteGridViewModel> LocateInMainMenuInXml(
      string[] ids);

    /// <summary>
    /// Saves a site's data sources (providers per module per site) and returns the view model. The saved site is returned in JSON format.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="site">The view model of the site object.</param>
    /// <param name="provider">The provider through which the site ought to be saved.</param>
    /// <param name="sourcePagesSiteId">The ID of the site to copy the pages from.</param>
    /// <returns>The saved site.</returns>
    [WebHelp(Comment = "Saves a site's data sources (providers per module per site) and returns the view model. The saved site is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sources/{siteId}?provider={provider}")]
    [OperationContract]
    SiteConfigurationViewModel SaveSiteDataSources(
      string siteId,
      SiteConfigurationViewModel site,
      string provider);

    /// <summary>
    /// Saves a site's data sources (providers per module per site) and returns the view model. The saved site is returned in JSON format.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="site">The view model of the site object.</param>
    /// <param name="provider">The provider through which the site ought to be saved.</param>
    /// <param name="sourcePagesSiteId">The ID of the site to copy the pages from.</param>
    /// <returns>The saved site.</returns>
    /// 
    ///             [WebHelp(Comment = "Saves a site's data sources (providers per module per site) and returns the view model. The saved site is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/sources/{siteId}?provider={provider}")]
    [OperationContract]
    SiteConfigurationViewModel SaveSiteDataSourcesInXml(
      string siteId,
      SiteConfigurationViewModel site,
      string provider);

    /// <summary>
    /// Gets a collection of all available data sources. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all available data sources. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/datasources/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DataSourceGridViewModel> GetDataSources(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets a collection of all available data sources. The results are returned in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the sites.</param>
    /// <param name="skip">Number of sites to skip in result set. (used for paging)</param>
    /// <param name="take">Number of sites to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all available data sources. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/datasources/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DataSourceGridViewModel> GetDataSourcesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the site configuration.</summary>
    /// <param name="siteId">The site id.</param>
    [WebHelp(Comment = "Gets the site configuration. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{siteId}/config/")]
    [OperationContract]
    SiteConfigurationViewModel GetSiteConfiguration(string siteId);

    /// <summary>Gets the data sources configuration of a new site.</summary>
    /// <param name="siteName">The site name.</param>
    [WebHelp(Comment = "Gets the data sources configuration of a new site. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/config/sources/?siteName={siteName}")]
    [OperationContract]
    IEnumerable<SiteDataSourceConfigViewModel> GetNewSiteSourcesConfiguration(
      string siteName);

    /// <summary>
    /// Gets a collection of all available provider links for specific data source of the site. The results are returned in JSON format.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.AvailableLinkViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all available provider links for specific data source of the site. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{siteId}/{dataSourceName}/availablelinks/")]
    [OperationContract]
    CollectionContext<AvailableLinkViewModel> GetSiteDataSourceAvailableLinks(
      string siteId,
      string dataSourceName);

    /// <summary>
    /// Gets a collection of all available provider links for specific data source of the site. The results are returned in XML format.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.AvailableLinkViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets a collection of all available provider links for specific data source of the site. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{siteId}/{dataSourceName}/availablelinks/")]
    [OperationContract]
    CollectionContext<AvailableLinkViewModel> GetSiteDataSourceAvailableLinksInXml(
      string siteId,
      string dataSourceName);

    /// <summary>
    /// Deletes a provider linked on a specific site. The results are returned in JSON format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to delete.</param>
    /// <returns>True if the deletion was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/deleteprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool DeleteDataSource(string managerName, string providerName);

    /// <summary>
    /// Deletes a provider linked on a specific site. The results are returned in XML format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to delete.</param>
    /// <returns>True if the deletion was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/deleteprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool DeleteDataSourceInXml(string managerName, string providerName);

    /// <summary>
    /// Enables a provider linked on a specific site. The results are returned in JSON format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to enable.</param>
    /// <returns>True if enabling was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/enableprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool EnableDataSource(string managerName, string providerName);

    /// <summary>
    /// Enables a provider linked on a specific site. The results are returned in XML format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to enable.</param>
    /// <returns>True if enabling was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/enableprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool EnableDataSourceInXml(string managerName, string providerName);

    /// <summary>
    /// Disables a provider linked on a specific site. The results are returned in JSON format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to disable.</param>
    /// <returns>True if disabling was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/disableprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool DisableDataSource(string managerName, string providerName);

    /// <summary>
    /// Disables a provider linked on a specific site. The results are returned in XML format.
    /// </summary>
    /// <param name="managerName">The name of the related manager.</param>
    /// <param name="providerName">Name of the provider to disable.</param>
    /// <returns>True if disabling was successful. False otherwise.</returns>
    [WebHelp(Comment = "Deletes a provider linked on a specific site.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/disableprovider/?managerName={managerName}&providerName={providerName}")]
    [OperationContract]
    bool DisableDataSourceInXml(string managerName, string providerName);
  }
}
