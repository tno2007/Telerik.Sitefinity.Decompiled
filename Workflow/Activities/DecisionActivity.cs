// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.DecisionActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Workflow.Activities.Designers;
using Telerik.Sitefinity.Workflow.Exceptions;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// This activity represents a visualizable decision that user may perform. Optionally,
  /// user can specify the code to be executed upon activity's execution.
  /// </summary>
  [Designer(typeof (DecisionActivityDesigner))]
  public class DecisionActivity : SetApprovalStatusActivity, ICloneable
  {
    private const string invalidOperationExceptionMessage = "Invalid workflow operation";

    public DecisionRestrictionType RestrictionType { get; set; }

    public string AvailableOnlyIfState { get; set; }

    public string AvailableOnlyIfStateNot { get; set; }

    public bool HideIfNoStatus { get; set; }

    /// <summary>
    /// Gets or sets the title of the decision that will be used when rendering the user interface element.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the css class that ought to be applied to the user interface element rendered by this decision.
    /// </summary>
    public string CssClass { get; set; }

    public bool RunAsUICommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the dialog that should be opened in order to gather additional information when the
    /// decision has been performed by the user.
    /// </summary>
    public string ArgumentDialogName { get; set; }

    /// <summary>
    /// Gets or sets the ordinal in which the decisions belonging to the same switch statement ought to be rendered.
    /// </summary>
    public int Ordinal { get; set; }

    /// <summary>
    /// Gets or sets the name of the placeholder in which the activity ought to be rendered.
    /// </summary>
    public PlaceholderName Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the decision ought to persist the item before
    /// entering workflow.
    /// </summary>
    public bool PersistOnDecision { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates wheather the action closes the form.
    /// </summary>
    public bool ClosesForm { get; set; }

    /// <summary>
    /// Result from the execution of the code specified in the "Execute code"
    /// <example>
    /// App.WorkWith().Pages().Get().
    /// </example>
    /// </summary>
    public InArgument<bool> CodeExecutionResult { get; set; }

    /// <summary>
    /// Gets or sets the resource class to vbe used for localizing the action titles. If this is not set the action titles will be usd literaly and will not be localized
    /// </summary>
    /// <value>The resource class.</value>
    public string ResourceClass { get; set; }

    /// <summary>
    /// Represents the name of a command to be executed on the client which might not be exactly related to the workflow (preview for example)
    /// </summary>
    public string ContentCommandName { get; set; }

    /// <summary>Warning to be displayed in monolingual mode</summary>
    /// <remarks>The string represents a resource key</remarks>
    [Description("Warning to be displayed in monolingual mode")]
    public string MonolingualWarning { get; set; }

    /// <summary>Warning to be displayed on in multilingual mode</summary>
    /// <remarks>The string represents a resource key</remarks>
    [Description("Warning to be displayed in multilingual mode")]
    public string MultilingualWarning { get; set; }

    /// <summary>
    /// Gets or sets the value which determines whether this decision activity is hidden in the UI (available through code only).
    /// </summary>
    /// <value>The value which determines whether this decision activity is hidden in the UI (available through code only).</value>
    public bool HideInUI { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the decision will be available only for users that can skip the workflow.
    /// </summary>
    public bool AvailableOnlyIfUserCanSkipWorkflow { get; set; }

    public IList<string> GetRestrictedStates()
    {
      string[] strArray = new string[0];
      string[] source;
      if (this.RestrictionType == DecisionRestrictionType.Inclusive)
        source = this.AvailableOnlyIfState.Split(',');
      else
        source = this.AvailableOnlyIfStateNot.Split(',');
      for (int index = 0; index < ((IEnumerable<string>) source).Count<string>(); ++index)
        source[index] = source[index].Trim();
      return (IList<string>) ((IEnumerable<string>) source).ToList<string>();
    }

    protected override void Execute(CodeActivityContext context)
    {
      this.ValidateContext(context);
      base.Execute(context);
    }

    /// <summary>
    /// Validates if the current operation is valid for the current workflow context. E.g. appplies the decision activity restrictions.
    /// </summary>
    /// <param name="context">The context.</param>
    private void ValidateContext(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      string str = (string) dataContext.GetProperties()["itemStatus"].GetValue((object) dataContext);
      if (string.IsNullOrEmpty(str))
      {
        PropertyDescriptor property = dataContext.GetProperties()["IsItemPublished"];
        if (property != null)
          str = (bool) property.GetValue((object) dataContext) ? "Published" : str;
      }
      if (string.IsNullOrEmpty(str))
      {
        if (this.HideIfNoStatus)
          throw new WorkflowSecurityException("Invalid workflow operation");
      }
      else
      {
        if (this.RestrictionType == DecisionRestrictionType.Inclusive && !this.GetRestrictedStates().Contains(str))
          throw new WorkflowSecurityException("Invalid workflow operation");
        if (this.RestrictionType == DecisionRestrictionType.Exclusive && this.GetRestrictedStates().Contains(str))
          throw new WorkflowSecurityException("Invalid workflow operation");
        if (!this.AvailableOnlyIfUserCanSkipWorkflow)
          return;
        IWorkflowExecutionDefinition wed = (IWorkflowExecutionDefinition) dataContext.GetProperties()["workflowExecutionDefinition"].GetValue((object) dataContext);
        if (!wed.CanUserPublish() && !wed.CanUserSkip())
          throw new WorkflowSecurityException("Invalid workflow operation");
      }
    }

    public object Clone()
    {
      DecisionActivity decisionActivity = new DecisionActivity();
      decisionActivity.ActionName = this.ActionName;
      decisionActivity.ArgumentDialogName = this.ArgumentDialogName;
      decisionActivity.AvailableOnlyIfState = this.AvailableOnlyIfState;
      decisionActivity.AvailableOnlyIfStateNot = this.AvailableOnlyIfStateNot;
      decisionActivity.AvailableOnlyIfUserCanSkipWorkflow = this.AvailableOnlyIfUserCanSkipWorkflow;
      decisionActivity.DisplayName = this.DisplayName;
      decisionActivity.HideIfNoStatus = this.HideIfNoStatus;
      decisionActivity.MonolingualWarning = this.MonolingualWarning;
      decisionActivity.MultilingualWarning = this.MultilingualWarning;
      decisionActivity.Ordinal = this.Ordinal;
      decisionActivity.PersistOnDecision = this.PersistOnDecision;
      decisionActivity.RestrictionType = this.RestrictionType;
      decisionActivity.ResultStatus = this.ResultStatus;
      decisionActivity.RunAsUICommand = this.RunAsUICommand;
      decisionActivity.SynchronizeMultiligualStatuses = this.SynchronizeMultiligualStatuses;
      decisionActivity.Title = this.Title;
      decisionActivity.ResourceClass = this.ResourceClass;
      decisionActivity.Placeholder = this.Placeholder;
      decisionActivity.HideInUI = this.HideInUI;
      decisionActivity.CssClass = this.CssClass;
      decisionActivity.ClosesForm = this.ClosesForm;
      decisionActivity.ContentCommandName = this.ContentCommandName;
      return (object) decisionActivity;
    }
  }
}
