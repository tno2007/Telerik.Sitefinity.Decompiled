// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Provides utility methods for working with controls.</summary>
  public static class ControlUtilities
  {
    private static ControlsConfig config = Config.Get<ControlsConfig>();
    private static object templateCacheLock = new object();
    private static Type telerikSitefinityResourcesReferenceType = (Type) null;
    private static readonly string precompiledAssemblyName = "Telerik.Sitefinity.PrecompiledTemplates";
    private static readonly string precompiledTemplatesNamespace = "ASP";
    private static readonly string sitefinityPrecompiledTemplatesFolder = "templates";
    private static bool isPrecompiledAssemblyChecked = false;
    internal static bool useFileSystemResources = false;
    private static Assembly precompiledAssembly;
    private static Regex controlIdFilterRegex = new Regex("^[^A-Za-z]+|[^\\w_ \\-:\\.]+", RegexOptions.Compiled);
    private const string ModifiedTemplateNamesCacheKey = "sf_altered_templates_key";
    private const string localizedKeyDelimiter = "_";

    /// <summary>Adds the CSS class to the specified control.</summary>
    /// <param name="control">The control.</param>
    /// <param name="cssClass">The CSS class.</param>
    public static void AddCssClass(WebControl control, string cssClass)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (string.IsNullOrEmpty(cssClass))
        return;
      if (string.IsNullOrEmpty(control.CssClass))
      {
        control.CssClass = cssClass;
      }
      else
      {
        WebControl webControl = control;
        webControl.CssClass = webControl.CssClass + " " + cssClass;
      }
    }

    /// <summary>Resolves the resource URL.</summary>
    /// <param name="path">The path.</param>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    public static string ResolveResourceUrl(string path, Page page)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException(nameof (path));
      if (page == null)
        throw new ArgumentNullException(nameof (page));
      if (path.StartsWith("~/"))
        return page.ResolveUrl(path);
      int num = !path.StartsWith("[") ? path.IndexOf(",") : throw new NotImplementedException("Libraries are in process of implementation.");
      Type type = num == -1 ? Config.Get<ControlsConfig>().ResourcesAssemblyInfo : TypeResolutionService.ResolveType(path.Substring(num + 1).Trim());
      return page.ClientScript.GetWebResourceUrl(type, path);
    }

    /// <summary>
    /// Resolves a resource url without need of Page (usable in services)
    /// </summary>
    /// <remarks>Uses internal MS method</remarks>
    /// <param name="path"></param>
    public static string ResolveResourceUrlWithoutPage(string path)
    {
      if (path.StartsWith("~/"))
        return ControlUtilities.ResolveUrl(path);
      int num = !path.StartsWith("[") ? path.IndexOf(",") : throw new NotImplementedException("Libraries are in process of implementation.");
      return new Page().ClientScript.GetWebResourceUrl(num == -1 ? Config.Get<ControlsConfig>().ResourcesAssemblyInfo : TypeResolutionService.ResolveType(path.Substring(num + 1).Trim()), path);
    }

    /// <summary>
    /// Returns a site relative HTTP path from a partial path starting out with a ~.
    /// Same syntax that ASP.Net internally supports but this method can be used
    /// outside of the Page framework.
    /// 
    /// Works like Control.ResolveUrl including support for ~ syntax
    /// but returns an absolute URL.
    /// </summary>
    /// <param name="originalUrl">Any Url including those starting with ~</param>
    /// <returns>relative url</returns>
    public static string ResolveUrl(string originalUrl)
    {
      if (originalUrl == null)
        return (string) null;
      if (originalUrl.IndexOf("://") != -1 || !originalUrl.StartsWith("~"))
        return originalUrl;
      if (SystemManager.CurrentHttpContext != null)
        return SystemManager.CurrentHttpContext.Request.ApplicationPath + originalUrl.Substring(1).Replace("//", "/");
      throw new ArgumentException("Invalid URL: Relative URL not allowed.");
    }

    /// <summary>
    /// Retrieves text from embedded resource file from Telerik.Sitefinity.Resources
    /// </summary>
    /// <param name="telerikSitefinityResourcesEmbeddedPath">Path to the embedded in Telerik.Sitefinity.Resources file</param>
    /// <returns>A string containing the text from the embedded file.</returns>
    public static string GetSitefinityTextResource(string telerikSitefinityResourcesEmbeddedPath)
    {
      if (ControlUtilities.telerikSitefinityResourcesReferenceType == (Type) null)
        ControlUtilities.telerikSitefinityResourcesReferenceType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources");
      return ControlUtilities.GetTextResource(telerikSitefinityResourcesEmbeddedPath, ControlUtilities.telerikSitefinityResourcesReferenceType);
    }

    /// <summary>Retrieves text from embedded resource file.</summary>
    /// <param name="fileName">The fully qualified name of the embedded file.</param>
    /// <param name="assemblyInfo">A type from the assembly containing the embedded resource.</param>
    /// <returns>A string containing the text from the embedded file.</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="fileName" /> or <paramref name="assemblyInfo" /> is <c>null</c>.</exception>
    public static string GetTextResource(string fileName, Type assemblyInfo)
    {
      if (string.IsNullOrEmpty(fileName))
        throw new ArgumentNullException(nameof (fileName));
      if (assemblyInfo == (Type) null)
        throw new ArgumentNullException(nameof (assemblyInfo));
      string textResource = (string) null;
      using (Stream manifestResourceStream = assemblyInfo.Assembly.GetManifestResourceStream(fileName))
      {
        if (manifestResourceStream != null)
        {
          using (StreamReader streamReader = new StreamReader(manifestResourceStream, true))
            textResource = streamReader.ReadToEnd();
        }
      }
      return textResource;
    }

    /// <summary>Loads control from virtual path.</summary>
    /// <param name="virtualPath">The virtual path of the file to create an instance of.</param>
    /// <returns><see cref="T:System.Web.UI.TemplateControl" /></returns>
    public static System.Web.UI.TemplateControl LoadControl(string virtualPath) => ControlUtilities.LoadControl(virtualPath, (Page) null);

    /// <summary>
    /// Loads control from virtual path and initializes it with the provided page instance.
    /// </summary>
    /// <param name="virtualPath">The virtual path of the file to create an instance of.</param>
    /// <param name="page">A <see cref="T:System.Web.UI.Page" /> instance or null.
    /// If page instance is provided the control is initialized with that page.</param>
    /// <returns><see cref="T:System.Web.UI.TemplateControl" /></returns>
    public static System.Web.UI.TemplateControl LoadControl(
      string virtualPath,
      Page page)
    {
      System.Web.UI.TemplateControl templateControl = (System.Web.UI.TemplateControl) null;
      ControlUtilities.CheckPrecompiledAssembly();
      if (ControlUtilities.precompiledAssembly != (Assembly) null)
      {
        string[] strArray1 = virtualPath.Split(new char[1]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries);
        if (virtualPath.StartsWith("~/SFRes/"))
        {
          templateControl = ControlUtilities.LoadControlFromAssembly(strArray1[strArray1.Length - 1], ControlUtilities.precompiledAssembly);
        }
        else
        {
          string str;
          if (virtualPath.StartsWith("~/SfCtrlPresentation/") && ControlPresentationResolver.HashToControlPathCache.TryGetValue(strArray1[strArray1.Length - 2], out str))
          {
            string[] strArray2 = str.Split(new char[1]
            {
              '/'
            }, StringSplitOptions.RemoveEmptyEntries);
            string templateName = strArray2[strArray2.Length - 1];
            if (!ControlUtilities.IsTemplatedModified(strArray2.Length > 1 ? ControlUtilities.GetTemplateKey(templateName, strArray2[0]) : templateName))
              templateControl = ControlUtilities.LoadControlFromAssembly(templateName, ControlUtilities.precompiledAssembly);
          }
        }
      }
      if (templateControl == null)
        templateControl = ControlUtilities.CompileControl(virtualPath);
      if (templateControl is UserControl userControl)
        userControl.InitializeAsUserControl(page);
      return templateControl;
    }

    /// <summary>
    /// Sanitizes the given HTML string so that it is safe to display as unencoded HTML.
    /// </summary>
    /// <param name="html">HTML object to be sanitized.</param>
    /// <returns>The sanitized HTML string.</returns>
    public static object Sanitize(object input)
    {
      if ((object) (input as Lstring) != null)
        input = (object) (input as Lstring).ToString();
      return input is string input1 ? (object) ControlUtilities.Sanitize(input1) : input;
    }

    /// <summary>
    /// Sanitizes the given HTML string so that it is safe to display as unencoded HTML.
    /// </summary>
    /// <param name="html">HTML object to be sanitized.</param>
    /// <returns>The sanitized HTML string.</returns>
    public static string Sanitize(string input) => !Config.Get<SecurityConfig>().DisableHtmlSanitization ? ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(input) : input;

    /// <summary>
    /// Sanitizes the given URL string. Will encode the parameters if needed and remove the url if the protocol is not allowed or if it is malformed.
    /// </summary>
    /// <param name="url">The URL string to be sanitized.</param>
    /// <returns>The sanitized URL string.</returns>
    public static string SanitizeUrl(string url) => !Config.Get<SecurityConfig>().DisableHtmlSanitization ? ObjectFactory.Resolve<IHtmlSanitizer>().SanitizeUrl(url) : url;

    private static void CheckPrecompiledAssembly()
    {
      if (ControlUtilities.isPrecompiledAssemblyChecked)
        return;
      try
      {
        ControlUtilities.precompiledAssembly = Assembly.Load(ControlUtilities.precompiledAssemblyName);
      }
      catch (FileNotFoundException ex)
      {
      }
      finally
      {
        ControlUtilities.isPrecompiledAssemblyChecked = true;
      }
    }

    private static System.Web.UI.TemplateControl CompileControl(string virtualPath) => CompilationHelpers.LoadControl<System.Web.UI.TemplateControl>(virtualPath, true);

    private static System.Web.UI.TemplateControl LoadControlFromAssembly(
      string templateName,
      Assembly assembly)
    {
      string typeName = string.Format("{0}.{1}_", (object) ControlUtilities.precompiledTemplatesNamespace, (object) ControlUtilities.sitefinityPrecompiledTemplatesFolder) + templateName.ToLower().Replace('.', '_');
      return (System.Web.UI.TemplateControl) assembly.CreateInstance(typeName);
    }

    private static string GetTemplateKey(string templateName, string controlType) => controlType.IsNullOrEmpty() || controlType == "~" ? templateName : controlType + "_" + templateName;

    private static bool IsPhysicalFileNewer(string virtualPath)
    {
      if (ControlUtilities.useFileSystemResources)
      {
        string resPath = (string) null;
        PathDefinition virtualPathDefinition = VirtualPathManager.GetVirtualPathDefinition(virtualPath, out resPath);
        if (virtualPathDefinition != null)
        {
          string resourceFileSystemPath = DebugFileSystemResolver.GetResourceFileSystemPath(virtualPathDefinition, virtualPath);
          if (ControlUtilities.precompiledAssembly != (Assembly) null && File.GetLastWriteTimeUtc(resourceFileSystemPath) > File.GetLastWriteTimeUtc(ControlUtilities.precompiledAssembly.Location))
            return true;
        }
      }
      return false;
    }

    private static bool IsTemplatedModified(string templateKey)
    {
      HashSet<string> stringSet = (HashSet<string>) ControlUtilities.templateCache["sf_altered_templates_key"];
      if (stringSet == null)
      {
        if (!Bootstrapper.IsReady)
          return false;
        lock (ControlUtilities.templateCacheLock)
        {
          stringSet = (HashSet<string>) ControlUtilities.templateCache["sf_altered_templates_key"];
          if (stringSet == null)
          {
            stringSet = new HashSet<string>((IEnumerable<string>) PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.IsDifferentFromEmbedded && p.Data != default (string))).Select<ControlPresentation, string>((Expression<Func<ControlPresentation, string>>) (p => ControlUtilities.GetTemplateKey(p.EmbeddedTemplateName, p.ControlType))).ToList<string>());
            ControlUtilities.templateCache.Add("sf_altered_templates_key", (object) stringSet, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (ControlPresentation), (string) null));
          }
        }
      }
      return stringSet.Contains(templateKey);
    }

    /// <summary>Gets a control template.</summary>
    /// <param name="virtualPath">Virtual path to template's location.</param>
    /// <param name="resourceFileName">The fully qualified name of the embedded file.</param>
    /// <param name="assemblyInfo">A type that determines the assembly containing the embedded resource file.</param>
    /// <param name="templateDeclaration">The template declaration.</param>
    /// <returns>
    /// An <see cref="T:System.Web.UI.ITemplate" /> object loaded from virtual path or embedded resource.
    /// </returns>
    public static ITemplate GetTemplate(
      string virtualPath,
      string resourceFileName,
      Type assemblyInfo,
      string templateDeclaration)
    {
      return ControlUtilities.GetTemplate(virtualPath, resourceFileName, assemblyInfo, templateDeclaration, false);
    }

    /// <summary>Gets a control template.</summary>
    /// <param name="virtualPath">Virtual path to template's location.</param>
    /// <param name="resourceFileName">The fully qualified name of the embedded file.</param>
    /// <param name="assemblyInfo">A type that determines the assembly containing the embedded resource file.</param>
    /// <param name="templateDeclaration">The template declaration.</param>
    /// <returns>
    /// An <see cref="T:System.Web.UI.ITemplate" /> object loaded from virtual path or embedded resource.
    /// </returns>
    public static ITemplate GetTemplate(
      string virtualPath,
      string resourceFileName,
      Type assemblyInfo,
      string templateDeclaration,
      bool addChildrenAsDirectDescendants)
    {
      string key;
      if (string.IsNullOrEmpty(virtualPath))
      {
        key = !string.IsNullOrEmpty(resourceFileName) ? resourceFileName : throw new ArgumentException("Insufficient data to create template.");
        if (assemblyInfo != (Type) null)
          key = key + ", " + assemblyInfo.Assembly.FullName;
      }
      else
        key = virtualPath;
      ITemplate template = (ITemplate) ControlUtilities.templateCache[key];
      if (template == null)
      {
        lock (ControlUtilities.templateCacheLock)
        {
          template = (ITemplate) ControlUtilities.templateCache[key];
          if (template == null)
          {
            string declaration;
            if (!string.IsNullOrEmpty(virtualPath))
            {
              string str = HostingEnvironment.VirtualPathProvider.FileExists(virtualPath) ? VirtualPathUtility.GetExtension(virtualPath) : throw new ArgumentException(Res.Get<ErrorMessages>("CannotFindTemplate", (object) virtualPath));
              if (str.Equals(".ascx", StringComparison.OrdinalIgnoreCase) || str.Equals(".aspx", StringComparison.OrdinalIgnoreCase))
              {
                template = (ITemplate) new VirtualPathTemplate(virtualPath, addChildrenAsDirectDescendants);
                goto label_28;
              }
              else
              {
                using (StreamReader streamReader = new StreamReader(HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open()))
                  declaration = streamReader.ReadToEnd();
                resourceFileName = virtualPath;
              }
            }
            else if (!string.IsNullOrEmpty(templateDeclaration))
            {
              declaration = templateDeclaration;
            }
            else
            {
              if (string.IsNullOrEmpty(resourceFileName))
                return (ITemplate) null;
              declaration = ControlUtilities.GetTextResource(resourceFileName, assemblyInfo);
            }
            if (string.IsNullOrEmpty(declaration))
            {
              string str = string.IsNullOrEmpty(virtualPath) ? resourceFileName : virtualPath;
              throw new ArgumentException(Res.Get<ErrorMessages>().InvalidResourceNameOrEmptyTemplate.Arrange((object) str, (object) assemblyInfo.Assembly.FullName));
            }
            template = (ITemplate) new ControlUtilities.StringTemplate(declaration, resourceFileName);
label_28:
            ControlUtilities.templateCache.Add(key, (object) template, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes((double) ControlUtilities.config.TemplateCacheExpirationInMinutes)));
          }
        }
      }
      return template;
    }

    /// <summary>
    /// Gets a control template.
    /// The method first tries to load a template form presentation data.
    /// If that fails the template is loaded either from virtual path or embedded resource.
    /// </summary>
    /// <param name="info">
    /// Provides information about the template to be loaded.
    /// </param>
    /// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object loaded from presentation data, virtual path or embedded resource.</returns>
    public static ITemplate GetTemplate(TemplateInfo info) => info != null ? ControlUtilities.GetControlTemplate(info) : throw new ArgumentNullException(nameof (info));

    /// <summary>
    /// Gets a control template.
    /// The method first tries to load a template form the virtual path, if provided.
    /// If that fails the template is loaded form embedded resource.
    /// </summary>
    /// <param name="info">
    /// Provides information about the template to be loaded.
    /// </param>
    /// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object loaded from virtual path or embedded resource.</returns>
    public static ITemplate GetControlTemplate(TemplateInfo info)
    {
      string str;
      if (ControlUtilities.IsPathForEmbeddedTemplate(info.TemplatePath))
      {
        if (!string.IsNullOrEmpty(info.Declaration))
        {
          str = info.TemplateName + (object) info.Declaration.GetHashCode();
        }
        else
        {
          if (!(info.ControlType != (Type) null))
            throw new ArgumentException("The TemplateInfo class contains insufficient data to create template.");
          ViewModeControlSettings modeControlSettings;
          if (ControlUtilities.config.ViewMap.TryGetValue(info.ControlType, out modeControlSettings))
          {
            if (!string.IsNullOrEmpty(info.ConfigAdditionalKey))
            {
              string key = string.Empty;
              int length = info.ConfigAdditionalKey.IndexOf('|');
              if (length > -1)
                key = info.ConfigAdditionalKey.Substring(0, length);
              AdditionalTemplateElement additionalTemplateElement;
              str = !modeControlSettings.AdditionalTemplates.TryGetValue(info.ConfigAdditionalKey, out additionalTemplateElement) ? (string.IsNullOrEmpty(key) || !modeControlSettings.AdditionalTemplates.TryGetValue(key, out additionalTemplateElement) ? modeControlSettings.LayoutTemplatePath : additionalTemplateElement.LayoutTemplatePath) : additionalTemplateElement.LayoutTemplatePath;
            }
            else
              str = modeControlSettings.LayoutTemplatePath;
          }
          else if (Bootstrapper.IsSystemInitialized)
          {
            if (!string.IsNullOrEmpty(info.Key))
            {
              str = ControlPresentationResolver.GenerateUrl(new Guid(info.Key));
            }
            else
            {
              str = info.TemplatePath;
              if (!string.IsNullOrEmpty(str) && !str.StartsWith("~/SfCtrlPresentation/") && !info.ControlType.IsGenericType)
                str = ControlPresentationResolver.GenerateUrl(info.ControlType, str);
            }
          }
          else
            str = info.TemplatePath;
        }
      }
      else
        str = info.TemplatePath;
      return ControlUtilities.GetTemplate(str, info.TemplateName, info.TemplateResourceInfo, info.Declaration, info.AddChildrenAsDirectDescendants);
    }

    public static string GetClientTemplate(string resourceFileName, Type assemblyInfo) => ControlUtilities.GetTextResource(resourceFileName, assemblyInfo);

    /// <summary>
    /// Provides a check on a template to see if it is embedded or no
    /// </summary>
    /// <param name="templateInfo"></param>
    /// <returns></returns>
    public static bool IsPathForEmbeddedTemplate(string templatePath)
    {
      bool flag = string.IsNullOrEmpty(templatePath);
      if (!flag)
      {
        if (templatePath[0] == '~')
          templatePath = templatePath.Substring(1, templatePath.Length - 1);
        if (!string.IsNullOrEmpty(templatePath))
        {
          foreach (VirtualPathElement virtualPath in (ConfigElementCollection) Config.Get<VirtualPathSettingsConfig>().VirtualPaths)
          {
            string str1 = virtualPath.VirtualPath;
            if (!string.IsNullOrEmpty(str1) && virtualPath.ResolverName == "EmbeddedResourceResolver")
            {
              if (str1[0] == '~')
                str1 = str1.Substring(1, str1.Length - 1);
              if (!string.IsNullOrEmpty(str1))
              {
                if (str1.Equals(templatePath, StringComparison.OrdinalIgnoreCase))
                {
                  flag = true;
                  break;
                }
                if (str1[str1.Length - 1] == '*')
                {
                  string str2 = str1.Substring(0, str1.Length - 1);
                  if (!string.IsNullOrEmpty(str2) && templatePath.StartsWith(str2, StringComparison.OrdinalIgnoreCase))
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Determines whether the specified string represents globally unique identifier (GUID).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// 	<c>true</c> if the specified value is GUID; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsGuid(string value) => value != null && value.Length == 36 && Regex.IsMatch(value, "^?[\\da-f]{8}-([\\da-f]{4}-){3}[\\da-f]{12}?$", RegexOptions.IgnoreCase);

    /// <summary>Removes the template from cache.</summary>
    /// <param name="key">The template key.</param>
    /// <returns></returns>
    public static bool RemoveTemplateFromCache(string key)
    {
      bool flag = false;
      lock (ControlUtilities.templateCacheLock)
      {
        if (ControlUtilities.templateCache.Contains(key))
        {
          ControlUtilities.templateCache.Remove(key);
          flag = true;
        }
      }
      return flag;
    }

    /// <summary>Converts layout template name to VPP path</summary>
    /// <param name="layoutTemplateName">Name of the layout template.</param>
    /// <returns></returns>
    public static string ToVppPath(string layoutTemplateName) => "~/SFRes/" + layoutTemplateName;

    /// <summary>
    /// Sets a control id for the specified as parameter control from a given string value.
    /// The controlId is transformed to camelCase string that begins with a letter A-Z or a-z
    /// Can be followed by: letters (A-Za-z), digits (0-9), hyphens ("-"), underscores ("_"), colons (":"), and periods (".") all other
    /// chars are removed. The control id will not be updated if the result of the string transformation is an empty string.
    /// </summary>
    /// <param name="name">The string value from which to generate a controlID value.</param>
    /// <param name="control">The control which ID should be updated</param>
    public static void SetControlIdFromName(string name, Control control) => ControlUtilities.SetControlIdFromName(name, control, (char[]) null);

    public static void SetControlIdFromName(string name, Control control, char[] separators)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (!(name != string.Empty))
        return;
      string str1 = ControlUtilities.controlIdFilterRegex.Replace(name, "");
      if (string.IsNullOrEmpty(str1))
        return;
      string str2 = str1.TocamelCase(separators);
      if (string.IsNullOrEmpty(str2))
        return;
      control.ID = str2;
    }

    /// <summary>Resolves the placeholders from the specified control</summary>
    public static PlaceHoldersCollection GetPlaceholders(Control layoutControl)
    {
      PlaceHoldersCollection placeholders = new PlaceHoldersCollection();
      foreach (Control control in new ControlTraverser(layoutControl, TraverseMethod.BreadthFirst))
      {
        if (control is HtmlGenericControl htmlGenericControl && htmlGenericControl.Attributes["class"].Contains("sf_colsIn"))
          placeholders.Add((Control) htmlGenericControl);
      }
      return placeholders;
    }

    /// <summary>
    /// Gets localized version of given object.
    /// E.g. (itemId)_(language)
    /// </summary>
    /// <param name="itemId">The id.</param>
    /// <returns></returns>
    public static string GetLocalizedKey(object itemId) => ControlUtilities.GetLocalizedKey(itemId, (string) null);

    /// <summary>
    /// Gets localized version of given object.
    /// E.g. (itemId)_(language)
    /// </summary>
    /// <param name="itemId">The id.</param>
    /// <param name="language"></param>
    /// <returns></returns>
    public static string GetLocalizedKey(object itemId, string language) => ControlUtilities.GetLocalizedKey(itemId, language, (string) null);

    /// <summary>
    /// Gets localized version of given object.
    /// E.g. (itemId)_(language)_(suffix)
    /// </summary>
    /// <param name="itemId">The id.</param>
    /// <param name="language">The language</param>
    /// <param name="suffix">The suffix</param>
    /// <returns></returns>
    public static string GetLocalizedKey(object itemId, string language, string suffix)
    {
      if (string.IsNullOrEmpty(language))
        language = CommentsUtilities.GetCurrentLanguage();
      string localizedKey = itemId.ToString() + "_" + language;
      if (!string.IsNullOrEmpty(suffix))
        localizedKey = localizedKey + "_" + suffix;
      return localizedKey;
    }

    /// <summary>
    /// Tries to parse the given localized key and to extract item id, language and suffix (if present).
    /// </summary>
    /// <param name="localizedKey">The localized key.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="language">The language.</param>
    /// <param name="suffix">The suffix.</param>
    /// <returns></returns>
    internal static bool LocalizedKeyTryParse(
      string localizedKey,
      out string itemId,
      out string language,
      out string suffix)
    {
      itemId = language = suffix = (string) null;
      try
      {
        string[] source = localizedKey.Split(new string[1]
        {
          "_"
        }, StringSplitOptions.RemoveEmptyEntries);
        int num = ((IEnumerable<string>) source).Count<string>();
        if (num > 0)
          itemId = source[0];
        if (num > 1)
          language = source[1];
        if (num > 2)
          suffix = source[2];
        return true;
      }
      catch
      {
      }
      return false;
    }

    /// <summary>Gets the unique provider key.</summary>
    /// <param name="dataSourceName">Name of the data source. Name of the data source. That is usually manager type. For dynamic items it is the dynamic module name.</param>
    /// <param name="providerName">Name of the provider.</param>
    public static string GetUniqueProviderKey(string dataSourceName, string providerName) => dataSourceName + "_" + providerName;

    /// <summary>Try get the item id by key.</summary>
    /// <param name="key">The thread key.</param>
    public static bool TryGetItemId(string key, out Guid contentItemId)
    {
      contentItemId = Guid.Empty;
      if (key.IsNullOrEmpty())
        return false;
      string[] source = key.Split('_');
      return ((IEnumerable<string>) source).Count<string>() > 0 && Guid.TryParse(source[0], out contentItemId);
    }

    internal static IControlBehaviorResolver BehaviorResolver => ObjectFactory.Resolve<IControlBehaviorResolver>();

    private static ICacheManager templateCache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    /// <summary>Gets the item identifier used for relating items.</summary>
    /// <param name="item">The item.</param>
    public static Guid GetItemIdForRelations(object item)
    {
      Guid empty = Guid.Empty;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
      PropertyDescriptor propertyDescriptor = properties["OriginalContentId"];
      if (propertyDescriptor != null)
        empty = (Guid) propertyDescriptor.GetValue(item);
      if (empty == Guid.Empty)
        empty = (Guid) properties["Id"].GetValue(item);
      return empty;
    }

    [DebuggerDisplay("StringTemplate, loaded from {name}")]
    private class StringTemplate : IContentPlaceHolderContainer, ITemplate
    {
      private Telerik.Sitefinity.Web.UI.Templates.RootBuilder rootBuilder;
      private string name;

      public StringTemplate(string declaration, string templateName)
      {
        this.rootBuilder = new Telerik.Sitefinity.Web.UI.Templates.RootBuilder(templateName, declaration);
        this.name = templateName;
      }

      public void InstantiateIn(Control container) => this.InstantiateIn(container, (PlaceHoldersCollection) null);

      public void InstantiateIn(Control container, PlaceHoldersCollection placeHolders)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (container is GenericContainer genericContainer)
        {
          genericContainer.IsExternal = false;
          genericContainer.TemplateName = this.name;
        }
        if (placeHolders != null)
          this.rootBuilder.CreateChildControls(container, container, placeHolders);
        else
          this.rootBuilder.CreateChildControls(container, container);
      }

      PlaceHoldersCollection IContentPlaceHolderContainer.InstantiateIn(
        Control container)
      {
        PlaceHoldersCollection placeHolders = new PlaceHoldersCollection();
        this.InstantiateIn(container, placeHolders);
        return placeHolders;
      }
    }
  }
}
