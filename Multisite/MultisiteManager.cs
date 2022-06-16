// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite.Configuration;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>Managers class for the multisite module.</summary>
  public class MultisiteManager : ManagerBase<MultisiteDataProvider>
  {
    private const string DeletedSitesKey = "deleted-sites";

    static MultisiteManager()
    {
      ManagerBase<MultisiteDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(MultisiteManager.Provider_Executing);
      ManagerBase<MultisiteDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(MultisiteManager.Provider_Executed);
    }

    /// <summary>
    /// Executes before flushing or committing Multisite transactions.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      MultisiteDataProvider multisiteDataProvider = sender as MultisiteDataProvider;
      IList dirtyItems = multisiteDataProvider.GetDirtyItems();
      if (dirtyItems.Count == 0)
        return;
      if (!(multisiteDataProvider.GetExecutionStateData("deleted-sites") is List<Guid> guidList))
        guidList = new List<Guid>();
      foreach (object itemInTransaction in (IEnumerable) dirtyItems)
      {
        if (itemInTransaction is Site site)
        {
          if (multisiteDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.Deleted)
            guidList.Add(site.Id);
        }
        else
        {
          SiteDataSourceLink dataSourceLink;
          if ((dataSourceLink = itemInTransaction as SiteDataSourceLink) != null)
          {
            if (multisiteDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.New && dataSourceLink.DataSource == null)
            {
              SiteDataSource siteDataSource = dirtyItems.OfType<SiteDataSource>().Where<SiteDataSource>((Func<SiteDataSource, bool>) (ds => ds.Name == dataSourceLink.DataSourceName && ds.Provider == dataSourceLink.ProviderName)).SingleOrDefault<SiteDataSource>();
              if (siteDataSource == null)
                siteDataSource = multisiteDataProvider.GetDataSources().Where<SiteDataSource>((Expression<Func<SiteDataSource, bool>>) (s => s.Name == dataSourceLink.DataSourceName && s.Provider == dataSourceLink.ProviderName)).FirstOrDefault<SiteDataSource>();
              if (siteDataSource == null)
              {
                siteDataSource = multisiteDataProvider.CreateDataSource(dataSourceLink.DataSourceName, dataSourceLink.ProviderName);
                siteDataSource.OwnerSiteId = dataSourceLink.Site.Id;
              }
              dataSourceLink.DataSource = siteDataSource;
            }
          }
          else if (itemInTransaction is SiteDataSource siteDataSource1 && multisiteDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.New && siteDataSource1.OwnerSiteId == Guid.Empty)
          {
            SiteDataSourceLink siteDataSourceLink = siteDataSource1.Sites.FirstOrDefault<SiteDataSourceLink>();
            if (siteDataSourceLink != null)
              siteDataSource1.OwnerSiteId = siteDataSourceLink.SiteId;
          }
        }
      }
      if (!guidList.Any<Guid>())
        return;
      multisiteDataProvider.SetExecutionStateData("deleted-sites", (object) guidList);
    }

    /// <summary>
    /// Handles the post commit event of the Multisite provider.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      MultisiteDataProvider multisiteDataProvider = sender as MultisiteDataProvider;
      List<Guid> deletedSites = multisiteDataProvider.GetExecutionStateData("deleted-sites") as List<Guid>;
      if (deletedSites == null || !deletedSites.Any<Guid>())
        return;
      foreach (Type multisiteEnabledManager in (IEnumerable<Type>) SystemManager.MultisiteEnabledManagers)
      {
        if (ManagerBase.GetManager(multisiteEnabledManager) is IMultisiteEnabledManager manager)
        {
          foreach (DataProviderBase staticProvider in manager.StaticProviders)
          {
            try
            {
              IMultisiteEnabledManager managerInTransaction = (IMultisiteEnabledManager) ManagerBase.GetManagerInTransaction(multisiteEnabledManager, staticProvider.Name, "deleted-sites");
              if (managerInTransaction is IMultisiteEnabledManagerExtended enabledManagerExtended)
              {
                enabledManagerExtended.DeleteSiteLinks(deletedSites.ToArray());
              }
              else
              {
                IQueryable<SiteItemLink> siteItemLinks = managerInTransaction.GetSiteItemLinks();
                Expression<Func<SiteItemLink, bool>> predicate = (Expression<Func<SiteItemLink, bool>>) (l => deletedSites.Contains(l.SiteId));
                foreach (SiteItemLink link in siteItemLinks.Where<SiteItemLink>(predicate).ToArray<SiteItemLink>())
                  managerInTransaction.Delete(link);
              }
              managerInTransaction.Provider.CommitTransaction();
            }
            catch (Exception ex)
            {
              if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                throw;
              else
                Log.Write((object) string.Format("MultisiteManager: Failed to delete site links from '{0}' after deleting a site. Exception: {1}. See the error log for more information.", (object) multisiteEnabledManager.FullName, (object) ex.Message), ConfigurationPolicy.Trace);
            }
          }
        }
      }
      multisiteDataProvider.SetExecutionStateData("deleted-sites", (object) null);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.MultisiteManager" /> class.
    /// </summary>
    public MultisiteManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.MultisiteManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public MultisiteManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.MultisiteManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public MultisiteManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Get an instance of the MultisiteManager using the default provider
    /// </summary>
    /// <returns>Instance of MultisiteManager</returns>
    public static MultisiteManager GetManager() => ManagerBase<MultisiteDataProvider>.GetManager<MultisiteManager>();

    /// <summary>
    /// Get an instance of the MultisiteManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the MultisiteManager</returns>
    public static MultisiteManager GetManager(string providerName) => ManagerBase<MultisiteDataProvider>.GetManager<MultisiteManager>(providerName);

    /// <summary>
    /// Get an instance of the MultisiteManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the MultisiteManager</returns>
    public static MultisiteManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<MultisiteDataProvider>.GetManager<MultisiteManager>(providerName, transactionName);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> instance.
    /// </summary>
    public Site CreateSite() => this.Provider.CreateSite();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> with the given id.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>Created <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> instance</returns>
    public Site CreateSite(Guid siteId) => this.Provider.CreateSite(siteId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> for removal.
    /// </summary>
    /// <param name="site">The site to delete.</param>
    public void Delete(Site site)
    {
      if (site.IsDefault)
        throw new InvalidOperationException("This site is default and cannot be deleted!");
      this.Provider.Delete(site);
    }

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> object with the given id.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    public Site GetSite(Guid siteId) => this.Provider.GetSite(siteId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.Site" /> objects.
    /// </returns>
    public IQueryable<Site> GetSites() => this.Provider.GetSites();

    /// <summary>Sets the default site.</summary>
    /// <param name="siteId">The site id.</param>
    internal void SetDefaultSite(Guid siteId)
    {
      Site site1 = this.GetSite(siteId);
      IQueryable<Site> sites = this.GetSites();
      Expression<Func<Site, bool>> predicate = (Expression<Func<Site, bool>>) (s => s.Id != siteId && s.IsDefault);
      foreach (Site site2 in (IEnumerable<Site>) sites.Where<Site>(predicate))
      {
        if (site2.IsDefault)
          site2.IsDefault = false;
      }
      if (site1.IsDefault)
        return;
      site1.IsDefault = true;
    }

    /// <summary>Changes the owner of a site.</summary>
    /// <param name="site">The site to change the owner for.</param>
    /// <param name="newOwnerID">ID of the new owner of the site.</param>
    public virtual void ChangeOwner(Site site, Guid newOwnerID) => this.Provider.ChangeOwner(site, newOwnerID);

    internal SiteDataSource GetOrCreateDataSource(
      string name,
      string provider,
      string title)
    {
      SiteDataSource dataSource = this.Provider.GetDataSources().Where<SiteDataSource>((Expression<Func<SiteDataSource, bool>>) (ds => ds.Name == name && ds.Provider == provider)).FirstOrDefault<SiteDataSource>();
      if (dataSource == null)
      {
        dataSource = this.Provider.CreateDataSource(name, provider);
        dataSource.Title = title;
      }
      return dataSource;
    }

    internal IQueryable<SiteDataSource> GetDataSources() => this.Provider.GetDataSources();

    public void Delete(SiteDataSource link) => this.Provider.Delete(link);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> instance.
    /// </summary>
    public SiteDataSourceLink CreateSiteDataSourceLink() => this.Provider.CreateSiteDataSourceLink();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> instance with the given id.
    /// </summary>
    /// <param name="siteDataSourceLinkId">The data source id.</param>
    public SiteDataSourceLink CreateSiteDataSourceLink(Guid siteId) => this.Provider.CreateSiteDataSourceLink(siteId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> for removal.
    /// </summary>
    /// <param name="source">The data source to delete.</param>
    public void Delete(SiteDataSourceLink link) => this.Provider.Delete(link);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> object with the given id.
    /// </summary>
    /// <param name="siteDataSourceLinkId">The data source id.</param>
    public SiteDataSourceLink GetSiteDataSourceLink(Guid siteDataSourceLinkId) => this.Provider.GetSiteDataSourceLink(siteDataSourceLinkId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> objects.
    /// </returns>
    public IQueryable<SiteDataSourceLink> GetSiteDataSourceLinks() => this.Provider.GetSiteDataSourceLinks();

    /// <summary>
    /// Marks all <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteDataSourceLink" /> for the specified <see cref="!:DataSource" /> for removal.
    /// </summary>
    /// <param name="datasourceName">The name of the <see cref="!:DataSource" />.</param>
    public void DeleteSiteDataSourceLinks(string datasourceName)
    {
      IQueryable<SiteDataSourceLink> siteDataSourceLinks = this.GetSiteDataSourceLinks();
      Expression<Func<SiteDataSourceLink, bool>> predicate1 = (Expression<Func<SiteDataSourceLink, bool>>) (l => l.DataSource.Name == datasourceName);
      foreach (SiteDataSourceLink siteDataSourceLink in (IEnumerable<SiteDataSourceLink>) siteDataSourceLinks.Where<SiteDataSourceLink>(predicate1))
      {
        SiteDataSourceLink link = siteDataSourceLink;
        IQueryable<Site> sites = this.GetSites();
        Expression<Func<Site, bool>> predicate2 = (Expression<Func<Site, bool>>) (s => s.SiteDataSourceLinks.Contains(link));
        foreach (Site site in (IEnumerable<Site>) sites.Where<Site>(predicate2))
          site.SiteDataSourceLinks.Remove(link);
        this.Delete(link);
      }
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<MultisiteConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    public override string ModuleName => "MultisiteInternal";

    /// <summary>Gets the providers settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<MultisiteConfig>().Providers;

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
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take);
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
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }
  }
}
