// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.ISitefinityClaimsAuthenticationModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

namespace Telerik.Sitefinity.Security.Claims
{
  public interface ISitefinityClaimsAuthenticationModule
  {
    string AuthenticationProtocol { get; }

    string RPAuthenticationType { get; }

    string STSAuthenticationType { get; }

    IEnumerable<IExternalAuthenticationProvider> ExternalAuthenticationProviders { get; }

    /// <summary>Gets the realm.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default realm for context or the one that is configured
    /// </returns>
    string GetRealm(HttpContextBase context = null);

    /// <summary>Gets the issuer.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// Default issuer for context or the one that is configured
    /// </returns>
    string GetIssuer(HttpContextBase context = null);

    /// <summary>Gets the local service.</summary>
    /// <param name="context">The context.</param>
    /// <returns>Local service for current context</returns>
    string GetLocalService(HttpContextBase context = null);

    void RedirectToNeedAdminRightsPage(HttpContextBase context, ClaimsPrincipal principal = null);

    void RedirectToSiteNotAccessiblePage(HttpContextBase context, ClaimsPrincipal principal = null);

    void SendUnauthorizedResponse(HttpContextBase context);

    string GetLogoutUrl(string redirectUri);

    List<string> GetSignOutAuthenticationTypes();
  }
}
