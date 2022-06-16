// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Configuration.PublishingDataItemsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Data;

namespace Telerik.Sitefinity.Publishing.Configuration
{
  /// <summary>Main config for Sitefinity's publishing system</summary>
  [DescriptionResource(typeof (PublishingMessages), "PublishingConfig")]
  public class PublishingDataItemsConfig : ConfigSection
  {
    private const string DefaultProviderName = "OAPublishingDataItemsProvider";

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage pages.
    /// </summary>
    [DescriptionResource(typeof (PublishingMessages), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OAPublishingDataItemsProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets or sets the default provider settings</summary>
    [DescriptionResource(typeof (PublishingMessages), "DataItemsProviderSettings")]
    [ConfigurationProperty("providerSettings")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> ProviderSettings
    {
      get => (ConfigElementDictionary<string, DataProviderSettings>) this["providerSettings"];
      set => this["providerSettings"] = (object) value;
    }

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ProviderSettings.Add(new DataProviderSettings((ConfigElement) this.ProviderSettings)
      {
        Name = "OAPublishingDataItemsProvider",
        Description = "A provider that stores publishing data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessPublishingPointDynamicTypeProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/PublishingItems"
          }
        }
      });
    }

    private static class PropertyNames
    {
      public const string DefaultProvider = "defaultProvider";
      public const string ProviderSettings = "providerSettings";
      public const string TweeterSettings = "TweeterSettings";
    }
  }
}
