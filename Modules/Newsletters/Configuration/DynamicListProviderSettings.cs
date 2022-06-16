// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Configuration.DynamicListProviderSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Newsletters.Configuration
{
  /// <summary>
  /// Configuration element that provides the information about a dynamic list provider.
  /// </summary>
  public class DynamicListProviderSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DynamicListProviderSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Configuration.DynamicListProviderSettings" /> class.
    /// </summary>
    internal DynamicListProviderSettings()
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
    /// Gets or sets the title (what users will see) of this provider.
    /// </summary>
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>The type of the data provider.</summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
    public Type ProviderType
    {
      get => (Type) this["type"];
      set => this["type"] = (object) value;
    }
  }
}
