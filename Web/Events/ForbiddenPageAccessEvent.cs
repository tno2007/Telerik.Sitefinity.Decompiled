// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.ForbiddenPageAccessEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An event notifying that the current authenticated user was denied to access the requested front-end page.
  /// </summary>
  public class ForbiddenPageAccessEvent : IForbiddenPageAccessEvent, IEvent
  {
    /// <inheritdoc />
    public string Origin { get; set; }

    /// <inheritdoc />
    public HttpContextBase HttpContext { get; internal set; }

    /// <inheritdoc />
    public PageSiteNode Page { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.ForbiddenPageAccessEvent" /> class.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="page">The page.</param>
    /// <param name="origin">The origin.</param>
    internal ForbiddenPageAccessEvent(
      HttpContextBase httpContext,
      PageSiteNode page,
      string origin)
    {
      this.HttpContext = httpContext;
      this.Page = page;
      this.Origin = origin;
    }
  }
}
