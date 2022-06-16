// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.LstringSingleViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public class LstringSingleViewModel
  {
    private IDictionary<string, string> valuesPerCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.LstringSingleViewModel" /> class.
    /// </summary>
    public LstringSingleViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.LstringSingleViewModel" /> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public LstringSingleViewModel(Lstring model)
      : this(model, (IEnumerable<CultureInfo>) model.GetAvailableLanguages())
    {
    }

    public LstringSingleViewModel(Lstring model, IEnumerable<CultureInfo> cultures)
    {
      this.Value = model.Value;
      this.PersistedValue = model.PersistedValue;
      if (string.IsNullOrEmpty(model.PersistedValue))
        this.PersistedValue = this.Value;
      foreach (CultureInfo culture in cultures)
      {
        if (model[culture] != null)
          this.ValuesPerCulture.Add(culture.Name, model[culture]);
      }
    }

    internal LstringSingleViewModel(ILstring model)
    {
      string str;
      model.TryGetValue(out str, SystemManager.CurrentContext.Culture);
      if (str.IsNullOrEmpty() && SystemManager.RequestLanguageFallbackMode != FallbackMode.NoFallback)
        model.TryGetValue(out str, CultureInfo.InvariantCulture);
      this.Value = str;
      this.PersistedValue = str;
      foreach (CultureInfo availableLanguage in model.GetAvailableLanguages())
      {
        if (model.TryGetValue(out str, availableLanguage))
          this.ValuesPerCulture.Add(availableLanguage.Name, str);
      }
    }

    /// <summary>Transfers to lstring.</summary>
    /// <param name="model">The model.</param>
    public void TransferToLstring(Lstring model)
    {
      foreach (string key in (IEnumerable<string>) this.ValuesPerCulture.Keys)
        model[key] = this.ValuesPerCulture[key];
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [DataMember]
    public string Value { get; set; }

    /// <summary>Gets or sets the persisted value.</summary>
    /// <value>The persisted value.</value>
    [DataMember]
    public string PersistedValue { get; set; }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => this.Value;

    /// <summary>Gets or sets the values per culture.</summary>
    /// <value>The values per culture.</value>
    [DataMember]
    public IDictionary<string, string> ValuesPerCulture
    {
      get
      {
        if (this.valuesPerCulture == null)
          this.valuesPerCulture = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        return this.valuesPerCulture;
      }
      set => this.valuesPerCulture = value;
    }
  }
}
