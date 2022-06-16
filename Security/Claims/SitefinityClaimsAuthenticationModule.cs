// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SitefinityClaimsAuthenticationModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Sitefinity HttpModule that handles the authentication in the application.
  /// <para>It handles the authentication requests by processing the token or the valid credentials (basic authentication).</para>
  /// </summary>
  public class SitefinityClaimsAuthenticationModule : ISitefinityClaimsAuthenticationModule
  {
    private bool? stsSignout;
    private string realm;
    private string issuer;

    /// <summary>Gets the Authentication protocol</summary>
    public string AuthenticationProtocol { get; internal set; }

    /// <summary>
    /// Gets a value indication the authentication type of the relying party
    /// </summary>
    public string RPAuthenticationType { get; internal set; }

    /// <summary>
    /// Gets a value indication the authentication type of the security token service
    /// </summary>
    public string STSAuthenticationType { get; internal set; }

    /// <summary>Gets the external authentication providers</summary>
    public IEnumerable<IExternalAuthenticationProvider> ExternalAuthenticationProviders { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating what service to be used.
    /// </summary>
    public string Service { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the query string used for sign out.
    /// </summary>
    public string SignOutQueryString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the query string used for sign out.
    /// </summary>
    public string Reply { get; set; }

    /// <summary>Gets or sets a value indicating the request.</summary>
    public string Request { get; set; }

    /// <summary>Gets or sets a value indicating the request pointer.</summary>
    public string RequestPtr { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether https is required.
    /// </summary>
    public bool RequireHttps { get; set; }

    /// <summary>Gets or sets a value indicating the resource used.</summary>
    public string Resource { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the context used for signing in.
    /// </summary>
    public string SignInContext { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the query string used for sign out.
    /// </summary>
    public string SignInQueryString { get; set; }

    /// <summary>
    /// Gets a value indicating whether this request is using basic authentication.
    /// </summary>
    /// <value>
    /// <c>true</c> if this request uses basic authentication; otherwise, <c>false</c>.
    /// </value>
    internal static bool IsBasicAuthentication
    {
      get
      {
        string header = SystemManager.CurrentHttpContext.Request.Headers["Authorization"];
        return !string.IsNullOrEmpty(header) && header.StartsWith("Basic");
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.SitefinityClaimsAuthenticationModule" /> class
    /// </summary>
    public SitefinityClaimsAuthenticationModule()
      : this((string) null, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.SitefinityClaimsAuthenticationModule" /> class
    /// </summary>
    /// <param name="issuer">The issuer.</param>
    /// <param name="realm">The realm.</param>
    public SitefinityClaimsAuthenticationModule(string issuer, string realm)
    {
      this.ExternalAuthenticationProviders = Enumerable.Empty<IExternalAuthenticationProvider>();
      this.issuer = issuer.IsNullOrEmpty() ? "http://localhost" : issuer;
      this.realm = realm.IsNullOrEmpty() ? "http://localhost" : realm;
    }

    /// <summary>Gets the realm.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default realm for context or the one that is configured
    /// </returns>
    public string GetRealm(HttpContext context = null) => context != null ? this.GetRealm((HttpContextBase) new HttpContextWrapper(context)) : this.GetRealm(SystemManager.CurrentHttpContext);

    /// <summary>Gets the realm.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default realm for context or the one that is configured
    /// </returns>
    public string GetRealm(HttpContextBase context)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      string realm;
      if (context != null && this.realm == "http://localhost")
      {
        string str = context.Request.Url.GetLeftPart(UriPartial.Authority);
        if (!context.Request.IsSecureConnection && UrlPath.IsSecuredConnection(context))
          str = str.Replace("http://", "https://");
        realm = VirtualPathUtility.AppendTrailingSlash(new Uri(str + context.Request.ApplicationPath).AbsoluteUri);
      }
      else
        realm = this.realm;
      return realm;
    }

    /// <summary>Gets the issuer.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default issuer for context or the one that is configured
    /// </returns>
    public string GetIssuer(HttpContext context = null) => context != null ? this.GetIssuer((HttpContextBase) new HttpContextWrapper(context)) : this.GetIssuer(SystemManager.CurrentHttpContext);

    /// <summary>Gets the issuer.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default issuer for context or the one that is configured
    /// </returns>
    public string GetIssuer(HttpContextBase context)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      return context == null || !(this.issuer == "http://localhost") ? this.issuer : this.GetLocalService(context);
    }

    /// <summary>Gets the local service.</summary>
    /// <param name="context">The context.</param>
    /// <returns>Local service for current context</returns>
    public string GetLocalService(HttpContextBase context = null)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      string applicationPath = context.Request.ApplicationPath;
      string str = this.Service.StartsWith("/") ? this.Service : "/" + this.Service;
      string relativeUri = (applicationPath.Length > 1 ? applicationPath : string.Empty) + str;
      string localService = new Uri(new Uri(this.GetRealm(context)), relativeUri).AbsoluteUri;
      if (this.RequireHttps)
        localService = localService.Replace("http://", "https://");
      return localService;
    }

    void ISitefinityClaimsAuthenticationModule.RedirectToNeedAdminRightsPage(
      HttpContextBase context,
      ClaimsPrincipal principal = null)
    {
      SFClaimsAuthenticationManager.SetRejectionInformation(context, ClaimsManager.GetCurrentPrincipal(), UserLoggingReason.SiteAccessNotAllowed);
      context.GetOwinContext().Authentication.Challenge(this.RPAuthenticationType);
      context.ApplicationInstance.CompleteRequest();
    }

    void ISitefinityClaimsAuthenticationModule.RedirectToSiteNotAccessiblePage(
      HttpContextBase context,
      ClaimsPrincipal principal = null)
    {
      SFClaimsAuthenticationManager.SetRejectionInformation(context, ClaimsManager.GetCurrentPrincipal(), UserLoggingReason.SiteNotAccessible);
      context.GetOwinContext().Authentication.Challenge(this.RPAuthenticationType);
      context.ApplicationInstance.CompleteRequest();
    }

    void ISitefinityClaimsAuthenticationModule.SendUnauthorizedResponse(
      HttpContextBase context)
    {
      ClaimsManager.SendUnauthorizedResponse(context);
    }

    string ISitefinityClaimsAuthenticationModule.GetLogoutUrl(
      string redirectUri)
    {
      string localService = this.GetLocalService((HttpContextBase) null);
      return this.StsSignout || this.GetIssuer((HttpContext) null).Equals(localService, StringComparison.OrdinalIgnoreCase) ? string.Format("~/Sitefinity/SignOut?sts_signout=true&redirect_uri={0}", (object) HttpUtility.UrlEncode(redirectUri)) : string.Format("~/Sitefinity/SignOut?redirect_uri={0}", (object) HttpUtility.UrlEncode(redirectUri));
    }

    /// <summary>Gets the authentication types used for sign out.</summary>
    /// <returns>List of the used types</returns>
    public List<string> GetSignOutAuthenticationTypes()
    {
      List<string> authenticationTypes = new List<string>();
      authenticationTypes.Add(this.RPAuthenticationType);
      if (this.StsSignout || this.GetIssuer((HttpContext) null).Equals(this.GetLocalService((HttpContextBase) null), StringComparison.OrdinalIgnoreCase))
        authenticationTypes.Add(this.STSAuthenticationType);
      return authenticationTypes;
    }

    private bool StsSignout
    {
      get
      {
        if (!this.stsSignout.HasValue)
          this.stsSignout = new bool?(Config.Get<SecurityConfig>().StsSignout);
        return this.stsSignout.Value;
      }
    }
  }
}
