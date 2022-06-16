// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentItemService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  /// <summary>
  /// This web service is used to work with <see cref="T:Telerik.Sitefinity.GenericContent.Model.ContentItem" /> objects.
  /// </summary>
  public class ContentItemService : ContentServiceBase<ContentItem, ContentViewModel, ContentManager>
  {
    private static readonly Regex pageIdFilterRegex = new Regex("PageId\\=([a-z0-9A-Z\\-]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Gets the content of a specified type from the given provider.
    /// </summary>
    /// <param name="providerName">
    /// The provider from which the content ought to be retrieved.
    /// </param>
    /// <returns>Query of the content.</returns>
    public override IQueryable<ContentItem> GetContentItems(string providerName)
    {
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "DraftLinksParser"
        });
      return this.GetManager(providerName).GetContent();
    }

    protected override ProcessFilterExtendedResult<ContentItem> ProcessFilterExtended(
      string sortExpression,
      int skip,
      int take,
      ref string filter,
      string providerName,
      ref int? totalCount)
    {
      ContentManager manager = this.GetManager(providerName);
      if ("#ContentBlocksNotUsedOnAnyPage#".Equals(filter, StringComparison.OrdinalIgnoreCase))
      {
        ProcessFilterExtendedResult<ContentItem> filterExtendedResult = new ProcessFilterExtendedResult<ContentItem>();
        filterExtendedResult.SkipRegularLogic = false;
        filterExtendedResult.PostProcessingContext = (object) null;
        filterExtendedResult.CustomQuery = (IQueryable<ContentItem>) null;
        filter = string.Empty;
        filterExtendedResult.CustomQuery = manager.GetContentNotUsedOnAnyPage();
        return filterExtendedResult;
      }
      if (filter == null || !filter.Contains("PageId="))
        return base.ProcessFilterExtended(sortExpression, skip, take, ref filter, providerName, ref totalCount);
      ProcessFilterExtendedResult<ContentItem> filterExtendedResult1 = new ProcessFilterExtendedResult<ContentItem>()
      {
        SkipRegularLogic = false,
        PostProcessingContext = (object) null,
        CustomQuery = Enumerable.Empty<ContentItem>().AsQueryable<ContentItem>()
      };
      Match match = ContentItemService.pageIdFilterRegex.Match(filter, 0);
      filter = filter.Replace(match.Value, "");
      if (match.Success && match.Groups.Count >= 2 && match.Groups[1].Success)
      {
        Guid pageID;
        if (Guid.TryParse(match.Groups[1].Value, out pageID))
        {
          PageNode pageNode = PageManager.GetManager().GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == pageID)).SingleOrDefault<PageNode>();
          if (pageNode != null)
          {
            Guid id = pageNode.GetPageData().Id;
            IQueryable<ContentItem> contentByPage = this.GetManager(providerName).GetContentByPage(id);
            List<ContentItem> source = new List<ContentItem>();
            foreach (ContentItem cnt in (IEnumerable<ContentItem>) contentByPage)
              source.Add(manager.GetMaster(cnt));
            filterExtendedResult1.CustomQuery = source.AsQueryable<ContentItem>();
          }
        }
      }
      return filterExtendedResult1;
    }

    /// <summary>
    /// Gets the child content of a specified content item from the given provider.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the children ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <returns>Query of the child content.</returns>
    public override IQueryable<ContentItem> GetChildContentItems(
      Guid parentId,
      string providerName)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets a single content.</summary>
    /// <param name="id">Id of the content to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the content ought to be retrieved.</param>
    /// <returns>A single content.</returns>
    public override ContentItem GetContentItem(Guid id, string providerName)
    {
      try
      {
        return this.GetManager(providerName).GetContent(id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (ContentItem).Name, (object) id), ex);
      }
    }

    /// <summary>Gets the parent content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override ContentItem GetParentContentItem(Guid id, string providerName) => throw new NotSupportedException();

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <returns>An instance of the manager.</returns>
    public override ContentManager GetManager(string providerName) => ContentManager.GetManager(providerName);

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>An instance of the manager.</returns>
    public ContentManager GetManager(string providerName, string transactionName) => ContentManager.GetManager(providerName, transactionName);

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <param name="dataProvider">Instance of the data provider used to retrieve the items.</param>
    /// <param name="liveItem">The live content item related to the master content item.</param>
    /// <param name="tempItem">The temp content item related to the master content item.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    public override IEnumerable<ContentViewModel> GetViewModelList(
      IEnumerable<ContentItem> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, ContentItem> liveContentDictionary,
      IDictionary<Guid, ContentItem> tempContentDictionary)
    {
      List<ContentViewModel> viewModelList = new List<ContentViewModel>();
      foreach (ContentItem content in contentList)
      {
        ContentItem itemFromDictionary1 = this.GetItemFromDictionary<Guid, ContentItem>(liveContentDictionary, content.Id);
        ContentItem itemFromDictionary2 = this.GetItemFromDictionary<Guid, ContentItem>(tempContentDictionary, content.Id);
        viewModelList.Add(new ContentViewModel(content, dataProvider, itemFromDictionary1, itemFromDictionary2));
      }
      return (IEnumerable<ContentViewModel>) viewModelList;
    }

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <param name="dataProvider">Instance of the data provider used to retrieve the items.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    [Obsolete("Please use GetViewModelList with four args. Date: 2011/5/20.")]
    public override IEnumerable<ContentViewModel> GetViewModelList(
      IEnumerable<ContentItem> contentList,
      ContentDataProviderBase dataProvider)
    {
      List<ContentViewModel> viewModelList = new List<ContentViewModel>();
      foreach (ContentItem content in contentList)
        viewModelList.Add(new ContentViewModel((Content) content, dataProvider));
      return (IEnumerable<ContentViewModel>) viewModelList;
    }

    /// <summary>Saves the content.</summary>
    /// <param name="content">The content.</param>
    /// <param name="contentId">The content id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="version">The version.</param>
    /// <param name="published">The published.</param>
    /// <param name="checkOut">The check out.</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <returns></returns>
    public override ContentItemContext<ContentItem> SaveContent(
      ContentItemContext<ContentItem> context,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ContentManager manager = this.GetManager(providerName);
      ContentItem contentItem1 = context.Item;
      Guid result = Guid.Empty;
      if (SystemManager.CurrentHttpContext.Request.QueryString["draftPageId"] != null)
        Guid.TryParse(SystemManager.CurrentHttpContext.Request.QueryString["draftPageId"], out result);
      CommonMethods.RecompileItemUrls((Content) contentItem1, (IManager) manager);
      ContentUIStatus uiStatus;
      this.FixContentStatus(contentItem1, providerName, out uiStatus);
      VersionManager versionManager = (VersionManager) null;
      if (contentItem1.SupportsContentLifecycle)
      {
        if (contentItem1.Status == ContentLifecycleStatus.Live)
          contentItem1 = manager.Edit(contentItem1);
        if (contentItem1.Status == ContentLifecycleStatus.Master)
          contentItem1 = manager.CheckOut(contentItem1);
        if (contentItem1.Status == ContentLifecycleStatus.Temp)
          contentItem1 = manager.CheckIn(contentItem1);
        if (contentItem1.Status != ContentLifecycleStatus.Master)
          throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpected);
      }
      ++contentItem1.Version;
      if (contentItem1 != null)
      {
        versionManager = VersionManager.GetManager();
        versionManager.CreateVersion((IDataItem) contentItem1, uiStatus == ContentUIStatus.Published);
      }
      if (contentItem1.SupportsContentLifecycle)
      {
        switch (uiStatus)
        {
          case ContentUIStatus.Published:
            contentItem1 = manager.Publish(contentItem1);
            break;
          case ContentUIStatus.Scheduled:
            contentItem1 = manager.Schedule(contentItem1, contentItem1.PublicationDate, contentItem1.ExpirationDate);
            break;
        }
      }
      Guid id = manager.Publish(manager.GetMaster(contentItem1)).Id;
      manager.SaveChanges();
      versionManager?.SaveChanges();
      ServiceUtility.DisableCache();
      ContentItem contentItem2 = this.GetContentItem(id, providerName);
      ContentItemContext<ContentItem> contentItemContext = new ContentItemContext<ContentItem>();
      contentItemContext.Item = contentItem2;
      return contentItemContext;
    }

    private void FixContentStatus(
      ContentItem content,
      string providerName,
      out ContentUIStatus uiStatus)
    {
      uiStatus = content.UIStatus;
      if (content.OriginalContentId == Guid.Empty)
        content.Status = ContentLifecycleStatus.Master;
      else
        content.Status = ContentLifecycleStatus.Temp;
    }

    /// <summary>Deletes the content.</summary>
    /// <param name="Id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="version">The version.</param>
    /// <param name="published">The published.</param>
    /// <param name="checkOut">The check out.</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="deletedLanguage">The deleted language.</param>
    /// <returns></returns>
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
      ServiceUtility.RequestBackendUserAuthentication();
      ContentItem contentItem = this.GetContentItem(new Guid(Id), providerName);
      RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(Id), contentItem.GetType().FullName);
      string transactionName = Guid.NewGuid().ToString();
      ContentManager manager1 = this.GetManager(providerName, transactionName);
      if (version != null)
      {
        this.DeleteVersion(version);
        return true;
      }
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage))
        language = new CultureInfo(deletedLanguage);
      if (contentItem.SupportsContentLifecycle)
        contentItem = manager1.GetMaster(contentItem);
      manager1.DeleteItem((object) contentItem, language);
      if (manager1.Provider.GetDirtyItemStatus((object) contentItem) == SecurityConstants.TransactionActionType.Deleted)
      {
        PageManager manager2 = PageManager.GetManager((string) null, transactionName);
        manager1.UnshareContentOnAllPages(new Guid(Id), manager2);
      }
      TransactionManager.CommitTransaction(transactionName);
      return true;
    }

    /// <summary>Batches the content of the delete.</summary>
    /// <param name="Ids">The ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="deletedLanguage">The deleted language.</param>
    /// <returns></returns>
    public override bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      string transactionName = Guid.NewGuid().ToString();
      ContentManager manager1 = this.GetManager(providerName, transactionName);
      PageManager manager2 = PageManager.GetManager((string) null, transactionName);
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
        language = new CultureInfo(deletedLanguage);
      string empty = string.Empty;
      Stack<string> stringStack = new Stack<string>();
      foreach (string id in Ids)
      {
        try
        {
          ContentItem content = manager1.GetContent(new Guid(id));
          RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(id), content.GetType().FullName);
          manager1.DeleteItem((object) content, language);
          if (manager1.Provider.GetDirtyItemStatus((object) content) == SecurityConstants.TransactionActionType.Deleted)
          {
            ContentItem live = manager1.GetLive(content);
            if (live != null)
              manager1.UnshareContentOnAllPages(live.Id, manager2);
          }
          TransactionManager.CommitTransaction(transactionName);
        }
        catch (Exception ex)
        {
          stringStack.Push(empty);
        }
      }
      if (stringStack.Count > 0)
      {
        string notAllowToDelete = Res.Get<ContentResources>().WorkflowRulesDoNotAllowToDelete;
        StringBuilder stringBuilder = new StringBuilder();
        while (stringStack.Count > 0)
          stringBuilder.Append("\n" + stringStack.Pop());
        throw new Exception(string.Format(notAllowToDelete, (object) stringBuilder));
      }
      return true;
    }
  }
}
