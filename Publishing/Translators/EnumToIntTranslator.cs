// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.EnumToIntTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class EnumToIntTranslator : TranslatorBase
  {
    public static string TranslatorName = nameof (EnumToIntTranslator);

    public override string Name => EnumToIntTranslator.TranslatorName;

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      if (valuesToTranslate.Length > 1)
        throw new ArgumentException("Too many arguents", nameof (valuesToTranslate));
      if (valuesToTranslate.Length == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      return valuesToTranslate[0] != null ? (object) Convert.ToInt32(valuesToTranslate[0]) : throw new ArgumentNullException(nameof (valuesToTranslate), "Enum value is null");
    }

    public new Dictionary<string, string> GetDefaultSettings() => new Dictionary<string, string>();
  }
}
