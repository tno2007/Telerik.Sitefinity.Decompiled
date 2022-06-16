// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.IHashedVirtualFileResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Classes that implement this interface should be virtual file resolvers with ability to compute a hash for a given virtual file.
  /// </summary>
  public interface IHashedVirtualFileResolver : IVirtualFileResolver
  {
    /// <summary>Returns a hash of the specified virtual paths.</summary>
    /// <param name="definition">The file resolver definition.</param>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
    /// <returns>A hash of the specified virtual paths.</returns>
    string GetFileHash(
      PathDefinition definition,
      string virtualPath,
      IEnumerable virtualPathDependencies);
  }
}
