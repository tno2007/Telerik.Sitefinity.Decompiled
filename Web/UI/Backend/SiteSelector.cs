// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.SiteSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  public class SiteSelector : SimpleScriptView
  {
    private bool showCustomizeAndallSitesLink;
    private string dataKeyName = "Id";
    private string dataTextField = "Name";
    private int numberOfSitesToBeDisplayed = 15;
    private string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Multisite/Multisite.svc/");
    private bool preventDefaultBehavior;
    private bool renderAsDropDown;
    private bool renderSitesRepeaterForOneSite;
    private string siteMenuHeading;
    private bool useContextualSiteMenu;
    private bool includeSiteDetailView;
    private SiteDetailView siteDetailView;
    private IEnumerable<ISite> sites;
    private IEnumerable<ISite> mainMenuSites;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.SiteSelector.ascx");
    internal const string siteSelectorScript = "Telerik.Sitefinity.Web.UI.Backend.Scripts.SiteSelector.js";
    internal const string clickMenuScript = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";

    /// <summary>Gets all online sites.</summary>
    public IEnumerable<ISite> Sites
    {
      get
      {
        if (this.sites == null)
        {
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null)
          {
            UserActivityRecord userActivityRecord = UserActivityManager.GetCurrentUserActivity();
            this.sites = userActivityRecord == null ? (IEnumerable<ISite>) new List<ISite>() : (IEnumerable<ISite>) multisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => userActivityRecord.AllowedSites.Contains(s.Id))).OrderBy<ISite, string>((Func<ISite, string>) (s => s.Name));
          }
          else
            this.sites = (IEnumerable<ISite>) new List<ISite>();
        }
        return this.sites;
      }
    }

    /// <summary>Gets the sites located in main menu.</summary>
    public IEnumerable<ISite> MainMenuSites
    {
      get
      {
        if (this.mainMenuSites == null)
        {
          UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(ClaimsManager.GetCurrentUserId());
          if (cachedUserProfile != null)
          {
            IEnumerable<Guid> preferences = JsonConvert.DeserializeObject<IEnumerable<Guid>>(cachedUserProfile.GetPreference<string>("mainMenuSites", "[]"));
            if (preferences != null && preferences.Count<Guid>() > 0)
              this.mainMenuSites = this.Sites.Where<ISite>((Func<ISite, bool>) (s => preferences.Contains<Guid>(s.Id)));
          }
        }
        if (this.mainMenuSites == null)
          this.mainMenuSites = this.Sites.Where<ISite>((Func<ISite, bool>) (s => s.IsLocatedInMainMenu));
        return this.mainMenuSites;
      }
    }

    /// <summary>
    /// Gets or sets the number more of sites which will be displayed.
    /// 15 by default.
    /// </summary>
    /// <value>The number of sites.</value>
    public int NumberOfSitesToBeDisplayed
    {
      get => this.numberOfSitesToBeDisplayed;
      set => this.numberOfSitesToBeDisplayed = value;
    }

    /// <summary>Gets or sets the web service URL format.</summary>
    /// <value>The web service URL format.</value>
    public string WebServiceUrl
    {
      get => this.webServiceUrl;
      set => this.webServiceUrl = value;
    }

    /// <summary>Gets or sets the prevent default behavior setting.</summary>
    public bool PreventDefaultBehavior
    {
      get => this.preventDefaultBehavior;
      set => this.preventDefaultBehavior = value;
    }

    /// <summary>
    /// Gets or sets the value of the render as drop down setting.
    /// </summary>
    public bool RenderAsDropDown
    {
      get => this.renderAsDropDown;
      set => this.renderAsDropDown = value;
    }

    /// <summary>
    /// Gets or sets the value indicating wheather to render the sites repeater when only onse site is present in the system.
    /// </summary>
    public bool RenderSitesRepeaterForOneSite
    {
      get => this.renderSitesRepeaterForOneSite;
      set => this.renderSitesRepeaterForOneSite = value;
    }

    /// <summary>
    /// Gets or sets the value of the label of the webiste list.
    /// </summary>
    public string SiteMenuHeading
    {
      get => this.siteMenuHeading;
      set => this.siteMenuHeading = value;
    }

    /// <summary>
    /// Gets or sets the value of whether to use the simple version of the site menu.
    /// </summary>
    public bool UseContextualSiteMenu
    {
      get => this.useContextualSiteMenu;
      set => this.useContextualSiteMenu = value;
    }

    /// <summary>Gets the current site id.</summary>
    internal Guid SiteId
    {
      get
      {
        if (!this.UseContextualSiteMenu)
          return SystemManager.CurrentContext.CurrentSite.Id;
        Guid result = Guid.Empty;
        string input = SystemManager.CurrentHttpContext.Request.QueryString["sf_site"];
        if (!input.IsNullOrEmpty())
          Guid.TryParse(input, out result);
        if ((!SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile) || !ClaimsManager.GetCurrentIdentity().IsGlobalUser) && result == Guid.Empty && !this.AlwaysShowAllSitesLinkInContextMenu)
          result = SystemManager.CurrentContext.CurrentSite.Id;
        return result;
      }
    }

    /// <summary>Gets the site menu label.</summary>
    protected virtual Label SitesMenuLabel => this.Container.GetControl<Label>("sitesMenuLabel", true);

    /// <summary>Gets the repeater that lists the more sites.</summary>
    protected virtual Repeater SitesRepeater => this.Container.GetControl<Repeater>("sitesRepeater", true);

    /// <summary>Gets the label that displays current site name.</summary>
    protected virtual Label CurrentSiteLabel => this.Container.GetControl<Label>("currentSiteLabel", true);

    /// <summary>Gets the sites drop down title.</summary>
    protected virtual Label SitesDropDownTitle => this.Container.GetControl<Label>("sitesDropDownTitle", true);

    /// <summary>Gets the HyperLink for all sites.</summary>
    protected virtual HyperLink AllSitesLink => this.Container.GetControl<HyperLink>("allSitesLink", true);

    /// <summary>Gets the HyperLink for removing site context.</summary>
    protected virtual HyperLink RemoveSiteContextLink => this.Container.GetControl<HyperLink>("removeSiteContextLink", true);

    /// <summary>Gets the HyperLink for manage sites.</summary>
    protected virtual HyperLink ManageSitesLink => this.Container.GetControl<HyperLink>("manageSitesLink", true);

    /// <summary>Gets the HyperLink for global settings.</summary>
    protected virtual HyperLink GlobalSettingsLink => this.Container.GetControl<HyperLink>("globalSettingsLink", true);

    /// <summary>Gets the global settings li.</summary>
    protected virtual HtmlGenericControl GlobalSettingsLi => this.Container.GetControl<HtmlGenericControl>("globalSettingsLi", true);

    /// <summary>Gets the customize HyperLink.</summary>
    protected virtual HyperLink CustomizeLink => this.Container.GetControl<HyperLink>("customizeLink", true);

    /// <summary>Gets the html control that displays the sites menu.</summary>
    protected virtual HtmlGenericControl SitesMenu => this.Container.GetControl<HtmlGenericControl>("sitesMenu", true);

    /// <summary>
    /// The drop down used to select current site. Only available when in DropDown mode.
    /// </summary>
    protected DropDownList SitesDropDown => this.Container.GetControl<DropDownList>("sitesDropDown", true);

    /// <summary>Gets the html control that contains all sites links.</summary>
    protected virtual HtmlGenericControl AllSitesLi => this.Container.GetControl<HtmlGenericControl>("allSitesLi", true);

    /// <summary>
    /// Gets the html control that contains the remove site context link.
    /// </summary>
    protected virtual HtmlGenericControl RemoveSiteContextLi => this.Container.GetControl<HtmlGenericControl>("removeSiteContextLi", true);

    /// <summary>Gets the html control that contains customize links.</summary>
    protected virtual HtmlGenericControl CustomizeSitesLi => this.Container.GetControl<HtmlGenericControl>("customizeSitesLi", true);

    /// <summary>Gets the manage sites li.</summary>
    protected virtual HtmlGenericControl ManageSitesLi => this.Container.GetControl<HtmlGenericControl>("manageSitesLi", true);

    /// <summary>
    /// Gets the html control that displays all sites selector dialog.
    /// </summary>
    protected virtual HtmlGenericControl AllSites => this.Container.GetControl<HtmlGenericControl>("allSites", true);

    /// <summary>Gets the all sites selector.</summary>
    protected FlatSelector AllSitesSelector => this.Container.GetControl<FlatSelector>("allSitesSelector", true);

    /// <summary>Gets all sites done selecting button.</summary>
    protected virtual LinkButton AllSitesDoneSelectingButton => this.Container.GetControl<LinkButton>("allSitesDoneSelectingButton", true);

    /// <summary>Gets all sites cancel selecting button.</summary>
    protected virtual LinkButton AllSitesCancelSelectingButton => this.Container.GetControl<LinkButton>("allSitesCancelSelectingButton", true);

    /// <summary>
    /// Gets the html control that displays customize sites selector dialog.
    /// </summary>
    protected virtual HtmlGenericControl CustomizeSites => this.Container.GetControl<HtmlGenericControl>("customizeSites", true);

    /// <summary>Gets the customize sites selector.</summary>
    protected FlatSelector CustomizeSitesSelector => this.Container.GetControl<FlatSelector>("customizeSitesSelector", true);

    /// <summary>Gets customize sites done selecting button.</summary>
    protected virtual LinkButton CustomizeSitesDoneSelectingButton => this.Container.GetControl<LinkButton>("customizeSitesDoneSelectingButton", true);

    /// <summary>Gets customize sites cancel selecting button.</summary>
    protected virtual LinkButton CustomizeSitesCancelSelectingButton => this.Container.GetControl<LinkButton>("customizeSitesCancelSelectingButton", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.Sites.Count<ISite>() < 2)
        return;
      this.Controls.Add((Control) new BackendMultisiteSessionControl());
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      MultisiteResources multisiteResources = Res.Get<MultisiteResources>();
      if (!this.RenderAsDropDown)
      {
        string str = HttpUtility.HtmlEncode(SystemManager.CurrentContext.MultisiteContext.CurrentSite.Name);
        this.SitesMenuLabel.Text = this.SiteMenuHeading;
        this.CurrentSiteLabel.Text = str;
        this.CurrentSiteLabel.ToolTip = str;
        if (this.UseContextualSiteMenu)
        {
          this.ManageSitesLi.Visible = false;
          this.GlobalSettingsLi.Visible = false;
          this.InitializeRemoveSiteContextLink(multisiteResources);
        }
        else
        {
          this.InitializeManageSitesLink(multisiteResources);
          this.InitializeGlobalSettingsLink(multisiteResources);
        }
        this.ConfigureSitesClickMenuMode(multisiteResources);
      }
      else
        this.ConfigureSitesDropDownMode(multisiteResources);
      base.OnPreRender(e);
    }

    /// <summary>Binds the sites drop down.</summary>
    protected virtual void BindSitesDropDown()
    {
      Guid siteId = this.SiteId;
      foreach (ISite site in this.Sites)
      {
        int num = siteId == site.Id ? 1 : 0;
        this.SitesDropDown.Items.Add(new ListItem(site.Name, site.SiteMapRootNodeId.ToString())
        {
          Attributes = {
            ["SiteId"] = site.Id.ToString()
          }
        });
        if (num != 0)
          this.SitesDropDown.SelectedValue = site.SiteMapRootNodeId.ToString();
      }
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SiteSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public bool AlwaysShowAllSitesLinkInContextMenu { get; set; }

    private void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item || !(e.Item.FindControl("siteLink") is HyperLink control))
        return;
      object obj1 = DataBinder.Eval(e.Item.DataItem, this.dataTextField);
      if (obj1 != null)
        control.Text = HttpUtility.HtmlEncode(obj1.ToString());
      object obj2 = DataBinder.Eval(e.Item.DataItem, this.dataKeyName);
      if (obj2 != null)
        control.Attributes.Add("name", obj2.ToString());
      if (!(this.SiteId == new Guid(obj2.ToString())))
        return;
      control.CssClass += " sfSel";
    }

    private void SetSelectorsServiceUrl()
    {
      string str = this.webServiceUrl + "user/" + SecurityManager.GetCurrentUserId().ToString() + "/sites/?sortExpression=Name";
      this.AllSitesSelector.ServiceUrl = str;
      this.CustomizeSitesSelector.ServiceUrl = str;
      this.AllSitesSelector.BindOnLoad = false;
      this.CustomizeSitesSelector.BindOnLoad = false;
    }

    private void ConfigureSitesDropDownMode(MultisiteResources multisiteResources)
    {
      this.AllSites.Visible = false;
      this.CustomizeSites.Visible = false;
      this.SitesMenu.Visible = false;
      if (this.Sites.Count<ISite>() < 2)
        return;
      this.SitesDropDown.Visible = true;
      this.SitesDropDownTitle.Visible = true;
      this.SitesDropDownTitle.Text = multisiteResources.Sites;
      this.BindSitesDropDown();
    }

    /// <summary>Configures the site selector in default mode</summary>
    private void ConfigureSitesClickMenuMode(MultisiteResources multisiteResources)
    {
      int num = this.Sites.Count<ISite>();
      bool flag = this.MainMenuSites.Count<ISite>() > 0;
      if (flag || num > this.NumberOfSitesToBeDisplayed)
      {
        this.showCustomizeAndallSitesLink = true;
        this.SetSelectorsServiceUrl();
        this.SitesRepeater.DataSource = !flag ? (object) this.Sites.Take<ISite>(this.NumberOfSitesToBeDisplayed) : (object) this.MainMenuSites;
        this.AllSitesLink.Text = string.Format(multisiteResources.AllSites, (object) num);
        if (this.UseContextualSiteMenu)
          this.CustomizeSitesLi.Visible = false;
        else
          this.CustomizeLink.Text = multisiteResources.Customize;
      }
      else
      {
        if (num > 1 || this.RenderSitesRepeaterForOneSite)
        {
          this.SitesRepeater.DataSource = (object) this.Sites;
        }
        else
        {
          this.SitesRepeater.Visible = false;
          this.SitesMenuLabel.Visible = false;
          this.ManageSitesLi.Attributes.Add("class", "sfCustomizeMenuWrp sfClearfix sfNoBorder sfMTopNone sfPTopNone");
          if (!this.ManageSitesLi.Visible && !this.GlobalSettingsLi.Visible)
            this.SitesMenu.Attributes.Add("class", "sfSiteSelectorMenuAsLabel");
        }
        this.AllSites.Visible = false;
        this.CustomizeSites.Visible = false;
        this.AllSitesLi.Visible = false;
        this.CustomizeSitesLi.Visible = false;
      }
      if (!this.UseContextualSiteMenu)
        this.RemoveSiteContextLi.Visible = false;
      this.SitesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.Repeater_ItemDataBound);
      this.SitesRepeater.DataBind();
    }

    /// <summary>Sets the url to remove site context</summary>
    private void InitializeRemoveSiteContextLink(MultisiteResources multisiteResources)
    {
      if (SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile) && ClaimsManager.GetCurrentIdentity().IsGlobalUser || this.AlwaysShowAllSitesLinkInContextMenu)
      {
        QueryStringBuilder collection = new QueryStringBuilder(this.Context.Request.QueryString.ToString());
        if (collection.Contains("sf_site"))
          collection.Remove("sf_site");
        this.RemoveSiteContextLink.Text = multisiteResources.SitesAll;
        this.RemoveSiteContextLink.NavigateUrl = this.Context.Request.Path + collection.ToQueryString();
        if (!(this.SiteId == Guid.Empty))
          return;
        this.CurrentSiteLabel.Text = multisiteResources.SitesAll;
        this.RemoveSiteContextLink.CssClass += " sfSel";
      }
      else
        this.RemoveSiteContextLink.Visible = false;
    }

    /// <summary>Sets the url to manage sites page</summary>
    private void InitializeManageSitesLink(MultisiteResources multisiteResources)
    {
      Guid id;
      string str;
      if (LicenseState.CheckIsModuleLicensed("FBD4773B-8688-4C75-8563-28BFDA27A185"))
      {
        id = MultisiteModule.HomePageId;
        str = multisiteResources.ManageSites;
      }
      else
      {
        id = MultisiteModule.SiteSettingsPageId;
        str = multisiteResources.ManageSite;
      }
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(id, false);
      if (siteMapNode != null && siteMapNode.IsAccessibleToUser(this.Context))
      {
        this.ManageSitesLink.Text = str;
        this.ManageSitesLink.NavigateUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      }
      else
        this.ManageSitesLi.Visible = false;
    }

    /// <summary>Sets the url to global settings page</summary>
    private void InitializeGlobalSettingsLink(MultisiteResources multisiteResources)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.SettingsNodeId, false);
      if (siteMapNode.IsAccessibleToUser(this.Context) && ClaimsManager.GetCurrentIdentity().IsGlobalUser)
      {
        this.GlobalSettingsLink.NavigateUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
        this.GlobalSettingsLink.Text = multisiteResources.GlobalSettings;
      }
      else
        this.GlobalSettingsLi.Visible = false;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (SiteSelector).FullName, this.ClientID);
      behaviorDescriptor.AddElementProperty("currentSiteLabel", this.CurrentSiteLabel.ClientID);
      behaviorDescriptor.AddElementProperty("sitesMenu", this.SitesMenu.ClientID);
      behaviorDescriptor.AddElementProperty("removeSiteContextLink", this.RemoveSiteContextLink.ClientID);
      behaviorDescriptor.AddProperty("selectedSite", (object) this.SiteId);
      behaviorDescriptor.AddProperty("mainMenuSites", (object) this.MainMenuSites.Select<ISite, Guid>((Func<ISite, Guid>) (s => s.Id)).ToArray<Guid>());
      behaviorDescriptor.AddProperty("siteIdParamKey", (object) "sf_site");
      behaviorDescriptor.AddProperty("preventDefaultBehavior", (object) this.PreventDefaultBehavior);
      if (this.showCustomizeAndallSitesLink)
      {
        behaviorDescriptor.AddElementProperty("allSitesLink", this.AllSitesLink.ClientID);
        behaviorDescriptor.AddElementProperty("customizeLink", this.CustomizeLink.ClientID);
        behaviorDescriptor.AddProperty("baseServiceUrl", (object) this.WebServiceUrl);
        behaviorDescriptor.AddElementProperty("allSites", this.AllSites.ClientID);
        behaviorDescriptor.AddComponentProperty("allSitesSelector", this.AllSitesSelector.ClientID);
        behaviorDescriptor.AddElementProperty("allSitesDoneSelectingButton", this.AllSitesDoneSelectingButton.ClientID);
        behaviorDescriptor.AddElementProperty("allSitesCancelSelectingButton", this.AllSitesCancelSelectingButton.ClientID);
        behaviorDescriptor.AddElementProperty("customizeSites", this.CustomizeSites.ClientID);
        behaviorDescriptor.AddComponentProperty("customizeSitesSelector", this.CustomizeSitesSelector.ClientID);
        behaviorDescriptor.AddElementProperty("customizeSitesDoneSelectingButton", this.CustomizeSitesDoneSelectingButton.ClientID);
        behaviorDescriptor.AddElementProperty("customizeSitesCancelSelectingButton", this.CustomizeSitesCancelSelectingButton.ClientID);
      }
      if (this.RenderAsDropDown)
        behaviorDescriptor.AddElementProperty("sitesDropDown", this.SitesDropDown.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI))
    {
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Scripts.SiteSelector.js", this.GetType().Assembly.GetName().ToString())
    };
  }
}
