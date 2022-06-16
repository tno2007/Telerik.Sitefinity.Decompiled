// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling.Configuration;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack;
using Telerik.Sitefinity.Scheduling.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>API entry point for the scheduling sub-system</summary>
  public class SchedulingModule : ModuleBase, IStatusProvider
  {
    /// <summary>Name of the scheduling module</summary>
    public static readonly string ModuleName = "Scheduling";
    /// <summary>
    /// Name of the default resource class for the scheduling sub-system
    /// </summary>
    public static readonly string ResourceClassId = typeof (SchedulingResources).Name;
    private static readonly Type[] managerTypes = new Type[1]
    {
      typeof (SchedulingManager)
    };
    internal const string SchedulingServiceUrl = "Sitefinity/Services/SchedulingService.svc";
    internal const string TaskStatusesScript = "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskStatuses.js";
    internal const string TaskCommandsScript = "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.TaskCommands.js";
    internal const string SchedulingManagementScript = "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.SchedulingManagement.js";
    internal const string PageName = "SchedulingManagementPage";
    private readonly object statusCacheSync = new object();
    internal static readonly Guid PageId = new Guid("513d06b4-6f15-4dc8-9fb3-d077f11ddf87");

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => SchedulingModule.PageId;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => SchedulingModule.managerTypes;

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartup;

    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().WebService<Telerik.Sitefinity.Scheduling.Web.Services.SchedulingService>("Sitefinity/Services/SchedulingService.svc").SitemapFilter<SchedulingModuleNodeFilter>().ServiceStackPlugin((IPlugin) new SchedulingServiceStackPlugin()).Localization<SchedulingResources>();
      SystemManager.StatusProviderRegistry.RegisterProvider<IStatusProvider>((IStatusProvider) this);
      ObjectFactory.Container.RegisterType<ISystemStatusRetriever, SchedulingSystemStatusRetreiver>("SchedulingSystemStatusRetreiver");
    }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer) => this.InstallBackendPage(initializer);

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (!(upgradeFrom < SitefinityVersion.Sitefinity11_0))
        return;
      IQueryable<ScheduledTaskData> taskData = initializer.GetManagerInTransaction<SchedulingManager>().GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.Key.StartsWith("WorkflowCallTask|"));
      foreach (ScheduledTaskData data in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
      {
        if (SchedulingTaskFactory.ResolveTask(data) is WorkflowCallTask workflowCallTask)
        {
          if (workflowCallTask.ContentType == typeof (PageNode))
            workflowCallTask.ContentProviderName = initializer.PageManager.Provider.Name;
          if (workflowCallTask.OperationName == "ScheduledPublish")
            workflowCallTask.OperationName = "Publish";
          data.Key = workflowCallTask.BuildUniqueKey();
        }
      }
    }

    /// <summary>Gets the module config.</summary>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<SchedulingConfig>();

    string IStatusProvider.Name => SchedulingModule.ModuleName;

    StatusBehaviour IStatusProvider.Behaviour => StatusBehaviour.Workflow;

    int IStatusProvider.Priority => 10;

    bool IStatusProvider.IsTypeSupported(Type itemType) => !(itemType.FullName == "Telerik.Sitefinity.Blogs.Model.Blog") && !(itemType.FullName == "Telerik.Sitefinity.Lists.Model.List") && !(itemType.FullName == "Telerik.Sitefinity.Events.Model.Calendar") && typeof (IScheduleable).IsAssignableFrom(itemType);

    IItemStatusData IStatusProvider.GetItem(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid id)
    {
      string languageKey = culture.GetLanguageKey();
      SchedulingModule.SchedulingStatusData schedulingStatusData;
      return this.GetPendingScheduling(itemType, itemProvider, languageKey, rootKey).TryGetValue(id, out schedulingStatusData) ? (IItemStatusData) schedulingStatusData : (IItemStatusData) null;
    }

    IEnumerable<IItemStatusData> IStatusProvider.GetItems(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid[] ids)
    {
      string languageKey = culture.GetLanguageKey();
      return (IEnumerable<IItemStatusData>) this.GetPendingScheduling(itemType, itemProvider, languageKey, rootKey).Where<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>>((Func<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, bool>) (i => ((IEnumerable<Guid>) ids).Contains<Guid>(i.Key))).Select<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, SchedulingModule.SchedulingStatusData>((Func<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, SchedulingModule.SchedulingStatusData>) (i => i.Value));
    }

    IEnumerable<IItemStatusData> IStatusProvider.GetItemsByFilter(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      string filterName)
    {
      if (!(filterName == "Scheduled"))
        return (IEnumerable<IItemStatusData>) new IItemStatusData[0];
      string languageKey = culture.GetLanguageKey();
      return (IEnumerable<IItemStatusData>) this.GetPendingScheduling(itemType, itemProvider, languageKey, rootKey).Where<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>>((Func<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, bool>) (i => i.Value.PublicationDate.HasValue)).Select<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, SchedulingModule.SchedulingStatusData>((Func<KeyValuePair<Guid, SchedulingModule.SchedulingStatusData>, SchedulingModule.SchedulingStatusData>) (i => i.Value));
    }

    IEnumerable<IStatusFilter> IStatusProvider.GetFilters()
    {
      StatusFilter statusFilter = new StatusFilter("Scheduled", "Scheduled");
      StatusFilterCollection filters = new StatusFilterCollection(SchedulingModule.ModuleName);
      filters.Add((IStatusFilter) statusFilter);
      return (IEnumerable<IStatusFilter>) filters;
    }

    IWarningData IStatusProvider.GetWarning(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid id)
    {
      return (IWarningData) null;
    }

    private IDictionary<Guid, SchedulingModule.SchedulingStatusData> GetPendingScheduling(
      Type itemType,
      string itemProvider,
      string cultureName,
      string rootKey = null)
    {
      string scopeKey = WorkflowCallTask.BuildScopeKey(itemType.FullName, itemProvider, cultureName, rootKey);
      string fullName = itemType.FullName;
      ICacheManager cacheManager = this.GetCacheManager();
      if (!(cacheManager[scopeKey] is IDictionary<Guid, SchedulingModule.SchedulingStatusData> pendingScheduling1))
      {
        lock (this.statusCacheSync)
        {
          if (!(cacheManager[scopeKey] is IDictionary<Guid, SchedulingModule.SchedulingStatusData> pendingScheduling1))
          {
            pendingScheduling1 = (IDictionary<Guid, SchedulingModule.SchedulingStatusData>) new Dictionary<Guid, SchedulingModule.SchedulingStatusData>();
            IQueryable<ScheduledTaskData> taskData = SchedulingManager.GetManager().GetTaskData();
            Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.Key.StartsWith(scopeKey) && !t.IsRunning && (int) t.Status == 0);
            foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
            {
              string operationName;
              Guid guid = WorkflowCallTask.ResolveItemIdFromKey(scheduledTaskData.Key, scopeKey.Length + 1, out operationName);
              SchedulingModule.SchedulingStatusData schedulingStatusData;
              if (!pendingScheduling1.TryGetValue(guid, out schedulingStatusData))
              {
                schedulingStatusData = new SchedulingModule.SchedulingStatusData(guid);
                pendingScheduling1.Add(guid, schedulingStatusData);
              }
              if (((IEnumerable<string>) new string[2]
              {
                "Publish",
                "ScheduledPublish"
              }).Contains<string>(operationName))
                schedulingStatusData.PublicationDate = new DateTime?(scheduledTaskData.ExecuteTime);
              else
                schedulingStatusData.ExpirationDate = new DateTime?(scheduledTaskData.ExecuteTime);
            }
            cacheManager.Add(scopeKey, (object) pendingScheduling1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (ScheduledTaskData), scopeKey), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return pendingScheduling1;
    }

    private ICacheManager GetCacheManager() => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    [UpgradeInfo(Description = "Installs backend page for the Scheduling module", FailMassage = "Failed to install backend page for the Scheduling module.", Id = "94057ff4-6e9a-4576-a15c-bacf720242af", UpgradeTo = 7600)]
    private void InstallSchedulingModuleBackendPage(SiteInitializer initializer) => this.InstallBackendPage(initializer);

    private void InstallBackendPage(SiteInitializer initializer) => initializer.Installer.CreateModulePage(SchedulingModule.PageId, "SchedulingManagementPage").PlaceUnder(SiteInitializer.ToolsNodeId).SetTitleLocalized<SchedulingResources>("SchedulingManagementPageTitle").SetHtmlTitleLocalized<SchedulingResources>("SchedulingManagementPageTitle").SetUrlNameLocalized<SchedulingResources>("SchedulingManagementPageUrlName").AddControl((Control) new SchedulingManagement()).Done();

    private class SchedulingStatusData : IItemStatusData, ISchedulingStatus
    {
      private IDictionary<string, string> parameters;
      private string operation;

      public SchedulingStatusData(Guid itemId)
      {
        this.ItemId = itemId;
        this.parameters = this.parameters;
      }

      public Guid ItemId { get; private set; }

      public string Status => this.PublicationDate.HasValue ? "Scheduled" : string.Empty;

      public string GetTextFormat(out object[] arguments)
      {
        arguments = (object[]) null;
        return this.Status;
      }

      public DateTime? PublicationDate { get; internal set; }

      public DateTime? ExpirationDate { get; internal set; }
    }
  }
}
