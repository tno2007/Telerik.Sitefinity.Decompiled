// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Routing;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Modules.Forms.Data;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.MessageTemplates;
using Telerik.Sitefinity.Modules.Forms.Report;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Operations;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;
using Telerik.Sitefinity.Web.Services.Extensibility;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Represents generic content module.</summary>
  [Telerik.Sitefinity.Modules.ModuleId("A64410F7-2F1E-4068-81D0-E28D864DE323")]
  public class FormsModule : 
    ContentModuleBase,
    ITypeSettingsProvider,
    IActionMessageTemplatesProvider,
    ITrackingReporter,
    IPublishingEnabledModule
  {
    /// <summary>
    /// Defines the configuration key that the FormsView control will use to load its sub-views
    /// </summary>
    public const string FormsViewConfigKey = "FormsView";
    /// <summary>Identity for the home page in the forms module</summary>
    public static readonly Guid HomePageId = new Guid("2E5484C2-07F8-4BD5-845C-72CA2375F80B");
    /// <summary>
    /// Identity for the page group used by all pages in the forms module
    /// </summary>
    public static readonly Guid EntriesPageID = new Guid("6D03990E-E79B-43F4-8B7B-65F1E5DCAA92");
    /// <summary>
    /// Identity for the page group used by all pages in the forms module
    /// </summary>
    public static readonly Guid FormsPageGroupId = new Guid("6B4942DD-0BED-4A4E-8E4A-20AE6639BDD9");
    /// <summary>Name of the module.</summary>
    public const string EntriesUrlName = "entries";
    /// <summary>Name of the module.</summary>
    public const string ModuleName = "Forms";
    /// <summary>Id of the module. Used for licensing.</summary>
    public const string ModuleId = "A64410F7-2F1E-4068-81D0-E28D864DE323";
    /// <summary>Localization resources' class Id for forms</summary>
    public static readonly string ResourceClassId = typeof (FormsResources).Name;
    /// <summary>
    /// Defines the control id of the ContentView inside the Forms screen.
    /// </summary>
    public const string FormsBackedContentViewControlId = "frmsCntView";
    /// <summary>
    /// Defines the control id of the ContentView inside the Form Responses screen.
    /// </summary>
    public const string FormResponsesBackedContentViewControlId = "frmRspnsesCntView";
    private ConnectorFormsEventHandler connectorFormsEventHandler;
    private static IEnumerable<IActionMessageTemplate> actionMessageTemplates;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[1]
    {
      typeof (FormsManager)
    };

    /// <summary>Gets the landing page id for forms module.</summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => FormsModule.HomePageId;

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartupAsync;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().Dialog<SiteSelectorDialog>().Configuration<FormsConfig>().SitemapFilter<FormsSitemapFilter>().Route("FormsEditor", (RouteBase) ObjectFactory.Resolve<FormsRoute>()).Route("FormsRedirect", (RouteBase) new Route("Sitefinity/Forms", (IRouteHandler) new BackendRedirectRouteHandler("Forms", string.Empty))).Route("FormsEntries", (RouteBase) new Route("Sitefinity/Content/FormEntries/Export", (IRouteHandler) ObjectFactory.Resolve<FormEntriesRouteHandler>())).Route("AjaxFormsSubmit", (RouteBase) new Route("Forms/Submit", (IRouteHandler) ObjectFactory.Resolve<FormsSubmitRouteHandler>()));
      SystemManager.TypeRegistry.Register(typeof (FormDescription).FullName, new SitefinityType()
      {
        PluralTitle = Res.Get<FormsResources>().FormsResourcesTitlePlural,
        SingularTitle = Res.Get<FormsResources>().FormsResourcesTitlePlural,
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null,
        Icon = "clipboard"
      });
      SystemManager.TypeRegistry.Register(typeof (FormDraft).FullName, new SitefinityType()
      {
        PluralTitle = Res.Get<FormsResources>().FormsResourcesTitlePlural,
        SingularTitle = Res.Get<FormsResources>().FormsResourcesTitlePlural,
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null,
        Icon = "clipboard"
      });
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (FormOperationsProvider), typeof (FormOperationsProvider).Name);
      this.InitializeNotificationSubscribtion();
      this.RegisterModuleForSubscriberSyncronization();
      this.RegisterIoCTypes();
    }

    /// <summary>Integrate the module into the system.</summary>
    public override void Load()
    {
      base.Load();
      Bootstrapper.Initialized -= new EventHandler<ExecutedEventArgs>(this.Bootstrapper_Initialized);
      Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(this.Bootstrapper_Initialized);
    }

    /// <summary>Handles the Initialized event of the Bootstrapper.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    protected virtual void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
    {
      if (!(e.CommandName == "Bootstrapped") || !SystemManager.IsModuleEnabled("Forms") || this.connectorFormsEventHandler != null)
        return;
      this.connectorFormsEventHandler = new ConnectorFormsEventHandler();
      this.connectorFormsEventHandler.Initialize();
    }

    /// <summary>
    /// This method is invoked during the unload process of an active module from Sitefinity, e.g. when a module is deactivated. For instance this method is also invoked for every active module during a restart of the application.
    /// Typically you will use this method to unsubscribe the module from all events to which it has subscription.
    /// </summary>
    public override void Unload()
    {
      if (this.connectorFormsEventHandler != null)
        this.connectorFormsEventHandler.Uninitialize();
      base.Unload();
    }

    /// <summary>
    /// Uninstall the module from Sitefinity system. Deletes the module artifacts added with Install method.
    /// </summary>
    /// <param name="initializer">The site initializer instance.</param>
    public override void Uninstall(SiteInitializer initializer)
    {
      if (this.connectorFormsEventHandler == null)
        return;
      this.connectorFormsEventHandler.Uninitialize();
    }

    /// <summary>Installs the pages.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallPages(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(FormsModule.FormsPageGroupId, "Forms").PlaceUnder(CommonNode.TypesOfContent).SetOrdinal(7).LocalizeUsing<FormsResources>().SetTitleLocalized("FormsTitle").SetUrlNameLocalized("PageGroupNodeTitle").SetDescriptionLocalized("PageGroupNodeDescription").AddAttribute("hideFromMenuOnInvalidLicense", "true").AddAttribute("ModuleIdAttribute", "A64410F7-2F1E-4068-81D0-E28D864DE323").AddChildPage(FormsModule.HomePageId, "Forms").LocalizeUsing<FormsResources>().SetTitleLocalized("FormsTitle").SetUrlNameLocalized("FormsUrlName").SetDescriptionLocalized("FormsDescription").SetHtmlTitleLocalized("FormsHtmlTitle").AddContentView("FormsBackend", "frmsCntView").Done().AddChildPage(FormsModule.EntriesPageID, "ResponsesFor").LocalizeUsing<FormsResources>().SetTitleLocalized("FormsResponses").SetUrlName("entries").SetHtmlTitleLocalized("ResponsesHtmlTitle").AddContentView((Action<ContentView>) (c =>
    {
      c.ID = "frmRspnsesCntView";
      c.ControlDefinitionName = "FormsBackend";
      c.MasterViewName = "FormsBackendListDetail";
      c.ContentViewDisplayMode = ContentViewDisplayMode.Master;
    })).Done();

    /// <summary>Installs the taxonomies.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallTaxonomies(SiteInitializer initializer) => this.InstallTaxonomy(initializer, typeof (FormDescription));

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>();

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallConfiguration(SiteInitializer initializer)
    {
      initializer.Installer.PageToolbox().ContentSection().LoadOrAddWidget<FormsControl>("FormsControl").SetTitle("FormsControlTitle").SetDescription("FormsControlDescription").LocalizeUsing<FormsResources>().SetCssClass("sfFormsIcn");
      this.InitializeSortingExpressionSettings(initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings);
    }

    protected internal override IDictionary<Type, Guid> GetTypeLandingPagesMapping() => (IDictionary<Type, Guid>) new Dictionary<Type, Guid>()
    {
      {
        typeof (FormDescription),
        FormsModule.HomePageId
      }
    };

    /// <summary>
    /// Initializes the sorting expression settings - adding command items for the static columns.
    /// </summary>
    /// <param name="sortingExpressions">The sorting expressions.</param>
    protected virtual void InitializeSortingExpressionSettings(
      ConfigElementList<SortingExpressionElement> sortingExpressions)
    {
      this.AddSortingExpression(sortingExpressions, typeof (FormEntry));
      this.AddSortingExpressionForm(sortingExpressions, typeof (Form));
    }

    /// <summary>
    /// Initializes the sorting expression settings - adding command items for the static columns.
    /// </summary>
    /// <param name="sortingExpressions">The sorting expressions.</param>
    protected virtual void InitializeFormSortingExpressionSettings(
      ConfigElementList<SortingExpressionElement> sortingExpressions)
    {
      this.AddSortingExpressionForm(sortingExpressions, typeof (Form));
    }

    private void InitializeFormNotificationTemplate(
      Guid templateId,
      IMessageTemplateRequest messageTemplate,
      Action<FormsConfig, Guid> updateAction)
    {
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      INotificationService notificationService = SystemManager.GetNotificationService();
      if (templateId != Guid.Empty)
      {
        try
        {
          notificationService.UpdateMessageTemplate(serviceContext, templateId, messageTemplate);
        }
        catch
        {
          templateId = Guid.Empty;
        }
      }
      if (templateId == Guid.Empty)
        templateId = notificationService.CreateMessageTemplate(serviceContext, messageTemplate);
      Telerik.Sitefinity.Configuration.Config.UpdateSection<FormsConfig>((Action<FormsConfig>) (formsConfig => updateAction(formsConfig, templateId)));
    }

    private void AddSortingExpressionForm(
      ConfigElementList<SortingExpressionElement> sortingExpressions,
      Type contentType)
    {
      ConfigElementList<SortingExpressionElement> configElementList1 = sortingExpressions;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "NewModifiedFirst";
      element1.ResourceClassId = typeof (FormsResources).Name;
      element1.SortingExpression = "LastModified DESC";
      configElementList1.Add(element1);
      ConfigElementList<SortingExpressionElement> configElementList2 = sortingExpressions;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "NewCreatedFirst";
      element2.ResourceClassId = typeof (FormsResources).Name;
      element2.SortingExpression = "DateCreated DESC";
      configElementList2.Add(element2);
      ConfigElementList<SortingExpressionElement> configElementList3 = sortingExpressions;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element3.ContentType = contentType.FullName;
      element3.SortingExpressionTitle = "ByTitleAsc";
      element3.ResourceClassId = typeof (FormsResources).Name;
      element3.SortingExpression = "Title ASC";
      configElementList3.Add(element3);
      ConfigElementList<SortingExpressionElement> configElementList4 = sortingExpressions;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element4.ContentType = contentType.FullName;
      element4.SortingExpressionTitle = "ByTitleDesc";
      element4.ResourceClassId = typeof (FormsResources).Name;
      element4.SortingExpression = "Title DESC";
      configElementList4.Add(element4);
    }

    private void AddSortingExpression(
      ConfigElementList<SortingExpressionElement> sortingExpressions,
      Type contentType)
    {
      ConfigElementList<SortingExpressionElement> configElementList1 = sortingExpressions;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "FirstSubmittedOnTop";
      element1.ResourceClassId = typeof (FormsResources).Name;
      element1.SortingExpression = "SubmittedOn ASC";
      configElementList1.Add(element1);
      ConfigElementList<SortingExpressionElement> configElementList2 = sortingExpressions;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "LastSubmittedOnTop";
      element2.ResourceClassId = typeof (FormsResources).Name;
      element2.SortingExpression = "SubmittedOn DESC";
      configElementList2.Add(element2);
    }

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      base.Upgrade(initializer, upgradeFrom);
      if (upgradeFrom.Build <= 1310)
      {
        bool suppressSecurityChecks = initializer.PageManager.Provider.SuppressSecurityChecks;
        initializer.PageManager.Provider.SuppressSecurityChecks = true;
        PageNode pageNode = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == FormsModule.EntriesPageID)).SingleOrDefault<PageNode>();
        if (pageNode != null)
        {
          Res.SetLstring(pageNode.Title, FormsModule.ResourceClassId, "FormsResponses");
          Res.SetLstring(pageNode.Page.HtmlTitle, FormsModule.ResourceClassId, "ResponsesHtmlTitle");
        }
        initializer.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      if (upgradeFrom.Build <= 1600)
      {
        bool suppressSecurityChecks = initializer.PageManager.Provider.SuppressSecurityChecks;
        initializer.PageManager.Provider.SuppressSecurityChecks = true;
        PageNode pageNode = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == FormsModule.HomePageId)).SingleOrDefault<PageNode>();
        if (pageNode != null)
          pageNode.RenderAsLink = true;
        initializer.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      if (upgradeFrom.Major == 4 && upgradeFrom.Minor < 2)
      {
        ConfigElementDictionary<string, DataProviderSettings> providers = Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>().Providers;
        LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<FormDescription, FormsManager>(initializer, providers.Select<KeyValuePair<string, DataProviderSettings>, DataProviderSettings>((Func<KeyValuePair<string, DataProviderSettings>, DataProviderSettings>) (kvp => kvp.Value)));
      }
      if (upgradeFrom.Build < 3040)
        initializer.Installer.PageToolbox().ContentSection().LoadOrAddWidget<FormsControl>("FormsControl").SetTitle("FormsControlTitle").SetDescription("FormsControlDescription").LocalizeUsing<FormsResources>().SetCssClass("sfFormsIcn");
      if (upgradeFrom.Build <= 3900)
        this.InitializeFormSortingExpressionSettings(initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings);
      if (upgradeFrom < SitefinityVersion.Sitefinity7_0)
      {
        if (ManagerBase<FormsDataProvider>.StaticProvidersCollection != null && ManagerBase<FormsDataProvider>.StaticProvidersCollection.Count > 0)
        {
          ManagerBase<FormsDataProvider>.StaticProvidersCollection.ToList<FormsDataProvider>().ForEach((Action<FormsDataProvider>) (p =>
          {
            using (FormsManager managerInTransaction = initializer.GetManagerInTransaction<FormsManager>(p.Name))
              FormsModule.UpgradeFormEnrties(managerInTransaction);
          }));
        }
        else
        {
          using (FormsManager managerInTransaction = initializer.GetManagerInTransaction<FormsManager>())
            FormsModule.UpgradeFormEnrties(managerInTransaction);
        }
      }
      if (!(upgradeFrom < SitefinityVersion.Sitefinity8_0))
        return;
      initializer.AddSupportedPermissionSetsToSecurityRoot<FormsManager>("SitemapGeneration");
      initializer.RemoveSupportedPermissionSetsToSecurityRoot<FormsManager>("Comments");
    }

    private static void UpgradeFormEnrties(FormsManager manager)
    {
      IQueryable<FormDescription> forms = manager.GetForms();
      if (forms == null)
        return;
      forms.ToList<FormDescription>().ForEach((Action<FormDescription>) (f =>
      {
        if (f.FormEntriesSeed <= 0L)
          return;
        IQueryable<FormEntry> formEntries = manager.GetFormEntries(f);
        if (formEntries == null)
          return;
        formEntries.ToList<FormEntry>().ForEach((Action<FormEntry>) (e =>
        {
          Telerik.Sitefinity.Multisite.ISite site = (Telerik.Sitefinity.Multisite.ISite) null;
          // ISSUE: reference to a compiler-generated field
          Guid siteId = manager.GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == this.f.Id)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).FirstOrDefault<Guid>();
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null)
            site = multisiteContext.GetSiteById(siteId);
          if (site == null)
            site = SystemManager.CurrentContext.CurrentSite;
          e.SourceSiteId = site.Id;
          e.SourceSiteName = site.Name;
          manager.ResolveFormEntrySourceSiteName(e);
        }));
      }));
    }

    [UpgradeInfo(Description = "Transferring referral code seeds from form descriptions to counters.", FailMassage = "Failed to tranfer referral code seeds from form descriptions to counters.", Id = "8C302F8D-1CE2-40E6-A8E3-FFA317CB0399", UpgradeTo = 6000)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
    private void UpgradeFormEntrySeeds(SiteInitializer initializer)
    {
      FormsManager manager = FormsManager.GetManager((string) null, nameof (UpgradeFormEntrySeeds));
      List<FormDescription> forms = (List<FormDescription>) null;
      while (forms == null || forms.Count > 0)
        manager.ExecuteAndCommitWithRetries((Action) (() =>
        {
          forms = manager.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.FormEntriesSeed > 0L)).Take<FormDescription>(200).ToList<FormDescription>();
          Dictionary<string, long> dictionary = forms.ToDictionary<FormDescription, string, long>((Func<FormDescription, string>) (f => f.EntriesTypeName), (Func<FormDescription, long>) (f => f.FormEntriesSeed + 1L));
          forms.ForEach((Action<FormDescription>) (f => f.FormEntriesSeed = 0L));
          if (manager.Provider is OpenAccessFormsProvider provider2 && provider2.CounterDecorator is OpenAccessCounterDecorator)
          {
            ((OpenAccessCounterDecorator) provider2.CounterDecorator).InitMultipleCounters((IDictionary<string, long>) dictionary);
          }
          else
          {
            foreach (FormDescription formDescription in forms)
              manager.Provider.SetNextReferralCode(formDescription.EntriesTypeName, formDescription.FormEntriesSeed + 1L);
          }
        }), 10);
    }

    [UpgradeInfo(Description = "Update forms email notifications templates.", FailMassage = "Failed to update forms email notifications templates.", Id = "A6329930-0881-45DD-A20E-61ECB2958D15", UpgradeTo = 7100)]
    private void UpdateFormsEmailNotificationsTemplates(SiteInitializer initializer)
    {
      FormsConfig formsConfig = initializer.Context.GetConfig<FormsConfig>();
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      IMessageTemplateResponse template1 = notificationService.GetMessageTemplates(serviceContext, (QueryParameters) null).Where<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (p => p.Id == formsConfig.Notifications.FormEntrySubmittedNotificationTemplateId)).FirstOrDefault<IMessageTemplateResponse>();
      if (template1 != null)
      {
        NewFormResponseMessageTemplate responseMessageTemplate = new NewFormResponseMessageTemplate();
        IMessageTemplateRequest defaultMessageTemplate = responseMessageTemplate.GetDefaultMessageTemplate();
        if (template1.BodyHtml != defaultMessageTemplate.BodyHtml || template1.Subject != defaultMessageTemplate.Subject)
        {
          template1.ResolveKey = responseMessageTemplate.GetKey();
          notificationService.UpdateMessageTemplate(serviceContext, template1.Id, (IMessageTemplateRequest) template1);
        }
      }
      IMessageTemplateResponse template2 = notificationService.GetMessageTemplates(serviceContext, (QueryParameters) null).Where<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (p => p.Id == formsConfig.Notifications.UpdatedFormEntrySubmittedNotificationTemplateId)).FirstOrDefault<IMessageTemplateResponse>();
      if (template2 == null)
        return;
      ModifiedFormResponseMessageTemplate responseMessageTemplate1 = new ModifiedFormResponseMessageTemplate();
      IMessageTemplateRequest defaultMessageTemplate1 = responseMessageTemplate1.GetDefaultMessageTemplate();
      if (!(template2.BodyHtml != defaultMessageTemplate1.BodyHtml) && !(template2.Subject != defaultMessageTemplate1.Subject))
        return;
      template2.ResolveKey = responseMessageTemplate1.GetKey();
      notificationService.UpdateMessageTemplate(serviceContext, template2.Id, (IMessageTemplateRequest) template2);
    }

    [UpgradeInfo(Description = "Update forms subscribers.", FailMassage = "Failed to update forms subscribers.", Id = "A6321930-0881-45DD-A20E-61ECB2938D15", UpgradeTo = 7100)]
    private void UpdateFormsSubscribers(SiteInitializer initializer)
    {
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      foreach (FormDescription form in (IEnumerable<FormDescription>) FormsManager.GetManager().GetForms())
      {
        foreach (ISubscriberResponse subscriberResponse in notificationService.GetSubscribers(serviceContext, form.SubscriptionListId, (QueryParameters) null).Concat<ISubscriberResponse>(notificationService.GetSubscribers(serviceContext, form.SubscriptionListIdAfterFormUpdate, (QueryParameters) null)).Where<ISubscriberResponse>((Func<ISubscriberResponse, bool>) (p => p.Email != p.ResolveKey)))
          notificationService.UpdateSubscriber(serviceContext, subscriberResponse.Id, (ISubscriberRequest) new SubscriberRequestProxy()
          {
            CustomProperties = subscriberResponse.CustomProperties,
            Disabled = subscriberResponse.Disabled,
            FirstName = subscriberResponse.FirstName,
            LastName = subscriberResponse.LastName,
            Email = subscriberResponse.Email,
            ResolveKey = subscriberResponse.Email
          });
      }
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CAF-01345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    IDictionary<string, ITypeSettings> ITypeSettingsProvider.GetTypeSettings()
    {
      ITypeSettings typeSettings = ContractFactory.Instance.Create(typeof (FormDraft), "form-drafts");
      List<IPropertyMapping> propertyMappingList1 = new List<IPropertyMapping>();
      List<IPropertyMapping> propertyMappingList2 = propertyMappingList1;
      PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
      propertyMappingProxy1.Name = LinqHelper.MemberName<FormDraft>((Expression<Func<FormDraft, object>>) (x => x.Name));
      propertyMappingProxy1.ReadOnly = true;
      PersistentPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
      propertyMappingList2.Add((IPropertyMapping) propertyMappingProxy2);
      List<IPropertyMapping> propertyMappingList3 = propertyMappingList1;
      PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
      propertyMappingProxy3.Name = LinqHelper.MemberName<FormDraft>((Expression<Func<FormDraft, object>>) (x => x.Rules));
      PersistentPropertyMappingProxy propertyMappingProxy4 = propertyMappingProxy3;
      propertyMappingList3.Add((IPropertyMapping) propertyMappingProxy4);
      foreach (IPropertyMapping propertyMapping in propertyMappingList1)
        typeSettings.Properties.Add(propertyMapping);
      CalculatedPropertyMappingProxy propertyMappingProxy5 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy5.Name = "Fields";
      propertyMappingProxy5.ResolverType = typeof (FormFieldsProperty).FullName;
      CalculatedPropertyMappingProxy propertyMappingProxy6 = propertyMappingProxy5;
      typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy6);
      CalculatedPropertyMappingProxy propertyMappingProxy7 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy7.Name = "Title";
      propertyMappingProxy7.ResolverType = typeof (FormTitleProperty).FullName;
      CalculatedPropertyMappingProxy propertyMappingProxy8 = propertyMappingProxy7;
      typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy8);
      CalculatedPropertyMappingProxy propertyMappingProxy9 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy9.Name = "AvailableActions";
      propertyMappingProxy9.ResolverType = typeof (FormAvailableActionsProperty).FullName;
      CalculatedPropertyMappingProxy propertyMappingProxy10 = propertyMappingProxy9;
      typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy10);
      CalculatedPropertyMappingProxy propertyMappingProxy11 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy11.Name = "Steps";
      propertyMappingProxy11.ResolverType = typeof (FormStepsProperty).FullName;
      CalculatedPropertyMappingProxy propertyMappingProxy12 = propertyMappingProxy11;
      typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy12);
      return (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>()
      {
        {
          typeSettings.ClrType,
          typeSettings
        }
      };
    }

    /// <inheritdoc />
    public IEnumerable<IActionMessageTemplate> GetActionMessageTemplates()
    {
      if (FormsModule.actionMessageTemplates == null)
        FormsModule.actionMessageTemplates = (IEnumerable<IActionMessageTemplate>) new IActionMessageTemplate[3]
        {
          (IActionMessageTemplate) new NewFormResponseMessageTemplate(),
          (IActionMessageTemplate) new ModifiedFormResponseMessageTemplate(),
          (IActionMessageTemplate) new FormConfirmationMessageTemplate()
        };
      return FormsModule.actionMessageTemplates;
    }

    /// <inheritdoc />
    public object GetReport() => (object) new FormsModuleReporter().GetReport();

    /// <summary>Gets the service context.</summary>
    /// <returns>Service context</returns>
    internal static ServiceContext GetServiceContext() => new ServiceContext("ThisApplicationKey", "Forms");

    private void SubscribeToFormSubmission() => EventHub.Subscribe<IFormEntryCreatedEvent>(new SitefinityEventHandler<IFormEntryCreatedEvent>(this.FormSubmitted));

    private void UnsubscribeFromFormSubmission() => EventHub.Unsubscribe<IFormEntryCreatedEvent>(new SitefinityEventHandler<IFormEntryCreatedEvent>(this.FormSubmitted));

    private void SubscribeToFormEntryUpdateSubmission() => EventHub.Subscribe<IFormEntryUpdatedEvent>(new SitefinityEventHandler<IFormEntryUpdatedEvent>(this.FormSubmitted));

    /// <summary>
    /// Method will register a module for subscriber's synchronization when an user is changed, deleted, deactivated.
    /// </summary>
    private void RegisterModuleForSubscriberSyncronization() => ObjectFactory.Resolve<INotificationSubscriptionSynchronizer>().Register(FormsModule.GetServiceContext());

    private void FormSubmitted(IFormEntryEvent evt) => this.SendNotification(evt);

    /// <summary>Sends notification</summary>
    /// <param name="evt">The form entry event</param>
    protected virtual void SendNotification(IFormEntryEvent evt)
    {
      if (evt == null || !Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>().Notifications.Enabled)
        return;
      IDictionary<string, string> notificationTemplateItems = this.GetNotificationTemplateItems(evt);
      if (evt.FormSubscriptionListId != Guid.Empty || evt.NotificationEmails != null && evt.NotificationEmails.Count<string>() > 0)
        this.SendFormSubscribersNotification(evt, notificationTemplateItems);
      if (!evt.SendConfirmationEmail || !(evt is IFormEntryCreatedEvent))
        return;
      this.SendConfirmationEmail(evt, notificationTemplateItems);
    }

    private void SendFormSubscribersNotification(
      IFormEntryEvent evt,
      IDictionary<string, string> templateItems)
    {
      FormsConfig formsConfig = Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      IMessageTemplateRequest messageTemplateRequest;
      switch (evt)
      {
        case IFormEntryCreatedEvent _:
          messageTemplateRequest = this.ResolveTemplate((IActionMessageTemplate) new NewFormResponseMessageTemplate(), evt.FormId);
          break;
        case IFormEntryUpdatedEvent _:
          messageTemplateRequest = this.ResolveTemplate((IActionMessageTemplate) new ModifiedFormResponseMessageTemplate(), evt.FormId);
          break;
        default:
          return;
      }
      INotificationService notificationService = SystemManager.GetNotificationService();
      if (evt.FormSubscriptionListId != Guid.Empty)
      {
        MessageJobRequestProxy messageJob = new MessageJobRequestProxy()
        {
          SubscriptionsListId = evt.FormSubscriptionListId,
          MessageTemplate = messageTemplateRequest,
          Description = string.Format("Form entry submission email notification for form '{0}'", (object) evt.FormTitle),
          SenderProfileName = formsConfig.Notifications.SenderProfile
        };
        notificationService.SendMessage(serviceContext, (IMessageJobRequest) messageJob, templateItems);
      }
      if (evt.NotificationEmails == null || evt.NotificationEmails.Count<string>() <= 0)
        return;
      IEnumerable<SubscriberRequestProxy> subscriberRequestProxies = evt.NotificationEmails.Select<string, SubscriberRequestProxy>((Func<string, SubscriberRequestProxy>) (p => new SubscriberRequestProxy()
      {
        Email = p
      }));
      MessageJobRequestProxy messageJob1 = new MessageJobRequestProxy()
      {
        Subscribers = (IEnumerable<ISubscriberRequest>) subscriberRequestProxies,
        MessageTemplate = messageTemplateRequest,
        Description = string.Format("Form entry submission email notification for form '{0}'", (object) evt.FormTitle),
        SenderProfileName = formsConfig.Notifications.SenderProfile
      };
      notificationService.SendMessage(serviceContext, (IMessageJobRequest) messageJob1, templateItems);
    }

    private void SendConfirmationEmail(
      IFormEntryEvent evt,
      IDictionary<string, string> templateItems)
    {
      FormsManager formsManager = FormsManager.GetManager();
      IEnumerable<SubscriberRequestProxy> source = formsManager.GetForm(evt.FormId).Controls.Select(p => new
      {
        Id = p.Id,
        Control = ControlUtilities.BehaviorResolver.GetBehaviorObject(formsManager.LoadControl((ObjectData) p, (CultureInfo) null))
      }).Where(p => p.Control.GetType().Name == "EmailTextFieldController" || p.Control.GetType().Name == "FormEmailTextBox").Select(p => FormsModule.GetStringValue(evt.Controls.First<IFormEntryEventControl>((Func<IFormEntryEventControl, bool>) (c => c.Id == p.Id)).Value).ToLower()).Distinct<string>().Where<string>((Func<string, bool>) (p => !p.IsNullOrEmpty())).Select<string, SubscriberRequestProxy>((Func<string, SubscriberRequestProxy>) (p => new SubscriberRequestProxy()
      {
        Email = p
      }));
      if (!source.Any<SubscriberRequestProxy>())
        return;
      INotificationService notificationService = SystemManager.GetNotificationService();
      FormsConfig formsConfig = Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      IMessageTemplateRequest messageTemplateRequest = this.ResolveTemplate((IActionMessageTemplate) new FormConfirmationMessageTemplate(), evt.FormId);
      MessageJobRequestProxy messageJobRequestProxy = new MessageJobRequestProxy()
      {
        Subscribers = (IEnumerable<ISubscriberRequest>) source,
        MessageTemplate = messageTemplateRequest,
        Description = string.Format("Confirmation email to the user who submitted the form '{0}'", (object) evt.FormTitle),
        SenderProfileName = formsConfig.Notifications.SenderProfile
      };
      ServiceContext context = serviceContext;
      MessageJobRequestProxy messageJob = messageJobRequestProxy;
      IDictionary<string, string> contextItems = templateItems;
      notificationService.SendMessage(context, (IMessageJobRequest) messageJob, contextItems);
    }

    private IMessageTemplateRequest ResolveTemplate(
      IActionMessageTemplate actionMessageTemplate,
      Guid formId)
    {
      INotificationService notificationService = SystemManager.GetNotificationService();
      actionMessageTemplate.ApplyVariations(("parentid", formId.ToString()), ("language", SystemManager.CurrentContext.Culture.Name));
      IMessageTemplateRequest messageTemplateRequest = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate((ServiceContext) null, actionMessageTemplate.GetKey());
      if (messageTemplateRequest == null)
      {
        actionMessageTemplate.ApplyVariations(("siteid", SystemManager.CurrentContext.CurrentSite.Id.ToString()), ("parentid", (string) null), ("language", (string) null));
        messageTemplateRequest = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate((ServiceContext) null, actionMessageTemplate.GetKey());
      }
      if (messageTemplateRequest == null)
      {
        actionMessageTemplate.ApplyVariations(("siteid", (string) null));
        messageTemplateRequest = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate((ServiceContext) null, actionMessageTemplate.GetKey());
      }
      if (messageTemplateRequest == null)
        messageTemplateRequest = actionMessageTemplate.GetDefaultMessageTemplate();
      return messageTemplateRequest;
    }

    private IDictionary<string, string> GetNotificationTemplateItems(IFormEntryEvent evt)
    {
      string domainUrl = UrlPath.GetDomainUrl();
      string name = SystemManager.CurrentContext.CurrentSite.Name;
      string str1 = string.Empty;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null && currentHttpContext.Request != null)
      {
        string absoluteUri = currentHttpContext.Request.Url.AbsoluteUri;
        str1 = !absoluteUri.Contains("/Sitefinity/Services/Forms/FormsService.svc/entry") ? (currentHttpContext.Request.UrlReferrer != (Uri) null ? currentHttpContext.Request.UrlReferrer.AbsoluteUri : absoluteUri) : string.Format("{0}/Sitefinity/Content/Forms/Entries/{1}?sf_site={2}", (object) domainUrl, (object) evt.FormName, (object) SystemManager.CurrentContext.CurrentSite.Id);
      }
      string str2 = string.Format("{0}/Sitefinity/Content/Forms/Entries/{1}?entryid={2}&sf_site={3}", (object) domainUrl, (object) evt.FormName, (object) evt.EntryId, (object) SystemManager.CurrentContext.CurrentSite.Id);
      string username = evt.Username;
      if (string.IsNullOrWhiteSpace(username))
        username = Res.Get<FormsResources>("AnonymousUsername");
      Dictionary<string, string> notificationTemplateItems = new Dictionary<string, string>();
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormProjectName.FieldName, name);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormHost.FieldName, UrlPath.GetDomainUrl() + "/" + name.TrimStart('/'));
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormUrl.FieldName, str1);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormSiteUrl.FieldName, SystemManager.CurrentContext.CurrentSite.LiveUrl);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormLogoUrl.FieldName, domainUrl + "/SFRes/Images/Telerik.Sitefinity.Resources/Images.Sitefinity_Primary.png");
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormTitle.FieldName, evt.FormTitle);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormEntryUrl.FieldName, str2);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormEntryReferralCode.FieldName, evt.ReferralCode);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormUsername.FieldName, username);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormIpAddress.FieldName, evt.IpAddress);
      notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormSubmittedOn.FieldName, evt.SubmissionTime.ToSitefinityUITime().ToString("dd MMMM yyyy, hh:mm tt"));
      foreach (IFormEntryEventControl entryEventControl in evt.Controls.Where<IFormEntryEventControl>((Func<IFormEntryEventControl, bool>) (p => p.Type == FormEntryEventControlType.FieldControl || p.Type == FormEntryEventControlType.FileFieldControl)))
      {
        string key = string.Empty;
        string str3 = string.Empty;
        if (entryEventControl is IFormEntryEventClientControl eventClientControl)
          key = FormsModule.GetControlPlaceholderField(eventClientControl.ControlType + eventClientControl.ClientID, entryEventControl.Title).FieldName;
        if (entryEventControl.Type == FormEntryEventControlType.FieldControl)
          str3 = FormsModule.GetStringValue(entryEventControl.Value);
        else if (entryEventControl.Type == FormEntryEventControlType.FileFieldControl && entryEventControl.Value is IEnumerable<ContentLink> contentLinks)
          str3 = FormsModule.GetContentLinksHtml(contentLinks);
        notificationTemplateItems.Add(key, str3);
      }
      if (Telerik.Sitefinity.Configuration.Config.Get<FormsConfig>().Notifications.EnableDetailedNotificationMessage && evt.Controls.Any<IFormEntryEventControl>())
      {
        string str4 = evt.ExecuteByFormEntryEventType<string>((Func<string>) (() => this.GetNotificationControlsHtml(evt.Controls)), (Func<string>) (() => this.GetUpdateNotificationControlsHtml(evt.Controls)));
        notificationTemplateItems.Add(FormActionMessageTemplate.PlaceholderFields.FormFields.FieldName, str4);
      }
      return (IDictionary<string, string>) notificationTemplateItems;
    }

    protected virtual string GetNotificationControlsHtml(
      IEnumerable<IFormEntryEventControl> controls)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (controls != null)
      {
        stringBuilder.Append("<table><tr><td width=\"596\" style=\"596px; padding: 0; border-top: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; border-right: 1px solid #e4e4e4; border-bottom: 1px solid #ffffff;\"><table width=\"596\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width: 596px; margin: 0 auto; padding: 0; background-color: #fff; border: 0; \">");
        foreach (IFormEntryEventControl control in controls)
        {
          switch (control.Type)
          {
            case FormEntryEventControlType.FieldControl:
            case FormEntryEventControlType.FileFieldControl:
              stringBuilder.AppendFormat("<tr><td width=\"100\" bgcolor=\"#f2f2f2\" border=\"0\" style=\"width: 100px; padding: 10px 9px; border: 0; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 13px; font-weight: normal; font-style: normal; text-align: left; background-color: #f2f2f2; vertical-align: top; \">{0}</td>", (object) (control.Title ?? ""));
              stringBuilder.Append("<td width=\"459\" border=\"0\" style=\"width: 459px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 13px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">");
              string str = "";
              if (control.Type == FormEntryEventControlType.FieldControl)
                str = FormsModule.GetStringValue(control.Value);
              else if (control.Value is IEnumerable<ContentLink> contentLinks)
                str = FormsModule.GetContentLinksHtml(contentLinks);
              stringBuilder.Append(str);
              stringBuilder.Append("</td></tr>");
              continue;
            case FormEntryEventControlType.SectionHeader:
              stringBuilder.AppendFormat("<tr><td colspan=\"2\" width=\"578\" style=\"width: 578px; padding: 12px 9px 12px; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 15px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">{0}</td></tr>", (object) control.Title);
              continue;
            case FormEntryEventControlType.InstructionalText:
              stringBuilder.AppendFormat("<tr><td colspan=\"2\" width=\"578\" style=\"width: 578px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: italic; color: #666; text-align: left; vertical-align: top; \">{0}</td></tr>", (object) control.Title);
              continue;
            default:
              continue;
          }
        }
        stringBuilder.Append("</table></td></tr></table>");
      }
      return stringBuilder.ToString();
    }

    protected virtual string GetUpdateNotificationControlsHtml(
      IEnumerable<IFormEntryEventControl> controls)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (controls != null)
      {
        stringBuilder.Append("<table><tr><td width=\"596\" style=\"596px; padding: 0; border-top: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; border-right: 1px solid #e4e4e4; border-bottom: 1px solid #ffffff;\"><table width=\"596\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width: 596px; margin: 0 auto; padding: 0; background-color: #fff; border: 0; \">");
        foreach (IFormEntryEventControl control in controls)
        {
          switch (control.Type)
          {
            case FormEntryEventControlType.FieldControl:
            case FormEntryEventControlType.FileFieldControl:
              stringBuilder.AppendFormat("<tr><td width=\"100\" bgcolor=\"#f2f2f2\" border=\"0\" style=\"width: 100px; padding: 10px 9px; border: 0; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 13px; font-weight: normal; font-style: normal; text-align: left; background-color: #f2f2f2; vertical-align: top; \">{0}</td>", (object) (control.Title ?? ""));
              if (control is IFormEntryOldValueControl entryOldValueControl)
              {
                string fieldValueToString1 = FormsModule.GetFieldValueToString(control.Type, entryOldValueControl.OldValue);
                string fieldValueToString2 = FormsModule.GetFieldValueToString(control.Type, control.Value);
                if (fieldValueToString1 != fieldValueToString2)
                {
                  stringBuilder.Append("<td width=\"220\" style=\"width: 220px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; text-decoration:line-through; color:#FF0000; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">");
                  stringBuilder.Append(fieldValueToString1);
                  stringBuilder.Append("</td>");
                  stringBuilder.Append("<td width=\"220\" style=\"width: 220px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; color:#379957; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">");
                  stringBuilder.Append(fieldValueToString2);
                  stringBuilder.Append("</td>");
                }
                else
                {
                  stringBuilder.Append("<td width=\"220\" style=\"width: 220px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">");
                  stringBuilder.Append(fieldValueToString1);
                  stringBuilder.Append("</td>");
                  stringBuilder.Append("<td width=\"220\" style=\"width: 220px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; border-left: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">");
                  stringBuilder.Append("&nbsp;");
                  stringBuilder.Append("</td>");
                }
                stringBuilder.Append("</tr>");
                continue;
              }
              continue;
            case FormEntryEventControlType.SectionHeader:
              stringBuilder.AppendFormat("<tr><td colspan=\"3\" width=\"578\" style=\"width: 578px; padding: 12px 9px 12px; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 15px; font-weight: normal; font-style: normal; text-align: left; vertical-align: top; \">{0}</td></tr>", (object) control.Title);
              continue;
            case FormEntryEventControlType.InstructionalText:
              stringBuilder.AppendFormat("<tr><tr><td colspan=\"3\" width=\"578\" style=\"width: 578px; padding: 10px 9px; border-bottom: 1px solid #e4e4e4; font-family: arial,verdana,sans-serif; line-height: 1.2; font-size: 12px; font-weight: normal; font-style: italic; color: #666; text-align: left; vertical-align: top; \">{0}</td></tr>", (object) control.Title);
              continue;
            default:
              continue;
          }
        }
        stringBuilder.Append("</table></td></tr></table>");
      }
      return stringBuilder.ToString();
    }

    internal static PlaceholderField GetControlPlaceholderField(
      string fieldName,
      string title)
    {
      return new PlaceholderField(fieldName.Replace(" ", "") + "." + title?.Replace(" ", "_"), title ?? fieldName);
    }

    private void InitializeNotificationSubscribtion()
    {
      this.SubscribeToFormSubmission();
      this.SubscribeToFormEntryUpdateSubmission();
    }

    private static string GetFieldValueToString(FormEntryEventControlType type, object controlValue)
    {
      if (controlValue == null)
        return string.Empty;
      if (type == FormEntryEventControlType.FieldControl)
        return FormsModule.GetStringValue(controlValue);
      return controlValue is IEnumerable<ContentLink> contentLinks ? FormsModule.GetContentLinksHtml(contentLinks) : string.Empty;
    }

    private static string GetStringValue(object value)
    {
      if (value == null)
        return string.Empty;
      if (!(value is string stringValue))
      {
        TypeConverter converter = TypeDescriptor.GetConverter(value);
        stringValue = !converter.CanConvertTo(typeof (string)) ? value.ToString() : (string) converter.ConvertTo(value, typeof (string));
      }
      return stringValue;
    }

    private static string GetContentLinksHtml(IEnumerable<ContentLink> contentLinks)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ContentLink contentLink in contentLinks)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(", ");
        string itemAdditionalInfo = contentLink.ChildItemAdditionalInfo;
        string[] source = contentLink.ChildItemAdditionalInfo.Split(new string[1]
        {
          "|"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (((IEnumerable<string>) source).Any<string>())
        {
          string s = source[0];
          UriBuilder uriBuilder = new UriBuilder(source[1]);
          stringBuilder.AppendFormat("<a href=\"{0}\">{1}</a>", (object) uriBuilder.Uri.ToString(), (object) HttpUtility.HtmlEncode(s));
        }
      }
      return stringBuilder.ToString();
    }

    private void RegisterIoCTypes()
    {
      ObjectFactory.Container.RegisterType<IFormFieldBackendConfigurator, DefaultFormFieldBackendConfigurator>();
      ObjectFactory.Container.RegisterType<IFormMultipageDecorator, FormMultipageDecorator>();
      ObjectFactory.Container.RegisterType<IContentTransfer, FormsContentTransfer>(new FormsContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType<IPreviewFormDecorator, PreviewFormDecorator>((LifetimeManager) new ContainerControlledLifetimeManager());
    }

    public void ConfigurePublishing()
    {
      SitefinityContentPipeSettings pipeSettings = (SitefinityContentPipeSettings) PublishingSystemFactory.GetPipeSettings("FormInboundPipe");
      pipeSettings.ContentTypeName = typeof (FormDescription).FullName;
      pipeSettings.MaxItems = 0;
      PublishingSystemFactory.RegisterTemplatePipe("BackendSearchItemTemplate", (PipeSettings) pipeSettings, (Predicate<PipeSettings>) (ps => ps is SitefinityContentPipeSettings && (ps as SitefinityContentPipeSettings).ContentTypeName == typeof (FormDescription).FullName));
    }
  }
}
