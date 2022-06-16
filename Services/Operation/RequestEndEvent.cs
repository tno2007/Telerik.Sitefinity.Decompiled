// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RequestEndEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A context operation bound to a specific finishing request.
  /// </summary>
  public class RequestEndEvent : IContextOperationEndEvent, IContextOperationEvent, IEvent
  {
    private readonly string operationKey;
    private readonly string status;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestEndEvent" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public RequestEndEvent(HttpContextBase context)
    {
      this.operationKey = context != null ? RequestStartEvent.GetOperationKey(context) : throw new ArgumentNullException(nameof (context));
      this.status = context.Response.StatusCode.ToString();
      this.ResponseType = context.Response.ContentType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestEndEvent" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public RequestEndEvent(HttpContext context)
      : this((HttpContextBase) new HttpContextWrapper(context))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestEndEvent" /> class.
    /// </summary>
    /// <param name="operation">The key of the operation.</param>
    public RequestEndEvent(string operation) => this.operationKey = operation;

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    public string OperationKey => this.operationKey;

    /// <summary>Gets the status of the operation.</summary>
    /// <value>The status.</value>
    public string Status => this.status;

    /// <summary>Gets or sets the type of the response.</summary>
    /// <value>The type of the response.</value>
    public string ResponseType { get; set; }
  }
}
