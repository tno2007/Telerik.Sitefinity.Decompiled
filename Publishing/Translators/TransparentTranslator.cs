// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.TransparentTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class TransparentTranslator : TranslatorBase
  {
    public const string TranslatorName = "TransparentTranslator";

    public override string Name => nameof (TransparentTranslator);

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      return ((IEnumerable<object>) valuesToTranslate).Count<object>() == 1 ? valuesToTranslate[0] : (object) valuesToTranslate;
    }
  }
}
