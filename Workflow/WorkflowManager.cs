// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security.AntiXss;
using System.Xaml;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.ContentWorkflows;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent.AnyContent;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Configuration;
using Telerik.Sitefinity.Workflow.Data;
using Telerik.Sitefinity.Workflow.Model;
using Telerik.Sitefinity.Workflow.Model.DurableInstancing;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow
{
  public class WorkflowManager : 
    ManagerBase<WorkflowDataProvider>,
    IMultisiteEnabledManagerExtended,
    IMultisiteEnabledManager,
    IManager,
    IDisposable,
    IProviderResolver
  {
    private static readonly ConcurrentDictionary<string, object> runningWorkflowsArgs = new ConcurrentDictionary<string, object>();
    [Obsolete("AllContentKey is not used any more. When the definition is created with no workflow scope relation means that it is for all content types")]
    public const string AllContentKey = "ALL_CONTENT";
    public const string CONTEXT_BAG_SEGMENT_ID = "SegmentId";
    public const string CONTEXT_BAG_CHECK_RELATED_DATA = "CheckRelatingData";
    public const string CONTEXT_BAG_PUBLICATION_DATE = "PublicationDate";
    public const string SKIP_RECYCLE_BIN = "SkipRecycleBin";
    internal const string ValidateItemKey = "validateItem";
    private const string workflowCacheKey = "workflowServiceCache";
    private const string THREAD_CULTURE = "culture";
    private const string HTTP_CONTEXT = "httpContext";
    private const string THREAD_EXCEPTION = "exception";
    private const string CONTEXT_BAG = "contextBag";
    private const string BOLD_FORMAT = "<strong>{0}</strong>";
    private static bool isFormsAuthenticaiotn = Config.Get<SecurityConfig>().AuthenticationMode == AuthenticationMode.Forms;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowManager" /> class.
    /// </summary>
    public WorkflowManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public WorkflowManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public WorkflowManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    public static ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    /// <summary>Creates the instance.</summary>
    /// <returns></returns>
    public Instance CreateInstance() => this.Provider.CreateInstance();

    /// <summary>Creates the instance.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public Instance CreateInstance(Guid id) => this.Provider.CreateInstance(id);

    /// <summary>Gets the instance.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public Instance GetInstance(Guid id) => this.Provider.GetInstance(id);

    /// <summary>Gets the instances.</summary>
    /// <returns></returns>
    public IQueryable<Instance> GetInstances() => this.Provider.GetInstances();

    /// <summary>Deletes the specified instance.</summary>
    /// <param name="instance">The instance.</param>
    public void Delete(Instance instance)
    {
      LicenseLimitations.ValidateWorkflow();
      this.Provider.Delete(instance);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    public Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition()
    {
      LicenseLimitations.ValidateWorkflow();
      return this.Provider.CreateWorkflowDefinition();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type with the specified id.
    /// </summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    public Telerik.Sitefinity.Workflow.Model.WorkflowDefinition CreateWorkflowDefinition(
      Guid workflowDefinitionId)
    {
      LicenseLimitations.ValidateWorkflow();
      return this.Provider.CreateWorkflowDefinition(workflowDefinitionId);
    }

    /// <summary>Gets the workflow definition by its id.</summary>
    /// <param name="workflowDefinitionId">Id of the workflow definition to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> object.</returns>
    public Telerik.Sitefinity.Workflow.Model.WorkflowDefinition GetWorkflowDefinition(
      Guid workflowDefinitionId)
    {
      return this.Provider.GetWorkflowDefinition(workflowDefinitionId);
    }

    /// <summary>Gets the query of the workflow definitions.</summary>
    /// <returns>Query of workflow definitions.</returns>
    public IQueryable<Telerik.Sitefinity.Workflow.Model.WorkflowDefinition> GetWorkflowDefinitions() => this.Provider.GetWorkflowDefinitions();

    /// <summary>Deletes a workflow definition.</summary>
    /// <param name="workflowDefinition">The instance of workflow definition to be deleted.</param>
    public void Delete(Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition)
    {
      LicenseLimitations.ValidateWorkflow();
      this.Provider.Delete(workflowDefinition);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public WorkflowScope CreateWorkflowScope() => this.Provider.CreateWorkflowScope();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowScopeId">Id of the workflow scope.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public WorkflowScope CreateWorkflowScope(Guid workflowScopeId) => this.Provider.CreateWorkflowScope(workflowScopeId);

    /// <summary>Gets the workflow scope by its id.</summary>
    /// <param name="workflowScopeId">Id of the workflow scope to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowScope" /> object.</returns>
    public WorkflowScope GetWorkflowScope(Guid workflowScopeId) => this.Provider.GetWorkflowScope(workflowScopeId);

    /// <summary>Gets the query of the workflow scopes.</summary>
    /// <returns>Query of workflow scopes.</returns>
    public IQueryable<WorkflowScope> GetWorkflowScopes() => this.Provider.GetWorkflowScopes();

    /// <summary>Deletes a workflow scope.</summary>
    /// <param name="workflowScope">The instance of workflow type scope to be deleted.</param>
    public void Delete(WorkflowScope workflowScope) => this.Provider.Delete(workflowScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public WorkflowTypeScope CreateWorkflowTypeScope() => this.Provider.CreateWorkflowTypeScope();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> type with the specified id.
    /// </summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public WorkflowTypeScope CreateWorkflowTypeScope(Guid workflowTypeScopeId) => this.Provider.CreateWorkflowTypeScope(workflowTypeScopeId);

    /// <summary>Gets the workflow type scope by its id.</summary>
    /// <param name="workflowTypeScopeId">Id of the workflow type scope to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowTypeScope" /> object.</returns>
    public WorkflowTypeScope GetWorkflowTypeScope(Guid workflowTypeScopeId) => this.Provider.GetWorkflowTypeScope(workflowTypeScopeId);

    /// <summary>Gets the query of the workflow type scopes.</summary>
    /// <returns>Query of workflow type scopes.</returns>
    public IQueryable<WorkflowTypeScope> GetWorkflowTypeScopes() => this.Provider.GetWorkflowTypeScopes();

    /// <summary>Deletes a workflow type scope.</summary>
    /// <param name="workflowTypeScope">The instance of workflow type scope to be deleted.</param>
    public void Delete(WorkflowTypeScope workflowTypeScope) => this.Provider.Delete(workflowTypeScope);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public WorkflowLevel CreateWorkflowLevel() => this.Provider.CreateWorkflowLevel();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> type with the specified id.
    /// </summary>
    /// <param name="workflowLevelId">Id of the workflow level.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public WorkflowLevel CreateWorkflowLevel(Guid workflowLevelId) => this.Provider.CreateWorkflowLevel(workflowLevelId);

    /// <summary>Gets the workflow level by its id.</summary>
    /// <param name="workflowLevelId">Id of the workflow level to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> object.</returns>
    public WorkflowLevel GetWorkflowLevel(Guid workflowLevelId) => this.Provider.GetWorkflowLevel(workflowLevelId);

    /// <summary>Gets the query of the workflow levels.</summary>
    /// <returns>Query of workflow levels.</returns>
    public IQueryable<WorkflowLevel> GetWorkflowLevels() => this.Provider.GetWorkflowLevels();

    /// <summary>Deletes a workflow level.</summary>
    /// <param name="workflowLevel">The instance of workflow type level to be deleted.</param>
    public void Delete(WorkflowLevel workflowLevel) => this.Provider.Delete(workflowLevel);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type.
    /// </summary>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public WorkflowPermission CreateWorkflowPermission() => this.Provider.CreateWorkflowPermission();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type with the specified id.
    /// </summary>
    /// <param name="workflowPermissionId">Id of the workflow permission.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public WorkflowPermission CreateWorkflowPermission(Guid workflowPermissionId) => this.Provider.CreateWorkflowPermission(workflowPermissionId);

    /// <summary>Gets the workflow permission by its id.</summary>
    /// <param name="workflowPermissionId">Id of the workflow permission to be retrieved.</param>
    /// <returns>A newly created <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> object.</returns>
    public WorkflowPermission GetWorkflowPermission(Guid workflowPermissionId) => this.Provider.GetWorkflowPermission(workflowPermissionId);

    /// <summary>Gets the query of the workflow permissions.</summary>
    /// <returns>Query of workflow permissions.</returns>
    public IQueryable<WorkflowPermission> GetWorkflowPermissions() => this.Provider.GetWorkflowPermissions();

    /// <summary>Deletes a workflow permission.</summary>
    /// <param name="workflowPermission">The instance of workflow permission to be deleted.</param>
    public void Delete(WorkflowPermission workflowPermission) => this.Provider.Delete(workflowPermission);

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<WorkflowConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public override string ModuleName => "Workflow";

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<WorkflowConfig>().Providers;

    /// <summary>
    /// Get an instance of the workflow manager using the default provider
    /// </summary>
    /// <returns>Instance of workflow manager</returns>
    public static WorkflowManager GetManager() => ManagerBase<WorkflowDataProvider>.GetManager<WorkflowManager>();

    /// <summary>
    /// Get an instance of the workflow manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the workflow manager</returns>
    public static WorkflowManager GetManager(string providerName) => ManagerBase<WorkflowDataProvider>.GetManager<WorkflowManager>(providerName);

    /// <summary>
    /// Get an instance of the workflow manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the workflow manager</returns>
    public static WorkflowManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<WorkflowDataProvider>.GetManager<WorkflowManager>(providerName, transactionName);
    }

    /// <summary>Gets the workflow service URL from type.</summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    [Obsolete("Use GetWorkflowServiceUrl(Type type, string providerName, Guid itemId, IWorkflowExecutionDefinition wed = null)")]
    public static string GetWorkflowServiceUrl(System.Type type) => WorkflowManager.GetWorkflowServiceUrl(type, (IWorkflowExecutionDefinition) null);

    /// <summary>Gets the workflow service URL from workflow item.</summary>
    /// <param name="type">The item's type.</param>
    /// <param name="wed">Workflow execution definition that will be used.</param>
    /// <returns></returns>
    public static string GetWorkflowServiceUrl(System.Type type, IWorkflowExecutionDefinition wed)
    {
      if (LicenseState.Current.LicenseInfo.WorkflowFeaturesLevel != 100)
        return WorkflowConfig.GetDefaultWorkflowUrl(type);
      if (wed != null && !string.IsNullOrEmpty(wed.CustomXamlxUrl))
        return wed.CustomXamlxUrl;
      WorkflowConfig workflowConfig = Config.Get<WorkflowConfig>();
      WorkflowElement workflowElement;
      if (workflowConfig.Workflows.TryGetValue(type.FullName, out workflowElement) || type.BaseType == typeof (DynamicContent) && workflowConfig.Workflows.TryGetValue(typeof (DynamicContent).FullName, out workflowElement))
        return workflowElement.ServiceUrl;
      throw new System.InvalidOperationException(string.Format("No approval workflow mapping found for type: {0}", (object) type.FullName));
    }

    /// <summary>Runs the new or existing workflow.</summary>
    /// <param name="workflowItemId">Id of the workflow item.</param>
    /// <returns>An id of the workflow item being run.</returns>
    public static Guid RunWorkflow(Guid workflowItemId, string workflowServiceVirtualPath) => Guid.NewGuid();

    /// <summary>
    /// Gets the currently available decisions for the Content Approval workflow and given
    /// workflow item id.
    /// </summary>
    /// <param name="workflowItemId">Id of the item being in the workflow.</param>
    /// <param name="itemCulture">Culture used to resolve the item status</param>
    /// <returns>A collection of <see cref="!:VisualDecision" /> objects.</returns>
    [Obsolete("Use GetCurrentDecisions(Type itemType, string providerName, Guid itemId, CultureInfo itemStatusCulture = null)")]
    public static IDictionary<string, DecisionActivity> GetCurrentDecisions(
      string itemId,
      System.Type itemType,
      string providerName,
      CultureInfo itemStatusCulture = null)
    {
      Guid itemId1 = Guid.Empty;
      if (!string.IsNullOrEmpty(itemId))
        itemId1 = new Guid(itemId);
      return WorkflowManager.GetCurrentDecisions(itemType, providerName, itemId1, itemStatusCulture);
    }

    /// <summary>
    /// Gets the currently available decisions for the Content Approval workflow and given
    /// workflow item id.
    /// </summary>
    /// <param name="itemType">Type of the item for which the workflow is executed.</param>
    /// <param name="providerName">Name of item's provider.</param>
    /// <param name="itemId">Id of the content item.</param>
    /// <param name="itemStatusCulture">Culture used to resolve the item status</param>
    /// <returns>A collection of <see cref="!:VisualDecision" /> objects.</returns>
    public static IDictionary<string, DecisionActivity> GetCurrentDecisions(
      System.Type itemType,
      string providerName,
      Guid itemId,
      CultureInfo itemStatusCulture = null)
    {
      return WorkflowManager.GetCurrentDecisions(itemType, providerName, itemId, out IWorkflowExecutionDefinition _, itemStatusCulture);
    }

    public static IDictionary<string, DecisionActivity> GetCurrentDecisions(
      System.Type itemType,
      string providerName,
      Guid itemId,
      out IWorkflowExecutionDefinition wed,
      CultureInfo itemStatusCulture = null)
    {
      WorkflowStatusContext itemStatus = new WorkflowStatusContext();
      itemStatus.WorkflowStatus = "Created";
      wed = WorkflowManager.GetWorkflowExecutionDefinition(itemType, providerName, itemId, itemStatusCulture);
      FlowchartWorkflowInspector flowchartInspector = WorkflowManager.GetContentServiceFlowchartInspector(WorkflowManager.GetWorkflowServiceUrl(itemType, wed));
      if (itemId != Guid.Empty)
      {
        try
        {
          itemStatus = WorkflowManager.ResolveWorkflowStatus(itemType, providerName, itemId, itemStatusCulture);
        }
        catch (UnauthorizedAccessException ex)
        {
          return (IDictionary<string, DecisionActivity>) new Dictionary<string, DecisionActivity>();
        }
      }
      return flowchartInspector.GetVisualDecisions(itemStatus, wed);
    }

    internal static IDictionary<string, DecisionActivity> GetCurrentDecisions(
      IWorkflowItem item,
      IManager manager,
      CultureInfo itemStatusCulture = null)
    {
      new WorkflowStatusContext().WorkflowStatus = "Created";
      IWorkflowExecutionDefinition executionDefinition = WorkflowManager.GetWorkflowExecutionDefinition(item, itemStatusCulture);
      return WorkflowManager.GetContentServiceFlowchartInspector(WorkflowManager.GetWorkflowServiceUrl(item.GetActualType(), executionDefinition)).GetVisualDecisions(WorkflowManager.ResolveWorkflowStatus(item, manager, itemStatusCulture), executionDefinition);
    }

    /// <summary>Gets the workflow states.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="workflowDefinition">The workflow definition.</param>
    /// <returns></returns>
    [Obsolete("Use GetWorkflowStates(Type itemType, string providerName, Guid itemId, CultureInfo cultureInfo, IWorkflowExecutionDefinition wed = null)")]
    public static IEnumerable<string> GetWorkflowStates(
      System.Type itemType,
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition = null)
    {
      IWorkflowExecutionDefinition wed = (IWorkflowExecutionDefinition) null;
      if (workflowDefinition != null)
        wed = (IWorkflowExecutionDefinition) new WorkflowDefinitionProxy(workflowDefinition);
      return WorkflowManager.GetWorkflowStates(itemType, (string) null, Guid.Empty, (CultureInfo) null, wed);
    }

    /// <summary>Gets the workflow states.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="wed">The workflow execution definition.</param>
    /// <returns></returns>
    public static IEnumerable<string> GetWorkflowStates(
      System.Type itemType,
      string providerName,
      Guid itemId,
      CultureInfo cultureInfo,
      IWorkflowExecutionDefinition wed = null)
    {
      if (wed == null)
        wed = WorkflowManager.GetWorkflowExecutionDefinition(itemType, providerName, itemId, cultureInfo);
      return WorkflowManager.GetContentServiceFlowchartInspector(WorkflowManager.GetWorkflowServiceUrl(itemType, wed)).GetWorkflowStates(wed);
    }

    /// <summary>Sends a message to workflow.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="operationName">Name of the operation.</param>
    /// <param name="contextBag">The context bag.</param>
    /// <returns></returns>
    public static string MessageWorkflow(
      Guid itemId,
      System.Type itemType,
      string providerName,
      string operationName,
      bool isCheckedOut,
      Dictionary<string, string> contextBag)
    {
      int num = 0;
      string str = (string) null;
      while (num <= 3)
      {
        try
        {
          Dictionary<string, string> contextBag1 = new Dictionary<string, string>((IDictionary<string, string>) contextBag);
          str = WorkflowManager.MessageWorkflowInternal(itemId, itemType, providerName, operationName, isCheckedOut, contextBag1);
          break;
        }
        catch (Exception ex)
        {
          if (ex is OptimisticVerificationException)
            ++num;
          else
            throw;
        }
      }
      return str;
    }

    private static string MessageWorkflowInternal(
      Guid itemId,
      System.Type itemType,
      string providerName,
      string operationName,
      bool isCheckedOut,
      Dictionary<string, string> contextBag)
    {
      using (new MethodPerformanceRegion("Message Workflow"))
      {
        string str;
        contextBag.TryGetValue("validateItem", out str);
        if (str != null && str.ToLower() == "true")
          WorkflowManager.ValidateItemChange(itemId, itemType, operationName, providerName);
        if (providerName.IsNullOrEmpty())
        {
          IManager mappedManager = ManagerBase.GetMappedManager(itemType);
          if (mappedManager != null)
            providerName = mappedManager.Provider.Name;
        }
        IWorkflowExecutionDefinition executionDefinition = WorkflowManager.GetWorkflowExecutionDefinition(itemType, providerName, itemId);
        string workflowServiceUrl = WorkflowManager.GetWorkflowServiceUrl(itemType, executionDefinition);
        if (!contextBag.Keys.Contains<string>("ContentType"))
          contextBag.Add("ContentType", itemType.FullName);
        if (Config.Get<WorkflowConfig>().RunWorkflowAsService)
          return WorkflowManager.MessageWorkflow(workflowServiceUrl, operationName, executionDefinition, providerName, itemId, isCheckedOut, contextBag);
        Activity workflowDefinition = WorkflowManager.LoadContentApprovalWorkflow(workflowServiceUrl);
        ManualResetEvent evt = new ManualResetEvent(false);
        bool requiresTransaction = !string.IsNullOrEmpty(SystemManager.CurrentContext.GlobalTransaction);
        Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
        dictionary1.Add(nameof (operationName), (object) operationName);
        dictionary1.Add(nameof (itemId), (object) itemId);
        dictionary1.Add(nameof (providerName), (object) providerName);
        dictionary1.Add(nameof (isCheckedOut), (object) isCheckedOut);
        dictionary1.Add(nameof (contextBag), (object) contextBag);
        dictionary1.Add("culture", (object) CultureInfo.CurrentCulture.Name);
        dictionary1.Add("httpContext", (object) HttpContext.Current);
        if (executionDefinition != null)
          dictionary1.Add("workflowDefinitionId", (object) executionDefinition.Id);
        contextBag.Add("userHostAddress", HttpContext.Current.Request.UserHostAddress);
        WorkflowApplication workflowApplication = new WorkflowApplication(workflowDefinition)
        {
          Completed = (Action<WorkflowApplicationCompletedEventArgs>) (eventArgs => WorkflowManager.DisposeWorkflowThread(evt, requiresTransaction)),
          Aborted = (Action<WorkflowApplicationAbortedEventArgs>) (eventArgs => WorkflowManager.DisposeWorkflowThread(evt, requiresTransaction)),
          OnUnhandledException = new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>(WorkflowManager.OnWorkflowUnhandledException)
        };
        string key = workflowApplication.Id.ToString();
        WorkflowManager.runningWorkflowsArgs[key] = (object) dictionary1;
        ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
        try
        {
          if (!requiresTransaction)
            SystemManager.CurrentTransactions = (ContextTransactions) null;
          using (new MethodPerformanceRegion("Run workflow activity"))
          {
            using (new CultureRegion(SystemManager.CurrentContext.Culture))
            {
              workflowApplication.Run();
              evt.WaitOne();
            }
          }
        }
        finally
        {
          SystemManager.CurrentTransactions = currentTransactions;
        }
        object obj1;
        WorkflowManager.runningWorkflowsArgs.TryRemove(key, out obj1);
        Dictionary<string, object> dictionary2 = obj1 as Dictionary<string, object>;
        object obj2;
        dictionary2.TryGetValue("exception", out obj2);
        dictionary2.Clear();
        if (obj2 != null)
          throw obj2 as Exception;
        return (string) null;
      }
    }

    private static void ValidateItemChange(
      Guid itemId,
      System.Type itemType,
      string operationName,
      string providerName)
    {
      if (!WorkflowManager.ShouldValidateItemChange(operationName) || !(itemType == typeof (PageNode)))
        return;
      PageData pageData = PageManager.GetManager(providerName).GetPageNode(itemId).GetPageData();
      if (!PageManager.IsPageDataStillLocked(pageData))
        throw new InvalidWorkflowOperationException(operationName, "This page has been unlocked by another user and your changes cannot be saved.");
      if (PageManager.IsPageDataOwnerChanged(pageData))
        throw new InvalidWorkflowOperationException(operationName, "The owner of this page has changed and your changes cannot be saved.");
    }

    private static bool ShouldValidateItemChange(string operationName) => ((IEnumerable<string>) new string[4]
    {
      "Publish",
      "SaveDraft",
      "Schedule",
      "StopSchedule"
    }).Contains<string>(operationName) || operationName.StartsWith("SendFor") || operationName.StartsWith("SaveAs");

    /// <summary>
    /// Handles workflow exceptions when workflow is run InProcess
    ///  </summary>
    /// <param name="args">The <see cref="T:System.Activities.WorkflowApplicationUnhandledExceptionEventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    private static UnhandledExceptionAction OnWorkflowUnhandledException(
      WorkflowApplicationUnhandledExceptionEventArgs args)
    {
      (WorkflowManager.runningWorkflowsArgs[args.InstanceId.ToString()] as IDictionary<string, object>)["exception"] = (object) args.UnhandledException;
      return UnhandledExceptionAction.Abort;
    }

    private static void DisposeWorkflowThread(ManualResetEvent evt, bool requiresTransaction)
    {
      if (!requiresTransaction)
      {
        SystemManager.ClearCurrentTransactions();
        HttpContext.Current = (HttpContext) null;
      }
      evt.Set();
    }

    /// <summary>Sends a message to workflow.</summary>
    /// <param name="workflowInstanceId">Id of the workflow instance to which the message ought to be sent.</param>
    /// <param name="operationName">Name of the operation that ought to be messages to the workflow instance.</param>
    /// <param name="itemStatus">Current status of the workflow item.</param>
    /// <param name="workflowDefinition">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> that defines the workflow instance.
    /// </param>
    /// <returns>The new status of the workflow that resulted from the message being sent to the workflow.</returns>
    [Obsolete("Won't be supported any more.")]
    public static string MessageWorkflow(
      string wokflowUrl,
      string operationName,
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition,
      Guid contentId,
      string providerName,
      bool isCheckedOut,
      Dictionary<string, string> contextBag)
    {
      return WorkflowManager.MessageWorkflow(wokflowUrl, operationName, (IWorkflowExecutionDefinition) new WorkflowDefinitionProxy(workflowDefinition), providerName, contentId, isCheckedOut, contextBag);
    }

    /// <summary>Sends a message to workflow.</summary>
    /// <param name="workflowInstanceId">Id of the workflow instance to which the message ought to be sent.</param>
    /// <param name="operationName">Name of the operation that ought to be messages to the workflow instance.</param>
    /// <param name="itemStatus">Current status of the workflow item.</param>
    /// <param name="wed">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> that defines how the workflow will be executed.
    /// </param>
    /// <returns>The new status of the workflow that resulted from the message being sent to the workflow.</returns>
    [Obsolete("Won't be supported any more.")]
    public static string MessageWorkflow(
      string workflowUrl,
      string operationName,
      IWorkflowExecutionDefinition wed,
      string providerName,
      Guid contentId,
      bool isCheckedOut,
      Dictionary<string, string> contextBag)
    {
      string str1 = (string) null;
      string str2 = (string) null;
      BindingTimeoutsElememet timeoutsElememet = (BindingTimeoutsElememet) null;
      if (AzureRuntime.IsRunning)
      {
        bool flag = Config.Get<WorkflowConfig>().UseExternalEndpointOnWindowsAzure;
        if (!flag)
        {
          Uri internalEndpointUri = AzureRuntime.CurrentRoleInternalEndpointUri;
          if (internalEndpointUri == (Uri) null)
          {
            flag = true;
          }
          else
          {
            str1 = internalEndpointUri.GetLeftPart(UriPartial.Authority);
            str2 = SystemManager.Host;
          }
        }
        if (flag)
          timeoutsElememet = Config.Get<WorkflowConfig>().WindowsAzureExternalEndpointBindingTimeouts;
      }
      if (str1 == null)
      {
        string workflowBaseUrl = Config.Get<SystemConfig>().ServicesPaths.WorkflowBaseUrl;
        str1 = string.IsNullOrEmpty(workflowBaseUrl) ? WorkflowManager.GetHost() : workflowBaseUrl;
      }
      string uri = str1 + VirtualPathUtility.ToAbsolute(workflowUrl);
      UriBuilder uriBuilder = new UriBuilder(uri);
      NameValueCollection queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
      if (!queryString.HasKeys() || !queryString.Keys.Contains("sf_site"))
      {
        queryString.Add("sf_site", SystemManager.CurrentContext.CurrentSite.Id.ToString());
        uriBuilder.Query = queryString.ToString();
        uri = uriBuilder.ToString();
      }
      EndpointAddress remoteAddress = new EndpointAddress(uri);
      BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
      basicHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      basicHttpBinding.MaxBufferSize = int.MaxValue;
      BasicHttpBinding binding = basicHttpBinding;
      binding.AllowCookies = false;
      if (Debugger.IsAttached)
      {
        TimeSpan maxValue = TimeSpan.MaxValue;
        binding.CloseTimeout = maxValue;
        binding.OpenTimeout = maxValue;
        binding.ReceiveTimeout = maxValue;
        binding.SendTimeout = maxValue;
      }
      else
        WorkflowManager.ConfigureBindingTimeouts(binding, timeoutsElememet ?? Config.Get<WorkflowConfig>().BindingTimeouts);
      ServiceClient serviceClient = new ServiceClient((Binding) binding, remoteAddress);
      using (new OperationContextScope((IContextChannel) serviceClient.InnerChannel))
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        NameValueCollection headers = currentHttpContext.Request.Headers;
        HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();
        if (WorkflowManager.isFormsAuthenticaiotn)
          requestMessageProperty.Headers[HttpRequestHeader.Cookie] = WorkflowManager.GetCookieHeader(currentHttpContext.Response.Cookies);
        else if (SitefinityClaimsAuthenticationModule.IsBasicAuthentication)
        {
          string header = HttpContext.Current.Request.Headers["Authorization"];
          requestMessageProperty.Headers.Add("Authorization", header);
          ClaimsManager.Logout();
        }
        else
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity != null && currentIdentity.UserId.ToString().ToUpperInvariant() == SecurityManager.SystemAccountIDs[0])
          {
            string str3 = SecurityManager.EncryptData(SecurityManager.SystemAccountIDs[0]);
            requestMessageProperty.Headers.Add("SF-Sys-Message", str3);
          }
          else
          {
            bool flag = false;
            string name1 = System.Enum.GetName(typeof (HttpRequestHeader), (object) HttpRequestHeader.Cookie);
            foreach (string key in headers.Keys)
            {
              if (key == name1)
              {
                requestMessageProperty.Headers.Add(name1, headers[name1]);
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              if (currentHttpContext.Request.Cookies.Count > 0)
              {
                requestMessageProperty.Headers[HttpRequestHeader.Cookie] = WorkflowManager.GetCookieHeader(currentHttpContext.Request.Cookies);
              }
              else
              {
                string name2 = System.Enum.GetName(typeof (HttpRequestHeader), (object) HttpRequestHeader.Authorization);
                if (((IEnumerable<string>) currentHttpContext.Request.Headers.AllKeys).Contains<string>(name2))
                  requestMessageProperty.Headers.Add(name2, currentHttpContext.Request.Headers[name2]);
              }
            }
          }
        }
        requestMessageProperty.Headers[HttpRequestHeader.Referer] = WorkflowManager.GetHost();
        if (str2 != null)
          requestMessageProperty.Headers[HttpRequestHeader.Host] = str2;
        foreach (string key in headers.Keys)
        {
          if (key.StartsWith("SF_", StringComparison.OrdinalIgnoreCase))
            requestMessageProperty.Headers.Add(key, headers[key]);
        }
        contextBag.Add("userHostAddress", HttpContext.Current.Request.UserHostAddress);
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = (object) requestMessageProperty;
        Guid workflowDefinitionId = wed == null ? Guid.Empty : wed.Id;
        return serviceClient.SendMessage(operationName, isCheckedOut, workflowDefinitionId, contentId, providerName, contextBag);
      }
    }

    private static string GetCookieHeader(HttpCookieCollection cookieCollection)
    {
      string cookieHeader = string.Empty;
      int num = ((IEnumerable<string>) cookieCollection.AllKeys).Count<string>();
      for (int index = 0; index < num; ++index)
      {
        cookieHeader = cookieHeader + cookieCollection[index].Name + "=" + cookieCollection[index].Value;
        if (index != num - 1)
          cookieHeader += "; ";
      }
      return cookieHeader;
    }

    /// <summary>
    /// Gets the appropriate workflow definition based on the workflow item type.
    /// </summary>
    /// <param name="workflowItem">
    /// An instance of <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> for which the workflow definition
    /// ought to be obtained.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" />.</returns>
    [Obsolete("Use one of the overloads of GetWorkflowExecutionDefinition.")]
    public static Telerik.Sitefinity.Workflow.Model.WorkflowDefinition GetWorkflowDefinition(
      System.Type itemType)
    {
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition = WorkflowDefinitionExtensions.FromIWorkflowExecutionDefinition(WorkflowManager.GetWorkflowExecutionDefinition(itemType, (string) null, Guid.Empty));
      return workflowDefinition.WorkflowType == WorkflowType.Default ? (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition) null : workflowDefinition;
    }

    /// <summary>Gets a workflow execution definition by its id.</summary>
    /// <param name="id">Workflow's id.</param>
    /// <returns>Workflow's execution definition</returns>
    public static IWorkflowExecutionDefinition GetWorkflowExecutionDefinition(
      Guid id)
    {
      return ObjectFactory.Resolve<IWorkflowDefinitionResolver>().GetWorkflowExecutionDefinition(id) ?? (IWorkflowExecutionDefinition) WorkflowDefinitionProxy.DefaultWorkflow;
    }

    /// <summary>
    /// Gets the appropriate workflow definition based on the workflow item.
    /// </summary>
    /// <param name="workflowItem">
    /// An instance of <see cref="T:Telerik.Sitefinity.IWorkflowItem" /> for which the workflow definition
    /// ought to be obtained.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" />.</returns>
    public static IWorkflowExecutionDefinition GetWorkflowExecutionDefinition(
      IWorkflowItem item,
      CultureInfo culture = null)
    {
      System.Type contentType = item.GetType();
      if (item is IWorkflowChildItem workflowChildItem)
        contentType = workflowChildItem.WorkflowType;
      return WorkflowManager.GetWorkflowExecutionDefinition(contentType, item.GetWorkflowItemProviderName(), item.Id, culture);
    }

    /// <summary>
    /// Gets the appropriate workflow definition based on the workflow item's properties.
    /// </summary>
    /// <param name="contentType">Item's type</param>
    /// <param name="contentProviderName">Item's provider name</param>
    /// <param name="contentId">Item's Id</param>
    /// <param name="culture">Item's culture</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" />.</returns>
    public static IWorkflowExecutionDefinition GetWorkflowExecutionDefinition(
      System.Type contentType,
      string contentProviderName,
      Guid contentId,
      CultureInfo culture = null)
    {
      WorkflowResolutionContext context = new WorkflowResolutionContext();
      context.ContentType = contentType;
      context.ContentId = contentId;
      context.Site = SystemManager.CurrentContext.CurrentSite;
      context.ContentProviderName = contentProviderName;
      if (context.ContentProviderName.IsNullOrEmpty())
      {
        IManager manager = (IManager) null;
        System.Type managerType;
        if (ManagerBase.TryGetMappedManagerType(contentType, out managerType))
          manager = ManagerBase.GetManager(managerType);
        if (manager != null)
          context.ContentProviderName = manager.Provider.Name;
      }
      context.Culture = culture == null ? SystemManager.CurrentContext.Culture : culture;
      return ObjectFactory.Resolve<IWorkflowDefinitionResolver>().ResolveWorkflowExecutionDefinition((IWorkflowResolutionContext) context) ?? (IWorkflowExecutionDefinition) WorkflowDefinitionProxy.DefaultWorkflow;
    }

    /// <summary>
    /// Copies the instance of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> to the instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" />.
    /// This method is used when working with services that require ViewModel implementations of WorkflowDefinition.
    /// </summary>
    /// <param name="source">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> that is used as a source for copying.
    /// </param>
    /// <param name="target">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> that is used as a target of copying.
    /// </param>
    public static void CopyToWorkflowDefinition(
      WorkflowDefinitionViewModel source,
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition target,
      WorkflowManager workflowManager)
    {
      target.AllowAdministratorsToSkipWorkflow = source.AllowAdministratorsToSkipWorkflow;
      target.AllowPublishersToSkipWorkflow = source.AllowPublishersToSkipWorkflow;
      target.IsActive = source.IsActive;
      target.AllowNotes = source.AllowNotes;
      target.Title = source.Title;
      target.WorkflowType = source.WorkflowType;
      if (source.DateCreated != DateTime.MinValue)
        target.DateCreated = source.DateCreated;
      if (source.Scopes != null)
        WorkflowManager.CopyToWorkflowScopes(source.Scopes, target.Scopes, workflowManager);
      if (source.Levels == null)
        return;
      WorkflowManager.CopyToWorkflowLevels(source.Levels, target.Levels, workflowManager);
    }

    /// <summary>
    /// Copies the instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> to the instance of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" />.
    /// This method is used when working with services that required ViewModel implementations of WorkflowDefinition.
    /// </summary>
    /// <param name="source">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> that is used as a source for copying.
    /// </param>
    /// <param name="target">
    /// An instance of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> that is used as a target of copying.
    /// </param>
    public static void CopyToWorkflowDefinitionViewModel(
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition source,
      WorkflowDefinitionViewModel target)
    {
      WorkflowManager.CopyToWorkflowDefinitionViewModel(new WorkflowDefinitionProxy(source), target);
    }

    internal static void CopyToWorkflowDefinitionViewModel(
      WorkflowDefinitionProxy source,
      WorkflowDefinitionViewModel target)
    {
      target.Id = source.Id.ToString();
      target.AllowAdministratorsToSkipWorkflow = source.AllowAdministratorsToSkipWorkflow;
      target.AllowPublishersToSkipWorkflow = source.AllowPublishersToSkipWorkflow;
      target.IsActive = source.IsActive;
      target.AllowNotes = source.AllowNotes;
      target.Title = source.Title;
      target.WorkflowType = source.WorkflowType;
      target.DateCreated = !(source.DateCreated != DateTime.MinValue) ? DateTime.UtcNow : source.DateCreated;
      target.OwnerUserName = SecurityManager.GetPrincipalName(source.Owner);
      target.UIStatus = source.IsActive ? "Active" : "Inactive";
      WorkflowManager.SetWorkflowType(source, target);
      if (source.Scopes != null)
      {
        target.Scopes = (IList<WorkflowScopeViewModel>) new List<WorkflowScopeViewModel>();
        foreach (IWorkflowExecutionScope scope1 in source.Scopes)
        {
          IWorkflowExecutionScope scope = scope1;
          Telerik.Sitefinity.Multisite.ISite site = (Telerik.Sitefinity.Multisite.ISite) null;
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null)
            site = multisiteContext.GetSites().Where<Telerik.Sitefinity.Multisite.ISite>((Func<Telerik.Sitefinity.Multisite.ISite, bool>) (s => s.Id == scope.SiteId)).FirstOrDefault<Telerik.Sitefinity.Multisite.ISite>();
          if (WorkflowManager.ValidateScopeState(scope, site))
          {
            WorkflowScopeViewModel workflowScopeViewModel = new WorkflowScopeViewModel();
            workflowScopeViewModel.TypeScopes = (IList<WorkflowTypeScopeViewModel>) new List<WorkflowTypeScopeViewModel>();
            workflowScopeViewModel.Id = scope.Id;
            if (site != null)
              WorkflowManager.SetSiteScope(workflowScopeViewModel, site);
            WorkflowManager.SetCultureScope(scope, workflowScopeViewModel);
            foreach (WorkflowTypeScopeProxy typeScope in (IEnumerable<WorkflowTypeScopeProxy>) scope.TypeScopes.OrderBy<WorkflowTypeScopeProxy, string>((Func<WorkflowTypeScopeProxy, string>) (ts => ts.ContentType)))
            {
              if (!typeScope.ContentType.IsNullOrEmpty() && WorkflowManager.ValidateTypeScopeState((IWorkflowExecutionTypeScope) typeScope))
              {
                WorkflowTypeScopeViewModel typeScopeViewModel = new WorkflowTypeScopeViewModel()
                {
                  ContentType = typeScope.ContentType,
                  ContentFilter = typeScope.GetContentFilter(),
                  IncludeChildren = typeScope.IncludeChildren,
                  Title = WorkflowManager.GetScopeUITitle(typeScope.ContentType)
                };
                workflowScopeViewModel.TypeScopes.Add(typeScopeViewModel);
              }
            }
            target.Scopes.Add(workflowScopeViewModel);
          }
        }
        if (source.Levels != null)
        {
          target.Levels = (IList<WorkflowLevelViewModel>) new List<WorkflowLevelViewModel>();
          foreach (IWorkflowExecutionLevel level in source.Levels)
          {
            WorkflowLevelViewModel workflowLevelViewModel = new WorkflowLevelViewModel();
            workflowLevelViewModel.Id = level.Id;
            workflowLevelViewModel.ActionName = level.ActionName;
            workflowLevelViewModel.Ordinal = level.Ordinal;
            workflowLevelViewModel.NotifyApprovers = level.NotifyApprovers;
            workflowLevelViewModel.NotifyAdministrators = level.NotifyAdministrators;
            workflowLevelViewModel.CustomEmailRecipients = (IList<string>) level.CustomEmailRecipients.ToList<string>();
            if (level.Permissions != null)
            {
              workflowLevelViewModel.Permissions = (IList<WorkflowPermissionViewModel>) new List<WorkflowPermissionViewModel>();
              foreach (IWorkflowExecutionPermission permission in level.Permissions)
                workflowLevelViewModel.Permissions.Add(new WorkflowPermissionViewModel()
                {
                  ActionName = permission.ActionName,
                  PrincipalType = permission.PrincipalType,
                  PrincipalId = permission.PrincipalId,
                  PrincipalName = permission.PrincipalName
                });
            }
            target.Levels.Add(workflowLevelViewModel);
          }
        }
      }
      WorkflowManager.SetScopeItemsUI(target);
    }

    internal static List<WorkflowElement> GetWorkflowContentTypes(
      bool globalContext = true)
    {
      List<WorkflowElement> list = Config.Get<WorkflowConfig>().Workflows.Values.ToList<WorkflowElement>();
      List<WorkflowElement> workflowContentTypes = new List<WorkflowElement>();
      foreach (WorkflowElement workflowElement in list)
      {
        if (workflowElement != null)
        {
          if (!globalContext || string.IsNullOrEmpty(workflowElement.ModuleName) ? SystemManager.ValidateModuleItem((IModuleDependentItem) workflowElement) : SystemManager.IsModuleEnabled(workflowElement.ModuleName))
            workflowContentTypes.Add(workflowElement);
        }
      }
      return workflowContentTypes;
    }

    private static bool ValidateScopeState(IWorkflowExecutionScope scope, Telerik.Sitefinity.Multisite.ISite site)
    {
      List<string> list1 = scope.Cultures.ToList<string>();
      List<string> stringList = new List<string>();
      List<string> second = site == null ? ((IEnumerable<CultureInfo>) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToList<string>() : ((IEnumerable<CultureInfo>) site.PublicContentCultures).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToList<string>();
      if (scope.Cultures.Count<string>() > 0 && !list1.Intersect<string>((IEnumerable<string>) second).Any<string>())
        return false;
      int num = scope.TypeScopes.Where<WorkflowTypeScopeProxy>((Func<WorkflowTypeScopeProxy, bool>) (ts => ts.ContentType.IsNullOrEmpty())).Count<WorkflowTypeScopeProxy>();
      List<string> list2 = scope.TypeScopes.Select<WorkflowTypeScopeProxy, string>((Func<WorkflowTypeScopeProxy, string>) (ts => ts.ContentType)).ToList<string>();
      List<string> list3 = WorkflowManager.GetWorkflowContentTypes().Select<WorkflowElement, string>((Func<WorkflowElement, string>) (t => t.ContentType)).ToList<string>();
      return num != 0 || list2.Intersect<string>((IEnumerable<string>) list3).Any<string>();
    }

    private static bool ValidateTypeScopeState(IWorkflowExecutionTypeScope typeScope) => WorkflowManager.GetWorkflowContentTypes().Where<WorkflowElement>((Func<WorkflowElement, bool>) (t => t.ContentType == typeScope.ContentType)).Any<WorkflowElement>();

    private static void SetWorkflowType(
      WorkflowDefinitionProxy source,
      WorkflowDefinitionViewModel target)
    {
      string empty = string.Empty;
      string input;
      switch (source.WorkflowType)
      {
        case WorkflowType.Default:
          input = Res.Get<WorkflowResources>().NoApproval;
          break;
        case WorkflowType.Custom:
          input = Res.Get<WorkflowResources>().CustomWorkflowDescription;
          break;
        default:
          input = source.Levels.Count<IWorkflowExecutionLevel>().ToString();
          break;
      }
      target.UIWorkflowType = AntiXssEncoder.HtmlEncode(input, true);
    }

    private static void SetCultureScope(
      IWorkflowExecutionScope source,
      WorkflowScopeViewModel target)
    {
      target.Language = (IList<CultureViewModel>) new List<CultureViewModel>();
      foreach (string culture in source.Cultures)
        target.Language.Add(new CultureViewModel(CultureInfo.GetCultureInfo(culture)));
    }

    private static void SetSiteScope(WorkflowScopeViewModel scope, Telerik.Sitefinity.Multisite.ISite site)
    {
      if (site == null)
        return;
      scope.Site = new WorkflowSimpleSiteSelectorModel()
      {
        SiteId = site.Id,
        Name = site.Name,
        SiteMapRootNodeId = site.SiteMapRootNodeId,
        Cultures = (IList<CultureViewModel>) ((IEnumerable<CultureInfo>) site.PublicContentCultures).Select<CultureInfo, CultureViewModel>((Func<CultureInfo, CultureViewModel>) (x => new CultureViewModel(x))).ToList<CultureViewModel>()
      };
    }

    internal static string GetScopeUITitle(string contentType, bool globalContext = true)
    {
      WorkflowElement workflowElement = WorkflowManager.GetWorkflowContentTypes(globalContext).Where<WorkflowElement>((Func<WorkflowElement, bool>) (x => x.ContentType == contentType)).FirstOrDefault<WorkflowElement>();
      if (workflowElement == null)
        return (string) null;
      string title = workflowElement.Title;
      if (!string.IsNullOrEmpty(workflowElement.ResourceClassId))
        title = Res.Get(workflowElement.ResourceClassId, title);
      return title;
    }

    private static string FormatScopeSummary(
      string siteName,
      string language,
      string contentTypes,
      WorkflowResources resources)
    {
      return !SystemManager.CurrentContext.IsOneSiteMode ? (language != null ? string.Format(AntiXssEncoder.HtmlEncode(resources.ScopeItemFormatFull, true), (object) string.Format("<strong>{0}</strong>", (object) siteName), (object) language, (object) contentTypes) : string.Format(AntiXssEncoder.HtmlEncode(resources.ScopeItemFormatSimple, true), (object) string.Format("<strong>{0}</strong>", (object) siteName), (object) contentTypes)) : (language != null ? string.Format(AntiXssEncoder.HtmlEncode(resources.ScopeItemFormatSimple, true), (object) string.Format("<strong>{0}</strong>", (object) language), (object) contentTypes) : contentTypes);
    }

    private static string GenerateScopeSummary(WorkflowScopeViewModel scope)
    {
      string empty = string.Empty;
      WorkflowResources resources = Res.Get<WorkflowResources>();
      string language = (string) null;
      if (scope.Language != null)
      {
        List<string> list = scope.Language.Select<CultureViewModel, string>((Func<CultureViewModel, string>) (l => l.DisplayName)).ToList<string>();
        if (list.Count > 0)
          language = list.Aggregate<string>((Func<string, string, string>) ((current, next) => current + resources.Separator + next));
      }
      List<string> stringList = new List<string>();
      foreach (WorkflowTypeScopeViewModel typeScope in (IEnumerable<WorkflowTypeScopeViewModel>) scope.TypeScopes)
      {
        if (!typeScope.ContentType.IsNullOrEmpty() && !WorkflowManager.GetScopeUITitle(typeScope.ContentType).IsNullOrEmpty())
          stringList.Add(WorkflowManager.GetScopeUITitle(typeScope.ContentType));
      }
      string contentTypes = stringList.Count > 0 ? WorkflowManager.AggregateStrings(stringList, 2) : AntiXssEncoder.HtmlEncode(resources.ALL_CONTENT, true);
      return WorkflowManager.FormatScopeSummary(scope.Site != null ? AntiXssEncoder.HtmlEncode(scope.Site.Name, true) : AntiXssEncoder.HtmlEncode(resources.SitesAll, true), language, contentTypes, resources);
    }

    private static void SetScopeItemsUI(WorkflowDefinitionViewModel model)
    {
      if (model.Scopes.Count == 0)
      {
        model.UIScopeItem1 = AntiXssEncoder.HtmlEncode(Res.Get<WorkflowResources>().ContentScopeDeleted, true);
        model.UIScopeItem2 = AntiXssEncoder.HtmlEncode(Res.Get<WorkflowResources>().WorkflowNotApplied, true);
        model.UIScopeItem3 = string.Empty;
      }
      else
      {
        model.UIScopeItem1 = model.Scopes.Count > 0 ? WorkflowManager.GenerateScopeSummary(model.Scopes[0]) : string.Empty;
        model.UIScopeItem2 = model.Scopes.Count > 1 ? WorkflowManager.GenerateScopeSummary(model.Scopes[1]) : string.Empty;
        if (model.Scopes.Count == 3)
          model.UIScopeItem3 = WorkflowManager.GenerateScopeSummary(model.Scopes[2]);
        else if (model.Scopes.Count > 3)
          model.UIScopeItem3 = string.Format(AntiXssEncoder.HtmlEncode(Res.Get<WorkflowResources>().AndMoreAreasFormat, true), (object) (model.Scopes.Count - 2));
        else
          model.UIScopeItem3 = string.Empty;
      }
    }

    private static string AggregateStrings(List<string> stringList, int maxItems = 5)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string separator = Res.Get<WorkflowResources>().Separator;
      if (stringList.Count > maxItems)
      {
        stringBuilder.Append(stringList.Take<string>(maxItems).Aggregate<string>((Func<string, string, string>) ((current, next) => current + separator + next)));
        stringBuilder.Append(string.Format(Res.Get<WorkflowResources>().AndMoreContentItemsFormat, (object) (stringList.Count - maxItems)));
      }
      else
        stringBuilder.Append(stringList.Aggregate<string>((Func<string, string, string>) ((current, next) => current + separator + next)));
      return stringBuilder.ToString();
    }

    /// <summary>Converts from string to QueryData.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns></returns>
    public static QueryData ConvertFrom(string value)
    {
      if (string.IsNullOrEmpty(value))
        return (QueryData) null;
      using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(value)))
        return (QueryData) new DataContractJsonSerializer(typeof (QueryData)).ReadObject((Stream) memoryStream);
    }

    public static void AddLanguageToWorkflowContext(
      Dictionary<string, string> context,
      CultureInfo language)
    {
      if (language == null)
        return;
      context.Add("Language", language.Name);
    }

    /// <summary>
    /// Executes the specified workflow operations if the current user has permissions.
    /// </summary>
    /// <param name="workflowItem">The workflow item.</param>
    /// <param name="operationNames">The operation names.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="checkout">if set to <c>true</c> the item is checked out before calling the service.</param>
    internal static void ExecuteWorkflowOperations(
      IApprovalWorkflowItem workflowItem,
      IList<string> operationNames,
      string providerName = null,
      bool checkout = false)
    {
      if (workflowItem == null)
        throw new ArgumentNullException(nameof (workflowItem));
      if (operationNames == null)
        throw new ArgumentNullException(nameof (operationNames));
      if (operationNames.Count == 0)
        throw new ArgumentException("The count of operations cannot be zero.");
      IDictionary<string, DecisionActivity> currentDecisions = WorkflowManager.GetCurrentDecisions(workflowItem.GetType(), providerName, workflowItem.Id);
      if (currentDecisions == null)
        return;
      int index = 0;
      bool flag = false;
      for (; index < operationNames.Count; ++index)
      {
        if (currentDecisions.ContainsKey(operationNames[index]))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      for (; index < operationNames.Count; ++index)
      {
        if (checkout)
        {
          IAnyDraftFacade anyDraftFacade = (string.IsNullOrEmpty(providerName) ? App.WorkWith() : App.Prepare().SetContentProvider(providerName).WorkWith()).AnyContentItem(workflowItem.GetType(), workflowItem.Id);
          if (!anyDraftFacade.IsCheckedOut())
            anyDraftFacade.CheckOut().SaveChanges();
        }
        try
        {
          WorkflowManager.MessageWorkflow(workflowItem.Id, workflowItem.GetType(), providerName, operationNames[index], checkout, new Dictionary<string, string>());
        }
        catch
        {
          break;
        }
      }
    }

    /// <summary>
    /// Configures the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowManager" /> whether to use role's input (external) endpoint to
    /// message the workflow service or the internal one (the default). Changes the <see cref="T:Telerik.Sitefinity.Workflow.Configuration.WorkflowConfig" />).
    /// </summary>
    /// <param name="useExternalEndpoint"><c>true</c> to use the external endpoint; <c>false</c> to reset to the default internal endpoint.</param>
    /// <param name="elevatePrivileges"><c>true</c> to bypass the configuration security.</param>
    public static void ConfigureAzureEndpoint(bool useExternalEndpoint, bool elevatePrivileges = false)
    {
      if (elevatePrivileges)
        Config.ElevatedUpdateSection<WorkflowConfig>((Action<WorkflowConfig>) (config => config.UseExternalEndpointOnWindowsAzure = useExternalEndpoint));
      else
        Config.UpdateSection<WorkflowConfig>((Action<WorkflowConfig>) (config => config.UseExternalEndpointOnWindowsAzure = useExternalEndpoint));
    }

    private static void ConfigureBindingTimeouts(
      BasicHttpBinding binding,
      BindingTimeoutsElememet bindingTimeouts)
    {
      if (bindingTimeouts.AllPropertiesTimeoutDefaultValue.HasValue)
      {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds((double) bindingTimeouts.AllPropertiesTimeoutDefaultValue.Value);
        binding.CloseTimeout = timeSpan;
        binding.OpenTimeout = timeSpan;
        binding.ReceiveTimeout = timeSpan;
        binding.SendTimeout = timeSpan;
      }
      if (bindingTimeouts.CloseTimeout.HasValue)
        binding.CloseTimeout = TimeSpan.FromMilliseconds((double) bindingTimeouts.CloseTimeout.Value);
      if (bindingTimeouts.OpenTimeout.HasValue)
        binding.OpenTimeout = TimeSpan.FromMilliseconds((double) bindingTimeouts.OpenTimeout.Value);
      if (bindingTimeouts.ReceiveTimeout.HasValue)
        binding.ReceiveTimeout = TimeSpan.FromMilliseconds((double) bindingTimeouts.ReceiveTimeout.Value);
      if (!bindingTimeouts.SendTimeout.HasValue)
        return;
      binding.SendTimeout = TimeSpan.FromMilliseconds((double) bindingTimeouts.SendTimeout.Value);
    }

    private static WorkflowStatusContext ResolveWorkflowStatus(
      System.Type itemType,
      string providerName,
      Guid itemId,
      CultureInfo itemCulture = null)
    {
      IManager manager = !SystemManager.CurrentContext.GlobalTransaction.IsNullOrEmpty() ? ManagerBase.GetMappedManagerInTransaction(itemType, providerName, SystemManager.CurrentContext.GlobalTransaction) : ManagerBase.GetMappedManager(itemType, providerName);
      return WorkflowManager.ResolveWorkflowStatus(manager.GetItem(itemType, itemId) as IWorkflowItem, manager, itemCulture);
    }

    internal static WorkflowStatusContext ResolveWorkflowStatus(
      IWorkflowItem workflowItem,
      IManager manager,
      CultureInfo itemCulture = null)
    {
      WorkflowStatusContext workflowStatusContext = new WorkflowStatusContext();
      if (manager is ILifecycleManager lifecycleManager)
      {
        ILifecycleDataItem live = lifecycleManager.Lifecycle.GetLive(workflowItem as ILifecycleDataItem);
        if (live != null)
        {
          ILifecycleDataItem lifecycleDataItem = live;
          workflowStatusContext.IsItemPublished = lifecycleDataItem == null || LifecycleExtensions.IsItemPublished<ILifecycleDataItem>(lifecycleDataItem, itemCulture);
        }
      }
      else if (workflowItem is PageNode page)
      {
        PageSiteNode siteMapNode = page.GetSiteMapNode();
        workflowStatusContext.IsItemPublished = LifecycleExtensions.IsItemPublished<LifecycleDataItemProxy>(new LifecycleDataItemProxy(siteMapNode.CurrentPageDataItem), itemCulture);
      }
      if (workflowItem != null)
      {
        if (workflowItem is IApprovalWorkflowItem approvalWorkflowItem)
        {
          itemCulture = itemCulture ?? SystemManager.CurrentContext.Culture;
          string str = approvalWorkflowItem.GetWorkflowState(itemCulture);
          if (str.IsNullOrEmpty())
            str = workflowStatusContext.IsItemPublished ? "Published" : "Draft";
          workflowStatusContext.WorkflowStatus = str;
        }
        else
        {
          ApprovalTrackingRecord approvalTrackingRecord = workflowItem.GetCurrentApprovalTrackingRecord(itemCulture);
          workflowStatusContext.WorkflowStatus = approvalTrackingRecord == null ? (workflowStatusContext.IsItemPublished ? "Published" : "Draft") : approvalTrackingRecord.Status;
        }
      }
      else
        workflowStatusContext.WorkflowStatus = (string) null;
      return workflowStatusContext;
    }

    /// <summary>
    /// Loads the xamlx file representing ContentApprovalWorkflow and returns an <see cref="T:System.Activities.Activity" />
    /// which represents that workflow.
    /// </summary>
    /// <returns>An instance of <see cref="T:System.Activities.Activity" /> that represents the workflow.</returns>
    private static Activity LoadContentApprovalWorkflow(string workflowServiceVirtualPath)
    {
      string workflowCacheKey = WorkflowManager.GetWorkflowCacheKey(workflowServiceVirtualPath);
      Sequence rootActivity = (Sequence) WorkflowManager.Cache.GetData(workflowCacheKey);
      if (rootActivity == null)
      {
        lock (WorkflowManager.Cache)
        {
          rootActivity = (Sequence) WorkflowManager.Cache.GetData(workflowCacheKey);
          if (rootActivity == null)
          {
            using (new MethodPerformanceRegion(string.Format("Load Workflow for path: {0}", (object) workflowServiceVirtualPath)))
            {
              using (Stream stream = SitefinityFile.Open(workflowServiceVirtualPath))
              {
                rootActivity = (Sequence) ((WorkflowService) XamlServices.Load(stream)).Body;
                if (!rootActivity.Variables.Any<Variable>((Func<Variable, bool>) (v => v.Name == "workflowExecutionDefinition")))
                {
                  Collection<Variable> variables = rootActivity.Variables;
                  Variable<IWorkflowExecutionDefinition> variable = new Variable<IWorkflowExecutionDefinition>();
                  variable.Name = "workflowExecutionDefinition";
                  variables.Add((Variable) variable);
                }
                WorkflowManager.ProcessWorkflow(rootActivity);
                WorkflowManager.Cache.Add(workflowCacheKey, (object) rootActivity, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowLevel), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowScope), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowTypeScope), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (WorkflowPermission), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteItemLink), typeof (WorkflowScope).FullName), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
              }
            }
          }
        }
      }
      return (Activity) rootActivity;
    }

    /// <summary>
    /// Processes the workflow activities to prepare it for either InProcess execution or XAML WCF service execution
    /// For InProces execution we remove the service Request/Reply activites and insert a custom activity to pass workflow execution arguments
    /// </summary>
    /// <param name="rootActivity">The root activity.</param>
    private static void ProcessWorkflow(Sequence rootActivity)
    {
      if (Config.Get<WorkflowConfig>().RunWorkflowAsService)
        return;
      if (rootActivity.Activities[0] is Receive)
        rootActivity.Activities.RemoveAt(0);
      if (!(rootActivity.Activities[0] is WorkflowManager.SetWorfklowVariables))
        rootActivity.Activities.Insert(0, (Activity) new WorkflowManager.SetWorfklowVariables());
      if (!(rootActivity.Activities[rootActivity.Activities.Count - 1] is SendReply))
        return;
      rootActivity.Activities.RemoveAt(rootActivity.Activities.Count - 1);
    }

    private static string GetWorkflowCacheKey(string workflowServiceVirtualPath) => string.Format("{0}_{1}", (object) "workflowServiceCache", (object) workflowServiceVirtualPath);

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Workflow.FlowchartWorkflowInspector" /> for the ContentApprovalWorkflow
    /// service.
    /// </summary>
    /// <returns></returns>
    private static FlowchartWorkflowInspector GetContentServiceFlowchartInspector(
      string workflowServiceUrl)
    {
      Sequence sequence = (Sequence) WorkflowManager.LoadContentApprovalWorkflow(workflowServiceUrl);
      Flowchart workflowInstance = (Flowchart) null;
      foreach (Activity activity in sequence.Activities)
      {
        if (activity.GetType() == typeof (Flowchart))
          workflowInstance = (Flowchart) activity;
      }
      return workflowInstance != null ? new FlowchartWorkflowInspector(workflowInstance) : throw new System.InvalidOperationException("No flowcharts have been found in the ContentApprovalWorkflow.");
    }

    /// <summary>Returns the host of the application.</summary>
    /// <returns>String with protocol and host.</returns>
    private static string GetHost() => "http://" + SystemManager.CurrentHttpContext.Request.Url.Authority;

    private static void CopyToWorkflowLevels(
      IList<WorkflowLevelViewModel> source,
      IList<WorkflowLevel> target,
      WorkflowManager workflowManager)
    {
      List<Guid> list = source.Select<WorkflowLevelViewModel, Guid>((Func<WorkflowLevelViewModel, Guid>) (wl => wl.Id)).ToList<Guid>();
      List<WorkflowLevel> workflowLevelList = new List<WorkflowLevel>((IEnumerable<WorkflowLevel>) target);
      List<WorkflowLevelViewModel> workflowLevelViewModelList = new List<WorkflowLevelViewModel>((IEnumerable<WorkflowLevelViewModel>) source);
      foreach (WorkflowLevel workflowLevel in workflowLevelList)
      {
        if (!list.Contains(workflowLevel.Id))
        {
          target.Remove(workflowLevel);
          workflowManager.Delete(workflowLevel);
        }
      }
      foreach (WorkflowLevelViewModel workflowLevelViewModel in workflowLevelViewModelList)
      {
        Guid id = workflowLevelViewModel.Id;
        WorkflowLevel workflowLevel = target.Where<WorkflowLevel>((Func<WorkflowLevel, bool>) (l => l.Id == id)).SingleOrDefault<WorkflowLevel>() ?? workflowManager.CreateWorkflowLevel();
        workflowLevel.ActionName = workflowLevelViewModel.ActionName;
        workflowLevel.NotifyApprovers = workflowLevelViewModel.NotifyApprovers;
        workflowLevel.NotifyAdministrators = workflowLevelViewModel.NotifyAdministrators;
        workflowLevel.CustomEmailRecipients = workflowLevelViewModel.CustomEmailRecipients;
        workflowLevel.Ordinal = workflowLevelViewModel.Ordinal;
        WorkflowManager.CopyToLevelPermissions(workflowLevelViewModel.Permissions, workflowLevel.Permissions, workflowManager);
        target.Add(workflowLevel);
      }
    }

    private static void CopyToWorkflowScopes(
      IList<WorkflowScopeViewModel> source,
      IList<WorkflowScope> target,
      WorkflowManager workflowManager)
    {
      List<Guid> list = source.Select<WorkflowScopeViewModel, Guid>((Func<WorkflowScopeViewModel, Guid>) (ws => ws.Id)).ToList<Guid>();
      List<WorkflowScope> workflowScopeList = new List<WorkflowScope>((IEnumerable<WorkflowScope>) target);
      List<WorkflowScopeViewModel> workflowScopeViewModelList = new List<WorkflowScopeViewModel>((IEnumerable<WorkflowScopeViewModel>) source);
      foreach (WorkflowScope workflowScope in workflowScopeList)
      {
        if (!list.Contains(workflowScope.Id))
        {
          target.Remove(workflowScope);
          workflowManager.Delete(workflowScope);
        }
      }
      foreach (WorkflowScopeViewModel workflowScopeViewModel in workflowScopeViewModelList)
      {
        Guid id = workflowScopeViewModel.Id;
        WorkflowScope scope = target.Where<WorkflowScope>((Func<WorkflowScope, bool>) (s => s.Id == id)).SingleOrDefault<WorkflowScope>() ?? workflowManager.CreateWorkflowScope();
        if (workflowScopeViewModel.Language != null)
          scope.SetLanguages(workflowScopeViewModel.Language.Select<CultureViewModel, string>((Func<CultureViewModel, string>) (l => l.ShortName)));
        List<WorkflowSimpleSiteSelectorModel> source1 = new List<WorkflowSimpleSiteSelectorModel>();
        if (workflowScopeViewModel.Site != null)
          source1.Add(workflowScopeViewModel.Site);
        WorkflowManager.CopyToWorkflowSites((IList<WorkflowSimpleSiteSelectorModel>) source1, scope, workflowManager);
        if (workflowScopeViewModel.TypeScopes != null)
        {
          List<WorkflowTypeScope> workflowTypeScopeList = new List<WorkflowTypeScope>((IEnumerable<WorkflowTypeScope>) scope.TypeScopes);
          List<WorkflowTypeScopeViewModel> typeScopeViewModelList = new List<WorkflowTypeScopeViewModel>((IEnumerable<WorkflowTypeScopeViewModel>) workflowScopeViewModel.TypeScopes);
          foreach (WorkflowTypeScope workflowTypeScope in workflowTypeScopeList)
          {
            if (!workflowScopeViewModel.TypeScopes.Select<WorkflowTypeScopeViewModel, string>((Func<WorkflowTypeScopeViewModel, string>) (ts => ts.ContentType)).ToList<string>().Contains(workflowTypeScope.ContentType))
            {
              scope.TypeScopes.Remove(workflowTypeScope);
              workflowManager.Delete(workflowTypeScope);
            }
          }
          foreach (WorkflowTypeScopeViewModel typeScopeViewModel in typeScopeViewModelList)
          {
            string contentType = typeScopeViewModel.ContentType;
            WorkflowTypeScope typeScope = scope.TypeScopes.Where<WorkflowTypeScope>((Func<WorkflowTypeScope, bool>) (tsv => tsv.ContentType == contentType)).SingleOrDefault<WorkflowTypeScope>() ?? workflowManager.CreateWorkflowTypeScope();
            typeScope.ContentType = typeScopeViewModel.ContentType;
            typeScope.SetContentFilter((IEnumerable<string>) typeScopeViewModel.ContentFilter);
            typeScope.IncludeChildren = typeScopeViewModel.IncludeChildren;
            scope.TypeScopes.Add(typeScope);
          }
        }
        target.Add(scope);
      }
    }

    private static void CopyToWorkflowSites(
      IList<WorkflowSimpleSiteSelectorModel> source,
      WorkflowScope scope,
      WorkflowManager workflowManager)
    {
      List<Guid> clientIds = source.Select<WorkflowSimpleSiteSelectorModel, Guid>((Func<WorkflowSimpleSiteSelectorModel, Guid>) (ws => ws.SiteId)).ToList<Guid>();
      List<SiteItemLink> list = workflowManager.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (x => x.ItemId == scope.Id)).ToList<SiteItemLink>();
      List<Guid> serverIds = list.Select<SiteItemLink, Guid>((Func<SiteItemLink, Guid>) (x => x.SiteId)).ToList<Guid>();
      IEnumerable<Guid> guids = clientIds.Where<Guid>((Func<Guid, bool>) (x => !serverIds.Contains(x)));
      foreach (Guid guid in serverIds.Where<Guid>((Func<Guid, bool>) (x => !clientIds.Contains(x))))
      {
        Guid deletedSiteId = guid;
        SiteItemLink link = list.FirstOrDefault<SiteItemLink>((Func<SiteItemLink, bool>) (x => x.SiteId == deletedSiteId));
        if (link != null)
          workflowManager.Delete(link);
      }
      foreach (Guid siteId in guids)
        workflowManager.AddItemLink(siteId, (IDataItem) scope);
    }

    internal static void CopyToLevelPermissions(
      IList<WorkflowPermissionViewModel> source,
      IList<WorkflowPermission> target,
      WorkflowManager workflowManager)
    {
      List<Guid> list = source.Select<WorkflowPermissionViewModel, Guid>((Func<WorkflowPermissionViewModel, Guid>) (wp => wp.Id)).ToList<Guid>();
      List<WorkflowPermission> workflowPermissionList = new List<WorkflowPermission>((IEnumerable<WorkflowPermission>) target);
      List<WorkflowPermissionViewModel> permissionViewModelList = new List<WorkflowPermissionViewModel>((IEnumerable<WorkflowPermissionViewModel>) source);
      foreach (WorkflowPermission workflowPermission in workflowPermissionList)
      {
        if (!list.Contains(workflowPermission.Id))
        {
          target.Remove(workflowPermission);
          workflowManager.Delete(workflowPermission);
        }
        else
        {
          Guid id = workflowPermission.Id;
          WorkflowPermissionViewModel permissionViewModel = source.Where<WorkflowPermissionViewModel>((Func<WorkflowPermissionViewModel, bool>) (wp => wp.Id == id)).SingleOrDefault<WorkflowPermissionViewModel>();
          if (permissionViewModel != null)
            permissionViewModelList.Remove(permissionViewModel);
        }
      }
      foreach (WorkflowPermissionViewModel permissionViewModel in permissionViewModelList)
      {
        WorkflowPermission workflowPermission = workflowManager.CreateWorkflowPermission();
        workflowPermission.ActionName = permissionViewModel.ActionName;
        workflowPermission.PrincipalType = permissionViewModel.PrincipalType;
        workflowPermission.PrincipalId = permissionViewModel.PrincipalId;
        workflowPermission.PrincipalName = permissionViewModel.PrincipalName;
        target.Add(workflowPermission);
      }
    }

    /// <summary>Gets the shareable types.</summary>
    System.Type[] IMultisiteEnabledManager.GetShareableTypes() => new System.Type[1]
    {
      typeof (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition)
    };

    /// <summary>Gets the site specific types.</summary>
    System.Type[] IMultisiteEnabledManager.GetSiteSpecificTypes() => new System.Type[0];

    /// <summary>
    /// Deletes the site links, deletes workflow scopes for the deleted sites,
    /// setting the workflow definition inactive if the deleted scopes are the only assigned to the workflow definition.
    /// </summary>
    /// <param name="sites">The sites.</param>
    public void DeleteSiteLinks(params Guid[] sites)
    {
      SiteItemLink[] array = this.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => sites.Contains<Guid>(l.SiteId) && l.ItemType.Equals(typeof (WorkflowScope).FullName))).ToArray<SiteItemLink>();
      foreach (IGrouping<Guid, SiteItemLink> grouping in ((IEnumerable<SiteItemLink>) array).GroupBy<SiteItemLink, Guid>((Func<SiteItemLink, Guid>) (l => l.ItemId)))
      {
        Guid itemId = grouping.Key;
        if (!this.GetSiteItemLinks().Any<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == itemId && !sites.Contains<Guid>(l.SiteId))))
        {
          WorkflowScope workflowScope = this.GetWorkflowScope(itemId);
          Telerik.Sitefinity.Workflow.Model.WorkflowDefinition definition = workflowScope.Definition;
          workflowScope.Definition = (Telerik.Sitefinity.Workflow.Model.WorkflowDefinition) null;
          this.Delete(workflowScope);
        }
      }
      foreach (SiteItemLink link in array)
        this.Delete(link);
    }

    /// <summary>
    /// This activity is dynamically inserted in workflow sequence in order to be able to pass arguemnts to the workflow context
    /// </summary>
    internal sealed class SetWorfklowVariables : CodeActivity
    {
      protected override void Execute(CodeActivityContext context)
      {
        if (!(WorkflowManager.runningWorkflowsArgs[context.WorkflowInstanceId.ToString()] is Dictionary<string, object> runningWorkflowsArg) || runningWorkflowsArg.Count <= 0)
          return;
        HttpContext.Current = runningWorkflowsArg["httpContext"] as HttpContext;
        if (runningWorkflowsArg.ContainsKey("contextBag") && runningWorkflowsArg["contextBag"] is Dictionary<string, string> dictionary)
        {
          if (dictionary.ContainsKey("SegmentId"))
            HttpContext.Current.Items[(object) "SegmentId"] = (object) dictionary["SegmentId"];
          if (dictionary.ContainsKey("PublicationDate"))
            HttpContext.Current.Items[(object) "PublicationDate"] = (object) dictionary["PublicationDate"];
        }
        foreach (object property in context.DataContext.GetProperties())
        {
          object obj;
          if (property is PropertyDescriptor propertyDescriptor && runningWorkflowsArg.TryGetValue(propertyDescriptor.Name, out obj))
            propertyDescriptor.SetValue((object) context.DataContext, obj);
        }
      }
    }

    internal class Constants
    {
      public const string ContentType = "ContentType";
      public const string Transaction = "sf_global_transaction";
      public const string LanguageOverrideKey = "Language";
    }

    internal static class OperationNames
    {
      internal const string Publish = "Publish";
      internal const string SaveDraft = "SaveDraft";
      internal const string DiscardDraft = "DiscardDraft";
      internal const string Schedule = "Schedule";
      internal const string StopSchedule = "StopSchedule";
      internal const string StopScheduleUnpublish = "StopScheduleUnpublish";
      internal const string Unpublish = "Unpublish";
      internal const string Reject = "Reject";
      internal const string SaveAsAwaitingPublishing = "SaveAsAwaitingPublishing";
      internal const string SaveAsAwaitingApproval = "SaveAsAwaitingApproval";
      internal const string SaveAsAwaitingReview = "SaveAsAwaitingReview";
      internal const string SendForApproval = "SendForApproval";
      internal const string SendForReview = "SendForReview";
      internal const string SendForPublishing = "SendForPublishing";
      internal const string Delete = "Delete";
      internal const string Upload = "Upload";
      internal const string UploadDraft = "UploadDraft";
      internal const string UploadPublished = "UploadPublished";
      internal const string DefaultWorkflowOperation = "DefaultWorkflowOperation";
    }
  }
}
