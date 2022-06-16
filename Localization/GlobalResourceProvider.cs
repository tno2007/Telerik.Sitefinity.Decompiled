// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.GlobalResourceProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Represents global resource provider for Sitefinity’s text resources.
  /// </summary>
  public class GlobalResourceProvider : IResourceProvider
  {
    private readonly string classKey;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.GlobalResourceProvider" /> with the provided class ID or virtual path.
    /// </summary>
    /// <param name="classKey">A string representing a class key or virtual path.</param>
    public GlobalResourceProvider(string classKey) => this.classKey = classKey;

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
    public virtual object GetObject(string resourceKey, CultureInfo culture) => SystemManager.CurrentHttpContext == null ? (object) "Compile time value" : (object) Res.Get(this.classKey, resourceKey, culture);

    /// <summary>Gets an object to read resource values from a source.</summary>
    /// <returns>
    /// The <see cref="T:System.Resources.IResourceReader" /> associated with the current resource provider.
    /// </returns>
    public virtual IResourceReader ResourceReader => SystemManager.CurrentHttpContext == null ? (IResourceReader) null : (IResourceReader) new GlobalResourceReader(this.classKey);
  }
}
