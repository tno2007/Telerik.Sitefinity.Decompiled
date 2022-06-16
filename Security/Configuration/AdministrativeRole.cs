// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.AdministrativeRole
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Defines an administrative role.</summary>
  public class AdministrativeRole : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.AdministrativeRole" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public AdministrativeRole(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the administrative role.</summary>
    [ConfigurationProperty("roleProvider", DefaultValue = "", IsRequired = true)]
    public string RoleProvider
    {
      get => (string) this["roleProvider"];
      set => this["roleProvider"] = (object) value;
    }

    /// <summary>Gets or sets the name of the administrative role.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemName")]
    [ConfigurationProperty("roleName", DefaultValue = "", IsRequired = true)]
    public string RoleName
    {
      get => (string) this["roleName"];
      set => this["roleName"] = (object) value;
    }
  }
}
