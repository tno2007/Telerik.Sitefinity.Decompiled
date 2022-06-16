// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.RequestTokenAuthenticationFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Adding this filter to your ServiceStack service class will request token when calling the service
  /// </summary>
  public abstract class RequestTokenAuthenticationFilterAttribute : 
    RequestAdministrationAuthenticationFilterAttribute
  {
    private const string HttpsOnly = "HTTPS Only";
    private const string HttpsOnlyStatusCodeMessage = "403";

    /// <inheritdoc />
    public override void Execute(IRequest req, IResponse res, object requestDto)
    {
      if (req == null)
        throw new HttpError(HttpStatusCode.NotFound);
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      int num = this.RequireHttps() ? 1 : 0;
      bool flag = UrlPath.IsSecuredConnection(currentHttpContext);
      if (num != 0 && !flag)
        throw new HttpError(HttpStatusCode.Forbidden, "403", "HTTPS Only");
      string authenticationKey = this.GetAuthenticationKey();
      if (string.IsNullOrWhiteSpace(authenticationKey))
      {
        base.Execute(req, res, requestDto);
      }
      else
      {
        string header = req.Headers[this.GetAuthHeaderName()];
        if (string.IsNullOrEmpty(header) || !string.Equals(header, authenticationKey))
          throw new HttpError(HttpStatusCode.NotFound);
        SecurityManager.AuthenticateSystemRequest(currentHttpContext);
      }
    }

    /// <summary>
    /// Gets the name of the HTTP header which carries the token.
    /// </summary>
    /// <returns>The HTTP header name.</returns>
    protected abstract string GetAuthHeaderName();

    /// <summary>Gets the expected token.</summary>
    /// <returns>The expected token.</returns>
    protected abstract string GetAuthenticationKey();

    /// <summary>
    /// Checks whether the service should be executed only under HTTPS.
    /// </summary>
    /// <returns>A value, indicating whether the service requires HTTPS.</returns>
    protected abstract bool RequireHttps();
  }
}
