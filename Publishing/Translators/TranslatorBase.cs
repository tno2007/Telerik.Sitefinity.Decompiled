// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.TranslatorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing.Translators
{
  public abstract class TranslatorBase : ITranslator
  {
    public abstract string Name { get; }

    /// <summary>Transaltes the specified list of values.</summary>
    /// <param name="valuesToTranslate">The values to translate.</param>
    /// <param name="translationSettings">The translation settings.</param>
    /// <returns></returns>
    public abstract object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings);

    /// <summary>
    /// Convert a value to string, favouring TypeConverter over ToString
    /// </summary>
    /// <param name="value">Value that has to be converted. Will be empty string if </param>
    /// <returns>Value, converted</returns>
    protected virtual string ConvertTostring(object value)
    {
      if (value == null)
        return string.Empty;
      TypeConverter converter = TypeDescriptor.GetConverter(value);
      return converter != null && converter.CanConvertTo(typeof (string)) ? converter.ConvertToString(value) : value.ToString();
    }

    public virtual Dictionary<string, string> GetDefaultSettings() => new Dictionary<string, string>();
  }
}
