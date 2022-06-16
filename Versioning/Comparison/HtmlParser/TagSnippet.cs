// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.TagSnippet
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>Summary description for TagSnippet.</summary>
  internal class TagSnippet : Snippet
  {
    private string _text;

    public TagSnippet(int index, int length, string text)
      : base(index, length)
    {
      this._text = text;
    }

    public override string Text => this._text;

    public override bool Equals(Snippet snippet) => snippet is TagSnippet && this._text == snippet.Text;
  }
}
