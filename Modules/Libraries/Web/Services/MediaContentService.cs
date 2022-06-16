// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.MediaContentService`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Content.Web.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  public class MediaContentService<TMediaContent, TLibrary, TMediaContentViewModel, TManager> : 
    ContentServiceBase<TMediaContent, TLibrary, TMediaContentViewModel, LibraryViewModel, TManager>
    where TMediaContent : MediaContent
    where TLibrary : Library
    where TMediaContentViewModel : MediaContentViewModel
    where TManager : LibrariesManager, IContentLifecycleManager<TMediaContent>
  {
    /// <summary>
    /// Gets the content of a specified type from the given provider.
    /// </summary>
    /// <param name="providerName">
    /// The provider from which the content ought to be retrieved.
    /// </param>
    /// <returns>Query of the content.</returns>
    public override IQueryable<TMediaContent> GetContentItems(string providerName) => this.GetManager(providerName).GetItems<TMediaContent>();

    /// <summary>
    /// Gets the child content of a specified content item from the given provider.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the children ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <returns>Query of the child content.</returns>
    public override IQueryable<TMediaContent> GetChildContentItems(
      Guid parentId,
      string providerName)
    {
      TManager manager = this.GetManager(providerName);
      if (manager.GetItems<TLibrary>().Any<TLibrary>((Expression<Func<TLibrary, bool>>) (a => a.Id == parentId)))
        return manager.GetItems<TMediaContent>().Where<TMediaContent>((Expression<Func<TMediaContent, bool>>) (img => img.Parent.Id == parentId && img.FolderId == new Guid?()));
      return manager.GetItems<TMediaContent>().Where<TMediaContent>((Expression<Func<TMediaContent, bool>>) (img => img.FolderId == (Guid?) parentId));
    }

    /// <inheritdoc />
    protected override IQueryable<TMediaContent> GetDescendants(
      Guid parentId,
      string providerName,
      string filterExpression)
    {
      TManager manager = this.GetManager(providerName);
      IFolder folder = manager.GetFolder(parentId);
      IQueryable<TMediaContent> queryable = manager.GetItems<TMediaContent>();
      if (!string.IsNullOrEmpty(filterExpression))
        queryable = queryable.Where<TMediaContent>(filterExpression);
      return manager.GetDescendantsFromQuery<TMediaContent>(queryable, folder);
    }

    /// <inheritdoc />
    protected override IQueryable<TMediaContent> GetDescendants(
      Guid parentId,
      string providerName)
    {
      return this.GetDescendants(parentId, providerName, (string) null);
    }

    /// <summary>Gets a single content.</summary>
    /// <param name="id">Id of the content to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the content ought to be retrieved.</param>
    /// <returns>A single content.</returns>
    public override TMediaContent GetContentItem(Guid id, string providerName)
    {
      try
      {
        return (TMediaContent) this.GetManager(providerName).GetItem(typeof (TMediaContent), id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (TMediaContent).Name, (object) id), ex);
      }
    }

    /// <summary>Gets the parent content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override TLibrary GetParentContentItem(Guid id, string providerName)
    {
      try
      {
        return (TLibrary) this.GetManager(providerName).GetItem(typeof (TLibrary), id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (TLibrary).Name), ex);
      }
    }

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <returns>An instance of the manager.</returns>
    public override TManager GetManager(string providerName) => WcfContext.CurrentTransactionName.IsNullOrWhitespace() ? (TManager) LibrariesManager.GetManager(providerName) : (TManager) LibrariesManager.GetManager(providerName, WcfContext.CurrentTransactionName);

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <param name="transactioName">The name of the transaction.</param>
    /// <returns>An instance of the manager.</returns>
    internal TManager GetManager(string providerName, string transactioName) => (TManager) LibrariesManager.GetManager(providerName, transactioName);

    /// <summary>Gets the view model list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="liveItem">The dictionary  of live image related to the master image</param>
    /// <param name="tempItem">The dictionary  of temp image related to the master image</param>
    /// <returns></returns>
    public override IEnumerable<TMediaContentViewModel> GetViewModelList(
      IEnumerable<TMediaContent> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, TMediaContent> liveContentDictionary,
      IDictionary<Guid, TMediaContent> tempContentDictionary)
    {
      List<TMediaContentViewModel> viewModelList = new List<TMediaContentViewModel>();
      foreach (TMediaContent content in contentList)
      {
        TMediaContent itemFromDictionary1 = this.GetItemFromDictionary<Guid, TMediaContent>(liveContentDictionary, content.Id);
        TMediaContent itemFromDictionary2 = this.GetItemFromDictionary<Guid, TMediaContent>(tempContentDictionary, content.Id);
        TMediaContentViewModel instance = (TMediaContentViewModel) Activator.CreateInstance(typeof (TMediaContentViewModel), (object) content, (object) dataProvider, (object) itemFromDictionary1, (object) itemFromDictionary2);
        viewModelList.Add(instance);
      }
      return (IEnumerable<TMediaContentViewModel>) viewModelList;
    }

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <param name="dataProvider">Instance of the data provider used to retrieve the items.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    [Obsolete("Please use GetViewModelList with four args. Date: 2011/5/20.")]
    public override IEnumerable<TMediaContentViewModel> GetViewModelList(
      IEnumerable<TMediaContent> contentList,
      ContentDataProviderBase dataProvider)
    {
      List<TMediaContentViewModel> viewModelList = new List<TMediaContentViewModel>();
      foreach (TMediaContent content in contentList)
      {
        TMediaContentViewModel instance = (TMediaContentViewModel) Activator.CreateInstance(typeof (TMediaContentViewModel), (object) content, (object) dataProvider);
        viewModelList.Add(instance);
      }
      return (IEnumerable<TMediaContentViewModel>) viewModelList;
    }

    protected override void OnBeforeCheckOutItem(TMediaContent item)
    {
      base.OnBeforeCheckOutItem(item);
      LibraryViewModel.ValidateMediaContentBeforeCheckout((MediaContent) item);
    }

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
      Guid result;
      if (!Guid.TryParse(newParentId, out result))
        return false;
      string str = "BatchChangeParentTask" + Guid.NewGuid().ToString();
      TManager manager = this.GetManager(providerName, str);
      List<IFolder> source = new List<IFolder>();
      string[] ids1 = new string[1];
      for (int index = 0; index < ids.Length; ++index)
      {
        Guid id;
        if (Guid.TryParse(ids[index], out id))
        {
          if (manager.GetFolders().Any<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == id)))
          {
            IFolder folder = manager.GetFolder(id);
            manager.ValidateFolder(folder, result);
            source.Add(folder);
          }
          else
          {
            ids1[0] = ids[index];
            base.BatchChangeParent(ids1, newParentId, providerName, workflowOperation);
          }
        }
      }
      if (source.Any<IFolder>())
      {
        Guid taskId = LibrariesManager.StartLibraryMoveTask(source.Select<IFolder, Guid>((Func<IFolder, Guid>) (f => f.Id)).ToList<Guid>().ToArray(), result, providerName, str);
        foreach (IFolder folderObj in source)
          IContentServiceExtensions.SetLibraryRunningTaskId(folderObj, (LibrariesManager) manager, taskId);
        TransactionManager.CommitTransaction(str);
      }
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    public override bool BatchDeleteChildContent(
      string[] Ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      List<string> list = ((IEnumerable<string>) Ids).ToList<string>();
      Guid parentGuid;
      if (Guid.TryParse(parentId, out parentGuid))
      {
        LibrariesManager manager = LibrariesManager.GetManager(providerName);
        for (int index = list.Count - 1; index >= 0; --index)
        {
          Guid itemGuid;
          if (Guid.TryParse(list[index], out itemGuid))
          {
            Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == itemGuid));
            if (folder != null)
            {
              manager.Delete((IFolder) folder);
              list.RemoveAt(index);
            }
          }
        }
        manager.SaveChanges();
        Folder folder1 = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == parentGuid));
        if (folder1 != null)
          return base.BatchDeleteChildContent(list.ToArray(), folder1.RootId.ToString(), providerName, workflowOperation, deletedLanguage, checkRelatingData);
      }
      return base.BatchDeleteChildContent(list.ToArray(), parentId, providerName, workflowOperation, deletedLanguage, checkRelatingData);
    }

    /// <inheridoc />
    public override bool BatchPublishChildItem(
      string[] ids,
      string providerName,
      string parentId,
      string workflowOperation)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MediaContentService<TMediaContent, TLibrary, TMediaContentViewModel, TManager>.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new MediaContentService<TMediaContent, TLibrary, TMediaContentViewModel, TManager>.\u003C\u003Ec__DisplayClass13_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.ids = ids;
      // ISSUE: reference to a compiler-generated field
      bool flag = base.BatchPublishChildItem(cDisplayClass130.ids, providerName, parentId, workflowOperation);
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      List<MediaContent> list = manager.GetMediaItems().Where<MediaContent>(Expression.Lambda<Func<MediaContent, bool>>((Expression) Expression.AndAlso((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
      {
        cDisplayClass130.ids,
        (Expression) Expression.Call(i.OriginalContentId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())
      }), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Telerik.Sitefinity.GenericContent.Model.Content.get_Status))), typeof (int)), (Expression) Expression.Constant((object) 2, typeof (int)))), parameterExpression)).ToList<MediaContent>();
      this.EnsureMediaFileForLifecycleItems(manager, (IEnumerable<MediaContent>) list, ContentLifecycleStatus.Live);
      return flag;
    }

    /// <summary>
    /// Saves the child content item and returns the saved child content item in JSON format. If the child
    /// content item with the specified pageId exists the content item will be updated; otherwise a new child
    /// content item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be saved.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child content item.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public override ContentItemContext<TMediaContent> SaveChildContent(
      ContentItemContext<TMediaContent> content,
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      try
      {
        LibrariesManager manager = LibrariesManager.GetManager(providerName);
        Guid parentGuid;
        if (!string.IsNullOrEmpty(newParentId) && Guid.TryParse(newParentId, out parentGuid))
        {
          Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == parentGuid));
          if (folder != null)
          {
            content.Item.FolderId = new Guid?(folder.Id);
            newParentId = folder.RootId.ToString();
          }
          else
            content.Item.FolderId = new Guid?();
        }
        if (!content.Item.IsVectorGraphics() && content.Item.Uploaded && !content.Item.GetThumbnails((CultureInfo) null).Any<Thumbnail>())
          manager.RegenerateThumbnails((MediaContent) content.Item);
        return base.SaveChildContent(content, parentId, contentId, providerName, newParentId, version, published, checkOut, workflowOperation);
      }
      catch (SecurityDemandFailException ex)
      {
        throw new SecurityDemandFailException("You don't have permissions to perform this action.");
      }
    }

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="excludeFolders">Default value is false. If set to true gets only the specific media content items, else gets the media content items and the folders of the same level.</param>
    /// <param name="includeSubFoldersItems">Default value is false. If set to true then gets all media content items including those in the library subfolders.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public override CollectionContext<TMediaContentViewModel> GetChildrenContentItems(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      string workflowOperation,
      bool excludeFolders = false,
      bool includeSubFoldersItems = false)
    {
      if (excludeFolders)
        return base.GetChildrenContentItems(parentId, providerName, sortExpression, filter, skip, take, workflowOperation, excludeFolders, includeSubFoldersItems);
      ServiceUtility.RequestAuthentication();
      TManager manager = this.GetManager(providerName);
      Guid parentGuid = new Guid(parentId);
      IQueryable<IFolder> source1;
      if (manager.GetFolders().Any<Folder>((Expression<Func<Folder, bool>>) (a => a.Id == parentGuid)))
        source1 = (IQueryable<IFolder>) manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) parentGuid));
      else
        source1 = (IQueryable<IFolder>) manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.RootId == parentGuid && f.ParentId == new Guid?()));
      if (!string.IsNullOrWhiteSpace(filter))
      {
        if (!NamedFiltersHandler.TryParseFilterName(filter, out string _))
        {
          try
          {
            using (new CultureRegion(CultureInfo.InvariantCulture))
              source1 = source1.Where<IFolder>(filter);
          }
          catch (MemberAccessException ex)
          {
          }
          catch (ParseException ex)
          {
          }
        }
      }
      int num = source1.Count<IFolder>();
      string[] source2 = new string[1]{ "Title" };
      string sortExpressionName;
      if (!string.IsNullOrWhiteSpace(sortExpression) && ContentHelper.TryParseSortExpressionName(sortExpression, out sortExpressionName))
      {
        if (((IEnumerable<string>) source2).Contains<string>(sortExpressionName))
        {
          try
          {
            using (new CultureRegion(CultureInfo.InvariantCulture))
              source1 = source1.OrderBy<IFolder>(sortExpression);
          }
          catch (MemberAccessException ex)
          {
          }
          catch (ParseException ex)
          {
          }
        }
      }
      int skip1 = skip;
      int take1 = take;
      if (skip != 0)
      {
        source1 = source1.Skip<IFolder>(skip);
        skip1 -= num;
      }
      if (take != 0)
      {
        source1 = source1.Take<IFolder>(take);
        take1 -= source1.Count<IFolder>();
      }
      List<TMediaContentViewModel> source3 = new List<TMediaContentViewModel>();
      foreach (IFolder folder in source1.ToList<IFolder>())
      {
        TMediaContentViewModel instance = (TMediaContentViewModel) Activator.CreateInstance(typeof (TMediaContentViewModel), (object) folder, (object) manager.Provider);
        source3.Add(instance);
      }
      CollectionContext<TMediaContentViewModel> childrenContentItems = base.GetChildrenContentItems(parentId, providerName, sortExpression, filter, skip1, take1, workflowOperation, excludeFolders, includeSubFoldersItems);
      List<TMediaContentViewModel> items = new List<TMediaContentViewModel>(childrenContentItems.Items.Count<TMediaContentViewModel>() + source3.Count<TMediaContentViewModel>());
      foreach (TMediaContentViewModel contentViewModel in source3)
        items.Add(contentViewModel);
      if (take1 > 0)
        items.AddRange(childrenContentItems.Items);
      ServiceUtility.DisableCache();
      return new CollectionContext<TMediaContentViewModel>((IEnumerable<TMediaContentViewModel>) items)
      {
        TotalCount = childrenContentItems.TotalCount + num
      };
    }

    protected internal override bool CallWorkflowBatch(
      string[] ids,
      string providerName,
      string operationName,
      string deletedLanguage = null,
      bool checkRelatingData = false)
    {
      TManager manager = this.GetManager(providerName);
      return base.CallWorkflowBatch(((IEnumerable<string>) ids).Where<string>((Func<string, bool>) (id => !manager.GetFolders().Any<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == new Guid(id))))).ToArray<string>(), providerName, operationName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Saves the child folder and returns the saved folder item in XML format. If the child
    /// folder with the specified contentId exists the folder item will be updated; otherwise a new child
    /// folder item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the folder to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child folder.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    public override ItemContext<FolderDetailViewModel> SaveChildFolder(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName)
    {
      return this.SaveChildFolderInternal(content, parentId, contentId, providerName);
    }

    /// <summary>
    /// Saves the child folder and returns the saved folder item in XML format. If the child
    /// folder with the specified contentId exists the folder item will be updated; otherwise a new child
    /// folder item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the folder to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child folder.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    public override ItemContext<FolderDetailViewModel> SaveChildFolderInXml(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName)
    {
      return this.SaveChildFolderInternal(content, parentId, contentId, providerName);
    }

    /// <summary>Sets the image as cover of its parent.</summary>
    /// <param name="contentId">The Id of the image.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    public override void SetAsCover(string contentId, string providerName)
    {
      Guid result;
      if (string.IsNullOrEmpty(contentId) || !Guid.TryParse(contentId, out result))
        throw new ArgumentNullException(nameof (contentId));
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      Telerik.Sitefinity.Libraries.Model.Image image = manager.GetImage(result);
      Guid? nullable = image != null ? image.FolderId : throw new ItemNotFoundException(string.Format("An image with id: '{0}' is not found.", (object) contentId));
      IFolder folder;
      if (!nullable.HasValue)
      {
        folder = (IFolder) image.Parent;
      }
      else
      {
        LibrariesManager librariesManager = manager;
        nullable = image.FolderId;
        Guid id = nullable.Value;
        folder = librariesManager.GetFolder(id);
      }
      folder.CoverId = new Guid?(result);
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public override CollectionContext<MediaFileLinkViewModel> GetMediaFileLinks(
      string contentId,
      string providerName,
      string culture)
    {
      using (new CultureRegion(culture))
      {
        Guid result;
        if (string.IsNullOrEmpty(contentId) || !Guid.TryParse(contentId, out result))
          throw new ArgumentNullException(nameof (contentId));
        LibrariesManager manager = LibrariesManager.GetManager(providerName);
        MediaContent mediaItem = manager.GetMediaItem(result);
        if (mediaItem == null)
          throw new ItemNotFoundException(string.Format("Media content with id: '{0}' is not found.", (object) contentId));
        if (!(manager.GetMaster((Telerik.Sitefinity.GenericContent.Model.Content) mediaItem) is MediaContent master))
          throw new ItemNotFoundException(string.Format("Master item for media content with id: '{0}' is not found.", (object) contentId));
        IOrderedEnumerable<\u003C\u003Ef__AnonymousType20<Guid, int, IOrderedEnumerable<MediaFileLink>>> source = master.MediaFileLinks.GroupBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (ml => ml.FileId)).Select(g => new
        {
          FileId = g.Key,
          Count = g.Count<MediaFileLink>(),
          MediaLinks = g.OrderBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (mfl => mfl.FileId))
        }).OrderByDescending(grp => grp.Count);
        Guid defaultFileId = mediaItem.FileId;
        return new CollectionContext<MediaFileLinkViewModel>(source.Select(g => g.MediaLinks.FirstOrDefault<MediaFileLink>()).Select<MediaFileLink, MediaFileLinkViewModel>((Func<MediaFileLink, MediaFileLinkViewModel>) (g => new MediaFileLinkViewModel(g)
        {
          IsDefault = defaultFileId == g.FileId
        })));
      }
    }

    /// <inheritdoc />
    public override void CopyFileLink(string contentId, string providerName, string fileId)
    {
      Guid result;
      if (string.IsNullOrEmpty(contentId) || !Guid.TryParse(contentId, out result))
        throw new ArgumentNullException(nameof (contentId));
      Guid fileIdGuid;
      if (string.IsNullOrEmpty(fileId) || !Guid.TryParse(fileId, out fileIdGuid))
        throw new ArgumentNullException(nameof (fileId));
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      MediaContent mediaItem = manager.GetMediaItem(result);
      if (mediaItem == null)
        throw new ItemNotFoundException(string.Format("Media content with id: '{0}' is not found.", (object) contentId));
      MediaFileLink mediaFileLink = mediaItem.MediaFileLinks.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileIdGuid));
      if (mediaFileLink != null)
      {
        CultureInfo cultureByLcid = AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture);
        manager.CopyUploadedContent(mediaItem, cultureByLcid, true);
      }
      manager.SaveChanges();
    }

    /// <inheritdoc />
    protected override void SaveAdditionalInfo(
      TManager manager,
      TMediaContent item,
      Dictionary<string, object> additionalInfo)
    {
      base.SaveAdditionalInfo(manager, item, additionalInfo);
      manager.RecompileMediaFileUrls((MediaContent) item, additionalInfo);
    }

    /// <inheritdoc />
    protected override void LoadAdditionalInfo(
      TManager manager,
      ContentItemContext<TMediaContent> context)
    {
      base.LoadAdditionalInfo(manager, context);
      if (context == null)
        return;
      List<TMediaContent> items = new List<TMediaContent>()
      {
        context.Item
      };
      this.EnsureMediaFileForLifecycleItems((LibrariesManager) manager, (IEnumerable<MediaContent>) items, ContentLifecycleStatus.Temp);
      if (context.SfAdditionalInfo == null)
        context.SfAdditionalInfo = new Dictionary<string, object>();
      TMediaContent mediaContentItem = context.Item;
      manager.LoadMediaContentAdditionalInfo((MediaContent) mediaContentItem, context.SfAdditionalInfo);
    }

    /// <summary>
    /// When a temp or live(from More actions -&gt; Publish) item is created, it has no file attached to it.
    /// We copy the file from the "default" version
    /// </summary>
    /// <param name="manager">The manager to use</param>
    /// <param name="items">The items to attach file to.</param>
    private void EnsureMediaFileForLifecycleItems(
      LibrariesManager manager,
      IEnumerable<MediaContent> items,
      ContentLifecycleStatus status)
    {
      int currentCulture = AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      bool flag = false;
      foreach (MediaContent mediaContent in items)
      {
        if ((mediaContent.Status == status || status == ContentLifecycleStatus.Temp && mediaContent.Status == ContentLifecycleStatus.PartialTemp) && mediaContent.FileId == Guid.Empty)
        {
          MediaFileLink mediaFileLink = mediaContent.MediaFileLinks.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (fl => fl.Culture != currentCulture));
          if (mediaFileLink != null)
          {
            CultureInfo cultureByLcid = AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture);
            manager.CopyUploadedContent(mediaContent, cultureByLcid);
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      manager.SaveChanges();
    }

    private ItemContext<FolderDetailViewModel> SaveChildFolderInternal(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName)
    {
      if (content.Item.Id != Guid.Empty)
      {
        ServiceUtility.RequestAuthentication();
        string transactioName = "SaveChildFolderTask" + Guid.NewGuid().ToString();
        TManager manager = this.GetManager(providerName, transactioName);
        IFolder folder = manager.GetFolder(content.Item.Id);
        this.CheckChildContentPermissions(this.GetLibraryByFolder((LibrariesManager) manager, folder));
        Guid parentId1 = folder.ParentId;
        Guid parentId2 = content.Item.ParentId;
        if (parentId1 != parentId2)
        {
          manager.ValidateFolder(folder, parentId2);
          content.Item.ParentId = parentId1;
          Guid taskId = LibrariesManager.StartLibraryMoveTask(folder.Id, parentId2, providerName, manager.TransactionName);
          IContentServiceExtensions.SetLibraryRunningTaskId(folder, (LibrariesManager) manager, taskId);
          ItemContext<FolderDetailViewModel> itemContext = base.SaveChildFolder(content, parentId, contentId, providerName);
          TransactionManager.CommitTransaction(manager.TransactionName);
          return itemContext;
        }
      }
      return base.SaveChildFolder(content, parentId, contentId, providerName);
    }

    private void CheckChildContentPermissions(Library library)
    {
      if (library != null)
      {
        if (library is Telerik.Sitefinity.Libraries.Model.Album)
        {
          if (!library.IsGranted("Image", "ManageImage"))
            goto label_8;
        }
        if (library is Telerik.Sitefinity.Libraries.Model.DocumentLibrary)
        {
          if (!library.IsGranted("Document", "ManageDocument"))
            goto label_8;
        }
        if (!(library is Telerik.Sitefinity.Libraries.Model.VideoLibrary))
          return;
        if (library.IsGranted("Video", "ManageVideo"))
          return;
label_8:
        throw new SecurityDemandFailException("You don't have permissions to perform this action.");
      }
    }

    private Library GetLibraryByFolder(LibrariesManager manager, IFolder item) => item is Folder folder && folder.RootId != Guid.Empty ? manager.GetLibrary(folder.RootId) : (Library) null;
  }
}
