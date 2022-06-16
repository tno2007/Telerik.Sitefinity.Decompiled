// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.LanguageDataTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.SiteSync
{
  internal class LanguageDataTranslator : TranslatorBase
  {
    public const string TranslatorName = "languageDataTranslator";
    private static readonly string urlDataFormat = "\r\n                <LanguageData>\r\n                    <ApplicationName>{0}</ApplicationName>\r\n                    <Id>{1}</Id>\r\n                    <ExpirationDate>{2}</ExpirationDate>\r\n                    <Language>{3}</Language>\r\n                    <LanguageVersion>{4}</LanguageVersion>\r\n                    <LastModified>{5}</LastModified>\r\n                    <PublicationDate>{6}</PublicationDate>\r\n                    <ScheduledDate>{7}</ScheduledDate>\r\n                    <ContentState>{8}</ContentState>\r\n                </LanguageData>";

    public override string Name => "languageDataTranslator";

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
        if (obj is LanguageData languageData)
        {
          CultureInfo cultureByName = AppSettings.CurrentSettings.GetCultureByName(languageData.Language);
          if (cultureByName.Equals((object) Thread.CurrentThread.CurrentCulture) || cultureByName.Equals((object) SystemManager.CurrentContext.Culture))
          {
            string str = this.ConvertToString(languageData);
            stringList.Add(str);
          }
        }
      }
      return (object) stringList;
    }

    protected string ConvertToString(LanguageData item)
    {
      string urlDataFormat = LanguageDataTranslator.urlDataFormat;
      object[] objArray = new object[9];
      objArray[0] = (object) item.ApplicationName;
      objArray[1] = (object) item.Id.ToString();
      DateTime dateTime;
      string empty1;
      if (!item.ExpirationDate.HasValue)
      {
        empty1 = string.Empty;
      }
      else
      {
        dateTime = item.ExpirationDate.Value.ToUniversalTime();
        empty1 = dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      objArray[2] = (object) empty1;
      objArray[3] = (object) item.Language;
      objArray[4] = (object) item.LanguageVersion;
      dateTime = item.LastModified;
      dateTime = dateTime.ToUniversalTime();
      objArray[5] = (object) dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
      dateTime = item.PublicationDate;
      dateTime = dateTime.ToUniversalTime();
      objArray[6] = (object) dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
      string empty2;
      if (!item.ScheduledDate.HasValue)
      {
        empty2 = string.Empty;
      }
      else
      {
        dateTime = item.ScheduledDate.Value;
        dateTime = dateTime.ToUniversalTime();
        empty2 = dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      objArray[7] = (object) empty2;
      objArray[8] = (object) (int) item.ContentState;
      return string.Format(urlDataFormat, objArray);
    }

    public static object TranslateXmlToLanguageData(List<string> valuesToTranslate, object args)
    {
      if (valuesToTranslate.Count == 0)
        throw new ArgumentException("No values to translate", nameof (valuesToTranslate));
      List<LanguageData> languageData1 = new List<LanguageData>();
      foreach (string s in valuesToTranslate)
      {
        // ISSUE: reference to a compiler-generated field
        if (LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, ILanguageDataManager>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (ILanguageDataManager), typeof (LanguageDataTranslator)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, ILanguageDataManager> target1 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, ILanguageDataManager>> p1 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (LanguageDataTranslator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__0, args);
        ILanguageDataManager languageDataManager = target1((CallSite) p1, obj1);
        XElement xelement = XDocument.Parse(HttpUtility.HtmlDecode(s)).Element((XName) "LanguageData");
        string language = xelement.Element((XName) "Language").Value;
        if (language == string.Empty)
          language = (string) null;
        // ISSUE: reference to a compiler-generated field
        if (LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, ILifecycleDataItem>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (ILifecycleDataItem), typeof (LanguageDataTranslator)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, ILifecycleDataItem> target2 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, ILifecycleDataItem>> p3 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Item", typeof (LanguageDataTranslator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) LanguageDataTranslator.\u003C\u003Eo__4.\u003C\u003Ep__2, args);
        IList<LanguageData> languageData2 = target2((CallSite) p3, obj2).LanguageData;
        if (!languageData2.Any<LanguageData>((Func<LanguageData, bool>) (l => l.Language == language)))
        {
          LanguageData languageData3 = languageDataManager.CreateLanguageData();
          if (language != null)
            languageData3.Language = language;
          languageData3.ApplicationName = xelement.Element((XName) "ApplicationName").Value;
          if (!string.IsNullOrEmpty(xelement.Element((XName) "ExpirationDate").Value))
            languageData3.ExpirationDate = new DateTime?(DateTime.Parse(xelement.Element((XName) "ExpirationDate").Value, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime());
          languageData3.LanguageVersion = int.Parse(xelement.Element((XName) "LanguageVersion").Value);
          if (!string.IsNullOrEmpty(xelement.Element((XName) "LastModified").Value))
            languageData3.LastModified = DateTime.Parse(xelement.Element((XName) "LastModified").Value, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime();
          if (!string.IsNullOrEmpty(xelement.Element((XName) "PublicationDate").Value))
            languageData3.PublicationDate = DateTime.Parse(xelement.Element((XName) "PublicationDate").Value, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime();
          if (!string.IsNullOrEmpty(xelement.Element((XName) "ScheduledDate").Value))
            languageData3.ScheduledDate = new DateTime?(DateTime.Parse(xelement.Element((XName) "ScheduledDate").Value, (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime());
          languageData3.ContentState = (LifecycleState) Enum.Parse(typeof (LifecycleState), xelement.Element((XName) "ContentState").Value);
          languageData2.Add(languageData3);
          languageData1.Add(languageData3);
        }
      }
      return (object) languageData1;
    }
  }
}
