// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PagesWorkflowFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Fluent.Pages
{
  internal class PagesWorkflowFacade
  {
    private PagesFacade pagesFacade;
    private IWorkflowUtils utils;
    private IQueryable<PageNode> pageNodes;

    public PagesWorkflowFacade(PagesFacade pagesFacade, IWorkflowUtils utils)
    {
      this.pagesFacade = pagesFacade;
      this.utils = utils;
    }

    /// <summary>Filters pages by their workflow status.</summary>
    /// <param name="workflowStatuses">The workflow status</param>
    /// <returns>The facade.</returns>
    public virtual PagesWorkflowFacade FilterByWorkflowStatus(
      params string[] workflowStatuses)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PagesWorkflowFacade.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new PagesWorkflowFacade.\u003C\u003Ec__DisplayClass4_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.workflowStatuses = workflowStatuses;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.pagesFacade.PageNodes = this.pagesFacade.PageNodes.Where<PageNode>(Expression.Lambda<Func<PageNode, bool>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
      {
        cDisplayClass40.workflowStatuses,
        (Expression) Expression.Call(p.ApprovalWorkflowState, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())
      }), parameterExpression));
      return this;
    }

    public PagesFacade Done() => this.pagesFacade;

    /// <summary>
    /// WARNING: The method will execute the current query expression. Use when the current built expression is unlikely to return a large collection. Shows only items that have been sent for review/approval/publishing and the current user can review/approve/publish.
    /// </summary>
    /// <returns>The facade.</returns>
    public PagesWorkflowFacade ThatCurrentUserCanApprove()
    {
      this.pagesFacade.PageNodes = this.pagesFacade.PageNodes.ToList<PageNode>().Where<PageNode>((Func<PageNode, bool>) (x =>
      {
        string forWorkflowState = this.utils.GetRequiredActionForWorkflowState(x.ApprovalWorkflowState.ToString());
        return forWorkflowState != null && this.utils.CanUser((IWorkflowItem) x, forWorkflowState);
      })).AsQueryable<PageNode>();
      return this;
    }
  }
}
