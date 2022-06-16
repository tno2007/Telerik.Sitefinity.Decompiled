// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SynonymTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.SiteSync
{
  internal class SynonymTranslator : TranslatorBase
  {
    public const string TranslatorName = "synonymTranslatorTranslator";
    private static readonly string urlDataFormat = "\r\n                <Synonym>\r\n                    <ApplicationName>{0}</ApplicationName>\r\n                    <Culture>{1}</Culture>\r\n                    <Id>{2}</Id>\r\n                    <LastModified>{3}</LastModified>\r\n                    <Value>{4}</Value>\r\n                    <Weight>{5}</Weight>\r\n                </Synonym>";

    public override string Name => "synonymTranslatorTranslator";

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      if (valuesToTranslate.Length > 1)
        throw new ArgumentException("Too many arguments", nameof (valuesToTranslate));
      if (valuesToTranslate.Length == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      List<string> stringList = new List<string>();
      foreach (object obj in (IEnumerable) valuesToTranslate[0])
      {
        if (obj is Synonym)
        {
          Synonym synonym = (Synonym) obj;
          string urlDataFormat = SynonymTranslator.urlDataFormat;
          object[] objArray = new object[6];
          objArray[0] = synonym.ApplicationName == null ? (object) string.Empty : (object) synonym.ApplicationName;
          int culture = synonym.Culture;
          objArray[1] = (object) synonym.Culture.ToString();
          objArray[2] = (object) synonym.Id.ToString();
          objArray[3] = (object) synonym.LastModified.ToUniversalTime().ToString((IFormatProvider) CultureInfo.InvariantCulture);
          objArray[4] = synonym.Value == null ? (object) string.Empty : (object) synonym.Value;
          objArray[5] = (object) synonym.Weight.ToString();
          string str = string.Format(urlDataFormat, objArray);
          stringList.Add(str);
        }
      }
      return (object) stringList;
    }
  }
}
