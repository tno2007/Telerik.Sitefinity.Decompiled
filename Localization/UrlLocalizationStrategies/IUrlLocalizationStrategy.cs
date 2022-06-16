// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>Base interface for all url strategies</summary>
  public interface IUrlLocalizationStrategy
  {
    /// <summary>
    /// Initializes the strategy. Gets settings from configuration, etc.
    /// </summary>
    /// <param name="settings">The settings from configuration.</param>
    void Initialize(IUrlLocalizationContext context);

    /// <summary>
    /// Unresolves the url and returns the actual url(without language segments). Detects the culture/UI culture from it.
    /// </summary>
    /// <param name="url">Url</param>
    /// <param name="culture">Culture</param>
    /// <param name="uiCulture">UI culture</param>
    /// <returns>Returns the url without the language segments and cultures that can be taken from it</returns>
    string UnResolveUrl(string url, out CultureInfo culture, out CultureInfo uiCulture);

    /// <summary>
    /// Resolves the url to such that has language information in it
    /// </summary>
    /// <param name="url">Url to be processed</param>
    /// <returns>The url with the language info in it</returns>
    /// <remarks>Useless the current UI culture as a target</remarks>
    string ResolveUrl(string url);

    /// <summary>
    /// Resolves the url to such that has language information in it
    /// </summary>
    /// <param name="url">Url to be processed</param>
    /// <param name="targetCulture">Culture for the resolved url</param>
    /// <returns>Resolved url for the specified culture</returns>
    string ResolveUrl(string url, CultureInfo targetCulture);
  }
}
