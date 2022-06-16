// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.ClientResourceManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>
  /// This class provides common functionality for working with client side resources, such
  /// as stylesheets and javascript files
  /// </summary>
  public class ClientResourceManager
  {
    private readonly bool useThemes;
    private readonly string theme;
    private Type resourcesAssemblyInfo;
    private Control host;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Utilities.ClientResourceManager" /> class.
    /// </summary>
    public ClientResourceManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Utilities.ClientResourceManager" /> class.
    /// </summary>
    /// <param name="useThemes">if set to <c>true</c> [use themes].</param>
    public ClientResourceManager(bool useThemes)
    {
      this.useThemes = useThemes;
      if (!this.useThemes)
        return;
      this.theme = Config.Get<AppearanceConfig>().BackendTheme;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Utilities.ClientResourceManager" /> class.
    /// </summary>
    /// <param name="useThemes">if set to <c>true</c> [use themes].</param>
    /// <param name="theme">The theme.</param>
    /// <param name="host">The host.</param>
    public ClientResourceManager(bool useThemes, string theme, Control host)
    {
      this.useThemes = useThemes;
      this.theme = theme;
      this.host = host;
    }

    /// <summary>Gets the resources assmebly.</summary>
    /// <value>The resources assmebly.</value>
    protected virtual Type ResourcesAssemblyInfo
    {
      get
      {
        if (this.resourcesAssemblyInfo == (Type) null)
          this.resourcesAssemblyInfo = new ControlsConfig().ResourcesAssemblyInfo;
        return this.resourcesAssemblyInfo;
      }
    }

    /// <summary>Combines the resources.</summary>
    /// <param name="resources">The resources.</param>
    /// <param name="useEmbedded">if set to <c>true</c> [use embedded].</param>
    /// <returns></returns>
    public string CombineResources(IList<ResourceFile> resources, bool? useEmbedded, Page page)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ResourceFile resource in (IEnumerable<ResourceFile>) resources)
      {
        Type assemblyInfo = (Type) null;
        if (!string.IsNullOrEmpty(resource.AssemblyInfo))
          assemblyInfo = Type.GetType(resource.AssemblyInfo);
        if (resource.JavaScriptLibrary == JavaScriptLibrary.None)
        {
          bool? nullable = useEmbedded;
          bool flag = true;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            stringBuilder.Append(this.ReadEmbeddedFile(this.GetEmbeddedResourceName(resource.Name, resource.Static), assemblyInfo));
          else
            stringBuilder.Append(this.ReadExternalFile(this.GetExternalResourcePath(resource.Name, resource.Static, page)));
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>Reads the external file.</summary>
    /// <param name="resourcePath">The resource path.</param>
    /// <returns></returns>
    protected virtual string ReadExternalFile(string resourcePath)
    {
      if (string.IsNullOrEmpty(resourcePath))
        throw new ArgumentNullException(nameof (resourcePath));
      if (!File.Exists(resourcePath))
        throw new FileNotFoundException(resourcePath);
      using (StreamReader streamReader = new StreamReader(resourcePath))
        return streamReader.ReadToEnd();
    }

    /// <summary>Reads the embedded file.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <param name="assemblyInfo">The assembly info.</param>
    /// <returns></returns>
    protected virtual string ReadEmbeddedFile(string resourceName, Type assemblyInfo)
    {
      if (string.IsNullOrEmpty(resourceName))
        throw new ArgumentNullException(resourceName);
      Type type = assemblyInfo;
      if ((object) type == null)
        type = this.ResourcesAssemblyInfo;
      assemblyInfo = type;
      Stream manifestResourceStream = assemblyInfo.Assembly.GetManifestResourceStream(resourceName);
      if (manifestResourceStream == null)
        throw new ArgumentException(Res.Get<ErrorMessages>("InvalidResourceName", (object) resourceName));
      string end;
      using (StreamReader streamReader = new StreamReader(manifestResourceStream))
        end = streamReader.ReadToEnd();
      return this.PerformSubstitution(end);
    }

    /// <summary>
    /// Performs the substitution of the embedded resource references with the usable urls.
    /// </summary>
    /// <param name="text">The text on which the substitution should be performed.</param>
    /// <returns></returns>
    protected virtual string PerformSubstitution(string text) => new Regex("<%\\s*=\\s*WebResource\\(\\\"(?<1>.+)\\\"\\)\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(text, new MatchEvaluator(this.ReplaceResource));

    /// <summary>Replaces the resource.</summary>
    /// <param name="m">The m.</param>
    /// <returns></returns>
    private string ReplaceResource(Match m) => this.host.Page.ClientScript.GetWebResourceUrl(this.ResourcesAssemblyInfo, m.Groups[1].Value);

    /// <summary>Gets the path of the external resource.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <param name="isStatic">if set to <c>true</c> [is static].</param>
    /// <returns>Path of the external resource.</returns>
    public virtual string GetExternalResourcePath(string resourceName, bool isStatic, Page page)
    {
      if (string.IsNullOrEmpty(resourceName))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ResourceLink_ResourceNameNotDefined);
      if (isStatic || resourceName.StartsWith("~"))
        return resourceName;
      StringBuilder stringBuilder = new StringBuilder();
      if (!this.theme.IsNullOrEmpty())
      {
        string virtualPath = (string) null;
        ThemeElement themeElement = ThemeController.GetThemeElement(this.theme, ThemeController.IsBackendPage(), page);
        if (themeElement != null)
          virtualPath = themeElement.Path;
        if (virtualPath != null)
          stringBuilder.Append(VirtualPathUtility.AppendTrailingSlash(virtualPath));
      }
      string str1 = resourceName;
      char[] chArray = new char[1]{ '/' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str2))
        {
          stringBuilder.Append(str2);
          stringBuilder.Append("/");
        }
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      stringBuilder.Replace("/App_Data", "");
      return stringBuilder.ToString();
    }

    /// <summary>Gets the name of the embedded resource.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <param name="isStatic">if set to <c>true</c> [is static].</param>
    /// <returns>Name of the embedded resource</returns>
    public virtual string GetEmbeddedResourceName(string resourceName, bool isStatic)
    {
      if (string.IsNullOrEmpty(resourceName))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ResourceLink_ResourceNameNotDefined);
      if (isStatic)
        return resourceName.Replace("/", ".");
      StringBuilder stringBuilder = new StringBuilder();
      ThemeElement themeElement = ThemeController.GetThemeElement(this.theme, ThemeController.IsBackendPage());
      if (themeElement != null && !string.IsNullOrEmpty(themeElement.Namespace))
      {
        stringBuilder.Append(themeElement.Namespace);
      }
      else
      {
        stringBuilder.Append("Telerik.Sitefinity.Resources.Themes");
        if (!"Telerik.Sitefinity.Resources.Themes".EndsWith(".", StringComparison.OrdinalIgnoreCase))
          stringBuilder.Append(".");
        stringBuilder.Append(this.theme);
      }
      stringBuilder.Append(".");
      string str1 = resourceName;
      char[] chArray = new char[1]{ '/' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str2))
        {
          stringBuilder.Append(str2);
          stringBuilder.Append(".");
        }
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }
  }
}
