// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.DictionaryObjectViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Viewmodel class used for WCF REST services to serialize Dictionaries as objects with properties
  /// Typical use is to serialize Attribute collections in REST services - that are presented as an Attibute object with each dictionary key - value appearing as real property
  /// </summary>
  [Serializable]
  public class DictionaryObjectViewModel : ISerializable
  {
    private IDictionary<string, string> innerDictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.DictionaryObjectViewModel" /> class.
    /// </summary>
    public DictionaryObjectViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.DictionaryObjectViewModel" /> class.
    /// </summary>
    /// <param name="attributes">The attributes.</param>
    public DictionaryObjectViewModel(IDictionary<string, string> initDictionary)
      : this()
    {
      this.CopyFrom(initDictionary);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.DictionaryObjectViewModel" /> class.
    /// Used when deserializing.
    /// </summary>
    /// <param name="info">The info.</param>
    /// <param name="context">The context.</param>
    protected DictionaryObjectViewModel(SerializationInfo info, StreamingContext context)
      : this()
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      foreach (SerializationEntry serializationEntry in info)
      {
        string str = !(serializationEntry.Value is object[] objArray) ? serializationEntry.Value.ToString() : JsonSerializer.SerializeToString<object[]>(objArray);
        this.Dictionary[serializationEntry.Name] = str;
      }
    }

    private Type GetEntryType(string key, string value)
    {
      string str = (string) null;
      int num = key.LastIndexOf('_');
      if (num > 0 && num < key.Length - 2)
        str = key.Substring(num + 1);
      if (!string.IsNullOrEmpty(str))
      {
        string lowerInvariant = str.ToLowerInvariant();
        if (lowerInvariant == "boolean")
          return typeof (bool);
        if (lowerInvariant == "integer")
          return typeof (int);
      }
      return typeof (object);
    }

    /// <summary>Gets the dictionary values</summary>
    /// <value>The attributes.</value>
    public IDictionary<string, string> Dictionary
    {
      get
      {
        if (this.innerDictionary == null)
          this.innerDictionary = (IDictionary<string, string>) new System.Collections.Generic.Dictionary<string, string>();
        return this.innerDictionary;
      }
    }

    /// <summary>Gets the object data. Used when serializing.</summary>
    /// <param name="info">The info.</param>
    /// <param name="context">The context.</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        return;
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) this.Dictionary)
      {
        Type entryType = this.GetEntryType(keyValuePair.Key, keyValuePair.Value);
        if (entryType != typeof (object))
        {
          object obj = JsonSerializer.DeserializeFromString(keyValuePair.Value, entryType);
          info.AddValue(keyValuePair.Key, obj);
          break;
        }
        object obj1 = (object) keyValuePair.Value;
        if (!string.IsNullOrEmpty(keyValuePair.Value) && keyValuePair.Value.StartsWith("["))
        {
          if (keyValuePair.Value.EndsWith("]"))
          {
            try
            {
              obj1 = (object) JsonSerializer.DeserializeFromString<object[]>(keyValuePair.Value);
            }
            catch
            {
            }
          }
        }
        info.AddValue(keyValuePair.Key, obj1);
      }
    }

    /// <summary>Copies current keyvalue pairs to a target dictionary</summary>
    /// <param name="targetDictionary">The target dictionary.</param>
    public void CopyTo(IDictionary<string, string> targetDictionary)
    {
      if (this.innerDictionary == null || this.innerDictionary.Count <= 0)
        return;
      foreach (KeyValuePair<string, string> inner in (IEnumerable<KeyValuePair<string, string>>) this.innerDictionary)
        targetDictionary[inner.Key] = inner.Value;
    }

    /// Copies  keyvalue pairs from a source dictionary
    public void CopyFrom(IDictionary<string, string> sourceDictionary)
    {
      if (sourceDictionary == null)
        return;
      foreach (KeyValuePair<string, string> source in (IEnumerable<KeyValuePair<string, string>>) sourceDictionary)
        this.Dictionary[source.Key] = source.Value;
    }
  }
}
