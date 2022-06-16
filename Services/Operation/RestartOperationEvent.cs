// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Operation.RestartOperationEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services.Operation
{
  /// <summary>Event that is raised when Sitefinity is restarted.</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Services.Events.IEvent" />
  internal class RestartOperationEvent : IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Operation.RestartOperationEvent" /> class.
    /// </summary>
    public RestartOperationEvent()
    {
      if (SystemManager.CurrentContext == null || SystemManager.CurrentContext.CurrentSite == null)
        return;
      this.SiteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    /// <summary>Gets or sets the kind of the operation.</summary>
    /// <value>The kind of the operation.</value>
    public SystemManager.RestartOperationKind OperationKind { get; internal set; }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets or sets the parameters of the restart operation.</summary>
    /// <value>The parameters.</value>
    public NameValueCollection Parameters { get; internal set; }

    /// <summary>Gets or sets the reason.</summary>
    /// <value>The reason.</value>
    public OperationReason Reason { get; internal set; }

    /// <summary>Gets or sets the stack trace.</summary>
    /// <value>The stack trace.</value>
    public string StackTrace { get; internal set; }

    /// <summary>Gets or sets the user that invoked the restart.</summary>
    /// <value>The user.</value>
    public string User { get; set; }

    /// <summary>
    /// Gets or sets the flags that indicate the type of the restart.
    /// </summary>
    /// <value>The flags.</value>
    public SystemRestartFlags Flags { get; internal set; }

    /// <summary>Gets or sets the site identifier.</summary>
    /// <value>The site identifier.</value>
    public Guid SiteId { get; internal set; }
  }
}
