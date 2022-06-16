// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.HtmlFilterProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.DynamicModules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Provider class for HTML filters</summary>
  public class HtmlFilterProvider
  {
    private static IDictionary<string, IHtmlFilter> contentFilters = (IDictionary<string, IHtmlFilter>) new Dictionary<string, IHtmlFilter>()
    {
      {
        "LinksParser",
        (IHtmlFilter) new DynamicLinksParser()
      },
      {
        "DraftLinksParser",
        (IHtmlFilter) new DynamicLinksParser(true)
      }
    };

    /// <summary>Applies filters to provided HTML</summary>
    /// <param name="html">The HTML string.</param>
    /// <returns>The result HTML</returns>
    public static string ApplyFilters(string html)
    {
      if (SystemManager.CurrentHttpContext == null)
        return string.Empty;
      string[] strArray = (string[]) SystemManager.CurrentHttpContext.Items[(object) "sfContentFilters"];
      if (strArray != null)
      {
        foreach (string key in strArray)
        {
          IHtmlFilter htmlFilter;
          if (HtmlFilterProvider.contentFilters.TryGetValue(key, out htmlFilter))
            html = htmlFilter.Apply(html);
        }
      }
      if (!Config.Get<SecurityConfig>().DisableHtmlSanitization)
        html = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(html);
      return html;
    }
  }
}
