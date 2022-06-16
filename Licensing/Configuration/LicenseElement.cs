// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Configuration.LicenseElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing.Providers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Licensing.Configuration
{
  [ObjectInfo(typeof (ConfigDescriptions), Description = "LicenseConfigDescription", Title = "LicenseConfigCaption")]
  public class LicenseElement : ConfigElement
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LicenseElement(ConfigElement parent)
      : base(parent)
    {
    }

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeLicenseProviders();
    }

    /// <summary>Gets or sets the license service URL.</summary>
    /// <value>The license service URL.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "LicenseServiceUrl")]
    [ConfigurationProperty("licenseServiceUrl", DefaultValue = "http://localhost:12345/LicensingService.svc")]
    [Obsolete("This property has never been used.")]
    public string LicenseServiceUrl
    {
      get => (string) this["licenseServiceUrl"];
      set => this["licenseServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the web service connection time out in milliseconds.
    /// </summary>
    /// <value>The connection time out in milliseconds.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "LicenseServiceConnectionTimeOut")]
    [ConfigurationProperty("serviceConnectionTimeOut", DefaultValue = 3000)]
    [Obsolete("This property has never been used.")]
    public int ServiceConnectionTimeOut
    {
      get => (int) this["serviceConnectionTimeOut"];
      set => this["serviceConnectionTimeOut"] = (object) value;
    }

    /// <summary>
    /// The name of the default license provider in case there are multiple providers
    /// </summary>
    [ConfigurationProperty("defaultLicenseProvider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultLicenseProviderDescription", Title = "DefaultLicenseProviderTitle")]
    internal string DefaultLicenseProvider
    {
      get => (string) this["defaultLicenseProvider"];
      set => this["defaultLicenseProvider"] = (object) value;
    }

    /// <summary>A list of registered license providers</summary>
    [ConfigurationProperty("licenseProviders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LicenseProvidersDescription", Title = "LicenseProvidersTitle")]
    internal virtual ConfigElementDictionary<string, LicenseProviderConfigElement> LicenseProviders => (ConfigElementDictionary<string, LicenseProviderConfigElement>) this["licenseProviders"];

    private void InitializeLicenseProviders()
    {
      ConfigElementDictionary<string, LicenseProviderConfigElement> licenseProviders = this.LicenseProviders;
      licenseProviders.Add(new LicenseProviderConfigElement((ConfigElement) licenseProviders)
      {
        Name = "FileLicenseProvider",
        ProviderType = typeof (FileLicenseProvider)
      });
    }
  }
}
