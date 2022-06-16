// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryService`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services.Content.Web.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  public abstract class LibraryService<TLibrary, TChildContent, TViewModel, TManager> : 
    ContentServiceBase<TLibrary, TViewModel, TManager>
    where TLibrary : Library
    where TChildContent : MediaContent
    where TViewModel : LibraryViewModel
    where TManager : LibrariesManager, IContentLifecycleManager<TLibrary>
  {
    /// <summary>
    /// Gets the content of a specified type from the given provider.
    /// </summary>
    /// <param name="providerName">
    /// The provider from which the content ought to be retrieved.
    /// </param>
    /// <returns>Query of the content.</returns>
    public override IQueryable<TLibrary> GetContentItems(string providerName) => this.GetManager(providerName).GetItems<TLibrary>();

    /// <summary>
    /// Gets the child content of a specified content item from the given provider.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the children ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <returns>Query of the child content.</returns>
    public override IQueryable<TLibrary> GetChildContentItems(
      Guid parentId,
      string providerName)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets a single content.</summary>
    /// <param name="id">Id of the content to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the content ought to be retrieved.</param>
    /// <returns>A single content.</returns>
    public override TLibrary GetContentItem(Guid id, string providerName)
    {
      try
      {
        return (TLibrary) this.GetManager(providerName).GetItem(typeof (TLibrary), id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (TLibrary).Name, (object) id), ex);
      }
    }

    /// <summary>Gets the parent content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override TLibrary GetParentContentItem(Guid id, string providerName) => throw new NotSupportedException();

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <returns>An instance of the manager.</returns>
    public override TManager GetManager(string providerName) => (TManager) LibrariesManager.GetManager(providerName);

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>An instance of the manager.</returns>
    internal TManager GetManager(string providerName, string transactionName) => (TManager) LibrariesManager.GetManager(providerName, transactionName);

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent Id.</param>
    /// <param name="providerName">Name of the provider.</param>
    public override bool BatchChangeParent(
      string[] ids,
      string newParentId,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices(true);
      Guid result1;
      if (!Guid.TryParse(newParentId, out result1))
        return false;
      string transactionName = "MoveLibraryTask" + Guid.NewGuid().ToString();
      TManager manager = this.GetManager(providerName, transactionName);
      bool flag = false;
      for (int index = 0; index < ids.Length; ++index)
      {
        Guid result2;
        if (Guid.TryParse(ids[index], out result2))
        {
          manager.ValidateFolder(manager.GetFolder(result2), result1);
          IFolder folder = manager.GetFolder(result2);
          Guid taskId = LibrariesManager.StartLibraryMoveTask(result2, result1, providerName, transactionName);
          flag |= IContentServiceExtensions.SetLibraryRunningTaskId(folder, (LibrariesManager) manager, taskId);
        }
      }
      if (flag)
        TransactionManager.CommitTransaction(transactionName);
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>
    /// Saves the content item and returns the saved content item in JSON format. If the content item
    /// with the specified Id exists the content item will be updates; otherwise new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The Id of the content item to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="workflowOperation">The workflow operation used for the content item to be saved.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public override ContentItemContext<TLibrary> SaveContent(
      ContentItemContext<TLibrary> content,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      Lstring urlName = content.Item.UrlName;
      string[] availableLanguages = content.Item.GetAvailableLanguages();
      foreach (string cultureName in availableLanguages)
        content.Item.UrlName[cultureName] = (string) urlName;
      Library library1 = this.GetManager(providerName).GetLibraries().SingleOrDefault<Library>((Expression<Func<Library, bool>>) (a => a.Id == content.Item.Id));
      Guid? nullable1;
      if (library1 != null)
      {
        nullable1 = content.Item.ParentId;
        if (nullable1.HasValue)
        {
          nullable1 = content.Item.ParentId;
          Guid empty = Guid.Empty;
          if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
          {
            string transactionName1 = "moveUnderParent" + Guid.NewGuid().ToString();
            // ISSUE: variable of a boxed type
            __Boxed<TManager> manager = (object) this.GetManager(providerName);
            Library library2 = library1;
            nullable1 = content.Item.ParentId;
            Guid newParentId1 = nullable1.Value;
            manager.ValidateFolder((IFolder) library2, newParentId1);
            Guid id = library1.Id;
            nullable1 = content.Item.ParentId;
            Guid newParentId2 = nullable1.Value;
            string librariesProvider = providerName;
            string transactionName2 = transactionName1;
            Guid guid = LibrariesManager.StartLibraryMoveTask(id, newParentId2, librariesProvider, transactionName2);
            content.Item.RunningTask = guid;
            ContentItemContext<TLibrary> contentItemContext = base.SaveContent(content, contentId, providerName, version, published, checkOut, workflowOperation);
            TransactionManager.CommitTransaction(transactionName1);
            return contentItemContext;
          }
        }
      }
      if (library1 == null)
      {
        nullable1 = content.Item.ParentId;
        if (nullable1.HasValue)
        {
          nullable1 = content.Item.ParentId;
          Guid empty1 = Guid.Empty;
          if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != empty1 ? 1 : 0) : 0) : 1) != 0)
          {
            TManager manager1 = this.GetManager(providerName);
            Folder folder1 = manager1.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == content.Item.Id)) ?? manager1.CreateFolder(content.Item.Id);
            Lstring title = content.Item.Title;
            Lstring description = content.Item.Description;
            folder1.Title = title;
            folder1.UrlName = urlName;
            folder1.Description = description;
            foreach (string cultureName in availableLanguages)
            {
              folder1.Title[cultureName] = (string) title;
              folder1.UrlName[cultureName] = (string) urlName;
              folder1.Description[cultureName] = (string) description;
            }
            Guid empty2 = Guid.Empty;
            Guid rootId;
            if (manager1.GetItems<TLibrary>().Any<TLibrary>((Expression<Func<TLibrary, bool>>) (a => (Guid?) a.Id == content.Item.ParentId)))
            {
              Folder folder2 = folder1;
              nullable1 = content.Item.ParentId;
              Guid guid = nullable1.Value;
              folder2.RootId = guid;
              Folder folder3 = folder1;
              nullable1 = new Guid?();
              Guid? nullable2 = nullable1;
              folder3.ParentId = nullable2;
              rootId = folder1.RootId;
            }
            else
            {
              // ISSUE: variable of a boxed type
              __Boxed<TManager> manager2 = (object) manager1;
              nullable1 = content.Item.ParentId;
              Guid id = nullable1.Value;
              Folder folder4 = manager2.GetFolder(id);
              folder1.ParentId = content.Item.ParentId;
              folder1.RootId = folder4.RootId;
              rootId = folder1.RootId;
            }
            foreach (string supportedPermissionSet in manager1.GetLibrary(rootId).SupportedPermissionSets)
              manager1.GetLibrary(rootId).Demand(SecurityActionTypes.Create, supportedPermissionSet);
            manager1.ValidateFolderUrl(folder1);
            bool suppressSecurityChecks = manager1.Provider.SuppressSecurityChecks;
            manager1.Provider.SuppressSecurityChecks = true;
            manager1.Provider.Delete((Library) content.Item);
            manager1.Provider.SuppressSecurityChecks = suppressSecurityChecks;
            if (string.IsNullOrEmpty(manager1.TransactionName))
              manager1.SaveChanges();
            else
              TransactionManager.CommitTransaction(manager1.TransactionName);
            TLibrary instance = (TLibrary) Activator.CreateInstance(typeof (TLibrary), (object) manager1.Provider.ApplicationName, (object) folder1.Id);
            instance.Title = content.Item.Title;
            instance.ParentId = content.Item.ParentId;
            content.Item = instance;
            return content;
          }
        }
      }
      return base.SaveContent(content, contentId, providerName, version, published, checkOut, workflowOperation);
    }

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <param name="dataProvider">Instance of the data provider used to retrieve the items.</param>
    /// <param name="liveItem">The dictionary  of live album related to the master album</param>
    /// <param name="tempItem">The dictionary  of temp album related to the master album</param>
    /// <returns>An enumerable of view model content objects.</returns>
    public override IEnumerable<TViewModel> GetViewModelList(
      IEnumerable<TLibrary> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, TLibrary> liveContentDictionary,
      IDictionary<Guid, TLibrary> tempContentDictionary)
    {
      List<TViewModel> viewModelList = new List<TViewModel>();
      foreach (TLibrary content in contentList)
      {
        TLibrary itemFromDictionary1 = this.GetItemFromDictionary<Guid, TLibrary>(liveContentDictionary, content.Id);
        TLibrary itemFromDictionary2 = this.GetItemFromDictionary<Guid, TLibrary>(tempContentDictionary, content.Id);
        TViewModel instance = (TViewModel) Activator.CreateInstance(typeof (TViewModel), (object) content, (object) dataProvider, (object) itemFromDictionary1, (object) itemFromDictionary2);
        viewModelList.Add(instance);
      }
      if (dataProvider is IFolderOAProvider provider)
        this.SetChildFoldersCount((IEnumerable<LibraryViewModel>) viewModelList, provider.GetFolders());
      if (dataProvider is LibrariesDataProvider librariesDataProvider)
      {
        IQueryable<TChildContent> items = librariesDataProvider.GetItems(typeof (TChildContent), (string) null, (string) null, 0, 0) as IQueryable<TChildContent>;
        this.SetChildItemsInformation<TChildContent, TLibrary, TViewModel>((IEnumerable<TViewModel>) viewModelList, items.Where<TChildContent>((Expression<Func<TChildContent, bool>>) (i => i.FolderId == new Guid?())));
      }
      return (IEnumerable<TViewModel>) viewModelList;
    }

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <param name="dataProvider">Instance of the data provider used to retrieve the items.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    [Obsolete("Please use GetViewModelList with four args. Date: 2011/5/20.")]
    public override IEnumerable<TViewModel> GetViewModelList(
      IEnumerable<TLibrary> contentList,
      ContentDataProviderBase dataProvider)
    {
      List<TViewModel> viewModelList = new List<TViewModel>();
      foreach (TLibrary content in contentList)
      {
        TViewModel instance = (TViewModel) Activator.CreateInstance(typeof (TViewModel), (object) content, (object) dataProvider);
        viewModelList.Add(instance);
      }
      if (dataProvider is IFolderOAProvider provider)
        this.SetChildFoldersCount((IEnumerable<LibraryViewModel>) viewModelList, provider.GetFolders());
      if (dataProvider is LibrariesDataProvider librariesDataProvider)
      {
        IQueryable<TChildContent> items = librariesDataProvider.GetItems(typeof (TChildContent), (string) null, (string) null, 0, 0) as IQueryable<TChildContent>;
        this.SetChildItemsInformation<TChildContent, TLibrary, TViewModel>((IEnumerable<TViewModel>) viewModelList, items);
      }
      return (IEnumerable<TViewModel>) viewModelList;
    }

    public override bool DeleteContent(
      string Id,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      this.StopAndDeleteTasks(new string[1]{ Id }, providerName);
      return base.DeleteContent(Id, providerName, version, published, checkOut, workflowOperation, deletedLanguage, checkRelatingData);
    }

    public override bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      this.StopAndDeleteTasks(Ids, providerName);
      return base.BatchDeleteContent(Ids, providerName, workflowOperation, deletedLanguage, checkRelatingData);
    }

    public override bool DeleteContentInXml(
      string Id,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      this.StopAndDeleteTasks(new string[1]{ Id }, providerName);
      return base.DeleteContentInXml(Id, providerName, version, published, checkOut, workflowOperation, deletedLanguage, checkRelatingData);
    }

    public override bool BatchDeleteContentInXml(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      this.StopAndDeleteTasks(Ids, providerName);
      return base.BatchDeleteContentInXml(Ids, providerName, workflowOperation, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    public override CollectionContext<FolderViewModel> GetFolders(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      ServiceUtility.RequestAuthentication();
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(this.LandingPageId, false);
      string baseUrl = siteMapNode == null ? "" : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      int? totalCount1 = new int?(0);
      IDictionary<string, HierarchyLevelState> dictionary = (IDictionary<string, HierarchyLevelState>) null;
      IFolderManager folderManager = (IFolderManager) this.GetManager(providerName);
      IEnumerable<FolderViewModel> items;
      if (hierarchyMode)
      {
        IQueryable<TLibrary> queryable = this.GetContentItems(providerName);
        int num = queryable.Select<TLibrary, Guid>((Expression<Func<TLibrary, Guid>>) (i => i.Id)).Count<Guid>();
        using (new CultureRegion(CultureInfo.InvariantCulture))
          queryable = DataProviderBase.SetExpressions<TLibrary>(queryable, filter, sortExpression, new int?(skip), new int?(take), ref totalCount1);
        FolderViewModel[] array = ((IEnumerable<TLibrary>) queryable.ToArray<TLibrary>()).Select<TLibrary, FolderViewModel>((Func<TLibrary, FolderViewModel>) (l => new FolderViewModel((Library) l, baseUrl, providerName))).ToArray<FolderViewModel>();
        this.SetHasChildren((IEnumerable<FolderViewModel>) array, folderManager.GetFolders());
        if (take != 0)
        {
          if (dictionary == null)
            dictionary = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
          dictionary[Guid.Empty.ToString()] = new HierarchyLevelState()
          {
            Skip = skip,
            Take = take,
            Total = num
          };
        }
        items = (IEnumerable<FolderViewModel>) array;
      }
      else
      {
        int? totalCount2 = new int?(0);
        IQueryable<FolderViewModel> source1;
        using (new CultureRegion(CultureInfo.InvariantCulture))
          source1 = DataProviderBase.SetExpressions<TLibrary>(this.GetContentItems(providerName), filter, sortExpression, new int?(skip), new int?(take), ref totalCount2).Select<TLibrary, FolderViewModel>((Expression<Func<TLibrary, FolderViewModel>>) (a => new FolderViewModel(a, baseUrl, providerName)));
        int? totalCount3 = new int?(0);
        IQueryable<FolderViewModel> source2;
        using (new CultureRegion(CultureInfo.InvariantCulture))
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          source2 = DataProviderBase.SetExpressions<Folder>(folderManager.GetFolders<TLibrary>(), filter, sortExpression, new int?(skip), new int?(take), ref totalCount3).Select<Folder, FolderViewModel>(Expression.Lambda<Func<Folder, FolderViewModel>>((Expression) Expression.MemberInit(Expression.New(typeof (FolderViewModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FolderViewModel.set_Id)), )))); //unable to render the statement
        }
        int? nullable1 = totalCount2;
        int? nullable2 = totalCount3;
        totalCount1 = nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
        IQueryable<FolderViewModel> source3 = ((IEnumerable<FolderViewModel>) source1.ToArray<FolderViewModel>()).Union<FolderViewModel>((IEnumerable<FolderViewModel>) source2.ToArray<FolderViewModel>()).AsQueryable<FolderViewModel>();
        if (!string.IsNullOrEmpty(sortExpression))
          source3 = source3.OrderBy<FolderViewModel>(sortExpression);
        if (skip != 0)
          source3 = source3.Skip<FolderViewModel>(skip);
        if (take != 0)
          source3 = source3.Take<FolderViewModel>(take);
        items = (IEnumerable<FolderViewModel>) source3.ToList<FolderViewModel>();
      }
      ServiceUtility.DisableCache();
      if (dictionary != null)
      {
        FoldersCollectionContext folders = new FoldersCollectionContext(items);
        folders.TotalCount = totalCount1.HasValue ? totalCount1.Value : 0;
        folders.FoldersContext = dictionary;
        return (CollectionContext<FolderViewModel>) folders;
      }
      return new CollectionContext<FolderViewModel>(items)
      {
        TotalCount = totalCount1.HasValue ? totalCount1.Value : 0
      };
    }

    /// <summary>
    /// Gets the collection of sub folder objects and returns them in JSON format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    public override CollectionContext<FolderViewModel> GetSubFolders(
      string folderId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      ServiceUtility.RequestAuthentication();
      TManager manager = this.GetManager(providerName);
      int? totalCount = new int?(0);
      List<FolderViewModel> folderViewModelList = new List<FolderViewModel>();
      IFolderManager folderManager = (IFolderManager) manager;
      Guid folderGuid = Guid.Parse(folderId);
      IQueryable<Folder> queryable = folderManager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folderGuid || f.ParentId == new Guid?() && f.RootId == folderGuid));
      int num = queryable.Select<Folder, Guid>((Expression<Func<Folder, Guid>>) (f => f.Id)).Count<Guid>();
      using (new CultureRegion(CultureInfo.InvariantCulture))
        queryable = DataProviderBase.SetExpressions<Folder>(queryable, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
      if (queryable.Count<Folder>() > 0 & hierarchyMode)
      {
        SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(this.LandingPageId, false);
        string baseUrl;
        if (siteMapNode != null)
        {
          Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == folderGuid));
          Guid id = folder != null ? folder.RootId : folderGuid;
          Library library = manager.GetLibrary(id);
          string itemUrl = manager.GetItemUrl((ILocatable) library);
          baseUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + itemUrl;
        }
        else
          baseUrl = "";
        this.SetFolders(folderManager, folderViewModelList, queryable, providerName, baseUrl, hierarchyMode);
      }
      ServiceUtility.DisableCache();
      if (take != 0)
      {
        IDictionary<string, HierarchyLevelState> dictionary = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
        dictionary[folderId] = new HierarchyLevelState()
        {
          Skip = skip,
          Take = take,
          Total = num
        };
        FoldersCollectionContext subFolders = new FoldersCollectionContext((IEnumerable<FolderViewModel>) folderViewModelList);
        subFolders.TotalCount = folderViewModelList.Count<FolderViewModel>();
        subFolders.FoldersContext = dictionary;
        return (CollectionContext<FolderViewModel>) subFolders;
      }
      return new CollectionContext<FolderViewModel>((IEnumerable<FolderViewModel>) folderViewModelList)
      {
        TotalCount = folderViewModelList.Count<FolderViewModel>()
      };
    }

    /// <summary>Gets the collection of predecessor folders objects.</summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="excludeNeighbours">Indicates weather or not to exclude the neighbors of the folder.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    public override CollectionContext<FolderViewModel> GetPredecessorFolders(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours = false)
    {
      TManager manager = this.GetManager(providerName);
      CollectionContext<FolderViewModel> predecessorFolders = base.GetPredecessorFolders(folderId, providerName, sortExpression, excludeNeighbours);
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(this.LandingPageId, false);
      string str1 = siteMapNode != null ? RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) : string.Empty;
      if (!string.IsNullOrEmpty(str1))
      {
        foreach (FolderViewModel folderViewModel1 in predecessorFolders.Items)
        {
          Guid rootId1 = folderViewModel1.RootId;
          Guid guid1 = new Guid();
          Guid guid2 = guid1;
          Guid id = rootId1 != guid2 ? folderViewModel1.RootId : folderViewModel1.Id;
          Library library = manager.GetLibrary(id);
          string itemUrl = manager.GetItemUrl((ILocatable) library);
          string str2 = str1 + itemUrl;
          FolderViewModel folderViewModel2 = folderViewModel1;
          string baseUrl = str2;
          Dictionary<string, string> urlParams = new Dictionary<string, string>();
          urlParams.Add("provider", providerName);
          Guid rootId2 = folderViewModel1.RootId;
          guid1 = new Guid();
          Guid guid3 = guid1;
          string str3;
          if (!(rootId2 != guid3))
          {
            str3 = (string) null;
          }
          else
          {
            guid1 = folderViewModel1.Id;
            str3 = guid1.ToString();
          }
          urlParams.Add(nameof (folderId), str3);
          string str4 = this.AppendUrlParameters(baseUrl, urlParams);
          folderViewModel2.Url = str4;
        }
      }
      return predecessorFolders;
    }

    internal abstract Guid LandingPageId { get; }

    private void StopAndDeleteTasks(string[] libraryIds, string providerName)
    {
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      List<Guid> tasksToDelete = new List<Guid>();
      foreach (string libraryId in libraryIds)
      {
        Guid parsedId = Guid.Parse(libraryId);
        TLibrary library = manager.GetItems<TLibrary>().FirstOrDefault<TLibrary>((Expression<Func<TLibrary, bool>>) (x => x.Id == parsedId));
        if ((object) library != null)
        {
          Guid runningTask = library.RunningTask;
          if (runningTask != new Guid())
          {
            tasksToDelete.Add(runningTask);
            Scheduler.Instance.StopTask(runningTask);
          }
        }
      }
      Thread.Sleep(200);
      SchedulingManager.DeleteAllTasks((Expression<Func<ScheduledTaskData, bool>>) (x => tasksToDelete.Contains(x.Id)));
    }
  }
}
