// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.EmbeddedResourceResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Caching;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  internal class EmbeddedResourceResolver : IVirtualFileResolver
  {
    public CacheDependency GetCacheDependency(
      PathDefinition definition,
      string virtualPaht,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      return (CacheDependency) null;
    }

    public bool Exists(PathDefinition definition, string virtualPaht)
    {
      Assembly assembly = this.GetAssembly(definition);
      string resourceName = this.GetResourceName(definition, virtualPaht);
      return ((IEnumerable<string>) assembly.GetManifestResourceNames()).Contains<string>(resourceName);
    }

    public Stream Open(PathDefinition definition, string virtualPaht) => this.GetAssembly(definition).GetManifestResourceStream(this.GetResourceName(definition, virtualPaht));

    protected virtual string GetResourceName(PathDefinition definition, string virtualPath) => definition.IsWildcard ? virtualPath.Substring(definition.VirtualPath.Length) : definition.ResourceLocation;

    protected virtual Assembly GetAssembly(PathDefinition definition)
    {
      object assembly;
      if (!definition.Items.TryGetValue("Assembly", out assembly))
      {
        lock (this)
        {
          if (!definition.Items.TryGetValue("Assembly", out assembly))
          {
            string[] strArray = definition.ResourceLocation.Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 2)
            {
              assembly = (object) Assembly.Load(strArray[1].Trim());
              definition.ResourceLocation = strArray[0].Trim();
            }
            else
              assembly = strArray.Length == 1 ? (object) Assembly.Load(strArray[0].Trim()) : throw new InvalidOperationException("Invalid PathDefinition.");
            definition.Items.Add("Assembly", assembly);
          }
        }
      }
      return (Assembly) assembly;
    }
  }
}
