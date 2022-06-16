// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.ConfigurationPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Configuration.Web.UI
{
  /// <summary>Represents ControlPanel for configuration policies.</summary>
  public class ConfigurationPanel : ProviderControlPanel<Page>
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Configuration.Web.UI.ConfigurationPanel" />.
    /// </summary>
    public ConfigurationPanel()
      : base(false)
    {
      this.ResourceClassId = typeof (PageResources).Name;
      this.Title = Res.Get<PageResources>().Configuration;
    }

    /// <summary>Gets the name of default config provider.</summary>
    /// <returns>provider name string</returns>
    protected override string GetDefaultProviderName() => Config.DefaultProvider.Name;

    /// <inheritdoc />
    protected override void InitializeControls(Control viewContainer)
    {
      this.SearchControl = this.TryResolveSearchControl();
      base.InitializeControls(viewContainer);
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews()
    {
      this.AddView<SettingsView>();
      this.AddView<PermissionsView>();
    }

    private Control TryResolveSearchControl() => ObjectFactory.IsTypeRegistered<IConfigurationPanelSearchControl>() ? ObjectFactory.Resolve<IConfigurationPanelSearchControl>() as Control : (Control) null;
  }
}
