// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.PagePreRenderCompleteEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <inheritdoc />
  internal class PagePreRenderCompleteEvent : IPagePreRenderCompleteEvent, IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.PagePreRenderCompleteEvent" /> class.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="pageSiteNode">The page site.</param>
    /// <param name="origin">The origin.</param>
    internal PagePreRenderCompleteEvent(Page page, PageSiteNode pageSiteNode, string origin)
    {
      this.Page = page;
      this.PageSiteNode = pageSiteNode;
      this.Origin = origin;
    }

    /// <inheritdoc />
    public Page Page { get; internal set; }

    /// <inheritdoc />
    public PageSiteNode PageSiteNode { get; internal set; }

    /// <inheritdoc />
    public string Origin { get; set; }
  }
}
