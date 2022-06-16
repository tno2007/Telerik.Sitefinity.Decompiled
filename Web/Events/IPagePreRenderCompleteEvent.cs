// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.IPagePreRenderCompleteEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An interface of the event notifying that a page's prerender event has completed.
  /// The event is raised at 'PreRenderComplete' event of ASP.NET page lifecycle.
  /// A subscription for this event can be created by using the <see cref="!:EventHub" />.
  /// Once the event is being handled, the user can use it to modify a sitefinity dynamic page.
  /// </summary>
  public interface IPagePreRenderCompleteEvent : IEvent
  {
    /// <summary>Gets the site node of the page.</summary>
    /// <value>The page.</value>
    PageSiteNode PageSiteNode { get; }

    /// <summary>Gets the intialized ASP.NET page.</summary>
    /// <value>The page.</value>
    Page Page { get; }
  }
}
