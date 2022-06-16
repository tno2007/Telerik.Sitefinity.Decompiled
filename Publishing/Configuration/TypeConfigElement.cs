// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Configuration.TypeConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Publishing.Configuration
{
  public class TypeConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Configuration.TypeConfigElement" /> class.
    /// </summary>
    internal TypeConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Configuration.TypeConfigElement" /> class.
    /// </summary>
    public TypeConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Configuration.TypeConfigElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal TypeConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
    public string FullName
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }

    /// <summary>Gets or sets the assembly qualified name.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("assemblyQualifiedName", IsKey = false, IsRequired = true)]
    public string AssemblyQualifiedName
    {
      get => (string) this["assemblyQualifiedName"];
      set => this["assemblyQualifiedName"] = (object) value;
    }
  }
}
