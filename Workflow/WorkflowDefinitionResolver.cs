// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowDefinitionResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Multisite;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Default implementation of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowDefinitionResolver" />, which
  /// uses definitions defined in the database.
  /// </summary>
  public class WorkflowDefinitionResolver : IWorkflowDefinitionResolver
  {
    /// <summary>
    /// Finds the workflow definition to be used for a particular item.
    /// Searches definitions in the database and selects the most specific
    /// applicable one. If more than one definitions match and are equally
    /// specific, then the most recent one will be selected.
    /// </summary>
    /// <param name="context">Contains parameters describing the content item.</param>
    /// <returns>Selected workflow.</returns>
    public virtual IWorkflowExecutionDefinition ResolveWorkflowExecutionDefinition(
      IWorkflowResolutionContext context)
    {
      string content = context.ContentType != (Type) null ? context.ContentType.FullName : (string) null;
      string culture = context.Culture != null ? context.Culture.Name : (string) null;
      ISite site = context.Site != null ? context.Site : (ISite) null;
      Guid contentId1 = context.ContentId;
      Guid contentId2 = context.ContentId;
      return this.GetCache().GetAllScopes().FirstOrDefault<WorkflowTypeScopeProxy>((Func<WorkflowTypeScopeProxy, bool>) (ws => ws.IsActive && (ws.SiteId == Guid.Empty || site != null && ws.SiteId == site.Id) && (!ws.Cultures.Any<string>() || culture != null && ws.Cultures.Contains<string>(culture)) && (ws.ContentType.IsNullOrEmpty() || content != null && ws.ContentType == content) && ws.IsItemInScope(context)))?.Scope.Definition;
    }

    /// <summary>Gets workflow definition from the database by its id.</summary>
    /// <param name="id">Id of the workflow definition</param>
    /// <returns>Definition as defined in the database, or default (no-approval-needed)
    /// if id was not in the database.</returns>
    public virtual IWorkflowExecutionDefinition GetWorkflowExecutionDefinition(
      Guid id)
    {
      return id == Guid.Empty ? (IWorkflowExecutionDefinition) WorkflowDefinitionProxy.DefaultWorkflow : (IWorkflowExecutionDefinition) this.GetCache().GetById(id);
    }

    /// <summary>Gets the cache containing workflow definitions.</summary>
    /// <returns>Returns the workflow definitions cache.</returns>
    private WorkflowDefinitionsCache GetCache() => WorkflowDefinitionsCache.GetCache();
  }
}
