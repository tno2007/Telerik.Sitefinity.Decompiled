// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.PathDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Web.Caching;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  [DebuggerDisplay("{VirtualPath}, Resolver={ResolverName}, ResourceLocation={ResourceLocation}")]
  public class PathDefinition
  {
    private NameValueCollection parameteres;
    private IDictionary<string, object> items;

    public string VirtualPath { get; set; }

    public string ResourceLocation { get; set; }

    public string ResolverName { get; set; }

    public bool IsWildcard { get; set; }

    public NameValueCollection Parameters
    {
      get
      {
        if (this.parameteres == null)
          this.parameteres = new NameValueCollection();
        return this.parameteres;
      }
      set => this.parameteres = value;
    }

    public IDictionary<string, object> Items
    {
      get
      {
        if (this.items == null)
          this.items = (IDictionary<string, object>) new Dictionary<string, object>();
        return this.items;
      }
    }

    public bool FileExists(string virtualPath) => ObjectFactory.Resolve<IVirtualFileResolver>(this.ResolverName).Exists(this, virtualPath);

    public Stream OpenFile(string virtualPath) => ObjectFactory.Resolve<IVirtualFileResolver>(this.ResolverName).Open(this, virtualPath);

    public CacheDependency GetCacheDependency(
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      return ObjectFactory.Resolve<IVirtualFileResolver>(this.ResolverName).GetCacheDependency(this, virtualPath, virtualPathDependencies, utcStart);
    }

    /// <summary>Returns a hash of the specified virtual paths.</summary>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
    /// <returns>A hash of the specified virtual paths.</returns>
    public virtual string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies) => ObjectFactory.Resolve<IVirtualFileResolver>(this.ResolverName) is IHashedVirtualFileResolver virtualFileResolver ? virtualFileResolver.GetFileHash(this, virtualPath, virtualPathDependencies) : (string) null;
  }
}
