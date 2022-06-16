// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.UrlLocalizationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Service that initializes and wraps the default URL localization strategy
  /// Used as a singleton by the object factory
  /// </summary>
  public class UrlLocalizationService : IUrlLocalizationService
  {
    private readonly ConcurrentProperty<IUrlLocalizationStrategy> currentUrlLocalizationStrategy;
    private IUrlLocalizationStrategy noneUrlLocalizationStrategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.UrlLocalizationStrategies.UrlLocalizationService" /> class.
    /// </summary>
    public UrlLocalizationService() => this.currentUrlLocalizationStrategy = new ConcurrentProperty<IUrlLocalizationStrategy>(new Func<IUrlLocalizationStrategy>(this.LoadCurrentUrlLocalizationStrategy));

    /// <summary>Gets the current URL localization strategy.</summary>
    public virtual IUrlLocalizationStrategy CurrentUrlLocalizationStrategy => this.currentUrlLocalizationStrategy.Value;

    /// <summary>
    /// Processes the specified URL and modifies if needed. Detects the culture/UI culture from it.
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="culture">Culture</param>
    /// <param name="uiCulture">UI culture</param>
    /// <returns>
    /// Returns the modified URL and cultures that can be taken from it
    /// </returns>
    public string UnResolveUrl(string url, out CultureInfo culture, out CultureInfo uiCulture) => this.CurrentUrlLocalizationStrategy.UnResolveUrl(url, out culture, out uiCulture);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    public string UnResolveUrlReal(string url) => this.CurrentUrlLocalizationStrategy.UnResolveUrl(url, out CultureInfo _, out CultureInfo _);

    /// <summary>
    /// Detects the language from the URL and sets culture and UI culture that are configured to the current thread
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>The modified URL</returns>
    public string UnResolveUrlAndApplyDetectedCultures(string url)
    {
      CultureInfo uiCulture;
      url = this.UnResolveUrl(url, out CultureInfo _, out uiCulture);
      SystemManager.CurrentContext.Culture = uiCulture;
      return url;
    }

    /// <summary>Unresolves the actual URL name from the URL</summary>
    /// <param name="url">URL</param>
    /// <returns>the unresolved URL</returns>
    public string UnResolveUrl(string url) => this.UnResolveUrl(url, out CultureInfo _, out CultureInfo _);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Uses the current UI culture as a target</remarks>
    public string ResolveUrl(string url) => this.ResolveUrl(url, false);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <param name="forceResolve">if set to <c>true</c>, the specified URL is always resolved.</param>
    /// <returns>The URL with the language info in it</returns>
    /// <remarks>Uses the current UI culture as a target</remarks>
    public string ResolveUrl(string url, bool forceResolve) => this.CurrentUrlLocalizationStrategy.ResolveUrl(url, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Resolves the URL to such that has language information in it
    /// </summary>
    /// <param name="url">URL to be processed</param>
    /// <param name="targetCulture">Culture for the resolved URL</param>
    /// <returns>Resolved URL for the specified culture</returns>
    public string ResolveUrl(string url, CultureInfo targetCulture) => this.CurrentUrlLocalizationStrategy.ResolveUrl(url, targetCulture);

    /// <summary>
    /// Returns the URL for the specified language version of the specified page, using the current url strategy. This method will
    /// work for pages in SPLIT mode - it will return correct URL for the desired language no matter which of the different language
    /// nodes you specify as argument.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="targetCulture">The target culture.</param>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    [Obsolete("Use ResolveUrl method")]
    public string ResolvePageUrl(PageNode pageNode, CultureInfo targetCulture) => this.ResolvePageUrl(pageNode, targetCulture, false);

    /// <summary>
    /// Returns the URL for the specified language version of the specified page, using the current url strategy. This method will
    /// work for pages in SPLIT mode - it will return correct URL for the desired language no matter which of the different language
    /// nodes you specify as argument.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="targetCulture">The target culture.</param>
    /// <param name="useNewImplementation">if set to <c>true</c> the method will use the new implementation.</param>
    /// <returns></returns>
    [Obsolete("Use ResolveUrl method")]
    public string ResolvePageUrl(
      PageNode pageNode,
      CultureInfo targetCulture,
      bool useNewImplementation)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      SystemManager.CurrentContext.Culture = targetCulture;
      try
      {
        return this.ResolveUrl(!useNewImplementation ? pageNode.GetFullUrl() : pageNode.GetFullUrl(targetCulture, false, false), targetCulture);
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
      }
    }

    protected IUrlLocalizationStrategy GetNoneUrlLocalizationStrategy()
    {
      if (this.noneUrlLocalizationStrategy == null)
        this.noneUrlLocalizationStrategy = (IUrlLocalizationStrategy) new NoneUrlLocalizationStrategy();
      return this.noneUrlLocalizationStrategy;
    }

    private IUrlLocalizationStrategy LoadCurrentUrlLocalizationStrategy()
    {
      IUrlLocalizationStrategy localizationStrategy = (IUrlLocalizationStrategy) null;
      if (LicenseState.Current.LicenseInfo.LocalizationFeaturesLevel >= 100)
      {
        UrlLocalizationContext context = new UrlLocalizationContext();
        IUrlLocalizationStrategySettings strategySettings = context.StrategySettings;
        if (strategySettings != null)
        {
          localizationStrategy = (IUrlLocalizationStrategy) Activator.CreateInstance(strategySettings.UrlLocalizationStrategyType);
          localizationStrategy.Initialize((IUrlLocalizationContext) context);
        }
      }
      if (localizationStrategy == null)
        localizationStrategy = this.GetNoneUrlLocalizationStrategy();
      return localizationStrategy;
    }
  }
}
