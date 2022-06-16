// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>Represents a base class for Multisite data providers.</summary>
  public abstract class MultisiteDataProvider : DataProviderBase, IDataEventProvider
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "Site"
    };

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> instance.
    /// </summary>
    [MethodPermission("Site", new string[] {"CreateEditSite"})]
    public abstract Site CreateSite();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> with the given id.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>Created <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> instance</returns>
    [MethodPermission("Site", new string[] {"CreateEditSite"})]
    public abstract Site CreateSite(Guid siteId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> for removal.
    /// </summary>
    /// <param name="site">The site to delete.</param>
    [ParameterPermission("site", "Site", new string[] {"DeleteSite"})]
    public abstract void Delete(Site site);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> object with the given id.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    [EnumeratorPermission("Site", new string[] {"AccessSite"})]
    public abstract Site GetSite(Guid siteId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> objects.
    /// </returns>
    [EnumeratorPermission("Site", new string[] {"AccessSite"})]
    public abstract IQueryable<Site> GetSites();

    /// <summary>Changes the owner of a site.</summary>
    /// <param name="site">The site to change the owner for.</param>
    /// <param name="newOwnerID">ID of the new owner of the site.</param>
    [ParameterPermission("site", "Site", new string[] {"ChangeOwner"})]
    public abstract void ChangeOwner(Site site, Guid newOwnerID);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns>The new data item.</returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Site))
        return (object) this.CreateSite(id);
      if (itemType == typeof (SiteDataSourceLink))
        return (object) this.CreateSiteDataSourceLink(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (item.GetType() != typeof (Site))
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      this.Delete((Site) item);
      this.providerDecorator.DeletePermissions(item);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSource" /> instance.
    /// </summary>
    /// <param name="name">The name of the data source. Usually, this is the type of the manager</param>
    /// <param name="provider">The provider name. Usially, this is the name of the virtual provider</param>
    /// <returns></returns>
    public abstract SiteDataSource CreateDataSource(string name, string provider);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSource" /> object with the given unique combination of data source and provider.
    /// </summary>
    /// <param name="name">The name of the data source. Usually, this is the type of the manager</param>
    /// <param name="provider">The provider name. Usially, this is the name of the virtual provider</param>
    public abstract IQueryable<SiteDataSource> GetDataSources();

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSource" /> for removal.
    /// </summary>
    /// <param name="source">The data source to delete.</param>
    public abstract void Delete(SiteDataSource source);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> instance.
    /// </summary>
    public abstract SiteDataSourceLink CreateSiteDataSourceLink();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> instance with the given id.
    /// </summary>
    /// <param name="siteDataSourceLinkId">The data source id.</param>
    public abstract SiteDataSourceLink CreateSiteDataSourceLink(
      Guid siteDataSourceLinkId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> for removal.
    /// </summary>
    /// <param name="source">The data source to delete.</param>
    public abstract void Delete(SiteDataSourceLink source);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> object with the given id.
    /// </summary>
    /// <param name="siteDataSourceLinkId">The data source id.</param>
    public abstract SiteDataSourceLink GetSiteDataSourceLink(
      Guid siteDataSourceLinkId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> objects.
    /// </returns>
    public abstract IQueryable<SiteDataSourceLink> GetSiteDataSourceLinks();

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Site))
        return (object) this.GetSite(id);
      return itemType == typeof (SiteDataSourceLink) ? (object) this.GetSiteDataSourceLink(id) : base.GetItem(itemType, id);
    }

    /// <inheritdoc />
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (Site)))
        return base.GetItemOrDefault(itemType, id);
      try
      {
        return (object) this.GetSite(id);
      }
      catch (Exception ex)
      {
        return (object) null;
      }
    }

    /// <summary>
    /// Retrieves a collection of items of the specified type form the data store.
    /// </summary>
    /// <param name="itemType">The type of the items to be retrieved.</param>
    /// <param name="filterExpression">Defines filter expression to be applied.</param>
    /// <param name="orderExpression">Specifies the order of the items in the collection.</param>
    /// <param name="skip">The number of items to skip from the beginning of the result.</param>
    /// <param name="take">The number or items take in the collection from the result.</param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      int? totalCount = new int?(-1);
      return this.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Retrieves a collection of items of the specified type form the data store.
    /// </summary>
    /// <param name="itemType">The type of the items to be retrieved.</param>
    /// <param name="filterExpression">Defines filter expression to be applied.</param>
    /// <param name="orderExpression">Specifies the order of the items in the collection.</param>
    /// <param name="skip">The number of items to skip from the beginning of the result.</param>
    /// <param name="take">The number or items take in the collection from the result.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Site))
        return (IEnumerable) DataProviderBase.SetExpressions<Site>(this.GetSites(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (SiteDataSourceLink))
        return (IEnumerable) DataProviderBase.SetExpressions<SiteDataSourceLink>(this.GetSiteDataSourceLinks(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>Get a list of types served by this manager</summary>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (Site),
      typeof (SiteDataSourceLink)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => nameof (MultisiteDataProvider);

    /// <summary>Commits the provided transaction.</summary>
    public override void CommitTransaction()
    {
      this.InvalidateSitesCacheUponPermissionsUpdate();
      this.DemandPermissionsForSiteDataSourcesInTransaction();
      base.CommitTransaction();
    }

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    public override void FlushTransaction()
    {
      this.InvalidateSitesCacheUponPermissionsUpdate();
      this.DemandPermissionsForSiteDataSourcesInTransaction();
      this.VerifySitePropertiesConstraints();
      base.FlushTransaction();
    }

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission = this.CreatePermission("Site", root.Id, SecurityManager.BackEndUsersRole.Id);
      permission.GrantActions(false, "AccessSite");
      root.Permissions.Add(permission);
    }

    /// <summary>
    /// Invalidates the sites cache when a permission is detected as dirty in the transaction.
    /// </summary>
    private void InvalidateSitesCacheUponPermissionsUpdate()
    {
      if (this.GetDirtyItems().OfType<Permission>().Count<Permission>() <= 0)
        return;
      CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
      {
        new CacheDependencyKey() { Type = typeof (Site) }
      });
    }

    /// <summary>
    /// Checks is there are data-source links in the current transaction and throws and exception if the user doesn't have sufficient permissions on the related sites.
    /// </summary>
    private void DemandPermissionsForSiteDataSourcesInTransaction()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.IsUnrestricted)
        return;
      IList dirtyItems = this.GetDirtyItems();
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        SiteDataSourceLink dirtyDataSourceLink = obj as SiteDataSourceLink;
        if (dirtyDataSourceLink != null && dirtyDataSourceLink.SiteId != Guid.Empty)
        {
          Site itemInTransaction = dirtyItems.OfType<Site>().FirstOrDefault<Site>((Func<Site, bool>) (s => s.Id == dirtyDataSourceLink.SiteId));
          if (itemInTransaction == null || this.GetDirtyItemStatus((object) itemInTransaction) != SecurityConstants.TransactionActionType.Deleted)
          {
            Site securedObject = this.GetSites().FirstOrDefault<Site>((Expression<Func<Site, bool>>) (s => s.Id == dirtyDataSourceLink.SiteId));
            if (securedObject != null)
              securedObject.Demand("Site", "ConfigureModules");
          }
        }
      }
    }

    /// <summary>
    /// Checks for duplicate site names, duplicates in domain names and invalid domain names.
    /// Throws an exception if an error is found.
    /// </summary>
    private void VerifySitePropertiesConstraints()
    {
      foreach (Site site in this.GetDirtyItems().OfType<Site>())
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MultisiteDataProvider.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new MultisiteDataProvider.\u003C\u003Ec__DisplayClass28_0();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass280.\u003C\u003E4__this = this;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass280.updatedSite = site;
        // ISSUE: reference to a compiler-generated field
        switch (this.GetDirtyItemStatus((object) cDisplayClass280.updatedSite))
        {
          case SecurityConstants.TransactionActionType.New:
          case SecurityConstants.TransactionActionType.Updated:
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            cDisplayClass280.updatedSite.DomainAliases = (IList<string>) cDisplayClass280.updatedSite.DomainAliases.Select<string, string>((Func<string, string>) (a => MultisiteDataProvider.GetCleanDomainUrl(a))).Where<string>((Func<string, bool>) (u => !u.IsNullOrWhitespace())).ToList<string>();
            // ISSUE: reference to a compiler-generated field
            IQueryable<Site> source = this.GetSites().Where<Site>((Expression<Func<Site, bool>>) (s => s.Id != cDisplayClass280.updatedSite.Id));
            // ISSUE: reference to a compiler-generated field
            if (cDisplayClass280.updatedSite.Name.IsNullOrWhitespace())
              throw new BadRequestException(Res.Get<MultisiteResources>().InvalidSiteName);
            // ISSUE: reference to a compiler-generated field
            if (cDisplayClass280.updatedSite.LiveUrl.IsNullOrWhitespace())
              throw new BadRequestException(Res.Get<MultisiteResources>().InvalidDomainName);
            Func<string, Exception> validate = (Func<string, Exception>) (url =>
            {
              Exception exception = (Exception) null;
              if (url.IsNullOrEmpty())
                return (Exception) null;
              if (Uri.IsWellFormedUriString("http://" + url, UriKind.Absolute))
              {
                if (url.Trim().Contains(" "))
                {
                  exception = (Exception) new BadRequestException(string.Format(Res.Get<MultisiteResources>().SiteSpecificDomainCannotContainSpaces, (object) url));
                }
                else
                {
                  int length = url.IndexOf('/');
                  string str = length <= 0 ? url : url.Substring(0, length);
                  int num = str.IndexOf(':');
                  int result;
                  if (num <= -1 || num != str.Length - 1 && int.TryParse(str.Substring(num + 1), out result) && result >= 0 && result <= (int) ushort.MaxValue)
                    return (Exception) null;
                }
              }
              return exception ?? (Exception) new BadRequestException(string.Format(Res.Get<MultisiteResources>().SiteSpecificDomainIsInvalid, (object) url));
            });
            // ISSUE: reference to a compiler-generated field
            MultisiteDataProvider.ValidateSiteUrls(cDisplayClass280.updatedSite, validate);
            ParameterExpression parameterExpression;
            // ISSUE: method reference
            // ISSUE: field reference
            // ISSUE: method reference
            // ISSUE: method reference
            if (source.Any<Site>(Expression.Lambda<Func<Site, bool>>((Expression) Expression.Equal((Expression) Expression.Call(s.Name, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Trim)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass280, typeof (MultisiteDataProvider.\u003C\u003Ec__DisplayClass28_0)), FieldInfo.GetFieldFromHandle(__fieldref (MultisiteDataProvider.\u003C\u003Ec__DisplayClass28_0.updatedSite))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Site.get_Name))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Trim)), Array.Empty<Expression>())), parameterExpression)))
            {
              // ISSUE: reference to a compiler-generated field
              throw new ConflictException(string.Format(Res.Get<MultisiteResources>().DuplicateSiteName, (object) cDisplayClass280.updatedSite.Name));
            }
            using (IEnumerator<Site> enumerator = source.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Site existingSite = enumerator.Current;
                if (updatedSite.RequiresSsl == existingSite.RequiresSsl && this.IsUrlAlreadyRegisteredInSite(updatedSite.LiveUrl, existingSite))
                  throw new DuplicateUrlException(string.Format(Res.Get<MultisiteResources>().DomainAlreadyUsedByAnotherSite, (object) ((updatedSite.RequiresSsl ? "https://" : "http://") + updatedSite.LiveUrl), (object) existingSite.Name));
                if (updatedSite.RequiresSsl == existingSite.RequiresSsl && this.IsUrlAlreadyRegisteredInSite(updatedSite.StagingUrl, existingSite))
                  throw new DuplicateUrlException(string.Format(Res.Get<MultisiteResources>().TestingDomainAlreadyUsedByAnotherSite, (object) ((updatedSite.RequiresSsl ? "https://" : "http://") + updatedSite.StagingUrl), (object) existingSite.Name));
                string str = updatedSite.DomainAliases.FirstOrDefault<string>((Func<string, bool>) (alias => this.IsUrlAlreadyRegisteredInSite(alias, existingSite)));
                if (updatedSite.RequiresSsl == existingSite.RequiresSsl && !string.IsNullOrWhiteSpace(str))
                  throw new DuplicateUrlException(string.Format(Res.Get<MultisiteResources>().DomainAliasAlreadyUsedByAnotherSite, (object) ((updatedSite.RequiresSsl ? "https://" : "http://") + str), (object) existingSite.Name));
              }
              continue;
            }
          default:
            continue;
        }
      }
    }

    private static void ValidateSiteUrls(Site s, Func<string, Exception> validate)
    {
      Action<string> action = (Action<string>) (str =>
      {
        Exception exception = validate(str);
        if (exception != null)
          throw exception;
      });
      action(s.LiveUrl);
      action(s.StagingUrl);
      foreach (string domainAlias in (IEnumerable<string>) s.DomainAliases)
        action(domainAlias);
    }

    /// <summary>
    /// Gets a domain URL clean of protocol prefixes, trailing slashes and optionally other unnecessary stuff.
    /// This is put in a separate method
    /// (1) in order to potentially add more cleanup procedures,
    /// (2) to centralize the cleanup process for all urls,
    /// (3) to avoid mistakes of copy/pasting
    /// </summary>
    /// <param name="url">The domain URL.</param>
    /// <returns>The domain URL, clean of protocol prefixes, trailing slashes and optionally other unnecessary stuff.</returns>
    private static string GetCleanDomainUrl(string url) => (url.IndexOf("://") >= 0 ? url.Substring(url.IndexOf("://") + 3) : url).Trim('\\', '/', ' ', '\t', '\n');

    private bool IsUrlAlreadyRegisteredInSite(string url, Site site) => !string.IsNullOrWhiteSpace(url) && (url == site.LiveUrl || url == site.StagingUrl || site.DomainAliases.Contains(url));

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <inheritdoc />
    public bool DataEventsEnabled => true;

    /// <inheritdoc />
    public bool ApplyDataEventItemFilter(IDataItem item) => item is Site;
  }
}
