// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigValue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// A non-generic type to contain the <see cref="M:Telerik.Sitefinity.Configuration.ConfigValue.Create``1(``0,Telerik.Sitefinity.Configuration.ConfigElement)" /> factory method.
  /// Using the same pattern as in <see cref="T:System.Tuple" />.
  /// </summary>
  public static class ConfigValue
  {
    /// <summary>
    /// A generic factory method, to leverage compiler's type inference
    /// and allow omitting generic type parameters.
    /// </summary>
    /// <typeparam name="T">The inferred value type.</typeparam>
    /// <param name="value">The value of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" /> element.</param>
    /// <param name="parent">The parent config element.</param>
    public static ConfigValue<T> Create<T>(T value, ConfigElement parent) => new ConfigValue<T>(value, parent);

    /// <summary>
    /// Adds a primitive value wrapped in a <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" />
    /// to a <see cref="!:ConfigElementList" /> of <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" />,
    /// inferring the type and setting the parent automatically.
    /// </summary>
    /// <typeparam name="T">The inferred value type.</typeparam>
    /// <param name="collection">The list to add to.</param>
    /// <param name="value">The primitive value to add.</param>
    /// <example>
    /// This method allows the following code
    /// <code>
    /// this.ValueElementList.Add(ConfigValue.Create(42, this.ValueElementList));
    /// </code>
    /// to be expressed much shorter simply as
    /// <code>
    /// this.ValueElementList.Add(42);
    /// </code>
    /// </example>
    public static void Add<T>(this ConfigElementList<ConfigValue<T>> list, T value) => list.Add(ConfigValue.Create<T>(value, (ConfigElement) list));

    /// <summary>
    /// Adds a primitive value wrapped in a <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" />
    /// to a <see cref="!:ConfigElementDictionary" /> of <see cref="T:Telerik.Sitefinity.Configuration.ConfigValue`1" />,
    /// inferring the types and setting the parent automatically.
    /// </summary>
    /// <typeparam name="K">The inferred key type.</typeparam>
    /// <typeparam name="K">The inferred value type.</typeparam>
    /// <param name="collection">The dictionary to add to.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The primitive value.</param>
    /// <example>
    /// This method allows the following code
    /// <code>
    /// this.ValueElementList.Add("answer", ConfigValue.Create(42, this.ValueElementList));
    /// </code>
    /// to be expressed much shorter simply as
    /// <code>
    /// this.ValueElementList.Add("answer", 42);
    /// </code>
    /// </example>
    public static void Add<K, V>(
      this ConfigElementDictionary<K, ConfigValue<V>> dictionary,
      K key,
      V value)
    {
      dictionary.Add(key, ConfigValue.Create<V>(value, (ConfigElement) dictionary));
    }
  }
}
