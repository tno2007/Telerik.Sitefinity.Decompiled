// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SitePropertiesViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  [DataContract]
  public class SitePropertiesViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SitePropertiesViewModel" /> class.
    /// </summary>
    public SitePropertiesViewModel()
      : this((Site) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SitePropertiesViewModel" /> class.
    /// </summary>
    /// <param name="site">The site.</param>
    public SitePropertiesViewModel(Site site)
    {
      if (site == null)
        return;
      this.Id = site.Id;
      this.Name = site.Name;
      this.LiveUrl = site.LiveUrl;
      this.StagingUrl = site.StagingUrl;
      this.PublicContentCultures = (IList<CultureViewModel>) new List<CultureViewModel>();
      ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
      string str = site.DefaultCultureKey ?? resourcesConfig.Cultures.Values.First<CultureElement>().Key;
      foreach (string key in site.CultureKeys == null || site.CultureKeys.Count<string>() == 0 ? (IEnumerable<string>) new List<string>() : (IEnumerable<string>) site.CultureKeys)
      {
        CultureElement cultureElement;
        if (resourcesConfig.Cultures.TryGetValue(key, out cultureElement))
          this.PublicContentCultures.Add(new CultureViewModel(cultureElement)
          {
            IsDefault = cultureElement.Key == str
          });
      }
      this.UseSystemCultures = site.CultureKeys.Equals((object) null) || site.CultureKeys.Count.Equals(0);
      IEnumerable<string> list = (IEnumerable<string>) resourcesConfig.Cultures.Keys.ToList<string>();
      this.SystemCultures = (IList<CultureViewModel>) new List<CultureViewModel>();
      foreach (string key in list)
      {
        CultureElement cultureElement;
        if (resourcesConfig.Cultures.TryGetValue(key, out cultureElement))
          this.SystemCultures.Add(new CultureViewModel(cultureElement)
          {
            IsDefault = cultureElement.Key == str
          });
      }
      this.IsOffline = site.IsOffline;
      this.DomainAliases = site.DomainAliases;
      this.RequiresSsl = site.RequiresSsl;
      this.HomePageId = site.HomePageId;
      this.FrontEndLoginPageId = site.FrontEndLoginPageId;
      this.FrontEndLoginPageUrl = site.FrontEndLoginPageUrl;
      this.SiteMapRootNodeId = site.SiteMapRootNodeId;
      this.IsDefault = site.IsDefault;
      this.OfflineSiteMessage = site.OfflineSiteMessage;
      this.OfflinePageToRedirect = site.OfflinePageToRedirect;
      this.RedirectIfOffline = site.RedirectIfOffline;
      this.SiteConfigurationMode = PackagingOperations.IsMultisiteImportExportDisabled() ? SiteConfigurationMode.ConfigureManually : site.SiteConfigurationMode;
      this.IsCurrentSite = SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id == this.Id;
    }

    /// <summary>Gets or sets the site id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the site name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the staging URL.</summary>
    [DataMember]
    public string StagingUrl { get; set; }

    /// <summary>Gets or sets the live URL.</summary>
    [DataMember]
    public string LiveUrl { get; set; }

    /// <summary>Gets or sets a collection of public content cultures.</summary>
    [DataMember]
    public IList<CultureViewModel> PublicContentCultures { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the site is offline.
    /// </summary>
    [DataMember]
    public bool IsOffline { get; set; }

    /// <summary>Gets or sets a list of domain aliases.</summary>
    [DataMember]
    public IList<string> DomainAliases { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the site requires SSL.
    /// </summary>
    [DataMember]
    public bool RequiresSsl { get; set; }

    /// <summary>Gets or sets the home page id.</summary>
    [DataMember]
    public Guid HomePageId { get; set; }

    /// <summary>Gets the id of the frontend login page.</summary>
    /// <value>The id of the frontend login page.</value>
    [DataMember]
    public Guid FrontEndLoginPageId { get; set; }

    /// <summary>
    /// Gets the URL of the frontend login page.
    /// <para>If <see cref="P:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SitePropertiesViewModel.FrontEndLoginPageId" /> specified it will prevail and this parameter will be ignored.</para>
    /// </summary>
    /// <value>The URL of the frontend login page.</value>
    [DataMember]
    public string FrontEndLoginPageUrl { get; set; }

    /// <summary>Gets or sets the site map root node id.</summary>
    [DataMember]
    public Guid SiteMapRootNodeId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the default site.
    /// </summary>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a the textual message to display when the site if offline
    /// </summary>
    [DataMember]
    public string OfflineSiteMessage { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page to redirect to when the site if offline
    /// </summary>
    [DataMember]
    public Guid OfflinePageToRedirect { get; set; }

    /// <summary>
    /// Gets or sets the indicating whether to redirect the user when the site if offline. Otherwise a textual message is displayed
    /// </summary>
    [DataMember]
    public bool RedirectIfOffline { get; set; }

    /// <summary>
    /// Gets or sets the ID of the site to copy the pages from.
    /// </summary>
    [DataMember]
    public Guid SourcePagesSiteId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the instance is allowed to be set online or offline.
    /// </summary>
    [DataMember]
    public bool IsAllowedStartStop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this site is the currently selected site.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this site is the current site; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsCurrentSite { get; set; }

    /// <summary>Gets or sets the site configuration mode.</summary>
    /// <value>The site configuration mode.</value>
    [DataMember]
    public SiteConfigurationMode SiteConfigurationMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether specific cultures will be used
    /// </summary>
    [DataMember]
    public bool UseSystemCultures { get; set; }

    /// <summary>Gets or sets a collection of the system cultures.</summary>
    [DataMember]
    public IList<CultureViewModel> SystemCultures { get; set; }
  }
}
