// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.CommentsGenericService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  /// <summary>Service for comments</summary>
  [Obsolete("CommentsRestService should be used.")]
  public class CommentsGenericService : 
    ContentServiceBase<Comment, CommentsViewModel, CommentsManagerWrapper>
  {
    /// <summary>Gets the content items.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override IQueryable<Comment> GetContentItems(string providerName)
    {
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "LinksParser"
        });
      return this.GetManager(providerName).Provider is ContentDataProviderBase provider ? provider.GetComments() : Enumerable.Empty<Comment>().AsQueryable<Comment>();
    }

    /// <summary>Gets the child content items.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override IQueryable<Comment> GetChildContentItems(
      Guid parentId,
      string providerName)
    {
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "LinksParser"
        });
      return this.GetManager(providerName).Provider is ContentDataProviderBase provider ? provider.GetComments(parentId) : Enumerable.Empty<Comment>().AsQueryable<Comment>();
    }

    /// <summary>Gets the content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override Comment GetContentItem(Guid id, string providerName)
    {
      if (!(this.GetManager(providerName).Provider is ContentDataProviderBase provider))
        return (Comment) null;
      try
      {
        return provider.GetComment(id);
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (Comment).Name, (object) id), ex);
      }
    }

    /// <summary>Gets the parent content item.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override Comment GetParentContentItem(Guid id, string providerName) => throw new NotSupportedException();

    /// <summary>Gets the manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public override CommentsManagerWrapper GetManager(string providerName) => new CommentsManagerWrapper((IContentManager) ManagerBase.GetManager(WcfHelper.ResolveEncodedTypeName(SystemManager.CurrentHttpContext.Request.QueryString["managerType"], true, true), providerName));

    /// <summary>Gets the view model list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="liveItem">The dictionary  of live comment related to the master comment</param>
    /// <param name="tempItem">The dictionary  of temp comment related to the master comment</param>
    /// <returns></returns>
    public override IEnumerable<CommentsViewModel> GetViewModelList(
      IEnumerable<Comment> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, Comment> liveContentDictionary = null,
      IDictionary<Guid, Comment> tempContentDictionary = null)
    {
      List<CommentsViewModel> viewModelList = new List<CommentsViewModel>();
      foreach (Comment content in contentList)
      {
        CommentsViewModel commentsViewModel = new CommentsViewModel(content, dataProvider);
        viewModelList.Add(commentsViewModel);
      }
      return (IEnumerable<CommentsViewModel>) viewModelList;
    }

    /// <summary>Gets the view model list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <returns></returns>
    public override IEnumerable<CommentsViewModel> GetViewModelList(
      IEnumerable<Comment> contentList,
      ContentDataProviderBase dataProvider)
    {
      List<CommentsViewModel> viewModelList = new List<CommentsViewModel>();
      foreach (Comment content in contentList)
      {
        CommentsViewModel commentsViewModel = new CommentsViewModel(content, dataProvider);
        viewModelList.Add(commentsViewModel);
      }
      return (IEnumerable<CommentsViewModel>) viewModelList;
    }

    /// <summary>
    /// This method is obsolute for the comments that is why it is overriden to return null;
    /// </summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public override IDictionary<Guid, Comment> GetRelevantItemsList(
      List<Comment> contentList,
      CommentsManagerWrapper manager,
      ContentLifecycleStatus status)
    {
      return (IDictionary<Guid, Comment>) null;
    }
  }
}
