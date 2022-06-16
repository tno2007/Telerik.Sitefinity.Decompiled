// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IABCampaignService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Interface that defines the members of the service working with A/B campaigns.
  /// </summary>
  [ServiceContract]
  public interface IABCampaignService
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
    ABCampaignViewModel SaveCampaign(
      string campaignId,
      ABCampaignViewModel campaign,
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
    ABCampaignViewModel SaveCampaignInXml(
      string campaignId,
      ABCampaignViewModel campaign,
      string provider);

    /// <summary>
    /// Gets all A/B campaigns of the newsletter module for the given provider. The results are returned in JSON format.
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
    CollectionContext<ABCampaignViewModel> GetCampaigns(
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
    CollectionContext<ABCampaignViewModel> GetCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/abtests/{rootCampaignId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ABCampaignGridViewModel> GetABTestsPerCampaign(
      string provider,
      string rootCampaignId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/abtests/{rootCampaignId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ABCampaignGridViewModel> GetABTestsPerCampaignXml(
      string provider,
      string rootCampaignId,
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

    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/starttesting/{abCampaignId}/?provider={provider}")]
    [OperationContract]
    bool StartTesting(string abCampaignId, string provider);

    /// <summary>Ends the testing.</summary>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Winning campaign name</returns>
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/endtesting/{abCampaignId}/?provider={provider}")]
    [OperationContract]
    string EndTesting(string abCampaignId, string provider);

    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/decidewinner/{abCampaignId}/{winningCampaignId}/?provider={provider}")]
    [OperationContract]
    bool DecideWinner(string abCampaignId, string winningCampaignId, string provider);

    /// <summary>Gets the A/B test and returns it in JSON format.</summary>
    /// <param name="campaignId">Id of the A/B test that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the A/B test.</param>
    /// <returns>An instance of ABCampaignViewModel.</returns>
    [WebHelp(Comment = "Gets the A/B test and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{campaignId}/?provider={provider}")]
    [OperationContract]
    ABCampaignViewModel GetCampaign(string campaignId, string provider);

    /// <summary>Gets the A/B test and returns it in XML format.</summary>
    /// <param name="campaignId">Id of the A/B test that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the A/B test.</param>
    /// <returns>An instance of ABCampaignViewModel.</returns>
    [WebHelp(Comment = "Gets the A/B test and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{campaignId}/?provider={provider}")]
    [OperationContract]
    ABCampaignViewModel GetCampaignInXml(string campaignId, string provider);

    /// <summary>
    /// Creates an A/B test and creates a B version of the given issue. The result is returned in JSON format.
    /// </summary>
    /// <param name="issueAId">The issue A id.</param>
    /// <param name="isFromScratch">if set to <c>true</c> issue B message will be copied from the root campaign, otherwise it will be copied from issue A.</param>
    /// <param name="issueB">The issue B.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The newly created AB test.</returns>
    [WebHelp(Comment = "Creates an A/B test and creates a B version of the given issue. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/abtests/{issueAId}/?provider={provider}&isFromScratch={isFromScratch}")]
    [OperationContract]
    ABCampaignViewModel CreateAbTest(
      string issueAId,
      bool isFromScratch,
      IssueViewModel issueB,
      string provider);

    /// <summary>
    /// Creates an A/B test and creates a B version of the given issue. The result is returned in XML format.
    /// </summary>
    /// <param name="issueAId">The issue A id.</param>
    /// <param name="isFromScratch">if set to <c>true</c> issue B message will be copied from the root campaign, otherwise it will be copied from issue A.</param>
    /// <param name="issueB">The issue B.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The newly created AB test.</returns>
    [WebHelp(Comment = "Creates an A/B test and creates a B version of the given issue. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/abtests/{issueAId}/?provider={provider}&isFromScratch={isFromScratch}")]
    [OperationContract]
    ABCampaignViewModel CreateAbTestXml(
      string issueAId,
      bool isFromScratch,
      IssueViewModel issueB,
      string provider);

    /// <summary>
    /// Sets the TestingNote field of an A/B test. The result is returned in JSON format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the TestingNote field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    [WebHelp(Comment = "Sets the TestingNote field of an A/B test. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/settestingnote/{abTestId}/?provider={provider}")]
    [OperationContract]
    bool SetTestingNote(string abTestId, string value, string provider);

    /// <summary>
    /// Sets the TestingNote field of an A/B test. The result is returned in XML format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the TestingNote field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    [WebHelp(Comment = "Sets the TestingNote field of an A/B test. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/settestingnote/{abTestId}/?provider={provider}")]
    [OperationContract]
    bool SetTestingNoteXml(string abTestId, string value, string provider);

    /// <summary>
    /// Sets the Conclusion field of an A/B test. The result is returned in JSON format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the Conclusion field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    [WebHelp(Comment = "Sets the Conclusion field of an A/B test. The result is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/setconclusion/{abTestId}/?provider={provider}")]
    [OperationContract]
    bool SetConclusion(string abTestId, string value, string provider);

    /// <summary>
    /// Sets the Conclusion field of an A/B test. The result is returned in XML format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the Conclusion field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    [WebHelp(Comment = "Sets the Conclusion field of an A/B test. The result is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/setconclusion/{abTestId}/?provider={provider}")]
    [OperationContract]
    bool SetConclusionXml(string abTestId, string value, string provider);
  }
}
