// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.MediaFileAdditionalUrlsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class MediaFileAdditionalUrlsManager : 
    ManagerBase<OpenAccessMediaFileAdditionalUrlsProvider>
  {
    private static readonly object CacheLock = new object();
    private const string MediaFilesAdditionalUrlsKey = "sf_media_file_additional_urls_cache";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaFileAdditionalUrlsManager" /> class.
    /// </summary>
    public MediaFileAdditionalUrlsManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaFileAdditionalUrlsManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public MediaFileAdditionalUrlsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.MediaFileAdditionalUrlsManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public MediaFileAdditionalUrlsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    public static MediaFileAdditionalUrlsManager GetManager(
      string providerName)
    {
      return ManagerBase<OpenAccessMediaFileAdditionalUrlsProvider>.GetManager<MediaFileAdditionalUrlsManager>(providerName);
    }

    public static MediaFileAdditionalUrlsManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<OpenAccessMediaFileAdditionalUrlsProvider>.GetManager<MediaFileAdditionalUrlsManager>(providerName, transactionName);
    }

    public override string ModuleName => "MediaFileAdditionalURLs";

    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => "OpenAccessMediaFileAdditionalUrlsProvider");

    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
    {
      get
      {
        ConfigElementDictionary<string, DataProviderSettings> parent = new ConfigElementDictionary<string, DataProviderSettings>();
        parent.Add(new DataProviderSettings((ConfigElement) parent)
        {
          Name = "OpenAccessMediaFileAdditionalUrlsProvider",
          Description = "A provider that stores additional URLs data for media files in database using OpenAccess ORM.",
          ProviderType = typeof (OpenAccessMediaFileAdditionalUrlsProvider),
          Parameters = new NameValueCollection()
        });
        return parent;
      }
    }

    internal static string GetRedirectUrl(string path)
    {
      if (!Config.Get<LibrariesConfig>().MediaFileAdditionalUrls.AdditionalUrlsToFiles)
        return (string) null;
      path = LibrariesManager.CanonifyUrl(path);
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      if (!(cacheManager["sf_media_file_additional_urls_cache"] is IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData> dictionary1))
      {
        lock (MediaFileAdditionalUrlsManager.CacheLock)
        {
          if (!(cacheManager["sf_media_file_additional_urls_cache"] is IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData> dictionary1))
          {
            MediaFileAdditionalUrlsManager.LoadCache(cacheManager);
            dictionary1 = cacheManager["sf_media_file_additional_urls_cache"] as IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData>;
          }
        }
      }
      MediaFileAdditionalUrlsManager.AdditionalUrlData additionalUrlData;
      if (!dictionary1.TryGetValue(path, out additionalUrlData))
        return (string) null;
      using (LibrariesManager manager = LibrariesManager.GetManager(additionalUrlData.ProviderName))
      {
        MediaContent mediaItem = manager.GetMediaItem(additionalUrlData.Id);
        return mediaItem == null ? (string) null : mediaItem.ResolveMediaUrl(false, (CultureInfo) null);
      }
    }

    private static void LoadCache(ICacheManager cacheManager)
    {
      IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData> dictionary = (IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData>) null;
      using (MediaFileAdditionalUrlsManager additionalUrlsManager = new MediaFileAdditionalUrlsManager())
        dictionary = (IDictionary<string, MediaFileAdditionalUrlsManager.AdditionalUrlData>) additionalUrlsManager.GetMediaFileAdditionalUrls().Where<MediaFileAdditionalUrl>((Expression<Func<MediaFileAdditionalUrl, bool>>) (mfu => mfu.LiveItemId != Guid.Empty)).ToDictionary<MediaFileAdditionalUrl, string, MediaFileAdditionalUrlsManager.AdditionalUrlData>((Func<MediaFileAdditionalUrl, string>) (e => e.Url), (Func<MediaFileAdditionalUrl, MediaFileAdditionalUrlsManager.AdditionalUrlData>) (e => new MediaFileAdditionalUrlsManager.AdditionalUrlData()
        {
          ProviderName = e.ProviderName,
          Id = e.LiveItemId,
          Redirect = e.RedirectToDefault
        }));
      cacheManager.Add("sf_media_file_additional_urls_cache", (object) dictionary, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (MediaFileAdditionalUrl), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
    }

    internal virtual MediaFileAdditionalUrl CreateMediaFileAdditionalUrl(
      string url,
      string providerName,
      Guid itemId,
      bool masterHasIt,
      Guid liveItemId,
      bool redirect)
    {
      return this.Provider.CreateMediaFileAdditionalUrl(url, providerName, itemId, masterHasIt, liveItemId, redirect);
    }

    internal virtual IQueryable<MediaFileAdditionalUrl> GetMediaFileAdditionalUrls() => this.Provider.GetMediaFileAdditionalUrls();

    internal virtual void Delete(IEnumerable<MediaFileAdditionalUrl> toDelete) => this.Provider.Delete(toDelete);

    private struct AdditionalUrlData
    {
      public string ProviderName;
      public Guid Id;
      public bool Redirect;
    }
  }
}
