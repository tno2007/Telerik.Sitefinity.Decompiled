// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceFileProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>A resource file provider that can server resources</summary>
  public class ResourceFileProvider : IResourceProvider
  {
    /// <summary>The resource cache.</summary>
    private static readonly ConcurrentDictionary<string, string> resourcesPathsCache = new ConcurrentDictionary<string, string>();

    /// <summary>
    /// Attempts to get the requested resource by its name. If it succeeded the content of the resource will be available in the out parameter <paramref name="resourceContent" />.
    /// </summary>
    /// <param name="resourceName">The resource name.</param>
    /// <param name="resourceContent">Content of the resource.</param>
    /// <returns>
    /// <c>true</c> if the provider is able to serve the resource, <c>false</c> if not.
    /// </returns>
    public bool TryGetResourceContent(string resourceName, out string resourceContent)
    {
      try
      {
        resourceContent = this.GetResourceContent(resourceName);
        return true;
      }
      catch (ArgumentException ex)
      {
        resourceContent = (string) null;
        return false;
      }
    }

    /// <summary>Gets the resource content by its name.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns></returns>
    public string GetResourceContent(string resourceName)
    {
      Stream stream = (Stream) null;
      try
      {
        if (this.IsResourceModuleLoaded(SystemManager.CurrentHttpContext))
          stream = this.GetPhysicalFile(resourceName, stream);
        if (stream == null)
          stream = VirtualPathManager.OpenFile(resourceName);
        if (stream == null)
          throw new FileNotFoundException(string.Format("File {0} was not found.", (object) resourceName));
        using (StreamReader streamReader = new StreamReader(stream, true))
          return streamReader.ReadToEnd();
      }
      finally
      {
        stream?.Dispose();
      }
    }

    internal bool IsResourceModuleLoaded(HttpContextBase context) => ((IEnumerable<string>) context.ApplicationInstance.Modules.AllKeys).Any<string>((Func<string, bool>) (x => x == "ResourceModule"));

    internal Stream GetPhysicalFile(string resourceName, Stream fileStream)
    {
      string path;
      if (!ResourceFileProvider.resourcesPathsCache.TryGetValue(resourceName, out path))
      {
        string resourcePath;
        fileStream = ResourceHttpModule.GetWebResourceStream("Telerik.Sitefinity", resourceName, out resourcePath);
        if (fileStream != null)
          ResourceFileProvider.resourcesPathsCache.TryAdd(resourceName, resourcePath);
      }
      else
        fileStream = (Stream) new FileStream(path, FileMode.Open, FileAccess.Read);
      return fileStream;
    }
  }
}
