// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.DataProviderSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents the configuration elements associated with a provider.
  /// </summary>
  public class DataProviderSettings : ConfigElement, IDataProviderSettings, ITitledConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DataProviderSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.DataProviderSettings" /> class.
    /// </summary>
    internal DataProviderSettings()
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

    /// <summary>User friendly, localizable name.</summary>
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets description of the provider configured by this class.
    /// </summary>
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

    /// <summary>The type of the data provider.</summary>
    public Type ProviderType
    {
      get
      {
        Type providerType = (Type) null;
        if (!string.IsNullOrEmpty(this.ProviderTypeName))
          providerType = TypeResolutionService.ResolveType(this.ProviderTypeName);
        return providerType;
      }
      set
      {
        string name = value.Assembly.GetName().Name;
        if (name.StartsWith("artificial"))
          this.ProviderTypeName = value.FullName;
        this.ProviderTypeName = value.FullName + ", " + name;
      }
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

    [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
    public string ProviderTypeName
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the provider is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }
  }
}
