// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateEditorRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents a route handler for Sitefinity's template editor.
  /// </summary>
  public class TemplateEditorRouteHandler : RouteHandler
  {
    private CultureInfo culture;
    private bool? isBackend;

    /// <summary>
    /// Gets or sets a value indicating whether the requested page is for preview or edit mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    public bool IsPreview { get; set; }

    /// <summary>
    /// Gets or sets the the culture in which the template object will be edited
    /// </summary>
    /// <value>The culture of the template object</value>
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

    /// <summary>
    /// Gets a value indicating whether this template instance is backend - used in the Sitefinity administration.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend; otherwise, <c>false</c>.
    /// </value>
    public bool IsBackend
    {
      get
      {
        if (!this.isBackend.HasValue)
          this.isBackend = BackendSiteMap.GetCurrentProvider().CurrentNode == null ? new bool?(false) : new bool?(true);
        return this.isBackend.Value;
      }
    }

    /// <summary>Sets the page directives.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="pageData">The page data.</param>
    protected override void SetPageDirectives(Page handler, IPageData pageData) => ((DraftProxyBase) pageData).SetPageDirectives(handler);

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected override IPageData GetPageData(SiteMapNode node)
    {
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      TemplateDraftProxy pageData1 = (TemplateDraftProxy) items[(object) "TemplateDraftProxy"];
      if (pageData1 == null)
      {
        if (!(node is TemplateSiteNode tNode))
          return (IPageData) null;
        PageManager manager = PageManager.GetManager(((SiteMapBase) tNode.Provider).PageProviderName);
        if (this.ObjectEditCulture != null)
          SystemManager.CurrentContext.Culture = this.ObjectEditCulture;
        PageTemplate template = manager.GetTemplate(tNode.Id);
        if (SystemManager.CurrentContext.AppSettings.Multilingual && !((IEnumerable<CultureInfo>) template.AvailableCultures).Contains<CultureInfo>(this.ObjectEditCulture))
          throw new HttpException(404, "The requested page does not exist.");
        Guid lockedBy;
        int num = this.IsLocked(tNode, out lockedBy) ? 1 : 0;
        bool optimized = true;
        TemplateDraft pageData2;
        if (num != 0)
        {
          pageData2 = manager.CreateDraft<TemplateDraft>();
          pageData2.ParentTemplate = template;
        }
        else if (this.IsPreview)
        {
          pageData2 = manager.GetTemplatePreview(tNode.Id);
          optimized = false;
        }
        else
        {
          pageData2 = manager.EditTemplate(tNode.Id, this.ObjectEditCulture);
          manager.SaveChanges();
        }
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
          currentHttpContext.Items[(object) "IsFrontendPageEdit"] = (object) (template.Category != SiteInitializer.BackendTemplatesCategoryId);
        pageData1 = new TemplateDraftProxy(pageData2, manager.Provider, this.IsPreview, this.ObjectEditCulture, optimized);
        pageData1.LockedBy = lockedBy;
        items[(object) "TemplateDraftProxy"] = (object) pageData1;
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
      return base.GetHttpHandler(requestContext);
    }

    /// <summary>Gets the page template.</summary>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    protected override ITemplate GetPageTemplate(IPageData pageData) => ((DraftProxyBase) pageData).GetPageTemplate();

    /// <summary>
    /// Applies the layouts and controls to the provided page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="requestContext">The request context.</param>
    protected override void ApplyLayoutsAndControls(Page page, RequestContext requestContext)
    {
      SiteMapNode dataToken = (SiteMapNode) requestContext.RouteData.DataTokens["SiteMapNode"];
      page.Items[(object) "IsTemplate"] = (object) true;
      DraftProxyBase pageData = (DraftProxyBase) this.GetPageData(dataToken);
      pageData.ApplyLayouts(page);
      pageData.CreateChildControls(page);
    }

    private bool IsLocked(TemplateSiteNode tNode, out Guid lockedBy)
    {
      if (tNode == null)
      {
        lockedBy = Guid.Empty;
        return false;
      }
      PageTemplate template = PageManager.GetManager(((SiteMapBase) tNode.Provider).PageProviderName).GetTemplate(tNode.Id);
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      lockedBy = template.LockedBy;
      return template.LockedBy != Guid.Empty && template.LockedBy != currentUserId && !this.IsPreview;
    }
  }
}
