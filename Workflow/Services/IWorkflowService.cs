// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.IWorkflowService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Workflow.Services
{
  [ServiceContract]
  public interface IWorkflowService
  {
    /// <summary>Gets the workflow visual elements.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemCulture">The item culture to be considered when working with the item status</param>
    /// <remarks>The thread UI culture for the labels is set through the localization behavior</remarks>
    /// <returns></returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{itemId}/?itemType={itemType}&providerName={providerName}&itemCulture={itemCulture}&showMoreActions={showMoreActions}")]
    [OperationContract]
    WorkflowVisualElementsCollection GetWorkflowVisualElements(
      string itemType,
      string providerName,
      string itemId,
      string itemCulture,
      bool showMoreActions = true);

    /// <summary>Gets the workflow visual elements in XML.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemCulture">The item culture to be considered when working with the item status</param>
    /// <remarks>The thread UI culture for the labels is set through the localization behavior</remarks>
    /// <returns></returns>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{itemId}/?itemType={itemType}&providerName={providerName}&itemCulture={itemCulture}&showMoreActions={showMoreActions}")]
    [OperationContract]
    WorkflowVisualElementsCollection GetWorkflowVisualElementsInXml(
      string itemType,
      string providerName,
      string itemId,
      string itemCulture,
      bool showMoreActions = true);

    /// <summary>Messages the workflow.</summary>
    /// <param name="contextBag">Key value collection to be passed to the workflow</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="workflowOperation">The workflow operation name</param>
    /// <remarks>In multilingual mode the culture is automatically set by the localization behavior</remarks>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/MessageWorkflow/{itemId}/?itemType={itemType}&providerName={providerName}&workflowOperation={workflowOperation}")]
    [OperationContract]
    string MessageWorkflow(
      KeyValuePair<string, string>[] contextBag,
      string itemId,
      string itemType,
      string providerName,
      string workflowOperation);

    /// <summary>Messages the workflow in XML.</summary>
    /// <param name="contextBag">The context bag.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <remarks>In multilingual mode the culture is automatically set by the localization behavior</remarks>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/MessageWorkflow/{itemId}/?itemType={itemType}&providerName={providerName}&workflowOperation={workflowOperation}")]
    [OperationContract]
    string MessageWorkflowInXml(
      KeyValuePair<string, string>[] contextBag,
      string itemId,
      string itemType,
      string providerName,
      string workflowOperation);
  }
}
