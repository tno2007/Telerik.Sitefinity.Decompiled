// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.WorkflowService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using Telerik.Sitefinity.Workflow.Services.Data;
using Telerik.Sitefinity.Workflow.UI;

namespace Telerik.Sitefinity.Workflow.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class WorkflowService : IWorkflowService
  {
    public WorkflowVisualElementsCollection GetWorkflowVisualElements(
      string itemType,
      string providerName,
      string itemId,
      string itemCulture,
      bool showMoreActions = true)
    {
      return this.GetWorkflowVisualElementsInternal(itemType, providerName, itemId, itemCulture, showMoreActions);
    }

    public WorkflowVisualElementsCollection GetWorkflowVisualElementsInXml(
      string itemType,
      string providerName,
      string itemId,
      string itemCulture,
      bool showMoreActions = true)
    {
      return this.GetWorkflowVisualElementsInternal(itemType, providerName, itemId, itemCulture, showMoreActions);
    }

    public string MessageWorkflow(
      KeyValuePair<string, string>[] contextBag,
      string itemId,
      string itemType,
      string providerName,
      string workflowOperation)
    {
      return this.MessageWorkflowInternal(contextBag, itemId, itemType, providerName, workflowOperation);
    }

    public string MessageWorkflowInXml(
      KeyValuePair<string, string>[] contextBag,
      string itemId,
      string itemType,
      string providerName,
      string workflowOperation)
    {
      return this.MessageWorkflowInternal(contextBag, itemId, itemType, providerName, workflowOperation);
    }

    internal IList<WorkflowVisualElement> GetWorkflowVisualElements(
      Type itemType,
      string providerName,
      IApprovalWorkflowItem item,
      CultureInfo culture,
      bool showMoreActions = true)
    {
      string status = (string) null;
      IApprovalTrackingRecord approvalTrackingRecord = this.GetLastApprovalTrackingRecord(SystemManager.CurrentContext.AppSettings, item, culture);
      if (approvalTrackingRecord != null)
        status = approvalTrackingRecord.Status;
      return this.GetWorkflowVisualElements(itemType, providerName, item, culture, out IWorkflowExecutionDefinition _, status, showMoreActions);
    }

    private IList<WorkflowVisualElement> GetWorkflowVisualElements(
      Type itemType,
      string providerName,
      IApprovalWorkflowItem item,
      CultureInfo culture,
      out IWorkflowExecutionDefinition wed,
      string status,
      bool showMoreActions = true)
    {
      return this.GetWorkflowVisualElements(itemType, providerName, item == null ? Guid.Empty : item.Id, culture, out wed, status, showMoreActions);
    }

    private IList<WorkflowVisualElement> GetWorkflowVisualElements(
      Type itemType,
      string providerName,
      Guid id,
      CultureInfo culture,
      out IWorkflowExecutionDefinition wed,
      string status,
      bool showMoreActions = true)
    {
      IDictionary<string, DecisionActivity> source1 = (IDictionary<string, DecisionActivity>) null;
      IDictionary<string, DecisionActivity> currentDecisions = WorkflowManager.GetCurrentDecisions(itemType, providerName, id, out wed, culture);
      if (currentDecisions != null)
        source1 = (IDictionary<string, DecisionActivity>) currentDecisions.Where<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (d => !d.Value.HideInUI)).ToDictionary<KeyValuePair<string, DecisionActivity>, string, DecisionActivity>((Func<KeyValuePair<string, DecisionActivity>, string>) (x => x.Key), (Func<KeyValuePair<string, DecisionActivity>, DecisionActivity>) (x => x.Value));
      if (source1 == null)
        return (IList<WorkflowVisualElement>) null;
      if (!showMoreActions)
        source1 = (IDictionary<string, DecisionActivity>) source1.Where<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (d => d.Value.Placeholder != PlaceholderName.OtherActions)).ToDictionary<KeyValuePair<string, DecisionActivity>, string, DecisionActivity>((Func<KeyValuePair<string, DecisionActivity>, string>) (x => x.Key), (Func<KeyValuePair<string, DecisionActivity>, DecisionActivity>) (x => x.Value));
      if (wed.AllowNotes)
      {
        foreach (string key in (IEnumerable<string>) source1.Keys)
        {
          if (source1[key].ArgumentDialogName.IsNullOrEmpty())
          {
            string str = WorkflowHelper.InternalOperationDialog(key);
            if (!str.IsNullOrWhitespace())
            {
              source1[key].ArgumentDialogName = str;
              source1[key].PersistOnDecision = true;
            }
          }
        }
      }
      List<WorkflowVisualElement> source2 = new List<WorkflowVisualElement>();
      bool ignoreMultilingualWarnings = typeof (ILifecycleDataItem).IsAssignableFrom(itemType);
      foreach (KeyValuePair<string, DecisionActivity> keyValuePair in (IEnumerable<KeyValuePair<string, DecisionActivity>>) source1)
        source2.Add(WorkflowVisualElementFactory.ResolveVisualElement(keyValuePair.Key, keyValuePair.Value, ignoreMultilingualWarnings));
      foreach (WorkflowVisualElement workflowVisualElement in source2)
        WorkflowHelper.UpdateWorkflowVisualElement(workflowVisualElement, wed, status, culture);
      return (IList<WorkflowVisualElement>) source2.OrderBy<WorkflowVisualElement, int>((Func<WorkflowVisualElement, int>) (e => e.Ordinal)).ToList<WorkflowVisualElement>();
    }

    private WorkflowVisualElementsCollection GetWorkflowVisualElementsInternal(
      string itemType,
      string providerName,
      string itemId,
      string itemCulture,
      bool showMoreActions = true)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IWorkflowExecutionDefinition wed = (IWorkflowExecutionDefinition) null;
      IEnumerable<WorkflowVisualElement> source = (IEnumerable<WorkflowVisualElement>) null;
      string positiveMessage = string.Empty;
      string status = string.Empty;
      WcfApprovalTrackingRecord lastApprovalTrackingRecord = (WcfApprovalTrackingRecord) null;
      Type type = TypeResolutionService.ResolveType(itemType);
      Guid result;
      if (!Guid.TryParse(itemId, out result))
        result = new Guid(itemId);
      IApprovalWorkflowItem approvalWorkflowItem = this.GetApprovalWorkflowItem(type, providerName, result);
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      CultureInfo cultureInfo = this.GetCultureInfo(appSettings, itemCulture);
      if (approvalWorkflowItem != null)
      {
        IApprovalTrackingRecord approvalTrackingRecord = this.GetLastApprovalTrackingRecord(appSettings, approvalWorkflowItem, cultureInfo);
        if (approvalTrackingRecord != null)
        {
          status = approvalTrackingRecord.Status;
          if (!string.IsNullOrEmpty(status))
          {
            string key = status + "Message";
            try
            {
              positiveMessage = Res.Get<ApprovalWorkflowResources>().Get(key, Res.CurrentBackendCulture);
            }
            catch
            {
              positiveMessage = key;
            }
          }
          source = (IEnumerable<WorkflowVisualElement>) this.GetWorkflowVisualElements(type, providerName, approvalWorkflowItem, cultureInfo, out wed, status, showMoreActions);
          lastApprovalTrackingRecord = new WcfApprovalTrackingRecord(approvalTrackingRecord, (IWorkflowItem) approvalWorkflowItem);
        }
      }
      if (source == null)
      {
        source = (IEnumerable<WorkflowVisualElement>) this.GetWorkflowVisualElements(type, providerName, result, cultureInfo, out wed, (string) null, showMoreActions);
        if (source == null)
          return (WorkflowVisualElementsCollection) null;
      }
      WorkflowVisualElementsContext context = new WorkflowVisualElementsContext(string.Empty, string.Empty, positiveMessage, string.Empty, status, lastApprovalTrackingRecord);
      return new WorkflowVisualElementsCollection((IList<WorkflowVisualElement>) source.ToList<WorkflowVisualElement>(), context)
      {
        WorkflowDefinition = new WcfWorkflowDefinition(wed)
      };
    }

    private string MessageWorkflowInternal(
      KeyValuePair<string, string>[] contextBag,
      string itemId,
      string itemType,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string str = (string) null;
      if (itemId.IsGuid())
      {
        Dictionary<string, string> contextBag1 = new Dictionary<string, string>();
        if (contextBag != null)
          contextBag1 = ((IEnumerable<KeyValuePair<string, string>>) contextBag).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (k => k.Key), (Func<KeyValuePair<string, string>, string>) (v => v.Value));
        Guid guid1 = new Guid(itemId);
        Type type = TypeResolutionService.ResolveType(itemType);
        if (typeof (Content).IsAssignableFrom(type))
        {
          IManager mappedManager = ManagerBase.GetMappedManager(type, providerName);
          if (mappedManager.GetItem(type, guid1) is Content content1)
          {
            Guid guid2 = content1.Status != ContentLifecycleStatus.Master ? content1.OriginalContentId : content1.Id;
            Content content = (Content) mappedManager.GetItem(type, guid2);
            if (typeof (ILifecycleDataItem).IsAssignableFrom(content1.GetType()))
              (mappedManager as ILifecycleManager).Lifecycle.CheckOut((ILifecycleDataItem) content);
            else
              (mappedManager as IContentLifecycleManager).CheckOut(content);
            contextBag1.Add("ContentType", content1.GetType().FullName);
            str = WorkflowManager.MessageWorkflow(guid2, content1.GetType(), providerName, workflowOperation, true, contextBag1);
          }
        }
        else if (typeof (ILifecycleDataItemGeneric).IsAssignableFrom(type))
        {
          IManager mappedManager = ManagerBase.GetMappedManager(type, providerName);
          if (mappedManager.GetItem(type, guid1) is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
          {
            Guid guid3 = lifecycleDataItemGeneric.Status != ContentLifecycleStatus.Master ? lifecycleDataItemGeneric.OriginalContentId : lifecycleDataItemGeneric.Id;
            ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) mappedManager.GetItem(type, guid3);
            if (typeof (ILifecycleDataItem).IsAssignableFrom(lifecycleDataItemGeneric.GetType()))
            {
              ILifecycleManager lifecycleManager = mappedManager as ILifecycleManager;
              lifecycleManager.Lifecycle.CheckOut(lifecycleDataItem);
              lifecycleManager.SaveChanges();
            }
            contextBag1.Add("ContentType", lifecycleDataItemGeneric.GetType().FullName);
            str = WorkflowManager.MessageWorkflow(guid3, lifecycleDataItemGeneric.GetType(), providerName, workflowOperation, true, contextBag1);
          }
        }
        else
        {
          if (type == typeof (PageNode))
          {
            ZoneEditorValidationExtensions.ValidateChange(PageManager.GetManager().GetPageNode(guid1).GetPageData().Id, DesignMediaType.Page, workflowOperation, true);
            contextBag1.Add("OptimizedCopy", "True");
          }
          str = WorkflowManager.MessageWorkflow(guid1, type, providerName, workflowOperation, false, contextBag1);
        }
      }
      return str;
    }

    private IApprovalTrackingRecord GetLastApprovalTrackingRecord(
      IAppSettings settings,
      IApprovalWorkflowItem item,
      CultureInfo cultureInfo)
    {
      IApprovalTrackingRecord approvalTrackingRecord = (IApprovalTrackingRecord) null;
      if (item != null)
        approvalTrackingRecord = !settings.Multilingual ? item.GetCurrentApprovalTrackingRecordFromCache() : item.GetCurrentApprovalTrackingRecordFromCache(cultureInfo);
      return approvalTrackingRecord;
    }

    private CultureInfo GetCultureInfo(IAppSettings settings, string culture)
    {
      CultureInfo cultureInfo = (CultureInfo) null;
      if (settings.Multilingual)
        cultureInfo = !string.IsNullOrEmpty(culture) ? CultureInfo.GetCultureInfo(culture) : settings.DefaultFrontendLanguage;
      return cultureInfo;
    }

    private IApprovalWorkflowItem GetApprovalWorkflowItem(
      Type type,
      string providerName,
      Guid itemId)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(type, providerName);
      return itemId == Guid.Empty ? (IApprovalWorkflowItem) null : mappedManager.GetItem(type, itemId) as IApprovalWorkflowItem;
    }
  }
}
