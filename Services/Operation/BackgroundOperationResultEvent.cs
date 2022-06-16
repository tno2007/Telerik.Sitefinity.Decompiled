// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.BackgroundOperationResultEvent`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A result operation bound to a specific finishing background task.
  /// </summary>
  /// <typeparam name="T">The type of the event result item.</typeparam>
  internal class BackgroundOperationResultEvent<T> : 
    BackgroundOperationEndEvent,
    IResultOperationEndEvent<T>,
    IContextOperationEndEvent,
    IContextOperationEvent,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.BackgroundOperationResultEvent`1" /> class.
    /// </summary>
    /// <param name="operation">The key of the operation.</param>
    public BackgroundOperationResultEvent(string operation)
      : base(operation)
    {
    }

    /// <summary>
    /// Gets or sets a value specifying the result of the event.
    /// </summary>
    /// <value>The result.</value>
    public T Result { get; set; }
  }
}
