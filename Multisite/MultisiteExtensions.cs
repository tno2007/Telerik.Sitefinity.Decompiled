// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  internal static class MultisiteExtensions
  {
    /// <summary>
    /// Retruns the names of all providers available on the <paramref name="site" />.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <returns>A set of provider names.</returns>
    public static ISet<string> GetAllProviderNames(this ISite site)
    {
      if (site == null)
        throw new ArgumentNullException(nameof (site));
      ISet<string> allProviderNames = (ISet<string>) new HashSet<string>();
      foreach (IDataSource dataSource in SystemManager.DataSourceRegistry.GetDataSources())
      {
        IEnumerable<MultisiteContext.SiteDataSourceLinkProxy> providers = site.GetProviders(dataSource.Name);
        if (providers != null && providers.Any<MultisiteContext.SiteDataSourceLinkProxy>())
          allProviderNames.UnionWith(providers.Select<MultisiteContext.SiteDataSourceLinkProxy, string>((Func<MultisiteContext.SiteDataSourceLinkProxy, string>) (p => p.ProviderName)));
      }
      return allProviderNames;
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public static SiteItemLink CreateSiteItemLink(
      this IMultisiteEnabledOAProvider provider)
    {
      if (!(provider is DataProviderBase dataProviderBase))
        throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      SiteItemLink entity = new SiteItemLink();
      entity.ApplicationName = dataProviderBase.ApplicationName;
      provider.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public static void Delete(this IMultisiteEnabledOAProvider provider, SiteItemLink link) => provider.GetContext().Remove((object) link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="item">The item.</param>
    public static void DeleteLinksForItem(this IMultisiteEnabledOAProvider provider, IDataItem item) => provider.DeleteLinksForItem(item, item.GetType().FullName);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="item">The item.</param>
    /// <param name="itemType">Actual type persisted in the <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />.</param>
    public static void DeleteLinksForItem(
      this IMultisiteEnabledOAProvider provider,
      IDataItem item,
      string itemType)
    {
      IQueryable<SiteItemLink> siteItemLinks = provider.GetSiteItemLinks();
      Expression<Func<SiteItemLink, bool>> predicate = (Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == item.Id && l.ItemType == itemType);
      foreach (SiteItemLink link in siteItemLinks.Where<SiteItemLink>(predicate).ToArray<SiteItemLink>())
        provider.Delete(link);
    }

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<SiteItemLink> GetSiteItemLinks(
      this IMultisiteEnabledOAProvider provider)
    {
      string appName = provider is DataProviderBase dataProvider ? dataProvider.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return SitefinityQuery.Get<SiteItemLink>(dataProvider, MethodBase.GetCurrentMethod()).Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public static SiteItemLink AddItemLink(
      this IMultisiteEnabledOAProvider provider,
      Guid siteId,
      IDataItem item)
    {
      return provider.AddItemLink(siteId, item, item.GetType().FullName);
    }

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public static SiteItemLink AddItemLink(
      this IMultisiteEnabledOAProvider provider,
      Guid siteId,
      IDataItem item,
      string itemType)
    {
      SiteItemLink siteItemLink = provider.CreateSiteItemLink();
      siteItemLink.SiteId = siteId;
      siteItemLink.ItemType = itemType;
      siteItemLink.ItemId = item.Id;
      return siteItemLink;
    }

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<T> GetSiteItems<T>(
      this IMultisiteEnabledOAProvider provider,
      Guid siteId)
      where T : IDataItem
    {
      string appName = provider is DataProviderBase dataProvider ? dataProvider.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return Queryable.Cast<T>(SitefinityQuery.Get(typeof (T), dataProvider)).Join((IEnumerable<SiteItemLink>) provider.GetSiteItemLinks(), (Expression<Func<T, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.c.ApplicationName == appName && data.l.ItemType == typeof (T).FullName && data.l.SiteId == siteId).Select(data => data.c);
    }

    /// <summary>
    /// Gets the items linked to the specified site filtered by view permissions.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="method">The method with permission attributes</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<T> GetSiteItems<T>(
      this IMultisiteEnabledOAProvider provider,
      Guid siteId,
      MethodBase method)
      where T : IDataItem
    {
      if (!(provider is DataProviderBase dataProviderBase))
        throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      IQueryable<T> source = (IQueryable<T>) typeof (SitefinityQuery).GetMethod("Get", new Type[2]
      {
        typeof (DataProviderBase),
        typeof (MethodBase)
      }).MakeGenericMethod(typeof (T)).Invoke((object) null, new object[2]
      {
        (object) dataProviderBase,
        (object) method
      });
      string appName = dataProviderBase.ApplicationName;
      return Queryable.Cast<T>(source).Join((IEnumerable<SiteItemLink>) provider.GetSiteItemLinks(), (Expression<Func<T, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.c.ApplicationName == appName && data.l.ItemType == typeof (T).FullName && data.l.SiteId == siteId).Select(data => data.c);
    }

    /// <summary>Filters the specified items by site.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="provider">The provider.</param>
    /// <param name="items">The items.</param>
    /// <param name="siteId">The site id.</param>
    internal static IQueryable<T> FilterBySite<T>(
      this IMultisiteEnabledOAProvider provider,
      IQueryable<T> items,
      Guid siteId)
      where T : IDataItem
    {
      return provider.FilterBySite<T>(items, siteId, typeof (T).FullName);
    }

    /// <summary>Filters the specified items by site.</summary>
    /// <typeparam name="T">The type of the T.</typeparam>
    /// <param name="provider">The provider.</param>
    /// <param name="items">The items.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    internal static IQueryable<T> FilterBySite<T>(
      this IMultisiteEnabledOAProvider provider,
      IQueryable<T> items,
      Guid siteId,
      string itemType)
      where T : IDataItem
    {
      string appName = provider is DataProviderBase dataProviderBase ? dataProviderBase.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return Queryable.Cast<T>(items).Join((IEnumerable<SiteItemLink>) provider.GetSiteItemLinks(), (Expression<Func<T, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.c.ApplicationName == appName && data.l.ItemType == itemType && data.l.SiteId == siteId).Select(data => data.c);
    }

    /// <summary>Gets the items not linked to any site.</summary>
    /// <param name="provider">The provider.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<T> GetNotLinkedItems<T>(
      this IMultisiteEnabledOAProvider provider)
      where T : IDataItem
    {
      string appName = provider is DataProviderBase dataProvider ? dataProvider.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return Queryable.Except<T>(Queryable.Cast<T>(SitefinityQuery.Get(typeof (T), dataProvider)).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName)), (IEnumerable<T>) Queryable.Cast<T>(SitefinityQuery.Get(typeof (T), dataProvider)).Join((IEnumerable<SiteItemLink>) provider.GetSiteItemLinks(), (Expression<Func<T, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.c.ApplicationName == appName && data.l.ItemType == typeof (T).FullName).Select(data => data.c));
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s for a given <see cref="T:Telerik.Sitefinity.Model.IDataItem" />.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="provider">The <see cref="T:Telerik.Sitefinity.Multisite.IMultisiteEnabledProvider" /> provider from which we take the <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s.</param>
    /// <param name="item">The item.</param>
    /// <param name="itemType">The item type.</param>
    /// <returns>A query containing the given item <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s from the given provider.</returns>
    internal static IQueryable<SiteItemLink> GetItemSiteLinks(
      this IMultisiteEnabledProvider provider,
      IDataItem item,
      string itemType)
    {
      string appName = provider is DataProviderBase dataProviderBase ? dataProviderBase.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return provider.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == item.Id && l.ItemType == itemType && l.ApplicationName == appName));
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s for a given <see cref="T:Telerik.Sitefinity.Model.IDataItem" />.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="provider">The <see cref="T:Telerik.Sitefinity.Multisite.IMultisiteEnabledProvider" /> provider from which we take the <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s.</param>
    /// <param name="item">The item.</param>
    /// <returns>A query containing the given item <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" />s from the given provider.</returns>
    internal static IQueryable<SiteItemLink> GetItemSiteLinks(
      this IMultisiteEnabledProvider provider,
      IDataItem item)
    {
      return provider.GetItemSiteLinks(item, item.GetType().FullName);
    }

    internal static IQueryable<SiteItemLink> GetItemsSiteLinks<T>(
      this IMultisiteEnabledOAProvider provider,
      IQueryable<T> items)
      where T : IDataItem
    {
      string appName = provider is DataProviderBase dataProviderBase ? dataProviderBase.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return items.Join((IEnumerable<SiteItemLink>) provider.GetSiteItemLinks(), (Expression<Func<T, Guid>>) (item => item.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (item, l) => new
      {
        item = item,
        l = l
      }).Where(data => data.l.ApplicationName == appName).Select(data => data.l);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public static SiteItemLink CreateSiteItemLink(this IMultisiteEnabledManager manager) => ((IMultisiteEnabledProvider) manager.Provider).CreateSiteItemLink();

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public static void Delete(this IMultisiteEnabledManager manager, SiteItemLink link) => ((IMultisiteEnabledProvider) manager.Provider).Delete(link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="item">The item.</param>
    public static void DeleteLinksForItem(this IMultisiteEnabledManager manager, IDataItem item) => ((IMultisiteEnabledProvider) manager.Provider).DeleteLinksForItem(item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<SiteItemLink> GetSiteItemLinks(
      this IMultisiteEnabledManager manager)
    {
      return ((IMultisiteEnabledProvider) manager.Provider).GetSiteItemLinks();
    }

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public static SiteItemLink AddItemLink(
      this IMultisiteEnabledManager manager,
      Guid siteId,
      IDataItem item)
    {
      return ((IMultisiteEnabledProvider) manager.Provider).AddItemLink(siteId, item);
    }

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public static IQueryable<T> GetSiteItems<T>(
      this IMultisiteEnabledManager manager,
      Guid siteId)
      where T : IDataItem
    {
      return ((IMultisiteEnabledProvider) manager.Provider).GetSiteItems<T>(siteId);
    }
  }
}
