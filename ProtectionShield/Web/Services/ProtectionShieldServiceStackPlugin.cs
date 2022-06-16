// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.Services.ProtectionShieldServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using Telerik.Sitefinity.ProtectionShield.Web.Services.Dto;

namespace Telerik.Sitefinity.ProtectionShield.Web.Services
{
  /// <summary>
  /// Represents a ServiceStack plug-in for Protection shield service.
  /// </summary>
  internal class ProtectionShieldServiceStackPlugin : IPlugin
  {
    internal const string ProtectionShieldWebServiceRoute = "/Sitefinity/shield";
    internal const string AccessTokensRoute = "/Sitefinity/shield/access-tokens";
    internal const string BlockAccessTokensRoute = "/Sitefinity/shield/access-tokens/block";
    internal const string UnblockAccessTokensRoute = "/Sitefinity/shield/access-tokens/unblock";
    internal const string RemoveAccessTokensRoute = "/Sitefinity/shield/access-tokens/remove";
    internal const string ProtectionShieldRoute = "/Sitefinity/shield/protection";
    internal const string EmailInvitationRoute = "/Sitefinity/shield/invite";
    internal const string ResendEmailInvitationRoute = "/Sitefinity/shield/invite/resend";

    /// <summary>Adds the Protection shield service routes</summary>
    /// <param name="appHost">The service stack appHost</param>
    public void Register(IAppHost appHost)
    {
      if (appHost == null)
        throw new ArgumentNullException(nameof (appHost));
      appHost.RegisterService(typeof (ProtectionShieldWebService), "/Sitefinity/shield");
      appHost.Routes.Add<AccessTokensMessage>("/Sitefinity/shield/access-tokens", "GET").Add<BlockAccessTokenMessage>("/Sitefinity/shield/access-tokens/block", "POST").Add<UnblockAccessTokenMessage>("/Sitefinity/shield/access-tokens/unblock", "POST").Add<RemoveAccessTokenMessage>("/Sitefinity/shield/access-tokens/remove", "POST").Add<ProtectionShieldMessage>("/Sitefinity/shield/protection", "POST").Add<EmailInvitationMessage>("/Sitefinity/shield/invite", "POST").Add<ResendEmailInvitationMessage>("/Sitefinity/shield/invite/resend", "POST");
    }
  }
}
