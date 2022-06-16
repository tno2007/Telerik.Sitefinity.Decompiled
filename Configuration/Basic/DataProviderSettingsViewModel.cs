// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.DataProviderSettingsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>
  /// View model class for <see cref="T:Telerik.Sitefinity.Configuration.DataProviderSettings" />.
  /// </summary>
  [DataContract]
  public class DataProviderSettingsViewModel
  {
    public DataProviderSettingsViewModel()
    {
    }

    public DataProviderSettingsViewModel(DataProviderSettings provider)
    {
      this.Id = provider.Name;
      this.Name = provider.Name;
      this.ProviderType = provider.ProviderType.FullName;
      this.Enabled = provider.Enabled;
      this.Title = string.IsNullOrEmpty(provider.Title) ? provider.Name : Res.GetLocalizable(provider.Title, provider.ResourceClassId);
      this.Description = Res.GetLocalizable(provider.Description, provider.ResourceClassId);
      BlobStorageTypeConfigElement typeConfigElement = Config.Get<LibrariesConfig>().BlobStorage.BlobStorageTypes.Values.SingleOrDefault<BlobStorageTypeConfigElement>((Func<BlobStorageTypeConfigElement, bool>) (t => t.ProviderType == provider.ProviderType));
      this.ProviderTypeTitle = typeConfigElement == null ? provider.ProviderType.Name : Res.GetLocalizable(typeConfigElement.Title, typeConfigElement.ResourceClassId);
    }

    [DataMember]
    public string Id { get; set; }

    /// <summary>The unique name of the provider.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>The user friendly name of the provider.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Description of the provider.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>The .NET type that implements the provider logic.</summary>
    [DataMember]
    public string ProviderType { get; set; }

    /// <summary>The friendly name of the provider type.</summary>
    [DataMember]
    public string ProviderTypeTitle { get; set; }

    /// <summary>A value inidicating whether the provider is enabled.</summary>
    [DataMember]
    public bool Enabled { get; set; }

    /// <summary>
    /// A value indicating whether the provider is the default one.
    /// </summary>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>A value indicating whether the provider has settings.</summary>
    [DataMember]
    public bool HasSettings { get; set; }
  }
}
