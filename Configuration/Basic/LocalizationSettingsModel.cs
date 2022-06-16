// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.LocalizationSettingsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>Represents the model for basic localization settings.</summary>
  [DataContract]
  public class LocalizationSettingsModel
  {
    /// <summary>Gets a collection of the supported cultures.</summary>
    [DataMember]
    public CultureViewModel[] Cultures { get; set; }

    /// <summary>Gets a collection of the supported backend cultures.</summary>
    [DataMember]
    public CultureViewModel[] BackendCultures { get; set; }

    /// <summary>
    /// Gets or sets the culture to be used when working in monolingual mode.
    /// </summary>
    /// <value>The monolingual culture.</value>
    [DataMember]
    public string MonolingualCulture { get; set; }

    /// <summary>
    /// Gets or sets the key of the default URL localization strategy.
    /// </summary>
    [DataMember]
    public string DefaultLocalizationStrategy { get; set; }

    /// <summary>
    /// Gets or sets the domain URL localization strategy settings.
    /// </summary>
    [DataMember]
    [Obsolete("Use DefaultStrategySettings")]
    public CultureSettingViewModel[] SubdomainStrategySettings { get; set; }

    /// <summary>
    /// Gets or sets the default localization strategy settings specidfied by DefaultLocalizationStrategy.
    /// </summary>
    [DataMember]
    public CultureSettingViewModel[] DefaultStrategySettings { get; set; }

    /// <summary>
    /// Initialize the properties of this instance
    /// with the settings from the configuration object.
    /// </summary>
    /// <param name="config">The configuration object.</param>
    /// <param name="includeSiteNames">if set to <c>true</c> the names of the sites in which each language is used will be included.</param>
    public void Configure(ResourcesConfig config, bool includeSitesNames = false)
    {
      this.Cultures = config.Cultures.Values.Select<CultureElement, CultureViewModel>((Func<CultureElement, CultureViewModel>) (c => new CultureViewModel(c))).ToArray<CultureViewModel>();
      string str = string.IsNullOrEmpty(config.DefaultCultureKey) ? config.Cultures.Keys.First<string>() : config.DefaultCultureKey;
      foreach (CultureViewModel culture1 in this.Cultures)
      {
        CultureViewModel culture = culture1;
        culture.IsDefault = culture.Key == str;
        if (includeSitesNames && SystemManager.MultisiteContext != null)
        {
          IEnumerable<ISite> source = SystemManager.MultisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => ((IEnumerable<CultureInfo>) s.PublicContentCultures).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.Name == culture.Culture))));
          culture.SitesNames = source.Select<ISite, string>((Func<ISite, string>) (s => s.Name)).ToArray<string>();
          culture.SitesUsingCultureAsDefault = source.Where<ISite>((Func<ISite, bool>) (s => s.DefaultCulture.Name == culture.Culture)).Select<ISite, string>((Func<ISite, string>) (s => s.Name)).ToArray<string>();
        }
      }
      this.BackendCultures = config.BackendCultures.Values.Select<CultureElement, CultureViewModel>((Func<CultureElement, CultureViewModel>) (c => new CultureViewModel(c))).ToArray<CultureViewModel>();
      foreach (CultureViewModel backendCulture in this.BackendCultures)
        backendCulture.IsDefault = backendCulture.Key == config.DefaultBackendCultureKey;
      this.DefaultLocalizationStrategy = config.DefaultUrlLocalizationStrategyKey;
      this.DefaultStrategySettings = this.GetUrlStrategySettings(config, this.DefaultLocalizationStrategy);
      List<CultureSettingViewModel> source1 = new List<CultureSettingViewModel>((IEnumerable<CultureSettingViewModel>) this.GetUrlStrategySettings(config, typeof (DomainUrlLocalizationStrategy).Name));
      foreach (CultureViewModel culture2 in this.Cultures)
      {
        CultureViewModel culture = culture2;
        if (!source1.Any<CultureSettingViewModel>((Func<CultureSettingViewModel, bool>) (s => s.Key == culture.Key)))
          source1.Add(new CultureSettingViewModel()
          {
            Key = culture.Key
          });
      }
      this.SubdomainStrategySettings = source1.ToArray();
    }

    private CultureSettingViewModel[] GetUrlStrategySettings(
      ResourcesConfig config,
      string strategyName)
    {
      UrlLocalizationStrategyElement localizationStrategyElement;
      return config.UrlLocalizationStrategies.TryGetValue(strategyName, out localizationStrategyElement) ? localizationStrategyElement.StrategyCultureSettings.Values.Select<CultureSettingElement, CultureSettingViewModel>((Func<CultureSettingElement, CultureSettingViewModel>) (cs => new CultureSettingViewModel(cs))).ToArray<CultureSettingViewModel>() : new CultureSettingViewModel[0];
    }

    /// <summary>
    /// Applies the values of the properties of this instance to the given config object.
    /// </summary>
    public void Apply(ref ResourcesConfig config)
    {
      config.DefaultBackendCultureKey = (string) null;
      string defaultCultureKey;
      this.UpdateCultures(config, config.Cultures, this.Cultures, out defaultCultureKey);
      this.UpdateCultures(config, config.BackendCultures, this.BackendCultures, out defaultCultureKey);
      config.DefaultBackendCultureKey = defaultCultureKey;
      if (string.IsNullOrEmpty(this.DefaultLocalizationStrategy))
        return;
      config.DefaultUrlLocalizationStrategyKey = this.DefaultLocalizationStrategy;
      UrlLocalizationStrategyElement localizationStrategyElement;
      if (!(this.DefaultLocalizationStrategy == typeof (DomainUrlLocalizationStrategy).Name) || !config.UrlLocalizationStrategies.TryGetValue(this.DefaultLocalizationStrategy, out localizationStrategyElement))
        return;
      ConfigElementDictionary<string, CultureSettingElement> strategyCultureSettings = localizationStrategyElement.StrategyCultureSettings;
      strategyCultureSettings.Clear();
      foreach (CultureSettingViewModel subdomainStrategySetting in this.SubdomainStrategySettings)
      {
        if (!subdomainStrategySetting.Setting.IsNullOrEmpty())
          strategyCultureSettings.Add(new CultureSettingElement()
          {
            CultureKey = subdomainStrategySetting.Key,
            Setting = subdomainStrategySetting.Setting
          });
      }
    }

    private void UpdateCultures(
      ResourcesConfig config,
      ConfigElementDictionary<string, CultureElement> target,
      CultureViewModel[] cultures,
      out string defaultCultureKey)
    {
      defaultCultureKey = (string) null;
      List<string> stringList = new List<string>();
      foreach (string key1 in (IEnumerable<string>) target.Keys)
      {
        string key = key1;
        if (!((IEnumerable<CultureViewModel>) cultures).Any<CultureViewModel>((Func<CultureViewModel, bool>) (i => i.Key == key)))
          stringList.Add(key);
      }
      foreach (string key in stringList)
        target.Remove(key);
      foreach (CultureViewModel culture in cultures)
      {
        CultureElement cultureElement;
        if (!target.TryGetValue(culture.Key, out cultureElement))
        {
          cultureElement = this.CreateCultureElement(culture);
          target.Add(cultureElement);
        }
        if (culture.IsDefault)
          defaultCultureKey = culture.Key;
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture.Culture);
        config.CheckCustomCulture(cultureInfo);
      }
    }

    private CultureElement CreateCultureElement(CultureViewModel item) => new CultureElement()
    {
      Key = item.Key,
      Culture = item.Culture,
      UICulture = item.UICulture,
      FieldSuffix = item.FieldSuffix
    };
  }
}
