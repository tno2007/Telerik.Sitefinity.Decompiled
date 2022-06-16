// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.Services.ProtectionShieldCookieServiceStackPlugin
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
  internal class ProtectionShieldCookieServiceStackPlugin : IPlugin
  {
    internal const string ProtectionShieldCookieWebServiceRoute = "/shield";
    internal const string CookiesRoute = "/shield/set-cookie";

    /// <summary>Adds the Protection shield service routes</summary>
    /// <param name="appHost">The service stack appHost</param>
    public void Register(IAppHost appHost)
    {
      if (appHost == null)
        throw new ArgumentNullException(nameof (appHost));
      appHost.RegisterService(typeof (ProtectionShieldCookieWebService), "/shield");
      appHost.Routes.Add<CookieMessage>("/shield/set-cookie", "GET");
    }
  }
}
