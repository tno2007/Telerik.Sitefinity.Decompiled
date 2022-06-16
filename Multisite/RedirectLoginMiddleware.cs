// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.RedirectLoginMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Events;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>a
  /// When the HTTP status code is Unauthorized (401) redirects to the front end login (if such is set).
  /// </summary>
  internal class RedirectLoginMiddleware : OwinMiddleware
  {
    private const string AccessToken = "access_token";

    public RedirectLoginMiddleware(OwinMiddleware next)
      : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
      RedirectLoginMiddleware redirectLoginMiddleware = this;
      await redirectLoginMiddleware.Next.Invoke(context);
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      PageSiteNode node = currentHttpContext.Items[(object) "targetNode"] as PageSiteNode;
      if (context.Response.StatusCode == 401 && context.Authentication.AuthenticationResponseChallenge == null)
      {
        RedirectStrategyType redirectStrategy = RedirectStrategyType.None;
        PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
        string str = string.Empty;
        if (actualCurrentNode != null)
        {
          SiteMapProvider provider = actualCurrentNode.Provider;
        }
        if (redirectLoginMiddleware.ShouldRedirectToFrontendLoginPage())
          str = RouteHelper.GetFrontEndLogin(currentHttpContext, out redirectStrategy, SitefinitySiteMap.GetCurrentProvider());
        redirectLoginMiddleware.RaiseUnauthorizedPageAccessEvent(currentHttpContext, node, redirectStrategy, str);
        if (string.IsNullOrWhiteSpace(str))
          return;
        string pathAndQuery = currentHttpContext.Request.Url.PathAndQuery;
        if (pathAndQuery.Contains(SecurityManager.AuthenticationReturnUrl) || pathAndQuery.Contains("access_token"))
          return;
        currentHttpContext.Response.Redirect(str, false);
      }
      else
      {
        if (context.Response.StatusCode != 403 || currentHttpContext.Items[(object) "sf_custom_error_page"] != null || context.Authentication.AuthenticationResponseChallenge != null)
          return;
        redirectLoginMiddleware.RaiseForbiddenPageAccessEvent(currentHttpContext, node);
      }
    }

    private bool ShouldRedirectToFrontendLoginPage()
    {
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if ((currentSite.FrontEndLoginPageId != Guid.Empty ? 1 : (!string.IsNullOrEmpty(currentSite.FrontEndLoginPageUrl) ? 1 : 0)) == 0)
        return false;
      return !SystemManager.IsBackendRequest() || Config.Get<SecurityConfig>().AuthenticateOnFrontendLoginPage;
    }

    private void RaiseUnauthorizedPageAccessEvent(
      HttpContextBase httpContext,
      PageSiteNode node,
      RedirectStrategyType redirectStrategy,
      string redirectUrl)
    {
      string name = this.GetType().Name;
      EventHub.Raise((IEvent) new UnauthorizedPageAccessEvent(httpContext, node, redirectStrategy, name, redirectUrl));
    }

    private void RaiseForbiddenPageAccessEvent(HttpContextBase httpContext, PageSiteNode node)
    {
      string name = this.GetType().Name;
      EventHub.Raise((IEvent) new ForbiddenPageAccessEvent(httpContext, node, name));
    }
  }
}
