// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SitefinityRedirectUriValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Redirect Uri validator</summary>
  public class SitefinityRedirectUriValidator : IRedirectUriValidator
  {
    /// <summary>
    /// Determines whether the requested URI is safe for redirection (valid uri, relative or licensed or registered in multisite, or it is to the same host as the request or it is on localhost)
    /// </summary>
    /// <param name="requestedUri">The requested URI.</param>
    /// <returns>Return true or false whether the uri is valid or not</returns>
    [Obsolete("Use ObjectFactory.Resolve<IRedirectUriValidator>().IsValid()")]
    public static bool IsUriValid(string requestedUri)
    {
      if (string.IsNullOrWhiteSpace(requestedUri))
        return false;
      if (!RouteHelper.IsAbsoluteUrl(requestedUri))
        return true;
      Uri uri = new Uri(requestedUri);
      foreach (string allLicensedDomain in LicenseState.Current.LicenseInfo.AllLicensedDomains)
      {
        if (uri.Authority.Equals(allLicensedDomain, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      return multisiteContext != null && multisiteContext.GetSites().Any<ISite>((Func<ISite, bool>) (site => site.LiveUrl == uri.Authority || site.StagingUrl == uri.Authority || site.DomainAliases.Any<string>((Func<string, bool>) (alias => alias == uri.Authority)))) || SystemManager.CurrentHttpContext.Request.Url.Authority.Equals(uri.Authority) || SitefinityRedirectUriValidator.IsLocalhostUri(uri);
    }

    /// <summary>
    /// Returns <c>true</c> if the given requestUri is trusted for redirection. If domain is: <para>relative</para><para>or is licensed</para><para>or is from multisite</para><para>or is localhost</para><para>or is the same as request</para><para>Otherwise false.</para>
    /// </summary>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <returns>
    ///   <c>true</c> if the specified request URI is safe for redirection; otherwise, <c>false</c>.
    /// </returns>
    public bool IsValid(string redirectUri) => SitefinityRedirectUriValidator.IsUriValid(redirectUri);

    /// <summary>
    /// Determines whether an URI links to localhost.
    /// Ignores port.
    /// </summary>
    /// <param name="requestedUri">The requested URI.</param>
    /// <returns><c>true</c> if the URI is links to localhost, <c>false</c> otherwise.</returns>
    private static bool IsLocalhostUri(Uri requestedUri) => requestedUri.Host.Equals("localhost");
  }
}
