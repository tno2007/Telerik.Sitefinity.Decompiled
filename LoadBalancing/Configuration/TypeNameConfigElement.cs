// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Configuration.TypeNameConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.LoadBalancing.Configuration
{
  /// <summary>Represents full type name configuration elememnt.</summary>
  public class TypeNameConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.TypeNameConfigElement" /> class.
    /// </summary>
    internal TypeNameConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.TypeNameConfigElement" /> class.
    /// </summary>
    public TypeNameConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.TypeNameConfigElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal TypeNameConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
    public string Value
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }
  }
}
