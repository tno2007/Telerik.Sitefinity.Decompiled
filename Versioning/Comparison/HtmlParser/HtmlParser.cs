// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.HtmlParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>Summary description for HtmlParser.</summary>
  internal class HtmlParser
  {
    private string buffer = string.Empty;
    private Regex _tagRegEx;
    private Regex _attribRegEx;
    private Regex _symbolsRegEx;
    private Regex _separatorsRegEx;
    private Regex _wordRegEx;

    public Regex TagRegEx
    {
      get
      {
        if (this._tagRegEx == null)
          this._tagRegEx = new Regex("<\\s*(\\S+?)(\\s[^>]*)?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this._tagRegEx;
      }
    }

    public Regex AttribRegEx
    {
      get
      {
        if (this._attribRegEx == null)
          this._attribRegEx = new Regex("\\s*(\\S+)=(\"{0,1})(\\S+)\\2\\s*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this._attribRegEx;
      }
    }

    public Regex SymbolsRegEx
    {
      get
      {
        if (this._symbolsRegEx == null)
          this._symbolsRegEx = new Regex("&[A-Za-z]{2,4};", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this._symbolsRegEx;
      }
    }

    public Regex SeparatorsRegEx
    {
      get
      {
        if (this._separatorsRegEx == null)
          this._separatorsRegEx = new Regex("[,\\.;:\\(\\)\\[\\]\\{\\}]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this._separatorsRegEx;
      }
    }

    public Regex WordRegEx
    {
      get
      {
        if (this._wordRegEx == null)
          this._wordRegEx = new Regex("\\S+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this._wordRegEx;
      }
    }

    public SnippetCollection Parse(string input)
    {
      this.buffer = input;
      SnippetCollection snippets = new SnippetCollection();
      snippets.Add((Snippet) new TagSnippet(0, 0, "content"));
      this.ParseHtmlTags(snippets);
      snippets.Add((Snippet) new TagSnippet(input.Length, 0, "/content"));
      this.buffer = string.Empty;
      return snippets;
    }

    private void ParseHtmlTags(SnippetCollection snippets) => this.ParseTags(snippets, 0, this.buffer.Length);

    private void ParseTags(SnippetCollection snippets, int start, int length)
    {
      int start1 = start;
      for (Match match = this.TagRegEx.Match(this.buffer, start, length); match.Success; match = match.NextMatch())
      {
        if (match.Index > start1)
          this.ParseHtmlText(snippets, start1, match.Index - start1);
        Snippet snippet;
        if (match.Groups[1].Value.ToLower() == "img")
        {
          snippet = (Snippet) new ImageSnippet(match.Index, match.Length, match.Value);
          this.SetAttributes((ImageSnippet) snippet, match.Groups[2].Value);
        }
        else
          snippet = (Snippet) new TagSnippet(match.Index, match.Length, match.Value);
        snippets.Add(snippet);
        start1 = match.Index + match.Length;
      }
      int length1 = start + length - start1;
      if (length1 <= 0)
        return;
      this.ParseHtmlText(snippets, start1, length1);
    }

    private void ParseHtmlText(SnippetCollection snippets, int start, int length)
    {
      int start1 = start;
      for (Match match = this.SymbolsRegEx.Match(this.buffer, start, length); match.Success; match = match.NextMatch())
      {
        if (match.Index > start1)
          this.ParseSentences(snippets, start1, match.Index - start1);
        SymbolSnippet symbolSnippet = new SymbolSnippet(match.Index, match.Length, match.Value);
        snippets.Add((Snippet) symbolSnippet);
        start1 = match.Index + match.Length;
      }
      int length1 = start + length - start1;
      if (length1 <= 0)
        return;
      this.ParseSentences(snippets, start1, length1);
    }

    private void ParseSentences(SnippetCollection snippets, int start, int length)
    {
      int start1 = start;
      for (Match match = this.SeparatorsRegEx.Match(this.buffer, start, length); match.Success; match = match.NextMatch())
      {
        if (match.Index > start1)
          this.ParseWords(snippets, start1, match.Index - start1);
        SymbolSnippet symbolSnippet = new SymbolSnippet(match.Index, match.Length, match.Value);
        snippets.Add((Snippet) symbolSnippet);
        start1 = match.Index + match.Length;
      }
      int length1 = start + length - start1;
      if (length1 <= 0)
        return;
      this.ParseWords(snippets, start1, length1);
    }

    private void ParseWords(SnippetCollection snippets, int start, int length)
    {
      for (Match match = this.WordRegEx.Match(this.buffer, start, length); match.Success; match = match.NextMatch())
      {
        WordSnippet wordSnippet = new WordSnippet(match.Index, match.Length, match.Value);
        snippets.Add((Snippet) wordSnippet);
      }
    }

    private void SetAttributes(ImageSnippet image, string input)
    {
      for (Match match = this.AttribRegEx.Match(input); match.Success; match = match.NextMatch())
      {
        if (match.Groups[1].Value.Trim().ToLower() == "src")
        {
          image.ImageUrl = match.Groups[3].Value;
          break;
        }
      }
    }
  }
}
