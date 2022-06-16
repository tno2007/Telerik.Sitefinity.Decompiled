// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.HtmlParsing.HtmlStripper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Utilities.HtmlParsing
{
  /// <summary>
  /// This class presents a functionality for stripping tags and attributes from an HTML code using a white-list or a black-list.
  /// </summary>
  internal static class HtmlStripper
  {
    internal static readonly HtmlStripperOptions StrippingOptions = new HtmlStripperOptions()
    {
      UseWhiteList = false,
      Tags = (IList<HtmlStripperTag>) new List<HtmlStripperTag>()
      {
        new HtmlStripperTag() { TagName = "script" },
        new HtmlStripperTag() { TagName = "style" },
        new HtmlStripperTag() { TagName = "link" },
        new HtmlStripperTag() { TagName = "iframe" },
        new HtmlStripperTag() { TagName = "meta" },
        new HtmlStripperTag() { TagName = "plaintext" },
        new HtmlStripperTag() { TagName = "embed" },
        new HtmlStripperTag() { TagName = "object" }
      },
      AttributeExpression = "^(?!on).+",
      ValueExpression = "^((?!.*javascript)(?!.*vbscript)(?!.*expression.*)(?!data).*)$"
    };
    private const string base64String = "base64";
    private static Regex tagRegex;
    private const string tagRegexPattern = "<[^>]+>";

    /// <summary>
    /// Strips tags and attributes from the passed HTML code using the given options.
    /// </summary>
    /// <param name="rawHtml">The raw HTML.</param>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    public static string StripHtml(string rawHtml, HtmlStripperOptions options)
    {
      bool useWhiteList = options.UseWhiteList;
      if (useWhiteList && (options.Tags == null || options.Tags.Count == 0))
        return rawHtml;
      StringBuilder stringBuilder = new StringBuilder(rawHtml.Length);
      using (HtmlParser htmlParser = new HtmlParser(rawHtml))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = false;
        htmlParser.KeepRawHTML = true;
        IList<HtmlStripperTag> tags = options.Tags;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          if (useWhiteList)
          {
            string chunkUsingWhiteList = HtmlStripper.ParseHtmlChunkUsingWhiteList(next, tags);
            stringBuilder.Append(chunkUsingWhiteList);
          }
          else
          {
            string chunkUsingBlackList = HtmlStripper.ParseHtmlChunkUsingBlackList(next, tags, options.AttributeExpression, options.ValueExpression);
            stringBuilder.Append(chunkUsingBlackList);
          }
        }
      }
      return stringBuilder.ToString();
    }

    public static string StripHtmlTags(string html)
    {
      if (string.IsNullOrEmpty(html))
        return html;
      if (HtmlStripper.tagRegex == null)
        HtmlStripper.tagRegex = new Regex("<[^>]+>", RegexOptions.Compiled);
      return HtmlStripper.tagRegex.Replace(html, string.Empty);
    }

    private static string ParseHtmlChunkUsingWhiteList(
      HtmlChunk chunk,
      IList<HtmlStripperTag> availableTags)
    {
      bool flag = false;
      if (chunk.Type == HtmlChunkType.OpenTag)
      {
        HtmlStripperTag tag = HtmlStripper.FindTag(chunk.TagName, availableTags);
        if (tag == null)
          return (string) null;
        string[] attributes = chunk.Attributes;
        if (chunk.ParamsCount > 0 && !tag.IgnoreAttributes)
        {
          for (int index = chunk.ParamsCount - 1; index >= 0; --index)
          {
            if (!HtmlStripper.IsValidTagAttribute(tag, attributes[index], chunk.Values[index].TrimStart()))
            {
              chunk.RemoveAttribute(attributes[index]);
              flag = true;
            }
          }
        }
        return !flag ? chunk.Html : chunk.GenerateHtml();
      }
      return chunk.Type == HtmlChunkType.CloseTag ? (HtmlStripper.FindTag(chunk.TagName, availableTags) == null ? (string) null : chunk.Html) : (chunk.Type == HtmlChunkType.Text ? chunk.Html : (string) null);
    }

    private static string ParseHtmlChunkUsingBlackList(
      HtmlChunk chunk,
      IList<HtmlStripperTag> exludeTags,
      string attributeExpression,
      string valueExpression)
    {
      if (chunk.Type == HtmlChunkType.OpenTag)
      {
        if (HtmlStripper.FindTag(chunk.TagName, exludeTags) != null)
          return (string) null;
        string[] attributes = chunk.Attributes;
        if (chunk.ParamsCount > 0)
        {
          for (int index = chunk.ParamsCount - 1; index >= 0; --index)
          {
            string attributeValue = chunk.Values[index].TrimStart();
            if (!HtmlStripper.IsValidTagAttribute(attributes[index], attributeValue, attributeExpression, valueExpression, exludeTags))
              chunk.RemoveAttribute(attributes[index]);
            if (attributeValue.Contains("base64"))
              chunk.Values[index] = HtmlStripper.ParsedBase64String(attributeValue, HtmlStripper.StrippingOptions);
          }
        }
        return chunk.GenerateHtml();
      }
      return chunk.Type == HtmlChunkType.CloseTag ? (HtmlStripper.FindTag(chunk.TagName, exludeTags) != null ? (string) null : chunk.Html) : (chunk.Type == HtmlChunkType.Text ? chunk.Html : (string) null);
    }

    private static HtmlStripperTag FindTag(
      string tagName,
      IList<HtmlStripperTag> tags)
    {
      if (tags == null || tags.Count == 0)
        return (HtmlStripperTag) null;
      for (int index = 0; index < tags.Count; ++index)
      {
        HtmlStripperTag tag = tags[index];
        if (tagName.Equals(tag.TagName, StringComparison.InvariantCultureIgnoreCase))
          return tag;
      }
      return (HtmlStripperTag) null;
    }

    private static bool IsValidTagAttribute(
      HtmlStripperTag tag,
      string attributeName,
      string attributeValue)
    {
      if (tag.AvailableAttributes == null || tag.AvailableAttributes.Count == 0)
        return false;
      for (int index = 0; index < tag.AvailableAttributes.Count; ++index)
      {
        HtmlStripperAttribute availableAttribute = tag.AvailableAttributes[index];
        if (attributeName.Equals(availableAttribute.Name, StringComparison.InvariantCultureIgnoreCase))
        {
          if (string.IsNullOrEmpty(availableAttribute.ValueExpression))
            return true;
          return attributeValue != null && Regex.IsMatch(attributeValue, availableAttribute.ValueExpression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }
      }
      return false;
    }

    private static bool IsValidTagAttribute(
      string attributeName,
      string attributeValue,
      string attributeExpression,
      string valueExpression,
      IList<HtmlStripperTag> excludeTags)
    {
      bool flag = true;
      if (!string.IsNullOrEmpty(attributeExpression))
      {
        flag = Regex.IsMatch(attributeName, attributeExpression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!flag)
          return false;
      }
      if (!string.IsNullOrEmpty(valueExpression))
      {
        if (attributeValue == null)
          return false;
        flag = Regex.IsMatch(attributeValue, valueExpression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
      }
      for (int index = 0; index < excludeTags.Count; ++index)
      {
        string str = string.Format("<{0}", (object) excludeTags[index].TagName);
        if (attributeName.StartsWith(str))
          return false;
      }
      return flag;
    }

    private static string ParsedBase64String(string attributeValue, HtmlStripperOptions options)
    {
      int startIndex = attributeValue.IndexOf("base64");
      string[] strArray = attributeValue.Substring(startIndex).Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length >= 2)
      {
        string s = strArray[1];
        try
        {
          attributeValue = HtmlStripper.StripHtml(Encoding.ASCII.GetString(Convert.FromBase64String(s)), options);
        }
        catch (FormatException ex)
        {
          return attributeValue;
        }
      }
      return attributeValue;
    }
  }
}
