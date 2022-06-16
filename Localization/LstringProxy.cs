// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LstringProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  internal class LstringProxy : ILstring
  {
    private string monoVal;
    private Dictionary<CultureInfo, string> values;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.LstringProxy" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public LstringProxy(string value) => this.monoVal = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.LstringProxy" /> class.
    /// </summary>
    /// <param name="values">The values.</param>
    public LstringProxy(IDictionary<CultureInfo, string> values) => this.values = new Dictionary<CultureInfo, string>(values);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.LstringProxy" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    public LstringProxy(Lstring source)
      : this(source, source.IsMultilingual())
    {
    }

    public LstringProxy(Lstring source, bool isMultilingual)
    {
      if (!isMultilingual)
      {
        this.monoVal = (string) source;
      }
      else
      {
        CultureInfo[] availableLanguages = source.GetAvailableLanguages();
        this.values = new Dictionary<CultureInfo, string>(availableLanguages.Length);
        foreach (CultureInfo cultureInfo in availableLanguages)
        {
          string str;
          source.TryGetValue(out str, cultureInfo);
          this.values.Add(cultureInfo, str);
        }
      }
    }

    /// <summary>Tries the get value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The value for the culture.</returns>
    public virtual bool TryGetValue(out string value, CultureInfo culture = null)
    {
      if (this.values == null)
      {
        value = this.monoVal;
        return true;
      }
      bool flag = false;
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      else if (culture == CultureInfo.InvariantCulture && !AppSettings.CurrentSettings.LegacyMultilingual)
      {
        culture = AppSettings.CurrentSettings.DefaultFrontendLanguage;
        flag = true;
      }
      if (this.values.TryGetValue(culture, out value))
        return true;
      if (!flag || !this.values.Any<KeyValuePair<CultureInfo, string>>())
        return false;
      value = this.values.Values.First<string>();
      return true;
    }

    /// <summary>Gets the available languages.</summary>
    /// <returns>The available languages</returns>
    public CultureInfo[] GetAvailableLanguages()
    {
      if (this.values != null)
        return this.values.Keys.ToArray<CultureInfo>();
      return new CultureInfo[1]
      {
        CultureInfo.InvariantCulture
      };
    }
  }
}
