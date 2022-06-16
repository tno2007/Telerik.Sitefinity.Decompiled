// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.IUrlLocalizationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  public interface IUrlLocalizationService
  {
    /// <summary>
    /// Processes the specified URL and modifies if needed. Detects the culture/UI culture from it.
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="culture">Culture</param>
    /// <param name="uiCulture">UI culture</param>
    /// <returns>
    /// Returns the modified URL and cultures that can be taken from it
    /// </returns>
    string UnResolveUrl(string url, out CultureInfo culture, out CultureInfo uiCulture);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    string UnResolveUrlReal(string url);

    /// <summary>
    /// Detects the language from the URL and sets culture and UI culture that are configured to the current thread
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>The modified URL</returns>
    string UnResolveUrlAndApplyDetectedCultures(string url);

    /// <summary>Unresolves the actual URL name from the URL</summary>
    /// <param name="url">URL</param>
    /// <returns>the unresolved URL</returns>
    string UnResolveUrl(string url);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Uses the current UI culture as a target</remarks>
    string ResolveUrl(string url);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <param name="forceResolve">if set to <c>true</c>, the specified URL is always resolved.</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Uses the current UI culture as a target</remarks>
    string ResolveUrl(string url, bool forceResolve);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <param name="targetCulture">Culture for the resolved URL</param>
    /// <returns>Resolved URL for the specified culture</returns>
    string ResolveUrl(string url, CultureInfo targetCulture);

    /// <summary>
    /// Returns the URL for the specified language version of the specified page, using the current url strategy. This method will
    /// work for pages in SPLIT mode - it will return correct URL for the desired language no matter which of the different language
    /// nodes you specify as argument.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="targetCulture">The target culture.</param>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    string ResolvePageUrl(PageNode pageNode, CultureInfo targetCulture);

    /// <summary>
    /// Returns the URL for the specified language version of the specified page, using the current url strategy. This method will
    /// work for pages in SPLIT mode - it will return correct URL for the desired language no matter which of the different language
    /// nodes you specify as argument.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="targetCulture">The target culture.</param>
    /// <param name="useNewImplementation">if set to <c>true</c> the method will use the new implementation.</param>
    /// <returns></returns>
    string ResolvePageUrl(PageNode pageNode, CultureInfo targetCulture, bool useNewImplementation);
  }
}
