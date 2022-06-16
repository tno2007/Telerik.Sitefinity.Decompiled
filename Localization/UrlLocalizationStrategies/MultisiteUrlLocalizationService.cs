// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.MultisiteUrlLocalizationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  internal class MultisiteUrlLocalizationService : UrlLocalizationService
  {
    private readonly string strategyKeyPrefix = "sf_url_loc_strategy_";
    private readonly object sitesCacheSync = new object();

    public MultisiteUrlLocalizationService() => CacheDependency.Subscribe(typeof (ResourcesConfig), new ChangedCallback(this.ConfigChangedCallback));

    public override IUrlLocalizationStrategy CurrentUrlLocalizationStrategy
    {
      get
      {
        ISite currentSite = SystemManager.CurrentContext.MultisiteContext.CurrentSite;
        string key = this.strategyKeyPrefix + currentSite.Id.ToString();
        ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
        if (!(cacheManager[key] is IUrlLocalizationStrategy localizationStrategy1))
        {
          lock (this.sitesCacheSync)
          {
            if (!(cacheManager[key] is IUrlLocalizationStrategy localizationStrategy1))
            {
              AppSettings currentSettings = AppSettings.CurrentSettings;
              if (currentSite.Cultures.Length > 1)
              {
                MultisiteUrlLocalizationContext context = new MultisiteUrlLocalizationContext(currentSite);
                IUrlLocalizationStrategySettings strategySettings = context.StrategySettings;
                if (strategySettings != null)
                {
                  localizationStrategy1 = (IUrlLocalizationStrategy) Activator.CreateInstance(strategySettings.UrlLocalizationStrategyType);
                  localizationStrategy1.Initialize((IUrlLocalizationContext) context);
                }
              }
              if (localizationStrategy1 == null)
                localizationStrategy1 = this.GetNoneUrlLocalizationStrategy();
              cacheManager.Add(key, (object) localizationStrategy1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Site), currentSite.Id), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
            }
          }
        }
        return localizationStrategy1;
      }
    }

    private void ConfigChangedCallback(
      ICacheDependencyHandler caller,
      Type itemType,
      string itemKey)
    {
      string key = this.strategyKeyPrefix + SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id.ToString();
      SystemManager.GetCacheManager(CacheManagerInstance.Internal).Remove(key);
    }
  }
}
