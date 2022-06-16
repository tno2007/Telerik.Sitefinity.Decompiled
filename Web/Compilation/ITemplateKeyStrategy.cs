// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.DefaultTemplateKeyStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Compilation
{
  internal class DefaultTemplateKeyStrategy : ITemplateKeyStrategy
  {
    private ISet<string> processed = (ISet<string>) new HashSet<string>();

    public ISet<string> GetKeys()
    {
      HashSet<string> virtualPaths = new HashSet<string>();
      this.LoadVirtualPathsFromSitefinity((ISet<string>) virtualPaths);
      this.LoadVirtualPathsFromModules((ISet<string>) virtualPaths);
      this.LoadVirtualPahtsFromConfig((ISet<string>) virtualPaths);
      return (ISet<string>) virtualPaths;
    }

    private void LoadVirtualPathsFromSitefinity(ISet<string> virtualPaths)
    {
      foreach (string str in ((IEnumerable<string>) Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceNames()).Where<string>((Func<string, bool>) (x => x.EndsWith(".ascx"))).Select<string, string>((Func<string, string>) (x => "~/SFRes/" + x)))
        virtualPaths.Add(str);
      this.processed.Add("~/SFRes/*");
      this.processed.Add("~/ExtRes/*");
    }

    private void LoadVirtualPathsFromModules(ISet<string> virtualPaths)
    {
      foreach (KeyValuePair<string, IModule> applicationModule in (IEnumerable<KeyValuePair<string, IModule>>) SystemManager.ApplicationModules)
      {
        if (applicationModule.Value is ModuleBase moduleBase)
        {
          IDictionary<string, Action<VirtualPathElement>> virtualPathsInternal = moduleBase.GetVirtualPathsInternal();
          if (virtualPathsInternal != null)
          {
            foreach (KeyValuePair<string, Action<VirtualPathElement>> keyValuePair in (IEnumerable<KeyValuePair<string, Action<VirtualPathElement>>>) virtualPathsInternal)
            {
              VirtualPathElement element = new VirtualPathElement();
              if (keyValuePair.Value != null)
              {
                keyValuePair.Value(element);
              }
              else
              {
                element.ResolverName = typeof (EmbeddedResourceResolver).Name;
                element.ResourceLocation = moduleBase.GetType().Assembly.GetName().FullName;
                element.VirtualPath = keyValuePair.Key;
              }
              if (string.Equals(element.ResolverName, typeof (EmbeddedResourceResolver).Name))
              {
                if (string.IsNullOrEmpty(element.VirtualPath))
                  element.VirtualPath = keyValuePair.Key;
                this.ExtractVirtualPathsFromAssembly(virtualPaths, element);
              }
            }
          }
        }
      }
    }

    private void LoadVirtualPahtsFromConfig(ISet<string> virtualPaths)
    {
      foreach (VirtualPathElement virtualPath in (ConfigElementCollection) Config.Get<VirtualPathSettingsConfig>().VirtualPaths)
        this.ExtractVirtualPathsFromAssembly(virtualPaths, virtualPath);
    }

    private void ExtractVirtualPathsFromAssembly(
      ISet<string> virtualPaths,
      VirtualPathElement element)
    {
      if (this.processed.Contains(element.VirtualPath))
        return;
      this.processed.Add(element.VirtualPath);
      string resourceLocation = element.ResourceLocation;
      if (string.IsNullOrEmpty(resourceLocation))
        return;
      if (resourceLocation.IndexOf(",") != -1)
        return;
      try
      {
        Assembly assembly = Assembly.Load(resourceLocation);
        string virtualPathStart = element.VirtualPath.TrimEnd('*');
        foreach (string str in ((IEnumerable<string>) assembly.GetManifestResourceNames()).Where<string>((Func<string, bool>) (x => x.EndsWith(".ascx"))).Select<string, string>((Func<string, string>) (x => virtualPathStart + x)))
          virtualPaths.Add(str);
      }
      catch (FileNotFoundException ex)
      {
        if (!Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      catch (FileLoadException ex)
      {
        if (!Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }
  }
}
