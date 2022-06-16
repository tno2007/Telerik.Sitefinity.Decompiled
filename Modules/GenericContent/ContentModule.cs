// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Clients;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>Represents generic content module.</summary>
  public class ContentModule : ContentModuleBase, IPublishingEnabledModule, ITrackingReporter
  {
    /// <summary>Name of the news module.</summary>
    public const string ModuleName = "GenericContent";
    /// <summary>
    /// Name of the resource class used for localization of labels used in generic content module
    /// </summary>
    public static string ResourceClassId = typeof (ContentResources).Name;
    /// <summary>Id of the generic content page group</summary>
    public static readonly Guid ContentPageGroupId = new Guid("22E8317C-DA61-4ddd-9DC1-A4CD8589B769");
    /// <summary>
    /// Id of the comments page for the generic content module
    /// </summary>
    public static readonly Guid CommentsPageId = new Guid("E096B2A2-419B-48cb-9BB1-A5E5FBE6E1D8");
    /// <summary>Id of the home page for the generic content module</summary>
    public static readonly Guid HomePageId = new Guid("E7F92ABD-DE82-4e70-8D32-AFE4A5DB16AC");
    /// <summary>
    /// Defines the control id for the ContentView inside the Content blocks screen.
    /// </summary>
    public const string ContentBlocksContentViewControlId = "cntBlcksCntView";

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[1]
    {
      typeof (ContentManager)
    };

    /// <summary>
    /// Gets the identity of the home (landing) page for the Content module.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => ContentModule.HomePageId;

    /// <summary>Gets the managers initialization mode</summary>
    /// <value>The initialziation mode.</value>
    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartupAsync;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().SitemapFilter<GenericContentNodeFilter>();
      SystemManager.TypeRegistry.Register(typeof (ContentItem).FullName, new SitefinityType()
      {
        SingularTitle = Res.Get<ContentResources>().ContentBlocksSharedTitle,
        PluralTitle = Res.Get<ContentResources>().ContentBlocksSharedTitle,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type,
        Icon = "list-alt",
        ModuleName = this.Name
      });
      ObjectFactory.Container.RegisterType<IContentTransfer, ContentBlocksContentTransfer>(new ContentBlocksContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType(typeof (IOperationProvider), typeof (GenericContentOperationProvider), typeof (GenericContentOperationProvider).Name);
    }

    /// <summary>Installs the pages.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallPages(SiteInitializer initializer)
    {
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade = initializer.Installer.CreateModuleGroupPage(ContentModule.ContentPageGroupId, "ContentBlocksGroup").PlaceUnder(CommonNode.TypesOfContent).SetOrdinal(10).LocalizeUsing<ContentResources>().SetTitleLocalized("ModuleTitle").SetUrlNameLocalized("PageGroupNodeTitle").SetDescriptionLocalized("PageGroupNodeDescription").AddChildPage(ContentModule.HomePageId, "ContentBlocks").LocalizeUsing<PageResources>().SetTitleLocalized("ContentBlocksTitle").SetUrlNameLocalized("ContentBlocksUrlName").SetHtmlTitleLocalized("ContentBlocksHtmlTitle").SetDescriptionLocalized("ContentBlocksDescription");
      BackendContentView backendContentView = new BackendContentView();
      backendContentView.ID = "cntBlcksCntView";
      backendContentView.ControlDefinitionName = ContentDefinitions.BackendDefinitionName;
      modulePageFacade.AddControl((Control) backendContentView).Done();
    }

    /// <summary>Installs the taxonomies.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallTaxonomies(SiteInitializer initializer) => this.InstallTaxonomy(initializer, typeof (ContentItem));

    protected override ConfigSection GetModuleConfig() => (ConfigSection) Telerik.Sitefinity.Configuration.Config.Get<ContentConfig>();

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallConfiguration(SiteInitializer initializer) => this.InitializeSortingExpressionSettings(initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings);

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (upgradeFrom.Build <= 1210)
        this.UpgradeFrom1210(initializer);
      if (upgradeFrom.Build <= 1733)
        this.InstallTaxonomies(initializer);
      if (upgradeFrom.Build < 3860)
        this.Upgrade_UsersWhoHadPermissionsOverPagesCanManipulateContentBlocks(initializer);
      if (upgradeFrom.Build <= 3900)
        this.InitializeSortingExpressionSettings(initializer.Context.GetConfig<ContentViewConfig>().SortingExpressionSettings);
      if (!(upgradeFrom < SitefinityVersion.Sitefinity8_0))
        return;
      initializer.AddSupportedPermissionSetsToSecurityRoot<ContentManager>("SitemapGeneration");
      initializer.RemoveSupportedPermissionSetsToSecurityRoot<ContentManager>("Comments");
    }

    /// <summary>
    /// Upgrade from 3800 or earlier:
    /// Grant all users/roles who have permissions to create/modify/delete pages the permissions to manage content blocks as well.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    private void Upgrade_UsersWhoHadPermissionsOverPagesCanManipulateContentBlocks(
      SiteInitializer initializer)
    {
      List<Guid> guidList = new List<Guid>();
      string[] source1 = new string[5]
      {
        "Create",
        "CreateChildControls",
        "Delete",
        "EditContent",
        "Modify"
      };
      foreach (DataProviderBase staticProvider in (Collection<PageDataProvider>) initializer.PageManager.StaticProviders)
      {
        PageManager manager = PageManager.GetManager(staticProvider.Name, "UpgradeContentPermissions");
        using (new ElevatedModeRegion((IManager) manager))
        {
          IQueryable<PageNode> pageNodes = manager.GetPageNodes();
          Expression<Func<PageNode, bool>> predicate1 = (Expression<Func<PageNode, bool>>) (n => n.RootNode == default (object) && n.Id != SiteInitializer.BackendRootNodeId);
          foreach (ISecuredObject securedObject in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate1))
          {
            IQueryable<Permission> activePermissions = securedObject.GetActivePermissions();
            Expression<Func<Permission, bool>> predicate2 = (Expression<Func<Permission, bool>>) (p => p.PrincipalId != SecurityManager.OwnerRole.Id);
            foreach (Permission permission in (IEnumerable<Permission>) activePermissions.Where<Permission>(predicate2))
            {
              Permission perm = permission;
              if (((IEnumerable<string>) source1).Any<string>((Func<string, bool>) (action => perm.IsGranted(action))) && !guidList.Contains(perm.PrincipalId))
                guidList.Add(perm.PrincipalId);
            }
          }
          TransactionManager.CommitTransaction("UpgradeContentPermissions");
        }
      }
      string[] source2 = new string[5]
      {
        "ChangeOwner",
        "ChangePermissions",
        "Create",
        "Delete",
        "Modify"
      };
      foreach (DataProviderSettings provider in (ConfigElementCollection) Telerik.Sitefinity.Configuration.Config.Get<ContentConfig>().Providers)
      {
        ContentManager manager = ContentManager.GetManager(provider.Name, "UpgradeContentPermissions");
        using (new ElevatedModeRegion((IManager) manager))
        {
          ISecuredObject securityRoot = manager.Provider.GetSecurityRoot(true);
          foreach (Guid guid in guidList)
          {
            Guid principalId = guid;
            Permission principalPermission = securityRoot.Permissions.FirstOrDefault<Permission>((Func<Permission, bool>) (p => p.ObjectId == securityRoot.Id && p.SetName == "General" && p.PrincipalId == principalId));
            if (principalPermission == null)
            {
              principalPermission = manager.CreatePermission("General", securityRoot.Id, principalId);
              securityRoot.Permissions.Add(principalPermission);
            }
            if (!((IEnumerable<string>) source2).Any<string>((Func<string, bool>) (action => principalPermission.IsGranted(action))))
            {
              principalPermission.UndenyActions("Create", "Modify", "Delete");
              principalPermission.GrantActions(true, "Create", "Modify", "Delete");
            }
          }
          securityRoot.SupportedPermissionSets = new string[1]
          {
            "General"
          };
          securityRoot.PermissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>()
          {
            {
              "General",
              "ContentGeneralActionTitle"
            }
          };
          TransactionManager.FlushTransaction("UpgradeContentPermissions");
          using (new DataSyncModeRegion((IManager) manager))
          {
            IQueryable<ContentItem> content1 = manager.GetContent();
            Expression<Func<ContentItem, Guid>> selector = (Expression<Func<ContentItem, Guid>>) (c => c.Id);
            foreach (Guid id in content1.Select<ContentItem, Guid>(selector).ToArray<Guid>())
            {
              ContentItem content2 = manager.GetContent(id);
              content2.InheritsPermissions = true;
              content2.CanInheritPermissions = true;
              manager.CreatePermissionInheritanceAssociation(securityRoot, (ISecuredObject) content2);
            }
            TransactionManager.FlushTransaction("UpgradeContentPermissions");
          }
          TransactionManager.CommitTransaction("UpgradeContentPermissions");
        }
      }
    }

    private void UpgradeFrom1210(SiteInitializer initializer)
    {
      bool suppressSecurityChecks = initializer.PageManager.Provider.SuppressSecurityChecks;
      initializer.PageManager.Provider.SuppressSecurityChecks = true;
      PageNode pageNode1 = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == ContentModule.ContentPageGroupId)).SingleOrDefault<PageNode>();
      if (pageNode1 != null)
      {
        pageNode1.Title = (Lstring) Res.Expression(ContentModule.ResourceClassId, "ModuleTitle");
        Res.SetLstring(pageNode1.UrlName, ContentModule.ResourceClassId, "PageGroupNodeTitle");
        Res.SetLstring(pageNode1.Description, ContentModule.ResourceClassId, "PageGroupNodeDescription");
      }
      PageNode pageNode2 = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == ContentModule.HomePageId)).SingleOrDefault<PageNode>();
      if (pageNode2 != null)
      {
        string name = typeof (PageResources).Name;
        Res.SetLstring(pageNode2.UrlName, name, "ContentBlocksUrlName");
        Res.SetLstring(pageNode2.Page.HtmlTitle, name, "ContentBlocksHtmlTitle");
        Res.SetLstring(pageNode2.Description, name, "ContentBlocksDescription");
      }
      initializer.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      ToolboxSection toolboxSection = initializer.Context.GetConfig<ToolboxesConfig>().Toolboxes["PageControls"].Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (e => e.Name == "ContentToolboxSection")).FirstOrDefault<ToolboxSection>();
      if (toolboxSection == null)
        return;
      ToolboxItem toolboxItem = toolboxSection.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == "GenericContentView")).FirstOrDefault<ToolboxItem>();
      if (toolboxItem == null)
        return;
      toolboxSection.Tools.Remove(toolboxItem);
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CAF-21345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    /// <summary>Configures publishing.</summary>
    public void ConfigurePublishing()
    {
      Type type = typeof (ContentItem);
      string typeFullName = type.FullName;
      string name = type.Name;
      PublishingSystemFactory.RegisterPipeDescriptionProvider("ContentOutboundPipe", (IPipeDescriptionProvider) new ContentOutPipeDescriptionProvider());
      PublishingSystemFactory.RegisterType(typeof (IContentPipeDesignerView), typeof (ContentModule.ContentPipeDesignerView), name);
      PublishingSystemFactory.RegisterType(typeof (IContentOutPipeDesignerView), typeof (ContentModule.ContentOutPipeDesignerView), name);
      SitefinityContentPipeSettings pipeSettings = (SitefinityContentPipeSettings) PublishingSystemFactory.GetPipeSettings("ContentInboundPipe");
      pipeSettings.ContentTypeName = typeFullName;
      pipeSettings.MaxItems = 0;
      PublishingSystemFactory.RegisterPipeForAllContentTemplates((PipeSettings) pipeSettings, (Predicate<PipeSettings>) (ps => ps is SitefinityContentPipeSettings && (ps as SitefinityContentPipeSettings).ContentTypeName == typeFullName));
      PublishingDefinitions.AddBackendFeedsContentTypeFilterSection(type, "Content", "Content");
      IList<IDefinitionField> contentPipeDefinitions = PublishingSystemFactory.CreateDefaultContentPipeDefinitions();
      contentPipeDefinitions.Add((IDefinitionField) new SimpleDefinitionField("SourceName", Res.Get<ContentResources>().Name));
      PublishingSystemFactory.RegisterContentPipeDefinitions("ContentInboundPipe", typeFullName, contentPipeDefinitions);
      PublishingSystemFactory.RegisterContentPipeDefinitions("ContentOutboundPipe", typeFullName, contentPipeDefinitions);
    }

    /// <summary>Initializes the sorting expression settings.</summary>
    /// <param name="sortingExpressions">Sorting expression</param>
    protected virtual void InitializeSortingExpressionSettings(
      ConfigElementList<SortingExpressionElement> sortingExpressions)
    {
      this.AddSortingExpression(sortingExpressions, typeof (ContentItem));
    }

    private void AddSortingExpression(
      ConfigElementList<SortingExpressionElement> sortingExpressions,
      Type contentType)
    {
      ConfigElementList<SortingExpressionElement> configElementList1 = sortingExpressions;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "NewModifiedFirst";
      element1.ResourceClassId = typeof (ContentResources).Name;
      element1.SortingExpression = "LastModified DESC";
      configElementList1.Add(element1);
      ConfigElementList<SortingExpressionElement> configElementList2 = sortingExpressions;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "NewCreatedFirst";
      element2.ResourceClassId = typeof (ContentResources).Name;
      element2.SortingExpression = "DateCreated DESC";
      configElementList2.Add(element2);
      ConfigElementList<SortingExpressionElement> configElementList3 = sortingExpressions;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element3.ContentType = contentType.FullName;
      element3.SortingExpressionTitle = "ByTitleAsc";
      element3.ResourceClassId = typeof (ContentResources).Name;
      element3.SortingExpression = "Title ASC";
      configElementList3.Add(element3);
      ConfigElementList<SortingExpressionElement> configElementList4 = sortingExpressions;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element4.ContentType = contentType.FullName;
      element4.SortingExpressionTitle = "ByTitleDesc";
      element4.ResourceClassId = typeof (ContentResources).Name;
      element4.SortingExpression = "Title DESC";
      configElementList4.Add(element4);
      ConfigElementList<SortingExpressionElement> configElementList5 = sortingExpressions;
      SortingExpressionElement element5 = new SortingExpressionElement((ConfigElement) sortingExpressions);
      element5.ContentType = contentType.FullName;
      element5.SortingExpressionTitle = "CustomSorting";
      element5.ResourceClassId = typeof (ContentResources).Name;
      element5.SortingExpression = "Custom";
      element5.IsCustom = true;
      configElementList5.Add(element5);
    }

    public object GetReport()
    {
      int num = 0;
      foreach (string providerName in ContentManager.GetManager().GetProviderNames(ProviderBindingOptions.SkipSystem))
      {
        ContentManager manager = ContentManager.GetManager(providerName);
        num += manager.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (cb => (int) cb.Status == 2)).Count<ContentItem>();
      }
      int countThatUseContent = this.GetTotalCountThatUseContent();
      return (object) new ContentModuleReport()
      {
        ModuleName = "GenericContent",
        SharedContentBlocksCount = num,
        UsedOnPagesAndTemplatesCount = countThatUseContent
      };
    }

    private int GetTotalCountThatUseContent() => new ContentRelationStatisticsClient().GetContentRelationsCount("SubjectType == \"{0}\" && RelationType == \"{1}\" && (ObjectType == \"{2}\" || ObjectType == \"{3}\")".Arrange((object) typeof (ContentItem).FullName, (object) "Contains", (object) typeof (PageTemplate).FullName, (object) typeof (PageData).FullName), "ObjectId");

    private class ContentPipeDesignerView : IContentPipeDesignerView, IContentPipeDesignerViewBase
    {
      public Type ContentType => typeof (ContentItem);

      public Control BuildControl(Control caller) => caller.Page.LoadControl(typeof (ContentSelectorsPipeDesignerView), (object[]) null);

      public bool UseDefaultDesigner => true;

      public IContentPipeDefaultDesignerView GetDefaultDesignerInfo() => (IContentPipeDefaultDesignerView) new ContentModule.ContentPipeDefaultDesignerView();
    }

    private class ContentOutPipeDesignerView : 
      IContentOutPipeDesignerView,
      IContentPipeDesignerViewBase
    {
      public IContentOutPipeDefaultDesignerView GetDefaultDesignerInfo() => (IContentOutPipeDefaultDesignerView) new ContentModule.ContentPipeDefaultDesignerView();

      public Type ContentType => typeof (ContentItem);

      public Control BuildControl(Control caller) => (Control) null;

      public bool UseDefaultDesigner => true;
    }

    private class ContentPipeDefaultDesignerView : 
      IContentPipeDefaultDesignerView,
      IContentOutPipeDefaultDesignerView
    {
      private readonly Labels resources = Res.Get<Labels>();

      public string TitleText => this.resources.WhichContentItemsToDisplay;

      public string ChooseAllText => this.resources.AllPublishedItems;

      public string SelectionOfItem => this.resources.SelectionOfItems;

      public string SelectorDateRangesTitle => this.resources.DisplayItemsPublishedIn;

      public string ItemsName => Res.Get<ContentResources>().ContentItemsPluralTypeName;
    }
  }
}
