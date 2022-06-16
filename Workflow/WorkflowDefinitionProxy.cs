// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowDefinitionProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Wrapper for <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" />
  /// </summary>
  public class WorkflowDefinitionProxy : IWorkflowExecutionDefinition
  {
    private static WorkflowDefinitionProxy defaultWorkflowDefinition;

    /// <summary>Gets default no-approval-needed workflow definition.</summary>
    public static WorkflowDefinitionProxy DefaultWorkflow
    {
      get
      {
        if (WorkflowDefinitionProxy.defaultWorkflowDefinition == null)
          WorkflowDefinitionProxy.defaultWorkflowDefinition = new WorkflowDefinitionProxy();
        return WorkflowDefinitionProxy.defaultWorkflowDefinition;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowDefinitionProxy" /> class
    /// by a database object model.
    /// </summary>
    /// <param name="wd">Workflow definition's database model.</param>
    public WorkflowDefinitionProxy(WorkflowDefinition wd)
    {
      this.Id = wd.Id;
      this.Title = wd.Title;
      this.WorkflowType = wd.WorkflowType;
      this.Owner = wd.Owner;
      this.AllowAdministratorsToSkipWorkflow = wd.AllowAdministratorsToSkipWorkflow;
      this.AllowPublishersToSkipWorkflow = wd.AllowPublishersToSkipWorkflow;
      this.CustomXamlxUrl = wd.CustomXamlxUrl;
      this.DateCreated = wd.DateCreated;
      this.LastModified = wd.LastModified;
      this.IsActive = wd.IsActive;
      this.AllowNotes = wd.AllowNotes;
      List<WorkflowScopeProxy> workflowScopeProxyList = new List<WorkflowScopeProxy>();
      foreach (WorkflowScope scope in (IEnumerable<WorkflowScope>) wd.Scopes)
      {
        WorkflowScopeProxy workflowScopeProxy = new WorkflowScopeProxy(scope, (IWorkflowExecutionDefinition) this);
        workflowScopeProxyList.Add(workflowScopeProxy);
      }
      this.Scopes = (IEnumerable<IWorkflowExecutionScope>) workflowScopeProxyList;
      List<WorkflowLevelProxy> workflowLevelProxyList = new List<WorkflowLevelProxy>();
      foreach (WorkflowLevel level in (IEnumerable<WorkflowLevel>) wd.Levels)
      {
        WorkflowLevelProxy workflowLevelProxy = new WorkflowLevelProxy(level);
        workflowLevelProxyList.Add(workflowLevelProxy);
      }
      this.Levels = (IEnumerable<IWorkflowExecutionLevel>) workflowLevelProxyList;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowDefinitionProxy" /> class
    /// with the default no-approval-needed workflow.
    /// </summary>
    protected WorkflowDefinitionProxy()
    {
      this.Id = Guid.Empty;
      this.Title = "Default no-approval-needed workflow";
      this.WorkflowType = WorkflowType.Default;
      this.AllowAdministratorsToSkipWorkflow = true;
      this.AllowPublishersToSkipWorkflow = true;
      this.AllowNotes = false;
      this.CustomXamlxUrl = (string) null;
      this.Scopes = (IEnumerable<IWorkflowExecutionScope>) new List<IWorkflowExecutionScope>();
      this.Levels = (IEnumerable<IWorkflowExecutionLevel>) new List<IWorkflowExecutionLevel>();
    }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string Title { get; private set; }

    /// <inheritdoc />
    public WorkflowType WorkflowType { get; private set; }

    /// <inheritdoc />
    public bool AllowAdministratorsToSkipWorkflow { get; private set; }

    /// <inheritdoc />
    public bool AllowPublishersToSkipWorkflow { get; private set; }

    /// <inheritdoc />
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Xamlx is ok.")]
    public string CustomXamlxUrl { get; private set; }

    /// <summary>
    /// Gets the date of creation for the workflow definition.
    /// </summary>
    public DateTime DateCreated { get; private set; }

    /// <summary>
    /// Gets the id of the user that created this workflow definition.
    /// </summary>
    public Guid Owner { get; private set; }

    /// <summary>Gets the time this item was last modified.</summary>
    public DateTime LastModified { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the workflow is active or not.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>Gets the workflow scopes.</summary>
    /// <value>The workflow scopes.</value>
    public IEnumerable<IWorkflowExecutionScope> Scopes { get; private set; }

    /// <summary>Gets the workflow levels.</summary>
    /// <value>The workflow levels.</value>
    public IEnumerable<IWorkflowExecutionLevel> Levels { get; private set; }

    /// <summary>Gets a value indicating whether notes are allowed.</summary>
    public bool AllowNotes { get; private set; }
  }
}
