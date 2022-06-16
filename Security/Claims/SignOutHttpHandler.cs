// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SignOutHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Sign out handler that process the requests for logging off users
  /// </summary>
  public class SignOutHttpHandler : IHttpHandler
  {
    internal const string LogoutUsernameQueryStringKey = "username";
    internal const string LogoutProviderQueryStringKey = "provider";
    internal const string RedirectQueryStringKey = "redirect";
    internal const string SignOutHttpHandlerBaseUrl = "Sitefinity/SignOut";
    /// <summary>Displays the self logout page.</summary>
    internal const string SelfLogoutMethod = "selflogout";
    /// <summary>
    /// Displays the user limit reached page with an option to logout other users (if the user has rights).
    /// </summary>
    internal const string ForceLogoutMethod = "forcelogout";
    /// <summary>Displays the need admin rights page.</summary>
    internal const string NeedAdminRightsMethod = "needadminrights";
    /// <summary>Displays the need admin rights page.</summary>
    internal const string SiteNotAccessibleMethod = "sitenotaccessible";
    /// <summary>There was a login exception</summary>
    internal const string LoginFailed = "loginFailed";

    public bool IsReusable => true;

    /// <summary>
    /// Checks query string for username and provider to perform logout of another user
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    /// <exception cref="T:System.ArgumentException"></exception>
    public void ProcessRequest(HttpContextBase context)
    {
      string str = context.Items[(object) "method"] as string;
      if (!string.IsNullOrWhiteSpace(str))
      {
        if ("selflogout" == str)
          PageManager.RenderPage(context, SiteInitializer.UserAlreadyLoggedInPageId, (Action<Page>) (p => { }));
        else if ("forcelogout" == str)
          PageManager.RenderPage(context, SiteInitializer.UserLimitReachedPageId, (Action<Page>) (p => { }));
        else if ("needadminrights" == str)
          PageManager.RenderPage(context, SiteInitializer.NeedAdminRightsPageId, (Action<Page>) (p => { }));
        else if ("sitenotaccessible" == str)
        {
          PageManager.RenderPage(context, SiteInitializer.SiteNotAccessiblePageId, (Action<Page>) (p => { }));
        }
        else
        {
          if (!("loginFailed" == str))
            throw new ArgumentException(string.Format("Unsupported sign out method: {0}", (object) str));
          PageManager.RenderPage(context, SiteInitializer.LoginFailedPageId, (Action<Page>) (p => { }));
        }
      }
      else
      {
        string queryStringParam1 = SignOutHttpHandler.GetQueryStringParam(context, "username");
        string queryStringParam2 = SignOutHttpHandler.GetQueryStringParam(context, "provider");
        if (!string.IsNullOrWhiteSpace(queryStringParam1))
        {
          SecurityManager.Logout(queryStringParam2, queryStringParam1);
        }
        else
        {
          AuthenticationProperties properties = new AuthenticationProperties();
          if (SignOutHttpHandler.ShouldRedirect(context) && !string.IsNullOrWhiteSpace(context.Request.QueryString["redirect_uri"]))
            properties.RedirectUri = context.Request.QueryString["redirect_uri"];
          if (string.IsNullOrEmpty(properties.RedirectUri))
            properties.RedirectUri = UrlPath.ResolveUrl("~/", true);
          List<string> stringList = new List<string>()
          {
            ClaimsManager.CurrentAuthenticationModule.RPAuthenticationType
          };
          if ("true".Equals(context.Request.QueryString["sts_signout"], StringComparison.OrdinalIgnoreCase))
            stringList.Add(ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);
          context.GetOwinContext().Authentication.SignOut(properties, stringList.ToArray());
          context.ApplicationInstance.CompleteRequest();
        }
      }
    }

    private static bool ShouldRedirect(HttpContextBase context)
    {
      bool result;
      if (!bool.TryParse(SignOutHttpHandler.GetQueryStringParam(context, "redirect"), out result))
        result = true;
      return result;
    }

    private static string GetQueryStringParam(HttpContextBase context, string parameterName)
    {
      string queryStringParam = (string) null;
      NameValueCollection queryString = context.Request.QueryString;
      if (queryString.Keys.Contains(parameterName) && !string.IsNullOrWhiteSpace(queryString[parameterName]))
        queryStringParam = queryString[parameterName];
      return queryStringParam;
    }
  }
}
