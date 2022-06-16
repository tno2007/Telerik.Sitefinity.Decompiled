// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ThemeController
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Xml;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Rendering;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  public static class ThemeController
  {
    public const string ThemeKey = "theme";
    public const string NoThemeName = "notheme";
    private static string GlobalResourceLoadOrderFileName = "cssLoadOrder.xml";
    private static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings()
    {
      IgnoreWhitespace = true,
      IgnoreComments = true
    };
    private static object ResourceLockObject = new object();

    public static ResourceLinks GetGlobalStyles(Page page)
    {
      bool isBackendPage = ThemeController.IsBackendPage();
      string key = (string) page.Items[(object) "theme"];
      if (page.GetType() != typeof (PageProxy) && Config.Get<PagesConfig>().AllowChangePageThemeAtRuntime)
      {
        string str = page.Request.QueryStringGet("theme", (Func<CacheVariationParamValidator>) (() =>
        {
          ThemeController.ThemeQueryParameterValidator globalStyles = new ThemeController.ThemeQueryParameterValidator();
          ThemeController.ThemeQueryParameterValidator parameterValidator = globalStyles;
          string[] strArray;
          if (!isBackendPage)
            strArray = (string[]) null;
          else
            strArray = new string[1]{ "true" };
          parameterValidator.Arguments = strArray;
          return (CacheVariationParamValidator) globalStyles;
        }));
        if (!str.IsNullOrEmpty())
          key = str;
      }
      if (string.IsNullOrEmpty(key))
      {
        key = !isBackendPage ? Config.Get<AppearanceConfig>().DefaultFrontendTheme : (!ThemeController.IsPreviewPage(page) ? Config.Get<AppearanceConfig>().BackendTheme : Config.Get<AppearanceConfig>().DefaultFrontendTheme);
        page.Items[(object) "theme"] = (object) key;
      }
      if (string.IsNullOrEmpty(key) || key == "notheme")
        return (ResourceLinks) null;
      ThemeElement theme;
      (!isBackendPage ? Config.Get<AppearanceConfig>().FrontendThemes : (!ThemeController.IsPreviewPage(page) ? Config.Get<AppearanceConfig>().BackendThemes : Config.Get<AppearanceConfig>().FrontendThemes)).TryGetValue(key, out theme);
      if (theme == null)
        return (ResourceLinks) null;
      string externalStylesCacheKey = ThemeController.GetExternalStylesCacheKey(theme, page);
      IList<ResourceFile> globalResorces = (IList<ResourceFile>) ThemeController.Cache.GetData(externalStylesCacheKey);
      if (globalResorces == null)
      {
        lock (ThemeController.ResourceLockObject)
        {
          globalResorces = (IList<ResourceFile>) ThemeController.Cache.GetData(externalStylesCacheKey);
          if (globalResorces == null)
          {
            if (ThemeController.IsThemeEmbedded(theme.Name, page, ThemeController.IsBackendPage() && !ThemeController.IsPreviewPage(page)))
            {
              globalResorces = ThemeController.GetEmbeddedStyleSheets(theme);
            }
            else
            {
              globalResorces = ThemeController.GetExternalStyleSheets(theme, page);
              globalResorces = ThemeController.SortExternalStyles(globalResorces, theme, page);
            }
            ThemeController.Cache.Add(externalStylesCacheKey, (object) globalResorces, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new AbsoluteTime(TimeSpan.FromMinutes(1.0)));
          }
        }
      }
      ResourceLinks globalStyles1 = new ResourceLinks();
      foreach (ResourceFile resourceFile in (IEnumerable<ResourceFile>) globalResorces)
      {
        resourceFile.Static = true;
        resourceFile.IsFromTheme = true;
        globalStyles1.Links.Add(resourceFile);
      }
      return globalStyles1;
    }

    /// <summary>
    /// Gets the theme for the given <see cref="T:System.Web.HttpContextBase" /> instance.
    /// </summary>
    /// <param name="page">The <see cref="T:System.Web.HttpContextBase" /> instance.</param>
    /// <param name="defaultValue">The default value for the theme.</param>
    /// <param name="isBackendPage">if set to <c>true</c> the current request is to the Sitefinity backend.</param>
    /// <returns></returns>
    public static string GetCurrentTheme(HttpContextBase context, string defaultValue)
    {
      bool isBackendPage = ThemeController.IsBackendPage();
      return ThemeController.GetCurrentTheme(context, defaultValue, isBackendPage);
    }

    internal static string GetCurrentTheme(
      HttpContextBase context,
      string defaultValue,
      bool isBackendPage)
    {
      string currentTheme1 = (string) null;
      if (Config.Get<PagesConfig>().AllowChangePageThemeAtRuntime)
        currentTheme1 = context.Request.QueryStringGet("theme", (Func<CacheVariationParamValidator>) (() =>
        {
          ThemeController.ThemeQueryParameterValidator currentTheme2 = new ThemeController.ThemeQueryParameterValidator();
          ThemeController.ThemeQueryParameterValidator parameterValidator = currentTheme2;
          string[] strArray;
          if (!isBackendPage)
            strArray = (string[]) null;
          else
            strArray = new string[1]{ "true" };
          parameterValidator.Arguments = strArray;
          return (CacheVariationParamValidator) currentTheme2;
        }));
      if (currentTheme1.IsNullOrEmpty())
        currentTheme1 = defaultValue;
      if (string.IsNullOrEmpty(currentTheme1))
        currentTheme1 = !isBackendPage ? Config.Get<AppearanceConfig>().DefaultFrontendTheme : (!SystemManager.IsPreviewMode ? Config.Get<AppearanceConfig>().BackendTheme : Config.Get<AppearanceConfig>().DefaultFrontendTheme);
      return currentTheme1;
    }

    private static bool ValidateQueryStringTheme(string themeName, bool isBackendPage) => ThemeController.IsAspNetTheme(themeName) || (!isBackendPage ? Config.Get<AppearanceConfig>().FrontendThemes : (!SystemManager.IsPreviewMode ? Config.Get<AppearanceConfig>().BackendThemes : Config.Get<AppearanceConfig>().FrontendThemes)).ContainsKey(themeName);

    public static void SetPageTheme(string themeName, Page page)
    {
      page.Items[(object) "theme"] = (object) themeName;
      if (!ThemeController.IsAspNetTheme(themeName, page))
        return;
      page.Theme = themeName;
    }

    public static string GetPageTheme(Page page) => page.Items[(object) "theme"] as string;

    public static bool IsThemeEmbedded(string themeName, Page page)
    {
      if (string.IsNullOrEmpty(themeName))
        return false;
      ThemeElement themeElement = ThemeController.GetThemeElement(themeName, ThemeController.IsBackendPage(), page);
      return themeElement != null && !string.IsNullOrEmpty(themeElement.Namespace);
    }

    public static bool IsThemeEmbedded(string themeName, Page page, bool isBackendPage)
    {
      if (string.IsNullOrEmpty(themeName))
        return false;
      ThemeElement themeElement = ThemeController.GetThemeElement(themeName, isBackendPage, page);
      return themeElement != null && !string.IsNullOrEmpty(themeElement.Namespace);
    }

    private static IList<ResourceFile> GetExternalStyleSheets(
      ThemeElement theme,
      Page page)
    {
      List<ResourceFile> externalStyleSheets = new List<ResourceFile>();
      string globalThemeFolder = ThemeController.GetGlobalThemeFolder(theme, page);
      string str1 = page.Server.MapPath("~/App_Themes");
      if (!globalThemeFolder.StartsWith(str1))
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(globalThemeFolder);
        foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*").Where<FileInfo>((Func<FileInfo, bool>) (f => f.Extension == ".css")))
        {
          string str2 = VirtualPathUtility.AppendTrailingSlash(theme.Path) + "global/" + fileInfo.Name;
          externalStyleSheets.Add(new ResourceFile()
          {
            Name = str2.Replace("App_Data/", ""),
            Version = fileInfo.LastWriteTime.Ticks.ToString()
          });
        }
        if (ObjectFactory.IsTypeRegistered<ILessCompiler>())
        {
          foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*").Where<FileInfo>((Func<FileInfo, bool>) (f => f.Extension == ".less" && !f.Name.StartsWith("_"))))
          {
            string str3 = VirtualPathUtility.AppendTrailingSlash(theme.Path) + "global/" + fileInfo.Name;
            externalStyleSheets.Add(new ResourceFile()
            {
              Name = str3.Replace("App_Data/", ""),
              Version = fileInfo.LastWriteTime.Ticks.ToString()
            });
          }
        }
      }
      else if (ObjectFactory.IsTypeRegistered<ILessCompiler>())
      {
        foreach (FileInfo enumerateFile in new DirectoryInfo(globalThemeFolder).Parent.EnumerateFiles("*.less"))
        {
          if (!enumerateFile.Name.StartsWith("_"))
          {
            string str4 = VirtualPathUtility.AppendTrailingSlash(theme.Path) + enumerateFile.Name;
            externalStyleSheets.Add(new ResourceFile()
            {
              Name = str4,
              Version = enumerateFile.LastWriteTime.Ticks.ToString()
            });
          }
        }
      }
      return (IList<ResourceFile>) externalStyleSheets;
    }

    private static IList<ResourceFile> GetEmbeddedStyleSheets(ThemeElement theme)
    {
      List<ResourceFile> embeddedStyleSheets = new List<ResourceFile>();
      foreach (WebResourceAttribute customAttribute in (WebResourceAttribute[]) Assembly.GetAssembly(Type.GetType(theme.AssemblyInfo)).GetCustomAttributes(typeof (WebResourceAttribute), false))
      {
        if (customAttribute.WebResource.StartsWith(theme.Namespace + ".Global.") && customAttribute.WebResource.EndsWith(".css"))
          embeddedStyleSheets.Add(new ResourceFile()
          {
            AssemblyInfo = theme.AssemblyInfo,
            Name = customAttribute.WebResource
          });
      }
      return (IList<ResourceFile>) embeddedStyleSheets;
    }

    internal static bool IsAspNetTheme(string themeName, Page page = null)
    {
      if (string.IsNullOrEmpty(themeName))
        return false;
      ThemeElement themeElement = ThemeController.GetThemeElement(themeName, ThemeController.IsBackendPage(), page);
      return themeElement != null && !string.IsNullOrEmpty(themeElement.Path) && themeElement.Path.StartsWith("~/App_Themes/");
    }

    public static bool IsBackendPage()
    {
      object obj = SystemManager.CurrentHttpContext.Items[(object) "IsFrontendPageEdit"];
      return (obj == null || !(bool) obj) && ControlExtensions.IsBackend();
    }

    public static bool IsPreviewPage(Page page) => page.RouteData.RouteHandler is PageEditorRouteHandler routeHandler && routeHandler.IsPreview;

    public static ThemeElement GetThemeElement(
      string themeName,
      bool isBackendTheme,
      Page page = null)
    {
      switch (themeName)
      {
        case "notheme":
          return (ThemeElement) null;
        case null:
          throw new ArgumentNullException(nameof (themeName));
        default:
          ThemeElement themeElement;
          (!isBackendTheme ? Config.Get<AppearanceConfig>().FrontendThemes : Config.Get<AppearanceConfig>().BackendThemes).TryGetValue(themeName, out themeElement);
          return themeElement;
      }
    }

    /// <summary>
    /// Returns if the page should use embeded stylesheets or those from the external template
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static bool UseEmbeddedStyles(Page page) => ThemeController.IsThemeEmbedded(ThemeController.GetPageTheme(page) ?? string.Empty, page, ThemeController.IsBackendPage() && !ThemeController.IsPreviewPage(page));

    /// <summary>
    /// Returns resoucefiles set to the theme path for each of the speicfied style sheets
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="styleSheetNames">Names of the style sheets files</param>
    /// <returns>List of external resourceFiles for the current theme</returns>
    public static List<ResourceFile> GetExternalResourcesByTheme(
      Page page,
      params string[] styleSheetNames)
    {
      string themeName = ThemeController.GetPageTheme(page) ?? string.Empty;
      string themePath = string.Empty;
      if (!string.IsNullOrEmpty(themeName))
      {
        ThemeElement themeElement = ThemeController.GetThemeElement(themeName, ThemeController.IsBackendPage(), page);
        if (themeElement != null)
          themePath = themeElement.Path;
      }
      return ((IEnumerable<string>) styleSheetNames).Select<string, ResourceFile>((Func<string, ResourceFile>) (s => new ResourceFile()
      {
        Name = (VirtualPathUtility.AppendTrailingSlash(themePath) + "/" + s).Replace("App_Data/", "")
      })).ToList<ResourceFile>();
    }

    /// <summary>
    /// Reorders the list with the global stylesheets according to the one specified in the
    /// Global/cssLoadOrder.xml file (if available)
    /// </summary>
    /// <param name="globalResorces">resources list</param>
    /// <param name="theme">Theme</param>
    /// <param name="page">Page</param>
    /// <returns></returns>
    private static IList<ResourceFile> SortExternalStyles(
      IList<ResourceFile> globalResorces,
      ThemeElement theme,
      Page page)
    {
      string str = Path.Combine(ThemeController.GetGlobalThemeFolder(theme, page), ThemeController.GlobalResourceLoadOrderFileName);
      List<ResourceFile> resourceFileList = new List<ResourceFile>();
      if (File.Exists(str))
      {
        Queue<string> cssLoadOrder = ThemeController.GetCssLoadOrder(str);
        if (cssLoadOrder.Count > 0 && globalResorces.Count > 0)
        {
          while (cssLoadOrder.Count > 0)
          {
            string cssFile = "/" + cssLoadOrder.Dequeue();
            ResourceFile resourceFile = globalResorces.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.Name.EndsWith(cssFile, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<ResourceFile>();
            if (resourceFile != null)
            {
              resourceFileList.Add(resourceFile);
              globalResorces.Remove(resourceFile);
            }
          }
          if (globalResorces.Count > 0)
            resourceFileList.AddRange((IEnumerable<ResourceFile>) globalResorces);
          return (IList<ResourceFile>) resourceFileList;
        }
      }
      return globalResorces;
    }

    private static Queue<string> GetCssLoadOrder(string fileName)
    {
      Queue<string> cssLoadOrder = new Queue<string>();
      try
      {
        using (XmlReader xmlReader = XmlReader.Create(fileName, ThemeController.ReaderSettings))
        {
          while (xmlReader.ReadToFollowing("file"))
            cssLoadOrder.Enqueue(xmlReader.ReadString());
        }
      }
      catch
      {
      }
      return cssLoadOrder;
    }

    private static ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    /// <summary>Returns the path for the global theme styles</summary>
    /// <param name="theme">The theme.</param>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    private static string GetGlobalThemeFolder(ThemeElement theme, Page page) => Path.Combine(page.Server.MapPath(theme.Path), "Global");

    /// <summary>
    /// Prepares the cache key for storing the external styles
    /// </summary>
    /// <param name="theme">Theme</param>
    /// <param name="page">Page</param>
    /// <returns>Cache key</returns>
    private static string GetExternalStylesCacheKey(ThemeElement theme, Page page) => page is PageProxy pageProxy ? nameof (ThemeController) + theme.Name + pageProxy.RawUrl : nameof (ThemeController) + theme.Name + page.Request.RawUrl;

    [Serializable]
    private class ThemeQueryParameterValidator : CacheVariationParamValidator
    {
      protected override bool Validate(string paramValue, string[] arguments)
      {
        bool isBackendPage = arguments != null && arguments.Length != 0 && arguments[0] == "true";
        return ThemeController.ValidateQueryStringTheme(paramValue, isBackendPage);
      }
    }
  }
}
