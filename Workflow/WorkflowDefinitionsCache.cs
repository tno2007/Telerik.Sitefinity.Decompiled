// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowDefinitionsCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  internal class WorkflowDefinitionsCache : IWorkflowDefinitionsCache
  {
    private readonly IDictionary<Guid, WorkflowDefinitionProxy> definitions;
    private const string WorkflowDefinitionsKey = "sf_workflow_definitions_cache";
    private static readonly object DefinitionsCacheSync = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowDefinitionsCache" /> class.
    /// </summary>
    /// <param name="workflowDefinitions">The workflow definitions.</param>
    internal WorkflowDefinitionsCache(
      IEnumerable<WorkflowDefinition> workflowDefinitions)
    {
      this.DateCached = DateTime.UtcNow;
      List<WorkflowDefinitionProxy> source = new List<WorkflowDefinitionProxy>();
      foreach (WorkflowDefinition workflowDefinition in workflowDefinitions)
      {
        WorkflowDefinitionProxy workflowDefinitionProxy = new WorkflowDefinitionProxy(workflowDefinition);
        source.Add(workflowDefinitionProxy);
      }
      this.definitions = (IDictionary<Guid, WorkflowDefinitionProxy>) source.OrderByDescending<WorkflowDefinitionProxy, DateTime>((Func<WorkflowDefinitionProxy, DateTime>) (w => w.LastModified)).ToDictionary<WorkflowDefinitionProxy, Guid, WorkflowDefinitionProxy>((Func<WorkflowDefinitionProxy, Guid>) (w => w.Id), (Func<WorkflowDefinitionProxy, WorkflowDefinitionProxy>) (w => w));
    }

    /// <summary>TODO workflow document</summary>
    /// <param name="id">TODO workflow document Id</param>
    /// <returns>TODO workflow document return</returns>
    public WorkflowDefinitionProxy GetById(Guid id)
    {
      WorkflowDefinitionProxy byId;
      this.definitions.TryGetValue(id, out byId);
      return byId;
    }

    /// <summary>
    /// Returns workflow definitions ordered by priority and last modified date.
    /// </summary>
    /// <returns>TODO workflow document return</returns>
    public IEnumerable<WorkflowDefinitionProxy> GetAll() => (IEnumerable<WorkflowDefinitionProxy>) this.definitions.Values;

    /// <summary>
    /// Returns workflow definition scopes ordered by priority and last modified date.
    /// </summary>
    /// <returns>All workflow scopes from cache</returns>
    public IEnumerable<WorkflowTypeScopeProxy> GetAllScopes() => (IEnumerable<WorkflowTypeScopeProxy>) this.definitions.Values.SelectMany<WorkflowDefinitionProxy, IWorkflowExecutionScope>((Func<WorkflowDefinitionProxy, IEnumerable<IWorkflowExecutionScope>>) (wd => wd.Scopes)).SelectMany<IWorkflowExecutionScope, WorkflowTypeScopeProxy>((Func<IWorkflowExecutionScope, IEnumerable<WorkflowTypeScopeProxy>>) (ws => (IEnumerable<WorkflowTypeScopeProxy>) ws.TypeScopes)).OrderByDescending<WorkflowTypeScopeProxy, ScopePriorityFlags>((Func<WorkflowTypeScopeProxy, ScopePriorityFlags>) (wts => wts.Priority)).ThenByDescending<WorkflowTypeScopeProxy, DateTime>((Func<WorkflowTypeScopeProxy, DateTime>) (ws => ws.LastModified));

    /// <summary>TODO workflow document</summary>
    /// <returns>TODO workflow document return</returns>
    internal static WorkflowDefinitionsCache GetCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      if (!(cacheManager["sf_workflow_definitions_cache"] is WorkflowDefinitionsCache cache1))
      {
        lock (WorkflowDefinitionsCache.DefinitionsCacheSync)
        {
          if (!(cacheManager["sf_workflow_definitions_cache"] is WorkflowDefinitionsCache cache1))
          {
            WorkflowManager manager = WorkflowManager.GetManager();
            using (new ElevatedModeRegion((IManager) manager))
              cache1 = new WorkflowDefinitionsCache((IEnumerable<WorkflowDefinition>) manager.GetWorkflowDefinitions().Include<WorkflowDefinition>((Expression<Func<WorkflowDefinition, object>>) (w => w.Levels)).Include<WorkflowDefinition>((Expression<Func<WorkflowDefinition, object>>) (w => w.Scopes)).Include<WorkflowDefinition>((Expression<Func<WorkflowDefinition, object>>) (w => w.Scopes.Select<WorkflowScope, IList<WorkflowTypeScope>>((Func<WorkflowScope, IList<WorkflowTypeScope>>) (s => s.TypeScopes)))).Include<WorkflowDefinition>((Expression<Func<WorkflowDefinition, object>>) (w => w.Levels.Select<WorkflowLevel, IList<WorkflowPermission>>((Func<WorkflowLevel, IList<WorkflowPermission>>) (l => l.Permissions)))));
            cacheManager.Add("sf_workflow_definitions_cache", (object) cache1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowDefinition), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowLevel), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowScope), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowTypeScope), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowPermission), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteItemLink), typeof (WorkflowScope).FullName), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return cache1;
    }

    internal DateTime DateCached { get; private set; }
  }
}
