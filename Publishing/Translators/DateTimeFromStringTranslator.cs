// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.DateTimeFromStringTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class DateTimeFromStringTranslator : TranslatorBase
  {
    public const string TranslatorName = "datetimefromstring";

    public override string Name => "datetimefromstring";

    public override object Translate(object[] data, IDictionary<string, string> translationSettings) => (object) DateTime.Parse((string) ((IEnumerable<object>) data).First<object>());
  }
}
