// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.IPublishingAdminService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Publishing.Web.Services
{
  /// <summary>
  /// Web service facilitating the backend adminstration of Publishing points - creation, modification, deletion
  /// </summary>
  [ServiceContract(Namespace = "PublishingAdminService")]
  public interface IPublishingAdminService
  {
    /// <summary>
    /// Gets the list of publishing points persisted in the system.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip number(linq).</param>
    /// <param name="take">The take number(linq)</param>
    /// <param name="filter">The dynamic linq expression -&gt; filter.</param>
    /// <returns>
    /// a list of publishing points - with limited data like title, owner, and date of creation
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?providerName={providerName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<PublishingPointViewModel> GetPublishingPoints(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the publishing point.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">if set to <c>true</c> the method will return a blank publishing point(not persisted)</param>
    /// <returns>
    /// a specific publishing point or a blank(new) publishing point
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{pointId}/?providerName={providerName}&createNew={createNew}&itemTemplate={itemTemplate}")]
    DataItemContext<PublishingPointDetailViewModel> GetPublishingPoint(
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate);

    /// <summary>Saves the publishing point.</summary>
    /// <param name="point">The point.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">need for compatiblity with the get method</param>
    /// <returns>the saved publishing point</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{pointId}/?providerName={providerName}&createNew={createNew}&itemTemplate={itemTemplate}")]
    DataItemContext<PublishingPointDetailViewModel> SavePublishingPoint(
      DataItemContext<PublishingPointDetailViewModel> point,
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate);

    /// <summary>Deletes a publishing point.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <param name="createNew">if set to <c>true</c> [create new].</param>
    /// <returns>true on success</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{pointId}/?providerName={providerName}&createNew={createNew}&itemTemplate={itemTemplate}")]
    bool DeletePublishingPoint(
      string providerName,
      string pointId,
      bool createNew,
      string itemTemplate);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}")]
    bool BatchDeletePoints(string providerName, string[] Ids);

    /// <summary>
    /// Gets a collection context of outgoing publishing pipes.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pipeTypeName">Name of the pipe type.</param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>A collection context of publishing pipes.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/pipes/?providerName={providerName}&pipeTypeName={pipeTypeName}&sort={sort}&filter={filter}&skip={skip}&take={take}")]
    CollectionContext<PublishingPipeViewModel> GetOutgoingPublishingPipes(
      string providerName,
      string pipeTypeName,
      string sort,
      string filter,
      int skip,
      int take);

    /// <summary>
    /// Gets a collection context of outgoing publishing pipes.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pipeTypeName">Name of the pipe type.</param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>A collection context of publishing pipes.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/indboundpipes/?providerName={providerName}&pipeTypeName={pipeTypeName}&sort={sort}&filter={filter}&skip={skip}&take={take}")]
    CollectionContext<PublishingPipeViewModel> GetInboundPublishingPipes(
      string providerName,
      string pipeTypeName,
      string sort,
      string filter,
      int skip,
      int take);

    /// <summary>
    /// Activates or deactivates a single publishing point.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The id of the publishing point to be changed.</param>
    /// <param name="setActive">if set to <c>true</c> the point is activated; otherwise deactivated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/setactive/{pointId}/?providerName={providerName}&setActive={setActive}")]
    bool SetActive(string providerName, string pointId, bool setActive);

    /// <summary>
    /// Activates or deactivates an array of publishing points.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="ids">An array containing the Ids of the publishing points to be deleted.</param>
    /// <param name="setActive">if set to <c>true</c> the points are activated; otherwise deactivated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batch/setactive/?providerName={providerName}&setActive={setActive}")]
    bool BatchSetActive(string providerName, string[] ids, bool setActive);

    /// <summary>Runs the pipes.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/runpipes/{pointId}/?providerName={providerName}")]
    bool RunPipes(string providerName, string pointId);

    /// <summary>Runs the pipes.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/reindex/{pointId}/?providerName={providerName}")]
    bool ReindexSearchContent(string providerName, string pointId);

    /// <summary>Returns the status of the reindex operation.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="pointId">The point id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getReindexStatus/")]
    Dictionary<Guid, ReindexStatusDto> GetReindexStatus();
  }
}
