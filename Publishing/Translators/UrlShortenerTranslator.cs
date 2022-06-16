// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.UrlShortenerTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UrlShorteners;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public class UrlShortenerTranslator : TranslatorBase
  {
    public const string TranslatorName = "UrlShortenerTranslator";

    public override string Name => nameof (UrlShortenerTranslator);

    /// <summary>Translates url names into actual urls</summary>
    /// <param name="settings">Sitefinity content settings.</param>
    /// <param name="data">List of url names.</param>
    /// <returns></returns>
    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      try
      {
        IUrlShortener urlShortener = ObjectFactory.Resolve<IUrlShortener>("BitLy");
        urlShortener.Initialize();
        return (object) urlShortener.ShortenUrl(valuesToTranslate[0].ToString());
      }
      catch (Exception ex)
      {
        return valuesToTranslate[0];
      }
    }
  }
}
