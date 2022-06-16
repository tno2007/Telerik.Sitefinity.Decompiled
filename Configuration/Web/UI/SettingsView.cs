// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.SettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI
{
  /// <summary>Code Behind for settings view.</summary>
  public class SettingsView : ViewModeControl<ConfigurationPanel>
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.SettingsView.ascx");
    private ClientLabelManager clientLabelManager;

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SettingsView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for
    /// posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.AddClientLabelsAndConstants();
      this.HandleSpecificConfigurationNavigation();
      this.InitializeClientBackLink();
    }

    private void HandleSpecificConfigurationNavigation()
    {
      RequestContext requestContext = this.Page.GetRequestContext();
      if (requestContext == null)
        return;
      object obj = requestContext.RouteData.Values["Params"];
      if (obj == null)
        return;
      string[] strArray = (string[]) obj;
      if (strArray.Length > 1)
        throw new HttpException(404, "Page not found");
      if (strArray.Length != 1)
        return;
      string name = strArray[0] + "Config";
      ConfigSection configSection = this.HasConfigSection(name) ? Config.GetConfigSection(name) : throw new HttpException(404, "Page not found");
      this.clientLabelManager.AddConstant("rootNode", configSection.TagName);
      this.clientLabelManager.AddConstant("rootNodeTitle", this.GetSectionTitle(configSection.GetType().Name));
    }

    private void InitializeClientBackLink()
    {
      HyperLink control = this.Host.Container.GetControl<HyperLink>("backLink", false);
      if (control == null)
        return;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.BasicSettingsNodeId, false);
      if (siteMapNode == null)
        return;
      control.NavigateUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      control.Text = Res.Get<PageResources>().Basic;
      control.Visible = true;
    }

    private void AddClientLabelsAndConstants()
    {
      ClientLabelManager clientLabelManager = new ClientLabelManager();
      clientLabelManager.ID = "settingsView_clientLabelManager";
      clientLabelManager.ClientIDMode = ClientIDMode.Static;
      this.clientLabelManager = clientLabelManager;
      this.Controls.Add((Control) this.clientLabelManager);
      this.clientLabelManager.AddConstant("configSectionDelimiter", ConfigSectionItems.ConfigSectionDelimiter);
      this.clientLabelManager.AddConstant("moveServiceUrl", RouteHelper.ResolveUrl("Sitefinity/Services/Configuration/ConfigSectionItems.svc/move", UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash));
      this.clientLabelManager.AddClientLabel("Labels", "ModifiedDefaults");
      this.clientLabelManager.AddClientLabel("Labels", "SectionSaved");
      this.clientLabelManager.AddClientLabel("Labels", "ConfirmDelete");
      this.clientLabelManager.AddClientLabel("Labels", "ShowEncryptionOptions");
      this.clientLabelManager.AddClientLabel("Labels", "HideEncryptionOptions");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationDatabase");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationFileSystem");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationDefaultValue");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationDatabaseInCollection");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationFileSystemInCollection");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationDefaultValueInCollection");
      this.clientLabelManager.AddClientLabel("Labels", "DefaultValue");
      this.clientLabelManager.AddClientLabel("Labels", "Source");
      this.clientLabelManager.AddClientLabel("Labels", "Modified");
      this.clientLabelManager.AddClientLabel("Labels", "ExternallyDefined");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigItemSaveLocationModifiedDefault");
      this.clientLabelManager.AddClientLabel("Labels", "ConfigPropertyPathTitle");
      this.clientLabelManager.AddConstant("isOneSite", SystemManager.CurrentContext.GetSites(true).Count<ISite>() == 1 ? "true" : string.Empty);
      this.clientLabelManager.AddConstant("isGlobalUser", ClaimsManager.GetCurrentIdentity().IsGlobalUser ? "true" : string.Empty);
      this.clientLabelManager.AddConstant("currentSiteId", SystemManager.CurrentContext.CurrentSite.Id.ToString());
    }

    private bool HasConfigSection(string name)
    {
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (ConfigSection)))
      {
        if (string.Compare(registerEventArgs.TypeTo.Name, name, true) == 0)
          return true;
      }
      return false;
    }

    private string GetSectionTitle(string str) => str.Substring(0, str.LastIndexOf("Config"));
  }
}
