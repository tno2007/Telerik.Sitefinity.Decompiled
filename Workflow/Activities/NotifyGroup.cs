// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.NotifyGroup
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Workflow.Activities.Designers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  [Designer(typeof (NotifyGroupDesigner))]
  public class NotifyGroup : NotificationActivityBase
  {
    /// <summary>Gets or sets the group.</summary>
    /// <value>The group.</value>
    [RequiredArgument]
    public string Group { get; set; }

    /// <summary>Gets the emails.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override List<string> GetEmails(CodeActivityContext context)
    {
      string actionName = this.Group;
      IWorkflowExecutionLevel workflowExecutionLevel = ActivitiesHelper.ExecutionDefinitionFromDataContext(context.DataContext).Levels.Where<IWorkflowExecutionLevel>((Func<IWorkflowExecutionLevel, bool>) (wl => wl.ActionName == actionName)).FirstOrDefault<IWorkflowExecutionLevel>();
      return workflowExecutionLevel != null && workflowExecutionLevel.CustomEmailRecipients != null ? workflowExecutionLevel.CustomEmailRecipients.ToList<string>() : new List<string>();
    }

    public override IEnumerable<User> GetUsers(CodeActivityContext context)
    {
      HashSet<User> users = new HashSet<User>();
      List<IWorkflowExecutionPermission> executionPermissionList = new List<IWorkflowExecutionPermission>();
      IWorkflowExecutionDefinition executionDefinition = ActivitiesHelper.ExecutionDefinitionFromDataContext(context.DataContext);
      string actionName = this.Group;
      IWorkflowExecutionLevel workflowExecutionLevel = executionDefinition.Levels.Where<IWorkflowExecutionLevel>((Func<IWorkflowExecutionLevel, bool>) (wl => wl.ActionName == actionName)).FirstOrDefault<IWorkflowExecutionLevel>();
      if (workflowExecutionLevel != null)
      {
        foreach (IWorkflowExecutionPermission permission in workflowExecutionLevel.Permissions)
        {
          if (permission.PrincipalType == WorkflowPrincipalType.User)
          {
            if (workflowExecutionLevel.NotifyApprovers)
            {
              User user = UserManager.FindUser(Guid.Parse(permission.PrincipalId));
              if (user != null)
                users.Add(user);
            }
          }
          else
          {
            ProvidersCollection<RoleDataProvider> staticProviders = RoleManager.GetManager().StaticProviders;
            Guid id = Guid.Parse(permission.PrincipalId);
            foreach (RoleDataProvider roleDataProvider in (Collection<RoleDataProvider>) staticProviders)
            {
              try
              {
                RoleManager manager = RoleManager.GetManager(roleDataProvider.Name);
                Role role = manager.GetRole(id);
                if (role != null)
                {
                  ApplicationRole applicationRole = Config.Get<SecurityConfig>().ApplicationRoles["Administrators"];
                  if (role.Id == applicationRole.Id && workflowExecutionLevel.NotifyAdministrators)
                    users.UnionWith(manager.GetUsersInRole(role.Id).Where<User>((Func<User, bool>) (u => u != null)));
                  else if (role.Id != applicationRole.Id)
                  {
                    if (workflowExecutionLevel.NotifyApprovers)
                      users.UnionWith(manager.GetUsersInRole(role.Id).Where<User>((Func<User, bool>) (u => u != null)));
                  }
                }
              }
              catch (ItemNotFoundException ex)
              {
              }
            }
          }
        }
      }
      return (IEnumerable<User>) users;
    }
  }
}
