// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.CustomPermissionsDisplaySettingsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  public class CustomPermissionsDisplaySettingsConfig : ConfigElement
  {
    /// <summary>
    /// </summary>
    internal CustomPermissionsDisplaySettingsConfig()
      : base(false)
    {
    }

    public CustomPermissionsDisplaySettingsConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the full name of the type for which the permission set is customized.
    /// </summary>
    [ConfigurationProperty("setName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string SetName
    {
      get => (string) this["setName"];
      set => this["setName"] = (object) value;
    }

    /// <summary>
    /// A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SecuredObjectCustomPermissionSets")]
    [ConfigurationProperty("securedObjectCustomPermissionSets")]
    [ConfigurationCollection(typeof (SecuredObjectCustomPermissionSet), AddItemName = "securedObjectType")]
    public ConfigElementDictionary<string, SecuredObjectCustomPermissionSet> SecuredObjectCustomPermissionSets => (ConfigElementDictionary<string, SecuredObjectCustomPermissionSet>) this["securedObjectCustomPermissionSets"];
  }
}
