// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Events;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation.Pipes;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Publishing.Twitter.Services;
using Telerik.Sitefinity.Publishing.Web;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Publishing module - maintains initialization and installation
  /// </summary>
  public class PublishingModule : SecuredModuleBase
  {
    /// <summary>Publishing module name</summary>
    public const string ModuleName = "Publishing";
    public const string PublishingDataServiceUrl = "Services/Content/PublishingDataService.svc";
    /// <summary>Page group taxon for the publishing module</summary>
    public static readonly Guid PublishingPageGroupId = new Guid("A9186EFA-7BC2-43B6-8523-D3CABA94C736");
    /// <summary>ID of the landing page for the publishing module</summary>
    public static readonly Guid HomePageId = new Guid("53E0B648-D59D-4418-836E-4924C48153AC");
    /// <summary>
    /// ID of the resource class used for localization of messages
    /// </summary>
    public static readonly string ResourceClassId = typeof (PublishingMessages).Name;
    private readonly Type[] managers = new Type[1]
    {
      typeof (PublishingManager)
    };

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => this.managers;

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => PublishingModule.HomePageId;

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartup;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      SystemManager.RegisterRoute("Feeds", (RouteBase) new Route(Config.Get<PublishingConfig>().FeedsBaseURl + "/{*" + PublishingRouteHandler.FeedUrlName + "}", (IRouteHandler) new PublishingRouteHandler()), "Publishing", false);
      Config.RegisterSection<PublishingConfig>();
      Config.RegisterSection<PublishingDataItemsConfig>();
      PublishingModule.OnConfiguring((object) this, (EventArgs) null);
      this.RegisterDefaultPipeSettingsAndMappings();
      this.RegisterPipes();
      PublishingModule.RegisterTranslators();
      this.RegisterPublishingPointBusinessObjects();
      this.RegisterTemplatePipes();
      this.RegisterPipeDefinitions();
      this.RegisterPublishingPointMetaFields();
      SystemManager.ModelReset -= new EventHandler<EventArgs>(this.SystemManager_ModelReset);
      SystemManager.ModelReset += new EventHandler<EventArgs>(this.SystemManager_ModelReset);
      Config.RegisterSection<TwitterConfig>();
      ObjectFactory.RegisterSitemapNodeFilter<PublishingNodeFilter>("Publishing");
      App.WorkWith().Module("Publishing").Initialize().WebService<PublishingPointDataService>("Services/Content/PublishingDataService.svc");
      this.Subscribe();
      Bootstrapper.Initialized -= new EventHandler<ExecutedEventArgs>(this.Bootstrapper_Initialized);
      Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(this.Bootstrapper_Initialized);
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (TwitterFeedWidget), typeof (DynamicTypeBase));
    }

    protected void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
    {
      if (!(e.CommandName == "Bootstrapped"))
        return;
      foreach (IModule module in (IEnumerable<IModule>) SystemManager.ApplicationModules.Values)
      {
        if (module is IPublishingEnabledModule publishingEnabledModule)
        {
          try
          {
            publishingEnabledModule.ConfigurePublishing();
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw ex;
          }
        }
      }
      PublishingModule.OnConfigured((object) this, (EventArgs) null);
      Bootstrapper.Initialized -= new EventHandler<ExecutedEventArgs>(this.Bootstrapper_Initialized);
    }

    public static event EventHandler<EventArgs> Configuring;

    private static void OnConfiguring(object sender, EventArgs args)
    {
      if (PublishingModule.Configuring == null)
        return;
      PublishingModule.Configuring(sender, args);
    }

    public static event EventHandler<EventArgs> Configured;

    private static void OnConfigured(object sender, EventArgs args)
    {
      if (PublishingModule.Configured == null)
        return;
      PublishingModule.Configured(sender, args);
    }

    public void Subscribe()
    {
      EventHub.Subscribe<IDataEvent>(new SitefinityEventHandler<IDataEvent>(this.DataEventHandler));
      EventHub.Subscribe<ConfigEvent>(new SitefinityEventHandler<ConfigEvent>(this.ConfigEventHandler));
    }

    private void DataEventHandler(IDataEvent dataEvent)
    {
      if (!PublishingModule.EventFilter(dataEvent))
        return;
      PublishingManager.CallSubscribedPipes((Func<PublishingItemFilter, PublishingSystemEventInfo>) (filter => PublishingManager.GetPublishingSystemEventInfo(dataEvent, filter)));
    }

    private void ConfigEventHandler(ConfigEvent configEvent)
    {
      if (!Bootstrapper.IsReady)
        return;
      BackgroundTasksService backgroundTasksService = ObjectFactory.Resolve<BackgroundTasksService>();
      HttpContext context = HttpContext.Current;
      Action action = (Action) (() =>
      {
        using (new HttpRequestRegion(context.Request.Url.AbsoluteUri))
          PublishingManager.CallSubscribedPipes(closure_0 ?? (closure_0 = (Func<PublishingItemFilter, PublishingSystemEventInfo>) (filter => PublishingManager.GetPublishingSystemEventInfo(configEvent))));
      });
      backgroundTasksService.EnqueueTask(action, true);
    }

    internal static bool EventFilter(IDataEvent evt)
    {
      switch (evt)
      {
        case IPreProcessingEvent _:
        case IGeoLocatableEvent _:
          return false;
        case ILifecycleEvent lifecycleEvent:
          if (lifecycleEvent.Status == "Temp")
            return false;
          break;
      }
      return true;
    }

    protected void SystemManager_ModelReset(object sender, EventArgs e) => this.RegisterContentPipeDefinitions();

    private void RegisterPublishingPointBusinessObjects()
    {
      PublishingSystemFactory.RegisterPublishingPoint("PassThrough", typeof (PassThroughPublishingPoint));
      PublishingSystemFactory.RegisterPublishingPoint("Persistent", typeof (PersistentPublishingPoint));
    }

    internal static void RegisterTranslators()
    {
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new DateTimeTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new LstringTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new ConcatenationTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new TransparentTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new UserNameTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new UrlTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new TaxonomyTitleTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new RegExTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new DateTimeFromStringTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new UrlShortenerTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new HtmlStripperTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new EnumToIntTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new ToDecimalTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new ToLongTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new SynonymTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new SynonymFromStringTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new ItemToGuidTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new LanguageDataFromStringTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new LanguageDataTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new ContentIdentifiersTranslator());
      PipeTranslatorFactory.RegisterTranslator((TranslatorBase) new AuthorTranslator());
    }

    private void RegisterDefaultPipeSettingsAndMappings()
    {
      PipeSettings templatePipeSettings1 = (PipeSettings) PageInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("PagePipe", templatePipeSettings1);
      PublishingSystemFactory.RegisterPipeSettings("PagePipe", templatePipeSettings1);
      PublishingSystemFactory.RegisterPipeMappings("PagePipe", true, (IList<Mapping>) PageInboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings2 = (PipeSettings) TwitterFeedOutboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("Twitter", templatePipeSettings2);
      PublishingSystemFactory.RegisterPipeSettings("Twitter", templatePipeSettings2);
      PublishingSystemFactory.RegisterPipeMappings("Twitter", false, (IList<Mapping>) TwitterFeedOutboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings3 = (PipeSettings) TwitterInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("TwitterInboundPipe", templatePipeSettings3);
      PublishingSystemFactory.RegisterPipeSettings("TwitterInboundPipe", templatePipeSettings3);
      PublishingSystemFactory.RegisterPipeMappings("TwitterInboundPipe", true, (IList<Mapping>) TwitterInboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings4 = (PipeSettings) ContentInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("ContentInboundPipe", templatePipeSettings4);
      PublishingSystemFactory.RegisterPipeSettings("ContentInboundPipe", templatePipeSettings4);
      PublishingSystemFactory.RegisterPipeMappings("ContentInboundPipe", true, (IList<Mapping>) ContentInboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings5 = (PipeSettings) ContentOutboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("ContentOutboundPipe", templatePipeSettings5);
      PublishingSystemFactory.RegisterPipeSettings("ContentOutboundPipe", templatePipeSettings5);
      PublishingSystemFactory.RegisterPipeMappings("ContentOutboundPipe", false, (IList<Mapping>) ContentOutboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings6 = (PipeSettings) RSSInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("RSSInboundPipe", templatePipeSettings6);
      PublishingSystemFactory.RegisterPipeSettings("RSSInboundPipe", templatePipeSettings6);
      PublishingSystemFactory.RegisterPipeMappings("RSSInboundPipe", true, (IList<Mapping>) RSSInboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings7 = (PipeSettings) RSSOutboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("RSSOutboundPipe", templatePipeSettings7);
      PublishingSystemFactory.RegisterPipeSettings("RSSOutboundPipe", templatePipeSettings7);
      PublishingSystemFactory.RegisterPipeMappings("RSSOutboundPipe", false, (IList<Mapping>) RSSOutboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings8 = (PipeSettings) Pop3InboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("Pop3InboundPipe", templatePipeSettings8);
      PublishingSystemFactory.RegisterPipeSettings("Pop3InboundPipe", templatePipeSettings8);
      PublishingSystemFactory.RegisterPipeMappings("Pop3InboundPipe", true, (IList<Mapping>) Pop3InboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings9 = (PipeSettings) ConfigurationsInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("ConfigurationsInboundPipe", templatePipeSettings9);
      PublishingSystemFactory.RegisterPipeSettings("ConfigurationsInboundPipe", templatePipeSettings9);
      PublishingSystemFactory.RegisterPipeMappings("ConfigurationsInboundPipe", true, (IList<Mapping>) ConfigurationsInboundPipe.GetDefaultMappings());
      PipeSettings templatePipeSettings10 = (PipeSettings) FormInboundPipe.GetTemplatePipeSettings();
      PublishingSystemFactory.RegisterTemplatePipeSettings("FormInboundPipe", templatePipeSettings10);
      PublishingSystemFactory.RegisterPipeSettings("FormInboundPipe", templatePipeSettings10);
      PublishingSystemFactory.RegisterPipeMappings("FormInboundPipe", true, (IList<Mapping>) ContentInboundPipe.GetDefaultMappings());
    }

    private void RegisterPipes()
    {
      PublishingSystemFactory.RegisterPipe("ContentInboundPipe", typeof (ContentInboundPipe));
      PublishingSystemFactory.RegisterPipe("RSSOutboundPipe", typeof (RSSOutboundPipe));
      PublishingSystemFactory.RegisterPipe("ContentOutboundPipe", typeof (ContentOutboundPipe));
      PublishingSystemFactory.RegisterPipe("RSSInboundPipe", typeof (RSSInboundPipe));
      PublishingSystemFactory.RegisterPipe("Pop3InboundPipe", typeof (Pop3InboundPipe));
      PublishingSystemFactory.RegisterPipe("Twitter", typeof (TwitterFeedOutboundPipe));
      PublishingSystemFactory.RegisterPipe("TwitterInboundPipe", typeof (TwitterInboundPipe));
      PublishingSystemFactory.RegisterPipe("ConfigurationsInboundPipe", typeof (ConfigurationsInboundPipe));
      PublishingSystemFactory.RegisterPipe("FormInboundPipe", typeof (FormInboundPipe));
    }

    private void RegisterTemplatePipes()
    {
      PublishingSystemFactory.RegisterPipeForAllContentTemplates(PublishingSystemFactory.GetPipeSettings("PagePipe"), (Predicate<PipeSettings>) (ps => ps.PipeName == "PagePipe"));
      PipeSettings pipeSettings1 = PublishingSystemFactory.GetPipeSettings("RSSOutboundPipe");
      PublishingSystemFactory.RegisterTemplatePipe(PublishingDefinitions.ItemTemplate, pipeSettings1, (Predicate<PipeSettings>) (ps => ps.PipeName == "RSSOutboundPipe"));
      PipeSettings pipeSettings2 = PublishingSystemFactory.GetPipeSettings("ContentInboundPipe");
      PublishingSystemFactory.RegisterTemplatePipe(PublishingDefinitions.ItemTemplate, pipeSettings2, (Predicate<PipeSettings>) (ps => ps.PipeName == "ContentInboundPipe"));
    }

    private void RegisterPipeDefinitions()
    {
      IList<IDefinitionField> contentPipeDefinitions = PublishingSystemFactory.CreateDefaultContentPipeDefinitions();
      PublishingSystemFactory.RegisterPipeDefinitions("ContentInboundPipe", contentPipeDefinitions);
      PublishingSystemFactory.RegisterPipeDefinitions("ContentOutboundPipe", contentPipeDefinitions);
      PublishingSystemFactory.RegisterPipeDefinitions("FormInboundPipe", contentPipeDefinitions);
      this.RegisterContentPipeDefinitions();
      IList<IDefinitionField> rssPipeDefinitions = (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultRSSPipeDefinitions();
      PublishingSystemFactory.RegisterPipeDefinitions("RSSOutboundPipe", rssPipeDefinitions);
      PublishingSystemFactory.RegisterPipeDefinitions("RSSInboundPipe", rssPipeDefinitions);
      PublishingSystemFactory.RegisterPipeDefinitions("PagePipe", (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultPagePipeDefinitions());
      PublishingSystemFactory.RegisterPipeDefinitions("Twitter", (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultTwitterPipeDefinitions());
      PublishingSystemFactory.RegisterPipeDefinitions("TwitterInboundPipe", (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultTwitterInboundPipeDefinitions());
      PublishingSystemFactory.RegisterPipeDefinitions("Twitter", (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultTwitterPipeDefinitions());
      PublishingSystemFactory.RegisterPipeDefinitions("Pop3InboundPipe", (IList<IDefinitionField>) PublishingSystemFactory.CreateDefaultPop3PipeDefinitions());
      PublishingSystemFactory.RegisterPipeDefinitions("ConfigurationsInboundPipe", ConfigurationsInboundPipe.CreateDefaultAdvancedSettingsPipeDefinitions());
    }

    private void RegisterContentPipeDefinitions()
    {
      MetadataManager manager = MetadataManager.GetManager();
      foreach (string name in Config.Get<PublishingConfig>().ContentPipeTypes.Select<TypeConfigElement, string>((Func<TypeConfigElement, string>) (pt => string.IsNullOrEmpty(pt.AssemblyQualifiedName) ? pt.FullName : pt.AssemblyQualifiedName)))
      {
        Type type = TypeResolutionService.ResolveType(name);
        IList<IDefinitionField> collection = !PublishingSystemFactory.ContentPipeDefinitionsRegistered("ContentInboundPipe", type.FullName) ? PublishingSystemFactory.CreateDefaultContentPipeDefinitions() : (IList<IDefinitionField>) PublishingSystemFactory.GetContentPipeDefinitions("ContentInboundPipe", type.FullName);
        List<IDefinitionField> definitions = !(collection is List<IDefinitionField>) ? new List<IDefinitionField>((IEnumerable<IDefinitionField>) collection) : (List<IDefinitionField>) collection;
        MetaType metaType = manager.GetMetaType(type);
        if (metaType != null)
        {
          foreach (SimpleDefinitionField simpleDefinitionField in metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (mf => mf.FieldName != "Category" && mf.FieldName != "Tags")).Select<MetaField, SimpleDefinitionField>((Func<MetaField, SimpleDefinitionField>) (mf => new SimpleDefinitionField(mf.FieldName, mf.Title, mf.Required))))
          {
            SimpleDefinitionField metaField = simpleDefinitionField;
            int index = definitions.FindIndex((Predicate<IDefinitionField>) (f => f.Name == metaField.Name));
            if (index > -1)
              definitions[index] = (IDefinitionField) metaField;
            else
              definitions.Add((IDefinitionField) metaField);
          }
          PublishingSystemFactory.UnregisterContentPipeDefinitions("ContentInboundPipe", type.FullName);
          PublishingSystemFactory.RegisterContentPipeDefinitions("ContentInboundPipe", type.FullName, (IList<IDefinitionField>) definitions);
          PublishingSystemFactory.UnregisterContentPipeDefinitions("ContentOutboundPipe", type.FullName);
          PublishingSystemFactory.RegisterContentPipeDefinitions("ContentOutboundPipe", type.FullName, (IList<IDefinitionField>) definitions);
        }
      }
    }

    private void RegisterPublishingPointMetaFields() => PublishingSystemFactory.RegisterPublishingPointMetaFields((IEnumerable<IDefinitionField>) PublishingSystemFactory.GetDefaultPublishingPointMetaFields());

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      PageManager pageManager = initializer.PageManager;
      PageNode pageNode = pageManager.GetPageNode(SiteInitializer.ToolsNodeId);
      Guid id = PublishingModule.PublishingPageGroupId;
      PageNode parentNode = pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == id)).SingleOrDefault<PageNode>();
      if (parentNode == null)
      {
        parentNode = initializer.CreatePageNode(PublishingModule.PublishingPageGroupId, pageNode, NodeType.Group);
        parentNode.Name = "AlternativePublishing";
        parentNode.Title = (Lstring) Res.Expression("PageResources", "AlternativePublishingTitle");
        parentNode.ShowInNavigation = true;
        parentNode.ModuleName = this.Name;
        Res.SetLstring(parentNode.UrlName, typeof (PageResources).Name, "AlternativePublishingUrlName");
        Res.SetLstring(parentNode.Description, typeof (PageResources).Name, "AlternativePublishingDescription");
      }
      id = PublishingModule.HomePageId;
      if (pageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == id)) == null)
      {
        PageDataElement pageDataElement = new PageDataElement();
        pageDataElement.PageId = PublishingModule.HomePageId;
        pageDataElement.Name = "AlternativePublishing";
        pageDataElement.MenuName = "AlternativePublishingTitle";
        pageDataElement.UrlName = "AlternativePublishingUrlName";
        pageDataElement.Description = "AlternativePublishingDescription";
        pageDataElement.ResourceClassId = typeof (PageResources).Name;
        pageDataElement.IncludeScriptManager = true;
        pageDataElement.ShowInNavigation = false;
        pageDataElement.EnableViewState = false;
        pageDataElement.TemplateName = "DefaultBackend";
        PageDataElement pageInfo = pageDataElement;
        BackendContentView backendContentView1 = new BackendContentView();
        backendContentView1.ModuleName = this.Name;
        backendContentView1.ControlDefinitionName = PublishingDefinitions.BackendDefinitionName;
        BackendContentView backendContentView2 = backendContentView1;
        initializer.CreatePageFromConfiguration((PageElement) pageInfo, parentNode, (Control) backendContentView2).ModuleName = this.Name;
      }
      initializer.Installer.PageToolbox().ContentSection().LoadOrAddWidget<FeedEmbedControl>("FeedEmbedControl").SetTitle("FeedEmbedControlTitle").SetDescription("FeedEmbedControlDescription").LocalizeUsing<PageResources>().SetCssClass("sfFeedsIcn").Done();
      initializer.Installer.PageToolbox().LoadOrAddSection("Social").LoadOrAddWidget<TwitterFeedWidget>("TwitterFeedWidget").SetTitle("TwitterFeedWidgetTitle").SetDescription("TwitterFeedWidgetDescription").LocalizeUsing<PublishingMessages>().SetCssClass("sfTwitterFeedIcn").Done();
      initializer.RegisterControlTemplate(TwitterFeedWidget.layoutTemplatePath, typeof (TwitterFeedWidget).FullName, "Twitter Feed");
    }

    /// <inheritdoc />
    public override IList<SecurityRoot> GetSecurityRoots(bool getContextRootsOnly = true)
    {
      List<SecurityRoot> securityRoots = new List<SecurityRoot>();
      foreach (SecurityRoot securityRoot in (IEnumerable<SecurityRoot>) base.GetSecurityRoots(getContextRootsOnly))
      {
        if (securityRoot.DataProviderName != "SearchPublishingProvider")
          securityRoots.Add(securityRoot);
      }
      return (IList<SecurityRoot>) securityRoots;
    }

    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<PublishingConfig>();

    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (upgradeFrom.Build < 1191)
      {
        foreach (PipeSettings pipeSettings1 in initializer.GetManagerInTransaction<PublishingManager>("SearchPublishingProvider").GetPipeSettings<SitefinityContentPipeSettings>().ToList<SitefinityContentPipeSettings>())
        {
          foreach (PipeSettings pipeSettings2 in pipeSettings1.PublishingPoint.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.IsInbound)).ToList<PipeSettings>())
            pipeSettings2.MaxItems = 0;
        }
      }
      if (upgradeFrom.Build < 3040)
        initializer.Installer.PageToolbox().ContentSection().LoadOrAddWidget<FeedEmbedControl>("FeedEmbedControl").SetTitle("FeedEmbedControlTitle").SetDescription("FeedEmbedControlDescription").LocalizeUsing<PageResources>().SetCssClass("sfFeedsIcn").Done();
      if (upgradeFrom.Build >= 4965)
        return;
      initializer.Installer.PageToolbox().LoadOrAddSection("Social").LoadOrAddWidget<TwitterFeedWidget>("TwitterFeedWidget").SetTitle("TwitterFeedWidgetTitle").SetDescription("TwitterFeedWidgetDescription").LocalizeUsing<PublishingMessages>().SetCssClass("sfTwitterFeedIcn").Done();
      initializer.RegisterControlTemplate(TwitterFeedWidget.layoutTemplatePath, typeof (TwitterFeedWidget).FullName, "TwitterFeedWidget");
    }
  }
}
