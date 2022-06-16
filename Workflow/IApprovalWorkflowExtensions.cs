// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.IApprovalWorkflowExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="!:IApprovalWorkflow" /> items.
  /// </summary>
  public static class IApprovalWorkflowExtensions
  {
    internal const string DeletedWorkflowItems = "deleted_workflow_items";

    /// <summary>
    /// Gets the localized status of the <see cref="T:Telerik.Sitefinity.IApprovalWorkflowItem" />.
    /// </summary>
    /// <param name="workflowItem">Instance of the <see cref="T:Telerik.Sitefinity.IApprovalWorkflowItem" />.</param>
    /// <returns>Localized approval status.</returns>
    public static string GetLocalizedStatus(
      this IApprovalWorkflowItem workflowItem,
      CultureInfo culture = null)
    {
      return workflowItem.GetLocalizedStatus(out string _, out IStatusInfo _, culture);
    }

    internal static string GetLocalizedStatus(
      this IWorkflowItem workflowItem,
      out string statusKey,
      out IStatusInfo statusInfo,
      CultureInfo culture = null)
    {
      object contextItem = (object) null;
      if (workflowItem.TryGetExternalStatus(out statusInfo, culture))
      {
        contextItem = (object) statusInfo.Data;
        statusKey = statusInfo.Data.Status;
        try
        {
          if (!statusKey.IsNullOrEmpty())
          {
            string status = Res.Get<ApprovalWorkflowResources>().Get(statusKey);
            return workflowItem.ParseWorkflowStatus(status, culture, contextItem);
          }
        }
        catch (Exception ex)
        {
          return statusKey;
        }
      }
      ILstring approvalWorlflowState;
      switch (workflowItem)
      {
        case IDynamicFieldsContainer dataItem:
          approvalWorlflowState = (ILstring) dataItem.GetString("ApprovalWorkflowState");
          break;
        case PageDataProxy pageDataProxy:
          approvalWorlflowState = pageDataProxy.ApprovalWorlflowState;
          break;
        default:
          statusKey = string.Empty;
          return string.Empty;
      }
      if (culture == null)
        culture = culture.GetLstring();
      statusKey = (string) null;
      if (approvalWorlflowState != null)
      {
        approvalWorlflowState.TryGetValue(out statusKey, culture);
        if (string.IsNullOrEmpty(statusKey) && (culture == CultureInfo.InvariantCulture || !approvalWorlflowState.TryGetValue(out statusKey, CultureInfo.InvariantCulture)))
          return string.Empty;
      }
      if (statusKey.IsNullOrEmpty())
        return string.Empty;
      if (statusKey == "Scheduled")
      {
        ISchedulingStatus schedulingStatus = contextItem as ISchedulingStatus;
        statusKey = schedulingStatus == null ? "Draft" : "Published";
        ILstring lstring = (ILstring) new LstringProxy(statusKey);
      }
      try
      {
        return workflowItem.GetLocalizedStatus(statusKey, culture, contextItem);
      }
      catch (Exception ex)
      {
      }
      return statusKey;
    }

    /// <summary>
    /// Sets the workflow status (Published, Draft and etc.) of the specified workflow item. The workflow status of each item is culture specific.
    /// In multilingual, if no culture is specified, then the culture of the thread is used. In monolingual the invariant culture is used
    /// </summary>
    /// <param name="workflowItem">The workflow item.</param>
    /// <param name="applicationName">Name of the application in which the workflow item is stored.</param>
    /// <param name="status">The workflow status.</param>
    /// <param name="culture">The culture.</param>
    public static void SetWorkflowStatus(
      this IApprovalWorkflowItem workflowItem,
      string applicationName,
      string status,
      CultureInfo culture = null)
    {
      IDataItem dataItem = workflowItem as IDataItem;
      DataProviderBase dataProviderBase = (DataProviderBase) null;
      if (dataItem != null)
        dataProviderBase = dataItem.Provider as DataProviderBase;
      culture = culture ?? SystemManager.CurrentContext.Culture;
      workflowItem.CreateApprovalTrackingRecord(applicationName, status, dataProviderBase.GetNewGuid(), culture);
      workflowItem.ApprovalWorkflowState.SetString(culture, status);
    }

    /// <summary>Creates the approval tracking record.</summary>
    /// <param name="workflowItemId">The workflow item id.</param>
    /// <param name="applicationName">Name of the application.</param>
    /// <param name="status">The status.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    internal static ApprovalTrackingRecord CreateApprovalTrackingRecord(
      this IWorkflowItem workflowItem,
      string applicationName,
      string status,
      Guid id = default (Guid),
      CultureInfo culture = null)
    {
      if (culture == null)
        culture = !SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.InvariantCulture : SystemManager.CurrentContext.Culture;
      Guid guid = id == Guid.Empty ? Guid.NewGuid() : id;
      ApprovalTrackingRecord entity = new ApprovalTrackingRecord()
      {
        ApplicationName = applicationName,
        Culture = AppSettings.CurrentSettings.GetCultureLcid(culture),
        DateCreated = DateTime.Now,
        Id = guid,
        LastModified = DateTime.Now,
        Note = string.Empty,
        Status = status,
        UserId = SecurityManager.CurrentUserId,
        WorkflowItemId = workflowItem.Id
      };
      OpenAccessContextBase.GetContext((object) workflowItem)?.Add((object) entity);
      return entity;
    }

    internal static IEnumerable<CultureInfo> SyncMultilingualRecords(
      this IWorkflowItem workflowItem,
      ApprovalTrackingRecord trackingRecord,
      DataProviderBase provider,
      IEnumerable<CultureInfo> culturesToSync)
    {
      int trackingRecordCulture = trackingRecord.Culture;
      culturesToSync = ((IEnumerable<CultureInfo>) (workflowItem as ILocalizable).AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !c.Equals((object) trackingRecordCulture) && !c.Equals((object) CultureInfo.InvariantCulture)));
      foreach (CultureInfo culture in culturesToSync)
      {
        ApprovalTrackingRecord approvalTrackingRecord = new ApprovalTrackingRecord()
        {
          ApplicationName = trackingRecord.ApplicationName,
          Culture = AppSettings.CurrentSettings.GetCultureLcid(culture),
          DateCreated = DateTime.Now,
          Id = provider != null ? provider.GetNewGuid() : Guid.NewGuid(),
          LastModified = DateTime.Now,
          Note = trackingRecord.Note,
          Status = trackingRecord.Status,
          UserId = SecurityManager.CurrentUserId,
          WorkflowItemId = trackingRecord.WorkflowItemId
        };
      }
      return culturesToSync;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecord" /> list
    /// that has the workflow action history of the item.
    /// </summary>
    public static IQueryable<ApprovalTrackingRecord> GetApprovalRecords(
      this IWorkflowItem item)
    {
      if (!(OpenAccessContextBase.GetContext((object) item) is OpenAccessContext context))
        return Enumerable.Empty<ApprovalTrackingRecord>().AsQueryable<ApprovalTrackingRecord>();
      return context.GetAll<ApprovalTrackingRecord>().Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.WorkflowItemId == item.Id));
    }

    internal static void DeleteApprovalRecords(this IWorkflowItem item)
    {
      List<ApprovalTrackingRecord> list = item.GetApprovalRecords().ToList<ApprovalTrackingRecord>();
      if (!(OpenAccessContextBase.GetContext((object) item) is OpenAccessContext context) || list.Count <= 0)
        return;
      context.Delete((IEnumerable) list);
      context.SaveChanges();
    }

    internal static IQueryable<ApprovalTrackingRecord> GetApprovalRecords(
      IList<IWorkflowItem> items,
      IOpenAccessDataProvider provider)
    {
      List<Guid> itemIds = items.Select<IWorkflowItem, Guid>((Func<IWorkflowItem, Guid>) (x => x.Id)).ToList<Guid>();
      if (itemIds.Count <= 0)
        return Enumerable.Empty<ApprovalTrackingRecord>().AsQueryable<ApprovalTrackingRecord>();
      return provider.GetContext().GetAll<ApprovalTrackingRecord>().Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => itemIds.Contains(x.WorkflowItemId)));
    }

    internal static bool HasMultilingualApprovalTrackingRecords(this IApprovalWorkflowItem item) => item.GetApprovalRecords().Any<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (r => r.Culture != CultureInfo.InvariantCulture.LCID));

    internal static KeyValuePair<Guid, int[]>? GetDeletedItemCultures(
      this IApprovalWorkflowItem item,
      DataProviderBase provider,
      SecurityConstants.TransactionActionType itemStatus)
    {
      switch (itemStatus)
      {
        case SecurityConstants.TransactionActionType.Updated:
          string[] array1 = DataExtensions.AppSettings.Current.AllLanguages.Values.Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToArray<string>();
          string[] cultures;
          if (provider.IsLstringFieldDirty((object) item, "Title", out cultures, array1))
          {
            int[] array2 = ((IEnumerable<string>) cultures).Select<string, int>((Func<string, int>) (x => CultureInfo.GetCultureInfo(x).LCID)).ToArray<int>();
            return new KeyValuePair<Guid, int[]>?(new KeyValuePair<Guid, int[]>(item.Id, array2));
          }
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          return new KeyValuePair<Guid, int[]>?(new KeyValuePair<Guid, int[]>(item.Id, (int[]) null));
      }
      return new KeyValuePair<Guid, int[]>?();
    }

    internal static void DeleteApprovalRecords(DataProviderBase provider)
    {
      if (!(provider.GetExecutionStateData("deleted_workflow_items") is IList<Guid> executionStateData))
        return;
      SitefinityOAContext transaction = provider.GetTransaction() as SitefinityOAContext;
      foreach (Guid guid in (IEnumerable<Guid>) executionStateData)
      {
        Guid deletedItem = guid;
        transaction.GetAll<ApprovalTrackingRecord>().Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.WorkflowItemId == deletedItem)).DeleteAll();
        WorkflowDefinitionExtensions.UpdateWorkflowScopesOnContentFilterItemDeleted(deletedItem);
      }
      provider.SetExecutionStateData("deleted_workflow_items", (object) null);
    }
  }
}
