// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SynonymFromStringTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.SiteSync
{
  internal class SynonymFromStringTranslator : TranslatorBase
  {
    public const string TranslatorName = "synonymFromStringTranslator";

    public override string Name => "synonymFromStringTranslator";

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      return ((IEnumerable<object>) valuesToTranslate).Any<object>() ? SynonymFromStringTranslator.TranslateXmlToSynonym(valuesToTranslate.Cast<string>().ToList<string>(), typeof (Synonym)) : throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
    }

    public static object TranslateXmlToSynonym(List<string> valuesToTranslate, Type t)
    {
      if (valuesToTranslate.Count == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      List<Synonym> synonym = new List<Synonym>();
      foreach (string s in valuesToTranslate)
      {
        string text = HttpUtility.HtmlDecode(s);
        Synonym instance = (Synonym) Activator.CreateInstance(t);
        XElement xelement = XDocument.Parse(text).Element((XName) "Synonym");
        instance.ApplicationName = xelement.Element((XName) "ApplicationName").Value;
        instance.Culture = int.Parse(xelement.Element((XName) "Culture").Value);
        instance.Id = new Guid(xelement.Element((XName) "Id").Value);
        instance.LastModified = DateTime.Parse(xelement.Element((XName) "LastModified").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        instance.Value = xelement.Element((XName) "Value").Value;
        instance.Weight = int.Parse(xelement.Element((XName) "Weight").Value);
        synonym.Add(instance);
      }
      return (object) synonym;
    }
  }
}
