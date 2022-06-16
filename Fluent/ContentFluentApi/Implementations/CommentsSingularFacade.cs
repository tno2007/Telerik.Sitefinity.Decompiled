// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manage individual comments</summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  /// <typeparam name="TCommentedItem">The type of the commented item.</typeparam>
  public class CommentsSingularFacade<TParentFacade, TCommentedItem> : 
    BaseContentSingularFacadeWithoutLifeCycle<CommentsSingularFacade<TParentFacade, TCommentedItem>, TParentFacade, Comment>
    where TParentFacade : BaseFacade
    where TCommentedItem : Content
  {
    private ICommentsManager commentsManager;
    private TCommentedItem commentedItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="commentedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public CommentsSingularFacade(AppSettings settings, TCommentedItem commentedItem)
      : base(settings)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="commentedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public CommentsSingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      TCommentedItem commentedItem)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="commentedItem" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When itemId is emtpy Guid</exception>
    public CommentsSingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      TCommentedItem commentedItem,
      Guid itemID)
      : base(settings, parentFacade, itemID)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="comment">Content item to be the initial state of the facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="comment" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public CommentsSingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      TCommentedItem commentedItem,
      Comment comment)
      : base(settings, parentFacade, comment)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="M:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsSingularFacade`2.GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => ManagerBase.GetMappedManagerInTransaction(typeof (TCommentedItem), this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> BlockEmail(
      string email)
    {
      this.GetCommentsManager().BlockCommentsForEmail(email);
      return this;
    }

    /// <summary>Blocks the comments coming from the given IP address.</summary>
    /// <param name="ip">The IP address.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> BlockIP(
      string ip)
    {
      this.GetCommentsManager().BlockCommentsForIP(ip);
      return this;
    }

    /// <summary>Create a new item with random ID</summary>
    /// <returns>This facade</returns>
    public override CommentsSingularFacade<TParentFacade, TCommentedItem> CreateNew()
    {
      this.Item = this.GetManager().CreateComment((ICommentable) this.CommentedItem);
      return this;
    }

    protected ICommentsManager GetManager() => (ICommentsManager) base.GetManager();

    /// <summary>Create a new item with specific ID</summary>
    /// <param name="itemId">ID of the item to create</param>
    /// <returns>This facade</returns>
    public override CommentsSingularFacade<TParentFacade, TCommentedItem> CreateNew(
      Guid itemId)
    {
      this.Item = this.GetManager().CreateComment((ICommentable) this.CommentedItem, itemId);
      return this;
    }

    /// <summary>
    /// Hides the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> Hide()
    {
      this.Item.Visible = false;
      this.Item.CommentStatus = CommentStatus.Hidden;
      return this;
    }

    /// <summary>Delete the currently loaded item</summary>
    /// <returns>This facade</returns>
    public override CommentsSingularFacade<TParentFacade, TCommentedItem> Delete()
    {
      this.CommentedItem.Comments.Remove(this.Get());
      return base.Delete();
    }

    /// <summary>
    /// Marks the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as favourite.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    /// <exception cref="T:System.NotImplementedException">the method is not implemented</exception>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> MarkAsFavourite() => throw new NotImplementedException();

    /// <summary>
    /// Marks the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as spam.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> MarkAsSpam()
    {
      this.Item.CommentStatus = CommentStatus.Spam;
      return this;
    }

    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> Reply(
      Guid commentId)
    {
      FacadeHelper.Assert<ArgumentException>(commentId != Guid.Empty, "Can not replay to a comment with empty GUID");
      FacadeHelper.AssertNotNull<Comment>(this.GetCommentsManager().GetComment(commentId), "Can not reply to a comment that does not exist");
      this.Item.CommentedItemType = typeof (Comment).AssemblyQualifiedName;
      this.Item.CommentedItemID = commentId;
      return this;
    }

    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> Reply(
      Comment reply)
    {
      FacadeHelper.AssertArgumentNotNull<Comment>(reply, nameof (reply));
      reply.CommentedItemType = typeof (Comment).AssemblyQualifiedName;
      reply.CommentedItemID = this.Item.Id;
      return this;
    }

    /// <summary>
    /// Shows the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentsSingularFacade<TParentFacade, TCommentedItem> Show()
    {
      this.Item.Visible = true;
      this.Item.CommentStatus = CommentStatus.Published;
      return this;
    }

    /// <summary>Gets the commented item.</summary>
    /// <value>The commented item.</value>
    /// <exception cref="T:System.InvalidOperationException">If getting and returned value is null</exception>
    protected virtual TCommentedItem CommentedItem
    {
      get
      {
        FacadeHelper.AssertNotNull<TCommentedItem>(this.commentedItem, "Commented item can not be null");
        return this.commentedItem;
      }
    }

    /// <summary>Cached access to the comments manager</summary>
    /// <returns>Comments manager</returns>
    protected virtual ICommentsManager GetCommentsManager()
    {
      if (this.commentsManager == null)
      {
        this.commentsManager = this.GetManager();
        FacadeHelper.AssertNotNull<ICommentsManager>(this.commentsManager, "{0} does not implement {1}".Arrange((object) this.GetManager().GetType(), (object) typeof (ICommentsManager)));
      }
      return this.commentsManager;
    }
  }
}
