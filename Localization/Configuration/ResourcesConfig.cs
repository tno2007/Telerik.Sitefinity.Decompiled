// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.ResourcesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Defines configuration settings to support the infrastructure for configuring and managing resources.
  /// </summary>
  public class ResourcesConfig : ConfigSection
  {
    private readonly ConcurrentProperty<Dictionary<string, ResourcesConfig.CultureInfoWrapper>> allCultures;
    internal CultureInfo initialDefaultCulture = CultureInfo.GetCultureInfo("en");

    public ResourcesConfig() => this.allCultures = new ConcurrentProperty<Dictionary<string, ResourcesConfig.CultureInfoWrapper>>(new Func<Dictionary<string, ResourcesConfig.CultureInfoWrapper>>(this.LoadAllCultures));

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage resources.
    /// </summary>
    [ConfigurationProperty("defaultProvider", DefaultValue = "XmlDataProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets a collection of the supported frontend cultures.</summary>
    [ConfigurationProperty("cultures")]
    public virtual ConfigElementDictionary<string, CultureElement> Cultures => (ConfigElementDictionary<string, CultureElement>) this["cultures"];

    /// <summary>Gets or sets the default culture for the application.</summary>
    [ConfigurationProperty("defaultCultureKey", DefaultValue = "")]
    public virtual string DefaultCultureKey
    {
      get => (string) this["defaultCultureKey"];
      set => this["defaultCultureKey"] = (object) value;
    }

    /// <summary>Gets a collection of the supported backend cultures.</summary>
    [ConfigurationProperty("backendCultures")]
    public virtual ConfigElementDictionary<string, CultureElement> BackendCultures => (ConfigElementDictionary<string, CultureElement>) this["backendCultures"];

    /// <summary>
    /// Gets or sets the default backend culture for the application.
    /// </summary>
    [ConfigurationProperty("defaultBackendCultureKey", DefaultValue = "")]
    public virtual string DefaultBackendCultureKey
    {
      get => (string) this["defaultBackendCultureKey"];
      set => this["defaultBackendCultureKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this site is multilingual.
    /// </summary>
    /// <value><c>true</c> if multilingual; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("multilingual", DefaultValue = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("The system is always in Multilinlgual.")]
    public virtual bool Multilingual
    {
      get => true;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the culture to be used when working in monolingual mode.
    /// </summary>
    /// <value>The monolingual culture.</value>
    [Obsolete("This property must not be used anymore. The monolingual culture is now stored as an element in Cultures/BackendCultures.")]
    [ConfigurationProperty("monolingualCulture", DefaultValue = "en")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public virtual string MonolingualCulture
    {
      get => (string) this["monolingualCulture"];
      set => this["monolingualCulture"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include the default language in the url when using the subfolder strategy - /en/page instead /page
    /// By default the prefix is included only for non default cultures
    /// </summary>
    [ConfigurationProperty("includeSubfoderPrefixForDefaultLanguage", DefaultValue = false)]
    [Browsable(false)]
    [Obsolete("Moved as a parameter of the SubFolderUrlLocalizationStrategy")]
    internal bool IncludeSubfoderPrefixForDefaultLanguage
    {
      get => (bool) this["includeSubfoderPrefixForDefaultLanguage"];
      set => this["includeSubfoderPrefixForDefaultLanguage"] = (object) value;
    }

    /// <summary>
    /// Gets the default frontend culture configuration for the system. This is the language that the system content was initially created in before other languages were added.
    /// </summary>
    /// <value>The default frontend culture.</value>
    [Obsolete("Use AppSettings.DefaultFrontendLanguage")]
    public virtual CultureElement DefaultCulture => string.IsNullOrEmpty(this.DefaultCultureKey) ? (CultureElement) null : ResourcesConfig.GetDefaultCultureElement(this.DefaultCultureKey, this.Cultures);

    /// <summary>Gets the default frontend culture configuration.</summary>
    /// <value>The default frontend culture.</value>
    public virtual CultureElement DefaultBackendCulture => ResourcesConfig.GetDefaultCultureElement(this.DefaultBackendCultureKey, this.BackendCultures);

    /// <summary>
    /// This is a flag set in order to normalize localizable items on next startup after languages are added/removed.
    /// </summary>
    [ConfigurationProperty("mustNormalizeItems", DefaultValue = false)]
    public virtual bool MustNormalizeItems
    {
      get => (bool) this["mustNormalizeItems"];
      set => this["mustNormalizeItems"] = (object) value;
    }

    /// <summary>Gets or sets the name of the default culture.</summary>
    [ConfigurationProperty("fallback", DefaultValue = true)]
    public virtual bool Fallback
    {
      get => (bool) this["fallback"];
      set => this["fallback"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [ConfigurationProperty("providers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>
    /// Gets or sets the key of the default URL localization strategy.
    /// </summary>
    [ConfigurationProperty("defaultUrlLocalizationStrategyKey", DefaultValue = "")]
    public virtual string DefaultUrlLocalizationStrategyKey
    {
      get => (string) this["defaultUrlLocalizationStrategyKey"];
      set => this["defaultUrlLocalizationStrategyKey"] = (object) value;
    }

    /// <summary>
    /// Represents a map between a culture and localization strategy settings
    /// </summary>
    [ConfigurationProperty("urlLocalizationStrategySettings")]
    public virtual ConfigElementDictionary<string, UrlLocalizationStrategyElement> UrlLocalizationStrategies => (ConfigElementDictionary<string, UrlLocalizationStrategyElement>) this["urlLocalizationStrategySettings"];

    /// <summary>Gets the default frontend culture configuration.</summary>
    /// <value>The default frontend culture.</value>
    public virtual Type DefaultUrlLocalizationStrategy
    {
      get
      {
        UrlLocalizationStrategyElement localizationStrategyElement;
        return this.UrlLocalizationStrategies.TryGetValue(this.DefaultUrlLocalizationStrategyKey, out localizationStrategyElement) ? localizationStrategyElement.UrlLocalizationStrategyType : (Type) null;
      }
    }

    /// <summary>
    /// Gets or sets the read only mode of the Lables and messages.
    /// </summary>
    [ConfigurationProperty("LabelsAndMessagesReadOnly", DefaultValue = false)]
    [ObjectInfo(Description = "When set to false Labels & Messages screen is hidden from the UI.", Title = "LabelsAndMessagesReadOnly")]
    public bool LabelsAndMessagesReadOnly
    {
      get => (bool) this[nameof (LabelsAndMessagesReadOnly)];
      set => this[nameof (LabelsAndMessagesReadOnly)] = (object) value;
    }

    /// <summary>Specifies if the backend has more than one language</summary>
    public bool IsBackendMultilingual => this.BackendCultures.Count > 1;

    /// <summary>
    /// Returns both the frontend and backend UI cultures without duplicates
    /// </summary>
    public CultureInfo[] FrontendAndBackendCultures => this.AllCultures.Select<KeyValuePair<string, ResourcesConfig.CultureInfoWrapper>, CultureInfo>((Func<KeyValuePair<string, ResourcesConfig.CultureInfoWrapper>, CultureInfo>) (c => c.Value.Culture)).ToArray<CultureInfo>();

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      UrlLocalizationStrategyElement localizationStrategyElement;
      if (oldVersion.Build < 3860 && this.IncludeSubfoderPrefixForDefaultLanguage && this.UrlLocalizationStrategies.TryGetValue("SubFolderUrlLocalizationStrategy", out localizationStrategyElement))
        localizationStrategyElement.Parameters.Add("includeSubfoderPrefixForDefaultLanguage", "true");
      if (oldVersion.Build >= SitefinityVersion.Sitefinity13_1.Build || this.Cultures.Count <= 1)
        return;
      this.DefaultCultureKey = string.Empty;
    }

    protected virtual void UpgradeFrom1210()
    {
    }

    /// <summary>
    /// Called when a section is updated internally from additional source, e.g database.
    /// Clears internally cached cultures, so they will be populated again after the config is loaded from the database.
    /// </summary>
    protected internal override void OnSectionChanged()
    {
      base.OnSectionChanged();
      this.allCultures.Reset();
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      if (this.Providers.Count == 0)
        this.Providers.Add(new DataProviderSettings((ConfigElement) this.Providers)
        {
          Name = "XmlDataProvider",
          Description = "XML data provider for localizable resources.",
          ProviderType = typeof (XmlResourceDataProvider)
        });
      CultureElement element1 = new CultureElement((ConfigElement) this.Cultures)
      {
        Key = CulturesConfig.GenerateCultureKey(this.initialDefaultCulture, this.initialDefaultCulture),
        Culture = this.initialDefaultCulture.Name,
        UICulture = this.initialDefaultCulture.Name
      };
      this.DefaultCultureKey = element1.Key;
      this.Cultures.Add(element1);
      CultureElement element2 = new CultureElement((ConfigElement) this.BackendCultures)
      {
        Key = CulturesConfig.GenerateCultureKey(this.initialDefaultCulture, this.initialDefaultCulture),
        Culture = this.initialDefaultCulture.Name,
        UICulture = this.initialDefaultCulture.Name
      };
      this.BackendCultures.Add(element2);
      this.DefaultBackendCultureKey = element2.Key;
      this.InitializeStrategies();
    }

    private void InitializeStrategies()
    {
      UrlLocalizationStrategyElement element = new UrlLocalizationStrategyElement((ConfigElement) this.UrlLocalizationStrategies);
      element.UrlLocalizationStrategyType = typeof (SubFolderUrlLocalizationStrategy);
      element.UrlLocalizationStrategyName = typeof (SubFolderUrlLocalizationStrategy).Name;
      this.UrlLocalizationStrategies.Add(element);
      this.DefaultUrlLocalizationStrategyKey = element.UrlLocalizationStrategyName;
      this.UrlLocalizationStrategies.Add(new UrlLocalizationStrategyElement((ConfigElement) this.UrlLocalizationStrategies)
      {
        UrlLocalizationStrategyType = typeof (DomainUrlLocalizationStrategy),
        UrlLocalizationStrategyName = typeof (DomainUrlLocalizationStrategy).Name
      });
    }

    /// <summary>Returns a default culture element by a specified key</summary>
    /// <param name="defaultKey">default key</param>
    /// <param name="cultures">dictionary of cultures</param>
    /// <returns></returns>
    private static CultureElement GetDefaultCultureElement(
      string defaultKey,
      ConfigElementDictionary<string, CultureElement> cultures)
    {
      if (cultures.Count > 0)
      {
        if (!string.IsNullOrEmpty(defaultKey))
        {
          CultureElement defaultCultureElement = (CultureElement) null;
          if (cultures.TryGetValue(defaultKey, out defaultCultureElement))
            return defaultCultureElement;
          string str = string.Join(", ", cultures.Keys.Select<string, string>((Func<string, string>) (s => "'" + s + "'")).ToArray<string>());
          throw new KeyNotFoundException(string.Format("Default culture key not found. It is configured as '{0}'. Existing culture key names are: {1}.", (object) defaultKey, (object) str));
        }
        if (cultures.Count == 1)
          return cultures.Values.First<CultureElement>();
      }
      return (CultureElement) null;
    }

    [ConfigurationProperty("customCultures")]
    [Browsable(false)]
    internal IDictionary<int, string> CustomCultures => (IDictionary<int, string>) this["customCultures"];

    internal void CheckCustomCulture(CultureInfo culture)
    {
      if (culture.LCID != 4096)
        return;
      int num1 = 0;
      foreach (KeyValuePair<int, string> customCulture in (IEnumerable<KeyValuePair<int, string>>) this.CustomCultures)
      {
        if (culture.Name == customCulture.Value)
          return;
        num1 = customCulture.Key;
      }
      if (num1 == 0)
        num1 = 100000;
      int num2;
      this.CustomCultures.Add(num2 = num1 + 1, culture.Name);
    }

    internal Dictionary<string, ResourcesConfig.CultureInfoWrapper> AllCultures => this.allCultures.Value;

    private Dictionary<string, ResourcesConfig.CultureInfoWrapper> LoadAllCultures()
    {
      Dictionary<string, ResourcesConfig.CultureInfoWrapper> dictionary = new Dictionary<string, ResourcesConfig.CultureInfoWrapper>();
      foreach (CultureElement culture in (IEnumerable<CultureElement>) this.Cultures.Values)
      {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture.UICulture);
        if (!dictionary.ContainsKey(cultureInfo.Name))
          dictionary.Add(cultureInfo.Name, new ResourcesConfig.CultureInfoWrapper(culture, cultureInfo));
      }
      foreach (CultureElement culture in (IEnumerable<CultureElement>) this.BackendCultures.Values)
      {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture.UICulture);
        if (!dictionary.ContainsKey(cultureInfo.Name))
          dictionary.Add(cultureInfo.Name, new ResourcesConfig.CultureInfoWrapper(culture, cultureInfo));
      }
      return dictionary;
    }

    internal CultureInfo DefaultCultureInfo => this.DefaultCulture != null ? this.AllCultures[this.DefaultCulture.UICulture].Culture : CultureInfo.InvariantCulture;

    internal static string GetCultureSuffix(CultureInfo culture)
    {
      ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
      ResourcesConfig.CultureInfoWrapper cultureInfoWrapper;
      return resourcesConfig.AllCultures.TryGetValue(culture.Name, out cultureInfoWrapper) && !resourcesConfig.DefaultCultureInfo.Equals((object) cultureInfoWrapper.Culture) ? cultureInfoWrapper.FieldSufix : string.Empty;
    }

    internal static bool TryGetCultureFromSuffix(string cultureSufix, out CultureInfo culture)
    {
      if (string.IsNullOrEmpty(cultureSufix))
      {
        culture = AppSettings.CurrentSettings.GetCultureByName(cultureSufix);
        return true;
      }
      List<ResourcesConfig.CultureInfoWrapper> list = Config.Get<ResourcesConfig>().AllCultures.Values.Where<ResourcesConfig.CultureInfoWrapper>((Func<ResourcesConfig.CultureInfoWrapper, bool>) (c => c.FieldSufix == cultureSufix)).ToList<ResourcesConfig.CultureInfoWrapper>();
      if (list.Count > 0)
      {
        ResourcesConfig.CultureInfoWrapper cultureInfoWrapper;
        if (list.Count == 1)
          cultureInfoWrapper = list[0];
        else if (Bootstrapper.IsReady)
        {
          CultureInfo[] siteCultures = SystemManager.CurrentContext.CurrentSite.PublicContentCultures;
          cultureInfoWrapper = list.Where<ResourcesConfig.CultureInfoWrapper>((Func<ResourcesConfig.CultureInfoWrapper, bool>) (c => ((IEnumerable<CultureInfo>) siteCultures).Contains<CultureInfo>(c.Culture))).FirstOrDefault<ResourcesConfig.CultureInfoWrapper>();
        }
        else
          cultureInfoWrapper = list[0];
        if (cultureInfoWrapper != null)
        {
          culture = cultureInfoWrapper.Culture;
          return true;
        }
      }
      culture = (CultureInfo) null;
      return false;
    }

    internal class Constants
    {
      internal const string XmlProviderName = "XmlDataProvider";
    }

    internal class CultureInfoWrapper
    {
      private readonly CultureInfo culture;
      private readonly string fieldSufix;

      public CultureInfoWrapper(CultureElement culture)
        : this(culture, CultureInfo.GetCultureInfo(culture.UICulture))
      {
      }

      public CultureInfoWrapper(CultureElement culture, CultureInfo cultureInfo)
      {
        this.culture = cultureInfo;
        this.fieldSufix = culture.FieldSuffix;
        if (!this.fieldSufix.IsNullOrEmpty())
          return;
        this.fieldSufix = this.GenerateSuffix();
      }

      public CultureInfo Culture => this.culture;

      public string FieldSufix => this.fieldSufix;

      private string GenerateSuffix() => (!this.culture.IsNeutralCulture || this.culture.Name.IndexOf("-") >= 0 ? this.culture.Name : (this.culture.LCID != 20 ? this.culture.TwoLetterISOLanguageName : "no")).Replace("-", "_").ToLowerInvariant();
    }
  }
}
