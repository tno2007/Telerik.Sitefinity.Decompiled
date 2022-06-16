// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RequestStartEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A context operation bound to a specific starting request.
  /// </summary>
  internal class RequestStartEvent : IContextOperationStartEvent, IContextOperationEvent, IEvent
  {
    private readonly string operationKey;
    internal const string DefaultCategoryName = "HttpRequest";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestStartEvent" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public RequestStartEvent(HttpContextBase context)
    {
      this.operationKey = context != null ? RequestStartEvent.GetOperationKey(context) : throw new ArgumentNullException(nameof (context));
      this.ResponseType = context.Response.ContentType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestStartEvent" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public RequestStartEvent(HttpContext context)
      : this((HttpContextBase) new HttpContextWrapper(context))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.RequestStartEvent" /> class.
    /// </summary>
    /// <param name="operation">The key of the operation.</param>
    public RequestStartEvent(string operation) => this.operationKey = operation;

    internal static string GetOperationKey(HttpContextBase context)
    {
      string operationKey = (string) context.Items[(object) "sf_request_event_operation_Key"];
      if (operationKey == null)
      {
        Uri url = context.Request.Url;
        operationKey = !(url != (Uri) null) ? context.Request.RawUrl : url.AbsoluteUri;
        context.Items[(object) "sf_request_event_operation_Key"] = (object) operationKey;
      }
      return operationKey;
    }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    public string OperationKey => this.operationKey;

    /// <summary>
    /// Gets the category of the started operation
    /// Could be an unique string specific for the operation of this type, e.g. HttpRequest, Background, etc..
    /// </summary>
    /// <value>The category.</value>
    public string Category => "HttpRequest";

    /// <summary>Gets or sets the type of the response.</summary>
    /// <value>The type of the response.</value>
    public string ResponseType { get; set; }
  }
}
