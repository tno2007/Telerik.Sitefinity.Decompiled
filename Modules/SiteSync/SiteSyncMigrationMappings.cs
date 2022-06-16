// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncMigrationMappings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.SiteSync
{
  /// <preliminary />
  [DataContract]
  public class SiteSyncMigrationMappings
  {
    /// <summary>
    /// Stores the mappings as [typeName, [propertyName, [oldValue, newValue]]]. The default stored property is "Id"
    /// </summary>
    [DataMember]
    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> mappings;

    public SiteSyncMigrationMappings() => this.mappings = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

    public string GetMapping(string typeName, string value, string propertyName = "ItemId") => value != null && this.mappings.ContainsKey(typeName) && this.mappings[typeName].ContainsKey(propertyName) && this.mappings[typeName][propertyName].ContainsKey(value) ? this.mappings[typeName][propertyName][value] : (string) null;

    public void AddMapping(string typeName, string oldValue, string newValue, string propertyName = "ItemId")
    {
      if (!this.mappings.ContainsKey(typeName))
        this.mappings.Add(typeName, new Dictionary<string, Dictionary<string, string>>());
      if (!this.mappings[typeName].ContainsKey(propertyName))
        this.mappings[typeName].Add(propertyName, new Dictionary<string, string>());
      if (this.mappings[typeName][propertyName].ContainsKey(oldValue))
        return;
      this.mappings[typeName][propertyName].Add(oldValue, newValue);
    }

    public void AddMappings(SiteSyncMigrationMappings newMappings)
    {
      foreach (string key1 in newMappings.mappings.Keys)
      {
        if (!this.mappings.ContainsKey(key1))
          this.mappings.Add(key1, new Dictionary<string, Dictionary<string, string>>());
        foreach (string key2 in newMappings.mappings[key1].Keys)
        {
          if (!this.mappings[key1].ContainsKey(key2))
            this.mappings[key1].Add(key2, new Dictionary<string, string>());
          Dictionary<string, string> dictionary = newMappings.mappings[key1][key2];
          foreach (string key3 in dictionary.Keys)
          {
            string str = dictionary[key3];
            if ((!this.mappings[key1][key2].ContainsKey(key3) || this.mappings[key1][key2][key3] != str) && !string.IsNullOrWhiteSpace(str))
            {
              this.mappings[key1][key2][key3] = str;
              ObjectFactory.Resolve<ISiteSyncLogger>().WriteFormat("MAPPING: {0} ({1}): {2} --> {3}", (object) key2, (object) key1, (object) key3, (object) str);
            }
          }
        }
      }
    }

    public bool HasValues() => this.mappings.Any<KeyValuePair<string, Dictionary<string, Dictionary<string, string>>>>();
  }
}
