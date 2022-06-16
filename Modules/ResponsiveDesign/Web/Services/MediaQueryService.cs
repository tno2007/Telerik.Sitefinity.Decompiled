// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>
  /// Web service for working with media queries of the Responsive Design module.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MediaQueryService : IMediaQueryService
  {
    /// <summary>
    /// Gets the single media query and returns it in JSON format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the media query.</param>
    /// <returns>An instance of ItemContext that contains the media query to be retrieved.</returns>
    public ItemContext<MediaQueryViewModel> GetMediaQuery(
      string mediaQueryId,
      string providerName)
    {
      return this.GetMediaQueryInternal(mediaQueryId, providerName);
    }

    /// <summary>
    /// Gets the single media query and returns it in XML format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the media query.</param>
    /// <returns>An instance of ItemContext that contains the media query to be retrieved.</returns>
    public ItemContext<MediaQueryViewModel> GetMediaQueryInXml(
      string mediaQueryId,
      string providerName)
    {
      return this.GetMediaQueryInternal(mediaQueryId, providerName);
    }

    /// <summary>
    /// Gets all the media queries for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the media queries ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the media queries.</param>
    /// <param name="skip">Number of media queries to skip in result set. (used for paging)</param>
    /// <param name="take">Number of media queries to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.</returns>
    public CollectionContext<MediaQueryViewModel> GetMediaQueries(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetMediaQueriesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all the media queries for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the media queries ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the media queries.</param>
    /// <param name="skip">Number of media queries to skip in result set. (used for paging)</param>
    /// <param name="take">Number of media queries to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.</returns>
    public CollectionContext<MediaQueryViewModel> GetMediaQueriesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetMediaQueriesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created.
    /// The saved media query is returned in JSON format.
    /// </summary>
    /// <param name="productId">Id of the media query to be saved.</param>
    /// <param name="product">The media query to be saved.</param>
    /// <param name="provider">The provider through which the media query ought to be saved.</param>
    /// <param name="itemType">The name of the actual type being saved.</param>
    /// <returns>The saved media query.</returns>
    public ItemContext<MediaQueryViewModel> SaveMediaQuery(
      string mediaQueryId,
      ItemContext<MediaQueryViewModel> mediaQuery,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveMediaQueryInternal(mediaQueryId, mediaQuery, provider, itemType);
    }

    /// <summary>
    /// Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created.
    /// The saved media query is returned in XML format.
    /// </summary>
    /// <param name="productId">Id of the media query to be saved.</param>
    /// <param name="product">The media query to be saved.</param>
    /// <param name="provider">The provider through which the media query ought to be saved.</param>
    /// <param name="itemType">The name of the actual type being saved.</param>
    /// <returns>The saved media query.</returns>
    public ItemContext<MediaQueryViewModel> SaveMediaQueryInXml(
      string mediaQueryId,
      ItemContext<MediaQueryViewModel> mediaQuery,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveMediaQueryInternal(mediaQueryId, mediaQuery, provider, itemType);
    }

    /// <summary>
    /// Deletes the media query by id and returns true if the media query has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    public bool DeleteMediaQuery(string mediaQueryId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteMediaQueryInternal(mediaQueryId, provider);
    }

    /// <summary>
    /// Deletes the media query by id and returns true if the media query has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    public bool DeleteMediaQueryInXml(string mediaQueryId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteMediaQueryInternal(mediaQueryId, provider);
    }

    /// <summary>
    /// Deletes a collection of media queries. Result is returned in JSON format.
    /// </summary>
    /// <param name="mediaQueryIds">An array of the media query ids of the media queries to delete.</param>
    /// <param name="provider">The name of the responsive design provider.</param>
    /// <returns>True if all media queries have been deleted; otherwise false.</returns>
    public bool BatchDeleteMediaQueries(string[] mediaQueryIds, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteMediaQueriesInternal(mediaQueryIds, provider);
    }

    /// <summary>
    /// Deletes a collection of media queries. Result is returned in XML format.
    /// </summary>
    /// <param name="mediaQueryIds">An array of the media query ids of the media queries to delete.</param>
    /// <param name="provider">The name of the responsive design provider.</param>
    /// <returns>True if all media queries have been deleted; otherwise false.</returns>
    public bool BatchDeleteMediaQueriesInXml(string[] mediaQueryIds, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteMediaQueriesInternal(mediaQueryIds, provider);
    }

    /// <summary>
    /// Saves or updates the media query links for the provided page/template.
    /// </summary>
    /// <param name="mediaQueryLink"></param>
    /// <param name="provider">The provider through which the media query link ought to be saved.</param>
    /// <returns>The saved media query link view model</returns>
    public ItemContext<MediaQueryLinkViewModel> SaveMediaQueryLink(
      ItemContext<MediaQueryLinkViewModel> mediaQueryLink,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveMediaQueryLinkInternal(mediaQueryLink, provider);
    }

    private ItemContext<MediaQueryViewModel> GetMediaQueryInternal(
      string mediaQueryId,
      string providerName)
    {
      Guid guid = new Guid(mediaQueryId);
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(providerName);
      MediaQuery mediaQuery = manager.GetMediaQuery(guid);
      IQueryable<MediaQueryRule> source1 = manager.GetMediaQueryRules().Where<MediaQueryRule>((Expression<Func<MediaQueryRule, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Id));
      IQueryable<NavigationTransformation> source2 = manager.GetNavigationTransformations().Where<NavigationTransformation>((Expression<Func<NavigationTransformation, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Id));
      int pagesCount = manager.GetPageDataForQuery(guid, PageManager.GetManager()).Count<PageData>();
      int templatesCount = manager.GetTemplateIdsForQuery(mediaQuery.Id).Count<Guid>();
      MediaQueryViewModel target = new MediaQueryViewModel();
      ResponsiveDesignSynchronizer.Synchronize(mediaQuery, source1.ToArray<MediaQueryRule>(), source2.ToArray<NavigationTransformation>(), pagesCount, templatesCount, target);
      return new ItemContext<MediaQueryViewModel>()
      {
        Item = target
      };
    }

    private CollectionContext<MediaQueryViewModel> GetMediaQueriesInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(provider);
      IQueryable<MediaQuery> source1 = manager.GetMediaQueries();
      int num = source1.Count<MediaQuery>();
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<MediaQuery>(sortExpression);
      if (skip > 0)
        source1 = source1.Skip<MediaQuery>(skip);
      if (take > 0)
        source1 = source1.Take<MediaQuery>(take);
      if (!string.IsNullOrEmpty(filter))
        source1 = source1.Where<MediaQuery>(filter);
      ServiceUtility.DisableCache();
      List<MediaQueryViewModel> items = new List<MediaQueryViewModel>();
      foreach (MediaQuery mediaQuery1 in (IEnumerable<MediaQuery>) source1)
      {
        MediaQuery mediaQuery = mediaQuery1;
        MediaQueryViewModel target = new MediaQueryViewModel();
        IQueryable<MediaQueryRule> source2 = manager.GetMediaQueryRules().Where<MediaQueryRule>((Expression<Func<MediaQueryRule, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Id));
        IQueryable<NavigationTransformation> source3 = manager.GetNavigationTransformations().Where<NavigationTransformation>((Expression<Func<NavigationTransformation, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Id));
        int pagesCount = manager.GetPageDataForQuery(mediaQuery.Id, PageManager.GetManager()).Count<PageData>();
        int templatesCount = manager.GetTemplateIdsForQuery(mediaQuery.Id).Count<Guid>();
        ResponsiveDesignSynchronizer.Synchronize(mediaQuery, source2.ToArray<MediaQueryRule>(), source3.ToArray<NavigationTransformation>(), pagesCount, templatesCount, target);
        items.Add(target);
      }
      return new CollectionContext<MediaQueryViewModel>((IEnumerable<MediaQueryViewModel>) items)
      {
        TotalCount = num
      };
    }

    private ItemContext<MediaQueryViewModel> SaveMediaQueryInternal(
      string mediaQueryId,
      ItemContext<MediaQueryViewModel> mediaQuery,
      string provider,
      string itemType)
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(provider);
      Guid guid = new Guid(mediaQueryId);
      if (mediaQuery.Item.Id == Guid.Empty)
      {
        MediaQuery mediaQuery1 = manager.CreateMediaQuery();
        ResponsiveDesignSynchronizer.Synchronize(mediaQuery.Item, mediaQuery1);
        if (mediaQuery.Item.Rules != null)
        {
          foreach (MediaQueryRuleViewModel rule in mediaQuery.Item.Rules)
          {
            MediaQueryRule mediaQueryRule = manager.CreateMediaQueryRule();
            ResponsiveDesignSynchronizer.Synchronize(mediaQuery1.Id, rule, mediaQueryRule);
          }
        }
        if (mediaQuery.Item.NavigationTransformations != null)
        {
          foreach (NavigationTransformationViewModel navigationTransformation1 in mediaQuery.Item.NavigationTransformations)
          {
            NavigationTransformation navigationTransformation2 = manager.CreateNavigationTransformation();
            navigationTransformation2.ApplicationName = mediaQuery1.ApplicationName;
            navigationTransformation2.ParentMediaQueryId = mediaQuery1.Id;
            navigationTransformation2.CssClasses = navigationTransformation1.CssClasses;
            navigationTransformation2.TransformationName = navigationTransformation1.TransformationName;
          }
        }
      }
      else
      {
        MediaQuery mediaQuery2 = manager.GetMediaQuery(mediaQuery.Item.Id);
        ResponsiveDesignSynchronizer.Synchronize(mediaQuery.Item, mediaQuery2);
        IQueryable<MediaQueryRule> mediaQueryRules = manager.GetMediaQueryRules();
        Expression<Func<MediaQueryRule, bool>> predicate1 = (Expression<Func<MediaQueryRule, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Item.Id);
        foreach (MediaQueryRule mediaQueryRule in mediaQueryRules.Where<MediaQueryRule>(predicate1).ToList<MediaQueryRule>())
          manager.DeleteMediaQueryRule(mediaQueryRule);
        if (mediaQuery.Item.Rules != null)
        {
          foreach (MediaQueryRuleViewModel rule in mediaQuery.Item.Rules)
          {
            MediaQueryRule mediaQueryRule = manager.CreateMediaQueryRule();
            ResponsiveDesignSynchronizer.Synchronize(mediaQuery2.Id, rule, mediaQueryRule);
          }
        }
        IQueryable<NavigationTransformation> navigationTransformations = manager.GetNavigationTransformations();
        Expression<Func<NavigationTransformation, bool>> predicate2 = (Expression<Func<NavigationTransformation, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQuery.Item.Id);
        foreach (NavigationTransformation navigationTransformation in navigationTransformations.Where<NavigationTransformation>(predicate2).ToList<NavigationTransformation>())
          manager.DeleteNavigationTransformation(navigationTransformation);
        if (mediaQuery.Item.NavigationTransformations != null)
        {
          foreach (NavigationTransformationViewModel navigationTransformation3 in mediaQuery.Item.NavigationTransformations)
          {
            NavigationTransformation navigationTransformation4 = manager.CreateNavigationTransformation();
            navigationTransformation4.ApplicationName = mediaQuery2.ApplicationName;
            navigationTransformation4.ParentMediaQueryId = mediaQuery2.Id;
            navigationTransformation4.CssClasses = navigationTransformation3.CssClasses;
            navigationTransformation4.TransformationName = navigationTransformation3.TransformationName;
          }
        }
      }
      ServiceUtility.DisableCache();
      manager.SaveChanges();
      return mediaQuery;
    }

    private bool DeleteMediaQueryInternal(string mediaQueryId, string provider)
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(provider);
      Guid mediaQueryIdGuid = new Guid(mediaQueryId);
      MediaQuery mediaQuery = manager.GetMediaQuery(mediaQueryIdGuid);
      manager.DeleteMediaQuery(mediaQuery);
      IQueryable<MediaQueryRule> mediaQueryRules = manager.GetMediaQueryRules();
      Expression<Func<MediaQueryRule, bool>> predicate1 = (Expression<Func<MediaQueryRule, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQueryIdGuid);
      foreach (MediaQueryRule mediaQueryRule in mediaQueryRules.Where<MediaQueryRule>(predicate1).ToList<MediaQueryRule>())
        manager.DeleteMediaQueryRule(mediaQueryRule);
      IQueryable<NavigationTransformation> navigationTransformations = manager.GetNavigationTransformations();
      Expression<Func<NavigationTransformation, bool>> predicate2 = (Expression<Func<NavigationTransformation, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQueryIdGuid);
      foreach (NavigationTransformation navigationTransformation in navigationTransformations.Where<NavigationTransformation>(predicate2).ToList<NavigationTransformation>())
        manager.DeleteNavigationTransformation(navigationTransformation);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool BatchDeleteMediaQueriesInternal(string[] mediaQueryIds, string provider)
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(provider);
      foreach (string mediaQueryId in mediaQueryIds)
      {
        Guid mediaQueryIdGuid = new Guid(mediaQueryId);
        MediaQuery mediaQuery = manager.GetMediaQuery(mediaQueryIdGuid);
        manager.DeleteMediaQuery(mediaQuery);
        IQueryable<MediaQueryRule> mediaQueryRules = manager.GetMediaQueryRules();
        Expression<Func<MediaQueryRule, bool>> predicate1 = (Expression<Func<MediaQueryRule, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQueryIdGuid);
        foreach (MediaQueryRule mediaQueryRule in (IEnumerable<MediaQueryRule>) mediaQueryRules.Where<MediaQueryRule>(predicate1))
          manager.DeleteMediaQueryRule(mediaQueryRule);
        IQueryable<NavigationTransformation> navigationTransformations = manager.GetNavigationTransformations();
        Expression<Func<NavigationTransformation, bool>> predicate2 = (Expression<Func<NavigationTransformation, bool>>) (mqr => mqr.ParentMediaQueryId == mediaQueryIdGuid);
        foreach (NavigationTransformation navigationTransformation in navigationTransformations.Where<NavigationTransformation>(predicate2).ToList<NavigationTransformation>())
          manager.DeleteNavigationTransformation(navigationTransformation);
      }
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private ItemContext<MediaQueryLinkViewModel> SaveMediaQueryLinkInternal(
      ItemContext<MediaQueryLinkViewModel> mediaQueryLinkVM,
      string provider)
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(provider);
      MediaQueryLink mediaQueryLink1 = manager.GetMediaQueryLinks().Where<MediaQueryLink>((Expression<Func<MediaQueryLink, bool>>) (m => m.ItemId == mediaQueryLinkVM.Item.ItemId)).SingleOrDefault<MediaQueryLink>();
      MediaQueryLinkType linkType = (MediaQueryLinkType) Enum.Parse(typeof (MediaQueryLinkType), mediaQueryLinkVM.Item.LinkType);
      if (mediaQueryLink1 == null)
      {
        if (linkType == MediaQueryLinkType.Inherit)
          return mediaQueryLinkVM;
        MediaQueryLink mediaQueryLink2 = manager.CreateMediaQueryLink();
        ResponsiveDesignSynchronizer.Synchronize(mediaQueryLinkVM.Item, mediaQueryLink2, linkType);
      }
      else if (linkType == MediaQueryLinkType.Inherit)
        manager.DeleteMediaQueryLink(mediaQueryLink1);
      else
        ResponsiveDesignSynchronizer.Synchronize(mediaQueryLinkVM.Item, mediaQueryLink1, linkType);
      ServiceUtility.DisableCache();
      manager.SaveChanges();
      return mediaQueryLinkVM;
    }
  }
}
