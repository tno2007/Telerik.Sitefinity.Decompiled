// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  public class ConfigProperty
  {
    /// <summary>
    /// No need for thread safety here, values persisted are always evaluated to the same result for the lifetime of the application domain.
    /// </summary>
    private static IDictionary<string, bool> siteSpecificPropsCache = SystemManager.CreateStaticCache<string, bool>();
    private ConfigurationProperty property;

    public ConfigProperty(ConfigurationProperty property) => this.property = property;

    /// <summary>Gets the name of this <see cref="T:Telerik.Sitefinity.Configuration.ConfigPropertyInfo" />.
    /// </summary>
    /// <returns>The name of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigPropertyInfo" />.
    /// </returns>
    public string Name => this.property.Name;

    /// <summary>Gets the default value for this <see cref="T:Telerik.Sitefinity.Configuration.ConfigPropertyInfo" />
    /// property.</summary>
    /// <returns>An <see cref="T:System.Object" /> that can be cast to the type specified
    /// by the <see cref="P:Telerik.Sitefinity.Configuration.ConfigPropertyInfo.Type" /> property.
    /// </returns>
    public object DefaultValue => this.property.DefaultValue;

    /// <summary>Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// is the key for the containing <see cref="T:System.Configuration.ConfigurationElement" />
    /// object.</summary>
    /// <returns>true if this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// object is the key for the containing element; otherwise, false. The default is
    /// false.</returns>
    public bool IsKey => this.property.IsKey;

    /// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> used to
    /// convert this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" /> into
    /// an XML representation for writing to the configuration file.</summary>
    /// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> used to convert
    /// this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" /> into an XML representation
    /// for writing to the configuration file.</returns>
    /// <exception cref="T:System.Exception">This <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// cannot be converted. </exception>
    public TypeConverter Converter
    {
      get
      {
        try
        {
          return this.property.Converter;
        }
        catch (ConfigurationErrorsException ex)
        {
          return (TypeConverter) null;
        }
      }
    }

    /// <summary>Gets a value that indicates whether the property is the default collection
    /// of an element. </summary>
    /// <returns>true if the property is the default collection of an element; otherwise,
    /// false.</returns>
    public bool IsDefaultCollection => this.property.IsDefaultCollection;

    /// <summary>Gets the type of this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// object.</summary>
    /// <returns>A <see cref="T:System.Type" /> representing the type of this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// object.</returns>
    public Type Type => this.property.Type;

    /// <summary>Gets the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />,
    /// which is used to validate this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />
    /// object.</summary>
    /// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" />
    /// validator, which is used to validate this <see cref="T:Telerik.Sitefinity.Configuration.ConfigProperty" />.
    /// </returns>
    public ConfigurationValidatorBase Validator => this.property.Validator;

    /// <summary>
    /// Gets a value indicating whether this property is required (non-<c>null</c> for reference types; non-<c>null</c> and non-empty for strings).
    /// </summary>
    /// <value>
    /// <c>true</c> if the property is required; otherwise, <c>false</c>. The default is <c>false</c>.
    /// </value>
    public bool IsRequired => this.property.IsRequired;

    /// <summary>
    /// Gets a value indicating whether the property can persist different values for different Sitefinity sites.
    /// </summary>
    internal bool IsSiteSpecific(ConfigElement el)
    {
      SiteSettingsConfig siteSettingsConfig = Config.Get<SiteSettingsConfig>();
      if (siteSettingsConfig.SiteSpecificProperties.Count == 0)
        return false;
      string path = el.GetPath() + ":" + this.Name;
      if (ConfigProperty.siteSpecificPropsCache.ContainsKey(path))
        return ConfigProperty.siteSpecificPropsCache[path];
      ConfigProperty.siteSpecificPropsCache[path] = !(el is ConfigElementCollection) && (el.Section == null || Config.IsSectionAllowedSiteSpecific(el.Section.GetType().Name)) && !this.IsKey && siteSettingsConfig != null && siteSettingsConfig.SiteSpecificProperties != null && siteSettingsConfig.SiteSpecificProperties.Count != 0 && siteSettingsConfig.SiteSpecificProperties.Values.Any<PropertyPath>((Func<PropertyPath, bool>) (x => x.Path == path));
      return ConfigProperty.siteSpecificPropsCache[path];
    }

    internal bool SkipOnExport { get; set; }

    internal bool IsSecret { get; set; }
  }
}
