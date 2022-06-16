// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.PageNodeCacheItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Data
{
  public class PageNodeCacheItem
  {
    /// <summary>Gets the full path key.</summary>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">if set to <c>true</c> [fallback to any language].</param>
    public static string GetFullPathKey(
      CultureInfo culture,
      bool fallbackToAnyLanguage,
      bool resolveUrl)
    {
      return string.Format("cul_{0}_{1}_{2}", (object) culture, (object) fallbackToAnyLanguage, (object) resolveUrl);
    }

    /// <summary>Gets the default key.</summary>
    public static string GetDefaultKey() => PageNodeCacheItem.GetFullPathKey(string.Empty, false);

    /// <summary>Gets the full path key.</summary>
    /// <param name="separator">The separator.</param>
    /// <param name="fallbackToAnyLanguage">if set to <c>true</c> [fallback to any language].</param>
    public static string GetFullPathKey(string separator, bool fallbackToAnyLanguage) => string.Format("{0}_{1}", (object) separator, (object) fallbackToAnyLanguage);

    /// <summary>
    /// Gets or sets the dictionary which contain the full paths of the page which depend on culture, separator and fallback option.
    /// </summary>
    public Dictionary<string, string> FullPathDictionary { get; set; }

    /// <summary>Gets or sets the name of the URL.</summary>
    public string UrlName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show in navigation].
    /// </summary>
    public bool ShowInNavigation { get; set; }

    /// <summary>Gets or sets the type of the node.</summary>
    public NodeType NodeType { get; set; }
  }
}
