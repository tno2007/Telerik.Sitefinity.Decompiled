// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitemapNodeFilterExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Extension methods for <c>ISitemapNodeFilter</c>
  /// </summary>
  public static class SitemapNodeFilterExtensions
  {
    /// <summary>
    /// Reads configuration and returns if the filter was turned off in config. If the filter is not configured, returns true (default)
    /// </summary>
    /// <param name="filter">Sitemap node filter to extend</param>
    /// <param name="filterName">Name of the filter element, as entered in PagesConfig.SitemapNodeFilters</param>
    /// <returns>True if the filter is enabled in config, or false it it is disabled. If <paramref name="filerName" />
    /// is not in the config, the method returns true</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="filterName" /> is null or empty.</exception>
    public static bool IsFilterEnabled(this ISitemapNodeFilter filter, string filterName)
    {
      if (string.IsNullOrEmpty(filterName))
        throw new ArgumentNullException(nameof (filterName));
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      if (!pagesConfig.IsSitemapNodeFilteringEnabled)
        return false;
      SitemapNodeFilterElement nodeFilterElement;
      return !pagesConfig.SitemapNodeFilters.TryGetValue(filterName, out nodeFilterElement) || nodeFilterElement.IsEnabled;
    }
  }
}
