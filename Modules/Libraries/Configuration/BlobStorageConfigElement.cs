// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.BlobStorageConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>Blob storage configuration.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageDescription", Title = "BlobStorageTitle")]
  public class BlobStorageConfigElement : ConfigElement
  {
    public BlobStorageConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>The key of the default blob storage provider.</summary>
    [ConfigurationProperty("defaultProvider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageDefaultProviderDescription", Title = "BlobStorageDefaultProviderTitle")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>
    /// Concrete physical blob storage locations, represented by parameterized instances of the supported blob storage provider types.
    /// </summary>
    [ConfigurationProperty("providers")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageProvidersDescription", Title = "BlobStorageProvidersTitle")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>
    /// The kinds of blob storage that the Libraries module can use.
    /// </summary>
    [ConfigurationProperty("blobStorageTypes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypesDescription", Title = "BlobStorageTypesTitle")]
    public virtual ConfigElementDictionary<string, BlobStorageTypeConfigElement> BlobStorageTypes => (ConfigElementDictionary<string, BlobStorageTypeConfigElement>) this["blobStorageTypes"];

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string DefaultProvider = "defaultProvider";
      public const string Providers = "providers";
      public const string BlobStorageTypes = "blobStorageTypes";
    }
  }
}
