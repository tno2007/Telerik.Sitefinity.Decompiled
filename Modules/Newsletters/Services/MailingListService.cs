// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.MailingListService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>Service for managing newsletter mailing lists.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MailingListService : IMailingListService
  {
    /// <summary>
    /// Saves a newsletter mailing list. If the mailing list with specified id exists that mailing list will be updated; otherwise new mailing list will be created.
    /// The saved mailing list is returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list to be saved.</param>
    /// <param name="mailingList">The view model of the mailing list object.</param>
    /// <param name="provider">The provider through which the mailing list ought to be saved.</param>
    /// <returns>The saved mailing list.</returns>
    public MailingListViewModel SaveMailingList(
      string mailingListId,
      MailingListViewModel mailingList,
      string provider)
    {
      MailingListService.DemandPermissions();
      return this.SaveMailingListInternal(mailingListId, mailingList, provider);
    }

    /// <summary>
    /// Saves a newsletter mailing list. If the mailing list with specified id exists that mailing list will be updated; otherwise new mailing list will be created.
    /// The saved mailing list is returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="mailingList">The view model of the mailing list object.</param>
    /// <param name="provider">The provider through which the mailing list ought to be saved.</param>
    /// <returns></returns>
    public MailingListViewModel SaveMailingListInXml(
      string mailingListId,
      MailingListViewModel mailingList,
      string provider)
    {
      MailingListService.DemandPermissions();
      return this.SaveMailingListInternal(mailingListId, mailingList, provider);
    }

    /// <summary>
    /// Gets all mailing lists of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the mailing lists ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the mailing lists.</param>
    /// <param name="skip">Number of mailing lists to skip in result set. (used for paging)</param>
    /// <param name="take">Number of mailing lists to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> objects.
    /// </returns>
    public CollectionContext<MailingListViewModel> GetMailingLists(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      MailingListService.DemandPermissions();
      return this.GetMailingListsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all mailing lists of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the mailing lists ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the mailing lists.</param>
    /// <param name="skip">Number of mailing lists to skip in result set. (used for paging)</param>
    /// <param name="take">Number of mailing lists to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> objects.
    /// </returns>
    public CollectionContext<MailingListViewModel> GetMailingListsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      MailingListService.DemandPermissions();
      return this.GetMailingListsInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Deletes the mailing list by id and returns true if the mailing list has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteMailingList(string mailingListId, string provider)
    {
      MailingListService.DemandPermissions();
      return this.DeleteMailingListInternal(mailingListId, provider);
    }

    /// <summary>
    /// Deletes the mailing list by id and returns true if the mailing list has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteMailingListInXml(string mailingListId, string provider)
    {
      MailingListService.DemandPermissions();
      return this.DeleteMailingListInternal(mailingListId, provider);
    }

    /// <summary>
    /// Deletes a collection of mailing lists. Result is returned in JSON format.
    /// </summary>
    /// <param name="mailingListIds">An array of the ids of the mailing lists to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all mailing lists have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteMailingLists(string[] mailingListIds, string provider)
    {
      MailingListService.DemandPermissions();
      return this.BatchDeleteMailingListsInternal(mailingListIds, provider);
    }

    /// <summary>
    /// Deletes a collection of mailing lists. Result is returned in XML format.
    /// </summary>
    /// <param name="mailingListIds">An array of the ids of the mailing lists to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all mailing lists have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteMailingListsInXml(string[] mailingListIds, string provider)
    {
      MailingListService.DemandPermissions();
      return this.BatchDeleteMailingListsInternal(mailingListIds, provider);
    }

    /// <summary>Gets the mailing list and returns it in JSON format.</summary>
    /// <param name="mailingListId">Id of the mailing list that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the mailing list.</param>
    /// <returns>An instance of MailingListViewModel.</returns>
    public MailingListViewModel GetMailingList(
      string mailingListId,
      string provider)
    {
      return this.GetMailingListInternal(mailingListId, provider);
    }

    /// <summary>Gets the mailing list and returns it in XML format.</summary>
    /// <param name="mailingListId">Id of the mailing list that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the mailing list.</param>
    /// <returns>An instance of MailingListViewModel.</returns>
    public MailingListViewModel GetMailingListInXml(
      string mailingListId,
      string provider)
    {
      return this.GetMailingListInternal(mailingListId, provider);
    }

    private static void DemandPermissions() => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity =>
    {
      if (!identity.IsBackendUser)
        return true;
      return !AppPermission.IsGranted(AppAction.ManageNewsletters);
    }));

    private CollectionContext<MailingListViewModel> GetMailingListsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      IQueryable<MailingList> mailingLists = manager.GetMailingLists();
      IQueryable<MailingList> source1;
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = mailingLists.OrderBy<MailingList>(sortExpression);
      else
        source1 = (IQueryable<MailingList>) mailingLists.OrderBy<MailingList, DateTime>((Expression<Func<MailingList, DateTime>>) (m => m.LastModified));
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<MailingList>(filter);
      int num = source1.Count<MailingList>();
      if (skip > 0)
        source1 = source1.Skip<MailingList>(skip);
      if (take > 0)
        source1 = source1.Take<MailingList>(take);
      List<MailingListViewModel> items = new List<MailingListViewModel>();
      foreach (MailingList source2 in (IEnumerable<MailingList>) source1)
      {
        MailingListViewModel target = new MailingListViewModel();
        items.Add(this.GetMailingListViewModel(source2, target, manager));
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<MailingListViewModel>((IEnumerable<MailingListViewModel>) items)
      {
        TotalCount = num
      };
    }

    private MailingListViewModel SaveMailingListInternal(
      string mailingListId,
      MailingListViewModel mailingList,
      string provider)
    {
      Guid id = new Guid(mailingListId);
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      if (id == Guid.Empty)
      {
        MailingList mailingList1 = manager.CreateMailingList();
        Synchronizer.Synchronize(mailingList, mailingList1, manager);
        manager.SaveChanges();
        ServiceUtility.DisableCache();
        return this.GetMailingListViewModel(mailingList1, mailingList, manager);
      }
      MailingList mailingList2 = manager.GetMailingList(id);
      Synchronizer.Synchronize(mailingList, mailingList2, manager);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return this.GetMailingListViewModel(mailingList2, mailingList, manager);
    }

    private bool DeleteMailingListInternal(string mailingListId, string provider)
    {
      Guid id = new Guid(mailingListId);
      if (id != Guid.Empty)
      {
        NewslettersManager manager = NewslettersManager.GetManager(provider);
        manager.DeleteMailingList(id);
        manager.SaveChanges();
        ServiceUtility.DisableCache();
        return true;
      }
      ServiceUtility.DisableCache();
      return false;
    }

    private bool BatchDeleteMailingListsInternal(string[] mailingListIds, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      foreach (string mailingListId in mailingListIds)
      {
        Guid id = new Guid(mailingListId);
        manager.DeleteMailingList(id);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private void CopyToMailingListObject(
      MailingListViewModel source,
      MailingList target,
      NewslettersManager manager)
    {
      target.DefaultFromName = (Lstring) source.DefaultFromName;
      target.DefaultReplyToEmail = (Lstring) source.DefaultReplyToEmail;
      target.DefaultSubject = (Lstring) source.DefaultSubject;
      target.SubscriptionReminder = (Lstring) source.SubscriptionReminder;
      target.Title = (Lstring) source.Title;
      target.SendWelcomeMessage = source.SendWelcomeMessage;
      target.WelcomeMessageSubject = source.WelcomeMessageSubject;
      target.WelcomeMessageEmailAddress = source.WelcomeMessageEmailAddress;
      target.SendGoodByeMessage = source.SendGoodByeMessage;
      target.GoodByeMessageSubject = source.GoodByeMessageSubject;
      target.GoodByeMessageEmailAddress = source.GoodByeMessageEmailAddress;
      foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) target.DynamicLists)
      {
        DynamicListSettings existingDynamicLists = dynamicList;
        if (source.DynamicLists.Where<DynamicListSettingsViewModel>((Func<DynamicListSettingsViewModel, bool>) (dl => dl.ListKey == existingDynamicLists.ListKey && dl.DynamicListProviderName == existingDynamicLists.DynamicListProviderName)).Count<DynamicListSettingsViewModel>() == 0)
          target.DynamicLists.Remove(existingDynamicLists);
      }
      foreach (DynamicListSettingsViewModel dynamicList1 in (IEnumerable<DynamicListSettingsViewModel>) source.DynamicLists)
      {
        DynamicListSettingsViewModel dynamicList = dynamicList1;
        DynamicListSettings target1 = target.DynamicLists.Where<DynamicListSettings>((Func<DynamicListSettings, bool>) (dl => dl.ListKey == dynamicList.ListKey && dl.DynamicListProviderName == dynamicList.DynamicListProviderName)).SingleOrDefault<DynamicListSettings>();
        if (target1 != null)
        {
          Synchronizer.Synchronize(dynamicList, target1);
        }
        else
        {
          DynamicListSettings target2 = new DynamicListSettings();
          Synchronizer.Synchronize(dynamicList, target2);
          target.DynamicLists.Add(target2);
        }
      }
      target.WelcomeTemplate = !(source.WelcomeTemplateId != Guid.Empty) || !source.SendWelcomeMessage ? (MessageBody) null : manager.GetMessageBody(source.WelcomeTemplateId);
      if (source.GoodByeTemplateId != Guid.Empty && source.SendGoodByeMessage)
        target.GoodByeTemplate = manager.GetMessageBody(source.GoodByeTemplateId);
      else
        target.GoodByeTemplate = (MessageBody) null;
    }

    private MailingListViewModel GetMailingListViewModel(
      MailingList source,
      MailingListViewModel target,
      NewslettersManager manager)
    {
      target.Id = source.Id;
      target.DefaultFromName = (string) source.DefaultFromName;
      target.DefaultReplyToEmail = (string) source.DefaultReplyToEmail;
      target.DefaultSubject = (string) source.DefaultSubject;
      target.SubscriptionReminder = (string) source.SubscriptionReminder;
      target.Title = (string) source.Title;
      target.SubscribersCount = (source.SubscribersCount() + source.DynamicSubscribersCount()).ToString();
      target.SubscribersCountText = target.SubscribersCount == "1" ? Res.Get<NewslettersResources>().OneSubscriber : string.Format(Res.Get<NewslettersResources>().NumberOfSubscribers, (object) target.SubscribersCount);
      target.SendWelcomeMessage = source.SendWelcomeMessage;
      target.WelcomeMessageSubject = source.WelcomeMessageSubject;
      target.WelcomeMessageEmailAddress = source.WelcomeMessageEmailAddress;
      target.SendGoodByeMessage = source.SendGoodByeMessage;
      target.GoodByeMessageSubject = source.GoodByeMessageSubject;
      target.GoodByeMessageEmailAddress = source.GoodByeMessageEmailAddress;
      target.UnsubscribePageId = source.UnsubscribePageId;
      target.CampaignsCount = manager.GetIssues().Count<Campaign>((Expression<Func<Campaign, bool>>) (i => i.List != default (object) && i.List.Id == source.Id && (int) i.CampaignState != 6 && (int) i.CampaignState != 7));
      target.CampaignsText = target.CampaignsCount == 1 ? Res.Get<NewslettersResources>().OneCampaign : string.Format(Res.Get<NewslettersResources>().NumberOfCampaigns, (object) target.CampaignsCount);
      foreach (DynamicListSettings dynamicList in (IEnumerable<DynamicListSettings>) source.DynamicLists)
      {
        DynamicListSettingsViewModel settingsViewModel = new DynamicListSettingsViewModel();
        DynamicListSettingsViewModel target1 = settingsViewModel;
        Synchronizer.Synchronize(dynamicList, target1);
        target.DynamicLists.Add(settingsViewModel);
      }
      target.WelcomeTemplateId = source.WelcomeTemplate == null ? Guid.Empty : source.WelcomeTemplate.Id;
      target.GoodByeTemplateId = source.GoodByeTemplate == null ? Guid.Empty : source.GoodByeTemplate.Id;
      return target;
    }

    private MailingListViewModel GetMailingListInternal(
      string mailingListId,
      string provider)
    {
      MailingListService.DemandPermissions();
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      MailingList mailingList = manager.GetMailingList(Telerik.Sitefinity.Utilities.Utility.StringToGuid(mailingListId));
      ServiceUtility.DisableCache();
      return this.GetMailingListViewModel(mailingList, new MailingListViewModel(), manager);
    }
  }
}
