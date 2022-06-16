// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.LanguageDataFromStringTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Publishing.Translators;

namespace Telerik.Sitefinity.SiteSync
{
  internal class LanguageDataFromStringTranslator : TranslatorBase
  {
    public const string TranslatorName = "languageDataFromStringTranslator";

    public override string Name => "languageDataFromStringTranslator";

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      if (valuesToTranslate.Length > 1)
        throw new ArgumentException("Too many arguents", nameof (valuesToTranslate));
      if (valuesToTranslate.Length == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      List<LanguageData> languageDataList = new List<LanguageData>();
      foreach (string text in (IEnumerable) valuesToTranslate[0])
      {
        LanguageData instance = (LanguageData) Activator.CreateInstance(typeof (LanguageData));
        XElement xelement = XDocument.Parse(text).Element((XName) "LanguageData");
        instance.ApplicationName = xelement.Element((XName) "ApplicationName").Value;
        instance.Id = new Guid(xelement.Element((XName) "Id").Value);
        if (xelement.Element((XName) "ExpirationDate").Value != string.Empty)
          instance.ExpirationDate = new DateTime?(DateTime.Parse(xelement.Element((XName) "ExpirationDate").Value));
        instance.Language = xelement.Element((XName) "Language").Value;
        instance.LanguageVersion = int.Parse(xelement.Element((XName) "LanguageVersion").Value);
        instance.LastModified = DateTime.Parse(xelement.Element((XName) "LastModified").Value);
        instance.PublicationDate = DateTime.Parse(xelement.Element((XName) "PublicationDate").Value);
        if (xelement.Element((XName) "ScheduledDate").Value != string.Empty)
          instance.ScheduledDate = new DateTime?(DateTime.Parse(xelement.Element((XName) "ScheduledDate").Value));
        instance.ContentState = (LifecycleState) Enum.Parse(typeof (LifecycleState), xelement.Element((XName) "ContentState").Value);
        languageDataList.Add(instance);
      }
      return (object) languageDataList;
    }
  }
}
