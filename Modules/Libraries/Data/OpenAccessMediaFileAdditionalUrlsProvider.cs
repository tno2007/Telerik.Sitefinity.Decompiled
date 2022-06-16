// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Data.OpenAccessMediaFileAdditionalUrlsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Data
{
  /// <summary>
  /// Implements data layer for additional URLs to media files, using OpenAccess ORM.
  /// </summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessMediaFileAdditionalUrlsProvider : 
    DataProviderBase,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <inheritdoc />
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new MediaFileAdditionalUrlsMetadataSource(context);

    /// <inheritdoc />
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (MediaFileAdditionalUrl)
    };

    /// <inheritdoc />
    public override string RootKey => nameof (OpenAccessMediaFileAdditionalUrlsProvider);

    internal virtual MediaFileAdditionalUrl CreateMediaFileAdditionalUrl(
      string url,
      string providerName,
      Guid itemId,
      bool masterHasIt,
      Guid liveItemId,
      bool redirect)
    {
      MediaFileAdditionalUrl entity = new MediaFileAdditionalUrl();
      entity.Url = url;
      entity.ProviderName = providerName;
      entity.ItemId = itemId;
      entity.MasterHasIt = masterHasIt;
      entity.LiveItemId = liveItemId;
      entity.RedirectToDefault = redirect;
      this.GetContext().Add((object) entity);
      return entity;
    }

    internal virtual IQueryable<MediaFileAdditionalUrl> GetMediaFileAdditionalUrls() => SitefinityQuery.Get<MediaFileAdditionalUrl>((DataProviderBase) this, MethodBase.GetCurrentMethod());

    internal virtual void Delete(IEnumerable<MediaFileAdditionalUrl> toDelete) => this.GetContext().Delete((IEnumerable) toDelete);

    /// <inheritdoc />
    protected override bool InitializeDefaultData()
    {
      bool flag = base.InitializeDefaultData();
      if (Config.Get<LibrariesConfig>().MediaFileAdditionalUrls.MediaFilesAdditionalUrlsInitialized)
        return flag;
      Dictionary<string, MediaFileAdditionalUrl> dictionary1 = this.GetMediaFileAdditionalUrls().ToDictionary<MediaFileAdditionalUrl, string, MediaFileAdditionalUrl>((Func<MediaFileAdditionalUrl, string>) (mfu => mfu.Url), (Func<MediaFileAdditionalUrl, MediaFileAdditionalUrl>) (mfu => mfu));
      Dictionary<string, MediaFileAdditionalUrl> dictionary2 = new Dictionary<string, MediaFileAdditionalUrl>();
      foreach (string providerName in LibrariesManager.GetManager().GetProviderNames(ProviderBindingOptions.NoFilter))
        this.GetProviderUrls(providerName, dictionary1, dictionary2);
      if (dictionary1.Any<KeyValuePair<string, MediaFileAdditionalUrl>>())
        this.Delete((IEnumerable<MediaFileAdditionalUrl>) dictionary1.Values);
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      section.MediaFileAdditionalUrls.MediaFilesAdditionalUrlsInitialized = true;
      bool disableSecurityChecks = ConfigProvider.DisableSecurityChecks;
      ConfigProvider.DisableSecurityChecks = true;
      try
      {
        manager.SaveSection((ConfigSection) section);
      }
      finally
      {
        ConfigProvider.DisableSecurityChecks = disableSecurityChecks;
      }
      return flag || dictionary1.Any<KeyValuePair<string, MediaFileAdditionalUrl>>() || dictionary2.Any<KeyValuePair<string, MediaFileAdditionalUrl>>();
    }

    private void GetProviderUrls(
      string providerName,
      Dictionary<string, MediaFileAdditionalUrl> toDelete,
      Dictionary<string, MediaFileAdditionalUrl> insertedItems)
    {
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      IQueryable<MediaContent> mediaItems = manager.GetMediaItems();
      Expression<Func<MediaContent, bool>> predicate = (Expression<Func<MediaContent, bool>>) (item => (int) item.Status == 0 || (int) item.Status == 2 && item.Visible);
      foreach (MediaContent mediaContent in (IEnumerable<MediaContent>) mediaItems.Where<MediaContent>(predicate))
      {
        foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) mediaContent.MediaFileLinks)
        {
          if (mediaFileLink != null && mediaFileLink.Urls != null && mediaFileLink.Urls.Count > 0)
          {
            foreach (MediaFileUrl mediaFileUrl in mediaContent.MediaFileUrls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (url => !url.IsDefault)))
            {
              string str = LibrariesManager.CanonifyUrl(mediaFileUrl.Url);
              if (!manager.Provider.IsStandardMediaContentUrl(str, mediaContent))
              {
                Guid itemId = mediaContent.OriginalContentId == Guid.Empty ? mediaContent.Id : mediaContent.OriginalContentId;
                MediaFileAdditionalUrl fileAdditionalUrl;
                if (insertedItems.ContainsKey(str))
                {
                  if (!(insertedItems[str].ProviderName != providerName) && !(insertedItems[str].ItemId != itemId))
                    fileAdditionalUrl = insertedItems[str];
                  else
                    continue;
                }
                else
                {
                  if (toDelete.ContainsKey(str))
                  {
                    fileAdditionalUrl = toDelete[str];
                    fileAdditionalUrl.ProviderName = providerName;
                    fileAdditionalUrl.ItemId = itemId;
                    fileAdditionalUrl.MasterHasIt = false;
                    fileAdditionalUrl.LiveItemId = Guid.Empty;
                    fileAdditionalUrl.RedirectToDefault = mediaFileUrl.RedirectToDefault;
                    toDelete.Remove(str);
                  }
                  else
                    fileAdditionalUrl = this.CreateMediaFileAdditionalUrl(str, providerName, itemId, false, Guid.Empty, mediaFileUrl.RedirectToDefault);
                  insertedItems[str] = fileAdditionalUrl;
                }
                if (mediaContent.Status == ContentLifecycleStatus.Master)
                  fileAdditionalUrl.MasterHasIt = true;
                if (mediaContent.Status == ContentLifecycleStatus.Live)
                  fileAdditionalUrl.LiveItemId = mediaContent.Id;
              }
            }
          }
        }
      }
    }
  }
}
