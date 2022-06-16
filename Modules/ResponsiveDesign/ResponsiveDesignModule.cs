// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>Module class for the Responsive Design module.</summary>
  [Telerik.Sitefinity.Modules.ModuleId("01F89003-7A52-4C08-BA60-45C8B8824B38")]
  public class ResponsiveDesignModule : ModuleBase
  {
    /// <summary>
    /// The identifier of the responsive design module node id.
    /// </summary>
    public static readonly Guid ResponsiveDesignModuleNodeId = new Guid("486458AC-834A-4F4E-9DF3-225B197F83C0");
    /// <summary>The identifier of the media queries node.</summary>
    public static readonly Guid ResponsiveAndMobileDesignNodeId = new Guid("61B619B8-8B69-4DF4-947F-466CFA5E2758");
    /// <summary>Name of the Responsive Design module.</summary>
    public const string moduleName = "ResponsiveDesign";
    /// <summary>Id of the module. Used for licensing.</summary>
    public const string ModuleId = "01F89003-7A52-4C08-BA60-45C8B8824B38";
    /// <summary>
    /// Name of the securable action for accessing responsive design module.
    /// </summary>
    internal static readonly string AccessResponsiveDesignAction = "AccessResponsiveDesign";
    /// <summary>The module route name</summary>
    internal const string ModuleRouteName = "ResponsiveDesignTransformations";
    /// <summary>The responsive design settings service URL.</summary>
    internal const string ResponsiveDesignSettingsServiceUrl = "Sitefinity/Services/ResponsiveDesign/Settings.svc";

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.ModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => ResponsiveDesignModule.ResponsiveAndMobileDesignNodeId;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[1]
    {
      typeof (ResponsiveDesignManager)
    };

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartupAsync;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      App.WorkWith().Module(settings.Name).Initialize().Configuration<ResponsiveDesignConfig>().Localization<ResponsiveDesignResources>().SitemapFilter<ResponsiveDesignNodeFilter>().BasicSettings<ResponsiveDesignTransformationsBasicSettingsView>("ResponsiveDesignTransformations", "ResponsiveDesignTransformations", "PageResources").Route("ResponsiveDesignTransformations", (RouteBase) new Route("Sitefinity/Public/ResponsiveDesign/layout_transformations.css", (IRouteHandler) new ResponsiveDesignTransformationRouteHandler())).WebService(typeof (ResponsiveDesignSettingsService), "Sitefinity/Services/ResponsiveDesign/Settings.svc");
      base.Initialize(settings);
    }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      this.InstallPages(initializer);
      this.InstallPermissions(initializer);
    }

    private void InstallPages(SiteInitializer initializer)
    {
      ModulePageFacade<ModuleNodeFacade<ModuleInstallFacade>> modulePageFacade = initializer.Installer.CreateModuleGroupPage(ResponsiveDesignModule.ResponsiveDesignModuleNodeId, nameof (ResponsiveDesignModule)).PlaceUnder(CommonNode.Design).SetOrdinal(3).LocalizeUsing<ResponsiveDesignResources>().SetTitleLocalized("ResponsiveAndMobileDesignNodeTitle").SetUrlNameLocalized("ResponsiveAndMobileDesignNodeUrlName").SetDescriptionLocalized("ResponsiveAndMobileDesignNodeDescription").AddChildPage(ResponsiveDesignModule.ResponsiveAndMobileDesignNodeId, "ResponsiveAndMobileDesign").LocalizeUsing<ResponsiveDesignResources>().SetOrdinal(1).SetUrlNameLocalized("RuleGroupsUrlName").SetTitleLocalized("ResponsiveAndMobileDesignNodeTitle").SetHtmlTitleLocalized("ResponsiveAndMobileDesignNodeTitle").SetDescriptionLocalized("ResponsiveAndMobileDesignNodeDescription").IncludeScriptManager().SetTemplate("DefaultBackend");
      BackendContentView backendContentView = new BackendContentView();
      backendContentView.ControlDefinitionName = "RulesGroupBackend";
      modulePageFacade.AddControl((Control) backendContentView);
    }

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4ZF1-8CAF-81345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<ResponsiveDesignConfig>();

    private void InstallPermissions(SiteInitializer initializer)
    {
      SecurityManager managerInTransaction = initializer.GetManagerInTransaction<SecurityManager>();
      SecurityRoot securityRoot = managerInTransaction.GetSecurityRoot("ApplicationSecurityRoot");
      if (securityRoot == null)
        securityRoot = managerInTransaction.CreateSecurityRoot("ApplicationSecurityRoot", "Backend");
      Permission permission = managerInTransaction.GetPermission("Backend", securityRoot.Id, SecurityManager.DesignersRole.Id);
      if (permission == null)
      {
        permission = managerInTransaction.CreatePermission("Backend", securityRoot.Id, SecurityManager.DesignersRole.Id);
        securityRoot.Permissions.Add(permission);
      }
      permission.GrantActions(true, ResponsiveDesignModule.AccessResponsiveDesignAction);
    }
  }
}
