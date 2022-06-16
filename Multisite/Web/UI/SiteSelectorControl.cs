// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SiteSelectorControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite.Web.UI.Designers;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  [ControlDesigner(typeof (SiteSelectorControlDesigner))]
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class SiteSelectorControl : SimpleView
  {
    private bool includeCurrentSite = true;
    private bool useLiveUrl = true;
    private string label = Res.Get<MultisiteResources>("SiteSelectorControlDefaultLabel");
    private Guid currentSiteId;
    private PageManager pageManager;
    private UrlLocalizationService urlService;
    internal const string siteSelectorControlTemplateDisplayName = "Site selector template";
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Multisite.SiteSelectorControl.ascx";
    public static readonly string defaultLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Multisite.SiteSelectorControl.ascx");

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => SiteSelectorControl.defaultLayoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>
    /// Gets or sets the site selector type - drop-down, list or custom
    /// </summary>
    public SiteSelectorControlDesign SelectorType { get; set; }

    /// <summary>Gets or sets the label of the site selector</summary>
    public string Label
    {
      get => this.label;
      set => this.label = value;
    }

    /// <summary>Gets or sets the use live URL flag.</summary>
    public bool UseLiveUrl
    {
      get => this.useLiveUrl;
      set => this.useLiveUrl = value;
    }

    /// <summary>Gets or sets the include current site option</summary>
    public bool IncludeCurrentSite
    {
      get => this.includeCurrentSite;
      set => this.includeCurrentSite = value;
    }

    /// <summary>Gets or sets the show site names and languages option</summary>
    public bool ShowSiteNamesAndLanguages { get; set; }

    /// <summary>Gets or sets the show language only option</summary>
    public bool ShowLanguagesOnly { get; set; }

    /// <summary>Gets the page manager.</summary>
    private PageManager PageMngr
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager();
        return this.pageManager;
      }
    }

    /// <summary>Gets the URL service.</summary>
    private UrlLocalizationService UrlService
    {
      get
      {
        if (this.urlService == null)
          this.urlService = ObjectFactory.Resolve<UrlLocalizationService>();
        return this.urlService;
      }
    }

    /// <summary>The header label for the SiteSelectorControl</summary>
    protected System.Web.UI.WebControls.Label HeaderLabel => this.Container.GetControl<System.Web.UI.WebControls.Label>("headerLabel", true);

    /// <summary>The control that will render the sites</summary>
    protected Repeater SitesRepeater => this.Container.GetControl<Repeater>(this.SelectorType != SiteSelectorControlDesign.ListOfLinks ? (this.SelectorType != SiteSelectorControlDesign.DropDownMenu ? (string) null : "siteSelector_dropDown") : "siteSelectorRepeater_listOfLinks", true);

    /// <summary>
    /// The drop down used to select current site. Only available when in DropDown mode.
    /// </summary>
    protected DropDownList SitesDropDown => this.Container.GetControl<DropDownList>("siteSelector_dropDown", true);

    /// <summary>Panel that represents the error messages</summary>
    protected Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Overridden. Calls Evaluate on the conditional template container to correctly use the controls inside of the templates
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
    }

    /// <summary>Initializes the controls.</summary>
    protected override void InitializeControls(GenericContainer container)
    {
      this.currentSiteId = SystemManager.CurrentContext.CurrentSite.Id;
      List<SiteSelectorControl.SiteInfo> sitesDataSource = this.GetSitesDataSource();
      this.HeaderLabel.Text = this.Label;
      this.BindSitesContainers(sitesDataSource);
    }

    /// <summary>Gets the sites.</summary>
    protected virtual List<SiteSelectorControl.SiteInfo> GetSitesDataSource()
    {
      IEnumerable<ISite> source = SystemManager.CurrentContext.MultisiteContext.GetSites();
      if (!this.IncludeCurrentSite)
        source = source.Where<ISite>((Func<ISite, bool>) (s => !s.IsDefault));
      List<SiteSelectorControl.SiteInfo> sitesDataSource = new List<SiteSelectorControl.SiteInfo>();
      foreach (ISite site in source)
      {
        if (((IEnumerable<CultureInfo>) site.PublicContentCultures).Count<CultureInfo>() > 1 && (this.ShowLanguagesOnly || this.ShowSiteNamesAndLanguages))
        {
          sitesDataSource.AddRange((IEnumerable<SiteSelectorControl.SiteInfo>) this.GetMultilingualSiteInfos(site));
        }
        else
        {
          CultureInfo defaultCulture = site.DefaultCulture;
          bool isCurrentSite = this.currentSiteId == site.Id;
          sitesDataSource.Add(this.GetSiteInfo(site, defaultCulture, isCurrentSite));
        }
      }
      return sitesDataSource;
    }

    /// <summary>
    /// Binds the sites containers (repeater or drop down) with the specified sites
    /// </summary>
    protected virtual void BindSitesContainers(List<SiteSelectorControl.SiteInfo> siteInfos)
    {
      if (this.SelectorType == SiteSelectorControlDesign.DropDownMenu)
      {
        DropDownList sitesDropDown = this.SitesDropDown;
        if (!this.IsDesignMode())
          sitesDropDown.Attributes["onChange"] = "document.location = this.value;";
        foreach (SiteSelectorControl.SiteInfo siteInfo in siteInfos)
        {
          string str = this.FormatUrlProtocol(siteInfo.Url, siteInfo);
          ListItem listItem = new ListItem(siteInfo.DisplayName, str);
          sitesDropDown.Items.Add(listItem);
          if (siteInfo.IsCurrent)
            sitesDropDown.SelectedValue = str;
        }
        if (this.IncludeCurrentSite)
          return;
        ListItem listItem1 = new ListItem(Res.Get<MultisiteResources>("OtherSites"), string.Empty);
        sitesDropDown.Items.Add(listItem1);
        sitesDropDown.SelectedValue = string.Empty;
      }
      else
      {
        this.SitesRepeater.ItemCreated += new RepeaterItemEventHandler(this.SitesRepeater_ItemCreated);
        this.SitesRepeater.DataSource = (object) siteInfos;
        this.SitesRepeater.DataBind();
      }
    }

    /// <summary>
    /// Handles the ItemCreated event of the SitesRepeater control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void SitesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      SiteSelectorControl.SiteInfo dataItem = (SiteSelectorControl.SiteInfo) e.Item.DataItem;
      string displayName = dataItem.DisplayName;
      string url = dataItem.Url;
      if (dataItem.IsCurrent)
      {
        HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("siteHolder");
        string str = "sfSel";
        string attribute = control.Attributes["class"];
        control.Attributes.Add("class", string.IsNullOrEmpty(attribute) ? str : attribute + " " + str);
      }
      else
        ((HtmlAnchor) e.Item.FindControl("siteLink")).HRef = this.FormatUrlProtocol(url, dataItem);
      ((HtmlContainerControl) e.Item.FindControl("siteName")).InnerText = displayName;
    }

    /// <summary>Gets the name of the displayed language.</summary>
    protected virtual string GetDisplayedLanguageName(CultureInfo lang) => lang.NativeName;

    /// <inheritdoc />
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Gets the multilingual site infos.</summary>
    private List<SiteSelectorControl.SiteInfo> GetMultilingualSiteInfos(
      ISite site)
    {
      List<SiteSelectorControl.SiteInfo> multilingualSiteInfos = new List<SiteSelectorControl.SiteInfo>();
      bool flag = true;
      foreach (CultureInfo publicContentCulture in site.PublicContentCultures)
      {
        string siteUrl = string.Empty;
        bool isCurrentSite = false;
        if (site.Id == this.currentSiteId)
        {
          PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
          PageNode pageNode = this.PageMngr.GetPageNode(actualCurrentNode.Id);
          flag = ((IEnumerable<CultureInfo>) pageNode.AvailableCultures).Contains<CultureInfo>(publicContentCulture);
          if (flag)
          {
            if (actualCurrentNode.UiCulture == publicContentCulture.Name)
              isCurrentSite = true;
            siteUrl = this.ResolveDefaultSiteUrl(pageNode, publicContentCulture);
          }
        }
        else
        {
          using (new SiteRegion(site))
            siteUrl = this.ResolveSiteHomePage(site, publicContentCulture);
        }
        if (flag)
          multilingualSiteInfos.Add(this.GetSiteInfo(site, publicContentCulture, isCurrentSite, siteUrl));
      }
      return multilingualSiteInfos;
    }

    /// <summary>
    /// Resolves the home page of the site and add the culture to it.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <param name="culture">The site culture.</param>
    /// <returns>Homepage URL.</returns>
    protected virtual string ResolveSiteHomePage(ISite site, CultureInfo culture)
    {
      string str = string.Empty;
      string url = VirtualPathUtility.RemoveTrailingSlash(this.UrlService.ResolveUrl("/", culture));
      if (!RouteHelper.IsAbsoluteUrl(url))
      {
        if (!url.StartsWith("/"))
          url = !url.StartsWith("~/") ? "/" + url : url.Replace("~/", "/");
        str = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.RemoveTrailingSlash(this.GetSiteUrl(site)) + url);
      }
      return str;
    }

    /// <summary>Gets the site info.</summary>
    /// <param name="site">The site.</param>
    /// <param name="cultureInfo">The culture info.</param>
    /// <param name="isCurrentSite">The is current site.</param>
    private SiteSelectorControl.SiteInfo GetSiteInfo(
      ISite site,
      CultureInfo cultureInfo,
      bool isCurrentSite,
      string siteUrl = null)
    {
      if (siteUrl == null)
        siteUrl = this.GetSiteUrl(site);
      string siteDisplayName = this.GetSiteDisplayName(site.Name, cultureInfo);
      return new SiteSelectorControl.SiteInfo()
      {
        DisplayName = siteDisplayName,
        Url = siteUrl,
        RequiresSsl = site.RequiresSsl,
        IsCurrent = isCurrentSite
      };
    }

    /// <summary>
    /// Gets the site url based on UseLiveUrl setting of the SiteSelector
    /// </summary>
    private string GetSiteUrl(ISite site)
    {
      string siteUrl = site.StagingUrl;
      if (this.UseLiveUrl)
        siteUrl = site.LiveUrl;
      if (!siteUrl.EndsWith("/"))
        siteUrl += "/";
      return siteUrl;
    }

    /// <summary>Formats the url protocol if needed</summary>
    private string FormatUrlProtocol(string url, SiteSelectorControl.SiteInfo siteInfo)
    {
      string str = url;
      if (!string.IsNullOrEmpty(url) && !url.StartsWith("http"))
        str = !siteInfo.RequiresSsl ? "http://" + url : "https://" + url;
      return str;
    }

    /// <summary>Gets the display name of the site.</summary>
    /// <param name="siteName">The name of the site.</param>
    /// <param name="cultureInfo">The culture info.</param>
    /// <returns>Site display name according to settings</returns>
    private string GetSiteDisplayName(string siteName, CultureInfo cultureInfo) => !this.ShowLanguagesOnly || cultureInfo == null ? (!this.ShowSiteNamesAndLanguages || cultureInfo == null ? siteName : string.Format("{0} - {1}", (object) siteName, (object) this.GetDisplayedLanguageName(cultureInfo))) : this.GetDisplayedLanguageName(cultureInfo);

    /// <summary>Resolves the default site URL.</summary>
    /// <param name="cultureInfo">The culture info.</param>
    /// <param name="actualPageNode">The actual page node.</param>
    private string ResolveDefaultSiteUrl(PageNode actualPageNode, CultureInfo cultureInfo)
    {
      string url = this.UrlService.ResolveUrl(actualPageNode.GetFullUrl(cultureInfo, false, false), cultureInfo);
      string siteUrl = this.GetSiteUrl(SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(actualPageNode.RootNodeId != Guid.Empty ? actualPageNode.RootNodeId : actualPageNode.Id));
      string str = RouteHelper.ResolveUrl(url, UrlResolveOptions.CurrentRequestRelative);
      return VirtualPathUtility.RemoveTrailingSlash(siteUrl) + str;
    }

    /// <summary>
    /// Used for sending basing site information to dropdown and list when binding
    /// </summary>
    protected struct SiteInfo
    {
      public string DisplayName { get; set; }

      public string Url { get; set; }

      public bool RequiresSsl { get; set; }

      public bool IsCurrent { get; set; }
    }
  }
}
