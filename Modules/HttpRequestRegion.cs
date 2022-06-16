// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.HttpRequestRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Telerik.Sitefinity.Modules
{
  internal sealed class HttpRequestRegion : IDisposable
  {
    private HttpContext oldContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.HttpRequestRegion" /> class.
    /// </summary>
    /// <param name="requestUrl">The request URL.</param>
    public HttpRequestRegion(string requestUrl) => this.ConstructorInternal(requestUrl, (IPrincipal) null, (IDictionary) null);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.HttpRequestRegion" /> class.
    /// </summary>
    /// <param name="requestUrl">The request URL.</param>
    /// <param name="user">The user of the HTTP Context.</param>
    /// <param name="items">The items of the HTTP context.</param>
    public HttpRequestRegion(string requestUrl, IPrincipal user, IDictionary items) => this.ConstructorInternal(requestUrl, user, items);

    private void ConstructorInternal(string requestUrl, IPrincipal user, IDictionary items)
    {
      this.oldContext = HttpContext.Current;
      HttpResponse response = new HttpResponse((TextWriter) new StringWriter(new StringBuilder()));
      HttpContext httpContext = new HttpContext(new HttpRequest("", requestUrl, ""), response);
      if (user != null)
        httpContext.User = user;
      else if (this.oldContext != null)
        httpContext.User = this.oldContext.User;
      if (items == null && this.oldContext != null)
        items = this.oldContext.Items;
      if (items != null)
      {
        foreach (object key in (IEnumerable) items.Keys)
          httpContext.Items[key] = items[key];
      }
      HttpContext.Current = httpContext;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => HttpContext.Current = this.oldContext;
  }
}
