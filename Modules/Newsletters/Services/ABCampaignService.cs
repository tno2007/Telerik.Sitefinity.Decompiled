// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ABCampaignService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Service that provides method for working with A/B campaigns.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ABCampaignService : IABCampaignService
  {
    /// <summary>
    /// Saves a campaign. If the campaign with specified id exists that campaign will be updated; otherwise new campaign will be created.
    /// The saved campaign is returned in JSON format.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be saved.</param>
    /// <param name="campaign">The view model of the campaign object.</param>
    /// <param name="provider">The provider through which the campaign ought to be saved.</param>
    /// <returns>The saved campaign.</returns>
    public ABCampaignViewModel SaveCampaign(
      string campaignId,
      ABCampaignViewModel campaign,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
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
    public ABCampaignViewModel SaveCampaignInXml(
      string campaignId,
      ABCampaignViewModel campaign,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
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
    public CollectionContext<ABCampaignViewModel> GetCampaigns(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
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
    public CollectionContext<ABCampaignViewModel> GetCampaignsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetCampaignsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<ABCampaignGridViewModel> GetABTestsPerCampaign(
      string provider,
      string rootCampaignId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetABTestsPerCampaignInternal(provider, rootCampaignId, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all AB tests of the newsletter module for the given provider and root campaign id. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <param name="sortExpression">Sort expression used to order the campaigns.</param>
    /// <param name="skip">Number of campaigns to skip in result set. (used for paging)</param>
    /// <param name="take">Number of campaigns to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignGridViewModel" /> objects.
    /// </returns>
    public CollectionContext<ABCampaignGridViewModel> GetABTestsPerCampaignXml(
      string provider,
      string rootCampaignId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetABTestsPerCampaignInternal(provider, rootCampaignId, sortExpression, skip, take, filter);
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
      ServiceUtility.RequestBackendUserAuthentication();
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
      ServiceUtility.RequestBackendUserAuthentication();
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
      ServiceUtility.RequestBackendUserAuthentication();
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
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteCampaignsInternal(campaignIds, provider);
    }

    /// <summary>Starts ab testing.</summary>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public bool StartTesting(string abCampaignId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.StartTesting(new Guid(abCampaignId));
      manager.SaveChanges();
      return true;
    }

    /// <summary>Ends the testing.</summary>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Winning campaign name</returns>
    public string EndTesting(string abCampaignId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      string str = manager.EndTesting(new Guid(abCampaignId));
      manager.SaveChanges();
      return str;
    }

    /// <summary>Decides ab test winner.</summary>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <param name="winningCampaignId">The winning campaign id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public bool DecideWinner(string abCampaignId, string winningCampaignId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.DecideWinner(new Guid(abCampaignId), new Guid(winningCampaignId));
      manager.SaveChanges();
      return true;
    }

    /// <summary>Gets the A/B test and returns it in JSON format.</summary>
    /// <param name="campaignId">Id of the A/B test that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the A/B test.</param>
    /// <returns>An instance of ABCampaignViewModel.</returns>
    public ABCampaignViewModel GetCampaign(string campaignId, string provider) => this.GetCampaignInternal(campaignId, provider);

    /// <summary>Gets the A/B test and returns it in XML format.</summary>
    /// <param name="campaignId">Id of the A/B test that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the A/B test.</param>
    /// <returns>An instance of ABCampaignViewModel.</returns>
    public ABCampaignViewModel GetCampaignInXml(
      string campaignId,
      string provider)
    {
      return this.GetCampaignInternal(campaignId, provider);
    }

    /// <summary>
    /// Creates an A/B test and creates a B version of the given issue. The result is returned in JSON format.
    /// </summary>
    /// <param name="issueAId">The issue A id.</param>
    /// <param name="isFromScratch">if set to <c>true</c> issue B message will be copied from the root campaign, otherwise it will be copied from issue A.</param>
    /// <param name="issueB">The issue B.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The newly created AB test.</returns>
    public ABCampaignViewModel CreateAbTest(
      string issueAId,
      bool isFromScratch,
      IssueViewModel issueB,
      string provider)
    {
      return this.CreateAbTestInternal(issueAId, isFromScratch, issueB, provider);
    }

    /// <summary>
    /// Creates an A/B test and creates a B version of the given issue. The result is returned in XML format.
    /// </summary>
    /// <param name="issueAId">The issue A id.</param>
    /// <param name="isFromScratch">if set to <c>true</c> issue B message will be copied from the root campaign, otherwise it will be copied from issue A.</param>
    /// <param name="issueB">The issue B.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The newly created AB test.</returns>
    public ABCampaignViewModel CreateAbTestXml(
      string issueAId,
      bool isFromScratch,
      IssueViewModel issueB,
      string provider)
    {
      return this.CreateAbTestInternal(issueAId, isFromScratch, issueB, provider);
    }

    /// <summary>
    /// Sets the TestingNote field of an A/B test. The result is returned in JSON format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the TestingNote field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    public bool SetTestingNote(string abTestId, string value, string provider) => this.SetTestingNoteInternal(abTestId, value, provider);

    /// <summary>
    /// Sets the TestingNote field of an A/B test. The result is returned in XML format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the TestingNote field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    public bool SetTestingNoteXml(string abTestId, string value, string provider) => this.SetTestingNoteInternal(abTestId, value, provider);

    /// <summary>
    /// Sets the Conclusion field of an A/B test. The result is returned in JSON format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the Conclusion field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    public bool SetConclusion(string abTestId, string value, string provider) => this.SetConclusionInternal(abTestId, value, provider);

    /// <summary>
    /// Sets the Conclusion field of an A/B test. The result is returned in XML format.
    /// </summary>
    /// <param name="abTestId">The A/B test Id.</param>
    /// <param name="value">The new value for the Conclusion field.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Whether the operation was successful.</returns>
    public bool SetConclusionXml(string abTestId, string value, string provider) => this.SetConclusionInternal(abTestId, value, provider);

    private ABCampaignViewModel SaveCampaignInternal(
      string campaignId,
      ABCampaignViewModel campaign,
      string provider)
    {
      Guid campaignId1 = new Guid(campaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      ABCampaign abCampaign;
      if (campaignId1 == Guid.Empty)
      {
        abCampaign = manager.CreateABCampaign();
      }
      else
      {
        abCampaign = manager.GetABCampaign(campaignId1);
        if (abCampaign.ABTestingStatus == ABTestingStatus.Done || abCampaign.ABTestingStatus == ABTestingStatus.InProgress)
          throw new InvalidOperationException(Res.Get<NewslettersResources>().TestAlreadyStarted);
      }
      ABCampaignService.SetScheduledTasks(campaign, abCampaign, manager);
      this.CopyToCampaign(campaign, abCampaign, manager);
      manager.SaveChanges();
      campaign.Id = abCampaign.Id;
      ServiceUtility.DisableCache();
      return campaign;
    }

    private static void SetScheduledTasks(
      ABCampaignViewModel campaign,
      ABCampaign abTest,
      NewslettersManager newslettersManager)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      if (campaign.ScheduleDate.HasValue)
        newslettersManager.ScheduleAbTestSend(abTest.Id, campaign.ScheduleDate.Value);
      else
        newslettersManager.UnscheduleAbTestSend(abTest.Id);
      manager.SaveChanges();
    }

    private CollectionContext<ABCampaignViewModel> GetCampaignsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      IQueryable<ABCampaign> source1 = manager.GetABCampaigns();
      int num = source1.Count<ABCampaign>();
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<ABCampaign>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<ABCampaign>(filter);
      if (skip > 0)
        source1 = source1.Skip<ABCampaign>(skip);
      if (take > 0)
        source1 = source1.Take<ABCampaign>(take);
      List<ABCampaignViewModel> items = new List<ABCampaignViewModel>();
      foreach (ABCampaign source2 in (IEnumerable<ABCampaign>) source1)
        items.Add(this.CopyFromCampaign(source2, new ABCampaignViewModel(), manager));
      ServiceUtility.DisableCache();
      return new CollectionContext<ABCampaignViewModel>((IEnumerable<ABCampaignViewModel>) items)
      {
        TotalCount = num
      };
    }

    private CollectionContext<ABCampaignGridViewModel> GetABTestsPerCampaignInternal(
      string provider,
      string rootCampaignId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guid rootCampaignId1 = new Guid(rootCampaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      IQueryable<ABCampaign> source1 = manager.GetABCampaigns(rootCampaignId1);
      int num = source1.Count<ABCampaign>();
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<ABCampaign>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<ABCampaign>(filter);
      if (skip > 0)
        source1 = source1.Skip<ABCampaign>(skip);
      if (take > 0)
        source1 = source1.Take<ABCampaign>(take);
      List<ABCampaignGridViewModel> items = new List<ABCampaignGridViewModel>();
      foreach (ABCampaign source2 in (IEnumerable<ABCampaign>) source1)
        items.Add(this.CopyFromCampaign(source2, new ABCampaignGridViewModel(), manager));
      ServiceUtility.DisableCache();
      return new CollectionContext<ABCampaignGridViewModel>((IEnumerable<ABCampaignGridViewModel>) items)
      {
        TotalCount = num
      };
    }

    private bool DeleteCampaignInternal(string campaignId, string provider)
    {
      Guid campaignId1 = new Guid(campaignId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.DeleteABCampaign(campaignId1);
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
        ABCampaign campaign = manager.GetABCampaigns().Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (c => c.Id == campaignId)).SingleOrDefault<ABCampaign>();
        manager.DeleteABCampaign(campaign);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private void CopyToCampaign(
      ABCampaignViewModel source,
      ABCampaign target,
      NewslettersManager manager)
    {
      target.Name = source.Name;
      target.CampaignA = !(source.CampaignAId != Guid.Empty) ? (Campaign) null : manager.GetIssue(source.CampaignAId);
      target.CampaignB = !(source.CampaignBId != Guid.Empty) ? (Campaign) null : manager.GetIssue(source.CampaignBId);
      target.RootCampaign = !(source.RootCampaignId != Guid.Empty) ? (Campaign) null : manager.GetCampaign(source.RootCampaignId);
      target.TestingEnds = source.TestingEnds;
      target.TestingSamplePercentage = Convert.ToInt32(source.TestingSamplePercentage);
      target.WinningCondition = (CampaignWinningCondition) source.WinningCondition;
      target.TestingNote = source.TestingNote;
    }

    private ABCampaignViewModel CopyFromCampaign(
      ABCampaign source,
      ABCampaignViewModel target,
      NewslettersManager manager)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ABCampaignService.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new ABCampaignService.\u003C\u003Ec__DisplayClass28_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass280.source = source;
      // ISSUE: reference to a compiler-generated field
      target.Id = cDisplayClass280.source.Id;
      // ISSUE: reference to a compiler-generated field
      target.Name = cDisplayClass280.source.Name;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass280.source.CampaignA != null)
      {
        // ISSUE: reference to a compiler-generated field
        target.CampaignAId = cDisplayClass280.source.CampaignA.Id;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAName = cDisplayClass280.source.CampaignA.Name;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAMessageSubject = cDisplayClass280.source.CampaignA.MessageSubject;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAFromName = cDisplayClass280.source.CampaignA.FromName;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAReplyToEmail = cDisplayClass280.source.CampaignA.ReplyToEmail;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAList = (string) cDisplayClass280.source.CampaignA.List.Title;
        // ISSUE: reference to a compiler-generated field
        target.CampaignAMessageBodyType = cDisplayClass280.source.CampaignA.MessageBody.MessageBodyType;
        // ISSUE: reference to a compiler-generated field
        target.SubscribersCount = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(cDisplayClass280.source.CampaignA.List) && !sub.IsSuspended)).Count<Subscriber>().ToString();
      }
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass280.source.CampaignB != null)
      {
        // ISSUE: reference to a compiler-generated field
        target.CampaignBId = cDisplayClass280.source.CampaignB.Id;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBName = cDisplayClass280.source.CampaignB.Name;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBMessageSubject = cDisplayClass280.source.CampaignB.MessageSubject;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBFromName = cDisplayClass280.source.CampaignB.FromName;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBReplyToEmail = cDisplayClass280.source.CampaignB.ReplyToEmail;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBList = (string) cDisplayClass280.source.CampaignB.List.Title;
        // ISSUE: reference to a compiler-generated field
        target.CampaignBMessageBodyType = cDisplayClass280.source.CampaignB.MessageBody.MessageBodyType;
      }
      // ISSUE: reference to a compiler-generated field
      target.TestingEnds = cDisplayClass280.source.TestingEnds;
      // ISSUE: reference to a compiler-generated field
      target.TestingSamplePercentage = (Decimal) cDisplayClass280.source.TestingSamplePercentage;
      // ISSUE: reference to a compiler-generated field
      target.WinningCondition = (int) cDisplayClass280.source.WinningCondition;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass280.source.RootCampaign != null)
      {
        // ISSUE: reference to a compiler-generated field
        target.RootCampaignId = cDisplayClass280.source.RootCampaign.Id;
      }
      // ISSUE: reference to a compiler-generated field
      target.TestingNote = cDisplayClass280.source.TestingNote;
      SchedulingManager manager1 = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      target.ScheduleDate = manager1.GetTaskData().Where<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "AbTestSendTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass280.source.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression)).Select<ScheduledTaskData, DateTime?>((Expression<Func<ScheduledTaskData, DateTime?>>) (t => (DateTime?) t.ExecuteTime)).FirstOrDefault<DateTime?>();
      return target;
    }

    private ABCampaignGridViewModel CopyFromCampaign(
      ABCampaign source,
      ABCampaignGridViewModel target,
      NewslettersManager manager)
    {
      target.Id = source.Id;
      target.Name = source.Name != null ? source.Name : string.Empty;
      if (source.DateSent != DateTime.MinValue)
        target.DateSent = new DateTime?(source.DateSent);
      int int32 = Convert.ToInt32(Math.Floor((double) manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(source.CampaignA.List) && !sub.IsSuspended)).Count<Subscriber>() * ((double) source.TestingSamplePercentage / 100.0)));
      target.SampleUsers = string.Format("{0} ({1}%)", (object) int32, (object) source.TestingSamplePercentage);
      target.TestingNote = source.TestingNote != null ? source.TestingNote : string.Empty;
      if (source.WinnerIssue == null)
      {
        target.Winner = string.Empty;
      }
      else
      {
        string str = source.WinnerIssueOriginalId == source.CampaignA.Id ? Res.Get<NewslettersResources>().IssueA : Res.Get<NewslettersResources>().IssueB;
        target.Winner = string.Format("{0} ({1})", (object) source.WinnerIssue.Name, (object) str);
      }
      target.DateEnded = source.TestingEnds;
      target.Conclusion = source.Conclusion != null ? source.Conclusion : string.Empty;
      target.LastModified = source.LastModified;
      return target;
    }

    private ABCampaignViewModel GetCampaignInternal(
      string campaignId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      ABCampaign abCampaign = manager.GetABCampaign(Telerik.Sitefinity.Utilities.Utility.StringToGuid(campaignId));
      ServiceUtility.DisableCache();
      return this.CopyFromCampaign(abCampaign, new ABCampaignViewModel(), manager);
    }

    private ABCampaignViewModel CreateAbTestInternal(
      string issueAId,
      bool isFromScratch,
      IssueViewModel issueBModel,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid issueId = new Guid(issueAId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Campaign issue1 = manager.GetIssue(issueId);
      issue1.CampaignState = issue1.CampaignState != CampaignState.Sending && issue1.CampaignState != CampaignState.Completed ? CampaignState.ABTest : throw new InvalidOperationException(Res.Get<NewslettersResources>().IssueAlreadySent);
      Campaign issue2 = manager.CreateIssue(issue1.RootCampaign, true);
      if (issueBModel.DeliveryDate == DateTime.MinValue)
        issueBModel.DeliveryDate = issue2.DeliveryDate;
      Synchronizer.Synchronize((CampaignViewModel) issueBModel, issue2, manager);
      issue2.CampaignState = CampaignState.ABTest;
      if (!isFromScratch)
        manager.CopyMessageBody(issue1.MessageBody, issue2.MessageBody);
      issue2.MessageBody.Name = issue2.Name;
      if (issueBModel.ListId != Guid.Empty)
      {
        MailingList mailingList = manager.GetMailingList(issueBModel.ListId);
        issue2.List = mailingList;
      }
      ABCampaign abCampaign = manager.CreateABCampaign();
      abCampaign.DateSent = DateTime.UtcNow;
      abCampaign.CampaignA = issue1;
      abCampaign.CampaignB = issue2;
      abCampaign.RootCampaign = issue1.RootCampaign;
      abCampaign.TestingEnds = DateTime.UtcNow.AddDays(7.0);
      abCampaign.ABTestingStatus = ABTestingStatus.Stopped;
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return this.CopyFromCampaign(abCampaign, new ABCampaignViewModel(), manager);
    }

    private bool SetTestingNoteInternal(string abTestId, string value, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.GetABCampaign(new Guid(abTestId)).TestingNote = value;
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool SetConclusionInternal(string abTestId, string value, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.GetABCampaign(new Guid(abTestId)).Conclusion = value;
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }
  }
}
