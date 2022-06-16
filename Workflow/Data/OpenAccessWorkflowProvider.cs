// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Data.OpenAccessWorkflowProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Upgrades;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Sitefinity.Workflow.Model.DurableInstancing;

namespace Telerik.Sitefinity.Workflow.Data
{
  /// <summary>Implements the workflow data layer with OpenAccess.</summary>
  public class OpenAccessWorkflowProvider : 
    WorkflowDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IMultisiteEnabledOAProvider,
    IOpenAccessUpgradableProvider
  {
    /// <summary>Creates the instance.</summary>
    /// <returns></returns>
    public override Instance CreateInstance() => this.CreateInstance(this.GetNewGuid());

    /// <summary>Creates the instance by id.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override Instance CreateInstance(Guid id)
    {
      Instance entity = new Instance()
      {
        ApplicationName = this.ApplicationName,
        LastModified = DateTime.UtcNow,
        CreationTime = DateTime.UtcNow
      };
      entity.LastModified = DateTime.UtcNow;
      entity.LastUpdated = DateTime.UtcNow;
      entity.PendingTimer = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the instance by id.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override Instance GetInstance(Guid id)
    {
      Instance itemById = this.GetContext().GetItemById<Instance>(id.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the instances as IQueryable object.</summary>
    /// <returns></returns>
    public override IQueryable<Instance> GetInstances()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Instance>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Instance>((Expression<Func<Instance, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes the specified instance.</summary>
    /// <param name="instance">The instance.</param>
    public override void Delete(Instance instance)
    {
      if (instance == null)
        throw new ArgumentNullException(nameof (instance));
      this.GetContext().Remove((object) instance);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type.
    /// </summary>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.
    /// </returns>
    public override Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition() => this.CreateWorkflowDefinition(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type with the specified id.
    /// </summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.
    /// </returns>
    public override Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition(
      Guid workflowDefinitionId)
    {
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition = new Telerik.Sitefinity.Workflow.Model.WorkflowDefinition();
      workflowDefinition.ApplicationName = this.ApplicationName;
      workflowDefinition.Id = workflowDefinitionId;
      workflowDefinition.LastModified = DateTime.UtcNow;
      workflowDefinition.DateCreated = DateTime.UtcNow;
      workflowDefinition.Owner = SecurityManager.GetCurrentUserId();
      workflowDefinition.InheritsPermissions = true;
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot() ?? throw new InvalidOperationException(string.Format(Res.Get<SecurityResources>().NoSecurityRoot, (object) typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition).AssemblyQualifiedName)), (ISecuredObject) workflowDefinition);
      this.GetContext().Add((object) workflowDefinition);
      return workflowDefinition;
    }

    /// <summary>Gets the workflow definition by its id.</summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition to be retrieved.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.
    /// </returns>
    public override Telerik.Sitefinity.Workflow.Model.WorkflowDefinition GetWorkflowDefinition(
      Guid workflowDefinitionId)
    {
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition itemById = this.GetContext().GetItemById<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>(workflowDefinitionId.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the query of the workflow definitions.</summary>
    /// <returns>Query of workflow definitions.</returns>
    public override IQueryable<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition> GetWorkflowDefinitions()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition>((Expression<Func<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a workflow definition.</summary>
    /// <param name="workflowDefinition">The instance of workflow definition to be deleted.</param>
    public override void Delete(Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition)
    {
      ISecuredObject securityRoot = this.GetSecurityRoot();
      if (securityRoot != null)
        this.providerDecorator.DeletePermissionsInheritanceAssociation(securityRoot, (ISecuredObject) workflowDefinition);
      this.GetContext().Remove((object) workflowDefinition);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type.
    /// </summary>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.
    /// </returns>
    public override WorkflowScope CreateWorkflowScope() => this.CreateWorkflowScope(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowScopeId">Id of the workflow scope.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.
    /// </returns>
    public override WorkflowScope CreateWorkflowScope(Guid workflowScopeId)
    {
      WorkflowScope entity = new WorkflowScope();
      entity.ApplicationName = this.ApplicationName;
      entity.Id = workflowScopeId;
      entity.LastModified = DateTime.UtcNow;
      entity.Language = string.Empty;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the workflow scope by its id.</summary>
    /// <param name="workflowScopeId">Id of the workflow scope to be retrieved.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.
    /// </returns>
    public override WorkflowScope GetWorkflowScope(Guid workflowScopeId)
    {
      WorkflowScope itemById = this.GetContext().GetItemById<WorkflowScope>(workflowScopeId.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the query of the workflow scopes.</summary>
    /// <returns>Query of workflow scopes.</returns>
    public override IQueryable<WorkflowScope> GetWorkflowScopes()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<WorkflowScope>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<WorkflowScope>((Expression<Func<WorkflowScope, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a workflow type scope.</summary>
    /// <param name="workflowScope">The instance of workflow scope to be deleted.</param>
    public override void Delete(WorkflowScope workflowScope) => this.GetContext().Remove((object) workflowScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type.
    /// </summary>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.
    /// </returns>
    public override WorkflowTypeScope CreateWorkflowTypeScope() => this.CreateWorkflowTypeScope(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.
    /// </returns>
    public override WorkflowTypeScope CreateWorkflowTypeScope(
      Guid workflowTypeScopeId)
    {
      WorkflowTypeScope entity = new WorkflowTypeScope();
      entity.ApplicationName = this.ApplicationName;
      entity.Id = workflowTypeScopeId;
      entity.LastModified = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the workflow type scope by its id.</summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope to be retrieved.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.
    /// </returns>
    public override WorkflowTypeScope GetWorkflowTypeScope(Guid workflowtypeScopeId)
    {
      WorkflowTypeScope itemById = this.GetContext().GetItemById<WorkflowTypeScope>(workflowtypeScopeId.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the query of the workflow type scopes.</summary>
    /// <returns>Query of workflow type scopes.</returns>
    public override IQueryable<WorkflowTypeScope> GetWorkflowTypeScopes()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<WorkflowTypeScope>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<WorkflowTypeScope>((Expression<Func<WorkflowTypeScope, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a workflow type scope.</summary>
    /// <param name="workflowTypeScope">The instance of workflow type scope to be deleted.</param>
    public override void Delete(WorkflowTypeScope workflowTypeScope) => this.GetContext().Remove((object) workflowTypeScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type.
    /// </summary>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.
    /// </returns>
    public override WorkflowLevel CreateWorkflowLevel() => this.CreateWorkflowLevel(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type with the specified id.
    /// </summary>
    /// <param name="workflowLevelId">Id of the workflow level.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.
    /// </returns>
    public override WorkflowLevel CreateWorkflowLevel(Guid workflowLevelId)
    {
      WorkflowLevel entity = new WorkflowLevel();
      entity.ApplicationName = this.ApplicationName;
      entity.Id = workflowLevelId;
      entity.LastModified = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the workflow level by its id.</summary>
    /// <param name="workflowLevelId">Id of the workflow level to be retrieved.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.
    /// </returns>
    public override WorkflowLevel GetWorkflowLevel(Guid workflowLevelId)
    {
      WorkflowLevel itemById = this.GetContext().GetItemById<WorkflowLevel>(workflowLevelId.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the query of the workflow levels.</summary>
    /// <returns>Query of workflow levels.</returns>
    public override IQueryable<WorkflowLevel> GetWorkflowLevels()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<WorkflowLevel>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<WorkflowLevel>((Expression<Func<WorkflowLevel, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a workflow type level.</summary>
    /// <param name="workflowLevel">The instance of workflow level to be deleted.</param>
    public override void Delete(WorkflowLevel workflowLevel) => this.GetContext().Remove((object) workflowLevel);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type.
    /// </summary>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.
    /// </returns>
    public override WorkflowPermission CreateWorkflowPermission() => this.CreateWorkflowPermission(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type with the specified id.
    /// </summary>
    /// <param name="workflowPermissionId">Id of the workflow permission.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.
    /// </returns>
    public override WorkflowPermission CreateWorkflowPermission(
      Guid workflowPermissionId)
    {
      WorkflowPermission entity = new WorkflowPermission();
      entity.ApplicationName = this.ApplicationName;
      entity.Id = workflowPermissionId;
      entity.LastModified = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the workflow permission by its id.</summary>
    /// <param name="workflowPermissionId">Id of the workflow permission to be retrieved.</param>
    /// <returns>
    /// A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.
    /// </returns>
    public override WorkflowPermission GetWorkflowPermission(
      Guid workflowPermissionId)
    {
      WorkflowPermission itemById = this.GetContext().GetItemById<WorkflowPermission>(workflowPermissionId.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Gets the query of the workflow permissions.</summary>
    /// <returns>Query of workflow permissions.</returns>
    public override IQueryable<WorkflowPermission> GetWorkflowPermissions()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<WorkflowPermission>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<WorkflowPermission>((Expression<Func<WorkflowPermission, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a workflow permission.</summary>
    /// <param name="workflowPermission">The instance of workflow permission to be deleted.</param>
    public override void Delete(WorkflowPermission workflowPermission) => this.GetContext().Remove((object) workflowPermission);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new WorfklowMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public override SiteItemLink CreateSiteItemLink() => MultisiteExtensions.CreateSiteItemLink(this);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public override void Delete(SiteItemLink link) => MultisiteExtensions.Delete(this, link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public override void DeleteLinksForItem(IDataItem item) => MultisiteExtensions.DeleteLinksForItem(this, item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<SiteItemLink> GetSiteItemLinks() => MultisiteExtensions.GetSiteItemLinks(this);

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public override SiteItemLink AddItemLink(Guid siteId, IDataItem item) => MultisiteExtensions.AddItemLink(this, siteId, item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<T> GetSiteItems<T>(Guid siteId) => MultisiteExtensions.GetSiteItems<T>(this, siteId);

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
    }

    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0)
        return;
      if (upgradedFromSchemaVersionNumber < SitefinityVersion.Sitefinity8_0.Build)
      {
        string upgradeScript = "DELETE FROM sf_workflow_scope\r\n                                WHERE content_type ='ALL_CONTENT'";
        if (context.DatabaseContext.DatabaseType == DatabaseType.Oracle)
          upgradeScript = "DELETE FROM \"sf_workflow_scope\"\r\n                                WHERE \"content_type\" = 'ALL_CONTENT'";
        OpenAccessConnection.Upgrade(context, "Delete workflow scopes for 'All content' of workflow definitions.", upgradeScript);
        CommonUpgrader commonUpgrader = new CommonUpgrader(this.GetMetaDataSource(context.DatabaseContext) as SitefinityMetadataSourceBase);
        string str = "Remove orphaned columns/tables of Title property of workflow definition, converted to non-localizable";
        try
        {
          SqlGenerator.Get(context.Connection.DbType);
          commonUpgrader.DeleteMultilingualField(context, "sf_workflow_definition", "title", true, true);
          context.SaveChanges();
          Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          context.ClearChanges();
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        }
      }
      if (upgradedFromSchemaVersionNumber >= 6800)
        return;
      new WorkflowNewModelUpgradeScript(this, context, upgradedFromSchemaVersionNumber).Upgrade_To_Sitefinity_11_1();
    }
  }
}
