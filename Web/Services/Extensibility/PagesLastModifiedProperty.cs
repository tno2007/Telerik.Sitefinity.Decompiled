// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.PagesLastModifiedProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  internal class PagesLastModifiedProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (DateTime);

    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null || manager == null)
        return (IDictionary<object, object>) values;
      ILookup<Guid, ApprovalTrackingRecord> lookup = IApprovalWorkflowExtensions.GetApprovalRecords((IList<IWorkflowItem>) items.Cast<object>().Select<object, PageSiteNode>((Func<object, PageSiteNode>) (x => PropertyHelpers.GetSiteMapNode(x))).Select<PageSiteNode, WorkflowItemWrapper>((Func<PageSiteNode, WorkflowItemWrapper>) (y => new WorkflowItemWrapper(y))).ToList<WorkflowItemWrapper>().Cast<IWorkflowItem>().ToList<IWorkflowItem>(), (IOpenAccessDataProvider) manager.Provider).OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (x => x.LastModified)).Take<ApprovalTrackingRecord>(1).ToLookup<ApprovalTrackingRecord, Guid, ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, Guid>) (x => x.WorkflowItemId), (Func<ApprovalTrackingRecord, ApprovalTrackingRecord>) (y => y));
      foreach (object key in items)
      {
        PageSiteNode siteMapNode = PropertyHelpers.GetSiteMapNode(key);
        List<ApprovalTrackingRecord> list = lookup[siteMapNode.Id].ToList<ApprovalTrackingRecord>();
        DateTime dateTime;
        if (list.Count > 0)
        {
          DateTime lastModified1 = list[0].LastModified;
          DateTime lastModified2 = siteMapNode.LastModified;
          dateTime = !(lastModified1 > lastModified2) ? siteMapNode.LastModified : lastModified1;
        }
        else
          dateTime = siteMapNode.LastModified;
        values[key] = (object) dateTime;
      }
      return (IDictionary<object, object>) values;
    }
  }
}
