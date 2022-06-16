// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Data.WorkflowDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Sitefinity.Workflow.Model.DurableInstancing;

namespace Telerik.Sitefinity.Workflow.Data
{
  /// <summary>
  /// Defines the basic functionality that should be implemented by workflow data providers
  /// </summary>
  public abstract class WorkflowDataProvider : DataProviderBase, IMultisiteEnabledProvider
  {
    private static Type[] knownTypes;
    private string[] supportedPermissionSets = new string[1]
    {
      "WorkflowDefinition"
    };
    private IDictionary<string, string> permissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        "WorkflowDefinition",
        "WorkflowActionPermissionsListTitle"
      }
    };

    /// <summary>Creates the instance.</summary>
    /// <returns></returns>
    public abstract Instance CreateInstance();

    /// <summary>Creates the instance by id.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract Instance CreateInstance(Guid id);

    /// <summary>Gets the instance by id.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract Instance GetInstance(Guid id);

    /// <summary>Gets the instances as IQueryable object.</summary>
    /// <returns></returns>
    public abstract IQueryable<Instance> GetInstances();

    /// <summary>Deletes the specified instance.</summary>
    /// <param name="instance">The instance.</param>
    public abstract void Delete(Instance instance);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    [ValuePermission("WorkflowDefinition", new string[] {"Create"})]
    public abstract Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type with the specified id.
    /// </summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    [ValuePermission("WorkflowDefinition", new string[] {"Create"})]
    public abstract Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition(
      Guid workflowDefinitionId);

    /// <summary>Gets the workflow definition by its id.</summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    [ValuePermission("WorkflowDefinition", new string[] {"View"})]
    public abstract Telerik.Sitefinity.Workflow.Model.WorkflowDefinition GetWorkflowDefinition(
      Guid workflowDefinitionId);

    /// <summary>Gets the query of the workflow definitions.</summary>
    /// <returns>Query of workflow definitions.</returns>
    [EnumeratorPermission("WorkflowDefinition", new string[] {"View"})]
    public abstract IQueryable<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition> GetWorkflowDefinitions();

    /// <summary>Deletes a workflow definition.</summary>
    /// <param name="workflowDefinition">The instance of workflow definition to be deleted.</param>
    [ParameterPermission("workflowDefinition", "WorkflowDefinition", new string[] {"Delete"})]
    public abstract void Delete(Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public abstract WorkflowScope CreateWorkflowScope();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowScopeId">Id of the workflow scope.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public abstract WorkflowScope CreateWorkflowScope(Guid workflowScopeId);

    /// <summary>Gets the workflow scope by its id.</summary>
    /// <param name="workflowScopeId">Id of the workflow scope to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public abstract WorkflowScope GetWorkflowScope(Guid workflowScopeId);

    /// <summary>Gets the query of the workflow scopes.</summary>
    /// <returns>Query of workflow scopes.</returns>
    public abstract IQueryable<WorkflowScope> GetWorkflowScopes();

    /// <summary>Deletes a workflow scope.</summary>
    /// <param name="workflowScope">The instance of workflow scope to be deleted.</param>
    public abstract void Delete(WorkflowScope workflowScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public abstract WorkflowTypeScope CreateWorkflowTypeScope();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public abstract WorkflowTypeScope CreateWorkflowTypeScope(
      Guid workflowTypeScopeId);

    /// <summary>Gets the workflow type scope by its id.</summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public abstract WorkflowTypeScope GetWorkflowTypeScope(Guid workflowTypeScopeId);

    /// <summary>Gets the query of the workflow type scopes.</summary>
    /// <returns>Query of workflow type scopes.</returns>
    public abstract IQueryable<WorkflowTypeScope> GetWorkflowTypeScopes();

    /// <summary>Deletes a workflow type scope.</summary>
    /// <param name="workflowTypeScope">The instance of workflow type scope to be deleted.</param>
    public abstract void Delete(WorkflowTypeScope workflowTypeScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public abstract WorkflowLevel CreateWorkflowLevel();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type with the specified id.
    /// </summary>
    /// <param name="workflowLevelId">Id of the workflow level.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public abstract WorkflowLevel CreateWorkflowLevel(Guid workflowLevelId);

    /// <summary>Gets the workflow level by its id.</summary>
    /// <param name="workflowLevelId">Id of the workflow level to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public abstract WorkflowLevel GetWorkflowLevel(Guid workflowLevelId);

    /// <summary>Gets the query of the workflow levels.</summary>
    /// <returns>Query of workflow levels.</returns>
    public abstract IQueryable<WorkflowLevel> GetWorkflowLevels();

    /// <summary>Deletes a workflow level.</summary>
    /// <param name="workflowLevel">The instance of workflow level to be deleted.</param>
    public abstract void Delete(WorkflowLevel workflowLevel);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public abstract WorkflowPermission CreateWorkflowPermission();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type with the specified id.
    /// </summary>
    /// <param name="workflowPermissionId">Id of the workflow permission.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public abstract WorkflowPermission CreateWorkflowPermission(
      Guid workflowPermissionId);

    /// <summary>Gets the workflow permission by its id.</summary>
    /// <param name="workflowPermissionId">Id of the workflow permission to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public abstract WorkflowPermission GetWorkflowPermission(
      Guid workflowPermissionId);

    /// <summary>Gets the query of the workflow permissions.</summary>
    /// <returns>Query of workflow permissions.</returns>
    public abstract IQueryable<WorkflowPermission> GetWorkflowPermissions();

    /// <summary>Deletes a workflow permission.</summary>
    /// <param name="workflowPermission">The instance of workflow permission to be deleted.</param>
    public abstract void Delete(WorkflowPermission workflowPermission);

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets => this.supportedPermissionSets;

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition))
        return (object) this.CreateWorkflowDefinition(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      return itemType == typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition) ? (object) this.GetWorkflowDefinition(id) : base.GetItem(itemType, id);
    }

    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (!(itemType == typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetWorkflowDefinitions().Where<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>((Expression<Func<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition, bool>>) (def => def.Id == id)).FirstOrDefault<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition))
        return (IEnumerable) DataProviderBase.SetExpressions<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>(this.GetWorkflowDefinitions(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Instance))
        return (IEnumerable) DataProviderBase.SetExpressions<Instance>(this.GetInstances(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      Type type1 = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      this.providerDecorator.DeletePermissions(item);
      Type type2 = typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition);
      if (!(type1 == type2))
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      this.Delete((Telerik.Sitefinity.Workflow.Model.WorkflowDefinition) item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes()
    {
      if (WorkflowDataProvider.knownTypes == null)
        WorkflowDataProvider.knownTypes = new Type[2]
        {
          typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition),
          typeof (Instance)
        };
      return WorkflowDataProvider.knownTypes;
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (WorkflowDataProvider);

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission1 = this.CreatePermission("WorkflowDefinition", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "View");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("WorkflowDefinition", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "Modify", "Delete");
      root.Permissions.Add(permission2);
    }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permission set object titles.</value>
    public override IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.permissionsetObjectTitleResKeys;
      set => this.permissionsetObjectTitleResKeys = value;
    }

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition), "WorkflowDefinition", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public abstract SiteItemLink CreateSiteItemLink();

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public abstract void Delete(SiteItemLink link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public abstract void DeleteLinksForItem(IDataItem item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<SiteItemLink> GetSiteItemLinks();

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public abstract SiteItemLink AddItemLink(Guid siteId, IDataItem item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<T> GetSiteItems<T>(Guid siteId) where T : IDataItem;
  }
}
