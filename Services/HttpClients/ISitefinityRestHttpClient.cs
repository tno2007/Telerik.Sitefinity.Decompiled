// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.HttpClients.ISitefinityRestHttpClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Http;
using Microsoft.Http.Headers;
using System;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Services.HttpClients
{
  public interface ISitefinityRestHttpClient
  {
    UserLoggingReason AuthenticateToServer(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe = false);

    string BaseUrl { get; set; }

    UserLoggingReason ForceAuthenticateToServer(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe = false);

    bool Logout();

    bool Logout(string membershipProvider, string userName, string password);

    RequestHeaders DefaultHeaders { get; set; }

    Uri BaseAddress { get; set; }

    HttpResponseMessage Get(Uri requestUri);

    HttpResponseMessage Get(string requestUri);

    HttpWebRequestTransportSettings TransportSettings { get; }
  }
}
