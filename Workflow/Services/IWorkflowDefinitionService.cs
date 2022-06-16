// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.IWorkflowDefinitionService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow.Services
{
  /// <summary>
  /// Defines the members of the workflow definition service.
  /// </summary>
  [ServiceContract]
  public interface IWorkflowDefinitionService
  {
    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> in JSON format.
    /// </summary>
    /// <param name="provider">
    /// The name of the workflow provider from which the workflow definitions should be retrived.
    /// </param>
    /// <param name="sortExpression">
    /// The sort expression used to order the retrieved workflow definitions.
    /// </param>
    /// <param name="skip">
    /// The number of workflow definitions to skip before populating the collection (used primarily for paging).
    /// </param>
    /// <param name="take">
    /// The maximum number of workflow definitions to take in the collection (used primarily for paging).
    /// </param>
    /// <param name="filter">
    /// The filter expression in dynamic LINQ format used to filter the retrieved workflow definitions.
    /// </param>
    /// <param name="workflowFilter">The workflow filter.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object withworkflow definitions and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of WorkflowDefinitionViewModel objects in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&workflowFilter={workflowFilter}")]
    [OperationContract]
    CollectionContext<WorkflowDefinitionViewModel> GetWorkflowDefinitions(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string workflowFilter);

    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> in XML format.
    /// </summary>
    /// <param name="provider">The name of the workflow provider from which the workflow definitions should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved workflow definitions.</param>
    /// <param name="skip">The number of workflow definitions to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of workflow definitions to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved workflow definitions.</param>
    /// <param name="workflowFilter">The workflow filter.</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object withworkflow definitions and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of WorkflowDefinitionViewModel objects in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&workflowFilter={workflowFilter}")]
    [OperationContract]
    CollectionContext<WorkflowDefinitionViewModel> GetWorkflowDefinitionsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string workflowFilter);

    /// <summary>
    /// Saves the workflow definition and returns the saved definition in JSON.
    /// </summary>
    /// <param name="workflowDefinitionViewModel">The view model of the workflow definition.</param>
    /// <param name="workflowDefinitionId">The workflow definition id.</param>
    /// <param name="provider">The provider name through which the workflow definition ought to be saved.</param>
    /// <returns>The saved workflow definition view model object.</returns>
    [WebHelp(Comment = "Saves the workflow definition and returns the saved definition in JSON.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{workflowDefinitionId}/?provider={provider}")]
    [OperationContract]
    WorkflowDefinitionViewModel SaveWorkflowDefinition(
      WorkflowDefinitionViewModel workflowDefinitionViewModel,
      string workflowDefinitionId,
      string provider);

    /// <summary>
    /// Saves the workflow definition and returns the saved definition in XML.
    /// </summary>
    /// <param name="workflowDefinitionViewModel">The view model of the workflow definition.</param>
    /// <param name="workflowDefinitionId">The workflow definition id.</param>
    /// <param name="provider">The provider name through which the workflow definition ought to be saved.</param>
    /// <returns>The saved workflow definition view model object.</returns>
    [WebHelp(Comment = "Saves the workflow definition and returns the saved definition in XML.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{workflowDefinitionId}/?provider={provider}")]
    [OperationContract]
    WorkflowDefinitionViewModel SaveWorkflowDefinitionInXml(
      WorkflowDefinitionViewModel workflowDefinitionViewModel,
      string workflowDefinitionId,
      string provider);

    /// <summary>Deletes the specified workflow definitions.</summary>
    /// <param name="workflowDefinitionIds">
    /// Id of the workflow definitions to be deleted.
    /// </param>
    /// <param name="provider">
    /// The name of the workflow provider from which the workflow definition should be deleted.
    /// </param>
    [WebHelp(Comment = "Deletes the workflow definition.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batch/?provider={provider}")]
    [OperationContract]
    void DeleteWorkflowDefinitions(string[] workflowDefinitionIds, string provider);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "lang-scope/?skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<CultureViewModel> GetLanguagesForSites(
      Guid[] siteIds,
      int skip,
      int take,
      string filter);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "content-scope/?skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<WorkflowTypeScopeViewModel> GetContentScope(
      int skip,
      int take,
      string filter);
  }
}
