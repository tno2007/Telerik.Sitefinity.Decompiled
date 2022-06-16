// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ICampaignService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
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
  public interface ICampaignService
  {
    /// <summary>
    /// Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created.
    /// The saved campaign is returned in JSON format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be saved.</param>
    /// <param name="campaign">The view model of the campaign object.</param>
    /// <param name="provider">The provider through which the campaign ought to be saved.</param>
    /// <returns>The saved campaign.</returns>
    [WebHelp(Comment = "Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created. The saved campaign is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{campaignId}/?provider={provider}")]
    [OperationContract]
    CampaignViewModel SaveCampaign(
      string campaignId,
      CampaignViewModel campaign,
      string provider);

    /// <summary>
    /// Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created.
    /// The saved campaign is returned in XML format.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="campaign">The view model of the campaign object.</param>
    /// <param name="provider">The provider through which the campaign ought to be saved.</param>
    /// <returns>The saved campaign.</returns>
    [WebHelp(Comment = "Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created. The saved campaign is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{campaignId}/?provider={provider}")]
    [OperationContract]
    CampaignViewModel SaveCampaignInXml(
      string campaignId,
      CampaignViewModel campaign,
      string provider);

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
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/obsolete/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<CampaignViewModel> GetCampaigns(
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
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/obsolete/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<CampaignViewModel> GetCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all root campaigns of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all root campaigns of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<CampaignGridViewModel> GetRootCampaigns(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all root campaigns of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all root campaigns of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<CampaignGridViewModel> GetRootCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Deletes the campaign by id and returns true if the campaign has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the campaign for given provider and supplied id and type. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{campaignId}/?provider={provider}")]
    [OperationContract]
    bool DeleteCampaign(string campaignId, string provider);

    /// <summary>
    /// Deletes the campaign by id and returns true if the campaign has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the campaign for given provider and supplied id and type. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{campaignId}/?provider={provider}")]
    [OperationContract]
    bool DeleteCampaignInXml(string campaignId, string provider);

    /// <summary>
    /// Deletes a collection of campaigns. Result is returned in JSON format.
    /// </summary>
    /// <param name="campaignIds">An array of the ids of the campaigns to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all campaigns have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple campaigns.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteCampaigns(string[] campaignIds, string provider);

    /// <summary>
    /// Deletes a collection of campaigns. Result is returned in XML format.
    /// </summary>
    /// <param name="campaignIds">An array of the ids of the campaigns to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all campaigns have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple campaigns.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteCampaignsInXml(string[] campaignIds, string provider);

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="campaignId">Id of the campaign that ought to be tested.</param>
    /// <param name="provider">The name of the provider.</param>
    /// <param name="testEmailAddresses">An array of email addresses to which the test message ought to be sent.</param>
    [WebHelp(Comment = "Sends a test message for a given campaign to the specified email addresses.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sendtest/{campaignId}/?provider={provider}")]
    [OperationContract]
    [Obsolete("Use SendTestCampaign(string provider, CampaignViewModel campaign, string[] testEmailAddresses).")]
    void SendTestMessage(string campaignId, string provider, string[] testEmailAddresses);

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="provider">The name of the provider.</param>
    /// <param name="campaign">The campaign that ought to be tested.</param>
    /// <param name="testEmailAddresses">An array of email addresses to which the test message ought to be sent.</param>
    [WebHelp(Comment = "Sends a test message for a given campaign to the specified email addresses.")]
    [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sendtest/?provider={provider}")]
    [OperationContract]
    void SendTestCampaign(string provider, CampaignViewModel campaign, string[] testEmailAddresses);

    /// <summary>Sends the campaign.</summary>
    /// <param name="campaignId">Id of the campaign that ought to be sent.</param>
    /// <param name="provider">The name of the provider.</param>
    [WebHelp(Comment = "Sends the campaign.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/send/?provider={provider}")]
    [OperationContract]
    void SendCampaign(string campaignId, string provider);

    /// <summary>
    /// Gets the merge tags for the given mailing list; including the dynamic lists.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list.</param>
    /// <returns>A dictionary of strings representing title and value of the merge tag.</returns>
    [WebHelp(Comment = "Gets the merge tags for the given campaign.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mergetags/{mailingListId}/?provider={provider}")]
    [OperationContract]
    CollectionContext<MergeTagViewModel> GetMergeTags(
      string mailingListId,
      string provider);

    /// <summary>
    /// Gets the merge tags for the given mailing list; including the dynamic lists.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list.</param>
    /// <returns>A dictionary of strings representing title and value of the merge tag.</returns>
    [WebHelp(Comment = "Gets the merge tags for the given campaign.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/mergetags/{mailingListId}/?provider={provider}")]
    [OperationContract]
    CollectionContext<MergeTagViewModel> GetMergeTagsInXml(
      string mailingListId,
      string provider);

    /// <summary>
    /// Schedules the campaign to be delivered on the given date and time.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be scheduled.</param>
    /// <param name="deliveryDate">Date at which the campaign ought to be delivered.</param>
    /// <param name="provider">The name of the Newsletters data provider to be used to perform the operation.</param>
    [WebHelp(Comment = "Schedules the campaign to be delivered on the given date and time.")]
    [WebInvoke(Method = "PUT", UriTemplate = "/schedule/{campaignId}/?provider={provider}")]
    [OperationContract]
    void ScheduleCampaign(string campaignId, DateTime deliveryDate, string provider);

    /// <summary>
    /// Gets a message body with generated raw message source.
    /// </summary>
    /// <param name="messageBody">The message body.</param>
    /// <returns>Message body with generated raw message source.</returns>
    [WebHelp(Comment = "Gets a message body with generated raw message source.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/rawmessage/")]
    [OperationContract]
    MessageBodyViewModel GetRawMessage(MessageBodyViewModel messageBody);

    /// <summary>Gets the campign and returns it in JSON format.</summary>
    /// <param name="campaignId">Id of the campign that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the campign.</param>
    /// <returns>An instance of CampaignViewModel.</returns>
    [WebHelp(Comment = "Gets the campign and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{campaignId}/?provider={provider}")]
    [OperationContract]
    CampaignViewModel GetCampaign(string campaignId, string provider);

    /// <summary>Gets the campign and returns it in XML format.</summary>
    /// <param name="campaignId">Id of the campign that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the campign.</param>
    /// <returns>An instance of CampaignViewModel.</returns>
    [WebHelp(Comment = "Gets the campign and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{campaignId}/?provider={provider}")]
    [OperationContract]
    CampaignViewModel GetCampaignInXml(string campaignId, string provider);

    /// <summary>
    /// Saves an issue. If the issue with specified id exists that issue will be updated; otherwise new issue will be created.
    /// The saved issue is returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be saved.</param>
    /// <param name="issue">The view model of the issue object.</param>
    /// <param name="provider">The provider through which the issue ought to be saved.</param>
    /// <returns>The saved issue.</returns>
    [WebHelp(Comment = "Saves an issue. If the issue with specified id exists that issue will be updated; otherwise new issue will be created. The saved issue is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    IssueViewModel SaveIssue(string issueId, IssueViewModel issue, string provider);

    /// <summary>
    /// Saves an issue. If the issue with specified id exists that issue will be updated; otherwise new issue will be created.
    /// The saved issue is returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be saved.</param>
    /// <param name="issue">The view model of the issue object.</param>
    /// <param name="provider">The provider through which the issue ought to be saved.</param>
    /// <returns>The saved issue.</returns>
    [WebHelp(Comment = "Saves an issue. If the issue with specified id exists that campaign will be updated; otherwise new issue will be created. The saved issue is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    IssueViewModel SaveIssueInXml(
      string issueId,
      IssueViewModel issue,
      string provider);

    /// <summary>
    /// Deletes the issue by id and returns true if the issue has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the issue for given provider and supplied id and type. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    bool DeleteIssue(string issueId, string provider);

    /// <summary>
    /// Deletes the issue by id and returns true if the issue has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the issue for given provider and supplied id and type. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    bool DeleteIssueInXml(string issueId, string provider);

    /// <summary>
    /// Deletes a collection of issues. Result is returned in JSON format.
    /// </summary>
    /// <param name="issueIds">An array of the ids of the issues to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all issues have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple issues.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issue/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteIssues(string[] issueIds, string provider);

    /// <summary>
    /// Deletes a collection of issues. Result is returned in XML format.
    /// </summary>
    /// <param name="issueIds">An array of the ids of the issues to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all issues have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple issues.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issue/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteIssuesInXml(string[] issueIds, string provider);

    /// <summary>Gets the issue and returns it in JSON format.</summary>
    /// <param name="issueId">Id of the issue that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the issue.</param>
    /// <returns>An instance of IssueViewModel.</returns>
    [WebHelp(Comment = "Gets the issue and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    IssueViewModel GetIssue(string issueId, string provider);

    /// <summary>Gets the issue and returns it in XML format.</summary>
    /// <param name="issueId">Id of the issue that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the issue.</param>
    /// <returns>An instance of IssueViewModel.</returns>
    [WebHelp(Comment = "Gets the issue and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issue/{issueId}/?provider={provider}")]
    [OperationContract]
    IssueViewModel GetIssueInXml(string issueId, string provider);

    /// <summary>
    /// Gets all issues of the newsletter module for a specific campaign the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="rootCampaignId">ID of the root campaign for which the issues are to be retrieved.</param>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all issues of the newsletter module for a specific campaign the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issues/{rootCampaignId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<IssueGridViewModel> GetCampaignIssues(
      string rootCampaignId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all issues of the newsletter module for a specific campaign the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="rootCampaignId">ID of the root campaign for which the issues are to be retrieved.</param>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all issues of the newsletter module for a specific campaign the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issues/{rootCampaignId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<IssueGridViewModel> GetCampaignIssuesInXml(
      string rootCampaignId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all issues of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all issues of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/issues/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<IssueGridViewModel> GetIssues(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all issues of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all issues of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/issues/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<IssueGridViewModel> GetIssuesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);
  }
}
