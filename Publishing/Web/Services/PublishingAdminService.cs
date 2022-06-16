// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.PublishingAdminService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.PublishingPoints;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Publishing.Web.Services
{
  /// <summary>
  /// Web service facilitating the backend administration of Publishing points - creation, modification, deletion
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class PublishingAdminService : IPublishingAdminService
  {
    protected const string ContentTypeFilter = "ContentType";
    protected const string ReplacementString = "-";
    private const string AdvancedSettingsIndexId = "23e8cc23-715a-4393-87ba-d723f7cdf71d";

    protected void RequireAuthorization(string providerName, string action)
    {
      ISecuredObject securityRoot = PublishingManager.GetManager(providerName).Provider.GetSecurityRoot();
      if (securityRoot == null)
        return;
      if (!securityRoot.IsGranted("General", action))
        throw new WebProtocolException(HttpStatusCode.Forbidden, "Request denied", (Exception) null);
    }

    /// <summary>
    /// Gets the list of publishing points persisted in the system.
    /// </summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip number(linq).</param>
    /// <param name="take">The take number(linq)</param>
    /// <param name="filter">The dynamic linq expression -&gt; filter.</param>
    /// <returns>
    /// a list of publishing points - with limited data like title, owner, and date of creation
    /// </returns>
    public virtual CollectionContext<PublishingPointViewModel> GetPublishingPoints(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "View");
      IQueryable<PublishingPoint> queryable = this.LoadPublishingPoints(PublishingManager.GetManager(providerName));
      if (!string.IsNullOrEmpty(filter))
        queryable = this.ProcessPublishingPointsFilter(queryable, filter);
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<PublishingPoint>(sortExpression);
      int num = queryable.Count<PublishingPoint>();
      if (skip != -1)
        queryable = queryable.Skip<PublishingPoint>(skip);
      if (take != -1)
        queryable = queryable.Take<PublishingPoint>(take);
      List<PublishingPointViewModel> items = new List<PublishingPointViewModel>();
      foreach (PublishingPoint publishingPoint in (IEnumerable<PublishingPoint>) queryable)
      {
        PublishingPointViewModel publishingPointViewModel = new PublishingPointViewModel();
        publishingPointViewModel.IsActive = publishingPoint.IsActive;
        publishingPointViewModel.IsBackend = publishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All;
        publishingPointViewModel.Title = publishingPoint.Name;
        publishingPointViewModel.Id = publishingPoint.Id;
        publishingPointViewModel.LastPublicationDate = publishingPoint.LastPublicationDate;
        publishingPointViewModel.DateCreated = publishingPoint.DateCreated;
        Guid owner = publishingPoint.Owner;
        publishingPointViewModel.Owner = UserProfilesHelper.GetUserDisplayName(publishingPoint.Owner);
        foreach (PipeSettings settings in publishingPoint.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (p => !p.IsInbound)))
          publishingPointViewModel.OutputPipes.Add(this.GetPipeShortData(settings));
        items.Add(publishingPointViewModel);
      }
      CollectionContext<PublishingPointViewModel> publishingPoints = new CollectionContext<PublishingPointViewModel>((IEnumerable<PublishingPointViewModel>) items);
      publishingPoints.TotalCount = num;
      this.DisableServiceCache();
      return publishingPoints;
    }

    /// <summary>Gets the publishing point and applies authorization.</summary>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">if set to <c>true</c> the method will return a blank publishing point(not persisted)</param>
    /// <returns>
    /// a specific publishing point or a blank(new) publishing point
    /// </returns>
    public virtual DataItemContext<PublishingPointDetailViewModel> GetPublishingPoint(
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "View");
      this.ProcessSystemTemplates(pointId, ref itemTemplate);
      DataItemContext<PublishingPointDetailViewModel> publishingPointViewModel = this.GetPublishingPointViewModel(PublishingManager.GetManager(providerName), providerName, pointId, createNew, itemTemplate);
      this.DisableServiceCache();
      return publishingPointViewModel;
    }

    /// <summary>
    /// Saves the publishing point and applies authorization..
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">need for compatiblity with the get method</param>
    /// <returns>the saved publishing point</returns>
    public virtual DataItemContext<PublishingPointDetailViewModel> SavePublishingPoint(
      DataItemContext<PublishingPointDetailViewModel> point,
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "Modify");
      this.ProcessSystemTemplates(pointId, ref itemTemplate);
      string transactionName = "SavePublishingPoint_" + (object) Guid.NewGuid();
      try
      {
        PublishingManager manager1 = PublishingManager.GetManager(providerName, transactionName);
        MetadataManager manager2 = MetadataManager.GetManager(string.Empty, transactionName);
        bool isNewlyCreated;
        PublishingPoint publishingPoint = this.SavePublishingPointFromViewModel(point, manager1, manager2, pointId, createNew, itemTemplate, out isNewlyCreated);
        point.Item.Id = publishingPoint.Id;
        this.ReschedulePublishingPointPipes(publishingPoint, transactionName);
        TransactionManager.CommitTransaction(transactionName);
        if (isNewlyCreated && manager1.Provider.Name == "SearchPublishingProvider")
        {
          IPipe pipe = ((IEnumerable<IPipe>) publishingPoint.GetOutboundPipes()).FirstOrDefault<IPipe>();
          if (pipe != null && pipe.PipeSettings is SearchIndexPipeSettings pipeSettings)
          {
            string catalogName = pipeSettings.CatalogName;
            IEnumerable<IFieldDefinition> indexDefinitions = PublishingAdminService.GetIndexDefinitions((PipeSettings) pipeSettings);
            SearchIndexCreatingEvent indexCreatingEvent = new SearchIndexCreatingEvent();
            indexCreatingEvent.Name = catalogName;
            indexCreatingEvent.Definitions = indexDefinitions;
            EventHub.Raise((IEvent) indexCreatingEvent);
            IEnumerable<IFieldDefinition> definitions = indexCreatingEvent.Definitions;
            if (this.GetService().IndexExists(catalogName))
              throw new InvalidOperationException(string.Format("Search index with name {0} already exists. Please choose a different name.", (object) catalogName));
            this.GetService().CreateIndex(catalogName, definitions);
            EventHub.Raise((IEvent) new SearchIndexCreatedEvent()
            {
              Name = catalogName
            });
          }
        }
        PublishingManager.RemovePointFromCache(publishingPoint);
        SchedulingManager.RescheduleNextRun();
        this.DisableServiceCache();
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        throw new Exception(SitefinityExtensions.RemoveCRLF(ex.Message));
      }
      return point;
    }

    /// <summary>Reschedules the publishing point pipes.</summary>
    /// <param name="point">The point.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public virtual void ReschedulePublishingPointPipes(
      PublishingPoint point,
      string transactionName)
    {
      PublishingManager.ReschedulePublishingPointPipes(point, transactionName);
    }

    private void ThrowDuplicateUrlException(string url) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<PublishingMessages>().DuplicateUrlException, (object) url), (Exception) null);

    /// <summary>Deletes a publishing point.</summary>
    /// <param name="pointId">The point id.</param>
    /// <returns>true on success</returns>
    public virtual bool DeletePublishingPoint(
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "Delete");
      string transactionName = "DeletePublishingPointTransaction" + Guid.NewGuid().ToString();
      PublishingManager manager = PublishingManager.GetManager(providerName, transactionName);
      PublishingPoint publishingPoint = manager.GetPublishingPoint(new Guid(pointId));
      manager.DeletePublishingPoint(publishingPoint);
      TransactionManager.CommitTransaction(transactionName);
      this.DeleteExternalData((IManager) manager, publishingPoint);
      this.DisableServiceCache();
      return true;
    }

    /// <summary>Batches the delete points.</summary>
    /// <param name="Ids">The ids.</param>
    /// <returns></returns>
    public virtual bool BatchDeletePoints(string providerName, string[] Ids)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "Delete");
      string transactionName = "BackDeletePublishingPoints" + Guid.NewGuid().ToString();
      PublishingManager manager = PublishingManager.GetManager(providerName, transactionName);
      List<string> source = new List<string>();
      foreach (string id in Ids)
      {
        PublishingPoint publishingPoint = manager.GetPublishingPoint(new Guid(id));
        if (publishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All)
        {
          source.Add(publishingPoint.Name);
        }
        else
        {
          manager.DeletePublishingPoint(publishingPoint);
          this.DeleteExternalData((IManager) manager, publishingPoint);
        }
      }
      TransactionManager.CommitTransaction(transactionName);
      this.DisableServiceCache();
      string str = source.FirstOrDefault<string>();
      if (str != null && providerName == "SearchPublishingProvider")
        throw new InvalidOperationException(Res.Get("SearchResources", "SystemIndexCannotBeDeletedFormat").Arrange((object) str));
      return true;
    }

    public virtual CollectionContext<PublishingPipeViewModel> GetInboundPublishingPipes(
      string providerName,
      string pipeTypeName,
      string sort,
      string filter,
      int skip,
      int take)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "View");
      List<PublishingPipeViewModel> items = new List<PublishingPipeViewModel>();
      IQueryable<PipeSettings> source1 = PublishingManager.GetManager(providerName).GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.PipeName == pipeTypeName));
      int num = source1.Count<PipeSettings>();
      IQueryable<PipeSettings> source2 = source1.Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.IsActive == true && p.IsInbound == true));
      if (!string.IsNullOrEmpty(filter))
        source2 = source2.Where<PipeSettings>(filter);
      if (sort != null)
        source2 = source2.OrderBy<PipeSettings>(sort);
      if (skip > 0)
        source2 = source2.Skip<PipeSettings>(skip);
      if (take > 0)
        source2 = source2.Take<PipeSettings>(take);
      foreach (PipeSettings pipeSettings in (IEnumerable<PipeSettings>) source2)
        items.Add(new PublishingPipeViewModel()
        {
          ID = pipeSettings.Id,
          Title = !string.IsNullOrEmpty(pipeSettings.Title) ? pipeSettings.Title : pipeSettings.PublishingPoint.Name
        });
      CollectionContext<PublishingPipeViewModel> inboundPublishingPipes = new CollectionContext<PublishingPipeViewModel>((IEnumerable<PublishingPipeViewModel>) items);
      inboundPublishingPipes.TotalCount = num;
      ServiceUtility.DisableCache();
      return inboundPublishingPipes;
    }

    /// <summary>
    /// Gets a collection context of outgoing publishing pipes.
    /// </summary>
    /// <param name="pipeTypeName">Name of the pipe type.</param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>A collection context of publishing pipes.</returns>
    public virtual CollectionContext<PublishingPipeViewModel> GetOutgoingPublishingPipes(
      string providerName,
      string pipeTypeName,
      string sort,
      string filter,
      int skip,
      int take)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "View");
      List<PublishingPipeViewModel> items = new List<PublishingPipeViewModel>();
      PublishingManager manager = PublishingManager.GetManager(providerName);
      IQueryable<PipeSettings> source1 = manager.GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.PipeName == pipeTypeName));
      int num = source1.Count<PipeSettings>();
      IQueryable<PipeSettings> source2 = source1.Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.IsActive == true && p.IsInbound == false));
      Guid siteId = SystemManager.CurrentContext.CurrentSite.Id;
      List<Guid> pointLinks = manager.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId)).ToList<Guid>();
      IQueryable<PipeSettings> source3 = source2.Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.PublishingPoint.IsSharedWithAllSites || pointLinks.Contains(p.PublishingPoint.Id)));
      if (!string.IsNullOrEmpty(filter))
        source3 = source3.Where<PipeSettings>(filter);
      if (sort != null)
        source3 = source3.OrderBy<PipeSettings>(sort);
      IEnumerable<PipeSettings> source4 = source3.AsEnumerable<PipeSettings>().Where<PipeSettings>((Func<PipeSettings, bool>) (p => p.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live));
      if (skip > 0)
        source4 = source4.Skip<PipeSettings>(skip);
      if (take > 0)
        source4 = source4.Take<PipeSettings>(take);
      foreach (PipeSettings pipeSettings in source4)
        items.Add(new PublishingPipeViewModel()
        {
          ID = pipeSettings.Id,
          Title = !string.IsNullOrEmpty(pipeSettings.Title) ? pipeSettings.Title : pipeSettings.PublishingPoint.Name
        });
      CollectionContext<PublishingPipeViewModel> outgoingPublishingPipes = new CollectionContext<PublishingPipeViewModel>((IEnumerable<PublishingPipeViewModel>) items);
      outgoingPublishingPipes.TotalCount = num;
      this.DisableServiceCache();
      return outgoingPublishingPipes;
    }

    /// <summary>
    /// Activates or deactivates a single publishing point.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pointId">The id of the publishing point to be changed.</param>
    /// <param name="setActive">if set to <c>true</c> the point is activated; otherwise deactivated.</param>
    /// public bool DeleteContent(string pointId, bool setActive)
    public virtual bool SetActive(string providerName, string pointId, bool setActive)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "Modify");
      PublishingManager manager = PublishingManager.GetManager();
      manager.GetPublishingPoint(new Guid(pointId)).IsActive = setActive;
      manager.SaveChanges();
      return true;
    }

    /// <summary>
    /// Activates or deactivates an array of publishing points.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array containing the Ids of the publishing points to be deleted.</param>
    /// <param name="setActive">if set to <c>true</c> the points are activated; otherwise deactivated.</param>
    public virtual bool BatchSetActive(string providerName, string[] ids, bool setActive)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.RequireAuthorization(providerName, "Modify");
      PublishingManager manager = PublishingManager.GetManager(providerName);
      foreach (string id in ids)
        manager.GetPublishingPoint(new Guid(id)).IsActive = setActive;
      manager.SaveChanges();
      return true;
    }

    /// <summary>Runs the input pipes of the publishing point</summary>
    /// <param name="pipeID">The publishing point Id.</param>
    /// <returns></returns>
    public virtual bool RunPipes(string providerName, string pointId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PublishingManager.InvokeInboundPushPipes(new Guid(pointId), providerName);
      return true;
    }

    /// <summary>Runs the input pipes of the publishing point</summary>
    /// <param name="pipeID">The publishing point Id.</param>
    /// <returns></returns>
    public virtual bool ReindexSearchContent(string providerName, string pointId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!SystemManager.IsModuleEnabled("Publishing"))
        throw new InvalidOperationException("Publishing module is disabled!");
      SchedulingManager manager = SchedulingManager.GetManager();
      IQueryable<ScheduledTaskData> taskData = manager.GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == ReindexTask.Name);
      foreach (ScheduledTaskData task in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
      {
        ReindexTaskSettings reindexTaskSettings = ReindexTaskSettings.Parse(task.TaskData);
        if (reindexTaskSettings.PublishingPointId == new Guid(pointId) && reindexTaskSettings.PublishingPointProvider == providerName)
        {
          if (task.Status != TaskStatus.Failed)
            throw new InvalidOperationException("Reindex already in progress");
          manager.DeleteTaskData(task);
          break;
        }
      }
      Guid guid = SystemManager.CurrentContext.MultisiteContext != null ? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id : Guid.Empty;
      PublishingPoint publishingPoint = PublishingManager.GetManager(providerName).GetPublishingPoint(Guid.Parse(pointId));
      ReindexTask reindexTask = new ReindexTask();
      reindexTask.Id = Guid.NewGuid();
      reindexTask.Title = publishingPoint != null ? publishingPoint.GetTitle((CultureInfo) null) : string.Empty;
      reindexTask.Settings = new ReindexTaskSettings()
      {
        PublishingPointId = new Guid(pointId),
        PublishingPointProvider = providerName,
        SiteId = guid
      };
      reindexTask.ExecuteTime = DateTime.UtcNow;
      ReindexTask task1 = reindexTask;
      manager.AddTask((ScheduledTask) task1);
      manager.SaveChanges();
      return true;
    }

    /// <inheritdoc />
    public Dictionary<Guid, ReindexStatusDto> GetReindexStatus()
    {
      IQueryable<ScheduledTaskData> queryable = SchedulingManager.GetManager(string.Empty, Guid.NewGuid().ToString()).GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == ReindexTask.Name && (int) t.Status != 2));
      Dictionary<Guid, ReindexStatusDto> reindexStatus = new Dictionary<Guid, ReindexStatusDto>();
      string str1 = Res.Get("SearchResources", "Indexing");
      foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) queryable)
      {
        ReindexTaskSettings reindexTaskSettings = ReindexTaskSettings.Parse(scheduledTaskData.TaskData);
        string str2 = scheduledTaskData.StatusMessage.IsNullOrEmpty() ? str1 : scheduledTaskData.StatusMessage;
        reindexStatus[reindexTaskSettings.PublishingPointId] = new ReindexStatusDto()
        {
          Progress = scheduledTaskData.Status == TaskStatus.Pending ? -1 : scheduledTaskData.Progress,
          StatusMessage = str2
        };
      }
      return reindexStatus;
    }

    /// <summary>Gets the publishing point view model.</summary>
    /// <param name="pointId">The point id.</param>
    /// <param name="manager">The pubslihing manager</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="itemTemplate">The item template</param>
    /// <param name="createNew">if set to <c>true</c> the method will return a blank publishing point(not persisted)</param>
    /// <returns>
    /// a specific publishing point or a blank(new) publishing point
    /// </returns>
    public DataItemContext<PublishingPointDetailViewModel> GetPublishingPointViewModel(
      PublishingManager manager,
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate)
    {
      PublishingPointDetailViewModel pointDetailViewModel;
      if (!createNew)
      {
        Guid publishingPointID = new Guid(pointId);
        PublishingPoint publishingPoint = manager.GetPublishingPoint(publishingPointID);
        if (!string.IsNullOrEmpty(publishingPoint.InboundPipesTemplate))
          itemTemplate = publishingPoint.InboundPipesTemplate;
        pointDetailViewModel = !string.IsNullOrEmpty(itemTemplate) ? new PublishingPointDetailViewModel(publishingPoint, providerName, itemTemplate) : new PublishingPointDetailViewModel(publishingPoint, providerName);
      }
      else
        pointDetailViewModel = new PublishingPointDetailViewModel(itemTemplate, providerName);
      string[] fieldNamesToHide = new string[3]
      {
        "Id",
        "ApplicationName",
        "LastModified"
      };
      foreach (SimpleDefinitionField simpleDefinitionField in pointDetailViewModel.PublishingPointDefinition.Where<SimpleDefinitionField>((Func<SimpleDefinitionField, bool>) (f => ((IEnumerable<string>) fieldNamesToHide).Contains<string>(f.Name))))
        simpleDefinitionField.HideInUI = true;
      pointDetailViewModel.PublishingPointDefinition.Sort((IComparer<SimpleDefinitionField>) DefinitionFieldComparer.TitleComparer);
      return new DataItemContext<PublishingPointDetailViewModel>()
      {
        Item = pointDetailViewModel,
        ItemType = pointDetailViewModel.GetType().FullName
      };
    }

    /// <summary>Saves the publishing point from the view model.</summary>
    /// <param name="point">The point.</param>
    /// <param name="manager">The publishing manager</param>
    /// <param name="metadataManager">The metadata manager</param>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">need for compatiblity with the get method</param>
    /// <param name="itemTemplate">The item template</param>
    /// <param name="isNewlyCreated">True, is the index is created for the first time</param>
    /// <returns>the saved publishing point</returns>
    public PublishingPoint SavePublishingPointFromViewModel(
      DataItemContext<PublishingPointDetailViewModel> point,
      PublishingManager manager,
      MetadataManager metadataManager,
      string pointId,
      bool createNew,
      string itemTemplate,
      out bool isNewlyCreated)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PublishingAdminService.\u003C\u003Ec__DisplayClass16_0 cDisplayClass160 = new PublishingAdminService.\u003C\u003Ec__DisplayClass16_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass160.wcfItem = point.Item;
      isNewlyCreated = false;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass160.itemId = point.Item.Id;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      if (this.LoadPublishingPoints(manager, true).Where<PublishingPoint>(Expression.Lambda<Func<PublishingPoint, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.Call(ppoint.Name, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass160, typeof (PublishingAdminService.\u003C\u003Ec__DisplayClass16_0)), FieldInfo.GetFieldFromHandle(__fieldref (PublishingAdminService.\u003C\u003Ec__DisplayClass16_0.wcfItem))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PublishingPointDetailViewModel.get_Title))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>())), (Expression) Expression.NotEqual((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PublishingPoint.get_Id))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass160, typeof (PublishingAdminService.\u003C\u003Ec__DisplayClass16_0)), FieldInfo.GetFieldFromHandle(__fieldref (PublishingAdminService.\u003C\u003Ec__DisplayClass16_0.itemId))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Inequality)))), parameterExpression)).FirstOrDefault<PublishingPoint>() != null)
        throw new ArgumentException(!(manager.Provider.Name == "SearchPublishingProvider") ? Res.Get<PublishingMessages>().DuplicateFeedTitle : Res.Get("SearchResources", "DuplicateSearchIndexTitle"));
      PublishingPoint publishingPoint;
      if (point.Item.Id == Guid.Empty | createNew)
      {
        isNewlyCreated = true;
        publishingPoint = string.IsNullOrEmpty(pointId) || !(pointId != Guid.Empty.ToString()) ? manager.CreatePublishingPoint() : manager.CreatePublishingPoint(new Guid(pointId));
        PublishingPointFactory.CreatePublishingPointDataItem((IEnumerable<IDefinitionField>) point.Item.PublishingPointDefinition, (IPublishingPoint) publishingPoint, metadataManager);
      }
      else
      {
        publishingPoint = manager.GetPublishingPoint(point.Item.Id);
        if (point.Item.PublishingPointBusinessObjectName == "Persistent")
          isNewlyCreated = PublishingPointFactory.UpdatePublishingPointDataItem((IEnumerable<IDefinitionField>) point.Item.PublishingPointDefinition, (IPublishingPoint) publishingPoint, metadataManager);
      }
      // ISSUE: reference to a compiler-generated field
      publishingPoint.Name = cDisplayClass160.wcfItem.Title;
      // ISSUE: reference to a compiler-generated field
      publishingPoint.Description = (Lstring) cDisplayClass160.wcfItem.Description;
      // ISSUE: reference to a compiler-generated field
      publishingPoint.IsActive = cDisplayClass160.wcfItem.IsActive;
      // ISSUE: reference to a compiler-generated field
      publishingPoint.PublishingPointBusinessObjectName = cDisplayClass160.wcfItem.PublishingPointBusinessObjectName;
      publishingPoint.IsSharedWithAllSites = point.Item.IsSharedWithAllSites;
      List<WcfPipeSettings> settings = new List<WcfPipeSettings>();
      // ISSUE: reference to a compiler-generated field
      settings.AddRange((IEnumerable<WcfPipeSettings>) cDisplayClass160.wcfItem.InboundSettings);
      // ISSUE: reference to a compiler-generated field
      settings.AddRange((IEnumerable<WcfPipeSettings>) cDisplayClass160.wcfItem.OutboundSettings);
      this.ClearDeletedSettings(publishingPoint, settings, manager);
      // ISSUE: reference to a compiler-generated field
      this.FillPipeSettings(publishingPoint, cDisplayClass160.wcfItem.InboundSettings, true, manager);
      // ISSUE: reference to a compiler-generated field
      this.FillPipeSettings(publishingPoint, cDisplayClass160.wcfItem.OutboundSettings, false, manager);
      // ISSUE: reference to a compiler-generated field
      this.ProcessAdditionalFields(publishingPoint, cDisplayClass160.wcfItem);
      IQueryable<RssPipeSettings> pipeSettings1 = manager.GetPipeSettings<RssPipeSettings>();
      foreach (PipeSettings pipeSettings2 in publishingPoint.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.GetType().FullName == typeof (RssPipeSettings).FullName)))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PublishingAdminService.\u003C\u003Ec__DisplayClass16_1 cDisplayClass161 = new PublishingAdminService.\u003C\u003Ec__DisplayClass16_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass161.currentId = pipeSettings2.Id;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass161.pipeSettingsUrl = ((RssPipeSettings) pipeSettings2).UrlName;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (pipeSettings1.Where<RssPipeSettings>((Expression<Func<RssPipeSettings, bool>>) (ps => ps.IsInbound == false && ps.UrlName == cDisplayClass161.pipeSettingsUrl && ps.Id != cDisplayClass161.currentId)).Any<RssPipeSettings>())
          this.ThrowDuplicateUrlException(((RssPipeSettings) pipeSettings2).UrlName);
      }
      if (!publishingPoint.IsSharedWithAllSites)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PublishingAdminService.\u003C\u003Ec__DisplayClass16_2 cDisplayClass162 = new PublishingAdminService.\u003C\u003Ec__DisplayClass16_2();
        manager.Provider.FlushTransaction();
        List<SiteItemLink> list = PublishingManager.GetSitesByPoint(publishingPoint).ToList<SiteItemLink>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass162.currentSite = SystemManager.CurrentContext.CurrentSite.Id;
        // ISSUE: reference to a compiler-generated method
        if (!list.Any<SiteItemLink>(new Func<SiteItemLink, bool>(cDisplayClass162.\u003CSavePublishingPointFromViewModel\u003Eb__3)))
        {
          // ISSUE: reference to a compiler-generated field
          manager.AddItemLink(cDisplayClass162.currentSite, (IDataItem) publishingPoint);
        }
        foreach (SiteItemLink link in list)
        {
          // ISSUE: reference to a compiler-generated field
          if (link.SiteId != cDisplayClass162.currentSite)
            manager.Delete(link);
        }
      }
      return publishingPoint;
    }

    /// <summary>
    /// For now the template is changed dynamically if the incoming publishing point is one of the sytem points.
    /// In the future the template of the point should be stored as a part of the publishing point in the DB.
    /// </summary>
    private void ProcessSystemTemplates(string pointId, ref string itemTemplate) => itemTemplate = pointId != "23e8cc23-715a-4393-87ba-d723f7cdf71d" ? itemTemplate : "AdvancedSettingsSearchTemplate";

    internal static IEnumerable<IFieldDefinition> GetIndexDefinitions(
      PipeSettings settings)
    {
      IEnumerable<IFieldDefinition> first = settings.Mappings.Mappings.Select<Mapping, IFieldDefinition>((Func<Mapping, IFieldDefinition>) (m =>
      {
        Type type = m.Type == null ? (Type) null : TypeResolutionService.ResolveType(m.Type);
        IFieldDefinition indexDefinitions = ObjectFactory.Resolve<IFieldDefinition>();
        indexDefinitions.Name = m.DestinationPropertyName;
        indexDefinitions.Type = type;
        if (indexDefinitions.Name == "PublicationDate")
          indexDefinitions.Type = typeof (DateTime);
        return indexDefinitions;
      }));
      IEnumerable<string> source = PublishingUtilities.SearchIndexAdditionalFields(settings);
      if (source != null && source.Any<string>())
      {
        IEnumerable<IFieldDefinition> second = source.Select<string, IFieldDefinition>((Func<string, IFieldDefinition>) (s =>
        {
          IFieldDefinition indexDefinitions = ObjectFactory.Resolve<IFieldDefinition>();
          indexDefinitions.Name = s;
          indexDefinitions.Type = (Type) null;
          if (!(s == "Categories"))
          {
            if (!(s == "Tags"))
            {
              if (!(s == "LibraryIds"))
              {
                if (s == "DateCreated")
                  indexDefinitions.Type = typeof (DateTime);
              }
              else
                indexDefinitions.Type = typeof (string[]);
            }
            else
              indexDefinitions.Type = typeof (string[]);
          }
          else
            indexDefinitions.Type = typeof (string[]);
          return indexDefinitions;
        }));
        first = first.Concat<IFieldDefinition>(second);
      }
      IFieldDefinition fieldDefinition1 = ObjectFactory.Resolve<IFieldDefinition>();
      fieldDefinition1.Name = "IdentityField";
      fieldDefinition1.Type = typeof (string);
      IFieldDefinition fieldDefinition2 = ObjectFactory.Resolve<IFieldDefinition>();
      fieldDefinition2.Name = "Language";
      fieldDefinition2.Type = typeof (string);
      IFieldDefinition fieldDefinition3 = ObjectFactory.Resolve<IFieldDefinition>();
      fieldDefinition3.Name = "IsOriginalContent";
      fieldDefinition3.Type = typeof (string);
      return first.Concat<IFieldDefinition>((IEnumerable<IFieldDefinition>) new List<IFieldDefinition>()
      {
        fieldDefinition2,
        fieldDefinition1,
        fieldDefinition3
      });
    }

    private void DeleteExternalData(IManager manager, PublishingPoint point)
    {
      if (!(manager.Provider.Name == "SearchPublishingProvider"))
        return;
      IPipe pipe = ((IEnumerable<IPipe>) point.GetOutboundPipes()).FirstOrDefault<IPipe>();
      if (pipe == null || !(pipe.PipeSettings is SearchIndexPipeSettings pipeSettings))
        return;
      string catalogName = pipeSettings.CatalogName;
      this.GetService().DeleteIndex(catalogName);
    }

    protected virtual void ClearDeletedSettings(
      PublishingPoint point,
      List<WcfPipeSettings> settings,
      PublishingManager manager)
    {
      List<PipeSettings> pipeSettingsList = new List<PipeSettings>();
      foreach (PipeSettings pipeSetting in (IEnumerable<PipeSettings>) point.PipeSettings)
      {
        PipeSettings setting = pipeSetting;
        if (settings.SingleOrDefault<WcfPipeSettings>((Func<WcfPipeSettings, bool>) (p => p.Id == setting.Id)) == null)
          pipeSettingsList.Add(setting);
      }
      foreach (PipeSettings pipeSettings in pipeSettingsList)
      {
        point.PipeSettings.Remove(pipeSettings);
        manager.DeletePipeSettings(pipeSettings);
      }
    }

    /// <summary>Fills the pipe settings.</summary>
    /// <param name="modelPublishingPoint">The point.</param>
    /// <param name="allViewModelSettings">The settings.</param>
    /// <param name="InBound">if set to <c>true</c> [in bound].</param>
    /// <param name="manager">Publishing manager to use</param>
    protected virtual void FillPipeSettings(
      PublishingPoint modelPublishingPoint,
      List<WcfPipeSettings> allViewModelSettings,
      bool InBound,
      PublishingManager manager)
    {
      foreach (WcfPipeSettings viewModelSetting in allViewModelSettings)
      {
        PipeSettings tempSetting = viewModelSetting.ConvertToModel();
        tempSetting.ApplicationName = manager.Provider.ApplicationName;
        if (string.IsNullOrEmpty(tempSetting.Description))
          tempSetting.Description = (string) modelPublishingPoint.Description;
        tempSetting.FilterExpression = viewModelSetting.AdditionalFilter;
        tempSetting.LanguageIds.Clear();
        if (viewModelSetting.LanguageIds.Count > 0)
          viewModelSetting.LanguageIds.ForEach((Action<string>) (id => tempSetting.LanguageIds.Add(id)));
        if (tempSetting is RssPipeSettings)
        {
          RssPipeSettings rssPipeSettings = tempSetting as RssPipeSettings;
          if (string.IsNullOrEmpty(rssPipeSettings.UrlName))
            rssPipeSettings.UrlName = Regex.Replace(modelPublishingPoint.Name.ToLowerInvariant(), PublishingAdminService.FilterExpression, "-");
        }
        if (tempSetting is SitefinityContentPipeSettings)
          (tempSetting as SitefinityContentPipeSettings).BackLinksPageId = !viewModelSetting.ContentLocationPageID.HasValue ? new Guid?() : viewModelSetting.ContentLocationPageID;
        if (tempSetting is SearchIndexPipeSettings)
        {
          SearchIndexPipeSettings indexPipeSettings = tempSetting as SearchIndexPipeSettings;
          indexPipeSettings.Title = modelPublishingPoint.Name;
          if (string.IsNullOrEmpty(indexPipeSettings.CatalogName))
            indexPipeSettings.CatalogName = Regex.Replace(modelPublishingPoint.Name.ToLowerInvariant(), PublishingAdminService.FilterExpression, "-");
        }
        Guid vmID = viewModelSetting.Id;
        PipeSettings target = manager.GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (s => s.Id == vmID)).SingleOrDefault<PipeSettings>();
        if (target == null)
        {
          IPipe pipe = PublishingSystemFactory.GetPipe(viewModelSetting.PipeName);
          target = !(vmID != Guid.Empty) ? manager.CreatePipeSettings(pipe.PipeSettingsType) : manager.Provider.CreatePipeSettings(pipe.PipeSettingsType, vmID);
          modelPublishingPoint.PipeSettings.Add(target);
        }
        tempSetting.CopySettings(target);
        viewModelSetting.MappingSettings.CopyToModel(manager, target.Mappings);
      }
    }

    protected void DisableServiceCache() => ServiceUtility.DisableCache();

    protected virtual IQueryable<PublishingPoint> ProcessPublishingPointsFilter(
      IQueryable<PublishingPoint> points,
      string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return points;
      if (!filter.Contains("ContentType"))
        return points.Where<PublishingPoint>(filter);
      string[] strArray = filter.Split(new string[1]{ "==" }, StringSplitOptions.None);
      string str1 = strArray[0].Trim();
      string str2 = strArray[1].Trim();
      string contentTypeName = str2.CompareTo("ContentType") != 0 ? str2 : str1;
      contentTypeName = contentTypeName.Trim('"');
      return PublishingManager.GetManager().GetPipeSettings<SitefinityContentPipeSettings>().Where<SitefinityContentPipeSettings>((Expression<Func<SitefinityContentPipeSettings, bool>>) (ps => ps.ContentTypeName == contentTypeName)).Select<SitefinityContentPipeSettings, PublishingPoint>((Expression<Func<SitefinityContentPipeSettings, PublishingPoint>>) (ps => ps.PublishingPoint)).ToList<PublishingPoint>().Distinct<PublishingPoint>().AsQueryable<PublishingPoint>();
    }

    protected virtual PipeShortData GetPipeShortData(PipeSettings settings)
    {
      string str = settings.GetLocalizedUIName();
      switch (settings)
      {
        case RssPipeSettings _:
          RssPipeSettings rssPipeSettings = settings as RssPipeSettings;
          if (!string.IsNullOrEmpty(rssPipeSettings.UrlName))
          {
            str = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", (object) (VirtualPathUtility.AppendTrailingSlash(PublishingManager.GetFeedsBaseURl()) + HttpUtility.UrlPathEncode(rssPipeSettings.UrlName)), (object) settings.GetLocalizedUIName());
            break;
          }
          break;
        case TwitterPipeSettings _:
          TwitterPipeSettings twitterPipeSettings = settings as TwitterPipeSettings;
          if (!string.IsNullOrEmpty(twitterPipeSettings.UserNameReference))
          {
            str = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", (object) string.Format("http://twitter.com/{0}", (object) twitterPipeSettings.UserNameReference), (object) settings.GetLocalizedUIName());
            break;
          }
          break;
      }
      return new PipeShortData()
      {
        PipeName = settings.PipeName,
        UIName = str
      };
    }

    private ISearchService GetService() => ServiceBus.ResolveService<ISearchService>();

    private void ProcessAdditionalFields(
      PublishingPoint dataItem,
      PublishingPointDetailViewModel viewModel)
    {
      PipeSettings pipeSettings = dataItem.PipeSettings.SingleOrDefault<PipeSettings>((Func<PipeSettings, bool>) (ps => ps is SearchIndexPipeSettings));
      if (pipeSettings == null)
        return;
      pipeSettings.AdditionalData["SearchIndexAdditionalFields"] = viewModel.AdditionalFields;
    }

    private IQueryable<PublishingPoint> LoadPublishingPoints(
      PublishingManager manager,
      bool ignoreMultisiteMode = false)
    {
      if (ignoreMultisiteMode)
        return manager.GetPublishingPoints();
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      return manager.GetSiteItems<PublishingPoint>(id).OrderBy<PublishingPoint, DateTime>((Expression<Func<PublishingPoint, DateTime>>) (p => p.DateCreated)).Union<PublishingPoint>((IEnumerable<PublishingPoint>) manager.GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.IsSharedWithAllSites))).GroupBy<PublishingPoint, Guid>((Expression<Func<PublishingPoint, Guid>>) (p => p.Id)).Select<IGrouping<Guid, PublishingPoint>, PublishingPoint>((Expression<Func<IGrouping<Guid, PublishingPoint>, PublishingPoint>>) (group => group.First<PublishingPoint>()));
    }

    protected static string FilterExpression => "[^" + ObjectFactory.Resolve<RegexStrategy>().UnicodeCategories + "\\-\\!\\$\\'\\(\\)\\+\\=\\@\\d]+";
  }
}
