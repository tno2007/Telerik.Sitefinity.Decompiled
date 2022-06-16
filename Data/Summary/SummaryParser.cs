// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Summary.SummaryParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Data.Summary
{
  /// <summary>Summary parser</summary>
  public static class SummaryParser
  {
    private static readonly object syncRoot = new object();
    private static readonly List<string> oldTags = new List<string>((IEnumerable<string>) new string[4]
    {
      "br",
      "img",
      "hr",
      "input"
    });
    private static Regex wordRegex;
    private const string wordRegexPattern = "\\w+";

    /// <summary>
    /// Parse a string of <paramref name="text" />, containing HTMl, and return a summary of it,
    /// as specified by the summary <paramref name="settings" />
    /// </summary>
    /// <param name="text">HTML to get a summary of</param>
    /// <param name="settings">Summary settings</param>
    /// <returns>HTML, containing the summary of <paramref name="text" /></returns>
    public static string GetSummary(string text, SummarySettings settings)
    {
      if (string.IsNullOrEmpty(text) || settings.Mode == SummaryMode.None)
        return text;
      lock (SummaryParser.syncRoot)
      {
        int num1 = settings.Count;
        switch (num1)
        {
          case -1:
            if (!settings.ClearFormatting)
              return text;
            num1 = int.MaxValue;
            break;
          case 0:
            return string.Empty;
        }
        int num2 = 0;
        StringBuilder stringBuilder = new StringBuilder();
        HtmlParser htmlParser = new HtmlParser(text);
        Stack<string> stringStack = new Stack<string>();
        List<string> stringList = new List<string>();
        if (!settings.ClearAllTags)
        {
          foreach (string str in settings.TagsToClear)
          {
            if (!string.IsNullOrEmpty(str.Trim()))
              stringList.Add(str.Trim());
          }
        }
        HtmlChunkType? nullable1 = new HtmlChunkType?();
        HtmlChunk next1;
        while ((next1 = htmlParser.ParseNext()) != null)
        {
          switch (next1.Type)
          {
            case HtmlChunkType.Text:
              string[] strArray = next1.GenerateHtml().Split(' ');
              for (int index = 0; index < strArray.Length; ++index)
              {
                string str = strArray[index];
                if (!string.IsNullOrEmpty(str))
                {
                  if (num2++ < num1)
                  {
                    stringBuilder.Append(str);
                    if (index < strArray.Length - 1)
                      stringBuilder.Append(" ");
                  }
                  else
                    break;
                }
                else
                  stringBuilder.Append(" ");
              }
              break;
            case HtmlChunkType.OpenTag:
              HtmlChunkType? nullable2 = nullable1;
              HtmlChunkType htmlChunkType = HtmlChunkType.CloseTag;
              if (nullable2.GetValueOrDefault() == htmlChunkType & nullable2.HasValue)
                stringBuilder.Append(" ");
              bool flag1 = stringList.Contains(next1.TagName) || settings.ClearAllTags;
              if (!settings.ClearFormatting && !flag1 || settings.ClearFormatting & flag1)
              {
                if (!next1.IsEndClosure && !SummaryParser.oldTags.Contains(next1.TagName))
                  stringStack.Push(next1.TagName);
                stringBuilder.Append(next1.GenerateHtml());
              }
              if (next1.TagName == "style")
              {
                HtmlChunk next2 = htmlParser.ParseNext();
                while (next2.Type != HtmlChunkType.OpenTag && next2.Type != HtmlChunkType.CloseTag)
                  next2 = htmlParser.ParseNext();
                break;
              }
              break;
            case HtmlChunkType.CloseTag:
              bool flag2 = stringList.Contains(next1.TagName) || settings.ClearAllTags;
              if (!settings.ClearFormatting && !flag2 || settings.ClearFormatting & flag2)
              {
                if (stringStack.Count > 0 && stringStack.Peek() == next1.TagName)
                  stringStack.Pop();
                stringBuilder.Append(next1.GenerateHtml());
                break;
              }
              break;
          }
          nullable1 = new HtmlChunkType?(next1.Type);
          if (num2 >= num1)
          {
            stringBuilder.Append("...");
            break;
          }
        }
        while (stringStack.Count > 0)
          stringBuilder.Append("</" + stringStack.Pop() + ">");
        return stringBuilder.ToString();
      }
    }

    /// <summary>
    /// Gets a length limited version of a string, avoiding word truncation.
    /// The result might be slightly shorter, but not longer.
    /// </summary>
    /// <param name="value">The possibly long value.</param>
    /// <param name="charCount">The maximum number of characters.</param>
    internal static string GetTextSummary(string value, int charCount)
    {
      if (value == null || value.Length <= charCount)
        return value;
      int length = 0;
      if (SummaryParser.wordRegex == null)
        SummaryParser.wordRegex = new Regex("\\w+", RegexOptions.Compiled);
      foreach (Match match in SummaryParser.wordRegex.Matches(value))
      {
        int num = match.Index + match.Length;
        if (num <= charCount)
          length = num;
        else
          break;
      }
      return value.Substring(0, length);
    }
  }
}
