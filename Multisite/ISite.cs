// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.ISite
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  public interface ISite
  {
    /// <summary>Gets the id.</summary>
    /// <value>The id.</value>
    Guid Id { get; }

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    string Name { get; }

    /// <summary>Gets the home page id.</summary>
    /// <value>The home page id.</value>
    Guid HomePageId { get; }

    /// <summary>Gets the id of the frontend login page.</summary>
    /// <value>The id of the frontend login page.</value>
    Guid FrontEndLoginPageId { get; }

    /// <summary>
    /// Gets the URL of the frontend login page.
    /// <para>If <see cref="P:Telerik.Sitefinity.Multisite.ISite.FrontEndLoginPageId" /> specified it will prevail and this parameter will be ignored.</para>
    /// </summary>
    /// <value>The URL of the frontend login page.</value>
    string FrontEndLoginPageUrl { get; }

    /// <summary>Gets the id of the default frontend template.</summary>
    /// <value>The id of the default frontend template.</value>
    Guid DefaultFrontendTemplateId { get; }

    /// <summary>Gets the site map root id.</summary>
    /// <value>The site map root id.</value>
    Guid SiteMapRootNodeId { get; }

    /// <summary>Gets the staging URL.</summary>
    /// <value>The staging URL.</value>
    string StagingUrl { get; }

    /// <summary>Gets the live URL.</summary>
    /// <value>The live URL.</value>
    string LiveUrl { get; }

    /// <summary>Gets the domain aliases.</summary>
    /// <value>The domain aliases.</value>
    IList<string> DomainAliases { get; }

    IList<MultisiteContext.SiteDataSourceLinkProxy> SiteDataSourceLinks { get; }

    /// <summary>
    /// Gets a value indicating whether the site requires SSL.
    /// </summary>
    /// <value><c>true</c> if requires SSL; otherwise, <c>false</c>.</value>
    bool RequiresSsl { get; }

    /// <summary>Gets the name of the site map.</summary>
    /// <value>The name of the site map.</value>
    string SiteMapName { get; }

    /// <summary>Gets the site map key.</summary>
    /// <value>The site map key.</value>
    string SiteMapKey { get; }

    /// <summary>
    /// Gets a value indicating whether this site is located in the main menu site selector.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this site is located in main menu site selector; otherwise, <c>false</c>.
    /// </value>
    bool IsLocatedInMainMenu { get; }

    /// <summary>Gets the site URL.</summary>
    /// <returns></returns>
    Uri GetUri();

    /// <summary>
    /// Gets site-data source link which contains information about the default provider for given data source associated with the current site
    /// </summary>
    /// <param name="dataSourceName">The name of the data source</param>
    /// <returns></returns>
    MultisiteContext.SiteDataSourceLinkProxy GetDefaultProvider(
      string dataSourceName);

    /// <summary>
    /// Gets collection of site-data source links which contains information about all providers for given data source associated with the current site
    /// </summary>
    /// <param name="dataSourceName">The name of the data source</param>
    /// <returns></returns>
    IEnumerable<MultisiteContext.SiteDataSourceLinkProxy> GetProviders(
      string dataSourceName);

    /// <summary>
    /// Gets a value indicating whether this site is offline and will not be accessible from the frontend.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is offline; otherwise, <c>false</c>.
    /// </value>
    bool IsOffline { get; }

    /// <summary>
    /// Gets the offline info behavior when the site is offline.
    /// </summary>
    /// <value>The offline info.</value>
    ISiteOfflineInfo OfflineInfo { get; }

    /// <summary>Gets the list of public content cultures.</summary>
    [Obsolete("Use cultures.")]
    CultureInfo[] PublicContentCultures { get; }

    /// <summary>Gets all languages for the site.</summary>
    CultureInfo[] Cultures { get; }

    /// <summary>Gets the public culture keys.</summary>
    [Obsolete("Use cultures.")]
    Dictionary<string, string> PublicCultures { get; }

    /// <summary>Gets the site default culture.</summary>
    CultureInfo DefaultCulture { get; }

    /// <summary>
    /// Gets the default culture key to find the Culture element in the ResourcesConfig.
    /// </summary>
    [Obsolete("For internal use only. Use DefaultCulture.")]
    string DefaultCultureKey { get; }

    /// <summary>Gets information if the site is marked as default.</summary>
    /// <value>The value if the site is default.</value>
    bool IsDefault { get; }

    /// <summary>
    /// Determines whether the specified module is accessible in context of this site.
    /// </summary>
    /// <param name="module">The module.</param>
    /// <returns>
    /// 	<c>true</c> if [is module accessible] [the specified module]; otherwise, <c>false</c>.
    /// </returns>
    bool IsModuleAccessible(IModule module);

    /// <summary>Sets a new home page for the site.</summary>
    /// <param name="pageId">The page id.</param>
    void SetHomePage(Guid pageId, PageManager manager = null);
  }
}
