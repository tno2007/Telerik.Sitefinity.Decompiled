// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.GuardActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.ComponentModel;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Workflow.Activities.Designers;
using Telerik.Sitefinity.Workflow.Exceptions;

namespace Telerik.Sitefinity.Workflow.Activities
{
  [Designer(typeof (GuardActivityDeigner))]
  public class GuardActivity : CodeActivity
  {
    /// <summary>
    /// Gets or sets the deny mode - meaning the WorkflowPermissions specified roles will be denied rather then allowed
    /// </summary>
    /// <value>
    /// The deny mode. By default this is false and the activity works in Allow mode
    /// </value>
    public bool DenyMode { get; set; }

    /// <summary>Gets or sets the workflow permissions.</summary>
    /// <value>The workflow permissions.</value>
    public string WorkflowPermissions { get; set; }

    /// <summary>Gets or sets the content permissions.</summary>
    /// <value>The content permissions.</value>
    public string ContentPermissions { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      if (ClaimsManager.IsUnrestricted())
        return;
      if (!this.CanPassGuard(ActivitiesHelper.ExecutionDefinitionFromDataContext(context.DataContext)))
      {
        if (this.DenyMode)
          throw new WorkflowSecurityException(string.Format("User with role(s): {0} is denied access", (object) this.WorkflowPermissions));
        throw new WorkflowSecurityException(string.Format("User doesn't have role(s):  {0} and is denied access", (object) this.WorkflowPermissions));
      }
      if (string.IsNullOrEmpty(this.ContentPermissions))
        return;
      PropertyDescriptor property = context.DataContext.GetProperties()["workflowItem"];
      if (property == null || !(property.GetValue((object) context.DataContext) is ISecuredObject securedContent))
        return;
      this.CheckContentPermissions(securedContent);
    }

    /// <summary>
    /// Determines whether this activity can pass guard for the specified workflow definition.
    /// </summary>
    /// <param name="workflowDefinition">The workflow definition.</param>
    /// <returns>
    ///   <c>true</c> if this activity can pass guard for the specified workflow definition; otherwise, <c>false</c>.
    /// </returns>
    internal bool CanPassGuard(IWorkflowExecutionDefinition workflowDefinition)
    {
      if (string.IsNullOrEmpty(this.WorkflowPermissions) || workflowDefinition.CanUserSkip())
        return true;
      string workflowPermissions = this.WorkflowPermissions;
      string[] separator = new string[1]{ "," };
      foreach (string str in workflowPermissions.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        if (workflowDefinition.CanUser(str.Trim()))
          return !this.DenyMode;
      }
      return this.DenyMode;
    }

    /// <summary>Checks the content item permissions.</summary>
    /// <param name="securedContent">The secured content</param>
    /// <returns></returns>
    private bool CheckContentPermissions(ISecuredObject securedContent)
    {
      if (string.IsNullOrEmpty(this.ContentPermissions))
        return true;
      string contentPermissions = this.ContentPermissions;
      char[] chArray1 = new char[1]{ ',' };
      foreach (string str1 in contentPermissions.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '.' };
        string[] strArray = str1.Split(chArray2);
        string permissionSet = strArray[0];
        string str2 = strArray[1];
        if (!securedContent.IsGranted(permissionSet, str2))
          return false;
      }
      return true;
    }
  }
}
