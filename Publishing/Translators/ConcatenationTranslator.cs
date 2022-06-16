// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.ConcatenationTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Translates an array of objects into a single string</summary>
  public class ConcatenationTranslator : TranslatorBase
  {
    public const string TranslatorName = "concatenationtranslator";

    public override string Name => "concatenationtranslator";

    /// <summary>
    /// Concats serveral objects of any type into a single string
    /// </summary>
    /// <param name="settings">Pipe settings</param>
    /// <param name="data">List of objects</param>
    /// <returns>String</returns>
    public override object Translate(object[] data, IDictionary<string, string> translationSettings)
    {
      if (data.Length == 0)
        return (object) string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < data.Length; ++index)
      {
        string str = ConcatenationTranslator.GetString(data[index]);
        stringBuilder.Append(str);
        if (index + 1 < data.Length)
          stringBuilder.Append(' ');
      }
      return (object) stringBuilder.ToString();
    }

    /// <summary>
    /// Converts an object into string, favoring TypeConverter to ToString
    /// </summary>
    /// <param name="obj">Object to convert.</param>
    /// <returns>Obj converted to string or an empty string.</returns>
    public static string GetString(object obj)
    {
      if (obj == null)
        return string.Empty;
      if (obj is string)
        return (string) obj;
      TypeConverter converter = TypeDescriptor.GetConverter(obj);
      return converter != null && converter.CanConvertTo(typeof (string)) ? converter.ConvertToString(obj) : obj.ToString();
    }
  }
}
