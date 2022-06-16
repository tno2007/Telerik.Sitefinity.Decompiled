// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.ResponsiveDesignTransformationHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Data;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web
{
  /// <summary>
  /// Http handler which generates the CSS for layout transformations when responsive design module is used.
  /// </summary>
  public class ResponsiveDesignTransformationHttpHandler : IHttpHandler
  {
    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <returns>
    /// true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that
    /// implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">
    /// An <see cref="T:System.Web.HttpContext" /> object that provides
    /// references to the intrinsic server objects (for example, Request, Response, Session,
    /// and Server) used to service HTTP requests.
    /// </param>
    public void ProcessRequest(HttpContext context)
    {
      context.Response.Clear();
      context.Response.Cache.SetCacheability(HttpCacheability.Public);
      context.Response.Cache.SetExpires(DateTime.Now.AddHours(1.0));
      context.Response.ContentType = "text/css";
      CultureInfo culture = (CultureInfo) null;
      if (context.Request.QueryString["culture"] != null)
        culture = CultureInfo.GetCultureInfo(context.Request.QueryString["culture"]);
      string s = (string) null;
      CssTransformationsCache instance = CssTransformationsCache.GetInstance();
      using (new CultureRegion(culture))
      {
        if (context.Request.QueryString["pageSiteNode"] != null)
          s = instance.GetCss(new PageSiteNodeResolver(context.Request.QueryString["pageSiteNode"]).Resolve().CurrentPageDataItem);
        else if (context.Request.QueryString["pageDataId"] != null)
        {
          Guid pageDataId = Guid.Parse(context.Request.QueryString["pageDataId"]);
          s = instance.GetCss(pageDataId, (Func<IEnumerable<Guid>>) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            PageNode page = PageManager.GetManager().GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Id == this.pageDataId)).Select<PageData, PageNode>((Expression<Func<PageData, PageNode>>) (p => p.NavigationNode)).SingleOrDefault<PageNode>();
            if (page == null)
              return Enumerable.Empty<Guid>();
            PageSiteNode siteMapNode = page.GetSiteMapNode();
            return siteMapNode == null ? Enumerable.Empty<Guid>() : (IEnumerable<Guid>) siteMapNode.CurrentPageDataItem.TemplatesIds;
          }));
        }
        else if (context.Request.QueryString["pageId"] != null)
        {
          Guid pageNodeId = Guid.Parse(context.Request.QueryString["pageId"]);
          PageNode page = PageManager.GetManager().GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == pageNodeId));
          if (page != null)
          {
            PageDataProxy currentPageDataItem = page.GetSiteMapNode().CurrentPageDataItem;
            s = instance.GetCss(currentPageDataItem);
          }
          else
            s = instance.GetCss();
        }
        else
          s = instance.GetCss();
      }
      context.Response.Write(s);
    }
  }
}
