// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewslettersDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// Base provider class for all concrete implementations of the newsletters module data providers.
  /// </summary>
  [ModuleId("3D8A2051-6F6F-437C-865E-B3177689AC12")]
  public abstract class NewslettersDataProvider : DataProviderBase
  {
    private Type[] knownTypes;

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public abstract DynamicListSettings CreateDynamicListSettings();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public abstract MailingList CreateList();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the list to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public abstract MailingList CreateList(Guid id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the list to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public abstract MailingList GetList(Guid id);

    /// <summary>Gets the query of all newsletter lists.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> objects.</returns>
    public abstract IQueryable<MailingList> GetLists();

    /// <summary>Deletes a list.</summary>
    /// <param name="list">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> to be deleted.</param>
    public abstract void DeleteList(MailingList list);

    /// <summary>Creates a new subscriber.</summary>
    /// <param name="generateShortId">Determines weather short id for the subscriber should be generated.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public abstract Subscriber CreateSubscriber(bool generateShortId);

    /// <summary>Creates a new subscriber with the specified id.</summary>
    /// <param name="subscriberId">Id of the subscriber to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public abstract Subscriber CreateSubscriber(bool generateShortId, Guid subscriberId);

    /// <summary>Gets the subscriber.</summary>
    /// <param name="subscriberId">Id of the subscriber to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public abstract Subscriber GetSubscriber(Guid subscriberId);

    /// <summary>Gets the query of the subscribers.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> objects.</returns>
    public abstract IQueryable<Subscriber> GetSubscribers();

    /// <summary>Deletes a subscriber.</summary>
    /// <param name="subscriber">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type to be deleted.</param>
    public abstract void DeleteSubscriber(Subscriber subscriber);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.SubscriptionInfo" />
    /// </summary>
    /// <returns>A newly created instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.SubscriptionInfo" />.</returns>
    public abstract SubscriptionInfo CreateSubscriptionInfo();

    /// <summary>
    /// Gets a query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.SubscriptionInfo" /> objects.
    /// </summary>
    /// <returns>A query of subscription info objects.</returns>
    public abstract IQueryable<SubscriptionInfo> GetSubscriptionInfos();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.UnsubscriptionInfo" /> type.
    /// </summary>
    /// <returns>A newly created instance of unsubscription info.</returns>
    public abstract UnsubscriptionInfo CreateUnsubscriptionInfo();

    /// <summary>
    /// Gets a query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.UnsubscriptionInfo" /> objects.
    /// </summary>
    /// <returns>A query of unsubscription info objects.</returns>
    public abstract IQueryable<UnsubscriptionInfo> GetUnsubscriptionInfos();

    /// <summary>Gets the query of the subscribers per mailing list.</summary>
    /// <param name="mailingListId">The mailing list Id.</param>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> objects.</returns>
    public abstract IQueryable<Subscriber> GetMailingListSubscribers(
      Guid mailingListId);

    /// <summary>Creates a new campaign and returns it.</summary>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public abstract Campaign CreateCampaign(bool generateShortId);

    /// <summary>
    /// Creates a new campaign with the specified id and returns it.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be created.</param>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public abstract Campaign CreateCampaign(bool generateShortId, Guid campaignId);

    /// <summary>Gets the campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the campaign.</returns>
    public abstract Campaign GetCampaign(Guid campaignId);

    /// <summary>Gets the query of campaigns.</summary>
    /// <returns>A query of campaigns.</returns>
    public abstract IQueryable<Campaign> GetCampaigns();

    /// <summary>Deletes a campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public abstract void DeleteCampaign(Campaign campaign);

    /// <summary>Creates a new issue and returns it.</summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the issue should be generated.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public abstract Campaign CreateIssue(Campaign rootCampaign, bool generateShortId);

    /// <summary>
    /// Creates a new issue with the specified id and returns it.
    /// </summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <param name="issueId">Id of the campaign to be created.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public abstract Campaign CreateIssue(
      Campaign rootCampaign,
      bool generateShortId,
      Guid issueId);

    /// <summary>Gets the issue by its id.</summary>
    /// <param name="issueId">Id of the issue to retrieve.</param>
    /// <returns>An instance of the campaign (issue).</returns>
    public abstract Campaign GetIssue(Guid issueId);

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaign">The campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public abstract Campaign GetLatestIssue(Campaign rootCampaign);

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaignId">The id of the root campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public abstract Campaign GetLatestIssue(Guid rootCampaignId);

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="rootCampaignId">Id of the campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public abstract IQueryable<Campaign> GetIssues(Guid rootCampaignId);

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="rootCampaign">The campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public abstract IQueryable<Campaign> GetIssues(Campaign rootCampaign);

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <returns>A query of the campaign's issues.</returns>
    public abstract IQueryable<Campaign> GetIssues();

    /// <summary>Creates a new A/B campaign and returns it.</summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public abstract ABCampaign CreateABCampaign();

    /// <summary>
    /// Creates a new A/B campaign with the specified id and returns it.
    /// </summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public abstract ABCampaign CreateABCampaign(Guid campaignId);

    /// <summary>Gets the A/B campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the A/B campaign.</returns>
    public abstract ABCampaign GetABCampaign(Guid campaignId);

    /// <summary>Gets the query of A/B campaigns.</summary>
    /// <returns>A query of campaigns.</returns>
    public abstract IQueryable<ABCampaign> GetABCampaigns();

    /// <summary>
    /// Gets the query of A/B campaigns for a given root campaign id.
    /// </summary>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <returns>A query of campaigns.</returns>
    public abstract IQueryable<ABCampaign> GetABCampaigns(Guid rootCampaignId);

    /// <summary>Deletes an A/B campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public abstract void DeleteABCampaign(ABCampaign campaign);

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public abstract CampaignLink CreateCampaignLink();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the campaign link to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public abstract CampaignLink CreateCampaignLink(Guid id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the campaign link to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public abstract CampaignLink GetCampaignLink(Guid id);

    /// <summary>Gets the query of all campaign links.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> objects.</returns>
    public abstract IQueryable<CampaignLink> GetCampaignLinks();

    /// <summary>Deletes a campaign link.</summary>
    /// <param name="campaignLink">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> to be deleted.</param>
    public abstract void DeleteCampaignLink(CampaignLink campaignLink);

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public abstract BounceStat CreateBounceStat();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the bounce stat to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public abstract BounceStat CreateBounceStat(Guid id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the bounce stat to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public abstract BounceStat GetBounceStat(Guid id);

    /// <summary>Gets the query of all bounce stats.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> objects.</returns>
    public abstract IQueryable<BounceStat> GetBounceStats();

    /// <summary>Deletes a bounce stat.</summary>
    /// <param name="bounceStat">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> to be deleted.</param>
    public abstract void DeleteBounceStat(BounceStat bounceStat);

    /// <summary>Creates a new message body and returns it.</summary>
    /// <returns>An instance of the newly created message body.</returns>
    public abstract MessageBody CreateMessageBody();

    /// <summary>
    /// Creates a new message body with the specified id and returns it.
    /// </summary>
    /// <param name="messageBodyId">The id of the message body with which the message body ought to be created.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public abstract MessageBody CreateMessageBody(Guid messageBodyId);

    /// <summary>Gets the message body by it's id.</summary>
    /// <param name="messageBodyId">Id of the message body to retrieve.</param>
    /// <returns>An instance of the message body.</returns>
    public abstract MessageBody GetMessageBody(Guid messageBodyId);

    /// <summary>Gets the query of message bodies.</summary>
    /// <returns>A query of message bodies</returns>
    public abstract IQueryable<MessageBody> GetMessageBodies();

    /// <summary>Deletes the message body.</summary>
    /// <param name="messageBody">The message body to be deleted.</param>
    public abstract void DeleteMessageBody(MessageBody messageBody);

    /// <summary>Copies the message body to another message body.</summary>
    /// <param name="source">The source message body.</param>
    /// <param name="target">The target message body.</param>
    public abstract void CopyMessageBody(MessageBody source, MessageBody target);

    /// <summary>Creates the message body page.</summary>
    /// <param name="messageBody">The message body.</param>
    /// <returns>The related page.</returns>
    public abstract PageData CreateMessageBodyPage(MessageBody messageBody);

    /// <summary>Gets the standard campaign pages root node.</summary>
    /// <returns>The standard campaign pages root node</returns>
    public abstract PageNode GetStandardCampaignRootNode();

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.</returns>
    public abstract LinkClickStat CreateLinkClickStat();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> objects.</returns>
    public abstract IQueryable<LinkClickStat> GetLinkClickStats();

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.</returns>
    public abstract DeliveryEntry CreateDeliveryEntry();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> objects.</returns>
    public abstract IQueryable<DeliveryEntry> GetDeliveryEntries();

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.</returns>
    public abstract OpenStat CreateOpenStat();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" /> objects.</returns>
    public abstract IQueryable<OpenStat> GetOpenStats();

    /// <summary>Creates the issue subscriber report.</summary>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public abstract IssueSubscriberReport CreateIssueSubscriberReport();

    /// <summary>Creates the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public abstract IssueSubscriberReport CreateIssueSubscriberReport(
      Guid reportId);

    /// <summary>Gets the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of issue subscriber report.</returns>
    public abstract IssueSubscriberReport GetIssueSubscriberReport(
      Guid reportId);

    /// <summary>Gets the issue subscriber reports.</summary>
    /// <returns>A query of issue subscriber reports.</returns>
    public abstract IQueryable<IssueSubscriberReport> GetIssueSubscriberReports();

    /// <summary>Updates the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    internal virtual void UpdateIssueSubscriberReport(Guid reportId)
    {
      IssueSubscriberReport subscriberReport;
      try
      {
        subscriberReport = this.GetIssueSubscriberReport(reportId);
      }
      catch (ItemNotFoundException ex)
      {
        return;
      }
      this.UpdateIssueSubscriberReport(subscriberReport);
    }

    internal virtual void UpdateIssueSubscriberReport(IssueSubscriberReport issueSubscriberReport)
    {
      try
      {
        issueSubscriberReport.MessageStatus = (MessageStatus) (this.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.Campaign.Id == issueSubscriberReport.IssueId && b.Subscriber.Id == issueSubscriberReport.SubscriberId && !b.IsProcessing)).Any<BounceStat>() ? 1024 : 0);
        if (issueSubscriberReport.Subscriber != null && issueSubscriberReport.Issue != null)
        {
          issueSubscriberReport.DeliveryStatus = this.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (d => d.CampaignId == issueSubscriberReport.IssueId && d.SubscriberId == issueSubscriberReport.SubscriberId)).Select<DeliveryEntry, DeliveryStatus>((Expression<Func<DeliveryEntry, DeliveryStatus>>) (d => d.DeliveryStatus)).FirstOrDefault<DeliveryStatus>();
          IssueSubscriberReport subscriberReport1 = issueSubscriberReport;
          DateTime? dateOpened = issueSubscriberReport.DateOpened;
          DateTime? nullable;
          if (!dateOpened.HasValue)
            nullable = this.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (op => op.CampaignId == issueSubscriberReport.IssueId && op.SubscriberId == issueSubscriberReport.SubscriberId)).Select<OpenStat, DateTime?>((Expression<Func<OpenStat, DateTime?>>) (op => (DateTime?) op.OpenedDate)).FirstOrDefault<DateTime?>();
          else
            nullable = dateOpened;
          subscriberReport1.DateOpened = nullable;
          IssueSubscriberReport subscriberReport2 = issueSubscriberReport;
          int num;
          if (!issueSubscriberReport.HasClicked)
            num = this.GetLinkClickStats().Any<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.CampaignId == issueSubscriberReport.IssueId && c.SubscriberId == issueSubscriberReport.SubscriberId)) ? 1 : 0;
          else
            num = 1;
          subscriberReport2.HasClicked = num != 0;
        }
        IssueSubscriberReport subscriberReport = issueSubscriberReport;
        int num1;
        if (!issueSubscriberReport.HasUnsubscribed)
          num1 = this.GetUnsubscriptionInfos().Any<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (u => u.Issue.Id == issueSubscriberReport.IssueId && u.Subscriber.Id == issueSubscriberReport.SubscriberId)) ? 1 : 0;
        else
          num1 = 1;
        subscriberReport.HasUnsubscribed = num1 != 0;
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
      }
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => throw new NotImplementedException();

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item) => throw new NotImplementedException();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes()
    {
      if (this.knownTypes == null)
        this.knownTypes = new Type[1]
        {
          typeof (MailingList)
        };
      return this.knownTypes;
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => nameof (NewslettersDataProvider);

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission1 = this.CreatePermission("Newsletters", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "View");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("Newsletters", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "Modify", "Delete");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("Newsletters", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "Create", "Modify", "Delete", "ChangeOwner");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("Newsletters", root.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(false, "Create");
      root.Permissions.Add(permission4);
    }
  }
}
