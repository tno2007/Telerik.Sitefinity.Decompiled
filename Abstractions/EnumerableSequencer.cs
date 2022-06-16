// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.EnumerableSequencer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Abstractions
{
  public class EnumerableSequencer : IEnumerable, IEnumerator, IDisposable
  {
    private IEnumerable[] enumerables;
    private List<IEnumerator> usedEnumerators;
    private IEnumerator currentEnumerator;
    private int indexOfCurrentEnumerable;
    private IEnumerable currentEnumerable;

    public EnumerableSequencer(params IEnumerable[] args)
    {
      if (args == null || args.Length == 0)
      {
        this.enumerables = new IEnumerable[1];
        this.enumerables[0] = (IEnumerable) new object[0];
      }
      else
        this.enumerables = args;
      this.indexOfCurrentEnumerable = 0;
      this.currentEnumerable = this.enumerables[this.indexOfCurrentEnumerable];
      this.usedEnumerators = new List<IEnumerator>();
    }

    public IEnumerator GetEnumerator() => (IEnumerator) this;

    public void Dispose()
    {
      for (int index = 0; index < this.usedEnumerators.Count; ++index)
      {
        if (this.usedEnumerators[index] is IDisposable usedEnumerator)
          usedEnumerator.Dispose();
      }
    }

    public object Current => this.currentEnumerator.Current;

    public bool MoveNext()
    {
      if (this.currentEnumerator == null)
      {
        this.currentEnumerator = this.currentEnumerable.GetEnumerator();
        this.usedEnumerators.Add(this.currentEnumerator);
      }
      if (this.currentEnumerator.MoveNext())
        return true;
      if (this.indexOfCurrentEnumerable + 1 >= this.enumerables.Length)
        return false;
      ++this.indexOfCurrentEnumerable;
      this.currentEnumerable = this.enumerables[this.indexOfCurrentEnumerable];
      this.currentEnumerator = (IEnumerator) null;
      return this.MoveNext();
    }

    public void Reset() => this.currentEnumerator.Reset();
  }
}
