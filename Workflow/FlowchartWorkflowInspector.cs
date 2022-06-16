// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.FlowchartWorkflowInspector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// This class is used to provide design time information about the given workflow.
  /// </summary>
  /// <typeparam name="TWorkflow"></typeparam>
  public class FlowchartWorkflowInspector
  {
    private Flowchart root;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:FlowchartWorkflowInspector&lt;TWorkflow&gt;" /> class.
    /// </summary>
    /// <param name="workflowInstance">The workflow instance.</param>
    public FlowchartWorkflowInspector(Flowchart workflowInstance) => this.root = workflowInstance;

    /// <summary>
    /// Finds the workflow activity by it's type and display name (the search is recursive).
    /// </summary>
    /// <typeparam name="TActivity">The type of the activity.</typeparam>
    /// <param name="displayName">The display name of the activity.</param>
    /// <returns>An instance of the workflow activity.</returns>
    public TActivity FindActivityByName<TActivity>(string displayName) where TActivity : Activity => this.FindActivityByNameRecursive<TActivity>(this.root, displayName);

    /// <summary>
    /// Finds the workflow activity by it's type and display name in the given parent activity
    /// (the search is recursive).
    /// </summary>
    /// <typeparam name="TActivity">The type of the activity.</typeparam>
    /// <param name="parent">The parent activity in which the activity ought to be found.</param>
    /// <param name="displayName">The display name of the activity.</param>
    /// <returns>An instance of the workflow activity.</returns>
    public TActivity FindActivityByName<TActivity>(Flowchart parent, string displayName) where TActivity : Activity => this.FindActivityByNameRecursive<TActivity>(parent, displayName);

    /// <summary>
    /// Gets the visual decisions for the given workflow item status and workflow definition.
    /// </summary>
    /// <param name="itemStatus">The status of the <see cref="T:Telerik.Sitefinity.IWorkflowItem" />.</param>
    /// <param name="workflowDefinition">An instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> that defines the given workflow.</param>
    /// <returns>The dictionary of decision activities.</returns>
    [Obsolete("Use GetVisualDecisions(WorkflowStatusContext, IWorkflowExecutionDefinition)")]
    public IDictionary<string, DecisionActivity> GetVisualDecisions(
      WorkflowStatusContext itemStatus,
      WorkflowDefinition workflowDefinition)
    {
      return this.GetVisualDecisions(itemStatus, (IWorkflowExecutionDefinition) new WorkflowDefinitionProxy(workflowDefinition));
    }

    /// <summary>
    /// Gets the visual decisions for the given workflow item status and workflow definition.
    /// </summary>
    /// <param name="itemStatus">The status of the <see cref="T:Telerik.Sitefinity.IWorkflowItem" />.</param>
    /// <param name="workflowDefinition">An instance of the <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> that defines the given workflow.</param>
    /// <returns>The dictionary of decision activities.</returns>
    public IDictionary<string, DecisionActivity> GetVisualDecisions(
      WorkflowStatusContext itemStatus,
      IWorkflowExecutionDefinition workflowDefinition)
    {
      FlowSwitch<string> nodeRecursively = this.FindNodeRecursively<FlowSwitch<string>>(this.GetWorkflowFlowchart(workflowDefinition, itemStatus).StartNode);
      Dictionary<string, DecisionActivity> decisions1 = this.GetDecisions(nodeRecursively, 2, itemStatus, workflowDefinition);
      IDictionary<string, DecisionActivity> decisions2;
      if (decisions1.Count > 0)
      {
        decisions2 = (IDictionary<string, DecisionActivity>) decisions1;
      }
      else
      {
        Flowchart flowchart = (Flowchart) null;
        if (string.IsNullOrEmpty(itemStatus.WorkflowStatus))
        {
          flowchart = ((FlowStep) nodeRecursively.Default).Action as Flowchart;
        }
        else
        {
          foreach (KeyValuePair<string, FlowNode> keyValuePair in (IEnumerable<KeyValuePair<string, FlowNode>>) nodeRecursively.Cases)
          {
            if (keyValuePair.Key == itemStatus.WorkflowStatus)
            {
              Activity action = ((FlowStep) keyValuePair.Value).Action;
              if (action.GetType() == typeof (GuardActivity))
              {
                GuardActivity guardActivity = action as GuardActivity;
                if (!ClaimsManager.IsUnrestricted() && !guardActivity.CanPassGuard(workflowDefinition))
                  return (IDictionary<string, DecisionActivity>) null;
                flowchart = ((FlowStep) ((FlowStep) keyValuePair.Value).Next).Action as Flowchart;
              }
              else if (action.GetType() == typeof (Flowchart))
                flowchart = (Flowchart) action;
            }
          }
          if (flowchart == null && nodeRecursively.Default != null)
            flowchart = ((FlowStep) nodeRecursively.Default).Action as Flowchart;
        }
        decisions2 = (IDictionary<string, DecisionActivity>) this.GetDecisions(this.FindNodeRecursively<FlowSwitch<string>>(flowchart.StartNode), 2, itemStatus, workflowDefinition);
      }
      Dictionary<string, DecisionActivity> decisions3 = FlowchartWorkflowInspector.CloneDecisions(decisions2);
      this.AjdustUiPositions((IDictionary<string, DecisionActivity>) decisions3, workflowDefinition);
      return (IDictionary<string, DecisionActivity>) decisions3;
    }

    /// <summary>Gets the workflow states.</summary>
    /// <param name="workflowDefinition">The workflow definition.</param>
    /// <returns></returns>
    public IEnumerable<string> GetWorkflowStates(
      IWorkflowExecutionDefinition workflowDefinition)
    {
      IEnumerable<string> workflowStates = (IEnumerable<string>) null;
      FlowSwitch<string> nodeRecursively = this.FindNodeRecursively<FlowSwitch<string>>(this.GetWorkflowFlowchart(workflowDefinition).StartNode);
      if (nodeRecursively != null && nodeRecursively.Cases != null)
        workflowStates = nodeRecursively.Cases.Select<KeyValuePair<string, FlowNode>, string>((Func<KeyValuePair<string, FlowNode>, string>) (switchCase => switchCase.Key));
      return workflowStates;
    }

    private FlowNode EvaluateFlowDecision(
      FlowDecision node,
      IWorkflowExecutionDefinition workflowDefinition,
      WorkflowStatusContext itemState)
    {
      PropertyDescriptor property = TypeDescriptor.GetProperties((object) node.Condition)["ExpressionText"];
      return property != null && property.GetValue((object) node.Condition) as string == "IsItemPublished" && !itemState.IsItemPublished ? node.False : node.True;
    }

    private TActivity FindActivityRecursive<TActivity>(
      FlowNode parentNode,
      IWorkflowExecutionDefinition workflowDefinition,
      WorkflowStatusContext itemState)
      where TActivity : Activity
    {
      TActivity activityRecursive = default (TActivity);
      if (parentNode == null)
        throw new ArgumentNullException(nameof (parentNode));
      if (parentNode is FlowDecision)
        activityRecursive = this.FindActivityRecursive<TActivity>(this.EvaluateFlowDecision((FlowDecision) parentNode, workflowDefinition, itemState), workflowDefinition, itemState);
      if ((object) activityRecursive == null && parentNode is FlowStep flowStep)
      {
        if (flowStep.Action.GetType() == typeof (GuardActivity))
        {
          GuardActivity action = flowStep.Action as GuardActivity;
          if (ClaimsManager.IsUnrestricted() || action.CanPassGuard(workflowDefinition))
            activityRecursive = this.FindActivityRecursive<TActivity>(flowStep.Next, workflowDefinition, itemState);
        }
        else if (flowStep.Action != null && flowStep.Action is TActivity)
        {
          GuardActivity action = flowStep.Action as GuardActivity;
          activityRecursive = (TActivity) flowStep.Action;
        }
      }
      return activityRecursive;
    }

    private TFlowNode FindNodeRecursively<TFlowNode>(FlowNode parentNode) where TFlowNode : FlowNode
    {
      if (parentNode.GetType() == typeof (TFlowNode))
        return (TFlowNode) parentNode;
      return parentNode is FlowStep flowStep ? this.FindNodeRecursively<TFlowNode>(flowStep.Next) : default (TFlowNode);
    }

    private TActivity FindActivityByNameRecursive<TActivity>(Flowchart parent, string displayName) where TActivity : Activity
    {
      foreach (FlowNode node in parent.Nodes)
      {
        if (node is FlowStep flowStep)
        {
          if (flowStep.Action is TActivity action && action.DisplayName == displayName)
            return action;
          if (flowStep.Action.GetType() == typeof (Flowchart))
            this.FindActivityByNameRecursive<TActivity>((Flowchart) flowStep.Action, displayName);
        }
      }
      return default (TActivity);
    }

    private Dictionary<string, DecisionActivity> GetDecisions(
      FlowSwitch<string> decisionFlowSwitch,
      int depth,
      WorkflowStatusContext itemState,
      IWorkflowExecutionDefinition workflowDefinition)
    {
      Dictionary<string, DecisionActivity> decisions = new Dictionary<string, DecisionActivity>();
      if (decisionFlowSwitch != null)
      {
        foreach (KeyValuePair<string, FlowNode> keyValuePair in (IEnumerable<KeyValuePair<string, FlowNode>>) decisionFlowSwitch.Cases)
        {
          string key = keyValuePair.Key;
          DecisionActivity activityRecursive = this.FindActivityRecursive<DecisionActivity>(keyValuePair.Value, workflowDefinition, itemState);
          if (activityRecursive != null)
          {
            if (string.IsNullOrEmpty(itemState.WorkflowStatus))
            {
              if (activityRecursive.HideIfNoStatus)
                continue;
            }
            else
            {
              if (activityRecursive.RestrictionType == DecisionRestrictionType.Inclusive)
              {
                if (!activityRecursive.GetRestrictedStates().Contains(itemState.WorkflowStatus))
                  continue;
              }
              else if (activityRecursive.RestrictionType == DecisionRestrictionType.Exclusive && activityRecursive.GetRestrictedStates().Contains(itemState.WorkflowStatus))
                continue;
              if (activityRecursive.AvailableOnlyIfUserCanSkipWorkflow && !workflowDefinition.CanUserSkip())
                continue;
            }
            if (activityRecursive.ContentCommandName == "mobile-preview")
            {
              if (LicenseState.CheckIsModuleLicensedInCurrentDomain("01F89003-7A52-4C08-BA60-45C8B8824B38"))
                decisions.Add(key, activityRecursive);
            }
            else
              decisions.Add(key, activityRecursive);
          }
        }
      }
      return decisions;
    }

    private Flowchart GetWorkflowFlowchart(
      IWorkflowExecutionDefinition workflowDefinition,
      WorkflowStatusContext itemStatus)
    {
      Flowchart flowchart = (Flowchart) null;
      FlowNode startNode1;
      if (this.root.StartNode is FlowDecision startNode2)
      {
        if (workflowDefinition != null && workflowDefinition.CanUserSkip())
          return this.FindActivityRecursive<Flowchart>(startNode2.True, workflowDefinition, itemStatus);
        startNode1 = startNode2.False;
      }
      else
        startNode1 = this.root.StartNode;
      FlowSwitch<WorkflowType> nodeRecursively = this.FindNodeRecursively<FlowSwitch<WorkflowType>>(startNode1);
      if (!(nodeRecursively.Expression.ResultType == typeof (WorkflowType)))
        throw new InvalidOperationException("The first FlowSwitch in your workflow must be of type FlowSwitch<WorkflowType>");
      if (workflowDefinition == null)
        return ((FlowStep) nodeRecursively.Default).Action as Flowchart;
      foreach (KeyValuePair<WorkflowType, FlowNode> keyValuePair in (IEnumerable<KeyValuePair<WorkflowType, FlowNode>>) nodeRecursively.Cases)
      {
        if (keyValuePair.Key == workflowDefinition.WorkflowType)
          flowchart = ((FlowStep) keyValuePair.Value).Action as Flowchart;
      }
      return flowchart ?? ((FlowStep) nodeRecursively.Default).Action as Flowchart;
    }

    private Flowchart GetWorkflowFlowchart(IWorkflowExecutionDefinition workflowDefinition)
    {
      FlowSwitch<WorkflowType> nodeRecursively = this.FindNodeRecursively<FlowSwitch<WorkflowType>>(this.root.StartNode is FlowDecision ? (this.root.StartNode as FlowDecision).False : (FlowNode) null);
      if (nodeRecursively == null || !(nodeRecursively.Expression.ResultType == typeof (WorkflowType)))
        throw new InvalidOperationException("The first FlowSwitch in your workflow must be of type FlowSwitch<WorkflowType>");
      return workflowDefinition == null ? ((FlowStep) nodeRecursively.Default).Action as Flowchart : nodeRecursively.Cases.Where<KeyValuePair<WorkflowType, FlowNode>>((Func<KeyValuePair<WorkflowType, FlowNode>, bool>) (workflowCase => workflowCase.Key == workflowDefinition.WorkflowType)).Select<KeyValuePair<WorkflowType, FlowNode>, Flowchart>((Func<KeyValuePair<WorkflowType, FlowNode>, Flowchart>) (workflowCase => ((FlowStep) workflowCase.Value).Action as Flowchart)).FirstOrDefault<Flowchart>() ?? ((FlowStep) nodeRecursively.Default).Action as Flowchart;
    }

    private void AjdustUiPositions(
      IDictionary<string, DecisionActivity> decisions,
      IWorkflowExecutionDefinition wed)
    {
      if (!wed.CanUserSkip())
        return;
      bool flag = decisions.Any<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (d => d.Key == "Reject"));
      foreach (KeyValuePair<string, DecisionActivity> decision in (IEnumerable<KeyValuePair<string, DecisionActivity>>) decisions)
      {
        DecisionActivity decisionActivity = decision.Value;
        string key = decision.Key;
        if (key == "Publish" || key == "UploadPublished")
          decisionActivity.Placeholder = PlaceholderName.MainAction;
        else if (key == "Reject" || key == "UploadDraft" || key == "Upload")
        {
          decisionActivity.Placeholder = PlaceholderName.SecondaryActions;
          decisionActivity.Ordinal = 0;
        }
        else if (key == "Preview")
        {
          decisionActivity.Placeholder = PlaceholderName.SecondaryActions;
          decisionActivity.Ordinal = 1;
        }
        else
          decisionActivity.Placeholder = !key.StartsWith("SaveAs") && !(key == "SaveDraft") || flag ? PlaceholderName.OtherActions : PlaceholderName.SecondaryActions;
      }
    }

    private static Dictionary<string, DecisionActivity> CloneDecisions(
      IDictionary<string, DecisionActivity> decisions)
    {
      Dictionary<string, DecisionActivity> dictionary = new Dictionary<string, DecisionActivity>();
      foreach (KeyValuePair<string, DecisionActivity> decision in (IEnumerable<KeyValuePair<string, DecisionActivity>>) decisions)
      {
        DecisionActivity decisionActivity = decision.Value.Clone() as DecisionActivity;
        dictionary.Add(decision.Key, decisionActivity);
      }
      return dictionary;
    }
  }
}
