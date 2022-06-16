// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClientOA
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Http;
using Microsoft.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
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
  public class SitefinityRestHttpClientOA : HttpClient, ISitefinityRestHttpClient
  {
    public const string SimpleWebTokenServiceUrl = "/Sitefinity/Authenticate/SWT";
    public const string ClaimsLogOutServiceUrl = "/Sitefinity/SignOut";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClient" /> class.
    /// </summary>
    public SitefinityRestHttpClientOA()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.HttpClients.SitefinityRestHttpClient" /> class.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    public SitefinityRestHttpClientOA(string baseUrl)
      : this()
    {
      this.BaseUrl = baseUrl;
    }

    /// <summary>Gets the specified request URI.</summary>
    /// <param name="requestUri">The request URI.</param>
    /// <returns></returns>
    public HttpResponseMessage Get(Uri requestUri) => HttpMethodExtensions.Get(this, requestUri);

    /// <summary>Gets the specified request URI by string</summary>
    /// <param name="requestUri">The request string.</param>
    /// <returns></returns>
    public HttpResponseMessage Get(string requestUri) => HttpMethodExtensions.Get(this, requestUri);

    /// <summary>Gets or sets the base URL.</summary>
    /// <value>The base URL.</value>
    public string BaseUrl
    {
      get => this.BaseAddress.ToString();
      set => this.BaseAddress = new Uri(value);
    }

    /// <summary>Logouts the current user.</summary>
    /// <returns></returns>
    public bool Logout() => throw new NotSupportedException();

    /// <summary>
    /// Logouts the specified user name.
    /// Note that this method sends the default headers, which should conain the security token set in AuthenticateToServerInternal
    /// </summary>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="rememberMe">The remember me.</param>
    /// <returns></returns>
    public bool Logout(string membershipProvider, string userName, string password = null)
    {
      Uri logoutServiceUrl = this.GetLogoutServiceUrl();
      HttpQueryString httpQueryString = new HttpQueryString();
      httpQueryString.Add(new KeyValuePair<string, string>("username", userName));
      httpQueryString.Add(new KeyValuePair<string, string>("provider", membershipProvider));
      HttpQueryString queryString = httpQueryString;
      HttpMethodExtensions.Get(this, logoutServiceUrl, queryString);
      this.DefaultHeaders["Authorization"] = (string) null;
      return true;
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

    private UserLoggingReason AuthenticateToServerInternal(
      string membershipProvider,
      string userName,
      string password,
      bool rememberMe,
      bool logoutOtherUsersIfNeeded)
    {
      Uri authenticationServiceUrl = this.GetAuthenticationServiceUrl();
      HttpContent content = HttpContent.Create(Encoding.UTF8.GetBytes(string.Format("wrap_name={0}&wrap_password={1}", (object) userName, (object) password)));
      RequestHeaders headers = new RequestHeaders();
      headers.Add("Content-Type", "application/x-www-form-urlencoded");
      HttpResponseMessage httpResponseMessage = this.Send(HttpMethod.POST, authenticationServiceUrl, headers, content);
      if (this.TransportSettings.Cookies == null)
      {
        this.TransportSettings.Cookies = new CookieContainer();
        this.TransportSettings.Cookies.SetCookies(this.BaseAddress, httpResponseMessage.Headers.SetCookie.ToString());
      }
      string query = httpResponseMessage.StatusCode == HttpStatusCode.OK ? httpResponseMessage.Content.ReadAsString() : throw new Exception(string.Format("The authentication reqeust failed with status code: {0}", (object) httpResponseMessage.StatusCode));
      string str = !string.IsNullOrEmpty(query) ? System.Web.HttpUtility.ParseQueryString(query)["wrap_access_token"] : throw new Exception("The authentication service didn't return any security token.");
      if (string.IsNullOrEmpty(str))
        throw new Exception("The authentication service did not returned the expected token format 'wrap_access_token=(url encoded token)'");
      this.DefaultHeaders["Authorization"] = "WRAP access_token=\"" + str + "\"";
      if (!logoutOtherUsersIfNeeded)
        ;
      return UserLoggingReason.Success;
    }

    private Uri GetAuthenticationServiceUrl() => this.GetServiceUrl("/Sitefinity/Authenticate/SWT");

    private Uri GetLogoutServiceUrl() => this.GetServiceUrl("/Sitefinity/SignOut");

    private Uri GetServiceUrl(string servicePath) => new Uri(VirtualPathUtility.RemoveTrailingSlash(this.BaseUrl) + servicePath);

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
