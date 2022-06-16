// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.UnauthorizedPageAccessEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An event notifying that the current user was denied to access the requested front-end page.
  /// </summary>
  internal class UnauthorizedPageAccessEvent : IUnauthorizedPageAccessEvent, IEvent
  {
    /// <inheritdoc />
    public string Origin { get; set; }

    /// <inheritdoc />
    public HttpContextBase HttpContext { get; internal set; }

    /// <inheritdoc />
    public PageSiteNode Page { get; internal set; }

    /// <inheritdoc />
    public RedirectStrategyType RedirectStrategy { get; internal set; }

    /// <inheritdoc />
    public string RedirectUrl { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.UnauthorizedPageAccessEvent" /> class.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="page">The page.</param>
    /// <param name="redirectStrategy">The redirect strategy.</param>
    /// <param name="origin">The origin.</param>
    internal UnauthorizedPageAccessEvent(
      HttpContextBase httpContext,
      PageSiteNode page,
      RedirectStrategyType redirectStrategy,
      string origin,
      string redirectUrl)
    {
      this.HttpContext = httpContext;
      this.Page = page;
      this.RedirectStrategy = redirectStrategy;
      this.Origin = origin;
      this.RedirectUrl = redirectUrl;
    }
  }
}
