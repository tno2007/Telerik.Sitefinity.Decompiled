// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewslettersModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.Web;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>Module class for the Newsletters module</summary>
  [Telerik.Sitefinity.Modules.ModuleId("3D8A2051-6F6F-437C-865E-B3177689AC12")]
  public class NewslettersModule : ModuleBase, ITrackingReporter
  {
    /// <summary>
    /// Name of the newsletters module. (e.g. used in NewslettersManager)
    /// </summary>
    public const string ModuleName = "Newsletters";
    public const string CampaignUrlDiscriminator = "Sitefinity/SFNwslttrs";
    /// <summary>Id of the module. Used for licensing.</summary>
    public const string ModuleId = "3D8A2051-6F6F-437C-865E-B3177689AC12";
    private static readonly Type[] managerTypes = new Type[1]
    {
      typeof (NewslettersManager)
    };
    public static readonly Guid newslettersNodeId = new Guid("54F9F24B-F994-4DC0-A7A5-E7FA681E04B5");
    public static readonly Guid landingPageId = new Guid("C05B99E2-38DB-4F2B-B505-57088B56ED43");
    public static readonly Guid campaignsPageId = new Guid("ECE6FB22-7C88-4779-8241-29841571CACD");
    [Obsolete("This page does not exist.")]
    public static readonly Guid abCampaignsPageId = new Guid("9FFE70D5-F62D-4EE9-AA0F-16D2892F4F8E");
    [Obsolete("This page does not exist.")]
    public static readonly Guid abCampaignReportPageId = new Guid("EEC30DBF-36A8-41EB-8937-E6DBB881D962");
    public static readonly Guid mailingListsPageId = new Guid("DDCBF99D-FAAF-4DFB-A598-E7E80C592C5B");
    public static readonly Guid subscribersPageId = new Guid("2DC71B38-1A12-462D-BFCD-396C035D016E");
    public static readonly Guid templatesPageId = new Guid("44691A89-D768-4F43-872E-0785CD0F6331");
    public static readonly Guid reportsPageId = new Guid("C881B7B4-5CC1-45C2-8020-2B1AE3D4D68D");
    public static readonly Guid campaignOverviewPageId = new Guid("95F22EE4-0C8D-4715-84C9-913D339B373C");
    public static readonly Guid issueReportsPageId = new Guid("5663FD50-F277-4A57-B8BE-DBF3BB1B87F9");
    public static readonly Guid subscribersReportPageId = new Guid("6E2C6638-A484-4DAC-9ADD-5BA334A10FF3");
    public static readonly Guid abTestReportPageId = new Guid("C5ADC2EC-C0ED-11E1-837C-285F6188709B");
    [Obsolete("This page does not exist.")]
    public static readonly Guid campaignReportPageId = new Guid("7B09D4FD-B1A3-4E2F-BB54-13659DA9F8DC");
    public static readonly Guid subscriberReportPageId = new Guid("95C2F10B-51C1-4583-8873-49841E7C60E9");
    public static readonly Guid campaignManageMentGroupPageId = new Guid("760BDA3B-560B-4746-A847-05FF52F4679C");
    public static readonly Guid subscribersGroupPageId = new Guid("E7100CF1-6848-423E-8F68-6B443FE1765F");
    public static readonly Guid standardCampaignRootNodeId = new Guid("BFB71911-3969-4952-9248-C81EBE53ADDC");
    private static readonly string ResourceClassId = typeof (NewslettersResources).Name;

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.ModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => NewslettersModule.landingPageId;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => NewslettersModule.managerTypes;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module("Newsletters").Initialize().Configuration<NewslettersConfig>().Localization<NewslettersResources>().SitemapFilter<NewslettersNodeFilter>().Dialog<MailingListForm>().Dialog<SubscriberForm>().Dialog<CampaignDetailView>().Dialog<CampaignPreviewDialog>().Dialog<ScheduleCampaignDialog>().Dialog<TestEmailsForm>().Dialog<SelectListsDialog>().Dialog<ImportSubscribers>().Dialog<TemplateForm>().Dialog<DynamicMailingListSettings>().Dialog<MailingListSubscribersDialog>().Dialog<CampaignsStatisticsDialog>().Dialog<AbTestDetailView>().Dialog<ManageSubscribersDialog>().Route("Newsletters", (RouteBase) ObjectFactory.Resolve<NewslettersRoute>()).Route("NewslettersLinks", (RouteBase) new Route(NewslettersLinkRouteHandler.linkPrefix.ToString() + "/", (IRouteHandler) ObjectFactory.Resolve<NewslettersLinkRouteHandler>()));
    }

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<NewslettersConfig>();

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      this.InstallPages(initializer);
      this.InstallWidgets(initializer);
    }

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (upgradeFrom.Build <= 0)
        return;
      bool suppressSecurityChecks = initializer.PageManager.Provider.SuppressSecurityChecks;
      initializer.PageManager.Provider.SuppressSecurityChecks = true;
      if (upgradeFrom.Build <= 1210)
        this.UpgradeTo1210(initializer);
      if (upgradeFrom.Build <= 1339)
        this.UpgradeTo1339(initializer);
      if (upgradeFrom.Build <= 1371)
        this.UpgradeTo1371_FixNullCampaignDeliveryDate();
      if (upgradeFrom.Build <= 1840)
        this.UpgradeTo4216(initializer);
      if (upgradeFrom.Build < 3040)
        this.Upgrade_rebuildNewslettersMenu(initializer);
      if (upgradeFrom.Build <= 3900)
      {
        this.Upgrade_FixAbTestScheduledDate(initializer);
        this.Upgrade_FixAbTestDraftIssues(initializer);
      }
      if (upgradeFrom < SitefinityVersion.Sitefinity7_0)
        this.Upgrade_FixPagesStructure(initializer);
      if (upgradeFrom < SitefinityVersion.Sitefinity8_1)
        this.UpgradeTo81(initializer);
      initializer.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
    }

    private void InstallWidgets(SiteInitializer initializer) => initializer.Installer.Toolbox(CommonToolbox.PageWidgets).LoadOrAddSection("NewslettersToolboxSectionName").LocalizeUsing<PageResources>().SetTitle("NewslettersToolboxSectionTitle").SetDescription("NewslettersToolboxSectionDescription").LoadOrAddWidget<SubscribeForm>("SubscribeForm").LocalizeUsing<NewslettersResources>().SetTitle("SubscribeFormTitle").SetDescription("SubscribeFormDescription").SetCssClass("sfFormsIcn").Done().LoadOrAddWidget<UnsubscribeForm>("UnsubscribeForm").LocalizeUsing<NewslettersResources>().SetTitle("UnsubscribeFormTitle").SetDescription("UnsubscribeFormDescription").SetCssClass("sfFormsIcn").Done();

    private void InstallPages(SiteInitializer initializer)
    {
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade1 = initializer.Installer.CreateModuleGroupPage(NewslettersModule.newslettersNodeId, "Newsletters").PlaceUnder(CommonNode.Marketing).SetOrdinal(3f).ShowInNavigation().RenderAsText().LocalizeUsing<NewslettersResources>().SetTitleLocalized("PageGroupNodeTitle").SetUrlNameLocalized("PageGroupNodeUrlName").SetDescriptionLocalized("PageGroupNodeDescription").AddAttribute("hideFromMenuOnInvalidLicense", "true").AddAttribute("ModuleIdAttribute", "3D8A2051-6F6F-437C-865E-B3177689AC12").AddChildPage(NewslettersModule.landingPageId, "Newsletters").LocalizeUsing<NewslettersResources>().SetTitleLocalized("DashboardTitle").SetUrlNameLocalized("DashboardUrlName").SetDescriptionLocalized("DashboardDescription").SetHtmlTitleLocalized("Newsletters");
      NewslettersControlPanel newslettersControlPanel1 = new NewslettersControlPanel();
      newslettersControlPanel1.ID = "newslttrsCntrlPnl";
      newslettersControlPanel1.ViewMode = typeof (DashboardView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade2 = modulePageFacade1.AddControl((Control) newslettersControlPanel1).Done().AddChildPage(NewslettersModule.campaignsPageId, "Campaigns").LocalizeUsing<NewslettersResources>().SetTitleLocalized("CampaignsTitle").SetUrlNameLocalized("CampaignsUrlName").SetDescriptionLocalized("CampaignsDescription").SetHtmlTitleLocalized("CampaignsHtmlTitle").ShowInNavigation();
      NewslettersControlPanel newslettersControlPanel2 = new NewslettersControlPanel();
      newslettersControlPanel2.ID = "cmpgnsCtrlPnl";
      newslettersControlPanel2.ViewMode = typeof (CampaignsView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade3 = modulePageFacade2.AddControl((Control) newslettersControlPanel2).Done().AddChildPage(NewslettersModule.templatesPageId, "Templates").LocalizeUsing<NewslettersResources>().SetTitleLocalized("TemplatesTitle").SetUrlNameLocalized("TemplatesUrlName").SetDescriptionLocalized("TemplatesDescription").SetHtmlTitleLocalized("TemplatesTitle").ShowInNavigation();
      NewslettersControlPanel newslettersControlPanel3 = new NewslettersControlPanel();
      newslettersControlPanel3.ID = "tmpltsCtrlPnl";
      newslettersControlPanel3.ViewMode = typeof (TemplatesView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade4 = modulePageFacade3.AddControl((Control) newslettersControlPanel3).Done().AddChildPage(NewslettersModule.reportsPageId, "Reports").LocalizeUsing<NewslettersResources>().SetTitleLocalized("ReportsTitle").SetUrlNameLocalized("ReportsUrlName").SetDescriptionLocalized("ReportsDescription").SetHtmlTitleLocalized("ReportsHtmlTitle").HideFromNavigation();
      NewslettersControlPanel newslettersControlPanel4 = new NewslettersControlPanel();
      newslettersControlPanel4.ID = "rprtsCtrlPnl";
      newslettersControlPanel4.ViewMode = typeof (ReportsView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade5 = modulePageFacade4.AddControl((Control) newslettersControlPanel4).Done().AddChildPage(NewslettersModule.mailingListsPageId, "MailingLists").LocalizeUsing<NewslettersResources>().SetTitleLocalized("MailingListsTitle").SetUrlNameLocalized("MailingListsUrlName").SetDescriptionLocalized("MailingListsDescription").SetHtmlTitleLocalized("MailingListsHtmlTitle").ShowInNavigation();
      NewslettersControlPanel newslettersControlPanel5 = new NewslettersControlPanel();
      newslettersControlPanel5.ID = "mailingLstCtrlPnl";
      newslettersControlPanel5.ViewMode = typeof (MailingListsView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade6 = modulePageFacade5.AddControl((Control) newslettersControlPanel5).Done().AddChildPage(NewslettersModule.subscribersPageId, "Subscribers").LocalizeUsing<NewslettersResources>().SetTitleLocalized("SubscribersTitle").SetUrlNameLocalized("SubscribersUrlName").SetDescriptionLocalized("SubscribersDescription").SetHtmlTitleLocalized("SubscribersHtmlTitle").ShowInNavigation();
      NewslettersControlPanel newslettersControlPanel6 = new NewslettersControlPanel();
      newslettersControlPanel6.ID = "sbscrbrsCtrlPnl";
      newslettersControlPanel6.ViewMode = typeof (SubscribersView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade7 = modulePageFacade6.AddControl((Control) newslettersControlPanel6).Done().AddChildPage(NewslettersModule.campaignOverviewPageId, "CampaignOverview").LocalizeUsing<NewslettersResources>().SetTitleLocalized("CampaignOverviewTitle").SetUrlNameLocalized("CampaignOverviewUrlName").SetDescriptionLocalized("CampaignOverviewDescription").SetHtmlTitleLocalized("CampaignOverviewHtmlTitle");
      NoSidebarNewslettersControlPanel newslettersControlPanel7 = new NoSidebarNewslettersControlPanel();
      newslettersControlPanel7.ID = "cmpgnOvwCtrlPnl";
      newslettersControlPanel7.ViewMode = typeof (CampaignOverview).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade8 = modulePageFacade7.AddControl((Control) newslettersControlPanel7).Done().AddChildPage(NewslettersModule.issueReportsPageId, "IssueReport").LocalizeUsing<NewslettersResources>().SetTitleLocalized("IssueReportTitle").SetUrlNameLocalized("IssueReportUrlName").SetDescriptionLocalized("IssueReportDescription").SetHtmlTitleLocalized("IssueReportHtmlTitle");
      NoSidebarNewslettersControlPanel newslettersControlPanel8 = new NoSidebarNewslettersControlPanel();
      newslettersControlPanel8.ID = "issueRprtCtrlPnl";
      newslettersControlPanel8.ViewMode = typeof (IssueReportView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade9 = modulePageFacade8.AddControl((Control) newslettersControlPanel8).Done().AddChildPage(NewslettersModule.subscribersReportPageId, "SubscribersReport").LocalizeUsing<NewslettersResources>().SetTitleLocalized("SubscribersReportTitle").SetUrlNameLocalized("SubscribersReportUrlName").SetDescriptionLocalized("SubscribersReportDescription").SetHtmlTitleLocalized("SubscribersReportHtmlTitle");
      SubscribersReportControlPanel reportControlPanel = new SubscribersReportControlPanel();
      reportControlPanel.ID = "subscribersRprtCtrlPnl";
      reportControlPanel.ViewMode = typeof (SubscribersReportView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade10 = modulePageFacade9.AddControl((Control) reportControlPanel).Done().AddChildPage(NewslettersModule.subscriberReportPageId, "SubscriberReport").LocalizeUsing<NewslettersResources>().SetTitleLocalized("SubscriberReportTitle").SetUrlNameLocalized("SubscriberReportUrlName").SetDescriptionLocalized("SubscriberReportDescription").SetHtmlTitleLocalized("SubscriberReportHtmlTitle");
      NewslettersControlPanel newslettersControlPanel9 = new NewslettersControlPanel();
      newslettersControlPanel9.ID = "sbscriberRprtCtrlPnl";
      newslettersControlPanel9.ViewMode = typeof (SubscriberReportView).Name;
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade11 = modulePageFacade10.AddControl((Control) newslettersControlPanel9).Done().AddChildPage(NewslettersModule.abTestReportPageId, "AbTestReport").LocalizeUsing<NewslettersResources>().SetTitleLocalized("AbTestReportTitle").SetUrlNameLocalized("AbTestReportUrlName").SetDescriptionLocalized("AbTestReportDescription").SetHtmlTitleLocalized("AbTestReportHtmlTitle");
      NoSidebarNewslettersControlPanel newslettersControlPanel10 = new NoSidebarNewslettersControlPanel();
      newslettersControlPanel10.ID = "abTestRprtCtrlPnl";
      newslettersControlPanel10.ViewMode = typeof (AbTestReportView).Name;
      modulePageFacade11.AddControl((Control) newslettersControlPanel10).Done().Done();
    }

    private void UpgradeTo1210(SiteInitializer initializer)
    {
      PageNode pageNode = initializer.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == NewslettersModule.newslettersNodeId));
      if (pageNode == null)
        return;
      pageNode.Title = (Lstring) Res.Expression("NewslettersResources", "PageGroupNodeTitle");
      Res.SetLstring(pageNode.Description, NewslettersModule.ResourceClassId, "PageGroupNodeDescription");
    }

    private void UpgradeTo1339(SiteInitializer initializer)
    {
      PageNode pageNode = initializer.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == NewslettersModule.mailingListsPageId));
      PageData pageData = pageNode.GetPageData();
      if (pageNode == null || pageData == null)
        return;
      string controlType = typeof (NewslettersControlPanel).FullName;
      PageControl pageControl = pageData.Controls.FirstOrDefault<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == controlType));
      if (pageControl == null)
        return;
      ControlProperty controlProperty = pageControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ViewMode"));
      if (controlProperty == null)
        return;
      controlProperty.Value = typeof (MailingListsView).Name;
    }

    private void Upgrade_rebuildNewslettersMenu(SiteInitializer initializer)
    {
      PageManager pageManager = initializer.PageManager;
      PageNode pageNode1 = pageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == NewslettersModule.newslettersNodeId));
      if (pageNode1 != null)
      {
        Guid parentId = pageNode1.Id;
        IQueryable<PageNode> source = pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == parentId));
        Expression<Func<PageNode, float>> keySelector = (Expression<Func<PageNode, float>>) (p => p.Ordinal);
        foreach (PageNode pageNode2 in (IEnumerable<PageNode>) source.OrderBy<PageNode, float>(keySelector))
          pageManager.Delete(pageNode2);
        pageManager.Delete(pageNode1);
        initializer.SaveChanges(false);
      }
      this.InstallPages(initializer);
    }

    private void Upgrade_FixAbTestScheduledDate(SiteInitializer initializer)
    {
      IQueryable<ABCampaign> queryable = initializer.GetManagerInTransaction<NewslettersManager>().GetABCampaigns().Where<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => (int) t.ABTestingStatus == 0));
      SchedulingManager managerInTransaction = initializer.GetManagerInTransaction<SchedulingManager>();
      foreach (ABCampaign abCampaign in (IEnumerable<ABCampaign>) queryable)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NewslettersModule.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new NewslettersModule.\u003C\u003Ec__DisplayClass13_0();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass130.abTest = abCampaign;
        ParameterExpression parameterExpression;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        ScheduledTaskData scheduledTaskData = managerInTransaction.GetTaskData().Where<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "AbTestSendTask", (Expression) Expression.Equal(t.Key, (Expression) Expression.Call(cDisplayClass130.abTest.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression)).FirstOrDefault<ScheduledTaskData>();
        if (scheduledTaskData != null)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass130.abTest.DateSent = scheduledTaskData.ExecuteTime;
        }
      }
    }

    private void Upgrade_FixAbTestDraftIssues(SiteInitializer initializer)
    {
      IQueryable<ABCampaign> abCampaigns = initializer.GetManagerInTransaction<NewslettersManager>().GetABCampaigns();
      Expression<Func<ABCampaign, bool>> predicate = (Expression<Func<ABCampaign, bool>>) (t => (int) t.ABTestingStatus == 0 || (int) t.ABTestingStatus == 1);
      foreach (ABCampaign abCampaign in (IEnumerable<ABCampaign>) abCampaigns.Where<ABCampaign>(predicate))
      {
        abCampaign.CampaignA.CampaignState = CampaignState.ABTest;
        abCampaign.CampaignB.CampaignState = CampaignState.ABTest;
      }
    }

    private void Upgrade_FixPagesStructure(SiteInitializer initializer)
    {
      PageManager pageManager = initializer.PageManager;
      PageNode pageNode1 = pageManager.GetPageNode(NewslettersModule.newslettersNodeId);
      PageNode pageNode2 = pageManager.GetPageNode(NewslettersModule.campaignsPageId);
      if (pageNode2.ParentId != NewslettersModule.newslettersNodeId)
        pageManager.ChangeParent(pageNode2, pageNode1);
      PageNode pageNode3 = pageManager.GetPageNode(NewslettersModule.templatesPageId);
      if (pageNode3.ParentId != NewslettersModule.newslettersNodeId)
        pageManager.ChangeParent(pageNode3, pageNode1);
      PageNode pageNode4 = pageManager.GetPageNode(NewslettersModule.reportsPageId);
      if (pageNode4.ParentId != NewslettersModule.newslettersNodeId)
        pageManager.ChangeParent(pageNode4, pageNode1);
      PageNode pageNode5 = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (x => x.Id == NewslettersModule.campaignManageMentGroupPageId));
      if (pageNode5 != null)
        pageManager.Delete(pageNode5);
      PageNode pageNode6 = pageManager.GetPageNode(NewslettersModule.mailingListsPageId);
      if (pageNode6.ParentId != NewslettersModule.newslettersNodeId)
        pageManager.ChangeParent(pageNode6, pageNode1);
      PageNode pageNode7 = pageManager.GetPageNode(NewslettersModule.subscribersPageId);
      if (pageNode7.ParentId != NewslettersModule.newslettersNodeId)
        pageManager.ChangeParent(pageNode7, pageNode1);
      PageNode pageNode8 = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (x => x.Id == NewslettersModule.subscribersGroupPageId));
      if (pageNode8 != null)
        pageManager.Delete(pageNode8);
      if (!(pageNode1.ParentId != SiteInitializer.MarketingNodeId))
        return;
      PageNode pageNode9 = pageManager.GetPageNode(SiteInitializer.MarketingNodeId);
      pageManager.ChangeParent(pageNode1, pageNode9);
      pageNode1.RenderAsLink = false;
      pageNode1.Ordinal = 3f;
    }

    private void UpgradeTo1371_FixNullCampaignDeliveryDate()
    {
      foreach (string providerName in NewslettersManager.GetManager().StaticProviders.Select<NewslettersDataProvider, string>((Func<NewslettersDataProvider, string>) (p => p.Name)).ToArray<string>())
      {
        NewslettersManager manager = NewslettersManager.GetManager(providerName, "NewsLettersModule.UpgradeFrom1371_FixNullCampaignDeliveryDate");
        manager.Provider.SuppressSecurityChecks = true;
        foreach (Campaign campaign in manager.GetCampaigns().ToArray<Campaign>())
        {
          if (campaign.DeliveryDate == new DateTime())
            campaign.DeliveryDate = DateTime.UtcNow;
        }
      }
      try
      {
        TransactionManager.CommitTransaction("NewsLettersModule.UpgradeFrom1371_FixNullCampaignDeliveryDate");
      }
      catch
      {
        try
        {
          TransactionManager.RollbackTransaction("NewsLettersModule.UpgradeFrom1371_FixNullCampaignDeliveryDate");
        }
        catch
        {
        }
      }
    }

    private void UpgradeTo4216(SiteInitializer initializer) => this.InstallWidgets(initializer);

    internal static bool TryUpgradeTo2860(out string errorMessage)
    {
      bool flag = true;
      errorMessage = string.Empty;
      try
      {
        if (!LicenseState.CheckIsModuleLicensedInAnyDomain("3D8A2051-6F6F-437C-865E-B3177689AC12") || !SystemManager.IsModuleEnabled("Newsletters"))
          return flag;
        string[] array = NewslettersManager.GetManager().StaticProviders.Select<NewslettersDataProvider, string>((Func<NewslettersDataProvider, string>) (p => p.Name)).ToArray<string>();
        SchedulingManager manager1 = SchedulingManager.GetManager();
        for (int index1 = 0; index1 < array.Length; ++index1)
        {
          NewslettersManager manager2 = NewslettersManager.GetManager(array[index1]);
          NewslettersModule.UpgradeAbTests(manager2, manager1);
          manager2.Provider.SetExecutionStateData("ignore-statistics", (object) true);
          manager2.SaveChanges();
          NewslettersModule.CreateIssuesForOldCampaigns(manager2, manager1);
          manager2.Provider.SetExecutionStateData("ignore-statistics", (object) true);
          manager2.SaveChanges();
          NewslettersModule.UpdateStatistics(manager2);
          IOrderedQueryable<DeliveryEntry> source = manager2.GetDeliveryEntries().OrderBy<DeliveryEntry, Guid>((Expression<Func<DeliveryEntry, Guid>>) (o => o.CampaignId));
          double num = Math.Ceiling((double) source.Count<DeliveryEntry>() / 200.0);
          for (int index2 = 0; (double) index2 < num; ++index2)
          {
            foreach (DeliveryEntry deliveryEntry in (IEnumerable<DeliveryEntry>) source.Skip<DeliveryEntry>(index2 * 200).Take<DeliveryEntry>(200))
            {
              DeliveryEntry entry = deliveryEntry;
              if (manager2.GetSubscribers().Any<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Id == entry.SubscriberId)))
              {
                IssueSubscriberReport issueSubscriberReport = manager2.GetIssueSubscriberReports().FirstOrDefault<IssueSubscriberReport>((Expression<Func<IssueSubscriberReport, bool>>) (r => r.IssueId == entry.CampaignId && r.SubscriberId == entry.SubscriberId));
                if (issueSubscriberReport == null)
                {
                  issueSubscriberReport = manager2.CreateIssueSubscriberReport();
                  issueSubscriberReport.IssueId = entry.CampaignId;
                  issueSubscriberReport.SubscriberId = entry.SubscriberId;
                }
                manager2.Provider.UpdateIssueSubscriberReport(issueSubscriberReport);
              }
            }
            manager2.Provider.SetExecutionStateData("ignore-statistics", (object) true);
            manager2.SaveChanges();
          }
        }
      }
      catch (Exception ex)
      {
        flag = false;
        Log.Write((object) ex);
        errorMessage = ex.Message;
      }
      return flag;
    }

    private void UpgradeTo81(SiteInitializer initializer)
    {
      SchedulingManager managerInTransaction = initializer.GetManagerInTransaction<SchedulingManager>();
      IQueryable<ScheduledTaskData> taskData = managerInTransaction.GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == "BounceCheckTask");
      foreach (ScheduledTaskData task in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
        managerInTransaction.DeleteTaskData(task);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = NewslettersModule.GetServiceContext();
      NewslettersConfig newslettersConfig = Config.Get<NewslettersConfig>();
      string customProperty = notificationService.GetSenderProfile(serviceContext, newslettersConfig.NotificationsSmtpProfile).CustomProperties["defaultSenderEmailAddress"];
      if (customProperty == null)
        return;
      NewslettersModule.UpdateSenderProfileSenderAddress(notificationService, serviceContext, "SendGrid", customProperty);
      NewslettersModule.UpdateSenderProfileSenderAddress(notificationService, serviceContext, "Mandrill", customProperty);
      NewslettersModule.UpdateSenderProfileSenderAddress(notificationService, serviceContext, "MailGun", customProperty);
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CAF-81395CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    private static void UpdateSenderProfileSenderAddress(
      INotificationService ns,
      ServiceContext serviceContext,
      string profileName,
      string senderEmailAddress)
    {
      ISenderProfile senderProfile = ns.GetSenderProfile(serviceContext, profileName);
      senderProfile.CustomProperties["defaultSenderEmailAddress"] = senderEmailAddress;
      ns.SaveSenderProfile(serviceContext, senderProfile);
    }

    private static void CreateIssuesForOldCampaigns(
      NewslettersManager managerInstance,
      SchedulingManager schedManager)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersModule.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new NewslettersModule.\u003C\u003Ec__DisplayClass22_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass220.rootCampaigns = managerInstance.GetCampaigns().ToArray<Campaign>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersModule.\u003C\u003Ec__DisplayClass22_1 cDisplayClass221 = new NewslettersModule.\u003C\u003Ec__DisplayClass22_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass221.CS\u0024\u003C\u003E8__locals1 = cDisplayClass220;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (cDisplayClass221.camp = 0; cDisplayClass221.camp < cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns.Length; cDisplayClass221.camp++)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (managerInstance.GetIssues(cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns[cDisplayClass221.camp]).Count<Campaign>() <= 0)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          Campaign issue = managerInstance.CreateIssue(cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns[cDisplayClass221.camp], true);
          if (issue.CampaignState == CampaignState.MissingMailingList)
          {
            IQueryable<DeliveryEntry> deliveryEntries = managerInstance.GetDeliveryEntries();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            Expression<Func<DeliveryEntry, bool>> predicate = (Expression<Func<DeliveryEntry, bool>>) (i => i.CampaignId == cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns[cDisplayClass221.camp].Id);
            issue.CampaignState = !deliveryEntries.Any<DeliveryEntry>(predicate) ? CampaignState.Draft : CampaignState.Completed;
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns[cDisplayClass221.camp].CampaignState == CampaignState.Scheduled)
          {
            ParameterExpression parameterExpression;
            // ISSUE: method reference
            // ISSUE: field reference
            // ISSUE: field reference
            // ISSUE: field reference
            // ISSUE: method reference
            // ISSUE: method reference
            // ISSUE: method reference
            ScheduledTaskData scheduledTaskData = schedManager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.AndAlso(t.TaskName == "CampaignDeliveryTask", (Expression) Expression.Equal((Expression) Expression.Call(t.TaskData, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.ArrayIndex((Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass221, typeof (NewslettersModule.\u003C\u003Ec__DisplayClass22_1)), FieldInfo.GetFieldFromHandle(__fieldref (NewslettersModule.\u003C\u003Ec__DisplayClass22_1.CS\u0024\u003C\u003E8__locals1))), FieldInfo.GetFieldFromHandle(__fieldref (NewslettersModule.\u003C\u003Ec__DisplayClass22_0.rootCampaigns))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass221, typeof (NewslettersModule.\u003C\u003Ec__DisplayClass22_1)), FieldInfo.GetFieldFromHandle(__fieldref (NewslettersModule.\u003C\u003Ec__DisplayClass22_1.camp)))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Campaign.get_Id))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()))), parameterExpression));
            if (scheduledTaskData != null)
            {
              scheduledTaskData.Key = issue.Id.ToString();
              scheduledTaskData.TaskData = issue.Id.ToString();
              issue.DeliveryDate = scheduledTaskData.ExecuteTime;
              schedManager.SaveChanges();
            }
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          cDisplayClass221.CS\u0024\u003C\u003E8__locals1.rootCampaigns[cDisplayClass221.camp].CampaignState = CampaignState.Draft;
          if (issue.MessageBody.PlainTextVersion == "")
            issue.MessageBody.PlainTextVersion = (string) null;
        }
      }
    }

    private static void UpdateStatistics(NewslettersManager managerInstance)
    {
      Campaign[] array = managerInstance.GetCampaigns().ToArray<Campaign>();
      for (int index = 0; index < array.Length; ++index)
      {
        IQueryable<Campaign> issues = managerInstance.GetIssues(array[index]);
        if (issues.Count<Campaign>() == 1)
        {
          Campaign campaign = issues.First<Campaign>();
          if (managerInstance.Provider is IOpenAccessDataProvider provider)
          {
            SitefinityOAContext context = provider.GetContext();
            if (context != null)
            {
              using (UpgradingContext upgradingContext = new UpgradingContext(context))
              {
                switch (upgradingContext.Connection.DbType)
                {
                  case DatabaseType.MsSql:
                  case DatabaseType.SqlAzure:
                  case DatabaseType.SqlCE:
                  case DatabaseType.MySQL:
                    upgradingContext.ExecuteSQL("UPDATE sf_delivery_entry SET campaign_id = '{0}' WHERE campaign_id = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE sf_bounce_stat SET sf_campaign_id = '{0}' WHERE sf_campaign_id = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE sf_link_click_stat SET campaign_id = '{0}' WHERE campaign_id = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE sf_open_stat SET campaign_id = '{0}' WHERE campaign_id = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    continue;
                  case DatabaseType.Oracle:
                  case DatabaseType.PostgreSql:
                    upgradingContext.ExecuteSQL("UPDATE \"sf_delivery_entry\" SET \"campaign_id\" = '{0}' WHERE \"campaign_id\" = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE \"sf_bounce_stat\" SET \"sf_campaign_id\" = '{0}' WHERE \"sf_campaign_id\" = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE \"sf_link_click_stat\" SET \"campaign_id\" = '{0}' WHERE \"campaign_id\" = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    upgradingContext.ExecuteSQL("UPDATE \"sf_open_stat\" SET \"campaign_id\" = '{0}' WHERE \"campaign_id\" = '{1}'".Arrange((object) campaign.Id, (object) array[index].Id));
                    continue;
                  default:
                    throw new ArgumentException(string.Format("Unsupported database type: {0}", (object) Enum.GetName(typeof (DatabaseType), (object) upgradingContext.Connection.DbType)));
                }
              }
            }
          }
        }
      }
    }

    private static void UpgradeAbTests(
      NewslettersManager managerInstance,
      SchedulingManager schedManager)
    {
      foreach (ABCampaign abCampaign in (IEnumerable<ABCampaign>) managerInstance.GetABCampaigns())
      {
        Campaign campaignA = abCampaign.CampaignA;
        Campaign campaignB = abCampaign.CampaignB;
        Campaign campaign = managerInstance.CreateCampaign(false);
        campaign.Name = campaignA.Name + " " + Res.Get<NewslettersResources>().ABTestNote;
        campaign.List = campaignA.List;
        campaign.FromName = campaignA.FromName;
        campaign.ReplyToEmail = campaignA.ReplyToEmail;
        managerInstance.CopyMessageBody(campaignA.MessageBody, campaign.MessageBody);
        abCampaign.ABTestingStatus = ABTestingStatus.Done;
        abCampaign.RootCampaign = campaign;
        abCampaign.Name = campaignA.Name;
        managerInstance.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        managerInstance.SaveChanges();
        abCampaign.CampaignA = NewslettersModule.CopyIssue(managerInstance, schedManager, campaign, campaignA);
        managerInstance.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        managerInstance.SaveChanges();
        abCampaign.CampaignB = NewslettersModule.CopyIssue(managerInstance, schedManager, campaign, campaignB);
        managerInstance.Provider.SetExecutionStateData("ignore-statistics", (object) true);
        managerInstance.SaveChanges();
      }
    }

    private static Campaign CopyIssue(
      NewslettersManager managerInstance,
      SchedulingManager schedManager,
      Campaign rootCampaign,
      Campaign campaignToCopy)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NewslettersModule.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new NewslettersModule.\u003C\u003Ec__DisplayClass25_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.campaignToCopy = campaignToCopy;
      Campaign campaign = managerInstance.CreateCampaign(true);
      campaign.RootCampaign = rootCampaign;
      // ISSUE: reference to a compiler-generated field
      Synchronizer.CopyProperties(cDisplayClass250.campaignToCopy, campaign);
      // ISSUE: reference to a compiler-generated field
      managerInstance.CopyMessageBody(cDisplayClass250.campaignToCopy.MessageBody, campaign.MessageBody);
      campaign.CampaignState = CampaignState.ABTest;
      if (campaign.MessageBody.PlainTextVersion == "")
        campaign.MessageBody.PlainTextVersion = (string) null;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass250.campaignToCopy.CampaignState == CampaignState.Scheduled)
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        ScheduledTaskData scheduledTaskData = schedManager.GetTaskData().FirstOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.Equal((Expression) Expression.Call(t.TaskData, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass250, typeof (NewslettersModule.\u003C\u003Ec__DisplayClass25_0)), FieldInfo.GetFieldFromHandle(__fieldref (NewslettersModule.\u003C\u003Ec__DisplayClass25_0.campaignToCopy))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Campaign.get_Id))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>())), parameterExpression));
        if (scheduledTaskData != null)
          campaign.DeliveryDate = scheduledTaskData.ExecuteTime;
      }
      // ISSUE: reference to a compiler-generated field
      managerInstance.CopyIssueStatistics(cDisplayClass250.campaignToCopy, campaign);
      return campaign;
    }

    /// <summary>Gets the service context.</summary>
    /// <returns></returns>
    internal static ServiceContext GetServiceContext() => new ServiceContext("ThisApplicationKey", "Newsletters");

    /// <inheritdoc />
    object ITrackingReporter.GetReport()
    {
      DateTime lastYear = DateTime.UtcNow.AddYears(-1);
      NewslettersManager manager = NewslettersManager.GetManager();
      IQueryable<Campaign> campaigns = manager.GetCampaigns();
      int num1 = campaigns.Count<Campaign>();
      int num2 = campaigns.Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.LastModified > lastYear)).Count<Campaign>();
      IQueryable<Campaign> issues = manager.GetIssues();
      int num3 = issues.Count<Campaign>();
      int num4 = issues.Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.LastModified > lastYear)).Count<Campaign>();
      int num5 = issues.Where<Campaign>((Expression<Func<Campaign, bool>>) (c => (int) c.MessageBody.MessageBodyType == 2)).Count<Campaign>();
      int num6 = issues.Where<Campaign>((Expression<Func<Campaign, bool>>) (c => (int) c.MessageBody.MessageBodyType == 1)).Count<Campaign>();
      int num7 = issues.Where<Campaign>((Expression<Func<Campaign, bool>>) (c => (int) c.MessageBody.MessageBodyType == 0)).Count<Campaign>();
      return (object) new NewslettersModuleReport()
      {
        ModuleName = "Newsletters",
        CampaignsCount = num1,
        CampaignsModifiedLastYearCount = num2,
        IssuesWithWebPageBodyCount = num5,
        IssuesWithRichTextBodyCount = num6,
        IssuesWithPlainTextBodyCount = num7,
        IssuesCount = num3,
        IssuesModifiedLastYearCount = num4
      };
    }
  }
}
