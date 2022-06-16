// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Events;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Manages the data layer of Sitefinity's publishing system
  /// </summary>
  public class PublishingManager : 
    ManagerBase<PublishingDataProviderBase>,
    IMultisiteEnabledManager,
    IManager,
    IDisposable,
    IProviderResolver
  {
    private const string PublishingPointKeyNamePrefix = "PublishingPoint-";
    private static readonly TimeSpan PublishingPointSiteCacheExpiration = TimeSpan.FromMinutes(5.0);
    private static readonly object publishingPointSiteCacheLock = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    public PublishingManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public PublishingManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public PublishingManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<PublishingConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public override string ModuleName => "Publishing";

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<PublishingConfig>().ProviderSettings;

    /// <summary>
    /// Get an instance of the publishing manager using the default provider
    /// </summary>
    /// <returns>Instance of publishing manager</returns>
    public static PublishingManager GetManager() => ManagerBase<PublishingDataProviderBase>.GetManager<PublishingManager>();

    /// <summary>
    /// Get an instance of the publishing manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the publishing manager</returns>
    public static PublishingManager GetManager(string providerName) => ManagerBase<PublishingDataProviderBase>.GetManager<PublishingManager>(providerName);

    /// <summary>Get publishing manager in named transaction</summary>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="transactionName">Name of the transaction</param>
    /// <returns>Manager in named transaction. Don't use SaveChanges with it, use TransactionManager.CommitTransaction, instead.</returns>
    public static PublishingManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<PublishingDataProviderBase>.GetManager<PublishingManager>(providerName, transactionName);
    }

    /// <summary>Create a publishing point AND its dynamic type</summary>
    /// <param name="providerName">Name of the publishing provider to use</param>
    /// <param name="id">ID of the publishing point</param>
    /// <returns>Publishing point</returns>
    public static IPublishingPoint CreatePublishingPointAndDynamicType(
      string providerName,
      Guid id)
    {
      PublishingPointDynamicTypeManager.CreateDynamicType((IPublishingPoint) PublishingManager.GetManager(providerName).CreatePublishingPoint(id));
      return (IPublishingPoint) PublishingManager.GetManager(providerName).GetPublishingPoint(id);
    }

    /// <summary>
    /// Get the feeds absolute base url, as specified in the configuration
    /// </summary>
    /// <returns>Absolute feeds base url</returns>
    public static string GetFeedsBaseURl()
    {
      string url = string.Format("~/{0}", (object) Config.Get<PublishingConfig>().FeedsBaseURl);
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      return currentSite != null ? new SiteUrlResolver(new Uri(currentSite.GetUri().GetLeftPart(UriPartial.Authority))).ResolveUrl(url) : RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);
    }

    /// <summary>
    /// Get provider name from query string using by looking it up with a configurable key
    /// </summary>
    /// <returns>Name of the publishing provider specified in the request's query string</returns>
    public static string GetProviderNameFromQueryString() => SystemManager.CurrentHttpContext.Request.QueryString[Config.Get<PublishingConfig>().ProviderNameQueryKey];

    /// <summary>
    /// Create a new publishing point and associated it with a random ID
    /// </summary>
    /// <returns>Publishing point in transaction.</returns>
    public PublishingPoint CreatePublishingPoint() => this.Provider.CreatePublishingPoint();

    /// <summary>
    /// Create a new publishing point by explicitly specifying its ID
    /// </summary>
    /// <param name="id">ID of the publishing point that is to be created.</param>
    /// <returns>Publishing point in transaction.</returns>
    public PublishingPoint CreatePublishingPoint(Guid id) => this.Provider.CreatePublishingPoint(id);

    /// <summary>
    /// Mark a publishing point for deleteion. You have to use a global transaction with this method.
    /// </summary>
    /// <param name="publishingPoint">Publishing point to be deleted when the transaction is commited.</param>
    public void DeletePublishingPoint(PublishingPoint publishingPoint)
    {
      if (string.IsNullOrEmpty(this.TransactionName))
        throw new InvalidOperationException("transaction name can not be null when you call DeletePublishingPoint method");
      MetadataManager manager = MetadataManager.GetManager(string.Empty, this.Provider.TransactionName);
      if (publishingPoint.StorageTypeName != null)
      {
        PublishingExtensionMethods.FullTypeName dynamicTypeName = publishingPoint.GetDynamicTypeName();
        MetaType metaType = manager.GetMetaType(dynamicTypeName.Namespace, dynamicTypeName.ClassName);
        if (metaType != null)
          manager.Delete(metaType);
      }
      this.Provider.DeletePublishingPoint(publishingPoint);
    }

    /// <summary>Get a specific publishing point by its ID</summary>
    /// <param name="publishingPointID">ID of the publishing point to retrieve</param>
    /// <returns>Instance of the publishing point with the specified ID</returns>
    /// <exception cref="!:InvalidDataException">
    /// When a publishing point with the specified ID cannot be found.
    /// </exception>
    public PublishingPoint GetPublishingPoint(Guid publishingPointID) => this.Provider.GetPublishingPoint(publishingPointID);

    /// <summary>Retrieve all publishing points as a query</summary>
    /// <returns>Query of all publishing points.</returns>
    public IQueryable<PublishingPoint> GetPublishingPoints() => (IQueryable<PublishingPoint>) this.Provider.GetPublishingPoints().OrderBy<PublishingPoint, DateTime>((Expression<Func<PublishingPoint, DateTime>>) (p => p.DateCreated));

    /// <summary>
    /// Deletes the pipes that are pointing from the Forum/Blog publishing points to the Feed
    /// </summary>
    /// <param name="contentId">The connected content to the feed</param>
    internal void DeleteRssFeedPipe(Guid contentId)
    {
      IQueryable<SitefinityContentPipeSettings> pipeSettings = this.GetPipeSettings<SitefinityContentPipeSettings>();
      Expression<Func<SitefinityContentPipeSettings, bool>> predicate = (Expression<Func<SitefinityContentPipeSettings, bool>>) (ps => ps.ContentLinks.Contains(contentId));
      foreach (SitefinityContentPipeSettings contentPipeSettings in (IEnumerable<SitefinityContentPipeSettings>) pipeSettings.Where<SitefinityContentPipeSettings>(predicate))
      {
        PublishingPoint publishingPoint = contentPipeSettings.PublishingPoint;
        this.DeletePipeSettings((PipeSettings) contentPipeSettings);
        publishingPoint.PipeSettings.Remove((PipeSettings) contentPipeSettings);
        if (!((IEnumerable<IPipe>) publishingPoint.GetInboundPipes()).Any<IPipe>())
          this.DeletePublishingPoint(publishingPoint);
      }
    }

    /// <summary>Gets the manager for publishing point items.</summary>
    /// <returns></returns>
    public PublishingPointDynamicTypeManager GetDynamicTypeManager() => this.GetDynamicTypeManager(string.Empty);

    /// <summary>Gets the manager for publishing point items.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public PublishingPointDynamicTypeManager GetDynamicTypeManager(
      string providerName)
    {
      return PublishingPointDynamicTypeManager.GetManager(providerName, this.Provider.TransactionName);
    }

    /// <summary>Gets the manager for publishing point items.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <returns></returns>
    public PublishingPointDynamicTypeManager GetDynamicTypeManager(
      IPublishingPoint publishingPoint)
    {
      return this.GetDynamicTypeManager(publishingPoint.StorageItemsProvider);
    }

    /// <summary>Saves the changes.</summary>
    public override void SaveChanges()
    {
      if (this.TransactionName != null)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().GlobalTransactionUsed);
      if (!string.IsNullOrEmpty(this.Provider.TransactionName))
        return;
      base.SaveChanges();
    }

    /// <summary>Retrieve the pipe settins for a specific pipe</summary>
    /// <param name="pipeName">Name of the pipe whose settings to look for</param>
    /// <returns>Settins for a paipe with <paramref name="pipeName" /></returns>
    public PipeSettings GetPipeSettings(string pipeName) => this.Provider.GetPipeSettings(pipeName);

    /// <summary>Gets all pipe settings.</summary>
    /// <returns></returns>
    public IQueryable<PipeSettings> GetPipeSettings() => (IQueryable<PipeSettings>) this.Provider.GetPipeSettings().OrderBy<PipeSettings, DateTime>((Expression<Func<PipeSettings, DateTime>>) (p => p.LastModified));

    /// <summary>Gets pipe settings .</summary>
    /// <returns></returns>
    public IQueryable<TPipeSettings> GetPipeSettings<TPipeSettings>() where TPipeSettings : PipeSettings => (IQueryable<TPipeSettings>) this.Provider.GetPipeSettings<TPipeSettings>().OrderBy<TPipeSettings, DateTime>((Expression<Func<TPipeSettings, DateTime>>) (p => p.LastModified));

    /// <summary>Creates the pipe settings.</summary>
    /// <param name="settingsType">Type of the settings.</param>
    /// <returns></returns>
    public PipeSettings CreatePipeSettings(Type settingsType) => this.Provider.CreatePipeSettings(settingsType);

    /// <summary>Creates the pipe settings.</summary>
    /// <typeparam name="TPipeSettings">The type of the pipe settings.</typeparam>
    /// <returns></returns>
    public TPipeSettings CreatePipeSettings<TPipeSettings>() where TPipeSettings : PipeSettings => this.Provider.CreatePipeSettings<TPipeSettings>();

    public void DeletePipeSettings(PipeSettings pipeSettings) => this.Provider.DeletePipeSettings(pipeSettings);

    /// <summary>Creates the mapping settings.</summary>
    /// <returns></returns>
    public MappingSettings CreateMappingSettings() => this.Provider.CreateMappingSettings();

    /// <summary>Creates the mapping.</summary>
    /// <returns></returns>
    public Mapping CreateMapping() => this.Provider.CreateMapping();

    /// <summary>
    /// Mark <paramref name="toDelete" /> for deletion
    /// </summary>
    /// <param name="toDelete">Mapping to mark for deletion</param>
    public void DeleteMapping(Mapping toDelete) => this.Provider.DeleteMapping(toDelete);

    /// <summary>
    /// Mark <paramref name="toDelete" /> for deletion
    /// </summary>
    /// <param name="toDelete">Mapping settings to mark for deletion</param>
    public void DeleteMappingSettings(MappingSettings toDelete) => this.Provider.DeleteMappingSettings(toDelete);

    /// <summary>Create a mapping translator with random ID</summary>
    /// <returns>Newly created item which is added to the transaction</returns>
    public PipeMappingTranslation CreatePipeMappingTranslation() => this.Provider.CreatePipeMappingTranslation();

    /// <summary>Create a mapping translator settings with specific ID</summary>
    /// <param name="id">ID of the new item</param>
    /// <returns>Newly created item which is added to the transaction</returns>
    public PipeMappingTranslation CreatePipeMappingTranslation(Guid id) => this.Provider.CreatePipeMappingTranslation(id);

    /// <summary>Get a mapping translator settings by ID</summary>
    /// <param name="id">ID of the mapping translator settings to retrieve</param>
    /// <returns>Instance from data source or exception</returns>
    public PipeMappingTranslation GetPipeMappingTranslation(Guid id) => this.Provider.GetPipeMappingTranslation(id);

    /// <summary>
    /// Retrieves all mapping translator settings for this provider
    /// </summary>
    /// <returns>Query of all items in the current provider</returns>
    public IQueryable<PipeMappingTranslation> GetPipeMappingTranslations() => this.Provider.GetPipeMappingTranslations();

    /// <summary>
    /// Removes <paramref name="toDelete" /> from transaction
    /// </summary>
    /// <param name="toDelete">Item to delete</param>
    public void DeletePipeMappingTranslation(PipeMappingTranslation toDelete) => this.Provider.DeletePipeMappingTranslation(toDelete);

    public static void CallSubscribedPipes(IEnumerable<PublishingSystemEventInfo> args)
    {
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in args)
      {
        PublishingSystemEventInfo item = publishingSystemEventInfo;
        PublishingManager.CallSubscribedPipes((Func<PublishingItemFilter, PublishingSystemEventInfo>) (filter => item));
      }
    }

    internal static void CallSubscribedPipes(
      Func<PublishingItemFilter, PublishingSystemEventInfo> eventInfoFactory)
    {
      foreach (DataProviderSettings providerSetting in (ConfigElementCollection) Config.Get<PublishingConfig>().ProviderSettings)
      {
        IQueryable<PublishingPoint> publishingPoints = PublishingManager.GetManager(providerSetting.Name).GetPublishingPoints();
        Expression<Func<PublishingPoint, bool>> predicate = (Expression<Func<PublishingPoint, bool>>) (p => p.IsActive);
        foreach (PublishingPoint publishingPoint in (IEnumerable<PublishingPoint>) publishingPoints.Where<PublishingPoint>(predicate))
        {
          IEnumerable<PipeSettings> source = publishingPoint.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (s => s.IsInbound && s.IsActive && s.InvocationMode == PipeInvokationMode.Push));
          if (source.Any<PipeSettings>())
          {
            PublishingSystemEventInfo publishingSystemEventInfo = eventInfoFactory(publishingPoint.Settings.ItemFilterStrategy);
            if (publishingSystemEventInfo != null)
            {
              foreach (PipeSettings pipe1 in source)
              {
                try
                {
                  if (PublishingSystemFactory.IsPipeRegistered(pipe1.PipeName))
                  {
                    IPipe pipe2 = PublishingSystemFactory.GetPipe(pipe1);
                    if (pipe2.CanProcessItem((object) publishingSystemEventInfo))
                      ((IPushPipe) pipe2).PushData((IList<PublishingSystemEventInfo>) new List<PublishingSystemEventInfo>()
                      {
                        publishingSystemEventInfo
                      });
                  }
                }
                catch (Exception ex)
                {
                  if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                    throw ex;
                }
              }
            }
          }
        }
      }
    }

    internal static PublishingSystemEventInfo GetPublishingSystemEventInfo(
      IDataEvent dataEvent,
      PublishingItemFilter filterMode)
    {
      switch (filterMode)
      {
        case PublishingItemFilter.Live:
          if (dataEvent.Action == SecurityConstants.TransactionActionType.Deleted.ToString())
            return dataEvent.ToPubSysEventInfo();
          ILifecycleEvent lifecycleEvent = dataEvent as ILifecycleEvent;
          bool flag1 = dataEvent is IApprovalWorkflowEvent approvalWorkflowEvent && approvalWorkflowEvent.ApprovalWorkflowState == OperationStatus.Unpublished.ToString();
          if (((lifecycleEvent == null || string.IsNullOrEmpty(lifecycleEvent.Status) ? 1 : (lifecycleEvent.Status == "Live" ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
          {
            SecurityConstants.TransactionActionType transactionActionType = (SecurityConstants.TransactionActionType) Enum.Parse(typeof (SecurityConstants.TransactionActionType), dataEvent.Action);
            bool flag2 = approvalWorkflowEvent == null && lifecycleEvent != null && !lifecycleEvent.Visible;
            if (flag1 | flag2)
              transactionActionType = SecurityConstants.TransactionActionType.Deleted;
            return dataEvent.ToPubSysEventInfo(new SecurityConstants.TransactionActionType?(transactionActionType));
          }
          break;
        case PublishingItemFilter.All:
          return dataEvent.ToPubSysEventInfo();
      }
      return (PublishingSystemEventInfo) null;
    }

    internal static PublishingSystemEventInfo GetPublishingSystemEventInfo(
      ConfigEvent configEvent)
    {
      return new PublishingSystemEventInfo()
      {
        Item = (object) ConfigManager.GetManager().GetSection(configEvent.ConfigName),
        ItemAction = configEvent.Action
      };
    }

    /// <summary>
    /// For every registered provider, active publishing point and active pipe, call ToPublishingPoint
    /// </summary>
    public static void FlushAllPipes()
    {
      foreach (DataProviderSettings providerSetting in (ConfigElementCollection) Config.Get<PublishingConfig>().ProviderSettings)
      {
        try
        {
          PublishingManager.FlushAllPipesInProvider(providerSetting.Name);
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
    }

    /// <summary>
    /// For every active publishing point and active pipe, call ToPublishingPoint
    /// </summary>
    /// <param name="providerName">Name of the provider to use</param>
    public static void FlushAllPipesInProvider(string providerName)
    {
      PublishingManager manager = PublishingManager.GetManager(providerName);
      IQueryable<PublishingPoint> publishingPoints = manager.GetPublishingPoints();
      Expression<Func<PublishingPoint, bool>> predicate = (Expression<Func<PublishingPoint, bool>>) (pp => pp.IsActive == true);
      foreach (PublishingPoint publishingPoint in (IEnumerable<PublishingPoint>) publishingPoints.Where<PublishingPoint>(predicate))
      {
        try
        {
          PublishingManager.FlushAllPipesInPublishingPoint(manager);
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
    }

    /// <summary>
    /// For every active pipe in the provider, call ToPublishingPoint
    /// </summary>
    /// <param name="manager">Manager with resolved provider</param>
    public static void FlushAllPipesInPublishingPoint(PublishingManager manager)
    {
      IQueryable<PipeSettings> pipeSettings1 = manager.GetPipeSettings();
      Expression<Func<PipeSettings, bool>> predicate = (Expression<Func<PipeSettings, bool>>) (ps => ps.IsActive == true);
      foreach (PipeSettings pipeSettings2 in (IEnumerable<PipeSettings>) pipeSettings1.Where<PipeSettings>(predicate))
      {
        try
        {
          PublishingManager.FlushPipe(pipeSettings2);
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
    }

    /// <summary>Wrap the ToPublishingPoint call</summary>
    /// <param name="pipeSettings">Active pipe settings to use</param>
    private static void FlushPipe(PipeSettings pipeSettings)
    {
      IPipe pipe = PublishingSystemFactory.GetPipe(pipeSettings.PipeName);
      if (!(pipe is IPushPipe) || !(pipe is IInboundPipe))
        return;
      pipe.Initialize(pipeSettings);
      ((IInboundPipe) pipe).ToPublishingPoint();
    }

    /// <summary>Invokes the inbound push pipes.</summary>
    /// <param name="publishingPointId">The publishing point id.</param>
    /// <param name="providerName">Name of the provider.</param>
    public static void InvokeInboundPushPipes(Guid publishingPointId, string providerName) => PublishingManager.InvokeInboundPushPipes(publishingPointId, providerName, (Action<int, int, PipeSettings>) null, (Predicate<PipeSettings>) null);

    /// <summary>Invokes the inbound push pipes.</summary>
    /// <param name="publishingPointName">The publishing point name.</param>
    /// <param name="providerName">Name of the provider.</param>
    public static void InvokeInboundPushPipes(string publishingPointName, string providerName) => PublishingManager.InvokeInboundPushPipes(PublishingManager.GetManager(providerName).GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (pp => pp.Name == publishingPointName)).FirstOrDefault<PublishingPoint>() ?? throw new ArgumentNullException("Publishing point with name '{0}' can not be found."), (Action<int, int, PipeSettings>) null, (Predicate<PipeSettings>) null);

    /// <summary>Invokes the inbound push pipes.</summary>
    /// <param name="publishingPointId">The publishing point id</param>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="action">The action that will be invoked after each pipe is processed</param>
    /// <param name="predicate">The predicate, used to validate if the current pipe settings should be processed</param>
    internal static void InvokeInboundPushPipes(
      Guid publishingPointId,
      string providerName,
      Action<int, int, PipeSettings> action,
      Predicate<PipeSettings> predicate)
    {
      PublishingManager.InvokeInboundPushPipes(PublishingManager.GetManager(providerName, Guid.NewGuid().ToString()).GetPublishingPoint(publishingPointId), action, predicate);
    }

    /// <summary>Invokes the inbound push pipes.</summary>
    /// <param name="point">The publishing point</param>
    /// <param name="action">The action that will be invoked before each pipe is processed</param>
    /// <param name="predicate">The predicate, used to validate if the current pipe settings should be processed</param>
    private static void InvokeInboundPushPipes(
      PublishingPoint point,
      Action<int, int, PipeSettings> action,
      Predicate<PipeSettings> predicate)
    {
      List<PipeSettings> list = point.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (s => s.IsInbound && s.IsActive)).ToList<PipeSettings>();
      for (int index = 0; index < list.Count; ++index)
      {
        if (PublishingSystemFactory.IsPipeRegistered(list[index].PipeName) && (predicate == null || predicate(list[index])))
        {
          IPipe pipe = PublishingSystemFactory.GetPipe(list[index].PipeName);
          if (pipe is IPushPipe && pipe is IInboundPipe)
          {
            if (action != null)
              action(index, list.Count, list[index]);
            pipe.Initialize(list[index]);
            try
            {
              ((IInboundPipe) pipe).ToPublishingPoint();
            }
            catch (Exception ex)
            {
              Log.Error("Failed invoking ToPublishingPoint() of " + list[index].PipeName + ". Exception message: " + ex.Message, (object) ConfigurationPolicy.ErrorLog);
            }
          }
        }
      }
    }

    private static DateTime GetNextDay(DayOfWeek day)
    {
      DateTime nextDay = DateTime.UtcNow.Date;
      while (nextDay.DayOfWeek != day)
        nextDay = nextDay.AddDays(1.0);
      return nextDay;
    }

    /// <summary>Reschedules the publishing point pipes.</summary>
    /// <param name="point">The point.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public static void ReschedulePublishingPointPipes(PublishingPoint point, string transactionName)
    {
      IEnumerable<PipeSettings> pipeSettingses = point.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (pipe => pipe.IsInbound));
      SchedulingManager manager = SchedulingManager.GetManager(string.Empty, transactionName);
      foreach (PipeSettings pipeSettings in pipeSettingses)
      {
        string pipeIdTemplate = "pipeId=\"" + pipeSettings.Id.ToString().ToLower() + "\"";
        IQueryable<ScheduledTaskData> taskData = manager.GetTaskData();
        Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (p => p.TaskName == "PublishingSystemInvokerTask" && p.TaskData.Contains(pipeIdTemplate) && p.IsRunning == false);
        foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
          manager.DeleteItem((object) scheduledTaskData);
        if (pipeSettings.ScheduleType != 0)
        {
          DateTime? scheduleTime = pipeSettings.ScheduleTime;
          InboundPipeInvokeTask task = new InboundPipeInvokeTask();
          task.PipeSettingsId = pipeSettings.Id;
          task.SiteId = SystemManager.CurrentContext.CurrentSite.Id;
          switch (pipeSettings.ScheduleType)
          {
            case 1:
              task.ExecuteTime = DateTime.UtcNow.AddMinutes(5.0);
              break;
            case 2:
              task.ExecuteTime = DateTime.UtcNow.AddMinutes(30.0);
              break;
            case 3:
              task.ExecuteTime = DateTime.UtcNow.AddHours(1.0);
              break;
            case 4:
              DateTime dateTime1 = DateTime.UtcNow.Date;
              if (scheduleTime.HasValue)
              {
                DateTime utc = PublishingManager.ConvertToUtc(scheduleTime.Value);
                if (utc < DateTime.UtcNow)
                  dateTime1 = dateTime1.AddDays(1.0);
                dateTime1 = dateTime1.AddMinutes((double) utc.Minute);
                dateTime1 = dateTime1.AddSeconds((double) utc.Second);
                dateTime1 = dateTime1.AddMilliseconds((double) utc.Millisecond);
                dateTime1 = dateTime1.AddHours((double) utc.Hour);
              }
              task.ExecuteTime = dateTime1;
              break;
            case 5:
              DateTime dateTime2 = PublishingManager.GetNextDay((DayOfWeek) pipeSettings.ScheduleDay);
              if (scheduleTime.HasValue)
              {
                DateTime utc = PublishingManager.ConvertToUtc(scheduleTime.Value);
                if (utc < DateTime.UtcNow)
                  dateTime2 = dateTime2.AddDays(7.0);
                dateTime2 = dateTime2.AddMinutes((double) utc.Minute);
                dateTime2 = dateTime2.AddSeconds((double) utc.Second);
                dateTime2 = dateTime2.AddMilliseconds((double) utc.Millisecond);
                dateTime2 = dateTime2.AddHours((double) utc.Hour);
              }
              task.ExecuteTime = dateTime2;
              break;
            default:
              continue;
          }
          manager.AddTask((ScheduledTask) task);
        }
      }
      if (!string.IsNullOrEmpty(transactionName))
        return;
      manager.SaveChanges();
    }

    private static DateTime ConvertToUtc(DateTime source)
    {
      TimeZoneInfo local = TimeZoneInfo.Local;
      return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(source, DateTimeKind.Unspecified), local);
    }

    Type[] IMultisiteEnabledManager.GetShareableTypes() => new Type[0];

    Type[] IMultisiteEnabledManager.GetSiteSpecificTypes() => new Type[1]
    {
      typeof (PublishingPoint)
    };

    /// <summary>Gets the sites that use the given publishing point.</summary>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    internal static IEnumerable<SiteItemLink> GetSitesByPoint(
      PublishingPoint point)
    {
      object provider = ((IDataItem) point).Provider;
      if (provider == null)
        throw new ArgumentException("The passed publishing point does not have provider.");
      if (!(provider is IMultisiteEnabledProvider))
        return (IEnumerable<SiteItemLink>) new List<SiteItemLink>(0);
      return (IEnumerable<SiteItemLink>) ((IMultisiteEnabledProvider) provider).GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == point.Id));
    }

    /// <summary>
    /// Gets the sites that use the given publishing point from a cache.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    internal static IList<Guid> GetSitesByPointFromCache(PublishingPoint point)
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      string key = "PublishingPoint-" + (object) point.Id;
      IList<Guid> byPointFromCache = (IList<Guid>) cacheManager[key];
      if (byPointFromCache == null)
      {
        lock (PublishingManager.publishingPointSiteCacheLock)
        {
          byPointFromCache = (IList<Guid>) cacheManager[key];
          if (byPointFromCache == null)
          {
            byPointFromCache = !point.IsSharedWithAllSites ? (IList<Guid>) PublishingManager.GetSitesByPoint(point).Select<SiteItemLink, Guid>((Func<SiteItemLink, Guid>) (l => l.SiteId)).ToList<Guid>() : (IList<Guid>) SystemManager.CurrentContext.GetSites().Select<ISite, Guid>((Func<ISite, Guid>) (s => s.Id)).ToList<Guid>();
            cacheManager.Add(key, (object) byPointFromCache, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Site), (string) null), (ICacheItemExpiration) new SlidingTime(PublishingManager.PublishingPointSiteCacheExpiration));
          }
        }
      }
      return byPointFromCache;
    }

    internal static void RemovePointFromCache(PublishingPoint point)
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      string key = "PublishingPoint-" + (object) point.Id;
      lock (PublishingManager.publishingPointSiteCacheLock)
        cacheManager.Remove(key);
    }
  }
}
