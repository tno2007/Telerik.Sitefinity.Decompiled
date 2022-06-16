// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Configuration.ProfileTypeSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.UserProfiles.Configuration
{
  /// <summary>
  /// Represents the configuration elements associated with a provider.
  /// </summary>
  public class ProfileTypeSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ProfileTypeSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.DataProviderSettings" /> class.
    /// </summary>
    internal ProfileTypeSettings()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets the name of the provider configured by this class.
    /// </summary>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage security.
    /// </summary>
    [ConfigurationProperty("profileProviderName", DefaultValue = "", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProfileProviderDescription", Title = "ProfileProviderCaption")]
    public virtual string ProfileProvider
    {
      get => (string) this["profileProviderName"];
      set => this["profileProviderName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the profile type can use all membership providers.
    /// </summary>
    /// <value>The use all membership providers.</value>
    [ConfigurationProperty("useAllMembershipProviders", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseAllMembershipProvidersDescription", Title = "UseAllMembershipProvidersCaption")]
    public bool? UseAllMembershipProviders
    {
      get => (bool?) this["useAllMembershipProviders"];
      set => this["useAllMembershipProviders"] = (object) value;
    }

    [ConfigurationProperty("membershipProviders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MembershipProvidersDescription", Title = "MembershipProvidersTitle")]
    public ConfigElementList<MembershipProviderElement> MembershipProviders => (ConfigElementList<MembershipProviderElement>) this["membershipProviders"];
  }
}
