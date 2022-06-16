// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.HtmlStripperTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Summary;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class HtmlStripperTranslator : TranslatorBase
  {
    public const string TranslatorName = "htmlStripperTranslator";

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => "htmlStripperTranslator";

    /// <summary>Translates the specified values to translate.</summary>
    /// <param name="valuesToTranslate">The values to translate.</param>
    /// <param name="translationSettings">The translation settings.</param>
    /// <returns></returns>
    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object obj in valuesToTranslate)
        stringBuilder.Append(ConcatenationTranslator.GetString(obj));
      SummarySettings settings = new SummarySettings(SummaryMode.Words, int.MaxValue, false, true);
      string str = string.Empty;
      try
      {
        str = SummaryParser.GetSummary(stringBuilder.ToString(), settings).Replace("&nbsp;", " ");
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
      for (Match match1 = Regex.Match(stringBuilder.ToString(), "<meta name=\"keywords\" content=\"[^<]*\"", RegexOptions.IgnoreCase | RegexOptions.Multiline); match1.Success; match1 = match1.NextMatch())
      {
        Match match2 = Regex.Match(match1.Value, "content=\"[^<]*\"");
        if (match2.Success)
          str = str + " " + match2.Value.Replace("content=", "").Replace("\"", "");
      }
      for (Match match3 = Regex.Match(stringBuilder.ToString(), "<meta name=\"description\" content=\"[^<]*\"", RegexOptions.IgnoreCase | RegexOptions.Multiline); match3.Success; match3 = match3.NextMatch())
      {
        Match match4 = Regex.Match(match3.Value, "content=\"[^<]*\"");
        if (match4.Success)
          str = str + " " + match4.Value.Replace("content=", "").Replace("\"", "");
      }
      return (object) str;
    }
  }
}
