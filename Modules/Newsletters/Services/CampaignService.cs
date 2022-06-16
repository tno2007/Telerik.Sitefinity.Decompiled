// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.CampaignService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Service that provides method for working with all supported types of the campaings in the newsletter module.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class CampaignService : ICampaignService
  {
    /// <summary>
    /// Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created.
    /// The saved campaign is returned in JSON format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be saved.</param>
    /// <param name="campaign">The view model of the campaign object.</param>
    /// <param name="provider">The provider through which the campaign ought to be saved.</param>
    /// <returns>The saved campaign.</returns>
    public CampaignViewModel SaveCampaign(
      string campaignId,
      CampaignViewModel campaign,
      string provider)
    {
      CampaignService.DemandPermissions();
      return this.SaveCampaignInternal(campaignId, campaign, provider);
    }

    /// <summary>
    /// Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created.
    /// The saved campaign is returned in XML format.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="campaign">The view model of the campaign object.</param>
    /// <param name="provider">The provider through which the campaign ought to be saved.</param>
    /// <returns>The saved campaign.</returns>
    public CampaignViewModel SaveCampaignInXml(
      string campaignId,
      CampaignViewModel campaign,
      string provider)
    {
      CampaignService.DemandPermissions();
      return this.SaveCampaignInternal(campaignId, campaign, provider);
    }

    /// <summary>
    /// Gets all campaigns of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> objects.
    /// </returns>
    [Obsolete("Use GetRootCampaigns instead.")]
    public CollectionContext<CampaignViewModel> GetCampaigns(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetCampaignsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all campaigns of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> objects.
    /// </returns>
    [Obsolete("Use GetRootCampaignsInXml instead.")]
    public CollectionContext<CampaignViewModel> GetCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetCampaignsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all root campaigns of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<CampaignGridViewModel> GetRootCampaigns(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetRootCampaignsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all root campaigns of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<CampaignGridViewModel> GetRootCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetRootCampaignsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Deletes the campaign by id and returns true if the campaign has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteCampaign(string campaignId, string provider)
    {
      CampaignService.DemandPermissions();
      return this.DeleteCampaignInternal(campaignId, provider);
    }

    /// <summary>
    /// Deletes the campaign by id and returns true if the campaign has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteCampaignInXml(string campaignId, string provider)
    {
      CampaignService.DemandPermissions();
      return this.DeleteCampaignInternal(campaignId, provider);
    }

    /// <summary>
    /// Deletes a collection of campaigns. Result is returned in JSON format.
    /// </summary>
    /// <param name="campaignIds">An array of the ids of the campaigns to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all campaigns have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteCampaigns(string[] campaignIds, string provider)
    {
      CampaignService.DemandPermissions();
      return this.BatchDeleteCampaignsInternal(campaignIds, provider);
    }

    /// <summary>
    /// Deletes a collection of campaigns. Result is returned in XML format.
    /// </summary>
    /// <param name="campaignIds">An array of the ids of the campaigns to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all campaigns have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteCampaignsInXml(string[] campaignIds, string provider)
    {
      CampaignService.DemandPermissions();
      return this.BatchDeleteCampaignsInternal(campaignIds, provider);
    }

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="campaignId">Id of the campaign that ought to be tested.</param>
    /// <param name="provider">The name of the provider.</param>
    /// <param name="testEmailAddresses">An array of email addresses to which the test message ought to be sent.</param>
    [Obsolete("Use SendTestCampaign(string provider, CampaignViewModel campaign, string[] testEmailAddresses).")]
    public void SendTestMessage(string campaignId, string provider, string[] testEmailAddresses)
    {
      CampaignService.DemandPermissions();
      this.SendTestMessageObsoleteInternal(campaignId, provider, testEmailAddresses);
    }

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="provider">The name of the provider.</param>
    /// <param name="campaign">The campaign that ought to be tested.</param>
    /// <param name="testEmailAddresses">An array of email addresses to which the test message ought to be sent.</param>
    public void SendTestCampaign(
      string provider,
      CampaignViewModel campaign,
      string[] testEmailAddresses)
    {
      CampaignService.DemandPermissions();
      this.SendTestMessageInternal(provider, campaign, testEmailAddresses);
    }

    /// <summary>Sends the campaign.</summary>
    /// <param name="campaignId">Id of the campaign that ought to be sent.</param>
    /// <param name="provider">The name of the provider.</param>
    public void SendCampaign(string campaignId, string provider)
    {
      CampaignService.DemandPermissions();
      this.SendCampaignInternal(campaignId, provider);
    }

    /// <summary>
    /// Gets the merge tags for the given mailing list; including the dynamic lists.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list.</param>
    /// <returns>A dictionary of strings representing title and value of the merge tag.</returns>
    public CollectionContext<MergeTagViewModel> GetMergeTags(
      string mailingListId,
      string providerName)
    {
      CampaignService.DemandPermissions();
      return this.GetMergeTagsInternal(mailingListId, providerName);
    }

    /// <summary>
    /// Gets the merge tags for the given mailing list; including the dynamic lists.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list.</param>
    /// <returns>A dictionary of strings representing title and value of the merge tag.</returns>
    public CollectionContext<MergeTagViewModel> GetMergeTagsInXml(
      string mailingListId,
      string providerName)
    {
      CampaignService.DemandPermissions();
      return this.GetMergeTagsInternal(mailingListId, providerName);
    }

    /// <summary>
    /// Schedules the campaign to be delivered on the given date and time.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be scheduled.</param>
    /// <param name="deliveryDate">Date at which the campaign ought to be delivered.</param>
    public void ScheduleCampaign(string campaignId, DateTime deliveryDate, string providerName)
    {
      CampaignService.DemandPermissions();
      NewslettersManager manager = NewslettersManager.GetManager(providerName);
      manager.ScheduleCampaign(new Guid(campaignId), deliveryDate);
      manager.SaveChanges();
    }

    /// <summary>
    /// Gets a message body with generated raw message source.
    /// </summary>
    /// <param name="messageBody">The message body.</param>
    /// <returns>Message body with generated raw message source.</returns>
    public MessageBodyViewModel GetRawMessage(MessageBodyViewModel messageBody)
    {
      CampaignService.DemandPermissions();
      MessageBody messageBody1 = new MessageBody();
      Synchronizer.Synchronize(messageBody, messageBody1);
      Synchronizer.Synchronize(messageBody1, messageBody);
      return messageBody;
    }

    /// <summary>Gets the campign and returns it in JSON format.</summary>
    /// <param name="campaignId">Id of the campign that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the campign.</param>
    /// <returns>An instance of CampaignViewModel.</returns>
    public CampaignViewModel GetCampaign(string campaignId, string provider) => this.GetCampaignInternal(campaignId, provider);

    /// <summary>Gets the campign and returns it in XML format.</summary>
    /// <param name="campaignId">Id of the campign that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the campign.</param>
    /// <returns>An instance of CampaignViewModel.</returns>
    public CampaignViewModel GetCampaignInXml(string campaignId, string provider) => this.GetCampaignInternal(campaignId, provider);

    /// <summary>
    /// Saves an issue. If the issue with specified id exists that issue will be updated; otherwise new issue will be created.
    /// The saved issue is returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be saved.</param>
    /// <param name="issue">The view model of the issue object.</param>
    /// <param name="provider">The provider through which the issue ought to be saved.</param>
    /// <returns>The saved issue.</returns>
    public IssueViewModel SaveIssue(
      string issueId,
      IssueViewModel issue,
      string provider)
    {
      CampaignService.DemandPermissions();
      return (IssueViewModel) this.SaveCampaignInternal(issueId, (CampaignViewModel) issue, provider);
    }

    /// <summary>
    /// Saves an issue. If the issue with specified id exists that issue will be updated; otherwise new issue will be created.
    /// The saved issue is returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be saved.</param>
    /// <param name="issue">The view model of the issue object.</param>
    /// <param name="provider">The provider through which the issue ought to be saved.</param>
    /// <returns>The saved issue.</returns>
    public IssueViewModel SaveIssueInXml(
      string issueId,
      IssueViewModel issue,
      string provider)
    {
      CampaignService.DemandPermissions();
      return (IssueViewModel) this.SaveCampaignInternal(issueId, (CampaignViewModel) issue, provider);
    }

    /// <summary>
    /// Deletes the issue by id and returns true if the issue has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    public bool DeleteIssue(string issueId, string provider)
    {
      CampaignService.DemandPermissions();
      return this.DeleteCampaignInternal(issueId, provider);
    }

    /// <summary>
    /// Deletes the issue by id and returns true if the issue has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    public bool DeleteIssueInXml(string issueId, string provider)
    {
      CampaignService.DemandPermissions();
      return this.DeleteCampaignInternal(issueId, provider);
    }

    /// <summary>
    /// Deletes a collection of issues. Result is returned in JSON format.
    /// </summary>
    /// <param name="issueIds">An array of the ids of the issues to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all issues have been deleted; otherwise false.</returns>
    public bool BatchDeleteIssues(string[] issueIds, string provider)
    {
      CampaignService.DemandPermissions();
      return this.BatchDeleteCampaignsInternal(issueIds, provider);
    }

    /// <summary>
    /// Deletes a collection of issues. Result is returned in XML format.
    /// </summary>
    /// <param name="issueIds">An array of the ids of the issues to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all issues have been deleted; otherwise false.</returns>
    public bool BatchDeleteIssuesInXml(string[] issueIds, string provider)
    {
      CampaignService.DemandPermissions();
      return this.BatchDeleteCampaignsInternal(issueIds, provider);
    }

    /// <summary>Gets the issue and returns it in JSON format.</summary>
    /// <param name="issueId">Id of the issue that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the issue.</param>
    /// <returns>An instance of IssueViewModel.</returns>
    public IssueViewModel GetIssue(string issueId, string provider) => this.GetIssueInternal(issueId, provider);

    /// <summary>Gets the issue and returns it in XML format.</summary>
    /// <param name="issueId">Id of the issue that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the issue.</param>
    /// <returns>An instance of IssueViewModel.</returns>
    public IssueViewModel GetIssueInXml(string issueId, string provider) => this.GetIssueInternal(issueId, provider);

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
    public CollectionContext<IssueGridViewModel> GetCampaignIssues(
      string rootCampaignId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetIssuesInternal(rootCampaignId, provider, sortExpression, skip, take, filter);
    }

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
    public CollectionContext<IssueGridViewModel> GetCampaignIssuesInXml(
      string rootCampaignId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetIssuesInternal(rootCampaignId, provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all issues of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<IssueGridViewModel> GetIssues(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetIssuesInternal(Guid.Empty.ToString(), provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all issues of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the issues ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the issues.</param>
    /// <param name="skip">Number of issues to skip in result set. (used for paging)</param>
    /// <param name="take">Number of issues to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<IssueGridViewModel> GetIssuesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      CampaignService.DemandPermissions();
      return this.GetIssuesInternal(Guid.Empty.ToString(), provider, sortExpression, skip, take, filter);
    }

    private static void DemandPermissions() => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity =>
    {
      if (!identity.IsBackendUser)
        return true;
      return !AppPermission.IsGranted(AppAction.ManageNewsletters);
    }));

    private CampaignViewModel SaveCampaignInternal(
      string campaignId,
      CampaignViewModel campaign,
      string provider)
    {
      Guid campaignId1 = new Guid(campaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Campaign modelCampaign = (Campaign) null;
      if (campaignId1 == Guid.Empty)
      {
        modelCampaign = manager.CreateCampaign(true);
      }
      else
      {
        modelCampaign = manager.GetCampaign(campaignId1);
        if (modelCampaign.CampaignState == CampaignState.Sending || modelCampaign.CampaignState == CampaignState.Completed)
          throw new InvalidOperationException(Res.Get<NewslettersResources>().IssueAlreadySent);
      }
      if (modelCampaign.Name != campaign.Name || modelCampaign.List != null && modelCampaign.List.Id != campaign.ListId)
      {
        Campaign campaign1 = manager.GetABCampaigns().Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => t.CampaignA.Id == modelCampaign.Id)).Select<ABCampaign, Campaign>((Expression<Func<ABCampaign, Campaign>>) (t => t.CampaignB)).FirstOrDefault<Campaign>();
        if (campaign1 != null)
        {
          campaign1.Name = campaign.Name;
          campaign1.List = !(campaign.ListId != Guid.Empty) ? (MailingList) null : manager.GetMailingList(campaign.ListId);
        }
      }
      if (campaign.DeliveryDate == DateTime.MinValue)
        campaign.DeliveryDate = modelCampaign.DeliveryDate;
      Synchronizer.Synchronize(campaign, modelCampaign, manager);
      modelCampaign.MessageBody.Name = modelCampaign.Name;
      if (modelCampaign.CampaignState == CampaignState.Draft && campaign.IsReadyForSending)
        modelCampaign.CampaignState = CampaignState.PendingSending;
      if (campaign.ListId != Guid.Empty)
      {
        MailingList mailingList = manager.GetMailingList(campaign.ListId);
        modelCampaign.List = mailingList;
      }
      else
        modelCampaign.List = (MailingList) null;
      if (modelCampaign.CampaignState != CampaignState.Scheduled)
        manager.UnscheduleCampaign(modelCampaign.Id);
      modelCampaign.LastModified = DateTime.UtcNow;
      manager.SaveChanges();
      Synchronizer.Synchronize(modelCampaign, campaign, manager);
      ServiceUtility.DisableCache();
      return campaign;
    }

    private IQueryable<Campaign> GetCampaigns(
      string sortExpression,
      int skip,
      int take,
      string filter,
      NewslettersManager manager,
      out int totalCount)
    {
      IQueryable<Campaign> source = manager.GetCampaigns();
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<Campaign>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<Campaign>(filter);
      totalCount = source.Count<Campaign>();
      if (skip > 0)
        source = source.Skip<Campaign>(skip);
      if (take > 0)
        source = source.Take<Campaign>(take);
      return source;
    }

    private CollectionContext<CampaignViewModel> GetCampaignsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      NewslettersManager manager1 = NewslettersManager.GetManager(provider);
      int totalCount;
      IQueryable<Campaign> campaigns = this.GetCampaigns(sortExpression, skip, take, filter, manager1, out totalCount);
      List<CampaignViewModel> items = new List<CampaignViewModel>();
      foreach (Campaign source in (IEnumerable<Campaign>) campaigns)
      {
        CampaignViewModel campaignViewModel = new CampaignViewModel();
        CampaignViewModel target = campaignViewModel;
        NewslettersManager manager2 = manager1;
        Synchronizer.Synchronize(source, target, manager2);
        items.Add(campaignViewModel);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<CampaignViewModel>((IEnumerable<CampaignViewModel>) items)
      {
        TotalCount = totalCount
      };
    }

    private CollectionContext<IssueGridViewModel> GetIssuesInternal(
      string rootCampaignId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guid result;
      if (!Guid.TryParse(rootCampaignId, out result))
        throw new ArgumentException(string.Format("Invalid root campaign Id {0}", (object) rootCampaignId));
      NewslettersManager manager1 = NewslettersManager.GetManager(provider);
      IQueryable<Campaign> source1 = !(result != Guid.Empty) ? manager1.GetIssues() : manager1.GetIssues(result);
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<Campaign>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<Campaign>(filter);
      int num = source1.Count<Campaign>();
      if (skip > 0)
        source1 = source1.Skip<Campaign>(skip);
      if (take > 0)
        source1 = source1.Take<Campaign>(take);
      List<IssueGridViewModel> items = new List<IssueGridViewModel>();
      foreach (Campaign source2 in (IEnumerable<Campaign>) source1)
      {
        IssueGridViewModel issueGridViewModel = new IssueGridViewModel();
        IssueGridViewModel target = issueGridViewModel;
        NewslettersManager manager2 = manager1;
        Synchronizer.Synchronize(source2, target, manager2);
        items.Add(issueGridViewModel);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<IssueGridViewModel>((IEnumerable<IssueGridViewModel>) items)
      {
        TotalCount = num
      };
    }

    private CollectionContext<CampaignGridViewModel> GetRootCampaignsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      NewslettersManager manager1 = NewslettersManager.GetManager(provider);
      int totalCount;
      IQueryable<Campaign> campaigns = this.GetCampaigns(sortExpression, skip, take, filter, manager1, out totalCount);
      List<CampaignGridViewModel> items = new List<CampaignGridViewModel>();
      foreach (Campaign source in (IEnumerable<Campaign>) campaigns)
      {
        CampaignGridViewModel campaignGridViewModel = new CampaignGridViewModel();
        CampaignGridViewModel target = campaignGridViewModel;
        NewslettersManager manager2 = manager1;
        Synchronizer.Synchronize(source, target, manager2);
        items.Add(campaignGridViewModel);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<CampaignGridViewModel>((IEnumerable<CampaignGridViewModel>) items)
      {
        TotalCount = totalCount
      };
    }

    private bool DeleteCampaignInternal(string campaignId, string provider)
    {
      Guid campaignId1 = new Guid(campaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.DeleteCampaign(campaignId1);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool BatchDeleteCampaignsInternal(string[] campaignIds, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      foreach (string campaignId1 in campaignIds)
      {
        Guid campaignId = new Guid(campaignId1);
        Campaign campaign = manager.GetCampaigns().Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.Id == campaignId)).SingleOrDefault<Campaign>();
        manager.DeleteCampaign(campaign);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private void SendTestMessageObsoleteInternal(
      string campaignId,
      string provider,
      string[] testEmailAddresses)
    {
      Guid campaignId1 = new Guid(campaignId);
      NewslettersManager.GetManager(provider).SendTestMessageForCampaign(campaignId1, testEmailAddresses);
    }

    private void SendTestMessageInternal(
      string provider,
      CampaignViewModel campaign,
      string[] testEmailAddresses)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Campaign campaign1 = manager.GetCampaign(campaign.Id);
      Synchronizer.Synchronize(campaign, campaign1, manager);
      manager.SendTestMessageForCampaign(campaign1, testEmailAddresses);
      manager.CancelChanges();
    }

    private void SendCampaignInternal(string campaignId, string provider)
    {
      Guid guid = new Guid(campaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.UnscheduleCampaign(guid);
      bool tooManySubscribers = false;
      manager.SendIssue(guid, out tooManySubscribers);
      if (tooManySubscribers)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "You have more subscribers than your license permits. Please remove some of the subscribers in order to send campaigns.", (Exception) null);
    }

    private CollectionContext<MergeTagViewModel> GetMergeTagsInternal(
      string mailingListId,
      string providerName)
    {
      Guid id = new Guid(mailingListId);
      NewslettersManager manager = NewslettersManager.GetManager(providerName);
      List<MergeTagViewModel> items = new List<MergeTagViewModel>();
      foreach (MergeTag mergeTag in (IEnumerable<MergeTag>) manager.GetMergeTags(manager.GetMailingList(id)))
      {
        MergeTagViewModel mergeTagViewModel = new MergeTagViewModel();
        MergeTagViewModel target = mergeTagViewModel;
        Synchronizer.Synchronize(mergeTag, target);
        items.Add(mergeTagViewModel);
      }
      return new CollectionContext<MergeTagViewModel>((IEnumerable<MergeTagViewModel>) items)
      {
        TotalCount = items.Count
      };
    }

    private CampaignViewModel GetCampaignInternal(
      string campaignId,
      string provider)
    {
      CampaignService.DemandPermissions();
      NewslettersManager manager1 = NewslettersManager.GetManager(provider);
      Campaign campaign = manager1.GetCampaign(Telerik.Sitefinity.Utilities.Utility.StringToGuid(campaignId));
      CampaignViewModel campaignInternal = new CampaignViewModel();
      CampaignViewModel target = campaignInternal;
      NewslettersManager manager2 = manager1;
      Synchronizer.Synchronize(campaign, target, manager2);
      ServiceUtility.DisableCache();
      return campaignInternal;
    }

    private IssueViewModel GetIssueInternal(string issueId, string provider)
    {
      CampaignService.DemandPermissions();
      NewslettersManager manager1 = NewslettersManager.GetManager(provider);
      Campaign campaign = manager1.GetCampaign(Telerik.Sitefinity.Utilities.Utility.StringToGuid(issueId));
      IssueViewModel issueInternal = new IssueViewModel();
      IssueViewModel target = issueInternal;
      NewslettersManager manager2 = manager1;
      Synchronizer.Synchronize(campaign, (CampaignViewModel) target, manager2);
      ServiceUtility.DisableCache();
      return issueInternal;
    }
  }
}
