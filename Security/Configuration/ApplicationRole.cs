// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.ApplicationRole
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Defines application role. Application roles are like user roles, except that they are assigned to users automatically based on predefined conditions.
  /// </summary>
  [DescriptionResource(typeof (ConfigDescriptions), "ApplicationRoleDescription")]
  [DebuggerDisplay("[Config] AppRole {Name}, Id = {Id}")]
  public class ApplicationRole : ConfigElement, IRoleInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.ApplicationRole" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ApplicationRole(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the role.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemName")]
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the name of the permission.</summary>
    [ConfigurationProperty("id", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    public Guid Id
    {
      get => (Guid) this["id"];
      set => this["id"] = (object) value;
    }

    /// <summary>Gets or sets description of the application role.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ApplicationRoleDescriptionDescription")]
    [ConfigurationProperty("description", DefaultValue = "")]
    public string Description
    {
      get => !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, (string) this["description"]) : (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Specifies whether users can be manually (through the user interface) assigned to this application role.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "AllowManualUserAssignment")]
    [ConfigurationProperty("allowManualUserAssignment", DefaultValue = false)]
    public bool AllowManualUserAssignment
    {
      get => (bool) this["allowManualUserAssignment"];
      set => this["allowManualUserAssignment"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }
  }
}
