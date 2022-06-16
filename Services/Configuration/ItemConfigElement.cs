// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.ItemConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>Defines migration configuration element data.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemConfigElementDescription", Title = "ItemConfigElementTitle")]
  public class ItemConfigElement : ConfigElement
  {
    private const string NameKey = "name";
    private const string DataKey = "data";
    private const string VersionKey = "version";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.ItemConfigElement" /> class.
    /// </summary>
    public ItemConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.ItemConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ItemConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the programmatic name of the configuration element.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the configuration data.</summary>
    /// <value>The configuration data.</value>
    [Browsable(false)]
    [ConfigurationProperty("data", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemDataDescription", Title = "ItemDataTitle")]
    public string Data
    {
      get => (string) this["data"];
      set => this["data"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the version string of the configuration data.
    /// </summary>
    /// <value>The version string.</value>
    [Browsable(false)]
    [ConfigurationProperty("version", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VersionStringDescription", Title = "VersionStringTitle")]
    public string VersionString
    {
      get => (string) this["version"];
      set => this["version"] = (object) value;
    }

    /// <summary>Gets or sets the version of the configuration data.</summary>
    /// <value>The version.</value>
    [Browsable(false)]
    public Version Version
    {
      get => Version.Parse(this.VersionString);
      set => this.VersionString = value != (Version) null ? value.ToString() : (string) null;
    }
  }
}
