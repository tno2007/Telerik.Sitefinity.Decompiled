// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.LstringTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Translates a list of lstrings into a list of strings</summary>
  public class LstringTranslator : TranslatorBase
  {
    public const string TranslatorName = "lstringtranslator";

    public override string Name => "lstringtranslator";

    /// <summary>Converts lstrings into strings</summary>
    /// <param name="settings">Pipe settings</param>
    /// <param name="lstrings">Array of listring-s</param>
    /// <returns>Array of string-s</returns>
    public override object Translate(
      object[] lstrings,
      IDictionary<string, string> translationSettings)
    {
      string[] strArray = new string[lstrings.Length];
      for (int index = 0; index < lstrings.Length; ++index)
        strArray[index] = ((Lstring) lstrings[index]).Value;
      return (object) strArray;
    }
  }
}
