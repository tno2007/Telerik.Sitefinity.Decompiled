// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.IResourceProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web
{
  /// <summary>Defines the common properties of resource providers.</summary>
  public interface IResourceProvider
  {
    /// <summary>
    /// Attempts to get the requested resource by its name. If it succeeded the content of the resource will be available in the out parameter <paramref name="resourceContent" />.
    /// </summary>
    /// <param name="resourceName">The resource name.</param>
    /// <param name="resourceContent">Content of the resource.</param>
    /// <returns><c>true</c> if the provider is able to serve the resource, <c>false</c> if not.</returns>
    bool TryGetResourceContent(string resourceName, out string resourceContent);

    /// <summary>Gets the resource content by its name.</summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns></returns>
    string GetResourceContent(string resourceName);
  }
}
