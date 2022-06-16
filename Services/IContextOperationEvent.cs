﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IContextOperationEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// An event containing information about operation in the current context.
  /// </summary>
  public interface IContextOperationEvent : IEvent
  {
    /// <summary>Gets the operation key.</summary>
    /// <value>The operation key.</value>
    string OperationKey { get; }
  }
}
