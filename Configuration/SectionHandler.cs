// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SectionHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.HealthMonitoring.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents the <c>configuration/telerik/sitefinity</c> web.config section.
  /// </summary>
  public class SectionHandler : ConfigurationSection
  {
    internal const string DataEncryptionResolverName = "Encrypt";
    internal const string AppSettingsResolverName = "AppSettings";
    internal const string EnvironmentVariablesResolverName = "EnvVariables";

    /// <summary>
    /// Provides (meta-)configuration for Sitefinity's configuration subsystem.
    /// </summary>
    [ConfigurationProperty("sitefinityConfig")]
    public SitefinitySettingsElement Settings => (SitefinitySettingsElement) this["sitefinityConfig"];

    /// <summary>
    /// Specifies hosting environment settings like a specific hosting platform.
    /// </summary>
    [ConfigurationProperty("environment")]
    public SitefinityEnvironmentElement Environment => (SitefinityEnvironmentElement) this["environment"];

    [ConfigurationProperty("testing")]
    public SitefinityTestingElement Testing => (SitefinityTestingElement) this["testing"];

    [ConfigurationProperty("healthMonitoring")]
    public HealthMonitoringElement HealthMonitoring
    {
      get => (HealthMonitoringElement) this["healthMonitoring"];
      set => this["healthMonitoring"] = (object) value;
    }

    protected override void InitializeDefault()
    {
      base.InitializeDefault();
      this.LoadDefaults();
    }

    protected internal void LoadDefaults()
    {
      this.Settings.Providers.Add(new ProviderSettings("XmlConfigProvider", "Telerik.Sitefinity.Configuration.Data.XmlConfigProvider, Telerik.Sitefinity"));
      this.Settings.DefaultProvider = "XmlConfigProvider";
      this.Settings.DefaultSecretResolver = "Encrypt";
      this.Settings.SecretResolvers.Add(new ProviderSettings("Encrypt", TypeResolutionService.UnresolveType(typeof (DataEncryptionResolver)))
      {
        Parameters = {
          {
            "title",
            "ConfigEncryptOption"
          },
          {
            "resourceClassId",
            "Labels"
          }
        }
      });
      this.Settings.SecretResolvers.Add(new ProviderSettings("AppSettings", TypeResolutionService.UnresolveType(typeof (AppSettingsResolver)))
      {
        Parameters = {
          {
            "title",
            "ConfigAppSettingKeyOption"
          },
          {
            "resourceClassId",
            "Labels"
          }
        }
      });
      this.Settings.SecretResolvers.Add(new ProviderSettings("EnvVariables", TypeResolutionService.UnresolveType(typeof (EnvironmentVariablesResolver)))
      {
        Parameters = {
          {
            "title",
            "ConfigEnvVariableKeyOption"
          },
          {
            "resourceClassId",
            "Labels"
          }
        }
      });
    }
  }
}
