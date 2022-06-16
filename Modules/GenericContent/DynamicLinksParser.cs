// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.DynamicLinksParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.DynamicModules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Provides functionality for resolving links in the Html content
  /// </summary>
  public class DynamicLinksParser : IContentFilter, IHtmlFilter
  {
    private bool preserveOriginalValue;
    private bool? resolveAsAbsolute = new bool?(false);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.DynamicLinksParser" /> class.
    /// </summary>
    public DynamicLinksParser()
      : this(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.DynamicLinksParser" /> class.
    /// </summary>
    /// <param name="preserveOriginalValue">if set to <c>true</c> [preserve original value].</param>
    public DynamicLinksParser(bool preserveOriginalValue)
      : this(preserveOriginalValue, new bool?())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.DynamicLinksParser" /> class.
    /// </summary>
    /// <param name="preserveOriginalValue">if set to <c>true</c> [preserve original value].</param>
    public DynamicLinksParser(bool preserveOriginalValue, bool? resolveAsAbsolute)
    {
      this.preserveOriginalValue = preserveOriginalValue;
      this.resolveAsAbsolute = resolveAsAbsolute;
    }

    /// <summary>Applies the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual string Apply(Content content, string html) => this.Apply(html);

    /// <inheritdoc />
    public virtual string Apply(string html)
    {
      bool resolveAsAbsoluteUrl = ((int) this.resolveAsAbsolute ?? (Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls ? 1 : 0)) != 0;
      return LinkParser.ResolveLinks(html, new Telerik.Sitefinity.Web.Utilities.GetItemUrl(this.GetItemUrl), (ResolveUrl) null, this.preserveOriginalValue, resolveAsAbsoluteUrl);
    }

    /// <summary>Resolves the item URL.</summary>
    /// <param name="key">The key.</param>
    /// <param name="id">The id.</param>
    /// <param name="status">status</param>
    /// <param name="resolveAsAbsoluteUrl">if set to <c>true</c> [resolve as absolute URL].</param>
    /// <returns></returns>
    protected virtual string GetItemUrl(
      string key,
      Guid id,
      bool resolveAsAbsoluteUrl,
      ContentLifecycleStatus status)
    {
      return DynamicLinksParser.GetContentUrl(key, id, resolveAsAbsoluteUrl, status);
    }

    public static string GetContentUrl(
      string key,
      Guid id,
      bool resolveAsAbsoluteUrl,
      ContentLifecycleStatus status)
    {
      string str1 = (string) null;
      NameValueCollection nameValueCollection = (NameValueCollection) null;
      if (!key.IsNullOrEmpty())
      {
        string[] strArray = key.Split(new string[3]
        {
          "|",
          "%7C",
          "%7C".ToLower()
        }, StringSplitOptions.None);
        if (strArray.Length > 1)
        {
          key = strArray[0];
          for (int index = 1; index < strArray.Length; ++index)
          {
            string str2 = strArray[index];
            int length = str2.IndexOf(":");
            int num = 1;
            if (length == -1)
            {
              length = str2.IndexOf("%3A", StringComparison.OrdinalIgnoreCase);
              num = "%3A".Length;
            }
            if (length > -1)
            {
              if (nameValueCollection == null)
                nameValueCollection = new NameValueCollection();
              nameValueCollection.Add(str2.Substring(0, length), str2.Substring(length + num));
            }
            else if (str1 == null)
              str1 = str2;
          }
        }
      }
      if (!(key == "images") && !(key == "Telerik.Sitefinity.Libraries.Model.Image"))
      {
        if (!(key == "videos") && !(key == "Telerik.Sitefinity.Libraries.Model.Video"))
        {
          if (key == "documents" || key == "Telerik.Sitefinity.Libraries.Model.Document")
          {
            LibrariesManager manager = LibrariesManager.GetManager(str1);
            using (new ElevatedModeRegion((IManager) manager))
            {
              IQueryable<Document> documents = manager.GetDocuments();
              Document mediaContent = DynamicLinksParser.GetItem<Document>(id, status, documents);
              if (mediaContent != null)
                return mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
            }
          }
          else
          {
            string name = nameValueCollection != null ? nameValueCollection["lng"] : string.Empty;
            Type itemType = TypeResolutionService.ResolveType(key, false);
            if (itemType != (Type) null)
            {
              IManager manager;
              if (ManagerBase.TryGetMappedManager(itemType, str1, out manager))
              {
                IDataItem dataItem;
                using (new ElevatedModeRegion(manager))
                  dataItem = manager.GetItem(itemType, id) as IDataItem;
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
                IContentLocationService contentLocationService = SystemManager.GetContentLocationService();
                IContentItemLocation contentItemLocation = contentLocationService.GetItemDefaultLocation(dataItem, cultureInfo) ?? contentLocationService.GetItemLocations(dataItem, cultureInfo).FirstOrDefault<IContentItemLocation>();
                if (contentItemLocation != null)
                  return contentItemLocation.ItemAbsoluteUrl;
              }
              return (string) null;
            }
            Guid result;
            if (key == "pages")
            {
              str1 = "SitefinitySiteMap";
              result = SiteInitializer.FrontendRootNodeId;
            }
            else
            {
              if (!Guid.TryParse(key, out result))
                result = Guid.Empty;
              if (str1.IsNullOrEmpty())
                str1 = !(result == SiteInitializer.BackendRootNodeId) ? "SitefinitySiteMap" : Config.Get<PagesConfig>().BackendRootNode;
            }
            SiteMapProvider siteMapProvider = SiteMapBase.GetSiteMapProvider(str1);
            if (siteMapProvider != null)
            {
              using (SiteRegion.FromSiteMapRoot(result, SiteContextResolutionTypes.ByParam))
              {
                CultureInfo culture = name.IsNullOrEmpty() ? SystemManager.CurrentContext.Culture : CultureInfo.GetCultureInfo(name);
                string url = string.Empty;
                if (siteMapProvider is SiteMapBase)
                {
                  SiteMapNode siteMapNodeFromKey = ((SiteMapBase) siteMapProvider).FindSiteMapNodeFromKey(id.ToString(), false);
                  if (siteMapNodeFromKey != null)
                  {
                    if (siteMapNodeFromKey is PageSiteNode pageSiteNode && !((IEnumerable<CultureInfo>) pageSiteNode.AvailableLanguages).Contains<CultureInfo>(culture))
                      culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
                    using (new CultureRegion(culture))
                      url = siteMapNodeFromKey.Url;
                  }
                }
                else
                  url = siteMapProvider.FindSiteMapNodeFromKey(id.ToString()).Url;
                if (!url.IsNullOrEmpty())
                  return UrlPath.ResolveUrl(url, resolveAsAbsoluteUrl);
              }
            }
          }
        }
        else
        {
          LibrariesManager manager = LibrariesManager.GetManager(str1);
          using (new ElevatedModeRegion((IManager) manager))
          {
            IQueryable<Video> videos = manager.GetVideos();
            Video mediaContent = DynamicLinksParser.GetItem<Video>(id, status, videos);
            if (mediaContent != null)
              return mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
          }
        }
      }
      else
      {
        LibrariesManager manager = LibrariesManager.GetManager(str1);
        using (new ElevatedModeRegion((IManager) manager))
        {
          IQueryable<Image> images = manager.GetImages();
          Image mediaContent = DynamicLinksParser.GetItem<Image>(id, status, images);
          if (mediaContent != null)
          {
            string name = nameValueCollection != null ? nameValueCollection["tmb"] : string.Empty;
            return !name.IsNullOrEmpty() ? mediaContent.ResolveThumbnailUrl(name, resolveAsAbsoluteUrl) : mediaContent.ResolveMediaUrl(resolveAsAbsoluteUrl, (CultureInfo) null);
          }
        }
      }
      return "#Link.Not.Resolved#";
    }

    public static T GetItem<T>(Guid id, ContentLifecycleStatus status, IQueryable<T> query) where T : Content
    {
      switch (status)
      {
        case ContentLifecycleStatus.Master:
          T obj1 = Queryable.FirstOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.Id == id)));
          if ((object) obj1 == null)
            return default (T);
          if (obj1.Status == ContentLifecycleStatus.Master)
            return obj1;
          Guid masterId1 = obj1.OriginalContentId;
          return Queryable.FirstOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.Id == masterId1)));
        case ContentLifecycleStatus.Temp:
          T obj2 = Queryable.FirstOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.Id == id)));
          if ((object) obj2 == null)
            return default (T);
          if (obj2.Status == ContentLifecycleStatus.Temp)
            return obj2;
          Guid masterId2 = obj2.Status != ContentLifecycleStatus.Master ? obj2.OriginalContentId : obj2.Id;
          return Queryable.SingleOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.OriginalContentId == masterId2 && (int) i.Status == 1)));
        case ContentLifecycleStatus.Live:
          T obj3 = Queryable.FirstOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.Id == id)));
          if ((object) obj3 == null)
            return default (T);
          Guid liveItemId;
          if (obj3.Status == ContentLifecycleStatus.Live)
          {
            liveItemId = obj3.Id;
            return Queryable.SingleOrDefault<T>(query.Where<T>(PredefinedFilters.PublishedItemsFilter<T>()).Where<T>((Expression<Func<T, bool>>) (i => i.Id == liveItemId)));
          }
          Guid masterId3 = obj3.Status != ContentLifecycleStatus.Master ? obj3.OriginalContentId : obj3.Id;
          T obj4 = Queryable.SingleOrDefault<T>(query.Where<T>((Expression<Func<T, bool>>) (i => i.OriginalContentId == masterId3 && (int) i.Status == 2)));
          if ((object) obj4 == null)
            return default (T);
          liveItemId = obj4.Id;
          return Queryable.SingleOrDefault<T>(query.Where<T>(PredefinedFilters.PublishedItemsFilter<T>()).Where<T>((Expression<Func<T, bool>>) (i => i.Id == liveItemId)));
        default:
          return default (T);
      }
    }
  }
}
