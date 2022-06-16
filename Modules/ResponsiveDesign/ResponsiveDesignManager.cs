// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// The manager class for working with the Responsive Design module.
  /// </summary>
  public class ResponsiveDesignManager : ManagerBase<ResponsiveDesignDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignManager" /> class.
    /// </summary>
    public ResponsiveDesignManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public ResponsiveDesignManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public ResponsiveDesignManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    public override string ModuleName => "ResponsiveDesign";

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => "OpenAccessDataProvider");

    /// <summary>
    /// Gets the providers settings.
    /// If you override this method, you shoud also override TryGetProviderSettings
    /// </summary>
    /// <returns></returns>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ResponsiveDesignConfig>().Providers;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public MediaQuery CreateMediaQuery() => this.Provider.CreateMediaQuery();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public MediaQuery CreateMediaQuery(Guid id, string applicationName) => this.Provider.CreateMediaQuery(id, applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public MediaQuery GetMediaQuery(Guid id) => this.Provider.GetMediaQuery(id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </returns>
    public IQueryable<MediaQuery> GetMediaQueries() => this.Provider.GetMediaQueries();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object.
    /// </summary>
    /// <param name="mediaQuery">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object to be deleted.
    /// </param>
    public void DeleteMediaQuery(MediaQuery mediaQuery) => this.Provider.DeleteMediaQuery(mediaQuery);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public MediaQueryRule CreateMediaQueryRule() => this.Provider.CreateMediaQueryRule();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public MediaQueryRule CreateMediaQueryRule(Guid id, string applicationName) => this.Provider.CreateMediaQueryRule(id, applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public MediaQueryRule GetMediaQueryRule(Guid id) => this.Provider.GetMediaQueryRule(id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </returns>
    public IQueryable<MediaQueryRule> GetMediaQueryRules() => (IQueryable<MediaQueryRule>) this.Provider.GetMediaQueryRules().OrderBy<MediaQueryRule, string>((Expression<Func<MediaQueryRule, string>>) (r => r.Description));

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object.
    /// </summary>
    /// <param name="mediaQueryRule">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object to be deleted.
    /// </param>
    public void DeleteMediaQueryRule(MediaQueryRule mediaQueryRule) => this.Provider.DeleteMediaQueryRule(mediaQueryRule);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public MediaQueryLink CreateMediaQueryLink() => this.Provider.CreateMediaQueryLink();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public MediaQueryLink CreateMediaQueryLink(Guid id, string applicationName) => this.Provider.CreateMediaQueryLink(id, applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public MediaQueryLink GetMediaQueryLink(Guid id) => this.Provider.GetMediaQueryLink(id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.
    /// </returns>
    public IQueryable<MediaQueryLink> GetMediaQueryLinks() => this.Provider.GetMediaQueryLinks();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object.
    /// </summary>
    /// <param name="mediaQueryLink">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object to be deleted.
    /// </param>
    public void DeleteMediaQueryLink(MediaQueryLink mediaQueryLink) => this.Provider.DeleteMediaQueryLink(mediaQueryLink);

    /// <summary>
    /// Returns the IDs of pages to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    [Obsolete("Use GetPageDataIdsForMediaQuery method instead")]
    public IEnumerable<Guid> GetPageNodeIdsForMediaQuery(Guid mediaQueryId) => this.GetRelatedItemIdsForQuery(mediaQueryId, DesignMediaType.Page);

    /// <summary>
    /// Returns the IDs of pages to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    public IEnumerable<Guid> GetPageDataIdsForMediaQuery(Guid mediaQueryId) => this.GetRelatedItemIdsForQuery(mediaQueryId, DesignMediaType.Page);

    /// <summary>
    /// Returns the page nodes to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    /// <param name="pageManager"></param>
    /// <returns></returns>
    [Obsolete("Use GetPageDataForQuery method instead")]
    public IEnumerable<PageNode> GetPageNodesForQuery(
      Guid mediaQueryId,
      PageManager pageManager)
    {
      IEnumerable<Guid> pageNodeIds = this.GetPageNodeIdsForMediaQuery(mediaQueryId);
      return (IEnumerable<PageNode>) pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (n => pageNodeIds.Contains<Guid>(n.Id))).Select<PageData, PageNode>((Expression<Func<PageData, PageNode>>) (x => x.NavigationNode));
    }

    /// <summary>
    /// Returns the page data objects to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    /// <param name="pageManager"></param>
    /// <returns></returns>
    public IQueryable<PageData> GetPageDataForQuery(
      Guid mediaQueryId,
      PageManager pageManager)
    {
      IEnumerable<Guid> pageDataIds = this.GetRelatedItemIdsForQuery(mediaQueryId, DesignMediaType.Page);
      return pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (n => pageDataIds.Contains<Guid>(n.Id)));
    }

    /// <summary>
    /// Returns the IDs of templates to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    /// <returns></returns>
    public IEnumerable<Guid> GetTemplateIdsForQuery(Guid mediaQueryId) => this.GetRelatedItemIdsForQuery(mediaQueryId, DesignMediaType.Template);

    /// <summary>
    /// Returns the templates to which the specified media query is assigned explicitly.
    /// </summary>
    /// <param name="mediaQueryId"></param>
    /// <param name="pageManager"></param>
    /// <returns></returns>
    public IEnumerable<PageTemplate> GetTemplatesForQuery(
      Guid mediaQueryId,
      PageManager pageManager)
    {
      IEnumerable<Guid> templateIds = this.GetTemplateIdsForQuery(mediaQueryId);
      return (IEnumerable<PageTemplate>) pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => templateIds.Contains<Guid>(t.Id)));
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public NavigationTransformation CreateNavigationTransformation() => this.Provider.CreateNavigationTransformation();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public NavigationTransformation CreateNavigationTransformation(
      Guid id,
      string applicationName)
    {
      return this.Provider.CreateNavigationTransformation(id, applicationName);
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public NavigationTransformation GetNavigationTransformation(Guid id) => this.Provider.GetNavigationTransformation(id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </returns>
    public IQueryable<NavigationTransformation> GetNavigationTransformations() => this.Provider.GetNavigationTransformations();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object.
    /// </summary>
    /// <param name="navigationTransformation">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object to be deleted.
    /// </param>
    public void DeleteNavigationTransformation(NavigationTransformation navigationTransformation) => this.Provider.DeleteNavigationTransformation(navigationTransformation);

    internal IEnumerable<Guid> GetRelatedItemIdsForQuery(
      Guid mediaQueryId,
      DesignMediaType usageType)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ResponsiveDesignManager.\u003C\u003Ec__DisplayClass35_0 cDisplayClass350 = new ResponsiveDesignManager.\u003C\u003Ec__DisplayClass35_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass350.mediaQueryId = mediaQueryId;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass350.itemType = usageType.ToString();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return (IEnumerable<Guid>) this.GetMediaQueryLinks().Where<MediaQueryLink>((Expression<Func<MediaQueryLink, bool>>) (mql => mql.ItemType == cDisplayClass350.itemType && mql.MediaQueries.Any<MediaQuery>((Func<MediaQuery, bool>) (mq => mq.Id == cDisplayClass350.mediaQueryId)))).Select<MediaQueryLink, Guid>((Expression<Func<MediaQueryLink, Guid>>) (mql => mql.ItemId));
    }

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    public override void SaveChanges()
    {
      base.SaveChanges();
      ConfigManager manager = ConfigManager.GetManager();
      bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
      manager.Provider.SuppressSecurityChecks = true;
      ResponsiveDesignConfig section = manager.GetSection<ResponsiveDesignConfig>();
      bool flag = this.GetMediaQueries().Any<MediaQuery>((Expression<Func<MediaQuery, bool>>) (mq => mq.IsActive == true));
      if (section.Enabled != flag)
      {
        section.Enabled = flag;
        manager.SaveSection((ConfigSection) section);
      }
      manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
    }

    /// <summary>
    /// Get an instance of the Responsive Design manager using the default provider
    /// </summary>
    /// <returns>Instance of Responsive Design manager</returns>
    public static ResponsiveDesignManager GetManager() => ManagerBase<ResponsiveDesignDataProvider>.GetManager<ResponsiveDesignManager>();

    /// <summary>
    /// Get an instance of the Responsive Design manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider to use, or null/empty string to use the default provider.
    /// </param>
    /// <returns>Instance of the Responsive Design manager</returns>
    public static ResponsiveDesignManager GetManager(string providerName) => ManagerBase<ResponsiveDesignDataProvider>.GetManager<ResponsiveDesignManager>(providerName);

    /// <summary>
    /// Get an instance of the Responsive Design manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">
    /// Name of the provider to use, or null/empty string to use the default provider.
    /// </param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the Responsive Design manager</returns>
    public static ResponsiveDesignManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ResponsiveDesignDataProvider>.GetManager<ResponsiveDesignManager>(providerName, transactionName);
    }
  }
}
