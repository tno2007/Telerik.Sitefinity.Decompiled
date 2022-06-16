// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigValueDictionary
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents a dictionary of configuration elments, mapping string keys to string values.
  /// </summary>
  public class ConfigValueDictionary : 
    ConfigElementDictionary<string, KeyValueConfigElement>,
    IDictionary<string, string>,
    ICollection<KeyValuePair<string, string>>,
    IEnumerable<KeyValuePair<string, string>>,
    IEnumerable
  {
    internal ConfigValueDictionary()
    {
    }

    /// <summary>
    /// Initalizes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigValueDictionary" /> class.
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    public ConfigValueDictionary(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initalizes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigValueDictionary" /> class.
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <param name="keyComparer">The equality comparer to use when comparing keys.</param>
    public ConfigValueDictionary(ConfigElement parent, IEqualityComparer<string> keyComparer)
      : base(parent, keyComparer)
    {
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <returns>
    /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.
    /// </returns>
    /// <param name="key">The key of the value to get or set.</param>
    public string this[string key]
    {
      get => base[key].Value;
      set => this[key] = this.NewItem(key, value);
    }

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    /// <returns>A collection containing the values in the dictionary.</returns>
    public ICollection<string> Values => (ICollection<string>) base.Values.Select<KeyValueConfigElement, string>((Func<KeyValueConfigElement, string>) (kv => kv.Value)).ToList<string>();

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <returns><c>true</c>, if the dictionary contains an element with the specified key; <c>false</c> otherwise.
    /// </returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
    /// </param>
    public bool TryGetValue(string key, out string value)
    {
      KeyValueConfigElement valueConfigElement;
      int num = this.TryGetValue(key, out valueConfigElement) ? 1 : 0;
      value = valueConfigElement.Value;
      return num != 0;
    }

    /// <summary>Adds a new key/value pair to the dictionary.</summary>
    public void Add(string key, string value) => this.Add(this.NewItem(key, value));

    void ICollection<KeyValuePair<string, string>>.Add(
      KeyValuePair<string, string> item)
    {
      this.Add(item.Key, item.Value);
    }

    bool ICollection<KeyValuePair<string, string>>.Remove(
      KeyValuePair<string, string> item)
    {
      return this.Remove(item.Key);
    }

    bool ICollection<KeyValuePair<string, string>>.Contains(
      KeyValuePair<string, string> item)
    {
      return this.Contains(this.NewItem(item.Key, item.Value));
    }

    void ICollection<KeyValuePair<string, string>>.CopyTo(
      KeyValuePair<string, string>[] array,
      int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException();
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException();
      foreach (KeyValueConfigElement valueConfigElement in (IEnumerable<KeyValueConfigElement>) (this as ICollection<KeyValueConfigElement>))
      {
        if (arrayIndex >= array.Length)
          throw new ArgumentException();
        array[arrayIndex++] = new KeyValuePair<string, string>(valueConfigElement.Key, valueConfigElement.Value);
      }
    }

    IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
    {
      foreach (KeyValueConfigElement valueConfigElement in (ConfigElementCollection) this)
        yield return new KeyValuePair<string, string>(valueConfigElement.Key, valueConfigElement.Value);
    }

    private KeyValueConfigElement NewItem(string key, string value)
    {
      KeyValueConfigElement valueConfigElement = (KeyValueConfigElement) this.CreateNew();
      valueConfigElement.Key = key;
      valueConfigElement.Value = value;
      return valueConfigElement;
    }
  }
}
