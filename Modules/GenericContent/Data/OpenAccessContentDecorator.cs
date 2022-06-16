// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Data.OpenAccessContentDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Data
{
  /// <summary>
  /// Represents content decorator for OpenAccess content data provider.
  /// </summary>
  public class OpenAccessContentDecorator : IContentProviderDecorator, ICloneable
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    public virtual ContentDataProviderBase DataProvider { get; set; }

    internal virtual SitefinityOAContext Context => (SitefinityOAContext) this.DataProvider.GetTransaction();

    /// <summary>Parses the object identity.</summary>
    /// <param name="persistantType">The CLR type of the persistent object.</param>
    /// <param name="stringId">The string representation of the object identity.</param>
    /// <returns></returns>
    protected internal virtual IObjectId ParseObjectId(Type objectType, string stringId) => Database.OID.ParseObjectId(objectType, stringId);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="ip">The ip.</param>
    [Obsolete]
    public void BlockCommentsForEmail(string email) => throw new NotImplementedException();

    /// <summary>Blocks the comments coming from the given IP</summary>
    /// <param name="ip">The ip.</param>
    [Obsolete]
    public void BlockCommentsForIP(string ip) => throw new NotImplementedException();

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(ICommentable commentedItem) => this.CreateComment(commentedItem, this.DataProvider.GetNewGuid());

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(Type commentedItemType, Guid commentedItemId) => this.CreateComment(DataExtensions.AppSettings.GetContentManager(commentedItemType.FullName, this.DataProvider.Name).GetCommentedItem(commentedItemType, commentedItemId), this.DataProvider.GetNewGuid());

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="commentId">The comment id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public Comment CreateComment(ICommentable commentedItem, Guid commentId)
    {
      if (this.DataProvider.EnableCommentsBackwardCompatibility)
      {
        Comment comment1 = new Comment();
        comment1.ApplicationName = this.DataProvider.ApplicationName;
        comment1.DateCreated = DateTime.UtcNow;
        comment1.PublicationDate = DateTime.UtcNow;
        comment1.LastModified = DateTime.UtcNow;
        comment1.CommentedItem = commentedItem;
        comment1.ProviderName = this.DataProvider.Name;
        comment1.Id = commentId;
        Comment comment2 = comment1;
        if (commentedItem != null)
        {
          comment2.ParentGroupIds = this.GetParentGroupIds(comment2);
          comment2.DirtyItemState = SecurityConstants.TransactionActionType.New;
          this.Context.Add((object) comment2);
        }
        comment2.Provider = (object) this.DataProvider;
        return comment2;
      }
      return new Comment(this.DataProvider.ApplicationName, commentId)
      {
        CommentedItem = commentedItem
      };
    }

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public Comment CreateComment(
      Type commentedItemType,
      Guid commentedItemId,
      Guid commentId)
    {
      if (this.DataProvider.EnableCommentsBackwardCompatibility)
        return this.CreateComment(DataExtensions.AppSettings.GetContentManager(commentedItemType.FullName, this.DataProvider.Name).GetCommentedItem(commentedItemType, commentedItemId), commentId);
      return new Comment(this.DataProvider.ApplicationName, commentId)
      {
        CommentedItemType = commentedItemType.FullName,
        CommentedItemID = commentId
      };
    }

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    public void Delete(Comment comment)
    {
      if (!this.DataProvider.EnableCommentsBackwardCompatibility)
        return;
      this.Context.Remove((object) comment);
    }

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    internal void Delete_Old(Comment comment) => this.Context.Remove((object) comment);

    /// <summary>Gets a comment by the specified pageId.</summary>
    /// <param name="commentId">The comment pageId.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComment")]
    public Comment GetComment(Guid commentId)
    {
      if (commentId == Guid.Empty)
        throw new ArgumentNullException(nameof (commentId));
      if (!this.DataProvider.EnableCommentsBackwardCompatibility)
        throw new ItemNotFoundException("Comment.Id " + (object) commentId);
      ICommentService commentsService = SystemManager.GetCommentsService();
      IComment comment = commentsService.GetComment(commentId.ToString());
      if (comment == null)
        throw new ItemNotFoundException("Comment.Id " + (object) commentId);
      IThread thread = commentsService.GetThread(comment.ThreadKey);
      return this.GetOldComment(comment, thread);
    }

    /// <summary>Gets an IQueryable of comments.</summary>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    public IQueryable<Comment> GetComments()
    {
      List<Comment> source = new List<Comment>();
      if (this.DataProvider.EnableCommentsBackwardCompatibility)
      {
        IEnumerable<IDataSource> dataSources = SystemManager.DataSourceRegistry.GetDataSources().Where<IDataSource>((Func<IDataSource, bool>) (m => m.ProviderInfos.Any<DataProviderInfo>((Func<DataProviderInfo, bool>) (p => p.ProviderType == this.DataProvider.GetType()))));
        CommentFilter filter1 = new CommentFilter();
        foreach (IDataSource dataSource in dataSources)
        {
          string uniqueProviderKey = ControlUtilities.GetUniqueProviderKey(dataSource.Name, this.DataProvider.Name);
          filter1.GroupKey.Add(uniqueProviderKey);
        }
        ICommentService commentsService = SystemManager.GetCommentsService();
        IEnumerable<IComment> comments = commentsService.GetComments(filter1);
        if (comments.Count<IComment>() > 0)
        {
          IEnumerable<string> collection = comments.Select<IComment, string>((Func<IComment, string>) (c => c.ThreadKey)).Distinct<string>();
          ThreadFilter filter2 = new ThreadFilter();
          filter2.ThreadKey.AddRange(collection);
          IEnumerable<IThread> threads = commentsService.GetThreads(filter2);
          foreach (IComment comment in comments)
          {
            IComment c = comment;
            source.Add(this.GetOldComment(c, threads.Single<IThread>((Func<IThread, bool>) (t => t.Key == c.ThreadKey))));
          }
        }
      }
      return source.AsQueryable<Comment>();
    }

    /// <summary>Gets an IQueryable of old comments.</summary>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    internal IQueryable<Comment> GetComments_Old()
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<Comment>((DataProviderBase) this.DataProvider, MethodBase.GetCurrentMethod()).Where<Comment>((Expression<Func<Comment, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Gets an IQueryable of comments for specified id of item that implements <see cref="T:Telerik.Sitefinity.GenericContent.Model.ICommentable" />.
    /// </summary>
    /// <returns>IQueryable of comments</returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    public IQueryable<Comment> GetComments(Guid commentableItemId)
    {
      List<Comment> source = new List<Comment>();
      if (this.DataProvider.EnableCommentsBackwardCompatibility)
      {
        string localizedKey = ControlUtilities.GetLocalizedKey((object) commentableItemId);
        ICommentService commentsService = SystemManager.GetCommentsService();
        IThread thread = commentsService.GetThread(localizedKey);
        if (thread != null)
        {
          foreach (IComment comment in commentsService.GetComments(new CommentFilter()
          {
            ThreadKey = {
              localizedKey
            }
          }))
            source.Add(this.GetOldComment(comment, thread));
        }
      }
      return source.AsQueryable<Comment>();
    }

    private Comment GetOldComment(IComment newComment, IThread parentThread)
    {
      Guid id = newComment.Key.IsGuid() ? Guid.Parse(newComment.Key) : Guid.Empty;
      Comment commentOld = this.Context.GetAll<Comment>().FirstOrDefault<Comment>((Expression<Func<Comment, bool>>) (c => c.Id == id));
      if (commentOld == null)
      {
        commentOld = new Comment(this.DataProvider.ApplicationName, id);
        this.Context.Add((object) commentOld);
        CommentsUtilities.Populate(ref commentOld, newComment, parentThread.Type, parentThread.DataSource);
        commentOld.DirtyItemState = SecurityConstants.TransactionActionType.None;
        commentOld.Provider = (object) this.DataProvider;
      }
      return commentOld;
    }

    /// <summary>
    /// Gets or sets the IDs of the parents of the commented item passed as an argument
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual IList<Guid> GetParentGroupIds(Comment comment)
    {
      List<Guid> guidList = new List<Guid>();
      guidList.Add(comment.CommentedItemID);
      if (comment.CommentedItem is IHasParent)
      {
        for (Content content = ((IHasParent) comment.CommentedItem).Parent; content != null; content = !(content is IHasParent) ? (Content) null : ((IHasParent) content).Parent)
          guidList.Add(content.Id);
      }
      return guidList.Count <= 0 ? (IList<Guid>) null : (IList<Guid>) guidList;
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public object Clone() => this.MemberwiseClone();
  }
}
