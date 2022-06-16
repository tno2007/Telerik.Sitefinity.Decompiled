// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Localization.Configuration;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Defines a context for initializing an instance of <see cref="T:Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationStrategy" /> interface.
  /// </summary>
  public interface IUrlLocalizationContext
  {
    /// <summary>Gets or sets the default cultures.</summary>
    /// <value>The default cultures.</value>
    ILanguageCultures GetDefaultLanguageCultures();

    /// <summary>
    /// Gets or sets the language cultures for the current url localization strategy.
    /// </summary>
    /// <value>The language cultures.</value>
    IEnumerable<LanguageCultures> GetAllLanguageCultures();

    /// <summary>Gets the strategy settings.</summary>
    /// <returns></returns>
    IUrlLocalizationStrategySettings StrategySettings { get; }
  }
}
