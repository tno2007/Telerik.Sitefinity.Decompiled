// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IIssueReportService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Interface that defines the members of the service working with all types of campaigns (base type).
  /// </summary>
  [ServiceContract]
  public interface IIssueReportService
  {
    /// <summary>
    /// Gets all campaigns of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all campaigns of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [Obsolete("Use Campaign.svc/issues/ instead.")]
    IEnumerable<IssueReportViewModel> GetIssueReports(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all campaigns of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all campaigns of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [Obsolete("Use Campaign.svc/xml/issues/ instead.")]
    IEnumerable<IssueReportViewModel> GetIssueReportsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the issue unique clicks. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ClickStatViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets the unique clicks for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/uniqueclicks/{issueId}/?provider={provider}")]
    IEnumerable<ClickStatViewModel> GetIssueUniqueClicks(
      string issueId,
      string provider);

    /// <summary>
    /// Gets the issue unique clicks. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ClickStatViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets the unique clicks for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/uniqueclicks/{issueId}/?provider={provider}")]
    IEnumerable<ClickStatViewModel> GetIssueUniqueClicksXml(
      string issueId,
      string provider);

    /// <summary>
    /// Gets the issue clicks by hour. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    [WebHelp(Comment = "Gets clicks by hour for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/clicksbyhour/{issueId}/?provider={provider}")]
    IEnumerable<KeyValuePair<string, int>> GetIssueClicksByHour(
      string issueId,
      string provider);

    /// <summary>
    /// Gets the issue clicks by hour. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    [WebHelp(Comment = "Gets clicks by hour for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/clicksbyhour/{issueId}/?provider={provider}")]
    IEnumerable<KeyValuePair<string, int>> GetIssueClicksByHourXml(
      string issueId,
      string provider);

    /// <summary>
    /// Gets the issue subscribers. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueSubscriberViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets subscribers for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/subscribers/{issueId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&byClickedLink={byClickedLink}")]
    CollectionContext<IssueSubscriberViewModel> GetIssueSubscribers(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string byClickedLink);

    /// <summary>
    /// Gets the issue subscribers. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueSubscriberViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets subscribers a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/subscribers/{issueId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&byClickedLink={byClickedLink}")]
    CollectionContext<IssueSubscriberViewModel> GetIssueSubscribersXml(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string byClickedLink);

    /// <summary>
    /// Gets the issue subscriber clicks. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets subscriber clicks for a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/subscriberclicks/{issueId}/?subscriberId={subscriberId}&provider={provider}")]
    IEnumerable<SubscriberIssueClickViewModel> GetIssueSubscriberClicks(
      string issueId,
      string subscriberId,
      string provider);

    /// <summary>
    /// Gets the issue subscriber clicks. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets subscriber clicks a given issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/subscriberclicks/{issueId}/?subscriberId={subscriberId}&provider={provider}")]
    IEnumerable<SubscriberIssueClickViewModel> GetIssueSubscriberClicksXml(
      string issueId,
      string subscriberId,
      string provider);

    /// <summary>
    /// Gets all the clicks for an issue in a given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">The issue id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all the clicks for an issue in a given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/clicks/{issueId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SubscriberIssueClickViewModel> GetIssueClicks(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all the clicks for an issue in a given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">The issue id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all the clicks for an issue in a given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/clicks/{issueId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SubscriberIssueClickViewModel> GetIssueClicksXml(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the total number of clicks for each link in an issue. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    [WebHelp(Comment = "Gets the total number of clicks for each link in an issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/totallinkclicks/{issueId}/?search={search}&provider={provider}")]
    IEnumerable<KeyValuePair<string, int>> GetIssueTotalLinkClicks(
      string issueId,
      string search,
      string provider);

    /// <summary>
    /// Gets the total number of clicks for each link in an issue. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    [WebHelp(Comment = "Gets the total number of clicks for each link in an issue.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/totallinkclicks/{issueId}/?search={search}&provider={provider}")]
    IEnumerable<KeyValuePair<string, int>> GetIssueTotalLinkClicksXml(
      string issueId,
      string search,
      string provider);
  }
}
