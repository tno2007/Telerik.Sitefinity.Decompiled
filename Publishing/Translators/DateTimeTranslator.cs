// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.DateTimeTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>
  /// Translate a list of datetime-s or strings into a list of UTC datetimes
  /// </summary>
  public class DateTimeTranslator : TranslatorBase
  {
    public const string TranslatorName = "datetimetranslator";

    public override string Name => "datetimetranslator";

    /// <summary>
    /// Translate a list of datetime-s or strings into a list of UTC datetimes
    /// </summary>
    /// <param name="settings">Pipe settings</param>
    /// <param name="data">List of strings or datetimtes</param>
    /// <returns>List of datetimes in UTC format</returns>
    public override object Translate(object[] data, IDictionary<string, string> translationSettings)
    {
      if (data.Length == 0)
        return (object) new DateTime[0];
      DateTime[] dateTimeArray = new DateTime[data.Length];
      for (int index = 0; index < data.Length; ++index)
      {
        DateTime dateTime = !(data[index].GetType() == typeof (DateTime)) ? DateTime.Parse((string) data[index]) : (DateTime) data[index];
        switch (dateTime.Kind)
        {
          case DateTimeKind.Unspecified:
            dateTimeArray[index] = dateTime.ToUniversalTime();
            break;
          case DateTimeKind.Utc:
            dateTimeArray[index] = dateTime;
            break;
          case DateTimeKind.Local:
            dateTimeArray[index] = dateTime.ToUniversalTime();
            break;
          default:
            throw new NotImplementedException();
        }
      }
      return (object) dateTimeArray;
    }
  }
}
