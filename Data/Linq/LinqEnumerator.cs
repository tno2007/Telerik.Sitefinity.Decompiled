// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.LinqEnumerator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.Linq
{
  public class LinqEnumerator<TDataItem> : IEnumerator<TDataItem>, IDisposable, IEnumerator
  {
    private IEnumerator enumerator;

    public LinqEnumerator(IEnumerator enumerator) => this.enumerator = enumerator;

    public TDataItem Current => (TDataItem) this.enumerator.Current;

    public void Dispose()
    {
    }

    object IEnumerator.Current => this.enumerator.Current;

    public bool MoveNext() => this.enumerator.MoveNext();

    public void Reset() => this.enumerator.Reset();
  }
}
