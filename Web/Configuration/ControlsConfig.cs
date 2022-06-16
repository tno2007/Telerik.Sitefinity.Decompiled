// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ControlsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>Configuration section for control settings.</summary>
  public class ControlsConfig : ConfigSection
  {
    /// <summary>
    /// Gets or sets the default resource assembly.
    /// Resource assembly is an assembly that contains all Sitefinity resources such as
    /// embedded templates, images, CSS files and etc.
    /// By default this is Telerik.Sitefinity.Resources.dll.
    /// </summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("resourcesAssemblyInfo", DefaultValue = "Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources", IsRequired = false)]
    public Type ResourcesAssemblyInfo
    {
      get => (Type) this["resourcesAssemblyInfo"];
      set => this["resourcesAssemblyInfo"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the expiration period for cached control templates.
    /// </summary>
    [ConfigurationProperty("templateCacheExpirationInMinutes", DefaultValue = 10, IsRequired = false)]
    public int TemplateCacheExpirationInMinutes
    {
      get => (int) this["templateCacheExpirationInMinutes"];
      set => this["templateCacheExpirationInMinutes"] = (object) value;
    }

    /// <summary>
    /// Specifies how view and templates are mapped to controls.
    /// </summary>
    [ConfigurationProperty("viewMap")]
    [ConfigurationCollection(typeof (ViewModeControlSettings), AddItemName = "viewSettings")]
    public ConfigElementDictionary<Type, ViewModeControlSettings> ViewMap => (ConfigElementDictionary<Type, ViewModeControlSettings>) this["viewMap"];
  }
}
