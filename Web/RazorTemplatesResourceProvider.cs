// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RazorTemplatesResourceProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using RazorEngine;
using RazorGenerator.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// A Razor templates provider that returns an executed template from a resourceName.
  /// Uses precompiled templates and if not available compiles the requested template runtime keeping track what is compiled.
  /// </summary>
  internal class RazorTemplatesResourceProvider : IResourceProvider
  {
    /// <summary>The custom razor compiled templates.</summary>
    internal static readonly HashSet<string> customRazorCompiledTemplates = new HashSet<string>();
    /// <summary>
    /// The mappings with pre-compilation Razor templates.
    /// All types assignable from RazorTemplateBase class.
    /// </summary>
    internal static readonly IDictionary<string, Type> PreCompiledRazorTemplatesMappings = (IDictionary<string, Type>) ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).Where<Type>((Func<Type, bool>) (t => typeof (RazorTemplateBase).IsAssignableFrom(t))).Select<Type, KeyValuePair<string, Type>>((Func<Type, KeyValuePair<string, Type>>) (t => new KeyValuePair<string, Type>(t.FullName, t))).ToDictionary<KeyValuePair<string, Type>, string, Type>((Func<KeyValuePair<string, Type>, string>) (t => t.Key), (Func<KeyValuePair<string, Type>, Type>) (t => t.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    /// <summary>The razor compilation lock member.</summary>
    private static readonly object razorCompilationLock = new object();
    /// <summary>The razor extension</summary>
    private const string RazorExtension = ".sfhtml";
    private IResourceProvider templateFilesProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.RazorTemplatesResourceProvider" /> class.
    /// </summary>
    public RazorTemplatesResourceProvider()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.RazorTemplatesResourceProvider" /> class.
    /// </summary>
    /// <param name="templateProvider">The provider that will be used to fetch razor template files if they are not precompiled.</param>
    public RazorTemplatesResourceProvider(IResourceProvider templateProvider) => this.templateFilesProvider = templateProvider;

    /// <summary>
    /// Attempts to get the requested resource by its name. If it succeeded the content of the resource will be available in the out parameter <paramref name="resourceContent" />.
    /// </summary>
    /// <param name="resourceName">The resource name.</param>
    /// <param name="resourceContent">Content of the resource.</param>
    /// <returns>
    /// <c>true</c> if the provider is able to serve the resource, <c>false</c> if not.
    /// </returns>
    public virtual bool TryGetResourceContent(string resourceName, out string resourceContent)
    {
      if (this.IsRazorTemplate(resourceName))
      {
        resourceContent = this.GetResourceContent(resourceName);
        return true;
      }
      resourceContent = (string) null;
      return false;
    }

    /// <summary>Gets the resource content by its name.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns></returns>
    public virtual string GetResourceContent(string resourceName)
    {
      string resourceNamespace = this.GetResourceNamespace(resourceName);
      string resourceContent;
      if (this.IsTemplatePrecompiled(resourceNamespace))
      {
        resourceContent = this.GetPrecompiledTemplate(resourceNamespace);
      }
      else
      {
        this.EnsureTemplateIsCompiled(resourceName);
        resourceContent = Razor.Run(resourceName);
      }
      return resourceContent;
    }

    internal string GetPrecompiledTemplate(string resourceNamespace) => (Activator.CreateInstance(RazorTemplatesResourceProvider.PreCompiledRazorTemplatesMappings[resourceNamespace]) as RazorTemplateBase).TransformText();

    internal void EnsureTemplateIsCompiled(string resourceName)
    {
      if (RazorTemplatesResourceProvider.customRazorCompiledTemplates.Contains(resourceName))
        return;
      lock (RazorTemplatesResourceProvider.razorCompilationLock)
      {
        if (RazorTemplatesResourceProvider.customRazorCompiledTemplates.Contains(resourceName))
          return;
        this.CompileRazorTemplate(resourceName);
      }
    }

    internal bool IsTemplatePrecompiled(string resourceNamespace) => RazorTemplatesResourceProvider.PreCompiledRazorTemplatesMappings != null && RazorTemplatesResourceProvider.PreCompiledRazorTemplatesMappings.ContainsKey(resourceNamespace);

    internal string GetResourceNamespace(string resourceName) => resourceName.EndsWith(".sfhtml") ? resourceName.Remove(resourceName.LastIndexOf(".sfhtml")) : resourceName;

    /// <summary>Compiles the razor template.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    internal void CompileRazorTemplate(string resourceName)
    {
      Razor.Compile(this.GetTemplateFilesProvider().GetResourceContent(resourceName), resourceName);
      RazorTemplatesResourceProvider.customRazorCompiledTemplates.Add(resourceName);
    }

    /// <summary>
    /// Gets the provider that will be used to fetch razor template files if they are not precompiled.
    /// </summary>
    /// <returns></returns>
    private IResourceProvider GetTemplateFilesProvider()
    {
      if (this.templateFilesProvider == null)
        this.templateFilesProvider = (IResourceProvider) new ResourceFileProvider();
      return this.templateFilesProvider;
    }

    internal bool IsRazorTemplate(string resourceName) => resourceName.EndsWith(".sfhtml");
  }
}
