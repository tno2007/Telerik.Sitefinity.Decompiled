// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.TranslatedUrlsLocalizationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Class that provides the functionality to store/load the pages when the urls are translated for the specific language
  /// </summary>
  public class TranslatedUrlsLocalizationStrategy : IUrlLocalizationStrategy
  {
    /// <summary>
    /// Initializes the strategy. Gets settings from configuration, etc.
    /// </summary>
    public void Initialize(IUrlLocalizationContext context)
    {
    }

    /// <summary>
    /// Processes the specified URL and modifies if needed. Detects the culture/UIculture from it.
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="culture">Culture</param>
    /// <param name="uiCulture">UI culture</param>
    /// <returns>
    /// Returns the modified URL and cultures that can be taken from it
    /// </returns>
    public string UnResolveUrl(string url, out CultureInfo culture, out CultureInfo uiCulture) => throw new NotImplementedException();

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Useses the current UI culture as a target</remarks>
    public string ResolveUrl(string url) => url;

    /// <summary>
    /// Resolves the url to such that has language information in it
    /// </summary>
    /// <param name="url">Url to be processed</param>
    /// <param name="targetCulture">Culture for the resolved url</param>
    /// <returns>Resolved url for the specified culture</returns>
    public string ResolveUrl(string url, CultureInfo targetCulture) => url;

    public string BuildLanguageSetting(ILanguageCultures language) => throw new NotImplementedException();
  }
}
