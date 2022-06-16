// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.OpenAccessNewslettersDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  /// <summary>
  /// OpenAccess implementation of the data provider for the newsletters module.
  /// </summary>
  public class OpenAccessNewslettersDataProvider : 
    NewslettersDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "Newsletters"
    };

    public override DynamicListSettings CreateDynamicListSettings()
    {
      DynamicListSettings entity = new DynamicListSettings();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public override MailingList CreateList() => this.CreateList(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the list to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public override MailingList CreateList(Guid id)
    {
      MailingList entity = new MailingList(id, this.ApplicationName);
      entity.LastModified = DateTime.UtcNow;
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the list to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" />.</returns>
    public override MailingList GetList(Guid id)
    {
      MailingList list = !(id == Guid.Empty) ? this.GetContext().GetItemById<MailingList>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      list.Provider = (object) this;
      return list;
    }

    /// <summary>Gets the query of all newsletter lists.</summary>
    /// <returns>
    /// A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> objects.
    /// </returns>
    public override IQueryable<MailingList> GetLists()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MailingList>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MailingList>((Expression<Func<MailingList, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a list.</summary>
    /// <param name="list">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> to be deleted.</param>
    public override void DeleteList(MailingList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      this.GetContext().Remove((object) list);
    }

    /// <summary>Creates a new subscriber.</summary>
    /// <param name="generateShortId">Determines weather short id for the subscriber should be generated.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.
    /// </returns>
    public override Subscriber CreateSubscriber(bool generateShortId) => this.CreateSubscriber(generateShortId, this.GetNewGuid());

    /// <summary>Creates a new subscriber with the specified id.</summary>
    /// <param name="generateShortId">Determines weather short id for the subscriber should be generated.</param>
    /// <param name="subscriberId">Id of the subscriber to be created.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.
    /// </returns>
    public override Subscriber CreateSubscriber(bool generateShortId, Guid subscriberId)
    {
      Subscriber entity = new Subscriber(subscriberId, this.ApplicationName);
      entity.DateCreated = DateTime.UtcNow;
      entity.LastModified = DateTime.UtcNow;
      entity.Provider = (object) this;
      if (generateShortId)
        entity.ShortId = SecurityManager.GetRandomKey(3);
      if (subscriberId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the subscriber.</summary>
    /// <param name="subscriberId">Id of the subscriber to be retrieved.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type.
    /// </returns>
    public override Subscriber GetSubscriber(Guid subscriberId)
    {
      Subscriber subscriber = !(subscriberId == Guid.Empty) ? this.GetContext().GetItemById<Subscriber>(subscriberId.ToString()) : throw new ArgumentException("SubscriberId cannot be Empty Guid");
      subscriber.Provider = (object) this;
      return subscriber;
    }

    /// <summary>Gets the query of the subscribers.</summary>
    /// <returns>
    /// A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> objects.
    /// </returns>
    public override IQueryable<Subscriber> GetSubscribers()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Subscriber>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Subscriber>((Expression<Func<Subscriber, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a subscriber.</summary>
    /// <param name="subscriber">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type to be deleted.</param>
    public override void DeleteSubscriber(Subscriber subscriber)
    {
      if (subscriber == null)
        throw new ArgumentNullException(nameof (subscriber));
      IQueryable<IssueSubscriberReport> subscriberReports = this.GetIssueSubscriberReports();
      Expression<Func<IssueSubscriberReport, bool>> predicate = (Expression<Func<IssueSubscriberReport, bool>>) (r => r.Subscriber == subscriber);
      foreach (IssueSubscriberReport entity in subscriberReports.Where<IssueSubscriberReport>(predicate).ToArray<IssueSubscriberReport>())
        this.GetContext().Remove((object) entity);
      this.GetContext().Remove((object) subscriber);
    }

    public override SubscriptionInfo CreateSubscriptionInfo()
    {
      SubscriptionInfo entity = new SubscriptionInfo();
      entity.ApplicationName = this.ApplicationName;
      entity.LastModified = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    public override IQueryable<SubscriptionInfo> GetSubscriptionInfos()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<SubscriptionInfo>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<SubscriptionInfo>((Expression<Func<SubscriptionInfo, bool>>) (b => b.ApplicationName == appName));
    }

    public override UnsubscriptionInfo CreateUnsubscriptionInfo()
    {
      UnsubscriptionInfo entity = new UnsubscriptionInfo();
      entity.ApplicationName = this.ApplicationName;
      entity.LastModified = DateTime.UtcNow;
      this.GetContext().Add((object) entity);
      return entity;
    }

    public override IQueryable<UnsubscriptionInfo> GetUnsubscriptionInfos()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<UnsubscriptionInfo>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<UnsubscriptionInfo>((Expression<Func<UnsubscriptionInfo, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets the query of the subscribers per mailing list.</summary>
    /// <param name="mailingListId">The mailing list Id.</param>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> objects.</returns>
    public override IQueryable<Subscriber> GetMailingListSubscribers(
      Guid mailingListId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OpenAccessNewslettersDataProvider.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new OpenAccessNewslettersDataProvider.\u003C\u003Ec__DisplayClass15_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass150.mailingListId = mailingListId;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass150.appName = this.ApplicationName;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return SitefinityQuery.Get<Subscriber>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Subscriber>((Expression<Func<Subscriber, bool>>) (b => b.ApplicationName == cDisplayClass150.appName && b.Lists.Any<MailingList>((Func<MailingList, bool>) (l => l.Id == cDisplayClass150.mailingListId))));
    }

    /// <summary>Creates a new campaign and returns it.</summary>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public override Campaign CreateCampaign(bool generateShortId) => this.CreateCampaign(generateShortId, this.GetNewGuid());

    /// <summary>
    /// Creates a new campaign with the specified id and returns it.
    /// </summary>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <param name="campaignId">Id of the campaign to be created.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public override Campaign CreateCampaign(bool generateShortId, Guid campaignId)
    {
      Campaign entity = new Campaign(campaignId, this.ApplicationName)
      {
        Provider = (object) this,
        LastModified = DateTime.UtcNow,
        DeliveryDate = DateTime.UtcNow.AddDays(7.0),
        MessageBody = new MessageBody(this.GetNewGuid(), this.ApplicationName)
        {
          LastModified = DateTime.UtcNow
        }
      };
      if (generateShortId)
        entity.ShortId = SecurityManager.GetRandomKey(3);
      if (campaignId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the campaign.</returns>
    public override Campaign GetCampaign(Guid campaignId)
    {
      Campaign campaign = !(campaignId == Guid.Empty) ? this.GetContext().GetItemById<Campaign>(campaignId.ToString()) : throw new ArgumentException("campaignId cannot be Empty Guid");
      campaign.Provider = (object) this;
      return campaign;
    }

    /// <summary>Gets the query of campaigns.</summary>
    /// <returns>A query of the specified type of the campaign.</returns>
    public override IQueryable<Campaign> GetCampaigns()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Campaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Campaign>((Expression<Func<Campaign, bool>>) (b => b.ApplicationName == appName && b.RootCampaign == default (object)));
    }

    /// <summary>Deletes a campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public override void DeleteCampaign(Campaign campaign)
    {
      Campaign[] campaignArray = campaign != null ? this.GetIssues(campaign).ToArray<Campaign>() : throw new ArgumentNullException(nameof (campaign));
      for (int index = campaignArray.Length - 1; index >= 0; --index)
        this.DeleteCampaign(campaignArray[index]);
      IQueryable<ABCampaign> abCampaigns = this.GetABCampaigns();
      Expression<Func<ABCampaign, bool>> predicate1 = (Expression<Func<ABCampaign, bool>>) (ab => ab.CampaignA.Id == campaign.Id || ab.CampaignB.Id == campaign.Id);
      foreach (ABCampaign campaign1 in (IEnumerable<ABCampaign>) abCampaigns.Where<ABCampaign>(predicate1))
        this.DeleteABCampaign(campaign1);
      IQueryable<IssueSubscriberReport> subscriberReports = this.GetIssueSubscriberReports();
      Expression<Func<IssueSubscriberReport, bool>> predicate2 = (Expression<Func<IssueSubscriberReport, bool>>) (r => r.Issue == campaign);
      foreach (IssueSubscriberReport entity in subscriberReports.Where<IssueSubscriberReport>(predicate2).ToArray<IssueSubscriberReport>())
        this.GetContext().Remove((object) entity);
      this.GetContext().Remove((object) campaign);
    }

    /// <summary>Creates a new issue and returns it.</summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the issue should be generated.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public override Campaign CreateIssue(Campaign rootCampaign, bool generateShortId) => this.CreateIssue(rootCampaign, generateShortId, this.GetNewGuid());

    /// <summary>
    /// Creates a new issue with the specified id and returns it.
    /// </summary>
    /// <param name="rootCampaign">The root campaign of this issue.</param>
    /// <param name="generateShortId">Determines weather short id for the campaign should be generated.</param>
    /// <param name="issueId">Id of the campaign to be created.</param>
    /// <returns>An instance of the newly created campaign (issue).</returns>
    public override Campaign CreateIssue(
      Campaign rootCampaign,
      bool generateShortId,
      Guid issueId)
    {
      Campaign campaign = this.CreateCampaign(generateShortId, issueId);
      Synchronizer.Synchronize(rootCampaign, campaign);
      this.CopyMessageBody(rootCampaign.MessageBody, campaign.MessageBody);
      campaign.RootCampaign = rootCampaign;
      return campaign;
    }

    /// <summary>Gets the issue by its id.</summary>
    /// <param name="issueId">Id of the issue to retrieve.</param>
    /// <returns>An instance of the campaign (issue).</returns>
    public override Campaign GetIssue(Guid issueId) => this.GetCampaign(issueId);

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaign">The campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public override Campaign GetLatestIssue(Campaign rootCampaign) => this.GetIssues(rootCampaign).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState != 6 && (int) i.CampaignState != 7)).OrderByDescending<Campaign, DateTime>((Expression<Func<Campaign, DateTime>>) (issue => issue.DeliveryDate)).FirstOrDefault<Campaign>();

    /// <summary>Gets the latest issue of the campaign.</summary>
    /// <param name="rootCampaignId">The id of the root campaign to get the latest issue for.</param>
    /// <returns>The latest issue of the campaign</returns>
    public override Campaign GetLatestIssue(Guid rootCampaignId) => this.GetIssues(rootCampaignId).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState != 6 && (int) i.CampaignState != 7)).OrderByDescending<Campaign, DateTime>((Expression<Func<Campaign, DateTime>>) (issue => issue.DeliveryDate)).FirstOrDefault<Campaign>();

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="issueId">Id of the campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public override IQueryable<Campaign> GetIssues(Guid rootCampaignId)
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Campaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Campaign>((Expression<Func<Campaign, bool>>) (b => b.ApplicationName == appName && b.RootCampaign != default (object) && b.RootCampaign.Id == rootCampaignId));
    }

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <param name="rootCampaign">The campaign for which to retrive the issues.</param>
    /// <returns>A query of the campaign's issues.</returns>
    public override IQueryable<Campaign> GetIssues(Campaign rootCampaign)
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Campaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Campaign>((Expression<Func<Campaign, bool>>) (b => b.ApplicationName == appName && b.RootCampaign == rootCampaign));
    }

    /// <summary>Gets the query of  issues of a specific campaign.</summary>
    /// <returns>A query of the campaign's issues.</returns>
    public override IQueryable<Campaign> GetIssues()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Campaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<Campaign>((Expression<Func<Campaign, bool>>) (b => b.ApplicationName == appName && b.RootCampaign != default (object)));
    }

    /// <summary>Creates a new A/B campaign and returns it.</summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public override ABCampaign CreateABCampaign() => this.CreateABCampaign(this.GetNewGuid());

    /// <summary>
    /// Creates a new A/B campaign with the specified id and returns it.
    /// </summary>
    /// <returns>An instance of the newly created campaign.</returns>
    public override ABCampaign CreateABCampaign(Guid campaignId)
    {
      ABCampaign entity = new ABCampaign(campaignId, this.ApplicationName)
      {
        Provider = (object) this,
        LastModified = DateTime.UtcNow
      };
      if (campaignId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the A/B campaign by its id.</summary>
    /// <param name="campaignId">Id of the campaign to retrieve.</param>
    /// <returns>An instance of the A/B campaign.</returns>
    public override ABCampaign GetABCampaign(Guid campaignId)
    {
      ABCampaign abCampaign = !(campaignId == Guid.Empty) ? this.GetContext().GetItemById<ABCampaign>(campaignId.ToString()) : throw new ArgumentException("campaignId cannot be Empty Guid");
      abCampaign.Provider = (object) this;
      return abCampaign;
    }

    /// <summary>Gets the query of A/B campaigns.</summary>
    /// <returns>A query of campaigns.</returns>
    public override IQueryable<ABCampaign> GetABCampaigns()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ABCampaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Gets the query of A/B campaigns for a given root campaign.
    /// </summary>
    /// <param name="rootCampaignId">Id of the root campaign.</param>
    /// <returns>A query of campaigns.</returns>
    public override IQueryable<ABCampaign> GetABCampaigns(Guid rootCampaignId)
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ABCampaign>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (b => b.ApplicationName == appName && b.RootCampaign.Id == rootCampaignId));
    }

    /// <summary>Deletes an A/B campaign.</summary>
    /// <param name="campaign">An instance of the campaign to be deleted.</param>
    public override void DeleteABCampaign(ABCampaign campaign)
    {
      if (campaign == null)
        throw new ArgumentNullException(nameof (campaign));
      this.GetContext().Remove((object) campaign);
    }

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public override CampaignLink CreateCampaignLink() => this.CreateCampaignLink(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the campaign link to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public override CampaignLink CreateCampaignLink(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      CampaignLink entity = new CampaignLink(id, this.ApplicationName)
      {
        Provider = (object) this,
        LastModified = DateTime.UtcNow
      };
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the campaign link to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" />.</returns>
    public override CampaignLink GetCampaignLink(Guid id)
    {
      CampaignLink campaignLink = !(id == Guid.Empty) ? this.GetContext().GetItemById<CampaignLink>(id.ToString()) : throw new ArgumentException("id cannot be Empty Guid");
      campaignLink.Provider = (object) this;
      return campaignLink;
    }

    /// <summary>Gets the query of all campaign links.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> objects.</returns>
    public override IQueryable<CampaignLink> GetCampaignLinks()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<CampaignLink>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<CampaignLink>((Expression<Func<CampaignLink, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a campaign link.</summary>
    /// <param name="campaignLink">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.CampaignLink" /> to be deleted.</param>
    public override void DeleteCampaignLink(CampaignLink campaignLink)
    {
      if (campaignLink == null)
        throw new ArgumentNullException(nameof (campaignLink));
      this.GetContext().Remove((object) campaignLink);
    }

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public override BounceStat CreateBounceStat() => this.CreateBounceStat(this.GetNewGuid());

    /// <summary>
    /// Creates a new instance of the persistent <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> class with the specified
    /// primary key.
    /// </summary>
    /// <param name="id">The primary key of the bounce stat to be created.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public override BounceStat CreateBounceStat(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      BounceStat entity = new BounceStat(id, this.ApplicationName)
      {
        Provider = (object) this,
        LastModified = DateTime.UtcNow
      };
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> by the specified id.
    /// </summary>
    /// <param name="id">Id of the bounce stat to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" />.</returns>
    public override BounceStat GetBounceStat(Guid id)
    {
      BounceStat bounceStat = !(id == Guid.Empty) ? this.GetContext().GetItemById<BounceStat>(id.ToString()) : throw new ArgumentException("id cannot be Empty Guid");
      bounceStat.Provider = (object) this;
      return bounceStat;
    }

    /// <summary>Gets the query of all bounce stats.</summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> objects.</returns>
    public override IQueryable<BounceStat> GetBounceStats()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<BounceStat>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<BounceStat>((Expression<Func<BounceStat, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes a bounce stat.</summary>
    /// <param name="bounceStat">An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.BounceStat" /> to be deleted.</param>
    public override void DeleteBounceStat(BounceStat bounceStat)
    {
      if (bounceStat == null)
        throw new ArgumentNullException(nameof (bounceStat));
      this.GetContext().Remove((object) bounceStat);
    }

    /// <summary>Creates a new message body and returns it.</summary>
    /// <returns>An instance of the newly created message body.</returns>
    public override MessageBody CreateMessageBody() => this.CreateMessageBody(this.GetNewGuid());

    /// <summary>
    /// Creates a new message body with the specified id and returns it.
    /// </summary>
    /// <param name="messageBodyId">The id of the message body with which the message body ought to be created.</param>
    /// <returns>An instance of the newly created campaign.</returns>
    public override MessageBody CreateMessageBody(Guid messageBodyId)
    {
      MessageBody entity = new MessageBody(messageBodyId, this.ApplicationName)
      {
        Provider = (object) this,
        LastModified = DateTime.UtcNow
      };
      if (messageBodyId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the message body by it's id.</summary>
    /// <param name="messageBodyId">Id of the message body to retrieve.</param>
    /// <returns>An instance of the message body.</returns>
    public override MessageBody GetMessageBody(Guid messageBodyId)
    {
      MessageBody messageBody = !(messageBodyId == Guid.Empty) ? this.GetContext().GetItemById<MessageBody>(messageBodyId.ToString()) : throw new ArgumentException("messageBodyId cannot be Empty Guid");
      messageBody.Provider = (object) this;
      return messageBody;
    }

    /// <summary>Gets the query of message bodies.</summary>
    /// <returns>A query of message bodies</returns>
    public override IQueryable<MessageBody> GetMessageBodies()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MessageBody>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MessageBody>((Expression<Func<MessageBody, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Deletes the message body.</summary>
    /// <param name="messageBody">The message body to be deleted.</param>
    public override void DeleteMessageBody(MessageBody messageBody)
    {
      if (messageBody == null)
        throw new ArgumentNullException(nameof (messageBody));
      this.GetContext().Remove((object) messageBody);
    }

    /// <summary>Copies the message body to another message body.</summary>
    /// <param name="source">The source message body.</param>
    /// <param name="target">The target message body.</param>
    public override void CopyMessageBody(MessageBody source, MessageBody target)
    {
      bool isInTransaction = false;
      target.BodyText = source.BodyText;
      target.IsTemplate = source.IsTemplate;
      target.MessageBodyType = source.MessageBodyType;
      target.PlainTextVersion = source.PlainTextVersion;
      target.CopiedTemplateId = source.CopiedTemplateId;
      if (target.MessageBodyType != MessageBodyType.InternalPage)
        return;
      PageManager pageManagerInstance = this.GetPageManagerInstance(out isInTransaction);
      PageData pageData = pageManagerInstance.GetPageData(source.Id);
      PageData targetPage = pageManagerInstance.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Id == target.Id)).SingleOrDefault<PageData>() ?? this.CreateMessageBodyPage(target);
      pageManagerInstance.CopyPageData(pageData, targetPage);
      if (isInTransaction)
        return;
      pageManagerInstance.SaveChanges();
    }

    /// <summary>Creates the message body page.</summary>
    /// <param name="messageBody">The message body.</param>
    /// <returns>The related page.</returns>
    public override PageData CreateMessageBodyPage(MessageBody messageBody)
    {
      bool isInTransaction = false;
      PageManager pageManagerInstance = this.GetPageManagerInstance(out isInTransaction);
      Guid id = messageBody.Id;
      IQueryable<PageNode> source = pageManagerInstance.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == id));
      if (source.Count<PageNode>() > 0)
        return source.First<PageNode>().GetPageData();
      PageNode pageNode = pageManagerInstance.CreatePageNode(id);
      pageNode.RootNode = this.GetStandardCampaignRootNode();
      pageNode.SupportedPermissionSets = new string[1]
      {
        "Pages"
      };
      this.AllowAllActionsToEveryone(pageNode, pageManagerInstance);
      PageData pageData = pageManagerInstance.CreatePageData(id);
      if (messageBody.InternalPageTemplateId != Guid.Empty)
        pageData.Template = pageManagerInstance.GetTemplate(messageBody.InternalPageTemplateId);
      pageData.NavigationNode = pageNode;
      if (!isInTransaction)
        pageManagerInstance.SaveChanges();
      return pageData;
    }

    /// <summary>Gets the standard campaign pages root node.</summary>
    /// <returns>The standard campaign pages root node</returns>
    public override PageNode GetStandardCampaignRootNode()
    {
      bool isInTransaction = false;
      PageManager pageManagerInstance = this.GetPageManagerInstance(out isInTransaction);
      bool suppressSecurityChecks = pageManagerInstance.Provider.SuppressSecurityChecks;
      pageManagerInstance.Provider.SuppressSecurityChecks = true;
      PageNode campaignRootNode = pageManagerInstance.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == NewslettersModule.standardCampaignRootNodeId)).SingleOrDefault<PageNode>();
      if (campaignRootNode == null)
      {
        campaignRootNode = pageManagerInstance.CreatePageNode(NewslettersModule.standardCampaignRootNodeId);
        pageManagerInstance.SaveChanges();
        pageManagerInstance.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      return campaignRootNode;
    }

    private void AllowAllActionsToEveryone(PageNode myPage, PageManager pManager)
    {
      Guid id = Config.Get<SecurityConfig>().ApplicationRoles["Everyone"].Id;
      Telerik.Sitefinity.Security.Model.Permission permission1 = (Telerik.Sitefinity.Security.Model.Permission) null;
      if (myPage == null)
        return;
      pManager.BreakPermiossionsInheritance((ISecuredObject) myPage);
      foreach (Telerik.Sitefinity.Security.Model.Permission permission2 in myPage.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.ObjectId == myPage.Id && p.SetName == "Pages")))
      {
        if (permission2.PrincipalId == id)
        {
          permission1 = permission2;
        }
        else
        {
          permission2.Grant = 0;
          permission2.Deny = 0;
        }
      }
      if (permission1 == null)
      {
        permission1 = pManager.CreatePermission("Pages", myPage.Id, id);
        myPage.Permissions.Add(permission1);
      }
      foreach (SecurityAction action in (ConfigElementCollection) Config.Get<SecurityConfig>().Permissions["Pages"].Actions)
        permission1.GrantActions(true, action.Name);
    }

    /// <summary>
    /// Gets an instance of page manager, either in the current transaction (if there is one) or seprartely (if there isn't one).
    /// </summary>
    /// <param name="isInTransaction">Set to true if the current manager instance is in a transaction</param>
    /// <returns>An instance of page manager, either in the current transaction or seprartely.</returns>
    private PageManager GetPageManagerInstance(out bool isInTransaction)
    {
      isInTransaction = !string.IsNullOrWhiteSpace(this.TransactionName);
      return !isInTransaction ? PageManager.GetManager() : (PageManager) ManagerBase.GetManagerInTransaction(typeof (PageManager), "", this.TransactionName);
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" />.
    /// </returns>
    public override LinkClickStat CreateLinkClickStat()
    {
      LinkClickStat entity = new LinkClickStat();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> object.
    /// </summary>
    /// <returns>
    /// A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.LinkClickStat" /> objects.
    /// </returns>
    public override IQueryable<LinkClickStat> GetLinkClickStats() => this.GetContext().GetAll<LinkClickStat>();

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" />.</returns>
    public override DeliveryEntry CreateDeliveryEntry()
    {
      DeliveryEntry entity = new DeliveryEntry();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> object.
    /// </summary>
    /// <returns>A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DeliveryEntry" /> objects.</returns>
    public override IQueryable<DeliveryEntry> GetDeliveryEntries() => this.GetContext().GetAll<DeliveryEntry>();

    /// <summary>Deletes the delivery entry.</summary>
    /// <param name="deliveryEntry">The delivery entry.</param>
    internal void DeleteDeliveryEntry(DeliveryEntry deliveryEntry)
    {
      if (deliveryEntry == null)
        throw new ArgumentNullException(nameof (deliveryEntry));
      this.GetContext().Remove((object) deliveryEntry);
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" />.
    /// </returns>
    public override OpenStat CreateOpenStat()
    {
      OpenStat entity = new OpenStat();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" /> object.
    /// </summary>
    /// <returns>
    /// A query of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.OpenStat" /> objects.
    /// </returns>
    public override IQueryable<OpenStat> GetOpenStats() => (IQueryable<OpenStat>) this.GetContext().Scope.Extent<OpenStat>();

    /// <summary>Creates the issue subscriber report.</summary>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public override IssueSubscriberReport CreateIssueSubscriberReport() => this.CreateIssueSubscriberReport(this.GetNewGuid());

    /// <summary>Creates the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of the newly created issue subscriber report.</returns>
    public override IssueSubscriberReport CreateIssueSubscriberReport(
      Guid reportId)
    {
      IssueSubscriberReport entity = new IssueSubscriberReport(reportId, this.ApplicationName);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the issue subscriber report.</summary>
    /// <param name="reportId">The report id.</param>
    /// <returns>An instance of issue subscriber report.</returns>
    public override IssueSubscriberReport GetIssueSubscriberReport(
      Guid reportId)
    {
      IssueSubscriberReport subscriberReport = !(reportId == Guid.Empty) ? this.GetContext().GetItemById<IssueSubscriberReport>(reportId.ToString()) : throw new ArgumentException("reportId cannot be Empty Guid");
      subscriberReport.Provider = (object) this;
      return subscriberReport;
    }

    /// <summary>Gets the issue subscriber reports.</summary>
    /// <returns>A query of issue subscriber reports.</returns>
    public override IQueryable<IssueSubscriberReport> GetIssueSubscriberReports()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<IssueSubscriberReport>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new NewsletterMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
    }

    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber <= 0 || upgradedFromSchemaVersionNumber >= 1371)
        return;
      OpenAccessConnection.MsSqlUpgrade(context.Connection, "OpenAccessNewslettersDataProvider: Upgrade to 1371", (System.Action<IDbCommand>) (cmd => cmd.ExecuteNonQuery("\r\n                        IF OBJECT_ID('sf_subscriber_sf_lst', 'U') IS NOT NULL\r\n                            INSERT sf_lst_sf_subscriber (id, id2)\r\n                            SELECT id2, id FROM sf_subscriber_sf_lst\r\n                    ")));
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }
  }
}
