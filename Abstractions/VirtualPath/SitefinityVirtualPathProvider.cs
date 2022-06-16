// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.SitefinityVirtualPathProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Provides a set of methods that enable a Web application to retrieve resources
  /// from a virtual file system.
  /// </summary>
  public class SitefinityVirtualPathProvider : VirtualPathProvider
  {
    /// <summary>
    /// Gets a value that indicates whether a file exists in the virtual file system.
    /// </summary>
    /// <param name="virtualPath">The path to the virtual file.</param>
    /// <returns>
    /// true if the file exists in the virtual file system; otherwise, false.
    /// </returns>
    public override bool FileExists(string virtualPath) => this.Previous.FileExists(virtualPath) || VirtualPathManager.FileExists(virtualPath);

    /// <summary>Gets a virtual file from the virtual file system.</summary>
    /// <param name="virtualPath">The path to the virtual file.</param>
    /// <returns>
    /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual file system.
    /// </returns>
    public override VirtualFile GetFile(string virtualPath) => this.Previous.FileExists(virtualPath) ? this.Previous.GetFile(virtualPath) : (VirtualFile) new SitefinityVirtualFile(virtualPath);

    /// <summary>
    /// Gets a value that indicates whether a directory exists in the virtual file system.
    /// </summary>
    /// <param name="virtualDir">The path to the virtual directory.</param>
    /// <returns>
    /// true if the directory exists in the virtual file system; otherwise, false.
    /// </returns>
    public override bool DirectoryExists(string virtualDir) => this.Previous.DirectoryExists(virtualDir);

    /// <summary>
    /// Creates a cache dependency based on the specified virtual paths.
    /// </summary>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
    /// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.
    /// </returns>
    public override CacheDependency GetCacheDependency(
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      if (this.Previous != null)
      {
        if (this.Previous.FileExists(virtualPath))
        {
          IEnumerator enumerator = virtualPathDependencies?.GetEnumerator();
          string str = enumerator == null || !enumerator.MoveNext() ? (string) null : enumerator.Current as string;
          if (str == null || !str.StartsWith("~/SFPageService/") && !str.StartsWith("~/SFMVCPageService/"))
            return this.Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
        if (this.Previous.DirectoryExists(virtualPath))
          return this.Previous.GetCacheDependency(virtualPath, (IEnumerable) new string[1]
          {
            virtualPath
          }, utcStart);
      }
      return VirtualPathManager.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
    }

    /// <summary>
    /// Gets a virtual directory from the virtual file system.
    /// </summary>
    /// <param name="virtualDir">The path to the virtual directory.</param>
    /// <returns>
    /// A descendent of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class that represents a directory in the virtual file system.
    /// </returns>
    public override VirtualDirectory GetDirectory(string virtualDir) => this.Previous.GetDirectory(virtualDir);

    /// <summary>Returns a hash of the specified virtual paths.</summary>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
    /// <returns>A hash of the specified virtual paths.</returns>
    public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies) => this.Previous.FileExists(virtualPath) ? this.Previous.GetFileHash(virtualPath, virtualPathDependencies) : VirtualPathManager.GetFileHash(virtualPath, virtualPathDependencies) ?? base.GetFileHash(virtualPath, virtualPathDependencies);
  }
}
