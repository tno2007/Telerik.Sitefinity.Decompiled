// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.CommentsModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Comments.Web.Services;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments
{
  /// <summary>Entry point for the Comments Module</summary>
  internal class CommentsModule : ContentModuleBase
  {
    /// <summary>
    /// Name of the comments module. (e.g. used in CommentsManager)
    /// </summary>
    public const string ModuleName = "Comments";
    /// <summary>
    /// Identity for the comments page for the Comments module
    /// </summary>
    public static readonly Guid CommentsPageId = new Guid("4e0f96fb-62c9-4423-8484-c05aa690971a");
    /// <summary>Localization resources' class Id for news</summary>
    public static readonly string ResourceClassId = typeof (CommentsResources).Name;
    /// <summary>The id of the news backed content view control.</summary>
    public const string CommentsBackendContentViewControlId = "cmmntCntView";
    private static readonly Type[] managerTypes = new Type[0];
    public const string commentsRestServiceUrl = "Sitefinity/Public/Services/CommentsService.svc/";
    public const string FrontendCommentsFullTemplateTitleName = "Comments";
    public const string CommentsListViewTemplateTitleName = "Comments list";
    public const string SubmitCommentTemplateTitleName = "Comments submit form";
    public static readonly Guid ModeratorsRoleId = new Guid("a83fab2e-8664-4636-b197-879b7e121b9e");
    public static readonly string ModeratorsRoleName = "Moderators (comments)";
    public const int CommentsPerPageBackend = 50;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => CommentsModule.managerTypes;

    /// <summary>
    /// Gets the identity of the home (landing) page for the News module.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => CommentsModule.CommentsPageId;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().Localization<CommentsResources>().SitemapFilter<CommentsNodeFilter>().WebService<CommentsRestService>("Sitefinity/Public/Services/CommentsService.svc/").ServiceStackPlugin((IPlugin) new CommentServiceStackPlugin());
      SystemManager.TypeRegistry.Register(typeof (IComment).FullName, new SitefinityType()
      {
        PluralTitle = Res.Get<CommentsResources>().CommentsPluralTypeName,
        SingularTitle = Res.Get<CommentsResources>().CommentsSingleTypeName,
        Kind = SitefinityTypeKind.Type,
        ModuleName = this.Name,
        Parent = (string) null
      });
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (CommentsWidget), typeof (CommentsWidget), "CommentsResources", "CommentsTitle", "CommentsTitle");
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (CommentsListView), typeof (CommentsListView), "CommentsResources", "CommentsTitle", "CommentsListViewTemplateName");
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (CommentsSubmitForm), typeof (CommentsSubmitForm), "CommentsResources", "CommentsTitle", "CommentsForm");
      this.Subscribe();
      this.RegisterConfigRestrictions();
    }

    /// <summary>
    /// Loads the module dependencies after the module has been initialized and installed.
    /// </summary>
    public override void Load()
    {
      base.Load();
      StatisticCache.Current.RegisterStatisticSupport<CommentsStatisticSupport>();
      ManagerBase<ConfigProvider>.Executed += new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
    }

    public override void Unload()
    {
      ManagerBase<ConfigProvider>.Executed -= new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
      base.Unload();
    }

    /// <summary>Invalidated cache dependencies.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    private void ConfigManager_Executed(object sender, ExecutedEventArgs args)
    {
      if (args == null || !(args.CommandName == "SaveSection") || !(args.CommandArguments.GetType() == typeof (CommentsModuleConfig)))
        return;
      CacheDependency.Notify((IList<CacheDependencyKey>) new CacheDependencyKey[1]
      {
        new CacheDependencyKey() { Type = typeof (IThread) }
      });
    }

    public override void Install(SiteInitializer initializer)
    {
      base.Install(initializer);
      this.InstallToolboxConfiguration(initializer);
      this.InstallPermissions(initializer);
      this.InstallRoles(initializer);
      this.DisableCommentsForSomeTypes(initializer);
      initializer.RegisterControlTemplate(CommentsWidget.layoutTemplateName, typeof (CommentsWidget).FullName, "Comments");
      initializer.RegisterControlTemplate(CommentsListView.layoutTemplateName, typeof (CommentsListView).FullName, "Comments list");
      initializer.RegisterControlTemplate(CommentsSubmitForm.layoutTemplateName, typeof (CommentsSubmitForm).FullName, "Comments submit form");
    }

    private void InstallRoles(SiteInitializer initializer)
    {
      RoleManager managerInTransaction = initializer.GetManagerInTransaction<RoleManager>();
      if (managerInTransaction.Provider.Abilities.ContainsKey("AddRole") && !managerInTransaction.Provider.Abilities["AddRole"].Supported && !managerInTransaction.Provider.Abilities["AddRole"].Allowed)
        return;
      if (managerInTransaction.GetRoles().SingleOrDefault<Role>((Expression<Func<Role, bool>>) (r => r.Id == CommentsModule.ModeratorsRoleId)) != null || managerInTransaction.RoleExists(CommentsModule.ModeratorsRoleName))
        return;
      managerInTransaction.CreateRole(CommentsModule.ModeratorsRoleId, CommentsModule.ModeratorsRoleName);
    }

    private void InstallPermissions(SiteInitializer initializer)
    {
      SecurityManager managerInTransaction = initializer.GetManagerInTransaction<SecurityManager>();
      SecurityRoot securityRoot = managerInTransaction.GetSecurityRoot("ApplicationSecurityRoot");
      if (securityRoot == null)
        securityRoot = managerInTransaction.CreateSecurityRoot("ApplicationSecurityRoot", "Backend");
      Telerik.Sitefinity.Security.Model.Permission permission1 = managerInTransaction.GetPermission("Backend", securityRoot.Id, SecurityManager.AdminRole.Id);
      if (permission1 == null)
      {
        permission1 = managerInTransaction.CreatePermission("Backend", securityRoot.Id, SecurityManager.AdminRole.Id);
        securityRoot.Permissions.Add(permission1);
      }
      permission1.GrantActions(true, "AccessComments");
      Telerik.Sitefinity.Security.Model.Permission permission2 = managerInTransaction.GetPermission("Backend", securityRoot.Id, SecurityManager.EditorsRole.Id);
      if (permission2 == null)
      {
        permission2 = managerInTransaction.CreatePermission("Backend", securityRoot.Id, SecurityManager.EditorsRole.Id);
        securityRoot.Permissions.Add(permission2);
      }
      permission2.GrantActions(true, "AccessComments");
      Telerik.Sitefinity.Security.Model.Permission permission3 = managerInTransaction.GetPermission("Backend", securityRoot.Id, CommentsModule.ModeratorsRoleId);
      if (permission3 == null)
      {
        permission3 = managerInTransaction.CreatePermission("Backend", securityRoot.Id, CommentsModule.ModeratorsRoleId);
        securityRoot.Permissions.Add(permission3);
      }
      permission3.GrantActions(true, "AccessComments");
    }

    private void DisableCommentsForSomeTypes(SiteInitializer initializer)
    {
      CommentsModuleConfig config = initializer.Context.GetConfig<CommentsModuleConfig>();
      config.CommentableTypes["Telerik.Sitefinity.Pages.Model.PageNode"].AllowComments = false;
      config.CommentableTypes["Telerik.Sitefinity.Libraries.Model.Image"].AllowComments = false;
      config.CommentableTypes["Telerik.Sitefinity.Libraries.Model.Video"].AllowComments = false;
      config.CommentableTypes["Telerik.Sitefinity.Libraries.Model.Document"].AllowComments = false;
      config.CommentableTypes["Telerik.Sitefinity.Lists.Model.ListItem"].AllowComments = false;
    }

    /// <summary>Installs the pages.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallPages(SiteInitializer initializer)
    {
      initializer.GetManagerInTransaction<PageManager>();
      initializer.Installer.CreateModulePage(CommentsModule.CommentsPageId, "Comments").PlaceUnder(SiteInitializer.DiscussionsPageNodeId).LocalizeUsing<CommentsResources>().SetTitleLocalized("CommentsTitle").SetHtmlTitleLocalized("CommentsTitle").SetUrlNameLocalized("CommentsUrlName").SetDescriptionLocalized("CommentsTitle").SetOrdinal(2).AddControl((Control) new ViewContainer()
      {
        ControlDefinitionName = "CommentsModuleBackend",
        ConfigType = typeof (CommentsModuleConfig).FullName
      }).Done();
    }

    /// <summary>Installs the taxonomies.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallTaxonomies(SiteInitializer initializer)
    {
    }

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<CommentsModuleConfig>();

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected override void InstallConfiguration(SiteInitializer initializer)
    {
    }

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected void InstallToolboxConfiguration(SiteInitializer initializer)
    {
      Toolbox toolbox = initializer.Context.GetConfig<ToolboxesConfig>().Toolboxes["PageControls"];
      ToolboxSection element = toolbox.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (e => e.Name == "ContentToolboxSection")).FirstOrDefault<ToolboxSection>();
      if (element == null)
      {
        element = new ToolboxSection((ConfigElement) toolbox.Sections)
        {
          Name = "ContentToolboxSection",
          Title = "ContentToolboxSectionTitle",
          Description = "ContentToolboxSectionDescription",
          ResourceClassId = typeof (PageResources).Name
        };
        toolbox.Sections.Add(element);
      }
      if (element.Tools.Any<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == "Comments")))
        return;
      element.Tools.Add(new ToolboxItem((ConfigElement) element.Tools)
      {
        Name = "Comments",
        Title = "CommentsTitle",
        Description = "CommentsDescription",
        ResourceClassId = "CommentsResources",
        ModuleName = "Comments",
        ControlType = typeof (CommentsWidget).AssemblyQualifiedName,
        CssClass = "sfCommentsIcn"
      });
    }

    /// <summary>
    /// Subscribes for EventHub data events in order to check comments
    /// </summary>
    protected virtual void Subscribe()
    {
      EventHub.Subscribe<IDataEvent>(new SitefinityEventHandler<IDataEvent>(this.CheckComments));
      EventHub.Subscribe<ICommentCreatingEvent>(new SitefinityEventHandler<ICommentCreatingEvent>(this.AppendNoFollowLinksInComments));
      EventHub.Subscribe<ICommentUpdatingEvent>(new SitefinityEventHandler<ICommentUpdatingEvent>(this.AppendNoFollowLinksInComments));
    }

    protected virtual void CheckComments(IDataEvent dataEvent)
    {
      if (!(dataEvent.Action == DataEventAction.Deleted) || dataEvent is IRecyclableDataEvent recyclableDataEvent && recyclableDataEvent.RecycleBinAction != RecycleBinAction.PermanentDelete || !Config.Get<CommentsModuleConfig>().DeleteAssociatedItemComments)
        return;
      ICommentService commentsService = SystemManager.GetCommentsService();
      this.TryDeleteThread(dataEvent.ItemId.ToString(), commentsService);
      IMultilingualEvent multilingualEvent = dataEvent as IMultilingualEvent;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && multilingualEvent != null)
      {
        List<string> stringList = new List<string>();
        if (multilingualEvent.Language == null)
          stringList.AddRange((IEnumerable<string>) SystemManager.CurrentContext.CurrentSite.PublicCultures.Values);
        else
          stringList.Add(multilingualEvent.Language);
        foreach (string language in stringList)
          this.TryDeleteThread(ControlUtilities.GetLocalizedKey((object) dataEvent.ItemId, language, CommentsBehaviorUtilities.GetLocalizedKeySuffix(dataEvent.ItemType.FullName)), commentsService);
      }
      else
        this.TryDeleteThread(ControlUtilities.GetLocalizedKey((object) dataEvent.ItemId, (string) null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(dataEvent.ItemType.FullName)), commentsService);
    }

    private void AppendNoFollowLinksInComments(ICommentEvent @event)
    {
      int num = Config.Get<SystemConfig>().SeoAndOpenGraphConfig.AppendNoFollowLinksForUntrustedContent ? 1 : 0;
      string trustedDomains = Config.Get<SystemConfig>().SeoAndOpenGraphConfig.TrustedDomains;
      if (num == 0 || string.IsNullOrEmpty(@event?.Item?.Message))
        return;
      @event.Item.Message = new HtmlLinkModifier(trustedDomains, TrustedDomainOptions.IncludeAll).AppendNoFollowAttributeToAnchorTags(@event.Item.Message);
    }

    private void TryDeleteThread(string threadKey, ICommentService cs)
    {
      if (cs.GetThread(threadKey) == null)
        return;
      cs.DeleteThread(threadKey);
    }

    private void RegisterConfigRestrictions()
    {
      CommandWidgetReadOnlyConfigRestrictionStrategy.Add("CommentsSettings", RestrictionLevel.ReadOnlyConfigFile);
      CommandWidgetReadOnlyConfigRestrictionStrategy.Add("Settings", RestrictionLevel.ReadOnlyConfigFile);
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-1B0F-4BF7-8CAE-81345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);
  }
}
