// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IssueReportService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.Data.Reports;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Service that provides method for working with all supported types of the campaings in the newsletter module.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class IssueReportService : IIssueReportService
  {
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
    [Obsolete("Use CampaignService.GetIssues or CampaignService.GetCampaignIssues instead.")]
    public IEnumerable<IssueReportViewModel> GetIssueReports(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueReportsInternal(provider, sortExpression, skip, take, filter);
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
    [Obsolete("Use CampaignService.GetIssuesInXml or CampaignService.GetCampaignIssuesInXml instead.")]
    public IEnumerable<IssueReportViewModel> GetIssueReportsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueReportsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the issue unique clicks. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ClickStatViewModel" /> objects.</returns>
    public IEnumerable<ClickStatViewModel> GetIssueUniqueClicks(
      string issueId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueUniqueClicksInternal(issueId, provider);
    }

    /// <summary>
    /// Gets the issue unique clicks. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ClickStatViewModel" /> objects.</returns>
    public IEnumerable<ClickStatViewModel> GetIssueUniqueClicksXml(
      string issueId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueUniqueClicksInternal(issueId, provider);
    }

    /// <summary>
    /// Gets the issue clicks by hour. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    public IEnumerable<KeyValuePair<string, int>> GetIssueClicksByHour(
      string issueId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueClicksByHourInternal(issueId, provider);
    }

    /// <summary>
    /// Gets the issue clicks by hour. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    public IEnumerable<KeyValuePair<string, int>> GetIssueClicksByHourXml(
      string issueId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueClicksByHourInternal(issueId, provider);
    }

    /// <summary>
    /// Gets the issue subscriber. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueSubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<IssueSubscriberViewModel> GetIssueSubscribers(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string byClickedLink)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueSubscribersInternal(issueId, provider, sortExpression, skip, take, filter, byClickedLink);
    }

    /// <summary>
    /// Gets the issue subscribers. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueSubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<IssueSubscriberViewModel> GetIssueSubscribersXml(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string byClickedLink)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueSubscribersInternal(issueId, provider, sortExpression, skip, take, filter, byClickedLink);
    }

    /// <summary>
    /// Gets the issue subscriber clicks. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    public IEnumerable<SubscriberIssueClickViewModel> GetIssueSubscriberClicks(
      string issueId,
      string subscriberId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueSubscriberClicksInternal(issueId, subscriberId, provider);
    }

    /// <summary>
    /// Gets the issue subscriber clicks. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberIssueClickViewModel" /> objects.</returns>
    public IEnumerable<SubscriberIssueClickViewModel> GetIssueSubscriberClicksXml(
      string issueId,
      string subscriberId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueSubscriberClicksInternal(issueId, subscriberId, provider);
    }

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
    public CollectionContext<SubscriberIssueClickViewModel> GetIssueClicks(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueClicksInternal(issueId, provider, sortExpression, skip, take, filter);
    }

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
    public CollectionContext<SubscriberIssueClickViewModel> GetIssueClicksXml(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueClicksInternal(issueId, provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the total number of clicks for each link in an issue. The results are returned in JSON format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    public IEnumerable<KeyValuePair<string, int>> GetIssueTotalLinkClicks(
      string issueId,
      string search,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueTotalLinkClicksInternal(issueId, search, provider);
    }

    /// <summary>
    /// Gets the total number of clicks for each link in an issue. The results are returned in XML format.
    /// </summary>
    /// <param name="issueId">Id of the issue that we are getting clicks for.</param>
    /// <param name="provider">Name of the provider from which the campaigns ought to be retrieved.</param>
    /// <returns>Collection context object of <see cref="!:KeyValuePair" /> objects.</returns>
    public IEnumerable<KeyValuePair<string, int>> GetIssueTotalLinkClicksXml(
      string issueId,
      string search,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetIssueTotalLinkClicksInternal(issueId, search, provider);
    }

    private IEnumerable<IssueReportViewModel> GetIssueReportsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      IQueryable<Campaign> source1 = NewslettersManager.GetManager(provider).GetCampaigns();
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<Campaign>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<Campaign>(filter);
      if (skip > 0)
        source1 = source1.Skip<Campaign>(skip);
      if (take > 0)
        source1 = source1.Take<Campaign>(take);
      List<IssueReportViewModel> source2 = new List<IssueReportViewModel>();
      foreach (Campaign campaign in (IEnumerable<Campaign>) source1)
      {
        CampaignReport source3 = new CampaignReport(provider, campaign.Id);
        IssueReportViewModel issueReportViewModel = new IssueReportViewModel(campaign, provider);
        IssueReportViewModel target = issueReportViewModel;
        Synchronizer.Synchronize(source3, target);
        source2.Add(issueReportViewModel);
      }
      ServiceUtility.DisableCache();
      return source2.AsEnumerable<IssueReportViewModel>();
    }

    private IEnumerable<ClickStatViewModel> GetIssueUniqueClicksInternal(
      string issueId,
      string provider)
    {
      Guid campaignId = Guid.Parse(issueId);
      IQueryable<\u003C\u003Ef__AnonymousType50<string, int>> source = NewslettersManager.GetManager(provider).GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == campaignId)).GroupBy(cs => new
      {
        Url = cs.Url,
        SubscriberId = cs.SubscriberId
      }).GroupBy(cs => new{ Url = cs.Key.Url }).Select(cs => new
      {
        Url = cs.Key.Url,
        Count = cs.Count<IGrouping<\u003C\u003Ef__AnonymousType48<string, Guid>, LinkClickStat>>()
      });
      List<ClickStatViewModel> uniqueClicksInternal = new List<ClickStatViewModel>(source.Count());
      foreach (var data in source)
        uniqueClicksInternal.Add(new ClickStatViewModel()
        {
          Url = data.Url,
          Clicks = data.Count
        });
      ServiceUtility.DisableCache();
      return (IEnumerable<ClickStatViewModel>) uniqueClicksInternal;
    }

    private IEnumerable<KeyValuePair<string, int>> GetIssueClicksByHourInternal(
      string issueId,
      string provider)
    {
      Guid campaignId = Guid.Parse(issueId);
      IQueryable<LinkClickStat> queryable = NewslettersManager.GetManager(provider).GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == campaignId));
      KeyValuePair<string, int>[] clicksByHourInternal = new KeyValuePair<string, int>[24];
      for (int hour = 0; hour < 24; ++hour)
      {
        string key = IssueReportService.HourString(new DateTime(1, 1, 1, hour, 0, 0), (IFormatProvider) SystemManager.CurrentContext.Culture);
        clicksByHourInternal[hour] = new KeyValuePair<string, int>(key, 0);
      }
      foreach (LinkClickStat linkClickStat in (IEnumerable<LinkClickStat>) queryable)
      {
        KeyValuePair<string, int>[] keyValuePairArray1 = clicksByHourInternal;
        DateTime sitefinityUiTime = linkClickStat.DateTimeClicked.ToSitefinityUITime();
        int hour1 = sitefinityUiTime.Hour;
        KeyValuePair<string, int> keyValuePair1 = keyValuePairArray1[hour1];
        KeyValuePair<string, int>[] keyValuePairArray2 = clicksByHourInternal;
        sitefinityUiTime = linkClickStat.DateTimeClicked.ToSitefinityUITime();
        int hour2 = sitefinityUiTime.Hour;
        KeyValuePair<string, int> keyValuePair2 = new KeyValuePair<string, int>(keyValuePair1.Key, keyValuePair1.Value + 1);
        keyValuePairArray2[hour2] = keyValuePair2;
      }
      ServiceUtility.DisableCache();
      return (IEnumerable<KeyValuePair<string, int>>) clicksByHourInternal;
    }

    private static string HourString(DateTime dt, IFormatProvider provider)
    {
      DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
      string format = Regex.Replace(Regex.Replace(instance.ShortTimePattern, "[^hHt\\s]", ""), "\\s+", " ").Trim();
      if (format.Length == 0)
        return "";
      if (format.Length == 1)
        format = "%" + format;
      return dt.ToString(format, (IFormatProvider) instance);
    }

    private CollectionContext<IssueSubscriberViewModel> GetIssueSubscribersInternal(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string byClickedLink)
    {
      Guid id = Guid.Parse(issueId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Campaign issue = manager.GetIssue(id);
      IQueryable<IssueSubscriberReport> queryable = manager.GetIssueSubscriberReports().Where<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.Issue == issue));
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<IssueSubscriberReport>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<IssueSubscriberReport>(filter);
      if (!string.IsNullOrEmpty(byClickedLink))
      {
        IQueryable<IGrouping<Guid, LinkClickStat>> inner = manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.CampaignId == id && c.Url == byClickedLink)).GroupBy<LinkClickStat, Guid>((Expression<Func<LinkClickStat, Guid>>) (c => c.SubscriberId));
        queryable = queryable.Join<IssueSubscriberReport, IGrouping<Guid, LinkClickStat>, Guid, IssueSubscriberReport>((IEnumerable<IGrouping<Guid, LinkClickStat>>) inner, (Expression<Func<IssueSubscriberReport, Guid>>) (r => r.Subscriber.Id), (Expression<Func<IGrouping<Guid, LinkClickStat>, Guid>>) (c => c.Key), (Expression<Func<IssueSubscriberReport, IGrouping<Guid, LinkClickStat>, IssueSubscriberReport>>) ((r, c) => r));
      }
      int num = queryable.Count<IssueSubscriberReport>();
      if (skip > 0)
        queryable = queryable.Skip<IssueSubscriberReport>(skip);
      if (take > 0)
        queryable = queryable.Take<IssueSubscriberReport>(take);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      IQueryable<IssueSubscriberViewModel> items = queryable.Select<IssueSubscriberReport, IssueSubscriberViewModel>(Expression.Lambda<Func<IssueSubscriberReport, IssueSubscriberViewModel>>((Expression) Expression.MemberInit(Expression.New(typeof (IssueSubscriberViewModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IssueSubscriberViewModel.set_DateOpened)), )))); //unable to render the statement
      ServiceUtility.DisableCache();
      return new CollectionContext<IssueSubscriberViewModel>((IEnumerable<IssueSubscriberViewModel>) items)
      {
        TotalCount = num
      };
    }

    private static string GetIssueSubscriberDeliveryState(
      MessageStatus messageStatus,
      DeliveryStatus deliveryStatus)
    {
      string subscriberDeliveryState;
      switch (messageStatus)
      {
        case MessageStatus.Normal:
          subscriberDeliveryState = deliveryStatus != DeliveryStatus.Success ? Res.Get<NewslettersResources>().NotDelivered : Res.Get<NewslettersResources>().Delivered;
          break;
        case MessageStatus.EmailAddressDoesNotExist:
          subscriberDeliveryState = Res.Get<NewslettersResources>().EmailAddressDoesNotExist;
          break;
        default:
          subscriberDeliveryState = Res.Get<NewslettersResources>().NotDelivered;
          break;
      }
      return subscriberDeliveryState;
    }

    private IEnumerable<SubscriberIssueClickViewModel> GetIssueSubscriberClicksInternal(
      string issueId,
      string subscriberId,
      string provider)
    {
      Guid campaignId = Guid.Parse(issueId);
      Guid subscriberGuid = Guid.Parse(subscriberId);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      IQueryable<SubscriberIssueClickViewModel> subscriberClicksInternal = NewslettersManager.GetManager(provider).GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.CampaignId == campaignId && cs.SubscriberId == subscriberGuid)).Select<LinkClickStat, SubscriberIssueClickViewModel>(Expression.Lambda<Func<LinkClickStat, SubscriberIssueClickViewModel>>((Expression) Expression.MemberInit(Expression.New(typeof (SubscriberIssueClickViewModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SubscriberIssueClickViewModel.set_Url)), )))); //unable to render the statement
      ServiceUtility.DisableCache();
      return (IEnumerable<SubscriberIssueClickViewModel>) subscriberClicksInternal;
    }

    private CollectionContext<SubscriberIssueClickViewModel> GetIssueClicksInternal(
      string issueId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guid issueGuid;
      if (!Guid.TryParse(issueId, out issueGuid))
        throw new ArgumentException(string.Format("Invalid issue Id {0}", (object) issueId));
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      IQueryable<LinkClickStat> queryable = manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.CampaignId == issueGuid));
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<LinkClickStat>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<LinkClickStat>(filter);
      int num = queryable.Count<LinkClickStat>();
      if (skip > 0)
        queryable = queryable.Skip<LinkClickStat>(skip);
      if (take > 0)
        queryable = queryable.Take<LinkClickStat>(take);
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      IQueryable<SubscriberIssueClickViewModel> items = queryable.Join<LinkClickStat, Subscriber, Guid, SubscriberIssueClickViewModel>((IEnumerable<Subscriber>) manager.GetSubscribers(), (Expression<Func<LinkClickStat, Guid>>) (c => c.SubscriberId), (Expression<Func<Subscriber, Guid>>) (s => s.Id), Expression.Lambda<Func<LinkClickStat, Subscriber, SubscriberIssueClickViewModel>>((Expression) Expression.MemberInit(Expression.New(typeof (SubscriberIssueClickViewModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SubscriberIssueClickViewModel.set_Url)), )))); //unable to render the statement
      ServiceUtility.DisableCache();
      return new CollectionContext<SubscriberIssueClickViewModel>((IEnumerable<SubscriberIssueClickViewModel>) items)
      {
        TotalCount = num
      };
    }

    private IEnumerable<KeyValuePair<string, int>> GetIssueTotalLinkClicksInternal(
      string issueId,
      string search,
      string provider)
    {
      Guid issueGuid;
      if (!Guid.TryParse(issueId, out issueGuid))
        throw new ArgumentException(string.Format("Invalid issue Id {0}", (object) issueId));
      IQueryable<LinkClickStat> source = NewslettersManager.GetManager(provider).GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.CampaignId == issueGuid));
      if (!string.IsNullOrEmpty(search))
        source = source.Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (c => c.Url.Contains(search)));
      IQueryable<KeyValuePair<string, int>> linkClicksInternal = source.GroupBy<LinkClickStat, string>((Expression<Func<LinkClickStat, string>>) (c => c.Url)).Select<IGrouping<string, LinkClickStat>, KeyValuePair<string, int>>((Expression<Func<IGrouping<string, LinkClickStat>, KeyValuePair<string, int>>>) (c => new KeyValuePair<string, int>(c.Key, c.Count<LinkClickStat>())));
      ServiceUtility.DisableCache();
      return (IEnumerable<KeyValuePair<string, int>>) linkClicksInternal;
    }
  }
}
