// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowScopeProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Wrapper for <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionScope" />
  /// </summary>
  public class WorkflowScopeProxy : IWorkflowExecutionScope
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowScopeProxy" /> class
    /// by a database object model.
    /// </summary>
    /// <param name="ws">Workflow scope's database model.</param>
    /// <param name="definitionProxy">Workflow definition proxy.</param>
    public WorkflowScopeProxy(WorkflowScope ws, IWorkflowExecutionDefinition definitionProxy)
    {
      this.Id = ws.Id;
      this.SiteId = ws.GetScopeSiteId();
      this.Cultures = (IEnumerable<string>) new List<string>();
      if (!ws.Language.IsNullOrEmpty())
        this.Cultures = (IEnumerable<string>) new HashSet<string>((IEnumerable<string>) ws.Language.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries));
      this.LastModified = ws.LastModified;
      this.TypeScopes = (IList<WorkflowTypeScopeProxy>) new List<WorkflowTypeScopeProxy>();
      this.Definition = definitionProxy;
      if (ws.TypeScopes.Count > 0)
      {
        foreach (WorkflowTypeScope typeScope in (IEnumerable<WorkflowTypeScope>) ws.TypeScopes)
          this.TypeScopes.Add(new WorkflowTypeScopeProxy(typeScope, (IWorkflowExecutionScope) this));
      }
      else
        this.TypeScopes.Add(new WorkflowTypeScopeProxy((IWorkflowExecutionScope) this));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowScopeProxy" /> class
    /// with the default values.
    /// </summary>
    protected WorkflowScopeProxy()
    {
      this.Id = Guid.Empty;
      this.SiteId = Guid.Empty;
      this.Cultures = (IEnumerable<string>) new List<string>();
      this.TypeScopes = (IList<WorkflowTypeScopeProxy>) new List<WorkflowTypeScopeProxy>();
    }

    /// <summary>Gets the id of the workflow definition.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the workflow definition.</summary>
    public IWorkflowExecutionDefinition Definition { get; internal set; }

    /// <summary>Gets the site id.</summary>
    public Guid SiteId { get; private set; }

    /// <summary>Gets the last modified.</summary>
    public DateTime LastModified { get; private set; }

    /// <summary>Gets the language cultures.</summary>
    public IEnumerable<string> Cultures { get; private set; }

    /// <summary>Gets the workflow type scopes.</summary>
    public IList<WorkflowTypeScopeProxy> TypeScopes { get; private set; }
  }
}
