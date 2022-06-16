// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.VersioningRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Handle Page/Template version requests</summary>
  public class VersioningRouteHandler : RouteHandler
  {
    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public override IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      string name = requestContext.HttpContext.Request.QueryString["uiCulture"];
      if (!name.IsNullOrEmpty())
        SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(name);
      else
        RouteHelper.ApplyThreadCulturesForCurrentUser();
      try
      {
        requestContext.HttpContext.Items[(object) "versionpreview"] = (object) true;
        Guid guid1 = new Guid(requestContext.RouteData.Values["itemId"].ToString());
        Guid guid2 = new Guid(requestContext.RouteData.Values["VersionNumber"].ToString());
        bool result = false;
        if (requestContext.HttpContext.Request.QueryString["IsTemplate"] != null)
          bool.TryParse(requestContext.HttpContext.Request.QueryString["IsTemplate"].ToString(), out result);
        PageManager manager1 = PageManager.GetManager();
        VersionManager manager2 = VersionManager.GetManager();
        HttpContextBase httpContext = requestContext.HttpContext;
        if (result)
        {
          IRendererCommonData lastContainer = manager1.GetTemplate(guid1).GetLastContainer<IRendererCommonData>();
          if (lastContainer.IsExternallyRendered())
          {
            string previewUrl = UrlPath.ResolveAbsoluteUrl(PageTemplateViewModel.GetPreviewUrl(guid1));
            return this.RedirectToExternalRendererHandler(httpContext, lastContainer.Renderer, guid2, previewUrl);
          }
          TemplateDraft templatePreview = manager1.GetTemplatePreview(guid1);
          manager1.SaveChanges();
          requestContext.HttpContext.Items[(object) "IsTemplate"] = (object) true;
          manager2.GetSpecificVersionByChangeId((object) templatePreview, guid2);
          TemplateDraftProxy templateDraftProxy = new TemplateDraftProxy(templatePreview, manager1.Provider, true, (CultureInfo) null);
          requestContext.RouteData.DataTokens.Add("draft", (object) templateDraftProxy);
          requestContext.HttpContext.Items[(object) "CurrentVersionTemplateId"] = (object) templatePreview.TemplateId.ToString();
          return this.BuildHandler(requestContext, (IPageData) templateDraftProxy);
        }
        PageDraft preview = manager1.GetPreview(guid1);
        PageSiteNode siteMapNode = preview.ParentPage.NavigationNode.GetSiteMapNode();
        if (siteMapNode.IsExternallyRendered())
          return this.RedirectToExternalRendererHandler(httpContext, siteMapNode.CurrentPageDataItem.Renderer, guid2, siteMapNode.GetPagePreviewUrl());
        manager1.SaveChanges();
        requestContext.HttpContext.Items[(object) "IsFrontendPageEdit"] = (object) !preview.ParentPage.NavigationNode.IsBackend;
        manager2.GetSpecificVersionByChangeId((object) preview, guid2);
        preview.Themes[SystemManager.CurrentContext.Culture] = "";
        Telerik.Sitefinity.Modules.Pages.PageDraftProxy pageDraftProxy = new Telerik.Sitefinity.Modules.Pages.PageDraftProxy(preview, (PageNode) null, manager1.Provider, true, (CultureInfo) null);
        requestContext.RouteData.DataTokens.Add("draft", (object) pageDraftProxy);
        this.SetRequestVariables(preview, requestContext);
        requestContext.HttpContext.Items[(object) "CurrentVersionTemplateId"] = (object) preview.TemplateId.ToString();
        return this.BuildHandler(requestContext, (IPageData) pageDraftProxy);
      }
      catch (TemplateNotFoundException ex)
      {
        if (Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions))
        {
          throw;
        }
        else
        {
          requestContext.HttpContext.Response.Write(Res.Get<PageResources>().PageTemplateNotFound);
          requestContext.HttpContext.Response.End();
          return (IHttpHandler) null;
        }
      }
    }

    /// <summary>Sets the page directives.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="pageData">The page data.</param>
    protected override void SetPageDirectives(Page handler, IPageData pageData) => ((DraftProxyBase) pageData).SetPageDirectives(handler);

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected override IPageData GetPageData(SiteMapNode node) => throw new NotImplementedException();

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
      DraftProxyBase dataToken = requestContext.RouteData.DataTokens["draft"] as DraftProxyBase;
      dataToken.ApplyLayouts(page);
      dataToken.CreateChildControls(page);
    }

    /// <summary>
    /// Initializes the content of a backend page.
    /// The main navigation is added prior to this method.
    /// </summary>
    /// <param name="handler">A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.</param>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    protected override void InitializeContent(Page handler, RequestContext requestContext)
    {
      RouteHandler.EnsureScriptManager(handler);
      ResourceLinks child1 = new ResourceLinks()
      {
        UseEmbeddedThemes = true
      };
      child1.Links.Add(new ResourceFile()
      {
        Name = "Telerik.Sitefinity.Resources.Themes.Light.Styles.Video.css",
        Static = true
      });
      handler.Form.Controls.Add((Control) child1);
      base.InitializeContent(handler, requestContext);
      if (handler.Form == null)
        return;
      handler.Form.Attributes.Add("class", "sfPagePreviewWrp");
      handler.Form.Style.Add("position", "relative");
      handler.Form.Style.Add("height", "100%");
      HtmlGenericControl child2 = new HtmlGenericControl("div");
      child2.Attributes.Add("class", "sfPagePreviewOverlay");
      string webResourceUrl = handler.ClientScript.GetWebResourceUrl(TypeResolutionService.ResolveType("Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources"), "Telerik.Sitefinity.Resources.Themes.Light.Images.sfBlank.gif");
      child2.Attributes.Add("style", "position: absolute; top: 0; right: 0; bottom: 0; left: 0; width: 100%; height: 100%; z-index: 10000; background-image: url({0});".Arrange((object) webResourceUrl));
      handler.Form.Controls.Add((Control) child2);
    }

    private IHttpHandler RedirectToExternalRendererHandler(
      HttpContextBase httpContext,
      string rendererName,
      Guid versionId,
      string previewUrl)
    {
      RequestContext requestContext = httpContext.Request.RequestContext;
      if (!UrlPath.IsKnownProxy(httpContext))
        return new SiteOfflineRouteHandler(Res.Get<Labels>().CannotProcessExternalRendererPage.Arrange((object) rendererName))
        {
          Title = "Cannot process page"
        }.GetHttpHandler(requestContext);
      string pathAndQuery = new Uri(previewUrl).PathAndQuery;
      string str = "?";
      if (pathAndQuery.Contains("?"))
        str = "&";
      string url = pathAndQuery + string.Format("{0}sfversion={1}", (object) str, (object) versionId);
      requestContext.HttpContext.Response.Redirect(url, false);
      if (requestContext.HttpContext.ApplicationInstance != null)
        requestContext.HttpContext.ApplicationInstance.CompleteRequest();
      return (IHttpHandler) new VersioningRouteHandler.EmptyHttpHandler();
    }

    private void SetRequestVariables(PageDraft draft, RequestContext requestContext)
    {
      if (draft.ParentPage != null && draft.ParentPage.NavigationNode != null)
      {
        SiteMapProvider providerForPageNode = SiteMapBase.GetSiteMapProviderForPageNode(draft.ParentPage.NavigationNode);
        PageSiteNode siteMapNodeFromKey = providerForPageNode.FindSiteMapNodeFromKey(draft.ParentPage.NavigationNode.Id.ToString()) as PageSiteNode;
        requestContext.HttpContext.Items[(object) "ServedPageNode"] = (object) siteMapNodeFromKey;
        requestContext.HttpContext.Items[(object) "SF_SiteMap"] = (object) providerForPageNode;
      }
      else
        requestContext.HttpContext.Items[(object) "SF_SiteMap"] = (object) SitefinitySiteMap.GetCurrentProvider();
    }

    private class EmptyHttpHandler : IHttpHandler
    {
      public bool IsReusable => false;

      public void ProcessRequest(HttpContext context) => throw new NotImplementedException();
    }
  }
}
