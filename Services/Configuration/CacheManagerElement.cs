// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.CacheManagerElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Services.Configuration
{
  public class CacheManagerElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CacheManagerElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name of the module or system service.
    /// </summary>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [ConfigurationProperty("pollWhetherItemsAreExpiredIntervalSeconds", DefaultValue = 60)]
    public int PollWhetherItemsAreExpiredIntervalSeconds
    {
      get => (int) this["pollWhetherItemsAreExpiredIntervalSeconds"];
      set => this["pollWhetherItemsAreExpiredIntervalSeconds"] = (object) value;
    }

    [ConfigurationProperty("startScavengingAfterItemCount", DefaultValue = 1000)]
    public int StartScavengingAfterItemCount
    {
      get => (int) this["startScavengingAfterItemCount"];
      set => this["startScavengingAfterItemCount"] = (object) value;
    }

    [ConfigurationProperty("whenScavengingRemoveItemCount", DefaultValue = 10)]
    public int WhenScavengingRemoveItemCount
    {
      get => (int) this["whenScavengingRemoveItemCount"];
      set => this["whenScavengingRemoveItemCount"] = (object) value;
    }

    [ConfigurationProperty("cacheStore", DefaultValue = CacheStore.InMemory)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Not supported.")]
    public CacheStore CacheStore
    {
      get => (CacheStore) this["cacheStore"];
      set => this["cacheStore"] = (object) value;
    }

    [ConfigurationProperty("backingStoreName", DefaultValue = "")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Not supported.")]
    public string BackingStoreName
    {
      get => (string) this["backingStoreName"];
      set => this["backingStoreName"] = (object) value;
    }

    [ConfigurationProperty("customCacheStoreType")]
    [TypeConverter(typeof (StringTypeConverter))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Not supported.")]
    public Type CustomCacheStoreType
    {
      get => (Type) this["customCacheStoreType"];
      set => this["customCacheStoreType"] = (object) value;
    }
  }
}
