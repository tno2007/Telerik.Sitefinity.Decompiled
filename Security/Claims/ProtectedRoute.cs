// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.ProtectedRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Checks for authentication to protect the backend pages of Sitefinity
  /// </summary>
  public class ProtectedRoute : Route
  {
    private static bool cleanSetupCookie;
    private static readonly string publicServicesPath = "Sitefinity" + "/Public/";
    private static readonly string frontEndServicesPath = "Sitefinity" + "/Frontend/";
    private static readonly string appStatusPath = AppStatusService.GetAppStatusPageRelativeUrl().Trim('/');

    public ProtectedRoute(string url)
      : base(url, (IRouteHandler) new ProtectedRoute.EmptyRouteHandler())
    {
    }

    public ProtectedRoute(string url, IRouteHandler routeHandler)
      : base(url, routeHandler)
    {
    }

    public ProtectedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
      : base(url, defaults, routeHandler)
    {
    }

    public ProtectedRoute(
      string url,
      RouteValueDictionary defaults,
      RouteValueDictionary constraints,
      IRouteHandler routeHandler)
      : base(url, defaults, constraints, routeHandler)
    {
    }

    public ProtectedRoute(
      string url,
      RouteValueDictionary defaults,
      RouteValueDictionary constraints,
      RouteValueDictionary dataTokens,
      IRouteHandler routeHandler)
      : base(url, defaults, constraints, dataTokens, routeHandler)
    {
    }

    /// <summary>
    /// Handles the case when the user is not allowed to view some resource.
    /// If the user is not authenticated he is redirected to the login page.
    /// If the user is authenticated - the HttpException with HttpStatusCode.Forbidden is thrown.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="message"></param>
    internal static void HandleItemViewNotAllowed(HttpContextBase context, string message)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.IsAuthenticated)
        throw new HttpException(403, message);
      ClaimsManager.CurrentAuthenticationModule.SendUnauthorizedResponse(context);
    }

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      RouteData routeData = base.GetRouteData(httpContext);
      if (routeData != null)
      {
        HttpContextBase context = httpContext.ApplicationInstance != null ? httpContext : SystemManager.CurrentHttpContext;
        string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
        if (!string.IsNullOrEmpty(StartupWizard.InitialLoginUserName))
        {
          if (!virtualPath.StartsWith(ProtectedRoute.appStatusPath, StringComparison.OrdinalIgnoreCase))
          {
            try
            {
              HttpCookie cookie = httpContext.Request.Cookies["sf_initial_login_key"];
              if (cookie != null)
              {
                if (cookie.Value == StartupWizard.InitialLoginValidationKey)
                  this.StartupWizardInitialUserLogin(httpContext, StartupWizard.InitialLoginUserName);
              }
            }
            catch
            {
            }
            finally
            {
              if (ProtectedRoute.cleanSetupCookie)
              {
                StartupWizard.InitialLoginUserName = string.Empty;
                StartupWizard.InitialLoginValidationKey = string.Empty;
              }
              else
                ProtectedRoute.cleanSetupCookie = true;
            }
            return (RouteData) null;
          }
        }
        if (!ProtectedRoute.IsBackendUnprotected(virtualPath))
        {
          BackendRestriction.Current.EndRequestIfForbidden(httpContext);
          SFClaimsAuthenticationManager.ProcessRejectedUser(context);
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity == null || !currentIdentity.IsAuthenticated)
          {
            ClaimsManager.CurrentAuthenticationModule.SendUnauthorizedResponse(context);
            routeData = new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
          }
          else if (!currentIdentity.IsBackendUser)
          {
            ClaimsManager.CurrentAuthenticationModule.RedirectToNeedAdminRightsPage(httpContext);
            routeData = new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
          }
          else if (SystemManager.CurrentContext.MultisiteContext != null && !SystemManager.CurrentContext.MultisiteContext.GetAllowedSites(currentIdentity.UserId, currentIdentity.MembershipProvider).Contains<Guid>(SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id))
          {
            ClaimsManager.CurrentAuthenticationModule.RedirectToSiteNotAccessiblePage(httpContext);
            routeData = new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
          }
        }
        else
          context.Items[(object) "SuppressAuthenticationRequirement"] = (object) true;
        if (routeData.RouteHandler is ProtectedRoute.EmptyRouteHandler)
          routeData = (RouteData) null;
      }
      return routeData;
    }

    public static bool IsBackendUnprotected(string virtualPath) => virtualPath.StartsWith(ProtectedRoute.publicServicesPath, StringComparison.OrdinalIgnoreCase) || virtualPath.StartsWith(ProtectedRoute.frontEndServicesPath, StringComparison.OrdinalIgnoreCase) || virtualPath.StartsWith(ProtectedRoute.appStatusPath, StringComparison.OrdinalIgnoreCase);

    private void StartupWizardInitialUserLogin(HttpContextBase httpContext, string username)
    {
      if (string.IsNullOrEmpty(username))
        return;
      AuthenticationProperties properties = ChallengeProperties.ForInitialUser(username);
      properties.RedirectUri = httpContext.Request.Url.AbsoluteUri;
      httpContext.GetOwinContext().Authentication.Challenge(properties, ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);
      int num = (int) SecurityManager.AuthenticateUser((string) null, username, false, out User _);
    }

    private class EmptyRouteHandler : IRouteHandler
    {
      public IHttpHandler GetHttpHandler(RequestContext requestContext) => throw new NotSupportedException();
    }
  }
}
