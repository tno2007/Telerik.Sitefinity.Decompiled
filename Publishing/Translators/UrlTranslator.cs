// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.UrlTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class UrlTranslator : TranslatorBase
  {
    public const string TranslatorName = "urltranslator";

    public override string Name => "urltranslator";

    /// <summary>Translates url names into actual urls</summary>
    /// <param name="settings">Sitefinity content settings.</param>
    /// <param name="data">List of url names.</param>
    /// <returns></returns>
    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      return (object) "http://telerik.com";
    }
  }
}
