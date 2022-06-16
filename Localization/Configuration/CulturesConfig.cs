// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.CulturesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Globalization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Defines configuration settings for predefined cultures in Sitefinity. They are used by the localization configuration.
  /// </summary>
  public class CulturesConfig : ConfigSection
  {
    /// <summary>Gets the predefined neutral cultures.</summary>
    /// <value>The predefined languages.</value>
    [ConfigurationProperty("predefinedLanguages")]
    public virtual ConfigElementList<CultureElement> PredefinedLanguages => (ConfigElementList<CultureElement>) this["predefinedLanguages"];

    /// <summary>Gets the predefined specific cultures.</summary>
    /// <value>The predefined cultures.</value>
    [ConfigurationProperty("predefinedCultures")]
    public virtual ConfigElementList<CultureElement> PredefinedCultures => (ConfigElementList<CultureElement>) this["predefinedCultures"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      if (this.PredefinedLanguages.Count == 0)
        this.InitializeLanguages();
      if (this.PredefinedCultures.Count != 0)
        return;
      this.InitializeCultures();
    }

    private void InitializeLanguages()
    {
      foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
      {
        if (culture.LCID != (int) sbyte.MaxValue && string.Compare(culture.Name, "zh-Hans", true) != 0 && string.Compare(culture.Name, "zh-Hant", true) != 0)
        {
          CultureElement element = new CultureElement(CulturesConfig.GenerateCultureKey(culture, culture), culture.Name, culture.Name);
          if (string.Equals(culture.Name, "id"))
            element.FieldSuffix = "idn";
          this.PredefinedLanguages.Add(element);
        }
      }
    }

    private void InitializeCultures()
    {
      foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        this.PredefinedCultures.Add(new CultureElement(CulturesConfig.GenerateCultureKey(culture, culture), culture.Name, culture.Name));
    }

    /// <summary>
    /// Generates a string that can be used for a culture key.
    /// </summary>
    /// <param name="neutralCulture">The neutral culture, which English name will be used.</param>
    /// <param name="specificCulture">The specific culture, which name will be used.</param>
    /// <returns>Returns a string in the format {NeutralEnglishName}-{SpecificName}</returns>
    public static string GenerateCultureKey(CultureInfo neutralCulture, CultureInfo specificCulture) => string.Format("{0}-{1}", (object) neutralCulture.EnglishName, (object) specificCulture.Name).ToLowerInvariant();
  }
}
