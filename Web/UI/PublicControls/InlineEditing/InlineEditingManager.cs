// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Analytics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing
{
  /// <summary>
  /// Representing a class for managing the inline editing controls
  /// </summary>
  public class InlineEditingManager : SimpleView
  {
    internal const string scriptName = "Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.Scripts.InlineEditingManager.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.InlineEditing.InlineEditingManager.ascx");
    private bool currentSiteMapNodeSet;
    private PageSiteNode currentSiteMapNode;
    private bool currentPageNodeSet;
    private PageNode currentPageNode;
    private bool? visible;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager" /> class.
    /// </summary>
    public InlineEditingManager() => this.Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager" /> class.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageVersion">The page version.</param>
    /// <param name="pageStatus">The page status.</param>
    public InlineEditingManager(Guid pageId, int pageVersion, ContentLifecycleStatus pageStatus)
    {
      this.PageId = pageId;
      this.PageVersion = pageVersion;
      this.PageStatus = pageStatus;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? InlineEditingManager.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the visibility.</summary>
    /// <value>The visibility.</value>
    public override bool Visible
    {
      get => this.visible.HasValue ? this.visible.Value : ControlExtensions.InlineEditingIsEnabled();
      set => this.visible = new bool?(value);
    }

    /// <summary>
    /// Represents the id of the current page data when loaded
    /// </summary>
    public Guid PageId { get; private set; }

    /// <summary>
    /// Represents te version of the current page data when loaded
    /// </summary>
    public int PageVersion { get; private set; }

    /// <summary>The content lifecycle status of the page when loaded</summary>
    public ContentLifecycleStatus PageStatus { get; private set; }

    /// <summary>Gets the current site map node.</summary>
    /// <value>The current site map node.</value>
    protected PageSiteNode CurrentSiteMapNode
    {
      get
      {
        if (!this.currentSiteMapNodeSet)
        {
          this.currentSiteMapNode = SiteMapBase.GetActualCurrentNode();
          this.currentSiteMapNodeSet = true;
        }
        return this.currentSiteMapNode;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> object for the current site map node.
    /// </summary>
    /// <value>The current PageNode object.</value>
    protected PageNode CurrentPageNode
    {
      get
      {
        if (!this.currentPageNodeSet)
        {
          PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
          if (currentSiteMapNode != null)
            this.currentPageNode = PageManager.GetManager().GetPageNode(currentSiteMapNode.Id);
          this.currentPageNodeSet = true;
        }
        return this.currentPageNode;
      }
    }

    public HtmlInputHidden PageIdHiddenField => this.Container.GetControl<HtmlInputHidden>("pageId", true);

    public HtmlInputHidden BaseUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("baseUrl", true);

    internal HtmlInputHidden RedirectToRootHiddenField => this.Container.GetControl<HtmlInputHidden>("redirectToRoot", true);

    public HtmlInputHidden PageUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("pageUrl", true);

    public HtmlInputHidden ServiceUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("serviceUrl", true);

    public HtmlInputHidden ImageServiceUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("imageServiceUrl", true);

    public HtmlInputHidden FlatTaxonServiceUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("flatTaxonServiceUrl", true);

    public HtmlInputHidden IsPageLockedHiddenField => this.Container.GetControl<HtmlInputHidden>("isPageLocked", true);

    public HtmlInputHidden CurrentUICultureHiddenField => this.Container.GetControl<HtmlInputHidden>("currentUICulture", true);

    public HtmlInputHidden SiteBaseUrlHiddenField => this.Container.GetControl<HtmlInputHidden>("siteBaseUrl", true);

    public HyperLink AnalyticsStatsLink => this.Container.GetControl<HyperLink>("analyticsStatsLink", true);

    /// <summary>
    /// Gets a reference to the button that opens the page in the backend edit screen.
    /// </summary>
    public HyperLink EditPageInBackendLink => this.Container.GetControl<HyperLink>("editPageInBackendLink", true);

    public ResourceLinks KendoStylesResources => this.Container.GetControl<ResourceLinks>("kendoStyles", false);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager" /> instance for a given <see cref="T:System.Web.UI.Page" />
    /// object.
    /// </summary>
    /// <returns>
    /// The current <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager" /> instance for the selected
    /// <see cref="T:System.Web.UI.Page" /> object, or null if no instance is defined.
    /// </returns>
    /// <param name="page">
    /// The page instance to retrieve the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.InlineEditingManager" />
    /// from.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="page" /> is null.
    /// </exception>
    public static InlineEditingManager GetCurrent(Page page)
    {
      if (page == null)
        throw new ArgumentNullException(Res.Get<ErrorMessages>().PageIsNull);
      return (InlineEditingManager) page.Items[(object) typeof (InlineEditingManager).FullName];
    }

    /// <inheritdoc />
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
      writer.RenderBeginTag(this.TagKey);
    }

    /// <inheritdoc />
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
      this.PageIdHiddenField.Value = currentSiteMapNode.Key;
      this.BaseUrlHiddenField.Value = (HostingEnvironment.ApplicationVirtualPath + "/ExtRes").Replace("//", "/");
      this.ServiceUrlHiddenField.Value = VirtualPathUtility.ToAbsolute("~/RestApi/Sitefinity/inlineediting");
      this.ImageServiceUrlHiddenField.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/");
      this.FlatTaxonServiceUrlHiddenField.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc");
      this.IsPageLockedHiddenField.Value = (currentSiteMapNode.CurrentPageDataItem.LockedBy != Guid.Empty && currentSiteMapNode.CurrentPageDataItem.LockedBy != SecurityManager.GetCurrentUserId()).ToString().ToLower();
      this.CurrentUICultureHiddenField.Value = SystemManager.CurrentContext.Culture.Name;
      this.SiteBaseUrlHiddenField.Value = HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/";
      this.EditPageInBackendLink.NavigateUrl = this.GetPageEditUrl();
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      string str = actualCurrentNode.UnresolvedUrl;
      if (SiteMapBase.GetCurrentProvider().FindSiteMapNode(HttpContext.Current) is PageSiteNode siteMapNode && siteMapNode.NodeType == NodeType.Group)
        str = str.Replace(siteMapNode.UnresolvedUrl, string.Empty);
      this.PageUrlHiddenField.Value = HttpUtility.UrlPathEncode(PageManager.RemoveExtension(str.TrimStart('~', '/'), out string _));
      bool flag = false;
      if (actualCurrentNode.IsHomePage() && Config.Get<PagesConfig>().OpenHomePageAsSiteRoot)
        flag = true;
      this.RedirectToRootHiddenField.Value = flag.ToString().ToLower();
      this.AnalyticsStatsLink.Visible = SystemManager.IsModuleEnabled("Analytics");
      if (this.AnalyticsStatsLink.Visible && ObjectFactory.IsTypeRegistered<IAnalyticsApiAccessManager>())
      {
        this.AnalyticsStatsLink.NavigateUrl = ObjectFactory.Resolve<IAnalyticsApiAccessManager>().GetTopContentReportPath(this.GetPageUrl(), currentSiteMapNode.Id);
        this.AnalyticsStatsLink.Text = "Page Analytics";
      }
      if (this.KendoStylesResources == null)
        return;
      this.KendoStylesResources.Visible = SystemManager.IsInlineEditingMode;
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Initializes this instance.</summary>
    private void Initialize()
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      if (this.DesignMode)
        return;
      if (InlineEditingManager.GetCurrent(this.Page) != null)
        throw new Exception("More than one InlineEditingManagers");
      this.Page.Items[(object) typeof (InlineEditingManager).FullName] = (object) this;
    }

    /// <summary>Gets the page URL.</summary>
    /// <returns></returns>
    protected string GetPageUrl()
    {
      string pageUrl = string.Empty;
      PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
      if (currentSiteMapNode != null)
        pageUrl = UrlPath.ResolveUrl(currentSiteMapNode.GetLiveUrl());
      return pageUrl;
    }

    /// <summary>
    /// Determines whether the user can edit the contents of the specified page.
    /// </summary>
    /// <param name="page">The page node to be checked.</param>
    /// <returns>
    /// 	<c>true</c> if the user can edit the contents of the specified page; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsContentEditable(PageNode page)
    {
      if (page == null)
        return true;
      return page.IsGranted("Pages", "EditContent");
    }

    /// <summary>Gets the page edit URL in the backend.</summary>
    /// <returns></returns>
    protected string GetPageEditUrl()
    {
      string pageEditUrl = string.Empty;
      PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
      if (currentSiteMapNode != null)
      {
        CultureInfo culture = (CultureInfo) null;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          culture = SystemManager.CurrentContext.Culture;
        string pageEditBackendUrl = currentSiteMapNode.GetPageEditBackendUrl(culture);
        string str = pageEditBackendUrl.Contains("?") ? "&" : "?";
        pageEditUrl = pageEditBackendUrl + str + ("sf_site=" + SystemManager.CurrentContext.CurrentSite.Id.ToString());
      }
      return pageEditUrl;
    }
  }
}
