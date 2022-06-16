// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.BlobStorageTypeConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// A kind of blob storage that the Libraries module can use.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypeDescription", Title = "BlobStorageTypeTitle")]
  public class BlobStorageTypeConfigElement : ConfigElement, ITitledConfigElement
  {
    public BlobStorageTypeConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Unique name for the provider, used as a key.</summary>
    [ConfigurationProperty("name", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypeNameDescription", Title = "BlobStorageTypeNameTitle")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// The .NET class that implements this kind of blob storage.
    /// </summary>
    [ConfigurationProperty("providerType", IsRequired = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypeProviderTypeDescription", Title = "BlobStorageTypeProviderTypeTitle")]
    public Type ProviderType
    {
      get => (Type) this["providerType"];
      set => this["providerType"] = (object) value;
    }

    /// <summary>User friendly name for this kind of blob storage.</summary>
    [ConfigurationProperty("title", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypeTitleDescription", Title = "BlobStorageTypeTitleTitle")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Describes this kind of blob storage.</summary>
    [ConfigurationProperty("description")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageTypeDescriptionDescription", Title = "BlobStorageTypeDescriptionTitle")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Optional resource class ID used to localize the <see cref="P:Telerik.Sitefinity.Modules.Libraries.Configuration.BlobStorageTypeConfigElement.Title" /> and <see cref="P:Telerik.Sitefinity.Modules.Libraries.Configuration.BlobStorageTypeConfigElement.Description" />, which is used as resource key in this case.
    /// </summary>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    [ConfigurationProperty("settingsViewTypeOrPath")]
    public string SettingsViewTypeOrPath
    {
      get => (string) this["settingsViewTypeOrPath"];
      set => this["settingsViewTypeOrPath"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string Name = "name";
      public const string ProviderType = "providerType";
      public const string SettingsViewTypeOrPath = "settingsViewTypeOrPath";
      public const string Title = "title";
      public const string Description = "description";
      public const string ResourceClassId = "resourceClassId";
    }
  }
}
