// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.RelocationScriptsCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class RelocationScriptsCache
  {
    private readonly ResponsiveDesignCache mainCache;
    private readonly Dictionary<string, string> relocationScripts = new Dictionary<string, string>();
    private readonly object relocationScriptsSync = new object();

    private RelocationScriptsCache(ResponsiveDesignCache mainCache) => this.mainCache = mainCache;

    internal static RelocationScriptsCache GetInstance()
    {
      ResponsiveDesignCache mainCache = ResponsiveDesignCache.GetInstance();
      return mainCache.GetOrAddSubCache("RelocationScripts", (Func<object>) (() => (object) new RelocationScriptsCache(mainCache))) as RelocationScriptsCache;
    }

    internal void GetRelocationScripts(
      PageDataProxy pageData,
      CultureInfo culture,
      TextWriter output)
    {
      this.GenerateRelocationScripts(this.mainCache.GetMediaQueries(pageData, ResponsiveDesignBehavior.MiniSite), culture, output);
    }

    private static string GetRelocationScriptsKey(Guid mediaQueryId, CultureInfo culture) => mediaQueryId.ToString() + "/" + culture.Name;

    private static string GenerateRelocationScript(IMediaQuery mediaQuery, CultureInfo culture)
    {
      StringBuilder scriptGenerator = new StringBuilder();
      foreach (IMediaQueryRule mediaQueryRule in mediaQuery.MediaQueryRules)
      {
        if (RelocationScriptsCache.GenerateCondition(scriptGenerator, mediaQueryRule))
        {
          string lower = RelocationScriptsCache.GetRedirectUrl(mediaQuery.MiniSitePageId, culture).ToLower();
          scriptGenerator.Append(" { ");
          scriptGenerator.AppendLine();
          scriptGenerator.Append("if(!(decodeURI(window.location.pathname.toString().toLowerCase()).indexOf('");
          scriptGenerator.Append(lower);
          scriptGenerator.Append("') == 0)) {");
          scriptGenerator.AppendLine();
          scriptGenerator.Append("window.location = '");
          scriptGenerator.Append(lower);
          scriptGenerator.Append("';");
          scriptGenerator.AppendLine();
          scriptGenerator.Append("}");
          scriptGenerator.AppendLine();
          scriptGenerator.Append(" } ");
          scriptGenerator.AppendLine();
        }
      }
      return scriptGenerator.ToString();
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    private static bool GenerateCondition(
      StringBuilder scriptGenerator,
      IMediaQueryRule mediaQueryRule)
    {
      int length = scriptGenerator.Length;
      CSSMediaQuery cssMediaQuery = MediaQueryParser.Parse(mediaQueryRule.ResultingRule);
      int num = 0;
      scriptGenerator.Append("if(");
      if (cssMediaQuery.Width.HasValue)
      {
        scriptGenerator.Append("(screen.width == ");
        scriptGenerator.Append((object) cssMediaQuery.Width);
        scriptGenerator.Append(") && ");
        ++num;
      }
      int? nullable = cssMediaQuery.MinWidth;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(screen.width > ");
        scriptGenerator.Append((object) cssMediaQuery.MinWidth);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MaxWidth;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(screen.width < ");
        scriptGenerator.Append((object) cssMediaQuery.MaxWidth);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.Height;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(screen.height == ");
        scriptGenerator.Append((object) cssMediaQuery.Height);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MinHeight;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(screen.height > ");
        scriptGenerator.Append((object) cssMediaQuery.MinHeight);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MaxHeight;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(screen.height < ");
        scriptGenerator.Append((object) cssMediaQuery.MaxHeight);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.DeviceWidth;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceWidth == ");
        scriptGenerator.Append((object) cssMediaQuery.DeviceWidth);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MinDeviceWidth;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceWidth > ");
        scriptGenerator.Append((object) cssMediaQuery.MinDeviceWidth);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MaxDeviceWidth;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceWidth < ");
        scriptGenerator.Append((object) cssMediaQuery.MaxDeviceWidth);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.DeviceHeight;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceHeight == ");
        scriptGenerator.Append((object) cssMediaQuery.DeviceHeight);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MinDeviceHeight;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceHeight > ");
        scriptGenerator.Append((object) cssMediaQuery.MinDeviceHeight);
        scriptGenerator.Append(") && ");
        ++num;
      }
      nullable = cssMediaQuery.MaxDeviceHeight;
      if (nullable.HasValue)
      {
        scriptGenerator.Append("(_rdDeviceHeight < ");
        scriptGenerator.Append((object) cssMediaQuery.MaxDeviceHeight);
        scriptGenerator.Append(") && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.Orientation))
      {
        scriptGenerator.Append("(_rdOrientation == ");
        scriptGenerator.Append(cssMediaQuery.Orientation);
        scriptGenerator.Append(") && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.AspectRatio))
      {
        scriptGenerator.Append("((screen.width / screen.height) == (");
        scriptGenerator.Append(cssMediaQuery.AspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.MinAspectRatio))
      {
        scriptGenerator.Append("((screen.width / screen.height) > (");
        scriptGenerator.Append(cssMediaQuery.MinAspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.MaxAspectRatio))
      {
        scriptGenerator.Append("((screen.width / screen.height) < (");
        scriptGenerator.Append(cssMediaQuery.MaxAspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.DeviceAspectRatio))
      {
        scriptGenerator.Append("((_rdDeviceWidth / _rdDeviceHeight) == (");
        scriptGenerator.Append(cssMediaQuery.DeviceAspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.MinDeviceAspectRatio))
      {
        scriptGenerator.Append("((_rdDeviceWidth / _rdDeviceHeight) > (");
        scriptGenerator.Append(cssMediaQuery.MinDeviceAspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (!string.IsNullOrEmpty(cssMediaQuery.MaxDeviceAspectRatio))
      {
        scriptGenerator.Append("((_rdDeviceWidth / _rdDeviceHeight) < (");
        scriptGenerator.Append(cssMediaQuery.MaxDeviceAspectRatio);
        scriptGenerator.Append(")) && ");
        ++num;
      }
      if (num == 0)
      {
        scriptGenerator.Remove(scriptGenerator.Length - 3, 3);
        return false;
      }
      scriptGenerator.Remove(scriptGenerator.Length - 4, 4);
      if (num == 1)
      {
        scriptGenerator.Remove(length + 2, 1);
        scriptGenerator.Remove(scriptGenerator.Length - 1, 1);
      }
      scriptGenerator.Append(")");
      return true;
    }

    private static string GetRedirectUrl(Guid redirectPageId, CultureInfo culture)
    {
      using (new CultureRegion(culture))
      {
        SiteMapNode siteMapNodeFromKey = ((SiteMapBase) SitefinitySiteMap.GetCurrentProvider()).FindSiteMapNodeFromKey(redirectPageId.ToString(), false);
        return siteMapNodeFromKey != null ? RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) : string.Empty;
      }
    }

    /// <summary>
    /// Generates a client script for rules that redirect to page.
    /// </summary>
    /// <param name="mediaQueries">MediaQuery items for which relocation scripts need to be generated.</param>
    /// <param name="culture">Redirects to pages with the specified culture.</param>
    /// <param name="output">Where scripts will be written to.</param>
    private void GenerateRelocationScripts(
      IEnumerable<IMediaQuery> mediaQueries,
      CultureInfo culture,
      TextWriter output)
    {
      output.Write("(function() {");
      output.Write("var _rdDeviceWidth = (window.innerWidth > 0) ? window.innerWidth : screen.width;");
      output.Write("var _rdDeviceHeight = (window.innerHeight > 0) ? window.innerHeight : screen.height;");
      output.Write("var _rdOrientation = (window.width > window.height) ? 'landscape' : 'portrait';");
      foreach (IMediaQuery mediaQuery in mediaQueries)
        output.Write(this.GetRelocationScript(mediaQuery, culture));
      output.Write("})();");
    }

    private string GetRelocationScript(IMediaQuery mediaQuery, CultureInfo culture)
    {
      string relocationScriptsKey = RelocationScriptsCache.GetRelocationScriptsKey(mediaQuery.Id, culture);
      string relocationScript = (string) null;
      if (!this.relocationScripts.TryGetValue(relocationScriptsKey, out relocationScript))
      {
        lock (this.relocationScriptsSync)
        {
          if (!this.relocationScripts.TryGetValue(relocationScriptsKey, out relocationScript))
          {
            relocationScript = RelocationScriptsCache.GenerateRelocationScript(mediaQuery, culture);
            this.relocationScripts.Add(relocationScriptsKey, relocationScript);
          }
        }
      }
      return relocationScript;
    }
  }
}
