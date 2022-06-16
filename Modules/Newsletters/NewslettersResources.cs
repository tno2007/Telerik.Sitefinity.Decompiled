// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewslettersResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// Represents string resources for user interface of the newsletters module.
  /// </summary>
  [ObjectInfo("NewslettersResources", ResourceClassId = "NewslettersResources", TitlePlural = "NewslettersResourcesTitlePlural")]
  public class NewslettersResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public NewslettersResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.NewslettersResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public NewslettersResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Newsletters</summary>
    [ResourceEntry("Newsletters", Description = "The title of this class.", LastModified = "2010/09/17", Value = "Email Campaigns")]
    public string Newsletters => this[nameof (Newsletters)];

    /// <summary>Generic Content</summary>
    [ResourceEntry("NewslettersResourcesTitle", Description = "The title of this class.", LastModified = "2011/07/27", Value = "Email Campaigns")]
    public string NewslettersResourcesTitle => this[nameof (NewslettersResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for Generic Content user interface.
    /// </summary>
    [ResourceEntry("NewslettersResourcesDescription", Description = "The description of this class.", LastModified = "2011/07/27", Value = "Contains localizable resources for Email Campaigns user interface.")]
    public string NewslettersResourcesDescription => this[nameof (NewslettersResourcesDescription)];

    /// <summary>The title of the page group node in the navigation</summary>
    [ResourceEntry("PageGroupNodeTitle", Description = "The title of the page group node in the navigation", LastModified = "2010/09/17", Value = "Email Campaigns")]
    public string PageGroupNodeTitle => this[nameof (PageGroupNodeTitle)];

    /// <summary>The url name of the page group node in the navigation</summary>
    [ResourceEntry("PageGroupNodeUrlName", Description = "The url name of the page group node", LastModified = "2010/09/17", Value = "Email Campaigns")]
    public string PageGroupNodeUrlName => this[nameof (PageGroupNodeUrlName)];

    /// <summary>
    /// The description of the page group node in the navigation
    /// </summary>
    [ResourceEntry("PageGroupNodeDescription", Description = "The description of the page group node in the navigation", LastModified = "2010/09/17", Value = "Newsletters module")]
    public string PageGroupNodeDescription => this[nameof (PageGroupNodeDescription)];

    /// <summary>phrase: Create new campaign</summary>
    [ResourceEntry("CreateNewCampaign", Description = "phrase: Create new campaign", LastModified = "2010/09/17", Value = "Create a new campaign")]
    public string CreateNewCampaign => this[nameof (CreateNewCampaign)];

    /// <summary>phrase: Dashboard</summary>
    [ResourceEntry("Dashboard", Description = "word: Dashboard", LastModified = "2010/09/17", Value = "Dashboard")]
    public string Dashboard => this[nameof (Dashboard)];

    /// <summary>word: Campaigns</summary>
    [ResourceEntry("Campaigns", Description = "word: Campaigns", LastModified = "2010/09/17", Value = "Campaigns")]
    public string Campaigns => this[nameof (Campaigns)];

    /// <summary>phrase: Mailing lists</summary>
    [ResourceEntry("MailingLists", Description = "phrase: Mailing lists", LastModified = "2010/09/17", Value = "Mailing lists")]
    public string MailingLists => this[nameof (MailingLists)];

    /// <summary>word: Reports</summary>
    [ResourceEntry("Reports", Description = "word: Reports", LastModified = "2010/09/17", Value = "Reports")]
    public string Reports => this[nameof (Reports)];

    /// <summary>word: Report</summary>
    [ResourceEntry("Report", Description = "word: Report", LastModified = "2012/05/07", Value = "Report")]
    public string Report => this[nameof (Report)];

    /// <summary>word: Settings</summary>
    [ResourceEntry("Settings", Description = "word: Settings", LastModified = "2010/09/17", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Title of the dashboard page</summary>
    [ResourceEntry("DashboardTitle", Description = "Title of the dasboard page", LastModified = "2010/09/18", Value = "Overview")]
    public string DashboardTitle => this[nameof (DashboardTitle)];

    /// <summary>Url name of the dashboard page</summary>
    [ResourceEntry("DashboardUrlName", Description = "Url name of the dashboard page", LastModified = "2010/09/18", Value = "Overview")]
    public string DashboardUrlName => this[nameof (DashboardUrlName)];

    /// <summary>Description of the dashboard page</summary>
    [ResourceEntry("DashboardDescription", Description = "Description of the dashboard page", LastModified = "2010/09/18", Value = "Overview page of the newsletters module")]
    public string DashboardDescription => this[nameof (DashboardDescription)];

    /// <summary>Html title of the dashboard page</summary>
    [ResourceEntry("DashboardHtmlTitle", Description = "Html title of the dashboard page", LastModified = "2010/09/18", Value = "Overview")]
    public string DashboardHtmlTitle => this[nameof (DashboardHtmlTitle)];

    /// <summary>Title of the campaigns page</summary>
    [ResourceEntry("CampaignsTitle", Description = "Title of the campaigns page", LastModified = "2010/09/18", Value = "Campaigns")]
    public string CampaignsTitle => this[nameof (CampaignsTitle)];

    /// <summary>Url name of the campaigns page</summary>
    [ResourceEntry("CampaignsUrlName", Description = "Url name of the campaigns page", LastModified = "2010/09/18", Value = "Campaigns")]
    public string CampaignsUrlName => this[nameof (CampaignsUrlName)];

    /// <summary>Url name of the kendo campaigns page</summary>
    [ResourceEntry("KendoCampaignsUrlName", Description = "Url name of the kendo campaigns page", LastModified = "2010/03/08", Value = "KendoCampaigns")]
    public string KendoCampaignsUrlName => this[nameof (KendoCampaignsUrlName)];

    /// <summary>Description of the campaigns page</summary>
    [ResourceEntry("CampaignsDescription", Description = "Description of the campaigns page", LastModified = "2010/09/18", Value = "Campaigns page")]
    public string CampaignsDescription => this[nameof (CampaignsDescription)];

    /// <summary>Html title of the campaigns page</summary>
    [ResourceEntry("CampaignsHtmlTitle", Description = "Html title of the campaigns page", LastModified = "2010/09/18", Value = "Campaigns")]
    public string CampaginsHtmlTitle => this["CampaignsHtmlTitle"];

    /// <summary>Title of the A/B tests page</summary>
    [ResourceEntry("ABCampaignsTitle", Description = "Title of the A/B tests page", LastModified = "2011/10/20", Value = "A/B tests")]
    public string ABCampaignsTitle => this[nameof (ABCampaignsTitle)];

    /// <summary>A/B test</summary>
    [ResourceEntry("ABTest", Description = "A/B test", LastModified = "2012/06/14", Value = "A/B test")]
    public string ABTest => this[nameof (ABTest)];

    /// <summary>Url name of the A/B tests</summary>
    [ResourceEntry("ABCampaignsUrlName", Description = "Url name of the A/B tests", LastModified = "2011/10/20", Value = "ab-tests")]
    public string ABCampaignsUrlName => this[nameof (ABCampaignsUrlName)];

    /// <summary>Description of the A/B tests page</summary>
    [ResourceEntry("ABCampaignsDescription", Description = "Description of the A/B tests page", LastModified = "2011/10/20", Value = "A/B tests page")]
    public string ABCampaignsDescription => this[nameof (ABCampaignsDescription)];

    /// <summary>Html title of the A/B tests page</summary>
    [ResourceEntry("ABCampaignsHtmlTitle", Description = "Html title of the A/B tests page", LastModified = "2011/10/20", Value = "A/B tests")]
    public string ABCampaignsHtmlTitle => this[nameof (ABCampaignsHtmlTitle)];

    /// <summary>Title of the A/B test report page</summary>
    [ResourceEntry("ABCampaignReportPage", Description = "Title of the A/B test report page", LastModified = "2011/03/05", Value = "A/B test report")]
    public string ABCampaignReportPage => this[nameof (ABCampaignReportPage)];

    /// <summary>Title of the A/B test report page</summary>
    [ResourceEntry("AbCampaignReportTitle", Description = "Title of the A/B test report", LastModified = "2011/04/11", Value = "A/B test report title")]
    public string AbCampaignReportTitle => this[nameof (AbCampaignReportTitle)];

    /// <summary>Url name of the A/B test report page</summary>
    [ResourceEntry("ABCampaignReportUrlName", Description = "Url name of the A/B test report page", LastModified = "2011/03/05", Value = "report")]
    public string ABCampaignReportUrlName => this[nameof (ABCampaignReportUrlName)];

    /// <summary>Descriptin of the A/B test report page</summary>
    [ResourceEntry("AVCampaignReportDescription", Description = "Descriptin of the A/B test report page", LastModified = "2011/03/05", Value = "A/B test report")]
    public string AVCampaignReportDescription => this[nameof (AVCampaignReportDescription)];

    /// <summary>Html title of the A/B test report page</summary>
    [ResourceEntry("ABCampaignReportHtmlTitle", Description = "Html title of the A/B test report page", LastModified = "2011/11/01", Value = "A/B test report")]
    public string ABCampaignReportHtmlTitle => this[nameof (ABCampaignReportHtmlTitle)];

    /// <summary>Phrase: A/B test {0} report</summary>
    [ResourceEntry("ABTestReport", Description = "Phrase: A/B test {0} report", LastModified = "2012/03/08", Value = "A/B test {0} report")]
    public string ABTestReport => this[nameof (ABTestReport)];

    /// <summary>Title of the mailing lists page</summary>
    [ResourceEntry("MailingListsTitle", Description = "Title of the mailing lists page", LastModified = "2010/09/18", Value = "Mailing lists")]
    public string MailingListsTitle => this[nameof (MailingListsTitle)];

    /// <summary>Url name of the mailing lists page</summary>
    [ResourceEntry("MailingListsUrlName", Description = "Url name of the mailing lists page", LastModified = "2010/09/18", Value = "MailingLists")]
    public string MailingListsUrlName => this[nameof (MailingListsUrlName)];

    /// <summary>
    /// Description of the mailing lists page of the newsletters module
    /// </summary>
    [ResourceEntry("MailingListsDescription", Description = "Description of the mailing lists page of the newsletter module", LastModified = "2010/09/18", Value = "Mailing lists page of the newsletters module")]
    public string MailingListsDescription => this[nameof (MailingListsDescription)];

    /// <summary>
    /// Html title of the mailing lists page of the newsletters module
    /// </summary>
    [ResourceEntry("MailingListsHtmlTitle", Description = "Html title of the mailing lists page of the newsletters module", LastModified = "2010/09/18", Value = "Mailing lists")]
    public string MailingListsHtmlTitle => this[nameof (MailingListsHtmlTitle)];

    /// <summary>
    /// Title of the templates page of the newsletters module.
    /// </summary>
    [ResourceEntry("TemplatesTitle", Description = "Title of the templates page of the newsletters module.", LastModified = "2010/12/17", Value = "Message templates")]
    public string TemplatesTitle => this[nameof (TemplatesTitle)];

    /// <summary>
    /// Url name of the templates page of the newsletters module.
    /// </summary>
    [ResourceEntry("TemplatesUrlName", Description = "Url name of the templates page of the newsletters module.", LastModified = "2010/12/17", Value = "Templates")]
    public string TemplatesUrlName => this[nameof (TemplatesUrlName)];

    /// <summary>
    /// Description of the templates page of the newsletters module.
    /// </summary>
    [ResourceEntry("TemplatesDescription", Description = "Description of the templates page of the newsletters module.", LastModified = "2010/12/17", Value = "Templates page")]
    public string TemplatesDescription => this[nameof (TemplatesDescription)];

    /// <summary>Title of the reports page of the newsletters module</summary>
    [ResourceEntry("ReportsTitle", Description = "Title of the reports page of the newsletters module", LastModified = "2010/09/18", Value = "Reports")]
    public string ReportsTitle => this[nameof (ReportsTitle)];

    /// <summary>
    /// Url name of the reports page of the newsletters module
    /// </summary>
    [ResourceEntry("ReportsUrlName", Description = "Url name of the reports page of the newsletters module", LastModified = "2010/09/18", Value = "Reports")]
    public string ReportsUrlName => this[nameof (ReportsUrlName)];

    /// <summary>
    /// Description of the reports page of the newsletters module
    /// </summary>
    [ResourceEntry("ReportsDescription", Description = "Description of the reports page of the newsletters module", LastModified = "2010/09/18", Value = "Reports page")]
    public string ReportsDescription => this[nameof (ReportsDescription)];

    /// <summary>
    /// Html title of the reports page of the newsletters module
    /// </summary>
    [ResourceEntry("ReportsHtmlTitle", Description = "Html title of the reports page of the newsletters module", LastModified = "2010/09/18", Value = "Reports")]
    public string ReportsHtmlTitle => this[nameof (ReportsHtmlTitle)];

    /// <summary>
    /// Title of the campaign report page of the newsletters module.
    /// </summary>
    [ResourceEntry("CampaignReportTitle", Description = "Title of the campaign report page of the newsletters module.", LastModified = "2010/12/20", Value = "Campaign report")]
    public string CampaignReportTitle => this[nameof (CampaignReportTitle)];

    /// <summary>
    /// Url name of the campaign report page of the newsletters module.
    /// </summary>
    [ResourceEntry("CampaignReportUrlName", Description = "Url name of the campaign report page of the newsletters module.", LastModified = "2010/12/20", Value = "campaign-report")]
    public string CampaignReportUrlName => this[nameof (CampaignReportUrlName)];

    /// <summary>
    /// Description of the campaign report page of the newsletters module.
    /// </summary>
    [ResourceEntry("CampaignReportDescription", Description = "Description of the campaign report page of the newsletters module.", LastModified = "2010/12/20", Value = "Page that displays the report for a single campaign")]
    public string CampaignReportDescription => this[nameof (CampaignReportDescription)];

    /// <summary>
    /// Html title of the campaign report page of the newsletters module.
    /// </summary>
    [ResourceEntry("CampaignReportHtmlTitle", Description = "Html title of the campaign report page of the newsletters module", LastModified = "2010/12/20", Value = "Campaign report")]
    public string CampaignReportHtmlTitle => this[nameof (CampaignReportHtmlTitle)];

    /// <summary>
    /// Title of the campaign overview page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("CampaignOverviewTitle", Description = "Title of the campaign overview page of the Email Campaigns module.", LastModified = "2010/12/20", Value = "Campaign overview")]
    public string CampaignOverviewTitle => this[nameof (CampaignOverviewTitle)];

    /// <summary>
    /// Url name of the campaign overview page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("CampaignOverviewUrlName", Description = "Url name of the campaign overview page of the Email Campaigns module.", LastModified = "2012/05/10", Value = "campaign-overview")]
    public string CampaignOverviewUrlName => this[nameof (CampaignOverviewUrlName)];

    /// <summary>
    /// Description of the campaign overview page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("CampaignOverviewDescription", Description = "Description of the campaign overview page of the Email Campaigns module.", LastModified = "2012/05/10", Value = "Page that displays the report for a single campaign.")]
    public string CampaignOverviewDescription => this[nameof (CampaignOverviewDescription)];

    /// <summary>
    /// Html title of the campaign overview page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("CampaignOverviewHtmlTitle", Description = "Html title of the campaign overview page of the Email Campaigns module.", LastModified = "2012/05/04", Value = "Campaign report")]
    public string CampaignOverviewHtmlTitle => this[nameof (CampaignOverviewHtmlTitle)];

    /// <summary>
    /// Title of the issue report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("IssueReportTitle", Description = "Title of the issue report page of the Email Campaigns module.", LastModified = "2012/05/08", Value = "Issue report")]
    public string IssueReportTitle => this[nameof (IssueReportTitle)];

    /// <summary>
    /// Url name of the issue report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("IssueReportUrlName", Description = "Url name of the issue report page of the Email Campaigns module.", LastModified = "2012/05/08", Value = "issue-report")]
    public string IssueReportUrlName => this[nameof (IssueReportUrlName)];

    /// <summary>
    /// Description of the issue report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("IssueReportDescription", Description = "Description of the issue report page of the Email Campaigns module.", LastModified = "2012/05/08", Value = "Issue Report Description")]
    public string IssueReportDescription => this[nameof (IssueReportDescription)];

    /// <summary>
    /// Html title of the issue report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("IssueReportHtmlTitle", Description = "Html title of the issue report page of the Email Campaigns module.", LastModified = "2012/05/08", Value = "Issue report")]
    public string IssueReportHtmlTitle => this[nameof (IssueReportHtmlTitle)];

    /// <summary>
    /// Title of the subscriber report page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscriberReportTitle", Description = "Title of the subscriber report page of the newsletters module", LastModified = "2011/03/05", Value = "Subscriber report")]
    public string SubscriberReportTitle => this[nameof (SubscriberReportTitle)];

    /// <summary>
    /// Url name of the subscriber report page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscriberReportUrlName", Description = "Url name of the subscriber report page of the newsletters module", LastModified = "2011/03/05", Value = "subscriber-report")]
    public string SubscriberReportUrlName => this[nameof (SubscriberReportUrlName)];

    /// <summary>Page that displays the report for a single subscriber</summary>
    [ResourceEntry("SubscriberReportDescription", Description = "Page that displays the report for a single subscriber", LastModified = "2011/03/05", Value = "Page that displays the report for a single subscriber")]
    public string SubscriberReportDescription => this[nameof (SubscriberReportDescription)];

    /// <summary>
    /// Html title of the subscriber report page of the newsletters module.
    /// </summary>
    [ResourceEntry("SubscriberReportHtmlTitle", Description = "Html title of the subscriber report page of the newsletters module.", LastModified = "2011/03/05", Value = "Subscriber report")]
    public string SubscriberReportHtmlTitle => this[nameof (SubscriberReportHtmlTitle)];

    /// <summary>Title of the settings page of the newsletters module</summary>
    [ResourceEntry("SettingsTitle", Description = "Title of the settings page of the newsletters module", LastModified = "2010/09/18", Value = "Settings")]
    public string SettingsTitle => this[nameof (SettingsTitle)];

    /// <summary>
    /// Url name of the settings page of the newsletters module
    /// </summary>
    [ResourceEntry("SettingsUrlName", Description = "Url name of the settings page of the newsletters module", LastModified = "2010/09/18", Value = "Settings")]
    public string SettingsUrlName => this[nameof (SettingsUrlName)];

    /// <summary>
    /// Description of the settings page of the newsletters module
    /// </summary>
    [ResourceEntry("SettingsDescription", Description = "Description of the settings page of the newsletters module", LastModified = "2010/09/18", Value = "Settings page of the newsletters module")]
    public string SettingsDescription => this[nameof (SettingsDescription)];

    /// <summary>
    /// Html title of the settings page of the newsletters module
    /// </summary>
    [ResourceEntry("SettingsHtmlTitle", Description = "Html title of the settings page of the newsletters module", LastModified = "2010/09/18", Value = "Settings")]
    public string SettingsHtmlTitle => this["SettingsTitle"];

    /// <summary>phrase: Back to mailing lists</summary>
    [ResourceEntry("BackToMailingLists", Description = "phrase: Back to mailing lists", LastModified = "2010/09/18", Value = "Back to mailing lists")]
    public string BackToMailingLists => this[nameof (BackToMailingLists)];

    /// <summary>phrase: Create a mailing list</summary>
    [ResourceEntry("CreateAMailingList", Description = "phrase: Create a mailing list", LastModified = "2010/09/18", Value = "Create a mailing list")]
    public string CreateAMailingList => this[nameof (CreateAMailingList)];

    /// <summary>phrase: Create this mailing list</summary>
    [ResourceEntry("CreateThisMailingList", Description = "phrase: Create this mailing list", LastModified = "2010/09/18", Value = "Create this mailing list")]
    public string CreateThisMailingList => this[nameof (CreateThisMailingList)];

    /// <summary>phrase: Mailing list title</summary>
    [ResourceEntry("MailingListTitle", Description = "phrase: Mailing list title", LastModified = "2010/09/18", Value = "Mailing list title")]
    public string MailingListTitle => this[nameof (MailingListTitle)];

    /// <summary>phrase: From name</summary>
    [ResourceEntry("DefaultFromName", Description = "phrase: From name", LastModified = "2011/10/10", Value = "From name")]
    public string DefaultFromName => this[nameof (DefaultFromName)];

    /// <summary>phrase: Reply-to email</summary>
    [ResourceEntry("DefaultReplyToEmail", Description = "phrase: Reply-to email", LastModified = "2011/10/10", Value = "Reply-to email")]
    public string DefaultReplyToEmail => this[nameof (DefaultReplyToEmail)];

    /// <summary>phrase: Subject</summary>
    [ResourceEntry("DefaultSubject", Description = "phrase: Subject", LastModified = "2011/10/10", Value = "Subject")]
    public string DefaultSubject => this[nameof (DefaultSubject)];

    /// <summary>
    /// phrase: Text to remind your subscribers how they got on your list
    /// </summary>
    [ResourceEntry("SubscriptionReminder", Description = "phrase: Text to remind your subscribers how they got on your list", LastModified = "2011/10/10", Value = "Text to remind your subscribers how they got on your list")]
    public string SubscriptionReminder => this[nameof (SubscriptionReminder)];

    /// <summary>word: Title</summary>
    [ResourceEntry("Title", Description = "word: Title", LastModified = "2010/09/18", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>
    /// phrase: Are you sure you want to delete this mailing list?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteMailingList", Description = "phrase: Are you sure you want to delete this mailing list?", LastModified = "2011/10/24", Value = "Are you sure you want to delete this mailing list?")]
    public string AreYouSureDeleteMailingList => this[nameof (AreYouSureDeleteMailingList)];

    /// <summary>
    /// phrase: Are you sure you want to delete this mailing list(s)?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteMailingLists", Description = "phrase: Are you sure you want to delete this mailing list(s)?", LastModified = "2011/10/24", Value = "Are you sure you want to delete this mailing list(s)?")]
    public string AreYouSureDeleteMailingLists => this[nameof (AreYouSureDeleteMailingLists)];

    /// <summary>word: Subscribers</summary>
    [ResourceEntry("SubscribersCount", Description = "word: Subscribers", LastModified = "2010/09/18", Value = "Subscribers")]
    public string SubscribersCount => this[nameof (SubscribersCount)];

    /// <summary>phrase: Add subscriber</summary>
    [ResourceEntry("AddSubscriber", Description = "phrase: Add subscriber", LastModified = "2010/09/18", Value = "Add subscriber")]
    public string AddSubscriber => this[nameof (AddSubscriber)];

    /// <summary>phrase: Import subscribers</summary>
    [ResourceEntry("ImportSubscribers", Description = "phrase: Import subscribers", LastModified = "2010/09/18", Value = "Import subscribers")]
    public string ImportSubscribers => this[nameof (ImportSubscribers)];

    /// <summary>phrase: Manage subscribers</summary>
    [ResourceEntry("ManageSubscribers", Description = "phrase: Manage subscribers", LastModified = "2010/09/18", Value = "Manage subscribers")]
    public string ManageSubscribers => this[nameof (ManageSubscribers)];

    /// <summary>phrase: Show subscribers</summary>
    [ResourceEntry("ShowSubscribers", Description = "phrase: Show subscribers", LastModified = "2010/09/18", Value = "Show subscribers")]
    public string ShowSubscribers => this[nameof (ShowSubscribers)];

    /// <summary>phrase: Create a subscriber</summary>
    [ResourceEntry("CreateASubscriber", Description = "phrase: Create a subscriber", LastModified = "2010/09/18", Value = "Create a subscriber")]
    public string CreateASubscriber => this[nameof (CreateASubscriber)];

    /// <summary>word: First name</summary>
    [ResourceEntry("FirstName", Description = "word: First name", LastModified = "2010/09/19", Value = "First name")]
    public string FirstName => this[nameof (FirstName)];

    /// <summary>word: First name</summary>
    [ResourceEntry("FirstNamePublicForm", Description = "word: First name", LastModified = "2010/09/19", Value = "First name <em class='sfNote'>(optional)</em>")]
    public string FirstNamePublicForm => this[nameof (FirstNamePublicForm)];

    /// <summary>word: Last name</summary>
    [ResourceEntry("LastName", Description = "word: Last name", LastModified = "2010/09/19", Value = "Last name")]
    public string LastName => this[nameof (LastName)];

    /// <summary>word: Last name</summary>
    [ResourceEntry("LastNamePublicForm", Description = "word: Last name", LastModified = "2010/09/19", Value = "Last name <em class='sfNote'>(optional)</em>")]
    public string LastNamePublicForm => this[nameof (LastNamePublicForm)];

    /// <summary>word: Email</summary>
    [ResourceEntry("Email", Description = "word: Email", LastModified = "2010/09/19", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>word: Create this subscriber</summary>
    [ResourceEntry("CreateThisSubscriber", Description = "phrase: Create this subscriber", LastModified = "2010/09/19", Value = "Create this subscriber")]
    public string CreateThisSubscriber => this[nameof (CreateThisSubscriber)];

    /// <summary>word: Subscribers</summary>
    [ResourceEntry("Subscribers", Description = "word: Subscribers", LastModified = "2010/09/19", Value = "Subscribers")]
    public string Subscribers => this[nameof (Subscribers)];

    /// <summary>
    /// Title of the subscribers page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscribersTitle", Description = "Title of the subscribers page of the newsletters module", LastModified = "2010/09/19", Value = "Subscribers")]
    public string SubscribersTitle => this[nameof (SubscribersTitle)];

    /// <summary>
    /// Url name of the subscribers page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscribersUrlName", Description = "Url name of the subscribers page of the newsletters module", LastModified = "2010/09/19", Value = "Subscribers")]
    public string SubscribersUrlName => this[nameof (SubscribersUrlName)];

    /// <summary>
    /// Description of the subscribers page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscribersDescription", Description = "Description of the subscribers page of the newsletters module", LastModified = "2010/09/19", Value = "Page for managing newsletter subscribers")]
    public string SubscribersDescription => this[nameof (SubscribersDescription)];

    /// <summary>
    /// Html title of the subscribers page of the newsletters module
    /// </summary>
    [ResourceEntry("SubscribersHtmlTitle", Description = "Html title of the subscribers page of the newsletters module", LastModified = "2010/09/20", Value = "Subscribers")]
    public string SubscribersHtmlTitle => this[nameof (SubscribersHtmlTitle)];

    /// <summary>phrase: Back to subscribers</summary>
    [ResourceEntry("BackToSubscribers", Description = "phrase: Back to subscribers", LastModified = "2010/09/20", Value = "Back to subscribers")]
    public string BackToSubscribers => this[nameof (BackToSubscribers)];

    /// <summary>
    /// phrase: Are you sure you want to delete this subscriber?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteSubscriber", Description = "phrase: Are you sure you want to delete this subscriber?", LastModified = "2011/10/24", Value = "Are you sure you want to delete this subscriber?")]
    public string AreYouSureDeleteSubscriber => this[nameof (AreYouSureDeleteSubscriber)];

    /// <summary>
    /// phrase: {0} users will be deleted. Are you sure you want to delete {0} users?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteSubscribers", Description = "phrase: {0} users will be deleted. Are you sure you want to delete {0} users?", LastModified = "2011/10/24", Value = "{0} users will be deleted. Are you sure you want to delete {0} users?")]
    public string AreYouSureDeleteSubscribers => this[nameof (AreYouSureDeleteSubscribers)];

    /// <summary>phrase: Create a campaign</summary>
    [ResourceEntry("CreateACampaign", Description = "phrase: Create a campaign", LastModified = "2010/09/21", Value = "Create a campaign")]
    public string CreateACampaign => this[nameof (CreateACampaign)];

    /// <summary>phrase: Standard campaign</summary>
    [ResourceEntry("StandardCampaign", Description = "phrase: Standard campaign", LastModified = "2010/09/21", Value = "Standard campaign")]
    public string StandardCampaign => this[nameof (StandardCampaign)];

    /// <summary>phrase: HTML Campaign</summary>
    [ResourceEntry("HtmlCampaign", Description = "phrase: HTML campaign", LastModified = "2010/09/21", Value = "HTML Campaign")]
    public string HtmlCampaign => this[nameof (HtmlCampaign)];

    /// <summary>phrase: Plain text campaign</summary>
    [ResourceEntry("PlainTextCampaign", Description = "phrase: Plain text campaign", LastModified = "2010/09/21", Value = "Plain text campaign")]
    public string PlainTextCampaign => this[nameof (PlainTextCampaign)];

    /// <summary>phrase: A/B Test</summary>
    [ResourceEntry("ABCampaign", Description = "phrase: A/B Test", LastModified = "2010/09/21", Value = "A/B Test")]
    public string ABCampaign => this[nameof (ABCampaign)];

    /// <summary>phrase: Back to dashboard</summary>
    [ResourceEntry("BackToDashboard", Description = "phrase: Back to dashboard", LastModified = "2010/09/21", Value = "Back to dashboard")]
    public string BackToDashboard => this[nameof (BackToDashboard)];

    /// <summary>phrase: Back to campaigns</summary>
    [ResourceEntry("BackToCampaigns", Description = "phrase: Back to campaigns", LastModified = "2010/09/21", Value = "Back to campaigns")]
    public string BackToCampaigns => this[nameof (BackToCampaigns)];

    /// <summary>word: Next</summary>
    [ResourceEntry("Next", Description = "word: Next", LastModified = "2010/09/21", Value = "Next")]
    public string Next => this[nameof (Next)];

    /// <summary>phrase: Go to add content</summary>
    [ResourceEntry("GoAddContent", Description = "phrase: Go to add content", LastModified = "2012/05/09", Value = "Go to add content")]
    public string GoAddContent => this[nameof (GoAddContent)];

    /// <summary>phrase: Create and go to Campaigns</summary>
    [ResourceEntry("CreateAndGoEmailCampaigns", Description = "phrase: Create and go to Campaigns", LastModified = "2012/06/13", Value = "Create and go to Campaigns")]
    public string CreateAndGoEmailCampaigns => this[nameof (CreateAndGoEmailCampaigns)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "word: Cancel", LastModified = "2010/03/09", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>word: Previous</summary>
    [ResourceEntry("Previous", Description = "word: Previous", LastModified = "2010/09/21", Value = "Previous")]
    public string Previous => this[nameof (Previous)];

    /// <summary>phrase: Campaign info</summary>
    [ResourceEntry("CampaignInfo", Description = "phrase: Campaign info", LastModified = "2010/09/21", Value = "Campaign info")]
    public string CampaignInfo => this[nameof (CampaignInfo)];

    /// <summary>phrase: Campaign name</summary>
    [ResourceEntry("CampaignName", Description = "phrase: Campaign name", LastModified = "2010/09/21", Value = "Campaign name")]
    public string CampaignName => this[nameof (CampaignName)];

    /// <summary>phrase: Message subject</summary>
    [ResourceEntry("MessageSubject", Description = "phrase: Message subject", LastModified = "2010/09/21", Value = "Message subject")]
    public string MessageSubject => this[nameof (MessageSubject)];

    /// <summary>phrase: From name</summary>
    [ResourceEntry("FromName", Description = "phrase: From name", LastModified = "2010/09/21", Value = "From name")]
    public string FromName => this[nameof (FromName)];

    /// <summary>phrase: Reply to email</summary>
    [ResourceEntry("ReplyToEmail", Description = "phrase: Reply to email", LastModified = "2010/09/21", Value = "Reply to email")]
    public string ReplyToEmail => this[nameof (ReplyToEmail)];

    /// <summary>phrase: Select the mailing list</summary>
    [ResourceEntry("SelectMailingListStepTitle", Description = "phrase: Select the mailing list", LastModified = "2010/09/21", Value = "Select the mailing list")]
    public string SelectMailingListStepTitle => this[nameof (SelectMailingListStepTitle)];

    /// <summary>phrase: Compose message</summary>
    [ResourceEntry("ComposeMessage", Description = "phrase: Compose message", LastModified = "2010/09/21", Value = "Compose message")]
    public string ComposeMessage => this[nameof (ComposeMessage)];

    /// <summary>phrase: Confirm campaign</summary>
    [ResourceEntry("ConfirmCampaign", Description = "phrase: Confirm campaign", LastModified = "2010/09/21", Value = "Confirm campaign")]
    public string ConfirmCampaign => this[nameof (ConfirmCampaign)];

    /// <summary>phrase: Save campaign and exit</summary>
    [ResourceEntry("SaveCampaign", Description = "phrase: Save campaign and exit", LastModified = "2011/04/11", Value = "Save as draft")]
    public string SaveCampaign => this[nameof (SaveCampaign)];

    /// <summary>phrase: Send this issue</summary>
    [ResourceEntry("SendThisIssue", Description = "phrase: Send this issue", LastModified = "2012/05/03", Value = "Send this issue")]
    public string SendThisIssue => this[nameof (SendThisIssue)];

    /// <summary>phrase: Send this campaign</summary>
    [ResourceEntry("SendThisCampaign", Description = "phrase: Send this campaign", LastModified = "2011/04/11", Value = "Send this campaign")]
    public string SendThisCampaign => this[nameof (SendThisCampaign)];

    /// <summary>word: Preview</summary>
    [ResourceEntry("PreviewCampaign", Description = "word: Preview", LastModified = "2010/09/22", Value = "Preview")]
    public string PreviewCampaign => this[nameof (PreviewCampaign)];

    /// <summary>phrase: Issue preview</summary>
    [ResourceEntry("IssuePreview", Description = "phrase: Issue preview", LastModified = "2012/05/04", Value = "Issue preview")]
    public string IssuePreview => this[nameof (IssuePreview)];

    /// <summary>phrase: Preview message</summary>
    [ResourceEntry("PreviewMessage", Description = "phrase: Preview message", LastModified = "2011/03/05", Value = "Preview message")]
    public string PreviewMessage => this[nameof (PreviewMessage)];

    /// <summary>phrase: Send check</summary>
    [ResourceEntry("SendTest", Description = "phrase: Send check", LastModified = "2012/06/20", Value = "Send check")]
    public string SendTest => this[nameof (SendTest)];

    /// <summary>word: Send</summary>
    [ResourceEntry("Send", Description = "word: Send", LastModified = "2010/09/22", Value = "Send")]
    public string Send => this[nameof (Send)];

    /// <summary>phrase: Schedule delivery</summary>
    [ResourceEntry("ScheduleDelivery", Description = "phrase: Schedule delivery", LastModified = "2010/09/22", Value = "Schedule delivery")]
    public string ScheduleDelivery => this[nameof (ScheduleDelivery)];

    /// <summary>phrase: Campaign type</summary>
    [ResourceEntry("CampaignType", Description = "phrase: Campaign type", LastModified = "2010/09/22", Value = "Campaign type")]
    public string CampaignType => this[nameof (CampaignType)];

    /// <summary>User friendly name for the standard campaign type</summary>
    [ResourceEntry("StandardCampaignType", Description = "User friendly name for the standard campaign type", LastModified = "2011/11/03", Value = "Template with widgets")]
    public string StandardCampaignType => this[nameof (StandardCampaignType)];

    /// <summary>User friendly name for the html campaign type</summary>
    [ResourceEntry("HtmlCampaignType", Description = "User friendly name for the html campaign type", LastModified = "2011/11/03", Value = "Rich text (HTML)")]
    public string HtmlCampaignType => this[nameof (HtmlCampaignType)];

    /// <summary>User friendly name for the plain text campaign type</summary>
    [ResourceEntry("PlainTextCampaignType", Description = "User friendly name for the plain text campaign type", LastModified = "2010/09/22", Value = "Plain text")]
    public string PlainTextCampaignType => this[nameof (PlainTextCampaignType)];

    /// <summary>User friendly name for the A/B test type</summary>
    [ResourceEntry("ABCampaignType", Description = "User friendly name for the A/B test type", LastModified = "2010/09/22", Value = "A/B Test")]
    public string ABCampaignType => this[nameof (ABCampaignType)];

    /// <summary>phrase : Delivery options</summary>
    [ResourceEntry("DeliveryOptions", Description = "phrase : Delivery options", LastModified = "2010/09/23", Value = "Delivery options")]
    public string DeliveryOptions => this[nameof (DeliveryOptions)];

    /// <summary>
    /// phrase: Are you sure you want to delete this campaign?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteCampaign", Description = "phrase: Are you sure you want to delete this campaign?", LastModified = "2012/01/05", Value = "Are you sure you want to delete this campaign?")]
    public string AreYouSureDeleteCampaign => this[nameof (AreYouSureDeleteCampaign)];

    /// <summary>phrase: Are you sure you want to delete this issue?</summary>
    [ResourceEntry("AreYouSureDeleteIssue", Description = "phrase: Are you sure you want to delete this issue?", LastModified = "2012/06/05", Value = "Are you sure you want to delete this issue?")]
    public string AreYouSureDeleteIssue => this[nameof (AreYouSureDeleteIssue)];

    /// <summary>
    /// phrase: Are you sure you want to delete these campaigns?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteCampaigns", Description = "phrase: Are you sure you want to delete these campaigns?", LastModified = "2012/01/05", Value = "Are you sure you want to delete these campaigns?")]
    public string AreYouSureDeleteCampaigns => this[nameof (AreYouSureDeleteCampaigns)];

    /// <summary>phrase: Send check message</summary>
    [ResourceEntry("SendTestMessage", Description = "phrase: Send check message", LastModified = "2012/06/14", Value = "Send check message")]
    public string SendTestMessage => this[nameof (SendTestMessage)];

    /// <summary>phrase: Email addresses</summary>
    [ResourceEntry("EnterTestEmailAddresses", Description = "phrase: Email addresses", LastModified = "2010/09/24", Value = "Enter email address to which you'd like send test campaign. If using more than one, separate with comma (,).")]
    public string EnterTestEmailAddresses => this[nameof (EnterTestEmailAddresses)];

    /// <summary>
    /// phrase: SMTP server settings are configured under Advanced Settings, on the Notifications profile which matches the name specified here.
    /// </summary>
    [ResourceEntry("SmtpNotificationsInfo", Description = "phrase: SMTP server settings are configured under Advanced Settings, on the Notifications profile which matches the name specified here.", LastModified = "2012/07/03", Value = "SMTP server settings are configured under Advanced Settings, on the Notifications profile which matches the name specified here.")]
    public string GetPropertiesInternal => this[nameof (GetPropertiesInternal)];

    /// <summary>phrase: SMTP Settings</summary>
    [ResourceEntry("SmtpSettings", Description = "phrase: SMTP Settings", LastModified = "2010/09/27", Value = "SMTP settings")]
    public string SmtpSettings => this[nameof (SmtpSettings)];

    /// <summary>word: Host</summary>
    [ResourceEntry("Host", Description = "word: Host", LastModified = "2010/09/27", Value = "Host")]
    public string Host => this[nameof (Host)];

    /// <summary>word: Port</summary>
    [ResourceEntry("Port", Description = "word: Port", LastModified = "2010/09/27", Value = "Port")]
    public string Port => this[nameof (Port)];

    /// <summary>word: Username</summary>
    [ResourceEntry("Username", Description = "word: Username", LastModified = "2010/09/27", Value = "Username")]
    public string Username => this[nameof (Username)];

    /// <summary>word: Password</summary>
    [ResourceEntry("Password", Description = "word: Password", LastModified = "2010/09/27", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>word: Domain</summary>
    [ResourceEntry("Domain", Description = "word: Domain", LastModified = "2010/09/27", Value = "Domain")]
    public string Domain => this[nameof (Domain)];

    /// <summary>phrase: Enable SSL</summary>
    [ResourceEntry("EnableSSL", Description = "word: Enable SSL", LastModified = "2010/09/27", Value = "Enable SSL?")]
    public string EnableSSL => this[nameof (EnableSSL)];

    /// <summary>phrase: Save this settings</summary>
    [ResourceEntry("SaveThisSettings", Description = "phrase: Save this settings", LastModified = "2010/09/27", Value = "Save this settings")]
    public string SaveThisSettings => this[nameof (SaveThisSettings)];

    /// <summary>phrase: Define the content of the message</summary>
    [ResourceEntry("DefineTheContentOfTheMessage", Description = "phrase: Define the content of the message", LastModified = "2010/09/27", Value = "Define the content of the message")]
    public string DefineTheContentOfTheMessage => this[nameof (DefineTheContentOfTheMessage)];

    /// <summary>phrase: Compose message in editor</summary>
    [ResourceEntry("ComposeMessageInEditor", Description = "phrase: Compose message in editor", LastModified = "2010/09/29", Value = "Compose message in editor")]
    public string ComposeMessageInEditor => this[nameof (ComposeMessageInEditor)];

    /// <summary>phrase: Launch editor</summary>
    [ResourceEntry("LaunchEditor", Description = "phrase: Launch editor", LastModified = "2010/09/29", Value = "Launch editor")]
    public string LaunchEditor => this[nameof (LaunchEditor)];

    /// <summary>phrase: Or elipsis</summary>
    [ResourceEntry("OrElipsis", Description = "Or with an elipsis", LastModified = "2010/09/29", Value = "Or...")]
    public string OrElipsis => this[nameof (OrElipsis)];

    /// <summary>phrase: Select a page</summary>
    [ResourceEntry("SelectAPage", Description = "phrase: Select a page", LastModified = "2010/09/29", Value = "Select a page")]
    public string SelectAPage => this[nameof (SelectAPage)];

    /// <summary>Text of the link for selecting a page</summary>
    [ResourceEntry("SelectAPageLink", Description = "Text of the link for selecting a page", LastModified = "2010/09/29", Value = "Select")]
    public string SelectAPageLink => this[nameof (SelectAPageLink)];

    /// <summary>phrase: Mailing List</summary>
    [ResourceEntry("MailingList", Description = "phrase: Mailing list", LastModified = "2010/10/10", Value = "Mailing list")]
    public string MailingList => this[nameof (MailingList)];

    /// <summary>phrase: Current campaigns</summary>
    [ResourceEntry("CurrentCampaigns", Description = "phrase: Current campaigns", LastModified = "2010/10/10", Value = "Current campaigns")]
    public string CurrentCampaigns => this[nameof (CurrentCampaigns)];

    /// <summary>word: Today</summary>
    [ResourceEntry("Today", Description = "word: Today", LastModified = "2010/10/10", Value = "Today")]
    public string Today => this[nameof (Today)];

    /// <summary>phrase: This week</summary>
    [ResourceEntry("ThisWeek", Description = "phrase: This week", LastModified = "2010/10/10", Value = "This week")]
    public string ThisWeek => this[nameof (ThisWeek)];

    /// <summary>phrase: This month</summary>
    [ResourceEntry("ThisMonth", Description = "phrase: This month", LastModified = "2010/10/10", Value = "This month")]
    public string ThisMonth => this[nameof (ThisMonth)];

    /// <summary>phrase: Scheduled campaigns</summary>
    [ResourceEntry("ScheduledCampaigns", Description = "phrase: Scheduled campaigns", LastModified = "2010/10/10", Value = "Scheduled campaigns")]
    public string ScheduledCampaigns => this[nameof (ScheduledCampaigns)];

    /// <summary>phrase: Total number of subscribers</summary>
    [ResourceEntry("TotalNumberOfSubscribers", Description = "phrase: Total number of subscribers", LastModified = "2010/10/10", Value = "Total number of subscribers")]
    public string TotalNumberOfSubscribers => this[nameof (TotalNumberOfSubscribers)];

    /// <summary>phrase: New subscibers</summary>
    [ResourceEntry("NewSubscribers", Description = "phrase: New subscribers", LastModified = "2010/10/10", Value = "New subscribers")]
    public string NewSubscribers => this[nameof (NewSubscribers)];

    /// <summary>phrase: Unsubscribed</summary>
    [ResourceEntry("Unsubscribed", Description = "word: Unsubscribed", LastModified = "2010/10/10", Value = "Unsubscribed")]
    public string Unsubscribed => this[nameof (Unsubscribed)];

    /// <summary>phrase: Subscribers activity</summary>
    [ResourceEntry("SubscribersActivity", Description = "phrase: Subscribers activity", LastModified = "2010/10/10", Value = "Subscribers activity")]
    public string SubscribersActivity => this[nameof (SubscribersActivity)];

    /// <summary>phrase: Detailed report</summary>
    [ResourceEntry("DetailedReport", Description = "phrase: Detailed report", LastModified = "2010/10/10", Value = "Detailed report")]
    public string DetailedReport => this[nameof (DetailedReport)];

    /// <summary>word: Delivered</summary>
    [ResourceEntry("Delivered", Description = "word: Delivered", LastModified = "2010/10/10", Value = "Delivered")]
    public string Delivered => this[nameof (Delivered)];

    /// <summary>word: Queued</summary>
    [ResourceEntry("Queued", Description = "word: Queued", LastModified = "2010/10/10", Value = "Queued")]
    public string Queued => this[nameof (Queued)];

    /// <summary>word: Bounced</summary>
    [ResourceEntry("Bounced", Description = "word: Bounced", LastModified = "2010/10/10", Value = "Bounced")]
    public string Bounced => this[nameof (Bounced)];

    /// <summary>phrase: Delivery date</summary>
    [ResourceEntry("DeliveryDate", Description = "phrase: Delivery date", LastModified = "2010/10/10", Value = "Delivery date")]
    public string DeliveryDate => this[nameof (DeliveryDate)];

    /// <summary>
    /// phrase: Currently, there are no campaigns that are ready for sending or in the processes of delivery.
    /// </summary>
    [ResourceEntry("NoCurrentCampaigns", Description = "phrase: Currently, there are no campaigns that are ready for sending or in the processes of delivery.", LastModified = "2010/10/10", Value = "Currently, there are no campaigns that are ready for sending or in the processes of delivery.")]
    public string NoCurrentCampaigns => this[nameof (NoCurrentCampaigns)];

    /// <summary>
    /// phrase: Currently, there are no campaigns that have been scheduled for delivery.
    /// </summary>
    [ResourceEntry("NoScheduledCampaigns", Description = "phrase: Currently, there are no campaigns that have been scheduled for delivery.", LastModified = "2010/10/10", Value = "Currently, there are no campaigns that have been scheduled for delivery.")]
    public string NoScheduledCampaigns => this[nameof (NoScheduledCampaigns)];

    /// <summary>phrase: Select mailing lists</summary>
    [ResourceEntry("SelectMailingLists", Description = "phrase: Select mailing lists", LastModified = "2010/10/10", Value = "Select mailing lists")]
    public string SelectMailingLists => this[nameof (SelectMailingLists)];

    /// <summary>phrase: Select a mailing list</summary>
    [ResourceEntry("SelectMailingList", Description = "phrase: Select a mailing list", LastModified = "2011/11/07", Value = "Select a mailing list")]
    public string SelectMailingList => this[nameof (SelectMailingList)];

    /// <summary>phrase: You need to select a mailing list.</summary>
    [ResourceEntry("SelectMailingListErrorMessage", Description = "phrase: Select a mailing list", LastModified = "2011/11/07", Value = "You need to select a mailing list.")]
    public string SelectMailingListErrorMessage => this[nameof (SelectMailingListErrorMessage)];

    /// <summary>word: Import</summary>
    [ResourceEntry("Import", Description = "word: Import", LastModified = "2010/10/13", Value = "Import")]
    public string Import => this[nameof (Import)];

    /// <summary>phrase: What is the format of the file?</summary>
    [ResourceEntry("SelectSourceLabel", Description = "phrase: What is the format of the file?", LastModified = "2011/10/06", Value = "What is the format of the file?")]
    public string SelectSourceLabel => this[nameof (SelectSourceLabel)];

    /// <summary>phrase: Comma separated</summary>
    [ResourceEntry("CommaSeparatedList", Description = "phrase: Comma separated", LastModified = "2011/10/06", Value = "Comma separated")]
    public string CommaSeparatedList => this[nameof (CommaSeparatedList)];

    /// <summary>phrase: Tab sepearated</summary>
    [ResourceEntry("TabSeparatedList", Description = "phrase: Tab separated", LastModified = "2011/10/06", Value = "Tab separated")]
    public string TabSeparatedList => this[nameof (TabSeparatedList)];

    /// <summary>
    /// phrase: Subscribers will be imported in the following mailing list:
    /// </summary>
    [ResourceEntry("SubscribersWillBeImportedInFollowingMailingList", Description = "phrase: Subscribers will be imported in the following mailing list:", LastModified = "2010/10/13", Value = "Subscribers will be imported in the following mailing list:")]
    public string SubscribersWillBeImportedInFollowingMailingList => this[nameof (SubscribersWillBeImportedInFollowingMailingList)];

    /// <summary>
    /// phrase: Map the source columns to the subscriber's properties
    /// </summary>
    [ResourceEntry("MappingInstruction", Description = "phrase: Map the source columns to the subscriber's properties", LastModified = "2010/10/13", Value = "Map the source columns to the subscriber's properties")]
    public string MappingInstructions => this["MappingInstruction"];

    /// <summary>phrase: First column</summary>
    [ResourceEntry("FirstColumn", Description = "phrase: First column", LastModified = "2010/10/14", Value = "First column")]
    public string FirstColumn => this[nameof (FirstColumn)];

    /// <summary>phrase: Second column</summary>
    [ResourceEntry("SecondColumn", Description = "phrase: Second column", LastModified = "2010/10/14", Value = "Second column")]
    public string SecondColumn => this[nameof (SecondColumn)];

    /// <summary>phrase: Third column</summary>
    [ResourceEntry("ThirdColumn", Description = "phrase: Third column", LastModified = "2010/10/14", Value = "Third column")]
    public string ThirdColumn => this[nameof (ThirdColumn)];

    /// <summary>
    /// phrase: Replace existing subscribers which have the same emails as imported subscribers
    /// </summary>
    [ResourceEntry("OverwriteExistingSubscribers", Description = "phrase: Replace existing subscribers which have the same emails as imported subscribers", LastModified = "2011/10/12", Value = "Replace existing subscribers which have the same emails as imported subscribers")]
    public string OverwriteExistingSubscribers => this[nameof (OverwriteExistingSubscribers)];

    /// <summary>
    /// phrase: Do not import subscribers with emails that already exist
    /// </summary>
    [ResourceEntry("DoNotOverwriteExistingSubscribers", Description = "phrase: Do not import subscribers with emails that already exist", LastModified = "2011/10/12", Value = "Do not import subscribers with emails that already exist")]
    public string DoNotOverwriteExistingSubscribers => this[nameof (DoNotOverwriteExistingSubscribers)];

    /// <summary>phrase: Select the file you want to import</summary>
    [ResourceEntry("SelectSubscribersFile", Description = "phrase: Select the file you want to import", LastModified = "2011/10/06", Value = "Select the file you want to import")]
    public string SelectSubscribersFile => this[nameof (SelectSubscribersFile)];

    /// <summary>phrase: Define the content of the message A</summary>
    [ResourceEntry("DefineTheContentOfTheMessageA", Description = "phrase: Define the content of the message A", LastModified = "2010/10/16", Value = "Define the content of the message A")]
    public string DefineTheContentOfTheMessageA => this[nameof (DefineTheContentOfTheMessageA)];

    /// <summary>phrase: Define the content of the message B</summary>
    [ResourceEntry("DefineTheContentOfTheMessageB", Description = "phrase: Define the content of the message B", LastModified = "2010/10/16", Value = "Define the content of the message B")]
    public string DefineTheContentOfTheMessageB => this["DefineTheContentOfTheMessageA"];

    /// <summary>phrase: How to split the campaign?</summary>
    [ResourceEntry("HowToSplitTheCampaign", Description = "phrase: How to split the campaign?", LastModified = "2010/10/16", Value = "How to split the campaign?")]
    public string HowToSplitTheCampaign => this[nameof (HowToSplitTheCampaign)];

    /// <summary>phrase: Determine the size of the testing sample.</summary>
    [ResourceEntry("DetermineTheSizeOfTheTestingSample", Description = "phrase: Determine the size of the testing sample.", LastModified = "2010/10/16", Value = "Determine the size of the testing sample.")]
    public string DetermineTheSizeOfTheTestingSample => this[nameof (DetermineTheSizeOfTheTestingSample)];

    /// <summary>phrase: How to pick the winning campaign?</summary>
    [ResourceEntry("HowToPickTheWinningCampaign", Description = "phrase: How to pick the winning campaign?", LastModified = "2010/10/16", Value = "How to pick the winning campaign?")]
    public string HowToPickTheWinningCampaign => this[nameof (HowToPickTheWinningCampaign)];

    /// <summary>phrase: Open rate</summary>
    [ResourceEntry("OpenRate", Description = "phrase: Open rate", LastModified = "2010/10/16", Value = "Open rate")]
    public string OpenRate => this[nameof (OpenRate)];

    /// <summary>phrase: Click rate</summary>
    [ResourceEntry("ClickRate", Description = "phrase: Click rate", LastModified = "2010/10/16", Value = "Click rate")]
    public string ClickRate => this[nameof (ClickRate)];

    /// <summary>word: Manually</summary>
    [ResourceEntry("Manually", Description = "word: Manually", LastModified = "2010/10/16", Value = "Manually")]
    public string Manually => this[nameof (Manually)];

    /// <summary>
    /// phrase: The campaign that is opened the most is the winner.
    /// </summary>
    [ResourceEntry("OpenRateDescription", Description = "phrase: The campaign that is opened the most is the winner.", LastModified = "2010/10/16", Value = "The campaign that is opened the most is the winner.")]
    public string OpenRateDescription => this[nameof (OpenRateDescription)];

    /// <summary>
    /// phrase: The campaign that receives the most clicks is the winner.
    /// </summary>
    [ResourceEntry("ClickRateDescription", Description = "phrase: The campaign that receives the most clicks is the winner.", LastModified = "2010/10/16", Value = "The campaign that receives the most clicks is the winner.")]
    public string ClickRateDescription => this[nameof (ClickRateDescription)];

    /// <summary>
    /// phrase: You choose the winner in the campaign report, then we'll send the winning campaign.
    /// </summary>
    [ResourceEntry("ManuallyDescription", Description = "phrase: You choose the winner in the campaign report, then we'll send the winning campaign.", LastModified = "2010/10/16", Value = "You choose the winner in the campaign report, then we'll send the winning campaign.")]
    public string ManuallyDescription => this[nameof (ManuallyDescription)];

    /// <summary>phrase: Determine the winner after the first:</summary>
    [ResourceEntry("DetermineTheWinnerAfterTheFirst", Description = "phrase: Determine the winner after the first:", LastModified = "2010/10/16", Value = "Determine the winner after the first:")]
    public string DetermineTheWinnerAfterTheFirst => this[nameof (DetermineTheWinnerAfterTheFirst)];

    /// <summary>word: Days</summary>
    [ResourceEntry("Days", Description = "word: Days", LastModified = "2010/10/16", Value = "days")]
    public string Days => this[nameof (Days)];

    /// <summary>word: Hours</summary>
    [ResourceEntry("Hours", Description = "word: hours", LastModified = "2010/10/16", Value = "hours")]
    public string Hours => this[nameof (Hours)];

    /// <summary>phrase: Example: Company Monthly Newsletter</summary>
    [ResourceEntry("CampaignNameDescription", Description = "phrase: Example: Company Monthly Newsletter", LastModified = "2012/05/02", Value = "Example: <em class='sfNote'>Company Monthly Newsletter</em>")]
    public string CampaignNameDescription => this[nameof (CampaignNameDescription)];

    /// <summary>
    /// phrase: Keep it relevant and non-spammy to avoid spam filters.
    /// </summary>
    [ResourceEntry("MessageSubjectDescription", Description = "phrase: Keep it relevant and non-spammy to avoid spam filters.", LastModified = "2010/10/16", Value = "Keep it relevant and non-spammy to avoid spam filters.")]
    public string MessageSubjectDescription => this[nameof (MessageSubjectDescription)];

    /// <summary>
    /// phrase: Use something they'll instantly recognize, like your company name.
    /// </summary>
    [ResourceEntry("FromNameDescription", Description = "phrase: Use something they'll instantly recognize, like your company name.", LastModified = "2010/10/16", Value = "Use something they'll instantly recognize, like your company name.")]
    public string FromNameDescription => this[nameof (FromNameDescription)];

    /// <summary>phrase: Their replies will go to this email address.</summary>
    [ResourceEntry("ReplyToEmailDescription", Description = "phrase: Their replies will go to this email address.", LastModified = "2010/10/16", Value = "Their replies will go to this email address.")]
    public string ReplyToEmailDescription => this[nameof (ReplyToEmailDescription)];

    /// <summary>
    /// phrase: Good example: \"Acme Company Email Campaigns\" - Bad example: \"Cust_11_01_2007\"
    /// </summary>
    [ResourceEntry("MailingListTitleDescription", Description = "phrase: Good example: \"Acme Company Email Campaigns\" - Bad example: \"Cust_11_01_2007\"", LastModified = "2010/10/16", Value = "Good example: \"Acme Company Email Campaigns\" <br />Bad example: \"Cust_11_01_2007\"")]
    public string MailingListTitleDescription => this[nameof (MailingListTitleDescription)];

    /// <summary>
    /// phrase: Use something recognizable (like your company name)
    /// </summary>
    [ResourceEntry("DefaultFromNameDescription", Description = "phrase: Use something recognizable (like your company name)", LastModified = "2010/10/16", Value = "Use something recognizable (like your company name)")]
    public string DefaultFromNameDescription => this[nameof (DefaultFromNameDescription)];

    /// <summary>phrase: This is the address people will reply to.</summary>
    [ResourceEntry("DefaultReplyToEmailDescription", Description = "phrase: This is the address people will reply to.", LastModified = "2010/10/16", Value = "This is the address people will reply to.")]
    public string DefaultReplyToEmailDescription => this[nameof (DefaultReplyToEmailDescription)];

    /// <summary>
    /// phrase: Keep it relevant and non-spammy. You can set different subject line when you create campaigns.
    /// </summary>
    [ResourceEntry("DefaultSubjectDescription", Description = "phrase: Keep it relevant and non-spammy. You can set different subject line when you create campaigns.", LastModified = "2010/10/16", Value = "Keep it relevant and non-spammy. You can set different subject line when you create campaigns.")]
    public string DefaultSubjectDescription => this[nameof (DefaultSubjectDescription)];

    /// <summary>
    /// phrase: Example: "You are receiving this email because you opted in at our website..." or "We send special offers to customers who opted in at..."
    /// </summary>
    [ResourceEntry("SubscriptionReminderDescription", Description = "phrase: Example: \"You are receiving this email because you opted in at our website...\" or \"We send special offers to customers who opted in at...\"", LastModified = "2010/10/16", Value = "Example: \"You are receiving this email because you opted in at our website...\" or \"We send special offers to customers who opted in at...\"")]
    public string SubscriptionReminderDescription => this[nameof (SubscriptionReminderDescription)];

    /// <summary>
    /// phrase: Which mailing list would you like to send this campaign to?
    /// </summary>
    [ResourceEntry("SelectMailingListInstruction", Description = "phrase: Which mailing list would you like to send this campaign to?", LastModified = "2010/10/16", Value = "Which mailing list would you like to send this campaign to?")]
    public string SelectMailingListInstruction => this[nameof (SelectMailingListInstruction)];

    /// <summary>Title of the subscribe form control</summary>
    [ResourceEntry("SubscribeFormTitle", Description = "Title of the subscribe form control", LastModified = "2010/10/26", Value = "Subscribe form")]
    public string SubscribeFormTitle => this[nameof (SubscribeFormTitle)];

    /// <summary>The description of the subscribe form</summary>
    [ResourceEntry("SubscribeFormDescription", Description = "The description of the subscribe form", LastModified = "2010/10/26", Value = "Form that lets users subscribe to the newsletter")]
    public string SubscribeFormDescription => this[nameof (SubscribeFormDescription)];

    /// <summary>Title of the unsubscribe form control</summary>
    [ResourceEntry("UnsubscribeFormTitle", Description = "Title of the unsubscribe form control", LastModified = "2012/05/31", Value = "Unsubscribe")]
    public string UnsubscribeFormTitle => this[nameof (UnsubscribeFormTitle)];

    /// <summary>The description of the unsubscribe form</summary>
    [ResourceEntry("UnsubscribeFormDescription", Description = "The description of the unsubscribe form", LastModified = "2019/06/03", Value = "Form that lets users to unsubscribe from the newsletter")]
    public string UnsubscribeFormDescription => this[nameof (UnsubscribeFormDescription)];

    /// <summary>phrase: Email Address</summary>
    [ResourceEntry("EmailAddress", Description = "phrase: Email Address", LastModified = "2010/10/27", Value = "Email")]
    public string EmailAddress => this[nameof (EmailAddress)];

    /// <summary>phrase: Subscribe to mailing list</summary>
    [ResourceEntry("SubscribeToMailingList", Description = "phrase: Subscribe to mailing list", LastModified = "2010/10/27", Value = "Subscribe to mailing list")]
    public string SubscribeToMailingList => this[nameof (SubscribeToMailingList)];

    /// <summary>phrase: Required fields are bold</summary>
    [ResourceEntry("RequiredFieldsAreBold", Description = "phrase: Required fields are bold.", LastModified = "2010/10/27", Value = "Required fields are bold")]
    public string RequiredFieldsAreBold => this[nameof (RequiredFieldsAreBold)];

    /// <summary>phrase: Click edit and select the mailing list</summary>
    [ResourceEntry("ClickEditAndSelectMailingList", Description = "phrase: Click Edit and select the mailing list", LastModified = "2010/10/27", Value = "Click edit and select the mailing list")]
    public string ClickEditAndSelectMailingList => this[nameof (ClickEditAndSelectMailingList)];

    /// <summary>
    /// phrase: You have successfully subscribed to this mailing list. Thank you.
    /// </summary>
    [ResourceEntry("SuccessfulSubscription", Description = "phrase: You have successfully subscribed to this mailing list. Thank you.", LastModified = "2010/10/28", Value = "You have successfully subscribed to this mailing list. Thank you.")]
    public string SuccessfulSubscription => this[nameof (SuccessfulSubscription)];

    /// <summary>
    /// phrase: A subscriber with given the email is already subscribed to this mailing list.
    /// </summary>
    [ResourceEntry("EmailExistsInTheMailingList", Description = "phrase: A subscriber with given email is already subscribed to this mailing list.", LastModified = "2010/10/28", Value = "A subscriber with the given email is already subscribed to this mailing list.")]
    public string EmailExistsInTheMailingList => this[nameof (EmailExistsInTheMailingList)];

    /// <summary>word: Unsubscribe</summary>
    [ResourceEntry("Unsubscribe", Description = "word: Unsubscribe", LastModified = "2010/10/28", Value = "Unsubscribe")]
    public string Unsubscribe => this[nameof (Unsubscribe)];

    /// <summary>Title of the newsletter controls toolbox.</summary>
    [ResourceEntry("NewslettersControlsTitle", Description = "Title of the newsletter controls toolbox", LastModified = "2010/12/13", Value = "Newsletter controls")]
    public string NewslettersControlsTitle => this[nameof (NewslettersControlsTitle)];

    /// <summary>The description of the newsletters control toolbox.</summary>
    [ResourceEntry("NewslettersControlsDescription", Description = "The description of the newsletters control toolbox", LastModified = "2010/12/13", Value = "The toolbox for the newsletter campaign design")]
    public string NewslettersControlsDescription => this[nameof (NewslettersControlsDescription)];

    /// <summary>phrase: Use SMTP authentication</summary>
    [ResourceEntry("UseSmtpAuthentication", Description = "phrase: Use SMTP authentication.", LastModified = "2010/12/14", Value = "Use SMTP authentication")]
    public string UseSmtpAuthentication => this[nameof (UseSmtpAuthentication)];

    /// <summary>
    /// Message shown to user when no mailing lists have been yet created.
    /// </summary>
    [ResourceEntry("NoMailingListsCreatedYet", Description = "Message shown to user when no mailing lists have been yet created.", LastModified = "2010/12/15", Value = "No mailing lists have been created yet.")]
    public string NoMailingListsCreatedYet => this[nameof (NoMailingListsCreatedYet)];

    /// <summary>
    /// Message shown to user when no subsribers have been yet created.
    /// </summary>
    [ResourceEntry("NoSubscribersCreatedYet", Description = "Message shown to user when no subscribers have been yet created.", LastModified = "2010/12/15", Value = "No subscribers have been created yet.")]
    public string NoSubscribersCreatedYet => this[nameof (NoSubscribersCreatedYet)];

    /// <summary>
    /// Message shown to user when no campaigns have been yet created.
    /// </summary>
    [ResourceEntry("NoCampaignsCreatedYet", Description = "Messages shown to user when no campaigns have been yet created.", LastModified = "2010/12/15", Value = "No campaigns have been created yet.")]
    public string NoCampaignsCreatedYet => this[nameof (NoCampaignsCreatedYet)];

    /// <summary>Title of the form for editing subscriber information.</summary>
    [ResourceEntry("EditASubscriber", Description = "Title of the form for editing subscriber information", LastModified = "2011/10/03", Value = "Edit a subscriber")]
    public string EditASubscriber => this[nameof (EditASubscriber)];

    /// <summary>Validation message for the missing email address.</summary>
    [ResourceEntry("EmailIsRequired", Description = "Validation message for the missing email address.", LastModified = "2010/12/15", Value = "Email address is required")]
    public string EmailIsRequired => this[nameof (EmailIsRequired)];

    /// <summary>
    /// The title of the mailing list selector in the subscribe form designer.
    /// </summary>
    [ResourceEntry("SubscribeDesignerSelectMailingListTitle", Description = "The title of the mailing list selector in the subscribe form designer.", LastModified = "2010/12/15", Value = "Let users subscribe to:")]
    public string SubscribeDesignerSelectMailingListTitle => this[nameof (SubscribeDesignerSelectMailingListTitle)];

    /// <summary>
    /// The description of the mailing list selector in the subscribe form designer.
    /// </summary>
    [ResourceEntry("SubscribeDesignerSelectMailingListDescription", Description = "The description of the mailing list selector in the subscribe form designer.", LastModified = "2010/12/15", Value = "Here are listed all the mailing lists defined in the Newsletters module.")]
    public string SubscribeDesignerSelectMailingListDescription => this[nameof (SubscribeDesignerSelectMailingListDescription)];

    /// <summary>
    /// The title of the mailing list selector in the unsubscribe form designer.
    /// </summary>
    [ResourceEntry("UnsubscribeDesignerSelectMailingListTitle", Description = "The title of the mailing list selector in the unsubscribe form designer.", LastModified = "2010/12/22", Value = "Let users unsubscribe from:")]
    public string UnsubscribeDesignerSelectMailingListTitle => this[nameof (UnsubscribeDesignerSelectMailingListTitle)];

    /// <summary>
    /// The description of the mailing list selector in the unsubscribe form designer.
    /// </summary>
    [ResourceEntry("UnsubscribeDesignerSelectMailingListDescription", Description = "The description of the mailing list selector in the unsubscribe form designer.", LastModified = "2010/12/22", Value = "Here are listed all the mailing lists defined in the Newsletters module.")]
    public string UnsubscribeDesignerSelectMailingListDescription => this[nameof (UnsubscribeDesignerSelectMailingListDescription)];

    /// <summary>Example of the title for the subscribe form widget.</summary>
    [ResourceEntry("SubscribeFormWidgetTitleExample", Description = "Example of the title for the subscribe form widget.", LastModified = "2010/12/15", Value = "e.g. Want to know more about our latest products?")]
    public string SubscribeFormWidgetTitleExample => this[nameof (SubscribeFormWidgetTitleExample)];

    /// <summary>
    /// Example of the description for the subscribe form widget.
    /// </summary>
    [ResourceEntry("SubscribeFormWidgetDescriptionExample", Description = "Example of the description for the subscribe form widget.", LastModified = "2010/12/15", Value = "e.g. Keep up with the latest products, upgrades and financing options...")]
    public string SubscribeFormWidgetDescriptionExample => this[nameof (SubscribeFormWidgetDescriptionExample)];

    /// <summary>Example of the title for the unsubscribe form widget.</summary>
    [ResourceEntry("UnsubscribeFormWidgetTitleExample", Description = "Example of the title for the unsubscribe form widget.", LastModified = "2010/12/22", Value = "e.g. Had enough of us?")]
    public string UnsubscribeFormWidgetTitleExample => this[nameof (UnsubscribeFormWidgetTitleExample)];

    /// <summary>
    /// Example of the description for the unsubscribe form widget.
    /// </summary>
    [ResourceEntry("UnsubscribeFormWidgetDescriptionExample", Description = "Example of the description for the unsubscribe form widget.", LastModified = "2010/12/22", Value = "e.g. If you don't want to receive emails for this mailing list anymore, supply your email and will send you a link to unsubscribe.")]
    public string UnsubscribeFormWidgetDescriptionExample => this[nameof (UnsubscribeFormWidgetDescriptionExample)];

    /// <summary>
    /// Title of the message templates functionality in the newsletters module.
    /// </summary>
    [ResourceEntry("Templates", Description = "Title of the message templates functionality in newsletters module.", LastModified = "2010/12/17", Value = "Message templates")]
    public string Templates => this[nameof (Templates)];

    /// <summary>phrase: Create a message template</summary>
    [ResourceEntry("CreateATemplate", Description = "phrase: Create a message template", LastModified = "2011/09/29", Value = "Create a message template")]
    public string CreateATemplate => this[nameof (CreateATemplate)];

    /// <summary>phrase: Template with widgets</summary>
    [ResourceEntry("StandardTemplate", Description = "phrase: Template with widgets", LastModified = "2011/09/29", Value = "Template with widgets")]
    public string StandardTemplate => this[nameof (StandardTemplate)];

    /// <summary>phrase: Rich text (HTML)</summary>
    [ResourceEntry("HtmlTemplate", Description = "phrase: Rich text (HTML)", LastModified = "2011/09/29", Value = "Rich text (HTML)")]
    public string HtmlTemplate => this[nameof (HtmlTemplate)];

    /// <summary>phrase: Plain text</summary>
    [ResourceEntry("PlainTextTemplate", Description = "phrase: Plain text", LastModified = "2011/09/29", Value = "Plain text")]
    public string PlainTextTemplate => this[nameof (PlainTextTemplate)];

    /// <summary>phrase: Template name</summary>
    [ResourceEntry("TemplateName", Description = "phrase: Template name", LastModified = "2010/12/17", Value = "Template name")]
    public string TemplateName => this[nameof (TemplateName)];

    /// <summary>phrase: Create this template</summary>
    [ResourceEntry("CreateThisTemplate", Description = "phrase: Create this template", LastModified = "2010/12/17", Value = "Create this template")]
    public string CreateThisTemplate => this[nameof (CreateThisTemplate)];

    /// <summary>phrase: Create a new standard template</summary>
    [ResourceEntry("CreateNewStandardTemplate", Description = "phrase: Create a new standard template", LastModified = "2010/12/17", Value = "Create a new standard template")]
    public string CreateNewStandardTemplate => this[nameof (CreateNewStandardTemplate)];

    /// <summary>phrase: Create new html template</summary>
    [ResourceEntry("CreateNewHtmlTemplate", Description = "phrase: Create new html template", LastModified = "2010/12/17", Value = "Create new html template")]
    public string CreateNewHtmlTemplate => this[nameof (CreateNewHtmlTemplate)];

    /// <summary>phrase: Create new plain text template</summary>
    [ResourceEntry("CreateNewPlainTextTemplate", Description = "phrase: Create new plain text template", LastModified = "2010/12/17", Value = "Create new plain text template")]
    public string CreateNewPlainTextTemplate => this[nameof (CreateNewPlainTextTemplate)];

    /// <summary>phrase: Back to Message templates</summary>
    [ResourceEntry("BackToMessageTemplates", Description = "phrase: Back to Message templates", LastModified = "2011/10/04", Value = "Back to Message templates")]
    public string BackToMessageTemplates => this[nameof (BackToMessageTemplates)];

    /// <summary>
    /// Message shown to user if no templates have been created yet.
    /// </summary>
    [ResourceEntry("NoTemplatesCreatedYet", Description = "Message shown to user if no templates have been created yet.", LastModified = "2010/12/18", Value = "No templates have been created yet.")]
    public string NoTemplatesCreatedYet => this[nameof (NoTemplatesCreatedYet)];

    /// <summary>Confirmation message for deleting templates</summary>
    [ResourceEntry("AreYouSureDeleteTemplate", Description = "Confirmation message for deleting templates", LastModified = "2010/12/18", Value = "Are you sure you want to delete this template(s)?")]
    public string AreYouSureDeleteTemplate => this[nameof (AreYouSureDeleteTemplate)];

    /// <summary>phrase: Create a message template</summary>
    [ResourceEntry("CreateAMessageTemplate", Description = "phrase: Create a message template", LastModified = "2010/12/18", Value = "Create a message template")]
    public string CreateAMessageTemplate => this[nameof (CreateAMessageTemplate)];

    /// <summary>phrase: Send a confirmation email to new subscribers</summary>
    [ResourceEntry("SendAConfirmationEmailToNewSubscribers", Description = "phrase: Send a confirmation email to new subscribers?", LastModified = "2010/12/18", Value = "Send a confirmation email to new subscribers?")]
    public string SendAConfirmationEmailToNewSubscribers => this[nameof (SendAConfirmationEmailToNewSubscribers)];

    /// <summary>word: Opened</summary>
    [ResourceEntry("Opened", Description = "word: Opened", LastModified = "2010/12/20", Value = "Opened")]
    public string Opened => this[nameof (Opened)];

    /// <summary>word: Unopened</summary>
    [ResourceEntry("Unopened", Description = "word: Unopened", LastModified = "2010/12/20", Value = "Unopened")]
    public string Unopened => this[nameof (Unopened)];

    /// <summary>phrase: Total recipients</summary>
    [ResourceEntry("TotalRecipients", Description = "phrase: Total recipients", LastModified = "2010/12/20", Value = "Total recipients")]
    public string TotalRecipients => this[nameof (TotalRecipients)];

    /// <summary>phrase: Successful deliveries</summary>
    [ResourceEntry("SuccessfulDeliveries", Description = "phrase: Successful deliveries", LastModified = "2010/12/20", Value = "Successful deliveries")]
    public string SuccessfulDeliveries => this[nameof (SuccessfulDeliveries)];

    /// <summary>phrase: Times forwarded</summary>
    [ResourceEntry("TimesForwarded", Description = "phrase: Times forwarded", LastModified = "2010/12/20", Value = "Times forwarded")]
    public string TimesForwarded => this[nameof (TimesForwarded)];

    /// <summary>phrase: Forwarded opens</summary>
    [ResourceEntry("ForwardedOpens", Description = "phrase: Forwarded opens", LastModified = "2010/12/20", Value = "Forwarded opens")]
    public string ForwardedOpens => this[nameof (ForwardedOpens)];

    /// <summary>phrase: Recipients who opened</summary>
    [ResourceEntry("RecipientsWhoOpened", Description = "phrase: Recipients who opened", LastModified = "2010/12/20", Value = "Recipients who opened")]
    public string RecipientsWhoOpened => this[nameof (RecipientsWhoOpened)];

    /// <summary>phrase: Total times opened</summary>
    [ResourceEntry("TotalTimesOpened", Description = "phrase: Total times opened", LastModified = "2010/12/20", Value = "Total times opened")]
    public string TotalTimesOpened => this[nameof (TotalTimesOpened)];

    /// <summary>phrase: Last open date</summary>
    [ResourceEntry("LastOpenDate", Description = "phrase: Last open date", LastModified = "2010/12/20", Value = "Last open date")]
    public string LastOpenDate => this[nameof (LastOpenDate)];

    /// <summary>phrase: Recipients who clicked</summary>
    [ResourceEntry("RecipientsWhoClicked", Description = "phrase: Recipients who clicked", LastModified = "2010/12/20", Value = "Recipients who clicked")]
    public string RecipientsWhoClicked => this[nameof (RecipientsWhoClicked)];

    /// <summary>phrase: Total clicks</summary>
    [ResourceEntry("TotalClicks", Description = "phrase: Total clicks", LastModified = "2010/12/20", Value = "Total clicks")]
    public string TotalClicks => this[nameof (TotalClicks)];

    /// <summary>phrase: Last click</summary>
    [ResourceEntry("LastClick", Description = "phrase: Last click", LastModified = "2010/12/20", Value = "Last click")]
    public string LastClick => this[nameof (LastClick)];

    /// <summary>phrase: Total unsubscriptions</summary>
    [ResourceEntry("TotalUnsubscriptions", Description = "phrase: Total unsubscriptions", LastModified = "2010/12/20", Value = "Total unsubscriptions")]
    public string TotalUnsubscriptions => this[nameof (TotalUnsubscriptions)];

    /// <summary>phrase: Edit campaign</summary>
    [ResourceEntry("EditCampaign", Description = "phrase: Edit campaign", LastModified = "2010/12/20", Value = "Edit campaign")]
    public string EditCampaign => this[nameof (EditCampaign)];

    /// <summary>phrase: Subscriber successfully created</summary>
    [ResourceEntry("NewSubscriberCreatedSuccessfully", Description = "phrase: Subscriber successfully created", LastModified = "2011/11/02", Value = "Subscriber successfully created")]
    public string NewSubscriberCreatedSuccessfully => this[nameof (NewSubscriberCreatedSuccessfully)];

    /// <summary>
    /// phrase: Subscriber information has been successfully saved
    /// </summary>
    [ResourceEntry("SubscriberInformationSavedSuccessfully", Description = "phrase: Subscriber information has been successfully saved", LastModified = "2010/12/21", Value = "Subscriber information has been successfully saved")]
    public string SubscriberInformationSavedSuccessfully => this[nameof (SubscriberInformationSavedSuccessfully)];

    /// <summary>phrase: Mailing list successfully created</summary>
    [ResourceEntry("NewMailingListSuccessfullyCreated", Description = "phrase: Mailing list successfully created", LastModified = "2011/11/02", Value = "Mailing list successfully created")]
    public string NewMailingListSuccessfullyCreated => this[nameof (NewMailingListSuccessfullyCreated)];

    /// <summary>
    /// phrase: Mailing list information has been successfully saved.
    /// </summary>
    [ResourceEntry("MailingListInformationSuccessfullySaved", Description = "phrase: Mailing list information has been successfully saved.", LastModified = "2010/12/21", Value = "Mailing list information has been successfully saved.")]
    public string MailingListInformationSuccessfullySaved => this[nameof (MailingListInformationSuccessfullySaved)];

    /// <summary>phrase: Edit mailing list</summary>
    [ResourceEntry("EditMailingList", Description = "phrase: Edit mailing list", LastModified = "2010/12/21", Value = "Edit mailing list")]
    public string EditMailingList => this[nameof (EditMailingList)];

    /// <summary>phrase: Type</summary>
    [ResourceEntry("TemplateType", Description = "phrase: Type", LastModified = "2011/09/29", Value = "Type")]
    public string TemplateType => this[nameof (TemplateType)];

    /// <summary>phrase: New template has been successfully created</summary>
    [ResourceEntry("NewTemplateCreatedSuccessfully", Description = "phrase: New template has been successfully created.", LastModified = "2011/03/11", Value = "New template has been successfully created.")]
    public string NewTemplateCreatedSuccessfully => this[nameof (NewTemplateCreatedSuccessfully)];

    /// <summary>phrase: Template has been successfully saved.</summary>
    [ResourceEntry("TemplateSavedSuccessfully", Description = "phrase: Template has been successfully saved.", LastModified = "2010/12/22", Value = "Template has been successfully saved.")]
    public string TemplateSavedSuccessfully => this[nameof (TemplateSavedSuccessfully)];

    /// <summary>User friendly name of the html campaign type.</summary>
    [ResourceEntry("RichTextHtml", Description = "User friendly name of the html campaign type.", LastModified = "2010/12/22", Value = "Rich text (HTML)")]
    public string RichTextHtml => this[nameof (RichTextHtml)];

    /// <summary>Description of the html campaign type.</summary>
    [ResourceEntry("RichTextHtmlDescription", Description = "Description of the html campaign type", LastModified = "2010/12/22", Value = "Text with rich formatting (bold, italic, bullets, etc.). Images can be inserted, too. Some email clients may not interpret it correctly.")]
    public string RichTextHtmlDescription => this[nameof (RichTextHtmlDescription)];

    /// <summary>User friendly name of the plain text campaign.</summary>
    [ResourceEntry("PlainText", Description = "User friendly name of the plain text campaign.", LastModified = "2010/12/22", Value = "Plain text")]
    public string PlainText => this[nameof (PlainText)];

    /// <summary>Description of the plain text campaign type.</summary>
    [ResourceEntry("PlainTextDescription", Description = "Description of the plain text campaign type.", LastModified = "2010/12/22", Value = "Text with no options to be formatted. Accessible with all email clients.")]
    public string PlainTextDescription => this[nameof (PlainTextDescription)];

    /// <summary>User friendly name of the standard campaign.</summary>
    [ResourceEntry("LikeAWebPage", Description = "User friendly name of the standard campaign.", LastModified = "2010/12/22", Value = "Like a web page")]
    public string LikeAWebPage => this[nameof (LikeAWebPage)];

    /// <summary>Description of the standard campaign type.</summary>
    [ResourceEntry("LikeAWebPageDescription", Description = "Description of the standard campaign type.", LastModified = "2010/12/22", Value = "For multi column layouts and rich design. The content is added by widgets. Some email clients may not interpret it correctly.")]
    public string LikeAWebPageDescription => this[nameof (LikeAWebPageDescription)];

    /// <summary>phrase: Choose the type of campaign.</summary>
    [ResourceEntry("ChooseTheTypeOfCampaign", Description = "phrase: Choose the type of campaign.", LastModified = "2010/12/22", Value = "Choose the type of campaign")]
    public string ChooseTheTypeOfCampaign => this[nameof (ChooseTheTypeOfCampaign)];

    /// <summary>phrase: Start from scratch</summary>
    [ResourceEntry("StartFromScratch", Description = "phrase: Start from scratch", LastModified = "2010/12/22", Value = "Start from scratch")]
    public string StartFromScratch => this[nameof (StartFromScratch)];

    /// <summary>phrase: Use already created page</summary>
    [ResourceEntry("UseAlreadyCreatedPage", Description = "phrase: Use already created page", LastModified = "2010/12/22", Value = "Use already created page")]
    public string UseAlreadyCreatedPage => this[nameof (UseAlreadyCreatedPage)];

    /// <summary>word: Overview</summary>
    [ResourceEntry("Overview", Description = "word: Overview", LastModified = "2010/12/25", Value = "Overview")]
    public string Overview => this[nameof (Overview)];

    /// <summary>phrase: Campaign has been sent successfully.</summary>
    [ResourceEntry("CampaignSentSuccessfully", Description = "phrase: Campaign has been sent successfully.", LastModified = "2010/12/26", Value = "Campaign has been sent successfully.")]
    public string CampaignSentSuccessfully => this[nameof (CampaignSentSuccessfully)];

    /// <summary>word: Done</summary>
    [ResourceEntry("Done", Description = "word: Done", LastModified = "2010/12/27", Value = "Done")]
    public string Done => this[nameof (Done)];

    /// <summary>
    /// phrase: The campaign's message is composed like a web page
    /// </summary>
    [ResourceEntry("CampaignMessageComposedAsAWebPage", Description = "phrase: The campaign's message is composed like a web page", LastModified = "2010/12/27", Value = "The campaign's message is composed like a web page")]
    public string CampaignMessageComposedAsAWebPage => this[nameof (CampaignMessageComposedAsAWebPage)];

    /// <summary>phrase: Edit campaign message</summary>
    [ResourceEntry("EditCampaignMessage", Description = "phrase: Edit campaign message", LastModified = "2010/12/27", Value = "Edit campaign message")]
    public string EditCampaignMessage => this[nameof (EditCampaignMessage)];

    /// <summary>phrase: Edit a message template</summary>
    [ResourceEntry("EditTemplate", Description = "phrase: Edit a message template", LastModified = "2011/09/29", Value = "Edit a message template")]
    public string EditTemplate => this[nameof (EditTemplate)];

    /// <summary>
    /// phrase: What type of template would you like to create?
    /// </summary>
    [ResourceEntry("SelectTemplateType", Description = "phrase: What type of template would you like to create?", LastModified = "2011/09/29", Value = "What type of template would you like to create?")]
    public string SelectTemplateType => this[nameof (SelectTemplateType)];

    /// <summary>phrase: Discard this campaign</summary>
    [ResourceEntry("DiscardCampaign", Description = "phrase: Discard this campaign", LastModified = "2011/04/11", Value = "Discard this campaign")]
    public string DiscardCampaign => this[nameof (DiscardCampaign)];

    /// <summary>phrase: Campaign message</summary>
    [ResourceEntry("CampaignMessage", Description = "phrase: Campaign message", LastModified = "2011/01/02", Value = "Campaign message")]
    public string CampaignMessage => this[nameof (CampaignMessage)];

    /// <summary>phrase: New campaign has been successfully created.</summary>
    [ResourceEntry("NewCampaignSuccessfullyCreated", Description = "phrase: New campaign has been successfully created.", LastModified = "2011/01/05", Value = "New campaign has been successfully created.")]
    public string NewCampaignSuccessfullyCreated => this[nameof (NewCampaignSuccessfullyCreated)];

    /// <summary>phrase: Campaign has been successfully saved.</summary>
    [ResourceEntry("CampaignSuccessfullySaved", Description = "phrase: Campaign has been successfully saved.", LastModified = "2011/01/05", Value = "Campaign has been successfully saved.")]
    public string CampaignSuccessfullySaved => this[nameof (CampaignSuccessfullySaved)];

    /// <summary>phrase: -- select a merge tag --</summary>
    [ResourceEntry("SelectAMergeTag", Description = "phrase: -- select a merge tag --", LastModified = "2011/01/06", Value = "-- select a merge tag --")]
    public string SelectAMergeTag => this[nameof (SelectAMergeTag)];

    /// <summary>word: Insert</summary>
    [ResourceEntry("Insert", Description = "word: Insert", LastModified = "2011/01/06", Value = "Insert")]
    public string Insert => this[nameof (Insert)];

    /// <summary>
    /// phrase: Please select the merge tag you'd like to insert.
    /// </summary>
    [ResourceEntry("MustChooseMergeTagBeforeInserting", Description = "phrase: Please select the merge tag you'd like to insert.", LastModified = "2011/01/06", Value = "Please select the merge tag you'd like to insert.")]
    public string MustChooseMergeTagBeforeInserting => this[nameof (MustChooseMergeTagBeforeInserting)];

    /// <summary>phrase: Subscribe form</summary>
    [ResourceEntry("SubscriberDesignerSelectMailingListTitle", Description = "phrase: Subscribe form", LastModified = "2011/01/06", Value = "Subscribe form")]
    public string SubscriberDesignerSelectMailingListTitle => this[nameof (SubscriberDesignerSelectMailingListTitle)];

    /// <summary>
    /// phrase: I'd like to welcome new subscribers with an email message
    /// </summary>
    [ResourceEntry("SendWelcomeEmail", Description = "phrase: I'd like to welcome new subscribers with an email message", LastModified = "2011/01/06", Value = "I'd like to welcome new subscribers with an email message")]
    public string SendWelcomeEmail => this[nameof (SendWelcomeEmail)];

    /// <summary>
    /// phrase: e.g. Thank you for subscribing to product Z newsletter
    /// </summary>
    [ResourceEntry("WelcomeEmailSubjectDescription", Description = "phrase: e.g. Thank you for subscribing to product Z newsletter", LastModified = "2011/01/06", Value = "e.g. Thank you for subscribing to product Z newsletter")]
    public string WelcomEmailSubjectDescription => this["WelcomeEmailSubjectDescription"];

    /// <summary>
    /// phrase: I'd like to follow up with people that unsubscribe
    /// </summary>
    [ResourceEntry("SendGoodByeEmail", Description = "phrase: I'd like to follow up with people that unsubscribe", LastModified = "2011/01/06", Value = "I'd like to follow up with people that unsubscribe")]
    public string SendGoodByeEmail => this[nameof (SendGoodByeEmail)];

    /// <summary>
    /// phrase: e.g. We are sorry to see you go, hope to see you back soon
    /// </summary>
    [ResourceEntry("GoodByeSubjectDescription", Description = "phrase: e.g. We are sorry to see you go, hope to see you back soon", LastModified = "2011/01/06", Value = "e.g. We are sorry to see you go, hope to see you back soon")]
    public string GoodByeSubjectDescription => this[nameof (GoodByeSubjectDescription)];

    /// <summary>phrase: e.g. no-reply@our-company.com</summary>
    [ResourceEntry("WelcomeGoodbyeEmailAddressDescription", Description = "phrase: e.g. no-reply@our-company.com", LastModified = "2011/01/06", Value = "e.g. no-reply@our-company.com")]
    public string WelcomeGoodbyeEmailAddressDescription => this[nameof (WelcomeGoodbyeEmailAddressDescription)];

    /// <summary>word: Subject</summary>
    [ResourceEntry("Subject", Description = "word: Subject", LastModified = "2011/01/06", Value = "Subject")]
    public string Subject => this[nameof (Subject)];

    /// <summary>phrase: Choose template</summary>
    [ResourceEntry("ChooseTemplate", Description = "phrase: Choose template", LastModified = "2011/01/06", Value = "Choose template")]
    public string ChooseTemplate => this[nameof (ChooseTemplate)];

    /// <summary>The title of the newsletters module</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the newsletters module.", LastModified = "2011/01/06", Value = "Email Campaigns")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>phrase: Test your campaign before sending it</summary>
    [ResourceEntry("SendTestCampaignTitle", Description = "phrase: Test your campaign before sending it.", LastModified = "2011/01/09", Value = "Test your campaign before sending it.")]
    public string SendTestCampaignTitle => this[nameof (SendTestCampaignTitle)];

    /// <summary>phrase: Send test</summary>
    [ResourceEntry("SendTestIssueTitle", Description = "phrase: Send test", LastModified = "2012/04/27", Value = "Send test")]
    public string SendTestIssueTitle => this[nameof (SendTestIssueTitle)];

    /// <summary>phrase: Email addresses</summary>
    [ResourceEntry("EnterIssueTestEmailAddresses", Description = "phrase: Enter email address (separate with comma if you want to send it to more than one)", LastModified = "2012/04/27", Value = "<div>Test your issue before sending it to the users</div><label class=\"sfMTop15 sfTxtLbl\">Enter email address</label><p class='sfExample'>Separate with comma if you want to send it to more than one email address</p>")]
    public string EnterIssueTestEmailAddresses => this[nameof (EnterIssueTestEmailAddresses)];

    /// <summary>
    /// phrase: Test your issue before sending it to the users
    /// </summary>
    [ResourceEntry("TestIssueBeforeSending", Description = "phrase: Test your issue before sending it to the users", LastModified = "2010/04/27", Value = "Test your issue before sending it to the users")]
    public string TestIssueBeforeSending => this[nameof (TestIssueBeforeSending)];

    /// <summary>phrase: Send test</summary>
    [ResourceEntry("SendTestButton", Description = "phrase: Send test", LastModified = "2012/04/27", Value = "Send test")]
    public string SendTestButton => this[nameof (SendTestButton)];

    /// <summary>phrase: Schedule issue delivery</summary>
    [ResourceEntry("ScheduleIssueDelivery", Description = "phrase: Schedule issue delivery", LastModified = "2012/04/27", Value = "Schedule issue delivery")]
    public string ScheduleIssueDelivery => this[nameof (ScheduleIssueDelivery)];

    /// <summary>phrase: Deliver this issue on</summary>
    [ResourceEntry("DeliverIssueOn", Description = "phrase: Deliver this issue on", LastModified = "2012/04/27", Value = "Deliver this issue on")]
    public string DeliverIssueOn => this[nameof (DeliverIssueOn)];

    /// <summary>
    /// phrase: Select exact date and time when an issue of this campaign to be delivered to subscribers
    /// </summary>
    [ResourceEntry("DeliverIssueOnDescription", Description = "phrase: Select exact date and time when an issue of this campaign to be delivered to subscribers", LastModified = "2012/07/06", Value = "Select exact date and time when an issue of this campaign to be delivered to subscribers")]
    public string DeliverIssueOnDescription => this[nameof (DeliverIssueOnDescription)];

    /// <summary>phrase: Schedule this issue</summary>
    [ResourceEntry("ScheduleIssue", Description = "phrase: Schedule this issue", LastModified = "2012/04/27", Value = "Schedule this issue")]
    public string ScheduleIssue => this[nameof (ScheduleIssue)];

    /// <summary>phrase: This issue will be sent to {0} subsribers</summary>
    [ResourceEntry("SendIssuePromptText", Description = "phrase: This issue will be sent to {0} subsribers", LastModified = "2012/05/04", Value = "This issue will be sent to {0} subsribers")]
    public string SendIssuePromptText => this[nameof (SendIssuePromptText)];

    /// <summary>phrase: Send issue for [campaign name]</summary>
    [ResourceEntry("SendIssueFor", Description = "Send issue for", LastModified = "2012/05/03", Value = "Send issue for")]
    public string SendIssueFor => this[nameof (SendIssueFor)];

    /// <summary>phrase: Test message has been sent successfully.</summary>
    [ResourceEntry("TestMessageSentSuccessfully", Description = "phrase: Test message has been sent successfully.", LastModified = "2011/01/09", Value = "Test message has been sent successfully.")]
    public string TestMessageSentSuccessfully => this[nameof (TestMessageSentSuccessfully)];

    /// <summary>phrase: The title of the campaign state</summary>
    [ResourceEntry("CampaignState", Description = "The title of the campaign state", LastModified = "2011/01/11", Value = "State")]
    public string CampaignState => this[nameof (CampaignState)];

    /// <summary>
    /// Status of the campaign when it is saved, but not ready for sending.
    /// </summary>
    [ResourceEntry("CampaignStateDraft", Description = "Status of the campaign when it is saved, but not ready for sending", LastModified = "2011/01/11", Value = "Draft")]
    public string CampaignStateDraft => this[nameof (CampaignStateDraft)];

    /// <summary>Status of the campaign when it is ready to be sent.</summary>
    [ResourceEntry("CampaignStatePendingSending", Description = "Status of the campaign when it is ready to be sent", LastModified = "2011/01/11", Value = "Ready for sending")]
    public string CampaignStatePendingSending => this[nameof (CampaignStatePendingSending)];

    /// <summary>
    /// Status of the campaign when it is scheduled to be sent.
    /// </summary>
    [ResourceEntry("CampaignStateScheduled", Description = "Status of the campaign when it is scheduled to be sent.", LastModified = "2011/01/11", Value = "Scheduled for sending")]
    public string CampaignStateScheduled => this[nameof (CampaignStateScheduled)];

    /// <summary>Status of the campaign when it is being sent.</summary>
    [ResourceEntry("CampaignStateSending", Description = "Status of the campaign when it is being sent.", LastModified = "2011/01/11", Value = "Sending...")]
    public string CampaignStateSending => this[nameof (CampaignStateSending)];

    /// <summary>
    /// Status of the campaign when the sending has been completed.
    /// </summary>
    [ResourceEntry("CampaignStateCompleted", Description = "Status of the campaign when the sending has been completed.", LastModified = "2011/01/11", Value = "Sent")]
    public string CampaignStateCompleted => this[nameof (CampaignStateCompleted)];

    /// <summary>
    /// phrase: Subscribers have been successfully imported to the mailing list.
    /// </summary>
    [ResourceEntry("ImportedSubscribersSuccessfully", Description = "phrase: Subscribers have been successfully imported to the mailing list.", LastModified = "2011/01/11", Value = "Subscribers have been successfully imported to the mailing list.")]
    public string ImportedSubscribersSuccessfully => this[nameof (ImportedSubscribersSuccessfully)];

    /// <summary>phrase: There is no file selected for import</summary>
    [ResourceEntry("NoFileSelectedForImport", Description = "phrase: There is no file selected for import", LastModified = "2011/11/03", Value = "There is no file selected for import")]
    public string NoFileSelectedForImport => this[nameof (NoFileSelectedForImport)];

    /// <summary>
    /// phrase: The selected file cannot be imported. Check whether it is in the correct format
    /// </summary>
    [ResourceEntry("FileCannotBeImported", Description = "phrase:The selected file cannot be imported. Check whether it is in the correct format", LastModified = "2011/11/03", Value = "The selected file cannot be imported. Check whether it is in the correct format")]
    public string FileCannotBeImported => this[nameof (FileCannotBeImported)];

    /// <summary>
    /// phrase: No subscribers have been imported to the mailing list.
    /// </summary>
    [ResourceEntry("NoSubscribersImported", Description = "phrase: No subscribers have been imported to the mailing list", LastModified = "2011/10/17", Value = "No subscribers have been imported to the mailing list")]
    public string NoSubscribersImported => this[nameof (NoSubscribersImported)];

    /// <summary>phrase: POP3</summary>
    [ResourceEntry("Pop3", Description = "phrase: POP3", LastModified = "2019/01/21", Value = "POP3")]
    public string Pop3 => this[nameof (Pop3)];

    /// <summary>
    /// phrase: To track bounced emails, you need to setup a POP3 server
    /// </summary>
    [ResourceEntry("Pop3SettingsDescription", Description = "phrase: To track bounced emails, you need to setup a POP3 server", LastModified = "2019/01/21", Value = "To track bounced emails, you need to setup a POP3 server")]
    public string Pop3SettingsDescription => this[nameof (Pop3SettingsDescription)];

    /// <summary>phrase: POP3 Server</summary>
    [ResourceEntry("Pop3Server", Description = "phrase: POP3 Server", LastModified = "2011/03/05", Value = "POP3 Server")]
    public string Pop3Server => this[nameof (Pop3Server)];

    /// <summary>phrase: POP3 Username</summary>
    [ResourceEntry("Pop3Username", Description = "phrase: POP3 Username", LastModified = "2011/03/05", Value = "POP3 Username")]
    public string Pop3Username => this[nameof (Pop3Username)];

    /// <summary>phrase: POP3 Password</summary>
    [ResourceEntry("Pop3Password", Description = "phrase: POP3 Password", LastModified = "2011/03/05", Value = "POP3 Password")]
    public string Pop3Password => this[nameof (Pop3Password)];

    /// <summary>phrase: POP3 Server</summary>
    [ResourceEntry("Pop3ServerTitle", Description = "phrase: POP3 Server", LastModified = "2011/03/05", Value = "POP3 Server")]
    public string Pop3ServerTitle => this[nameof (Pop3ServerTitle)];

    /// <summary>phrase: The host address of the POP3 server</summary>
    [ResourceEntry("Pop3ServerDescription", Description = "phrase: The host address of the POP3 server", LastModified = "2011/03/05", Value = "The host address of the POP3 server")]
    public string Pop3ServerDescription => this[nameof (Pop3ServerDescription)];

    /// <summary>phrase: POP3 Username</summary>
    [ResourceEntry("Pop3UsernameTitle", Description = "phrase: POP3 Username", LastModified = "2011/03/05", Value = "POP3 Username")]
    public string Pop3UsernameTitle => this[nameof (Pop3UsernameTitle)];

    /// <summary>
    /// phrase: The username used to log into the POP3 account
    /// </summary>
    [ResourceEntry("Pop3UsernameDescription", Description = "phrase: The username used to log into the POP3 account", LastModified = "2011/03/05", Value = "The username used to log into the POP3 account")]
    public string Pop3UsernameDescription => this[nameof (Pop3UsernameDescription)];

    /// <summary>phrase: POP3 Password</summary>
    [ResourceEntry("Pop3PasswordTitle", Description = "phrase: POP3 Password", LastModified = "2011/03/05", Value = "POP3 Password")]
    public string Pop3PasswordTitle => this[nameof (Pop3PasswordTitle)];

    /// <summary>
    /// phrase: The password used to log into the POP3 account
    /// </summary>
    [ResourceEntry("Pop3PasswordDescription", Description = "phrase: The password used to log into the POP3 account", LastModified = "2011/03/05", Value = "The password used to log into the POP3 account.")]
    public string Pop3PasswordDescription => this[nameof (Pop3PasswordDescription)];

    /// <summary>phrase: POP3 port</summary>
    [ResourceEntry("Pop3Port", Description = "phrase: POP3 port", LastModified = "2011/03/05", Value = "POP3 port")]
    public string Pop3Port => this[nameof (Pop3Port)];

    /// <summary>phrase: POP3 port number</summary>
    [ResourceEntry("POP3PortTitle", Description = "phrase: POP3 port number", LastModified = "2011/03/05", Value = "POP3 port number")]
    public string POP3PortTitle => this[nameof (POP3PortTitle)];

    /// <summary>
    /// phrase: The number of the port to be used by the POP3 client
    /// </summary>
    [ResourceEntry("Pop3PortDescription", Description = "phrase: The number of the port to be used by the POP3 client", LastModified = "2011/03/05", Value = "The number of the port to be used by the POP3 client")]
    public string Pop3PortDescription => this[nameof (Pop3PortDescription)];

    /// <summary>phrase: POP3 uses SSL</summary>
    [ResourceEntry("Pop3UsesSSL", Description = "phrase: POP3 uses SSL", LastModified = "2011/03/05", Value = "Use SSL for connection to POP3 server")]
    public string Pop3UsesSSL => this[nameof (Pop3UsesSSL)];

    /// <summary>phrase: Pop3 Uses SSL</summary>
    [ResourceEntry("Pop3UsesSSLTitle", Description = "phrase: Pop3 Uses SSL", LastModified = "2011/03/05", Value = "Pop3 uses SSL?")]
    public string Pop3UsesSSLTitle => this[nameof (Pop3UsesSSLTitle)];

    /// <summary>
    /// phrase: Determines whether POP3 client should connect with SSL
    /// </summary>
    [ResourceEntry("Pop3UsesSSLDescription", Description = "phrase: Determines whether POP3 client should connect with SSL", LastModified = "2012/01/05", Value = "Determines whether POP3 client should connect with SSL")]
    public string Pop3UsesSSLDescription => this[nameof (Pop3UsesSSLDescription)];

    /// <summary>phrase: POP3 Settings</summary>
    [ResourceEntry("Pop3Settings", Description = "phrase: POP3 Settings", LastModified = "2011/03/05", Value = "POP3 Settings")]
    public string Pop3Settings => this[nameof (Pop3Settings)];

    /// <summary>phrase: Use SMTP SSL</summary>
    [ResourceEntry("UseSmtpSSL", Description = "phrase: Use SMTP SSL", LastModified = "2011/03/05", Value = "Use SMTP SSL")]
    public string UseSmtpSSL => this[nameof (UseSmtpSSL)];

    /// <summary>
    /// phrase: POP3 port number must be a value between 0 and 1023
    /// </summary>
    [ResourceEntry("Pop3PortOutOfRange", Description = "phrase: POP3 port number must be a value between 0 and 1023", LastModified = "2011/03/05", Value = "POP3 port number must be a value between 0 and 1023")]
    public string Pop3PortOutOfRange => this[nameof (Pop3PortOutOfRange)];

    /// <summary>
    /// phrase: SMTP port number must be a value between 0 and 1023
    /// </summary>
    [ResourceEntry("SMTPPortOutOfRange", Description = "phrase: SMTP port number must be a value between 0 and 65535", LastModified = "2011/10/13", Value = "SMTP port number must be a value between 0 and 65535")]
    public string SMTPPortOutOfRange => this[nameof (SMTPPortOutOfRange)];

    /// <summary>phrase: Track bounced messages</summary>
    [ResourceEntry("TrackBouncedMessages", Description = "phrase: Track bounced messages", LastModified = "2011/03/05", Value = "Track bounced messages")]
    public string TrackBouncedMessages => this[nameof (TrackBouncedMessages)];

    /// <summary>phrase: Track bounced emails for Email campaigns</summary>
    [ResourceEntry("TrackBouncedEmails", Description = "phrase: Track bounced emails for Email campaigns", LastModified = "2019/02/20", Value = "Track bounced emails for Email campaigns")]
    public string TrackBouncedEmails => this[nameof (TrackBouncedEmails)];

    /// <summary>phrase: Bounce actions</summary>
    [ResourceEntry("BounceActions", Description = "phrase: Bounce actions", LastModified = "2011/03/05", Value = "Bounce actions")]
    public string BounceActions => this[nameof (BounceActions)];

    /// <summary>phrase: Bounce email settings</summary>
    [ResourceEntry("BouncedEmailsSettings", Description = "phrase: Bounced emails settings", LastModified = "2015-10-007", Value = "Bounced emails settings")]
    public string BouncedEmailsSettings => this[nameof (BouncedEmailsSettings)];

    /// <summary>phrase: Bounce email</summary>
    [ResourceEntry("BouncedEmails", Description = "phrase: Bounced emails", LastModified = "2019/01/21", Value = "Bounced emails")]
    public string BouncedEmails => this[nameof (BouncedEmails)];

    /// <summary>phrase: Soft bounce</summary>
    [ResourceEntry("SoftBounceAction", Description = "phrase: Soft bounce", LastModified = "2015/06/23", Value = "Soft bounce")]
    public string SoftBounceAction => this[nameof (SoftBounceAction)];

    /// <summary>phrase: On soft bounce</summary>
    [ResourceEntry("OnSoftBounce", Description = "phrase: On soft bounce", LastModified = "2019/02/20", Value = "On soft bounce")]
    public string OnSoftBounce => this[nameof (OnSoftBounce)];

    /// <summary>phrase: SMTP server provider</summary>
    [ResourceEntry("SMTPServiceProvider", Description = "phrase: Select email service", LastModified = "2015-07-10", Value = "Select email service")]
    public string SMTPServiceProvider => this[nameof (SMTPServiceProvider)];

    /// <summary>phrase: Hard bounce</summary>
    [ResourceEntry("HardBounceAction", Description = "phrase: Hard bounce", LastModified = "2015/06/23", Value = "Hard bounce")]
    public string HardBounceAction => this[nameof (HardBounceAction)];

    /// <summary>phrase: On hard bounce</summary>
    [ResourceEntry("OnHardBounce", Description = "phrase: On hard bounce", LastModified = "2019/02/20", Value = "On hard bounce")]
    public string OnHardBounce => this[nameof (OnHardBounce)];

    /// <summary>phrase: Delete subscriber</summary>
    [ResourceEntry("DeleteSubscriber", Description = "phrase: Delete subscriber", LastModified = "2011/03/05", Value = "Delete subscriber")]
    public string DeleteSubscriber => this[nameof (DeleteSubscriber)];

    /// <summary>phrase: Suspend subscriber</summary>
    [ResourceEntry("SuspendSubscriber", Description = "phrase: Suspend subscriber", LastModified = "2011/03/05", Value = "Suspend subscriber")]
    public string SuspendSubscriber => this[nameof (SuspendSubscriber)];

    /// <summary>phrase: Retry sending</summary>
    [ResourceEntry("RetrySending", Description = "phrase: Retry sending", LastModified = "2015/06/23", Value = "Retry sending")]
    public string RetrySending => this[nameof (RetrySending)];

    /// <summary>phrase: Do nothing</summary>
    [ResourceEntry("DoNothing", Description = "phrase: Do nothing", LastModified = "2011/03/05", Value = "Do nothing")]
    public string DoNothing => this[nameof (DoNothing)];

    /// <summary>phrase: Connect this list</summary>
    [ResourceEntry("ConnectThisList", Description = "phrase: Connect this list", LastModified = "2011/03/05", Value = "Connect this list")]
    public string ConnectThisList => this[nameof (ConnectThisList)];

    /// <summary>
    /// phrase: This setting allows you to pull subscribers dynamically from an outside data source.
    /// </summary>
    [ResourceEntry("DynamicListDescription", Description = "phrase: This setting allows you to pull subscribers dynamically from an outside data source.", LastModified = "2011/03/05", Value = "This setting allows you to pull subscribers dynamically from an outside data source.")]
    public string DynamicListDescription => this[nameof (DynamicListDescription)];

    /// <summary>phrase: Connect this list to:</summary>
    [ResourceEntry("ConnectThisListTo", Description = "phrase: Connect this list to", LastModified = "2011/03/05", Value = "Connect this list to:")]
    public string ConnectThisListTo => this[nameof (ConnectThisListTo)];

    /// <summary>phrase: Not connected</summary>
    [ResourceEntry("NotConnected", Description = "phrase: Not connected", LastModified = "2011/03/05", Value = "Not connected")]
    public string NotConnected => this[nameof (NotConnected)];

    /// <summary>phrase: Dynamic list providers</summary>
    [ResourceEntry("DynamicListProviders", Description = "phrase: Dynamic list providers", LastModified = "2011/03/05", Value = "Dynamic list providers")]
    public string DynamicListProviders => this[nameof (DynamicListProviders)];

    /// <summary>phrase: Connection title</summary>
    [ResourceEntry("ConnectionTitle", Description = "Title of the dynamic list connection", LastModified = "2011/04/02", Value = "Connection title")]
    public string ConnectionTitle => this[nameof (ConnectionTitle)];

    /// <summary>Description of the connection title field</summary>
    [ResourceEntry("ConnectionTitleDescription", Description = "Description of the connection title field", LastModified = "2011/03/05", Value = "Title of the connection; e.g. 'Sitefinity small business edition customers'")]
    public string ConnectionTitleDescription => this[nameof (ConnectionTitleDescription)];

    /// <summary>
    /// Description of the field with all the available dynamic lists
    /// </summary>
    [ResourceEntry("DynamicListsFieldDescription", Description = "Description of the field with all the available dynamic lists", LastModified = "2011/03/05", Value = "Choose the dynamic list to which you wish to connect; dynamic lists are external data sources of your subscribers.")]
    public string DynamicListsFieldDescription => this[nameof (DynamicListsFieldDescription)];

    /// <summary>phrase: Map required fields</summary>
    [ResourceEntry("MapRequiredFields", Description = "phrase: Map required fields", LastModified = "2011/03/05", Value = "Map required fields")]
    public string MapRequiredFields => this[nameof (MapRequiredFields)];

    /// <summary>phrase: Test SMTP settings</summary>
    [ResourceEntry("SmtpTestTitle", Description = "phrase: Test SMTP settings", LastModified = "2011/04/10", Value = "Test SMTP settings")]
    public string SmtpTestTitle => this[nameof (SmtpTestTitle)];

    /// <summary>
    /// phrase: Test your SMTP settings by sending a test email message
    /// </summary>
    [ResourceEntry("SmtpTestNote", Description = "phrase: Test your SMTP settings by sending a test email message", LastModified = "2011/04/10", Value = "Test your SMTP settings by sending a test email message")]
    public string SmtpTestNote => this[nameof (SmtpTestNote)];

    /// <summary>phrase: Test POP3 settings</summary>
    [ResourceEntry("TestPop3Settings", Description = "phrase: Test POP3 settings", LastModified = "2011/03/05", Value = "Test POP3 settings")]
    public string TestPop3Settings => this[nameof (TestPop3Settings)];

    /// <summary>phrase: Perform a test</summary>
    [ResourceEntry("PerformATest", Description = "phrase: Perform a test", LastModified = "2011/03/05", Value = "Perform a test")]
    public string PerformATest => this[nameof (PerformATest)];

    /// <summary>phrase: Base template (you can change it later)</summary>
    [ResourceEntry("BaseTemplateTitle", Description = "phrase: Base template (you can change it later)", LastModified = "2011/03/05", Value = "Base template (you can change it later)")]
    public string BaseTemplateTitle => this[nameof (BaseTemplateTitle)];

    /// <summary>phrase: Compose the template in page editor</summary>
    [ResourceEntry("ComposeTheTemplateInPageEditor", Description = "phrase: Compose the template in page editor", LastModified = "2011/03/05", Value = "Compose the template in page editor")]
    public string ComposeTheTemplateInPageEditor => this[nameof (ComposeTheTemplateInPageEditor)];

    /// <summary>phrase: Use a template</summary>
    [ResourceEntry("CreateCampaignFromTemplate", Description = "phrase: Use a template", LastModified = "2011/11/07", Value = "Use a template")]
    public string CreateCampaignFromTemplate => this[nameof (CreateCampaignFromTemplate)];

    /// <summary>phrase: Start from scratch</summary>
    [ResourceEntry("CreateCampaignFromScratch", Description = "phrase: Start from scratch", LastModified = "2011/11/07", Value = "Start from scratch")]
    public string CreateCampaignFromScratch => this[nameof (CreateCampaignFromScratch)];

    /// <summary>phrase: Plain text version</summary>
    [ResourceEntry("PlainTextVersion", Description = "phrase: Plain text version", LastModified = "2011/03/05", Value = "Plain text version")]
    public string PlainTextVersion => this[nameof (PlainTextVersion)];

    /// <summary>
    /// phrase: Every email message should have plain text version; otherwise your message may be treated as spam
    /// </summary>
    [ResourceEntry("PlainTextVersionDescription", Description = "phrase: Every email message should have plain text version; otherwise your message may be treated as spam", LastModified = "2011/04/10", Value = "Every email message should have plain text version; otherwise your message may be treated as spam")]
    public string PlainTextVersionDescription => this[nameof (PlainTextVersionDescription)];

    /// <summary>phrase: Automatically generate plain text version</summary>
    [ResourceEntry("AutomaticallyGeneratePlainTextVersion", Description = "phrase: Automatically generate plain text version", LastModified = "2011/03/05", Value = "Automatically generate plain text version")]
    public string AutomaticallyGeneratePlainTextVersion => this[nameof (AutomaticallyGeneratePlainTextVersion)];

    /// <summary>phrase: Manually enter plain text version</summary>
    [ResourceEntry("ManuallyEnterPlainTextVersion", Description = "phrase: Manually enter plain text version", LastModified = "2011/03/05", Value = "Manually enter plain text version")]
    public string ManuallyEnterPlainTextVersion => this[nameof (ManuallyEnterPlainTextVersion)];

    /// <summary>phrase: Enable Google tracking</summary>
    [ResourceEntry("EnableGoogleAnalyticsTrackingForThisCampaign", Description = "phrase: Enable Google tracking ", LastModified = "2012/03/16", Value = "Enable Google tracking ")]
    public string EnableGoogleAnalyticsTrackingForThisCampaign => this[nameof (EnableGoogleAnalyticsTrackingForThisCampaign)];

    /// <summary>phrase: Clicked links</summary>
    [ResourceEntry("ClickedLinks", Description = "phrase: Clicked links", LastModified = "2011/03/05", Value = "Clicked links")]
    public string ClickedLinks => this[nameof (ClickedLinks)];

    /// <summary>phrase: Link url</summary>
    [ResourceEntry("LinkUrl", Description = "phrase: Link url", LastModified = "2011/03/05", Value = "Link url")]
    public string LinkUrl => this[nameof (LinkUrl)];

    /// <summary>phrase: Clicked on</summary>
    [ResourceEntry("ClickedOn", Description = "phrase: Clicked on", LastModified = "2011/03/05", Value = "Clicked on")]
    public string ClickedOn => this[nameof (ClickedOn)];

    /// <summary>phrase: Belongs to mailing lists</summary>
    [ResourceEntry("BelongsToMailingLists", Description = "phrase: Belongs to mailing lists", LastModified = "2011/03/05", Value = "Belongs to mailing lists")]
    public string BelongsToMailingLists => this[nameof (BelongsToMailingLists)];

    /// <summary>word: Joined</summary>
    [ResourceEntry("Joined", Description = "word: Joined", LastModified = "2011/03/05", Value = "Joined")]
    public string Joined => this[nameof (Joined)];

    /// <summary>phrase: Create an A/B test</summary>
    [ResourceEntry("CreateABCampaign", Description = "phrase: Create an A/B test", LastModified = "2012/06/05", Value = "Create an A/B test")]
    public string CreateABCampaign => this[nameof (CreateABCampaign)];

    /// <summary>phrase: Edit A/B test</summary>
    [ResourceEntry("EditABCampaign", Description = "phrase: Edit A/B test", LastModified = "2012/06/18", Value = "Edit A/B test")]
    public string EditABCampaign => this[nameof (EditABCampaign)];

    /// <summary>phrase: Create new A/B test</summary>
    [ResourceEntry("CreateNewABCampaign", Description = "phrase: Create new A/B test", LastModified = "2011/03/05", Value = "Create new A/B test")]
    public string CreateNewABCampaign => this[nameof (CreateNewABCampaign)];

    /// <summary>phrase: Select campaign A</summary>
    [ResourceEntry("SelectCampaignA", Description = "phrase: Select campaign A", LastModified = "2011/03/05", Value = "Select campaign A")]
    public string SelectCampaignA => this[nameof (SelectCampaignA)];

    /// <summary>phrase: Select campaign B</summary>
    [ResourceEntry("SelectCampaignB", Description = "phrase: Select campaign B", LastModified = "2011/03/05", Value = "Select campaign B")]
    public string SelectCampaignB => this[nameof (SelectCampaignB)];

    /// <summary>
    /// phrase: Campaign B must share the same mailing list as the selected campaign A
    /// </summary>
    [ResourceEntry("SelectCampaignBDescription", Description = "phrase: Campaign B must share the same mailing list as the selected campaign A", LastModified = "2011/03/05", Value = "Campaign B must share the same mailing list as the selected campaign A")]
    public string SelectCampaignBDescription => this[nameof (SelectCampaignBDescription)];

    /// <summary>
    /// phrase: After the testing period is over, send the campaign with...
    /// </summary>
    [ResourceEntry("WinningFactorTitle", Description = "phrase: After the testing period is over, send the campaign with...", LastModified = "2011/03/05", Value = "After the testing period is over, send the campaign with...")]
    public string WinningFactorTitle => this[nameof (WinningFactorTitle)];

    /// <summary>phrase: More opened emails</summary>
    [ResourceEntry("MoreOpenedEmails", Description = "phrase: More opened emails", LastModified = "2011/03/05", Value = "More opened emails")]
    public string MoreOpenedEmails => this[nameof (MoreOpenedEmails)];

    /// <summary>phrase: More link clicks</summary>
    [ResourceEntry("MoreLinkClicks", Description = "phrase: More link clicks", LastModified = "2011/03/05", Value = "More link clicks")]
    public string MoreLinkClicks => this[nameof (MoreLinkClicks)];

    /// <summary>phrase: Less bounces</summary>
    [ResourceEntry("LessBounces", Description = "phrase: Less bounces", LastModified = "2011/03/05", Value = "Less bounces")]
    public string LessBounces => this[nameof (LessBounces)];

    /// <summary>
    /// phrase: As a testing sample, use following percentage of the subscribers
    /// </summary>
    [ResourceEntry("TestSampleTitle", Description = "phrase: As a testing sample, use following percentage of the subscribers", LastModified = "2011/03/05", Value = "As a testing sample, use following percentage of the subscribers:")]
    public string TestSampleTitle => this[nameof (TestSampleTitle)];

    /// <summary>phrase: Decide the winning campaign on</summary>
    [ResourceEntry("TestingPeriodEnd", Description = "phrase: Decide the winning campaign on", LastModified = "2011/03/05", Value = "Decide the winning campaign on")]
    public string TestingPeriodEnd => this[nameof (TestingPeriodEnd)];

    /// <summary>phrase: A/B tests</summary>
    [ResourceEntry("ABCampaigns", Description = "phrase: A/B tests", LastModified = "2011/03/05", Value = "A/B tests")]
    public string ABCampaigns => this[nameof (ABCampaigns)];

    /// <summary>phrase: A/B Test title</summary>
    [ResourceEntry("ABTestName", Description = "phrase: A/B Test title", LastModified = "2011/11/07", Value = "A/B Test title")]
    public string ABTestName => this[nameof (ABTestName)];

    /// <summary>phrase: Campaign A name</summary>
    [ResourceEntry("CampaignAName", Description = "phrase: Campaign A name", LastModified = "2011/03/05", Value = "Campaign A name")]
    public string CampaignAName => this[nameof (CampaignAName)];

    /// <summary>phrase: Campaign B name</summary>
    [ResourceEntry("CampaignBName", Description = "phrase: Campaign B name", LastModified = "2011/03/05", Value = "Campaign B name")]
    public string CampaignBName => this[nameof (CampaignBName)];

    /// <summary>phrase: Winning condition</summary>
    [ResourceEntry("WinningCondition", Description = "phrase: Winning condition", LastModified = "2011/03/05", Value = "Winning condition")]
    public string WinningCondition => this[nameof (WinningCondition)];

    /// <summary>phrase: Test sample</summary>
    [ResourceEntry("TestSample", Description = "phrase: Test sample", LastModified = "2011/03/05", Value = "Test sample")]
    public string TestSample => this[nameof (TestSample)];

    /// <summary>phrase: Testing ends</summary>
    [ResourceEntry("TestingEnds", Description = "phrase: Testing ends", LastModified = "2011/03/05", Value = "Testing ends")]
    public string TestingEnds => this[nameof (TestingEnds)];

    /// <summary>phrase: No A/B tests have been created yet</summary>
    [ResourceEntry("NoABCampaignsCreatedYet", Description = "phrase: No A/B tests have been created yet", LastModified = "2011/11/01", Value = "No A/B tests have been created yet")]
    public string NoABCampaignsCreatedYet => this[nameof (NoABCampaignsCreatedYet)];

    /// <summary>phrase: Start testing</summary>
    [ResourceEntry("StartTesting", Description = "phrase: Start testing", LastModified = "2011/03/05", Value = "Start testing")]
    public string StartTesting => this[nameof (StartTesting)];

    /// <summary>phrase: End testing</summary>
    [ResourceEntry("EndTesting", Description = "phrase: End testing", LastModified = "2011/03/05", Value = "End testing")]
    public string EndTesting => this[nameof (EndTesting)];

    /// <summary>
    /// phrase: Are you sure that you want to delete this A/B test?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteABCampaign", Description = "phrase: Are you sure that you want to delete this A/B test?", LastModified = "2011/03/05", Value = "Are you sure that you want to delete this A/B test?")]
    public string AreYouSureDeleteABCampaign => this[nameof (AreYouSureDeleteABCampaign)];

    /// <summary>
    /// phrase: Are you sure that you want to delete this A/B test(s)?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteABCampaigns", Description = "phrase: Are you sure that you want to delete this A/B test(s)?", LastModified = "2011/03/05", Value = "Are you sure that you want to delete this A/B test(s)?")]
    public string AreYouSureDeleteABCampaigns => this[nameof (AreYouSureDeleteABCampaigns)];

    /// <summary>phrase: A/B test has been sent successfully</summary>
    [ResourceEntry("ABCampaignSentSuccessfully", Description = "phrase: A/B test has been sent successfully", LastModified = "2011/03/05", Value = "A/B test has been sent successfully")]
    public string ABCampaignSentSuccessfully => this[nameof (ABCampaignSentSuccessfully)];

    /// <summary>phrase: New A/B test has been successfully created</summary>
    [ResourceEntry("NewABCampaignSuccessfullyCreated", Description = "phrase: New A/B test has been successfully created", LastModified = "2011/03/05", Value = "New A/B test has been successfully created")]
    public string NewABCampaignSuccessfullyCreated => this[nameof (NewABCampaignSuccessfullyCreated)];

    /// <summary>phrase: A/B test has been successfully saved</summary>
    [ResourceEntry("ABCampaignSuccessfullySaved", Description = "phrase: A/B test has been successfully saved", LastModified = "2011/03/05", Value = "A/B test has been successfully saved")]
    public string ABCampaignSuccessfullySaved => this[nameof (ABCampaignSuccessfullySaved)];

    /// <summary>phrase: AB test successfully started</summary>
    [ResourceEntry("ABTestSuccessfullySent", Description = "phrase: AB test successfully started", LastModified = "2011/10/27", Value = "AB test successfully sent.")]
    public string ABTestSuccessfullySent => this[nameof (ABTestSuccessfullySent)];

    /// <summary>
    /// phrase: AB test has been ended. You should manually decide which is the winning campaign
    /// </summary>
    [ResourceEntry("ABTestEndManualDecision", Description = "phrase: AB test has been ended. You should manually decide which is the winning campaign", LastModified = "2011/10/27", Value = "AB test has been ended. You should manually decide which is the winning campaign")]
    public string ABTestEndManualDecision => this[nameof (ABTestEndManualDecision)];

    /// <summary>
    /// phrase: AB test has ended. Winning campaign {0} is sent
    /// </summary>
    [ResourceEntry("ABTestEnded", Description = "phrase: AB test has ended. Winning campaign {0} is sent", LastModified = "2011/10/27", Value = "AB test has ended. Winning campaign {0} is sent")]
    public string ABTestEnded => this[nameof (ABTestEnded)];

    /// <summary>
    /// phrase: If this test is ended the winning campaign will be sent. Are you sure you want to continue?
    /// </summary>
    [ResourceEntry("ABTestEndConfirmation", Description = "phrase: If this test is ended the winning campaign will be sent. Are you sure you want to continue?", LastModified = "2011/10/27", Value = "If this test is ended the winning campaign will be sent. Are you sure you want to continue?")]
    public string ABTestEndConfirmation => this[nameof (ABTestEndConfirmation)];

    /// <summary>
    /// phrase: Campaign {0} is marked as a winner and will be sent to the subscribers. Are you sure you want to continue?
    /// </summary>
    [ResourceEntry("ABTestDecisionConfirmation", Description = "phrase: Campaign {0} is marked as a winner and will be sent to the subscribers. Are you sure you want to continue?", LastModified = "2011/10/27", Value = "Campaign {0} is marked as a winner and will be sent to the subscribers. Are you sure you want to continue?")]
    public string ABTestDecisionConfirmation => this[nameof (ABTestDecisionConfirmation)];

    /// <summary>phrase: Back to all A/B tests</summary>
    [ResourceEntry("BackToAllABCampaigns", Description = "phrase: Back to all A/B tests", LastModified = "2011/03/05", Value = "Back to all A/B tests")]
    public string BackToAllABCampaigns => this[nameof (BackToAllABCampaigns)];

    /// <summary>phrase: Back to A/B test report.</summary>
    [ResourceEntry("BackToABCampaignReport", Description = "phrase: Back to A/B test report.", LastModified = "2011/06/10", Value = "Back to A/B test report")]
    public string BackToABCampaignReport => this[nameof (BackToABCampaignReport)];

    /// <summary>phrase: I will manually decide the winning campaign</summary>
    [ResourceEntry("ManualWinningDecision", Description = "phrase: I will manually decide the winning campaign", LastModified = "2011/03/05", Value = "I will manually decide the winning campaign")]
    public string ManualWinningDecision => this[nameof (ManualWinningDecision)];

    /// <summary>phrase: Add connection</summary>
    [ResourceEntry("AddConnection", Description = "phrase: Add connection", LastModified = "2011/03/05", Value = "Add connection")]
    public string AddConnection => this[nameof (AddConnection)];

    /// <summary>phrase: Connection source</summary>
    [ResourceEntry("ConnectionSource", Description = "phrase: Connection source", LastModified = "2011/03/05", Value = "Connection source")]
    public string ConnectionSource => this[nameof (ConnectionSource)];

    /// <summary>
    /// phrase: The source of the connection. For example 'Forms module' will provide all Forms as possible connections.
    /// </summary>
    [ResourceEntry("ConnectionSourceDescription", Description = "phrase: The source of the connection. For example 'Forms module' will provide all Forms as possible connections.", LastModified = "2011/03/05", Value = "The source of the connection. For example 'Forms module' will provide all Forms as possible connections.")]
    public string ConnectionSourceDescription => this[nameof (ConnectionSourceDescription)];

    /// <summary>phrase: Sitefinity users</summary>
    [ResourceEntry("UsersDynamicListProviderTitle", Description = "phrase: Sitefinity users", LastModified = "2011/03/05", Value = "Sitefinity users")]
    public string UsersDynamicListProviderTitle => this[nameof (UsersDynamicListProviderTitle)];

    /// <summary>phrase: Sitefinity Forms module</summary>
    [ResourceEntry("FormsDynamicListProviderTitle", Description = "phrase: Sitefinity Forms module", LastModified = "2011/03/05", Value = "Sitefinity Forms module")]
    public string FormsDynamicListProviderTitle => this[nameof (FormsDynamicListProviderTitle)];

    /// <summary>phrase: -- Select connection source --</summary>
    [ResourceEntry("SelectConnectionSource", Description = "phrase: -- Select connection source --", LastModified = "2012/07/05", Value = "-- Select connection source --")]
    public string SelectConnectionSource => this[nameof (SelectConnectionSource)];

    /// <summary>phrase: Dynamic list filter expression</summary>
    [ResourceEntry("DynamicListFilterTitle", Description = "phrase: Dynamic list filter expression", LastModified = "2011/03/05", Value = "Dynamic list filter expression")]
    public string DynamicListFilterTitle => this[nameof (DynamicListFilterTitle)];

    [ResourceEntry("DynamicListFilterDescription", Description = "phrase: Use dynamic LINQ syntax with the merge tags. For example, if merge tag is {|CustomerForm.State|}, you can write a filter like: State <> 'California'", LastModified = "2012/01/05", Value = "Use dynamic LINQ syntax with the merge tags. For example, if merge tag is {|CustomerForm.State|}, you can write a filter like: State <> 'California'")]
    public string DynamicListFilterDescription => this[nameof (DynamicListFilterDescription)];

    /// <summary>phrase: Select mailing list</summary>
    [ResourceEntry("SubscriberDesignerSelectListTitle", Description = "phrase: Select mailing list", LastModified = "2011/03/05", Value = "Select mailing list")]
    public string SubscriberDesignerSelectListTitle => this[nameof (SubscriberDesignerSelectListTitle)];

    /// <summary>
    /// Select the mailing list to which you wish the users to subscribe to
    /// </summary>
    [ResourceEntry("SubscribeDesignerSelectListDescription", Description = "Select the mailing list to which you wish the users to subscribe to", LastModified = "2011/03/05", Value = "Select the mailing list to which you wish the users to subscribe to")]
    public string SubscribeDesignerSelectListDescription => this[nameof (SubscribeDesignerSelectListDescription)];

    /// <summary>phrase: Subscribe to list</summary>
    [ResourceEntry("SubscribeToList", Description = "phrase: Subscribe to list", LastModified = "2011/03/05", Value = "Subscribe to the mailing list")]
    public string SubscribeToList => this[nameof (SubscribeToList)];

    /// <summary>phrase: Click edit and select mailing list</summary>
    [ResourceEntry("ClickEditAndSelectList", Description = "phrase: Click edit and select mailing list", LastModified = "2011/03/05", Value = "Click edit and select mailing list")]
    public string ClickEditAndSelectList => this[nameof (ClickEditAndSelectList)];

    /// <summary>phrase: Unsubscribe message</summary>
    [ResourceEntry("UnsubscribeMessageOnSuccess", Description = "phrase: Click edit and select mailing list", LastModified = "2011/03/05", Value = "<strong>Unsubscribe successful</strong> <br/> You have been successfully unsubscribed from our newsletter ({|Subscriber.Email|}) <br /> If you change your mind you can {|MergeContextItems.SubscribeLink|} to our newsletter again. <br /> Thank you.")]
    public string UnsubscribeMessageOnSuccess => this[nameof (UnsubscribeMessageOnSuccess)];

    /// <summary>phrase: Select mailing list</summary>
    [ResourceEntry("UnsubscribeDesignerSelectListTitle", Description = "phrase: Select mailing list", LastModified = "2011/03/05", Value = "Select mailing list")]
    public string UnsubscribeDesignerSelectListTitle => this[nameof (UnsubscribeDesignerSelectListTitle)];

    /// <summary>
    /// phrase: Select the mailing list from which the user should unsubscribe.
    /// </summary>
    [ResourceEntry("UnsubscribeDesignerSelectListDescription", Description = "phrase: Select the mailing list from which the user should unsubscribe.", LastModified = "2011/03/05", Value = "Select the mailing list from which the user should unsubscribe.")]
    public string UnsubscribeDesignerSelectListDescription => this[nameof (UnsubscribeDesignerSelectListDescription)];

    /// <summary>phrase: Missing mailing list</summary>
    [ResourceEntry("CampaignStateMissingMailingList", Description = "phrase: Missing mailing list", LastModified = "2011/03/05", Value = "Missing mailing list")]
    public string CampaignStateMissingMailingList => this[nameof (CampaignStateMissingMailingList)];

    /// <summary>phrase: General information</summary>
    [ResourceEntry("GeneralInformation", Description = "phrase: General information", LastModified = "2011/03/05", Value = "General information")]
    public string GeneralInformation => this[nameof (GeneralInformation)];

    /// <summary>phrase: Testing status</summary>
    [ResourceEntry("TestingStatus", Description = "phrase: Testing status", LastModified = "2011/03/05", Value = "Testing status")]
    public string TestingStatus => this[nameof (TestingStatus)];

    /// <summary>phrase: Test status</summary>
    [ResourceEntry("TestStatus", Description = "phrase: Test status", LastModified = "2011/03/05", Value = "Test status")]
    public string TestStatus => this[nameof (TestStatus)];

    /// <summary>phrase: So far sent messages</summary>
    [ResourceEntry("SoFarSentMessages", Description = "phrase: So far sent messages", LastModified = "2011/03/05", Value = "So far sent messages")]
    public string SoFarSentMessages => this[nameof (SoFarSentMessages)];

    /// <summary>phrase: Test results</summary>
    [ResourceEntry("TestResults", Description = "phrase: Test results", LastModified = "2011/03/05", Value = "Test results")]
    public string TestResults => this[nameof (TestResults)];

    /// <summary>phrase: Campaign A</summary>
    [ResourceEntry("CampaignA", Description = "phrase: Campaign A", LastModified = "2011/03/05", Value = "Campaign A")]
    public string CampaignA => this[nameof (CampaignA)];

    /// <summary>phrase: Campaign B</summary>
    [ResourceEntry("CampaignB", Description = "phrase: Campaign B", LastModified = "2011/03/05", Value = "Campaign B")]
    public string CampaignB => this[nameof (CampaignB)];

    /// <summary>phrase: Full report</summary>
    [ResourceEntry("FullReport", Description = "phrase: Full report", LastModified = "2011/03/05", Value = "Full report")]
    public string FullReport => this[nameof (FullReport)];

    /// <summary>phrase: Number of opened messages</summary>
    [ResourceEntry("NumberOfOpenedMessages", Description = "phrase: Number of opened messages", LastModified = "2011/03/05", Value = "Number of opened messages")]
    public string NumberOfOpenedMessages => this[nameof (NumberOfOpenedMessages)];

    /// <summary>phrase: Number of clicked links</summary>
    [ResourceEntry("NumberOfClickedLinks", Description = "phrase: Number of clicked links", LastModified = "2011/03/05", Value = "Number of clicked links")]
    public string NumberOfClickedLinks => this[nameof (NumberOfClickedLinks)];

    /// <summary>phrase: See which links were clicked</summary>
    [ResourceEntry("SeeWhichLinksWereClicked", Description = "phrase: See which links were clicked", LastModified = "2011/03/05", Value = "See which links were clicked")]
    public string SeeWhichLinksWereClicked => this[nameof (SeeWhichLinksWereClicked)];

    /// <summary>phrase: Number of bounced messages</summary>
    [ResourceEntry("NumberOfBouncedMessages", Description = "phrase: Number of bounced messages", LastModified = "2011/03/05", Value = "Number of bounced messages")]
    public string NumberOfBouncedMessages => this[nameof (NumberOfBouncedMessages)];

    /// <summary>phrase: End testing and...</summary>
    [ResourceEntry("EndTestingAnd", Description = "phrase: End testing and...", LastModified = "2011/03/05", Value = "End testing and...")]
    public string EndTestingAnd => this[nameof (EndTestingAnd)];

    /// <summary>phrase: Make Campaign A winner</summary>
    [ResourceEntry("MakeCampaignAWinner", Description = "phrase: Make Campaign A winner", LastModified = "2011/03/05", Value = "Make Campaign A winner")]
    public string MakeCampaignAWinner => this[nameof (MakeCampaignAWinner)];

    /// <summary>phrase: Make Campaign B winner</summary>
    [ResourceEntry("MakeCampaignBWinner", Description = "phrase: Make Campaign B winner", LastModified = "2011/03/05", Value = "Make Campaign B winner")]
    public string MakeCampaignBWinner => this[nameof (MakeCampaignBWinner)];

    /// <summary>phrase: Message in HTML</summary>
    [ResourceEntry("MessageInHtml", Description = "phrase: Message in HTML", LastModified = "2011/03/05", Value = "Message in HTML")]
    public string MessageInHtml => this[nameof (MessageInHtml)];

    /// <summary>phrase: Message in plain text</summary>
    [ResourceEntry("MessageInPlainText", Description = "phrase: Message in plain text", LastModified = "2011/03/05", Value = "Message in plain text")]
    public string MessageInPlainText => this[nameof (MessageInPlainText)];

    /// <summary>phrase: Go back</summary>
    [ResourceEntry("GoBack", Description = "phrase: Go back", LastModified = "2011/03/05", Value = "Go back")]
    public string GoBack => this[nameof (GoBack)];

    /// <summary>phrase: Schedule campaign delivery</summary>
    [ResourceEntry("ScheduleCampaignDelivery", Description = "phrase: Schedule campaign delivery", LastModified = "2011/03/05", Value = "Schedule campaign delivery")]
    public string ScheduleCampaignDelivery => this[nameof (ScheduleCampaignDelivery)];

    /// <summary>phrase: Deliver campaign on</summary>
    [ResourceEntry("DeliverCampaignOn", Description = "phrase: Deliver campaign on", LastModified = "2011/03/05", Value = "Deliver campaign on")]
    public string DeliverCampaignOn => this[nameof (DeliverCampaignOn)];

    /// <summary>
    /// phrase: Select the date and time when you wish the campaign to be delivered to the subscribers.
    /// </summary>
    [ResourceEntry("DeliverCampaignOnDescription", Description = "phrase: Select the date and time when you wish the campaign to be delivered to the subscribers.", LastModified = "2011/03/05", Value = "Select the date and time when you wish the campaign to be delivered to the subscribers.")]
    public string DeliverCampaignOnDescription => this[nameof (DeliverCampaignOnDescription)];

    /// <summary>phrase: Schedule</summary>
    [ResourceEntry("Schedule", Description = "phrase: Schedule", LastModified = "2011/03/05", Value = "Schedule")]
    public string Schedule => this[nameof (Schedule)];

    /// <summary>phrase: In progress</summary>
    [ResourceEntry("InProgress", Description = "phrase: In progress", LastModified = "2011/03/05", Value = "In progress")]
    public string InProgress => this[nameof (InProgress)];

    /// <summary>word: Scheduled</summary>
    [ResourceEntry("Scheduled", Description = "word: Scheduled", LastModified = "2011/03/05", Value = "Scheduled")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>word: Stopped</summary>
    [ResourceEntry("Stopped", Description = "word: Stopped", LastModified = "2011/03/05", Value = "Stopped")]
    public string Stopped => this[nameof (Stopped)];

    /// <summary>phrase: Clicks count</summary>
    [ResourceEntry("ClicksCount", Description = "phrase: Clicks count", LastModified = "2011/03/05", Value = "Clicks count")]
    public string ClicksCount => this[nameof (ClicksCount)];

    /// <summary>phrase: of the test sample</summary>
    [ResourceEntry("OfTheTestSample", Description = "phrase: of the test sample", LastModified = "2011/03/05", Value = "of the test sample")]
    public string OfTheTestSample => this[nameof (OfTheTestSample)];

    /// <summary>phrase: Settings for newsletters</summary>
    [ResourceEntry("SettingsForNewsletters", Description = "phrase: Settings for newsletters", LastModified = "2011/06/07", Value = "Settings for email campaigns")]
    public string SettingsForNewsletters => this[nameof (SettingsForNewsletters)];

    /// <summary>
    /// Message displayed to users when creating a new mailing list and no message templates that could be used for welcome or goodbye message are present
    /// </summary>
    [ResourceEntry("MailingListNoTemplatesMessage", Description = "Message displayed to users when creating a new mailing list and no message templates that could be used for welcome or goodbye message are present.", LastModified = "2011/06/10", Value = "In order to be able to send welcome and follow up messages, you need to have at least one message template. <a href=\"{0}\" target=\"_top\" class=\"sfMoreDetails\">Create your first message template</a>")]
    public string MailingListNoTemplatesMessage => this[nameof (MailingListNoTemplatesMessage)];

    /// <summary>
    /// phrase: Skip the first row (check when first row represents the headers)
    /// </summary>
    [ResourceEntry("SkipFirstRowHeaders", Description = "phrase: Skip the first row (check when first row represents the headers)", LastModified = "2011/06/09", Value = "Skip the first row (check when first row represents the headers)")]
    public string SkipFirstRowHeaders => this[nameof (SkipFirstRowHeaders)];

    /// <summary>
    /// Message shown to user when searching for a mailing list and no mailing lists are found.
    /// </summary>
    [ResourceEntry("NoMailingListsFound", Description = "Message shown to user when searching for a mailing list and no mailing lists are found.", LastModified = "2011/06/10", Value = "No mailing lists were found.")]
    public string NoMailingListsFound => this[nameof (NoMailingListsFound)];

    /// <summary>
    /// Message displayed to the user when he or she unsubscribes from the mailing list.
    /// </summary>
    [ResourceEntry("UnsubscribedSuccessMessage", Description = "Message displayed to the user when he or she unsubscribes from the mailing list.", LastModified = "2011/06/10", Value = "You have successfully unsubscribed from the mailing list. You will no longer receive the newsletters sent to this mailing list.")]
    public string UnsubscribedSuccessMessage => this[nameof (UnsubscribedSuccessMessage)];

    /// <summary>
    /// Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.
    /// </summary>
    [ResourceEntry("YouDontBelongToTheMailingList", Description = "Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.", LastModified = "2011/06/10", Value = "You don't belong to the mailing list and cannot unsubscribe.")]
    public string YouDontBelongToTheMailingList => this[nameof (YouDontBelongToTheMailingList)];

    /// <summary>
    /// Message displayed to user who tries to unsubscribe and some error prevents the action.
    /// </summary>
    [ResourceEntry("YouCannotUnsubscribe", Description = "Message displayed to user who tries to unsubscribe and some error prevents the action.", LastModified = "2012/06/04", Value = "Error: You cannot unsubscribe.")]
    public string YouCannotUnsubscribe => this[nameof (YouCannotUnsubscribe)];

    /// <summary>
    /// The message displayed to users when they have more subscribers in the newsletters module than allowed.
    /// </summary>
    [ResourceEntry("LicenseWarning", Description = "The message displayed to users when they have more subscribers in the newsletters module than allowed.", LastModified = "2011/06/10", Value = "You have {0} subscribers in your system, which is more than permitted by your license. Please either remove some of the subscribers or upgrade your edition of Sitefinity. Until resolved, you will not be able to send campaigns.")]
    public string LicenseWarning => this[nameof (LicenseWarning)];

    /// <summary>Email Campaigns</summary>
    [ResourceEntry("NewslettersResourcesTitlePlural", Description = "Title plural for the newsletters module.", LastModified = "2011/07/21", Value = "Email Campaigns")]
    public string NewslettersResourcesTitlePlural => this[nameof (NewslettersResourcesTitlePlural)];

    /// <summary>Phrase: Campaign text</summary>
    [ResourceEntry("CampaignText", Description = "Phrase: Campaign text", LastModified = "2011/10/20", Value = "Campaign text")]
    public string CampaignText => this[nameof (CampaignText)];

    /// <summary>Phrase: Insert dynamic data in the text</summary>
    [ResourceEntry("InsertDynamicData", Description = "Phrase: Insert dynamic data in the text", LastModified = "2011/10/20", Value = "Insert dynamic data in the text")]
    public string InsertDynamicData => this[nameof (InsertDynamicData)];

    /// <summary>Phrase: Issue text</summary>
    [ResourceEntry("IssueText", Description = "Phrase: Campaign text", LastModified = "2011/10/20", Value = "Issue text")]
    public string IssueText => this[nameof (IssueText)];

    /// <summary>Menu title: Campaigns management</summary>
    [ResourceEntry("CampaignsManagement", Description = "Menu title: Campaigns management", LastModified = "2011/10/20", Value = "Campaigns management")]
    public string CampaignsManagement => this[nameof (CampaignsManagement)];

    /// <summary>Menu Url name: Campaigns management</summary>
    [ResourceEntry("CampaignsManagementUrlName", Description = "Menu Url name: Campaigns management", LastModified = "2011/10/20", Value = "campaigns-management")]
    public string CampaignsManagementUrlName => this[nameof (CampaignsManagementUrlName)];

    /// <summary>Menu title: Subscribers</summary>
    [ResourceEntry("SubscribersGroupTitle", Description = "Menu title: Subscribers", LastModified = "2011/10/06", Value = "Subscribers and lists")]
    public string SubscribersGroupTitle => this[nameof (SubscribersGroupTitle)];

    /// <summary>Localized group url: Subscribers</summary>
    [ResourceEntry("SubscribersGroupTitleUrlName", Description = "Localized group url: Subscribers", LastModified = "2011/10/03", Value = "subscribers-group")]
    public string SubscribersGroupTitleUrlName => this[nameof (SubscribersGroupTitleUrlName)];

    /// <summary>Phrase: Edit title</summary>
    [ResourceEntry("EditTitle", Description = "Phrase: Edit title", LastModified = "2011/10/04", Value = "Edit title")]
    public string EditTitle => this[nameof (EditTitle)];

    /// <summary>word: Name</summary>
    [ResourceEntry("Name", Description = "word: Name", LastModified = "2011/10/05", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>word: Browse</summary>
    [ResourceEntry("Browse", Description = "word: Browse", LastModified = "2011/10/06", Value = "Browse")]
    public string Browse => this[nameof (Browse)];

    /// <summary>
    /// phrase: In which mailing lists you want the subscribers to be imported?
    /// </summary>
    [ResourceEntry("InWichMailingListsToImportSubscribers", Description = "phrase: In which mailing lists you want the subscribers to be imported?", LastModified = "2011/10/06", Value = "In which mailing lists you want the subscribers to be imported?")]
    public string InWichMailingListsToImportSubscribers => this[nameof (InWichMailingListsToImportSubscribers)];

    /// <summary>phrase: Import subscribers to {0}</summary>
    [ResourceEntry("ImportSubscribersTo", Description = "phrase: Import subscribers to {0}", LastModified = "2011/10/06", Value = "Import subscribers to {0}")]
    public string ImportSubscribersTo => this[nameof (ImportSubscribersTo)];

    /// <summary>phrase: Used For</summary>
    [ResourceEntry("UsedFor", Description = "phrase: Used For", LastModified = "2011/10/10", Value = "Used For")]
    public string UsedFor => this[nameof (UsedFor)];

    /// <summary>phrase: 1 issue</summary>
    [ResourceEntry("OneCampaign", Description = "phrase: 1 issue", LastModified = "2012/05/21", Value = "1 issue")]
    public string OneCampaign => this[nameof (OneCampaign)];

    /// <summary>phrase: {0} issues</summary>
    [ResourceEntry("NumberOfCampaigns", Description = "phrase: {0} issues", LastModified = "2012/05/21", Value = "{0} issues")]
    public string NumberOfCampaigns => this[nameof (NumberOfCampaigns)];

    /// <summary>phrase: Mail settings</summary>
    [ResourceEntry("MailSettings", Description = "phrase: Mail settings", LastModified = "2011/10/06", Value = "Mail settings")]
    public string MailSettings => this[nameof (MailSettings)];

    /// <summary>phrase: (where the subscribers will be imported)</summary>
    [ResourceEntry("WhereTheSubscribersWillBeImported", Description = "phrase: (where the subscribers will be imported)", LastModified = "2011/10/11", Value = "(where the subscribers will be imported)")]
    public string WhereTheSubscribersWillBeImported => this[nameof (WhereTheSubscribersWillBeImported)];

    /// <summary>phrase: No first name</summary>
    [ResourceEntry("NoFirstName", Description = "phrase: No first name", LastModified = "2011/10/11", Value = "No first name")]
    public string NoFirstName => this[nameof (NoFirstName)];

    /// <summary>phrase: No last name</summary>
    [ResourceEntry("NoLastName", Description = "phrase: No last name", LastModified = "2011/10/11", Value = "No last name")]
    public string NoLastName => this[nameof (NoLastName)];

    /// <summary>phrase: 1 subscriber</summary>
    [ResourceEntry("OneSubscriber", Description = "phrase: 1 subscriber", LastModified = "2011/10/12", Value = "1 subscriber")]
    public string OneSubscriber => this[nameof (OneSubscriber)];

    /// <summary>phrase: {0} subscribers</summary>
    [ResourceEntry("NumberOfSubscribers", Description = "phrase: {0} subscribers", LastModified = "2011/10/12", Value = "{0} subscribers")]
    public string NumberOfSubscribers => this[nameof (NumberOfSubscribers)];

    /// <summary>phrase: 1 issue uses this mailing list</summary>
    [ResourceEntry("OneUsingCampaigns", Description = "phrase: 1 issue uses this mailing list", LastModified = "2012/05/21", Value = "1 issue uses this mailing list")]
    public string OneUsingCampaigns => this[nameof (OneUsingCampaigns)];

    /// <summary>phrase: {0} issues use this mailing list</summary>
    [ResourceEntry("NumberOfUsingCampaigns", Description = "phrase: {0} issues use this mailing list", LastModified = "2012/05/21", Value = "{0} issues use this mailing list")]
    public string NumberOfUsingCampaigns => this[nameof (NumberOfUsingCampaigns)];

    /// <summary>phrase: {0} Subscribers for {1}</summary>
    [ResourceEntry("NumberOfSubscribersFor", Description = "phrase: {0} Subscribers for {1}", LastModified = "2011/10/14", Value = "{0} Subscribers for {1}")]
    public string NumberOfSubscribersFor => this[nameof (NumberOfSubscribersFor)];

    /// <summary>phrase: Manage mailing list</summary>
    [ResourceEntry("ManageMailingList", Description = "phrase: Manage mailing list", LastModified = "2011/10/14", Value = "Manage mailing list")]
    public string ManageMailingList => this[nameof (ManageMailingList)];

    /// <summary>phrase: Add from existing</summary>
    [ResourceEntry("AddFromExisting", Description = "phrase: Add from existing", LastModified = "2011/11/17", Value = "Add from existing")]
    public string AddFromExisting => this[nameof (AddFromExisting)];

    /// <summary>phrase: Add existing subscribers to {0} mailing list</summary>
    [ResourceEntry("AddExistingSubscribers", Description = "phrase: Add existing subscribers to {0} mailing list", LastModified = "2011/11/17", Value = "Add existing subscribers to {0} mailing list")]
    public string AddExistingSubscribers => this[nameof (AddExistingSubscribers)];

    /// <summary>phrase: View report</summary>
    [ResourceEntry("ViewReport", Description = "phrase: View report", LastModified = "2011/11/21", Value = "View report")]
    public string ViewReport => this[nameof (ViewReport)];

    /// <summary>phrase: Remove from this list</summary>
    [ResourceEntry("RemoveFromThisList", Description = "phrase: Remove from this list", LastModified = "2011/11/21", Value = "Remove from this list")]
    public string RemoveFromThisList => this[nameof (RemoveFromThisList)];

    /// <summary>
    /// phrase: Are you sure you want to remove {0} subscribers from this mailing list?
    /// </summary>
    [ResourceEntry("AreYouSureRemoveSubscribers", Description = "phrase: Are you sure you want to remove {0} subscribers from this mailing list?", LastModified = "2011/11/21", Value = "Are you sure you want to remove {0} subscribers from this mailing list?")]
    public string AreYouSureRemoveSubscribers => this[nameof (AreYouSureRemoveSubscribers)];

    /// <summary>
    /// phrase: Are you sure you want to remove this subscriber from this mailing list?
    /// </summary>
    [ResourceEntry("AreYouSureRemoveSubscriber", Description = "phrase: Are you sure you want to remove this subscriber from this mailing list?", LastModified = "2011/11/21", Value = "Are you sure you want to remove this subscriber from this mailing list?")]
    public string AreYouSureRemoveSubscriber => this[nameof (AreYouSureRemoveSubscriber)];

    /// <summary>
    /// phrase: {0} users will be deleted and will be removed from all mailing lists in which they are added. Are you sure you want to delete {0} users?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteSubscribersPerMailingList", Description = "phrase: {0} users will be deleted and will be removed from all mailing lists in which they are added. Are you sure you want to delete {0} users?", LastModified = "2011/11/21", Value = "{0} users will be deleted and will be removed from all mailing lists in which they are added. Are you sure you want to delete {0} users?")]
    public string AreYouSureDeleteSubscribersPerMailingList => this[nameof (AreYouSureDeleteSubscribersPerMailingList)];

    /// <summary>
    /// phrase: This user will be deleted and will be removed from all mailing lists in which he is added. Are you sure you want to delete this user?
    /// </summary>
    [ResourceEntry("AreYouSureDeleteSubscriberPerMailingList", Description = "phrase: This user will be deleted and will be removed from all mailing lists in which he is added. Are you sure you want to delete this user?", LastModified = "2011/11/21", Value = "This user will be deleted and will be removed from all mailing lists in which he is added. Are you sure you want to delete this user?")]
    public string AreYouSureDeleteSubscriberPerMailingList => this[nameof (AreYouSureDeleteSubscriberPerMailingList)];

    /// <summary>word: Remove</summary>
    [ResourceEntry("Remove", Description = "word: Remove", LastModified = "2011/11/21", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    /// <summary>phrase: Subscribers for {0} mailing list</summary>
    [ResourceEntry("SubscribersForMailingList", Description = "phrase: Subscribers for {0} mailing list", LastModified = "2011/11/24", Value = "Subscribers for {0} mailing list")]
    public string SubscribersForMailingList => this[nameof (SubscribersForMailingList)];

    /// <summary>
    /// phrase: Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.
    /// </summary>
    [ResourceEntry("ModuleNotLicensed", Description = "Module Not Licensed message", LastModified = "2012/01/05", Value = "Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.")]
    public string ModuleNotLicensed => this[nameof (ModuleNotLicensed)];

    /// <summary>
    /// phrase: You must enter at least one test email address.
    /// </summary>
    [ResourceEntry("EnterAtLeastOneTestEmailAddress", Description = "phrase: You must enter at least one test email address.", LastModified = "2011/10/10", Value = "You must enter at least one test email address.")]
    public string EnterAtLeastOneTestEmailAddress => this[nameof (EnterAtLeastOneTestEmailAddress)];

    /// <summary>
    /// phrase: Notifications SMTP profile name cannot be an empty string.
    /// </summary>
    [ResourceEntry("NotificationsSmtpProfileNameCannotBeEmpty", Description = "phrase: Notifications SMTP profile name cannot be an empty string.", LastModified = "2012/05/30", Value = "Notifications SMTP profile name cannot be an empty string.")]
    public string NotificationsSmtpProfileNameCannotBeEmpty => this[nameof (NotificationsSmtpProfileNameCannotBeEmpty)];

    /// <summary>
    /// phrase: Notifications SMTP profile \"{0}\" set for newsletters cannot be found.
    /// </summary>
    [ResourceEntry("NotificationsSmtpProfileNotFound", Description = "phrase: Notifications SMTP profile \"{0}\" set for newsletters cannot be found.", LastModified = "2012/05/30", Value = "Notifications SMTP profile \"{0}\" set for newsletters cannot be found.")]
    public string NotificationsSmtpProfileNotFound => this[nameof (NotificationsSmtpProfileNotFound)];

    /// <summary>
    /// phrase: Notifications SMTP profile \"{0}\" is not an SMTP profile.
    /// </summary>
    [ResourceEntry("NotificationsSmtpProfileIsNotSmtp", Description = "phrase: Notifications SMTP profile \"{0}\" is not an SMTP profile.", LastModified = "2012/05/30", Value = "Notifications SMTP profile \"{0}\" is not an SMTP profile.")]
    public string NotificationsSmtpProfileIsNotSmtp => this[nameof (NotificationsSmtpProfileIsNotSmtp)];

    /// <summary>
    /// phrase: SmtpHost property set through configuration of Email Campaigns module cannot be an empty string.
    /// </summary>
    [ResourceEntry("SmtpHostCannotBeEmpty", Description = "phrase: SmtpHost property set through configuration of Email Campaigns module cannot be an empty string.", LastModified = "2012/01/02", Value = "SmtpHost property set through configuration of Email Campaigns module cannot be an empty string.")]
    public string SmtpHostCannotBeEmpty => this[nameof (SmtpHostCannotBeEmpty)];

    /// <summary>
    /// phrase: SmtpPort property set through configuration of Email Campaigns module must be an integer between 0 and 65535.
    /// </summary>
    [ResourceEntry("SmtpPortMustBeInRange", Description = "phrase: SmtpPort property set through configuration of Email Campaigns module must be an integer between 0 and 65535.", LastModified = "2012/01/02", Value = "SmtpPort property set through configuration of Email Campaigns module must be an integer between 0 and 65535.")]
    public string SmtpPortMustBeInRange => this[nameof (SmtpPortMustBeInRange)];

    /// <summary>
    /// phrase: When SMTP authentication is turned on, you must provide value for the SmtpUserName property.
    /// </summary>
    [ResourceEntry("ProvideSmtpUserName", Description = "phrase: When SMTP authentication is turned on, you must provide value for the SmtpUserName property.", LastModified = "2012/01/02", Value = "When SMTP authentication is turned on, you must provide value for the SmtpUserName property.")]
    public string ProvideSmtpUserName => this[nameof (ProvideSmtpUserName)];

    /// <summary>
    /// phrase: When SMTP authentication is turned on, you must provide value for the SmtpPassword property.
    /// </summary>
    [ResourceEntry("ProvideSmtpPassword", Description = "phrase: When SMTP authentication is turned on, you must provide value for the ProvidSmtpPassword property.", LastModified = "2012/01/02", Value = "When SMTP authentication is turned on, you must provide value for the ProvidSmtpPassword property.")]
    public string ProvideSmtpPassword => this[nameof (ProvideSmtpPassword)];

    /// <summary>phrase: Missing mailing list.</summary>
    [ResourceEntry("MissingMailingList", Description = "phrase: Missing mailing list.", LastModified = "2012/01/02", Value = "Missing mailing list.")]
    public string MissingMailingList => this[nameof (MissingMailingList)];

    [ResourceEntry("EditTitleAndProperties", Description = "phrase: Title & Properties", LastModified = "2012/07/05", Value = "Title & Properties")]
    public string EditTitleAndProperties => this[nameof (EditTitleAndProperties)];

    /// <summary>phrase: Mailing list settings</summary>
    [ResourceEntry("MailingListSettings", Description = "phrase: Mail settings", LastModified = "2012/05/09", Value = "Mail settings")]
    public string MailingListSettings => this[nameof (MailingListSettings)];

    /// <summary>phrase: Back to content</summary>
    [ResourceEntry("BackToContent", Description = "phrase: Back to content", LastModified = "2012/03/23", Value = "Back to content")]
    public string BackToContent => this[nameof (BackToContent)];

    /// <summary>phrase: Subscriber {0} is already a member of {1}.",</summary>
    [ResourceEntry("SubscriberIsAlreadyMemberInList", Description = "phrase: Subscriber {0} is already a member of {1}.", LastModified = "2012/03/15", Value = "Subscriber \"{0}\" is already a member of \"{1}\".")]
    public string SubscriberIsAlreadyMemberInList => this[nameof (SubscriberIsAlreadyMemberInList)];

    /// <summary>phrase: Loading</summary>
    [ResourceEntry("Loading", Description = "phrase: Loading", LastModified = "2012/03/28", Value = "Loading...")]
    public string Loading => this[nameof (Loading)];

    /// <summary>
    /// phrase: You should select a connection source to be able to continue
    /// </summary>
    [ResourceEntry("SelectConnectionSourceErrorMessage", Description = "phrase: You should select a connection source to be able to continue", LastModified = "2012/04/04", Value = "You should select a connection source to be able to continue")]
    public string SelectConnectionSourceErrorMessage => this[nameof (SelectConnectionSourceErrorMessage)];

    /// <summary>phrase: Default mail settings</summary>
    [ResourceEntry("DefaultMailSettings", Description = "phrase: Default mail settings", LastModified = "2012/05/02", Value = "Default mail settings")]
    public string DefaultMailSettings => this[nameof (DefaultMailSettings)];

    /// <summary>word: Tracking</summary>
    [ResourceEntry("Tracking", Description = "word: Tracking", LastModified = "2012/05/02", Value = "Tracking")]
    public string Tracking => this[nameof (Tracking)];

    /// <summary>phrase: Create and go to add the first issue</summary>
    [ResourceEntry("CreateAndGoAddFirstIssue", Description = "phrase: Create and go to add the first issue", LastModified = "2010/05/02", Value = "Create and go to add the first issue")]
    public string CreateAndGoAddFirstIssue => this[nameof (CreateAndGoAddFirstIssue)];

    /// <summary>Status of the campaign when it has 1 draft issue.</summary>
    [ResourceEntry("CampaignIssueStateOneDraft", Description = "Status of the campaign when it has 1 draft issue.", LastModified = "2012/05/04", Value = "1 issue draft")]
    public string CampaignIssueStateOneDraft => this[nameof (CampaignIssueStateOneDraft)];

    /// <summary>Status of the campaign when it has 1 sent issue.</summary>
    [ResourceEntry("CampaignIssueOneSent", Description = "Status of the campaign when it has 1 draft issue and 1 sent issue", LastModified = "2012/05/04", Value = "1 issue sent")]
    public string CampaignIssueOneSent => this[nameof (CampaignIssueOneSent)];

    /// <summary>
    /// Status of the campaign when it has multiple sent issues.
    /// </summary>
    [ResourceEntry("CampaignIssueMultipleSent", Description = "Status of the campaign when it has 1 draft issue and multiple sent issues", LastModified = "2012/05/04", Value = "{0} issues sent")]
    public string CampaignIssueMultipleSent => this[nameof (CampaignIssueMultipleSent)];

    /// <summary>Status of the campaign when it has 1 scheduled issue.</summary>
    [ResourceEntry("CampaignIssueStateOneScheduled", Description = "Status of the campaign when it has 1 scheduled issue.", LastModified = "2012/05/04", Value = "1 issue scheduled")]
    public string CampaignIssueStateOneScheduled => this[nameof (CampaignIssueStateOneScheduled)];

    /// <summary>
    /// Status of the campaign when it has 1 scheduled A/B test.
    /// </summary>
    [ResourceEntry("OneScheduledAbTest", Description = "Status of the campaign when it has 1 scheduled A/B test.", LastModified = "2012/06/25", Value = "1 A/B test scheduled")]
    public string OneScheduledAbTest => this[nameof (OneScheduledAbTest)];

    /// <summary>Status of the campaign when it has 1 draft A/B test.</summary>
    [ResourceEntry("OneDraftAbTest", Description = "Status of the campaign when it has 1 draft A/B test.", LastModified = "2012/06/25", Value = "1 draft A/B test")]
    public string OneDraftAbTest => this[nameof (OneDraftAbTest)];

    /// <summary>phrase: Create an issue for {0}</summary>
    [ResourceEntry("CreateIssueFor", Description = "phrase: Create an issue for {0}", LastModified = "2010/05/02", Value = "Create an issue for {0}")]
    public string CreateIssueFor => this[nameof (CreateIssueFor)];

    /// <summary>phrase: Issue name</summary>
    [ResourceEntry("IssueName", Description = "phrase: Issue name", LastModified = "2010/05/02", Value = "Issue name")]
    public string IssueName => this[nameof (IssueName)];

    /// <summary>phrase: Example: December issue</summary>
    [ResourceEntry("IssueNameDescription", Description = "phrase: Example: December issue", LastModified = "2012/05/02", Value = "Example: <em class='sfNote'>December issue</em>")]
    public string IssueNameDescription => this[nameof (IssueNameDescription)];

    /// <summary>phrase: Create new issue</summary>
    [ResourceEntry("CreateNewIssue", Description = "phrase: Create new issue", LastModified = "2012/05/07", Value = "Create new issue")]
    public string CreateNewIssue => this[nameof (CreateNewIssue)];

    /// <summary>phrase: Edit issue</summary>
    [ResourceEntry("EditIssue", Description = "phrase: Edit issue", LastModified = "2012/05/07", Value = "Edit issue")]
    public string EditIssue => this[nameof (EditIssue)];

    /// <summary>phrase: Campaign Overview Report</summary>
    [ResourceEntry("CampaignOverviewReportTitle", Description = "phrase: Campaign Overview Report", LastModified = "2012/05/08", Value = "Campaign Overview Report")]
    public string CampaignOverviewReportTitle => this[nameof (CampaignOverviewReportTitle)];

    /// <summary>phrase: Clicked</summary>
    [ResourceEntry("Clicked", Description = "phrase: Clicked", LastModified = "2012/05/08", Value = "Clicked")]
    public string Clicked => this[nameof (Clicked)];

    /// <summary>phrase: Sent to</summary>
    [ResourceEntry("SentTo", Description = "phrase: Sent to", LastModified = "2012/05/09", Value = "Sent to")]
    public string SentTo => this[nameof (SentTo)];

    /// <summary>phrase: Date sent</summary>
    [ResourceEntry("DateSent", Description = "phrase: Date sent", LastModified = "2012/05/09", Value = "Date sent")]
    public string DateSent => this[nameof (DateSent)];

    /// <summary>phrase: Date ended</summary>
    [ResourceEntry("DateEnded", Description = "phrase: Date ended", LastModified = "2012/06/14", Value = "Date ended")]
    public string DateEnded => this[nameof (DateEnded)];

    /// <summary>
    /// phrase: Edit content and properties to send a new issue of this campaign.
    /// </summary>
    [ResourceEntry("CreateNewIssueDescription", Description = "phrase: Edit content and properties to send a new issue of this campaign.", LastModified = "2012/05/10", Value = "Edit content and properties to send a new issue of this campaign")]
    public string CreateNewIssueDescription => this[nameof (CreateNewIssueDescription)];

    /// <summary>phrase: Edit and send...</summary>
    [ResourceEntry("EditAndSend", Description = "phrase: Edit and send...", LastModified = "2012/05/10", Value = "Edit and send...")]
    public string EditAndSend => this[nameof (EditAndSend)];

    /// <summary>phrase: All Campaigns</summary>
    [ResourceEntry("AllCampaigns", Description = "phrase: All Campaigns", LastModified = "2012/05/11", Value = "All Campaigns")]
    public string AllCampaigns => this[nameof (AllCampaigns)];

    /// <summary>phrase: Back to {0}</summary>
    [ResourceEntry("BackToFormat", Description = "phrase: Back to {0}", LastModified = "2012/05/14", Value = "Back to {0}")]
    public string BackToFormat => this["BackToFromat"];

    /// <summary>phrase: Create and go to {0}</summary>
    [ResourceEntry("CreateAndGoToFormat", Description = "phrase: Create and go to {0}", LastModified = "2012/05/14", Value = "Create and go to {0}")]
    public string CreateAndGoToFormat => this[nameof (CreateAndGoToFormat)];

    /// <summary>phrase: Statistics for {0}</summary>
    [ResourceEntry("StatisticsFor", Description = "phrase: Statistics for {0}", LastModified = "2012/05/14", Value = "Statistics for {0}")]
    public string StatisticsFor => this[nameof (StatisticsFor)];

    /// <summary>
    /// text: {0} emails sent to subscribers in {1} mailing list.
    /// </summary>
    [ResourceEntry("MailingListStatistics", Description = "text: {0} emails sent to subscribers in {1} mailing list.", LastModified = "2012/05/14", Value = "<b>{0}</b> emails sent to subscribers in <i>{1}</i> mailing list.")]
    public string MailingListStatistics => this[nameof (MailingListStatistics)];

    /// <summary>
    /// text: <b>{0}</b> ({1}%) of them are not delivered (soft bounced) <b>{2}</b> ({3}%) are not delivered (hard bounced).
    /// </summary>
    [ResourceEntry("BounceStatistics", Description = "text: <b>{0}</b> ({1}%) of them are not delivered (soft bounced) <b>{2}</b> ({3}%) are not delivered (hard bounced).", LastModified = "2015/06/24", Value = "<b>{0}</b> ({1}%) of them are not delivered (soft bounced) <b>{2}</b> ({3}%) are not delivered (hard bounced).")]
    public string BounceStatistics => this[nameof (BounceStatistics)];

    /// <summary>phrase: Bounced email settings</summary>
    [ResourceEntry("BouncedEmailSettings", Description = "phrase: Bounced email settings", LastModified = "2012/05/14", Value = "Bounced email settings")]
    public string BouncedEmailSettings => this[nameof (BouncedEmailSettings)];

    /// <summary>
    /// text: To improve this metric consider keeping your mailing list up to date.
    /// </summary>
    [ResourceEntry("HowToImproveDelivered", Description = "text: To improve this metric consider keeping your mailing list up to date.", LastModified = "2012/05/14", Value = "To improve this metric consider keeping your mailing list up to date.")]
    public string HowToImproveDelivered => this[nameof (HowToImproveDelivered)];

    /// <summary>
    /// text: To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sending email.
    /// </summary>
    [ResourceEntry("HowToImproveUniqueOpenings", Description = "text: To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sending email.", LastModified = "2012/05/14", Value = "To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sending email.")]
    public string HowToImproveUniqueOpenings => this[nameof (HowToImproveUniqueOpenings)];

    /// <summary>
    /// text: To improve this metric consider creating an A/B test that tests the relevance of your information. Make sure there are enough actionable links for your audience.
    /// </summary>
    [ResourceEntry("HowToImproveUniqueClicks", Description = "text: To improve this metric consider creating an A/B test that tests the relevance of your information. Make sure there are enough actionable links for your audience.", LastModified = "2012/05/14", Value = "To improve this metric consider creating an A/B test that tests the relevance of your information. Make sure there are enough actionable links for your audience.")]
    public string HowToImproveUniqueClicks => this[nameof (HowToImproveUniqueClicks)];

    /// <summary>
    /// text: To minimize this metric consider segmenting your customers and sending more relevant information to your audience.
    /// </summary>
    [ResourceEntry("HowToImproveUnsubscribed", Description = "text: To minimize this metric consider segmenting your customers and sending more relevant information to your audience.", LastModified = "2012/05/14", Value = "To minimize this metric consider segmenting your customers and sending more relevant information to your audience.")]
    public string HowToImproveUnsubscribed => this[nameof (HowToImproveUnsubscribed)];

    /// <summary>
    /// text: To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sender email.
    /// </summary>
    [ResourceEntry("HowToImproveOpenedInFirstHours", Description = "text: To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sender email.", LastModified = "2012/05/14", Value = "To improve this metric consider creating an A/B test that tests separately the timing of your campaign, its subject or sender email.")]
    public string HowToImproveOpenedInFirstHours => this[nameof (HowToImproveOpenedInFirstHours)];

    /// <summary>phrase: How to improve?</summary>
    [ResourceEntry("HowToImprove", Description = "phrase: How to improve?", LastModified = "2012/05/15", Value = "How to improve?")]
    public string HowToImprove => this[nameof (HowToImprove)];

    /// <summary>
    /// phrase: {0}% delivery rate in comparison to {1}% previous issue
    /// </summary>
    [ResourceEntry("DeliveryStatisticsComparison", Description = "phrase: {0}% delivery rate in comparison to {1}% previous issue", LastModified = "2012/06/04", Value = "{0}% delivery rate in comparison to {1}% previous issue")]
    public string DeliveryStatisticsComparison => this[nameof (DeliveryStatisticsComparison)];

    /// <summary>phrase: {0}% delivery rate</summary>
    [ResourceEntry("DeliveryStatistics", Description = "phrase: {0}% delivery rate", LastModified = "2012/06/04", Value = "{0}% delivery rate")]
    public string DeliveryStatistics => this[nameof (DeliveryStatistics)];

    /// <summary>phrase: Unique openings</summary>
    [ResourceEntry("UniqueOpenings", Description = "phrase: Unique openings", LastModified = "2012/05/15", Value = "Unique openings")]
    public string UniqueOpenings => this[nameof (UniqueOpenings)];

    /// <summary>
    /// phrase: {0}% opening rate in comparison to {1}% previous issue
    /// </summary>
    [ResourceEntry("UniqueOpeningsStatisticsComparison", Description = "phrase: {0}% opening rate in comparison to {1}% previous issue", LastModified = "2012/06/04", Value = "{0}% opening rate in comparison to {1}% previous issue")]
    public string UniqueOpeningsStatisticsComparison => this[nameof (UniqueOpeningsStatisticsComparison)];

    /// <summary>phrase: {0}% opening rate</summary>
    [ResourceEntry("UniqueOpeningsStatistics", Description = "phrase: {0}% opening rate", LastModified = "2012/06/04", Value = "{0}% opening rate")]
    public string UniqueOpeningsStatistics => this[nameof (UniqueOpeningsStatistics)];

    /// <summary>phrase: Unique clicks</summary>
    [ResourceEntry("UniqueClicks", Description = "phrase: Unique clicks", LastModified = "2012/05/15", Value = "Unique clicks")]
    public string UniqueClicks => this[nameof (UniqueClicks)];

    /// <summary>
    /// phrase: {0}% click through rate in comparison to {1}% previous issue
    /// </summary>
    [ResourceEntry("UniqueClicksStatisticsComparison", Description = "phrase: {0}% click through rate in comparison to {1}% previous issue", LastModified = "2012/06/04", Value = "{0}% click through rate in comparison to {1}% previous issue")]
    public string UniqueClicksStatisticsComparison => this[nameof (UniqueClicksStatisticsComparison)];

    /// <summary>phrase: {0}% click through rate</summary>
    [ResourceEntry("UniqueClicksStatistics", Description = "phrase: {0}% click through rate", LastModified = "2012/06/04", Value = "{0}% click through rate")]
    public string UniqueClicksStatistics => this[nameof (UniqueClicksStatistics)];

    /// <summary>
    /// phrase: {0}% unsubscribe in comparison to {1}% previous issue
    /// </summary>
    [ResourceEntry("UnsubscribeStatisticsComparison", Description = "phrase: {0}% unsubscribe in comparison to {1}% previous issue", LastModified = "2012/06/04", Value = "{0}% unsubscribe in comparison to {1}% previous issue")]
    public string UnsubscribeStatisticsComparison => this[nameof (UnsubscribeStatisticsComparison)];

    /// <summary>phrase: {0}% unsubscribe</summary>
    [ResourceEntry("UnsubscribeStatistics", Description = "phrase: {0}% unsubscribe", LastModified = "2012/06/04", Value = "{0}% unsubscribe")]
    public string UnsubscribeStatistics => this[nameof (UnsubscribeStatistics)];

    /// <summary>phrase: Opened in the first 48 hrs</summary>
    [ResourceEntry("OpenedInFirstHours", Description = "phrase: Opened in the first 48 hrs", LastModified = "2012/05/15", Value = "Opened in the first <b>48</b> hrs")]
    public string OpenedInFirstHours => this[nameof (OpenedInFirstHours)];

    /// <summary>
    /// phrase: {0}% open rate in comparison to {1}% previous issue
    /// </summary>
    [ResourceEntry("OpenedInFirstHoursStatisticsComparison", Description = "phrase: {0}% open rate in comparison to {1}% previous issue", LastModified = "2012/06/04", Value = "{0}% open rate in comparison to {1}% previous issue")]
    public string OpenedInFirstHoursStatisticsComparison => this[nameof (OpenedInFirstHoursStatisticsComparison)];

    /// <summary>phrase: {0}% open rate</summary>
    [ResourceEntry("OpenedInFirstHoursStatistics", Description = "phrase: {0}% open rate", LastModified = "2012/06/04", Value = "{0}% open rate")]
    public string OpenedInFirstHoursStatistics => this[nameof (OpenedInFirstHoursStatistics)];

    /// <summary>
    /// Title of the subscribers report page of the Email Campaigns module
    /// </summary>
    [ResourceEntry("SubscribersReportTitle", Description = "Title of the subscribers report page of the Email Campaigns module", LastModified = "2012/05/14", Value = "Subscribers report")]
    public string SubscribersReportTitle => this[nameof (SubscribersReportTitle)];

    /// <summary>
    /// Url name of the subscribers report page of the Email Campaigns module
    /// </summary>
    [ResourceEntry("SubscribersReportUrlName", Description = "Url name of the subscribers report page of the Email Campaigns module", LastModified = "2012/05/14", Value = "subscribers-report")]
    public string SubscribersReportUrlName => this[nameof (SubscribersReportUrlName)];

    /// <summary>
    /// Page that displays the report for all subscribers per issue
    /// </summary>
    [ResourceEntry("SubscribersReportDescription", Description = "Page that displays the report for all subscribers per issue", LastModified = "2012/05/14", Value = "Page that displays the report for all subscribers per issue")]
    public string SubscribersReportDescription => this[nameof (SubscribersReportDescription)];

    /// <summary>
    /// Html title of the subscribers report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("SubscribersReportHtmlTitle", Description = "Html title of the subscribers report page of the Email Campaigns module.", LastModified = "2011/05/14", Value = "Subscribers report")]
    public string SubscribersReportHtmlTitle => this[nameof (SubscribersReportHtmlTitle)];

    /// <summary>phrase: Subscribers activity for {0}</summary>
    [ResourceEntry("SubscribersActivityFor", Description = "phrase: Subscribers activity for {0}", LastModified = "2011/05/14", Value = "Subscribers activity for {0}")]
    public string SubscribersActivityFor => this[nameof (SubscribersActivityFor)];

    /// <summary>phrase: Delivery state</summary>
    [ResourceEntry("DeliveryState", Description = "phrase: Delivery state", LastModified = "2011/05/15", Value = "Delivery state")]
    public string DeliveryState => this[nameof (DeliveryState)];

    /// <summary>phrase: Date opened</summary>
    [ResourceEntry("DateOpened", Description = "phrase: Date opened", LastModified = "2011/05/15", Value = "Date opened")]
    public string DateOpened => this[nameof (DateOpened)];

    /// <summary>phrase: Clicked by</summary>
    [ResourceEntry("ClickedBy", Description = "phrase: Clicked by", LastModified = "2012/05/16", Value = "Clicked by")]
    public string ClickedBy => this[nameof (ClickedBy)];

    /// <summary>phrase: View subscribers</summary>
    [ResourceEntry("ViewSubscribers", Description = "phrase: View subscribers", LastModified = "2012/05/16", Value = "View subscribers")]
    public string ViewSubscribers => this[nameof (ViewSubscribers)];

    /// <summary>phrase: Last 10 subscribers who ckicked</summary>
    [ResourceEntry("LastSubscribersWhoClickedTitle", Description = "phrase: Last 10 subscribers who clicked", LastModified = "2012/05/17", Value = "Last 10 subscribers who clicked")]
    public string LastSubscribersWhoClickedTitle => this[nameof (LastSubscribersWhoClickedTitle)];

    /// <summary>phrase: Clicked Url</summary>
    [ResourceEntry("ClickedUrl", Description = "phrase: Clicked Url", LastModified = "2012/05/17", Value = "Clicked Url")]
    public string ClickedUrl => this[nameof (ClickedUrl)];

    /// <summary>phrase: Date clicked</summary>
    [ResourceEntry("DateClicked", Description = "phrase: Date clicked", LastModified = "2012/05/17", Value = "Date clicked")]
    public string DateClicked => this[nameof (DateClicked)];

    /// <summary>phrase: View all subscribers</summary>
    [ResourceEntry("ViewAllSubscribers", Description = "phrase: View all subscribers", LastModified = "2012/05/17", Value = "View all subscribers")]
    public string ViewAllSubscribers => this[nameof (ViewAllSubscribers)];

    /// <summary>phrase: Clicks by hour</summary>
    [ResourceEntry("ClicksByHour", Description = "phrase: Clicks by hour", LastModified = "2011/05/16", Value = "Clicks by hour")]
    public string ClicksByHour => this[nameof (ClicksByHour)];

    /// <summary>word: clicks</summary>
    [ResourceEntry("Clicks", Description = "word: clicks", LastModified = "2011/05/17", Value = "clicks")]
    public string Clicks => this[nameof (Clicks)];

    /// <summary>phrase: Not delivered</summary>
    [ResourceEntry("NotDelivered", Description = "phrase: Not delivered", LastModified = "2011/05/17", Value = "Not delivered")]
    public string NotDelivered => this[nameof (NotDelivered)];

    /// <summary>phrase: Invalid email (not delivered)</summary>
    [ResourceEntry("EmailAddressDoesNotExist", Description = "phrase: Invalid email (not delivered)", LastModified = "2011/05/17", Value = "Invalid email (not delivered)")]
    public string EmailAddressDoesNotExist => this[nameof (EmailAddressDoesNotExist)];

    /// <summary>phrase: Full mailbox (not delivered)</summary>
    [ResourceEntry("MailboxIsFull", Description = "phrase: Full mailbox (not delivered)", LastModified = "2011/05/17", Value = "Full mailbox (not delivered)")]
    public string MailboxIsFull => this[nameof (MailboxIsFull)];

    /// <summary>phrase: Unsubscribe page</summary>
    [ResourceEntry("UnsubscribePage", Description = "phrase: Unsubscribe page", LastModified = "2012/05/23", Value = "Unsubscribe page")]
    public string UnsubscribePage => this[nameof (UnsubscribePage)];

    /// <summary>
    /// phrase: There must be an Unsubscribe widget on this page
    /// </summary>
    [ResourceEntry("UnsubscribePageDescription", Description = "phrase: There must be an Unsubscribe widget on this page", LastModified = "2012/05/23", Value = "There must be an Unsubscribe widget on this page")]
    public string UnsubscribePageDescription => this[nameof (UnsubscribePageDescription)];

    /// <summary>phrase: Filter subscribers</summary>
    [ResourceEntry("FilterSubscribers", Description = "phrase: Filter subscribers", LastModified = "2012/05/30", Value = "Filter subscribers")]
    public string FilterSubscribers => this[nameof (FilterSubscribers)];

    /// <summary>phrase: All Subscribers</summary>
    [ResourceEntry("AllSubscribers", Description = "phrase: All subscribers", LastModified = "2012/05/30", Value = "All subscribers")]
    public string AllSubscribers => this[nameof (AllSubscribers)];

    /// <summary>phrase: by Clicked links</summary>
    [ResourceEntry("ByClickedLinks", Description = "phrase: by Clicked links", LastModified = "2012/05/30", Value = "by Clicked links...")]
    public string ByClickedLinks => this[nameof (ByClickedLinks)];

    /// <summary>phrase: unsubscribe</summary>
    [ResourceEntry("UnsubscribeLink", Description = "Label of the unsubscribe link.", LastModified = "2012/10/08", Value = "unsubscribe")]
    public string UnsubscribeLink => this[nameof (UnsubscribeLink)];

    /// <summary>phrase: subscribe</summary>
    [ResourceEntry("SubscribeLink", Description = "subscribe", LastModified = "2012/06/05", Value = "subscribe")]
    public string SubscribeLink => this[nameof (SubscribeLink)];

    /// <summary>phrase: Unsubscribe message</summary>
    [ResourceEntry("UnsubscribeMessage", Description = "Unsubscribe message", LastModified = "2012/05/31", Value = "Unsubscribe message")]
    public string UnsubscribeMessage => this[nameof (UnsubscribeMessage)];

    /// <summary>phrase: Subscribers by Clicked link</summary>
    [ResourceEntry("SubscribersByClickedLink", Description = "phrase: Subscribers by Clicked link", LastModified = "2012/05/31", Value = "Subscribers by Clicked link")]
    public string SubscribersByClickedLink => this[nameof (SubscribersByClickedLink)];

    /// <summary>Explanation text for the A/B tests.</summary>
    [ResourceEntry("AbTestExplanation", Description = "Explanation text for the A/B tests.", LastModified = "2012/06/08", Value = "<p>Performing an A/B test will help you find out which of the versions of an email is more effective.</p><p>The main idea is that you prepare two versions of one email &mdash; version A is sent to a small portion of the subscribers in one mailing list and version B is sent to another small group of them.</p><p>After the email is sent the results of both emails are measured and based on them the winning email is chosen and sent to the rest of the subscribers.</p>")]
    public string AbTestExplanation => this[nameof (AbTestExplanation)];

    /// <summary>phrase: Subscribe Successful</summary>
    [ResourceEntry("SubscribeSuccessful", Description = "phrase: Subscribe Successful", LastModified = "2012/06/05", Value = "Subscribe Successful")]
    public string SubscribeSuccessful => this[nameof (SubscribeSuccessful)];

    /// <summary>phrase: What's this?</summary>
    [ResourceEntry("WhatsThis", Description = "phrase: What's this?", LastModified = "2012/06/06", Value = "What's this?")]
    public string WhatsThis => this[nameof (WhatsThis)];

    /// <summary>
    /// text: The reasons for an email to bounce might be (but not limited to): <br /><ul><li>Full user mailbox</li><li>Too large message</li><li>Message is considered as a SPAM</li><li>Invalid email address</li></ul>
    /// </summary>
    [ResourceEntry("WhatsVariousBouncesHint", Description = "text: The reasons for an email to bounce might be (but not limited to): <br /><ul><li>Full user mailbox</li><li>Too large message</li><li>Message is considered as a SPAM</li><li>Invalid email address</li></ul>", LastModified = "2012/06/06", Value = "The reasons for an email to bounce might be (but not limited to): <br /><ul><li>Full user mailbox</li><li>Too large message</li><li>Message is considered as a SPAM</li><li>Invalid email address</li></ul>")]
    public string WhatsVariousBouncesHint => this[nameof (WhatsVariousBouncesHint)];

    /// <summary>phrase: Copy content from {0}</summary>
    [ResourceEntry("CopyContentFromFormat", Description = "phrase: Copy content from {0}", LastModified = "2012/06/08", Value = "Copy content from {0}")]
    public string CopyContentFromFormat => this[nameof (CopyContentFromFormat)];

    /// <summary>phrase: What is an A/B test?</summary>
    [ResourceEntry("WhatsAbTest", Description = "phrase: What is an A/B test?", LastModified = "2012/06/08", Value = "What is an A/B test?")]
    public string WhatsAbTest => this[nameof (WhatsAbTest)];

    /// <summary>phrase: A/B Test Name</summary>
    [ResourceEntry("ABTestNameColumn", Description = "phrase: A/B Test Name", LastModified = "2012/06/11", Value = "A/B Test Name")]
    public string ABTestNameColumn => this[nameof (ABTestNameColumn)];

    /// <summary>phrase: sample users</summary>
    [ResourceEntry("SampleUsers", Description = "phrase: sample users", LastModified = "2012/06/11", Value = "Sample users")]
    public string SampleUsers => this[nameof (SampleUsers)];

    /// <summary>phrase: winner</summary>
    [ResourceEntry("Winner", Description = "phrase: Winner", LastModified = "2012/06/11", Value = "Winner")]
    public string Winner => this[nameof (Winner)];

    /// <summary>phrase: winner issue</summary>
    [ResourceEntry("WinnerIssue", Description = "phrase: Winner issue", LastModified = "2012/06/20", Value = "Winner issue")]
    public string WinnerIssue => this[nameof (WinnerIssue)];

    /// <summary>phrase: Send an A/B test</summary>
    [ResourceEntry("SendABTest", Description = "phrase: Send an A/B test", LastModified = "2012/06/12", Value = "Send an A/B test")]
    public string SendABTest => this[nameof (SendABTest)];

    /// <summary>phrase: Issues that will be sent for an A/B test</summary>
    [ResourceEntry("IssuesThatWillBeSentForABTest", Description = "phrase: Issues that will be sent for an A/B test", LastModified = "2012/06/12", Value = "Issues that will be sent for an A/B test")]
    public string IssuesThatWillBeSentForABTest => this[nameof (IssuesThatWillBeSentForABTest)];

    /// <summary>phrase: A/B Test settings</summary>
    [ResourceEntry("ABTestSettings", Description = "phrase: A/B Test settings", LastModified = "2012/06/12", Value = "A/B Test settings")]
    public string ABTestSettings => this[nameof (ABTestSettings)];

    /// <summary>phrase: Edit this issue</summary>
    [ResourceEntry("EditThisIssue", Description = "phrase: Edit this issue", LastModified = "2012/06/12", Value = "Edit this issue")]
    public string EditThisIssue => this[nameof (EditThisIssue)];

    /// <summary>phrase: Test name</summary>
    [ResourceEntry("TestName", Description = "phrase: Test name", LastModified = "2012/06/12", Value = "Test name")]
    public string TestName => this[nameof (TestName)];

    /// <summary>
    /// phrase: For your convenience only. Used to help you easily find and recognize a particular test
    /// </summary>
    [ResourceEntry("TestNameDescription", Description = "phrase: For your convenience only. Used to help you easily find and recognize a particular test ", LastModified = "2012/06/12", Value = "For your convenience only. Used to help you easily find and recognize a particular test")]
    public string TestNameDescription => this[nameof (TestNameDescription)];

    /// <summary>phrase: What are you testing?</summary>
    [ResourceEntry("TestingNoteTitle", Description = "phrase: What are you testing?", LastModified = "2012/06/12", Value = "What are you testing?")]
    public string TestingNoteTitle => this[nameof (TestingNoteTitle)];

    /// <summary>
    /// phrase: Add a note what exactly you want to test and what you expect to learn from this test. This note will be added to this A/B test detailed info
    /// </summary>
    [ResourceEntry("TestingNoteDescription", Description = "phrase: Add a note what exactly you want to test and what you expect to learn from this test. This note will be added to this A/B test detailed info", LastModified = "2012/06/12", Value = "Add a note what exactly you want to test and what you expect to learn from this test. This note will be added to this A/B test detailed info")]
    public string TestingNoteDescription => this[nameof (TestingNoteDescription)];

    /// <summary>phrase: Testing sample</summary>
    [ResourceEntry("TestingSample", Description = "phrase: Testing sample", LastModified = "2012/06/12", Value = "Testing sample")]
    public string TestingSample => this[nameof (TestingSample)];

    /// <summary>
    /// phrase: Use the slider to define what part of the users in All_Clients mailing list will participate in the A/B test. After the test is finished the winning issue will be sent to the rest of the users in the mailing list
    /// </summary>
    [ResourceEntry("TestingSampleDescription", Description = "phrase: Use the slider to define what part of the users in All_Clients mailing list will participate in the A/B test. After the test is finished the winning issue will be sent to the rest of the users in the mailing list", LastModified = "2012/06/12", Value = "Use the slider to define what part of the users in {0} mailing list will participate in the A/B test. After the test is finished the winning issue will be sent to the rest of the users in the mailing list")]
    public string TestingSampleDescription => this[nameof (TestingSampleDescription)];

    /// <summary>phrase: Winning issue</summary>
    [ResourceEntry("WinningIssue", Description = "phrase: Winning issue", LastModified = "2012/06/12", Value = "Winning issue")]
    public string WinningIssue => this[nameof (WinningIssue)];

    /// <summary>
    /// phrase: Select the criteria by which the winning issue to be selected. The issue that meets selected criteria will be sent to the rest of the users.
    /// </summary>
    [ResourceEntry("WinningIssueDescription", Description = "phrase: Select the criteria by which the winning issue to be selected. The issue that meets selected criteria will be sent to the rest of the users.", LastModified = "2012/06/12", Value = "Select the criteria by which the winning issue to be selected. The issue that meets selected criteria will be sent to the rest of the users.")]
    public string WinningIssueDescription => this[nameof (WinningIssueDescription)];

    /// <summary>phrase: A/B test end</summary>
    [ResourceEntry("ABTestEnd", Description = "phrase: A/B test end", LastModified = "2012/06/12", Value = "A/B test end")]
    public string ABTestEnd => this[nameof (ABTestEnd)];

    /// <summary>
    /// phrase: Select when the A/B test should end and the winning issue to be sent to the rest of the users
    /// </summary>
    [ResourceEntry("ABTestEndDescription", Description = "phrase: Select when the A/B test should end and the winning issue to be sent to the rest of the users", LastModified = "2012/06/12", Value = "Select when the A/B test should end and the winning issue to be sent to the rest of the users")]
    public string ABTestEndDescription => this[nameof (ABTestEndDescription)];

    /// <summary>phrase: Send this A/B test...</summary>
    [ResourceEntry("SendThisABTestDotDotDot", Description = "phrase: Send this A/B test...", LastModified = "2012/06/12", Value = "Send this A/B test...")]
    public string SendThisABTestDotDotDot => this[nameof (SendThisABTestDotDotDot)];

    /// <summary>phrase: Send this A/B test</summary>
    [ResourceEntry("SendThisABTest", Description = "phrase: Send this A/B test", LastModified = "2012/06/12", Value = "Send this A/B test")]
    public string SendThisABTest => this[nameof (SendThisABTest)];

    /// <summary>phrase: Immediately</summary>
    [ResourceEntry("Immediately", Description = "phrase: Immediately", LastModified = "2012/06/12", Value = "Immediately")]
    public string Immediately => this[nameof (Immediately)];

    /// <summary>phrase: Schedule the test to be sent on...</summary>
    [ResourceEntry("ScheduleTheTestToBeSentOn", Description = "phrase: Schedule the test to be sent on...", LastModified = "2012/06/12", Value = "Schedule the test to be sent on...")]
    public string ScheduleTheTestToBeSentOn => this[nameof (ScheduleTheTestToBeSentOn)];

    /// <summary>phrase: {0} (A)</summary>
    [ResourceEntry("IssueATitle", Description = "phrase: {0} (A)", LastModified = "2012/06/12", Value = "{0} (A)")]
    public string IssueATitle => this[nameof (IssueATitle)];

    /// <summary>phrase: {0} (B)</summary>
    [ResourceEntry("IssueBTitle", Description = "phrase: {0} (B)", LastModified = "2012/06/12", Value = "{0} (B)")]
    public string IssueBTitle => this[nameof (IssueBTitle)];

    /// <summary>phrase: {0} ({1}% of all users)</summary>
    [ResourceEntry("TestingSamplePercentage", Description = "phrase: {0} ({1}% of all users)", LastModified = "2012/06/13", Value = "{0} ({1}% of all users)")]
    public string TestingSamplePercentage => this[nameof (TestingSamplePercentage)];

    /// <summary>phrase: What was tested</summary>
    [ResourceEntry("WhatWasTested", Description = "phrase: What was tested", LastModified = "2012/06/11", Value = "What was tested")]
    public string WhatWasTested => this[nameof (WhatWasTested)];

    /// <summary>phrase: What was tested?</summary>
    [ResourceEntry("WhatWasTestedTitle", Description = "phrase: What was tested?", LastModified = "2012/06/28", Value = "What was tested?")]
    public string WhatWasTestedTitle => this[nameof (WhatWasTestedTitle)];

    /// <summary>word: Conclusion</summary>
    [ResourceEntry("Conclusion", Description = "word: Conclusion", LastModified = "2012/06/28", Value = "Conclusion")]
    public string Conclusion => this[nameof (Conclusion)];

    /// <summary>phrase: Schedule this A/B test</summary>
    [ResourceEntry("ScheduleThisABTest", Description = "phrase: Schedule this A/B test", LastModified = "2012/06/18", Value = "Schedule this A/B test")]
    public string ScheduleThisABTest => this[nameof (ScheduleThisABTest)];

    /// <summary>phrase: (AB test)</summary>
    [ResourceEntry("ABTestNote", Description = "phrase: (AB test)", LastModified = "2012/06/21", Value = "(AB test)")]
    public string ABTestNote => this[nameof (ABTestNote)];

    /// <summary>
    /// phrase: The selected file cannot be imported because of its file format. Only files in .csv or .txt format can be imported
    /// </summary>
    [ResourceEntry("UnsupportedFileFormat", Description = "phrase: The selected file cannot be imported because of its file format. Only files in .csv or .txt format can be imported", LastModified = "2012/06/25", Value = "The selected file cannot be imported because of its file format. Only files in .csv or .txt format can be imported")]
    public string UnsupportedFileFormat => this[nameof (UnsupportedFileFormat)];

    /// <summary>phrase: Supported files: .csv, .txt</summary>
    [ResourceEntry("SupportedFileTypes", Description = "phrase: Supported files: .csv, .txt", LastModified = "2012/06/25", Value = "Supported files: .csv, .txt")]
    public string SupportedFileTypes => this[nameof (SupportedFileTypes)];

    /// <summary>
    /// Title of the A/B test report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("AbTestReportTitle", Description = "Title of the A/B test report page of the Email Campaigns module.", LastModified = "2012/06/28", Value = "A/B test report")]
    public string AbTestReportTitle => this[nameof (AbTestReportTitle)];

    /// <summary>
    /// Url name of the A/B test report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("AbTestReportUrlName", Description = "Url name of the A/B test report page of the Email Campaigns module.", LastModified = "2012/05/08", Value = "ab-test-report")]
    public string AbTestReportUrlName => this[nameof (AbTestReportUrlName)];

    /// <summary>
    /// Description of the A/B test report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("AbTestReportDescription", Description = "Description of the A/B test report page of the Email Campaigns module.", LastModified = "2012/06/28", Value = "This page displays statistics comparing two issues in an A/B Test.")]
    public string AbTestReportDescription => this[nameof (AbTestReportDescription)];

    /// <summary>
    /// Html title of the A/B test report page of the Email Campaigns module.
    /// </summary>
    [ResourceEntry("AbTestReportHtmlTitle", Description = "Html title of the A/B test report page of the Email Campaigns module.", LastModified = "2012/06/28", Value = "A/B test report")]
    public string AbTestReportHtmlTitle => this[nameof (AbTestReportHtmlTitle)];

    /// <summary>phrase: issue A.</summary>
    [ResourceEntry("IssueA", Description = "phrase: issue A", LastModified = "2012/06/28", Value = "issue A")]
    public string IssueA => this[nameof (IssueA)];

    /// <summary>phrase: issue B.</summary>
    [ResourceEntry("IssueB", Description = "phrase: issue B", LastModified = "2012/06/28", Value = "issue B")]
    public string IssueB => this[nameof (IssueB)];

    /// <summary>phrase: Test sent to {0} ({1}%) sample of the users.</summary>
    [ResourceEntry("TestSentToSampleOfUsers", Description = "phrase: Test sent to {0} ({1}%) sample of the users", LastModified = "2012/06/28", Value = "Test sent to <strong>{0}</strong> ({1}%) sample of the users")]
    public string TestSentToSampleOfUsers => this[nameof (TestSentToSampleOfUsers)];

    /// <summary>phrase: Winning criteria</summary>
    [ResourceEntry("WinningCriteria", Description = "phrase: Winning criteria", LastModified = "2012/06/28", Value = "Winning criteria")]
    public string WinningCriteria => this[nameof (WinningCriteria)];

    /// <summary>word: Analysis</summary>
    [ResourceEntry("Analysis", Description = "word: Analysis", LastModified = "2012/06/28", Value = "Analysis")]
    public string Analysis => this[nameof (Analysis)];

    /// <summary>
    /// text: Here you may describe the purpose of the performed A/B test
    /// </summary>
    [ResourceEntry("WhatWasTestedDescription", Description = "text: Here you may describe the purpose of the performed A/B test", LastModified = "2012/06/28", Value = "Here you may describe the purpose of the performed A/B test")]
    public string WhatWasTestedDescription => this[nameof (WhatWasTestedDescription)];

    /// <summary>
    /// text: Here you may put your findings from the performed A/B test.
    /// </summary>
    [ResourceEntry("ConclusionDescription", Description = "text: Here you may put your findings from the performed A/B test.", LastModified = "2012/06/28", Value = "Here you may put your findings from the performed A/B test.")]
    public string ConclusionDescription => this[nameof (ConclusionDescription)];

    /// <summary>phrase: This test will be sent to {0} users</summary>
    [ResourceEntry("SendAbTestPromptText", Description = "phrase: This test will be sent to {0} users", LastModified = "2012/06/29", Value = "This test will be sent to {0} users")]
    public string SendAbTestPromptText => this[nameof (SendAbTestPromptText)];

    /// <summary>
    /// phrase: This issue is already sent and cannot be edited. Please, refresh the page.
    /// </summary>
    [ResourceEntry("IssueAlreadySent", Description = "phrase: This issue is already sent and cannot be edited. Please, refresh the page.", LastModified = "2012/06/29", Value = "This issue is already sent and cannot be edited. Please, refresh the page.")]
    public string IssueAlreadySent => this[nameof (IssueAlreadySent)];

    /// <summary>
    /// phrase: This test is already started and cannot be edited. Please, refresh the page.
    /// </summary>
    [ResourceEntry("TestAlreadyStarted", Description = "phrase: This test is already started and cannot be edited. Please, refresh the page.", LastModified = "2012/06/29", Value = "This test is already started and cannot be edited. Please, refresh the page.")]
    public string TestAlreadyStarted => this[nameof (TestAlreadyStarted)];

    /// <summary>word: Optional</summary>
    [ResourceEntry("Optional", Description = "word: Optional", LastModified = "2012/07/05", Value = "Optional")]
    public string Optional => this[nameof (Optional)];

    /// <summary>word: Export subscribers</summary>
    [ResourceEntry("ExportSubscribers", Description = "word: Export subscribers", LastModified = "2012/08/22", Value = "Export subscribers")]
    public string ExportSubscribers => this[nameof (ExportSubscribers)];

    /// <summary>word: File format</summary>
    [ResourceEntry("FileFormat", Description = "word: File format", LastModified = "2012/08/22", Value = "File format")]
    public string FileFormat => this[nameof (FileFormat)];

    [ResourceEntry("CsvFile", Description = "word: Csv file", LastModified = "2012/08/22", Value = ".csv file")]
    public string CsvFile => this[nameof (CsvFile)];

    [ResourceEntry("TxtFile", Description = "word: Txt file", LastModified = "2012/08/22", Value = ".txt file")]
    public string TxtFile => this[nameof (TxtFile)];

    /// <summary>phrase: Do not export subscribers with same emails</summary>
    [ResourceEntry("DoNotExportExistingSubscribers", Description = "phrase: Do not export subscribers with same emails", LastModified = "2012/08/22", Value = "Do not export subscribers with same emails")]
    public string DoNotExportExistingSubscribers => this[nameof (DoNotExportExistingSubscribers)];

    /// <summary>phrase: Export</summary>
    [ResourceEntry("Export", Description = "phrase: Export", LastModified = "2012/08/22", Value = "Export")]
    public string Export => this[nameof (Export)];

    /// <summary>phrase: Subscribers from selected mailing lists...</summary>
    [ResourceEntry("SubsFromSelectedLists", Description = "phrase: Subscribers from selected mailing lists...", LastModified = "2012/08/22", Value = "Subscribers from selected mailing lists...")]
    public string SubsFromSelectedLists => this[nameof (SubsFromSelectedLists)];

    /// <summary>phrase: Subscribers who do not belong to any list</summary>
    [ResourceEntry("SubsNoMailingList", Description = "phrase: Subscribers who do not belong to any list", LastModified = "2012/09/04", Value = "Subscribers who do not belong to any list")]
    public string SubsNoMailingList => this[nameof (SubsNoMailingList)];

    /// <summary>phrase: File cannot be exported.</summary>
    [ResourceEntry("FileCannotBeExported", Description = "phrase:File cannot be exported.", LastModified = "2012/09/04", Value = "File cannot be exported.")]
    public string FileCannotBeExported => this[nameof (FileCannotBeExported)];

    /// <summary>phrase: Export subscribers from {0}</summary>
    [ResourceEntry("ExportSubscribersFrom", Description = "phrase: Export subscribers from {0}", LastModified = "2012/08/27", Value = "Export subscribers from {0}")]
    public string ExportSubscribersFrom => this[nameof (ExportSubscribersFrom)];

    /// <summary>phrase: There are no subscribers.</summary>
    [ResourceEntry("NoSubscribers", Description = "phrase: No subscribers", LastModified = "2012/09/04", Value = "There are no subscribers.")]
    public string NoSubscribers => this[nameof (NoSubscribers)];

    /// <summary>phrase: No mailing list has been selected.</summary>
    [ResourceEntry("NoMailingListSelected", Description = "phrase: No mailing list has been selected.", LastModified = "2012/09/04", Value = "No mailing list has been selected.")]
    public string NoMailingListSelected => this[nameof (NoMailingListSelected)];

    /// <summary>phrase: Test message subject</summary>
    [ResourceEntry("TestMessageSubject", Description = "phrase: the message subject when sending a test message", LastModified = "2012/10/19", Value = "Sitefinity newsletter test message")]
    public string TestMessageSubject => this[nameof (TestMessageSubject)];

    /// <summary>phrase: Test message body</summary>
    [ResourceEntry("TestMessageBody", Description = "phrase: the message body when sending a test message", LastModified = "2012/10/19", Value = "This is a test message sent from Sitefinity newsletter module to verify your SMTP settings. Your SMTP settings are working properly.")]
    public string TestMessageBody => this[nameof (TestMessageBody)];

    /// <summary>word: Issues</summary>
    /// <value>Issues</value>
    [ResourceEntry("Issues", Description = "word: Issues", LastModified = "2012/12/27", Value = "Issues")]
    public string Issues => this[nameof (Issues)];

    /// <summary>phrase: Sending on</summary>
    /// <value>Sending on</value>
    [ResourceEntry("SendingOn", Description = "phrase: Sending on", LastModified = "2013/01/02", Value = "Sending on")]
    public string SendingOn => this[nameof (SendingOn)];

    /// <summary>phrase: Last modified</summary>
    /// <value>Last modified</value>
    [ResourceEntry("LastModified", Description = "phrase: Last modified", LastModified = "2013/01/02", Value = "Last modified")]
    public string LastModified => this[nameof (LastModified)];

    /// <summary>word: Drafts</summary>
    /// <value>Drafts</value>
    [ResourceEntry("Drafts", Description = "word: Drafts", LastModified = "2013/01/08", Value = "Drafts")]
    public string Drafts => this[nameof (Drafts)];

    /// <summary>
    /// Status of the campaign when it has multiple scheduled issues.
    /// </summary>
    /// <value>{0} issues scheduled</value>
    [ResourceEntry("CampaignIssueStateMultipleScheduled", Description = "Status of the campaign when it has multiple scheduled issues.", LastModified = "2013/01/08", Value = "{0} issues scheduled")]
    public string CampaignIssueStateMultipleScheduled => this[nameof (CampaignIssueStateMultipleScheduled)];

    /// <summary>
    /// Status of the campaign when it has multiple draft issues.
    /// </summary>
    /// <value>{0} issues draft</value>
    [ResourceEntry("CampaignIssueStateMultipleDraft", Description = "Status of the campaign when it has multiple draft issues.", LastModified = "2013/01/08", Value = "{0} issues draft")]
    public string CampaignIssueStateMultipleDraft => this[nameof (CampaignIssueStateMultipleDraft)];

    /// <summary>
    /// Status of the campaign when it has multiple scheduled A/B tests.
    /// </summary>
    /// <value>{0} A/B tests scheduled</value>
    [ResourceEntry("MultipleScheduledAbTests", Description = "Status of the campaign when it has multiple scheduled A/B tests.", LastModified = "2013/01/08", Value = "{0} A/B tests scheduled")]
    public string MultipleScheduledAbTests => this[nameof (MultipleScheduledAbTests)];

    /// <summary>
    /// Status of the campaign when it has multiple draft A/B tests.
    /// </summary>
    /// <value>{0} draft A/B tests</value>
    [ResourceEntry("MultipleDraftAbTests", Description = "Status of the campaign when it has multiple draft A/B tests.", LastModified = "2013/01/08", Value = "{0} draft A/B tests")]
    public string MultipleDraftAbTests => this[nameof (MultipleDraftAbTests)];

    /// <summary>Soft bounce definition</summary>
    /// <value>A soft bounce is a temporary problem that prevents the email to be delivered to the recipient. Possible reasons are a full mailbox of the recipient (over quota), the recipient’s email server is down or offline, or the email message is too large.</value>
    [ResourceEntry("SoftBounceDefinition", Description = "A soft bounce is a temporary problem that prevents the email to be delivered to the recipient. Possible reasons are a full mailbox of the recipient (over quota), the recipient’s email server is down or offline, or the email message is too large.", LastModified = "2015-07-10", Value = "A soft bounce is a temporary problem that prevents the email to be delivered to the recipient. Possible reasons are a full mailbox of the recipient (over quota), the recipient’s email server is down or offline, or the email message is too large.")]
    public string SoftBounceDefinition => this[nameof (SoftBounceDefinition)];

    /// <summary>Hard bounce definition</summary>
    /// <value>A soft bounce is a temporary problem that prevents the email to be delivered to the recipient. Possible reasons are a full mailbox of the recipient (over quota), the recipient’s email server is down or offline, or the email message is too large.</value>
    [ResourceEntry("HardBounceDefinition", Description = "A hard bounce indicates a permanent problem that prevents the email to be delivered to the recipient. Possible reasons are a non-existent or wrong email address of the recipient, non-existent or wrong domain name, or the recipient’s email server has completely blocked the email delivery.", LastModified = "2015-07-10", Value = "A hard bounce indicates a permanent problem that prevents the email to be delivered to the recipient. Possible reasons are a non-existent or wrong email address of the recipient, non-existent or wrong domain name, or the recipient’s email server has completely blocked the email delivery.")]
    public string HardBounceDefinition => this[nameof (HardBounceDefinition)];

    /// <summary>label: Profile</summary>
    /// <value>Profile</value>
    [ResourceEntry("SmtpProfile", Description = "label: Profile", LastModified = "2015/07/10", Value = "Profile")]
    public string SmtpProfile => this[nameof (SmtpProfile)];
  }
}
