// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Compilation.PageCompilationStartEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services.Compilation
{
  /// <summary>
  /// This class describes a compilation start event for a <see cref="T:System.Web.UI.Page" />.
  /// </summary>
  /// <seealso cref="!:Telerik.Sitefinity.Services.IItemCompilationStartEvent" />
  internal class PageCompilationStartEvent : 
    IPageCompilationStartEvent,
    IPageCompilationEvent,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Compilation.PageCompilationStartEvent" /> class.
    /// </summary>
    /// <param name="compilationId">The compilation identifier. It is used to match start and end compilation events.</param>
    /// <param name="pageSiteNode">The site node of the page.</param>
    /// <param name="rootNodeId">The identifier of the root of the page site node.</param>
    /// <param name="virtualPath">The virtual path of the page.</param>
    public PageCompilationStartEvent(
      Guid compilationId,
      PageSiteNode pageSiteNode,
      Guid rootNodeId,
      string virtualPath)
    {
      this.Start = DateTime.UtcNow;
      this.CompilationId = compilationId;
      this.NodeId = pageSiteNode.Id;
      this.PageId = pageSiteNode.PageId;
      this.Title = pageSiteNode.Title;
      this.Url = pageSiteNode.Url;
      this.RootNodeId = rootNodeId;
      this.VirtualPath = virtualPath;
      this.Timestamp = Stopwatch.GetTimestamp();
      this.Origin = SystemManager.CurrentContext.CurrentSite != null ? SystemManager.CurrentContext.CurrentSite.Id.ToString() : (string) null;
    }

    /// <summary>Gets the compilation identifier.</summary>
    /// <value>The compilation identifier.</value>
    public Guid CompilationId { get; private set; }

    /// <summary>Gets the compilation start time in UTC.</summary>
    /// <value>The compilation start time in UTC.</value>
    public DateTime Start { get; private set; }

    /// <summary>Gets the page node identifier.</summary>
    /// <value>The node identifier.</value>
    public Guid NodeId { get; private set; }

    /// <summary>Gets the identifier of the page.</summary>
    /// <value>The identifier of the page.</value>
    public Guid PageId { get; private set; }

    /// <summary>Gets the virtual path of the page.</summary>
    /// <value>The virtual path of the item.</value>
    public string VirtualPath { get; private set; }

    /// <summary>Gets the title of the page.</summary>
    /// <value>The title of the page.</value>
    public string Title { get; private set; }

    /// <summary>Gets the URL of the page.</summary>
    /// <value>The URL of the page.</value>
    public string Url { get; private set; }

    /// <summary>
    /// Gets the identifier of the root of the page site node.
    /// </summary>
    /// <value>The identifier of the root of the page site node.</value>
    public Guid RootNodeId { get; private set; }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets the timestamp when the event occurred.</summary>
    /// <value>The timestamp.</value>
    public long Timestamp { get; private set; }
  }
}
