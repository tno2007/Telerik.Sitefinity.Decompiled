// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.WorkflowStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A locked by property for retrieving which item is locked.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class WorkflowStatusProperty : CalculatedProperty
  {
    private string workflowState;
    private bool orderAscending = true;

    /// <summary>Gets the workflow state</summary>
    internal string WorkflowState => this.workflowState;

    /// <summary>
    /// Gets a value indicating whether the order is ascending
    /// </summary>
    internal bool OrderAscending => this.orderAscending;

    /// <inheritdoc />
    public override void Initialize(NameValueCollection parameters, Type parentType)
    {
      base.Initialize(parameters, parentType);
      this.workflowState = parameters["WorkflowState"];
      string parameter = parameters["Order"];
      this.orderAscending = parameter != null && parameter == "ASC";
    }

    /// <inheritdoc />
    public override Type ReturnType => typeof (ItemEventInfo);

    /// <inheritdoc />
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      List<ILifecycleDataItemGeneric> list1 = items.Cast<ILifecycleDataItemGeneric>().ToList<ILifecycleDataItemGeneric>();
      if (list1.Count > 0)
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        IList<ILifecycleDataItemGeneric> masterItems = DisplayStatusProperty.GetMasterItems((IList<ILifecycleDataItemGeneric>) list1, manager, culture);
        IList<ILifecycleDataItemGeneric> liveItems = (IList<ILifecycleDataItemGeneric>) null;
        IQueryable<ApprovalTrackingRecord> approvalRecords = IApprovalWorkflowExtensions.GetApprovalRecords((IList<IWorkflowItem>) masterItems.Cast<IWorkflowItem>().ToList<IWorkflowItem>(), manager.Provider as IOpenAccessDataProvider).Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Status == this.workflowState));
        CommonMethods.ExecuteMlLogic<object>((Action<object>) (item =>
        {
          int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture);
          approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid));
        }), (Action<object>) (item =>
        {
          int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
          int cultureLcidInv = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture);
          approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid || x.Culture == cultureLcidInv));
        }), (Action<object>) (item =>
        {
          int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
          approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid));
        }));
        if (this.orderAscending)
          approvalRecords = (IQueryable<ApprovalTrackingRecord>) approvalRecords.OrderBy<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (x => x.DateCreated));
        else
          approvalRecords = (IQueryable<ApprovalTrackingRecord>) approvalRecords.OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (x => x.DateCreated));
        ILookup<Guid, ApprovalTrackingRecord> lookup = approvalRecords.ToLookup<ApprovalTrackingRecord, Guid, ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, Guid>) (x => x.WorkflowItemId), (Func<ApprovalTrackingRecord, ApprovalTrackingRecord>) (y => y));
        foreach (ILifecycleDataItemGeneric key1 in list1)
        {
          ItemEventInfo itemEventInfo1 = (ItemEventInfo) null;
          if (key1 is ILocalizable localizable)
          {
            List<CultureInfo> list2 = ((IEnumerable<CultureInfo>) localizable.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (x => x != CultureInfo.InvariantCulture)).ToList<CultureInfo>();
            if (list2.Count > 0 && !list2.Contains(SystemManager.CurrentContext.Culture))
            {
              values.Add((object) key1, (object) itemEventInfo1);
              continue;
            }
          }
          Guid key2 = key1.Status == ContentLifecycleStatus.Master ? key1.Id : key1.OriginalContentId;
          ApprovalTrackingRecord approvalTrackingRecord = lookup[key2].FirstOrDefault<ApprovalTrackingRecord>();
          ItemEventInfo itemEventInfo2;
          if (approvalTrackingRecord != null)
          {
            itemEventInfo2 = new ItemEventInfo()
            {
              Date = approvalTrackingRecord.DateCreated,
              User = UserProfilesHelper.GetUserDisplayName(approvalTrackingRecord.UserId)
            };
          }
          else
          {
            if (liveItems == null)
              liveItems = DisplayStatusProperty.GetLiveItems((IList<ILifecycleDataItemGeneric>) list1, manager, culture);
            itemEventInfo2 = DisplayStatusProperty.GetFallBackItemInfo(this.workflowState, key1, masterItems, liveItems);
          }
          values.Add((object) key1, (object) itemEventInfo2);
        }
      }
      return (IDictionary<object, object>) values;
    }

    internal new class Constants
    {
      public const string WorkflowPropName = "WorkflowState";
      public const string OrderPropName = "Order";
      public const string OrderAscending = "ASC";
    }
  }
}
