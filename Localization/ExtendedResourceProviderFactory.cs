// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ExtendedResourceProviderFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Compilation;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Serves as factory class for creating resource providers.
  /// </summary>
  public class ExtendedResourceProviderFactory : ResourceProviderFactory
  {
    /// <summary>Creates a global resource provider.</summary>
    /// <returns>
    /// An <see cref="T:System.Web.Compilation.IResourceProvider" />.
    /// </returns>
    /// <param name="classKey">The name of the resource class.</param>
    public override IResourceProvider CreateGlobalResourceProvider(string classKey) => (IResourceProvider) new GlobalResourceProvider(classKey);

    /// <summary>Creates a local resource provider.</summary>
    /// <returns>
    /// An <see cref="T:System.Web.Compilation.IResourceProvider" />.
    /// </returns>
    /// <param name="virtualPath">The path to a resource file.</param>
    public override IResourceProvider CreateLocalResourceProvider(
      string virtualPath)
    {
      return (IResourceProvider) new LocalResourceProvider(virtualPath);
    }
  }
}
