// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Frontend.Resources.ResourceTemplateProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Resources
{
  /// <summary>
  /// Instance of this class replaces resources in a given template with their localized values.
  /// The instance provides in-memory cache for all parsed templates.
  /// A template's cache is invalidated when the file with the given file path is changed and also when the app is restarted.
  /// </summary>
  public class ResourceTemplateProcessor
  {
    private readonly Dictionary<string, ResourceTemplateProcessor.CachedTemplate> cachedTemplates;
    private readonly VirtualPathProvider virtualPathProvider;
    private readonly Regex resourceRegex;
    private const string ResourcePattern = "@\\(\\s*Res.Get<(?<ResourceClass>[\\w_\\d]+)>\\(\\)\\.(?<PropertyName>[\\w_\\d]+)\\s*\\)";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Frontend.Resources.ResourceTemplateProcessor" /> class.
    /// </summary>
    public ResourceTemplateProcessor()
      : this(HostingEnvironment.VirtualPathProvider)
    {
      this.cachedTemplates = new Dictionary<string, ResourceTemplateProcessor.CachedTemplate>();
      this.resourceRegex = new Regex("@\\(\\s*Res.Get<(?<ResourceClass>[\\w_\\d]+)>\\(\\)\\.(?<PropertyName>[\\w_\\d]+)\\s*\\)", RegexOptions.Compiled);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Frontend.Resources.ResourceTemplateProcessor" /> class.
    /// </summary>
    /// <param name="virtualPathProvider">The virtual path provider for retrieving files.</param>
    public ResourceTemplateProcessor(VirtualPathProvider virtualPathProvider) => this.virtualPathProvider = virtualPathProvider;

    /// <summary>Localizes the template at the specified path.</summary>
    /// <param name="templatePath">The template path.</param>
    /// 
    ///             /// <param name="processedTemplate">The localized template.</param>
    /// <returns>True if template is processed correctly, otherwise false.</returns>
    public bool Process(string templatePath, out string processedTemplate) => this.ProcessInternal(templatePath, templatePath, out processedTemplate);

    /// <summary>Localizes the template at the specified path.</summary>
    /// <param name="templatePath">The template path.</param>
    /// <param name="culture">The culture for the template version.</param>
    /// <param name="processedTemplate">The localized template.</param>
    /// <returns>True if template is processed correctly, otherwise false.</returns>
    public bool Process(string templatePath, CultureInfo culture, out string processedTemplate)
    {
      if (culture == null)
        return this.ProcessInternal(templatePath, templatePath, out processedTemplate);
      string cacheKey = culture.Name + "_" + templatePath;
      using (new CultureRegion(culture))
        return this.ProcessInternal(templatePath, cacheKey, out processedTemplate);
    }

    private bool ProcessInternal(
      string templatePath,
      string cacheKey,
      out string processedTemplate)
    {
      if (this.ShouldProcessTemplate(cacheKey))
      {
        lock (this.cachedTemplates)
        {
          if (this.ShouldProcessTemplate(cacheKey))
          {
            try
            {
              string template = this.GetTemplate(templatePath);
              processedTemplate = this.ReplaceResources(template);
              CacheDependency cacheDependency = this.virtualPathProvider.GetCacheDependency(templatePath, (IEnumerable) null, DateTime.UtcNow);
              this.cachedTemplates[cacheKey] = new ResourceTemplateProcessor.CachedTemplate()
              {
                Dependency = cacheDependency,
                Template = processedTemplate
              };
              return true;
            }
            catch (Exception ex)
            {
              Log.Write((object) string.Format("Error when localizing a template with path {0}. Exception: {1}", (object) templatePath, (object) ex.ToString()));
              processedTemplate = ex.Message;
              return false;
            }
          }
          else
          {
            processedTemplate = this.cachedTemplates[cacheKey].Template;
            return true;
          }
        }
      }
      else
      {
        processedTemplate = this.cachedTemplates[cacheKey].Template;
        return true;
      }
    }

    /// <summary>
    /// Replaces the resources in the given markup with their respective localized values.
    /// </summary>
    /// <param name="markup">The markup.</param>
    /// <returns>Markup with replaced values.</returns>
    private string ReplaceResources(string markup) => this.resourceRegex.Replace(markup, (MatchEvaluator) (match => Res.Get(match.Groups["ResourceClass"].Value, match.Groups["PropertyName"].Value)));

    /// <summary>
    /// Gets the template from a file at the specified path as string.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The template as string.</returns>
    private string GetTemplate(string path)
    {
      string template = string.Empty;
      if (this.virtualPathProvider.FileExists(path))
      {
        using (StreamReader streamReader = new StreamReader(this.virtualPathProvider.GetFile(path).Open()))
          template = streamReader.ReadToEnd();
      }
      return template;
    }

    /// <summary>
    /// Determines whether the template should be parsed - when the file is changed or if it is not in the cache.
    /// </summary>
    /// <param name="templatePath">The template path.</param>
    /// <returns>True if template should be processed, otherwise false.</returns>
    private bool ShouldProcessTemplate(string templatePath)
    {
      if (!this.cachedTemplates.ContainsKey(templatePath))
        return true;
      return this.cachedTemplates[templatePath].Dependency != null && this.cachedTemplates[templatePath].Dependency.HasChanged;
    }

    private class CachedTemplate
    {
      public string Template { get; set; }

      public CacheDependency Dependency { get; set; }
    }
  }
}
