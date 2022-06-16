// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageAssemblyLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Sitefinity.Web
{
  internal class PageAssemblyLoader
  {
    private const string BackendPagesAssemblyPrefix = "Telerik.Sitefinity.PrecompiledPages.Backend";
    private const string FrontEndPagesAssemblyPrefix = "Telerik.Sitefinity.PrecompiledPages.Frontend";
    private static IDictionary<string, PageAssemblyLoader> cachedAssemblyLoaders = (IDictionary<string, PageAssemblyLoader>) new Dictionary<string, PageAssemblyLoader>()
    {
      {
        "Telerik.Sitefinity.PrecompiledPages.Backend",
        new PageAssemblyLoader("Telerik.Sitefinity.PrecompiledPages.Backend")
      },
      {
        "Telerik.Sitefinity.PrecompiledPages.Frontend",
        new PageAssemblyLoader("Telerik.Sitefinity.PrecompiledPages.Frontend")
      }
    };
    private volatile bool assemblySequenceLoadError;
    private readonly object asmLoadLock = new object();
    private IDictionary<string, Assembly> virtualPathToAssemblyMap = (IDictionary<string, Assembly>) new Dictionary<string, Assembly>();
    private IDictionary<string, string> virtualPathToVersionMap = (IDictionary<string, string>) new Dictionary<string, string>();
    private string assemblyPrefix;
    private int assemblyCounter;

    internal static bool TryLoadFromAssembly(
      string virtualPath,
      PageSiteNode siteNode,
      out Page instance)
    {
      return siteNode.IsBackend ? PageAssemblyLoader.TryLoadFromAssembly(virtualPath, siteNode, "Telerik.Sitefinity.PrecompiledPages.Backend", out instance) : PageAssemblyLoader.TryLoadFromAssembly(virtualPath, siteNode, "Telerik.Sitefinity.PrecompiledPages.Frontend", out instance);
    }

    private static bool TryLoadFromAssembly(
      string virtualPath,
      PageSiteNode siteNode,
      string assemblyPrefix,
      out Page instance)
    {
      instance = (Page) null;
      PageAssemblyLoader pageAssemblyLoader;
      return PageAssemblyLoader.cachedAssemblyLoaders.TryGetValue(assemblyPrefix, out pageAssemblyLoader) && pageAssemblyLoader.TryGetInstance(virtualPath, siteNode, out instance);
    }

    private PageAssemblyLoader(string assemblyPrefix) => this.assemblyPrefix = assemblyPrefix;

    private bool TryGetInstance(string virtualPath, PageSiteNode siteNode, out Page instance)
    {
      instance = (Page) null;
      Assembly assembly = (Assembly) null;
      if (!this.virtualPathToAssemblyMap.TryGetValue(virtualPath, out assembly) && !this.assemblySequenceLoadError)
      {
        lock (this.asmLoadLock)
        {
          while (!this.assemblySequenceLoadError)
          {
            if (!this.virtualPathToAssemblyMap.TryGetValue(virtualPath, out assembly))
              this.LoadNextAssembly(this.assemblyPrefix, this.assemblyCounter++);
            else
              break;
          }
        }
      }
      if (assembly != (Assembly) null)
      {
        string str = (string) null;
        if (this.virtualPathToVersionMap.TryGetValue(virtualPath, out str) && str == siteNode.VersionKey)
        {
          string typeName = "ASP." + virtualPath.ToLowerInvariant().TrimStart('~', '/').Replace('.', '_').Replace('/', '_').Replace('-', '_');
          if (assembly.CreateInstance(typeName) is Page instance1)
          {
            instance = instance1;
            return true;
          }
        }
      }
      return false;
    }

    private void LoadNextAssembly(string assemblyPrefix, int counter)
    {
      string formattedAssemblyName = this.GetFormattedAssemblyName(assemblyPrefix, counter);
      try
      {
        Assembly assembly = Assembly.Load(formattedAssemblyName);
        MethodInfo method = assembly.GetType("SF.VersionMappings").GetMethod("GetCache", BindingFlags.Static | BindingFlags.NonPublic);
        if (!(method != (MethodInfo) null))
          return;
        foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) (method.Invoke((object) null, (object[]) null) as IDictionary<string, string>))
        {
          this.virtualPathToAssemblyMap.Add(keyValuePair.Key, assembly);
          this.virtualPathToVersionMap.Add(keyValuePair.Key, keyValuePair.Value);
        }
      }
      catch (FileNotFoundException ex)
      {
        this.assemblySequenceLoadError = true;
      }
    }

    private string GetFormattedAssemblyName(string assemblyPrefix, int counter) => string.Format("{0}.{1}", (object) assemblyPrefix, (object) counter);
  }
}
