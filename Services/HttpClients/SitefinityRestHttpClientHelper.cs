// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClientHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Http;
using System.Text;

namespace Telerik.Sitefinity.Services.HttpClients
{
  /// <summary>
  /// Contains helper methods for constructing Sitefintiy service calls
  /// </summary>
  public static class SitefinityRestHttpClientHelper
  {
    private const string credentialsFormat = "\r\n        {{\r\n            \"MembershipProvider\":\"{0}\",\r\n            \"UserName\":\"{1}\",\r\n            \"Password\":\"{2}\",\r\n            \"Persistent\":{3}\r\n        }}";
    public const string AuthenticateMethodName = "Authenticate";
    public const string UsersServiceUrl = "/Sitefinity/Services/Security/Users.svc/";

    /// <summary>
    /// Gets the credentials prepared as a json content for service call.
    /// </summary>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
    /// <returns>The credentials as json content</returns>
    public static HttpContent GetCredentials(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe)
    {
      return HttpContent.Create(Encoding.UTF8.GetBytes(string.Format("\r\n        {{\r\n            \"MembershipProvider\":\"{0}\",\r\n            \"UserName\":\"{1}\",\r\n            \"Password\":\"{2}\",\r\n            \"Persistent\":{3}\r\n        }}", (object) membershipProvider, (object) userName, (object) password, (object) rememberMe.ToString().ToLower())), "application/json");
    }
  }
}
