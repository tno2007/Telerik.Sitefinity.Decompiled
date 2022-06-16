// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.Synchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Data.Reports;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  /// <summary>
  /// This class provides functionality for synchronizing the states of the model and view model instances.
  /// </summary>
  public static class Synchronizer
  {
    /// <summary>
    /// Copies the Campaign instance (model) to the CampaignGridViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel" /> type.</param>
    /// <param name="manager">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    public static void Synchronize(
      Campaign source,
      CampaignGridViewModel target,
      NewslettersManager manager)
    {
      target.Id = source.Id;
      target.Name = source.Name;
      target.CampaignState = source.CampaignState;
      target.ListSubscriberCount = source.List == null ? 0 : source.List.Subscribers().Count<Subscriber>();
      target.MessageBodyId = source.MessageBody.Id;
      target.MessageBodyType = source.MessageBody.MessageBodyType;
      switch (source.MessageBody.MessageBodyType)
      {
        case MessageBodyType.PlainText:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().PlainTextCampaignType;
          break;
        case MessageBodyType.HtmlText:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().HtmlCampaignType;
          break;
        case MessageBodyType.InternalPage:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().StandardCampaignType;
          break;
      }
      if (manager == null && source.Provider != null)
        manager = !(source.Provider is NewslettersDataProvider provider) ? NewslettersManager.GetManager() : NewslettersManager.GetManager(provider.Name);
      target.CampaignStateUX = Synchronizer.GetCampaignStateUX(source, manager);
      target.CampaignStateClass = Enum.GetName(typeof (CampaignState), (object) target.CampaignState).ToLowerInvariant();
    }

    /// <summary>Synchronizes two Campaign instances.</summary>
    /// <param name="source">The source campaign.</param>
    /// <param name="target">The target campaign.</param>
    internal static void CopyProperties(Campaign source, Campaign target)
    {
      target.List = source.List;
      target.Name = source.Name;
      target.MessageSubject = source.MessageSubject;
      target.FromName = source.FromName;
      target.ReplyToEmail = source.ReplyToEmail;
      target.IsReadyForSending = source.IsReadyForSending;
      target.CampaignState = source.CampaignState;
      target.DeliveryDate = source.DeliveryDate;
      target.UseGoogleTracking = source.UseGoogleTracking;
      target.Provider = source.Provider;
    }

    /// <summary>Synchronizes two Campaign instances.</summary>
    /// <param name="source">The source campaign.</param>
    /// <param name="target">The target campaign.</param>
    public static void Synchronize(
      Campaign source,
      Campaign target,
      OpenAccessNewslettersDataProvider provider = null)
    {
      Synchronizer.CopyProperties(source, target);
      target.RootCampaign = source.RootCampaign;
      target.CampaignLinks.Clear();
      foreach (CampaignLink campaignLink in (IEnumerable<CampaignLink>) source.CampaignLinks)
        target.CampaignLinks.Add(new CampaignLink(provider != null ? provider.GetNewGuid() : Guid.NewGuid(), campaignLink.ApplicationName)
        {
          Campaign = target,
          Url = campaignLink.Url
        });
      target.BounceStats.Clear();
      foreach (BounceStat bounceStat in (IEnumerable<BounceStat>) source.BounceStats)
        target.BounceStats.Add(new BounceStat(provider != null ? provider.GetNewGuid() : Guid.NewGuid(), bounceStat.ApplicationName)
        {
          Campaign = target,
          Subscriber = bounceStat.Subscriber,
          SmtpStatus = bounceStat.SmtpStatus,
          BounceStatus = bounceStat.BounceStatus,
          AdditionalInfo = bounceStat.AdditionalInfo
        });
    }

    /// <summary>
    /// Copies the Campaign instance (model) to the IssueGridViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel" /> type.</param>
    /// <param name="manager">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    internal static void Synchronize(
      Campaign source,
      IssueGridViewModel target,
      NewslettersManager manager)
    {
      target.Id = source.RootCampaign != null ? source.Id : throw new InvalidOperationException("The source has to be an issue, campaign with RootCampaign.");
      target.RootCampaignId = source.RootCampaign.Id;
      target.Name = source.Name;
      IssueStatistics issueStatistics = new IssueStatistics(source.Id, manager);
      target.Sent = issueStatistics.SentMailsCount;
      target.Delivered = issueStatistics.DeliveredMailsCount;
      target.Opened = issueStatistics.UniqueOpeningsCount;
      target.Clicked = issueStatistics.ClickedIssuesCount;
      target.DateSent = new DateTime?(source.DeliveryDate);
      target.LastModified = source.LastModified;
      target.MessageBodyType = source.MessageBody.MessageBodyType;
      target.MessageBodyId = source.MessageBody.Id;
      CampaignState campaignState;
      switch (source.CampaignState)
      {
        case CampaignState.Draft:
        case CampaignState.PendingSending:
          target.IssueStateUX = Res.Get<NewslettersResources>().CampaignStateDraft;
          campaignState = CampaignState.Draft;
          break;
        case CampaignState.Sending:
        case CampaignState.Completed:
          target.IssueStateUX = Res.Get<NewslettersResources>().CampaignStateCompleted;
          campaignState = CampaignState.Completed;
          break;
        case CampaignState.Scheduled:
          target.IssueStateUX = Res.Get<NewslettersResources>().CampaignStateScheduled;
          campaignState = CampaignState.Scheduled;
          break;
        default:
          campaignState = source.CampaignState;
          break;
      }
      target.IssueStateClass = Enum.GetName(typeof (CampaignState), (object) campaignState).ToLowerInvariant();
    }

    /// <summary>
    /// Copies the Campaign instance (model) to the CampaignViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> type.</param>
    /// <param name="manager">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    public static void Synchronize(
      Campaign source,
      CampaignViewModel target,
      NewslettersManager manager)
    {
      target.Id = source.Id;
      if (source.List != null)
      {
        target.ListId = source.List.Id;
        target.ListTitle = (string) source.List.Title;
        target.ListSubscriberCount = source.List.Subscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => !s.IsSuspended)).Count<Subscriber>() + source.List.DynamicSubscribersCount();
      }
      else
        target.ListId = Guid.Empty;
      target.FromName = source.FromName;
      target.MessageSubject = source.MessageSubject;
      target.Name = source.Name;
      target.ReplyToEmail = source.ReplyToEmail;
      target.DeliveryDate = source.DeliveryDate;
      target.CampaignState = source.CampaignState;
      target.UseGoogleTracking = source.UseGoogleTracking;
      if (target is IssueViewModel issueViewModel)
      {
        issueViewModel.RootCampaignId = source.RootCampaign.Id;
        issueViewModel.RootCampaignName = source.RootCampaign.Name;
      }
      target.MessageBody = new MessageBodyViewModel();
      Synchronizer.Synchronize(source.MessageBody, target.MessageBody);
      switch (source.MessageBody.MessageBodyType)
      {
        case MessageBodyType.PlainText:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().PlainTextCampaignType;
          break;
        case MessageBodyType.HtmlText:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().HtmlCampaignType;
          break;
        case MessageBodyType.InternalPage:
          target.CampaignTypeUX = Res.Get<NewslettersResources>().StandardCampaignType;
          break;
      }
      target.CampaignStateUX = Synchronizer.GetCampaignStateUX(source, manager);
      target.CampaignStateClass = Enum.GetName(typeof (CampaignState), (object) target.CampaignState).ToLowerInvariant();
      foreach (MergeTag mergeTag in (IEnumerable<MergeTag>) manager.GetMergeTags())
      {
        MergeTagViewModel target1 = new MergeTagViewModel();
        Synchronizer.Synchronize(mergeTag, target1);
        if (target.MessageBody.MessageBodyType != MessageBodyType.PlainText || !(mergeTag.PropertyName == "UnsubscribeLink"))
          target.MergeTags.Add(target1);
      }
    }

    /// <summary>
    /// Copies the CampaignReport instance (model) to the IssueReportViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.Reports.CampaignReport" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.IssueReportViewModel" /> type.</param>
    [Obsolete]
    internal static void Synchronize(CampaignReport source, IssueReportViewModel target)
    {
      target.CampaignName = source.CampaignName;
      target.SuccessfulDeliveries = source.SuccessfulDeliveries;
      target.TotalTimesOpened = source.TotalTimesOpened;
      target.TotalClicks = source.TotalClicks;
    }

    /// <summary>
    /// Copies the CampaignViewModel instance (view model) to the Campaign (model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type.</param>
    /// <param name="manager">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    public static void Synchronize(
      CampaignViewModel source,
      Campaign target,
      NewslettersManager manager)
    {
      target.FromName = source.FromName;
      target.MessageSubject = source.MessageSubject;
      target.Name = source.Name;
      target.ReplyToEmail = source.ReplyToEmail;
      target.IsReadyForSending = source.IsReadyForSending;
      target.DeliveryDate = source.DeliveryDate;
      target.CampaignState = source.CampaignState;
      target.UseGoogleTracking = source.UseGoogleTracking;
      if (source is IssueViewModel issueViewModel)
      {
        Campaign campaign = manager.GetCampaign(issueViewModel.RootCampaignId);
        target.RootCampaign = campaign;
        if (source.MessageBody.BodyText == null)
          manager.CopyMessageBody(campaign.MessageBody, target.MessageBody);
      }
      if (issueViewModel != null && source.MessageBody.BodyText == null)
        return;
      Synchronizer.Synchronize(source.MessageBody, target.MessageBody, source, manager);
    }

    /// <summary>
    /// Copies the MessageBody instance (model) to the MessageBodyViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> type.</param>
    public static void Synchronize(MessageBody source, MessageBodyViewModel target)
    {
      target.BodyText = LinkParser.ResolveLinks(source.BodyText, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, true, false);
      target.Id = source.Id;
      target.IsTemplate = source.IsTemplate;
      target.MessageBodyType = source.MessageBodyType;
      target.Name = source.Name;
      target.PlainTextVersion = source.PlainTextVersion;
      RawMessageSource rawMessageSource = new RawMessageSource(source, NewslettersManager.GetRootUrl());
      target.RawSourceHtml = rawMessageSource.Source;
      if (target.PlainTextVersion == null)
      {
        if (rawMessageSource.Source == null)
          return;
        target.RawSourcePlainText = HtmlStripper.StripTagsRegexCompiled(rawMessageSource.Source);
      }
      else
        target.RawSourcePlainText = target.PlainTextVersion;
    }

    /// <summary>
    /// Copies the MessageBodyViewModel instance (view model) to the MessageBody (model) and copies content from a template if needed.
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> type.</param>
    /// <param name="campaign">View model of the campaign to which this message body belongs to.</param>
    /// <param name="manager">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    public static void Synchronize(
      MessageBodyViewModel source,
      MessageBody target,
      CampaignViewModel campaign,
      NewslettersManager manager)
    {
      Synchronizer.Synchronize(source, target);
      PageManager manager1 = PageManager.GetManager();
      PageData targetPage = manager1.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Id == target.Id)).SingleOrDefault<PageData>() ?? manager.CreateMessageBodyPage(target);
      Guid campaignTemplateId = campaign.CampaignTemplateId;
      if (campaignTemplateId != Guid.Empty)
      {
        MessageBody messageBody = manager.GetMessageBody(campaignTemplateId);
        target.MessageBodyType = messageBody.MessageBodyType;
        switch (messageBody.MessageBodyType)
        {
          case MessageBodyType.PlainText:
          case MessageBodyType.HtmlText:
            target.BodyText = messageBody.BodyText;
            break;
          case MessageBodyType.InternalPage:
            if (target.CopiedTemplateId != campaignTemplateId)
            {
              PageData pageData = manager1.GetPageData(campaignTemplateId);
              manager1.CopyPageData(pageData, targetPage);
              manager1.SaveChanges();
              target.CopiedTemplateId = campaignTemplateId;
              break;
            }
            break;
        }
      }
      target.BodyText = LinkParser.UnresolveLinks(target.BodyText);
    }

    /// <summary>
    /// Copies the MessageBodyViewModel instance (view model) to the MessageBody (model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> type.</param>
    /// <param name="manager">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    public static void Synchronize(MessageBodyViewModel source, MessageBody target)
    {
      target.BodyText = source.BodyText;
      target.IsTemplate = source.IsTemplate;
      target.MessageBodyType = source.MessageBodyType;
      target.PlainTextVersion = source.PlainTextVersion;
    }

    /// <summary>
    /// Copies the DynamicListSettings instance (model) to the DynamicListSettingsViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.DynamicListSettings" /> type.</param>
    /// <param name="target">Instance of the <see cref="!:DynamiclistSettingsViewModel" /> type.</param>
    public static void Synchronize(DynamicListSettings source, DynamicListSettingsViewModel target)
    {
      target.ConnectionName = source.ConnectionName;
      target.DynamicListProviderName = source.DynamicListProviderName;
      target.EmailMappedField = source.EmailMappedField;
      target.FirstNameMappedField = source.FirstNameMappedField;
      target.LastNameMappedField = source.LastNameMappedField;
      target.ListKey = source.ListKey;
    }

    /// <summary>
    /// Copies the DynamicListSettingsViewModel instance (view model) to the DynamicListSettings (model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.DynamicListSettingsViewModel" /> type.</param>
    /// <param name="target">Instance of the <see cref="!:DynamiclistSettings" /> type.</param>
    public static void Synchronize(DynamicListSettingsViewModel source, DynamicListSettings target)
    {
      target.ConnectionName = source.ConnectionName;
      target.DynamicListProviderName = source.DynamicListProviderName;
      target.EmailMappedField = source.EmailMappedField;
      target.FirstNameMappedField = source.FirstNameMappedField;
      target.LastNameMappedField = source.LastNameMappedField;
      target.ListKey = source.ListKey;
    }

    /// <summary>
    /// Copies the DynamicListInfo (model) to the DynamicListInfoViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.DynamicListInfo" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.DynamicListInfoViewModel" /> type.</param>
    public static void Synchronize(DynamicListInfo source, DynamicListInfoViewModel target)
    {
      target.Key = source.Key;
      target.ProviderName = source.ProviderName;
      target.Title = source.Title;
    }

    /// <summary>
    /// Copies the MergeTag (model) to the MergeTagViewModel (view model).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.MergeTag" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MergeTagViewModel" /> type.</param>
    public static void Synchronize(MergeTag source, MergeTagViewModel target)
    {
      target.DeclaringTypeName = source.DeclaringTypeName;
      target.PropertyName = source.PropertyName;
      target.Title = source.Title;
      target.ComposedTag = source.ComposedTag;
    }

    /// <summary>
    /// Copies the MailingListViewModel (view model) to MailingList (target).
    /// </summary>
    /// <param name="source">Instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> type.</param>
    /// <param name="target">Instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MailingList" /> type.</param>
    /// <param name="manager">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> through which the model is manager.
    /// </param>
    public static void Synchronize(
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
      target.UnsubscribePageId = source.UnsubscribePageId;
      int count = target.DynamicLists.Count;
      for (int index = 0; index < count; ++index)
      {
        DynamicListSettings existingDynamicLists = target.DynamicLists[index];
        if (source.DynamicLists.Where<DynamicListSettingsViewModel>((Func<DynamicListSettingsViewModel, bool>) (dl => dl.ListKey == existingDynamicLists.ListKey && dl.DynamicListProviderName == existingDynamicLists.DynamicListProviderName)).Count<DynamicListSettingsViewModel>() == 0)
        {
          target.DynamicLists.Remove(existingDynamicLists);
          --count;
          --index;
        }
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

    /// <summary>
    /// Generates the UX status of the campaign according to its related issues.
    /// </summary>
    /// <param name="source">The campaign to generate the UX status for.</param>
    /// <param name="manager">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersManager" /> that was used to obtain the model.</param>
    /// <returns>The UX status of the campaign according to its related issues.</returns>
    private static string GetCampaignStateUX(Campaign source, NewslettersManager manager)
    {
      int num1 = manager.GetIssues(source.Id).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 4 || (int) i.CampaignState == 2)).Count<Campaign>();
      int num2 = manager.GetIssues(source.Id).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 3)).Count<Campaign>();
      int num3 = manager.GetIssues(source.Id).Where<Campaign>((Expression<Func<Campaign, bool>>) (i => (int) i.CampaignState == 0 || (int) i.CampaignState == 1)).Count<Campaign>();
      int num4 = manager.GetABCampaigns(source.Id).Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => (int) t.ABTestingStatus == 0)).Count<ABCampaign>();
      int num5 = manager.GetABCampaigns(source.Id).Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => (int) t.ABTestingStatus == 1)).Count<ABCampaign>();
      List<string> values = new List<string>(5);
      if (num1 == 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueOneSent);
      else if (num1 > 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueMultipleSent.Arrange((object) num1));
      if (num2 == 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueStateOneScheduled);
      else if (num2 > 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueStateMultipleScheduled.Arrange((object) num2));
      if (num3 == 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueStateOneDraft);
      else if (num3 > 1)
        values.Add(Res.Get<NewslettersResources>().CampaignIssueStateMultipleDraft.Arrange((object) num3));
      if (num4 == 1)
        values.Add(Res.Get<NewslettersResources>().OneScheduledAbTest);
      else if (num4 > 1)
        values.Add(Res.Get<NewslettersResources>().MultipleScheduledAbTests.Arrange((object) num4));
      if (num5 == 1)
        values.Add(Res.Get<NewslettersResources>().OneDraftAbTest);
      else if (num5 > 1)
        values.Add(Res.Get<NewslettersResources>().MultipleDraftAbTests.Arrange((object) num5));
      return string.Join(", ", (IEnumerable<string>) values);
    }
  }
}
