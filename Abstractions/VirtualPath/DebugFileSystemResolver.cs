// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.DebugFileSystemResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  internal class DebugFileSystemResolver : IVirtualFileResolver
  {
    private static readonly object syncLock = new object();
    private static readonly ConcurrentDictionary<string, string> pathCache = new ConcurrentDictionary<string, string>();

    public bool Exists(PathDefinition definition, string virtualPath) => File.Exists(DebugFileSystemResolver.GetResourceFileSystemPath(definition, virtualPath));

    public Stream Open(PathDefinition definition, string virtualPaht) => (Stream) new FileStream(DebugFileSystemResolver.GetResourceFileSystemPath(definition, virtualPaht), FileMode.Open, FileAccess.Read);

    public CacheDependency GetCacheDependency(
      PathDefinition definition,
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      return new CacheDependency(DebugFileSystemResolver.GetResourceFileSystemPath(definition, virtualPath));
    }

    private static Assembly GetAssembly(PathDefinition definition, string resName)
    {
      object assembly1;
      if (!definition.Items.TryGetValue("Assembly", out assembly1))
      {
        lock (DebugFileSystemResolver.syncLock)
        {
          if (!definition.Items.TryGetValue("Assembly", out assembly1))
          {
            string[] strArray = definition.ResourceLocation.Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 2)
            {
              assembly1 = (object) Assembly.Load(strArray[1].Trim());
              definition.ResourceLocation = strArray[0].Trim();
            }
            else if (strArray.Length == 1)
            {
              assembly1 = (object) Assembly.Load(strArray[0].Trim());
            }
            else
            {
              foreach (Assembly assembly2 in ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (x => x.GetName().Name.StartsWith("Telerik.Sitefinity"))))
              {
                if (((IEnumerable<string>) assembly2.GetManifestResourceNames()).Any<string>((Func<string, bool>) (x => x == resName)))
                  return assembly2;
              }
              throw new InvalidOperationException("Invalid PathDefinition.");
            }
            definition.Items.Add("Assembly", assembly1);
          }
        }
      }
      return (Assembly) assembly1;
    }

    internal static string GetResourceFileSystemPath(PathDefinition definition, string virtualPath)
    {
      string path = (string) null;
      virtualPath = virtualPath.TrimStart('~');
      if (!DebugFileSystemResolver.pathCache.TryGetValue(virtualPath, out path))
      {
        Assembly asm;
        string resourceName = DebugFileSystemResolver.GetResourceName(definition, virtualPath, out asm);
        if (asm == (Assembly) null)
          asm = DebugFileSystemResolver.GetAssembly(definition, resourceName);
        string defaultNamespace = asm.GetDefaultNamespace();
        string extension = VirtualPathUtility.GetExtension(virtualPath);
        string str = DebugFileSystemResolver.GetSolutionDir() + "\\" + asm.GetName().Name + "\\";
        string[] strArray = resourceName.Replace(defaultNamespace, string.Empty).Replace(extension, string.Empty).Trim().Split('.');
        bool flag = true;
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (flag && Directory.Exists(str + strArray[index]))
          {
            str = str + strArray[index] + "\\";
          }
          else
          {
            flag = false;
            str = str + strArray[index] + ".";
          }
        }
        path = str.TrimEnd('.') + extension;
        if (!File.Exists(path))
          path = DebugFileSystemResolver.HandleExceptionalScenarios(resourceName, defaultNamespace, extension);
        DebugFileSystemResolver.pathCache.TryAdd(virtualPath, path);
      }
      return path;
    }

    private static string HandleExceptionalScenarios(
      string resName,
      string defaultNamespace,
      string extension)
    {
      return defaultNamespace.Equals("Telerik.Sitefinity.Analytics.Server.Infrastructure") ? DebugFileSystemResolver.GetSolutionDir() + "\\Analytics\\Telerik.Sitefinity.Analytics" + "\\" + resName.Replace(defaultNamespace, string.Empty).Replace(extension, string.Empty).Replace(".", "\\") + extension : (string) null;
    }

    private static string GetSolutionDir()
    {
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      return baseDirectory.Substring(0, baseDirectory.LastIndexOf("\\SitefinityWebApp"));
    }

    private static string GetResourceName(
      PathDefinition definition,
      string virtualPath,
      out Assembly asm)
    {
      asm = (Assembly) null;
      if (virtualPath.StartsWith("~/SfCtrlPresentation/".TrimStart('~')))
      {
        string[] strArray1 = virtualPath.Split(new char[1]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries);
        string str;
        if (ControlPresentationResolver.HashToControlPathCache.TryGetValue(strArray1[strArray1.Length - 2], out str))
        {
          string[] strArray2 = str.Split(new char[1]{ '/' }, StringSplitOptions.RemoveEmptyEntries);
          Type type = TypeResolutionService.ResolveType(strArray2[0], false);
          if (type != (Type) null)
            asm = type.Assembly;
          return strArray2[strArray2.Length - 1];
        }
      }
      return definition.IsWildcard ? virtualPath.Substring(definition.VirtualPath.Length) : definition.ResourceLocation;
    }
  }
}
