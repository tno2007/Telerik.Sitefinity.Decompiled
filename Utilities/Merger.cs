// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.Merger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Utilities
{
  /// <summary>
  /// This class is responsible for merging the merge tags of the messages through the context
  /// </summary>
  internal static class Merger
  {
    private static Regex regex = new Regex("(%7B%7C|\\{\\|)(.+?)\\.(.+?)(%7C%7D|\\|\\})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private const string MergeTagsStart = "(%7B%7C|\\{\\|)";
    private const string MergeTagsEnd = "(%7C%7D|\\|\\})";
    private const string MergeGroup = "(.+?)";

    /// <summary>
    /// Merges the merge tags from the source with the actual values provided by the context.
    /// <para>Each merge tag should be with the following syntax:</para>
    /// <para>{|Group1.Group2|} e.g. {|Subscriber.FirstName|}, {|Custom.CurrentDate|}</para>
    /// </summary>
    /// <param name="source">Text that contains merge tags to be merged.</param>
    /// <param name="context">Context that contains the objects from which the merge tag values ought to be obtained.</param>
    /// <param name="clearUnmergedTags">Indicated if the tags that are found but does not have corresponding context value should be removed.</param>
    /// <returns>Merged text with actual values.</returns>
    public static string MergeTags(
      string source,
      IDictionary<string, string> context,
      bool clearUnmergedTags = false)
    {
      return string.IsNullOrEmpty(source) || context == null || context.Count == 0 ? source : Merger.regex.Replace(source, (MatchEvaluator) (match => Merger.ReplaceMergeTag(match, context, clearUnmergedTags)));
    }

    private static string ReplaceMergeTag(
      Match m,
      IDictionary<string, string> context,
      bool clearUnmergedTags = false)
    {
      string key = m.Groups[2].Value.Trim() + "." + m.Groups[3].Value.Trim();
      string str;
      if (context.TryGetValue(key, out str))
        return str;
      return !clearUnmergedTags ? m.Value : string.Empty;
    }

    /// <summary>
    /// Clears all merge tags except the ones with the given skipCategories.
    /// E.g. if skipCategories contain a 'Subscriber' all tags that start with '{|Subscriber.' will be left untouched.
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="skipCategories">The categories of tags that will remain after the clear operation.</param>
    /// <returns>The modified string with cleaned merge tags</returns>
    public static string ClearMergeTags(string source, IEnumerable<string> skipCategories)
    {
      if (string.IsNullOrEmpty(source))
        return source;
      List<Regex> excludeRegexes = new List<Regex>();
      foreach (string skipCategory in skipCategories)
      {
        string pattern = "(%7B%7C|\\{\\|)(" + skipCategory + ").(.+?)(%7C%7D|\\|\\})";
        excludeRegexes.Add(new Regex(pattern));
      }
      return Merger.regex.Replace(source, (MatchEvaluator) (match => Merger.FilterMergeTag(match, excludeRegexes)));
    }

    private static string FilterMergeTag(Match match, List<Regex> excludeRegexes)
    {
      bool flag = false;
      foreach (Regex excludeRegex in excludeRegexes)
      {
        if (excludeRegex.IsMatch(match.Value))
        {
          flag = true;
          break;
        }
      }
      return !flag ? string.Empty : match.Value;
    }
  }
}
