// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SitesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.Kendo;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>Represents the Multi-site management backend UI</summary>
  public class SitesView : KendoView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SitesView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SitesView.ascx");
    private bool showSite;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SitesView" /> class.
    /// </summary>
    public SitesView() => this.LayoutTemplatePath = SitesView.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("sitesGrid", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the reference to the stop confirmation dialog</summary>
    protected virtual PromptDialog StopConfirmationDialog => this.Container.GetControl<PromptDialog>("stopConfirmationDialog", true);

    /// <summary>
    /// Gets the reference to the delete module confirmation dialog
    /// </summary>
    protected virtual PromptDialog DeleteConfirmationDialog => this.Container.GetControl<PromptDialog>("deleteConfirmationDialog", true);

    /// <summary>Gets the reference to the site detail view</summary>
    protected virtual SiteDetailView SiteDetailView => this.Container.GetControl<SiteDetailView>("siteDetailView", true);

    /// <summary>Gets the reference to search widget</summary>
    protected virtual SearchWidget SearchWidget => this.Container.GetControl<SearchWidget>("searchWidget", true);

    /// <summary>Gets the reference to the create site link</summary>
    protected virtual HtmlAnchor CreateSiteLink => this.Container.GetControl<HtmlAnchor>("createSiteLink", false);

    /// <summary>Gets the reference to the page behaviour control</summary>
    protected virtual PageBehaviourControl PageBehaviourField => this.StopConfirmationDialog.CustomContentWrapper.FindControl("pageBehaviourField") as PageBehaviourControl;

    /// <summary>Gets the RAD window manager.</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the permissions dialog.</summary>
    protected virtual Telerik.Web.UI.RadWindow PermissionsDialog => this.RadWindowManager.Windows.Cast<Telerik.Web.UI.RadWindow>().First<Telerik.Web.UI.RadWindow>((Func<Telerik.Web.UI.RadWindow, bool>) (w => w.ID == "permissions"));

    /// <summary>Gets the reference to the create site link</summary>
    public virtual bool ShowSite
    {
      get => this.showSite;
      set => this.showSite = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether separate users per site mode is enabled
    /// </summary>
    public bool SeparateUsersPerSiteModeEnabled { get; set; }

    protected virtual string DashboardUrl => BackendSiteMap.FindSiteMapNode(SiteInitializer.DashboardPageNodeId, true).Url;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SitesView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      string str1 = this.Page.ResolveUrl("~/Sitefinity/Services/Multisite/Multisite.svc/");
      controlDescriptor.AddProperty("webServiceUrl", (object) str1);
      controlDescriptor.AddProperty("dashboardUrl", (object) this.Page.ResolveUrl(this.DashboardUrl));
      controlDescriptor.AddProperty("showSite", (object) this.ShowSite);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("siteDetailView", this.SiteDetailView.ClientID);
      controlDescriptor.AddComponentProperty("searchWidget", this.SearchWidget.ClientID);
      controlDescriptor.AddElementProperty("createSiteLink", this.CreateSiteLink.ClientID);
      if (this.PageBehaviourField != null)
        controlDescriptor.AddComponentProperty("pageBehaviourField", this.PageBehaviourField.ClientID);
      controlDescriptor.AddComponentProperty("stopConfirmationDialog", this.StopConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("deleteConfirmationDialog", this.DeleteConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("permissionsDialog", this.PermissionsDialog.ClientID);
      string str2 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog?showPermissionSetNameTitle=false&backLabelText={0}&managerClassName={1}&securedObjectTypeName={2}&title={3}&securedObjectID={4}".Arrange((object) Res.Get<MultisiteResources>().BackToSites, (object) typeof (MultisiteManager).FullName, (object) typeof (Site).FullName, (object) "{0}", (object) "{1}"), UrlResolveOptions.Rooted);
      controlDescriptor.AddProperty("_permissionsDialogUrl", (object) str2);
      controlDescriptor.AddProperty("separateUsersPerSiteModeEnabled", (object) this.SeparateUsersPerSiteModeEnabled);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SitesView.js", typeof (SitesView).Assembly.FullName)
    };

    protected override void InitializeControls(GenericContainer container)
    {
      if (!LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185") && !this.IsDesignMode())
        this.RedirectToSinglesite();
      string permissionSet = "Site";
      string str = "CreateEditSite";
      if (!MultisiteManager.GetManager().Provider.SecurityRoot.IsGranted(permissionSet, str))
        this.CreateSiteLink.Visible = false;
      this.SeparateUsersPerSiteModeEnabled = SecurityManager.AllowSeparateUsersPerSite;
    }

    private void RedirectToSinglesite()
    {
      HttpResponseBase response = SystemManager.CurrentHttpContext.Response;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(MultisiteModule.SiteSettingsPageId, false);
      VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash));
      if (siteMapNode == null)
        return;
      response.Clear();
      response.Redirect(siteMapNode.Url);
    }
  }
}
