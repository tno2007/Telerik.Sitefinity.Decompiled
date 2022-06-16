// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Configuration.UserProfilesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.UserProfiles.Configuration
{
  /// <summary>Defines user profiles configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "UserProfilesConfig")]
  public class UserProfilesConfig : ConfigSection
  {
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      string str = "OpenAccessProfileProvider";
      this.Providers.Add(new DataProviderSettings((ConfigElement) this.Providers)
      {
        Name = str,
        Description = "A provider that stores user profiles data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessProfileProvider),
        Parameters = new NameValueCollection()
        {
          {
            "urlFormat",
            "/[User.Id]"
          },
          {
            "applicationName",
            "/UserProfiles"
          },
          {
            "isNickNameUnique",
            "true"
          }
        }
      });
      UserProfilesHelper.UpdateConfiguration(this, typeof (SitefinityProfile).FullName, new UserProfileTypeViewModel()
      {
        ProfileProviderName = str,
        MembershipProvidersUsage = MembershipProvidersUsage.AllProviders
      });
    }

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage security.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessProfileProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>
    /// Gets a collection of profile membership provider settings.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ProfileTypesSettings")]
    [ConfigurationProperty("profileTypesSettings")]
    public virtual ConfigElementDictionary<string, ProfileTypeSettings> ProfileTypesSettings => (ConfigElementDictionary<string, ProfileTypeSettings>) this["profileTypesSettings"];
  }
}
