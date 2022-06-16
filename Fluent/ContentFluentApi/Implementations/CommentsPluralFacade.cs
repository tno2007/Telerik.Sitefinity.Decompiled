// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsPluralFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manage a collection of comments</summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  /// <typeparam name="TCommentedItem">The type of the commented item.</typeparam>
  public class CommentsPluralFacade<TParentFacade, TCommentedItem> : 
    BaseContentPluralFacadeWithoutLifeCycle<CommentsPluralFacade<TParentFacade, TCommentedItem>, CommentsSingularFacade<TParentFacade, TCommentedItem>, TParentFacade, Comment>
    where TParentFacade : BaseFacade
    where TCommentedItem : Content
  {
    private IQueryable<Comment> comments;
    private TCommentedItem commentedItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsPluralFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public CommentsPluralFacade(AppSettings settings, TCommentedItem commentedItem)
      : base(settings)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsPluralFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public CommentsPluralFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      TCommentedItem commentedItem)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<TCommentedItem>(commentedItem, nameof (commentedItem));
      this.commentedItem = commentedItem;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="M:Telerik.Sitefinity.Fluent.ContentFluentApi.CommentsPluralFacade`2.GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => ManagerBase.GetMappedManagerInTransaction(typeof (TCommentedItem), this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> BlockEmail(
      string email)
    {
      FacadeHelper.AssertIsValidEmail(email, "Parameter 'email' is not a valid email address.");
      this.GetCommentsManager().BlockCommentsForEmail(email);
      return this;
    }

    /// <summary>Blocks the comments coming from the given IP address.</summary>
    /// <param name="ip">The IP address.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> BlockIP(
      string ip)
    {
      FacadeHelper.AssertIsValidIPAddress(ip, "Parameter 'email' is not a valid IP address.");
      this.GetCommentsManager().BlockCommentsForIP(ip);
      return this;
    }

    /// <summary>
    /// Hides the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> Hide()
    {
      foreach (Comment comment in (IEnumerable<Comment>) this.Items)
      {
        comment.Visible = false;
        comment.CommentStatus = CommentStatus.Hidden;
      }
      return this;
    }

    /// <summary>
    /// Marks the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as favourite.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    /// <exception cref="T:System.NotImplementedException">the method is not implemented</exception>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> MarkAsFavourite() => throw new NotImplementedException();

    /// <summary>
    /// Marks the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as spam.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> MarkAsSpam()
    {
      foreach (Comment comment in (IEnumerable<Comment>) this.Items)
        comment.CommentStatus = CommentStatus.Spam;
      return this;
    }

    /// <summary>
    /// Shows the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsPluralFacade<TParentFacade, TCommentedItem> Show()
    {
      foreach (Comment comment in (IEnumerable<Comment>) this.Items)
      {
        comment.Visible = true;
        comment.CommentStatus = CommentStatus.Published;
      }
      return this;
    }

    /// <summary>Delete selected comments</summary>
    /// <returns>This facade</returns>
    public override CommentsPluralFacade<TParentFacade, TCommentedItem> Delete()
    {
      foreach (Comment comment in this.Items.ToList<Comment>())
        this.GetCommentsManager().Delete(comment);
      return this;
    }

    /// <summary>Gets the comments manager.</summary>
    /// <returns>Comments manager</returns>
    protected virtual ICommentsManager GetCommentsManager()
    {
      ICommentsManager manager = this.GetManager();
      FacadeHelper.AssertNotNull<ICommentsManager>(manager, "{0} does not implement {1}".Arrange((object) this.GetManager().GetType(), (object) typeof (ICommentsManager)));
      return manager;
    }

    /// <summary>Gets the commented item.</summary>
    /// <value>The commented item.</value>
    protected virtual TCommentedItem CommentedItem
    {
      get
      {
        FacadeHelper.AssertNotNull<TCommentedItem>(this.commentedItem, "Commented item can not be null");
        return this.commentedItem;
      }
    }

    protected override IQueryable<Comment> LoadItems()
    {
      Guid commentedItemID = this.CommentedItem.Id;
      string commentedItemTypeName = typeof (TCommentedItem).FullName;
      return this.GetCommentsManager().GetComments().Where<Comment>((Expression<Func<Comment, bool>>) (c => c.CommentedItemType == commentedItemTypeName && c.CommentedItemID == commentedItemID));
    }

    protected virtual ICommentsManager GetManager() => (ICommentsManager) base.GetManager();
  }
}
