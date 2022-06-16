// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.RegExTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Regex translator - removes a regex</summary>
  public class RegExTranslator : TranslatorBase
  {
    public const string TranslatorName = "regextranslator";

    public override string Name => "regextranslator";

    /// <summary>
    /// Accept a list of objects convertible to string and remove a regex
    /// </summary>
    /// <param name="settings">Pipe settings</param>
    /// <param name="data">Objects that will be concatenated and joined to form the regex input</param>
    /// <returns>Joined input with removed by regex parts</returns>
    public override object Translate(object[] data, IDictionary<string, string> translationSettings)
    {
      string pattern = ".*";
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object obj in data)
        stringBuilder.Append(ConcatenationTranslator.GetString(obj));
      return (object) Regex.Replace(stringBuilder.ToString(), pattern, string.Empty);
    }
  }
}
