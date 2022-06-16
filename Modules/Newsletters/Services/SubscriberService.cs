// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.SubscriberService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>Service for managing newsletter subscribers.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class SubscriberService : ISubscriberService
  {
    /// <summary>
    /// Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created.
    /// The saved subscriber is returned in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be saved.</param>
    /// <param name="subscriber">The view model of the subscriber object.</param>
    /// <param name="provider">The provider through which the subscriber ought to be saved.</param>
    /// <returns>The saved subscriber.</returns>
    public SubscriberViewModel SaveSubscriber(
      string subscriberId,
      SubscriberViewModel subscriber,
      string provider)
    {
      SubscriberService.DemandPermissions();
      return this.SaveSubscriberInternal(subscriberId, subscriber, provider);
    }

    /// <summary>
    /// Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created.
    /// The saved subscriber is returned in XML format.
    /// </summary>
    /// <param name="subscriberId">The subscriber id.</param>
    /// <param name="subscriber">The view model of the subscriber object.</param>
    /// <param name="provider">The provider through which the subscriber ought to be saved.</param>
    /// <returns>The saved subscriber.</returns>
    public SubscriberViewModel SaveSubscriberInXml(
      string subscriberId,
      SubscriberViewModel subscriber,
      string provider)
    {
      SubscriberService.DemandPermissions();
      return this.SaveSubscriberInternal(subscriberId, subscriber, provider);
    }

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<SubscriberViewModel> GetSubscribers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      SubscriberService.DemandPermissions();
      return this.GetSubscribersInternal(string.Empty, provider, sortExpression, skip, take, filter, (string) null);
    }

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<SubscriberViewModel> GetSubscribersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      SubscriberService.DemandPermissions();
      return this.GetSubscribersInternal(string.Empty, provider, sortExpression, skip, take, filter, (string) null);
    }

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider and mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="dynamicListKey">The dynamic list key. If presented, the method will return the subscribers from the given dynamic list.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<SubscriberViewModel> GetMailingListSubscribers(
      string mailingListId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey)
    {
      SubscriberService.DemandPermissions();
      return this.GetSubscribersInternal(mailingListId, provider, sortExpression, skip, take, filter, dynamicListKey);
    }

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider and mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="dynamicListKey">The dynamic list key. If presented, the method will return the subscribers from the given dynamic list.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    public CollectionContext<SubscriberViewModel> GetMailingListSubscribersInXml(
      string mailingListId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey)
    {
      SubscriberService.DemandPermissions();
      return this.GetSubscribersInternal(mailingListId, provider, sortExpression, skip, take, filter, dynamicListKey);
    }

    /// <summary>
    /// Deletes the subscriber by id and returns true if the subscriber has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteSubscriber(string subscriberId, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.DeleteSubscriberInternal(subscriberId, provider);
    }

    /// <summary>
    /// Deletes the subscriber by id and returns true if the subscriber has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteSubscriberInXml(string subscriberId, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.DeleteSubscriberInternal(subscriberId, provider);
    }

    /// <summary>
    /// Deletes a collection of subscribers. Result is returned in JSON format.
    /// </summary>
    /// <param name="subscriberIds">An array of the ids of the subscribers to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all subscribers have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteSubscribers(string[] subscriberIds, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.BatchDeleteSubscribersInternal(subscriberIds, provider);
    }

    /// <summary>
    /// Deletes a collection of subscribers. Result is returned in XML format.
    /// </summary>
    /// <param name="subscriberIds">An array of the ids of the subscribers to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all subscribers have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteSubscribersInXml(string[] subscriberIds, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.BatchDeleteSubscribersInternal(subscriberIds, provider);
    }

    /// <summary>
    /// Adding existing subscribers to a mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to add.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully added; otherwise false.
    /// </returns>
    public bool AddSubscribers(string mailingListId, string[] subscriberIds, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.AddSubscribersInternal(mailingListId, subscriberIds, provider);
    }

    /// <summary>
    /// Adding existing subscribers to a mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to add.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully added; otherwise false.
    /// </returns>
    public bool AddSubscribersInXml(string mailingListId, string[] subscriberIds, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.AddSubscribersInternal(mailingListId, subscriberIds, provider);
    }

    /// <summary>
    /// Removing existing subscribers from a mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to remove.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully removed; otherwise false.
    /// </returns>
    public bool RemoveSubscribers(string mailingListId, string[] subscriberIds, string provider)
    {
      SubscriberService.DemandPermissions();
      return this.RemoveSubscribersInternal(mailingListId, subscriberIds, provider);
    }

    /// <summary>
    /// Removing existing subscribers from a mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to remove.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully removed; otherwise false.
    /// </returns>
    public bool RemoveSubscribersInXml(
      string mailingListId,
      string[] subscriberIds,
      string provider)
    {
      SubscriberService.DemandPermissions();
      return this.RemoveSubscribersInternal(mailingListId, subscriberIds, provider);
    }

    /// <summary>
    /// Gets the single subscriber item and returs it in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the subscriber.</param>
    /// <returns>An instance of SubscriberViewModel.</returns>
    public SubscriberViewModel GetSubscriber(
      string subscriberId,
      string provider)
    {
      return this.GetSubscriberInternal(subscriberId, provider);
    }

    /// <summary>
    /// Gets the single subscriber item and returns it in XML format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the subscriber.</param>
    /// <returns>An instance of SubscriberViewModel.</returns>
    public SubscriberViewModel GetSubscriberInXml(
      string subscriberId,
      string provider)
    {
      return this.GetSubscriberInternal(subscriberId, provider);
    }

    private static void DemandPermissions() => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity =>
    {
      if (!identity.IsBackendUser)
        return true;
      return !AppPermission.IsGranted(AppAction.ManageNewsletters);
    }));

    private SubscriberViewModel SaveSubscriberInternal(
      string subscriberId,
      SubscriberViewModel subscriber,
      string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Guid subscriberId1 = new Guid(subscriberId);
      if (subscriberId1 == Guid.Empty)
      {
        Subscriber subscriber1 = manager.CreateSubscriber(true);
        this.CopyToSubscriber(subscriber, subscriber1);
        subscriber1.Lists.Clear();
        foreach (MailingListViewModel list in subscriber.Lists)
        {
          MailingList mailingList = manager.GetMailingList(list.Id);
          if (this.IsSubscriberAlreadyInList(mailingList, subscriber1))
            throw new ArgumentException(string.Format(Res.Get<NewslettersResources>().SubscriberIsAlreadyMemberInList, (object) subscriber.Email, (object) mailingList.Title));
          subscriber1.Lists.Add(mailingList);
        }
        manager.SaveChanges();
      }
      else
      {
        Subscriber subscriber2 = manager.GetSubscriber(subscriberId1);
        this.CopyToSubscriber(subscriber, subscriber2);
        subscriber2.Lists.Clear();
        foreach (MailingListViewModel list1 in subscriber.Lists)
        {
          MailingList list = manager.GetMailingList(list1.Id);
          if (this.IsSubscriberAlreadyInList(list, subscriber2))
            throw new ArgumentException(string.Format(Res.Get<NewslettersResources>().SubscriberIsAlreadyMemberInList, (object) subscriber.Email, (object) list.Title));
          if (!subscriber2.Lists.Any<MailingList>((Func<MailingList, bool>) (l => l.Id == list.Id)))
            subscriber2.Lists.Add(list);
        }
        manager.SaveChanges();
      }
      ServiceUtility.DisableCache();
      return (SubscriberViewModel) null;
    }

    private bool IsSubscriberAlreadyInList(MailingList list, Subscriber sub) => list.Subscribers().Any<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Id != sub.Id && string.Compare(s.Email, sub.Email, true) == 0));

    private CollectionContext<SubscriberViewModel> GetSubscribersInternal(
      string mailingListId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      int? totalCount = new int?(0);
      HashSet<SubscriberViewModel> items = !string.IsNullOrEmpty(dynamicListKey) ? this.GetDynamicSubscribersViewModels(mailingListId, sortExpression, skip, take, filter, dynamicListKey, manager, ref totalCount) : this.GetSubscribersViewModels(mailingListId, sortExpression, skip, take, filter, manager, ref totalCount);
      ServiceUtility.DisableCache();
      return new CollectionContext<SubscriberViewModel>((IEnumerable<SubscriberViewModel>) items)
      {
        TotalCount = totalCount.Value
      };
    }

    private HashSet<SubscriberViewModel> GetSubscribersViewModels(
      string mailingListId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      NewslettersManager newslettersManager,
      ref int? totalCount)
    {
      IQueryable<Subscriber> source1;
      if (!string.IsNullOrEmpty(mailingListId))
      {
        Guid listId = new Guid(mailingListId);
        source1 = newslettersManager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Lists.Any<MailingList>((Func<MailingList, bool>) (l => l.Id == listId))));
      }
      else
        source1 = newslettersManager.GetSubscribers();
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<Subscriber>(filter);
      totalCount = new int?(source1.Count<Subscriber>());
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<Subscriber>(sortExpression);
      if (skip > 0)
        source1 = source1.Skip<Subscriber>(skip);
      if (take > 0)
        source1 = source1.Take<Subscriber>(take);
      HashSet<SubscriberViewModel> subscribersViewModels = new HashSet<SubscriberViewModel>();
      foreach (Subscriber source2 in (IEnumerable<Subscriber>) source1)
        subscribersViewModels.Add(this.GetViewModel(source2, new SubscriberViewModel()));
      return subscribersViewModels;
    }

    private HashSet<SubscriberViewModel> GetDynamicSubscribersViewModels(
      string mailingListId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey,
      NewslettersManager manager,
      ref int? totalCount)
    {
      MailingList mailingList = manager.GetMailingList(new Guid(mailingListId));
      HashSet<SubscriberViewModel> subscribersViewModels = new HashSet<SubscriberViewModel>();
      DynamicListSettings dynamicList = mailingList.DynamicLists.FirstOrDefault<DynamicListSettings>((Func<DynamicListSettings, bool>) (l => l.ListKey == dynamicListKey));
      foreach (object subscriber in manager.GetDynamicListProvider(dynamicList.DynamicListProviderName).GetSubscribers(dynamicList.ListKey, filter, sortExpression, new int?(skip), new int?(take), ref totalCount))
      {
        SubscriberViewModel viewModel = this.GetViewModel(dynamicList, subscriber, new SubscriberViewModel());
        if (!string.IsNullOrWhiteSpace(viewModel.Email) || !string.IsNullOrWhiteSpace(viewModel.Name))
          subscribersViewModels.Add(viewModel);
      }
      return subscribersViewModels;
    }

    private bool DeleteSubscriberInternal(string subscriberId, string provider)
    {
      Guid subscriberId1 = new Guid(subscriberId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.DeleteSubscriber(subscriberId1);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool BatchDeleteSubscribersInternal(string[] subscriberIds, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      foreach (string subscriberId1 in subscriberIds)
      {
        Guid subscriberId2 = new Guid(subscriberId1);
        manager.DeleteSubscriber(subscriberId2);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool AddSubscribersInternal(
      string mailingListId,
      string[] subscriberIds,
      string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      IEnumerable<Guid> guids = ((IEnumerable<string>) subscriberIds).Select<string, Guid>((Func<string, Guid>) (s => new Guid(s)));
      MailingList mailingList = manager.GetMailingList(new Guid(mailingListId));
      HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (Subscriber subscriber in (IEnumerable<Subscriber>) mailingList.Subscribers())
        subscriber.Lists.Remove(mailingList);
      foreach (Guid subscriberId in guids)
      {
        Subscriber subscriber = manager.GetSubscriber(subscriberId);
        string email = subscriber.Email;
        if (!stringSet.Contains(email))
        {
          subscriber.Lists.Add(mailingList);
          stringSet.Add(email);
        }
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool RemoveSubscribersInternal(
      string mailingListId,
      string[] subscriberIds,
      string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      MailingList mailingList = manager.GetMailingList(new Guid(mailingListId));
      foreach (string subscriberId1 in subscriberIds)
      {
        Guid subscriberId2 = new Guid(subscriberId1);
        manager.GetSubscriber(subscriberId2).Lists.Remove(mailingList);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private void CopyToSubscriber(SubscriberViewModel source, Subscriber target)
    {
      target.Email = source.Email;
      target.FirstName = source.FirstName;
      target.LastName = source.LastName;
    }

    private SubscriberViewModel GetViewModel(
      Subscriber source,
      SubscriberViewModel target)
    {
      target.Id = source.Id;
      target.Email = source.Email;
      target.FirstName = source.FirstName;
      target.LastName = source.LastName;
      target.Name = source.FirstName + " " + source.LastName;
      foreach (MailingList list in (IEnumerable<MailingList>) source.Lists)
        target.Lists.Add(new MailingListViewModel(list));
      return target;
    }

    private SubscriberViewModel GetViewModel(
      DynamicListSettings dynamicList,
      object source,
      SubscriberViewModel target)
    {
      MergeTag mergeTag1 = new MergeTag(dynamicList.FirstNameMappedField);
      object obj1 = TypeDescriptor.GetProperties(source)[mergeTag1.PropertyName].GetValue(source);
      if (obj1 != null)
        target.FirstName = obj1.ToString();
      MergeTag mergeTag2 = new MergeTag(dynamicList.LastNameMappedField);
      object obj2 = TypeDescriptor.GetProperties(source)[mergeTag2.PropertyName].GetValue(source);
      if (obj2 != null)
        target.LastName = obj2.ToString();
      target.Name = target.FirstName + " " + target.LastName;
      MergeTag mergeTag3 = new MergeTag(dynamicList.EmailMappedField);
      object obj3 = TypeDescriptor.GetProperties(source)[mergeTag3.PropertyName].GetValue(source);
      target.Email = obj3 == null ? string.Empty : obj3.ToString();
      return target;
    }

    private SubscriberViewModel GetSubscriberInternal(
      string subscriberId,
      string provider)
    {
      SubscriberService.DemandPermissions();
      Subscriber subscriber = NewslettersManager.GetManager(provider).GetSubscriber(Telerik.Sitefinity.Utilities.Utility.StringToGuid(subscriberId));
      ServiceUtility.DisableCache();
      return this.GetViewModel(subscriber, new SubscriberViewModel());
    }
  }
}
