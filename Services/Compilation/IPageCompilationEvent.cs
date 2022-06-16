// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Compilation.IPageCompilationEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services.Compilation
{
  /// <summary>
  /// This interface describes the contract for an event that contains information about the compilation of a <see cref="T:System.Web.UI.Page" />.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Services.Events.IEvent" />
  internal interface IPageCompilationEvent : IEvent
  {
    /// <summary>Gets the compilation identifier.</summary>
    /// <value>The compilation identifier.</value>
    Guid CompilationId { get; }

    /// <summary>Gets the page node identifier.</summary>
    /// <value>The node identifier.</value>
    Guid NodeId { get; }

    /// <summary>Gets the identifier of the page data.</summary>
    /// <value>The identifier of the page data.</value>
    Guid PageId { get; }

    /// <summary>Gets the virtual path of the page.</summary>
    /// <value>The virtual path of the item.</value>
    string VirtualPath { get; }

    /// <summary>Gets the title of the page.</summary>
    /// <value>The title of the page.</value>
    string Title { get; }

    /// <summary>Gets the URL of the page.</summary>
    /// <value>The URL of the page.</value>
    string Url { get; }

    /// <summary>
    /// Gets the identifier of the root of the page site node.
    /// </summary>
    /// <value>The identifier of the root of the page site node.</value>
    Guid RootNodeId { get; }

    /// <summary>Gets the timestamp when the event occurred.</summary>
    /// <value>The timestamp.</value>
    long Timestamp { get; }
  }
}
