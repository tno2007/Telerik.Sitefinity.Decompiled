// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.MultisiteUrlLocalizationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Multisite;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Defines a context for initializing an instance of <see cref="T:Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationStrategy" /> interface for a specific site.
  /// </summary>
  public class MultisiteUrlLocalizationContext : UrlLocalizationContext
  {
    private readonly ISite site;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.UrlLocalizationStrategies.MultisiteUrlLocalizationContext" /> class.
    /// </summary>
    /// <param name="site">The site.</param>
    public MultisiteUrlLocalizationContext(ISite site) => this.site = site;

    /// <summary>Gets the site.</summary>
    /// <value>The site.</value>
    public ISite Site => this.site;

    /// <inheritdoc />
    public override IEnumerable<LanguageCultures> GetAllLanguageCultures()
    {
      List<LanguageCultures> languageCultures = new List<LanguageCultures>();
      foreach (CultureElement culture in (IEnumerable<CultureElement>) this.Config.Cultures.Values)
      {
        if (this.site.PublicCultures.ContainsKey(culture.Key))
          languageCultures.Add(new LanguageCultures(culture));
      }
      return (IEnumerable<LanguageCultures>) languageCultures;
    }

    /// <inheritdoc />
    public override ILanguageCultures GetDefaultLanguageCultures()
    {
      CultureElement defaultCulture;
      if (!this.Config.Cultures.TryGetValue(this.site.DefaultCultureKey, out defaultCulture))
        defaultCulture = this.Config.DefaultCulture;
      return (ILanguageCultures) new LanguageCultures(defaultCulture);
    }
  }
}
