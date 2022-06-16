// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.SiteSettingsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.SiteSettings.Model;

namespace Telerik.Sitefinity.SiteSettings
{
  /// <summary>Managers class for the Site setting.</summary>
  internal class SiteSettingsManager : ManagerBase<SiteSettingsDataProvider>
  {
    private static readonly object settingsCacheSync = new object();
    internal const string SitePolicyTypeName = "sitepolicy";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSettings.SiteSettingsManager" /> class.
    /// </summary>
    public SiteSettingsManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSettings.SiteSettingsManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public SiteSettingsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSettings.SiteSettingsManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public SiteSettingsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    internal SiteSetting CreateSetting() => this.Provider.CreateSetting();

    internal SiteSetting CreateSetting(Guid settingId) => this.Provider.CreateSetting(settingId);

    internal void Delete(SiteSetting setting) => this.Provider.Delete(setting);

    internal SiteSetting GetSetting(Guid settingId) => this.Provider.GetSetting(settingId);

    internal IQueryable<SiteSetting> GetSettings() => this.Provider.GetSettings();

    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<SiteSettingsConfig>().DefaultProvider);

    public override string ModuleName => string.Empty;

    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<SiteSettingsConfig>().Providers;

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Get an instance of the SiteSettingsManager using the default provider
    /// </summary>
    /// <returns>Instance of SiteSettingsManager</returns>
    public static SiteSettingsManager GetManager() => ManagerBase<SiteSettingsDataProvider>.GetManager<SiteSettingsManager>();

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static SiteSettingsManager GetManager(string providerName) => ManagerBase<SiteSettingsDataProvider>.GetManager<SiteSettingsManager>(providerName);

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static SiteSettingsManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<SiteSettingsDataProvider>.GetManager<SiteSettingsManager>(providerName, transactionName);
    }

    internal static bool HasSetting<T>(string policyType, string policyName) => (object) SiteSettingsManager.GetSetting<T>(policyType, policyName) != null;

    internal static bool HasSetting(Type contractType, string policyType, string policyName) => SiteSettingsManager.GetSetting(contractType, policyType, policyName) != null;

    internal static void DeleteSetting<T>(string policyType, string policyName) => SiteSettingsManager.DeleteSetting(typeof (T), policyType, policyName);

    internal static void DeleteSetting(Type contractType, string policyType, string policyName)
    {
      string name = contractType.Name;
      SiteSettingsManager manager = SiteSettingsManager.GetManager();
      SiteSetting setting = manager.GetSettings().Where<SiteSetting>((Expression<Func<SiteSetting, bool>>) (s => s.Name == name && s.PolicyType == policyType && s.PolicyName == policyName)).SingleOrDefault<SiteSetting>();
      if (setting == null)
        return;
      manager.Delete(setting);
      manager.SaveChanges();
    }

    internal static T GetSetting<T>(string policyType, string policyName)
    {
      object setting = SiteSettingsManager.GetSetting(typeof (T), policyType, policyName);
      return setting != null ? (T) setting : default (T);
    }

    internal static object GetSetting(Type contractType, string policyType, string policyName)
    {
      string name = contractType.Name;
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      string settingCacheKey = SiteSettingsManager.GetSettingCacheKey(name, policyType, policyName);
      object obj = cacheManager[settingCacheKey];
      if (obj == null)
      {
        lock (SiteSettingsManager.settingsCacheSync)
        {
          obj = cacheManager[settingCacheKey];
          if (obj == null)
          {
            SiteSetting siteSetting = SiteSettingsManager.GetManager().GetSettings().Where<SiteSetting>((Expression<Func<SiteSetting, bool>>) (s => s.Name == name && s.PolicyType == policyType && s.PolicyName == policyName)).SingleOrDefault<SiteSetting>();
            if (siteSetting != null)
            {
              obj = SiteSettingsManager.Deserialize(siteSetting.Data, contractType);
              cacheManager.Add(settingCacheKey, obj, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteSetting), siteSetting.Id), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
            }
            else
            {
              obj = new object();
              cacheManager.Add(settingCacheKey, obj, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteSetting), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
            }
          }
        }
      }
      return obj != null && contractType.IsAssignableFrom(obj.GetType()) ? obj : (object) null;
    }

    internal static void SaveSetting<T>(string policyType, string policyName, object settingObject) => SiteSettingsManager.SaveSetting(typeof (T), policyType, policyName, settingObject);

    internal static void SaveSetting(
      Type contractType,
      string policyType,
      string policyName,
      object settingObject)
    {
      string name = contractType.Name;
      SiteSettingsManager manager = SiteSettingsManager.GetManager();
      SiteSetting siteSetting = manager.GetSettings().Where<SiteSetting>((Expression<Func<SiteSetting, bool>>) (s => s.Name == name && s.PolicyType == policyType && s.PolicyName == policyName)).SingleOrDefault<SiteSetting>();
      if (siteSetting == null)
      {
        siteSetting = manager.CreateSetting();
        siteSetting.Name = name;
        siteSetting.PolicyType = policyType;
        siteSetting.PolicyName = policyName;
      }
      siteSetting.Data = SiteSettingsManager.Serialize(settingObject, contractType);
      manager.SaveChanges();
    }

    private static string Serialize(object obj, Type contractType)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(contractType).WriteObject((Stream) memoryStream, obj);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
    }

    private static object Deserialize(string jsonString, Type contractType)
    {
      using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        return new DataContractJsonSerializer(contractType).ReadObject((Stream) memoryStream);
    }

    private static string GetSettingCacheKey(string name, string policyType, string policyName) => "sf_settings_" + name + "_" + policyType + "_" + policyName;
  }
}
