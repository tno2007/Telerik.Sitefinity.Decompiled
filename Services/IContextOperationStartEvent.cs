// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IContextOperationStartEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// An event containing information about starting operation in the current context.
  /// </summary>
  public interface IContextOperationStartEvent : IContextOperationEvent, IEvent
  {
    /// <summary>
    /// Gets the category of the started operation
    /// Could be an unique string specific for the operation of this type, e.g. HttpRequest, Background, etc..
    /// </summary>
    /// <value>The category.</value>
    string Category { get; }
  }
}
