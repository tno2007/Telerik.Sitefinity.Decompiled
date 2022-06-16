// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.BackgroundOperationEndEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A context operation bound to a specific finishing background task.
  /// </summary>
  internal class BackgroundOperationEndEvent : 
    IContextOperationEndEvent,
    IContextOperationEvent,
    IEvent
  {
    private readonly string operationKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.BackgroundOperationEndEvent" /> class.
    /// </summary>
    /// <param name="operation">The key of the operation.</param>
    public BackgroundOperationEndEvent(string operation) => this.operationKey = operation;

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    public string OperationKey => this.operationKey;

    /// <summary>
    /// Gets or sets the status of the executed background operation.
    /// </summary>
    /// <value>The status.</value>
    public string Status { get; set; }
  }
}
