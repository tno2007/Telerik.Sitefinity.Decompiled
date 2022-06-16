// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.HtmlProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Utilities.CssParsing;
using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This class provides functionality for processing Sitefinity generated markup into
  /// email acceptable markup.
  /// </summary>
  public static class HtmlProcessor
  {
    private static Regex espacesRegEx = new Regex("(\\t|\\n|\\r)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Processes the native Sitefinity html and returns email acceptable html.
    /// </summary>
    /// <param name="html">Native Sitefinity html.</param>
    /// <param name="rootUrl">Root url of the web site.</param>
    /// <returns>Email acceptable html.</returns>
    public static string ProcessHtml(string html, Uri rootUrl = null)
    {
      if (rootUrl != (Uri) null)
        html = CssMerger.DownloadAndInlineCss(html, rootUrl);
      StringBuilder builder = new StringBuilder();
      using (HtmlParser htmlParser = new HtmlParser(html))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = true;
        htmlParser.KeepRawHTML = false;
        bool flag = false;
        IList<string> generatedTags = (IList<string>) new List<string>();
        Stack<ChunkInfo> openedTags = new Stack<ChunkInfo>();
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          string str = (string) null;
          generatedTags.Clear();
          if (!flag)
            flag = HtmlProcessor.HasReachedStart(next);
          else if (!HtmlProcessor.HasReadEnd(next))
          {
            if (!HtmlProcessor.SkipOnce(next))
            {
              string tagName1 = next.TagName;
              if (string.Equals(next.TagName, "div", StringComparison.OrdinalIgnoreCase))
              {
                string attributeValue = HtmlProcessor.GetAttributeValue(next, "class");
                if (!string.IsNullOrWhiteSpace(attributeValue))
                {
                  IEnumerable<string> source = ((IEnumerable<string>) attributeValue.Split(new char[1]
                  {
                    ' '
                  }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (c => c.Trim()));
                  if (source.Contains<string>("sfPublicWrapper", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) || source.Contains<string>("sf_cols", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
                  {
                    HtmlProcessor.ChangeToTable(next);
                    str = HtmlProcessor.GenerateRow(source.Contains<string>("sfPublicWrapper", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase), generatedTags);
                  }
                  else if (source.Contains<string>("sf_colsOut", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
                    HtmlProcessor.ChangeToColumn(next, source.ToArray<string>());
                }
              }
              if (!next.IsEndClosure && string.Equals(tagName1, "div", StringComparison.OrdinalIgnoreCase))
              {
                if (next.Type == HtmlChunkType.OpenTag)
                  openedTags.Push(new ChunkInfo(next.TagName));
                else if (next.Type == HtmlChunkType.CloseTag)
                  HtmlProcessor.TryCloseChangeChunk(next, builder, openedTags);
              }
              string html1 = next.GenerateHtml();
              if (!string.IsNullOrEmpty(str))
              {
                html1 += str;
                foreach (string tagName2 in (IEnumerable<string>) generatedTags)
                  openedTags.Push(new ChunkInfo(tagName2, true));
              }
              builder.Append(HtmlProcessor.CleanHtml(html1));
            }
          }
          else
            break;
        }
      }
      return HttpUtility.HtmlDecode(builder.ToString().Trim());
    }

    private static bool HasReachedStart(HtmlChunk chunk) => chunk.Type == HtmlChunkType.OpenTag && chunk.TagName == "form";

    private static bool HasReadEnd(HtmlChunk chunk) => chunk.Type == HtmlChunkType.CloseTag && chunk.TagName == "form";

    private static bool SkipOnce(HtmlChunk chunk) => chunk.IsComment || chunk.TagName == "script" || chunk.TagName == "input" || chunk.Html == "\\r" || chunk.Html == "\\n" || string.IsNullOrEmpty(chunk.TagName) && string.IsNullOrEmpty(chunk.Html.Trim());

    private static string GetAttributeValue(HtmlChunk chunk, string attributeName)
    {
      int index = Array.FindIndex<string>(chunk.Attributes, 0, chunk.ParamsCount, (Predicate<string>) (i => i.Equals(attributeName, StringComparison.OrdinalIgnoreCase)));
      return index != -1 ? chunk.Values[index] : (string) null;
    }

    private static void ChangeToTable(HtmlChunk chunk)
    {
      chunk.TagName = "table";
      chunk.AddAttribute("cellpadding", "0");
      chunk.AddAttribute("cellspacing", "0");
      chunk.AddAttribute("border", "0");
    }

    private static string GenerateRow(bool isMainContainer, IList<string> generatedTags)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<tr>");
      generatedTags.Add("tr");
      if (isMainContainer)
      {
        stringBuilder.Append("<td valign=\"top\">");
        generatedTags.Add("td");
      }
      return stringBuilder.ToString();
    }

    private static void ChangeToColumn(HtmlChunk chunk, string[] classes)
    {
      string str1 = classes[classes.Length - 1];
      string str2 = str1.Substring(str1.LastIndexOf('_') + 1);
      string attributeValue = HtmlProcessor.GetAttributeValue(chunk, "style");
      bool flag = false;
      if (!string.IsNullOrEmpty(attributeValue))
        flag = attributeValue.Contains("width");
      if (!string.IsNullOrEmpty(str2) && !flag)
        chunk.AddAttribute("width", str2 + "%");
      else if (chunk.HasAttribute("width"))
        chunk.RemoveAttribute("width");
      chunk.TagName = "td";
      chunk.RemoveAttribute("class");
      chunk.AddAttribute("valign", "top");
    }

    private static void TryCloseChangeChunk(
      HtmlChunk chunk,
      StringBuilder builder,
      Stack<ChunkInfo> openedTags)
    {
      if (openedTags.Count == 0)
        return;
      ChunkInfo chunkInfo = openedTags.Pop();
      if (chunkInfo.IsArtificial)
      {
        string tagName = chunkInfo.TagName;
        if (!(tagName == "td"))
        {
          if (!(tagName == "tr"))
          {
            if (!(tagName == "table"))
              throw new NotSupportedException();
            builder.Append("</table>");
          }
          else
            builder.Append("</tr>");
        }
        else
          builder.Append("</td>");
        HtmlProcessor.TryCloseChangeChunk(chunk, builder, openedTags);
      }
      else
      {
        string tagName = chunkInfo.TagName;
        if (!(tagName == "td") && !(tagName == "tr") && !(tagName == "table"))
          return;
        chunk.TagName = chunkInfo.TagName;
      }
    }

    private static string CleanHtml(string html) => HtmlProcessor.espacesRegEx.Replace(html, "");
  }
}
