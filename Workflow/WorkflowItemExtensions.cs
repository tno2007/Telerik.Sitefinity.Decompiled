// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.WorkflowItemExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> type.
  /// </summary>
  public static class WorkflowItemExtensions
  {
    internal const string ApprovalTrackingRecordsCacheKey = "ApprovalTracking";
    private static readonly object atrCacheSync = new object();

    /// <summary>Gets the current status of the workflow item.</summary>
    /// <param name="workflowItem">
    /// An instance of <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> for which the status is to be retrieved.
    /// </param>
    /// <returns>The workflow status of the item.</returns>
    public static string GetWorkflowItemStatus(this IWorkflowItem workflowItem)
    {
      ApprovalTrackingRecord approvalTrackingRecord = workflowItem.GetCurrentApprovalTrackingRecord();
      return approvalTrackingRecord != null ? approvalTrackingRecord.Status : string.Empty;
    }

    internal static string GetWorkflowItemProviderName(this IWorkflowItem item)
    {
      switch (item)
      {
        case IDataItem dataItem:
          return dataItem.GetProviderName();
        case IDataItemProxy dataItemProxy:
          return dataItemProxy.GetProviderName();
        default:
          return string.Empty;
      }
    }

    internal static Type GetActualType(this IWorkflowItem item) => item is IDataItemProxy dataItemProxy ? dataItemProxy.GetActualType() : item.GetType();

    internal static string GetRootKey(this IWorkflowItem item)
    {
      string rootKey = (string) null;
      if (item is IHasRoot)
        rootKey = ((IHasRoot) item).RootKey;
      return rootKey;
    }

    /// <summary>
    /// Gets the current (latest) <see cref="T:Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecord" /> for the instance of
    /// <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> object.
    /// </summary>
    /// <param name="workflowItem">
    /// An instance of <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> for which the current record
    /// ought to be retrieved.
    /// </param>
    /// <returns>
    /// The current <see cref="T:Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecord" /> for the <see cref="T:Telerik.Sitefinity.IWorkflowItem" />
    /// </returns>
    public static ApprovalTrackingRecord GetCurrentApprovalTrackingRecord(
      this IWorkflowItem workflowItem,
      CultureInfo culture = null)
    {
      IAppSettings contextSettings = DataExtensions.AppSettings.ContextSettings;
      IQueryable<ApprovalTrackingRecord> approvalRecords = (workflowItem as IApprovalWorkflowItem).GetApprovalRecords();
      if (approvalRecords.Count<ApprovalTrackingRecord>() == 0 && workflowItem is PageNode pageNode)
      {
        string approvalWorkflowState;
        if (contextSettings.Multilingual)
        {
          culture = culture ?? SystemManager.CurrentContext.Culture;
          approvalWorkflowState = pageNode.ApprovalWorkflowState.GetString(culture, false);
        }
        else
        {
          culture = culture ?? CultureInfo.InvariantCulture;
          approvalWorkflowState = (string) pageNode.ApprovalWorkflowState;
        }
        return new ApprovalTrackingRecord()
        {
          Status = approvalWorkflowState,
          Id = Guid.Empty,
          UserId = Guid.Empty,
          LastModified = DateTime.Now,
          DateCreated = DateTime.Now,
          Culture = AppSettings.CurrentSettings.GetCultureLcid(culture)
        };
      }
      int invariant = CultureInfo.InvariantCulture.LCID;
      if (contextSettings.Multilingual)
      {
        culture = culture ?? SystemManager.CurrentContext.Culture;
        int cultureId = AppSettings.CurrentSettings.GetCultureLcid(culture);
        IOrderedQueryable<ApprovalTrackingRecord> source = approvalRecords.OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (ap => ap.DateCreated));
        Func<ApprovalTrackingRecord, bool> func = (Func<ApprovalTrackingRecord, bool>) (ar => ar.Culture == cultureId);
        if (culture.Equals((object) contextSettings.DefaultFrontendLanguage))
        {
          int defaultCultureId = AppSettings.CurrentSettings.GetCultureLcid(contextSettings.DefaultFrontendLanguage);
          func = (Func<ApprovalTrackingRecord, bool>) (ar => ar.Culture == cultureId || ar.Culture == defaultCultureId);
        }
        Func<ApprovalTrackingRecord, bool> predicate = func;
        return source.Where<ApprovalTrackingRecord>(predicate).FirstOrDefault<ApprovalTrackingRecord>();
      }
      return approvalRecords.OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (ap => ap.DateCreated)).FirstOrDefault<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (ar => ar.Culture == invariant));
    }

    internal static IApprovalTrackingRecord GetCurrentApprovalTrackingRecordFromCache(
      this IWorkflowItem workflowItem,
      CultureInfo culture = null)
    {
      ICacheManager cacheManager1 = SystemManager.GetCacheManager("ApprovalTracking");
      Guid id = workflowItem.Id;
      string key1 = id.ToString() + (culture != null ? culture.Name : string.Empty);
      object trackingRecordFromCache = cacheManager1[key1];
      if (trackingRecordFromCache == null)
      {
        lock (WorkflowItemExtensions.atrCacheSync)
        {
          trackingRecordFromCache = cacheManager1[key1];
          if (trackingRecordFromCache == null)
          {
            ApprovalTrackingRecord approvalTrackingRecord = workflowItem.GetCurrentApprovalTrackingRecord(culture);
            trackingRecordFromCache = approvalTrackingRecord == null ? new object() : (object) new ApprovalTrackingRecordProxy(approvalTrackingRecord);
            ICacheManager cacheManager2 = cacheManager1;
            string key2 = key1;
            object obj = trackingRecordFromCache;
            ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2];
            Type trackedItemType = typeof (ApprovalTrackingRecord);
            id = workflowItem.Id;
            string key3 = id.ToString();
            cacheItemExpirationArray[0] = (ICacheItemExpiration) new DataItemCacheDependency(trackedItemType, key3);
            cacheItemExpirationArray[1] = (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(5.0));
            cacheManager2.Add(key2, obj, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
          }
        }
      }
      return trackingRecordFromCache as IApprovalTrackingRecord;
    }

    /// <summary>
    /// Copies the approval tracking records from one workflow item to another
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    [Obsolete("This method is no longer used by Sitefinity content. As of Sitefinity 7.0 there is no need to copy the records anymore.\r\n        Refer to the extension method of the IWorkflowItem interface -> GetApprovalRecords() in namespace Telerik.Sitefinity")]
    public static void CopyApprovalRecords(this IWorkflowItem source, IWorkflowItem destination)
    {
      if (source.ApprovalTrackingRecordMap == null)
        return;
      if (destination.ApprovalTrackingRecordMap == null)
      {
        IDataItem dataItem = source as IDataItem;
        destination.ApprovalTrackingRecordMap = new ApprovalTrackingRecordMap()
        {
          Id = dataItem != null ? dataItem.GetNewGuid() : Guid.NewGuid()
        };
      }
      else
        destination.ApprovalTrackingRecordMap.ApprovalRecords.Clear();
      foreach (ApprovalTrackingRecord approvalRecord in (IEnumerable<ApprovalTrackingRecord>) source.ApprovalTrackingRecordMap.ApprovalRecords)
      {
        Guid destinationId = destination.Id;
        if (approvalRecord.Parents.FirstOrDefault<ApprovalTrackingRecordMap>((Func<ApprovalTrackingRecordMap, bool>) (p => p.Id == destinationId)) == null)
          approvalRecord.Parents.Add(destination.ApprovalTrackingRecordMap);
        destination.ApprovalTrackingRecordMap.ApprovalRecords.Add(approvalRecord);
      }
    }

    /// <summary>Copies the approval status and the approval records</summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    public static void CopyApprovalStatus(
      this IApprovalWorkflowItem source,
      IApprovalWorkflowItem destination)
    {
      destination.ApprovalWorkflowState.CopyFrom(source.ApprovalWorkflowState);
    }

    /// <summary>
    /// Gets the localized status of the <see cref="T:Telerik.Sitefinity.IApprovalWorkflowItem" />.
    /// </summary>
    /// <param name="workflowItem">Instance of the <see cref="T:Telerik.Sitefinity.IApprovalWorkflowItem" />.</param>
    /// <returns>Localized approval status.</returns>
    internal static string GetLocalizedStatus(
      this IWorkflowItem workflowItem,
      string state,
      CultureInfo culture = null,
      object contextItem = null)
    {
      return WorkflowItemExtensions.GetParsedWorkflowStatus(workflowItem, culture, state, contextItem);
    }

    private static string GetParsedWorkflowStatus(
      IWorkflowItem workflowItem,
      CultureInfo culture,
      string state,
      object contextItem = null)
    {
      IWorkflowExecutionDefinition executionDefinition = WorkflowManager.GetWorkflowExecutionDefinition(workflowItem);
      ISchedulingStatus schedulingStatus = contextItem as ISchedulingStatus;
      string status;
      if (state == "Draft" && schedulingStatus != null)
      {
        DateTime? nullable = schedulingStatus.ExpirationDate;
        if (!nullable.HasValue)
        {
          nullable = schedulingStatus.PublicationDate;
          if (!nullable.HasValue)
            goto label_9;
        }
        nullable = schedulingStatus.ExpirationDate;
        if (nullable.HasValue)
        {
          nullable = schedulingStatus.PublicationDate;
          if (nullable.HasValue)
          {
            status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) string.Format("{0}, {1}", (object) Res.Get<ApprovalWorkflowResources>().ScheduledPublicationDate, (object) Res.Get<ApprovalWorkflowResources>().ScheduledExpirationDate));
            goto label_10;
          }
        }
        nullable = schedulingStatus.PublicationDate;
        if (nullable.HasValue)
        {
          status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) Res.Get<ApprovalWorkflowResources>().ScheduledPublicationDate);
          goto label_10;
        }
        else
        {
          status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) Res.Get<ApprovalWorkflowResources>().ScheduledExpirationDate);
          goto label_10;
        }
      }
