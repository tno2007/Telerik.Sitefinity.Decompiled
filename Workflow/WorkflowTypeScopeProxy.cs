// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowTypeScopeProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Wrapper for <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionTypeScope" />
  /// </summary>
  public class WorkflowTypeScopeProxy : IWorkflowExecutionTypeScope
  {
    private IEnumerable<string> cultures;
    private Guid siteId;
    private string contentType;
    private string contentFilter;
    private bool includeChildren;
    private bool isActive;
    private IWorkflowItemScopeResolver itemScopeResolver;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowTypeScopeProxy" /> class
    /// by a database object model.
    /// </summary>
    /// <param name="wts">Workflow type scope's database model.</param>
    /// <param name="scopeProxy">Workflow scope proxy.</param>
    public WorkflowTypeScopeProxy(WorkflowTypeScope wts, IWorkflowExecutionScope scopeProxy)
    {
      this.Id = wts.Id;
      this.Scope = scopeProxy;
      this.IsActive = scopeProxy.Definition.IsActive;
      this.SiteId = scopeProxy.SiteId;
      this.Cultures = scopeProxy.Cultures;
      this.ContentType = wts.ContentType;
      this.ContentFilter = wts.ContentFilter;
      this.IncludeChildren = wts.IncludeChildren;
      this.LastModified = wts.LastModified;
      this.ItemScopeResolver = WorkflowTypeScopeProxy.SetItemScopeResolver(wts);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowTypeScopeProxy" /> class
    /// by a database object model.
    /// </summary>
    /// <param name="scopeProxy">Workflow scope proxy.</param>
    public WorkflowTypeScopeProxy(IWorkflowExecutionScope scopeProxy)
    {
      this.Id = Guid.Empty;
      this.Scope = scopeProxy;
      this.IsActive = scopeProxy.Definition.IsActive;
      this.SiteId = scopeProxy.SiteId;
      this.Cultures = scopeProxy.Cultures;
      this.ContentType = string.Empty;
      this.ContentFilter = string.Empty;
      this.IncludeChildren = false;
      this.LastModified = scopeProxy.LastModified;
      this.ItemScopeResolver = (IWorkflowItemScopeResolver) null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowTypeScopeProxy" /> class
    /// with the default values.
    /// </summary>
    protected WorkflowTypeScopeProxy()
    {
      this.Id = Guid.Empty;
      this.SiteId = Guid.Empty;
      this.ContentType = string.Empty;
      this.Cultures = (IEnumerable<string>) new List<string>();
      this.ContentFilter = string.Empty;
      this.LastModified = DateTime.MinValue;
      this.ItemScopeResolver = (IWorkflowItemScopeResolver) null;
    }

    /// <summary>Gets the id of the workflow definition.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the scope.</summary>
    /// <value>The scope.</value>
    public IWorkflowExecutionScope Scope { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the scope is active.
    /// </summary>
    public bool IsActive
    {
      get => this.isActive;
      set
      {
        this.isActive = value;
        this.ChangePriority(ScopePriorityFlags.Active, this.isActive);
      }
    }

    /// <summary>Gets or sets the site id.</summary>
    public Guid SiteId
    {
      get => this.siteId;
      set
      {
        this.siteId = value;
        this.ChangePriority(ScopePriorityFlags.HasSiteScope, this.siteId != Guid.Empty);
      }
    }

    /// <summary>Gets or sets the type of the content.</summary>
    public string ContentType
    {
      get => this.contentType;
      set
      {
        this.contentType = value;
        this.ChangePriority(ScopePriorityFlags.HasContentScope, !this.contentType.IsNullOrEmpty());
      }
    }

    /// <summary>Gets the content filter.</summary>
    public string ContentFilter
    {
      get => this.contentFilter;
      private set => this.contentFilter = value;
    }

    /// <summary>Gets a value indicating whether to include children.</summary>
    /// <value>
    ///   <c>true</c> if include children; otherwise, <c>false</c>.
    /// </value>
    public bool IncludeChildren
    {
      get => this.includeChildren;
      private set => this.includeChildren = value;
    }

    /// <summary>Gets or sets the language cultures.</summary>
    public IEnumerable<string> Cultures
    {
      get => this.cultures;
      set
      {
        this.cultures = value;
        this.ChangePriority(ScopePriorityFlags.HasCultureScope, this.cultures != null && this.cultures.Any<string>());
      }
    }

    /// <summary>Gets the priority.</summary>
    /// <value>The priority.</value>
    internal ScopePriorityFlags Priority { get; private set; }

    /// <summary>Gets the last modified.</summary>
    /// <value>The last modified.</value>
    public DateTime LastModified { get; private set; }

    /// <summary>
    /// Determines whether specified context item is in the workflow scope.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>
    ///   <c>true</c> if the specified item is in the scope; otherwise, <c>false</c>.
    /// </returns>
    public bool IsItemInScope(IWorkflowResolutionContext context) => this.ItemScopeResolver == null || this.ItemScopeResolver.ResolveItem(context, (IWorkflowExecutionTypeScope) this);

    /// <summary>Gets the item scope resolver.</summary>
    internal IWorkflowItemScopeResolver ItemScopeResolver
    {
      get => this.itemScopeResolver;
      private set
      {
        this.itemScopeResolver = value;
        this.ChangePriority(ScopePriorityFlags.HasItemsScope, this.itemScopeResolver != null);
      }
    }

    private static IWorkflowItemScopeResolver SetItemScopeResolver(
      WorkflowTypeScope wts)
    {
      return !wts.ContentType.IsNullOrEmpty() && ObjectFactory.IsTypeRegistered<IWorkflowItemScopeResolver>(wts.ContentType) ? ObjectFactory.Resolve<IWorkflowItemScopeResolver>(wts.ContentType) : (IWorkflowItemScopeResolver) null;
    }

    private void ChangePriority(ScopePriorityFlags priority, bool set)
    {
      if (set)
        this.Priority |= priority;
      else
        this.Priority &= ~priority;
    }
  }
}
