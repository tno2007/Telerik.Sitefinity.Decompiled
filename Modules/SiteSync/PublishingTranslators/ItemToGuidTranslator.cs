// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ItemToGuidTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Translators;

namespace Telerik.Sitefinity.SiteSync
{
  internal class ItemToGuidTranslator : TranslatorBase
  {
    public const string TranslatorName = "ItemToGuidTranslator";

    public override string Name => nameof (ItemToGuidTranslator);

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      if (valuesToTranslate.Length > 1)
        throw new ArgumentException("Too many arguents", nameof (valuesToTranslate));
      if (valuesToTranslate.Length == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      List<string> stringList = new List<string>();
      foreach (object obj in (IEnumerable) valuesToTranslate[0])
      {
        if (obj is IHasParent)
        {
          IHasParent hasParent = (IHasParent) obj;
          stringList.Add(hasParent.Parent.Id.ToString());
        }
      }
      return (object) stringList;
    }
  }
}
