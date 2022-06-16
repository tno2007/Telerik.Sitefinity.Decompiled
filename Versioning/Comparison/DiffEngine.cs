// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.DiffEngine
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  internal class DiffEngine
  {
    private string[] cssClasses = new string[2]
    {
      "diff_new",
      "diff_deleted"
    };
    private HtmlParser _parser;
    internal string BeginTag = "<span class=\"{0}\">";
    internal string EndTag = "</span>";

    public string DeletedCSSClassName
    {
      get => this.cssClasses[1];
      set => this.cssClasses[1] = value;
    }

    public string AddedCSSClassName
    {
      get => this.cssClasses[0];
      set => this.cssClasses[0] = value;
    }

    private string GetBeginTag(DiffEngine.DiffType diffType) => string.Format(this.BeginTag, (object) this.cssClasses[(int) diffType]);

    public string GetEndTag() => this.EndTag;

    private string GetDiffImage(DiffEngine.DiffType diffType) => string.Format("<table class=\"{0}\"><tr><td>$1</td></tr></table>", (object) this.cssClasses[(int) diffType]);

    public HtmlParser Parser
    {
      get
      {
        if (this._parser == null)
          this._parser = new HtmlParser();
        return this._parser;
      }
    }

    private string GetCombinedSnippetHTML(
      string content,
      SnippetCollection snippets,
      int start,
      int length,
      DiffEngine.DiffType diff)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      for (int index = start; index < start + length; ++index)
      {
        Snippet snippet = snippets[index];
        string s = content.Substring(snippet.Index, snippets[index + 1].Index - snippet.Index);
        if (snippet is WordSnippet || snippet is SymbolSnippet)
        {
          if (!flag)
          {
            flag = true;
            stringBuilder.Append(this.GetBeginTag(diff));
          }
          stringBuilder.Append(HttpUtility.HtmlEncode(s));
        }
        else
        {
          if (flag)
          {
            flag = false;
            stringBuilder.Append(this.GetEndTag());
          }
          stringBuilder.Append(HttpUtility.HtmlEncode(s));
        }
      }
      if (flag)
        stringBuilder.Append(this.GetEndTag());
      return stringBuilder.ToString();
    }

    private string GetDifferenceHtml(
      string content1,
      SnippetCollection snippets1,
      string content2,
      SnippetCollection snippets2,
      DiffEngine.Item currentItem)
    {
      string combinedSnippetHtml1 = this.GetCombinedSnippetHTML(content2, snippets2, currentItem.StartB, currentItem.insertedB, DiffEngine.DiffType.Deleted);
      string combinedSnippetHtml2 = this.GetCombinedSnippetHTML(content1, snippets1, currentItem.StartA, currentItem.deletedA, DiffEngine.DiffType.New);
      string diffImage = this.GetDiffImage(DiffEngine.DiffType.Deleted);
      return Regex.Replace(combinedSnippetHtml1, "(<img[^>]+>)", diffImage, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) + Regex.Replace(combinedSnippetHtml2, "(<img[^>]+>)", this.GetDiffImage(DiffEngine.DiffType.New), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    }

    public string GetDiffs(string content1, string content2)
    {
      SnippetCollection snippetCollection1 = this.Parser.Parse(content1);
      SnippetCollection snippetCollection2 = this.Parser.Parse(content2);
      DiffEngine.Item[] objArray = this.DiffText(snippetCollection1, snippetCollection2);
      StringBuilder stringBuilder = new StringBuilder(content1);
      for (int index = objArray.Length - 1; index >= 0; --index)
      {
        Snippet snippet1 = snippetCollection1[objArray[index].StartA];
        Snippet snippet2 = snippetCollection1[objArray[index].StartA + objArray[index].deletedA];
        string differenceHtml = this.GetDifferenceHtml(content1, snippetCollection1, content2, snippetCollection2, objArray[index]);
        stringBuilder = stringBuilder.Remove(snippet1.Index, snippet2.Index - snippet1.Index).Insert(snippet1.Index, differenceHtml);
      }
      return stringBuilder.ToString();
    }

    private DiffEngine.Item[] DiffText(SnippetCollection TextA, SnippetCollection TextB)
    {
      Hashtable h = new Hashtable(TextA.Count + TextB.Count);
      DiffEngine.DiffData diffData1 = new DiffEngine.DiffData(this.DiffCodes(TextA, h));
      DiffEngine.DiffData diffData2 = new DiffEngine.DiffData(this.DiffCodes(TextB, h));
      int num = diffData1.Length + diffData2.Length + 1;
      int[] DownVector = new int[2 * num + 2];
      int[] UpVector = new int[2 * num + 2];
      this.LCS(diffData1, 0, diffData1.Length, diffData2, 0, diffData2.Length, DownVector, UpVector);
      this.Optimize(diffData1);
      this.Optimize(diffData2);
      return this.CreateDiffs(diffData1, diffData2);
    }

    private void Optimize(DiffEngine.DiffData Data)
    {
      int index1 = 0;
      while (index1 < Data.Length)
      {
        while (index1 < Data.Length && !Data.modified[index1])
          ++index1;
        int index2 = index1;
        while (index2 < Data.Length && Data.modified[index2])
          ++index2;
        if (index2 < Data.Length && Data.data[index1] == Data.data[index2])
        {
          Data.modified[index1] = false;
          Data.modified[index2] = true;
        }
        else
          index1 = index2;
      }
    }

    private int[] DiffCodes(SnippetCollection aText, Hashtable h)
    {
      int count = h.Count;
      int[] numArray = new int[aText.Count];
      for (int index = 0; index < aText.Count; ++index)
      {
        string text = aText[index].Text;
        object obj = h[(object) text];
        if (obj == null)
        {
          ++count;
          h[(object) text] = (object) count;
          numArray[index] = count;
        }
        else
          numArray[index] = (int) obj;
      }
      return numArray;
    }

    private DiffEngine.SMSRD SMS(
      DiffEngine.DiffData DataA,
      int LowerA,
      int UpperA,
      DiffEngine.DiffData DataB,
      int LowerB,
      int UpperB,
      int[] DownVector,
      int[] UpVector)
    {
      int num1 = DataA.Length + DataB.Length + 1;
      int num2 = LowerA - LowerB;
      int num3 = UpperA - UpperB;
      bool flag = (uint) (UpperA - LowerA - (UpperB - LowerB) & 1) > 0U;
      int num4 = num1 - num2;
      int num5 = num1 - num3;
      int num6 = (UpperA - LowerA + UpperB - LowerB) / 2 + 1;
      DownVector[num4 + num2 + 1] = LowerA;
      UpVector[num5 + num3 - 1] = UpperA;
      for (int index1 = 0; index1 <= num6; ++index1)
      {
        for (int index2 = num2 - index1; index2 <= num2 + index1; index2 += 2)
        {
          int index3;
          if (index2 == num2 - index1)
          {
            index3 = DownVector[num4 + index2 + 1];
          }
          else
          {
            index3 = DownVector[num4 + index2 - 1] + 1;
            if (index2 < num2 + index1 && DownVector[num4 + index2 + 1] >= index3)
              index3 = DownVector[num4 + index2 + 1];
          }
          for (int index4 = index3 - index2; index3 < UpperA && index4 < UpperB && DataA.data[index3] == DataB.data[index4]; ++index4)
            ++index3;
          DownVector[num4 + index2] = index3;
          if (flag && num3 - index1 < index2 && index2 < num3 + index1 && UpVector[num5 + index2] <= DownVector[num4 + index2])
          {
            DiffEngine.SMSRD smsrd;
            smsrd.x = DownVector[num4 + index2];
            smsrd.y = DownVector[num4 + index2] - index2;
            return smsrd;
          }
        }
        for (int index5 = num3 - index1; index5 <= num3 + index1; index5 += 2)
        {
          int num7;
          if (index5 == num3 + index1)
          {
            num7 = UpVector[num5 + index5 - 1];
          }
          else
          {
            num7 = UpVector[num5 + index5 + 1] - 1;
            if (index5 > num3 - index1 && UpVector[num5 + index5 - 1] < num7)
              num7 = UpVector[num5 + index5 - 1];
          }
          for (int index6 = num7 - index5; num7 > LowerA && index6 > LowerB && DataA.data[num7 - 1] == DataB.data[index6 - 1]; --index6)
            --num7;
          UpVector[num5 + index5] = num7;
          if (!flag && num2 - index1 <= index5 && index5 <= num2 + index1 && UpVector[num5 + index5] <= DownVector[num4 + index5])
          {
            DiffEngine.SMSRD smsrd;
            smsrd.x = DownVector[num4 + index5];
            smsrd.y = DownVector[num4 + index5] - index5;
            return smsrd;
          }
        }
      }
      return new DiffEngine.SMSRD();
    }

    private void LCS(
      DiffEngine.DiffData DataA,
      int LowerA,
      int UpperA,
      DiffEngine.DiffData DataB,
      int LowerB,
      int UpperB,
      int[] DownVector,
      int[] UpVector)
    {
      for (; LowerA < UpperA && LowerB < UpperB && DataA.data[LowerA] == DataB.data[LowerB]; ++LowerB)
        ++LowerA;
      for (; LowerA < UpperA && LowerB < UpperB && DataA.data[UpperA - 1] == DataB.data[UpperB - 1]; --UpperB)
        --UpperA;
      if (LowerA == UpperA)
      {
        while (LowerB < UpperB)
          DataB.modified[LowerB++] = true;
      }
      else if (LowerB == UpperB)
      {
        while (LowerA < UpperA)
          DataA.modified[LowerA++] = true;
      }
      else
      {
        DiffEngine.SMSRD smsrd = this.SMS(DataA, LowerA, UpperA, DataB, LowerB, UpperB, DownVector, UpVector);
        this.LCS(DataA, LowerA, smsrd.x, DataB, LowerB, smsrd.y, DownVector, UpVector);
        this.LCS(DataA, smsrd.x, UpperA, DataB, smsrd.y, UpperB, DownVector, UpVector);
      }
    }

    private DiffEngine.Item[] CreateDiffs(
      DiffEngine.DiffData DataA,
      DiffEngine.DiffData DataB)
    {
      ArrayList arrayList = new ArrayList();
      int index1 = 0;
      int index2 = 0;
      while (index1 < DataA.Length || index2 < DataB.Length)
      {
        if (index1 < DataA.Length && !DataA.modified[index1] && index2 < DataB.Length && !DataB.modified[index2])
        {
          ++index1;
          ++index2;
        }
        else
        {
          int num1 = index1;
          int num2 = index2;
          while (index1 < DataA.Length && (index2 >= DataB.Length || DataA.modified[index1]))
            ++index1;
          while (index2 < DataB.Length && (index1 >= DataA.Length || DataB.modified[index2]))
            ++index2;
          if (num1 < index1 || num2 < index2)
            arrayList.Add((object) new DiffEngine.Item()
            {
              StartA = num1,
              StartB = num2,
              deletedA = (index1 - num1),
              insertedB = (index2 - num2)
            });
        }
      }
      DiffEngine.Item[] diffs = new DiffEngine.Item[arrayList.Count];
      arrayList.CopyTo((Array) diffs);
      return diffs;
    }

    private struct SMSRD
    {
      internal int x;
      internal int y;
    }

    internal class DiffData
    {
      internal int Length;
      internal int[] data;
      internal bool[] modified;

      internal DiffData(int[] initData)
      {
        this.data = initData;
        this.Length = initData.Length;
        this.modified = new bool[this.Length + 2];
      }
    }

    internal enum DiffType
    {
      New,
      Deleted,
    }

    internal struct Item
    {
      public int StartA;
      public int StartB;
      public int deletedA;
      public int insertedB;
    }
  }
}
