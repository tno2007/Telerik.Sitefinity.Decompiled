// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.Snippet
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Versioning.Comparison
{
  internal abstract class Snippet
  {
    private int _index;
    private int _length;

    protected Snippet(int index, int length)
    {
      this._index = index;
      this._length = length;
    }

    public int Index
    {
      get => this._index;
      set => this._index = value;
    }

    public int Length
    {
      get => this._length;
      set => this._length = value;
    }

    public abstract bool Equals(Snippet snippet);

    public abstract string Text { get; }
  }
}
