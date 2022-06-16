// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Operation.SendResponseHeadersEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services.Operation
{
  /// <summary>Raised on sending response headers.</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Services.IContextOperationEvent" />
  internal class SendResponseHeadersEvent : IContextOperationEvent, IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Operation.SendResponseHeadersEvent" /> class.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="sender">The sender.</param>
    public SendResponseHeadersEvent(string origin, object sender)
    {
      this.OperationKey = "PreSendResponseHeaders";
      this.Origin = origin;
      this.Sender = sender;
    }

    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    public string OperationKey { get; }

    /// <summary>Gets or sets the origin of the event.</summary>
    public string Origin { get; set; }

    /// <summary>Gets or sets the sender.</summary>
    /// <value>The sender.</value>
    public object Sender { get; set; }
  }
}
