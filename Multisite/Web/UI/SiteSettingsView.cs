// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>
  /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView" /> class.
  /// </summary>
  public class SiteSettingsView : KendoView
  {
    private string backLinkUrl;
    internal const string ScriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteSettingsView.js";
    private static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SiteSettingsView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView" /> class.
    /// </summary>
    public SiteSettingsView()
    {
      this.LayoutTemplatePath = SiteSettingsView.TemplatePath;
      this.backLinkUrl = this.ResolveBacklinkUrl();
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the reference to the site detail view</summary>
    protected virtual SiteDetailView SiteDetailView => this.Container.GetControl<SiteDetailView>("siteDetailView", true);

    /// <summary>Gets the backlink url</summary>
    protected virtual string BackLinkUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.backLinkUrl))
          this.backLinkUrl = this.ResolveBacklinkUrl();
        return this.backLinkUrl;
      }
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SiteSettingsView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("siteDetailView", this.SiteDetailView.ClientID);
      controlDescriptor.AddProperty("_isInStandaloneMode", (object) true);
      controlDescriptor.AddProperty("_currentSiteId", (object) SystemManager.CurrentContext.CurrentSite.Id);
      controlDescriptor.AddProperty("backLinkUrl", (object) this.BackLinkUrl);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteSettingsView.js", typeof (SiteSettingsView).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (!LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185") || this.IsDesignMode())
        return;
      this.RedirectToMultisite();
    }

    private void RedirectToMultisite()
    {
      HttpResponseBase response = SystemManager.CurrentHttpContext.Response;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(MultisiteModule.HomePageId, false);
      VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash));
      if (siteMapNode == null)
        return;
      response.Clear();
      response.Redirect(siteMapNode.Url);
    }

    private string ResolveBacklinkUrl()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(Config.Get<PagesConfig>().BackendHomePageId, false);
      return RouteHelper.ResolveUrl(siteMapNode != null ? siteMapNode.Url : "~/Sitefinity", UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
