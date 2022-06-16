// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigValue`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// <para>
  /// A config element representing a single value.
  /// </para>
  /// <para>
  /// This is needed as all Sitefinity configuration collections (e.g. <see cref="!:ConfigElementList" />) work with descendants of
  /// <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> and all primitive values need to be wrapped in such.
  /// </para>
  /// </summary>
  public class ConfigValue<T> : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue" /> class.
    /// </summary>
    /// <param name="parent">The parent config element.</param>
    public ConfigValue(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue" /> class.
    /// </summary>
    /// <param name="value">The wrapped value.</param>
    /// <param name="parent">The parent config element.</param>
    public ConfigValue(T value, ConfigElement parent)
      : base(parent)
    {
      this.Value = value;
    }

    /// <summary>The wrapped value.</summary>
    [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
    public T Value
    {
      get => (T) this["value"];
      set => this[nameof (value)] = (object) value;
    }

    /// <summary>
    /// Implicitly converts a <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" /> to the underlying value.
    /// </summary>
    /// <param name="element">The <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" /> instance to convert.</param>
    /// <returns>The underlying <see cref="P:Telerik.Sitefinity.Configuration.ConfigValue`1.Value" />.</returns>
    public static implicit operator T(ConfigValue<T> element) => element.Value;
  }
}
