// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  public class ContentLinksManager : 
    ManagerBase<ContentLinksProviderBase>,
    IContentLinksManager,
    IContentLinksManagerProxy
  {
    private static bool initialized;
    private static readonly object contentLinksCacheSync = new object();
    private static CacheProfileElement contentLinksCacheSettings = (CacheProfileElement) null;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager" /> class with the default provider.
    /// </summary>
    public ContentLinksManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public ContentLinksManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public ContentLinksManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Creates the content link from the specified data items
    /// </summary>
    /// <param name="parentDataItem">The parent data item.</param>
    /// <param name="childDataItem">The child data item.</param>
    /// <returns></returns>
    public ContentLink CreateContentLink(
      string componentPropertyName,
      IDataItem parentDataItem,
      IDataItem childDataItem)
    {
      if (parentDataItem == null)
        throw new ArgumentNullException(nameof (parentDataItem));
      if (childDataItem == null)
        throw new ArgumentNullException(nameof (childDataItem));
      string parentItemProviderName = !(parentDataItem.Provider is DataProviderBase provider1) ? string.Empty : provider1.Name;
      string childItemProviderName = !(childDataItem.Provider is DataProviderBase provider2) ? string.Empty : provider2.Name;
      return this.Provider.CreateContentLink(componentPropertyName, parentDataItem.Id, childDataItem.Id, parentItemProviderName, childItemProviderName, parentDataItem.GetType(), childDataItem.GetType());
    }

    /// <summary>Gets the content links.</summary>
    /// <returns></returns>
    public IQueryable<ContentLink> GetContentLinks() => this.Provider.GetContentLinks();

    /// <summary>Deletes the specified content link.</summary>
    /// <param name="contentLink">The content link.</param>
    public void Delete(ContentLink contentLink) => this.Provider.Delete(contentLink);

    /// <summary>Creates the content link.</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public ContentLink CreateContentLink(
      string componentPropertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType)
    {
      return this.Provider.CreateContentLink(componentPropertyName, parentItemId, childItemId, parentItemProviderName, childItemProviderName, parentItemType, childItemType);
    }

    /// <summary>Creates the content link.</summary>
    /// <param name="id">The id.</param>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public ContentLink CreateContentLink(
      Guid id,
      string componentPropertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType)
    {
      return this.Provider.CreateContentLink(id, componentPropertyName, parentItemId, childItemId, parentItemProviderName, childItemProviderName, parentItemType, childItemType);
    }

    /// <summary>Creates the content link.</summary>
    /// <param name="componentPropertyName">Name of the parent property.</param>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemTypeName">Name of the parent item type.</param>
    /// <param name="childItemTypeName">Name of the child item type.</param>
    /// <returns></returns>
    public ContentLink CreateContentLink(
      string componentPropertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      string parentItemTypeName,
      string childItemTypeName)
    {
      Type parentItemType = TypeResolutionService.ResolveType(parentItemTypeName);
      Type childItemType = TypeResolutionService.ResolveType(childItemTypeName);
      return this.CreateContentLink(componentPropertyName, parentItemId, childItemId, parentItemProviderName, childItemProviderName, parentItemType, childItemType);
    }

    /// <summary>Creates the content link.</summary>
    /// <param name="id">The id.</param>
    /// <param name="componentPropertyName">Name of the parent property.</param>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemTypeName">Name of the parent item type.</param>
    /// <param name="childItemTypeName">Name of the child item type.</param>
    /// <returns></returns>
    public ContentLink CreateContentLink(
      Guid id,
      string componentPropertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      string parentItemTypeName,
      string childItemTypeName)
    {
      Type parentItemType = TypeResolutionService.ResolveType(parentItemTypeName);
      Type childItemType = TypeResolutionService.ResolveType(childItemTypeName);
      return this.CreateContentLink(id, componentPropertyName, parentItemId, childItemId, parentItemProviderName, childItemProviderName, parentItemType, childItemType);
    }

    /// <summary>
    /// Creates a content link and sets it to the specified parent data item
    /// If there is already a content link set it is deleted
    /// </summary>
    /// <param name="componentPropertyName"></param>
    /// <param name="parentDataItem"></param>
    /// <param name="childDataItem"></param>
    public void SetContentLink(
      string componentPropertyName,
      IDataItem parentDataItem,
      IDataItem childDataItem)
    {
      ContentLink contentLink = this.GetContentLink(parentDataItem.Id, parentDataItem.GetType(), componentPropertyName);
      if (contentLink != null)
        this.Delete(contentLink);
      this.CreateContentLink(componentPropertyName, parentDataItem, childDataItem);
    }

    /// <summary>
    /// Creates a content link and sets it to the specified parent data item
    /// If there is already a content link set it is deleted
    /// </summary>
    /// <param name="contentLink">The content link.</param>
    public void SetContentLink(ContentLink contentLink)
    {
      ContentLink contentLink1 = this.GetContentLink(contentLink.ParentItemId, contentLink.ParentItemType, contentLink.ComponentPropertyName);
      string componentPropertyName = contentLink.ComponentPropertyName;
      Guid parentItemId = contentLink.ParentItemId;
      Guid childItemId = contentLink.ChildItemId;
      string itemProviderName1 = contentLink.ParentItemProviderName;
      string itemProviderName2 = contentLink.ChildItemProviderName;
      string parentItemType = contentLink.ParentItemType;
      string childItemType = contentLink.ChildItemType;
      if (contentLink1 != null)
        this.Delete(contentLink1);
      this.CreateContentLink(componentPropertyName, parentItemId, childItemId, itemProviderName1, itemProviderName2, parentItemType, childItemType);
    }

    /// <summary>Gets the content links.</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public IQueryable<ContentLink> GetContentLinks(
      Guid parentItemId,
      Type parentItemType,
      string propertyName)
    {
      return this.Provider.GetContentLinks(parentItemId, parentItemType, propertyName);
    }

    /// <summary>
    /// Gets a single content link based on the specified parent properties
    /// </summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public ContentLink GetContentLink(
      Guid parentItemId,
      Type parentItemType,
      string propertyName)
    {
      return this.Provider.GetContentLinks(parentItemId, parentItemType, propertyName).SingleOrDefault<ContentLink>();
    }

    /// <summary>
    /// Gets a single content link based on the specified parent properties
    /// </summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="parentItemTypeName">Name of the parent item type.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public ContentLink GetContentLink(
      Guid parentItemId,
      string parentItemTypeName,
      string propertyName)
    {
      Type parentItemType = TypeResolutionService.ResolveType(parentItemTypeName);
      return this.GetContentLink(parentItemId, parentItemType, propertyName);
    }

    /// <summary>
    /// Gets the common content link information from a cached item. It tries to use provider's native cache.
    /// or if it is disabled tries to use an internal cache.
    /// If no such item exists tries to create one.
    /// <para>If there is no available information - returns null.</para>
    /// </summary>
    /// <param name="contentLinksProvider"></param>
    /// <param name="parentItemId"></param>
    /// <param name="componentPropertyName"></param>
    /// <returns></returns>
    internal static List<IContentLink> GetCachedContentLinks(
      string contentLinksProvider,
      Guid parentItemId,
      string componentPropertyName)
    {
      ContentLinksManager manager = ContentLinksManager.GetManager(contentLinksProvider);
      if (manager.IsCacheEnabledFor(typeof (ContentLink), manager.Provider))
        return ((IEnumerable<IContentLink>) manager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId && cl.ComponentPropertyName == componentPropertyName))).ToList<IContentLink>();
      string key = contentLinksProvider + parentItemId.ToString() + componentPropertyName;
      object obj = ContentLinksManager.ContentLinksCache[key];
      if (obj == null)
      {
        lock (ContentLinksManager.contentLinksCacheSync)
        {
          obj = ContentLinksManager.ContentLinksCache[key];
          if (obj == null)
          {
            bool flag = false;
            IQueryable<ContentLink> source = manager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId && cl.ComponentPropertyName == componentPropertyName));
            if (source != null && source.Count<ContentLink>() > 0)
            {
              List<ContentLinksManager.ContentLinkProxy> contentLinkProxyList = new List<ContentLinksManager.ContentLinkProxy>();
              List<ICacheItemExpiration> cacheItemExpirationList = new List<ICacheItemExpiration>();
              foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source)
              {
                ContentLinksManager.ContentLinkProxy contentLinkProxy = new ContentLinksManager.ContentLinkProxy()
                {
                  ParentItemId = contentLink.ParentItemId,
                  ComponentPropertyName = contentLink.ComponentPropertyName,
                  ChildItemId = contentLink.ChildItemId,
                  ChildItemProviderName = contentLink.ChildItemProviderName
                };
                contentLinkProxyList.Add(contentLinkProxy);
                cacheItemExpirationList.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (ContentLink), parentItemId.ToString()));
              }
              ICacheItemExpiration configuredCacheExpiration = ContentLinksManager.GetConfiguredCacheExpiration();
              cacheItemExpirationList.Add(configuredCacheExpiration);
              ContentLinksManager.ContentLinksCache.Add(key, (object) contentLinkProxyList, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationList.ToArray());
              obj = (object) contentLinkProxyList;
              flag = true;
            }
            if (!flag)
            {
              obj = new object();
              ContentLinksManager.ContentLinksCache.Add(key, obj, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, new List<ICacheItemExpiration>()
              {
                ContentLinksManager.GetConfiguredCacheExpiration(),
                (ICacheItemExpiration) new DataItemCacheDependency(typeof (ContentLink), (string) null)
              }.ToArray());
            }
          }
        }
      }
      return obj is List<ContentLinksManager.ContentLinkProxy> source1 ? ((IEnumerable<IContentLink>) source1).ToList<IContentLink>() : (List<IContentLink>) null;
    }

    private static ICacheItemExpiration GetConfiguredCacheExpiration()
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) ContentLinksManager.ContentLinksCacheSettings.Duration);
      return !ContentLinksManager.ContentLinksCacheSettings.SlidingExpiration ? (ICacheItemExpiration) new AbsoluteTime(timeSpan) : (ICacheItemExpiration) new SlidingTime(timeSpan);
    }

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<ContentLinksConfig>().DefaultProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => "Metadata";

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ContentLinksConfig>().Providers;

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    /// <param name="updateSchema">if set to <c>true</c> [update schema].</param>
    public new virtual void SaveChanges() => this.Provider.CommitTransaction();

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static ContentLinksManager GetManager() => ManagerBase<ContentLinksProviderBase>.GetManager<ContentLinksManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ContentLinksManager GetManager(string providerName) => ManagerBase<ContentLinksProviderBase>.GetManager<ContentLinksManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static ContentLinksManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ContentLinksProviderBase>.GetManager<ContentLinksManager>(providerName, transactionName);
    }

    internal static bool Initialized => ContentLinksManager.initialized;

    internal static bool IsOpenAccessProvider
    {
      get
      {
        ContentLinksConfig contentLinksConfig = Config.Get<ContentLinksConfig>();
        return typeof (IOpenAccessDataProvider).IsAssignableFrom(contentLinksConfig.Providers[contentLinksConfig.DefaultProvider].ProviderType);
      }
    }

    private static ICacheManager ContentLinksCache => SystemManager.GetCacheManager(CacheManagerInstance.ContentLinks);

    private static CacheProfileElement ContentLinksCacheSettings
    {
      get
      {
        if (ContentLinksManager.contentLinksCacheSettings == null)
          ContentLinksManager.contentLinksCacheSettings = Config.Get<SystemConfig>().ContentLinksCache;
        return ContentLinksManager.contentLinksCacheSettings;
      }
    }

    /// <summary>
    /// Removes the redundant content links.
    /// The Save of content item duplicates its content links, so we need to remove the redundant, no longer used content links.
    /// </summary>
    /// <param name="contentItem">The content  item.</param>
    internal void RemoveRedundantContentLinks(IContent item)
    {
      IQueryable<ContentLink> queryable = this.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == item.Id));
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties((object) item))
      {
        PropertyDescriptor property = property1;
        if (property.PropertyType == typeof (ContentLink[]))
        {
          ContentLink[] source1 = property.GetValue((object) item) as ContentLink[];
          IQueryable<ContentLink> source2 = queryable;
          Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ComponentPropertyName == property.Name);
          foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source2.Where<ContentLink>(predicate))
          {
            ContentLink dbLink = contentLink;
            if (!((IEnumerable<ContentLink>) source1).Any<ContentLink>((Func<ContentLink, bool>) (cl => cl.Id == dbLink.Id)))
              this.Delete(dbLink);
          }
        }
      }
    }

    /// <summary>
    /// Removes the redundant content links.
    /// The Save of dynamic content item duplicates its content links, so we need to remove the redundant, no longer used content links.
    /// </summary>
    /// <param name="masterItem">The master item.</param>
    /// <param name="additionalItems">All items (Live and/or Temp) that may use the the same content links.</param>
    internal void RemoveRedundantContentLinks(
      ILifecycleDataItem masterItem,
      params ILifecycleDataItem[] additionalItems)
    {
      IQueryable<ContentLink> queryable = this.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == masterItem.Id));
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties((object) masterItem))
      {
        PropertyDescriptor property = property1;
        if (property.PropertyType == typeof (ContentLink[]))
        {
          ContentLink[] source1 = property.GetValue((object) masterItem) as ContentLink[];
          List<ContentLink> source2 = new List<ContentLink>();
          if (additionalItems != null && additionalItems.Length != 0)
          {
            foreach (ILifecycleDataItem additionalItem in additionalItems)
            {
              if (property.GetValue((object) additionalItem) is ContentLink[] collection)
                source2.AddRange((IEnumerable<ContentLink>) collection);
            }
          }
          IQueryable<ContentLink> source3 = queryable;
          Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ComponentPropertyName == property.Name);
          foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source3.Where<ContentLink>(predicate))
          {
            ContentLink dbLink = contentLink;
            if (!((IEnumerable<ContentLink>) source1).Any<ContentLink>((Func<ContentLink, bool>) (cl => cl.Id == dbLink.Id)) && !source2.Any<ContentLink>((Func<ContentLink, bool>) (cl => cl.Id == dbLink.Id)))
              this.Delete(dbLink);
          }
        }
      }
    }

    /// <summary>
    /// This method is called after data provider initialization,
    /// when the manager is instantiated for the first time in the application lifecycle.
    /// </summary>
    protected override void OnInitialized() => ContentLinksManager.initialized = true;

    internal class ContentLinkProxy : CacheableItem, IContentLink
    {
      /// <inheritdoc />
      public Guid ParentItemId { get; set; }

      /// <inheritdoc />
      public string ComponentPropertyName { get; set; }

      /// <inheritdoc />
      public string ChildItemProviderName { get; set; }

      /// <inheritdoc />
      public Guid ChildItemId { get; set; }
    }
  }
}
