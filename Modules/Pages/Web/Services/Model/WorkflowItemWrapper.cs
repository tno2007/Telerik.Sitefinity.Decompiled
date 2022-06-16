// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WorkflowItemWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  internal class WorkflowItemWrapper : IWorkflowItem, IDataItemProxy
  {
    private string providerName;

    public WorkflowItemWrapper(PageSiteNode siteNode)
    {
      this.Id = siteNode.Id;
      this.providerName = siteNode.PageProviderName;
    }

    public Guid Id { get; private set; }

    public ApprovalTrackingRecordMap ApprovalTrackingRecordMap { get; set; }

    public Type GetActualType() => typeof (PageNode);

    public string GetProviderName() => this.providerName;
  }
}
