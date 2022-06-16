// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.FileSystemResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Caching;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  internal class FileSystemResolver : IVirtualFileResolver
  {
    public CacheDependency GetCacheDependency(
      PathDefinition definition,
      string virtualPaht,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      if (virtualPathDependencies == null)
        return (CacheDependency) null;
      StringCollection stringCollection = (StringCollection) null;
      HttpServerUtilityBase server = SystemManager.CurrentHttpContext.Server;
      foreach (string virtualPathDependency in virtualPathDependencies)
      {
        string str = server.MapPath(virtualPathDependency);
        if (stringCollection == null)
          stringCollection = new StringCollection();
        stringCollection.Add(str);
      }
      if (stringCollection == null)
        return (CacheDependency) null;
      string[] strArray = new string[stringCollection.Count];
      stringCollection.CopyTo(strArray, 0);
      return new CacheDependency(strArray, utcStart);
    }

    public bool Exists(PathDefinition definition, string virtualPaht) => SystemManager.CurrentHttpContext != null && File.Exists(SystemManager.CurrentHttpContext.Server.MapPath(definition.ResourceLocation));

    public Stream Open(PathDefinition definition, string virtualPath) => (Stream) File.OpenRead(SystemManager.CurrentHttpContext.Server.MapPath(definition.ResourceLocation));
  }
}
