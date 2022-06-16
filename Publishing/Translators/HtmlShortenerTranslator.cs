// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.HtmlShortenerTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Text;
using Telerik.Sitefinity.Data.Summary;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Shortens HTML</summary>
  public class HtmlShortenerTranslator : TranslatorBase
  {
    public const string TranslatorName = "htmlshortenertranslator";

    public override string Name => "htmlshortenertranslator";

    /// <summary>
    /// Accepts array of HTML-s and converts them into a single HTML
    /// </summary>
    /// <param name="settings">Pipe settings</param>
    /// <param name="htmlItems">HTML items</param>
    /// <returns>HTML</returns>
    public override object Translate(
      object[] htmlItems,
      IDictionary<string, string> translationSettings)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object htmlItem in htmlItems)
        stringBuilder.Append(ConcatenationTranslator.GetString(htmlItem));
      SummarySettings settings = new SummarySettings(SummaryMode.Words, 20, false, new string[4]
      {
        "img",
        "script",
        "object",
        "embed"
      });
      return (object) SummaryParser.GetSummary(stringBuilder.ToString(), settings);
    }
  }
}
