// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.NewslettersRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>
  ///  Represents a route handler for Sitefinity's forms designer.
  /// </summary>
  public class NewslettersRouteHandler : RouteHandler
  {
    /// <summary>
    /// Gets or sets a value indicating whether the requested page is for preview or edit mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    public bool IsPreview { get; set; }

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
      StandardMessageDraftProxy pageData = (StandardMessageDraftProxy) items[(object) "NewslettersDescriptionProxy"];
      if (pageData == null)
      {
        NewslettersManager newslettersManager = node is NewslettersSiteNode formNode ? NewslettersManager.GetManager(formNode.NewslettersProviderName) : throw new ArgumentException("Invalid node provided.");
        PageManager pageManager = NewslettersManager.GetPageManager();
        Guid messageBodyId = new Guid(formNode.Key);
        MessageBody messageBody = newslettersManager.GetMessageBody(messageBodyId);
        if (messageBody == null)
          throw new HttpException(404, "There is no campaign with the specified id \"{0}\"".Arrange((object) formNode.Key));
        Guid lockedBy;
        int num = this.IsLocked(formNode, out lockedBy) ? 1 : 0;
        bool suppressSecurityChecks = pageManager.Provider.SuppressSecurityChecks;
        pageManager.Provider.SuppressSecurityChecks = true;
        PageDraft standardCampaignDraft;
        if (num != 0)
          standardCampaignDraft = pageManager.CreateDraft<PageDraft>(messageBody.Id);
        else if (this.IsPreview)
        {
          standardCampaignDraft = pageManager.GetPreview(messageBody.Id);
        }
        else
        {
          standardCampaignDraft = pageManager.EditPage(messageBody.Id, true, CultureInfo.InvariantCulture);
          pageManager.SaveChanges();
        }
        pageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        pageData = new StandardMessageDraftProxy(standardCampaignDraft, messageBody, NewslettersManager.GetManager(formNode.NewslettersProviderName).Provider, this.IsPreview);
        pageData.LockedBy = lockedBy;
        items[(object) "NewslettersDescriptionProxy"] = (object) pageData;
      }
      return (IPageData) pageData;
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
      DraftProxyBase pageData = (DraftProxyBase) this.GetPageData((SiteMapNode) requestContext.RouteData.DataTokens["SiteMapNode"]);
      pageData.ApplyLayouts(page);
      pageData.CreateChildControls(page);
    }

    private bool IsLocked(NewslettersSiteNode formNode, out Guid lockedBy)
    {
      lockedBy = Guid.Empty;
      return false;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public override IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      SystemManager.SetPageDesignMode(true);
      return base.GetHttpHandler(requestContext);
    }
  }
}
