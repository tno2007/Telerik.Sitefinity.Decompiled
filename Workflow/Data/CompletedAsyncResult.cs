// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Data.CompletedAsyncResult`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Workflow.Data
{
  internal class CompletedAsyncResult<T> : AsyncResult
  {
    private T data;

    public CompletedAsyncResult(T data, AsyncCallback callback, object state)
      : base(callback, state)
    {
      this.data = data;
      this.Complete(true);
    }

    public static T End(IAsyncResult result) => AsyncResult.End<CompletedAsyncResult<T>>(result).data;
  }
}
