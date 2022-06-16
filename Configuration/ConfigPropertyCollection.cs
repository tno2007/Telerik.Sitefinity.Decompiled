// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigPropertyCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Represents a collection of config element properties.</summary>
  public class ConfigPropertyCollection : KeyedCollection<string, ConfigProperty>
  {
    private ConfigProperty[] keyProperties;

    /// <summary>Gets the key for item.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    protected override string GetKeyForItem(ConfigProperty item) => item.Name;

    public ConfigProperty[] KeyProperties => this.keyProperties;

    public bool TryGetValue(string key, out ConfigProperty value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out value);
      foreach (ConfigProperty configProperty in (IEnumerable<ConfigProperty>) this.Items)
      {
        if (this.Comparer.Equals(this.GetKeyForItem(configProperty), key))
        {
          value = configProperty;
          return true;
        }
      }
      value = (ConfigProperty) null;
      return false;
    }

    /// <summary>Inserts the item.</summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The item.</param>
    protected override void InsertItem(int index, ConfigProperty item)
    {
      base.InsertItem(index, item);
      if (!item.IsKey)
        return;
      if (this.keyProperties == null)
      {
        this.keyProperties = new ConfigProperty[1]{ item };
      }
      else
      {
        int length = this.keyProperties.Length;
        ConfigProperty[] destinationArray = new ConfigProperty[length + 1];
        Array.Copy((Array) this.keyProperties, (Array) destinationArray, length);
        destinationArray[length] = item;
        this.keyProperties = destinationArray;
      }
    }
  }
}
