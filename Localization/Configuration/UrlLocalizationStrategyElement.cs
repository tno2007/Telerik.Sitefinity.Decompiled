// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.UrlLocalizationStrategyElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Localization.Configuration
{
  public class UrlLocalizationStrategyElement : ConfigElement, IUrlLocalizationStrategySettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Configuration.CultureElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public UrlLocalizationStrategyElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal UrlLocalizationStrategyElement()
      : base(false)
    {
    }

    [ConfigurationProperty("urlLocalizationStrategyName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string UrlLocalizationStrategyName
    {
      get => (string) this["urlLocalizationStrategyName"];
      set => this["urlLocalizationStrategyName"] = (object) value;
    }

    [ConfigurationProperty("urlLocalizationStrategyType", DefaultValue = null, IsRequired = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    public virtual Type UrlLocalizationStrategyType
    {
      get => (Type) this["urlLocalizationStrategyType"];
      set => this["urlLocalizationStrategyType"] = (object) value;
    }

    /// <summary>
    /// Represents a map between the installed cultures and the settings for the the current localization strategy
    /// <example>
    /// For example if domain strategy is used
    /// key is the culture and setting is the domain
    /// English - mydomain.com
    /// Spanish - myspanishdomain.es
    /// For url
    /// 
    /// </example>
    /// </summary>
    /// <value>The URL localization strategy settings.</value>
    [ConfigurationProperty("urlLocalizationStrategySettings")]
    public virtual ConfigElementDictionary<string, CultureSettingElement> StrategyCultureSettings => (ConfigElementDictionary<string, CultureSettingElement>) this["urlLocalizationStrategySettings"];

    /// <summary>
    /// Gets a collection of user-defined parameters for the strategy.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    public IEnumerable<ICultureSetting> GetCultureSettings() => (IEnumerable<ICultureSetting>) this.StrategyCultureSettings.Values;
  }
}
