// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>Represents the control panel for basic settings.</summary>
  public class BasicSettingsPanel : BaseSettingsPanel
  {
    /// <summary>Gets or sets the title of the panel.</summary>
    public override string Title
    {
      get => Res.Get<PageResources>().BasicSettingsTitle;
      set => base.Title = value;
    }

    /// <summary>Creates the child controls.</summary>
    protected override void CreateChildControls()
    {
      RequestContext requestContext = this.Page.GetRequestContext();
      if (requestContext != null)
      {
        object obj = requestContext.RouteData.Values["Params"];
        if (obj != null)
        {
          string[] strArray = (string[]) obj;
          if (strArray.Length > 1)
            throw new HttpException(404, "View not found");
          if (strArray.Length == 1)
            this.ViewMode = strArray[0];
        }
      }
      base.CreateChildControls();
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.AdvancedSettingsNodeId, false);
      if (siteMapNode == null)
        return;
      this.SettingsLink.NavigateUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      this.SettingsLink.Text = Res.Get<PageResources>().Advanced;
      if (ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        return;
      ConfigElementDictionary<string, PropertyPath> specificProperties = Config.Get<SiteSettingsConfig>().SiteSpecificProperties;
      if ((specificProperties != null ? (specificProperties.Count == 0 ? 1 : 0) : 0) == 0)
        return;
      this.SettingsLink.Visible = false;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews()
    {
      foreach (BasicSettingsRegistration settingsRegistration in SystemManager.GetEditableBasicSettingsRegistrations())
        this.AddView(settingsRegistration.ViewType, settingsRegistration.SettingsName, (string) null, (string) null, (string) null);
    }

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      list.Add((ICommandPanel) new BasicSettingsSidebar());
    }
  }
}
