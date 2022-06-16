// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewslettersManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.BasicSettings;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists;
using Telerik.Sitefinity.Modules.Newsletters.Events;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>Managers class for the newsletters module.</summary>
  [ModuleId("3D8A2051-6F6F-437C-865E-B3177689AC12")]
  public class NewslettersManager : ManagerBase<NewslettersDataProvider>
  {
    private const string providerExecutingMessageBodyPagesToDelete = "providerExecutingMessageBodyPagesToDelete";
    private const string reportsToUpdateKey = "reports-to-update";
    private const string newDeliveryEntriesKey = "new-delivery-entries";
    internal const string ignoreStatisticsKey = "ignore-statistics";
    internal const string NewslettersEventSource = "Sitefinity Newsletters";
    private static readonly object statisticsLock = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> class.
    /// </summary>
    public NewslettersManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public NewslettersManager(string providerName)
      : this(providerName, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public NewslettersManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    static NewslettersManager()
    {
      ManagerBase<NewslettersDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(NewslettersManager.Provider_Executing);
      ManagerBase<NewslettersDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(NewslettersManager.Provider_Executed);
    }

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public MailingList CreateMailingList() => this.Provider.CreateList();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the list to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public MailingList CreateMailingList(Guid id) => this.Provider.CreateList(id);

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the list to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    [Obsolete("Use CreateMailingList(Guid id) instead.")]
    public MailingList CreateList(Guid id) => this.Provider.CreateList(id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the list to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public MailingList GetMailingList(Guid id) => this.Provider.GetList(id);

    /// <summary>Gets the query of all newsletter lists.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> objects.</returns>
    public IQueryable<MailingList> GetMailingLists() => this.Provider.GetLists();

    /// <summary>Deletes a list.</summary>
    /// <param name="list">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> to be deleted.</param>
    public void DeleteMailingList(Guid id)
    {
      IQueryable<Campaign> campaigns = this.GetCampaigns();
      Expression<Func<Campaign, bool>> predicate1 = (Expression<Func<Campaign, bool>>) (c => c.List.Id == id);
      foreach (Campaign campaign in (IEnumerable<Campaign>) campaigns.Where<Campaign>(predicate1))
      {
        campaign.CampaignState = CampaignState.MissingMailingList;
        campaign.List = (MailingList) null;
      }
      IQueryable<Campaign> issues = this.GetIssues();
      Expression<Func<Campaign, bool>> predicate2 = (Expression<Func<Campaign, bool>>) (c => c.List.Id == id);
      foreach (Campaign campaign in (IEnumerable<Campaign>) issues.Where<Campaign>(predicate2))
        campaign.List = (MailingList) null;
      MailingList list = this.GetMailingList(id);
      IQueryable<Subscriber> subscribers = this.GetSubscribers();
      Expression<Func<Subscriber, bool>> predicate3 = (Expression<Func<Subscriber, bool>>) (s => s.Lists.Contains(list));
      foreach (Subscriber subscriber in (IEnumerable<Subscriber>) subscribers.Where<Subscriber>(predicate3))
        subscriber.Lists.Remove(list);
      this.Provider.DeleteList(list);
    }

    /// <summary>Gets the instances of dynamic lists providers.</summary>
    /// <returns></returns>
    internal virtual IList<IDynamicListProvider> GetDynamicListProviders()
    {
      List<IDynamicListProvider> dynamicListProviders1 = new List<IDynamicListProvider>();
      ConfigElementDictionary<string, DynamicListProviderSettings> dynamicListProviders2 = this.GetNewslettersConfig().DynamicListProviders;
      foreach (string key in (IEnumerable<string>) dynamicListProviders2.Keys)
      {
        Type providerType = dynamicListProviders2[key].ProviderType;
        object obj = !(providerType == (Type) null) ? Activator.CreateInstance(providerType) : throw new ArgumentException("ProviderType property of DynamicListProviderSettings cannot be null");
        dynamicListProviders1.Add((IDynamicListProvider) obj);
      }
      return (IList<IDynamicListProvider>) dynamicListProviders1;
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider" /> of the specified name.
    /// </summary>
    /// <param name="providerName">Name of the provider that ought to be retrieved.</param>
    /// <returns>An instance of the <see cref="!:IDynamicListProvider." /></returns>
    internal virtual IDynamicListProvider GetDynamicListProvider(
      string providerName)
    {
      return ObjectFactory.IsTypeRegistered<IDynamicListProvider>(providerName) ? ObjectFactory.Resolve<IDynamicListProvider>(providerName) : (IDynamicListProvider) Activator.CreateInstance(Config.Get<NewslettersConfig>().DynamicListProviders[providerName].ProviderType);
    }

    /// <summary>Gets the list of all the available dynamic lists.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.DynamicListInfo" /> objects.</returns>
    public IList<DynamicListInfo> GetDynamicLists()
    {
      List<DynamicListInfo> dynamicLists = new List<DynamicListInfo>();
      foreach (IDynamicListProvider dynamicListProvider in (IEnumerable<IDynamicListProvider>) this.GetDynamicListProviders())
      {
        foreach (DynamicListInfo dynamicList in dynamicListProvider.GetDynamicLists())
          dynamicLists.Add(dynamicList);
      }
      return (IList<DynamicListInfo>) dynamicLists;
    }

    /// <summary>Gets the dynamic lists for a specified provider.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.DynamicListInfo" /> objects.</returns>
    public IList<DynamicListInfo> GetDynamicLists(string dynamicListProviderName) => (IList<DynamicListInfo>) this.GetDynamicListProvider(dynamicListProviderName).GetDynamicLists().ToList<DynamicListInfo>();

    /// <summary>Creates and returns a new subscriber.</summary>
    /// <param name="generateShortId">Determines weather short id for the subscriber should be generated.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public Subscriber CreateSubscriber(bool generateShortId) => this.Provider.CreateSubscriber(generateShortId);

    /// <summary>
    /// Creates and returns a new subscriber with the specified id.
    /// </summary>
    /// <param name="subscriberId">The id of the subscriber to be created.</param>
    /// <param name="generateShortId">Determines weather short id for the subscriber should be generated.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public Subscriber CreateSubscriber(bool generateShortId, Guid subscriberId) => this.Provider.CreateSubscriber(generateShortId, subscriberId);

    /// <summary>Gets the subscriber with the specified id.</summary>
    /// <param name="subscriberId">Id of the subscriber to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.</returns>
    public Subscriber GetSubscriber(Guid subscriberId) => this.Provider.GetSubscriber(subscriberId);

    /// <summary>Gets the query of all subscribers.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> objects.</returns>
    public IQueryable<Subscriber> GetSubscribers() => this.Provider.GetSubscribers();

    /// <summary>Deletes the subscriber with the specified id.</summary>
    /// <param name="subscriberId">Id of the subscriber to be deleted.</param>
    public void DeleteSubscriber(Guid subscriberId) => this.DeleteSubscriber(this.GetSubscriber(subscriberId));

    /// <summary>Deletes the subscriber.</summary>
    /// <param name="subscriber">Subscriber to be deleted.</param>
    public void DeleteSubscriber(Subscriber subscriber) => this.Provider.DeleteSubscriber(subscriber);

    public bool Subscribe(Subscriber subscriber, Guid listId)
    {
      MailingList mailingList = this.GetMailingLists().Where<MailingList>((Expression<Func<MailingList, bool>>) (ml => ml.Id == listId)).SingleOrDefault<MailingList>();
      if (mailingList == null)
        return false;
      subscriber.Lists.Add(mailingList);
      SubscriptionInfo subscriptionInfo = this.Provider.CreateSubscriptionInfo();
      subscriptionInfo.SubscriptionDate = DateTime.Now;
      subscriptionInfo.SubscriptionListId = listId;
      subscriber.SubscriptionInfo.Add(subscriptionInfo);
      if (mailingList.WelcomeTemplate != null)
      {
        try
        {
          RawMessageSource rawMessage = new RawMessageSource(mailingList.WelcomeTemplate, NewslettersManager.GetRootUrl());
          MergeContextItems mergeContextItems = Composer.ResolveUnsubscribeLink(mailingList.UnsubscribePageId, subscriber.Id.ToString(), Guid.Empty, listId);
          using (MailMessage mailMessage = Composer.ComposeMessage(mailingList.WelcomeMessageEmailAddress, subscriber, mailingList.WelcomeMessageSubject, rawMessage, (object) mailingList, (object) subscriber, (object) mergeContextItems))
          {
            string empty = string.Empty;
            string messageHtml = (string) null;
            AlternateView alternateView1 = mailMessage.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/plain"));
            AlternateView alternateView2 = mailMessage.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/html"));
            if (alternateView1 != null)
              empty = Encoding.GetEncoding(alternateView1.ContentType.CharSet).GetString(((MemoryStream) alternateView1.ContentStream).ToArray());
            if (mailMessage.IsBodyHtml)
              messageHtml = alternateView2 == null ? empty : Encoding.GetEncoding(alternateView2.ContentType.CharSet).GetString(((MemoryStream) alternateView2.ContentStream).ToArray());
            NewslettersManager.SendMessageViaNotificationsService(empty, messageHtml, mailMessage.Subject, (string) mailingList.Title, mailingList.Id, mailMessage.From.Address, mailMessage.From.DisplayName, (IEnumerable<Subscriber>) new List<Subscriber>()
            {
              subscriber
            }.AsQueryable<Subscriber>(), (Dictionary<string, string>) null);
          }
        }
        catch (Exception ex)
        {
          Log.Write((object) ex);
        }
      }
      EventHub.Raise((IEvent) new NewsletterSubscriptionCompletedEvent(subscriber.Email, mailingList.Id, "Sitefinity Newsletters"));
      return true;
    }

    /// <summary>Unsubscribes the specified subscriber.</summary>
    /// <param name="subscriber">The subscriber.</param>
    /// <param name="listId">The list id.</param>
    /// <returns>Whether unsubscription was done.</returns>
    public bool Unsubscribe(Subscriber subscriber, Guid listId) => this.Unsubscribe(subscriber, listId, (Campaign) null);

    /// <summary>Unsubscribes the specified subscriber.</summary>
    /// <param name="subscriber">The subscriber.</param>
    /// <param name="listId">The list id.</param>
    /// <param name="issue">The issue.</param>
    /// <returns>Whether unsubscription was done.</returns>
    public bool Unsubscribe(Subscriber subscriber, Guid listId, Campaign issue)
    {
      MailingList mailingList = this.GetMailingLists().Where<MailingList>((Expression<Func<MailingList, bool>>) (ml => ml.Id == listId)).SingleOrDefault<MailingList>();
      if (!subscriber.Lists.Any<MailingList>((Func<MailingList, bool>) (ml => ml.Id == listId)) || mailingList == null)
        return false;
      subscriber.Lists.Remove(mailingList);
      ++mailingList.TotalUnsubscriptions;
      if (!this.GetUnsubscriptionInfos().Any<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (info => info.Issue == issue && info.Subscriber == subscriber)))
      {
        UnsubscriptionInfo unsubscriptionInfo = this.CreateUnsubscriptionInfo();
        unsubscriptionInfo.Issue = issue;
        unsubscriptionInfo.Subscriber = subscriber;
        unsubscriptionInfo.UnsubscriptionDate = DateTime.UtcNow;
        unsubscriptionInfo.UnsubscriptionListId = listId;
      }
      if (mailingList.GoodByeTemplate != null)
      {
        try
        {
          RawMessageSource rawMessage = new RawMessageSource(mailingList.GoodByeTemplate, NewslettersManager.GetRootUrl());
          using (MailMessage mailMessage = Composer.ComposeMessage(mailingList.GoodByeMessageEmailAddress, subscriber, mailingList.GoodByeMessageSubject, rawMessage, (object) mailingList, (object) subscriber))
          {
            string empty = string.Empty;
            string messageHtml = (string) null;
            AlternateView alternateView1 = mailMessage.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/plain"));
            AlternateView alternateView2 = mailMessage.AlternateViews.FirstOrDefault<AlternateView>((Func<AlternateView, bool>) (view => view.ContentType.MediaType == "text/html"));
            if (alternateView1 != null)
              empty = Encoding.GetEncoding(alternateView1.ContentType.CharSet).GetString(((MemoryStream) alternateView1.ContentStream).ToArray());
            if (mailMessage.IsBodyHtml)
              messageHtml = alternateView2 == null ? empty : Encoding.GetEncoding(alternateView2.ContentType.CharSet).GetString(((MemoryStream) alternateView2.ContentStream).ToArray());
            NewslettersManager.SendMessageViaNotificationsService(empty, messageHtml, mailMessage.Subject, (string) mailingList.Title, mailingList.Id, mailMessage.From.Address, mailMessage.From.DisplayName, (IEnumerable<Subscriber>) new List<Subscriber>()
            {
              subscriber
            }.AsQueryable<Subscriber>(), (Dictionary<string, string>) null);
          }
        }
        catch (Exception ex)
        {
          Log.Write((object) ex);
        }
      }
      EventHub.Raise((IEvent) new NewsletterSubscriptionDeletedEvent(subscriber.Email, mailingList.Id, issue == null ? Guid.Empty : issue.Id, "Sitefinity Newsletters"));
      return true;
    }

    /// <summary>Creates a new campaign and returns it.</summary>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public Campaign CreateCampaign(bool generateShortId) => this.Provider.CreateCampaign(generateShortId);

    /// <summary>
    /// Creates a new campaign with the specified id and returns it.
    /// </summary>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <param name="campaignId">Id of the campaign to be created.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public Campaign CreateCampaign(bool generateShortId, Guid campaignId) => this.Provider.CreateCampaign(generateShortId, campaignId);

    /// <summary>Gets the campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the campaign.</returns>
    public Campaign GetCampaign(Guid campaignId) => this.Provider.GetCampaign(campaignId);

    /// <summary>Gets the query of  campaigns.</summary>
    /// <returns>A query of the specified type of the campaign.</returns>
    public IQueryable<Campaign> GetCampaigns() => this.Provider.GetCampaigns();

    /// <summary>Deletes a campaign.</summary>
    /// <param name="campaignId">Id of the campaign to be deleted.</param>
    /// \
    public void DeleteCampaign(Guid campaignId) => this.DeleteCampaign(this.GetCampaign(campaignId));

    /// <summary>Deletes a campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public void DeleteCampaign(Campaign campaign)
    {
      PageManager manager = PageManager.GetManager();
      if (campaign.MessageBody != null)
      {
        Guid id = campaign.MessageBody.Id;
        PageNode pageNode = manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == id)).SingleOrDefault<PageNode>();
        if (pageNode != null)
          App.WorkWith().Page(pageNode.Id).Delete().SaveChanges();
      }
      this.Provider.DeleteCampaign(campaign);
    }

    public PageNode GetStandardCampaignRootNode() => this.provider.GetStandardCampaignRootNode();

    /// <summary>
    /// Schedules the campaign to be delivered at the specified time.
    /// </summary>
    /// <param name="campaignId">Id of the campaign to be delivered.</param>
    /// <param name="deliveryDate">Date and time of when the campaign ought to be delivered.</param>
    public void ScheduleCampaign(Guid campaignId, DateTime deliveryDate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new NewslettersManager.\u003C\u003Ec__DisplayClass30_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass300.campaignId = campaignId;
      // ISSUE: reference to a compiler-generated field
      Campaign campaign = this.GetCampaign(cDisplayClass300.campaignId);
      campaign.CampaignState = campaign.CampaignState != CampaignState.Sending && campaign.CampaignState != CampaignState.Completed ? CampaignState.Scheduled : throw new System.InvalidOperationException(Res.Get<NewslettersResources>().IssueAlreadySent);
      campaign.DeliveryDate = deliveryDate;
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      ScheduledTaskData task1 = manager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "CampaignDeliveryTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass300.campaignId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      if (task1 != null && task1.ExecuteTime != deliveryDate)
      {
        manager.DeleteTaskData(task1);
        task1 = (ScheduledTaskData) null;
      }
      if (task1 != null)
        return;
      // ISSUE: reference to a compiler-generated field
      CampaignDeliveryTask task2 = new CampaignDeliveryTask(cDisplayClass300.campaignId, deliveryDate);
      manager.AddTask((ScheduledTask) task2);
      manager.SaveChanges();
    }

    /// <summary>
    /// Unschedules the campaign. Does not fail if no scheduling task is found.
    /// </summary>
    /// <param name="campaignId">The campaign id.</param>
    public void UnscheduleCampaign(Guid campaignId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new NewslettersManager.\u003C\u003Ec__DisplayClass31_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.campaignId = campaignId;
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      ScheduledTaskData task = manager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "CampaignDeliveryTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass310.campaignId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      if (task == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.GetCampaign(cDisplayClass310.campaignId).CampaignState = CampaignState.Draft;
      manager.DeleteTaskData(task);
      manager.SaveChanges();
    }

    /// <summary>Creates a new issue and returns it.</summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the issue should be generated.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public Campaign CreateIssue(Campaign rootCampaign, bool generateShortId) => this.Provider.CreateIssue(rootCampaign, generateShortId);

    /// <summary>
    /// Creates a new issue with the specified id and returns it.
    /// </summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <param name="issueId">Id of the campaign to be created.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public Campaign CreateIssue(Campaign rootCampaign, bool generateShortId, Guid issueId) => this.Provider.CreateIssue(rootCampaign, generateShortId, issueId);

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaign">The root campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public Campaign GetLatestIssue(Campaign rootCampaign) => this.Provider.GetLatestIssue(rootCampaign);

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaignId">The id of the root campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public Campaign GetLatestIssue(Guid rootCampaignId) => this.Provider.GetLatestIssue(rootCampaignId);

    /// <summary>Gets the issue by its id.</summary>
    /// <param name="issueId">Id of the issue to retrieve.</param>
    /// <returns>An instance of the campaign (issue).</returns>
    public Campaign GetIssue(Guid issueId) => this.Provider.GetIssue(issueId);

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="rootCampaign">The campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public IQueryable<Campaign> GetIssues(Campaign rootCampaign) => this.Provider.GetIssues(rootCampaign);

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="rootCampaignId">Id of the campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public IQueryable<Campaign> GetIssues(Guid rootCampaignId) => this.Provider.GetIssues(rootCampaignId);

    /// <summary>Gets the query of all issues.</summary>
    /// <returns>A query of all issues.</returns>
    public IQueryable<Campaign> GetIssues() => this.Provider.GetIssues();

    /// <summary>Deletes a campaign.</summary>
    /// <param name="issueId">Id of the campaign to be deleted.</param>
    public void DeleteIssue(Guid issueId) => this.DeleteCampaign(this.GetIssue(issueId));

    /// <summary>Deletes a campaign.</summary>
    /// <param name="issueId">An instance of the campaign to be deleted.</param>
    public void DeleteIssue(Campaign issue) => this.DeleteCampaign(issue);

    /// <summary>Creates a new A/B campaign and returns it.</summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public ABCampaign CreateABCampaign() => this.Provider.CreateABCampaign();

    /// <summary>
    /// Creates a new A/B campaign with the specified id and returns it.
    /// </summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public ABCampaign CreateABCampaign(Guid campaignId) => this.Provider.CreateABCampaign(campaignId);

    /// <summary>Gets the A/B campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the A/B campaign.</returns>
    public ABCampaign GetABCampaign(Guid campaignId) => this.Provider.GetABCampaign(campaignId);

    /// <summary>Gets the query of A/B campaigns.</summary>
    /// <returns>A query of campaigns.</returns>
    public IQueryable<ABCampaign> GetABCampaigns() => this.Provider.GetABCampaigns();

    /// <summary>
    /// Gets the query of A/B campaigns for a given root campaign.
    /// </summary>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <returns>A query of campaigns.</returns>
    public IQueryable<ABCampaign> GetABCampaigns(Guid rootCampaignId) => this.Provider.GetABCampaigns(rootCampaignId);

    /// <summary>Deletes an A/B campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public void DeleteABCampaign(ABCampaign campaign) => this.Provider.DeleteABCampaign(campaign);

    /// <summary>Deletes an A/B campaign.</summary>
    /// <param name="campaignId">Id of the A/B campaign to be deleted.</param>
    public void DeleteABCampaign(Guid campaignId) => this.Provider.DeleteABCampaign(this.GetABCampaign(campaignId));

    /// <summary>
    /// Gets the list of campaigns that are eligable for A/B testing
    /// </summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> objects.</returns>
    public IList<Campaign> GetABTestingEligableCampaigns()
    {
      List<Campaign> list = this.GetIssues().Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 1 || (int) i.CampaignState == 0)).ToList<Campaign>();
      List<Campaign> campaignList = new List<Campaign>();
      foreach (Campaign campaign in list)
      {
        Campaign issue = campaign;
        if (list.Where<Campaign>((Func<Campaign, bool>) (i => i.List.Id == issue.List.Id)).Count<Campaign>() > 1)
          campaignList.Add(issue);
      }
      return (IList<Campaign>) list;
    }

    /// <summary>Starts ab testing.</summary>
    /// <remarks>Calls SaveChanges internally.</remarks>
    /// <param name="abCampaignId">The ab campaign id.</param>
    public void StartTesting(Guid abCampaignId)
    {
      if (this.HasTooManySubscribers())
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "You have more subscribers than your license permits. Please remove some of the subscribers in order to send campaigns.", (Exception) null);
      NewsletterValidator.VerifySmtpSettings();
      NewslettersManager.RunInBackground((Action) (() => NewslettersManager.StartTestingImpl(abCampaignId, this.Provider.Name)), "Newsletter: Failed send A/B test sample: {0}");
    }

    private static void StartTestingImpl(Guid abCampaignId, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      int num1 = 0;
      int num2 = 0;
      ABCampaign abCampaign = manager.GetABCampaign(abCampaignId);
      abCampaign.ABTestingStatus = abCampaign.ABTestingStatus != ABTestingStatus.Done && abCampaign.ABTestingStatus != ABTestingStatus.InProgress ? ABTestingStatus.InProgress : throw new System.InvalidOperationException(Res.Get<NewslettersResources>().TestAlreadyStarted);
      abCampaign.DateSent = DateTime.UtcNow;
      abCampaign.CampaignA.CampaignState = CampaignState.ABTest;
      abCampaign.CampaignB.CampaignState = CampaignState.ABTest;
      manager.SaveChanges();
      IQueryable<Subscriber> source = abCampaign.CampaignA.List.Subscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => !s.IsSuspended));
      RawMessageSource rawMessage1 = new RawMessageSource(abCampaign.CampaignA.MessageBody, NewslettersManager.GetRootUrl());
      RawMessageSource rawMessage2 = new RawMessageSource(abCampaign.CampaignB.MessageBody, NewslettersManager.GetRootUrl());
      int int32 = Convert.ToInt32(Math.Floor((double) (source.Count<Subscriber>() * abCampaign.TestingSamplePercentage) * 0.01));
      Subscriber[] array = source.OrderBy<Subscriber, Guid>((Expression<Func<Subscriber, Guid>>) (s => s.Id)).Take<Subscriber>(int32).ToArray<Subscriber>();
      List<Subscriber> subscriberList1 = new List<Subscriber>();
      List<Subscriber> subscriberList2 = new List<Subscriber>();
      List<DeliveryEntry> deliveryEntryList1 = new List<DeliveryEntry>();
      List<DeliveryEntry> deliveryEntryList2 = new List<DeliveryEntry>();
      for (int index = 0; index < int32; ++index)
      {
        Subscriber subscriber = array[index];
        Guid empty = Guid.Empty;
        int num3 = (num1 + num2) % 2 == 0 ? 1 : 0;
        Guid id;
        if (num3 != 0)
        {
          id = abCampaign.CampaignA.Id;
          subscriberList1.Add(subscriber);
        }
        else
        {
          id = abCampaign.CampaignB.Id;
          subscriberList2.Add(subscriber);
        }
        DeliveryEntry deliveryEntry = manager.CreateDeliveryEntry();
        deliveryEntry.CampaignId = id;
        deliveryEntry.DeliverySubscriber = subscriber;
        deliveryEntry.DeliveryDate = DateTime.UtcNow;
        deliveryEntry.DeliveryStatus = DeliveryStatus.Pending;
        if (num3 != 0)
        {
          ++num1;
          deliveryEntryList1.Add(deliveryEntry);
        }
        else
        {
          ++num2;
          deliveryEntryList2.Add(deliveryEntry);
        }
        if (index % 200 == 0)
          manager.SaveChanges();
      }
      manager.SaveChanges();
      try
      {
        NewslettersManager.SendCampaignViaNotificationsService(rawMessage1, abCampaign.CampaignA, (IEnumerable<Subscriber>) subscriberList1);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
        deliveryEntryList1.ForEach((Action<DeliveryEntry>) (entry => entry.DeliveryStatus = DeliveryStatus.Failure));
      }
      try
      {
        NewslettersManager.SendCampaignViaNotificationsService(rawMessage2, abCampaign.CampaignB, (IEnumerable<Subscriber>) subscriberList2);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
        deliveryEntryList2.ForEach((Action<DeliveryEntry>) (entry => entry.DeliveryStatus = DeliveryStatus.Failure));
      }
      manager.SaveChanges();
      NewslettersManager.SetNotificationsBackgroundStatsCollection(abCampaign.CampaignA.Id, provider);
      NewslettersManager.SetNotificationsBackgroundStatsCollection(abCampaign.CampaignB.Id, provider);
    }

    /// <summary>Ends ab testing.</summary>
    /// <remarks>Calls SaveChanges internally.</remarks>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <returns></returns>
    public string EndTesting(Guid abCampaignId)
    {
      ABCampaign abCampaign = this.GetABCampaign(abCampaignId);
      if (abCampaign.WinningCondition == CampaignWinningCondition.ManualDecision)
        throw new ArgumentException(Res.Get<NewslettersResources>("ABTestEndManualDecision"));
      abCampaign.ABTestingStatus = abCampaign.ABTestingStatus != ABTestingStatus.Done ? ABTestingStatus.Done : throw new System.InvalidOperationException(Res.Get<NewslettersResources>().TestAlreadyStarted);
      IssueStatistics issueStatistics1 = new IssueStatistics(abCampaign.CampaignA.Id, this);
      IssueStatistics issueStatistics2 = new IssueStatistics(abCampaign.CampaignB.Id, this);
      bool flag;
      switch (abCampaign.WinningCondition)
      {
        case CampaignWinningCondition.MoreOpenedEmails:
          flag = issueStatistics1.UniqueOpeningsCount >= issueStatistics2.UniqueOpeningsCount;
          break;
        case CampaignWinningCondition.MoreLinkClicks:
          flag = issueStatistics1.ClickedIssuesCount >= issueStatistics2.ClickedIssuesCount;
          break;
        case CampaignWinningCondition.LessBounces:
          flag = issueStatistics1.BouncesCount <= issueStatistics2.BouncesCount;
          break;
        default:
          flag = true;
          break;
      }
      Campaign winningCampaign = !flag ? abCampaign.CampaignB : abCampaign.CampaignA;
      this.SaveChanges();
      this.DecideWinner(abCampaign, winningCampaign);
      return winningCampaign.Name;
    }

    /// <summary>
    /// Creates a winning issue and sends it to the rest of the subscribers.
    /// </summary>
    /// <remarks>Calls SaveChanges internally.</remarks>
    /// <param name="abCampaignId">The ab campaign id.</param>
    /// <param name="winningCampaignId">The winning campaign id.</param>
    public void DecideWinner(Guid abCampaignId, Guid winningCampaignId)
    {
      ABCampaign abCampaign = this.GetABCampaign(abCampaignId);
      if (abCampaign.ABTestingStatus == ABTestingStatus.Done)
        throw new System.InvalidOperationException(Res.Get<NewslettersResources>().TestAlreadyStarted);
      Campaign campaign = this.GetCampaign(winningCampaignId);
      this.DecideWinner(abCampaign, campaign);
    }

    /// <summary>
    /// Creates a winning issue and sends it to the rest of the subscribers.
    /// </summary>
    /// <remarks>Calls SaveChanges internally.</remarks>
    /// <param name="abCampaign">The ab campaign.</param>
    /// <param name="winningCampaign">The winning campaign.</param>
    public void DecideWinner(ABCampaign abCampaign, Campaign winningCampaign)
    {
      if (winningCampaign.List == null)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new NewslettersManager.\u003C\u003Ec__DisplayClass54_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.\u003C\u003E4__this = this;
      abCampaign.WinnerIssueOriginalId = winningCampaign.Id;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.copiedWinningIssue = this.CreateIssue(abCampaign.RootCampaign, true);
      // ISSUE: reference to a compiler-generated field
      abCampaign.WinnerIssue = cDisplayClass540.copiedWinningIssue;
      // ISSUE: reference to a compiler-generated field
      Synchronizer.CopyProperties(winningCampaign, cDisplayClass540.copiedWinningIssue);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.copiedWinningIssue.CampaignState = CampaignState.Draft;
      // ISSUE: reference to a compiler-generated field
      this.CopyMessageBody(winningCampaign.MessageBody, cDisplayClass540.copiedWinningIssue.MessageBody);
      // ISSUE: reference to a compiler-generated field
      this.CopyIssueStatistics(abCampaign.CampaignA, cDisplayClass540.copiedWinningIssue);
      // ISSUE: reference to a compiler-generated field
      this.CopyIssueStatistics(abCampaign.CampaignB, cDisplayClass540.copiedWinningIssue);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass540.copiedWinningIssue.RootCampaign != null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.CopyMessageBody(cDisplayClass540.copiedWinningIssue.MessageBody, cDisplayClass540.copiedWinningIssue.RootCampaign.MessageBody);
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.copiedWinningIssue.DeliveryDate = DateTime.UtcNow;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.copiedWinningIssue.CampaignState = CampaignState.Sending;
      this.SaveChanges();
      // ISSUE: reference to a compiler-generated field
      IQueryable<Subscriber> source = cDisplayClass540.copiedWinningIssue.List.Subscribers();
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (Subscriber), "s");
      // ISSUE: method reference
      UnaryExpression left = Expression.Not((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Subscriber.get_IsSuspended))));
      // ISSUE: method reference
      MethodInfo methodFromHandle = (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Queryable.Any));
      // ISSUE: method reference
      Expression[] expressionArray = new Expression[2]
      {
        (Expression) Expression.Call((Expression) Expression.Constant((object) this, typeof (NewslettersManager)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (NewslettersManager.GetDeliveryEntries)), Array.Empty<Expression>()),
        null
      };
      ParameterExpression parameterExpression2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      expressionArray[1] = (Expression) Expression.Quote((Expression) Expression.Lambda<Func<DeliveryEntry, bool>>((Expression) Expression.AndAlso(d.CampaignId == cDisplayClass540.copiedWinningIssue.Id, (Expression) Expression.Equal(d.SubscriberId, (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Subscriber.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), parameterExpression2));
      UnaryExpression right = Expression.Not((Expression) Expression.Call((Expression) null, methodFromHandle, expressionArray));
      Expression<Func<Subscriber, bool>> predicate = Expression.Lambda<Func<Subscriber, bool>>((Expression) Expression.AndAlso((Expression) left, (Expression) right), parameterExpression1);
      Subscriber[] array = source.Where<Subscriber>(predicate).ToArray<Subscriber>();
      // ISSUE: reference to a compiler-generated field
      RawMessageSource rawMessage = new RawMessageSource(cDisplayClass540.copiedWinningIssue.MessageBody, NewslettersManager.GetRootUrl());
      List<Subscriber> subscriberList = new List<Subscriber>();
      for (int index = 0; index < array.Length; ++index)
      {
        Subscriber subscriber = array[index];
        subscriberList.Add(subscriber);
        DeliveryEntry deliveryEntry = this.CreateDeliveryEntry();
        // ISSUE: reference to a compiler-generated field
        deliveryEntry.CampaignId = cDisplayClass540.copiedWinningIssue.Id;
        deliveryEntry.DeliverySubscriber = subscriber;
        deliveryEntry.DeliveryDate = DateTime.UtcNow;
        deliveryEntry.DeliveryStatus = DeliveryStatus.Pending;
        if (index % 200 == 0)
          this.SaveChanges();
      }
      this.SaveChanges();
      // ISSUE: reference to a compiler-generated field
      NewslettersManager.SendCampaignViaNotificationsService(rawMessage, cDisplayClass540.copiedWinningIssue, (IEnumerable<Subscriber>) subscriberList);
      this.SaveChanges();
      // ISSUE: reference to a compiler-generated field
      NewslettersManager.SetNotificationsBackgroundStatsCollection(cDisplayClass540.copiedWinningIssue.Id, this.Provider.Name);
    }

    /// <summary>
    /// Copies the issue statistics. Copies DeliveryEntries, ClickStats, BounceStats and OpenStats.
    /// </summary>
    /// <remarks>Calls SaveChanges internally.</remarks>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    internal void CopyIssueStatistics(Campaign source, Campaign target)
    {
      IOrderedQueryable<BounceStat> source1 = this.GetBounceStats().Where<BounceStat>((Expression<Func<BounceStat, bool>>) (s => s.Campaign.Id == source.Id)).OrderBy<BounceStat, Guid>((Expression<Func<BounceStat, Guid>>) (s => s.Id));
      double num1 = Math.Ceiling((double) source1.Count<BounceStat>() / 200.0);
      for (int index = 0; (double) index < num1; ++index)
      {
        foreach (BounceStat bounceStat1 in (IEnumerable<BounceStat>) source1.Skip<BounceStat>(index * 200).Take<BounceStat>(200))
        {
          BounceStat bounceStat2 = this.CreateBounceStat();
          bounceStat2.AdditionalInfo = bounceStat1.AdditionalInfo;
          bounceStat2.Campaign = target;
          bounceStat2.LastModified = bounceStat1.LastModified;
          bounceStat2.BounceStatus = bounceStat1.BounceStatus;
          bounceStat2.Provider = bounceStat1.Provider;
          bounceStat2.SmtpStatus = bounceStat1.SmtpStatus;
          bounceStat2.Subscriber = bounceStat1.Subscriber;
        }
        this.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        this.SaveChanges();
      }
      IOrderedQueryable<LinkClickStat> source2 = this.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.CampaignId == source.Id)).OrderBy<LinkClickStat, Guid>((Expression<Func<LinkClickStat, Guid>>) (c => c.CampaignId));
      double num2 = Math.Ceiling((double) source2.Count<LinkClickStat>() / 200.0);
      for (int index = 0; (double) index < num2; ++index)
      {
        foreach (LinkClickStat linkClickStat1 in (IEnumerable<LinkClickStat>) source2.Skip<LinkClickStat>(index * 200).Take<LinkClickStat>(200))
        {
          LinkClickStat linkClickStat2 = this.CreateLinkClickStat();
          linkClickStat2.CampaignId = target.Id;
          linkClickStat2.DateTimeClicked = linkClickStat1.DateTimeClicked;
          linkClickStat2.SubscriberId = linkClickStat1.SubscriberId;
          linkClickStat2.Url = linkClickStat1.Url;
        }
        this.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        this.SaveChanges();
      }
      IOrderedQueryable<OpenStat> source3 = this.GetOpenStats().Where<OpenStat>((Expression<Func<OpenStat, bool>>) (o => o.CampaignId == source.Id)).OrderBy<OpenStat, Guid>((Expression<Func<OpenStat, Guid>>) (o => o.CampaignId));
      double num3 = Math.Ceiling((double) source3.Count<OpenStat>() / 200.0);
      for (int index = 0; (double) index < num3; ++index)
      {
        foreach (OpenStat openStat1 in (IEnumerable<OpenStat>) source3.Skip<OpenStat>(index * 200).Take<OpenStat>(200))
        {
          OpenStat openStat2 = this.CreateOpenStat();
          openStat2.CampaignId = target.Id;
          openStat2.OpenedDate = openStat1.OpenedDate;
          openStat2.SubscriberId = openStat1.SubscriberId;
        }
        this.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        this.SaveChanges();
      }
      IOrderedQueryable<UnsubscriptionInfo> source4 = this.GetUnsubscriptionInfos().Where<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (u => u.Issue == source)).OrderBy<UnsubscriptionInfo, Guid>((Expression<Func<UnsubscriptionInfo, Guid>>) (u => u.Id));
      double num4 = Math.Ceiling((double) source4.Count<UnsubscriptionInfo>() / 200.0);
      for (int index = 0; (double) index < num4; ++index)
      {
        foreach (UnsubscriptionInfo unsubscriptionInfo1 in (IEnumerable<UnsubscriptionInfo>) source4.Skip<UnsubscriptionInfo>(index * 200).Take<UnsubscriptionInfo>(200))
        {
          UnsubscriptionInfo unsubscriptionInfo2 = this.CreateUnsubscriptionInfo();
          unsubscriptionInfo2.Issue = target;
          unsubscriptionInfo2.Subscriber = unsubscriptionInfo1.Subscriber;
          unsubscriptionInfo2.Reason = unsubscriptionInfo1.Reason;
          unsubscriptionInfo2.UnsubscriptionDate = unsubscriptionInfo1.UnsubscriptionDate;
          unsubscriptionInfo2.UnsubscriptionListId = unsubscriptionInfo1.UnsubscriptionListId;
        }
        this.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        this.SaveChanges();
      }
      IOrderedQueryable<DeliveryEntry> source5 = this.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (e => e.CampaignId == source.Id)).OrderBy<DeliveryEntry, Guid>((Expression<Func<DeliveryEntry, Guid>>) (e => e.CampaignId));
      double num5 = Math.Ceiling((double) source5.Count<DeliveryEntry>() / 200.0);
      for (int index = 0; (double) index < num5; ++index)
      {
        foreach (DeliveryEntry deliveryEntry1 in (IEnumerable<DeliveryEntry>) source5.Skip<DeliveryEntry>(index * 200).Take<DeliveryEntry>(200))
        {
          DeliveryEntry deliveryEntry2 = this.CreateDeliveryEntry();
          deliveryEntry2.CampaignId = target.Id;
          deliveryEntry2.DeliveryDate = deliveryEntry1.DeliveryDate;
          deliveryEntry2.DeliveryStatus = deliveryEntry1.DeliveryStatus;
          deliveryEntry2.SubscriberId = deliveryEntry1.SubscriberId;
        }
        this.SaveChanges();
      }
    }

    /// <summary>Schedules the A/B test send.</summary>
    /// <param name="abTestId">The A/B test id.</param>
    /// <param name="deliveryDate">The delivery date.</param>
    public void ScheduleAbTestSend(Guid abTestId, DateTime deliveryDate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass56_0 cDisplayClass560 = new NewslettersManager.\u003C\u003Ec__DisplayClass56_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass560.abTestId = abTestId;
      // ISSUE: reference to a compiler-generated field
      ABCampaign abCampaign = this.GetABCampaign(cDisplayClass560.abTestId);
      abCampaign.DateSent = deliveryDate;
      abCampaign.ABTestingStatus = ABTestingStatus.Scheduled;
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      ScheduledTaskData task1 = manager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "AbTestSendTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass560.abTestId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      if (task1 != null && task1.ExecuteTime != deliveryDate)
      {
        manager.DeleteTaskData(task1);
        task1 = (ScheduledTaskData) null;
      }
      if (task1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AbTestSendTask task2 = new AbTestSendTask(cDisplayClass560.abTestId, deliveryDate);
        manager.AddTask((ScheduledTask) task2);
        manager.SaveChanges();
      }
      this.SaveChanges();
    }

    /// <summary>
    /// Unschedules the A/B test send task. Does not fail is no such task exists.
    /// </summary>
    /// <param name="abTestId">The A/B test id.</param>
    public void UnscheduleAbTestSend(Guid abTestId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass57_0 cDisplayClass570 = new NewslettersManager.\u003C\u003Ec__DisplayClass57_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass570.abTestId = abTestId;
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      ScheduledTaskData task = manager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "AbTestSendTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass570.abTestId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      if (task == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.GetABCampaign(cDisplayClass570.abTestId).ABTestingStatus = ABTestingStatus.Stopped;
      manager.DeleteTaskData(task);
      manager.SaveChanges();
      this.SaveChanges();
    }

    /// <summary>Schedules the A/B test end.</summary>
    /// <param name="abTestId">The A/B test id.</param>
    /// <param name="endDate">The end date.</param>
    public void ScheduleAbTestEnd(Guid abTestId, DateTime endDate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersManager.\u003C\u003Ec__DisplayClass58_0 cDisplayClass580 = new NewslettersManager.\u003C\u003Ec__DisplayClass58_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass580.abTestId = abTestId;
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      ScheduledTaskData task1 = manager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "AbTestEndTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass580.abTestId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      if (task1 != null && task1.ExecuteTime != endDate)
      {
        manager.DeleteTaskData(task1);
        task1 = (ScheduledTaskData) null;
      }
      if (task1 != null)
        return;
      // ISSUE: reference to a compiler-generated field
      AbTestEndTask task2 = new AbTestEndTask(cDisplayClass580.abTestId, endDate);
      manager.AddTask((ScheduledTask) task2);
      manager.SaveChanges();
    }

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public CampaignLink CreateCampaignLink() => this.Provider.CreateCampaignLink();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the campaign link to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public CampaignLink CreateCampaignLink(Guid id) => this.Provider.CreateCampaignLink(id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the campaign link to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public CampaignLink GetCampaignLink(Guid id) => this.Provider.GetCampaignLink(id);

    /// <summary>Gets the query of all campaign links.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> objects.</returns>
    public IQueryable<CampaignLink> GetCampaignLinks() => this.Provider.GetCampaignLinks();

    /// <summary>Deletes a campaign link.</summary>
    /// <param name="campaignLink">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> to be deleted.</param>
    public void DeleteCampaignLink(CampaignLink campaignLink) => this.Provider.DeleteCampaignLink(campaignLink);

    /// <summary>Deletes a campaign link.</summary>
    /// <param name="id">The id of the campaign link to be deleted.</param>
    public void DeleteCampaignLink(Guid id) => this.Provider.DeleteCampaignLink(this.GetCampaignLink(id));

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public BounceStat CreateBounceStat() => this.Provider.CreateBounceStat();

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the bounce stat to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public BounceStat CreateBounceStat(Guid id) => this.Provider.CreateBounceStat(id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the bounce stat to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public BounceStat GetBounceStat(Guid id) => this.Provider.GetBounceStat(id);

    /// <summary>Gets the query of all bounce stats.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> objects.</returns>
    public IQueryable<BounceStat> GetBounceStats() => this.Provider.GetBounceStats();

    /// <summary>Deletes a bounce stat.</summary>
    /// <param name="bounceStat">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> to be deleted.</param>
    public void DeleteBounceStat(BounceStat bounceStat) => this.Provider.DeleteBounceStat(bounceStat);

    /// <summary>Deletes a bounce stat.</summary>
    /// <param name="id">Id of the bounce stat to be deleted.</param>
    public void DeleteBounceStat(Guid id) => this.Provider.DeleteBounceStat(this.GetBounceStat(id));

    /// <summary>Creates a new message body and returns it.</summary>
    /// <returns>An instance of the newly created message body.</returns>
    public MessageBody CreateMessageBody() => this.Provider.CreateMessageBody();

    /// <summary>
    /// Creates a new message body with the specified id and returns it.
    /// </summary>
    /// <param name="messageBodyId">The id of the message body with which the message body ought to be created.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public MessageBody CreateMessageBody(Guid messageBodyId) => this.Provider.CreateMessageBody(messageBodyId);

    /// <summary>Gets the message body by it's id.</summary>
    /// <param name="messageBodyId">Id of the message body to retrieve.</param>
    /// <returns>An instance of the message body.</returns>
    public MessageBody GetMessageBody(Guid messageBodyId) => this.Provider.GetMessageBody(messageBodyId);

    /// <summary>Gets the query of message bodies.</summary>
    /// <returns>A query of message bodies</returns>
    public IQueryable<MessageBody> GetMessageBodies() => this.Provider.GetMessageBodies();

    /// <summary>Deletes the message body.</summary>
    /// <param name="messageBodyId">The id of the message body to be deleted.</param>
    public void DeleteMessageBody(Guid messageBodyId) => this.DeleteMessageBody(this.GetMessageBody(messageBodyId));

    /// <summary>Deletes the message body.</summary>
    /// <param name="messageBody">The message body to be deleted.</param>
    public void DeleteMessageBody(MessageBody messageBody)
    {
      IQueryable<MailingList> mailingLists1 = this.GetMailingLists();
      Expression<Func<MailingList, bool>> predicate1 = (Expression<Func<MailingList, bool>>) (ml => ml.SendWelcomeMessage == true && ml.WelcomeTemplate.Id == messageBody.Id);
      foreach (MailingList mailingList in (IEnumerable<MailingList>) mailingLists1.Where<MailingList>(predicate1))
      {
        mailingList.SendWelcomeMessage = false;
        mailingList.WelcomeTemplate = (MessageBody) null;
        mailingList.WelcomeMessageSubject = string.Empty;
        mailingList.WelcomeMessageEmailAddress = string.Empty;
      }
      IQueryable<MailingList> mailingLists2 = this.GetMailingLists();
      Expression<Func<MailingList, bool>> predicate2 = (Expression<Func<MailingList, bool>>) (ml => ml.SendGoodByeMessage == true && ml.GoodByeTemplate.Id == messageBody.Id);
      foreach (MailingList mailingList in (IEnumerable<MailingList>) mailingLists2.Where<MailingList>(predicate2))
      {
        mailingList.SendGoodByeMessage = false;
        mailingList.GoodByeTemplate = (MessageBody) null;
        mailingList.GoodByeMessageSubject = string.Empty;
        mailingList.GoodByeMessageEmailAddress = string.Empty;
      }
      this.Provider.DeleteMessageBody(messageBody);
    }

    /// <summary>
    /// Copies the message body.
    /// Note: This method may create or modify page data in another transaction for InternalPage body types.
    /// It will SaveChanges internally.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    internal void CopyMessageBody(MessageBody source, MessageBody target) => this.provider.CopyMessageBody(source, target);

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="campaign">The campaign for which the messages ought to be sent.</param>
    /// <param name="testEmailAddresses"></param>
    public void SendTestMessageForCampaign(Guid campaignId, string[] testEmailAddresses) => this.SendTestMessageForCampaignInternal(this.GetCampaigns().Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.Id == campaignId)).Single<Campaign>(), testEmailAddresses);

    /// <summary>
    /// Sends a test message for a given campaign to the specified email addresses.
    /// </summary>
    /// <param name="campaign">The campaign for which the messages ought to be sent.</param>
    /// <param name="testEmailAddresses"></param>
    public void SendTestMessageForCampaign(Campaign campaign, string[] testEmailAddresses) => this.SendTestMessageForCampaignInternal(campaign, testEmailAddresses);

    private void SendTestMessageForCampaignInternal(Campaign campaign, string[] testEmailAddresses)
    {
      NewsletterValidator.VerifySmtpSettings();
      SystemManager.CurrentHttpContext.Items[(object) "IsBackendRequest"] = (object) true;
      RawMessageSource rawMessage = new RawMessageSource(campaign.MessageBody, NewslettersManager.GetRootUrl(), true);
      MergeContextItems mergeContextItems = Composer.ResolveUnsubscribeLink(campaign, "{|Subscriber.ResolveKey|}");
      foreach (string testEmailAddress in testEmailAddresses)
      {
        Subscriber subscriber = new Subscriber()
        {
          FirstName = "First name",
          LastName = "Last name",
          Email = testEmailAddress
        };
        object[] objArray = new object[4]
        {
          (object) campaign,
          (object) campaign.List,
          (object) subscriber,
          (object) mergeContextItems
        };
        using (MailMessage message = Composer.ComposeMessage(campaign.ReplyToEmail, subscriber, campaign.MessageSubject, rawMessage, objArray))
        {
          try
          {
            NewslettersManager.SendMessageViaNotificationsService(Composer.GetMessagePlainTextBody(message), Composer.GetMessageHtmlBody(message), message.Subject, campaign.Name, campaign.Id, message.From.Address, message.From.DisplayName, (IEnumerable<Subscriber>) new Subscriber[1]
            {
              subscriber
            }, (Dictionary<string, string>) null);
          }
          catch (Exception ex)
          {
            Log.Write((object) ex);
          }
        }
      }
    }

    /// <summary>Sends the campaign.</summary>
    /// <param name="campaignId">Id of the campaign to be sent.</param>
    /// <param name="tooManySubscribers">Set to true if there more subscribers in the system than the license permits</param>
    [Obsolete("Sitefinity sends issues, not whole campaigns. Use SendIssue instead.")]
    public void SendCampaign(Guid campaignId, out bool tooManySubscribers) => this.SendCampaignAsync(campaignId, out tooManySubscribers);

    /// <summary>Sends a campaign (or an issue).</summary>
    /// <param name="campaignId">Id of the campaign or issue to be sent.</param>
    /// <param name="tooManySubscribers">Set to true if there more subscribers in the system than the license permits</param>
    private void SendCampaignAsync(Guid campaignId, out bool tooManySubscribers)
    {
      tooManySubscribers = this.HasTooManySubscribers();
      if (tooManySubscribers)
        return;
      NewsletterValidator.VerifySmtpSettings();
      Campaign campaign = this.GetCampaign(campaignId);
      if (campaign.CampaignState != CampaignState.ABTest && campaign.CampaignState != CampaignState.CompletedABTest)
        campaign.CampaignState = CampaignState.Sending;
      campaign.DeliveryDate = DateTime.UtcNow;
      this.SaveChanges();
      NewslettersManager.RunInBackground((Action) (() => NewslettersManager.SendCampaignWorker(campaignId, this.Provider.Name)), "Newsletter: Failed send campaign: {0}");
    }

    /// <summary>Sends the issue.</summary>
    /// <param name="issueId">Id of the issue to be sent.</param>
    /// <param name="tooManySubscribers">Set to true if there more subscribers in the system than the license permits</param>
    public void SendIssue(Guid issueId, out bool tooManySubscribers)
    {
      Campaign issue = this.GetIssue(issueId);
      if (issue == null || issue.RootCampaign == null)
        throw new ArgumentException(string.Format("Issue with ID \"{0}\" has no root campaign", (object) issueId.ToString()));
      if (issue.CampaignState == CampaignState.Sending || issue.CampaignState == CampaignState.Completed)
        throw new System.InvalidOperationException(Res.Get<NewslettersResources>().IssueAlreadySent);
      this.SendCampaignAsync(issueId, out tooManySubscribers);
    }

    /// <summary>Sends the campaign while blocking the current thread.</summary>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="tooManySubscribers">if set to <c>true</c> [too many subscribers].</param>
    internal void SendCampaignSynchronized(Guid campaignId, out bool tooManySubscribers)
    {
      tooManySubscribers = this.HasTooManySubscribers();
      if (tooManySubscribers)
        return;
      NewsletterValidator.VerifySmtpSettings();
      Campaign campaign = this.GetCampaign(campaignId);
      if (campaign.CampaignState != CampaignState.ABTest && campaign.CampaignState != CampaignState.CompletedABTest)
        campaign.CampaignState = CampaignState.Sending;
      campaign.DeliveryDate = DateTime.UtcNow;
      this.SaveChanges();
      NewslettersManager.SendCampaignWorker(campaignId, this.Provider.Name);
    }

    public virtual void OnSendCampaignCompleted(AsyncCompletedEventArgs e)
    {
      if (this.SendCampaignCompleted == null)
        return;
      this.SendCampaignCompleted((object) this, e);
    }

    public event AsyncCompletedEventHandler SendCampaignCompleted;

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.</returns>
    public LinkClickStat CreateLinkClickStat() => this.Provider.CreateLinkClickStat();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> objects.</returns>
    public IQueryable<LinkClickStat> GetLinkClickStats() => this.Provider.GetLinkClickStats();

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.</returns>
    public OpenStat CreateOpenStat() => this.Provider.CreateOpenStat();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> objects.</returns>
    public IQueryable<OpenStat> GetOpenStats() => this.Provider.GetOpenStats();

    /// <summary>Gets the total number of active subscriptions.</summary>
    /// <returns>Number of active subscriptions.</returns>
    public int CalculateTotalNumberOfSubscriptions() => this.Provider.GetSubscriptionInfos().Count<SubscriptionInfo>() - this.Provider.GetUnsubscriptionInfos().Count<UnsubscriptionInfo>();

    /// <summary>Gets the total number of new subscriptions today.</summary>
    /// <returns>Number of new subscriptions today.</returns>
    public int CalculateNewSubscriptionsToday()
    {
      DateTime today = DateTime.Now.BeginningToday();
      return this.Provider.GetSubscriptionInfos().Where<SubscriptionInfo>((Expression<Func<SubscriptionInfo, bool>>) (s => s.SubscriptionDate >= today)).Count<SubscriptionInfo>();
    }

    /// <summary>Gets the total number of new subscriptions this week.</summary>
    /// <returns>Number of new subscriptions this week.</returns>
    public int CalculateNewSubscriptionsThisWeek()
    {
      DateTime thisWeek = DateTime.Now.BeginningThisWeek();
      return this.Provider.GetSubscriptionInfos().Where<SubscriptionInfo>((Expression<Func<SubscriptionInfo, bool>>) (s => s.SubscriptionDate >= thisWeek)).Count<SubscriptionInfo>();
    }

    /// <summary>
    /// Gets the total number of new subscriptions this month.
    /// </summary>
    /// <returns>Number of new subscriptions this month.</returns>
    public int CalculateNewSubscriptionsThisMonth()
    {
      DateTime thisMonth = DateTime.Now.BeginningThisMonth();
      return this.Provider.GetSubscriptionInfos().Where<SubscriptionInfo>((Expression<Func<SubscriptionInfo, bool>>) (s => s.SubscriptionDate >= thisMonth)).Count<SubscriptionInfo>();
    }

    /// <summary>Gets the total number of unsubscriptions today.</summary>
    /// <returns>Number of unsubscriptions today.</returns>
    public int CalculateUnsubscriptionsToday()
    {
      DateTime today = DateTime.Now.BeginningToday();
      return this.Provider.GetUnsubscriptionInfos().Where<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (s => s.UnsubscriptionDate >= today)).Count<UnsubscriptionInfo>();
    }

    /// <summary>Gets the total number of unsubscriptions this week.</summary>
    /// <returns>Number of unsubscriptions this week.</returns>
    public int CalculateUnsubscriptionsThisWeek()
    {
      DateTime thisWeek = DateTime.Now.BeginningThisWeek();
      return this.Provider.GetUnsubscriptionInfos().Where<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (s => s.UnsubscriptionDate >= thisWeek)).Count<UnsubscriptionInfo>();
    }

    /// <summary>Gets the total number of unsubscriptions this month.</summary>
    /// <returns>Number of unsubscriptions this month.</returns>
    public int CalculateUnsubscriptionsThisMonth()
    {
      DateTime thisMonth = DateTime.Now.BeginningThisMonth();
      return this.Provider.GetUnsubscriptionInfos().Where<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (s => s.UnsubscriptionDate >= thisMonth)).Count<UnsubscriptionInfo>();
    }

    /// <summary>
    /// Gets the total number of unique clicks (by a subscriber).
    /// </summary>
    /// <param name="campaignId">
    /// Id of the campaign for which the unique clicks ought to be retrieved.
    /// </param>
    /// <returns>Total number of unique clicks.</returns>
    public int CalculateUniqueNumberOfClicks(Guid campaignId) => this.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (l => l.CampaignId == campaignId)).GroupBy<LinkClickStat, Guid>((Expression<Func<LinkClickStat, Guid>>) (x => x.SubscriberId)).Count<IGrouping<Guid, LinkClickStat>>();

    /// <summary>
    /// Calculates the total number of clicks made for a given campaign.
    /// </summary>
    /// <param name="campaignId">Id of the campaign for which the clicks ought to be calculated.</param>
    /// <returns>A total number of the clicks for the specified campaign.</returns>
    public int CalculateTotalNumberOfClicks(Guid campaignId) => this.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (l => l.CampaignId == campaignId)).Count<LinkClickStat>();

    /// <summary>
    /// Calculates the date and the time of the last click that has been made in the specified
    /// campaign.
    /// </summary>
    /// <param name="campaignId">
    /// Id of the campaign for which the date and time of the last click ought to be
    /// calculated.
    /// </param>
    /// <returns>The date and the time of the last click.</returns>
    public DateTime CalculateTheDateTimeOfTheLastClick(Guid campaignId) => this.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (l => l.CampaignId == campaignId)).OrderByDescending<LinkClickStat, DateTime>((Expression<Func<LinkClickStat, DateTime>>) (l => l.DateTimeClicked)).First<LinkClickStat>().DateTimeClicked;

    public IList<MergeTag> GetMergeTags() => (IList<MergeTag>) new List<MergeTag>()
    {
      new MergeTag("First name", "FirstName", typeof (Subscriber).Name),
      new MergeTag("Last name", "LastName", typeof (Subscriber).Name),
      new MergeTag("Subscriber email", "Email", typeof (Subscriber).Name),
      new MergeTag("Subscription reminder", "SubscriptionReminder", typeof (MailingList).Name),
      new MergeTag("Issue subject", "MessageSubject", typeof (Campaign).Name),
      new MergeTag("Issue name", "Name", typeof (Campaign).Name),
      new MergeTag("From name (issue)", "FromName", typeof (Campaign).Name),
      new MergeTag("Reply to email (issue)", "ReplyToEmail", typeof (Campaign).Name),
      new MergeTag("Mailing list title", "Title", typeof (MailingList).Name),
      new MergeTag("Mailing list subject", "DefaultSubject", typeof (MailingList).Name),
      new MergeTag("From name (mailing list)", "DefaultFromName", typeof (MailingList).Name),
      new MergeTag("Reply to email (mailing list)", "DefaultReplyToEmail", typeof (MailingList).Name),
      new MergeTag("Unsubscribe link", "UnsubscribeLink", typeof (MergeContextItems).Name)
    };

    public IList<MergeTag> GetMergeTags(MailingList mailingList)
    {
      List<MergeTag> mergeTags = new List<MergeTag>((IEnumerable<MergeTag>) this.GetMergeTags());
      if (mailingList == null)
        return (IList<MergeTag>) mergeTags;
      foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) mailingList.DynamicLists)
      {
        foreach (MergeTag availableProperty in (IEnumerable<MergeTag>) this.GetDynamicListProvider(dynamicList.DynamicListProviderName).GetAvailableProperties(dynamicList.ListKey))
        {
          if (!availableProperty.IsMapped(dynamicList))
            mergeTags.Add(availableProperty);
        }
      }
      return (IList<MergeTag>) mergeTags;
    }

    public PageData CreateMessageBodyPage(MessageBody messageBody) => this.provider.CreateMessageBodyPage(messageBody);

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.</returns>
    public DeliveryEntry CreateDeliveryEntry() => this.Provider.CreateDeliveryEntry();

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> objects.</returns>
    public IQueryable<DeliveryEntry> GetDeliveryEntries() => this.Provider.GetDeliveryEntries();

    /// <summary>Deletes the delivery entry.</summary>
    /// <param name="deliveryEntry">The delivery entry.</param>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    internal void DeleteDeliveryEntry(DeliveryEntry deliveryEntry)
    {
      if (!(this.Provider is OpenAccessNewslettersDataProvider provider))
        throw new System.InvalidOperationException();
      provider.DeleteDeliveryEntry(deliveryEntry);
    }

    /// <summary>Creates the issue subscriber report.</summary>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public IssueSubscriberReport CreateIssueSubscriberReport() => this.Provider.CreateIssueSubscriberReport();

    /// <summary>Creates the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public IssueSubscriberReport CreateIssueSubscriberReport(Guid reportId) => this.Provider.CreateIssueSubscriberReport(reportId);

    /// <summary>Gets the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of issue subscriber report.</returns>
    public IssueSubscriberReport GetIssueSubscriberReport(Guid reportId) => this.Provider.GetIssueSubscriberReport(reportId);

    /// <summary>Gets the issue subscriber reports.</summary>
    /// <returns>A query of issue subscriber reports.</returns>
    public IQueryable<IssueSubscriberReport> GetIssueSubscriberReports() => this.Provider.GetIssueSubscriberReports();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.UnsubscriptionInfo" /> type.
    /// </summary>
    /// <returns>A newly created instance of unsubscription info.</returns>
    public UnsubscriptionInfo CreateUnsubscriptionInfo() => this.Provider.CreateUnsubscriptionInfo();

    /// <summary>
    /// Gets a query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.UnsubscriptionInfo" /> objects.
    /// </summary>
    /// <returns>A query of unsubscription info objects.</returns>
    public IQueryable<UnsubscriptionInfo> GetUnsubscriptionInfos() => this.Provider.GetUnsubscriptionInfos();

    /// <summary>Gets the newsletters config.</summary>
    /// <returns>The newsletters config</returns>
    protected NewslettersConfig GetNewslettersConfig() => Config.Get<NewslettersConfig>();

    private static void SendCampaignWorker(Guid campaignId, string providerName)
    {
      NewslettersManager manager = (NewslettersManager) null;
      Campaign campaign = (Campaign) null;
      try
      {
        string rootUrl = NewslettersManager.GetRootUrl();
        manager = NewslettersManager.GetManager(providerName);
        campaign = manager.GetCampaign(campaignId);
        if (campaign.List == null)
          throw new ArgumentException(Res.Get<NewslettersResources>().MissingMailingList);
        if (campaign.RootCampaign != null)
          manager.CopyMessageBody(campaign.MessageBody, campaign.RootCampaign.MessageBody);
        foreach (string allLinkUrl in (IEnumerable<string>) MessageEnhancer.GetAllLinkUrls(campaign.MessageBody))
        {
          string campaignLinkUrl = allLinkUrl;
          if (!manager.GetCampaignLinks().Any<CampaignLink>((Expression<Func<CampaignLink, bool>>) (cl => cl.Url == campaignLinkUrl)))
          {
            CampaignLink campaignLink = manager.CreateCampaignLink();
            campaignLink.Url = campaignLinkUrl;
            campaignLink.Campaign = campaign;
          }
        }
        RawMessageSource rawMessage = new RawMessageSource(campaign.MessageBody, rootUrl);
        IOrderedQueryable<Subscriber> orderedQueryable = manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(campaign.List) && sub.IsSuspended == false)).OrderBy<Subscriber, Guid>((Expression<Func<Subscriber, Guid>>) (sub => sub.Id));
        double num = Math.Ceiling((double) orderedQueryable.Count<Subscriber>() / 200.0);
        for (int index = 0; (double) index < num; ++index)
        {
          foreach (Subscriber subscriber in (IEnumerable<Subscriber>) orderedQueryable.Skip<Subscriber>(index * 200).Take<Subscriber>(200))
          {
            DeliveryEntry deliveryEntry = manager.CreateDeliveryEntry();
            deliveryEntry.CampaignId = campaign.Id;
            deliveryEntry.DeliveryDate = DateTime.UtcNow;
            deliveryEntry.DeliveryStatus = DeliveryStatus.Pending;
            deliveryEntry.DeliverySubscriber = subscriber;
          }
          manager.SaveChanges();
        }
        List<Subscriber> dynamicSubscribers = NewslettersManager.GetDynamicSubscribers(manager, campaign);
        dynamicSubscribers.AddRange((IEnumerable<Subscriber>) orderedQueryable);
        NewslettersManager.SendCampaignViaNotificationsService(rawMessage, campaign, dynamicSubscribers.Distinct<Subscriber>());
        manager.SaveChanges();
        NewslettersManager.SetNotificationsBackgroundStatsCollection(campaign.Id, manager.Provider.Name);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("Newsletter: Failed send campaign '{0}': {1}", campaign == null ? (object) "Null-campaign" : (object) campaign.Name, (object) ex));
      }
      finally
      {
        manager?.Dispose();
      }
    }

    private static List<Subscriber> GetDynamicSubscribers(
      NewslettersManager manager,
      Campaign campaign)
    {
      List<Subscriber> dynamicSubscribers = new List<Subscriber>();
      foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) campaign.List.DynamicLists)
      {
        foreach (object subscriber1 in manager.GetDynamicListProvider(dynamicList.DynamicListProviderName).GetSubscribers(dynamicList.ListKey))
        {
          Subscriber subscriber2 = new Subscriber();
          subscriber2.Id = Guid.Empty;
          subscriber2.ShortId = "000000";
          MergeTag mergeTag1 = new MergeTag(dynamicList.FirstNameMappedField);
          object obj1 = TypeDescriptor.GetProperties(subscriber1)[mergeTag1.PropertyName].GetValue(subscriber1);
          subscriber2.FirstName = obj1 == null ? "First name" : obj1.ToString();
          MergeTag mergeTag2 = new MergeTag(dynamicList.LastNameMappedField);
          object obj2 = TypeDescriptor.GetProperties(subscriber1)[mergeTag2.PropertyName].GetValue(subscriber1);
          subscriber2.LastName = obj2 == null ? "Last name" : obj2.ToString();
          MergeTag mergeTag3 = new MergeTag(dynamicList.EmailMappedField);
          object obj3 = TypeDescriptor.GetProperties(subscriber1)[mergeTag3.PropertyName].GetValue(subscriber1);
          if (obj3 != null)
          {
            subscriber2.Email = obj3.ToString();
            dynamicSubscribers.Add(subscriber2);
          }
        }
      }
      return dynamicSubscribers;
    }

    private static ISenderProfile GetSenderProfile()
    {
      INotificationService notificationService = SystemManager.GetNotificationService();
      string newslettersNotificationProfileName = SystemManager.CurrentContext.GetSetting<NewslettersSettingsContract, INewslettersSettings>().NotificationsSmtpProfile;
      return notificationService.GetSenderProfiles((QueryParameters) null).FirstOrDefault<ISenderProfile>((Func<ISenderProfile, bool>) (profile => profile.ProfileName == newslettersNotificationProfileName)) ?? notificationService.GetDefaultSenderProfile(NewslettersModule.GetServiceContext(), "smtp");
    }

    /// <summary>Sends a campaign via notifications service.</summary>
    /// <remarks>Modifies the Campaign object. You should SaveChanges on its manager.</remarks>
    /// <param name="rawMessage">The raw message.</param>
    /// <param name="campaign">The campaign.</param>
    /// <param name="subscribers">The subscribers.</param>
    internal static void SendCampaignViaNotificationsService(
      RawMessageSource rawMessage,
      Campaign campaign,
      IEnumerable<Subscriber> subscribers)
    {
      MergeContextItems mergeContextItems = Composer.ResolveUnsubscribeLink(campaign, "{|Subscriber.ResolveKey|}");
      rawMessage.EnhanceLinks(campaign, "{|Subscriber.ShortId|}");
      string source = Merger.MergeMatchedTagsOnly(rawMessage.Source, (object) campaign, (object) campaign.List, (object) mergeContextItems);
      if (!string.IsNullOrEmpty(rawMessage.OpeningTracker))
        source += rawMessage.OpeningTracker;
      string messagePlainText = !string.IsNullOrEmpty(rawMessage.PlainTextSource) ? rawMessage.PlainTextSource : HtmlStripper.StripTagsRegexCompiled(source);
      string str = rawMessage.IsHtml ? source : (string) null;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!Config.Get<NewslettersConfig>().DisableSitefinityEmailHeaders)
      {
        dictionary.Add("X-Sitefinity-Subscriber", "{|Subscriber.ResolveKey|}");
        dictionary.Add("X-Sitefinity-Campaign", campaign.Id.ToString());
      }
      string messageHtml = str;
      string messageSubject = campaign.MessageSubject;
      string name = campaign.Name;
      Guid id = campaign.Id;
      string replyToEmail = campaign.ReplyToEmail;
      string fromName = campaign.FromName;
      IEnumerable<Subscriber> subscribers1 = subscribers;
      Dictionary<string, string> customMessageHeaders = dictionary;
      Guid guid = NewslettersManager.SendMessageViaNotificationsService(messagePlainText, messageHtml, messageSubject, name, id, replyToEmail, fromName, subscribers1, customMessageHeaders);
      campaign.RelatedNotificationJobId = guid;
    }

    internal static void SetNotificationsBackgroundStatsCollection(
      Guid issueId,
      string dataProviderName)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      manager.AddTask((ScheduledTask) new CollectIssueStatisticsTask(issueId, DateTime.UtcNow.AddSeconds(10.0)));
      manager.SaveChanges();
    }

    internal static void CollectStatisticsFromNotificationService(
      Guid issueId,
      string dataProviderName)
    {
      try
      {
        NewslettersManager manager = NewslettersManager.GetManager(dataProviderName);
        INotificationService notificationService = SystemManager.GetNotificationService();
        bool flag = false;
        Campaign camp = manager.GetCampaign(issueId);
        if (camp.RelatedNotificationJobId != Guid.Empty)
        {
          IMessageJobResponse messageJobResponse = notificationService.GetMessageJobs(NewslettersModule.GetServiceContext(), (QueryParameters) null).FirstOrDefault<IMessageJobResponse>((Func<IMessageJobResponse, bool>) (j => j.Id == camp.RelatedNotificationJobId));
          if (messageJobResponse != null)
          {
            Guid result1;
            Guid[] array1 = notificationService.GetUnsuccessfulSubscribers(messageJobResponse.Id).Where<string>((Func<string, bool>) (subId => subId != null && !subId.StartsWith("dynamic_"))).Select<string, Guid>((Func<string, Guid>) (subId => !Guid.TryParse(subId, out result1) ? Guid.Empty : result1)).ToArray<Guid>();
            double num1 = Math.Ceiling((double) array1.Length / 200.0);
            for (int index = 0; (double) index < num1; ++index)
            {
              IEnumerable<Guid> failedSubscribersPage = ((IEnumerable<Guid>) array1).Skip<Guid>(index * 200).Take<Guid>(200);
              IQueryable<DeliveryEntry> deliveryEntries = manager.GetDeliveryEntries();
              Expression<Func<DeliveryEntry, bool>> predicate = (Expression<Func<DeliveryEntry, bool>>) (de => de.CampaignId == issueId && failedSubscribersPage.Contains<Guid>(de.SubscriberId) && (int) de.DeliveryStatus == 2);
              foreach (DeliveryEntry deliveryEntry in deliveryEntries.Where<DeliveryEntry>(predicate).ToArray<DeliveryEntry>())
                deliveryEntry.DeliveryStatus = DeliveryStatus.Failure;
              manager.SaveChanges();
            }
            Guid result2;
            Guid[] array2 = notificationService.GetSentSubscribers(messageJobResponse.Id).Where<string>((Func<string, bool>) (subId => subId != null && !subId.StartsWith("dynamic_"))).Select<string, Guid>((Func<string, Guid>) (subId => !Guid.TryParse(subId, out result2) ? Guid.Empty : result2)).ToArray<Guid>();
            double num2 = Math.Ceiling((double) array2.Length / 200.0);
            for (int index = 0; (double) index < num2; ++index)
            {
              IEnumerable<Guid> sentSubscribersPage = ((IEnumerable<Guid>) array2).Skip<Guid>(index * 200).Take<Guid>(200);
              IQueryable<DeliveryEntry> deliveryEntries = manager.GetDeliveryEntries();
              Expression<Func<DeliveryEntry, bool>> predicate = (Expression<Func<DeliveryEntry, bool>>) (de => de.CampaignId == issueId && sentSubscribersPage.Contains<Guid>(de.SubscriberId) && (int) de.DeliveryStatus == 2);
              foreach (DeliveryEntry deliveryEntry in deliveryEntries.Where<DeliveryEntry>(predicate).ToArray<DeliveryEntry>())
                deliveryEntry.DeliveryStatus = DeliveryStatus.Success;
              manager.SaveChanges();
            }
            if (manager.GetDeliveryEntries().Count<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (entry => entry.CampaignId == issueId && (int) entry.DeliveryStatus == 2)) == 0 || messageJobResponse.JobStatus == MessageJobStatus.Failed || messageJobResponse.JobStatus == MessageJobStatus.Finished)
              flag = true;
          }
          else
          {
            flag = true;
            Guid result;
            IEnumerable<Guid> source1 = notificationService.GetUnsuccessfulSubscribers(camp.RelatedNotificationJobId).Select<string, Guid>((Func<string, Guid>) (subId => !Guid.TryParse(subId, out result) ? Guid.Empty : result));
            IQueryable<DeliveryEntry> source2 = manager.GetDeliveryEntries().Where<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (entry => entry.CampaignId == issueId && (int) entry.DeliveryStatus == 2));
            while (source2.Count<DeliveryEntry>() > 0)
            {
              foreach (DeliveryEntry deliveryEntry in source2.Take<DeliveryEntry>(200).ToList<DeliveryEntry>())
                deliveryEntry.DeliveryStatus = !source1.Contains<Guid>(deliveryEntry.SubscriberId) ? DeliveryStatus.Success : DeliveryStatus.Failure;
              manager.SaveChanges();
            }
          }
        }
        if (flag)
        {
          lock (NewslettersManager.statisticsLock)
          {
            if (camp.CampaignState != CampaignState.ABTest && camp.CampaignState != CampaignState.CompletedABTest)
            {
              camp.CampaignState = CampaignState.Completed;
            }
            else
            {
              ABCampaign abCampaign = manager.GetABCampaigns().Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => t.CampaignA == camp || t.CampaignB == camp)).FirstOrDefault<ABCampaign>();
              if (abCampaign != null && (camp == abCampaign.CampaignA && abCampaign.CampaignB.CampaignState == CampaignState.CompletedABTest || camp == abCampaign.CampaignB && abCampaign.CampaignA.CampaignState == CampaignState.CompletedABTest))
                manager.ScheduleAbTestEnd(abCampaign.Id, abCampaign.TestingEnds);
              camp.CampaignState = CampaignState.CompletedABTest;
            }
            manager.SaveChanges();
          }
          if (!Config.Get<NewslettersConfig>().TrackBouncedMessages)
            return;
          BounceCheckTask.ScheduleBounceCheck(dataProviderName);
        }
        else
          NewslettersManager.SetNotificationsBackgroundStatsCollection(issueId, dataProviderName);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("Caught exception in CollectStatisticsFromNotificationsService:\n - Type:{0}\n - Source:{1}\n - Message:{2}\n - Stack:{3}", (object) ex.GetType().FullName, (object) ex.Source, (object) ex.Message, (object) ex.StackTrace), ConfigurationPolicy.ErrorLog);
      }
    }

    /// <summary>Sends a message via notifications service.</summary>
    /// <param name="messagePlainText">The message body plain text.</param>
    /// <param name="messageHtml">The message body HTML.</param>
    /// <param name="subject">The message subject.</param>
    /// <param name="relatedItemTitle">The related campaign or mailing list title.</param>
    /// <param name="relatedItemId">The related campaign or mailing list id.</param>
    /// <param name="fromAddress">The message sender address.</param>
    /// <param name="fromName">From message sender name.</param>
    /// <param name="subscribers">The subscribers.</param>
    private static Guid SendMessageViaNotificationsService(
      string messagePlainText,
      string messageHtml,
      string subject,
      string relatedItemTitle,
      Guid relatedItemId,
      string fromAddress,
      string fromName,
      IEnumerable<Subscriber> subscribers,
      Dictionary<string, string> customMessageHeaders)
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      IQueryable<SubscriberRequestProxy> queryable = subscribers.AsQueryable<Subscriber>().Select<Subscriber, SubscriberRequestProxy>(Expression.Lambda<Func<Subscriber, SubscriberRequestProxy>>((Expression) Expression.MemberInit(Expression.New(typeof (SubscriberRequestProxy)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SubscriberRequestProxy.set_Email)), )))); //unable to render the statement
      return SystemManager.GetNotificationService().SendMessage(NewslettersModule.GetServiceContext(), (IMessageJobRequest) new MessageJobRequestProxy()
      {
        Description = string.Format("Sending message for \"{0}\", ID {1}", (object) relatedItemTitle, (object) relatedItemId.ToString()),
        MessageTemplate = (IMessageTemplateRequest) new MessageTemplateRequestProxy()
        {
          BodyHtml = messageHtml,
          PlainTextVersion = messagePlainText,
          Subject = subject
        },
        Subscribers = (IEnumerable<ISubscriberRequest>) queryable,
        SenderEmailAddress = fromAddress,
        SenderName = fromName,
        SenderProfileName = NewslettersManager.GetSenderProfile().ProfileName,
        CustomMessageHeaders = (IDictionary<string, string>) customMessageHeaders,
        ClearSubscriptionData = true
      }, (IDictionary<string, string>) null);
    }

    private static Dictionary<string, string> GetSubscriberCustomProperties(Subscriber sub)
    {
      Dictionary<string, string> customProperties = new Dictionary<string, string>()
      {
        {
          "ShortId",
          sub.ShortId
        }
      };
      if (sub.Id != Guid.Empty)
      {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) sub))
        {
          if (property.Attributes[typeof (DataMemberAttribute)] != null && property.GetValue((object) sub) != null)
            customProperties.Add(property.Name, property.GetValue((object) sub).ToString());
        }
      }
      return customProperties;
    }

    /// <summary>Gets the root URL.</summary>
    /// <returns>The root URL.</returns>
    public static string GetRootUrl() => VirtualPathUtility.AppendTrailingSlash(RouteHelper.ResolveUrl("~/", UrlResolveOptions.Absolute));

    /// <summary>
    /// Recalculates the issue subscribers reports based on which of them were affected in a transaction.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="reportsToUpdate">The updated reports.</param>
    private static void RecalculateStatistics(
      NewslettersDataProvider provider,
      IEnumerable<Guid> reportsToUpdate)
    {
      if (reportsToUpdate == null)
        return;
      foreach (Guid reportId in reportsToUpdate)
        provider.UpdateIssueSubscriberReport(reportId);
      int num = 2;
      while (num > 0)
      {
        try
        {
          if (provider.GetDirtyItems().Count > 0)
            provider.CommitTransaction();
          num = 0;
        }
        catch (OptimisticVerificationException ex)
        {
          provider.RollbackTransaction();
          --num;
        }
      }
    }

    /// <summary>
    /// Get an instance of the newsletters manager using the default provider
    /// </summary>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" />.</returns>
    public static NewslettersManager GetManager() => ManagerBase<NewslettersDataProvider>.GetManager<NewslettersManager>();

    /// <summary>
    /// Get an instance of the newsletters manager by explicitly specifying the required provider to use.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /></returns>
    public static NewslettersManager GetManager(string providerName) => ManagerBase<NewslettersDataProvider>.GetManager<NewslettersManager>(providerName);

    /// <summary>
    /// Get an instance of the manager that uses a specific provider and is part of a named transaction
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null for default provider</param>
    /// <param name="namedTransactionName">Name of the transaction to join.</param>
    /// <returns>Instance of the manager that uses a specific provider and is part of a named transaction</returns>
    public static NewslettersManager GetManager(
      string providerName,
      string namedTransactionName)
    {
      return ManagerBase<NewslettersDataProvider>.GetManager<NewslettersManager>(providerName, namedTransactionName);
    }

    public static PageManager GetPageManager() => PageManager.GetManager();

    private static void RunInBackground(Action action, string errorMessageToLog)
    {
      Guid siteId = SystemManager.CurrentContext.CurrentSite.Id;
      SystemManager.RunWithElevatedPrivilegeDelegate elevDelegate = (SystemManager.RunWithElevatedPrivilegeDelegate) (p =>
      {
        using (SiteRegion.FromSiteId(siteId))
          action();
      });
      CultureInfo uiCulture = SystemManager.CurrentContext.Culture;
      ThreadPool.QueueUserWorkItem((WaitCallback) (tmp =>
      {
        try
        {
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          SystemManager.CurrentContext.Culture = uiCulture;
          try
          {
            SystemManager.RunWithElevatedPrivilege(elevDelegate);
          }
          finally
          {
            SystemManager.CurrentContext.Culture = culture;
          }
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format(errorMessageToLog, (object) ex));
        }
      }));
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<NewslettersConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public override string ModuleName => "Newsletters";

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<NewslettersConfig>().Providers;

    internal bool HasTooManySubscribers()
    {
      int subscribersCount = LicenseState.Current.LicenseInfo.TotalNewsLettersSubscribersCount;
      if (subscribersCount == 0)
        return false;
      int totalSubscribers = this.GetTotalSubscribers();
      return subscribersCount < totalSubscribers;
    }

    internal int GetTotalSubscribers()
    {
      int num1 = this.GetSubscribers().Count<Subscriber>();
      int num2 = 0;
      List<string> stringList = new List<string>();
      foreach (MailingList mailingList in (IEnumerable<MailingList>) this.GetMailingLists())
      {
        foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) mailingList.DynamicLists)
        {
          if (!stringList.Contains(dynamicList.ListKey))
          {
            stringList.Add(dynamicList.ListKey);
            IDynamicListProvider dynamicListProvider = this.GetDynamicListProvider(dynamicList.DynamicListProviderName);
            num2 += dynamicListProvider.SubscribersCount(dynamicList.ListKey);
          }
        }
      }
      return num1 + num2;
    }

    /// <summary>
    /// Executes before flushing or committing a Forums transactions and records which forums and threads statistics(counters) should be updated
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    internal static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      NewslettersDataProvider newslettersDataProvider = sender as NewslettersDataProvider;
      bool? executionStateData = newslettersDataProvider.GetExecutionStateData("ignore-statistics") as bool?;
      if (executionStateData.HasValue && executionStateData.Value)
        return;
      IList dirtyItems = newslettersDataProvider.GetDirtyItems();
      List<Guid> data1 = new List<Guid>();
      HashSet<DeliveryEntry> data2 = new HashSet<DeliveryEntry>();
      HashSet<Guid> data3 = new HashSet<Guid>();
      for (int index = 0; index < dirtyItems.Count; ++index)
      {
        object itemInTransaction = dirtyItems[index];
        SecurityConstants.TransactionActionType dirtyItemStatus = newslettersDataProvider.GetDirtyItemStatus(itemInTransaction);
        if (itemInTransaction is MessageBody messageBody && messageBody.MessageBodyType == MessageBodyType.InternalPage && dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
          data1.Add(messageBody.Id);
        DeliveryEntry devEntry = itemInTransaction as DeliveryEntry;
        if (devEntry != null)
        {
          if (dirtyItemStatus == SecurityConstants.TransactionActionType.New && devEntry.DeliveryStatus != DeliveryStatus.Pending)
            data2.Add(devEntry);
          else if (dirtyItemStatus == SecurityConstants.TransactionActionType.Updated)
          {
            Guid guid = newslettersDataProvider.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.SubscriberId == devEntry.SubscriberId && r.IssueId == devEntry.CampaignId)).Select<IssueSubscriberReport, Guid>((Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Id)).FirstOrDefault<Guid>();
            if (guid != new Guid())
              data3.Add(guid);
            else
              data2.Add(devEntry);
          }
        }
        LinkClickStat clickStat = itemInTransaction as LinkClickStat;
        if (clickStat != null)
          data3.UnionWith((IEnumerable<Guid>) newslettersDataProvider.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.SubscriberId == clickStat.SubscriberId && r.IssueId == clickStat.CampaignId && r.HasClicked == false)).Select<IssueSubscriberReport, Guid>((Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Id)));
        if (itemInTransaction is BounceStat bounceStat)
        {
          Guid issueId = bounceStat.Campaign.Id;
          Guid subscriberId = bounceStat.Subscriber.Id;
          data3.UnionWith((IEnumerable<Guid>) newslettersDataProvider.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.IssueId == issueId && r.SubscriberId == subscriberId)).Select<IssueSubscriberReport, Guid>((Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Id)));
        }
        OpenStat openStat = itemInTransaction as OpenStat;
        if (openStat != null)
          data3.UnionWith((IEnumerable<Guid>) newslettersDataProvider.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.IssueId == openStat.CampaignId && r.SubscriberId == openStat.SubscriberId && r.DateOpened == new DateTime?())).Select<IssueSubscriberReport, Guid>((Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Id)));
        if (itemInTransaction is UnsubscriptionInfo unsubscriptionInfo && unsubscriptionInfo.Issue != null && unsubscriptionInfo.Subscriber != null)
        {
          Guid issueId = unsubscriptionInfo.Issue.Id;
          Guid subscrId = unsubscriptionInfo.Subscriber.Id;
          data3.UnionWith((IEnumerable<Guid>) newslettersDataProvider.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.IssueId == issueId && r.SubscriberId == subscrId)).Select<IssueSubscriberReport, Guid>((Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Id)));
        }
      }
      if (data1.Count > 0)
        newslettersDataProvider.SetExecutionStateData("providerExecutingMessageBodyPagesToDelete", (object) data1);
      if (data2 != null && data2.Count > 0)
        newslettersDataProvider.SetExecutionStateData("new-delivery-entries", (object) data2);
      if (data3 == null || data3.Count <= 0)
        return;
      newslettersDataProvider.SetExecutionStateData("reports-to-update", (object) data3);
    }

    /// <summary>
    /// Handles the post commit event of the Forums provider and updates the forum and thread statistics if necessary
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    internal static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      NewslettersDataProvider provider = sender as NewslettersDataProvider;
      bool? executionStateData1 = provider.GetExecutionStateData("ignore-statistics") as bool?;
      provider.SetExecutionStateData("ignore-statistics", (object) null);
      if (executionStateData1.HasValue && executionStateData1.Value)
        return;
      List<Guid> executionStateData2 = provider.GetExecutionStateData("providerExecutingMessageBodyPagesToDelete") as List<Guid>;
      provider.SetExecutionStateData("providerExecutingMessageBodyPagesToDelete", (object) null);
      if (executionStateData2 != null && executionStateData2.Count > 0)
      {
        PageManager manager = PageManager.GetManager();
        foreach (Guid guid in executionStateData2)
        {
          Guid pageId = guid;
          PageData pageData = manager.GetPageDataList().SingleOrDefault<PageData>((Expression<Func<PageData, bool>>) (p => p.Id == pageId));
          if (pageData != null)
          {
            manager.DeleteItem((object) pageData);
            manager.SaveChanges();
          }
        }
      }
      HashSet<Guid> reportsToUpdate = provider.GetExecutionStateData("reports-to-update") as HashSet<Guid>;
      HashSet<DeliveryEntry> executionStateData3 = provider.GetExecutionStateData("new-delivery-entries") as HashSet<DeliveryEntry>;
      try
      {
        provider.SetExecutionStateData("reports-to-update", (object) null);
        provider.SetExecutionStateData("new-delivery-entries", (object) null);
        if (executionStateData3 != null && executionStateData3.Count > 0)
        {
          if (reportsToUpdate == null)
            reportsToUpdate = new HashSet<Guid>();
          foreach (DeliveryEntry deliveryEntry in executionStateData3)
          {
            Campaign issue = provider.GetIssue(deliveryEntry.CampaignId);
            if (issue.CampaignState != CampaignState.ABTest && issue.CampaignState != CampaignState.CompletedABTest)
            {
              IssueSubscriberReport subscriberReport = provider.CreateIssueSubscriberReport();
              subscriberReport.Issue = issue;
              subscriberReport.SubscriberId = deliveryEntry.SubscriberId;
              reportsToUpdate.Add(subscriberReport.Id);
            }
          }
        }
        if (reportsToUpdate == null || reportsToUpdate.Count <= 0)
          return;
        NewslettersManager.RecalculateStatistics(provider, (IEnumerable<Guid>) reportsToUpdate);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
      }
    }

    private delegate void CampaignSendWorkerDelegate(
      Guid campaignId,
      string providerName,
      HttpContext context);
  }
}
