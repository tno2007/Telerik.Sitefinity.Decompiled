// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageEditorRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Represents a route handler for Sitefinity's page editor.
  /// </summary>
  public class PageEditorRouteHandler : RouteHandler
  {
    private CultureInfo culture;
    private bool? isBackend;

    /// <summary>
    /// Gets or sets a value indicating whether the requested page is for preview or edit mode.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    public bool IsPreview { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the requested page is for inline editing mode.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    internal bool IsInlineEditing { get; set; }

    /// <summary>Gets or sets the object edit culture.</summary>
    public CultureInfo ObjectEditCulture
    {
      get
      {
        if (this.culture != null)
          return this.culture;
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        return appSettings.Multilingual && this.IsBackend ? appSettings.DefaultBackendLanguage : appSettings.DefaultFrontendLanguage;
      }
      set => this.culture = value;
    }

    /// <summary>Gets a value indicating whether is backend.</summary>
    public bool IsBackend
    {
      get
      {
        if (!this.isBackend.HasValue)
          this.isBackend = BackendSiteMap.GetCurrentProvider().CurrentNode == null ? new bool?(false) : new bool?(true);
        return this.isBackend.Value;
      }
    }

    /// <inheritdoc />
    protected override IHttpHandler BuildHandler(
      RequestContext requestContext,
      IPageData pageData)
    {
      Page handler = this.GetHandler(requestContext, pageData);
      handler.Init += (EventHandler) ((sender, args) =>
      {
        if (!this.IsInlineEditing && !this.IsPreview)
          return;
        if (this.IsPreview)
          SystemManager.IsFrontEndControlRender = true;
        Page page = (Page) sender;
        PageSiteNode firstPageDataNode = RouteHelper.GetFirstPageDataNode((PageSiteNode) requestContext.RouteData.DataTokens["SiteMapNode"], true);
        if (firstPageDataNode == null)
          throw new InvalidOperationException("Invalid SiteMap node specified. Either the current group node doesn't have child nodes or the current user does not have rights to view any of the child nodes.");
        page.Controls.AddAt(0, (Control) new ScriptManagerWrapper()
        {
          PageId = firstPageDataNode.PageId,
          PageVersion = firstPageDataNode.Version,
          PageStatus = firstPageDataNode.Status,
          Published = firstPageDataNode.Visible
        });
        page.Controls.Add((Control) new SitefinityRequiredControls());
        PageHelper.AddSeoAndOpenGraphControls(page, firstPageDataNode);
      });
      return (IHttpHandler) handler;
    }

    /// <summary>Sets the page directives.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="pageData">The page data.</param>
    protected override void SetPageDirectives(Page handler, IPageData pageData) => ((DraftProxyBase) pageData).SetPageDirectives(handler);

    /// <summary>Gets the page template.</summary>
    /// <param name="pageData">The page data.</param>
    /// <returns>The page template</returns>
    protected override ITemplate GetPageTemplate(IPageData pageData) => ((DraftProxyBase) pageData).GetPageTemplate();

    /// <summary>
    /// Applies the layouts and controls to the provided page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="requestContext">The request context.</param>
    protected override void ApplyLayoutsAndControls(Page page, RequestContext requestContext)
    {
      SiteMapNode dataToken = (SiteMapNode) requestContext.RouteData.DataTokens["SiteMapNode"];
      DraftProxyBase pageData = this.GetPageData(dataToken) as DraftProxyBase;
      Guid lockedBy;
      if (this.IsLocked((PageSiteNode) dataToken, out lockedBy))
        pageData.LockedBy = lockedBy;
      page.Items[(object) "SF_PageUrlEvaluationMode"] = (object) pageData.UrlEvaluationMode;
      page.Items[(object) "validateItem"] = (object) true;
      pageData.ApplyLayouts(page);
      pageData.CreateChildControls(page);
    }

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <returns>The page data.</returns>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    protected override IPageData GetPageData(SiteMapNode node)
    {
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      Telerik.Sitefinity.Modules.Pages.PageDraftProxy pageData1 = (Telerik.Sitefinity.Modules.Pages.PageDraftProxy) items[(object) "StaticPageDraft"];
      if (pageData1 == null)
      {
        if (!(node is PageSiteNode siteNode))
          return (IPageData) null;
        PageManager manager = PageManager.GetManager();
        bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
        manager.Provider.SuppressSecurityChecks = true;
        if (this.ObjectEditCulture != null)
          SystemManager.CurrentContext.Culture = this.ObjectEditCulture;
        try
        {
          PageNode pageNode = manager.GetPageNode(siteNode.Id);
          if (SystemManager.CurrentContext.AppSettings.Multilingual && pageNode.GetPageData() != null && !((IEnumerable<CultureInfo>) pageNode.AvailableCultures).Contains<CultureInfo>(this.ObjectEditCulture))
            throw new HttpException(404, "The requested page does not exist.");
          Guid personalizationSegment = this.GetCurrentPersonalizationSegment();
          if ((this.IsInlineEditing || this.IsPreview) && !siteNode.IsBackend && personalizationSegment == Guid.Empty)
          {
            string variationKey = siteNode.GetVariationKey();
            if (!string.IsNullOrEmpty(variationKey))
              personalizationSegment = Guid.Parse(variationKey);
          }
          PageData pageData2 = manager.GetPageData(pageNode, this.ObjectEditCulture, personalizationSegment);
          bool optimized = true;
          PageDraft pageData3;
          if (this.IsLocked(pageNode, out Guid _))
          {
            if (this.IsInlineEditing)
            {
              pageData3 = manager.PagesLifecycle.GetMaster(pageData2);
            }
            else
            {
              pageData3 = manager.CreateDraft<PageDraft>();
              pageData3.ParentPage = pageData2;
              pageData3.IsPersonalized = false;
            }
          }
          else if (this.IsPreview)
          {
            pageData3 = manager.GetPreview(pageData2.Id);
            optimized = false;
          }
          else
          {
            if (this.IsInlineEditing)
            {
              pageData3 = manager.PagesLifecycle.GetTemp(pageData2);
              if (pageData3 == null)
              {
                pageData3 = manager.PagesLifecycle.GetMaster(pageData2);
                if (pageData3 == null)
                {
                  pageData3 = manager.CreateDraftForPage(pageData2, this.ObjectEditCulture);
                  pageData2.Drafts.Add(pageData3);
                }
              }
              pageData3.IsPersonalized = personalizationSegment != Guid.Empty;
              if (personalizationSegment != Guid.Empty)
              {
                pageData3.PersonalizationMasterId = pageNode.GetPageData().Id;
                pageData3.PersonalizationSegmentId = personalizationSegment;
              }
            }
            else
            {
              pageData3 = manager.EditPage(pageData2.Id);
              pageData3.IsPersonalized = false;
            }
            manager.SaveChanges();
          }
          pageData1 = new Telerik.Sitefinity.Modules.Pages.PageDraftProxy(pageData3, pageNode, manager.Provider, this.IsPreview, this.ObjectEditCulture, optimized, this.IsInlineEditing);
          items[(object) "StaticPageDraft"] = (object) pageData1;
        }
        finally
        {
          if (this.IsPreview)
            manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      return (IPageData) pageData1;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public override IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      SystemManager.SetPageDesignMode(true);
      SystemManager.SetPagePreviewMode(this.IsPreview);
      SystemManager.SetInlineEditingMode(this.IsInlineEditing);
      return base.GetHttpHandler(requestContext);
    }

    /// <inheritdoc />
    protected override void InitializeHttpHandler(Page handler, RequestContext requestContext)
    {
      base.InitializeHttpHandler(handler, requestContext);
      handler.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    private bool IsLocked(PageNode pn, out Guid lockedBy)
    {
      if (this.IsPreview)
      {
        if (!pn.IsGranted("Pages", "View"))
          throw new SecurityDemandFailException("You are not authorized to view the content of this page.");
      }
      if (!this.IsPreview && !this.IsInlineEditing)
      {
        if (!pn.IsGranted("Pages", "EditContent"))
          throw new SecurityDemandFailException("You are not authorized to edit the content of this page.");
      }
      PageData pageData = pn.GetPageData();
      if (pageData != null)
      {
        Guid currentUserId = SecurityManager.GetCurrentUserId();
        lockedBy = pageData.LockedBy;
        if (pageData.LockedBy != Guid.Empty && pageData.LockedBy != currentUserId && !this.IsPreview)
          return true;
      }
      lockedBy = Guid.Empty;
      return false;
    }

    private bool IsLocked(PageSiteNode pageSiteNode, out Guid lockedBy)
    {
      if (pageSiteNode != null)
        return this.IsLocked(PageManager.GetManager().GetPageNode(pageSiteNode.Id), out lockedBy);
      lockedBy = Guid.Empty;
      return false;
    }

    private Guid GetCurrentPersonalizationSegment() => SystemManager.CurrentHttpContext.Request.QueryString["segment"] != null ? new Guid(SystemManager.CurrentHttpContext.Request.QueryString["segment"]) : Guid.Empty;
  }
}
