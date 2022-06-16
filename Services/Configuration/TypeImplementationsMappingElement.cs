// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.TypeImplementationsMappingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Maps a, possibly abstract, type or interface to a list of types that inherit/implement it. This setting is used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.
  /// </summary>
  public class TypeImplementationsMappingElement : ConfigElement, IEnumerable<Type>, IEnumerable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsMappingElement" /> class.
    /// </summary>
    /// <param name="parent">The parent config element.</param>
    public TypeImplementationsMappingElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsMappingElement" /> class.
    /// </summary>
    /// <param name="parent">The parent config element.</param>
    public TypeImplementationsMappingElement(ConfigElement parent, Type type)
      : base(parent)
    {
      this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsMappingElement" /> class.
    /// </summary>
    internal TypeImplementationsMappingElement()
      : base(false)
    {
    }

    /// <summary>A, possibly abstract, type or interface.</summary>
    [ConfigurationProperty("type", IsKey = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TypeImplementationsMappingElementTypeDescription", Title = "TypeImplementationsMappingElementTypeTitle")]
    public Type Type
    {
      get => (Type) this["type"];
      set => this["type"] = (object) value;
    }

    /// <summary>
    /// List of types that inherit/implement the type specified in the <see cref="P:Telerik.Sitefinity.Services.TypeImplementationsMappingElement.Type" /> property.
    /// </summary>
    [ConfigurationProperty("implementations")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TypeImplementationsMappingElementImplementationsDescription", Title = "TypeImplementationsMappingElementImplementationsTitle")]
    public ConfigElementList<TypeImplementationsElement> Implementations => (ConfigElementList<TypeImplementationsElement>) this["implementations"];

    /// <summary>Adds a new implementation type.</summary>
    /// <param name="implementationType">Implementation type.</param>
    public void Add(Type implementationType) => this.Implementations.Add(new TypeImplementationsElement((ConfigElement) this.Implementations, implementationType));

    public IEnumerator<Type> GetEnumerator() => this.Implementations.Select<TypeImplementationsElement, Type>((Func<TypeImplementationsElement, Type>) (i => i.Type)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
