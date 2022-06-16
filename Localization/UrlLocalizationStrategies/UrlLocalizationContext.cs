// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.UrlLocalizationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Defines a context for initializing an instance of <see cref="T:Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationStrategy" /> interface.
  /// </summary>
  public class UrlLocalizationContext : IUrlLocalizationContext
  {
    private ResourcesConfig config;
    private IUrlLocalizationStrategySettings strategySettings;

    /// <inheritdoc />
    public virtual ILanguageCultures GetDefaultLanguageCultures()
    {
      CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      return (ILanguageCultures) new LanguageCultures(frontendLanguage, frontendLanguage);
    }

    /// <inheritdoc />
    public virtual IEnumerable<LanguageCultures> GetAllLanguageCultures()
    {
      List<LanguageCultures> languageCultures = new List<LanguageCultures>();
      foreach (CultureElement culture in (IEnumerable<CultureElement>) this.Config.Cultures.Values)
        languageCultures.Add(new LanguageCultures(culture));
      return (IEnumerable<LanguageCultures>) languageCultures;
    }

    /// <inheritdoc />
    public virtual IUrlLocalizationStrategySettings StrategySettings
    {
      get
      {
        if (this.strategySettings == null)
          this.strategySettings = (IUrlLocalizationStrategySettings) this.Config.UrlLocalizationStrategies[this.Config.DefaultUrlLocalizationStrategyKey];
        return this.strategySettings;
      }
    }

    /// <summary>Gets the ResourcesConfig.</summary>
    /// <value>The config.</value>
    protected ResourcesConfig Config
    {
      get
      {
        if (this.config == null)
          this.config = Telerik.Sitefinity.Configuration.Config.Get<ResourcesConfig>();
        return this.config;
      }
    }
  }
}
