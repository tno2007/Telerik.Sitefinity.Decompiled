// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RedirectRoutingHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Redirects the request to the specified URL.</summary>
  public class RedirectRoutingHandler : IRouteHandler
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.RedirectRoutingHandler" />.
    /// </summary>
    protected RedirectRoutingHandler()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.RedirectRoutingHandler" /> with the provided URL.
    /// </summary>
    /// <param name="redirectUrl">The URL to redirect to.</param>
    public RedirectRoutingHandler(string redirectUrl)
      : this(redirectUrl, (string) null, true)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.RedirectRoutingHandler" /> with the provided URL.
    /// </summary>
    /// <param name="redirectUrl">The URL to redirect to.</param>
    /// <param name="handlerName">The name of this handler.</param>
    public RedirectRoutingHandler(string redirectUrl, string handlerName, bool permenentRedirect)
    {
      this.RedirectUrl = !string.IsNullOrEmpty(redirectUrl) ? redirectUrl : throw new ArgumentNullException(nameof (redirectUrl));
      this.Name = handlerName;
      this.PermanentRedirect = permenentRedirect;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <returns>An object that processes the request.</returns>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      HttpResponseBase response = requestContext.HttpContext.Response;
      response.Status = !this.PermanentRedirect ? "302 found" : "301 Moved Permanently";
      response.AddHeader("Location", UrlPath.ResolveUrl(this.RedirectUrl));
      response.End();
      return (IHttpHandler) null;
    }

    /// <summary>Gets the root path for this handler.</summary>
    public virtual string Root => string.Empty;

    /// <summary>Gets the name of this handler.</summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    public string ResourceClassId { get; set; }

    /// <summary>Gets (or protected sets) the URL to redirect to.</summary>
    public virtual string RedirectUrl { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether a permenent redirect will be done.
    /// </summary>
    /// <value><c>true</c> if [permanent redirect]; otherwise, <c>false</c>.</value>
    public virtual bool PermanentRedirect { get; private set; }
  }
}
