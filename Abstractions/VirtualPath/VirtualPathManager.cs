// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.VirtualPathManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.Caching;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  public static class VirtualPathManager
  {
    private static volatile bool initialized;
    private static IList<PathDefinition> wildcardPaths;
    private static IDictionary<string, PathDefinition> virtualPaths;
    private static object syncLock = new object();

    static VirtualPathManager() => SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(VirtualPathManager.System_ShuttingDown);

    public static CacheDependency GetCacheDependency(
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      string resPath;
      PathDefinition virtualPathDefinition = VirtualPathManager.GetVirtualPathDefinition(virtualPath, out resPath);
      if (virtualPathDefinition == null)
      {
        foreach (string virtualPathDependency in virtualPathDependencies)
        {
          virtualPathDefinition = VirtualPathManager.GetVirtualPathDefinition(virtualPathDependency, out resPath);
          if (virtualPathDefinition != null)
            break;
        }
      }
      if (virtualPathDefinition != null)
        return virtualPathDefinition.GetCacheDependency(resPath, virtualPathDependencies, utcStart);
      if (virtualPath.Equals("~/_appstart.cshtml", StringComparison.InvariantCultureIgnoreCase) || virtualPath.Equals("~/_appstart.vbhtml", StringComparison.InvariantCultureIgnoreCase))
        return (CacheDependency) null;
      throw new ArgumentException("File \"{0}\" does not exist.".Arrange((object) UrlPath.ResolveUrl(virtualPath)), nameof (virtualPath));
    }

    public static bool FileExists(string virtualPath)
    {
      string virtualPath1 = UrlPath.ResolveUrl(virtualPath);
      PathDefinition definition = VirtualPathManager.GetDefinition(virtualPath1);
      return definition != null && definition.FileExists(virtualPath1);
    }

    public static Stream OpenFile(string virtualPath)
    {
      string virtualPath1 = UrlPath.ResolveUrl(virtualPath);
      return (VirtualPathManager.GetDefinition(virtualPath1) ?? throw new ArgumentException("File \"{0}\" does not exist.".Arrange((object) virtualPath1), nameof (virtualPath))).OpenFile(virtualPath1);
    }

    /// <summary>Returns a hash of the specified virtual paths.</summary>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
    /// <returns>A hash of the specified virtual paths.</returns>
    public static string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
    {
      string virtualPath1 = UrlPath.ResolveUrl(virtualPath);
      return VirtualPathManager.GetDefinition(virtualPath1)?.GetFileHash(virtualPath1, virtualPathDependencies);
    }

    /// <summary>Adds the virtual path.</summary>
    /// <typeparam name="TResolver">The type of the resolver. It has to implement IVirtualFileResolver.</typeparam>
    /// <param name="virtualPath">The virtual path.</param>
    /// <param name="resolverName">Name of the resolver.</param>
    /// <param name="resourceLocation">The resource location. Usually an assembly name that the resolver may interpret.</param>
    /// <param name="parameters">Additional parameters that the resolver may interpret.</param>
    public static void AddVirtualFileResolver<TResolver>(
      string virtualPath,
      string resolverName,
      string resourceLocation = null,
      NameValueCollection parameters = null)
      where TResolver : IVirtualFileResolver
    {
      if (resolverName == null)
        throw new ArgumentNullException(nameof (resolverName));
      if (resolverName.IsNullOrWhitespace())
        throw new ArgumentOutOfRangeException(nameof (resolverName), "resolverName parameter cannot be empty.");
      if (!VirtualPathManager.initialized)
        VirtualPathManager.Initialize();
      ObjectFactory.Container.RegisterType<IVirtualFileResolver, TResolver>(resolverName, (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      VirtualPathManager.AddVirtualPath(VirtualPathManager.CreatePathDefinition(virtualPath, typeof (TResolver), resolverName, resourceLocation, parameters));
    }

    /// <summary>
    /// Removes the virtual file resolver that has the specified virtual path.
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    public static void RemoveVirtualFileResolver(string virtualPath)
    {
      bool isWildcard;
      string resPath = VirtualPathManager.DefinitionVirtualPath(virtualPath, out isWildcard);
      if (isWildcard)
      {
        PathDefinition pathDefinition = VirtualPathManager.wildcardPaths.FirstOrDefault<PathDefinition>((Func<PathDefinition, bool>) (p => string.Equals(p.VirtualPath, resPath, StringComparison.OrdinalIgnoreCase)));
        if (pathDefinition == null)
          return;
        VirtualPathManager.wildcardPaths.Remove(pathDefinition);
      }
      else
      {
        if (!VirtualPathManager.virtualPaths.ContainsKey(resPath))
          return;
        VirtualPathManager.virtualPaths.Remove(resPath);
      }
    }

    private static void Initialize()
    {
      lock (VirtualPathManager.syncLock)
      {
        if (VirtualPathManager.initialized)
          return;
        VirtualPathManager.virtualPaths = (IDictionary<string, PathDefinition>) new Dictionary<string, PathDefinition>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        VirtualPathManager.wildcardPaths = (IList<PathDefinition>) new List<PathDefinition>();
        foreach (VirtualPathElement virtualPath in (ConfigElementCollection) Config.Get<VirtualPathSettingsConfig>().VirtualPaths)
          VirtualPathManager.AddVirtualPath(VirtualPathManager.CreatePathDefinition(virtualPath.VirtualPath, virtualPath));
        VirtualPathManager.initialized = true;
      }
    }

    private static void AddVirtualPath(PathDefinition definition)
    {
      if (definition.IsWildcard)
        VirtualPathManager.wildcardPaths.Add(definition);
      else
        VirtualPathManager.virtualPaths.Add(definition.VirtualPath, definition);
    }

    private static PathDefinition GetDefinition(string virtualPath)
    {
      if (!VirtualPathManager.initialized)
        VirtualPathManager.Initialize();
      PathDefinition definition;
      if (!VirtualPathManager.virtualPaths.TryGetValue(virtualPath, out definition))
        definition = VirtualPathManager.wildcardPaths.FirstOrDefault<PathDefinition>((Func<PathDefinition, bool>) (w => virtualPath.StartsWith(w.VirtualPath, StringComparison.OrdinalIgnoreCase)));
      return definition;
    }

    private static PathDefinition CreatePathDefinition(
      string virtualPath,
      VirtualPathElement config)
    {
      return VirtualPathManager.CreatePathDefinition(virtualPath, config.ResolverType, config.ResolverName, config.ResourceLocation, config.Parameters);
    }

    private static PathDefinition CreatePathDefinition(
      string virtualPath,
      Type resolverType,
      string resolverName,
      string resourceLocaton,
      NameValueCollection paramters)
    {
      if (resolverType != (Type) null && !ObjectFactory.IsTypeRegistered(typeof (IVirtualFileResolver), resolverType, resolverName, true))
        ObjectFactory.Container.RegisterType(typeof (IVirtualFileResolver), resolverType, resolverName, (LifetimeManager) new ContainerControlledLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      bool isWildcard;
      string str = VirtualPathManager.DefinitionVirtualPath(virtualPath, out isWildcard);
      return new PathDefinition()
      {
        IsWildcard = isWildcard,
        VirtualPath = str,
        ResolverName = resolverName,
        ResourceLocation = resourceLocaton,
        Parameters = paramters != null ? new NameValueCollection(paramters) : (NameValueCollection) null
      };
    }

    private static string DefinitionVirtualPath(string virtualPath, out bool isWildcard)
    {
      isWildcard = virtualPath.EndsWith("*");
      return UrlPath.ResolveUrl(isWildcard ? virtualPath.Left(virtualPath.Length - 1) : virtualPath);
    }

    private static void System_ShuttingDown(object sender, CancelEventArgs e) => VirtualPathManager.Reset();

    public static void Reset()
    {
      lock (VirtualPathManager.syncLock)
      {
        if (!VirtualPathManager.initialized)
          return;
        VirtualPathManager.virtualPaths = (IDictionary<string, PathDefinition>) null;
        VirtualPathManager.wildcardPaths = (IList<PathDefinition>) null;
        VirtualPathManager.initialized = false;
      }
    }

    internal static PathDefinition GetVirtualPathDefinition(
      string virtualPath,
      out string resPath)
    {
      resPath = UrlPath.ResolveUrl(virtualPath);
      return VirtualPathManager.GetDefinition(resPath) ?? (PathDefinition) null;
    }
  }
}
