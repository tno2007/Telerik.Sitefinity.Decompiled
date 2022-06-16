// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.NotifyStateChanger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Activities.Designers;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Workflow.Activities
{
  [Designer(typeof (NotifyStateChangerDesigner))]
  public class NotifyStateChanger : NotificationActivityBase
  {
    /// <summary>Gets or sets the group changer.</summary>
    /// <value>The group changer.</value>
    [RequiredArgument]
    public string State { get; set; }

    /// <summary>Finds the users who need to be notified.</summary>
    /// <param name="context">The context.</param>
    /// <returns>Collection of users.</returns>
    public override IEnumerable<User> GetUsers(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      IWorkflowItem workflowItem1 = (IWorkflowItem) dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
      Guid id = (Guid) dataContext.GetProperties()["itemId"].GetValue((object) dataContext);
      string providerName = (string) dataContext.GetProperties()["providerName"].GetValue((object) dataContext);
      Type type = workflowItem1.GetType();
      string globalTransaction = SystemManager.CurrentContext.GlobalTransaction;
      IWorkflowItem workflowItem2 = (IWorkflowItem) (!string.IsNullOrEmpty(globalTransaction) ? ManagerBase.GetMappedManagerInTransaction(type, providerName, globalTransaction) : ManagerBase.GetMappedManager(type, providerName)).GetItem(type, id);
      string status = this.State;
      ApprovalTrackingRecord approvalTrackingRecord = workflowItem2.GetApprovalRecords().Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (item => item.Status == status)).OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (item => item.LastModified)).FirstOrDefault<ApprovalTrackingRecord>();
      if (approvalTrackingRecord != null)
      {
        foreach (DataProviderBase contextProvider in UserManager.GetManager().GetContextProviders())
        {
          try
          {
            User user = UserManager.GetManager(contextProvider.Name).GetUser(approvalTrackingRecord.UserId);
            if (user != null)
              return (IEnumerable<User>) new List<User>()
              {
                user
              };
          }
          catch (Exception ex)
          {
          }
        }
      }
      return (IEnumerable<User>) new List<User>();
    }
  }
}
