// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceCombining.InMemoryPageRender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.ResourceCombining
{
  /// <summary>
  /// Functionality for rendering a page in memory and resolving the script references from it
  /// </summary>
  public class InMemoryPageRender
  {
    internal const string IsInIndexMode = "IsInIndexMode";

    /// <summary>
    /// Creates <see cref="T:System.Web.HttpContext" /> object for the provided URL.
    /// </summary>
    /// <param name="rawUrl">The URL to create context for.</param>
    /// <param name="writer">The writer.</param>
    /// <returns>A <see cref="T:System.Web.HttpContext" /> object.</returns>
    public static HttpContext CreateHttpContext(string rawUrl, TextWriter writer) => RouteHelper.CreateHttpContext(rawUrl, writer);

    /// <summary>Renders the page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <returns></returns>
    public string RenderPage(PageNode pageNode) => this.RenderPageInternal(pageNode, false, true, new Guid?());

    /// <summary>Renders the page.</summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="isPreview">if set to <c>true</c> then the method renders the draft version of the page.</param>
    /// <param name="isIndexMode">if set to <c>true</c> then the page is rendered in index mode.</param>
    /// <param name="segmentId">The segment identifier.</param>
    /// <returns>The output string of the page.</returns>
    public string RenderPage(PageNode pageNode, bool isPreview, bool isIndexMode, Guid? segmentId = null) => this.RenderPageInternal(pageNode, isPreview, isIndexMode, segmentId);

    private string RenderPageInternal(
      PageNode pageNode,
      bool isPreview,
      bool isIndexMode,
      Guid? segmentId)
    {
      string str = string.Empty;
      if (pageNode == null || pageNode.NodeType == NodeType.Group || pageNode.HasLinkedNode())
        return string.Empty;
      string url = (string) null;
      if (pageNode.NodeType == NodeType.External)
        url = pageNode.GetPageData().ExternalPage;
      else if (pageNode.NodeType == NodeType.OuterRedirect)
        url = pageNode.RedirectUrl.ToString();
      if (url != null)
        str = RouteHelper.GetWebContent(UrlPath.ResolveUrl(url, true));
      else if (pageNode.NodeType == NodeType.Standard)
      {
        using (StringWriter writer1 = new StringWriter())
        {
          using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
          {
            ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
            SystemManager.CurrentTransactions = (ContextTransactions) null;
            HttpContext current = HttpContext.Current;
            try
            {
              IDataItem pageData = (IDataItem) pageNode.GetPageData();
              if (pageData != null)
              {
                PageDataProvider provider = (PageDataProvider) pageData.GetProvider();
                HttpContext context;
                Page page = isPreview ? this.GetPageDraft(pageNode, provider, writer2, segmentId, out context) : this.GetPage(pageNode, provider, writer2, segmentId, out context);
                if (isIndexMode)
                  page.Items[(object) "IsInIndexMode"] = (object) true;
                HttpContext.Current = context;
                context.Handler = (IHttpHandler) page;
                page.ProcessRequest(context);
              }
            }
            finally
            {
              this.DisposeTransactions();
              HttpContext.Current = current;
              SystemManager.CurrentTransactions = currentTransactions;
            }
          }
          str = writer1.ToString();
        }
      }
      return str;
    }

    /// <summary>Gets the script references for a page built in memory</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public PageDataScripts GetScripts(
      PageData pageData,
      PageDataProvider provider,
      SiteMapProvider siteMapProvider)
    {
      string fullUrl = pageData.NavigationNode.GetFullUrl();
      PageDataScripts scripts = new PageDataScripts()
      {
        PageData = pageData
      };
      using (StringWriter writer1 = new StringWriter())
      {
        using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
        {
          HttpContext context = (HttpContext) null;
          Page usingWorkerRequest = this.GetPageUsingWorkerRequest(pageData, fullUrl, provider, writer2, out context);
          ScriptManager.GetCurrent(usingWorkerRequest).ResolveScriptReference += new EventHandler<ScriptReferenceEventArgs>(this.ResolveScriptReference);
          context.Handler = (IHttpHandler) usingWorkerRequest;
          usingWorkerRequest.ProcessRequest(context);
        }
      }
      return scripts;
    }

    private RequestContext CreatePageNodeRequestContext(
      PageNode pageNode,
      HtmlTextWriter writer,
      out HttpContext context)
    {
      string rawUrl = pageNode.GetUrl();
      if (rawUrl.IsNullOrEmpty())
        rawUrl = VirtualPathUtility.AppendTrailingSlash(HostingEnvironment.ApplicationVirtualPath);
      context = RouteHelper.CreateHttpContext(rawUrl, (TextWriter) writer);
      if (HttpContext.Current != null)
      {
        context.User = HttpContext.Current.User;
        foreach (object key in (IEnumerable) HttpContext.Current.Items.Keys)
        {
          if (!context.Items.Contains(key))
            context.Items[key] = HttpContext.Current.Items[key];
        }
      }
      context.Items[(object) "RadControlRandomNumber"] = (object) 0;
      SiteMapProvider providerForPageNode = SiteMapBase.GetSiteMapProviderForPageNode(pageNode);
      if (providerForPageNode != null)
      {
        SiteMapNode siteMapNodeFromKey = providerForPageNode.FindSiteMapNodeFromKey(pageNode.Id.ToString("D"));
        if (siteMapNodeFromKey != null)
          context.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = (object) siteMapNodeFromKey;
      }
      return new RequestContext((HttpContextBase) new HttpContextWrapper(context), new RouteData());
    }

    private Page GetPage(
      PageNode pageNode,
      PageDataProvider provider,
      HtmlTextWriter writer,
      Guid? segmentId,
      out HttpContext context)
    {
      RequestContext nodeRequestContext = this.CreatePageNodeRequestContext(pageNode, writer, out context);
      HttpContext current = HttpContext.Current;
      Page page = (Page) null;
      try
      {
        HttpContext.Current = context;
        PageData pageData = pageNode.GetPageData();
        if (segmentId.HasValue && segmentId.Value != Guid.Empty)
        {
          pageData = provider.GetPageDataList().FirstOrDefault<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id && (Guid?) p.PersonalizationSegmentId == segmentId));
          if (pageData == null)
            throw new ArgumentException(string.Format("No page variation found for segment {0}", (object) segmentId));
        }
        StaticPageData staticPageData = new StaticPageData(pageData, provider);
        if (!string.IsNullOrEmpty(staticPageData.MasterPage))
        {
          page = this.CreateHandler(nodeRequestContext, (ITemplate) null);
          page.MasterPageFile = staticPageData.MasterPage;
        }
        else
        {
          ITemplate pageTemplate = staticPageData.GetPageTemplate();
          if (pageTemplate == null)
            pageTemplate = ControlUtilities.GetTemplate(new TemplateInfo()
            {
              TemplateName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx",
              ControlType = typeof (Telerik.Sitefinity.Web.PageRouteHandler)
            });
          page = this.CreateHandler(nodeRequestContext, pageTemplate);
        }
        page.Items[(object) "staticPageData"] = (object) staticPageData;
      }
      finally
      {
        HttpContext.Current = current;
      }
      return page;
    }

    private Page GetPageDraft(
      PageNode pageNode,
      PageDataProvider provider,
      HtmlTextWriter writer,
      Guid? segmentId,
      out HttpContext context)
    {
      RequestContext nodeRequestContext = this.CreatePageNodeRequestContext(pageNode, writer, out context);
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      PageDraft pageData = provider.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (d => d.ParentId == pageNode.PageId && d.Owner == currentUserId && d.IsTempDraft == true)).SingleOrDefault<PageDraft>();
      if (pageData == null)
        return this.GetPage(pageNode, provider, writer, segmentId, out context);
      Telerik.Sitefinity.Modules.Pages.PageDraftProxy pageDraftProxy = new Telerik.Sitefinity.Modules.Pages.PageDraftProxy(pageData, pageNode, provider, true, CultureInfo.InvariantCulture);
      HttpContext current = HttpContext.Current;
      Page pageDraft = (Page) null;
      try
      {
        HttpContext.Current = context;
        if (!string.IsNullOrEmpty(pageDraftProxy.MasterPage))
        {
          pageDraft = this.CreateHandler(nodeRequestContext, (ITemplate) null);
          pageDraft.MasterPageFile = pageDraftProxy.MasterPage;
        }
        else
        {
          ITemplate pageTemplate = pageDraftProxy.GetPageTemplate();
          if (pageTemplate == null)
            pageTemplate = ControlUtilities.GetTemplate(new TemplateInfo()
            {
              TemplateName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx",
              ControlType = typeof (Telerik.Sitefinity.Web.PageRouteHandler)
            });
          pageDraft = this.CreateHandler(nodeRequestContext, pageTemplate);
        }
        pageDraft.Items[(object) "isPreview"] = new object();
        pageDraft.Items[(object) "staticPageData"] = (object) pageDraftProxy;
      }
      finally
      {
        HttpContext.Current = current;
      }
      return pageDraft;
    }

    private Page GetPageUsingWorkerRequest(
      PageData pageData,
      string url,
      PageDataProvider provider,
      HtmlTextWriter writer,
      out HttpContext context)
    {
      context = new HttpContext((HttpWorkerRequest) new FakeWorkerRequest(url, "", (TextWriter) writer));
      RequestContext requestContext = new RequestContext((HttpContextBase) new HttpContextWrapper(context), new RouteData());
      StaticPageData staticPageData = new StaticPageData(pageData, provider);
      Page handler = this.CreateHandler(requestContext, staticPageData.GetPageTemplate());
      handler.Items[(object) "staticPageData"] = (object) staticPageData;
      return handler;
    }

    private void LoadPage(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      PlaceHoldersCollection handlerPlaceHolders = PageHelper.CreateHandlerPlaceHolders(page);
      page.SetPlaceHolders(handlerPlaceHolders);
      if (page.Items[(object) "isPreview"] == null)
      {
        StaticPageData staticPageData = page.Items[(object) "staticPageData"] as StaticPageData;
        staticPageData.ApplyLayouts(page);
        staticPageData.CreateChildControls(page, false);
      }
      else
      {
        Telerik.Sitefinity.Modules.Pages.PageDraftProxy pageDraftProxy = page.Items[(object) "staticPageData"] as Telerik.Sitefinity.Modules.Pages.PageDraftProxy;
        pageDraftProxy.ApplyLayouts(page);
        pageDraftProxy.CreateChildControls(page);
      }
    }

    private void ResolveScriptReference(object sender, ScriptReferenceEventArgs e)
    {
      try
      {
        ((sender as ScriptManager).Page.Items[(object) "scriptsMap"] as PageDataScripts).Scripts.Add(e.Script);
      }
      catch
      {
      }
    }

    private Page CreateHandler(RequestContext requestContext, ITemplate pageTemplate)
    {
      Page handler;
      if (pageTemplate is VirtualPathTemplate virtualPathTemplate && virtualPathTemplate.IsPage)
      {
        handler = (Page) virtualPathTemplate.GetInstance();
        PlaceHoldersCollection placeHolders = handler.GetPlaceHolders();
        if (handler is ISitefinityPage sitefinityPage)
        {
          sitefinityPage.RequestContext = requestContext;
          sitefinityPage.PlaceHolders = placeHolders;
        }
        else
        {
          IDictionary items = requestContext.HttpContext.Items;
          items[(object) "sf_RequestContext"] = (object) requestContext;
          items[(object) "sf_PlaceHolders"] = (object) placeHolders;
        }
      }
      else
      {
        ISitefinityPage sitefinityPage = CompilationHelpers.LoadControl<ISitefinityPage>("~/SFRes/Telerik.Sitefinity.Resources.SitefinityDefault.aspx");
        handler = (Page) sitefinityPage;
        sitefinityPage.RequestContext = requestContext;
        string str = ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrl(requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath);
        handler.AppRelativeVirtualPath = str;
        if (pageTemplate != null)
        {
          if (pageTemplate is IContentPlaceHolderContainer placeHolderContainer)
            sitefinityPage.PlaceHolders = placeHolderContainer.InstantiateIn((Control) handler);
          else
            pageTemplate.InstantiateIn((Control) handler);
        }
        handler.Init += new EventHandler(this.page_Init);
        handler.Load += new EventHandler(this.LoadPage);
        handler.PreRenderComplete += new EventHandler(this.page_PreRenderComplete);
      }
      return handler;
    }

    private void page_Init(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      if (page.Form != null)
      {
        ScriptManager current1 = ScriptManager.GetCurrent(page);
        if (current1 == null)
        {
          ControlLiteralRepresentation.FakeManager child = new ControlLiteralRepresentation.FakeManager();
          page.Form.Controls.Add((Control) child);
        }
        else if (current1 is RadScriptManager radScriptManager)
          radScriptManager.EnableHandlerDetection = false;
        RadStyleSheetManager current2 = RadStyleSheetManager.GetCurrent(page);
        if (current2 == null)
          page.Form.Controls.Add((Control) new InMemoryPageRender.FakeRadStyleSheetManager());
        else
          current2.EnableHandlerDetection = false;
      }
      else
      {
        ControlLiteralRepresentation.FakeManager child = new ControlLiteralRepresentation.FakeManager();
        page.Controls.Add((Control) child);
        page.Controls.Add((Control) new InMemoryPageRender.FakeRadStyleSheetManager());
      }
    }

    private void page_PreRenderComplete(object sender, EventArgs e)
    {
      ResourceLinks.RegisterDynamicWebResourceContainer(sender as Control);
      RadStyleSheetManager.GetCurrent((Page) sender)?.StyleSheets.Clear();
    }

    private void DisposeTransactions() => SystemManager.CurrentTransactions?.Dispose();

    internal class FakeRadStyleSheetManager : RadStyleSheetManager
    {
      protected override void OnInit(EventArgs e)
      {
      }

      protected override void OnLoad(EventArgs e)
      {
      }

      protected override void OnPreRender(EventArgs e)
      {
      }
    }
  }
}
