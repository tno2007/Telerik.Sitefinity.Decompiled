// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Http;
using Microsoft.Http.Headers;
using System;
using System.Runtime.CompilerServices;
using System.Web;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Services.HttpClients
{
  /// <summary>
  /// An HTTP client class (see Microsoft WCF REST Starter Kit) that contains functionality for authenticating to a Sitefinty
  /// instance and store the received authentication cookie information for all later requests done through it.
  /// </summary>
  /// <example>
  /// // meta code sample for authenticating to a sitefinity instance and then publishing a news item.
  /// var sitefinityClient = new SitefinityRestHttpClient("http://localhost/");
  /// sitefinityClient.ForceAuthenticateToServer(null, "administrator", "passwordoftheadministrator");
  /// sitefinityClient.Put(newsServiceUrl, HttpContent.Create(Encoding.UTF8.GetBytes(newItemAsJson)));
  /// </example>
  /// <seealso cref="!:http://msdn.microsoft.com/en-us/library/ee391967.aspx" />
  public class SitefinityRestHttpClient : HttpClient, ISitefinityRestHttpClient
  {
    private const string LogoutCurrentUserMethodName = "Logout";
    private const string LogoutUserWithCredentialsMethod = "LogoutCredentials";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClient" /> class.
    /// </summary>
    public SitefinityRestHttpClient() => this.DefaultHeaders.ContentType = "application/json";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClient" /> class.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    public SitefinityRestHttpClient(string baseUrl)
      : this()
    {
      this.BaseUrl = baseUrl;
    }

    /// <summary>Gets or sets the base URL.</summary>
    /// <value>The base URL.</value>
    public string BaseUrl
    {
      get => this.BaseAddress.ToString();
      set => this.BaseAddress = new Uri(value);
    }

    /// <summary>Logouts the current user.</summary>
    /// <returns></returns>
    public bool Logout()
    {
      HttpResponseMessage response = this.Get("/Sitefinity/Services/Security/Users.svc/Logout");
      response.EnsureStatusIsSuccessful();
      return response.Content.ReadAsString().ToLower() == "true";
    }

    /// <summary>Logouts the specified user.</summary>
    /// <remarks>
    /// This method is sending credentials over the wire which is potential security risk. You should use SSL connection.
    /// </remarks>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="rememberMe">The remember me.</param>
    /// <returns></returns>
    public bool Logout(string membershipProvider, string userName, string password)
    {
      HttpResponseMessage response = this.Post(new Uri(VirtualPathUtility.RemoveTrailingSlash(this.BaseUrl) + "/Sitefinity/Services/Security/Users.svc/LogoutCredentials"), SitefinityRestHttpClientHelper.GetCredentials(membershipProvider, userName, password, false));
      response.EnsureStatusIsSuccessful();
      return response.Content.ReadAsString().ToLower() == "true";
    }

    /// <summary>
    /// Authenticates to the sitefinity server logging out other users that are logged with those credentials if needed.
    /// </summary>
    /// <remarks>
    /// This method is sending credentials over the wire which is potential security risk. You should use SSL connection.
    /// </remarks>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="rememberMe">The remember me.</param>
    /// <returns></returns>
    public UserLoggingReason ForceAuthenticateToServer(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe = false)
    {
      return this.AuthenticateToServerInternal(membershipProvider, userName, password, rememberMe, true);
    }

    /// <summary>Attempts to Authenticates to the sitefinity server.</summary>
    /// <remarks>
    /// This method is sending credentials over the wire which is potential security risk. You should use SSL connection.
    /// </remarks>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="rememberMe">The remember me.</param>
    public UserLoggingReason AuthenticateToServer(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe = false)
    {
      return this.AuthenticateToServerInternal(membershipProvider, userName, password, rememberMe, false);
    }

    /// <summary>Gets the specified request URI.</summary>
    /// <param name="requestUri">The request URI.</param>
    /// <returns>The response.</returns>
    public HttpResponseMessage Get(Uri requestUri) => HttpMethodExtensions.Get(this, requestUri);

    /// <summary>Gets the specified request URI by string</summary>
    /// <param name="requestUri">The request string.</param>
    /// <returns></returns>
    public HttpResponseMessage Get(string requestUri) => HttpMethodExtensions.Get(this, requestUri);

    private UserLoggingReason AuthenticateToServerInternal(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe,
      bool logoutOtherUsersIfNeeded)
    {
      HttpContent credentials = SitefinityRestHttpClientHelper.GetCredentials(membershipProvider, userName, password, rememberMe);
      string str1 = VirtualPathUtility.RemoveTrailingSlash(this.BaseUrl);
      HttpResponseMessage response1 = this.Post(new Uri(str1 + "/Sitefinity/Services/Security/Users.svc/Authenticate"), credentials);
      response1.EnsureStatusIsSuccessful();
      UserLoggingReason result;
      if (!Enum.TryParse<UserLoggingReason>(response1.Content.ReadAsString(), out result))
        throw new Exception("The user service returned a result that is not a valid value of UserLoggingReason enum.");
      switch (result)
      {
        case UserLoggingReason.Success:
          this.SetDefaultCookiesFrom(response1);
          return result;
        case UserLoggingReason.UserLoggedFromDifferentIp:
        case UserLoggingReason.UserLoggedFromDifferentComputer:
        case UserLoggingReason.UserAlreadyLoggedIn:
          if (logoutOtherUsersIfNeeded)
          {
            Uri uri = new Uri(str1 + "/Sitefinity/Services/Security/Users.svc/Authenticate");
            if (this.Logout(membershipProvider, userName, password))
            {
              HttpResponseMessage response2 = this.Post(uri, SitefinityRestHttpClientHelper.GetCredentials(membershipProvider, userName, password, rememberMe));
              string str2 = response2.EnsureStatusIsSuccessful().Content.ReadAsString();
              if (!(str2 == "0"))
                return (UserLoggingReason) Enum.Parse(typeof (UserLoggingReason), str2);
              this.SetDefaultCookiesFrom(response2);
              return UserLoggingReason.Success;
            }
          }
          return result;
        default:
          return result;
      }
    }

    private void SetDefaultCookiesFrom(HttpResponseMessage response) => this.DefaultHeaders.Cookie = response.Headers.SetCookie.CleanExpired().StackRepeatedCookiesByName();

    [SpecialName]
    RequestHeaders ISitefinityRestHttpClient.get_DefaultHeaders() => this.DefaultHeaders;

    [SpecialName]
    void ISitefinityRestHttpClient.set_DefaultHeaders(RequestHeaders value) => this.DefaultHeaders = value;

    [SpecialName]
    Uri ISitefinityRestHttpClient.get_BaseAddress() => this.BaseAddress;

    [SpecialName]
    void ISitefinityRestHttpClient.set_BaseAddress(Uri value) => this.BaseAddress = value;

    [SpecialName]
    HttpWebRequestTransportSettings ISitefinityRestHttpClient.get_TransportSettings() => this.TransportSettings;
  }
}
