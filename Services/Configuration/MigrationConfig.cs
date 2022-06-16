// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.MigrationConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines migration system configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "MigrationConfig")]
  public class MigrationConfig : ConfigSection
  {
    internal const string BuildKey = "build";
    internal const string VersionKey = "version";

    [Browsable(false)]
    [ConfigurationProperty("items")]
    [DescriptionResource(typeof (ConfigDescriptions), "ItemConfigElements")]
    internal virtual ConfigElementDictionary<string, ItemConfigElement> Items => (ConfigElementDictionary<string, ItemConfigElement>) this["items"];

    internal void SetJsonItem<TValue>(string key, TValue item, Version version = null)
    {
      MigrationConfig.AssertKey(key);
      this.Items.Add(key, new ItemConfigElement()
      {
        Name = key,
        Data = item.ToJson<TValue>(),
        Version = version
      });
    }

    internal TResult GetJsonItem<TResult>(string key)
    {
      MigrationConfig.AssertKey(key);
      ItemConfigElement itemConfigElement = this.Items[key];
      return itemConfigElement == null ? default (TResult) : itemConfigElement.Data.FromJson<TResult>();
    }

    internal TResult GetItem<TResult>(string key)
    {
      MigrationConfig.AssertKey(key);
      ItemConfigElement itemConfigElement = this.Items[key];
      return itemConfigElement == null || string.IsNullOrEmpty(itemConfigElement.Data) ? default (TResult) : (TResult) TypeDescriptor.GetConverter(typeof (TResult)).ConvertFromString(itemConfigElement.Data);
    }

    internal void SetItem<TValue>(string key, TValue value, Version version = null)
    {
      MigrationConfig.AssertKey(key);
      this.Items.Add(key, new ItemConfigElement()
      {
        Name = key,
        Data = value.ToString(),
        Version = version
      });
    }

    private static void AssertKey(string key)
    {
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException(nameof (key));
    }
  }
}
