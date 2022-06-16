// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ContentIdentifiersTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Translators;

namespace Telerik.Sitefinity.SiteSync
{
  internal class ContentIdentifiersTranslator : TranslatorBase
  {
    public const string TranslatorName = "contentIdentifiersTranslator";

    /// <summary>Name of the translator. Used for resolving.</summary>
    public override string Name => "contentIdentifiersTranslator";

    /// <summary>Translates the specified list of values.</summary>
    /// <param name="valuesToTranslate">The values to translate.</param>
    /// <param name="translationSettings">The translation settings.</param>
    /// <returns></returns>
    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      Guid guid1 = Guid.Empty;
      if (valuesToTranslate.Length != 0)
      {
        for (int index = 0; index < valuesToTranslate.Length; ++index)
        {
          object g = valuesToTranslate[index];
          if (g != null)
          {
            if (g.GetType() == typeof (Guid))
            {
              Guid guid2 = (Guid) g;
              if (guid2 != Guid.Empty)
              {
                guid1 = guid2;
                break;
              }
            }
            else if (g.GetType() == typeof (string))
            {
              Guid guid3 = new Guid((string) g);
              if (guid3 != Guid.Empty)
              {
                guid1 = guid3;
                break;
              }
            }
          }
        }
      }
      return (object) guid1;
    }
  }
}