label_9:
      status = !(state == "Rejected") || executionDefinition.Levels.Count<IWorkflowExecutionLevel>() <= 2 ? Res.Get<ApprovalWorkflowResources>().Get(state) : Res.Get<ApprovalWorkflowResources>().RejectedForApproval;
label_10:
      return workflowItem.ParseWorkflowStatus(status, culture, contextItem);
    }

    internal static string ParseWorkflowStatus(
      this IWorkflowItem workflowItem,
      string status,
      CultureInfo culture = null,
      object contextItem = null)
    {
      IApprovalTrackingRecord record = (IApprovalTrackingRecord) null;
      if (status.Contains("lastApprovalRecord"))
        record = workflowItem.GetCurrentApprovalTrackingRecordFromCache(culture);
      Dictionary<string, object> context = new Dictionary<string, object>();
      context["content"] = !(workflowItem is PageNode) ? (object) workflowItem : (object) ((PageNode) workflowItem).GetPageData(culture);
      if (record != null)
      {
        WcfApprovalTrackingRecord approvalTrackingRecord = new WcfApprovalTrackingRecord(record, workflowItem, status);
        context["lastApprovalRecord"] = (object) approvalTrackingRecord;
      }
      if (contextItem == null && workflowItem is ILifecycleDataItem lifecycleDataItem)
      {
        LanguageData languageData = lifecycleDataItem.GetLanguageData(culture);
        if (languageData != null)
          contextItem = (object) new
          {
            ExpirationDate = languageData.ExpirationDate,
            PublicationDate = languageData.PublicationDate
          };
      }
      if (contextItem != null)
      {
        if (status.Contains("content.ExpirationDate"))
          status = status.Replace("content.ExpirationDate", "item.ExpirationDate");
        if (status.Contains("content.PublicationDate"))
          status = status.Replace("content.PublicationDate", "item.PublicationDate");
        context["item"] = contextItem;
      }
      return LabelExpressionResolver.Parse(status, context);
    }

    internal static bool IsOperationSupported(
      this IWorkflowItem workflowItem,
      string operation,
      CultureInfo culture = null)
    {
      culture = culture ?? SystemManager.CurrentContext.Culture;
      return !SystemManager.CurrentContext.AppSettings.Multilingual || !(workflowItem is ILocalizable localizable) || ((IEnumerable<CultureInfo>) localizable.AvailableCultures).Contains<CultureInfo>(culture);
    }
  }
}
