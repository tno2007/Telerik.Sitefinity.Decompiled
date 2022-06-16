// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.IPropertySerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Defines the common contract for components capable of serializing control properties from and to string.
  /// </summary>
  public interface IPropertySerializer
  {
    /// <summary>Serializes the proxy property into a string.</summary>
    /// <param name="proxyProperty">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that contains the
    /// properties of the MVC controller.
    /// </param>
    /// <returns>A string that represents serialized properties.</returns>
    string SerializeProperties(ControlProperty proxyProperty);

    /// <summary>
    /// Deserializes the serialized properties and sets them on the instance of
    /// the object being proxied by the <see cref="!:MvcProxyBase" />.
    /// </summary>
    /// <param name="serializedProperties">
    /// String representing the serialized properties.
    /// </param>
    /// <param name="proxyInstance">
    /// Instance of the object on which the properties should be set, once deserialized.
    /// </param>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "proxied is spelled correctly.")]
    void DeserializeProperties(string serializedProperties, object proxyInstance);
  }
}
