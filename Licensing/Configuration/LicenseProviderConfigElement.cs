// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Configuration.LicenseProviderConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Licensing.Configuration
{
  /// <summary>License provider config element class</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Title = "LicenseProviderTitle")]
  internal class LicenseProviderConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Configuration.LicenseProviderConfigElement" /> class
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LicenseProviderConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the unique name for the provider, used as a key.
    /// </summary>
    [ConfigurationProperty("name", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LicenseProviderNameDescription", Title = "LicenseProviderNameTitle")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the .NET class that implements this provider.
    /// </summary>
    [ConfigurationProperty("providerType", IsRequired = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LicenseProviderTypeDescription", Title = "LicenseProviderTypeTitle")]
    public Type ProviderType
    {
      get => (Type) this["providerType"];
      set => this["providerType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }
  }
}
