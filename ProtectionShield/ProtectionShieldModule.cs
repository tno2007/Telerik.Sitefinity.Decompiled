// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.ProtectionShieldModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.ProtectionShield.Web.Services;
using Telerik.Sitefinity.ProtectionShield.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Events;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Protection shield module for Sitefinity</summary>
  [Telerik.Sitefinity.Modules.ModuleId("9DFDBB8F-CD5F-4125-81A0-06F31A97068E")]
  internal class ProtectionShieldModule : SecuredModuleBase
  {
    /// <summary>
    /// The shield route used for displaying the forbidden access page
    /// </summary>
    public const string ShieldRoute = "sf/forbidden";
    /// <summary>
    /// Identity for the page group used by all pages in the Protection shield module.
    /// </summary>
    public static readonly Guid ProtectionShieldGroupPageId = new Guid("247EF7F3-8424-44EE-8864-69446F764925");
    /// <summary>
    /// Identity for the protection shield page for the Protection shield module.
    /// </summary>
    public static readonly Guid ProtectionShieldPageNodeId = new Guid("EAB985B0-D21A-4916-870F-33EBDCEBD27F");
    /// <summary>The module id.</summary>
    internal const string ModuleId = "9DFDBB8F-CD5F-4125-81A0-06F31A97068E";
    /// <summary>The module name.</summary>
    internal const string ModuleName = "ProtectionShield";
    private static readonly Type[] МanagerTypes = new Type[1]
    {
      typeof (ProtectionShieldManager)
    };

    /// <inheritdoc />
    public override Guid LandingPageId => ProtectionShieldModule.ProtectionShieldGroupPageId;

    /// <inheritdoc />
    public override Type[] Managers => ProtectionShieldModule.МanagerTypes;

    /// <inheritdoc />
    public override void Install(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(ProtectionShieldModule.ProtectionShieldPageNodeId, "ProtectionShield").PlaceUnder(CommonNode.Settings).SetOrdinal(5).LocalizeUsing<ProtectionShieldResources>().SetTitleLocalized("ProtectionShieldResourcesTitlePlural").SetDescriptionLocalized("ProtectionShieldDescription").SetUrlNameLocalized("ProtectionShieldUrlName").AddChildPage(ProtectionShieldModule.ProtectionShieldGroupPageId, "ProtectionShieldResourcesTitlePlural").SetOrdinal(1).LocalizeUsing<ProtectionShieldResources>().SetTitleLocalized("ProtectionShieldResourcesTitlePlural").SetHtmlTitleLocalized("ProtectionShieldResourcesTitlePlural").SetDescriptionLocalized("ProtectionShieldDescription").SetUrlNameLocalized("ProtectionShieldUrlName").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new ProtectionShieldSettingsView(), "Content").HideFromNavigation().Done().Done();

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (!(upgradeFrom < SitefinityVersion.Sitefinity12_0))
        return;
      this.UpgradeTo12();
    }

    public override void Load()
    {
      base.Load();
      EventHub.Subscribe<IPagePreRenderCompleteEvent>(new SitefinityEventHandler<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler));
    }

    /// <inheritdoc />
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      if (!LicenseState.CheckIsModuleLicensedInAnyDomain("9DFDBB8F-CD5F-4125-81A0-06F31A97068E"))
        return;
      App.WorkWith().Module("ProtectionShield").Initialize().Localization<ProtectionShieldResources>().Configuration<ProtectionShieldConfig>().SitemapFilter<ProtectionShieldModuleNodeFilter>().ServiceStackPlugin((IPlugin) new ProtectionShieldServiceStackPlugin()).ServiceStackPlugin((IPlugin) new ProtectionShieldCookieServiceStackPlugin());
      RouteManager.RegisterRoute("Shield", (RouteBase) new Route("sf/forbidden", (IRouteHandler) new ProtectionShieldRouteHandler()), "System", false);
    }

    /// <inheritdoc />
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<ProtectionShieldConfig>();

    private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent @event)
    {
      if (!@event.PageSiteNode.IsBackend)
        return;
      string protectionShieldImg = ProtectionShieldHelper.GetHelper().GetProtectionShieldImg();
      if (string.IsNullOrEmpty(protectionShieldImg))
        return;
      LiteralControl child = new LiteralControl(protectionShieldImg);
      @event.Page.Controls.Add((Control) child);
    }

    private void UpgradeTo12()
    {
      ISenderProfile senderProfile = SystemManager.GetNotificationService().GetSenderProfile((ServiceContext) null, "SystemConfigSmtpSettingsMigrated");
      if (senderProfile == null)
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ProtectionShieldConfig section = manager.GetSection<ProtectionShieldConfig>();
      section.Notifications.SenderProfile = senderProfile.ProfileName;
      manager.SaveSection((ConfigSection) section);
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CAF-81345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.Install(initializer);

    /// <summary>Gets the service context.</summary>
    /// <returns>The service context</returns>
    internal static ServiceContext GetServiceContext() => new ServiceContext("ThisApplicationKey", "ProtectionShield");
  }
}
