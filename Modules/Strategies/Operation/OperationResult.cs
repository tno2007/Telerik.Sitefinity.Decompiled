// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.OperationResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  internal class OperationResult
  {
    internal OperationResult() => this.Success = true;

    [DataMember]
    public bool Success { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string[] AvailableActions { get; set; }

    [DataMember]
    public ItemOperation UndoOperation { get; set; }

    [DataMember]
    public string Message { get; set; }

    [DataMember]
    public string MultipleItemsMessage { get; set; }

    [DataMember]
    public IEnumerable<Guid> SucceededItemsIds { get; set; }

    [DataMember]
    public IEnumerable<Guid> FailedItemsIds { get; set; }

    [DataMember]
    public NotificationType Type { get; set; }

    internal static OperationResult SuccessResult() => new OperationResult()
    {
      Success = true
    };

    internal static OperationResult FailureResult() => new OperationResult()
    {
      Success = false
    };
  }
}
