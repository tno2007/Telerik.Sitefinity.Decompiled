// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.MultisiteService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.ScheduledTasks;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Multisite.Web.Services
{
  /// <summary>
  /// Service that provides methods for working with multi sites.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MultisiteService : IMultisiteService
  {
    internal const string NewProviderPrefix = "new-sf-provider-";
    internal const string WebServiceUrl = "~/Sitefinity/Services/Multisite/Multisite.svc/";
    internal const string MainMenuSitesKey = "mainMenuSites";

    /// <inheritdoc />
    public SiteConfigurationViewModel CreateSite(
      SiteConfigurationViewModel site)
    {
      return this.SaveSiteInternal(Guid.Empty.ToString(), site);
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel CreateSiteInXml(
      SiteConfigurationViewModel site)
    {
      return this.SaveSiteInternal(Guid.Empty.ToString(), site);
    }

    /// <inheritdoc />
    public void CreateSiteAsync(SiteConfigurationViewModel site)
    {
      this.ValidateAllowedAction("CreateEditSite");
      SchedulingManager manager = SchedulingManager.GetManager();
      IQueryable<ScheduledTaskData> taskData = manager.GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == CreateSiteTask.Name);
      foreach (ScheduledTaskData task in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
      {
        if (CreateSiteTaskSettings.Parse(task.TaskData).Model.Name == site.Name)
        {
          if (task.Status != TaskStatus.Failed)
            throw new InvalidOperationException("Site is already initializing");
          manager.DeleteTaskData(task);
        }
      }
      CreateSiteTask createSiteTask = new CreateSiteTask();
      createSiteTask.Id = Guid.NewGuid();
      createSiteTask.Settings = new CreateSiteTaskSettings()
      {
        Model = site,
        CurrentUserId = SecurityManager.CurrentUserId
      };
      createSiteTask.ExecuteTime = DateTime.UtcNow;
      createSiteTask.Title = site.Name;
      CreateSiteTask task1 = createSiteTask;
      manager.AddTask((ScheduledTask) task1);
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public SiteInitializationStatus CheckSiteInitializationStatus(
      string siteName)
    {
      IQueryable<ScheduledTaskData> taskData = SchedulingManager.GetManager().GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == CreateSiteTask.Name);
      foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
      {
        if (CreateSiteTaskSettings.Parse(scheduledTaskData.TaskData).Model.Name == siteName)
          return new SiteInitializationStatus()
          {
            IsInProgress = scheduledTaskData.Status != TaskStatus.Failed,
            ErrorMessage = scheduledTaskData.StatusMessage
          };
      }
      return new SiteInitializationStatus()
      {
        IsInProgress = false,
        ErrorMessage = string.Empty
      };
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel SaveSite(
      string siteId,
      SiteConfigurationViewModel site)
    {
      return this.SaveSiteInternal(siteId, site);
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel SaveSiteInXml(
      string siteId,
      SiteConfigurationViewModel site)
    {
      return this.SaveSiteInternal(siteId, site);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> GetSites(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSitesInternal(sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> GetSitesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSitesInternal(sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> GetSitesForUser(
      string userId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSitesForUserInternal(userId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> GetSitesForUserInXml(
      string userId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSitesForUserInternal(userId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public SiteViewModel GetSite(string siteId) => this.GetSiteInternal(siteId);

    /// <inheritdoc />
    public SiteViewModel GetSiteInXml(string siteId) => this.GetSiteInternal(siteId);

    /// <inheritdoc />
    public bool DeleteSite(string siteId) => this.DeleteSiteInternal(siteId);

    /// <inheritdoc />
    public bool DeleteSiteInXml(string siteId) => this.DeleteSiteInternal(siteId);

    /// <inheritdoc />
    public bool SetIsOffline(SiteViewModel site) => this.SetIsOfflineInternal(site);

    /// <inheritdoc />
    public bool SetIsOfflineInXml(SiteViewModel site) => this.SetIsOfflineInternal(site);

    /// <inheritdoc />
    public bool SetDefault(string siteId) => this.SetDefaultInternal(siteId);

    /// <inheritdoc />
    public bool SetDefaultInXml(string siteId) => this.SetDefaultInternal(siteId);

    /// <inheritdoc />
    public SiteConfigurationModeViewModel SetSiteConfigurationMode(
      string siteId,
      SiteConfigurationModeViewModel mode)
    {
      return this.SetSiteConfigurationModeInternal(siteId, mode);
    }

    /// <inheritdoc />
    public SiteConfigurationModeViewModel SetSiteConfigurationModeInXml(
      string siteId,
      SiteConfigurationModeViewModel mode)
    {
      return this.SetSiteConfigurationModeInternal(siteId, mode);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> LocateInMainMenu(
      string[] ids)
    {
      return this.LocateInMainMenuInternal(ids);
    }

    /// <inheritdoc />
    public CollectionContext<SiteGridViewModel> LocateInMainMenuInXml(
      string[] ids)
    {
      return this.LocateInMainMenuInternal(ids);
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel SaveSiteDataSources(
      string siteId,
      SiteConfigurationViewModel site,
      string provider)
    {
      return this.SaveSiteDataSourcesInternal(siteId, site, false);
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel SaveSiteDataSourcesInXml(
      string siteId,
      SiteConfigurationViewModel site,
      string provider)
    {
      return this.SaveSiteDataSourcesInternal(siteId, site, false);
    }

    /// <inheritdoc />
    [Obsolete]
    public CollectionContext<DataSourceGridViewModel> GetDataSources(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetDataSourcesInternal(sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    [Obsolete]
    public CollectionContext<DataSourceGridViewModel> GetDataSourcesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetDataSourcesInternal(sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public SiteConfigurationViewModel GetSiteConfiguration(string siteId) => this.GetSiteConfigurationInternal(siteId);

    public IEnumerable<SiteDataSourceConfigViewModel> GetNewSiteSourcesConfiguration(
      string siteName)
    {
      return this.GetNewSiteSourcesConfigurationInternal(siteName);
    }

    /// <inheritdoc />
    public CollectionContext<AvailableLinkViewModel> GetSiteDataSourceAvailableLinks(
      string siteId,
      string dataSourceName)
    {
      return this.GetSiteDataSourceAvailableLinksInternal(siteId, dataSourceName);
    }

    /// <inheritdoc />
    public CollectionContext<AvailableLinkViewModel> GetSiteDataSourceAvailableLinksInXml(
      string siteId,
      string dataSourceName)
    {
      return this.GetSiteDataSourceAvailableLinksInternal(siteId, dataSourceName);
    }

    /// <inheritdoc />
    public bool DeleteDataSource(string managerName, string providerName) => this.DeleteDataSourceInternal(managerName, providerName);

    /// <inheritdoc />
    public bool DeleteDataSourceInXml(string managerName, string providerName) => this.DeleteDataSourceInternal(managerName, providerName);

    /// <inheritdoc />
    public bool EnableDataSource(string managerName, string providerName) => this.EnableDataSourceInternal(managerName, providerName);

    /// <inheritdoc />
    public bool EnableDataSourceInXml(string managerName, string providerName) => this.EnableDataSourceInternal(managerName, providerName);

    /// <inheritdoc />
    public bool DisableDataSource(string managerName, string providerName) => this.DisableDataSourceInternal(managerName, providerName);

    /// <inheritdoc />
    public bool DisableDataSourceInXml(string managerName, string providerName) => this.DisableDataSourceInternal(managerName, providerName);

    private CollectionContext<SiteGridViewModel> GetSitesInternal(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      try
      {
        ServiceUtility.RequestBackendUserAuthentication();
        IQueryable<MultisiteContext.SiteProxy> source = SystemManager.CurrentContext.GetSites(true).Select<ISite, MultisiteContext.SiteProxy>((Func<ISite, MultisiteContext.SiteProxy>) (s => s as MultisiteContext.SiteProxy)).AsQueryable<MultisiteContext.SiteProxy>();
        if (!string.IsNullOrEmpty(filter))
          source = source.Where<MultisiteContext.SiteProxy>(filter);
        if (!string.IsNullOrEmpty(sortExpression))
          source = source.OrderBy<MultisiteContext.SiteProxy>(sortExpression);
        int num = source.Count<MultisiteContext.SiteProxy>();
        if (skip > 0)
          source = source.Skip<MultisiteContext.SiteProxy>(skip);
        if (take > 0)
          source = source.Take<MultisiteContext.SiteProxy>(take);
        ServiceUtility.DisableCache();
        ResourcesConfig resConfig = Config.Get<ResourcesConfig>();
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ; //unable to render the statement
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private CollectionContext<SiteGridViewModel> GetSitesForUserInternal(
      string userId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      try
      {
        Guid result;
        if (!Guid.TryParse(userId, out result))
          throw new ArgumentException("Invalid user id format. Must be guid.");
        if (ClaimsManager.GetCurrentUserId() != result)
          ServiceUtility.RequestAuthentication(true);
        UserActivityRecord userActivityRecord = (UserActivityRecord) UserActivityManager.Cache[userId];
        List<Guid> allowedSites = userActivityRecord == null ? SystemManager.MultisiteContext.GetAllowedSites(result, "Default").ToList<Guid>() : userActivityRecord.AllowedSites;
        IQueryable<ISite> source = SystemManager.MultisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => allowedSites.Contains(s.Id))).AsQueryable<ISite>();
        if (!string.IsNullOrEmpty(sortExpression))
          source = source.OrderBy<ISite>(sortExpression);
        if (!string.IsNullOrEmpty(filter))
          source = source.Where<ISite>(filter);
        int num = source.Count<ISite>();
        if (skip > 0)
          source = source.Skip<ISite>(skip);
        if (take > 0)
          source = source.Take<ISite>(take);
        ServiceUtility.DisableCache();
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ; //unable to render the statement
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private static Site GetSite(MultisiteManager manager, Guid siteId)
    {
      try
      {
        return manager.GetSite(siteId);
      }
      catch (ItemNotFoundException ex)
      {
        return (Site) null;
      }
    }

    private SiteViewModel GetSiteInternal(string siteId)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "AccessSite");
        Guid siteId1 = new Guid(siteId);
        Site site = MultisiteService.GetSite(MultisiteManager.GetManager(), siteId1);
        SiteViewModel siteInternal = site != null ? new SiteViewModel(site) : throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
        siteInternal.IsAllowedStartStop = this.IsCurrentUserAllowedAction((ISecuredObject) site, "StartStopSite");
        ServiceUtility.DisableCache();
        return siteInternal;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private bool DeleteSiteInternal(string siteId)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "DeleteSite");
        Guid parsedSiteId = new Guid(siteId);
        using (new UnrestrictedModeRegion())
        {
          MultisiteManager manager1 = MultisiteManager.GetManager();
          Site site = MultisiteService.GetSite(manager1, parsedSiteId);
          Guid id1 = site != null ? site.SiteMapRootNodeId : throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
          List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
          foreach (SiteDataSourceLink siteDataSourceLink in (IEnumerable<SiteDataSourceLink>) site.SiteDataSourceLinks)
          {
            SiteDataSourceLink dataSourceLink = siteDataSourceLink;
            if (manager1.GetSiteDataSourceLinks().Count<SiteDataSourceLink>((Expression<Func<SiteDataSourceLink, bool>>) (l => l.DataSource.Name == dataSourceLink.DataSource.Name && l.DataSource.Provider == dataSourceLink.DataSource.Provider && l.Site.Id != parsedSiteId)) == 0)
              tupleList.Add(new Tuple<string, string>(dataSourceLink.DataSource.Name, dataSourceLink.DataSource.Provider));
          }
          foreach (Tuple<string, string> tuple in tupleList)
            this.DisableDataSourceInternal(tuple.Item1, tuple.Item2);
          manager1.Delete(site);
          manager1.SaveChanges();
          this.DeleteSplitTaxonomiesForSite(parsedSiteId);
          if (id1 != Guid.Empty)
          {
            PageManager manager2 = PageManager.GetManager();
            PageNode pageNode = manager2.GetPageNode(id1);
            if (pageNode != null)
            {
              Guid id2 = pageNode.Id;
              PageHelper.DeletePageNodesTree(pageNode, manager2);
              this.RemoveRootPageNodeFromCustomSecurityConfigLabels(id2);
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
        this.HandleException(ex);
        throw;
      }
    }

    private void DeleteSplitTaxonomiesForSite(Guid siteId)
    {
      Type taxonomyInterface = typeof (ITaxonomy);
      string[] taxonomyTypeNames = ((IEnumerable<Type>) taxonomyInterface.Assembly.GetTypes()).Where<Type>((Func<Type, bool>) (p => taxonomyInterface.IsAssignableFrom(p))).Select<Type, string>((Func<Type, string>) (t => t.FullName)).ToArray<string>();
      ITaxonomyMultisiteTaskService multisiteTaskService = ObjectFactory.Resolve<ITaxonomyMultisiteTaskService>();
      foreach (TaxonomyDataProvider staticProvider in (Collection<TaxonomyDataProvider>) TaxonomyManager.GetManager().StaticProviders)
      {
        IQueryable<SiteItemLink> source = TaxonomyManager.GetManager(staticProvider.Name).Provider.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && taxonomyTypeNames.Contains<string>(l.ItemType)));
        Expression<Func<SiteItemLink, Guid>> selector = (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId);
        foreach (Guid taxonomyId in (IEnumerable<Guid>) source.Select<SiteItemLink, Guid>(selector).Distinct<Guid>())
          multisiteTaskService.CreateTaxonomyUnlinkFromSiteCleanTask(siteId, taxonomyId, staticProvider.Name);
      }
    }

    private void RemoveRootPageNodeFromCustomSecurityConfigLabels(Guid rootPageNodeId)
    {
      ConfigManager manager = ConfigManager.GetManager();
      SecurityConfig section = manager.GetSection<SecurityConfig>();
      string str = rootPageNodeId.ToString();
      CustomPermissionsDisplaySettingsConfig permissionsDisplaySetting = section.CustomPermissionsDisplaySettings["Pages"];
      bool flag = false;
      if (permissionsDisplaySetting != null && permissionsDisplaySetting.SecuredObjectCustomPermissionSets.Keys.Count > 0)
      {
        SecuredObjectCustomPermissionSet customPermissionSet = permissionsDisplaySetting.SecuredObjectCustomPermissionSets[typeof (PageNode).FullName];
        if (customPermissionSet != null)
        {
          List<string> list = ((IEnumerable<string>) customPermissionSet.SecuredObjectIds.Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
          if (list.Contains(str))
          {
            list.Remove(str);
            customPermissionSet.SecuredObjectIds = string.Join(",", (IEnumerable<string>) list);
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      manager.SaveSection((ConfigSection) section);
    }

    private bool SetIsOfflineInternal(SiteViewModel site)
    {
      try
      {
        this.ValidateAllowedAction(site.Id.ToString(), "StartStopSite");
        MultisiteManager manager = MultisiteManager.GetManager();
        Site site1 = MultisiteService.GetSite(manager, site.Id);
        if (site1 == null)
          throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
        site1.IsOffline = site.IsOffline;
        site1.OfflineSiteMessage = site.OfflineSiteMessage;
        site1.OfflinePageToRedirect = site.OfflinePageToRedirect;
        manager.SaveChanges();
        ServiceUtility.DisableCache();
        return site1.IsOffline;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private bool SetDefaultInternal(string siteId)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "CreateEditSite");
        Guid siteId1 = new Guid(siteId);
        MultisiteManager manager = MultisiteManager.GetManager();
        manager.SetDefaultSite(siteId1);
        manager.SaveChanges();
        ServiceUtility.DisableCache();
        return true;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private SiteConfigurationModeViewModel SetSiteConfigurationModeInternal(
      string siteId,
      SiteConfigurationModeViewModel configuration)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "CreateEditSite");
        Guid siteId1 = new Guid(siteId);
        MultisiteManager manager = MultisiteManager.GetManager();
        Site site = MultisiteService.GetSite(manager, siteId1);
        if (site == null)
          throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
        site.SiteConfigurationMode = configuration.Mode;
        manager.SaveChanges();
        ServiceUtility.DisableCache();
        return configuration;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    internal SiteConfigurationViewModel SaveSiteInternal(
      string siteId,
      SiteConfigurationViewModel site,
      bool createSiteDependencies = false,
      Guid? rootNodeId = null,
      string transactionName = null,
      bool skipRestart = false)
    {
      try
      {
        if (new Guid(siteId) == Guid.Empty)
          this.ValidateAllowedAction("CreateEditSite");
        else
          this.ValidateAllowedAction(siteId, "CreateEditSite");
        using (new UnrestrictedModeRegion())
          return this.SaveSite(siteId, site, out bool _, createSiteDependencies, rootNodeId, transactionName, skipRestart);
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    internal SiteConfigurationViewModel SaveSite(
      string siteId,
      SiteConfigurationViewModel site,
      out bool restartNeeded,
      bool createSiteDependencies = false,
      Guid? rootNodeId = null,
      string transactionName = null,
      bool skipRestart = false)
    {
      Guid siteId1 = new Guid(siteId);
      bool flag1 = siteId1 == Guid.Empty;
      restartNeeded = false;
      SiteInitializer siteInitializer = string.IsNullOrEmpty(transactionName) ? SiteInitializer.GetInitializer() : new SiteInitializer(transactionName);
      transactionName = string.IsNullOrEmpty(transactionName) ? siteInitializer.TransactionName : transactionName;
      Guid guid = Guid.Empty;
      try
      {
        MultisiteManager manager1 = MultisiteManager.GetManager((string) null, transactionName);
        ConfigManager manager2 = ConfigManager.GetManager();
        List<ConfigSection> sectionsToSave = new List<ConfigSection>();
        Site site1 = !flag1 ? manager1.GetSite(siteId1) : manager1.CreateSite();
        site1.Name = site.Name;
        ResourcesConfig section1 = manager2.GetSection<ResourcesConfig>();
        bool resourcesConfigChanged;
        MultisiteService.UpdateSiteCultures(site, site1, sectionsToSave, section1, out resourcesConfigChanged);
        site1.StagingUrl = !site.StagingUrl.IsNullOrEmpty() ? site.StagingUrl.ToLower() : string.Empty;
        site1.LiveUrl = !site.LiveUrl.IsNullOrEmpty() ? site.LiveUrl.ToLower() : string.Empty;
        if (site1.IsOffline != site.IsOffline)
          site1.IsOffline = site.IsOffline;
        site1.DomainAliases = site.DomainAliases;
        site1.RequiresSsl = site.RequiresSsl;
        site1.HomePageId = site.HomePageId;
        site1.FrontEndLoginPageId = site.FrontEndLoginPageId;
        site1.FrontEndLoginPageUrl = site.FrontEndLoginPageUrl;
        site1.IsDefault = site.IsDefault;
        site1.OfflineSiteMessage = site.OfflineSiteMessage;
        site1.OfflinePageToRedirect = site.OfflinePageToRedirect;
        site1.RedirectIfOffline = site.RedirectIfOffline;
        site1.SiteConfigurationMode = site.SiteConfigurationMode;
        TransactionManager.CommitTransaction(transactionName);
        if (flag1 | createSiteDependencies)
        {
          guid = site1.Id;
          Guid targetSiteHomepageId = Guid.Empty;
          CultureElement cultureElement = (CultureElement) null;
          section1.Cultures.TryGetValue(site1.DefaultCultureKey, out cultureElement);
          PageNode targetSiteRoot = (PageNode) null;
          Guid rootId = !rootNodeId.HasValue ? siteInitializer.PageManager.Provider.GetNewGuid() : rootNodeId.Value;
          if (cultureElement != null)
          {
            using (new CultureRegion(cultureElement.UICulture))
              targetSiteRoot = siteInitializer.CreateSiteRoot(rootId, site.Name, site.Name);
          }
          else
            targetSiteRoot = siteInitializer.CreateSiteRoot(rootId, site.Name, site.Name);
          site1.SiteMapRootNodeId = targetSiteRoot.Id;
          siteInitializer.PageManager.Provider.WithSuppressedValidationOnCommit((Action) (() => TransactionManager.CommitTransaction(transactionName)));
          site1 = this.UpdateDataSources(site1.Id, site, true, manager1);
          SecurityConfig section2 = manager2.GetSection<SecurityConfig>();
          CustomPermissionsDisplaySettingsConfig permissionsDisplaySetting = section2.CustomPermissionsDisplaySettings["Pages"];
          if (permissionsDisplaySetting != null && permissionsDisplaySetting.SecuredObjectCustomPermissionSets.Keys.Count > 0)
          {
            SecuredObjectCustomPermissionSet customPermissionSet = permissionsDisplaySetting.SecuredObjectCustomPermissionSets[typeof (PageNode).FullName];
            bool flag2 = false;
            if (customPermissionSet != null)
            {
              if (string.IsNullOrWhiteSpace(customPermissionSet.SecuredObjectIds))
              {
                customPermissionSet.SecuredObjectIds = targetSiteRoot.Id.ToString();
                flag2 = true;
              }
              else
              {
                List<string> list = ((IEnumerable<string>) customPermissionSet.SecuredObjectIds.Split(new char[1]
                {
                  ','
                }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
                if (!list.Contains(targetSiteRoot.Id.ToString()))
                {
                  list.Add(targetSiteRoot.Id.ToString());
                  customPermissionSet.SecuredObjectIds = string.Join(",", (IEnumerable<string>) list);
                  flag2 = true;
                }
              }
            }
            if (flag2)
              sectionsToSave.Add((ConfigSection) section2);
          }
          if (site.SourcePagesSiteId != Guid.Empty)
          {
            if (SystemManager.CurrentHttpContext != null)
              SystemManager.CurrentHttpContext.Server.ScriptTimeout = 1800;
            using (SiteRegion.FromSiteId(site.SourcePagesSiteId))
            {
              MultisiteService.DuplicateSitePages(transactionName, site.SourcePagesSiteId, site1.Id, targetSiteRoot, out targetSiteHomepageId);
              MultisiteService.DuplicateSiteItemLinks(site.SourcePagesSiteId, site1.Id, transactionName);
            }
          }
          TransactionManager.CommitTransaction(transactionName);
          if (targetSiteHomepageId != Guid.Empty)
          {
            site1 = manager1.GetSite(site1.Id);
            site1.HomePageId = targetSiteHomepageId;
            TransactionManager.CommitTransaction(transactionName);
          }
        }
        foreach (ConfigSection section3 in sectionsToSave)
        {
          if (section3 is ResourcesConfig)
            manager2.SaveSection(section3, true);
          else
            manager2.SaveSection(section3);
        }
        SystemManager.GetCacheManager(CacheManagerInstance.UserActivities).Remove(SecurityManager.CurrentUserId.ToString());
        SiteMapBase.Reset();
        if (resourcesConfigChanged)
        {
          if (!skipRestart)
            SystemManager.RestartApplication(OperationReason.FromKey("LocalizationChange"), SystemRestartFlags.ResetModel);
          else
            restartNeeded = true;
        }
        return new SiteConfigurationViewModel(site1);
      }
      catch (Exception ex1)
      {
        TransactionManager.RollbackTransaction(transactionName);
        if (guid != Guid.Empty)
        {
          try
          {
            this.DeleteSiteInternal(guid.ToString());
          }
          catch (Exception ex2)
          {
            Log.Write((object) ex2);
          }
        }
        Log.Write((object) ex1);
        throw;
      }
    }

    private static void UpdateSiteCultures(
      SiteConfigurationViewModel site,
      Site modelSite,
      List<ConfigSection> sectionsToSave,
      ResourcesConfig resourcesConfig,
      out bool resourcesConfigChanged)
    {
      string str;
      if (site.UseSystemCultures || site.PublicContentCultures == null || site.PublicContentCultures.Count == 0)
      {
        site.PublicContentCultures = (IList<CultureViewModel>) new List<CultureViewModel>();
        IList<CultureViewModel> systemCultures = site.SystemCultures;
        str = (systemCultures != null ? systemCultures.SingleOrDefault<CultureViewModel>((Func<CultureViewModel, bool>) (x => x.IsDefault))?.Key : (string) null) ?? resourcesConfig.Cultures.Values.First<CultureElement>().Key;
      }
      else
      {
        CultureViewModel cultureViewModel = site.PublicContentCultures.SingleOrDefault<CultureViewModel>((Func<CultureViewModel, bool>) (culture => culture.IsDefault));
        str = cultureViewModel != null ? cultureViewModel.Key : site.PublicContentCultures.First<CultureViewModel>().Key;
      }
      modelSite.DefaultCultureKey = str;
      resourcesConfigChanged = false;
      resourcesConfigChanged = MultisiteService.AddMissingCultures(site.PublicContentCultures, modelSite, resourcesConfig);
      MultisiteService.DeleteRemovedCultures(site, modelSite);
      MultisiteService.ReorderCultures(site, modelSite);
      if (!resourcesConfigChanged)
        return;
      sectionsToSave.Add((ConfigSection) resourcesConfig);
    }

    private static bool AddMissingCultures(
      IList<CultureViewModel> cultures,
      Site modelSite,
      ResourcesConfig resourcesConfig)
    {
      bool flag = false;
      foreach (CultureViewModel culture1 in (IEnumerable<CultureViewModel>) cultures)
      {
        if (!resourcesConfig.Cultures.Contains(culture1.Key))
        {
          resourcesConfig.Cultures.Add(new CultureElement()
          {
            Culture = culture1.Culture,
            DisplayName = culture1.DisplayName,
            Key = culture1.Key,
            UICulture = culture1.UICulture,
            FieldSuffix = culture1.FieldSuffix
          });
          flag = true;
        }
        if (!modelSite.CultureKeys.Contains(culture1.Key))
          modelSite.CultureKeys.Add(culture1.Key);
        if (culture1.IsDefault)
          modelSite.DefaultCultureKey = culture1.Key;
        CultureInfo culture2 = new CultureInfo(culture1.Culture);
        resourcesConfig.CheckCustomCulture(culture2);
      }
      return flag;
    }

    /// <summary>
    /// Deletes site's cultures if they were removed from the site's view model presentation.
    /// </summary>
    /// <param name="site">The view model that presents site's properties in the UI.</param>
    /// <param name="modelSite">The site's persistent model will be updated (synchronized).</param>
    private static void DeleteRemovedCultures(SiteConfigurationViewModel site, Site modelSite)
    {
      List<string> stringList = new List<string>();
      foreach (string cultureKey in (IEnumerable<string>) modelSite.CultureKeys)
      {
        bool flag = false;
        foreach (CultureViewModel publicContentCulture in (IEnumerable<CultureViewModel>) site.PublicContentCultures)
        {
          if (publicContentCulture.Key.Equals(cultureKey))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          stringList.Add(cultureKey);
      }
      foreach (string str in stringList)
        modelSite.CultureKeys.Remove(str);
    }

    private static void ReorderCultures(SiteConfigurationViewModel site, Site modelSite)
    {
      if (!site.PublicContentCultures.Count.Equals(modelSite.CultureKeys.Count) || site.PublicContentCultures.Select<CultureViewModel, string>((Func<CultureViewModel, string>) (culture => culture.Key)).SequenceEqual<string>((IEnumerable<string>) modelSite.CultureKeys))
        return;
      List<string> stringList = new List<string>(site.PublicContentCultures.Count);
      for (int i = 0; i < site.PublicContentCultures.Count; i++)
      {
        string str = modelSite.CultureKeys.SingleOrDefault<string>((Func<string, bool>) (cultureKey => cultureKey.Equals(site.PublicContentCultures[i].Key)));
        if (string.IsNullOrEmpty(str))
          return;
        stringList.Add(str);
      }
      modelSite.CultureKeys = (IList<string>) stringList;
    }

    private static void DuplicateSitePages(
      string transactionName,
      Guid sourceSiteId,
      Guid targetSiteId,
      PageNode targetSiteRoot,
      out Guid targetSiteHomepageId)
    {
      targetSiteHomepageId = Guid.Empty;
      PageManager manager = PageManager.GetManager((string) null, transactionName);
      ISite sourceSite = SystemManager.CurrentContext.MultisiteContext.GetSiteById(sourceSiteId);
      ISite siteById = SystemManager.CurrentContext.MultisiteContext.GetSiteById(targetSiteId);
      CultureInfo[] array = ((IEnumerable<CultureInfo>) sourceSite.PublicContentCultures).Intersect<CultureInfo>((IEnumerable<CultureInfo>) siteById.PublicContentCultures).ToArray<CultureInfo>();
      if (sourceSite == null || !(sourceSite.SiteMapRootNodeId != Guid.Empty))
        return;
      PageNode sourceRootNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == sourceSite.SiteMapRootNodeId));
      if (sourceRootNode == null)
        return;
      IDictionary<Guid, ISet<Guid>> pageIdProperties = MultisiteService.GetPageIdProperties(manager, sourceSite.SiteMapRootNodeId);
      Dictionary<Guid, Guid> correspondingPageId1 = new Dictionary<Guid, Guid>();
      MultisiteService.DuplicatePageNodesTree(sourceRootNode, targetSiteRoot, manager, (IDictionary<Guid, Guid>) correspondingPageId1, array);
      correspondingPageId1.TryGetValue(sourceSite.HomePageId, out targetSiteHomepageId);
      TransactionManager.FlushTransaction(transactionName);
      Dictionary<Guid, Guid> correspondingPageId2 = correspondingPageId1;
      PageManager pManagerInstance = manager;
      MultisiteService.MirrorPageIdProperties(pageIdProperties, (IDictionary<Guid, Guid>) correspondingPageId2, pManagerInstance);
      MultisiteService.MirrorRedirectPageId((IDictionary<Guid, Guid>) correspondingPageId1, targetSiteRoot.Id, manager);
    }

    private static void MirrorRedirectPageId(
      IDictionary<Guid, Guid> correspondingPageId,
      Guid newSiteRootNodeId,
      PageManager pManager)
    {
      IQueryable<PageNode> pageNodes = pManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (n => n.RootNodeId == newSiteRootNodeId && (Guid?) n.LinkedNodeId != new Guid?());
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
      {
        Guid guid;
        if (correspondingPageId.TryGetValue(pageNode.LinkedNodeId, out guid))
          pageNode.LinkedNodeId = guid;
      }
      TransactionManager.FlushTransaction(pManager.TransactionName);
    }

    private static void MirrorPageIdProperties(
      IDictionary<Guid, ISet<Guid>> originalPageIdPropertyIds,
      IDictionary<Guid, Guid> correspondingPageId,
      PageManager pManagerInstance)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MultisiteService.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new MultisiteService.\u003C\u003Ec__DisplayClass54_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.originalPageIdPropertyIds = originalPageIdPropertyIds;
      // ISSUE: reference to a compiler-generated field
      foreach (Guid key in (IEnumerable<Guid>) cDisplayClass540.originalPageIdPropertyIds.Keys)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MultisiteService.\u003C\u003Ec__DisplayClass54_1 cDisplayClass541 = new MultisiteService.\u003C\u003Ec__DisplayClass54_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass541.CS\u0024\u003C\u003E8__locals1 = cDisplayClass540;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass541.pageId = key;
        Guid guid;
        // ISSUE: reference to a compiler-generated field
        if (correspondingPageId.TryGetValue(cDisplayClass541.pageId, out guid))
        {
          IQueryable<ControlProperty> properties = pManagerInstance.GetProperties();
          ParameterExpression parameterExpression;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          Expression<Func<ControlProperty, bool>> predicate = Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.Equal(p.Value, (Expression) Expression.Call(cDisplayClass541.pageId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())), parameterExpression);
          // ISSUE: reference to a compiler-generated method
          foreach (ControlProperty controlProperty in properties.Where<ControlProperty>(predicate).AsEnumerable<ControlProperty>().Where<ControlProperty>(new Func<ControlProperty, bool>(cDisplayClass541.\u003CMirrorPageIdProperties\u003Eb__1)).ToArray<ControlProperty>())
            controlProperty.Value = guid.ToString();
          TransactionManager.FlushTransaction(pManagerInstance.TransactionName);
        }
      }
    }

    private static IDictionary<Guid, ISet<Guid>> GetPageIdProperties(
      PageManager pManagerInstance,
      Guid rootNodeId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MultisiteService.\u003C\u003Ec__DisplayClass55_0 cDisplayClass550 = new MultisiteService.\u003C\u003Ec__DisplayClass55_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass550.rootNodeId = rootNodeId;
      Dictionary<Guid, ISet<Guid>> pageIdProperties = new Dictionary<Guid, ISet<Guid>>();
      // ISSUE: reference to a compiler-generated field
      IQueryable<PageNode> source = pManagerInstance.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.RootNodeId == cDisplayClass550.rootNodeId));
      Expression<Func<PageNode, Guid>> selector = (Expression<Func<PageNode, Guid>>) (p => p.Id);
      foreach (Guid guid in (IEnumerable<Guid>) source.Select<PageNode, Guid>(selector))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MultisiteService.\u003C\u003Ec__DisplayClass55_1 cDisplayClass551 = new MultisiteService.\u003C\u003Ec__DisplayClass55_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass551.pageId = guid;
        ParameterExpression parameterExpression;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        Guid[] array = pManagerInstance.GetProperties().Where<ControlProperty>(Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.Equal(p.Value, (Expression) Expression.Call(cDisplayClass551.pageId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())), parameterExpression)).Select<ControlProperty, Guid>((Expression<Func<ControlProperty, Guid>>) (p => p.Id)).ToArray<Guid>();
        if (array.Length != 0)
        {
          // ISSUE: reference to a compiler-generated field
          pageIdProperties[cDisplayClass551.pageId] = (ISet<Guid>) new HashSet<Guid>((IEnumerable<Guid>) array);
        }
      }
      return (IDictionary<Guid, ISet<Guid>>) pageIdProperties;
    }

    private SiteConfigurationViewModel SaveSiteDataSourcesInternal(
      string siteId,
      SiteConfigurationViewModel modelviewSite,
      bool isCreatingSite,
      MultisiteManager multisiteManager = null)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "ConfigureModules");
        Guid siteId1 = new Guid(siteId);
        using (new UnrestrictedModeRegion())
        {
          if (multisiteManager == null)
            multisiteManager = MultisiteManager.GetManager(string.Empty, Guid.NewGuid().ToString());
          return new SiteConfigurationViewModel(this.UpdateDataSources(siteId1, modelviewSite, isCreatingSite, multisiteManager));
        }
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private Site UpdateDataSources(
      Guid siteId,
      SiteConfigurationViewModel modelviewSite,
      bool isCreatingSite,
      MultisiteManager multisiteManager)
    {
      Site persistedSite = (Site) null;
      persistedSite = multisiteManager.GetSite(siteId);
      if (persistedSite == null)
        throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
      if (modelviewSite.DataSources != null)
      {
        SystemManager.DataSourceRegistry.GetDataSources();
        string usersDatasourceName = typeof (UserManager).FullName;
        foreach (SiteDataSourceConfigViewModel dataSource1 in (IEnumerable<SiteDataSourceConfigViewModel>) modelviewSite.DataSources)
        {
          SiteDataSourceConfigViewModel source = dataSource1;
          IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().SingleOrDefault<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == source.Name));
          List<SiteDataSourceLink> list;
          if (source.IsChecked && dataSource != null)
          {
            List<SiteDataSourceLink> updatedLinks = new List<SiteDataSourceLink>();
            foreach (SiteDataSourceLinkViewModel link in (IEnumerable<SiteDataSourceLinkViewModel>) source.Links)
              updatedLinks.Add(MultisiteService.CreateOrUpdateSiteDataSourceLink(siteId, source.Name, link.ProviderName, link.ProviderTitle, new bool?(link.IsDefault), multisiteManager));
            list = persistedSite.SiteDataSourceLinks.Where<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (l => l.DataSource.Name == source.Name && !updatedLinks.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (updated => updated.Id == l.Id)) && !source.Links.Any<SiteDataSourceLinkViewModel>((Func<SiteDataSourceLinkViewModel, bool>) (vl => vl.ProviderName == l.DataSource.Provider)))).ToList<SiteDataSourceLink>();
          }
          else
            list = persistedSite.SiteDataSourceLinks.Where<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (l => l.DataSource.Name == source.Name)).ToList<SiteDataSourceLink>();
          if (source.Name == usersDatasourceName && list.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (p => p.DataSource.Name == usersDatasourceName && p.ProviderName == dataSource.GetDefaultProvider())) && !SystemManager.CurrentContext.MultisiteContext.GetSites().Any<ISite>((Func<ISite, bool>) (s => s.Id != persistedSite.Id && s.SiteDataSourceLinks.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (sdsl => sdsl.DataSourceName == usersDatasourceName && sdsl.ProviderName == dataSource.GetDefaultProvider())))))
            throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().CantUnassignDefaultUsersProviderFromAllSites, (Exception) null);
          foreach (SiteDataSourceLink link in list)
          {
            persistedSite.SiteDataSourceLinks.Remove(link);
            multisiteManager.Delete(link);
          }
        }
      }
      persistedSite.SiteConfigurationMode = modelviewSite.SiteConfigurationMode;
      if (!multisiteManager.TransactionName.IsNullOrEmpty())
        TransactionManager.CommitTransaction(multisiteManager.TransactionName);
      else
        multisiteManager.SaveChanges();
      return persistedSite;
    }

    internal static SiteDataSourceLink CreateOrUpdateSiteDataSourceLink(
      Guid siteId,
      string dataSourceName,
      string providerName = null,
      string dataSourceTitle = null,
      bool? isDefault = null,
      MultisiteManager multisiteManager = null)
    {
      int num = multisiteManager != null ? 1 : 0;
      if (num == 0)
        multisiteManager = MultisiteManager.GetManager();
      Site site = multisiteManager.GetSite(siteId);
      IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().SingleOrDefault<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == dataSourceName));
      if (string.IsNullOrEmpty(dataSourceTitle))
        dataSourceTitle = MultisiteService.GetSiteDataSourceTitle(site.Name, dataSource.Title);
      if (string.IsNullOrWhiteSpace(providerName) || providerName.StartsWith("new-sf-provider-", StringComparison.OrdinalIgnoreCase))
        providerName = MultisiteService.GetProviderName(dataSourceTitle, dataSource);
      SiteDataSourceLink siteDataSourceLink = site.SiteDataSourceLinks.FirstOrDefault<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (l => l.DataSource.Name == dataSourceName && l.DataSource.Provider == providerName));
      if (siteDataSourceLink == null)
      {
        siteDataSourceLink = multisiteManager.CreateSiteDataSourceLink();
        siteDataSourceLink.DataSource = multisiteManager.GetOrCreateDataSource(dataSourceName, providerName, dataSourceTitle);
      }
      if (!isDefault.HasValue && !site.SiteDataSourceLinks.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (dsl => dsl.DataSourceName == dataSourceName && dsl.IsDefault)))
        isDefault = new bool?(true);
      siteDataSourceLink.IsDefault = isDefault.HasValue && isDefault.Value;
      siteDataSourceLink.SiteId = site.Id;
      if (!site.SiteDataSourceLinks.Contains(siteDataSourceLink))
        site.SiteDataSourceLinks.Add(siteDataSourceLink);
      if (num == 0)
        multisiteManager.SaveChanges();
      return siteDataSourceLink;
    }

    private static void DuplicatePageNodesTree(
      PageNode sourceRootNode,
      PageNode destRootNode,
      PageManager pManagerInstance,
      IDictionary<Guid, Guid> correspondingPageId,
      CultureInfo[] cultures)
    {
      foreach (PageNode pageNode1 in sourceRootNode.Nodes.Where<PageNode>((Func<PageNode, bool>) (node => !node.IsDeleted)).ToArray<PageNode>())
      {
        CultureInfo cultureInfo1 = (CultureInfo) null;
        IEnumerable<CultureInfo> source = ((IEnumerable<CultureInfo>) pageNode1.AvailableCultures).Intersect<CultureInfo>((IEnumerable<CultureInfo>) cultures);
        if (source != null)
        {
          if (source.Any<CultureInfo>())
          {
            PageNode pageNode2;
            try
            {
              pageNode2 = pManagerInstance.CreatePageNode();
              correspondingPageId[pageNode1.Id] = pageNode2.Id;
              pManagerInstance.CopyPageNode(pageNode1, pageNode2, (CultureInfo) null, (CultureInfo) null, false, false);
              pageNode2.LocalizationStrategy = pageNode1.LocalizationStrategy;
              LocalizationHelper.ClearLstringProperties((object) pageNode2);
              CultureInfo[] availableLanguages = pageNode1.Title.GetAvailableLanguages();
              LocalizationHelper.CopyLstringProperties<PageNode>(pageNode1, pageNode2, CultureInfo.InvariantCulture);
              if (((IEnumerable<CultureInfo>) cultures).Count<CultureInfo>() > 0)
              {
                foreach (CultureInfo culture in cultures)
                {
                  if (((IEnumerable<CultureInfo>) availableLanguages).Contains<CultureInfo>(culture))
                    LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) pageNode1, (IDynamicFieldsContainer) pageNode2, culture, culture, false, false);
                }
              }
              foreach (PageData pageData in (IEnumerable<PageData>) pageNode1.PageDataList)
              {
                if (pageData != null)
                {
                  string culture = pageData.Culture;
                  if (!string.IsNullOrEmpty(culture) && pageNode1.LocalizationStrategy != LocalizationStrategy.NotSelected)
                  {
                    CultureInfo cultureInfo2 = CultureInfo.GetCultureInfo(culture);
                    if (((IEnumerable<CultureInfo>) cultures).Contains<CultureInfo>(cultureInfo2))
                    {
                      if (cultureInfo1 != null)
                        cultureInfo1 = SystemManager.CurrentContext.Culture;
                      SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(culture);
                    }
                    else
                      continue;
                  }
                  MultisiteService.InitializeDuplicatePage(pManagerInstance, pageNode2, pageData);
                }
              }
              pManagerInstance.ChangeParent(pageNode2, destRootNode, false);
              TransactionManager.CommitTransaction(pManagerInstance.TransactionName);
            }
            finally
            {
              if (cultureInfo1 != null)
                SystemManager.CurrentContext.Culture = cultureInfo1;
            }
            MultisiteService.DuplicatePageNodesTree(pageNode1, pageNode2, pManagerInstance, correspondingPageId, cultures);
          }
        }
      }
    }

    private static void InitializeDuplicatePage(
      PageManager pManagerInstance,
      PageNode duplicateChild,
      PageData sourcePageData)
    {
      string sourceCulture = sourcePageData.Culture;
      PageData targetPage = pManagerInstance.GetPageDataList(duplicateChild).Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Culture == sourceCulture)).FirstOrDefault<PageData>() ?? pManagerInstance.CreatePageData();
      pManagerInstance.CopyPageData(sourcePageData, targetPage, (CultureInfo) null, (CultureInfo) null, true, true, true);
      if (duplicateChild.LocalizationStrategy == LocalizationStrategy.Synced)
      {
        IEnumerable<CultureInfo> cultureInfos = ((IEnumerable<CultureInfo>) duplicateChild.GetAvailableCultures()).Where<CultureInfo>((Func<CultureInfo, bool>) (x => x != CultureInfo.InvariantCulture));
        foreach (PageControl control in (IEnumerable<PageControl>) targetPage.Controls)
        {
          if (control.Strategy == PropertyPersistenceStrategy.Translatable)
          {
            List<ControlProperty> aggregateProps = new List<ControlProperty>();
            foreach (CultureInfo language in cultureInfos)
              aggregateProps.AddRange(control.GetProperties(language));
            foreach (ControlProperty controlProperty in control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !aggregateProps.Contains(x))).ToList<ControlProperty>())
            {
              control.Properties.Remove(controlProperty);
              pManagerInstance.Delete(controlProperty);
            }
          }
        }
      }
      targetPage.Culture = sourceCulture;
      targetPage.Version = 1;
      targetPage.LastControlId = sourcePageData.LastControlId;
      IEnumerable<string> source = sourcePageData.PublishedTranslations.Distinct<string>();
      foreach (LanguageData sourceLanguageData in (IEnumerable<LanguageData>) sourcePageData.LanguageData)
      {
        string language = sourceLanguageData.Language;
        if (language == null || source.Contains<string>(language))
        {
          LanguageData languageData = pManagerInstance.CreateLanguageData();
          languageData.Language = language;
          languageData.CopyFrom(sourceLanguageData);
          targetPage.LanguageData.Add(languageData);
          if (language != null)
            targetPage.PublishedTranslations.Add(language);
        }
      }
      targetPage.NavigationNode = duplicateChild;
    }

    private static void DuplicateSiteItemLinks(
      Guid sourceSiteId,
      Guid targetSiteId,
      string transactionName)
    {
      foreach (Type multisiteEnabledManager in (IEnumerable<Type>) SystemManager.MultisiteEnabledManagers)
      {
        try
        {
          IMultisiteEnabledManager managerInTransaction = (IMultisiteEnabledManager) ManagerBase.GetManagerInTransaction(multisiteEnabledManager, (string) null, transactionName);
          foreach (Type shareableType1 in managerInTransaction.GetShareableTypes())
          {
            Type shareableType = shareableType1;
            IQueryable<SiteItemLink> siteItemLinks = managerInTransaction.GetSiteItemLinks();
            Expression<Func<SiteItemLink, bool>> predicate = (Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == sourceSiteId && l.ItemType == shareableType.FullName);
            foreach (SiteItemLink siteItemLink1 in siteItemLinks.Where<SiteItemLink>(predicate).ToArray<SiteItemLink>())
            {
              SiteItemLink siteItemLink2 = managerInTransaction.CreateSiteItemLink();
              siteItemLink2.ItemId = siteItemLink1.ItemId;
              siteItemLink2.ItemType = siteItemLink1.ItemType;
              siteItemLink2.SiteId = targetSiteId;
              if (managerInTransaction.Provider.GetDirtyItems().Count > 0)
                TransactionManager.CommitTransaction(transactionName);
            }
            TransactionManager.CommitTransaction(transactionName);
          }
        }
        catch (TargetInvocationException ex)
        {
          Log.Write((object) ex);
        }
      }
    }

    private CollectionContext<SiteGridViewModel> LocateInMainMenuInternal(
      string[] ids)
    {
      try
      {
        IEnumerable<Guid> guids = ((IEnumerable<string>) ids).Select<string, Guid>((Func<string, Guid>) (i => new Guid(i)));
        List<Guid> guidList = new List<Guid>();
        IQueryable<Site> allowedSites = this.GetAllowedSites(out MultisiteManager _);
        foreach (Site site in (IEnumerable<Site>) allowedSites)
        {
          this.ValidateAllowedAction((ISecuredObject) site, "AccessSite");
          if (guids.Contains<Guid>(site.Id))
            guidList.Add(site.Id);
        }
        IQueryable<Site> source = allowedSites.Where<Site>((Expression<Func<Site, bool>>) (s => guids.Contains<Guid>(s.Id)));
        UserProfilesHelper.GetUserProfileManager(typeof (SitefinityProfile)).SetPreference<string>(ClaimsManager.GetCurrentUserId(), "mainMenuSites", (object) JsonConvert.SerializeObject((object) guidList));
        ServiceUtility.DisableCache();
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ; //unable to render the statement
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private IQueryable<Site> GetAllowedSites(out MultisiteManager manager)
    {
      manager = MultisiteManager.GetManager();
      IQueryable<Site> source = manager.GetSites();
      UserActivityRecord userActivityRecord = UserActivityManager.GetCurrentUserActivity();
      if (userActivityRecord != null)
        source = source.Where<Site>((Expression<Func<Site, bool>>) (s => userActivityRecord.AllowedSites.Contains(s.Id)));
      return source;
    }

    private CollectionContext<AvailableLinkViewModel> GetSiteDataSourceAvailableLinksInternal(
      string siteId,
      string dataSourceName)
    {
      try
      {
        Guid parsedSiteId = new Guid(siteId);
        if (parsedSiteId == Guid.Empty)
          this.ValidateAllowedAction("AccessSite");
        else
          this.ValidateAllowedAction(siteId, "AccessSite");
        MultisiteContext currentContext = SystemManager.CurrentContext as MultisiteContext;
        IDataSource dataSource1 = SystemManager.DataSourceRegistry.GetDataSources().Single<IDataSource>((Func<IDataSource, bool>) (ds => ds.Name == dataSourceName));
        IEnumerable<DataProviderInfo> providerInfos = dataSource1.ProviderInfos;
        bool isDynamicModule = dataSource1 is DynamicModuleDataSourceProxy;
        Dictionary<string, ISiteDataSource> dictionary = currentContext.GetDataSourcesByName(dataSourceName).ToDictionary<ISiteDataSource, string, ISiteDataSource>((Func<ISiteDataSource, string>) (s => s.Provider), (Func<ISiteDataSource, ISiteDataSource>) (s => s));
        foreach (DataProviderInfo providerInfo in providerInfos)
        {
          IEnumerable<Guid> sites = (IEnumerable<Guid>) null;
          ISiteDataSource siteDataSource;
          if (dictionary.TryGetValue(providerInfo.ProviderName, out siteDataSource))
            sites = siteDataSource.Sites;
          dictionary[providerInfo.ProviderName] = (ISiteDataSource) new MultisiteContext.SiteDataSourceProxy(dataSource1, providerInfo, isDynamicModule, sites);
        }
        List<AvailableLinkViewModel> items = new List<AvailableLinkViewModel>();
        string str = (string) null;
        if (parsedSiteId != Guid.Empty)
          str = SystemManager.CurrentContext.GetSites().FirstOrDefault<ISite>((Func<ISite, bool>) (x => x.Id.Equals(parsedSiteId))).SiteDataSourceLinks.FirstOrDefault<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (x => x.IsDefault && x.DataSourceName.Equals(dataSourceName)))?.ProviderName;
        foreach (ISiteDataSource siteDataSource in (IEnumerable<ISiteDataSource>) dictionary.Values.OrderBy<ISiteDataSource, string>((Func<ISiteDataSource, string>) (s => s.Title)).ThenBy<ISiteDataSource, string>((Func<ISiteDataSource, string>) (s => s.Provider)))
        {
          ISiteDataSource dataSource = siteDataSource;
          SiteDataSourceLinkViewModel sourceLinkViewModel = MultisiteService.GetNewDataSourceLinkViewModel(parsedSiteId, dataSource.Provider, dataSource.Title, dataSource.Name, dataSource.Provider.Equals(str));
          AvailableLinkViewModel availableLinkViewModel = new AvailableLinkViewModel();
          availableLinkViewModel.Link = sourceLinkViewModel;
          string[] array = currentContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => dataSource.Sites.Contains<Guid>(s.Id) && s.Id != parsedSiteId)).Select<ISite, string>((Func<ISite, string>) (s => s.Name)).ToArray<string>();
          availableLinkViewModel.UsedAlsoBy = array;
          availableLinkViewModel.IsDeletable = !providerInfos.Any<DataProviderInfo>((Func<DataProviderInfo, bool>) (x => x.ProviderName.Equals(dataSource.Provider))) && array.Length == 0;
          items.Add(availableLinkViewModel);
        }
        ServiceUtility.DisableCache();
        return new CollectionContext<AvailableLinkViewModel>((IEnumerable<AvailableLinkViewModel>) items)
        {
          TotalCount = items.Count
        };
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private static SiteDataSourceLinkViewModel GetNewDataSourceLinkViewModel(
      Guid parsedSiteId,
      string providerName,
      string providerTitle,
      string dataSourceName,
      bool isDefault)
    {
      return new SiteDataSourceLinkViewModel()
      {
        Id = Guid.Empty,
        IsDefault = isDefault,
        ProviderName = providerName,
        ProviderTitle = string.IsNullOrWhiteSpace(providerTitle) ? providerName : providerTitle,
        SiteId = parsedSiteId,
        IsGlobalProvider = dataSourceName == typeof (UserManager).FullName && SecurityManager.IsGlobalUserProvider(providerName)
      };
    }

    private CollectionContext<DataSourceGridViewModel> GetDataSourcesInternal(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      try
      {
        AppPermission.Root.Demand("Site", "AccessSite");
        IQueryable<IDataSource> source = SystemManager.DataSourceRegistry.GetDataSources().AsQueryable<IDataSource>();
        if (!string.IsNullOrEmpty(filter))
          source = source.Where<IDataSource>(filter);
        if (!string.IsNullOrEmpty(sortExpression))
          source = source.OrderBy<IDataSource>(sortExpression);
        int num = source.Count<IDataSource>();
        if (skip > 0)
          source = source.Skip<IDataSource>(skip);
        if (take > 0)
          source = source.Take<IDataSource>(take);
        ServiceUtility.DisableCache();
        return new CollectionContext<DataSourceGridViewModel>((IEnumerable<DataSourceGridViewModel>) source.Select<IDataSource, DataSourceGridViewModel>((Expression<Func<IDataSource, DataSourceGridViewModel>>) (d => new DataSourceGridViewModel(d))))
        {
          TotalCount = num
        };
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private SiteConfigurationViewModel GetSiteConfigurationInternal(
      string siteId)
    {
      try
      {
        this.ValidateAllowedAction(siteId, "ConfigureModules");
        Guid siteId1 = new Guid(siteId);
        Site site = MultisiteManager.GetManager().GetSite(siteId1);
        SiteConfigurationViewModel configurationInternal = site != null ? new SiteConfigurationViewModel(site) : throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound);
        configurationInternal.DataSources = this.GetSiteSourcesConfigurationInternal(site.Name, site);
        ServiceUtility.DisableCache();
        return configurationInternal;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private static SiteDataSourceLinkViewModel CreateSampleLink(
      string providerName,
      string providerTitle,
      Guid siteId,
      string siteName,
      IDataSource dataSource)
    {
      return new SiteDataSourceLinkViewModel()
      {
        Id = Guid.Empty,
        SiteId = siteId,
        IsDefault = true,
        DataSourceName = dataSource.Name,
        ProviderName = providerName,
        ProviderTitle = providerTitle
      };
    }

    private static string GetProviderName(string providerTitle, IDataSource dataSource) => ManagerExtensions.GenerateProviderName(dataSource, providerTitle);

    private static string GetSiteDataSourceTitle(string siteName, string dataSourceTitle) => siteName + " " + dataSourceTitle;

    private ICollection<SiteDataSourceConfigViewModel> GetSiteSourcesConfigurationInternal(
      string siteName,
      Site site = null)
    {
      List<SiteDataSourceConfigViewModel> configurationInternal = new List<SiteDataSourceConfigViewModel>();
      IEnumerable<IDataSource> dataSources = SystemManager.DataSourceRegistry.GetDataSources();
      MultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext as MultisiteContext;
      foreach (IDataSource dataSource1 in dataSources)
      {
        IDataSource dataSource = dataSource1;
        SiteDataSourceConfigViewModel sourceConfigViewModel = new SiteDataSourceConfigViewModel();
        sourceConfigViewModel.Name = dataSource.Name;
        sourceConfigViewModel.Title = dataSource.Title;
        sourceConfigViewModel.DependantDataSources = dataSource.DependantDataSources;
        sourceConfigViewModel.AllowMultipleProviders = !(dataSource is IMultisiteDataSource multisiteDataSource) || multisiteDataSource.AllowMultipleProviders;
        string providerTitle = siteName + " " + dataSource.Title;
        IEnumerable<string> existringProviderNames = multisiteContext.GetDataSourcesByName(dataSource.Name).Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Sites.Any<Guid>())).Select<ISiteDataSource, string>((Func<ISiteDataSource, string>) (ds => ds.Provider));
        string providerName = ManagerExtensions.GenerateProviderName(providerTitle, existringProviderNames);
        sourceConfigViewModel.SampleLink = MultisiteService.CreateSampleLink(providerName, providerTitle, site != null ? site.Id : Guid.Empty, siteName, dataSource);
        if (site != null)
        {
          foreach (SiteDataSourceLink siteDataSourceLink in (IEnumerable<SiteDataSourceLink>) site.SiteDataSourceLinks.Where<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (l => l.DataSource.Name == dataSource.Name)).ToArray<SiteDataSourceLink>())
          {
            SiteDataSourceLinkViewModel sourceLinkViewModel = (SiteDataSourceLinkViewModel) null;
            try
            {
              sourceLinkViewModel = new SiteDataSourceLinkViewModel(siteDataSourceLink);
            }
            catch (InvalidOperationException ex)
            {
              MultisiteManager manager = MultisiteManager.GetManager();
              site.SiteDataSourceLinks.Remove(siteDataSourceLink);
              manager.Delete(siteDataSourceLink);
              manager.SaveChanges();
            }
            if (sourceLinkViewModel != null)
              sourceConfigViewModel.Links.Add(sourceLinkViewModel);
          }
        }
        else if (dataSource.Name == typeof (LibrariesManager).FullName)
          sourceConfigViewModel.Links.Add(sourceConfigViewModel.SampleLink);
        if (sourceConfigViewModel.Links.Count == 0 && dataSource.Name == typeof (UserManager).FullName)
        {
          foreach (DataProviderInfo provider in dataSource.Providers)
          {
            SiteDataSourceLinkViewModel sourceLinkViewModel = new SiteDataSourceLinkViewModel()
            {
              Id = Guid.Empty,
              SiteId = site != null ? site.Id : Guid.Empty,
              IsDefault = provider.ProviderName.Equals(dataSource.GetDefaultProvider()),
              DataSourceName = dataSource.Name,
              ProviderName = provider.ProviderName,
              ProviderTitle = provider.ProviderTitle,
              IsGlobalProvider = SecurityManager.IsGlobalUserProvider(provider.ProviderName)
            };
            sourceConfigViewModel.Links.Add(sourceLinkViewModel);
          }
        }
        sourceConfigViewModel.IsChecked = (uint) sourceConfigViewModel.Links.Count > 0U;
        if (sourceConfigViewModel.Links.Count == 0)
        {
          DataProviderInfo dataProviderInfo = dataSource.Providers.FirstOrDefault<DataProviderInfo>((Func<DataProviderInfo, bool>) (p =>
          {
            if (string.IsNullOrWhiteSpace(p.ProviderTitle))
              return false;
            return p.ProviderTitle.Equals(providerTitle, StringComparison.OrdinalIgnoreCase) || p.ProviderTitle.Equals(siteName, StringComparison.OrdinalIgnoreCase);
          }));
          if (dataProviderInfo != null)
          {
            SiteDataSourceLinkViewModel sourceLinkViewModel = new SiteDataSourceLinkViewModel()
            {
              Id = Guid.Empty,
              SiteId = site != null ? site.Id : Guid.Empty,
              IsDefault = true,
              DataSourceName = dataSource.Name,
              ProviderName = dataProviderInfo.ProviderName,
              ProviderTitle = dataProviderInfo.ProviderTitle
            };
            sourceConfigViewModel.Links.Add(sourceLinkViewModel);
          }
        }
        configurationInternal.Add(sourceConfigViewModel);
      }
      ServiceUtility.DisableCache();
      return (ICollection<SiteDataSourceConfigViewModel>) configurationInternal;
    }

    private bool DeleteDataSourceInternal(string managerName, string providerName)
    {
      this.ValidateAllowedAction("ConfigureModules");
      MultisiteManager manager = MultisiteManager.GetManager();
      SiteDataSource link = manager.GetDataSources().Where<SiteDataSource>((Expression<Func<SiteDataSource, bool>>) (ds => ds.Name == managerName && ds.Provider == providerName)).FirstOrDefault<SiteDataSource>();
      if (link == null)
        return false;
      manager.Delete(link);
      manager.SaveChanges();
      return true;
    }

    private bool EnableDataSourceInternal(string managerName, string providerName)
    {
      MultisiteManager manager = MultisiteManager.GetManager();
      manager.GetDataSources().Where<SiteDataSource>((Expression<Func<SiteDataSource, bool>>) (ds => ds.Name == managerName && ds.Provider == providerName)).FirstOrDefault<SiteDataSource>();
      manager.GetOrCreateDataSource(managerName, providerName, providerName);
      manager.SaveChanges();
      return true;
    }

    private bool DisableDataSourceInternal(string managerName, string providerName) => this.DeleteDataSource(managerName, providerName);

    private IEnumerable<SiteDataSourceConfigViewModel> GetNewSiteSourcesConfigurationInternal(
      string siteName)
    {
      try
      {
        this.ValidateAllowedAction("CreateEditSite");
        return (IEnumerable<SiteDataSourceConfigViewModel>) this.GetSiteSourcesConfigurationInternal(siteName);
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
        throw;
      }
    }

    private void ValidateAllowedAction(string action)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!this.IsCurrentUserAllowedAction(action))
        throw new UnauthorizedAccessException(AuthorizationPermissionProvider.GetDetailedErrorMessage("Site", new string[1]
        {
          action
        }));
    }

    private void ValidateAllowedAction(string siteId, string action)
    {
      Guid result;
      Guid.TryParse(siteId, out result);
      this.ValidateAllowedAction((ISecuredObject) (MultisiteManager.GetManager().GetSite(result) ?? throw WebProtocolException.NotFound(Res.Get<MultisiteResources>().NotFound)), action);
    }

    private void ValidateAllowedAction(ISecuredObject site, string action)
    {
      if (site == null)
        throw new ArgumentNullException("Site cannot be null.");
      ServiceUtility.RequestBackendUserAuthentication();
      if (!this.IsCurrentUserAllowedAction(site, action))
        throw new UnauthorizedAccessException(AuthorizationPermissionProvider.GetDetailedErrorMessage("Site", new string[1]
        {
          action
        }));
    }

    private bool IsCurrentUserAllowedAction(string action) => MultisiteManager.GetManager().SecurityRoot.IsGranted("Site", action);

    private bool IsCurrentUserAllowedAction(ISecuredObject site, string action) => site.IsGranted("Site", action);

    private string[] GetCultureDisplayNames(IList<string> cultureKeys)
    {
      List<string> stringList = new List<string>(cultureKeys.Count);
      if (cultureKeys != null)
      {
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        foreach (string cultureKey in (IEnumerable<string>) cultureKeys)
        {
          CultureElement cultureElement;
          if (resourcesConfig.Cultures.TryGetValue(cultureKey, out cultureElement))
            stringList.Add(cultureElement.DisplayName);
        }
      }
      return stringList.ToArray();
    }

    private void HandleException(Exception ex)
    {
      if (ex is BadRequestException)
        throw WebProtocolException.BadRequest(ex.Message, ex);
      if (ex is ConflictException || ex is DuplicateUrlException)
        throw WebProtocolException.Conflict(ex.Message, ex);
      if (ex is UnauthorizedAccessException)
        this.ThrowWebProtocolException(ex, ex.Message);
      if (ex is SecurityDemandFailException)
      {
        string detailedErrorMessage = AuthorizationPermissionProvider.GetDetailedErrorMessage("Site", new string[1]
        {
          "CreateEditSite"
        });
        this.ThrowWebProtocolException(ex, detailedErrorMessage);
      }
      if (ex is ItemNotFoundException)
        throw WebProtocolException.NotFound(ex.Message);
      throw ex;
    }

    private void ThrowWebProtocolException(Exception ex, string errorMessage)
    {
      if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Request.IsAuthenticated)
        throw WebProtocolException.Forbidden(errorMessage, ex);
      throw WebProtocolException.Unauthorized(errorMessage, ex);
    }
  }
}
