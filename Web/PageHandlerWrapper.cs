// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageHandlerWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Concurrent;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// This class facilitates the cached pages processing blocking. The cache blocking essentially block all requests for a page, until there is an output cache
  /// available. This avoids unnecessary processing of page requests - that can lead to high processor usage and database usage.
  /// The blocking is done only for pages with output cache turned on!
  /// </summary>
  internal class PageHandlerWrapper : IHttpHandler, IRequiresSessionState, IHandlerWrapper
  {
    private IHttpHandler baseHandler;
    internal static ConcurrentDictionary<string, object> PageRequestsToBeCached = new ConcurrentDictionary<string, object>();

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" />
    /// instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable;
    /// otherwise, false.</returns>
    /// <value></value>
    public bool IsReusable => this.baseHandler.IsReusable;

    /// <summary>
    /// Gets the http handler which actually handles the request.
    /// </summary>
    public IHttpHandler OriginalHandler => this.baseHandler;

    public PageHandlerWrapper(IHttpHandler original) => this.baseHandler = original;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that
    /// implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides
    /// references to the intrinsic server objects (for example, Request, Response, Session,
    /// and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context)
    {
      if (this.PageIsToBeCached(context))
      {
        string originalString = context.Request.Url.OriginalString;
        object obj = new object();
        Monitor.Enter(obj);
        try
        {
          PageHandlerWrapper.PageRequestsToBeCached.TryAdd(originalString, obj);
          this.baseHandler.ProcessRequest(context);
        }
        finally
        {
          PageHandlerWrapper.PageRequestsToBeCached.TryRemove(originalString, out object _);
          Monitor.Exit(obj);
        }
      }
      else
        this.baseHandler.ProcessRequest(context);
    }

    private bool PageIsToBeCached(HttpContext context)
    {
      object obj = context.Items[(object) PageRouteHandler.AddCacheDependencies];
      return obj != null && (bool) obj;
    }
  }
}
