// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ModuleSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Defines configuration settings for Sitefinity module or system service.
  /// </summary>
  [DescriptionResource(typeof (ConfigDescriptions), "ModuleSettings")]
  public class ModuleSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ModuleSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name of the module or system service.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ModuleSettingsNameDescription", Title = "ModuleSettingsNameTitle")]
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name that will be displayed for the item on the user interface.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemTitle")]
    [ConfigurationProperty("title", DefaultValue = "")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets short description of the module or system service.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ModuleSettingsDescription")]
    [ConfigurationProperty("description", DefaultValue = "")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
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

    /// <summary>Gets or sets the module id.</summary>
    /// <value>The module id.</value>
    [ConfigurationProperty("moduleId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    public Guid ModuleId
    {
      get => (Guid) this["moduleId"];
      set => this["moduleId"] = (object) value;
    }

    /// <summary>The type of the system service.</summary>
    [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ModuleSettingsTypeDescription", Title = "ModuleSettingsTypeTitle")]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
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

    /// <summary>
    /// Defines the startup type of the service. The default value is OnFirstCall.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "StartupType")]
    [ConfigurationProperty("startupType", DefaultValue = StartupType.OnFirstCall)]
    public StartupType StartupType
    {
      get => (StartupType) this["startupType"];
      set => this["startupType"] = (object) value;
    }

    /// <summary>Gets or sets a version this module is installed to.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ConfigurationProperty("version", DefaultValue = null)]
    [TypeConverter(typeof (StringVersionConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VersionAttributeDescription", Title = "VersionAttributeTitle")]
    [Obsolete("Not used any more. Since 9.0 the value is stored in the database table 'sf_module_vrsn'")]
    public Version Version
    {
      get => (Version) this["version"];
      set => this["version"] = (object) value;
    }

    /// <summary>
    /// Gets or sets if there is operation error when manipulating module .
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ConfigurationProperty("errorMessage", DefaultValue = "")]
    [Obsolete("Not used any more. Since 9.0 the value is stored in the database table 'sf_module_vrsn'")]
    public string ErrorMessage
    {
      get => (string) this["errorMessage"];
      set => this["errorMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the module is visible in the UI or not.
    /// </summary>
    /// <value>
    ///   <c>true</c> if visible; otherwise, <c>false</c>.
    /// </value>
    [Browsable(true)]
    [ReadOnly(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ConfigurationProperty("hidden", DefaultValue = false)]
    internal bool Hidden
    {
      get => (bool) this["hidden"];
      set => this["hidden"] = (object) value;
    }
  }
}
