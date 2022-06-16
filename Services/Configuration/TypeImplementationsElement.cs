// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.TypeImplementationsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A types that inherit/implement a, possibly abstract, type or interface; used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.
  /// </summary>
  public class TypeImplementationsElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsElement" /> class.
    /// </summary>
    /// <param name="parent">The parent config element.</param>
    public TypeImplementationsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsElement" /> class.
    /// </summary>
    /// <param name="parent">The parent config element.</param>
    public TypeImplementationsElement(ConfigElement parent, Type type)
      : base(parent)
    {
      this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TypeImplementationsElement" /> class.
    /// </summary>
    internal TypeImplementationsElement()
      : base(false)
    {
    }

    /// <summary>
    /// A types that inherit/implement a, possibly abstract, type or interface.
    /// </summary>
    [ConfigurationProperty("type", IsKey = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TypeImplementationsElementTypeDescription", Title = "TypeImplementationsElementTypeTitle")]
    public Type Type
    {
      get => (Type) this["type"];
      set => this["type"] = !value.IsAbstract ? (object) value : throw new ArgumentException("Abstract types, including interfaces, are not considerted implementation.");
    }
  }
}
