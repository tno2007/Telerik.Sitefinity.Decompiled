// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.AuthorTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.SiteSync
{
  internal class AuthorTranslator : TranslatorBase
  {
    public const string TranslatorName = "authorTranslator";

    public override string Name => "authorTranslator";

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      if (valuesToTranslate.Length == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      foreach (object obj in valuesToTranslate)
      {
        if (obj != null)
        {
          string g = obj.ToString();
          if (!string.IsNullOrEmpty(g))
          {
            if (!g.IsGuid())
              return (object) g;
            Guid guid = new Guid(g);
            return (object) DataResolver.Resolve((object) new AuthorTranslator.SimpleOwnership()
            {
              Owner = guid
            }, "Author");
          }
        }
      }
      return (object) string.Empty;
    }

    private class SimpleOwnership : IOwnership
    {
      public Guid Owner { get; set; }
    }
  }
}
