// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LocalResourceProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Represents implementation of standard ASP.Net local resource provider.
  /// </summary>
  public class LocalResourceProvider : IResourceProvider
  {
    private string virtualPath;
    private Dictionary<string, string> cache = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.LocalResourceProvider" />
    /// </summary>
    /// <param name="virtualPath">The virtual path to the resource file.</param>
    public LocalResourceProvider(string virtualPath)
    {
      this.virtualPath = virtualPath;
      IDictionaryEnumerator enumerator = this.ResourceReader.GetEnumerator();
      while (enumerator.MoveNext())
        this.cache.Add((string) enumerator.Key, (string) enumerator.Value);
    }

    /// <summary>Returns a resource object for the key and culture.</summary>
    /// <returns>
    /// An <see cref="T:System.Object" /> that contains the resource value for the <paramref name="resourceKey" /> and <paramref name="culture" />.
    /// </returns>
    /// <param name="resourceKey">
    /// The key identifying a particular resource.
    /// </param>
    /// <param name="culture">
    /// The culture identifying a localized value for the resource.
    /// </param>
    public virtual object GetObject(string resourceKey, CultureInfo culture)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      string str;
      if (this.cache.TryGetValue(resourceKey + culture.Name, out str))
        return (object) str;
      while (!culture.Equals((object) CultureInfo.InvariantCulture))
      {
        culture = culture.Parent;
        if (this.cache.TryGetValue(resourceKey + culture.Name, out str))
          return (object) str;
      }
      throw new KeyNotFoundException(Res.Get<ErrorMessages>("KeyNotPresentInLocalResources", (object) resourceKey));
    }

    /// <summary>Gets an object to read resource values from a source.</summary>
    /// <returns>
    /// The <see cref="T:System.Resources.IResourceReader" /> associated with the current resource provider.
    /// </returns>
    public virtual IResourceReader ResourceReader => (IResourceReader) new LocalResourceReader(this.virtualPath, ".resx");
  }
}
