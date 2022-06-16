// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>ViewModel class for the workflow definition.</summary>
  public class WorkflowDefinitionViewModel
  {
    /// <summary>Gets or sets the id of the workflow definition.</summary>
    public string Id { get; set; }

    /// <summary>Gets or sets the title of the workflow definition.</summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the workflow is active or not.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>Gets or sets one of the predefined workflow types.</summary>
    public WorkflowType WorkflowType { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether administrators will entirely skip workflow and
    /// go to the last step.
    /// </summary>
    public bool AllowAdministratorsToSkipWorkflow { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether last level approvers (publishers)
    /// will entirely skip the workflow and go to the last step.
    /// </summary>
    public bool AllowPublishersToSkipWorkflow { get; set; }

    /// <summary>
    /// Gets or sets the date of creation for the workflow definition.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the user name of the user that created the workflow definitions.
    /// </summary>
    public string OwnerUserName { get; set; }

    /// <summary>
    /// Gets the collection of workflow scope objects that define for which content
    /// should workflow be invoked.
    /// </summary>
    public IList<WorkflowScopeViewModel> Scopes { get; set; }

    /// <summary>Gets the collection of workflow level objects</summary>
    public IList<WorkflowLevelViewModel> Levels { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the notes are allowed.
    /// </summary>
    public bool AllowNotes { get; set; }

    /// <summary>
    /// Gets or sets the workflow UI status (depends on wheather workflow is active or inactive).
    /// </summary>
    public string UIStatus { get; set; }

    /// <summary>
    /// Gets or sets one of the predefined workflow types as string.
    /// </summary>
    public string UIWorkflowType { get; set; }

    /// <summary>Gets or sets the UIScopeItem1</summary>
    public string UIScopeItem1 { get; set; }

    /// <summary>Gets or sets the UIScopeItem2</summary>
    public string UIScopeItem2 { get; set; }

    /// <summary>Gets or sets the UIScopeLine3</summary>
    public string UIScopeItem3 { get; set; }
  }
}
